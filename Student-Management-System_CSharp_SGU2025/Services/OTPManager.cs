using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.Services
{
    /// <summary>
    /// Quản lý OTP (One-Time Password) cho khôi phục mật khẩu
    /// </summary>
    public class OTPManager
    {
        // Dictionary lưu trữ OTP: Key = Username, Value = (OTP Code, Expiry Time)
        private static Dictionary<string, (string otpCode, DateTime expiryTime)> otpStorage 
            = new Dictionary<string, (string, DateTime)>();

        // Thời gian hiệu lực của OTP (phút)
        private const int OTP_VALIDITY_MINUTES = 10;

        // Độ dài mã OTP
        private const int OTP_LENGTH = 6;

        /// <summary>
        /// Tạo mã OTP ngẫu nhiên 6 chữ số
        /// </summary>
        /// <returns>Chuỗi OTP 6 chữ số</returns>
        private static string TaoMaOTP()
        {
            Random random = new Random();
            string otp = "";
            
            for (int i = 0; i < OTP_LENGTH; i++)
            {
                otp += random.Next(0, 10).ToString();
            }

            Console.WriteLine($"[OTPManager] Đã tạo mã OTP: {otp}");
            return otp;
        }

        /// <summary>
        /// Tạo và lưu trữ OTP mới cho username
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <returns>Mã OTP được tạo</returns>
        public static string TaoOTPChoNguoiDung(string username)
        {
            try
            {
                Console.WriteLine($"[OTPManager] Tạo OTP cho user: {username}");

                // Xóa OTP cũ nếu có
                if (otpStorage.ContainsKey(username))
                {
                    Console.WriteLine($"[OTPManager] Xóa OTP cũ của {username}");
                    otpStorage.Remove(username);
                }

                // Tạo OTP mới
                string otpCode = TaoMaOTP();
                DateTime expiryTime = DateTime.Now.AddMinutes(OTP_VALIDITY_MINUTES);

                // Lưu vào storage
                otpStorage[username] = (otpCode, expiryTime);

                Console.WriteLine($"[OTPManager] ✅ OTP cho {username}: {otpCode}");
                Console.WriteLine($"[OTPManager] Hết hạn lúc: {expiryTime:HH:mm:ss dd/MM/yyyy}");

                return otpCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[OTPManager] ❌ Lỗi tạo OTP: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Xác thực OTP của username
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <param name="inputOTP">Mã OTP người dùng nhập</param>
        /// <returns>True nếu OTP đúng và còn hiệu lực, False nếu sai hoặc hết hạn</returns>
        public static bool XacThucOTP(string username, string inputOTP)
        {
            try
            {
                Console.WriteLine($"[OTPManager] Xác thực OTP cho user: {username}");
                Console.WriteLine($"[OTPManager] OTP nhập vào: {inputOTP}");

                // Kiểm tra có OTP cho username này không
                if (!otpStorage.ContainsKey(username))
                {
                    Console.WriteLine($"[OTPManager] ❌ Không tìm thấy OTP cho {username}");
                    return false;
                }

                // Lấy OTP đã lưu
                var (storedOTP, expiryTime) = otpStorage[username];

                // Kiểm tra OTP có hết hạn không
                if (DateTime.Now > expiryTime)
                {
                    Console.WriteLine($"[OTPManager] ❌ OTP đã hết hạn lúc {expiryTime:HH:mm:ss}");
                    otpStorage.Remove(username); // Xóa OTP hết hạn
                    return false;
                }

                // Kiểm tra OTP có khớp không
                if (storedOTP != inputOTP)
                {
                    Console.WriteLine($"[OTPManager] ❌ OTP không khớp. Đúng: {storedOTP}, Nhập: {inputOTP}");
                    return false;
                }

                Console.WriteLine($"[OTPManager] ✅ OTP hợp lệ cho {username}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[OTPManager] ❌ Lỗi xác thực OTP: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa OTP sau khi đã sử dụng thành công
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        public static void XoaOTP(string username)
        {
            try
            {
                if (otpStorage.ContainsKey(username))
                {
                    otpStorage.Remove(username);
                    Console.WriteLine($"[OTPManager] ✅ Đã xóa OTP của {username}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[OTPManager] ❌ Lỗi xóa OTP: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thời gian còn lại của OTP (giây)
        /// </summary>
        /// <param name="username">Tên đăng nhập</param>
        /// <returns>Số giây còn lại, hoặc 0 nếu không có/hết hạn</returns>
        public static int LaySoGiayConLai(string username)
        {
            try
            {
                if (!otpStorage.ContainsKey(username))
                    return 0;

                var (_, expiryTime) = otpStorage[username];
                TimeSpan timeLeft = expiryTime - DateTime.Now;

                return timeLeft.TotalSeconds > 0 ? (int)timeLeft.TotalSeconds : 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Dọn dẹp tất cả OTP hết hạn (gọi định kỳ nếu cần)
        /// </summary>
        public static void DonDepOTPHetHan()
        {
            try
            {
                List<string> expiredKeys = new List<string>();

                foreach (var kvp in otpStorage)
                {
                    if (DateTime.Now > kvp.Value.expiryTime)
                    {
                        expiredKeys.Add(kvp.Key);
                    }
                }

                foreach (var key in expiredKeys)
                {
                    otpStorage.Remove(key);
                    Console.WriteLine($"[OTPManager] Đã xóa OTP hết hạn của {key}");
                }

                if (expiredKeys.Count > 0)
                {
                    Console.WriteLine($"[OTPManager] ✅ Đã dọn dẹp {expiredKeys.Count} OTP hết hạn");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[OTPManager] ❌ Lỗi dọn dẹp OTP: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy số lượng OTP đang lưu trữ (cho debug)
        /// </summary>
        public static int LaySoLuongOTP()
        {
            return otpStorage.Count;
        }
    }
}
