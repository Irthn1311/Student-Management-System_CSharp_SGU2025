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
    public partial class BaoCaoKhoi : UserControl
    {
        public BaoCaoKhoi()
        {
            InitializeComponent();
        }

        // --- Getter / Setter cho số học sinh Giỏi ---
        public string SoGioi
        {
            get { return lblGioi.Text; }
            set { lblGioi.Text = value; }
        }

        // --- Getter / Setter cho số học sinh Khá ---
        public string SoKha
        {
            get { return lblKha.Text; }
            set { lblKha.Text = value; }
        }

        // --- Getter / Setter cho số học sinh Trung Bình ---
        public string SoTrungBinh
        {
            get { return lblTrungBinh.Text; }
            set { lblTrungBinh.Text = value; }
        }

        // --- Getter / Setter cho số học sinh Yếu ---
        public string SoYeu
        {
            get { return lblYeu.Text; }
            set { lblYeu.Text = value; }
        }

        private void lblGioi_Click(object sender, EventArgs e)
        {

        }

        private void pnlKhoiStats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
