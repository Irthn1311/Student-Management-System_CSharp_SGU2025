using Student_Management_System_CSharp_SGU2025.DAO; // Namespace chứa HocSinhDAO
using Student_Management_System_CSharp_SGU2025.DTO; // Namespace chứa HocSinhDTO
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class HocSinhBLL
    {
        // 1. Tạo thể hiện của HocSinhDAO
        private HocSinhDAO hocSinhDAO;

        public HocSinhBLL()
        {
            hocSinhDAO = new HocSinhDAO(); // Khởi tạo DAO khi BLL được tạo
        }

        /// <summary>
        /// Hàm tổng hợp kiểm tra tính hợp lệ của dữ liệu HocSinhDTO.
        /// </summary>
        /// <param name="hs">Đối tượng HocSinhDTO cần kiểm tra.</param>
        /// <param name="errors">Danh sách các lỗi tìm thấy (nếu có).</param>
        /// <returns>True nếu dữ liệu hợp lệ, False nếu có lỗi.</returns>
        public bool ValidateHocSinh(HocSinhDTO hs, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(hs.HoTen)) errors.Add("Họ và tên không được để trống.");
            if (!IsValidNgaySinh(hs.NgaySinh)) errors.Add("Ngày sinh không hợp lệ (tuổi 16-18)."); // Thêm giải thích
            if (string.IsNullOrWhiteSpace(hs.GioiTinh) || (hs.GioiTinh != "Nam" && hs.GioiTinh != "Nữ")) errors.Add("Vui lòng chọn giới tính.");
            if (string.IsNullOrWhiteSpace(hs.TrangThai) || (hs.TrangThai != "Đang học" && hs.TrangThai != "Nghỉ học")) errors.Add("Trạng thái không hợp lệ.");

            
            if (string.IsNullOrWhiteSpace(hs.Email))
            {
                errors.Add("Email không được để trống.");
            }
            // Chỉ kiểm tra định dạng nếu không rỗng (để tránh báo 2 lỗi trùng lặp)
            else if (!IsValidEmail(hs.Email))
            {
                errors.Add("Email không đúng định dạng.");
            }

            if (string.IsNullOrWhiteSpace(hs.SdtHS))
            {
                errors.Add("Số điện thoại không được để trống.");
            }
            // Chỉ kiểm tra định dạng nếu không rỗng
            else if (!IsValidPhoneNumber(hs.SdtHS))
            {
                errors.Add("Số điện thoại không hợp lệ.");
            }

            return errors.Count == 0;
        }

        // Các hàm kiểm tra định dạng (IsValid...) 
        private bool IsValidEmail(string email)
        {
            // Nếu email rỗng hoặc chỉ có khoảng trắng, coi như không hợp lệ (hoặc hợp lệ tùy yêu cầu)
            if (string.IsNullOrWhiteSpace(email))
                return false; // Đổi thành true nếu cho phép email trống

            try
            {
                // Cách kiểm tra chuẩn và đơn giản nhất trong .NET
                var addr = new System.Net.Mail.MailAddress(email);
                // Đảm bảo địa chỉ trả về đúng là chuỗi gốc (tránh trường hợp MailAddress tự sửa lỗi)
                return addr.Address == email;
            }
            catch (FormatException) // Bắt lỗi nếu định dạng không đúng
            {
                return false;
            }
            catch (ArgumentException) // Bắt lỗi nếu chuỗi rỗng (đã kiểm tra ở trên nhưng để an toàn)
            {
                return false;
            }
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Nếu SĐT rỗng hoặc chỉ có khoảng trắng, coi như không hợp lệ (hoặc hợp lệ tùy yêu cầu)
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false; // Đổi thành true nếu cho phép SĐT trống

            // Xóa các ký tự không phải số (dấu cách, dấu gạch ngang)
            string digitsOnly = Regex.Replace(phoneNumber, @"[^\d]", "");

            // Kiểm tra độ dài phải là 10 hoặc 11 số
            if (digitsOnly.Length < 10 || digitsOnly.Length > 11)
                return false;

            // Kiểm tra phải bắt đầu bằng số 0
            if (!digitsOnly.StartsWith("0"))
                return false;

            // Kiểm tra toàn bộ chuỗi gốc xem có ký tự lạ nào khác ngoài số, dấu cách, gạch ngang không
            // return Regex.IsMatch(phoneNumber, @"^[\d\s\-]+$");

            // Regex chặt hơn: Bắt đầu bằng 0, theo sau là 9 hoặc 10 chữ số
            return Regex.IsMatch(digitsOnly, @"^0\d{9,10}$");
        }
        private bool IsValidNgaySinh(DateTime ngaySinh) {
            // 1. Không được là ngày trong tương lai
            if (ngaySinh.Date > DateTime.Today) // So sánh chỉ phần ngày
            {
                return false;
            }

            // 2. Kiểm tra tuổi hợp lệ (16-18 tuổi)
            int currentYear = DateTime.Now.Year;
            int birthYear = ngaySinh.Year;
            int age = currentYear - birthYear;

            // Điều chỉnh tuổi nếu chưa qua sinh nhật trong năm hiện tại
            // Ví dụ: Sinh 01/11/2008, hôm nay 22/10/2024 -> Vẫn tính là 15 tuổi
            if (ngaySinh.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            // Kiểm tra xem tuổi có nằm trong khoảng 16 đến 18 không
            if (age < 16 || age > 18)
            {
                return false; // Không đúng độ tuổi cấp 3
            }

            return true; // Hợp lệ
        }


        // === CÁC HÀM NGHIỆP VỤ GỌI QUA DAO ===

        /// <summary>
        /// Lấy danh sách tất cả học sinh.
        /// </summary>
        /// <returns>List HocSinhDTO.</returns>
        public List<HocSinhDTO> GetAllHocSinh()
        {
            try
            {
                // Gọi hàm tương ứng trong DAO
                return hocSinhDAO.LayDanhSachHocSinh();
            }
            catch (Exception ex)
            {
                // Xử lý hoặc ghi log lỗi
                Console.WriteLine("Lỗi BLL GetAllHocSinh: " + ex.Message);
                throw; // Ném lại lỗi để lớp UI xử lý (hoặc trả về list rỗng)
            }
        }

        /// <summary>
        /// Tổng số lượng tất cả học sinh.
        /// </summary>
        /// <returns>Tổng số lượng học sinh</returns>
        public int GetTotalHocSinh()
        {
            try
            {
                return hocSinhDAO.DemTongSoLuongHocSinh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetTotalHocSinh: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tổng số lượng tất cả học sinh nam .
        /// </summary>
        /// <returns>Tổng số lượng học sinh nam </returns>
        public int GetTotalHocSinhNam()
        {
            try
            {
                return hocSinhDAO.DemTongSoLuongHocSinhNam();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetTotalHocSinhNam: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tổng số lượng tất cả học sinh nữ.
        /// </summary>
        /// <returns>Tổng số lượng học sinh nữ</returns>
        public int GetTotalHocSinhNu()
        {
            try
            {
                return hocSinhDAO.DemTongSoLuongHocSinhNu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetTotalHocSinhNu: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Tổng số lượng tất cả học sinh có trạng thái "Đang học".
        /// </summary>
        /// <returns>Tổng số lượng học sinh có trạng thái " Đang học"</returns>
        public int GetTotalHocSinhDangHoc()
        {
            try
            {
                return hocSinhDAO.DemTongSoLuongHocSinhDangHoc();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetTotalHocSinhDangHoc: " + ex.Message);
                throw;
            }
        }


        /// <summary>
        /// Thêm học sinh mới sau khi đã kiểm tra dữ liệu và trùng lặp.
        /// </summary>
        /// <returns>ID (Ma_Hoc_Sinh) của học sinh vừa thêm, hoặc -1 nếu thất bại.</returns>
        public int AddHocSinh(HocSinhDTO hs)
        {
            List<string> errors;
            if (!ValidateHocSinh(hs, out errors))
            {
                throw new ArgumentException("Dữ liệu học sinh không hợp lệ:\n" + string.Join("\n", errors));
            }

            try
            {
                // === THÊM KIỂM TRA TRÙNG LẶP ===
                if (!string.IsNullOrWhiteSpace(hs.SdtHS) && hocSinhDAO.KiemTraTrungSdt(hs.SdtHS))
                {
                    throw new ArgumentException($"Số điện thoại '{hs.SdtHS}' đã tồn tại.");
                }
                if (!string.IsNullOrWhiteSpace(hs.Email) && hocSinhDAO.KiemTraTrungEmail(hs.Email))
                {
                    throw new ArgumentException($"Email '{hs.Email}' đã tồn tại.");
                }
                // ================================

                return hocSinhDAO.ThemHocSinh(hs);
            }
            catch (ArgumentException) // Bắt lỗi validation (trùng lặp)
            {
                throw; // Ném lại để UI xử lý
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL AddHocSinh: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Cập nhật thông tin học sinh sau khi đã kiểm tra dữ liệu và trùng lặp.
        /// </summary>
        /// <returns>True nếu thành công, False nếu thất bại.</returns>
        public bool UpdateHocSinh(HocSinhDTO hs)
        {
            List<string> errors;
            if (!ValidateHocSinh(hs, out errors))
            {
                throw new ArgumentException("Dữ liệu học sinh không hợp lệ:\n" + string.Join("\n", errors));
            }

            if (!hocSinhDAO.KiemTraTonTai(hs.MaHS))
            {
                throw new ArgumentException($"Không tìm thấy học sinh với mã {hs.MaHS} để cập nhật.");
            }

            try
            {
                // === THÊM KIỂM TRA TRÙNG LẶP KHI CẬP NHẬT ===
                // Kiểm tra SĐT (loại trừ chính học sinh này)
                if (!string.IsNullOrWhiteSpace(hs.SdtHS) && hocSinhDAO.KiemTraTrungSdt(hs.SdtHS, hs.MaHS))
                {
                    throw new ArgumentException($"Số điện thoại '{hs.SdtHS}' đã tồn tại cho một học sinh khác.");
                }

                // Kiểm tra Email (loại trừ chính học sinh này)
                if (!string.IsNullOrWhiteSpace(hs.Email) && hocSinhDAO.KiemTraTrungEmail(hs.Email, hs.MaHS))
                {
                    throw new ArgumentException($"Email '{hs.Email}' đã tồn tại cho một học sinh khác.");
                }
                // ===========================================

                return hocSinhDAO.CapNhatHocSinh(hs);
            }
            catch (ArgumentException) // Bắt lỗi validation (trùng lặp)
            {
                throw; // Ném lại để UI xử lý
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL UpdateHocSinh: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Xóa học sinh.
        /// </summary>
        /// <param name="maHS">Mã học sinh cần xóa.</param>
        /// <returns>True nếu thành công, False nếu thất bại.</returns>
        public bool DeleteHocSinh(int maHS)
        {
            // Có thể thêm kiểm tra ràng buộc trước khi xóa (ví dụ: học sinh còn điểm, còn TKB không?)
            if (!hocSinhDAO.KiemTraTonTai(maHS))
            {
                throw new ArgumentException($"Không tìm thấy học sinh với mã {maHS} để xóa.");
            }
            try
            {
                return hocSinhDAO.XoaHocSinh(maHS);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL DeleteHocSinh: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Tìm học sinh theo mã.
        /// </summary>
        public HocSinhDTO GetHocSinhById(int maHS)
        {
            try
            {
                return hocSinhDAO.TimHocSinhTheoMa(maHS);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetHocSinhById: " + ex.Message);
                throw;
            }
        }

        // Bạn có thể thêm các hàm BLL khác ở đây, ví dụ: tìm kiếm, lọc...
        // public List<HocSinhDTO> SearchHocSinhByName(string name) { ... }
    }
}