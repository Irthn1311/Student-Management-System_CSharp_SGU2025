using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FormDuyetYeuCau : Form
    {
        private YeuCauChuyenLopDTO yeuCau;
        private string tenDangNhapAdmin;
        private LopHocBUS lopHocBUS;
        private PhanLopBLL phanLopBLL;
        private YeuCauChuyenLopBLL yeuCauBLL;
        private int khoiHienTai;
        private List<LopDTO> danhSachLop;

        public FormDuyetYeuCau(YeuCauChuyenLopDTO yeuCau, string tenDangNhapAdmin)
        {
            InitializeComponent();
            this.yeuCau = yeuCau;
            this.tenDangNhapAdmin = tenDangNhapAdmin;
            lopHocBUS = new LopHocBUS();
            phanLopBLL = new PhanLopBLL();
            yeuCauBLL = new YeuCauChuyenLopBLL();
            danhSachLop = new List<LopDTO>();
        }

        private void FormDuyetYeuCau_Load(object sender, EventArgs e)
        {
            LoadThongTinYeuCau();
            LoadDanhSachLop();
        }

        private void LoadThongTinYeuCau()
        {
            try
            {
                // Hiển thị thông tin học sinh
                lblHocSinhValue.Text = yeuCau.TenHocSinh;
                lblLopHienTaiValue.Text = yeuCau.TenLopHienTai;
                lblHocKyValue.Text = $"{yeuCau.TenHocKy} - {yeuCau.TenNamHoc}";
                lblLopMongMuonValue.Text = yeuCau.TenLopMongMuon ?? "Không chỉ định";
                
                // Hiển thị lý do
                txtLyDoYeuCau.Text = yeuCau.LyDoYeuCau;
                txtLyDoYeuCau.ReadOnly = true;

                // Lấy khối hiện tại
                var lopHienTai = lopHocBUS.LayLopTheoId(yeuCau.MaLopHienTai);
                khoiHienTai = lopHienTai?.maKhoi ?? 0;
                
                // Hiển thị mã yêu cầu
                lblMaYeuCau.Text = $"Mã yêu cầu: #{yeuCau.MaYeuCau}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin yêu cầu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachLop()
        {
            try
            {
                cbLopDuocDuyet.Items.Clear();
                cbLopDuocDuyet.Items.Add("-- Chọn lớp để duyệt --");

                // Lấy danh sách lớp cùng khối
                var dsLopFull = lopHocBUS.DocDSLop();
                
                danhSachLop = new List<LopDTO>();
                foreach (var lop in dsLopFull)
                {
                    if (lop.maLop != yeuCau.MaLopHienTai && lop.maKhoi == khoiHienTai)
                    {
                        danhSachLop.Add(lop);
                    }
                }

                if (danhSachLop.Count == 0)
                {
                    MessageBox.Show($"Không có lớp nào cùng khối (Khối {khoiHienTai}) để duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbLopDuocDuyet.SelectedIndex = 0;
                    return;
                }

                // Header
                cbLopDuocDuyet.Items.Add($"═══ CÁC LỚP KHỐI {khoiHienTai} ═══");

                foreach (var lop in danhSachLop)
                {
                    int siSo = phanLopBLL.GetHocSinhByLop(lop.maLop, yeuCau.MaHocKy)?.Count ?? 0;
                    int siSoToiDa = lop.siSo > 0 ? lop.siSo : siSo;
                    int siSoConLai = siSoToiDa - siSo;
                    if (siSoConLai < 0) siSoConLai = 0;

                    string siSoTag = siSoConLai <= 0 ? " ❌ ĐẦY" : $" ✅ Còn {siSoConLai} chỗ";

                    string displayText = $"{lop.tenLop} (Khối {lop.maKhoi}) [{siSo}/{siSoToiDa}]{siSoTag}";

                    cbLopDuocDuyet.Items.Add(new ComboBoxItem
                    {
                        Text = displayText,
                        Value = lop.maLop,
                        Tag = new { SiSo = siSo, SiSoToiDa = siSoToiDa, IsEnabled = siSoConLai > 0 }
                    });
                }

                // Tự động chọn lớp mong muốn nếu có
                if (yeuCau.MaLopMongMuon.HasValue)
                {
                    for (int i = 0; i < cbLopDuocDuyet.Items.Count; i++)
                    {
                        if (cbLopDuocDuyet.Items[i] is ComboBoxItem item)
                        {
                            if ((int)item.Value == yeuCau.MaLopMongMuon.Value)
                            {
                                cbLopDuocDuyet.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }

                if (cbLopDuocDuyet.SelectedIndex == -1)
                {
                    cbLopDuocDuyet.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDuyetYeuCau_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn lớp
                if (cbLopDuocDuyet.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp để duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra không phải header
                var selectedItem = cbLopDuocDuyet.SelectedItem;
                if (selectedItem is string)
                {
                    MessageBox.Show("Vui lòng chọn một lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ComboBoxItem item = selectedItem as ComboBoxItem;
                if (item == null)
                {
                    MessageBox.Show("Vui lòng chọn một lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maLopDuocDuyet = (int)item.Value;
                dynamic tagData = item.Tag;
                bool isEnabled = tagData.IsEnabled;

                // Kiểm tra lớp có đầy không
                if (!isEnabled)
                {
                    var lopDuocDuyet = lopHocBUS.LayLopTheoId(maLopDuocDuyet);
                    MessageBox.Show($"Lớp {lopDuocDuyet.tenLop} đã đầy sĩ số.\n\nKhông thể duyệt yêu cầu.", "Không thể duyệt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy ghi chú
                string ghiChuAdmin = txtGhiChuAdmin.Text.Trim();

                // Xác nhận
                var lopMoi = lopHocBUS.LayLopTheoId(maLopDuocDuyet);
                string message = $"Xác nhận duyệt yêu cầu chuyển lớp:\n\n" +
                    $"📌 Học sinh: {yeuCau.TenHocSinh}\n" +
                    $"📤 Từ lớp: {yeuCau.TenLopHienTai}\n" +
                    $"📥 Sang lớp: {lopMoi.tenLop} (Khối {lopMoi.maKhoi})\n" +
                    $"📊 Sĩ số lớp mới: {tagData.SiSo}/{tagData.SiSoToiDa} → {tagData.SiSo + 1}/{tagData.SiSoToiDa}";

                if (!string.IsNullOrWhiteSpace(ghiChuAdmin))
                {
                    message += $"\n\n💬 Ghi chú: {ghiChuAdmin}";
                }

                message += "\n\nHệ thống sẽ tự động chuyển học sinh sang lớp mới.";

                var result = MessageBox.Show(message, "Xác nhận duyệt yêu cầu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Duyệt yêu cầu
                    bool thanhCong = yeuCauBLL.DuyetYeuCau(yeuCau.MaYeuCau, maLopDuocDuyet, tenDangNhapAdmin, ghiChuAdmin);

                    if (thanhCong)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không thể duyệt yêu cầu. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Event vẽ các item trong combobox
        private void cbLopDuocDuyet_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0) return;

                e.DrawBackground();

                var item = cbLopDuocDuyet.Items[e.Index];
                string text = item.ToString();

                Color textColor = Color.Black;
                Color backgroundColor = Color.White;
                Font itemFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);

                if (text.Contains("═══"))
                {
                    textColor = Color.FromArgb(0, 102, 204);
                    backgroundColor = Color.FromArgb(230, 240, 255);
                    itemFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (item is ComboBoxItem cbItem && cbItem.Tag != null)
                {
                    if (text.Contains("❌ ĐẦY"))
                    {
                        textColor = Color.FromArgb(220, 38, 38);
                        backgroundColor = Color.FromArgb(254, 242, 242);
                        itemFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    }
                    else if (text.Contains("✅"))
                    {
                        textColor = Color.FromArgb(22, 163, 74);
                        backgroundColor = Color.FromArgb(240, 253, 244);
                        itemFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                    }
                }
                else if (text.StartsWith("--"))
                {
                    textColor = Color.Gray;
                    itemFont = new Font("Segoe UI", 9.5F, FontStyle.Italic);
                }

                using (SolidBrush bgBrush = new SolidBrush(backgroundColor))
                {
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);
                }

                using (SolidBrush textBrush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(text, itemFont, textBrush, e.Bounds.X + 5, e.Bounds.Y + 5);
                }

                e.DrawFocusRectangle();
            }
            catch
            {
                e.DrawBackground();
                using (SolidBrush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cbLopDuocDuyet.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
                }
            }
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public object Tag { get; set; }
            public override string ToString() => Text;
        }

        private void cbLopDuocDuyet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtLyDoYeuCau_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
