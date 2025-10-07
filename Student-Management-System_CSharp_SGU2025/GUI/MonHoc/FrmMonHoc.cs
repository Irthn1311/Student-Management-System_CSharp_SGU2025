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
            // ==== Cấu hình DataGridView ====
            dgvMonHoc.AutoGenerateColumns = false;
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.RowTemplate.Height = 40;

            // ==== Thêm dữ liệu mẫu ====
            dgvMonHoc.Rows.Add("TOAN", "Toán học", "5 tiết", "Môn chính");
            dgvMonHoc.Rows.Add("VAN", "Ngữ văn", "5 tiết", "Môn chính");
            dgvMonHoc.Rows.Add("LY", "Vật lý", "3 tiết", "Tự nhiên");

            // ==== Gắn sự kiện ====
            dgvMonHoc.CellPainting += dgvMonHoc_CellPainting;
            dgvMonHoc.CellClick += dgvMonHoc_CellClick;
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
    }
}
