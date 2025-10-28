using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.Scheduling;
using System.Threading;
using Student_Management_System_CSharp_SGU2025.BUS;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThoiKhoaBieu
{
    public partial class ThoiKhoaBieu : UserControl
    {
        public ThoiKhoaBieu()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ThoiKhoaBieu_Load);
        }

        private void ThoiKhoaBieu_Load(object sender, EventArgs e)
        {
           
            
        }

        private void guna2HtmlLabel25_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ThoiKhoaBieu_Load_1(object sender, EventArgs e)
        {
            // On load, we could fetch temp or official week and render if needed.
            RenderFromTemp(1, 1);
        }

        // Hook: Generate Auto TKB
        private void btnGenerateAuto_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int semesterId = 1; // TODO: bind from UI
                int weekNo = 1;     // TODO: bind from UI

                var service = new SchedulingService();
                var req = service.BuildRequestFromDatabase(semesterId, weekNo);
                if (req.Assignments == null || req.Assignments.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu phân công trong học kỳ này.");
                    return;
                }

                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(req.TimeBudgetSec + 5)))
                {
                    var sol = service.GenerateSchedule(req, cts.Token);
                    if (!service.ValidateHardConstraints(sol))
                    {
                        MessageBox.Show("Còn vi phạm cứng. Hệ thống vẫn sẽ lưu vào tạm để bạn xem.", "Scheduling", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    service.PersistToTemp(semesterId, weekNo, sol);
                    RenderFromTemp(semesterId, weekNo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sinh TKB: {ex.Message}", "Scheduling", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int semesterId = 1;
                int weekNo = 1;
                var service = new SchedulingService();
                service.AcceptToOfficial(semesterId, weekNo);
                MessageBox.Show("Đã lưu thời khóa biểu chính thức.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lưu lịch chính thức: {ex.Message}");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new SchedulingService();
                service.RollbackTemp();
                tableThoiKhoaBieu.Controls.Clear();
                MessageBox.Show("Đã xóa lịch tạm.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể xóa lịch tạm: {ex.Message}");
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
                    // Nền xanh dương rất nhạt
                    return (Color.FromArgb(30, 64, 175), Color.FromArgb(96, 165, 250), Color.FromArgb(239, 246, 255));

                case "Vật lý":
                    // Nền cam rất nhạt
                    return (Color.FromArgb(154, 52, 18), Color.FromArgb(251, 146, 60), Color.FromArgb(255, 247, 237));

                case "Tiếng Anh":
                    // Nền tím rất nhạt
                    return (Color.FromArgb(107, 33, 168), Color.FromArgb(192, 132, 252), Color.FromArgb(245, 243, 255));

                case "Sinh học":
                    // Nền xanh ngọc rất nhạt
                    return (Color.FromArgb(17, 94, 89), Color.FromArgb(45, 212, 191), Color.FromArgb(240, 253, 250));

                case "Hóa học":
                    // Nền hồng rất nhạt
                    return (Color.FromArgb(157, 23, 77), Color.FromArgb(244, 114, 182), Color.FromArgb(253, 242, 248));

                case "Ngữ văn":
                    // Nền xanh lá rất nhạt
                    return (Color.FromArgb(22, 101, 52), Color.FromArgb(74, 222, 128), Color.FromArgb(240, 253, 244));

                case "Lịch sử":
                    // Nền vàng rất nhạt
                    return (Color.FromArgb(133, 77, 14), Color.FromArgb(250, 204, 21), Color.FromArgb(254, 252, 232));

                case "Địa lý":
                    // Nền chàm rất nhạt
                    return (Color.FromArgb(55, 48, 163), Color.FromArgb(129, 140, 248), Color.FromArgb(238, 242, 255));

                case "GDCD":
                    // Nền đỏ rất nhạt
                    return (Color.FromArgb(153, 27, 27), Color.FromArgb(248, 113, 113), Color.FromArgb(254, 242, 242));

                case "Tự học":
                    // Nền xám rất nhạt
                    return (Color.Black, Color.FromArgb(209, 213, 219), Color.FromArgb(249, 250, 251));

                case "Thể dục":
                    // Nền xanh lá cây rất nhạt
                    return (Color.FromArgb(21, 128, 61), Color.FromArgb(74, 222, 128), Color.FromArgb(220, 252, 231));

                case "Quốc phòng":
                    // Nền xám xanh rất nhạt
                    return (Color.FromArgb(71, 85, 105), Color.FromArgb(148, 163, 184), Color.FromArgb(241, 245, 249));

                case "Tin học":
                    // Nền xám đậm rất nhạt
                    return (Color.FromArgb(15, 23, 42), Color.FromArgb(100, 116, 139), Color.FromArgb(241, 245, 249));

                // Màu mặc định cho các môn không có trong danh sách
                default:
                    return (Color.Black, Color.Gainsboro, Color.WhiteSmoke);
            }
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

