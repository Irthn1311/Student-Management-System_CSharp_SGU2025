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

namespace Student_Management_System_CSharp_SGU2025.GUI.GiaoVien
{
    public partial class GiaoVien : UserControl
    {
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private BindingList<GiaoVienDTO> bindingListGiaoVien;
        private List<GiaoVienDTO> danhSachGiaoVienFull;
        private List<MonHocDTO> danhSachMonHoc;

        public GiaoVien()
        {
            InitializeComponent();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            bindingListGiaoVien = new BindingList<GiaoVienDTO>();
            danhSachGiaoVienFull = new List<GiaoVienDTO>();
            danhSachMonHoc = new List<MonHocDTO>();
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

            // ====== CỘT ICON (Thao tác) ======
            var colView = new DataGridViewImageColumn()
            {
                Name = "View",
                HeaderText = "Thao tác",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
            };
            var colDel = new DataGridViewImageColumn()
            {
                Name = "Delete",
                HeaderText = "",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
            };

            tableGiaoVien.Columns.Add(colView);
            tableGiaoVien.Columns.Add(colDel);

            int viewColWidth = 70;
            int delColWidth = 34;

            tableGiaoVien.Columns["View"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableGiaoVien.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableGiaoVien.Columns["View"].Width = viewColWidth;
            tableGiaoVien.Columns["Delete"].Width = delColWidth;

            tableGiaoVien.Columns["View"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableGiaoVien.Columns["Delete"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableGiaoVien.Columns["View"].DefaultCellStyle.Padding = new Padding(6);
            tableGiaoVien.Columns["Delete"].DefaultCellStyle.Padding = new Padding(6);

            // Event
            tableGiaoVien.CellFormatting += tableGiaoVien_CellFormatting;
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
        private void HandleViewClick(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= bindingListGiaoVien.Count)
                return;

            var giaoVien = bindingListGiaoVien[rowIndex];
            if (giaoVien == null)
                return;

            // Mở form chi tiết/sửa giáo viên
            var formChiTiet = new ChinhSuaGiaoVien(giaoVien.MaGiaoVien);
            if (formChiTiet.ShowDialog() == DialogResult.OK)
            {
                // Reload dữ liệu sau khi sửa
                LoadData();
            }
        }

        private void HandleDelClick(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= bindingListGiaoVien.Count)
                return;

            var giaoVien = bindingListGiaoVien[rowIndex];
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
                    bool success = giaoVienBUS.XoaGiaoVien(giaoVien.MaGiaoVien);
                    if (success)
                    {
                        MessageBox.Show("Xóa giáo viên thành công!", "Thông báo",
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
                    MessageBox.Show($"Lỗi khi xóa giáo viên: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GiaoVien_Load(object sender, EventArgs e)
        {
            try
            {
                // Load danh sách môn học cho combobox
                LoadDanhSachMonHoc();

                // Load dữ liệu
                LoadData();

                // Gắn sự kiện tìm kiếm và lọc
                txtTimKiemGiaoVien.TextChanged += TxtTimKiemGiaoVien_TextChanged;
                cbBoMon.SelectedIndexChanged += CbBoMon_SelectedIndexChanged;
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
                ChuyenMon = !string.IsNullOrEmpty(gv.TenMonChuyenMon) ? gv.TenMonChuyenMon : "Chưa phân công",
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
                    null,
                    null
                );

                try
                {
                    tableGiaoVien.Rows[idx].Cells["View"].Value = Properties.Resources.icon_eye;
                    tableGiaoVien.Rows[idx].Cells["Delete"].Value = Properties.Resources.deleteicon;
                }
                catch (Exception ex)
                {
                    // Nếu không load được icon, bỏ qua
                    Console.WriteLine($"Không thể load icon: {ex.Message}");
                }
            });
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

        private void BtnThemGiaoVien_Click(object sender, EventArgs e)
        {
            var formThem = new ThemGiaoVien();
            if (formThem.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void tableGiaoVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string column = tableGiaoVien.Columns[e.ColumnIndex].Name;

            if (column == "View")
                HandleViewClick(e.RowIndex);
            else if (column == "Delete")
                HandleDelClick(e.RowIndex);
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
    }
}
