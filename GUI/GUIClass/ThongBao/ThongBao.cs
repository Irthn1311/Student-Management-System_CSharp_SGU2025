using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            // Kiểm tra quyền truy cập
            if (!PermissionHelper.CheckAccessPermission(PermissionHelper.QLTHONGBAO, "Quản lý thông báo"))
            {
                this.Enabled = false;
                return;
            }

            // Cấu hình bảng
            SetupTableThongBao();

            // Load loại thông báo
            LoadLoaiThongBao();

            // Load dữ liệu
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

                    // Tạo màu cho chưa đọc
                    int index = tableThongBao.Rows.Add(
                        tb.MaThongBao,
                        tb.TieuDe,
                        noiDungNgan,
                        tb.TenLoaiThongBao ?? tb.LoaiThongBao,
                        tb.DoiTuongNhan,
                        tb.NgayTao.ToString("dd/MM/yyyy HH:mm"),
                        tb.TenNguoiTao ?? tb.MaNguoiTao,
                        ngayHetHan,
                        "" // Thao tác
                    );

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

                // Thẻ 1: Tổng thông báo
                if (statCardTongThongBao != null)
                {
                    statCardTongThongBao.lbCardTitle.Text = "Tổng thông báo";
                    statCardTongThongBao.lbCardValue.Text = stats.TongThongBao.ToString();
                    statCardTongThongBao.lbCardNote.Text = $"{stats.ChuaDoc} chưa đọc";
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

                // Thẻ 4: Thông báo quan trọng
                if (statCardThongBaoGiaoVu != null)
                {
                    statCardThongBaoGiaoVu.lbCardTitle.Text = "Quan trọng";
                    statCardThongBaoGiaoVu.lbCardValue.Text = (stats.KhanCap + stats.QuanTrong).ToString();
                    statCardThongBaoGiaoVu.lbCardNote.Text = "Cần chú ý";
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
            tableThongBao.Columns.Add("NgayTao", "Ngày tạo");
            tableThongBao.Columns.Add("NguoiTao", "Người tạo");
            tableThongBao.Columns.Add("NgayHetHan", "Hết hạn");
            tableThongBao.Columns.Add("ThaoTac", "Thao tác");

            // Căn chỉnh
            ApplyColumnAlignmentAndWrapping(tableThongBao);
            tableThongBao.Columns["TieuDe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["NoiDung"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["LoaiThongBao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["DoiTuongNhan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["NguoiTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Kích thước
            tableThongBao.Columns["MaThongBao"].Visible = false;
            tableThongBao.Columns["TieuDe"].FillWeight = 25; tableThongBao.Columns["TieuDe"].MinimumWidth = 180;
            tableThongBao.Columns["NoiDung"].FillWeight = 30; tableThongBao.Columns["NoiDung"].MinimumWidth = 200;
            tableThongBao.Columns["LoaiThongBao"].FillWeight = 10; tableThongBao.Columns["LoaiThongBao"].MinimumWidth = 100;
            tableThongBao.Columns["DoiTuongNhan"].FillWeight = 12; tableThongBao.Columns["DoiTuongNhan"].MinimumWidth = 120;
            tableThongBao.Columns["NgayTao"].FillWeight = 12; tableThongBao.Columns["NgayTao"].MinimumWidth = 130;
            tableThongBao.Columns["NguoiTao"].FillWeight = 10; tableThongBao.Columns["NguoiTao"].MinimumWidth = 100;
            tableThongBao.Columns["NgayHetHan"].FillWeight = 10; tableThongBao.Columns["NgayHetHan"].MinimumWidth = 100;
            tableThongBao.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableThongBao.Columns["ThaoTac"].Width = 100;

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
            // Lọc theo trạng thái đọc
            if (cbDoiTuongNhan.SelectedIndex == 0)
                selectedDaDoc = null;
            else if (cbDoiTuongNhan.SelectedIndex == 1)
                selectedDaDoc = false; // Chưa đọc
            else if (cbDoiTuongNhan.SelectedIndex == 2)
                selectedDaDoc = true;  // Đã đọc

            currentPage = 1;
            ApplyFilters();
        }

        private void btnThemThongBao_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra quyền tạo thông báo
                if (!PermissionHelper.CheckAccessPermission(PermissionHelper.QLTHONGBAO, "Quản lý thông báo"))
                {
                    MessageBox.Show("Bạn không có quyền thêm thông báo!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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

                Image editIcon = Properties.Resources.icon_eye;
                Image deleteIcon = Properties.Resources.bin;

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle viewRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);

                e.Graphics.DrawImage(editIcon, viewRect);
                e.Graphics.DrawImage(deleteIcon, deleteRect);

                e.Handled = true;
            }
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
            int spacing = 15;
            int totalWidth = iconSize * 2 + spacing;
            int startXInCell = (cellBounds.Width - totalWidth) / 2;

            int viewIconEndX = startXInCell + iconSize;
            int deleteIconStartX = startXInCell + iconSize + spacing;
            int deleteIconEndX = deleteIconStartX + iconSize;

            int maThongBao = Convert.ToInt32(dgv.Rows[rowIndex].Cells[idColumnName].Value);

            if (xClick >= startXInCell && xClick < viewIconEndX)
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
            else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
            {
                // Xóa
                if (!PermissionHelper.CheckDataGridIconPermission(dgv, "delete", "Quản lý thông báo"))
                    return;

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
    }
}
