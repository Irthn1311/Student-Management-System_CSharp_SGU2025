using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class NamHocBUS
    {
        private NamHocDAO namHocDAO;

        public NamHocBUS()
        {
            namHocDAO = new NamHocDAO();
        }

        public bool ThemNamHoc(NamHocDTO namHoc)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(namHoc.MaNamHoc))
                    throw new ArgumentException("Mã năm học không được để trống.");

                if (string.IsNullOrWhiteSpace(namHoc.TenNamHoc))
                    throw new ArgumentException("Tên năm học không được để trống.");

                if (namHoc.NgayBD.Date >= namHoc.NgayKT.Date)
                    throw new ArgumentException("Ngày bắt đầu phải trước ngày kết thúc.");

                // Kiểm tra trùng mã
                if (KiemTraNamHocTonTai(namHoc.MaNamHoc))
                    throw new ArgumentException("Mã năm học đã tồn tại.");

                return namHocDAO.themNamHoc(namHoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS ThemNamHoc: {ex.Message}");
                throw;
            }
        }

        public List<NamHocDTO> DocDSNamHoc()
        {
            try
            {
                return namHocDAO.DocDSNamHoc();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS DocDSNamHoc: {ex.Message}");
                throw;
            }
        }

        public NamHocDTO LayNamHocTheoMa(string maNamHoc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maNamHoc))
                    throw new ArgumentException("Mã năm học không được để trống.");

                return namHocDAO.LayNamHocTheoMa(maNamHoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS LayNamHocTheoMa: {ex.Message}");
                throw;
            }
        }

        public bool CapNhatNamHoc(NamHocDTO namHoc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(namHoc.MaNamHoc))
                    throw new ArgumentException("Mã năm học không được để trống.");

                if (string.IsNullOrWhiteSpace(namHoc.TenNamHoc))
                    throw new ArgumentException("Tên năm học không được để trống.");

                if (namHoc.NgayBD.Date >= namHoc.NgayKT.Date)
                    throw new ArgumentException("Ngày bắt đầu phải trước ngày kết thúc.");

                if (!KiemTraNamHocTonTai(namHoc.MaNamHoc))
                    throw new ArgumentException("Không tìm thấy năm học.");

                return namHocDAO.updateNamHoc(namHoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS CapNhatNamHoc: {ex.Message}");
                throw;
            }
        }

        public bool XoaNamHoc(string maNamHoc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maNamHoc))
                    throw new ArgumentException("Mã năm học không được để trống.");

                if (!KiemTraNamHocTonTai(maNamHoc))
                    throw new ArgumentException("Không tìm thấy năm học cần xóa.");

                // TODO: Kiểm tra xem năm học có đang được sử dụng không
                // Ví dụ: Kiểm tra có học kỳ, lớp học phần, điểm liên quan không

                return namHocDAO.XoaNamHoc(maNamHoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS XoaNamHoc: {ex.Message}");
                throw;
            }
        }

        private bool KiemTraNamHocTonTai(string maNamHoc)
        {
            try
            {
                List<NamHocDTO> ds = namHocDAO.DocDSNamHoc();
                return ds != null && ds.Any(nh => nh.MaNamHoc == maNamHoc);
            }
            catch
            {
                return false;
            }
        }
    }
}