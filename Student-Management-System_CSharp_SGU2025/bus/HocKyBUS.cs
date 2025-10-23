using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class HocKyBUS
    {
        private HocKyDAO hocKyDAO;

        public HocKyBUS()
        {
            hocKyDAO = new HocKyDAO();
        }

        public bool ThemHocKy(HocKyDTO hocKy)
        {
            try
            {
                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrWhiteSpace(hocKy.TenHocKy))
                    throw new ArgumentException("Tên học kỳ không được để trống");

                if (string.IsNullOrWhiteSpace(hocKy.MaNamHoc))
                    throw new ArgumentException("Mã năm học không được để trống");

                if (hocKy.NgayKT <= hocKy.NgayBD)
                    throw new ArgumentException("Ngày kết thúc phải sau ngày bắt đầu");

                return hocKyDAO.ThemHocKy(hocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS ThemHocKy: {ex.Message}");
                throw;
            }
        }

        public List<HocKyDTO> DocDSHocKy()
        {
            try
            {
                List<HocKyDTO> ds = hocKyDAO.DocDSHocKy();

                // Cập nhật trạng thái dựa trên ngày hiện tại
                foreach (var hk in ds)
                {
                    hk.TrangThai = TinhTrangThai(hk.NgayBD, hk.NgayKT);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS DocDSHocKy: {ex.Message}");
                throw;
            }
        }

        public HocKyDTO LayHocKyTheoMa(int maHocKy)
        {
            try
            {
                return hocKyDAO.LayHocKyTheoMa(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS LayHocKyTheoMa: {ex.Message}");
                throw;
            }
        }

        public bool CapNhatHocKy(HocKyDTO hocKy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hocKy.TenHocKy))
                    throw new ArgumentException("Tên học kỳ không được để trống");

                if (hocKy.NgayKT <= hocKy.NgayBD)
                    throw new ArgumentException("Ngày kết thúc phải sau ngày bắt đầu");

                return hocKyDAO.CapNhatHocKy(hocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS CapNhatHocKy: {ex.Message}");
                throw;
            }
        }

        public bool XoaHocKy(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                return hocKyDAO.XoaHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS XoaHocKy: {ex.Message}");
                throw;
            }
        }

        public List<HocKyDTO> LayDanhSachHocKyTheoNamHoc(string maNamHoc)
        {
            try
            {
                return hocKyDAO.LayDanhSachHocKyTheoNamHoc(maNamHoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS LayDanhSachHocKyTheoNamHoc: {ex.Message}");
                throw;
            }
        }

        private string TinhTrangThai(DateTime ngayBD, DateTime ngayKT)
        {
            DateTime now = DateTime.Now.Date;
            DateTime batDau = ngayBD.Date;
            DateTime ketThuc = ngayKT.Date;

            if (now >= batDau && now <= ketThuc)
                return "Đang diễn ra";
            else if (now < batDau)
                return "Chưa bắt đầu";
            else
                return "Đã kết thúc";
        }
    }
}