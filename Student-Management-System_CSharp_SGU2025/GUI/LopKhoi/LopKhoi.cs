using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_;
using System;
using System.Drawing;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class LopKhoi : UserControl
    {
        private LopHocBUS lopHocBUS;
        private GiaoVienBUS giaoVienBUS;
        private NamHocBUS namHocBUS;
        private List<LopDTO> danhSachLopGoc;
        private Dictionary<int, string> danhSachLopVoiNamHoc; // Lưu MaLop -> TenNamHoc
        private string namHocHienTai; // Lưu năm học đang được chọn
        private List<GiaoVienDTO> danhSachGiaoVien; // Danh sách giáo viên cho filter

        public LopKhoi()
        {
            InitializeComponent();
            lopHocBUS = new LopHocBUS();
            giaoVienBUS = new GiaoVienBUS();
            namHocBUS = new NamHocBUS();
            danhSachLopGoc = new List<LopDTO>();
            danhSachLopVoiNamHoc = new Dictionary<int, string>();
            danhSachGiaoVien = new List<GiaoVienDTO>();

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
            LoadTrangThaiComboBox();

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
            dgvLop.Columns.Add("NamHoc", "Năm học"); // ✅ Thêm cột năm học
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
            dgvLop.Columns["NamHoc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["NamHoc"].Width = 150; // Đặt độ rộng cố định cho cột năm học

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
                    // Lấy tất cả lớp kèm năm học
                    danhSachLopVoiNamHoc = lopHocBUS.DocDSLopVoiNamHoc();
                    // Lấy danh sách lớp từ DB
                    danhSachLopGoc = lopHocBUS.DocDSLop();
                }
                else
                {
                    // Lấy lớp theo năm học cụ thể
                    danhSachLopGoc = lopHocBUS.DocDSLopTheoNamHoc(namHocHienTai);
                    danhSachLopVoiNamHoc = new Dictionary<int, string>();
                    // Lấy tên năm học
                    List<NamHocDTO> dsNamHoc = namHocBUS.DocDSNamHoc();
                    NamHocDTO namHoc = dsNamHoc?.FirstOrDefault(nh => nh.MaNamHoc == namHocHienTai);
                    string tenNamHoc = namHoc?.TenNamHoc ?? "";
                    foreach (var lop in danhSachLopGoc)
                    {
                        danhSachLopVoiNamHoc[lop.maLop] = tenNamHoc;
                    }
                }
                // Áp dụng filter sau khi load
                ApDungFilter();
                CapNhatThongKeKhoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nạp dữ liệu lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ LOAD DANH SÁCH NĂM HỌC VÀO COMBOBOX
        private void LoadNamHocComboBox()
        {
            try
            {
                if (cbNamHoc == null) return;

                cbNamHoc.Items.Clear();
                cbNamHoc.Items.Add("Tất cả năm học"); // Option để xem tất cả lớp

                List<NamHocDTO> dsNamHoc = namHocBUS.DocDSNamHoc();
                if (dsNamHoc != null && dsNamHoc.Count > 0)
                {
                    foreach (NamHocDTO nh in dsNamHoc.OrderByDescending(n => n.NgayBD))
                    {
                        cbNamHoc.Items.Add(nh.TenNamHoc);
                    }
                }

                cbNamHoc.SelectedIndex = 0; // Chọn "Tất cả năm học" mặc định
                namHocHienTai = null;
                
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
                if (cbGiaoVien == null) return;

                cbGiaoVien.Items.Clear();
                cbGiaoVien.Items.Add("Tất cả GV");

                danhSachGiaoVien = giaoVienBUS.DocDSGiaoVien();
                if (danhSachGiaoVien != null && danhSachGiaoVien.Count > 0)
                {
                    foreach (GiaoVienDTO gv in danhSachGiaoVien.OrderBy(g => g.HoTen))
                    {
                        cbGiaoVien.Items.Add(gv.HoTen);
                    }
                }

                cbGiaoVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nạp danh sách giáo viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void LoadTrangThaiComboBox()
        {
            try
            {
                if (cbTrangThai == null) return;
                
                // Đảm bảo selectedIndex = 0 (Tất cả)
                if (cbTrangThai.Items.Count > 0)
                {
                    cbTrangThai.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo filter trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ XỬ LÝ KHI CHỌN NĂM HỌC
        private void cbNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbNamHoc == null || cbNamHoc.SelectedItem == null) return;

                string selectedText = cbNamHoc.SelectedItem.ToString();
                
                if (selectedText == "Tất cả năm học")
                {
                    namHocHienTai = null;
                }
                else
                {
                    // Lấy mã năm học từ tên năm học
                    List<NamHocDTO> dsNamHoc = namHocBUS.DocDSNamHoc();
                    NamHocDTO namHoc = dsNamHoc?.FirstOrDefault(nh => nh.TenNamHoc == selectedText);
                    namHocHienTai = namHoc?.MaNamHoc;
                }

                // Reload dữ liệu
                LoadData();
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
                    // Tìm và chọn "Tất cả năm học"
                    for (int i = 0; i < cbNamHoc.Items.Count; i++)
                    {
                        if (cbNamHoc.Items[i].ToString().Contains("Tất cả"))
                        {
                            cbNamHoc.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (cbGiaoVien != null && cbGiaoVien.Items.Count > 0)
                    cbGiaoVien.SelectedIndex = 0; // "Tất cả GV"

                if (cbSiSo != null && cbSiSo.Items.Count > 0)
                    cbSiSo.SelectedIndex = 0; // "Tất cả sĩ số"

                if (cbTrangThai != null && cbTrangThai.Items.Count > 0)
                    cbTrangThai.SelectedIndex = 0; // "Tất cả"

                // Áp dụng filter sau khi reset
                ApDungFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi reset filter: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ ÁP DỤNG TẤT CẢ FILTER VÀ TÌM KIẾM
        private void ApDungFilter()
        {
            try
            {
                // Đảm bảo dữ liệu đã được load
                if (danhSachLopGoc == null || danhSachLopGoc.Count == 0)
                {
                    LoadData();
                    return;
                }

                // Lấy dữ liệu gốc
                List<LopDTO> danhSachLoc = new List<LopDTO>(danhSachLopGoc);

                // Filter theo năm học (đã được xử lý trong LoadData)
                // Không cần filter lại ở đây vì đã được lọc trong LoadData

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

                // Filter theo tình trạng lớp (hiện tại chưa có trường TrangThai trong LopDTO, tạm thời bỏ qua)
                // Có thể thêm sau khi có trường TrangThai trong LopDTO
                // string selectedTrangThai = cbTrangThai?.SelectedItem?.ToString();
                // if (!string.IsNullOrEmpty(selectedTrangThai) && selectedTrangThai != "Tất cả")
                // {
                //     // Logic filter theo tình trạng (cần thêm trường TrangThai vào LopDTO)
                // }

                // Tìm kiếm theo text (tìm kiếm nâng cao)
                string searchText = txtSearch?.Text?.Trim()?.ToLower();
                if (!string.IsNullOrEmpty(searchText))
                {
                    danhSachLoc = danhSachLoc.Where(lop =>
                    {
                        // Tìm theo mã lớp
                        if (lop.maLop.ToString().Contains(searchText))
                            return true;

                        // Tìm theo tên lớp
                        if (lop.tenLop.ToLower().Contains(searchText))
                            return true;

                        // Tìm theo giáo viên chủ nhiệm
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

                // Lấy năm học của lớp
                string tenNamHoc = danhSachLopVoiNamHoc.ContainsKey(lop.maLop) 
                    ? danhSachLopVoiNamHoc[lop.maLop] 
                    : (string.IsNullOrEmpty(namHocHienTai) ? "N/A" : "");

                dgvLop.Rows.Add(lop.maLop, lop.tenLop, $"Khối {lop.maKhoi}", lop.siSo, tenGVCN, tenNamHoc, "Xem");
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

            Image editIcon = Properties.Resources.edit_icon;
            Image deleteIcon = Properties.Resources.delete_icon;

            int iconSize = 20;
            int spacing = 10;
            int totalWidth = iconSize * 2 + spacing;

            int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
            int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

            e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize));
            e.Graphics.DrawImage(deleteIcon, new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize));

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

            // ✅ CLICK ICON SỬA (truyền maLop để load, nhưng không cho sửa maLop trong form)
            if (clickPoint.X >= startX && clickPoint.X <= startX + iconSize)
            {
                SuaLopHoc frm = new SuaLopHoc(maLop); // Truyền mã lớp (tự động từ DB)
                frm.StartPosition = FormStartPosition.CenterParent;

                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadData(); // ✅ Reload dữ liệu và cập nhật thống kê (maLop giữ nguyên)
                }
            }
            // ✅ CLICK ICON XÓA
            else if (clickPoint.X >= startX + iconSize + spacing && clickPoint.X <= startX + iconSize * 2 + spacing)
            {
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
                            LoadData(); // ✅ Reload dữ liệu và cập nhật thống kê
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

        private void statCardKhoi3_Load(object sender, EventArgs e)
        {

        }
    }
}