using System;
using System.Collections.Generic;
using System.Threading;

namespace Student_Management_System_CSharp_SGU2025.Scheduling
{
	/// <summary>
	/// Request parameters and data required to generate a weekly schedule.
	/// </summary>
	public class ScheduleRequest
	{
		public int SemesterId { get; set; }
		public int WeekNo { get; set; }
		public IList<int> ClassIds { get; set; } = new List<int>();
		public IList<string> TeacherIds { get; set; } = new List<string>();
		public IList<int> SubjectIds { get; set; } = new List<int>();
		public IList<AssignmentRequirement> Assignments { get; set; } = new List<AssignmentRequirement>();
		public SlotsConfig SlotsConfig { get; set; } = new SlotsConfig();
		public WeightConfig WeightConfig { get; set; } = new WeightConfig();
		public string InitialSolutionStrategy { get; set; } = "Greedy";
		public int IterMax { get; set; } = 5000;
		public int TabuTenure { get; set; } = 9;
		public int TimeBudgetSec { get; set; } = 90;
		public int NoImproveLimit { get; set; } = 500;
	}

	/// <summary>
	/// Defines the time grid configuration for the timetable.
	/// DAYS = {2,3,4,5,6} (Thứ 2..Thứ 6), PERIODS = 1..10 (Sáng 1..5, Chiều 6..10)
	/// </summary>
	public class SlotsConfig
	{
		public int DayOfWeekFrom { get; set; } = 2; // Monday = 2
		public int DayOfWeekTo { get; set; } = 6;   // Friday = 6 (Thứ 6)
		public int PeriodsPerDay { get; set; } = 10; // Tiết 1..10
	}

	/// <summary>
	/// Represents one placed lesson in the timetable.
	/// </summary>
	public class AssignmentSlot
	{
		public int MaLop { get; set; }
		public int Thu { get; set; }
		public int Tiet { get; set; }
		public int MaMon { get; set; }
		public string MaGV { get; set; } = string.Empty;
		public string Phong { get; set; } = string.Empty;
	}

	/// <summary>
	/// The generated solution including the list of placed slots and score.
	/// </summary>
	public class ScheduleSolution
	{
		public List<AssignmentSlot> Slots { get; set; } = new List<AssignmentSlot>();
		public int Cost { get; set; }
		public int HardViolations { get; set; }
		public SoftCounts SoftCounts { get; set; } = new SoftCounts();
	}

	/// <summary>
	/// Soft constraint weights.
	/// </summary>
	public class WeightConfig
	{
		public int ConsecutiveHeavy { get; set; } = 5;
		public int SubjectSpread { get; set; } = 3;
		public int DailyBalance { get; set; } = 2;
		public int Stability { get; set; } = 1;
	}

	/// <summary>
	/// Counts of soft constraint signals.
	/// </summary>
	public class SoftCounts
	{
		public int ConsecutiveHeavy { get; set; }
		public int SubjectSpread { get; set; }
		public int DailyBalance { get; set; }
		public int Stability { get; set; }
	}

	/// <summary>
	/// Hard and soft violation reporting.
	/// </summary>
	public class ConflictReport
	{
		public int HardViolations { get; set; }
		public SoftCounts SoftCounts { get; set; } = new SoftCounts();
		public IList<string> Messages { get; set; } = new List<string>();
	}

	/// <summary>
	/// Required weekly teaching periods per assignment GV-Mon-Lop.
	/// </summary>
	public class AssignmentRequirement
	{
		public int MaLop { get; set; }
		public string MaGV { get; set; } = string.Empty;
		public int MaMon { get; set; }
		public int SoTietTuan { get; set; }
	}
}


