using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
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
    public partial class ucBaoCao : UserControl
    {
        private HocKyDAO hocKyDAO;
        private LopHocBUS lopHocBUS;
        private PhanLopDAO phanLopDAO;
        private GiaoVienDAO giaoVienDAO;
        private ucBaoCaoBangDiem ucBangDiem;
        private ucBaoCaoThongKeHocLuc ucThongKeHocLuc;

        public ucBaoCao()
        {
            InitializeComponent();
            hocKyDAO = new HocKyDAO();
            lopHocBUS = new LopHocBUS();
            phanLopDAO = new PhanLopDAO();
            giaoVienDAO = new GiaoVienDAO();

            SetupCardHoverEffects();
            InitializeUserControls();
            AttachEventHandlers();
            LoadHocKyToCombobox();
        }

        /// <summary>
        /// Load danh sách học kỳ vào combobox theo định dạng "TenHocKy - MaNamHoc"
        /// </summary>
        private void LoadHocKyToCombobox()
        {
            try
            {
                // Lấy danh sách học kỳ từ database (đã sắp xếp theo thứ tự mới nhất)
                List<HocKyDTO> dsHocKy = hocKyDAO.DocDSHocKy();

                // Xóa dữ liệu cũ trong combobox
                cboHocKy.Items.Clear();
                cboHocKy.DisplayMember = "Text";
                cboHocKy.ValueMember = "Value";

                // Tạo danh sách các item để thêm vào combobox
                var itemsToAdd = new List<dynamic>();

                // Tìm học kỳ mới nhất có dữ liệu
                int indexHocKyMoiNhatCoDuLieu = -1;

                for (int i = 0; i < dsHocKy.Count; i++)
                {
                    HocKyDTO hocKy = dsHocKy[i];
                    string displayText = $"{hocKy.TenHocKy} - {hocKy.MaNamHoc}";

                    var item = new { Text = displayText, Value = hocKy.MaHocKy };
                    itemsToAdd.Add(item);

                    // Kiểm tra học kỳ này có dữ liệu không
                    if (indexHocKyMoiNhatCoDuLieu == -1 && hocKyDAO.KiemTraHocKyCoXepLoai(hocKy.MaHocKy))
                    {
                        indexHocKyMoiNhatCoDuLieu = i;
                    }
                }

                // Thêm tất cả items vào combobox
                foreach (var item in itemsToAdd)
                {
                    cboHocKy.Items.Add(item);
                }

                // Chọn học kỳ mới nhất có dữ liệu, nếu không có thì chọn học kỳ mới nhất
                if (cboHocKy.Items.Count > 0)
                {
                    if (indexHocKyMoiNhatCoDuLieu >= 0)
                    {
                        cboHocKy.SelectedIndex = indexHocKyMoiNhatCoDuLieu;
                    }
                    else
                    {
                        cboHocKy.SelectedIndex = 0; // Chọn học kỳ mới nhất (dù chưa có dữ liệu)
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu học kỳ: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load danh sách lớp theo học kỳ được chọn
        /// </summary>
        private void LoadDanhSachLopTheoHocKy()
        {
            try
            {
                // Xóa tất cả item cũ trong container
                pnlClassesContainer.Controls.Clear();

                // Kiểm tra học kỳ được chọn
                if (cboHocKy.SelectedItem == null)
                {
                    return;
                }

                dynamic selectedHocKy = cboHocKy.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                // Lấy danh sách lớp theo học kỳ (thông qua bảng PhanLop)
                List<LopDTO> dsLop = lopHocBUS.DocDSLop();

                // Lọc các lớp có học sinh trong học kỳ này
                var dsLopCoHocSinh = new List<(LopDTO lop, int siSo)>();

                foreach (var lop in dsLop)
                {
                    // Đếm số học sinh trong lớp cho học kỳ này
                    int siSo = phanLopDAO.DemSoLuongHocSinhTrongLop(lop.maLop, maHocKy);

                    if (siSo > 0)
                    {
                        dsLopCoHocSinh.Add((lop, siSo));
                    }
                }

                // Sắp xếp theo tên lớp
                dsLopCoHocSinh = dsLopCoHocSinh.OrderBy(x => x.lop.tenLop).ToList();

                // Tạo và thêm các item lớp học vào container
                foreach (var (lop, siSo) in dsLopCoHocSinh)
                {
                    // Lấy tên giáo viên chủ nhiệm
                    string tenGVCN = "Chưa có GVCN";
                    if (!string.IsNullOrEmpty(lop.maGVCN))
                    {
                        tenGVCN = giaoVienDAO.LayTenGiaoVienTheoMa(lop.maGVCN) ?? "Chưa có GVCN";
                    }

                    // Tạo item lớp học
                    var itemLop = new BaoCao.itemLopHoc();
                    itemLop.SetClassInfo(lop.maLop, lop.tenLop, siSo, tenGVCN);

                    // Thêm vào container
                    pnlClassesContainer.Controls.Add(itemLop);
                }

                // Hiển thị thông báo nếu không có lớp nào
                if (dsLopCoHocSinh.Count == 0)
                {
                    Label lblNoData = new Label
                    {
                        Text = "Không có dữ liệu lớp học cho học kỳ này",
                        Font = new Font("Segoe UI", 10, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Padding = new Padding(20)
                    };
                    pnlClassesContainer.Controls.Add(lblNoData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load danh sách lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupCardHoverEffects()
        {
            // Add hover effects to cards
            SetupCardHover(cardThongKeDiem);
            SetupCardHover(cardBaoCaoTongHop);
        }

        private void ShowDefaultPanels(bool visible)
        {
            // Ẩn/hiện 2 panel gốc trong pnlContent
            pnlClassListHeader.Visible = visible;
            pnlClassesContainer.Visible = visible;
        }

        private void SetupCardHover(Guna.UI2.WinForms.Guna2Panel card)
        {
            card.MouseEnter += (s, e) =>
            {
                card.ShadowDecoration.Depth = 10;
                card.Cursor = Cursors.Hand;
            };

            card.MouseLeave += (s, e) =>
            {
                card.ShadowDecoration.Depth = 5;
                card.Cursor = Cursors.Default;
            };
        }

        private void InitializeUserControls()
        {
            // Khởi tạo 2 UserControl chính
            ucBangDiem = new ucBaoCaoBangDiem();
            ucThongKeHocLuc = new ucBaoCaoThongKeHocLuc();

            // Thêm vào panel (ẩn hết lúc đầu)
            pnlContent.Controls.Add(ucBangDiem);
            pnlContent.Controls.Add(ucThongKeHocLuc);

            ucBangDiem.Dock = DockStyle.Fill;
            ucThongKeHocLuc.Dock = DockStyle.Fill;

            ucBangDiem.Visible = false;
            ucThongKeHocLuc.Visible = false;
        }

        private void AttachEventHandlers()
        {
            // 3 nút điều hướng
            btnDanhSachLop.Click += BtnTab_Click;
            btnBangDiem.Click += BtnTab_Click;
            btnThongKeHocLuc.Click += BtnTab_Click;

            // 3 card điều hướng tương ứng
            cardBaoCaoHocSinh.Click += (s, e) => BtnTab_Click(btnDanhSachLop, e);
            cardThongKeDiem.Click += (s, e) => BtnTab_Click(btnBangDiem, e);
            cardBaoCaoTongHop.Click += (s, e) => BtnTab_Click(btnThongKeHocLuc, e);

            // Combobox chọn học kỳ
            cboHocKy.SelectedIndexChanged += CboHocKy_SelectedIndexChanged;
        }

        private void BtnTab_Click(object sender, EventArgs e)
        {
            // Ẩn tất cả các usercontrol trước
            ucBangDiem.Visible = false;
            ucThongKeHocLuc.Visible = false;

            // Ẩn tạm 2 panel gốc
            ShowDefaultPanels(false);

            // Kiểm tra nút được nhấn
            if (sender == btnDanhSachLop || sender == cardBaoCaoHocSinh)
            {
                // Hiện lại panel danh sách lớp
                ShowDefaultPanels(true);

                // Đặt trạng thái Checked cho nút
                btnDanhSachLop.Checked = true;
                btnBangDiem.Checked = false;
                btnThongKeHocLuc.Checked = false;
            }
            else if (sender == btnBangDiem || sender == cardThongKeDiem)
            {
                ucBangDiem.Visible = true;
                ucBangDiem.BringToFront();

                btnDanhSachLop.Checked = false;
                btnBangDiem.Checked = true;
                btnThongKeHocLuc.Checked = false;
            }
            else if (sender == btnThongKeHocLuc || sender == cardBaoCaoTongHop)
            {
                ucThongKeHocLuc.Visible = true;
                ucThongKeHocLuc.BringToFront();

                btnDanhSachLop.Checked = false;
                btnBangDiem.Checked = false;
                btnThongKeHocLuc.Checked = true;
            }
        }


        private void CboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHocKy.SelectedItem != null)
            {
                dynamic selectedHocKy = cboHocKy.SelectedItem;
                int maHocKy = selectedHocKy.Value;
                string displayText = selectedHocKy.Text;

                Console.WriteLine($"Đã chọn học kỳ: {displayText} (Mã: {maHocKy})");

                // Load lại danh sách lớp theo học kỳ mới
                LoadDanhSachLopTheoHocKy();

                // Có thể gọi các phương thức load dữ liệu ở đây
                // ucBangDiem.LoadDataTheoHocKy(maHocKy);
                // ucThongKeHocLuc.LoadDataTheoHocKy(maHocKy);
            }
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cardThongKeDiem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void cardBaoCaoHocSinh_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlClass1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cardBaoCaoTongHop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblThongKeDiemDesc_Click(object sender, EventArgs e)
        {

        }

        private void lblThongKeDiemTitle_Click(object sender, EventArgs e)
        {

        }

        private void pnlIconThongKe_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cardBaoCaoHocSinh_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblBaoCaoHocSinhDesc_Click(object sender, EventArgs e)
        {

        }

        private void pnlIconBaoCao_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblBaoCaoHocSinhTitle_Click(object sender, EventArgs e)
        {

        }

        private void cardThongKeDiem_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pnlClassesContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void itemLopHoc1_Load(object sender, EventArgs e)
        {

        }

        private void pnlClassListHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {

        }

        private void lblClassListTitle_Click(object sender, EventArgs e)
        {

        }

        private void pnlTabs_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnThongKeHocLuc_Click(object sender, EventArgs e)
        {

        }

        private void btnBangDiem_Click(object sender, EventArgs e)
        {

        }

        private void btnDanhSachLop_Click(object sender, EventArgs e)
        {

        }
    }
}
