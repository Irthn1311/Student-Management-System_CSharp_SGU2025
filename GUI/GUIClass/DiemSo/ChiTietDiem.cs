using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Services;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextRectangle = iTextSharp.text.Rectangle;
namespace Student_Management_System_CSharp_SGU2025.GUI.DiemSo
{
    public partial class ChiTietDiem : Form
    {
        private string maHocSinh;
        private int maHocKy;
        private NhapDiemBUS nhapDiemBUS;
        private ChiTietDiemDTO currentDiemDTO;
        private MonHocFilterService monHocFilterService;

        public ChiTietDiem()
        {
            InitializeComponent();
            nhapDiemBUS = new NhapDiemBUS();
            monHocFilterService = new MonHocFilterService();
        }

        // Constructor với tham số
        public ChiTietDiem(string maHocSinh, int maHocKy) : this()
        {
            this.maHocSinh = maHocSinh;
            this.maHocKy = maHocKy;
        }

        private void ChiTietDiem_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadChiTietDiem();
        }

        /// <summary>
        /// Cấu hình DataGridView để hiển thị điểm
        /// </summary>
        private void ConfigureDataGridView()
        {
            // Cấu hình header
            dgvChiTietDiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            dgvChiTietDiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvChiTietDiem.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            dgvChiTietDiem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvChiTietDiem.ColumnHeadersHeight = 45;
            dgvChiTietDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cấu hình cells
            dgvChiTietDiem.DefaultCellStyle.BackColor = Color.White;
            dgvChiTietDiem.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvChiTietDiem.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9);
            dgvChiTietDiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            dgvChiTietDiem.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            dgvChiTietDiem.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Cấu hình rows
            dgvChiTietDiem.RowTemplate.Height = 50;
            dgvChiTietDiem.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Cấu hình borders
            dgvChiTietDiem.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvChiTietDiem.GridColor = Color.FromArgb(229, 231, 235);
            dgvChiTietDiem.BorderStyle = BorderStyle.None;

            // Cấu hình columns width
            dgvChiTietDiem.Columns["colMonHoc"].Width = 500;
            dgvChiTietDiem.Columns["colDiemTB"].Width = 270;

            // Căn giữa cột điểm
            dgvChiTietDiem.Columns["colDiemTB"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Loại bỏ selection
            dgvChiTietDiem.EnableHeadersVisualStyles = false;

            // Ngăn đổi màu tiêu đề khi chọn
            dgvChiTietDiem.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            dgvChiTietDiem.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(75, 85, 99);

            // Không cho chỉnh sửa
            dgvChiTietDiem.ReadOnly = true;
        }

        private void LoadChiTietDiem()
        {
            try
            {
                ChiTietDiemDTO dto = nhapDiemBUS.GetChiTietDiem(maHocSinh, maHocKy);

                if (dto == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin học sinh!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                currentDiemDTO = dto;

                // Hiển thị thông tin học sinh
                lblMaHS.Text = dto.MaHocSinh;
                lblTenHocSinh.Text = dto.HoTen;

                // Hiển thị điểm trung bình chung
                if (dto.DiemTB.HasValue)
                {
                    lblDTB.Text = dto.DiemTB.Value.ToString("0.0");
                    ApplyColorToDiemTB(lblDTB, dto.DiemTB.Value);
                }
                else
                {
                    lblDTB.Text = "Chưa đủ điểm";
                    lblDTB.ForeColor = Color.Gray;
                }

                // Xóa dữ liệu cũ trong DataGridView
                dgvChiTietDiem.Rows.Clear();

                // ✅ Lấy danh sách môn học từ MonHoc_NamHoc_Khoi để đảm bảo hiển thị đúng các môn cần có điểm
                List<MonHocDTO> danhSachMonHocHopLe = new List<MonHocDTO>();
                
                // Lấy lớp của học sinh trong học kỳ này
                var phanLopDAO = new DAO.PhanLopDAO();
                int maLop = phanLopDAO.LayLopCuaHocSinh(int.Parse(maHocSinh), maHocKy);
                
                if (maLop > 0)
                {
                    // Lấy thông tin lớp để biết khối
                    var lopDAO = new LopDAO();
                    var lop = lopDAO.LayLopTheoId(maLop);
                    
                    // Lấy danh sách môn học hợp lệ cho lớp và học kỳ từ MonHoc_NamHoc_Khoi
                    danhSachMonHocHopLe = monHocFilterService.GetSubjectsForSemesterAndClass(maHocKy, maLop);
                    
                    // Debug: Log thông tin để kiểm tra
                    System.Diagnostics.Debug.WriteLine($"[ChiTietDiem] MaHocSinh: {maHocSinh}, MaHocKy: {maHocKy}, MaLop: {maLop}, MaKhoi: {lop?.maKhoi ?? -1}, SoMonHoc: {danhSachMonHocHopLe?.Count ?? 0}");
                }
                else
                {
                    // Nếu không có phân lớp, lấy môn học theo năm học của học kỳ
                    var hocKyBUS = new HocKyBUS();
                    var hocKy = hocKyBUS.LayHocKyTheoMa(maHocKy);
                    if (hocKy != null && !string.IsNullOrEmpty(hocKy.MaNamHoc))
                    {
                        var monHocNamHocKhoiBUS = new MonHoc_NamHoc_KhoiBUS();
                        danhSachMonHocHopLe = monHocNamHocKhoiBUS.LayDanhSachMonHocTheoNamHoc(hocKy.MaNamHoc);
                        
                        // Debug: Log thông tin để kiểm tra
                        System.Diagnostics.Debug.WriteLine($"[ChiTietDiem] MaHocSinh: {maHocSinh}, MaHocKy: {maHocKy}, MaNamHoc: {hocKy.MaNamHoc}, SoMonHoc: {danhSachMonHocHopLe?.Count ?? 0}");
                    }
                }
                
                // Debug: Log danh sách môn học
                if (danhSachMonHocHopLe != null && danhSachMonHocHopLe.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"[ChiTietDiem] Danh sách môn học ({danhSachMonHocHopLe.Count} môn): {string.Join(", ", danhSachMonHocHopLe.Select(m => $"{m.maMon}-{m.tenMon}"))}");
                    
                    // Kiểm tra xem có môn ABC không
                    var monABC = danhSachMonHocHopLe.FirstOrDefault(m => m.maMon == 16 || m.tenMon == "ABC");
                    if (monABC != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"[ChiTietDiem] ✅ Tìm thấy môn ABC: MaMon={monABC.maMon}, TenMon={monABC.tenMon}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"[ChiTietDiem] ❌ KHÔNG tìm thấy môn ABC trong danh sách!");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[ChiTietDiem] ⚠️ Danh sách môn học rỗng hoặc null!");
                }

                // Nếu vẫn không có môn học, sử dụng dữ liệu từ DiemCacMon (fallback)
                if (danhSachMonHocHopLe == null || danhSachMonHocHopLe.Count == 0)
                {
                    // Fallback: Sử dụng dữ liệu từ DiemCacMon
                    var danhSachMonHoc = dto.DiemCacMon.OrderBy(x => x.Key).ToList();
                    foreach (var monHoc in danhSachMonHoc)
                    {
                        string diemTBText = monHoc.Value.DiemTrungBinh.HasValue 
                            ? monHoc.Value.DiemTrungBinh.Value.ToString("0.0") 
                            : "Chưa có";
                        
                        dgvChiTietDiem.Rows.Add(monHoc.Value.TenMonHoc, diemTBText);
                    }
                }
                else
                {
                    // ✅ Hiển thị TẤT CẢ môn học từ MonHoc_NamHoc_Khoi (kể cả chưa có điểm)
                    // Sắp xếp theo MaMonHoc để hiển thị tuần tự
                    var danhSachMonHocSorted = danhSachMonHocHopLe.OrderBy(x => x.maMon).ToList();
                    
                    foreach (var monHoc in danhSachMonHocSorted)
                    {
                        // Tìm điểm của môn học này trong DiemCacMon
                        string diemTBText = "Chưa có";
                        if (dto.DiemCacMon != null && dto.DiemCacMon.ContainsKey(monHoc.maMon))
                        {
                            var diemMon = dto.DiemCacMon[monHoc.maMon];
                            if (diemMon.DiemTrungBinh.HasValue)
                            {
                                diemTBText = diemMon.DiemTrungBinh.Value.ToString("0.0");
                            }
                        }
                        
                        dgvChiTietDiem.Rows.Add(monHoc.tenMon, diemTBText);
                    }
                }

                // Áp dụng màu sắc cho cột điểm trung bình
                ApplyColorToDiemTBColumn();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load chi tiết điểm: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Áp dụng màu sắc cho cột điểm trung bình trong DataGridView
        /// </summary>
        private void ApplyColorToDiemTBColumn()
        {
            foreach (DataGridViewRow row in dgvChiTietDiem.Rows)
            {
                if (row.Cells["colDiemTB"].Value != null && !string.IsNullOrEmpty(row.Cells["colDiemTB"].Value.ToString()))
                {
                    string diemText = row.Cells["colDiemTB"].Value.ToString();
                    if (diemText != "Chưa có" && float.TryParse(diemText, out float score))
                    {
                        if (score >= 8.0)
                        {
                            row.Cells["colDiemTB"].Style.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                            row.Cells["colDiemTB"].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        }
                        else if (score >= 6.5)
                        {
                            row.Cells["colDiemTB"].Style.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương
                            row.Cells["colDiemTB"].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        }
                        else if (score >= 5.0)
                        {
                            row.Cells["colDiemTB"].Style.ForeColor = Color.FromArgb(234, 179, 8); // Vàng
                            row.Cells["colDiemTB"].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        }
                        else
                        {
                            row.Cells["colDiemTB"].Style.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                            row.Cells["colDiemTB"].Style.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
                        }
                    }
                    else if (diemText == "Chưa có")
                    {
                        row.Cells["colDiemTB"].Style.ForeColor = Color.Gray;
                    }
                }
            }
        }

        /// <summary>
        /// Áp dụng màu sắc theo điểm số
        /// </summary>
        private void ApplyColorToDiemTB(Label label, float diem)
        {
            if (diem >= 8.0)
            {
                label.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
            }
            else if (diem >= 6.5)
            {
                label.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương
            }
            else if (diem >= 5.0)
            {
                label.ForeColor = Color.FromArgb(234, 179, 8); // Vàng
            }
            else
            {
                label.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void lblTenHocSinh_Click(object sender, EventArgs e)
        {
        }

        private void lblDTB_Click(object sender, EventArgs e)
        {
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo hộp thoại lưu file
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Lưu báo cáo kết quả học tập",
                    FileName = $"BaoCaoHocTap_{currentDiemDTO.MaHocSinh}_{DateTime.Now:yyyyMMdd}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    XuatPDF(saveDialog.FileName);
                    MessageBox.Show("Xuất PDF thành công!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở file PDF sau khi tạo
                    System.Diagnostics.Process.Start(saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất PDF: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void XuatPDF(string filePath)
        {
            Document document = new Document(PageSize.A4, 40, 40, 60, 60);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            document.Open();

            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            // Header
            PdfPTable headerTable = new PdfPTable(1);
            headerTable.WidthPercentage = 100;
            PdfPCell headerCell = new PdfPCell(new Phrase("Báo cáo kết quả học tập", titleFont));
            headerCell.BackgroundColor = new BaseColor(33, 150, 243);
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Padding = 15;
            headerCell.Border = iTextRectangle.NO_BORDER;
            headerTable.AddCell(headerCell);
            document.Add(headerTable);

            document.Add(new Paragraph("\n"));

            // Thông tin học sinh
            PdfPTable infoTable = new PdfPTable(2);
            infoTable.WidthPercentage = 100;
            infoTable.SetWidths(new float[] { 1f, 2f });
            infoTable.SpacingBefore = 10;
            infoTable.SpacingAfter = 20;

            AddInfoRow(infoTable, "Mã học sinh:", currentDiemDTO.MaHocSinh, boldFont, normalFont);
            AddInfoRow(infoTable, "Họ và tên học sinh:", currentDiemDTO.HoTen, boldFont, normalFont);

            string diemTBText = currentDiemDTO.DiemTB.HasValue ?
                currentDiemDTO.DiemTB.Value.ToString("0.0") : "Chưa đủ điểm";

            BaseColor diemTBColor = GetColorForScore(currentDiemDTO.DiemTB ?? 0);
            iTextSharp.text.Font diemTBFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, diemTBColor);

            AddInfoRow(infoTable, "Điểm trung bình chung:", diemTBText, boldFont, diemTBFont);

            document.Add(infoTable);

            // Tiêu đề "Điểm số"
            Paragraph diemSoTitle = new Paragraph("Điểm số:", headerFont);
            diemSoTitle.SpacingBefore = 10;
            diemSoTitle.SpacingAfter = 10;
            document.Add(diemSoTitle);

            // Bảng điểm - Tự động dựa theo môn học trong database
            PdfPTable scoreTable = new PdfPTable(3);
            scoreTable.WidthPercentage = 100;
            scoreTable.SetWidths(new float[] { 1f, 1f, 1f });

            // Sắp xếp môn học theo MaMonHoc
            var sortedMonHoc = currentDiemDTO.DiemCacMon.OrderBy(x => x.Key);

            foreach (var monHoc in sortedMonHoc)
            {
                DiemMonHocDTO diemMon = monHoc.Value;
                AddScoreRow(scoreTable, diemMon.TenMonHoc + ":", diemMon.DiemTrungBinh, normalFont, bf);
            }

            document.Add(scoreTable);

            document.Add(new Paragraph("\n\n"));
            Paragraph footer = new Paragraph($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}",
                new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.ITALIC, BaseColor.GRAY));
            footer.Alignment = Element.ALIGN_RIGHT;
            document.Add(footer);

            document.Close();
            writer.Close();
        }

        private void AddInfoRow(PdfPTable table, string label, string value,
            iTextSharp.text.Font labelFont, iTextSharp.text.Font valueFont)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont));
            labelCell.Border = iTextRectangle.NO_BORDER; // DÙNG ALIAS
            labelCell.PaddingBottom = 8;
            table.AddCell(labelCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont));
            valueCell.Border = iTextRectangle.NO_BORDER; // DÙNG ALIAS
            valueCell.PaddingBottom = 8;
            table.AddCell(valueCell);
        }

        private void AddScoreRow(PdfPTable table, string monHoc, float? diem,
            iTextSharp.text.Font normalFont, BaseFont bf)
        {
            string diemText = diem.HasValue ? diem.Value.ToString("0.0") : "Chưa có";
            BaseColor diemColor = diem.HasValue ? GetColorForScore(diem.Value) : BaseColor.GRAY;

            iTextSharp.text.Font diemFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, diemColor);

            PdfPCell monHocCell = new PdfPCell(new Phrase(monHoc, normalFont));
            monHocCell.Border = iTextRectangle.NO_BORDER; // DÙNG ALIAS
            monHocCell.PaddingBottom = 8;
            monHocCell.PaddingLeft = 5;
            table.AddCell(monHocCell);

            PdfPCell diemCell = new PdfPCell(new Phrase(diemText, diemFont));
            diemCell.Border = iTextRectangle.NO_BORDER; // DÙNG ALIAS
            diemCell.PaddingBottom = 8;
            table.AddCell(diemCell);

            PdfPCell emptyCell = new PdfPCell(new Phrase(""));
            emptyCell.Border = iTextRectangle.NO_BORDER; // DÙNG ALIAS
            table.AddCell(emptyCell);
        }

        private BaseColor GetColorForScore(float diem)
        {
            if (diem >= 8.0)
                return new BaseColor(22, 163, 74);
            else if (diem >= 6.5)
                return new BaseColor(30, 136, 229);
            else if (diem >= 5.0)
                return new BaseColor(234, 179, 8);
            else
                return new BaseColor(220, 38, 38);
        }

    }

      
    }

