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

namespace Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_
{
    public partial class FrmThemMonHoc : Form
    {
        private MonHocBUS monHocBUS;

        public FrmThemMonHoc()
        {
            InitializeComponent();
            monHocBUS = new MonHocBUS();
            this.Load += FrmThemMonHoc_Load;
        }

        private void FrmThemMonHoc_Load(object sender, EventArgs e)
        {
            lbHeader.BackColor = guna2HtmlLabel1.BackColor;

            // ✅ Vô hiệu hóa txtMaMon vì database tự động tăng
            txtMaMon.Enabled = false;
            txtMaMon.Text = "(Tự động)";
            txtMaMon.ForeColor = Color.Gray;

            // ✅ Thiết lập ComboBox loại môn học
            cbGhiChu.Items.Clear();
            cbGhiChu.Items.Add("-- Chọn loại môn học --");
            cbGhiChu.Items.Add("Môn chính");
            cbGhiChu.Items.Add("Khoa học tự nhiên");
            cbGhiChu.Items.Add("Khoa học xã hội");
            cbGhiChu.Items.Add("Kỹ năng khác");
            cbGhiChu.SelectedIndex = 0;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtTenMon.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenMon.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSoTiet.Text))
                {
                    MessageBox.Show("Vui lòng nhập số tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTiet.Focus();
                    return;
                }

                // Kiểm tra số tiết phải là số nguyên dương
                if (!int.TryParse(txtSoTiet.Text, out int soTiet) || soTiet <= 0)
                {
                    MessageBox.Show("Số tiết phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTiet.Focus();
                    return;
                }

                // ✅ Kiểm tra đã chọn loại môn học chưa
                if (cbGhiChu.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn loại môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGhiChu.Focus();
                    return;
                }

                // Tạo đối tượng MonHocDTO
                MonHocDTO monHocMoi = new MonHocDTO();
                monHocMoi.tenMon = txtTenMon.Text.Trim();
                monHocMoi.soTiet = soTiet;
                monHocMoi.ghiChu = cbGhiChu.SelectedItem.ToString(); // ✅ Lấy giá trị từ ComboBox

                // Thêm môn học
                bool kq = monHocBUS.ThemMonHoc(monHocMoi);

                if (kq)
                {
                    MessageBox.Show("Thêm môn học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm môn học thất bại. Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}