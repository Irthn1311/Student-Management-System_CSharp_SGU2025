using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
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
using iTextRectangle = iTextSharp.text.Rectangle;

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
                    itemLop.SetClassInfo(lop.maLop, lop.tenLop, siSo, tenGVCN, maHocKy);

                    // Đăng ký sự kiện để xử lý khi người dùng click nút Xem
                    itemLop.OnViewClassDetails += ItemLop_OnViewClassDetails;

                    // Thêm vào container
                    pnlClassesContainer.Controls.Add(itemLop);
                }

                // Hiển thị thông báo nếu không có lớp nào
                if (dsLopCoHocSinh.Count == 0)
                {
                    Label lblNoData = new Label
                    {
                        Text = "Không có dữ liệu lớp học cho học kỳ này",
                        Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Italic),
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

        /// <summary>
        /// Xử lý sự kiện khi người dùng click nút Xem chi tiết lớp
        /// </summary>
        private void ItemLop_OnViewClassDetails(object sender, BaoCao.ClassViewEventArgs e)
        {

            try
            {
                // Tìm Form cha để hiển thị modal
                Form parentForm = this.FindForm();

                if (parentForm == null)
                {
                    MessageBox.Show("Không tìm thấy Form cha", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo Form BaoCaoLop
                BaoCao.BaoCaoLop frmBaoCaoLop = new BaoCao.BaoCaoLop();

                // Truyền dữ liệu vào Form
                frmBaoCaoLop.SetThongTinLop(e.MaLop, e.TenLop, e.MaHocKy);

                // Hiển thị Form như modal dialog (chặn tương tác với form cha)
                frmBaoCaoLop.ShowDialog(parentForm);

                // Form tự động dispose khi đóng
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị chi tiết lớp: {ex.Message}",
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

            // Khởi tạo 2 UserControl chính (không cần BaoCaoLop nữa)
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

                // ✅ CẬP NHẬT: Truyền mã học kỳ sang ucBaoCaoBangDiem
                ucBangDiem.LoadClassesBySemester(maHocKy);

                // ✅ THÊM MỚI: Truyền mã học kỳ sang ucBaoCaoThongKeHocLuc
                ucThongKeHocLuc.LoadBaoCaoTheoHocKy(maHocKy);
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
            // Kiểm tra học kỳ đã được chọn chưa
            if (cboHocKy.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn học kỳ trước khi xuất báo cáo!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dynamic selectedHocKy = cboHocKy.SelectedItem;
                int maHocKy = selectedHocKy.Value;
                string displayText = selectedHocKy.Text;

                // Tạo SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Xuất báo cáo danh sách lớp";
                saveDialog.FileName = $"BaoCao_DanhSachLop_{displayText.Replace(" ", "_").Replace("-", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Lấy danh sách lớp theo học kỳ
                    List<LopDTO> dsLop = lopHocBUS.DocDSLop();
                    var dsLopCoHocSinh = new List<(LopDTO lop, int siSo, string tenGVCN)>();

                    foreach (var lop in dsLop)
                    {
                        int siSo = phanLopDAO.DemSoLuongHocSinhTrongLop(lop.maLop, maHocKy);
                        if (siSo > 0)
                        {
                            string tenGVCN = "Chưa có GVCN";
                            if (!string.IsNullOrEmpty(lop.maGVCN))
                            {
                                tenGVCN = giaoVienDAO.LayTenGiaoVienTheoMa(lop.maGVCN) ?? "Chưa có GVCN";
                            }
                            dsLopCoHocSinh.Add((lop, siSo, tenGVCN));
                        }
                    }

                    // Sắp xếp theo tên lớp
                    dsLopCoHocSinh = dsLopCoHocSinh.OrderBy(x => x.lop.tenLop).ToList();

                    // Kiểm tra có dữ liệu không
                    if (dsLopCoHocSinh.Count == 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Không có dữ liệu lớp học để xuất!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Tạo workbook
                    var workbook = new ClosedXML.Excel.XLWorkbook();

                    // === SHEET 1: TỔNG HỢP DANH SÁCH LỚP ===
                    CreateSummarySheet(workbook, dsLopCoHocSinh, displayText, maHocKy);

                    // === CÁC SHEET TIẾP THEO: CHI TIẾT TỪNG LỚP ===
                    foreach (var (lop, siSo, tenGVCN) in dsLopCoHocSinh)
                    {
                        CreateClassDetailSheet(workbook, lop, siSo, tenGVCN, maHocKy, displayText);
                    }

                    // Lưu file
                    workbook.SaveAs(saveDialog.FileName);

                    Cursor = Cursors.Default;

                    MessageBox.Show($"Xuất báo cáo thành công!\nĐường dẫn: {saveDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn mở file không
                    if (MessageBox.Show("Bạn có muốn mở file Excel vừa xuất?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tạo sheet tổng hợp danh sách lớp
        /// </summary>
        private void CreateSummarySheet(ClosedXML.Excel.XLWorkbook workbook,
            List<(LopDTO lop, int siSo, string tenGVCN)> dsLopCoHocSinh,
            string tenHocKy, int maHocKy)
        {
            var worksheet = workbook.Worksheets.Add("Tổng hợp");

            // === TIÊU ĐỀ ===
            worksheet.Cell(1, 1).Value = "DANH SÁCH LỚP HỌC";
            worksheet.Range(1, 1, 1, 5).Merge();
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 16;
            worksheet.Cell(1, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

            // === THÔNG TIN HỌC KỲ ===
            worksheet.Cell(2, 1).Value = $"Học kỳ: {tenHocKy}";
            worksheet.Cell(2, 1).Style.Font.Bold = true;
            worksheet.Cell(3, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            worksheet.Cell(4, 1).Value = $"Tổng số lớp: {dsLopCoHocSinh.Count}";

            // === HEADER ===
            int headerRow = 6;
            var headers = new[] { "STT", "Tên lớp", "Khối", "Sĩ số", "Giáo viên chủ nhiệm" };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                cell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                cell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            }

            // === DỮ LIỆU ===
            int currentRow = headerRow + 1;
            int stt = 1;
            int tongSiSo = 0;

            foreach (var (lop, siSo, tenGVCN) in dsLopCoHocSinh)
            {
                worksheet.Cell(currentRow, 1).Value = stt++;
                worksheet.Cell(currentRow, 2).Value = lop.tenLop;
                worksheet.Cell(currentRow, 3).Value = lop.maKhoi;
                worksheet.Cell(currentRow, 4).Value = siSo;
                worksheet.Cell(currentRow, 5).Value = tenGVCN;

                tongSiSo += siSo;

                // Căn giữa cột STT, Khối, Sĩ số
                worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                worksheet.Cell(currentRow, 3).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                // Border
                for (int i = 1; i <= 5; i++)
                {
                    worksheet.Cell(currentRow, i).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                currentRow++;
            }
            // === TỔNG KẾT ===
            int summaryRow = currentRow + 1;
            worksheet.Cell(summaryRow, 1).Value = "TỔNG CỘNG";
            worksheet.Range(summaryRow, 1, summaryRow, 3).Merge();
            worksheet.Cell(summaryRow, 1).Style.Font.Bold = true;
            worksheet.Cell(summaryRow, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

            worksheet.Cell(summaryRow, 4).Value = tongSiSo;
            worksheet.Cell(summaryRow, 4).Style.Font.Bold = true;
            worksheet.Cell(summaryRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
            worksheet.Cell(summaryRow, 4).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightYellow;

            // Border tổng cộng
            for (int i = 1; i <= 5; i++)
            {
                worksheet.Cell(summaryRow, i).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Medium;
            }

            // === TỰ ĐỘNG ĐIỀU CHỈNH ĐỘ RỘNG CỘT ===
            worksheet.Columns().AdjustToContents();

            // Đảm bảo độ rộng tối thiểu
            if (worksheet.Column(2).Width < 20) worksheet.Column(2).Width = 20; // Tên lớp
            if (worksheet.Column(5).Width < 30) worksheet.Column(5).Width = 30; // GVCN
        }

        /// <summary>
        /// Tạo sheet chi tiết cho từng lớp
        /// </summary>
        private void CreateClassDetailSheet(ClosedXML.Excel.XLWorkbook workbook,
            LopDTO lop, int siSo, string tenGVCN, int maHocKy, string tenHocKy)
        {
            // Tạo tên sheet hợp lệ (loại bỏ ký tự đặc biệt)
            string sheetName = lop.tenLop;
            // Excel không cho phép các ký tự: \ / ? * [ ]
            sheetName = System.Text.RegularExpressions.Regex.Replace(sheetName, @"[\\/\?\*\[\]]", "");
            // Giới hạn 31 ký tự
            if (sheetName.Length > 31)
                sheetName = sheetName.Substring(0, 31);

            var worksheet = workbook.Worksheets.Add(sheetName);

            // === TIÊU ĐỀ ===
            worksheet.Cell(1, 1).Value = $"DANH SÁCH HỌC SINH LỚP {lop.tenLop}";
            worksheet.Range(1, 1, 1, 7).Merge();
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 14;
            worksheet.Cell(1, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

            // === THÔNG TIN LỚP ===
            worksheet.Cell(2, 1).Value = $"Học kỳ: {tenHocKy}";
            worksheet.Cell(3, 1).Value = $"Giáo viên chủ nhiệm: {tenGVCN}";
            worksheet.Cell(4, 1).Value = $"Sĩ số: {siSo} học sinh";
            worksheet.Cell(5, 1).Value = $"Khối: {lop.maKhoi}";

            // === HEADER ===
            int headerRow = 7;
            var headers = new[] { "STT", "Mã HS", "Họ và tên", "Ngày sinh", "Giới tính", "Số điện thoại", "Email" };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightBlue;
                cell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                cell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            }

            // === DỮ LIỆU HỌC SINH ===
            List<HocSinhDTO> dsHocSinh = phanLopDAO.LayDanhSachHocSinhTrongLop(lop.maLop, maHocKy);
            dsHocSinh = dsHocSinh.OrderBy(hs => hs.HoTen).ToList();

            int currentRow = headerRow + 1;
            int stt = 1;

            foreach (var hs in dsHocSinh)
            {
                worksheet.Cell(currentRow, 1).Value = stt++;
                worksheet.Cell(currentRow, 2).Value = hs.MaHS;
                worksheet.Cell(currentRow, 3).Value = hs.HoTen;
                worksheet.Cell(currentRow, 4).Value = hs.NgaySinh.ToString("dd/MM/yyyy");
                worksheet.Cell(currentRow, 5).Value = hs.GioiTinh;
                worksheet.Cell(currentRow, 6).Value = hs.SdtHS ?? "";
                worksheet.Cell(currentRow, 7).Value = hs.Email ?? "";

                // Căn giữa các cột
                worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                // Border
                for (int i = 1; i <= 7; i++)
                {
                    worksheet.Cell(currentRow, i).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                }

                currentRow++;
            }

            // === THỐNG KÊ ===
            int statsRow = currentRow + 2;
            worksheet.Cell(statsRow, 1).Value = "Thống kê:";
            worksheet.Cell(statsRow, 1).Style.Font.Bold = true;

            statsRow++;
            int soNam = dsHocSinh.Count(hs => hs.GioiTinh == "Nam");
            int soNu = dsHocSinh.Count(hs => hs.GioiTinh == "Nữ");

            worksheet.Cell(statsRow, 1).Value = $"Nam: {soNam}";
            worksheet.Cell(statsRow, 2).Value = $"Nữ: {soNu}";

            // === TỰ ĐỘNG ĐIỀU CHỈNH ĐỘ RỘNG CỘT ===
            worksheet.Columns().AdjustToContents();

            // Đảm bảo độ rộng tối thiểu
            if (worksheet.Column(3).Width < 25) worksheet.Column(3).Width = 25; // Họ tên
            if (worksheet.Column(6).Width < 15) worksheet.Column(6).Width = 15; // SĐT
            if (worksheet.Column(7).Width < 30) worksheet.Column(7).Width = 30; // Email
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
            // Kiểm tra học kỳ đã được chọn chưa
            if (cboHocKy.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn học kỳ trước khi xuất báo cáo!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dynamic selectedHocKy = cboHocKy.SelectedItem;
                int maHocKy = selectedHocKy.Value;
                string displayText = selectedHocKy.Text;

                // Tạo SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Xuất báo cáo tổng quan",
                    FileName = $"BaoCao_TongQuan_{displayText.Replace(" ", "_").Replace("-", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Lấy danh sách lớp theo học kỳ
                    List<LopDTO> dsLop = lopHocBUS.DocDSLop();
                    var dsLopCoHocSinh = new List<(LopDTO lop, int siSo, string tenGVCN)>();

                    foreach (var lop in dsLop)
                    {
                        int siSo = phanLopDAO.DemSoLuongHocSinhTrongLop(lop.maLop, maHocKy);
                        if (siSo > 0)
                        {
                            string tenGVCN = "Chưa có GVCN";
                            if (!string.IsNullOrEmpty(lop.maGVCN))
                            {
                                tenGVCN = giaoVienDAO.LayTenGiaoVienTheoMa(lop.maGVCN) ?? "Chưa có GVCN";
                            }
                            dsLopCoHocSinh.Add((lop, siSo, tenGVCN));
                        }
                    }

                    // Sắp xếp theo tên lớp
                    dsLopCoHocSinh = dsLopCoHocSinh.OrderBy(x => x.lop.tenLop).ToList();

                    // Kiểm tra có dữ liệu không
                    if (dsLopCoHocSinh.Count == 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Không có dữ liệu lớp học để xuất!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Xuất PDF
                    XuatPDFTongQuan(saveDialog.FileName, dsLopCoHocSinh, displayText, maHocKy);

                    Cursor = Cursors.Default;

                    MessageBox.Show($"Xuất báo cáo PDF thành công!\nĐường dẫn: {saveDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn mở file không
                    if (MessageBox.Show("Bạn có muốn mở file PDF vừa xuất?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Lỗi khi xuất báo cáo PDF: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất báo cáo tổng quan ra file PDF
        /// </summary>
        private void XuatPDFTongQuan(string filePath,
            List<(LopDTO lop, int siSo, string tenGVCN)> dsLopCoHocSinh,
            string tenHocKy, int maHocKy)
        {
            Document document = new Document(PageSize.A4, 40, 40, 60, 60);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            document.Open();

            // Load font tiếng Việt
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font smallFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // === HEADER ===
            PdfPTable headerTable = new PdfPTable(1);
            headerTable.WidthPercentage = 100;
            PdfPCell headerCell = new PdfPCell(new Phrase("BÁO CÁO TỔNG QUAN TOÀN KHỐI", titleFont));
            headerCell.BackgroundColor = new BaseColor(33, 150, 243);
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Padding = 15;
            headerCell.Border = iTextRectangle.NO_BORDER;
            headerTable.AddCell(headerCell);
            document.Add(headerTable);

            document.Add(new Paragraph("\n"));

            // === THÔNG TIN HỌC KỲ ===
            Paragraph hocKyInfo = new Paragraph($"Học kỳ: {tenHocKy}", headerFont);
            hocKyInfo.Alignment = Element.ALIGN_CENTER;
            document.Add(hocKyInfo);

            Paragraph ngayXuat = new Paragraph($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}", smallFont);
            ngayXuat.Alignment = Element.ALIGN_CENTER;
            ngayXuat.SpacingAfter = 20;
            document.Add(ngayXuat);

            // === TÍNH TOÁN THỐNG KÊ ===
            int tongSoLop = dsLopCoHocSinh.Count;
            int tongSoHocSinh = dsLopCoHocSinh.Sum(x => x.siSo);

            // Thống kê Nam/Nữ từng lớp
            int tongNam = 0;
            int tongNu = 0;

            foreach (var (lop, siSo, tenGVCN) in dsLopCoHocSinh)
            {
                List<HocSinhDTO> dsHS = phanLopDAO.LayDanhSachHocSinhTrongLop(lop.maLop, maHocKy);
                tongNam += dsHS.Count(hs => hs.GioiTinh == "Nam");
                tongNu += dsHS.Count(hs => hs.GioiTinh == "Nữ");
            }

            // Thống kê xếp loại (nếu có XepLoaiBUS)
            XepLoaiBUS xepLoaiBUS = new XepLoaiBUS();
            Dictionary<string, int> thongKeKhoi10 = new Dictionary<string, int>();
            Dictionary<string, int> thongKeKhoi11 = new Dictionary<string, int>();
            Dictionary<string, int> thongKeKhoi12 = new Dictionary<string, int>();

            try
            {
                thongKeKhoi10 = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(maHocKy, 10);
                thongKeKhoi11 = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(maHocKy, 11);
                thongKeKhoi12 = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(maHocKy, 12);
            }
            catch
            {
                // Bỏ qua nếu không có dữ liệu xếp loại
            }

            // === BẢNG THỐNG KÊ TỔNG QUAN ===
            PdfPTable statsTable = new PdfPTable(2);
            statsTable.WidthPercentage = 100;
            statsTable.SetWidths(new float[] { 1f, 1f });
            statsTable.SpacingBefore = 10;
            statsTable.SpacingAfter = 20;

            // Card 1: Tổng số lớp
            AddStatCard(statsTable, "Tổng số lớp", tongSoLop.ToString(),
                new BaseColor(59, 130, 246), bf);

            // Card 2: Tổng số học sinh
            AddStatCard(statsTable, "Tổng số học sinh", tongSoHocSinh.ToString(),
                new BaseColor(16, 185, 129), bf);

            // Card 3: Học sinh Nam
            AddStatCard(statsTable, "Học sinh Nam", $"{tongNam} ({(tongSoHocSinh > 0 ? (tongNam * 100.0 / tongSoHocSinh).ToString("0.0") : "0")}%)",
                new BaseColor(99, 102, 241), bf);

            // Card 4: Học sinh Nữ
            AddStatCard(statsTable, "Học sinh Nữ", $"{tongNu} ({(tongSoHocSinh > 0 ? (tongNu * 100.0 / tongSoHocSinh).ToString("0.0") : "0")}%)",
                new BaseColor(236, 72, 153), bf);

            document.Add(statsTable);

            // === DANH SÁCH CÁC LỚP HỌC ===
            Paragraph lopHocTitle = new Paragraph("DANH SÁCH CÁC LỚP HỌC", headerFont);
            lopHocTitle.SpacingBefore = 10;
            lopHocTitle.SpacingAfter = 10;
            document.Add(lopHocTitle);

            PdfPTable lopTable = new PdfPTable(5);
            lopTable.WidthPercentage = 100;
            lopTable.SetWidths(new float[] { 0.5f, 1.5f, 0.8f, 0.8f, 2f });

            // Header
            AddTableHeader(lopTable, "STT", bf);
            AddTableHeader(lopTable, "Tên lớp", bf);
            AddTableHeader(lopTable, "Khối", bf);
            AddTableHeader(lopTable, "Sĩ số", bf);
            AddTableHeader(lopTable, "Giáo viên chủ nhiệm", bf);

            // Data
            int stt = 1;
            foreach (var (lop, siSo, tenGVCN) in dsLopCoHocSinh)
            {
                AddTableCell(lopTable, stt.ToString(), normalFont, Element.ALIGN_CENTER);
                AddTableCell(lopTable, lop.tenLop, boldFont, Element.ALIGN_LEFT);
                AddTableCell(lopTable, lop.maKhoi.ToString(), normalFont, Element.ALIGN_CENTER);
                AddTableCell(lopTable, siSo.ToString(), normalFont, Element.ALIGN_CENTER);
                AddTableCell(lopTable, tenGVCN, normalFont, Element.ALIGN_LEFT);
                stt++;
            }

            // Tổng cộng
            PdfPCell totalLabelCell = new PdfPCell(new Phrase("TỔNG CỘNG", boldFont));
            totalLabelCell.Colspan = 3;
            totalLabelCell.HorizontalAlignment = Element.ALIGN_CENTER;
            totalLabelCell.BackgroundColor = new BaseColor(243, 244, 246);
            totalLabelCell.Padding = 8;
            lopTable.AddCell(totalLabelCell);

            PdfPCell totalValueCell = new PdfPCell(new Phrase(tongSoHocSinh.ToString(), boldFont));
            totalValueCell.HorizontalAlignment = Element.ALIGN_CENTER;
            totalValueCell.BackgroundColor = new BaseColor(254, 249, 195);
            totalValueCell.Padding = 8;
            lopTable.AddCell(totalValueCell);

            PdfPCell emptyCell = new PdfPCell(new Phrase(""));
            emptyCell.BackgroundColor = new BaseColor(243, 244, 246);
            lopTable.AddCell(emptyCell);

            document.Add(lopTable);

            // === THỐNG KÊ XẾP LOẠI THEO KHỐI (NẾU CÓ) ===
            if (thongKeKhoi10.Values.Sum() > 0 || thongKeKhoi11.Values.Sum() > 0 || thongKeKhoi12.Values.Sum() > 0)
            {
                document.Add(new Paragraph("\n"));

                Paragraph xepLoaiTitle = new Paragraph("THỐNG KÊ XẾP LOẠI THEO KHỐI", headerFont);
                xepLoaiTitle.SpacingBefore = 10;
                xepLoaiTitle.SpacingAfter = 10;
                document.Add(xepLoaiTitle);

                PdfPTable xepLoaiTable = new PdfPTable(6);
                xepLoaiTable.WidthPercentage = 100;
                xepLoaiTable.SetWidths(new float[] { 1.5f, 1f, 1f, 1f, 1f, 1f });

                // Header
                AddTableHeader(xepLoaiTable, "Khối", bf);
                AddTableHeader(xepLoaiTable, "Giỏi", bf);
                AddTableHeader(xepLoaiTable, "Khá", bf);
                AddTableHeader(xepLoaiTable, "TB", bf);
                AddTableHeader(xepLoaiTable, "Yếu", bf);
                AddTableHeader(xepLoaiTable, "Kém", bf);

                // Khối 10
                if (thongKeKhoi10.Values.Sum() > 0)
                {
                    AddTableCell(xepLoaiTable, "Khối 10", boldFont, Element.ALIGN_LEFT);
                    AddTableCell(xepLoaiTable, thongKeKhoi10["Giỏi"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi10["Khá"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi10["Trung bình"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi10["Yếu"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi10["Kém"].ToString(), normalFont, Element.ALIGN_CENTER);
                }

                // Khối 11
                if (thongKeKhoi11.Values.Sum() > 0)
                {
                    AddTableCell(xepLoaiTable, "Khối 11", boldFont, Element.ALIGN_LEFT);
                    AddTableCell(xepLoaiTable, thongKeKhoi11["Giỏi"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi11["Khá"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi11["Trung bình"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi11["Yếu"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi11["Kém"].ToString(), normalFont, Element.ALIGN_CENTER);
                }

                // Khối 12
                if (thongKeKhoi12.Values.Sum() > 0)
                {
                    AddTableCell(xepLoaiTable, "Khối 12", boldFont, Element.ALIGN_LEFT);
                    AddTableCell(xepLoaiTable, thongKeKhoi12["Giỏi"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi12["Khá"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi12["Trung bình"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi12["Yếu"].ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, thongKeKhoi12["Kém"].ToString(), normalFont, Element.ALIGN_CENTER);
                }

                document.Add(xepLoaiTable);
            }

            // === FOOTER ===
            document.Add(new Paragraph("\n\n"));
            Paragraph footer = new Paragraph(
                $"Báo cáo được tạo tự động bởi Hệ thống Quản lý Học sinh\nNgày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}",
                new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.ITALIC, BaseColor.GRAY));
            footer.Alignment = Element.ALIGN_CENTER;
            document.Add(footer);

            document.Close();
            writer.Close();
        }

        /// <summary>
        /// Thêm card thống kê vào bảng
        /// </summary>
        private void AddStatCard(PdfPTable table, string label, string value, BaseColor color, BaseFont bf)
        {
            PdfPTable cardTable = new PdfPTable(1);
            cardTable.WidthPercentage = 100;

            // Header card
            PdfPCell headerCell = new PdfPCell(new Phrase(label,
                new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE)));
            headerCell.BackgroundColor = color;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Padding = 8;
            headerCell.Border = iTextRectangle.NO_BORDER;
            cardTable.AddCell(headerCell);

            // Value card
            PdfPCell valueCell = new PdfPCell(new Phrase(value,
                new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD, color)));
            valueCell.BackgroundColor = new BaseColor(249, 250, 251);
            valueCell.HorizontalAlignment = Element.ALIGN_CENTER;
            valueCell.Padding = 12;
            valueCell.Border = iTextRectangle.NO_BORDER;
            cardTable.AddCell(valueCell);

            PdfPCell containerCell = new PdfPCell(cardTable);
            containerCell.Padding = 5;
            containerCell.Border = iTextRectangle.NO_BORDER;
            table.AddCell(containerCell);
        }

        /// <summary>
        /// Thêm header cell cho bảng
        /// </summary>
        private void AddTableHeader(PdfPTable table, string text, BaseFont bf)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text,
                new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            cell.BackgroundColor = new BaseColor(229, 231, 235);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 8;
            table.AddCell(cell);
        }

        /// <summary>
        /// Thêm data cell cho bảng
        /// </summary>
        private void AddTableCell(PdfPTable table, string text, iTextSharp.text.Font font, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 8;
            cell.Border = iTextRectangle.BOX;
            cell.BorderColor = new BaseColor(229, 231, 235);
            table.AddCell(cell);
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
