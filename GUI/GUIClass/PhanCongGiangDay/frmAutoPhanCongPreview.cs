using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Student_Management_System_CSharp_SGU2025.BUS.Services;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using MySql.Data.MySqlClient;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class frmAutoPhanCongPreview : Form
    {
        #region Fields
        private List<PhanCongCandidate> currentCandidates;
        private AssignmentAutoService autoService;
        private AssignmentPersistService persistService;
        private PhanCongGiangDayBUS phanCongBUS;
        private HocKyBUS hocKyBus;
        private MonHocDAO monHocDAO;
        private GiaoVienDAO giaoVienDAO;
        private LopDAO lopDAO;

        private int? selectedHocKyId = null;
        private bool isReadOnly = false;

        private Dictionary<int, MonHocDTO> monHocCache;
        private Dictionary<string, GiaoVienDTO> giaoVienCache;
        private List<GiaoVienDTO> allTeachers;
        #endregion

        #region Events
        public event EventHandler OnAssignmentAccepted;
        #endregion

        #region Constructor & Initialization
        public frmAutoPhanCongPreview()
        {
            InitializeComponent();

            // ✅ QUAN TRỌNG: Configure grid TRƯỚC khi load data
            ConfigureGridColumns();

            InitializeServices();
        }

        private void InitializeServices()
        {
            try
            {
                autoService = new AssignmentAutoService();
                persistService = new AssignmentPersistService();
                phanCongBUS = new PhanCongGiangDayBUS();
                hocKyBus = new HocKyBUS();

                monHocDAO = new MonHocDAO();
                giaoVienDAO = new GiaoVienDAO();
                lopDAO = new LopDAO();

                // ✅ Attach grid events TRƯỚC khi load data
                AttachGridEvents();

                // ✅ LOAD DATA CACHE - nếu fail vẫn cho form hiển thị
                bool cacheLoaded = LoadDataCache();

                if (cacheLoaded)
                {
                    LoadHocKyToComboBox();
                    LoadKhoiFilter();
                    LoadMonHocToFilters();
                    UpdateStatusMessage("📌 Chọn học kỳ và nhấn 'Tạo tự động' để bắt đầu", StatusType.Info);
                }
                else
                {
                    // Disable các nút khi không có data
                    SetButtonsState(false, false);
                    UpdateStatusMessage("⚠️ Không thể load dữ liệu. Vui lòng kiểm tra database.", StatusType.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}\n\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Ensure form vẫn có thể hiển thị
                try
                {
                    SetButtonsState(false, false);
                    UpdateStatusMessage("✗ Lỗi khởi tạo", StatusType.Error);
                }
                catch { }
            }
        }

        /// <summary>
        /// Load data cache - return false nếu fail
        /// </summary>
        private bool LoadDataCache()
        {
            try
            {
                // Initialize dictionaries
                monHocCache = new Dictionary<int, MonHocDTO>();
                giaoVienCache = new Dictionary<string, GiaoVienDTO>();
                allTeachers = new List<GiaoVienDTO>();

                // Load môn học
                var dsMonHoc = monHocDAO.DocDSMH();
                if (dsMonHoc == null || dsMonHoc.Count == 0)
                {
                    Console.WriteLine("⚠ Không có dữ liệu môn học");
                    return false;
                }

                foreach (var mon in dsMonHoc)
                {
                    monHocCache[mon.maMon] = mon;
                }

                // Load giáo viên
                try
                {
                    allTeachers = giaoVienDAO.DocDSGiaoVien();

                    if (allTeachers == null || allTeachers.Count == 0)
                    {
                        Console.WriteLine("⚠ Không có dữ liệu giáo viên");
                        return false;
                    }

                    foreach (var gv in allTeachers)
                    {
                        if (!string.IsNullOrEmpty(gv.MaGiaoVien))
                        {
                            giaoVienCache[gv.MaGiaoVien] = gv;
                        }
                    }

                    Console.WriteLine($"✅ Loaded {allTeachers.Count} teachers, {monHocCache.Count} subjects");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Lỗi load giáo viên: {ex.Message}");
                    MessageBox.Show(
                        $"Lỗi khi load danh sách giáo viên:\n\n{ex.Message}\n\n" +
                        "Có thể do dữ liệu NgaySinh không hợp lệ trong database.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi load cache: {ex.Message}");
                MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void frmAutoPhanCongPreview_Load(object sender, EventArgs e)
        {
            // Form initialized
        }
        #endregion

        #region Status & UI Helpers
        private enum StatusType
        {
            Info,
            Success,
            Warning,
            Error
        }

        private void UpdateStatusMessage(string message, StatusType type)
        {
            if (lblStatus != null)
            {
                lblStatus.Text = message;
                Color color;
                switch (type)
                {
                    case StatusType.Success:
                        color = Color.FromArgb(22, 163, 74);
                        break;
                    case StatusType.Warning:
                        color = Color.FromArgb(234, 88, 12);
                        break;
                    case StatusType.Error:
                        color = Color.FromArgb(220, 38, 38);
                        break;
                    case StatusType.Info:
                        color = Color.FromArgb(59, 130, 246);
                        break;
                    default:
                        color = Color.FromArgb(100, 116, 139);
                        break;
                }
                lblStatus.ForeColor = color;
            }
        }

        private void SetButtonsState(bool generateEnabled, bool actionButtonsEnabled)
        {
            if (btnGenerate != null) btnGenerate.Enabled = generateEnabled;
            if (btnValidate != null) btnValidate.Enabled = actionButtonsEnabled;
            if (btnSaveTemp != null) btnSaveTemp.Enabled = actionButtonsEnabled;
            if (btnAccept != null) btnAccept.Enabled = actionButtonsEnabled;
        }

        private void AttachGridEvents()
        {
            if (grid == null) return;

            grid.CellValueChanged += Grid_CellValueChanged;
            grid.CurrentCellDirtyStateChanged += Grid_CurrentCellDirtyStateChanged;
            grid.DataError += Grid_DataError;
            grid.KeyDown += Grid_KeyDown;
        }
        #endregion

        #region Semester Management
        private void LoadHocKyToComboBox()
        {
            try
            {
                if (cbHocKy == null) return;

                List<HocKyDTO> danhSachHocKy = SemesterHelper.GetEditableSemesters();

                cbHocKy.Items.Clear();
                cbHocKy.DisplayMember = "Text";
                cbHocKy.ValueMember = "Value";

                if (danhSachHocKy == null || danhSachHocKy.Count == 0)
                {
                    MessageBox.Show(
                        "⚠️ Không có học kỳ nào có thể tạo phân công!\n\n" +
                        "Chỉ có thể tạo phân công cho học kỳ HIỆN TẠI hoặc TƯƠNG LAI.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    SetButtonsState(false, false);
                    UpdateStatusMessage("⚠️ Không có học kỳ khả dụng", StatusType.Warning);
                    return;
                }

                foreach (var hk in danhSachHocKy)
                {
                    string trangThai = SemesterHelper.GetStatus(hk.MaHocKy);
                    string icon = trangThai == "Đang diễn ra" ? "🟢" : "🔵";
                    string displayText = $"{hk.TenHocKy} - {hk.MaNamHoc}";
                    
                    // Kiểm tra trạng thái phân công
                    bool hasOfficial = phanCongBUS.HasAssignmentsForSemester(hk.MaHocKy);
                    bool hasTemp = phanCongBUS.HasTempAssignmentsForSemester(hk.MaHocKy);
                    
                    string statusText = hasOfficial
                        ? " (ĐÃ PHÂN CÔNG)"
                        : (hasTemp ? " (ĐANG LƯU TẠM)" : " (CHƯA PHÂN)");

                    cbHocKy.Items.Add(new ComboBoxItem
                    {
                        Text = $"{icon} {displayText} ({trangThai}){statusText}",
                        Value = hk.MaHocKy
                    });
                }

                SelectCurrentSemester();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load danh sách học kỳ: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusMessage($"✗ Lỗi: {ex.Message}", StatusType.Error);
            }
        }

        private void SelectCurrentSemester()
        {
            if (cbHocKy == null || cbHocKy.Items.Count == 0) return;

            var hocKyHienTai = SemesterHelper.GetCurrentSemester();
            if (hocKyHienTai != null)
            {
                for (int i = 0; i < cbHocKy.Items.Count; i++)
                {
                    var item = cbHocKy.Items[i] as ComboBoxItem;
                    if (item != null && (int)item.Value == hocKyHienTai.MaHocKy)
                    {
                        cbHocKy.SelectedIndex = i;
                        return;
                    }
                }
            }

            cbHocKy.SelectedIndex = 0;
        }

        private void cbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKy.SelectedItem == null) return;

            var selectedItem = cbHocKy.SelectedItem as ComboBoxItem;
            if (selectedItem == null) return;

            int newHocKyId = (int)selectedItem.Value;
            
            // Kiểm tra nếu có dữ liệu tạm cho học kỳ này
            bool hasTemp = phanCongBUS.HasTempAssignmentsForSemester(newHocKyId);
            
            if (hasTemp && selectedHocKyId.HasValue && selectedHocKyId.Value != newHocKyId)
            {
                // Hỏi người dùng muốn tải lại dữ liệu tạm hay tạo mới
                var dialogResult = MessageBox.Show(
                    $"Học kỳ này đang có dữ liệu phân công tạm.\n\n" +
                    $"Bạn muốn:\n" +
                    $"• Tải lại dữ liệu tạm (Yes)\n" +
                    $"• Tạo mới (No)\n" +
                    $"• Hủy (Cancel)",
                    "Dữ liệu tạm tồn tại",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                
                if (dialogResult == DialogResult.Cancel)
                {
                    // Khôi phục selection cũ
                    for (int i = 0; i < cbHocKy.Items.Count; i++)
                    {
                        var item = cbHocKy.Items[i] as ComboBoxItem;
                        if (item != null && (int)item.Value == selectedHocKyId.Value)
                        {
                            cbHocKy.SelectedIndex = i;
                            return;
                        }
                    }
                    return;
                }
                
                if (dialogResult == DialogResult.Yes)
                {
                    // Tải lại từ PhanCong_Temp
                    LoadTempAssignments(newHocKyId);
                    selectedHocKyId = newHocKyId;
                    isReadOnly = !SemesterHelper.IsEditable(selectedHocKyId.Value);
                    SetButtonsState(true, true);
                    return;
                }
                // DialogResult.No: tiếp tục tạo mới (clear grid)
            }

            selectedHocKyId = newHocKyId;
            isReadOnly = !SemesterHelper.IsEditable(selectedHocKyId.Value);

            string status = SemesterHelper.GetStatus(selectedHocKyId.Value);
            UpdateStatusMessage($"📅 Học kỳ: {status} - Sẵn sàng tạo phân công", StatusType.Success);

            SetButtonsState(true, false);

            if (grid != null)
            {
                grid.ReadOnly = isReadOnly;
                if (grid.Columns.Contains("TenGiaoVien"))
                {
                    grid.Columns["TenGiaoVien"].ReadOnly = isReadOnly;
                }
            }

            LoadExistingAssignments();
        }
        
        /// <summary>
        /// Tải phân công tạm từ PhanCong_Temp
        /// </summary>
        private void LoadTempAssignments(int hocKyId)
        {
            try
            {
                const string sql = @"SELECT MaLop, MaGiaoVien, MaMonHoc, MaHocKy, SoTietTuan, Score, Note
                                    FROM PhanCong_Temp
                                    WHERE MaHocKy = @MaHocKy";
                
                currentCandidates = new List<PhanCongCandidate>();
                
                using (var conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", hocKyId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                currentCandidates.Add(new PhanCongCandidate
                                {
                                    MaLop = reader.GetInt32("MaLop"),
                                    MaGiaoVien = reader.GetString("MaGiaoVien"),
                                    MaMonHoc = reader.GetInt32("MaMonHoc"),
                                    SoTietTuan = reader.GetInt32("SoTietTuan"),
                                    Score = reader.IsDBNull(reader.GetOrdinal("Score")) ? 0 : reader.GetInt32("Score"),
                                    Note = reader.IsDBNull(reader.GetOrdinal("Note")) ? "" : reader.GetString("Note")
                                });
                            }
                        }
                    }
                }
                
                EnrichCandidatesWithNames(currentCandidates);
                RefreshGrid();
                
                UpdateStatusMessage($"📋 Đã tải {currentCandidates.Count} phân công tạm", StatusType.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu tạm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusMessage($"✗ Lỗi: {ex.Message}", StatusType.Error);
            }
        }
        #endregion

        #region Data Loading
        private void LoadExistingAssignments()
        {
            if (!selectedHocKyId.HasValue) return;

            try
            {
                var pcDao = new PhanCongGiangDayDAO();
                var existingAssignments = pcDao.LayPhanCongTheoHocKy(selectedHocKyId.Value);

                if (existingAssignments != null && existingAssignments.Count > 0)
                {
                    currentCandidates = existingAssignments.Select(pc => new PhanCongCandidate
                    {
                        MaLop = pc.MaLop,
                        MaMonHoc = pc.MaMonHoc,
                        MaGiaoVien = pc.MaGiaoVien,
                        SoTietTuan = GetSoTietFromMonHoc(pc.MaMonHoc),
                        Note = "✓ Phân công đã lưu"
                    }).ToList();

                    EnrichCandidatesWithNames(currentCandidates);
                    RefreshGrid();

                    UpdateStatusMessage($"📋 Đã có {currentCandidates.Count} phân công. Có thể tạo thêm.",
                        StatusType.Info);
                }
                else
                {
                    currentCandidates = new List<PhanCongCandidate>();
                    RefreshGrid();
                    UpdateStatusMessage("📌 Nhấn 'Tạo tự động' để bắt đầu", StatusType.Info);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải phân công: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatusMessage($"✗ Lỗi: {ex.Message}", StatusType.Error);
            }
        }

        private int GetSoTietFromMonHoc(int maMonHoc)
        {
            if (monHocCache != null && monHocCache.TryGetValue(maMonHoc, out var mon))
            {
                return mon.soTiet;
            }
            return 0;
        }

        /// <summary>
        /// Load khối 10, 11, 12 vào ComboBox
        /// </summary>
        private void LoadKhoiFilter()
        {
            try
            {
                if (cbKhoi == null) return;

                cbKhoi.Items.Clear();
                cbKhoi.Items.Add("Tất cả");
                cbKhoi.Items.Add("10");
                cbKhoi.Items.Add("11");
                cbKhoi.Items.Add("12");
                cbKhoi.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UpdateStatusMessage($"⚠ Lỗi load khối: {ex.Message}", StatusType.Warning);
            }
        }

        /// <summary>
        /// Load môn học với TÊN hiển thị (không phải mã)
        /// </summary>
        private void LoadMonHocToFilters()
        {
            try
            {
                if (cbMon == null) return;

                cbMon.Items.Clear();
                
                // Thêm ComboBoxItem với DisplayMember
                var allItem = new ComboBoxItem { Text = "Tất cả môn", Value = null };
                cbMon.Items.Add(allItem);

                if (monHocCache != null)
                {
                    foreach (var mon in monHocCache.Values.OrderBy(m => m.tenMon))
                    {
                        var item = new ComboBoxItem 
                        { 
                            Text = mon.tenMon,  // Hiển thị TÊN môn
                            Value = mon.maMon    // Giá trị là MÃ môn
                        };
                        cbMon.Items.Add(item);
                    }
                }

                cbMon.DisplayMember = "Text";
                cbMon.ValueMember = "Value";
                cbMon.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UpdateStatusMessage($"⚠ Lỗi load môn học: {ex.Message}", StatusType.Warning);
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

        private void RefreshGrid()
        {
            try
            {
                if (grid != null)
                {
                    grid.DataSource = null;
                    grid.DataSource = currentCandidates;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Lỗi refresh grid: {ex.Message}");
            }
        }

        private void EnrichCandidatesWithNames(List<PhanCongCandidate> candidates)
        {
            if (candidates == null) return;

            foreach (var c in candidates)
            {
                try
                {
                    var lop = lopDAO.LayLopTheoId(c.MaLop);
                    c.TenLop = lop?.tenLop ?? $"Lớp {c.MaLop}";

                    if (monHocCache != null && monHocCache.TryGetValue(c.MaMonHoc, out var mon))
                    {
                        c.TenMon = mon.tenMon;
                        if (c.SoTietTuan == 0)
                            c.SoTietTuan = mon.soTiet;
                    }
                    else
                    {
                        c.TenMon = $"Môn {c.MaMonHoc}";
                    }

                    if (giaoVienCache != null && giaoVienCache.TryGetValue(c.MaGiaoVien, out var gv))
                    {
                        c.TenGiaoVien = gv.HoTen;
                    }
                    else
                    {
                        c.TenGiaoVien = c.MaGiaoVien;
                    }

                    if (string.IsNullOrWhiteSpace(c.Note) || c.Note == "GVCN")
                    {
                        if (c.Note == "GVCN")
                        {
                            c.Note = $"✓ {c.TenGiaoVien} là GVCN của {c.TenLop}";
                        }
                        else if (string.IsNullOrWhiteSpace(c.Note))
                        {
                            c.Note = "Phân công dựa trên chuyên môn";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠ Lỗi enrich candidate: {ex.Message}");
                }
            }
        }
        #endregion

        #region Grid Configuration
        /// <summary>
        /// ✅ FIX: Configure grid columns - CHỈ GỌI 1 LẦN trong constructor
        /// </summary>
        private void ConfigureGridColumns()
        {
            if (grid == null) return;

            // ✅ CLEAR existing columns (nếu có từ Designer)
            grid.Columns.Clear();
            grid.AutoGenerateColumns = false;

            var colLop = new DataGridViewTextBoxColumn
            {
                Name = "TenLop",
                HeaderText = "Lớp học",
                DataPropertyName = "TenLop",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59)
                }
            };

            var colMon = new DataGridViewTextBoxColumn
            {
                Name = "TenMon",
                HeaderText = "Môn học",
                DataPropertyName = "TenMon",
                Width = 180,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(51, 65, 85)
                }
            };

            var colGV = new DataGridViewTextBoxColumn
            {
                Name = "TenGiaoVien",
                HeaderText = "Giáo viên phụ trách",
                DataPropertyName = "TenGiaoVien",
                Width = 220,
                ReadOnly = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(59, 130, 246),
                    BackColor = Color.FromArgb(239, 246, 255)
                }
            };

            var colSoTiet = new DataGridViewTextBoxColumn
            {
                Name = "SoTietTuan",
                HeaderText = "Số tiết/Hk",
                DataPropertyName = "SoTietTuan",
                Width = 110,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(220, 38, 38)
                }
            };

            var colNote = new DataGridViewTextBoxColumn
            {
                Name = "Note",
                HeaderText = "Ghi chú",
                DataPropertyName = "Note",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(100, 116, 139)
                }
            };

            grid.Columns.AddRange(new DataGridViewColumn[] { colLop, colMon, colGV, colSoTiet, colNote });

            Console.WriteLine($"✅ Grid columns configured: {grid.Columns.Count} columns");
        }
        #endregion

        #region Grid Events
        private void Grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grid != null && grid.IsCurrentCellDirty)
            {
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (grid == null || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (isReadOnly) return;

            if (grid.Columns[e.ColumnIndex].Name == "TenGiaoVien")
            {
                var row = grid.Rows[e.RowIndex];
                var candidate = row.DataBoundItem as PhanCongCandidate;
                if (candidate != null)
                {
                    string newTeacherName = row.Cells["TenGiaoVien"].Value?.ToString();
                    UpdateTeacherForCandidate(candidate, newTeacherName);
                }
            }
        }

        private void UpdateTeacherForCandidate(PhanCongCandidate candidate, string teacherName)
        {
            if (string.IsNullOrWhiteSpace(teacherName) || giaoVienCache == null) return;

            var teacher = giaoVienCache.Values.FirstOrDefault(gv =>
                gv.HoTen.Equals(teacherName, StringComparison.OrdinalIgnoreCase));

            if (teacher != null)
            {
                candidate.MaGiaoVien = teacher.MaGiaoVien;
                candidate.TenGiaoVien = teacher.HoTen;
                candidate.Note = "✏️ Đã chỉnh sửa thủ công";
            }
        }

        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            Console.WriteLine($"⚠ Grid DataError at row {e.RowIndex}: {e.Exception?.Message}");
            UpdateStatusMessage($"⚠ Lỗi dữ liệu ở dòng {e.RowIndex + 1}", StatusType.Warning);
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && !isReadOnly)
            {
                DeleteSelectedRows();
                e.Handled = true;
            }
        }

        private void DeleteSelectedRows()
        {
            if (grid == null || grid.SelectedRows.Count == 0) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa {grid.SelectedRows.Count} phân công đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            foreach (DataGridViewRow row in grid.SelectedRows)
            {
                var candidate = row.DataBoundItem as PhanCongCandidate;
                if (candidate != null && currentCandidates != null)
                {
                    currentCandidates.Remove(candidate);
                }
            }

            RefreshGrid();
            UpdateStatusMessage($"✓ Đã xóa {grid.SelectedRows.Count} phân công", StatusType.Success);
        }
        #endregion

        #region Auto Generation
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (!selectedHocKyId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn học kỳ trước!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (giaoVienCache == null || giaoVienCache.Count == 0)
            {
                MessageBox.Show(
                    "❌ Không có dữ liệu giáo viên!\n\n" +
                    "Không thể tạo phân công khi không có giáo viên.\n\n" +
                    "Vui lòng kiểm tra bảng GiaoVien trong database.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _ = GenerateAsync();
        }

        private async Task GenerateAsync()
        {
            try
            {
                UpdateStatusMessage("⏳ Đang tạo phân công tự động...", StatusType.Info);
                if (progressBar != null)
                {
                    progressBar.Visible = true;
                    progressBar.Value = 10;
                }

                // ✅ Đọc filter Khối
                int? khoi = null;
                if (cbKhoi != null && cbKhoi.SelectedItem != null && cbKhoi.SelectedItem.ToString() != "Tất cả")
                {
                    if (int.TryParse(cbKhoi.SelectedItem.ToString(), out int k)) khoi = k;
                }

                // ✅ Đọc filter Môn học (từ ComboBoxItem)
                string maMon = null;
                if (cbMon != null && cbMon.SelectedItem is ComboBoxItem monItem && monItem.Value != null)
                {
                    maMon = monItem.Value.ToString();
                }
                // ✅ Chỉ cho phép GV dạy đúng chuyên môn, không giới hạn tải
                var policy = new AssignmentPolicy
                {
                    MaxLoadPerTeacherPerWeek = int.MaxValue, // Không giới hạn tải
                    AllowNonPrimarySpecialty = false, // Chỉ cho phép GV dạy đúng chuyên môn
                    SpecialtyWeight = 10,
                    LoadBalanceWeight = 5
                };
                
                Console.WriteLine($"📋 Policy: MaxLoad={policy.MaxLoadPerTeacherPerWeek}, AllowNonPrimary={policy.AllowNonPrimarySpecialty}");

                await Task.Delay(50);
                if (progressBar != null) progressBar.Value = 35;

                Console.WriteLine($"🔄 Bắt đầu tạo phân công cho HocKy ID: {selectedHocKyId}");

                var res = await Task.Run(() => autoService.GenerateAutoAssignmentsFiltered(
                    selectedHocKyId.Value, policy, khoi, maMon));

                Console.WriteLine($"✅ Đã tạo {res.Candidates.Count} candidates");

                currentCandidates = res.Candidates;
                if (progressBar != null) progressBar.Value = 70;

                EnrichCandidatesWithNames(currentCandidates);
                RefreshGrid();

                if (progressBar != null)
                {
                    progressBar.Value = 100;
                    await Task.Delay(200);
                    progressBar.Visible = false;
                }

                if (res.Report.HardViolations > 0)
                {
                    UpdateStatusMessage($"⚠ Có {res.Report.HardViolations} vấn đề cần kiểm tra", StatusType.Warning);
                    MessageBox.Show(
                        string.Join("\n\n", res.Report.Messages),
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                else
                {
                    UpdateStatusMessage($"✓ Đã tạo {currentCandidates.Count} phân công thành công!", StatusType.Success);
                }

                SetButtonsState(true, true);
            }
            catch (Exception ex)
            {
                if (progressBar != null) progressBar.Visible = false;
                UpdateStatusMessage($"✗ Lỗi: {ex.Message}", StatusType.Error);
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Handlers
        private void BtnValidate_Click(object sender, EventArgs e)
        {
            if (currentCandidates == null || currentCandidates.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để kiểm tra.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var report = autoService.ValidateAutoAssignments(currentCandidates);
            if (report.HardViolations == 0)
            {
                UpdateStatusMessage($"✓ Kiểm tra OK! {currentCandidates.Count} phân công hợp lệ", StatusType.Success);
                MessageBox.Show(
                    $"Tất cả {currentCandidates.Count} phân công đều hợp lệ!\n\nBạn có thể lưu hoặc chấp nhận ngay.",
                    "Kiểm tra thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                UpdateStatusMessage($"⚠ Có {report.HardViolations} lỗi vi phạm", StatusType.Error);
                MessageBox.Show(
                    string.Join("\n\n", report.Messages),
                    "Lỗi vi phạm",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnSaveTemp_Click(object sender, EventArgs e)
        {
            if (!selectedHocKyId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentCandidates == null || currentCandidates.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu để lưu.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // ✅ Truyền hocKyId vào PersistTemporary
                persistService.PersistTemporary(currentCandidates, selectedHocKyId.Value);
                UpdateStatusMessage($"💾 Đã lưu tạm {currentCandidates.Count} phân công", StatusType.Success);
                
                // Refresh semester status in combo
                LoadHocKyToComboBox();
                
                MessageBox.Show(
                    $"Đã lưu tạm {currentCandidates.Count} phân công.\n\nBạn có thể xem lại và chỉnh sửa trước khi chấp nhận chính thức.",
                    "Lưu thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                UpdateStatusMessage($"✗ Lỗi lưu tạm: {ex.Message}", StatusType.Error);
                MessageBox.Show($"Lỗi khi lưu tạm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            if (!selectedHocKyId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Kiểm tra xem có dữ liệu trong memory hoặc trong PhanCong_Temp
            bool hasInMemory = currentCandidates != null && currentCandidates.Count > 0;
            bool hasInTemp = phanCongBUS.HasTempAssignmentsForSemester(selectedHocKyId.Value);

            if (!hasInMemory && !hasInTemp)
            {
                MessageBox.Show("Chưa có dữ liệu để chấp nhận!\n\nVui lòng tạo phân công trước.", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Nếu có dữ liệu trong memory nhưng chưa lưu tạm → tự động lưu tạm trước
            if (hasInMemory && !hasInTemp)
            {
                var saveFirst = MessageBox.Show(
                    $"⚠️ Dữ liệu chưa được lưu tạm!\n\n" +
                    $"Bạn có muốn lưu tạm trước khi chấp nhận không?\n\n" +
                    $"• Yes: Lưu tạm rồi chấp nhận (An toàn - có thể chỉnh sửa sau)\n" +
                    $"• No: Chấp nhận trực tiếp (Nhanh - không lưu tạm)\n" +
                    $"• Cancel: Hủy",
                    "Xác nhận",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (saveFirst == DialogResult.Cancel) return;

                if (saveFirst == DialogResult.Yes)
                {
                    // Lưu tạm trước
                    try
                    {
                        persistService.PersistTemporary(currentCandidates, selectedHocKyId.Value);
                        UpdateStatusMessage($"💾 Đã lưu tạm {currentCandidates.Count} phân công", StatusType.Info);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi lưu tạm: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else // DialogResult.No - Chấp nhận trực tiếp từ memory
                {
                    // Lưu tạm ngầm định trước khi chấp nhận (để đảm bảo dữ liệu đồng bộ)
                    try
                    {
                        persistService.PersistTemporary(currentCandidates, selectedHocKyId.Value);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi chuẩn bị dữ liệu: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            // ✅ Nếu có dữ liệu trong PhanCong_Temp nhưng khác với memory → hỏi người dùng muốn dùng cái nào
            else if (hasInTemp && hasInMemory)
            {
                // Kiểm tra xem dữ liệu có khác nhau không
                var tempCount = GetTempAssignmentsCount(selectedHocKyId.Value);
                if (tempCount != currentCandidates.Count)
                {
                    var choice = MessageBox.Show(
                        $"⚠️ Phát hiện dữ liệu khác nhau:\n\n" +
                        $"• Trong bộ nhớ: {currentCandidates.Count} phân công\n" +
                        $"• Đã lưu tạm: {tempCount} phân công\n\n" +
                        $"Bạn muốn chấp nhận:\n" +
                        $"• Yes: Dữ liệu đã lưu tạm ({tempCount} phân công)\n" +
                        $"• No: Dữ liệu hiện tại trong bộ nhớ ({currentCandidates.Count} phân công - sẽ cập nhật lại tạm)\n" +
                        $"• Cancel: Hủy",
                        "Chọn dữ liệu",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (choice == DialogResult.Cancel) return;

                    if (choice == DialogResult.No)
                    {
                        // Cập nhật lại tạm với dữ liệu hiện tại
                        try
                        {
                            persistService.PersistTemporary(currentCandidates, selectedHocKyId.Value);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi cập nhật dữ liệu tạm: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    // DialogResult.Yes: Dùng dữ liệu đã lưu tạm (không làm gì)
                }
            }

            // ✅ Đếm số phân công sẽ được chấp nhận
            int countToAccept = hasInTemp 
                ? GetTempAssignmentsCount(selectedHocKyId.Value) 
                : (currentCandidates?.Count ?? 0);

            var confirm = MessageBox.Show(
                $"✅ Bạn có chắc chắn muốn chấp nhận {countToAccept} phân công này?\n\n" +
                $"📋 Học kỳ: {cbHocKy.Text}\n" +
                $"📊 Số phân công: {countToAccept}\n\n" +
                "➡️ Dữ liệu sẽ được lưu vào bảng PhanCongGiangDay chính thức.\n" +
                "⚠️ Thao tác này không thể hoàn tác!",
                "Xác nhận chấp nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                // ✅ Chấp nhận từ PhanCong_Temp (đã được đảm bảo có dữ liệu)
                persistService.AcceptToOfficial(selectedHocKyId.Value);

                UpdateStatusMessage($"✓ Đã chấp nhận {countToAccept} phân công!", StatusType.Success);
                MessageBox.Show(
                    $"✅ Đã chấp nhận {countToAccept} phân công thành công!\n\n" +
                    "Dữ liệu đã được lưu vào bảng chính thức.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                currentCandidates = null;
                if (grid != null) grid.DataSource = null;
                SetButtonsState(true, false);

                // Refresh semester status
                LoadHocKyToComboBox();

                OnAssignmentAccepted?.Invoke(this, EventArgs.Empty);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                UpdateStatusMessage($"✗ Lỗi: {ex.Message}", StatusType.Error);
                MessageBox.Show($"Lỗi khi chấp nhận: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy số lượng phân công tạm cho học kỳ
        /// </summary>
        private int GetTempAssignmentsCount(int hocKyId)
        {
            try
            {
                const string sql = "SELECT COUNT(*) FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
                using (var conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", hocKyId);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        private void BtnRollback_Click(object sender, EventArgs e)
        {
            if (!selectedHocKyId.HasValue)
            {
                MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa phân công tạm của học kỳ này?\n\nThao tác này không thể hoàn tác.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                // Xóa chỉ cho học kỳ đã chọn
                const string sql = "DELETE FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
                using (var conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", selectedHocKyId.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                
                currentCandidates = new List<PhanCongCandidate>();
                RefreshGrid();
                
                // Refresh semester status
                LoadHocKyToComboBox();
                
                UpdateStatusMessage("🗑 Đã xóa phân công tạm", StatusType.Info);
                MessageBox.Show("Đã xóa phân công tạm thành công.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                UpdateStatusMessage($"✗ Lỗi xóa: {ex.Message}", StatusType.Error);
                MessageBox.Show($"Lỗi khi xóa phân công tạm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}