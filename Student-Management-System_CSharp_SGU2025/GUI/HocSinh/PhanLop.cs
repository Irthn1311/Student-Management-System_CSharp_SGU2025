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

namespace Student_Management_System_CSharp_SGU2025.GUI.HocSinh
{
    public partial class PhanLop : Form
    {
        private LopHocBUS lopHocBus;
        private HocSinhBLL hocSinhBus;
        private HocKyBUS hocKyBus;
        private PhanLopBLL phanLopBLL;
        private List<DTO.LopDTO> danhSachLop;
        private List<DTO.HocKyDTO> danhSachHocKy;
        private List<DTO.HocSinhDTO> hocSinhDTO;
        private List<DTO.HocSinhDTO> hocSinhDTOGoc; // Danh sách học sinh gốc để tìm kiếm
        private List<(int maHocSinh, int maLop, int maHocKy)> danhSachPhanLop;
        
        private bool isShowingHocSinh = true;
        private int selectedHocSinhIndex = -1; // Index của học sinh được chọn

        public PhanLop()
        {
            InitializeComponent();
            lopHocBus = new LopHocBUS();
            hocSinhBus = new HocSinhBLL();
            hocKyBus = new HocKyBUS();
            phanLopBLL = new PhanLopBLL();
            danhSachLop = new List<DTO.LopDTO>();
            danhSachHocKy = new List<DTO.HocKyDTO>();
            hocSinhDTO = new List<DTO.HocSinhDTO>();
            hocSinhDTOGoc = new List<DTO.HocSinhDTO>();
            danhSachPhanLop = new List<(int maHocSinh, int maLop, int maHocKy)>();

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
            if (selectedHocSinhIndex >= 0 && selectedHocSinhIndex < hocSinhDTO.Count)
            {
                HocSinhDTO selectedHocSinh = hocSinhDTO[selectedHocSinhIndex];
                txtHocSinhDuocChon.Text = selectedHocSinh.HoTen;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một học sinh trước khi nhấn nút Chọn.", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThemPhanLop_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các điều kiện đầu vào
                if (string.IsNullOrEmpty(txtHocSinhDuocChon.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng chọn học sinh trước khi thêm phân lớp.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbHocKyNamHoc.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbLop.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã học sinh từ tên đã chọn
                string tenHocSinhChon = txtHocSinhDuocChon.Text.Trim();
                int maHocSinh = -1;

                foreach (var hs in hocSinhDTOGoc)
                {
                    if (hs.HoTen == tenHocSinhChon)
                    {
                        maHocSinh = hs.MaHS;
                        break;
                    }
                }

                if (maHocSinh == -1)
                {
                    MessageBox.Show("Không tìm thấy học sinh được chọn.", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy mã lớp từ ComboBox
                string tenLopChon = cbLop.SelectedItem.ToString();
                int maLop = -1;

                foreach (var lop in danhSachLop)
                {
                    if (lop.TenLop == tenLopChon)
                    {
                        maLop = lop.MaLop;
                        break;
                    }
                }

                // Lấy mã học kỳ từ ComboBox
                string tenHocKyChon = cbHocKyNamHoc.SelectedItem.ToString();
                int maHocKy = -1;

                foreach (var hk in danhSachHocKy)
                {
                    if ((hk.TenHocKy + "-" + hk.MaNamHoc) == tenHocKyChon)
                    {
                        maHocKy = hk.MaHocKy;
                        break;
                    }
                }

                // Gọi hàm thêm phân lớp từ BLL
                bool result = phanLopBLL.AddPhanLop(maHocSinh, maLop, maHocKy);

                if (result)
                {
                    MessageBox.Show($"Đã thêm phân lớp cho học sinh {tenHocSinhChon} vào lớp {tenLopChon} học kỳ {tenHocKyChon}.",
                                   "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại bảng phân lớp
                    LoadTablePhanLop();

                    // Xóa thông tin đã chọn
                    txtHocSinhDuocChon.Clear();
                    selectedHocSinhIndex = -1;
                }
                else
                {
                    MessageBox.Show("Thêm phân lớp thất bại.", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm phân lớp: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // Đóng form hiện tại
        }

        private void btnHocSinh_Click(object sender, EventArgs e)
        {
            isShowingHocSinh = true;
            UpdateView();
        }

        private void btnPhanLop_Click(object sender, EventArgs e)
        {
            isShowingHocSinh = false;
            UpdateView();
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterTablePhanLop();
        }

        #region Setup Tables

        private void SetupTables()
        {
            SetupTableHocSinh();
            SetupTablePhanLop();
            UpdateView();
        }

        private void SetupTableHocSinh()
        {
            // Xóa cột cũ và cấu hình chung
            tableHocSinh.Columns.Clear();
            ApplyBaseTableStyle(tableHocSinh);

            // Thêm cột mới (không có cột Lớp và Thao tác)
            tableHocSinh.Columns.Add("MaHS", "Mã HS");
            tableHocSinh.Columns.Add("HoTen", "Họ và tên");
            tableHocSinh.Columns.Add("NgaySinh", "Ngày sinh");
            tableHocSinh.Columns.Add("GioiTinh", "Giới tính");
            tableHocSinh.Columns.Add("TrangThai", "Trạng thái");

            // Căn chỉnh cột
            ApplyColumnAlignmentAndWrapping(tableHocSinh);
            tableHocSinh.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Tùy chỉnh kích thước
            tableHocSinh.Columns["MaHS"].FillWeight = 15; tableHocSinh.Columns["MaHS"].MinimumWidth = 80;
            tableHocSinh.Columns["HoTen"].FillWeight = 35; tableHocSinh.Columns["HoTen"].MinimumWidth = 200;
            tableHocSinh.Columns["NgaySinh"].FillWeight = 15; tableHocSinh.Columns["NgaySinh"].MinimumWidth = 120;
            tableHocSinh.Columns["GioiTinh"].FillWeight = 15; tableHocSinh.Columns["GioiTinh"].MinimumWidth = 100;
            tableHocSinh.Columns["TrangThai"].FillWeight = 20; tableHocSinh.Columns["TrangThai"].MinimumWidth = 120;

            // Gắn sự kiện
            tableHocSinh.CellFormatting += tableHocSinh_CellFormatting;
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
            if (isShowingHocSinh)
            {
                tableHocSinh.Visible = true;
                tablePhanLop.Visible = false;
                btnHocSinh.FillColor = Color.FromArgb(22, 163, 74);
                btnPhanLop.FillColor = Color.FromArgb(200, 200, 200);
            }
            else
            {
                tableHocSinh.Visible = false;
                tablePhanLop.Visible = true;
                btnHocSinh.FillColor = Color.FromArgb(200, 200, 200);
                btnPhanLop.FillColor = Color.FromArgb(22, 163, 74);
            }
        }

        #endregion

        #region Load Data

        private void LoadData()
        {
            LoadTableHocSinh();
            LoadTablePhanLop();
        }

        private void LoadTableHocSinh()
        {
            tableHocSinh.Rows.Clear();
            hocSinhDTO = hocSinhBus.GetAllHocSinh();
            hocSinhDTOGoc = new List<DTO.HocSinhDTO>(hocSinhDTO); // Lưu danh sách gốc

            foreach (HocSinhDTO hs in hocSinhDTO)
            {
                tableHocSinh.Rows.Add(hs.MaHS, hs.HoTen, hs.NgaySinh.ToString("dd/MM/yyyy"),
                                     hs.GioiTinh, hs.TrangThai);
            }
        }

        private void LoadTablePhanLop()
        {
            danhSachPhanLop = phanLopBLL.GetAllPhanLop();
            RefreshTablePhanLop(danhSachPhanLop);
        }

        #endregion

        #region Event Handlers

        private void tableHocSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Giới tính
            if (tableHocSinh.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null)
            {
                FormatGenderCell(e);
            }

            // Trạng thái
            if (tableHocSinh.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                FormatStatusCell(e);
            }
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
            // Event handler cho txtTimKiem
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            
            // Event handler cho btnChon
            btnChon.Click += btnChon_Click;
            
            
            
            // Event handler cho tableHocSinh
            tableHocSinh.CellClick += tableHocSinh_CellClick;
            tableHocSinh.CellDoubleClick += tableHocSinh_CellDoubleClick;
            
            // Event handler cho ComboBox
            cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
            cbLop.SelectedIndexChanged += cbLop_SelectedIndexChanged;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimKiem.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(searchText))
            {
                // Nếu ô tìm kiếm trống, hiển thị tất cả học sinh
                hocSinhDTO = new List<DTO.HocSinhDTO>(hocSinhDTOGoc);
            }
            else
            {
                // Lọc học sinh theo từ khóa tìm kiếm
                hocSinhDTO = hocSinhDTOGoc.Where(hs => 
                    hs.HoTen.ToLower().Contains(searchText) ||
                    hs.MaHS.ToString().Contains(searchText) ||
                    hs.NgaySinh.ToString("dd/MM/yyyy").Contains(searchText) ||
                    hs.GioiTinh.ToLower().Contains(searchText) ||
                    hs.TrangThai.ToLower().Contains(searchText)
                ).ToList();
            }
            
            // Cập nhật lại bảng
            RefreshTableHocSinh();
        }

        private void tableHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < hocSinhDTO.Count)
            {
                selectedHocSinhIndex = e.RowIndex;
            }
        }

        private void tableHocSinh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < hocSinhDTO.Count)
            {
                selectedHocSinhIndex = e.RowIndex;
                HocSinhDTO selectedHocSinh = hocSinhDTO[selectedHocSinhIndex];
                txtHocSinhDuocChon.Text = selectedHocSinh.HoTen;
            }
        }

        private void RefreshTableHocSinh()
        {
            tableHocSinh.Rows.Clear();
            foreach (HocSinhDTO hs in hocSinhDTO)
            {
                tableHocSinh.Rows.Add(hs.MaHS, hs.HoTen, hs.NgaySinh.ToString("dd/MM/yyyy"),
                                     hs.GioiTinh, hs.TrangThai);
            }
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
