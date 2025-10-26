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

        public Color PanelColor
        {
            get { return panelMain.FillColor; }  // <-- Sửa thành FillColor
            set { panelMain.FillColor = value; }  // <-- Sửa thành FillColor
        }

        
        public Color BorderColor
        {
            get { return panelMain.BorderColor; }
            set { panelMain.BorderColor = value; }
        }

        // Property để điều khiển MÀU CHỮ của các Label
        public Color TextColor
        {
            // Chỉ cần lấy màu của 1 label làm đại diện
            get { return lblTenKhoi.ForeColor; } 
            set
            {
              
                lblTenKhoi.ForeColor = value;    
                lblSoLop.ForeColor = value;  
                lblSoHocSinh.ForeColor = value; 
            }
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
