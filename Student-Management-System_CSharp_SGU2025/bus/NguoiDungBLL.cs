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
        /// Cập nhật trạng thái tài khoản người dùng
        /// </summary>
        public bool UpdateTrangThai(string tenDangNhap, string trangThai)
        {
            return nguoiDungDAO.UpdateTrangThai(tenDangNhap, trangThai);
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

        /// <summary>
        /// Thêm tài khoản mới với vai trò và hồ sơ
        /// </summary>
        public bool ThemTaiKhoan(string tenDangNhap, string hoTen, string email, string soDienThoai,
            DateTime ngaySinh, string gioiTinh, string maVaiTro)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(tenDangNhap))
                    throw new Exception("Tên đăng nhập không được để trống!");

                if (nguoiDungDAO.CheckTenDangNhapExists(tenDangNhap))
                    throw new Exception("Tên đăng nhập đã tồn tại!");

                if (string.IsNullOrWhiteSpace(maVaiTro))
                    throw new Exception("Vui lòng chọn vai trò!");

                // Tạo mật khẩu từ ngày sinh (format: ddMMyyyy)
                string matKhau = ngaySinh.ToString("ddMMyyyy");

                // Tạo DTO NguoiDung
                NguoiDungDTO nguoiDung = new NguoiDungDTO
                {
                    TenDangNhap = tenDangNhap,
                    MatKhau = matKhau, // Có thể hash tại đây nếu cần
                    TrangThai = "Hoạt động"
                };

                // Tạo DTO HoSoNguoiDung
                HoSoNguoiDungDTO hoSo = new HoSoNguoiDungDTO
                {
                    TenDangNhap = tenDangNhap,
                    HoTen = hoTen,
                    Email = email,
                    SoDienThoai = soDienThoai,
                    NgaySinh = ngaySinh,
                    GioiTinh = gioiTinh,
                    LoaiDoiTuong = maVaiTro // LoaiDoiTuong = MaVaiTro
                };

                // Thêm vào database
                return nguoiDungDAO.ThemNguoiDungVoiVaiTroVaHoSo(nguoiDung, maVaiTro, hoSo);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Kiểm tra tên đăng nhập đã tồn tại
        /// </summary>
        public bool KiemTraTenDangNhapTonTai(string tenDangNhap)
        {
            return nguoiDungDAO.CheckTenDangNhapExists(tenDangNhap);
        }


        /// <summary>
        /// Xóa người dùng
        /// </summary>
        public bool XoaNguoiDung(string tenDangNhap)
        {
            try
            {
                // Kiểm tra tài khoản có tồn tại không
                if (!nguoiDungDAO.CheckTenDangNhapExists(tenDangNhap))
                {
                    throw new Exception($"Tài khoản '{tenDangNhap}' không tồn tại!");
                }

                // Không cho phép xóa tài khoản admin
                var nguoiDung = nguoiDungDAO.GetNguoiDungByTenDangNhap(tenDangNhap);
                if (nguoiDung != null && !string.IsNullOrEmpty(nguoiDung.MaVaiTro))
                {
                    if (nguoiDung.MaVaiTro.ToLower().Contains("admin"))
                    {
                        throw new Exception("Không thể xóa tài khoản quản trị viên!");
                    }
                }

                return nguoiDungDAO.DeleteNguoiDung(tenDangNhap);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Cập nhật mật khẩu
        /// </summary>
        public bool CapNhatMatKhau(string tenDangNhap, string matKhauMoi)
        {
            return nguoiDungDAO.UpdateMatKhau(tenDangNhap, matKhauMoi);
        }

        /// <summary>
        /// ✅ Cập nhật vai trò cho tài khoản
        /// </summary>
        public bool CapNhatVaiTro(string tenDangNhap, string maVaiTroMoi)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tenDangNhap))
                    throw new Exception("Tên đăng nhập không được để trống!");

                if (string.IsNullOrWhiteSpace(maVaiTroMoi))
                    throw new Exception("Mã vai trò không được để trống!");

                return nguoiDungDAO.UpdateVaiTro(tenDangNhap, maVaiTroMoi);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Toggle trạng thái tài khoản (Hoạt động <-> Bị khóa)
        /// </summary>
        public bool ToggleTrangThai(string tenDangNhap)
        {
            try
            {
                // Lấy trạng thái hiện tại
                var nguoiDung = nguoiDungDAO.GetNguoiDungByTenDangNhap(tenDangNhap);
                if (nguoiDung == null)
                    throw new Exception("Không tìm thấy tài khoản!");

                // Đổi trạng thái
                string trangThaiMoi = nguoiDung.TrangThai == "Hoạt động" ? "Bị khóa" : "Hoạt động";

                return nguoiDungDAO.UpdateTrangThai(tenDangNhap, trangThaiMoi);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ✅ Cập nhật vai trò và đồng bộ LoaiDoiTuong
        /// </summary>
        public bool CapNhatVaiTroVaLoaiDoiTuong(string tenDangNhap, string maVaiTroMoi)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tenDangNhap))
                    throw new Exception("Tên đăng nhập không được để trống!");

                if (string.IsNullOrWhiteSpace(maVaiTroMoi))
                    throw new Exception("Mã vai trò không được để trống!");

                return nguoiDungDAO.UpdateVaiTroVaLoaiDoiTuong(tenDangNhap, maVaiTroMoi);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Lấy hồ sơ người dùng theo tên đăng nhập
        /// </summary>
        public HoSoNguoiDungDTO GetHoSoByTenDangNhap(string tenDangNhap)
        {
            return nguoiDungDAO.GetHoSoByTenDangNhap(tenDangNhap);
        }

        /// <summary>
        /// Tìm kiếm người dùng theo từ khóa (tên đăng nhập, vai trò, trạng thái)
        /// </summary>
        public List<NguoiDungDTO> SearchNguoiDung(string keyword)
        {
            try
            {
                // Nếu từ khóa rỗng, trả về tất cả
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return nguoiDungDAO.GetAllNguoiDung();
                }

                return nguoiDungDAO.SearchNguoiDung(keyword.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungBLL.SearchNguoiDung: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// ✅ Đổi mật khẩu tài khoản (PLAINTEXT - Không hash)
        /// </summary>
        /// <param name="tenDangNhap">Tên đăng nhập</param>
        /// <param name="matKhauCu">Mật khẩu cũ (plaintext)</param>
        /// <param name="matKhauMoi">Mật khẩu mới (plaintext)</param>
        /// <returns>True nếu đổi thành công, False nếu thất bại</returns>
        public bool DoiMatKhau(string tenDangNhap, string matKhauCu, string matKhauMoi)
        {
            try
            {
                // 1. Validate input
                if (string.IsNullOrWhiteSpace(tenDangNhap))
                    throw new Exception("Tên đăng nhập không được để trống!");

                if (string.IsNullOrWhiteSpace(matKhauCu))
                    throw new Exception("Mật khẩu cũ không được để trống!");

                if (string.IsNullOrWhiteSpace(matKhauMoi))
                    throw new Exception("Mật khẩu mới không được để trống!");

                if (matKhauMoi.Length < 6)
                    throw new Exception("Mật khẩu mới phải có ít nhất 6 ký tự!");

                // 2. ✅ KHÔNG HASH - So sánh trực tiếp plaintext
                bool matKhauCuDung = nguoiDungDAO.KiemTraMatKhauHienTai(tenDangNhap, matKhauCu);

                if (!matKhauCuDung)
                {
                    throw new Exception("Mật khẩu hiện tại không đúng!");
                }

                // 3. ✅ KHÔNG HASH - Lưu plaintext trực tiếp
                bool ketQua = nguoiDungDAO.UpdateMatKhau(tenDangNhap, matKhauMoi);

                if (ketQua)
                {
                    Console.WriteLine($"[SUCCESS] Đã đổi mật khẩu cho tài khoản: {tenDangNhap}");
                }

                return ketQua;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungBLL.DoiMatKhau: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// ✅ Lấy email của tài khoản đăng nhập (để gửi thông báo)
        /// </summary>
        public string LayEmailTaiKhoan(string tenDangNhap)
        {
            try
            {
                var danhSachMaVaiTro = new PhanQuyenBUS().GetVaiTroByNguoiDung(tenDangNhap);

                // Trường hợp học sinh
                if (tenDangNhap.StartsWith("HS", StringComparison.OrdinalIgnoreCase))
                {
                    string maHocSinhStr = tenDangNhap.Substring(2);
                    if (int.TryParse(maHocSinhStr, out int maHocSinh))
                    {
                        HocSinhBLL hocSinhBLL = new HocSinhBLL();
                        return hocSinhBLL.LayEmailTheoMaHocSinh(maHocSinh);
                    }
                }

                // Trường hợp phụ huynh
                if (tenDangNhap.StartsWith("PH", StringComparison.OrdinalIgnoreCase))
                {
                    string maPhuHuynhStr = tenDangNhap.Substring(2);
                    if (int.TryParse(maPhuHuynhStr, out int maPhuHuynh))
                    {
                        PhuHuynhBLL phuHuynhBLL = new PhuHuynhBLL();
                        var phuHuynh = phuHuynhBLL.GetPhuHuynhById(maPhuHuynh);
                        return phuHuynh?.Email;
                    }
                }

                // Trường hợp giáo viên
                if (tenDangNhap.StartsWith("GV", StringComparison.OrdinalIgnoreCase))
                {
                    GiaoVienBUS giaoVienBUS = new GiaoVienBUS();
                    var giaoVien = giaoVienBUS.LayGiaoVienTheoMa(tenDangNhap);
                    return giaoVien?.Email;
                }

                // Trường hợp khác - lấy từ HoSoNguoiDung
                var hoSo = nguoiDungDAO.GetHoSoByTenDangNhap(tenDangNhap);
                return hoSo?.Email;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] NguoiDungBLL.LayEmailTaiKhoan: {ex.Message}");
                return null;
            }
        }

    }
}
