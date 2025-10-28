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
        private GiaoVienBUS giaoVienBUS;

        public ThemLopHoc()
        {
            InitializeComponent();
            lopHocBUS = new LopHocBUS();
            giaoVienBUS = new GiaoVienBUS();
            this.Load += ThemLopHoc_Load;
        }

        private void ThemLopHoc_Load(object sender, EventArgs e)
        {
            // ✅ HIỂN THỊ MÃ LỚP TIẾP THEO
            int maLopMoi = lopHocBUS.LayMaLopTiepTheo();
            txtMaLop.Text = maLopMoi.ToString();
            txtMaLop.Enabled = false; // Không cho sửa
            txtMaLop.ForeColor = Color.Black;

            LoadDanhSachGiaoVien();
        }

        private void LoadDanhSachGiaoVien()
        {
            try
            {
                List<GiaoVienDTO> dsgv = giaoVienBUS.DocDSGiaoVien();

                // ✅ Lấy danh sách GVCN đã phân công
                List<string> dsGVCNDaPhanCong = lopHocBUS.LayDanhSachMaGVCNDangPhanCong() ?? new List<string>();

                var dshienthi = new List<object>();
                dshienthi.Add(new { MaGiaoVien = "", HoTen = "-- Chọn giáo viên --" });

                // ✅ CHỈ THÊM GIÁO VIÊN CHƯA LÀM GVCN
                foreach (var gv in dsgv ?? new List<GiaoVienDTO>())
                {
                    if (!dsGVCNDaPhanCong.Contains(gv.MaGiaoVien))
                    {
                        dshienthi.Add(new { MaGiaoVien = gv.MaGiaoVien, HoTen = gv.HoTen });
                    }
                }

                cbGVCN.DataSource = dshienthi;
                cbGVCN.DisplayMember = "HoTen";
                cbGVCN.ValueMember = "MaGiaoVien";
                cbGVCN.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách giáo viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (cbKhoi.SelectedItem == null || cbKhoi.SelectedIndex == 0)
                {
                    MessageBox.Show("Vui lòng chọn khối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbKhoi.Focus();
                    return;
                }

                // Kiểm tra sĩ số
                if (string.IsNullOrWhiteSpace(txtSiSo.Text) || !int.TryParse(txtSiSo.Text.Trim(), out int siSo) || siSo < 0)
                {
                    MessageBox.Show("Vui lòng nhập sĩ số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSiSo.Focus();
                    return;
                }

                // Kiểm tra giáo viên chủ nhiệm
                if (cbGVCN.SelectedValue == null || string.IsNullOrEmpty(cbGVCN.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn giáo viên chủ nhiệm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGVCN.Focus();
                    return;
                }

                // ✅ Tạo đối tượng LopDTO - GÁN maLop từ TextBox
                LopDTO lopMoi = new LopDTO();
                lopMoi.maLop = Convert.ToInt32(txtMaLop.Text); // ✅ GÁN MÃ LỚP THỦ CÔNG
                lopMoi.tenLop = txtTenLop.Text.Trim();
                lopMoi.maKhoi = Convert.ToInt32(cbKhoi.SelectedItem.ToString());
                lopMoi.siSo = siSo;
                lopMoi.maGVCN = cbGVCN.SelectedValue.ToString();

                // Thêm lớp học
                bool kq = lopHocBUS.ThemLop(lopMoi, out string message, out string errorField);

                if (kq)
                {
                    MessageBox.Show("Thêm lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (string.IsNullOrEmpty(message)) message = "Thêm lớp học thất bại. Vui lòng kiểm tra lại thông tin.";
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Focus theo errorField
                    switch (errorField)
                    {
                        case "tenLop":
                            txtTenLop.Focus();
                            break;
                        case "maKhoi":
                            cbKhoi.Focus();
                            break;
                        case "siSo":
                            txtSiSo.Focus();
                            break;
                        case "maGVCN":
                            cbGVCN.Focus();
                            break;
                        default:
                            break;
                    }
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