using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.Services;

namespace Student_Management_System_CSharp_SGU2025.GUI.DangNhap
{
    public partial class FrmDoiMatKhau : Form
    {
        private string tenDangNhap;
        private string email;
        private LoginBUS loginBUS;
        private EmailService emailService;

        public FrmDoiMatKhau(string tenDangNhap, string email, EmailService emailService)
        {
            InitializeComponent();
            
            this.tenDangNhap = tenDangNhap;
            this.email = email;
            this.emailService = emailService;
            this.loginBUS = new LoginBUS();

            // Cấu hình form
            this.Text = "Đổi mật khẩu - THPT TTPT";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Cấu hình textbox
            txtMatKhauMoi.PasswordChar = '●';
            txtXacNhanMatKhau.PasswordChar = '●';
            txtMatKhauMoi.MaxLength = 50;
            txtXacNhanMatKhau.MaxLength = 50;

            // Gắn sự kiện
            this.Load += FrmDoiMatKhau_Load;
            btnXacNhan.Click += BtnXacNhan_Click;
            btnHuy.Click += BtnHuy_Click;
            txtXacNhanMatKhau.KeyPress += TxtXacNhanMatKhau_KeyPress;
        }

        private void FrmDoiMatKhau_Load(object sender, EventArgs e)
        {
            Console.WriteLine($"[FrmDoiMatKhau] Form load cho user: {tenDangNhap}");
            txtMatKhauMoi.Focus();
        }

        private void TxtXacNhanMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                string matKhauMoi = txtMatKhauMoi.Text.Trim();
                string xacNhanMatKhau = txtXacNhanMatKhau.Text.Trim();

                Console.WriteLine($"[FrmDoiMatKhau] Đổi mật khẩu cho user: {tenDangNhap}");

                // Validate input
                if (string.IsNullOrWhiteSpace(matKhauMoi))
                {
                    MessageBox.Show(
                        "Vui lòng nhập mật khẩu mới!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(xacNhanMatKhau))
                {
                    MessageBox.Show(
                        "Vui lòng nhập xác nhận mật khẩu!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtXacNhanMatKhau.Focus();
                    return;
                }

                // Kiểm tra độ dài mật khẩu
                if (matKhauMoi.Length < 6)
                {
                    MessageBox.Show(
                        "Mật khẩu phải có ít nhất 6 ký tự!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                // Kiểm tra mật khẩu khớp
                if (matKhauMoi != xacNhanMatKhau)
                {
                    MessageBox.Show(
                        "Mật khẩu xác nhận không khớp!\nVui lòng nhập lại.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    txtXacNhanMatKhau.Clear();
                    txtXacNhanMatKhau.Focus();
                    return;
                }

                // Xác nhận với người dùng
                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc chắn muốn đổi mật khẩu cho tài khoản:\n{tenDangNhap}?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return;

                Console.WriteLine($"[FrmDoiMatKhau] Đang cập nhật mật khẩu mới...");

                // Lấy thông tin tài khoản hiện tại
                var taiKhoan = loginBUS.LayNguoiDungTheoTen(tenDangNhap);
                if (!taiKhoan.HasValue)
                {
                    throw new Exception("Không tìm thấy tài khoản!");
                }

                // Cập nhật mật khẩu mới (giữ nguyên trạng thái tài khoản)
                bool success = loginBUS.CapNhatNguoiDung(
                    tenDangNhap, 
                    matKhauMoi, 
                    taiKhoan.Value.trangThai);

                if (success)
                {
                    Console.WriteLine($"[FrmDoiMatKhau] ✅ Đổi mật khẩu thành công");

                    // Gửi email thông báo
                    try
                    {
                        emailService.GuiThongBaoDoiMatKhauThanhCong(email, tenDangNhap);
                        Console.WriteLine($"[FrmDoiMatKhau] ✅ Đã gửi email thông báo");
                    }
                    catch (Exception emailEx)
                    {
                        Console.WriteLine($"[FrmDoiMatKhau] ⚠️ Không gửi được email thông báo: {emailEx.Message}");
                        // Không hiển thị lỗi cho user vì đã đổi mật khẩu thành công
                    }

                    MessageBox.Show(
                        "✅ Đổi mật khẩu thành công!\n\n" +
                        "Bạn có thể đăng nhập bằng mật khẩu mới.\n" +
                        "Một email thông báo đã được gửi đến hộp thư của bạn.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Đóng form và trở về form đăng nhập
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Console.WriteLine($"[FrmDoiMatKhau] ❌ Đổi mật khẩu thất bại");
                    
                    MessageBox.Show(
                        "Không thể cập nhật mật khẩu!\nVui lòng thử lại sau.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FrmDoiMatKhau] ❌ Lỗi đổi mật khẩu: {ex.Message}");
                Console.WriteLine($"[FrmDoiMatKhau] Stack trace: {ex.StackTrace}");
                
                MessageBox.Show(
                    $"Đã xảy ra lỗi khi đổi mật khẩu:\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy?\nMật khẩu sẽ không được thay đổi.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Console.WriteLine($"[FrmDoiMatKhau] Người dùng hủy đổi mật khẩu");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void FrmDoiMatKhau_Load_1(object sender, EventArgs e)
        {

        }
    }
}
