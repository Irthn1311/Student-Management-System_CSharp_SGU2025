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
    public partial class SuaLopHoc : Form
    {
        private LopHocBUS lopHocBUS;
        private GiaoVienBUS giaoVienBUS;
        private int maLopHienTai;
        private string maGVCNHienTai; // ✅ Lưu GVCN hiện tại để xử lý đặc biệt

        public SuaLopHoc(int maLop)
        {
            InitializeComponent();
            lopHocBUS = new LopHocBUS();
            giaoVienBUS = new GiaoVienBUS();
            maLopHienTai = maLop;

            this.Load += SuaLopHoc_Load;
        }

        private void SuaLopHoc_Load(object sender, EventArgs e)
        {
            lbHeader.BackColor = guna2HtmlLabel1.BackColor;

            txtMaLop.Enabled = false;
            txtMaLop.ForeColor = Color.Gray;

            // ✅ Load thông tin lớp trước để lấy GVCN hiện tại
            LopDTO lopHienTai = lopHocBUS.LayLopTheoId(maLopHienTai);
            if (lopHienTai != null)
            {
                maGVCNHienTai = lopHienTai.maGVCN;
            }

            // ✅ Load danh sách giáo viên (bao gồm GVCN hiện tại)
            LoadDanhSachGiaoVien();

            // ✅ Load thông tin lớp lên form
            LoadThongTinLop();
        }

        private void LoadDanhSachGiaoVien()
        {
            try
            {
                List<GiaoVienDTO> dsgv = giaoVienBUS.DocDSGiaoVien() ?? new List<GiaoVienDTO>();
                List<string> dsGVCNDaPhanCong = lopHocBUS.LayDanhSachMaGVCNDangPhanCong() ?? new List<string>();

                var dshienthi = new List<object>();
                dshienthi.Add(new { MaGiaoVien = "", HoTen = "-- Chọn giáo viên --" });

                foreach (var gv in dsgv)
                {
                    // ✅ Hiển thị giáo viên chưa làm GVCN HOẶC đang là GVCN của lớp này
                    if (!dsGVCNDaPhanCong.Contains(gv.MaGiaoVien) || (!string.IsNullOrEmpty(maGVCNHienTai) && gv.MaGiaoVien == maGVCNHienTai))
                    {
                        dshienthi.Add(new { MaGiaoVien = gv.MaGiaoVien, HoTen = gv.HoTen });
                    }
                }

                cbGVCN.DataSource = dshienthi;
                cbGVCN.DisplayMember = "HoTen";
                cbGVCN.ValueMember = "MaGiaoVien";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách giáo viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongTinLop()
        {
            try
            {
                LopDTO lop = lopHocBUS.LayLopTheoId(maLopHienTai);

                if (lop != null)
                {
                    txtMaLop.Text = lop.maLop.ToString();
                    txtTenLop.Text = lop.tenLop;
                    cbKhoi.SelectedItem = lop.maKhoi.ToString();
                    txtSiSo.Text = lop.siSo.ToString();
                    cbGVCN.SelectedValue = lop.maGVCN;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin lớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
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

                if (cbGVCN.SelectedValue == null || string.IsNullOrEmpty(cbGVCN.SelectedValue.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn giáo viên chủ nhiệm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGVCN.Focus();
                    return;
                }

                // Kiểm tra sĩ số
                if (string.IsNullOrWhiteSpace(txtSiSo.Text) || !int.TryParse(txtSiSo.Text.Trim(), out int siSo) || siSo < 0)
                {
                    MessageBox.Show("Vui lòng nhập sĩ số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSiSo.Focus();
                    return;
                }

                // Tạo đối tượng LopDTO với dữ liệu mới
                LopDTO lopCapNhat = new LopDTO();
                lopCapNhat.maLop = maLopHienTai; // ✅ Giữ nguyên mã lớp (không thay đổi)
                lopCapNhat.tenLop = txtTenLop.Text.Trim();
                lopCapNhat.maKhoi = Convert.ToInt32(cbKhoi.SelectedItem.ToString());
                lopCapNhat.siSo = siSo;
                lopCapNhat.maGVCN = cbGVCN.SelectedValue.ToString();

                // Cập nhật lớp học
                bool kq = lopHocBUS.CapNhatLop(lopCapNhat, out string message, out string errorField);

                if (kq)
                {
                    MessageBox.Show("Cập nhật lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (string.IsNullOrEmpty(message)) message = "Cập nhật lớp học thất bại. Vui lòng kiểm tra lại thông tin.";
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}