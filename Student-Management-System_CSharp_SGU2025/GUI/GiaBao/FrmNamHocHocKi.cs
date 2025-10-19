using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WinFormsApp2
{
    public partial class FrmNamHocHocKi : Form
    {
        private Guna2Panel pnlSidebar = null!;
        private Guna2Panel pnlHeader = null!;
        private Guna2Panel pnlContent = null!;
        private Guna2Button btnNamHoc = null!;
        private Guna2Button btnHocKy = null!;
        private Guna2DataGridView dgvData = null!;
        private Guna2Button btnAction = null!;

        // Các Font và Color dùng trong logic và formatting
        private readonly Font defaultFont = new Font("Segoe UI", 10, FontStyle.Regular);
        private readonly Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Color columnHeaderSelectionColor = Color.FromArgb(190, 220, 255);
        private readonly Color CardTextColor = Color.White;

        public FrmNamHocHocKi()
        {
            InitializeComponent();
            // Khởi tạo các thuộc tính cơ bản của Form
            this.Width = FormWidth;
            this.Height = FormHeight;
            this.Text = "Năm học & Học kỳ";
            this.BackColor = Color.FromArgb(249, 250, 252);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Font = defaultFont;
        }

        private void FrmNamHocHocKi_Load(object sender, EventArgs e)
        {
            InitializeCustomControls();
            ShowNamHocTab();
            dgvData.ClearSelection();
        }

        // Phương thức này chứa logic khởi tạo các Controls (Sidebar, Header, DataGridView,...)
        // Hiện tại, toàn bộ controls được tạo bằng code trong phương thức này.
        private void InitializeCustomControls()
        {
            // --- SIDEBAR (192x900) ---
            pnlSidebar = new Guna2Panel
            {
                Location = new Point(0, 0),
                Size = new Size(SidebarWidth, FormHeight),
                FillColor = Color.White,
                Dock = DockStyle.Left,
            };
            this.Controls.Add(pnlSidebar);

            // --- HEADER PANEL (1248x56) ---
            pnlHeader = new Guna2Panel
            {
                Location = new Point(SidebarWidth, 0),
                Size = new Size(ContentAreaWidth, HeaderHeight),
                FillColor = Color.White,
            };
            this.Controls.Add(pnlHeader);

            Label lblTitle = new Label { Text = "Năm học & Học kỳ", Location = new Point(20, 10), Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
            Label lblSubtitle = new Label { Text = "Trang chủ / Năm học & Học kỳ", Location = new Point(20, 32), Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Gray, AutoSize = true };
            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            Guna2TextBox txtSearch = new Guna2TextBox
            {
                PlaceholderText = "Tìm kiếm...",
                Location = new Point(ContentAreaWidth - 280, 14),
                Size = new Size(180, 30),
                BorderRadius = 15,
                FillColor = Color.FromArgb(240, 240, 240),
                BorderThickness = 0,
                Font = defaultFont,
            };
            Guna2Button btnNotification = new Guna2Button
            {
                Text = "🔔",
                Location = new Point(ContentAreaWidth - 90, 10),
                Size = new Size(35, 35),
                FillColor = Color.Transparent,
                BorderRadius = 17,
                Font = new Font("Segoe UI Emoji", 12, FontStyle.Regular),
                ForeColor = Color.Black
            };
            Guna2CircleButton pnlAvatar = new Guna2CircleButton { Location = new Point(ContentAreaWidth - 50, 10), Size = new Size(36, 36), FillColor = Color.FromArgb(0, 123, 255), };
            Label lblAvatarText = new Label { Text = "NV", Location = new Point(0, 0), Size = new Size(36, 36), Font = boldFont, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter, };
            pnlAvatar.Controls.Add(lblAvatarText);
            pnlHeader.Controls.AddRange(new Control[] { txtSearch, btnNotification, pnlAvatar });

            // --- CONTENT PANEL (1248x844) ---
            pnlContent = new Guna2Panel
            {
                Location = new Point(SidebarWidth, HeaderHeight),
                Size = new Size(ContentAreaWidth, ContentBodyHeight),
                FillColor = this.BackColor,
            };
            this.Controls.Add(pnlContent);

            // Tab Buttons và Action Button
            btnNamHoc = new Guna2Button { Text = "Năm học", Location = new Point(20, 20), Size = new Size(100, TabButtonHeight), BorderRadius = 8, Font = boldFont, };
            btnNamHoc.Click += (s, ev) => ShowNamHocTab();
            pnlContent.Controls.Add(btnNamHoc);

            btnHocKy = new Guna2Button { Text = "Học kỳ", Location = new Point(120, 20), Size = new Size(100, TabButtonHeight), BorderThickness = 1, BorderRadius = 8, BorderColor = Color.LightGray, FillColor = Color.White, ForeColor = Color.Black, Font = boldFont, };
            btnHocKy.Click += (s, ev) => ShowHocKyTab();
            pnlContent.Controls.Add(btnHocKy);

            btnAction = new Guna2Button { Location = new Point(ContentAreaWidth - 20 - 150, 20), Size = new Size(150, TabButtonHeight), FillColor = Color.FromArgb(0, 123, 255), ForeColor = Color.White, BorderRadius = 8, Font = boldFont };
            btnAction.Click += BtnAction_Click;
            pnlContent.Controls.Add(btnAction);

            // DataGridView
            dgvData = new Guna2DataGridView
            {
                Location = new Point(20, 300),
                Size = new Size(ContentAreaWidth - 40, ContentBodyHeight - 320),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoGenerateColumns = false,
                Font = defaultFont,
                GridColor = Color.LightGray,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 40 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                EnableHeadersVisualStyles = false
            };

            // Cấu hình Style Guna2DataGridView
            dgvData.DefaultCellStyle.BackColor = Color.White;
            dgvData.DefaultCellStyle.ForeColor = Color.Black;
            dgvData.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvData.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvData.ColumnHeadersDefaultCellStyle.Font = boldFont;

            // GÁN SỰ KIỆN
            dgvData.SelectionChanged += dgvData_SelectionChanged;
            dgvData.CellClick += dgvData_CellClick;
            dgvData.CellFormatting += DgvData_CellFormatting;
            dgvData.CellPainting += DgvData_CellPainting;
            dgvData.ColumnHeaderMouseClick += dgvData_ColumnHeaderMouseClick;
            dgvData.CellPainting += DgvData_HeaderCellPainting;

            pnlContent.Controls.Add(dgvData);
        }

        private Guna2Panel CreateStatCard(int index, string title, string count, string subText, Color bgColor)
        {
            Guna2Panel pnlStat = new Guna2Panel
            {
                Tag = "StatCard",
                Size = new Size(CardWidth, CardHeight),
                Location = new Point(20 + index * (CardWidth + 20), CardY),
                FillColor = bgColor,
                BorderRadius = 10,
            };

            // 1. Label Title
            Label lblTitleStat = new Label
            {
                Text = title,
                Location = new Point(15, 10),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = CardTextColor,
                BackColor = Color.Transparent,
                AutoSize = true
            };

            // 2. Label Count (Số liệu lớn)
            Label lblCountStat = new Label
            {
                Text = count,
                Location = new Point(15, 35),
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = CardTextColor,
                BackColor = Color.Transparent,
                AutoSize = true
            };

            // 3. Label SubText
            Label lblSubText = new Label
            {
                Text = subText,
                Location = new Point(15, 88),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = CardTextColor,
                BackColor = Color.Transparent,
                AutoSize = true
            };

            pnlStat.Controls.AddRange(new Control[] { lblTitleStat, lblCountStat, lblSubText });
            return pnlStat;
        }

        // --- XỬ LÝ CHUYỂN TAB VÀ LOAD DATA ---

        private void ClearStatCards()
        {
            var cards = pnlContent.Controls.OfType<Guna2Panel>().Where(p => p.Tag?.ToString() == "StatCard").ToList();
            foreach (var card in cards) { pnlContent.Controls.Remove(card); card.Dispose(); }
        }

        private void ShowNamHocTab()
        {
            btnNamHoc.FillColor = Color.FromArgb(0, 123, 255); btnNamHoc.ForeColor = Color.White; btnNamHoc.BorderThickness = 0;
            btnHocKy.FillColor = Color.White; btnHocKy.ForeColor = Color.Black; btnHocKy.BorderThickness = 1;
            btnAction.Text = "+ Tạo năm học mới"; btnAction.Tag = "NamHoc";
            ClearStatCards(); DisplayNamHocCards(); LoadNamHocData();
            dgvData.Invalidate();
        }

        private void ShowHocKyTab()
        {
            btnHocKy.FillColor = Color.FromArgb(0, 123, 255); btnHocKy.ForeColor = Color.White; btnHocKy.BorderThickness = 0;
            btnNamHoc.FillColor = Color.White; btnNamHoc.ForeColor = Color.Black; btnNamHoc.BorderThickness = 1;
            btnAction.Text = "+ Thêm học kỳ"; btnAction.Tag = "HocKy";
            ClearStatCards(); DisplayHocKyCards(); LoadHocKyData();
            dgvData.Invalidate();
        }

        private void DisplayNamHocCards()
        {
            var data = new[]
            {
                new { Title = "Năm học hiện tại", Count = "2024-2025", SubText = "Đang diễn ra", Color = Color.FromArgb(40, 167, 69) },
                new { Title = "Học kỳ", Count = "Học kỳ I", SubText = "01/09 - 31/12/2024", Color = Color.FromArgb(0, 123, 255) },
                new { Title = "Tổng năm học", Count = "3", SubText = "Trong hệ thống", Color = Color.FromArgb(253, 126, 20) },
            };
            for (int i = 0; i < data.Length; i++)
            {
                Guna2Panel pnlStat = CreateStatCard(i, data[i].Title, data[i].Count, data[i].SubText, data[i].Color);
                pnlContent.Controls.Add(pnlStat);
            }
            dgvData.Location = new Point(20, CardY + CardHeight + 20);
            dgvData.Size = new Size(ContentAreaWidth - 40, pnlContent.Height - (CardY + CardHeight + 40));
        }

        private void DisplayHocKyCards()
        {
            var data = new[]
            {
                new { Title = "Học kỳ hiện tại", Count = "Học kỳ I", SubText = "2024-2025", Color = Color.FromArgb(0, 123, 255) },
                new { Title = "Thời gian", Count = "4 tháng", SubText = "01/09 - 31/12/2024", Color = Color.FromArgb(40, 167, 69) },
                new { Title = "Tổng học kỳ", Count = "4", SubText = "Trong hệ thống", Color = Color.FromArgb(102, 16, 242) },
            };
            for (int i = 0; i < data.Length; i++)
            {
                Guna2Panel pnlStat = CreateStatCard(i, data[i].Title, data[i].Count, data[i].SubText, data[i].Color);
                pnlContent.Controls.Add(pnlStat);
            }
            dgvData.Location = new Point(20, CardY + CardHeight + 20);
            dgvData.Size = new Size(ContentAreaWidth - 40, pnlContent.Height - (CardY + CardHeight + 40));
        }

        private void LoadNamHocData()
        {
            dgvData.Columns.Clear();
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamHoc", HeaderText = "Năm học", DataPropertyName = "NamHoc", Width = 250 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayBatDau", HeaderText = "Ngày bắt đầu", DataPropertyName = "NgayBatDau", Width = 250 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayKetThuc", HeaderText = "Ngày kết thúc", DataPropertyName = "NgayKetThuc", Width = 250 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "Trạng thái", DataPropertyName = "TrangThai", Width = 250 });
            dgvData.Columns.Add(new DataGridViewImageColumn { Name = "ThaoTac", HeaderText = "Thao tác", Width = 200, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            var namHocData = new[]
            {
                new { NamHoc = "2024-2025", NgayBatDau = "01/09/2024", NgayKetThuc = "31/05/2025", TrangThai = "Đang diễn ra" },
                new { NamHoc = "2023-2024", NgayBatDau = "01/09/2023", NgayKetThuc = "31/05/2024", TrangThai = "Đã kết thúc" },
                new { NamHoc = "2022-2023", NgayBatDau = "01/09/2022", NgayKetThuc = "31/05/2023", TrangThai = "Đã kết thúc" },
            };
            dgvData.DataSource = namHocData;
            dgvData.ClearSelection();
        }

        private void LoadHocKyData()
        {
            dgvData.Columns.Clear();
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamHoc", HeaderText = "Năm học", DataPropertyName = "NamHoc", Width = 200 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "HocKy", HeaderText = "Học kỳ", DataPropertyName = "HocKy", Width = 150 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayBatDau", HeaderText = "Ngày bắt đầu", DataPropertyName = "NgayBatDau", Width = 200 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayKetThuc", HeaderText = "Ngày kết thúc", DataPropertyName = "NgayKetThuc", Width = 200 });
            dgvData.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "Trạng thái", DataPropertyName = "TrangThai", Width = 200 });
            dgvData.Columns.Add(new DataGridViewImageColumn { Name = "ThaoTac", HeaderText = "Thao tác", Width = 200, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            var hocKyData = new[]
            {
                new { NamHoc = "2024-2025", HocKy = "Học kỳ I", NgayBatDau = "01/09/2024", NgayKetThuc = "31/12/2024", TrangThai = "Đang diễn ra" },
                new { NamHoc = "2024-2025", HocKy = "Học kỳ II", NgayBatDau = "01/01/2025", NgayKetThuc = "31/05/2025", TrangThai = "Chưa bắt đầu" },
                new { NamHoc = "2023-2024", HocKy = "Học kỳ I", NgayBatDau = "01/09/2023", NgayKetThuc = "31/12/2023", TrangThai = "Đã kết thúc" },
                new { NamHoc = "2023-2024", HocKy = "Học kỳ II", NgayBatDau = "01/01/2024", NgayKetThuc = "31/05/2024", TrangThai = "Đã kết thúc" },
            };
            dgvData.DataSource = hocKyData;
            dgvData.ClearSelection();
        }

        // --- XỬ LÝ PAINTING VÀ CẬP NHẬT HEADER KHI CLICK ---

        // Buộc DataGridView vẽ lại Header khi có cột được chọn
        private void DgvData_HeaderCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                Color backColor = dgvData.Columns[e.ColumnIndex].Selected ? columnHeaderSelectionColor : dgvData.ColumnHeadersDefaultCellStyle.BackColor;

                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                e.PaintContent(e.ClipBounds);
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
        }

        // Chặn chọn dòng (Row Select)
        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                dgvData.ClearSelection();
            }
            dgvData.Invalidate();
        }

        // Đánh dấu cột được click là Selected và buộc vẽ lại Header
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                foreach (DataGridViewColumn col in dgvData.Columns)
                {
                    col.Selected = false;
                }
                dgvData.Columns[e.ColumnIndex].Selected = true;
                dgvData.Invalidate();
            }
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                col.Selected = false;
            }
            dgvData.Columns[e.ColumnIndex].Selected = true;
            dgvData.Invalidate();
        }

        // --- CÁC HÀM XỬ LÝ SỰ KIỆN KHÁC ---

        private void BtnAction_Click(object? sender, EventArgs e)
        {
            if (btnAction.Tag?.ToString() == "NamHoc") { MessageBox.Show("Mở Form Tạo năm học mới", "Hành động"); }
            else if (btnAction.Tag?.ToString() == "HocKy") { MessageBox.Show("Mở Form Thêm học kỳ", "Hành động"); }
        }

        private void DgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvData.Columns[e.ColumnIndex].Name != "TrangThai") return;
            string status = e.Value?.ToString() ?? string.Empty;
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Color backColor, foreColor;
            if (status == "Đang diễn ra") { backColor = Color.FromArgb(220, 255, 230); foreColor = Color.FromArgb(40, 167, 69); }
            else if (status == "Chưa bắt đầu") { backColor = Color.FromArgb(230, 240, 255); foreColor = Color.FromArgb(0, 123, 255); }
            else { backColor = Color.White; foreColor = Color.Gray; }

            e.CellStyle.BackColor = backColor;
            e.CellStyle.ForeColor = foreColor;

            if ((dgvData.Rows[e.RowIndex].Selected))
            {
                e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
                e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
            }
        }

        private void DgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvData.Columns[e.ColumnIndex].Name == "ThaoTac" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                int iconSize = 20;
                int padding = 10;
                int xStart = e.CellBounds.Left + 2 * padding;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;
                Font iconFont = new Font("Segoe UI Emoji", 12);
                TextRenderer.DrawText(e.Graphics, "✏️", iconFont, new Rectangle(xStart, y, iconSize, iconSize), Color.Blue, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                xStart += iconSize + padding;
                TextRenderer.DrawText(e.Graphics, "🗑️", iconFont, new Rectangle(xStart, y, iconSize, iconSize), Color.Red, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                e.Handled = true;
            }
        }
    }
}