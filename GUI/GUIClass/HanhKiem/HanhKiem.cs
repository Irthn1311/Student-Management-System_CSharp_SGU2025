using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Student_Management_System_CSharp_SGU2025.DAO.HanhKiemDAO;


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

        private int pageSize = 50; // Số học sinh trên mỗi trang
        private int currentPage = 1; // Trang hiện tại
        private int totalPages = 0; // Tổng số trang
        private List<HanhKiemDTO> fullListHanhKiem = new List<HanhKiemDTO>(); // Danh sách đầy đủ
        private string searchKeyword = ""; // Từ khóa tìm kiếm
        private Dictionary<int, HanhKiemDisplayDTO> _cachedDisplayInfo = new Dictionary<int, HanhKiemDisplayDTO>();
        private string _cachedTenHocKy = "";
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
            // ⭐ GẮN SỰ KIỆN TRƯỚC KHI LOAD COMBOBOX
            cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
            cbLop.SelectedIndexChanged += cbLop_SelectedIndexChanged;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // Load ComboBox
            LoadHocKyComboBox();
            LoadLopComboBox();

            PermissionHelper.ApplyPermissionHanhKiem(btnXepHanhKiemTuDong, btnLuuHanhKiem);
        
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
                // ⭐ TẠM THỜI GỠ SỰ KIỆN ĐỂ TRÁNH KÍCH HOẠT KHI ĐANG LOAD
                cbHocKyNamHoc.SelectedIndexChanged -= cbHocKyNamHoc_SelectedIndexChanged;

                // Lấy danh sách học kỳ từ database (đã sắp xếp theo thứ tự mới nhất)
                List<HocKyDTO> dsHocKy = hocKyDAO.GetAllHocKy();

                // Xóa dữ liệu cũ trong combobox
                cbHocKyNamHoc.Items.Clear();
                cbHocKyNamHoc.DisplayMember = "Text";
                cbHocKyNamHoc.ValueMember = "Value";

                // Tạo danh sách các item để thêm vào combobox
                var itemsToAdd = new List<dynamic>();

                // Tìm học kỳ mới nhất có dữ liệu hạnh kiểm
                int indexHocKyMoiNhatCoDuLieu = -1;

                for (int i = 0; i < dsHocKy.Count; i++)
                {
                    HocKyDTO hocKy = dsHocKy[i];
                    string displayText = $"{hocKy.TenHocKy} - {hocKy.MaNamHoc}";

                    var item = new { Text = displayText, Value = hocKy.MaHocKy };
                    itemsToAdd.Add(item);

                    // Kiểm tra học kỳ này có dữ liệu hạnh kiểm không
                    if (indexHocKyMoiNhatCoDuLieu == -1 && hanhKiemDAO.KiemTraHocKyCoDuLieuHanhKiem(hocKy.MaHocKy))
                    {
                        indexHocKyMoiNhatCoDuLieu = i;
                        Console.WriteLine($"[LoadHocKyComboBox] Tìm thấy học kỳ có dữ liệu: {displayText} (index={i})");
                    }
                }

                // Thêm tất cả items vào combobox
                foreach (var item in itemsToAdd)
                {
                    cbHocKyNamHoc.Items.Add(item);
                }

                // ⭐ GẮN LẠI SỰ KIỆN TRƯỚC KHI SET SELECTEDINDEX
                cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;

                // Chọn học kỳ mới nhất có dữ liệu hạnh kiểm, nếu không có thì chọn học kỳ đầu tiên
                if (cbHocKyNamHoc.Items.Count > 0)
                {
                    if (indexHocKyMoiNhatCoDuLieu >= 0)
                    {
                        cbHocKyNamHoc.SelectedIndex = indexHocKyMoiNhatCoDuLieu;
                        Console.WriteLine($"[LoadHocKyComboBox] Đã chọn học kỳ có dữ liệu tại index: {indexHocKyMoiNhatCoDuLieu}");
                    }
                    else
                    {
                        cbHocKyNamHoc.SelectedIndex = 0; // Chọn học kỳ đầu tiên (mới nhất)
                        Console.WriteLine("[LoadHocKyComboBox] Không có học kỳ nào có dữ liệu, chọn học kỳ đầu tiên");
                    }
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
            if (!PermissionHelper.CheckUpdatePermission(PermissionHelper.QLHANHKIEM, "Quản lý hạnh kiểm"))
                return;
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
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    tableHanhKiem.Rows.Clear();
                    fullListHanhKiem.Clear();
                    currentPage = 1;
                    totalPages = 0;
                    UpdatePaginationUI();
                    return;
                }

                // ⭐ KIỂM TRA KIỂU DỮ LIỆU AN TOÀN
                int maHocKy;
                try
                {
                    dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                    maHocKy = selectedHocKy.Value;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    Console.WriteLine("[LoadDuLieuHanhKiem] SelectedItem không có thuộc tính Value");
                    return;
                }

                int? maLop = null;
                if (cbLop.SelectedItem != null)
                {
                    try
                    {
                        dynamic selectedLop = cbLop.SelectedItem;
                        int lopValue = selectedLop.Value;
                        if (lopValue > 0)
                        {
                            maLop = lopValue;
                        }
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        Console.WriteLine("[LoadDuLieuHanhKiem] cbLop SelectedItem không có thuộc tính Value");
                    }
                }

                // ✅ GỌI 1 LẦN DUY NHẤT - ĐÃ CÓ ĐẦY ĐỦ THÔNG TIN
                var dsHanhKiemDisplay = hanhKiemDAO.LayDanhSachHanhKiemDisplay(maHocKy, maLop);

                HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
                string tenHocKy = hocKy != null ? hocKy.TenHocKy : "";

                fullListHanhKiem.Clear();

                // ✅ KHÔNG CẦN QUERY THÊM - CHỈ CHUYỂN ĐỔI SANG DTO
                foreach (var hkDisplay in dsHanhKiemDisplay)
                {
                    fullListHanhKiem.Add(new HanhKiemDTO
                    {
                        MaHocSinh = hkDisplay.MaHocSinh,
                        MaHocKy = hkDisplay.MaHocKy,
                        XepLoai = hkDisplay.XepLoai,
                        NhanXet = hkDisplay.NhanXet
                    });
                }

                // Lưu thêm thông tin hiển thị (tên, lớp) vào dictionary để dùng sau
                _cachedDisplayInfo = dsHanhKiemDisplay.ToDictionary(x => x.MaHocSinh);
                _cachedTenHocKy = tenHocKy;

                currentPage = 1;
                HienThiDanhSachHanhKiem();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu hạnh kiểm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị danh sách hạnh kiểm với phân trang
        /// </summary>
        private void HienThiDanhSachHanhKiem()
        {
            try
            {
                List<HanhKiemDTO> filteredList = fullListHanhKiem;

                if (!string.IsNullOrWhiteSpace(searchKeyword))
                {
                    string keyword = searchKeyword.Trim().ToLower();
                    filteredList = fullListHanhKiem.FindAll(x =>
                    {
                        // ✅ DÙNG CACHE THAY VÌ QUERY
                        if (!_cachedDisplayInfo.TryGetValue(x.MaHocSinh, out var info))
                            return false;

                        // Tìm kiếm theo: Mã HS, Họ tên, Tên lớp, Tên học kỳ, Xếp loại, Nhận xét
                        return x.MaHocSinh.ToString().Contains(keyword) ||
                               (info.HoTen != null && info.HoTen.ToLower().Contains(keyword)) ||
                               (info.TenLop != null && info.TenLop.ToLower().Contains(keyword)) ||
                               (_cachedTenHocKy != null && _cachedTenHocKy.ToLower().Contains(keyword)) ||
                               (x.XepLoai != null && x.XepLoai.ToLower().Contains(keyword)) ||
                               (x.NhanXet != null && x.NhanXet.ToLower().Contains(keyword));
                    });
                }

                totalPages = (int)Math.Ceiling((double)filteredList.Count / pageSize);

                if (currentPage < 1) currentPage = 1;
                if (currentPage > totalPages && totalPages > 0) currentPage = totalPages;

                var pagedList = filteredList
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                tableHanhKiem.Rows.Clear();
                tableHanhKiem.SuspendLayout();

                // ✅ KHÔNG CẦN QUERY TRONG VÒNG LẶP NỮA!
                foreach (var hk in pagedList)
                {
                    if (!_cachedDisplayInfo.TryGetValue(hk.MaHocSinh, out var info))
                        continue;

                    tableHanhKiem.Rows.Add(
                        info.MaHocSinh,
                        info.HoTen,
                        info.TenLop,
                        _cachedTenHocKy,
                        hk.XepLoai,
                        hk.NhanXet
                    );

                    int rowIndex = tableHanhKiem.Rows.Count - 1;
                    ApplyXepLoaiColor(rowIndex, hk.XepLoai);
                }

                tableHanhKiem.ResumeLayout();
                UpdatePaginationUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị danh sách hạnh kiểm: {ex.Message}", "Lỗi",
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

                // ⭐ KIỂM TRA KIỂU DỮ LIỆU AN TOÀN
                int maHocKy;
                try
                {
                    dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                    maHocKy = selectedHocKy.Value;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
                {
                    Console.WriteLine($"[CapNhatThongKe] Lỗi: {ex.Message}");
                    return;
                }

                int tongHS = 0;
                int soTot = 0, soKha = 0, soTrungBinh = 0, soYeu = 0, chuaDanhGia = 0;

                // Đếm số học sinh có xếp loại (từ bảng)
                foreach (DataGridViewRow row in tableHanhKiem.Rows)
                {
                    if (row.IsNewRow) continue;
                    tongHS++;

                    string xepLoai = row.Cells[4].Value?.ToString()?.Trim();

                    if (string.IsNullOrEmpty(xepLoai)) continue;

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
                    try
                    {
                        dynamic selectedLop = cbLop.SelectedItem;
                        int lopValue = selectedLop.Value;
                        if (lopValue > 0)
                        {
                            maLop = lopValue;
                        }
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        Console.WriteLine("[CapNhatThongKe] cbLop SelectedItem không có thuộc tính Value");
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
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLHANHKIEM, "Quản lý hạnh kiểm"))
                return;
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

        /// <summary>
        /// Cập nhật UI phân trang (hiển thị số trang, enable/disable nút)
        /// </summary>
        private void UpdatePaginationUI()
        {
            if (totalPages == 0)
            {
                lblTrangHienTai.Text = "0/0";
                btnTrangTruoc.Enabled = false;
                btnTrangSau.Enabled = false;
            }
            else
            {
                lblTrangHienTai.Text = $"{currentPage}/{totalPages}";
                btnTrangTruoc.Enabled = currentPage > 1;
                btnTrangSau.Enabled = currentPage < totalPages;
            }
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                HienThiDanhSachHanhKiem();
            }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                HienThiDanhSachHanhKiem();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Lưu từ khóa tìm kiếm
            searchKeyword = txtSearch.Text.Trim();

            // Reset về trang đầu tiên khi tìm kiếm
            currentPage = 1;

            // Hiển thị lại danh sách với từ khóa mới
            HienThiDanhSachHanhKiem();
        }
    }
}
