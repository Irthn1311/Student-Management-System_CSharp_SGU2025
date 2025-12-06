using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.BUS.Services;
using Student_Management_System_CSharp_SGU2025.BUS;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmXacThucOTP : Form
    {
        private string tenDangNhap;
        private string email;
        private Timer countdownTimer;
        private int secondsLeft;
        private EmailService emailService;

        public FrmXacThucOTP(string tenDangNhap, string email, EmailService emailService)
        {
            InitializeComponent();
            
            this.tenDangNhap = tenDangNhap;
            this.email = email;
            this.emailService = emailService;

            // Cấu hình form
            this.Text = "Xác thực OTP - THPT TTPT";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Cấu hình txtOTP
            txtOTP.MaxLength = 6;
            txtOTP.Font = new Font("Courier New", 14, FontStyle.Bold);
            txtOTP.TextAlign = HorizontalAlignment.Center;

            // Gắn sự kiện
            this.Load += FrmXacThucOTP_Load;
            btnXacNhan.Click += BtnXacNhan_Click;
            btnGuiLai.Click += BtnGuiLai_Click;
            txtOTP.KeyPress += TxtOTP_KeyPress;

            // Khởi tạo timer đếm ngược
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // 1 giây
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        private void FrmXacThucOTP_Load(object sender, EventArgs e)
        {
            Console.WriteLine($"[FrmXacThucOTP] Form load cho user: {tenDangNhap}");
            
            // Bắt đầu đếm ngược
            secondsLeft = OTPManager.LaySoGiayConLai(tenDangNhap);
            if (secondsLeft <= 0)
            {
                secondsLeft = 600; // 10 phút = 600 giây (backup nếu không lấy được)
            }
            
            countdownTimer.Start();
            CapNhatHienThiThoiGian();
            
            // Focus vào textbox OTP
            txtOTP.Focus();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            secondsLeft--;
            
            if (secondsLeft <= 0)
            {
                countdownTimer.Stop();
                MessageBox.Show(
                    "Mã OTP đã hết hạn!\nVui lòng nhấn 'Gửi lại' để nhận mã mới.",
                    "Hết hạn",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                
                txtOTP.Enabled = false;
                btnXacNhan.Enabled = false;
            }
            else
            {
                CapNhatHienThiThoiGian();
            }
        }

        private void CapNhatHienThiThoiGian()
        {
            int minutes = secondsLeft / 60;
            int seconds = secondsLeft % 60;
            
            lblTimeLeft.Text = $"⏱️ Thời gian còn lại: {minutes:D2}:{seconds:D2}";
            lblTimeLeft.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTimeLeft.ForeColor = secondsLeft < 60 ? Color.Red : Color.FromArgb(21, 101, 192);
        }

        private void TxtOTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và các phím điều khiển
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Nhấn Enter để xác nhận
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnXacNhan_Click(sender, e);
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                string otpInput = txtOTP.Text.Trim();

                // Validate input
                if (string.IsNullOrWhiteSpace(otpInput))
                {
                    MessageBox.Show(
                        "Vui lòng nhập mã OTP!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtOTP.Focus();
                    return;
                }

                if (otpInput.Length != 6)
                {
                    MessageBox.Show(
                        "Mã OTP phải có 6 chữ số!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtOTP.Focus();
                    return;
                }

                Console.WriteLine($"[FrmXacThucOTP] Xác thực OTP: {otpInput} cho user: {tenDangNhap}");

                // Xác thực OTP
                bool isValid = OTPManager.XacThucOTP(tenDangNhap, otpInput);

                if (isValid)
                {
                    Console.WriteLine($"[FrmXacThucOTP] ✅ OTP hợp lệ");
                    
                    countdownTimer.Stop();
                    
                    // Xóa OTP đã sử dụng
                    OTPManager.XoaOTP(tenDangNhap);
                    
                    MessageBox.Show(
                        "Xác thực thành công!\nBạn có thể đổi mật khẩu mới.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Mở form đổi mật khẩu
                    this.Hide();
                    FrmDoiMatKhau frmDoiMatKhau = new FrmDoiMatKhau(tenDangNhap, email, emailService);
                    frmDoiMatKhau.FormClosed += (s, args) => this.Close();
                    frmDoiMatKhau.ShowDialog();
                }
                else
                {
                    Console.WriteLine($"[FrmXacThucOTP] ❌ OTP không hợp lệ");
                    
                    MessageBox.Show(
                        "Mã OTP không đúng hoặc đã hết hạn!\nVui lòng kiểm tra lại hoặc nhấn 'Gửi lại'.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    
                    txtOTP.Clear();
                    txtOTP.Focus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FrmXacThucOTP] ❌ Lỗi xác thực: {ex.Message}");
                MessageBox.Show(
                    $"Đã xảy ra lỗi khi xác thực:\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnGuiLai_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có muốn gửi lại mã OTP mới không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                Console.WriteLine($"[FrmXacThucOTP] Gửi lại OTP cho user: {tenDangNhap}");

                // Disable nút gửi lại tạm thời
                btnGuiLai.Enabled = false;
                btnGuiLai.Text = "Đang gửi...";

                // Tạo OTP mới
                string newOTP = OTPManager.TaoOTPChoNguoiDung(tenDangNhap);

                // Gửi email
                bool success = emailService.GuiOTP(email, tenDangNhap, newOTP);

                if (success)
                {
                    Console.WriteLine($"[FrmXacThucOTP] ✅ Gửi lại OTP thành công");
                    
                    MessageBox.Show(
                        $"Mã OTP mới đã được gửi đến email:\n{MaskEmail(email)}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Reset thời gian đếm ngược
                    secondsLeft = 600; // 10 phút
                    countdownTimer.Start();
                    
                    // Enable lại các control
                    txtOTP.Enabled = true;
                    btnXacNhan.Enabled = true;
                    txtOTP.Clear();
                    txtOTP.Focus();
                }
                else
                {
                    Console.WriteLine($"[FrmXacThucOTP] ❌ Gửi email thất bại");
                    
                    MessageBox.Show(
                        "Không thể gửi email!\nVui lòng kiểm tra kết nối mạng và thử lại.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FrmXacThucOTP] ❌ Lỗi gửi lại OTP: {ex.Message}");
                MessageBox.Show(
                    $"Đã xảy ra lỗi khi gửi lại OTP:\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Enable lại nút gửi lại
                btnGuiLai.Enabled = true;
                btnGuiLai.Text = "Gửi lại";
            }
        }

        private string MaskEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return email;

            int atIndex = email.IndexOf('@');
            string localPart = email.Substring(0, atIndex);
            string domainPart = email.Substring(atIndex);

            if (localPart.Length <= 1)
                return email;

            return localPart[0] + "**" + domainPart;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            countdownTimer?.Stop();
            countdownTimer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
