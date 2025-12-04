using Student_Management_System_CSharp_SGU2025.Scheduling;

namespace Student_Management_System_CSharp_SGU2025.Config
{
	/// <summary>
	/// Root configuration class for timetable module.
	/// Maps to timetable_config.json structure.
	/// </summary>
	public class TimetableConfigRoot
	{
		// Cấu hình thời gian / khung tiết
		public SlotsConfig CauHinhTietHoc { get; set; } = new SlotsConfig();

		// Cấu hình trọng số cho các ràng buộc mềm
		public WeightConfig CauHinhTrongSo { get; set; } = new WeightConfig();

		// Tham số mặc định cho thuật toán
		public AlgorithmDefaults ThamSoThuatToan { get; set; } = new AlgorithmDefaults();
	}

	/// <summary>
	/// Default values for algorithm parameters (can be overridden by UI).
	/// </summary>
	public class AlgorithmDefaults
	{
		public int SoVongLapToiDa { get; set; } = 5000;
		public int DoDaiTabu { get; set; } = 9;
		public int ThoiGianChayToiDaGiay { get; set; } = 90;
		public int GioiHanKhongCaiThien { get; set; } = 500;
	}
}

