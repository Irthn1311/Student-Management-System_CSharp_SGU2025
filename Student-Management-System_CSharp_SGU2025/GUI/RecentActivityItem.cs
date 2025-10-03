using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class RecentActivityItem : UserControl
    {
        public RecentActivityItem()
        {
            InitializeComponent();
        }

        [Category("Custom Props")]
        public Color IndicatorColor
        {
            get { return panelIndicator.BackColor; }
            set { panelIndicator.BackColor = value; }
        }

        [Category("Custom Props")]
        public string ActivityText
        {
            get { return labelActivityText.Text; }
            set { labelActivityText.Text = value; }
        }

        [Category("Custom Props")]
        public string TimeText
        {
            get { return labelTimeText.Text; }
            set { labelTimeText.Text = value; }
        }
    }
}
