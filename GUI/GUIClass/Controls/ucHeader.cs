using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ucHeader : UserControl
    {
        public ucHeader()
        {
            InitializeComponent();
            this.Load += ucHeader_Load;
        }

        // Public methods để update header text
        public void UpdateHeader(string title, string breadcrumb)
        {
            lblTitle.Text = title;
            lblBreadcrumb.Text = breadcrumb;
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }

        public void SetBreadcrumb(string breadcrumb)
        {
            lblBreadcrumb.Text = breadcrumb;
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlHeader_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void ucHeader_Load(object sender, EventArgs e)
        {
            UpdateUserInfo();
        }

        /// <summary>
        /// ✅ Cập nhật thông tin người dùng từ SessionManager
        /// </summary>
        public void UpdateUserInfo()
        {
            try
            {
                if (SessionManager.IsLoggedIn())
                {
                    Console.WriteLine($"[DEBUG] UpdateUserInfo - TenDangNhap: {SessionManager.TenDangNhap}");
                    Console.WriteLine($"[DEBUG] UpdateUserInfo - HoTen: {SessionManager.HoTen}");
                    Console.WriteLine($"[DEBUG] UpdateUserInfo - VaiTro: {SessionManager.VaiTro}");

                    // ✅ LUÔN HIỂN THỊ TÊN ĐĂNG NHẬP (không dùng HoTen)
                    if (this.Controls.Find("tenDangNhap", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2HtmlLabel lblUserName)
                    {
                        lblUserName.Text = SessionManager.TenDangNhap; // ✅ Luôn dùng TenDangNhap
                        Console.WriteLine($"[SUCCESS] Đã set tenDangNhap = {SessionManager.TenDangNhap}");
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] Không tìm thấy control 'tenDangNhap'");
                    }

                    // ✅ HIỂN THỊ VAI TRÒ
                    if (this.Controls.Find("lblLogRole", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2HtmlLabel lblRole)
                    {
                        string role = SessionManager.GetDisplayRole();
                        lblRole.Text = role;
                        Console.WriteLine($"[SUCCESS] Đã set lblLogRole = {role}");
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] Không tìm thấy control 'lblLogRole'");
                    }
                }
                else
                {
                    Console.WriteLine("[INFO] Chưa đăng nhập");

                    if (this.Controls.Find("tenDangNhap", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2HtmlLabel lblUserName)
                    {
                        lblUserName.Text = "Guest";
                    }

                    if (this.Controls.Find("lblLogRole", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2HtmlLabel lblRole)
                    {
                        lblRole.Text = "Khách";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi cập nhật thông tin người dùng: {ex.Message}");
                Console.WriteLine($"[ERROR] Stack trace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// ✅ Public method để refresh thông tin người dùng từ bên ngoài
        /// </summary>
        public void RefreshUserInfo()
        {
            UpdateUserInfo();
        }

    }
}
