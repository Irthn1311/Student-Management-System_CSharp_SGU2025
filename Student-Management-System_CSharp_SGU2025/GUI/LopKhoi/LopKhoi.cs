using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_;
using Student_Management_System_CSharp_SGU2025.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class LopKhoi : UserControl
    {
        private LopHocBUS lopHocBUS;
        private GiaoVienBUS giaoVienBUS;
        private List<LopDTO> danhSachLopGoc;

        public LopKhoi()
        {
            InitializeComponent();
            lopHocBUS = new LopHocBUS();
            giaoVienBUS = new GiaoVienBUS();
            danhSachLopGoc = new List<LopDTO>();

            // Gắn sự kiện
            this.Load += LopKhoi_Load;
            SetupDataGridView();
        }

        private void LopKhoi_Load(object sender, EventArgs e)
        {
            if (dgvLop == null) return;

            // --- Cập nhật thống kê ---
            CapNhatThongKeKhoi();

            // SỬ DỤNG PROPERTY MỚI ĐỂ THAY ĐỔI MÀU
            statCardKhoi1.PanelColor = Color.FromArgb(59, 130, 246);
            statCardKhoi1.TextColor = Color.White;

            statCardKhoi2.PanelColor = Color.FromArgb(34, 197, 94);
            statCardKhoi2.TextColor = Color.White;

            statCardKhoi3.PanelColor = Color.FromArgb(249, 115, 22);
            statCardKhoi3.TextColor = Color.White;

            // ✅ GẮN SỰ KIỆN CLICK CHO CÁC STAT CARD
            statCardKhoi1.Click += StatCardKhoi1_Click;
            statCardKhoi2.Click += StatCardKhoi2_Click;
            statCardKhoi3.Click += StatCardKhoi3_Click;

            // ✅ Nếu statCard có panel con, cần gắn sự kiện cho tất cả controls
            GanSuKienClickChoTatCaControl(statCardKhoi1, StatCardKhoi1_Click);
            GanSuKienClickChoTatCaControl(statCardKhoi2, StatCardKhoi2_Click);
            GanSuKienClickChoTatCaControl(statCardKhoi3, StatCardKhoi3_Click);

            // --- Cấu hình & nạp dữ liệu ---
            LoadData();

            // --- Gắn sự kiện ---
            dgvLop.CellPainting += dgvLop_CellPainting;
            dgvLop.CellClick += dgvLop_CellClick;
            PermissionHelper.ApplyPermissionLopHoc(btnThem, dgvLop);
        }

        // ✅ HÀM HỖ TRỢ: Gắn sự kiện click cho tất cả controls con
        private void GanSuKienClickChoTatCaControl(Control parent, EventHandler clickHandler)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Click += clickHandler;
                if (ctrl.HasChildren)
                {
                    GanSuKienClickChoTatCaControl(ctrl, clickHandler);
                }
            }
        }

        // ✅ SỰ KIỆN CLICK CHO KHỐI 10
        private void StatCardKhoi1_Click(object sender, EventArgs e)
        {
            LocTheoKhoi(10);
            guna2ComboBox1.SelectedIndex = 1; // Set ComboBox về "Khối 10"
        }

        // ✅ SỰ KIỆN CLICK CHO KHỐI 11
        private void StatCardKhoi2_Click(object sender, EventArgs e)
        {
            LocTheoKhoi(11);
            guna2ComboBox1.SelectedIndex = 2; // Set ComboBox về "Khối 11"
        }

        // ✅ SỰ KIỆN CLICK CHO KHỐI 12
        private void StatCardKhoi3_Click(object sender, EventArgs e)
        {
            LocTheoKhoi(12);
            guna2ComboBox1.SelectedIndex = 3; // Set ComboBox về "Khối 12"
        }

        private void SetupDataGridView()
        {
            dgvLop.Columns.Clear();
            dgvLop.Rows.Clear();

            dgvLop.Columns.Add("MaLop", "Mã lớp");
            dgvLop.Columns.Add("TenLop", "Tên lớp");
            dgvLop.Columns.Add("Khoi", "Khối");
            dgvLop.Columns.Add("SiSo", "Sĩ số");
            dgvLop.Columns.Add("GVCN", "Giáo viên CN");
            dgvLop.Columns.Add("ThaoTac", "Thao tác");

            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvLop.Columns["ThaoTac"].Width = 60;
            dgvLop.Columns["ThaoTac"].Resizable = DataGridViewTriState.False;

            dgvLop.ColumnHeadersHeight = 50;

            dgvLop.Columns["MaLop"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["TenLop"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["Khoi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["SiSo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLop.Columns["GVCN"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvLop.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvLop.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204);
            dgvLop.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLop.EnableHeadersVisualStyles = false;
            dgvLop.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 102, 204);

            dgvLop.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvLop.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvLop.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvLop.RowTemplate.Height = 40;
            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLop.AllowUserToAddRows = false;
            dgvLop.ReadOnly = true;
        }

        // ✅ LOAD DỮ LIỆU: maLop được tự động sinh từ DB (auto-increment/trigger)
        private void LoadData()
        {
            try
            {
                danhSachLopGoc = lopHocBUS.DocDSLop(); // Lấy maLop tự động từ DB
                HienThiDanhSachLop(danhSachLopGoc);
                CapNhatThongKeKhoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nạp dữ liệu lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ HIỂN THỊ: Hiển thị maLop tự động từ DB
        private void HienThiDanhSachLop(List<LopDTO> danhSach)
        {
            dgvLop.Rows.Clear();

            foreach (LopDTO lop in danhSach)
            {
                string tenGVCN = "Chưa phân công";

                if (!string.IsNullOrEmpty(lop.maGVCN))
                {
                    try
                    {
                        string ten = giaoVienBUS.LayTenGiaoVienTheoMa(lop.maGVCN);
                        if (!string.IsNullOrEmpty(ten))
                        {
                            tenGVCN = ten;
                        }
                        else
                        {
                            tenGVCN = $"Không tìm thấy ({lop.maGVCN})";
                        }
                    }
                    catch
                    {
                        tenGVCN = $"Lỗi ({lop.maGVCN})";
                    }
                }

                dgvLop.Rows.Add(lop.maLop, lop.tenLop, $"Khối {lop.maKhoi}", lop.siSo, tenGVCN);
            }
        }

        private void LocTheoKhoi(int? maKhoi)
        {
            try
            {
                if (maKhoi == null)
                {
                    HienThiDanhSachLop(danhSachLopGoc);
                }
                else
                {
                    List<LopDTO> danhSachLoc = danhSachLopGoc
                        .Where(lop => lop.maKhoi == maKhoi.Value)
                        .ToList();

                    HienThiDanhSachLop(danhSachLoc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CẬP NHẬT THỐNG KÊ KHỐI
        private void CapNhatThongKeKhoi()
        {
            try
            {
                var ds = danhSachLopGoc ?? new List<LopDTO>();
                int soLopKhoi10 = ds.Count(l => l.maKhoi == 10);
                int soLopKhoi11 = ds.Count(l => l.maKhoi == 11);
                int soLopKhoi12 = ds.Count(l => l.maKhoi == 12);

                int siSoKhoi10 = ds.Where(l => l.maKhoi == 10).Sum(l => l.siSo);
                int siSoKhoi11 = ds.Where(l => l.maKhoi == 11).Sum(l => l.siSo);
                int siSoKhoi12 = ds.Where(l => l.maKhoi == 12).Sum(l => l.siSo);

                statCardKhoi1.SetData("Khối 10", $"{soLopKhoi10} lớp", $"{siSoKhoi10} học sinh");
                statCardKhoi2.SetData("Khối 11", $"{soLopKhoi11} lớp", $"{siSoKhoi11} học sinh");
                statCardKhoi3.SetData("Khối 12", $"{soLopKhoi12} lớp", $"{siSoKhoi12} học sinh");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLop_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvLop.Columns["ThaoTac"].Index)
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            // ✅ Lấy thông tin quyền từ Tag
            dynamic permissions = dgvLop.Tag;
            bool canUpdate = permissions?.CanUpdate ?? true; // Mặc định true nếu chưa set
            bool canDelete = permissions?.CanDelete ?? true;

            Image editIcon = Properties.Resources.edit_icon;
            Image deleteIcon = Properties.Resources.delete_icon;

            int iconSize = 20;
            int spacing = 10;
            int startX = e.CellBounds.Left + (e.CellBounds.Width - iconSize * 2 - spacing) / 2;
            int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

            // ✅ Vẽ icon Edit (với opacity nếu không có quyền)
            if (canUpdate)
            {
                e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize));
            }
            else
            {
                // Vẽ icon mờ (disabled)
                using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                {
                    float[][] matrixItems = {
                new float[] {0.3f, 0, 0, 0, 0},
                new float[] {0, 0.3f, 0, 0, 0},
                new float[] {0, 0, 0.3f, 0, 0},
                new float[] {0, 0, 0, 0.3f, 0},
                new float[] {0.5f, 0.5f, 0.5f, 0, 1}
            };
                    var colorMatrix = new System.Drawing.Imaging.ColorMatrix(matrixItems);
                    attributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default,
                                             System.Drawing.Imaging.ColorAdjustType.Bitmap);
                    e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize),
                                        0, 0, editIcon.Width, editIcon.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            // ✅ Vẽ icon Delete (với opacity nếu không có quyền)
            int deleteX = startX + iconSize + spacing;
            if (canDelete)
            {
                e.Graphics.DrawImage(deleteIcon, new Rectangle(deleteX, y, iconSize, iconSize));
            }
            else
            {
                // Vẽ icon mờ (disabled)
                using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                {
                    float[][] matrixItems = {
                new float[] {0.3f, 0, 0, 0, 0},
                new float[] {0, 0.3f, 0, 0, 0},
                new float[] {0, 0, 0.3f, 0, 0},
                new float[] {0, 0, 0, 0.3f, 0},
                new float[] {0.5f, 0.5f, 0.5f, 0, 1}
            };
                    var colorMatrix = new System.Drawing.Imaging.ColorMatrix(matrixItems);
                    attributes.SetColorMatrix(colorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default,
                                             System.Drawing.Imaging.ColorAdjustType.Bitmap);
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(deleteX, y, iconSize, iconSize),
                                        0, 0, deleteIcon.Width, deleteIcon.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            e.Handled = true;
        }

        // ✅ XỬ LÝ CLICK ICON - SỬA VÀ XÓA (maLop không thay đổi khi sửa, lấy từ DB)
        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvLop.Columns["ThaoTac"].Index)
                return;

            Point clickPoint = dgvLop.PointToClient(Cursor.Position);
            Rectangle cellRect = dgvLop.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            int iconSize = 18;
            int spacing = 10;
            int totalWidth = iconSize * 2 + spacing;
            int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;

            int maLop = Convert.ToInt32(dgvLop.Rows[e.RowIndex].Cells["MaLop"].Value);
            string tenLop = dgvLop.Rows[e.RowIndex].Cells["TenLop"].Value.ToString();

            // ✅ CLICK ICON SỬA
            if (clickPoint.X >= startX && clickPoint.X <= startX + iconSize)
            {
                // ✅ Kiểm tra quyền UPDATE
                if (!PermissionHelper.CheckDataGridIconPermission(dgvLop, "edit", "Quản lý lớp học"))
                    return;

                SuaLopHoc frm = new SuaLopHoc(maLop);
                frm.StartPosition = FormStartPosition.CenterParent;

                DialogResult result = frm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
            // ✅ CLICK ICON XÓA
            else if (clickPoint.X >= startX + iconSize + spacing && clickPoint.X <= startX + iconSize * 2 + spacing)
            {
                // ✅ Kiểm tra quyền DELETE
                if (!PermissionHelper.CheckDataGridIconPermission(dgvLop, "delete", "Quản lý lớp học"))
                    return;

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc muốn xóa lớp '{tenLop}'?\n\nLưu ý: Thao tác này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        bool kq = lopHocBUS.XoaLop(maLop);

                        if (kq)
                        {
                            MessageBox.Show("Xóa lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Xóa lớp học thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa lớp học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void statCardKhoi10_Load(object sender, EventArgs e)
        {

        }

        // ✅ THÊM MỚI: Không cần nhập maLop (DB tự sinh), reload để hiển thị maLop mới
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLLOPHOC, "Quản lý lớp học"))
                return;
            ThemLopHoc formThem = new ThemLopHoc(); // Form chỉ nhập tenLop, maKhoi, maGVCN (maLop tự động)

            DialogResult result = formThem.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadData(); // ✅ Reload và cập nhật thống kê, hiển thị maLop mới từ DB

                // Debug: Kiểm tra maLop mới nhất (có thể xóa sau khi test)
                var lopMoiNhat = danhSachLopGoc.OrderByDescending(l => l.maLop).FirstOrDefault();
                if (lopMoiNhat != null)
                {
                    // Console.WriteLine($"Mã lớp mới tự động: {lopMoiNhat.maLop}"); // Hoặc log vào file/debug
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = guna2ComboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedText))
                return;

            switch (selectedText)
            {
                case "Tất cả khối":
                    LocTheoKhoi(null);
                    break;

                case "Khối 10":
                    LocTheoKhoi(10);
                    break;

                case "Khối 11":
                    LocTheoKhoi(11);
                    break;

                case "Khối 12":
                    LocTheoKhoi(12);
                    break;

                default:
                    LocTheoKhoi(null);
                    break;
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LopKhoi_Load_1(object sender, EventArgs e)
        {

        }

        private void dgvLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}