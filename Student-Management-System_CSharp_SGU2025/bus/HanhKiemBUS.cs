using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class HanhKiemBUS
    {
        private HanhKiemDAO hanhKiemDAO = new HanhKiemDAO();
        // Danh sách xếp loại hợp lệ theo yêu cầu
        private readonly List<string> XepLoaiHopLe = new List<string> { "Tốt", "Khá", "TB", "Yếu" };

        // === 1. Hiển thị: Đọc Danh Sách Hạnh Kiểm (Đầy đủ) ===
        public List<HanhKiem> DocDSHanhKiem()
        {
            // Gọi hàm JOIN từ DAO để lấy danh sách đầy đủ (HK + Tên HS + Tên HK)
            return hanhKiemDAO.DocDSHanhKiemDayDu();
        }

        // === 2. CRUD + Validate: Thêm Hạnh Kiểm ===
        public string ThemHanhKiem(HanhKiem hk)
        {
            // Validation 1: Dữ liệu cơ bản
            if (string.IsNullOrEmpty(hk.MaHocSinh) || hk.MaHocKy <= 0)
            {
                return "Mã học sinh và Mã học kỳ không được để trống.";
            }

            // Validation 2: Xếp loại hợp lệ
            if (!XepLoaiHopLe.Contains(hk.XepLoai))
            {
                return "Xếp loại phải là 'Tốt', 'Khá', 'TB', hoặc 'Yếu'.";
            }

            // Logic nghiệp vụ: Kiểm tra xem hạnh kiểm này đã tồn tại chưa
            if (hanhKiemDAO.LayHanhKiemTheoKey(hk.MaHocSinh, hk.MaHocKy) != null)
            {
                return "Hạnh kiểm cho học kỳ này đã tồn tại. Vui lòng dùng chức năng Sửa.";
            }

            if (hanhKiemDAO.ThemHanhKiem(hk))
            {
                return "Thêm hạnh kiểm thành công.";
            }
            return "Thêm hạnh kiểm thất bại.";
        }

        // === 3. CRUD + Validate: Cập nhật Hạnh Kiểm ===
        public string CapNhatHanhKiem(HanhKiem hk)
        {
            // Validation 1: Key
            if (string.IsNullOrEmpty(hk.MaHocSinh) || hk.MaHocKy <= 0)
            {
                return "Mã học sinh và Mã học kỳ không hợp lệ.";
            }
            // Validation 2: Xếp loại hợp lệ
            if (!XepLoaiHopLe.Contains(hk.XepLoai))
            {
                return "Xếp loại phải là 'Tốt', 'Khá', 'TB', hoặc 'Yếu'.";
            }

            if (hanhKiemDAO.CapNhatHanhKiem(hk))
            {
                return "Cập nhật hạnh kiểm thành công.";
            }
            return "Cập nhật hạnh kiểm thất bại.";
        }

        // === 4. CRUD: Xóa Hạnh Kiểm ===
        public string XoaHanhKiem(string maHocSinh, int maHocKy)
        {
            if (hanhKiemDAO.XoaHanhKiem(maHocSinh, maHocKy))
            {
                return "Xóa hạnh kiểm thành công.";
            }
            return "Xóa hạnh kiểm thất bại.";
        }
    }
}