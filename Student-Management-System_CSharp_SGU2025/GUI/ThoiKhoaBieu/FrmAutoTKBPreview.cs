using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.Scheduling;
using Student_Management_System_CSharp_SGU2025.BUS;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThoiKhoaBieu
{
    /// <summary>
    /// Form Preview cho Auto TKB - Cấu hình & Generate
    /// </summary>
    public partial class FrmAutoTKBPreview : Form
    {
        private int semesterId;
        private ScheduleSolution currentSolution;
        private SchedulingService schedulingService;

        // UI Controls
        private Guna2Panel panelConfig;
        private Guna2Panel panelPreview;
        private Guna2Panel panelButtons;
        
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblStatus;
        private Guna2HtmlLabel lblIterations;
        private Guna2HtmlLabel lblTimeBudget;
        private Guna2HtmlLabel lblTabuTenure;
        
        private Guna2NumericUpDown numIterations;
        private Guna2NumericUpDown numTimeBudget;
        private Guna2NumericUpDown numTabuTenure;
        
        private Guna2ProgressBar progressBar;
        private Guna2TextBox txtLog;
        
        private Guna2Button btnGenerate;
        private Guna2Button btnRegenerate;
        private Guna2Button btnValidate;
        private Guna2Button btnSave;
        private Guna2Button btnCancel;

        public FrmAutoTKBPreview(int semesterId)
        {
            this.semesterId = semesterId;
            this.schedulingService = new SchedulingService();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Auto Tạo Thời khóa biểu - Preview & Cấu hình";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Title
            lblTitle = new Guna2HtmlLabel
            {
                Text = "<b>🤖 Auto Tạo Thời khóa biểu - Tabu Search</b>",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(30, 20),
                AutoSize = true
            };

            // Config Panel
            panelConfig = CreateConfigPanel();
            
            // Status & Progress
            lblStatus = new Guna2HtmlLabel
            {
                Text = "Sẵn sàng tạo TKB. Nhấn 'Generate' để bắt đầu.",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(30, 220),
                Size = new Size(900, 25),
                AutoSize = false
            };

            progressBar = new Guna2ProgressBar
            {
                Location = new Point(30, 250),
                Size = new Size(920, 15),
                Visible = false,
                ProgressColor = Color.FromArgb(34, 197, 94),
                ProgressColor2 = Color.FromArgb(22, 163, 74),
                TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
            };

            // Log Panel
            txtLog = new Guna2TextBox
            {
                Location = new Point(30, 280),
                Size = new Size(920, 280),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 9F),
                BorderRadius = 8
            };

            // Buttons Panel
            panelButtons = CreateButtonsPanel();

            // Add all controls
            this.Controls.AddRange(new Control[] 
            { 
                lblTitle, panelConfig, lblStatus, progressBar, txtLog, panelButtons 
            });
        }

        private Guna2Panel CreateConfigPanel()
        {
            var panel = new Guna2Panel
            {
                Location = new Point(30, 60),
                Size = new Size(920, 140),
                FillColor = Color.White,
                BorderRadius = 10
            };
            panel.ShadowDecoration.Enabled = true;

            // Iterations
            lblIterations = new Guna2HtmlLabel
            {
                Text = "Số vòng lặp (Iterations):",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };

            numIterations = new Guna2NumericUpDown
            {
                Location = new Point(220, 15),
                Size = new Size(150, 30),
                Minimum = 1000,
                Maximum = 10000,
                Value = 5000,
                BorderRadius = 6
            };

            // Time Budget
            lblTimeBudget = new Guna2HtmlLabel
            {
                Text = "Thời gian tối đa (giây):",
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };

            numTimeBudget = new Guna2NumericUpDown
            {
                Location = new Point(220, 55),
                Size = new Size(150, 30),
                Minimum = 10,
                Maximum = 300,
                Value = 90,
                BorderRadius = 6
            };

            // Tabu Tenure
            lblTabuTenure = new Guna2HtmlLabel
            {
                Text = "Độ dài Tabu List:",
                Location = new Point(20, 100),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };

            numTabuTenure = new Guna2NumericUpDown
            {
                Location = new Point(220, 95),
                Size = new Size(150, 30),
                Minimum = 5,
                Maximum = 20,
                Value = 9,
                BorderRadius = 6
            };

            // Info labels
            var lblInfoIter = new Guna2HtmlLabel
            {
                Text = "💡 Càng cao càng tốt (nhưng lâu hơn)",
                Location = new Point(400, 20),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8.5F)
            };

            var lblInfoTime = new Guna2HtmlLabel
            {
                Text = "⏱ Timeout để tránh chạy quá lâu",
                Location = new Point(400, 60),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8.5F)
            };

            var lblInfoTabu = new Guna2HtmlLabel
            {
                Text = "📊 Khuyến nghị: 7-12",
                Location = new Point(400, 100),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8.5F)
            };

            panel.Controls.AddRange(new Control[] 
            { 
                lblIterations, numIterations, lblInfoIter,
                lblTimeBudget, numTimeBudget, lblInfoTime,
                lblTabuTenure, numTabuTenure, lblInfoTabu
            });

            return panel;
        }

        private Guna2Panel CreateButtonsPanel()
        {
            var panel = new Guna2Panel
            {
                Location = new Point(30, 580),
                Size = new Size(920, 60),
                FillColor = Color.White,
                BorderRadius = 8
            };
            panel.ShadowDecoration.Enabled = true;

            btnGenerate = CreateButton("🚀 Generate", new Point(20, 12), 140, Color.FromArgb(75, 85, 99));
            btnRegenerate = CreateButton("🔄 Regenerate", new Point(170, 12), 140, Color.FromArgb(59, 130, 246));
            btnValidate = CreateButton("✓ Kiểm tra", new Point(320, 12), 120, Color.FromArgb(234, 88, 12));
            btnSave = CreateButton("💾 Lưu & Đóng", new Point(450, 12), 140, Color.FromArgb(22, 163, 74));
            btnCancel = CreateButton("✗ Hủy", new Point(600, 12), 100, Color.FromArgb(220, 38, 38));

            btnGenerate.Click += BtnGenerate_Click;
            btnRegenerate.Click += BtnRegenerate_Click;
            btnValidate.Click += BtnValidate_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            btnRegenerate.Enabled = false;
            btnValidate.Enabled = false;
            btnSave.Enabled = false;

            panel.Controls.AddRange(new Control[] 
            { 
                btnGenerate, btnRegenerate, btnValidate, btnSave, btnCancel 
            });

            return panel;
        }

        private Guna2Button CreateButton(string text, Point location, int width, Color fillColor)
        {
            return new Guna2Button
            {
                Text = text,
                Location = location,
                Size = new Size(width, 36),
                BorderRadius = 6,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                FillColor = fillColor,
                Cursor = Cursors.Hand
            };
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            GenerateTKB();
        }

        private void BtnRegenerate_Click(object sender, EventArgs e)
        {
            GenerateTKB();
        }

        private void GenerateTKB()
        {
            try
            {
                // Disable buttons
                btnGenerate.Enabled = false;
                btnRegenerate.Enabled = false;
                btnValidate.Enabled = false;
                btnSave.Enabled = false;

                // Show progress
                lblStatus.Text = "⏳ Đang tạo TKB... Vui lòng đợi.";
                lblStatus.ForeColor = Color.FromArgb(59, 130, 246);
                progressBar.Visible = true;
                progressBar.Value = 0;
                txtLog.Clear();
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Bắt đầu tạo TKB cho học kỳ {semesterId}...\r\n");
                Application.DoEvents();

                Cursor.Current = Cursors.WaitCursor;

                // Build request
                var req = schedulingService.BuildRequestFromDatabase(semesterId, 1);
                req.IterMax = (int)numIterations.Value;
                req.TimeBudgetSec = (int)numTimeBudget.Value;
                req.TabuTenure = (int)numTabuTenure.Value;

                if (req.Assignments == null || req.Assignments.Count == 0)
                {
                    MessageBox.Show(
                        "Chưa có dữ liệu phân công giảng dạy trong học kỳ này.\n\n" +
                        "Vui lòng thực hiện 'Auto Phân công' trước!",
                        "Thiếu dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Tìm thấy {req.Assignments.Count} phân công giảng dạy.\r\n");
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Cấu hình: MaxIter={req.IterMax}, TimeBudget={req.TimeBudgetSec}s, TabuTenure={req.TabuTenure}\r\n");
                progressBar.Value = 10;
                Application.DoEvents();

                // Generate using Tabu Search
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Chạy Tabu Search...\r\n");
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(req.TimeBudgetSec + 10)))
                {
                    currentSolution = schedulingService.GenerateSchedule(req, cts.Token);
                }

                progressBar.Value = 80;
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Hoàn thành! Tổng tiết: {currentSolution.Slots.Count}, Cost: {currentSolution.Cost}\r\n");

                // Validate
                var isValid = schedulingService.ValidateHardConstraints(currentSolution);
                if (!isValid)
                {
                    var conflicts = schedulingService.AnalyzeConflicts(currentSolution);
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⚠ Phát hiện {conflicts.HardViolations} vi phạm cứng:\r\n");
                    foreach (var msg in conflicts.Messages)
                    {
                        txtLog.AppendText($"   - {msg}\r\n");
                    }
                    
                    lblStatus.Text = $"⚠ TKB có {conflicts.HardViolations} vi phạm. Xem log bên dưới.";
                    lblStatus.ForeColor = Color.FromArgb(234, 88, 12);
                }
                else
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ TKB hợp lệ (Hard = 0)!\r\n");
                    lblStatus.Text = $"✅ TKB hợp lệ! Tổng {currentSolution.Slots.Count} tiết, Cost = {currentSolution.Cost}";
                    lblStatus.ForeColor = Color.FromArgb(22, 163, 74);
                }

                progressBar.Value = 100;

                // Enable buttons
                btnRegenerate.Enabled = true;
                btnValidate.Enabled = true;
                btnSave.Enabled = isValid; // Chỉ cho Save nếu hợp lệ
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ❌ LỖI: {ex.Message}\r\n");
                lblStatus.Text = "❌ Lỗi khi tạo TKB. Xem log bên dưới.";
                lblStatus.ForeColor = Color.FromArgb(220, 38, 38);
                MessageBox.Show($"Lỗi:\n\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnGenerate.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private void BtnValidate_Click(object sender, EventArgs e)
        {
            if (currentSolution == null || currentSolution.Slots == null || currentSolution.Slots.Count == 0)
            {
                MessageBox.Show("Chưa có TKB để kiểm tra. Vui lòng Generate trước.", "Thông báo");
                return;
            }

            try
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Đang kiểm tra TKB...\r\n");

                var isValid = schedulingService.ValidateHardConstraints(currentSolution);
                var conflicts = schedulingService.AnalyzeConflicts(currentSolution);

                if (isValid)
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ Kiểm tra PASS! TKB hợp lệ.\r\n");
                    MessageBox.Show(
                        $"✅ TKB hợp lệ!\n\n" +
                        $"📊 Tổng tiết: {currentSolution.Slots.Count}\n" +
                        $"💯 Điểm soft: {currentSolution.Cost}\n" +
                        $"🎯 Hard violations: 0",
                        "Kiểm tra thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    btnSave.Enabled = true;
                }
                else
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⚠ Kiểm tra FAIL! {conflicts.HardViolations} vi phạm:\r\n");
                    foreach (var msg in conflicts.Messages)
                    {
                        txtLog.AppendText($"   - {msg}\r\n");
                    }
                    
                    MessageBox.Show(
                        $"⚠ TKB có {conflicts.HardViolations} vi phạm cứng!\n\n" +
                        string.Join("\n", conflicts.Messages.Take(10)),
                        "Kiểm tra thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (currentSolution == null || currentSolution.Slots == null || currentSolution.Slots.Count == 0)
            {
                MessageBox.Show("Chưa có TKB để lưu.", "Thông báo");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn lưu TKB này?\n\n" +
                $"📊 Tổng tiết: {currentSolution.Slots.Count}\n" +
                $"💯 Điểm: {currentSolution.Cost}\n\n" +
                $"TKB sẽ được lưu vào bảng tạm (TKB_Temp).\n" +
                $"Bạn có thể xem lại và chốt sau.",
                "Xác nhận lưu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Đang lưu TKB vào bảng tạm...\r\n");
                schedulingService.PersistToTemp(semesterId, 1, currentSolution);
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ Đã lưu thành công!\r\n");

                MessageBox.Show(
                    "✅ Đã lưu TKB vào bảng tạm!\n\n" +
                    "Bạn có thể quay lại màn hình chính và chọn lớp để xem chi tiết.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ❌ Lỗi lưu: {ex.Message}\r\n");
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc muốn hủy?\n\nTKB chưa lưu sẽ bị mất.",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        // Empty handlers for compatibility
        private void guna2HtmlLabel25_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel6_Click(object sender, EventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
    }
}

