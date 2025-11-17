using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.Scheduling;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
//using Student_Management_System_CSharp_SGU2025.ConnectDatabase;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
	internal class ThoiKhoaBieuDAO
	{
		public void ClearTemp()
		{
			const string sql = "DELETE FROM TKB_Temp";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}

		public void InsertTemp(int semesterId, int weekNo, ScheduleSolution solution)
		{
			const string sql = @"INSERT INTO TKB_Temp(SemesterId, WeekNo, MaLop, Thu, Tiet, MaMon, MaGV, Phong)
				VALUES (@SemesterId, @WeekNo, @MaLop, @Thu, @Tiet, @MaMon, @MaGV, @Phong)";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var tx = conn.BeginTransaction())
				{
					try
					{
						foreach (var s in solution.Slots)
						{
							using (var cmd = new MySqlCommand(sql, conn, tx))
							{
								cmd.Parameters.AddWithValue("@SemesterId", semesterId);
								cmd.Parameters.AddWithValue("@WeekNo", weekNo);
								cmd.Parameters.AddWithValue("@MaLop", s.MaLop);
								cmd.Parameters.AddWithValue("@Thu", s.Thu);
								cmd.Parameters.AddWithValue("@Tiet", s.Tiet);
								cmd.Parameters.AddWithValue("@MaMon", s.MaMon);
								cmd.Parameters.AddWithValue("@MaGV", s.MaGV);
								cmd.Parameters.AddWithValue("@Phong", string.IsNullOrEmpty(s.Phong) ? (object)DBNull.Value : s.Phong);
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

		public void AcceptTempToOfficial(int semesterId, int weekNo)
		{
			// Map TKB_Temp rows to official ThoiKhoaBieu through PhanCongGiangDay to obtain MaPhanCong.
			// Assumption: a unique PhanCongGiangDay exists for (MaLop, MaGV, MaMon, MaHocKy=SemesterId)
			const string insertSql = @"
				INSERT INTO ThoiKhoaBieu(MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc)
				SELECT pc.MaPhanCong,
					CASE WHEN t.Thu IN (2,3,4,5,6,7) THEN CONCAT('Thu ', t.Thu) ELSE CAST(t.Thu AS CHAR) END AS ThuTrongTuan,
					t.Tiet AS TietBatDau,
					1 AS SoTiet,
					t.Phong
				FROM TKB_Temp t
				JOIN PhanCongGiangDay pc ON pc.MaLop = t.MaLop AND pc.MaGiaoVien = t.MaGV AND pc.MaMonHoc = t.MaMon AND pc.MaHocKy = @SemesterId
				WHERE t.SemesterId = @SemesterId AND t.WeekNo = @WeekNo;";

			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var tx = conn.BeginTransaction())
				{
					try
					{
						using (var cmd = new MySqlCommand(insertSql, conn, tx))
						{
							cmd.Parameters.AddWithValue("@SemesterId", semesterId);
							cmd.Parameters.AddWithValue("@WeekNo", weekNo);
							cmd.ExecuteNonQuery();
						}

						using (var clear = new MySqlCommand("DELETE FROM TKB_Temp WHERE SemesterId=@SemesterId AND WeekNo=@WeekNo", conn, tx))
						{
							clear.Parameters.AddWithValue("@SemesterId", semesterId);
							clear.Parameters.AddWithValue("@WeekNo", weekNo);
							clear.ExecuteNonQuery();
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

		public List<AssignmentSlot> GetWeek(int semesterId, int weekNo)
		{
			const string sql = @"SELECT SemesterId, WeekNo, MaLop, Thu, Tiet, MaMon, MaGV, Phong FROM TKB_Temp WHERE SemesterId=@SemesterId AND WeekNo=@WeekNo";
			var result = new List<AssignmentSlot>();
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", semesterId);
					cmd.Parameters.AddWithValue("@WeekNo", weekNo);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							result.Add(new AssignmentSlot
							{
								MaLop = reader.GetInt32("MaLop"),
								Thu = reader.GetInt32("Thu"),
								Tiet = reader.GetInt32("Tiet"),
								MaMon = reader.GetInt32("MaMon"),
								MaGV = reader.GetString("MaGV"),
								Phong = reader.IsDBNull(reader.GetOrdinal("Phong")) ? string.Empty : reader.GetString("Phong")
							});
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Lấy TKB của 1 lớp cụ thể (từ temp hoặc official)
		/// </summary>
		public List<AssignmentSlot> GetWeekByClass(int semesterId, int weekNo, int maLop)
		{
			const string sql = @"SELECT SemesterId, WeekNo, MaLop, Thu, Tiet, MaMon, MaGV, Phong 
								 FROM TKB_Temp 
								 WHERE SemesterId=@SemesterId AND WeekNo=@WeekNo AND MaLop=@MaLop";
			var result = new List<AssignmentSlot>();
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", semesterId);
					cmd.Parameters.AddWithValue("@WeekNo", weekNo);
					cmd.Parameters.AddWithValue("@MaLop", maLop);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							result.Add(new AssignmentSlot
							{
								MaLop = reader.GetInt32("MaLop"),
								Thu = reader.GetInt32("Thu"),
								Tiet = reader.GetInt32("Tiet"),
								MaMon = reader.GetInt32("MaMon"),
								MaGV = reader.GetString("MaGV"),
								Phong = reader.IsDBNull(reader.GetOrdinal("Phong")) ? string.Empty : reader.GetString("Phong")
							});
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Kiểm tra xem học kỳ đã có TKB chưa (temp hoặc official)
		/// </summary>
		public bool HasScheduleForSemester(int semesterId)
		{
			const string sqlTemp = @"SELECT COUNT(*) FROM TKB_Temp WHERE SemesterId=@SemesterId";
			const string sqlOfficial = @"SELECT COUNT(*) 
										 FROM ThoiKhoaBieu tkb
										 JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
										 WHERE pc.MaHocKy = @SemesterId";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				
				// Check temp first
				using (var cmd = new MySqlCommand(sqlTemp, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", semesterId);
					int count = Convert.ToInt32(cmd.ExecuteScalar());
					if (count > 0) return true;
				}
				
				// Check official
				using (var cmd = new MySqlCommand(sqlOfficial, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", semesterId);
					int count = Convert.ToInt32(cmd.ExecuteScalar());
					return count > 0;
				}
			}
		}

		/// <summary>
		/// Lấy TKB chính thức của học kỳ và lớp
		/// </summary>
		public List<AssignmentSlot> GetOfficialSchedule(int semesterId, int? maLop = null)
		{
			string sql = @"SELECT pc.MaLop, 
								  CAST(SUBSTRING_INDEX(tkb.ThuTrongTuan, ' ', -1) AS SIGNED) AS Thu,
								  tkb.TietBatDau AS Tiet,
								  pc.MaMonHoc AS MaMon,
								  pc.MaGiaoVien AS MaGV,
								  tkb.PhongHoc AS Phong
						   FROM ThoiKhoaBieu tkb
						   JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
						   WHERE pc.MaHocKy = @SemesterId";
			
			if (maLop.HasValue && maLop.Value > 0)
			{
				sql += " AND pc.MaLop = @MaLop";
			}

			var result = new List<AssignmentSlot>();
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", semesterId);
					if (maLop.HasValue && maLop.Value > 0)
					{
						cmd.Parameters.AddWithValue("@MaLop", maLop.Value);
					}
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							result.Add(new AssignmentSlot
							{
								MaLop = reader.GetInt32("MaLop"),
								Thu = reader.GetInt32("Thu"),
								Tiet = reader.GetInt32("Tiet"),
								MaMon = reader.GetInt32("MaMon"),
								MaGV = reader.GetString("MaGV"),
								Phong = reader.IsDBNull(reader.GetOrdinal("Phong")) ? string.Empty : reader.GetString("Phong")
							});
						}
					}
				}
			}
			return result;
		}

		public bool HasConflict(int semesterId, int weekNo, AssignmentSlot slot)
		{
			const string sql = @"
				SELECT 1 FROM (
					SELECT MaLop, Thu, Tiet, MaMon, MaGV FROM TKB_Temp WHERE SemesterId=@SemesterId AND WeekNo=@WeekNo
					UNION ALL
					SELECT pc.MaLop,
						CAST(SUBSTRING_INDEX(tkb.ThuTrongTuan, ' ', -1) AS SIGNED) AS Thu,
						tkb.TietBatDau AS Tiet,
						pc.MaMonHoc AS MaMon,
						pc.MaGiaoVien AS MaGV
					FROM ThoiKhoaBieu tkb
					JOIN PhanCongGiangDay pc ON pc.MaPhanCong = tkb.MaPhanCong AND pc.MaHocKy=@SemesterId
				) x
				WHERE (x.MaGV=@MaGV OR x.MaLop=@MaLop) AND x.Thu=@Thu AND x.Tiet=@Tiet
				LIMIT 1;";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", semesterId);
					cmd.Parameters.AddWithValue("@WeekNo", weekNo);
					cmd.Parameters.AddWithValue("@MaGV", slot.MaGV);
					cmd.Parameters.AddWithValue("@MaLop", slot.MaLop);
					cmd.Parameters.AddWithValue("@Thu", slot.Thu);
					cmd.Parameters.AddWithValue("@Tiet", slot.Tiet);
					using (var reader = cmd.ExecuteReader())
					{
						return reader.Read();
					}
				}
			}
		}

		/// <summary>
		/// Kiểm tra lớp đã có tiết học tại vị trí (học kỳ, thứ, tiết) chưa.
		/// Spec: ExistsLop(maHocKy, thu, tiet, maLop)
		/// </summary>
		public bool ExistsLop(int maHocKy, int thu, int tiet, int maLop)
		{
			const string sql = @"
				SELECT 1 FROM (
					SELECT MaLop, Thu, Tiet FROM TKB_Temp WHERE SemesterId=@SemesterId
					UNION ALL
					SELECT pc.MaLop,
						CAST(SUBSTRING_INDEX(tkb.ThuTrongTuan, ' ', -1) AS SIGNED) AS Thu,
						tkb.TietBatDau AS Tiet
					FROM ThoiKhoaBieu tkb
					JOIN PhanCongGiangDay pc ON pc.MaPhanCong = tkb.MaPhanCong AND pc.MaHocKy=@MaHocKy
				) x
				WHERE x.MaLop=@MaLop AND x.Thu=@Thu AND x.Tiet=@Tiet
				LIMIT 1;";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", maHocKy);
					cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
					cmd.Parameters.AddWithValue("@MaLop", maLop);
					cmd.Parameters.AddWithValue("@Thu", thu);
					cmd.Parameters.AddWithValue("@Tiet", tiet);
					using (var reader = cmd.ExecuteReader())
					{
						return reader.Read();
					}
				}
			}
		}

		/// <summary>
		/// Kiểm tra giáo viên đã dạy tại vị trí (học kỳ, thứ, tiết) chưa.
		/// Spec: ExistsGV(maHocKy, thu, tiet, maGiaoVien)
		/// </summary>
		public bool ExistsGV(int maHocKy, int thu, int tiet, string maGiaoVien)
		{
			const string sql = @"
				SELECT 1 FROM (
					SELECT MaGV, Thu, Tiet FROM TKB_Temp WHERE SemesterId=@SemesterId
					UNION ALL
					SELECT pc.MaGiaoVien AS MaGV,
						CAST(SUBSTRING_INDEX(tkb.ThuTrongTuan, ' ', -1) AS SIGNED) AS Thu,
						tkb.TietBatDau AS Tiet
					FROM ThoiKhoaBieu tkb
					JOIN PhanCongGiangDay pc ON pc.MaPhanCong = tkb.MaPhanCong AND pc.MaHocKy=@MaHocKy
				) x
				WHERE x.MaGV=@MaGV AND x.Thu=@Thu AND x.Tiet=@Tiet
				LIMIT 1;";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", maHocKy);
					cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
					cmd.Parameters.AddWithValue("@MaGV", maGiaoVien);
					cmd.Parameters.AddWithValue("@Thu", thu);
					cmd.Parameters.AddWithValue("@Tiet", tiet);
					using (var reader = cmd.ExecuteReader())
					{
						return reader.Read();
					}
				}
			}
		}

		/// <summary>
		/// Bulk replace: Xóa TKB cũ của học kỳ và ghi mới (transaction safe)
		/// Spec: BulkReplace(maHocKy, rows)
		/// </summary>
		public void BulkReplace(int maHocKy, List<AssignmentSlot> slots)
		{
			const string deleteSql = @"
				DELETE tkb FROM ThoiKhoaBieu tkb
				JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
				WHERE pc.MaHocKy = @MaHocKy";

			const string insertSql = @"
				INSERT INTO ThoiKhoaBieu(MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc)
				SELECT pc.MaPhanCong,
					CASE WHEN @Thu IN (2,3,4,5,6,7) THEN CONCAT('Thu ', @Thu) ELSE CAST(@Thu AS CHAR) END,
					@Tiet, 1, @Phong
				FROM PhanCongGiangDay pc
				WHERE pc.MaLop = @MaLop AND pc.MaMonHoc = @MaMon AND pc.MaGiaoVien = @MaGV AND pc.MaHocKy = @MaHocKy
				LIMIT 1";

			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var tx = conn.BeginTransaction())
				{
					try
					{
						// Xóa TKB cũ
						using (var delCmd = new MySqlCommand(deleteSql, conn, tx))
						{
							delCmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
							delCmd.ExecuteNonQuery();
						}

						// Ghi TKB mới
						foreach (var slot in slots)
						{
							using (var insCmd = new MySqlCommand(insertSql, conn, tx))
							{
								insCmd.Parameters.AddWithValue("@Thu", slot.Thu);
								insCmd.Parameters.AddWithValue("@Tiet", slot.Tiet);
								insCmd.Parameters.AddWithValue("@Phong", string.IsNullOrEmpty(slot.Phong) ? (object)DBNull.Value : slot.Phong);
								insCmd.Parameters.AddWithValue("@MaLop", slot.MaLop);
								insCmd.Parameters.AddWithValue("@MaMon", slot.MaMon);
								insCmd.Parameters.AddWithValue("@MaGV", slot.MaGV);
								insCmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
								insCmd.ExecuteNonQuery();
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
	}
}


