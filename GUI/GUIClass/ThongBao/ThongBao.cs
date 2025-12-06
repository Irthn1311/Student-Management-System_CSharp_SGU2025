using Guna.UI2.WinForms; // Đảm bảo đã thêm using này
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThongBao // Đổi namespace nếu cần
{
    public partial class ThongBao : UserControl
    {
        public ThongBao()
        {
            InitializeComponent();
        }

        private void ThongBao_Load(object sender, EventArgs e)
        {
            // --- Cấu hình các bảng ---
            SetupTableThongBao();

            // --- Nạp dữ liệu mẫu ---
            LoadSampleDataThongBao();

         

            // --- Cấu hình Thẻ Thống kê ---
            SetupStatCards();
        }

        // Cấu hình các thẻ thống kê
        private void SetupStatCards()
        {
           
            // Màu chữ mặc định cho Tiêu đề và Ghi chú (ví dụ: xám đậm)
            Color defaultTextColor = Color.FromArgb(71, 85, 105); // Slate-700

            // Thẻ 1: Tổng thông báo (Dùng màu xanh dương)
            statCardTongThongBao.lbCardTitle.Text = "Tổng thông báo";
            statCardTongThongBao.lbCardValue.Text = "58"; // Dữ liệu mẫu
            statCardTongThongBao.lbCardNote.Text = "Trong hệ thống";
            // statCardTongThongBao.guna2Panel1.FillColor = Color.FromArgb(239, 246, 255); // Nền xanh dương rất nhạt (tùy chọn)
            statCardTongThongBao.lbCardValue.ForeColor = Color.FromArgb(37, 99, 235);     // Giá trị màu xanh dương đậm (Blue-600)
            statCardTongThongBao.lbCardTitle.ForeColor = defaultTextColor;
            statCardTongThongBao.lbCardNote.ForeColor = defaultTextColor;


            // Thẻ 2: Gửi GV (Dùng màu xanh lá)
            statCardThongBaoGiaoVien.lbCardTitle.Text = "Gửi Giáo viên";
            statCardThongBaoGiaoVien.lbCardValue.Text = "15"; // Dữ liệu mẫu
            statCardThongBaoGiaoVien.lbCardNote.Text = "Thông báo gần đây";
            // statCardThongBaoGiaoVien.guna2Panel1.FillColor = Color.FromArgb(240, 253, 244); // Nền xanh lá rất nhạt (tùy chọn)
            statCardThongBaoGiaoVien.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);     // Giá trị màu xanh lá đậm (Green-600)
            statCardThongBaoGiaoVien.lbCardTitle.ForeColor = defaultTextColor;
            statCardThongBaoGiaoVien.lbCardNote.ForeColor = defaultTextColor;

            // Thẻ 3: Gửi HS (Dùng màu cam)
            statCardThongBaoHocSinh.lbCardTitle.Text = "Gửi Học sinh";
            statCardThongBaoHocSinh.lbCardValue.Text = "25"; // Dữ liệu mẫu
            statCardThongBaoHocSinh.lbCardNote.Text = "Thông báo gần đây";
            // statCardThongBaoHocSinh.guna2Panel1.FillColor = Color.FromArgb(255, 247, 237); // Nền cam rất nhạt (tùy chọn)
            statCardThongBaoHocSinh.lbCardValue.ForeColor = Color.FromArgb(234, 88, 12);     // Giá trị màu cam đậm (Orange-600)
            statCardThongBaoHocSinh.lbCardTitle.ForeColor = defaultTextColor;
            statCardThongBaoHocSinh.lbCardNote.ForeColor = defaultTextColor;

            // Thẻ 4: Gửi GVụ (Dùng màu tím)
            statCardThongBaoGiaoVu.lbCardTitle.Text = "Gửi Giáo vụ";
            statCardThongBaoGiaoVu.lbCardValue.Text = "8"; // Dữ liệu mẫu
            statCardThongBaoGiaoVu.lbCardNote.Text = "Thông báo gần đây";
            // statCardThongBaoGiaoVu.guna2Panel1.FillColor = Color.FromArgb(245, 243, 255); // Nền tím rất nhạt (tùy chọn)
            statCardThongBaoGiaoVu.lbCardValue.ForeColor = Color.FromArgb(124, 58, 237);     // Giá trị màu tím đậm (Purple-600)
            statCardThongBaoGiaoVu.lbCardTitle.ForeColor = defaultTextColor;
            statCardThongBaoGiaoVu.lbCardNote.ForeColor = defaultTextColor;
        }

        #region Bảng Thông Báo

        private void SetupTableThongBao()
        {
            // --- Xóa cột cũ và cấu hình chung ---
            tableThongBao.Columns.Clear();
            ApplyBaseTableStyle(tableThongBao); // Áp dụng style chung

            // --- Thêm cột mới (Thêm cột NoiDung) ---
            tableThongBao.Columns.Add("MaTB", "Mã TB");
            tableThongBao.Columns.Add("TieuDe", "Tiêu đề");
            tableThongBao.Columns.Add("NoiDung", "Nội dung"); // <-- THÊM CỘT NỘI DUNG
            tableThongBao.Columns.Add("LoaiThongBao", "Loại");
            tableThongBao.Columns.Add("DoiTuongNhan", "Gửi đến");
            tableThongBao.Columns.Add("NgayDienRa", "Ngày diễn ra");
            tableThongBao.Columns.Add("NguoiTao", "Người tạo");
            tableThongBao.Columns.Add("NgayHetHan", "Ngày hết hạn");
            tableThongBao.Columns.Add("ThaoTac", "Thao tác");

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tableThongBao); // Áp dụng căn giữa mặc định
                                                            // Chỉnh lại căn lề cho các cột cần thiết
            tableThongBao.Columns["TieuDe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableThongBao.Columns["NoiDung"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Căn trái Nội dung
            tableThongBao.Columns["LoaiThongBao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Căn trái Loại
            tableThongBao.Columns["DoiTuongNhan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; // Căn trái Gửi đến
            tableThongBao.Columns["NguoiTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- Tùy chỉnh kích thước ---
            tableThongBao.Columns["MaTB"].Visible = false; // Ẩn cột Mã TB
            tableThongBao.Columns["TieuDe"].FillWeight = 25; tableThongBao.Columns["TieuDe"].MinimumWidth = 180;
            tableThongBao.Columns["NoiDung"].FillWeight = 35; tableThongBao.Columns["NoiDung"].MinimumWidth = 250; // Cho cột Nội dung rộng hơn
            tableThongBao.Columns["LoaiThongBao"].FillWeight = 10; tableThongBao.Columns["LoaiThongBao"].MinimumWidth = 80;
            tableThongBao.Columns["DoiTuongNhan"].FillWeight = 15; tableThongBao.Columns["DoiTuongNhan"].MinimumWidth = 100;
            tableThongBao.Columns["NgayDienRa"].FillWeight = 12; tableThongBao.Columns["NgayDienRa"].MinimumWidth = 100;
            tableThongBao.Columns["NguoiTao"].FillWeight = 10; tableThongBao.Columns["NguoiTao"].MinimumWidth = 80;
            tableThongBao.Columns["NgayHetHan"].FillWeight = 12; tableThongBao.Columns["NgayHetHan"].MinimumWidth = 100;
            tableThongBao.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableThongBao.Columns["ThaoTac"].Width = 100;

            // Thêm Padding cho cột Tiêu đề
            tableThongBao.Columns["TieuDe"].DefaultCellStyle.Padding = new Padding(10, 0, 0, 0); // Cách lề trái 10px

            // --- Gắn sự kiện ---
            tableThongBao.CellPainting -= tableThongBao_CellPainting; // Gỡ nếu có
            tableThongBao.CellPainting += tableThongBao_CellPainting;
            tableThongBao.CellClick -= tableThongBao_CellClick; // Gỡ nếu có
            tableThongBao.CellClick += tableThongBao_CellClick;
            tableThongBao.CellFormatting -= tableThongBao_CellFormatting; // Gỡ nếu có
            tableThongBao.CellFormatting += tableThongBao_CellFormatting; // <-- GẮN SỰ KIỆN ĐỊNH DẠNG MÀU
        }

        // Hàm định dạng màu cho cột "Gửi đến"
        private void tableThongBao_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua header

            // Chỉ xử lý cột "DoiTuongNhan"
            if (tableThongBao.Columns[e.ColumnIndex].Name == "DoiTuongNhan" && e.Value != null)
            {
                string doiTuong = e.Value.ToString().ToLower(); // Chuyển về chữ thường để dễ so sánh

                // Reset về màu mặc định trước khi áp dụng màu mới
                e.CellStyle.ForeColor = Color.FromArgb(40, 40, 40); // Màu chữ mặc định của bảng

                if (doiTuong == "admin")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(190, 24, 93); // Màu hồng đậm (ví dụ)
                }
                else if (doiTuong == "giaovu")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(124, 58, 237); // Màu tím đậm (ví dụ)
                }
                else if (doiTuong == "giaovien")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52); // Màu xanh lá cây đậm (ví dụ)
                }
                else if (doiTuong == "hocsinh")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(154, 52, 18); // Màu cam đậm (ví dụ)
                }
                else if (doiTuong == "all")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216); // Màu xanh dương đậm (ví dụ)
                }
                // Bạn có thể thêm các else if khác để xử lý màu cho lớp cụ thể (ví dụ: 10A1, 12A2) nếu muốn
                else if (doiTuong.Contains("10a") || doiTuong.Contains("11a") || doiTuong.Contains("12a")) // Ví dụ tô màu cho lớp
                {
                    e.CellStyle.ForeColor = Color.FromArgb(55, 48, 163); // Màu chàm (ví dụ)
                }
            }
        }

        private void LoadSampleDataThongBao()
        {
            tableThongBao.Rows.Clear();
            // Thêm dữ liệu mẫu (Thêm Nội dung và "" cho cột thao tác)
            tableThongBao.Rows.Add("TB001", "Họp phụ huynh lớp 12", "Nội dung họp phụ huynh cuối năm...", "Họp PH", "12A1, 12A2", "10/10/2025 08:00", "Admin", "16/10/2025", "");
            tableThongBao.Rows.Add("TB002", "Khen thưởng học sinh giỏi HK1", "Danh sách học sinh giỏi...", "Khen thưởng", "ALL", "09/10/2025 10:30", "Giáo vụ", "", "");
            tableThongBao.Rows.Add("TB003", "Báo cáo kết quả học tập tháng 9", "Yêu cầu GV nộp báo cáo...", "Báo cáo", "GiaoVien", "08/10/2025 15:00", "Admin", "15/10/2025", "");
            tableThongBao.Rows.Add("TB004", "Lịch thi giữa kỳ I", "Chi tiết lịch thi các môn...", "Lịch thi", "HocSinh", "07/10/2025 09:00", "Giáo vụ", "25/10/2025", "");
            tableThongBao.Rows.Add("TB005", "Thông báo nghỉ lễ", "Toàn trường được nghỉ lễ...", "Chung", "ALL", "05/10/2025 11:00", "Admin", "", "");
            tableThongBao.Rows.Add("TB006", "Sự kiện văn nghệ chào mừng", "Kế hoạch tổ chức văn nghệ...", "Sự kiện", "ALL", "04/10/2025 14:00", "Giáo vụ", "10/11/2025", "");
            tableThongBao.Rows.Add("TB007", "Nộp học phí kỳ 1", "Hạn chót nộp học phí...", "Học phí", "HocSinh", "01/10/2025 08:00", "Giáo vụ", "30/10/2025", "");
            tableThongBao.Rows.Add("TB008", "Đăng ký tham gia CLB", "Mở đơn đăng ký CLB...", "CLB", "HocSinh", "29/09/2025 16:00", "Giáo vụ", "15/10/2025", "");
            tableThongBao.Rows.Add("TB009", "Tập huấn PCCC cho GVNV", "Lịch tập huấn phòng cháy...", "Tập huấn", "GiaoVien, GiaoVu", "28/09/2025 10:00", "Admin", "05/11/2025", "");
            tableThongBao.Rows.Add("TB010", "Khảo sát chất lượng đầu năm", "Link khảo sát online...", "Khảo sát", "HocSinh", "25/09/2025 07:30", "Giáo vụ", "10/10/2025", "");
            tableThongBao.Rows.Add("TB011", "Kế hoạch năm học mới", "Chi tiết kế hoạch giảng dạy...", "Kế hoạch", "GiaoVien", "15/09/2025 13:30", "Admin", "", "");
            tableThongBao.Rows.Add("TB012", "Thông báo thay đổi lịch học", "Lớp 11A1 đổi tiết Lý...", "Lịch học", "11A1", "12/09/2025 17:00", "Giáo vụ", "", "");
        }

        // Vẽ icon cho bảng Thông Báo
        private void tableThongBao_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Copy code từ hàm vẽ icon của các bảng khác, chỉ đổi tên table và cột
            if (e.RowIndex >= 0 && e.ColumnIndex == tableThongBao.Columns["ThaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                Image editIcon = Properties.Resources.repair; // Đảm bảo có icon tên 'repair' trong Resources
                Image deleteIcon = Properties.Resources.bin;   // Đảm bảo có icon tên 'bin' trong Resources

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle editRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);

                e.Graphics.DrawImage(editIcon, editRect);
                e.Graphics.DrawImage(deleteIcon, deleteRect);

                e.Handled = true;
            }
        }

        // Xử lý click icon cho bảng Thông Báo
        private void tableThongBao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Copy code từ hàm click icon của các bảng khác, đổi tên table, cột và ID
            if (e.RowIndex >= 0 && e.ColumnIndex == tableThongBao.Columns["ThaoTac"].Index)
            {
                // Sử dụng hàm HandleIconClick chung, truyền vào MaTB
                HandleIconClick(tableThongBao, e.RowIndex, "MaTB");
            }
        }

        #endregion

        #region Hàm dùng chung và Helper (Copy từ HocSinh.cs nếu chưa có)

        // Hàm áp dụng style cơ bản cho DataGridView
        private void ApplyBaseTableStyle(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Mặc định căn giữa header
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ColumnHeadersHeight = 42; // Tăng chiều cao header

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.RowTemplate.Height = 46;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Xóa sự kiện cũ để tránh gắn nhiều lần
            dgv.CellMouseEnter -= DataGridView_CellMouseEnter;
            dgv.CellMouseLeave -= DataGridView_CellMouseLeave;
            dgv.SelectionChanged -= DataGridView_SelectionChanged;

            // Gắn sự kiện hover và selection
            dgv.CellMouseEnter += DataGridView_CellMouseEnter;
            dgv.CellMouseLeave += DataGridView_CellMouseLeave;
            dgv.SelectionChanged += DataGridView_SelectionChanged;

            // Đảm bảo màu header không đổi khi click
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
        }

        // Hàm căn chỉnh cột và wrap text
        private void ApplyColumnAlignmentAndWrapping(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Mặc định căn giữa cell
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }

        // Hàm xử lý click icon chung
        private void HandleIconClick(Guna.UI2.WinForms.Guna2DataGridView dgv, int rowIndex, string idColumnName)
        {
            Rectangle cellBounds = dgv.GetCellDisplayRectangle(dgv.Columns["ThaoTac"].Index, rowIndex, false); // Lấy ô thao tác
            Point clickPosInCell = dgv.PointToClient(Cursor.Position);
            int xClick = clickPosInCell.X - cellBounds.Left;

            int iconSize = 18;
            int spacing = 15;
            int totalWidth = iconSize * 2 + spacing;
            int startXInCell = (cellBounds.Width - totalWidth) / 2;

            int editIconEndX = startXInCell + iconSize;
            int deleteIconStartX = startXInCell + iconSize + spacing;
            int deleteIconEndX = deleteIconStartX + iconSize;

            string idValue = dgv.Rows[rowIndex].Cells[idColumnName].Value?.ToString() ?? "Thông báo này";

            if (xClick >= startXInCell && xClick < editIconEndX)
            {
                MessageBox.Show($"Bạn đã click Sửa cho: {idValue}");
                // TODO: Mở form sửa thông báo, truyền idValue vào
            }
            else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa {idValue}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dgv.Rows.RemoveAt(rowIndex);
                    MessageBox.Show("Đã xóa.");
                    // TODO: Thêm code xóa thông báo trong cơ sở dữ liệu
                }
            }
        }

        // Sự kiện hover chung
        private void DataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
                if (dgv != null) // Thêm kiểm tra null
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(243, 246, 255);
            }
        }

        // Sự kiện rời chuột chung
        private void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
                if (dgv != null) // Thêm kiểm tra null
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        (e.RowIndex % 2 == 0) ? Color.White : Color.FromArgb(250, 250, 250);
            }
        }

        // Bỏ chọn dòng khi selection thay đổi
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
            if (dgv != null) // Thêm kiểm tra null
                dgv.ClearSelection();
        }

        #endregion

        // Các sự kiện nút bấm khác (Thêm, Lọc, Tìm kiếm...) sẽ được thêm vào đây
        // private void btnThemThongBao_Click(object sender, EventArgs e) { ... }
        // private void cbLocLoaiTB_SelectedIndexChanged(object sender, EventArgs e) { ... }
        // private void txtTimKiemTB_TextChanged(object sender, EventArgs e) { ... }
    }
}