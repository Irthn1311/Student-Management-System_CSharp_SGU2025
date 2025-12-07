using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Student_Management_System_CSharp_SGU2025.BUS.Services
{
    /// <summary>
    /// Service for exporting Timetable to Excel
    /// Uses EPPlus (OfficeOpenXml) library
    /// </summary>
    public class TKBExportService
    {
        private readonly ThoiKhoaBieuBUS _tkbBUS;
        private readonly LopHocBUS _lopBUS;
        private readonly GiaoVienBUS _giaoVienBUS;
        private readonly HocKyBUS _hocKyBUS;

        public TKBExportService()
        {
            _tkbBUS = new ThoiKhoaBieuBUS();
            _lopBUS = new LopHocBUS();
            _giaoVienBUS = new GiaoVienBUS();
            _hocKyBUS = new HocKyBUS();
        }

        /// <summary>
        /// Export timetable by Class (one worksheet per class)
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ</param>
        /// <param name="savePath">Đường dẫn lưu file</param>
        public void ExportClassSchedule(int maHocKy, string savePath)
        {
            // Set EPPlus license context (required for non-commercial use)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Load data
            var allSlots = _tkbBUS.GetTKBViewByHocKy(maHocKy);
            var classes = _lopBUS.DocDSLop();
            var hocKy = _hocKyBUS.LayHocKyTheoMa(maHocKy);

            if (allSlots == null || allSlots.Count == 0)
            {
                throw new Exception("Không có dữ liệu thời khóa biểu cho học kỳ này.");
            }

            // Get unique classes that have TKB data
            var classesWithTKB = allSlots
                .Select(s => s.MaLop)
                .Distinct()
                .Join(classes, maLop => maLop, lop => lop.maLop, (maLop, lop) => lop)
                .OrderBy(l => l.tenLop)
                .ToList();

            if (classesWithTKB.Count == 0)
            {
                throw new Exception("Không tìm thấy lớp nào có dữ liệu thời khóa biểu.");
            }

            // Create Excel package
            using (var package = new ExcelPackage())
            {
                // Remove default worksheet
                package.Workbook.Worksheets.Delete("Sheet1");

                string hocKyName = hocKy != null ? hocKy.TenHocKy : $"Học kỳ {maHocKy}";

                // Create one worksheet per class
                foreach (var lop in classesWithTKB)
                {
                    var slots = allSlots.Where(s => s.MaLop == lop.maLop).ToList();
                    CreateClassWorksheet(package, lop.tenLop, slots, hocKyName);
                }

                // Save file
                var fileInfo = new FileInfo(savePath);
                package.SaveAs(fileInfo);
            }
        }

        /// <summary>
        /// Export timetable by Teacher (one worksheet per teacher)
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ</param>
        /// <param name="savePath">Đường dẫn lưu file</param>
        public void ExportTeacherSchedule(int maHocKy, string savePath)
        {
            // Set EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Load data
            var allSlots = _tkbBUS.GetTKBViewByHocKy(maHocKy);
            var teachers = _giaoVienBUS.DocDSGiaoVien();
            var hocKy = _hocKyBUS.LayHocKyTheoMa(maHocKy);

            if (allSlots == null || allSlots.Count == 0)
            {
                throw new Exception("Không có dữ liệu thời khóa biểu cho học kỳ này.");
            }

            // Get unique teachers that have TKB data
            var teachersWithTKB = allSlots
                .Select(s => s.MaGiaoVien)
                .Distinct()
                .Join(teachers, maGV => maGV, gv => gv.MaGiaoVien, (maGV, gv) => gv)
                .OrderBy(gv => gv.HoTen)
                .ToList();

            if (teachersWithTKB.Count == 0)
            {
                throw new Exception("Không tìm thấy giáo viên nào có dữ liệu thời khóa biểu.");
            }

            // Create Excel package
            using (var package = new ExcelPackage())
            {
                // Remove default worksheet
                package.Workbook.Worksheets.Delete("Sheet1");

                string hocKyName = hocKy != null ? hocKy.TenHocKy : $"Học kỳ {maHocKy}";

                // Create one worksheet per teacher
                foreach (var gv in teachersWithTKB)
                {
                    var slots = allSlots.Where(s => s.MaGiaoVien == gv.MaGiaoVien).ToList();
                    CreateTeacherWorksheet(package, gv.HoTen, gv.MaGiaoVien, slots, hocKyName);
                }

                // Save file
                var fileInfo = new FileInfo(savePath);
                package.SaveAs(fileInfo);
            }
        }

        /// <summary>
        /// Create a worksheet for a class
        /// </summary>
        private void CreateClassWorksheet(ExcelPackage package, string tenLop, List<TimeTableSlotDTO> slots, string hocKyName)
        {
            // Create worksheet (Excel sheet name max 31 chars)
            string sheetName = tenLop.Length > 31 ? tenLop.Substring(0, 31) : tenLop;
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // Header
            worksheet.Cells[1, 1].Value = $"THỜI KHÓA BIỂU LỚP {tenLop} - {hocKyName}";
            worksheet.Cells[1, 1, 1, 6].Merge = true;
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1].Style.Font.Size = 14;
            worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Row(1).Height = 25;

            // Column headers: Tiết, Thứ 2, Thứ 3, Thứ 4, Thứ 5, Thứ 6
            worksheet.Cells[2, 1].Value = "Tiết";
            worksheet.Cells[2, 2].Value = "Thứ 2";
            worksheet.Cells[2, 3].Value = "Thứ 3";
            worksheet.Cells[2, 4].Value = "Thứ 4";
            worksheet.Cells[2, 5].Value = "Thứ 5";
            worksheet.Cells[2, 6].Value = "Thứ 6";

            // Style header row
            for (int col = 1; col <= 6; col++)
            {
                var cell = worksheet.Cells[2, col];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(59, 130, 246)); // Blue
                cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
            worksheet.Row(2).Height = 20;

            // Period rows (Tiết 1 to Tiết 10)
            for (int tiet = 1; tiet <= 10; tiet++)
            {
                int row = tiet + 2; // Start from row 3
                worksheet.Cells[row, 1].Value = $"Tiết {tiet}";
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[row, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(243, 244, 246)); // Light gray

                // Fill data for each day (Thứ 2-7, columns 2-7)
                for (int thu = 2; thu <= 7; thu++)
                {
                    int col = thu; // Column 2 = Thứ 2, Column 7 = Thứ 7
                    var slot = slots.FirstOrDefault(s => s.Thu == thu && s.Tiet == tiet);

                    var cell = worksheet.Cells[row, col];
                    if (slot != null)
                    {
                        cell.Value = $"{slot.TenMon}\n{slot.TenGiaoVien}";
                        cell.Style.WrapText = true;
                    }
                    else
                    {
                        cell.Value = "";
                    }

                    // Style cell
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    if (slot != null)
                    {
                        // Light background color for filled cells
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(239, 246, 255)); // Light blue
                    }
                    else
                    {
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                    }
                }
                worksheet.Row(row).Height = 40; // Height for wrapped text
            }

            // Auto-fit columns
            worksheet.Column(1).Width = 10; // Tiết column
            for (int col = 2; col <= 6; col++)
            {
                worksheet.Column(col).Width = 20; // Day columns
            }
        }

        /// <summary>
        /// Create a worksheet for a teacher
        /// </summary>
        private void CreateTeacherWorksheet(ExcelPackage package, string tenGiaoVien, string maGiaoVien, List<TimeTableSlotDTO> slots, string hocKyName)
        {
            // Create worksheet (Excel sheet name max 31 chars)
            string sheetName = tenGiaoVien.Length > 31 ? tenGiaoVien.Substring(0, 31) : tenGiaoVien;
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // Header
            worksheet.Cells[1, 1].Value = $"THỜI KHÓA BIỂU GIÁO VIÊN {tenGiaoVien} ({maGiaoVien}) - {hocKyName}";
            worksheet.Cells[1, 1, 1, 6].Merge = true;
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1].Style.Font.Size = 14;
            worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Row(1).Height = 25;

            // Column headers: Tiết, Thứ 2, Thứ 3, Thứ 4, Thứ 5, Thứ 6, Thứ 7
            worksheet.Cells[2, 1].Value = "Tiết";
            worksheet.Cells[2, 2].Value = "Thứ 2";
            worksheet.Cells[2, 3].Value = "Thứ 3";
            worksheet.Cells[2, 4].Value = "Thứ 4";
            worksheet.Cells[2, 5].Value = "Thứ 5";
            worksheet.Cells[2, 6].Value = "Thứ 6";
            worksheet.Cells[2, 7].Value = "Thứ 7";

            // Style header row
            for (int col = 1; col <= 7; col++)
            {
                var cell = worksheet.Cells[2, col];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(34, 197, 94)); // Green
                cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
            worksheet.Row(2).Height = 20;

            // Period rows (Tiết 1 to Tiết 10)
            for (int tiet = 1; tiet <= 10; tiet++)
            {
                int row = tiet + 2; // Start from row 3
                worksheet.Cells[row, 1].Value = $"Tiết {tiet}";
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                worksheet.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[row, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(243, 244, 246)); // Light gray

                // Fill data for each day (Thứ 2-7, columns 2-7)
                for (int thu = 2; thu <= 7; thu++)
                {
                    int col = thu; // Column 2 = Thứ 2, Column 7 = Thứ 7
                    var slot = slots.FirstOrDefault(s => s.Thu == thu && s.Tiet == tiet);

                    var cell = worksheet.Cells[row, col];
                    if (slot != null)
                    {
                        cell.Value = $"{slot.TenMon}\n{slot.TenLop}";
                        cell.Style.WrapText = true;
                    }
                    else
                    {
                        cell.Value = "";
                    }

                    // Style cell
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    if (slot != null)
                    {
                        // Light background color for filled cells
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(240, 253, 244)); // Light green
                    }
                    else
                    {
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
                    }
                }
                worksheet.Row(row).Height = 40; // Height for wrapped text
            }

            // Auto-fit columns
            worksheet.Column(1).Width = 10; // Tiết column
            for (int col = 2; col <= 7; col++)
            {
                worksheet.Column(col).Width = 20; // Day columns
            }
        }

        /// <summary>
        /// ✅ Xuất thời khóa biểu theo lớp ra PDF với thiết kế đẹp mắt
        /// </summary>
        public void ExportClassScheduleToPDF(int maHocKy, string savePath)
        {
            // Load data
            var allSlots = _tkbBUS.GetTKBViewByHocKy(maHocKy);
            var classes = _lopBUS.DocDSLop();
            var hocKy = _hocKyBUS.LayHocKyTheoMa(maHocKy);

            if (allSlots == null || allSlots.Count == 0)
            {
                throw new Exception("Không có dữ liệu thời khóa biểu cho học kỳ này.");
            }

            // Get unique classes that have TKB data
            var classesWithTKB = allSlots
                .Select(s => s.MaLop)
                .Distinct()
                .Join(classes, maLop => maLop, lop => lop.maLop, (maLop, lop) => lop)
                .OrderBy(l => l.tenLop)
                .ToList();

            if (classesWithTKB.Count == 0)
            {
                throw new Exception("Không tìm thấy lớp nào có dữ liệu thời khóa biểu.");
            }

            string hocKyName = hocKy != null ? hocKy.TenHocKy : $"Học kỳ {maHocKy}";

            // Create PDF document (Landscape orientation for better table display)
            Document document = new Document(PageSize.A4.Rotate(), 20, 20, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
            document.Open();

            // Font hỗ trợ tiếng Việt
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            
            Font titleFont = new Font(bf, 18, Font.BOLD, BaseColor.WHITE);
            Font headerFont = new Font(bf, 12, Font.BOLD, BaseColor.BLACK);
            Font normalFont = new Font(bf, 10, Font.NORMAL, BaseColor.BLACK);
            Font boldFont = new Font(bf, 10, Font.BOLD, BaseColor.BLACK);

            // Create one page per class
            foreach (var lop in classesWithTKB)
            {
                var slots = allSlots.Where(s => s.MaLop == lop.maLop).ToList();
                
                // Header với màu đẹp
                PdfPTable headerTable = new PdfPTable(1);
                headerTable.WidthPercentage = 100;
                PdfPCell headerCell = new PdfPCell(new Phrase($"THỜI KHÓA BIỂU LỚP {lop.tenLop.ToUpper()} - {hocKyName.ToUpper()}", titleFont));
                headerCell.BackgroundColor = new BaseColor(59, 130, 246); // Blue
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.Padding = 15;
                headerCell.Border = Rectangle.NO_BORDER;
                headerTable.AddCell(headerCell);
                document.Add(headerTable);
                document.Add(new Paragraph("\n"));

                // Create timetable table: 7 columns (Tiết + 6 days)
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1.2f, 1f, 1f, 1f, 1f, 1f, 1f });

                // Header row
                string[] headers = { "Tiết", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
                foreach (string header in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, headerFont));
                    cell.BackgroundColor = new BaseColor(59, 130, 246); // Blue
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Padding = 8;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = BaseColor.WHITE;
                    table.AddCell(cell);
                }

                // Data rows (Tiết 1-10)
                for (int tiet = 1; tiet <= 10; tiet++)
                {
                    // Tiết column
                    PdfPCell tietCell = new PdfPCell(new Phrase($"Tiết {tiet}", boldFont));
                    tietCell.BackgroundColor = new BaseColor(243, 244, 246); // Light gray
                    tietCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tietCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tietCell.Padding = 8;
                    tietCell.Border = Rectangle.BOX;
                    tietCell.BorderColor = BaseColor.WHITE;
                    table.AddCell(tietCell);

                    // Day columns (Thứ 2-7)
                    for (int thu = 2; thu <= 7; thu++)
                    {
                        var slot = slots.FirstOrDefault(s => s.Thu == thu && s.Tiet == tiet);
                        PdfPCell cell = new PdfPCell();
                        
                        if (slot != null)
                        {
                            // Add content with line break
                            Phrase phrase = new Phrase();
                            phrase.Add(new Chunk(slot.TenMon, boldFont));
                            phrase.Add(new Chunk("\n", normalFont));
                            phrase.Add(new Chunk(slot.TenGiaoVien, normalFont));
                            cell.AddElement(phrase);
                            cell.BackgroundColor = new BaseColor(239, 246, 255); // Light blue
                        }
                        else
                        {
                            cell.BackgroundColor = BaseColor.WHITE;
                        }

                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Padding = 8;
                        cell.Border = Rectangle.BOX;
                        cell.BorderColor = BaseColor.WHITE;
                        cell.FixedHeight = 50f;
                        table.AddCell(cell);
                    }
                }

                document.Add(table);

                // Add page break if not last class
                if (lop != classesWithTKB.Last())
                {
                    document.NewPage();
                }
            }

            document.Close();
        }

        /// <summary>
        /// ✅ Xuất thời khóa biểu theo giáo viên ra PDF với thiết kế đẹp mắt
        /// </summary>
        public void ExportTeacherScheduleToPDF(int maHocKy, string savePath)
        {
            // Load data
            var allSlots = _tkbBUS.GetTKBViewByHocKy(maHocKy);
            var teachers = _giaoVienBUS.DocDSGiaoVien();
            var hocKy = _hocKyBUS.LayHocKyTheoMa(maHocKy);

            if (allSlots == null || allSlots.Count == 0)
            {
                throw new Exception("Không có dữ liệu thời khóa biểu cho học kỳ này.");
            }

            // Get unique teachers that have TKB data
            var teachersWithTKB = allSlots
                .Select(s => s.MaGiaoVien)
                .Distinct()
                .Join(teachers, maGV => maGV, gv => gv.MaGiaoVien, (maGV, gv) => gv)
                .OrderBy(gv => gv.HoTen)
                .ToList();

            if (teachersWithTKB.Count == 0)
            {
                throw new Exception("Không tìm thấy giáo viên nào có dữ liệu thời khóa biểu.");
            }

            string hocKyName = hocKy != null ? hocKy.TenHocKy : $"Học kỳ {maHocKy}";

            // Create PDF document (Landscape orientation)
            Document document = new Document(PageSize.A4.Rotate(), 20, 20, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
            document.Open();

            // Font hỗ trợ tiếng Việt
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            
            Font titleFont = new Font(bf, 18, Font.BOLD, BaseColor.WHITE);
            Font headerFont = new Font(bf, 12, Font.BOLD, BaseColor.BLACK);
            Font normalFont = new Font(bf, 10, Font.NORMAL, BaseColor.BLACK);
            Font boldFont = new Font(bf, 10, Font.BOLD, BaseColor.BLACK);

            // Create one page per teacher
            foreach (var gv in teachersWithTKB)
            {
                var slots = allSlots.Where(s => s.MaGiaoVien == gv.MaGiaoVien).ToList();
                
                // Header với màu đẹp (màu xanh lá)
                PdfPTable headerTable = new PdfPTable(1);
                headerTable.WidthPercentage = 100;
                PdfPCell headerCell = new PdfPCell(new Phrase($"THỜI KHÓA BIỂU GIẢNG DẠY\n{gv.HoTen.ToUpper()} ({gv.MaGiaoVien}) - {hocKyName.ToUpper()}", titleFont));
                headerCell.BackgroundColor = new BaseColor(34, 197, 94); // Green
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.Padding = 15;
                headerCell.Border = Rectangle.NO_BORDER;
                headerTable.AddCell(headerCell);
                document.Add(headerTable);
                document.Add(new Paragraph("\n"));

                // Create timetable table: 7 columns (Tiết + 6 days)
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1.2f, 1f, 1f, 1f, 1f, 1f, 1f });

                // Header row
                string[] headers = { "Tiết", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
                foreach (string header in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, headerFont));
                    cell.BackgroundColor = new BaseColor(34, 197, 94); // Green
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Padding = 8;
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = BaseColor.WHITE;
                    table.AddCell(cell);
                }

                // Data rows (Tiết 1-10)
                for (int tiet = 1; tiet <= 10; tiet++)
                {
                    // Tiết column
                    PdfPCell tietCell = new PdfPCell(new Phrase($"Tiết {tiet}", boldFont));
                    tietCell.BackgroundColor = new BaseColor(243, 244, 246); // Light gray
                    tietCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tietCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tietCell.Padding = 8;
                    tietCell.Border = Rectangle.BOX;
                    tietCell.BorderColor = BaseColor.WHITE;
                    table.AddCell(tietCell);

                    // Day columns (Thứ 2-7)
                    for (int thu = 2; thu <= 7; thu++)
                    {
                        var slot = slots.FirstOrDefault(s => s.Thu == thu && s.Tiet == tiet);
                        PdfPCell cell = new PdfPCell();
                        
                        if (slot != null)
                        {
                            // Add content with line break
                            Phrase phrase = new Phrase();
                            phrase.Add(new Chunk(slot.TenLop, boldFont));
                            phrase.Add(new Chunk("\n", normalFont));
                            phrase.Add(new Chunk(slot.TenMon, normalFont));
                            cell.AddElement(phrase);
                            cell.BackgroundColor = new BaseColor(240, 253, 244); // Light green
                        }
                        else
                        {
                            cell.BackgroundColor = BaseColor.WHITE;
                        }

                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Padding = 8;
                        cell.Border = Rectangle.BOX;
                        cell.BorderColor = BaseColor.WHITE;
                        cell.FixedHeight = 50f;
                        table.AddCell(cell);
                    }
                }

                document.Add(table);

                // Add page break if not last teacher
                if (gv != teachersWithTKB.Last())
                {
                    document.NewPage();
                }
            }

            document.Close();
        }
    }
}

