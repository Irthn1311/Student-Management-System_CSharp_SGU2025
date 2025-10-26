using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Student_Management_System_CSharp_SGU2025.GUI.NamHoc
{
    public partial class ucNamHoc : UserControl
    {
        public ucNamHoc()
        {
            InitializeComponent();
        }

        private void ucNamHoc_Load(object sender, EventArgs e)
        {
            SetupCardNH();
            SetupTbNamHoc();
            InitializeDefaultView();
            SetupTbHocKy();
            SetupCardHK();
        }

        private void SetupCardNH()
        {
            // ===== CARD 1 =====
            statCardNH1.SetData("Năm học hiện tại", "2024-2025", "Đang diễn ra");
            statCardNH1.PanelColor = ColorTranslator.FromHtml("#20bf59");
            statCardNH1.TextColor = Color.White;

            var lblTen1 = statCardNH1.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
            var lblSo1 = statCardNH1.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
            var lblDesc1 = statCardNH1.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

            if (lblTen1 != null) lblTen1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            if (lblSo1 != null) lblSo1.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            if (lblDesc1 != null) lblDesc1.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            var panelMain1 = statCardNH1.Controls["panelMain"] as Guna2Panel;
            if (panelMain1 != null) panelMain1.BorderRadius = 15;

            // ===== CARD 2 =====
            statCardNH2.SetData("Học kỳ", "Học kỳ I", "01/09 - 31/12/2024");
            statCardNH2.PanelColor = ColorTranslator.FromHtml("#3781f4");
            statCardNH2.TextColor = Color.White;

            var lblTen2 = statCardNH2.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
            var lblSo2 = statCardNH2.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
            var lblDesc2 = statCardNH2.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

            if (lblTen2 != null) lblTen2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            if (lblSo2 != null) lblSo2.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            if (lblDesc2 != null) lblDesc2.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            var panelMain2 = statCardNH2.Controls["panelMain"] as Guna2Panel;
            if (panelMain2 != null) panelMain2.BorderRadius = 15;

            // ===== CARD 3 =====
            statCardNH3.SetData("Tổng năm học", "3", "Trong hệ thống");
            statCardNH3.PanelColor = ColorTranslator.FromHtml("#f66f14");
            statCardNH3.TextColor = Color.White;

            var lblTen3 = statCardNH3.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
            var lblSo3 = statCardNH3.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
            var lblDesc3 = statCardNH3.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

            if (lblTen3 != null) lblTen3.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            if (lblSo3 != null) lblSo3.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            if (lblDesc3 != null) lblDesc3.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            var panelMain3 = statCardNH3.Controls["panelMain"] as Guna2Panel;
            if (panelMain3 != null) panelMain3.BorderRadius = 15;
        }

        private void SetupCardHK()
        {
            // ===== CARD 1 =====
            statCardHK1.SetData("Học kỳ hiện tại", "Học kỳ I", "2024-2025");
            statCardHK1.PanelColor = ColorTranslator.FromHtml("#357ef1");
            statCardHK1.TextColor = Color.White;

            var lblTen4 = statCardNH1.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
            var lblSo4 = statCardNH1.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
            var lblDesc4 = statCardNH1.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

            if (lblTen4 != null) lblTen4.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            if (lblSo4 != null) lblSo4.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            if (lblDesc4 != null) lblDesc4.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            var panelMain4 = statCardNH1.Controls["panelMain"] as Guna2Panel;
            if (panelMain4 != null) panelMain4.BorderRadius = 15;

            // ===== CARD 2 =====
            statCardHK2.SetData("Thời gian", "4 tháng", "01/09 - 31/12/2024");
            statCardHK2.PanelColor = ColorTranslator.FromHtml("#1eba57");
            statCardHK2.TextColor = Color.White;

            var lblTen5 = statCardHK2.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
            var lblSo5 = statCardHK2.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
            var lblDesc5 = statCardHK2.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

            if (lblTen5 != null) lblTen5.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            if (lblSo5 != null) lblSo5.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            if (lblDesc5 != null) lblDesc5.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            var panelMain5 = statCardNH2.Controls["panelMain"] as Guna2Panel;
            if (panelMain5 != null) panelMain5.BorderRadius = 15;

            // ===== CARD 3 =====
            statCardHK3.SetData("Tổng học kỳ", "4", "Trong hệ thống");
            statCardHK3.PanelColor = ColorTranslator.FromHtml("#a44ef6");
            statCardHK3.TextColor = Color.White;

            var lblTen6 = statCardNH3.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
            var lblSo6 = statCardNH3.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
            var lblDesc6 = statCardNH3.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

            if (lblTen6 != null) lblTen6.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            if (lblSo6 != null) lblSo6.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            if (lblDesc6 != null) lblDesc6.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            var panelMain6 = statCardHK3.Controls["panelMain"] as Guna2Panel;
            if (panelMain6 != null) panelMain6.BorderRadius = 15;
        }

        private void InitializeDefaultView()
        {
            // Hiển thị tbNamHoc, ẩn tbHocKy
            tbNamHoc.Visible = true;
            tbHocKy.Visible = false;

            // Hiển thị btnAddNamHoc, ẩn btnAddHocKy
            btnAddNamHoc.Visible = true;
            btnAddHocKy.Visible = false;

            statCardNH1.Visible = true;
            statCardNH2.Visible = true;
            statCardNH3.Visible = true;
            statCardHK1.Visible = false;
            statCardHK2.Visible = false;
            statCardHK3.Visible = false;

            // Nút btnNamHoc màu xanh dương, chữ trắng (giả sử được chọn)
            btnNamHoc.FillColor = Color.FromArgb(30, 136, 229);
            btnNamHoc.ForeColor = Color.White;

            // Nút btnHocKy màu trắng, chữ đen (chưa chọn)
            btnHocKy.FillColor = Color.White;
            btnHocKy.ForeColor = Color.Black;
        }
        private void SetupTbNamHoc()
        {
            //tbNamHoc.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            tbNamHoc.Rows.Clear();
            tbNamHoc.Columns.Clear();

            tbNamHoc.Columns.Add("namHoc", "Năm học");
            tbNamHoc.Columns.Add("ngayBatDau", "Ngày bắt đầu");
            tbNamHoc.Columns.Add("ngayKetThuc", "Ngày kết thúc");
            tbNamHoc.Columns.Add("trangThai", "Trạng thái");
            tbNamHoc.Columns.Add("thaoTac", "Thao tác");

            tbNamHoc.RowTemplate.Height = 48;


            tbNamHoc.Rows.Add("2024-2025", "01/09/2024", "31/05/2025", "Đang diễn ra", "");
            tbNamHoc.Rows.Add("2023-2024", "01/09/2023", "31/05/2024", "Đã kết thúc", "");
            tbNamHoc.Rows.Add("2022-2023", "01/09/2022", "31/05/2023", "Đã kết thúc", "");

            // ===== Định dạng hiển thị =====
            tbNamHoc.EnableHeadersVisualStyles = false;
            tbNamHoc.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            tbNamHoc.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            tbNamHoc.BackgroundColor = Color.White;
            tbNamHoc.BorderStyle = BorderStyle.None;
            tbNamHoc.GridColor = Color.FromArgb(240, 240, 240);



            tbNamHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tbNamHoc.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            tbNamHoc.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
            tbNamHoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            tbNamHoc.DefaultCellStyle.SelectionForeColor = Color.Black;
            tbNamHoc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 🔹 Header style
            tbNamHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            tbNamHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
            tbNamHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            tbNamHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbNamHoc.ColumnHeadersHeight = 50;
            tbNamHoc.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Padding nhẹ
            tbNamHoc.DefaultCellStyle.Padding = new Padding(17, 0, 0, 0);

            // Trạng thái — đổi màu
            foreach (DataGridViewRow row in tbNamHoc.Rows)
            {
                string status = row.Cells["trangThai"].Value?.ToString();
                if (status == "Đang diễn ra")
                {
                    row.Cells["trangThai"].Style.BackColor = Color.FromArgb(205, 255, 230);
                    row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(40, 150, 70);
                }
                else
                {
                    row.Cells["trangThai"].Style.BackColor = Color.FromArgb(240, 240, 240);
                    row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(100, 100, 100);
                }

                row.Cells["trangThai"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Cells["trangThai"].Style.SelectionBackColor = row.Cells["trangThai"].Style.BackColor;
            }

            tbNamHoc.AllowUserToAddRows = false;
            tbNamHoc.ReadOnly = true;
            tbNamHoc.AllowUserToDeleteRows = false;
            tbNamHoc.AllowUserToResizeColumns = false;
            tbNamHoc.AllowUserToResizeRows = false;
            tbNamHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tbNamHoc.MultiSelect = false;

            // Gắn sự kiện icon thao tác
            tbNamHoc.CellPainting += tbNamHoc_CellPainting;
            tbNamHoc.CellClick += tbNamHoc_CellClick;
        }

        private void SetupTbHocKy()
        {
            tbHocKy.Rows.Clear();
            tbHocKy.Columns.Clear();

            tbHocKy.Columns.Add("namHocHK", "Năm học");
            tbHocKy.Columns.Add("hocKy", "Học kỳ");
            tbHocKy.Columns.Add("ngayBatDauHK", "Ngày bắt đầu");
            tbHocKy.Columns.Add("ngayKetThucHK", "Ngày kết thúc");
            tbHocKy.Columns.Add("trangThaiHK", "Trạng thái");
            tbHocKy.Columns.Add("thaoTacHK", "Thao tác");

            tbHocKy.RowTemplate.Height = 48;

            tbHocKy.Rows.Add("2024-2025", "Học kỳ I", "01/09/2024", "31/12/2024", "Đang diễn ra", "");
            tbHocKy.Rows.Add("2024-2025", "Học kỳ II", "01/01/2025", "31/05/2025", "Chưa bắt đầu", "");
            tbHocKy.Rows.Add("2023-2024", "Học kỳ I", "01/09/2023", "31/12/2023", "Đã kết thúc", "");
            tbHocKy.Rows.Add("2023-2024", "Học kỳ II", "01/01/2024", "31/05/2024", "Đã kết thúc", "");

            // ===== Định dạng hiển thị =====
            tbHocKy.EnableHeadersVisualStyles = false;
            tbHocKy.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            tbHocKy.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            tbHocKy.BackgroundColor = Color.White;
            tbHocKy.BorderStyle = BorderStyle.None;
            tbHocKy.GridColor = Color.FromArgb(240, 240, 240);

            tbHocKy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tbHocKy.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            tbHocKy.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
            tbHocKy.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            tbHocKy.DefaultCellStyle.SelectionForeColor = Color.Black;
            tbHocKy.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 🔹 Header style
            tbHocKy.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            tbHocKy.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
            tbHocKy.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            tbHocKy.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbHocKy.ColumnHeadersHeight = 50;
            tbHocKy.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Padding nhẹ
            tbHocKy.DefaultCellStyle.Padding = new Padding(17, 0, 0, 0);

            // Trạng thái — đổi màu
            foreach (DataGridViewRow row in tbHocKy.Rows)
            {
                string status = row.Cells["trangThaiHK"].Value?.ToString();
                if (status == "Đang diễn ra")
                {
                    row.Cells["trangThaiHK"].Style.BackColor = Color.FromArgb(205, 255, 230);
                    row.Cells["trangThaiHK"].Style.ForeColor = Color.FromArgb(40, 150, 70);
                }
                else if (status == "Chưa bắt đầu")
                {
                    row.Cells["trangThaiHK"].Style.BackColor = Color.FromArgb(230, 237, 255);
                    row.Cells["trangThaiHK"].Style.ForeColor = Color.FromArgb(20, 100, 200);
                }
                else
                {
                    row.Cells["trangThaiHK"].Style.BackColor = Color.FromArgb(240, 240, 240);
                    row.Cells["trangThaiHK"].Style.ForeColor = Color.FromArgb(100, 100, 100);
                }

                row.Cells["trangThaiHK"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Cells["trangThaiHK"].Style.SelectionBackColor = row.Cells["trangThaiHK"].Style.BackColor;
            }

            tbHocKy.AllowUserToAddRows = false;
            tbHocKy.ReadOnly = true;
            tbHocKy.AllowUserToDeleteRows = false;
            tbHocKy.AllowUserToResizeColumns = false;
            tbHocKy.AllowUserToResizeRows = false;
            tbHocKy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tbHocKy.MultiSelect = false;

            // Gắn sự kiện icon thao tác
            tbHocKy.CellPainting += tbHocKy_CellPainting;
            tbHocKy.CellClick += tbHocKy_CellClick;
        }


        private void tbNamHoc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbNamHoc.Columns["thaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                int iconSize = 22;
                int iconEyeSize = 32;  // ✅ Icon_eye lớn hơn
                int padding = 6;

                int xEdit = e.CellBounds.Left + padding;
                int xDelete = xEdit + iconEyeSize + 4 * padding;  // ✅ Cách tính toán dựa trên iconEyeSize
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconEyeSize) / 2;  // ✅ Căn giữa dựa trên iconEyeSize
                int yDelete = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;  // Căn giữa icon delete

                try
                {
                    Image editIcon = Image.FromFile(@"..\..\Images\icon_eye.png");
                    Image deleteIcon = Image.FromFile(@"..\..\Images\deleteicon.png");

                    // ✅ Vẽ icon_eye với kích thước lớn hơn
                    e.Graphics.DrawImage(editIcon, new Rectangle(xEdit, y, iconEyeSize, iconEyeSize));

                    // Vẽ icon delete với kích thước bình thường
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(xDelete, yDelete, iconSize, iconSize));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi load icon thao tác: " + ex.Message);
                }

                e.Handled = true;
            }
        }

        private void tbNamHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbNamHoc.Columns["thaoTac"].Index)
            {
                var cell = tbNamHoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = tbNamHoc.PointToClient(Cursor.Position).X - cell.X;

                int iconSize = 22;
                int iconEyeSize = 32;  // ✅ Cùng kích thước như trong CellPainting
                int padding = 6;

                int eyeRight = padding + iconEyeSize;  // ✅ Cập nhật hitbox cho icon_eye
                int deleteLeft = eyeRight + 4 * padding;
                int deleteRight = deleteLeft + iconSize;

                string namHoc = tbNamHoc.Rows[e.RowIndex].Cells["namHoc"].Value.ToString();

                if (x < eyeRight)  // ✅ Cập nhật điều kiện click
                {
                    MessageBox.Show($"Sửa thông tin năm học: {namHoc}", "Sửa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (x > deleteLeft && x < deleteRight)
                {
                    MessageBox.Show($"Xóa năm học: {namHoc}", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void tbHocKy_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbHocKy.Columns["thaoTacHK"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                int iconSizeHK = 22;
                int iconEyeSizeHK = 32;
                int paddingHK = 6;

                int xEditHK = e.CellBounds.Left + paddingHK;
                int xDeleteHK = xEditHK + iconEyeSizeHK + 4 * paddingHK;
                int yHK = e.CellBounds.Top + (e.CellBounds.Height - iconEyeSizeHK) / 2;
                int yDeleteHK = e.CellBounds.Top + (e.CellBounds.Height - iconSizeHK) / 2;

                try
                {
                    Image editIconHK = Image.FromFile(@"..\..\Images\icon_eye.png");
                    Image deleteIconHK = Image.FromFile(@"..\..\Images\deleteicon.png");

                    e.Graphics.DrawImage(editIconHK, new Rectangle(xEditHK, yHK, iconEyeSizeHK, iconEyeSizeHK));
                    e.Graphics.DrawImage(deleteIconHK, new Rectangle(xDeleteHK, yDeleteHK, iconSizeHK, iconSizeHK));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi load icon thao tác (tbHocKy): " + ex.Message);
                }

                e.Handled = true;
            }
        }

        private void tbHocKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbHocKy.Columns["thaoTacHK"].Index)
            {
                var cell = tbHocKy.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = tbHocKy.PointToClient(Cursor.Position).X - cell.X;

                int iconSizeHK = 22;
                int iconEyeSizeHK = 32;
                int paddingHK = 6;

                int eyeRightHK = paddingHK + iconEyeSizeHK;
                int deleteLeftHK = eyeRightHK + 4 * paddingHK;
                int deleteRightHK = deleteLeftHK + iconSizeHK;

                string namHocHK = tbHocKy.Rows[e.RowIndex].Cells["namHocHK"].Value.ToString();
                string hocKyInfo = tbHocKy.Rows[e.RowIndex].Cells["hocKy"].Value.ToString();

                if (x < eyeRightHK)
                {
                    MessageBox.Show($"Sửa thông tin học kỳ: {namHocHK} - {hocKyInfo}", "Sửa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (x > deleteLeftHK && x < deleteRightHK)
                {
                    MessageBox.Show($"Xóa học kỳ: {namHocHK} - {hocKyInfo}", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void statCardKhoi2_Load(object sender, EventArgs e)
        {

        }

        private void statCardNH1_Load(object sender, EventArgs e)
        {

        }

        private void statCardNH3_Load(object sender, EventArgs e)
        {

        }

        private void tbNamHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //// Tắt chức năng highlight cell khi nhấn
            //tbNamHoc.DefaultCellStyle.SelectionBackColor = Color.White;
            //tbNamHoc.DefaultCellStyle.SelectionForeColor = Color.FromArgb(50, 50, 50);
        }

        private void btnNamHoc_Click(object sender, EventArgs e)
        {
            // Hiển thị tbNamHoc, ẩn tbHocKy
            tbNamHoc.Visible = true;
            tbHocKy.Visible = false;

            // Hiển thị btnAddNamHoc, ẩn btnAddHocKy
            btnAddNamHoc.Visible = true;
            btnAddHocKy.Visible = false;

            statCardNH1.Visible = true;
            statCardNH2.Visible = true;
            statCardNH3.Visible = true;
            statCardHK1.Visible = false;
            statCardHK2.Visible = false;
            statCardHK3.Visible = false;

            // Nút btnNamHoc màu xanh dương, chữ trắng
            btnNamHoc.FillColor = Color.FromArgb(30, 136, 229);
            btnNamHoc.ForeColor = Color.White;

            // Nút btnHocKy màu trắng, chữ đen
            btnHocKy.FillColor = Color.White;
            btnHocKy.ForeColor = Color.Black;
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            // Hiển thị tbHocKy, ẩn tbNamHoc
            tbNamHoc.Visible = false;
            tbHocKy.Visible = true;

            // Hiển thị btnAddHocKy, ẩn btnAddNamHoc
            btnAddNamHoc.Visible = false;
            btnAddHocKy.Visible = true;

            statCardNH1.Visible = false;
            statCardNH2.Visible = false;
            statCardNH3.Visible = false;
            statCardHK1.Visible = true;
            statCardHK2.Visible = true;
            statCardHK3.Visible = true;


            // Nút btnHocKy màu xanh dương, chữ trắng
            btnHocKy.FillColor = Color.FromArgb(30, 136, 229);
            btnHocKy.ForeColor = Color.White;

            // Nút btnNamHoc màu trắng, chữ đen
            btnNamHoc.FillColor = Color.White;
            btnNamHoc.ForeColor = Color.Black;
        }

        private void btnAddNamHoc_Click(object sender, EventArgs e)
        {

        }

        private void tbHocKy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddHocKy_Click(object sender, EventArgs e)
        {

        }
    }
}
