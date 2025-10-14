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
    public partial class StatCardTKB : UserControl
    {
        public StatCardTKB()
        {
            InitializeComponent();
        }
        private string _monHoc;
        public string MonHoc
        {
            get => _monHoc;
            set { _monHoc = value; lblMonHoc.Text = value; }
        }

        private string _giaoVien;
        public string GiaoVien
        {
            get => _giaoVien;
            set { _giaoVien = value; lblGiaoVien.Text = value; }
        }

        private string _phong;
        public string Phong
        {
            get => _phong;
            set { _phong = value; lblPhong.Text = value; }
        }

        private Color _mauNen;
        public Color MauNen
        {
            get => _mauNen;
            set { _mauNen = value; guna2Panel1.FillColor = value; }
        }

        private void lblPhong_Click(object sender, EventArgs e)
        {

        }
    }
}
