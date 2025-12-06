using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.XepLoai
{
    public partial class CardTheoKhoi : UserControl
    {
        public CardTheoKhoi()
        {
            InitializeComponent();
        }

        // --- Getter / Setter cho số học sinh Giỏi ---
        public string SoGioi
        {
            get { return lblSoGioi.Text; }
            set { lblSoGioi.Text = value; }
        }

        // --- Getter / Setter cho số học sinh Khá ---
        public string SoKha
        {
            get { return lblSoKha.Text; }
            set { lblSoKha.Text = value; }
        }

        // --- Getter / Setter cho số học sinh Trung Bình ---
        public string SoTrungBinh
        {
            get { return lblSoTrungBinh.Text; }
            set { lblSoTrungBinh.Text = value; }
        }

        // --- Getter / Setter cho số học sinh Yếu ---
        public string SoYeu
        {
            get { return lblSoYeu.Text; }
            set { lblSoYeu.Text = value; }
        }

        // --- Getter / Setter cho số học sinh Kem ---
        public string SoKem
        {
            get { return lblSoKem.Text; }
            set { lblSoKem.Text = value; }
        }

        // --- Getter / Setter cho số học sinh của khối ---
        public string SoHocSinhKhoi
        {
            get { return lblSoHocSinhKhoi.Text; }
            set { lblSoHocSinhKhoi.Text = value; }
        }

        public string KhoiLop
        {
            get { return lblKhoiLop.Text; }
            set { lblKhoiLop.Text = value; }
        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void pnlCardKhoi_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
