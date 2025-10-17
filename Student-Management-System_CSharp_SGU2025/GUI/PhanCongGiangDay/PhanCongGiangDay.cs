using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System.Drawing.Imaging;

namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    public partial class PhanCongGiangDay : UserControl
    {
        public PhanCongGiangDay()
        {
            InitializeComponent();
        }

        private void PhanCongGiangDay_Load(object sender, EventArgs e)
        {
            // 1️⃣ Gọi hàm load thẻ thống kê
            LoadStatCards();

            // 2️⃣ Gọi hàm load dữ liệu bảng
            LoadData();

        }
        
        private void LoadStatCards()
        {
            /*
            panelShow.Controls.Clear(); // Xóa hết để load lại

            // Tạo danh sách card (ví dụ 4 cái)
            StatCard card1 = new StatCard();
            card1.Title = "Tổng giáo viên";
            card1.Value = "32";
            card1.BackColor = Color.DodgerBlue;

            StatCard card2 = new StatCard();
            card2.Title = "Tổng môn học";
            card2.Value = "18";
            card2.BackColor = Color.MediumSeaGreen;

            StatCard card3 = new StatCard();
            card3.Title = "Tổng lớp học";
            card3.Value = "12";
            card3.BackColor = Color.Orange;

            StatCard card4 = new StatCard();
            card4.Title = "Phân công hiện tại";
            card4.Value = "45";
            card4.BackColor = Color.MediumOrchid;

            // Danh sách cards
            StatCard[] cards = { card1, card2, card3, card4 };

            // Căn đều 4 card trên panelShow
            int spacing = 20;
            int cardWidth = (panelShow.Width - spacing * (cards.Length + 1)) / cards.Length;
            int cardHeight = panelShow.Height - 20;

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].Size = new Size(cardWidth, cardHeight);
                cards[i].Location = new Point(spacing + i * (cardWidth + spacing), 10);
                panelShow.Controls.Add(cards[i]);
            }
            */


            // Ví dụ dữ liệu mẫu
            statCardPhanCongGiangDay1.Title = "Tổng phân công";
            statCardPhanCongGiangDay1.Value = "36";

            statCardPhanCongGiangDay2.Title = "Giáo viên";
            statCardPhanCongGiangDay2.Value = "36";
            statCardPhanCongGiangDay2.TitleColor = Color.FromArgb(30, 136, 229);

            statCardPhanCongGiangDay3.Title = "Môn học";
            statCardPhanCongGiangDay3.Value = "20";
            statCardPhanCongGiangDay3.TitleColor = Color.FromArgb(20, 163, 74);

            statCardPhanCongGiangDay4.Title = "Lớp học";
            statCardPhanCongGiangDay4.Value = "235";
            statCardPhanCongGiangDay4.TitleColor = Color.FromArgb(234, 88, 12);


        }
        

        private void LoadData()
        {
            // ===================================
            // 1️⃣ CẤU HÌNH DATAGRIDVIEW
            // ===================================

            // --- Cài đặt cơ bản ---
            dgvPhanCong.Columns.Clear();
            dgvPhanCong.Rows.Clear();
            dgvPhanCong.AutoGenerateColumns = false;
            dgvPhanCong.AllowUserToAddRows = false;
            dgvPhanCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhanCong.ReadOnly = true;

            // --- Thiết lập giao diện ---
            dgvPhanCong.BackgroundColor = Color.White;
            dgvPhanCong.BorderStyle = BorderStyle.None;
            dgvPhanCong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPhanCong.RowHeadersVisible = false;

            // --- Style cho tiêu đề cột ---
            dgvPhanCong.EnableHeadersVisualStyles = false;
            dgvPhanCong.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPhanCong.ColumnHeadersHeight = 50; // Tăng chiều cao tiêu đề
            dgvPhanCong.ColumnHeadersDefaultCellStyle.BackColor = Color.White; // Nền trắng cho tiêu đề
            dgvPhanCong.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139); // Màu chữ xám cho tiêu đề
            dgvPhanCong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvPhanCong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White; // Ngăn đổi màu khi click
            dgvPhanCong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa tiêu đề

            // --- Style cho các dòng dữ liệu ---
            dgvPhanCong.RowTemplate.Height = 45; // Tăng chiều cao dòng
            dgvPhanCong.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvPhanCong.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59); // Màu chữ chính
            dgvPhanCong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252); // Màu nền khi chọn dòng
            dgvPhanCong.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59); // Màu chữ khi chọn dòng
            dgvPhanCong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa nội dung
            dgvPhanCong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252); // Màu xen kẽ cho dễ nhìn

            // ===================================
            // 2️⃣ TẠO CỘT VÀ THIẾT LẬP ĐỘ RỘNG
            // ===================================
            dgvPhanCong.Columns.Add("GiaoVien", "Giáo viên");
            dgvPhanCong.Columns.Add("MonHoc", "Môn học");
            dgvPhanCong.Columns.Add("Lop", "Lớp");
            dgvPhanCong.Columns.Add("HocKy", "Học kỳ");
            dgvPhanCong.Columns.Add("SoTiet", "Số tiết");
            dgvPhanCong.Columns.Add("ThaoTac", "Thao tác");

            // Thiết lập chế độ co giãn
            dgvPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhanCong.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPhanCong.Columns["ThaoTac"].Width = 80;

            // ===================================
            // 3️⃣ NẠP DỮ LIỆU MẪU
            // ===================================
            dgvPhanCong.Rows.Add("Nguyễn Thị Hoa", "Toán học", "10A1", "HK I", "5 tiết/tuần");
            dgvPhanCong.Rows.Add("Trần Văn Nam", "Ngữ văn", "10A1", "HK I", "5 tiết/tuần");
            dgvPhanCong.Rows.Add("Lê Thị Mai", "Tiếng Anh", "10A2", "HK I", "4 tiết/tuần");
            dgvPhanCong.Rows.Add("Phạm Văn Đức", "Vật lý", "11A1", "HK I", "3 tiết/tuần");
            dgvPhanCong.Rows.Add("Hoàng Thị Lan", "Hóa học", "11A2", "HK I", "3 tiết/tuần");
            dgvPhanCong.Rows.Add("Vũ Văn Hùng", "Sinh học", "11A3", "HK I", "3 tiết/tuần");
            dgvPhanCong.Rows.Add("Đỗ Thị Thu", "Lịch sử", "12A1", "HK I", "2 tiết/tuần");
            dgvPhanCong.Rows.Add("Bùi Văn Toàn", "Địa lý", "12A2", "HK I", "2 tiết/tuần");

            // ===================================
            // 4️⃣ GẮN SỰ KIỆN
            // ===================================
            dgvPhanCong.CellPainting += dgvPhanCong_CellPainting;
            dgvPhanCong.CellClick += dgvPhanCong_CellClick;
        }

        private void dgvPhanCong_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // --- Tô màu cho cột "Lớp" ---
            if (dgvPhanCong.Columns[e.ColumnIndex].Name == "Lop")
            {
                string lopText = e.Value?.ToString();
                if (!string.IsNullOrEmpty(lopText))
                {
                    if (lopText.StartsWith("10"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246);   // Màu xanh dương
                    }
                    else if (lopText.StartsWith("11"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(34, 197, 94);    // Màu xanh lá
                    }
                    else if (lopText.StartsWith("12"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(249, 115, 22);   // Màu cam
                    }
                }
            }

            // Kiểm tra nếu đang vẽ ô trong cột "TuyChinh"
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPhanCong.Columns["ThaoTac"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Icon từ Resources
                Image deleteIcon = Properties.Resources.delete_icon; // 🗑️

                int iconSize = 20;
                int x = e.CellBounds.Left + (e.CellBounds.Width - iconSize) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                e.Graphics.DrawImage(deleteIcon, new Rectangle(x, y, iconSize, iconSize));

                e.Handled = true;
            }
        }

        private void dgvPhanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Chỉ xử lý khi click vào cột "ThaoTac" và không phải hàng tiêu đề
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPhanCong.Columns["ThaoTac"].Index)
            {
                // SỬA LẠI TÊN CỘT Ở ĐÂY
                string gv = dgvPhanCong.Rows[e.RowIndex].Cells["GiaoVien"].Value.ToString();

                if (MessageBox.Show($"Xóa phân công của {gv}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgvPhanCong.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void dgvPhanCong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelShow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelPhanCongGiangDay_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
