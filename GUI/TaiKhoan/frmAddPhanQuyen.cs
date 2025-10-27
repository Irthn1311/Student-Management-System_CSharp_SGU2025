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
    public partial class frmAddPhanQuyen : Form
    {
        public frmAddPhanQuyen()
        {
            InitializeComponent();
        }

        private void frmAddPhanQuyen_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddQuyen_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thêm quyền mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Đóng form
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
