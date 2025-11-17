using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    /// <summary>
    /// BLL cho Người Dùng (Tài khoản đăng nhập)
    /// </summary>
    public class NguoiDungBLL
    {
        private NguoiDungDAO nguoiDungDAO;

        public NguoiDungBLL()
        {
            nguoiDungDAO = new NguoiDungDAO();
        }

        /// <summary>
        /// Thêm người dùng mới với mật khẩu tự động hash
        /// </summary>
        public bool AddNguoiDung(NguoiDungDTO nguoiDung)
        {
            // Kiểm tra tên đăng nhập đã tồn tại chưa
            if (nguoiDungDAO.CheckTenDangNhapExists(nguoiDung.TenDangNhap))
            {
                Console.WriteLine($"[WARNING] Tên đăng nhập '{nguoiDung.TenDangNhap}' đã tồn tại!");
                return false;
            }

            // Hash mật khẩu trước khi lưu (nếu chưa hash)
            if (!nguoiDung.MatKhau.StartsWith("$2"))  // Kiểm tra xem đã hash BCrypt chưa
            {
                nguoiDung.MatKhau = HashPassword(nguoiDung.MatKhau);
            }

            return nguoiDungDAO.AddNguoiDung(nguoiDung);
        }

        /// <summary>
        /// Thêm người dùng mới KHÔNG kiểm tra trùng (dùng cho import hàng loạt)
        /// </summary>
        public bool AddNguoiDungNoCheck(NguoiDungDTO nguoiDung)
        {
            // Hash mật khẩu trước khi lưu (nếu chưa hash)
            if (!nguoiDung.MatKhau.StartsWith("$2"))  // Kiểm tra xem đã hash BCrypt chưa
            {
                nguoiDung.MatKhau = HashPassword(nguoiDung.MatKhau);
            }

            return nguoiDungDAO.AddNguoiDung(nguoiDung);
        }

        /// <summary>
        /// Kiểm tra tên đăng nhập đã tồn tại chưa
        /// </summary>
        public bool CheckTenDangNhapExists(string tenDangNhap)
        {
            return nguoiDungDAO.CheckTenDangNhapExists(tenDangNhap);
        }

        /// <summary>
        /// Lấy thông tin người dùng theo tên đăng nhập
        /// </summary>
        public NguoiDungDTO GetNguoiDungByTenDangNhap(string tenDangNhap)
        {
            return nguoiDungDAO.GetNguoiDungByTenDangNhap(tenDangNhap);
        }

        /// <summary>
        /// Lấy tất cả người dùng
        /// </summary>
        public List<NguoiDungDTO> GetAllNguoiDung()
        {
            return nguoiDungDAO.GetAllNguoiDung();
        }

        /// <summary>
        /// Cập nhật mật khẩu (tự động hash)
        /// </summary>
        public bool UpdateMatKhau(string tenDangNhap, string matKhauMoi)
        {
            string hashedPassword = HashPassword(matKhauMoi);
            return nguoiDungDAO.UpdateMatKhau(tenDangNhap, hashedPassword);
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        public bool DeleteNguoiDung(string tenDangNhap)
        {
            return nguoiDungDAO.DeleteNguoiDung(tenDangNhap);
        }

        /// <summary>
        /// Hash mật khẩu bằng SHA256 (simple version - nên dùng BCrypt trong thực tế)
        /// </summary>
        private string HashPassword(string password)
        {
            // ✅ Dùng SHA256 (đơn giản hơn BCrypt, không cần thư viện thêm)
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Tạo username tự động từ Mã Học Sinh (hs001, hs002, hs003...)
        /// </summary>
        public string GenerateUsernameFromMaHS(int maHS)
        {
            return $"hs{maHS:D3}";  // hs001, hs002, hs003...
        }

        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        public bool ValidateLogin(string tenDangNhap, string matKhau)
        {
            var nguoiDung = nguoiDungDAO.GetNguoiDungByTenDangNhap(tenDangNhap);
            if (nguoiDung == null) return false;

            string hashedInputPassword = HashPassword(matKhau);
            return nguoiDung.MatKhau == hashedInputPassword;
        }
    }
}
