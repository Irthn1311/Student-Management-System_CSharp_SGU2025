using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class HanhKiemBUS
    {
        private HanhKiemDAO hanhKiemDAO = new HanhKiemDAO();
        // Sửa "TB" thành "Trung bình"
        private readonly List<string> XepLoaiHopLe = new List<string> { "Tốt", "Khá", "Trung bình", "Yếu" };

        // === 1. Hiển thị: Đọc Danh Sách Hạnh Kiểm (Đầy đủ) ===
        public List<HanhKiemDTO> DocDSHanhKiem() // Sửa: HanhKiem -> HanhKiemDTO
        {
            return hanhKiemDAO.DocDSHanhKiemDayDu();
        }

        // === 2. CRUD + Validate: Thêm Hạnh Kiểm ===
        public string ThemHanhKiem(HanhKiemDTO hk) // Sửa: HanhKiem -> HanhKiemDTO
        {
            if (hk.MaHocSinh <= 0 || hk.MaHocKy <= 0)
            {
                return "Mã học sinh và Mã học kỳ không được để trống/âm.";
            }

            if (string.IsNullOrWhiteSpace(hk.XepLoai) || !XepLoaiHopLe.Contains(hk.XepLoai))
            {
                return "Xếp loại phải là 'Tốt', 'Khá', 'Trung bình', hoặc 'Yếu'.";
            }

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
        public string CapNhatHanhKiem(HanhKiemDTO hk) // Sửa: HanhKiem -> HanhKiemDTO
        {
            if (hk.MaHocSinh <= 0 || hk.MaHocKy <= 0)
            {
                return "Mã học sinh và Mã học kỳ không hợp lệ.";
            }
            if (string.IsNullOrWhiteSpace(hk.XepLoai) || !XepLoaiHopLe.Contains(hk.XepLoai))
            {
                return "Xếp loại phải là 'Tốt', 'Khá', 'Trung bình', hoặc 'Yếu'.";
            }

            if (hanhKiemDAO.CapNhatHanhKiem(hk))
            {
                return "Cập nhật hạnh kiểm thành công.";
            }
            return "Cập nhật hạnh kiểm thất bại.";
        }

        // === 4. CRUD: Xóa Hạnh Kiểm ===
        public string XoaHanhKiem(int maHocSinh, int maHocKy) // Sửa: string -> int
        {
            if (hanhKiemDAO.XoaHanhKiem(maHocSinh, maHocKy))
            {
                return "Xóa hạnh kiểm thành công.";
            }
            return "Xóa hạnh kiểm thất bại.";
        }

        // === 5. Lấy Hạnh Kiểm Theo Học Sinh và Học Kỳ ===
        public HanhKiemDTO GetHanhKiemByStudent(int maHocSinh, int maHocKy) // Sửa: HanhKiem -> HanhKiemDTO
        {
            try
            {
                return hanhKiemDAO.LayHanhKiemTheoKey(maHocSinh, maHocKy); // Đã là int
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS GetHanhKiemByStudent: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lấy tất cả hạnh kiểm trong hệ thống
        /// Dùng cho logic phân lớp tự động
        /// </summary>
        public List<HanhKiemDTO> GetAllHanhKiem()
        {
            try
            {
                return hanhKiemDAO.GetAllHanhKiem();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy tất cả hạnh kiểm: " + ex.Message);
            }
        }
    }
}