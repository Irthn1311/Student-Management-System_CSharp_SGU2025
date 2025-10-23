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

                // Kiểm tra trùng lặp
                if (phanCongDAO.KiemTraTrungLap(phanCong.MaLop, phanCong.MaGiaoVien,
                                                phanCong.MaMonHoc, phanCong.MaHocKy))
                {
                    throw new Exception("Phân công này đã tồn tại trong hệ thống!");
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

                // Kiểm tra trùng lặp (trừ bản ghi hiện tại)
                if (phanCongDAO.KiemTraTrungLap(phanCong.MaLop, phanCong.MaGiaoVien,
                                                phanCong.MaMonHoc, phanCong.MaHocKy,
                                                phanCong.MaPhanCong))
                {
                    throw new Exception("Phân công này đã tồn tại trong hệ thống!");
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
    }
}