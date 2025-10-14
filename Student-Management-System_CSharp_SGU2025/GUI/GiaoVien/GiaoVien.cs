using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.GiaoVien
{
    public partial class GiaoVien : UserControl
    {
        public GiaoVien()
        {
            InitializeComponent();
            SetupTableGiaoVien();
        }

        private void SetupTableGiaoVien()
        {
            // Cấu hình chung
            tableGiaoVien.EnableHeadersVisualStyles = false;
            tableGiaoVien.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            tableGiaoVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            tableGiaoVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableGiaoVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            tableGiaoVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableGiaoVien.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            tableGiaoVien.ColumnHeadersHeight = 42;

            tableGiaoVien.DefaultCellStyle.BackColor = Color.White;
            tableGiaoVien.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            tableGiaoVien.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            tableGiaoVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            tableGiaoVien.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            tableGiaoVien.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            tableGiaoVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            tableGiaoVien.GridColor = Color.FromArgb(230, 230, 230);
            tableGiaoVien.RowTemplate.Height = 46;
            tableGiaoVien.BorderStyle = BorderStyle.None;
            tableGiaoVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableGiaoVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tableGiaoVien.MultiSelect = false;
            tableGiaoVien.ReadOnly = true;
            tableGiaoVien.AllowUserToResizeColumns = false;
            tableGiaoVien.AllowUserToResizeRows = false;
            tableGiaoVien.RowHeadersVisible = false;
            tableGiaoVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tableGiaoVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // === Tạo các cột dữ liệu ===
            tableGiaoVien.Columns.Clear();
            tableGiaoVien.Columns.Add("MaGv", "Mã GV");
            tableGiaoVien.Columns.Add("HoTen", "Họ và tên");
            tableGiaoVien.Columns.Add("GioiTinh", "Giới tính");
            tableGiaoVien.Columns.Add("ChuyenMon", "Chuyên môn");
            tableGiaoVien.Columns.Add("Sdt", "Sdt");
            tableGiaoVien.Columns.Add("TrangThai", "Trạng thái");

            // Căn giữa toàn bộ header và cell
            foreach (DataGridViewColumn col in tableGiaoVien.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }

            // Họ tên căn trái
            tableGiaoVien.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // === Tùy chỉnh kích thước hợp lý ===
            tableGiaoVien.Columns["MaGv"].FillWeight = 10; tableGiaoVien.Columns["MaGv"].MinimumWidth = 60;
            tableGiaoVien.Columns["HoTen"].FillWeight = 25; tableGiaoVien.Columns["HoTen"].MinimumWidth = 150;
            tableGiaoVien.Columns["GioiTinh"].FillWeight = 10; tableGiaoVien.Columns["GioiTinh"].MinimumWidth = 80;
            tableGiaoVien.Columns["ChuyenMon"].FillWeight = 12; tableGiaoVien.Columns["ChuyenMon"].MinimumWidth = 100;
            tableGiaoVien.Columns["Sdt"].FillWeight = 10; tableGiaoVien.Columns["Sdt"].MinimumWidth = 70;
            tableGiaoVien.Columns["TrangThai"].FillWeight = 10; tableGiaoVien.Columns["TrangThai"].MinimumWidth = 90;

            // ====== CỘT ICON (Thao tác) ======
            var colView = new DataGridViewImageColumn()
            {
                Name = "View",
                HeaderText = "Thao tác",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
            };
            var colDel = new DataGridViewImageColumn()
            {
                Name = "Delete",
                HeaderText = "",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
            };

            tableGiaoVien.Columns.Add(colView);
            tableGiaoVien.Columns.Add(colDel);

            int viewColWidth = 70;
            int delColWidth = 34;

            tableGiaoVien.Columns["View"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableGiaoVien.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableGiaoVien.Columns["View"].Width = viewColWidth;
            tableGiaoVien.Columns["Delete"].Width = delColWidth;

            tableGiaoVien.Columns["View"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableGiaoVien.Columns["Delete"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableGiaoVien.Columns["View"].DefaultCellStyle.Padding = new Padding(6);
            tableGiaoVien.Columns["Delete"].DefaultCellStyle.Padding = new Padding(6);

            // Event
            tableGiaoVien.CellFormatting += tableGiaoVien_CellFormatting;
            tableGiaoVien.CellClick += tableGiaoVien_CellContentClick;

            tableGiaoVien.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    tableGiaoVien.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(243, 246, 255);
            };
            tableGiaoVien.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    tableGiaoVien.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        (e.RowIndex % 2 == 0) ? Color.White : Color.FromArgb(250, 250, 250);
            };

            tableGiaoVien.SelectionChanged += (s, e) => tableGiaoVien.ClearSelection();
        }

        // === Thêm icon vào cột thao tác ===
        private void AddRowWithIcons(string ma, string ten, string gioitinh, string chuyenmon, string sdt, string trangthai)
        {
            // Thêm dữ liệu dạng text vào các ô tương ứng
            int idx = tableGiaoVien.Rows.Add(ma, ten, gioitinh, chuyenmon, sdt, trangthai, null, null);

            try
            {
                // Gán trực tiếp ảnh từ Resources vào các ô chứa ảnh.
                // Cách này không cần kiểm tra file tồn tại vì tài nguyên đã được nhúng vào chương trình.
                tableGiaoVien.Rows[idx].Cells["View"].Value = Properties.Resources.icon_eye;
                tableGiaoVien.Rows[idx].Cells["Delete"].Value = Properties.Resources.deleteicon;
            }
            catch (Exception ex)
            {
                // Khối catch vẫn nên giữ lại để xử lý các lỗi không mong muốn khác
                MessageBox.Show("Không thể load icon thao tác: " + ex.Message);
            }
        }

        // === Hàm xử lý click icon (hiện tại rỗng) ===
        private void HandleViewClick(int rowIndex)
        {
            // TODO: Hiển thị chi tiết học sinh và chỉnh sửa
        }

        private void HandleDelClick(int rowIndex)
        {
            // TODO: Xóa học sinh
        }

        private void GiaoVien_Load(object sender, EventArgs e)
        {
            // chèn dữ liệu mẫu vào các thẻ thống kê
            statCardTongGiaoVien.lbCardTitle.Text = "Tổng giáo viên";
            statCardTongGiaoVien.lbCardValue.Text = "87";
            statCardTongGiaoVien.lbCardNote.Text = "";

            statCardGiaoVienNam.lbCardTitle.Text = "Nam";
            statCardGiaoVienNam.lbCardValue.Text = "42";
            statCardGiaoVienNam.lbCardNote.Text = "";

            statCardGiaoVienNu.lbCardTitle.Text = "Nữ";
            statCardGiaoVienNu.lbCardValue.Text = "47";
            statCardGiaoVienNu.lbCardNote.Text = "";

            statCardBoMon.lbCardTitle.Text = "Bộ môn";
            statCardBoMon.lbCardValue.Text = "12";
            statCardBoMon.lbCardNote.Text = "";

            statCardTongGiaoVien.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardGiaoVienNam.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardGiaoVienNu.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardBoMon.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardBoMon.lbCardTitle.ForeColor = Color.FromArgb(220, 38, 38);

            // chèn dữ liệu mẫu vào bảng
            tableGiaoVien.Rows.Clear();

            AddRowWithIcons("GV001", "Nguyễn Văn An", "Nam", "Toán học", "0912345678", "Đang giảng dạy");
            AddRowWithIcons("GV002", "Trần Thị Bình", "Nữ", "Ngữ văn", "0987654321", "Đang giảng dạy");
            AddRowWithIcons("GV003", "Lê Hoàng Cường", "Nam", "Vật lý", "0905123456", "Đang giảng dạy");
            AddRowWithIcons("GV004", "Phạm Thị Dung", "Nữ", "Hóa học", "0971234567", "Đang giảng dạy");
            AddRowWithIcons("GV005", "Hoàng Văn Em", "Nam", "Tin học", "0934567890", "Đang giảng dạy");
            AddRowWithIcons("GV006", "Vũ Thị Hoa", "Nữ", "Sinh học", "0945123987", "Đang giảng dạy");
            AddRowWithIcons("GV007", "Đỗ Văn Khoa", "Nam", "Lịch sử", "0923344556", "Đang giảng dạy");
            AddRowWithIcons("GV008", "Bùi Thị Lan", "Nữ", "Địa lý", "0967123456", "Đang giảng dạy");
            AddRowWithIcons("GV009", "Ngô Minh Nhật", "Nam", "Tiếng Anh", "0956123789", "Nghỉ dạy");
            AddRowWithIcons("GV010", "Phan Thị Mai", "Nữ", "Giáo dục công dân", "0938123456", "Đang giảng dạy");

        }

        private void tableGiaoVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string column = tableGiaoVien.Columns[e.ColumnIndex].Name;

            if (column == "View")
                HandleViewClick(e.RowIndex);
            else if (column == "Delete")
                HandleDelClick(e.RowIndex);
        }

        private void tableGiaoVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (tableGiaoVien.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold);
                if (e.Value.ToString() == "Nam")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216);
                    e.CellStyle.BackColor = Color.FromArgb(219, 234, 254);
                }
                else if (e.Value.ToString() == "Nữ")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(190, 24, 93);
                    e.CellStyle.BackColor = Color.FromArgb(243, 232, 255);
                }
            }

            if (tableGiaoVien.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                e.CellStyle.ForeColor = e.Value.ToString() == "Đang học"
                    ? Color.FromArgb(52, 168, 83)
                    : Color.Gray;
            }
        }
    }
}
