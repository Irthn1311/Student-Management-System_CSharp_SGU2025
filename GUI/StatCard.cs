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
    public partial class StatCard : UserControl
    {
        public StatCard()
        {
            InitializeComponent();
        }

        [Category("Custom Props")]
        public string Title
        {
            get { return labelCardTitle.Text; }
            set { labelCardTitle.Text = value; }
        }

        [Category("Custom Props")]
        public string Value
        {
            get { return labelCardValue.Text; }
            set { labelCardValue.Text = value; }
        }

        [Category("Custom Props")]
        public Image Icon
        {
            get { return pictureBoxCardIcon.Image; }
            set { pictureBoxCardIcon.Image = value; }
        }
    }
}
