using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.TaiKhoan
{
    public partial class statCardThongKeTaiKhoan : UserControl
    {
        public statCardThongKeTaiKhoan()
        {
            InitializeComponent();
        }

        public Color PanelBackgroundColor
        {
            get { return panelMain.FillColor; }
            set { panelMain.FillColor = value; }
        }

        // Property để thay đổi màu nền của PictureBox
        public Color PictureBoxBackgroundColor
        {
            get { return PictureBoxTaiKhoan.BackColor; }
            set { PictureBoxTaiKhoan.BackColor = value; }
        }

        // Property để gán ảnh cho PictureBox
        public Image Icon
        {
            get { return PictureBoxTaiKhoan.Image; }
            set { PictureBoxTaiKhoan.Image = value; }
        }
        public Color IconFillColor
        {
            get { return panelImage.FillColor; }
            set { panelImage.FillColor = value; }
        }
        public string TitleGhiChu
        {
            get => lblGhiChu.Text;
            set => lblGhiChu.Text = value;
        }

        public string TitleLietKe
        {
            get => lblLietKe.Text;
            set => lblLietKe.Text = value;
        }
        private void PictureBoxTaiKhoan_Click(object sender, EventArgs e)
        {

        }

        private void lblMon_Click(object sender, EventArgs e)
        {

        }

        private void lblLietKe_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
