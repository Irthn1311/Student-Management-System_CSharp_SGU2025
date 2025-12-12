using System;
using System.Collections.Generic;
using System.Linq;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Scheduling;
using AssignmentSlot = Student_Management_System_CSharp_SGU2025.DTO.AssignmentSlotDTO;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
	public class ThoiKhoaBieuBUS
	{
		private readonly ThoiKhoaBieuDAO _dao;
		private readonly PhanCongGiangDayDAO _phanCongDAO;
		private readonly LopDAO _lopDAO;
		private readonly GiaoVienDAO _giaoVienDAO;

		public ThoiKhoaBieuBUS()
		{
			_dao = new ThoiKhoaBieuDAO();
			_phanCongDAO = new PhanCongGiangDayDAO();
			_lopDAO = new LopDAO();
			_giaoVienDAO = new GiaoVienDAO();
		}

		public void ClearTemp()
		{
			_dao.ClearTemp();
		}

		public void InsertTemp(int semesterId, int weekNo, ScheduleSolution sol)
		{
			_dao.InsertTemp(semesterId, weekNo, sol.Slots);
		}

		public void AcceptTempToOfficial(int semesterId, int weekNo)
		{
			_dao.AcceptTempToOfficial(semesterId, weekNo);
		}

		/// <summary>
		/// ✅ Chấp nhận tất cả các tuần của học kỳ từ TKB_Temp vào ThoiKhoaBieu chính thức
		/// </summary>
		public void AcceptAllWeeksForSemester(int semesterId)
		{
			_dao.AcceptAllWeeksForSemester(semesterId);
		}

		public List<AssignmentSlot> GetWeek(int semesterId, int weekNo)
		{
			// AssignmentSlot is a type alias for AssignmentSlotDTO, so we can return directly
			return _dao.GetWeek(semesterId, weekNo).Select(s => (AssignmentSlot)s).ToList();
		}

		public List<AssignmentSlot> GetWeekByClass(int semesterId, int weekNo, int maLop)
		{
			return _dao.GetWeekByClass(semesterId, weekNo, maLop).Select(s => (AssignmentSlot)s).ToList();
		}

		public bool HasScheduleForSemester(int semesterId)
		{
			return _dao.HasScheduleForSemester(semesterId);
		}

		public List<AssignmentSlot> GetOfficialSchedule(int semesterId, int? maLop = null)
		{
			return _dao.GetOfficialSchedule(semesterId, maLop).Select(s => (AssignmentSlot)s).ToList();
		}

		public bool HasConflict(int semesterId, int weekNo, AssignmentSlot slot)
		{
			return _dao.HasConflict(semesterId, weekNo, (AssignmentSlotDTO)slot);
		}

		/// <summary>
		/// Lấy danh sách thời khóa biểu theo học kỳ với đầy đủ thông tin
		/// Tự động thêm Chào Cờ và SHL (môn ngoài bắt buộc, không có trong database)
		/// </summary>
		/// <param name="maHocKy">Mã học kỳ</param>
		/// <returns>Danh sách các ô thời khóa biểu</returns>
		public List<TimeTableSlotDTO> GetTKBViewByHocKy(int maHocKy)
		{
			var result = _dao.GetTKBViewByHocKy(maHocKy);
			
			// ✅ Tự động thêm Chào Cờ và SHL cho tất cả lớp
			AddChaoCoAndSHL(result, maHocKy);
			
			return result;
		}
		
		/// <summary>
		/// Thêm Chào Cờ (tiết 1 thứ 2) và SHL (tiết cuối buổi chính thứ 6) cho tất cả lớp
		/// </summary>
		private void AddChaoCoAndSHL(List<TimeTableSlotDTO> slots, int maHocKy)
		{
			// Lấy danh sách tất cả lớp có phân công trong học kỳ này
			var allPhanCong = _phanCongDAO.LayPhanCongTheoHocKy(maHocKy);
			var allLopIds = allPhanCong?.Select(pc => pc.MaLop).Distinct().ToList() ?? new List<int>();
			
			// Nếu không có phân công, lấy từ danh sách lớp có trong slots
			if (allLopIds.Count == 0)
			{
				allLopIds = slots.Select(s => s.MaLop).Distinct().ToList();
			}
			
			foreach (var maLop in allLopIds)
			{
				// Kiểm tra xem đã có Chào Cờ chưa
				bool hasChaoCo = slots.Any(s => s.MaLop == maLop && s.Thu == 2 && s.Tiet == 1);
				if (!hasChaoCo)
				{
					// Lấy tên lớp
					var lop = _lopDAO.LayLopTheoId(maLop);
					string tenLop = lop != null ? lop.tenLop : $"Lớp {maLop}";
					
					slots.Add(new TimeTableSlotDTO
					{
						MaThoiKhoaBieu = 0, // Không có trong database
						MaPhanCong = 0, // Không có phân công
						Thu = 2, // Thứ 2
						Tiet = 1, // Tiết 1 buổi sáng
						TenLop = tenLop,
						TenMon = "Chào cờ",
						TenGiaoVien = "", // Không có giáo viên
						MaGiaoVien = "",
						MaLop = maLop
					});
				}
				
				// Kiểm tra xem đã có SHL chưa
				// Xác định khối để biết buổi chính
				var lopForKhoi = _lopDAO.LayLopTheoId(maLop);
				int khoi = lopForKhoi != null ? lopForKhoi.MaKhoi : 10;
				bool isMainSessionMorning = (khoi == 11 || khoi == 12);
				int tietSHL = isMainSessionMorning ? 5 : 10; // Tiết cuối buổi chính
				
				bool hasSHL = slots.Any(s => s.MaLop == maLop && s.Thu == 6 && s.Tiet == tietSHL);
				if (!hasSHL)
				{
					string tenLop = lopForKhoi != null ? lopForKhoi.tenLop : $"Lớp {maLop}";
					
					slots.Add(new TimeTableSlotDTO
					{
						MaThoiKhoaBieu = 0, // Không có trong database
						MaPhanCong = 0, // Không có phân công
						Thu = 6, // Thứ 6
						Tiet = tietSHL, // Tiết cuối buổi chính
						TenLop = tenLop,
						TenMon = "Sinh hoạt lớp",
						TenGiaoVien = "", // Không có giáo viên
						MaGiaoVien = "",
						MaLop = maLop
					});
				}
			}
		}

		/// <summary>
		/// Kiểm tra và xác thực việc di chuyển/chỉnh sửa thời khóa biểu
		/// Logic-based validation: Sử dụng SQL JOIN để kiểm tra xung đột
		/// </summary>
		/// <param name="maPhanCong">Mã phân công cần di chuyển</param>
		/// <param name="thuMoi">Thứ mới (2-6)</param>
		/// <param name="tietMoi">Tiết mới (1-10)</param>
		/// <param name="currentTkbId">Mã thời khóa biểu hiện tại (0 nếu là thêm mới)</param>
		/// <returns>MoveResult chứa kết quả validation</returns>
		public MoveResult ValidateAndMove(int maPhanCong, int thuMoi, int tietMoi, int currentTkbId = 0)
		{
			try
			{
				// a. Lấy thông tin phân công (MaLop, MaGV)
				var phanCong = _phanCongDAO.LayPhanCongTheoMa(maPhanCong);
				if (phanCong == null)
				{
					return MoveResult.Fail("Không tìm thấy phân công giảng dạy!");
				}

				int maLop = phanCong.MaLop;
				string maGV = phanCong.MaGiaoVien;

				// Lấy tên lớp và tên giáo viên để hiển thị trong thông báo lỗi
				var lop = _lopDAO.LayLopTheoId(maLop);
				string tenLop = lop != null ? lop.tenLop : $"Lớp {maLop}";

				var giaoVien = _giaoVienDAO.LayGiaoVienTheoMa(maGV);
				string tenGV = giaoVien != null ? giaoVien.HoTen : maGV;

				// b. Kiểm tra lớp có bận không
				if (_dao.CheckClassBusy(maLop, thuMoi, tietMoi, currentTkbId))
				{
					return MoveResult.Fail($"Lớp {tenLop} đã có tiết học khác vào Thứ {thuMoi}, Tiết {tietMoi}.");
				}

				// c. Kiểm tra giáo viên có bận không
				if (_dao.CheckTeacherBusy(maGV, thuMoi, tietMoi, currentTkbId))
				{
					return MoveResult.Fail($"Giáo viên {tenGV} đang dạy lớp khác vào tiết này.");
				}

				// d. Cả hai kiểm tra đều pass
				return MoveResult.Success("Vị trí này hợp lệ, có thể di chuyển.");
			}
			catch (Exception ex)
			{
				return MoveResult.Fail($"Lỗi khi kiểm tra: {ex.Message}");
			}
		}

		/// <summary>
		/// Lưu thay đổi thời khóa biểu (thêm mới hoặc cập nhật)
		/// </summary>
		/// <param name="slot">Đối tượng TimeTableSlotDTO chứa thông tin cần lưu</param>
		/// <returns>True nếu lưu thành công, False nếu thất bại</returns>
		public bool SaveTimetableChange(TimeTableSlotDTO slot)
		{
			if (slot == null)
				throw new ArgumentNullException(nameof(slot), "Dữ liệu thời khóa biểu không được để trống");

			try
			{
				// Nếu MaThoiKhoaBieu > 0: Đây là cập nhật
				// Nếu MaThoiKhoaBieu = 0: Đây là thêm mới
				if (slot.MaThoiKhoaBieu > 0)
				{
					// Cập nhật bản ghi hiện có
					return _dao.UpdateTKB(
						slot.MaThoiKhoaBieu,
						slot.Thu,
						slot.Tiet,
						1, // SoTiet mặc định là 1
						null // PhongHoc có thể null
					);
				}
				else
				{
					// Thêm mới bản ghi
					int newId = _dao.InsertTKB(
						slot.MaPhanCong,
						slot.Thu,
						slot.Tiet,
						1, // SoTiet mặc định là 1
						null // PhongHoc có thể null
					);
					
					// Cập nhật MaThoiKhoaBieu vào slot
					if (newId > 0)
					{
						slot.MaThoiKhoaBieu = newId;
						return true;
					}
					return false;
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Lỗi khi lưu thời khóa biểu: {ex.Message}", ex);
			}
		}

		/// <summary>
		/// Lấy thời khóa biểu theo giáo viên cho học kỳ cụ thể.
		/// </summary>
		public List<TimeTableSlotDTO> GetTKBByTeacher(int maHocKy, string maGiaoVien)
		{
			return _dao.GetTKBByTeacher(maHocKy, maGiaoVien);
		}

		/// <summary>
		/// Kiểm tra xem học kỳ có temp schedule chưa.
		/// </summary>
		public bool HasTempScheduleForSemester(int semesterId)
		{
			return _dao.HasTempScheduleForSemester(semesterId);
		}

		/// <summary>
		/// Xóa temp schedule cho học kỳ và tuần cụ thể.
		/// </summary>
		public void ClearTempForSemester(int semesterId, int weekNo)
		{
			_dao.ClearTempForSemester(semesterId, weekNo);
		}

		/// <summary>
		/// Lấy mã lớp chủ nhiệm của giáo viên.
		/// </summary>
		public int? GetHomeroomClassIdForTeacher(string maGiaoVien)
		{
			if (string.IsNullOrWhiteSpace(maGiaoVien))
				return null;

			var allClasses = _lopDAO.DocDSLop();
			var homeroomClass = allClasses.FirstOrDefault(l => !string.IsNullOrEmpty(l.maGVCN) && l.maGVCN.Equals(maGiaoVien, StringComparison.OrdinalIgnoreCase));
			return homeroomClass?.maLop;
		}

		/// <summary>
		/// Xóa thời khóa biểu theo danh sách phân công (dùng khi đổi chuyên môn giáo viên)
		/// </summary>
		/// <param name="danhSachMaPhanCong">Danh sách mã phân công cần xóa thời khóa biểu</param>
		public void XoaThoiKhoaBieuTheoPhanCong(List<int> danhSachMaPhanCong)
		{
			if (danhSachMaPhanCong == null || danhSachMaPhanCong.Count == 0)
				return;

			_dao.XoaThoiKhoaBieuTheoPhanCong(danhSachMaPhanCong);
		}
	}
}


