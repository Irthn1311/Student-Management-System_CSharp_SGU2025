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
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ucXepLoai : UserControl
    {
        private HocKyDAO hocKyDAO;
        private NamHocDAO namHocDAO;
        private XepLoaiDAO xepLoaiDAO;
        private XepLoaiBUS xepLoaiBUS;
        public ucXepLoai()
        {
            InitializeComponent();
            hocKyDAO = new HocKyDAO();
            namHocDAO = new NamHocDAO();
            xepLoaiDAO = new XepLoaiDAO();
            xepLoaiBUS = new XepLoaiBUS();
            LoadHocKy();
        }

        private void LoadSampleData()
        {
            // Thêm dữ liệu mẫu vào DataGridView
            tableXepLoai.Rows.Add("","Nguyễn Văn An", "10A1", "8.5", "Tốt", "Giỏi", "Giỏi");
            tableXepLoai.Rows.Add("","Trần Thị Bình", "10A1", "9.2", "Tốt", "Giỏi", "Giỏi");
            tableXepLoai.Rows.Add("","Lê Hoàng Cường", "10A2", "7.8", "Khá", "Khá", "Khá");
            tableXepLoai.Rows.Add("","Phạm Thị Dung", "10A2", "8.8", "Tốt", "Giỏi", "Giỏi");
            tableXepLoai.Rows.Add("", "Hoàng Văn Em", "11A1", "6.5", "Khá", "Trung bình", "Trung bình");
            tableXepLoai.Rows.Add("", "Vũ Thị Hoa", "11A1", "7.5", "Khá", "Khá", "Khá");
            tableXepLoai.Rows.Add("", "Vũ Thị A", "11A2", "9.5", "Tốt", "Giỏi", "Giỏi");
        }

        /// <summary>
        /// Load danh sách học kỳ vào combobox
        /// </summary>
        private void LoadHocKy()
        {
            try
            {
                cbHocKyNamHoc.Items.Clear();
                cbHocKyNamHoc.DisplayMember = "Text";
                cbHocKyNamHoc.ValueMember = "Value";

                List<HocKyDTO> dsHocKy = hocKyDAO.GetAllHocKy();

                foreach (var hk in dsHocKy)
                {
                    string displayText = $"{hk.TenHocKy} - {hk.MaNamHoc}";
                    cbHocKyNamHoc.Items.Add(new { Text = displayText, Value = hk.MaHocKy });
                }

                if (cbHocKyNamHoc.Items.Count > 0)
                {
                    cbHocKyNamHoc.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách học kỳ: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Load danh sách lớp theo học kỳ được chọn
        /// </summary>
        private void LoadLopTheoHocKy(int maHocKy)
        {
            try
            {
                cbLop.Items.Clear();
                cbLop.DisplayMember = "Text";
                cbLop.ValueMember = "Value";

                // Thêm tùy chọn "Tất cả lớp"
                cbLop.Items.Add(new { Text = "Tất cả lớp", Value = 0 });

                List<LopDTO> dsLop = xepLoaiDAO.GetDanhSachLopTheoHocKy(maHocKy);

                foreach (var lop in dsLop)
                {
                    cbLop.Items.Add(new { Text = lop.TenLop, Value = lop.MaLop });
                }

                if (cbLop.Items.Count > 0)
                {
                    cbLop.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void LoadDuLieuXepLoai()
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    tableXepLoai.Rows.Clear();
                    LoadThongKe();
                    return;
                }

                dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                int? maLop = null;
                if (cbLop.SelectedItem != null)
                {
                    dynamic selectedLop = cbLop.SelectedItem;
                    int lopValue = selectedLop.Value;
                    if (lopValue > 0)
                    {
                        maLop = lopValue;
                    }
                }

                // Lấy danh sách đầy đủ (có cả hạnh kiểm và xếp loại)
                List<XepLoaiDTO> dsXepLoai = xepLoaiBUS.LayDanhSachXepLoaiDayDu(maHocKy, maLop);
                dsXepLoai = dsXepLoai.OrderBy(x => x.MaHocSinh).ToList();


                tableXepLoai.Rows.Clear();
                foreach (var item in dsXepLoai)
                {
                    tableXepLoai.Rows.Add(
                        item.MaHocSinh,
                        item.HoTen,
                        item.TenLop,
                        item.DiemTB.HasValue ? item.DiemTB.Value.ToString("0.0") : "",
                        item.HanhKiem ?? "", // Cột Hành kiểm
                        item.HocLuc ?? "",
                        item.XepLoaiTongKet ?? "" // Cột Xếp loại
                    );
                }
                LoadThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu xếp loại: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            // Tô màu cột Học lực (index 5)
            if (e.ColumnIndex == 5 && e.Value != null)
            {
                ApplyColorFormatting(e, e.Value.ToString());
            }

            // Tô màu cột Hạnh kiểm (index 4)
            if (e.ColumnIndex == 4 && e.Value != null)
            {
                ApplyColorFormatting(e, e.Value.ToString());
            }

            // Tô màu cột Xếp loại (index 6)
            if (e.ColumnIndex == 6 && e.Value != null)
            {
                ApplyColorFormatting(e, e.Value.ToString());
            }

        }

        private void ApplyColorFormatting(DataGridViewCellFormattingEventArgs e, string value)
        {
            switch (value)
            {
                case "Giỏi":
                case "Tốt":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
                    break;
                case "Khá":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
                    break;
                case "Trung bình":
                case "Trung Bình":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(194, 65, 12);
                    break;
                case "Yếu":
                case "Kém":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
                    break;
            }
        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKyNamHoc.SelectedItem != null)
            {
                dynamic selected = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selected.Value;
                LoadLopTheoHocKy(maHocKy);
                LoadDuLieuXepLoai();
                LoadThongKeToanTruong();
                LoadThongKeTheoKhoi();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có dữ liệu không
                if (tableXepLoai.Rows.Count == 0 || cbHocKyNamHoc.SelectedItem == null)
                {
                    MessageBox.Show("Không có dữ liệu để xuất báo cáo!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiển thị dialog cho người dùng chọn định dạng
                DialogResult result = MessageBox.Show(
                    "Chọn định dạng xuất báo cáo:\n\n" +
                    "YES - Xuất file PDF\n" +
                    "NO - Xuất file Excel\n" +
                    "CANCEL - Hủy",
                    "Chọn định dạng xuất báo cáo",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    XuatBaoCaoPDF();
                }
                else if (result == DialogResult.No)
                {
                    XuatBaoCaoExcel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Load thống kê xếp loại theo học kỳ và lớp
        /// </summary>
        private void LoadThongKe()
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    // Reset về 0
                    CapNhatCardThongKe(thongKeXepLoai1, "Giỏi", 0, 0, Color.FromArgb(34, 197, 94));
                    CapNhatCardThongKe(thongKeXepLoai2, "Khá", 0, 0, Color.FromArgb(59, 130, 246));
                    CapNhatCardThongKe(thongKeXepLoai3, "Trung bình", 0, 0, Color.FromArgb(249, 115, 22));
                    CapNhatCardThongKe(thongKeXepLoai4, "Yếu", 0, 0, Color.FromArgb(239, 68, 68));
                    return;
                }

                dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                int? maLop = null;
                if (cbLop.SelectedItem != null)
                {
                    dynamic selectedLop = cbLop.SelectedItem;
                    int lopValue = selectedLop.Value;
                    if (lopValue > 0)
                    {
                        maLop = lopValue;
                    }
                }

                // Lấy thống kê xếp loại tổng kết
                Dictionary<string, int> thongKe = xepLoaiBUS.ThongKeXepLoaiTongKet(maHocKy, maLop);

                // Tính tổng số học sinh
                int tongSo = thongKe.Values.Sum();

                // Cập nhật các card thống kê
                CapNhatCardThongKe(thongKeXepLoai1, "Giỏi", thongKe["Giỏi"], tongSo, Color.FromArgb(34, 197, 94));
                CapNhatCardThongKe(thongKeXepLoai2, "Khá", thongKe["Khá"], tongSo, Color.FromArgb(59, 130, 246));
                CapNhatCardThongKe(thongKeXepLoai3, "Trung bình", thongKe["Trung bình"], tongSo, Color.FromArgb(249, 115, 22));
                CapNhatCardThongKe(thongKeXepLoai4, "Yếu", thongKe["Yếu"], tongSo, Color.FromArgb(239, 68, 68));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật thông tin cho card thống kê
        /// </summary>
        private void CapNhatCardThongKe(dynamic card, string xepLoai, int soLuong, int tongSo, Color mauSac)
        {
            card.TieuDe1 = xepLoai;
            card.TieuDe2 = soLuong.ToString();

            if (tongSo > 0)
            {
                double phanTram = (double)soLuong / tongSo * 100;
                card.TieuDe3 = $"{phanTram:0.0}% học sinh";
            }
            else
            {
                card.TieuDe3 = "0% học sinh";
            }

            card.FillColor = mauSac;
        }

        /// <summary>
        /// Load thống kê xếp loại toàn trường theo học kỳ (không phân biệt lớp)
        /// Hiển thị lên ProgressBar và Label
        /// </summary>
        private void LoadThongKeToanTruong()
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    // Reset về 0
                    CapNhatProgressBar(progressBarGioi, lblPercentGioi, 0);
                    CapNhatProgressBar(progressBarKha, lblPercentKha, 0);
                    CapNhatProgressBar(progressBarTB, lblPercentTB, 0);
                    CapNhatProgressBar(progressBarYeu, lblPercentYeu, 0);
                    // Reset ProgressBar Kém
                    if (progressBarKem != null)
                    {
                        CapNhatProgressBar(progressBarKem, lblPercentKem, 0);
                    }
                    else if (lblPercentKem != null)
                    {
                        lblPercentKem.Text = "0%";
                    }
                    return;
                }

                dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                // Lấy thống kê toàn trường (không truyền maLop)
                Dictionary<string, int> thongKeToanTruong = xepLoaiBUS.ThongKeXepLoaiTongKet(maHocKy, null);

                // Tính tổng số học sinh toàn trường
                int tongSoToanTruong = thongKeToanTruong.Values.Sum();

                if (tongSoToanTruong > 0)
                {
                    // Tính phần trăm và cập nhật ProgressBar
                    double phanTramGioi = (double)thongKeToanTruong["Giỏi"] / tongSoToanTruong * 100;
                    double phanTramKha = (double)thongKeToanTruong["Khá"] / tongSoToanTruong * 100;
                    double phanTramTB = (double)thongKeToanTruong["Trung bình"] / tongSoToanTruong * 100;
                    double phanTramYeu = (double)thongKeToanTruong["Yếu"] / tongSoToanTruong * 100;
                    double phanTramKem = (double)thongKeToanTruong["Kém"] / tongSoToanTruong * 100;

                    CapNhatProgressBar(progressBarGioi, lblPercentGioi, phanTramGioi);
                    CapNhatProgressBar(progressBarKha, lblPercentKha, phanTramKha);
                    CapNhatProgressBar(progressBarTB, lblPercentTB, phanTramTB);
                    CapNhatProgressBar(progressBarYeu, lblPercentYeu, phanTramYeu);

                    // Cập nhật ProgressBar và Label Kém
                    if (progressBarKem != null && lblPercentKem != null)
                    {
                        CapNhatProgressBar(progressBarKem, lblPercentKem, phanTramKem);
                    }
                    else if (lblPercentKem != null)
                    {
                        lblPercentKem.Text = $"{phanTramKem:0.0}%";
                    }
                }
                else
                {
                    CapNhatProgressBar(progressBarGioi, lblPercentGioi, 0);
                    CapNhatProgressBar(progressBarKha, lblPercentKha, 0);
                    CapNhatProgressBar(progressBarTB, lblPercentTB, 0);
                    CapNhatProgressBar(progressBarYeu, lblPercentYeu, 0);

                    // Reset ProgressBar và Label Kém
                    if (progressBarKem != null)
                    {
                        CapNhatProgressBar(progressBarKem, lblPercentKem, 0);
                    }
                    else if (lblPercentKem != null)
                    {
                        lblPercentKem.Text = "0%";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê toàn trường: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật giá trị ProgressBar và Label phần trăm
        /// ProgressBar có Maximum = 1000 để hiển thị chính xác số thập phân
        /// </summary>
        private void CapNhatProgressBar(Guna.UI2.WinForms.Guna2ProgressBar progressBar, Label label, double phanTram)
        {
            // Set Maximum = 1000 để có độ chính xác cao
            progressBar.Maximum = 1000;

            // Tính Value = phần trăm * 10 (vì Maximum = 1000 tương đương 100%)
            int value = (int)Math.Round(phanTram * 10);
            progressBar.Value = Math.Min(value, 1000); // Đảm bảo không vượt quá Maximum

            // Hiển thị label với 1 số thập phân
            label.Text = $"{phanTram:0.0}%";
        }

        /// <summary>
        /// Xuất báo cáo ra file PDF
        /// </summary>
        private void XuatBaoCaoPDF()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Lưu báo cáo xếp loại",
                    FileName = $"BaoCaoXepLoai_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Tạo document PDF
                    Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveDialog.FileName, FileMode.Create));
                    document.Open();

                    // Font hỗ trợ tiếng Việt
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

                    // Tiêu đề
                    Paragraph title = new Paragraph("BÁO CÁO XẾP LOẠI HỌC SINH", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 10
                    };
                    document.Add(title);

                    // Thông tin học kỳ và lớp
                    dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                    string thongTinHocKy = $"Học kỳ: {selectedHocKy.Text}";

                    if (cbLop.SelectedItem != null)
                    {
                        dynamic selectedLop = cbLop.SelectedItem;
                        thongTinHocKy += $" - Lớp: {selectedLop.Text}";
                    }

                    Paragraph info = new Paragraph(thongTinHocKy, normalFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 15
                    };
                    document.Add(info);

                    // Lấy thống kê
                    int maHocKy = selectedHocKy.Value;
                    int? maLop = null;
                    if (cbLop.SelectedItem != null)
                    {
                        dynamic selectedLop = cbLop.SelectedItem;
                        int lopValue = selectedLop.Value;
                        if (lopValue > 0) maLop = lopValue;
                    }

                    Dictionary<string, int> thongKe = xepLoaiBUS.ThongKeXepLoaiTongKet(maHocKy, maLop);
                    int tongSo = thongKe.Values.Sum();

                    // Bảng thống kê
                    PdfPTable thongKeTable = new PdfPTable(5) { WidthPercentage = 100, SpacingAfter = 15 };
                    thongKeTable.SetWidths(new float[] { 1, 1, 1, 1, 1 });

                    // Header thống kê
                    string[] thongKeHeaders = { "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" };
                    foreach (string header in thongKeHeaders)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, headerFont))
                        {
                            BackgroundColor = new BaseColor(220, 220, 220),
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5
                        };
                        thongKeTable.AddCell(cell);
                    }

                    // Dữ liệu thống kê
                    foreach (string xepLoai in thongKeHeaders)
                    {
                        int soLuong = thongKe[xepLoai];
                        double phanTram = tongSo > 0 ? (double)soLuong / tongSo * 100 : 0;
                        string text = $"{soLuong}\n({phanTram:0.0}%)";

                        PdfPCell cell = new PdfPCell(new Phrase(text, normalFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5
                        };
                        thongKeTable.AddCell(cell);
                    }
                    document.Add(thongKeTable);

                    // Bảng danh sách học sinh
                    PdfPTable table = new PdfPTable(7) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 1f, 2.5f, 1.5f, 1.2f, 1.5f, 1.5f, 1.5f });

                    // Header
                    string[] headers = { "Mã HS", "Họ tên", "Lớp", "ĐTB", "Hạnh kiểm", "Học lực", "Xếp loại" };
                    foreach (string header in headers)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(header, headerFont))
                        {
                            BackgroundColor = new BaseColor(200, 200, 200),
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 5
                        };
                        table.AddCell(cell);
                    }

                    // Dữ liệu
                    foreach (DataGridViewRow row in tableXepLoai.Rows)
                    {
                        if (row.IsNewRow) continue;

                        for (int i = 0; i < 7; i++)
                        {
                            string cellValue = row.Cells[i].Value?.ToString() ?? "";
                            PdfPCell cell = new PdfPCell(new Phrase(cellValue, normalFont))
                            {
                                HorizontalAlignment = (i == 1 || i == 2) ? Element.ALIGN_LEFT : Element.ALIGN_CENTER,
                                Padding = 5
                            };
                            table.AddCell(cell);
                        }
                    }
                    document.Add(table);

                    // Thời gian xuất
                    Paragraph footer = new Paragraph($"\nNgày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", normalFont)
                    {
                        Alignment = Element.ALIGN_RIGHT,
                        SpacingBefore = 10
                    };
                    document.Add(footer);

                    document.Close();
                    writer.Close();

                    MessageBox.Show($"Xuất báo cáo PDF thành công!\nĐường dẫn: {saveDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở file
                    if (MessageBox.Show("Bạn có muốn mở file vừa xuất?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xuất PDF: {ex.Message}");
            }
        }

        /// <summary>
        /// Xuất báo cáo ra file Excel
        /// </summary>
        private void XuatBaoCaoExcel()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu báo cáo xếp loại",
                    FileName = $"BaoCaoXepLoai_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Xếp loại");

                        // Tiêu đề
                        worksheet.Cell(1, 1).Value = "BÁO CÁO XẾP LOẠI HỌC SINH";
                        worksheet.Range(1, 1, 1, 7).Merge();
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        // Thông tin học kỳ
                        dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                        string thongTinHocKy = $"Học kỳ: {selectedHocKy.Text}";

                        if (cbLop.SelectedItem != null)
                        {
                            dynamic selectedLop = cbLop.SelectedItem;
                            thongTinHocKy += $" - Lớp: {selectedLop.Text}";
                        }

                        worksheet.Cell(2, 1).Value = thongTinHocKy;
                        worksheet.Range(2, 1, 2, 7).Merge();
                        worksheet.Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        // Lấy thống kê
                        int maHocKy = selectedHocKy.Value;
                        int? maLop = null;
                        if (cbLop.SelectedItem != null)
                        {
                            dynamic selectedLop = cbLop.SelectedItem;
                            int lopValue = selectedLop.Value;
                            if (lopValue > 0) maLop = lopValue;
                        }

                        Dictionary<string, int> thongKe = xepLoaiBUS.ThongKeXepLoaiTongKet(maHocKy, maLop);
                        int tongSo = thongKe.Values.Sum();

                        // Thống kê
                        int currentRow = 4;
                        worksheet.Cell(currentRow, 1).Value = "THỐNG KÊ XẾP LOẠI";
                        worksheet.Range(currentRow, 1, currentRow, 5).Merge();
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.LightGray;

                        currentRow++;
                        string[] thongKeHeaders = { "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" };
                        for (int i = 0; i < thongKeHeaders.Length; i++)
                        {
                            worksheet.Cell(currentRow, i + 1).Value = thongKeHeaders[i];
                            worksheet.Cell(currentRow, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            worksheet.Cell(currentRow, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                        }

                        currentRow++;
                        for (int i = 0; i < thongKeHeaders.Length; i++)
                        {
                            int soLuong = thongKe[thongKeHeaders[i]];
                            double phanTram = tongSo > 0 ? (double)soLuong / tongSo * 100 : 0;
                            worksheet.Cell(currentRow, i + 1).Value = $"{soLuong} ({phanTram:0.0}%)";
                            worksheet.Cell(currentRow, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }

                        // Bảng danh sách
                        currentRow += 2;
                        worksheet.Cell(currentRow, 1).Value = "DANH SÁCH HỌC SINH";
                        worksheet.Range(currentRow, 1, currentRow, 7).Merge();
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.LightGray;

                        currentRow++;
                        string[] headers = { "Mã HS", "Họ tên", "Lớp", "ĐTB", "Hạnh kiểm", "Học lực", "Xếp loại" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cell(currentRow, i + 1).Value = headers[i];
                            worksheet.Cell(currentRow, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(currentRow, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            worksheet.Cell(currentRow, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                        }

                        // Dữ liệu
                        currentRow++;
                        foreach (DataGridViewRow row in tableXepLoai.Rows)
                        {
                            if (row.IsNewRow) continue;

                            for (int i = 0; i < 7; i++)
                            {
                                worksheet.Cell(currentRow, i + 1).Value = row.Cells[i].Value?.ToString() ?? "";

                                if (i == 0) // Mã HS
                                {
                                    worksheet.Cell(currentRow, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                                else if (i == 3) // ĐTB
                                {
                                    worksheet.Cell(currentRow, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                                else if (i >= 4) // Hạnh kiểm, Học lực, Xếp loại
                                {
                                    worksheet.Cell(currentRow, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                }
                            }
                            currentRow++;
                        }

                        // Thời gian xuất
                        currentRow++;
                        worksheet.Cell(currentRow, 6).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
                        worksheet.Range(currentRow, 6, currentRow, 7).Merge();
                        worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                        // Định dạng
                        worksheet.Columns().AdjustToContents();
                        worksheet.Column(2).Width = 25; // Họ tên

                        // Border cho bảng thống kê
                        var thongKeRange = worksheet.Range(5, 1, 6, 5);
                        thongKeRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        thongKeRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        // Border cho bảng danh sách
                        var tableRange = worksheet.Range(9, 1, currentRow - 2, 7);
                        tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        workbook.SaveAs(saveDialog.FileName);
                    }

                    MessageBox.Show($"Xuất báo cáo Excel thành công!\nĐường dẫn: {saveDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở file
                    if (MessageBox.Show("Bạn có muốn mở file vừa xuất?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xuất Excel: {ex.Message}");
            }
        }
        /// <summary>
        /// Load thống kê xếp loại theo từng khối lớp
        /// Khối 10 = MaKhoi 10, Khối 11 = MaKhoi 11, Khối 12 = MaKhoi 12
        /// </summary>
        private void LoadThongKeTheoKhoi()
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    // Reset về 0 cho tất cả các card
                    ResetCardTheoKhoi(cardTheoKhoi1, "Khối 10");
                    ResetCardTheoKhoi(cardTheoKhoi2, "Khối 11");
                    ResetCardTheoKhoi(cardTheoKhoi3, "Khối 12");
                    return;
                }

                dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                // Thống kê cho Khối 10 (MaKhoi = 10)
                CapNhatCardTheoKhoi(cardTheoKhoi1, maHocKy, 10, "Khối 10");

                // Thống kê cho Khối 11 (MaKhoi = 11)
                CapNhatCardTheoKhoi(cardTheoKhoi2, maHocKy, 11, "Khối 11");

                // Thống kê cho Khối 12 (MaKhoi = 12)
                CapNhatCardTheoKhoi(cardTheoKhoi3, maHocKy, 12, "Khối 12");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê theo khối: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật thông tin cho card thống kê theo khối
        /// Luôn hiển thị đúng tổng số học sinh của khối
        /// Xếp loại nào chưa có thì hiển thị 0
        /// </summary>
        private void CapNhatCardTheoKhoi(dynamic card, int maHocKy, int maKhoi, string tenKhoi)
        {
            try
            {
                // Đếm tổng số học sinh của khối (bao gồm cả chưa có xếp loại)
                int tongSoHocSinh = xepLoaiBUS.DemTongSoHocSinhTheoKhoi(maHocKy, maKhoi);

                // Lấy thống kê xếp loại tổng kết theo khối (chỉ đếm học sinh có xếp loại)
                Dictionary<string, int> thongKe = xepLoaiBUS.ThongKeXepLoaiTongKetTheoKhoi(maHocKy, maKhoi);

                // Cập nhật các thuộc tính của card - TÁCH RIÊNG YẾU VÀ KÉM
                card.SoGioi = thongKe["Giỏi"].ToString();
                card.SoKha = thongKe["Khá"].ToString();
                card.SoTrungBinh = thongKe["Trung bình"].ToString();
                card.SoYeu = thongKe["Yếu"].ToString(); // CHỈ TÍNH YẾU
                card.SoKem = thongKe["Kém"].ToString(); // TÍNH KÉM RIÊNG
                card.SoHocSinhKhoi = $"{tongSoHocSinh} học sinh";
                card.KhoiLop = tenKhoi;
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, vẫn cố gắng hiển thị số học sinh
                try
                {
                    int tongSoHocSinh = xepLoaiBUS.DemTongSoHocSinhTheoKhoi(maHocKy, maKhoi);
                    card.SoGioi = "0";
                    card.SoKha = "0";
                    card.SoTrungBinh = "0";
                    card.SoYeu = "0";
                    card.SoKem = "0"; // Thêm reset cho Kém
                    card.SoHocSinhKhoi = $"{tongSoHocSinh} học sinh";
                    card.KhoiLop = tenKhoi;
                }
                catch
                {
                    ResetCardTheoKhoi(card, tenKhoi);
                }

                Console.WriteLine($"Lỗi cập nhật card {tenKhoi}: {ex.Message}");
            }
        }

        /// <summary>
        /// Reset card thống kê theo khối về giá trị mặc định
        /// </summary>
        private void ResetCardTheoKhoi(dynamic card, string tenKhoi)
        {
            card.SoGioi = "0";
            card.SoKha = "0";
            card.SoTrungBinh = "0";
            card.SoYeu = "0";
            card.SoKem = "0"; // Thêm reset cho Kém
            card.SoHocSinhKhoi = "0 học sinh";
            card.KhoiLop = tenKhoi;
        }

        private void ucXepLoai_Load(object sender, EventArgs e)
        {
           

            LoadThongKe();
            LoadThongKeToanTruong();
            LoadThongKeTheoKhoi();
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDuLieuXepLoai();
        }

        private void panelTrong_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLuuTongKet_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                // Đếm số học sinh có đủ điều kiện
                int soHocSinhDuDieuKien = 0;
                int soLuuThanhCong = 0;
                int soLuuThatBai = 0;
                int soThieuDieuKien = 0;

                foreach (DataGridViewRow row in tableXepLoai.Rows)
                {
                    if (row.IsNewRow) continue;

                    int maHocSinh = Convert.ToInt32(row.Cells[0].Value);
                    string hanhKiem = row.Cells[4].Value?.ToString() ?? "";
                    string hocLuc = row.Cells[5].Value?.ToString() ?? "";
                    string xepLoai = row.Cells[6].Value?.ToString() ?? "";

                    // Chỉ lưu nếu có đủ hạnh kiểm và học lực
                    if (!string.IsNullOrEmpty(hanhKiem) && !string.IsNullOrEmpty(hocLuc) &&
                        !string.IsNullOrEmpty(xepLoai))
                    {
                        soHocSinhDuDieuKien++;

                        if (xepLoaiBUS.LuuXepLoai(maHocSinh, maHocKy, xepLoai, ""))
                        {
                            soLuuThanhCong++;
                        }
                        else
                        {
                            soLuuThatBai++;
                        }
                    }
                    else
                    {
                        soThieuDieuKien++;
                    }
                }

                string thongBao = $"Kết quả lưu xếp loại tổng kết:\n" +
                                 $"- Lưu thành công: {soLuuThanhCong} học sinh\n";

                if (soLuuThatBai > 0)
                {
                    thongBao += $"- Lưu thất bại: {soLuuThatBai} học sinh\n";
                }

                if (soThieuDieuKien > 0)
                {
                    thongBao += $"- Chưa đủ điều kiện (thiếu học lực hoặc hạnh kiểm): {soThieuDieuKien} học sinh\n";
                }

                MessageBox.Show(thongBao,
                    soLuuThatBai == 0 ? "Thành công" : "Thông báo",
                    MessageBoxButtons.OK,
                    soLuuThatBai == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu xếp loại: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void progressBarKem_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cardTheoKhoi3_Load(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
