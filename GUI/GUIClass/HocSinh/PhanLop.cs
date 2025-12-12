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
using System.Text.RegularExpressions;
using OfficeOpenXml;
using Student_Management_System_CSharp_SGU2025.BUS.Services;
using System.IO;
using Student_Management_System_CSharp_SGU2025.DAO;
using MySql.Data.MySqlClient;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class PhanLop : Form
    {
        private LopHocBUS lopHocBus;
        private HocSinhBLL hocSinhBus;
        private HocKyBUS hocKyBus;
        private PhanLopBLL phanLopBLL;
        private PhanLopTuDongBLL phanLopTuDongBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        private NguoiDungBLL nguoiDungBLL;
        private ThemDiemBUS themDiemBUS;
        private HanhKiemBUS hanhKiemBUS;
        private XepLoaiBUS xepLoaiBUS;
        private DiemSoDAO diemSoDAO;
        private HanhKiemDAO hanhKiemDAO;
        private XepLoaiDAO xepLoaiDAO;
        private MonHocDAO monHocDAO;
        private HocKyDAO hocKyDAO;
        private List<DTO.LopDTO> danhSachLop;
        private List<DTO.HocKyDTO> danhSachHocKy;
        private List<(int maHocSinh, int maLop, int maHocKy)> danhSachPhanLop;
        private List<(int maHocSinh, int maLop, int maHocKy)> danhSachPhanLopGoc; // Danh sách phân lớp gốc để tìm kiếm
        private Dictionary<string, (HocKyDTO hk1, HocKyDTO hk2)> danhSachNamHoc; // Map từ chuỗi hiển thị năm học đến (HK1, HK2)

        public PhanLop()
        {
            InitializeComponent();
            lopHocBus = new LopHocBUS();
            hocSinhBus = new HocSinhBLL();
            hocKyBus = new HocKyBUS();
            phanLopBLL = new PhanLopBLL();
            phanLopTuDongBLL = new PhanLopTuDongBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            nguoiDungBLL = new NguoiDungBLL();
            themDiemBUS = new ThemDiemBUS();
            hanhKiemBUS = new HanhKiemBUS();
            xepLoaiBUS = new XepLoaiBUS();
            diemSoDAO = new DiemSoDAO();
            hanhKiemDAO = new HanhKiemDAO();
            xepLoaiDAO = new XepLoaiDAO();
            monHocDAO = new MonHocDAO();
            hocKyDAO = new HocKyDAO();
            danhSachLop = new List<DTO.LopDTO>();
            danhSachHocKy = new List<DTO.HocKyDTO>();
            danhSachPhanLop = new List<(int maHocSinh, int maLop, int maHocKy)>();
            danhSachPhanLopGoc = new List<(int, int, int)>();
            danhSachNamHoc = new Dictionary<string, (HocKyDTO hk1, HocKyDTO hk2)>();

            LoadComboBox();
            SetupTables();
            LoadData();
            SetupEventHandlers();
        }

        private void LoadComboBox()
        {
            // ✅ Load ComboBox theo NĂM HỌC (gộp cả HK1 và HK2) - Tự động chọn năm học "Đang diễn ra"
            danhSachHocKy = hocKyBus.DocDSHocKy();
            cbHocKyNamHoc.Items.Clear();
            
            // Nhóm học kỳ theo năm học
            var hocKyTheoNamHoc = danhSachHocKy
                .GroupBy(hk => hk.MaNamHoc)
                .OrderByDescending(g => g.Key) // Sắp xếp năm học mới nhất trước
                .ToList();
            
            // Thêm từng năm học vào ComboBox (dạng "Học kỳ I & II - 2025-2026")
            foreach (var nhomNamHoc in hocKyTheoNamHoc)
            {
                string maNamHoc = nhomNamHoc.Key;
                var danhSachHK = nhomNamHoc.OrderBy(hk => hk.TenHocKy).ToList();
                
                // Tìm HK1 và HK2 của năm học này
                HocKyDTO hk1 = danhSachHK.FirstOrDefault(hk => 
                    (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                    (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));
                HocKyDTO hk2 = danhSachHK.FirstOrDefault(hk => 
                    hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));
                
                // Tạo chuỗi hiển thị: "Học kỳ I & II - 2025-2026"
                string hienThiNamHoc = $"Học kỳ I & II - {maNamHoc}";
                cbHocKyNamHoc.Items.Add(hienThiNamHoc);
                
                // Lưu mapping để dễ dàng lấy HK1 và HK2 khi chọn
                if (hk1 != null && hk2 != null)
                {
                    danhSachNamHoc[hienThiNamHoc] = (hk1, hk2);
                }
            }
            
            // ✅ Tự động chọn năm học có học kỳ "Đang diễn ra"
            int selectedIndex = -1;
            foreach (var nhomNamHoc in hocKyTheoNamHoc)
            {
                bool coHocKyDangDienRa = nhomNamHoc.Any(hk => hk.TrangThai == "Đang diễn ra");
                if (coHocKyDangDienRa)
                {
                    selectedIndex = hocKyTheoNamHoc.IndexOf(nhomNamHoc);
                    break;
                }
            }
            
            if (selectedIndex >= 0 && selectedIndex < cbHocKyNamHoc.Items.Count)
            {
                cbHocKyNamHoc.SelectedIndex = selectedIndex;
            }
            else if (cbHocKyNamHoc.Items.Count > 0)
            {
                cbHocKyNamHoc.SelectedIndex = 0; // Nếu không có "Đang diễn ra", chọn năm học đầu tiên
            }

            // Gắn sự kiện cho ComboBox Học Kỳ
            cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;

            // Load ComboBox Lớp Học
            danhSachLop = lopHocBus.DocDSLop();
            cbLop.Items.Clear();
            cbLop.Items.Add("Chọn lớp");
            foreach (var lop in danhSachLop)
            {
                cbLop.Items.Add(lop.TenLop);
            }
            if (cbLop.Items.Count > 0)
            {
                cbLop.SelectedIndex = 0; // Chọn mục đầu tiên làm mặc định
            }

        }

        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterTablePhanLop();
            // ✅ Cập nhật trạng thái nút khi thay đổi năm học
            UpdateButtonStates();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            // btnChon giờ là btnTimKiem - chức năng tìm kiếm
            // Chức năng này đã được xử lý bởi txtTimKiem_TextChanged
            // Nút này có thể dùng để focus vào ô tìm kiếm hoặc xóa tìm kiếm
            txtTimKiem.Focus();
        }

        private void btnThemPhanLop_Click(object sender, EventArgs e)
        {
            try
            {
                // btnThemPhanLop giờ là btnPhanLopTuDong - Phân lớp tự động
                
                // ✅ Kiểm tra đã chọn học kỳ chưa (giờ không có "Chọn học kỳ" nữa nên chỉ cần check null)
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    MessageBox.Show("Không có học kỳ để phân lớp tự động.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Lấy năm học được chọn (dạng "Học kỳ I & II - 2025-2026")
                string namHocChon = cbHocKyNamHoc.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(namHocChon))
                {
                    MessageBox.Show("Không có năm học được chọn.", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy HK1 và HK2 của năm học được chọn
                if (!danhSachNamHoc.ContainsKey(namHocChon))
                {
                    MessageBox.Show("Không tìm thấy thông tin học kỳ cho năm học được chọn.", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var (hk1, hk2) = danhSachNamHoc[namHocChon];
                if (hk1 == null || hk2 == null)
                {
                    MessageBox.Show($"Năm học {namHocChon} phải có đầy đủ HK1 và HK2!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // *** KIỂM TRA NĂM HỌC ĐÃ ĐƯỢC PHÂN LỚP CHƯA (kiểm tra cả HK1 và HK2) ***
                int soHocSinhDaPhanLopHK1 = phanLopBLL.CountHocSinhInHocKy(hk1.MaHocKy);
                int soHocSinhDaPhanLopHK2 = phanLopBLL.CountHocSinhInHocKy(hk2.MaHocKy);
                
                if (soHocSinhDaPhanLopHK1 > 0 || soHocSinhDaPhanLopHK2 > 0)
                {
                    string thongBao = $"⚠️ NĂM HỌC ĐÃ ĐƯỢC PHÂN LỚP!\n\n";
                    thongBao += $"Năm học: {namHocChon}\n\n";
                    if (soHocSinhDaPhanLopHK1 > 0)
                        thongBao += $"   • HK1 ({hk1.TenHocKy}): {soHocSinhDaPhanLopHK1} học sinh\n";
                    if (soHocSinhDaPhanLopHK2 > 0)
                        thongBao += $"   • HK2 ({hk2.TenHocKy}): {soHocSinhDaPhanLopHK2} học sinh\n";
                    thongBao += "\n❌ Đã phân lớp tự động rồi, không thể phân lớp tự động lại!\n\n";
                    
                    MessageBox.Show(thongBao, "Không thể phân lớp lại",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                // ✅ Truyền HK1 để phân lớp (logic sẽ tự động phân cho cả HK1 và HK2)
                // Hiển thị preview trước khi thực hiện
                var preview = phanLopTuDongBLL.TaoPreviewPhanLop(hk1.MaHocKy);

                // Kiểm tra lỗi
                if (preview.ContainsKey("Loi"))
                {
                    MessageBox.Show($"Không thể tạo preview:\n\n{preview["Loi"]}", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // TẠO THÔNG BÁO PREVIEW CHI TIẾT
                string previewMessage = "╔════════════════════════════════════════════════╗\n";
                previewMessage += "║      XEM TRƯỚC KẾT QUẢ PHÂN LỚP TỰ ĐỘNG       ║\n";
                previewMessage += "╚════════════════════════════════════════════════╝\n\n";

                // Loại phân lớp
                previewMessage += $"📋 Kịch bản: {preview["LoaiPhanLop"]}\n";
                if (preview.ContainsKey("HocKyNguon"))
                {
                    previewMessage += $"   Nguồn dữ liệu: {preview["HocKyNguon"]}\n";
                }
                previewMessage += "\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n";

                // Tổng số học sinh
                previewMessage += $"👥 Tổng số học sinh 'Đang học': {preview["TongSoHocSinh"]}\n\n";

                // Hiển thị theo kịch bản
                if (preview.ContainsKey("SoHSLenLop")) // Kịch bản NEXT_YEAR
                {
                    int soHSLenLop = (int)preview["SoHSLenLop"];
                    int soHSOLai = (int)preview["SoHSOLai"];
                    double tyLe = (double)preview["TyLeLenLop"];

                    previewMessage += "📊 DỰ KIẾN:\n";
                    previewMessage += $"   ✓ Lên lớp: {soHSLenLop} học sinh\n";
                    previewMessage += $"   ⚠️ Ở lại (học lại): {soHSOLai} học sinh\n";
                    previewMessage += $"   → Tỷ lệ lên lớp: {tyLe:0.0}%\n";
                    previewMessage += $"   → Sẽ phân lớp cho cả HK1 và HK2 năm học mới\n\n";

                    if (preview.ContainsKey("SoHSGapLoi") && (int)preview["SoHSGapLoi"] > 0)
                    {
                        previewMessage += $"⚠️ Thiếu dữ liệu: {preview["SoHSGapLoi"]} học sinh\n";
                        previewMessage += "   (Không có đủ điểm HK1/HK2 hoặc hạnh kiểm năm học trước)\n\n";
                    }
                }
                else if (preview.ContainsKey("PhuongPhapPhanLop")) // Kịch bản FIRST_TIME
                {
                    previewMessage += "📊 DỰ KIẾN:\n";
                    previewMessage += $"   → {preview["PhuongPhapPhanLop"]}\n\n";

                    if (preview.ContainsKey("HocSinhTheoKhoi"))
                    {
                        var hocSinhTheoKhoi = preview["HocSinhTheoKhoi"] as Dictionary<int, int>;
                        if (hocSinhTheoKhoi != null)
                        {
                            foreach (var kvp in hocSinhTheoKhoi.OrderBy(x => x.Key))
                            {
                                previewMessage += $"   ✓ Khối {kvp.Key}: {kvp.Value} học sinh\n";
                            }
                            previewMessage += "\n";
                        }
                    }

                    if (preview.ContainsKey("HocSinhTheoChuCai"))
                    {
                        var hocSinhTheoChuCai = preview["HocSinhTheoChuCai"] as Dictionary<char, int>;
                        if (hocSinhTheoChuCai != null && hocSinhTheoChuCai.Count > 0)
                        {
                            previewMessage += "   📝 Phân bổ theo chữ cái:\n";
                            foreach (var kvp in hocSinhTheoChuCai.OrderBy(x => x.Key == '?' ? 999 : (int)x.Key))
                            {
                                string chuCaiDisplay = kvp.Key == '?' ? "Ký tự đặc biệt" : $"Chữ '{kvp.Key}'";
                                previewMessage += $"      • {chuCaiDisplay}: {kvp.Value} học sinh\n";
                            }
                            previewMessage += "\n";
                        }
                    }

                    if (preview.ContainsKey("SoHSGapLoi") && (int)preview["SoHSGapLoi"] > 0)
                    {
                        previewMessage += $"   ❌ Lỗi xử lý: {preview["SoHSGapLoi"]} học sinh\n\n";
                    }
                }

                previewMessage += "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n";
                previewMessage += "Bạn có muốn tiếp tục phân lớp tự động không?";

                DialogResult result = MessageBox.Show(previewMessage, "Xác nhận phân lớp tự động",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // ✅ Chạy phân lớp tự động trên background thread để không block UI
                    // Truyền HK1, logic sẽ tự động phân cho cả HK1 và HK2
                    _ = PhanLopTuDongAsync(hk1.MaHocKy);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                this.Enabled = true;
                MessageBox.Show($"Đã xảy ra lỗi khi phân lớp tự động:\n{ex.Message}\n\nStack trace:\n{ex.StackTrace}",
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Phân lớp tự động trên background thread để không block UI
        /// </summary>
        private async Task PhanLopTuDongAsync(int maHocKyHienTai)
        {
            try
            {
                // Disable UI và hiển thị progress
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Enabled = false;
                        this.Cursor = Cursors.WaitCursor;
                    });
                }
                else
                {
                    this.Enabled = false;
                    this.Cursor = Cursors.WaitCursor;
                }
                
                // Thực hiện phân lớp tự động trên background thread
                var ketQua = await Task.Run(() => phanLopTuDongBLL.ThucHienPhanLopTuDong(maHocKyHienTai));
                
                // ✅ Update UI trên UI thread
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Cursor = Cursors.Default;
                        this.Enabled = true;

                        if (ketQua.success)
                        {
                            // ✅ Hiển thị thông báo thành công với ScrollableMessageBox nếu có nhiều thông tin
                            string thongBaoThanhCong = $"✓ Phân lớp tự động thành công!\n\n" +
                                           $"Đã phân lớp: {ketQua.soHocSinhDaPhanLop} học sinh\n\n" +
                                           $"{ketQua.message}";
                            
                            // Sử dụng ScrollableMessageBox để xem đầy đủ thông tin
                            ScrollableMessageBox.Show("Thành công", thongBaoThanhCong, MessageBoxIcon.Information);

                            // ✅ Chỉ refresh lại bảng phân lớp của học kỳ vừa phân lớp (không load tất cả)
                            FilterTablePhanLop(); // FilterTablePhanLop sẽ chỉ load học kỳ đã chọn
                            
                            // ✅ Cập nhật trạng thái nút sau khi phân lớp tự động thành công
                            UpdateButtonStates();
                            
                            // Tự động chuyển sang tab Phân lớp để xem kết quả
                            btnPhanLop_Click(null, null);
                        }
                        else
                        {
                            // Kiểm tra nếu message quá dài (> 500 ký tự) thì dùng ScrollableMessageBox
                            if (ketQua.message.Length > 500)
                            {
                                ScrollableMessageBox.Show("Lỗi", $"✗ Phân lớp tự động thất bại!\n\n{ketQua.message}", MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"✗ Phân lớp tự động thất bại!\n\n{ketQua.message}",
                                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    });
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    this.Enabled = true;

                    if (ketQua.success)
                    {
                        string thongBaoThanhCong = $"✓ Phân lớp tự động thành công!\n\n" +
                                       $"Đã phân lớp: {ketQua.soHocSinhDaPhanLop} học sinh\n\n" +
                                       $"{ketQua.message}";
                        ScrollableMessageBox.Show("Thành công", thongBaoThanhCong, MessageBoxIcon.Information);
                        FilterTablePhanLop();
                        // ✅ Cập nhật trạng thái nút sau khi phân lớp tự động thành công
                        UpdateButtonStates();
                        btnPhanLop_Click(null, null);
                    }
                    else
                    {
                        if (ketQua.message.Length > 500)
                        {
                            ScrollableMessageBox.Show("Lỗi", $"✗ Phân lớp tự động thất bại!\n\n{ketQua.message}", MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show($"✗ Phân lớp tự động thất bại!\n\n{ketQua.message}",
                                           "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ✅ Restore UI nếu có lỗi
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Cursor = Cursors.Default;
                        this.Enabled = true;
                        MessageBox.Show($"Lỗi khi phân lớp tự động:\n\n{ex.Message}",
                                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    this.Enabled = true;
                    MessageBox.Show($"Lỗi khi phân lớp tự động:\n\n{ex.Message}",
                                   "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PhanLop_Load(object sender, EventArgs e)
        {
            // Form load event - được gọi tự động khi form được mở
            // Các thao tác khởi tạo đã được thực hiện trong constructor
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // Đóng form hiện tại
        }

        private void btnHocSinh_Click(object sender, EventArgs e)
        {
            // Chức năng này không còn dùng nữa vì đã xóa tableHocSinh
        }

        private void btnPhanLop_Click(object sender, EventArgs e)
        {
            // Chức năng này không còn dùng nữa vì đã xóa tableHocSinh
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterTablePhanLop();
        }

        #region Setup Tables

        private void SetupTables()
        {
            SetupTablePhanLop();
        }

        private void SetupTablePhanLop()
        {
            // Xóa cột cũ và cấu hình chung
            tablePhanLop.Columns.Clear();
            ApplyBaseTableStyle(tablePhanLop);

            // Thêm cột mới
            tablePhanLop.Columns.Add("HocSinh", "Học Sinh");
            tablePhanLop.Columns.Add("Lop", "Lớp");
            tablePhanLop.Columns.Add("HocKy", "Học Kỳ");
            tablePhanLop.Columns.Add("ThaoTac", "Thao tác");

            // Căn chỉnh cột
            ApplyColumnAlignmentAndWrapping(tablePhanLop);
            tablePhanLop.Columns["HocSinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhanLop.Columns["Lop"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablePhanLop.Columns["HocKy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tablePhanLop.Columns["ThaoTac"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Tùy chỉnh kích thước
            tablePhanLop.Columns["HocSinh"].FillWeight = 40; tablePhanLop.Columns["HocSinh"].MinimumWidth = 200;
            tablePhanLop.Columns["Lop"].FillWeight = 20; tablePhanLop.Columns["Lop"].MinimumWidth = 100;
            tablePhanLop.Columns["HocKy"].FillWeight = 25; tablePhanLop.Columns["HocKy"].MinimumWidth = 150;
            tablePhanLop.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablePhanLop.Columns["ThaoTac"].Width = 100;

            // Gắn sự kiện
            tablePhanLop.CellPainting += tablePhanLop_CellPainting;
            tablePhanLop.CellClick += tablePhanLop_CellClick;
        }

        private void UpdateView()
        {
            // Hàm này không còn dùng nữa vì đã xóa chức năng chuyển đổi giữa 2 bảng
        }

        #endregion

        #region Load Data

        private void LoadData()
        {
            // ✅ Load dữ liệu phân lớp của học kỳ đã chọn (tự động là học kỳ "Đang diễn ra")
            FilterTablePhanLop(); // FilterTablePhanLop sẽ chỉ load học kỳ đã chọn trong comboBox
            // ✅ Cập nhật trạng thái nút sau khi load dữ liệu
            UpdateButtonStates();
        }

        private void LoadTablePhanLop()
        {
            // ✅ Load dữ liệu phân lớp của NĂM HỌC đã chọn (cả HK1 và HK2)
            string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
            
            if (string.IsNullOrEmpty(selectedNamHoc) || !danhSachNamHoc.ContainsKey(selectedNamHoc))
            {
                // Nếu chưa chọn năm học, để trống
                danhSachPhanLop = new List<(int, int, int)>();
                danhSachPhanLopGoc = new List<(int, int, int)>();
                RefreshTablePhanLop(danhSachPhanLop);
                return;
            }
            
            // Lấy HK1 và HK2 của năm học được chọn
            var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
            if (hk1 == null || hk2 == null)
            {
                danhSachPhanLop = new List<(int, int, int)>();
                danhSachPhanLopGoc = new List<(int, int, int)>();
                RefreshTablePhanLop(danhSachPhanLop);
                return;
            }
            
            // ✅ Load phân lớp của CẢ HK1 và HK2 của năm học đã chọn
            var allPhanLop = phanLopBLL.GetAllPhanLop();
            danhSachPhanLop = allPhanLop
                .Where(pl => pl.maHocKy == hk1.MaHocKy || pl.maHocKy == hk2.MaHocKy)
                .ToList();
            
            danhSachPhanLopGoc = new List<(int, int, int)>(danhSachPhanLop); // Lưu danh sách gốc để tìm kiếm
            RefreshTablePhanLop(danhSachPhanLop);
        }

        /// <summary>
        /// Cập nhật trạng thái enable/disable của các nút
        /// - "Phân lớp chuyển trường": chỉ enable khi đã phân lớp tự động cho cả HK1 và HK2 VÀ có học kỳ "Đang diễn ra"
        /// - "Nhập Excel": disable khi đã phân lớp tự động cho cả HK1 và HK2, enable khi chưa phân lớp
        /// </summary>
        private void UpdateButtonStates()
        {
            try
            {
                // Kiểm tra đã chọn năm học chưa
                string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedNamHoc) || !danhSachNamHoc.ContainsKey(selectedNamHoc))
                {
                    // Chưa chọn năm học, disable các nút
                    btnPhanLopChuyenTruong.Enabled = false;
                    btnNhapExcel.Enabled = false;
                    btnThemHocSinh.Enabled = false;
                    return;
                }

                // Lấy HK1 và HK2 của năm học được chọn
                var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
                if (hk1 == null || hk2 == null)
                {
                    // Không có đủ HK1 và HK2, disable các nút
                    btnPhanLopChuyenTruong.Enabled = false;
                    btnNhapExcel.Enabled = false;
                    btnThemHocSinh.Enabled = false;
                    return;
                }

                // Kiểm tra trạng thái học kỳ
                string trangThaiHK1 = SemesterHelper.GetStatus(hk1.MaHocKy);
                string trangThaiHK2 = SemesterHelper.GetStatus(hk2.MaHocKy);
                bool hk1DangDienRa = trangThaiHK1 == "Đang diễn ra";
                bool hk2DangDienRa = trangThaiHK2 == "Đang diễn ra";
                bool coHocKyDangDienRa = hk1DangDienRa || hk2DangDienRa;

                // ✅ KIỂM TRA: Đã phân lớp tự động cho cả HK1 và HK2 chưa
                var allPhanLop = phanLopBLL.GetAllPhanLop();
                int soHocSinhDaPhanLopHK1 = allPhanLop.Count(p => p.maHocKy == hk1.MaHocKy);
                int soHocSinhDaPhanLopHK2 = allPhanLop.Count(p => p.maHocKy == hk2.MaHocKy);
                
                // Lấy số học sinh đang học để tính ngưỡng
                int tongSoHocSinhDangHoc = hocSinhBus.GetTotalHocSinhDangHoc();
                
                // Ngưỡng: Phải có ít nhất 50 học sinh được phân lớp HOẶC ít nhất 30% số học sinh đang học
                // Điều này đảm bảo rằng đã có phân lớp tự động, không phải chỉ 1-2 học sinh chuyển trường
                int nguongToiThieu = Math.Max(50, (int)(tongSoHocSinhDangHoc * 0.3));
                
                // Chỉ enable nút "Phân lớp chuyển trường" khi:
                // 1. Cả HK1 và HK2 đều đã có phân lớp tự động (đạt ngưỡng)
                // 2. Có ít nhất một học kỳ "Đang diễn ra"
                bool daPhanLopTuDongHK1 = soHocSinhDaPhanLopHK1 >= nguongToiThieu;
                bool daPhanLopTuDongHK2 = soHocSinhDaPhanLopHK2 >= nguongToiThieu;
                
                btnPhanLopChuyenTruong.Enabled = daPhanLopTuDongHK1 && daPhanLopTuDongHK2 && coHocKyDangDienRa;

                // ✅ LOGIC CHO NÚT "NHẬP EXCEL" VÀ "THÊM HỌC SINH":
                // Disable nếu đã phân lớp tự động cho CẢ HK1 VÀ HK2 (không cần quan tâm "Đang diễn ra")
                // Vì các nút này dùng cho tuyển sinh (học sinh chưa được phân lớp)
                // Enable nếu chưa phân lớp tự động cho cả HK1 và HK2
                if (daPhanLopTuDongHK1 && daPhanLopTuDongHK2)
                {
                    // Đã phân lớp tự động cho cả HK1 và HK2 → disable các nút tuyển sinh
                    btnNhapExcel.Enabled = false;
                    btnThemHocSinh.Enabled = false;
                }
                else
                {
                    // Chưa phân lớp tự động cho cả HK1 và HK2 → enable các nút tuyển sinh
                    btnNhapExcel.Enabled = true;
                    btnThemHocSinh.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, disable các nút để an toàn
                btnPhanLopChuyenTruong.Enabled = false;
                btnNhapExcel.Enabled = false;
                btnThemHocSinh.Enabled = false;
                Console.WriteLine($"Lỗi khi cập nhật trạng thái nút: {ex.Message}");
            }
        }

        #endregion

        #region Event Handlers

        private void tableHocSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Hàm này không còn dùng nữa vì đã xóa tableHocSinh
        }

        private void tablePhanLop_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tablePhanLop.Columns["ThaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                Image deleteIcon = Properties.Resources.bin;

                int iconSize = 18;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - iconSize) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle deleteRect = new Rectangle(startX, y, iconSize, iconSize);
                e.Graphics.DrawImage(deleteIcon, deleteRect);

                e.Handled = true;
            }
        }

        

        #endregion

        #region Event Handlers

        private void SetupEventHandlers()
        {
            // Event handler cho txtTimKiem - bây giờ dùng cho tablePhanLop
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            
            // Event handler cho btnChon
            btnChon.Click += btnChon_Click;
            
            // Event handler cho ComboBox
            cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
            cbLop.SelectedIndexChanged += cbLop_SelectedIndexChanged;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimKiem.Text.Trim().ToLower();
            
            List<(int maHocSinh, int maLop, int maHocKy)> filteredPhanLop;
            
            if (string.IsNullOrEmpty(searchText))
            {
                // Nếu ô tìm kiếm trống, hiển thị tất cả phân lớp
                filteredPhanLop = new List<(int, int, int)>(danhSachPhanLopGoc);
            }
            else
            {
                // Lọc phân lớp theo tên học sinh
                filteredPhanLop = danhSachPhanLopGoc.Where(pl =>
                {
                    string tenHocSinh = hocSinhBus.GetHocSinhById(pl.maHocSinh)?.HoTen ?? "";
                    return tenHocSinh.ToLower().Contains(searchText) ||
                           pl.maHocSinh.ToString().Contains(searchText);
                }).ToList();
            }
            
            // Cập nhật lại bảng
            RefreshTablePhanLop(filteredPhanLop);
        }

        private void tableHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Hàm này không còn dùng nữa vì đã xóa tableHocSinh
        }

        private void tableHocSinh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Hàm này không còn dùng nữa vì đã xóa tableHocSinh
        }

        private void RefreshTableHocSinh()
        {
            // Hàm này không còn dùng nữa vì đã xóa tableHocSinh
        }


        private void FilterTablePhanLop()
        {
            // ✅ Load dữ liệu phân lớp của NĂM HỌC đã chọn (cả HK1 và HK2)
            string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
            string selectedLop = cbLop.SelectedItem?.ToString();

            // ✅ Nếu không có năm học được chọn, để trống
            if (string.IsNullOrEmpty(selectedNamHoc) || !danhSachNamHoc.ContainsKey(selectedNamHoc))
            {
                danhSachPhanLop = new List<(int, int, int)>();
                danhSachPhanLopGoc = new List<(int, int, int)>();
                RefreshTablePhanLop(danhSachPhanLop);
                return;
            }

            // Lấy HK1 và HK2 của năm học được chọn
            var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
            if (hk1 == null || hk2 == null)
            {
                danhSachPhanLop = new List<(int, int, int)>();
                danhSachPhanLopGoc = new List<(int, int, int)>();
                RefreshTablePhanLop(danhSachPhanLop);
                return;
            }

            // ✅ Lấy phân lớp của CẢ HK1 và HK2 của năm học đã chọn
            var allPhanLop = phanLopBLL.GetAllPhanLop();
            var phanLopByNamHoc = allPhanLop
                .Where(pl => pl.maHocKy == hk1.MaHocKy || pl.maHocKy == hk2.MaHocKy)
                .ToList();
            
            // ✅ KIỂM TRA: Năm học đã phân lớp tự động chưa?
            int tongSoHocSinhDangHoc = hocSinhBus.GetTotalHocSinhDangHoc();
            int nguongToiThieu = Math.Max(50, (int)(tongSoHocSinhDangHoc * 0.3));
            int soHocSinhDaPhanLopHK1 = phanLopByNamHoc.Count(p => p.maHocKy == hk1.MaHocKy);
            int soHocSinhDaPhanLopHK2 = phanLopByNamHoc.Count(p => p.maHocKy == hk2.MaHocKy);
            bool daPhanLopTuDongHK1 = soHocSinhDaPhanLopHK1 >= nguongToiThieu;
            bool daPhanLopTuDongHK2 = soHocSinhDaPhanLopHK2 >= nguongToiThieu;
            bool daPhanLopTuDong = daPhanLopTuDongHK1 || daPhanLopTuDongHK2;

            // ✅ Nếu chưa phân lớp tự động → thêm học sinh chưa phân lớp từ Excel
            List<(int maHocSinh, int maLop, int maHocKy)> danhSachHienThi = new List<(int, int, int)>(phanLopByNamHoc);
            
            if (!daPhanLopTuDong)
            {
                // Lấy tất cả học sinh "Đang học" chưa có phân lớp cho HK1 và HK2
                var allHocSinh = hocSinhBus.GetAllHocSinh();
                var hocSinhDangHoc = allHocSinh.Where(hs => hs.TrangThai == "Đang học").ToList();
                
                // Lấy danh sách mã học sinh đã có phân lớp
                var maHocSinhDaPhanLop = phanLopByNamHoc.Select(pl => pl.maHocSinh).ToHashSet();
                
                // Lọc ra học sinh chưa phân lớp
                var hocSinhChuaPhanLop = hocSinhDangHoc
                    .Where(hs => !maHocSinhDaPhanLop.Contains(hs.MaHS))
                    .ToList();
                
                // Thêm vào danh sách hiển thị với maLop = 0 và maHocKy = hk1.MaHocKy (để đánh dấu chưa phân lớp)
                foreach (var hs in hocSinhChuaPhanLop)
                {
                    danhSachHienThi.Add((hs.MaHS, 0, hk1.MaHocKy)); // maLop = 0 nghĩa là chưa phân lớp
                }
            }
            
            // Lọc thêm theo lớp nếu có
            var filteredPhanLop = danhSachHienThi.Where(pl =>
            {
                // Kiểm tra lớp
                bool lopMatch = true;
                if (selectedLop != "Chọn lớp" && !string.IsNullOrEmpty(selectedLop))
                {
                    // Nếu chưa phân lớp (maLop = 0), không lọc theo lớp
                    if (pl.maLop == 0)
                    {
                        lopMatch = false; // Ẩn học sinh chưa phân lớp khi lọc theo lớp
                    }
                    else
                    {
                        string tenLop = "";
                        foreach (var lop in danhSachLop)
                        {
                            if (lop.MaLop == pl.maLop)
                            {
                                tenLop = lop.TenLop;
                                break;
                            }
                        }
                        lopMatch = tenLop == selectedLop;
                    }
                }

                return lopMatch;
            }).ToList();

            // Cập nhật danh sách gốc và bảng
            danhSachPhanLop = filteredPhanLop;
            danhSachPhanLopGoc = new List<(int, int, int)>(filteredPhanLop);
            RefreshTablePhanLop(filteredPhanLop);
        }

        private void RefreshTablePhanLop(List<(int maHocSinh, int maLop, int maHocKy)> phanLopList)
        {
            tablePhanLop.Rows.Clear();
            
            foreach (var pl in phanLopList)
            {
                string tenHocSinh = hocSinhBus.GetHocSinhById(pl.maHocSinh)?.HoTen ?? $"HS {pl.maHocSinh}";

                // ✅ Nếu maLop = 0 → học sinh chưa phân lớp
                string tenLop = "";
                if (pl.maLop == 0)
                {
                    tenLop = "Chưa phân lớp";
                }
                else
                {
                    foreach (var lop in danhSachLop)
                    {
                        if (lop.MaLop == pl.maLop)
                        {
                            tenLop = lop.TenLop;
                            break;
                        }
                    }
                }

                string tenHocKy = "";
                foreach (var hk in danhSachHocKy)
                {
                    if (hk.MaHocKy == pl.maHocKy)
                    {
                        tenHocKy = hk.TenHocKy + "-" + hk.MaNamHoc;
                        break;
                    }
                }

                tablePhanLop.Rows.Add(tenHocSinh, tenLop, tenHocKy, "");
            }
        }

        private void tablePhanLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tablePhanLop.Columns["ThaoTac"].Index)
            {
                try
                {
                    // Lấy thông tin phân lớp từ danh sách hiện tại (đã được lọc)
                    var phanLopToDelete = GetPhanLopFromFilteredList(e.RowIndex);
                    
                    if (phanLopToDelete.maHocSinh == -1)
                    {
                        MessageBox.Show("Không thể lấy thông tin phân lớp để xóa.", "Lỗi", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int maHS = phanLopToDelete.maHocSinh;
                    int maLop = phanLopToDelete.maLop;
                    int maHocKy = phanLopToDelete.maHocKy;

                    // Lấy tên học sinh để hiển thị
                    var hocSinh = hocSinhBus.GetHocSinhById(maHS);
                    if (hocSinh == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin học sinh.", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    string tenHocSinh = hocSinh.HoTen ?? $"HS {maHS}";
                    string trangThaiHS = hocSinh.TrangThai ?? "";

                    // ✅ TRƯỜNG HỢP 1: Kiểm tra điều kiện xóa phân lớp
                    // Không cho xóa nếu trạng thái là "Đang học", "Đang học(CT)", "Nghỉ học"
                    if (trangThaiHS == "Đang học" || trangThaiHS == "Đang học(CT)" || trangThaiHS == "Nghỉ học")
                    {
                        MessageBox.Show($"⚠️ KHÔNG THỂ XÓA PHÂN LỚP!\n\n" +
                                       $"Học sinh {tenHocSinh} có trạng thái '{trangThaiHS}'.\n\n" +
                                       $"Chỉ có thể xóa phân lớp khi học sinh có trạng thái 'Bảo lưu' hoặc 'Thôi học'.",
                                       "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Chỉ cho phép xóa nếu trạng thái là "Bảo lưu" hoặc "Thôi học"
                    if (trangThaiHS != "Bảo lưu" && trangThaiHS != "Thôi học")
                    {
                        MessageBox.Show($"⚠️ KHÔNG THỂ XÓA PHÂN LỚP!\n\n" +
                                       $"Học sinh {tenHocSinh} có trạng thái '{trangThaiHS}'.\n\n" +
                                       $"Chỉ có thể xóa phân lớp khi học sinh có trạng thái 'Bảo lưu' hoặc 'Thôi học'.",
                                       "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ✅ Kiểm tra trạng thái học kỳ: HK1 & HK2 phải có ít nhất 1 "Đang diễn ra" hoặc cả 2 "Chưa bắt đầu"
                    string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
                    if (string.IsNullOrEmpty(selectedNamHoc) || !danhSachNamHoc.ContainsKey(selectedNamHoc))
                    {
                        MessageBox.Show("Không thể xác định năm học để kiểm tra trạng thái học kỳ.", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
                    if (hk1 == null || hk2 == null)
                    {
                        MessageBox.Show("Không thể xác định HK1 và HK2 để kiểm tra trạng thái học kỳ.", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string trangThaiHK1 = SemesterHelper.GetStatus(hk1.MaHocKy);
                    string trangThaiHK2 = SemesterHelper.GetStatus(hk2.MaHocKy);
                    bool hk1DangDienRa = trangThaiHK1 == "Đang diễn ra";
                    bool hk2DangDienRa = trangThaiHK2 == "Đang diễn ra";
                    bool hk1ChuaBatDau = trangThaiHK1 == "Chưa bắt đầu";
                    bool hk2ChuaBatDau = trangThaiHK2 == "Chưa bắt đầu";

                    // Kiểm tra: Phải có ít nhất 1 "Đang diễn ra" HOẶC cả 2 đều "Chưa bắt đầu"
                    bool coTheXoa = (hk1DangDienRa || hk2DangDienRa) || (hk1ChuaBatDau && hk2ChuaBatDau);
                    
                    if (!coTheXoa)
                    {
                        MessageBox.Show($"⚠️ KHÔNG THỂ XÓA PHÂN LỚP!\n\n" +
                                       $"Năm học {selectedNamHoc}:\n" +
                                       $"• HK1: {trangThaiHK1}\n" +
                                       $"• HK2: {trangThaiHK2}\n\n" +
                                       $"Chỉ có thể xóa khi:\n" +
                                       $"• Có ít nhất 1 học kỳ 'Đang diễn ra', HOẶC\n" +
                                       $"• Cả 2 học kỳ đều 'Chưa bắt đầu'.",
                                       "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (MessageBox.Show($"Bạn có chắc muốn xóa phân lớp của học sinh {tenHocSinh} (Mã HS: {maHS})?\n\n" +
                                       $"Trạng thái học sinh: {trangThaiHS}\n" +
                                       $"Năm học: {selectedNamHoc}",
                                       "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (phanLopBLL.DeletePhanLop(maHS, maLop, maHocKy))
                        {
                            MessageBox.Show("Đã xóa phân lớp thành công.", "Thành công", 
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // ✅ Chỉ cập nhật lại bảng phân lớp của học kỳ hiện tại (không load tất cả)
                            FilterTablePhanLop();
                            // ✅ Cập nhật trạng thái nút sau khi xóa phân lớp
                            UpdateButtonStates();
                        }
                        else
                        {
                            MessageBox.Show("Xóa phân lớp thất bại.", "Lỗi", 
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xóa phân lớp: " + ex.Message, "Lỗi", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private (int maHocSinh, int maLop, int maHocKy) GetPhanLopFromFilteredList(int rowIndex)
        {
            // Lấy thông tin từ bảng hiện tại để tìm lại trong danh sách gốc
            if (rowIndex >= 0 && rowIndex < tablePhanLop.Rows.Count)
            {
                string tenHocSinh = tablePhanLop.Rows[rowIndex].Cells["HocSinh"].Value?.ToString();
                string tenLop = tablePhanLop.Rows[rowIndex].Cells["Lop"].Value?.ToString();
                string tenHocKy = tablePhanLop.Rows[rowIndex].Cells["HocKy"].Value?.ToString();

                // Tìm trong danh sách phân lớp gốc
                foreach (var pl in danhSachPhanLop)
                {
                    // Lấy tên học sinh
                    string tenHS = hocSinhBus.GetHocSinhById(pl.maHocSinh)?.HoTen ?? $"HS {pl.maHocSinh}";
                    
                    // Lấy tên lớp
                    string tenLopFromPl = "";
                    foreach (var lop in danhSachLop)
                    {
                        if (lop.MaLop == pl.maLop)
                        {
                            tenLopFromPl = lop.TenLop;
                            break;
                        }
                    }
                    
                    // Lấy tên học kỳ
                    string tenHocKyFromPl = "";
                    foreach (var hk in danhSachHocKy)
                    {
                        if (hk.MaHocKy == pl.maHocKy)
                        {
                            tenHocKyFromPl = hk.TenHocKy + "-" + hk.MaNamHoc;
                            break;
                        }
                    }

                    // So sánh để tìm đúng phân lớp
                    if (tenHS == tenHocSinh && tenLopFromPl == tenLop && tenHocKyFromPl == tenHocKy)
                    {
                        return pl;
                    }
                }
            }
            
            return (-1, -1, -1); // Không tìm thấy
        }

        #endregion

        #region Helper Methods

        private void ApplyBaseTableStyle(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ColumnHeadersHeight = 42;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.RowTemplate.Height = 46;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Đảm bảo màu header không đổi khi click
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
        }

        private void ApplyColumnAlignmentAndWrapping(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }

        private void FormatGenderCell(DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold);
            e.CellStyle.Padding = new Padding(5, 3, 5, 3);

            if (e.Value.ToString() == "Nam")
            {
                e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216);
                e.CellStyle.BackColor = Color.FromArgb(219, 234, 254);
            }
            else if (e.Value.ToString() == "Nữ")
            {
                e.CellStyle.ForeColor = Color.FromArgb(190, 24, 93);
                e.CellStyle.BackColor = Color.FromArgb(253, 232, 255);
            }
        }
        

        private void FormatStatusCell(DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold);
            e.CellStyle.Padding = new Padding(5, 3, 5, 3);

            if (e.Value.ToString() == "Đang học")
            {
                e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
            }
            else
            {
                e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
            }
        }

        #endregion

        private void btnPhanLop_Click_1(object sender, EventArgs e)
        {

        }

        private void btnThemHocSinh_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Kiểm tra đã chọn năm học chưa
                string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedNamHoc) || !danhSachNamHoc.ContainsKey(selectedNamHoc))
                {
                    MessageBox.Show("Vui lòng chọn năm học.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy HK1 và HK2 của năm học được chọn
                var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
                if (hk1 == null || hk2 == null)
                {
                    MessageBox.Show($"Năm học {selectedNamHoc} phải có đầy đủ HK1 và HK2!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ Kiểm tra: Nếu đã phân lớp tự động cho cả HK1 và HK2 → không cho thêm học sinh
                var allPhanLop = phanLopBLL.GetAllPhanLop();
                int soHocSinhDaPhanLopHK1 = allPhanLop.Count(p => p.maHocKy == hk1.MaHocKy);
                int soHocSinhDaPhanLopHK2 = allPhanLop.Count(p => p.maHocKy == hk2.MaHocKy);
                
                int tongSoHocSinhDangHoc = hocSinhBus.GetTotalHocSinhDangHoc();
                int nguongToiThieu = Math.Max(50, (int)(tongSoHocSinhDangHoc * 0.3));//nghĩa là tối thiểu 50 học sinh và 30% tổng số học sinh đang học
                
                bool daPhanLopTuDongHK1 = soHocSinhDaPhanLopHK1 >= nguongToiThieu;
                bool daPhanLopTuDongHK2 = soHocSinhDaPhanLopHK2 >= nguongToiThieu;
                
                if (daPhanLopTuDongHK1 && daPhanLopTuDongHK2)
                {
                    MessageBox.Show($"⚠️ KHÔNG THỂ THÊM HỌC SINH!\n\n" +
                                   $"Năm học {selectedNamHoc} đã được phân lớp tự động.\n\n" +
                                   $"Vui lòng chọn năm học khác (chưa phân lớp) để thêm học sinh tuyển sinh.",
                                   "Không thể thêm học sinh",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở form thêm học sinh
                ThemHoSoHocSinh frmThemHocSinh = new ThemHoSoHocSinh();
                frmThemHocSinh.StartPosition = FormStartPosition.CenterScreen;

                // Hiển thị form dưới dạng Dialog và chờ kết quả
                DialogResult result = frmThemHocSinh.ShowDialog(this);

                // ✅ Kiểm tra kết quả trả về từ form
                if (result == DialogResult.OK)
                {
                    try
                    {
                        // ✅ Lấy học sinh vừa tạo từ form
                        HocSinhDTO newHS = frmThemHocSinh.NewHocSinh;

                        if (newHS != null)
                        {
                            // ✅ Refresh lại bảng phân lớp để hiển thị học sinh vừa thêm
                            FilterTablePhanLop();
                            
                            // ✅ Cập nhật trạng thái nút sau khi thêm học sinh
                            UpdateButtonStates();

                            MessageBox.Show($"✅ Thêm học sinh thành công!\n\n" +
                                           $"Học sinh '{newHS.HoTen}' đã được thêm vào hệ thống.\n\n" +
                                           $"Học sinh này sẽ được hiển thị trong bảng phân lớp và sẽ được phân vào lớp 10 khi thực hiện 'Phân lớp tự động'.",
                                           "Thêm học sinh thành công",
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Đã xảy ra lỗi sau khi thêm học sinh:\n{ex.Message}",
                                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi mở form thêm học sinh:\n{ex.Message}",
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Kiểm tra đã chọn năm học chưa
                string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedNamHoc) || !danhSachNamHoc.ContainsKey(selectedNamHoc))
                {
                    MessageBox.Show("Vui lòng chọn năm học.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy HK1 và HK2 của năm học được chọn
                var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
                if (hk1 == null || hk2 == null)
                {
                    MessageBox.Show($"Năm học {selectedNamHoc} phải có đầy đủ HK1 và HK2!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra trạng thái học kỳ
                string trangThaiHK1 = SemesterHelper.GetStatus(hk1.MaHocKy);// SemesterHelper là class helper để lấy trạng thái của học kỳ
                string trangThaiHK2 = SemesterHelper.GetStatus(hk2.MaHocKy);
                bool hk1DangDienRa = trangThaiHK1 == "Đang diễn ra";
                bool hk2DangDienRa = trangThaiHK2 == "Đang diễn ra";
                bool coHocKyDangDienRa = hk1DangDienRa || hk2DangDienRa;

                // ✅ KIỂM TRA: Nếu HK1 & HK2 "Đang diễn ra" và đã phân lớp → không cho nhập Excel
                var allPhanLop = phanLopBLL.GetAllPhanLop();
                int soHocSinhDaPhanLopHK1 = allPhanLop.Count(p => p.maHocKy == hk1.MaHocKy);
                int soHocSinhDaPhanLopHK2 = allPhanLop.Count(p => p.maHocKy == hk2.MaHocKy);
                
                int tongSoHocSinhDangHoc = hocSinhBus.GetTotalHocSinhDangHoc();
                int nguongToiThieu = Math.Max(50, (int)(tongSoHocSinhDangHoc * 0.3));
                
                bool daPhanLopTuDongHK1 = soHocSinhDaPhanLopHK1 >= nguongToiThieu;
                bool daPhanLopTuDongHK2 = soHocSinhDaPhanLopHK2 >= nguongToiThieu;
                
                if (coHocKyDangDienRa && (daPhanLopTuDongHK1 || daPhanLopTuDongHK2))
                {
                    MessageBox.Show($"⚠️ KHÔNG THỂ NHẬP EXCEL!\n\n" +
                                   $"Năm học {selectedNamHoc} đang diễn ra và đã được phân lớp tự động.\n\n" +
                                   $"Vui lòng chọn năm học khác (chưa phân lớp) để nhập Excel.",
                                   "Không thể nhập Excel",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở file dialog để chọn file Excel
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Files|*.xlsx;*.xls",
                    Title = "Chọn file Excel để nhập học sinh tuyển sinh (HocSinh, PhuHuynh, MoiQuanHe)"
                };

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return; // Người dùng hủy
                }

                string filePath = openFileDialog.FileName;

                // Hiển thị thông báo đang xử lý
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // Lấy học kỳ để nhập (ưu tiên HK1, nếu không có thì HK2)
                var hocKyDeNhap = hk1DangDienRa ? hk1 : (hk2DangDienRa ? hk2 : hk1);

                // Gọi hàm nhập Excel cho học sinh tuyển sinh
                ImportExcelTuyenSinh(filePath, hocKyDeNhap, hk1, hk2);

                this.Cursor = Cursors.Default;

                // ✅ Refresh lại bảng phân lớp để hiển thị học sinh vừa nhập
                FilterTablePhanLop();
                
                // ✅ Cập nhật trạng thái nút sau khi nhập Excel
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Đã xảy ra lỗi khi nhập Excel:\n{ex.Message}\n\nStack trace:\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPhanLopChuyenTruong_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Kiểm tra đã chọn năm học chưa
                string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedNamHoc) || !danhSachNamHoc.ContainsKey(selectedNamHoc))
                {
                    MessageBox.Show("Vui lòng chọn năm học.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy HK1 và HK2 của năm học được chọn
                var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
                if (hk1 == null || hk2 == null)
                {
                    MessageBox.Show($"Năm học {selectedNamHoc} phải có đầy đủ HK1 và HK2!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra trạng thái học kỳ
                string trangThaiHK1 = SemesterHelper.GetStatus(hk1.MaHocKy);
                string trangThaiHK2 = SemesterHelper.GetStatus(hk2.MaHocKy);
                
                if (trangThaiHK1 != "Đang diễn ra" && trangThaiHK2 != "Đang diễn ra")
                {
                    MessageBox.Show($"Cả HK1 và HK2 của năm học {selectedNamHoc} đều không phải 'Đang diễn ra'.\n\nVui lòng kiểm tra lại cấu hình học kỳ.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ KIỂM TRA: Cả HK1 và HK2 PHẢI đã được phân lớp tự động rồi mới cho phép phân lớp chuyển trường
                var allPhanLop = phanLopBLL.GetAllPhanLop();
                int soHocSinhDaPhanLopHK1 = allPhanLop.Count(p => p.maHocKy == hk1.MaHocKy);
                int soHocSinhDaPhanLopHK2 = allPhanLop.Count(p => p.maHocKy == hk2.MaHocKy);
                
                // Lấy số học sinh đang học để tính ngưỡng
                int tongSoHocSinhDangHoc = hocSinhBus.GetTotalHocSinhDangHoc();
                
                // Ngưỡng: Phải có ít nhất 50 học sinh được phân lớp HOẶC ít nhất 30% số học sinh đang học
                // Điều này đảm bảo rằng đã có phân lớp tự động, không phải chỉ 1-2 học sinh chuyển trường
                int nguongToiThieu = Math.Max(50, (int)(tongSoHocSinhDangHoc * 0.3));
                
                bool daPhanLopTuDongHK1 = soHocSinhDaPhanLopHK1 >= nguongToiThieu;
                bool daPhanLopTuDongHK2 = soHocSinhDaPhanLopHK2 >= nguongToiThieu;
                
                if (!daPhanLopTuDongHK1 || !daPhanLopTuDongHK2)
                {
                    string thongBao = $"⚠️ CHƯA THỂ PHÂN LỚP CHUYỂN TRƯỜNG!\n\n";
                    thongBao += $"Năm học: {selectedNamHoc}\n\n";
                    thongBao += $"HK1 ({hk1.TenHocKy}): {soHocSinhDaPhanLopHK1} học sinh (Ngưỡng: {nguongToiThieu})\n";
                    thongBao += $"HK2 ({hk2.TenHocKy}): {soHocSinhDaPhanLopHK2} học sinh (Ngưỡng: {nguongToiThieu})\n\n";
                    
                    if (!daPhanLopTuDongHK1 && !daPhanLopTuDongHK2)
                    {
                        thongBao += "❌ Cả HK1 và HK2 đều chưa được phân lớp tự động!\n\n";
                    }
                    else if (!daPhanLopTuDongHK1)
                    {
                        thongBao += "❌ HK1 chưa được phân lớp tự động!\n\n";
                    }
                    else
                    {
                        thongBao += "❌ HK2 chưa được phân lớp tự động!\n\n";
                    }
                    
                    thongBao += "Vui lòng thực hiện 'Phân lớp tự động' cho cả HK1 và HK2 trước khi phân lớp chuyển trường.";
                    
                    MessageBox.Show(thongBao, "Chưa thể phân lớp chuyển trường",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // CHẶN NGAY, KHÔNG CHO PHÂN LỚP CHUYỂN TRƯỜNG
                }

                // Lấy học kỳ đang diễn ra để nhập Excel (ưu tiên HK1, nếu không có thì HK2)
                var hocKyHienTai = trangThaiHK1 == "Đang diễn ra" ? hk1 : hk2;

                // Mở file dialog để chọn file Excel
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Excel Files|*.xlsx;*.xls",
                    Title = "Chọn file Excel để nhập dữ liệu phân lớp chuyển trường"
                };

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return; // Người dùng hủy
                }

                string filePath = openFileDialog.FileName;

                // Hiển thị thông báo đang xử lý
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // Gọi hàm nhập Excel
                ImportExcelPhanLopChuyenTruong(filePath, hocKyHienTai);

                this.Cursor = Cursors.Default;

                // ✅ Chỉ refresh lại bảng phân lớp của học kỳ vừa nhập Excel (không load tất cả)
                FilterTablePhanLop(); // FilterTablePhanLop sẽ chỉ load học kỳ đã chọn
                
                // ✅ Cập nhật trạng thái nút sau khi nhập Excel phân lớp chuyển trường
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Đã xảy ra lỗi khi nhập Excel:\n{ex.Message}\n\nStack trace:\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hàm chính để nhập Excel cho phân lớp chuyển trường
        /// ✅ Phân lớp cho CẢ HK1 và HK2 cùng lúc
        /// </summary>
        private void ImportExcelPhanLopChuyenTruong(string filePath, HocKyDTO hocKyHienTai)
        {
            // ✅ Set LicenseContext cho EPPlus (bắt buộc từ phiên bản 5.0+)
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Kiểm tra xem file có ít nhất 6 worksheet không
                if (package.Workbook.Worksheets.Count < 6)
                {
                    throw new Exception("File Excel phải có ít nhất 6 worksheet: HocSinh, PhuHuynh, MoiQuanHe, Diem, HanhKiem, XepLoai");
                }

                // Đọc từng worksheet
                var wsHocSinh = package.Workbook.Worksheets["HocSinh"] ?? package.Workbook.Worksheets[0];
                var wsPhuHuynh = package.Workbook.Worksheets["PhuHuynh"] ?? package.Workbook.Worksheets[1];
                var wsMoiQuanHe = package.Workbook.Worksheets["MoiQuanHe"] ?? package.Workbook.Worksheets[2];
                var wsDiem = package.Workbook.Worksheets["Diem"] ?? package.Workbook.Worksheets[3];
                var wsHanhKiem = package.Workbook.Worksheets["HanhKiem"] ?? package.Workbook.Worksheets[4];
                var wsXepLoai = package.Workbook.Worksheets["XepLoai"] ?? package.Workbook.Worksheets[5];

                // ✅ BƯỚC 0: KIỂM TRA HỌC KỲ TRƯỚC KHI THÊM BẤT KỲ DỮ LIỆU NÀO
                // Đọc dữ liệu học sinh từ Excel (chưa thêm vào DB) để kiểm tra học kỳ
                Dictionary<string, (int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhTuExcel = 
                    DocDuLieuHocSinhTuExcel(wsHocSinh, hocKyHienTai);
                
                // Lọc ra danh sách học sinh đủ điều kiện (có đủ học kỳ cần thiết)
                // Học sinh không đủ điều kiện sẽ bị loại bỏ, KHÔNG được thêm vào DB
                HashSet<string> hocSinhDuDieuKien = LocHocSinhDuDieuKien(hocSinhTuExcel, hocKyHienTai);
                
                if (hocSinhDuDieuKien.Count == 0)
                {
                    MessageBox.Show("Không có học sinh nào đủ điều kiện chuyển trường. Vui lòng kiểm tra lại dữ liệu Excel và cấu hình học kỳ.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Nhập Học Sinh với trạng thái "Đang học(CT)"
                // CHỈ nhập những học sinh đã được xác nhận đủ điều kiện
                // Trả về Dictionary: tên học sinh -> (mã học sinh, dòng Excel, khối, ngày chuyển vào, nguyện vọng) để track học sinh thành công
                Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong = 
                    ImportHocSinhFromWorksheetChuyenTruong(wsHocSinh, hocKyHienTai, hocSinhDuDieuKien);

                if (hocSinhThanhCong.Count == 0)
                {
                    MessageBox.Show("Không có học sinh nào được nhập thành công. Vui lòng kiểm tra lại dữ liệu Excel.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Chỉ nhập Phụ Huynh của học sinh đã nhập thành công
                // Nếu phụ huynh lỗi thì rollback học sinh và DỪNG LẠI
                HashSet<int> phuHuynhMoiTao = new HashSet<int>();
                Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong = 
                    ImportPhuHuynhFromWorksheetChuyenTruong(wsPhuHuynh, hocSinhThanhCong, out phuHuynhMoiTao);

                // ✅ KIỂM TRA: Nếu sau khi nhập phụ huynh, không còn học sinh nào thì DỪNG LẠI
                if (hocSinhThanhCong.Count == 0)
                {
                    MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập phụ huynh. Quá trình nhập Excel đã dừng lại.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Chỉ nhập Mối Quan Hệ của học sinh đã nhập thành công
                ImportMoiQuanHeFromWorksheetChuyenTruong(wsMoiQuanHe, hocSinhThanhCong, phuHuynhThanhCong);

                // ✅ KIỂM TRA: Nếu sau khi nhập mối quan hệ, không còn học sinh nào thì DỪNG LẠI
                if (hocSinhThanhCong.Count == 0)
                {
                    MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập mối quan hệ. Quá trình nhập Excel đã dừng lại.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Nhập Điểm, Hạnh kiểm, Xếp loại cho học sinh đã nhập thành công
                ImportDiemHanhKiemXepLoaiFromExcel(wsDiem, wsHanhKiem, wsXepLoai, hocSinhThanhCong, hocKyHienTai, phuHuynhThanhCong);

                // ✅ KIỂM TRA: Nếu sau khi nhập điểm/hạnh kiểm/xếp loại, không còn học sinh nào thì DỪNG LẠI
                if (hocSinhThanhCong.Count == 0)
                {
                    MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập điểm, hạnh kiểm, xếp loại. Quá trình nhập Excel đã dừng lại.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5. Kiểm tra điều kiện và tự động phân lớp cho CẢ HK1 và HK2
                // ✅ Lấy HK1 và HK2 của năm học từ dropdown
                string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
                HocKyDTO hk1ForPhanLop = null;
                HocKyDTO hk2ForPhanLop = null;
                
                if (!string.IsNullOrEmpty(selectedNamHoc) && danhSachNamHoc.ContainsKey(selectedNamHoc))
                {
                    var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
                    hk1ForPhanLop = hk1;
                    hk2ForPhanLop = hk2;
                }
                else
                {
                    // Fallback: Nếu không lấy được từ dropdown, dùng hocKyHienTai và tìm học kỳ còn lại
                    hk1ForPhanLop = hocKyHienTai;
                    // Tìm HK2 từ danh sách học kỳ (cùng năm học - dùng MaNamHoc)
                    var allHocKy = hocKyBus.DocDSHocKy();
                    var hocKyCungNamHoc = allHocKy.Where(hk => 
                        hk.MaNamHoc == hocKyHienTai.MaNamHoc && 
                        hk.MaHocKy != hocKyHienTai.MaHocKy).FirstOrDefault();
                    hk2ForPhanLop = hocKyCungNamHoc;
                }
                
                if (hk1ForPhanLop != null && hk2ForPhanLop != null)
                {
                    PhanLopTuDongChoHocSinhChuyenTruong(hocSinhThanhCong, hk1ForPhanLop, hk2ForPhanLop);
                }
                else
                {
                    MessageBox.Show("Không thể phân lớp: Không tìm thấy đủ thông tin HK1 và HK2.", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // ✅ CHỈ hiển thị thông báo thành công nếu còn học sinh
                if (hocSinhThanhCong.Count > 0)
                {
                    MessageBox.Show($"✅ Nhập Excel thành công!\n\nĐã nhập {hocSinhThanhCong.Count} học sinh chuyển trường và tự động phân lớp.",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Nhập học sinh từ worksheet (trạng thái sẽ được đặt thành "Đang học(CT)")
        /// CHỈ nhập những học sinh trong danh sách hocSinhDuDieuKien
        /// </summary>
        private Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> 
            ImportHocSinhFromWorksheetChuyenTruong(ExcelWorksheet ws, HocKyDTO hocKyHienTai, HashSet<string> hocSinhDuDieuKien)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            int errorCount = 0;
            int successCount = 0;
            StringBuilder errors = new StringBuilder();
            Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong = 
                new Dictionary<string, (int, int, string, DateTime, string)>();
            HashSet<string> sdtDaNhap = new HashSet<string>();
            HashSet<string> emailDaNhap = new HashSet<string>();

            // Tính 1/3 thời gian học kỳ
            DateTime motPhanBaHocKy = DateTime.MinValue;
            if (hocKyHienTai.NgayBD.HasValue && hocKyHienTai.NgayKT.HasValue)
            {
                TimeSpan khoangThoiGian = hocKyHienTai.NgayKT.Value - hocKyHienTai.NgayBD.Value;
                motPhanBaHocKy = hocKyHienTai.NgayBD.Value.AddDays(khoangThoiGian.TotalDays / 3.0);
            }

            // ✅ Tự động phát hiện vị trí cột bằng cách đọc header row
            int colHoTen = -1, colNgaySinh = -1, colGioiTinh = -1, colSdt = -1, colEmail = -1, 
                colTrangThai = -1, colKhoi = -1, colNgayChuyenVao = -1, colNguyenVong = -1;
            
            // Đọc header row (dòng 1) để tìm vị trí cột
            int headerRow = 1;
            int maxCol = ws.Dimension?.End.Column ?? 10;
            for (int col = 1; col <= maxCol; col++)
            {
                string headerText = ws.Cells[headerRow, col].Text.Trim().ToLower();
                if (headerText.Contains("họ") && headerText.Contains("tên"))
                    colHoTen = col;
                else if (headerText.Contains("ngày") && headerText.Contains("sinh"))
                    colNgaySinh = col;
                else if (headerText.Contains("giới") && headerText.Contains("tính"))
                    colGioiTinh = col;
                else if (headerText.Contains("sđt") || headerText.Contains("sdt") || headerText.Contains("điện thoại"))
                    colSdt = col;
                else if (headerText.Contains("email"))
                    colEmail = col;
                else if (headerText.Contains("trạng") && headerText.Contains("thái"))
                    colTrangThai = col;
                else if (headerText.Contains("khối"))
                    colKhoi = col;
                else if (headerText.Contains("ngày") && (headerText.Contains("chuyển") || headerText.Contains("vào")))
                    colNgayChuyenVao = col;
                else if (headerText.Contains("nguyện") && headerText.Contains("vọng"))
                    colNguyenVong = col;
            }
            
            // ✅ Fallback: Nếu không tìm thấy bằng header, dùng vị trí mặc định (giả định KHÔNG có cột Mã HS)
            if (colHoTen == -1) colHoTen = 1;
            if (colNgaySinh == -1) colNgaySinh = 2;
            if (colGioiTinh == -1) colGioiTinh = 3;
            if (colSdt == -1) colSdt = 4;
            if (colEmail == -1) colEmail = 5;
            if (colTrangThai == -1) colTrangThai = 6;
            if (colKhoi == -1) colKhoi = 7;
            if (colNgayChuyenVao == -1) colNgayChuyenVao = 8;
            if (colNguyenVong == -1) colNguyenVong = 9;

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    // Đọc dữ liệu từ các cột (tự động phát hiện vị trí)
                    string hoTen = ws.Cells[row, colHoTen].Text.Trim();
                    string ngaySinhStr = ws.Cells[row, colNgaySinh].Text.Trim();
                    string gioiTinh = ws.Cells[row, colGioiTinh].Text.Trim();
                    string sdtHS = ws.Cells[row, colSdt].Text.Trim();
                    string email = ws.Cells[row, colEmail].Text.Trim();
                    string khoi = ws.Cells[row, colKhoi].Text.Trim();
                    string ngayChuyenVaoStr = ws.Cells[row, colNgayChuyenVao].Text.Trim();
                    string nguyenVong = ws.Cells[row, colNguyenVong].Text.Trim();

                    // Bỏ qua dòng trống
                    if (string.IsNullOrWhiteSpace(hoTen)
                        && string.IsNullOrWhiteSpace(ngaySinhStr)
                        && string.IsNullOrWhiteSpace(gioiTinh))
                    {
                        continue;
                    }

                    // ✅ CHỈ nhập những học sinh trong danh sách đủ điều kiện
                    if (!hocSinhDuDieuKien.Contains(hoTen.Trim()))
                    {
                        // Học sinh này không đủ điều kiện → Bỏ qua, không thêm vào DB
                        continue;
                    }

                    // Validate dữ liệu
                    if (string.IsNullOrWhiteSpace(hoTen))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Thiếu họ tên");
                        errorCount++;
                        continue;
                    }

                    // ✅ Parse ngày sinh với nhiều format khác nhau hoặc dạng số serial Excel (giống HocSinh.cs)
                    DateTime ngaySinh = DateTime.MinValue;
                    bool parsedDate = false;
                    // Nếu ô là số (Excel lưu ngày tháng dạng serial)
                    var cellNgaySinh = ws.Cells[row, colNgaySinh];
                    if (cellNgaySinh.Value != null && double.TryParse(cellNgaySinh.Value.ToString(), out double serialValue))
                    {
                        try
                        {
                            ngaySinh = DateTime.FromOADate(serialValue);
                            parsedDate = true;
                        }
                        catch { /* Nếu lỗi thì thử tiếp các cách khác */ }
                    }
                    if (!parsedDate)
                    {
                        // Thử các format phổ biến
                        string[] dateFormats = {
                            "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy",
                            "yyyy-MM-dd", "dd/MM/yy", "d/M/yy"
                        };
                        foreach (string format in dateFormats)
                        {
                            if (DateTime.TryParseExact(ngaySinhStr, format,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out ngaySinh))
                            {
                                parsedDate = true;
                                break;
                            }
                        }
                    }
                    // Nếu vẫn chưa parse được, thử parse tự động
                    if (!parsedDate && DateTime.TryParse(ngaySinhStr, out ngaySinh))
                    {
                        parsedDate = true;
                    }
                    if (!parsedDate)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Ngày sinh không hợp lệ ({ngaySinhStr})");
                        errorCount++;
                        continue;
                    }

                    // Validate giới tính
                    if (!string.IsNullOrWhiteSpace(gioiTinh) && gioiTinh != "Nam" && gioiTinh != "Nữ")
                    {
                        errors.AppendLine($"Dòng {row - 1}: Giới tính không hợp lệ ({gioiTinh})");
                        errorCount++;
                        continue;
                    }

                    // Validate email
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        try
                        {
                            var emailAddr = new System.Net.Mail.MailAddress(email);
                        }
                        catch
                        {
                            errors.AppendLine($"Dòng {row - 1}: Email không hợp lệ ({email})");
                            errorCount++;
                            continue;
                        }
                    }

                    // Validate SĐT
                    if (!string.IsNullOrWhiteSpace(sdtHS) && !Regex.IsMatch(sdtHS, @"^\d+$"))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Số điện thoại không hợp lệ ({sdtHS})");
                        errorCount++;
                        continue;
                    }

                    // Validate khối
                    if (string.IsNullOrWhiteSpace(khoi))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Thiếu khối lớp");
                        errorCount++;
                        continue;
                    }

                    // ✅ VALIDATE: Kiểm tra khối và nguyện vọng chuyển lớp phải khớp nhau
                    if (!string.IsNullOrWhiteSpace(nguyenVong) && !string.IsNullOrWhiteSpace(khoi))
                    {
                        // Kiểm tra format: Nguyện vọng phải bắt đầu bằng khối
                        // Ví dụ: Khối 10 -> Nguyện vọng phải là "10A1", "10A2", etc.
                        // Ví dụ: Khối 11 -> Nguyện vọng phải là "11A1", "11B1", etc.
                        if (!nguyenVong.StartsWith(khoi))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Khối '{khoi}' không khớp với nguyện vọng chuyển lớp '{nguyenVong}'. Nguyện vọng phải bắt đầu bằng khối (ví dụ: Khối 10 -> 10A1, 10A2...)");
                            errorCount++;
                            continue;
                        }
                    }

                    // Parse ngày chuyển vào
                    DateTime ngayChuyenVao = DateTime.MinValue;
                    bool parsedNgayChuyenVao = false;
                    var cellNgayChuyenVao = ws.Cells[row, colNgayChuyenVao];
                    if (cellNgayChuyenVao.Value != null && double.TryParse(cellNgayChuyenVao.Value.ToString(), out double serialValue2))
                    {
                        try
                        {
                            ngayChuyenVao = DateTime.FromOADate(serialValue2);
                            parsedNgayChuyenVao = true;
                        }
                        catch { }
                    }
                    if (!parsedNgayChuyenVao)
                    {
                        string[] dateFormats = {
                            "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy",
                            "yyyy-MM-dd", "dd/MM/yy", "d/M/yy"
                        };
                        foreach (string format in dateFormats)
                        {
                            if (DateTime.TryParseExact(ngayChuyenVaoStr, format,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out ngayChuyenVao))
                            {
                                parsedNgayChuyenVao = true;
                                break;
                            }
                        }
                    }
                    if (!parsedNgayChuyenVao && DateTime.TryParse(ngayChuyenVaoStr, out ngayChuyenVao))
                    {
                        parsedNgayChuyenVao = true;
                    }
                    if (!parsedNgayChuyenVao)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Ngày chuyển vào không hợp lệ ({ngayChuyenVaoStr})");
                        errorCount++;
                        continue;
                    }

                    // Kiểm tra ngày chuyển vào phải trước 1/3 thời gian học kỳ
                    if (motPhanBaHocKy != DateTime.MinValue && ngayChuyenVao >= motPhanBaHocKy)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Ngày chuyển vào ({ngayChuyenVao:dd/MM/yyyy}) phải trước 1/3 thời gian học kỳ ({motPhanBaHocKy:dd/MM/yyyy})");
                        errorCount++;
                        continue;
                    }

                    // ✅ KIỂM TRA TRÙNG SĐT/EMAIL TRONG CÙNG FILE EXCEL
                    if (!string.IsNullOrWhiteSpace(sdtHS))
                    {
                        if (sdtDaNhap.Contains(sdtHS))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Số điện thoại '{sdtHS}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            continue;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        if (emailDaNhap.Contains(email))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Email '{email}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            continue;
                        }
                    }

                    // Tạo DTO và thêm vào DB với trạng thái "Đang học"
                    HocSinhDTO hs = new HocSinhDTO
                    {
                        HoTen = hoTen,
                        NgaySinh = ngaySinh,
                        GioiTinh = gioiTinh,
                        SdtHS = sdtHS,
                        Email = email,
                        TrangThai = "Đang học(CT)", // ✅ Đặt trạng thái "Đang học(CT)" để biết học sinh chuyển trường
                        TenDangNhap = null
                    };

                    int newMaHS = hocSinhBus.AddHocSinh(hs);
                    if (newMaHS > 0)
                    {
                        hs.MaHS = newMaHS;
                        
                        // ✅ Đánh dấu SĐT và Email đã nhập thành công
                        if (!string.IsNullOrWhiteSpace(sdtHS))
                            sdtDaNhap.Add(sdtHS);
                        if (!string.IsNullOrWhiteSpace(email))
                            emailDaNhap.Add(email);
                        
                        // ✅ Tạo tài khoản
                        string username = $"HS{newMaHS:D3}";
                        if (!nguoiDungBLL.CheckTenDangNhapExists(username))
                        {
                            var nguoiDung = new NguoiDungDTO
                            {
                                TenDangNhap = username,
                                MatKhau = "123456",
                                VaiTro = "HocSinh"
                            };
                            nguoiDungBLL.AddNguoiDungNoCheck(nguoiDung);
                        }

                        // ✅ Lưu học sinh thành công vào Dictionary
                        hocSinhThanhCong[hoTen.Trim()] = (newMaHS, row, khoi, ngayChuyenVao, nguyenVong);
                        successCount++;
                    }
                    else
                    {
                        errors.AppendLine($"Dòng {row - 1}: Không thể thêm học sinh {hoTen}");
                        errorCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                }
            }

            // Hiển thị kết quả
            if (errorCount > 0)
            {
                MessageBox.Show($"Nhập Học Sinh:\n- Thêm mới: {successCount}\n- Lỗi: {errorCount}\n\nChi tiết lỗi:\n{errors}",
                    "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return hocSinhThanhCong;
        }

        /// <summary>
        /// Nhập phụ huynh từ worksheet (chỉ nhập phụ huynh của học sinh đã nhập thành công)
        /// Nếu phụ huynh lỗi thì rollback học sinh
        /// </summary>
        private Dictionary<string, (int maPH, int excelRow)> ImportPhuHuynhFromWorksheetChuyenTruong(
            ExcelWorksheet ws, 
            Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong,
            out HashSet<int> phuHuynhMoiTao)
        {
            // ✅ Khởi tạo HashSet cho phụ huynh mới tạo
            phuHuynhMoiTao = new HashSet<int>();
            
            int rowCount = ws.Dimension?.Rows ?? 0;
            if (rowCount < 2) return new Dictionary<string, (int, int)>();

            // ✅ Tự động phát hiện vị trí cột bằng cách đọc header row
            int colHoTen = -1, colSdt = -1, colEmail = -1, colDiaChi = -1;
            
            // Đọc header row (dòng 1) để tìm vị trí cột
            int headerRow = 1;
            int maxCol = ws.Dimension?.End.Column ?? 5;
            for (int col = 1; col <= maxCol; col++)
            {
                string headerText = ws.Cells[headerRow, col].Text.Trim().ToLower();
                if (headerText.Contains("họ") && headerText.Contains("tên"))
                    colHoTen = col;
                else if (headerText.Contains("sđt") || headerText.Contains("sdt") || headerText.Contains("điện thoại"))
                    colSdt = col;
                else if (headerText.Contains("email"))
                    colEmail = col;
                else if (headerText.Contains("địa") && headerText.Contains("chỉ"))
                    colDiaChi = col;
            }
            
            // ✅ Fallback: Nếu không tìm thấy bằng header, dùng vị trí mặc định (giả định KHÔNG có cột Mã PH)
            if (colHoTen == -1) colHoTen = 1;
            if (colSdt == -1) colSdt = 2;
            if (colEmail == -1) colEmail = 3;
            if (colDiaChi == -1) colDiaChi = 4;

            int successCount = 0;
            int skippedCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();
            var skipped = new StringBuilder();
            Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong = new Dictionary<string, (int, int)>();
            List<int> hocSinhCanRollback = new List<int>();
            HashSet<string> sdtDaNhap = new HashSet<string>();
            HashSet<string> emailDaNhap = new HashSet<string>();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    // Đọc dữ liệu từ các cột (tự động phát hiện vị trí)
                    string hoTen = ws.Cells[row, colHoTen].Text.Trim();
                    string sdt = ws.Cells[row, colSdt].Text.Trim();
                    string email = ws.Cells[row, colEmail].Text.Trim();
                    string diaChi = ws.Cells[row, colDiaChi].Text.Trim();

                    if (string.IsNullOrWhiteSpace(hoTen)
                        && string.IsNullOrWhiteSpace(sdt)
                        && string.IsNullOrWhiteSpace(email)
                        && string.IsNullOrWhiteSpace(diaChi))
                    {
                        continue;
                    }

                    // ✅ KIỂM TRA: Chỉ nhập phụ huynh nếu có học sinh tương ứng ở cùng dòng Excel
                    bool coHocSinhTuongUng = false;
                    int maHSTuongUng = 0;
                    foreach (var kvp in hocSinhThanhCong)
                    {
                        if (kvp.Value.excelRow == row)
                        {
                            coHocSinhTuongUng = true;
                            maHSTuongUng = kvp.Value.maHS;
                            break;
                        }
                    }

                    if (!coHocSinhTuongUng)
                    {
                        skipped.AppendLine($"Dòng {row - 1}: {hoTen} - Bỏ qua (Không có học sinh tương ứng ở dòng {row - 1})");
                        skippedCount++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(hoTen))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Thiếu họ tên phụ huynh");
                        errorCount++;
                        hocSinhCanRollback.Add(maHSTuongUng);
                        continue;
                    }

                    // ✅ KIỂM TRA TRÙNG SĐT/EMAIL TRONG CÙNG FILE EXCEL
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        if (sdtDaNhap.Contains(sdt))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Số điện thoại '{sdt}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            hocSinhCanRollback.Add(maHSTuongUng);
                            continue;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        if (emailDaNhap.Contains(email))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Email '{email}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            hocSinhCanRollback.Add(maHSTuongUng);
                            continue;
                        }
                    }

                    PhuHuynhDTO ph = new PhuHuynhDTO
                    {
                        HoTen = hoTen,
                        SoDienThoai = sdt,
                        Email = email,
                        DiaChi = diaChi
                    };

                    // ✅ Kiểm tra phụ huynh đã tồn tại trong DB không
                    PhuHuynhDTO existing = null;
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        try { existing = phuHuynhBLL.GetPhuHuynhBySdt(sdt); } catch { existing = null; }
                    }
                    if (existing == null && !string.IsNullOrWhiteSpace(email))
                    {
                        try 
                        { 
                            var danhSachPH = phuHuynhBLL.GetAllPhuHuynh();
                            existing = danhSachPH.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.Email) && p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                        } 
                        catch { existing = null; }
                    }

                    if (existing != null)
                    {
                        skippedCount++;
                        phuHuynhThanhCong[hoTen.Trim()] = (existing.MaPhuHuynh, row);
                        // ✅ Phụ huynh đã tồn tại → không đánh dấu là mới tạo
                        if (!string.IsNullOrWhiteSpace(sdt))
                            sdtDaNhap.Add(sdt);
                        if (!string.IsNullOrWhiteSpace(email))
                            emailDaNhap.Add(email);
                    }
                    else
                    {
                        try
                        {
                            bool success = phuHuynhBLL.AddPhuHuynh(ph);
                            if (success)
                            {
                                if (!string.IsNullOrWhiteSpace(sdt))
                                    sdtDaNhap.Add(sdt);
                                if (!string.IsNullOrWhiteSpace(email))
                                    emailDaNhap.Add(email);
                                
                                try 
                                { 
                                    var danhSachPH = phuHuynhBLL.GetAllPhuHuynh();
                                    var phMoi = danhSachPH.FirstOrDefault(p => 
                                        p.HoTen == hoTen && 
                                        (string.IsNullOrWhiteSpace(sdt) || p.SoDienThoai == sdt));
                                    if (phMoi != null)
                                    {
                                        phuHuynhThanhCong[hoTen.Trim()] = (phMoi.MaPhuHuynh, row);
                                        // ✅ Đánh dấu phụ huynh này là mới tạo
                                        phuHuynhMoiTao.Add(phMoi.MaPhuHuynh);
                                        successCount++;
                                    }
                                } 
                                catch { }
                            }
                            else
                            {
                                errors.AppendLine($"Dòng {row - 1}: Không thể thêm phụ huynh {hoTen}");
                                errorCount++;
                                hocSinhCanRollback.Add(maHSTuongUng);
                            }
                        }
                        catch (ArgumentException vex)
                        {
                            errors.AppendLine($"Dòng {row - 1}: {vex.Message}");
                            errorCount++;
                            hocSinhCanRollback.Add(maHSTuongUng);
                        }
                        catch (Exception ex)
                        {
                            errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                            errorCount++;
                            hocSinhCanRollback.Add(maHSTuongUng);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ ROLLBACK học sinh nếu phụ huynh lỗi
            foreach (int maHS in hocSinhCanRollback)
            {
                try
                {
                    hocSinhBus.DeleteHocSinh(maHS);
                    string username = $"HS{maHS:D3}";
                    try { nguoiDungBLL.DeleteNguoiDung(username); } catch { }
                    // Xóa khỏi dictionary
                    var keyToRemove = hocSinhThanhCong.FirstOrDefault(kvp => kvp.Value.maHS == maHS);
                    if (!string.IsNullOrEmpty(keyToRemove.Key))
                    {
                        hocSinhThanhCong.Remove(keyToRemove.Key);
                    }
                }
                catch { }
            }

            if (errorCount > 0 || skippedCount > 0 || hocSinhCanRollback.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine($"Nhập Phụ Huynh (chuyển trường):");
                if (successCount > 0)
                    result.AppendLine($"- Thêm mới: {successCount}");
                if (skippedCount > 0)
                    result.AppendLine($"- Bỏ qua (đã tồn tại hoặc không có học sinh tương ứng): {skippedCount}");
                if (errorCount > 0)
                    result.AppendLine($"- Lỗi: {errorCount}");
                if (hocSinhCanRollback.Count > 0)
                    result.AppendLine($"- ⚠️ Đã rollback {hocSinhCanRollback.Count} học sinh do phụ huynh lỗi");
                if (skipped.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Chi tiết bỏ qua:");
                    result.Append(skipped);
                }
                if (errors.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Chi tiết lỗi:");
                    result.Append(errors);
                }
                MessageBox.Show(result.ToString(), "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return phuHuynhThanhCong;
        }

        /// <summary>
        /// Nhập mối quan hệ từ worksheet
        /// Nếu lỗi thì rollback học sinh
        /// </summary>
        private void ImportMoiQuanHeFromWorksheetChuyenTruong(
            ExcelWorksheet ws,
            Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong,
            Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            if (rowCount < 2) return;

            int successCount = 0;
            int skippedCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();
            var warnings = new StringBuilder();
            var skipped = new StringBuilder();
            List<int> hocSinhCanRollback = new List<int>(); // ✅ Track học sinh cần rollback

            for (int row = 2; row <= rowCount; row++)
            {
                string tenHS = ""; // ✅ Khai báo ở ngoài try để dùng trong catch
                int maHS = -1; // ✅ Khai báo ở ngoài để tránh khai báo lại
                try
                {
                    tenHS = ws.Cells[row, 1].Text.Trim();
                    string tenPH = ws.Cells[row, 2].Text.Trim();
                    string moiQuanHe = ws.Cells[row, 3].Text.Trim();

                    if (string.IsNullOrWhiteSpace(tenHS)
                        && string.IsNullOrWhiteSpace(tenPH)
                        && string.IsNullOrWhiteSpace(moiQuanHe))
                    {
                        continue;
                    }

                    // ✅ MATCH HỌC SINH: Ưu tiên match theo dòng Excel, sau đó mới match theo tên
                    maHS = -1;
                    // 1. Ưu tiên: Match theo dòng Excel (nếu học sinh ở dòng này)
                    foreach (var kvp in hocSinhThanhCong)
                    {
                        if (kvp.Value.excelRow == row)
                        {
                            maHS = kvp.Value.maHS;
                            tenHS = kvp.Key; // Cập nhật tên học sinh chính xác
                            break;
                        }
                    }
                    
                    // 2. Nếu không match theo dòng, match theo tên
                    if (maHS == -1)
                    {
                        if (hocSinhThanhCong.ContainsKey(tenHS))
                        {
                            maHS = hocSinhThanhCong[tenHS].maHS;
                        }
                        else
                        {
                            // Tìm học sinh trùng tên (có thể có nhiều học sinh cùng tên)
                            var hsTrungTen = hocSinhThanhCong.Where(kvp => 
                                kvp.Key.Equals(tenHS, StringComparison.OrdinalIgnoreCase)).ToList();
                            
                            if (hsTrungTen.Count == 0)
                            {
                                skipped.AppendLine($"Dòng {row - 1}: Bỏ qua (Học sinh '{tenHS}' không có trong danh sách nhập thành công)");
                                skippedCount++;
                                continue;
                            }
                            else if (hsTrungTen.Count == 1)
                            {
                                maHS = hsTrungTen[0].Value.maHS;
                                tenHS = hsTrungTen[0].Key; // Cập nhật tên chính xác
                            }
                            else
                            {
                                // ⚠️ TRÙNG TÊN: Có nhiều học sinh cùng tên
                                warnings.AppendLine($"⚠️ Dòng {row - 1}: Có {hsTrungTen.Count} học sinh tên '{tenHS}' - Đã chọn học sinh đầu tiên (Mã HS: {hsTrungTen[0].Value.maHS})");
                                maHS = hsTrungTen[0].Value.maHS;
                                tenHS = hsTrungTen[0].Key;
                            }
                        }
                    }

                    if (moiQuanHe != "Cha" && moiQuanHe != "Mẹ" && moiQuanHe != "Ông" &&
                        moiQuanHe != "Bà" && moiQuanHe != "Người giám hộ")
                    {
                        errors.AppendLine($"Dòng {row - 1}: Mối quan hệ không hợp lệ ({moiQuanHe})");
                        errorCount++;
                        if (!hocSinhCanRollback.Contains(maHS))
                            hocSinhCanRollback.Add(maHS);
                        continue;
                    }
                    var hs = hocSinhBus.GetHocSinhById(maHS);
                    if (hs == null)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Không tìm thấy học sinh '{tenHS}'");
                        errorCount++;
                        continue;
                    }

                    // ✅ MATCH PHỤ HUYNH: Ưu tiên match theo dòng Excel, sau đó mới match theo tên
                    PhuHuynhDTO ph = null;
                    int maPH = -1;
                    
                    // 1. Ưu tiên: Match theo dòng Excel (nếu phụ huynh ở dòng này)
                    foreach (var kvp in phuHuynhThanhCong)
                    {
                        if (kvp.Value.excelRow == row)
                        {
                            maPH = kvp.Value.maPH;
                            ph = phuHuynhBLL.GetPhuHuynhById(maPH);
                            break;
                        }
                    }
                    
                    // 2. Nếu không match theo dòng, match theo tên từ dictionary
                    if (ph == null && phuHuynhThanhCong.ContainsKey(tenPH))
                    {
                        maPH = phuHuynhThanhCong[tenPH].maPH;
                        ph = phuHuynhBLL.GetPhuHuynhById(maPH);
                    }
                    
                    // 3. Nếu vẫn chưa tìm thấy, tìm trong database theo tên
                    if (ph == null)
                    {
                        var danhSachPH = phuHuynhBLL.GetAllPhuHuynh();
                        var danhSachPHTrung = danhSachPH.Where(p => 
                            p.HoTen.Equals(tenPH, StringComparison.OrdinalIgnoreCase)).ToList();
                        
                        if (danhSachPHTrung.Count == 0)
                        {
                            errors.AppendLine($"Dòng {row - 1}: Không tìm thấy phụ huynh '{tenPH}'");
                            errorCount++;
                            if (maHS > 0 && !hocSinhCanRollback.Contains(maHS))
                                hocSinhCanRollback.Add(maHS);
                            continue;
                        }
                        else if (danhSachPHTrung.Count == 1)
                        {
                            ph = danhSachPHTrung[0];
                            maPH = ph.MaPhuHuynh;
                        }
                        else
                        {
                            // ⚠️ TRÙNG TÊN: Có nhiều phụ huynh cùng tên
                            ph = danhSachPHTrung[0];
                            maPH = ph.MaPhuHuynh;
                            warnings.AppendLine($"⚠️ Dòng {row - 1}: Có {danhSachPHTrung.Count} phụ huynh tên '{tenPH}' - Đã chọn MaPH {maPH}");
                        }
                    }

                    bool success = hocSinhPhuHuynhBLL.AddQuanHe(maHS, ph.MaPhuHuynh, moiQuanHe);
                    if (success)
                    {
                        successCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                    // Nếu có học sinh trong dòng này thì rollback
                    if (!string.IsNullOrEmpty(tenHS) && hocSinhThanhCong.ContainsKey(tenHS))
                    {
                        int maHSRollback = hocSinhThanhCong[tenHS].maHS;
                        if (!hocSinhCanRollback.Contains(maHSRollback))
                            hocSinhCanRollback.Add(maHSRollback);
                    }
                    else if (maHS > 0 && !hocSinhCanRollback.Contains(maHS))
                    {
                        hocSinhCanRollback.Add(maHS);
                    }
                }
            }

            // ✅ ROLLBACK học sinh nếu mối quan hệ lỗi
            foreach (int maHS in hocSinhCanRollback)
            {
                try
                {
                    hocSinhBus.DeleteHocSinh(maHS);
                    string username = $"HS{maHS:D3}";
                    try { nguoiDungBLL.DeleteNguoiDung(username); } catch { }
                    try { hocSinhPhuHuynhBLL.DeleteQuanHeByHocSinh(maHS); } catch { }
                    // Xóa khỏi dictionary
                    var keyToRemove = hocSinhThanhCong.FirstOrDefault(kvp => kvp.Value.maHS == maHS);
                    if (!string.IsNullOrEmpty(keyToRemove.Key))
                    {
                        hocSinhThanhCong.Remove(keyToRemove.Key);
                    }
                }
                catch { }
            }

            if (errorCount > 0 || skippedCount > 0 || hocSinhCanRollback.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine($"Nhập Mối Quan Hệ (chuyển trường):");
                if (successCount > 0)
                    result.AppendLine($"- Thêm mới: {successCount}");
                if (skippedCount > 0)
                    result.AppendLine($"- Bỏ qua (đã tồn tại hoặc không có học sinh tương ứng): {skippedCount}");
                if (errorCount > 0)
                    result.AppendLine($"- Lỗi: {errorCount}");
                if (hocSinhCanRollback.Count > 0)
                    result.AppendLine($"- ⚠️ Đã rollback {hocSinhCanRollback.Count} học sinh do mối quan hệ lỗi");
                if (skipped.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Chi tiết bỏ qua:");
                    result.Append(skipped);
                }
                if (errors.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Chi tiết lỗi:");
                    result.Append(errors);
                }
                if (warnings.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Cảnh báo:");
                    result.Append(warnings);
                }
                MessageBox.Show(result.ToString(), "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Đọc dữ liệu học sinh từ Excel (chưa thêm vào DB) để kiểm tra học kỳ
        /// </summary>
        private Dictionary<string, (int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> 
            DocDuLieuHocSinhTuExcel(ExcelWorksheet ws, HocKyDTO hocKyHienTai)
        {
            Dictionary<string, (int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> result = 
                new Dictionary<string, (int, string, DateTime, string)>();
            
            int rowCount = ws.Dimension?.Rows ?? 0;
            if (rowCount < 2) return result;

            // ✅ Tự động phát hiện vị trí cột bằng cách đọc header row
            int colHoTen = -1, colKhoi = -1;
            
            int headerRow = 1;
            int maxCol = ws.Dimension?.End.Column ?? 10;
            for (int col = 1; col <= maxCol; col++)
            {
                string headerText = ws.Cells[headerRow, col].Text.Trim().ToLower();
                if (headerText.Contains("họ") && headerText.Contains("tên"))
                    colHoTen = col;
                else if (headerText.Contains("khối"))
                    colKhoi = col;
            }
            
            if (colHoTen == -1) colHoTen = 1;
            if (colKhoi == -1) colKhoi = 7;

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string hoTen = ws.Cells[row, colHoTen].Text.Trim();
                    string khoi = ws.Cells[row, colKhoi].Text.Trim();
                    
                    if (string.IsNullOrWhiteSpace(hoTen))
                        continue;
                    
                    // Lấy ngày chuyển vào và nguyện vọng (nếu có)
                    string ngayChuyenVaoStr = "";
                    string nguyenVong = "";
                    DateTime ngayChuyenVao = DateTime.MinValue;
                    
                    // Tìm cột ngày chuyển vào và nguyện vọng
                    for (int col = 1; col <= maxCol; col++)
                    {
                        string headerText = ws.Cells[headerRow, col].Text.Trim().ToLower();
                        if (headerText.Contains("ngày") && (headerText.Contains("chuyển") || headerText.Contains("vào")))
                        {
                            ngayChuyenVaoStr = ws.Cells[row, col].Text.Trim();
                        }
                        else if (headerText.Contains("nguyện") && headerText.Contains("vọng"))
                        {
                            nguyenVong = ws.Cells[row, col].Text.Trim();
                        }
                    }
                    
                    // Parse ngày chuyển vào (đơn giản, không cần validate kỹ)
                    if (!string.IsNullOrWhiteSpace(ngayChuyenVaoStr))
                    {
                        DateTime.TryParse(ngayChuyenVaoStr, out ngayChuyenVao);
                    }
                    
                    result[hoTen.Trim()] = (row, khoi, ngayChuyenVao, nguyenVong);
                }
                catch
                {
                    // Bỏ qua lỗi khi đọc
                }
            }
            
            return result;
        }

        /// <summary>
        /// Lọc ra danh sách học sinh đủ điều kiện (có đủ học kỳ cần thiết)
        /// Học sinh không đủ điều kiện sẽ bị loại bỏ, KHÔNG được thêm vào DB
        /// Trả về HashSet chứa tên các học sinh đủ điều kiện
        /// </summary>
        private HashSet<string> LocHocSinhDuDieuKien(
            Dictionary<string, (int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhTuExcel,
            HocKyDTO hocKyHienTai)
        {
            HashSet<string> hocSinhDuDieuKien = new HashSet<string>();
            
            if (hocSinhTuExcel.Count == 0)
                return hocSinhDuDieuKien; // Không có học sinh → Trả về danh sách rỗng

            // Dictionary để lưu các học kỳ cần thiết cho từng học sinh
            Dictionary<string, List<(string TenHocKy, string MaNamHoc)>> hocKyCanThiet = 
                new Dictionary<string, List<(string TenHocKy, string MaNamHoc)>>();
            
            // Tính toán học kỳ cần thiết cho từng học sinh
            string maNamHocHienTai = hocKyHienTai.MaNamHoc;
            bool laHocKy1 = hocKyHienTai.TenHocKy.Contains("I") || hocKyHienTai.TenHocKy.Contains("1");
            
            foreach (var kvp in hocSinhTuExcel)
            {
                string hoTen = kvp.Key;
                string khoiStr = kvp.Value.khoi;
                
                if (!int.TryParse(khoiStr, out int maKhoi))
                    continue;
                
                List<(string TenHocKy, string MaNamHoc)> danhSachHocKyCanThiet = 
                    new List<(string TenHocKy, string MaNamHoc)>();
                
                // 1. Kiểm tra học kỳ của năm học hiện tại (nếu HK2 đang diễn ra thì cần HK1)
                if (!laHocKy1)
                {
                    danhSachHocKyCanThiet.Add(("Học kỳ I", maNamHocHienTai));
                }
                
                // 2. Kiểm tra học kỳ của các năm học trước (theo khối)
                if (maKhoi == 11 || maKhoi == 12)
                {
                    string maNamHocTruoc = "";
                    if (!string.IsNullOrEmpty(maNamHocHienTai) && maNamHocHienTai.Contains("-"))
                    {
                        var parts = maNamHocHienTai.Split('-');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                        {
                            int namBatDauTruoc = namBatDau - 1;
                            int namKetThucTruoc = namBatDau;
                            maNamHocTruoc = $"{namBatDauTruoc}-{namKetThucTruoc}";
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(maNamHocTruoc))
                    {
                        danhSachHocKyCanThiet.Add(("Học kỳ I", maNamHocTruoc));
                        danhSachHocKyCanThiet.Add(("Học kỳ II", maNamHocTruoc));
                    }
                }
                
                if (maKhoi == 12)
                {
                    // Khối 12: Cần check thêm 2 học kỳ của năm học trước nữa
                    string maNamHocTruoc = "";
                    if (!string.IsNullOrEmpty(maNamHocHienTai) && maNamHocHienTai.Contains("-"))
                    {
                        var parts = maNamHocHienTai.Split('-');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                        {
                            int namBatDauTruoc = namBatDau - 1;
                            int namKetThucTruoc = namBatDau;
                            maNamHocTruoc = $"{namBatDauTruoc}-{namKetThucTruoc}";
                        }
                    }
                    
                    string maNamHocTruocNua = "";
                    if (!string.IsNullOrEmpty(maNamHocTruoc) && maNamHocTruoc.Contains("-"))
                    {
                        var parts = maNamHocTruoc.Split('-');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                        {
                            int namBatDauTruocNua = namBatDau - 1;
                            int namKetThucTruocNua = namBatDau;
                            maNamHocTruocNua = $"{namBatDauTruocNua}-{namKetThucTruocNua}";
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(maNamHocTruocNua))
                    {
                        danhSachHocKyCanThiet.Add(("Học kỳ I", maNamHocTruocNua));
                        danhSachHocKyCanThiet.Add(("Học kỳ II", maNamHocTruocNua));
                    }
                }
                
                if (danhSachHocKyCanThiet.Count > 0)
                {
                    hocKyCanThiet[hoTen] = danhSachHocKyCanThiet;
                }
            }
            
            // Kiểm tra từng học sinh xem có đủ điều kiện không
            HashSet<(string TenHocKy, string MaNamHoc)> hocKyKhongTonTai = 
                new HashSet<(string TenHocKy, string MaNamHoc)>();
            List<string> hocSinhKhongDuDieuKien = new List<string>();
            
            // Nếu không có học sinh nào cần học kỳ (ví dụ: tất cả đều là khối 10, HK1)
            // → Tất cả đều đủ điều kiện
            if (hocKyCanThiet.Count == 0)
            {
                // Tất cả học sinh đều không cần học kỳ → Tất cả đều đủ điều kiện
                foreach (var kvp in hocSinhTuExcel)
                {
                    hocSinhDuDieuKien.Add(kvp.Key);
                }
                return hocSinhDuDieuKien;
            }
            
            // Kiểm tra từng học sinh có cần học kỳ không
            foreach (var kvp in hocSinhTuExcel)
            {
                string hoTen = kvp.Key;
                
                // Nếu học sinh này không cần học kỳ nào → Đủ điều kiện
                if (!hocKyCanThiet.ContainsKey(hoTen))
                {
                    hocSinhDuDieuKien.Add(hoTen);
                    continue;
                }
                
                // Kiểm tra xem tất cả học kỳ cần thiết có tồn tại không
                var danhSachHocKyCanThiet = hocKyCanThiet[hoTen];
                bool duDieuKien = true;
                
                foreach (var hk in danhSachHocKyCanThiet)
                {
                    HocKyDTO hocKy = hocKyBus.LayHocKyTheoTenVaNamHoc(hk.TenHocKy.Trim(), hk.MaNamHoc.Trim());
                    if (hocKy == null)
                    {
                        hocKyKhongTonTai.Add((hk.TenHocKy.Trim(), hk.MaNamHoc.Trim()));
                        duDieuKien = false;
                        break; // Chỉ cần 1 học kỳ không tồn tại là không đủ điều kiện
                    }
                }
                
                if (duDieuKien)
                {
                    hocSinhDuDieuKien.Add(hoTen);
                }
                else
                {
                    if (!hocSinhKhongDuDieuKien.Contains(hoTen))
                        hocSinhKhongDuDieuKien.Add(hoTen);
                }
            }
            
            // Hiển thị cảnh báo cho học sinh không đủ điều kiện (nếu có)
            if (hocSinhKhongDuDieuKien.Count > 0)
            {
                StringBuilder loiHocKy = new StringBuilder();
                loiHocKy.AppendLine("⚠️ CẢNH BÁO: Có học sinh không đủ điều kiện chuyển trường");
                loiHocKy.AppendLine();
                loiHocKy.AppendLine($"Có {hocSinhKhongDuDieuKien.Count} học sinh KHÔNG được thêm vào hệ thống:");
                foreach (var hoTen in hocSinhKhongDuDieuKien)
                {
                    loiHocKy.AppendLine($"   • {hoTen}");
                }
                loiHocKy.AppendLine();
                loiHocKy.AppendLine("Các học kỳ sau KHÔNG TỒN TẠI trong hệ thống:");
                foreach (var hk in hocKyKhongTonTai)
                {
                    loiHocKy.AppendLine($"   • {hk.TenHocKy} ({hk.MaNamHoc})");
                }
                loiHocKy.AppendLine();
                loiHocKy.AppendLine("💡 LÝ DO:");
                loiHocKy.AppendLine("   - Hệ thống chỉ cho phép lưu điểm, hạnh kiểm, xếp loại");
                loiHocKy.AppendLine("     cho các học kỳ đã tồn tại trong database.");
                loiHocKy.AppendLine("   - Nếu trường mới mở (bắt đầu từ 2025-2026),");
                loiHocKy.AppendLine("     chỉ cho phép học sinh khối 10 chuyển trường.");
                loiHocKy.AppendLine("   - Nếu cần nhận học sinh khối 11, 12, vui lòng");
                loiHocKy.AppendLine("     thêm các học kỳ tương ứng vào database trước.");
                loiHocKy.AppendLine();
                if (hocSinhDuDieuKien.Count > 0)
                {
                    loiHocKy.AppendLine($"✅ Các học sinh khác ({hocSinhDuDieuKien.Count} học sinh) vẫn được thêm vào hệ thống.");
                }
                
                ScrollableMessageBox.Show("Cảnh báo: Học kỳ không tồn tại", loiHocKy.ToString(), MessageBoxIcon.Warning);
            }
            
            return hocSinhDuDieuKien;
        }

        /// <summary>
        /// Kiểm tra điểm, hạnh kiểm, xếp loại từ Excel và LƯU VÀO DATABASE (nếu học kỳ tồn tại trong DB)
        /// Logic theo khối:
        /// - Khối 10: Nếu HK1 đang diễn ra → không cần check. Nếu HK2 đang diễn ra → check HK1
        /// - Khối 11: Tương tự khối 10 + check 2 học kỳ của năm học trước (khối 10)
        /// - Khối 12: Tương tự khối 10 + check 4 học kỳ của 2 năm học trước (khối 10 và khối 11)
        /// 
        /// ⚠️ QUAN TRỌNG:
        /// - Chỉ lưu điểm, hạnh kiểm, xếp loại vào SQL nếu học kỳ tương ứng ĐÃ TỒN TẠI trong database
        /// - Nếu học kỳ không tồn tại → KHÔNG cho phép chuyển trường (báo lỗi)
        /// - Logic này đảm bảo: Trường mới mở (từ 2025-2026) chỉ nhận khối 10, không nhận khối 11, 12
        /// </summary>
        private void ImportDiemHanhKiemXepLoaiFromExcel(
            ExcelWorksheet wsDiem,
            ExcelWorksheet wsHanhKiem,
            ExcelWorksheet wsXepLoai,
            Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong,
            HocKyDTO hocKyHienTai,
            Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong)
        {
            // ✅ LƯU Ý: KHÔNG cần lấy danh sách học kỳ từ database
            // Chỉ cần xác định danh sách học kỳ cần thiết dựa trên (TenHocKy, MaNamHoc)
            // và so sánh với dữ liệu trong Excel
            
            // Dictionary để lưu các học kỳ cần thiết cho từng học sinh (theo khối)
            // ✅ LƯU Ý: Chỉ cần lưu (TenHocKy, MaNamHoc), không cần MaHocKy từ database
            Dictionary<int, List<(string TenHocKy, string MaNamHoc)>> hocKyCanThiet = 
                new Dictionary<int, List<(string TenHocKy, string MaNamHoc)>>();
            
            // Tính toán học kỳ cần thiết cho từng học sinh theo logic mới
            foreach (var kvp in hocSinhThanhCong)
            {
                int maHS = kvp.Value.maHS;
                string khoiStr = kvp.Value.khoi;
                
                if (!int.TryParse(khoiStr, out int maKhoi))
                    continue;
                
                // Lấy năm học hiện tại từ học kỳ hiện tại
                string maNamHocHienTai = hocKyHienTai.MaNamHoc;
                
                List<(string TenHocKy, string MaNamHoc)> danhSachHocKyCanThiet = 
                    new List<(string TenHocKy, string MaNamHoc)>();
                
                // Xác định học kỳ đang diễn ra là HK1 hay HK2
                bool laHocKy1 = hocKyHienTai.TenHocKy.Contains("I") || hocKyHienTai.TenHocKy.Contains("1");
                
                // 1. Kiểm tra học kỳ của năm học hiện tại (nếu HK2 đang diễn ra thì cần HK1)
                if (!laHocKy1)
                {
                    // Nếu HK2 đang diễn ra → cần check HK1 của năm học hiện tại
                    // ✅ Chỉ cần thêm (TenHocKy, MaNamHoc), không cần tìm trong database
                    danhSachHocKyCanThiet.Add(("Học kỳ I", maNamHocHienTai));
                }
                // Nếu HK1 đang diễn ra → không cần check học kỳ nào của năm hiện tại
                
                // 2. Kiểm tra học kỳ của các năm học trước (theo khối)
                // ✅ LƯU Ý: Chỉ cần (TenHocKy, MaNamHoc), không cần tìm trong database
                if (maKhoi == 11 || maKhoi == 12)
                {
                    // Khối 11 và 12: Cần check 2 học kỳ của năm học trước (khối 10)
                    string maNamHocTruoc = "";
                    if (!string.IsNullOrEmpty(maNamHocHienTai) && maNamHocHienTai.Contains("-"))
                    {
                        var parts = maNamHocHienTai.Split('-');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                        {
                            int namBatDauTruoc = namBatDau - 1;
                            int namKetThucTruoc = namBatDau;
                            maNamHocTruoc = $"{namBatDauTruoc}-{namKetThucTruoc}";
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(maNamHocTruoc))
                    {
                        // ✅ Chỉ cần thêm (TenHocKy, MaNamHoc), không cần tìm trong database
                        danhSachHocKyCanThiet.Add(("Học kỳ I", maNamHocTruoc));
                        danhSachHocKyCanThiet.Add(("Học kỳ II", maNamHocTruoc));
                    }
                }
                
                if (maKhoi == 12)
                {
                    // Khối 12: Cần check thêm 2 học kỳ của năm học trước nữa (khối 11)
                    string maNamHocTruoc = "";
                    if (!string.IsNullOrEmpty(maNamHocHienTai) && maNamHocHienTai.Contains("-"))
                    {
                        var parts = maNamHocHienTai.Split('-');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                        {
                            int namBatDauTruoc = namBatDau - 1;
                            int namKetThucTruoc = namBatDau;
                            maNamHocTruoc = $"{namBatDauTruoc}-{namKetThucTruoc}";
                        }
                    }
                    
                    // Tìm năm học trước nữa (2023-2024 nếu hiện tại là 2025-2026)
                    string maNamHocTruocNua = "";
                    if (!string.IsNullOrEmpty(maNamHocTruoc) && maNamHocTruoc.Contains("-"))
                    {
                        var parts = maNamHocTruoc.Split('-');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                        {
                            int namBatDauTruocNua = namBatDau - 1;
                            int namKetThucTruocNua = namBatDau;
                            maNamHocTruocNua = $"{namBatDauTruocNua}-{namKetThucTruocNua}";
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(maNamHocTruocNua))
                    {
                        // ✅ Chỉ cần thêm (TenHocKy, MaNamHoc), không cần tìm trong database
                        danhSachHocKyCanThiet.Add(("Học kỳ I", maNamHocTruocNua));
                        danhSachHocKyCanThiet.Add(("Học kỳ II", maNamHocTruocNua));
                    }
                }
                
                hocKyCanThiet[maHS] = danhSachHocKyCanThiet;
                
                // ✅ DEBUG: Log học kỳ cần thiết cho từng học sinh
                if (danhSachHocKyCanThiet.Count > 0)
                {
                    var hocKyInfo = string.Join(", ", danhSachHocKyCanThiet.Select(hk => 
                        $"{hk.TenHocKy} ({hk.MaNamHoc})"));
                    System.Diagnostics.Debug.WriteLine($"✅ Học sinh {kvp.Key} (Khối {khoiStr}, Mã HS: {maHS}) cần {danhSachHocKyCanThiet.Count} học kỳ: {hocKyInfo}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"⚠️ Học sinh {kvp.Key} (Khối {khoiStr}, Mã HS: {maHS}) KHÔNG cần học kỳ nào (ví dụ: khối 10, HK1)");
                }
            }
            
            // ✅ Dictionary để track học sinh có đủ điều kiện (khai báo sớm để dùng trong phần kiểm tra học kỳ)
            Dictionary<int, bool> hocSinhDuDieuKien = new Dictionary<int, bool>();
            foreach (var kvp in hocSinhThanhCong)
            {
                hocSinhDuDieuKien[kvp.Value.maHS] = true;
            }

            // ✅ LƯU Ý: KHÔNG return sớm nữa
            // Dù học sinh không cần học kỳ nào trước đó, vẫn phải lưu điểm/hạnh kiểm/xếp loại cho học kỳ HIỆN TẠI
            // để học sinh hiện trong giao diện điểm, hạnh kiểm, xếp loại
            bool coHocSinhCanHocKy = hocKyCanThiet.Values.Any(list => list.Count > 0);

            // ✅ BƯỚC MỚI: Kiểm tra từng học sinh xem học kỳ cần thiết của họ có tồn tại trong DB không
            // Dictionary để lưu MaHocKy tương ứng với (TenHocKy, MaNamHoc) cho các học kỳ tồn tại
            Dictionary<(string TenHocKy, string MaNamHoc), int> hocKyTonTai = 
                new Dictionary<(string TenHocKy, string MaNamHoc), int>();
            
            // Danh sách học sinh không đủ điều kiện (cần học kỳ không tồn tại)
            List<int> hocSinhKhongDuDieuKien = new List<int>();
            HashSet<(string TenHocKy, string MaNamHoc)> hocKyKhongTonTai = 
                new HashSet<(string TenHocKy, string MaNamHoc)>();
            
            // Kiểm tra từng học sinh
            foreach (var kvp in hocSinhThanhCong)
            {
                int maHS = kvp.Value.maHS;
                var danhSachHocKyCanThiet = hocKyCanThiet.ContainsKey(maHS) ? hocKyCanThiet[maHS] : new List<(string TenHocKy, string MaNamHoc)>();
                
                // Nếu học sinh không cần học kỳ nào (ví dụ: khối 10, HK1) → Bỏ qua, giữ lại
                if (danhSachHocKyCanThiet.Count == 0)
                {
                    continue; // Học sinh này không cần học kỳ → Giữ lại
                }
                
                // Kiểm tra từng học kỳ cần thiết của học sinh này
                bool hocSinhDuHocKy = true;
                foreach (var hk in danhSachHocKyCanThiet)
                {
                    // Kiểm tra xem đã cache chưa
                    if (!hocKyTonTai.ContainsKey(hk))
                    {
                        // Chưa cache → Kiểm tra trong DB
                        HocKyDTO hocKy = hocKyBus.LayHocKyTheoTenVaNamHoc(hk.TenHocKy.Trim(), hk.MaNamHoc.Trim());
                        if (hocKy != null)
                        {
                            // Học kỳ tồn tại → Lưu MaHocKy vào cache
                            hocKyTonTai[hk] = hocKy.MaHocKy;
                        }
                        else
                        {
                            // Học kỳ không tồn tại → Đánh dấu
                            hocKyKhongTonTai.Add(hk);
                            hocSinhDuHocKy = false;
                        }
                    }
                }
                
                // Nếu học sinh này thiếu học kỳ → Đánh dấu không đủ điều kiện
                if (!hocSinhDuHocKy)
                {
                    hocSinhKhongDuDieuKien.Add(maHS);
                    hocSinhDuDieuKien[maHS] = false;
                }
            }
            
            // ✅ Nếu có học sinh không đủ điều kiện → Báo lỗi và rollback chỉ những học sinh đó
            if (hocSinhKhongDuDieuKien.Count > 0)
            {
                StringBuilder loiHocKy = new StringBuilder();
                loiHocKy.AppendLine("❌ MỘT SỐ HỌC SINH KHÔNG THỂ CHUYỂN TRƯỜNG!");
                loiHocKy.AppendLine();
                loiHocKy.AppendLine($"Có {hocSinhKhongDuDieuKien.Count} học sinh không đủ điều kiện:");
                foreach (var maHS in hocSinhKhongDuDieuKien)
                {
                    var hocSinhInfo = hocSinhThanhCong.FirstOrDefault(kvp => kvp.Value.maHS == maHS);
                    if (hocSinhInfo.Key != null)
                    {
                        loiHocKy.AppendLine($"   • {hocSinhInfo.Key} (Mã HS: {maHS})");
                    }
                }
                loiHocKy.AppendLine();
                loiHocKy.AppendLine("Các học kỳ sau KHÔNG TỒN TẠI trong hệ thống:");
                foreach (var hk in hocKyKhongTonTai)
                {
                    loiHocKy.AppendLine($"   • {hk.TenHocKy} ({hk.MaNamHoc})");
                }
                loiHocKy.AppendLine();
                loiHocKy.AppendLine("💡 LÝ DO:");
                loiHocKy.AppendLine("   - Hệ thống chỉ cho phép lưu điểm, hạnh kiểm, xếp loại");
                loiHocKy.AppendLine("     cho các học kỳ đã tồn tại trong database.");
                loiHocKy.AppendLine("   - Nếu trường mới mở (bắt đầu từ 2025-2026),");
                loiHocKy.AppendLine("     chỉ cho phép học sinh khối 10 chuyển trường.");
                loiHocKy.AppendLine("   - Nếu cần nhận học sinh khối 11, 12, vui lòng");
                loiHocKy.AppendLine("     thêm các học kỳ tương ứng vào database trước.");
                loiHocKy.AppendLine();
                loiHocKy.AppendLine($"✅ Các học sinh khác (nếu có) sẽ được tiếp tục xử lý.");
                
                ScrollableMessageBox.Show("Cảnh báo: Một số học sinh không đủ điều kiện", loiHocKy.ToString(), MessageBoxIcon.Warning);
                
                // Rollback CHỈ những học sinh không đủ điều kiện
                List<string> keysToRemoveHocKy = new List<string>();
                foreach (var maHS in hocSinhKhongDuDieuKien)
                {
                    try
                    {
                        hocSinhBus.DeleteHocSinh(maHS);
                        string username = $"HS{maHS:D3}";
                        try { nguoiDungBLL.DeleteNguoiDung(username); } catch { }
                        
                        // Tìm key (hoTen) để xóa khỏi danh sách thành công
                        foreach (var kvp in hocSinhThanhCong)
                        {
                            if (kvp.Value.maHS == maHS)
                            {
                                keysToRemoveHocKy.Add(kvp.Key);
                                break;
                            }
                        }
                    }
                    catch { }
                }
                
                // Xóa khỏi danh sách thành công
                foreach (var key in keysToRemoveHocKy)
                {
                    hocSinhThanhCong.Remove(key);
                }
                
                // Nếu không còn học sinh nào → Dừng lại
                if (hocSinhThanhCong.Count == 0)
                {
                    return;
                }
            }

            // Lấy danh sách môn học
            var danhSachMonHoc = monHocDAO.DocDSMH();
            var monHocDict = danhSachMonHoc.ToDictionary(m => m.maMon, m => m.tenMon);

            var errors = new StringBuilder();
            int errorCount = 0;

            // ✅ Tự động phát hiện vị trí cột bằng cách đọc header row cho worksheet Diem
            int colHoTenDiem = -1, colTenHocKy = -1, colNamHoc = -1, colMaMonHoc = -1, 
                colTenMonHoc = -1, colDiemTX = -1, colDiemGK = -1, colDiemCK = -1, colDiemTB = -1;
            
            int headerRowDiem = 1;
            int maxColDiem = wsDiem.Dimension?.End.Column ?? 9;
            
            // ✅ DEBUG: Log header row để kiểm tra
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: Đọc header row của worksheet Diem ===");
            for (int col = 1; col <= maxColDiem; col++)
            {
                string headerText = wsDiem.Cells[headerRowDiem, col].Text.Trim();
                System.Diagnostics.Debug.WriteLine($"Cột {col}: '{headerText}'");
            }
            
            for (int col = 1; col <= maxColDiem; col++)
            {
                string headerText = wsDiem.Cells[headerRowDiem, col].Text.Trim().ToLower();
                // ✅ Sửa logic: Tìm "Họ và tên" - phải chứa "họ" và "tên" nhưng KHÔNG chứa "học kỳ" hoặc "môn"
                if (headerText.Contains("họ") && headerText.Contains("tên") && 
                    !headerText.Contains("học") && !headerText.Contains("kỳ") && !headerText.Contains("môn"))
                {
                    colHoTenDiem = col;
                }
                // ✅ Tìm "Tên học kỳ" - phải chứa "tên", "học", "kỳ" và KHÔNG chứa "môn"
                else if (headerText.Contains("tên") && headerText.Contains("học") && 
                         headerText.Contains("kỳ") && !headerText.Contains("môn"))
                {
                    colTenHocKy = col;
                }
                else if (headerText.Contains("năm") && headerText.Contains("học"))
                {
                    colNamHoc = col;
                }
                else if (headerText.Contains("mã") && headerText.Contains("môn"))
                {
                    colMaMonHoc = col;
                }
                // ✅ Tìm "Tên môn học" - phải chứa "tên" và "môn"
                else if (headerText.Contains("tên") && headerText.Contains("môn"))
                {
                    colTenMonHoc = col;
                }
                else if (headerText.Contains("thường") && headerText.Contains("xuyên"))
                {
                    colDiemTX = col;
                }
                else if (headerText.Contains("giữa") && headerText.Contains("kỳ"))
                {
                    colDiemGK = col;
                }
                else if (headerText.Contains("cuối") && headerText.Contains("kỳ"))
                {
                    colDiemCK = col;
                }
                else if (headerText.Contains("trung") && headerText.Contains("bình"))
                {
                    colDiemTB = col;
                }
            }
            
            // ✅ DEBUG: Log các cột đã tìm thấy
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: Các cột đã tìm thấy ===");
            System.Diagnostics.Debug.WriteLine($"colHoTenDiem = {colHoTenDiem}, colTenHocKy = {colTenHocKy}, colNamHoc = {colNamHoc}");
            System.Diagnostics.Debug.WriteLine($"colMaMonHoc = {colMaMonHoc}, colTenMonHoc = {colTenMonHoc}");
            System.Diagnostics.Debug.WriteLine($"colDiemTX = {colDiemTX}, colDiemGK = {colDiemGK}, colDiemCK = {colDiemCK}, colDiemTB = {colDiemTB}");
            
            // Fallback: Nếu không tìm thấy bằng header, dùng vị trí mặc định
            if (colHoTenDiem == -1) colHoTenDiem = 1;
            if (colTenHocKy == -1) colTenHocKy = 2;
            if (colNamHoc == -1) colNamHoc = 3;
            if (colMaMonHoc == -1) colMaMonHoc = 4;
            if (colTenMonHoc == -1) colTenMonHoc = 5;
            if (colDiemTX == -1) colDiemTX = 6;
            if (colDiemGK == -1) colDiemGK = 7;
            if (colDiemCK == -1) colDiemCK = 8;
            if (colDiemTB == -1) colDiemTB = 9;
            
            // ✅ DEBUG: Log sau khi fallback
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: Sau khi fallback ===");
            System.Diagnostics.Debug.WriteLine($"colHoTenDiem = {colHoTenDiem}, colTenHocKy = {colTenHocKy}, colNamHoc = {colNamHoc}");
            System.Diagnostics.Debug.WriteLine($"colMaMonHoc = {colMaMonHoc}, colTenMonHoc = {colTenMonHoc}");

            // 1. Nhập điểm
            // ✅ LƯU Ý: Lưu trữ dựa trên (TenHocKy, MaNamHoc) thay vì MaHocKy
            int rowCountDiem = wsDiem.Dimension?.Rows ?? 0;
            Dictionary<int, Dictionary<(string TenHocKy, string MaNamHoc), Dictionary<int, DiemSoDTO>>> diemTheoHS = 
                new Dictionary<int, Dictionary<(string TenHocKy, string MaNamHoc), Dictionary<int, DiemSoDTO>>>();

            for (int row = 2; row <= rowCountDiem; row++)
            {
                try
                {
                    // ✅ Đọc từ Excel: Tên học kỳ và Năm học thay vì Mã học kỳ
                    string tenHS = wsDiem.Cells[row, colHoTenDiem].Text.Trim();
                    string tenHocKy = wsDiem.Cells[row, colTenHocKy].Text.Trim();
                    string namHoc = wsDiem.Cells[row, colNamHoc].Text.Trim();
                    
                    // ✅ DEBUG: Log giá trị đọc từ Excel
                    System.Diagnostics.Debug.WriteLine($"Đọc từ Excel dòng {row}: Tên HS='{tenHS}', Tên học kỳ='{tenHocKy}', Năm học='{namHoc}'");
                    
                    // ✅ Hiển thị MessageBox để debug (chỉ hiển thị 5 dòng đầu để không spam)
                    // if (row <= 6)
                    // {
                    //     MessageBox.Show($"Dòng {row}:\nTên HS: '{tenHS}'\nTên học kỳ: '{tenHocKy}'\nNăm học: '{namHoc}'", 
                    //         "DEBUG: Đọc từ Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // }
                    string maMonHocStr = wsDiem.Cells[row, colMaMonHoc].Text.Trim();
                    string tenMonHoc = wsDiem.Cells[row, colTenMonHoc].Text.Trim();
                    string diemTXStr = wsDiem.Cells[row, colDiemTX].Text.Trim();
                    string diemGKStr = wsDiem.Cells[row, colDiemGK].Text.Trim();
                    string diemCKStr = wsDiem.Cells[row, colDiemCK].Text.Trim();
                    string diemTBStr = wsDiem.Cells[row, colDiemTB].Text.Trim();

                    if (string.IsNullOrWhiteSpace(tenHS))
                        continue;

                    // Tìm học sinh trong danh sách thành công
                    var hsMatch = hocSinhThanhCong.FirstOrDefault(kvp => 
                        kvp.Key.Equals(tenHS, StringComparison.OrdinalIgnoreCase));
                    if (hsMatch.Key == null)
                        continue;

                    int maHS = hsMatch.Value.maHS;
                    
                    // ✅ KHÔNG cần tìm học kỳ trong database - chỉ so sánh dựa trên (TenHocKy, MaNamHoc)
                    if (!int.TryParse(maMonHocStr, out int maMonHoc))
                    {
                        errors.AppendLine($"Dòng {row - 1} (Điểm): Mã môn học không hợp lệ ({maMonHocStr})");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                        continue;
                    }

                    // Kiểm tra học kỳ có trong danh sách học kỳ cần thiết của học sinh này không
                    if (!hocKyCanThiet.ContainsKey(maHS))
                    {
                        // Học sinh này không có trong danh sách học kỳ cần thiết → bỏ qua
                        continue;
                    }
                    
                    // ✅ So sánh dựa trên (TenHocKy, MaNamHoc) - không cần MaHocKy
                    bool hocKyCanThietCuaHS = hocKyCanThiet[maHS].Any(hk => 
                        hk.TenHocKy.Trim().Equals(tenHocKy.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        hk.MaNamHoc.Trim() == namHoc.Trim());
                    
                    if (!hocKyCanThietCuaHS)
                    {
                        // Học kỳ này không cần thiết cho học sinh này → bỏ qua (không báo lỗi)
                        continue;
                    }

                    // Kiểm tra môn học
                    if (!monHocDict.ContainsKey(maMonHoc) || 
                        !monHocDict[maMonHoc].Equals(tenMonHoc, StringComparison.OrdinalIgnoreCase))
                    {
                        errors.AppendLine($"Dòng {row - 1} (Điểm): Môn học không hợp lệ ({tenMonHoc})");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                        continue;
                    }

                    // Parse điểm
                    if (!float.TryParse(diemTXStr, out float diemTX) || diemTX < 0 || diemTX > 10 ||
                        !float.TryParse(diemGKStr, out float diemGK) || diemGK < 0 || diemGK > 10 ||
                        !float.TryParse(diemCKStr, out float diemCK) || diemCK < 0 || diemCK > 10 ||
                        !float.TryParse(diemTBStr, out float diemTB) || diemTB < 0 || diemTB > 10)
                    {
                        errors.AppendLine($"Dòng {row - 1} (Điểm): Điểm không hợp lệ (phải từ 0-10)");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                        continue;
                    }

                    // Lưu điểm vào dictionary - key là (TenHocKy, MaNamHoc)
                    var hocKyKey = (TenHocKy: tenHocKy.Trim(), MaNamHoc: namHoc.Trim());
                    if (!diemTheoHS.ContainsKey(maHS))
                        diemTheoHS[maHS] = new Dictionary<(string TenHocKy, string MaNamHoc), Dictionary<int, DiemSoDTO>>();
                    if (!diemTheoHS[maHS].ContainsKey(hocKyKey))
                        diemTheoHS[maHS][hocKyKey] = new Dictionary<int, DiemSoDTO>();

                    // ✅ LƯU Ý: MaHocKy = 0 vì không lưu vào database, chỉ để xét điều kiện
                    diemTheoHS[maHS][hocKyKey][maMonHoc] = new DiemSoDTO
                    {
                        MaHocSinh = maHS.ToString(), // ✅ DiemSoDTO.MaHocSinh là string
                        MaMonHoc = maMonHoc,
                        MaHocKy = 0, // ✅ Không cần MaHocKy vì chỉ xét điều kiện, không lưu vào DB
                        DiemThuongXuyen = diemTX,
                        DiemGiuaKy = diemGK,
                        DiemCuoiKy = diemCK,
                        DiemTrungBinh = diemTB
                    };
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1} (Điểm): {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ Tự động phát hiện vị trí cột bằng cách đọc header row cho worksheet HanhKiem
            int colHoTenHK = -1, colTenHocKyHK = -1, colNamHocHK = -1, colXepLoai = -1, colNhanXet = -1;
            
            int headerRowHK = 1;
            int maxColHK = wsHanhKiem.Dimension?.End.Column ?? 5;
            
            // ✅ DEBUG: Log header row để kiểm tra
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: Đọc header row của worksheet HanhKiem ===");
            for (int col = 1; col <= maxColHK; col++)
            {
                string headerText = wsHanhKiem.Cells[headerRowHK, col].Text.Trim();
                System.Diagnostics.Debug.WriteLine($"Cột {col}: '{headerText}'");
            }
            
            for (int col = 1; col <= maxColHK; col++)
            {
                string headerText = wsHanhKiem.Cells[headerRowHK, col].Text.Trim().ToLower();
                // ✅ Sửa logic: Tìm "Họ và tên" - phải chứa "họ" và "tên" nhưng KHÔNG chứa "học kỳ"
                if (headerText.Contains("họ") && headerText.Contains("tên") && 
                    !headerText.Contains("học") && !headerText.Contains("kỳ"))
                {
                    colHoTenHK = col;
                }
                // ✅ Tìm "Tên học kỳ" - phải chứa "tên", "học", "kỳ"
                else if (headerText.Contains("tên") && headerText.Contains("học") && headerText.Contains("kỳ"))
                {
                    colTenHocKyHK = col;
                }
                else if (headerText.Contains("năm") && headerText.Contains("học"))
                {
                    colNamHocHK = col;
                }
                else if (headerText.Contains("xếp") && headerText.Contains("loại"))
                {
                    colXepLoai = col;
                }
                else if (headerText.Contains("nhận") && headerText.Contains("xét"))
                {
                    colNhanXet = col;
                }
            }
            
            // ✅ DEBUG: Log các cột đã tìm thấy
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: Các cột đã tìm thấy (HanhKiem) ===");
            System.Diagnostics.Debug.WriteLine($"colHoTenHK = {colHoTenHK}, colTenHocKyHK = {colTenHocKyHK}, colNamHocHK = {colNamHocHK}");
            System.Diagnostics.Debug.WriteLine($"colXepLoai = {colXepLoai}, colNhanXet = {colNhanXet}");
            
            // Fallback
            if (colHoTenHK == -1) colHoTenHK = 1;
            if (colTenHocKyHK == -1) colTenHocKyHK = 2;
            if (colNamHocHK == -1) colNamHocHK = 3;
            if (colXepLoai == -1) colXepLoai = 4;
            if (colNhanXet == -1) colNhanXet = 5;

            // 2. Nhập hạnh kiểm
            // ✅ LƯU Ý: Lưu trữ dựa trên (TenHocKy, MaNamHoc) thay vì MaHocKy
            int rowCountHanhKiem = wsHanhKiem.Dimension?.Rows ?? 0;
            Dictionary<int, Dictionary<(string TenHocKy, string MaNamHoc), HanhKiemDTO>> hanhKiemTheoHS = 
                new Dictionary<int, Dictionary<(string TenHocKy, string MaNamHoc), HanhKiemDTO>>();

            for (int row = 2; row <= rowCountHanhKiem; row++)
            {
                try
                {
                    // ✅ Đọc từ Excel: Tên học kỳ và Năm học thay vì Mã học kỳ
                    string tenHS = wsHanhKiem.Cells[row, colHoTenHK].Text.Trim();
                    string tenHocKy = wsHanhKiem.Cells[row, colTenHocKyHK].Text.Trim();
                    string namHoc = wsHanhKiem.Cells[row, colNamHocHK].Text.Trim();
                    string xepLoai = wsHanhKiem.Cells[row, colXepLoai].Text.Trim();
                    string nhanXet = wsHanhKiem.Cells[row, colNhanXet].Text.Trim();
                    
                    // ✅ DEBUG: Log giá trị đọc từ Excel (chỉ 3 dòng đầu)
                    if (row <= 4)
                    {
                        System.Diagnostics.Debug.WriteLine($"Đọc từ Excel (HanhKiem) dòng {row}: Tên HS='{tenHS}', Tên học kỳ='{tenHocKy}', Năm học='{namHoc}', Xếp loại='{xepLoai}'");
                    }

                    if (string.IsNullOrWhiteSpace(tenHS))
                        continue;

                    var hsMatch = hocSinhThanhCong.FirstOrDefault(kvp => 
                        kvp.Key.Equals(tenHS, StringComparison.OrdinalIgnoreCase));
                    if (hsMatch.Key == null)
                        continue;

                    int maHS = hsMatch.Value.maHS;
                    
                    // ✅ KHÔNG cần tìm học kỳ trong database - chỉ so sánh dựa trên (TenHocKy, MaNamHoc)
                    // Kiểm tra học kỳ có trong danh sách học kỳ cần thiết của học sinh này không
                    if (!hocKyCanThiet.ContainsKey(maHS))
                    {
                        continue;
                    }
                    
                    // ✅ So sánh dựa trên (TenHocKy, MaNamHoc) - không cần MaHocKy
                    bool hocKyCanThietCuaHS = hocKyCanThiet[maHS].Any(hk => 
                        hk.TenHocKy.Trim().Equals(tenHocKy.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        hk.MaNamHoc.Trim() == namHoc.Trim());
                    
                    if (!hocKyCanThietCuaHS)
                    {
                        // Bỏ qua học kỳ không cần thiết
                        continue;
                    }

                    if (xepLoai != "Tốt" && xepLoai != "Khá" && xepLoai != "Trung bình" && xepLoai != "Yếu")
                    {
                        errors.AppendLine($"Dòng {row - 1} (Hạnh kiểm): Xếp loại không hợp lệ ({xepLoai})");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                        continue;
                    }

                    // Lưu hạnh kiểm vào dictionary - key là (TenHocKy, MaNamHoc)
                    var hocKyKey = (TenHocKy: tenHocKy.Trim(), MaNamHoc: namHoc.Trim());
                    if (!hanhKiemTheoHS.ContainsKey(maHS))
                        hanhKiemTheoHS[maHS] = new Dictionary<(string TenHocKy, string MaNamHoc), HanhKiemDTO>();

                    hanhKiemTheoHS[maHS][hocKyKey] = new HanhKiemDTO
                    {
                        MaHocSinh = maHS,
                        MaHocKy = 0, // ✅ Không cần MaHocKy vì chỉ xét điều kiện, không lưu vào DB
                        XepLoai = xepLoai,
                        NhanXet = nhanXet
                    };
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1} (Hạnh kiểm): {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ Tự động phát hiện vị trí cột bằng cách đọc header row cho worksheet XepLoai
            int colHoTenXL = -1, colTenHocKyXL = -1, colNamHocXL = -1, colHocLuc = -1, colGhiChu = -1;
            
            int headerRowXL = 1;
            int maxColXL = wsXepLoai.Dimension?.End.Column ?? 5;
            
            // ✅ DEBUG: Log header row để kiểm tra
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: Đọc header row của worksheet XepLoai ===");
            for (int col = 1; col <= maxColXL; col++)
            {
                string headerText = wsXepLoai.Cells[headerRowXL, col].Text.Trim();
                System.Diagnostics.Debug.WriteLine($"Cột {col}: '{headerText}'");
            }
            
            for (int col = 1; col <= maxColXL; col++)
            {
                string headerText = wsXepLoai.Cells[headerRowXL, col].Text.Trim().ToLower();
                // ✅ Sửa logic: Tìm "Họ và tên" - phải chứa "họ" và "tên" nhưng KHÔNG chứa "học kỳ"
                if (headerText.Contains("họ") && headerText.Contains("tên") && 
                    !headerText.Contains("học") && !headerText.Contains("kỳ"))
                {
                    colHoTenXL = col;
                }
                // ✅ Tìm "Tên học kỳ" - phải chứa "tên", "học", "kỳ"
                else if (headerText.Contains("tên") && headerText.Contains("học") && headerText.Contains("kỳ"))
                {
                    colTenHocKyXL = col;
                }
                else if (headerText.Contains("năm") && headerText.Contains("học"))
                {
                    colNamHocXL = col;
                }
                else if (headerText.Contains("học") && headerText.Contains("lực"))
                {
                    colHocLuc = col;
                }
                else if (headerText.Contains("ghi") && headerText.Contains("chú"))
                {
                    colGhiChu = col;
                }
            }
            
            // ✅ DEBUG: Log các cột đã tìm thấy
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: Các cột đã tìm thấy (XepLoai) ===");
            System.Diagnostics.Debug.WriteLine($"colHoTenXL = {colHoTenXL}, colTenHocKyXL = {colTenHocKyXL}, colNamHocXL = {colNamHocXL}");
            System.Diagnostics.Debug.WriteLine($"colHocLuc = {colHocLuc}, colGhiChu = {colGhiChu}");
            
            // Fallback
            if (colHoTenXL == -1) colHoTenXL = 1;
            if (colTenHocKyXL == -1) colTenHocKyXL = 2;
            if (colNamHocXL == -1) colNamHocXL = 3;
            if (colHocLuc == -1) colHocLuc = 4;
            if (colGhiChu == -1) colGhiChu = 5;

            // 3. Nhập xếp loại và kiểm tra điều kiện
            int rowCountXepLoai = wsXepLoai.Dimension?.Rows ?? 0;
            // ✅ LƯU Ý: Lưu trữ dựa trên (TenHocKy, MaNamHoc) thay vì MaHocKy
            Dictionary<int, Dictionary<(string TenHocKy, string MaNamHoc), XepLoaiDTO>> xepLoaiTheoHS = 
                new Dictionary<int, Dictionary<(string TenHocKy, string MaNamHoc), XepLoaiDTO>>();

            for (int row = 2; row <= rowCountXepLoai; row++)
            {
                try
                {
                    // ✅ Đọc từ Excel: Tên học kỳ và Năm học thay vì Mã học kỳ
                    string tenHS = wsXepLoai.Cells[row, colHoTenXL].Text.Trim();
                    string tenHocKy = wsXepLoai.Cells[row, colTenHocKyXL].Text.Trim();
                    string namHoc = wsXepLoai.Cells[row, colNamHocXL].Text.Trim();
                    string hocLuc = wsXepLoai.Cells[row, colHocLuc].Text.Trim();
                    string ghiChu = wsXepLoai.Cells[row, colGhiChu].Text.Trim();
                    
                    // ✅ DEBUG: Log giá trị đọc từ Excel (chỉ 3 dòng đầu)
                    if (row <= 4)
                    {
                        System.Diagnostics.Debug.WriteLine($"Đọc từ Excel (XepLoai) dòng {row}: Tên HS='{tenHS}', Tên học kỳ='{tenHocKy}', Năm học='{namHoc}', Học lực='{hocLuc}'");
                    }

                    if (string.IsNullOrWhiteSpace(tenHS))
                        continue;

                    var hsMatch = hocSinhThanhCong.FirstOrDefault(kvp => 
                        kvp.Key.Equals(tenHS, StringComparison.OrdinalIgnoreCase));
                    if (hsMatch.Key == null)
                        continue;

                    int maHS = hsMatch.Value.maHS;
                    
                    // ✅ KHÔNG cần tìm học kỳ trong database - chỉ so sánh dựa trên (TenHocKy, MaNamHoc)
                    // Kiểm tra học kỳ có trong danh sách học kỳ cần thiết của học sinh này không
                    if (!hocKyCanThiet.ContainsKey(maHS))
                    {
                        continue;
                    }
                    
                    // ✅ So sánh dựa trên (TenHocKy, MaNamHoc) - không cần MaHocKy
                    bool hocKyCanThietCuaHS = hocKyCanThiet[maHS].Any(hk => 
                        hk.TenHocKy.Trim().Equals(tenHocKy.Trim(), StringComparison.OrdinalIgnoreCase) &&
                        hk.MaNamHoc.Trim() == namHoc.Trim());
                    
                    if (!hocKyCanThietCuaHS)
                    {
                        // Bỏ qua học kỳ không cần thiết
                        continue;
                    }

                    // ✅ KIỂM TRA ĐIỀU KIỆN: Học lực không được "Yếu" hoặc "Kém"
                    if (hocLuc == "Yếu" || hocLuc == "Kém")
                    {
                        errors.AppendLine($"Dòng {row - 1} (Xếp loại): Học sinh {tenHS} có học lực '{hocLuc}' - KHÔNG ĐỦ ĐIỀU KIỆN");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                        continue;
                    }

                    if (hocLuc != "Giỏi" && hocLuc != "Khá" && hocLuc != "Trung bình" && 
                        hocLuc != "Yếu" && hocLuc != "Kém")
                    {
                        errors.AppendLine($"Dòng {row - 1} (Xếp loại): Học lực không hợp lệ ({hocLuc})");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                        continue;
                    }

                    // Lưu xếp loại vào dictionary - key là (TenHocKy, MaNamHoc)
                    var hocKyKey = (TenHocKy: tenHocKy.Trim(), MaNamHoc: namHoc.Trim());
                    if (!xepLoaiTheoHS.ContainsKey(maHS))
                        xepLoaiTheoHS[maHS] = new Dictionary<(string TenHocKy, string MaNamHoc), XepLoaiDTO>();

                    xepLoaiTheoHS[maHS][hocKyKey] = new XepLoaiDTO
                    {
                        MaHocSinh = maHS,
                        MaHocKy = 0, // ✅ Không cần MaHocKy vì chỉ xét điều kiện, không lưu vào DB
                        HocLuc = hocLuc,
                        GhiChu = ghiChu
                    };
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1} (Xếp loại): {ex.Message}");
                    errorCount++;
                }
            }

            // 4. Kiểm tra đầy đủ dữ liệu cho từng học sinh (theo khối)
            foreach (var kvp in hocSinhThanhCong)
            {
                int maHS = kvp.Value.maHS;
                string tenHS = kvp.Key;
                string khoiStr = kvp.Value.khoi;

                // Lấy danh sách học kỳ cần thiết cho học sinh này
                if (!hocKyCanThiet.ContainsKey(maHS))
                {
                    // Không tìm thấy trong dictionary → lỗi logic
                    errors.AppendLine($"Học sinh {tenHS} (Khối {khoiStr}): Không xác định được các học kỳ cần thiết (lỗi logic)");
                    errorCount++;
                    hocSinhDuDieuKien[maHS] = false;
                    continue;
                }
                
                var hocKyCanThietCuaHS = hocKyCanThiet[maHS];
                
                // ✅ Nếu danh sách học kỳ cần thiết rỗng (ví dụ: khối 10, HK1 đang diễn ra)
                // → Không cần check điểm nào → Coi là thỏa điều kiện
                if (hocKyCanThietCuaHS.Count == 0)
                {
                    // Học sinh này không cần học kỳ nào (ví dụ: khối 10, HK1) → Thỏa điều kiện
                    // Không cần làm gì, giữ nguyên hocSinhDuDieuKien[maHS] = true
                    continue;
                }

                // Kiểm tra đầy đủ điểm cho tất cả học kỳ cần thiết và tất cả môn học
                foreach (var hk in hocKyCanThietCuaHS)
                {
                    var hocKyKey = (TenHocKy: hk.TenHocKy.Trim(), MaNamHoc: hk.MaNamHoc.Trim());
                    
                    // ✅ Kiểm tra dựa trên (TenHocKy, MaNamHoc) thay vì MaHocKy
                    bool coDiemHK = diemTheoHS.ContainsKey(maHS) && diemTheoHS[maHS].ContainsKey(hocKyKey);
                    if (!coDiemHK)
                    {
                        errors.AppendLine($"Học sinh {tenHS} (Khối {khoiStr}): Thiếu điểm học kỳ {hk.TenHocKy} ({hk.MaNamHoc})");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                        continue;
                    }

                    foreach (var mon in danhSachMonHoc)
                    {
                        if (!diemTheoHS[maHS][hocKyKey].ContainsKey(mon.maMon))
                        {
                            errors.AppendLine($"Học sinh {tenHS} (Khối {khoiStr}): Thiếu điểm môn {mon.tenMon} học kỳ {hk.TenHocKy} ({hk.MaNamHoc})");
                            errorCount++;
                            hocSinhDuDieuKien[maHS] = false;
                        }
                    }
                }

                // Kiểm tra đầy đủ hạnh kiểm cho tất cả học kỳ cần thiết
                foreach (var hk in hocKyCanThietCuaHS)
                {
                    var hocKyKey = (TenHocKy: hk.TenHocKy.Trim(), MaNamHoc: hk.MaNamHoc.Trim());
                    if (!hanhKiemTheoHS.ContainsKey(maHS) || !hanhKiemTheoHS[maHS].ContainsKey(hocKyKey))
                    {
                        errors.AppendLine($"Học sinh {tenHS} (Khối {khoiStr}): Thiếu hạnh kiểm học kỳ {hk.TenHocKy} ({hk.MaNamHoc})");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                    }
                }

                // Kiểm tra đầy đủ xếp loại cho tất cả học kỳ cần thiết
                foreach (var hk in hocKyCanThietCuaHS)
                {
                    var hocKyKey = (TenHocKy: hk.TenHocKy.Trim(), MaNamHoc: hk.MaNamHoc.Trim());
                    if (!xepLoaiTheoHS.ContainsKey(maHS) || !xepLoaiTheoHS[maHS].ContainsKey(hocKyKey))
                    {
                        errors.AppendLine($"Học sinh {tenHS} (Khối {khoiStr}): Thiếu xếp loại học kỳ {hk.TenHocKy} ({hk.MaNamHoc})");
                        errorCount++;
                        hocSinhDuDieuKien[maHS] = false;
                    }
                }
            }

            // ✅ LƯU ĐIỂM, HẠNH KIỂM, XẾP LOẠI VÀO SQL
            // 1. Lưu cho các học kỳ cần thiết (nếu có) - chỉ lưu nếu học kỳ tồn tại trong DB
            // 2. LUÔN LUÔN lưu cho học kỳ HIỆN TẠI (đang diễn ra) để học sinh hiện trong giao diện
            int soDiemDaLuu = 0;
            int soHanhKiemDaLuu = 0;
            int soXepLoaiDaLuu = 0;
            StringBuilder loiLuu = new StringBuilder();
            
            // Lấy MaHocKy của học kỳ hiện tại
            int maHocKyHienTai = hocKyHienTai.MaHocKy;
            var hocKyHienTaiKey = (TenHocKy: hocKyHienTai.TenHocKy.Trim(), MaNamHoc: hocKyHienTai.MaNamHoc.Trim());
            
            foreach (var kvp in hocSinhDuDieuKien.Where(kvp => kvp.Value))
            {
                int maHS = kvp.Key;
                
                // ✅ BƯỚC 1: Lưu điểm/hạnh kiểm/xếp loại cho các học kỳ CẦN THIẾT (nếu có)
                if (hocKyCanThiet.ContainsKey(maHS) && hocKyCanThiet[maHS].Count > 0)
                {
                    // Kiểm tra học sinh có dữ liệu điểm, hạnh kiểm, xếp loại không
                    if (diemTheoHS.ContainsKey(maHS) && hanhKiemTheoHS.ContainsKey(maHS) && xepLoaiTheoHS.ContainsKey(maHS))
                    {
                        // Lưu điểm cho tất cả học kỳ cần thiết
                        foreach (var hk in hocKyCanThiet[maHS])
                        {
                            var hocKyKey = (TenHocKy: hk.TenHocKy.Trim(), MaNamHoc: hk.MaNamHoc.Trim());
                            
                            // Kiểm tra học kỳ có tồn tại trong DB không
                            if (!hocKyTonTai.ContainsKey(hocKyKey))
                                continue; // Bỏ qua nếu học kỳ không tồn tại
                            
                            int maHocKy = hocKyTonTai[hocKyKey];
                            
                            // Lưu điểm
                            if (diemTheoHS[maHS].ContainsKey(hocKyKey))
                            {
                                foreach (var diem in diemTheoHS[maHS][hocKyKey].Values)
                                {
                                    try
                                    {
                                        diem.MaHocKy = maHocKy;
                                        if (diemSoDAO.UpsertDiemSo(diem))
                                        {
                                            soDiemDaLuu++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        loiLuu.AppendLine($"Lỗi lưu điểm (HS {maHS}, HK {maHocKy}, Môn {diem.MaMonHoc}): {ex.Message}");
                                    }
                                }
                            }
                            
                            // Lưu hạnh kiểm
                            if (hanhKiemTheoHS[maHS].ContainsKey(hocKyKey))
                            {
                                try
                                {
                                    var hkDTO = hanhKiemTheoHS[maHS][hocKyKey];
                                    hkDTO.MaHocKy = maHocKy;
                                    if (hanhKiemDAO.LuuHanhKiem(hkDTO))
                                    {
                                        soHanhKiemDaLuu++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    loiLuu.AppendLine($"Lỗi lưu hạnh kiểm (HS {maHS}, HK {maHocKy}): {ex.Message}");
                                }
                            }
                            
                            // Lưu xếp loại
                            if (xepLoaiTheoHS[maHS].ContainsKey(hocKyKey))
                            {
                                try
                                {
                                    var xlDTO = xepLoaiTheoHS[maHS][hocKyKey];
                                    if (xepLoaiDAO.LuuXepLoai(xlDTO.MaHocSinh, maHocKy, xlDTO.HocLuc, xlDTO.GhiChu ?? ""))
                                    {
                                        soXepLoaiDaLuu++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    loiLuu.AppendLine($"Lỗi lưu xếp loại (HS {maHS}, HK {maHocKy}): {ex.Message}");
                                }
                            }
                        }
                    }
                }
                
                // ✅ BƯỚC 2: LUÔN LUÔN lưu điểm/hạnh kiểm/xếp loại cho học kỳ HIỆN TẠI
                // Nếu Excel có dữ liệu → Lưu dữ liệu từ Excel
                // Nếu Excel không có dữ liệu → Tạo bản ghi rỗng (NULL) để học sinh hiện trong giao diện
                
                // Lưu điểm cho học kỳ hiện tại
                if (diemTheoHS.ContainsKey(maHS) && diemTheoHS[maHS].ContainsKey(hocKyHienTaiKey))
                {
                    // Excel có dữ liệu → Lưu dữ liệu từ Excel
                    foreach (var diem in diemTheoHS[maHS][hocKyHienTaiKey].Values)
                    {
                        try
                        {
                            diem.MaHocKy = maHocKyHienTai;
                            if (diemSoDAO.UpsertDiemSo(diem))
                            {
                                soDiemDaLuu++;
                            }
                        }
                        catch (Exception ex)
                        {
                            loiLuu.AppendLine($"Lỗi lưu điểm (HS {maHS}, HK {maHocKyHienTai}, Môn {diem.MaMonHoc}): {ex.Message}");
                        }
                    }
                }
                else
                {
                    // Excel không có dữ liệu → Tạo bản ghi rỗng cho tất cả 13 môn học
                    // Sử dụng danhSachMonHoc đã khai báo ở đầu method
                    foreach (var monHoc in danhSachMonHoc)
                    {
                        try
                        {
                            var diemRong = new DiemSoDTO
                            {
                                MaHocSinh = maHS.ToString(),
                                MaMonHoc = monHoc.maMon,
                                MaHocKy = maHocKyHienTai,
                                DiemThuongXuyen = null,
                                DiemGiuaKy = null,
                                DiemCuoiKy = null,
                                DiemTrungBinh = null
                            };
                            if (diemSoDAO.UpsertDiemSo(diemRong))
                            {
                                soDiemDaLuu++;
                            }
                        }
                        catch (Exception ex)
                        {
                            loiLuu.AppendLine($"Lỗi tạo bản ghi điểm rỗng (HS {maHS}, HK {maHocKyHienTai}, Môn {monHoc.maMon}): {ex.Message}");
                        }
                    }
                }
                
                // Lưu hạnh kiểm cho học kỳ hiện tại
                if (hanhKiemTheoHS.ContainsKey(maHS) && hanhKiemTheoHS[maHS].ContainsKey(hocKyHienTaiKey))
                {
                    // Excel có dữ liệu → Lưu dữ liệu từ Excel
                    try
                    {
                        var hkDTO = hanhKiemTheoHS[maHS][hocKyHienTaiKey];
                        hkDTO.MaHocKy = maHocKyHienTai;
                        if (hanhKiemDAO.LuuHanhKiem(hkDTO))
                        {
                            soHanhKiemDaLuu++;
                        }
                    }
                    catch (Exception ex)
                    {
                        loiLuu.AppendLine($"Lỗi lưu hạnh kiểm (HS {maHS}, HK {maHocKyHienTai}): {ex.Message}");
                    }
                }
                else
                {
                    // Excel không có dữ liệu → Tạo bản ghi rỗng (NULL)
                    try
                    {
                        var hkRong = new HanhKiemDTO
                        {
                            MaHocSinh = maHS,
                            MaHocKy = maHocKyHienTai,
                            XepLoai = null, // NULL để hiển thị rỗng trong giao diện
                            NhanXet = null
                        };
                        if (hanhKiemDAO.LuuHanhKiem(hkRong))
                        {
                            soHanhKiemDaLuu++;
                        }
                    }
                    catch (Exception ex)
                    {
                        loiLuu.AppendLine($"Lỗi tạo bản ghi hạnh kiểm rỗng (HS {maHS}, HK {maHocKyHienTai}): {ex.Message}");
                    }
                }
                
                // Lưu xếp loại cho học kỳ hiện tại
                if (xepLoaiTheoHS.ContainsKey(maHS) && xepLoaiTheoHS[maHS].ContainsKey(hocKyHienTaiKey))
                {
                    // Excel có dữ liệu → Lưu dữ liệu từ Excel
                    try
                    {
                        var xlDTO = xepLoaiTheoHS[maHS][hocKyHienTaiKey];
                        if (xepLoaiDAO.LuuXepLoai(xlDTO.MaHocSinh, maHocKyHienTai, xlDTO.HocLuc, xlDTO.GhiChu ?? ""))
                        {
                            soXepLoaiDaLuu++;
                        }
                    }
                    catch (Exception ex)
                    {
                        loiLuu.AppendLine($"Lỗi lưu xếp loại (HS {maHS}, HK {maHocKyHienTai}): {ex.Message}");
                    }
                }
                else
                {
                    // Excel không có dữ liệu → Tạo bản ghi rỗng (NULL)
                    try
                    {
                        if (xepLoaiDAO.LuuXepLoai(maHS, maHocKyHienTai, null, ""))
                        {
                            soXepLoaiDaLuu++;
                        }
                    }
                    catch (Exception ex)
                    {
                        loiLuu.AppendLine($"Lỗi tạo bản ghi xếp loại rỗng (HS {maHS}, HK {maHocKyHienTai}): {ex.Message}");
                    }
                }
            }
            
            // ✅ Thông báo kết quả lưu dữ liệu (nếu có)
            if (soDiemDaLuu > 0 || soHanhKiemDaLuu > 0 || soXepLoaiDaLuu > 0)
            {
                System.Diagnostics.Debug.WriteLine($"✅ Đã lưu vào SQL: {soDiemDaLuu} điểm, {soHanhKiemDaLuu} hạnh kiểm, {soXepLoaiDaLuu} xếp loại");
            }
            if (loiLuu.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine($"⚠️ Lỗi khi lưu: {loiLuu}");
            }

            // ✅ Hiển thị kết quả kiểm tra chi tiết
            StringBuilder resultKiemTra = new StringBuilder();
            resultKiemTra.AppendLine("╔════════════════════════════════════════════════╗");
            resultKiemTra.AppendLine("║   KẾT QUẢ KIỂM TRA ĐIỀU KIỆN CHUYỂN TRƯỜNG     ║");
            resultKiemTra.AppendLine("╚════════════════════════════════════════════════╝");
            resultKiemTra.AppendLine();
            
            // Đếm số học sinh thỏa và không thỏa điều kiện
            int soHSThoaDieuKien = hocSinhDuDieuKien.Count(kvp => kvp.Value);
            int soHSKhongThoaDieuKien = hocSinhDuDieuKien.Count(kvp => !kvp.Value);
            
            resultKiemTra.AppendLine($"📊 TỔNG KẾT:");
            resultKiemTra.AppendLine($"   ✓ Thỏa điều kiện: {soHSThoaDieuKien} học sinh");
            if (soHSKhongThoaDieuKien > 0)
            {
                resultKiemTra.AppendLine($"   ✗ Không thỏa điều kiện: {soHSKhongThoaDieuKien} học sinh");
            }
            resultKiemTra.AppendLine();
            
            // Danh sách học sinh thỏa điều kiện
            if (soHSThoaDieuKien > 0)
            {
                resultKiemTra.AppendLine("✅ DANH SÁCH HỌC SINH THỎA ĐIỀU KIỆN:");
                foreach (var kvp in hocSinhDuDieuKien.Where(kvp => kvp.Value))
                {
                    var hsInfo = hocSinhThanhCong.FirstOrDefault(h => h.Value.maHS == kvp.Key);
                    if (hsInfo.Key != null)
                    {
                        resultKiemTra.AppendLine($"   • {hsInfo.Key} (Khối {hsInfo.Value.khoi}, Mã HS: {kvp.Key})");
                    }
                }
                resultKiemTra.AppendLine();
            }
            
            // Chi tiết lỗi (nếu có)
            if (errorCount > 0)
            {
                resultKiemTra.AppendLine("❌ CHI TIẾT LỖI:");
                resultKiemTra.Append(errors);
                resultKiemTra.AppendLine();
            }
            
            // Hiển thị thông báo
            if (errorCount > 0 || soHSKhongThoaDieuKien > 0)
            {
                ScrollableMessageBox.Show("Kết quả kiểm tra điều kiện", resultKiemTra.ToString(), MessageBoxIcon.Warning);
            }
            else if (soHSThoaDieuKien > 0)
            {
                ScrollableMessageBox.Show("Kết quả kiểm tra điều kiện", resultKiemTra.ToString(), MessageBoxIcon.Information);
            }

            // ✅ ROLLBACK học sinh không đủ điều kiện (xóa học sinh, mối quan hệ, phụ huynh mới tạo)
            var keysToRemove = hocSinhDuDieuKien.Where(kvp => !kvp.Value).Select(kvp => kvp.Key).ToList();
            
            // ✅ Track các phụ huynh cần kiểm tra xóa (phụ huynh liên quan đến học sinh bị rollback)
            HashSet<int> phuHuynhCanKiemTraXoa = new HashSet<int>();
            
            foreach (var maHS in keysToRemove)
            {
                try
                {
                    // ✅ Lấy danh sách phụ huynh liên quan đến học sinh này TRƯỚC KHI xóa mối quan hệ
                    try 
                    { 
                        var danhSachPhuHuynh = hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(maHS);
                        foreach (var (phuHuynh, moiQuanHe) in danhSachPhuHuynh)
                        {
                            // Kiểm tra xem phụ huynh này có trong danh sách phụ huynh thành công không
                            // (tức là phụ huynh này được thêm trong lần import này)
                            if (phuHuynhThanhCong.Values.Any(v => v.maPH == phuHuynh.MaPhuHuynh))
                            {
                                phuHuynhCanKiemTraXoa.Add(phuHuynh.MaPhuHuynh);
                            }
                        }
                    } 
                    catch { }
                    
                    // Xóa mối quan hệ
                    try { hocSinhPhuHuynhBLL.DeleteQuanHeByHocSinh(maHS); } catch { }

                    // Xóa học sinh
                    hocSinhBus.DeleteHocSinh(maHS);
                    string username = $"HS{maHS:D3}";
                    try { nguoiDungBLL.DeleteNguoiDung(username); } catch { }

                    // Xóa khỏi dictionary
                    var keyToRemove = hocSinhThanhCong.FirstOrDefault(kvp => kvp.Value.maHS == maHS);
                    if (!string.IsNullOrEmpty(keyToRemove.Key))
                    {
                        hocSinhThanhCong.Remove(keyToRemove.Key);
                    }
                }
                catch { }
            }
            
            // ✅ Xóa phụ huynh mới tạo nếu không còn học sinh nào liên quan
            foreach (var maPH in phuHuynhCanKiemTraXoa)
            {
                try
                {
                    // Kiểm tra xem phụ huynh này có còn học sinh nào liên quan không
                    var danhSachHocSinh = hocSinhPhuHuynhBLL.GetHocSinhByPhuHuynh(maPH);
                    if (danhSachHocSinh == null || danhSachHocSinh.Count == 0)
                    {
                        // Không còn học sinh nào liên quan → Xóa phụ huynh
                        try 
                        { 
                            phuHuynhBLL.DeletePhuHuynh(maPH);
                        } 
                        catch { }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Tự động phân lớp cho học sinh chuyển trường
        /// </summary>
        /// <summary>
        /// Phân lớp tự động cho học sinh chuyển trường - Phân lớp cho CẢ HK1 và HK2 cùng lúc
        /// </summary>
        private void PhanLopTuDongChoHocSinhChuyenTruong(
            Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong,
            HocKyDTO hk1,
            HocKyDTO hk2)
        {
            if (hocSinhThanhCong.Count == 0)
                return;

            // ✅ Kiểm tra có đủ HK1 và HK2 không
            if (hk1 == null || hk2 == null)
            {
                MessageBox.Show("Không thể phân lớp: Thiếu thông tin học kỳ HK1 hoặc HK2.", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy danh sách lớp
            var allLop = lopHocBus.DocDSLop();
            
            int successCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();
            var warnings = new StringBuilder();
            // ✅ Dictionary để lưu thông tin lớp đã phân cho từng học sinh
            Dictionary<int, (string tenLop, string nguyenVong, bool laNguyenVong)> lopDaPhan = 
                new Dictionary<int, (string, string, bool)>();
            // ✅ HashSet để track học sinh KHÔNG phân lớp được (cần rollback)
            HashSet<int> hocSinhKhongPhanLopDuoc = new HashSet<int>();

            foreach (var kvp in hocSinhThanhCong)
            {
                int maHS = kvp.Value.maHS;
                string tenHS = kvp.Key;
                string khoi = kvp.Value.khoi;
                string nguyenVong = kvp.Value.nguyenVong;

                try
                {
                    // Parse khối
                    if (!int.TryParse(khoi, out int maKhoi))
                    {
                        errors.AppendLine($"Học sinh {tenHS}: Khối không hợp lệ ({khoi})");
                        errorCount++;
                        hocSinhKhongPhanLopDuoc.Add(maHS); // ✅ Đánh dấu cần rollback
                        continue;
                    }

                    // Lấy danh sách lớp cùng khối
                    var lopCungKhoi = allLop.Where(l => l.maKhoi == maKhoi).ToList();
                    if (lopCungKhoi.Count == 0)
                    {
                        errors.AppendLine($"Học sinh {tenHS}: Không tìm thấy lớp nào trong khối {khoi}");
                        errorCount++;
                        hocSinhKhongPhanLopDuoc.Add(maHS); // ✅ Đánh dấu cần rollback
                        continue;
                    }

                    LopDTO lopDuocPhan = null;

                    // Nếu có nguyện vọng
                    if (!string.IsNullOrWhiteSpace(nguyenVong))
                    {
                        // Tìm lớp nguyện vọng
                        var lopNguyenVong = lopHocBus.LayLopTheoTen(nguyenVong);
                        if (lopNguyenVong != null)
                        {
                            // ✅ Kiểm tra lớp nguyện vọng cùng khối - BÁO LỖI nếu không khớp
                            if (lopNguyenVong.maKhoi == maKhoi)
                            {
                                // ✅ Kiểm tra sĩ số cho CẢ HK1 và HK2 (lấy max để đảm bảo cả 2 học kỳ đều còn chỗ)
                                int siSoHK1 = phanLopBLL.CountHocSinhInLop(lopNguyenVong.maLop, hk1.MaHocKy);
                                int siSoHK2 = phanLopBLL.CountHocSinhInLop(lopNguyenVong.maLop, hk2.MaHocKy);
                                int siSoHienTai = Math.Max(siSoHK1, siSoHK2);
                                
                                if (siSoHienTai < lopNguyenVong.siSo)
                                {
                                    lopDuocPhan = lopNguyenVong;
                                }
                                else
                                {
                                    warnings.AppendLine($"Học sinh {tenHS}: Lớp nguyện vọng '{nguyenVong}' đã đầy (HK1: {siSoHK1}, HK2: {siSoHK2}, Tối đa: {lopNguyenVong.siSo}) - Tự động phân lớp");
                                }
                            }
                            else
                            {
                                // ✅ BÁO LỖI nếu khối và nguyện vọng không khớp - sẽ rollback học sinh
                                errors.AppendLine($"Học sinh {tenHS}: Khối '{khoi}' không khớp với nguyện vọng chuyển lớp '{nguyenVong}' (Lớp {nguyenVong} thuộc khối {lopNguyenVong.maKhoi}, không phải khối {maKhoi})");
                                errorCount++;
                                hocSinhKhongPhanLopDuoc.Add(maHS); // ✅ Đánh dấu cần rollback
                                continue;
                            }
                        }
                        else
                        {
                            warnings.AppendLine($"Học sinh {tenHS}: Lớp nguyện vọng '{nguyenVong}' không tồn tại - Tự động phân lớp");
                        }
                    }

                    // Nếu không có lớp nguyện vọng phù hợp, tự động phân lớp
                    if (lopDuocPhan == null)
                    {
                        // ✅ Sắp xếp lớp theo sĩ số hiện tại (tăng dần) - ưu tiên lớp có ít học sinh nhất
                        // ✅ Lấy max sĩ số giữa HK1 và HK2 để đảm bảo cả 2 học kỳ đều còn chỗ
                        var lopConCho = lopCungKhoi
                            .Select(l => new
                            {
                                Lop = l,
                                SiSoHK1 = phanLopBLL.CountHocSinhInLop(l.maLop, hk1.MaHocKy),
                                SiSoHK2 = phanLopBLL.CountHocSinhInLop(l.maLop, hk2.MaHocKy),
                                SiSoHienTai = Math.Max(
                                    phanLopBLL.CountHocSinhInLop(l.maLop, hk1.MaHocKy),
                                    phanLopBLL.CountHocSinhInLop(l.maLop, hk2.MaHocKy)
                                )
                            })
                            .Where(x => x.SiSoHienTai < x.Lop.siSo)
                            .OrderBy(x => x.SiSoHienTai)
                            .ThenBy(x => x.Lop.tenLop)
                            .ToList();

                        if (lopConCho.Count > 0)
                        {
                            lopDuocPhan = lopConCho[0].Lop;
                        }
                        else
                        {
                            errors.AppendLine($"Học sinh {tenHS}: Khối {khoi} đã đầy, không thể phân lớp");
                            errorCount++;
                            hocSinhKhongPhanLopDuoc.Add(maHS); // ✅ Đánh dấu cần rollback
                            continue;
                        }
                    }

                    // ✅ Kiểm tra học sinh chưa được phân lớp trong CẢ HK1 và HK2
                    bool daPhanLopHK1 = phanLopBLL.CheckHocSinhDaPhanLop(maHS, hk1.MaHocKy);
                    bool daPhanLopHK2 = phanLopBLL.CheckHocSinhDaPhanLop(maHS, hk2.MaHocKy);
                    
                    if (daPhanLopHK1 && daPhanLopHK2)
                    {
                        warnings.AppendLine($"Học sinh {tenHS}: Đã được phân lớp trong cả HK1 và HK2 - Bỏ qua");
                        continue;
                    }
                    else if (daPhanLopHK1 || daPhanLopHK2)
                    {
                        // Nếu chỉ phân lớp một học kỳ, vẫn tiếp tục phân lớp cho học kỳ còn lại
                        warnings.AppendLine($"Học sinh {tenHS}: Đã được phân lớp trong {(daPhanLopHK1 ? "HK1" : "HK2")} - Sẽ phân lớp cho học kỳ còn lại");
                    }

                    // ✅ Phân lớp cho CẢ HK1 và HK2
                    try
                    {
                        bool successHK1 = false;
                        bool successHK2 = false;
                        
                        // Phân lớp cho HK1 (nếu chưa có)
                        if (!daPhanLopHK1)
                        {
                            successHK1 = phanLopBLL.AddPhanLop(maHS, lopDuocPhan.maLop, hk1.MaHocKy);
                        }
                        else
                        {
                            successHK1 = true; // Đã có rồi, coi như thành công
                        }
                        
                        // Phân lớp cho HK2 (nếu chưa có)
                        if (!daPhanLopHK2)
                        {
                            successHK2 = phanLopBLL.AddPhanLop(maHS, lopDuocPhan.maLop, hk2.MaHocKy);
                        }
                        else
                        {
                            successHK2 = true; // Đã có rồi, coi như thành công
                        }
                        
                        if (successHK1 && successHK2)
                        {
                            successCount++;
                            // ✅ Lưu thông tin lớp đã phân
                            bool laNguyenVong = !string.IsNullOrWhiteSpace(nguyenVong) && 
                                                 lopDuocPhan.tenLop.Equals(nguyenVong, StringComparison.OrdinalIgnoreCase);
                            lopDaPhan[maHS] = (lopDuocPhan.tenLop, nguyenVong, laNguyenVong);
                        }
                        else
                        {
                            string chiTiet = "";
                            if (!successHK1) chiTiet += "HK1 thất bại. ";
                            if (!successHK2) chiTiet += "HK2 thất bại.";
                            errors.AppendLine($"Học sinh {tenHS}: Không thể phân lớp vào {lopDuocPhan.tenLop} ({chiTiet.Trim()})");
                            errorCount++;
                            hocSinhKhongPhanLopDuoc.Add(maHS); // ✅ Đánh dấu cần rollback
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine($"Học sinh {tenHS}: Lỗi khi phân lớp - {ex.Message}");
                        errorCount++;
                        hocSinhKhongPhanLopDuoc.Add(maHS); // ✅ Đánh dấu cần rollback
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Học sinh {tenHS}: {ex.Message}");
                    errorCount++;
                    hocSinhKhongPhanLopDuoc.Add(maHS); // ✅ Đánh dấu cần rollback
                }
            }

            // ✅ ROLLBACK các học sinh không phân lớp được
            // Xóa học sinh, mối quan hệ, điểm, hạnh kiểm, xếp loại, phụ huynh (nếu chỉ liên quan đến học sinh này)
            foreach (int maHS in hocSinhKhongPhanLopDuoc)
            {
                try
                {
                    // ✅ XÓA THEO THỨ TỰ: Điểm → Hạnh kiểm → Xếp loại → Mối quan hệ → Học sinh → Tài khoản
                    
                    // 1. Xóa điểm số (tất cả học kỳ của học sinh này)
                    try 
                    { 
                        using (var conn = DAO.ConnectDatabase.ConnectionDatabase.GetConnection())
                        {
                            conn.Open();
                            using (var cmd = new MySqlCommand("DELETE FROM DiemSo WHERE MaHocSinh = @maHS", conn))
                            {
                                cmd.Parameters.AddWithValue("@maHS", maHS);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    } 
                    catch (Exception exDelDiem) 
                    { 
                        System.Diagnostics.Debug.WriteLine($"⚠️ Lỗi xóa điểm của học sinh {maHS}: {exDelDiem.Message}"); 
                    }
                    
                    // 2. Xóa hạnh kiểm (tất cả học kỳ của học sinh này)
                    try 
                    { 
                        using (var conn = DAO.ConnectDatabase.ConnectionDatabase.GetConnection())
                        {
                            conn.Open();
                            using (var cmd = new MySqlCommand("DELETE FROM HanhKiem WHERE MaHocSinh = @maHS", conn))
                            {
                                cmd.Parameters.AddWithValue("@maHS", maHS);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    } 
                    catch (Exception exDelHK) 
                    { 
                        System.Diagnostics.Debug.WriteLine($"⚠️ Lỗi xóa hạnh kiểm của học sinh {maHS}: {exDelHK.Message}"); 
                    }
                    
                    // 3. Xóa xếp loại (tất cả học kỳ của học sinh này)
                    try 
                    { 
                        using (var conn = DAO.ConnectDatabase.ConnectionDatabase.GetConnection())
                        {
                            conn.Open();
                            using (var cmd = new MySqlCommand("DELETE FROM XepLoai WHERE MaHocSinh = @maHS", conn))
                            {
                                cmd.Parameters.AddWithValue("@maHS", maHS);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    } 
                    catch (Exception exDelXL) 
                    { 
                        System.Diagnostics.Debug.WriteLine($"⚠️ Lỗi xóa xếp loại của học sinh {maHS}: {exDelXL.Message}"); 
                    }
                    
                    // 4. Xóa phân lớp (nếu có)
                    try 
                    { 
                        using (var conn = DAO.ConnectDatabase.ConnectionDatabase.GetConnection())
                        {
                            conn.Open();
                            using (var cmd = new MySqlCommand("DELETE FROM PhanLop WHERE MaHocSinh = @maHS", conn))
                            {
                                cmd.Parameters.AddWithValue("@maHS", maHS);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    } 
                    catch (Exception exDelPL) 
                    { 
                        System.Diagnostics.Debug.WriteLine($"⚠️ Lỗi xóa phân lớp của học sinh {maHS}: {exDelPL.Message}"); 
                    }
                    
                    // 5. Xóa mối quan hệ phụ huynh
                    try { hocSinhPhuHuynhBLL.DeleteQuanHeByHocSinh(maHS); } 
                    catch (Exception exDelQH) 
                    { 
                        System.Diagnostics.Debug.WriteLine($"⚠️ Lỗi xóa mối quan hệ của học sinh {maHS}: {exDelQH.Message}"); 
                    }
                    
                    // 6. Xóa học sinh
                    bool deleteSuccess = false;
                    try 
                    { 
                        deleteSuccess = hocSinhBus.DeleteHocSinh(maHS);
                        if (!deleteSuccess)
                        {
                            errors.AppendLine($"⚠️ Không thể xóa học sinh (Mã HS: {maHS}) - Có thể học sinh không tồn tại hoặc đã bị xóa trước đó.");
                        }
                    } 
                    catch (Exception exDelHS) 
                    { 
                        errors.AppendLine($"Lỗi khi xóa học sinh (Mã HS: {maHS}): {exDelHS.Message}");
                    }
                    
                    // 7. Xóa tài khoản người dùng (nếu học sinh đã bị xóa thành công)
                    if (deleteSuccess)
                    {
                        string username = $"HS{maHS:D3}";
                        try { nguoiDungBLL.DeleteNguoiDung(username); } 
                        catch (Exception exDelTK) 
                        { 
                            // Không cần báo lỗi nếu tài khoản không tồn tại
                            System.Diagnostics.Debug.WriteLine($"⚠️ Lỗi xóa tài khoản {username}: {exDelTK.Message}"); 
                        }
                    }
                    
                    // ✅ Xóa khỏi dictionary để không hiển thị trong kết quả thành công
                    var keyToRemove = hocSinhThanhCong.FirstOrDefault(kvp => kvp.Value.maHS == maHS);
                    if (!string.IsNullOrEmpty(keyToRemove.Key))
                    {
                        hocSinhThanhCong.Remove(keyToRemove.Key);
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Lỗi khi rollback học sinh (Mã HS: {maHS}): {ex.Message}");
                }
            }
            
            // ✅ Nếu có học sinh bị rollback, thêm thông báo vào errors
            if (hocSinhKhongPhanLopDuoc.Count > 0)
            {
                errors.AppendLine();
                errors.AppendLine($"⚠️ LƯU Ý: {hocSinhKhongPhanLopDuoc.Count} học sinh không phân lớp được đã bị xóa khỏi hệ thống (không có lớp phù hợp).");
            }

            // ✅ Hiển thị kết quả phân lớp chi tiết
            StringBuilder result = new StringBuilder();
            result.AppendLine("╔════════════════════════════════════════════════╗");
            result.AppendLine("║      KẾT QUẢ PHÂN LỚP CHUYỂN TRƯỜNG            ║");
            result.AppendLine("╚════════════════════════════════════════════════╝");
            result.AppendLine();
            
            result.AppendLine($"📊 TỔNG KẾT:");
            result.AppendLine($"   ✓ Thành công: {successCount} học sinh");
            if (errorCount > 0)
                result.AppendLine($"   ✗ Lỗi: {errorCount} học sinh");
            if (hocSinhKhongPhanLopDuoc.Count > 0)
                result.AppendLine($"   ⚠️ Đã xóa: {hocSinhKhongPhanLopDuoc.Count} học sinh (không phân lớp được)");
            result.AppendLine();
            
            // ✅ Danh sách học sinh được phân lớp thành công (với lớp được phân)
            if (successCount > 0)
            {
                result.AppendLine("✅ DANH SÁCH HỌC SINH ĐÃ ĐƯỢC PHÂN LỚP:");
                foreach (var kvp in lopDaPhan)
                {
                    int maHS = kvp.Key;
                    var lopInfo = kvp.Value;
                    
                    // Tìm thông tin học sinh
                    var hsInfo = hocSinhThanhCong.FirstOrDefault(h => h.Value.maHS == maHS);
                    if (hsInfo.Key != null)
                    {
                        string tenHS = hsInfo.Key;
                        string khoi = hsInfo.Value.khoi;
                        
                        result.AppendLine($"   • {tenHS} (Khối {khoi}, Mã HS: {maHS})");
                        result.AppendLine($"     → Lớp được phân: {lopInfo.tenLop}");
                        if (!string.IsNullOrWhiteSpace(lopInfo.nguyenVong))
                        {
                            if (lopInfo.laNguyenVong)
                            {
                                result.AppendLine($"     → ✓ Đúng nguyện vọng: {lopInfo.nguyenVong}");
                            }
                            else
                            {
                                result.AppendLine($"     → ⚠️ Nguyện vọng: {lopInfo.nguyenVong} (không đủ chỗ, đã phân lớp khác)");
                            }
                        }
                        result.AppendLine();
                    }
                }
            }
            
            if (warnings.Length > 0)
            {
                result.AppendLine("⚠️ CẢNH BÁO:");
                result.Append(warnings);
                result.AppendLine();
            }
            
            if (errors.Length > 0)
            {
                result.AppendLine("❌ CHI TIẾT LỖI:");
                result.Append(errors);
            }

            if (errorCount > 0 || warnings.Length > 0)
            {
                ScrollableMessageBox.Show("Kết quả phân lớp", result.ToString(), MessageBoxIcon.Warning);
            }
            else if (successCount > 0)
            {
                ScrollableMessageBox.Show("Kết quả phân lớp", result.ToString(), MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Hàm nhập Excel cho học sinh tuyển sinh (lớp 9 vào lớp 10 chưa được phân lớp)
        /// Chỉ nhập HocSinh, PhuHuynh, MoiQuanHe (không cần điểm, hạnh kiểm, xếp loại)
        /// ✅ Đảm bảo transaction: Tất cả hoặc không có gì - Rollback toàn bộ nếu có bất kỳ lỗi nào
        /// </summary>
        private void ImportExcelTuyenSinh(string filePath, HocKyDTO hocKyDeNhap, HocKyDTO hk1, HocKyDTO hk2)
        {
            // ✅ Set LicenseContext cho EPPlus (bắt buộc từ phiên bản 5.0+)
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            
            // ✅ Danh sách học sinh đã thêm để rollback nếu cần
            List<int> hocSinhDaThem = new List<int>();
            List<int> phuHuynhDaThem = new List<int>();
            
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    // Kiểm tra xem file có ít nhất 3 worksheet không
                    if (package.Workbook.Worksheets.Count < 3)
                    {
                        throw new Exception("File Excel phải có ít nhất 3 worksheet: HocSinh, PhuHuynh, MoiQuanHe");
                    }

                    // Đọc từng worksheet
                    var wsHocSinh = package.Workbook.Worksheets["HocSinh"] ?? package.Workbook.Worksheets[0];
                    var wsPhuHuynh = package.Workbook.Worksheets["PhuHuynh"] ?? package.Workbook.Worksheets[1];
                    var wsMoiQuanHe = package.Workbook.Worksheets["MoiQuanHe"] ?? package.Workbook.Worksheets[2];

                    // 1. Nhập Học Sinh với trạng thái "Đang học"
                    Dictionary<string, (int maHS, int excelRow)> hocSinhThanhCong = 
                        ImportHocSinhFromWorksheetTuyenSinh(wsHocSinh, hocSinhDaThem);

                    if (hocSinhThanhCong.Count == 0)
                    {
                        MessageBox.Show("Không có học sinh nào được nhập thành công. Vui lòng kiểm tra lại dữ liệu Excel.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 2. Nhập Phụ Huynh của học sinh đã nhập thành công
                    // ✅ Nếu có lỗi, sẽ rollback học sinh
                    Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong = 
                        ImportPhuHuynhFromWorksheetTuyenSinh(wsPhuHuynh, hocSinhThanhCong, hocSinhDaThem, phuHuynhDaThem);

                    // ✅ Kiểm tra: Nếu sau khi nhập phụ huynh, không còn học sinh nào thì rollback
                    if (hocSinhThanhCong.Count == 0)
                    {
                        RollbackTuyenSinh(hocSinhDaThem, phuHuynhDaThem);
                        MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập phụ huynh. Đã rollback toàn bộ dữ liệu.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. Nhập Mối Quan Hệ của học sinh đã nhập thành công
                    // ✅ Nếu có lỗi, sẽ rollback học sinh và phụ huynh
                    ImportMoiQuanHeFromWorksheetTuyenSinh(wsMoiQuanHe, hocSinhThanhCong, phuHuynhThanhCong, hocSinhDaThem, phuHuynhDaThem);

                    // ✅ Kiểm tra: Nếu sau khi nhập mối quan hệ, không còn học sinh nào thì rollback
                    if (hocSinhThanhCong.Count == 0)
                    {
                        RollbackTuyenSinh(hocSinhDaThem, phuHuynhDaThem);
                        MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập mối quan hệ. Đã rollback toàn bộ dữ liệu.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ✅ THÀNH CÔNG: Tất cả dữ liệu đã được thêm
                    MessageBox.Show($"✅ Nhập Excel thành công!\n\n" +
                                   $"Đã nhập {hocSinhThanhCong.Count} học sinh tuyển sinh.\n" +
                                   $"Đã nhập {phuHuynhThanhCong.Count} phụ huynh.\n" +
                                   $"Đã tạo {hocSinhThanhCong.Count} mối quan hệ.\n\n" +
                                   $"Các học sinh này sẽ được hiển thị trong bảng phân lớp và sẽ được phân vào lớp 10 khi thực hiện 'Phân lớp tự động'.",
                                   "Nhập Excel thành công",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // ✅ ROLLBACK TOÀN BỘ nếu có bất kỳ lỗi nào
                RollbackTuyenSinh(hocSinhDaThem, phuHuynhDaThem);
                throw new Exception($"Lỗi khi nhập Excel: {ex.Message}\n\nĐã rollback toàn bộ dữ liệu.", ex);
            }
        }

        /// <summary>
        /// Rollback toàn bộ dữ liệu đã thêm (học sinh, phụ huynh, mối quan hệ, tài khoản)
        /// </summary>
        private void RollbackTuyenSinh(List<int> hocSinhDaThem, List<int> phuHuynhDaThem)
        {
            // Xóa mối quan hệ trước
            foreach (int maHS in hocSinhDaThem)
            {
                try
                {
                    hocSinhPhuHuynhBLL.DeleteQuanHeByHocSinh(maHS);
                }
                catch { }
            }

            // Xóa phụ huynh
            foreach (int maPH in phuHuynhDaThem)
            {
                try
                {
                    phuHuynhBLL.DeletePhuHuynh(maPH);
                }
                catch { }
            }

            // Xóa học sinh và tài khoản
            foreach (int maHS in hocSinhDaThem)
            {
                try
                {
                    // Xóa tài khoản
                    string tenDangNhap = "HS" + maHS;
                    nguoiDungBLL.DeleteNguoiDung(tenDangNhap);
                    
                    // Xóa học sinh
                    hocSinhBus.DeleteHocSinh(maHS);
                }
                catch { }
            }
        }

        /// <summary>
        /// Nhập học sinh từ worksheet Excel (cho tuyển sinh)
        /// </summary>
        private Dictionary<string, (int maHS, int excelRow)> ImportHocSinhFromWorksheetTuyenSinh(ExcelWorksheet ws, List<int> hocSinhDaThem)
        {
            var result = new Dictionary<string, (int, int)>();
            int rowCount = ws.Dimension?.End.Row ?? 0;
            if (rowCount < 2) return result;

            // ✅ Tìm các cột (tự động phát hiện) - BỎ QUA cột "Mã HS" (auto-generated)
            int colHoTen = -1, colNgaySinh = -1, colGioiTinh = -1, colSdt = -1, colEmail = -1, colTrangThai = -1;
            
            for (int col = 1; col <= ws.Dimension.End.Column; col++)
            {
                string header = ws.Cells[1, col].Text.Trim().ToLower();
                // ✅ Bỏ qua cột "Mã HS" (auto-generated)
                if (header.Contains("mã hs") || header.Contains("mahs") || header.Contains("mã học sinh"))
                    continue;
                    
                if ((header.Contains("họ") && header.Contains("tên")) || header.Contains("hoten") || header == "họ và tên")
                    colHoTen = col;
                else if ((header.Contains("ngày") && header.Contains("sinh")) || header.Contains("ngaysinh"))
                    colNgaySinh = col;
                else if ((header.Contains("giới") && header.Contains("tính")) || header.Contains("gioitinh"))
                    colGioiTinh = col;
                else if (header.Contains("sđt") || header.Contains("sdt") || header.Contains("điện thoại") || header.Contains("sdths"))
                    colSdt = col;
                else if (header.Contains("email"))
                    colEmail = col;
                else if ((header.Contains("trạng") && header.Contains("thái")) || header.Contains("trangthai"))
                    colTrangThai = col;
            }

            // ✅ Fallback: Nếu không tìm thấy, tìm từ cột 2 trở đi (bỏ qua cột "Mã HS" ở đầu)
            if (colHoTen == -1)
            {
                // Tìm cột đầu tiên có dữ liệu (không phải "Mã HS")
                for (int col = 2; col <= Math.Min(ws.Dimension.End.Column, 7); col++)
                {
                    string header = ws.Cells[1, col].Text.Trim().ToLower();
                    if (!header.Contains("mã hs") && !header.Contains("mahs"))
                    {
                        colHoTen = col;
                        break;
                    }
                }
                if (colHoTen == -1) colHoTen = 2; // Mặc định cột 2 (bỏ qua cột 1 "Mã HS")
            }
            if (colNgaySinh == -1) colNgaySinh = colHoTen + 1;
            if (colGioiTinh == -1) colGioiTinh = colNgaySinh + 1;
            if (colSdt == -1) colSdt = colGioiTinh + 1;
            if (colEmail == -1) colEmail = colSdt + 1;
            if (colTrangThai == -1) colTrangThai = colEmail + 1;

            int successCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string hoTen = ws.Cells[row, colHoTen].Text.Trim();
                    string ngaySinhStr = ws.Cells[row, colNgaySinh].Text.Trim();
                    string gioiTinh = ws.Cells[row, colGioiTinh].Text.Trim();
                    string sdtHS = ws.Cells[row, colSdt].Text.Trim();
                    string email = ws.Cells[row, colEmail].Text.Trim();
                    string trangThai = ws.Cells[row, colTrangThai].Text.Trim();

                    // Bỏ qua dòng trống
                    if (string.IsNullOrWhiteSpace(hoTen) && string.IsNullOrWhiteSpace(ngaySinhStr))
                        continue;

                    // Validate dữ liệu
                    if (string.IsNullOrWhiteSpace(hoTen))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Thiếu họ tên");
                        errorCount++;
                        continue;
                    }

                    // Parse ngày sinh với nhiều format (ưu tiên format Việt Nam)
                    DateTime? ngaySinh = null;
                    bool parsedDate = false;
                    if (!string.IsNullOrWhiteSpace(ngaySinhStr))
                    {
                        // ✅ Ưu tiên format Việt Nam trước (dd/MM/yyyy)
                        string[] dateFormats = {
                            "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy",
                            "dd/MM/yy", "d/M/yy", "yyyy-MM-dd", "MM/dd/yyyy" // Format Mỹ (fallback)
                        };
                        
                        foreach (string format in dateFormats)
                        {
                            if (DateTime.TryParseExact(ngaySinhStr, format,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out DateTime parsed))
                            {
                                ngaySinh = parsed;
                                parsedDate = true;
                                break;
                            }
                        }
                        
                        // Nếu vẫn chưa parse được, thử parse tự động
                        if (!parsedDate && DateTime.TryParse(ngaySinhStr, out DateTime autoParsed))
                        {
                            ngaySinh = autoParsed;
                            parsedDate = true;
                        }
                    }

                    // Nếu không parse được ngày sinh, báo lỗi
                    if (!parsedDate || !ngaySinh.HasValue)
                    {
                        errors.AppendLine($"Dòng {row - 1}: {hoTen} - Ngày sinh không hợp lệ ({ngaySinhStr}). Vui lòng kiểm tra lại format (dd/MM/yyyy).");
                        errorCount++;
                        continue;
                    }

                    // ✅ Validate tuổi cho học sinh tuyển sinh (cho phép từ 15 tuổi trở lên)
                    int currentYear = DateTime.Now.Year;
                    int birthYear = ngaySinh.Value.Year;
                    int age = currentYear - birthYear;
                    
                    // Điều chỉnh tuổi nếu chưa qua sinh nhật trong năm hiện tại
                    if (ngaySinh.Value.Date > DateTime.Today.AddYears(-age))
                    {
                        age--;
                    }
                    
                    // Học sinh tuyển sinh lớp 10: cho phép từ 15 tuổi trở lên (thay vì 16)
                    if (age < 15)
                    {
                        errors.AppendLine($"Dòng {row - 1}: {hoTen} - Học sinh phải từ 15 tuổi trở lên (Hiện tại: {age} tuổi, Ngày sinh: {ngaySinh.Value:dd/MM/yyyy}).");
                        errorCount++;
                        continue;
                    }

                    // Kiểm tra ngày sinh không được là tương lai
                    if (ngaySinh.Value.Date > DateTime.Today)
                    {
                        errors.AppendLine($"Dòng {row - 1}: {hoTen} - Ngày sinh không được là ngày trong tương lai ({ngaySinh.Value:dd/MM/yyyy}).");
                        errorCount++;
                        continue;
                    }

                    // Mặc định trạng thái là "Đang học" nếu không có
                    if (string.IsNullOrWhiteSpace(trangThai))
                        trangThai = "Đang học";

                    // ✅ Chỉ nhập học sinh có trạng thái "Đang học" - BÁO LỖI nếu không phải
                    if (trangThai != "Đang học")
                    {
                        errors.AppendLine($"Dòng {row - 1}: {hoTen} - Trạng thái '{trangThai}' không hợp lệ. Chỉ chấp nhận học sinh có trạng thái 'Đang học'.");
                        errorCount++;
                        continue;
                    }

                    // Tạo học sinh mới
                    var hocSinh = new HocSinhDTO
                    {
                        HoTen = hoTen,
                        NgaySinh = ngaySinh.Value, // Đã validate ở trên
                        GioiTinh = gioiTinh,
                        SdtHS = sdtHS,
                        Email = email,
                        TrangThai = trangThai,
                        TenDangNhap = null // Học sinh tuyển sinh chưa có tài khoản
                    };

                    // ✅ Thêm học sinh tuyển sinh (cho phép từ 15 tuổi, bỏ qua validation tuổi 16)
                    try
                    {
                        int maHS = AddHocSinhTuyenSinh(hocSinh);
                        if (maHS > 0)
                        {
                            result[hoTen] = (maHS, row);
                            hocSinhDaThem.Add(maHS); // ✅ Lưu để rollback nếu cần
                            successCount++;
                        }
                        else
                        {
                            errors.AppendLine($"Dòng {row - 1}: {hoTen} - Không thể thêm học sinh");
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine($"Dòng {row - 1}: {hoTen} - Lỗi: {ex.Message}");
                        errorCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: Lỗi - {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ Hiển thị kết quả nhập Học Sinh (riêng biệt)
            if (successCount > 0 || errorCount > 0)
            {
                StringBuilder resultMsg = new StringBuilder();
                resultMsg.AppendLine("📋 KẾT QUẢ NHẬP HỌC SINH (Tuyển sinh):");
                resultMsg.AppendLine();
                if (successCount > 0)
                    resultMsg.AppendLine($"✅ Thành công: {successCount} học sinh");
                if (errorCount > 0)
                {
                    resultMsg.AppendLine($"❌ Lỗi: {errorCount} học sinh");
                    resultMsg.AppendLine();
                    resultMsg.AppendLine("Chi tiết lỗi:");
                    resultMsg.Append(errors);
                }
                
                if (resultMsg.Length > 1000)
                    ScrollableMessageBox.Show("Kết quả nhập Học Sinh", resultMsg.ToString(), 
                        errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                else
                    MessageBox.Show(resultMsg.ToString(), "Kết quả nhập Học Sinh", 
                        MessageBoxButtons.OK, 
                        errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }

            return result;
        }

        /// <summary>
        /// Thêm học sinh tuyển sinh (bỏ qua validation tuổi 16, cho phép từ 15 tuổi)
        /// </summary>
        private int AddHocSinhTuyenSinh(HocSinhDTO hocSinh)
        {
            try
            {
                // Validate các trường khác (trừ tuổi)
                if (string.IsNullOrWhiteSpace(hocSinh.HoTen))
                    throw new ArgumentException("Họ và tên không được để trống.");
                
                if (string.IsNullOrWhiteSpace(hocSinh.Email))
                    throw new ArgumentException("Email không được để trống.");
                
                if (string.IsNullOrWhiteSpace(hocSinh.GioiTinh) || (hocSinh.GioiTinh != "Nam" && hocSinh.GioiTinh != "Nữ"))
                    throw new ArgumentException("Giới tính không hợp lệ.");
                
                // Kiểm tra trùng lặp
                var hocSinhDAO = new HocSinhDAO();
                if (!string.IsNullOrWhiteSpace(hocSinh.SdtHS) && hocSinhDAO.KiemTraTrungSdt(hocSinh.SdtHS))
                    throw new ArgumentException($"Số điện thoại '{hocSinh.SdtHS}' đã tồn tại.");
                
                if (!string.IsNullOrWhiteSpace(hocSinh.Email) && hocSinhDAO.KiemTraTrungEmail(hocSinh.Email))
                    throw new ArgumentException($"Email '{hocSinh.Email}' đã tồn tại.");
                
                // Thêm học sinh vào CSDL (bỏ qua validation tuổi)
                int newMaHocSinh = hocSinhDAO.ThemHocSinh(hocSinh);
                if (newMaHocSinh <= 0)
                    throw new Exception("Thêm học sinh thất bại.");
                
                // Tạo tài khoản đăng nhập
                string tenDangNhap = "HS" + newMaHocSinh;
                string matKhauMacDinh = "123456";
                var loginBUS = new LoginBUS();
                bool taoTaiKhoanThanhCong = loginBUS.ThemNguoiDung(tenDangNhap, matKhauMacDinh, "Hoạt động");
                
                if (!taoTaiKhoanThanhCong)
                {
                    // Rollback: Xóa học sinh
                    hocSinhDAO.XoaHocSinh(newMaHocSinh);
                    throw new Exception("Tạo tài khoản đăng nhập thất bại. Đã rollback.");
                }
                
                // Cập nhật TenDangNhap cho học sinh
                bool capNhatThanhCong = hocSinhDAO.CapNhatTenDangNhap(newMaHocSinh, tenDangNhap);
                if (!capNhatThanhCong)
                {
                    // Rollback: Xóa học sinh và tài khoản
                    hocSinhDAO.XoaHocSinh(newMaHocSinh);
                    throw new Exception("Cập nhật tên đăng nhập cho học sinh thất bại. Đã rollback.");
                }
                
                // Gán vai trò student (INSERT vào NguoiDungVaiTro)
                using (var conn = DAO.ConnectDatabase.ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    string queryVaiTro = @"INSERT IGNORE INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) 
                                          VALUES (@TenDangNhap, @MaVaiTro)";
                    using (var cmd = new MySqlCommand(queryVaiTro, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MaVaiTro", "student");
                        cmd.ExecuteNonQuery();
                    }
                }
                
                return newMaHocSinh;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi AddHocSinhTuyenSinh: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Nhập phụ huynh từ worksheet Excel (cho tuyển sinh)
        /// ✅ Nếu có lỗi, sẽ rollback học sinh tương ứng
        /// ✅ Tìm học sinh theo tên (không phải theo dòng Excel) vì dòng có thể không tương ứng
        /// </summary>
        private Dictionary<string, (int maPH, int excelRow)> ImportPhuHuynhFromWorksheetTuyenSinh(
            ExcelWorksheet ws, 
            Dictionary<string, (int maHS, int excelRow)> hocSinhThanhCong,
            List<int> hocSinhDaThem,
            List<int> phuHuynhDaThem)
        {
            var result = new Dictionary<string, (int, int)>();
            int rowCount = ws.Dimension?.End.Row ?? 0;
            if (rowCount < 2) return result;

            // ✅ Tìm các cột - BỎ QUA cột "Mã PH" (auto-generated)
            int colHoTen = -1, colSdt = -1, colEmail = -1, colDiaChi = -1;
            
            for (int col = 1; col <= ws.Dimension.End.Column; col++)
            {
                string header = ws.Cells[1, col].Text.Trim().ToLower();
                // ✅ Bỏ qua cột "Mã PH" (auto-generated)
                if (header.Contains("mã ph") || header.Contains("maph") || header.Contains("mã phụ huynh"))
                    continue;
                    
                if ((header.Contains("họ") && header.Contains("tên")) || header.Contains("hoten") || header == "họ và tên")
                    colHoTen = col;
                else if (header.Contains("sđt") || header.Contains("sdt") || header.Contains("điện thoại"))
                    colSdt = col;
                else if (header.Contains("email"))
                    colEmail = col;
                else if ((header.Contains("địa") && header.Contains("chỉ")) || header.Contains("diachi"))
                    colDiaChi = col;
            }

            // ✅ Fallback: Nếu không tìm thấy, tìm từ cột 2 trở đi (bỏ qua cột "Mã PH" ở đầu)
            if (colHoTen == -1)
            {
                // Tìm cột đầu tiên có dữ liệu (không phải "Mã PH")
                for (int col = 2; col <= Math.Min(ws.Dimension.End.Column, 5); col++)
                {
                    string header = ws.Cells[1, col].Text.Trim().ToLower();
                    if (!header.Contains("mã ph") && !header.Contains("maph"))
                    {
                        colHoTen = col;
                        break;
                    }
                }
                if (colHoTen == -1) colHoTen = 2; // Mặc định cột 2 (bỏ qua cột 1 "Mã PH")
            }
            if (colSdt == -1) colSdt = colHoTen + 1;
            if (colEmail == -1) colEmail = colSdt + 1;
            if (colDiaChi == -1) colDiaChi = colEmail + 1;

            HashSet<string> emailDaNhap = new HashSet<string>();
            HashSet<string> sdtDaNhap = new HashSet<string>();
            List<int> hocSinhCanRollback = new List<int>(); // ✅ Danh sách học sinh cần rollback
            int successCount = 0;
            int errorCount = 0;
            int skippedCount = 0;
            var errors = new StringBuilder();
            var skipped = new StringBuilder();

            // ✅ Tạo map: tên học sinh -> mã học sinh (để tìm nhanh)
            Dictionary<string, int> mapTenHSToMaHS = new Dictionary<string, int>();
            foreach (var kvp in hocSinhThanhCong)
            {
                if (!mapTenHSToMaHS.ContainsKey(kvp.Key))
                {
                    mapTenHSToMaHS[kvp.Key] = kvp.Value.maHS;
                }
            }

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string hoTen = ws.Cells[row, colHoTen].Text.Trim();
                    string sdt = ws.Cells[row, colSdt].Text.Trim();
                    string email = ws.Cells[row, colEmail].Text.Trim();
                    string diaChi = ws.Cells[row, colDiaChi].Text.Trim();

                    if (string.IsNullOrWhiteSpace(hoTen) && string.IsNullOrWhiteSpace(sdt))
                        continue;

                    // ✅ Tìm học sinh tương ứng: Ưu tiên theo dòng Excel, sau đó theo tên
                    // ✅ Sử dụng mapTenHSToMaHS để tránh lặp lại
                    int maHSTuongUng = 0;
                    string tenHSTuongUng = "";
                    
                    // 1. Ưu tiên: Tìm theo dòng Excel
                    foreach (var kvp in hocSinhThanhCong)
                    {
                        if (kvp.Value.excelRow == row)
                        {
                            maHSTuongUng = kvp.Value.maHS;
                            tenHSTuongUng = kvp.Key;
                            break;
                        }
                    }
                    
                    // 2. Nếu không tìm thấy theo dòng, tìm theo tên (so khớp chính xác trước, sau đó một phần)
                    if (maHSTuongUng == 0)
                    {
                        // Ưu tiên: Tìm chính xác theo tên
                        if (mapTenHSToMaHS.ContainsKey(hoTen))
                        {
                            maHSTuongUng = mapTenHSToMaHS[hoTen];
                            tenHSTuongUng = hoTen;
                        }
                        else
                        {
                            // Tìm học sinh có tên gần giống nhất (so khớp một phần)
                            foreach (var kvp in hocSinhThanhCong)
                            {
                                string tenHS = kvp.Key;
                                // So khớp một phần (ví dụ: "Nguyễn Tuấn" trong "Nguyễn Tuấn Tà")
                                if (tenHS.IndexOf(hoTen, StringComparison.OrdinalIgnoreCase) >= 0 || 
                                    hoTen.IndexOf(tenHS, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    maHSTuongUng = kvp.Value.maHS;
                                    tenHSTuongUng = tenHS;
                                    break;
                                }
                            }
                        }
                    }

                    if (maHSTuongUng == 0)
                    {
                        skipped.AppendLine($"Dòng {row - 1}: {hoTen} - Bỏ qua (Không tìm thấy học sinh tương ứng)");
                        skippedCount++;
                        continue;
                    }
                    
                    // ✅ Kiểm tra xem học sinh này đã bị rollback chưa (tránh lặp lại lỗi)
                    if (hocSinhCanRollback.Contains(maHSTuongUng))
                    {
                        continue; // Đã bị rollback rồi, bỏ qua
                    }

                    if (string.IsNullOrWhiteSpace(hoTen))
                    {
                        // ✅ Nếu thiếu tên phụ huynh nhưng có học sinh → rollback học sinh
                        errors.AppendLine($"Dòng {row - 1}: Thiếu họ tên phụ huynh (Học sinh: {tenHSTuongUng})");
                        errorCount++;
                        if (!hocSinhCanRollback.Contains(maHSTuongUng))
                        {
                            hocSinhCanRollback.Add(maHSTuongUng);
                        }
                        continue;
                    }

                    // ✅ Kiểm tra trùng SDT trong cùng file Excel (kiểm tra trước)
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        if (sdtDaNhap.Contains(sdt))
                        {
                            errors.AppendLine($"Dòng {row - 1}: {hoTen} - Số điện thoại '{sdt}' đã được sử dụng ở dòng trước đó trong file Excel (Học sinh: {tenHSTuongUng})");
                            errorCount++;
                            if (!hocSinhCanRollback.Contains(maHSTuongUng))
                            {
                                hocSinhCanRollback.Add(maHSTuongUng);
                            }
                            continue;
                        }
                    }

                    // ✅ Kiểm tra trùng Email trong cùng file Excel (kiểm tra trước)
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        if (emailDaNhap.Contains(email.ToLower()))
                        {
                            errors.AppendLine($"Dòng {row - 1}: {hoTen} - Email '{email}' đã được sử dụng ở dòng trước đó trong file Excel (Học sinh: {tenHSTuongUng})");
                            errorCount++;
                            if (!hocSinhCanRollback.Contains(maHSTuongUng))
                            {
                                hocSinhCanRollback.Add(maHSTuongUng);
                            }
                            continue;
                        }
                    }

                    // ✅ Kiểm tra trùng SDT trong database
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        var phuHuynhDAO = new PhuHuynhDAO();
                        if (phuHuynhDAO.KiemTraTrungSdt(sdt))
                        {
                            errors.AppendLine($"Dòng {row - 1}: {hoTen} - Số điện thoại '{sdt}' đã tồn tại trong hệ thống (Học sinh: {tenHSTuongUng})");
                            errorCount++;
                            if (!hocSinhCanRollback.Contains(maHSTuongUng))
                            {
                                hocSinhCanRollback.Add(maHSTuongUng);
                            }
                            continue;
                        }
                    }

                    // ✅ Kiểm tra trùng Email trong database
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        var phuHuynhDAO = new PhuHuynhDAO();
                        if (phuHuynhDAO.KiemTraTrungEmail(email))
                        {
                            errors.AppendLine($"Dòng {row - 1}: {hoTen} - Email '{email}' đã tồn tại trong hệ thống (Học sinh: {tenHSTuongUng})");
                            errorCount++;
                            if (!hocSinhCanRollback.Contains(maHSTuongUng))
                            {
                                hocSinhCanRollback.Add(maHSTuongUng);
                            }
                            continue;
                        }
                    }

                    // Tạo phụ huynh mới
                    var phuHuynh = new PhuHuynhDTO
                    {
                        HoTen = hoTen,
                        SoDienThoai = sdt,
                        Email = email,
                        DiaChi = diaChi
                    };

                    try
                    {
                        bool addSuccess = phuHuynhBLL.AddPhuHuynh(phuHuynh);
                        if (addSuccess && !string.IsNullOrWhiteSpace(sdt))
                        {
                            // Lấy mã phụ huynh vừa thêm bằng số điện thoại
                            var phuHuynhVuaThem = phuHuynhBLL.GetPhuHuynhBySdt(sdt);
                            if (phuHuynhVuaThem != null)
                            {
                                result[hoTen] = (phuHuynhVuaThem.MaPhuHuynh, row);
                                phuHuynhDaThem.Add(phuHuynhVuaThem.MaPhuHuynh); // ✅ Lưu để rollback nếu cần
                                successCount++;
                                
                                if (!string.IsNullOrWhiteSpace(email))
                                    emailDaNhap.Add(email.ToLower());
                                if (!string.IsNullOrWhiteSpace(sdt))
                                    sdtDaNhap.Add(sdt);
                            }
                            else
                            {
                                errors.AppendLine($"Dòng {row - 1}: {hoTen} - Không thể lấy mã phụ huynh sau khi thêm (Học sinh: {tenHSTuongUng})");
                                errorCount++;
                                if (!hocSinhCanRollback.Contains(maHSTuongUng))
                                {
                                    hocSinhCanRollback.Add(maHSTuongUng);
                                }
                            }
                        }
                        else
                        {
                            // ✅ Thêm phụ huynh thất bại → rollback học sinh
                            errors.AppendLine($"Dòng {row - 1}: {hoTen} - Không thể thêm phụ huynh (Học sinh: {tenHSTuongUng})");
                            errorCount++;
                            if (!hocSinhCanRollback.Contains(maHSTuongUng))
                            {
                                hocSinhCanRollback.Add(maHSTuongUng);
                            }
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        // ✅ Lỗi validation (trùng SDT/Email) → rollback học sinh
                        errors.AppendLine($"Dòng {row - 1}: {hoTen} - {ex.Message} (Học sinh: {tenHSTuongUng})");
                        errorCount++;
                        if (!hocSinhCanRollback.Contains(maHSTuongUng))
                        {
                            hocSinhCanRollback.Add(maHSTuongUng);
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine($"Dòng {row - 1}: {hoTen} - Lỗi: {ex.Message} (Học sinh: {tenHSTuongUng})");
                        errorCount++;
                        if (!hocSinhCanRollback.Contains(maHSTuongUng))
                        {
                            hocSinhCanRollback.Add(maHSTuongUng);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: Lỗi đọc dữ liệu - {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ Hiển thị kết quả nhập Phụ Huynh (riêng biệt) TRƯỚC KHI rollback
            if (successCount > 0 || errorCount > 0 || skippedCount > 0 || hocSinhCanRollback.Count > 0)
            {
                StringBuilder resultMsg = new StringBuilder();
                resultMsg.AppendLine("👨‍👩‍👧 KẾT QUẢ NHẬP PHỤ HUYNH (Tuyển sinh):");
                resultMsg.AppendLine();
                if (successCount > 0)
                    resultMsg.AppendLine($"✅ Thành công: {successCount} phụ huynh");
                if (skippedCount > 0)
                    resultMsg.AppendLine($"⚠️ Bỏ qua: {skippedCount} phụ huynh");
                if (errorCount > 0)
                    resultMsg.AppendLine($"❌ Lỗi: {errorCount} phụ huynh");
                if (hocSinhCanRollback.Count > 0)
                    resultMsg.AppendLine($"⚠️ Sẽ rollback {hocSinhCanRollback.Count} học sinh do phụ huynh lỗi");
                
                if (skipped.Length > 0)
                {
                    resultMsg.AppendLine();
                    resultMsg.AppendLine("Chi tiết bỏ qua:");
                    resultMsg.Append(skipped);
                }
                if (errors.Length > 0)
                {
                    resultMsg.AppendLine();
                    resultMsg.AppendLine("Chi tiết lỗi:");
                    resultMsg.Append(errors);
                }
                
                if (resultMsg.Length > 1000)
                    ScrollableMessageBox.Show("Kết quả nhập Phụ Huynh", resultMsg.ToString(), 
                        errorCount > 0 || hocSinhCanRollback.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                else
                    MessageBox.Show(resultMsg.ToString(), "Kết quả nhập Phụ Huynh", 
                        MessageBoxButtons.OK, 
                        errorCount > 0 || hocSinhCanRollback.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }

            // ✅ ROLLBACK học sinh nếu phụ huynh lỗi
            foreach (int maHS in hocSinhCanRollback)
            {
                try
                {
                    // Xóa tài khoản
                    string tenDangNhap = "HS" + maHS;
                    nguoiDungBLL.DeleteNguoiDung(tenDangNhap);
                    
                    // Xóa học sinh
                    hocSinhBus.DeleteHocSinh(maHS);
                    
                    // Xóa khỏi danh sách
                    var keyToRemove = hocSinhThanhCong.FirstOrDefault(kvp => kvp.Value.maHS == maHS);
                    if (!string.IsNullOrEmpty(keyToRemove.Key))
                    {
                        hocSinhThanhCong.Remove(keyToRemove.Key);
                    }
                    
                    // Xóa khỏi danh sách đã thêm
                    hocSinhDaThem.Remove(maHS);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi rollback học sinh {maHS}: {ex.Message}");
                }
            }

            return result;
        }

        /// <summary>
        /// Nhập mối quan hệ từ worksheet Excel (cho tuyển sinh)
        /// ✅ Nếu có lỗi, sẽ rollback học sinh và phụ huynh tương ứng
        /// </summary>
        private void ImportMoiQuanHeFromWorksheetTuyenSinh(
            ExcelWorksheet ws, 
            Dictionary<string, (int maHS, int excelRow)> hocSinhThanhCong,
            Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong,
            List<int> hocSinhDaThem,
            List<int> phuHuynhDaThem)
        {
            int rowCount = ws.Dimension?.End.Row ?? 0;
            if (rowCount < 2) return;

            List<int> hocSinhCanRollback = new List<int>(); // ✅ Danh sách học sinh cần rollback
            Dictionary<int, int> phuHuynhTheoHocSinh = new Dictionary<int, int>(); // ✅ Map học sinh -> phụ huynh để rollback
            int successCount = 0;
            int errorCount = 0;
            int skippedCount = 0;
            var errors = new StringBuilder();
            var skipped = new StringBuilder();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string tenHS = ws.Cells[row, 1].Text.Trim();
                    string tenPH = ws.Cells[row, 2].Text.Trim();
                    string moiQuanHe = ws.Cells[row, 3].Text.Trim();

                    if (string.IsNullOrWhiteSpace(tenHS) || string.IsNullOrWhiteSpace(tenPH))
                        continue;

                    // Tìm học sinh và phụ huynh tương ứng
                    int maHS = 0, maPH = 0;

                    // Tìm học sinh theo tên (ưu tiên theo dòng Excel)
                    foreach (var kvp in hocSinhThanhCong)
                    {
                        if (kvp.Value.excelRow == row && kvp.Key.Equals(tenHS, StringComparison.OrdinalIgnoreCase))
                        {
                            maHS = kvp.Value.maHS;
                            break;
                        }
                    }
                    if (maHS == 0)
                    {
                        if (hocSinhThanhCong.ContainsKey(tenHS))
                            maHS = hocSinhThanhCong[tenHS].maHS;
                    }

                    // Tìm phụ huynh theo tên (ưu tiên theo dòng Excel)
                    foreach (var kvp in phuHuynhThanhCong)
                    {
                        if (kvp.Value.excelRow == row && kvp.Key.Equals(tenPH, StringComparison.OrdinalIgnoreCase))
                        {
                            maPH = kvp.Value.maPH;
                            break;
                        }
                    }
                    if (maPH == 0)
                    {
                        if (phuHuynhThanhCong.ContainsKey(tenPH))
                            maPH = phuHuynhThanhCong[tenPH].maPH;
                    }

                    if (maHS > 0 && maPH > 0)
                    {
                        try
                        {
                            // Thêm mối quan hệ
                            hocSinhPhuHuynhBLL.AddQuanHe(maHS, maPH, moiQuanHe);
                            phuHuynhTheoHocSinh[maHS] = maPH; // ✅ Lưu mapping để rollback
                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            // ✅ Lỗi khi thêm mối quan hệ → rollback học sinh và phụ huynh
                            errors.AppendLine($"Dòng {row - 1}: {tenHS} - {tenPH} - Lỗi: {ex.Message}");
                            errorCount++;
                            if (!hocSinhCanRollback.Contains(maHS))
                            {
                                hocSinhCanRollback.Add(maHS);
                            }
                        }
                    }
                    else
                    {
                        // ✅ Không tìm thấy học sinh hoặc phụ huynh
                        if (maHS == 0)
                        {
                            errors.AppendLine($"Dòng {row - 1}: Không tìm thấy học sinh '{tenHS}'");
                            errorCount++;
                        }
                        if (maPH == 0)
                        {
                            errors.AppendLine($"Dòng {row - 1}: Không tìm thấy phụ huynh '{tenPH}' (Học sinh: {tenHS})");
                            errorCount++;
                            if (maHS > 0 && !hocSinhCanRollback.Contains(maHS))
                            {
                                hocSinhCanRollback.Add(maHS);
                            }
                        }
                        skippedCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: Lỗi đọc dữ liệu - {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ Hiển thị kết quả nhập Mối Quan Hệ (riêng biệt) TRƯỚC KHI rollback
            if (successCount > 0 || errorCount > 0 || skippedCount > 0 || hocSinhCanRollback.Count > 0)
            {
                StringBuilder resultMsg = new StringBuilder();
                resultMsg.AppendLine("🔗 KẾT QUẢ NHẬP MỐI QUAN HỆ (Tuyển sinh):");
                resultMsg.AppendLine();
                if (successCount > 0)
                    resultMsg.AppendLine($"✅ Thành công: {successCount} mối quan hệ");
                if (skippedCount > 0)
                    resultMsg.AppendLine($"⚠️ Bỏ qua: {skippedCount} mối quan hệ");
                if (errorCount > 0)
                    resultMsg.AppendLine($"❌ Lỗi: {errorCount} mối quan hệ");
                if (hocSinhCanRollback.Count > 0)
                    resultMsg.AppendLine($"⚠️ Sẽ rollback {hocSinhCanRollback.Count} học sinh do mối quan hệ lỗi");
                
                if (skipped.Length > 0)
                {
                    resultMsg.AppendLine();
                    resultMsg.AppendLine("Chi tiết bỏ qua:");
                    resultMsg.Append(skipped);
                }
                if (errors.Length > 0)
                {
                    resultMsg.AppendLine();
                    resultMsg.AppendLine("Chi tiết lỗi:");
                    resultMsg.Append(errors);
                }
                
                if (resultMsg.Length > 1000)
                    ScrollableMessageBox.Show("Kết quả nhập Mối Quan Hệ", resultMsg.ToString(), 
                        errorCount > 0 || hocSinhCanRollback.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                else
                    MessageBox.Show(resultMsg.ToString(), "Kết quả nhập Mối Quan Hệ", 
                        MessageBoxButtons.OK, 
                        errorCount > 0 || hocSinhCanRollback.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }

            // ✅ ROLLBACK học sinh và phụ huynh nếu mối quan hệ lỗi
            foreach (int maHS in hocSinhCanRollback)
            {
                try
                {
                    // Xóa mối quan hệ (nếu có)
                    try { hocSinhPhuHuynhBLL.DeleteQuanHeByHocSinh(maHS); } catch { }
                    
                    // Xóa phụ huynh (nếu có)
                    if (phuHuynhTheoHocSinh.ContainsKey(maHS))
                    {
                        int maPH = phuHuynhTheoHocSinh[maHS];
                        try { phuHuynhBLL.DeletePhuHuynh(maPH); } catch { }
                        phuHuynhDaThem.Remove(maPH);
                    }
                    
                    // Xóa tài khoản
                    string tenDangNhap = "HS" + maHS;
                    nguoiDungBLL.DeleteNguoiDung(tenDangNhap);
                    
                    // Xóa học sinh
                    hocSinhBus.DeleteHocSinh(maHS);
                    
                    // Xóa khỏi danh sách
                    var keyToRemove = hocSinhThanhCong.FirstOrDefault(kvp => kvp.Value.maHS == maHS);
                    if (!string.IsNullOrEmpty(keyToRemove.Key))
                    {
                        hocSinhThanhCong.Remove(keyToRemove.Key);
                    }
                    
                    // Xóa khỏi danh sách đã thêm
                    hocSinhDaThem.Remove(maHS);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi rollback học sinh {maHS}: {ex.Message}");
                }
            }
        }
    }
}
