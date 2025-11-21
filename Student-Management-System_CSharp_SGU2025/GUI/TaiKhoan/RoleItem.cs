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
    public partial class RoleItem : UserControl
    {
        public RoleItem()
        {
            InitializeComponent();
        }

        public string RoleName
        {
            get => lblRoleName.Text;
            set => lblRoleName.Text = value;
        }

        public string RoleDescription
        {
            get => lblRoleDescription.Text;
            set => lblRoleDescription.Text = value;
        }

        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;

        private void txtRoleName_Click(object sender, EventArgs e)
        {

        }

        private void txtRole_Click(object sender, EventArgs e)
        {

        }

        private void btnXoaRole_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, e);

        }

        private void btnEditRole_Click(object sender, EventArgs e)
        {
            EditClicked?.Invoke(this, e);

        }

        private void RoleItem_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
