using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class GiaoVienBUS
    {
        private GiaoVienDAO giaoVienDAO;
        private NguoiDungDAO nguoiDungDAO;

        public GiaoVienBUS()
        {
            giaoVienDAO = new GiaoVienDAO();
            nguoiDungDAO = new NguoiDungDAO();
        }

        // ✅ LẤY TÊN GIÁO VIÊN THEO MÃ (Requirement chính)
        public string LayTenGiaoVienTheoMa(string maGiaoVien)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maGiaoVien))
                {
                    throw new ArgumentException("Mã giáo viên không được để trống!");
                }

                return giaoVienDAO.LayTenGiaoVienTheoMa(maGiaoVien);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy tên giáo viên: {ex.Message}");
            }
        }

        // Thêm giáo viên với validation và tự động tạo tài khoản
        public bool ThemGiaoVien(GiaoVienDTO giaoVien)
        {
            try
            {
                // Validation dữ liệu
                if (string.IsNullOrWhiteSpace(giaoVien.MaGiaoVien))
                {
                    throw new ArgumentException("Mã giáo viên không được để trống.");
                }

                if (string.IsNullOrWhiteSpace(giaoVien.HoTen))
                {
                    throw new ArgumentException("Họ tên không được để trống.");
                }

                // Kiểm tra mã giáo viên đã tồn tại chưa
                if (giaoVienDAO.LayGiaoVienTheoMa(giaoVien.MaGiaoVien) != null)
                {
                    throw new ArgumentException("Mã giáo viên đã tồn tại.");
                }

                // Kiểm tra tên đăng nhập đã tồn tại chưa (nếu dùng mã giáo viên làm tên đăng nhập)
                if (nguoiDungDAO.CheckTenDangNhapExists(giaoVien.MaGiaoVien))
                {
                    throw new ArgumentException("Tên đăng nhập (mã giáo viên) đã tồn tại trong hệ thống.");
                }

                // Kiểm tra email hợp lệ
                if (!string.IsNullOrEmpty(giaoVien.Email))
                {
                    if (!KiemTraEmailHopLe(giaoVien.Email))
                    {
                        throw new ArgumentException("Email không hợp lệ.");
                    }

                    if (giaoVienDAO.KiemTraEmailTonTai(giaoVien.Email))
                    {
                        throw new ArgumentException("Email đã được sử dụng.");
                    }
                }

                // Kiểm tra số điện thoại hợp lệ
                if (!string.IsNullOrEmpty(giaoVien.SoDienThoai))
                {
                    if (!KiemTraSoDienThoaiHopLe(giaoVien.SoDienThoai))
                    {
                        throw new ArgumentException("Số điện thoại không hợp lệ.");
                    }
                }

                // Kiểm tra ngày sinh hợp lệ
                if (giaoVien.NgaySinh != DateTime.MinValue)
                {
                    if (giaoVien.NgaySinh >= DateTime.Now)
                    {
                        throw new ArgumentException("Ngày sinh phải nhỏ hơn ngày hiện tại.");
                    }

                    int tuoi = DateTime.Now.Year - giaoVien.NgaySinh.Year;
                    if (tuoi < 22)
                    {
                        throw new ArgumentException("Giáo viên phải từ 22 tuổi trở lên.");
                    }
                }

                // ✅ Tạo tài khoản tự động cho giáo viên
                // Sử dụng mã giáo viên làm tên đăng nhập
                string tenDangNhap = giaoVien.MaGiaoVien;
                // Mật khẩu mặc định: ngày sinh (ddMMyyyy) hoặc "12345678"
                string matKhau = giaoVien.NgaySinh != DateTime.MinValue 
                    ? giaoVien.NgaySinh.ToString("ddMMyyyy") 
                    : "12345678";

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
                    HoTen = giaoVien.HoTen,
                    Email = giaoVien.Email,
                    SoDienThoai = giaoVien.SoDienThoai,
                    NgaySinh = giaoVien.NgaySinh != DateTime.MinValue ? (DateTime?)giaoVien.NgaySinh : null,
                    GioiTinh = giaoVien.GioiTinh,
                    DiaChi = giaoVien.DiaChi,
                    LoaiDoiTuong = "giaovien"
                };

                // ✅ Thêm giáo viên và tài khoản trong transaction
                // Sử dụng phương thức ThemNguoiDungVoiVaiTroVaHoSo với MaVaiTro = "teacher"
                bool themTaiKhoan = nguoiDungDAO.ThemNguoiDungVoiVaiTroVaHoSo(nguoiDung, "teacher", hoSo);
                
                if (!themTaiKhoan)
                {
                    throw new Exception("Không thể tạo tài khoản cho giáo viên.");
                }

                // Thêm giáo viên vào bảng GiaoVien
                return giaoVienDAO.ThemGiaoVien(giaoVien);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi thêm giáo viên: {ex.Message}");
            }
        }

        // Đọc danh sách giáo viên
        public List<GiaoVienDTO> DocDSGiaoVien()
        {
            try
            {
                return giaoVienDAO.DocDSGiaoVien();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi đọc danh sách giáo viên: {ex.Message}");
            }
        }

        // Lấy giáo viên theo mã
        public GiaoVienDTO LayGiaoVienTheoMa(string maGiaoVien)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maGiaoVien))
                {
                    throw new ArgumentException("Mã giáo viên không được để trống.");
                }

                return giaoVienDAO.LayGiaoVienTheoMa(maGiaoVien);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy thông tin giáo viên: {ex.Message}");
            }
        }

        // Cập nhật giáo viên với validation
        public bool CapNhatGiaoVien(GiaoVienDTO giaoVien)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(giaoVien.MaGiaoVien))
                {
                    throw new ArgumentException("Mã giáo viên không được để trống.");
                }

                if (string.IsNullOrWhiteSpace(giaoVien.HoTen))
                {
                    throw new ArgumentException("Họ tên không được để trống.");
                }

                // Kiểm tra giáo viên có tồn tại không
                GiaoVienDTO giaoVienHienTai = giaoVienDAO.LayGiaoVienTheoMa(giaoVien.MaGiaoVien);
                if (giaoVienHienTai == null)
                {
                    throw new ArgumentException("Không tìm thấy giáo viên với mã này.");
                }

                // Kiểm tra email hợp lệ và trùng lặp
                if (!string.IsNullOrEmpty(giaoVien.Email))
                {
                    if (!KiemTraEmailHopLe(giaoVien.Email))
                    {
                        throw new ArgumentException("Email không hợp lệ.");
                    }

                    if (giaoVienDAO.KiemTraEmailTonTai(giaoVien.Email, giaoVien.MaGiaoVien))
                    {
                        throw new ArgumentException("Email đã được sử dụng bởi giáo viên khác.");
                    }
                }

                // Kiểm tra số điện thoại hợp lệ
                if (!string.IsNullOrEmpty(giaoVien.SoDienThoai))
                {
                    if (!KiemTraSoDienThoaiHopLe(giaoVien.SoDienThoai))
                    {
                        throw new ArgumentException("Số điện thoại không hợp lệ.");
                    }
                }

                // Kiểm tra ngày sinh hợp lệ
                if (giaoVien.NgaySinh != DateTime.MinValue)
                {
                    if (giaoVien.NgaySinh >= DateTime.Now)
                    {
                        throw new ArgumentException("Ngày sinh phải nhỏ hơn ngày hiện tại.");
                    }

                    int tuoi = DateTime.Now.Year - giaoVien.NgaySinh.Year;
                    if (tuoi < 22)
                    {
                        throw new ArgumentException("Giáo viên phải từ 22 tuổi trở lên.");
                    }
                }

                return giaoVienDAO.CapNhatGiaoVien(giaoVien);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi cập nhật giáo viên: {ex.Message}");
            }
        }

        // Xóa giáo viên với validation
        public bool XoaGiaoVien(string maGiaoVien)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maGiaoVien))
                {
                    throw new ArgumentException("Mã giáo viên không được để trống.");
                }

                GiaoVienDTO giaoVien = giaoVienDAO.LayGiaoVienTheoMa(maGiaoVien);
                if (giaoVien == null)
                {
                    throw new ArgumentException("Không tìm thấy giáo viên với mã này.");
                }

                // Có thể thêm kiểm tra xem giáo viên có đang làm GVCN hay dạy môn nào không
                // Trước khi xóa (tùy theo yêu cầu hệ thống)

                return giaoVienDAO.XoaGiaoVien(maGiaoVien);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi xóa giáo viên: {ex.Message}");
            }
        }

        // Phương thức hỗ trợ: Kiểm tra email hợp lệ
        private bool KiemTraEmailHopLe(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        // Phương thức hỗ trợ: Kiểm tra số điện thoại hợp lệ
        private bool KiemTraSoDienThoaiHopLe(string soDienThoai)
        {
            if (string.IsNullOrWhiteSpace(soDienThoai))
                return false;

            // Kiểm tra số điện thoại Việt Nam (10 số, bắt đầu bằng 0)
            string pattern = @"^0\d{9}$";
            return Regex.IsMatch(soDienThoai, pattern);
        }

        // Tìm kiếm giáo viên theo tên hoặc mã
        public List<GiaoVienDTO> TimKiemGiaoVien(string keyword)
        {
            try
            {
                var danhSach = DocDSGiaoVien();
                if (string.IsNullOrWhiteSpace(keyword))
                    return danhSach;

                keyword = keyword.ToLower().Trim();
                return danhSach.Where(gv =>
                    (gv.HoTen != null && gv.HoTen.ToLower().Contains(keyword)) ||
                    (gv.MaGiaoVien != null && gv.MaGiaoVien.ToLower().Contains(keyword)) ||
                    (gv.Email != null && gv.Email.ToLower().Contains(keyword)) ||
                    (gv.SoDienThoai != null && gv.SoDienThoai.Contains(keyword))
                ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tìm kiếm giáo viên: {ex.Message}");
            }
        }

        // Lọc giáo viên theo bộ môn
        public List<GiaoVienDTO> LocGiaoVienTheoBoMon(int? maMonHoc)
        {
            try
            {
                var danhSach = DocDSGiaoVien();
                if (!maMonHoc.HasValue)
                    return danhSach;

                return danhSach.Where(gv => gv.MaMonChuyenMon == maMonHoc.Value).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lọc giáo viên theo bộ môn: {ex.Message}");
            }
        }

        // Tìm kiếm và lọc kết hợp
        public List<GiaoVienDTO> TimKiemVaLocGiaoVien(string keyword, int? maMonHoc)
        {
            try
            {
                var danhSach = DocDSGiaoVien();

                // Lọc theo bộ môn trước
                if (maMonHoc.HasValue)
                {
                    danhSach = danhSach.Where(gv => gv.MaMonChuyenMon == maMonHoc.Value).ToList();
                }

                // Tìm kiếm sau
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    keyword = keyword.ToLower().Trim();
                    danhSach = danhSach.Where(gv =>
                        (gv.HoTen != null && gv.HoTen.ToLower().Contains(keyword)) ||
                        (gv.MaGiaoVien != null && gv.MaGiaoVien.ToLower().Contains(keyword)) ||
                        (gv.Email != null && gv.Email.ToLower().Contains(keyword)) ||
                        (gv.SoDienThoai != null && gv.SoDienThoai.Contains(keyword))
                    ).ToList();
                }

                return danhSach;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tìm kiếm và lọc giáo viên: {ex.Message}");
            }
        }

        // Thống kê giáo viên
        public Dictionary<string, int> ThongKeGiaoVien()
        {
            try
            {
                var danhSach = DocDSGiaoVien();
                var thongKe = new Dictionary<string, int>
                {
                    ["TongGiaoVien"] = danhSach.Count,
                    ["Nam"] = danhSach.Count(gv => gv.GioiTinh == "Nam"),
                    ["Nu"] = danhSach.Count(gv => gv.GioiTinh == "Nữ"),
                    ["BoMon"] = danhSach.Where(gv => gv.MaMonChuyenMon.HasValue)
                                        .Select(gv => gv.MaMonChuyenMon.Value)
                                        .Distinct()
                                        .Count()
                };
                return thongKe;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi thống kê giáo viên: {ex.Message}");
            }
        }

        // ✅ Lấy mã giáo viên tiếp theo tự động
        public string LayMaGiaoVienTiepTheo()
        {
            try
            {
                return giaoVienDAO.LayMaGiaoVienTiepTheo();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy mã giáo viên tiếp theo: {ex.Message}");
            }
        }
    }
}