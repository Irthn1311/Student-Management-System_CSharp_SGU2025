using ClosedXML.Excel;
using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
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
        private KhenThuongKyLuatDAO ktklDAO = new KhenThuongKyLuatDAO();
        private HocSinhDAO hocSinhDAO = new HocSinhDAO();
        private HocKyDAO hocKyDAO = new HocKyDAO();
        private string searchKeyword = "";
        private KhenThuongKyLuatBUS ktklBUS = new KhenThuongKyLuatBUS();
        private int selectedMaHocKy = -1;
        private int selectedMaLop = -1;
        public DanhGia()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckAccessPermission(PermissionHelper.QLDANHGIA, "Quản lý đánh giá"))
            {
                this.Enabled = false;
                return;
            }
            SetupKhenThuongTable();
            SetupKyLuatTable();

            tbKhenThuong.Visible = true;
            tbKyLuat.Visible = false;

            btnAddDanhGia.Visible = true;
         

            btnKhenThuong.FillColor = Color.FromArgb(32, 136, 225); // Khen thưởng active
            btnKhenThuong.ForeColor = Color.White;

            btnKyLuat.FillColor = Color.White; // Kỷ luật inactive
            btnKyLuat.ForeColor = Color.Black;

            // Thẻ 1: Tổng khen thưởng
            thongKeCard1.TieuDe1 = "Tổng khen thưởng";
            thongKeCard1.TieuDe2 = "142";
            thongKeCard1.TieuDe3 = "Năm học này";
            thongKeCard1.FillColor = Color.FromArgb(34,197,94); // Xanh lá

            // Thẻ 2: Cấp trường
            thongKeCard2.TieuDe1 = "Cấp trường";
            thongKeCard2.TieuDe2 = "98";
            thongKeCard2.TieuDe3 = "69% tổng số";
            thongKeCard2.FillColor = Color.FromArgb(59,130,246); // Xanh dương

            // Thẻ 3: Cấp tỉnh
            thongKeCard3.TieuDe1 = "Cấp tỉnh";
            thongKeCard3.TieuDe2 = "32";
            thongKeCard3.TieuDe3 = "23% tổng số";
            thongKeCard3.FillColor = Color.FromArgb(249,115,22); // Cam

            // Thẻ 4: Vi phạm kỷ luật
            thongKeCard4.TieuDe1 = "Vi phạm kỷ luật";
            thongKeCard4.TieuDe2 = "28";
            thongKeCard4.TieuDe3 = "2.2% học sinh";
            thongKeCard4.FillColor = Color.FromArgb(239,68,68); // Đỏ

            LoadHocKyComboBox();
            LoadLopComboBox();
            txtSearch.TextChanged += txtSearch_TextChanged;
            ApplyFilter();

            // ✅ Áp dụng phân quyền
            PermissionHelper.ApplyPermissionDanhGia(
                btnAddDanhGia,
                btnDuyet, // btnDuyetDanhGia
                tbKhenThuong,
                tbKyLuat
            );

        }

        /// <summary>
        /// Cập nhật thống kê cho 4 card
        /// </summary>
        private void CapNhatThongKe()
        {
            try
            {
                int maHocKyHienTai = selectedMaHocKy;

                // Nếu chọn "Tất cả học kỳ", lấy học kỳ gần nhất
                if (maHocKyHienTai == -1)
                {
                    List<HocKyDTO> dsHocKy = hocKyDAO.GetAllHocKy();
                    if (dsHocKy.Count > 0)
                    {
                        maHocKyHienTai = dsHocKy[0].MaHocKy; // Lấy học kỳ đầu tiên (gần nhất)
                    }
                    else
                    {
                        // Không có học kỳ nào
                        thongKeCard1.TieuDe2 = "0";
                        thongKeCard2.TieuDe2 = "0";
                        thongKeCard2.TieuDe3 = "0% tổng số";
                        thongKeCard3.TieuDe2 = "0";
                        thongKeCard3.TieuDe3 = "0% tổng số";
                        thongKeCard4.TieuDe2 = "0";
                        thongKeCard4.TieuDe3 = "0% học sinh";
                        return;
                    }
                }

                // 1. Tổng khen thưởng trong học kỳ
                int tongKhenThuong = ktklDAO.DemKhenThuongTheoHocKy(maHocKyHienTai);
                thongKeCard1.TieuDe2 = tongKhenThuong.ToString();

                // 2. Cấp trường
                int soCapTruong = ktklDAO.DemKhenThuongTheoCapVaHocKy("Cấp trường", maHocKyHienTai);
                thongKeCard2.TieuDe2 = soCapTruong.ToString();

                // Tính % cấp trường trên tổng khen thưởng
                if (tongKhenThuong > 0)
                {
                    double phanTramCapTruong = (double)soCapTruong / tongKhenThuong * 100;
                    thongKeCard2.TieuDe3 = $"{phanTramCapTruong:F0}% tổng số";
                }
                else
                {
                    thongKeCard2.TieuDe3 = "0% tổng số";
                }

                // 3. Cấp tỉnh
                int soCapTinh = ktklDAO.DemKhenThuongTheoCapVaHocKy("Cấp tỉnh", maHocKyHienTai);
                thongKeCard3.TieuDe2 = soCapTinh.ToString();

                // Tính % cấp tỉnh trên tổng khen thưởng
                if (tongKhenThuong > 0)
                {
                    double phanTramCapTinh = (double)soCapTinh / tongKhenThuong * 100;
                    thongKeCard3.TieuDe3 = $"{phanTramCapTinh:F0}% tổng số";
                }
                else
                {
                    thongKeCard3.TieuDe3 = "0% tổng số";
                }

                // 4. Vi phạm kỷ luật (đếm số HỌC SINH vi phạm, không phải số lần vi phạm)
                int soHocSinhViPham = ktklDAO.DemHocSinhKyLuatTheoHocKy(maHocKyHienTai);
                int tongHocSinh = ktklDAO.DemTongHocSinhTheoHocKy(maHocKyHienTai);

                thongKeCard4.TieuDe2 = soHocSinhViPham.ToString();

                // Tính % học sinh vi phạm trên tổng học sinh
                if (tongHocSinh > 0)
                {
                    double phanTramViPham = (double)soHocSinhViPham / tongHocSinh * 100;
                    thongKeCard4.TieuDe3 = $"{phanTramViPham:F1}% học sinh";
                }
                else
                {
                    thongKeCard4.TieuDe3 = "0% học sinh";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thống kê: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            tbKhenThuong.Columns.Add("trangThaiKT", "Trạng thái");
            tbKhenThuong.Columns.Add("ngayKT", "Ngày");
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
            tbKhenThuong.RowTemplate.Height = 40; // Chiều cao mỗi dòng dữ liệu

            // 🔹 Padding nhẹ giữa các ô
            tbKhenThuong.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            tbKhenThuong.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            tbKhenThuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 🔹 Căn chỉnh riêng cho từng cột
            tbKhenThuong.Columns["hoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["thanhTich"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["capKhen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["trangThaiKT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["ngayKT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKhenThuong.Columns["thaoTac"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //btnEdit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //btnDelete.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            // 🔹 Thêm dữ liệu mẫu
            tbKhenThuong.Rows.Add("Nguyễn Văn An", "Giải Nhất Toán học cấp trường", "Cấp trường", "Đã duyệt", "15/10/2024");
            tbKhenThuong.Rows.Add("Trần Thị Bình", "Học sinh Giỏi", "Cấp trường", "Đã duyệt", "12/10/2024");
            tbKhenThuong.Rows.Add("Lê Hoàng Cường", "Giải Ba Tin học", "Cấp tỉnh", "Đã duyệt", "10/10/2024");
            tbKhenThuong.Rows.Add("Nguyễn Tuấn Tài", "Giải nhất Tin học", "Cấp tỉnh", "Chưa duyệt", "19/1/2024");




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
            //LoadKhenThuongData();
        }


        // 🌸 Hàm thiết kế giao diện cho bảng kỷ luật
        private void SetupKyLuatTable()
        {
            // 🔹 Xóa dữ liệu và cột cũ
            tbKyLuat.Rows.Clear();
            tbKyLuat.Columns.Clear();

            // 🔹 Thêm cột (Chỉ giữ lại cột text "thaoTacKL")
            tbKyLuat.Columns.Add("hocSinh", "Học sinh");
            tbKyLuat.Columns.Add("viPham", "Vi phạm");
            tbKyLuat.Columns.Add("xuLy", "Xử lý");
            tbKyLuat.Columns.Add("trangThaiKL", "Trạng thái");
            tbKyLuat.Columns.Add("ngayKL", "Ngày");
            tbKyLuat.Columns.Add("thaoTacKL", "Thao tác"); // <-- Cột này sẽ chứa 2 icon

            // 🎨 Thiết lập style tổng thể (GIỮ NGUYÊN NHƯ CODE CŨ CỦA BẠN)
            tbKyLuat.ThemeStyle.BackColor = Color.White;
            tbKyLuat.BackgroundColor = Color.White;
            tbKyLuat.BorderStyle = BorderStyle.None;
            tbKyLuat.CellBorderStyle = DataGridViewCellBorderStyle.None;
            tbKyLuat.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            tbKyLuat.RowHeadersVisible = false;
            tbKyLuat.GridColor = Color.FromArgb(230, 230, 230);
            tbKyLuat.EnableHeadersVisualStyles = false;

            // 🔹 Header (GIỮ NGUYÊN NHƯ CODE CŨ CỦA BẠN)
            tbKyLuat.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 252);
            tbKyLuat.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tbKyLuat.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tbKyLuat.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.ColumnHeadersHeight = 40;

            // 🔹 Dòng dữ liệu (GIỮ NGUYÊN NHƯ CODE CŨ CỦA BẠN)
            tbKyLuat.DefaultCellStyle.BackColor = Color.White;
            tbKyLuat.DefaultCellStyle.ForeColor = Color.Black;
            tbKyLuat.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            tbKyLuat.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            tbKyLuat.DefaultCellStyle.SelectionForeColor = Color.Black;
            tbKyLuat.RowTemplate.Height = 40; // Chiều cao mỗi dòng dữ liệu

            // 🔹 Padding và AutoSize
            tbKyLuat.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            tbKyLuat.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            tbKyLuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // --- THAY ĐỔI CHO CỘT THAO TÁC ---
            tbKyLuat.Columns["thaoTacKL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Không tự giãn
            tbKyLuat.Columns["thaoTacKL"].Width = 100; // Đặt độ rộng cố định (tăng/giảm nếu cần)
            tbKyLuat.Columns["thaoTacKL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa nội dung ô

            // 🔹 Căn chỉnh riêng cho từng cột (GIỮ NGUYÊN NHƯ CODE CŨ CỦA BẠN)
            tbKyLuat.Columns["hocSinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.Columns["viPham"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.Columns["xuLy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.Columns["trangThaiKL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbKyLuat.Columns["ngayKL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            // --- KHÔNG CẦN CĂN CHỈNH CHO CÁC CỘT ICON CŨ ---

            // 🔹 Thêm dữ liệu mẫu (Thêm "" cho cột thaoTacKL)
            tbKyLuat.Rows.Add("Nguyễn Văn B", "Đi học muộn 3 lần", "Nhắc nhở", "Cô Lan", "18/10/2024", "");
            tbKyLuat.Rows.Add("Trần Thị C", "Không làm bài tập", "Cảnh cáo", "Thầy Hùng", "16/10/2024", "");
            tbKyLuat.Rows.Add("Lê Văn D", "Gây gổ với bạn", "Khiển trách", "Hiệu trưởng", "14/10/2024", "");


            // 🔹 Bo góc và style khác (GIỮ NGUYÊN NHƯ CODE CŨ CỦA BẠN)
            tbKyLuat.ThemeStyle.RowsStyle.BackColor = Color.White;
            tbKyLuat.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            tbKyLuat.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            tbKyLuat.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
            tbKyLuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tbKyLuat.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 250, 252);
            tbKyLuat.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // 🔹 Đảm bảo màu header không đổi khi click (GIỮ NGUYÊN)
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
            tbKyLuat.MultiSelect = false;

            // 🌟 GẮN SỰ KIỆN VẼ VÀ CLICK CHO BẢNG KỶ LUẬT 🌟
            tbKyLuat.CellPainting += TbKyLuat_CellPainting;
            tbKyLuat.CellClick += TbKyLuat_CellClick;
            //LoadKyLuatData();

        }

     

        private void TbKyLuat_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbKyLuat.Columns["thaoTacKL"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                // ✅ Lấy permission - Sử dụng cách an toàn hơn
                bool canUpdate = true; // Mặc định true
                bool canDelete = true; // Mặc định true
                
                if (tbKyLuat.Tag != null)
                {
                    try
                    {
                        dynamic permissions = tbKyLuat.Tag;
                        canUpdate = permissions?.CanUpdate ?? true;
                        canDelete = permissions?.CanDelete ?? true;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        canUpdate = true;
                        canDelete = true;
                    }
                }

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Image editIcon = Properties.Resources.repair;
                Image deleteIcon = Properties.Resources.bin;

                Rectangle editRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);

                // ✅ Vẽ icon mờ nếu không có quyền
                if (canUpdate)
                {
                    e.Graphics.DrawImage(editIcon, editRect);
                }
                else
                {
                    DrawGrayScaleImage(e.Graphics, editIcon, editRect);
                }

                if (canDelete)
                {
                    e.Graphics.DrawImage(deleteIcon, deleteRect);
                }
                else
                {
                    DrawGrayScaleImage(e.Graphics, deleteIcon, deleteRect);
                }

                e.Handled = true;
            }
        }

        private void TbKyLuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbKyLuat.Columns["thaoTacKL"].Index)
            {
                Rectangle cellBounds = tbKyLuat.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Point clickPosInCell = tbKyLuat.PointToClient(Cursor.Position);
                int xClick = clickPosInCell.X - cellBounds.Left;

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startXInCell = (cellBounds.Width - totalWidth) / 2;

                int editIconEndX = startXInCell + iconSize;
                int deleteIconStartX = startXInCell + iconSize + spacing;
                int deleteIconEndX = deleteIconStartX + iconSize;

                string tenHS = tbKyLuat.Rows[e.RowIndex].Cells["hocSinh"].Value?.ToString() ?? "Học sinh này";
                int maKTKL = (int)tbKyLuat.Rows[e.RowIndex].Tag;

                if (xClick >= startXInCell && xClick < editIconEndX)
                {
                    // ✅ Kiểm tra quyền
                    if (!PermissionHelper.CheckDataGridIconPermission(tbKyLuat, "edit", "Quản lý đánh giá"))
                        return;

                    ThemDanhGia frm = new ThemDanhGia(maKTKL);
                    frm.StartPosition = FormStartPosition.CenterScreen;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        ApplyFilter();
                        MessageBox.Show("Đã cập nhật kỷ luật thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
                {
                    // ✅ Kiểm tra quyền
                    if (!PermissionHelper.CheckDataGridIconPermission(tbKyLuat, "delete", "Quản lý đánh giá"))
                        return;

                    if (MessageBox.Show($"Bạn có chắc muốn xóa kỷ luật của {tenHS}?",
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (ktklDAO.XoaKhenThuongKyLuat(maKTKL))
                        {
                            ApplyFilter();
                            MessageBox.Show("Đã xóa kỷ luật thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa kỷ luật!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }

        private void TbKhenThuong_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbKhenThuong.Columns["thaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                // ✅ Lấy permission - Sử dụng cách an toàn hơn
                bool canUpdate = true; // Mặc định true
                bool canDelete = true; // Mặc định true
                
                if (tbKhenThuong.Tag != null)
                {
                    try
                    {
                        dynamic permissions = tbKhenThuong.Tag;
                        canUpdate = permissions?.CanUpdate ?? true;
                        canDelete = permissions?.CanDelete ?? true;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        canUpdate = true;
                        canDelete = true;
                    }
                }

                int iconSize = 18;
                int padding = 4;

                int xEdit = e.CellBounds.Left + padding;
                int xDelete = e.CellBounds.Left + iconSize + 3 * padding;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Image editIcon = Properties.Resources.repair;
                Image deleteIcon = Properties.Resources.bin;

                // ✅ Vẽ icon mờ nếu không có quyền
                if (canUpdate)
                {
                    e.Graphics.DrawImage(editIcon, new Rectangle(xEdit, y, iconSize, iconSize));
                }
                else
                {
                    DrawGrayScaleImage(e.Graphics, editIcon, new Rectangle(xEdit, y, iconSize, iconSize));
                }

                if (canDelete)
                {
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(xDelete, y, iconSize, iconSize));
                }
                else
                {
                    DrawGrayScaleImage(e.Graphics, deleteIcon, new Rectangle(xDelete, y, iconSize, iconSize));
                }

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

                string tenHS = tbKhenThuong.Rows[e.RowIndex].Cells["hoTen"].Value?.ToString() ?? "Học sinh này";
                int maKTKL = (int)tbKhenThuong.Rows[e.RowIndex].Tag;

                if (x < editRight)
                {
                    // ✅ Kiểm tra quyền
                    if (!PermissionHelper.CheckDataGridIconPermission(tbKhenThuong, "edit", "Quản lý đánh giá"))
                        return;

                    ThemDanhGia frm = new ThemDanhGia(maKTKL);
                    frm.StartPosition = FormStartPosition.CenterScreen;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        ApplyFilter();
                        MessageBox.Show("Đã cập nhật khen thưởng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (x > deleteLeft && x < deleteLeft + iconSize)
                {
                    // ✅ Kiểm tra quyền
                    if (!PermissionHelper.CheckDataGridIconPermission(tbKhenThuong, "delete", "Quản lý đánh giá"))
                        return;

                    if (MessageBox.Show($"Bạn có chắc muốn xóa khen thưởng của {tenHS}?",
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (ktklDAO.XoaKhenThuongKyLuat(maKTKL))
                        {
                            ApplyFilter();
                            MessageBox.Show("Đã xóa khen thưởng thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa khen thưởng!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }


        // Load dữ liệu vào ComboBox học kỳ
        private void LoadHocKyComboBox()
        {
            try
            {
                cbHocKyNamHoc.Items.Clear();
                cbHocKyNamHoc.Items.Add("Tất cả học kỳ");

                List<HocKyDTO> dsHocKy = hocKyDAO.GetAllHocKy();

                foreach (HocKyDTO hk in dsHocKy)
                {
                    string displayText = $"{hk.TenHocKy}-{hk.MaNamHoc}";
                    cbHocKyNamHoc.Items.Add(new ThemDanhGia.ComboBoxItem
                    {
                        Text = displayText,
                        Value = hk.MaHocKy
                    });
                }

                cbHocKyNamHoc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải học kỳ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load dữ liệu vào ComboBox lớp
        private void LoadLopComboBox()
        {
            try
            {
                cbLop.Items.Clear();
                cbLop.Items.Add("Tất cả lớp");

                LopDAO lopDAO = new LopDAO();
                List<LopDTO> dsLop = lopDAO.GetDanhSachLopCoHocSinh();

                foreach (LopDTO lop in dsLop)
                {
                    cbLop.Items.Add(new ThemDanhGia.ComboBoxItem
                    {
                        Text = lop.TenLop,
                        Value = lop.MaLop
                    });
                }

                cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKhenThuongDataWithFilter()
        {
            try
            {
                tbKhenThuong.Rows.Clear();
                List<KhenThuongKyLuatDTO> dsKhenThuong = ktklBUS.LayDanhSachCoLoc(
                    "Khen thưởng", selectedMaHocKy, selectedMaLop, searchKeyword);

                foreach (var kt in dsKhenThuong)
                {
                    HocSinhDTO hs = hocSinhDAO.TimHocSinhTheoMa(kt.MaHocSinh);
                    string hoTen = hs != null ? hs.HoTen : "Không xác định";

                    tbKhenThuong.Rows.Add(
                        hoTen,
                        kt.NoiDung,
                        kt.CapKhenThuong ?? "",
                        kt.TrangThaiDuyet,
                        kt.NgayApDung.ToString("dd/MM/yyyy"),
                        ""
                    );

                    tbKhenThuong.Rows[tbKhenThuong.Rows.Count - 1].Tag = kt.MaKTKL;
                }

                cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        

        private void LoadKyLuatDataWithFilter()
        {
            try
            {
                tbKyLuat.Rows.Clear();
                List<KhenThuongKyLuatDTO> dsKyLuat = ktklBUS.LayDanhSachCoLoc(
                    "Kỷ luật", selectedMaHocKy, selectedMaLop, searchKeyword);

                foreach (var kl in dsKyLuat)
                {
                    HocSinhDTO hs = hocSinhDAO.TimHocSinhTheoMa(kl.MaHocSinh);
                    string hoTen = hs != null ? hs.HoTen : "Không xác định";

                    tbKyLuat.Rows.Add(
                        hoTen,
                        kl.NoiDung,
                        kl.MucXuLy ?? "",
                        kl.TrangThaiDuyet,
                        kl.NgayApDung.ToString("dd/MM/yyyy"),
                        ""
                    );

                    tbKyLuat.Rows[tbKyLuat.Rows.Count - 1].Tag = kl.MaKTKL;
                }

                cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        // Áp dụng filter
        private void ApplyFilter()
        {
            LoadKhenThuongDataWithFilter();
            LoadKyLuatDataWithFilter();
            CapNhatThongKe();
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

            btnKhenThuong.FillColor = Color.FromArgb(32, 136, 225);
            btnKhenThuong.ForeColor = Color.White;

            btnKyLuat.FillColor = Color.White;
            btnKyLuat.ForeColor = Color.Black;
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            tbKhenThuong.Visible = false;
            tbKyLuat.Visible = true;

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
            // ✅ Kiểm tra quyền thêm
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLDANHGIA, "Quản lý đánh giá"))
                return;

            ThemDanhGia frm = new ThemDanhGia();
            frm.StartPosition = FormStartPosition.CenterScreen;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter();
            }

        }



        private void thongKeCard1_Load_2(object sender, EventArgs e)
        {

        }

        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKyNamHoc.SelectedIndex == 0) // "Tất cả học kỳ"
            {
                selectedMaHocKy = -1;
            }
            else if (cbHocKyNamHoc.SelectedItem is ThemDanhGia.ComboBoxItem item)
            {
                selectedMaHocKy = Convert.ToInt32(item.Value);
            }

            ApplyFilter();
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLop.SelectedIndex == 0) // "Tất cả lớp"
            {
                selectedMaLop = -1;
            }
            else if (cbLop.SelectedItem is ThemDanhGia.ComboBoxItem item)
            {
                selectedMaLop = Convert.ToInt32(item.Value);
            }

            ApplyFilter();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckUpdatePermission(PermissionHelper.QLDANHGIA, "Quản lý đánh giá"))
                return;
            try
            {
                DataGridView currentTable = tbKhenThuong.Visible ? tbKhenThuong : tbKyLuat;
                string tenBang = tbKhenThuong.Visible ? "khen thưởng" : "kỷ luật";

                if (currentTable.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một đánh giá để duyệt!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow selectedRow = currentTable.SelectedRows[0];
                string hoTen = selectedRow.Cells[0].Value?.ToString() ?? "Học sinh này";
                int maKTKL = (int)selectedRow.Tag;

                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc muốn duyệt {tenBang} của {hoTen}?",
                    "Xác nhận duyệt",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    var result = ktklBUS.DuyetDanhGia(maKTKL, hoTen);

                    if (result.Success)
                    {
                        ApplyFilter();

                        // ⭐ TỰ ĐỘNG CẬP NHẬT HẠNH KIỂM SAU KHI DUYỆT KỶ LUẬT
                        if (!tbKhenThuong.Visible) // Chỉ khi đang ở tab Kỷ luật
                        {
                            CapNhatHanhKiemSauKhiDuyetKyLuat(maKTKL);
                        }
                    }

                    MessageBox.Show(result.Message, result.Success ? "Thông báo" : "Lỗi",
                        MessageBoxButtons.OK,
                        result.IsWarning ? MessageBoxIcon.Information : (result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi duyệt đánh giá: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tự động cập nhật hạnh kiểm sau khi duyệt kỷ luật
        /// </summary>
        private void CapNhatHanhKiemSauKhiDuyetKyLuat(int maKTKL)
        {
            try
            {
                // Lấy thông tin kỷ luật vừa duyệt
                KhenThuongKyLuatDTO kyLuat = ktklDAO.LayTheoMa(maKTKL);
                if (kyLuat == null) return;

                int maHocSinh = kyLuat.MaHocSinh;

                // Lấy tất cả học kỳ mà học sinh này có phân lớp
                PhanLopDAO phanLopDAO = new PhanLopDAO();
                var dsPhanLop = phanLopDAO.LayTatCaPhanLop()
                    .Where(pl => pl.maHocSinh == maHocSinh)
                    .Select(pl => pl.maHocKy)
                    .Distinct()
                    .ToList();

                if (dsPhanLop.Count == 0) return;

                // Tính lại hạnh kiểm cho từng học kỳ
                HanhKiemBUS hanhKiemBUS = new HanhKiemBUS();
                HanhKiemDAO hanhKiemDAO = new HanhKiemDAO();
                int soCapNhat = 0;

                foreach (int maHocKy in dsPhanLop)
                {
                    // Tính hạnh kiểm mới
                    string hanhKiemMoi = hanhKiemBUS.TinhHanhKiemTuDong(maHocSinh, maHocKy);

                    if (!string.IsNullOrEmpty(hanhKiemMoi))
                    {
                        // Lấy hạnh kiểm hiện tại (nếu có)
                        HanhKiemDTO hkHienTai = hanhKiemDAO.LayHanhKiem(maHocSinh, maHocKy);
                        string nhanXet = hkHienTai?.NhanXet ?? "";

                        // Lưu hạnh kiểm mới
                        HanhKiemDTO hkMoi = new HanhKiemDTO
                        {
                            MaHocSinh = maHocSinh,
                            MaHocKy = maHocKy,
                            XepLoai = hanhKiemMoi,
                            NhanXet = nhanXet
                        };

                        if (hanhKiemDAO.LuuHanhKiem(hkMoi))
                        {
                            soCapNhat++;
                        }
                    }
                }

                if (soCapNhat > 0)
                {
                    Console.WriteLine($"✅ Đã tự động cập nhật hạnh kiểm cho {soCapNhat} học kỳ của học sinh #{maHocSinh}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Lỗi khi tự động cập nhật hạnh kiểm: {ex.Message}");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchKeyword = txtSearch.Text.Trim();
            ApplyFilter();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Xác định bảng nào đang hiển thị
                DataGridView currentTable = tbKhenThuong.Visible ? tbKhenThuong : tbKyLuat;
                string tenBang = tbKhenThuong.Visible ? "Khen Thưởng" : "Kỷ Luật";

                // Kiểm tra có dữ liệu không
                if (currentTable.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu file Excel",
                    FileName = $"BaoCao_{tenBang.Replace(" ", "")}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // ✅ ClosedXML KHÔNG CẦN SET LICENSE
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add($"Báo cáo {tenBang}");

                        // === TIÊU ĐỀ BÁO CÁO ===
                        worksheet.Cell("A1").Value = $"BÁO CÁO DANH SÁCH {tenBang.ToUpper()}";
                        worksheet.Range("A1:F1").Merge();
                        worksheet.Cell("A1").Style
                            .Font.SetFontSize(16)
                            .Font.SetBold()
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                            .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        worksheet.Row(1).Height = 30;

                        // Thông tin bổ sung
                        worksheet.Cell("A2").Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                        worksheet.Range("A2:F2").Merge();
                        worksheet.Cell("A2").Style
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                            .Font.SetItalic();

                        // Thông tin lọc
                        string filterInfo = "Bộ lọc: ";
                        if (selectedMaHocKy != -1 && cbHocKyNamHoc.SelectedItem != null)
                        {
                            filterInfo += $"Học kỳ: {cbHocKyNamHoc.Text} | ";
                        }
                        if (selectedMaLop != -1 && cbLop.SelectedItem != null)
                        {
                            filterInfo += $"Lớp: {cbLop.Text} | ";
                        }
                        if (!string.IsNullOrWhiteSpace(searchKeyword))
                        {
                            filterInfo += $"Tìm kiếm: '{searchKeyword}'";
                        }
                        if (filterInfo == "Bộ lọc: ")
                        {
                            filterInfo = "Bộ lọc: Tất cả";
                        }

                        worksheet.Cell("A3").Value = filterInfo;
                        worksheet.Range("A3:F3").Merge();
                        worksheet.Cell("A3").Style
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                            .Font.SetItalic();
                        worksheet.Row(3).Height = 20;

                        // === HEADER ===
                        int startRow = 5;
                        int col = 1;

                        // Lấy tên cột (bỏ cột "Thao tác")
                        for (int i = 0; i < currentTable.Columns.Count; i++)
                        {
                            if (currentTable.Columns[i].Name == "thaoTac" ||
                                currentTable.Columns[i].Name == "thaoTacKL")
                                continue;

                            worksheet.Cell(startRow, col).Value = currentTable.Columns[i].HeaderText;
                            col++;
                        }

                        // Style cho header
                        var headerRange = worksheet.Range(startRow, 1, startRow, col - 1);
                        headerRange.Style
                            .Font.SetBold()
                            .Fill.SetBackgroundColor(XLColor.FromArgb(79, 129, 189))
                            .Font.SetFontColor(XLColor.White)
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                            .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                            .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                            .Border.SetInsideBorder(XLBorderStyleValues.Thin);
                        worksheet.Row(startRow).Height = 25;

                        // === DỮ LIỆU ===
                        int row = startRow + 1;
                        foreach (DataGridViewRow dgvRow in currentTable.Rows)
                        {
                            if (dgvRow.IsNewRow) continue;

                            col = 1;
                            for (int i = 0; i < currentTable.Columns.Count; i++)
                            {
                                if (currentTable.Columns[i].Name == "thaoTac" ||
                                    currentTable.Columns[i].Name == "thaoTacKL")
                                    continue;

                                var cellValue = dgvRow.Cells[i].Value;
                                worksheet.Cell(row, col).Value = cellValue?.ToString() ?? "";
                                col++;
                            }

                            // Style cho dòng dữ liệu
                            var dataRange = worksheet.Range(row, 1, row, col - 1);
                            dataRange.Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                            dataRange.Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);

                            // Màu xen kẽ cho dòng
                            if (row % 2 == 0)
                            {
                                dataRange.Style.Fill.SetBackgroundColor(XLColor.FromArgb(242, 242, 242));
                            }

                            row++;
                        }

                        // === TỔNG KẾT ===
                        int summaryRow = row + 1;
                        worksheet.Cell(summaryRow, 1).Value = $"Tổng số: {currentTable.Rows.Count} bản ghi";
                        worksheet.Range(summaryRow, 1, summaryRow, col - 1).Merge();
                        worksheet.Cell(summaryRow, 1).Style
                            .Font.SetBold()
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                        // === AUTO-FIT COLUMNS ===
                        for (int i = 1; i < col; i++)
                        {
                            worksheet.Column(i).AdjustToContents();
                            if (worksheet.Column(i).Width < 15)
                                worksheet.Column(i).Width = 15;
                            if (worksheet.Column(i).Width > 50)
                                worksheet.Column(i).Width = 50;
                        }

                        // Lưu file
                        workbook.SaveAs(saveDialog.FileName);

                        MessageBox.Show($"Xuất file Excel thành công!\n\nĐường dẫn:\n{saveDialog.FileName}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (MessageBox.Show("Bạn có muốn mở file Excel vừa xuất không?", "Xác nhận",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất Excel:\n{ex.Message}\n\nChi tiết:\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawGrayScaleImage(Graphics graphics, Image image, Rectangle rect)
        {
            var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
      new float[][] {
            new float[] {0.3f, 0.3f, 0.3f, 0, 0},
            new float[] {0.59f, 0.59f, 0.59f, 0, 0},
            new float[] {0.11f, 0.11f, 0.11f, 0, 0},
            new float[] {0, 0, 0, 0.3f, 0},
            new float[] {0, 0, 0, 0, 1}
      });

            // ✅ CHỈ ImageAttributes mới dùng using (vì implement IDisposable)
            using (var attributes = new System.Drawing.Imaging.ImageAttributes())
            {
                attributes.SetColorMatrix(grayScaleMatrix);
                graphics.DrawImage(image, rect, 0, 0, image.Width, image.Height,
                    GraphicsUnit.Pixel, attributes);
            }
        }

    }
}
