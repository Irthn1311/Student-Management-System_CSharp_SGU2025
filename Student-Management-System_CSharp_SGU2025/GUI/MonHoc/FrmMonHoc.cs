using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmMonHoc : UserControl
    {
        private MonHocBUS monHocBUS;
        private List<MonHocDTO> danhSachMonHocGoc;

        // ✅ Định nghĩa các loại môn học
        private const string LOAI_MON_CHINH = "Môn chính";
        private const string LOAI_KHOA_HOC_TU_NHIEN = "Khoa học tự nhiên";
        private const string LOAI_KHOA_HOC_XA_HOI = "Khoa học xã hội";
        private const string LOAI_KY_NANG_KHAC = "Kỹ năng khác";

        public FrmMonHoc()
        {
            InitializeComponent();
            monHocBUS = new MonHocBUS();
            danhSachMonHocGoc = new List<MonHocDTO>();
        }

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            // ===================================
            // 1️⃣ CẤU HÌNH DATAGRIDVIEW
            // ===================================
            SetupDataGridView();

            // ===================================
            // 2️⃣ NẠP DỮ LIỆU
            // ===================================
            LoadData();

            // ===================================
            // 3️⃣ GẮN SỰ KIỆN
            // ===================================
            dgvMonHoc.CellPainting += dgvMonHoc_CellPainting;
            dgvMonHoc.CellClick += dgvMonHoc_CellClick;

            //// ✅ GẮN SỰ KIỆN CLICK CHO CÁC STAT CARD
            //statcardMonHoc1.Click += StatCardMonChinh_Click;
            //statcardMonHoc2.Click += StatCardKhoaHocTuNhien_Click;
            //statcardMonHoc3.Click += StatCardKhoaHocXaHoi_Click;
            //statcardMonHoc4.Click += StatCardKyNangKhac_Click;

            //// ✅ Gắn sự kiện cho tất cả controls con của StatCard
            //GanSuKienClickChoTatCaControl(statcardMonHoc1, StatCardMonChinh_Click);
            //GanSuKienClickChoTatCaControl(statcardMonHoc2, StatCardKhoaHocTuNhien_Click);
            //GanSuKienClickChoTatCaControl(statcardMonHoc3, StatCardKhoaHocXaHoi_Click);
            //GanSuKienClickChoTatCaControl(statcardMonHoc4, StatCardKyNangKhac_Click);

            // ===================================
            // 4️⃣ CẬP NHẬT STAT CARD
            // ===================================
            CapNhatStatCards();
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

        // ✅ SỰ KIỆN CLICK CHO MÔN CHÍNH
        private void StatCardMonChinh_Click(object sender, EventArgs e)
        {
            var monChinh = danhSachMonHocGoc.Where(m => m.ghiChu == LOAI_MON_CHINH).ToList();
            HienThiDanhSachMonHoc(monChinh);
        }

        // ✅ SỰ KIỆN CLICK CHO KHOA HỌC TỰ NHIÊN
        private void StatCardKhoaHocTuNhien_Click(object sender, EventArgs e)
        {
            var khoaHocTuNhien = danhSachMonHocGoc.Where(m => m.ghiChu == LOAI_KHOA_HOC_TU_NHIEN).ToList();
            HienThiDanhSachMonHoc(khoaHocTuNhien);
        }

        // ✅ SỰ KIỆN CLICK CHO KHOA HỌC XÃ HỘI
        private void StatCardKhoaHocXaHoi_Click(object sender, EventArgs e)
        {
            var khoaHocXaHoi = danhSachMonHocGoc.Where(m => m.ghiChu == LOAI_KHOA_HOC_XA_HOI).ToList();
            HienThiDanhSachMonHoc(khoaHocXaHoi);
        }

        // ✅ SỰ KIỆN CLICK CHO KỸ NĂNG KHÁC
        private void StatCardKyNangKhac_Click(object sender, EventArgs e)
        {
            var kyNangKhac = danhSachMonHocGoc.Where(m => m.ghiChu == LOAI_KY_NANG_KHAC).ToList();
            HienThiDanhSachMonHoc(kyNangKhac);
        }

        // ✅ CẤU HÌNH DATAGRIDVIEW
        private void SetupDataGridView()
        {
            dgvMonHoc.AutoGenerateColumns = false;
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.ReadOnly = true;
            dgvMonHoc.RowTemplate.Height = 40;

            // Style cho tiêu đề cột
            dgvMonHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvMonHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 136, 229);
            dgvMonHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMonHoc.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 136, 229);
            dgvMonHoc.ColumnHeadersHeight = 40;
            dgvMonHoc.EnableHeadersVisualStyles = false;

            // Style cho các dòng dữ liệu
            dgvMonHoc.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvMonHoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvMonHoc.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        // ✅ LOAD DỮ LIỆU CHÍNH
        private void LoadData()
        {
            try
            {
                danhSachMonHocGoc = monHocBUS.DocDSMH();
                HienThiDanhSachMonHoc(danhSachMonHocGoc);
                CapNhatStatCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ HIỂN THỊ DANH SÁCH MÔN HỌC
        private void HienThiDanhSachMonHoc(List<MonHocDTO> danhSach)
        {
            dgvMonHoc.Rows.Clear();

            foreach (MonHocDTO mh in danhSach)
            {
                dgvMonHoc.Rows.Add(
                    mh.maMon.ToString(),
                    mh.tenMon,
                    $"{mh.soTiet} tiết",
                    mh.ghiChu
                );
            }
        }

        // ✅ CẬP NHẬT STAT CARDS ĐỘNG - DỰA TRÊN TRƯỜNG GhiChu
        private void CapNhatStatCards()
        {
            try
            {
                // Phân loại môn học theo GhiChu
                var monChinh = danhSachMonHocGoc.Where(m => m.ghiChu == LOAI_MON_CHINH).ToList();
                var khoaHocTuNhien = danhSachMonHocGoc.Where(m => m.ghiChu == LOAI_KHOA_HOC_TU_NHIEN).ToList();
                var khoaHocXaHoi = danhSachMonHocGoc.Where(m => m.ghiChu == LOAI_KHOA_HOC_XA_HOI).ToList();
                var kyNangKhac = danhSachMonHocGoc.Where(m => m.ghiChu == LOAI_KY_NANG_KHAC).ToList();

                // Card 1 - Môn chính
                statcardMonHoc1.SetData(
                    monChinh.Count.ToString(),
                    LOAI_MON_CHINH,
                    monChinh.Count > 0 ? string.Join(", ", monChinh.Select(m => m.tenMon)) : "Chưa có môn học"
                );
                statcardMonHoc1.PanelBackgroundColor = Color.FromArgb(219, 234, 254);
                statcardMonHoc1.SoLuongForeColor = Color.FromArgb(30, 136, 229);
                statcardMonHoc1.Cursor = Cursors.Hand;

                // Card 2 - Khoa học tự nhiên
                statcardMonHoc2.SetData(
                    khoaHocTuNhien.Count.ToString(),
                    LOAI_KHOA_HOC_TU_NHIEN,
                    khoaHocTuNhien.Count > 0 ? string.Join(", ", khoaHocTuNhien.Select(m => m.tenMon)) : "Chưa có môn học"
                );
                statcardMonHoc2.PanelBackgroundColor = Color.FromArgb(220, 252, 231);
                statcardMonHoc2.SoLuongForeColor = Color.FromArgb(22, 163, 74);
                statcardMonHoc2.Cursor = Cursors.Hand;

                // Card 3 - Khoa học xã hội
                statcardMonHoc3.SetData(
                    khoaHocXaHoi.Count.ToString(),
                    LOAI_KHOA_HOC_XA_HOI,
                    khoaHocXaHoi.Count > 0 ? string.Join(", ", khoaHocXaHoi.Select(m => m.tenMon)) : "Chưa có môn học"
                );
                statcardMonHoc3.PanelBackgroundColor = Color.FromArgb(255, 237, 213);
                statcardMonHoc3.SoLuongForeColor = Color.FromArgb(234, 88, 12);
                statcardMonHoc3.Cursor = Cursors.Hand;

                // Card 4 - Kỹ năng khác
                statcardMonHoc4.SetData(
                    kyNangKhac.Count.ToString(),
                    LOAI_KY_NANG_KHAC,
                    kyNangKhac.Count > 0 ? string.Join(", ", kyNangKhac.Select(m => m.tenMon)) : "Chưa có môn học"
                );
                statcardMonHoc4.PanelBackgroundColor = Color.FromArgb(243, 243, 255);
                statcardMonHoc4.SoLuongForeColor = Color.FromArgb(147, 51, 234);
                statcardMonHoc4.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ VẼ ICON TRONG CỘT THAO TÁC
        private void dgvMonHoc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvMonHoc.Columns["TuyChinh"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                Image editIcon = Properties.Resources.edit_icon;
                Image deleteIcon = Properties.Resources.delete_icon;

                int iconSize = 20;
                int spacing = 10;
                int totalWidth = iconSize * 2 + spacing;

                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize));
                e.Graphics.DrawImage(deleteIcon, new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize));

                e.Handled = true;
            }
        }

        // ✅ XỬ LÝ CLICK ICON (SỬA VÀ XÓA)
        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dgvMonHoc.Columns["TuyChinh"].Index)
                return;

            Point clickPoint = dgvMonHoc.PointToClient(Cursor.Position);
            Rectangle cellRect = dgvMonHoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            int iconSize = 20;
            int spacing = 10;
            int totalWidth = iconSize * 2 + spacing;
            int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;

            int xEdit = startX;
            int xDelete = startX + iconSize + spacing;

            int maMon = Convert.ToInt32(dgvMonHoc.Rows[e.RowIndex].Cells["MaMon"].Value);
            string tenMon = dgvMonHoc.Rows[e.RowIndex].Cells["TenMon"].Value.ToString();

            // ✅ CLICK ICON SỬA
            if (clickPoint.X >= xEdit && clickPoint.X <= xEdit + iconSize)
            {
                FrmSuaMonHoc formSua = new FrmSuaMonHoc(maMon);
                formSua.StartPosition = FormStartPosition.CenterParent;

                DialogResult result = formSua.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
            // ✅ CLICK ICON XÓA
            else if (clickPoint.X >= xDelete && clickPoint.X <= xDelete + iconSize)
            {
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa môn học:\n\n" +
                    $"Mã môn: {maMon}\n" +
                    $"Tên môn: {tenMon}\n\n" +
                    $"⚠️ Hành động này không thể hoàn tác!",
                    "Xác nhận xóa môn học",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        bool kq = monHocBUS.DeleteMonHoc(maMon);

                        if (kq)
                        {
                            MessageBox.Show(
                                $"✓ Đã xóa môn học '{tenMon}' thành công!",
                                "Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show(
                                $"✗ Không thể xóa môn học '{tenMon}'!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ✅ NÚT THÊM MÔN HỌC
        private void btnThemMonHoc_Click(object sender, EventArgs e)
        {
            FrmThemMonHoc formThem = new FrmThemMonHoc();
            formThem.StartPosition = FormStartPosition.CenterParent;

            DialogResult result = formThem.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void statcardMonHoc2_Load(object sender, EventArgs e)
        {

        }

        private void statcardMonHoc1_Load(object sender, EventArgs e)
        {

        }

        private void panelMonHoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvMonHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}