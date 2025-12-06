using Student_Management_System_CSharp_SGU2025.BUS; // Thêm để sử dụng LoginBUS
using Student_Management_System_CSharp_SGU2025.GUI; // Thêm để sử dụng FrmXacThucOTP
using Student_Management_System_CSharp_SGU2025.GUI.DangNhap; // Thêm để sử dụng FrmLienHeIT
using Student_Management_System_CSharp_SGU2025.BUS.Services; // Thêm để sử dụng EmailService và OTPManager
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmDangNhap : Form
    {
        private LoginBUS loginBUS;
        private HocSinhBLL hocSinhBLL;

        public FrmDangNhap()
        {
            InitializeComponent();
            loginBUS = new LoginBUS(); // Khởi tạo LoginBUS
            hocSinhBLL = new HocSinhBLL(); // Khởi tạo HocSinhBLL
            loginBUS = new LoginBUS(); // Khởi tạo LoginBUS
            hocSinhBLL = new HocSinhBLL(); // Khởi tạo HocSinhBLL
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            // Cài đặt ban đầu khi form load
            txtMatKhau.PasswordChar = '●'; // Ẩn mật khẩu
            txtTenDangNhap.Focus(); // Focus vào ô tên đăng nhập
            
            // Cho phép nhấn Enter để đăng nhập
            this.AcceptButton = btnDangNhap;


        }

        private void lbTenDangNhap_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ textbox
                string tenDangNhap = txtTenDangNhap.Text.Trim();
                string matKhau = txtMatKhau.Text.Trim();

                // Kiểm tra rỗng
                if (string.IsNullOrWhiteSpace(tenDangNhap))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(matKhau))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                }

                // Kiểm tra đăng nhập qua LoginBUS
                bool ketQua = loginBUS.KiemTraDangNhap(tenDangNhap, matKhau);

                if (ketQua)
                {
                    try
                    {
                        // ✅ LẤY THÔNG TIN ĐẦY ĐỦ
                        NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
                        PhanQuyenBUS phanQuyenBUS = new PhanQuyenBUS();

                        // Lấy thông tin người dùng
                        var nguoiDung = nguoiDungBLL.GetNguoiDungByTenDangNhap(tenDangNhap);

                        string hoTen = tenDangNhap;
                        string email = "";
                        string vaiTro = "Người dùng";
                        List<string> danhSachMaVaiTro = new List<string>();

                        if (nguoiDung != null)
                        {
                            // ✅ LẤY TÊN VAI TRÒ ĐỂ HIỂN THỊ
                            vaiTro = !string.IsNullOrEmpty(nguoiDung.VaiTro)
                                ? nguoiDung.VaiTro
                                : "Người dùng";

                            Console.WriteLine($"[DEBUG] VaiTro từ DB: {vaiTro}");
                        }

                        // Lấy hồ sơ người dùng (nếu có)
                        var hoSo = nguoiDungBLL.GetHoSoByTenDangNhap(tenDangNhap);
                        if (hoSo != null)
                        {
                            hoTen = !string.IsNullOrEmpty(hoSo.HoTen) ? hoSo.HoTen : tenDangNhap;
                            email = hoSo.Email ?? "";
                            Console.WriteLine($"[DEBUG] HoTen từ HoSo: {hoTen}");
                        }

                        // Lấy danh sách MaVaiTro để phân quyền
                        danhSachMaVaiTro = phanQuyenBUS.GetVaiTroByNguoiDung(tenDangNhap);

                        // ✅ LƯU VÀO SESSION MANAGER
                        SessionManager.Login(tenDangNhap, hoTen, email, vaiTro, danhSachMaVaiTro);

                        Console.WriteLine($"[SUCCESS] Đăng nhập - User: {tenDangNhap}, Họ tên: {hoTen}, Vai trò: {vaiTro}");
                    }
                    catch (Exception sessionEx)
                    {
                        Console.WriteLine($"[WARNING] Lỗi khi tạo session: {sessionEx.Message}");
                        // Vẫn cho phép đăng nhập nhưng với thông tin mặc định
                        SessionManager.Login(tenDangNhap, tenDangNhap, "", "Người dùng", new List<string>());
                    }

                    // Đăng nhập thành công
                    MessageBox.Show($"Đăng nhập thành công!\nChào mừng: {SessionManager.HoTen}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Ẩn form đăng nhập
                    this.Hide();

                    // Mở MainForm
                    MainForm mainForm = new MainForm();
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    // Đăng nhập thất bại
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!\nHoặc tài khoản đã bị khóa.",
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi đăng nhập:\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Console.WriteLine("Lỗi đăng nhập: " + ex.Message);
            }
            
        }



        private void lbChaoMung2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void linkLbLienHeIT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void cbGhiNhoDangNhap_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLbQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Console.WriteLine("[INFO] Người dùng click vào 'Quên mật khẩu'");

                // Tạo form đơn giản để nhập tên đăng nhập
                using (Form inputForm = new Form())
                {
                    inputForm.Text = "Quên mật khẩu";
                    inputForm.Size = new Size(400, 180);
                    inputForm.StartPosition = FormStartPosition.CenterParent;
                    inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    inputForm.MaximizeBox = false;
                    inputForm.MinimizeBox = false;

                    Label lblPrompt = new Label();
                    lblPrompt.Text = "Vui lòng nhập tên đăng nhập của bạn:";
                    lblPrompt.Location = new Point(20, 20);
                    lblPrompt.Size = new Size(350, 20);

                    TextBox txtInput = new TextBox();
                    txtInput.Location = new Point(20, 50);
                    txtInput.Size = new Size(340, 25);

                    Button btnOK = new Button();
                    btnOK.Text = "Xác nhận";
                    btnOK.Location = new Point(180, 90);
                    btnOK.Size = new Size(90, 30);
                    btnOK.DialogResult = DialogResult.OK;

                    Button btnCancel = new Button();
                    btnCancel.Text = "Hủy";
                    btnCancel.Location = new Point(280, 90);
                    btnCancel.Size = new Size(80, 30);
                    btnCancel.DialogResult = DialogResult.Cancel;

                    inputForm.Controls.AddRange(new Control[] { lblPrompt, txtInput, btnOK, btnCancel });
                    inputForm.AcceptButton = btnOK;
                    inputForm.CancelButton = btnCancel;

                    // Hiển thị form và kiểm tra kết quả
                    if (inputForm.ShowDialog() != DialogResult.OK)
                    {
                        Console.WriteLine("[INFO] Người dùng hủy thao tác khôi phục mật khẩu");
                        return;
                    }

                    string tenDangNhap = txtInput.Text.Trim();

                    // Kiểm tra nếu người dùng không nhập gì
                    if (string.IsNullOrWhiteSpace(tenDangNhap))
                    {
                        Console.WriteLine("[INFO] Tên đăng nhập trống");
                        MessageBox.Show(
                            "Vui lòng nhập tên đăng nhập!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    Console.WriteLine($"[DEBUG] Tên đăng nhập nhập vào: {tenDangNhap}");

                    // Kiểm tra tên đăng nhập có tồn tại không
                    var taiKhoan = loginBUS.LayNguoiDungTheoTen(tenDangNhap);
                    if (!taiKhoan.HasValue)
                    {
                        Console.WriteLine($"[WARNING] Tên đăng nhập '{tenDangNhap}' không tồn tại");
                        MessageBox.Show(
                            "Tên đăng nhập không tồn tại trong hệ thống!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    Console.WriteLine($"[SUCCESS] Tìm thấy tài khoản: {tenDangNhap}");

                    // Kiểm tra nếu là học sinh (tên đăng nhập bắt đầu bằng "HS")
                    if (tenDangNhap.StartsWith("HS", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("[INFO] Đây là tài khoản học sinh");
                        
                        // Trích xuất mã học sinh từ tên đăng nhập (VD: HS101 -> 101)
                        string maHocSinhStr = tenDangNhap.Substring(2); // Bỏ 2 ký tự "HS"
                        
                        if (int.TryParse(maHocSinhStr, out int maHocSinh))
                        {
                            Console.WriteLine($"[DEBUG] Mã học sinh: {maHocSinh}");
                            
                            // Gọi BUS để lấy email của học sinh
                            string email = hocSinhBLL.LayEmailTheoMaHocSinh(maHocSinh);
                            
                            if (string.IsNullOrWhiteSpace(email))
                            {
                                Console.WriteLine($"[ERROR] Học sinh {maHocSinh} không có email");
                                MessageBox.Show(
                                    "Tài khoản này chưa có email đăng ký!\n" +
                                    "Vui lòng liên hệ phòng IT để được hỗ trợ.",
                                    "Lỗi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                return;
                            }

                            Console.WriteLine($"[SUCCESS] Tìm thấy email: {email}");
                            
                            // Ẩn một phần email để bảo mật (VD: abc@gmail.com -> a**@gmail.com)
                            string emailMasked = MaskEmail(email);
                            
                            // Xác nhận với người dùng
                            DialogResult confirmResult = MessageBox.Show(
                                $"Hệ thống sẽ gửi mã OTP đến email:\n{emailMasked}\n\n" +
                                $"Bạn có muốn tiếp tục?",
                                "Xác nhận",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (confirmResult != DialogResult.Yes)
                            {
                                Console.WriteLine("[INFO] Người dùng hủy gửi OTP");
                                return;
                            }

                            // ✅ ĐỌC CẤU HÌNH TỪ App.config (AN TOÀN HƠN)
                            // Cấu hình email trong App.config.local (không push lên GitHub)
                            string GMAIL_ADDRESS = ConfigurationManager.AppSettings["GmailAddress"];
                            string GMAIL_APP_PASSWORD = ConfigurationManager.AppSettings["GmailAppPassword"];

                            // Kiểm tra đã cấu hình email chưa
                            if (string.IsNullOrEmpty(GMAIL_ADDRESS) || 
                                string.IsNullOrEmpty(GMAIL_APP_PASSWORD) ||
                                GMAIL_ADDRESS == "your-email@gmail.com" || 
                                GMAIL_APP_PASSWORD == "xxxx xxxx xxxx xxxx")
                            {
                                MessageBox.Show(
                                    "⚠️ Chức năng gửi email chưa được cấu hình!\n\n" +
                                    "Vui lòng:\n" +
                                    "1. Tạo Gmail App Password tại:\n" +
                                    "   https://myaccount.google.com/apppasswords\n\n" +
                                    "2. Mở file App.config.local và cập nhật:\n" +
                                    "   - GmailAddress: Email của bạn\n" +
                                    "   - GmailAppPassword: App Password 16 ký tự\n\n" +
                                    "3. Build lại project và test\n\n" +
                                    "📖 Xem thêm: EMAIL_SECURITY.md",
                                    "Cấu hình Email",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                return;
                            }

                            try
                            {
                                // Khởi tạo EmailService
                                EmailService emailService = new EmailService(
                                    GMAIL_ADDRESS,
                                    GMAIL_APP_PASSWORD,
                                    "THPT TTPT"
                                );

                                Console.WriteLine("[INFO] Đang tạo mã OTP...");
                                
                                // Tạo OTP
                                string otpCode = OTPManager.TaoOTPChoNguoiDung(tenDangNhap);
                                
                                Console.WriteLine("[INFO] Đang gửi email OTP...");
                                
                                // Hiển thị loading
                                this.Cursor = Cursors.WaitCursor;
                                
                                // Gửi OTP qua email
                                bool emailSent = emailService.GuiOTP(email, tenDangNhap, otpCode);
                                
                                this.Cursor = Cursors.Default;

                                if (emailSent)
                                {
                                    Console.WriteLine("[SUCCESS] Gửi OTP thành công");
                                    
                                    MessageBox.Show(
                                        $"✅ Mã OTP đã được gửi đến email:\n{emailMasked}\n\n" +
                                        $"Vui lòng kiểm tra hộp thư và nhập mã OTP.",
                                        "Thành công",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                                    // Mở form xác thực OTP
                                    FrmXacThucOTP frmOTP = new FrmXacThucOTP(tenDangNhap, email, emailService);
                                    frmOTP.ShowDialog();
                                }
                                else
                                {
                                    Console.WriteLine("[ERROR] Gửi email thất bại");
                                    
                                    MessageBox.Show(
                                        "❌ Không thể gửi email!\n\n" +
                                        "Vui lòng kiểm tra:\n" +
                                        "• Kết nối Internet\n" +
                                        "• Cấu hình Gmail SMTP\n" +
                                        "• App Password còn hiệu lực",
                                        "Lỗi gửi email",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception emailEx)
                            {
                                this.Cursor = Cursors.Default;
                                Console.WriteLine($"[ERROR] Lỗi gửi email: {emailEx.Message}");
                                
                                MessageBox.Show(
                                    $"❌ Lỗi gửi email:\n{emailEx.Message}\n\n" +
                                    $"Vui lòng kiểm tra cấu hình SMTP Gmail.",
                                    "Lỗi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[ERROR] Không thể chuyển đổi '{maHocSinhStr}' thành số");
                            MessageBox.Show(
                                "Định dạng tên đăng nhập không hợp lệ!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Tài khoản không phải học sinh (giáo viên, admin, etc.)
                        Console.WriteLine("[INFO] Tài khoản không phải học sinh");
                        MessageBox.Show(
                            "Chức năng khôi phục mật khẩu hiện chỉ hỗ trợ cho học sinh.\n" +
                            "Vui lòng liên hệ phòng IT để được hỗ trợ.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi xử lý quên mật khẩu: {ex.Message}");
                Console.WriteLine($"[ERROR] Stack trace: {ex.StackTrace}");
                MessageBox.Show(
                    $"Đã xảy ra lỗi khi xử lý yêu cầu:\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ẩn một phần email để bảo mật (VD: abc@gmail.com -> a**@gmail.com)
        /// </summary>
        private string MaskEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                return email;

            int atIndex = email.IndexOf('@');
            string localPart = email.Substring(0, atIndex);
            string domainPart = email.Substring(atIndex);

            if (localPart.Length <= 1)
                return email;

            string masked = localPart[0] + "**" + domainPart;
            return masked;
        }

        private void linkLbLienHeIT_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Console.WriteLine("[INFO] Người dùng click 'Liên hệ IT'");
                
                // Mở form liên hệ IT
                FrmLienHeIT frmLienHeIT = new FrmLienHeIT();
                frmLienHeIT.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi mở form liên hệ IT: {ex.Message}");
                MessageBox.Show(
                    $"Không thể mở form liên hệ IT:\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }




    }
}
