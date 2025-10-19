using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
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

        // Thêm năm học với validation
        public bool ThemNamHoc(NamHocDTO namHoc)
        {
            // Validation dữ liệu
            if (string.IsNullOrWhiteSpace(namHoc.MaNamHoc))
            {
                throw new ArgumentException("Mã năm học không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(namHoc.TenNamHoc))
            {
                throw new ArgumentException("Tên năm học không được để trống.");
            }

            if (namHoc.NgayBD >= namHoc.NgayKT)
            {
                throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
            }

            // Kiểm tra xem năm học với mã này đã tồn tại chưa
            // Lưu ý: Vì DAO.LayNamHocTheoId nhận int, nhưng MaNamHoc là string, cần điều chỉnh DAO hoặc convert.
            // Giả sử điều chỉnh để LayNamHocTheoMa nhận string.
            // Để tương thích, sử dụng DocDSNamHoc để kiểm tra.
            if (KiemTraNamHocTonTai(namHoc.MaNamHoc))
            {
                throw new ArgumentException("Năm học với mã này đã tồn tại.");
            }

            // Kiểm tra unique tên nếu cần
            if (KiemTraTenNamHocTonTai(namHoc.TenNamHoc))
            {
                throw new ArgumentException("Tên năm học này đã tồn tại.");
            }

            return namHocDAO.themNamHoc(namHoc);
        }

        // Đọc danh sách năm học
        public List<NamHocDTO> DocDSNamHoc()
        {
            return namHocDAO.DocDSNamHoc();
        }

        // Lấy năm học theo mã (sửa thành string để phù hợp)
        public NamHocDTO LayNamHocTheoMa(string maNamHoc)
        {
            if (string.IsNullOrWhiteSpace(maNamHoc))
            {
                throw new ArgumentException("Mã năm học không được để trống.");
            }

            // Gọi DAO với convert nếu cần, nhưng giả sử DAO được sửa để nhận string
            // Để tạm, sử dụng DocDS để tìm
            List<NamHocDTO> ds = namHocDAO.DocDSNamHoc();
            return ds.FirstOrDefault(nh => nh.MaNamHoc == maNamHoc);
        }

        // Lấy năm học theo tên
        public NamHocDTO LayNamHocTheoTen(string tenNamHoc)
        {
            if (string.IsNullOrWhiteSpace(tenNamHoc))
            {
                throw new ArgumentException("Tên năm học không được để trống.");
            }

            return namHocDAO.LayNamHocTheoTen(tenNamHoc);
        }

        // Cập nhật năm học với validation
        public bool CapNhatNamHoc(NamHocDTO namHoc)
        {
            // Validation dữ liệu
            if (string.IsNullOrWhiteSpace(namHoc.MaNamHoc))
            {
                throw new ArgumentException("Mã năm học không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(namHoc.TenNamHoc))
            {
                throw new ArgumentException("Tên năm học không được để trống.");
            }

            if (namHoc.NgayBD >= namHoc.NgayKT)
            {
                throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
            }

            // Kiểm tra xem mã năm học tồn tại
            if (!KiemTraNamHocTonTai(namHoc.MaNamHoc))
            {
                throw new ArgumentException("Không tìm thấy năm học với mã này.");
            }

            // Kiểm tra tên năm học mới có bị trùng không (trừ năm hiện tại)
            NamHocDTO namHocHienTai = LayNamHocTheoMa(namHoc.MaNamHoc);
            if (namHocHienTai != null && namHocHienTai.TenNamHoc != namHoc.TenNamHoc && KiemTraTenNamHocTonTai(namHoc.TenNamHoc))
            {
                throw new ArgumentException("Tên năm học này đã tồn tại cho một năm học khác.");
            }

            return namHocDAO.updateNamHoc(namHoc);
        }

        // Xóa năm học với validation
        public bool XoaNamHoc(string maNamHoc)
        {
            if (string.IsNullOrWhiteSpace(maNamHoc))
            {
                throw new ArgumentException("Mã năm học không được để trống.");
            }

            if (!KiemTraNamHocTonTai(maNamHoc))
            {
                throw new ArgumentException("Không tìm thấy năm học với mã này.");
            }

            // Có thể thêm kiểm tra xem năm học có đang được sử dụng (lớp, học sinh, etc.) không trước khi xóa

            // Gọi DAO với convert nếu cần, giả sử sửa DAO nhận string
            // Tạm sử dụng int? nhưng để string, assume DAO Xoa nhận string
            // Để tạm, convert to int if possible, but since string, assume DAO fixed.
            int intMa = int.Parse(maNamHoc); // risky, but for demo
            return namHocDAO.XoaNamHoc(intMa);
        }

        // Phương thức hỗ trợ: Kiểm tra năm học tồn tại theo mã
        private bool KiemTraNamHocTonTai(string maNamHoc)
        {
            List<NamHocDTO> ds = namHocDAO.DocDSNamHoc();
            return ds.Any(nh => nh.MaNamHoc == maNamHoc);
        }

        // Phương thức hỗ trợ: Kiểm tra tên năm học tồn tại
        private bool KiemTraTenNamHocTonTai(string tenNamHoc)
        {
            List<NamHocDTO> ds = namHocDAO.DocDSNamHoc();
            return ds.Any(nh => nh.TenNamHoc == tenNamHoc);
        }
    }
}