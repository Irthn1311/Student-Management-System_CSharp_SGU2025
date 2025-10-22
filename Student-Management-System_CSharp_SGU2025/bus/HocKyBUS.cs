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
                return hocKyDAO.ThemHocKy(hocKy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi thêm học kỳ: {ex.Message}");
            }
        }

        public List<HocKyDTO> DocDSHocKy()
        {
            try
            {
                return hocKyDAO.DocDSHocKy();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi đọc danh sách học kỳ: {ex.Message}");
            }
        }

        public HocKyDTO LayHocKyTheoMa(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                {
                    throw new Exception("Mã học kỳ không hợp lệ!");
                }
                return hocKyDAO.LayHocKyTheoMa(maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy thông tin học kỳ: {ex.Message}");
            }
        }

        public bool CapNhatHocKy(HocKyDTO hocKy)
        {
            try
            {
                return hocKyDAO.CapNhatHocKy(hocKy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi cập nhật học kỳ: {ex.Message}");
            }
        }

        public bool XoaHocKy(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                {
                    throw new Exception("Mã học kỳ không hợp lệ!");
                }

                return hocKyDAO.XoaHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi xóa học kỳ: {ex.Message}");
            }
        }
    }
}