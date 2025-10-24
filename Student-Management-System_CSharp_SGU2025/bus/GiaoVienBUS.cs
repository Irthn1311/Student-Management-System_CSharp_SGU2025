using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class GiaoVienBUS
    {
        private GiaoVienDAO giaoVienDAO;

        public GiaoVienBUS()
        {
            giaoVienDAO = new GiaoVienDAO();
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

        // Thêm giáo viên với validation
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
    }
}