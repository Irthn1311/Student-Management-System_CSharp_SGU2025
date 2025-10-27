using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI.GiaoVien;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_
{
    public partial class FrmThemPhanCongGiangDay : Form
    {
        private PhanCongGiangDayBUS phanCongBUS;
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;

        public FrmThemPhanCongGiangDay()
        {
            InitializeComponent();
            phanCongBUS = new PhanCongGiangDayBUS();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
        }

        private void FrmThemPhanCongGiangDay_Load(object sender, EventArgs e)
        {
            try
            {
                lbHeader.BackColor = guna2HtmlLabel1.BackColor;

                // Load dữ liệu cho các ComboBox
                LoadGiaoVien();
                LoadMonHoc();
                LoadLop();
                LoadHocKy();

                // Thiết lập DateTimePicker
                dtpNgayBatDau.Value = DateTime.Now;
                dtpNgayKetThuc.Value = DateTime.Now.AddMonths(4);
                dtpNgayBatDau.Format = DateTimePickerFormat.Custom;
                dtpNgayBatDau.CustomFormat = "dd/MM/yyyy";
                dtpNgayKetThuc.Format = DateTimePickerFormat.Custom;
                dtpNgayKetThuc.CustomFormat = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGiaoVien()
        {
            try
            {
                List<GiaoVienDTO> dsGV = giaoVienBUS.DocDSGiaoVien();
                cbGiaoVien.Items.Clear();
                cbGiaoVien.Items.Add(new ComboBoxItem
                {
                    Text = "-- Chọn giáo viên --",
                    Value = ""
                });

                if (dsGV != null && dsGV.Count > 0)
                {
                    foreach (GiaoVienDTO gv in dsGV)
                    {
                        cbGiaoVien.Items.Add(new ComboBoxItem
                        {
                            Text = $"{gv.MaGiaoVien} - {gv.HoTen}",
                            Value = gv.MaGiaoVien
                        });
                    }
                }

                cbGiaoVien.DisplayMember = "Text";
                cbGiaoVien.ValueMember = "Value";
                cbGiaoVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMonHoc()
        {
            try
            {
                List<MonHocDTO> dsMH = monHocBUS.DocDSMH();
                cbMonHoc.Items.Clear();
                cbMonHoc.Items.Add(new ComboBoxItem
                {
                    Text = "-- Chọn môn học --",
                    Value = 0
                });

                if (dsMH != null && dsMH.Count > 0)
                {
                    foreach (MonHocDTO mh in dsMH)
                    {
                        cbMonHoc.Items.Add(new ComboBoxItem
                        {
                            Text = $"{mh.tenMon} ({mh.soTiet} tiết)",
                            Value = mh.maMon
                        });
                    }
                }

                cbMonHoc.DisplayMember = "Text";
                cbMonHoc.ValueMember = "Value";
                cbMonHoc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách môn học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLop()
        {
            try
            {
                List<LopDTO> dsLop = lopHocBUS.DocDSLop();
                cbLop.Items.Clear();
                cbLop.Items.Add(new ComboBoxItem
                {
                    Text = "-- Chọn lớp --",
                    Value = 0
                });

                if (dsLop != null && dsLop.Count > 0)
                {
                    foreach (LopDTO lop in dsLop)
                    {
                        cbLop.Items.Add(new ComboBoxItem
                        {
                            Text = lop.tenLop,
                            Value = lop.maLop
                        });
                    }
                }

                cbLop.DisplayMember = "Text";
                cbLop.ValueMember = "Value";
                cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHocKy()
        {
            try
            {
                List<HocKyDTO> dsHK = hocKyBUS.DocDSHocKy();
                cbHocKy.Items.Clear();
                cbHocKy.Items.Add(new ComboBoxItem
                {
                    Text = "-- Chọn học kỳ --",
                    Value = 0
                });

                if (dsHK != null && dsHK.Count > 0)
                {
                    foreach (HocKyDTO hk in dsHK)
                    {
                        cbHocKy.Items.Add(new ComboBoxItem
                        {
                            Text = $"{hk.TenHocKy} - {hk.MaNamHoc}",
                            Value = hk.MaHocKy
                        });
                    }
                }

                cbHocKy.DisplayMember = "Text";
                cbHocKy.ValueMember = "Value";
                cbHocKy.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách học kỳ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate giáo viên
                if (cbGiaoVien.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn giáo viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGiaoVien.Focus();
                    return;
                }

                // Validate môn học
                if (cbMonHoc.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn môn học!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbMonHoc.Focus();
                    return;
                }

                // Validate lớp
                if (cbLop.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbLop.Focus();
                    return;
                }

                // Validate học kỳ
                if (cbHocKy.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbHocKy.Focus();
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

                // Lấy giá trị từ ComboBox
                string maGiaoVien = ((ComboBoxItem)cbGiaoVien.SelectedItem).Value.ToString();
                int maMonHoc = Convert.ToInt32(((ComboBoxItem)cbMonHoc.SelectedItem).Value);
                int maLop = Convert.ToInt32(((ComboBoxItem)cbLop.SelectedItem).Value);
                int maHocKy = Convert.ToInt32(((ComboBoxItem)cbHocKy.SelectedItem).Value);

                // Kiểm tra trùng lặp
                if (phanCongBUS.KiemTraPhanCongTonTai(maLop, maGiaoVien, maMonHoc, maHocKy))
                {
                    MessageBox.Show(
                        "Phân công này đã tồn tại!\n\n" +
                        "Giáo viên này đã được phân công dạy môn học này cho lớp này trong học kỳ này.",
                        "Trùng lặp",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Tạo đối tượng
                PhanCongGiangDayDTO phanCong = new PhanCongGiangDayDTO
                {
                    MaGiaoVien = maGiaoVien,
                    MaMonHoc = maMonHoc,
                    MaLop = maLop,
                    MaHocKy = maHocKy,
                    NgayBatDau = dtpNgayBatDau.Value.Date,
                    NgayKetThuc = dtpNgayKetThuc.Value.Date
                };

                // Thêm vào database
                bool kq = phanCongBUS.ThemPhanCong(phanCong);

                if (kq)
                {
                    MessageBox.Show(
                        "✓ Thêm phân công giảng dạy thành công!\n\n" +
                        $"• Giáo viên: {cbGiaoVien.Text}\n" +
                        $"• Môn học: {cbMonHoc.Text}\n" +
                        $"• Lớp: {cbLop.Text}\n" +
                        $"• Học kỳ: {cbHocKy.Text}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "✗ Thêm phân công thất bại!\n\nVui lòng kiểm tra lại thông tin.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi: {ex.Message}", "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ✅ THÊM NÚT MỞ FORM THÊM MÔN HỌC NHANH
        private void btnThemMonHocNhanh_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmThemMonHoc frm = new FrmThemMonHoc())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadMonHoc(); // Reload danh sách môn học
                        MessageBox.Show(
                            "✓ Thêm môn học thành công!\nMôn học mới đã xuất hiện trong danh sách.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}