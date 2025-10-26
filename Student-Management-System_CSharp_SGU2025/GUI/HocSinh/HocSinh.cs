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
    public partial class HocSinh : UserControl
    {
        public HocSinh()
        {
            InitializeComponent();
            SetupTableHocSinh();
        }
        private void SetupTableHocSinh()
        {
            // Cấu hình chung
            tableHocSinh.EnableHeadersVisualStyles = false;
            tableHocSinh.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            tableHocSinh.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            tableHocSinh.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableHocSinh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            tableHocSinh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableHocSinh.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            tableHocSinh.ColumnHeadersHeight = 42;

            tableHocSinh.DefaultCellStyle.BackColor = Color.White;
            tableHocSinh.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            tableHocSinh.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            tableHocSinh.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            tableHocSinh.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            tableHocSinh.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            tableHocSinh.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            tableHocSinh.GridColor = Color.FromArgb(230, 230, 230);
            tableHocSinh.RowTemplate.Height = 46;
            tableHocSinh.BorderStyle = BorderStyle.None;
            tableHocSinh.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableHocSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tableHocSinh.MultiSelect = false;
            tableHocSinh.ReadOnly = true;
            tableHocSinh.AllowUserToResizeColumns = false;
            tableHocSinh.AllowUserToResizeRows = false;
            tableHocSinh.RowHeadersVisible = false;
            tableHocSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tableHocSinh.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // === Tạo các cột dữ liệu ===
            tableHocSinh.Columns.Clear();
            tableHocSinh.Columns.Add("MaHS", "Mã HS");
            tableHocSinh.Columns.Add("HoTen", "Họ và tên");
            tableHocSinh.Columns.Add("NgaySinh", "Ngày sinh");
            tableHocSinh.Columns.Add("GioiTinh", "Giới tính");
            tableHocSinh.Columns.Add("Lop", "Lớp");
            tableHocSinh.Columns.Add("TrangThai", "Trạng thái");

            // Căn giữa toàn bộ header và cell
            foreach (DataGridViewColumn col in tableHocSinh.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }

            // Họ tên căn trái
            tableHocSinh.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // === Tùy chỉnh kích thước hợp lý ===
            tableHocSinh.Columns["MaHS"].FillWeight = 10; tableHocSinh.Columns["MaHS"].MinimumWidth = 60;
            tableHocSinh.Columns["HoTen"].FillWeight = 25; tableHocSinh.Columns["HoTen"].MinimumWidth = 150;
            tableHocSinh.Columns["NgaySinh"].FillWeight = 12; tableHocSinh.Columns["NgaySinh"].MinimumWidth = 100;
            tableHocSinh.Columns["GioiTinh"].FillWeight = 10; tableHocSinh.Columns["GioiTinh"].MinimumWidth = 80;
            tableHocSinh.Columns["Lop"].FillWeight = 10; tableHocSinh.Columns["Lop"].MinimumWidth = 70;
            tableHocSinh.Columns["TrangThai"].FillWeight = 10; tableHocSinh.Columns["TrangThai"].MinimumWidth = 90;

            // ====== CỘT ICON (Thao tác) ======
            var colView = new DataGridViewImageColumn()
            {
                Name = "View",
                HeaderText = "Thao tác",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
            };
            var colEdit = new DataGridViewImageColumn()
            {
                Name = "Edit",
                HeaderText = "",
                ImageLayout = DataGridViewImageCellLayout.Zoom,
            };

            tableHocSinh.Columns.Add(colView);
            tableHocSinh.Columns.Add(colEdit);

            int viewColWidth = 70; 
            int editColWidth = 34;

            tableHocSinh.Columns["View"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableHocSinh.Columns["Edit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableHocSinh.Columns["View"].Width = viewColWidth;
            tableHocSinh.Columns["Edit"].Width = editColWidth;

            tableHocSinh.Columns["View"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableHocSinh.Columns["Edit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableHocSinh.Columns["View"].DefaultCellStyle.Padding = new Padding(6);
            tableHocSinh.Columns["Edit"].DefaultCellStyle.Padding = new Padding(6);

            // Event
            tableHocSinh.CellFormatting += tableHocSinh_CellFormatting;
            tableHocSinh.CellClick += tableHocSinh_CellContentClick;

            tableHocSinh.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    tableHocSinh.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(243, 246, 255);
            };
            tableHocSinh.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    tableHocSinh.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                        (e.RowIndex % 2 == 0) ? Color.White : Color.FromArgb(250, 250, 250);
            };

            tableHocSinh.SelectionChanged += (s, e) => tableHocSinh.ClearSelection();
        }


        // === Thêm icon vào cột thao tác ===
        private void AddRowWithIcons(string ma, string ten, string ns, string gioitinh, string lop, string trangthai)
        {
            // Thêm dữ liệu dạng text vào các ô tương ứng
            int idx = tableHocSinh.Rows.Add(ma, ten, ns, gioitinh, lop, trangthai, null, null);

            try
            {
                // Gán trực tiếp ảnh từ Resources vào các ô chứa ảnh.
                // Cách này không cần kiểm tra file tồn tại vì tài nguyên đã được nhúng vào chương trình.
                tableHocSinh.Rows[idx].Cells["View"].Value = Properties.Resources.icon_eye;
                tableHocSinh.Rows[idx].Cells["Edit"].Value = Properties.Resources.deleteicon;
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

        private void HandleEditClick(int rowIndex)
        {
            // TODO: Xóa học sinh
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void statCardTongHocSinh_Load(object sender, EventArgs e)
        {

        }

        private void HocSinh_Load_1(object sender, EventArgs e)
        {

            // chèn dữ liệu mẫu vào Header
            headerQuanLiHocSinh.lbHeader.Text = "Hồ sơ Học sinh";
            headerQuanLiHocSinh.lbGhiChu.Text = "Trang chủ / Hồ sơ học sinh";
            headerQuanLiHocSinh.lbTenDangNhap.Text = "Nguyễn Văn A";
            headerQuanLiHocSinh.lbVaiTro.Text = "Giáo vụ";

            // chèn dữ liệu mẫu vào các thẻ thống kê
            statCardTongHocSinh.lbCardTitle.Text = "Tổng học sinh";
            statCardTongHocSinh.lbCardValue.Text = "1,247";
            statCardTongHocSinh.lbCardNote.Text = "+42 tháng này";

            statCardNam.lbCardTitle.Text = "Nam";
            statCardNam.lbCardValue.Text = "648";
            statCardNam.lbCardNote.Text = "52% tổng số";

            statCardNu.lbCardTitle.Text = "Nữ";
            statCardNu.lbCardValue.Text = "599";
            statCardNu.lbCardNote.Text = "48% tổng số";

            statCardDangHoc.lbCardTitle.Text = "Đang học";
            statCardDangHoc.lbCardValue.Text = "1,235";
            statCardDangHoc.lbCardNote.Text = "12 nghỉ học";

            statCardTongHocSinh.lbCardNote.ForeColor = Color.FromArgb(22, 163, 74);
            statCardNam.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardNu.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardDangHoc.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardDangHoc.lbCardNote.ForeColor = Color.FromArgb(220, 38, 38);


            // chèn dữ liệu mẫu vào bảng
            tableHocSinh.Rows.Clear();

            AddRowWithIcons("HS001", "Nguyễn Văn An", "15/03/2008", "Nam", "10A1", "Đang học");
            AddRowWithIcons("HS002", "Trần Thị Bình", "22/05/2008", "Nữ", "10A1", "Đang học");
            AddRowWithIcons("HS003", "Lê Hoàng Cường", "10/08/2008", "Nam", "10A2", "Đang học");
            AddRowWithIcons("HS004", "Phạm Thị Dung", "18/01/2008", "Nữ", "10A2", "Đang học");
            AddRowWithIcons("HS005", "Hoàng Văn Em", "25/11/2007", "Nam", "11A1", "Đang học");
            AddRowWithIcons("HS006", "Vũ Thị Hoa", "30/06/2007", "Nữ", "11A1", "Đang học");
            AddRowWithIcons("HS007", "Đỗ Văn Khoa", "12/09/2006", "Nam", "12A1", "Đang học");
            AddRowWithIcons("HS008", "Bùi Thị Lan", "05/04/2006", "Nữ", "12A2", "Đang học");
            AddRowWithIcons("HS009", "Ngô Minh Nhật", "10/02/2007", "Nam", "11A2", "Nghỉ học");
            AddRowWithIcons("HS010", "Phan Thị Mai", "08/07/2007", "Nữ", "11A3", "Đang học");

        }


        private void tableHocSinh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string column = tableHocSinh.Columns[e.ColumnIndex].Name;

            if (column == "View")
                HandleViewClick(e.RowIndex);
            else if (column == "Edit")
                HandleEditClick(e.RowIndex);
        }

        private void tableHocSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (tableHocSinh.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null)
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

            if (tableHocSinh.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                e.CellStyle.ForeColor = e.Value.ToString() == "Đang học"
                    ? Color.FromArgb(52, 168, 83)
                    : Color.Gray;
            }
        }

        private void statCardNam_Load(object sender, EventArgs e)
        {

        }

        private void headerQuanLiHocSinh_Load(object sender, EventArgs e)
        {

        }

        private void btnThemHocSinh_Click(object sender, EventArgs e)
        {
            ThemHoSoHocSinh frm = new ThemHoSoHocSinh();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }
    }
}


