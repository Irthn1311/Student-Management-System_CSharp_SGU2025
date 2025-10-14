using Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class LopKhoi :UserControl
    {
        public LopKhoi()
        {
            InitializeComponent();

            // Gắn sự kiện
            this.Load += LopKhoi_Load;
        }

        private void LopKhoi_Load(object sender, EventArgs e)
        {
            if (dgvLop == null) return; // tránh lỗi khi chưa có DataGridView trong Designer

            // --- Thông tin thống kê 3 khối ---
            statCardKhoi10.SetData("Khối 10", "5 lớp", "200 học sinh");
            statCardKhoi11.SetData("Khối 11", "4 lớp", "180 học sinh");
            statCardKhoi12.SetData("Khối 12", "3 lớp", "150 học sinh");

            // --- Cấu hình & nạp dữ liệu ---
            SetupDataGridView();
            LoadData();

            // --- Gắn sự kiện ---
            dgvLop.CellPainting += dgvLop_CellPainting;
            dgvLop.CellClick += dgvLop_CellClick;
        }

        // =======================
        // 1️⃣ CẤU HÌNH DATAGRIDVIEW
        // =======================
        private void SetupDataGridView()
        {
            dgvLop.Columns.Clear();
            dgvLop.Rows.Clear();

            dgvLop.Columns.Add("MaLop", "Mã lớp");
            dgvLop.Columns.Add("TenLop", "Tên lớp");
            dgvLop.Columns.Add("Khoi", "Khối");
            dgvLop.Columns.Add("SiSo", "Sĩ số");
            dgvLop.Columns.Add("GVCN", "Giáo viên CN");
            dgvLop.Columns.Add("ThaoTac", "Thao tác");
            // Đặt lại chế độ co giãn cột
            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvLop.Columns["ThaoTac"].Width = 60; // hoặc 70 nếu icon lớn hơn
            dgvLop.Columns["ThaoTac"].Resizable = DataGridViewTriState.False;

            // Các cột còn lại có thể set Fill nếu muốn
            dgvLop.Columns["MaLop"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["TenLop"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["Khoi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["SiSo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["GVCN"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Style cho tiêu đề
            dgvLop.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvLop.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgvLop.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLop.EnableHeadersVisualStyles = false;

            // Style cho dữ liệu
            dgvLop.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvLop.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvLop.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvLop.RowTemplate.Height = 40;
            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLop.AllowUserToAddRows = false;
            dgvLop.ReadOnly = true;
        }

        // =======================
        // 2️⃣ NẠP DỮ LIỆU MẪU
        // =======================
        private void LoadData()
        {
            dgvLop.Rows.Add("10A1", "Lớp 10A1", "Khối 10", 42, "Nguyễn Thị Hoa");
            dgvLop.Rows.Add("10A2", "Lớp 10A2", "Khối 10", 40, "Trần Văn Nam");
            dgvLop.Rows.Add("11A1", "Lớp 11A1", "Khối 11", 38, "Phạm Văn Đức");
            dgvLop.Rows.Add("11A2", "Lớp 11A2", "Khối 11", 39, "Hoàng Thị Lan");
            dgvLop.Rows.Add("12A1", "Lớp 12A1", "Khối 12", 35, "Đỗ Thị Thu");
            dgvLop.Rows.Add("12A2", "Lớp 12A2", "Khối 12", 36, "Bùi Văn Toàn");
        }

        // =======================
        // 3️⃣ VẼ ICON TRONG CỘT THAO TÁC
        // =======================
        private void dgvLop_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvLop.Columns["ThaoTac"].Index)
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            // Lấy icon từ Resources
            Image editIcon = Properties.Resources.edit_icon;   // ✏️
            Image deleteIcon = Properties.Resources.delete_icon; // 🗑️

            int iconSize = 20; // Kích thước icon nhỏ hơn
            int spacing = 10;  // Khoảng cách giữa 2 icon
            int totalWidth = iconSize * 2 + spacing;

            // Tính toán vị trí để căn giữa cell
            int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
            int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

            // Vẽ icon chỉnh sửa & xóa
            e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize));
            e.Graphics.DrawImage(deleteIcon, new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize));

            e.Handled = true;
        }

        // =======================
        // 4️⃣ XỬ LÝ CLICK ICON
        // =======================
        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvLop.Columns["ThaoTac"].Index)
                return;

            // Lấy vị trí click trong cell
            Point clickPoint = dgvLop.PointToClient(Cursor.Position);
            Rectangle cellRect = dgvLop.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            int iconSize = 18;
            int spacing = 10;
            int totalWidth = iconSize * 2 + spacing;
            int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;

            string maLop = dgvLop.Rows[e.RowIndex].Cells["MaLop"].Value.ToString();

            // Xác định click vào icon nào
            if (clickPoint.X >= startX && clickPoint.X <= startX + iconSize)
            {
                SuaLopHoc frm = new SuaLopHoc();
                frm.StartPosition = FormStartPosition.CenterParent; // 🔹 hiện giữa form cha
                frm.ShowDialog();
                // TODO: mở form chỉnh sửa
            }
            else if (clickPoint.X >= startX + iconSize + spacing && clickPoint.X <= startX + iconSize * 2 + spacing)
            {
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc muốn xóa lớp {maLop}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Yes)
                {
                    dgvLop.Rows.RemoveAt(e.RowIndex);
                }
            }
        }


        private void statCardKhoi10_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ThemLopHoc frm = new ThemLopHoc();
            frm.StartPosition = FormStartPosition.CenterParent; // 🔹 hiện giữa form cha
            frm.ShowDialog();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
