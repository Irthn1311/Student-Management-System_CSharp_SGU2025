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
using System.ComponentModel;
using System.Linq;


namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class HanhKiem : UserControl
    {

        private HanhKiemBUS hanhKiemBUS;
        private HocKyDAO hocKyDAO;
        private LopDAO lopDAO;
        private PhanLopDAO phanLopDAO;
        private HocSinhDAO hocSinhDAO;
        private HanhKiemDAO hanhKiemDAO;
        private XepLoaiDAO xepLoaiDAO;

        public HanhKiem()
        {
            InitializeComponent();
            hanhKiemBUS = new HanhKiemBUS();
            hocKyDAO = new HocKyDAO();
            lopDAO = new LopDAO();
            phanLopDAO = new PhanLopDAO();
            hocSinhDAO = new HocSinhDAO();
            hanhKiemDAO = new HanhKiemDAO();
            xepLoaiDAO = new XepLoaiDAO();
        }

        private void headerHanhKiem_Load(object sender, EventArgs e)
        {

        }

        private void HanhKiem_Load(object sender, EventArgs e)
        {

            // Trang trí tableNhapDiem
            ConfigureTableHanhKiem();
            // ĐĂNG KÝ SỰ KIỆN ĐỂ LUÔN ÁP DỤNG MÀU CHO HÀNG MỚI VÀ KHI BINDING HOÀN TẤT
            tableHanhKiem.RowsAdded += TableHanhKiem_RowsAdded;
            tableHanhKiem.DataBindingComplete += TableHanhKiem_DataBindingComplete;

            // chèn dữ liệu mẫu vào Header
            headerHanhKiem.lbHeader.Text = "Hạnh kiểm";
            headerHanhKiem.lbGhiChu.Text = "Trang chủ / Hạnh kiểm";
            headerHanhKiem.lbTenDangNhap.Text = "Nguyễn Văn A";
            headerHanhKiem.lbVaiTro.Text = "Giáo vụ";

            // chèn dữ liệu mẫu vào các thẻ thống kê
            statCarHanhKiemTot.lbCardTitle.Text = "Hạnh kiểm tốt";
            statCarHanhKiemTot.lbCardValue.Text = "892";
            statCarHanhKiemTot.lbCardNote.Text = "71.5% học sinh";


            statCardHanhKiemKha.lbCardTitle.Text = "Hạnh kiểm khá";
            statCardHanhKiemKha.lbCardValue.Text = "278";
            statCardHanhKiemKha.lbCardNote.Text = "22.3% học sinh";

            statCardHanhKiemTrungBinh.lbCardTitle.Text = "Hạnh kiểm trung bình";
            statCardHanhKiemTrungBinh.lbCardValue.Text = "65";
            statCardHanhKiemTrungBinh.lbCardNote.Text = "5.2% học sinh";

            statCardHanhKiemYeu.lbCardTitle.Text = "Hạnh kiểm yếu";
            statCardHanhKiemYeu.lbCardValue.Text = "12";
            statCardHanhKiemYeu.lbCardNote.Text = "1% học sinh";

            statCardChuaDanhGiaHanhKiem.lbCardTitle.Text = "Chưa đánh giá";
            statCardChuaDanhGiaHanhKiem.lbCardValue.Text = "4 học sinh";
            statCardChuaDanhGiaHanhKiem.lbCardNote.Text = "0.3% học sinh";

            statCarHanhKiemTot.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardHanhKiemKha.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardHanhKiemTrungBinh.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardHanhKiemYeu.lbCardValue.ForeColor = Color.FromArgb(220, 38, 38);
            statCardChuaDanhGiaHanhKiem.lbCardValue.ForeColor = Color.FromArgb(158, 163, 255);
            statCardChuaDanhGiaHanhKiem.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statCarHanhKiemTot.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statCardHanhKiemYeu.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statCardHanhKiemKha.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statCardHanhKiemTrungBinh.lbCardValue.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            // Load ComboBox
            LoadHocKyComboBox();
            LoadLopComboBox();

            // ⭐ GẮN SỰ KIỆN TRƯỚC KHI SET SELECTEDINDEX
            cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
            cbLop.SelectedIndexChanged += cbLop_SelectedIndexChanged;

            // Load dữ liệu ban đầu
            if (cbHocKyNamHoc.Items.Count > 0)
            {
                cbHocKyNamHoc.SelectedIndex = 0;
                // ⭐ SAU KHI SET SELECTEDINDEX, SỰ KIỆN SelectedIndexChanged SẼ TỰ GỌI LoadDuLieuHanhKiem()
            }

        }

        // Hàm cấu hình tableNhapDiem
        private void ConfigureTableHanhKiem()
        {
            // Cấu hình header
            tableHanhKiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            tableHanhKiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableHanhKiem.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tableHanhKiem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableHanhKiem.ColumnHeadersHeight = 40;
            tableHanhKiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cấu hình cells
            tableHanhKiem.DefaultCellStyle.BackColor = Color.White;
            tableHanhKiem.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            tableHanhKiem.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tableHanhKiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableHanhKiem.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            tableHanhKiem.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Cấu hình rows
            tableHanhKiem.RowTemplate.Height = 60;
            tableHanhKiem.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Cấu hình borders
            tableHanhKiem.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableHanhKiem.GridColor = Color.FromArgb(229, 231, 235);
            tableHanhKiem.BorderStyle = BorderStyle.None;

            // Cấu hình columns width
            tableHanhKiem.Columns[0].Width = 70;  // Mã Học sinh
            tableHanhKiem.Columns[1].Width = 150; // Họ và Tên
            tableHanhKiem.Columns[2].Width = 70; // Lớp
            tableHanhKiem.Columns[3].Width = 80; // Học Kì
            tableHanhKiem.Columns[4].Width = 90; // Xếp Loại
            tableHanhKiem.Columns[5].Width = 250; // Nhận Xét

            //// Căn giữa các cột điểm
            //for (int i = 2; i <= 5; i++)
            //{
            //    tableHanhKiem.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}

            // Loại bỏ selection
            tableHanhKiem.EnableHeadersVisualStyles = false;

            // Ngăn đổi màu tiêu đề khi chọn
            tableHanhKiem.EnableHeadersVisualStyles = false;
            tableHanhKiem.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableHanhKiem.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(75, 85, 99);

            foreach (DataGridViewColumn col in tableHanhKiem.Columns)
            {
                col.ReadOnly = true; // khóa hết
            }
            tableHanhKiem.Columns[5].ReadOnly = false; // Cho phép sửa Nhận Xét



        }

        private void TableHanhKiem_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Áp màu cho các hàng vừa được thêm
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount && i < tableHanhKiem.Rows.Count; i++)
            {
                if (tableHanhKiem.Rows[i].IsNewRow) continue;
                string xepLoai = tableHanhKiem.Rows[i].Cells[4].Value?.ToString();
                ApplyXepLoaiColor(i, xepLoai);
            }
        }

        private void TableHanhKiem_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Áp màu cho toàn bộ hàng sau khi binding/refresh xong
            for (int i = 0; i < tableHanhKiem.Rows.Count; i++)
            {
                if (tableHanhKiem.Rows[i].IsNewRow) continue;
                string xepLoai = tableHanhKiem.Rows[i].Cells[4].Value?.ToString();
                ApplyXepLoaiColor(i, xepLoai);
            }
        }


        // Thêm các hàm mới
        private void LoadHocKyComboBox()
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải học kỳ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLopComboBox()
        {
            try
            {
                cbLop.Items.Clear();
                cbLop.DisplayMember = "Text";
                cbLop.ValueMember = "Value";

                // Thêm tùy chọn "Tất cả lớp"
                cbLop.Items.Add(new { Text = "Tất cả lớp", Value = 0 });

                List<LopDTO> dsLop = lopDAO.GetDanhSachLopCoHocSinh();

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
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm thêm dữ liệu mẫu vào tableHanhKiem
        private void LoadSampleDataHanhKiem()
        {
            tableHanhKiem.Rows.Clear();

            tableHanhKiem.Rows.Add("1", "Nguyễn Văn An", "9A1", "Học Kì I", "Tốt", "Chăm chỉ");
            tableHanhKiem.Rows.Add("2", "Trần Thị Bình", "9A2", "Học Kì I", "Khá", "Năng động");
            tableHanhKiem.Rows.Add("3", "Lê Văn Cường", "9A1", "Học Kì I", "Trung Bình", "Cần cố gắng");
            tableHanhKiem.Rows.Add("4", "Phạm Thị Dung", "9A3", "Học Kì I", "Yếu", "Thiếu tập trung");
            tableHanhKiem.Rows.Add("5", "Hoàng Văn Em", "9A2", "Học Kì I", "Tốt", "Gương mẫu");
            tableHanhKiem.Rows.Add("6", "Vũ Thị Hà", "9A1", "Học Kì I", "Khá", "Thân thiện");
            tableHanhKiem.Rows.Add("7", "Đỗ Văn Khoa", "9A3", "Học Kì I", "Trung Bình", "Cần cố gắng");
            tableHanhKiem.Rows.Add("8", "Ngô Thị Lan", "9A2", "Học Kì I", "Yếu", "Thiếu tập trung");
            tableHanhKiem.Rows.Add("9", "Bùi Văn Minh", "9A1", "Học Kì I", "Tốt", "Chăm chỉ");
            tableHanhKiem.Rows.Add("10", "Trịnh Thị Nga", "9A3", "Học Kì I", "Khá", "Năng động");
            tableHanhKiem.Rows.Add("11", "Phan Văn Quang", "9A2", "Học Kì I", "Trung Bình", "Cần cố gắng");

            // Đổi màu cột Xếp Loại dựa trên giá trị
            foreach (DataGridViewRow row in tableHanhKiem.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    string xepLoai = row.Cells[4].Value.ToString();
                    switch (xepLoai)
                    {
                        case "Tốt":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(22, 163, 74); // Màu xanh lá
                            break;
                        case "Khá":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(30, 136, 229); // Màu xanh dương
                            break;
                        case "Trung Bình":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(219, 39, 119); // Màu hồng
                            break;
                        case "Yếu":
                            row.Cells[4].Style.ForeColor = Color.FromArgb(220, 38, 38); // Màu đỏ
                            break;
                        default:
                            row.Cells[4].Style.ForeColor = Color.Black; // Mặc định
                            break;
                    }

                }
            }
        }

        private void statCardHanhKiemTrungBinh_Load(object sender, EventArgs e)
        {

        }

        private void btnLuuHanhKiem_Click(object sender, EventArgs e)
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

                int soLuuThanhCong = 0;
                int soLuuThatBai = 0;

                foreach (DataGridViewRow row in tableHanhKiem.Rows)
                {
                    if (row.IsNewRow) continue;

                    int maHocSinh = Convert.ToInt32(row.Cells[0].Value);
                    string xepLoai = row.Cells[4].Value?.ToString() ?? "";
                    string nhanXet = row.Cells[5].Value?.ToString() ?? "";

                    HanhKiemDTO hk = new HanhKiemDTO
                    {
                        MaHocSinh = maHocSinh,
                        MaHocKy = maHocKy,
                        XepLoai = xepLoai,
                        NhanXet = nhanXet
                    };

                    if (hanhKiemBUS.LuuHanhKiem(hk))
                    {
                        soLuuThanhCong++;
                    }
                    else
                    {
                        soLuuThatBai++;
                    }
                }

                if (soLuuThatBai == 0)
                {
                    MessageBox.Show($"Lưu thành công {soLuuThanhCong} bản ghi hạnh kiểm!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Lưu thành công {soLuuThanhCong} bản ghi.\n" +
                        $"Thất bại {soLuuThatBai} bản ghi.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                LoadDuLieuHanhKiem();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu hạnh kiểm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDuLieuHanhKiem();
            CapNhatThongKe();
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDuLieuHanhKiem();
            CapNhatThongKe();
        }


        private void LoadDuLieuHanhKiem()
        {
            

            try
            {
                tableHanhKiem.Rows.Clear();

                if (cbHocKyNamHoc.SelectedItem == null)
                    return;

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

                // LẤY TRỰC TIẾP TỪ DATABASE - KHÔNG GỌI BUS
                // Chỉ lấy học sinh ĐÃ CÓ xếp loại hạnh kiểm
                var dsHanhKiem = hanhKiemDAO.LayDanhSachHanhKiemBindingList(maHocKy, maLop);

                HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
                string tenHocKy = hocKy != null ? hocKy.TenHocKy : "";

                foreach (var hk in dsHanhKiem)
                {
                    // Lấy thông tin học sinh
                    HocSinhDTO hs = hocSinhDAO.LayHocSinhTheoMa(hk.MaHocSinh);
                    if (hs == null) continue;

                    // Lấy lớp của học sinh
                    int maLopHS = phanLopDAO.LayLopCuaHocSinh(hs.MaHS, maHocKy);
                    string tenLop = "";
                    if (maLopHS > 0)
                    {
                        LopDTO lop = lopDAO.LayLopTheoId(maLopHS);
                        tenLop = lop != null ? lop.TenLop : "";
                    }

                    tableHanhKiem.Rows.Add(
                        hs.MaHS,
                        hs.HoTen,
                        tenLop,
                        tenHocKy,
                        hk.XepLoai,
                        hk.NhanXet
                    );

                    int rowIndex = tableHanhKiem.Rows.Count - 1;
                    ApplyXepLoaiColor(rowIndex, hk.XepLoai);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu hạnh kiểm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ApplyXepLoaiColor(int rowIndex, string xepLoai)
        {
            //switch (xepLoai)
            //{
            //    case "Tốt":
            //        tableHanhKiem.Rows[rowIndex].Cells[4].Style.ForeColor = Color.FromArgb(22, 163, 74);
            //        break;
            //    case "Khá":
            //        tableHanhKiem.Rows[rowIndex].Cells[4].Style.ForeColor = Color.FromArgb(30, 136, 229);
            //        break;
            //    case "Trung Bình":
            //        tableHanhKiem.Rows[rowIndex].Cells[4].Style.ForeColor = Color.FromArgb(219, 39, 119);
            //        break;
            //    case "Yếu":
            //        tableHanhKiem.Rows[rowIndex].Cells[4].Style.ForeColor = Color.FromArgb(220, 38, 38);
            //        break;
            //    default:
            //        tableHanhKiem.Rows[rowIndex].Cells[4].Style.ForeColor = Color.Black;
            //        break;
            //}
            //tableHanhKiem.Rows[rowIndex].Cells[4].Style.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            if (rowIndex < 0 || rowIndex >= tableHanhKiem.Rows.Count) return;

            // Bảo đảm không null và trim
            xepLoai = (xepLoai ?? "").Trim();

            var style = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            switch (xepLoai)
            {
                case "Tốt":
                    style.ForeColor = Color.FromArgb(22, 163, 74);
                    break;
                case "Khá":
                    style.ForeColor = Color.FromArgb(30, 136, 229);
                    break;
                case "Trung Bình":
                    style.ForeColor = Color.FromArgb(219, 39, 119);
                    break;
                case "Yếu":
                    style.ForeColor = Color.FromArgb(220, 38, 38);
                    break;
                default:
                    style.ForeColor = Color.Black;
                    break;
            }

            // Gán nguyên style cho ô (ghi đè những gì có trước)
            tableHanhKiem.Rows[rowIndex].Cells[4].Style = style;

        }

        private void CapNhatThongKe()
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                    return;

                dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                int tongHS = 0;
                int soTot = 0, soKha = 0, soTrungBinh = 0, soYeu = 0, chuaDanhGia = 0;

                // Đếm số học sinh có xếp loại (từ bảng)
                foreach (DataGridViewRow row in tableHanhKiem.Rows)
                {
                    if (row.IsNewRow) continue;
                    tongHS++;

                    string xepLoai = row.Cells[4].Value?.ToString()?.Trim(); // ⭐ THÊM .Trim()

                    if (string.IsNullOrEmpty(xepLoai)) continue; // ⭐ THÊM kiểm tra null

                    // ⭐ SỬ DỤNG StringComparison.OrdinalIgnoreCase để so sánh không phân biệt hoa thường
                    if (xepLoai.Equals("Tốt", StringComparison.OrdinalIgnoreCase))
                        soTot++;
                    else if (xepLoai.Equals("Khá", StringComparison.OrdinalIgnoreCase))
                        soKha++;
                    else if (xepLoai.Equals("Trung Bình", StringComparison.OrdinalIgnoreCase) ||
                             xepLoai.Equals("Trung binh", StringComparison.OrdinalIgnoreCase)) 
                        soTrungBinh++;
                    else if (xepLoai.Equals("Yếu", StringComparison.OrdinalIgnoreCase))
                        soYeu++;
                }

                // Đếm tổng số học sinh trong học kỳ (bao gồm cả chưa xếp loại)
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

                List<HocSinhDTO> dsTatCaHocSinh;
                if (maLop.HasValue)
                {
                    dsTatCaHocSinh = phanLopDAO.LayDanhSachHocSinhTrongLop(maLop.Value, maHocKy);
                }
                else
                {
                    dsTatCaHocSinh = new List<HocSinhDTO>();
                    List<LopDTO> dsLop = lopDAO.GetDanhSachLopTheoHocKy(maHocKy);
                    foreach (var lop in dsLop)
                    {
                        var hsLop = phanLopDAO.LayDanhSachHocSinhTrongLop(lop.MaLop, maHocKy);
                        dsTatCaHocSinh.AddRange(hsLop);
                    }
                    dsTatCaHocSinh = dsTatCaHocSinh.Distinct().ToList();
                }

                int tongTatCaHS = dsTatCaHocSinh.Count;
                chuaDanhGia = tongTatCaHS - tongHS;

                // Cập nhật các card
                statCarHanhKiemTot.lbCardValue.Text = soTot.ToString();
                statCarHanhKiemTot.lbCardNote.Text = tongTatCaHS > 0 ?
                    $"{(soTot * 100.0 / tongTatCaHS):F1}% học sinh" : "0% học sinh";

                statCardHanhKiemKha.lbCardValue.Text = soKha.ToString();
                statCardHanhKiemKha.lbCardNote.Text = tongTatCaHS > 0 ?
                    $"{(soKha * 100.0 / tongTatCaHS):F1}% học sinh" : "0% học sinh";

                statCardHanhKiemTrungBinh.lbCardValue.Text = soTrungBinh.ToString();
                statCardHanhKiemTrungBinh.lbCardNote.Text = tongTatCaHS > 0 ?
                    $"{(soTrungBinh * 100.0 / tongTatCaHS):F1}% học sinh" : "0% học sinh";

                statCardHanhKiemYeu.lbCardValue.Text = soYeu.ToString();
                statCardHanhKiemYeu.lbCardNote.Text = tongTatCaHS > 0 ?
                    $"{(soYeu * 100.0 / tongTatCaHS):F1}% học sinh" : "0% học sinh";

                statCardChuaDanhGiaHanhKiem.lbCardValue.Text = chuaDanhGia > 0 ?
                    $"{chuaDanhGia} học sinh" : "0 học sinh";
                statCardChuaDanhGiaHanhKiem.lbCardNote.Text = tongTatCaHS > 0 ?
                    $"{(chuaDanhGia * 100.0 / tongTatCaHS):F1}% học sinh" : "0% học sinh";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật thống kê: " + ex.Message);
            }
        }

        private void statCarHanhKiemTot_Load(object sender, EventArgs e)
        {

        }

        private void statCardHanhKiemYeu_Load(object sender, EventArgs e)
        {

        }

        private void tableHanhKiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXepHanhKiemTuDong_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show(
                    "Hệ thống sẽ tự động xếp hạnh kiểm cho các học sinh ĐÃ CÓ HỌC LỰC.\n\n" +
                    "Dữ liệu sẽ hiển thị trên bảng và chưa được lưu vào database.\n" +
                    "Bạn cần nhấn nút 'Lưu hạnh kiểm' để lưu vào database.\n\n" +
                    "Bạn có muốn tiếp tục?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

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

                // Lấy danh sách học sinh có học lực
                List<XepLoaiDTO> dsHocLuc = xepLoaiDAO.GetDanhSachXepLoai(maHocKy, maLop);

                HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
                string tenHocKy = hocKy != null ? hocKy.TenHocKy : "";

                int soXepThanhCong = 0;
                int soBoQua = 0;

                // Xóa dữ liệu cũ trên bảng
                tableHanhKiem.Rows.Clear();

                foreach (var xl in dsHocLuc)
                {
                    // Kiểm tra xem đã có hạnh kiểm trong database chưa
                    HanhKiemDTO hkHienTai = hanhKiemDAO.LayHanhKiem(xl.MaHocSinh, maHocKy);

                    // Lấy thông tin học sinh
                    HocSinhDTO hs = hocSinhDAO.LayHocSinhTheoMa(xl.MaHocSinh);
                    if (hs == null) continue;

                    // Lấy lớp của học sinh
                    int maLopHS = phanLopDAO.LayLopCuaHocSinh(hs.MaHS, maHocKy);
                    string tenLop = "";
                    if (maLopHS > 0)
                    {
                        LopDTO lop = lopDAO.LayLopTheoId(maLopHS);
                        tenLop = lop != null ? lop.TenLop : "";
                    }

                    string xepLoai;
                    string nhanXet;

                    // Nếu đã có hạnh kiểm trong database, giữ nguyên
                    if (hkHienTai != null && !string.IsNullOrEmpty(hkHienTai.XepLoai))
                    {
                        xepLoai = hkHienTai.XepLoai;
                        nhanXet = hkHienTai.NhanXet;
                        soBoQua++;
                    }
                    else
                    {
                        // Tính hạnh kiểm tự động cho học sinh chưa có
                        xepLoai = hanhKiemBUS.TinhHanhKiemTuDong(xl.MaHocSinh, maHocKy);
                        nhanXet = hkHienTai?.NhanXet ?? "";

                        if (!string.IsNullOrEmpty(xepLoai))
                        {
                            soXepThanhCong++;
                        }
                        else
                        {
                            // Nếu không tính được hạnh kiểm, bỏ qua
                            continue;
                        }
                    }

                    // Thêm vào bảng (chưa lưu database)
                    tableHanhKiem.Rows.Add(
                        hs.MaHS,
                        hs.HoTen,
                        tenLop,
                        tenHocKy,
                        xepLoai,
                        nhanXet
                    );

                    int rowIndex = tableHanhKiem.Rows.Count - 1;
                    ApplyXepLoaiColor(rowIndex, xepLoai);
                }

                // ⭐ SẮP XẾP THEO MÃ HỌC SINH (cột 0) TỪ THẤP ĐẾN CAO
                tableHanhKiem.Sort(tableHanhKiem.Columns[0], ListSortDirection.Ascending);

                // ⭐ ÁP DỤNG LẠI MÀU SAU KHI SẮP XẾP
                for (int i = 0; i < tableHanhKiem.Rows.Count; i++)
                {
                    if (tableHanhKiem.Rows[i].Cells[4].Value != null)
                    {
                        string xepLoai = tableHanhKiem.Rows[i].Cells[4].Value.ToString();
                        ApplyXepLoaiColor(i, xepLoai);
                    }
                }

                // Cập nhật thống kê
                CapNhatThongKe();

                MessageBox.Show(
                    $"Đã xếp hạnh kiểm tự động:\n" +
                    $"- Mới xếp: {soXepThanhCong} học sinh\n" +
                    $"- Giữ nguyên (đã có): {soBoQua} học sinh\n\n" +
                    $"Dữ liệu hiển thị trên bảng chưa được lưu.\n" +
                    $"Nhấn nút 'Lưu hạnh kiểm' để lưu vào database.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xếp hạnh kiểm tự động: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
