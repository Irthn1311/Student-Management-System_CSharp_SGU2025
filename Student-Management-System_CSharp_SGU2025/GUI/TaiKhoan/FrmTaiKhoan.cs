using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI.TaiKhoan;
using Student_Management_System_CSharp_SGU2025.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmTaiKhoan : UserControl
    {
        private NguoiDungBLL nguoiDungBLL;
        private PhanQuyenBUS phanQuyenBUS;
        public FrmTaiKhoan()
        {
            InitializeComponent();
            nguoiDungBLL = new NguoiDungBLL();
            phanQuyenBUS = new PhanQuyenBUS();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            SetupThongKeCards();
            SetupTaiKhoanTable();
            LoadTaiKhoanData();
            ApplyPermissions();
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Tài khoản
        /// </summary>
        private void ApplyPermissions()
        {
            try
            {
                // ✅ Kiểm tra quyền truy cập chức năng
                if (!PermissionHelper.HasAccessToFunction(PermissionHelper.QLTAIKHOAN))
                {
                    MessageBox.Show("Bạn không có quyền truy cập chức năng 'Quản lý tài khoản'!",
                                   "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Enabled = false;
                    return;
                }

                // ✅ Áp dụng phân quyền thông qua PermissionHelper (nhất quán với các form khác)
                PermissionHelper.ApplyPermissionTaiKhoan(
                    btnAddAcc,
                    btnVaiTro,
                    tbTaiKhoan
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng phân quyền: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTaiKhoanData()
        {
            try
            {
                List<NguoiDungDTO> danhSachTaiKhoan = nguoiDungBLL.GetAllNguoiDung();
                HienThiDanhSachTaiKhoan(danhSachTaiKhoan);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu tài khoản: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                // ✅ Lấy thông tin quyền từ Tag
                dynamic permissions = tbTaiKhoan.Tag;
                bool canUpdate = permissions?.CanUpdate ?? false;
                bool canDelete = permissions?.CanDelete ?? false;

                int iconSize = 16;
                int padding = 5;
                int xShield = e.CellBounds.Left + padding;
                int xLock = xShield + iconSize + 3 * padding;
                int xBin = xLock + iconSize + 3 * padding;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                // ✅ Vẽ icon shield (UPDATE) - chỉ hiển thị nếu có quyền
                Image shield = Image.FromFile(@"..\..\Images\shield.png");
                if (canUpdate)
                {
                    e.Graphics.DrawImage(shield, new Rectangle(xShield, y, iconSize, iconSize));
                }
                else
                {
                    // Vẽ icon mờ 30%
                    System.Drawing.Imaging.ImageAttributes imageAttr = new System.Drawing.Imaging.ImageAttributes();
                    System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix();
                    colorMatrix.Matrix33 = 0.3f; // Độ mờ 30%
                    imageAttr.SetColorMatrix(colorMatrix);

                    e.Graphics.DrawImage(shield,
                        new Rectangle(xShield, y, iconSize, iconSize),
                        0, 0, shield.Width, shield.Height,
                        GraphicsUnit.Pixel,
                        imageAttr);
                }

                // ✅ Vẽ icon lock (UPDATE) - luôn hiển thị
                Image lockIcon = Image.FromFile(@"..\..\Images\lock.png");
                if (canUpdate)
                {
                    e.Graphics.DrawImage(lockIcon, new Rectangle(xLock, y, iconSize, iconSize));
                }
                else
                {
                    // Vẽ icon mờ 30%
                    System.Drawing.Imaging.ImageAttributes imageAttr = new System.Drawing.Imaging.ImageAttributes();
                    System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix();
                    colorMatrix.Matrix33 = 0.3f;
                    imageAttr.SetColorMatrix(colorMatrix);

                    e.Graphics.DrawImage(lockIcon,
                        new Rectangle(xLock, y, iconSize, iconSize),
                        0, 0, lockIcon.Width, lockIcon.Height,
                        GraphicsUnit.Pixel,
                        imageAttr);
                }

                // ✅ Vẽ icon bin (DELETE) - chỉ hiển thị nếu có quyền
                Image bin = Image.FromFile(@"..\..\Images\bin.png");
                if (canDelete)
                {
                    e.Graphics.DrawImage(bin, new Rectangle(xBin, y, iconSize, iconSize));
                }
                else
                {
                    // Vẽ icon mờ 30%
                    System.Drawing.Imaging.ImageAttributes imageAttr = new System.Drawing.Imaging.ImageAttributes();
                    System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix();
                    colorMatrix.Matrix33 = 0.3f;
                    imageAttr.SetColorMatrix(colorMatrix);

                    e.Graphics.DrawImage(bin,
                        new Rectangle(xBin, y, iconSize, iconSize),
                        0, 0, bin.Width, bin.Height,
                        GraphicsUnit.Pixel,
                        imageAttr);
                }

                e.Handled = true;
            }
        }


        private void TbTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbTaiKhoan.Columns["thaoTac"].Index)
            {
                // ✅ Lấy thông tin quyền từ Tag
                dynamic permissions = tbTaiKhoan.Tag;
                bool canUpdate = permissions?.CanUpdate ?? false;
                bool canDelete = permissions?.CanDelete ?? false;

                var cell = tbTaiKhoan.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = tbTaiKhoan.PointToClient(Cursor.Position).X - cell.X;

                int iconSize = 16;
                int padding = 5;
                int shieldRight = padding + iconSize;
                int lockLeft = shieldRight + 3 * padding;
                int lockRight = lockLeft + iconSize;
                int binLeft = lockRight + 3 * padding;

                string tenTK = tbTaiKhoan.Rows[e.RowIndex].Cells["tenTaiKhoan"].Value.ToString();
                string trangThaiHienTai = tbTaiKhoan.Rows[e.RowIndex].Cells["trangThai"].Value.ToString();

                // ✅ 1. ICON SHIELD - SỬA VAI TRÒ (UPDATE)
                if (x < shieldRight)
                {
                    // Kiểm tra quyền UPDATE
                    if (!PermissionHelper.CheckUpdatePermission(PermissionHelper.QLTAIKHOAN, "Quản lý tài khoản"))
                        return;

                    try
                    {
                        NguoiDungDTO nguoiDung = nguoiDungBLL.GetNguoiDungByTenDangNhap(tenTK);

                        if (nguoiDung == null)
                        {
                            MessageBox.Show($"Không tìm thấy thông tin tài khoản '{tenTK}'!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string hoTen = tenTK;
                        string email = "";
                        string soDienThoai = "";
                        DateTime ngaySinh = DateTime.Now.AddYears(-18);
                        string gioiTinh = "Nam";

                        try
                        {
                            var hoSo = nguoiDungBLL.GetHoSoByTenDangNhap(tenTK);
                            if (hoSo != null)
                            {
                                hoTen = hoSo.HoTen ?? tenTK;
                                email = hoSo.Email ?? "";
                                soDienThoai = hoSo.SoDienThoai ?? "";
                                ngaySinh = hoSo.NgaySinh ?? DateTime.Now.AddYears(-18);
                                gioiTinh = hoSo.GioiTinh ?? "Nam";
                            }
                        }
                        catch { }

                        using (ThemTaiKhoan frmSua = new ThemTaiKhoan(
                            tenTK, hoTen, email, soDienThoai, ngaySinh, gioiTinh, nguoiDung.MaVaiTro))
                        {
                            if (frmSua.ShowDialog() == DialogResult.OK)
                            {
                                LoadTaiKhoanData();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi sửa vai trò: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // ✅ 2. ICON LOCK - KHÓA/MỞ KHÓA TÀI KHOẢN (UPDATE)
                else if (x > lockLeft && x < lockRight)
                {
                    // Kiểm tra quyền UPDATE
                    if (!PermissionHelper.CheckUpdatePermission(PermissionHelper.QLTAIKHOAN, "Quản lý tài khoản"))
                        return;

                    try
                    {
                        string trangThaiMoi = trangThaiHienTai == "Hoạt động" ? "Bị khóa" : "Hoạt động";
                        string thongBao = trangThaiMoi == "Bị khóa"
                            ? $"Bạn có chắc chắn muốn KHÓA tài khoản '{tenTK}'?\n⚠️ Người dùng sẽ không thể đăng nhập!"
                            : $"Bạn có chắc chắn muốn MỞ KHÓA tài khoản '{tenTK}'?";

                        DialogResult result = MessageBox.Show(thongBao, "Xác nhận",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            bool success = nguoiDungBLL.ToggleTrangThai(tenTK);

                            if (success)
                            {
                                LoadTaiKhoanData();
                                MessageBox.Show(
                                    $"Đã {(trangThaiMoi == "Bị khóa" ? "khóa" : "mở khóa")} tài khoản '{tenTK}' thành công!",
                                    "Thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật trạng thái thất bại!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // ✅ 3. ICON BIN - XÓA TÀI KHOẢN (DELETE)
                else if (x > binLeft && x < binLeft + iconSize)
                {
                    // Kiểm tra quyền DELETE
                    if (!PermissionHelper.CheckDeletePermission(PermissionHelper.QLTAIKHOAN, "Quản lý tài khoản"))
                        return;

                    try
                    {
                        DialogResult result = MessageBox.Show(
                            $"Bạn có chắc chắn muốn XÓA tài khoản '{tenTK}'?\n" +
                            $"⚠️ Hành động này KHÔNG THỂ HOÀN TÁC!",
                            "Xác nhận xóa",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            bool success = nguoiDungBLL.XoaNguoiDung(tenTK);

                            if (success)
                            {
                                LoadTaiKhoanData();
                                MessageBox.Show($"Đã xóa tài khoản '{tenTK}' thành công!", "Thành công",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Xóa tài khoản thất bại!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa tài khoản: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Hiển thị danh sách tài khoản đã lọc (dùng chung cho Load và Search)
        /// </summary>
        private void HienThiDanhSachTaiKhoan(List<NguoiDungDTO> danhSachTaiKhoan)
        {
            try
            {
                tbTaiKhoan.Rows.Clear();

                // ✅ Đếm số liệu thống kê
                int tongTaiKhoan = danhSachTaiKhoan.Count;
                int tongHoatDong = 0;
                int tongBiKhoa = 0;
                int tongAdmin = 0;

                foreach (var tk in danhSachTaiKhoan)
                {
                    // ✅ Định dạng thời gian đăng nhập cuối
                    string lastLogin = tk.LanDangNhapCuoi.HasValue
                        ? tk.LanDangNhapCuoi.Value.ToString("dd/MM/yyyy HH:mm")
                        : "Chưa đăng nhập";

                    // ✅ Thêm vào DataGridView
                    tbTaiKhoan.Rows.Add(
                        tk.TenDangNhap,
                        tk.VaiTro ?? "Chưa có vai trò",
                        tk.TrangThai ?? "Hoạt động",
                        lastLogin,
                        "" // Cột thao tác
                    );

                    // ✅ Đếm thống kê
                    if (tk.TrangThai == "Hoạt động") tongHoatDong++;
                    if (tk.TrangThai == "Bị khóa") tongBiKhoa++;

                    // ✅ KIỂM TRA CHÍNH XÁC MaVaiTro = "admin"
                    if (!string.IsNullOrEmpty(tk.MaVaiTro))
                    {
                        var danhSachMaVaiTro = tk.MaVaiTro.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                          .Select(v => v.Trim().ToLower())
                                                          .ToList();

                        if (danhSachMaVaiTro.Contains("admin"))
                        {
                            tongAdmin++;
                        }
                    }

                    // ✅ Tô màu cho vai trò
                    var row = tbTaiKhoan.Rows[tbTaiKhoan.Rows.Count - 1];
                    string vaiTro = tk.VaiTro ?? "";

                    if (vaiTro.ToLower().Contains("admin"))
                        row.Cells["vaiTro"].Style.ForeColor = Color.FromArgb(147, 51, 234);
                    else if (vaiTro.ToLower().Contains("giáo vụ"))
                        row.Cells["vaiTro"].Style.ForeColor = Color.FromArgb(59, 130, 246);
                    else if (vaiTro.ToLower().Contains("giáo viên"))
                        row.Cells["vaiTro"].Style.ForeColor = Color.FromArgb(34, 197, 94);
                    else if (vaiTro.ToLower().Contains("học sinh"))
                        row.Cells["vaiTro"].Style.ForeColor = Color.FromArgb(249, 115, 22);

                    // ✅ Tô màu cho trạng thái
                    string trangThai = tk.TrangThai ?? "Hoạt động";
                    if (trangThai == "Hoạt động")
                        row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(34, 197, 94);
                    else if (trangThai == "Bị khóa")
                        row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(239, 68, 68);
                }

                // ✅ Cập nhật thống kê
                statCardThongKeTaiKhoan1.TitleLietKe = tongTaiKhoan.ToString();
                statCardThongKeTaiKhoan2.TitleLietKe = tongHoatDong.ToString();
                statCardThongKeTaiKhoan3.TitleLietKe = tongBiKhoa.ToString();
                statCardThongKeTaiKhoan4.TitleLietKe = tongAdmin.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị danh sách tài khoản: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            ThemTaiKhoan themTaiKhoanForm = new ThemTaiKhoan();
            themTaiKhoanForm.StartPosition = FormStartPosition.CenterParent;
            themTaiKhoanForm.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadTaiKhoanData();
                    return;
                }

                List<NguoiDungDTO> ketQuaTimKiem = nguoiDungBLL.SearchNguoiDung(keyword);

                HienThiDanhSachTaiKhoan(ketQuaTimKiem);

                if (ketQuaTimKiem.Count == 0)
                {
                    // Có thể thêm label thông báo hoặc để trống
                    // lblThongBao.Text = $"Không tìm thấy kết quả cho '{keyword}'";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}