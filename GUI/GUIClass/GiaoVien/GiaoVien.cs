using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class GiaoVien : UserControl
    {
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private LopHocBUS lopHocBUS;
        private PhanCongGiangDayBUS phanCongBUS;
        private BindingList<GiaoVienDTO> bindingListGiaoVien;
        private List<GiaoVienDTO> danhSachGiaoVienFull;
        private List<MonHocDTO> danhSachMonHoc;
        private Dictionary<string, string> dictGiaoVienChuNhiem; // MaGiaoVien -> TenLop

        public GiaoVien()
        {
            InitializeComponent();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            lopHocBUS = new LopHocBUS();
            phanCongBUS = new PhanCongGiangDayBUS();
            bindingListGiaoVien = new BindingList<GiaoVienDTO>();
            danhSachGiaoVienFull = new List<GiaoVienDTO>();
            danhSachMonHoc = new List<MonHocDTO>();
            dictGiaoVienChuNhiem = new Dictionary<string, string>();
            SetupTableGiaoVien();
        }

        private void SetupTableGiaoVien()
        {
            // Cấu hình chung
            tableGiaoVien.EnableHeadersVisualStyles = false;
            tableGiaoVien.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            tableGiaoVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            tableGiaoVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableGiaoVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            tableGiaoVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableGiaoVien.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            tableGiaoVien.ColumnHeadersHeight = 42;

            tableGiaoVien.DefaultCellStyle.BackColor = Color.White;
            tableGiaoVien.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            tableGiaoVien.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            tableGiaoVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            tableGiaoVien.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            tableGiaoVien.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            tableGiaoVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            tableGiaoVien.GridColor = Color.FromArgb(230, 230, 230);
            tableGiaoVien.RowTemplate.Height = 46;
            tableGiaoVien.BorderStyle = BorderStyle.None;
            tableGiaoVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableGiaoVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tableGiaoVien.MultiSelect = false;
            tableGiaoVien.ReadOnly = true;
            tableGiaoVien.AllowUserToResizeColumns = false;
            tableGiaoVien.AllowUserToResizeRows = false;
            tableGiaoVien.RowHeadersVisible = false;
            tableGiaoVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tableGiaoVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // === Tạo các cột dữ liệu ===
            tableGiaoVien.Columns.Clear();
            tableGiaoVien.Columns.Add("MaGv", "Mã GV");
            tableGiaoVien.Columns.Add("HoTen", "Họ và tên");
            tableGiaoVien.Columns.Add("GioiTinh", "Giới tính");
            tableGiaoVien.Columns.Add("ChuyenMon", "Chuyên môn");
            tableGiaoVien.Columns.Add("Sdt", "Sdt");
            tableGiaoVien.Columns.Add("TrangThai", "Trạng thái");

            // ✅ Sử dụng LINQ to Objects để cấu hình các cột
            tableGiaoVien.Columns.Cast<DataGridViewColumn>()
                .ToList()
                .ForEach(col =>
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                });

            // Họ tên căn trái
            tableGiaoVien.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // === Tùy chỉnh kích thước hợp lý ===
            tableGiaoVien.Columns["MaGv"].FillWeight = 10; tableGiaoVien.Columns["MaGv"].MinimumWidth = 60;
            tableGiaoVien.Columns["HoTen"].FillWeight = 25; tableGiaoVien.Columns["HoTen"].MinimumWidth = 150;
            tableGiaoVien.Columns["GioiTinh"].FillWeight = 10; tableGiaoVien.Columns["GioiTinh"].MinimumWidth = 80;
            tableGiaoVien.Columns["ChuyenMon"].FillWeight = 12; tableGiaoVien.Columns["ChuyenMon"].MinimumWidth = 100;
            tableGiaoVien.Columns["Sdt"].FillWeight = 10; tableGiaoVien.Columns["Sdt"].MinimumWidth = 70;
            tableGiaoVien.Columns["TrangThai"].FillWeight = 10; tableGiaoVien.Columns["TrangThai"].MinimumWidth = 90;

            // ====== CỘT THAO TÁC (Xem, Sửa, Xóa) - Sử dụng CellPainting để vẽ icon ======
            tableGiaoVien.Columns.Add("ThaoTacGV", "Thao tác");
            tableGiaoVien.Columns["ThaoTacGV"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableGiaoVien.Columns["ThaoTacGV"].Width = 200; // Đủ rộng cho 3 icon lớn hơn
            tableGiaoVien.Columns["ThaoTacGV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Event
            tableGiaoVien.CellFormatting += tableGiaoVien_CellFormatting;
            tableGiaoVien.CellPainting += tableGiaoVien_CellPainting;
            tableGiaoVien.CellClick += tableGiaoVien_CellContentClick;

            tableGiaoVien.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    tableGiaoVien.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(243, 246, 255);
            };
            tableGiaoVien.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    tableGiaoVien.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        (e.RowIndex % 2 == 0) ? Color.White : Color.FromArgb(250, 250, 250);
            };

            tableGiaoVien.SelectionChanged += (s, e) => tableGiaoVien.ClearSelection();
        }


        // === Hàm xử lý click icon ===
        // Xem chi tiết giáo viên (chế độ chỉ đọc)
        private void HandleViewClick(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= tableGiaoVien.Rows.Count)
                return;

            // Lấy MaGiaoVien từ row để tránh lỗi khi sắp xếp
            string maGiaoVien = tableGiaoVien.Rows[rowIndex].Cells["MaGv"].Value?.ToString();
            if (string.IsNullOrEmpty(maGiaoVien))
                return;

            // Tìm giáo viên trong danh sách đầy đủ
            var giaoVien = danhSachGiaoVienFull.FirstOrDefault(gv => gv.MaGiaoVien == maGiaoVien);
            if (giaoVien == null)
                return;

            // Mở form xem chi tiết giáo viên mới
            var formChiTiet = new XemChiTietGiaoVien(giaoVien.MaGiaoVien);
            formChiTiet.ShowDialog();
        }

        // Sửa thông tin giáo viên
        private void HandleEditClick(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= tableGiaoVien.Rows.Count)
                return;

            // ✅ Kiểm tra quyền UPDATE
            if (!PermissionHelper.CheckDataGridIconPermission(tableGiaoVien, "edit", "Quản lý giáo viên"))
                return;

            // Lấy MaGiaoVien từ row để tránh lỗi khi sắp xếp
            string maGiaoVien = tableGiaoVien.Rows[rowIndex].Cells["MaGv"].Value?.ToString();
            if (string.IsNullOrEmpty(maGiaoVien))
                return;

            // Tìm giáo viên trong danh sách đầy đủ
            var giaoVien = danhSachGiaoVienFull.FirstOrDefault(gv => gv.MaGiaoVien == maGiaoVien);
            if (giaoVien == null)
                return;

            // Mở form chỉnh sửa giáo viên
            var formChinhSua = new ChinhSuaGiaoVien(giaoVien.MaGiaoVien, readOnly: false);
            if (formChinhSua.ShowDialog() == DialogResult.OK)
            {
                // Reload dữ liệu sau khi sửa
                LoadData();
            }
        }

        private void HandleDelClick(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= tableGiaoVien.Rows.Count)
                return;

            // ✅ Kiểm tra quyền DELETE
            if (!PermissionHelper.CheckDataGridIconPermission(tableGiaoVien, "delete", "Quản lý giáo viên"))
                return;

            // Lấy MaGiaoVien từ row để tránh lỗi khi sắp xếp
            string maGiaoVien = tableGiaoVien.Rows[rowIndex].Cells["MaGv"].Value?.ToString();
            if (string.IsNullOrEmpty(maGiaoVien))
                return;

            // Tìm giáo viên trong danh sách đầy đủ
            var giaoVien = danhSachGiaoVienFull.FirstOrDefault(gv => gv.MaGiaoVien == maGiaoVien);
            if (giaoVien == null)
                return;

            // Xác nhận xóa
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa giáo viên:\n\n" +
                $"Mã GV: {giaoVien.MaGiaoVien}\n" +
                $"Họ tên: {giaoVien.HoTen}\n\n" +
                $"Lưu ý: Tài khoản đăng nhập của giáo viên cũng sẽ bị xóa!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Kiểm tra giáo viên có phân công giảng dạy không
                    var danhSachPhanCong = phanCongBUS.LayPhanCongTheoGiaoVien(giaoVien.MaGiaoVien);
                    
                    Dictionary<int, string> danhSachGiaoVienThayThe = null;
                    
                    // Nếu có phân công, mở form chọn giáo viên thay thế
                    if (danhSachPhanCong.Count > 0)
                    {
                        var formChon = new ChonGiaoVienThayThe(giaoVien.MaGiaoVien);
                        if (formChon.ShowDialog() == DialogResult.OK)
                        {
                            danhSachGiaoVienThayThe = formChon.DanhSachGiaoVienThayThe;
                        }
                        else
                        {
                            // Người dùng hủy, không xóa
                            return;
                        }
                    }

                    // Thực hiện xóa với danh sách giáo viên thay thế
                    bool success = giaoVienBUS.XoaGiaoVien(giaoVien.MaGiaoVien, danhSachGiaoVienThayThe);
                    if (success)
                    {
                        string message = "Xóa giáo viên thành công!";
                        if (danhSachPhanCong.Count > 0)
                        {
                            message += $"\n\nĐã chuyển {danhSachPhanCong.Count} phân công giảng dạy sang giáo viên thay thế.";
                        }
                        MessageBox.Show(message, "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa giáo viên. Có thể giáo viên đang được sử dụng trong hệ thống.",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Lỗi khi xóa giáo viên: {ex.Message}";
                    
                    // Thông báo chi tiết hơn nếu thiếu giáo viên thay thế
                    if (ex.Message.Contains("thay thế") || ex.Message.Contains("replacement") || 
                        ex.Message.Contains("KHÔNG CÓ GIÁO VIÊN"))
                    {
                        errorMessage += "\n\nVui lòng đảm bảo tất cả phân công giảng dạy đều có giáo viên thay thế phù hợp.";
                    }
                    
                    MessageBox.Show(errorMessage, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GiaoVien_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ Kiểm tra quyền truy cập
                if (!PermissionHelper.CheckAccessPermission(PermissionHelper.QLGIAOVIEN, "Quản lý giáo viên"))
                {
                    this.Enabled = false;
                    return;
                }

                // ✅ Áp dụng phân quyền
                PermissionHelper.ApplyPermissionGiaoVien(btnThemGiaoVien, tableGiaoVien);

                // Load danh sách môn học cho combobox
                LoadDanhSachMonHoc();

                // Load dữ liệu
                LoadData();

                // Gắn sự kiện tìm kiếm và lọc
                txtTimKiemGiaoVien.TextChanged += TxtTimKiemGiaoVien_TextChanged;
                cbBoMon.SelectedIndexChanged += CbBoMon_SelectedIndexChanged;
                cbGVCN.SelectedIndexChanged += CbGVCN_SelectedIndexChanged;
                cbTrangThai.SelectedIndexChanged += CbTrangThai_SelectedIndexChanged;
                btnThemGiaoVien.Click += BtnThemGiaoVien_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachMonHoc()
        {
            try
            {
                danhSachMonHoc = monHocBUS.DocDSMH();
                cbBoMon.Items.Clear();
                cbBoMon.Items.Add("Tất cả bộ môn");
                
                // ✅ Sử dụng LINQ to Objects để thêm tên môn học vào combobox
                danhSachMonHoc.Select(m => m.tenMon)
                    .ToList()
                    .ForEach(tenMon => cbBoMon.Items.Add(tenMon));
                cbBoMon.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách môn học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // Load dữ liệu từ database
                danhSachGiaoVienFull = giaoVienBUS.DocDSGiaoVien();
                
                // Load danh sách lớp để lấy thông tin GVCN
                LoadDanhSachLop();
                
                // Áp dụng tìm kiếm và lọc
                ApplyFilterAndSearch();

                // Cập nhật thống kê
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachLop()
        {
            try
            {
                dictGiaoVienChuNhiem.Clear();
                var danhSachLop = lopHocBUS.DocDSLop();
                
                // ✅ Sử dụng LINQ to Objects để tạo dictionary map MaGiaoVien -> TenLop
                danhSachLop
                    .Where(lop => !string.IsNullOrEmpty(lop.maGVCN))
                    .ToList()
                    .ForEach(lop => 
                    {
                        // Nếu giáo viên đã là GVCN của lớp khác, nối thêm lớp mới
                        if (dictGiaoVienChuNhiem.ContainsKey(lop.maGVCN))
                        {
                            dictGiaoVienChuNhiem[lop.maGVCN] += $", {lop.tenLop}";
                        }
                        else
                        {
                            dictGiaoVienChuNhiem[lop.maGVCN] = lop.tenLop;
                        }
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải danh sách lớp: {ex.Message}");
            }
        }

        private void ApplyFilterAndSearch()
        {
            try
            {
                var danhSachLoc = danhSachGiaoVienFull.AsEnumerable();

                // Lọc theo bộ môn
                if (cbBoMon.SelectedIndex > 0 && cbBoMon.SelectedItem != null)
                {
                    string tenMon = cbBoMon.SelectedItem.ToString();
                    var monHoc = danhSachMonHoc.FirstOrDefault(m => m.tenMon == tenMon);
                    if (monHoc != null)
                    {
                        danhSachLoc = danhSachLoc.Where(gv => gv.MaMonChuyenMon == monHoc.maMon);
                    }
                }

                // Lọc theo GVCN
                if (cbGVCN.SelectedIndex > 0 && cbGVCN.SelectedItem != null)
                {
                    string filterGVCN = cbGVCN.SelectedItem.ToString();
                    if (filterGVCN == "Có làm GVCN")
                    {
                        danhSachLoc = danhSachLoc.Where(gv => dictGiaoVienChuNhiem.ContainsKey(gv.MaGiaoVien));
                    }
                    else if (filterGVCN == "Không làm GVCN")
                    {
                        danhSachLoc = danhSachLoc.Where(gv => !dictGiaoVienChuNhiem.ContainsKey(gv.MaGiaoVien));
                    }
                }

                // Lọc theo trạng thái
                if (cbTrangThai.SelectedIndex > 0 && cbTrangThai.SelectedItem != null)
                {
                    string trangThai = cbTrangThai.SelectedItem.ToString();
                    danhSachLoc = danhSachLoc.Where(gv => 
                        (gv.TrangThai ?? "Đang giảng dạy") == trangThai);
                }

                // Tìm kiếm
                string keyword = txtTimKiemGiaoVien.Text.Trim();
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    danhSachLoc = danhSachLoc.Where(gv =>
                        (gv.HoTen != null && gv.HoTen.ToLower().Contains(keyword)) ||
                        (gv.MaGiaoVien != null && gv.MaGiaoVien.ToLower().Contains(keyword)) ||
                        (gv.Email != null && gv.Email.ToLower().Contains(keyword)) ||
                        (gv.SoDienThoai != null && gv.SoDienThoai.Contains(keyword))
                    );
                }

                // ✅ Cập nhật BindingList bằng LINQ to Objects
                bindingListGiaoVien.Clear();
                danhSachLoc.ToList()
                    .ForEach(gv => bindingListGiaoVien.Add(gv));

                // Cập nhật DataGridView
                UpdateDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataGridView()
        {
            tableGiaoVien.Rows.Clear();
            
            // ✅ Sử dụng LINQ to Objects để xử lý và thêm dữ liệu vào DataGridView
            bindingListGiaoVien.Select(gv => new
            {
                gv.MaGiaoVien,
                gv.HoTen,
                GioiTinh = gv.GioiTinh ?? "",
                ChuyenMon = FormatChuyenMon(gv),
                SoDienThoai = gv.SoDienThoai ?? "",
                TrangThai = gv.TrangThai ?? "Đang giảng dạy"
            })
            .ToList()
            .ForEach(gv =>
            {
                int idx = tableGiaoVien.Rows.Add(
                    gv.MaGiaoVien,
                    gv.HoTen,
                    gv.GioiTinh,
                    gv.ChuyenMon,
                    gv.SoDienThoai,
                    gv.TrangThai,
                    ""  // ThaoTacGV - sẽ được vẽ trong CellPainting
                );
            });
        }

        private string FormatChuyenMon(GiaoVienDTO gv)
        {
            string chuyenMon = !string.IsNullOrEmpty(gv.TenMonChuyenMon) ? gv.TenMonChuyenMon : "Chưa phân công";
            
            // Nếu giáo viên là GVCN, thêm tên lớp vào
            if (dictGiaoVienChuNhiem.ContainsKey(gv.MaGiaoVien))
            {
                string tenLop = dictGiaoVienChuNhiem[gv.MaGiaoVien];
                chuyenMon += $" ({tenLop})";
            }
            
            return chuyenMon;
        }

        private void UpdateStatistics()
        {
            try
            {
                var thongKe = giaoVienBUS.ThongKeGiaoVien();

                statCardTongGiaoVien.lbCardTitle.Text = "Tổng giáo viên";
                statCardTongGiaoVien.lbCardValue.Text = thongKe["TongGiaoVien"].ToString();
                statCardTongGiaoVien.lbCardNote.Text = "";

                statCardGiaoVienNam.lbCardTitle.Text = "Nam";
                statCardGiaoVienNam.lbCardValue.Text = thongKe["Nam"].ToString();
                statCardGiaoVienNam.lbCardNote.Text = "";

                statCardGiaoVienNu.lbCardTitle.Text = "Nữ";
                statCardGiaoVienNu.lbCardValue.Text = thongKe["Nu"].ToString();
                statCardGiaoVienNu.lbCardNote.Text = "";

                statCardBoMon.lbCardTitle.Text = "Bộ môn";
                statCardBoMon.lbCardValue.Text = thongKe["BoMon"].ToString();
                statCardBoMon.lbCardNote.Text = "";

                statCardTongGiaoVien.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
                statCardGiaoVienNam.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
                statCardGiaoVienNu.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
                statCardBoMon.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
                statCardBoMon.lbCardTitle.ForeColor = Color.FromArgb(220, 38, 38);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật thống kê: {ex.Message}");
            }
        }

        private void TxtTimKiemGiaoVien_TextChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSearch();
        }

        private void CbBoMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSearch();
        }

        private void CbGVCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSearch();
        }

        private void CbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSearch();
        }

        private void BtnThemGiaoVien_Click(object sender, EventArgs e)
        {
            // ✅ Kiểm tra quyền CREATE
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLGIAOVIEN, "Quản lý giáo viên"))
                return;

            var formThem = new ThemGiaoVien();
            if (formThem.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void tableGiaoVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // ✅ Xử lý click vào cột ThaoTacGV
            if (e.ColumnIndex == tableGiaoVien.Columns["ThaoTacGV"].Index)
            {
                Rectangle cellBounds = tableGiaoVien.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Point clickPosInCell = tableGiaoVien.PointToClient(Cursor.Position);
                int xClick = clickPosInCell.X - cellBounds.Left;

                int iconSize = 22; // ✅ Phóng to icon từ 18 lên 22
                int spacing = 14; // ✅ Tăng khoảng cách từ 12 lên 14
                int totalWidth = iconSize * 3 + spacing * 2; // 3 icon: Xem, Sửa, Xóa
                int startXInCell = (cellBounds.Width - totalWidth) / 2;

                // ✅ Tính toán vị trí các icon
                int viewIconEndX = startXInCell + iconSize;
                int editIconStartX = startXInCell + iconSize + spacing;
                int editIconEndX = editIconStartX + iconSize;
                int deleteIconStartX = editIconStartX + iconSize + spacing;
                int deleteIconEndX = deleteIconStartX + iconSize;

                // ✅ Click Xem chi tiết (icon đầu tiên)
                if (xClick >= startXInCell && xClick < viewIconEndX)
                {
                    HandleViewClick(e.RowIndex);
                }
                // ✅ Click Sửa (icon thứ hai)
                else if (xClick >= editIconStartX && xClick < editIconEndX)
                {
                    HandleEditClick(e.RowIndex);
                }
                // ✅ Click Xóa (icon thứ ba)
                else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
                {
                    HandleDelClick(e.RowIndex);
                }
            }
        }

        private void tableGiaoVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (tableGiaoVien.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold);
                if (e.Value.ToString() == "Nam")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216);
                    e.CellStyle.BackColor = Color.FromArgb(219, 234, 254);
                }
                else if (e.Value.ToString() == "Nữ")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(190, 24, 93);
                    e.CellStyle.BackColor = Color.FromArgb(243, 232, 255);
                }
            }

            if (tableGiaoVien.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                string trangThai = e.Value.ToString();
                if (trangThai == "Đang giảng dạy")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(52, 168, 83);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Gray;
                }
            }
        }

        // ✅ Vẽ icon cho bảng Giáo Viên (Xem, Sửa, Xóa)
        private void tableGiaoVien_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableGiaoVien.Columns["ThaoTacGV"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                // ✅ Lấy permission từ Tag - Sử dụng cách an toàn hơn (không dùng dynamic)
                bool canUpdate = true; // Mặc định true
                bool canDelete = true; // Mặc định true
                
                if (tableGiaoVien.Tag != null)
                {
                    // ✅ Sử dụng reflection thay vì dynamic để tránh RuntimeBinderException
                    try
                    {
                        var tagType = tableGiaoVien.Tag.GetType();
                        var canUpdateProp = tagType.GetProperty("CanUpdate");
                        var canDeleteProp = tagType.GetProperty("CanDelete");
                        
                        if (canUpdateProp != null)
                        {
                            var value = canUpdateProp.GetValue(tableGiaoVien.Tag);
                            if (value is bool) canUpdate = (bool)value;
                        }
                        
                        if (canDeleteProp != null)
                        {
                            var value = canDeleteProp.GetValue(tableGiaoVien.Tag);
                            if (value is bool) canDelete = (bool)value;
                        }
                    }
                    catch
                    {
                        // Ignore errors - sử dụng giá trị mặc định
                        canUpdate = true;
                        canDelete = true;
                    }
                }

                // ✅ Icon Xem (luôn hiển thị - không cần quyền)
                Image viewIcon = CreateViewIcon();
                Image editIcon = Properties.Resources.edit_icon ?? Properties.Resources.repair;
                Image deleteIcon = Properties.Resources.deleteicon ?? Properties.Resources.bin;

                int iconSize = 22; // ✅ Phóng to icon từ 18 lên 22
                int spacing = 14; // ✅ Tăng khoảng cách từ 12 lên 14
                int totalWidth = iconSize * 3 + spacing * 2; // 3 icon với 2 khoảng cách
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle viewRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle editRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + (iconSize + spacing) * 2, y, iconSize, iconSize);

                // ✅ Vẽ icon Xem (luôn hiển thị)
                e.Graphics.DrawImage(viewIcon, viewRect);

                // ✅ Vẽ icon Sửa với độ mờ nếu không có quyền
                if (canUpdate)
                {
                    e.Graphics.DrawImage(editIcon, editRect);
                }
                else
                {
                    var grayScaleMatrix = new ColorMatrix(
                        new float[][] {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                        });
                    using (var attributes = new ImageAttributes())
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
                    var grayScaleMatrix = new ColorMatrix(
                        new float[][] {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                        });
                    using (var attributes = new ImageAttributes())
                    {
                        attributes.SetColorMatrix(grayScaleMatrix);
                        e.Graphics.DrawImage(deleteIcon, deleteRect, 0, 0, deleteIcon.Width, deleteIcon.Height,
                            GraphicsUnit.Pixel, attributes);
                    }
                }

                e.Handled = true;
            }
        }

        // ✅ Tạo icon "Xem" (eye icon) bằng code
        private Image CreateViewIcon()
        {
            Bitmap bmp = new Bitmap(22, 22); // ✅ Phóng to icon từ 18x18 lên 22x22
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Vẽ hình mắt đơn giản
                // Màu xanh dương cho icon "Xem"
                Pen pen = new Pen(Color.FromArgb(30, 136, 229), 2.5f); // ✅ Tăng độ dày nét vẽ
                Brush brush = new SolidBrush(Color.FromArgb(30, 136, 229));
                
                // ✅ Điều chỉnh vị trí và kích thước cho icon 22x22
                // Vẽ hình oval (mắt)
                g.DrawEllipse(pen, 3, 5, 16, 12);
                
                // Vẽ con ngươi
                g.FillEllipse(brush, 8, 9, 5, 5);
            }
            return bmp;
        }
    }
}
