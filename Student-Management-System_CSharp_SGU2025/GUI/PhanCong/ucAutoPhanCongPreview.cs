using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.Services;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.BUS;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI.PhanCong
{
	public class ucAutoPhanCongPreview : UserControl
	{
		private Guna2DataGridView grid;
		private Guna2Button btnGenerate;
		private Guna2Button btnValidate;
		private Guna2Button btnSaveTemp;
		private Guna2Button btnAccept;
		private Guna2Button btnRollback;
		private Guna2Panel panelButtons;
		private Guna2HtmlLabel lblTitle;
		private Guna2HtmlLabel lblStatus;
		private Guna2ProgressBar progressBar;

		private List<PhanCongCandidate> current;
		private AssignmentAutoService autoService;
		private AssignmentPersistService persistService;

		public ucAutoPhanCongPreview()
		{
			autoService = new AssignmentAutoService();
			persistService = new AssignmentPersistService();
			InitializeComponents();
		}

		private void InitializeComponents()
		{
			this.SuspendLayout();

			// Main container
			this.Size = new Size(900, 650);
			this.BackColor = Color.FromArgb(240, 240, 240);
			this.Padding = new Padding(15);

			// Title
			lblTitle = new Guna2HtmlLabel
			{
				Text = "<b>Auto Phân Công Giảng Dạy - Preview & Chỉnh Sửa</b>",
				Font = new Font("Segoe UI", 14F, FontStyle.Bold),
				ForeColor = Color.FromArgb(30, 41, 59),
				Location = new Point(20, 15),
				AutoSize = true
			};

			// Status Label
			lblStatus = new Guna2HtmlLabel
			{
				Text = "Nhấn 'Auto Generate' để tạo đề xuất phân công",
				Font = new Font("Segoe UI", 10F),
				ForeColor = Color.FromArgb(100, 116, 139),
				Location = new Point(20, 50),
				AutoSize = true
			};

			// Progress Bar
			progressBar = new Guna2ProgressBar
			{
				Location = new Point(20, 80),
				Size = new Size(860, 10),
				Visible = false,
				ProgressColor = Color.FromArgb(30, 136, 229),
				ProgressColor2 = Color.FromArgb(59, 130, 246)
			};

			// DataGridView
			grid = new Guna2DataGridView
			{
				Location = new Point(20, 110),
				Size = new Size(860, 420),
				AutoGenerateColumns = false,
				AllowUserToAddRows = false,
				AllowUserToDeleteRows = false,
				ReadOnly = false,
				SelectionMode = DataGridViewSelectionMode.FullRowSelect,
				BackgroundColor = Color.White,
				BorderStyle = BorderStyle.None,
				RowHeadersVisible = false,
				ColumnHeadersHeight = 40,
				RowTemplate = { Height = 35 },
				EnableHeadersVisualStyles = false
			};

			// Configure Grid Columns
			ConfigureGridColumns();

		// Button Panel
		panelButtons = new Guna2Panel
		{
			Location = new Point(20, 545),
			Size = new Size(860, 55),
			FillColor = Color.White,
			BorderRadius = 8
		};
		panelButtons.ShadowDecoration.Enabled = true;

			// Buttons
			btnGenerate = CreateStyledButton("Auto Generate", new Point(10, 10), 140, Color.FromArgb(75, 85, 99));
			btnValidate = CreateStyledButton("Kiểm tra", new Point(160, 10), 110, Color.FromArgb(234, 88, 12));
			btnSaveTemp = CreateStyledButton("Lưu tạm", new Point(280, 10), 110, Color.FromArgb(30, 136, 229));
			btnAccept = CreateStyledButton("✓ Chấp nhận", new Point(400, 10), 130, Color.FromArgb(22, 163, 74));
			btnRollback = CreateStyledButton("✗ Hủy tạm", new Point(540, 10), 110, Color.FromArgb(220, 38, 38));

			btnGenerate.Click += BtnGenerate_Click;
			btnValidate.Click += BtnValidate_Click;
			btnSaveTemp.Click += BtnSaveTemp_Click;
			btnAccept.Click += BtnAccept_Click;
			btnRollback.Click += BtnRollback_Click;

			panelButtons.Controls.AddRange(new Control[] { btnGenerate, btnValidate, btnSaveTemp, btnAccept, btnRollback });

			// Add all controls
			this.Controls.AddRange(new Control[] { lblTitle, lblStatus, progressBar, grid, panelButtons });

			// Initially disable action buttons
			SetActionButtonsEnabled(false);

			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private Guna2Button CreateStyledButton(string text, Point location, int width, Color fillColor)
		{
			return new Guna2Button
			{
				Text = text,
				Location = location,
				Size = new Size(width, 35),
				BorderRadius = 6,
				Font = new Font("Segoe UI", 9F, FontStyle.Bold),
				ForeColor = Color.White,
				FillColor = fillColor,
				Cursor = Cursors.Hand
			};
		}

		private void ConfigureGridColumns()
		{
			grid.Columns.Clear();

			// Column: Lớp
			var colLop = new DataGridViewTextBoxColumn
			{
				Name = "MaLop",
				HeaderText = "Lớp",
				DataPropertyName = "MaLop",
				Width = 80,
				ReadOnly = true
			};

			// Column: Môn học
			var colMon = new DataGridViewTextBoxColumn
			{
				Name = "MaMonHoc",
				HeaderText = "Môn học",
				DataPropertyName = "MaMonHoc",
				Width = 100,
				ReadOnly = true
			};

			// Column: Giáo viên (editable with ComboBox)
			var colGV = new DataGridViewTextBoxColumn
			{
				Name = "MaGiaoVien",
				HeaderText = "Giáo viên",
				DataPropertyName = "MaGiaoVien",
				Width = 120,
				ReadOnly = false
			};

			// Column: Số tiết/tuần
			var colSoTiet = new DataGridViewTextBoxColumn
			{
				Name = "SoTietTuan",
				HeaderText = "Tiết/tuần",
				DataPropertyName = "SoTietTuan",
				Width = 80,
				ReadOnly = true
			};

			// Column: Score
			var colScore = new DataGridViewTextBoxColumn
			{
				Name = "Score",
				HeaderText = "Điểm",
				DataPropertyName = "Score",
				Width = 60,
				ReadOnly = true
			};

			// Column: Note
			var colNote = new DataGridViewTextBoxColumn
			{
				Name = "Note",
				HeaderText = "Ghi chú",
				DataPropertyName = "Note",
				AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
				ReadOnly = true
			};

			grid.Columns.AddRange(new DataGridViewColumn[] { colLop, colMon, colGV, colSoTiet, colScore, colNote });

			// Style header
			grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
			{
				BackColor = Color.FromArgb(240, 240, 240),
				ForeColor = Color.FromArgb(30, 41, 59),
				Font = new Font("Segoe UI", 10F, FontStyle.Bold),
				Alignment = DataGridViewContentAlignment.MiddleCenter,
				SelectionBackColor = Color.FromArgb(240, 240, 240)
			};

			// Style cells
			grid.DefaultCellStyle = new DataGridViewCellStyle
			{
				BackColor = Color.White,
				ForeColor = Color.FromArgb(30, 41, 59),
				Font = new Font("Segoe UI", 9.5F),
				SelectionBackColor = Color.FromArgb(219, 234, 254),
				SelectionForeColor = Color.FromArgb(30, 41, 59),
				Padding = new Padding(5)
			};
		}

		private void SetActionButtonsEnabled(bool enabled)
		{
			btnValidate.Enabled = enabled;
			btnSaveTemp.Enabled = enabled;
			btnAccept.Enabled = enabled;
		}

		private void BtnGenerate_Click(object sender, EventArgs e)
		{
			try
			{
				lblStatus.Text = "Đang tạo đề xuất tự động...";
				progressBar.Visible = true;
				progressBar.Value = 30;
				Application.DoEvents();

				var policy = new AssignmentPolicy
				{
					MaxLoadPerTeacherPerWeek = 30,
					AllowNonPrimarySpecialty = false
				};

				var res = autoService.GenerateAutoAssignments(1, policy);
				current = res.Candidates;

				progressBar.Value = 80;

				// Enrich data with names
				EnrichCandidatesWithNames(current);

				grid.DataSource = null;
				grid.DataSource = current;

				progressBar.Value = 100;
				progressBar.Visible = false;

				if (res.Report.HardViolations > 0)
				{
					lblStatus.Text = $"⚠ Đề xuất có {res.Report.HardViolations} cảnh báo. Xem chi tiết bên dưới.";
					lblStatus.ForeColor = Color.FromArgb(234, 88, 12);
					MessageBox.Show(string.Join("\n", res.Report.Messages), "Cảnh báo phân công", 
						MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					lblStatus.Text = $"✓ Đã tạo {current.Count} đề xuất phân công thành công!";
					lblStatus.ForeColor = Color.FromArgb(22, 163, 74);
				}

				SetActionButtonsEnabled(true);
			}
			catch (Exception ex)
			{
				progressBar.Visible = false;
				lblStatus.Text = "✗ Lỗi khi tạo đề xuất";
				lblStatus.ForeColor = Color.FromArgb(220, 38, 38);
				MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void EnrichCandidatesWithNames(List<PhanCongCandidate> candidates)
		{
			var lopDao = new LopDAO();
			var monDao = new MonHocDAO();
			var gvDao = new GiaoVienDAO();

			foreach (var c in candidates)
			{
				// Get names instead of IDs for display (optional: use extra columns)
				var lop = lopDao.LayLopTheoId(c.MaLop);
				var mon = monDao.LayDSMonHocTheoId(c.MaMonHoc);
				var gv = gvDao.LayGiaoVienTheoMa(c.MaGiaoVien);

				// Add to Note column for better readability
				if (string.IsNullOrEmpty(c.Note))
				{
					c.Note = $"{lop?.tenLop ?? "?"} - {mon?.tenMon ?? "?"} - {gv?.HoTen ?? "?"}";
				}
			}
		}

		private void BtnValidate_Click(object sender, EventArgs e)
		{
			if (current == null || current.Count == 0)
			{
				MessageBox.Show("Chưa có dữ liệu để kiểm tra.", "Thông báo");
				return;
			}

			var report = autoService.ValidateAutoAssignments(current);
			if (report.HardViolations == 0)
			{
				lblStatus.Text = $"✓ Kiểm tra OK! {current.Count} phân công hợp lệ.";
				lblStatus.ForeColor = Color.FromArgb(22, 163, 74);
				MessageBox.Show("Tất cả phân công đều hợp lệ!", "Kiểm tra thành công", 
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				lblStatus.Text = $"⚠ Có {report.HardViolations} lỗi vi phạm";
				lblStatus.ForeColor = Color.FromArgb(220, 38, 38);
				MessageBox.Show(string.Join("\n", report.Messages), "Lỗi vi phạm", 
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void BtnSaveTemp_Click(object sender, EventArgs e)
		{
			if (current == null || current.Count == 0)
			{
				MessageBox.Show("Chưa có dữ liệu để lưu.", "Thông báo");
				return;
			}

			try
			{
				persistService.PersistTemporary(current);
				lblStatus.Text = "💾 Đã lưu tạm đề xuất vào bảng PhanCong_Temp.";
				lblStatus.ForeColor = Color.FromArgb(30, 136, 229);
				MessageBox.Show("Đã lưu tạm đề xuất.\n\nBạn có thể xem lại và chỉnh sửa trước khi chấp nhận chính thức.", 
					"Lưu thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi lưu tạm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnAccept_Click(object sender, EventArgs e)
		{
			var confirm = MessageBox.Show(
				"Bạn có chắc chắn muốn chấp nhận đề xuất này?\n\n" +
				"Dữ liệu sẽ được lưu vào bảng PhanCongGiangDay chính thức.",
				"Xác nhận chấp nhận",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (confirm != DialogResult.Yes) return;

			try
			{
				persistService.AcceptToOfficial(1);
				lblStatus.Text = "✓ Đã chấp nhận và lưu vào PhanCongGiangDay!";
				lblStatus.ForeColor = Color.FromArgb(22, 163, 74);
				MessageBox.Show("Đã chấp nhận đề xuất vào bảng chính thức.\n\nVui lòng tải lại danh sách để xem cập nhật.", 
					"Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
				
				// Clear current
				current = null;
				grid.DataSource = null;
				SetActionButtonsEnabled(false);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi chấp nhận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnRollback_Click(object sender, EventArgs e)
		{
			var confirm = MessageBox.Show(
				"Bạn có chắc chắn muốn xóa bảng tạm?\n\nThao tác này không thể hoàn tác.",
				"Xác nhận xóa",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning);

			if (confirm != DialogResult.Yes) return;

			try
			{
				persistService.RollbackTemp();
				lblStatus.Text = "🗑 Đã xóa bảng tạm PhanCong_Temp.";
				lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
				MessageBox.Show("Đã xóa bảng tạm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi khi xóa bảng tạm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
