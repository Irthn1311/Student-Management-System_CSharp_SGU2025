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
    public partial class SidebarMenuItem : UserControl
    {
        public SidebarMenuItem()
        {
            InitializeComponent();
        }

        [Category("Custom Props")]
        public string MenuText
        {
            get { return labelText.Text; }
            set { labelText.Text = value; }
        }

        [Category("Custom Props")]
        public Image MenuIcon
        {
            get { return pictureBoxIcon.Image; }
            set { pictureBoxIcon.Image = value; }
        }
    }
}
