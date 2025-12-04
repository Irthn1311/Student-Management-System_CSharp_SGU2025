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

                // ✅ Thứ tự hợp lý: Học kỳ → Lớp → Giáo viên
                LoadHocKy();
                LoadLop();
                // Không load giáo viên ngay, sẽ load khi chọn lớp
                // Không load môn học vì giáo viên chỉ dạy môn chuyên môn

                // ✅ Ẩn hoặc disable ComboBox môn học
                cbMonHoc.Visible = false;
                lblMonHoc.Visible = false;
                btnThemMonHocNhanh.Visible = false;

                // Thiết lập DateTimePicker
                dtpNgayBatDau.Value = DateTime.Now;
                dtpNgayKetThuc.Value = DateTime.Now.AddMonths(4);
                dtpNgayBatDau.Format = DateTimePickerFormat.Custom;
                dtpNgayBatDau.CustomFormat = "dd/MM/yyyy";
                dtpNgayKetThuc.Format = DateTimePickerFormat.Custom;
                dtpNgayKetThuc.CustomFormat = "dd/MM/yyyy";

                // ✅ Gắn event handler cho cbHocKy để có thể filter lớp theo học kỳ (nếu cần)
                cbHocKy.SelectedIndexChanged += CbHocKy_SelectedIndexChanged;
                
                // ✅ Gắn event handler cho cbLop để reload giáo viên khi chọn lớp
                cbLop.SelectedIndexChanged += CbLop_SelectedIndexChanged;
                
                // ✅ Gắn event handler cho cbGiaoVien để tự động set môn học
                cbGiaoVien.SelectedIndexChanged += CbGiaoVien_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load danh sách giáo viên (chỉ giáo viên có chuyên môn)
        /// Nếu có lớp được chọn, giáo viên chủ nhiệm sẽ được đưa lên đầu danh sách
        /// </summary>
        private void LoadGiaoVien(int? maLopSelected = null)
        {
            try
            {
                // ✅ Gỡ event handler tạm thời để tránh trigger khi đang load
                cbGiaoVien.SelectedIndexChanged -= CbGiaoVien_SelectedIndexChanged;

                // ✅ Chỉ lấy giáo viên có chuyên môn (MaMonChuyenMon != null)
                List<GiaoVienDTO> dsGV = giaoVienBUS.DocDSGiaoVien()
                    .Where(gv => gv.MaMonChuyenMon.HasValue && gv.TrangThai == "Đang giảng dạy")
                    .ToList();

                cbGiaoVien.Items.Clear();
                cbGiaoVien.Items.Add(new ComboBoxItem
                {
                    Text = "-- Chọn giáo viên --",
                    Value = ""
                });

                // ✅ Nếu có lớp được chọn, lấy giáo viên chủ nhiệm
                string maGVCN = null;
                if (maLopSelected.HasValue)
                {
                    var lop = lopHocBUS.LayLopTheoId(maLopSelected.Value);
                    if (lop != null && !string.IsNullOrEmpty(lop.maGVCN))
                    {
                        maGVCN = lop.maGVCN;
                    }
                }

                // ✅ Sắp xếp: GVCN ở đầu (nếu có), sau đó sắp xếp theo tên
                var gvCN = dsGV.FirstOrDefault(gv => gv.MaGiaoVien == maGVCN);
                var gvKhac = dsGV.Where(gv => gv.MaGiaoVien != maGVCN)
                    .OrderBy(gv => gv.HoTen)
                    .ToList();

                // Thêm GVCN đầu tiên (nếu có)
                if (gvCN != null)
                {
                    cbGiaoVien.Items.Add(new ComboBoxItem
                    {
                        Text = $"⭐ {gvCN.MaGiaoVien} - {gvCN.HoTen} (Chủ nhiệm) - {gvCN.TenMonChuyenMon ?? "Chưa có chuyên môn"}",
                        Value = gvCN.MaGiaoVien
                    });
                }

                // Thêm các giáo viên khác
                foreach (GiaoVienDTO gv in gvKhac)
                {
                    cbGiaoVien.Items.Add(new ComboBoxItem
                    {
                        Text = $"{gv.MaGiaoVien} - {gv.HoTen} - {gv.TenMonChuyenMon ?? "Chưa có chuyên môn"}",
                        Value = gv.MaGiaoVien
                    });
                }

                cbGiaoVien.DisplayMember = "Text";
                cbGiaoVien.ValueMember = "Value";
                cbGiaoVien.SelectedIndex = 0;

                // ✅ Gắn lại event handler
                cbGiaoVien.SelectedIndexChanged += CbGiaoVien_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ DEPRECATED: Không cần load môn học nữa vì giáo viên chỉ dạy môn chuyên môn
        /// Môn học sẽ được tự động lấy từ MaMonChuyenMon của giáo viên
        /// </summary>
        [Obsolete("Không sử dụng nữa. Môn học được tự động lấy từ MaMonChuyenMon của giáo viên.")]
        private void LoadMonHoc()
        {
            // Không làm gì cả
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
                    foreach (LopDTO lop in dsLop.OrderBy(l => l.tenLop))
                    {
                        // ✅ Hiển thị thông tin giáo viên chủ nhiệm nếu có
                        string displayText = lop.tenLop;
                        if (!string.IsNullOrEmpty(lop.maGVCN))
                        {
                            var gvCN = giaoVienBUS.LayGiaoVienTheoMa(lop.maGVCN);
                            if (gvCN != null)
                            {
                                displayText += $" (CN: {gvCN.HoTen})";
                            }
                        }

                        cbLop.Items.Add(new ComboBoxItem
                        {
                            Text = displayText,
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

        /// <summary>
        /// Event handler khi chọn học kỳ - Có thể filter lớp theo học kỳ (nếu cần)
        /// </summary>
        private void CbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // ✅ Khi chọn học kỳ, có thể reload lớp hoặc giáo viên nếu cần
                // Hiện tại chỉ reload giáo viên nếu đã chọn lớp
                if (cbLop.SelectedIndex > 0 && cbLop.SelectedItem is ComboBoxItem lopItem)
                {
                    int maLop = Convert.ToInt32(lopItem.Value);
                    LoadGiaoVien(maLop);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CbHocKy_SelectedIndexChanged: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler khi chọn lớp - Reload danh sách giáo viên với GVCN ở đầu
        /// </summary>
        private void CbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbLop.SelectedIndex > 0 && cbLop.SelectedItem is ComboBoxItem lopItem)
                {
                    int maLop = Convert.ToInt32(lopItem.Value);
                    // ✅ Reload giáo viên với lớp đã chọn (GVCN sẽ ở đầu)
                    LoadGiaoVien(maLop);
                }
                else
                {
                    // Nếu chọn "-- Chọn lớp --", load tất cả giáo viên
                    LoadGiaoVien(null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CbLop_SelectedIndexChanged: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler khi chọn giáo viên - Tự động set môn học từ MaMonChuyenMon
        /// </summary>
        private void CbGiaoVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbGiaoVien.SelectedIndex > 0 && cbGiaoVien.SelectedItem is ComboBoxItem gvItem)
                {
                    string maGiaoVien = gvItem.Value.ToString();
                    var gv = giaoVienBUS.LayGiaoVienTheoMa(maGiaoVien);
                    
                    if (gv != null && gv.MaMonChuyenMon.HasValue)
                    {
                        // ✅ Tự động set môn học từ chuyên môn của giáo viên
                        // Môn học đã được hiển thị trong text của ComboBox giáo viên
                        // Không cần làm gì thêm vì cbMonHoc đã bị ẩn
                        Console.WriteLine($"✅ Đã chọn giáo viên: {gv.HoTen}, Môn chuyên môn: {gv.TenMonChuyenMon} (MaMon: {gv.MaMonChuyenMon})");
                    }
                    else
                    {
                        MessageBox.Show(
                            "⚠️ Giáo viên này chưa có môn chuyên môn!\n\n" +
                            "Vui lòng cập nhật môn chuyên môn cho giáo viên trước khi phân công.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CbGiaoVien_SelectedIndexChanged: {ex.Message}");
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
                // ✅ Thứ tự validate hợp lý: Học kỳ → Lớp → Giáo viên
                
                // Validate học kỳ
                if (cbHocKy.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbHocKy.Focus();
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

                // Validate giáo viên
                if (cbGiaoVien.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn giáo viên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGiaoVien.Focus();
                    return;
                }

                // ✅ Validate môn học - Lấy từ MaMonChuyenMon của giáo viên
                string maGiaoVien = ((ComboBoxItem)cbGiaoVien.SelectedItem).Value.ToString();
                var giaoVien = giaoVienBUS.LayGiaoVienTheoMa(maGiaoVien);
                
                if (giaoVien == null || !giaoVien.MaMonChuyenMon.HasValue)
                {
                    MessageBox.Show(
                        "⚠️ Giáo viên này chưa có môn chuyên môn!\n\n" +
                        "Vui lòng cập nhật môn chuyên môn cho giáo viên trước khi phân công.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    cbGiaoVien.Focus();
                    return;
                }

                int maMonHoc = giaoVien.MaMonChuyenMon.Value; // ✅ Lấy từ MaMonChuyenMon

                // Validate ngày tháng
                if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value)
                {
                    MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpNgayKetThuc.Focus();
                    return;
                }

                // ✅ Lấy giá trị từ ComboBox (maMonHoc đã được lấy ở trên từ MaMonChuyenMon)
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
                    // ✅ Lấy tên môn học từ giáo viên
                    string tenMonHoc = giaoVien.TenMonChuyenMon ?? $"Mã môn: {maMonHoc}";
                    
                    MessageBox.Show(
                        "✓ Thêm phân công giảng dạy thành công!\n\n" +
                        $"• Giáo viên: {giaoVien.HoTen} ({giaoVien.MaGiaoVien})\n" +
                        $"• Môn học: {tenMonHoc} (Chuyên môn)\n" +
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

        // ✅ DEPRECATED: Nút thêm môn học nhanh không còn cần thiết
        // Vì giáo viên chỉ dạy môn chuyên môn, không cần chọn môn học trong form này
        [Obsolete("Không sử dụng nữa. Giáo viên chỉ dạy môn chuyên môn.")]
        private void btnThemMonHocNhanh_Click(object sender, EventArgs e)
        {
            // Không làm gì cả
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