using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Student_Management_System_CSharp_SGU2025.BUS.Services;
using Student_Management_System_CSharp_SGU2025.BUS.Scheduling;
using Student_Management_System_CSharp_SGU2025.BUS.Config;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DTO;
using Guna.UI2.WinForms;
using AssignmentSlot = Student_Management_System_CSharp_SGU2025.DTO.AssignmentSlotDTO;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    /// <summary>
    /// Form Preview cho Auto TKB - Cấu hình & Generate
    /// </summary>
    public partial class FrmAutoTKBPreview : Form
    {
        private readonly int _semesterId;
        private readonly int _weekNo = 1; // Default to week 1
        private ScheduleGenerationResult currentResult;
        private readonly SchedulingService _schedulingService;
        private readonly ThoiKhoaBieuBUS _tkbBUS;
        private TimetableConfigRoot _config;
        private CancellationTokenSource _cts = new CancellationTokenSource();

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
            _semesterId = semesterId;
            _schedulingService = new SchedulingService();
            _tkbBUS = new ThoiKhoaBieuBUS();
            InitializeComponent();
            this.Load += FrmAutoTKBPreview_Load;
        }

        private void FrmAutoTKBPreview_Load(object sender, EventArgs e)
        {
            // Check admin permission
            if (!PermissionHelper.HasPermission(PermissionHelper.QLTKB, PermissionHelper.CREATE))
            {
                MessageBox.Show("Bạn không có quyền tạo thời khóa biểu tự động!", 
                    "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            // Load config from JSON
            try
            {
                _config = TimetableConfigService.Load();
                
                // Pre-fill numeric controls từ cấu hình thuật toán
                if (_config.ThamSoThuatToan != null)
                {
                    numIterations.Value = Math.Max(numIterations.Minimum, Math.Min(numIterations.Maximum, _config.ThamSoThuatToan.SoVongLapToiDa));
                    numTimeBudget.Value = Math.Max(numTimeBudget.Minimum, Math.Min(numTimeBudget.Maximum, _config.ThamSoThuatToan.ThoiGianChayToiDaGiay));
                    numTabuTenure.Value = Math.Max(numTabuTenure.Minimum, Math.Min(numTabuTenure.Maximum, _config.ThamSoThuatToan.DoDaiTabu));
                }

                lblStatus.Text = "Đã tải cấu hình từ timetable_config.json. Sẵn sàng tạo TKB.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải cấu hình: {ex.Message}\n\nSẽ sử dụng giá trị mặc định.", 
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _config = TimetableConfigService.Load(); // Try again, will use defaults
            }
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
                Text = "<b>Auto Tạo Thời khóa biểu - Config-driven (Greedy + Tabu Search)</b>",
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

        private async void GenerateTKB()
        {
            try
            {
                // Cancel any previous generation
                _cts?.Cancel();
                _cts?.Dispose();
                _cts = new CancellationTokenSource();

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
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Bắt đầu tạo TKB cho học kỳ {_semesterId}, Tuần {_weekNo}...\r\n");
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Sử dụng SchedulingService (Greedy + Tabu Search)\r\n");
                Application.DoEvents();

                Cursor.Current = Cursors.WaitCursor;

                // Update config with UI values
                if (_config == null)
                {
                    _config = TimetableConfigService.Load();
                }
                
                // Ghi đè tham số thuật toán từ UI vào cấu hình
                _config.ThamSoThuatToan.SoVongLapToiDa = (int)numIterations.Value;
                _config.ThamSoThuatToan.ThoiGianChayToiDaGiay = (int)numTimeBudget.Value;
                _config.ThamSoThuatToan.DoDaiTabu = (int)numTabuTenure.Value;

                // Create progress reporter for UI updates
                var progress = new Progress<string>(message =>
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
                    Application.DoEvents();
                });

                progressBar.Value = 10;
                Application.DoEvents();

                // ✅ Xác định số tuần cần tạo TKB (18 tuần HK1, 17 tuần HK2, bỏ tuần cuối)
                var hocKyBUS = new HocKyBUS();
                var hocKy = hocKyBUS.LayHocKyTheoMa(_semesterId);
                int totalWeeks = 18; // Default
                
                if (hocKy != null)
                {
                    string tenHocKy = hocKy.TenHocKy?.ToLower() ?? "";
                    bool isSemester1 = (tenHocKy.Contains("i") && !tenHocKy.Contains("ii")) ||
                                       (tenHocKy.Contains("1") && !tenHocKy.Contains("2"));
                    totalWeeks = isSemester1 ? 18 : 17; // HK1: 18 tuần, HK2: 17 tuần
                }
                
                // Tuần cuối cùng (tuần thi) không xếp TKB
                int weeksToGenerate = totalWeeks - 1;
                
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Tạo TKB cố định cho {weeksToGenerate} tuần (bỏ tuần cuối - tuần thi)...\r\n");
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Chạy Greedy Initialization + Tabu Search Optimization...\r\n");
                
                // ✅ Tạo TKB 1 lần (cho tuần 1), sau đó áp dụng cho tất cả các tuần
                progressBar.Value = 10;
                Application.DoEvents();
                
                // Tạo TKB cho tuần 1
                currentResult = await _schedulingService.GenerateToTempWithConfigAsync(
                    _semesterId,
                    1, // Chỉ tạo cho tuần 1
                    _config,
                    _cts.Token,
                    progress);
                
                if (currentResult.Success)
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ TKB đã được tạo thành công cho tuần 1 ({currentResult.TotalSlots} tiết)\r\n");
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 📋 Đang áp dụng TKB cố định cho tất cả các tuần (1-{weeksToGenerate})...\r\n");
                    
                    // Lấy TKB từ tuần 1
                    var tkbBUS = new ThoiKhoaBieuBUS();
                    var slots = tkbBUS.GetWeek(_semesterId, 1);
                    
                    if (slots != null && slots.Count > 0)
                    {
                        // Áp dụng TKB này cho tất cả các tuần (2 đến weeksToGenerate)
                        int appliedCount = 0;
                        for (int week = 2; week <= weeksToGenerate; week++)
                        {
                            if (_cts.Token.IsCancellationRequested)
                            {
                                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⚠️ Đã hủy áp dụng TKB.\r\n");
                                break;
                            }
                            
                            progressBar.Value = 50 + (int)((week - 2) * 40.0 / (weeksToGenerate - 1));
                            Application.DoEvents();
                            
                            // ✅ Clear TKB_Temp cho tuần này trước khi insert (tránh duplicate)
                            tkbBUS.ClearTempForSemester(_semesterId, week);
                            
                            // ✅ Remove duplicate slots (cùng MaLop, Thu, Tiet) trước khi insert
                            var uniqueSlots = new List<AssignmentSlot>();
                            var seenSlots = new HashSet<string>(); // Key: "{MaLop}-{Thu}-{Tiet}"
                            
                            foreach (var slot in slots)
                            {
                                string key = $"{slot.MaLop}-{slot.Thu}-{slot.Tiet}";
                                if (!seenSlots.Contains(key))
                                {
                                    seenSlots.Add(key);
                                    uniqueSlots.Add(slot);
                                }
                            }
                            
                            // Lưu TKB cho tuần này (copy từ tuần 1)
                            var solution = new ScheduleSolution 
                            { 
                                Slots = new BindingList<AssignmentSlot>(uniqueSlots) 
                            };
                            tkbBUS.InsertTemp(_semesterId, week, solution);
                            appliedCount++;
                        }
                        
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ Đã áp dụng TKB cố định cho {appliedCount + 1} tuần (tuần 1-{weeksToGenerate})\r\n");
                        progressBar.Value = 90;
                    }
                    else
                    {
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⚠️ Không thể lấy TKB từ tuần 1 để áp dụng cho các tuần khác.\r\n");
                    }
                }
                else
                {
                    progressBar.Value = 90;
                }

                if (!currentResult.Success)
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ❌ Thất bại: {currentResult.Message}\r\n");
                    
                    lblStatus.Text = $"❌ Thất bại: {currentResult.Message}";
                    lblStatus.ForeColor = Color.FromArgb(220, 38, 38);
                    
                    // Only show error dialog if hard constraints are violated
                    if (currentResult.HardConstraintViolated)
                    {
                        MessageBox.Show(
                            $"Không thể tạo TKB:\n\n{currentResult.Message}",
                            "Thất bại",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Too many missing periods - show warning but allow inspection
                        MessageBox.Show(
                            $"TKB được tạo nhưng còn thiếu quá nhiều tiết:\n\n{currentResult.Message}\n\nBạn có thể xem trước và quyết định có chấp nhận không.",
                            "Cảnh báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Success (with or without warnings)
                    if (currentResult.HasMissingPeriods)
                    {
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⚠️ Hoàn thành với cảnh báo!\r\n");
                    }
                    else
                    {
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ Hoàn thành!\r\n");
                    }
                    
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 📊 Tổng tiết: {currentResult.TotalSlots}\r\n");
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 📈 Điểm ban đầu: {currentResult.InitialCost}\r\n");
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 📈 Điểm cuối: {currentResult.FinalCost}\r\n");
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⚠ Vi phạm ràng buộc cứng: {currentResult.HardViolations}\r\n");

                    // Report period coverage
                    if (currentResult.PeriodCoverage != null && currentResult.PeriodCoverage.Count > 0)
                    {
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 📋 Báo cáo số tiết đã xếp:\r\n");
                        var incompleteCount = 0;
                        var incompleteDetails = new List<string>();
                        
                        foreach (var kvp in currentResult.PeriodCoverage.OrderBy(x => x.Key))
                        {
                            var (required, placed) = kvp.Value;
                            if (placed < required)
                            {
                                incompleteCount++;
                                incompleteDetails.Add($"{kvp.Key}: Cần {required} tiết, đã xếp {placed} tiết (thiếu {required - placed} tiết)");
                            }
                        }
                        
                        // Show incomplete assignments first
                        if (incompleteDetails.Count > 0)
                        {
                            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⚠️ Có {incompleteCount} môn chưa đủ số tiết:\r\n");
                            foreach (var detail in incompleteDetails)
                            {
                                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}]   - {detail}\r\n");
                            }
                        }
                        
                        // Show summary
                        int totalRequired = currentResult.PeriodCoverage.Sum(kvp => kvp.Value.Required);
                        int totalPlaced = currentResult.PeriodCoverage.Sum(kvp => kvp.Value.Placed);
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 📊 Tổng kết: {totalPlaced}/{totalRequired} tiết đã xếp ({incompleteCount} môn chưa đủ)\r\n");
                        
                        if (incompleteCount == 0)
                        {
                            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ Tất cả các môn đã được xếp đủ số tiết!\r\n");
                        }
                        else if (currentResult.Success)
                        {
                            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⚠️ Còn thiếu {currentResult.MissingPeriods} tiết của {incompleteCount} môn. Bạn có thể chấp nhận và chỉnh sửa thủ công.\r\n");
                        }
                    }
                    
                    // Show incomplete assignments from result
                    if (currentResult.IncompleteAssignments != null && currentResult.IncompleteAssignments.Count > 0)
                    {
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 📝 Chi tiết các môn chưa đủ tiết:\r\n");
                        foreach (var msg in currentResult.IncompleteAssignments)
                        {
                            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}]   - {msg}\r\n");
                        }
                    }

                    // Check if temp schedule exists
                    if (_tkbBUS.HasTempScheduleForSemester(_semesterId))
                    {
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ Đã lưu vào TKB_Temp. Có thể xem trước và chấp nhận.\r\n");
                    }

                    // Update status label with appropriate color
                    if (currentResult.HasMissingPeriods)
                    {
                        lblStatus.Text = $"⚠️ {currentResult.Message}";
                        lblStatus.ForeColor = Color.FromArgb(234, 88, 12); // Orange for warning
                    }
                    else
                    {
                        lblStatus.Text = $"✅ {currentResult.Message}";
                        lblStatus.ForeColor = Color.FromArgb(22, 163, 74); // Green for success
                    }
                }

                progressBar.Value = 100;

                // Enable buttons based on success status
                btnRegenerate.Enabled = true;
                btnValidate.Enabled = currentResult.Success && !currentResult.HardConstraintViolated;
                // Enable Accept button for success (even with warnings) and no hard violations
                btnSave.Enabled = currentResult.Success && !currentResult.HardConstraintViolated;
            }
            catch (OperationCanceledException)
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⏸ Đã hủy tạo TKB.\r\n");
                lblStatus.Text = "⏸ Đã hủy tạo TKB.";
                lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ❌ LỖI: {ex.Message}\r\n");
                if (ex.StackTrace != null)
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Stack trace: {ex.StackTrace}\r\n");
                }
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
            if (currentResult == null || !currentResult.Success)
            {
                MessageBox.Show("Chưa có TKB để kiểm tra. Vui lòng Generate trước.", "Thông báo");
                return;
            }

            try
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Đang kiểm tra TKB...\r\n");

                // Get temp slots (already in AssignmentSlot format)
                var tempSlots = _tkbBUS.GetWeek(_semesterId, _weekNo);
                if (tempSlots == null || tempSlots.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy TKB tạm để kiểm tra.", "Thông báo");
                    return;
                }

                // Build solution from temp slots and validate
                var solution = new ScheduleSolution();
                solution.Slots = new BindingList<AssignmentSlot>();
                foreach (var slot in tempSlots)
                {
                    solution.Slots.Add(slot);
                }

                // Validate hard constraints
                bool isValid = _schedulingService.ValidateHardConstraints(solution);
                
                // Analyze conflicts for detailed report
                var conflicts = _schedulingService.AnalyzeConflicts(solution);
                
                string violationMsg = conflicts.HardViolations > 0 
                    ? $"Phát hiện {conflicts.HardViolations} vi phạm ràng buộc cứng" 
                    : "Không có vi phạm ràng buộc cứng";

                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ {violationMsg}\r\n");
                
                if (conflicts.Messages != null && conflicts.Messages.Count > 0)
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] Chi tiết vi phạm:\r\n");
                    foreach (var msg in conflicts.Messages.Take(10))
                    {
                        txtLog.AppendText($"   - {msg}\r\n");
                    }
                }
                
                MessageBox.Show(
                    $"✅ Kết quả kiểm tra:\n\n" +
                    $"📊 Tổng tiết: {currentResult.TotalSlots}\n" +
                    $"📈 Điểm ban đầu: {currentResult.InitialCost}\n" +
                    $"📈 Điểm cuối: {currentResult.FinalCost}\n" +
                    $"⚠ Vi phạm ràng buộc cứng: {conflicts.HardViolations}\n\n" +
                    $"{violationMsg}",
                    "Kiểm tra TKB",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (conflicts.HardViolations == 0)
                {
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (currentResult == null || !currentResult.Success)
            {
                MessageBox.Show("Chưa có TKB để lưu.", "Thông báo");
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn chấp nhận thời khóa biểu này và ghi vào bảng chính không?\n\n" +
                "⚠ Sau khi chấp nhận, TKB sẽ được ghi vào bảng ThoiKhoaBieu và không thể hoàn tác dễ dàng.",
                "Xác nhận chấp nhận TKB",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // Accept temp timetable to official
                _schedulingService.AcceptTempForSemester(_semesterId, _weekNo);
                
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ✅ Đã chấp nhận thời khóa biểu. Dữ liệu đã được ghi vào bảng ThoiKhoaBieu.\r\n");
                lblStatus.Text = "✅ Đã chấp nhận thời khóa biểu. Dữ liệu đã được ghi vào bảng ThoiKhoaBieu.";
                lblStatus.ForeColor = Color.FromArgb(22, 163, 74);

                MessageBox.Show(
                    $"✅ Đã chấp nhận thời khóa biểu thành công!\n\n" +
                    $"📊 Tổng tiết: {currentResult.TotalSlots}\n" +
                    $"📈 Điểm cuối: {currentResult.FinalCost}\n\n" +
                    $"Bạn có thể quay lại màn hình chính và chọn lớp để xem chi tiết.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể chấp nhận TKB:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Cancel any running generation
            _cts?.Cancel();

            var confirm = MessageBox.Show(
                "Bạn có muốn hủy và xóa TKB tạm chưa lưu không?\n\n" +
                "Chọn 'Có' để xóa TKB tạm và đóng.\n" +
                "Chọn 'Không' để giữ TKB tạm và đóng.\n" +
                "Chọn 'Hủy' để tiếp tục.",
                "Xác nhận hủy",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // Rollback temp timetable
                try
                {
                    _schedulingService.RollbackTempForSemester(_semesterId, _weekNo);
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] ⏸ Đã xóa TKB tạm.\r\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa TKB tạm: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (confirm == DialogResult.No)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void BtnRegenerate_Click(object sender, EventArgs e)
        {
            // Rollback current temp before regenerating
            try
            {
                _schedulingService.RollbackTempForSemester(_semesterId, _weekNo);
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 🔄 Đã xóa TKB tạm cũ. Bắt đầu tạo lại...\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa TKB tạm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            GenerateTKB();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            base.OnFormClosing(e);
        }

        // Empty handlers for compatibility (kept for Designer compatibility)
        private void Guna2HtmlLabel25_Click(object sender, EventArgs e) { }
        private void Guna2HtmlLabel6_Click(object sender, EventArgs e) { }
        private void Guna2Panel1_Paint(object sender, PaintEventArgs e) { }
    }
}

