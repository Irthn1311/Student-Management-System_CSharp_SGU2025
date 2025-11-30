using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.Utils
{
    /// <summary>
    /// Quản lý thông tin phiên đăng nhập của người dùng
    /// </summary>
    public static class SessionManager
    {
        // Thông tin người dùng hiện tại
        public static string TenDangNhap { get; set; }
        public static string HoTen { get; set; }
        public static string Email { get; set; }
        public static string VaiTro { get; private set; } // ✅ Thêm vai trò
        public static List<string> DanhSachVaiTro { get; private set; } // ✅ Danh sách vai trò

        /// <summary>
        /// Kiểm tra có người dùng đang đăng nhập hay không
        /// </summary>
        public static bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(TenDangNhap);
        }

        /// <summary>
        /// Đăng nhập - lưu thông tin người dùng
        /// </summary>
        public static void Login(string tenDangNhap, string hoTen = "", string email = "", string vaiTro = "", List<string> danhSachVaiTro = null)
        {
            TenDangNhap = tenDangNhap;
            HoTen = hoTen;
            Email = email;
            VaiTro = vaiTro; // ✅ Lưu vai trò
            DanhSachVaiTro = danhSachVaiTro ?? new List<string>(); // ✅ Lưu danh sách vai trò
        }

        /// <summary>
        /// Đăng xuất - xóa thông tin người dùng
        /// </summary>
        public static void Logout()
        {
            TenDangNhap = null;
            HoTen = null;
            Email = null;
            VaiTro = null;
            DanhSachVaiTro = null;
        }

        /// <summary>
        /// ✅ Lấy vai trò hiển thị (nếu có nhiều vai trò, lấy vai trò đầu tiên)
        /// </summary>
        public static string GetDisplayRole()
        {
            if (!string.IsNullOrEmpty(VaiTro))
                return VaiTro;

            if (DanhSachVaiTro != null && DanhSachVaiTro.Count > 0)
                return DanhSachVaiTro[0];

            return "Người dùng";
        }



    }
}