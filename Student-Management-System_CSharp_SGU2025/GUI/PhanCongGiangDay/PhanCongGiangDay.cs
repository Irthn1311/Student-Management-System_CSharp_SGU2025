using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_;
using Student_Management_System_CSharp_SGU2025.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    public partial class PhanCongGiangDay : UserControl
    {
        private PhanCongGiangDayBUS phanCongBUS;
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;

        public PhanCongGiangDay()
        {
            InitializeComponent();
            phanCongBUS = new PhanCongGiangDayBUS();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
        }

        private void PhanCongGiangDay_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFilters();
                LoadStatCards();
                LoadData();

                ApplyPermissions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatCards()
        {
            try
            {
                // Lấy dữ liệu thống kê
                List<PhanCongGiangDayDTO> dsPhanCong = phanCongBUS.DocDSPhanCong();
                int tongPhanCong = dsPhanCong?.Count ?? 0;

                // Đếm giáo viên được phân công
                int tongGiaoVien = dsPhanCong?.Select(pc => pc.MaGiaoVien).Distinct().Count() ?? 0;

                // Đếm môn học được phân công
                int tongMonHoc = dsPhanCong?.Select(pc => pc.MaMonHoc).Distinct().Count() ?? 0;

                // Đếm lớp học có phân công
                int tongLopHoc = dsPhanCong?.Select(pc => pc.MaLop).Distinct().Count() ?? 0;

                // Cập nhật các card
                statCardPhanCongGiangDay1.Title = "Tổng phân công";
                statCardPhanCongGiangDay1.Value = tongPhanCong.ToString();
                statCardPhanCongGiangDay1.TitleColor = Color.FromArgb(30, 136, 229);

                statCardPhanCongGiangDay2.Title = "Giáo viên";
                statCardPhanCongGiangDay2.Value = tongGiaoVien.ToString();
                statCardPhanCongGiangDay2.TitleColor = Color.FromArgb(30, 136, 229);

                statCardPhanCongGiangDay3.Title = "Môn học";
                statCardPhanCongGiangDay3.Value = tongMonHoc.ToString();
                statCardPhanCongGiangDay3.TitleColor = Color.FromArgb(20, 163, 74);

                statCardPhanCongGiangDay4.Title = "Lớp học";
                statCardPhanCongGiangDay4.Value = tongLopHoc.ToString();
                statCardPhanCongGiangDay4.TitleColor = Color.FromArgb(234, 88, 12);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Filter Methods
        /// <summary>
        /// Load data cho các filter ComboBox
        /// </summary>
        private void LoadFilters()
        {
            try
            {
                // ✅ Load Học kỳ từ database với grouping theo năm học
                cbHocKyNamHoc.Items.Clear();
                cbHocKyNamHoc.Items.Add(new ComboBoxItem { Text = "Tất cả Học kỳ", Value = null });
                
                var dsHocKy = hocKyBUS.DocDSHocKy();
                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    // Group theo năm học (extract từ TenHocKy, ví dụ: "HK I - 2024-2025" -> "2024-2025")
                    var namHocGroups = dsHocKy
                        .Select(hk => ExtractNamHoc(hk.TenHocKy))
                        .Distinct()
                        .OrderByDescending(nh => nh)
                        .ToList();

                    foreach (var namHoc in namHocGroups)
                    {
                        if (!string.IsNullOrEmpty(namHoc))
                        {
                            // Thêm option "Cả năm"
                            cbHocKyNamHoc.Items.Add(new ComboBoxItem 
                            { 
                                Text = $"📅 Cả năm {namHoc}", 
                                Value = $"NAM_{namHoc}" // Đánh dấu là năm học
                            });

                            // Thêm từng học kỳ trong năm
                            var hocKyTrongNam = dsHocKy
                                .Where(hk => ExtractNamHoc(hk.TenHocKy) == namHoc)
                                .OrderBy(hk => hk.TenHocKy)
                                .ToList();

                            foreach (var hk in hocKyTrongNam)
                            {
                                cbHocKyNamHoc.Items.Add(new ComboBoxItem 
                                { 
                                    Text = $"   {hk.TenHocKy}", // Indent để dễ nhìn
                                    Value = hk.MaHocKy 
                                });
                            }
                        }
                    }
                }
                
                cbHocKyNamHoc.DisplayMember = "Text";
                cbHocKyNamHoc.SelectedIndex = 0;
                cbHocKyNamHoc.SelectedIndexChanged += FilterChanged;

                // ✅ Load Khối (10-12 THPT)
                cbKhoi.Items.Clear();
                cbKhoi.Items.Add("Tất cả Khối");
                cbKhoi.Items.Add("Khối 10");
                cbKhoi.Items.Add("Khối 11");
                cbKhoi.Items.Add("Khối 12");
                cbKhoi.SelectedIndex = 0;
                cbKhoi.SelectedIndexChanged += FilterChanged;

                // ✅ Load Lớp từ database
                LoadLopFilter();
                cbLop.SelectedIndexChanged += FilterChanged;

                // ✅ Load Môn học từ database
                cbMonHoc.Items.Clear();
                cbMonHoc.Items.Add("Tất cả môn");
                var dsMonHoc = monHocBUS.DocDSMH();
                if (dsMonHoc != null)
                {
                    foreach (var mh in dsMonHoc)
                    {
                        cbMonHoc.Items.Add(new ComboBoxItem { Text = mh.tenMon, Value = mh.maMon });
                    }
                }
                cbMonHoc.DisplayMember = "Text";
                cbMonHoc.SelectedIndex = 0;
                cbMonHoc.SelectedIndexChanged += FilterChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load filters: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load danh sách lớp (có thể filter theo khối)
        /// </summary>
        private void LoadLopFilter(int? khoiFilter = null)
        {
            try
            {
                cbLop.Items.Clear();
                cbLop.Items.Add("Tất cả lớp");

                var dsLop = lopHocBUS.DocDSLop() ;
                if (dsLop != null)
                {
                    foreach (var lop in dsLop)
                    {
                        // Filter theo khối nếu có
                        if (khoiFilter.HasValue)
                        {
                            // Lấy khối từ tên lớp (VD: "10A1" -> Khối 10)
                            string tenLop = lop.tenLop?.Trim() ?? "";
                            if (tenLop.Length > 0 && char.IsDigit(tenLop[0]))
                            {
                                string khoiStr = new string(tenLop.TakeWhile(char.IsDigit).ToArray());
                                if (int.TryParse(khoiStr, out int khoi) && khoi == khoiFilter.Value)
                                {
                                    cbLop.Items.Add(new ComboBoxItem { Text = lop.tenLop, Value = lop.maLop });
                                }
                            }
                        }
                        else
                        {
                            cbLop.Items.Add(new ComboBoxItem { Text = lop.tenLop, Value = lop.maLop });
                        }
                    }
                }
                
                cbLop.DisplayMember = "Text";
                cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi load lớp: {ex.Message}");
            }
        }

        /// <summary>
        /// Event handler khi filter thay đổi
        /// </summary>
        private void FilterChanged(object sender, EventArgs e)
        {
            // Nếu cbKhoi thay đổi, reload cbLop
            if (sender == cbKhoi)
            {
                if (cbKhoi.SelectedIndex > 0)
                {
                    string khoiText = cbKhoi.SelectedItem.ToString();
                    if (khoiText.Contains("Khối "))
                    {
                        if (int.TryParse(khoiText.Replace("Khối ", ""), out int khoi))
                        {
                            LoadLopFilter(khoi);
                        }
                    }
                }
                else
                {
                    LoadLopFilter(); // Load tất cả lớp
                }
            }

            // Reload data với filter mới
            LoadData();
        }

        /// <summary>
        /// Áp dụng filters vào danh sách phân công
        /// </summary>
        private List<PhanCongGiangDayDTO> ApplyFilters(List<PhanCongGiangDayDTO> dsPhanCong)
        {
            if (dsPhanCong == null || dsPhanCong.Count == 0)
                return dsPhanCong;

            try
            {
                var filtered = dsPhanCong.AsEnumerable();

                // Filter theo Học kỳ hoặc Năm học
                if (cbHocKyNamHoc != null && cbHocKyNamHoc.SelectedItem is ComboBoxItem hkItem)
                {
                    // Chỉ filter khi Value != null (không phải "Tất cả")
                    if (hkItem.Value != null)
                    {
                        string valueStr = hkItem.Value.ToString();
                        
                        if (valueStr.StartsWith("NAM_"))
                        {
                            // Filter theo CẢ NĂM HỌC
                            string namHoc = valueStr.Replace("NAM_", "");
                            filtered = filtered.Where(pc =>
                            {
                                var hocKy = hocKyBUS.LayHocKyTheoMa(pc.MaHocKy);
                                if (hocKy != null)
                                {
                                    string namHocCuaHK = ExtractNamHoc(hocKy.TenHocKy);
                                    return namHocCuaHK == namHoc;
                                }
                                return false;
                            });
                        }
                        else
                        {
                            // Filter theo HỌC KỲ cụ thể
                            int maHK = Convert.ToInt32(hkItem.Value);
                            filtered = filtered.Where(pc => pc.MaHocKy == maHK);
                        }
                    }
                    // Nếu Value == null (Tất cả) thì không filter, giữ nguyên filtered
                }

                // Filter theo Khối
                if (cbKhoi != null && cbKhoi.SelectedIndex > 0)
                {
                    string khoiText = cbKhoi.SelectedItem.ToString();
                    if (khoiText.Contains("Khối "))
                    {
                        if (int.TryParse(khoiText.Replace("Khối ", ""), out int khoi))
                        {
                            filtered = filtered.Where(pc =>
                            {
                                var lop = lopHocBUS.LayLopTheoId(pc.MaLop);
                                if (lop != null)
                                {
                                    string tenLop = lop.tenLop?.Trim() ?? "";
                                    if (tenLop.Length > 0 && char.IsDigit(tenLop[0]))
                                    {
                                        string khoiStr = new string(tenLop.TakeWhile(char.IsDigit).ToArray());
                                        return int.TryParse(khoiStr, out int lopKhoi) && lopKhoi == khoi;
                                    }
                                }
                                return false;
                            });
                        }
                    }
                }

                // Filter theo Lớp
                if (cbLop != null && cbLop.SelectedIndex > 0 && cbLop.SelectedItem is ComboBoxItem lopItem)
                {
                    int maLop = Convert.ToInt32(lopItem.Value);
                    filtered = filtered.Where(pc => pc.MaLop == maLop);
                }

                // Filter theo Môn học
                if (cbMonHoc != null && cbMonHoc.SelectedIndex > 0 && cbMonHoc.SelectedItem is ComboBoxItem monItem)
                {
                    int maMon = Convert.ToInt32(monItem.Value);
                    filtered = filtered.Where(pc => pc.MaMonHoc == maMon);
                }

                return filtered.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi apply filters: {ex.Message}");
                return dsPhanCong;
            }
        }

        /// <summary>
        /// Extract năm học từ tên học kỳ (VD: "HK I - 2024-2025" -> "2024-2025")
        /// </summary>
        private string ExtractNamHoc(string tenHocKy)
        {
            if (string.IsNullOrEmpty(tenHocKy))
                return null;

            // Pattern: "HK I - 2024-2025" hoặc "Học kỳ I - 2024-2025"
            var parts = tenHocKy.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                // Lấy phần cuối (năm học)
                var namHoc = parts[parts.Length - 1].Trim();
                
                // Kiểm tra format năm học (YYYY hoặc YYYY-YYYY)
                if (namHoc.Length >= 4 && char.IsDigit(namHoc[0]))
                {
                    return namHoc;
                }
            }

            return null;
        }

        /// <summary>
        /// Helper class cho ComboBox items
        /// </summary>
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        #endregion

        private void LoadData()
        {
            try
            {
                // Cấu hình DataGridView
                dgvPhanCong.Columns.Clear();
                dgvPhanCong.Rows.Clear();
                dgvPhanCong.AutoGenerateColumns = false;
                dgvPhanCong.AllowUserToAddRows = false;
                dgvPhanCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPhanCong.ReadOnly = true;

                // Thiết lập giao diện
                dgvPhanCong.BackgroundColor = Color.White;
                dgvPhanCong.BorderStyle = BorderStyle.None;
                dgvPhanCong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvPhanCong.RowHeadersVisible = false;

                // Style cho tiêu đề cột
                dgvPhanCong.EnableHeadersVisualStyles = false;
                dgvPhanCong.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvPhanCong.ColumnHeadersHeight = 50;
                dgvPhanCong.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                dgvPhanCong.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                dgvPhanCong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                dgvPhanCong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
                dgvPhanCong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Style cho các dòng dữ liệu
                dgvPhanCong.RowTemplate.Height = 45;
                dgvPhanCong.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
                dgvPhanCong.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
                dgvPhanCong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
                dgvPhanCong.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
                dgvPhanCong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPhanCong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

                // Tạo cột
                dgvPhanCong.Columns.Add("MaPhanCong", "Mã");
                dgvPhanCong.Columns["MaPhanCong"].Visible = false; // Ẩn cột mã

                dgvPhanCong.Columns.Add("GiaoVien", "Giáo viên");
                dgvPhanCong.Columns.Add("MonHoc", "Môn học");
                dgvPhanCong.Columns.Add("Lop", "Lớp");
                dgvPhanCong.Columns.Add("HocKy", "Học kỳ");
                dgvPhanCong.Columns.Add("ThoiGian", "Thời gian");
                dgvPhanCong.Columns.Add("ThaoTac", "Thao tác");

                // Thiết lập chế độ co giãn
                dgvPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPhanCong.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPhanCong.Columns["ThaoTac"].Width = 100;

                // Lấy dữ liệu từ database
                List<PhanCongGiangDayDTO> dsPhanCong = phanCongBUS.DocDSPhanCong();

                // ✅ Áp dụng filters
                if (dsPhanCong != null && dsPhanCong.Count > 0)
                {
                    dsPhanCong = ApplyFilters(dsPhanCong);
                }

                if (dsPhanCong != null && dsPhanCong.Count > 0)
                {
                    foreach (PhanCongGiangDayDTO pc in dsPhanCong)
                    {
                        // Lấy thông tin giáo viên
                        GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(pc.MaGiaoVien);
                        string tenGV = gv != null ? gv.HoTen : pc.MaGiaoVien;

                        // Lấy thông tin môn học
                        MonHocDTO mh = monHocBUS.LayDSMonHocTheoId(pc.MaMonHoc);
                        string tenMH = mh != null ? mh.tenMon : $"MH-{pc.MaMonHoc}";

                        // Lấy thông tin lớp
                        LopDTO lop = lopHocBUS.LayLopTheoId(pc.MaLop);
                        string tenLop = lop != null ? lop.tenLop : $"Lớp-{pc.MaLop}";

                        // Lấy thông tin học kỳ
                        HocKyDTO hk = hocKyBUS.LayHocKyTheoMa(pc.MaHocKy);
                        string tenHK = hk != null ? hk.TenHocKy : $"HK-{pc.MaHocKy}";

                        // Định dạng thời gian
                        string thoiGian = $"{pc.NgayBatDau:dd/MM/yyyy} - {pc.NgayKetThuc:dd/MM/yyyy}";

                        dgvPhanCong.Rows.Add(
                            pc.MaPhanCong,
                            tenGV,
                            tenMH,
                            tenLop,
                            tenHK,
                            thoiGian,
                            ""
                        );
                    }
                }

                // Gắn sự kiện
                dgvPhanCong.CellPainting += dgvPhanCong_CellPainting;
                dgvPhanCong.CellClick += dgvPhanCong_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu bảng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPhanCong_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Tô màu cho cột "Lớp"
            if (dgvPhanCong.Columns[e.ColumnIndex].Name == "Lop" && e.RowIndex >= 0)
            {
                string lopText = e.Value?.ToString();
                if (!string.IsNullOrEmpty(lopText))
                {
                    if (lopText.Contains("10"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246);
                    }
                    else if (lopText.Contains("11"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(34, 197, 94);
                    }
                    else if (lopText.Contains("12"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(249, 115, 22);
                    }
                }
            }

            // Vẽ icon trong cột "ThaoTac"
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPhanCong.Columns["ThaoTac"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                try
                {
                    // ✅ Lấy permission từ Tag
                    dynamic permissions = dgvPhanCong.Tag;
                    bool canDelete = permissions?.CanDelete ?? true;

                    Image editIcon = Properties.Resources.icon_eye;
                    Image deleteIcon = Properties.Resources.delete_icon;

                    int iconSize = 20;
                    int iconEyeSize = 26;
                    int padding = 6;

                    int xEdit = e.CellBounds.Left + padding;
                    int xDelete = xEdit + iconEyeSize + (4 * padding);
                    int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;
                    int yEye = e.CellBounds.Top + (e.CellBounds.Height - iconEyeSize) / 2;

                    // Vẽ icon eye (luôn hiển thị bình thường)
                    e.Graphics.DrawImage(editIcon, new Rectangle(xEdit, yEye, iconEyeSize, iconEyeSize));

                    // ✅ Vẽ icon delete (tô xám nếu không có quyền)
                    Rectangle deleteRect = new Rectangle(xDelete, y, iconSize, iconSize);
                    if (canDelete)
                    {
                        e.Graphics.DrawImage(deleteIcon, deleteRect);
                    }
                    else
                    {
                        DrawGrayScaleImage(e.Graphics, deleteIcon, deleteRect);
                    }
                }
                catch { }

                e.Handled = true;
            }
        }

        private void dgvPhanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPhanCong.Columns["ThaoTac"].Index)
            {
                var cell = dgvPhanCong.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = dgvPhanCong.PointToClient(Cursor.Position).X - cell.X;

                int iconEyeSize = 26;
                int padding = 6;

                int eyeRight = padding + iconEyeSize;
                int deleteLeft = eyeRight + (4 * padding);

                int maPhanCong = Convert.ToInt32(dgvPhanCong.Rows[e.RowIndex].Cells["MaPhanCong"].Value);
                string tenGV = dgvPhanCong.Rows[e.RowIndex].Cells["GiaoVien"].Value.ToString();

                if (x < eyeRight)
                {
                    // XEM CHI TIẾT - không cần quyền
                    XemChiTietPhanCong(maPhanCong);
                }
                else if (x > deleteLeft)
                {
                    // ✅ XÓA PHÂN CÔNG - kiểm tra quyền
                    if (!PermissionHelper.CheckDataGridIconPermission(dgvPhanCong, "delete", "Phân công giảng dạy"))
                        return;

                    XoaPhanCong(maPhanCong, tenGV, e.RowIndex);
                }
            }
        }

        private void XemChiTietPhanCong(int maPhanCong)
        {
            try
            {
                PhanCongGiangDayDTO pc = phanCongBUS.LayPhanCongTheoMa(maPhanCong);

                if (pc != null)
                {
                    // Lấy thông tin chi tiết
                    GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(pc.MaGiaoVien);
                    MonHocDTO mh = monHocBUS.LayDSMonHocTheoId(pc.MaMonHoc);
                    LopDTO lop = lopHocBUS.LayLopTheoId(pc.MaLop);
                    HocKyDTO hk = hocKyBUS.LayHocKyTheoMa(pc.MaHocKy);

                    string thongTin = $"📚 THÔNG TIN PHÂN CÔNG GIẢNG DẠY\n\n" +
                                    $"🔑 Mã phân công: {pc.MaPhanCong}\n" +
                                    $"👨‍🏫 Giáo viên: {(gv != null ? gv.HoTen : pc.MaGiaoVien)}\n" +
                                    $"📖 Môn học: {(mh != null ? mh.tenMon : $"MH-{pc.MaMonHoc}")}\n" +
                                    $"🏫 Lớp: {(lop != null ? lop.tenLop : $"Lớp-{pc.MaLop}")}\n" +
                                    $"📅 Học kỳ: {(hk != null ? hk.TenHocKy : $"HK-{pc.MaHocKy}")}\n" +
                                    $"📅 Ngày bắt đầu: {pc.NgayBatDau:dd/MM/yyyy}\n" +
                                    $"📅 Ngày kết thúc: {pc.NgayKetThuc:dd/MM/yyyy}";

                    MessageBox.Show(thongTin, "Chi tiết phân công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin phân công!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaPhanCong(int maPhanCong, string tenGV, int rowIndex)
        {
            try
            {
                string thongTinXoa = $"Bạn có chắc chắn muốn xóa phân công này?\n\n" +
                                    $"👨‍🏫 Giáo viên: {tenGV}\n" +
                                    $"🔑 Mã: {maPhanCong}\n\n" +
                                    $"⚠️ CẢNH BÁO:\n" +
                                    $"• Thao tác này sẽ xóa vĩnh viễn phân công\n" +
                                    $"• KHÔNG THỂ HOÀN TÁC sau khi xóa!\n\n" +
                                    $"Bạn có muốn tiếp tục?";

                DialogResult result = MessageBox.Show(
                    thongTinXoa,
                    "⚠️ Xác nhận xóa phân công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

                if (result == DialogResult.Yes)
                {
                    bool xoaThanhCong = phanCongBUS.XoaPhanCong(maPhanCong);

                    if (xoaThanhCong)
                    {
                        dgvPhanCong.Rows.RemoveAt(rowIndex);
                        LoadStatCards(); // Cập nhật thống kê

                        MessageBox.Show(
                            $"✓ Đã xóa phân công của '{tenGV}' thành công!",
                            "Xóa thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            $"✗ Không thể xóa phân công!\n\nVui lòng kiểm tra lại!",
                            "Lỗi xóa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi khi xóa phân công!\n\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                try { LoadData(); } catch { }
            }
        }

        //private void btnThemPhanCong_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (FrmThemPhanCongGiangDay frm = new FrmThemPhanCongGiangDay())
        //        {
        //            if (frm.ShowDialog() == DialogResult.OK)
        //            {
        //                LoadData();
        //                LoadStatCards();
        //                MessageBox.Show("Thêm phân công thành công!", "Thành công",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void dgvPhanCong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void panelShow_Paint(object sender, PaintEventArgs e) { }
        private void panelPhanCongGiangDay_Paint(object sender, PaintEventArgs e) { }

        private void btnPhanCongMoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLPHANCONG, "Phân công giảng dạy"))
                    return;
                using (FrmThemPhanCongGiangDay frm = new FrmThemPhanCongGiangDay())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        LoadStatCards();
                        MessageBox.Show("Thêm phân công thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAutoPhanCong_Click(object sender, EventArgs e)
        {
            try
            {
                if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLPHANCONG, "Phân công giảng dạy"))
                    return;
                // ✅ Gọi ShowDialog trực tiếp
                using (var frm = new Student_Management_System_CSharp_SGU2025.GUI.PhanCong.frmAutoPhanCongPreview())
                {
                    var result = frm.ShowDialog();
                    
                    // ✅ Reload CHUYÊN NGHIỆP khi xác nhận thành công
                    if (result == DialogResult.OK)
                    {
                        await ReloadAfterAutoAssignmentAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở Auto Phân công: {ex.Message}\n\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reload data một cách chuyên nghiệp với animation và notification
        /// </summary>
        private async Task ReloadAfterAutoAssignmentAsync()
        {
            Panel loadingPanel = null;
            try
            {
                // 1️⃣ Hiển thị loading overlay
                loadingPanel = CreateLoadingOverlay();
                this.Controls.Add(loadingPanel);
                loadingPanel.BringToFront();
                loadingPanel.Visible = true;

                // 2️⃣ Reload data asynchronously
                await Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(300); // Smooth transition
                });

                this.Invoke((MethodInvoker)delegate
                {
                    LoadData();
                    LoadStatCards();
                });

                // 3️⃣ Đóng loading
                if (loadingPanel != null)
                {
                    this.Controls.Remove(loadingPanel);
                    loadingPanel.Dispose();
                }

                // 4️⃣ Hiển thị notification đẹp
                ShowSuccessNotification("✅ Phân công đã được lưu và cập nhật thành công!");

                // 5️⃣ Auto scroll to top và highlight
                if (dgvPhanCong != null && dgvPhanCong.Rows.Count > 0)
                {
                    dgvPhanCong.ClearSelection();
                    dgvPhanCong.FirstDisplayedScrollingRowIndex = 0;
                    dgvPhanCong.Rows[0].Selected = true;
                    
                    // Smooth scroll animation
                    dgvPhanCong.Refresh();
                }
            }
            catch (Exception ex)
            {
                // Clean up loading panel nếu có lỗi
                if (loadingPanel != null && this.Controls.Contains(loadingPanel))
                {
                    this.Controls.Remove(loadingPanel);
                    loadingPanel.Dispose();
                }
                
                MessageBox.Show($"Lỗi reload data: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tạo panel loading overlay đẹp (không dùng transparent Form)
        /// </summary>
        private Panel CreateLoadingOverlay()
        {
            var overlay = new Panel
            {
                BackColor = Color.FromArgb(250, 250, 250), // Light gray thay vì transparent
                Dock = DockStyle.Fill,
                Visible = false
            };

            var loadingLabel = new Label
            {
                Text = "🔄 Đang cập nhật dữ liệu...",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(59, 130, 246),
                AutoSize = true,
                BackColor = Color.FromArgb(250, 250, 250) // Same as panel background
            };

            loadingLabel.Location = new Point(
                (this.Width - loadingLabel.PreferredWidth) / 2,
                (this.Height - loadingLabel.PreferredHeight) / 2
            );

            overlay.Controls.Add(loadingLabel);
            return overlay;
        }

        /// <summary>
        /// Hiển thị notification thành công đẹp với animation
        /// </summary>
        private async void ShowSuccessNotification(string message)
        {
            var notification = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(240, 253, 244), // Light green
                Size = new Size(450, 80),
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                Opacity = 0 // Bắt đầu từ transparent
            };

            // Rounded corners
            notification.Region = System.Drawing.Region.FromHrgn(
                CreateRoundRectRgn(0, 0, notification.Width, notification.Height, 12, 12));

            // Icon check đẹp
            var lblIcon = new Label
            {
                Text = "✅",
                Font = new Font("Segoe UI", 18F, FontStyle.Regular),
                AutoSize = true,
                BackColor = Color.FromArgb(240, 253, 244), // Same as notification background
                Location = new Point(20, 25)
            };

            var lblMessage = new Label
            {
                Text = message.Replace("✅ ", ""), // Bỏ icon vì đã có riêng
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(22, 163, 74), // Green
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(60, 0),
                Size = new Size(380, 80)
            };

            notification.Controls.Add(lblIcon);
            notification.Controls.Add(lblMessage);

            // Position: Bottom-right corner của form
            notification.Location = new Point(
                this.Location.X + this.Width - notification.Width - 30,
                this.Location.Y + this.Height - notification.Height - 80
            );

            notification.Show();

            // 🎬 Fade-in animation
            for (double opacity = 0; opacity <= 1; opacity += 0.1)
            {
                notification.Opacity = opacity;
                await Task.Delay(20);
            }

            // Auto close sau 2.5 giây với fade-out
            await Task.Delay(2500);

            // 🎬 Fade-out animation
            for (double opacity = 1; opacity >= 0; opacity -= 0.1)
            {
                notification.Opacity = opacity;
                await Task.Delay(20);
            }

            notification.Close();
        }

        // Import Windows API for rounded corners
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void btnNhapDeXuat_Click(object sender, EventArgs e)
        {
            try
            {
                var persist = new Student_Management_System_CSharp_SGU2025.Services.AssignmentPersistService();
                persist.AcceptToOfficial(1);
                LoadData();
                LoadStatCards();
                MessageBox.Show("Đã nhập từ đề xuất vào PhanCongGiangDay.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể nhập đề xuất: {ex.Message}");
            }
        }

        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Phân công giảng dạy
        /// </summary>
        private void ApplyPermissions()
        {
            try
            {
                // Kiểm tra quyền truy cập chức năng
                if (!PermissionHelper.HasAccessToFunction(PermissionHelper.QLPHANCONG))
                {
                    MessageBox.Show(
                        "Bạn không có quyền truy cập chức năng Quản lý phân công!",
                        "Không có quyền",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    this.Enabled = false;
                    return;
                }

                // Áp dụng phân quyền cho các button và DataGridView
                PermissionHelper.ApplyPermissionPhanCong(
                    btnPhanCongMoi,
                    btnAutoPhanCong,
                    dgvPhanCong
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Lỗi áp dụng phân quyền: {ex.Message}");
            }
        }

        /// <summary>
        /// ✅ Vẽ ảnh xám (sao chép từ DanhGia.cs)
        /// </summary>
        private void DrawGrayScaleImage(Graphics graphics, Image image, Rectangle rect)
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
                graphics.DrawImage(image, rect, 0, 0, image.Width, image.Height,
                    GraphicsUnit.Pixel, attributes);
            }
        }

    }
}