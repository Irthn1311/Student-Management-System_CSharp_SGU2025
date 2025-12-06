using System;
using System.Text.RegularExpressions;

namespace Student_Management_System_CSharp_SGU2025.BUS.Utils
{
    /// <summary>
    /// Helper class để validate dữ liệu đầu vào (Email, Số điện thoại)
    /// </summary>
    public static class ValidationHelper
    {
        #region Email Validation

        /// <summary>
        /// Validate email theo chuẩn RFC 5322 và các quy tắc bổ sung
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <param name="errorMessage">Thông báo lỗi chi tiết (nếu có)</param>
        /// <returns>True nếu email hợp lệ, False nếu không</returns>
        public static bool IsValidEmail(string email, out string errorMessage)
        {
            errorMessage = null;

            // Kiểm tra null hoặc rỗng
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = "Email không được để trống.";
                return false;
            }

            email = email.Trim();

            // Kiểm tra độ dài tổng (≤ 254 ký tự)
            if (email.Length > 254)
            {
                errorMessage = "Email không được vượt quá 254 ký tự.";
                return false;
            }

            // Kiểm tra có dấu @
            int atIndex = email.IndexOf('@');
            if (atIndex <= 0 || atIndex == email.Length - 1)
            {
                errorMessage = "Email phải có dạng local-part@domain.";
                return false;
            }

            // Tách local-part và domain
            string localPart = email.Substring(0, atIndex);
            string domain = email.Substring(atIndex + 1);

            // Validate local-part
            if (!IsValidLocalPart(localPart, out errorMessage))
            {
                return false;
            }

            // Validate domain
            if (!IsValidDomain(domain, out errorMessage))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate local-part của email (phần trước @)
        /// </summary>
        private static bool IsValidLocalPart(string localPart, out string errorMessage)
        {
            errorMessage = null;

            // Kiểm tra độ dài (≤ 64 ký tự)
            if (localPart.Length > 64)
            {
                errorMessage = "Phần trước @ không được vượt quá 64 ký tự.";
                return false;
            }

            // Không được bắt đầu hoặc kết thúc bằng dấu .
            if (localPart.StartsWith(".") || localPart.EndsWith("."))
            {
                errorMessage = "Email không được bắt đầu hoặc kết thúc bằng dấu chấm.";
                return false;
            }

            // Không được có hai dấu .. liên tiếp
            if (localPart.Contains(".."))
            {
                errorMessage = "Email không được chứa hai dấu chấm liên tiếp.";
                return false;
            }

            // Kiểm tra ký tự hợp lệ: chữ cái, số, . _ % + -
            // Không được chứa khoảng trắng và ký tự đặc biệt lạ như / \ ' " * ?
            Regex localPartRegex = new Regex(@"^[a-zA-Z0-9._%+\-]+$");
            if (!localPartRegex.IsMatch(localPart))
            {
                errorMessage = "Phần trước @ chỉ được chứa chữ cái, số và các ký tự . _ % + -";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate domain của email (phần sau @)
        /// </summary>
        private static bool IsValidDomain(string domain, out string errorMessage)
        {
            errorMessage = null;

            // Kiểm tra độ dài (≤ 253 ký tự)
            if (domain.Length > 253)
            {
                errorMessage = "Phần sau @ không được vượt quá 253 ký tự.";
                return false;
            }

            // Không được bắt đầu hoặc kết thúc bằng dấu .
            if (domain.StartsWith(".") || domain.EndsWith("."))
            {
                errorMessage = "Tên miền không được bắt đầu hoặc kết thúc bằng dấu chấm.";
                return false;
            }

            // Không được có hai dấu .. liên tiếp
            if (domain.Contains(".."))
            {
                errorMessage = "Tên miền không được chứa hai dấu chấm liên tiếp.";
                return false;
            }

            // Tách domain thành các phần (subdomain, domain, TLD)
            string[] parts = domain.Split('.');

            // Phải có ít nhất 2 phần (domain.tld)
            if (parts.Length < 2)
            {
                errorMessage = "Tên miền phải có ít nhất một dấu chấm (vd: domain.com).";
                return false;
            }

            // Kiểm tra TLD (phần cuối cùng) phải dài ≥ 2 ký tự
            string tld = parts[parts.Length - 1];
            if (tld.Length < 2)
            {
                errorMessage = "Phần mở rộng tên miền (TLD) phải có ít nhất 2 ký tự (vd: .com, .vn).";
                return false;
            }

            // Kiểm tra từng phần của domain
            Regex domainPartRegex = new Regex(@"^[a-zA-Z0-9]([a-zA-Z0-9\-]*[a-zA-Z0-9])?$");
            foreach (string part in parts)
            {
                if (string.IsNullOrEmpty(part))
                {
                    errorMessage = "Tên miền không được chứa phần rỗng.";
                    return false;
                }

                // Không được bắt đầu hoặc kết thúc bằng dấu -
                if (!domainPartRegex.IsMatch(part))
                {
                    errorMessage = "Mỗi phần của tên miền chỉ được chứa chữ cái, số và dấu gạch ngang, không được bắt đầu/kết thúc bằng dấu gạch ngang.";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Overload đơn giản không trả về error message
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            string errorMessage;
            return IsValidEmail(email, out errorMessage);
        }

        #endregion

        #region Phone Number Validation

        /// <summary>
        /// Validate số điện thoại Việt Nam và quốc tế theo chuẩn E.164
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại cần kiểm tra</param>
        /// <param name="errorMessage">Thông báo lỗi chi tiết (nếu có)</param>
        /// <returns>True nếu số điện thoại hợp lệ, False nếu không</returns>
        public static bool IsValidPhoneNumber(string phoneNumber, out string errorMessage)
        {
            errorMessage = null;

            // Kiểm tra null hoặc rỗng
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                errorMessage = "Số điện thoại không được để trống.";
                return false;
            }

            phoneNumber = phoneNumber.Trim();

            // Loại bỏ các ký tự không phải số (trừ dấu + ở đầu)
            string cleanedPhone = CleanPhoneNumber(phoneNumber);

            // Kiểm tra số điện thoại Việt Nam
            if (cleanedPhone.StartsWith("0"))
            {
                return IsValidVietnamesePhoneNumber(cleanedPhone, out errorMessage);
            }

            // Kiểm tra số điện thoại quốc tế (bắt đầu bằng +)
            if (cleanedPhone.StartsWith("+"))
            {
                return IsValidInternationalPhoneNumber(cleanedPhone, out errorMessage);
            }

            // Trường hợp không bắt đầu bằng 0 hoặc +
            errorMessage = "Số điện thoại phải bắt đầu bằng 0 (Việt Nam) hoặc + (quốc tế).";
            return false;
        }

        /// <summary>
        /// Validate số điện thoại Việt Nam (bắt đầu bằng 0)
        /// </summary>
        private static bool IsValidVietnamesePhoneNumber(string phoneNumber, out string errorMessage)
        {
            errorMessage = null;

            // Phải có đúng 10 chữ số
            if (phoneNumber.Length != 10)
            {
                errorMessage = "Số điện thoại Việt Nam phải có đúng 10 chữ số.";
                return false;
            }

            // Kiểm tra đầu số hợp lệ của các nhà mạng Việt Nam (2 chữ số đầu)
            string prefix = phoneNumber.Substring(0, 2);
            string[] validPrefixes = { "03", "05", "07", "08", "09" };

            bool isValidPrefix = false;
            foreach (string validPrefix in validPrefixes)
            {
                if (prefix == validPrefix)
                {
                    isValidPrefix = true;
                    break;
                }
            }

            if (!isValidPrefix)
            {
                errorMessage = "Đầu số điện thoại không hợp lệ. Đầu số hợp lệ: 03, 05, 07, 08, 09.";
                return false;
            }

            // Kiểm tra chỉ chứa ký tự số
            Regex digitRegex = new Regex(@"^\d+$");
            if (!digitRegex.IsMatch(phoneNumber))
            {
                errorMessage = "Số điện thoại chỉ được chứa các chữ số.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate số điện thoại quốc tế theo chuẩn E.164 (bắt đầu bằng +)
        /// </summary>
        private static bool IsValidInternationalPhoneNumber(string phoneNumber, out string errorMessage)
        {
            errorMessage = null;

            // Phải bắt đầu bằng +
            if (!phoneNumber.StartsWith("+"))
            {
                errorMessage = "Số điện thoại quốc tế phải bắt đầu bằng dấu +.";
                return false;
            }

            // Loại bỏ dấu + để kiểm tra phần còn lại
            string digits = phoneNumber.Substring(1);

            // Kiểm tra chỉ chứa ký tự số
            Regex digitRegex = new Regex(@"^\d+$");
            if (!digitRegex.IsMatch(digits))
            {
                errorMessage = "Số điện thoại quốc tế chỉ được chứa dấu + và các chữ số.";
                return false;
            }

            // Độ dài phải từ 8-15 chữ số (không tính dấu +)
            if (digits.Length < 8 || digits.Length > 15)
            {
                errorMessage = "Số điện thoại quốc tế phải có từ 8-15 chữ số (sau dấu +).";
                return false;
            }

            // ✅ Kiểm tra trường hợp đặc biệt: số điện thoại Việt Nam với +84
            if (phoneNumber.StartsWith("+84"))
            {
                string vietnameseNumber = phoneNumber.Substring(3); // Bỏ +84

                // Phải có đúng 9 chữ số (loại bỏ số 0 đầu)
                if (vietnameseNumber.Length != 9)
                {
                    errorMessage = "Số điện thoại Việt Nam với +84 phải có 9 chữ số (không có số 0 đầu).";
                    return false;
                }

                // ✅ Kiểm tra đầu số hợp lệ (2 chữ số đầu sau +84)
                if (vietnameseNumber.Length < 2)
                {
                    errorMessage = "Số điện thoại Việt Nam không hợp lệ.";
                    return false;
                }

                string prefix = vietnameseNumber.Substring(0, 2);
                string[] validPrefixes = { "32", "33", "34", "35", "36", "37", "38", "39",
                                          "52", "53", "54", "55", "56", "57", "58", "59",
                                          "70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
                                          "81", "82", "83", "84", "85", "86", "87", "88", "89",
                                          "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" };

                bool isValidPrefix = false;
                foreach (string validPrefix in validPrefixes)
                {
                    if (prefix == validPrefix)
                    {
                        isValidPrefix = true;
                        break;
                    }
                }

                if (!isValidPrefix)
                {
                    errorMessage = $"Đầu số điện thoại Việt Nam không hợp lệ. Sau +84 phải là: 3x, 5x, 7x, 8x hoặc 9x (x là 0-9).";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Làm sạch số điện thoại (loại bỏ khoảng trắng, dấu gạch ngang)
        /// </summary>
        private static string CleanPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return "";

            // Giữ lại dấu + ở đầu (nếu có) và các chữ số
            string cleaned = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

            return cleaned;
        }

        /// <summary>
        /// Overload đơn giản không trả về error message
        /// </summary>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string errorMessage;
            return IsValidPhoneNumber(phoneNumber, out errorMessage);
        }

        /// <summary>
        /// Chuẩn hóa số điện thoại để lưu vào database (loại bỏ ký tự đặc biệt)
        /// </summary>
        public static string NormalizePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return "";

            return CleanPhoneNumber(phoneNumber);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Validate và trả về thông báo lỗi chi tiết cho UI
        /// </summary>
        public static bool ValidateEmailWithMessage(string email, out string errorMessage)
        {
            return IsValidEmail(email, out errorMessage);
        }

        /// <summary>
        /// Validate và trả về thông báo lỗi chi tiết cho UI
        /// </summary>
        public static bool ValidatePhoneNumberWithMessage(string phoneNumber, out string errorMessage)
        {
            return IsValidPhoneNumber(phoneNumber, out errorMessage);
        }

        #endregion

        /// <summary>
        /// Chuyển đổi số điện thoại quốc tế +84 về định dạng Việt Nam 0xxx
        /// </summary>
        public static string ConvertToVietnameseFormat(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber;

            string cleaned = CleanPhoneNumber(phoneNumber);

            // Nếu là số +84, chuyển về 0
            if (cleaned.StartsWith("+84"))
            {
                return "0" + cleaned.Substring(3); // +84914842533 → 0914842533
            }

            return cleaned;
        }

    }
}