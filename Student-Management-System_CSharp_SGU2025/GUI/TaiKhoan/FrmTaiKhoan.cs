using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmTaiKhoan : UserControl
    {
        public FrmTaiKhoan()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            SetupThongKeCards();
            SetupTaiKhoanTable();
            //tbTaiKhoan.CellClick += TbTaiKhoan_CellClick;

        }

        private void SetupThongKeCards()
        {
            // Card 1: Xanh dương
            statCardThongKeTaiKhoan1.TitleGhiChu = "Tổng tài khoản";
            statCardThongKeTaiKhoan1.TitleLietKe = "1,250";
            statCardThongKeTaiKhoan1.Icon = Properties.Resources.icons8_protect_white;
            statCardThongKeTaiKhoan1.IconFillColor = Color.FromArgb(147, 197, 253); // <-- Màu xanh dương nhạt
            statCardThongKeTaiKhoan1.PanelBackgroundColor = Color.FromArgb(59, 130, 246);

            // Card 2: Xanh lá
            statCardThongKeTaiKhoan2.TitleGhiChu = "Tài khoản hoạt động";
            statCardThongKeTaiKhoan2.TitleLietKe = "1,100";
            statCardThongKeTaiKhoan2.Icon = Properties.Resources.icons8_protect_white;
            statCardThongKeTaiKhoan2.IconFillColor = Color.FromArgb(134, 239, 172); // <-- Màu xanh lá nhạt
            statCardThongKeTaiKhoan2.PanelBackgroundColor = Color.FromArgb(34, 197, 94);

            // Card 3: Đỏ
            statCardThongKeTaiKhoan3.TitleGhiChu = "Tài khoản bị khóa";
            statCardThongKeTaiKhoan3.TitleLietKe = "150";
            statCardThongKeTaiKhoan3.Icon = Properties.Resources.icons8_lock_white;
            statCardThongKeTaiKhoan3.IconFillColor = Color.FromArgb(252, 165, 165); // <-- Màu đỏ nhạt
            statCardThongKeTaiKhoan3.PanelBackgroundColor = Color.FromArgb(239, 68, 68);

            // Card 4: Cam
            statCardThongKeTaiKhoan4.TitleGhiChu = "Tài khoản Admin";
            statCardThongKeTaiKhoan4.TitleLietKe = "5";
            statCardThongKeTaiKhoan4.Icon = Properties.Resources.icons8_protect_white;
            statCardThongKeTaiKhoan4.IconFillColor = Color.FromArgb(253, 186, 116); // <-- Màu cam nhạt
            statCardThongKeTaiKhoan4.PanelBackgroundColor = Color.FromArgb(249, 115, 22);
        }

        // 🌸 Hàm thiết kế giao diện cho bảng tài khoản
        private void SetupTaiKhoanTable()
        {
            tbTaiKhoan.Rows.Clear();
            tbTaiKhoan.Columns.Clear();

            // Các cột chữ
            tbTaiKhoan.Columns.Add("tenTaiKhoan", "Tên tài khoản");
            tbTaiKhoan.Columns.Add("vaiTro", "Vai trò");
            tbTaiKhoan.Columns.Add("trangThai", "Trạng thái");
            tbTaiKhoan.Columns.Add("lastLogin", "Đăng nhập gần nhất");

            tbTaiKhoan.Columns.Add("thaoTac", "Thao tác");

            // Gắn sự kiện vẽ và click
            tbTaiKhoan.CellPainting += TbTaiKhoan_CellPainting;
            tbTaiKhoan.CellClick += TbTaiKhoan_CellClick;

            tbTaiKhoan.EnableHeadersVisualStyles = false;
            tbTaiKhoan.BackgroundColor = Color.White;
            tbTaiKhoan.BorderStyle = BorderStyle.None;
            tbTaiKhoan.GridColor = Color.FromArgb(240, 240, 240);
            tbTaiKhoan.RowTemplate.Height = 48;
            tbTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tbTaiKhoan.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            tbTaiKhoan.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
            tbTaiKhoan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            tbTaiKhoan.DefaultCellStyle.SelectionForeColor = Color.Black;

            tbTaiKhoan.Columns["tenTaiKhoan"].DefaultCellStyle.Padding = new Padding(17, 0, 0, 0);

            // 🌸 Header style (theo Figma)
            tbTaiKhoan.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            tbTaiKhoan.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
            tbTaiKhoan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            tbTaiKhoan.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbTaiKhoan.ColumnHeadersHeight = 50;
            tbTaiKhoan.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            tbTaiKhoan.EnableHeadersVisualStyles = false;
            tbTaiKhoan.ColumnHeadersDefaultCellStyle.SelectionBackColor = tbTaiKhoan.ColumnHeadersDefaultCellStyle.BackColor;
            tbTaiKhoan.ColumnHeadersDefaultCellStyle.SelectionForeColor = tbTaiKhoan.ColumnHeadersDefaultCellStyle.ForeColor;

            // 🌼 Bo góc nhẹ (nếu đang dùng Guna2DataGridView)
            tbTaiKhoan.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tbTaiKhoan.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            tbTaiKhoan.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;

            // Dữ liệu mẫu
            tbTaiKhoan.Rows.Add("admin", "Admin", "Hoạt động", "05/10/2024 14:30");
            tbTaiKhoan.Rows.Add("giaovu01", "Giáo vụ", "Hoạt động", "05/10/2024 13:15");
            tbTaiKhoan.Rows.Add("gv.hoa", "Giáo viên", "Hoạt động", "05/10/2024 08:45");
            tbTaiKhoan.Rows.Add("gv.nam", "Giáo viên", "Hoạt động", "04/10/2024 16:20");
            tbTaiKhoan.Rows.Add("hs.an", "Học sinh", "Hoạt động", "05/10/2024 12:00");
            tbTaiKhoan.Rows.Add("hs.binh", "Học sinh", "Bị khóa", "02/10/2024 09:30");

            // 🎨 Màu chữ cho vai trò và trạng thái
            foreach (DataGridViewRow row in tbTaiKhoan.Rows)
            {
                string vaiTro = row.Cells["vaiTro"].Value?.ToString();
                if (vaiTro == "Admin") row.Cells["vaiTro"].Style.ForeColor = Color.FromArgb(147, 51, 234);
                else if (vaiTro == "Giáo vụ") row.Cells["vaiTro"].Style.ForeColor = Color.FromArgb(59, 130, 246);
                else if (vaiTro == "Giáo viên") row.Cells["vaiTro"].Style.ForeColor = Color.FromArgb(34, 197, 94);
                else if (vaiTro == "Học sinh") row.Cells["vaiTro"].Style.ForeColor = Color.FromArgb(249, 115, 22);

                string trangThai = row.Cells["trangThai"].Value?.ToString();
                if (trangThai == "Hoạt động") row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(34, 197, 94);
                else if (trangThai == "Bị khóa") row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(239, 68, 68);
            }

            tbTaiKhoan.AllowUserToAddRows = false;
            tbTaiKhoan.ReadOnly = true;
            tbTaiKhoan.AllowUserToAddRows = false;
            tbTaiKhoan.AllowUserToDeleteRows = false;
            tbTaiKhoan.AllowUserToResizeColumns = false;
            tbTaiKhoan.AllowUserToResizeRows = false;
            tbTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tbTaiKhoan.MultiSelect = false;


        }

        private void TbTaiKhoan_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbTaiKhoan.Columns["thaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                int iconSize = 16;
                int padding = 5;

                int xShield = e.CellBounds.Left + padding;
                int xLock = xShield + iconSize + 3 * padding;
                int xBin = xLock + iconSize + 3 * padding;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Image shield = Image.FromFile(@"..\..\Images\shield.png");
                Image lockIcon = Image.FromFile(@"..\..\Images\lock.png");
                Image bin = Image.FromFile(@"..\..\Images\bin.png");

                e.Graphics.DrawImage(shield, new Rectangle(xShield, y, iconSize, iconSize));
                e.Graphics.DrawImage(lockIcon, new Rectangle(xLock, y, iconSize, iconSize));
                e.Graphics.DrawImage(bin, new Rectangle(xBin, y, iconSize, iconSize));

                e.Handled = true;
            }
        }


        private void TbTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbTaiKhoan.Columns["thaoTac"].Index)
            {
                var cell = tbTaiKhoan.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = tbTaiKhoan.PointToClient(Cursor.Position).X - cell.X;

                int iconSize = 16;
                int padding = 5;
                int shieldRight = padding + iconSize;
                int lockLeft = shieldRight + 3 * padding;
                int lockRight = lockLeft + iconSize;
                int binLeft = lockRight + 3 * padding;

                string tenTK = tbTaiKhoan.Rows[e.RowIndex].Cells["tenTaiKhoan"].Value.ToString();

                if (x < shieldRight)
                {
                    MessageBox.Show($"Xem quyền của: {tenTK}");
                }
                else if (x > lockLeft && x < lockRight)
                {
                    MessageBox.Show($"Khóa/Mở tài khoản: {tenTK}");
                }
                else if (x > binLeft && x < binLeft + iconSize)
                {
                    MessageBox.Show($"Xóa tài khoản: {tenTK}");
                }
            }
        }

        private void thongKeTK1_Load(object sender, EventArgs e)
        {
        }

        private void thongKeTK2_Load(object sender, EventArgs e)
        {
        }

        private void thongKeTK3_Load(object sender, EventArgs e)
        {
        }

        private void thongKeTK4_Load(object sender, EventArgs e)
        {
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void tbTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnVaiTro_Click(object sender, EventArgs e)
        {
            frmPhanQuyen formPhanQuyen = new frmPhanQuyen();
            formPhanQuyen.StartPosition = FormStartPosition.CenterParent;
            formPhanQuyen.ShowDialog();
        }
    }
}