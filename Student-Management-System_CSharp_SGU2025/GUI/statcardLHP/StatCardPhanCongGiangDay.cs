using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    public partial class StatCardPhanCongGiangDay : UserControl
    {
        public StatCardPhanCongGiangDay()
        {
            InitializeComponent();
        }

        private void StatCardPhanCongGiangDay_Load(object sender, EventArgs e)
        {

        }
        // Thuộc tính để thay đổi lúc runtime
        public string Title
        {
            get => lblTen.Text;
            set => lblTen.Text = value;
        }

        public string Value
        {
            get => lblSoLuong.Text;
            set => lblSoLuong.Text = value;
        }

        public Color TitleColor
        {
            get => lblTen.ForeColor;
            set => lblTen.ForeColor = value;
        }

        private void lblSoLuong_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
