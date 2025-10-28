using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.Scheduling;
using System.Threading;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThoiKhoaBieu
{
    public partial class ThoiKhoaBieu : UserControl
    {
        private HocKyBUS hocKyBUS;
        private LopHocBUS lopBUS;
        private ThoiKhoaBieuBUS tkbBUS;
        
        private int currentSemesterId = 0;
        private int currentLopId = 0;
        private bool isLoading = false;
        private bool hasTKBForSemester = false;

        public ThoiKhoaBieu()
        {
            InitializeComponent();
            hocKyBUS = new HocKyBUS();
            lopBUS = new LopHocBUS();
            tkbBUS = new ThoiKhoaBieuBUS();
        }

        private void ThoiKhoaBieu_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void ThoiKhoaBieu_Load_1(object sender, EventArgs e)
        {
            // Removed duplicate - handled in ThoiKhoaBieu_Load
        }

        /// <summary>
        /// Initialize UI: Load danh sách Học kỳ và Lớp vào ComboBox
        /// </summary>
        private void InitializeUI()
        {
            try
            {
                isLoading = true;

                // Load Học kỳ
                LoadHocKyComboBox();

                // Load Lớp (disabled initially)
                LoadLopComboBox();
                cbLop.Enabled = false; // Disable until semester with TKB is selected

                // Disable action buttons initially
                btnLuuDiem.Enabled = false;
                btnXoa.Enabled = false;
                btnSapXepTuDong.Enabled = false;

                // Clear grid
                tableThoiKhoaBieu.Controls.Clear();
                lblTenThoiKhoaBieu.Text = "Thời khóa biểu";

                isLoading = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo giao diện: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                
                // Add placeholder
                cbHocKyNamHoc.Items.Add("-- Chọn học kỳ --");

                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    foreach (var hk in dsHocKy)
                    {
                        // Guna2ComboBox: Thêm text trực tiếp, lưu tag riêng
                        cbHocKyNamHoc.Items.Add(hk.TenHocKy);
                    }
                    
                    // Store actual data in Tag for lookup
                    cbHocKyNamHoc.Tag = dsHocKy;
                }

                cbHocKyNamHoc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load học kỳ: {ex.Message}\n\nChi tiết: {ex.StackTrace}", "Lỗi", 
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
                
                // Add placeholder
                cbLop.Items.Add("-- Chọn lớp --");

                if (dsLop != null && dsLop.Count > 0)
                {
                    foreach (var lop in dsLop)
                    {
                        cbLop.Items.Add(lop.tenLop);
                    }
                    
                    // Store actual data in Tag for lookup
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
        /// Sự kiện khi thay đổi Học kỳ
        /// LOGIC MỚI: Chọn HK → Kiểm tra đã có TKB chưa → Enable/Disable cbLop
        /// </summary>
        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            try
            {
                tableThoiKhoaBieu.Controls.Clear();
                
                int selectedIndex = cbHocKyNamHoc.SelectedIndex;
                
                // Index 0 = placeholder "-- Chọn học kỳ --"
                if (selectedIndex <= 0)
                {
                    // Reset state
                    currentSemesterId = 0;
                    hasTKBForSemester = false;
                    lblTenThoiKhoaBieu.Text = "Thời khóa biểu";
                    cbLop.Enabled = false;
                    cbLop.SelectedIndex = 0;
                    btnSapXepTuDong.Enabled = false;
                    btnLuuDiem.Enabled = false;
                    btnXoa.Enabled = false;
                    return;
                }

                // Lấy học kỳ từ Tag (danh sách đã load)
                var dsHocKy = cbHocKyNamHoc.Tag as List<HocKyDTO>;
                if (dsHocKy == null || selectedIndex > dsHocKy.Count)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy dữ liệu học kỳ.", "Lỗi");
                    return;
                }

                // selectedIndex - 1 vì index 0 là placeholder
                var selectedHK = dsHocKy[selectedIndex - 1];
                currentSemesterId = selectedHK.MaHocKy;
                string tenHocKy = selectedHK.TenHocKy;

                lblTenThoiKhoaBieu.Text = $"Thời khóa biểu - {tenHocKy}";

                // Kiểm tra xem học kỳ này đã có TKB chưa
                hasTKBForSemester = tkbBUS.HasScheduleForSemester(currentSemesterId);

                if (hasTKBForSemester)
                {
                    // ĐÃ có TKB → Enable cbLop để lọc theo lớp
                    cbLop.Enabled = true;
                    btnSapXepTuDong.Enabled = true;
                    btnSapXepTuDong.Text = "Tạo lại TKB";
                    btnXoa.Enabled = true; // Có thể xóa TKB tạm
                    
                    lblTenThoiKhoaBieu.Text = $"✓ {tenHocKy} (Đã có TKB - Chọn lớp để xem)";
                    lblTenThoiKhoaBieu.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else
                {
                    // CHƯA có TKB → Disable cbLop, hiện thông báo
                    cbLop.Enabled = false;
                    cbLop.SelectedIndex = 0;
                    btnSapXepTuDong.Enabled = true;
                    btnSapXepTuDong.Text = "Sắp xếp tự động";
                    btnLuuDiem.Enabled = false;
                    btnXoa.Enabled = false;

                    lblTenThoiKhoaBieu.Text = $"⚠ {tenHocKy} (Chưa có TKB)";
                    lblTenThoiKhoaBieu.ForeColor = Color.FromArgb(234, 88, 12);
                    
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
        /// Sự kiện khi thay đổi Lớp
        /// LOGIC MỚI: Chọn lớp → Hiển thị TKB của lớp đó
        /// </summary>
        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (currentSemesterId == 0) return;

            try
            {
                int selectedIndex = cbLop.SelectedIndex;
                
                // Index 0 = placeholder "-- Chọn lớp --"
                if (selectedIndex <= 0)
                {
                    tableThoiKhoaBieu.Controls.Clear();
                    currentLopId = 0;
                    
                    var selectedHK = GetSelectedHocKy();
                    lblTenThoiKhoaBieu.Text = $"Thời khóa biểu - {selectedHK?.TenHocKy} (Chọn lớp để xem)";
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
                
                // Hiển thị TKB của lớp đó
                lblTenThoiKhoaBieu.Text = $"Thời khóa biểu {selectedLop.tenLop}";
                lblTenThoiKhoaBieu.ForeColor = Color.FromArgb(30, 41, 59);
                LoadTKBByClass(currentSemesterId, currentLopId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn lớp: {ex.Message}", "Lỗi", 
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
        /// Helper: Lấy LopDTO đang chọn
        /// </summary>
        private LopDTO GetSelectedLop()
        {
            int selectedIndex = cbLop.SelectedIndex;
            if (selectedIndex <= 0) return null;

            var dsLop = cbLop.Tag as List<LopDTO>;
            if (dsLop == null || selectedIndex > dsLop.Count) return null;

            return dsLop[selectedIndex - 1];
        }

        /// <summary>
        /// Load và hiển thị TKB của 1 lớp cụ thể
        /// </summary>
        private void LoadTKBByClass(int semesterId, int maLop)
        {
            try
            {
                tableThoiKhoaBieu.Controls.Clear();

                // Try temp first, then official
                var slots = tkbBUS.GetWeekByClass(semesterId, 1, maLop);
                if (slots == null || slots.Count == 0)
                {
                    // Try official
                    slots = tkbBUS.GetOfficialSchedule(semesterId, maLop);
                }

                if (slots == null || slots.Count == 0)
                {
                    MessageBox.Show("Lớp này chưa có TKB.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RenderSlots(slots);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load TKB lớp: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút "Sắp xếp tự động" → Mở Form Preview để cấu hình
        /// </summary>
        private void btnGenerateAuto_Click(object sender, EventArgs e)
        {
            // Validate selection
            if (currentSemesterId == 0)
            {
                MessageBox.Show("Vui lòng chọn Học kỳ trước!", 
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbHocKyNamHoc.Focus();
                    return;
                }

            // Mở Form Preview để cấu hình
            try
            {
                using (var frmPreview = new FrmAutoTKBPreview(currentSemesterId))
                {
                    if (frmPreview.ShowDialog() == DialogResult.OK)
                    {
                        // Sau khi tạo TKB thành công, refresh UI
                        hasTKBForSemester = true;
                        cbLop.Enabled = true;
                        btnSapXepTuDong.Text = "Tạo lại TKB";
                        btnXoa.Enabled = true;
                        
                        var selectedHK = GetSelectedHocKy();
                        lblTenThoiKhoaBieu.Text = $"✓ {selectedHK?.TenHocKy} (Đã có TKB - Chọn lớp để xem)";
                        lblTenThoiKhoaBieu.ForeColor = Color.FromArgb(22, 163, 74);
                        
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
        /// Nút "Lưu thời khóa biểu" → Publish TKB (khóa không sửa được)
        /// </summary>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (currentSemesterId == 0)
            {
                MessageBox.Show("Vui lòng chọn Học kỳ!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn lưu Thời khóa biểu này vào hệ thống chính thức?\n\n" +
                "⚠ Sau khi lưu, TKB sẽ được áp dụng và không thể chỉnh sửa dễ dàng.",
                "Xác nhận lưu TKB",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int semesterId = currentSemesterId;
                int weekNo = 1;
                
                var service = new SchedulingService();
                service.AcceptToOfficial(semesterId, weekNo);
                
                btnLuuDiem.Enabled = false;
                btnXoa.Enabled = false;
                
                MessageBox.Show(
                    "✅ Đã lưu Thời khóa biểu chính thức!\n\n" +
                    "TKB đã được publish và có thể xem/in ấn.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể lưu lịch chính thức:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Nút "Xóa" → Rollback TKB tạm
        /// </summary>
        private void btnRollback_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa lịch tạm?\n\n" +
                "⚠ Thao tác này sẽ xóa TKB đang preview. Bạn sẽ cần tạo lại.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var service = new SchedulingService();
                service.RollbackTemp();
                tableThoiKhoaBieu.Controls.Clear();
                btnLuuDiem.Enabled = false;
                btnXoa.Enabled = false;
                
                // Recheck if semester still has TKB
                if (currentSemesterId > 0)
                {
                    hasTKBForSemester = tkbBUS.HasScheduleForSemester(currentSemesterId);
                    if (!hasTKBForSemester)
                    {
                        cbLop.Enabled = false;
                        cbLop.SelectedIndex = 0;
                        btnSapXepTuDong.Text = "Sắp xếp tự động";
                    }
                }
                
                MessageBox.Show(
                    "🗑 Đã xóa lịch tạm thành công.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể xóa lịch tạm:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Render danh sách slots vào lưới TKB
        /// </summary>
        private void RenderSlots(List<AssignmentSlot> slots)
        {
            tableThoiKhoaBieu.Controls.Clear();

            // Initialize DAOs for lookup
            var monDao = new MonHocBUS();
            var gvDao = new GiaoVienBUS();

            foreach (var s in slots)
            {
                // Get subject and teacher names
                string tenMon = "Môn " + s.MaMon;
                string tenGV = s.MaGV;
                
                try
                {
                    var mon = monDao.LayDSMonHocTheoId(s.MaMon);
                    if (mon != null) tenMon = mon.tenMon;
                    
                    var gv = gvDao.LayGiaoVienTheoMa(s.MaGV);
                    if (gv != null) tenGV = gv.HoTen;
                }
                catch
                {
                    // Fallback to IDs if lookup fails
                }

                var card = new StatCardTKB();
                var colorSet = GetColorSetForSubject(tenMon);
                card.SetData(
                    tenMon,
                    tenGV,
                    string.IsNullOrEmpty(s.Phong) ? "Phòng TBA" : s.Phong,
                    colorSet.TextColor,
                    colorSet.ProgressColor1,
                    colorSet.ProgressColor2
                );
                card.Dock = DockStyle.Fill;
                card.Margin = new Padding(5);
                
                // Map Thu (2-6) to grid column (1-5), Tiet (1-10) to row (1-10)
                int col = s.Thu - 1;  // Thu 2 -> col 1, Thu 6 -> col 5
                int row = s.Tiet;     // Tiet 1 -> row 1, Tiet 10 -> row 10
                
                tableThoiKhoaBieu.Controls.Add(card, col, row);
            }
        }

		private void RenderFromTemp(int semesterId, int weekNo)
		{
			tableThoiKhoaBieu.Controls.Clear();
			var bus = new ThoiKhoaBieuBUS();
			var slots = bus.GetWeek(semesterId, weekNo);

			// Initialize DAOs for lookup
			var monDao = new MonHocBUS();
			var gvDao = new GiaoVienBUS();

			foreach (var s in slots)
			{
				// Get subject and teacher names
				string tenMon = "Môn " + s.MaMon;
				string tenGV = s.MaGV;
				
				try
				{
					var mon = monDao.LayDSMonHocTheoId(s.MaMon);
					if (mon != null) tenMon = mon.tenMon;
					
					var gv = gvDao.LayGiaoVienTheoMa(s.MaGV);
					if (gv != null) tenGV = gv.HoTen;
				}
				catch
				{
					// Fallback to IDs if lookup fails
				}

				var card = new StatCardTKB();
				var colorSet = GetColorSetForSubject(tenMon);
				card.SetData(
					tenMon,
					tenGV,
					string.IsNullOrEmpty(s.Phong) ? "Phòng TBA" : s.Phong,
					colorSet.TextColor,
					colorSet.ProgressColor1,
					colorSet.ProgressColor2
				);
				card.Dock = DockStyle.Fill;
				card.Margin = new Padding(5);
				
				// Map Thu (2-7) to grid column (1-6), Tiet (1-10) to row (1-10)
				int col = s.Thu - 1;  // Thu 2 -> col 1, Thu 7 -> col 6
				int row = s.Tiet;     // Tiet 1 -> row 1, Tiet 10 -> row 10
				
				tableThoiKhoaBieu.Controls.Add(card, col, row);
			}
		}

        private (Color TextColor, Color ProgressColor1, Color ProgressColor2) GetColorSetForSubject(string subject)
        {
            switch (subject)
            {
                case "Toán học":
                case "Toán":
                    return (Color.FromArgb(30, 64, 175), Color.FromArgb(96, 165, 250), Color.FromArgb(239, 246, 255));

                case "Vật lý":
                case "Vật Lý":
                    return (Color.FromArgb(154, 52, 18), Color.FromArgb(251, 146, 60), Color.FromArgb(255, 247, 237));

                case "Tiếng Anh":
                    return (Color.FromArgb(107, 33, 168), Color.FromArgb(192, 132, 252), Color.FromArgb(245, 243, 255));

                case "Sinh học":
                    return (Color.FromArgb(17, 94, 89), Color.FromArgb(45, 212, 191), Color.FromArgb(240, 253, 250));

                case "Hóa học":
                    return (Color.FromArgb(157, 23, 77), Color.FromArgb(244, 114, 182), Color.FromArgb(253, 242, 248));

                case "Ngữ văn":
                    return (Color.FromArgb(22, 101, 52), Color.FromArgb(74, 222, 128), Color.FromArgb(240, 253, 244));

                case "Lịch sử":
                    return (Color.FromArgb(133, 77, 14), Color.FromArgb(250, 204, 21), Color.FromArgb(254, 252, 232));

                case "Địa lý":
                    return (Color.FromArgb(55, 48, 163), Color.FromArgb(129, 140, 248), Color.FromArgb(238, 242, 255));

                case "GDCD":
                case "Giáo Dục Công Dân":
                    return (Color.FromArgb(153, 27, 27), Color.FromArgb(248, 113, 113), Color.FromArgb(254, 242, 242));

                case "Tự học":
                    return (Color.Black, Color.FromArgb(209, 213, 219), Color.FromArgb(249, 250, 251));

                case "Thể dục":
                case "Giáo dục thể chất":
                    return (Color.FromArgb(21, 128, 61), Color.FromArgb(74, 222, 128), Color.FromArgb(220, 252, 231));

                case "Quốc phòng":
                case "Giáo dục Quốc phòng và An ninh":
                    return (Color.FromArgb(71, 85, 105), Color.FromArgb(148, 163, 184), Color.FromArgb(241, 245, 249));

                case "Tin học":
                case "Công nghệ":
                    return (Color.FromArgb(15, 23, 42), Color.FromArgb(100, 116, 139), Color.FromArgb(241, 245, 249));

                default:
                    return (Color.Black, Color.Gainsboro, Color.WhiteSmoke);
            }
        }

        // Empty event handlers (keep for Designer compatibility)
        private void guna2HtmlLabel25_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel6_Click(object sender, EventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
    }
}
