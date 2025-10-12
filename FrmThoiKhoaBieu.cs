using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class FrmThoiKhoaBieu : Form
    {
        private TableLayoutPanel? timetableLayout;

        // Định nghĩa màu cho từng môn học
        private readonly Dictionary<string, Color> SubjectColors = new Dictionary<string, Color>()
        {
            {"Toán học", Color.FromArgb(170, 209, 245)},
            {"Hóa học", Color.FromArgb(254, 196, 203)},
            {"GDCD", Color.FromArgb(226, 178, 245)},
            {"Ngữ văn", Color.FromArgb(184, 244, 219)},
            {"Sinh học", Color.FromArgb(204, 240, 182)},
            {"Tiếng Anh", Color.FromArgb(238, 201, 245)},
            {"Lịch sử", Color.FromArgb(249, 245, 184)},
            {"Vật lý", Color.FromArgb(254, 210, 173)},
            {"Thể dục", Color.FromArgb(230, 252, 194)},
            {"Địa lý", Color.FromArgb(173, 216, 255)},
            {"Tin học", Color.FromArgb(193, 230, 255)},
            {"Quốc phòng", Color.FromArgb(238, 238, 238)},
            {"Tự học", Color.FromArgb(238, 238, 238)},
            {"Sinh hoạt lớp", Color.FromArgb(193, 230, 255)}
        };

        // Dữ liệu thời khóa biểu mẫu: [Tiết][Thứ] (0=Tiết 1, 0=Thứ 2)
        private readonly (string Subject, string Teacher, string Room)[,] TimetableData = new (string, string, string)[5, 6]
        {
            // Tiết 1 (T2, T3, T4, T5, T6, T7)
            {("Toán học", "Nguyễn T. Hoa", "A101"), ("Vật lý", "Hoàng T. Lan", "A102"), ("Tiếng Anh", "Lê T. Mai", "C301"), ("Sinh học", "Vũ V. Hùng", "A103"), ("Toán học", "Nguyễn T. Hoa", "A101"), ("Thể dục", "Phạm V. Đức", "Sân TD")},
            // Tiết 2
            {("Toán học", "Nguyễn T. Hoa", "A101"), ("Vật lý", "Hoàng T. Lan", "A102"), ("Tiếng Anh", "Lê T. Mai", "C301"), ("Hóa học", "Vũ V. Hùng", "A103"), ("Ngữ văn", "Trần V. Nam", "B201"), ("Thể dục", "Phạm V. Đức", "Sân TD")},
            // Tiết 3
            {("Ngữ văn", "Trần V. Nam", "B201"), ("Hóa học", "Vũ V. Hùng", "A103"), ("Toán học", "Nguyễn T. Hoa", "A101"), ("Vật lý", "Hoàng T. Lan", "A102"), ("Tiếng Anh", "Lê T. Mai", "C301"), ("Tự học", "-", "B201")},
            // Tiết 4
            {("Tiếng Anh", "Lê T. Mai", "C301"), ("Sinh học", "Đỗ T. Thu", "A104"), ("Lịch sử", "Ngô T. Hường", "B203"), ("Ngữ văn", "Trần V. Nam", "B201"), ("Quốc phòng", "Hoàng V. Kiên", "E501"), ("Tự học", "-", "B201")},
            // Tiết 5
            {("Thể dục", "Phạm V. Đức", "Sân TD"), ("GDCD", "Bùi V. Toàn", "B202"), ("Địa lý", "Trần V. Long", "B204"), ("Tin học", "Lê V. An", "D401"), ("Sinh hoạt lớp", "GVCN", "B201"), ("-", "-", "-")},
        };

        public FrmThoiKhoaBieu()
        {
            InitializeComponent();
            // Gắn sự kiện SizeChanged cho Content Panel để tính toán lại kích thước bảng
            this.contentPanel.SizeChanged += contentPanel_SizeChanged;
        }

        private void FrmThoiKhoaBieu_Load(object sender, EventArgs e)
        {
            // Thiết lập padding dưới cùng để đảm bảo thanh cuộn xuất hiện khi cần
            this.contentPanel.Padding = new Padding(0, 0, 0, 100);

            SetupTimetableLayout();
            GenerateTimetable();
            GenerateLegend();

            // Kích hoạt tính toán lại layout ngay khi load (khi Form đã Maximized)
            contentPanel_SizeChanged(this.contentPanel, EventArgs.Empty);
        }

        private void contentPanel_SizeChanged(object sender, EventArgs e)
        {
            // Khoảng cách từ đỉnh contentPanel đến timetableHostPanel
            const int topOffset = 150;
            // Khoảng cách dưới cùng cho legend và padding
            const int bottomPadding = 30;
            // Chiều cao cố định của Legend Title và FlowPanel (khoảng 100px)
            const int legendHeight = 100;

            // Chiều cao tối đa mà timetableHostPanel có thể chiếm
            int availableHeight = this.contentPanel.Height - topOffset - bottomPadding - legendHeight;

            // Đảm bảo không bị quá nhỏ
            if (availableHeight < 200) availableHeight = 200;

            // 1. Cập nhật kích thước timetableHostPanel
            // Chiều ngang: contentPanel.Width - 40 (20px padding mỗi bên)
            timetableHostPanel.Size = new Size(this.contentPanel.Width - 40, availableHeight);

            // 2. Cập nhật vị trí của Legend
            int newLegendTitleY = timetableHostPanel.Bottom + 30; // 30px margin
            legendTitleLabel.Location = new Point(20, newLegendTitleY);

            int newLegendFlowPanelY = legendTitleLabel.Bottom + 5; // 5px margin
            legendFlowPanel.Location = new Point(20, newLegendFlowPanelY);
            legendFlowPanel.Width = timetableHostPanel.Width;
        }

        #region Logic Tạo Bảng

        private void SetupTimetableLayout()
        {
            timetableLayout = new TableLayoutPanel();
            timetableLayout.Dock = DockStyle.Fill;
            timetableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            timetableLayout.BackColor = Color.White;
            timetableLayout.Padding = new Padding(1);

            timetableLayout.ColumnCount = 7;
            timetableLayout.RowCount = 6;

            // Cột Tiết học cố định 50px
            timetableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            // Các cột ngày còn lại chia đều theo Percentage
            for (int i = 1; i < 7; i++)
            {
                timetableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / 6));
            }
            // Hàng Header cố định 40px
            timetableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            // Các hàng tiết học chia đều theo Percentage
            for (int i = 1; i < 6; i++)
            {
                timetableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 5));
            }

            this.timetableHostPanel.Controls.Add(timetableLayout);

            string[] days = { "", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            for (int col = 0; col < 7; col++)
            {
                AddHeaderCell(days[col], col, 0);
            }
            for (int row = 1; row < 6; row++)
            {
                AddHeaderCell($"Tiết {row}", 0, row);
            }
        }

        private void AddHeaderCell(string text, int col, int row)
        {
            Guna2HtmlLabel lbl = new Guna2HtmlLabel();
            lbl.Text = text;
            lbl.Dock = DockStyle.Fill;
            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl.ForeColor = Color.DimGray;
            lbl.TextAlignment = ContentAlignment.MiddleCenter;

            Guna2Panel headerCell = new Guna2Panel();
            headerCell.Dock = DockStyle.Fill;
            headerCell.FillColor = Color.WhiteSmoke;
            headerCell.BorderRadius = 0;
            headerCell.Controls.Add(lbl);

            timetableLayout?.Controls.Add(headerCell, col, row);
        }

        private void GenerateTimetable()
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 6; col++)
                {
                    var data = TimetableData[row, col];
                    timetableLayout?.Controls.Add(CreateSubjectPanel(data.Subject, data.Teacher, data.Room), col + 1, row + 1);
                }
            }
        }

        private Guna2Panel CreateSubjectPanel(string subject, string teacher, string room)
        {
            Color bgColor = SubjectColors.ContainsKey(subject) ? SubjectColors[subject] : Color.White;

            Guna2Panel subjectPanel = new Guna2Panel();
            subjectPanel.Dock = DockStyle.Fill;
            subjectPanel.FillColor = bgColor;
            subjectPanel.BorderRadius = 8;
            subjectPanel.Margin = new Padding(3);
            subjectPanel.Padding = new Padding(5);

            FlowLayoutPanel flowLayout = new FlowLayoutPanel();
            flowLayout.Dock = DockStyle.Fill;
            flowLayout.FlowDirection = FlowDirection.TopDown;
            flowLayout.WrapContents = false;
            flowLayout.BackColor = Color.Transparent;

            Guna2HtmlLabel lblSubject = new Guna2HtmlLabel();
            lblSubject.Text = subject;
            lblSubject.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSubject.ForeColor = Color.Black;
            lblSubject.BackColor = Color.Transparent;

            Guna2HtmlLabel lblTeacher = new Guna2HtmlLabel();
            lblTeacher.Text = teacher;
            lblTeacher.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            lblTeacher.ForeColor = Color.DimGray;
            lblTeacher.BackColor = Color.Transparent;

            Guna2HtmlLabel lblRoom = new Guna2HtmlLabel();
            lblRoom.Text = room;
            lblRoom.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            lblRoom.ForeColor = Color.FromArgb(70, 70, 70);
            lblRoom.BackColor = Color.Transparent;

            flowLayout.Controls.Add(lblSubject);
            if (!string.IsNullOrEmpty(teacher) && teacher != "-") flowLayout.Controls.Add(lblTeacher);
            if (!string.IsNullOrEmpty(room) && room != "-") flowLayout.Controls.Add(lblRoom);

            subjectPanel.Controls.Add(flowLayout);

            return subjectPanel;
        }

        private void GenerateLegend()
        {
            legendFlowPanel.FlowDirection = FlowDirection.LeftToRight;
            legendFlowPanel.WrapContents = true;
            legendFlowPanel.AutoScroll = true;
            legendFlowPanel.Padding = new Padding(0, 10, 0, 0);

            var subjectsToDisplay = SubjectColors
                .Where(item => item.Key != "-")
                .Select(item => new { Subject = item.Key, Color = item.Value })
                .Distinct()
                .ToList();

            foreach (var item in subjectsToDisplay)
            {
                Guna2Panel legendItem = new Guna2Panel();
                legendItem.Width = 150;
                legendItem.Height = 25;
                legendItem.Margin = new Padding(0, 0, 20, 10);
                legendItem.BackColor = Color.White;

                Guna2Panel colorBox = new Guna2Panel();
                colorBox.Size = new Size(15, 15);
                colorBox.FillColor = item.Color;
                colorBox.Location = new Point(0, 5);
                colorBox.BorderRadius = 3;

                Guna2HtmlLabel lbl = new Guna2HtmlLabel();
                lbl.Text = item.Subject;
                lbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                lbl.Location = new Point(25, 3);
                lbl.AutoSize = true;

                legendItem.Controls.Add(colorBox);
                legendItem.Controls.Add(lbl);

                legendFlowPanel.Controls.Add(legendItem);
            }
        }
        #endregion

        private void contentPanel_Paint(object sender, PaintEventArgs e) { }
        private void timetableHostPanel_Paint(object sender, PaintEventArgs e) { }
        private void contentPanel_Paint_1(object sender, PaintEventArgs e) { }
    }
}