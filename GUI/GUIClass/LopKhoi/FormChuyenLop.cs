using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FormChuyenLop : Form
    {
        private int maHocSinh;
        private int maLopCu;
        private int maHocKy;
        private string tenHocSinh;
        private string tenLopCu;
        private int khoiHienTai;
        
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;
        private NamHocBUS namHocBUS;
        private PhanLopBLL phanLopBLL;
        private List<LopDTO> danhSachLopFull;

        public int MaLopMoi { get; private set; }
        public string LyDo { get; private set; }

        public FormChuyenLop(int maHocSinh, int maLopCu, int maHocKy, string tenHocSinh, string tenLopCu)
        {
            InitializeComponent();
            this.maHocSinh = maHocSinh;
            this.maLopCu = maLopCu;
            this.maHocKy = maHocKy;
            this.tenHocSinh = tenHocSinh;
            this.tenLopCu = tenLopCu;
            
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
            namHocBUS = new NamHocBUS();
            phanLopBLL = new PhanLopBLL();
            danhSachLopFull = new List<LopDTO>();
            
            MaLopMoi = 0;
            LyDo = "";
            
            // Lấy khối của lớp hiện tại
            var lopCu = lopHocBUS.LayLopTheoId(maLopCu);
            khoiHienTai = lopCu?.maKhoi ?? 0;
        }

        private void FormChuyenLop_Load(object sender, EventArgs e)
        {
            LoadThongTin();
            LoadDanhSachLop();
        }

        private void LoadThongTin()
        {
            try
            {
                lblHocSinh.Text = $"Học sinh: {tenHocSinh} (Mã: {maHocSinh})";
                
                // Hiển thị thông tin lớp cũ với sĩ số hiện tại / sĩ số tối đa (lấy từ cấu hình lớp)
                var lopCu = lopHocBUS.LayLopTheoId(maLopCu);
                int siSoLopCu = phanLopBLL.LayDanhSachHocSinhTheoLopVaHocKy(maLopCu, maHocKy)?.Count ?? 0;

                // Sĩ số tối đa: ưu tiên lấy từ LopDTO.siSo (được nhập khi tạo lớp), nếu không có thì fallback về sĩ số hiện tại
                int siSoToiDaLopCu = (lopCu != null && lopCu.siSo > 0) ? lopCu.siSo : siSoLopCu;

                lblLopCu.Text = $"Lớp hiện tại: {tenLopCu} (Khối {khoiHienTai}) - Sĩ số: {siSoLopCu}/{siSoToiDaLopCu}";
                
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
                cbLopMoi.Items.Clear();
                cbLopMoi.Items.Add("-- Chọn lớp mới --");

                // ✅ LẤY TẤT CẢ LỚP - KHÔNG PHỤ THUỘC VÀO NĂM HỌC
                // Vì form chuyển lớp chỉ cho phép chuyển trong cùng khối,
                // nên lấy tất cả lớp và filter theo khối là hợp lý.
                // Điều này đảm bảo các lớp mới thêm vào (chưa có học sinh) cũng được hiển thị.
                danhSachLopFull = lopHocBUS.DocDSLop();

                if (danhSachLopFull == null || danhSachLopFull.Count == 0)
                {
                    MessageBox.Show("Không có lớp nào để chuyển.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // ✅ CHỈ LỌC THEO KHỐI - KHÔNG PHÂN BIỆT TÊN LỚP (cho phép tất cả ký tự đặc biệt, tiếng Việt, v.v.)
                var dsLopFiltered = danhSachLopFull
                    .Where(l => l != null && l.maLop != maLopCu && l.maKhoi == khoiHienTai)
                    .OrderBy(l => l.tenLop ?? "")
                    .ToList();

                // Debug: Log số lượng lớp được tìm thấy
                Console.WriteLine($"[FormChuyenLop] Tìm thấy {dsLopFiltered.Count} lớp cùng khối {khoiHienTai}");
                foreach (var lop in dsLopFiltered)
                {
                    Console.WriteLine($"[FormChuyenLop] Lớp: {lop.tenLop} (Mã: {lop.maLop}, Khối: {lop.maKhoi})");
                }

                if (dsLopFiltered.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy lớp nào cùng khối (Khối {khoiHienTai}) để chuyển.\n\n" +
                        $"Hệ thống chỉ cho phép chuyển lớp trong cùng khối.\n\n" +
                        $"Tổng số lớp trong hệ thống: {danhSachLopFull.Count}",
                        "Không có lớp để chuyển", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbLopMoi.SelectedIndex = 0;
                    return;
                }

                // Header hiển thị nhóm lớp cùng khối
                cbLopMoi.Items.Add($"═══ CÁC LỚP KHỐI {khoiHienTai} (Có thể chuyển) ⭐ ═══");

                foreach (var lop in dsLopFiltered)
                {
                    try
                    {
                        // Sĩ số hiện tại theo học kỳ
                        int siSo = phanLopBLL
                            .LayDanhSachHocSinhTheoLopVaHocKy(lop.maLop, maHocKy)?
                            .Count ?? 0;

                        // Sĩ số tối đa lấy từ cấu hình lớp, nếu chưa có thì xem hiện tại là tối đa
                        int siSoToiDa = lop.siSo > 0 ? lop.siSo : siSo;
                        int siSoConLai = siSoToiDa - siSo;
                        if (siSoConLai < 0) siSoConLai = 0;

                        string siSoTag = siSoConLai <= 0
                            ? " ❌ ĐẦY"
                            : $" ✅ Còn {siSoConLai} chỗ";

                        // ✅ Hiển thị tên lớp đầy đủ, không filter ký tự đặc biệt
                        string tenLop = lop.tenLop ?? $"Lớp {lop.maLop}";
                        string displayText =
                            $"{tenLop} (Khối {lop.maKhoi}) [{siSo}/{siSoToiDa}]{siSoTag}";

                        cbLopMoi.Items.Add(new ComboBoxItem
                        {
                            Text = displayText,
                            Value = lop.maLop,
                            Tag = new { SiSo = siSo, Khoi = lop.maKhoi, SiSoToiDa = siSoToiDa, IsEnabled = siSoConLai > 0 }
                        });
                    }
                    catch (Exception ex)
                    {
                        // Log lỗi nhưng vẫn tiếp tục với lớp khác
                        Console.WriteLine($"[FormChuyenLop] Lỗi khi thêm lớp {lop?.maLop}: {ex.Message}");
                    }
                }

                cbLopMoi.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn lớp mới
                if (cbLopMoi.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra có phải là header hay separator không
                var selectedItem = cbLopMoi.SelectedItem;
                if (selectedItem is string)
                {
                    MessageBox.Show("Vui lòng chọn một lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin lớp mới
                ComboBoxItem item = selectedItem as ComboBoxItem;
                if (item == null)
                {
                    MessageBox.Show("Vui lòng chọn một lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MaLopMoi = (int)item.Value;
                
                // Lấy thông tin lớp mới
                var lopMoi = lopHocBUS.LayLopTheoId(MaLopMoi);
                dynamic tagData = item.Tag;
                int siSoLopMoi = tagData.SiSo;           // sĩ số hiện tại
                int khoiLopMoi = tagData.Khoi;
                int siSoToiDa = tagData.SiSoToiDa;       // sĩ số tối đa (cấu hình khi tạo lớp)
                bool isEnabled = tagData.IsEnabled;

                // ✅ VALIDATION: Kiểm tra có được phép chọn không
                if (!isEnabled)
                {
                    MessageBox.Show($"Lớp {lopMoi.tenLop} đã đầy sĩ số ({siSoToiDa}/{siSoToiDa}).\n\nKhông thể chuyển học sinh vào lớp này.", 
                        "Không thể chuyển lớp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy lý do (optional)
                LyDo = txtLyDo.Text.Trim();

                // ✅ Kiểm tra cùng khối - ĐÂY LÀ ĐIỀU KIỆN DUY NHẤT (không kiểm tra tên lớp)
                if (khoiLopMoi != khoiHienTai)
                {
                    MessageBox.Show($"Không thể chuyển học sinh sang lớp khác khối.\n\n" +
                        $"Lớp hiện tại thuộc Khối {khoiHienTai}, lớp mới thuộc Khối {khoiLopMoi}.\n" +
                        $"Vui lòng chọn lại một lớp cùng khối.", 
                        "Không cho phép chuyển khác khối", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ KHÔNG CÓ VALIDATION NÀO VỀ TÊN LỚP - Cho phép tất cả ký tự đặc biệt, tiếng Việt, v.v.

                // Xác nhận
                string message = $"Xác nhận chuyển lớp:\n\n" +
                    $"📌 Học sinh: {tenHocSinh}\n" +
                    $"📤 Từ lớp: {tenLopCu} (Khối {khoiHienTai})\n" +
                    $"📥 Sang lớp: {lopMoi.tenLop} (Khối {khoiLopMoi})\n" +
                    $"📊 Sĩ số lớp mới: {siSoLopMoi}/{siSoToiDa} → {siSoLopMoi + 1}/{siSoToiDa}";
                
                if (!string.IsNullOrEmpty(LyDo))
                {
                    message += $"\n\n📝 Lý do: {LyDo}";
                }
                
                var result = MessageBox.Show(message, "Xác nhận chuyển lớp", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                    
                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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

        // ✅ EVENT VẼ CÁC ITEM TRONG COMBOBOX VỚI MÀU SẮC - HỖ TRỢ UNICODE ĐẦY ĐỦ
        private void cbLopMoi_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0) return;

                e.DrawBackground();

                var item = cbLopMoi.Items[e.Index];
                string text = item?.ToString() ?? "";

                // ✅ Đảm bảo text không null và hỗ trợ Unicode đầy đủ
                if (string.IsNullOrEmpty(text))
                {
                    text = "";
                }

                // Màu mặc định
                Color textColor = Color.Black;
                Color backgroundColor = Color.White;
                // ✅ Sử dụng font hỗ trợ Unicode tốt (Segoe UI hỗ trợ tiếng Việt và ký tự đặc biệt)
                Font itemFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);

                // Kiểm tra header/separator
                if (text.Contains("═══"))
                {
                    // Header - màu xanh đậm, in đậm
                    textColor = Color.FromArgb(0, 102, 204);
                    backgroundColor = Color.FromArgb(230, 240, 255);
                    itemFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (item is ComboBoxItem cbItem && cbItem.Tag != null)
                {
                    // Lấy thông tin từ Tag
                    dynamic tagData = cbItem.Tag;
                    bool isEnabled = tagData.IsEnabled;

                    if (text.Contains("❌ ĐẦY"))
                    {
                        // Lớp đầy - màu đỏ, background đỏ nhạt
                        textColor = Color.FromArgb(220, 38, 38);
                        backgroundColor = Color.FromArgb(254, 242, 242);
                        itemFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    }
                    else if (text.Contains("✅"))
                    {
                        // Lớp còn chỗ - màu xanh lá, background xanh nhạt
                        textColor = Color.FromArgb(22, 163, 74);
                        backgroundColor = Color.FromArgb(240, 253, 244);
                        itemFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                    }
                }
                else if (text.StartsWith("--"))
                {
                    // Item mặc định ("-- Chọn lớp mới --")
                    textColor = Color.Gray;
                    itemFont = new Font("Segoe UI", 9.5F, FontStyle.Italic);
                }

                // Vẽ background
                using (SolidBrush bgBrush = new SolidBrush(backgroundColor))
                {
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);
                }

                // ✅ Vẽ text với StringFormat để hỗ trợ Unicode tốt hơn
                using (SolidBrush textBrush = new SolidBrush(textColor))
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    
                    // Vẽ text với padding
                    RectangleF textRect = new RectangleF(
                        e.Bounds.X + 5, 
                        e.Bounds.Y, 
                        e.Bounds.Width - 10, 
                        e.Bounds.Height
                    );
                    
                    e.Graphics.DrawString(text, itemFont, textBrush, textRect, sf);
                }

                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                // Fallback: vẽ mặc định nếu có lỗi
                Console.WriteLine($"[FormChuyenLop] Lỗi khi vẽ item ComboBox: {ex.Message}");
                e.DrawBackground();
                try
                {
                    string fallbackText = cbLopMoi.Items[e.Index]?.ToString() ?? "";
                    using (SolidBrush brush = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(fallbackText, e.Font, brush, e.Bounds);
                    }
                }
                catch
                {
                    // Nếu vẫn lỗi, chỉ vẽ background
                }
            }
        }

        // Helper class cho ComboBox
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public object Tag { get; set; } // Lưu thêm metadata (sỉ số, khối)
            public override string ToString() => Text;
        }
    }
}
