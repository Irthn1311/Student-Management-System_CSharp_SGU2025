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
			
			// ✅ Kiểm tra có dữ liệu tạm không trước khi bắt đầu transaction
			const string checkSql = "SELECT COUNT(*) FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
			using (var checkConn = ConnectionDatabase.GetConnection())
			{
				checkConn.Open();
				using (var checkCmd = new MySqlCommand(checkSql, checkConn))
				{
					checkCmd.Parameters.AddWithValue("@MaHocKy", hocKyId);
					int tempCount = Convert.ToInt32(checkCmd.ExecuteScalar());
					if (tempCount == 0)
					{
						throw new InvalidOperationException("❌ Không có dữ liệu phân công tạm để chấp nhận!\n\nVui lòng tạo và lưu tạm phân công trước.");
					}
				}
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
						
						// ✅ Kiểm tra rowsAffected TRƯỚC khi commit
						if (rowsAffected == 0)
						{
							tx.Rollback();
							throw new InvalidOperationException("❌ Không có dữ liệu nào được lưu!\n\nKiểm tra:\n1. Bảng PhanCong_Temp có dữ liệu không?\n2. Học kỳ có đúng không?\n3. Có thể dữ liệu đã tồn tại trong PhanCongGiangDay.");
						}
						
						Console.WriteLine($"✅ Đã lưu {rowsAffected} phân công vào PhanCongGiangDay (HocKy: {hocKyId})");
						
						using (var clr = new MySqlCommand(clearSql, conn, tx))
						{
							clr.Parameters.AddWithValue("@MaHocKy", hocKyId);
							clr.ExecuteNonQuery();
						}
						
						// ✅ Commit chỉ một lần
						tx.Commit();
						Console.WriteLine($"✅ Đã xóa dữ liệu tạm cho HocKy {hocKyId}");
					}
					catch (Exception ex)
					{
						// ✅ Chỉ rollback nếu transaction chưa được commit/rollback
						try
						{
							if (tx != null && tx.Connection != null && tx.Connection.State == System.Data.ConnectionState.Open)
							{
								tx.Rollback();
							}
						}
						catch (Exception rollbackEx)
						{
							Console.WriteLine($"⚠️ Lỗi khi rollback transaction: {rollbackEx.Message}");
						}
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


