using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_
{
    public partial class FrmSuaMonHoc : Form
    {
        private MonHocBUS monHocBUS;
        private int maMonHienTai;

        public FrmSuaMonHoc(int maMon)
        {
            InitializeComponent();
            monHocBUS = new MonHocBUS();
            maMonHienTai = maMon;
            this.Load += FrmSuaMonHoc_Load;
        }

        private void FrmSuaMonHoc_Load(object sender, EventArgs e)
        {
            lbHeader.BackColor = guna2HtmlLabel1.BackColor;

            // ✅ Vô hiệu hóa txtMaMon vì không cho sửa mã
            txtMaMon.Enabled = false;
            txtMaMon.ForeColor = Color.Gray;

            // ✅ Load thông tin môn học hiện tại
            LoadThongTinMonHoc();
        }

        private void LoadThongTinMonHoc()
        {
            try
            {
                MonHocDTO monHoc = monHocBUS.LayDSMonHocTheoId(maMonHienTai);

                if (monHoc != null)
                {
                    txtMaMon.Text = monHoc.maMon.ToString();
                    txtTenMon.Text = monHoc.tenMon;
                    txtSoTiet.Text = monHoc.soTiet.ToString();

                    // ✅ Set giá trị cho Guna2ComboBox
                    if (!string.IsNullOrEmpty(monHoc.ghiChu))
                    {
                        // Tìm index của item khớp với ghiChu
                        int index = -1;
                        for (int i = 0; i < cbGhiChu.Items.Count; i++)
                        {
                            if (cbGhiChu.Items[i].ToString() == monHoc.ghiChu)
                            {
                                index = i;
                                break;
                            }
                        }
                        cbGhiChu.SelectedIndex = index >= 0 ? index : 0;
                    }
                    else
                    {
                        cbGhiChu.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin môn học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate tên môn học
                if (string.IsNullOrWhiteSpace(txtTenMon.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenMon.Focus();
                    return;
                }

                // Validate số tiết
                if (string.IsNullOrWhiteSpace(txtSoTiet.Text))
                {
                    MessageBox.Show("Vui lòng nhập số tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTiet.Focus();
                    return;
                }

                if (!int.TryParse(txtSoTiet.Text.Trim(), out int soTiet) || soTiet <= 0)
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

                // Tạo đối tượng MonHocDTO với dữ liệu mới
                MonHocDTO monHocCapNhat = new MonHocDTO();
                monHocCapNhat.maMon = maMonHienTai;
                monHocCapNhat.tenMon = txtTenMon.Text.Trim();
                monHocCapNhat.soTiet = soTiet;
                monHocCapNhat.ghiChu = cbGhiChu.SelectedItem.ToString(); // ✅ Lấy từ Guna2ComboBox

                // Cập nhật môn học
                bool kq = monHocBUS.UpdateMonHoc(monHocCapNhat);

                if (kq)
                {
                    MessageBox.Show("Cập nhật môn học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật môn học thất bại. Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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