using Guna.UI2.WinForms;
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
    public partial class DangNhap : Form
    {


        public DangNhap()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.WindowState = FormWindowState.Normal;

            panelLeft.Dock = DockStyle.Left;
            panelRight.Dock = DockStyle.Fill;
        }


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            
            txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập";
            txtPass.PlaceholderText = "Nhập mật khẩu";
            txtPass.PasswordChar = '●'; // ẩn mật khẩu

            cbRole.Items.Clear();
            cbRole.Items.Add("Quản trị viên");
            cbRole.Items.Add("Giáo viên");
            cbRole.Items.Add("Sinh viên");
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
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

        private void panelRight_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string user = txtTenDangNhap.Text.Trim();
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
    }
}
