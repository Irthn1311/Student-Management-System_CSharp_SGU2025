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
    public partial class statcardMonHoc : UserControl
    {
        public statcardMonHoc()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void SetData(string sl, string mon, string lk)
        {
            lblSoLuong.Text = sl;
            lblMon.Text = mon;
            lblLietKe.Text = lk;




        }

        private void lblSoLuong_Click(object sender, EventArgs e)
        {

        }
    }
}
