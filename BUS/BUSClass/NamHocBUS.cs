using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class NamHocBUS
    {
        private NamHocDAO namHocDAO;
        private MonHoc_NamHoc_KhoiBUS monHocNamHocKhoiBUS;

        public NamHocBUS()
        {
            namHocDAO = new NamHocDAO();
            monHocNamHocKhoiBUS = new MonHoc_NamHoc_KhoiBUS();
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

                // Thêm năm học vào database
                bool ketQua = namHocDAO.themNamHoc(namHoc);
                
                // ✅ Tự động copy môn học từ năm học trước sang năm học mới
                if (ketQua)
                {
                    try
                    {
                        // Tìm năm học trước (năm học có NgayKT gần nhất và < NgayBD của năm học mới)
                        var dsNamHoc = namHocDAO.DocDSNamHoc();
                        var namHocTruoc = dsNamHoc
                            .Where(nh => nh.NgayKT < namHoc.NgayBD)
                            .OrderByDescending(nh => nh.NgayKT)
                            .FirstOrDefault();
                        
                        if (namHocTruoc != null)
                        {
                            // Copy tất cả môn học từ năm học trước sang năm học mới
                            monHocNamHocKhoiBUS.CopyMonHocTuNamHocNaySangNamHocKhac(namHocTruoc.MaNamHoc, namHoc.MaNamHoc);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log lỗi nhưng không throw - năm học đã được tạo thành công
                        Console.WriteLine($"Lỗi khi tự động copy môn học: {ex.Message}");
                        // Có thể thêm logging hoặc thông báo cho user nếu cần
                    }
                }

                return ketQua;
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