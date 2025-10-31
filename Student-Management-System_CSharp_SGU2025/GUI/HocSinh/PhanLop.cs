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

namespace Student_Management_System_CSharp_SGU2025.GUI.HocSinh
{
    public partial class PhanLop : Form
    {
        private LopHocBUS lopHocBus;
        private HocSinhBLL hocSinhBus;
        private HocKyBUS hocKyBus;
        private PhanLopBLL phanLopBLL;
        private PhanLopTuDongBLL phanLopTuDongBLL;
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

                // Kiểm tra điều kiện phân lớp
                var kiemTra = phanLopTuDongBLL.KiemTraDieuKienPhanLop(maHocKyHienTai);
                if (!kiemTra.success)
                {
                    MessageBox.Show($"Không thể phân lớp tự động:\n{kiemTra.message}", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiển thị preview trước khi thực hiện
                var preview = phanLopTuDongBLL.TaoPreviewPhanLop(maHocKyHienTai);
                
                string previewMessage = "=== XEM TRƯỚC KẾT QUẢ PHÂN LỚP TỰ ĐỘNG ===\n\n";
                previewMessage += $"Loại phân lớp: {preview["LoaiPhanLop"]}\n";
                previewMessage += $"Tổng số học sinh hợp lệ: {preview["TongSoHocSinh"]}\n\n";

                if (preview.ContainsKey("SoHSDuocLenLop"))
                {
                    previewMessage += $"• Học sinh được lên lớp: {preview["SoHSDuocLenLop"]}\n";
                    previewMessage += $"• Học sinh ở lại: {preview["SoHSOLai"]}\n";
                    previewMessage += $"• Tỷ lệ lên lớp: {preview["TyLeLenLop"]:F2}%\n\n";
                }

                if (preview.ContainsKey("SoHSKhongHopLe") && (int)preview["SoHSKhongHopLe"] > 0)
                {
                    previewMessage += $"⚠️ Có {preview["SoHSKhongHopLe"]} học sinh không đủ điều kiện phân lớp\n";
                    previewMessage += "(Thiếu điểm, hạnh kiểm hoặc xếp loại)\n\n";
                }

                previewMessage += "\nBạn có muốn tiếp tục phân lớp tự động không?";

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
                        MessageBox.Show($"✓ Phân lớp tự động thành công!\n\n" +
                                       $"Đã phân lớp: {ketQua.soHocSinhDaPhanLop} học sinh\n\n" +
                                       $"{ketQua.message}",
                                       "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh lại bảng phân lớp
                        LoadTablePhanLop();
                        
                        // Tự động chuyển sang tab Phân lớp để xem kết quả
                        btnPhanLop_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show($"✗ Phân lớp tự động thất bại!\n\n{ketQua.message}",
                                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
