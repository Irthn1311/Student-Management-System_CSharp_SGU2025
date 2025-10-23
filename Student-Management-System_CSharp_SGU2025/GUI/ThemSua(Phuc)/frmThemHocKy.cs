using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.NamHoc
{
    public partial class frmThemHocKy : Form
    {
        private HocKyBUS hocKyBUS;
        private NamHocBUS namHocBUS;

        public frmThemHocKy()
        {
            InitializeComponent();
            hocKyBUS = new HocKyBUS();
            namHocBUS = new NamHocBUS();
        }

        private void frmThemHocKy_Load(object sender, EventArgs e)
        {
            try
            {
                // Load danh sách năm học vào ComboBox
                LoadDanhSachNamHoc();

                // Load danh sách học kỳ
                cboTenHocKy.Items.Clear();
                cboTenHocKy.Items.Add("Học kỳ I");
                cboTenHocKy.Items.Add("Học kỳ II");
                cboTenHocKy.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachNamHoc()
        {
            try
            {
                List<NamHocDTO> dsNamHoc = namHocBUS.DocDSNamHoc();

                if (dsNamHoc != null && dsNamHoc.Count > 0)
                {
                    cboNamHoc.DataSource = dsNamHoc;
                    cboNamHoc.DisplayMember = "TenNamHoc";
                    cboNamHoc.ValueMember = "MaNamHoc";
                    cboNamHoc.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Chưa có năm học nào trong hệ thống!\nVui lòng tạo năm học trước.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách năm học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboNamHoc.SelectedValue != null)
                {
                    // Lấy thông tin năm học được chọn
                    string maNamHoc = cboNamHoc.SelectedValue.ToString();
                    NamHocDTO namHoc = namHocBUS.LayNamHocTheoMa(maNamHoc);

                    if (namHoc != null)
                    {
                        // Tự động set ngày theo năm học
                        if (cboTenHocKy.SelectedIndex == 0) // Học kỳ I
                        {
                            dtpNgayBatDau.Value = namHoc.NgayBD;
                            // Kết thúc vào cuối tháng 12
                            dtpNgayKetThuc.Value = new DateTime(namHoc.NgayBD.Year, 12, 31);
                        }
                        else // Học kỳ II
                        {
                            // Bắt đầu từ đầu năm tiếp theo
                            dtpNgayBatDau.Value = new DateTime(namHoc.NgayBD.Year + 1, 1, 1);
                            dtpNgayKetThuc.Value = namHoc.NgayKT;
                        }

                        // Set min/max date theo năm học
                        dtpNgayBatDau.MinDate = namHoc.NgayBD;
                        dtpNgayBatDau.MaxDate = namHoc.NgayKT;
                        dtpNgayKetThuc.MinDate = namHoc.NgayBD;
                        dtpNgayKetThuc.MaxDate = namHoc.NgayKT;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn năm học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validate dữ liệu
                if (cboNamHoc.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn năm học!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboNamHoc.Focus();
                    return;
                }

                if (cboTenHocKy.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboTenHocKy.Focus();
                    return;
                }

                if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value)
                {
                    MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayKetThuc.Focus();
                    return;
                }

                // 2. Kiểm tra ngày học kỳ phải nằm trong năm học
                string maNamHoc = cboNamHoc.SelectedValue.ToString();
                NamHocDTO namHoc = namHocBUS.LayNamHocTheoMa(maNamHoc);

                if (dtpNgayBatDau.Value < namHoc.NgayBD || dtpNgayKetThuc.Value > namHoc.NgayKT)
                {
                    MessageBox.Show("Thời gian học kỳ phải nằm trong khoảng thời gian của năm học!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Kiểm tra học kỳ đã tồn tại chưa
                List<HocKyDTO> dsHocKy = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHoc);
                if (dsHocKy.Any(hk => hk.TenHocKy == cboTenHocKy.Text))
                {
                    MessageBox.Show($"Học kỳ '{cboTenHocKy.Text}' đã tồn tại trong năm học '{namHoc.TenNamHoc}'!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Tạo DTO
                HocKyDTO hocKy = new HocKyDTO
                {
                    TenHocKy = cboTenHocKy.Text,
                    MaNamHoc = maNamHoc,
                    NgayBD = dtpNgayBatDau.Value,
                    NgayKT = dtpNgayKetThuc.Value,
                    TrangThai = "Chưa bắt đầu"
                };

                // 5. Thêm vào database
                bool ketQua = hocKyBUS.ThemHocKy(hocKy);

                if (ketQua)
                {
                    MessageBox.Show("✓ Thêm học kỳ thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("✗ Thêm học kỳ thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi thêm học kỳ:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}