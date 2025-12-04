using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.Scheduling;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

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

		/// <summary>
		/// Lấy danh sách thời khóa biểu theo học kỳ với đầy đủ thông tin (JOIN)
		/// Trả về List<TimeTableSlotDTO> để hiển thị trên lưới TKB
		/// </summary>
		/// <param name="maHocKy">Mã học kỳ</param>
		/// <returns>Danh sách các ô thời khóa biểu</returns>
		public List<TimeTableSlotDTO> GetTKBViewByHocKy(int maHocKy)
		{
			const string sql = @"
				SELECT 
					tkb.MaThoiKhoaBieu, 
					CAST(SUBSTRING_INDEX(tkb.ThuTrongTuan, ' ', -1) AS SIGNED) AS Thu, 
					tkb.TietBatDau AS Tiet,
					pc.MaPhanCong, 
					l.TenLop, 
					l.MaLop, 
					mh.TenMonHoc AS TenMon, 
					gv.HoTen AS TenGiaoVien, 
					gv.MaGiaoVien
				FROM ThoiKhoaBieu tkb
				JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
				JOIN LopHoc l ON pc.MaLop = l.MaLop
				JOIN MonHoc mh ON pc.MaMonHoc = mh.MaMonHoc
				JOIN GiaoVien gv ON pc.MaGiaoVien = gv.MaGiaoVien
				WHERE pc.MaHocKy = @MaHocKy
				ORDER BY tkb.ThuTrongTuan, tkb.TietBatDau";

			var result = new List<TimeTableSlotDTO>();
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var slot = new TimeTableSlotDTO
							{
								MaThoiKhoaBieu = reader.GetInt32("MaThoiKhoaBieu"),
								MaPhanCong = reader.GetInt32("MaPhanCong"),
								Thu = reader.GetInt32("Thu"),
								Tiet = reader.GetInt32("Tiet"),
								TenLop = reader.IsDBNull(reader.GetOrdinal("TenLop")) ? string.Empty : reader.GetString("TenLop"),
								MaLop = reader.GetInt32("MaLop"),
								TenMon = reader.IsDBNull(reader.GetOrdinal("TenMon")) ? string.Empty : reader.GetString("TenMon"),
								TenGiaoVien = reader.IsDBNull(reader.GetOrdinal("TenGiaoVien")) ? string.Empty : reader.GetString("TenGiaoVien"),
								MaGiaoVien = reader.IsDBNull(reader.GetOrdinal("MaGiaoVien")) ? string.Empty : reader.GetString("MaGiaoVien")
							};
							result.Add(slot);
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Kiểm tra lớp có bận tại vị trí (thứ, tiết) không
		/// Logic-based validation: Sử dụng SQL JOIN thay vì DB constraint
		/// </summary>
		/// <param name="maLop">Mã lớp cần kiểm tra</param>
		/// <param name="thu">Thứ trong tuần (2-6)</param>
		/// <param name="tiet">Tiết bắt đầu (1-10)</param>
		/// <param name="excludeId">Mã thời khóa biểu cần loại trừ (khi đang sửa)</param>
		/// <returns>True nếu lớp đã bận, False nếu rảnh</returns>
		public bool CheckClassBusy(int maLop, int thu, int tiet, int excludeId = 0)
		{
			const string sql = @"
				SELECT COUNT(*) 
				FROM ThoiKhoaBieu tkb
				JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
				WHERE pc.MaLop = @MaLop 
					AND CAST(SUBSTRING_INDEX(tkb.ThuTrongTuan, ' ', -1) AS SIGNED) = @Thu 
					AND tkb.TietBatDau = @Tiet 
					AND (@ExcludeId = 0 OR tkb.MaThoiKhoaBieu != @ExcludeId)";

			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MaLop", maLop);
					cmd.Parameters.AddWithValue("@Thu", thu);
					cmd.Parameters.AddWithValue("@Tiet", tiet);
					cmd.Parameters.AddWithValue("@ExcludeId", excludeId);
					
					long count = (long)cmd.ExecuteScalar();
					return count > 0;
				}
			}
		}

		/// <summary>
		/// Kiểm tra giáo viên có bận tại vị trí (thứ, tiết) không
		/// Logic-based validation: Sử dụng SQL JOIN thay vì DB constraint
		/// </summary>
		/// <param name="maGV">Mã giáo viên cần kiểm tra</param>
		/// <param name="thu">Thứ trong tuần (2-6)</param>
		/// <param name="tiet">Tiết bắt đầu (1-10)</param>
		/// <param name="excludeId">Mã thời khóa biểu cần loại trừ (khi đang sửa)</param>
		/// <returns>True nếu giáo viên đã bận, False nếu rảnh</returns>
		public bool CheckTeacherBusy(string maGV, int thu, int tiet, int excludeId = 0)
		{
			if (string.IsNullOrWhiteSpace(maGV))
				throw new ArgumentException("Mã giáo viên không được để trống");

			const string sql = @"
				SELECT COUNT(*) 
				FROM ThoiKhoaBieu tkb
				JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
				WHERE pc.MaGiaoVien = @MaGV 
					AND CAST(SUBSTRING_INDEX(tkb.ThuTrongTuan, ' ', -1) AS SIGNED) = @Thu 
					AND tkb.TietBatDau = @Tiet 
					AND (@ExcludeId = 0 OR tkb.MaThoiKhoaBieu != @ExcludeId)";

			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MaGV", maGV);
					cmd.Parameters.AddWithValue("@Thu", thu);
					cmd.Parameters.AddWithValue("@Tiet", tiet);
					cmd.Parameters.AddWithValue("@ExcludeId", excludeId);
					
					long count = (long)cmd.ExecuteScalar();
					return count > 0;
				}
			}
		}

		/// <summary>
		/// Lấy thời khóa biểu theo giáo viên cho học kỳ cụ thể.
		/// </summary>
		public List<TimeTableSlotDTO> GetTKBByTeacher(int maHocKy, string maGiaoVien)
		{
			const string sql = @"
				SELECT 
					tkb.MaThoiKhoaBieu, 
					CAST(SUBSTRING_INDEX(tkb.ThuTrongTuan, ' ', -1) AS SIGNED) AS Thu, 
					tkb.TietBatDau AS Tiet,
					pc.MaPhanCong, 
					l.TenLop, 
					l.MaLop, 
					mh.TenMonHoc AS TenMon, 
					gv.HoTen AS TenGiaoVien, 
					gv.MaGiaoVien
				FROM ThoiKhoaBieu tkb
				JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
				JOIN LopHoc l ON pc.MaLop = l.MaLop
				JOIN MonHoc mh ON pc.MaMonHoc = mh.MaMonHoc
				JOIN GiaoVien gv ON pc.MaGiaoVien = gv.MaGiaoVien
				WHERE pc.MaHocKy = @MaHocKy AND gv.MaGiaoVien = @MaGiaoVien
				ORDER BY tkb.ThuTrongTuan, tkb.TietBatDau";

			var result = new List<TimeTableSlotDTO>();
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
					cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							var slot = new TimeTableSlotDTO
							{
								MaThoiKhoaBieu = reader.GetInt32("MaThoiKhoaBieu"),
								MaPhanCong = reader.GetInt32("MaPhanCong"),
								Thu = reader.GetInt32("Thu"),
								Tiet = reader.GetInt32("Tiet"),
								TenLop = reader.IsDBNull(reader.GetOrdinal("TenLop")) ? string.Empty : reader.GetString("TenLop"),
								MaLop = reader.GetInt32("MaLop"),
								TenMon = reader.IsDBNull(reader.GetOrdinal("TenMon")) ? string.Empty : reader.GetString("TenMon"),
								TenGiaoVien = reader.IsDBNull(reader.GetOrdinal("TenGiaoVien")) ? string.Empty : reader.GetString("TenGiaoVien"),
								MaGiaoVien = reader.IsDBNull(reader.GetOrdinal("MaGiaoVien")) ? string.Empty : reader.GetString("MaGiaoVien")
							};
							result.Add(slot);
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Xóa temp schedule cho học kỳ và tuần cụ thể.
		/// </summary>
		public void ClearTempForSemester(int semesterId, int weekNo)
		{
			const string sql = "DELETE FROM TKB_Temp WHERE SemesterId=@SemesterId AND WeekNo=@WeekNo";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", semesterId);
					cmd.Parameters.AddWithValue("@WeekNo", weekNo);
					cmd.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Kiểm tra xem học kỳ có temp schedule chưa.
		/// </summary>
		public bool HasTempScheduleForSemester(int semesterId)
		{
			const string sql = "SELECT COUNT(*) FROM TKB_Temp WHERE SemesterId=@SemesterId";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@SemesterId", semesterId);
					long count = (long)cmd.ExecuteScalar();
					return count > 0;
				}
			}
		}

		/// <summary>
		/// Thêm một bản ghi thời khóa biểu mới
		/// </summary>
		/// <param name="maPhanCong">Mã phân công</param>
		/// <param name="thu">Thứ trong tuần (2-6)</param>
		/// <param name="tiet">Tiết bắt đầu (1-10)</param>
		/// <param name="soTiet">Số tiết (mặc định 1)</param>
		/// <param name="phongHoc">Phòng học (có thể null)</param>
		/// <returns>Mã thời khóa biểu vừa tạo, hoặc 0 nếu thất bại</returns>
		public int InsertTKB(int maPhanCong, int thu, int tiet, int soTiet = 1, string phongHoc = null)
		{
			// Chuyển đổi thu (int) sang format string cho database
			string thuTrongTuan = $"Thu {thu}";

			const string sql = @"
				INSERT INTO ThoiKhoaBieu(MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc)
				VALUES(@MaPhanCong, @ThuTrongTuan, @TietBatDau, @SoTiet, @PhongHoc)";

			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MaPhanCong", maPhanCong);
					cmd.Parameters.AddWithValue("@ThuTrongTuan", thuTrongTuan);
					cmd.Parameters.AddWithValue("@TietBatDau", tiet);
					cmd.Parameters.AddWithValue("@SoTiet", soTiet);
					cmd.Parameters.AddWithValue("@PhongHoc", string.IsNullOrWhiteSpace(phongHoc) ? (object)DBNull.Value : phongHoc);
					
					cmd.ExecuteNonQuery();
					
					// Lấy ID vừa insert
					using (var cmdId = new MySqlCommand("SELECT LAST_INSERT_ID()", conn))
					{
						object result = cmdId.ExecuteScalar();
						return result != null ? Convert.ToInt32(result) : 0;
					}
				}
			}
		}

		/// <summary>
		/// Cập nhật một bản ghi thời khóa biểu
		/// </summary>
		/// <param name="maThoiKhoaBieu">Mã thời khóa biểu cần cập nhật</param>
		/// <param name="thu">Thứ trong tuần mới (2-6)</param>
		/// <param name="tiet">Tiết bắt đầu mới (1-10)</param>
		/// <param name="soTiet">Số tiết (mặc định 1)</param>
		/// <param name="phongHoc">Phòng học (có thể null)</param>
		/// <returns>True nếu cập nhật thành công</returns>
		public bool UpdateTKB(int maThoiKhoaBieu, int thu, int tiet, int soTiet = 1, string phongHoc = null)
		{
			// Chuyển đổi thu (int) sang format string cho database
			string thuTrongTuan = $"Thu {thu}";

			const string sql = @"
				UPDATE ThoiKhoaBieu
				SET ThuTrongTuan = @ThuTrongTuan,
					TietBatDau = @TietBatDau,
					SoTiet = @SoTiet,
					PhongHoc = @PhongHoc
				WHERE MaThoiKhoaBieu = @MaThoiKhoaBieu";

			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MaThoiKhoaBieu", maThoiKhoaBieu);
					cmd.Parameters.AddWithValue("@ThuTrongTuan", thuTrongTuan);
					cmd.Parameters.AddWithValue("@TietBatDau", tiet);
					cmd.Parameters.AddWithValue("@SoTiet", soTiet);
					cmd.Parameters.AddWithValue("@PhongHoc", string.IsNullOrWhiteSpace(phongHoc) ? (object)DBNull.Value : phongHoc);
					
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
	}
}


