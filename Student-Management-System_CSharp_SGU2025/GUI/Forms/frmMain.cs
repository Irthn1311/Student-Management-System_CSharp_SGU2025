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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeNavigation();
            
            // Set default page to Dashboard
            ShowDashboard();
        }

        private void InitializeNavigation()
        {
            // Wire up sidebar button events
            // Sử dụng reflection để access private buttons trong ucSidebar1
            var btnBangTin = GetControlByName(ucSidebar1, "btnBangTin") as Guna.UI2.WinForms.Guna2Button;
            var btnXepLoai = GetControlByName(ucSidebar1, "btnXepLoai") as Guna.UI2.WinForms.Guna2Button;
            var btnBaoCao = GetControlByName(ucSidebar1, "btnBaoCao") as Guna.UI2.WinForms.Guna2Button;

            if (btnBangTin != null)
            {
                btnBangTin.Click += BtnBangTin_Click;
            }

            if (btnXepLoai != null)
            {
                btnXepLoai.Click += BtnXepLoai_Click;
            }

            if (btnBaoCao != null)
            {
                btnBaoCao.Click += BtnBaoCao_Click;
            }
        }

        // Helper method để lấy control theo tên
        private Control GetControlByName(Control parent, string name)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.Name == name)
                    return ctrl;
                
                var found = GetControlByName(ctrl, name);
                if (found != null)
                    return found;
            }
            return null;
        }

        private void BtnBangTin_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void BtnXepLoai_Click(object sender, EventArgs e)
        {
            ShowXepLoai();
        }

        private void BtnBaoCao_Click(object sender, EventArgs e)
        {
            ShowBaoCao();
        }

        private void ShowDashboard()
        {
            // Hide all content panels
            ucDashboard1.Visible = true;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Bảng tin", "Trang chủ / Bảng tin");

            // Bring to front
            ucDashboard1.BringToFront();
        }

        private void ShowXepLoai()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = true;
            ucBaoCao1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Xếp loại & Tổng kết", "Trang chủ / Xếp loại & Tổng kết");

            // Bring to front
            ucXepLoai1.BringToFront();
        }

        private void ShowBaoCao()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = true;

            // Update header
            ucHeader1.UpdateHeader("Báo cáo", "Trang chủ / Báo cáo");

            // Bring to front
            ucBaoCao1.BringToFront();
        }

        private void ucDashboard1_Load(object sender, EventArgs e)
        {

        }
    }
}
