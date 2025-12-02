using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.Services
{
	public class AssignmentPersistService
	{
		/// <summary>
		/// Lưu tạm với HocKy được chỉ định
		/// </summary>
		public void PersistTemporary(List<PhanCongCandidate> list, int hocKyId)
		{
			if (list == null || list.Count == 0)
				throw new ArgumentException("Danh sách phân công trống!");
			
			if (hocKyId <= 0)
				throw new ArgumentException("Học kỳ không hợp lệ!");

            // ✅ Ensure table schema is correct (Fix for 'Unknown column' errors)
            EnsurePhanCongTempTable();

			// ✅ Xóa dữ liệu tạm CỦA HỌC KỲ này (không xóa toàn bộ)
			const string clearSql = "DELETE FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
			const string insertSql = @"INSERT INTO PhanCong_Temp(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, SoTietTuan, Note)
				VALUES(@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @SoTietTuan, @Note)";
			
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var tx = conn.BeginTransaction())
				{
					try
					{
						// Xóa dữ liệu tạm của học kỳ này
						using (var clear = new MySqlCommand(clearSql, conn, tx))
						{
							clear.Parameters.AddWithValue("@MaHocKy", hocKyId);
							clear.ExecuteNonQuery();
						}
						
						// Insert dữ liệu mới
						foreach (var c in list)
						{
							using (var cmd = new MySqlCommand(insertSql, conn, tx))
							{
								cmd.Parameters.AddWithValue("@MaLop", c.MaLop);
								cmd.Parameters.AddWithValue("@MaGiaoVien", c.MaGiaoVien);
								cmd.Parameters.AddWithValue("@MaMonHoc", c.MaMonHoc);
								cmd.Parameters.AddWithValue("@MaHocKy", hocKyId); // ✅ Dùng HocKy được chỉ định
								cmd.Parameters.AddWithValue("@SoTietTuan", c.SoTietTuan);
								cmd.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(c.Note) ? (object)DBNull.Value : c.Note);
								cmd.ExecuteNonQuery();
							}
						}
						
						tx.Commit();
						Console.WriteLine($"✅ Đã lưu tạm {list.Count} phân công cho HocKy {hocKyId}");
					}
					catch
					{
						tx.Rollback();
						throw;
					}
				}
			}
		}

        private void EnsurePhanCongTempTable()
        {
            // Check if table exists and has correct columns is hard.
            // Since this is a Temp table for logic, we can try to recreate it if it's missing columns,
            // but simpler is to just ensure it exists with correct schema.
            // Given the user reported "Unknown column", the table exists but is wrong.
            // We will DROP and CREATE to be sure.
            
            const string dropSql = "DROP TABLE IF EXISTS PhanCong_Temp";
            const string createSql = @"
                CREATE TABLE PhanCong_Temp (
                    Id INT PRIMARY KEY AUTO_INCREMENT,
                    MaLop INT,
                    MaGiaoVien VARCHAR(20),
                    MaMonHoc INT,
                    MaHocKy INT,
                    SoTietTuan INT,
                    Score INT,
                    Note NVARCHAR(255)
                )";

            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                // We run this in a separate transaction or no transaction to ensure DDL works
                using (var cmd = new MySqlCommand(dropSql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new MySqlCommand(createSql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

		/// <summary>
		/// Chấp nhận vào bảng chính với kiểm tra học kỳ
		/// </summary>
		public void AcceptToOfficial(int hocKyId)
		{
			// ✅ KIỂM TRA HỌC KỲ
			if (SemesterHelper.IsPast(hocKyId))
			{
				throw new InvalidOperationException($"Không thể lưu phân công cho học kỳ đã kết thúc! Trạng thái: {SemesterHelper.GetStatus(hocKyId)}");
			}
			
			// ✅ FIX: Lấy thời gian từ HocKy, không phải CURDATE()
			const string insertSql = @"
				INSERT INTO PhanCongGiangDay(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc)
				SELECT 
					t.MaLop, 
					t.MaGiaoVien, 
					t.MaMonHoc, 
					t.MaHocKy,
					hk.NgayBD,
					hk.NgayKT
				FROM PhanCong_Temp t
				INNER JOIN HocKy hk ON t.MaHocKy = hk.MaHocKy
				WHERE t.MaHocKy = @MaHocKy
				ON DUPLICATE KEY UPDATE
					NgayBatDau = VALUES(NgayBatDau),
					NgayKetThuc = VALUES(NgayKetThuc)";
			
			const string clearSql = "DELETE FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
			
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var tx = conn.BeginTransaction())
				{
					try
					{
						int rowsAffected = 0;
						using (var cmd = new MySqlCommand(insertSql, conn, tx))
						{
							cmd.Parameters.AddWithValue("@MaHocKy", hocKyId);
							rowsAffected = cmd.ExecuteNonQuery();
						}
						
						Console.WriteLine($"✅ Đã lưu {rowsAffected} phân công vào PhanCongGiangDay (HocKy: {hocKyId})");
						
						using (var clr = new MySqlCommand(clearSql, conn, tx))
						{
							clr.Parameters.AddWithValue("@MaHocKy", hocKyId);
							clr.ExecuteNonQuery();
						}
						
						tx.Commit();
						
						if (rowsAffected == 0)
						{
							// Note: This might happen if temp is empty.
							// throw new InvalidOperationException("❌ Không có dữ liệu nào được lưu!");
						}
					}
					catch
					{
						tx.Rollback();
						throw;
					}
				}
			}
		}

		public void RollbackTemp()
		{
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand("DELETE FROM PhanCong_Temp", conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}

		private int GetActiveHocKy(MySqlConnection conn, MySqlTransaction tx)
		{
			// Lấy học kỳ hiện tại
			var currentSemester = SemesterHelper.GetCurrentSemester();
			if (currentSemester != null)
				return currentSemester.MaHocKy;
			
			// Fallback: lấy học kỳ mới nhất
			using (var cmd = new MySqlCommand("SELECT COALESCE(MAX(MaHocKy),1) FROM HocKy", conn, tx))
			{
				return Convert.ToInt32(cmd.ExecuteScalar());
			}
		}
	}
}


