using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.NamHoc
{
    public partial class frmThemNamHoc : Form
    {
        private NamHocBUS namHocBUS;

        public frmThemNamHoc()
        {
            InitializeComponent();
            namHocBUS = new NamHocBUS();
        }

        private void frmThemNamHoc_Load(object sender, EventArgs e)
        {
            // Set ngày mặc định
            int namHienTai = DateTime.Now.Year;
            dtpNgayBatDau.Value = new DateTime(namHienTai, 9, 1);
            dtpNgayKetThuc.Value = new DateTime(namHienTai + 1, 5, 31);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate dữ liệu
                if (string.IsNullOrWhiteSpace(txtMaNamHoc.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã năm học", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNamHoc.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenNamHoc.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên năm học", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNamHoc.Focus();
                    return;
                }

                if (dtpNgayBatDau.Value.Date >= dtpNgayKetThuc.Value.Date)
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayBatDau.Focus();
                    return;
                }

                // Tạo đối tượng NamHocDTO
                NamHocDTO namHoc = new NamHocDTO
                {
                    MaNamHoc = txtMaNamHoc.Text.Trim(),
                    TenNamHoc = txtTenNamHoc.Text.Trim(),
                    NgayBD = dtpNgayBatDau.Value.Date,
                    NgayKT = dtpNgayKetThuc.Value.Date
                };

                // Lưu vào database
                bool ketQua = namHocBUS.ThemNamHoc(namHoc);

                if (ketQua)
                {
                    MessageBox.Show("Thêm năm học thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm năm học thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm năm học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dtpNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            // Tự động điều chỉnh ngày kết thúc nếu nhỏ hơn ngày bắt đầu
            if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value)
            {
                dtpNgayKetThuc.Value = dtpNgayBatDau.Value.AddMonths(9);
            }
        }

        private void txtTenNamHoc_TextChanged(object sender, EventArgs e)
        {
            // Tự động gợi ý mã và ngày khi nhập tên năm học
            string tenNamHoc = txtTenNamHoc.Text.Trim();
            
            if (tenNamHoc.Contains("-"))
            {
                try
                {
                    string[] parts = tenNamHoc.Split('-');
                    if (parts.Length == 2)
                    {
                        int namBatDau = int.Parse(parts[0].Trim());
                        int namKetThuc = int.Parse(parts[1].Trim());

                        if (namBatDau >= 2000 && namBatDau < 2100 && namKetThuc == namBatDau + 1)
                        {
                            // Chỉ tự động điền mã nếu chưa nhập
                            if (string.IsNullOrWhiteSpace(txtMaNamHoc.Text))
                            {
                                txtMaNamHoc.Text = tenNamHoc;
                            }
                            
                            // Tự động set ngày
                            dtpNgayBatDau.Value = new DateTime(namBatDau, 9, 1);
                            dtpNgayKetThuc.Value = new DateTime(namKetThuc, 5, 31);
                        }
                    }
                }
                catch
                {
                    // Ignore parsing errors
                }
            }
        }
    }
}