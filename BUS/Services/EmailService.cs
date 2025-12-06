using System;
using System.Net;
using System.Net.Mail;

namespace Student_Management_System_CSharp_SGU2025.BUS.Services
{
    /// <summary>
    /// Service gửi email qua SMTP (Gmail)
    /// </summary>
    public class EmailService
    {
        // Cấu hình SMTP Gmail
        private const string SMTP_HOST = "smtp.gmail.com";
        private const int SMTP_PORT = 587;
        private const bool USE_SSL = true;

        // TODO: Thay đổi thông tin email của bạn
        // Lưu ý: Cần sử dụng App Password, không phải mật khẩu Gmail thông thường
        // Hướng dẫn tạo App Password: https://support.google.com/accounts/answer/185833
        private string senderEmail;
        private string senderPassword;
        private string senderName;

        /// <summary>
        /// Khởi tạo EmailService với thông tin email gửi
        /// </summary>
        /// <param name="email">Email gửi (Gmail)</param>
        /// <param name="password">App Password của Gmail</param>
        /// <param name="name">Tên người gửi (hiển thị trong email)</param>
        public EmailService(string email, string password, string name = "THPT TTPT")
        {
            this.senderEmail = email;
            this.senderPassword = password;
            this.senderName = name;
        }

        /// <summary>
        /// Gửi email với nội dung tùy chỉnh
        /// </summary>
        /// <param name="toEmail">Email người nhận</param>
        /// <param name="subject">Tiêu đề email</param>
        /// <param name="body">Nội dung email (có thể dùng HTML)</param>
        /// <param name="isHtml">Có phải HTML không</param>
        /// <returns>True nếu gửi thành công, False nếu thất bại</returns>
        public bool GuiEmail(string toEmail, string subject, string body, bool isHtml = true)
        {
            try
            {
                Console.WriteLine($"[EmailService] Đang chuẩn bị gửi email đến: {toEmail}");
                Console.WriteLine($"[EmailService] Tiêu đề: {subject}");

                // Tạo email message
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail, senderName);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = isHtml;
                    mail.Priority = MailPriority.High;

                    // Cấu hình SMTP client
                    using (SmtpClient smtp = new SmtpClient(SMTP_HOST, SMTP_PORT))
                    {
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = USE_SSL;
                        smtp.Timeout = 20000; // 20 giây

                        Console.WriteLine($"[EmailService] Đang gửi email qua SMTP {SMTP_HOST}:{SMTP_PORT}...");
                        
                        // Gửi email
                        smtp.Send(mail);

                        Console.WriteLine($"[EmailService] ✅ Gửi email thành công đến {toEmail}");
                        return true;
                    }
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"[EmailService] ❌ Lỗi SMTP: {smtpEx.StatusCode} - {smtpEx.Message}");
                Console.WriteLine($"[EmailService] Chi tiết: {smtpEx.StackTrace}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmailService] ❌ Lỗi gửi email: {ex.Message}");
                Console.WriteLine($"[EmailService] Chi tiết: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Gửi mã OTP qua email với template đẹp
        /// </summary>
        /// <param name="toEmail">Email người nhận</param>
        /// <param name="tenNguoiDung">Tên người dùng</param>
        /// <param name="otpCode">Mã OTP (6 chữ số)</param>
        /// <returns>True nếu gửi thành công</returns>
        public bool GuiOTP(string toEmail, string tenNguoiDung, string otpCode)
        {
            Console.WriteLine($"[EmailService] Tạo email OTP cho {tenNguoiDung}");

            string subject = "Mã OTP khôi phục mật khẩu - THPT TTPT";
            
            string body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(135deg, #1565C0 0%, #1976D2 100%);
            color: white;
            padding: 30px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 40px 30px;
        }}
        .otp-box {{
            background: #f8f9fa;
            border: 2px dashed #1976D2;
            border-radius: 8px;
            padding: 20px;
            text-align: center;
            margin: 20px 0;
        }}
        .otp-code {{
            font-size: 36px;
            font-weight: bold;
            color: #1565C0;
            letter-spacing: 8px;
            font-family: 'Courier New', monospace;
        }}
        .warning {{
            background: #fff3cd;
            border-left: 4px solid #ffc107;
            padding: 15px;
            margin: 20px 0;
            border-radius: 4px;
        }}
        .footer {{
            background: #f8f9fa;
            padding: 20px;
            text-align: center;
            color: #6c757d;
            font-size: 12px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>🔐 Khôi phục mật khẩu</h1>
            <p>Hệ thống Quản lý Học sinh - THPT TTPT</p>
        </div>
        <div class='content'>
            <p>Xin chào <strong>{tenNguoiDung}</strong>,</p>
            <p>Bạn đã yêu cầu khôi phục mật khẩu cho tài khoản của mình. Đây là mã OTP của bạn:</p>
            
            <div class='otp-box'>
                <p style='margin: 0; color: #6c757d; font-size: 14px;'>MÃ OTP CỦA BẠN</p>
                <div class='otp-code'>{otpCode}</div>
                <p style='margin: 10px 0 0 0; color: #6c757d; font-size: 12px;'>Mã có hiệu lực trong <strong>10 phút</strong></p>
            </div>

            <div class='warning'>
                <strong>⚠️ Lưu ý:</strong>
                <ul style='margin: 10px 0 0 0; padding-left: 20px;'>
                    <li>Không chia sẻ mã OTP này với bất kỳ ai</li>
                    <li>Nếu bạn không yêu cầu khôi phục mật khẩu, vui lòng bỏ qua email này</li>
                    <li>Mã OTP chỉ được sử dụng một lần</li>
                </ul>
            </div>

            <p style='margin-top: 30px;'>Nếu bạn cần hỗ trợ, vui lòng liên hệ phòng IT của trường.</p>
            <p style='margin-top: 30px;'>Trân trọng,<br><strong>Ban Quản trị Hệ thống</strong></p>
        </div>
        <div class='footer'>
            <p>© 2025 THPT TTPT - Hệ thống Quản lý Học sinh</p>
            <p>Email này được gửi tự động, vui lòng không trả lời.</p>
        </div>
    </div>
</body>
</html>";

            return GuiEmail(toEmail, subject, body, true);
        }

        /// <summary>
        /// Gửi thông báo đổi mật khẩu thành công
        /// </summary>
        public bool GuiThongBaoDoiMatKhauThanhCong(string toEmail, string tenNguoiDung)
        {
            string subject = "Mật khẩu đã được thay đổi - THPT TTPT";
            
            string body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(135deg, #2e7d32 0%, #43a047 100%);
            color: white;
            padding: 30px;
            text-align: center;
        }}
        .content {{
            padding: 40px 30px;
        }}
        .success-icon {{
            text-align: center;
            font-size: 64px;
            margin: 20px 0;
        }}
        .footer {{
            background: #f8f9fa;
            padding: 20px;
            text-align: center;
            color: #6c757d;
            font-size: 12px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>✅ Đổi mật khẩu thành công</h1>
        </div>
        <div class='content'>
            <div class='success-icon'>🎉</div>
            <p>Xin chào <strong>{tenNguoiDung}</strong>,</p>
            <p>Mật khẩu của bạn đã được thay đổi thành công vào lúc <strong>{DateTime.Now:dd/MM/yyyy HH:mm:ss}</strong>.</p>
            <p>Bạn có thể đăng nhập vào hệ thống bằng mật khẩu mới.</p>
            <p style='margin-top: 30px; padding: 15px; background: #fff3cd; border-left: 4px solid #ffc107; border-radius: 4px;'>
                <strong>⚠️ Nếu bạn không thực hiện thay đổi này:</strong><br>
                Vui lòng liên hệ ngay với phòng IT để được hỗ trợ.
            </p>
            <p style='margin-top: 30px;'>Trân trọng,<br><strong>Ban Quản trị Hệ thống</strong></p>
        </div>
        <div class='footer'>
            <p>© 2025 THPT TTPT - Hệ thống Quản lý Học sinh</p>
        </div>
    </div>
</body>
</html>";

            return GuiEmail(toEmail, subject, body, true);
        }
    }
}
