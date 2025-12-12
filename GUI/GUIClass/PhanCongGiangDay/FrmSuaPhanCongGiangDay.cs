using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmSuaPhanCongGiangDay : Form
    {
        private PhanCongGiangDayBUS phanCongBUS;
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;
        private int maPhanCong;
        private PhanCongGiangDayDTO phanCongHienTai;

        public FrmSuaPhanCongGiangDay(int maPhanCong)
        {
            InitializeComponent();
            this.maPhanCong = maPhanCong;
            phanCongBUS = new PhanCongGiangDayBUS();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
        }

        private void FrmSuaPhanCongGiangDay_Load(object sender, EventArgs e)
        {
            try
            {
                LoadThongTinPhanCong();
                LoadGiaoVienThayThe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongTinPhanCong()
        {
            try
            {
                phanCongHienTai = phanCongBUS.LayPhanCongTheoMa(maPhanCong);
                if (phanCongHienTai == null)
                {
                    MessageBox.Show("Không tìm thấy phân công!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Load thông tin hiện tại
                GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(phanCongHienTai.MaGiaoVien);
                MonHocDTO mh = monHocBUS.LayDSMonHocTheoId(phanCongHienTai.MaMonHoc);
                LopDTO lop = lopHocBUS.LayLopTheoId(phanCongHienTai.MaLop);
                HocKyDTO hk = hocKyBUS.LayHocKyTheoMa(phanCongHienTai.MaHocKy);

                // Hiển thị thông tin không thể thay đổi
                lblLop.Text = $"Lớp: {(lop != null ? lop.tenLop : $"Lớp {phanCongHienTai.MaLop}")}";
                lblMonHoc.Text = $"Môn học: {(mh != null ? mh.tenMon : $"Môn {phanCongHienTai.MaMonHoc}")}";
                lblHocKy.Text = $"Học kỳ: {(hk != null ? $"{hk.TenHocKy} - {hk.MaNamHoc}" : $"Học kỳ {phanCongHienTai.MaHocKy}")}";
                lblGiaoVienHienTai.Text = $"Giáo viên hiện tại: {(gv != null ? $"{gv.HoTen} ({gv.MaGiaoVien})" : phanCongHienTai.MaGiaoVien)}";

                // Thiết lập ngày tháng
                dtpNgayBatDau.Value = phanCongHienTai.NgayBatDau;
                dtpNgayKetThuc.Value = phanCongHienTai.NgayKetThuc;
                dtpNgayBatDau.Format = DateTimePickerFormat.Custom;
                dtpNgayBatDau.CustomFormat = "dd/MM/yyyy";
                dtpNgayKetThuc.Format = DateTimePickerFormat.Custom;
                dtpNgayKetThuc.CustomFormat = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGiaoVienThayThe()
        {
            try
            {
                // Lấy danh sách giáo viên thay thế (cùng chuyên môn, loại trừ giáo viên hiện tại)
                var danhSachGVThayThe = giaoVienBUS.LayDanhSachGiaoVienThayThe(
                    phanCongHienTai.MaMonHoc, 
                    phanCongHienTai.MaGiaoVien
                );

                cbGiaoVienThayThe.Items.Clear();
                cbGiaoVienThayThe.Items.Add(new ComboBoxItem
                {
                    Text = "-- Chọn giáo viên thay thế --",
                    Value = ""
                });

                if (danhSachGVThayThe.Count == 0)
                {
                    cbGiaoVienThayThe.Items.Add(new ComboBoxItem
                    {
                        Text = "⚠️ KHÔNG CÓ GIÁO VIÊN THAY THẾ",
                        Value = ""
                    });
                    cbGiaoVienThayThe.Enabled = false;
                    lblThongBao.Text = "⚠️ Không có giáo viên nào cùng chuyên môn và trống lịch để thay thế!";
                    lblThongBao.ForeColor = Color.Red;
                }
                else
                {
                    foreach (var gv in danhSachGVThayThe)
                    {
                        cbGiaoVienThayThe.Items.Add(new ComboBoxItem
                        {
                            Text = $"{gv.MaGiaoVien} - {gv.HoTen}",
                            Value = gv.MaGiaoVien
                        });
                    }
                    lblThongBao.Text = $"✓ Tìm thấy {danhSachGVThayThe.Count} giáo viên có thể thay thế";
                    lblThongBao.ForeColor = Color.FromArgb(34, 197, 94);
                }

                cbGiaoVienThayThe.DisplayMember = "Text";
                cbGiaoVienThayThe.ValueMember = "Value";
                cbGiaoVienThayThe.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách giáo viên thay thế: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate giáo viên thay thế
                if (cbGiaoVienThayThe.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn giáo viên thay thế!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGiaoVienThayThe.Focus();
                    return;
                }

                string maGiaoVienMoi = ((ComboBoxItem)cbGiaoVienThayThe.SelectedItem).Value.ToString();
                if (string.IsNullOrEmpty(maGiaoVienMoi))
                {
                    MessageBox.Show("Vui lòng chọn giáo viên thay thế hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate ngày tháng
                if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value)
                {
                    MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayKetThuc.Focus();
                    return;
                }

                // Tạo đối tượng phân công mới
                PhanCongGiangDayDTO phanCongMoi = new PhanCongGiangDayDTO
                {
                    MaPhanCong = phanCongHienTai.MaPhanCong,
                    MaLop = phanCongHienTai.MaLop,
                    MaGiaoVien = maGiaoVienMoi,
                    MaMonHoc = phanCongHienTai.MaMonHoc,
                    MaHocKy = phanCongHienTai.MaHocKy,
                    NgayBatDau = dtpNgayBatDau.Value.Date,
                    NgayKetThuc = dtpNgayKetThuc.Value.Date
                };

                // Cập nhật
                bool kq = phanCongBUS.CapNhatPhanCong(phanCongMoi);

                if (kq)
                {
                    var gvMoi = giaoVienBUS.LayGiaoVienTheoMa(maGiaoVienMoi);
                    MessageBox.Show(
                        "✓ Cập nhật phân công thành công!\n\n" +
                        $"• Giáo viên mới: {gvMoi?.HoTen ?? maGiaoVienMoi}\n" +
                        $"• Lớp: {lblLop.Text}\n" +
                        $"• Môn học: {lblMonHoc.Text}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật phân công thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Class helper cho ComboBox
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void lblHocKy_Click(object sender, EventArgs e)
        {

        }
    }
}
