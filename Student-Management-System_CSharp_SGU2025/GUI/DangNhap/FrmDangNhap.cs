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
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void lbTenDangNhap_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // Mở form TrangChu và đóng hẳn ứng dụng khi TrangChu bị đóng
            var frm = new TrangChu();
            frm.FormClosed += (s, args) => this.Close(); // đóng form đăng nhập => kết thúc message loop
            frm.Show();
            this.Hide();
        }

        private void lbChaoMung2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void linkLbLienHeIT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
