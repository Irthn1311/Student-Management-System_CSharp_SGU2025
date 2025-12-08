using iTextSharp.text;
using iTextSharp.text.pdf;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.IO;

namespace Student_Management_System_CSharp_SGU2025.BUS.Services
{
    /// <summary>
    /// Service để tạo PDF cho yêu cầu chuyển lớp
    /// </summary>
    public class YeuCauChuyenLopPDFService
    {
        /// <summary>
        /// Tạo file PDF cho yêu cầu chuyển lớp
        /// </summary>
        /// <param name="yeuCau">Thông tin yêu cầu chuyển lớp</param>
        /// <param name="tenHocSinh">Tên học sinh</param>
        /// <param name="tenLopHienTai">Tên lớp hiện tại</param>
        /// <param name="tenLopMongMuon">Tên lớp mong muốn (có thể null)</param>
        /// <param name="tenHocKy">Tên học kỳ</param>
        /// <param name="tenNamHoc">Tên năm học</param>
        /// <param name="khoiHienTai">Khối hiện tại</param>
        /// <returns>Đường dẫn file PDF đã tạo</returns>
        public static string TaoPDFYeuCauChuyenLop(
            YeuCauChuyenLopDTO yeuCau,
            string tenHocSinh,
            string tenLopHienTai,
            string tenLopMongMuon,
            string tenHocKy,
            string tenNamHoc,
            int khoiHienTai)
        {
            try
            {
                // Tạo thư mục lưu PDF nếu chưa có
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string pdfFolder = Path.Combine(appPath, "PDFs", "YeuCauChuyenLop");
                if (!Directory.Exists(pdfFolder))
                {
                    Directory.CreateDirectory(pdfFolder);
                }

                // Tên file: YeuCauChuyenLop_MaYC_MaHS_NgayTao.pdf
                string fileName = $"YeuCauChuyenLop_{yeuCau.MaYeuCau}_{yeuCau.MaHocSinh}_{yeuCau.NgayTao:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(pdfFolder, fileName);

                // Tạo document PDF với margins tốt hơn
                Document document = new Document(PageSize.A4, 50, 50, 70, 70);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // Load font tiếng Việt
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont bf;
                try
                {
                    bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
                catch
                {
                    // Fallback nếu không tìm thấy font
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                }

                // Fonts với kích thước tối ưu
                iTextSharp.text.Font smallFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                iTextSharp.text.Font boldFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                // Header: CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM
                Paragraph header1 = new Paragraph("CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM", headerFont);
                header1.Alignment = Element.ALIGN_CENTER;
                header1.SpacingAfter = 5;
                document.Add(header1);

                // Độc lập - Tự do - Hạnh phúc
                Paragraph header2 = new Paragraph("Độc lập - Tự do - Hạnh phúc", headerFont);
                header2.Alignment = Element.ALIGN_CENTER;
                header2.SpacingAfter = 25;
                document.Add(header2);

                // Tiêu đề: ĐƠN XIN CHUYỂN LỚP với underline
                Paragraph title = new Paragraph("ĐƠN XIN CHUYỂN LỚP", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 25;
                document.Add(title);

                // Kính gửi
                Paragraph kinhGui = new Paragraph("Kính gửi: Ban Giám Hiệu trường THPT", normalFont);
                kinhGui.SpacingAfter = 20;
                document.Add(kinhGui);

                // Thông tin học sinh trong bảng đẹp hơn
                AddInfoRow(document, "Tôi tên là:", tenHocSinh, normalFont, boldFont);
                AddInfoRow(document, "Hiện đang học lớp:", $"{tenLopHienTai} (Khối {khoiHienTai})", normalFont, boldFont);
                AddInfoRow(document, "Học kỳ:", $"{tenHocKy} - {tenNamHoc}", normalFont, boldFont);
                AddInfoRow(document, "Mã học sinh:", yeuCau.MaHocSinh.ToString(), normalFont, boldFont);
                
                document.Add(new Paragraph("\n"));

                // Lý do xin chuyển lớp với box đẹp hơn
                Paragraph lyDoTitle = new Paragraph("Lý do xin chuyển lớp:", boldFont);
                lyDoTitle.SpacingBefore = 10;
                lyDoTitle.SpacingAfter = 10;
                document.Add(lyDoTitle);

                // Nội dung lý do trong box
                AddContentBox(document, yeuCau.LyDoYeuCau, normalFont);
                
                document.Add(new Paragraph("\n"));
                
                // Lớp mong muốn
                if (!string.IsNullOrWhiteSpace(tenLopMongMuon))
                {
                    AddInfoRow(document, "Xin được chuyển đến lớp:", tenLopMongMuon, normalFont, boldFont);
                }
                else
                {
                    AddInfoRow(document, "Xin được chuyển đến lớp:", "Để Ban Giám Hiệu quyết định", normalFont, boldFont);
                }

                // Chữ ký với layout đẹp hơn
                document.Add(new Paragraph("\n\n"));
                
                // Tạo table cho phần chữ ký
                PdfPTable signatureTable = new PdfPTable(1);
                signatureTable.WidthPercentage = 40;
                signatureTable.HorizontalAlignment = Element.ALIGN_RIGHT;
                signatureTable.DefaultCell.Border = Rectangle.NO_BORDER;
                
                Paragraph ngayThang = new Paragraph($"Ngày {yeuCau.NgayTao:dd} tháng {yeuCau.NgayTao:MM} năm {yeuCau.NgayTao:yyyy}", normalFont);
                ngayThang.Alignment = Element.ALIGN_CENTER;
                PdfPCell dateCell = new PdfPCell(ngayThang);
                dateCell.Border = Rectangle.NO_BORDER;
                dateCell.PaddingBottom = 15;
                signatureTable.AddCell(dateCell);
                
                Paragraph nguoiKy = new Paragraph("Người làm đơn", boldFont);
                nguoiKy.Alignment = Element.ALIGN_CENTER;
                PdfPCell signCell = new PdfPCell(nguoiKy);
                signCell.Border = Rectangle.NO_BORDER;
                signCell.PaddingTop = 30;
                signatureTable.AddCell(signCell);
                
                document.Add(signatureTable);
                
                // Thông tin xử lý (nếu đã xử lý) - thêm vào cuối
                if (yeuCau.TrangThai != "Chờ duyệt" && yeuCau.NgayXuLy.HasValue)
                {
                    document.Add(new Paragraph("\n\n"));
                    
                    // Đường kẻ ngang đẹp hơn
                    PdfPTable dividerTable = new PdfPTable(1);
                    dividerTable.WidthPercentage = 100;
                    dividerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    PdfPCell dividerCell = new PdfPCell(new Phrase(""));
                    dividerCell.Border = Rectangle.BOTTOM_BORDER;
                    dividerCell.BorderWidth = 1f;
                    dividerCell.BorderColor = BaseColor.GRAY;
                    dividerCell.PaddingBottom = 10;
                    dividerTable.AddCell(dividerCell);
                    document.Add(dividerTable);
                    
                    document.Add(new Paragraph("\n"));
                    
                    Paragraph xuLyTitle = new Paragraph("PHẦN XỬ LÝ CỦA BAN GIÁM HIỆU", boldFont);
                    xuLyTitle.Alignment = Element.ALIGN_CENTER;
                    xuLyTitle.SpacingAfter = 20;
                    document.Add(xuLyTitle);
                    
                    AddInfoRow(document, "Trạng thái:", yeuCau.TrangThai, normalFont, boldFont);
                    AddInfoRow(document, "Ngày xử lý:", yeuCau.NgayXuLy.Value.ToString("dd/MM/yyyy HH:mm"), normalFont, boldFont);
                    
                    if (!string.IsNullOrWhiteSpace(yeuCau.NguoiXuLy))
                    {
                        AddInfoRow(document, "Người xử lý:", yeuCau.NguoiXuLy, normalFont, boldFont);
                    }
                    
                    if (yeuCau.TrangThai == "Đã duyệt" && !string.IsNullOrEmpty(yeuCau.TenLopDuocDuyet))
                    {
                        AddInfoRow(document, "Lớp được duyệt:", yeuCau.TenLopDuocDuyet, normalFont, boldFont);
                    }
                    
                    if (!string.IsNullOrWhiteSpace(yeuCau.GhiChuAdmin))
                    {
                        document.Add(new Paragraph("\n"));
                        Paragraph ghiChuTitle = new Paragraph("Ghi chú:", boldFont);
                        ghiChuTitle.SpacingAfter = 10;
                        document.Add(ghiChuTitle);
                        AddContentBox(document, yeuCau.GhiChuAdmin, normalFont);
                    }
                }

                document.Close();
                writer.Close();

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tạo PDF yêu cầu chuyển lớp: {ex.Message}");
                throw new Exception($"Không thể tạo file PDF: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thêm dòng thông tin với nhãn và giá trị (không có dấu chấm)
        /// </summary>
        private static void AddInfoRow(Document document, string label, string value,
            iTextSharp.text.Font normalFont, iTextSharp.text.Font boldFont)
        {
            // Tạo table với 2 cột: nhãn và giá trị
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 0.30f, 0.70f });
            table.SpacingAfter = 10;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            
            // Cột 1: Nhãn
            PdfPCell labelCell = new PdfPCell(new Phrase(label, boldFont));
            labelCell.Border = Rectangle.NO_BORDER;
            labelCell.PaddingBottom = 6;
            labelCell.PaddingLeft = 0;
            labelCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(labelCell);
            
            // Cột 2: Giá trị
            PdfPCell valueCell = new PdfPCell(new Phrase(value ?? "", normalFont));
            valueCell.Border = Rectangle.NO_BORDER;
            valueCell.PaddingBottom = 6;
            valueCell.PaddingLeft = 5;
            valueCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(valueCell);
            
            document.Add(table);
        }

        /// <summary>
        /// Thêm nội dung trong box đẹp (cho lý do, ghi chú)
        /// </summary>
        private static void AddContentBox(Document document, string content, iTextSharp.text.Font normalFont)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                // Nếu không có nội dung, tạo box trống
                new PdfPTable(1).WidthPercentage = 100;
                new PdfPTable(1).SpacingAfter = 10;
                new PdfPTable(1).DefaultCell.Border = Rectangle.BOX;
                new PdfPTable(1).DefaultCell.BorderWidth = 1f;
                new PdfPTable(1).DefaultCell.BorderColor = new BaseColor(200, 200, 200);
                new PdfPTable(1).DefaultCell.Padding = 10;
                new PdfPTable(1).DefaultCell.MinimumHeight = 50;

                PdfPCell cell = new PdfPCell(new Phrase("", normalFont));
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1f;
                cell.BorderColor = new BaseColor(200, 200, 200);
                cell.Padding = 10;
                cell.MinimumHeight = 50;
                new PdfPTable(1).AddCell(cell);
                document.Add(new PdfPTable(1));
                return;
            }

            // Chia nội dung thành các dòng
            string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length == 0)
            {
                // Nếu không có dòng nào, tạo box trống
                new PdfPTable(1).WidthPercentage = 100;
                new PdfPTable(1).SpacingAfter = 10;
                new PdfPTable(1).DefaultCell.Border = Rectangle.BOX;
                new PdfPTable(1).DefaultCell.BorderWidth = 1f;
                new PdfPTable(1).DefaultCell.BorderColor = new BaseColor(200, 200, 200);
                new PdfPTable(1).DefaultCell.Padding = 10;
                new PdfPTable(1).DefaultCell.MinimumHeight = 50;

                PdfPCell cell = new PdfPCell(new Phrase("", normalFont));
                cell.Border = Rectangle.BOX;
                cell.BorderWidth = 1f;
                cell.BorderColor = new BaseColor(200, 200, 200);
                cell.Padding = 10;
                cell.MinimumHeight = 50;
                new PdfPTable(1).AddCell(cell);
                document.Add(new PdfPTable(1));
                return;
            }

            // Tạo box với border đẹp
            PdfPTable contentTable = new PdfPTable(1);
            contentTable.WidthPercentage = 100;
            contentTable.SpacingAfter = 10;
            contentTable.DefaultCell.Border = Rectangle.BOX;
            contentTable.DefaultCell.BorderWidth = 1f;
            contentTable.DefaultCell.BorderColor = new BaseColor(200, 200, 200);
            contentTable.DefaultCell.Padding = 12;

            // Thêm từng dòng vào box
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    PdfPCell contentCell = new PdfPCell(new Phrase(line.Trim(), normalFont));
                    contentCell.Border = Rectangle.NO_BORDER;
                    contentCell.PaddingBottom = 5;
                    contentCell.PaddingTop = 0;
                    contentCell.PaddingLeft = 0;
                    contentCell.PaddingRight = 0;
                    contentTable.AddCell(contentCell);
                }
            }

            // Nếu chỉ có 1 dòng, đảm bảo box có chiều cao tối thiểu
            if (lines.Length == 1)
            {
                PdfPCell firstCell = contentTable.GetRow(0).GetCells()[0];
                firstCell.MinimumHeight = 50;
            }

            document.Add(contentTable);
        }
    }
}

