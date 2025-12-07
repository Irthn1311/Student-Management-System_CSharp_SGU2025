using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextRectangle = iTextSharp.text.Rectangle;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
   

    public partial class BaoCaoLop : Form
    {
        private PhanLopDAO phanLopDAO;
        private HocSinhDAO hocSinhDAO;
        private XepLoaiBUS xepLoaiBUS;
        private int maLop;
        private int maHocKy;
        private string tenLop;
        private List<HocSinhDTO> danhSachHocSinh;
        private Dictionary<string, int> thongKeXepLoai;
        public event EventHandler OnClose;
        public BaoCaoLop()
        {
            InitializeComponent();

           
            this.FormBorderStyle = FormBorderStyle.Sizable; // Cho phép resize
            this.StartPosition = FormStartPosition.CenterParent; // Căn giữa parent form
            this.ShowInTaskbar = false;
            this.MinimizeBox = false;
            this.MaximizeBox = true;
            this.KeyPreview = true;
            tbHocSinh.CellFormatting += tbHocSinh_CellFormatting;

            phanLopDAO = new PhanLopDAO();
            hocSinhDAO = new HocSinhDAO();
            xepLoaiBUS = new XepLoaiBUS();
            danhSachHocSinh = new List<HocSinhDTO>();
            thongKeXepLoai = new Dictionary<string, int>();

            // Xử lý phím ESC
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };
        }

        /// <summary>
        /// Thiết lập thông tin lớp và tải dữ liệu học sinh
        /// </summary>
        public void SetThongTinLop(int maLop, string tenLop, int maHocKy)
        {
            this.maLop = maLop;
            this.tenLop = tenLop;
            this.maHocKy = maHocKy;

            // Cập nhật tiêu đề
            lblTitle.Text = $"Báo cáo lớp {tenLop}";

            // Load danh sách học sinh
            LoadDanhSachHocSinh();

            // Load thống kê và vẽ charts
            LoadThongKeVaVeCharts();
        }

        /// <summary>
        /// Load danh sách học sinh theo lớp và học kỳ
        /// </summary>
        private void LoadDanhSachHocSinh()
        {
            try
            {
                // Lấy danh sách học sinh trong lớp
                danhSachHocSinh = phanLopDAO.LayDanhSachHocSinhTrongLop(maLop, maHocKy);

                // Sắp xếp theo mã học sinh
                danhSachHocSinh = danhSachHocSinh.OrderBy(hs => hs.MaHS).ToList();

                // Hiển thị lên DataGridView
                HienThiDanhSach(danhSachHocSinh);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách học sinh: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị danh sách học sinh lên DataGridView
        /// </summary>
        private void HienThiDanhSach(List<HocSinhDTO> dsHocSinh)
        {
            tbHocSinh.Rows.Clear();

            foreach (var hs in dsHocSinh)
            {
                tbHocSinh.Rows.Add(
                    hs.MaHS,
                    hs.HoTen,
                    hs.NgaySinh.ToString("dd/MM/yyyy"),
                    hs.GioiTinh,
                    hs.SdtHS ?? "",
                    hs.Email ?? "",
                    hs.TrangThai
                );
            }
        }

        /// <summary>
        /// Format các cell trong DataGridView (màu sắc cho giới tính và trạng thái)
        /// </summary>
        private void tbHocSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột Giới tính (index 3)
            if (e.ColumnIndex == 3 && e.Value != null)
            {
                string gioiTinh = e.Value.ToString();
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                e.CellStyle.Padding = new Padding(5, 3, 5, 3);

                if (gioiTinh == "Nam")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216);
                    e.CellStyle.BackColor = Color.FromArgb(219, 234, 254);
                }
                else if (gioiTinh == "Nữ")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(190, 24, 93);
                    e.CellStyle.BackColor = Color.FromArgb(253, 232, 255);
                }
            }

            // Format cột Trạng thái (index 6)
            if (e.ColumnIndex == 6 && e.Value != null)
            {
                string trangThai = e.Value.ToString();
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                e.CellStyle.Padding = new Padding(5, 3, 5, 3);

                switch (trangThai)
                {
                    case "Đang học":
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                        e.CellStyle.BackColor = Color.FromArgb(220, 252, 231);
                        break;
                    case "Nghỉ học":
                        e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                        e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                        break;
                    case "Bảo lưu":
                        e.CellStyle.ForeColor = Color.FromArgb(194, 65, 12);
                        e.CellStyle.BackColor = Color.FromArgb(255, 237, 213);
                        break;
                    case "Thôi học":
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128);
                        e.CellStyle.BackColor = Color.FromArgb(243, 244, 246);
                        break;
                }
            }

            // Format cột Mã HS và Ngày sinh căn giữa
            if (e.ColumnIndex == 0 || e.ColumnIndex == 2)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void tbHocSinh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BaoCaoLop_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Xử lý phím ESC để đóng form
        /// </summary>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void BaoCaoLop_Load_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Load thống kê xếp loại và vẽ charts
        /// </summary>
        private void LoadThongKeVaVeCharts()
        {
            try
            {
                // Lấy thống kê xếp loại theo lớp
                thongKeXepLoai = xepLoaiBUS.ThongKeXepLoaiTongKet(maHocKy, maLop);

                // Vẽ bar chart
                VeBarChart();

                // Vẽ pie chart
                VePieChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Vẽ bar chart cho thống kê xếp loại
        /// </summary>
        private void VeBarChart()
        {
            chartBar.Series.Clear();
            chartBar.ChartAreas.Clear();
            chartBar.Legends.Clear();

            // Tạo ChartArea
            ChartArea chartArea = new ChartArea("ChartArea1");
            chartArea.AxisX.Title = "Xếp loại";
            chartArea.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.AxisY.Title = "Số lượng học sinh";
            chartArea.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            chartArea.BackColor = Color.White;
            chartBar.ChartAreas.Add(chartArea);

            // Tạo Series
            Series series = new Series("Xếp loại học lực");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(30, 136, 229);
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "{0}";
            series.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);

            // Thêm dữ liệu
            string[] xepLoai = { "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" };
            Color[] colors = {
                Color.FromArgb(16, 185, 129),   // Giỏi - Xanh lá
                Color.FromArgb(59, 130, 246),   // Khá - Xanh dương
                Color.FromArgb(245, 158, 11),   // Trung bình - Vàng
                Color.FromArgb(239, 68, 68),    // Yếu - Đỏ
                Color.FromArgb(107, 114, 128)   // Kém - Xám
            };

            for (int i = 0; i < xepLoai.Length; i++)
            {
                int value = thongKeXepLoai.ContainsKey(xepLoai[i]) ? thongKeXepLoai[xepLoai[i]] : 0;
                DataPoint point = series.Points.Add(value);
                point.AxisLabel = xepLoai[i];
                point.Color = colors[i];
                point.Label = value.ToString();
            }

            chartBar.Series.Add(series);
            chartBar.Titles.Clear();
            chartBar.Titles.Add("Thống kê xếp loại học lực");
            chartBar.Titles[0].Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            chartBar.Titles[0].ForeColor = Color.FromArgb(30, 136, 229);
        }

        /// <summary>
        /// Vẽ pie chart cho thống kê xếp loại
        /// </summary>
        private void VePieChart()
        {
            chartPie.Series.Clear();
            chartPie.ChartAreas.Clear();
            chartPie.Legends.Clear();

            // Tạo ChartArea
            ChartArea chartArea = new ChartArea("ChartArea1");
            chartArea.BackColor = Color.White;
            chartPie.ChartAreas.Add(chartArea);

            // Tạo Legend
            Legend legend = new Legend("Legend1");
            legend.Font = new System.Drawing.Font("Segoe UI", 9);
            legend.Docking = Docking.Bottom;
            chartPie.Legends.Add(legend);

            // Tạo Series
            Series series = new Series("Xếp loại học lực");
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "#PERCENT{P1}";
            series.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            series.LabelForeColor = Color.White;
            series["PieLabelStyle"] = "Outside";

            // Thêm dữ liệu
            string[] xepLoai = { "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" };
            Color[] colors = {
                Color.FromArgb(16, 185, 129),   // Giỏi - Xanh lá
                Color.FromArgb(59, 130, 246),   // Khá - Xanh dương
                Color.FromArgb(245, 158, 11),   // Trung bình - Vàng
                Color.FromArgb(239, 68, 68),    // Yếu - Đỏ
                Color.FromArgb(107, 114, 128)   // Kém - Xám
            };

            int tongSo = thongKeXepLoai.Values.Sum();
            for (int i = 0; i < xepLoai.Length; i++)
            {
                int value = thongKeXepLoai.ContainsKey(xepLoai[i]) ? thongKeXepLoai[xepLoai[i]] : 0;
                if (value > 0)
                {
                    DataPoint point = series.Points.Add(value);
                    point.Label = $"{xepLoai[i]}: {value}";
                    point.LegendText = $"{xepLoai[i]}: {value} ({(tongSo > 0 ? (value * 100.0 / tongSo).ToString("0.0") : "0")}%)";
                    point.Color = colors[i];
                }
            }

            chartPie.Series.Add(series);
            chartPie.Titles.Clear();
            chartPie.Titles.Add("Thống kê xếp loại học lực");
            chartPie.Titles[0].Font = new   System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            chartPie.Titles[0].ForeColor = Color.FromArgb(30, 136, 229);
        }

        /// <summary>
        /// Xử lý sự kiện click nút xuất PDF
        /// </summary>
        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Xuất báo cáo PDF",
                    FileName = $"BaoCao_Lop_{tenLop}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    XuatPDFBaoCaoLop(saveDialog.FileName);
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
                MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất báo cáo lớp ra PDF với charts
        /// </summary>
        private void XuatPDFBaoCaoLop(string filePath)
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
            PdfPCell headerCell = new PdfPCell(new Phrase($"BÁO CÁO LỚP {tenLop.ToUpper()}", titleFont));
            headerCell.BackgroundColor = new BaseColor(30, 136, 229);
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Padding = 15;
            headerCell.Border = iTextRectangle.NO_BORDER;
            headerTable.AddCell(headerCell);
            document.Add(headerTable);

            document.Add(new Paragraph("\n"));

            // === THÔNG TIN LỚP ===
            Paragraph lopInfo = new Paragraph($"Lớp: {tenLop}", headerFont);
            lopInfo.Alignment = Element.ALIGN_CENTER;
            document.Add(lopInfo);

            Paragraph ngayXuat = new Paragraph($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}", smallFont);
            ngayXuat.Alignment = Element.ALIGN_CENTER;
            ngayXuat.SpacingAfter = 20;
            document.Add(ngayXuat);

            // === THỐNG KÊ TỔNG QUAN ===
            int tongSoHocSinh = danhSachHocSinh.Count;
            int tongSoNam = danhSachHocSinh.Count(hs => hs.GioiTinh == "Nam");
            int tongSoNu = danhSachHocSinh.Count(hs => hs.GioiTinh == "Nữ");
            int tongSoCoXepLoai = thongKeXepLoai.Values.Sum();

            PdfPTable statsTable = new PdfPTable(2);
            statsTable.WidthPercentage = 100;
            statsTable.SetWidths(new float[] { 1f, 1f });
            statsTable.SpacingBefore = 10;
            statsTable.SpacingAfter = 20;

            AddStatCard(statsTable, "Tổng số học sinh", tongSoHocSinh.ToString(), new BaseColor(30, 136, 229), bf);
            AddStatCard(statsTable, "Học sinh Nam", $"{tongSoNam} ({(tongSoHocSinh > 0 ? (tongSoNam * 100.0 / tongSoHocSinh).ToString("0.0") : "0")}%)", new BaseColor(99, 102, 241), bf);
            AddStatCard(statsTable, "Học sinh Nữ", $"{tongSoNu} ({(tongSoHocSinh > 0 ? (tongSoNu * 100.0 / tongSoHocSinh).ToString("0.0") : "0")}%)", new BaseColor(236, 72, 153), bf);
            AddStatCard(statsTable, "Có xếp loại", $"{tongSoCoXepLoai}", new BaseColor(16, 185, 129), bf);

            document.Add(statsTable);

            // === THỐNG KÊ XẾP LOẠI ===
            if (tongSoCoXepLoai > 0)
            {
                Paragraph xepLoaiTitle = new Paragraph("THỐNG KÊ XẾP LOẠI HỌC LỰC", headerFont);
                xepLoaiTitle.SpacingBefore = 10;
                xepLoaiTitle.SpacingAfter = 10;
                document.Add(xepLoaiTitle);

                PdfPTable xepLoaiTable = new PdfPTable(3);
                xepLoaiTable.WidthPercentage = 100;
                xepLoaiTable.SetWidths(new float[] { 2f, 1f, 1f });

                // Header
                AddTableHeader(xepLoaiTable, "Xếp loại", bf);
                AddTableHeader(xepLoaiTable, "Số lượng", bf);
                AddTableHeader(xepLoaiTable, "Tỷ lệ", bf);

                // Data
                string[] xepLoai = { "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" };
                foreach (string xl in xepLoai)
                {
                    int soLuong = thongKeXepLoai.ContainsKey(xl) ? thongKeXepLoai[xl] : 0;
                    double tyLe = tongSoCoXepLoai > 0 ? (soLuong * 100.0 / tongSoCoXepLoai) : 0;

                    AddTableCell(xepLoaiTable, xl, boldFont, Element.ALIGN_LEFT);
                    AddTableCell(xepLoaiTable, soLuong.ToString(), normalFont, Element.ALIGN_CENTER);
                    AddTableCell(xepLoaiTable, $"{tyLe.ToString("0.0")}%", normalFont, Element.ALIGN_CENTER);
                }

                document.Add(xepLoaiTable);

                // === CHART IMAGES ===
                document.Add(new Paragraph("\n"));

                // Lưu charts thành hình ảnh tạm thời
                string tempBarChart = Path.Combine(Path.GetTempPath(), $"barchart_{Guid.NewGuid()}.png");
                string tempPieChart = Path.Combine(Path.GetTempPath(), $"piechart_{Guid.NewGuid()}.png");

                try
                {
                    // Lưu bar chart
                    chartBar.SaveImage(tempBarChart, ChartImageFormat.Png);
                    iTextSharp.text.Image barImage = iTextSharp.text.Image.GetInstance(tempBarChart);
                    barImage.ScaleToFit(450, 300);
                    barImage.Alignment = Element.ALIGN_CENTER;
                    document.Add(barImage);

                    document.Add(new Paragraph("\n"));

                    // Lưu pie chart
                    chartPie.SaveImage(tempPieChart, ChartImageFormat.Png);
                    iTextSharp.text.Image pieImage = iTextSharp.text.Image.GetInstance(tempPieChart);
                    pieImage.ScaleToFit(450, 300);
                    pieImage.Alignment = Element.ALIGN_CENTER;
                    document.Add(pieImage);

                    // Xóa file tạm
                    if (File.Exists(tempBarChart)) File.Delete(tempBarChart);
                    if (File.Exists(tempPieChart)) File.Delete(tempPieChart);
                }
                catch
                {
                    // Bỏ qua nếu không thể thêm charts
                }
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

            PdfPCell headerCell = new PdfPCell(new Phrase(label,
                new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE)));
            headerCell.BackgroundColor = color;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Padding = 8;
            headerCell.Border = iTextRectangle.NO_BORDER;
            cardTable.AddCell(headerCell);

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
    }
}
