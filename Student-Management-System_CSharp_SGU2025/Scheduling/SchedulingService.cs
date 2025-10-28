using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Student_Management_System_CSharp_SGU2025.Scheduling;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;

namespace Student_Management_System_CSharp_SGU2025.Scheduling
{
	/// <summary>
	/// Tabu Search based auto-scheduling service. Provides generation, evaluation and persistence helpers.
	/// </summary>
	public class SchedulingService
	{
		private const int HardPenalty = 1_000_000;

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

			foreach (var pc in assignments)
			{
				classIds.Add(pc.MaLop);
				teacherIds.Add(pc.MaGiaoVien);
				subjectIds.Add(pc.MaMonHoc);
				var required = phanCongBus.GetRequiredPeriods(pc.MaLop, pc.MaMonHoc, semesterId);
				req.Assignments.Add(new AssignmentRequirement
				{
					MaLop = pc.MaLop,
					MaGV = pc.MaGiaoVien,
					MaMon = pc.MaMonHoc,
					SoTietTuan = required
				});
			}

			req.ClassIds = classIds.ToList();
			req.TeacherIds = teacherIds.ToList();
			req.SubjectIds = subjectIds.ToList();
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
			var teacherAtTime = new Dictionary<string, int>();
			var classAtTime = new Dictionary<string, int>();

			foreach (var s in sol.Slots)
			{
				string kt = $"{s.MaGV}|{s.Thu}|{s.Tiet}";
				teacherAtTime[kt] = teacherAtTime.ContainsKey(kt) ? teacherAtTime[kt] + 1 : 1;
				string kc = $"{s.MaLop}|{s.Thu}|{s.Tiet}";
				classAtTime[kc] = classAtTime.ContainsKey(kc) ? classAtTime[kc] + 1 : 1;
			}

			int conflicts = 0;
			foreach (var kv in teacherAtTime) if (kv.Value > 1) { conflicts += kv.Value - 1; report.Messages.Add($"Trùng GV: {kv.Key}"); }
			foreach (var kv in classAtTime) if (kv.Value > 1) { conflicts += kv.Value - 1; report.Messages.Add($"Trùng Lớp: {kv.Key}"); }

			report.HardViolations = conflicts;
			return report;
		}

		public int EvaluateCost(ScheduleSolution sol, WeightConfig w)
		{
			var conflicts = AnalyzeConflicts(sol);
			int hard = conflicts.HardViolations * HardPenalty;

			// simplistic soft signals placeholders
			int consecutiveHeavy = 0;
			int subjectSpread = 0;
			int dailyBalance = 0;
			int stability = 0;

			int soft = w.ConsecutiveHeavy * consecutiveHeavy
				+ w.SubjectSpread * subjectSpread
				+ w.DailyBalance * dailyBalance
				+ w.Stability * stability;

			sol.SoftCounts = new SoftCounts
			{
				ConsecutiveHeavy = consecutiveHeavy,
				SubjectSpread = subjectSpread,
				DailyBalance = dailyBalance,
				Stability = stability
			};

			return hard + soft;
		}

		public void PersistToTemp(int semesterId, int weekNo, ScheduleSolution sol)
		{
			var bus = new ThoiKhoaBieuBUS();
			bus.ClearTemp();
			bus.InsertTemp(semesterId, weekNo, sol);
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

		private ScheduleSolution InitializeGreedy(ScheduleRequest request)
		{
			var sol = new ScheduleSolution();
			var byClass = request.Assignments
				.GroupBy(a => a.MaLop)
				.ToDictionary(g => g.Key, g => g.ToList());

			for (int thu = request.SlotsConfig.DayOfWeekFrom; thu <= request.SlotsConfig.DayOfWeekTo; thu++)
			{
				for (int tiet = 1; tiet <= request.SlotsConfig.PeriodsPerDay; tiet++)
				{
					foreach (var kv in byClass)
					{
						int maLop = kv.Key;
						foreach (var req in kv.Value.ToList())
						{
							if (req.SoTietTuan <= 0) continue;
							// naive place if free
							bool teacherBusy = sol.Slots.Any(s => s.MaGV == req.MaGV && s.Thu == thu && s.Tiet == tiet);
							bool classBusy = sol.Slots.Any(s => s.MaLop == maLop && s.Thu == thu && s.Tiet == tiet);
							if (teacherBusy || classBusy) continue;

							sol.Slots.Add(new AssignmentSlot
							{
								MaLop = maLop,
								Thu = thu,
								Tiet = tiet,
								MaMon = req.MaMon,
								MaGV = req.MaGV
							});
							req.SoTietTuan -= 1;
						}
					}
				}
			}

			return sol;
		}

		private IEnumerable<ScheduleSolution> GenerateNeighborhood(ScheduleSolution current, ScheduleRequest request)
		{
			// simple neighborhood: try swapping two random slots and moving one to empty
			var list = new List<ScheduleSolution>();
			var slots = current.Slots;
			for (int i = 0; i < Math.Min(50, slots.Count); i++)
			{
				for (int j = i + 1; j < Math.Min(50, slots.Count); j++)
				{
					var clone = Clone(current);
					var a = clone.Slots[i];
					var b = clone.Slots[j];
					(int aThu, int aTiet) = (a.Thu, a.Tiet);
					(a.Thu, a.Tiet) = (b.Thu, b.Tiet);
					(b.Thu, b.Tiet) = (aThu, aTiet);
					list.Add(clone);
				}
			}

			// move one slot to the first empty place
			foreach (var s in slots.Take(50))
			{
				for (int thu = request.SlotsConfig.DayOfWeekFrom; thu <= request.SlotsConfig.DayOfWeekTo; thu++)
				{
					for (int tiet = 1; tiet <= request.SlotsConfig.PeriodsPerDay; tiet++)
					{
						bool occupied = slots.Any(x => x.Thu == thu && x.Tiet == tiet && (x.MaLop == s.MaLop || x.MaGV == s.MaGV));
						if (occupied) continue;
						var clone = Clone(current);
						var target = clone.Slots.First(x => x.MaLop == s.MaLop && x.MaMon == s.MaMon && x.MaGV == s.MaGV && x.Thu == s.Thu && x.Tiet == s.Tiet);
						target.Thu = thu;
						target.Tiet = tiet;
						list.Add(clone);
						break;
					}
				}
			}

			return list;
		}

		private ScheduleSolution Clone(ScheduleSolution s)
		{
			return new ScheduleSolution
			{
				Slots = s.Slots.Select(x => new AssignmentSlot
				{
					MaLop = x.MaLop,
					Thu = x.Thu,
					Tiet = x.Tiet,
					MaMon = x.MaMon,
					MaGV = x.MaGV,
					Phong = x.Phong
				}).ToList(),
				Cost = s.Cost,
				HardViolations = s.HardViolations,
				SoftCounts = new SoftCounts
				{
					ConsecutiveHeavy = s.SoftCounts.ConsecutiveHeavy,
					SubjectSpread = s.SoftCounts.SubjectSpread,
					DailyBalance = s.SoftCounts.DailyBalance,
					Stability = s.SoftCounts.Stability
				}
			};
		}

		private string ComputeMoveKey(ScheduleSolution s)
		{
			// derive a light hash from first 5 slots
			return string.Join(";", s.Slots.Take(5).Select(x => $"{x.MaGV},{x.MaLop},{x.MaMon},{x.Thu},{x.Tiet}"));
		}
	}
}


