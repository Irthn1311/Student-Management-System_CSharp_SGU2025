using Student_Management_System_CSharp_SGU2025.BUS;
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

        public ChiTietDiem()
        {
            InitializeComponent();
            nhapDiemBUS = new NhapDiemBUS();
        }

        // Constructor với tham số
        public ChiTietDiem(string maHocSinh, int maHocKy) : this()
        {
            this.maHocSinh = maHocSinh;
            this.maHocKy = maHocKy;
        }

        private void ChiTietDiem_Load(object sender, EventArgs e)
        {
            LoadChiTietDiem();

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

                // Mảng các label điểm theo thứ tự
                Label[] diemLabels = new Label[]
                {
            diem1, diem2, diem3, diem4, diem5, diem6, diem7,
            diem8, diem9, diem10, diem11, diem12, diem13
                };

                // Hiển thị điểm các môn theo thứ tự MaMonHoc
                for (int i = 1; i <= 13; i++)
                {
                    if (dto.DiemCacMon.ContainsKey(i))
                    {
                        DiemMonHocDTO diemMon = dto.DiemCacMon[i];
                        HienThiDiem(diemLabels[i - 1], diemMon.DiemTrungBinh);
                    }
                    else
                    {
                        // Nếu môn học không tồn tại, ẩn label hoặc hiển thị "N/A"
                        if (i - 1 < diemLabels.Length)
                        {
                            diemLabels[i - 1].Text = "N/A";
                            diemLabels[i - 1].ForeColor = Color.LightGray;
                        }
                    }
                }

                // Thêm vào LoadChiTietDiem
                Label[] tenMonLabels = new Label[]
                {
    tenMon1, tenMon2, tenMon3, tenMon4, tenMon5, tenMon6,
    tenMon7, tenMon8, tenMon9, tenMon10, tenMon11, tenMon12, tenMon13
                };

                for (int i = 1; i <= 13; i++)
                {
                    if (dto.DiemCacMon.ContainsKey(i))
                    {
                        DiemMonHocDTO diemMon = dto.DiemCacMon[i];
                        tenMonLabels[i - 1].Text = diemMon.TenMonHoc + ":";
                        HienThiDiem(diemLabels[i - 1], diemMon.DiemTrungBinh);
                    }
                    else
                    {
                        tenMonLabels[i - 1].Text = "Môn " + i + ":";
                        diemLabels[i - 1].Text = "N/A";
                        diemLabels[i - 1].ForeColor = Color.LightGray;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load chi tiết điểm: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Hiển thị điểm và áp dụng màu sắc
        /// </summary>
        private void HienThiDiem(Label label, float? diem)
        {
            if (diem.HasValue)
            {
                label.Text = diem.Value.ToString("0.0");
                ApplyColorToDiemTB(label, diem.Value);
            }
            else
            {
                label.Text = "Chưa có";
                label.ForeColor = Color.Gray;
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

        private void dToan_Click(object sender, EventArgs e)
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

        private void diem3_Click(object sender, EventArgs e)
        {

        }

        private void diem6_Click(object sender, EventArgs e)
        {

        }
    }

      
    }

