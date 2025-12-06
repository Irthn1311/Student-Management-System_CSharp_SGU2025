using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.BUS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    /// <summary>
    /// ViewModel cho hiển thị phân công giảng dạy trên DataGridView
    /// </summary>
    public class PhanCongGiangDayViewModel
    {
        public int MaPhanCong { get; set; }
        public string GiaoVien { get; set; }
        public string MonHoc { get; set; }
        public string Lop { get; set; }
        public string HocKy { get; set; }
        public string ThoiGian { get; set; }
        public string ThaoTac { get; set; } = "";
        
        // Lưu các mã gốc để dùng khi cần
        public string MaGiaoVien { get; set; }
        public int MaMonHoc { get; set; }
        public int MaLop { get; set; }
        public int MaHocKy { get; set; }
    }

    public partial class PhanCongGiangDay : UserControl
    {
        private PhanCongGiangDayBUS phanCongBUS;
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;
        
        // BindingList để bind vào DataGridView
        private BindingList<PhanCongGiangDayViewModel> bindingList;

        public PhanCongGiangDay()
        {
            InitializeComponent();
            phanCongBUS = new PhanCongGiangDayBUS();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
        }

        // Nút "Sinh Dữ Liệu Test" cho SeedingService
        private Guna.UI2.WinForms.Guna2Button btnSeedData;

        private void PhanCongGiangDay_Load(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo nút Seed Data
                InitializeSeedDataButton();

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

        /// <summary>
        /// Khởi tạo nút "Sinh Dữ Liệu Test" cho SeedingService (ẩn, chỉ dùng cho testing)
        /// </summary>
        private void InitializeSeedDataButton()
        {
            btnSeedData = new Guna.UI2.WinForms.Guna2Button
            {
                Name = "btnSeedData",
                Text = "🧪 Sinh Dữ Liệu Test",
                Location = new Point(440, 20), // Sau btnAutoPhanCong
                Size = new Size(160, 42),
                BorderRadius = 10,
                FillColor = Color.OrangeRed, // Warning color
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Visible = false // Ẩn mặc định, chỉ bật khi cần test
            };
            btnSeedData.Click += BtnSeedData_Click;

            // Thêm vào panel
            if (panelPhanCongGiangDay != null)
            {
                panelPhanCongGiangDay.Controls.Add(btnSeedData);
            }
        }

        private void LoadStatCards(int? maHocKyFilter = null)
        {
            try
            {
                // Lấy dữ liệu thống kê
                List<PhanCongGiangDayDTO> dsPhanCong = phanCongBUS.DocDSPhanCong();
                
                // Áp dụng filter học kỳ nếu có
                if (maHocKyFilter.HasValue)
                {
                    dsPhanCong = dsPhanCong?.Where(pc => pc.MaHocKy == maHocKyFilter.Value).ToList();
                }
                
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
                // ✅ Gỡ event handlers cũ để tránh gắn nhiều lần
                cbHocKyNamHoc.SelectedIndexChanged -= FilterChanged;
                cbKhoi.SelectedIndexChanged -= FilterChanged;
                cbLop.SelectedIndexChanged -= FilterChanged;
                cbMonHoc.SelectedIndexChanged -= FilterChanged;

                // ✅ Load Học kỳ từ database với grouping theo năm học
                cbHocKyNamHoc.Items.Clear();
                cbHocKyNamHoc.Items.Add(new ComboBoxItem { Text = "Tất cả Học kỳ", Value = null });
                
                var dsHocKy = hocKyBUS.DocDSHocKy();
                Console.WriteLine($"🔍 LoadFilters: Đã load {dsHocKy?.Count ?? 0} học kỳ từ database");
                
                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    // ✅ SỬA: Group theo MaNamHoc thay vì extract từ TenHocKy
                    var namHocGroups = dsHocKy
                        .Select(hk => hk.MaNamHoc) // Lấy trực tiếp từ MaNamHoc
                        .Distinct()
                        .OrderByDescending(nh => nh)
                        .ToList();

                    Console.WriteLine($"🔍 LoadFilters: Tìm thấy {namHocGroups.Count} năm học: {string.Join(", ", namHocGroups)}");

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

                            // Thêm từng học kỳ trong năm (filter theo MaNamHoc)
                            var hocKyTrongNam = dsHocKy
                                .Where(hk => hk.MaNamHoc == namHoc)
                                .OrderBy(hk => hk.TenHocKy)
                                .ToList();

                            Console.WriteLine($"🔍 LoadFilters: Năm học {namHoc} có {hocKyTrongNam.Count} học kỳ");

                            foreach (var hk in hocKyTrongNam)
                            {
                                // Kiểm tra trạng thái phân công
                                bool hasOfficial = phanCongBUS.HasAssignmentsForSemester(hk.MaHocKy);
                                bool hasTemp = phanCongBUS.HasTempAssignmentsForSemester(hk.MaHocKy);
                                
                                string statusText = hasOfficial
                                    ? " (ĐÃ PHÂN CÔNG)"
                                    : (hasTemp ? " (ĐANG LƯU TẠM)" : " (CHƯA PHÂN)");
                                
                                cbHocKyNamHoc.Items.Add(new ComboBoxItem 
                                { 
                                    Text = $"   {hk.TenHocKy}{statusText}", // Indent + status
                                    Value = hk.MaHocKy 
                                });
                                
                                Console.WriteLine($"  ✅ Đã thêm: {hk.TenHocKy} (MaHocKy: {hk.MaHocKy})");
                            }
                        }
                    }
                    
                    Console.WriteLine($"✅ LoadFilters: Tổng cộng đã thêm {cbHocKyNamHoc.Items.Count} items vào ComboBox");
                }
                else
                {
                    Console.WriteLine($"⚠️ LoadFilters: Không có học kỳ nào trong database!");
                }
                
                // ✅ QUAN TRỌNG: Guna2ComboBox cần custom DrawItem để hiển thị ComboBoxItem đúng
                // Gỡ DrawItem cũ nếu có để tránh gắn nhiều lần
                cbHocKyNamHoc.DrawItem -= CbHocKyNamHoc_DrawItem;
                cbHocKyNamHoc.DrawMode = DrawMode.OwnerDrawFixed;
                cbHocKyNamHoc.DrawItem += CbHocKyNamHoc_DrawItem;
                
                // ✅ Gỡ event handler tạm thời để tránh trigger khi set SelectedIndex
                cbHocKyNamHoc.SelectedIndexChanged -= FilterChanged;
                
                // ✅ Set mặc định là học kỳ hiện tại (không trigger FilterChanged)
                SelectCurrentSemester();
                
                // ✅ Gắn lại event handler sau khi đã set SelectedIndex
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
                // Guna2ComboBox không cần DisplayMember/ValueMember, lưu trực tiếp ComboBoxItem
                cbLop.SelectedIndexChanged += FilterChanged;

                // ✅ Load Môn học từ database
                cbMonHoc.Items.Clear();
                cbMonHoc.Items.Add(new ComboBoxItem { Text = "Tất cả môn", Value = null });
                var dsMonHoc = monHocBUS.DocDSMH();
                if (dsMonHoc != null && dsMonHoc.Count > 0)
                {
                    foreach (var mh in dsMonHoc.OrderBy(m => m.tenMon))
                    {
                        cbMonHoc.Items.Add(new ComboBoxItem { Text = mh.tenMon, Value = mh.maMon });
                    }
                }
                // Guna2ComboBox không cần DisplayMember/ValueMember, lưu trực tiếp ComboBoxItem
                cbMonHoc.SelectedIndex = 0;
                cbMonHoc.SelectedIndexChanged += FilterChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load filters: {ex.Message}\n\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chọn học kỳ hiện tại làm mặc định
        /// </summary>
        private void SelectCurrentSemester()
        {
            try
            {
                var hocKyHienTai = SemesterHelper.GetCurrentSemester();
                Console.WriteLine($"🔍 SelectCurrentSemester: Học kỳ hiện tại = {hocKyHienTai?.TenHocKy ?? "null"} (MaHocKy: {hocKyHienTai?.MaHocKy ?? 0})");
                
                if (hocKyHienTai != null && cbHocKyNamHoc.Items.Count > 0)
                {
                    for (int i = 0; i < cbHocKyNamHoc.Items.Count; i++)
                    {
                        var item = cbHocKyNamHoc.Items[i] as ComboBoxItem;
                        if (item != null && item.Value != null)
                        {
                            string valueStr = item.Value.ToString();
                            // Bỏ qua các item là năm học (NAM_xxx)
                            if (!valueStr.StartsWith("NAM_") && valueStr == hocKyHienTai.MaHocKy.ToString())
                            {
                                cbHocKyNamHoc.SelectedIndex = i;
                                Console.WriteLine($"✅ SelectCurrentSemester: Đã chọn học kỳ hiện tại tại index {i}: {hocKyHienTai.TenHocKy} (MaHocKy: {hocKyHienTai.MaHocKy})");
                                return;
                            }
                        }
                    }
                    Console.WriteLine($"⚠️ SelectCurrentSemester: Không tìm thấy học kỳ hiện tại trong danh sách");
                }
                
                // Nếu không tìm thấy học kỳ hiện tại, chọn "Tất cả"
                if (cbHocKyNamHoc.Items.Count > 0)
                {
                    cbHocKyNamHoc.SelectedIndex = 0;
                    Console.WriteLine($"ℹ️ SelectCurrentSemester: Chọn mặc định index 0 (Tất cả Học kỳ)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi chọn học kỳ hiện tại: {ex.Message}\n{ex.StackTrace}");
                if (cbHocKyNamHoc.Items.Count > 0)
                {
                    cbHocKyNamHoc.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Custom DrawItem cho Guna2ComboBox để hiển thị ComboBoxItem đúng
        /// </summary>
        private void CbHocKyNamHoc_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0 || e.Index >= cbHocKyNamHoc.Items.Count)
                    return;

                e.DrawBackground();
                
                var item = cbHocKyNamHoc.Items[e.Index] as ComboBoxItem;
                string text = item != null ? item.Text : cbHocKyNamHoc.Items[e.Index]?.ToString() ?? "";
                
                // Vẽ text với màu phù hợp
                Color textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected 
                    ? Color.White 
                    : e.ForeColor;
                
                using (Brush brush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
                
                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CbHocKyNamHoc_DrawItem: {ex.Message}");
            }
        }

        /// <summary>
        /// Load danh sách lớp (có thể filter theo khối)
        /// </summary>
        private void LoadLopFilter(int? khoiFilter = null)
        {
            try
            {
                // Gỡ event handler tạm thời để tránh trigger khi đang load
                cbLop.SelectedIndexChanged -= FilterChanged;
                
                cbLop.Items.Clear();
                cbLop.Items.Add(new ComboBoxItem { Text = "Tất cả lớp", Value = null });

                var dsLop = lopHocBUS.DocDSLop();
                if (dsLop != null && dsLop.Count > 0)
                {
                    var lopList = dsLop.OrderBy(l => l.tenLop).ToList();
                    
                    foreach (var lop in lopList)
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
                
                // Guna2ComboBox không cần DisplayMember/ValueMember, lưu trực tiếp ComboBoxItem
                cbLop.SelectedIndex = 0;
                
                // Gắn lại event handler
                cbLop.SelectedIndexChanged += FilterChanged;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi load lớp: {ex.Message}");
                MessageBox.Show($"Lỗi khi load danh sách lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Event handler khi filter thay đổi
        /// </summary>
        private void FilterChanged(object sender, EventArgs e)
        {
            try
            {
                // Nếu cbKhoi thay đổi, reload cbLop
                if (sender == cbKhoi)
                {
                    if (cbKhoi.SelectedIndex > 0)
                    {
                        string khoiText = cbKhoi.SelectedItem?.ToString() ?? "";
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

                // ✅ Reload data với filter mới
                int? maHocKy = GetSelectedHocKyId();
                
                // Debug logging
                if (sender == cbHocKyNamHoc)
                {
                    Console.WriteLine($"🔍 Filter học kỳ thay đổi: SelectedIndex={cbHocKyNamHoc.SelectedIndex}, MaHocKy={maHocKy?.ToString() ?? "null"}");
                }
                
                LoadData(maHocKy);
                LoadStatCards(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi filter thay đổi: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Lỗi khi áp dụng bộ lọc: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Áp dụng filters vào danh sách phân công
        /// </summary>
        /// <param name="dsPhanCong">Danh sách phân công cần filter</param>
        /// <param name="skipHocKyFilter">Bỏ qua filter học kỳ nếu đã filter ở database level</param>
        private List<PhanCongGiangDayDTO> ApplyFilters(List<PhanCongGiangDayDTO> dsPhanCong, bool skipHocKyFilter = false)
        {
            if (dsPhanCong == null || dsPhanCong.Count == 0)
                return dsPhanCong;

            try
            {
                var filtered = dsPhanCong.AsEnumerable();

                // Filter theo Học kỳ hoặc Năm học (chỉ khi chưa filter ở database level)
                if (!skipHocKyFilter && cbHocKyNamHoc != null && cbHocKyNamHoc.SelectedIndex >= 0)
                {
                    Console.WriteLine($"🔍 ApplyFilters: SelectedIndex={cbHocKyNamHoc.SelectedIndex}, Items.Count={cbHocKyNamHoc.Items.Count}");
                    
                    ComboBoxItem hkItem = null;
                    
                    // ✅ Lấy ComboBoxItem từ Items[SelectedIndex] (Guna2ComboBox lưu object trực tiếp)
                    if (cbHocKyNamHoc.SelectedIndex < cbHocKyNamHoc.Items.Count)
                    {
                        hkItem = cbHocKyNamHoc.Items[cbHocKyNamHoc.SelectedIndex] as ComboBoxItem;
                        Console.WriteLine($"🔍 ApplyFilters: hkItem={(hkItem != null ? "found" : "null")}, Value={(hkItem?.Value?.ToString() ?? "null")}");
                    }
                    
                    if (hkItem != null && hkItem.Value != null)
                    {
                        string valueStr = hkItem.Value.ToString();
                        Console.WriteLine($"🔍 ApplyFilters: valueStr={valueStr}");
                        
                        if (valueStr.StartsWith("NAM_"))
                        {
                            // Filter theo CẢ NĂM HỌC - lấy danh sách học kỳ trong năm học đó
                            string namHoc = valueStr.Replace("NAM_", "");
                            Console.WriteLine($"🔍 ApplyFilters: Filter theo năm học: {namHoc}");
                            
                            // ✅ SỬA: Lấy danh sách học kỳ trong năm học này (filter theo MaNamHoc)
                            var dsHocKy = hocKyBUS.DocDSHocKy();
                            var maHocKyTrongNam = dsHocKy
                                .Where(hk => hk.MaNamHoc == namHoc) // So sánh trực tiếp MaNamHoc
                                .Select(hk => hk.MaHocKy)
                                .ToList();
                            
                            Console.WriteLine($"🔍 ApplyFilters: Tìm thấy {maHocKyTrongNam.Count} học kỳ trong năm {namHoc}");
                            
                            if (maHocKyTrongNam.Count > 0)
                            {
                                filtered = filtered.Where(pc => maHocKyTrongNam.Contains(pc.MaHocKy));
                            }
                            else
                            {
                                // Không có học kỳ nào trong năm học này
                                filtered = Enumerable.Empty<PhanCongGiangDayDTO>();
                            }
                        }
                        else
                        {
                            // Filter theo HỌC KỲ cụ thể
                            if (int.TryParse(valueStr, out int maHK))
                            {
                                Console.WriteLine($"🔍 ApplyFilters: Filter theo học kỳ cụ thể: MaHocKy={maHK}");
                                filtered = filtered.Where(pc => pc.MaHocKy == maHK);
                            }
                            else
                            {
                                Console.WriteLine($"⚠️ ApplyFilters: Không parse được '{valueStr}' thành int");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"ℹ️ ApplyFilters: Value == null hoặc hkItem == null (có thể là 'Tất cả'), không filter học kỳ");
                    }
                    // Nếu Value == null (Tất cả) thì không filter, giữ nguyên filtered
                }
                else
                {
                    Console.WriteLine($"ℹ️ ApplyFilters: skipHocKyFilter={skipHocKyFilter}, SelectedIndex={cbHocKyNamHoc?.SelectedIndex ?? -1}");
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
                if (cbLop != null && cbLop.SelectedIndex > 0)
                {
                    ComboBoxItem lopItem = null;
                    if (cbLop.SelectedItem is ComboBoxItem)
                    {
                        lopItem = cbLop.SelectedItem as ComboBoxItem;
                    }
                    else if (cbLop.SelectedIndex >= 0 && cbLop.SelectedIndex < cbLop.Items.Count)
                    {
                        lopItem = cbLop.Items[cbLop.SelectedIndex] as ComboBoxItem;
                    }
                    
                    if (lopItem != null && lopItem.Value != null)
                    {
                        if (int.TryParse(lopItem.Value.ToString(), out int maLop))
                        {
                            filtered = filtered.Where(pc => pc.MaLop == maLop);
                        }
                    }
                }

                // Filter theo Môn học
                if (cbMonHoc != null && cbMonHoc.SelectedIndex > 0)
                {
                    ComboBoxItem monItem = null;
                    if (cbMonHoc.SelectedItem is ComboBoxItem)
                    {
                        monItem = cbMonHoc.SelectedItem as ComboBoxItem;
                    }
                    else if (cbMonHoc.SelectedIndex >= 0 && cbMonHoc.SelectedIndex < cbMonHoc.Items.Count)
                    {
                        monItem = cbMonHoc.Items[cbMonHoc.SelectedIndex] as ComboBoxItem;
                    }
                    
                    if (monItem != null && monItem.Value != null)
                    {
                        if (int.TryParse(monItem.Value.ToString(), out int maMon))
                        {
                            filtered = filtered.Where(pc => pc.MaMonHoc == maMon);
                        }
                    }
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
        /// ⚠️ DEPRECATED: Không dùng nữa vì TenHocKy không chứa năm học, dùng MaNamHoc trực tiếp
        /// </summary>
        [Obsolete("Sử dụng MaNamHoc trực tiếp từ HocKyDTO thay vì extract từ TenHocKy")]
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
        /// Lấy năm học từ HocKyDTO (sử dụng MaNamHoc)
        /// </summary>
        private string GetNamHocFromHocKy(HocKyDTO hocKy)
        {
            return hocKy?.MaNamHoc ?? null;
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

        private void LoadData(int? maHocKyFilter = null)
        {
            try
            {
                Console.WriteLine($"📊 LoadData được gọi với maHocKyFilter={maHocKyFilter?.ToString() ?? "null"}");
                
                // ✅ Tắt sự kiện tạm thời để tăng tốc độ
                dgvPhanCong.SuspendLayout();
                
                // Cấu hình DataGridView (chỉ cần làm một lần)
                if (dgvPhanCong.Columns.Count == 0)
                {
                    SetupDataGridView();
                }
                else
                {
                    // Chỉ clear dữ liệu, giữ nguyên cấu hình
                    dgvPhanCong.DataSource = null;
                }

                // ✅ Lấy dữ liệu từ database
                // Nếu có filter học kỳ cụ thể từ parameter, lấy trực tiếp từ database (hiệu quả hơn)
                List<PhanCongGiangDayDTO> dsPhanCong;
                if (maHocKyFilter.HasValue)
                {
                    Console.WriteLine($"✅ LoadData: Lấy dữ liệu từ database theo học kỳ {maHocKyFilter.Value}");
                    dsPhanCong = phanCongBUS.GetBySemester(maHocKyFilter.Value);
                    Console.WriteLine($"✅ LoadData: Đã lấy được {dsPhanCong?.Count ?? 0} phân công từ database");
                }
                else
                {
                    Console.WriteLine($"✅ LoadData: Lấy tất cả phân công từ database");
                    // Lấy tất cả rồi filter bằng ApplyFilters
                    dsPhanCong = phanCongBUS.DocDSPhanCong();
                    Console.WriteLine($"✅ LoadData: Đã lấy được {dsPhanCong?.Count ?? 0} phân công từ database");
                }

                // ✅ Áp dụng filters (học kỳ/năm học, khối, lớp, môn học)
                if (dsPhanCong != null && dsPhanCong.Count > 0)
                {
                    int countBeforeFilter = dsPhanCong.Count;
                    dsPhanCong = ApplyFilters(dsPhanCong, skipHocKyFilter: maHocKyFilter.HasValue);
                    Console.WriteLine($"✅ LoadData: Sau filter: {dsPhanCong.Count} phân công (trước: {countBeforeFilter})");
                }
                else
                {
                    Console.WriteLine($"⚠️ LoadData: Không có dữ liệu phân công");
                }

                // ✅ Cache các lookup để tránh N+1 query
                var giaoVienCache = new Dictionary<string, string>();
                var monHocCache = new Dictionary<int, string>();
                var lopCache = new Dictionary<int, string>();
                var hocKyCache = new Dictionary<int, string>();

                // Load tất cả lookup một lần
                if (dsPhanCong != null && dsPhanCong.Count > 0)
                {
                    var uniqueGV = dsPhanCong.Select(pc => pc.MaGiaoVien).Distinct().ToList();
                    var uniqueMH = dsPhanCong.Select(pc => pc.MaMonHoc).Distinct().ToList();
                    var uniqueLop = dsPhanCong.Select(pc => pc.MaLop).Distinct().ToList();
                    var uniqueHK = dsPhanCong.Select(pc => pc.MaHocKy).Distinct().ToList();

                    // Cache giáo viên
                    foreach (var maGV in uniqueGV)
                    {
                        if (!giaoVienCache.ContainsKey(maGV))
                        {
                            var gv = giaoVienBUS.LayGiaoVienTheoMa(maGV);
                            giaoVienCache[maGV] = gv != null ? gv.HoTen : maGV;
                        }
                    }

                    // Cache môn học
                    foreach (var maMH in uniqueMH)
                    {
                        if (!monHocCache.ContainsKey(maMH))
                        {
                            var mh = monHocBUS.LayDSMonHocTheoId(maMH);
                            monHocCache[maMH] = mh != null ? mh.tenMon : $"MH-{maMH}";
                        }
                    }

                    // Cache lớp
                    foreach (var maLop in uniqueLop)
                    {
                        if (!lopCache.ContainsKey(maLop))
                        {
                            var lop = lopHocBUS.LayLopTheoId(maLop);
                            lopCache[maLop] = lop != null ? lop.tenLop : $"Lớp-{maLop}";
                        }
                    }

                    // Cache học kỳ
                    foreach (var maHK in uniqueHK)
                    {
                        if (!hocKyCache.ContainsKey(maHK))
                        {
                            var hk = hocKyBUS.LayHocKyTheoMa(maHK);
                            hocKyCache[maHK] = hk != null ? hk.TenHocKy : $"HK-{maHK}";
                        }
                    }
                }

                // ✅ Tạo BindingList từ ViewModel
                bindingList = new BindingList<PhanCongGiangDayViewModel>();
                
                if (dsPhanCong != null && dsPhanCong.Count > 0)
                {
                    foreach (PhanCongGiangDayDTO pc in dsPhanCong)
                    {
                        bindingList.Add(new PhanCongGiangDayViewModel
                        {
                            MaPhanCong = pc.MaPhanCong,
                            GiaoVien = giaoVienCache.ContainsKey(pc.MaGiaoVien) ? giaoVienCache[pc.MaGiaoVien] : pc.MaGiaoVien,
                            MonHoc = monHocCache.ContainsKey(pc.MaMonHoc) ? monHocCache[pc.MaMonHoc] : $"MH-{pc.MaMonHoc}",
                            Lop = lopCache.ContainsKey(pc.MaLop) ? lopCache[pc.MaLop] : $"Lớp-{pc.MaLop}",
                            HocKy = hocKyCache.ContainsKey(pc.MaHocKy) ? hocKyCache[pc.MaHocKy] : $"HK-{pc.MaHocKy}",
                            ThoiGian = $"{pc.NgayBatDau:dd/MM/yyyy} - {pc.NgayKetThuc:dd/MM/yyyy}",
                            ThaoTac = "",
                            MaGiaoVien = pc.MaGiaoVien,
                            MaMonHoc = pc.MaMonHoc,
                            MaLop = pc.MaLop,
                            MaHocKy = pc.MaHocKy
                        });
                    }
                }

                // ✅ Gán BindingList vào DataSource
                dgvPhanCong.DataSource = bindingList;
                
                // ✅ Resume layout
                dgvPhanCong.ResumeLayout();
            }
            catch (Exception ex)
            {
                dgvPhanCong.ResumeLayout();
                MessageBox.Show($"Lỗi khi tải dữ liệu bảng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cấu hình DataGridView một lần (chỉ gọi khi khởi tạo)
        /// </summary>
        private void SetupDataGridView()
        {
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

            // Tạo cột với DataPropertyName để bind
            var colMaPhanCong = new DataGridViewTextBoxColumn
            {
                Name = "MaPhanCong",
                HeaderText = "Mã",
                DataPropertyName = "MaPhanCong",
                Visible = false
            };
            dgvPhanCong.Columns.Add(colMaPhanCong);

            var colGiaoVien = new DataGridViewTextBoxColumn
            {
                Name = "GiaoVien",
                HeaderText = "Giáo viên",
                DataPropertyName = "GiaoVien"
            };
            dgvPhanCong.Columns.Add(colGiaoVien);

            var colMonHoc = new DataGridViewTextBoxColumn
            {
                Name = "MonHoc",
                HeaderText = "Môn học",
                DataPropertyName = "MonHoc"
            };
            dgvPhanCong.Columns.Add(colMonHoc);

            var colLop = new DataGridViewTextBoxColumn
            {
                Name = "Lop",
                HeaderText = "Lớp",
                DataPropertyName = "Lop"
            };
            dgvPhanCong.Columns.Add(colLop);

            var colHocKy = new DataGridViewTextBoxColumn
            {
                Name = "HocKy",
                HeaderText = "Học kỳ",
                DataPropertyName = "HocKy"
            };
            dgvPhanCong.Columns.Add(colHocKy);

            var colThoiGian = new DataGridViewTextBoxColumn
            {
                Name = "ThoiGian",
                HeaderText = "Thời gian",
                DataPropertyName = "ThoiGian"
            };
            dgvPhanCong.Columns.Add(colThoiGian);

            var colThaoTac = new DataGridViewTextBoxColumn
            {
                Name = "ThaoTac",
                HeaderText = "Thao tác",
                DataPropertyName = "ThaoTac",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 100
            };
            dgvPhanCong.Columns.Add(colThaoTac);

            // Thiết lập chế độ co giãn
            dgvPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanCong.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhanCong.Columns["ThaoTac"].Width = 100;

            // Gắn sự kiện (chỉ gắn một lần)
            dgvPhanCong.CellPainting += dgvPhanCong_CellPainting;
            dgvPhanCong.CellClick += dgvPhanCong_CellClick;
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
                    // ✅ Lấy permission từ Tag - Sử dụng cách an toàn hơn
                    bool canDelete = true; // Mặc định true
                    
                    if (dgvPhanCong.Tag != null)
                    {
                        try
                        {
                            dynamic permissions = dgvPhanCong.Tag;
                            canDelete = permissions?.CanDelete ?? true;
                        }
                        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                        {
                            canDelete = true;
                        }
                    }

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

                // ✅ Lấy dữ liệu từ DataBoundItem (BindingList)
                var viewModel = dgvPhanCong.Rows[e.RowIndex].DataBoundItem as PhanCongGiangDayViewModel;
                if (viewModel == null) return;

                int maPhanCong = viewModel.MaPhanCong;
                string tenGV = viewModel.GiaoVien;

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
                        // ✅ Xóa từ BindingList thay vì Rows
                        if (bindingList != null && rowIndex >= 0 && rowIndex < bindingList.Count)
                        {
                            bindingList.RemoveAt(rowIndex);
                        }
                        else
                        {
                            // Fallback: reload lại dữ liệu
                            int? maHocKy = GetSelectedHocKyId();
                            LoadData(maHocKy);
                        }
                        
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
                using (var frm = new frmAutoPhanCongPreview())
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
                    int? maHocKy = GetSelectedHocKyId();
                    LoadData(maHocKy);
                    LoadStatCards(maHocKy);
                    // Refresh semester status in filter combo
                    LoadFilters();
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


        /// <summary>
        /// Nút "Sinh Dữ Liệu Test" → Generate stress test data for all classes
        /// Sử dụng SeedingService để tạo phân công tự động cho toàn trường
        /// </summary>
        private async void BtnSeedData_Click(object sender, EventArgs e)
        {
            // ✅ Kiểm tra quyền CREATE
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLPHANCONG, "Sinh dữ liệu test"))
                return;

            // Lấy học kỳ đang chọn từ filter
            int? selectedHocKy = GetSelectedHocKyId();
            if (!selectedHocKy.HasValue || selectedHocKy.Value <= 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn Học kỳ từ dropdown filter trước!\n\n" +
                    "Chức năng này sẽ sinh phân công cho học kỳ đã chọn.",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int maHocKy = selectedHocKy.Value;

            // Safety warning
            var confirm = MessageBox.Show(
                "CẢNH BÁO: Hành động này sẽ XÓA SẠCH phân công hiện tại của học kỳ này và sinh lại dữ liệu mẫu cho toàn trường.\n\n" +
                $"Học kỳ: {GetSelectedHocKyName()}\n\n" +
                "Bạn có chắc không?",
                "Xác nhận sinh dữ liệu test",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            // Disable button and show wait cursor
            btnSeedData.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            string report = string.Empty;
            Exception error = null;

            try
            {
                // Execute seeding asynchronously
                report = await Task.Run(() =>
                {
                    var seedingService = new SeedingService();
                    return seedingService.SeedFullAssignments(maHocKy);
                });

                // Show success report
                MessageBox.Show(
                    report,
                    "Kết quả sinh dữ liệu test",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Reload data to show new assignments
                LoadData((int?)maHocKy);
                LoadStatCards((int?)maHocKy);
                LoadFilters(); // Refresh status indicators

                // Show success notification
                ShowSuccessNotification("✅ Đã sinh dữ liệu phân công thành công!");
            }
            catch (Exception ex)
            {
                error = ex;
                MessageBox.Show(
                    $"Lỗi khi sinh dữ liệu test:\n\n{ex.Message}\n\n" +
                    (!string.IsNullOrEmpty(report) ? $"Chi tiết:\n{report}" : ""),
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable button and reset cursor
                btnSeedData.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Lấy mã học kỳ đang chọn từ filter ComboBox
        /// </summary>
        private int? GetSelectedHocKyId()
        {
            try
            {
                if (cbHocKyNamHoc == null || cbHocKyNamHoc.SelectedIndex < 0)
                {
                    Console.WriteLine("⚠️ GetSelectedHocKyId: ComboBox null hoặc SelectedIndex < 0");
                    return null;
                }

                // ✅ Lấy trực tiếp từ Items[SelectedIndex] vì Guna2ComboBox lưu object trực tiếp
                if (cbHocKyNamHoc.SelectedIndex >= 0 && cbHocKyNamHoc.SelectedIndex < cbHocKyNamHoc.Items.Count)
                {
                    var item = cbHocKyNamHoc.Items[cbHocKyNamHoc.SelectedIndex] as ComboBoxItem;
                    
                    if (item == null)
                    {
                        Console.WriteLine($"⚠️ GetSelectedHocKyId: Item tại index {cbHocKyNamHoc.SelectedIndex} không phải ComboBoxItem");
                        return null;
                    }

                    if (item.Value == null)
                    {
                        Console.WriteLine($"⚠️ GetSelectedHocKyId: Item.Value là null (có thể là 'Tất cả')");
                        return null;
                    }

                    string valueStr = item.Value.ToString();
                    Console.WriteLine($"✅ GetSelectedHocKyId: SelectedIndex={cbHocKyNamHoc.SelectedIndex}, Value={valueStr}");
                    
                    // Nếu là năm học (NAM_xxx), không trả về học kỳ cụ thể
                    if (valueStr.StartsWith("NAM_"))
                    {
                        Console.WriteLine($"ℹ️ GetSelectedHocKyId: Đây là năm học ({valueStr}), trả về null");
                        return null;
                    }
                    
                    // Nếu là học kỳ cụ thể, parse thành int
                    if (int.TryParse(valueStr, out int maHK))
                    {
                        Console.WriteLine($"✅ GetSelectedHocKyId: Trả về MaHocKy={maHK}");
                        return maHK;
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ GetSelectedHocKyId: Không parse được '{valueStr}' thành int");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi GetSelectedHocKyId: {ex.Message}\n{ex.StackTrace}");
            }
            
            return null;
        }

        /// <summary>
        /// Lấy tên học kỳ đang chọn
        /// </summary>
        private string GetSelectedHocKyName()
        {
            if (cbHocKyNamHoc?.SelectedItem is ComboBoxItem item)
            {
                return item.Text?.Replace("   ", "").Trim() ?? "N/A";
            }
            return "N/A";
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
