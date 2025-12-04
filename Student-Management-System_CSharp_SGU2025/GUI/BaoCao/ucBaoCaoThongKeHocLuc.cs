using iTextSharp.text;
using iTextSharp.text.pdf;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.Utils;
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
    public partial class ucBaoCaoThongKeHocLuc : UserControl
    {
        private XepLoaiBUS xepLoaiBUS;
        private int currentMaHocKy = 0;
        public ucBaoCaoThongKeHocLuc()
        {
            InitializeComponent();
            xepLoaiBUS = new XepLoaiBUS();
        }

        /// <summary>
        /// Load báo cáo xếp loại theo học kỳ được chọn từ ucBaoCao
        /// Được gọi từ ucBaoCao khi người dùng thay đổi học kỳ
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ được chọn</param>
        public void LoadBaoCaoTheoHocKy(int maHocKy)
        {
            try
            {
                currentMaHocKy = maHocKy;

                // Load báo cáo cho từng khối
                LoadBaoCaoKhoi10(maHocKy);
                LoadBaoCaoKhoi11(maHocKy);
                LoadBaoCaoKhoi12(maHocKy);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load báo cáo xếp loại: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load báo cáo xếp loại cho Khối 10
        /// </summary>
        private void LoadBaoCaoKhoi10(int maHocKy)
        {
            try
            {
                int maKhoi = 10;

                // Lấy thống kê xếp loại tổng kết theo khối
                Dictionary<string, int> thongKe = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(maHocKy, maKhoi);

                // Lấy tổng số học sinh của khối (bao gồm cả chưa có xếp loại)
                int tongSoHocSinh = xepLoaiBUS.DemTongSoHocSinhTheoKhoi(maHocKy, maKhoi);

                // Cập nhật dữ liệu lên UserControl
                baoCaoKhoi1.SoGioi = thongKe["Giỏi"].ToString();
                baoCaoKhoi1.SoKha = thongKe["Khá"].ToString();
                baoCaoKhoi1.SoTrungBinh = thongKe["Trung bình"].ToString();
                baoCaoKhoi1.SoYeu = thongKe["Yếu"].ToString();

                // Tính số học sinh Kém (nếu UserControl có property này)
                // baoCaoKhoi1.SoKem = thongKe["Kém"].ToString();

                // Hiển thị tổng số học sinh khối 10 (nếu UserControl có property này)
                // baoCaoKhoi1.SoHocSinhKhoi = tongSoHocSinh.ToString();

                // Hiển thị tên khối (nếu UserControl có property này)
                // baoCaoKhoi1.KhoiLop = "Khối 10";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load báo cáo Khối 10: {ex.Message}");

                // Reset về giá trị mặc định khi có lỗi
                baoCaoKhoi1.SoGioi = "0";
                baoCaoKhoi1.SoKha = "0";
                baoCaoKhoi1.SoTrungBinh = "0";
                baoCaoKhoi1.SoYeu = "0";
            }
        }

        /// <summary>
        /// Load báo cáo xếp loại cho Khối 11
        /// </summary>
        private void LoadBaoCaoKhoi11(int maHocKy)
        {
            try
            {
                int maKhoi = 11;

                // Lấy thống kê xếp loại tổng kết theo khối
                Dictionary<string, int> thongKe = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(maHocKy, maKhoi);

                // Lấy tổng số học sinh của khối (bao gồm cả chưa có xếp loại)
                int tongSoHocSinh = xepLoaiBUS.DemTongSoHocSinhTheoKhoi(maHocKy, maKhoi);

                // Cập nhật dữ liệu lên UserControl
                baoCaoKhoi2.SoGioi = thongKe["Giỏi"].ToString();
                baoCaoKhoi2.SoKha = thongKe["Khá"].ToString();
                baoCaoKhoi2.SoTrungBinh = thongKe["Trung bình"].ToString();
                baoCaoKhoi2.SoYeu = thongKe["Yếu"].ToString();

                // Tính số học sinh Kém (nếu UserControl có property này)
                // baoCaoKhoi2.SoKem = thongKe["Kém"].ToString();

                // Hiển thị tổng số học sinh khối 11 (nếu UserControl có property này)
                // baoCaoKhoi2.SoHocSinhKhoi = tongSoHocSinh.ToString();

                // Hiển thị tên khối (nếu UserControl có property này)
                // baoCaoKhoi2.KhoiLop = "Khối 11";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load báo cáo Khối 11: {ex.Message}");

                // Reset về giá trị mặc định khi có lỗi
                baoCaoKhoi2.SoGioi = "0";
                baoCaoKhoi2.SoKha = "0";
                baoCaoKhoi2.SoTrungBinh = "0";
                baoCaoKhoi2.SoYeu = "0";
            }
        }

        /// <summary>
        /// Load báo cáo xếp loại cho Khối 12
        /// </summary>
        private void LoadBaoCaoKhoi12(int maHocKy)
        {
            try
            {
                int maKhoi = 12;

                // Lấy thống kê xếp loại tổng kết theo khối
                Dictionary<string, int> thongKe = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(maHocKy, maKhoi);

                // Lấy tổng số học sinh của khối (bao gồm cả chưa có xếp loại)
                int tongSoHocSinh = xepLoaiBUS.DemTongSoHocSinhTheoKhoi(maHocKy, maKhoi);

                // Cập nhật dữ liệu lên UserControl
                baoCaoKhoi3.SoGioi = thongKe["Giỏi"].ToString();
                baoCaoKhoi3.SoKha = thongKe["Khá"].ToString();
                baoCaoKhoi3.SoTrungBinh = thongKe["Trung bình"].ToString();
                baoCaoKhoi3.SoYeu = thongKe["Yếu"].ToString();

                // Tính số học sinh Kém (nếu UserControl có property này)
                // baoCaoKhoi3.SoKem = thongKe["Kém"].ToString();

                // Hiển thị tổng số học sinh khối 12 (nếu UserControl có property này)
                // baoCaoKhoi3.SoHocSinhKhoi = tongSoHocSinh.ToString();

                // Hiển thị tên khối (nếu UserControl có property này)
                // baoCaoKhoi3.KhoiLop = "Khối 12";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load báo cáo Khối 12: {ex.Message}");

                // Reset về giá trị mặc định khi có lỗi
                baoCaoKhoi3.SoGioi = "0";
                baoCaoKhoi3.SoKha = "0";
                baoCaoKhoi3.SoTrungBinh = "0";
                baoCaoKhoi3.SoYeu = "0";
            }
        }

        private void BtnExportStatistics_Click(object sender, EventArgs e)
        {
            // Kiểm tra học kỳ đã được chọn chưa
            if (currentMaHocKy == 0)
            {
                MessageBox.Show("Vui lòng chọn học kỳ trước khi xuất báo cáo!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra học kỳ có dữ liệu không
                HocKyDAO hocKyDAO = new HocKyDAO();
                if (!hocKyDAO.KiemTraHocKyCoXepLoai(currentMaHocKy))
                {
                    MessageBox.Show("Học kỳ này chưa có dữ liệu xếp loại để xuất báo cáo!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin học kỳ
                HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(currentMaHocKy);
                if (hocKy == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin học kỳ!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tenHocKy = $"{hocKy.TenHocKy} - {hocKy.MaNamHoc}";

                // Tạo SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Xuất báo cáo tổng hợp",
                    FileName = $"BaoCao_TongHop_{hocKy.TenHocKy.Replace(" ", "")}_{hocKy.MaNamHoc.Replace("-", "")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Lấy dữ liệu thống kê
                    var thongKeKhoi10 = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(currentMaHocKy, 10);
                    var thongKeKhoi11 = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(currentMaHocKy, 11);
                    var thongKeKhoi12 = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(currentMaHocKy, 12);

                    int tongHSKhoi10 = xepLoaiBUS.DemTongSoHocSinhTheoKhoi(currentMaHocKy, 10);
                    int tongHSKhoi11 = xepLoaiBUS.DemTongSoHocSinhTheoKhoi(currentMaHocKy, 11);
                    int tongHSKhoi12 = xepLoaiBUS.DemTongSoHocSinhTheoKhoi(currentMaHocKy, 12);

                    // Lấy danh sách lớp
                    LopHocBUS lopHocBUS = new LopHocBUS();
                    List<LopDTO> dsLop = lopHocBUS.GetDanhSachLopTheoHocKy(currentMaHocKy);

                    // Lấy thông tin học sinh nam/nữ
                    PhanLopDAO phanLopDAO = new PhanLopDAO();
                    int tongNam = 0;
                    int tongNu = 0;

                    foreach (var lop in dsLop)
                    {
                        List<HocSinhDTO> dsHS = phanLopDAO.LayDanhSachHocSinhTrongLop(lop.MaLop, currentMaHocKy);
                        tongNam += dsHS.Count(hs => hs.GioiTinh == "Nam");
                        tongNu += dsHS.Count(hs => hs.GioiTinh == "Nữ");
                    }

                    // Xuất PDF
                    XuatPDFBaoCaoTongHop(
                        saveDialog.FileName,
                        tenHocKy,
                        thongKeKhoi10, thongKeKhoi11, thongKeKhoi12,
                        tongHSKhoi10, tongHSKhoi11, tongHSKhoi12,
                        dsLop,
                        tongNam, tongNu,
                        currentMaHocKy
                    );

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
        /// Xuất báo cáo tổng hợp ra file PDF
        /// </summary>
        private void XuatPDFBaoCaoTongHop(
            string filePath,
    string tenHocKy,
    Dictionary<string, int> thongKeKhoi10,
    Dictionary<string, int> thongKeKhoi11,
    Dictionary<string, int> thongKeKhoi12,
    int tongHSKhoi10, int tongHSKhoi11, int tongHSKhoi12,
    List<LopDTO> dsLop,
    int tongNam, int tongNu,
    int maHocKy)
        {
            Document document = new Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            // Thêm bookmark và outline
            writer.PageEvent = new PDFPageEventHelper();

            document.Open();

            // Load font tiếng Việt
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            // === TRANG 1: TRANG BÌA ===
            TaoTrangBia(document, bf, tenHocKy);

            //// === TRANG 2: MỤC LỤC ===
            //document.NewPage();
            //TaoMucLuc(document, bf, dsLop);

            // === TRANG 3: TỔNG QUAN TOÀN KHỐI ===
            document.NewPage();
            TaoTrangTongQuan(document, bf,
                thongKeKhoi10, thongKeKhoi11, thongKeKhoi12,
                tongHSKhoi10, tongHSKhoi11, tongHSKhoi12,
                dsLop, tongNam, tongNu, tenHocKy);

            // === CÁC TRANG TIẾP THEO: DANH SÁCH CÁC LỚP HỌC ===
            if (dsLop.Count > 0)
            {
                PhanLopDAO phanLopDAO = new PhanLopDAO();
                GiaoVienDAO giaoVienDAO = new GiaoVienDAO();

                // Sắp xếp lớp theo khối và tên
                var dsLopSorted = dsLop.OrderBy(l => l.MaKhoi).ThenBy(l => l.TenLop).ToList();

                foreach (var lop in dsLopSorted)
                {
                    document.NewPage();
                    TaoTrangDanhSachLop(document, bf, lop, maHocKy, phanLopDAO, giaoVienDAO);
                }
            }

            document.Close();
            writer.Close();
        }

        /// <summary>
        /// Tạo trang danh sách chi tiết cho từng lớp
        /// </summary>
        private void TaoTrangDanhSachLop(Document document, BaseFont bf, LopDTO lop,
            int maHocKy, PhanLopDAO phanLopDAO, GiaoVienDAO giaoVienDAO)
        {
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font sectionFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            // === TIÊU ĐỀ LỚP ===
            Paragraph title = new Paragraph($"DANH SÁCH LỚP {lop.TenLop}", headerFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingBefore = 20;
            title.SpacingAfter = 20;
            document.Add(title);

            // === THÔNG TIN LỚP ===
            PdfPTable infoTable = new PdfPTable(2);
            infoTable.WidthPercentage = 70;
            infoTable.SetWidths(new float[] { 2f, 3f });
            infoTable.HorizontalAlignment = Element.ALIGN_LEFT;
            infoTable.SpacingAfter = 15;

            // Lấy thông tin GVCN
            string tenGVCN = "Chưa có GVCN";
            if (!string.IsNullOrEmpty(lop.MaGVCN))
            {
                tenGVCN = giaoVienDAO.LayTenGiaoVienTheoMa(lop.MaGVCN) ?? "Chưa có GVCN";
            }

            // Lấy danh sách học sinh
            List<HocSinhDTO> dsHocSinh = phanLopDAO.LayDanhSachHocSinhTrongLop(lop.MaLop, maHocKy);
            int soNam = dsHocSinh.Count(hs => hs.GioiTinh == "Nam");
            int soNu = dsHocSinh.Count(hs => hs.GioiTinh == "Nữ");

            AddInfoRowLop(infoTable, "Khối:", $"Khối {lop.MaKhoi}", bf);
            AddInfoRowLop(infoTable, "Sĩ số:", $"{dsHocSinh.Count} học sinh", bf);
            AddInfoRowLop(infoTable, "Nam/Nữ:", $"{soNam} Nam / {soNu} Nữ", bf);
            AddInfoRowLop(infoTable, "GVCN:", tenGVCN, bf);

            document.Add(infoTable);

            // === BẢNG DANH SÁCH HỌC SINH ===
            Paragraph dsTitle = new Paragraph("Danh sách học sinh:", sectionFont);
            dsTitle.SpacingBefore = 10;
            dsTitle.SpacingAfter = 10;
            document.Add(dsTitle);

            PdfPTable studentTable = new PdfPTable(6);
            studentTable.WidthPercentage = 100;
            studentTable.SetWidths(new float[] { 0.7f, 1.2f, 2.5f, 1.3f, 1f, 1.3f });
            studentTable.SpacingAfter = 15;

            // Header
            AddHeaderCell(studentTable, "STT", bf);
            AddHeaderCell(studentTable, "Mã HS", bf);
            AddHeaderCell(studentTable, "Họ và tên", bf);
            AddHeaderCell(studentTable, "Ngày sinh", bf);
            AddHeaderCell(studentTable, "Giới tính", bf);
            AddHeaderCell(studentTable, "Xếp loại", bf);

            // Lấy thông tin xếp loại
            var dsXepLoai = xepLoaiBUS.LayDanhSachXepLoaiDayDu(maHocKy, lop.MaLop);
            var xepLoaiDict = dsXepLoai.ToDictionary(x => x.MaHocSinh, x => x.XepLoaiTongKet ?? "Chưa có");

            // Data - sắp xếp theo tên
            var dsHocSinhSorted = dsHocSinh.OrderBy(hs => hs.HoTen).ToList();
            int stt = 1;

            foreach (var hs in dsHocSinhSorted)
            {
                AddDataCell(studentTable, stt.ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(studentTable, hs.MaHS.ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(studentTable, hs.HoTen, normalFont, Element.ALIGN_LEFT);
                AddDataCell(studentTable, hs.NgaySinh.ToString("dd/MM/yyyy"), normalFont, Element.ALIGN_CENTER);
                AddDataCell(studentTable, hs.GioiTinh, normalFont, Element.ALIGN_CENTER);

                // Xếp loại với màu sắc
                string xepLoai = xepLoaiDict.ContainsKey(hs.MaHS) ? xepLoaiDict[hs.MaHS] : "Chưa có";
                BaseColor xepLoaiColor = GetXepLoaiColor(xepLoai);
                AddDataCellWithColor(studentTable, xepLoai, bf, xepLoaiColor, Element.ALIGN_CENTER);

                stt++;
            }

            document.Add(studentTable);

            // === THỐNG KÊ XẾP LOẠI CỦA LỚP ===
            Paragraph thongKeTitle = new Paragraph("Thống kê xếp loại:", sectionFont);
            thongKeTitle.SpacingBefore = 15;
            thongKeTitle.SpacingAfter = 10;
            document.Add(thongKeTitle);

            var thongKeXepLoai = xepLoaiBUS.ThongKeXepLoaiTongKet(maHocKy, lop.MaLop);

            PdfPTable thongKeTable = new PdfPTable(5);
            thongKeTable.WidthPercentage = 90;
            thongKeTable.HorizontalAlignment = Element.ALIGN_LEFT;

            // Header thống kê
            AddHeaderCell(thongKeTable, "Giỏi", bf);
            AddHeaderCell(thongKeTable, "Khá", bf);
            AddHeaderCell(thongKeTable, "Trung bình", bf);
            AddHeaderCell(thongKeTable, "Yếu", bf);
            AddHeaderCell(thongKeTable, "Kém", bf);

            // Data thống kê
            AddDataCellWithColor(thongKeTable, thongKeXepLoai["Giỏi"].ToString(), bf,
                new BaseColor(22, 163, 74), Element.ALIGN_CENTER);
            AddDataCellWithColor(thongKeTable, thongKeXepLoai["Khá"].ToString(), bf,
                new BaseColor(30, 136, 229), Element.ALIGN_CENTER);
            AddDataCellWithColor(thongKeTable, thongKeXepLoai["Trung bình"].ToString(), bf,
                new BaseColor(234, 179, 8), Element.ALIGN_CENTER);
            AddDataCellWithColor(thongKeTable, thongKeXepLoai["Yếu"].ToString(), bf,
                new BaseColor(220, 38, 38), Element.ALIGN_CENTER);
            AddDataCellWithColor(thongKeTable, thongKeXepLoai["Kém"].ToString(), bf,
                new BaseColor(127, 29, 29), Element.ALIGN_CENTER);

            document.Add(thongKeTable);

            // Tỷ lệ xếp loại
            int tongCoXepLoai = thongKeXepLoai.Values.Sum();
            if (tongCoXepLoai > 0)
            {
                Paragraph tyLeInfo = new Paragraph(
                    $"\nTỷ lệ hoàn thành: {tongCoXepLoai}/{dsHocSinh.Count} học sinh ({(tongCoXepLoai * 100.0 / dsHocSinh.Count):0.0}%)",
                    new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC, BaseColor.DARK_GRAY));
                tyLeInfo.SpacingBefore = 10;
                document.Add(tyLeInfo);
            }
        }

        /// <summary>
        /// Thêm dòng thông tin cho bảng thông tin lớp
        /// </summary>
        private void AddInfoRowLop(PdfPTable table, string label, string value, BaseFont bf)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label,
                new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            labelCell.Border = iTextRectangle.NO_BORDER;
            labelCell.PaddingBottom = 5;
            table.AddCell(labelCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value,
                new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            valueCell.Border = iTextRectangle.NO_BORDER;
            valueCell.PaddingBottom = 5;
            table.AddCell(valueCell);
        }

        /// <summary>
        /// Thêm cell với màu sắc tùy chỉnh
        /// </summary>
        private void AddDataCellWithColor(PdfPTable table, string text, BaseFont bf, BaseColor color, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text,
                new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, color)));
            cell.HorizontalAlignment = alignment;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 8;
            cell.BorderColor = new BaseColor(229, 231, 235);
            table.AddCell(cell);
        }

        /// <summary>
        /// Lấy màu sắc theo xếp loại
        /// </summary>
        private BaseColor GetXepLoaiColor(string xepLoai)
        {
            switch (xepLoai)
            {
                case "Giỏi":
                    return new BaseColor(22, 163, 74);  // Xanh lá
                case "Khá":
                    return new BaseColor(30, 136, 229);  // Xanh dương
                case "Trung bình":
                    return new BaseColor(234, 179, 8);   // Vàng
                case "Yếu":
                    return new BaseColor(220, 38, 38);   // Đỏ
                case "Kém":
                    return new BaseColor(127, 29, 29);   // Đỏ đậm
                default:
                    return BaseColor.GRAY;               // Xám (chưa có)
            }
        }

        /// <summary>
        /// Tạo trang bìa báo cáo
        /// </summary>
        private void TaoTrangBia(Document document, BaseFont bf, string tenHocKy)
        {
            iTextSharp.text.Font schoolNameFont = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 22, iTextSharp.text.Font.BOLD, new BaseColor(33, 150, 243));
            iTextSharp.text.Font subtitleFont = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Logo và tên trường (căn giữa phía trên)
            Paragraph schoolName = new Paragraph("TRƯỜNG THPT ABC", schoolNameFont);
            schoolName.Alignment = Element.ALIGN_CENTER;
            schoolName.SpacingBefore = 100;
            document.Add(schoolName);

            Paragraph schoolName2 = new Paragraph("HỆ THỐNG QUẢN LÝ HỌC SINH",
                new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY));
            schoolName2.Alignment = Element.ALIGN_CENTER;
            schoolName2.SpacingAfter = 80;
            document.Add(schoolName2);

            // Tiêu đề báo cáo (lớn, nổi bật)
            Paragraph title = new Paragraph("BÁO CÁO TỔNG HỢP", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 15;
            document.Add(title);

            Paragraph subtitle = new Paragraph(tenHocKy.ToUpper(), subtitleFont);
            subtitle.Alignment = Element.ALIGN_CENTER;
            subtitle.SpacingAfter = 120;
            document.Add(subtitle);

            // Thông tin phía dưới
            Paragraph dateInfo = new Paragraph($"Ngày xuất báo cáo: {DateTime.Now:dd/MM/yyyy}", normalFont);
            dateInfo.Alignment = Element.ALIGN_CENTER;
            dateInfo.SpacingAfter = 10;
            document.Add(dateInfo);

            Paragraph timeInfo = new Paragraph($"Thời gian: {DateTime.Now:HH:mm:ss}", normalFont);
            timeInfo.Alignment = Element.ALIGN_CENTER;
            document.Add(timeInfo);
        }

        ///// <summary>
        ///// Tạo mục lục tự động
        ///// </summary>
        //private void TaoMucLuc(Document document, BaseFont bf, List<LopDTO> dsLop)
        //{
        //    iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        //    iTextSharp.text.Font sectionFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
        //    iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //    Paragraph mucLucTitle = new Paragraph("MỤC LỤC", headerFont);
        //    mucLucTitle.Alignment = Element.ALIGN_CENTER;
        //    mucLucTitle.SpacingBefore = 50;
        //    mucLucTitle.SpacingAfter = 30;
        //    document.Add(mucLucTitle);

        //    // Danh sách mục lục
        //    int currentPage = 3;

        //    // 1. Tổng quan toàn khối
        //    Paragraph item1 = new Paragraph($"1. Tổng quan toàn khối ........................................................... {currentPage}", sectionFont);
        //    item1.IndentationLeft = 30;
        //    item1.SpacingAfter = 10;
        //    document.Add(item1);

        //    // 2. Thống kê học lực theo khối
        //    Paragraph item2 = new Paragraph($"2. Thống kê học lực theo khối ............................................... {currentPage}", normalFont);
        //    item2.IndentationLeft = 50;
        //    item2.SpacingAfter = 8;
        //    document.Add(item2);

        //    // 3. Thống kê số lượng học sinh
        //    Paragraph item3 = new Paragraph($"3. Thống kê số lượng học sinh .............................................. {currentPage}", normalFont);
        //    item3.IndentationLeft = 50;
        //    item3.SpacingAfter = 8;
        //    document.Add(item3);

        //    // 4. Thống kê giới tính
        //    Paragraph item4 = new Paragraph($"4. Thống kê giới tính ............................................................... {currentPage}", normalFont);
        //    item4.IndentationLeft = 50;
        //    item4.SpacingAfter = 20;
        //    document.Add(item4);

        //    // 5. Danh sách lớp học
        //    if (dsLop.Count > 0)
        //    {
        //        Paragraph item5 = new Paragraph("5. Danh sách lớp học", sectionFont);
        //        item5.IndentationLeft = 30;
        //        item5.SpacingAfter = 10;
        //        document.Add(item5);

        //        // Sắp xếp theo khối
        //        var lopTheoKhoi = dsLop.OrderBy(l => l.MaKhoi).ThenBy(l => l.TenLop).ToList();

        //        foreach (var lop in lopTheoKhoi)
        //        {
        //            Paragraph lopItem = new Paragraph($"   - Lớp {lop.TenLop} ................................................................... {currentPage}", normalFont);
        //            lopItem.IndentationLeft = 60;
        //            lopItem.SpacingAfter = 5;
        //            document.Add(lopItem);
        //        }
        //    }
        //}

        /// <summary>
        /// Tạo trang tổng quan toàn khối
        /// </summary>
        private void TaoTrangTongQuan(Document document, BaseFont bf,
            Dictionary<string, int> thongKeKhoi10,
            Dictionary<string, int> thongKeKhoi11,
            Dictionary<string, int> thongKeKhoi12,
            int tongHSKhoi10, int tongHSKhoi11, int tongHSKhoi12,
            List<LopDTO> dsLop, int tongNam, int tongNu, string tenHocKy)
        {
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font sectionFont = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            // Tiêu đề
            Paragraph title = new Paragraph("TỔNG QUAN TOÀN KHỐI", headerFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingBefore = 30;
            title.SpacingAfter = 10;
            document.Add(title);

            Paragraph subtitle = new Paragraph(tenHocKy,
                new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.ITALIC, BaseColor.DARK_GRAY));
            subtitle.Alignment = Element.ALIGN_CENTER;
            subtitle.SpacingAfter = 20;
            document.Add(subtitle);

            // === 1. THỐNG KÊ TỔNG SỐ ===
            Paragraph section1 = new Paragraph("1. THỐNG KÊ TỔNG SỐ", sectionFont);
            section1.SpacingBefore = 15;
            section1.SpacingAfter = 10;
            document.Add(section1);

            int tongSoLop = dsLop.Count;
            int tongSoHocSinh = tongHSKhoi10 + tongHSKhoi11 + tongHSKhoi12;

            PdfPTable statsTable = new PdfPTable(2);
            statsTable.WidthPercentage = 70;
            statsTable.SetWidths(new float[] { 3f, 1f });
            statsTable.HorizontalAlignment = Element.ALIGN_LEFT;
            statsTable.SpacingAfter = 15;

            AddStatRow(statsTable, "Tổng số lớp:", tongSoLop.ToString(), bf, new BaseColor(33, 150, 243));
            AddStatRow(statsTable, "Tổng số học sinh:", tongSoHocSinh.ToString(), bf, new BaseColor(16, 185, 129));
            AddStatRow(statsTable, "Học sinh Nam:", $"{tongNam} ({(tongSoHocSinh > 0 ? (tongNam * 100.0 / tongSoHocSinh).ToString("0.0") : "0")}%)", bf, new BaseColor(59, 130, 246));
            AddStatRow(statsTable, "Học sinh Nữ:", $"{tongNu} ({(tongSoHocSinh > 0 ? (tongNu * 100.0 / tongSoHocSinh).ToString("0.0") : "0")}%)", bf, new BaseColor(236, 72, 153));

            document.Add(statsTable);

            // === 2. THỐNG KÊ HỌC LỰC THEO KHỐI ===
            Paragraph section2 = new Paragraph("2. THỐNG KÊ HỌC LỰC THEO KHỐI", sectionFont);
            section2.SpacingBefore = 15;
            section2.SpacingAfter = 10;
            document.Add(section2);

            PdfPTable hocLucTable = new PdfPTable(6);
            hocLucTable.WidthPercentage = 100;
            hocLucTable.SetWidths(new float[] { 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f });
            hocLucTable.SpacingAfter = 15;

            // Header
            AddHeaderCell(hocLucTable, "Khối", bf);
            AddHeaderCell(hocLucTable, "Giỏi", bf);
            AddHeaderCell(hocLucTable, "Khá", bf);
            AddHeaderCell(hocLucTable, "TB", bf);
            AddHeaderCell(hocLucTable, "Yếu", bf);
            AddHeaderCell(hocLucTable, "Kém", bf);

            // Khối 10
            if (tongHSKhoi10 > 0)
            {
                AddDataCell(hocLucTable, "Khối 10", boldFont, Element.ALIGN_LEFT);
                AddDataCell(hocLucTable, thongKeKhoi10["Giỏi"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi10["Khá"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi10["Trung bình"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi10["Yếu"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi10["Kém"].ToString(), normalFont, Element.ALIGN_CENTER);
            }

            // Khối 11
            if (tongHSKhoi11 > 0)
            {
                AddDataCell(hocLucTable, "Khối 11", boldFont, Element.ALIGN_LEFT);
                AddDataCell(hocLucTable, thongKeKhoi11["Giỏi"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi11["Khá"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi11["Trung bình"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi11["Yếu"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi11["Kém"].ToString(), normalFont, Element.ALIGN_CENTER);
            }

            // Khối 12
            if (tongHSKhoi12 > 0)
            {
                AddDataCell(hocLucTable, "Khối 12", boldFont, Element.ALIGN_LEFT);
                AddDataCell(hocLucTable, thongKeKhoi12["Giỏi"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi12["Khá"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi12["Trung bình"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi12["Yếu"].ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(hocLucTable, thongKeKhoi12["Kém"].ToString(), normalFont, Element.ALIGN_CENTER);
            }

            // Tổng cộng
            int tongGioi = thongKeKhoi10["Giỏi"] + thongKeKhoi11["Giỏi"] + thongKeKhoi12["Giỏi"];
            int tongKha = thongKeKhoi10["Khá"] + thongKeKhoi11["Khá"] + thongKeKhoi12["Khá"];
            int tongTB = thongKeKhoi10["Trung bình"] + thongKeKhoi11["Trung bình"] + thongKeKhoi12["Trung bình"];
            int tongYeu = thongKeKhoi10["Yếu"] + thongKeKhoi11["Yếu"] + thongKeKhoi12["Yếu"];
            int tongKem = thongKeKhoi10["Kém"] + thongKeKhoi11["Kém"] + thongKeKhoi12["Kém"];

            PdfPCell totalLabelCell = new PdfPCell(new Phrase("TỔNG CỘNG", boldFont));
            totalLabelCell.BackgroundColor = new BaseColor(254, 249, 195);
            totalLabelCell.HorizontalAlignment = Element.ALIGN_CENTER;
            totalLabelCell.Padding = 8;
            hocLucTable.AddCell(totalLabelCell);

            AddTotalCell(hocLucTable, tongGioi.ToString(), bf);
            AddTotalCell(hocLucTable, tongKha.ToString(), bf);
            AddTotalCell(hocLucTable, tongTB.ToString(), bf);
            AddTotalCell(hocLucTable, tongYeu.ToString(), bf);
            AddTotalCell(hocLucTable, tongKem.ToString(), bf);

            document.Add(hocLucTable);

            // === 3. TỶ LỆ HỌC LỰC ===
            Paragraph section3 = new Paragraph("3. TỶ LỆ HỌC LỰC", sectionFont);
            section3.SpacingBefore = 15;
            section3.SpacingAfter = 10;
            document.Add(section3);

            int tongCoXepLoai = tongGioi + tongKha + tongTB + tongYeu + tongKem;

            if (tongCoXepLoai > 0)
            {
                Paragraph tyLeGioi = new Paragraph($"• Giỏi: {tongGioi} học sinh ({(tongGioi * 100.0 / tongCoXepLoai):0.0}%)", normalFont);
                tyLeGioi.IndentationLeft = 20;
                tyLeGioi.SpacingAfter = 5;
                document.Add(tyLeGioi);

                Paragraph tyLeKha = new Paragraph($"• Khá: {tongKha} học sinh ({(tongKha * 100.0 / tongCoXepLoai):0.0}%)", normalFont);
                tyLeKha.IndentationLeft = 20;
                tyLeKha.SpacingAfter = 5;
                document.Add(tyLeKha);

                Paragraph tyLeTB = new Paragraph($"• Trung bình: {tongTB} học sinh ({(tongTB * 100.0 / tongCoXepLoai):0.0}%)", normalFont);
                tyLeTB.IndentationLeft = 20;
                tyLeTB.SpacingAfter = 5;
                document.Add(tyLeTB);

                Paragraph tyLeYeu = new Paragraph($"• Yếu: {tongYeu} học sinh ({(tongYeu * 100.0 / tongCoXepLoai):0.0}%)", normalFont);
                tyLeYeu.IndentationLeft = 20;
                tyLeYeu.SpacingAfter = 5;
                document.Add(tyLeYeu);

                Paragraph tyLeKem = new Paragraph($"• Kém: {tongKem} học sinh ({(tongKem * 100.0 / tongCoXepLoai):0.0}%)", normalFont);
                tyLeKem.IndentationLeft = 20;
                document.Add(tyLeKem);
            }
            else
            {
                Paragraph noData = new Paragraph("Chưa có dữ liệu xếp loại",
                    new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.ITALIC, BaseColor.GRAY));
                noData.IndentationLeft = 20;
                document.Add(noData);
            }

            // === FOOTER ===
            document.Add(new Paragraph("\n\n"));
            Paragraph footer = new Paragraph(
                $"Báo cáo được tạo tự động bởi Hệ thống Quản lý Học sinh\n{DateTime.Now:dd/MM/yyyy HH:mm}",
                new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.ITALIC, BaseColor.GRAY));
            footer.Alignment = Element.ALIGN_CENTER;
            document.Add(footer);
        }

        /// <summary>
        /// Thêm dòng thống kê với màu
        /// </summary>
        private void AddStatRow(PdfPTable table, string label, string value, BaseFont bf, BaseColor color)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label,
                new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
            labelCell.Border = iTextRectangle.NO_BORDER;
            labelCell.PaddingBottom = 8;
            table.AddCell(labelCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value,
                new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, color)));
            valueCell.Border = iTextRectangle.NO_BORDER;
            valueCell.PaddingBottom = 8;
            valueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(valueCell);
        }

        /// <summary>
        /// Thêm header cell cho bảng
        /// </summary>
        private void AddHeaderCell(PdfPTable table, string text, BaseFont bf)
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
        private void AddDataCell(PdfPTable table, string text, iTextSharp.text.Font font, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 8;
            cell.BorderColor = new BaseColor(229, 231, 235);
            table.AddCell(cell);
        }

        /// <summary>
        /// Thêm cell tổng cộng
        /// </summary>
        private void AddTotalCell(PdfPTable table, string text, BaseFont bf)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text,
                new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            cell.BackgroundColor = new BaseColor(254, 249, 195);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 8;
            table.AddCell(cell);
        }

        /// <summary>
        /// Helper class để xử lý page event
        /// </summary>
        private class PDFPageEventHelper : PdfPageEventHelper
        {
            private BaseFont bf;
            private iTextSharp.text.Font footerFont;

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    footerFont = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);
                }
                catch
                {
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    footerFont = new iTextSharp.text.Font(bf, 9);
                }
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                // Thêm số trang ở footer (trừ trang bìa)
                if (writer.PageNumber > 1)
                {
                    PdfPTable footer = new PdfPTable(1);
                    footer.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    footer.DefaultCell.Border = iTextRectangle.NO_BORDER;

                    PdfPCell pageCell = new PdfPCell(new Phrase($"Trang {writer.PageNumber - 1}", footerFont));
                    pageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pageCell.Border = iTextRectangle.NO_BORDER;
                    footer.AddCell(pageCell);

                    footer.WriteSelectedRows(0, -1, document.LeftMargin,
                        document.BottomMargin - 10, writer.DirectContent);
                }
            }
        }

        private void baoCaoKhoi1_Load(object sender, EventArgs e)
        {

        }

        private void pnlStatisticsHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlStatisticsFooter_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
