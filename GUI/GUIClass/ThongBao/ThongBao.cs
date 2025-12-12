using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThongBao
{
    public partial class ThongBao : UserControl
    {
        private ThongBaoBUS thongBaoBUS;
        private List<ThongBaoDTO> danhSachThongBaoFull;
        private List<ThongBaoDTO> danhSachThongBaoFiltered;
        private List<LoaiThongBaoDTO> danhSachLoaiThongBao;

        // Phân trang
        private int currentPage = 1;
        private int pageSize = 20;
        private int totalPages = 1;

        // Lọc
        private string selectedLoaiThongBao = null;
        private bool? selectedDaDoc = null;
        private string selectedDoiTuongNhan = null; // Filter theo đối tượng nhận

        public ThongBao()
        {
            InitializeComponent();
            thongBaoBUS = new ThongBaoBUS();
            danhSachThongBaoFull = new List<ThongBaoDTO>();
            danhSachThongBaoFiltered = new List<ThongBaoDTO>();
            danhSachLoaiThongBao = new List<LoaiThongBaoDTO>();
        }

        private void ThongBao_Load(object sender, EventArgs e)
        {
            // ✅ Kiểm tra quyền truy cập - Cho phép nếu có bất kỳ quyền nào (READ, CREATE, UPDATE, DELETE)
            // Không block load data nếu người dùng có CREATE, UPDATE hoặc DELETE (ngay cả khi không có READ)
            bool hasRead = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.READ);
            bool hasCreate = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.CREATE);
            bool hasUpdate = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.UPDATE);
            bool hasDelete = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.DELETE);
            
            // ✅ Kiểm tra xem có bất kỳ quyền nào không
            bool hasAnyPermission = hasRead || hasCreate || hasUpdate || hasDelete;
            
            // Nếu không có bất kỳ quyền nào thì disable form và return
            if (!hasAnyPermission)
            {
                this.Enabled = false;
                MessageBox.Show("Bạn không có quyền truy cập chức năng 'Quản lý thông báo'!", 
                    "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Luôn cho phép load data nếu có bất kỳ quyền nào
            // Cấu hình bảng
            SetupTableThongBao();

            // Load loại thông báo
            LoadLoaiThongBao();

            // Load đối tượng nhận
            LoadDoiTuongNhan();

            // Load dữ liệu (cho phép load ngay cả khi chỉ có CREATE, UPDATE hoặc DELETE)
            LoadData();

            // Áp dụng phân quyền
            ApplyPermissions();

            // Gắn sự kiện tìm kiếm
            if (txtTimKiemThongBao != null)
            {
                txtTimKiemThongBao.TextChanged -= txtTimKiemThongBao_TextChanged;
                txtTimKiemThongBao.TextChanged += txtTimKiemThongBao_TextChanged;
            }

            // Gắn sự kiện lọc loại
            if (cbLoaiTB != null)
            {
                cbLoaiTB.SelectedIndexChanged -= cbLoaiTB_SelectedIndexChanged;
                cbLoaiTB.SelectedIndexChanged += cbLoaiTB_SelectedIndexChanged;
            }

            // Gắn sự kiện lọc đối tượng nhận
            if (cbDoiTuongNhan != null)
            {
                cbDoiTuongNhan.SelectedIndexChanged -= cbDoiTuongNhan_SelectedIndexChanged;
                cbDoiTuongNhan.SelectedIndexChanged += cbDoiTuongNhan_SelectedIndexChanged;
            }

            // Gắn sự kiện nút thêm
            if (btnThemThongBao != null)
            {
                btnThemThongBao.Click -= btnThemThongBao_Click;
                btnThemThongBao.Click += btnThemThongBao_Click;
            }
        }

        #region Load Data

        private void LoadLoaiThongBao()
        {
            try
            {
                danhSachLoaiThongBao = thongBaoBUS.LayDanhSachLoaiThongBao();

                // Populate combobox loại thông báo
                if (cbLoaiTB != null)
                {
                    cbLoaiTB.Items.Clear();
                    cbLoaiTB.Items.Add("Tất cả loại");
                    foreach (var loai in danhSachLoaiThongBao)
                    {
                        cbLoaiTB.Items.Add(loai.TenLoai);
                    }
                    cbLoaiTB.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load loại thông báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDoiTuongNhan()
        {
            try
            {
                if (cbDoiTuongNhan != null)
                {
                    cbDoiTuongNhan.Items.Clear();
                    cbDoiTuongNhan.Items.Add("Tất cả đối tượng nhận");
                    cbDoiTuongNhan.Items.Add("Toàn trường");
                    cbDoiTuongNhan.Items.Add("Giáo viên");
                    cbDoiTuongNhan.Items.Add("Học sinh");
                    cbDoiTuongNhan.Items.Add("Phụ huynh");
                    cbDoiTuongNhan.Items.Add("Theo lớp");
                    cbDoiTuongNhan.Items.Add("Theo khối");
                    cbDoiTuongNhan.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load đối tượng nhận: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        }

        private void LoadData()
        {
            try
            {
                // Load danh sách thông báo theo người dùng hiện tại
                danhSachThongBaoFull = thongBaoBUS.LayDanhSachThongBao(
                    SessionManager.TenDangNhap,
                    null, null, null, 1, 1000  // Load tất cả để filter ở client
                );

                // Áp dụng filter
                ApplyFilters();

                // Load thống kê
                LoadStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            IEnumerable<ThongBaoDTO> filtered = danhSachThongBaoFull.AsEnumerable();

            // Lọc theo loại
            if (!string.IsNullOrEmpty(selectedLoaiThongBao))
            {
                filtered = filtered.Where(tb => tb.LoaiThongBao == selectedLoaiThongBao);
            }

            // Lọc theo trạng thái đọc
            if (selectedDaDoc.HasValue)
        {
                filtered = filtered.Where(tb => tb.DaDoc == selectedDaDoc.Value);
            }

            // Lọc theo đối tượng nhận (dựa vào PhamVi và MaVaiTroNhan trong schema)
            if (!string.IsNullOrEmpty(selectedDoiTuongNhan))
            {
                filtered = filtered.Where(tb =>
                {
                    switch (selectedDoiTuongNhan)
                    {
                        case "Toàn trường":
                            return tb.PhamVi == "ALL";
                        case "Giáo viên":
                            return tb.PhamVi == "VAI_TRO" && tb.MaVaiTroNhan == "teacher";
                        case "Học sinh":
                            return tb.PhamVi == "VAI_TRO" && tb.MaVaiTroNhan == "student";
                        case "Phụ huynh":
                            return tb.PhamVi == "VAI_TRO" && tb.MaVaiTroNhan == "parent";
                        case "Theo lớp":
                            return tb.PhamVi == "LOP" && tb.MaLop.HasValue;
                        case "Theo khối":
                            return tb.PhamVi == "KHOI" && tb.MaKhoi.HasValue;
                        default:
                            return true;
                    }
                });
            }

            // Tìm kiếm
            if (txtTimKiemThongBao != null && !string.IsNullOrWhiteSpace(txtTimKiemThongBao.Text))
            {
                string keyword = txtTimKiemThongBao.Text.ToLower();
                filtered = filtered.Where(tb => 
                    tb.TieuDe.ToLower().Contains(keyword) ||
                    (tb.NoiDung != null && tb.NoiDung.ToLower().Contains(keyword)) ||
                    (tb.DoiTuongNhan != null && tb.DoiTuongNhan.ToLower().Contains(keyword))
                );
            }

            // Convert về List
            danhSachThongBaoFiltered = filtered.ToList();

            // Tính tổng số trang
            totalPages = (int)Math.Ceiling((double)danhSachThongBaoFiltered.Count / pageSize);
            if (totalPages == 0) totalPages = 1;
            if (currentPage > totalPages) currentPage = totalPages;

            // Load dữ liệu vào bảng
            LoadTableData();
        }

        private void LoadTableData()
        {
            try
            {
                tableThongBao.Rows.Clear();

                // Lấy dữ liệu cho trang hiện tại
                var data = danhSachThongBaoFiltered
                    .OrderByDescending(tb => tb.NgayTao)
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                foreach (var tb in data)
                {
                    string noiDungNgan = tb.NoiDung != null && tb.NoiDung.Length > 100
                        ? tb.NoiDung.Substring(0, 100) + "..."
                        : tb.NoiDung;

                    string ngayHetHan = tb.NgayHetHan.HasValue
                        ? tb.NgayHetHan.Value.ToString("dd/MM/yyyy")
                        : "";

                    // Lấy text độ ưu tiên
                    string doUuTienText = tb.GetDoUuTienText();

                    // Tạo màu cho chưa đọc
                    int index = tableThongBao.Rows.Add(
                        tb.MaThongBao,
                        tb.TieuDe,
                        noiDungNgan,
                        tb.TenLoaiThongBao ?? tb.LoaiThongBao,
                        tb.DoiTuongNhan,
                        doUuTienText,
                        tb.NgayTao.ToString("dd/MM/yyyy HH:mm"),
                        tb.TenNguoiTao ?? tb.MaNguoiTao,
                        ngayHetHan,
                        tb.MaNguoiTao ?? "", // ✅ Lưu mã người tạo để kiểm tra quyền
                        "" // Thao tác
                    );

                    // Đặt màu cho cột độ ưu tiên
                    var doUuTienCell = tableThongBao.Rows[index].Cells["DoUuTien"];
                    switch (tb.DoUuTien)
                    {
                        case "KHAN_CAP":
                            doUuTienCell.Style.ForeColor = Color.Red;
                            doUuTienCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            break;
                        case "QUAN_TRONG":
                            doUuTienCell.Style.ForeColor = Color.Orange;
                            doUuTienCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            break;
                        default:
                            doUuTienCell.Style.ForeColor = Color.Green;
                            break;
                    }

                    // Đánh dấu thông báo chưa đọc bằng font đậm
                    if (!tb.DaDoc)
                    {
                        var row = tableThongBao.Rows[index];
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(20, 20, 20);
                    }
                }

                // Cập nhật label phân trang (nếu có)
                UpdatePaginationLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu bảng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePaginationLabel()
        {
            // Nếu có label phân trang, cập nhật text
            // lblPagination.Text = $"Trang {currentPage}/{totalPages} (Tổng: {danhSachThongBaoFiltered.Count})";
        }

        private void LoadStatistics()
        {
            try
            {
                var stats = thongBaoBUS.LayThongKe(SessionManager.TenDangNhap);

                Color defaultTextColor = Color.FromArgb(71, 85, 105);

                // Thẻ 1: Tổng thông báo - Hiển thị thống kê theo phạm vi
                if (statCardTongThongBao != null)
                {
                    statCardTongThongBao.lbCardTitle.Text = "Tổng thông báo";
                    statCardTongThongBao.lbCardValue.Text = stats.TongThongBao.ToString();
                    
                    // Hiển thị thống kê theo phạm vi gửi
                    List<string> phamViList = new List<string>();
                    if (stats.GuiToanTruong > 0)
                        phamViList.Add($"{stats.GuiToanTruong} toàn trường");
                    if (stats.GuiGiaoVien > 0)
                        phamViList.Add($"{stats.GuiGiaoVien} giáo viên");
                    if (stats.GuiHocSinh > 0)
                        phamViList.Add($"{stats.GuiHocSinh} học sinh");
                    
                    if (phamViList.Count > 0)
                    {
                        statCardTongThongBao.lbCardNote.Text = string.Join(", ", phamViList);
                    }
                    else
                    {
                        statCardTongThongBao.lbCardNote.Text = "Thông báo hệ thống";
                    }
                    
                    statCardTongThongBao.lbCardValue.ForeColor = Color.FromArgb(37, 99, 235);
                    statCardTongThongBao.lbCardTitle.ForeColor = defaultTextColor;
                    statCardTongThongBao.lbCardNote.ForeColor = defaultTextColor;
                }

                // Thẻ 2: Gửi Giáo viên
                if (statCardThongBaoGiaoVien != null)
                {
            statCardThongBaoGiaoVien.lbCardTitle.Text = "Gửi Giáo viên";
                    statCardThongBaoGiaoVien.lbCardValue.Text = stats.GuiGiaoVien.ToString();
            statCardThongBaoGiaoVien.lbCardNote.Text = "Thông báo gần đây";
                    statCardThongBaoGiaoVien.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardThongBaoGiaoVien.lbCardTitle.ForeColor = defaultTextColor;
            statCardThongBaoGiaoVien.lbCardNote.ForeColor = defaultTextColor;
                }

                // Thẻ 3: Gửi Học sinh
                if (statCardThongBaoHocSinh != null)
                {
            statCardThongBaoHocSinh.lbCardTitle.Text = "Gửi Học sinh";
                    statCardThongBaoHocSinh.lbCardValue.Text = stats.GuiHocSinh.ToString();
            statCardThongBaoHocSinh.lbCardNote.Text = "Thông báo gần đây";
                    statCardThongBaoHocSinh.lbCardValue.ForeColor = Color.FromArgb(234, 88, 12);
            statCardThongBaoHocSinh.lbCardTitle.ForeColor = defaultTextColor;
            statCardThongBaoHocSinh.lbCardNote.ForeColor = defaultTextColor;
                }

                // Thẻ 4: Thông báo quan trọng - Mở rộng với nhiều loại thống kê, xuống dòng cho 2 cái cuối
                if (statCardThongBaoGiaoVu != null)
                {
                    statCardThongBaoGiaoVu.lbCardTitle.Text = "Quan trọng";
                    int tongQuanTrong = stats.KhanCap + stats.QuanTrong;
                    statCardThongBaoGiaoVu.lbCardValue.Text = tongQuanTrong.ToString();
                    
                    // Tính toán thống kê bổ sung
                    int binhThuong = stats.TongThongBao - tongQuanTrong;
                    
                    // Hiển thị nhiều loại thống kê: Khẩn cấp, Quan trọng, Bình thường, Toàn trường
                    List<string> thongKeList = new List<string>();
                    
                    if (stats.KhanCap > 0)
                        thongKeList.Add($"{stats.KhanCap} khẩn cấp");
                    if (stats.QuanTrong > 0)
                        thongKeList.Add($"{stats.QuanTrong} quan trọng");
                    if (binhThuong > 0)
                        thongKeList.Add($"{binhThuong} bình thường");
                    if (stats.GuiToanTruong > 0)
                        thongKeList.Add($"{stats.GuiToanTruong} toàn trường");
                    
                    // Kết hợp các thống kê, 2 cái cuối xuống dòng để tránh tràn
                    if (thongKeList.Count > 0)
                    {
                        string thongKeText = "";
                        if (thongKeList.Count <= 2)
                        {
                            // Nếu có 2 hoặc ít hơn, viết trên 1 dòng
                            thongKeText = string.Join(", ", thongKeList);
                        }
                        else
                        {
                            // Nếu có nhiều hơn 2, 2 cái đầu trên 1 dòng, các cái còn lại xuống dòng
                            string dong1 = string.Join(", ", thongKeList.Take(2));
                            string dong2 = string.Join(", ", thongKeList.Skip(2));
                            thongKeText = dong1 + Environment.NewLine + dong2;
                        }
                        statCardThongBaoGiaoVu.lbCardNote.Text = thongKeText;
                    }
                    else
                    {
                        statCardThongBaoGiaoVu.lbCardNote.Text = "Cần chú ý";
                    }
                    
                    statCardThongBaoGiaoVu.lbCardValue.ForeColor = Color.FromArgb(124, 58, 237);
                    statCardThongBaoGiaoVu.lbCardTitle.ForeColor = defaultTextColor;
                    statCardThongBaoGiaoVu.lbCardNote.ForeColor = defaultTextColor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thống kê: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Table Setup

        private void SetupTableThongBao()
        {
            tableThongBao.Columns.Clear();
            ApplyBaseTableStyle(tableThongBao);

            // Thêm cột
            tableThongBao.Columns.Add("MaThongBao", "Mã TB");
            tableThongBao.Columns.Add("TieuDe", "Tiêu đề");
            tableThongBao.Columns.Add("NoiDung", "Nội dung");
            tableThongBao.Columns.Add("LoaiThongBao", "Loại");
            tableThongBao.Columns.Add("DoiTuongNhan", "Gửi đến");
            tableThongBao.Columns.Add("DoUuTien", "Độ ưu tiên");
            tableThongBao.Columns.Add("NgayTao", "Ngày tạo");
            tableThongBao.Columns.Add("NguoiTao", "Người tạo");
            tableThongBao.Columns.Add("NgayHetHan", "Hết hạn");
            tableThongBao.Columns.Add("MaNguoiTao", "Mã người tạo"); // ✅ Cột ẩn để kiểm tra quyền
            tableThongBao.Columns.Add("ThaoTac", "Thao tác");

            // Căn chỉnh
            ApplyColumnAlignmentAndWrapping(tableThongBao);
            tableThongBao.Columns["TieuDe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["NoiDung"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["LoaiThongBao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["DoiTuongNhan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["DoUuTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableThongBao.Columns["NguoiTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Kích thước
            tableThongBao.Columns["MaThongBao"].Visible = false;
            tableThongBao.Columns["MaNguoiTao"].Visible = false; // ✅ Ẩn cột mã người tạo
            tableThongBao.Columns["TieuDe"].FillWeight = 20; tableThongBao.Columns["TieuDe"].MinimumWidth = 150;
            tableThongBao.Columns["NoiDung"].FillWeight = 25; tableThongBao.Columns["NoiDung"].MinimumWidth = 180;
            tableThongBao.Columns["LoaiThongBao"].FillWeight = 8; tableThongBao.Columns["LoaiThongBao"].MinimumWidth = 90;
            tableThongBao.Columns["DoiTuongNhan"].FillWeight = 10; tableThongBao.Columns["DoiTuongNhan"].MinimumWidth = 110;
            tableThongBao.Columns["DoUuTien"].FillWeight = 8; tableThongBao.Columns["DoUuTien"].MinimumWidth = 100;
            tableThongBao.Columns["NgayTao"].FillWeight = 10; tableThongBao.Columns["NgayTao"].MinimumWidth = 120;
            tableThongBao.Columns["NguoiTao"].FillWeight = 8; tableThongBao.Columns["NguoiTao"].MinimumWidth = 90;
            tableThongBao.Columns["NgayHetHan"].FillWeight = 8; tableThongBao.Columns["NgayHetHan"].MinimumWidth = 90;
            tableThongBao.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableThongBao.Columns["ThaoTac"].Width = 140; // Tăng width để chứa 3 icon

            tableThongBao.Columns["TieuDe"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            // Gắn sự kiện
            tableThongBao.CellPainting -= tableThongBao_CellPainting;
            tableThongBao.CellPainting += tableThongBao_CellPainting;
            tableThongBao.CellClick -= tableThongBao_CellClick;
            tableThongBao.CellClick += tableThongBao_CellClick;
            tableThongBao.CellDoubleClick -= tableThongBao_CellDoubleClick;
            tableThongBao.CellDoubleClick += tableThongBao_CellDoubleClick;
            tableThongBao.CellFormatting -= tableThongBao_CellFormatting;
            tableThongBao.CellFormatting += tableThongBao_CellFormatting;
        }

        #endregion

        #region Event Handlers

        private void txtTimKiemThongBao_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            ApplyFilters();
        }

        private void cbLoaiTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoaiTB.SelectedIndex == 0)
            {
                selectedLoaiThongBao = null;
            }
            else
            {
                var selectedTen = cbLoaiTB.SelectedItem?.ToString();
                var loai = danhSachLoaiThongBao.FirstOrDefault(l => l.TenLoai == selectedTen);
                selectedLoaiThongBao = loai?.MaLoai;
            }
            currentPage = 1;
            ApplyFilters();
        }

        private void cbDoiTuongNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lọc theo đối tượng nhận
            if (cbDoiTuongNhan.SelectedIndex == 0)
            {
                selectedDoiTuongNhan = null; // Tất cả
            }
            else
            {
                string selected = cbDoiTuongNhan.SelectedItem?.ToString();
                selectedDoiTuongNhan = selected;
            }

            currentPage = 1;
            ApplyFilters();
        }

        private void btnThemThongBao_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Kiểm tra quyền tạo thông báo bằng CheckCreatePermission
                if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLTHONGBAO, "Quản lý thông báo"))
                    return;

                // Mở form thêm thông báo
                using (var frmThemThongBao = new FrmThemThongBao())
                {
                    if (frmThemThongBao.ShowDialog() == DialogResult.OK)
                    {
                        // Reload dữ liệu sau khi thêm thành công
                        LoadData();
                        MessageBox.Show("Thông báo đã được thêm thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form thêm thông báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tableThongBao_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int maThongBao = Convert.ToInt32(tableThongBao.Rows[e.RowIndex].Cells["MaThongBao"].Value);
                try
                {
                    // Lấy thông tin thông báo
                    var thongBao = thongBaoBUS.LayChiTietThongBao(maThongBao, SessionManager.TenDangNhap);
                    if (thongBao != null)
                    {
                        using (var frmChiTiet = new FrmChiTietThongBao(thongBao))
                        {
                            if (frmChiTiet.ShowDialog() == DialogResult.OK)
                            {
                                // Reload dữ liệu sau khi đánh dấu đã đọc
                                LoadData();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông báo!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở chi tiết thông báo: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Cell Painting & Formatting

        private void tableThongBao_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (tableThongBao.Columns[e.ColumnIndex].Name == "DoiTuongNhan" && e.Value != null)
            {
                string doiTuong = e.Value.ToString().ToLower();
                e.CellStyle.ForeColor = Color.FromArgb(40, 40, 40);

                if (doiTuong.Contains("toàn trường") || doiTuong.Contains("all"))
                    e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216);
                else if (doiTuong.Contains("giáo viên") || doiTuong.Contains("teacher"))
                    e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                else if (doiTuong.Contains("học sinh") || doiTuong.Contains("student"))
                    e.CellStyle.ForeColor = Color.FromArgb(154, 52, 18);
                else if (doiTuong.Contains("lớp"))
                    e.CellStyle.ForeColor = Color.FromArgb(55, 48, 163);
        }
        }

        private void tableThongBao_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableThongBao.Columns["ThaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                // ✅ Kiểm tra quyền UPDATE và DELETE
                bool hasUpdatePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.UPDATE);
                bool hasDeletePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.DELETE);
                bool hasCreatePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.CREATE);
                
                // ✅ Lấy mã người tạo của thông báo trong hàng này
                string maNguoiTao = "";
                if (tableThongBao.Rows[e.RowIndex].Cells["MaNguoiTao"].Value != null)
                {
                    maNguoiTao = tableThongBao.Rows[e.RowIndex].Cells["MaNguoiTao"].Value.ToString();
                }
                
                string currentUser = SessionManager.TenDangNhap ?? "";
                bool isOwnNotification = !string.IsNullOrEmpty(maNguoiTao) && 
                                        !string.IsNullOrEmpty(currentUser) && 
                                        maNguoiTao.Equals(currentUser, StringComparison.OrdinalIgnoreCase);
                
                // ✅ Logic phân quyền:
                // - Nếu có quyền UPDATE/DELETE → được sửa/xóa tất cả thông báo
                // - Nếu CHỈ có quyền CREATE → chỉ được sửa/xóa thông báo của chính mình
                bool canUpdate = hasUpdatePermission || (hasCreatePermission && !hasUpdatePermission && isOwnNotification);
                bool canDelete = hasDeletePermission || (hasCreatePermission && !hasDeletePermission && isOwnNotification);

                // Lấy icon từ resources hoặc tạo icon
                Image viewIcon = Properties.Resources.icon_eye ?? CreateSimpleIcon(Color.FromArgb(59, 130, 246)); // Xanh dương
                Image editIcon = Properties.Resources.icon_edit ?? CreateSimpleIcon(Color.FromArgb(34, 197, 94)); // Xanh lá
                Image deleteIcon = Properties.Resources.bin ?? CreateSimpleIcon(Color.FromArgb(239, 68, 68)); // Đỏ

                int iconSize = 18;
                int spacing = 12;
                int totalWidth = iconSize * 3 + spacing * 2; // 3 icon với 2 khoảng cách
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                // Vị trí 3 icon: Xem, Sửa, Xóa
                Rectangle viewRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle editRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + (iconSize + spacing) * 2, y, iconSize, iconSize);

                // ✅ Vẽ icon Xem (luôn hiển thị - không cần quyền)
                e.Graphics.DrawImage(viewIcon, viewRect);

                // ✅ Vẽ icon Sửa với độ mờ nếu không có quyền
                if (canUpdate)
                {
                    e.Graphics.DrawImage(editIcon, editRect);
                }
                else
                {
                    var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
                        new float[][] {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                        });
                    using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                    {
                        attributes.SetColorMatrix(grayScaleMatrix);
                        e.Graphics.DrawImage(editIcon, editRect, 0, 0, editIcon.Width, editIcon.Height,
                            GraphicsUnit.Pixel, attributes);
                    }
                }

                // ✅ Vẽ icon Xóa với độ mờ nếu không có quyền
                if (canDelete)
                {
                    e.Graphics.DrawImage(deleteIcon, deleteRect);
                }
                else
                {
                    var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
                        new float[][] {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                        });
                    using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                    {
                        attributes.SetColorMatrix(grayScaleMatrix);
                        e.Graphics.DrawImage(deleteIcon, deleteRect, 0, 0, deleteIcon.Width, deleteIcon.Height,
                            GraphicsUnit.Pixel, attributes);
                    }
                }

                e.Handled = true;
            }
        }

        // Helper method để tạo icon đơn giản nếu không có resource
        private Image CreateSimpleIcon(Color color)
        {
            Bitmap bmp = new Bitmap(18, 18);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(new SolidBrush(color), 2, 2, 14, 14);
            }
            return bmp;
        }

        private void tableThongBao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableThongBao.Columns["ThaoTac"].Index)
            {
                HandleIconClick(tableThongBao, e.RowIndex, "MaThongBao");
            }
        }

        #endregion

        #region Helper Methods

        private void ApplyPermissions()
        {
            PermissionHelper.ApplyPermissionThongBao(btnThemThongBao, tableThongBao);
        }

        private void ApplyBaseTableStyle(Guna2DataGridView dgv)
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

            dgv.CellMouseEnter -= DataGridView_CellMouseEnter;
            dgv.CellMouseLeave -= DataGridView_CellMouseLeave;
            dgv.SelectionChanged -= DataGridView_SelectionChanged;

            dgv.CellMouseEnter += DataGridView_CellMouseEnter;
            dgv.CellMouseLeave += DataGridView_CellMouseLeave;
            dgv.SelectionChanged += DataGridView_SelectionChanged;

            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
        }

        private void ApplyColumnAlignmentAndWrapping(Guna2DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }

        private void HandleIconClick(Guna2DataGridView dgv, int rowIndex, string idColumnName)
        {
            Rectangle cellBounds = dgv.GetCellDisplayRectangle(dgv.Columns["ThaoTac"].Index, rowIndex, false);
            Point clickPosInCell = dgv.PointToClient(Cursor.Position);
            int xClick = clickPosInCell.X - cellBounds.Left;

            int iconSize = 18;
            int spacing = 12;
            int totalWidth = iconSize * 3 + spacing * 2; // 3 icon với 2 khoảng cách
            int startXInCell = (cellBounds.Width - totalWidth) / 2;

            // Vị trí 3 icon
            int viewIconStartX = startXInCell;
            int viewIconEndX = viewIconStartX + iconSize;
            int editIconStartX = viewIconEndX + spacing;
            int editIconEndX = editIconStartX + iconSize;
            int deleteIconStartX = editIconEndX + spacing;
            int deleteIconEndX = deleteIconStartX + iconSize;

            int maThongBao = Convert.ToInt32(dgv.Rows[rowIndex].Cells[idColumnName].Value);

            if (xClick >= viewIconStartX && xClick < viewIconEndX)
            {
                // Xem chi tiết
                try
                {
                    var thongBao = thongBaoBUS.LayChiTietThongBao(maThongBao, SessionManager.TenDangNhap);
                    if (thongBao != null)
                    {
                        using (var frmChiTiet = new FrmChiTietThongBao(thongBao))
                        {
                            if (frmChiTiet.ShowDialog() == DialogResult.OK)
                            {
                                // Reload dữ liệu sau khi đánh dấu đã đọc
                                LoadData();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông báo!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở chi tiết thông báo: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (xClick >= editIconStartX && xClick < editIconEndX)
            {
                // Sửa thông báo
                try
                {
                    // ✅ Kiểm tra quyền chỉnh sửa
                    bool hasUpdatePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.UPDATE);
                    bool hasCreatePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.CREATE);
                    
                    // Lấy thông tin thông báo
                    var thongBao = thongBaoBUS.LayChiTietThongBao(maThongBao, SessionManager.TenDangNhap);
                    if (thongBao != null)
                    {
                        // ✅ Logic phân quyền:
                        // - Nếu có quyền UPDATE → được sửa tất cả thông báo
                        // - Nếu CHỈ có quyền CREATE → chỉ được sửa thông báo của chính mình
                        bool canEdit = hasUpdatePermission;
                        
                        if (!canEdit && hasCreatePermission)
                        {
                            // Kiểm tra xem có phải thông báo của chính mình không
                            string currentUser = SessionManager.TenDangNhap ?? "";
                            canEdit = !string.IsNullOrEmpty(thongBao.MaNguoiTao) && 
                                     !string.IsNullOrEmpty(currentUser) && 
                                     thongBao.MaNguoiTao.Equals(currentUser, StringComparison.OrdinalIgnoreCase);
                        }
                        
                        if (!canEdit)
                        {
                            MessageBox.Show("Bạn chỉ có thể chỉnh sửa thông báo do chính mình tạo!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        
                        // Mở form chỉnh sửa
                        using (var frmSua = new FrmThemThongBao(thongBao))
                        {
                            if (frmSua.ShowDialog() == DialogResult.OK)
                            {
                                // Reload dữ liệu sau khi sửa
                                LoadData();
                                MessageBox.Show("Thông báo đã được cập nhật thành công!", "Thành công",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông báo!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở form chỉnh sửa: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
            {
                // Xóa
                try
                {
                    // ✅ Kiểm tra quyền xóa
                    bool hasDeletePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.DELETE);
                    bool hasCreatePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.CREATE);
                    
                    // Lấy thông tin thông báo để kiểm tra người tạo
                    var thongBao = thongBaoBUS.LayChiTietThongBao(maThongBao, SessionManager.TenDangNhap);
                    if (thongBao == null)
                    {
                        MessageBox.Show("Không tìm thấy thông báo!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // ✅ Logic phân quyền:
                    // - Nếu có quyền DELETE → được xóa tất cả thông báo
                    // - Nếu CHỈ có quyền CREATE → chỉ được xóa thông báo của chính mình
                    bool canDelete = hasDeletePermission;
                    
                    if (!canDelete && hasCreatePermission)
                    {
                        // Kiểm tra xem có phải thông báo của chính mình không
                        string currentUser = SessionManager.TenDangNhap ?? "";
                        canDelete = !string.IsNullOrEmpty(thongBao.MaNguoiTao) && 
                                   !string.IsNullOrEmpty(currentUser) && 
                                   thongBao.MaNguoiTao.Equals(currentUser, StringComparison.OrdinalIgnoreCase);
                    }
                    
                    if (!canDelete)
                    {
                        MessageBox.Show("Bạn chỉ có thể xóa thông báo do chính mình tạo!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (MessageBox.Show("Bạn có chắc muốn xóa thông báo này?", "Xác nhận xóa",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        var result = thongBaoBUS.XoaThongBao(maThongBao, SessionManager.TenDangNhap);
                        if (result.Success)
                        {
                            MessageBox.Show(result.Message, "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show(result.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa thông báo: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as Guna2DataGridView;
                if (dgv != null)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(243, 246, 255);
            }
        }

        private void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as Guna2DataGridView;
                if (dgv != null)
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        (e.RowIndex % 2 == 0) ? Color.White : Color.FromArgb(250, 250, 250);
            }
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as Guna2DataGridView;
            if (dgv != null)
                dgv.ClearSelection();
        }

        #endregion

        private void tableThongBao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThemThongBao_Click_1(object sender, EventArgs e)
        {

        }
    }
}
