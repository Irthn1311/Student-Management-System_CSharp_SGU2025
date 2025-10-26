using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmMonHoc : UserControl
    {
        public FrmMonHoc()
        {
            InitializeComponent();
        }

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            // ===================================
            // 1️⃣ CẤU HÌNH DATAGRIDVIEW
            // ===================================

            // --- Cài đặt cơ bản ---
            dgvMonHoc.AutoGenerateColumns = false;
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.RowTemplate.Height = 40; // Chiều cao mỗi dòng dữ liệu

            // --- Style cho tiêu đề cột ---
            dgvMonHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvMonHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 136, 229); // <-- Thay đổi màu nền tiêu đề
            dgvMonHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMonHoc.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 136, 229); // <-- Ngăn đổi màu khi click
            dgvMonHoc.ColumnHeadersHeight = 40; // <-- Tăng chiều cao tiêu đề
            dgvMonHoc.EnableHeadersVisualStyles = false;

            // --- Style cho các dòng dữ liệu ---
            dgvMonHoc.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvMonHoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvMonHoc.DefaultCellStyle.SelectionForeColor = Color.Black;

            // ===================================
            // 2️⃣ NẠP DỮ LIỆU MẪU
            // ===================================
            dgvMonHoc.Rows.Clear(); // Xóa dữ liệu cũ trước khi nạp mới
            dgvMonHoc.Rows.Add("TOAN", "Toán học", "5 tiết", "Môn chính");
            dgvMonHoc.Rows.Add("VAN", "Ngữ văn", "5 tiết", "Môn chính");
            dgvMonHoc.Rows.Add("LY", "Vật lý", "3 tiết", "Tự nhiên");
            dgvMonHoc.Rows.Add("HOA", "Hóa học", "3 tiết", "Tự nhiên");
            dgvMonHoc.Rows.Add("SINH", "Sinh học", "3 tiết", "Tự nhiên");
            dgvMonHoc.Rows.Add("SU", "Lịch sử", "2 tiết", "Xã hội");
            dgvMonHoc.Rows.Add("DIA", "Địa lý", "2 tiết", "Xã hội");
            dgvMonHoc.Rows.Add("GDCD", "Giáo dục công dân", "1 tiết", "Xã hội");
            dgvMonHoc.Rows.Add("TIN", "Tin học", "2 tiết", "Kỹ năng khác");
            dgvMonHoc.Rows.Add("TD", "Thể dục", "2 tiết", "Kỹ năng khác");
            dgvMonHoc.Rows.Add("QPAN", "Giáo dục Quốc phòng", "2 tiết", "Kỹ năng khác");
            dgvMonHoc.Rows.Add("NHAC", "Âm nhạc", "1 tiết", "Nghệ thuật");
            dgvMonHoc.Rows.Add("MT", "Mỹ thuật", "1 tiết", "Nghệ thuật");

            // ===================================
            // 3️⃣ GẮN SỰ KIỆN
            // ===================================
            dgvMonHoc.CellPainting += dgvMonHoc_CellPainting;
            dgvMonHoc.CellClick += dgvMonHoc_CellClick;

            // ===================================
            // 4️⃣ THIẾT LẬP CÁC STAT CARD
            // ===================================
            // Card 1
            statcardMonHoc1.SetData("4", "Môn chính", "Toán,Văn,Anh");
            statcardMonHoc1.PanelBackgroundColor = Color.FromArgb(219, 234, 254);
            statcardMonHoc1.SoLuongForeColor = Color.FromArgb(30, 136, 229);

            // Card 2
            statcardMonHoc2.SetData("3", "Khoa học tự nhiên", "Lý,Hóa,Sinh");
            statcardMonHoc2.PanelBackgroundColor = Color.FromArgb(220, 252, 231);
            statcardMonHoc2.SoLuongForeColor = Color.FromArgb(22, 163, 74);

            // Card 3
            statcardMonHoc3.SetData("3", "Khoa học xã hội", "GDCD,Sử,Địa");
            statcardMonHoc3.PanelBackgroundColor = Color.FromArgb(255, 237, 213);
            statcardMonHoc3.SoLuongForeColor = Color.FromArgb(234, 88, 12);

            // Card 4
            statcardMonHoc4.SetData("3", "Kỹ năng khác", "GDCD,TD,Qp");
            statcardMonHoc4.PanelBackgroundColor = Color.FromArgb(243, 243, 255);
            statcardMonHoc4.SoLuongForeColor = Color.FromArgb(147, 51, 234);
        }

        private void dgvMonHoc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Kiểm tra vẽ ô trong cột thao tác
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvMonHoc.Columns["TuyChinh"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Lấy icon từ Resources
                Image editIcon = Properties.Resources.edit_icon;   // ✏️
                Image deleteIcon = Properties.Resources.delete_icon; // 🗑️

                int iconSize = 20;
                int spacing = 10;
                int totalWidth = iconSize * 2 + spacing;

                // Căn giữa 2 icon trong ô
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                // Vẽ 2 icon
                e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize));
                e.Graphics.DrawImage(deleteIcon, new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize));

                e.Handled = true;
            }
        }

        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua click tiêu đề
            if (e.RowIndex < 0 || e.ColumnIndex != dgvMonHoc.Columns["TuyChinh"].Index)
                return;

            // Xác định vị trí click
            Point clickPoint = dgvMonHoc.PointToClient(Cursor.Position);
            Rectangle cellRect = dgvMonHoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            int iconSize = 20;
            int spacing = 10;
            int totalWidth = iconSize * 2 + spacing;
            int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;

            int xEdit = startX;
            int xDelete = startX + iconSize + spacing;

            string maMon = dgvMonHoc.Rows[e.RowIndex].Cells["MaMon"].Value.ToString();

            // Xử lý click từng icon
            if (clickPoint.X >= xEdit && clickPoint.X <= xEdit + iconSize)
            {
                MessageBox.Show($"📝 Chỉnh sửa môn: {maMon}", "Sửa môn học");
                // TODO: Mở form chỉnh sửa
            }
            else if (clickPoint.X >= xDelete && clickPoint.X <= xDelete + iconSize)
            {
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc muốn xóa môn {maMon}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Yes)
                {
                    dgvMonHoc.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            // Tùy chọn xử lý nếu cần
        }

        private void statcardMonHoc2_Load(object sender, EventArgs e)
        {

        }

        private void statcardMonHoc1_Load(object sender, EventArgs e)
        {

        }

        private void panelMonHoc_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
