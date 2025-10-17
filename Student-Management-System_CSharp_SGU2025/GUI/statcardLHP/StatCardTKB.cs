using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    public partial class StatCardTKB : UserControl
    {
        public StatCardTKB()
        {
            InitializeComponent();
        }
        public void SetData(string monHoc, string giaoVien, string lop,
                        Color textColor, Color progressColor1, Color progressColor2)
        {
            // 1. Gán dữ liệu text
            lblMonHoc.Text = monHoc;
            lblGiaoVien.Text = giaoVien;
            lblLop.Text = lop;

            // 2. Gán màu chữ (ForeColor)s
            lblMonHoc.ForeColor = textColor;
            lblGiaoVien.ForeColor = textColor;
            lblLop.ForeColor = textColor;

            // 3. Gán màu cho ProgressBar (Guna2ProgressBar)
            // Giả sử ProgressBar của bạn tên là 'progressBar'
            progressBar.ProgressColor = progressColor1;
            panelStatTKB.FillColor = progressColor2;
            progressBar.Value = 7; // Đặt giá trị để thanh màu hiển thị
        }
        private void lblPhong_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
