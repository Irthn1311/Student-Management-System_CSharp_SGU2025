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

            LoadComboBox();
            SetupTables();
            LoadData();
            SetupEventHandlers();
        }

        private void LoadComboBox()
        {
            // Load ComboBox Học Kỳ 
            danhSachHocKy = hocKyBus.DocDSHocKy();
            cbHocKyNamHoc.Items.Clear();
            cbHocKyNamHoc.Items.Add("Chọn học kỳ");
            foreach (var hk in danhSachHocKy)
            {
                cbHocKyNamHoc.Items.Add(hk.TenHocKy + "-" + hk.MaNamHoc);
            }
            if (cbHocKyNamHoc.Items.Count > 0)
            {
                cbHocKyNamHoc.SelectedIndex = 0; // Chọn mục đầu tiên làm mặc định
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
                
                // Kiểm tra đã chọn học kỳ chưa
                if (cbHocKyNamHoc.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ hiện tại để phân lớp tự động.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã học kỳ hiện tại
                string tenHocKyChon = cbHocKyNamHoc.SelectedItem.ToString();
                int maHocKyHienTai = -1;

                foreach (var hk in danhSachHocKy)
                {
                    if ((hk.TenHocKy + "-" + hk.MaNamHoc) == tenHocKyChon)
                    {
                        maHocKyHienTai = hk.MaHocKy;
                        break;
                    }
                }

                if (maHocKyHienTai == -1)
                {
                    MessageBox.Show("Không tìm thấy học kỳ được chọn.", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // *** KIỂM TRA HỌC KỲ ĐƯỢC CHỌN ĐÃ ĐƯỢC PHÂN LỚP CHƯA ***
                int soHocSinhDaPhanLop = phanLopBLL.CountHocSinhInHocKy(maHocKyHienTai);
                if (soHocSinhDaPhanLop > 0)
                {
                    // Lấy tên học kỳ để hiển thị
                    string tenHocKyHienTai = "";
                    foreach (var hk in danhSachHocKy)
                    {
                        if (hk.MaHocKy == maHocKyHienTai)
                        {
                            tenHocKyHienTai = hk.TenHocKy + " " + hk.MaNamHoc;
                            break;
                        }
                    }

                    string thongBao = $"⚠️ HỌC KỲ ĐÃ ĐƯỢC PHÂN LỚP!\n\n";
                    thongBao += $"Học kỳ: {tenHocKyHienTai}\n\n";
                    thongBao += $"Số học sinh đã được phân lớp: {soHocSinhDaPhanLop}\n\n";
                    thongBao += "❌ Không thể phân lớp tự động lại!\n\n";
                    thongBao += "Nếu muốn phân lớp lại, bạn cần xóa dữ liệu phân lớp cũ trước.";
                    
                    MessageBox.Show(thongBao, "Không thể phân lớp lại",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // CHẶN NGAY, KHÔNG CHO PHÂN LỚP LẠI
                }

                // Hiển thị preview trước khi thực hiện
                var preview = phanLopTuDongBLL.TaoPreviewPhanLop(maHocKyHienTai);

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
                if (preview.ContainsKey("SoHSLenLop")) // Kịch bản HK2→HK1
                {
                    int soHSLenLop = (int)preview["SoHSLenLop"];
                    int soHSOLai = (int)preview["SoHSOLai"];
                    double tyLe = (double)preview["TyLeLenLop"];

                    previewMessage += "📊 DỰ KIẾN:\n";
                    previewMessage += $"   ✓ Lên lớp: {soHSLenLop} học sinh\n";
                    previewMessage += $"   ⚠️ Ở lại (học lại): {soHSOLai} học sinh\n";
                    previewMessage += $"   → Tỷ lệ lên lớp: {tyLe:0.0}%\n\n";

                    if (preview.ContainsKey("SoHSGapLoi") && (int)preview["SoHSGapLoi"] > 0)
                    {
                        previewMessage += $"⚠️ Thiếu dữ liệu: {preview["SoHSGapLoi"]} học sinh\n";
                        previewMessage += "   (Không có đủ điểm HK1/HK2 hoặc hạnh kiểm)\n\n";
                    }
                }
                else if (preview.ContainsKey("SoHSDuDieuKien")) // Kịch bản HK1→HK2
                {
                    int duDieuKien = (int)preview["SoHSDuDieuKien"];
                    int khongDuDieuKien = (int)preview["SoHSKhongDuDieuKien"];

                    previewMessage += "📊 DỰ KIẾN:\n";
                    previewMessage += $"   ✓ Đủ dữ liệu: {duDieuKien} học sinh\n";
                    previewMessage += $"      → Sẽ giữ nguyên lớp sang HK2\n\n";

                    if (khongDuDieuKien > 0)
                    {
                        previewMessage += $"   ⚠️ Thiếu dữ liệu: {khongDuDieuKien} học sinh\n";
                        previewMessage += "      (Chưa có điểm, hạnh kiểm hoặc xếp loại HK1)\n\n";
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
                    // Hiển thị progress
                    this.Cursor = Cursors.WaitCursor;
                    
                    // Thực hiện phân lớp tự động
                    var ketQua = phanLopTuDongBLL.ThucHienPhanLopTuDong(maHocKyHienTai);
                    
                    this.Cursor = Cursors.Default;

                    if (ketQua.success)
                    {
                        // ✅ Hiển thị thông báo thành công với ScrollableMessageBox nếu có nhiều thông tin
                        string thongBaoThanhCong = $"✓ Phân lớp tự động thành công!\n\n" +
                                       $"Đã phân lớp: {ketQua.soHocSinhDaPhanLop} học sinh\n\n" +
                                       $"{ketQua.message}";
                        
                        // Sử dụng ScrollableMessageBox để xem đầy đủ thông tin
                        ScrollableMessageBox.Show("Thành công", thongBaoThanhCong, MessageBoxIcon.Information);

                        // Refresh lại bảng phân lớp
                        LoadTablePhanLop();
                        
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
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Đã xảy ra lỗi khi phân lớp tự động:\n{ex.Message}\n\nStack trace:\n{ex.StackTrace}",
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoadTablePhanLop();
        }

        private void LoadTablePhanLop()
        {
            danhSachPhanLop = phanLopBLL.GetAllPhanLop();
            danhSachPhanLopGoc = new List<(int, int, int)>(danhSachPhanLop); // Lưu danh sách gốc để tìm kiếm
            RefreshTablePhanLop(danhSachPhanLop);
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
            string selectedHocKy = cbHocKyNamHoc.SelectedItem?.ToString();
            string selectedLop = cbLop.SelectedItem?.ToString();

            // Lấy tất cả phân lớp
            List<(int maHocSinh, int maLop, int maHocKy)> allPhanLop = phanLopBLL.GetAllPhanLop();
            
            // Lọc theo điều kiện
            var filteredPhanLop = allPhanLop.Where(pl =>
            {
                // Kiểm tra học kỳ
                bool hocKyMatch = true;
                if (selectedHocKy != "Chọn học kỳ" && !string.IsNullOrEmpty(selectedHocKy))
                {
                    string tenHocKy = "";
                    foreach (var hk in danhSachHocKy)
                    {
                        if (hk.MaHocKy == pl.maHocKy)
                        {
                            tenHocKy = hk.TenHocKy + "-" + hk.MaNamHoc;
                            break;
                        }
                    }
                    hocKyMatch = tenHocKy == selectedHocKy;
                }

                // Kiểm tra lớp
                bool lopMatch = true;
                if (selectedLop != "Chọn lớp" && !string.IsNullOrEmpty(selectedLop))
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

                return hocKyMatch && lopMatch;
            }).ToList();

            // Cập nhật bảng
            RefreshTablePhanLop(filteredPhanLop);
        }

        private void RefreshTablePhanLop(List<(int maHocSinh, int maLop, int maHocKy)> phanLopList)
        {
            tablePhanLop.Rows.Clear();
            
            foreach (var pl in phanLopList)
            {
                string tenHocSinh = hocSinhBus.GetHocSinhById(pl.maHocSinh)?.HoTen ?? $"HS {pl.maHocSinh}";

                string tenLop = "";
                foreach (var lop in danhSachLop)
                {
                    if (lop.MaLop == pl.maLop)
                    {
                        tenLop = lop.TenLop;
                        break;
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
                    string tenHocSinh = hocSinhBus.GetHocSinhById(maHS)?.HoTen ?? $"HS {maHS}";

                    if (MessageBox.Show($"Bạn có chắc muốn xóa phân lớp của học sinh {tenHocSinh} (Mã HS: {maHS})?", 
                                       "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (phanLopBLL.DeletePhanLop(maHS, maLop, maHocKy))
                        {
                            MessageBox.Show("Đã xóa phân lớp thành công.", "Thành công", 
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // Cập nhật lại bảng phân lớp
                            LoadTablePhanLop();
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

        private void btnPhanLopChuyenTruong_Click(object sender, EventArgs e)
        {
            try
            {
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

                // Kiểm tra học kỳ hiện tại
                var hocKyHienTai = SemesterHelper.GetCurrentSemester();
                if (hocKyHienTai == null)
                {
                    MessageBox.Show("Không tìm thấy học kỳ đang diễn ra. Vui lòng kiểm tra lại cấu hình học kỳ.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string trangThaiHocKy = SemesterHelper.GetStatus(hocKyHienTai.MaHocKy);
                if (trangThaiHocKy != "Đang diễn ra")
                {
                    MessageBox.Show($"Học kỳ hiện tại không phải 'Đang diễn ra' (Trạng thái: {trangThaiHocKy}).\n\nVui lòng kiểm tra lại cấu hình học kỳ.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị thông báo đang xử lý
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // Gọi hàm nhập Excel
                ImportExcelPhanLopChuyenTruong(filePath, hocKyHienTai);

                this.Cursor = Cursors.Default;

                // Refresh lại bảng phân lớp
                LoadTablePhanLop();
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
                Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong = 
                    ImportPhuHuynhFromWorksheetChuyenTruong(wsPhuHuynh, hocSinhThanhCong);

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

                // 5. Kiểm tra điều kiện và tự động phân lớp
                PhanLopTuDongChoHocSinhChuyenTruong(hocSinhThanhCong, hocKyHienTai);

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
            Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong)
        {
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
            // ✅ Track phụ huynh mới tạo (không phải đã tồn tại) để rollback sau này
            HashSet<int> phuHuynhMoiTao = new HashSet<int>();
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
            resultKiemTra.AppendLine("║   KẾT QUẢ KIỂM TRA ĐIỀU KIỆN CHUYỂN TRƯỜNG    ║");
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
        private void PhanLopTuDongChoHocSinhChuyenTruong(
            Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong,
            HocKyDTO hocKyHienTai)
        {
            if (hocSinhThanhCong.Count == 0)
                return;

            // Lấy danh sách lớp
            var allLop = lopHocBus.DocDSLop();
            
            int successCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();
            var warnings = new StringBuilder();
            // ✅ Dictionary để lưu thông tin lớp đã phân cho từng học sinh
            Dictionary<int, (string tenLop, string nguyenVong, bool laNguyenVong)> lopDaPhan = 
                new Dictionary<int, (string, string, bool)>();

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
                        continue;
                    }

                    // Lấy danh sách lớp cùng khối
                    var lopCungKhoi = allLop.Where(l => l.maKhoi == maKhoi).ToList();
                    if (lopCungKhoi.Count == 0)
                    {
                        errors.AppendLine($"Học sinh {tenHS}: Không tìm thấy lớp nào trong khối {khoi}");
                        errorCount++;
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
                            // Kiểm tra lớp nguyện vọng cùng khối
                            if (lopNguyenVong.maKhoi == maKhoi)
                            {
                                // Kiểm tra sĩ số
                                int siSoHienTai = phanLopBLL.CountHocSinhInLop(lopNguyenVong.maLop, hocKyHienTai.MaHocKy);
                                if (siSoHienTai < lopNguyenVong.siSo)
                                {
                                    lopDuocPhan = lopNguyenVong;
                                }
                                else
                                {
                                    warnings.AppendLine($"Học sinh {tenHS}: Lớp nguyện vọng '{nguyenVong}' đã đầy ({siSoHienTai}/{lopNguyenVong.siSo}) - Tự động phân lớp");
                                }
                            }
                            else
                            {
                                warnings.AppendLine($"Học sinh {tenHS}: Lớp nguyện vọng '{nguyenVong}' không cùng khối ({lopNguyenVong.maKhoi} != {maKhoi}) - Tự động phân lớp");
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
                        // Sắp xếp lớp theo sĩ số hiện tại (tăng dần) - ưu tiên lớp có ít học sinh nhất
                        var lopConCho = lopCungKhoi
                            .Select(l => new
                            {
                                Lop = l,
                                SiSoHienTai = phanLopBLL.CountHocSinhInLop(l.maLop, hocKyHienTai.MaHocKy)
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
                            continue;
                        }
                    }

                    // Kiểm tra học sinh chưa được phân lớp trong học kỳ này
                    if (phanLopBLL.CheckHocSinhDaPhanLop(maHS, hocKyHienTai.MaHocKy))
                    {
                        warnings.AppendLine($"Học sinh {tenHS}: Đã được phân lớp trong học kỳ này - Bỏ qua");
                        continue;
                    }

                    // Phân lớp
                    try
                    {
                        bool success = phanLopBLL.AddPhanLop(maHS, lopDuocPhan.maLop, hocKyHienTai.MaHocKy);
                        if (success)
                        {
                            successCount++;
                            // ✅ Lưu thông tin lớp đã phân
                            bool laNguyenVong = !string.IsNullOrWhiteSpace(nguyenVong) && 
                                                 lopDuocPhan.tenLop.Equals(nguyenVong, StringComparison.OrdinalIgnoreCase);
                            lopDaPhan[maHS] = (lopDuocPhan.tenLop, nguyenVong, laNguyenVong);
                        }
                        else
                        {
                            errors.AppendLine($"Học sinh {tenHS}: Không thể phân lớp vào {lopDuocPhan.tenLop}");
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine($"Học sinh {tenHS}: Lỗi khi phân lớp - {ex.Message}");
                        errorCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Học sinh {tenHS}: {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ Hiển thị kết quả phân lớp chi tiết
            StringBuilder result = new StringBuilder();
            result.AppendLine("╔════════════════════════════════════════════════╗");
            result.AppendLine("║      KẾT QUẢ PHÂN LỚP CHUYỂN TRƯỜNG         ║");
            result.AppendLine("╚════════════════════════════════════════════════╝");
            result.AppendLine();
            
            result.AppendLine($"📊 TỔNG KẾT:");
            result.AppendLine($"   ✓ Thành công: {successCount} học sinh");
            if (errorCount > 0)
                result.AppendLine($"   ✗ Lỗi: {errorCount} học sinh");
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
    }
}
