using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.Services
{
	public class AssignmentPolicy
	{
		/// <summary>
		/// Số tiết tối đa mỗi giáo viên được phân công trong MỘT HỌC KỲ
		/// (Không phải tuần!)
		/// </summary>
		public int MaxLoadPerTeacherPerWeek { get; set; } = 100; // ✅ Default 100 tiết/học kỳ
		public bool AllowNonPrimarySpecialty { get; set; } = false;
		public int SpecialtyWeight { get; set; } = 5;
		public int PriorityWeight { get; set; } = 2;
		public int LoadBalanceWeight { get; set; } = 3;
	}

	public class PhanCongCandidate
	{
		public int MaLop { get; set; }
		public int MaMonHoc { get; set; }
		public string MaGiaoVien { get; set; } = string.Empty;
		public int SoTietTuan { get; set; }
		public int Score { get; set; }
		public string Note { get; set; } = string.Empty;
		// Display fields for UI binding
		public string TenLop { get; set; } = string.Empty;
		public string TenMon { get; set; } = string.Empty;
		public string TenGiaoVien { get; set; } = string.Empty;
	}

	public class ValidationReport
	{
		public int HardViolations { get; set; }
		public List<string> Messages { get; set; } = new List<string>();
	}

	public class AutoAssignResult
	{
		public List<PhanCongCandidate> Candidates { get; set; } = new List<PhanCongCandidate>();
		public ValidationReport Report { get; set; } = new ValidationReport();
		public bool IsReadOnly { get; set; } = false;
		public string SemesterStatus { get; set; } = string.Empty;
	}

	public class AssignmentAutoService
	{
		/// <summary>
		/// Sinh đề xuất phân công có kiểm tra trạng thái học kỳ
		/// </summary>
		public AutoAssignResult GenerateAutoAssignments(int hocKyId, AssignmentPolicy policy)
		{
			var result = new AutoAssignResult();
			
			// ✅ KIỂM TRA HỌC KỲ CÓ THỂ CHỈNH SỬA KHÔNG
			if (SemesterHelper.IsPast(hocKyId))
			{
				result.IsReadOnly = true;
				result.SemesterStatus = SemesterHelper.GetStatus(hocKyId);
				result.Report.HardViolations++;
				result.Report.Messages.Add($"⚠ Học kỳ này đã kết thúc ({result.SemesterStatus}). Không thể tạo phân công mới!");
				return result;
			}
			
			result.SemesterStatus = SemesterHelper.GetStatus(hocKyId);
			
			// Logic cũ tiếp tục...
			var lopDao = new LopDAO();
			var monDao = new MonHocDAO();
			var pcDao = new PhanCongGiangDayDAO();

			var classes = lopDao.DocDSLop();
			var subjects = monDao.DocDSMH();	
			var current = pcDao.LayPhanCongTheoHocKy(hocKyId);

			var teacherToLoad = GetTeacherWeeklyLoad(hocKyId);
			var subjectToTeachers = GetSubjectSpecialists();

			foreach (var lop in classes)
			{
				string gvcn = GetGVCN(lop.maLop);

				foreach (var mon in subjects)
				{
					int required = mon.soTiet;
					if (required <= 0) continue;

					bool already = current.Any(x => x.MaLop == lop.maLop && x.MaMonHoc == mon.maMon && x.MaHocKy == hocKyId);
					if (already) continue;

					var candidates = subjectToTeachers.ContainsKey(mon.maMon)
						? subjectToTeachers[mon.maMon]
						: new List<string>();

					// B1: Ưu tiên GVCN
					if (!string.IsNullOrEmpty(gvcn) && candidates.Contains(gvcn))
					{
						int loadGVCN = teacherToLoad.ContainsKey(gvcn) ? teacherToLoad[gvcn] : 0;
						result.Candidates.Add(new PhanCongCandidate
						{
							MaLop = lop.maLop,
							MaMonHoc = mon.maMon,
							MaGiaoVien = gvcn,
							SoTietTuan = required,
							Score = policy.SpecialtyWeight + policy.PriorityWeight * 10,
							Note = "GVCN"
						});
						teacherToLoad[gvcn] = loadGVCN + required;
						continue;
					}

					// B2: Chọn GV khác
					var scored = new List<(string gv, int score)>();
					foreach (var gv in candidates)
					{
						int load = teacherToLoad.ContainsKey(gv) ? teacherToLoad[gv] : 0;
						int soTietMon = mon.soTiet;
						
						// ✅ HARD CHECK: Không cho vượt ngưỡng
						if (load + soTietMon > policy.MaxLoadPerTeacherPerWeek)
						{
							Console.WriteLine($"⚠️ Skip GV {gv}: Load hiện tại {load} + {soTietMon} = {load + soTietMon} > {policy.MaxLoadPerTeacherPerWeek}");
							continue; // Skip giáo viên này
						}
						
						int loadDelta = policy.MaxLoadPerTeacherPerWeek - load;
						int score = policy.SpecialtyWeight + (policy.LoadBalanceWeight * Math.Max(0, loadDelta));
						if (loadDelta < 0) score += loadDelta;

						bool sameClassOfficial = current.Any(x => x.MaLop == lop.maLop && x.MaGiaoVien == gv);
						bool sameClassProposed = result.Candidates.Any(x => x.MaLop == lop.maLop && x.MaGiaoVien == gv);
						if (sameClassOfficial || sameClassProposed) score += policy.PriorityWeight * 3;
						scored.Add((gv, score));
					}

					if (scored.Count == 0 && policy.AllowNonPrimarySpecialty)
					{
						foreach (var kv in teacherToLoad)
						{
							int load = kv.Value;
							scored.Add((kv.Key, policy.LoadBalanceWeight * Math.Max(0, policy.MaxLoadPerTeacherPerWeek - load)));
						}
					}

					if (scored.Count == 0)
					{
						result.Report.HardViolations++;
						result.Report.Messages.Add($"Không tìm được GV phù hợp cho Lớp {lop.maLop}, Môn {mon.maMon}.");
						continue;
					}

					var best = scored.OrderByDescending(x => x.score).First();
					result.Candidates.Add(new PhanCongCandidate
					{
						MaLop = lop.maLop,
						MaMonHoc = mon.maMon,
						MaGiaoVien = best.gv,
						SoTietTuan = required,
						Score = best.score
					});
					teacherToLoad[best.gv] = (teacherToLoad.ContainsKey(best.gv) ? teacherToLoad[best.gv] : 0) + required;
				}
			}

			return result;
		}

		/// <summary>
		/// Sinh đề xuất phân công có lọc theo khối và môn (tùy chọn) + kiểm tra học kỳ
		/// </summary>
		public AutoAssignResult GenerateAutoAssignmentsFiltered(int hocKyId, AssignmentPolicy policy, int? khoi, string maMonFilter)
		{
			var result = new AutoAssignResult();
			
			// ✅ KIỂM TRA HỌC KỲ
			if (SemesterHelper.IsPast(hocKyId))
			{
				result.IsReadOnly = true;
				result.SemesterStatus = SemesterHelper.GetStatus(hocKyId);
				result.Report.HardViolations++;
				result.Report.Messages.Add($"⚠ Học kỳ này đã kết thúc ({result.SemesterStatus}). Không thể tạo phân công mới!");
				return result;
			}
			
			result.SemesterStatus = SemesterHelper.GetStatus(hocKyId);
			
			// Logic cũ với filter...
			var lopDao = new LopDAO();
			var monDao = new MonHocDAO();
			var pcDao = new PhanCongGiangDayDAO();

			var classes = lopDao.DocDSLop() ?? new List<LopDTO>();
			if (khoi.HasValue)
			{
				classes = classes.Where(l => l.maKhoi == khoi.Value).ToList();
			}

			var subjects = monDao.DocDSMH() ?? new List<MonHocDTO>();
			int? monId = null;
			if (!string.IsNullOrWhiteSpace(maMonFilter) && int.TryParse(maMonFilter, out int parsed))
			{
				monId = parsed;
			}
			if (monId.HasValue)
			{
				subjects = subjects.Where(m => m.maMon == monId.Value).ToList();
			}

			var current = pcDao.LayPhanCongTheoHocKy(hocKyId) ?? new List<PhanCongGiangDayDTO>();
			var teacherToLoad = GetTeacherWeeklyLoad(hocKyId);
			var subjectToTeachers = GetSubjectSpecialists();

			foreach (var lop in classes)
			{
				string gvcn = GetGVCN(lop.maLop);

				foreach (var mon in subjects)
				{
					int required = mon.soTiet;
					if (required <= 0) continue;

					bool already = current.Any(x => x.MaLop == lop.maLop && x.MaMonHoc == mon.maMon && x.MaHocKy == hocKyId);
					if (already) continue;

					var candidates = subjectToTeachers.ContainsKey(mon.maMon)
						? subjectToTeachers[mon.maMon]
						: new List<string>();

					// Ưu tiên GVCN
					if (!string.IsNullOrEmpty(gvcn) && candidates.Contains(gvcn))
					{
						int loadGVCN = teacherToLoad.ContainsKey(gvcn) ? teacherToLoad[gvcn] : 0;
						result.Candidates.Add(new PhanCongCandidate
						{
							MaLop = lop.maLop,
							MaMonHoc = mon.maMon,
							MaGiaoVien = gvcn,
							SoTietTuan = required,
							Score = policy.SpecialtyWeight + policy.PriorityWeight * 10,
							Note = "GVCN"
						});
						teacherToLoad[gvcn] = loadGVCN + required;
						continue;
					}

					var scored = new List<(string gv, int score)>();
					foreach (var gv in candidates)
					{
						int load = teacherToLoad.ContainsKey(gv) ? teacherToLoad[gv] : 0;
						int soTietMon = mon.soTiet;
						
						// ✅ HARD CHECK: Không cho vượt ngưỡng
						if (load + soTietMon > policy.MaxLoadPerTeacherPerWeek)
						{
							Console.WriteLine($"⚠️ Skip GV {gv}: Load hiện tại {load} + {soTietMon} = {load + soTietMon} > {policy.MaxLoadPerTeacherPerWeek}");
							continue; // Skip giáo viên này
						}
						
						int loadDelta = policy.MaxLoadPerTeacherPerWeek - load;
						int score = policy.SpecialtyWeight + (policy.LoadBalanceWeight * Math.Max(0, loadDelta));
						if (loadDelta < 0) score += loadDelta;

						bool sameClassOfficial = current.Any(x => x.MaLop == lop.maLop && x.MaGiaoVien == gv);
						bool sameClassProposed = result.Candidates.Any(x => x.MaLop == lop.maLop && x.MaGiaoVien == gv);
						if (sameClassOfficial || sameClassProposed) score += policy.PriorityWeight * 3;
						scored.Add((gv, score));
					}

					if (scored.Count == 0 && policy.AllowNonPrimarySpecialty)
					{
						foreach (var kv in teacherToLoad)
						{
							int load = kv.Value;
							int soTietMon = mon.soTiet;
							
							// ✅ HARD CHECK: Không cho vượt ngưỡng ngay cả khi AllowNonPrimarySpecialty
							if (load + soTietMon > policy.MaxLoadPerTeacherPerWeek)
								continue;
								
							scored.Add((kv.Key, policy.LoadBalanceWeight * Math.Max(0, policy.MaxLoadPerTeacherPerWeek - load)));
						}
					}

					if (scored.Count == 0)
					{
						result.Report.HardViolations++;
						result.Report.Messages.Add($"Không tìm được GV phù hợp cho Lớp {lop.maLop}, Môn {mon.maMon}.");
						continue;
					}

					var best = scored.OrderByDescending(x => x.score).First();
					result.Candidates.Add(new PhanCongCandidate
					{
						MaLop = lop.maLop,
						MaMonHoc = mon.maMon,
						MaGiaoVien = best.gv,
						SoTietTuan = required,
						Score = best.score
					});
					teacherToLoad[best.gv] = (teacherToLoad.ContainsKey(best.gv) ? teacherToLoad[best.gv] : 0) + required;
				}
			}

			return result;
		}

		public ValidationReport ValidateAutoAssignments(List<PhanCongCandidate> list, int maxLoadPerSemester = 100)
		{
			var report = new ValidationReport();
			var seen = new HashSet<string>();
			var teacherLoad = new Dictionary<string, int>();
			
			foreach (var c in list)
			{
				// Check duplicate
				string key = $"{c.MaLop}|{c.MaMonHoc}|{c.MaGiaoVien}";
				if (!seen.Add(key))
				{
					report.HardViolations++;
					report.Messages.Add($"❌ Duplicate đề xuất: Lớp {c.TenLop}, Môn {c.TenMon}, GV {c.TenGiaoVien}");
				}
				
				// ✅ Check teacher load (số tiết/học kỳ)
				if (!teacherLoad.ContainsKey(c.MaGiaoVien))
					teacherLoad[c.MaGiaoVien] = 0;
				
				teacherLoad[c.MaGiaoVien] += c.SoTietTuan; // Tên biến là SoTietTuan nhưng thực tế là SoTiet/HocKy
			}
			
			// ✅ Validate teacher load không vượt ngưỡng
			foreach (var kv in teacherLoad)
			{
				if (kv.Value > maxLoadPerSemester)
				{
					report.HardViolations++;
					// Tìm tên GV
					var gvName = list.FirstOrDefault(c => c.MaGiaoVien == kv.Key)?.TenGiaoVien ?? kv.Key;
					report.Messages.Add($"⚠️ GV {gvName} vượt ngưỡng: {kv.Value}/{maxLoadPerSemester} tiết/học kỳ");
				}
			}
			
			return report;
		}

		private Dictionary<string, int> GetTeacherWeeklyLoad(int hocKyId)
		{
			const string sql = @"SELECT MaGiaoVien, SUM(m.SoTiet) AS LoadTiet
				FROM PhanCongGiangDay pc JOIN MonHoc m ON pc.MaMonHoc = m.MaMonHoc
				WHERE pc.MaHocKy = @MaHocKy
				GROUP BY MaGiaoVien";
			var result = new Dictionary<string, int>();
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MaHocKy", hocKyId);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							string gv = reader.GetString("MaGiaoVien");
							int load = reader.IsDBNull(reader.GetOrdinal("LoadTiet")) ? 0 : reader.GetInt32("LoadTiet");
							result[gv] = load;
						}
					}
				}
			}
			return result;
		}

		private Dictionary<int, List<string>> GetSubjectSpecialists()
		{
			const string sql = @"
				SELECT MaMonHoc, MaGiaoVien FROM GiaoVienChuyenMon
				UNION
				SELECT MaMonHoc, MaGiaoVien FROM GiaoVien_MonHoc";
			var result = new Dictionary<int, List<string>>();
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							int mon = reader.GetInt32("MaMonHoc");
							string gv = reader.GetString("MaGiaoVien");
							if (!result.ContainsKey(mon)) result[mon] = new List<string>();
							if (!result[mon].Contains(gv)) result[mon].Add(gv);
						}
					}
				}
			}
			return result;
		}

		private string GetGVCN(int maLop)
		{
			const string sql = "SELECT MaGiaoVienChuNhiem FROM LopHoc WHERE MaLop = @MaLop";
			using (var conn = ConnectionDatabase.GetConnection())
			{
				conn.Open();
				using (var cmd = new MySqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MaLop", maLop);
					object result = cmd.ExecuteScalar();
					return result != null ? result.ToString() : null;
				}
			}
		}
	}
}


