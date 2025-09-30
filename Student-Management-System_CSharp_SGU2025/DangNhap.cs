using System;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();
            string role = cbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Demo check
            if (user == "admin" && pass == "123" && role == "Quản trị viên")
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai thông tin đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void panelLeft_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblPass_Click(object sender, EventArgs e)
        {

        }
    }
}
