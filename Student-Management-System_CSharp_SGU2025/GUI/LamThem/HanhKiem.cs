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
    public partial class HanhKiem : UserControl
    {

        // Khai báo màu sắc
        private Color selectedColor = Color.FromArgb(33, 150, 243); // Màu xanh dương
        private Color normalColor = Color.White;
        private Color selectedTextColor = Color.White;
        private Color normalTextColor = Color.Black;
        private Color borderColor = Color.FromArgb(224, 224, 224);

        public HanhKiem()
        {
            InitializeComponent();
        }

        private void headerHanhKiem_Load(object sender, EventArgs e)
        {

        }

        private void HanhKiem_Load(object sender, EventArgs e)
        {

            // Trang trí tableNhapDiem
            ConfigureTableHanhKiem();

            // chèn dữ liệu mẫu vào Header
            headerHanhKiem.lbHeader.Text = "Hạnh kiểm";
            headerHanhKiem.lbGhiChu.Text = "Trang chủ / Hạnh kiểm";
            headerHanhKiem.lbTenDangNhap.Text = "Nguyễn Văn A";
            headerHanhKiem.lbVaiTro.Text = "Giáo vụ";

            // chèn dữ liệu mẫu vào các thẻ thống kê
            statCarHanhKiemTot.lbCardTitle.Text = "Hạnh kiểm tốt";
            statCarHanhKiemTot.lbCardValue.Text = "892";
            statCarHanhKiemTot.lbCardNote.Text = "71.5% học sinh";

            statCardHanhKiemKha.lbCardTitle.Text = "Hạnh kiểm khá";
            statCardHanhKiemKha.lbCardValue.Text = "278";
            statCardHanhKiemKha.lbCardNote.Text = "22.3% học sinh";

            statCardHanhKiemTrungBinh.lbCardTitle.Text = "Hạnh kiểm trung bình";
            statCardHanhKiemTrungBinh.lbCardValue.Text = "65";
            statCardHanhKiemTrungBinh.lbCardNote.Text = "5.2% học sinh";

            statCardHanhKiemYeu.lbCardTitle.Text = "Hạnh kiểm yếu";
            statCardHanhKiemYeu.lbCardValue.Text = "12";
            statCardHanhKiemYeu.lbCardNote.Text = "1% học sinh";

            statCardChuaDanhGiaHanhKiem.lbCardTitle.Text = "Chưa đánh giá";
            statCardChuaDanhGiaHanhKiem.lbCardValue.Text = "4 học sinh";
            statCardChuaDanhGiaHanhKiem.lbCardNote.Text = "0.3% học sinh";

            statCarHanhKiemTot.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardHanhKiemKha.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardHanhKiemTrungBinh.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardHanhKiemYeu.lbCardValue.ForeColor = Color.FromArgb(220, 38, 38);
            statCardChuaDanhGiaHanhKiem.lbCardValue.ForeColor = Color.FromArgb(158, 163, 255);
            statCardChuaDanhGiaHanhKiem.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statCarHanhKiemTot.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statCardHanhKiemYeu.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statCardHanhKiemKha.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statCardHanhKiemTrungBinh.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            // Thêm dữ liệu mẫu vào tableHanhKiem
            LoadSampleDataHanhKiem();

        }

        // Hàm cấu hình tableNhapDiem
        private void ConfigureTableHanhKiem()
        {
            // Cấu hình header
            tableHanhKiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            tableHanhKiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableHanhKiem.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tableHanhKiem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableHanhKiem.ColumnHeadersHeight = 40;
            tableHanhKiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cấu hình cells
            tableHanhKiem.DefaultCellStyle.BackColor = Color.White;
            tableHanhKiem.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            tableHanhKiem.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tableHanhKiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableHanhKiem.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            tableHanhKiem.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Cấu hình rows
            tableHanhKiem.RowTemplate.Height = 60;
            tableHanhKiem.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Cấu hình borders
            tableHanhKiem.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableHanhKiem.GridColor = Color.FromArgb(229, 231, 235);
            tableHanhKiem.BorderStyle = BorderStyle.None;

            // Cấu hình columns width
            tableHanhKiem.Columns[0].Width = 120;  // Mã Hạnh Kiểm
            tableHanhKiem.Columns[1].Width = 150; // Họ và Tên
            tableHanhKiem.Columns[2].Width = 70; // Lớp
            tableHanhKiem.Columns[3].Width = 80; // Học Kì
            tableHanhKiem.Columns[4].Width = 90; // Xếp Loại
            tableHanhKiem.Columns[5].Width = 250; // Nhận Xét

            // Căn giữa các cột điểm
            for (int i = 2; i <= 5; i++)
            {
                tableHanhKiem.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Loại bỏ selection
            tableHanhKiem.EnableHeadersVisualStyles = false;

            // Ngăn đổi màu tiêu đề khi chọn
            tableHanhKiem.EnableHeadersVisualStyles = false;
            tableHanhKiem.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableHanhKiem.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(75, 85, 99);

            foreach (DataGridViewColumn col in tableHanhKiem.Columns)
            {
                col.ReadOnly = true; // khóa hết
            }
            tableHanhKiem.Columns[4].ReadOnly = false; // Cho phép sửa Xếp Loại
            tableHanhKiem.Columns[5].ReadOnly = false; // Cho phép sửa Nhận Xét


        }

        // Hàm thêm dữ liệu mẫu vào tableHanhKiem
        private void LoadSampleDataHanhKiem()
        {
            tableHanhKiem.Rows.Clear();

            tableHanhKiem.Rows.Add("1", "Nguyễn Văn An", "9A1", "Học Kì I", "Tốt", "Chăm chỉ");
            tableHanhKiem.Rows.Add("2", "Trần Thị Bình", "9A2", "Học Kì I", "Khá", "Năng động");
            tableHanhKiem.Rows.Add("3", "Lê Văn Cường", "9A1", "Học Kì I", "Trung Bình", "Cần cố gắng");
            tableHanhKiem.Rows.Add("4", "Phạm Thị Dung", "9A3", "Học Kì I", "Yếu", "Thiếu tập trung");
            tableHanhKiem.Rows.Add("5", "Hoàng Văn Em", "9A2", "Học Kì I", "Tốt", "Gương mẫu");
            tableHanhKiem.Rows.Add("6", "Vũ Thị Hà", "9A1", "Học Kì I", "Khá", "Thân thiện");
            tableHanhKiem.Rows.Add("7", "Đỗ Văn Khoa", "9A3", "Học Kì I", "Trung Bình", "Cần cố gắng");
            tableHanhKiem.Rows.Add("8", "Ngô Thị Lan", "9A2", "Học Kì I", "Yếu", "Thiếu tập trung");
            tableHanhKiem.Rows.Add("9", "Bùi Văn Minh", "9A1", "Học Kì I", "Tốt", "Chăm chỉ");
            tableHanhKiem.Rows.Add("10", "Trịnh Thị Nga", "9A3", "Học Kì I", "Khá", "Năng động");
            tableHanhKiem.Rows.Add("11", "Phan Văn Quang", "9A2", "Học Kì I", "Trung Bình", "Cần cố gắng");

            // Đổi màu cột Xếp Loại dựa trên giá trị
            foreach (DataGridViewRow row in tableHanhKiem.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    string xepLoai = row.Cells[4].Value.ToString();
                    switch (xepLoai)
                    {
                        case "Tốt":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(22, 163, 74); // Màu xanh lá
                            break;
                        case "Khá":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(30, 136, 229); // Màu xanh dương
                            break;
                        case "Trung Bình":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(219, 39, 119); // Màu hồng
                            break;
                        case "Yếu":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(220, 38, 38); // Màu đỏ
                            break;
                        default:
                            row.Cells[4].Style.ForeColor = Color.Black; // Mặc định
                            break;
                    }
                    
                }
            }
        }

        private void statCardHanhKiemTrungBinh_Load(object sender, EventArgs e)
        {

        }

        private void btnLuuHanhKiem_Click(object sender, EventArgs e)
        {
            string[] validValues = { "Tốt", "Khá", "Trung Bình", "Yếu" };
            bool isValid = true;

            // Duyệt từng dòng trong DataGridView
            foreach (DataGridViewRow row in tableHanhKiem.Rows)
            {
                if (row.IsNewRow) continue; // bỏ qua dòng trống cuối

                string xepLoai = row.Cells[4].Value?.ToString().Trim();

                // Nếu không hợp lệ
                if (string.IsNullOrEmpty(xepLoai) || !validValues.Contains(xepLoai))
                {
                    isValid = false;
                    MessageBox.Show(
                        $"Giá trị 'Xếp Loại' ở dòng {row.Index + 1} không hợp lệ!\n" +
                        "Vui lòng nhập một trong các giá trị: Tốt, Khá, Trung Bình, Yếu.",
                        "Lỗi nhập liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    // Chọn lại ô sai để người dùng dễ thấy
                    tableHanhKiem.CurrentCell = row.Cells[4];
                    break;
                }
            }

            if (isValid)
            {
                // ✅ Sau này chỗ này sẽ gọi BUS -> DAO để lưu dữ liệu
                // Ví dụ:
                // HanhKiemBUS bus = new HanhKiemBUS();
                // bus.LuuDanhSachHanhKiem(...);

                // 🕐 Tạm thời chỉ hiển thị thông báo
                MessageBox.Show("Lưu thành công (demo, chưa kết nối CSDL)!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tùy chọn: cập nhật lại màu chữ cho cột Xếp Loại sau khi lưu
                foreach (DataGridViewRow row in tableHanhKiem.Rows)
                {
                    if (row.IsNewRow) continue;
                    string xepLoai = row.Cells[4].Value?.ToString();

                    if (xepLoai == null) continue;

                    switch (xepLoai)
                    {
                        case "Tốt":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(22, 163, 74);
                            break;
                        case "Khá":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(30, 136, 229);
                            break;
                        case "Trung Bình":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(219, 39, 119);
                            break;
                        case "Yếu":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(220, 38, 38);
                            break;
                        default:
                            row.Cells[4].Style.ForeColor = Color.Black;
                            break;
                    }
                }
            }
        }

        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
