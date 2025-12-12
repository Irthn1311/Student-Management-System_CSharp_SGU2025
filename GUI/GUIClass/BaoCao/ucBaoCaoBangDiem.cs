using ClosedXML.Excel;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ucBaoCaoBangDiem : UserControl
    {
        private LopHocBUS lopHocBUS;
        private NhapDiemBUS nhapDiemBUS; // ✅ THÊM
        private int currentMaHocKy = 0;
        private int? currentMaLop = null; // ✅ THÊM: Lưu mã lớp hiện tại
        private List<XemBangDiemDTO> currentBangDiemData = null; // Lưu dữ liệu bảng điểm hiện tại

        public ucBaoCaoBangDiem()
        {
            InitializeComponent();
            lopHocBUS = new LopHocBUS();
            nhapDiemBUS = new NhapDiemBUS(); // ✅ THÊM
            SetupGradeTable();

            // Khởi tạo combobox với placeholder
            cboClassSelect.Items.Clear();
            cboClassSelect.Items.Add("-- Chọn học kỳ trước --");
            cboClassSelect.SelectedIndex = 0;
            cboClassSelect.Enabled = false;
        }
        /// <summary>
        /// Load danh sách lớp theo học kỳ được chọn từ ucBaoCao
        /// Được gọi từ ucBaoCao khi người dùng thay đổi học kỳ
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ được chọn</param>
        public void LoadClassesBySemester(int maHocKy)
        {
            try
            {
                currentMaHocKy = maHocKy;

                // Xóa dữ liệu cũ
                cboClassSelect.Items.Clear();
                cboClassSelect.SelectedIndexChanged -= CboClassSelect_SelectedIndexChanged;

                // ✅ SỬA: Lấy danh sách lớp THEO HỌC KỲ (thông qua PhanLop)
                List<LopDTO> dsLop = lopHocBUS.GetDanhSachLopTheoHocKy(maHocKy);

                // Kiểm tra nếu không có lớp nào
                if (dsLop == null || dsLop.Count == 0)
                {
                    cboClassSelect.Items.Add("-- Không có lớp nào --");
                    cboClassSelect.SelectedIndex = 0;
                    cboClassSelect.Enabled = false;

                    // Xóa dữ liệu cũ trong bảng
                    dgvGrades.Rows.Clear();
                    return;
                }

                // Thêm option "Tất cả lớp"
                cboClassSelect.Items.Add(new ComboBoxItem
                {
                    Text = "Tất cả lớp",
                    Value = -1
                });

                // Thêm danh sách lớp vào combobox (đã được sắp xếp từ DAO)
                foreach (var lop in dsLop)
                {
                    cboClassSelect.Items.Add(new ComboBoxItem
                    {
                        Text = lop.TenLop,
                        Value = lop.MaLop
                    });
                }

                // Cấu hình combobox
                cboClassSelect.DisplayMember = "Text";
                cboClassSelect.ValueMember = "Value";
                cboClassSelect.Enabled = true;

                // Chọn item đầu tiên (Tất cả lớp)
                cboClassSelect.SelectedIndex = 0;

                // Bật lại event handler
                cboClassSelect.SelectedIndexChanged += CboClassSelect_SelectedIndexChanged;

                // Load dữ liệu cho tất cả lớp
                LoadGradeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load danh sách lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset về trạng thái mặc định khi có lỗi
                cboClassSelect.Items.Clear();
                cboClassSelect.Items.Add("-- Lỗi load dữ liệu --");
                cboClassSelect.SelectedIndex = 0;
                cboClassSelect.Enabled = false;
            }
        }

        private void SetupGradeTable()
        {
            // Configure DataGridView appearance
            dgvGrades.DefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 250, 251);
            dgvGrades.DefaultCellStyle.SelectionForeColor = Color.FromArgb(55, 65, 81);
            dgvGrades.EnableHeadersVisualStyles = false;
            dgvGrades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvGrades.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(17, 24, 39);
            dgvGrades.ColumnHeadersDefaultCellStyle.Font = new Font("Inter", 9F, FontStyle.Bold);
            dgvGrades.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            dgvGrades.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Configure DataGridView columns
            dgvGrades.Columns.Clear();

            // STT Column
            var colSTT = new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Inter", 10F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    Padding = new Padding(12, 0, 12, 0)
                }
            };
            dgvGrades.Columns.Add(colSTT);

            // Student Name Column
            var colStudent = new DataGridViewTextBoxColumn
            {
                Name = "HocSinh",
                HeaderText = "Học sinh",
                Width = 260,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Inter", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    Padding = new Padding(12, 0, 12, 0)
                }
            };
            dgvGrades.Columns.Add(colStudent);

            // Subject Columns with center alignment
            var subjects = new[] { "Toán", "Văn", "Anh", "Lý", "Hóa", "TB" };
            foreach (var subject in subjects)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = subject,
                    HeaderText = subject,
                    Width = subject == "TB" ? 110 : 115,
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } },
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Inter", 10F, subject == "TB" ? FontStyle.Bold : FontStyle.Regular),
                        ForeColor = subject == "TB" ? Color.FromArgb(30, 136, 229) : Color.FromArgb(55, 65, 81)
                    }
                };
                dgvGrades.Columns.Add(col);
            }

            // ✅ Giữ nguyên màu header khi chọn dòng
            dgvGrades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrades.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvGrades.ColumnHeadersDefaultCellStyle.BackColor;
            dgvGrades.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgvGrades.ColumnHeadersDefaultCellStyle.ForeColor;
        }


        /// <summary>
        /// ✅ LOAD ĐIỂM SỐ THỰC TẾ TỪ DATABASE
        /// Load điểm của học sinh theo lớp và học kỳ
        /// </summary>
        private void LoadGradeData()
        {
            try
            {
                // Kiểm tra học kỳ đã được chọn chưa
                if (currentMaHocKy == 0)
                {
                    dgvGrades.Rows.Clear();
                    return;
                }

                dgvGrades.Rows.Clear();

                // Lấy dữ liệu bảng điểm từ BUS
                List<XemBangDiemDTO> dsBangDiem;

                if (currentMaLop.HasValue && currentMaLop.Value > 0)
                {
                    // Load điểm theo lớp cụ thể
                    dsBangDiem = nhapDiemBUS.GetBangDiemTheoHocKyVaLop(currentMaHocKy, currentMaLop.Value);
                }
                else
                {
                    // Load điểm cho tất cả lớp trong học kỳ
                    dsBangDiem = nhapDiemBUS.GetBangDiemTheoHocKy(currentMaHocKy);
                }

                // Kiểm tra nếu không có dữ liệu
                if (dsBangDiem == null || dsBangDiem.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu điểm cho học kỳ và lớp được chọn.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Xóa biểu đồ nếu không có dữ liệu
                    ClearCharts();
                    return;
                }

                // Lưu dữ liệu để sử dụng cho biểu đồ
                currentBangDiemData = dsBangDiem;

                // Đưa dữ liệu vào DataGridView
                int stt = 1;
                foreach (var item in dsBangDiem)
                {
                    dgvGrades.Rows.Add(
                        stt++,
                        item.HoTen,
                        item.DiemToan.HasValue ? item.DiemToan.Value.ToString("0.0") : "",
                        item.DiemVan.HasValue ? item.DiemVan.Value.ToString("0.0") : "",
                        item.DiemAnh.HasValue ? item.DiemAnh.Value.ToString("0.0") : "",
                        item.DiemLy.HasValue ? item.DiemLy.Value.ToString("0.0") : "",
                        item.DiemHoa.HasValue ? item.DiemHoa.Value.ToString("0.0") : "",
                        item.DiemTB.HasValue ? item.DiemTB.Value.ToString("0.0") : ""
                    );
                }

                // ✅ Áp dụng màu cho cột điểm trung bình
                ApplyColorToDiemTB();

                // ✅ Vẽ biểu đồ thống kê
                VeBarChart();
                VeLineChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu điểm: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ ÁP DỤNG MÀU CHO CỘT ĐIỂM TRUNG BÌNH
        /// </summary>
        private void ApplyColorToDiemTB()
        {
            foreach (DataGridViewRow row in dgvGrades.Rows)
            {
                // Cột TB là cột thứ 7 (index 7)
                if (row.Cells[7].Value != null && !string.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                {
                    if (float.TryParse(row.Cells[7].Value.ToString(), out float score))
                    {
                        if (score >= 8.0)
                        {
                            // Xanh lá - Giỏi
                            row.Cells[7].Style.ForeColor = Color.FromArgb(22, 163, 74);
                            row.Cells[7].Style.Font = new Font("Inter", 10F, FontStyle.Bold);
                        }
                        else if (score >= 6.5)
                        {
                            // Xanh dương - Khá
                            row.Cells[7].Style.ForeColor = Color.FromArgb(30, 136, 229);
                            row.Cells[7].Style.Font = new Font("Inter", 10F, FontStyle.Bold);
                        }
                        else if (score >= 5.0)
                        {
                            // Vàng - Trung bình
                            row.Cells[7].Style.ForeColor = Color.FromArgb(234, 179, 8);
                            row.Cells[7].Style.Font = new Font("Inter", 10F, FontStyle.Bold);
                        }
                        else
                        {
                            // Đỏ - Yếu
                            row.Cells[7].Style.ForeColor = Color.FromArgb(220, 38, 38);
                            row.Cells[7].Style.Font = new Font("Inter", 10F, FontStyle.Bold);
                        }
                    }
                }
            }
        }

        private void CboClassSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Reload grade data when class is changed
                if (cboClassSelect.SelectedItem is ComboBoxItem item)
                {
                    int maLop = (int)item.Value;

                    // Cập nhật mã lớp hiện tại
                    if (maLop == -1)
                    {
                        currentMaLop = null; // Tất cả lớp
                    }
                    else
                    {
                        currentMaLop = maLop; // Lớp cụ thể
                    }

                    // ✅ Load dữ liệu điểm thực tế
                    LoadGradeData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
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
                // Tạo SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Xuất báo cáo điểm số",
                    FileName = $"BaoCao_DiemSo_HocKy_{currentMaHocKy}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Lấy thống kê điểm số
                    ThongKeDTO thongKe = nhapDiemBUS.GetThongKeDiemTheoHocKy(currentMaHocKy);

                    // Lấy danh sách lớp có điểm
                    List<LopDTO> dsLopCoDiem = nhapDiemBUS.GetDanhSachLopCoDiem(currentMaHocKy);

                    // Kiểm tra có dữ liệu không
                    if (thongKe == null || dsLopCoDiem == null || dsLopCoDiem.Count == 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Không có dữ liệu điểm để xuất!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Xuất Excel
                    XuatExcelBaoCaoDiemSo(saveDialog.FileName, thongKe, dsLopCoDiem, currentMaHocKy);

                    Cursor = Cursors.Default;

                    MessageBox.Show($"Xuất báo cáo Excel thành công!\nĐường dẫn: {saveDialog.FileName}",
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
                MessageBox.Show($"Lỗi khi xuất báo cáo Excel: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất báo cáo thống kê điểm số ra Excel
        /// </summary>
        private void XuatExcelBaoCaoDiemSo(string filePath, ThongKeDTO thongKe,
            List<LopDTO> dsLopCoDiem, int maHocKy)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Báo cáo điểm số");

            // === TIÊU ĐỀ ===
            worksheet.Cell(1, 1).Value = "THỐNG KÊ BÁO CÁO ĐIỂM SỐ";
            worksheet.Range(1, 1, 1, 8).Merge();
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 18;
            worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(33, 150, 243);
            worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;

            // === THÔNG TIN HỌC KỲ ===
            worksheet.Cell(2, 1).Value = $"Học kỳ: {GetTenHocKy(maHocKy)}";
            worksheet.Cell(2, 1).Style.Font.Bold = true;
            worksheet.Cell(2, 1).Style.Font.FontSize = 12;

            worksheet.Cell(3, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
            worksheet.Cell(3, 1).Style.Font.FontSize = 10;

            // === THỐNG KÊ TỔNG QUAN ===
            int currentRow = 5;
            worksheet.Cell(currentRow, 1).Value = "THỐNG KÊ TỔNG QUAN";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 1).Style.Font.FontSize = 14;
            worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Range(currentRow, 1, currentRow, 2).Merge();
            currentRow++;

            // Điểm trung bình chung
            worksheet.Cell(currentRow, 1).Value = "Điểm trung bình chung:";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 2).Value = thongKe.DiemTBChung.ToString("0.00");
            worksheet.Cell(currentRow, 2).Style.Font.FontColor = XLColor.FromArgb(30, 136, 229);
            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
            currentRow++;

            // Điểm trung bình cao nhất
            worksheet.Cell(currentRow, 1).Value = "Điểm trung bình cao nhất:";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 2).Value = thongKe.DiemCaoNhat.ToString("0.00");
            worksheet.Cell(currentRow, 2).Style.Font.FontColor = XLColor.FromArgb(22, 163, 74);
            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
            currentRow++;

            // Học sinh có điểm cao nhất
            worksheet.Cell(currentRow, 1).Value = "Học sinh có điểm cao nhất:";
            worksheet.Cell(currentRow, 1).Style.Alignment.Indent = 2;
            worksheet.Cell(currentRow, 2).Value = thongKe.HocSinhDiemCaoNhat;
            currentRow++;

            // Điểm trung bình thấp nhất
            worksheet.Cell(currentRow, 1).Value = "Điểm trung bình thấp nhất:";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 2).Value = thongKe.DiemThapNhat.ToString("0.00");
            worksheet.Cell(currentRow, 2).Style.Font.FontColor = XLColor.FromArgb(220, 38, 38);
            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
            currentRow++;

            // Học sinh có điểm thấp nhất
            worksheet.Cell(currentRow, 1).Value = "Học sinh có điểm thấp nhất:";
            worksheet.Cell(currentRow, 1).Style.Alignment.Indent = 2;
            worksheet.Cell(currentRow, 2).Value = thongKe.HocSinhDiemThapNhat;
            currentRow++;

            // Tổng số học sinh
            worksheet.Cell(currentRow, 1).Value = "Tổng số học sinh:";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 2).Value = thongKe.TongHocSinh;
            currentRow++;

            // Học sinh đã có điểm
            worksheet.Cell(currentRow, 1).Value = "Học sinh đã có điểm:";
            worksheet.Cell(currentRow, 1).Style.Alignment.Indent = 2;
            worksheet.Cell(currentRow, 2).Value = $"{thongKe.HocSinhDaNhapDiem} ({thongKe.TinhPhanTramDaNhapDiem():0.0}%)";
            worksheet.Cell(currentRow, 2).Style.Font.FontColor = XLColor.FromArgb(22, 163, 74);
            currentRow++;

            // Học sinh chưa có điểm
            worksheet.Cell(currentRow, 1).Value = "Học sinh chưa có điểm:";
            worksheet.Cell(currentRow, 1).Style.Alignment.Indent = 2;
            worksheet.Cell(currentRow, 2).Value = $"{thongKe.HocSinhChuaNhapDiem} ({thongKe.TinhPhanTramChuaNhapDiem():0.0}%)";
            worksheet.Cell(currentRow, 2).Style.Font.FontColor = XLColor.FromArgb(220, 38, 38);
            currentRow += 2;

            // === DANH SÁCH CÁC LỚP HỌC CÓ HỌC SINH CÓ ĐẦY ĐỦ ĐIỂM SỐ ===
            worksheet.Cell(currentRow, 1).Value = "DANH SÁCH CÁC LỚP HỌC CÓ HỌC SINH CÓ ĐẦY ĐỦ ĐIỂM SỐ";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 1).Style.Font.FontSize = 14;
            worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Range(currentRow, 1, currentRow, 8).Merge();
            currentRow++;

            // Header bảng
            int headerRow = currentRow;
            var headers = new[] { "STT", "Tên lớp", "Khối", "Sĩ số", "HS có đủ điểm",
        "Điểm TB lớp", "HS Giỏi", "HS Khá" };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.FromArgb(229, 231, 235);
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            currentRow++;

            // Dữ liệu các lớp
            int stt = 1;
            int tongSiSo = 0;
            int tongHSDaDu = 0;
            float tongDiemTBLop = 0;

            foreach (var lop in dsLopCoDiem.OrderBy(l => l.TenLop))
            {
                // Lấy danh sách học sinh có đủ điểm trong lớp
                List<XemBangDiemDTO> dsBangDiem = nhapDiemBUS.GetBangDiemTheoHocKyVaLop(maHocKy, lop.MaLop);

                // Đếm học sinh có điểm TB (đầy đủ điểm)
                int hsDaDu = dsBangDiem.Count(hs => hs.DiemTB.HasValue);

                // Tính điểm TB của lớp
                float? diemTBLop = hsDaDu > 0 ?
                    dsBangDiem.Where(hs => hs.DiemTB.HasValue).Average(hs => hs.DiemTB.Value) :
                    (float?)null;

                // Đếm học sinh giỏi và khá
                int hsGioi = dsBangDiem.Count(hs => hs.DiemTB.HasValue && hs.DiemTB.Value >= 8.0);
                int hsKha = dsBangDiem.Count(hs => hs.DiemTB.HasValue && hs.DiemTB.Value >= 6.5 && hs.DiemTB.Value < 8.0);

                worksheet.Cell(currentRow, 1).Value = stt++;
                worksheet.Cell(currentRow, 2).Value = lop.TenLop;
                worksheet.Cell(currentRow, 3).Value = lop.MaKhoi;
                worksheet.Cell(currentRow, 4).Value = dsBangDiem.Count;
                worksheet.Cell(currentRow, 5).Value = hsDaDu;
                worksheet.Cell(currentRow, 6).Value = diemTBLop.HasValue ?
                    diemTBLop.Value.ToString("0.00") : "N/A";
                worksheet.Cell(currentRow, 7).Value = hsGioi;
                worksheet.Cell(currentRow, 8).Value = hsKha;

                // Căn giữa các cột số
                for (int i = 1; i <= 8; i++)
                {
                    if (i != 2) // Trừ cột Tên lớp
                    {
                        worksheet.Cell(currentRow, i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }
                    worksheet.Cell(currentRow, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // Tô màu điểm TB lớp
                if (diemTBLop.HasValue)
                {
                    var cell = worksheet.Cell(currentRow, 6);
                    if (diemTBLop.Value >= 8.0)
                        cell.Style.Font.FontColor = XLColor.FromArgb(22, 163, 74);
                    else if (diemTBLop.Value >= 6.5)
                        cell.Style.Font.FontColor = XLColor.FromArgb(30, 136, 229);
                    else if (diemTBLop.Value >= 5.0)
                        cell.Style.Font.FontColor = XLColor.FromArgb(234, 179, 8);
                    else
                        cell.Style.Font.FontColor = XLColor.FromArgb(220, 38, 38);

                    cell.Style.Font.Bold = true;
                    tongDiemTBLop += diemTBLop.Value;
                }

                tongSiSo += dsBangDiem.Count;
                tongHSDaDu += hsDaDu;

                currentRow++;
            }

            // Tổng cộng
            currentRow++;
            worksheet.Cell(currentRow, 1).Value = "TỔNG CỘNG";
            worksheet.Range(currentRow, 1, currentRow, 3).Merge();
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 1).Style.Fill.BackgroundColor = XLColor.LightYellow;

            worksheet.Cell(currentRow, 4).Value = tongSiSo;
            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 4).Style.Fill.BackgroundColor = XLColor.LightYellow;

            worksheet.Cell(currentRow, 5).Value = tongHSDaDu;
            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 5).Style.Fill.BackgroundColor = XLColor.LightYellow;

            float diemTBToanKhoi = dsLopCoDiem.Count > 0 ? tongDiemTBLop / dsLopCoDiem.Count : 0;
            worksheet.Cell(currentRow, 6).Value = diemTBToanKhoi.ToString("0.00");
            worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(currentRow, 6).Style.Fill.BackgroundColor = XLColor.LightYellow;

            // Border tổng cộng
            for (int i = 1; i <= 8; i++)
            {
                worksheet.Cell(currentRow, i).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            }

            // === CHÚ THÍCH ===
            currentRow += 2;
            worksheet.Cell(currentRow, 1).Value = "Chú thích:";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 1).Style.Font.Italic = true;
            currentRow++;

            worksheet.Cell(currentRow, 1).Value = "- HS có đủ điểm: Học sinh đã có điểm trung bình (đủ điểm 13 môn)";
            worksheet.Cell(currentRow, 1).Style.Font.Italic = true;
            currentRow++;

            worksheet.Cell(currentRow, 1).Value = "- Điểm TB lớp: Trung bình điểm TB của các học sinh có đủ điểm trong lớp";
            worksheet.Cell(currentRow, 1).Style.Font.Italic = true;
            currentRow++;

            worksheet.Cell(currentRow, 1).Value = "- HS Giỏi: Học sinh có điểm TB >= 8.0";
            worksheet.Cell(currentRow, 1).Style.Font.Italic = true;
            currentRow++;

            worksheet.Cell(currentRow, 1).Value = "- HS Khá: Học sinh có điểm TB từ 6.5 đến < 8.0";
            worksheet.Cell(currentRow, 1).Style.Font.Italic = true;

            // === TỰ ĐỘNG ĐIỀU CHỈNH ĐỘ RỘNG CỘT ===
            worksheet.Columns().AdjustToContents();

            // Đảm bảo độ rộng tối thiểu
            if (worksheet.Column(1).Width < 8) worksheet.Column(1).Width = 8;   // STT
            if (worksheet.Column(2).Width < 20) worksheet.Column(2).Width = 20; // Tên lớp
            if (worksheet.Column(3).Width < 10) worksheet.Column(3).Width = 10; // Khối

            // Lưu file
            workbook.SaveAs(filePath);
        }

        /// <summary>
        /// Lấy tên học kỳ từ mã học kỳ
        /// </summary>
        private string GetTenHocKy(int maHocKy)
        {
            try
            {
                List<HocKyDTO> dsHocKy = nhapDiemBUS.GetDanhSachHocKy();
                HocKyDTO hocKy = dsHocKy.FirstOrDefault(hk => hk.MaHocKy == maHocKy);
                return hocKy != null ? $"{hocKy.TenHocKy} - {hocKy.MaNamHoc}" : $"Học kỳ {maHocKy}";
            }
            catch
            {
                return $"Học kỳ {maHocKy}";
            }
        }

        private void pnlGradeTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvGrades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// ✅ Vẽ bar chart
        /// - Nếu "Tất cả lớp": Vẽ biểu đồ phân phối điểm (histogram) full width
        /// - Nếu lớp cụ thể: Ẩn bar chart (chỉ hiển thị line chart)
        /// </summary>
        private void VeBarChart()
        {
            try
            {
                chartBar.Series.Clear();
                chartBar.ChartAreas.Clear();
                chartBar.Legends.Clear();

                // Kiểm tra có dữ liệu không
                if (currentBangDiemData == null || currentBangDiemData.Count == 0)
                {
                    ClearCharts();
                    return;
                }

                // Nếu chọn "Tất cả lớp": Vẽ biểu đồ phân phối điểm full width
                if (!currentMaLop.HasValue || currentMaLop.Value <= 0)
                {
                    // Điều chỉnh layout: chartBar full width, ẩn chartLine
                    chartBar.Visible = true; // ✅ Đảm bảo chartBar hiển thị khi chọn "Tất cả lớp"
                    chartBar.Location = new Point(20, 62);
                    chartBar.Size = new Size(1080, 258);
                    chartLine.Visible = false;
                    
                    VePhanPhoiDiem();
                    return;
                }

                // Nếu chọn lớp cụ thể: Ẩn bar chart, chỉ hiển thị line chart
                chartBar.Visible = false;
                chartLine.Visible = true;
                // Điều chỉnh layout: line chart full width
                chartLine.Location = new Point(20, 62);
                chartLine.Size = new Size(1080, 258);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi vẽ bar chart: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Vẽ biểu đồ phân phối điểm (histogram) khi chọn "Tất cả lớp"
        /// </summary>
        private void VePhanPhoiDiem()
        {
            try
            {
                // Lọc học sinh có điểm trung bình
                var dsCoDiem = currentBangDiemData
                    .Where(hs => hs.DiemTB.HasValue)
                    .ToList();

                if (dsCoDiem.Count == 0)
                {
                    ClearCharts();
                    return;
                }

                // Tạo ChartArea
                ChartArea chartArea = new ChartArea("ChartArea1");
                chartArea.AxisX.Title = "Điểm";
                chartArea.AxisX.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
                chartArea.AxisY.Title = "Số lượng học sinh";
                chartArea.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
                chartArea.BackColor = Color.White;
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
                // Hiển thị nhãn mỗi 1 điểm để dễ đọc (0, 1, 2, ..., 10)
                chartArea.AxisX.Interval = 1.0;
                chartArea.AxisX.LabelStyle.Angle = -45;
                chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 7);
                chartBar.ChartAreas.Add(chartArea);

                // Tạo Series
                Series series = new Series("Phân phối điểm");
                series.ChartType = SeriesChartType.Column;
                series.Color = Color.FromArgb(30, 136, 229);
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "{0}";
                series.Font = new Font("Segoe UI", 7, FontStyle.Bold);
                series.LabelForeColor = Color.FromArgb(17, 24, 39);

                // Tạo các khoảng điểm từ 0.0 đến 10.0 với bước 0.25
                Dictionary<string, int> phanPhoiDiem = new Dictionary<string, int>();
                for (double diem = 0.0; diem <= 10.0; diem += 0.25)
                {
                    phanPhoiDiem[diem.ToString("0.00")] = 0;
                }

                // Đếm số lượng học sinh trong mỗi khoảng điểm
                foreach (var item in dsCoDiem)
                {
                    if (item.DiemTB.HasValue)
                    {
                        double diem = item.DiemTB.Value;
                        // Làm tròn xuống đến bước 0.25 gần nhất
                        double diemLamTron = Math.Floor(diem * 4) / 4;
                        string key = diemLamTron.ToString("0.00");
                        if (phanPhoiDiem.ContainsKey(key))
                        {
                            phanPhoiDiem[key]++;
                        }
                    }
                }

                // Thêm dữ liệu vào biểu đồ
                foreach (var kvp in phanPhoiDiem)
                {
                    double diem = double.Parse(kvp.Key);
                    int soLuong = kvp.Value;
                    
                    DataPoint point = series.Points.Add(soLuong);
                    // Hiển thị nhãn điểm trên trục X
                    point.AxisLabel = diem.ToString("0.00");
                    // Chỉ hiển thị số lượng trên cột khi có dữ liệu
                    point.Label = soLuong > 0 ? soLuong.ToString() : "";
                    point.Color = Color.FromArgb(30, 136, 229);
                }

                chartBar.Series.Add(series);
                chartBar.Titles.Clear();
                chartBar.Titles.Add("Biểu đồ phổ điểm trung bình");
                chartBar.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
                chartBar.Titles[0].ForeColor = Color.FromArgb(17, 24, 39);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi vẽ biểu đồ phân phối điểm: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// ✅ Vẽ line chart
        /// - Nếu "Tất cả lớp": Ẩn biểu đồ line (chỉ hiển thị bar chart phân phối)
        /// - Nếu lớp cụ thể: Vẽ biểu đồ xu hướng điểm trung bình full width
        /// </summary>
        private void VeLineChart()
        {
            try
            {
                chartLine.Series.Clear();
                chartLine.ChartAreas.Clear();
                chartLine.Legends.Clear();

                // Kiểm tra có dữ liệu không
                if (currentBangDiemData == null || currentBangDiemData.Count == 0)
                {
                    return;
                }

                // Nếu chọn "Tất cả lớp": Ẩn line chart (chỉ hiển thị bar chart phân phối)
                if (!currentMaLop.HasValue || currentMaLop.Value <= 0)
                {
                    return;
                }

                // Nếu chọn lớp cụ thể: Vẽ biểu đồ xu hướng điểm trung bình full width
                // Tạo ChartArea
                ChartArea chartArea = new ChartArea("ChartArea1");
                chartArea.AxisX.Title = "Thứ tự học sinh";
                chartArea.AxisX.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
                chartArea.AxisY.Title = "Điểm trung bình";
                chartArea.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
                chartArea.AxisY.Minimum = 0;
                chartArea.AxisY.Maximum = 10;
                chartArea.BackColor = Color.White;
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
                chartLine.ChartAreas.Add(chartArea);

                // Tạo Series
                Series series = new Series("Điểm trung bình");
                series.ChartType = SeriesChartType.Line;
                series.Color = Color.FromArgb(30, 136, 229);
                series.BorderWidth = 3;
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 8;
                series.MarkerColor = Color.FromArgb(30, 136, 229);
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "{0:0.0}";
                series.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                series.LabelForeColor = Color.FromArgb(17, 24, 39);

                // Lọc học sinh có điểm trung bình và sắp xếp theo thứ tự trong bảng
                var dsCoDiem = currentBangDiemData
                    .Where(hs => hs.DiemTB.HasValue)
                    .ToList();

                if (dsCoDiem.Count == 0)
                {
                    return;
                }

                // Thêm dữ liệu vào biểu đồ theo thứ tự
                int stt = 1;
                foreach (var item in dsCoDiem)
                {
                    if (item.DiemTB.HasValue)
                    {
                        DataPoint point = series.Points.Add(item.DiemTB.Value);
                        point.AxisLabel = stt.ToString();
                        point.Label = item.DiemTB.Value.ToString("0.0");
                        stt++;
                    }
                }

                chartLine.Series.Add(series);
                chartLine.Titles.Clear();
                chartLine.Titles.Add("Xu hướng điểm trung bình");
                chartLine.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);
                chartLine.Titles[0].ForeColor = Color.FromArgb(17, 24, 39);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi vẽ line chart: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// ✅ Xóa biểu đồ khi không có dữ liệu
        /// </summary>
        private void ClearCharts()
        {
            try
            {
                chartBar.Series.Clear();
                chartBar.ChartAreas.Clear();
                chartBar.Legends.Clear();
                chartBar.Titles.Clear();

                chartLine.Series.Clear();
                chartLine.ChartAreas.Clear();
                chartLine.Legends.Clear();
                chartLine.Titles.Clear();
                
                // Reset về layout mặc định
                chartBar.Location = new Point(20, 62);
                chartBar.Size = new Size(534, 258);
                chartBar.Visible = true;
                
                chartLine.Location = new Point(566, 62);
                chartLine.Size = new Size(534, 258);
                chartLine.Visible = true;
            }
            catch
            {
                // Bỏ qua lỗi khi xóa
            }
        }

        /// <summary>
        /// Helper class cho ComboBox items
        /// </summary>
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

    }
}
