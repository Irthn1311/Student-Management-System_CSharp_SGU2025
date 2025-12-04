using System;
using System.Collections.Generic;
using System.Linq;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.Scheduling;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
	internal class ThoiKhoaBieuBUS
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
			_dao.InsertTemp(semesterId, weekNo, sol);
		}

		public void AcceptTempToOfficial(int semesterId, int weekNo)
		{
			_dao.AcceptTempToOfficial(semesterId, weekNo);
		}

		public List<AssignmentSlot> GetWeek(int semesterId, int weekNo)
		{
			return _dao.GetWeek(semesterId, weekNo);
		}

		public List<AssignmentSlot> GetWeekByClass(int semesterId, int weekNo, int maLop)
		{
			return _dao.GetWeekByClass(semesterId, weekNo, maLop);
		}

		public bool HasScheduleForSemester(int semesterId)
		{
			return _dao.HasScheduleForSemester(semesterId);
		}

		public List<AssignmentSlot> GetOfficialSchedule(int semesterId, int? maLop = null)
		{
			return _dao.GetOfficialSchedule(semesterId, maLop);
		}

		public bool HasConflict(int semesterId, int weekNo, AssignmentSlot slot)
		{
			return _dao.HasConflict(semesterId, weekNo, slot);
		}

		/// <summary>
		/// Lấy danh sách thời khóa biểu theo học kỳ với đầy đủ thông tin
		/// </summary>
		/// <param name="maHocKy">Mã học kỳ</param>
		/// <returns>Danh sách các ô thời khóa biểu</returns>
		public List<TimeTableSlotDTO> GetTKBViewByHocKy(int maHocKy)
		{
			return _dao.GetTKBViewByHocKy(maHocKy);
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
	}
}


