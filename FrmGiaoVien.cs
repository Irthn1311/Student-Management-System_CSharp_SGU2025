using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class FrmGiaoVien : Form
    {
        // Kích thước giao diện
        private const int FormWidth = 1440;
        private const int FormHeight = 900;

        // Kích thước thành phần
        private const int SidebarWidth = 192;
        private const int HeaderHeight = 56;
        private const int ContentWidth = FormWidth - SidebarWidth; // 1248
        private const int ContentHeight = FormHeight - HeaderHeight; // 844

        // Khai báo các Controls chính
        private Panel pnlSidebar;
        private Panel pnlHeader;
        private Panel pnlContent;
        private DataGridView dgvTeachers;

        public FrmGiaoVien()
        {
            InitializeComponent();
            this.Width = FormWidth;
            this.Height = FormHeight;
            this.Text = "1";
            this.BackColor = Color.FromArgb(249, 250, 252);
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Khóa kích thước Form
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        private void FrmGiaoVien_Load(object sender, EventArgs e)
        {
            InitializeCustomControls();
            LoadTeacherData();
        }

        private void InitializeCustomControls()
        {
            // -----------------------------------------------------
            // SIDEBAR PANEL (192x900) - Màu trắng
            // -----------------------------------------------------
            pnlSidebar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(SidebarWidth, FormHeight),
                BackColor = Color.White,
            };
            this.Controls.Add(pnlSidebar);


            // -----------------------------------------------------
            // HEADER PANEL (1248x56)
            // -----------------------------------------------------
            pnlHeader = new Panel
            {
                Location = new Point(SidebarWidth, 0),
                Size = new Size(ContentWidth, HeaderHeight),
                BackColor = Color.White,
            };
            this.Controls.Add(pnlHeader);

            // ... (Khối Header giữ nguyên) ...
            Label lblTitle = new Label { Text = "Quản Lý Giáo viên", Location = new Point(20, 10), Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
            Label lblSubtitle = new Label { Text = "Trang chủ / Quản lý Giáo viên", Location = new Point(20, 32), Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Gray, AutoSize = true };
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);

            int rightEdge = ContentWidth - 10;
            int searchBlockWidth = 400;
            int searchBlockStartX = rightEdge - searchBlockWidth;
            TextBox txtSearch = new TextBox { Text = "Tìm kiếm...", Location = new Point(searchBlockStartX + 55, 14), Size = new Size(200, 28), BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 10, FontStyle.Italic) };
            Label lblSearchIcon = new Label { Text = "🔍", Location = new Point(searchBlockStartX + 35, 17), AutoSize = true, ForeColor = Color.Gray };
            Button btnNotification = new Button { Text = "🔔", Location = new Point(searchBlockStartX + 270, 10), Size = new Size(35, 35), FlatStyle = FlatStyle.Flat, BackColor = Color.Transparent, FlatAppearance = { BorderSize = 0, MouseDownBackColor = Color.LightGray, MouseOverBackColor = Color.WhiteSmoke } };
            Panel pnlAvatar = new Panel { Location = new Point(searchBlockStartX + 320, 10), Size = new Size(36, 36), BackColor = Color.FromArgb(0, 123, 255), BorderStyle = BorderStyle.None, };
            Label lblAvatarText = new Label { Text = "NV", Location = new Point(0, 0), Size = new Size(36, 36), Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.White, TextAlign = ContentAlignment.MiddleCenter, };
            pnlAvatar.Controls.Add(lblAvatarText);
            Label lblUserName = new Label { Text = "Nguyễn Văn A", Location = new Point(searchBlockStartX + 365, 10), Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true };
            Label lblUserRole = new Label { Text = "Quản lý", Location = new Point(searchBlockStartX + 365, 28), Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Gray, AutoSize = true };
            pnlHeader.Controls.AddRange(new Control[] { lblSearchIcon, txtSearch, btnNotification, pnlAvatar, lblUserName, lblUserRole });


            // -----------------------------------------------------
            // CONTENT PANEL (1248x844)
            // -----------------------------------------------------
            pnlContent = new Panel
            {
                Location = new Point(SidebarWidth, HeaderHeight),
                Size = new Size(ContentWidth, ContentHeight),
                BackColor = this.BackColor,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right // Đảm bảo Content Panel cố định 
            };
            this.Controls.Add(pnlContent);

            // ... (Khối Controls Content trên giữ nguyên) ...
            Button btnAddTeacher = new Button { Text = "+ Thêm giáo viên", Location = new Point(20, 20), Size = new Size(130, 40), BackColor = Color.FromArgb(0, 123, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderSize = 0 }, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            pnlContent.Controls.Add(btnAddTeacher);
            Button btnAllSubjects = new Button { Text = "Tất cả bộ môn", Location = new Point(ContentWidth - 20 - 130, 20), Size = new Size(130, 40), BackColor = Color.White, ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, FlatAppearance = { BorderColor = Color.LightGray, BorderSize = 1 }, Font = new Font("Segoe UI", 10, FontStyle.Regular) };
            pnlContent.Controls.Add(btnAllSubjects);

            int cardSpacing = 20;
            int cardWidth = (ContentWidth - 5 * cardSpacing) / 4;
            int cardHeight = 120;
            int cardY = 80;
            var stats = new[] { new { Title = "Tổng giáo viên", Count = "87", Color = Color.Black, X = 0 }, new { Title = "Nam", Count = "42", Color = Color.Blue, X = 1 }, new { Title = "Nữ", Count = "45", Color = Color.Red, X = 2 }, new { Title = "Bộ môn", Count = "12", Color = Color.Green, X = 3 } };
            foreach (var stat in stats)
            {
                Panel pnlStat = new Panel { Size = new Size(cardWidth, cardHeight), Location = new Point(cardSpacing + stat.X * (cardWidth + cardSpacing), cardY), BackColor = Color.White, BorderStyle = BorderStyle.None, Margin = new Padding(0) };
                Label lblTitleStat = new Label { Text = stat.Title, Location = new Point(15, 10), Font = new Font("Segoe UI", 10, FontStyle.Regular), ForeColor = Color.Gray, AutoSize = true };
                Label lblCountStat = new Label { Text = stat.Count, Location = new Point(15, 45), Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = stat.Color, AutoSize = true };
                pnlStat.Controls.Add(lblTitleStat);
                pnlStat.Controls.Add(lblCountStat);
                pnlContent.Controls.Add(pnlStat);
            }

            // -----------------------------------------------------
            // DATAGRIDVIEW (KHÓA KÍCH THƯỚC VÀ VỊ TRÍ)
            // -----------------------------------------------------
            dgvTeachers = new DataGridView
            {
                Location = new Point(20, cardY + cardHeight + 20),
                Size = new Size(ContentWidth - 40, ContentHeight - (cardY + cardHeight + 40)),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true, // Khóa chỉnh sửa dữ liệu
                AllowUserToResizeColumns = false, // Khóa chỉnh sửa kích thước cột
                AllowUserToResizeRows = false, // Khóa chỉnh sửa kích thước hàng
                AutoGenerateColumns = false,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Name = "dgvTeachers",
                // KHÓA VỊ TRÍ: Chỉ neo vào Top và Left của Content Panel
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            // Cấu hình Style (Giữ nguyên)
            dgvTeachers.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvTeachers.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvTeachers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTeachers.EnableHeadersVisualStyles = false;
            dgvTeachers.ColumnHeadersHeight = 40;
            dgvTeachers.RowTemplate.Height = 40;
            dgvTeachers.GridColor = Color.LightGray;
            dgvTeachers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 255);
            dgvTeachers.DefaultCellStyle.SelectionForeColor = Color.Black;

            pnlContent.Controls.Add(dgvTeachers);

            dgvTeachers.CellFormatting += DgvTeachers_CellFormatting;
            dgvTeachers.CellPainting += DgvTeachers_CellPainting;
        }

        // Hàm thêm dữ liệu và cấu hình cột cho DataGridView
        private void LoadTeacherData()
        {
            // 1. Cấu hình các cột - TÍNH TOÁN CÁC CỘT VÀ KHÓA KÍCH THƯỚC NẰM NGANG
            // Tổng chiều rộng DataGridView: 1248 - 40 = 1208
            // Tổng cố định (100+200+100+200+200+180) = 980
            // Cột Thao tác (fill) = 1208 - 980 = 228
            const int ThaoTacWidth = 228; // Khóa chiều rộng cột Thao tác

            // Cột 1: Mã GV (Text)
            dgvTeachers.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaGV", HeaderText = "Mã GV", DataPropertyName = "MaGV", Width = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });
            // Cột 2: Họ và tên (Text)
            dgvTeachers.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "Họ và tên", DataPropertyName = "HoTen", Width = 200, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });
            // Cột 3: Giới tính (Text tùy chỉnh màu/vẽ)
            dgvTeachers.Columns.Add(new DataGridViewTextBoxColumn { Name = "GioiTinh", HeaderText = "Giới tính", DataPropertyName = "GioiTinh", Width = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.None, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            // Cột 4: Chuyên môn (Text)
            dgvTeachers.Columns.Add(new DataGridViewTextBoxColumn { Name = "ChuyenMon", HeaderText = "Chuyên môn", DataPropertyName = "ChuyenMon", Width = 200, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });
            // Cột 5: Lớp giảng dạy (Text)
            dgvTeachers.Columns.Add(new DataGridViewTextBoxColumn { Name = "LopGiangDay", HeaderText = "Lớp giảng dạy", DataPropertyName = "LopGiangDay", Width = 200, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });
            // Cột 6: SĐT (Text)
            dgvTeachers.Columns.Add(new DataGridViewTextBoxColumn { Name = "SDT", HeaderText = "SĐT", DataPropertyName = "SDT", Width = 180, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });
            // Cột 7: Thao tác (Image/Icon) - Khóa chiều rộng
            dgvTeachers.Columns.Add(new DataGridViewImageColumn { Name = "ThaoTac", HeaderText = "Thao tác", Width = ThaoTacWidth, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });

            // 2. Thêm dữ liệu mẫu (Giữ nguyên)
            var teachers = new[]
            {
                new { MaGV = "GV001", HoTen = "Nguyễn Thị Hoa", GioiTinh = "Nữ", ChuyenMon = "Toán học", LopGiangDay = "10A1, 10A2", SDT = "0912345678" },
                new { MaGV = "GV002", HoTen = "Trần Văn Nam", GioiTinh = "Nam", ChuyenMon = "Ngữ văn", LopGiangDay = "10A1, 11A1", SDT = "0912345679" },
                new { MaGV = "GV003", HoTen = "Lê Thị Mai", GioiTinh = "Nữ", ChuyenMon = "Tiếng Anh", LopGiangDay = "10A2, 10A3", SDT = "0912345680" },
                new { MaGV = "GV004", HoTen = "Phạm Văn Đức", GioiTinh = "Nam", ChuyenMon = "Vật lý", LopGiangDay = "11A1, 11A2", SDT = "0912345681" },
                new { MaGV = "GV005", HoTen = "Hoàng Thị Lan", GioiTinh = "Nữ", ChuyenMon = "Hóa học", LopGiangDay = "11A2, 11A3", SDT = "0912345682" }
            };

            dgvTeachers.DataSource = teachers;
        }

        // HỦY BỎ CellFormatting CŨ ĐỂ DÙNG CellPainting
        private void DgvTeachers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTeachers.Columns[e.ColumnIndex].Name == "GioiTinh" && e.RowIndex >= 0)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.FormattingApplied = true;
            }
        }

        // Xử lý CellPainting để vẽ bo góc cho cột Giới tính và icon Thao tác (Giữ nguyên logic)
        private void DgvTeachers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 1. Xử lý cột GIỚI TÍNH (Vẽ bo góc)
            if (dgvTeachers.Columns[e.ColumnIndex].Name == "GioiTinh")
            {
                e.PaintBackground(e.CellBounds, e.RowIndex % 2 != 0);
                e.Handled = true;

                string gioiTinh = e.Value?.ToString() ?? "";

                Color backColor, foreColor;
                if (gioiTinh == "Nam")
                {
                    backColor = Color.FromArgb(220, 230, 255);
                    foreColor = Color.Blue;
                }
                else if (gioiTinh == "Nữ")
                {
                    backColor = Color.FromArgb(255, 230, 235);
                    foreColor = Color.Red;
                }
                else
                {
                    backColor = Color.LightGray;
                    foreColor = Color.Black;
                }

                const int tagHeight = 25;
                const int cornerRadius = 12;

                SizeF textSize = e.Graphics.MeasureString(gioiTinh, e.CellStyle.Font);
                int tagWidth = (int)textSize.Width + 16;

                int tagX = e.CellBounds.Left + (e.CellBounds.Width - tagWidth) / 2;
                int tagY = e.CellBounds.Top + (e.CellBounds.Height - tagHeight) / 2;

                Rectangle tagRect = new Rectangle(tagX, tagY, tagWidth, tagHeight);

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(tagRect.X, tagRect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
                    path.AddArc(tagRect.Right - cornerRadius * 2, tagRect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
                    path.AddArc(tagRect.Right - cornerRadius * 2, tagRect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
                    path.AddArc(tagRect.X, tagRect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
                    path.CloseAllFigures();

                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }

                TextRenderer.DrawText(e.Graphics, gioiTinh, e.CellStyle.Font, tagRect, foreColor,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            // 2. Xử lý cột THAO TÁC (Vẽ icon)
            else if (dgvTeachers.Columns[e.ColumnIndex].Name == "ThaoTac")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                int iconSize = 20;
                int padding = 15;
                int cellCenterY = e.CellBounds.Top + (e.CellBounds.Height / 2);

                int totalIconWidth = 3 * iconSize + 2 * padding;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalIconWidth) / 2;

                int y = cellCenterY - (iconSize / 2);
                Font iconFont = new Font("Segoe UI Emoji", 12);

                TextRenderer.DrawText(e.Graphics, "◎", iconFont, new Rectangle(startX, y, iconSize, iconSize), Color.FromArgb(0, 123, 255), TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                int xStart = startX + iconSize + padding;
                TextRenderer.DrawText(e.Graphics, "✏️", iconFont, new Rectangle(xStart, y, iconSize, iconSize), Color.Green, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                xStart += iconSize + padding;
                TextRenderer.DrawText(e.Graphics, "🗑️", iconFont, new Rectangle(xStart, y, iconSize, iconSize), Color.Red, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                e.Handled = true;
            }
            else
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                e.Handled = false;
            }
        }

        // Cần có hàm này để sử dụng GraphicsPath. (Giữ nguyên)
        public static class GraphicsExtensions
        {
            public static GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                path.StartFigure();
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseFigure();
                return path;
            }
        }
    }
}