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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class PhanCongGiangDay : UserControl
    {
        private PhanCongGiangDayBUS phanCongBUS;
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private MonHoc_NamHoc_KhoiBUS monHocNamHocKhoiBUS;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;
        
        // BindingList để bind vào DataGridView
        private BindingList<DTO.PhanCongGiangDayViewModel> bindingList;

        public PhanCongGiangDay()
        {
            InitializeComponent();
            phanCongBUS = new PhanCongGiangDayBUS();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            monHocNamHocKhoiBUS = new MonHoc_NamHoc_KhoiBUS();
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
        /// Load danh sách môn học vào filter ComboBox (filter theo năm học và khối nếu có)
        /// ✅ Tự động cập nhật khi học kỳ thay đổi để hiển thị môn học mới được thêm vào năm học đó
        /// </summary>
        private void LoadMonHocFilter()
        {
            try
            {
                // Gỡ event handler tạm thời để tránh trigger khi đang load
                cbMonHoc.SelectedIndexChanged -= FilterChanged;
                
                cbMonHoc.Items.Clear();
                cbMonHoc.Items.Add(new ComboBoxItem { Text = "Tất cả môn", Value = null });

                // Lấy năm học từ học kỳ đã chọn (nếu có)
                int? maHocKy = GetSelectedHocKyId();
                string maNamHoc = null;
                int? maKhoi = null;

                if (maHocKy.HasValue)
                {
                    var hocKy = hocKyBUS.LayHocKyTheoMa(maHocKy.Value);
                    if (hocKy != null && !string.IsNullOrEmpty(hocKy.MaNamHoc))
                    {
                        maNamHoc = hocKy.MaNamHoc;
                    }
                }

                // Lấy khối từ filter khối (nếu có)
                if (cbKhoi != null && cbKhoi.SelectedIndex > 0)
                {
                    string khoiText = cbKhoi.SelectedItem?.ToString() ?? "";
                    if (khoiText.Contains("Khối "))
                    {
                        if (int.TryParse(khoiText.Replace("Khối ", ""), out int khoi))
                        {
                            maKhoi = khoi;
                        }
                    }
                }

                // ✅ Load môn học theo năm học và khối từ MonHoc_NamHoc_Khoi (để hiển thị môn học mới được thêm)
                List<MonHocDTO> dsMonHoc;
                if (!string.IsNullOrEmpty(maNamHoc) && maKhoi.HasValue)
                {
                    // Có cả năm học và khối → lấy môn học chính xác
                    dsMonHoc = monHocNamHocKhoiBUS.LayDanhSachMonHocTheoNamHocKhoi(maNamHoc, maKhoi.Value);
                }
                else if (!string.IsNullOrEmpty(maNamHoc))
                {
                    // Chỉ có năm học → lấy tất cả môn học trong năm học đó (tất cả khối)
                    dsMonHoc = monHocNamHocKhoiBUS.LayDanhSachMonHocTheoNamHoc(maNamHoc);
                }
                else
                {
                    // Nếu không có năm học, load tất cả môn học (fallback)
                    dsMonHoc = monHocBUS.DocDSMH();
                }

                if (dsMonHoc != null && dsMonHoc.Count > 0)
                {
                    foreach (var mh in dsMonHoc.OrderBy(m => m.tenMon))
                    {
                        cbMonHoc.Items.Add(new ComboBoxItem { Text = mh.tenMon, Value = mh.maMon });
                    }
                }
                
                // Gắn lại event handler
                cbMonHoc.SelectedIndexChanged += FilterChanged;
            }
            catch (Exception ex)
            {
                // Fallback: Load tất cả môn học nếu có lỗi
                try
                {
                    var dsMonHoc = monHocBUS.DocDSMH();
                    if (dsMonHoc != null && dsMonHoc.Count > 0)
                    {
                        foreach (var mh in dsMonHoc.OrderBy(m => m.tenMon))
                        {
                            cbMonHoc.Items.Add(new ComboBoxItem { Text = mh.tenMon, Value = mh.maMon });
                        }
                    }
                }
                catch { }
                
                // Gắn lại event handler
                cbMonHoc.SelectedIndexChanged += FilterChanged;
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
                var statistics = phanCongBUS.GetStatistics(maHocKyFilter);

                statCardPhanCongGiangDay1.Title = "Tổng phân công";
                statCardPhanCongGiangDay1.Value = statistics["TongPhanCong"].ToString();
                statCardPhanCongGiangDay1.TitleColor = Color.FromArgb(30, 136, 229);

                statCardPhanCongGiangDay2.Title = "Giáo viên";
                statCardPhanCongGiangDay2.Value = statistics["TongGiaoVien"].ToString();
                statCardPhanCongGiangDay2.TitleColor = Color.FromArgb(30, 136, 229);

                statCardPhanCongGiangDay3.Title = "Môn học";
                statCardPhanCongGiangDay3.Value = statistics["TongMonHoc"].ToString();
                statCardPhanCongGiangDay3.TitleColor = Color.FromArgb(20, 163, 74);

                statCardPhanCongGiangDay4.Title = "Lớp học";
                statCardPhanCongGiangDay4.Value = statistics["TongLopHoc"].ToString();
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
                cbHocKyNamHoc.SelectedIndexChanged -= FilterChanged;
                cbKhoi.SelectedIndexChanged -= FilterChanged;
                cbLop.SelectedIndexChanged -= FilterChanged;
                cbMonHoc.SelectedIndexChanged -= FilterChanged;

                cbHocKyNamHoc.Items.Clear();
                cbHocKyNamHoc.Items.Add(new ComboBoxItem { Text = "Tất cả Học kỳ", Value = null });
                
                var dsHocKy = hocKyBUS.DocDSHocKy();
                
                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    var namHocGroups = dsHocKy
                        .Select(hk => hk.MaNamHoc)
                        .Distinct()
                        .OrderByDescending(nh => nh)
                        .ToList();

                    foreach (var namHoc in namHocGroups)
                    {
                        if (!string.IsNullOrEmpty(namHoc))
                        {
                            cbHocKyNamHoc.Items.Add(new ComboBoxItem 
                            { 
                                Text = $"Cả năm {namHoc}", 
                                Value = $"NAM_{namHoc}"
                            });

                            var hocKyTrongNam = dsHocKy
                                .Where(hk => hk.MaNamHoc == namHoc)
                                .OrderBy(hk => hk.TenHocKy)
                                .ToList();

                            foreach (var hk in hocKyTrongNam)
                            {
                                bool hasOfficial = phanCongBUS.HasAssignmentsForSemester(hk.MaHocKy);
                                bool hasTemp = phanCongBUS.HasTempAssignmentsForSemester(hk.MaHocKy);
                                
                                string statusText = hasOfficial
                                    ? " (ĐÃ PHÂN CÔNG)"
                                    : (hasTemp ? " (ĐANG LƯU TẠM)" : " (CHƯA PHÂN)");
                                
                                cbHocKyNamHoc.Items.Add(new ComboBoxItem 
                                { 
                                    Text = $"   {hk.TenHocKy}{statusText}",
                                    Value = hk.MaHocKy 
                                });
                            }
                        }
                    }
                }
                
                cbHocKyNamHoc.DrawItem -= CbHocKyNamHoc_DrawItem;
                cbHocKyNamHoc.DrawMode = DrawMode.OwnerDrawFixed;
                cbHocKyNamHoc.DrawItem += CbHocKyNamHoc_DrawItem;
                
                cbHocKyNamHoc.SelectedIndexChanged -= FilterChanged;
                SelectCurrentSemester();
                cbHocKyNamHoc.SelectedIndexChanged += FilterChanged;

                cbKhoi.Items.Clear();
                cbKhoi.Items.Add("Tất cả Khối");
                cbKhoi.Items.Add("Khối 10");
                cbKhoi.Items.Add("Khối 11");
                cbKhoi.Items.Add("Khối 12");
                cbKhoi.SelectedIndex = 0;
                cbKhoi.SelectedIndexChanged += FilterChanged;

                LoadLopFilter();
                cbLop.SelectedIndexChanged += FilterChanged;

                cbMonHoc.Items.Clear();
                cbMonHoc.Items.Add(new ComboBoxItem { Text = "Tất cả môn", Value = null });
                
                // Load subjects based on selected semester/year and grade (if any)
                LoadMonHocFilter();
                
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
                
                if (hocKyHienTai != null && cbHocKyNamHoc.Items.Count > 0)
                {
                    for (int i = 0; i < cbHocKyNamHoc.Items.Count; i++)
                    {
                        var item = cbHocKyNamHoc.Items[i] as ComboBoxItem;
                        if (item != null && item.Value != null)
                        {
                            string valueStr = item.Value.ToString();
                            if (!valueStr.StartsWith("NAM_") && valueStr == hocKyHienTai.MaHocKy.ToString())
                            {
                                cbHocKyNamHoc.SelectedIndex = i;
                                return;
                            }
                        }
                    }
                }
                
                if (cbHocKyNamHoc.Items.Count > 0)
                {
                    cbHocKyNamHoc.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
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
            catch
            {
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
                        LoadLopFilter();
                    }
                    
                    // Reload môn học filter khi khối thay đổi
                    LoadMonHocFilter();
                }
                else if (sender == cbHocKyNamHoc)
                {
                    // ✅ Reload môn học filter khi học kỳ/năm học thay đổi
                    // Đảm bảo hiển thị các môn học mới được thêm vào năm học đó
                    LoadMonHocFilter();
                }

                int? maHocKy = GetSelectedHocKyId();
                
                LoadData(maHocKy);
                LoadStatCards(maHocKy);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng bộ lọc: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Tạo PhanCongFilterCriteria từ UI controls
        /// </summary>
        private PhanCongFilterCriteria BuildFilterCriteria(bool skipHocKyFilter = false)
        {
            var criteria = new PhanCongFilterCriteria();

            if (!skipHocKyFilter && cbHocKyNamHoc != null && cbHocKyNamHoc.SelectedIndex >= 0)
            {
                ComboBoxItem hkItem = null;
                if (cbHocKyNamHoc.SelectedIndex < cbHocKyNamHoc.Items.Count)
                {
                    hkItem = cbHocKyNamHoc.Items[cbHocKyNamHoc.SelectedIndex] as ComboBoxItem;
                }

                if (hkItem != null && hkItem.Value != null)
                {
                    string valueStr = hkItem.Value.ToString();
                    if (valueStr.StartsWith("NAM_"))
                    {
                        criteria.MaNamHoc = valueStr.Replace("NAM_", "");
                    }
                    else if (int.TryParse(valueStr, out int maHK))
                    {
                        criteria.MaHocKy = maHK;
                    }
                }
            }

            if (cbKhoi != null && cbKhoi.SelectedIndex > 0)
            {
                string khoiText = cbKhoi.SelectedItem.ToString();
                if (khoiText.Contains("Khối "))
                {
                    if (int.TryParse(khoiText.Replace("Khối ", ""), out int khoi))
                    {
                        criteria.Khoi = khoi;
                    }
                }
            }

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
                        criteria.MaLop = maLop;
                    }
                }
            }

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
                        criteria.MaMonHoc = maMon;
                    }
                }
            }

            return criteria;
        }

        /// <summary>
        /// Áp dụng filters vào danh sách phân công (sử dụng BUS)
        /// </summary>
        /// <param name="dsPhanCong">Danh sách phân công cần filter</param>
        /// <param name="skipHocKyFilter">Bỏ qua filter học kỳ nếu đã filter ở database level</param>
        private List<PhanCongGiangDayDTO> ApplyFilters(List<PhanCongGiangDayDTO> dsPhanCong, bool skipHocKyFilter = false)
        {
            if (dsPhanCong == null || dsPhanCong.Count == 0)
                return dsPhanCong ?? new List<PhanCongGiangDayDTO>();

            try
            {
                // ✅ Sử dụng BUS để apply filters
                var criteria = BuildFilterCriteria(skipHocKyFilter);
                return phanCongBUS.ApplyFilters(dsPhanCong, criteria, skipHocKyFilter);
            }
            catch (Exception ex)
            {
                return dsPhanCong;
            }
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
                dgvPhanCong.SuspendLayout();
                
                if (dgvPhanCong.Columns.Count == 0)
                {
                    SetupDataGridView();
                }
                else
                {
                    dgvPhanCong.DataSource = null;
                }

                var criteria = BuildFilterCriteria(skipHocKyFilter: maHocKyFilter.HasValue);
                var viewModels = phanCongBUS.GetFilteredViewModels(criteria, maHocKyFilter);

                bindingList = new BindingList<DTO.PhanCongGiangDayViewModel>(viewModels ?? new List<DTO.PhanCongGiangDayViewModel>());
                dgvPhanCong.DataSource = bindingList;
                
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
                Width = 150 // Tăng width để chứa 3 icon
            };
            dgvPhanCong.Columns.Add(colThaoTac);

            // Thiết lập chế độ co giãn
            dgvPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanCong.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhanCong.Columns["ThaoTac"].Width = 150; // Tăng width để chứa 3 icon

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

            // Vẽ icon cho cột "ThaoTac" (Xem, Sửa và Xóa)
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPhanCong.Columns["ThaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                // Kiểm tra quyền
                bool canUpdate = PermissionHelper.CheckDataGridIconPermission(dgvPhanCong, "update", "Phân công giảng dạy");
                bool canDelete = PermissionHelper.CheckDataGridIconPermission(dgvPhanCong, "delete", "Phân công giảng dạy");

                // Chỉ giữ lại 3 icon: Xem, Sửa, Xóa
                Image viewIcon = CreateViewIcon();
                Image editIcon = Properties.Resources.edit_icon ?? Properties.Resources.repair;
                Image deleteIcon = Properties.Resources.deleteicon ?? Properties.Resources.bin;

                int iconSize = 22;
                int spacing = 14;
                int totalWidth = iconSize * 3 + spacing * 2; // 3 icon: Xem, Sửa, Xóa
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle viewRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle editRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + (iconSize + spacing) * 2, y, iconSize, iconSize);

                // Vẽ icon Xem (luôn hiển thị)
                e.Graphics.DrawImage(viewIcon, viewRect);

                // Vẽ icon Sửa với độ mờ nếu không có quyền
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

                // Vẽ icon Xóa với độ mờ nếu không có quyền
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

        // Tạo icon "Xem" (eye icon) bằng code
        private Image CreateViewIcon()
        {
            Bitmap bmp = new Bitmap(22, 22);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Vẽ hình mắt đơn giản
                // Màu xanh dương cho icon "Xem"
                Pen pen = new Pen(Color.FromArgb(30, 136, 229), 2.5f);
                Brush brush = new SolidBrush(Color.FromArgb(30, 136, 229));
                
                // Vẽ hình oval (mắt)
                g.DrawEllipse(pen, 3, 5, 16, 12);
                
                // Vẽ con ngươi
                g.FillEllipse(brush, 8, 9, 5, 5);
            }
            return bmp;
        }

        private void dgvPhanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPhanCong.Columns["ThaoTac"].Index)
            {
                var viewModel = dgvPhanCong.Rows[e.RowIndex].DataBoundItem as DTO.PhanCongGiangDayViewModel;
                if (viewModel == null) return;

                int maPhanCong = viewModel.MaPhanCong;
                string tenGV = viewModel.GiaoVien;

                // Tính toán vị trí click để xác định icon nào được click
                Rectangle cellBounds = dgvPhanCong.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Point clickPosInCell = dgvPhanCong.PointToClient(Cursor.Position);
                int xClick = clickPosInCell.X - cellBounds.Left;

                int iconSize = 22;
                int spacing = 14;
                int totalWidth = iconSize * 3 + spacing * 2; // 3 icon: Xem, Sửa, Xóa
                int startXInCell = (cellBounds.Width - totalWidth) / 2;

                int viewIconEndX = startXInCell + iconSize;
                int editIconStartX = startXInCell + iconSize + spacing;
                int editIconEndX = editIconStartX + iconSize;
                int deleteIconStartX = editIconStartX + iconSize + spacing;
                int deleteIconEndX = deleteIconStartX + iconSize;

                // Click vào icon Xem
                if (xClick >= startXInCell && xClick < viewIconEndX)
                {
                    XemChiTietPhanCong(maPhanCong);
                }
                // Click vào icon Sửa
                else if (xClick >= editIconStartX && xClick < editIconEndX)
                {
                    if (PermissionHelper.CheckDataGridIconPermission(dgvPhanCong, "update", "Phân công giảng dạy"))
                    {
                        SuaPhanCong(maPhanCong, e.RowIndex);
                    }
                }
                // Click vào icon Xóa
                else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
                {
                    if (PermissionHelper.CheckDataGridIconPermission(dgvPhanCong, "delete", "Phân công giảng dạy"))
                    {
                        XoaPhanCong(maPhanCong, tenGV, e.RowIndex);
                    }
                }
            }
        }

        private void XemChiTietPhanCong(int maPhanCong)
        {
            try
            {
                FrmXemChiTietPhanCongGiangDay frm = new FrmXemChiTietPhanCongGiangDay(maPhanCong);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuaPhanCong(int maPhanCong, int rowIndex)
        {
            try
            {
                FrmSuaPhanCongGiangDay frm = new FrmSuaPhanCongGiangDay(maPhanCong);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Reload dữ liệu sau khi sửa thành công
                    LoadData();
                    MessageBox.Show("Đã cập nhật phân công thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa phân công:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaPhanCong(int maPhanCong, string tenGV, int rowIndex)
        {
            try
            {
                string thongTinXoa = $"Bạn có chắc chắn muốn xóa phân công này?\n\n" +
                                    $"Giáo viên: {tenGV}\n" +
                                    $"Mã: {maPhanCong}\n\n" +
                                    $"CẢNH BÁO:\n" +
                                    $"• Thao tác này sẽ xóa vĩnh viễn phân công\n" +
                                    $"• KHÔNG THỂ HOÀN TÁC sau khi xóa!\n\n" +
                                    $"Bạn có muốn tiếp tục?";

                DialogResult result = MessageBox.Show(
                    thongTinXoa,
                    "Xác nhận xóa phân công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

                if (result == DialogResult.Yes)
                {
                    bool xoaThanhCong = phanCongBUS.XoaPhanCong(maPhanCong);

                    if (xoaThanhCong)
                    {
                        if (bindingList != null && rowIndex >= 0 && rowIndex < bindingList.Count)
                        {
                            bindingList.RemoveAt(rowIndex);
                        }
                        else
                        {
                            int? maHocKy = GetSelectedHocKyId();
                            LoadData(maHocKy);
                        }
                        
                        LoadStatCards();

                        MessageBox.Show(
                            $"Đã xóa phân công của '{tenGV}' thành công!",
                            "Xóa thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            $"Không thể xóa phân công!\n\nVui lòng kiểm tra lại!",
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
                    $"Lỗi khi xóa phân công!\n\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                try { LoadData(); } catch { }
            }
        }


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
                    
                using (var frm = new frmAutoPhanCongPreview())
                {
                    var result = frm.ShowDialog();
                    
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

        private async Task ReloadAfterAutoAssignmentAsync()
        {
            Panel loadingPanel = null;
            try
            {
                loadingPanel = CreateLoadingOverlay();
                this.Controls.Add(loadingPanel);
                loadingPanel.BringToFront();
                loadingPanel.Visible = true;

                await Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(300);
                });

                this.Invoke((MethodInvoker)delegate
                {
                    int? maHocKy = GetSelectedHocKyId();
                    LoadData(maHocKy);
                    LoadStatCards(maHocKy);
                    LoadFilters();
                });

                if (loadingPanel != null)
                {
                    this.Controls.Remove(loadingPanel);
                    loadingPanel.Dispose();
                }

                ShowSuccessNotification("Phân công đã được lưu và cập nhật thành công!");

                if (dgvPhanCong != null && dgvPhanCong.Rows.Count > 0)
                {
                    dgvPhanCong.ClearSelection();
                    dgvPhanCong.FirstDisplayedScrollingRowIndex = 0;
                    dgvPhanCong.Rows[0].Selected = true;
                    dgvPhanCong.Refresh();
                }
            }
            catch (Exception ex)
            {
                if (loadingPanel != null && this.Controls.Contains(loadingPanel))
                {
                    this.Controls.Remove(loadingPanel);
                    loadingPanel.Dispose();
                }
                
                MessageBox.Show($"Lỗi reload data: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateLoadingOverlay()
        {
            var overlay = new Panel
            {
                BackColor = Color.FromArgb(250, 250, 250),
                Dock = DockStyle.Fill,
                Visible = false
            };

            var loadingLabel = new Label
            {
                Text = "Đang cập nhật dữ liệu...",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(59, 130, 246),
                AutoSize = true,
                BackColor = Color.FromArgb(250, 250, 250)
            };

            loadingLabel.Location = new Point(
                (this.Width - loadingLabel.PreferredWidth) / 2,
                (this.Height - loadingLabel.PreferredHeight) / 2
            );

            overlay.Controls.Add(loadingLabel);
            return overlay;
        }

        private async void ShowSuccessNotification(string message)
        {
            var notification = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(240, 253, 244),
                Size = new Size(450, 80),
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                Opacity = 0
            };

            notification.Region = System.Drawing.Region.FromHrgn(
                CreateRoundRectRgn(0, 0, notification.Width, notification.Height, 12, 12));

            var lblMessage = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(22, 163, 74),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(20, 0),
                Size = new Size(420, 80)
            };

            notification.Controls.Add(lblMessage);

            notification.Location = new Point(
                this.Location.X + this.Width - notification.Width - 30,
                this.Location.Y + this.Height - notification.Height - 80
            );

            notification.Show();

            for (double opacity = 0; opacity <= 1; opacity += 0.1)
            {
                notification.Opacity = opacity;
                await Task.Delay(20);
            }

            await Task.Delay(2500);

            for (double opacity = 1; opacity >= 0; opacity -= 0.1)
            {
                notification.Opacity = opacity;
                await Task.Delay(20);
            }

            notification.Close();
        }

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


        private async void BtnSeedData_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLPHANCONG, "Sinh dữ liệu test"))
                return;

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

            var confirm = MessageBox.Show(
                "CẢNH BÁO: Hành động này sẽ XÓA SẠCH phân công hiện tại của học kỳ này và sinh lại dữ liệu mẫu cho toàn trường.\n\n" +
                $"Học kỳ: {GetSelectedHocKyName()}\n\n" +
                "Bạn có chắc không?",
                "Xác nhận sinh dữ liệu test",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            btnSeedData.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            string report = string.Empty;

            try
            {
                report = await Task.Run(() =>
                {
                    var seedingService = new SeedingService();
                    return seedingService.SeedFullAssignments(maHocKy);
                });

                MessageBox.Show(
                    report,
                    "Kết quả sinh dữ liệu test",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadData((int?)maHocKy);
                LoadStatCards((int?)maHocKy);
                LoadFilters();

                ShowSuccessNotification("Đã sinh dữ liệu phân công thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi sinh dữ liệu test:\n\n{ex.Message}\n\n" +
                    (!string.IsNullOrEmpty(report) ? $"Chi tiết:\n{report}" : ""),
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
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
                    return null;
                }

                if (cbHocKyNamHoc.SelectedIndex >= 0 && cbHocKyNamHoc.SelectedIndex < cbHocKyNamHoc.Items.Count)
                {
                    var item = cbHocKyNamHoc.Items[cbHocKyNamHoc.SelectedIndex] as ComboBoxItem;
                    
                    if (item == null || item.Value == null)
                    {
                        return null;
                    }

                    string valueStr = item.Value.ToString();
                    
                    if (valueStr.StartsWith("NAM_"))
                    {
                        return null;
                    }
                    
                    if (int.TryParse(valueStr, out int maHK))
                    {
                        return maHK;
                    }
                }
            }
            catch
            {
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
            catch
            {
            }
        }


    }
}
