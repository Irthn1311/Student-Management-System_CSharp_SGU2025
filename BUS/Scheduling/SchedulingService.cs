using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Student_Management_System_CSharp_SGU2025.BUS.Scheduling;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.BUS.Config;
using Student_Management_System_CSharp_SGU2025.DTO;
using AssignmentSlot = Student_Management_System_CSharp_SGU2025.DTO.AssignmentSlotDTO;

namespace Student_Management_System_CSharp_SGU2025.BUS.Scheduling
{
	/// <summary>
	/// Tabu Search based auto-scheduling service. Provides generation, evaluation and persistence helpers.
	/// </summary>
	public class SchedulingService
	{
		private const int HardPenalty = 1_000_000;
		private Dictionary<int, int> _classToKhoiCache = new Dictionary<int, int>(); // Cache MaLop -> MaKhoi

		public ScheduleSolution GenerateSchedule(ScheduleRequest request, CancellationToken cancellationToken)
		{
			var start = DateTime.UtcNow;
			var best = InitializeGreedy(request);
			best.Cost = EvaluateCost(best, request.WeightConfig);
			var bestCost = best.Cost;
			var tabu = new Dictionary<string, int>();
			var rand = new Random(42);
			var iterSinceImprove = 0;

			var stopwatch = Stopwatch.StartNew();
			for (int iter = 0; iter < request.IterMax; iter++)
			{
				if (cancellationToken.IsCancellationRequested) break;
				if (stopwatch.Elapsed.TotalSeconds > request.TimeBudgetSec) break;

				var neighborhood = GenerateNeighborhood(best, request);
				ScheduleSolution candidate = null;
				int candidateCost = int.MaxValue;

				foreach (var neighbor in neighborhood)
				{
					if (!ValidateHardConstraints(neighbor))
						continue;

					var moveKey = ComputeMoveKey(neighbor);
					bool isTabu = tabu.ContainsKey(moveKey);
					var cost = EvaluateCost(neighbor, request.WeightConfig);
					bool aspiration = cost < bestCost;

					if (!isTabu || aspiration)
					{
						if (cost < candidateCost)
						{
							candidate = neighbor;
							candidateCost = cost;
						}
					}
				}

				if (candidate == null)
				{
					iterSinceImprove++;
					if (iterSinceImprove > request.NoImproveLimit) break;
					continue;
				}

				// Apply candidate
				best = candidate;
				best.Cost = candidateCost;
				var tabuKey = ComputeMoveKey(best);
				tabu[tabuKey] = iter + request.TabuTenure + rand.Next(0, 3);

				// Decrease tabu tenure
				var toRemove = new List<string>();
				foreach (var k in tabu.Keys)
				{
					if (tabu[k] <= iter) toRemove.Add(k);
				}
				foreach (var k in toRemove) tabu.Remove(k);

				if (best.Cost < bestCost)
				{
					bestCost = best.Cost;
					iterSinceImprove = 0;
				}
				else
				{
					iterSinceImprove++;
					if (iterSinceImprove > request.NoImproveLimit) break;
				}
			}

			// Final attempt: Try to add missing slots
			var beforeAdd = best.Slots.Count;
			best = TryAddMissingSlots(best, request);
			var afterAdd = best.Slots.Count;
			if (afterAdd > beforeAdd)
			{
				best.Cost = EvaluateCost(best, request.WeightConfig);
				System.Diagnostics.Debug.WriteLine($"TryAddMissingSlots: Added {afterAdd - beforeAdd} slots. Total: {afterAdd}");
			}
			
			// If still missing, try force placement (allow soft conflicts)
			if (afterAdd < request.Assignments.Sum(a => a.SoTietTuan))
			{
				var beforeForce = best.Slots.Count;
				best = TryForcePlaceMissingSlots(best, request);
				var afterForce = best.Slots.Count;
				if (afterForce > beforeForce)
				{
					best.Cost = EvaluateCost(best, request.WeightConfig);
					System.Diagnostics.Debug.WriteLine($"TryForcePlaceMissingSlots: Force-placed {afterForce - beforeForce} slots. Total: {afterForce}");
				}
			}

			// Final cleanup: Remove hard violations (duplicate slots for same class at same time)
			// This can happen if TryForcePlaceMissingSlots created conflicts
			best = RemoveHardViolations(best);
			best.Cost = EvaluateCost(best, request.WeightConfig);

			stopwatch.Stop();
			return best;
		}

		/// <summary>
		/// Build ScheduleRequest from database based on semester/week using existing DAO/BUS.
		/// </summary>
		public ScheduleRequest BuildRequestFromDatabase(int semesterId, int weekNo)
		{
			var phanCongBus = new PhanCongGiangDayBUS();
			var assignments = phanCongBus.GetBySemester(semesterId);

			var req = new ScheduleRequest
			{
				SemesterId = semesterId,
				WeekNo = weekNo
			};

			var classIds = new HashSet<int>();
			var teacherIds = new HashSet<string>();
			var subjectIds = new HashSet<int>();

			// Group by (Lop, Mon) to handle cases where same subject might have multiple teachers
			// In normal case, each (Lop, Mon) should have only one teacher, but we handle duplicates
			var assignmentGroups = assignments
				.GroupBy(pc => new { pc.MaLop, pc.MaMonHoc })
				.ToList();

			// Analyze assignments for potential issues
			var teacherWorkload = new Dictionary<string, int>(); // Teacher -> total periods per week
			var classWorkload = new Dictionary<int, int>(); // Class -> total periods per week
			var duplicateAssignments = new List<string>();

			foreach (var group in assignmentGroups)
			{
				var pc = group.First(); // Use first assignment in group
				classIds.Add(pc.MaLop);
				teacherIds.Add(pc.MaGiaoVien);
				subjectIds.Add(pc.MaMonHoc);
				
				// Get required periods for this specific week
				// Note: weekNo is passed to BuildRequestFromDatabase, but we need to get it from the request
				// For now, use the default (average) calculation, but we should pass weekNo if available
				var required = phanCongBus.GetRequiredPeriods(pc.MaLop, pc.MaMonHoc, semesterId, weekNo);
				
				// Track workload
				if (!teacherWorkload.ContainsKey(pc.MaGiaoVien))
					teacherWorkload[pc.MaGiaoVien] = 0;
				teacherWorkload[pc.MaGiaoVien] += required;
				
				if (!classWorkload.ContainsKey(pc.MaLop))
					classWorkload[pc.MaLop] = 0;
				classWorkload[pc.MaLop] += required;
				
				// If multiple teachers for same subject, we use the first one
				// (In practice, each class-subject should have only one teacher)
				if (group.Count() > 1)
				{
					duplicateAssignments.Add($"Lớp {pc.MaLop}, Môn {pc.MaMonHoc} có {group.Count()} giáo viên. Chỉ dùng GV {pc.MaGiaoVien}.");
					System.Diagnostics.Debug.WriteLine($"Warning: Lớp {pc.MaLop}, Môn {pc.MaMonHoc} có {group.Count()} giáo viên. Chỉ dùng GV {pc.MaGiaoVien}.");
				}
				
				req.Assignments.Add(new AssignmentRequirement
				{
					MaLop = pc.MaLop,
					MaGV = pc.MaGiaoVien,
					MaMon = pc.MaMonHoc,
					SoTietTuan = required
				});
			}

			// Check for overloaded teachers (more than 50 periods/week = 10 periods/day × 5 days)
			var overloadedTeachers = teacherWorkload.Where(kvp => kvp.Value > 50).ToList();
			if (overloadedTeachers.Any())
			{
				var msg = $"⚠️ PHÂN CÔNG CÓ VẤN ĐỀ: Có {overloadedTeachers.Count} giáo viên bị quá tải (vượt quá 50 tiết/tuần):";
				System.Diagnostics.Debug.WriteLine(msg);
				foreach (var kvp in overloadedTeachers.OrderByDescending(x => x.Value).Take(5))
				{
					var teacher = kvp.Key;
					var workload = kvp.Value;
					var detail = $"  - GV {teacher}: {workload} tiết/tuần";
					System.Diagnostics.Debug.WriteLine(detail);
				}
			}

			// Check for overloaded classes (more than 50 periods/week)
			var overloadedClasses = classWorkload.Where(kvp => kvp.Value > 50).ToList();
			if (overloadedClasses.Any())
			{
				var msg = $"⚠️ PHÂN CÔNG CÓ VẤN ĐỀ: Có {overloadedClasses.Count} lớp bị quá tải (vượt quá 50 tiết/tuần):";
				System.Diagnostics.Debug.WriteLine(msg);
				foreach (var kvp in overloadedClasses.OrderByDescending(x => x.Value).Take(5))
				{
					var classId = kvp.Key;
					var workload = kvp.Value;
					var detail = $"  - Lớp {classId}: {workload} tiết/tuần";
					System.Diagnostics.Debug.WriteLine(detail);
				}
			}

			// Log duplicate assignments
			if (duplicateAssignments.Any())
			{
				var msg = $"⚠️ PHÂN CÔNG CÓ VẤN ĐỀ: Có {duplicateAssignments.Count} phân công trùng lặp (cùng lớp-môn có nhiều giáo viên):";
				System.Diagnostics.Debug.WriteLine(msg);
				foreach (var detail in duplicateAssignments.Take(5))
				{
					System.Diagnostics.Debug.WriteLine($"  - {detail}");
				}
			}

			// Store analysis results for later reporting
			req.ClassIds = new BindingList<int>(classIds.ToList());
			req.TeacherIds = new BindingList<string>(teacherIds.ToList());
			req.SubjectIds = new BindingList<int>(subjectIds.ToList());
			
			// Report analysis results via Debug (will be visible in Output window)
			if (overloadedTeachers.Any() || overloadedClasses.Any() || duplicateAssignments.Any())
			{
				System.Diagnostics.Debug.WriteLine("═══════════════════════════════════════════════════════");
				System.Diagnostics.Debug.WriteLine("PHÂN TÍCH PHÂN CÔNG - PHÁT HIỆN VẤN ĐỀ:");
				if (overloadedTeachers.Any())
				{
					System.Diagnostics.Debug.WriteLine($"  ⚠️ {overloadedTeachers.Count} giáo viên quá tải:");
					foreach (var kvp in overloadedTeachers.OrderByDescending(x => x.Value))
					{
						var teacher = kvp.Key;
						var workload = kvp.Value;
						System.Diagnostics.Debug.WriteLine($"     GV {teacher}: {workload} tiết/tuần (tối đa 50)");
					}
				}
				if (overloadedClasses.Any())
				{
					System.Diagnostics.Debug.WriteLine($"  ⚠️ {overloadedClasses.Count} lớp quá tải:");
					foreach (var kvp in overloadedClasses.OrderByDescending(x => x.Value))
					{
						var classId = kvp.Key;
						var workload = kvp.Value;
						System.Diagnostics.Debug.WriteLine($"     Lớp {classId}: {workload} tiết/tuần (tối đa 50)");
					}
				}
				if (duplicateAssignments.Any())
				{
					System.Diagnostics.Debug.WriteLine($"  ⚠️ {duplicateAssignments.Count} phân công trùng lặp:");
					foreach (var detail in duplicateAssignments.Take(10))
					{
						System.Diagnostics.Debug.WriteLine($"     {detail}");
					}
				}
				System.Diagnostics.Debug.WriteLine("═══════════════════════════════════════════════════════");
			}
			
			return req;
		}

		public bool ValidateHardConstraints(ScheduleSolution sol)
		{
			// No teacher clashes, no class clashes
			var teacherAtTime = new HashSet<string>();
			var classAtTime = new HashSet<string>();

			foreach (var s in sol.Slots)
			{
				string keyTeacher = $"{s.MaGV}|{s.Thu}|{s.Tiet}";
				if (!teacherAtTime.Add(keyTeacher)) return false;

				string keyClass = $"{s.MaLop}|{s.Thu}|{s.Tiet}";
				if (!classAtTime.Add(keyClass)) return false;
			}
			return true;
		}

		public ConflictReport AnalyzeConflicts(ScheduleSolution sol)
		{
			var report = new ConflictReport();
			var teacherAtTime = new Dictionary<string, List<AssignmentSlot>>();
			var classAtTime = new Dictionary<string, List<AssignmentSlot>>();

			foreach (var s in sol.Slots)
			{
				string kt = $"{s.MaGV}|{s.Thu}|{s.Tiet}";
				if (!teacherAtTime.ContainsKey(kt))
					teacherAtTime[kt] = new List<AssignmentSlot>();
				teacherAtTime[kt].Add(s);
				
				string kc = $"{s.MaLop}|{s.Thu}|{s.Tiet}";
				if (!classAtTime.ContainsKey(kc))
					classAtTime[kc] = new List<AssignmentSlot>();
				classAtTime[kc].Add(s);
			}

			int conflicts = 0;
			foreach (var kv in teacherAtTime)
			{
				if (kv.Value.Count > 1)
				{
					conflicts += kv.Value.Count - 1;
					var details = string.Join(", ", kv.Value.Select(s => $"Lớp {s.MaLop} Môn {s.MaMon}"));
					report.Messages.Add($"Trùng GV {kv.Key}: {details}");
				}
			}
			
			foreach (var kv in classAtTime)
			{
				if (kv.Value.Count > 1)
				{
					conflicts += kv.Value.Count - 1;
					var details = string.Join(", ", kv.Value.Select(s => $"GV {s.MaGV} Môn {s.MaMon}"));
					report.Messages.Add($"Trùng Lớp {kv.Key}: {details}");
				}
			}

			report.HardViolations = conflicts;
			return report;
		}

		public int EvaluateCost(ScheduleSolution sol, WeightConfig w)
		{
			var conflicts = AnalyzeConflicts(sol);
			int hard = conflicts.HardViolations * HardPenalty;

			// Calculate soft constraint violations
			int consecutiveHeavy = CalculateConsecutiveHeavy(sol);
			int subjectSpread = CalculateSubjectSpread(sol);
			int dailyBalance = CalculateDailyBalance(sol);
			int stability = 0; // Placeholder for now

			int soft = w.TrongSoMonNangLienTiep * consecutiveHeavy
				+ w.TrongSoTrenMotNgay * subjectSpread
				+ w.TrongSoCanBangNgay * dailyBalance
				+ w.TrongSoOnDinh * stability;

			sol.SoftCounts = new SoftCounts
			{
				DemMonNangLienTiep = consecutiveHeavy,
				DemPhanBoTrongNgay = subjectSpread,
				DemCanBangNgay = dailyBalance,
				DemOnDinh = stability
			};

			return hard + soft;
		}

		/// <summary>
		/// Penalty for having too many periods of the same subject on consecutive days.
		/// </summary>
		private int CalculateConsecutiveHeavy(ScheduleSolution sol)
		{
			int penalty = 0;
			var byClassSubject = sol.Slots
				.GroupBy(s => new { s.MaLop, s.MaMon })
				.ToList();

			foreach (var group in byClassSubject)
			{
				var days = group.Select(s => s.Thu).OrderBy(d => d).ToList();
				for (int i = 1; i < days.Count; i++)
				{
					if (days[i] == days[i - 1] + 1)
					{
						// Consecutive days - check if too many periods on these days
						int periodsOnDay1 = group.Count(s => s.Thu == days[i - 1]);
						int periodsOnDay2 = group.Count(s => s.Thu == days[i]);
						if (periodsOnDay1 >= 3 || periodsOnDay2 >= 3)
						{
							penalty += (periodsOnDay1 + periodsOnDay2 - 2);
						}
					}
				}
			}

			return penalty;
		}

		/// <summary>
		/// Penalty for having too many periods of the same subject on the same day.
		/// Cho phép đến 4 tiết/ngày, nhưng ưu tiên các tiết liên tiếp (consecutive).
		/// </summary>
		private int CalculateSubjectSpread(ScheduleSolution sol)
		{
			int penalty = 0;
			var byClassSubjectDay = sol.Slots
				.GroupBy(s => new { s.MaLop, s.MaMon, s.Thu })
				.ToList();

			// Tracking auxiliary (môn trái buổi) periods per (Lớp, Môn) theo từng ngày
			// Mục tiêu: nếu đã xếp tiết trái buổi thì nên gom vào CÙNG 1 BUỔI trong 1 NGÀY,
			// tránh rải rác nhiều ngày khác nhau.
			var auxiliaryByClassSubject = new Dictionary<(int MaLop, int MaMon), Dictionary<int, int>>();

			foreach (var group in byClassSubjectDay)
			{
				int periodsOnDay = group.Count();
				var periods = group.Select(s => s.Tiet).OrderBy(t => t).ToList();

				// Xác định khối để biết buổi chính / buổi phụ
				int khoi = GetKhoiForClass(group.Key.MaLop);
				bool isMainSessionMorning = (khoi == 11 || khoi == 12); // 11,12: buổi chính = sáng; 10: buổi chính = chiều
				
				// Cho phép đến 4 tiết/ngày, chỉ penalty khi > 4
				if (periodsOnDay > 4)
				{
					penalty += (periodsOnDay - 4) * (periodsOnDay - 4); // Quadratic penalty
				}
				else if (periodsOnDay == 4)
				{
					// Kiểm tra xem 4 tiết có liên tiếp trong CÙNG BUỔI không
					bool isConsecutive = ArePeriodsConsecutive(periods);
					
					// Nếu 4 tiết nhưng không liên tiếp hoặc vượt ranh giới buổi → penalty
					if (!isConsecutive)
					{
						penalty += 10; // Penalty cho việc không liên tiếp hoặc vượt ranh giới buổi
					}
				}
				else if (periodsOnDay == 3)
				{
					// Kiểm tra xem 3 tiết có liên tiếp trong CÙNG BUỔI không
					bool isConsecutive = ArePeriodsConsecutive(periods);
					
					// Nếu 3 tiết nhưng không liên tiếp hoặc vượt ranh giới buổi → penalty nhẹ
					if (!isConsecutive)
					{
						penalty += 2; // Penalty nhẹ
					}
				}
				else if (periodsOnDay == 2)
				{
					// Kiểm tra xem 2 tiết có liên tiếp trong CÙNG BUỔI không
					bool isConsecutive = ArePeriodsConsecutive(periods);
					
					// Nếu 2 tiết nhưng không liên tiếp hoặc vượt ranh giới buổi → penalty rất nhẹ
					if (!isConsecutive)
					{
						penalty += 1; // Penalty rất nhẹ
					}
				}
				
				// Penalty thêm nếu các tiết rải rác giữa 2 buổi (không gom lại)
				var morningPeriods = periods.Where(p => p >= 1 && p <= 5).ToList();
				var afternoonPeriods = periods.Where(p => p >= 6 && p <= 10).ToList();

				// Thu thập thống kê các tiết TRÁI BUỔI (auxiliary) cho từng (Lớp, Môn, Ngày)
				// - Khối 11,12: buổi chính = sáng → trái buổi = chiều (tiết 6-10)
				// - Khối 10: buổi chính = chiều → trái buổi = sáng (tiết 1-5)
				int auxiliaryCountOnDay = isMainSessionMorning
					? afternoonPeriods.Count   // 11,12 → chiều là trái buổi
					: morningPeriods.Count;    // 10 → sáng là trái buổi

				if (auxiliaryCountOnDay > 0)
				{
					var key = (group.Key.MaLop, group.Key.MaMon);
					if (!auxiliaryByClassSubject.TryGetValue(key, out var dayDict))
					{
						dayDict = new Dictionary<int, int>();
						auxiliaryByClassSubject[key] = dayDict;
					}

					if (!dayDict.ContainsKey(group.Key.Thu))
					{
						dayDict[group.Key.Thu] = 0;
					}
					dayDict[group.Key.Thu] += auxiliaryCountOnDay;
				}
				
				// Nếu có tiết ở cả 2 buổi và mỗi buổi < 2 tiết → penalty (rời rạc)
				if (morningPeriods.Count > 0 && afternoonPeriods.Count > 0)
				{
					if (morningPeriods.Count < 2 && afternoonPeriods.Count < 2)
					{
						penalty += 5; // Penalty cho việc rời rạc giữa 2 buổi
					}
				}
			}

			// Penalty cấp cao hơn cho việc rải các TIẾT TRÁI BUỔI của cùng một (Lớp, Môn)
			// trên NHIỀU ngày khác nhau (môn trái buổi phải gom lại thành 1 buổi trong 1 ngày).
			foreach (var kvp in auxiliaryByClassSubject)
			{
				var dayCounts = kvp.Value;
				if (dayCounts.Count <= 1)
				{
					// Tất cả tiết trái buổi đã gom trong 1 ngày → OK, không phạt
					continue;
				}

				int distinctDays = dayCounts.Count;
				int totalAuxiliaryPeriods = dayCounts.Values.Sum();

				// Phạt theo số ngày dư (ngoài ngày đầu tiên) * tổng số tiết trái buổi
				// Ví dụ: 1 tiết sáng thứ 3 + 1 tiết sáng thứ 5 (khối 10) → distinctDays = 2, totalAux = 2
				// → penalty thêm = (2 - 1) * 2 = 2.
				int extraDays = distinctDays - 1;
				penalty += extraDays * totalAuxiliaryPeriods;
			}

			return penalty;
		}

		/// <summary>
		/// Kiểm tra xem các tiết có liên tiếp trong CÙNG BUỔI không (consecutive within same session).
		/// Buổi sáng: tiết 1-5, Buổi chiều: tiết 6-10
		/// Ví dụ: [1, 2, 3] hoặc [2, 3, 4, 5] → true (cùng buổi sáng)
		/// Ví dụ: [6, 7, 8] hoặc [7, 8, 9, 10] → true (cùng buổi chiều)
		/// Ví dụ: [1, 3, 5] → false (không liên tiếp)
		/// Ví dụ: [4, 5, 6] → false (vượt ranh giới buổi)
		/// </summary>
		private bool ArePeriodsConsecutive(IEnumerable<int> periods)
		{
			var sorted = periods.OrderBy(p => p).ToList();
			if (sorted.Count <= 1) return true;
			
			// Kiểm tra xem tất cả tiết có trong cùng buổi không
			bool allInMorning = sorted.All(p => p >= 1 && p <= 5);
			bool allInAfternoon = sorted.All(p => p >= 6 && p <= 10);
			
			if (!allInMorning && !allInAfternoon)
			{
				// Có tiết ở cả 2 buổi → không consecutive (vượt ranh giới)
				return false;
			}
			
			// Kiểm tra liên tiếp trong cùng buổi
			for (int i = 1; i < sorted.Count; i++)
			{
				if (sorted[i] != sorted[i - 1] + 1)
					return false;
			}
			return true;
		}

		/// <summary>
		/// Xác định buổi của một tiết (sáng: 1-5, chiều: 6-10).
		/// </summary>
		private string GetSessionForPeriod(int tiet)
		{
			if (tiet >= 1 && tiet <= 5) return "morning";
			if (tiet >= 6 && tiet <= 10) return "afternoon";
			return "unknown";
		}

		/// <summary>
		/// Penalty for unbalanced distribution of periods across days (some days too heavy, some too light).
		/// </summary>
		private int CalculateDailyBalance(ScheduleSolution sol)
		{
			int penalty = 0;
			var byClassDay = sol.Slots
				.GroupBy(s => new { s.MaLop, s.Thu })
				.ToList();

			var byClass = byClassDay.GroupBy(g => g.Key.MaLop).ToList();
			foreach (var classGroup in byClass)
			{
				var periodsPerDay = classGroup.Select(g => g.Count()).ToList();
				if (periodsPerDay.Count == 0) continue;

				int avg = periodsPerDay.Sum() / periodsPerDay.Count;
				foreach (int count in periodsPerDay)
				{
					int diff = Math.Abs(count - avg);
					if (diff > 2) // Allow some variance
					{
						penalty += diff - 2;
					}
				}
			}

			return penalty;
		}

	public void PersistToTemp(int semesterId, int weekNo, ScheduleSolution sol)
	{
		// Remove duplicate slots before persisting
		// Duplicates are slots with same (MaLop, Thu, Tiet) - keep only the first one
		var uniqueSlots = new List<AssignmentSlot>();
		var seenSlots = new HashSet<string>(); // Key: "{MaLop}-{Thu}-{Tiet}"
		
		foreach (var slot in sol.Slots)
		{
			string key = $"{slot.MaLop}-{slot.Thu}-{slot.Tiet}";
			if (!seenSlots.Contains(key))
			{
				seenSlots.Add(key);
				uniqueSlots.Add(slot);
			}
			else
			{
				// Log duplicate for debugging
				System.Diagnostics.Debug.WriteLine($"⚠️ Duplicate slot detected and removed: Lớp {slot.MaLop}, Thu {slot.Thu}, Tiet {slot.Tiet}, Môn {slot.MaMon}, GV {slot.MaGV}");
			}
		}
		
		// Create a new solution with unique slots only
		var uniqueSolution = new ScheduleSolution
		{
			Slots = new BindingList<AssignmentSlot>(uniqueSlots),
			Cost = sol.Cost
		};
		
		if (uniqueSlots.Count < sol.Slots.Count)
		{
			System.Diagnostics.Debug.WriteLine($"⚠️ Removed {sol.Slots.Count - uniqueSlots.Count} duplicate slots before persisting. Original: {sol.Slots.Count}, Unique: {uniqueSlots.Count}");
		}
		
		var bus = new ThoiKhoaBieuBUS();
		bus.ClearTempForSemester(semesterId, weekNo); // Clear only for this semester/week
		bus.InsertTemp(semesterId, weekNo, uniqueSolution);
	}

		public void AcceptToOfficial(int semesterId, int weekNo)
		{
			var bus = new ThoiKhoaBieuBUS();
			bus.AcceptTempToOfficial(semesterId, weekNo);
		}

	public void RollbackTemp()
	{
		var bus = new ThoiKhoaBieuBUS();
		bus.ClearTemp();
	}

	/// <summary>
	/// Maximum number of missing periods that is considered acceptable for a "success with warning" status.
	/// Schedules with missing periods <= this threshold will be marked as Success=true and can be accepted.
	/// </summary>
	private const int MAX_MISSING_PERIODS_ACCEPTABLE = 10;

	/// <summary>
	/// High-level method: Generate schedule using config and persist to TKB_Temp.
	/// </summary>
	public async Task<ScheduleGenerationResult> GenerateToTempWithConfigAsync(
		int semesterId,
		int weekNo,
		TimetableConfigRoot config,
		CancellationToken cancellationToken,
		IProgress<string> progress = null)
	{
		var result = new ScheduleGenerationResult
		{
			SemesterId = semesterId,
			WeekNo = weekNo
		};

		try
		{
			progress?.Report($"Đang tải dữ liệu phân công giảng dạy...");
			
			// Build request from database
			var request = BuildRequestFromDatabase(semesterId, weekNo);
			
			// Report assignment summary
			var byClass = request.Assignments.GroupBy(a => a.MaLop).ToList();
			int totalRequiredPeriods = request.Assignments.Sum(a => a.SoTietTuan);
			int totalAvailableSlots = byClass.Count * (request.SlotsConfig.ThuKetThuc - request.SlotsConfig.ThuBatDau + 1) * request.SlotsConfig.SoTietMoiNgay;
			
			progress?.Report($"📋 Tổng hợp: {request.Assignments.Count} phân công cho {byClass.Count} lớp");
			progress?.Report($"📊 Tổng số tiết yêu cầu: {totalRequiredPeriods} tiết/tuần");
			progress?.Report($"📊 Tổng số slot có sẵn: {totalAvailableSlots} slot/tuần ({byClass.Count} lớp × {(request.SlotsConfig.ThuKetThuc - request.SlotsConfig.ThuBatDau + 1)} ngày × {request.SlotsConfig.SoTietMoiNgay} tiết/ngày)");
			
			if (totalRequiredPeriods > totalAvailableSlots)
			{
				progress?.Report($"⚠️ CẢNH BÁO: Số tiết yêu cầu ({totalRequiredPeriods}) vượt quá số slot có sẵn ({totalAvailableSlots})!");
			}
			
			foreach (var classGroup in byClass.Take(3)) // Show first 3 classes
			{
				var classId = classGroup.Key;
				var classTotal = classGroup.Sum(a => a.SoTietTuan);
				var subjects = classGroup.Select(a => $"Môn {a.MaMon} ({a.SoTietTuan} tiết)").ToList();
				progress?.Report($"  - Lớp {classId}: {classTotal} tiết/tuần ({string.Join(", ", subjects.Take(5))}...)");
			}
			if (byClass.Count > 3)
			{
				progress?.Report($"  ... và {byClass.Count - 3} lớp khác");
			}
			
			progress?.Report($"Đang áp dụng cấu hình...");
			
			// Apply config to request
			request = TimetableConfigService.ApplyConfigToRequest(request, config);
			
			progress?.Report($"Đang khởi tạo lịch học ban đầu (Greedy)...");
			
			// Generate initial solution for cost calculation
			var initialSolution = InitializeGreedy(request);
			int initialCost = EvaluateCost(initialSolution, request.WeightConfig);
			result.InitialCost = initialCost;
			
			// Report initial placement
			var initialCoverage = ValidatePeriodCoverage(request, initialSolution);
			int initialTotalRequired = request.Assignments.Sum(a => a.SoTietTuan);
			int initialTotalPlaced = initialSolution.Slots.Count;
			progress?.Report($"  Đã xếp {initialTotalPlaced}/{initialTotalRequired} tiết trong giải pháp ban đầu");
			
			progress?.Report($"Đang tối ưu hóa lịch học (Tabu Search)...");
			
			// Run scheduling algorithm
			var solution = await Task.Run(() => GenerateSchedule(request, cancellationToken), cancellationToken);
			
			progress?.Report($"Đang xác thực lịch học...");
			
			// Validate hard constraints FIRST - if violated, don't persist
			bool isValid = ValidateHardConstraints(solution);
			var conflicts = AnalyzeConflicts(solution);
			result.HardViolations = conflicts.HardViolations;
			result.HardConstraintViolated = conflicts.HardViolations > 0;
			result.FinalCost = solution.Cost;
			result.TotalSlots = solution.Slots.Count;
			
			// If hard constraints are violated, mark as failure and don't persist
			if (result.HardConstraintViolated)
			{
				result.Success = false;
				result.IsAcceptable = false;
				result.Message = $"❌ Tạo lịch thất bại: Lịch có xung đột lớp/giáo viên ({conflicts.HardViolations} vi phạm ràng buộc cứng).";
				progress?.Report(result.Message);
				return result;
			}
			
			// Check period coverage (verify all subjects got required periods)
			var coverage = ValidatePeriodCoverage(request, solution);
			result.PeriodCoverage = coverage;
			
			// Calculate total required and assigned periods
			int totalRequired = request.Assignments.Sum(a => a.SoTietTuan);
			int totalPlaced = solution.Slots.Count;
			result.TotalRequiredPeriods = totalRequired;
			result.AssignedPeriods = totalPlaced;
			
			// Report final slot count
			if (totalPlaced < totalRequired)
			{
				progress?.Report($"⚠️ Sau khi tối ưu: {totalPlaced}/{totalRequired} tiết đã xếp (thiếu {totalRequired - totalPlaced} tiết)");
			}
			
			// Find incomplete assignments (group by Lop+Mon to avoid duplicates)
			var assignmentGroups = request.Assignments
				.GroupBy(a => new { a.MaLop, a.MaMon })
				.Select(g => g.First())
				.ToList();
			
			int missingSubjectsCount = 0;
			foreach (var req in assignmentGroups)
			{
				string key = $"{req.MaLop}|{req.MaMon}";
				if (coverage.ContainsKey(key))
				{
					var (required, placed) = coverage[key];
					if (placed < required)
					{
						missingSubjectsCount++;
						result.IncompleteAssignments.Add($"Lớp {req.MaLop}, Môn {req.MaMon}: Cần {required} tiết, đã xếp {placed} tiết");
					}
				}
				else
				{
					// Assignment not found in coverage - means 0 slots placed
					missingSubjectsCount++;
					result.IncompleteAssignments.Add($"Lớp {req.MaLop}, Môn {req.MaMon}: Cần {req.SoTietTuan} tiết, đã xếp 0 tiết");
				}
			}
			
			result.MissingSubjectsCount = missingSubjectsCount;
			
			// Determine success status based on missing periods threshold
			int missingPeriods = result.MissingPeriods;
			
			if (missingPeriods == 0)
			{
				// Perfect success - all periods placed
				result.Success = true;
				result.IsAcceptable = true;
				result.Message = $"✅ Tạo thành công: {totalPlaced}/{totalRequired} tiết đã xếp. Tất cả các môn đã được xếp đủ số tiết. Chi phí: {result.FinalCost}.";
			}
			else if (missingPeriods <= MAX_MISSING_PERIODS_ACCEPTABLE)
			{
				// Success with warning - acceptable schedule with minor missing periods
				result.Success = true;
				result.IsAcceptable = true;
				result.Message = $"⚠️ Hoàn thành với cảnh báo: {totalPlaced}/{totalRequired} tiết đã xếp (thiếu {missingPeriods} tiết của {missingSubjectsCount} môn). Chi phí: {result.FinalCost}.";
			}
			else
			{
				// Failure - too many missing periods
				result.Success = false;
				result.IsAcceptable = false;
				result.Message = $"❌ Tạo lịch thất bại: còn thiếu quá nhiều tiết ({missingPeriods}/{totalRequired} tiết của {missingSubjectsCount} môn). Chi phí: {result.FinalCost}.";
			}
			
			progress?.Report($"Đang lưu vào bảng tạm...");
			
			// Persist to temp (even for partial success, so user can inspect and manually adjust)
			PersistToTemp(semesterId, weekNo, solution);
			
			progress?.Report(result.Message);
		}
		catch (OperationCanceledException)
		{
			result.Success = false;
			result.Message = "Hủy bỏ tạo lịch học.";
			progress?.Report(result.Message);
		}
		catch (Exception ex)
		{
			result.Success = false;
			result.Message = $"Lỗi: {ex.Message}";
			progress?.Report(result.Message);
		}
		
		return result;
	}

	/// <summary>
	/// Convenience wrapper: Accept temp schedule for specific semester/week.
	/// </summary>
	public void AcceptTempForSemester(int semesterId, int weekNo)
	{
		AcceptToOfficial(semesterId, weekNo);
	}

	/// <summary>
	/// Convenience wrapper: Rollback temp schedule, optionally filtered by semester/week.
	/// </summary>
	public void RollbackTempForSemester(int semesterId, int? weekNo = null)
	{
		var bus = new ThoiKhoaBieuBUS();
		if (weekNo.HasValue)
		{
			bus.ClearTempForSemester(semesterId, weekNo.Value);
		}
		else
		{
			bus.ClearTemp();
		}
	}

		private ScheduleSolution InitializeGreedy(ScheduleRequest request)
		{
			var sol = new ScheduleSolution();
			
			// Create a list of all periods to place (with assignment info)
			var periodsToPlace = new BindingList<AssignmentRequirement>();
			foreach (var req in request.Assignments)
			{
				for (int i = 0; i < req.SoTietTuan; i++)
				{
					periodsToPlace.Add(req);
				}
			}

			// Shuffle to avoid clustering
			var rand = new Random(42);
			periodsToPlace = new BindingList<AssignmentRequirement>(periodsToPlace.OrderBy(x => rand.Next()).ToList());

			// Track how many periods of each (class, subject) are placed per day
			var dailyCount = new Dictionary<string, int>(); // key: "{maLop}|{maMon}|{thu}"
			
			// Generate all possible time slots (these can be reused by different classes)
			var allTimeSlots = new BindingList<(int thu, int tiet)>();
			for (int thu = request.SlotsConfig.ThuBatDau; thu <= request.SlotsConfig.ThuKetThuc; thu++)
			{
				for (int tiet = 1; tiet <= request.SlotsConfig.SoTietMoiNgay; tiet++)
				{
					allTimeSlots.Add((thu, tiet));
				}
			}

			// Place each period, trying to spread subjects across days
			int placedCount = 0;
			int failedCount = 0;
			var failedReasons = new Dictionary<string, int>(); // Track why slots failed
			
			foreach (var req in periodsToPlace)
			{
				bool placed = false;
				
				// Get class grade level (khối) to determine main/auxiliary session
				int khoi = GetKhoiForClass(req.MaLop);
				bool isMainSessionMorning = (khoi == 11 || khoi == 12); // Khối 11,12: buổi chính = sáng
				// Khối 10: buổi chính = chiều (isMainSessionMorning = false)
				
				// Count how many periods already placed for this assignment
				int periodsPlaced = sol.Slots.Count(s => s.MaLop == req.MaLop && s.MaMon == req.MaMon);
				int totalRequired = req.SoTietTuan;
				
				// Determine priority strategy based on how many periods are already placed
				// Strategy: Fill main session first, then auxiliary session
				var candidateSlots = allTimeSlots
					.Where(slot =>
					{
						// Check if teacher is busy at this time
						bool teacherBusy = sol.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet);
						// Check if class is busy at this time
						bool classBusy = sol.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet);
						return !teacherBusy && !classBusy;
					})
					.OrderBy(slot =>
					{
						// Priority 1: Prefer main session (morning for 11/12, afternoon for 10)
						bool isMainSession = isMainSessionMorning ? (slot.tiet <= 5) : (slot.tiet >= 6);
						int priority = isMainSession ? 0 : 1; // Main session = 0 (higher priority)
						
						// Priority 2: Prefer days with fewer periods of this subject already placed
						string key = $"{req.MaLop}|{req.MaMon}|{slot.thu}";
						int countOnDay = dailyCount.ContainsKey(key) ? dailyCount[key] : 0;
						
						// Priority 3: Within main session, prefer to fill one day first before spreading
						// Count how many periods of this subject-class are already on this day
						int periodsOnThisDay = sol.Slots.Count(s => 
							s.MaLop == req.MaLop && s.MaMon == req.MaMon && s.Thu == slot.thu);
						
						// Priority 4: Ưu tiên đặt consecutive periods trong CÙNG BUỔI (liên tiếp) - CHÍNH SÁCH MỚI
						int consecutiveBonus = 0;
						if (periodsOnThisDay > 0 && periodsOnThisDay < 4)
						{
							// Đã có tiết trên ngày này → ưu tiên đặt liên tiếp trong CÙNG BUỔI
							var existingPeriods = sol.Slots
								.Where(s => s.MaLop == req.MaLop && s.MaMon == req.MaMon && s.Thu == slot.thu)
								.Select(s => s.Tiet)
								.OrderBy(t => t)
								.ToList();
							
							// Kiểm tra xem slot này có tạo thành consecutive trong CÙNG BUỔI không
							var testPeriods = existingPeriods.Concat(new[] { slot.tiet }).OrderBy(t => t).ToList();
							bool wouldBeConsecutive = ArePeriodsConsecutive(testPeriods);
							
							// Kiểm tra xem có cùng buổi với các tiết đã có không
							string slotSession = GetSessionForPeriod(slot.tiet);
							bool sameSessionAsExisting = existingPeriods.All(p => GetSessionForPeriod(p) == slotSession);
							
							if (wouldBeConsecutive && sameSessionAsExisting)
							{
								consecutiveBonus = -50; // Ưu tiên cao cho consecutive trong cùng buổi
							}
							else if (sameSessionAsExisting && !wouldBeConsecutive)
							{
								consecutiveBonus = 10; // Penalty nhẹ cho cùng buổi nhưng không liên tiếp
							}
							else
							{
								consecutiveBonus = 30; // Penalty cao cho khác buổi (rời rạc)
							}
						}
						
						// Priority 5: Gom các tiết trái buổi vào cùng 1 buổi (tránh rời rạc)
						int sessionConcentrationBonus = 0;
						if (!isMainSession && periodsOnThisDay == 0)
						{
							// Đang ở buổi phụ và chưa có tiết nào trong ngày này
							// Kiểm tra xem có ngày nào khác đã có tiết ở buổi phụ chưa
							var auxiliaryPeriodsInOtherDays = sol.Slots
								.Where(s => s.MaLop == req.MaLop && s.MaMon == req.MaMon && s.Thu != slot.thu)
								.Select(s => s.Tiet)
								.Where(t => GetSessionForPeriod(t) == (isMainSessionMorning ? "afternoon" : "morning"))
								.ToList();
							
							if (auxiliaryPeriodsInOtherDays.Count > 0)
							{
								// Đã có tiết buổi phụ ở ngày khác → ưu tiên gom vào ngày đó
								sessionConcentrationBonus = 20; // Penalty cho việc tạo buổi phụ mới
							}
						}
						else if (!isMainSession && periodsOnThisDay > 0)
						{
							// Đang ở buổi phụ và đã có tiết trong ngày này
							// Kiểm tra xem các tiết đã có có ở buổi phụ không
							var existingPeriodsInDay = sol.Slots
								.Where(s => s.MaLop == req.MaLop && s.MaMon == req.MaMon && s.Thu == slot.thu)
								.Select(s => s.Tiet)
								.ToList();
							
							bool allInAuxiliary = existingPeriodsInDay.All(t => GetSessionForPeriod(t) == (isMainSessionMorning ? "afternoon" : "morning"));
							
							if (allInAuxiliary)
							{
								sessionConcentrationBonus = -30; // Ưu tiên gom vào cùng buổi phụ
							}
						}
						
						// If we're in auxiliary session and main session is not full, prefer to fill main session first
						if (!isMainSession)
						{
							// Check if main session is already full for this class on this day
							int mainSessionPeriodsOnDay = sol.Slots.Count(s => 
								s.MaLop == req.MaLop && s.Thu == slot.thu && 
								(isMainSessionMorning ? s.Tiet <= 5 : s.Tiet >= 6));
							int maxMainSessionPeriods = isMainSessionMorning ? 5 : 5; // 5 periods per day for main session
							
							if (mainSessionPeriodsOnDay < maxMainSessionPeriods)
							{
								priority += 100; // Heavily penalize auxiliary session if main session not full
							}
							else
							{
								// Main session is full, now prefer to concentrate auxiliary periods on one day
								priority += periodsOnThisDay > 0 ? 0 : 10; // Prefer days that already have auxiliary periods
							}
						}
						
						// Combine all priorities: session priority > consecutive bonus > session concentration > daily count > periods on day
						return priority * 100000 + consecutiveBonus * 10000 + sessionConcentrationBonus * 1000 + countOnDay * 100 + periodsOnThisDay;
					})
					.ThenBy(slot => rand.Next()) // Add some randomness for ties
					.ToList();

				// Try to place in the best candidate slot
				foreach (var slot in candidateSlots)
				{
					sol.Slots.Add(new AssignmentSlot
					{
						MaLop = req.MaLop,
						Thu = slot.thu,
						Tiet = slot.tiet,
						MaMon = req.MaMon,
						MaGV = req.MaGV
					});

					// Update daily count
					string dayKey = $"{req.MaLop}|{req.MaMon}|{slot.thu}";
					dailyCount[dayKey] = dailyCount.ContainsKey(dayKey) ? dailyCount[dayKey] + 1 : 1;

					placed = true;
					placedCount++;
					break;
				}

				if (!placed)
				{
					failedCount++;
					// Analyze why it failed
					int teacherConflicts = allTimeSlots.Count(slot => sol.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet));
					int classConflicts = allTimeSlots.Count(slot => sol.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet));
					string reason = teacherConflicts >= 50 ? "GV bận" : (classConflicts >= 50 ? "Lớp bận" : "Không rõ");
					failedReasons[reason] = failedReasons.ContainsKey(reason) ? failedReasons[reason] + 1 : 1;
				}
			}

			// Log statistics for debugging
			if (failedCount > 0)
			{
				System.Diagnostics.Debug.WriteLine($"InitializeGreedy: Placed {placedCount}/{periodsToPlace.Count}, Failed: {failedCount}");
				foreach (var kvp in failedReasons)
				{
					System.Diagnostics.Debug.WriteLine($"  - {kvp.Key}: {kvp.Value}");
				}
			}

			return sol;
		}

		private IEnumerable<ScheduleSolution> GenerateNeighborhood(ScheduleSolution current, ScheduleRequest request)
		{
			var list = new List<ScheduleSolution>();
			var slots = current.Slots;
			var rand = new Random();
			int maxNeighbors = Math.Min(100, slots.Count * 2); // Limit neighborhood size
			int generated = 0;

			// Strategy 1: Swap slots within same class (to spread subjects across days)
			var byClass = slots.GroupBy(s => s.MaLop).ToList();
			foreach (var classGroup in byClass)
			{
				var classSlots = classGroup.ToList();
				for (int i = 0; i < Math.Min(10, classSlots.Count) && generated < maxNeighbors; i++)
				{
					for (int j = i + 1; j < Math.Min(10, classSlots.Count) && generated < maxNeighbors; j++)
					{
						var a = classSlots[i];
						var b = classSlots[j];
						
						// Only swap if it makes sense (different days or different subjects)
						if (a.Thu == b.Thu && a.MaMon == b.MaMon) continue;

						var clone = Clone(current);
						var slotA = clone.Slots.First(x => x.MaLop == a.MaLop && x.MaMon == a.MaMon && x.MaGV == a.MaGV && x.Thu == a.Thu && x.Tiet == a.Tiet);
						var slotB = clone.Slots.First(x => x.MaLop == b.MaLop && x.MaMon == b.MaMon && x.MaGV == b.MaGV && x.Thu == b.Thu && x.Tiet == b.Tiet);
						
						// Check if swap is valid (no conflicts)
						bool conflictA = clone.Slots.Any(x => x != slotB && (x.MaLop == slotA.MaLop || x.MaGV == slotA.MaGV) && x.Thu == slotB.Thu && x.Tiet == slotB.Tiet);
						bool conflictB = clone.Slots.Any(x => x != slotA && (x.MaLop == slotB.MaLop || x.MaGV == slotB.MaGV) && x.Thu == slotA.Thu && x.Tiet == slotA.Tiet);
						
						if (!conflictA && !conflictB)
						{
							(slotA.Thu, slotA.Tiet, slotB.Thu, slotB.Tiet) = (slotB.Thu, slotB.Tiet, slotA.Thu, slotA.Tiet);
							list.Add(clone);
							generated++;
						}
					}
				}
			}

			// Strategy 2: Move slot to a different day (to improve spread)
			foreach (var s in slots.OrderBy(x => rand.Next()).Take(Math.Min(30, slots.Count)))
			{
				if (generated >= maxNeighbors) break;
				
				// Try moving to a different day
				for (int thu = request.SlotsConfig.ThuBatDau; thu <= request.SlotsConfig.ThuKetThuc; thu++)
				{
					if (thu == s.Thu) continue; // Skip same day
					
					for (int tiet = 1; tiet <= request.SlotsConfig.SoTietMoiNgay; tiet++)
					{
						bool occupied = slots.Any(x => x.Thu == thu && x.Tiet == tiet && (x.MaLop == s.MaLop || x.MaGV == s.MaGV));
						if (occupied) continue;
						
						var clone = Clone(current);
						var target = clone.Slots.First(x => x.MaLop == s.MaLop && x.MaMon == s.MaMon && x.MaGV == s.MaGV && x.Thu == s.Thu && x.Tiet == s.Tiet);
						target.Thu = thu;
						target.Tiet = tiet;
						list.Add(clone);
						generated++;
						break; // Only one move per slot
					}
				}
			}

			// Strategy 3: Random swaps (for exploration)
			for (int i = 0; i < Math.Min(20, slots.Count) && generated < maxNeighbors; i++)
			{
				int idx1 = rand.Next(slots.Count);
				int idx2 = rand.Next(slots.Count);
				if (idx1 == idx2) continue;

				var a = slots[idx1];
				var b = slots[idx2];
				
				// Only swap if same class (to keep structure)
				if (a.MaLop != b.MaLop) continue;

				var clone = Clone(current);
				var slotA = clone.Slots[idx1];
				var slotB = clone.Slots[idx2];
				
				bool conflictA = clone.Slots.Any(x => x != slotB && (x.MaLop == slotA.MaLop || x.MaGV == slotA.MaGV) && x.Thu == slotB.Thu && x.Tiet == slotB.Tiet);
				bool conflictB = clone.Slots.Any(x => x != slotA && (x.MaLop == slotB.MaLop || x.MaGV == slotB.MaGV) && x.Thu == slotA.Thu && x.Tiet == slotA.Tiet);
				
				if (!conflictA && !conflictB)
				{
					(slotA.Thu, slotA.Tiet, slotB.Thu, slotB.Tiet) = (slotB.Thu, slotB.Tiet, slotA.Thu, slotA.Tiet);
					list.Add(clone);
					generated++;
				}
			}

			// Strategy 4: Try to add missing slots for incomplete assignments
			var coverage = ValidatePeriodCoverage(request, current);
			var missingAssignments = request.Assignments
				.Where(req =>
				{
					string key = $"{req.MaLop}|{req.MaMon}";
					if (coverage.ContainsKey(key))
					{
						var (required, placed) = coverage[key];
						return placed < required;
					}
					return true; // Not found means missing
				})
				.Take(5) // Limit to 5 attempts per iteration
				.ToList();

			foreach (var req in missingAssignments)
			{
				if (generated >= maxNeighbors) break;
				
				// Try to add a new slot for this assignment
				for (int thu = request.SlotsConfig.ThuBatDau; thu <= request.SlotsConfig.ThuKetThuc; thu++)
				{
					if (generated >= maxNeighbors) break;
					
					for (int tiet = 1; tiet <= request.SlotsConfig.SoTietMoiNgay; tiet++)
					{
						// Check if slot is free for this class and teacher
						bool teacherBusy = slots.Any(s => s.MaGV == req.MaGV && s.Thu == thu && s.Tiet == tiet);
						bool classBusy = slots.Any(s => s.MaLop == req.MaLop && s.Thu == thu && s.Tiet == tiet);
						
						if (!teacherBusy && !classBusy)
						{
							var clone = Clone(current);
							clone.Slots.Add(new AssignmentSlot
							{
								MaLop = req.MaLop,
								Thu = thu,
								Tiet = tiet,
								MaMon = req.MaMon,
								MaGV = req.MaGV
							});
							list.Add(clone);
							generated++;
							break; // Only add one slot per assignment per iteration
						}
					}
				}
			}

			return list;
		}

		private ScheduleSolution Clone(ScheduleSolution s)
		{
			return new ScheduleSolution
			{
				Slots = new BindingList<AssignmentSlot>(s.Slots.Select(x => new AssignmentSlot
				{
					MaLop = x.MaLop,
					Thu = x.Thu,
					Tiet = x.Tiet,
					MaMon = x.MaMon,
					MaGV = x.MaGV,
					Phong = x.Phong
				}).ToList()),
				Cost = s.Cost,
				HardViolations = s.HardViolations,
				SoftCounts = new SoftCounts
				{
					DemMonNangLienTiep = s.SoftCounts.DemMonNangLienTiep,
					DemPhanBoTrongNgay = s.SoftCounts.DemPhanBoTrongNgay,
					DemCanBangNgay = s.SoftCounts.DemCanBangNgay,
					DemOnDinh = s.SoftCounts.DemOnDinh
				}
			};
		}

		private string ComputeMoveKey(ScheduleSolution s)
		{
			// derive a light hash from first 5 slots
			return string.Join(";", s.Slots.Take(5).Select(x => $"{x.MaGV},{x.MaLop},{x.MaMon},{x.Thu},{x.Tiet}"));
		}

		/// <summary>
		/// Final attempt to add missing slots for incomplete assignments.
		/// This is called after Tabu Search to try to place any remaining unplaced periods.
		/// </summary>
		private ScheduleSolution TryAddMissingSlots(ScheduleSolution current, ScheduleRequest request)
		{
			var coverage = ValidatePeriodCoverage(request, current);
			// Note: allTimeSlots is kept as List<(int, int)> because BindingList doesn't support tuples
			var allTimeSlots = new List<(int thu, int tiet)>();
			for (int thu = request.SlotsConfig.ThuBatDau; thu <= request.SlotsConfig.ThuKetThuc; thu++)
			{
				for (int tiet = 1; tiet <= request.SlotsConfig.SoTietMoiNgay; tiet++)
				{
					allTimeSlots.Add((thu, tiet));
				}
			}

			var result = Clone(current);
			int added = 0;
			var failedAssignments = new List<string>();

			// Group assignments by (Lop, Mon) to avoid duplicates
			var assignmentGroups = request.Assignments
				.GroupBy(a => new { a.MaLop, a.MaMon })
				.Select(g => g.First())
				.ToList();

			// Collect all missing assignments first
			var missingAssignments = new List<(AssignmentRequirement req, int missing)>();
			foreach (var req in assignmentGroups)
			{
				string key = $"{req.MaLop}|{req.MaMon}";
				if (coverage.ContainsKey(key))
				{
					var (required, placed) = coverage[key];
					int missing = required - placed;
					if (missing > 0)
					{
						missingAssignments.Add((req, missing));
					}
				}
				else
				{
					// Not found in coverage means 0 placed
					missingAssignments.Add((req, req.SoTietTuan));
				}
			}

			// Sort by missing count (try to fill those with fewer missing first)
			missingAssignments = missingAssignments.OrderBy(x => x.missing).ToList();

			// For each incomplete assignment, try to add missing slots
			foreach (var (req, missing) in missingAssignments)
			{
				int remaining = missing;
				
				// Get class grade level (khối) to determine main/auxiliary session
				int khoi = GetKhoiForClass(req.MaLop);
				bool isMainSessionMorning = (khoi == 11 || khoi == 12); // Khối 11,12: buổi chính = sáng
				
				// Try multiple strategies to find available slots
				for (int strategy = 0; strategy < 4 && remaining > 0; strategy++)
				{
					IEnumerable<(int thu, int tiet)> candidateSlots;
					
					switch (strategy)
					{
						case 0:
							// Strategy 1: Prefer main session first (morning for 11/12, afternoon for 10)
							candidateSlots = allTimeSlots
								.Where(slot =>
								{
									bool teacherBusy = result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet);
									bool classBusy = result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet);
									return !teacherBusy && !classBusy;
								})
								.OrderBy(slot =>
								{
									// Priority: main session first
									bool isMainSession = isMainSessionMorning ? (slot.tiet <= 5) : (slot.tiet >= 6);
									return isMainSession ? 0 : 1;
								})
								.ThenBy(slot => slot.thu)
								.ThenBy(slot => slot.tiet);
							break;
						case 1:
							// Strategy 2: Prefer auxiliary session, but concentrate on one day
							candidateSlots = allTimeSlots
								.Where(slot =>
								{
									bool teacherBusy = result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet);
									bool classBusy = result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet);
									if (teacherBusy || classBusy) return false;
									
									// Only consider auxiliary session slots
									bool isMainSession = isMainSessionMorning ? (slot.tiet <= 5) : (slot.tiet >= 6);
									return !isMainSession;
								})
								.OrderBy(slot =>
								{
									// Prefer days that already have auxiliary periods (concentrate on one day)
									int auxiliaryPeriodsOnDay = result.Slots.Count(s => 
										s.MaLop == req.MaLop && s.Thu == slot.thu && 
										(isMainSessionMorning ? s.Tiet >= 6 : s.Tiet <= 5));
									return auxiliaryPeriodsOnDay > 0 ? 0 : 1;
								})
								.ThenBy(slot => slot.thu)
								.ThenBy(slot => slot.tiet);
							break;
						case 2:
							// Strategy 3: Random order (fallback)
							candidateSlots = allTimeSlots
								.Where(slot =>
								{
									bool teacherBusy = result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet);
									bool classBusy = result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet);
									return !teacherBusy && !classBusy;
								})
								.OrderBy(slot => new Random().Next());
							break;
						default:
							// Strategy 4: Any available slot (last resort)
							candidateSlots = allTimeSlots
								.Where(slot =>
								{
									bool teacherBusy = result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet);
									bool classBusy = result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet);
									return !teacherBusy && !classBusy;
								})
								.OrderBy(slot => slot.thu)
								.ThenBy(slot => slot.tiet);
							break;
					}

					foreach (var slot in candidateSlots.Take(remaining))
					{
						// Double-check slot is still available (might have been added in previous iteration)
						bool teacherBusy = result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet);
						bool classBusy = result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet);
						
						if (!teacherBusy && !classBusy)
						{
							result.Slots.Add(new AssignmentSlot
							{
								MaLop = req.MaLop,
								Thu = slot.thu,
								Tiet = slot.tiet,
								MaMon = req.MaMon,
								MaGV = req.MaGV
							});
							added++;
							remaining--;
							
							if (remaining == 0) break;
						}
					}
					
					if (remaining == 0) break;
				}

				if (remaining > 0)
				{
					// Analyze why it failed
					int teacherBusySlots = allTimeSlots.Count(slot => result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet));
					int classBusySlots = allTimeSlots.Count(slot => result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet));
					int availableSlots = allTimeSlots.Count - Math.Max(teacherBusySlots, classBusySlots);
					failedAssignments.Add($"Lớp {req.MaLop}, Môn {req.MaMon} (GV {req.MaGV}): Thiếu {remaining} tiết (GV bận {teacherBusySlots}/50, Lớp bận {classBusySlots}/50, Còn {availableSlots} slot trống)");
				}
			}

			if (added > 0)
			{
				System.Diagnostics.Debug.WriteLine($"TryAddMissingSlots: Added {added} missing slots");
			}
			
			if (failedAssignments.Count > 0)
			{
				System.Diagnostics.Debug.WriteLine($"TryAddMissingSlots: Failed to add slots for {failedAssignments.Count} assignments:");
				foreach (var msg in failedAssignments)
				{
					System.Diagnostics.Debug.WriteLine($"  - {msg}");
				}
			}

			return result;
		}

		/// <summary>
		/// Validate that all required periods for each subject-class combination are placed.
		/// Returns a dictionary mapping "(MaLop, MaMon)" to (Required, Placed) counts.
		/// </summary>
		private Dictionary<string, (int Required, int Placed)> ValidatePeriodCoverage(ScheduleRequest request, ScheduleSolution solution)
		{
			var coverage = new Dictionary<string, (int, int)>();
			
			// Initialize with required counts
			foreach (var req in request.Assignments)
			{
				string key = $"{req.MaLop}|{req.MaMon}";
				if (!coverage.ContainsKey(key))
				{
					coverage[key] = (req.SoTietTuan, 0);
				}
			}
			
			// Count placed periods
			foreach (var slot in solution.Slots)
			{
				string key = $"{slot.MaLop}|{slot.MaMon}";
				if (coverage.ContainsKey(key))
				{
					var (required, placed) = coverage[key];
					coverage[key] = (required, placed + 1);
				}
			}
			
			return coverage;
		}

		/// <summary>
		/// Force placement of missing slots, allowing some soft conflicts if necessary.
		/// This is a last resort when normal placement fails.
		/// </summary>
		private ScheduleSolution TryForcePlaceMissingSlots(ScheduleSolution current, ScheduleRequest request)
		{
			var coverage = ValidatePeriodCoverage(request, current);
			var allTimeSlots = new List<(int thu, int tiet)>();
			for (int thu = request.SlotsConfig.ThuBatDau; thu <= request.SlotsConfig.ThuKetThuc; thu++)
			{
				for (int tiet = 1; tiet <= request.SlotsConfig.SoTietMoiNgay; tiet++)
				{
					allTimeSlots.Add((thu, tiet));
				}
			}

			var result = Clone(current);
			int added = 0;
			var failedAssignments = new List<string>();

			// Group assignments by (Lop, Mon) to avoid duplicates
			var assignmentGroups = request.Assignments
				.GroupBy(a => new { a.MaLop, a.MaMon })
				.Select(g => g.First())
				.ToList();

			// Collect all missing assignments
			var missingAssignments = new List<(AssignmentRequirement req, int missing)>();
			foreach (var req in assignmentGroups)
			{
				string key = $"{req.MaLop}|{req.MaMon}";
				if (coverage.ContainsKey(key))
				{
					var (required, placed) = coverage[key];
					int missing = required - placed;
					if (missing > 0)
					{
						missingAssignments.Add((req, missing));
					}
				}
				else
				{
					missingAssignments.Add((req, req.SoTietTuan));
				}
			}

			// Sort by missing count
			missingAssignments = missingAssignments.OrderBy(x => x.missing).ToList();

			// Try to force place each missing slot
			foreach (var (req, missing) in missingAssignments)
			{
				int remaining = missing;
				
				// Get class grade level (khối) to determine main/auxiliary session
				int khoi = GetKhoiForClass(req.MaLop);
				bool isMainSessionMorning = (khoi == 11 || khoi == 12); // Khối 11,12: buổi chính = sáng
				
				// Try all available slots, even if there are conflicts
				// Priority: main session > auxiliary session, no conflict > only teacher conflict > only class conflict > both conflicts
				var candidateSlots = allTimeSlots
					.Select(slot =>
					{
						bool teacherBusy = result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet);
						bool classBusy = result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet);
						
						// Determine if this is main session
						bool isMainSession = isMainSessionMorning ? (slot.tiet <= 5) : (slot.tiet >= 6);
						
						int conflictLevel = 0;
						if (teacherBusy) conflictLevel += 1;
						if (classBusy) conflictLevel += 2;
						
						// Session priority: main session = 0, auxiliary = 1
						int sessionPriority = isMainSession ? 0 : 1;
						
						return new { slot, conflictLevel, teacherBusy, classBusy, sessionPriority };
					})
					.Where(x => !x.teacherBusy || !x.classBusy) // At least one must be free
					.OrderBy(x => x.sessionPriority) // Prefer main session first
					.ThenBy(x => x.conflictLevel) // Then prefer no conflict
					.ThenBy(x => new Random().Next()) // Randomize within same conflict level
					.Take(remaining)
					.ToList();

				foreach (var candidate in candidateSlots)
				{
					// Only add if at least teacher OR class is free (soft conflict allowed)
					if (!candidate.teacherBusy || !candidate.classBusy)
					{
						// Check if this exact slot already exists
						bool exists = result.Slots.Any(s => 
							s.MaLop == req.MaLop && 
							s.MaMon == req.MaMon && 
							s.MaGV == req.MaGV && 
							s.Thu == candidate.slot.thu && 
							s.Tiet == candidate.slot.tiet);
						
						if (!exists)
						{
							result.Slots.Add(new AssignmentSlot
							{
								MaLop = req.MaLop,
								Thu = candidate.slot.thu,
								Tiet = candidate.slot.tiet,
								MaMon = req.MaMon,
								MaGV = req.MaGV
							});
							added++;
							remaining--;
							
							if (candidate.conflictLevel > 0)
							{
								System.Diagnostics.Debug.WriteLine($"Force-placed with conflict level {candidate.conflictLevel}: Lớp {req.MaLop}, Môn {req.MaMon}, Thu {candidate.slot.thu}, Tiet {candidate.slot.tiet}");
							}
							
							if (remaining == 0) break;
						}
					}
				}

				if (remaining > 0)
				{
					// Analyze why it failed
					int teacherBusySlots = allTimeSlots.Count(slot => result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet));
					int classBusySlots = allTimeSlots.Count(slot => result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet));
					int bothBusySlots = allTimeSlots.Count(slot => 
						result.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == slot.thu && s.Tiet == slot.tiet) &&
						result.Slots.Any(s => s.MaLop == req.MaLop && s.Thu == slot.thu && s.Tiet == slot.tiet));
					int availableSlots = allTimeSlots.Count - bothBusySlots;
					
					failedAssignments.Add($"Lớp {req.MaLop}, Môn {req.MaMon} (GV {req.MaGV}): Thiếu {remaining} tiết (GV bận {teacherBusySlots}/50, Lớp bận {classBusySlots}/50, Cả hai bận {bothBusySlots}/50, Còn {availableSlots} slot có thể dùng)");
				}
			}

			if (added > 0)
			{
				System.Diagnostics.Debug.WriteLine($"TryForcePlaceMissingSlots: Force-placed {added} slots (may have soft conflicts)");
			}
			
			if (failedAssignments.Count > 0)
			{
				System.Diagnostics.Debug.WriteLine($"TryForcePlaceMissingSlots: Still failed for {failedAssignments.Count} assignments:");
				foreach (var msg in failedAssignments)
				{
					System.Diagnostics.Debug.WriteLine($"  - {msg}");
				}
			}

			return result;
		}

		/// <summary>
		/// Remove hard violations from solution by removing duplicate slots.
		/// Hard violations occur when the same class or teacher has multiple slots at the same time.
		/// </summary>
		private ScheduleSolution RemoveHardViolations(ScheduleSolution sol)
		{
			var result = new ScheduleSolution
			{
				Slots = new BindingList<AssignmentSlot>(),
				Cost = sol.Cost
			};

			// Track slots by class-time and teacher-time to detect duplicates
			var classTimeSlots = new Dictionary<string, AssignmentSlot>(); // Key: "{MaLop}-{Thu}-{Tiet}"
			var teacherTimeSlots = new Dictionary<string, AssignmentSlot>(); // Key: "{MaGV}-{Thu}-{Tiet}"
			var removedCount = 0;

			foreach (var slot in sol.Slots)
			{
				string classKey = $"{slot.MaLop}-{slot.Thu}-{slot.Tiet}";
				string teacherKey = $"{slot.MaGV}-{slot.Thu}-{slot.Tiet}";

				// Check for conflicts
				bool classConflict = classTimeSlots.ContainsKey(classKey);
				bool teacherConflict = teacherTimeSlots.ContainsKey(teacherKey);

				if (!classConflict && !teacherConflict)
				{
					// No conflict, add the slot
					result.Slots.Add(slot);
					classTimeSlots[classKey] = slot;
					teacherTimeSlots[teacherKey] = slot;
				}
				else
				{
					// Conflict detected - remove this duplicate slot
					removedCount++;
					System.Diagnostics.Debug.WriteLine($"⚠️ Removed duplicate slot: Lớp {slot.MaLop}, Thu {slot.Thu}, Tiet {slot.Tiet}, Môn {slot.MaMon}, GV {slot.MaGV} (Class conflict: {classConflict}, Teacher conflict: {teacherConflict})");
				}
			}

			if (removedCount > 0)
			{
				System.Diagnostics.Debug.WriteLine($"RemoveHardViolations: Removed {removedCount} duplicate slots. Original: {sol.Slots.Count}, After cleanup: {result.Slots.Count}");
			}

			return result;
		}

		/// <summary>
		/// Get grade level (khối) for a class. Returns 10, 11, or 12.
		/// Uses cache to avoid repeated database queries.
		/// </summary>
		private int GetKhoiForClass(int maLop)
		{
			if (_classToKhoiCache.ContainsKey(maLop))
			{
				return _classToKhoiCache[maLop];
			}
			
			// Fallback: Load from database if not in cache
			var lopDAO = new LopDAO();
			var lop = lopDAO.LayLopTheoId(maLop);
			if (lop != null)
			{
				_classToKhoiCache[maLop] = lop.MaKhoi;
				return lop.MaKhoi;
			}
			
			// Default: Assume khối 10 if cannot determine
			return 10;
		}
	}
}


