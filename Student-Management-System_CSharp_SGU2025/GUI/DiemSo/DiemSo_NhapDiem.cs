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
    public partial class DiemSo_NhapDiem : UserControl
    {

        // Khai báo màu sắc
        private Color selectedColor = Color.FromArgb(33, 150, 243); // Màu xanh dương
        private Color normalColor = Color.White;
        private Color selectedTextColor = Color.White;
        private Color normalTextColor = Color.Black;
        private Color borderColor = Color.FromArgb(224, 224, 224);

        // Check khóa điểm 
        private bool isLocked = true;
        public DiemSo_NhapDiem()
        {
            InitializeComponent();
            // Không cho phép chỉnh sửa cột Trung Bình
            tableNhapDiem.Columns[5].ReadOnly = true;

        }

        // Hàm highlight button và reset cái cũ
        private void HighlightButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            // Reset tất cả buttons về trạng thái bình thường
            btnNhapDiem.FillColor = normalColor;
            btnNhapDiem.ForeColor = normalTextColor;
            btnNhapDiem.BorderColor = borderColor;
            btnNhapDiem.BorderThickness = 1;

            btnXemBangDiem.FillColor = normalColor;
            btnXemBangDiem.ForeColor = normalTextColor;
            btnXemBangDiem.BorderColor = borderColor;
            btnXemBangDiem.BorderThickness = 1;

            // Đổi màu button được chọn
            activeButton.FillColor = selectedColor;
            activeButton.ForeColor = selectedTextColor;
            activeButton.BorderColor = selectedColor;
        }

        // Hàm hiển thị bảng Nhập điểm
        private void ShowNhapDiem()
        {
            // Ẩn bảng Xem điểm
            tableXemBangDiem.Visible = false;

            // Hiển thị bảng Nhập điểm
            tableNhapDiem.Visible = true;

            // Ẩn nút Xuất Excel
            btnXuatExcel.Visible = false;

            // Hiện nút Khóa điểm và Lưu điểm
            btnKhoaDiem.Visible = true;
            btnLuuDiem.Visible = true;
        }

        // Hàm hiển thị bảng Xem điểm
        private void ShowXemBangDiem()
        {
            // Ẩn bảng Nhập điểm
            tableNhapDiem.Visible = false;

            // Hiển thị bảng Xem điểm
            tableXemBangDiem.Visible = true;

            // Hiện nút Xuất Excel
            btnXuatExcel.Visible = true;

            // Ẩn nút Khóa điểm và Lưu điểm
            btnKhoaDiem.Visible = false;
            btnLuuDiem.Visible = false;
        }

        // Hàm khởi tạo buttons
        private void InitializeButtons()
        {
            // Thiết lập style cho button Nhập điểm
            btnNhapDiem.BorderThickness = 1;
            btnNhapDiem.BorderColor = borderColor;
            btnNhapDiem.FillColor = normalColor;
            btnNhapDiem.ForeColor = normalTextColor;
            btnNhapDiem.Cursor = Cursors.Hand;

            // Thiết lập style cho button Xem bảng điểm
            btnXemBangDiem.BorderThickness = 1;
            btnXemBangDiem.BorderColor = borderColor;
            btnXemBangDiem.FillColor = normalColor;
            btnXemBangDiem.ForeColor = normalTextColor;
            btnXemBangDiem.Cursor = Cursors.Hand;

            // Mặc định hiển thị Nhập điểm
            HighlightButton(btnNhapDiem);
            ShowNhapDiem();
        }

        private void DiemSo_NhapDiem_Load(object sender, EventArgs e)
        {

            // Khởi tạo buttons
            InitializeButtons();

            // Trang trí tableNhapDiem
            ConfigureTableNhapDiem();

            // Trang trí tableXemBangDiem
            ConfigureTableXemBangDiem();

            // chèn dữ liệu mẫu vào Header
            headerQuanLiNhapDiem.lbHeader.Text = "Điểm số";
            headerQuanLiNhapDiem.lbGhiChu.Text = "Trang chủ / Điểm số";
            headerQuanLiNhapDiem.lbTenDangNhap.Text = "Nguyễn Văn A";
            headerQuanLiNhapDiem.lbVaiTro.Text = "Giáo vụ";

            // chèn dữ liệu mẫu vào các thẻ thống kê
            statCardDiemTrungBinh.lbCardTitle.Text = "Điểm trung bình";
            statCardDiemTrungBinh.lbCardValue.Text = "7.8";
            statCardDiemTrungBinh.lbCardNote.Text = "+0.3 so với kì trước";

            statCardDiemCaoNhat.lbCardTitle.Text = "Điểm cao nhất";
            statCardDiemCaoNhat.lbCardValue.Text = "9.5";
            statCardDiemCaoNhat.lbCardNote.Text = "Nguyễn Thị X";

            statCardDiemThapNhat.lbCardTitle.Text = "Điểm thấp nhất";
            statCardDiemThapNhat.lbCardValue.Text = "5.5";
            statCardDiemThapNhat.lbCardNote.Text = "Trần Văn Y";

            statCardDaNhap.lbCardTitle.Text = "Đã Nhập";
            statCardDaNhap.lbCardValue.Text = "38 / 42";
            statCardDaNhap.lbCardNote.Text = "4 học sinh chưa có điểm";

            statCardDiemTrungBinh.lbCardNote.ForeColor = Color.FromArgb(22, 163, 74);
            statCardDiemCaoNhat.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardDiemThapNhat.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardDaNhap.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardDaNhap.lbCardNote.ForeColor = Color.FromArgb(220, 38, 38);

            // Thêm dữ liệu mẫu vào tableNhapDiem
            LoadSampleDataNhapDiem();

            // Thêm dữ liệu mẫu vào tableXemBangDiem
            LoadSampleDataXemBangDiem();

            tableNhapDiem.ReadOnly = false;

        }

        // Hàm cấu hình tableNhapDiem
        private void ConfigureTableNhapDiem()
        {
            // Cấu hình header
            tableNhapDiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            tableNhapDiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableNhapDiem.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tableNhapDiem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableNhapDiem.ColumnHeadersHeight = 45;
            tableNhapDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cấu hình cells
            tableNhapDiem.DefaultCellStyle.BackColor = Color.White;
            tableNhapDiem.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            tableNhapDiem.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tableNhapDiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableNhapDiem.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            tableNhapDiem.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Cấu hình rows
            tableNhapDiem.RowTemplate.Height = 50;
            tableNhapDiem.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Cấu hình borders
            tableNhapDiem.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableNhapDiem.GridColor = Color.FromArgb(229, 231, 235);
            tableNhapDiem.BorderStyle = BorderStyle.None;

            // Cấu hình columns width
            tableNhapDiem.Columns[0].Width = 80;  // STT
            tableNhapDiem.Columns[1].Width = 250; // Họ và Tên
            tableNhapDiem.Columns[2].Width = 150; // Điểm TX
            tableNhapDiem.Columns[3].Width = 150; // Giữa Kì
            tableNhapDiem.Columns[4].Width = 150; // Cuối Kì
            tableNhapDiem.Columns[5].Width = 150; // Trung Bình

            // Căn giữa các cột điểm
            for (int i = 2; i <= 5; i++)
            {
                tableNhapDiem.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Loại bỏ selection
            tableNhapDiem.EnableHeadersVisualStyles = false;

            // Ngăn đổi màu tiêu đề khi chọn
            tableNhapDiem.EnableHeadersVisualStyles = false;
            tableNhapDiem.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableNhapDiem.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(75, 85, 99);



        }

        // Hàm cấu hình tableXemBangDiem
        private void ConfigureTableXemBangDiem()
        {
            // Cấu hình header
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableXemBangDiem.ColumnHeadersHeight = 45;
            tableXemBangDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cấu hình cells
            tableXemBangDiem.DefaultCellStyle.BackColor = Color.White;
            tableXemBangDiem.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            tableXemBangDiem.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tableXemBangDiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableXemBangDiem.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            tableXemBangDiem.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Cấu hình rows
            tableXemBangDiem.RowTemplate.Height = 50;
            tableXemBangDiem.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Cấu hình borders
            tableXemBangDiem.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableXemBangDiem.GridColor = Color.FromArgb(229, 231, 235);
            tableXemBangDiem.BorderStyle = BorderStyle.None;

            // Cấu hình columns width
            tableXemBangDiem.Columns[0].Width = 200; // Học sinh
            tableXemBangDiem.Columns[1].Width = 120; // Toán
            tableXemBangDiem.Columns[2].Width = 120; // Văn
            tableXemBangDiem.Columns[3].Width = 120; // Anh
            tableXemBangDiem.Columns[4].Width = 120; // Lý
            tableXemBangDiem.Columns[5].Width = 120; // Hóa
            tableXemBangDiem.Columns[6].Width = 140; // TB Chung

            // Căn giữa các cột điểm (trừ cột học sinh)
            for (int i = 1; i <= 6; i++)
            {
                tableXemBangDiem.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Loại bỏ selection
            tableXemBangDiem.EnableHeadersVisualStyles = false;

            // Không cho chỉnh sửa
            tableXemBangDiem.ReadOnly = true;

            tableXemBangDiem.EnableHeadersVisualStyles = false;
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(75, 85, 99);

        }

        // Hàm thêm dữ liệu mẫu vào tableNhapDiem
        private void LoadSampleDataNhapDiem()
        {
            tableNhapDiem.Rows.Clear();

            tableNhapDiem.Rows.Add("1", "Nguyễn Văn An", "8.5", "7.5", "8", "8.0");
            tableNhapDiem.Rows.Add("2", "Trần Thị Bình", "9", "8.5", "9", "8.8");
            tableNhapDiem.Rows.Add("3", "Lê Hoàng Cường", "7", "6.5", "7", "6.8");
            tableNhapDiem.Rows.Add("4", "Phạm Thị Dung", "8", "8", "8.5", "8.2");
            tableNhapDiem.Rows.Add("5", "Hoàng Văn Em", "6.5", "7", "6.5", "6.7");
            tableNhapDiem.Rows.Add("6", "Vũ Thị Hà", "5.5", "6", "6", "5.8");
            tableNhapDiem.Rows.Add("7", "Đỗ Văn Khoa", "8", "7.5", "8", "7.8");
            tableNhapDiem.Rows.Add("8", "Bùi Thị Lan", "9.5", "9", "9.5", "9.3");
            tableNhapDiem.Rows.Add("9", "Trịnh Văn Minh", "7.5", "7", "7.5", "7.3");
            tableNhapDiem.Rows.Add("10", "Ngô Thị Nga", "8.5", "8", "8.5", "8.3");

            // Đổi màu cột Trung Bình
            foreach (DataGridViewRow row in tableNhapDiem.Rows)
            {
                if (row.Cells[5].Value != null)
                {
                    double score = double.Parse(row.Cells[5].Value.ToString());
                    if (score >= 8.0)
                    {
                        row.Cells[5].Style.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                        row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                    else if (score >= 6.5)
                    {
                        row.Cells[5].Style.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương
                        row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                    else
                    {
                        row.Cells[5].Style.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                        row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                }
            }
        }

        // Hàm thêm dữ liệu mẫu vào tableXemBangDiem
        private void LoadSampleDataXemBangDiem()
        {
            tableXemBangDiem.Rows.Clear();

            tableXemBangDiem.Rows.Add("Nguyễn Văn An", "8.0", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Trần Thị Bình", "8.8", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Lê Hoàng Cường", "6.8", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Phạm Thị Dung", "8.2", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Hoàng Văn Em", "6.7", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Vũ Thị Hà", "5.8", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Đỗ Văn Khoa", "7.8", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Bùi Thị Lan", "9.3", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Trịnh Văn Minh", "7.3", "8.2", "7.8", "7.5", "8.0", "7.9");
            tableXemBangDiem.Rows.Add("Ngô Thị Nga", "8.3", "8.2", "7.8", "7.5", "8.0", "7.9");

            // Đổi màu cột TB Chung
            foreach (DataGridViewRow row in tableXemBangDiem.Rows)
            {
                if (row.Cells[6].Value != null)
                {
                    double score = double.Parse(row.Cells[6].Value.ToString());
                    if (score >= 8.0)
                    {
                        row.Cells[6].Style.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                    }
                    else if (score >= 6.5)
                    {
                        row.Cells[6].Style.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương
                    }
                    else
                    {
                        row.Cells[6].Style.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                    }
                    row.Cells[6].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
            }
        }

        private void headerQuanLiNhapDiem_Load(object sender, EventArgs e)
        {

        }

        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableNhapDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void btnLuuDiem_Click(object sender, EventArgs e)
        {

        }

        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            // Highlight button Nhập điểm
            HighlightButton(btnNhapDiem);
            // Hiển thị bảng Nhập điểm
            ShowNhapDiem();
        }

        private void btnXemBangDiem_Click(object sender, EventArgs e)
        {
            // Highlight button Xem bảng điểm
            HighlightButton(btnXemBangDiem);
            // Hiển thị bảng Xem điểm
            ShowXemBangDiem();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnKhoaDiem_Click(object sender, EventArgs e)
        {
            if (isLocked)
            {
                btnKhoaDiem.Image = Properties.Resources.unlock;
                btnKhoaDiem.Text = "Mở khóa điểm";
                isLocked = false;
                tableNhapDiem.ReadOnly = true; // Cho phép chỉnh sửa

            }
            else
            {
                btnKhoaDiem.Image = Properties.Resources.padlock;
                btnKhoaDiem.Text = "Khóa điểm";
                isLocked = true;
                tableNhapDiem.ReadOnly = false; // Khóa chỉnh sửa
                // Không cho phép chỉnh sửa cột Trung Bình
                tableNhapDiem.Columns[5].ReadOnly = true;
            }
        }

        private void tableXemBangDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
