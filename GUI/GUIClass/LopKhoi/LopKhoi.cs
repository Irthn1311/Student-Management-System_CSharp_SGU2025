using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI;
using Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class LopKhoi : UserControl
    {
        private LopHocBUS lopHocBUS;
        private GiaoVienBUS giaoVienBUS;
        private NamHocBUS namHocBUS;
        private List<LopDTO> danhSachLopGoc;
        private List<NamHocDTO> danhSachNamHoc;
        private bool dangNapNamHoc;
        private string namHocHienTai; // Lưu năm học đang được chọn
        private List<GiaoVienDTO> danhSachGiaoVien; // Danh sách giáo viên cho filter

        public LopKhoi()
        {
            InitializeComponent();
            lopHocBUS = new LopHocBUS();
            giaoVienBUS = new GiaoVienBUS();
            namHocBUS = new NamHocBUS();
            danhSachLopGoc = new List<LopDTO>();
            danhSachGiaoVien = new List<GiaoVienDTO>();
            danhSachNamHoc = new List<NamHocDTO>();

            // Gắn sự kiện
            this.Load += LopKhoi_Load;
            SetupDataGridView();
        }

        private void LopKhoi_Load(object sender, EventArgs e)
        {
            if (dgvLop == null) return;

            // --- Load danh sách năm học vào dropdown ---
            LoadNamHocComboBox();
            
            // --- Load danh sách giáo viên cho filter ---
            LoadGiaoVienComboBox();
            
            // --- Load danh sách sĩ số cho filter ---
            LoadSiSoComboBox();
            
            // --- Khởi tạo filter trạng thái ---
            //LoadTrangThaiComboBox();

            // --- Cập nhật thống kê ---
            CapNhatThongKeKhoi();

            // SỬ DỤNG PROPERTY MỚI ĐỂ THAY ĐỔI MÀU
            statCardKhoi1.PanelColor = Color.FromArgb(59, 130, 246);
            statCardKhoi1.TextColor = Color.White;

            statCardKhoi2.PanelColor = Color.FromArgb(34, 197, 94);
            statCardKhoi2.TextColor = Color.White;

            statCardKhoi3.PanelColor = Color.FromArgb(249, 115, 22);
            statCardKhoi3.TextColor = Color.White;

            // ✅ GẮN SỰ KIỆN CLICK CHO CÁC STAT CARD
            statCardKhoi1.Click += StatCardKhoi1_Click;
            statCardKhoi2.Click += StatCardKhoi2_Click;
            statCardKhoi3.Click += StatCardKhoi3_Click;

            // ✅ Nếu statCard có panel con, cần gắn sự kiện cho tất cả controls
            GanSuKienClickChoTatCaControl(statCardKhoi1, StatCardKhoi1_Click);
            GanSuKienClickChoTatCaControl(statCardKhoi2, StatCardKhoi2_Click);
            GanSuKienClickChoTatCaControl(statCardKhoi3, StatCardKhoi3_Click);

            // --- Cấu hình & nạp dữ liệu ---
            LoadData();

            // --- Gắn sự kiện ---
            dgvLop.CellPainting += dgvLop_CellPainting;
            dgvLop.CellClick += dgvLop_CellClick;
            PermissionHelper.ApplyPermissionLopHoc(btnThem, dgvLop);
            
            // 🆕 Thêm button "Quản lý yêu cầu chuyển lớp" cho ADMIN
            ThemButtonQuanLyYeuCau();
        }

        // ✅ HÀM HỖ TRỢ: Gắn sự kiện click cho tất cả controls con
        private void GanSuKienClickChoTatCaControl(Control parent, EventHandler clickHandler)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Click += clickHandler;
                if (ctrl.HasChildren)
                {
                    GanSuKienClickChoTatCaControl(ctrl, clickHandler);
                }
            }
        }

        // ✅ SỰ KIỆN CLICK CHO KHỐI 10
        private void StatCardKhoi1_Click(object sender, EventArgs e)
        {
            LocTheoKhoi(10);
            guna2ComboBox1.SelectedIndex = 1; // Set ComboBox về "Khối 10"
        }

        // ✅ SỰ KIỆN CLICK CHO KHỐI 11
        private void StatCardKhoi2_Click(object sender, EventArgs e)
        {
            LocTheoKhoi(11);
            guna2ComboBox1.SelectedIndex = 2; // Set ComboBox về "Khối 11"
        }

        // ✅ SỰ KIỆN CLICK CHO KHỐI 12
        private void StatCardKhoi3_Click(object sender, EventArgs e)
        {
            LocTheoKhoi(12);
            guna2ComboBox1.SelectedIndex = 3; // Set ComboBox về "Khối 12"
        }

        private void SetupDataGridView()
        {
            dgvLop.Columns.Clear();
            dgvLop.Rows.Clear();

            dgvLop.Columns.Add("MaLop", "Mã lớp");
            dgvLop.Columns.Add("TenLop", "Tên lớp");
            dgvLop.Columns.Add("Khoi", "Khối");
            dgvLop.Columns.Add("SiSo", "Sĩ số");
            dgvLop.Columns.Add("GVCN", "Giáo viên CN");
            dgvLop.Columns.Add("XemChiTiet", "Chi tiết"); // ✅ Thêm cột xem chi tiết
            dgvLop.Columns.Add("ThaoTac", "Thao tác");

            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvLop.Columns["ThaoTac"].Width = 60;
            dgvLop.Columns["ThaoTac"].Resizable = DataGridViewTriState.False;
            dgvLop.Columns["XemChiTiet"].Width = 80;
            dgvLop.Columns["XemChiTiet"].Resizable = DataGridViewTriState.False;

            dgvLop.ColumnHeadersHeight = 50;

            dgvLop.Columns["MaLop"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["TenLop"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["Khoi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["SiSo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["GVCN"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvLop.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvLop.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgvLop.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLop.EnableHeadersVisualStyles = false;
            dgvLop.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 102, 204);

            dgvLop.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvLop.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvLop.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvLop.RowTemplate.Height = 40;
            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLop.AllowUserToAddRows = false;
            dgvLop.ReadOnly = true;
        }

        // ✅ LOAD DỮ LIỆU: maLop được tự động sinh từ DB (auto-increment/trigger)
        private void LoadData()
        {
            try
            {
                // Lấy lớp theo năm học đã chọn (nếu có)
                if (string.IsNullOrEmpty(namHocHienTai))
                {
                    danhSachLopGoc = lopHocBUS.DocDSLop();
                }
                else
                {
                    danhSachLopGoc = lopHocBUS.DocDSLopTheoNamHoc(namHocHienTai);
                }

                // Đảm bảo luôn có list (không null)
                if (danhSachLopGoc == null)
                {
                    danhSachLopGoc = new List<LopDTO>();
                }

                // Áp dụng filter sau khi load
                ApDungFilter();
                CapNhatThongKeKhoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nạp dữ liệu lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Khởi tạo danh sách rỗng để tránh null                
                danhSachLopGoc = new List<LopDTO>();
                HienThiDanhSachLop(danhSachLopGoc);
            }
        }

        // ✅ LOAD DANH SÁCH NĂM HỌC VÀO COMBOBOX
        private void LoadNamHocComboBox()
        {
            try
            {
                if (cbNamHoc == null) return;

                dangNapNamHoc = true;

                cbNamHoc.Items.Clear();
                cbNamHoc.DisplayMember = "Text";
                cbNamHoc.ValueMember = "Value";
                cbNamHoc.Items.Add(new NamHocComboItem("Tất cả năm học", null)); // Option để xem tất cả lớp

                danhSachNamHoc = namHocBUS.DocDSNamHoc() ?? new List<NamHocDTO>();
                var danhSachSapXep = danhSachNamHoc.OrderByDescending(n => n.NgayBD).ToList();

                foreach (NamHocDTO nh in danhSachSapXep)
                {
                    cbNamHoc.Items.Add(new NamHocComboItem(nh.TenNamHoc, nh.MaNamHoc));
                }

                int indexMacDinh = 0;
                NamHocDTO namHocHienThoi = danhSachSapXep
                    .FirstOrDefault(nh => nh.NgayBD.Date <= DateTime.Today && nh.NgayKT.Date >= DateTime.Today);

                if (namHocHienThoi != null)
                {
                    for (int i = 1; i < cbNamHoc.Items.Count; i++)
                    {
                        if (((NamHocComboItem)cbNamHoc.Items[i]).Value == namHocHienThoi.MaNamHoc)
                        {
                            indexMacDinh = i;
                            break;
                        }
                    }
                }

                cbNamHoc.SelectedIndex = indexMacDinh;
                var item = cbNamHoc.SelectedItem as NamHocComboItem;
                namHocHienTai = item?.Value;

                dangNapNamHoc = false;

                // Load dữ liệu sau khi load combobox
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nạp danh sách năm học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ LOAD DANH SÁCH GIÁO VIÊN CHO FILTER
        private void LoadGiaoVienComboBox()
        {
            try
            {
                if (cbGiaoVien == null)
                {
                    MessageBox.Show("ComboBox giáo viên chưa được khởi tạo!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cbGiaoVien.Items.Clear();
                cbGiaoVien.Items.Add("Tất cả GV");

                // Lấy danh sách giáo viên từ BUS
                danhSachGiaoVien = giaoVienBUS.DocDSGiaoVien();
                
                if (danhSachGiaoVien == null || danhSachGiaoVien.Count == 0)
                {
                    cbGiaoVien.SelectedIndex = 0;
                    return;
                }

                // Lọc và sắp xếp giáo viên
                var dsGiaoVienHopLe = danhSachGiaoVien
                    .Where(gv => !string.IsNullOrWhiteSpace(gv.HoTen))
                    .OrderBy(gv => gv.HoTen)
                    .ToList();

                // Thêm từng giáo viên vào ComboBox
                foreach (GiaoVienDTO gv in dsGiaoVienHopLe)
                {
                    cbGiaoVien.Items.Add(gv.HoTen);
                }

                cbGiaoVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nạp danh sách giáo viên:\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ LOAD DANH SÁCH SĨ SỐ CHO FILTER
        private void LoadSiSoComboBox()
        {
            try
            {
                if (cbSiSo == null) return;

                cbSiSo.Items.Clear();
                cbSiSo.Items.Add("Tất cả sĩ số");
                cbSiSo.Items.Add("Dưới 30");
                cbSiSo.Items.Add("30 - 40");
                cbSiSo.Items.Add("41 - 50");
                cbSiSo.Items.Add("Trên 50");

                cbSiSo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nạp danh sách sĩ số: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ KHỞI TẠO FILTER TRẠNG THÁI (tạm thời chưa sử dụng vì chưa có trường TrangThai trong LopDTO)
        //private void LoadTrangThaiComboBox()
        //{
        //    try
        //    {
        //        if (cbTrangThai == null) return;
                
        //        // Đảm bảo selectedIndex = 0 (Tất cả)
        //        if (cbTrangThai.Items.Count > 0)
        //        {
        //            cbTrangThai.SelectedIndex = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi khởi tạo filter trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // ✅ XỬ LÝ KHI CHỌN NĂM HỌC
        private void cbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Bỏ qua khi đang nạp dữ liệu vào ComboBox
                if (dangNapNamHoc) return;

                // Kiểm tra ComboBox và item đã chọn
                if (cbNamHoc == null || cbNamHoc.SelectedIndex < 0) return;

                // Lấy item đã chọn
                var selectedItem = cbNamHoc.SelectedItem as NamHocComboItem;
                if (selectedItem == null) return;

                // Cập nhật năm học hiện tại
                namHocHienTai = selectedItem.Value;

                // Reload dữ liệu lớp học theo năm học đã chọn
                LoadData();

                // Cập nhật thống kê
                CapNhatThongKeKhoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc lớp theo năm học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ XỬ LÝ TÌM KIẾM
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApDungFilter();
        }

        // ✅ XỬ LÝ KHI CHỌN FILTER GIÁO VIÊN
        private void cbGiaoVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApDungFilter();
        }

        // ✅ XỬ LÝ KHI CHỌN FILTER SĨ SỐ
        private void cbSiSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApDungFilter();
        }

        // ✅ XỬ LÝ KHI CHỌN FILTER TRẠNG THÁI
        private void cbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApDungFilter();
        }

        // ✅ XỬ LÝ NÚT BỎ CHỌN TẤT CẢ
        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset tất cả filters về mặc định
                if (txtSearch != null)
                    txtSearch.Text = "";

                if (guna2ComboBox1 != null)
                    guna2ComboBox1.SelectedIndex = 0; // "Tất cả khối"

                if (cbNamHoc != null && cbNamHoc.Items.Count > 0)
                {
                    cbNamHoc.SelectedIndex = 0; // "Tất cả năm học"
                }

                if (cbGiaoVien != null && cbGiaoVien.Items.Count > 0)
                    cbGiaoVien.SelectedIndex = 0; // "Tất cả GV"

                if (cbSiSo != null && cbSiSo.Items.Count > 0)
                    cbSiSo.SelectedIndex = 0; // "Tất cả sĩ số"

                //if (cbTrangThai != null && cbTrangThai.Items.Count > 0)
                //    cbTrangThai.SelectedIndex = 0; // "Tất cả"

                //// Áp dụng filter sau khi reset
                ApDungFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi reset filter: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class NamHocComboItem
        {
            public NamHocComboItem(string text, string value)
            {
                Text = text;
                Value = value;
            }

            public string Text { get; }
            public string Value { get; }

            public override string ToString() => Text;
        }

        // ✅ ÁP DỤNG TẤT CẢ FILTER VÀ TÌM KIẾM
        private void ApDungFilter()
        {
            try
            {
                // QUAN TRỌNG: Không gọi LoadData() ở đây để tránh vòng lặp vô hạn
                if (danhSachLopGoc == null)
                {
                    danhSachLopGoc = new List<LopDTO>();
                }

                // Nếu danh sách rỗng, chỉ hiển thị rỗng
                if (danhSachLopGoc.Count == 0)
                {
                    HienThiDanhSachLop(new List<LopDTO>());
                    return;
                }

                // Lấy dữ liệu gốc
                List<LopDTO> danhSachLoc = new List<LopDTO>(danhSachLopGoc);

                // Filter theo khối
                string selectedKhoi = guna2ComboBox1?.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedKhoi) && selectedKhoi != "Tất cả khối")
                {
                    int maKhoi = int.Parse(selectedKhoi.Replace("Khối ", ""));
                    danhSachLoc = danhSachLoc.Where(l => l.maKhoi == maKhoi).ToList();
                }

                // Filter theo giáo viên chủ nhiệm
                string selectedGV = cbGiaoVien?.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedGV) && selectedGV != "Tất cả GV")
                {
                    GiaoVienDTO gv = danhSachGiaoVien?.FirstOrDefault(g => g.HoTen == selectedGV);
                    if (gv != null)
                    {
                        danhSachLoc = danhSachLoc.Where(l => l.maGVCN == gv.MaGiaoVien).ToList();
                    }
                }

                // Filter theo sĩ số
                string selectedSiSo = cbSiSo?.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedSiSo) && selectedSiSo != "Tất cả sĩ số")
                {
                    switch (selectedSiSo)
                    {
                        case "Dưới 30":
                            danhSachLoc = danhSachLoc.Where(l => l.siSo < 30).ToList();
                            break;
                        case "30 - 40":
                            danhSachLoc = danhSachLoc.Where(l => l.siSo >= 30 && l.siSo <= 40).ToList();
                            break;
                        case "41 - 50":
                            danhSachLoc = danhSachLoc.Where(l => l.siSo >= 41 && l.siSo <= 50).ToList();
                            break;
                        case "Trên 50":
                            danhSachLoc = danhSachLoc.Where(l => l.siSo > 50).ToList();
                            break;
                    }
                }

                // Tìm kiếm theo text
                string searchText = txtSearch?.Text?.Trim()?.ToLower();
                if (!string.IsNullOrEmpty(searchText))
                {
                    danhSachLoc = danhSachLoc.Where(lop =>
                    {
                        if (lop.maLop.ToString().Contains(searchText))
                            return true;

                        if (lop.tenLop.ToLower().Contains(searchText))
                            return true;

                        if (!string.IsNullOrEmpty(lop.maGVCN))
                        {
                            try
                            {
                                string tenGV = giaoVienBUS.LayTenGiaoVienTheoMa(lop.maGVCN);
                                if (!string.IsNullOrEmpty(tenGV) && tenGV.ToLower().Contains(searchText))
                                    return true;
                            }
                            catch { }
                        }

                        return false;
                    }).ToList();
                }

                // Hiển thị kết quả
                HienThiDanhSachLop(danhSachLoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi áp dụng filter: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ HIỂN THỊ: Hiển thị maLop tự động từ DB
        private void HienThiDanhSachLop(List<LopDTO> danhSach)
        {
            dgvLop.Rows.Clear();

            foreach (LopDTO lop in danhSach)
            {
                string tenGVCN = "Chưa phân công";

                if (!string.IsNullOrEmpty(lop.maGVCN))
                {
                    try
                    {
                        string ten = giaoVienBUS.LayTenGiaoVienTheoMa(lop.maGVCN);
                        if (!string.IsNullOrEmpty(ten))
                        {
                            tenGVCN = ten;
                        }
                        else
                        {
                            tenGVCN = $"Không tìm thấy ({lop.maGVCN})";
                        }
                    }
                    catch
                    {
                        tenGVCN = $"Lỗi ({lop.maGVCN})";
                    }
                }

                dgvLop.Rows.Add(lop.maLop, lop.tenLop, $"Khối {lop.maKhoi}", lop.siSo, tenGVCN, "Xem", "");
            }
        }

        private void LocTheoKhoi(int? maKhoi)
        {
            // Chỉ cần set combobox, ApDungFilter sẽ xử lý
            if (maKhoi == null)
            {
                guna2ComboBox1.SelectedIndex = 0; // "Tất cả khối"
            }
            else
            {
                guna2ComboBox1.SelectedIndex = maKhoi.Value - 9; // Khối 10 = index 1, Khối 11 = index 2, etc.
            }
            ApDungFilter();
        }

        // ✅ CẬP NHẬT THỐNG KÊ KHỐI
        private void CapNhatThongKeKhoi()
        {
            try
            {
                var ds = danhSachLopGoc ?? new List<LopDTO>();
                int soLopKhoi10 = ds.Count(l => l.maKhoi == 10);
                int soLopKhoi11 = ds.Count(l => l.maKhoi == 11);
                int soLopKhoi12 = ds.Count(l => l.maKhoi == 12);

                int siSoKhoi10 = ds.Where(l => l.maKhoi == 10).Sum(l => l.siSo);
                int siSoKhoi11 = ds.Where(l => l.maKhoi == 11).Sum(l => l.siSo);
                int siSoKhoi12 = ds.Where(l => l.maKhoi == 12).Sum(l => l.siSo);

                statCardKhoi1.SetData("Khối 10", $"{soLopKhoi10} lớp", $"{siSoKhoi10} học sinh");
                statCardKhoi2.SetData("Khối 11", $"{soLopKhoi11} lớp", $"{siSoKhoi11} học sinh");
                statCardKhoi3.SetData("Khối 12", $"{soLopKhoi12} lớp", $"{siSoKhoi12} học sinh");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLop_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvLop.Columns["ThaoTac"].Index)
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            // ✅ Lấy thông tin quyền từ Tag - Sử dụng cách an toàn hơn
            bool canUpdate = true; // Mặc định true
            bool canDelete = true; // Mặc định true
            
            if (dgvLop.Tag != null)
            {
                try
                {
                    dynamic permissions = dgvLop.Tag;
                    canUpdate = permissions?.CanUpdate ?? true;
                    canDelete = permissions?.CanDelete ?? true;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    // Nếu Tag không có thuộc tính CanUpdate/CanDelete, giữ giá trị mặc định
                    canUpdate = true;
                    canDelete = true;
                }
            }

            Image editIcon = Properties.Resources.edit_icon;
            Image deleteIcon = Properties.Resources.delete_icon;

            int iconSize = 20;
            int spacing = 10;
            int startX = e.CellBounds.Left + (e.CellBounds.Width - iconSize * 2 - spacing) / 2;
            int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

            // ✅ Vẽ icon Edit (với opacity nếu không có quyền)
            if (canUpdate)
            {
                e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize));
            }
            else
            {
                // Vẽ icon mờ (disabled)
                using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                {
                    float[][] matrixItems = {
                new float[] {0.3f, 0, 0, 0, 0},
                new float[] {0, 0.3f, 0, 0, 0},
                new float[] {0, 0, 0.3f, 0, 0},
                new float[] {0, 0, 0, 0.3f, 0},
                new float[] {0.5f, 0.5f, 0.5f, 0, 1}
            };
                    var colorMatrix = new System.Drawing.Imaging.ColorMatrix(matrixItems);
                    attributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default,
                                             System.Drawing.Imaging.ColorAdjustType.Bitmap);
                    e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize),
                                        0, 0, editIcon.Width, editIcon.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            // ✅ Vẽ icon Delete (với opacity nếu không có quyền)
            int deleteX = startX + iconSize + spacing;
            if (canDelete)
            {
                e.Graphics.DrawImage(deleteIcon, new Rectangle(deleteX, y, iconSize, iconSize));
            }
            else
            {
                // Vẽ icon mờ (disabled)
                using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                {
                    float[][] matrixItems = {
                new float[] {0.3f, 0, 0, 0, 0},
                new float[] {0, 0.3f, 0, 0, 0},
                new float[] {0, 0, 0.3f, 0, 0},
                new float[] {0, 0, 0, 0.3f, 0},
                new float[] {0.5f, 0.5f, 0.5f, 0, 1}
            };
                    var colorMatrix = new System.Drawing.Imaging.ColorMatrix(matrixItems);
                    attributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default,
                                             System.Drawing.Imaging.ColorAdjustType.Bitmap);
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(deleteX, y, iconSize, iconSize),
                                        0, 0, deleteIcon.Width, deleteIcon.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            e.Handled = true;
        }

        // ✅ XỬ LÝ CLICK ICON - SỬA, XÓA VÀ XEM CHI TIẾT
        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int maLop = Convert.ToInt32(dgvLop.Rows[e.RowIndex].Cells["MaLop"].Value);
            string tenLop = dgvLop.Rows[e.RowIndex].Cells["TenLop"].Value.ToString();

            // Xử lý click vào cột "Xem chi tiết"
            if (e.ColumnIndex == dgvLop.Columns["XemChiTiet"].Index)
            {
                XemChiTietLop(maLop);
                return;
            }

            // Xử lý click vào cột "Thao tác"
            if (e.ColumnIndex != dgvLop.Columns["ThaoTac"].Index)
                return;

            Point clickPoint = dgvLop.PointToClient(Cursor.Position);
            Rectangle cellRect = dgvLop.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            int iconSize = 18;
            int spacing = 10;
            int totalWidth = iconSize * 2 + spacing;
            int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;


            // ✅ CLICK ICON SỬA
            if (clickPoint.X >= startX && clickPoint.X <= startX + iconSize)
            {
                // ✅ Kiểm tra quyền UPDATE
                if (!PermissionHelper.CheckDataGridIconPermission(dgvLop, "edit", "Quản lý lớp học"))
                    return;

                SuaLopHoc frm = new SuaLopHoc(maLop);
                frm.StartPosition = FormStartPosition.CenterParent;

                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
            // ✅ CLICK ICON XÓA
            else if (clickPoint.X >= startX + iconSize + spacing && clickPoint.X <= startX + iconSize * 2 + spacing)
            {
                // ✅ Kiểm tra quyền DELETE
                if (!PermissionHelper.CheckDataGridIconPermission(dgvLop, "delete", "Quản lý lớp học"))
                    return;

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc muốn xóa lớp '{tenLop}'?\n\nLưu ý: Thao tác này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        bool kq = lopHocBUS.XoaLop(maLop);

                        if (kq)
                        {
                            MessageBox.Show("Xóa lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Xóa lớp học thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa lớp học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ✅ XEM CHI TIẾT LỚP
        private void XemChiTietLop(int maLop)
        {
            try
            {
                // Tạo form chi tiết lớp
                ChiTietLop frmChiTiet = new ChiTietLop(maLop);
                frmChiTiet.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở chi tiết lớp: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statCardKhoi10_Load(object sender, EventArgs e)
        {

        }

        // ✅ THÊM MỚI: Không cần nhập maLop (DB tự sinh), reload để hiển thị maLop mới
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLLOPHOC, "Quản lý lớp học"))
                return;
            ThemLopHoc formThem = new ThemLopHoc(); // Form chỉ nhập tenLop, maKhoi, maGVCN (maLop tự động)

            DialogResult result = formThem.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadData(); // ✅ Reload và cập nhật thống kê, hiển thị maLop mới từ DB

                // Debug: Kiểm tra maLop mới nhất (có thể xóa sau khi test)
                var lopMoiNhat = danhSachLopGoc.OrderByDescending(l => l.maLop).FirstOrDefault();
                if (lopMoiNhat != null)
                {
                    // Console.WriteLine($"Mã lớp mới tự động: {lopMoiNhat.maLop}"); // Hoặc log vào file/debug
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApDungFilter();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LopKhoi_Load_1(object sender, EventArgs e)
        {

        }

        private void dgvLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 🆕 Thêm button "Quản lý yêu cầu chuyển lớp" cho ADMIN
        /// </summary>
        private void ThemButtonQuanLyYeuCau()
        {
            try
            {
                // Tạo button mới
                Guna2Button btnQuanLyYeuCau = new Guna2Button();
                btnQuanLyYeuCau.Text = "📋 Yêu cầu chuyển lớp";
                btnQuanLyYeuCau.Size = new Size(180, 40);
                btnQuanLyYeuCau.FillColor = Color.FromArgb(139, 92, 246); // Màu tím
                btnQuanLyYeuCau.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                btnQuanLyYeuCau.ForeColor = Color.White;
                btnQuanLyYeuCau.BorderRadius = 8;
                btnQuanLyYeuCau.Cursor = Cursors.Hand;

                // Đặt vị trí button (bên cạnh button "Thêm")
                if (btnThem != null)
                {
                    btnQuanLyYeuCau.Location = new Point(btnThem.Location.X + btnThem.Width + 10, btnThem.Location.Y);
                }
                else
                {
                    btnQuanLyYeuCau.Location = new Point(30, 20);
                }

                // Gắn sự kiện click
                btnQuanLyYeuCau.Click += BtnQuanLyYeuCau_Click;

                // Thêm button vào form
                this.Controls.Add(btnQuanLyYeuCau);
                btnQuanLyYeuCau.BringToFront();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm button Quản lý yêu cầu: {ex.Message}");
            }
        }

        /// <summary>
        /// 🆕 Event khi click button "Quản lý yêu cầu chuyển lớp"
        /// </summary>
        private void BtnQuanLyYeuCau_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tên đăng nhập admin hiện tại
                // string tenDangNhapAdmin = PermissionHelper.GetCurrentUsername();
                // Sửa: Nếu bạn có một biến lưu username, hãy dùng nó. Nếu không, cần truyền username từ nơi khác.
                string tenDangNhapAdmin = Environment.UserName; // Hoặc lấy từ biến/thuộc tính hiện có

                if (string.IsNullOrEmpty(tenDangNhapAdmin))
                {
                    MessageBox.Show("Không xác định được người dùng hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mở form quản lý yêu cầu chuyển lớp
                FormQuanLyYeuCauChuyenLop form = new FormQuanLyYeuCauChuyenLop(tenDangNhapAdmin);
                form.ShowDialog();

                // Reload dữ liệu sau khi đóng form (nếu có thay đổi)
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý yêu cầu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}