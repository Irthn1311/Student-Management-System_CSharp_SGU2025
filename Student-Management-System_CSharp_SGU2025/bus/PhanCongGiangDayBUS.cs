using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
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
        /// Hiện tại dùng trường SoTiet từ MonHoc như mặc định (có thể mở rộng per-class sau).
        /// </summary>
        public int GetRequiredPeriods(int maLop, int maMon, int semesterId)
        {
            var monHocDAO = new Student_Management_System_CSharp_SGU2025.DAO.MonHocDAO();
            var mh = monHocDAO.LayDSMonHocTheoId(maMon);
            return mh != null ? mh.soTiet : 0;
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

                // Kiểm tra trùng lặp (Lớp - Môn - Học kỳ)
                if (phanCongDAO.KiemTraTrungLap(phanCong.MaLop, phanCong.MaGiaoVien,
                                                phanCong.MaMonHoc, phanCong.MaHocKy))
                {
                    throw new Exception("Phân công này đã tồn tại trong hệ thống (trùng Lớp-Môn-Học kỳ)!");
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

                // Kiểm tra trùng lặp (trừ bản ghi hiện tại)
                if (phanCongDAO.KiemTraTrungLap(phanCong.MaLop, phanCong.MaGiaoVien,
                                                phanCong.MaMonHoc, phanCong.MaHocKy,
                                                phanCong.MaPhanCong))
                {
                    throw new Exception("Phân công này đã tồn tại trong hệ thống (trùng Lớp-Môn-Học kỳ)!");
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
    }
}