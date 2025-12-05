using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		public BindingList<int> ClassIds { get; set; } = new BindingList<int>();
		public BindingList<string> TeacherIds { get; set; } = new BindingList<string>();
		public BindingList<int> SubjectIds { get; set; } = new BindingList<int>();
		public BindingList<AssignmentRequirement> Assignments { get; set; } = new BindingList<AssignmentRequirement>();
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
		/// <summary>
		/// Thứ bắt đầu xếp TKB (2 = Thứ 2).
		/// </summary>
		public int ThuBatDau { get; set; } = 2;

		/// <summary>
		/// Thứ kết thúc xếp TKB (7 = Thứ 7) → 6 ngày/tuần (Thứ 2 → Thứ 7).
		/// </summary>
		public int ThuKetThuc { get; set; } = 7;

		/// <summary>
		/// Số tiết mỗi ngày (1..10: Sáng 1..5, Chiều 6..10).
		/// </summary>
		public int SoTietMoiNgay { get; set; } = 10;
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
		public BindingList<AssignmentSlot> Slots { get; set; } = new BindingList<AssignmentSlot>();
		public int Cost { get; set; }
		public int HardViolations { get; set; }
		public SoftCounts SoftCounts { get; set; } = new SoftCounts();
	}

	/// <summary>
	/// Soft constraint weights.
	/// </summary>
	public class WeightConfig
	{
		/// <summary>
		/// Trọng số cho ràng buộc "môn nặng liên tiếp nhiều ngày".
		/// </summary>
		public int TrongSoMonNangLienTiep { get; set; } = 5;

		/// <summary>
		/// Trọng số cho ràng buộc "trải đều / gom môn trong ngày".
		/// </summary>
		public int TrongSoTrenMotNgay { get; set; } = 3;

		/// <summary>
		/// Trọng số cho ràng buộc "cân bằng số tiết giữa các ngày".
		/// </summary>
		public int TrongSoCanBangNgay { get; set; } = 2;

		/// <summary>
		/// Trọng số cho ràng buộc "ổn định" (hiện tại chưa dùng nhiều).
		/// </summary>
		public int TrongSoOnDinh { get; set; } = 1;
	}

	/// <summary>
	/// Counts of soft constraint signals.
	/// </summary>
	public class SoftCounts
	{
		public int DemMonNangLienTiep { get; set; }
		public int DemPhanBoTrongNgay { get; set; }
		public int DemCanBangNgay { get; set; }
		public int DemOnDinh { get; set; }
	}

	/// <summary>
	/// Hard and soft violation reporting.
	/// </summary>
	public class ConflictReport
	{
		public int HardViolations { get; set; }
		public SoftCounts SoftCounts { get; set; } = new SoftCounts();
		public BindingList<string> Messages { get; set; } = new BindingList<string>();
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

	/// <summary>
	/// Result of schedule generation operation with detailed statistics.
	/// </summary>
	public class ScheduleGenerationResult
	{
		public bool Success { get; set; }
		public int SemesterId { get; set; }
		public int WeekNo { get; set; }
		public int TotalSlots { get; set; }
		public int HardViolations { get; set; }
		public int InitialCost { get; set; }
		public int FinalCost { get; set; }
		public string Message { get; set; } = string.Empty;
		/// <summary>
		/// Dictionary mapping "(MaLop, MaMon)" to (Required, Placed) periods count
		/// </summary>
		public Dictionary<string, (int Required, int Placed)> PeriodCoverage { get; set; } = new Dictionary<string, (int, int)>();
		/// <summary>
		/// List of assignments that couldn't be fully placed
		/// </summary>
		public BindingList<string> IncompleteAssignments { get; set; } = new BindingList<string>();
		
		// New properties for partial success handling
		/// <summary>
		/// Total number of required periods for all assignments
		/// </summary>
		public int TotalRequiredPeriods { get; set; }
		/// <summary>
		/// Total number of periods actually assigned in the solution
		/// </summary>
		public int AssignedPeriods { get; set; }
		/// <summary>
		/// Number of missing periods (TotalRequiredPeriods - AssignedPeriods)
		/// </summary>
		public int MissingPeriods => TotalRequiredPeriods - AssignedPeriods;
		/// <summary>
		/// Number of subjects (Lop-Mon combinations) that have missing periods
		/// </summary>
		public int MissingSubjectsCount { get; set; }
		/// <summary>
		/// Whether the solution has any missing periods
		/// </summary>
		public bool HasMissingPeriods => MissingPeriods > 0;
		/// <summary>
		/// Whether hard constraints are violated (teacher/class conflicts)
		/// </summary>
		public bool HardConstraintViolated { get; set; }
		/// <summary>
		/// Whether the solution is acceptable (success with or without warnings)
		/// </summary>
		public bool IsAcceptable { get; set; }
	}
}


