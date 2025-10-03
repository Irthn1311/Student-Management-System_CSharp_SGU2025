using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class SidebarMenuItem : UserControl
    {
        public SidebarMenuItem()
        {
            InitializeComponent();

            // Lắng nghe click
            this.Click += SidebarMenuItem_Click;
            labelText.Click += SidebarMenuItem_Click;
            pictureBoxIcon.Click += SidebarMenuItem_Click;

            SetActive(false); // mặc định không active
        }

        // Sự kiện để FormMain có thể bắt được
        public event EventHandler ItemClicked;

        private bool isActive;
        [Category("Custom Props")]
        public bool IsActive
        {
            get { return isActive; }
            set { SetActive(value); }
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

        private void SidebarMenuItem_Click(object sender, EventArgs e)
        {
            // Kích hoạt sự kiện ra ngoài
            ItemClicked?.Invoke(this, e);
        }

        private void SetActive(bool active)
        {
            isActive = active;
            if (active)
            {
                this.BackColor = Color.LightBlue; // màu xanh nhạt
            }
            else
            {
                this.BackColor = Color.Transparent; // trở lại bình thường
            }
        }
    }
}
