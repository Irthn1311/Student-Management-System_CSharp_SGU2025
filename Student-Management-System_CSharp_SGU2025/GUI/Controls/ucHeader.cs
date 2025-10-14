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
    public partial class ucHeader : UserControl
    {
        public ucHeader()
        {
            InitializeComponent();
        }

        // Public methods để update header text
        public void UpdateHeader(string title, string breadcrumb)
        {
            lblTitle.Text = title;
            lblBreadcrumb.Text = breadcrumb;
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }

        public void SetBreadcrumb(string breadcrumb)
        {
            lblBreadcrumb.Text = breadcrumb;
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
