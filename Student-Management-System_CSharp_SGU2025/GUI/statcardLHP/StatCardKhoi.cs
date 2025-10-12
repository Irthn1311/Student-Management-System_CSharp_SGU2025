using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    public partial class StatCardKhoi : UserControl
    {
        public StatCardKhoi()
        {
            InitializeComponent();
        }

        // Gán dữ liệu và màu nền cho thẻ
        public void SetData(string tenKhoi, string soLop, string soHocSinh)
        {
            lblTenKhoi.Text = tenKhoi;
            lblSoLop.Text = soLop;
            lblSoHocSinh.Text = soHocSinh;




        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
