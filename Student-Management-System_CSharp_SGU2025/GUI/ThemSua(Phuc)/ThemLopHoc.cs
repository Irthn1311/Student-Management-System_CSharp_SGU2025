using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
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
    public partial class ThemLopHoc : Form
    {
        private LopHocBUS lopHocBUS;
        public ThemLopHoc()
        {
            InitializeComponent();
            lopHocBUS = new LopHocBUS();


        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void FrmLopHocComponent_Load(object sender, EventArgs e)
        {
            lbHeader.BackColor = guna2HtmlLabel1.BackColor;
        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtTenLop.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenLop.Focus();
                    return;
                }

                if (cbKhoi.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn khối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbKhoi.Focus();
                    return;
                }

                if (cbGVCN.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn giáo viên chủ nhiệm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGVCN.Focus();
                    return;
                }

                // Tạo đối tượng LopDTO
                LopDTO lopMoi = new LopDTO();
                lopMoi.tenLop = txtTenLop.Text.Trim();
                lopMoi.maKhoi = Convert.ToInt32(cbKhoi.SelectedValue); // Hoặc cbKhoi.SelectedItem tùy cách bind
                lopMoi.maGVCN = cbGVCN.SelectedValue.ToString(); // Hoặc cbGVCN.SelectedItem.ToString()

                // Thêm lớp học
                bool kq = lopHocBUS.ThemLop(lopMoi);

                if (kq)
                {
                    MessageBox.Show("Thêm lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm lớp học thất bại. Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu không đúng định dạng. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2HtmlLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lbHeader_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSoHocSinh_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
