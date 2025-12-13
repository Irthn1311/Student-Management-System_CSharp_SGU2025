using System;
using System.Collections.Generic;
using System.Linq;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;

namespace Student_Management_System_CSharp_SGU2025.BUS.Services
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
			
			// ✅ Không giới hạn tải, chỉ đảm bảo policy không null
			if (policy == null)
			{
				policy = new AssignmentPolicy();
			}
			// Đảm bảo chỉ cho phép GV đúng chuyên môn
			policy.AllowNonPrimarySpecialty = false;
			policy.MaxLoadPerTeacherPerWeek = int.MaxValue; // Không giới hạn
			
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
			var lopBus = new LopHocBUS();
			var monBus = new MonHocBUS();
			var pcBus = new PhanCongGiangDayBUS();

			var classes = lopBus.DocDSLop();
			var subjects = monBus.DocDSMH();	
			var current = pcBus.LayPhanCongTheoHocKy(hocKyId);

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

					// ✅ Debug: Log if no candidates found
					if (candidates.Count == 0)
					{
						Console.WriteLine($"⚠️ Môn {mon.maMon} ({mon.tenMon}) không có GV chuyên môn. AllowNonPrimary={policy.AllowNonPrimarySpecialty}");
					}

					// B1: Ưu tiên GVCN (không kiểm tra giới hạn tải)
					if (!string.IsNullOrEmpty(gvcn) && candidates.Contains(gvcn))
					{
						result.Candidates.Add(new PhanCongCandidate
						{
							MaLop = lop.maLop,
							MaMonHoc = mon.maMon,
							MaGiaoVien = gvcn,
							SoTietTuan = required,
							Score = policy.SpecialtyWeight + policy.PriorityWeight * 10,
							Note = "GVCN"
						});
						// Cập nhật tải để cân bằng (không giới hạn)
						if (!teacherToLoad.ContainsKey(gvcn)) teacherToLoad[gvcn] = 0;
						teacherToLoad[gvcn] += required;
						continue;
					}

					// B2: Chọn GV khác (chỉ chọn GV có chuyên môn đúng)
					var scored = new List<(string gv, int score)>();
					foreach (var gv in candidates)
					{
						int load = teacherToLoad.ContainsKey(gv) ? teacherToLoad[gv] : 0;
						
						// ✅ Không kiểm tra giới hạn tải, chỉ ưu tiên GV có tải thấp hơn
						int score = policy.SpecialtyWeight + (policy.LoadBalanceWeight * Math.Max(0, 100 - load));

						bool sameClassOfficial = current.Any(x => x.MaLop == lop.maLop && x.MaGiaoVien == gv);
						bool sameClassProposed = result.Candidates.Any(x => x.MaLop == lop.maLop && x.MaGiaoVien == gv);
						if (sameClassOfficial || sameClassProposed) score += policy.PriorityWeight * 3;
						scored.Add((gv, score));
					}

					// ✅ Chỉ cho phép GV đúng chuyên môn, không tìm GV ngoài chuyên môn
					if (scored.Count == 0)
					{
						Console.WriteLine($"❌ Không tìm được GV chuyên môn cho Lớp {lop.maLop}, Môn {mon.maMon} ({mon.tenMon})");
						result.Report.HardViolations++;
						result.Report.Messages.Add($"Không tìm được GV có chuyên môn phù hợp cho Lớp {lop.maLop}, Môn {mon.maMon} ({mon.tenMon}).");
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
					// Cập nhật tải để cân bằng (không giới hạn)
					if (!teacherToLoad.ContainsKey(best.gv)) teacherToLoad[best.gv] = 0;
					teacherToLoad[best.gv] += required;
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
			
			// ✅ Không giới hạn tải, chỉ đảm bảo policy không null
			if (policy == null)
			{
				policy = new AssignmentPolicy();
			}
			// Đảm bảo chỉ cho phép GV đúng chuyên môn
			policy.AllowNonPrimarySpecialty = false;
			policy.MaxLoadPerTeacherPerWeek = int.MaxValue; // Không giới hạn
			
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
			
			// ✅ Logic với filter theo MonHoc_NamHoc_Khoi
			var lopBus = new LopHocBUS();
			var monBus = new MonHocBUS();
			var pcBus = new PhanCongGiangDayBUS();
			var hocKyDAO = new HocKyDAO();
			var monHocNamHocKhoiBUS = new MonHoc_NamHoc_KhoiBUS();

			// Lấy năm học từ học kỳ
			var hocKy = hocKyDAO.LayHocKyTheoMa(hocKyId);
			string maNamHoc = hocKy?.MaNamHoc;

			var classes = lopBus.DocDSLop() ?? new List<LopDTO>();
			if (khoi.HasValue)
			{
				classes = classes.Where(l => l.maKhoi == khoi.Value).ToList();
			}

			// ✅ Load môn học từ MonHoc_NamHoc_Khoi thay vì tất cả môn học
			List<MonHocDTO> subjects = new List<MonHocDTO>();
			
			if (!string.IsNullOrEmpty(maNamHoc))
			{
				// Nếu có filter khối, lấy môn học theo năm học và khối
				if (khoi.HasValue)
				{
					subjects = monHocNamHocKhoiBUS.LayDanhSachMonHocTheoNamHocKhoi(maNamHoc, khoi.Value);
				}
				else
				{
					// Nếu không có filter khối, lấy tất cả môn học trong năm học (tất cả khối)
					subjects = monHocNamHocKhoiBUS.LayDanhSachMonHocTheoNamHoc(maNamHoc);
				}
			}
			else
			{
				// Fallback: Nếu không có năm học, load tất cả môn học
				subjects = monBus.DocDSMH() ?? new List<MonHocDTO>();
			}

			// Filter theo môn học cụ thể (nếu có)
			int? monId = null;
			if (!string.IsNullOrWhiteSpace(maMonFilter) && int.TryParse(maMonFilter, out int parsed))
			{
				monId = parsed;
			}
			if (monId.HasValue)
			{
				subjects = subjects.Where(m => m.maMon == monId.Value).ToList();
			}

			var current = pcBus.LayPhanCongTheoHocKy(hocKyId) ?? new List<PhanCongGiangDayDTO>();
			var teacherToLoad = GetTeacherWeeklyLoad(hocKyId);
			var subjectToTeachers = GetSubjectSpecialists();

			foreach (var lop in classes)
			{
				string gvcn = GetGVCN(lop.maLop);

				// ✅ Lọc môn học hợp lệ cho lớp này (dựa trên khối của lớp và năm học)
				List<MonHocDTO> validSubjectsForClass = subjects;
				if (!string.IsNullOrEmpty(maNamHoc))
				{
					// Chỉ lấy môn học hợp lệ cho khối của lớp này trong năm học
					validSubjectsForClass = monHocNamHocKhoiBUS.LayDanhSachMonHocTheoNamHocKhoi(maNamHoc, lop.maKhoi);
					
					// Nếu có filter môn học cụ thể, áp dụng filter
					if (monId.HasValue)
					{
						validSubjectsForClass = validSubjectsForClass.Where(m => m.maMon == monId.Value).ToList();
					}
				}

				foreach (var mon in validSubjectsForClass)
				{
					int required = mon.soTiet;
					if (required <= 0) continue;

					bool already = current.Any(x => x.MaLop == lop.maLop && x.MaMonHoc == mon.maMon && x.MaHocKy == hocKyId);
					if (already) continue;

					var candidates = subjectToTeachers.ContainsKey(mon.maMon)
						? subjectToTeachers[mon.maMon]
						: new List<string>();

					// ✅ Debug: Log if no candidates found
					if (candidates.Count == 0)
					{
						Console.WriteLine($"⚠️ Môn {mon.maMon} ({mon.tenMon}) không có GV chuyên môn. AllowNonPrimary={policy.AllowNonPrimarySpecialty}");
					}

					// Ưu tiên GVCN (không kiểm tra giới hạn tải)
					if (!string.IsNullOrEmpty(gvcn) && candidates.Contains(gvcn))
					{
						result.Candidates.Add(new PhanCongCandidate
						{
							MaLop = lop.maLop,
							MaMonHoc = mon.maMon,
							MaGiaoVien = gvcn,
							SoTietTuan = required,
							Score = policy.SpecialtyWeight + policy.PriorityWeight * 10,
							Note = "GVCN"
						});
						// Cập nhật tải để cân bằng (không giới hạn)
						if (!teacherToLoad.ContainsKey(gvcn)) teacherToLoad[gvcn] = 0;
						teacherToLoad[gvcn] += required;
						continue;
					}

					var scored = new List<(string gv, int score)>();
					foreach (var gv in candidates)
					{
						int load = teacherToLoad.ContainsKey(gv) ? teacherToLoad[gv] : 0;
						
						// ✅ Không kiểm tra giới hạn tải, chỉ ưu tiên GV có tải thấp hơn
						int score = policy.SpecialtyWeight + (policy.LoadBalanceWeight * Math.Max(0, 100 - load));

						bool sameClassOfficial = current.Any(x => x.MaLop == lop.maLop && x.MaGiaoVien == gv);
						bool sameClassProposed = result.Candidates.Any(x => x.MaLop == lop.maLop && x.MaGiaoVien == gv);
						if (sameClassOfficial || sameClassProposed) score += policy.PriorityWeight * 3;
						scored.Add((gv, score));
					}

					// ✅ Chỉ cho phép GV đúng chuyên môn, không tìm GV ngoài chuyên môn
					if (scored.Count == 0)
					{
						Console.WriteLine($"❌ [Filtered] Không tìm được GV chuyên môn cho Lớp {lop.maLop}, Môn {mon.maMon} ({mon.tenMon})");
						result.Report.HardViolations++;
						result.Report.Messages.Add($"Không tìm được GV có chuyên môn phù hợp cho Lớp {lop.maLop}, Môn {mon.maMon} ({mon.tenMon}).");
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
					// Cập nhật tải để cân bằng (không giới hạn)
					if (!teacherToLoad.ContainsKey(best.gv)) teacherToLoad[best.gv] = 0;
					teacherToLoad[best.gv] += required;
				}
			}

			return result;
		}

		public ValidationReport ValidateAutoAssignments(List<PhanCongCandidate> list)
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
			
			// ✅ Không kiểm tra giới hạn tải (đã bỏ giới hạn)
			// Chỉ log thông tin tải để tham khảo
			foreach (var kv in teacherLoad)
			{
				var gvName = list.FirstOrDefault(c => c.MaGiaoVien == kv.Key)?.TenGiaoVien ?? kv.Key;
				Console.WriteLine($"📊 GV {gvName}: {kv.Value} tiết/học kỳ");
			}
			
			return report;
		}

		private Dictionary<string, int> GetTeacherWeeklyLoad(int hocKyId)
		{
			var result = new Dictionary<string, int>();
			var pcBus = new PhanCongGiangDayBUS();
			var monBus = new MonHocBUS();
			
			try
			{
				// Get current assignments for the semester
				var assignments = pcBus.LayPhanCongTheoHocKy(hocKyId);
				var subjects = monBus.DocDSMH();
				
				// Calculate load per teacher
				foreach (var assignment in assignments)
				{
					var subject = subjects.FirstOrDefault(s => s.maMon == assignment.MaMonHoc);
					if (subject != null)
					{
						if (!result.ContainsKey(assignment.MaGiaoVien))
							result[assignment.MaGiaoVien] = 0;
						result[assignment.MaGiaoVien] += subject.soTiet;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❌ Lỗi GetTeacherWeeklyLoad: {ex.Message}");
			}
			
			return result;
		}

		private Dictionary<int, List<string>> GetSubjectSpecialists()
		{
			// ✅ Updated: Get teachers and their specialties through BUS layer
			var result = new Dictionary<int, List<string>>();
			var gvBus = new GiaoVienBUS();
			
			try
			{
				// Get all teachers with their specialties
				var teachers = gvBus.DocDSGiaoVien();
				
				foreach (var teacher in teachers)
				{
					// Check if teacher is active and has specialty
					if (teacher.TrangThai == "Đang giảng dạy" && teacher.MaMonChuyenMon.HasValue)
					{
						int mon = teacher.MaMonChuyenMon.Value;
						string gv = teacher.MaGiaoVien;
						
						if (!result.ContainsKey(mon)) 
							result[mon] = new List<string>();
							
						if (!result[mon].Contains(gv)) 
							result[mon].Add(gv);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❌ Lỗi GetSubjectSpecialists: {ex.Message}");
			}
			
			return result;
		}

		private string GetGVCN(int maLop)
		{
			try
			{
				var lopBus = new LopHocBUS();
				var lop = lopBus.LayLopTheoId(maLop);
				return lop?.maGVCN;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❌ Lỗi GetGVCN: {ex.Message}");
				return null;
			}
		}
	}
}