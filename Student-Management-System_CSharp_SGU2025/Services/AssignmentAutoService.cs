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
		public int MaxLoadPerTeacherPerWeek { get; set; } = 30;
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
	}

	public class AssignmentAutoService
	{
		public AutoAssignResult GenerateAutoAssignments(int hocKyId, AssignmentPolicy policy)
		{
			var lopDao = new LopDAO();
			var monDao = new MonHocDAO();
			var pcDao = new PhanCongGiangDayDAO();

			var classes = lopDao.DocDSLop(); // fallback to all if no ByHocKy method
			var subjects = monDao.DocDSMH();
			var current = pcDao.LayPhanCongTheoHocKy(hocKyId);

			var teacherToLoad = GetTeacherWeeklyLoad(hocKyId);
			var subjectToTeachers = GetSubjectSpecialists();

			var result = new AutoAssignResult();

			foreach (var lop in classes)
			{
				// Ưu tiên GVCN dạy lớp mình nếu phù hợp (theo spec 3.2)
				string gvcn = GetGVCN(lop.maLop);

				foreach (var mon in subjects)
				{
					int required = mon.soTiet; // default per week
					if (required <= 0) continue;

					// skip if already assigned in official list
					bool already = current.Any(x => x.MaLop == lop.maLop && x.MaMonHoc == mon.maMon && x.MaHocKy == hocKyId);
					if (already) continue;

					var candidates = subjectToTeachers.ContainsKey(mon.maMon)
						? subjectToTeachers[mon.maMon]
						: new List<string>();

					// B1: Ưu tiên GVCN nếu có và GVCN có thể dạy môn
					if (!string.IsNullOrEmpty(gvcn) && candidates.Contains(gvcn))
					{
						int loadGVCN = teacherToLoad.ContainsKey(gvcn) ? teacherToLoad[gvcn] : 0;
						// Không kiểm tra hard limit cho GVCN (theo yêu cầu: không giới hạn số tiết/tuần)
						result.Candidates.Add(new PhanCongCandidate
						{
							MaLop = lop.maLop,
							MaMonHoc = mon.maMon,
							MaGiaoVien = gvcn,
							SoTietTuan = required,
							Score = policy.SpecialtyWeight + policy.PriorityWeight * 10, // Điểm cao cho GVCN
							Note = "GVCN"
						});
						teacherToLoad[gvcn] = loadGVCN + required;
						continue;
					}

					// B2: Chọn GV khác theo heuristic cân bằng
					var scored = new List<(string gv, int score)>();
					foreach (var gv in candidates)
					{
						int load = teacherToLoad.ContainsKey(gv) ? teacherToLoad[gv] : 0;
						// Soft warning nếu vượt load, nhưng vẫn cho phép (theo spec: không áp hạn mức)
						int score = policy.SpecialtyWeight + (policy.LoadBalanceWeight * Math.Max(0, policy.MaxLoadPerTeacherPerWeek - load));
						scored.Add((gv, score));
					}

					// fallback: allow any teacher if policy allows
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

		public ValidationReport ValidateAutoAssignments(List<PhanCongCandidate> list)
		{
			var report = new ValidationReport();
			var seen = new HashSet<string>();
			foreach (var c in list)
			{
				string key = $"{c.MaLop}|{c.MaMonHoc}|{c.MaGiaoVien}";
				if (!seen.Add(key))
				{
					report.HardViolations++;
					report.Messages.Add($"Duplicate đề xuất: {key}");
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
			// Ưu tiên GiaoVienChuyenMon, fallback sang GiaoVien_MonHoc nếu không có
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

		/// <summary>
		/// Lấy GVCN (Giáo viên chủ nhiệm) của lớp
		/// </summary>
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


