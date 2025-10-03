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
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        // ...existing code...
        private void buttonHamburger_Click(object sender, EventArgs e)
        {
            
        }
        // ...existing code...

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void mainContentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void statCardUpcomingExams_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanelStatCards_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelSearchAdmin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sidebarMenuItemThiVaDiem_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sidebarMenuItem1_Load(object sender, EventArgs e)
        {
        }

        private void pictureBoxScoreChart_Click(object sender, EventArgs e)
        {

        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            mainContentPanel.Controls.Clear();
            PNMonHoc pn = new PNMonHoc();
            pn.Dock = DockStyle.Fill;

            // Add vào panelContent
            mainContentPanel.Controls.Add(pn);
        }

        private void sidebarMenuItemDanhGia_Load(object sender, EventArgs e)
        {

        }

        private void sidebarMenuItemTrangChu_Load(object sender, EventArgs e)
        {
            mainContentPanel.Controls.Clear();
            PNMonHoc pn = new PNMonHoc();
            pn.Dock = DockStyle.Fill;
            mainContentPanel.Controls.Add(pn);
        }
    }
}
