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

        public Color PanelBackgroundColor
        {
            get { return panelMauNen.FillColor; }
            set { panelMauNen.FillColor = value; }
        }

        public Color SoLuongForeColor
        {
            get { return lblSoLuong.ForeColor; }
            set { lblSoLuong.ForeColor = value; }
        }



        private void lblSoLuong_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
