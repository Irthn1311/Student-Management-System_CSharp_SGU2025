using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_;

namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    public partial class PhanCongGiangDay : UserControl
    {
        private PhanCongGiangDayBUS phanCongBUS;
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;

        public PhanCongGiangDay()
        {
            InitializeComponent();
            phanCongBUS = new PhanCongGiangDayBUS();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
        }

        private void PhanCongGiangDay_Load(object sender, EventArgs e)
        {
            try
            {
                LoadStatCards();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatCards()
        {
            try
            {
                // Lấy dữ liệu thống kê
                List<PhanCongGiangDayDTO> dsPhanCong = phanCongBUS.DocDSPhanCong();
                int tongPhanCong = dsPhanCong?.Count ?? 0;

                // Đếm giáo viên được phân công
                int tongGiaoVien = dsPhanCong?.Select(pc => pc.MaGiaoVien).Distinct().Count() ?? 0;

                // Đếm môn học được phân công
                int tongMonHoc = dsPhanCong?.Select(pc => pc.MaMonHoc).Distinct().Count() ?? 0;

                // Đếm lớp học có phân công
                int tongLopHoc = dsPhanCong?.Select(pc => pc.MaLop).Distinct().Count() ?? 0;

                // Cập nhật các card
                statCardPhanCongGiangDay1.Title = "Tổng phân công";
                statCardPhanCongGiangDay1.Value = tongPhanCong.ToString();
                statCardPhanCongGiangDay1.TitleColor = Color.FromArgb(30, 136, 229);

                statCardPhanCongGiangDay2.Title = "Giáo viên";
                statCardPhanCongGiangDay2.Value = tongGiaoVien.ToString();
                statCardPhanCongGiangDay2.TitleColor = Color.FromArgb(30, 136, 229);

                statCardPhanCongGiangDay3.Title = "Môn học";
                statCardPhanCongGiangDay3.Value = tongMonHoc.ToString();
                statCardPhanCongGiangDay3.TitleColor = Color.FromArgb(20, 163, 74);

                statCardPhanCongGiangDay4.Title = "Lớp học";
                statCardPhanCongGiangDay4.Value = tongLopHoc.ToString();
                statCardPhanCongGiangDay4.TitleColor = Color.FromArgb(234, 88, 12);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // Cấu hình DataGridView
                dgvPhanCong.Columns.Clear();
                dgvPhanCong.Rows.Clear();
                dgvPhanCong.AutoGenerateColumns = false;
                dgvPhanCong.AllowUserToAddRows = false;
                dgvPhanCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPhanCong.ReadOnly = true;

                // Thiết lập giao diện
                dgvPhanCong.BackgroundColor = Color.White;
                dgvPhanCong.BorderStyle = BorderStyle.None;
                dgvPhanCong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvPhanCong.RowHeadersVisible = false;

                // Style cho tiêu đề cột
                dgvPhanCong.EnableHeadersVisualStyles = false;
                dgvPhanCong.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvPhanCong.ColumnHeadersHeight = 50;
                dgvPhanCong.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                dgvPhanCong.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                dgvPhanCong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                dgvPhanCong.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
                dgvPhanCong.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Style cho các dòng dữ liệu
                dgvPhanCong.RowTemplate.Height = 45;
                dgvPhanCong.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
                dgvPhanCong.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
                dgvPhanCong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
                dgvPhanCong.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
                dgvPhanCong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPhanCong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

                // Tạo cột
                dgvPhanCong.Columns.Add("MaPhanCong", "Mã");
                dgvPhanCong.Columns["MaPhanCong"].Visible = false; // Ẩn cột mã

                dgvPhanCong.Columns.Add("GiaoVien", "Giáo viên");
                dgvPhanCong.Columns.Add("MonHoc", "Môn học");
                dgvPhanCong.Columns.Add("Lop", "Lớp");
                dgvPhanCong.Columns.Add("HocKy", "Học kỳ");
                dgvPhanCong.Columns.Add("ThoiGian", "Thời gian");
                dgvPhanCong.Columns.Add("ThaoTac", "Thao tác");

                // Thiết lập chế độ co giãn
                dgvPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPhanCong.Columns["ThaoTac"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvPhanCong.Columns["ThaoTac"].Width = 100;

                // Lấy dữ liệu từ database
                List<PhanCongGiangDayDTO> dsPhanCong = phanCongBUS.DocDSPhanCong();

                if (dsPhanCong != null && dsPhanCong.Count > 0)
                {
                    foreach (PhanCongGiangDayDTO pc in dsPhanCong)
                    {
                        // Lấy thông tin giáo viên
                        GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(pc.MaGiaoVien);
                        string tenGV = gv != null ? gv.HoTen : pc.MaGiaoVien;

                        // Lấy thông tin môn học
                        MonHocDTO mh = monHocBUS.LayDSMonHocTheoId(pc.MaMonHoc);
                        string tenMH = mh != null ? mh.tenMon : $"MH-{pc.MaMonHoc}";

                        // Lấy thông tin lớp
                        LopDTO lop = lopHocBUS.LayLopTheoId(pc.MaLop);
                        string tenLop = lop != null ? lop.tenLop : $"Lớp-{pc.MaLop}";

                        // Lấy thông tin học kỳ
                        HocKyDTO hk = hocKyBUS.LayHocKyTheoMa(pc.MaHocKy);
                        string tenHK = hk != null ? hk.TenHocKy : $"HK-{pc.MaHocKy}";

                        // Định dạng thời gian
                        string thoiGian = $"{pc.NgayBatDau:dd/MM/yyyy} - {pc.NgayKetThuc:dd/MM/yyyy}";

                        dgvPhanCong.Rows.Add(
                            pc.MaPhanCong,
                            tenGV,
                            tenMH,
                            tenLop,
                            tenHK,
                            thoiGian,
                            ""
                        );
                    }
                }

                // Gắn sự kiện
                dgvPhanCong.CellPainting += dgvPhanCong_CellPainting;
                dgvPhanCong.CellClick += dgvPhanCong_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu bảng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPhanCong_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Tô màu cho cột "Lớp"
            if (dgvPhanCong.Columns[e.ColumnIndex].Name == "Lop" && e.RowIndex >= 0)
            {
                string lopText = e.Value?.ToString();
                if (!string.IsNullOrEmpty(lopText))
                {
                    if (lopText.Contains("10"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(59, 130, 246);
                    }
                    else if (lopText.Contains("11"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(34, 197, 94);
                    }
                    else if (lopText.Contains("12"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(249, 115, 22);
                    }
                }
            }

            // Vẽ icon trong cột "ThaoTac"
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPhanCong.Columns["ThaoTac"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                try
                {
                    Image editIcon = Properties.Resources.icon_eye;
                    Image deleteIcon = Properties.Resources.delete_icon;

                    int iconSize = 20;
                    int iconEyeSize = 26;
                    int padding = 6;

                    int xEdit = e.CellBounds.Left + padding;
                    int xDelete = xEdit + iconEyeSize + (4 * padding);
                    int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;
                    int yEye = e.CellBounds.Top + (e.CellBounds.Height - iconEyeSize) / 2;

                    e.Graphics.DrawImage(editIcon, new Rectangle(xEdit, yEye, iconEyeSize, iconEyeSize));
                    e.Graphics.DrawImage(deleteIcon, new Rectangle(xDelete, y, iconSize, iconSize));
                }
                catch { }

                e.Handled = true;
            }
        }

        private void dgvPhanCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPhanCong.Columns["ThaoTac"].Index)
            {
                var cell = dgvPhanCong.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = dgvPhanCong.PointToClient(Cursor.Position).X - cell.X;

                int iconEyeSize = 26;
                int padding = 6;

                int eyeRight = padding + iconEyeSize;
                int deleteLeft = eyeRight + (4 * padding);

                int maPhanCong = Convert.ToInt32(dgvPhanCong.Rows[e.RowIndex].Cells["MaPhanCong"].Value);
                string tenGV = dgvPhanCong.Rows[e.RowIndex].Cells["GiaoVien"].Value.ToString();

                if (x < eyeRight)
                {
                    // XEM CHI TIẾT
                    XemChiTietPhanCong(maPhanCong);
                }
                else if (x > deleteLeft)
                {
                    // XÓA PHÂN CÔNG
                    XoaPhanCong(maPhanCong, tenGV, e.RowIndex);
                }
            }
        }

        private void XemChiTietPhanCong(int maPhanCong)
        {
            try
            {
                PhanCongGiangDayDTO pc = phanCongBUS.LayPhanCongTheoMa(maPhanCong);

                if (pc != null)
                {
                    // Lấy thông tin chi tiết
                    GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(pc.MaGiaoVien);
                    MonHocDTO mh = monHocBUS.LayDSMonHocTheoId(pc.MaMonHoc);
                    LopDTO lop = lopHocBUS.LayLopTheoId(pc.MaLop);
                    HocKyDTO hk = hocKyBUS.LayHocKyTheoMa(pc.MaHocKy);

                    string thongTin = $"📚 THÔNG TIN PHÂN CÔNG GIẢNG DẠY\n\n" +
                                    $"🔑 Mã phân công: {pc.MaPhanCong}\n" +
                                    $"👨‍🏫 Giáo viên: {(gv != null ? gv.HoTen : pc.MaGiaoVien)}\n" +
                                    $"📖 Môn học: {(mh != null ? mh.tenMon : $"MH-{pc.MaMonHoc}")}\n" +
                                    $"🏫 Lớp: {(lop != null ? lop.tenLop : $"Lớp-{pc.MaLop}")}\n" +
                                    $"📅 Học kỳ: {(hk != null ? hk.TenHocKy : $"HK-{pc.MaHocKy}")}\n" +
                                    $"📅 Ngày bắt đầu: {pc.NgayBatDau:dd/MM/yyyy}\n" +
                                    $"📅 Ngày kết thúc: {pc.NgayKetThuc:dd/MM/yyyy}";

                    MessageBox.Show(thongTin, "Chi tiết phân công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin phân công!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaPhanCong(int maPhanCong, string tenGV, int rowIndex)
        {
            try
            {
                string thongTinXoa = $"Bạn có chắc chắn muốn xóa phân công này?\n\n" +
                                    $"👨‍🏫 Giáo viên: {tenGV}\n" +
                                    $"🔑 Mã: {maPhanCong}\n\n" +
                                    $"⚠️ CẢNH BÁO:\n" +
                                    $"• Thao tác này sẽ xóa vĩnh viễn phân công\n" +
                                    $"• KHÔNG THỂ HOÀN TÁC sau khi xóa!\n\n" +
                                    $"Bạn có muốn tiếp tục?";

                DialogResult result = MessageBox.Show(
                    thongTinXoa,
                    "⚠️ Xác nhận xóa phân công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

                if (result == DialogResult.Yes)
                {
                    bool xoaThanhCong = phanCongBUS.XoaPhanCong(maPhanCong);

                    if (xoaThanhCong)
                    {
                        dgvPhanCong.Rows.RemoveAt(rowIndex);
                        LoadStatCards(); // Cập nhật thống kê

                        MessageBox.Show(
                            $"✓ Đã xóa phân công của '{tenGV}' thành công!",
                            "Xóa thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            $"✗ Không thể xóa phân công!\n\nVui lòng kiểm tra lại!",
                            "Lỗi xóa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi khi xóa phân công!\n\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                try { LoadData(); } catch { }
            }
        }

        //private void btnThemPhanCong_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (FrmThemPhanCongGiangDay frm = new FrmThemPhanCongGiangDay())
        //        {
        //            if (frm.ShowDialog() == DialogResult.OK)
        //            {
        //                LoadData();
        //                LoadStatCards();
        //                MessageBox.Show("Thêm phân công thành công!", "Thành công",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void dgvPhanCong_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void panelShow_Paint(object sender, PaintEventArgs e) { }
        private void panelPhanCongGiangDay_Paint(object sender, PaintEventArgs e) { }

        private void btnPhanCongMoi_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmThemPhanCongGiangDay frm = new FrmThemPhanCongGiangDay())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        LoadStatCards();
                        MessageBox.Show("Thêm phân công thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAutoPhanCong_Click(object sender, EventArgs e)
        {
            try
            {
                var uc = new Student_Management_System_CSharp_SGU2025.GUI.PhanCong.ucAutoPhanCongPreview();
                uc.Dock = DockStyle.Fill;
                using (var frm = new Form())
                {
                    frm.Text = "Auto Phân công (Preview)";
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.Size = new Size(900, 600);
                    frm.Controls.Add(uc);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở Auto Phân công: {ex.Message}");
            }
        }

        private void btnNhapDeXuat_Click(object sender, EventArgs e)
        {
            try
            {
                var persist = new Student_Management_System_CSharp_SGU2025.Services.AssignmentPersistService();
                persist.AcceptToOfficial(1);
                LoadData();
                LoadStatCards();
                MessageBox.Show("Đã nhập từ đề xuất vào PhanCongGiangDay.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể nhập đề xuất: {ex.Message}");
            }
        }
    }
}