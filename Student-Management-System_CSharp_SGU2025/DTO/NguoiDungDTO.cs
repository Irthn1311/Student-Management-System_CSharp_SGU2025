using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO cho bảng NguoiDung (Tài khoản đăng nhập)
    /// </summary>
    public class NguoiDungDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; } // Mật khẩu đã hash (BCrypt hoặc SHA256)
        public string VaiTro { get; set; } // "HocSinh", "GiaoVien", "GiaoVu", "Admin" hoặc MaVaiTro như "HS", "GV"
        public DateTime? NgayTao { get; set; }
        public string TrangThai { get; set; } // "Hoạt động", "Bị khóa"

        // Constructor mặc định
        public NguoiDungDTO()
        {
            NgayTao = DateTime.Now;
            TrangThai = "Hoạt động";
        }

        // Constructor đầy đủ
        public NguoiDungDTO(string tenDangNhap, string matKhau, string vaiTro)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            VaiTro = vaiTro;
            NgayTao = DateTime.Now;
            TrangThai = "Hoạt động";
        }
    }
}
