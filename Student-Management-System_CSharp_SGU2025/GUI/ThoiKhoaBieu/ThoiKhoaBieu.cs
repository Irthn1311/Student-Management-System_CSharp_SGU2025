using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.Scheduling;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.Utils;
using Student_Management_System_CSharp_SGU2025.Services;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Guna.UI2.WinForms;
using System.Text.RegularExpressions;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThoiKhoaBieu
{
    public partial class ThoiKhoaBieu : UserControl
    {
        private HocKyBUS hocKyBUS;
        private LopHocBUS lopBUS;
		private ThoiKhoaBieuBUS tkbBUS;
		private TKBExportService exportService;
		private GiaoVienBUS giaoVienBUS;
		private PhanLopBLL phanLopBLL;
		
		private int currentSemesterId = 0;
		private int currentLopId = 0;
		private string currentTeacherId = null;
		private string currentViewMode = "Thời khóa biểu lớp"; // "Thời khóa biểu lớp" or "Thời khóa biểu giảng dạy"
		private bool isLoading = false;
		private bool hasTKBForSemester = false;
		private Dictionary<string, Guna2Panel> gridPanels = new Dictionary<string, Guna2Panel>();
		private Guna2Panel dragSourcePanel = null;
		private ToolTip toolTipSlots;

		public ThoiKhoaBieu()
		{
			InitializeComponent();
			hocKyBUS = new HocKyBUS();
			lopBUS = new LopHocBUS();
			tkbBUS = new ThoiKhoaBieuBUS();
			giaoVienBUS = new GiaoVienBUS();
			phanLopBLL = new PhanLopBLL();
			exportService = new TKBExportService();
			toolTipSlots = new ToolTip();
			
			// Wire up export button
			btnXuatExcel.Click += BtnXuatExcel_Click;
		}

        private void ThoiKhoaBieu_Load(object sender, EventArgs e)
        {
            InitializeUI();
            ApplyPermissions();
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form
        /// </summary>
        private void ApplyPermissions()
        {
            try
            {
                // ✅ Kiểm tra quyền truy cập chức năng TRƯỚC
                if (!PermissionHelper.HasAccessToFunction(PermissionHelper.QLTKB))
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng 'Quản lý thời khóa biểu'!",
                                   "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Enabled = false;
                    return;
                }

                // ✅ Áp dụng phân quyền chi tiết cho các button
                PermissionHelper.ApplyPermissionThoiKhoaBieu(
                    btnSapXepTuDong,
                    null, // btnLuuDiem đã bị xóa
                    btnXoa
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng phân quyền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Initialize UI: Load danh sách Học kỳ và Lớp vào ComboBox
        /// </summary>
        private void InitializeUI()
        {
            try
            {
                isLoading = true;

                // Initialize view mode ComboBox
                InitializeViewModeComboBox();

                // Load Học kỳ
                LoadHocKyComboBox();

                // Load Lớp (disabled initially)
                LoadLopComboBox();
                cbLop.Enabled = false;

                // Load Giáo viên ComboBox
                LoadGiaoVienComboBox();
                cbGiaoVien.Visible = false;
                cbGiaoVien.Enabled = false;

                // Apply role-based UI restrictions
                ApplyRoleBasedTimetableView();

                // Disable action buttons initially
                btnXoa.Enabled = false;
                btnSapXepTuDong.Enabled = false;

				// Initialize grid with Guna2Panel controls
				InitGrid();
				lblContextInfo.Text = string.Empty;

                isLoading = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo giao diện: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Initialize view mode ComboBox with Vietnamese options
        /// </summary>
        private void InitializeViewModeComboBox()
        {
            cbViewMode.Items.Clear();
            cbViewMode.Items.Add("Thời khóa biểu lớp");
            cbViewMode.Items.Add("Thời khóa biểu giảng dạy");
            cbViewMode.SelectedIndex = 0;
            currentViewMode = "Thời khóa biểu lớp";
        }

        /// <summary>
        /// Load danh sách Học kỳ vào ComboBox
        /// </summary>
        private void LoadHocKyComboBox()
        {
            try
            {
                var dsHocKy = hocKyBUS.DocDSHocKy();
                cbHocKyNamHoc.Items.Clear();
                cbHocKyNamHoc.Items.Add("-- Chọn học kỳ --");

                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    foreach (var hk in dsHocKy)
                    {
                        cbHocKyNamHoc.Items.Add(hk.TenHocKy);
                    }
                    cbHocKyNamHoc.Tag = dsHocKy;
                }

                cbHocKyNamHoc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load học kỳ: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load danh sách Lớp vào ComboBox
        /// </summary>
        private void LoadLopComboBox()
        {
            try
            {
                var dsLop = lopBUS.DocDSLop();
                cbLop.Items.Clear();
                cbLop.Items.Add("-- Chọn lớp --");

                if (dsLop != null && dsLop.Count > 0)
                {
                    foreach (var lop in dsLop)
                    {
                        cbLop.Items.Add(lop.tenLop);
                    }
                    cbLop.Tag = dsLop;
                }

                cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load lớp: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load danh sách Giáo viên vào ComboBox
        /// </summary>
        private void LoadGiaoVienComboBox()
        {
            try
            {
                var dsGiaoVien = giaoVienBUS.DocDSGiaoVien();
                cbGiaoVien.Items.Clear();
                cbGiaoVien.Items.Add("-- Chọn giáo viên --");

                if (dsGiaoVien != null && dsGiaoVien.Count > 0)
                {
                    foreach (var gv in dsGiaoVien)
                    {
                        if (gv.TrangThai == "Đang giảng dạy" || string.IsNullOrEmpty(gv.TrangThai))
                        {
                            string displayText = $"{gv.HoTen} ({gv.MaGiaoVien})";
                            cbGiaoVien.Items.Add(displayText);
                        }
                    }
                    cbGiaoVien.Tag = dsGiaoVien;
                }

                cbGiaoVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load giáo viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Role Detection Helpers

        /// <summary>
        /// Get current teacher ID from logged in user
        /// </summary>
        private string GetCurrentTeacherId()
        {
            if (!SessionManager.IsLoggedIn())
                return null;

            string tenDangNhap = SessionManager.TenDangNhap;
            if (string.IsNullOrEmpty(tenDangNhap))
                return null;

            // Teacher username format: "GV001"
            if (tenDangNhap.StartsWith("GV", StringComparison.OrdinalIgnoreCase))
            {
                return tenDangNhap;
            }

            return null;
        }

        /// <summary>
        /// Get current student ID from logged in user
        /// </summary>
        private int? GetCurrentStudentId()
        {
            if (!SessionManager.IsLoggedIn())
                return null;

            string tenDangNhap = SessionManager.TenDangNhap;
            if (string.IsNullOrEmpty(tenDangNhap))
                return null;

            // Student username format: "HS101"
            if (tenDangNhap.StartsWith("HS", StringComparison.OrdinalIgnoreCase))
            {
                string maHocSinhStr = tenDangNhap.Substring(2);
                if (int.TryParse(maHocSinhStr, out int maHocSinh))
                {
                    return maHocSinh;
                }
            }

            return null;
        }

        /// <summary>
        /// Get student's class ID for current semester
        /// </summary>
        private int? GetStudentClass(int semesterId)
        {
            int? maHocSinh = GetCurrentStudentId();
            if (!maHocSinh.HasValue || semesterId <= 0)
                return null;

            try
            {
                int maLop = phanLopBLL.GetLopByHocSinh(maHocSinh.Value, semesterId);
                return maLop > 0 ? (int?)maLop : null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Check if current user is Admin (has CREATE/UPDATE/DELETE permissions)
        /// </summary>
        private bool IsAdmin()
        {
            return PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.CREATE) ||
                   PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.UPDATE) ||
                   PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.DELETE);
        }

        /// <summary>
        /// Check if current user is a teacher
        /// </summary>
        private bool IsTeacher()
        {
            return GetCurrentTeacherId() != null;
        }

        /// <summary>
        /// Check if current user is a student or parent
        /// </summary>
        private bool IsStudentOrParent()
        {
            if (!SessionManager.IsLoggedIn())
                return false;

            string tenDangNhap = SessionManager.TenDangNhap;
            if (string.IsNullOrEmpty(tenDangNhap))
                return false;

            return tenDangNhap.StartsWith("HS", StringComparison.OrdinalIgnoreCase) ||
                   tenDangNhap.StartsWith("PH", StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        /// <summary>
        /// Apply role-based UI restrictions and behavior
        /// </summary>
        private void ApplyRoleBasedTimetableView()
        {
            try
            {
                bool isAdmin = IsAdmin();
                bool isTeacher = IsTeacher();
                bool isStudentOrParent = IsStudentOrParent();
                string teacherId = GetCurrentTeacherId();

                // Admin: Full access
                if (isAdmin)
                {
                    cbViewMode.Enabled = true;
                    cbLop.Enabled = true;
                    cbGiaoVien.Enabled = true;
                    cbViewMode.SelectedIndex = 0; // Default to class view
                    return;
                }

                // Teacher: Restricted access
                if (isTeacher && !string.IsNullOrEmpty(teacherId))
                {
                    // Check if homeroom teacher
                    int? homeroomClassId = tkbBUS.GetHomeroomClassIdForTeacher(teacherId);
                    bool isHomeroomTeacher = homeroomClassId.HasValue;

                    if (isHomeroomTeacher)
                    {
                        // Homeroom teacher: Can view both teacher and homeroom class
                        cbViewMode.Enabled = true;
                        cbViewMode.Items.Clear();
                        cbViewMode.Items.Add("Thời khóa biểu lớp");
                        cbViewMode.Items.Add("Thời khóa biểu giảng dạy");

                        // Pre-select homeroom class in class view mode
                        if (cbViewMode.SelectedIndex == 0) // Class view
                        {
                            SelectClassInComboBox(homeroomClassId.Value);
                        }

                        // Pre-select current teacher in teacher view mode
                        SelectTeacherInComboBox(teacherId);
                        cbGiaoVien.Enabled = false; // Lock to own teacher
                    }
                    else
                    {
                        // Subject teacher only: Only teacher view
                        cbViewMode.Items.Clear();
                        cbViewMode.Items.Add("Thời khóa biểu giảng dạy");
                        cbViewMode.SelectedIndex = 0;
                        cbViewMode.Enabled = false; // Lock to teacher view

                        SelectTeacherInComboBox(teacherId);
                        cbGiaoVien.Enabled = false;
                        cbLop.Enabled = false;
                        cbLop.Visible = false;
                        cbGiaoVien.Visible = true;
                    }

                    // Disable auto-generate and delete for teachers
                    btnSapXepTuDong.Visible = false;
                    btnSapXepTuDong.Enabled = false;
                    btnXoa.Visible = false;
                    btnXoa.Enabled = false;
                    return;
                }

                // Student/Parent: Read-only, class view only
                if (isStudentOrParent)
                {
                    cbViewMode.Items.Clear();
                    cbViewMode.Items.Add("Thời khóa biểu lớp");
                    cbViewMode.SelectedIndex = 0;
                    cbViewMode.Enabled = false; // Lock to class view
                    cbGiaoVien.Visible = false;
                    cbGiaoVien.Enabled = false;

                    // Disable all editing features for students/parents
                    btnSapXepTuDong.Visible = false;
                    btnSapXepTuDong.Enabled = false;
                    btnXoa.Visible = false;
                    btnXoa.Enabled = false;

                    // Will pre-select student's class when semester is selected
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng phân quyền theo vai trò: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Helper: Select a class in the ComboBox by MaLop
        /// </summary>
        private void SelectClassInComboBox(int maLop)
        {
            var dsLop = cbLop.Tag as List<LopDTO>;
            if (dsLop == null) return;

            for (int i = 0; i < dsLop.Count; i++)
            {
                if (dsLop[i].maLop == maLop)
                {
                    cbLop.SelectedIndex = i + 1; // +1 because index 0 is placeholder
                    return;
                }
            }
        }

        /// <summary>
        /// Helper: Select a teacher in the ComboBox by MaGiaoVien
        /// </summary>
        private void SelectTeacherInComboBox(string maGiaoVien)
        {
            var dsGiaoVien = cbGiaoVien.Tag as List<GiaoVienDTO>;
            if (dsGiaoVien == null) return;

            for (int i = 0; i < dsGiaoVien.Count; i++)
            {
                if (dsGiaoVien[i].MaGiaoVien == maGiaoVien)
                {
                    cbGiaoVien.SelectedIndex = i + 1; // +1 because index 0 is placeholder
                    currentTeacherId = maGiaoVien;
                    return;
                }
            }
        }

        /// <summary>
        /// Sự kiện khi thay đổi Học kỳ
        /// </summary>
        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            try
            {
                ClearAllPanels();
                
                int selectedIndex = cbHocKyNamHoc.SelectedIndex;
                
                // Index 0 = placeholder
                if (selectedIndex <= 0)
                {
                    ResetState();
                    return;
                }

                // Lấy học kỳ từ Tag
                var dsHocKy = cbHocKyNamHoc.Tag as List<HocKyDTO>;
                if (dsHocKy == null || selectedIndex > dsHocKy.Count)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy dữ liệu học kỳ.", "Lỗi");
                    return;
                }

                var selectedHK = dsHocKy[selectedIndex - 1];
                currentSemesterId = selectedHK.MaHocKy;
                string tenHocKy = selectedHK.TenHocKy;
				lblContextInfo.Text = tenHocKy;

                // Kiểm tra xem học kỳ này đã có TKB chưa
                hasTKBForSemester = tkbBUS.HasScheduleForSemester(currentSemesterId);

                if (hasTKBForSemester)
                {
                    LoadData(currentSemesterId);
                    
                    // Apply role-based restrictions after semester is selected
                    ApplyRoleBasedTimetableView();
                    
                    // For students/parents: Pre-select their class
                    if (IsStudentOrParent())
                    {
                        int? studentClassId = GetStudentClass(currentSemesterId);
                        if (studentClassId.HasValue)
                        {
                            SelectClassInComboBox(studentClassId.Value);
                            cbLop.Enabled = false; // Lock to student's class
                        }
                    }
                    
                    if (PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.CREATE))
                    {
                        btnSapXepTuDong.Enabled = true;
                        btnSapXepTuDong.Text = "Tạo lại TKB";
                    }
                    
                    if (PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.DELETE))
                    {
                        btnXoa.Enabled = true;
                    }

                    
                }
                else
                {
                    cbLop.Enabled = false;
                    cbLop.SelectedIndex = 0;

                    if (PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.CREATE))
                    {
                        btnSapXepTuDong.Enabled = true;
                        btnSapXepTuDong.Text = "Sắp xếp tự động";
                    }

                    if (btnXoa.Visible)
                        btnXoa.Enabled = false;

                   

                    MessageBox.Show(
                        $"Học kỳ '{tenHocKy}' chưa có Thời khóa biểu.\n\n" +
                        $"📌 Vui lòng nhấn nút 'Sắp xếp tự động' để tạo TKB.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn học kỳ: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset state về ban đầu
        /// </summary>
        private void ResetState()
        {
            currentSemesterId = 0;
            hasTKBForSemester = false;
         
            cbLop.Enabled = false;
            cbLop.SelectedIndex = 0;
            btnSapXepTuDong.Enabled = false;
            
            if (btnXoa.Visible)
                btnXoa.Enabled = false;
        }

        /// <summary>
        /// Sự kiện khi thay đổi Lớp
        /// </summary>
        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (currentSemesterId == 0) return;

            try
            {
                int selectedIndex = cbLop.SelectedIndex;
                
                // Index 0 = placeholder
                if (selectedIndex <= 0)
                {
                    foreach (var panel in gridPanels.Values)
                    {
                        panel.Visible = true;
                    }
                    currentLopId = 0;
                    
                    var selectedHK = GetSelectedHocKy();
                    
                    return;
                }

                // Lấy lớp từ Tag
                var dsLop = cbLop.Tag as List<LopDTO>;
                if (dsLop == null || selectedIndex > dsLop.Count)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy dữ liệu lớp.", "Lỗi");
                    return;
                }

                var selectedLop = dsLop[selectedIndex - 1];
                currentLopId = selectedLop.maLop;
                
                
                // Load all data and filter by class BEFORE populating grid
                var allSlots = tkbBUS.GetTKBViewByHocKy(currentSemesterId);
                if (allSlots != null && allSlots.Count > 0)
                {
                    ClearAllPanels();
                    // Filter slots by selected class BEFORE populating
                    var filteredSlots = allSlots.Where(s => s.MaLop == currentLopId).ToList();
                    PopulateGridFromSlots(filteredSlots);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn lớp: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi thay đổi View Mode (Class/Teacher)
        /// </summary>
        private void cbViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            try
            {
                int selectedIndex = cbViewMode.SelectedIndex;
                if (selectedIndex < 0) return;

                currentViewMode = cbViewMode.Items[selectedIndex].ToString();

                // Show/hide and enable/disable appropriate ComboBoxes
                if (currentViewMode == "Thời khóa biểu lớp")
                {
                    cbLop.Visible = true;
                    cbLop.Enabled = true;
                    cbGiaoVien.Visible = false;
                    cbGiaoVien.Enabled = false;

                    // Auto-load timetable if class is already selected
                    if (cbLop.SelectedIndex > 0 && currentSemesterId > 0)
                    {
                        cbLop_SelectedIndexChanged(cbLop, EventArgs.Empty);
                    }
                }
                else if (currentViewMode == "Thời khóa biểu giảng dạy")
                {
                    cbLop.Visible = false;
                    cbLop.Enabled = false;
                    cbGiaoVien.Visible = true;
                    cbGiaoVien.Enabled = true;

                    // Auto-load timetable if teacher is already selected
                    if (cbGiaoVien.SelectedIndex > 0 && currentSemesterId > 0)
                    {
                        cbGiaoVien_SelectedIndexChanged(cbGiaoVien, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thay đổi chế độ xem: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện khi thay đổi Giáo viên
        /// </summary>
        private void cbGiaoVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (currentSemesterId == 0) return;

            try
            {
                int selectedIndex = cbGiaoVien.SelectedIndex;
                if (selectedIndex <= 0)
                {
                    ClearAllPanels();
                    return;
                }

                var dsGiaoVien = cbGiaoVien.Tag as List<GiaoVienDTO>;
                if (dsGiaoVien == null || selectedIndex > dsGiaoVien.Count)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy dữ liệu giáo viên.", "Lỗi");
                    return;
                }

                var selectedGV = dsGiaoVien[selectedIndex - 1];
                currentTeacherId = selectedGV.MaGiaoVien;
                
                

                LoadTimetableForTeacher(currentSemesterId, currentTeacherId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Helper: Lấy HocKyDTO đang chọn
        /// </summary>
        private HocKyDTO GetSelectedHocKy()
        {
            int selectedIndex = cbHocKyNamHoc.SelectedIndex;
            if (selectedIndex <= 0) return null;

            var dsHocKy = cbHocKyNamHoc.Tag as List<HocKyDTO>;
            if (dsHocKy == null || selectedIndex > dsHocKy.Count) return null;

            return dsHocKy[selectedIndex - 1];
        }

        /// <summary>
        /// Nút "Sắp xếp tự động" → Mở Form Preview để cấu hình
        /// </summary>
        private void btnGenerateAuto_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLTKB, "Thời khóa biểu"))
                return;

            if (currentSemesterId == 0)
            {
                MessageBox.Show("Vui lòng chọn Học kỳ trước!",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbHocKyNamHoc.Focus();
                return;
            }

            try
            {
                using (var frmPreview = new FrmAutoTKBPreview(currentSemesterId))
                {
                    if (frmPreview.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh UI sau khi tạo TKB thành công
                        hasTKBForSemester = true;
                        cbLop.Enabled = true;
                        btnSapXepTuDong.Text = "Tạo lại TKB";

                        if (PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.DELETE))
                        {
                            btnXoa.Enabled = true;
                        }

                      
                        
                        LoadData(currentSemesterId);

                        MessageBox.Show(
                            "✅ Thời khóa biểu đã được tạo thành công!\n\n" +
                            "Bạn có thể chọn lớp từ dropdown để xem TKB chi tiết.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở Form Preview: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút "Xóa" → Xóa toàn bộ TKB của học kỳ
        /// </summary>
        private void btnRollback_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckDeletePermission(PermissionHelper.QLTKB, "Thời khóa biểu"))
                return;

            if (currentSemesterId == 0)
            {
                MessageBox.Show("Vui lòng chọn Học kỳ trước!",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa toàn bộ thời khóa biểu của học kỳ này?\n\n" +
                "⚠ Thao tác này không thể hoàn tác!",
                "Xác nhận xóa TKB",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Delete all TKB records for this semester
                const string deleteSql = @"
                    DELETE tkb FROM ThoiKhoaBieu tkb
                    JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
                    WHERE pc.MaHocKy = @MaHocKy";

                using (var conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(deleteSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", currentSemesterId);
                        int rowsDeleted = cmd.ExecuteNonQuery();
                        
                        MessageBox.Show(
                            $"✅ Đã xóa {rowsDeleted} bản ghi thời khóa biểu.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                // Refresh UI
                hasTKBForSemester = false;
                cbLop.Enabled = false;
                cbLop.SelectedIndex = 0;
                ClearAllPanels();
                
               

                btnSapXepTuDong.Text = "Sắp xếp tự động";
                if (btnXoa.Visible)
                    btnXoa.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể xóa thời khóa biểu:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #region Grid Initialization & Data Loading

        /// <summary>
        /// Initialize the Days x Periods grid (hiện tại: Thứ 2 → Thứ 7, 10 tiết/ngày) với Guna2Panel controls
        /// </summary>
        private void InitGrid()
        {
            try
            {
                gridPanels.Clear();
                
                // Clear existing panels (except spacer panel)
                var controlsToRemove = new List<Control>();
                foreach (Control ctrl in tableThoiKhoaBieu.Controls)
                {
                    if (ctrl is Guna2Panel && ctrl.Name != "pnl_Spacer")
                    {
                        controlsToRemove.Add(ctrl);
                    }
                }
                foreach (var ctrl in controlsToRemove)
                {
                    tableThoiKhoaBieu.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }

                // Remove spacer panel if exists (will be recreated)
                var spacerPanel = tableThoiKhoaBieu.Controls.Cast<Control>().FirstOrDefault(c => c.Name == "pnl_Spacer");
                if (spacerPanel != null)
                {
                    tableThoiKhoaBieu.Controls.Remove(spacerPanel);
                    spacerPanel.Dispose();
                }

                // Create panels for each slot: Days 2-7 (columns 1-6), Periods 1-10
                // Row mapping: Header (row 0), Tiết 1-5 (rows 1-5), Spacer row (row 6), Tiết 6-10 (rows 7-11)
                // So actual row indices: Tiết 1=1, Tiết 2=2, Tiết 3=3, Tiết 4=4, Tiết 5=5, Tiết 6=7, Tiết 7=8, Tiết 8=9, Tiết 9=10, Tiết 10=11
                int[] rowMapping = { 1, 2, 3, 4, 5, 7, 8, 9, 10, 11 }; // Map tiet 1-10 to actual table rows (skip spacer row 6)
                
                for (int thu = 2; thu <= 7; thu++)
                {
                    for (int tiet = 1; tiet <= 10; tiet++)
                    {
                        // Check if user has permission to edit (Admin only typically)
                        bool canEdit = PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.UPDATE) ||
                                      PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.CREATE);

                        var panel = new Guna2Panel
                        {
                            Name = $"pnl_Thu{thu}_Tiet{tiet}",
                            BorderRadius = 4,
                            FillColor = Color.White,
                            BorderColor = Color.FromArgb(229, 231, 235),
                            BorderThickness = 1,
                            AllowDrop = canEdit,
                            Dock = DockStyle.Fill,
                            Margin = new Padding(2)
                        };

                        // Add subtle visual separator after period 5 (between morning and afternoon)
                        if (tiet == 6)
                        {
                            panel.BorderThickness = 2;
                            panel.BorderColor = Color.FromArgb(203, 213, 225); // Slightly darker border for visual separation
                        }

                        panel.ShadowDecoration.Enabled = false; // Disable shadow for cleaner look

                        var label = new Guna2HtmlLabel
                        {
                            Name = $"lbl_Thu{thu}_Tiet{tiet}",
                            Text = "<span style='color:#CCCCCC; font-size:10pt'>...</span>",
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            Padding = new Padding(8, 5, 8, 5),
                            TextAlignment = ContentAlignment.MiddleCenter,
                            AllowDrop = false
                        };
                        panel.Controls.Add(label);

                        // Attach drag-and-drop events only if user can edit
                        if (canEdit)
                        {
                            panel.MouseDown += Pnl_MouseDown;
                            panel.DragEnter += Pnl_DragEnter;
                            panel.DragLeave += Pnl_DragLeave;
                            panel.DragDrop += Pnl_DragDrop;
                        }

                        int col = thu - 1;
                        int row = rowMapping[tiet - 1]; // Direct mapping: Tiết 1-10 to rows 1-10

                        tableThoiKhoaBieu.Controls.Add(panel, col, row);
                        gridPanels[panel.Name] = panel;
                    }
                }

                // Create spacer panel for row 6 (between morning and afternoon sessions)
                // Span across all columns (0-6) to create a clean separator without borders
                var spacerPanelNew = new Guna2Panel
                {
                    Name = "pnl_Spacer",
                    BorderRadius = 0,
                    FillColor = Color.Transparent,
                    BorderColor = Color.Transparent,
                    BorderThickness = 0,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0)
                };
                spacerPanelNew.ShadowDecoration.Enabled = false;
                tableThoiKhoaBieu.Controls.Add(spacerPanelNew, 0, 6);
                tableThoiKhoaBieu.SetColumnSpan(spacerPanelNew, 7); // Span all 7 columns (Tiết + 6 days)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo lưới: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load timetable data and populate the grid based on current view mode
        /// </summary>
        private void LoadData(int maHocKy)
        {
            try
            {
                ClearAllPanels();

                if (currentViewMode == "Thời khóa biểu giảng dạy" && !string.IsNullOrEmpty(currentTeacherId))
                {
                    LoadTimetableForTeacher(maHocKy, currentTeacherId);
                    return;
                }

                // Class view mode: Load all data, but filter by selected class if one is selected
                var slots = tkbBUS.GetTKBViewByHocKy(maHocKy);
                
                if (slots == null || slots.Count == 0)
                {
                    return;
                }

                // If a class is selected, filter slots by that class BEFORE populating
                if (currentViewMode == "Thời khóa biểu lớp" && currentLopId > 0)
                {
                    slots = slots.Where(s => s.MaLop == currentLopId).ToList();
                }

                PopulateGridFromSlots(slots);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu TKB: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load timetable for a specific teacher
        /// </summary>
        private void LoadTimetableForTeacher(int maHocKy, string maGiaoVien)
        {
            try
            {
                ClearAllPanels();

                var slots = tkbBUS.GetTKBByTeacher(maHocKy, maGiaoVien);
                
                if (slots == null || slots.Count == 0)
                {
                    return;
                }

                PopulateGridFromSlots(slots, isTeacherView: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load TKB giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Populate grid from list of TimeTableSlotDTO
        /// </summary>
        private void PopulateGridFromSlots(List<TimeTableSlotDTO> slots, bool isTeacherView = false)
        {
            foreach (var slot in slots)
            {
                string panelName = $"pnl_Thu{slot.Thu}_Tiet{slot.Tiet}";
                
                if (gridPanels.TryGetValue(panelName, out Guna2Panel panel))
                {
                    panel.Tag = slot;

                    var label = panel.Controls.OfType<Guna2HtmlLabel>().FirstOrDefault();
                    if (label != null)
                    {
                        if (isTeacherView)
                        {
                            // Teacher view: Lớp (bold) + Môn (nhỏ, xám)
                            label.Text = $"<b style='font-size:11pt; color:#1F2937'>{slot.TenLop}</b><br>" +
                                         $"<span style='font-size:9pt; color:#6B7280'>{slot.TenMon}</span>";
                        }
                        else
                        {
                            // Class view: Môn (bold) + Giáo viên (nhỏ, xám) – KHÔNG lặp lại tên lớp
                            label.Text = $"<b style='font-size:11pt; color:#1F2937'>{slot.TenMon}</b><br>" +
                                         $"<span style='font-size:9pt; color:#6B7280'>{slot.TenGiaoVien}</span>";
                        }

                        // Tooltip chi tiết
                        string tooltipText = isTeacherView
                            ? $"Lớp: {slot.TenLop}\nMôn: {slot.TenMon}\nGiáo viên: {slot.TenGiaoVien}"
                            : $"Môn: {slot.TenMon}\nGiáo viên: {slot.TenGiaoVien}\nLớp: {slot.TenLop}";

                        toolTipSlots.SetToolTip(panel, tooltipText);
                    }

                    panel.FillColor = GetColorForSubject(slot.TenMon);
                    panel.BorderColor = Color.FromArgb(229, 231, 235);
                    panel.BorderThickness = 1;
                }
            }
        }

        /// <summary>
        /// Filter displayed slots by class ID
        /// </summary>
        private void FilterByClass(int maLop)
        {
            if (maLop <= 0)
            {
                foreach (var panel in gridPanels.Values)
                {
                    panel.Visible = true;
                }
                return;
            }

            try
            {
                foreach (var kvp in gridPanels)
                {
                    var panel = kvp.Value;
                    var slot = panel.Tag as TimeTableSlotDTO;

                    panel.Visible = (slot != null && slot.MaLop == maLop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc theo lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Clear all panels to empty state
        /// </summary>
        private void ClearAllPanels()
        {
            foreach (var panel in gridPanels.Values)
            {
                panel.Tag = null;
                panel.FillColor = Color.White; // White background for empty slot
                panel.Visible = true;
                
                var label = panel.Controls.OfType<Guna2HtmlLabel>().FirstOrDefault();
                if (label != null)
                {
                    label.Text = "<span style='color:#CCCCCC; font-size:10pt'>...</span>";
                }

                toolTipSlots.SetToolTip(panel, "Trống");
            }
        }

        #endregion

        #region Drag & Drop Handlers

        private void Pnl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // Check permission before allowing drag
            if (!PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.UPDATE))
                return;

            var panel = sender as Guna2Panel;
            if (panel == null || panel.Tag == null) return;

            dragSourcePanel = panel;
            panel.DoDragDrop(panel.Tag, DragDropEffects.Move);
        }

        private void Pnl_DragEnter(object sender, DragEventArgs e)
        {
            var panel = sender as Guna2Panel;
            if (panel == null) return;

            if (e.Data.GetDataPresent(typeof(TimeTableSlotDTO)))
            {
                e.Effect = DragDropEffects.Move;
                panel.BorderColor = Color.DeepSkyBlue;
                panel.BorderThickness = 2;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Pnl_DragLeave(object sender, EventArgs e)
        {
            var panel = sender as Guna2Panel;
            if (panel == null) return;

            panel.BorderColor = Color.FromArgb(213, 218, 223);
            panel.BorderThickness = 1;
        }

        private void Pnl_DragDrop(object sender, DragEventArgs e)
        {
            var targetPanel = sender as Guna2Panel;
            if (targetPanel == null) return;

            targetPanel.BorderColor = Color.FromArgb(213, 218, 223);
            targetPanel.BorderThickness = 1;

            if (!e.Data.GetDataPresent(typeof(TimeTableSlotDTO)))
            {
                return;
            }

            var sourceSlot = e.Data.GetData(typeof(TimeTableSlotDTO)) as TimeTableSlotDTO;
            if (sourceSlot == null) return;

            if (!ParsePanelName(targetPanel.Name, out int targetThu, out int targetTiet))
            {
                MessageBox.Show("Lỗi: Không thể xác định vị trí đích.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sourceSlot.Thu == targetThu && sourceSlot.Tiet == targetTiet)
            {
                return;
            }

            try
            {
                var result = tkbBUS.ValidateAndMove(
                    sourceSlot.MaPhanCong,
                    targetThu,
                    targetTiet,
                    sourceSlot.MaThoiKhoaBieu
                );

                if (!result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Không thể di chuyển",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                sourceSlot.Thu = targetThu;
                sourceSlot.Tiet = targetTiet;

                if (!tkbBUS.SaveTimetableChange(sourceSlot))
                {
                    MessageBox.Show("Lỗi khi lưu thay đổi vào cơ sở dữ liệu.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MoveSlotUI(dragSourcePanel, targetPanel, sourceSlot);
                LoadData(currentSemesterId);
                FilterByClass(currentLopId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý di chuyển: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dragSourcePanel = null;
            }
        }

        private void MoveSlotUI(Guna2Panel sourcePanel, Guna2Panel targetPanel, TimeTableSlotDTO slot)
        {
            var sourceLabel = sourcePanel.Controls.OfType<Guna2HtmlLabel>().FirstOrDefault();
            var targetLabel = targetPanel.Controls.OfType<Guna2HtmlLabel>().FirstOrDefault();

            if (sourceLabel == null || targetLabel == null) return;

            Color sourceColor = sourcePanel.FillColor;

            targetPanel.Tag = slot;
            targetLabel.Text = sourceLabel.Text;
            targetPanel.FillColor = sourceColor;

            sourcePanel.Tag = null;
            sourceLabel.Text = "";
            sourcePanel.FillColor = Color.White;
        }

        #endregion

        #region Helper Methods

        private bool ParsePanelName(string panelName, out int thu, out int tiet)
        {
            thu = 0;
            tiet = 0;

            var match = Regex.Match(panelName, @"pnl_Thu(\d+)_Tiet(\d+)");
            if (!match.Success || match.Groups.Count < 3)
            {
                return false;
            }

            if (!int.TryParse(match.Groups[1].Value, out thu) ||
                !int.TryParse(match.Groups[2].Value, out tiet))
            {
                return false;
            }

            return true;
        }

        private Color GetColorForSubject(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                return Color.White;
            }

            string normalized = subject.Trim().ToLower();

            var colorMap = new Dictionary<string, Color>
            {
                { "toán", Color.FromArgb(239, 246, 255) },
                { "toán học", Color.FromArgb(239, 246, 255) },
                { "vật lý", Color.FromArgb(255, 247, 237) },
                { "vật lí", Color.FromArgb(255, 247, 237) },
                { "hóa học", Color.FromArgb(253, 242, 248) },
                { "sinh học", Color.FromArgb(240, 253, 250) },
                { "sinh", Color.FromArgb(240, 253, 250) },
                { "ngữ văn", Color.FromArgb(240, 253, 244) },
                { "tiếng anh", Color.FromArgb(245, 243, 255) },
                { "anh văn", Color.FromArgb(245, 243, 255) },
                { "lịch sử", Color.FromArgb(254, 252, 232) },
                { "địa lý", Color.FromArgb(238, 242, 255) },
                { "địa lí", Color.FromArgb(238, 242, 255) },
                { "gdcd", Color.FromArgb(254, 242, 242) },
                { "giáo dục công dân", Color.FromArgb(254, 242, 242) },
                { "thể dục", Color.FromArgb(220, 252, 231) },
                { "giáo dục thể chất", Color.FromArgb(220, 252, 231) },
                { "quốc phòng", Color.FromArgb(241, 245, 249) },
                { "gdqp", Color.FromArgb(241, 245, 249) },
                { "giáo dục quốc phòng và an ninh", Color.FromArgb(241, 245, 249) },
                { "tin học", Color.FromArgb(241, 245, 249) },
                { "công nghệ", Color.FromArgb(241, 245, 249) },
                { "cn", Color.FromArgb(241, 245, 249) },
                { "khtn", Color.FromArgb(240, 253, 250) },
                { "chào cờ", Color.FromArgb(254, 252, 232) },
                { "shl", Color.FromArgb(254, 252, 232) },
                { "hđtn", Color.FromArgb(245, 243, 255) }
            };

            if (colorMap.TryGetValue(normalized, out Color predefinedColor))
            {
                return predefinedColor;
            }

            return GenerateColorFromString(subject);
        }

        private Color GenerateColorFromString(string text)
        {
            int hash = text.GetHashCode();
            int r = Math.Abs(hash) % 100 + 200;
            int g = Math.Abs(hash * 2) % 100 + 200;
            int b = Math.Abs(hash * 3) % 100 + 200;

            r = Math.Min(255, Math.Max(200, r));
            g = Math.Min(255, Math.Max(200, g));
            b = Math.Min(255, Math.Max(200, b));

            return Color.FromArgb(r, g, b);
        }

        #endregion

        #region Export Excel Event Handlers

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            if (currentSemesterId == 0)
            {
                MessageBox.Show("Vui lòng chọn Học kỳ trước!",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var contextMenu = new ContextMenuStrip();
            var menuItemClass = new ToolStripMenuItem("Xuất theo Lớp");
            var menuItemTeacher = new ToolStripMenuItem("Xuất theo Giáo viên");

            menuItemClass.Click += (s, args) => ExportToExcel("Class");
            menuItemTeacher.Click += (s, args) => ExportToExcel("Teacher");

            contextMenu.Items.Add(menuItemClass);
            contextMenu.Items.Add(menuItemTeacher);

            var button = sender as Guna2Button;
            if (button != null)
            {
                contextMenu.Show(button, new System.Drawing.Point(0, button.Height));
            }
            else
            {
                contextMenu.Show(Cursor.Position);
            }
        }

        private void ExportToExcel(string exportType)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                    saveDialog.FilterIndex = 1;
                    saveDialog.FileName = $"ThoiKhoaBieu_{exportType}_{currentSemesterId}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                    saveDialog.DefaultExt = "xlsx";
                    saveDialog.AddExtension = true;

                    if (saveDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    Cursor.Current = Cursors.WaitCursor;

                    if (exportType == "Class")
                    {
                        exportService.ExportClassSchedule(currentSemesterId, saveDialog.FileName);
                    }
                    else if (exportType == "Teacher")
                    {
                        exportService.ExportTeacherSchedule(currentSemesterId, saveDialog.FileName);
                    }

                    MessageBox.Show(
                        $"Đã xuất thời khóa biểu thành công!\n\n" +
                        $"File: {saveDialog.FileName}\n" +
                        $"Loại: {(exportType == "Class" ? "Theo Lớp" : "Theo Giáo viên")}",
                        "Xuất Excel thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show(
                    $"Không thể lưu file. File có thể đang được mở bởi ứng dụng khác.\n\nChi tiết: {ioEx.Message}",
                    "Lỗi xuất file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "Không có quyền truy cập thư mục được chọn.\n\nVui lòng chọn thư mục khác.",
                    "Lỗi quyền truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi xuất Excel:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        // Empty event handlers (keep for Designer compatibility)
        private void guna2HtmlLabel25_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel6_Click(object sender, EventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }

        private void lblBuoiChieu_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel26_Click(object sender, EventArgs e)
        {

        }

        private void tableThoiKhoaBieu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
