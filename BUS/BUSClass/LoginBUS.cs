using Student_Management_System_CSharp_SGU2025.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class LoginBUS
    {
        private LoginDAO loginDAO;

        public LoginBUS()
        {
            loginDAO = new LoginDAO();
        }

        /// <summary>
        /// Kiểm tra thông tin đăng nhập, bao gồm kiểm tra rỗng và xác thực với CSDL.
        /// </summary>
        public bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(matKhau))
            {
                throw new ArgumentException("Mật khẩu không được để trống.");
            }

            try
            {
                // ** LƯU Ý BẢO MẬT: Nên HASH mật khẩu trước khi gọi DAO **
                // var matKhauDaBam = MaHoaMatKhau(matKhau);
                // return loginDAO.KiemTraTaiKhoanCoDangNhapDuocKhong(tenDangNhap, matKhauDaBam, "Hoạt động");

                // Tạm thời dùng mật khẩu gốc
                return loginDAO.KiemTraTaiKhoanCoDangNhapDuocKhong(tenDangNhap, matKhau, "Hoạt động");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL KiemTraDangNhap: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng.
        /// </summary>
        public List<(String tenDangNhap, String matKhau, String trangThai)> LayDanhSachNguoiDung()
        {
            try
            {
                return loginDAO.LayDanhSachNguoiDung();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL LayDanhSachNguoiDung: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết một người dùng.
        /// </summary>
        public (string tenDangNhap, string matKhau, string trangThai)? LayNguoiDungTheoTen(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống.");
            }

            try
            {
                return loginDAO.LayNguoiDungTheoTen(tenDangNhap);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL LayNguoiDungTheoTen: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tìm kiếm người dùng.
        /// </summary>
        public List<(String tenDangNhap, String matKhau, String trangThai)> TimKiemNguoiDung(string tuKhoa)
        {
            try
            {
                // Nếu từ khóa rỗng, trả về tất cả
                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    return loginDAO.LayDanhSachNguoiDung();
                }
                return loginDAO.TimKiemNguoiDung(tuKhoa);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL TimKiemNguoiDung: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Thêm người dùng mới.
        /// </summary>
        public bool ThemNguoiDung(string tenDangNhap, string matKhau, string trangThai)
        {
            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(tenDangNhap))
                throw new ArgumentException("Tên đăng nhập không được để trống.");
            if (string.IsNullOrWhiteSpace(matKhau))
                throw new ArgumentException("Mật khẩu không được để trống.");
            if (string.IsNullOrWhiteSpace(trangThai))
                throw new ArgumentException("Trạng thái không được để trống.");

            // 2. Kiểm tra nghiệp vụ (ví dụ: độ dài mật khẩu)
            if (matKhau.Length < 6)
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự.");

            // 3. Kiểm tra tồn tại
            if (loginDAO.KiemTraNguoiDungTonTai(tenDangNhap))
            {
                throw new ArgumentException($"Tên đăng nhập '{tenDangNhap}' đã tồn tại.");
            }

            // 4. Gọi DAO (nên băm mật khẩu ở đây)
            // var matKhauDaBam = MaHoaMatKhau(matKhau);
            try
            {
                return loginDAO.ThemNguoiDung(tenDangNhap, matKhau, trangThai);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL ThemNguoiDung: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cập nhật người dùng.
        /// </summary>
        public bool CapNhatNguoiDung(string tenDangNhap, string matKhau, string trangThai)
        {
            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(tenDangNhap))
                throw new ArgumentException("Tên đăng nhập không được để trống.");
            if (string.IsNullOrWhiteSpace(matKhau))
                throw new ArgumentException("Mật khẩu không được để trống.");
            if (string.IsNullOrWhiteSpace(trangThai))
                throw new ArgumentException("Trạng thái không được để trống.");

            // 2. Kiểm tra nghiệp vụ (ví dụ: độ dài mật khẩu)
            if (matKhau.Length < 6)
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự.");

            // 3. Kiểm tra tồn tại
            if (!loginDAO.KiemTraNguoiDungTonTai(tenDangNhap))
            {
                throw new ArgumentException($"Tên đăng nhập '{tenDangNhap}' không tồn tại.");
            }

            // 4. Gọi DAO
            try
            {
                return loginDAO.CapNhatNguoiDung(tenDangNhap, matKhau, trangThai);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL CapNhatNguoiDung: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Xóa người dùng.
        /// </summary>
        public bool XoaNguoiDung(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
                throw new ArgumentException("Tên đăng nhập không được để trống.");

            // Không cho phép xóa admin
            if (tenDangNhap.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Không thể xóa tài khoản quản trị viên 'admin'.");
            }

            if (!loginDAO.KiemTraNguoiDungTonTai(tenDangNhap))
            {
                throw new ArgumentException($"Tên đăng nhập '{tenDangNhap}' không tồn tại.");
            }

            try
            {
                return loginDAO.XoaNguoiDung(tenDangNhap);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL XoaNguoiDung: " + ex.Message);
                return false;
            }
        }
    }
}