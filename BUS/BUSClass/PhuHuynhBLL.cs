using Student_Management_System_CSharp_SGU2025.DAO; // Namespace chứa PhuHuynhDAO
using Student_Management_System_CSharp_SGU2025.DTO; // Namespace chứa PhuHuynhDTO
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions; // Cần cho kiểm tra Email, SĐT

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class PhuHuynhBLL
    {
        // 1. Tạo thể hiện của PhuHuynhDAO
        private PhuHuynhDAO phuHuynhDAO;

        public PhuHuynhBLL()
        {
            phuHuynhDAO = new PhuHuynhDAO(); // Khởi tạo DAO
        }

        /// <summary>
        /// Hàm tổng hợp kiểm tra tính hợp lệ của dữ liệu PhuHuynhDTO.
        /// </summary>
        /// <param name="ph">Đối tượng PhuHuynhDTO cần kiểm tra.</param>
        /// <param name="errors">Danh sách các lỗi tìm thấy (nếu có).</param>
        /// <returns>True nếu dữ liệu hợp lệ, False nếu có lỗi.</returns>
        public bool ValidatePhuHuynh(PhuHuynhDTO ph, out List<string> errors)
        {
            errors = new List<string>();

            // 1. Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(ph.HoTen))
            {
                errors.Add("Họ và tên phụ huynh không được để trống.");
            }
            
             if (string.IsNullOrWhiteSpace(ph.SoDienThoai))
            {
                errors.Add("Số điện thoại không được để trống.");
            }

            // 2. Kiểm tra định dạng Email (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(ph.Email) && !IsValidEmail(ph.Email))
            {
                errors.Add("Địa chỉ Email không đúng định dạng.");
            }

            // 3. Kiểm tra định dạng Số điện thoại (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(ph.SoDienThoai) && !IsValidPhoneNumber(ph.SoDienThoai))
            {
                errors.Add("Số điện thoại không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(ph.DiaChi))
            {
                errors.Add("Địa chỉ không được để trống.");
            }

            return errors.Count == 0;
        }

        // --- Các hàm kiểm tra định dạng (IsValid...) ---
        
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return true; // Cho phép trống
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return true; // Cho phép trống
            string digitsOnly = Regex.Replace(phoneNumber, @"[^\d]", "");
            if (digitsOnly.Length < 10 || digitsOnly.Length > 11) return false;
            if (!digitsOnly.StartsWith("0")) return false;
            return Regex.IsMatch(phoneNumber, @"^[\d\s\-]+$");
        }


        // === CÁC HÀM NGHIỆP VỤ GỌI QUA DAO ===

        /// <summary>
        /// Lấy danh sách tất cả phụ huynh.
        /// </summary>
        public List<PhuHuynhDTO> GetAllPhuHuynh()
        {
            try
            {
                return phuHuynhDAO.LayDanhSachPhuHuynh();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetAllPhuHuynh: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Thêm phụ huynh mới (đã kiểm tra trùng SĐT và Email).
        /// </summary>
        public bool AddPhuHuynh(PhuHuynhDTO ph)
        {
            List<string> errors;
            if (!ValidatePhuHuynh(ph, out errors))
            {
                throw new ArgumentException("Dữ liệu phụ huynh không hợp lệ:\n" + string.Join("\n", errors));
            }

            try
            {
                // === KIỂM TRA TRÙNG LẶP KHI THÊM MỚI ===
                // Gọi hàm kiểm tra, KHÔNG truyền tham số thứ 2 (để nó dùng giá trị mặc định là 0)
                if (!string.IsNullOrWhiteSpace(ph.SoDienThoai) && phuHuynhDAO.KiemTraTrungSdt(ph.SoDienThoai))
                {
                    throw new ArgumentException($"Số điện thoại '{ph.SoDienThoai}' đã tồn tại.");
                }

                if (!string.IsNullOrWhiteSpace(ph.Email) && phuHuynhDAO.KiemTraTrungEmail(ph.Email))
                {
                    throw new ArgumentException($"Email '{ph.Email}' đã tồn tại.");
                }

                return phuHuynhDAO.ThemPhuHuynh(ph);
            }
            catch (ArgumentException) { throw; } // Ném lại lỗi validation
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL AddPhuHuynh: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin phụ huynh (đã kiểm tra trùng SĐT và Email).
        /// </summary>
        public bool UpdatePhuHuynh(PhuHuynhDTO ph)
        {
            List<string> errors;
            if (!ValidatePhuHuynh(ph, out errors))
            {
                throw new ArgumentException("Dữ liệu phụ huynh không hợp lệ:\n" + string.Join("\n", errors));
            }

            if (!phuHuynhDAO.KiemTraTonTai(ph.MaPhuHuynh))
            {
                throw new ArgumentException($"Không tìm thấy phụ huynh với mã {ph.MaPhuHuynh} để cập nhật.");
            }

            try
            {
                // === KIỂM TRA TRÙNG LẶP KHI CẬP NHẬT ===
                // Gọi hàm kiểm tra, TRUYỀN tham số thứ 2 là MaPhuHuynh để loại trừ chính nó
                if (!string.IsNullOrWhiteSpace(ph.SoDienThoai) && phuHuynhDAO.KiemTraTrungSdt(ph.SoDienThoai, ph.MaPhuHuynh))
                {
                    throw new ArgumentException($"Số điện thoại '{ph.SoDienThoai}' đã tồn tại cho một người khác.");
                }

                if (!string.IsNullOrWhiteSpace(ph.Email) && phuHuynhDAO.KiemTraTrungEmail(ph.Email, ph.MaPhuHuynh))
                {
                    throw new ArgumentException($"Email '{ph.Email}' đã tồn tại cho một người khác.");
                }

                return phuHuynhDAO.CapNhatPhuHuynh(ph);
            }
            catch (ArgumentException) { throw; } // Ném lại lỗi validation
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL UpdatePhuHuynh: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Xóa phụ huynh.
        /// </summary>
        public bool DeletePhuHuynh(int maPH)
        {
            if (!phuHuynhDAO.KiemTraTonTai(maPH))
            {
                throw new ArgumentException($"Không tìm thấy phụ huynh với mã {maPH} để xóa.");
            }
            // Cảnh báo: Xóa phụ huynh có thể ảnh hưởng đến bảng HocSinh_PhuHuynh
            // Cân nhắc logic nghiệp vụ (ví dụ: không cho xóa nếu đang liên kết với học sinh?)
            try
            {
                return phuHuynhDAO.XoaPhuHuynh(maPH);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL DeletePhuHuynh: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Tìm phụ huynh theo mã.
        /// </summary>
        public PhuHuynhDTO GetPhuHuynhById(int maPH)
        {
            try
            {
                return phuHuynhDAO.TimPhuHuynhTheoMa(maPH);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetPhuHuynhById: " + ex.Message);
                throw;
            }
        }

        // Bạn có thể thêm các hàm BLL khác nếu cần
        // Ví dụ: Tìm phụ huynh theo tên, SĐT
        // public List<PhuHuynhDTO> SearchPhuHuynhByName(string name) { ... }
        // Lấy phụ huynh theo SĐT (nếu có)
        public PhuHuynhDTO GetPhuHuynhBySdt(string sdt)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sdt)) return null;
                return phuHuynhDAO.TimPhuHuynhTheoSoDienThoai(sdt.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetPhuHuynhBySdt: " + ex.Message);
                throw;
            }
        }
    }
}