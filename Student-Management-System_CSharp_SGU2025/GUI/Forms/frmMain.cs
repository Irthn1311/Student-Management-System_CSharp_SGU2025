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
            // Wire up sidebar button events using public properties
            ucSidebar1.BangTinButton.Click += BtnBangTin_Click;
            ucSidebar1.XepLoaiButton.Click += BtnXepLoai_Click;
            ucSidebar1.BaoCaoButton.Click += BtnBaoCao_Click;
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
