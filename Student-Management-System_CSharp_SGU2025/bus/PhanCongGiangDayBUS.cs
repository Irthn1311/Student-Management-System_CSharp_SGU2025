using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.Services;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    /// <summary>
    /// Validation result for assignment pre-validation
    /// </summary>
    public class AssignmentValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();
    }

    internal class PhanCongGiangDayBUS
    {
        private PhanCongGiangDayDAO phanCongDAO;

        public PhanCongGiangDayBUS()
        {
            phanCongDAO = new PhanCongGiangDayDAO();
        }

        /// <summary>
        /// Lấy danh sách phân công theo học kỳ (để sinh thời khóa biểu).
        /// </summary>
        public List<PhanCongGiangDayDTO> GetBySemester(int semesterId)
        {
            return phanCongDAO.LayPhanCongTheoHocKy(semesterId);
        }

        /// <summary>
        /// Trả về số tiết/tuần yêu cầu của một (Lớp, Môn) theo cấu hình môn học.
        /// Giả định SoTiet trong MonHoc là số tiết/học kỳ.
        /// Phân bổ linh hoạt: số tiết có thể khác nhau giữa các tuần, miễn là tổng đủ trong kỳ.
        /// </summary>
        /// <param name="maLop">Mã lớp</param>
        /// <param name="maMon">Mã môn</param>
        /// <param name="semesterId">Mã học kỳ</param>
        /// <param name="weekNo">Số thứ tự tuần (1-based). Nếu không cung cấp, trả về số tiết trung bình.</param>
        public int GetRequiredPeriods(int maLop, int maMon, int semesterId, int? weekNo = null)
        {
            var monHocDAO = new Student_Management_System_CSharp_SGU2025.DAO.MonHocDAO();
            var mh = monHocDAO.LayDSMonHocTheoId(maMon);
            if (mh == null) return 0;

            int soTietHocKy = mh.soTiet; // Số tiết/học kỳ
            
            // Tính số tuần trong học kỳ
            var hocKyBUS = new HocKyBUS();
            var hocKy = hocKyBUS.LayHocKyTheoMa(semesterId);
            
            int weeksInSemester = 18; // Default: 18 tuần/học kỳ
            
            if (hocKy != null && hocKy.NgayBD.HasValue && hocKy.NgayKT.HasValue)
            {
                var days = (hocKy.NgayKT.Value - hocKy.NgayBD.Value).Days;
                weeksInSemester = Math.Max(1, days / 7); // Số tuần trong học kỳ
            }
            
            // Nếu không có weekNo, trả về số tiết trung bình (làm tròn lên)
            if (!weekNo.HasValue)
            {
                int periodsPerWeek = (int)Math.Ceiling((double)soTietHocKy / weeksInSemester);
                return Math.Min(periodsPerWeek, 10);
            }
            
            // Phân bổ số tiết linh hoạt cho từng tuần
            // Mục tiêu: Tổng số tiết trong kỳ = soTietHocKy, chênh lệch giữa các tuần ≤ 1
            // Chiến lược: Phân bổ đều nhất có thể, ưu tiên các tuần giữa kỳ
            int basePeriodsPerWeek = soTietHocKy / weeksInSemester; // Số tiết cơ bản mỗi tuần
            int remainder = soTietHocKy % weeksInSemester; // Số tiết dư ra cần phân bổ
            
            int weekIndex = weekNo.Value - 1; // 0-based
            int periodsThisWeek = basePeriodsPerWeek;
            
            if (remainder > 0)
            {
                // Phân bổ số tiết dư đều cho các tuần
                // Chiến lược: Phân bổ từ đầu để đảm bảo tuần đầu cũng có đủ tiết
                // Ví dụ: 35 tiết / 18 tuần = 1 tiết/tuần + 17 tiết dư
                // → 17 tuần đầu: 2 tiết, 1 tuần cuối: 1 tiết
                
                // Phân bổ từ đầu: remainder tuần đầu sẽ có thêm 1 tiết
                if (weekIndex < remainder)
                {
                    periodsThisWeek += 1; // Thêm 1 tiết cho tuần này
                }
            }
            
            // Đảm bảo ít nhất 1 tiết/tuần nếu môn có tiết trong kỳ
            if (soTietHocKy > 0 && periodsThisWeek == 0)
            {
                periodsThisWeek = 1;
            }
            
            // Giới hạn tối đa 10 tiết/tuần (hợp lý cho một môn)
            int result = Math.Min(periodsThisWeek, 10);
            
            // Debug logging để kiểm tra
            if (weekNo.HasValue && weekNo.Value == 1)
            {
                System.Diagnostics.Debug.WriteLine($"GetRequiredPeriods: Lớp {maLop}, Môn {maMon}, Tuần {weekNo.Value}: {soTietHocKy} tiết/kỳ / {weeksInSemester} tuần = {basePeriodsPerWeek} base + {remainder} dư → {result} tiết/tuần");
            }
            
            return result;
        }

        // Thêm phân công giảng dạy
        public bool ThemPhanCong(PhanCongGiangDayDTO phanCong)
        {
            try
            {
                // Validate dữ liệu
                if (phanCong.MaLop <= 0)
                    throw new ArgumentException("Mã lớp không hợp lệ");

                if (string.IsNullOrWhiteSpace(phanCong.MaGiaoVien))
                    throw new ArgumentException("Mã giáo viên không được để trống");

                if (phanCong.MaMonHoc <= 0)
                    throw new ArgumentException("Mã môn học không hợp lệ");

                if (phanCong.MaHocKy <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                if (phanCong.NgayKetThuc <= phanCong.NgayBatDau)
                    throw new ArgumentException("Ngày kết thúc phải sau ngày bắt đầu");

                // Kiểm tra giáo viên có chuyên môn phù hợp
                if (!phanCongDAO.KiemTraGiaoVienChuyenMon(phanCong.MaGiaoVien, phanCong.MaMonHoc))
                {
                    throw new Exception("Giáo viên không có chuyên môn phù hợp để dạy môn học này!");
                }

                // ✅ FIX: Kiểm tra trùng lặp môn học cho lớp trong học kỳ (không phân biệt giáo viên)
                if (phanCongDAO.KiemTraTrungLapMonHoc(phanCong.MaLop, phanCong.MaMonHoc, phanCong.MaHocKy))
                {
                    throw new Exception("Môn học này đã được phân công cho lớp trong học kỳ này!");
                }

                return phanCongDAO.ThemPhanCong(phanCong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm phân công: {ex.Message}", ex);
            }
        }

        // Đọc danh sách phân công
        public List<PhanCongGiangDayDTO> DocDSPhanCong()
        {
            try
            {
                return phanCongDAO.DocDSPhanCong();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đọc danh sách phân công: {ex.Message}", ex);
            }
        }

        // Lấy phân công theo mã
        public PhanCongGiangDayDTO LayPhanCongTheoMa(int maPhanCong)
        {
            try
            {
                if (maPhanCong <= 0)
                    throw new ArgumentException("Mã phân công không hợp lệ");

                return phanCongDAO.LayPhanCongTheoMa(maPhanCong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy phân công: {ex.Message}", ex);
            }
        }

        // Lấy danh sách phân công theo lớp
        public List<PhanCongGiangDayDTO> LayPhanCongTheoLop(int maLop)
        {
            try
            {
                if (maLop <= 0)
                    throw new ArgumentException("Mã lớp không hợp lệ");

                return phanCongDAO.LayPhanCongTheoLop(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy phân công theo lớp: {ex.Message}", ex);
            }
        }

        // Lấy danh sách phân công theo giáo viên
        public List<PhanCongGiangDayDTO> LayPhanCongTheoGiaoVien(string maGiaoVien)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maGiaoVien))
                    throw new ArgumentException("Mã giáo viên không hợp lệ");

                return phanCongDAO.LayPhanCongTheoGiaoVien(maGiaoVien);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy phân công theo giáo viên: {ex.Message}", ex);
            }
        }

        // Lấy danh sách phân công theo học kỳ
        public List<PhanCongGiangDayDTO> LayPhanCongTheoHocKy(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                return phanCongDAO.LayPhanCongTheoHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy phân công theo học kỳ: {ex.Message}", ex);
            }
        }

        // Cập nhật phân công
        public bool CapNhatPhanCong(PhanCongGiangDayDTO phanCong)
        {
            try
            {
                // Validate dữ liệu
                if (phanCong.MaPhanCong <= 0)
                    throw new ArgumentException("Mã phân công không hợp lệ");

                if (phanCong.MaLop <= 0)
                    throw new ArgumentException("Mã lớp không hợp lệ");

                if (string.IsNullOrWhiteSpace(phanCong.MaGiaoVien))
                    throw new ArgumentException("Mã giáo viên không được để trống");

                if (phanCong.MaMonHoc <= 0)
                    throw new ArgumentException("Mã môn học không hợp lệ");

                if (phanCong.MaHocKy <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                if (phanCong.NgayKetThuc <= phanCong.NgayBatDau)
                    throw new ArgumentException("Ngày kết thúc phải sau ngày bắt đầu");

                // Kiểm tra giáo viên có chuyên môn phù hợp
                if (!phanCongDAO.KiemTraGiaoVienChuyenMon(phanCong.MaGiaoVien, phanCong.MaMonHoc))
                {
                    throw new Exception("Giáo viên không có chuyên môn phù hợp để dạy môn học này!");
                }

                // ✅ FIX: Kiểm tra trùng lặp môn học cho lớp trong học kỳ (trừ bản ghi hiện tại)
                if (phanCongDAO.KiemTraTrungLapMonHoc(phanCong.MaLop, phanCong.MaMonHoc, phanCong.MaHocKy, phanCong.MaPhanCong))
                {
                    throw new Exception("Môn học này đã được phân công cho lớp trong học kỳ này!");
                }

                return phanCongDAO.CapNhatPhanCong(phanCong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật phân công: {ex.Message}", ex);
            }
        }

        // Xóa phân công
        public bool XoaPhanCong(int maPhanCong)
        {
            try
            {
                if (maPhanCong <= 0)
                    throw new ArgumentException("Mã phân công không hợp lệ");

                // Kiểm tra xem phân công có tồn tại không
                var phanCong = phanCongDAO.LayPhanCongTheoMa(maPhanCong);
                if (phanCong == null)
                    throw new Exception("Phân công không tồn tại trong hệ thống");

                return phanCongDAO.XoaPhanCong(maPhanCong);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa phân công: {ex.Message}", ex);
            }
        }

        // Kiểm tra phân công đã tồn tại
        public bool KiemTraPhanCongTonTai(int maLop, string maGiaoVien, int maMonHoc, int maHocKy)
        {
            try
            {
                return phanCongDAO.KiemTraTrungLap(maLop, maGiaoVien, maMonHoc, maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra phân công: {ex.Message}", ex);
            }
        }

        // Kiểm tra giáo viên có chuyên môn phù hợp
        public bool KiemTraGiaoVienChuyenMon(string maGiaoVien, int maMonHoc)
        {
            try
            {
                return phanCongDAO.KiemTraGiaoVienChuyenMon(maGiaoVien, maMonHoc);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra chuyên môn: {ex.Message}", ex);
            }
        }

        // Kiểm tra học kỳ có cho phép chỉnh sửa (không phải quá khứ)
        public bool KiemTraHocKyChoPhepChinhSua(int maHocKy)
        {
            try
            {
                var hocKyDAO = new HocKyDAO();
                var hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
                
                if (hocKy == null || !hocKy.NgayKT.HasValue)
                    return false;

                // Nếu ngày kết thúc < ngày hiện tại => quá khứ => không cho sửa
                return hocKy.NgayKT.Value.Date >= DateTime.Now.Date;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra học kỳ: {ex.Message}", ex);
            }
        }

        // Lọc phân công theo nhiều tiêu chí
        public List<PhanCongGiangDayDTO> LocPhanCong(int? maHocKy = null, int? maLop = null, 
                                                      int? maMonHoc = null, string maGiaoVien = null)
        {
            try
            {
                List<PhanCongGiangDayDTO> ds = DocDSPhanCong();

                if (maHocKy.HasValue)
                    ds = ds.FindAll(pc => pc.MaHocKy == maHocKy.Value);

                if (maLop.HasValue)
                    ds = ds.FindAll(pc => pc.MaLop == maLop.Value);

                if (maMonHoc.HasValue)
                    ds = ds.FindAll(pc => pc.MaMonHoc == maMonHoc.Value);

                if (!string.IsNullOrWhiteSpace(maGiaoVien))
                    ds = ds.FindAll(pc => pc.MaGiaoVien == maGiaoVien);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lọc phân công: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Validate assignment data without inserting (for UI pre-validation)
        /// </summary>
        /// <param name="phanCong">Assignment data to validate</param>
        /// <param name="isUpdate">True if validating for update, false for insert</param>
        /// <returns>AssignmentValidationResult with IsValid flag and list of errors/warnings</returns>
        public AssignmentValidationResult ValidateAssignment(PhanCongGiangDayDTO phanCong, bool isUpdate = false)
        {
            var result = new AssignmentValidationResult { IsValid = true };

            try
            {
                // Input validation
                if (phanCong.MaLop <= 0)
                {
                    result.IsValid = false;
                    result.Errors.Add("Mã lớp không hợp lệ");
                }

                if (string.IsNullOrWhiteSpace(phanCong.MaGiaoVien))
                {
                    result.IsValid = false;
                    result.Errors.Add("Mã giáo viên không được để trống");
                }

                if (phanCong.MaMonHoc <= 0)
                {
                    result.IsValid = false;
                    result.Errors.Add("Mã môn học không hợp lệ");
                }

                if (phanCong.MaHocKy <= 0)
                {
                    result.IsValid = false;
                    result.Errors.Add("Mã học kỳ không hợp lệ");
                }

                if (phanCong.NgayKetThuc <= phanCong.NgayBatDau)
                {
                    result.IsValid = false;
                    result.Errors.Add("Ngày kết thúc phải sau ngày bắt đầu");
                }

                if (isUpdate && phanCong.MaPhanCong <= 0)
                {
                    result.IsValid = false;
                    result.Errors.Add("Mã phân công không hợp lệ");
                }

                // Business rule validation (only if basic validation passes)
                if (result.IsValid)
                {
                    // Check teacher expertise
                    if (!phanCongDAO.KiemTraGiaoVienChuyenMon(phanCong.MaGiaoVien, phanCong.MaMonHoc))
                    {
                        result.IsValid = false;
                        result.Errors.Add("Giáo viên không có chuyên môn phù hợp để dạy môn học này!");
                    }

                    // ✅ CRITICAL: Check for duplicate subject-class-semester (regardless of teacher)
                    if (isUpdate)
                    {
                        if (phanCongDAO.KiemTraTrungLapMonHoc(phanCong.MaLop, phanCong.MaMonHoc, phanCong.MaHocKy, phanCong.MaPhanCong))
                        {
                            result.IsValid = false;
                            result.Errors.Add("Môn học này đã được phân công cho lớp trong học kỳ này!");
                        }
                    }
                    else
                    {
                        if (phanCongDAO.KiemTraTrungLapMonHoc(phanCong.MaLop, phanCong.MaMonHoc, phanCong.MaHocKy))
                        {
                            result.IsValid = false;
                            result.Errors.Add("Môn học này đã được phân công cho lớp trong học kỳ này!");
                        }
                    }

                    // Check if semester allows editing
                    if (!KiemTraHocKyChoPhepChinhSua(phanCong.MaHocKy))
                    {
                        result.Warnings.Add("Học kỳ này đã kết thúc. Việc chỉnh sửa có thể không được khuyến nghị.");
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add($"Lỗi khi kiểm tra dữ liệu: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Generate auto assignments with error handling
        /// </summary>
        /// <param name="hocKyId">Semester ID</param>
        /// <param name="policy">Assignment policy</param>
        /// <returns>AutoAssignResult with candidates and validation report</returns>
        public AutoAssignResult GenerateAutoAssignments(int hocKyId, AssignmentPolicy policy)
        {
            try
            {
                // Validate input
                if (hocKyId <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                if (policy == null)
                    throw new ArgumentNullException(nameof(policy), "Chính sách phân công không được để trống");

                var service = new AssignmentAutoService();
                var result = service.GenerateAutoAssignments(hocKyId, policy);

                // Log warnings if any
                if (result.Report.HardViolations > 0)
                {
                    Console.WriteLine($"⚠️ Auto-Assign có {result.Report.HardViolations} vi phạm:");
                    foreach (var msg in result.Report.Messages)
                    {
                        Console.WriteLine($"  - {msg}");
                    }
                }

                // Log if semester is read-only
                if (result.IsReadOnly)
                {
                    Console.WriteLine($"⚠️ Học kỳ {hocKyId} đã kết thúc ({result.SemesterStatus}). Không thể tạo phân công mới.");
                }

                return result;
            }
            catch (ArgumentException ex)
            {
                // Re-throw argument exceptions as-is
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi sinh phân công tự động: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Kiểm tra học kỳ có phân công chính thức không
        /// </summary>
        public bool HasAssignmentsForSemester(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                    return false;
                return phanCongDAO.HasAssignmentsForSemester(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra phân công học kỳ: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra học kỳ có phân công tạm không
        /// </summary>
        public bool HasTempAssignmentsForSemester(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                    return false;
                return phanCongDAO.HasTempAssignmentsForSemester(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra phân công tạm học kỳ: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Generate auto assignments with filters (by grade and/or subject)
        /// </summary>
        /// <param name="hocKyId">Semester ID</param>
        /// <param name="policy">Assignment policy</param>
        /// <param name="khoi">Grade filter (optional)</param>
        /// <param name="maMonFilter">Subject filter (optional)</param>
        /// <returns>AutoAssignResult with candidates and validation report</returns>
        public AutoAssignResult GenerateAutoAssignmentsFiltered(int hocKyId, AssignmentPolicy policy, int? khoi = null, string maMonFilter = null)
        {
            try
            {
                // Validate input
                if (hocKyId <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                if (policy == null)
                    throw new ArgumentNullException(nameof(policy), "Chính sách phân công không được để trống");

                var service = new AssignmentAutoService();
                var result = service.GenerateAutoAssignmentsFiltered(hocKyId, policy, khoi, maMonFilter);

                // Log warnings if any
                if (result.Report.HardViolations > 0)
                {
                    Console.WriteLine($"⚠️ Auto-Assign có {result.Report.HardViolations} vi phạm:");
                    foreach (var msg in result.Report.Messages)
                    {
                        Console.WriteLine($"  - {msg}");
                    }
                }

                // Log if semester is read-only
                if (result.IsReadOnly)
                {
                    Console.WriteLine($"⚠️ Học kỳ {hocKyId} đã kết thúc ({result.SemesterStatus}). Không thể tạo phân công mới.");
                }

                return result;
            }
            catch (ArgumentException ex)
            {
                // Re-throw argument exceptions as-is
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi sinh phân công tự động (có lọc): {ex.Message}", ex);
            }
        }
    }
}