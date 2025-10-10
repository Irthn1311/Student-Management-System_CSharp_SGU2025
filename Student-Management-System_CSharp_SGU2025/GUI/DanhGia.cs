using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class DanhGia : UserControl
    {
        public DanhGia()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            SetupKhenThuongTable();
            SetupKyLuatTable();

            tbKhenThuong.Visible = true;
            tbKyLuat.Visible = false;

            btnAddKhen.Visible = true;
            btnAddKyLuat.Visible = false;

            btnKhenThuong.FillColor = Color.FromArgb(32, 136, 225); // Khen thưởng active
            btnKhenThuong.ForeColor = Color.White;

            btnKyLuat.FillColor = Color.White; // Kỷ luật inactive
            btnKyLuat.ForeColor = Color.Black;

            // Thẻ 1: Tổng khen thưởng
            thongKeCard1.TieuDe1 = "Tổng khen thưởng";
            thongKeCard1.TieuDe2 = "142";
            thongKeCard1.TieuDe3 = "Năm học này";
            thongKeCard1.BackColor = Color.FromArgb(52, 199, 89); // Xanh lá

            // Thẻ 2: Cấp trường
            thongKeCard2.TieuDe1 = "Cấp trường";
            thongKeCard2.TieuDe2 = "98";
            thongKeCard2.TieuDe3 = "69% tổng số";
            thongKeCard2.BackColor = Color.FromArgb(0, 122, 255); // Xanh dương

            // Thẻ 3: Cấp tỉnh
            thongKeCard3.TieuDe1 = "Cấp tỉnh";
            thongKeCard3.TieuDe2 = "32";
            thongKeCard3.TieuDe3 = "23% tổng số";
            thongKeCard3.BackColor = Color.FromArgb(255, 149, 0); // Cam

            // Thẻ 4: Vi phạm kỷ luật
            thongKeCard4.TieuDe1 = "Vi phạm kỷ luật";
            thongKeCard4.TieuDe2 = "28";
            thongKeCard4.TieuDe3 = "2.2% học sinh";
            thongKeCard4.BackColor = Color.FromArgb(255, 59, 48); // Đỏ
        }

        // 🌸 Hàm thiết kế giao diện cho bảng khen thưởng
        private void SetupKhenThuongTable()
        {

            // 🔹 Xóa dữ liệu và cột cũ
            tbKhenThuong.Rows.Clear();
            tbKhenThuong.Columns.Clear();

            // 🔹 Thêm cột
            tbKhenThuong.Columns.Add("hoTen", "Họ và tên");
            tbKhenThuong.Columns.Add("thanhTich", "Thành tích");
            tbKhenThuong.Columns.Add("capKhen", "Cấp khen");
            tbKhenThuong.Columns.Add("ngay", "Ngày");
            tbKhenThuong.Columns.Add("thaoTac", "Thao tác");
            tbKhenThuong.CellPainting += TbKhenThuong_CellPainting;
            tbKhenThuong.CellClick += TbKhenThuong_CellClick;

            // 🎨 Thiết lập style tổng thể

            tbKhenThuong.ThemeStyle.BackColor = Color.White;
            tbKhenThuong.BackgroundColor = Color.White;
            tbKhenThuong.BorderStyle = BorderStyle.None;
            tbKhenThuong.CellBorderStyle = DataGridViewCellBorderStyle.None;
            tbKhenThuong.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            tbKhenThuong.RowHeadersVisible = false;
            tbKhenThuong.GridColor = Color.FromArgb(230, 230, 230);
            tbKhenThuong.EnableHeadersVisualStyles = false;

            // 🔹 Header
            tbKhenThuong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249,250,252);
            tbKhenThuong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tbKhenThuong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tbKhenThuong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.ColumnHeadersHeight = 40;

            // 🔹 Dòng dữ liệu
            tbKhenThuong.DefaultCellStyle.BackColor = Color.White;
            tbKhenThuong.DefaultCellStyle.ForeColor = Color.Black;
            tbKhenThuong.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            tbKhenThuong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            tbKhenThuong.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 🔹 Padding nhẹ giữa các ô
            tbKhenThuong.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            tbKhenThuong.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            tbKhenThuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 🔹 Căn chỉnh riêng cho từng cột
            tbKhenThuong.Columns["hoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["thanhTich"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["capKhen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["ngay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["thaoTac"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //btnEdit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //btnDelete.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            // 🔹 Thêm dữ liệu mẫu
            tbKhenThuong.Rows.Add("Nguyễn Văn An", "Giải Nhất Toán học cấp trường", "Cấp trường", "15/10/2024");
            tbKhenThuong.Rows.Add("Trần Thị Bình", "Học sinh Giỏi", "Cấp trường", "12/10/2024");
            tbKhenThuong.Rows.Add("Lê Hoàng Cường", "Giải Ba Tin học", "Cấp tỉnh", "10/10/2024");
            tbKhenThuong.Rows.Add("Nguyễn Tuấn Tài", "Giải nhất Tin học", "Cấp tỉnh", "19/1/2024");




            // 🔹 Bo góc nhẹ cho bảng (chỉ hiển thị đẹp nếu chứa trong panel)
            tbKhenThuong.ThemeStyle.RowsStyle.BackColor = Color.White;
            tbKhenThuong.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            tbKhenThuong.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            tbKhenThuong.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            tbKhenThuong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tbKhenThuong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 250, 252);
            tbKhenThuong.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // 🔹 Đảm bảo màu header không đổi khi click
            foreach (DataGridViewColumn col in tbKhenThuong.Columns)
            {
                col.HeaderCell.Style.SelectionBackColor = Color.FromArgb(249, 250, 252);
                col.HeaderCell.Style.SelectionForeColor = Color.Black;
            }
            tbKhenThuong.AllowUserToAddRows = false;
           tbKhenThuong.ReadOnly = true;
           tbKhenThuong.AllowUserToDeleteRows = false;
           tbKhenThuong.AllowUserToResizeColumns = false;
           tbKhenThuong.AllowUserToResizeRows = false;
           tbKhenThuong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           tbKhenThuong.MultiSelect = false;

        }

        // 🌸 Hàm thiết kế giao diện cho bảng kỷ luật
        private void SetupKyLuatTable()
        {
            // 🔹 Xóa dữ liệu và cột cũ
            tbKyLuat.Rows.Clear();
            tbKyLuat.Columns.Clear();

            // 🔹 Thêm cột
            tbKyLuat.Columns.Add("hocSinh", "Học sinh");
            tbKyLuat.Columns.Add("viPham", "Vi phạm");
            tbKyLuat.Columns.Add("xuLy", "Xử lý");
            tbKyLuat.Columns.Add("nguoiDuyet", "Người duyệt");
            tbKyLuat.Columns.Add("ngayKL", "Ngày");
            tbKyLuat.Columns.Add("thaoTacKL", "Thao tác");

            DataGridViewImageColumn btnEditKL = new DataGridViewImageColumn();
            btnEditKL.Name = "suaKL";
            btnEditKL.HeaderText = "Sửa";
            btnEditKL.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"..\..\Images\repair.png"));
            btnEditKL.ImageLayout = DataGridViewImageCellLayout.Zoom;
            tbKyLuat.Columns.Add(btnEditKL);

            DataGridViewImageColumn btnDeleteKL = new DataGridViewImageColumn();
            btnDeleteKL.Name = "xoaKL";
            btnDeleteKL.HeaderText = "Xóa";
            btnDeleteKL.Image = Image.FromFile(Path.Combine(Application.StartupPath, @"..\..\Images\bin.png"));
            btnDeleteKL.ImageLayout = DataGridViewImageCellLayout.Zoom;
            tbKyLuat.Columns.Add(btnDeleteKL);

            // 🎨 Thiết lập style tổng thể
            tbKyLuat.ThemeStyle.BackColor = Color.White;
            tbKyLuat.BackgroundColor = Color.White;
            tbKyLuat.BorderStyle = BorderStyle.None;
            tbKyLuat.CellBorderStyle = DataGridViewCellBorderStyle.None;
            tbKyLuat.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            tbKyLuat.RowHeadersVisible = false;
            tbKyLuat.GridColor = Color.FromArgb(230, 230, 230);
            tbKyLuat.EnableHeadersVisualStyles = false;

            // 🔹 Header
            tbKyLuat.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 252);
            tbKyLuat.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tbKyLuat.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tbKyLuat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.ColumnHeadersHeight = 40;

            // 🔹 Dòng dữ liệu
            tbKyLuat.DefaultCellStyle.BackColor = Color.White;
            tbKyLuat.DefaultCellStyle.ForeColor = Color.Black;
            tbKyLuat.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            tbKyLuat.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            tbKyLuat.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 🔹 Padding nhẹ giữa các ô
            tbKyLuat.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            tbKyLuat.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            tbKyLuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 🔹 Căn chỉnh riêng cho từng cột
            tbKyLuat.Columns["hocSinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.Columns["viPham"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.Columns["xuLy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.Columns["nguoiDuyet"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.Columns["ngayKL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //tbKyLuat.Columns["thaoTacKL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            btnEditKL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            btnDeleteKL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            // 🔹 Thêm dữ liệu mẫu
            tbKyLuat.Rows.Add("Nguyễn Văn B", "Đi học muộn 3 lần", "Nhắc nhở", "Cô Lan", "18/10/2024");
            tbKyLuat.Rows.Add("Trần Thị C", "Không làm bài tập", "Cảnh cáo", "Thầy Hùng", "16/10/2024");
            tbKyLuat.Rows.Add("Lê Văn D", "Gây gổ với bạn", "Khiển trách", "Hiệu trưởng", "14/10/2024");


            // 🔹 Bo góc nhẹ cho bảng
            tbKyLuat.ThemeStyle.RowsStyle.BackColor = Color.White;
            tbKyLuat.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            tbKyLuat.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            tbKyLuat.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            // 🔹 Tắt hiệu ứng highlight header khi chọn dòng
            tbKyLuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tbKyLuat.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 250, 252);
            tbKyLuat.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // 🔹 Đảm bảo màu header không đổi khi click
            foreach (DataGridViewColumn col in tbKyLuat.Columns)
            {
                col.HeaderCell.Style.SelectionBackColor = Color.FromArgb(249, 250, 252);
                col.HeaderCell.Style.SelectionForeColor = Color.Black;
            }
           tbKyLuat.ReadOnly = true;
           tbKyLuat.AllowUserToAddRows = false;
           tbKyLuat.AllowUserToDeleteRows = false;
           tbKyLuat.AllowUserToResizeColumns = false;
           tbKyLuat.AllowUserToResizeRows = false;
           tbKyLuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           tbKyLuat.MultiSelect = false;

        }

        private void TbKhenThuong_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbKhenThuong.Columns["thaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                int iconSize = 18;
                int padding = 4;

                int xEdit = e.CellBounds.Left + padding;
                int xDelete = e.CellBounds.Left + iconSize + 3 * padding;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Image editIcon = Image.FromFile(Path.Combine(Application.StartupPath, @"..\..\Images\repair.png"));
                Image deleteIcon = Image.FromFile(Path.Combine(Application.StartupPath, @"..\..\Images\bin.png"));

                e.Graphics.DrawImage(editIcon, new Rectangle(xEdit, y, iconSize, iconSize));
                e.Graphics.DrawImage(deleteIcon, new Rectangle(xDelete, y, iconSize, iconSize));

                e.Handled = true;
            }
        }

        private void TbKhenThuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbKhenThuong.Columns["thaoTac"].Index)
            {
                var cell = tbKhenThuong.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = tbKhenThuong.PointToClient(Cursor.Position).X - cell.X;

                int iconSize = 18;
                int padding = 4;
                int editRight = padding + iconSize;
                int deleteLeft = editRight + 3 * padding;

                if (x < editRight)
                {
                    MessageBox.Show($"Sửa dòng: {e.RowIndex + 1}");
                }
                else if (x > deleteLeft && x < deleteLeft + iconSize)
                {
                    MessageBox.Show($"Xóa dòng: {e.RowIndex + 1}");
                }
            }
        }


        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void thongKeCard1_Load(object sender, EventArgs e)
        {

        }

        private void thongKeCard1_Load_1(object sender, EventArgs e)
        {

        }

        private void thongKeCard2_Load(object sender, EventArgs e)
        {

        }

        private void tbKhenThuong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void thongKeCard3_Load(object sender, EventArgs e)
        {

        }

        private void thongKeCard4_Load(object sender, EventArgs e)
        {

        }

        private void tbKyLuat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            tbKhenThuong.Visible = true;
            tbKyLuat.Visible = false;
            btnAddKhen.Visible = true;
            btnAddKyLuat.Visible = false;

            btnKhenThuong.FillColor = Color.FromArgb(32, 136, 225);
            btnKhenThuong.ForeColor = Color.White;

            btnKyLuat.FillColor = Color.White;
            btnKyLuat.ForeColor = Color.Black;
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            tbKhenThuong.Visible = false;
            tbKyLuat.Visible = true;

            btnAddKhen.Visible = false;
            btnAddKyLuat.Visible = true;

            btnKyLuat.FillColor = Color.FromArgb(32, 136, 225);
            btnKyLuat.ForeColor = Color.White;

            btnKhenThuong.FillColor = Color.White;
            btnKhenThuong.ForeColor = Color.Black;
        }

        private void btnAddKyLuat_Click(object sender, EventArgs e)
        {

        }

        private void btnAddKhen_Click(object sender, EventArgs e)
        {

        }
    }
}
