using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FormGuiYeuCauChuyenLop : Form
    {
        private int maHocSinh;
        private int maLopHienTai;
        private int maHocKy;
        private string tenHocSinh;
        private string tenLopHienTai;
        private int khoiHienTai;
        private string tenDangNhapNguoiTao;
      
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;
        private NamHocBUS namHocBUS;
        private PhanLopBLL phanLopBLL;
        private YeuCauChuyenLopBLL yeuCauBLL;
        private List<LopDTO> danhSachLopFull;

        public FormGuiYeuCauChuyenLop(int maHocSinh, int maLopHienTai, int maHocKy, string tenHocSinh, string tenLopHienTai, string tenDangNhapNguoiTao)
        {
            InitializeComponent();
            this.maHocSinh = maHocSinh;
            this.maLopHienTai = maLopHienTai;
            this.maHocKy = maHocKy;
            this.tenHocSinh = tenHocSinh;
            this.tenLopHienTai = tenLopHienTai;
            this.tenDangNhapNguoiTao = tenDangNhapNguoiTao;
            
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
            namHocBUS = new NamHocBUS();
            phanLopBLL = new PhanLopBLL();
            yeuCauBLL = new YeuCauChuyenLopBLL();
            danhSachLopFull = new List<LopDTO>();
            
            // Lấy khối của lớp hiện tại
            var lopCu = lopHocBUS.LayLopTheoId(maLopHienTai);
            khoiHienTai = lopCu?.maKhoi ?? 0;
        }

        private void FormGuiYeuCauChuyenLop_Load(object sender, EventArgs e)
        {
            LoadThongTin();
            LoadDanhSachLop();
        }

        private void LoadThongTin()
        {
            try
            {
                lblHocSinh.Text = $"Học sinh: {tenHocSinh} (Mã: {maHocSinh})";
                
                // Hiển thị thông tin lớp hiện tại
                var lopCu = lopHocBUS.LayLopTheoId(maLopHienTai);
                int siSoLopCu = phanLopBLL.GetHocSinhByLop(maLopHienTai, maHocKy)?.Count ?? 0;
                int siSoToiDaLopCu = (lopCu != null && lopCu.siSo > 0) ? lopCu.siSo : siSoLopCu;

                lblLopHienTai.Text = $"Lớp hiện tại: {tenLopHienTai} (Khối {khoiHienTai}) - Sĩ số: {siSoLopCu}/{siSoToiDaLopCu}";
                
                // Lấy thông tin học kỳ
                var hocKy = hocKyBUS.LayHocKyTheoMa(maHocKy);
                if (hocKy != null)
                {
                    var namHoc = namHocBUS.LayNamHocTheoMa(hocKy.MaNamHoc);
                    lblHocKy.Text = $"Học kỳ: {hocKy.TenHocKy} - {namHoc?.TenNamHoc ?? hocKy.MaNamHoc}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachLop()
        {
            try
            {
                cbLopMongMuon.Items.Clear();
                cbLopMongMuon.Items.Add("-- Để admin quyết định --");

                // Lấy thông tin học kỳ để xác định năm học
                var hocKy = hocKyBUS.LayHocKyTheoMa(maHocKy);

                // Lấy danh sách lớp theo năm học của học kỳ
                if (hocKy != null && !string.IsNullOrWhiteSpace(hocKy.MaNamHoc))
                {
                    danhSachLopFull = lopHocBUS.DocDSLopTheoNamHoc(hocKy.MaNamHoc);
                }
                else
                {
                    danhSachLopFull = lopHocBUS.DocDSLop();
                }

                if (danhSachLopFull == null || danhSachLopFull.Count == 0)
                {
                    MessageBox.Show("Không có lớp nào khả dụng.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Chỉ lấy lớp cùng khối
                var dsLopFiltered = new List<LopDTO>();
                foreach (var l in danhSachLopFull)
                {
                    if (l.maLop != maLopHienTai && l.maKhoi == khoiHienTai)
                    {
                        dsLopFiltered.Add(l);
                    }
                }

                if (dsLopFiltered.Count == 0)
                {
                    cbLopMongMuon.SelectedIndex = 0;
                    return;
                }

                // Header
                cbLopMongMuon.Items.Add($"═══ CÁC LỚP KHỐI {khoiHienTai} (Tham khảo) ═══");

                foreach (var lop in dsLopFiltered)
                {
                    // Sĩ số hiện tại theo học kỳ
                    int siSo = phanLopBLL
                        .GetHocSinhByLop(lop.maLop, maHocKy)?
                        .Count ?? 0;

                    int siSoToiDa = lop.siSo > 0 ? lop.siSo : siSo;
                    int siSoConLai = siSoToiDa - siSo;
                    if (siSoConLai < 0) siSoConLai = 0;

                    string siSoTag = siSoConLai <= 0
                        ? " ❌ ĐẦY"
                        : $" ✅ Còn {siSoConLai} chỗ";

                    string displayText =
                        $"{lop.tenLop} (Khối {lop.maKhoi}) [{siSo}/{siSoToiDa}]{siSoTag}";

                    cbLopMongMuon.Items.Add(new ComboBoxItem
                    {
                        Text = displayText,
                        Value = lop.maLop,
                        Tag = new { SiSo = siSo, Khoi = lop.maKhoi, SiSoToiDa = siSoToiDa }
                    });
                }

                cbLopMongMuon.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuiYeuCau_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra lý do
                if (string.IsNullOrWhiteSpace(txtLyDo.Text))
                {
                    MessageBox.Show("Vui lòng nhập lý do chuyển lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLyDo.Focus();
                    return;
                }

                // Lấy lớp mong muốn (nếu có)
                int? maLopMongMuon = null;
                string tenLopMongMuon = "Để admin quyết định";

                if (cbLopMongMuon.SelectedIndex > 1) // Không phải header
                {
                    var selectedItem = cbLopMongMuon.SelectedItem;
                    if (selectedItem is ComboBoxItem item)
                    {
                        maLopMongMuon = (int)item.Value;
                        tenLopMongMuon = item.Text;
                    }
                }

                // Xác nhận
                string message = $"Xác nhận gửi yêu cầu chuyển lớp:\n\n" +
                    $"📌 Học sinh: {tenHocSinh}\n" +
                    $"📤 Từ lớp: {tenLopHienTai} (Khối {khoiHienTai})\n" +
                    $"📥 Lớp mong muốn: {tenLopMongMuon}\n" +
                    $"📝 Lý do: {txtLyDo.Text.Trim()}\n\n" +
                    $"Yêu cầu sẽ được gửi đến admin để xem xét.";
                
                var result = MessageBox.Show(message, "Xác nhận gửi yêu cầu", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                    
                if (result == DialogResult.Yes)
                {
                    // Tạo yêu cầu
                    YeuCauChuyenLopDTO yeuCau = new YeuCauChuyenLopDTO
                    {
                        MaHocSinh = maHocSinh,
                        MaLopHienTai = maLopHienTai,
                        MaLopMongMuon = maLopMongMuon,
                        MaHocKy = maHocKy,
                        LyDoYeuCau = txtLyDo.Text.Trim(),
                        NguoiTao = tenDangNhapNguoiTao,
                        TrangThai = "Chờ duyệt",
                        NgayTao = DateTime.Now
                    };

                    // Gửi yêu cầu
                    bool thanhCong = yeuCauBLL.GuiYeuCau(yeuCau);
                    
                    if (thanhCong)
                    {
                        MessageBox.Show($"✅ Đã gửi yêu cầu chuyển lớp thành công!\n\n" +
                            $"Yêu cầu của bạn đang chờ admin xem xét và phê duyệt.", 
                            "Thành công", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không thể gửi yêu cầu. Vui lòng thử lại.", 
                            "Lỗi", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
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
        private void cbLopMongMuon_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0) return;

                e.DrawBackground();

                var item = cbLopMongMuon.Items[e.Index];
                string text = item.ToString();

                Color textColor = Color.Black;
                Color backgroundColor = Color.White;
                Font itemFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);

                // Kiểm tra header/separator
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

                // Vẽ background
                using (SolidBrush bgBrush = new SolidBrush(backgroundColor))
                {
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);
                }

                // Vẽ text
                using (SolidBrush textBrush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(text, itemFont, textBrush, e.Bounds.X + 5, e.Bounds.Y + 5);
                }

                e.DrawFocusRectangle();
            }
            catch
            {
                // Fallback
                e.DrawBackground();
                using (SolidBrush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(cbLopMongMuon.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
                }
            }
        }

        // Helper class
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public object Tag { get; set; }
            public override string ToString() => Text;
        }
    }
}

