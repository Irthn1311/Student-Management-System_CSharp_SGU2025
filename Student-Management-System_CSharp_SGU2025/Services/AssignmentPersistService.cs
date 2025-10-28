using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.Services
{
	public class AssignmentPersistService
	{
		public void PersistTemporary(List<PhanCongCandidate> list)
		{
			const string clearSql = "DELETE FROM PhanCong_Temp";
			const string insertSql = @"INSERT INTO PhanCong_Temp(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, SoTietTuan, Score, Note)
				VALUES(@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @SoTietTuan, @Score, @Note)";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var tx = conn.BeginTransaction())
				{
					try
					{
						using (var clear = new MySqlCommand(clearSql, conn, tx))
						{
							clear.ExecuteNonQuery();
						}
						foreach (var c in list)
						{
							using (var cmd = new MySqlCommand(insertSql, conn, tx))
							{
								cmd.Parameters.AddWithValue("@MaLop", c.MaLop);
								cmd.Parameters.AddWithValue("@MaGiaoVien", c.MaGiaoVien);
								cmd.Parameters.AddWithValue("@MaMonHoc", c.MaMonHoc);
								cmd.Parameters.AddWithValue("@MaHocKy", GetActiveHocKy(conn, tx));
								cmd.Parameters.AddWithValue("@SoTietTuan", c.SoTietTuan);
								cmd.Parameters.AddWithValue("@Score", c.Score);
								cmd.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(c.Note) ? (object)DBNull.Value : c.Note);
								cmd.ExecuteNonQuery();
							}
						}
						tx.Commit();
					}
					catch
					{
						tx.Rollback();
						throw;
					}
				}
			}
		}

        public void AcceptToOfficial(int hocKyId)
		{
            const string insertSql = @"INSERT IGNORE INTO PhanCongGiangDay(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc)
                SELECT MaLop, MaGiaoVien, MaMonHoc, MaHocKy, CURDATE(), CURDATE()
                FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
			const string clearSql = "DELETE FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var tx = conn.BeginTransaction())
				{
					try
					{
						using (var cmd = new MySqlCommand(insertSql, conn, tx))
						{
							cmd.Parameters.AddWithValue("@MaHocKy", hocKyId);
							cmd.ExecuteNonQuery();
						}
						using (var clr = new MySqlCommand(clearSql, conn, tx))
						{
							clr.Parameters.AddWithValue("@MaHocKy", hocKyId);
							clr.ExecuteNonQuery();
						}
						tx.Commit();
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
			// Placeholder: in real app, get from context/UI. Here we default to max MaHocKy.
			using (var cmd = new MySqlCommand("SELECT COALESCE(MAX(MaHocKy),1) FROM HocKy", conn, tx))
			{
				return Convert.ToInt32(cmd.ExecuteScalar());
			}
		}
	}
}


