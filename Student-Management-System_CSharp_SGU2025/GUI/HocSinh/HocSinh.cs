using OfficeOpenXml; // Thư viện EPPlus
using OfficeOpenXml.Style; // Cần cho định dạng (tô màu, in đậm)
using Student_Management_System_CSharp_SGU2025.BUS; 
using Student_Management_System_CSharp_SGU2025.DAO; // ✅ Thêm để sử dụng NguoiDungDAO (nếu cần)
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO; // Cần cho FileInfo
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.HocSinh
{
    public partial class HocSinh : UserControl
    {

        private bool isShowingHocSinh = true;
        private bool isLoadingData = false;

        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        private LopHocBUS lopHocBUS;
        private PhanLopBLL phanLopBLL;
        private HocKyBUS hocKyBUS;
        private NguoiDungBLL nguoiDungBLL;

        // ✅ Chuyển sang BindingList để tự động cập nhật DataGridView
        private BindingList<HocSinhDTO> bindingListHocSinh;
        private BindingList<PhuHuynhDTO> bindingListPhuHuynh;

        // Danh sách tổng (để lọc)
        private List<HocSinhDTO> danhSachHocSinhFull;
        private List<PhuHuynhDTO> danhSachPhuHuynhFull;
        private List<(int hocSinh, int phuHuynh, string moiQuanHe)> danhSachMoiQuanHe;

        // ✅ PHÂN TRANG - Biến quản lý
        private int currentPageHocSinh = 1; // Trang hiện tại
        private int pageSizeHocSinh = 50; // Số dòng mỗi trang
        private List<HocSinhDTO> danhSachHocSinhFiltered; // Danh sách sau khi tìm kiếm/lọc

        // ✅ PHÂN TRANG PHỤ HUYNH - Biến quản lý
        private int currentPagePhuHuynh = 1; // Trang hiện tại
        private int pageSizePhuHuynh = 50; // Số dòng mỗi trang
        private List<PhuHuynhDTO> danhSachPhuHuynhFiltered; // Danh sách sau khi tìm kiếm/lọc

        // ✅ LỌC THEO LỚP - Biến quản lý
        private int? selectedMaLop = null; // Mã lớp được chọn (null = tất cả lớp)
        private int maHocKyHienTai = 0; // Học kỳ hiện tại để lấy lớp


        public HocSinh()
        {
            InitializeComponent();

            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            lopHocBUS = new LopHocBUS();
            phanLopBLL = new PhanLopBLL();
            hocKyBUS = new HocKyBUS();
            nguoiDungBLL = new NguoiDungBLL();

            // ✅ Khởi tạo BindingList
            bindingListHocSinh = new BindingList<HocSinhDTO>();
            bindingListPhuHuynh = new BindingList<PhuHuynhDTO>();

            danhSachHocSinhFull = new List<HocSinhDTO>();
            danhSachPhuHuynhFull = new List<PhuHuynhDTO>();
            danhSachMoiQuanHe = new List<(int hocSinh, int phuHuynh, string moiQuanHe)>();
            danhSachHocSinhFiltered = new List<HocSinhDTO>(); // ✅ Khởi tạo danh sách filtered
            danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>(); // ✅ Khởi tạo danh sách filtered phụ huynh

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void statCardTongHocSinh_Load(object sender, EventArgs e)
        {

        }

        private void HocSinh_Load_1(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckAccessPermission(PermissionHelper.QLHOCSINH, "Quản lý học sinh"))
            {
                // Nếu không có quyền, disable toàn bộ form
                this.Enabled = false;
                return;
            }
            isLoadingData = true; // Bắt đầu load dữ liệu

            // --- Thiết lập giao diện ban đầu ---
            SetInitialView();

            // --- Cấu hình các bảng ---
            SetupTableHocSinh();
            SetupTablePhuHuynh();
            SetupTableMoiQuanHe();

            // --- Nạp dữ liệu mẫu ---
            LoadDanhSachLop(); // ✅ Load danh sách lớp vào combobox
            LoadSampleDataHocSinh(); 
            LoadSampleDataPhuHuynh();
            LoadSampleDataMoiQuanHe();

            // --- Cấu hình Header và Thẻ Thống kê ---
            SetupHeaderAndStats();

            PermissionHelper.ApplyPermissionHocSinh(
              btnThemHocSinh,
              btnThemPhuHuynh,
              tableHocSinh,
              tablePhuHuynh
  );

            isLoadingData = false; // Kết thúc load dữ liệu

            // ✅ Force update label sau khi load xong (fix bug hiển thị 500 lần đầu)
            ForceUpdatePaginationLabel();
        }

        private void SetInitialView()
        {
            isShowingHocSinh = true;
            if (tabControlMain != null)
            {
                tabControlMain.SelectedIndex = 0; // Chọn tab Học Sinh
            }
            UpdateView();
        }

        private void UpdateView()
        {
            if (isShowingHocSinh)
            {
                headerQuanLiHocSinh.lbHeader.Text = "Hồ sơ Học sinh";
                headerQuanLiHocSinh.lbGhiChu.Text = "Trang chủ / Hồ sơ học sinh";

                // ✅ Đổi placeholder TextBox
                if (txtTimKiem != null)
                {
                    txtTimKiem.PlaceholderText = "Tìm học sinh ...";
                }
            }
            else
            {
                headerQuanLiHocSinh.lbHeader.Text = "Thông tin Phụ huynh";
                headerQuanLiHocSinh.lbGhiChu.Text = "Trang chủ / Phụ huynh";

                // ✅ Đổi placeholder TextBox
                if (txtTimKiem != null)
                {
                    txtTimKiem.PlaceholderText = "Tìm phụ huynh ...";
                }
            }

            // Reset bảng Mối Quan Hệ về hiển thị tất cả khi chuyển view
            LoadSampleDataMoiQuanHe();
        }

        // ✅ Event handler cho TabControl switching
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedIndex == 0) // Tab Học Sinh
            {
                isShowingHocSinh = true;
            }
            else if (tabControlMain.SelectedIndex == 1) // Tab Phụ Huynh
            {
                isShowingHocSinh = false;
                // Cập nhật phân trang cho Phụ Huynh khi chuyển tab
                int totalPages = (int)Math.Ceiling((double)danhSachPhuHuynhFiltered.Count / pageSizePhuHuynh);
                UpdatePaginationLabelPhuHuynh(totalPages, danhSachPhuHuynhFiltered.Count);
                UpdatePaginationButtonsPhuHuynh(totalPages);
            }
            
            UpdateView();
            ForceUpdatePaginationLabel();
        }

        // Cấu hình Header và Thẻ Thống kê (Tách ra từ Load cũ)
        private void SetupHeaderAndStats()
        {
            headerQuanLiHocSinh.lbTenDangNhap.Text = "Nguyễn Văn A";
            headerQuanLiHocSinh.lbVaiTro.Text = "Giáo vụ";

            int tongHocSinh = hocSinhBLL.GetTotalHocSinh();
            int tongHocSinhNam = hocSinhBLL.GetTotalHocSinhNam();
            int tongHocSinhNu = hocSinhBLL.GetTotalHocSinhNu();
            int tongHocSinhDangHoc = hocSinhBLL.GetTotalHocSinhDangHoc();
            int tongHocSinhNghiHoc = hocSinhBLL.GetTotalHocSinhNghiHoc();
            int tongHocSinhThoiHoc = hocSinhBLL.GetTotalHocSinhThoiHoc();
            int tongHocSinhBaoLuu = hocSinhBLL.GetTotalHocSinhBaoLuu();
            int tongHocSinhChuyenTruong = hocSinhBLL.GetTotalHocSinhChuyenTruong();

            // --- Tính toán phần trăm ---
            double phanTramNam = 0;
            double phanTramNu = 0;

            if (tongHocSinh > 0) // Tránh lỗi chia cho 0
            {
                // Quan trọng: Phải ép kiểu (double) để phép chia ra số thập phân
                phanTramNam = Math.Round(((double)tongHocSinhNam / tongHocSinh) * 100, 1); // Làm tròn 1 chữ số thập phân
                phanTramNu = 100.0 - phanTramNam;
            }

            // --- Cập nhật các thẻ thống kê ---
            statCardTongHocSinh.lbCardTitle.Text = "Tổng học sinh";
            statCardTongHocSinh.lbCardValue.Text = tongHocSinh.ToString("N0"); // Định dạng có dấu phẩy ngăn cách
            
            // ✅ Cải thiện: Làm rõ thông báo "TB: 42 HS..."
            int tongLop = lopHocBUS.GetTotalLopHoc();
            if (tongLop > 0)
            {
                double siSoTB = Math.Round((double)tongHocSinh / tongLop, 1);
                statCardTongHocSinh.lbCardNote.Text = $"Trung bình: {siSoTB} HS/lớp | Tổng lớp: {tongLop}";
            }
            else
            {
                statCardTongHocSinh.lbCardNote.Text = "Chưa có lớp học";
            }

            statCardNam.lbCardTitle.Text = "Nam";
            statCardNam.lbCardValue.Text = tongHocSinhNam.ToString("N0");
            statCardNam.lbCardNote.Text = $"{phanTramNam}%";

            statCardNu.lbCardTitle.Text = "Nữ";
            statCardNu.lbCardValue.Text = tongHocSinhNu.ToString("N0");
            statCardNu.lbCardNote.Text = $"{phanTramNu}%";

            statCardDangHoc.lbCardTitle.Text = "Đang học";
            statCardDangHoc.lbCardValue.Text = tongHocSinhDangHoc.ToString("N0");

            // Hiển thị chi tiết từng trạng thái (xuống dòng cho Bảo lưu và Đang học(CT))
            List<string> trangThaiListDong1 = new List<string>();
            List<string> trangThaiListDong2 = new List<string>();
            if (tongHocSinhNghiHoc > 0) trangThaiListDong1.Add($"{tongHocSinhNghiHoc} Nghỉ học");
            if (tongHocSinhThoiHoc > 0) trangThaiListDong1.Add($"{tongHocSinhThoiHoc} Thôi học");
            if (tongHocSinhBaoLuu > 0) trangThaiListDong2.Add($"{tongHocSinhBaoLuu} Bảo lưu");
            if (tongHocSinhChuyenTruong > 0) trangThaiListDong2.Add($"{tongHocSinhChuyenTruong} Đang học(CT)");
            
            string dong1 = trangThaiListDong1.Count > 0 ? string.Join(", ", trangThaiListDong1) : "";
            string dong2 = trangThaiListDong2.Count > 0 ? string.Join(", ", trangThaiListDong2) : "";
            
            if (!string.IsNullOrEmpty(dong1) && !string.IsNullOrEmpty(dong2))
            {
                statCardDangHoc.lbCardNote.Text = dong1 + "\n" + dong2;
            }
            else if (!string.IsNullOrEmpty(dong1))
            {
                statCardDangHoc.lbCardNote.Text = dong1;
            }
            else if (!string.IsNullOrEmpty(dong2))
            {
                statCardDangHoc.lbCardNote.Text = dong2;
            }
            else
            {
                statCardDangHoc.lbCardNote.Text = "0 Nghỉ học";
            }

            // ✅ Cải thiện: Đồng bộ màu sắc - Xanh lá cho Tổng học sinh và Đang học
            // Màu xanh lá (#16A34A) cho Tổng học sinh và Đang học (trạng thái tích cực)
            Color mauXanhLa = Color.FromArgb(22, 163, 74); // #16A34A
            statCardTongHocSinh.lbCardValue.ForeColor = mauXanhLa;
            statCardTongHocSinh.lbCardNote.ForeColor = Color.FromArgb(100, 22, 163, 74); // Màu nhạt hơn cho note
            
            // Màu xanh dương cho Nam
            statCardNam.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardNam.lbCardNote.ForeColor = Color.FromArgb(100, 30, 136, 229);
            
            // Màu hồng cho Nữ
            statCardNu.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardNu.lbCardNote.ForeColor = Color.FromArgb(100, 219, 39, 119);
            
            // ✅ Đồng bộ: Đang học cũng dùng màu xanh lá
            statCardDangHoc.lbCardValue.ForeColor = mauXanhLa;
            
            // ✅ Cải thiện: Màu cảnh báo cho "Nghỉ học" - Vàng nếu > 0, Đỏ nếu > 5% tổng số
            if (tongHocSinhNghiHoc > 0)
            {
                double tyLeNghiHoc = tongHocSinh > 0 ? (double)tongHocSinhNghiHoc / tongHocSinh * 100 : 0;
                if (tyLeNghiHoc > 5) // Nếu > 5% thì dùng màu đỏ cảnh báo
                {
                    statCardDangHoc.lbCardNote.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                }
                else
                {
                    statCardDangHoc.lbCardNote.ForeColor = Color.FromArgb(234, 179, 8); // Vàng
                }
            }
            else
            {
                statCardDangHoc.lbCardNote.ForeColor = mauXanhLa; // Xanh lá nếu không có nghỉ học
            }


        }

        #region Bảng Học Sinh

        private void SetupTableHocSinh()
        {
            // --- Xóa cột cũ và cấu hình chung ---
            tableHocSinh.Columns.Clear();
            ApplyBaseTableStyle(tableHocSinh); // Áp dụng style chung

            // --- Thêm cột mới (THÊM CỘT LỚP) ---
            tableHocSinh.Columns.Add("MaHS", "Mã HS");
            tableHocSinh.Columns.Add("HoTen", "Họ và tên");
            tableHocSinh.Columns.Add("Lop", "Lớp"); // ✅ Thêm cột Lớp
            tableHocSinh.Columns.Add("NgaySinh", "Ngày sinh");
            tableHocSinh.Columns.Add("GioiTinh", "Giới tính");
            tableHocSinh.Columns.Add("SDTHS", "SĐT"); // ✅ Thêm cột SĐT
            tableHocSinh.Columns.Add("Email", "Email"); // ✅ Thêm cột Email
            tableHocSinh.Columns.Add("TrangThai", "Trạng thái");
            tableHocSinh.Columns.Add("ThaoTacHS", "Thao tác"); // <-- Cột thao tác mới

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tableHocSinh);
            tableHocSinh.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableHocSinh.Columns["Lop"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tableHocSinh.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- Tùy chỉnh kích thước ---
            tableHocSinh.Columns["MaHS"].FillWeight = 7; tableHocSinh.Columns["MaHS"].MinimumWidth = 50;
            tableHocSinh.Columns["HoTen"].FillWeight = 22; tableHocSinh.Columns["HoTen"].MinimumWidth = 150;
            tableHocSinh.Columns["Lop"].FillWeight = 10; tableHocSinh.Columns["Lop"].MinimumWidth = 80; // ✅ Cột Lớp
            tableHocSinh.Columns["NgaySinh"].FillWeight = 10; tableHocSinh.Columns["NgaySinh"].MinimumWidth = 100;
            tableHocSinh.Columns["GioiTinh"].FillWeight = 9; tableHocSinh.Columns["GioiTinh"].MinimumWidth = 70;
            tableHocSinh.Columns["SDTHS"].FillWeight = 11; tableHocSinh.Columns["SDTHS"].MinimumWidth = 100;
            tableHocSinh.Columns["Email"].FillWeight = 16; tableHocSinh.Columns["Email"].MinimumWidth = 120;
            tableHocSinh.Columns["TrangThai"].FillWeight = 11; tableHocSinh.Columns["TrangThai"].MinimumWidth = 90;
            tableHocSinh.Columns["ThaoTacHS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableHocSinh.Columns["ThaoTacHS"].Width = 130; // ✅ Tăng độ rộng để chứa 3 icon (Xem, Sửa, Xóa)

            // --- Gắn sự kiện ---
            tableHocSinh.CellFormatting -= tableHocSinh_CellFormatting; // Gỡ sự kiện cũ (nếu có)
            tableHocSinh.CellFormatting += tableHocSinh_CellFormatting;
            tableHocSinh.CellPainting -= tableHocSinh_CellPainting; // Gỡ sự kiện cũ (nếu có)
            tableHocSinh.CellPainting += tableHocSinh_CellPainting;
            tableHocSinh.CellClick -= tableHocSinh_CellClick; // Gỡ sự kiện cũ (nếu có)
            tableHocSinh.CellClick += tableHocSinh_CellClick;
            tableHocSinh.SelectionChanged -= tableHocSinh_SelectionChanged; // Gỡ sự kiện cũ (nếu có)
            tableHocSinh.SelectionChanged += tableHocSinh_SelectionChanged;
        }

        private void LoadSampleDataHocSinh()
        {
            tableHocSinh.Rows.Clear();

            // ✅ Load tất cả từ DB
            danhSachHocSinhFull = hocSinhBLL.GetAllHocSinh();

            // ✅ Lấy học kỳ hiện tại để lấy lớp của học sinh
            try
            {
                List<HocKyDTO> dsHocKy = hocKyBUS.DocDSHocKy();
                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    // Tìm học kỳ đang diễn ra
                    var hocKyDangDienRa = dsHocKy.FirstOrDefault(hk => hk.TrangThai == "Đang diễn ra");
                    if (hocKyDangDienRa != null)
                    {
                        maHocKyHienTai = hocKyDangDienRa.MaHocKy;
                    }
                    else
                    {
                        // Nếu không có học kỳ đang diễn ra, lấy học kỳ mới nhất
                        var hocKyMoiNhat = dsHocKy.OrderByDescending(hk => hk.NgayBD).FirstOrDefault();
                        if (hocKyMoiNhat != null)
                        {
                            maHocKyHienTai = hocKyMoiNhat.MaHocKy;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy học kỳ hiện tại: {ex.Message}");
            }

            // ✅ Áp dụng lọc theo lớp nếu có
            ApplyLopFilter();

            // Debug: Kiểm tra số lượng
            Console.WriteLine($"[DEBUG] LoadSampleDataHocSinh: Full={danhSachHocSinhFull.Count}, Filtered={danhSachHocSinhFiltered.Count}");

            currentPageHocSinh = 1; // Reset về trang 1
            LoadPagedDataHocSinh(); // ✅ Load trang đầu tiên
        }

        // ✅ HÀM MỚI: Load dữ liệu theo trang
        private void LoadPagedDataHocSinh()
        {
            try
            {
                tableHocSinh.Rows.Clear();
                bindingListHocSinh.Clear();

                // Tính toán phân trang
                int totalRecords = danhSachHocSinhFiltered.Count;
                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizeHocSinh);

                // Debug: Kiểm tra số lượng
                Console.WriteLine($"[DEBUG] LoadPagedDataHocSinh: totalRecords={totalRecords}, totalPages={totalPages}, currentPage={currentPageHocSinh}");

                // Đảm bảo currentPage hợp lệ
                if (currentPageHocSinh < 1) currentPageHocSinh = 1;
                if (currentPageHocSinh > totalPages && totalPages > 0) currentPageHocSinh = totalPages;

                // Lấy dữ liệu của trang hiện tại
                var pagedData = danhSachHocSinhFiltered
                    .Skip((currentPageHocSinh - 1) * pageSizeHocSinh)
                    .Take(pageSizeHocSinh)
                    .ToList();

                // ✅ Sử dụng biến class-level maHocKyHienTai đã được lấy ở LoadSampleDataHocSinh

                // Thêm vào bảng
                foreach (HocSinhDTO hs in pagedData)
                {
                    bindingListHocSinh.Add(hs);
                    
                    // ✅ Lấy tên lớp của học sinh
                    string tenLop = "Chưa phân lớp";
                    if (maHocKyHienTai > 0)
                    {
                        try
                        {
                            int maLop = phanLopBLL.GetLopByHocSinh(hs.MaHS, maHocKyHienTai);
                            if (maLop > 0)
                            {
                                var lop = lopHocBUS.LayLopTheoId(maLop);
                                if (lop != null)
                                {
                                    tenLop = lop.tenLop;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi khi lấy lớp của học sinh {hs.MaHS}: {ex.Message}");
                        }
                    }
                    
                    tableHocSinh.Rows.Add(
                        hs.MaHS,
                        hs.HoTen,
                        tenLop, // ✅ Thêm cột Lớp
                        hs.NgaySinh.ToString("dd/MM/yyyy"),
                        hs.GioiTinh,
                        hs.SdtHS ?? "", // ✅ Hiển thị SĐT
                        hs.Email ?? "", // ✅ Hiển thị Email
                        hs.TrangThai,
                        ""
                    );
                }

                // ✅ Cập nhật label phân trang (tìm control theo tên)
                UpdatePaginationLabel(totalPages, totalRecords);

                // ✅ Enable/Disable nút
                UpdatePaginationButtons(totalPages);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu học sinh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Cập nhật label hiển thị trang
        private void UpdatePaginationLabel(int totalPages, int totalRecords)
        {
            // ✅ Xác định đang ở view nào và lấy current page tương ứng
            int currentPage = isShowingHocSinh ? currentPageHocSinh : currentPagePhuHuynh;
            string entityName = isShowingHocSinh ? "học sinh" : "phụ huynh";

            // Tìm label theo tên (giả sử bạn đặt tên là lblTrangHienTai hoặc tương tự)
            // Nếu không tìm thấy, tạm thời set text của control có chứa "Trang"
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label && (ctrl.Name.Contains("Trang") || ctrl.Name.Contains("lblPaging")))
                {
                    if (totalPages == 0)
                        ctrl.Text = $"Trang 0/0 (0 {entityName})";
                    else
                        ctrl.Text = $"Trang {currentPage}/{totalPages} ({totalRecords} {entityName})";
                    return;
                }
            }

            // Fallback: Tìm trong Panel hoặc GroupBox nếu label nằm trong đó
            FindAndUpdateLabel(this, totalPages, totalRecords, currentPage, entityName);
        }

        // ✅ Hàm đệ quy tìm label trong container
        private void FindAndUpdateLabel(Control parent, int totalPages, int totalRecords, int currentPage, string entityName)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label && (ctrl.Name.ToLower().Contains("trang") || ctrl.Text.Contains("Trang")))
                {
                    if (totalPages == 0)
                        ctrl.Text = $"Trang 0/0 (0 {entityName})";
                    else
                        ctrl.Text = $"Trang {currentPage}/{totalPages} ({totalRecords} {entityName})";
                    return;
                }

                // Tìm trong container con
                if (ctrl.HasChildren)
                {
                    FindAndUpdateLabel(ctrl, totalPages, totalRecords, currentPage, entityName);
                }
            }
        }

        // ✅ Enable/Disable nút phân trang
        private void UpdatePaginationButtons(int totalPages)
        {
            // ✅ Xác định đang ở view nào và lấy current page tương ứng
            int currentPage = isShowingHocSinh ? currentPageHocSinh : currentPagePhuHuynh;

            // Tìm nút Trang Trước (tên có thể là btnTrangTruoc, btnPrevious, etc.)
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button || ctrl.Name.Contains("Button"))
                {
                    if (ctrl.Name.ToLower().Contains("truoc") || ctrl.Name.ToLower().Contains("prev") || ctrl.Text.Contains("◄"))
                    {
                        ctrl.Enabled = (currentPage > 1);
                    }
                    else if (ctrl.Name.ToLower().Contains("sau") || ctrl.Name.ToLower().Contains("next") || ctrl.Text.Contains("►"))
                    {
                        ctrl.Enabled = (currentPage < totalPages);
                    }
                }
            }

            // Tìm trong Panel hoặc GroupBox
            FindAndUpdateButtons(this, totalPages, currentPage);
        }

        // ✅ Hàm đệ quy tìm button trong container
        private void FindAndUpdateButtons(Control parent, int totalPages, int currentPage)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button || ctrl.GetType().Name.Contains("Button"))
                {
                    string ctrlNameLower = ctrl.Name.ToLower();
                    string ctrlTextLower = ctrl.Text.ToLower();

                    if (ctrlNameLower.Contains("truoc") || ctrlNameLower.Contains("prev") ||
                        ctrlTextLower.Contains("◄") || ctrlTextLower.Contains("trước"))
                    {
                        ctrl.Enabled = (currentPage > 1);
                    }
                    else if (ctrlNameLower.Contains("sau") || ctrlNameLower.Contains("next") ||
                             ctrlTextLower.Contains("►") || ctrlTextLower.Contains("sau"))
                    {
                        ctrl.Enabled = (currentPage < totalPages);
                    }
                }

                if (ctrl.HasChildren)
                {
                    FindAndUpdateButtons(ctrl, totalPages, currentPage);
                }
            }
        }

        // ✅ Force update label trực tiếp bằng tên (fix bug lần đầu load)
        private void ForceUpdatePaginationLabel()
        {
            try
            {
                // Tính toán thông tin hiện tại
                int totalRecords = isShowingHocSinh ? danhSachHocSinhFiltered.Count : danhSachPhuHuynhFiltered.Count;
                int pageSize = isShowingHocSinh ? pageSizeHocSinh : pageSizePhuHuynh;
                int currentPage = isShowingHocSinh ? currentPageHocSinh : currentPagePhuHuynh;
                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                string entityName = isShowingHocSinh ? "học sinh" : "phụ huynh";

                // Update label bằng tên trực tiếp
                if (lblTrangHienTai != null)
                {
                    lblTrangHienTai.Text = $"Trang {currentPage}/{totalPages} ({totalRecords} {entityName})";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] ForceUpdatePaginationLabel: {ex.Message}");
            }
        }

        // ✅ Vẽ icon cho bảng Học Sinh (Xem, Sửa, Xóa)
        private void tableHocSinh_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableHocSinh.Columns["ThaoTacHS"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                // ✅ Lấy permission từ Tag
                dynamic permissions = tableHocSinh.Tag;
                bool canUpdate = permissions?.CanUpdate ?? true;
                bool canDelete = permissions?.CanDelete ?? true;

                // ✅ Icon Xem (luôn hiển thị - không cần quyền)
                Image viewIcon = CreateViewIcon();
                Image editIcon = Properties.Resources.repair;
                Image deleteIcon = Properties.Resources.bin;

                int iconSize = 18;
                int spacing = 12;
                int totalWidth = iconSize * 3 + spacing * 2; // 3 icon với 2 khoảng cách
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle viewRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle editRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + (iconSize + spacing) * 2, y, iconSize, iconSize);

                // ✅ Vẽ icon Xem (luôn hiển thị)
                e.Graphics.DrawImage(viewIcon, viewRect);

                // ✅ Vẽ icon Sửa với độ mờ nếu không có quyền
                if (canUpdate)
                {
                    e.Graphics.DrawImage(editIcon, editRect);
                }
                else
                {
                    var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
                        new float[][] {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                        });
                    using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                    {
                        attributes.SetColorMatrix(grayScaleMatrix);
                        e.Graphics.DrawImage(editIcon, editRect, 0, 0, editIcon.Width, editIcon.Height,
                            GraphicsUnit.Pixel, attributes);
                    }
                }

                // ✅ Vẽ icon Xóa với độ mờ nếu không có quyền
                if (canDelete)
                {
                    e.Graphics.DrawImage(deleteIcon, deleteRect);
                }
                else
                {
                    var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
                        new float[][] {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                        });
                    using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                    {
                        attributes.SetColorMatrix(grayScaleMatrix);
                        e.Graphics.DrawImage(deleteIcon, deleteRect, 0, 0, deleteIcon.Width, deleteIcon.Height,
                            GraphicsUnit.Pixel, attributes);
                    }
                }

                e.Handled = true;
            }
        }

        // ✅ Tạo icon "Xem" (eye icon) bằng code
        private Image CreateViewIcon()
        {
            Bitmap bmp = new Bitmap(18, 18);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                
                // Vẽ hình mắt đơn giản
                // Màu xanh dương cho icon "Xem"
                Pen pen = new Pen(Color.FromArgb(30, 136, 229), 2);
                Brush brush = new SolidBrush(Color.FromArgb(30, 136, 229));
                
                // Vẽ hình oval (mắt)
                g.DrawEllipse(pen, 2, 4, 14, 10);
                
                // Vẽ con ngươi
                g.FillEllipse(brush, 7, 7, 4, 4);
            }
            return bmp;
        }

        // Xử lý click icon cho bảng Học Sinh
        private void tableHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableHocSinh.Columns["ThaoTacHS"].Index)
            {
                HandleIconClick(tableHocSinh, e.RowIndex, "MaHS");
            }
        }

        // Định dạng màu cho cột Giới tính, Trạng thái
        private void tableHocSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua header

            // Giới tính
            if (tableHocSinh.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null)
            {
                FormatGenderCell(e); // Gọi hàm định dạng
            }

            // Trạng thái
            if (tableHocSinh.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                FormatStatusCell(e); // Gọi hàm định dạng
            }
        }

        // Xử lý khi chọn dòng trong bảng Học Sinh - Lọc bảng Mối Quan Hệ
        private void tableHocSinh_SelectionChanged(object sender, EventArgs e)
        {
            // Không xử lý khi đang load dữ liệu ban đầu
            if (isLoadingData) return;

            if (tableHocSinh.SelectedRows.Count > 0)
            {
                var selectedRow = tableHocSinh.SelectedRows[0];
                if (selectedRow.Cells["MaHS"].Value != null)
                {
                    // Lấy đúng mã học sinh để lọc mối quan hệ
                    int maHocSinh = Convert.ToInt32(selectedRow.Cells["MaHS"].Value);
                    LoadMoiQuanHeByHocSinh(maHocSinh);
                }
                else
                {
                    // Nếu không lấy được mã, hiển thị tất cả
                    LoadSampleDataMoiQuanHe();
                }
            }
            else
            {
                // Nếu không có dòng nào được chọn, hiển thị tất cả
                LoadSampleDataMoiQuanHe();
            }
        }

        #endregion

        #region Bảng Phụ Huynh

        private void SetupTablePhuHuynh()
        {
            // --- Xóa cột cũ và cấu hình chung ---
            tablePhuHuynh.Columns.Clear();
            ApplyBaseTableStyle(tablePhuHuynh); // Áp dụng style chung

            // --- Thêm cột mới ---
            tablePhuHuynh.Columns.Add("MaPH", "Mã PH");
            tablePhuHuynh.Columns.Add("HoTenPH", "Họ và Tên");
            tablePhuHuynh.Columns.Add("Sdt", "SĐT");
            tablePhuHuynh.Columns.Add("Email", "Email");
            tablePhuHuynh.Columns.Add("DiaChi", "Địa chỉ");
            tablePhuHuynh.Columns.Add("HocSinh", "Học Sinh"); // ✅ Thêm cột Học Sinh
            tablePhuHuynh.Columns.Add("ThaoTacPH", "Thao tác");

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tablePhuHuynh);
            tablePhuHuynh.Columns["HoTenPH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["DiaChi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["HocSinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["HocSinh"].DefaultCellStyle.WrapMode = DataGridViewTriState.True; // ✅ Cho phép xuống dòng

            // --- Tùy chỉnh kích thước ---
            tablePhuHuynh.Columns["MaPH"].FillWeight = 8; tablePhuHuynh.Columns["MaPH"].MinimumWidth = 50;
            tablePhuHuynh.Columns["HoTenPH"].FillWeight = 15; tablePhuHuynh.Columns["HoTenPH"].MinimumWidth = 90;
            tablePhuHuynh.Columns["Sdt"].FillWeight = 10; tablePhuHuynh.Columns["Sdt"].MinimumWidth = 80;
            tablePhuHuynh.Columns["Email"].FillWeight = 15; tablePhuHuynh.Columns["Email"].MinimumWidth = 100;
            tablePhuHuynh.Columns["DiaChi"].FillWeight = 20; tablePhuHuynh.Columns["DiaChi"].MinimumWidth = 150;
            tablePhuHuynh.Columns["HocSinh"].FillWeight = 22; tablePhuHuynh.Columns["HocSinh"].MinimumWidth = 200; // ✅ Cột Học Sinh
            tablePhuHuynh.Columns["ThaoTacPH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablePhuHuynh.Columns["ThaoTacPH"].Width = 80;

            // --- Gắn sự kiện ---
            tablePhuHuynh.CellPainting += tablePhuHuynh_CellPainting;
            tablePhuHuynh.CellClick += tablePhuHuynh_CellClick;
            tablePhuHuynh.SelectionChanged -= tablePhuHuynh_SelectionChanged;
            tablePhuHuynh.SelectionChanged += tablePhuHuynh_SelectionChanged;
        }

        private void LoadSampleDataPhuHuynh()
        {
            tablePhuHuynh.Rows.Clear();
            danhSachPhuHuynhFull = phuHuynhBLL.GetAllPhuHuynh(); // ✅ Load tất cả từ DB
            danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>(danhSachPhuHuynhFull); // ✅ Copy sang filtered list

            currentPagePhuHuynh = 1; // Reset về trang 1
            LoadPagedDataPhuHuynh(); // ✅ Load trang đầu tiên
        }

        // ✅ HÀM MỚI: Load dữ liệu Phụ Huynh theo trang
        private void LoadPagedDataPhuHuynh()
        {
            try
            {
                tablePhuHuynh.Rows.Clear();
                bindingListPhuHuynh.Clear();

                // Tính toán phân trang
                int totalRecords = danhSachPhuHuynhFiltered.Count;
                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizePhuHuynh);

                // Đảm bảo currentPage hợp lệ
                if (currentPagePhuHuynh < 1) currentPagePhuHuynh = 1;
                if (currentPagePhuHuynh > totalPages && totalPages > 0) currentPagePhuHuynh = totalPages;

                // Lấy dữ liệu của trang hiện tại
                var pagedData = danhSachPhuHuynhFiltered
                    .Skip((currentPagePhuHuynh - 1) * pageSizePhuHuynh)
                    .Take(pageSizePhuHuynh)
                    .ToList();

                // Thêm vào bảng
                foreach (PhuHuynhDTO ph in pagedData)
                {
                    bindingListPhuHuynh.Add(ph);
                    
                    // ✅ Lấy danh sách học sinh liên quan đến phụ huynh này từ database
                    try
                    {
                        var hocSinhLienQuan = hocSinhPhuHuynhBLL.GetHocSinhByPhuHuynh(ph.MaPhuHuynh);
                        var danhSachHocSinh = hocSinhLienQuan
                            .Select(item => $"{item.hocSinh.MaHS} - {item.hocSinh.HoTen} ({item.moiQuanHe})")
                            .ToList();
                        
                        string danhSachHocSinhText = danhSachHocSinh.Count > 0 
                            ? string.Join("\n", danhSachHocSinh) 
                            : "Chưa có học sinh";
                        
                        tablePhuHuynh.Rows.Add(ph.MaPhuHuynh, ph.HoTen, ph.SoDienThoai,
                                              ph.Email, ph.DiaChi, danhSachHocSinhText, "");
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi, vẫn hiển thị phụ huynh nhưng không có học sinh
                        Console.WriteLine($"Lỗi khi lấy học sinh của phụ huynh {ph.MaPhuHuynh}: {ex.Message}");
                        tablePhuHuynh.Rows.Add(ph.MaPhuHuynh, ph.HoTen, ph.SoDienThoai,
                                              ph.Email, ph.DiaChi, "Lỗi khi tải dữ liệu", "");
                    }
                }

                // ✅ Cập nhật label phân trang cho Phụ Huynh
                UpdatePaginationLabelPhuHuynh(totalPages, totalRecords);

                // ✅ Enable/Disable nút phân trang cho Phụ Huynh
                UpdatePaginationButtonsPhuHuynh(totalPages);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu phụ huynh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Vẽ icon cho bảng Phụ Huynh
        private void tablePhuHuynh_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tablePhuHuynh.Columns["ThaoTacPH"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                // ✅ Lấy permission từ Tag
                dynamic permissions = tablePhuHuynh.Tag;
                bool canUpdate = permissions?.CanUpdate ?? true;
                bool canDelete = permissions?.CanDelete ?? true;

                Image editIcon = Properties.Resources.repair;
                Image deleteIcon = Properties.Resources.bin;

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle editRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);

                // ✅ Vẽ icon với độ mờ nếu không có quyền
                if (canUpdate)
                {
                    e.Graphics.DrawImage(editIcon, editRect);
                }
                else
                {
                    // ✅ BỎ USING CHO ColorMatrix - chỉ dùng cho ImageAttributes
                    var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
                        new float[][] {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                        });
                    {
                        using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                        {
                            attributes.SetColorMatrix(grayScaleMatrix);
                            e.Graphics.DrawImage(editIcon, editRect, 0, 0, editIcon.Width, editIcon.Height,
                                GraphicsUnit.Pixel, attributes);
                        }
                    }
                }

                if (canDelete)
                {
                    e.Graphics.DrawImage(deleteIcon, deleteRect);
                }
                else
                {
                    // ✅ BỎ USING CHO ColorMatrix - chỉ dùng cho ImageAttributes
                    var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
                        new float[][] {
                    new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                    new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new float[] {0, 0, 0, 0.3f, 0},
                    new float[] {0, 0, 0, 0, 1}
                        });
                    {
                        using (var attributes = new ImageAttributes())
                        {
                            attributes.SetColorMatrix(grayScaleMatrix);
                            e.Graphics.DrawImage(deleteIcon, deleteRect, 0, 0, deleteIcon.Width, deleteIcon.Height,
                                GraphicsUnit.Pixel, attributes);
                        }
                    }
                }

                e.Handled = true;
            }
        }

        // Xử lý click icon cho bảng Phụ Huynh
        private void tablePhuHuynh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tablePhuHuynh.Columns["ThaoTacPH"].Index)
            {
                HandleIconClick(tablePhuHuynh, e.RowIndex, "MaPH");
            }
        }

        // Xử lý khi chọn dòng trong bảng Phụ Huynh - Lọc bảng Mối Quan Hệ
        private void tablePhuHuynh_SelectionChanged(object sender, EventArgs e)
        {
            // Không xử lý khi đang load dữ liệu ban đầu
            if (isLoadingData) return;

            if (tablePhuHuynh.SelectedRows.Count > 0)
            {
                var selectedRow = tablePhuHuynh.SelectedRows[0];
                if (selectedRow.Cells["MaPH"].Value != null)
                {
                    int maPhuHuynh = Convert.ToInt32(selectedRow.Cells["MaPH"].Value);
                    LoadMoiQuanHeByPhuHuynh(maPhuHuynh);
                }
            }
            else
            {
                // Nếu không có dòng nào được chọn, hiển thị tất cả
                LoadSampleDataMoiQuanHe();
            }
        }

        #endregion

        #region Bảng Mối Quan Hệ

        private void SetupTableMoiQuanHe()
        {
            // --- Xóa cột cũ và cấu hình chung ---
            tableMoiQuanHe.Columns.Clear();
            ApplyBaseTableStyle(tableMoiQuanHe); // Áp dụng style chung

            // --- Thêm cột mới ---
            tableMoiQuanHe.Columns.Add("HocSinhMQH", "Học Sinh"); // Đổi tên để tránh trùng
            tableMoiQuanHe.Columns.Add("PhuHuynhMQH", "Phụ Huynh"); // Đổi tên để tránh trùng
            tableMoiQuanHe.Columns.Add("QuanHe", "Mối quan hệ");
            tableMoiQuanHe.Columns.Add("ThaoTacMQH", "Thao Tác"); // Đổi tên để tránh trùng

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tableMoiQuanHe);
            tableMoiQuanHe.Columns["HocSinhMQH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableMoiQuanHe.Columns["PhuHuynhMQH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableMoiQuanHe.Columns["QuanHe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa
            tableMoiQuanHe.Columns["ThaoTacMQH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa


            // --- Tùy chỉnh kích thước ---
            tableMoiQuanHe.Columns["HocSinhMQH"].FillWeight = 30; tableMoiQuanHe.Columns["HocSinhMQH"].MinimumWidth = 150;
            tableMoiQuanHe.Columns["PhuHuynhMQH"].FillWeight = 30; tableMoiQuanHe.Columns["PhuHuynhMQH"].MinimumWidth = 150;
            tableMoiQuanHe.Columns["QuanHe"].FillWeight = 15; tableMoiQuanHe.Columns["QuanHe"].MinimumWidth = 100;
            tableMoiQuanHe.Columns["ThaoTacMQH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableMoiQuanHe.Columns["ThaoTacMQH"].Width = 80; // Độ rộng cột thao tác

            // --- Gắn sự kiện ---
            tableMoiQuanHe.CellPainting += tableMoiQuanHe_CellPainting;
            tableMoiQuanHe.CellClick += tableMoiQuanHe_CellClick;
            tableMoiQuanHe.SelectionChanged -= DataGridView_SelectionChanged; // Gỡ sự kiện cũ
            tableMoiQuanHe.SelectionChanged += DataGridView_SelectionChanged; // Gắn sự kiện clear selection
        }

        private void LoadSampleDataMoiQuanHe()
        {
            tableMoiQuanHe.Rows.Clear();
            danhSachMoiQuanHe = hocSinhPhuHuynhBLL.GetAllQuanHe();

            foreach ((int maHS, int maPH, string mqh) item in danhSachMoiQuanHe)
            {
                var hs = danhSachHocSinhFull.FirstOrDefault(x => x.MaHS == item.maHS);
                var ph = danhSachPhuHuynhFull.FirstOrDefault(x => x.MaPhuHuynh == item.maPH);
                string tenHS = hs != null ? $"{hs.MaHS} - {hs.HoTen}" : $"Không tìm thấy HS ({item.maHS})";
                string tenPH = ph != null ? $"{ph.MaPhuHuynh} - {ph.HoTen}" : $"Không tìm thấy PH ({item.maPH})";
                tableMoiQuanHe.Rows.Add(tenHS, tenPH, item.mqh, "");
            }

        }

        /// <summary>
        /// Load mối quan hệ chỉ của học sinh có trong tableHocSinh hiện tại (chỉ mối quan hệ đầu tiên của mỗi học sinh)
        /// </summary>
        private void LoadMoiQuanHeForCurrentPageHocSinh()
        {
            tableMoiQuanHe.Rows.Clear();
            
            // Lấy danh sách mã học sinh trong tableHocSinh hiện tại
            var maHocSinhInTable = new HashSet<int>();
            foreach (DataGridViewRow row in tableHocSinh.Rows)
            {
                if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int maHS))
                {
                    maHocSinhInTable.Add(maHS);
                }
            }

            if (maHocSinhInTable.Count == 0)
            {
                return; // Không có học sinh nào trong bảng
            }

            // Lấy tất cả mối quan hệ
            danhSachMoiQuanHe = hocSinhPhuHuynhBLL.GetAllQuanHe();

            // Lọc và chỉ lấy mối quan hệ đầu tiên của mỗi học sinh trong tableHocSinh
            var processedHS = new HashSet<int>();
            foreach ((int maHS, int maPH, string mqh) item in danhSachMoiQuanHe)
            {
                // Chỉ xử lý học sinh có trong tableHocSinh và chưa được xử lý (lấy mối quan hệ đầu tiên)
                if (maHocSinhInTable.Contains(item.maHS) && !processedHS.Contains(item.maHS))
                {
                    var hs = danhSachHocSinhFull.FirstOrDefault(x => x.MaHS == item.maHS);
                    var ph = danhSachPhuHuynhFull.FirstOrDefault(x => x.MaPhuHuynh == item.maPH);
                    string tenHS = hs != null ? $"{hs.MaHS} - {hs.HoTen}" : $"Không tìm thấy HS ({item.maHS})";
                    string tenPH = ph != null ? $"{ph.MaPhuHuynh} - {ph.HoTen}" : $"Không tìm thấy PH ({item.maPH})";
                    tableMoiQuanHe.Rows.Add(tenHS, tenPH, item.mqh, "");
                    processedHS.Add(item.maHS); // Đánh dấu đã xử lý học sinh này
                }
            }
        }

        // Lọc bảng Mối Quan Hệ theo Học Sinh được chọn
        /// <summary>
        /// Kiểm tra học sinh đã có mối quan hệ cùng loại chưa
        /// </summary>
        private bool DaTonTaiMoiQuanHeCungLoai(int maHocSinh, string loaiQuanHe)
        {
            danhSachMoiQuanHe = hocSinhPhuHuynhBLL.GetAllQuanHe();
            return danhSachMoiQuanHe.Any(x => x.hocSinh == maHocSinh && x.moiQuanHe.Trim().ToLower() == loaiQuanHe.Trim().ToLower());
        }

        /// <summary>
        /// Hàm thêm mối quan hệ, chỉ cho phép mỗi loại 1 lần trên mỗi học sinh
        /// </summary>
        private bool ThemMoiQuanHeKhongTrung(int maHocSinh, int maPhuHuynh, string loaiQuanHe)
        {
            if (DaTonTaiMoiQuanHeCungLoai(maHocSinh, loaiQuanHe))
            {
                MessageBox.Show($"Học sinh này đã có mối quan hệ '{loaiQuanHe}'. Không thể thêm trùng!", "Trùng mối quan hệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return hocSinhPhuHuynhBLL.AddQuanHe(maHocSinh, maPhuHuynh, loaiQuanHe);
        }
        private void LoadMoiQuanHeByHocSinh(int maHocSinh)
        {
            tableMoiQuanHe.Rows.Clear();
            danhSachMoiQuanHe = hocSinhPhuHuynhBLL.GetAllQuanHe();

            // Lọc chỉ các mối quan hệ có học sinh này
            var filteredList = danhSachMoiQuanHe.Where(x => x.hocSinh == maHocSinh).ToList();

            foreach ((int maHS, int maPH, string mqh) item in filteredList)
            {
                var hs = danhSachHocSinhFull.FirstOrDefault(x => x.MaHS == item.maHS);
                var ph = danhSachPhuHuynhFull.FirstOrDefault(x => x.MaPhuHuynh == item.maPH);
                string tenHS = hs != null ? $"{hs.MaHS} - {hs.HoTen}" : $"Không tìm thấy HS ({item.maHS})";
                string tenPH = ph != null ? $"{ph.MaPhuHuynh} - {ph.HoTen}" : $"Không tìm thấy PH ({item.maPH})";
                tableMoiQuanHe.Rows.Add(tenHS, tenPH, item.mqh, "");
            }
        }

        // Lọc bảng Mối Quan Hệ theo Phụ Huynh được chọn
        private void LoadMoiQuanHeByPhuHuynh(int maPhuHuynh)
        {
            tableMoiQuanHe.Rows.Clear();
            danhSachMoiQuanHe = hocSinhPhuHuynhBLL.GetAllQuanHe();

            // Lọc chỉ các mối quan hệ có phụ huynh này
            var filteredList = danhSachMoiQuanHe.Where(x => x.phuHuynh == maPhuHuynh).ToList();

            foreach ((int maHS, int maPH, string mqh) item in filteredList)
            {
                var hs = danhSachHocSinhFull.FirstOrDefault(x => x.MaHS == item.maHS);
                var ph = danhSachPhuHuynhFull.FirstOrDefault(x => x.MaPhuHuynh == item.maPH);
                string tenHS = hs != null ? $"{hs.MaHS} - {hs.HoTen}" : $"Không tìm thấy HS ({item.maHS})";
                string tenPH = ph != null ? $"{ph.MaPhuHuynh} - {ph.HoTen}" : $"Không tìm thấy PH ({item.maPH})";
                tableMoiQuanHe.Rows.Add(tenHS, tenPH, item.mqh, "");
            }
        }

        // Vẽ icon cho bảng Mối Quan Hệ
        private void tableMoiQuanHe_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableMoiQuanHe.Columns["ThaoTacMQH"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                // Chỉ lấy icon Xóa
                Image deleteIcon = Properties.Resources.bin;

                int iconSize = 18;

                // Tính toán vị trí X, Y để căn giữa 1 icon
                int startX = e.CellBounds.Left + (e.CellBounds.Width - iconSize) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle deleteRect = new Rectangle(startX, y, iconSize, iconSize);

                // Chỉ vẽ icon Xóa
                e.Graphics.DrawImage(deleteIcon, deleteRect);

                e.Handled = true;
            }
        }

        // Xử lý click icon cho bảng Mối Quan Hệ
        private void tableMoiQuanHe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Chỉ xử lý khi click vào hàng dữ liệu và cột "ThaoTacMQH"
            if (e.RowIndex >= 0 && e.ColumnIndex == tableMoiQuanHe.Columns["ThaoTacMQH"].Index)
            {
                try
                {

                    // Lấy chuỗi hiển thị dạng "MaHS - HoTen" và "MaPhuHuynh - HoTen"
                    string hsDisplay = tableMoiQuanHe.Rows[e.RowIndex].Cells["HocSinhMQH"].Value?.ToString();
                    string phDisplay = tableMoiQuanHe.Rows[e.RowIndex].Cells["PhuHuynhMQH"].Value?.ToString();

                    // Tách mã số từ chuỗi hiển thị
                    int maHS_local = -1;
                    int maPH_local = -1;
                    if (!string.IsNullOrEmpty(hsDisplay) && hsDisplay.Contains("-"))
                    {
                        var parts = hsDisplay.Split('-');
                        int.TryParse(parts[0].Trim(), out maHS_local);
                    }
                    if (!string.IsNullOrEmpty(phDisplay) && phDisplay.Contains("-"))
                    {
                        var parts = phDisplay.Split('-');
                        int.TryParse(parts[0].Trim(), out maPH_local);
                    }

                    if (maHS_local == -1 || maPH_local == -1)
                    {
                        MessageBox.Show("Không thể xác định học sinh hoặc phụ huynh để xóa mối quan hệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (hocSinhPhuHuynhBLL.DeleteQuanHe(maHS_local, maPH_local))
                    {
                        MessageBox.Show("Đã xóa mối quan hệ.");
                        LoadSampleDataMoiQuanHe(); // Nạp lại bảng MQH
                    }
                    else
                    {
                        MessageBox.Show("Xóa mối quan hệ thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Không thể lấy thông tin mối quan hệ để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) // Bắt các lỗi khác
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xóa mối quan hệ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Hàm dùng chung và Helper

        // Hàm áp dụng style cơ bản cho DataGridView
        private void ApplyBaseTableStyle(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Mặc định căn giữa header
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ColumnHeadersHeight = 42; // Tăng chiều cao header

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.RowTemplate.Height = 46;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            // Xóa sự kiện cũ để tránh gắn nhiều lần
            dgv.CellMouseEnter -= DataGridView_CellMouseEnter;
            dgv.CellMouseLeave -= DataGridView_CellMouseLeave;

            // Gắn sự kiện hover
            dgv.CellMouseEnter += DataGridView_CellMouseEnter;
            dgv.CellMouseLeave += DataGridView_CellMouseLeave;

            // Đảm bảo màu header không đổi khi click
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
        }

        // Hàm căn chỉnh cột và wrap text
        private void ApplyColumnAlignmentAndWrapping(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Mặc định căn giữa cell
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }

        // Hàm xử lý click icon chung
        // Xử lý click icon chung (KIỂM TRA QUYỀN TRƯỚC KHI THỰC HIỆN)
        private void HandleIconClick(Guna.UI2.WinForms.Guna2DataGridView dgv, int rowIndex, string idColumnName)
        {
            string thaoTacColName = "";
            if (dgv == tableHocSinh) thaoTacColName = "ThaoTacHS";
            else if (dgv == tablePhuHuynh) thaoTacColName = "ThaoTacPH";
            else return;

            Rectangle cellBounds = dgv.GetCellDisplayRectangle(dgv.Columns[thaoTacColName].Index, rowIndex, false);
            Point clickPosInCell = dgv.PointToClient(Cursor.Position);
            int xClick = clickPosInCell.X - cellBounds.Left;

            int iconSize = 18;
            int spacing = 12;
            int totalWidth = iconSize * 3 + spacing * 2; // ✅ 3 icon: Xem, Sửa, Xóa
            int startXInCell = (cellBounds.Width - totalWidth) / 2;

            // ✅ Tính toán vị trí các icon
            int viewIconEndX = startXInCell + iconSize;
            int editIconStartX = startXInCell + iconSize + spacing;
            int editIconEndX = editIconStartX + iconSize;
            int deleteIconStartX = editIconStartX + iconSize + spacing;
            int deleteIconEndX = deleteIconStartX + iconSize;

            string idValueStr = dgv.Rows[rowIndex].Cells[idColumnName].Value?.ToString();

            // ✅ Click Xem chi tiết (icon đầu tiên)
            if (dgv == tableHocSinh && xClick >= startXInCell && xClick < viewIconEndX)
            {
                int maHS;
                if (int.TryParse(idValueStr, out maHS))
                {
                    // Mở form xem chi tiết
                    GUI.XemChiTietHocSinh frmChiTiet = new GUI.XemChiTietHocSinh(maHS);
                    frmChiTiet.StartPosition = FormStartPosition.CenterParent;
                    frmChiTiet.ShowDialog();
                }
                return;
            }

            // ✅ Click Sửa (icon thứ hai)
            if (xClick >= editIconStartX && xClick < editIconEndX)
            {
                // Kiểm tra quyền
                if (!PermissionHelper.CheckDataGridIconPermission(dgv, "edit", "Quản lý học sinh"))
                    return;

                int maToEdit;
                if (int.TryParse(idValueStr, out maToEdit))
                {
                    if (dgv == tableHocSinh)
                    {
                        ChinhSuaHocSinh frmEditHS = new ChinhSuaHocSinh(maToEdit);
                        frmEditHS.StartPosition = FormStartPosition.CenterParent;

                        DialogResult result = frmEditHS.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            HocSinhDTO updatedHS = frmEditHS.UpdatedHocSinh;

                            if (updatedHS != null)
                            {
                                var hsInList = bindingListHocSinh.FirstOrDefault(hs => hs.MaHS == maToEdit);
                                if (hsInList != null)
                                {
                                    int index = bindingListHocSinh.IndexOf(hsInList);
                                    bindingListHocSinh[index] = updatedHS;
                                }

                                var hsInFullList = danhSachHocSinhFull.FirstOrDefault(hs => hs.MaHS == maToEdit);
                                if (hsInFullList != null)
                                {
                                    int index = danhSachHocSinhFull.IndexOf(hsInFullList);
                                    danhSachHocSinhFull[index] = updatedHS;
                                }

                                // ✅ Lấy tên lớp của học sinh
                                string tenLop = "Chưa phân lớp";
                                try
                                {
                                    int maHocKyHienTai = 0;
                                    List<HocKyDTO> dsHocKy = hocKyBUS.DocDSHocKy();
                                    if (dsHocKy != null && dsHocKy.Count > 0)
                                    {
                                        var hocKyDangDienRa = dsHocKy.FirstOrDefault(hk => hk.TrangThai == "Đang diễn ra");
                                        if (hocKyDangDienRa != null)
                                        {
                                            maHocKyHienTai = hocKyDangDienRa.MaHocKy;
                                        }
                                        else
                                        {
                                            var hocKyMoiNhat = dsHocKy.OrderByDescending(hk => hk.NgayBD).FirstOrDefault();
                                            if (hocKyMoiNhat != null)
                                            {
                                                maHocKyHienTai = hocKyMoiNhat.MaHocKy;
                                            }
                                        }
                                    }

                                    if (maHocKyHienTai > 0)
                                    {
                                        int maLop = phanLopBLL.GetLopByHocSinh(maToEdit, maHocKyHienTai);
                                        if (maLop > 0)
                                        {
                                            var lop = lopHocBUS.LayLopTheoId(maLop);
                                            if (lop != null)
                                            {
                                                tenLop = lop.tenLop;
                                            }
                                        }
                                    }
                                }
                                catch { }

                                dgv.Rows[rowIndex].SetValues(
                                    updatedHS.MaHS,
                                    updatedHS.HoTen,
                                    tenLop, // ✅ Cột Lớp
                                    updatedHS.NgaySinh.ToString("dd/MM/yyyy"),
                                    updatedHS.GioiTinh,
                                    updatedHS.SdtHS ?? "",
                                    updatedHS.Email ?? "",
                                    updatedHS.TrangThai,
                                    ""
                                );
                            }

                            LoadSampleDataMoiQuanHe();
                            SetupHeaderAndStats();
                        }
                    }
                    else if (dgv == tablePhuHuynh)
                    {
                        ChinhSuaPhuHuynh frmEditPH = new ChinhSuaPhuHuynh(maToEdit);
                        frmEditPH.StartPosition = FormStartPosition.CenterParent;

                        DialogResult result = frmEditPH.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            PhuHuynhDTO updatedPH = frmEditPH.UpdatedPhuHuynh;

                            if (updatedPH != null)
                            {
                                var phInList = bindingListPhuHuynh.FirstOrDefault(ph => ph.MaPhuHuynh == maToEdit);
                                if (phInList != null)
                                {
                                    int index = bindingListPhuHuynh.IndexOf(phInList);
                                    bindingListPhuHuynh[index] = updatedPH;
                                }

                                var phInFullList = danhSachPhuHuynhFull.FirstOrDefault(ph => ph.MaPhuHuynh == maToEdit);
                                if (phInFullList != null)
                                {
                                    int index = danhSachPhuHuynhFull.IndexOf(phInFullList);
                                    danhSachPhuHuynhFull[index] = updatedPH;
                                }

                                dgv.Rows[rowIndex].SetValues(
                                    updatedPH.MaPhuHuynh,
                                    updatedPH.HoTen,
                                    updatedPH.SoDienThoai,
                                    updatedPH.Email,
                                    updatedPH.DiaChi,
                                    ""
                                );
                            }
                        }
                    }
                }
                else { MessageBox.Show("Mã không hợp lệ để sửa."); }
            }
            // ✅ Click Xóa
            else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
            {
                // Kiểm tra quyền
                if (!PermissionHelper.CheckDataGridIconPermission(dgv, "delete", "Quản lý học sinh"))
                    return;

                if (dgv == tableHocSinh)
                {
                    if (MessageBox.Show($"Bạn có chắc muốn xóa học sinh {idValueStr}?\nTất cả mối quan hệ phụ huynh liên quan cũng sẽ bị xóa.",
                                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int maHS;
                        if (int.TryParse(idValueStr, out maHS))
                        {
                            // ======= BỔ SUNG: THÔNG BÁO TRẠNG THÁI TÀI KHOẢN TRƯỚC =======
                            string accountStatusMsg = null;
                            try
                            {
                                // Tìm TenDangNhap của học sinh vừa xóa (nếu còn trong danh sách cũ)
                                string tenDangNhap = null;
                                var hsOld = bindingListHocSinh.FirstOrDefault(hs => hs.MaHS == maHS) ?? danhSachHocSinhFull.FirstOrDefault(hs => hs.MaHS == maHS);
                                if (hsOld != null && !string.IsNullOrWhiteSpace(hsOld.TenDangNhap))
                                {
                                    tenDangNhap = hsOld.TenDangNhap;
                                }
                                if (string.IsNullOrWhiteSpace(tenDangNhap))
                                {
                                    try
                                    {
                                        var hsDto = hocSinhBLL.GetHocSinhById(maHS);
                                        if (hsDto != null && !string.IsNullOrWhiteSpace(hsDto.TenDangNhap))
                                            tenDangNhap = hsDto.TenDangNhap;
                                    }
                                    catch { }
                                }
                                if (!string.IsNullOrWhiteSpace(tenDangNhap))
                                {
                                    // Lấy thông tin tài khoản
                                    var tk = nguoiDungBLL.GetNguoiDungByTenDangNhap(tenDangNhap);
                                    if (tk != null)
                                    {
                                        if (tk.TrangThai != "Tạm khóa")
                                        {
                                            // Cập nhật trạng thái sang "Tạm khóa"
                                            bool updateStatus = nguoiDungBLL.UpdateTrangThai(tenDangNhap, "Tạm khóa");
                                            if (updateStatus)
                                            {
                                                accountStatusMsg = $"Đã chuyển trạng thái tài khoản '{tenDangNhap}' sang 'Tạm khóa'!";
                                            }
                                            else
                                            {
                                                accountStatusMsg = $"KHÔNG thể chuyển trạng thái tài khoản '{tenDangNhap}' sang 'Tạm khóa'!\nVui lòng kiểm tra lại dữ liệu hoặc liên hệ quản trị viên.";
                                            }
                                        }
                                        else
                                        {
                                            accountStatusMsg = $"Tài khoản '{tenDangNhap}' đã ở trạng thái 'Tạm khóa'.";
                                        }
                                    }
                                }
                            }
                            catch (Exception exTk)
                            {
                                accountStatusMsg = $"[WARNING] Không thể cập nhật trạng thái tài khoản học sinh khi xóa: {exTk.Message}";
                            }
                            // ======= END BỔ SUNG =======

                            bool deleteQuanHeSuccess = hocSinhPhuHuynhBLL.DeleteQuanHeByHocSinh(maHS); // Xóa QH trước
                            bool deleteHSSuccess = hocSinhBLL.DeleteHocSinh(maHS); // Xóa HS sau

                            if (deleteHSSuccess)
                            {
                                // Hiển thị thông báo trạng thái tài khoản (nếu có)
                                if (!string.IsNullOrWhiteSpace(accountStatusMsg))
                                {
                                    MessageBox.Show(accountStatusMsg, "Cập nhật tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                // ✅ Xóa khỏi các danh sách
                                var hsToRemove = bindingListHocSinh.FirstOrDefault(hs => hs.MaHS == maHS);
                                if (hsToRemove != null)
                                {
                                    bindingListHocSinh.Remove(hsToRemove);
                                }

                                var hsFullToRemove = danhSachHocSinhFull.FirstOrDefault(hs => hs.MaHS == maHS);
                                if (hsFullToRemove != null)
                                {
                                    danhSachHocSinhFull.Remove(hsFullToRemove);
                                }

                                // ✅ Xóa khỏi danh sách filtered
                                danhSachHocSinhFiltered.RemoveAll(hs => hs.MaHS == maHS);

                                tableHocSinh.Rows.RemoveAt(rowIndex);

                                // ✅ Tính toán lại số trang (nếu trang hiện tại trống sau khi xóa, chuyển về trang trước)
                                int totalRecords = danhSachHocSinhFiltered.Count;
                                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizeHocSinh);
                                if (totalPages > 0 && currentPageHocSinh > totalPages)
                                {
                                    currentPageHocSinh = totalPages;
                                    LoadPagedDataHocSinh();
                                }
                                else
                                {
                                    ForceUpdatePaginationLabel();
                                }

                                LoadSampleDataMoiQuanHe();
                                SetupHeaderAndStats();

                                MessageBox.Show("Đã xóa học sinh và các mối quan hệ liên quan.");
                            }
                            else
                            {
                                MessageBox.Show("Xóa học sinh thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else { MessageBox.Show("Mã học sinh không hợp lệ."); }
                    }
                }
                else if (dgv == tablePhuHuynh)
                {
                    if (MessageBox.Show($"Bạn có chắc muốn xóa phụ huynh {idValueStr}?\nTất cả mối quan hệ với học sinh liên quan cũng sẽ bị xóa.",
                                       "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int maPH;
                        if (int.TryParse(idValueStr, out maPH))
                        {
                            bool deleteQuanHeSuccess = hocSinhPhuHuynhBLL.DeleteQuanHeByPhuHuynh(maPH);
                            bool deletePHSuccess = phuHuynhBLL.DeletePhuHuynh(maPH);

                            if (deletePHSuccess)
                            {
                                // ✅ Xóa khỏi các danh sách
                                var phToRemove = bindingListPhuHuynh.FirstOrDefault(ph => ph.MaPhuHuynh == maPH);
                                if (phToRemove != null)
                                {
                                    bindingListPhuHuynh.Remove(phToRemove);
                                }

                                var phFullToRemove = danhSachPhuHuynhFull.FirstOrDefault(ph => ph.MaPhuHuynh == maPH);
                                if (phFullToRemove != null)
                                {
                                    danhSachPhuHuynhFull.Remove(phFullToRemove);
                                }

                                // ✅ Xóa khỏi danh sách filtered
                                danhSachPhuHuynhFiltered.RemoveAll(ph => ph.MaPhuHuynh == maPH);

                                tablePhuHuynh.Rows.RemoveAt(rowIndex);

                                // ✅ Tính toán lại số trang (nếu trang hiện tại trống sau khi xóa, chuyển về trang trước)
                                int totalRecords = danhSachPhuHuynhFiltered.Count;
                                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizePhuHuynh);
                                if (totalPages > 0 && currentPagePhuHuynh > totalPages)
                                {
                                    currentPagePhuHuynh = totalPages;
                                    LoadPagedDataPhuHuynh();
                                }
                                else
                                {
                                    ForceUpdatePaginationLabel();
                                }

                                LoadSampleDataMoiQuanHe();
                                SetupHeaderAndStats();

                                MessageBox.Show("Đã xóa phụ huynh và các mối quan hệ liên quan.");
                            }
                            else
                            {
                                MessageBox.Show("Xóa phụ huynh thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else { MessageBox.Show("Mã phụ huynh không hợp lệ."); }
                    }
                }
            }
        }

        // Hàm định dạng ô Giới tính
        private void FormatGenderCell(DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold); // Hơi đậm hơn
            e.CellStyle.Padding = new Padding(5, 3, 5, 3); // Thêm padding nhẹ

            if (e.Value.ToString() == "Nam")
            {
                e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216);
                e.CellStyle.BackColor = Color.FromArgb(219, 234, 254);
            }
            else if (e.Value.ToString() == "Nữ")
            {
                e.CellStyle.ForeColor = Color.FromArgb(190, 24, 93);
                e.CellStyle.BackColor = Color.FromArgb(253, 232, 255); // Đổi màu nền Nữ
            }
        }

        // ✅ Cải thiện: Hàm định dạng ô Trạng thái với màu sắc rõ ràng hơn
        private void FormatStatusCell(DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold);
            e.CellStyle.Padding = new Padding(5, 3, 5, 3);

            string trangThai = e.Value?.ToString() ?? "";

            if (trangThai == "Đang học")
            {
                e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá (#16A34A) - đồng bộ với thống kê
                e.CellStyle.BackColor = Color.FromArgb(240, 253, 244); // Nền xanh lá nhạt
            }
            else if (trangThai == "Nghỉ học" || trangThai.Contains("Nghỉ"))
            {
                e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ (#DC2626)
                e.CellStyle.BackColor = Color.FromArgb(254, 242, 242); // Nền đỏ nhạt
            }
            else if (trangThai == "Tạm dừng" || trangThai.Contains("Tạm"))
            {
                e.CellStyle.ForeColor = Color.FromArgb(234, 179, 8); // Vàng (#EAB308)
                e.CellStyle.BackColor = Color.FromArgb(254, 252, 232); // Nền vàng nhạt
            }
            else if (trangThai == "Đã tốt nghiệp" || trangThai.Contains("Tốt nghiệp"))
            {
                e.CellStyle.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương (#1E88E5)
                e.CellStyle.BackColor = Color.FromArgb(239, 246, 255); // Nền xanh dương nhạt
            }
            else
            {
                // Trạng thái khác - màu xám
                e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128); // Xám (#6B7280)
                e.CellStyle.BackColor = Color.FromArgb(249, 250, 251); // Nền xám nhạt
            }
        }


        // Sự kiện hover chung
        private void DataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(243, 246, 255);
            }
        }

        // Sự kiện rời chuột chung
        private void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    (e.RowIndex % 2 == 0) ? Color.White : Color.FromArgb(250, 250, 250);
            }
        }

        // Bỏ chọn dòng cho bảng Mối Quan Hệ
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
            dgv.ClearSelection();
        }

        #endregion

        private void statCardNam_Load(object sender, EventArgs e)
        {

        }

        private void headerQuanLiHocSinh_Load(object sender, EventArgs e)
        {

        }

        private void btnThemHocSinh_Click(object sender, EventArgs e)
        {

            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLHOCSINH, "Quản lý học sinh"))
                return;

            // 1. Tạo và hiển thị form Thêm
            ThemHoSoHocSinh frm = new ThemHoSoHocSinh();
            frm.StartPosition = FormStartPosition.CenterScreen;

            // 2. Hiển thị form dưới dạng Dialog và chờ kết quả
            DialogResult result = frm.ShowDialog();

            // 3. Kiểm tra kết quả trả về từ form Thêm
            if (result == DialogResult.OK)
            {
                try
                {
                    // ✅ Lấy học sinh vừa tạo từ form
                    HocSinhDTO newHS = frm.NewHocSinh;

                    if (newHS != null)
                    {
                        // ✅ Thêm vào danh sách full
                        danhSachHocSinhFull.Add(newHS);
                        
                        // ✅ Thêm vào danh sách filtered
                        danhSachHocSinhFiltered.Add(newHS);

                        // ✅ Tính toán lại số trang và kiểm tra xem học sinh mới có nằm trong trang hiện tại không
                        int totalRecords = danhSachHocSinhFiltered.Count;
                        int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizeHocSinh);
                        int startIndex = (currentPageHocSinh - 1) * pageSizeHocSinh;
                        int endIndex = startIndex + pageSizeHocSinh;
                        int currentIndex = danhSachHocSinhFiltered.Count - 1; // Index của học sinh vừa thêm

                        // ✅ Nếu học sinh mới nằm trong trang hiện tại, thêm vào bảng
                        if (currentIndex >= startIndex && currentIndex < endIndex)
                        {
                            bindingListHocSinh.Add(newHS);
                            tableHocSinh.Rows.Add(newHS.MaHS, newHS.HoTen, newHS.NgaySinh.ToString("dd/MM/yyyy"),
                                                 newHS.GioiTinh, newHS.SdtHS ?? "", newHS.Email ?? "", newHS.TrangThai, "");
                        }
                        
                        // ✅ Cập nhật lại số trang
                        ForceUpdatePaginationLabel();
                    }

                    // Load lại bảng Mối quan hệ
                    LoadSampleDataMoiQuanHe();

                    // Cập nhật lại các thẻ thống kê
                    SetupHeaderAndStats();

                    MessageBox.Show("Dữ liệu đã được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPhuHuynh_Click(object sender, EventArgs e)
        {
            isShowingHocSinh = !isShowingHocSinh; // Đảo trạng thái

            // ✅ Xóa text tìm kiếm khi chuyển view
            if (txtTimKiem != null)
            {
                txtTimKiem.Clear();
            }

            UpdateView(); // Cập nhật lại giao diện

            // ✅ Cập nhật lại label phân trang cho view hiện tại
            if (isShowingHocSinh)
            {
                // Reset về filtered list đầy đủ khi chuyển view
                danhSachHocSinhFiltered = danhSachHocSinhFull.ToList();
                currentPageHocSinh = 1;
                LoadPagedDataHocSinh();
            }
            else
            {
                // Reset về filtered list đầy đủ khi chuyển view
                danhSachPhuHuynhFiltered = danhSachPhuHuynhFull.ToList();
                currentPagePhuHuynh = 1;
                LoadPagedDataPhuHuynh();
            }
        }

        private void btnThemPhuHuynh_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLHOCSINH, "Quản lý học sinh"))
                return;

            // 1. Tạo và hiển thị form Thêm
            ThemPhuHuynh frm = new ThemPhuHuynh();
            frm.StartPosition = FormStartPosition.CenterScreen;

            // 2. Hiển thị form dưới dạng Dialog và chờ kết quả
            DialogResult result = frm.ShowDialog();

            // 3. Kiểm tra kết quả trả về từ form Thêm
            if (result == DialogResult.OK)
            {
                try
                {
                    // ✅ Lấy phụ huynh vừa tạo từ form
                    PhuHuynhDTO newPH = frm.NewPhuHuynh;

                    if (newPH != null)
                    {
                        // ✅ Thêm vào danh sách full
                        danhSachPhuHuynhFull.Add(newPH);
                        
                        // ✅ Thêm vào danh sách filtered
                        danhSachPhuHuynhFiltered.Add(newPH);

                        // ✅ Tính toán lại số trang và kiểm tra xem phụ huynh mới có nằm trong trang hiện tại không
                        int totalRecords = danhSachPhuHuynhFiltered.Count;
                        int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizePhuHuynh);
                        int startIndex = (currentPagePhuHuynh - 1) * pageSizePhuHuynh;
                        int endIndex = startIndex + pageSizePhuHuynh;
                        int currentIndex = danhSachPhuHuynhFiltered.Count - 1; // Index của phụ huynh vừa thêm

                        // ✅ Nếu phụ huynh mới nằm trong trang hiện tại, thêm vào bảng
                        if (currentIndex >= startIndex && currentIndex < endIndex)
                        {
                            bindingListPhuHuynh.Add(newPH);
                            tablePhuHuynh.Rows.Add(newPH.MaPhuHuynh, newPH.HoTen, newPH.SoDienThoai,
                                                  newPH.Email, newPH.DiaChi, "");
                        }
                        
                        // ✅ Cập nhật lại số trang
                        ForceUpdatePaginationLabel();
                    }

                    MessageBox.Show("Dữ liệu đã được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// ✅ Hàm chính để tạo file Excel và xuất TẤT CẢ dữ liệu (không phải chỉ 50 dòng trên giao diện).
        /// </summary>
        private void ExportAllDataToExcel(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Xóa các sheet cũ nếu file đã tồn tại
                while (package.Workbook.Worksheets.Count > 0)
                {
                    package.Workbook.Worksheets.Delete(0); // Xóa sheet ở vị trí đầu tiên
                }

                // ✅ 1. Xuất TẤT CẢ Học Sinh từ danhSachHocSinhFull
                ExportHocSinhToWorksheet(package, "HocSinh");

                // ✅ 2. Xuất TẤT CẢ Phụ Huynh từ danhSachPhuHuynhFull
                ExportPhuHuynhToWorksheet(package, "PhuHuynh");

                // ✅ 3. Xuất TẤT CẢ Mối Quan Hệ từ danhSachMoiQuanHe
                ExportMoiQuanHeToWorksheet(package, "MoiQuanHe");

                // Lưu file
                package.Save();
            }
        }

        /// <summary>
        /// ✅ Xuất TẤT CẢ Học Sinh từ danhSachHocSinhFull (không phải từ DataGridView)
        /// </summary>
        private void ExportHocSinhToWorksheet(ExcelPackage package, string sheetName)
        {
            var ws = package.Workbook.Worksheets.Add(sheetName);

            // --- 1. Thêm tiêu đề (Header) ---
            ws.Cells[1, 1].Value = "Mã HS";
            ws.Cells[1, 2].Value = "Họ và tên";
            ws.Cells[1, 3].Value = "Ngày sinh";
            ws.Cells[1, 4].Value = "Giới tính";
            ws.Cells[1, 5].Value = "SĐT"; // ✅ Thêm cột SĐT
            ws.Cells[1, 6].Value = "Email"; // ✅ Thêm cột Email
            ws.Cells[1, 7].Value = "Trạng thái";

            // Định dạng Header
            using (var range = ws.Cells[1, 1, 1, 7]) // ✅ Đổi từ 1,5 thành 1,7
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                range.Style.Font.Color.SetColor(Color.White);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // --- 2. Xuất TẤT CẢ dữ liệu từ danhSachHocSinhFull ---
            int row = 2;
            foreach (var hs in danhSachHocSinhFull)
            {
                ws.Cells[row, 1].Value = hs.MaHS;
                ws.Cells[row, 2].Value = hs.HoTen;
                ws.Cells[row, 3].Value = hs.NgaySinh.ToString("dd/MM/yyyy");
                ws.Cells[row, 4].Value = hs.GioiTinh;
                ws.Cells[row, 5].Value = hs.SdtHS ?? ""; // ✅ Xuất SĐT
                ws.Cells[row, 6].Value = hs.Email ?? ""; // ✅ Xuất Email
                ws.Cells[row, 7].Value = hs.TrangThai;

                // Định dạng màu cho Giới tính
                if (hs.GioiTinh == "Nam")
                    ws.Cells[row, 4].Style.Font.Color.SetColor(Color.FromArgb(29, 78, 216));
                else if (hs.GioiTinh == "Nữ")
                    ws.Cells[row, 4].Style.Font.Color.SetColor(Color.FromArgb(190, 24, 93));

                // Định dạng màu cho Trạng thái
                if (hs.TrangThai == "Đang học" || hs.TrangThai == "Đang học(CT)")
                    ws.Cells[row, 7].Style.Font.Color.SetColor(Color.FromArgb(22, 101, 52)); // ✅ Đổi từ row,5 thành row,7
                else
                    ws.Cells[row, 7].Style.Font.Color.SetColor(Color.FromArgb(153, 27, 27)); // ✅ Đổi từ row,5 thành row,7

                row++;
            }

            // --- 3. Tự động điều chỉnh độ rộng cột ---
            ws.Column(1).Width = 10;  // Mã HS
            ws.Column(2).Width = 30;  // Họ và tên
            ws.Column(3).Width = 15;  // Ngày sinh
            ws.Column(4).Width = 12;  // Giới tính
            ws.Column(5).Width = 15;  // SĐT
            ws.Column(6).Width = 25;  // Email
            ws.Column(7).Width = 15;  // Trạng thái

            // --- 4. Thêm viền cho toàn bộ dữ liệu ---
            if (row > 2)
            {
                using (var range = ws.Cells[1, 1, row - 1, 7]) // ✅ Đổi từ row-1,5 thành row-1,7
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
            }
        }

        /// <summary>
        /// ✅ Xuất TẤT CẢ Phụ Huynh từ danhSachPhuHuynhFull (không phải từ DataGridView)
        /// </summary>
        private void ExportPhuHuynhToWorksheet(ExcelPackage package, string sheetName)
        {
            var ws = package.Workbook.Worksheets.Add(sheetName);

            // --- 1. Thêm tiêu đề (Header) ---
            ws.Cells[1, 1].Value = "Mã PH";
            ws.Cells[1, 2].Value = "Họ và Tên";
            ws.Cells[1, 3].Value = "SĐT";
            ws.Cells[1, 4].Value = "Email";
            ws.Cells[1, 5].Value = "Địa chỉ";

            // Định dạng Header
            using (var range = ws.Cells[1, 1, 1, 5])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                range.Style.Font.Color.SetColor(Color.White);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // --- 2. Xuất TẤT CẢ dữ liệu từ danhSachPhuHuynhFull ---
            int row = 2;
            foreach (var ph in danhSachPhuHuynhFull)
            {
                ws.Cells[row, 1].Value = ph.MaPhuHuynh;
                ws.Cells[row, 2].Value = ph.HoTen;
                ws.Cells[row, 3].Value = ph.SoDienThoai;
                ws.Cells[row, 4].Value = ph.Email;
                ws.Cells[row, 5].Value = ph.DiaChi;
                row++;
            }

            // --- 3. Tự động điều chỉnh độ rộng cột ---
            ws.Column(1).Width = 10;  // Mã PH
            ws.Column(2).Width = 30;  // Họ và tên
            ws.Column(3).Width = 15;  // SĐT
            ws.Column(4).Width = 30;  // Email
            ws.Column(5).Width = 40;  // Địa chỉ

            // --- 4. Thêm viền cho toàn bộ dữ liệu ---
            if (row > 2)
            {
                using (var range = ws.Cells[1, 1, row - 1, 5])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
            }
        }

        /// <summary>
        /// ✅ Xuất TẤT CẢ Mối Quan Hệ từ danhSachMoiQuanHe (không phải từ DataGridView)
        /// </summary>
        private void ExportMoiQuanHeToWorksheet(ExcelPackage package, string sheetName)
        {
            var ws = package.Workbook.Worksheets.Add(sheetName);

            // --- 1. Thêm tiêu đề (Header) ---
            ws.Cells[1, 1].Value = "Học Sinh";
            ws.Cells[1, 2].Value = "Phụ Huynh";
            ws.Cells[1, 3].Value = "Mối quan hệ";

            // Định dạng Header
            using (var range = ws.Cells[1, 1, 1, 3])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                range.Style.Font.Color.SetColor(Color.White);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // --- 2. Xuất TẤT CẢ dữ liệu từ danhSachMoiQuanHe ---
            int row = 2;
            foreach (var item in danhSachMoiQuanHe)
            {
                // ✅ XUẤT TÊN (đơn giản cho người dùng)
                var hocSinh = danhSachHocSinhFull.FirstOrDefault(hs => hs.MaHS == item.hocSinh);
                var phuHuynh = danhSachPhuHuynhFull.FirstOrDefault(ph => ph.MaPhuHuynh == item.phuHuynh);

                string tenHS = hocSinh != null ? hocSinh.HoTen : $"[HS {item.hocSinh}]";
                string tenPH = phuHuynh != null ? phuHuynh.HoTen : $"[PH {item.phuHuynh}]";

                ws.Cells[row, 1].Value = tenHS;
                ws.Cells[row, 2].Value = tenPH;
                ws.Cells[row, 3].Value = item.moiQuanHe;
                row++;
            }

            // --- 3. Tự động điều chỉnh độ rộng cột ---
            ws.Column(1).Width = 30;  // Học Sinh
            ws.Column(2).Width = 30;  // Phụ Huynh
            ws.Column(3).Width = 18;  // Mối quan hệ

            // --- 4. Thêm viền cho toàn bộ dữ liệu ---
            if (row > 2)
            {
                using (var range = ws.Cells[1, 1, row - 1, 3])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
            }
        }


        private void ExportDataGridViewToWorksheet(ExcelPackage package, DataGridView dgv, string sheetName)
        {
            // ❌ HÀM NÀY KHÔNG DÙNG NỮA - Đã thay bằng ExportHocSinhToWorksheet, ExportPhuHuynhToWorksheet, ExportMoiQuanHeToWorksheet
            // để xuất TẤT CẢ dữ liệu từ danh sách Full thay vì chỉ 50 dòng từ DataGridView
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            // Yêu cầu bản quyền cho EPPlus 5 trở lên (dùng NonCommercial cho mục đích học tập/cá nhân)
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                sfd.FileName = "DanhSach_HocSinh_PhuHuynh.xlsx";
                sfd.Title = "Chọn nơi lưu file Excel";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Gọi hàm helper để xuất 3 bảng vào file
                        ExportAllDataToExcel(sfd.FileName);

                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi khi xuất file Excel: " + ex.Message, "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ✅ KHÔNG CẦN NỮA - BỎ COMBOBOX
        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không làm gì - ComboBox đã bị loại bỏ
        }

        // ✅ KHÔNG CẦN NỮA - BỎ COMBOBOX
        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không làm gì - ComboBox đã bị loại bỏ
        }

        // ✅ KHÔNG CẦN NỮA - BỎ HÀM LỌC
        private void FilterAndLoadHocSinh()
        {
            // Không làm gì - Hàm này đã được thay thế bởi LoadSampleDataHocSinh()
        }

        // btnPhanLop
        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            // 1. Tạo và hiển thị form Thêm
            GUI.HocSinh.PhanLop frm = new GUI.HocSinh.PhanLop();
            frm.StartPosition = FormStartPosition.CenterScreen;

            // 2. Hiển thị form dưới dạng Dialog và chờ kết quả
            DialogResult result = frm.ShowDialog();

            // 3. Kiểm tra kết quả trả về từ form Thêm
            if (result == DialogResult.OK)
            {

                try
                {
                    LoadSampleDataHocSinh();   // ✅ Load lại bảng học sinh
                    LoadSampleDataMoiQuanHe(); // Load lại bảng mối quan hệ
                    SetupHeaderAndStats();     // Cập nhật thống kê

                    MessageBox.Show("Dữ liệu đã được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn file Excel để nhập dữ liệu";
                ofd.Filter = "Excel Files|*.xlsx;*.xls";
                ofd.FilterIndex = 1;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                        ImportAllDataFromExcelWithBinding(ofd.FileName);

                        // ✅ Chỉ load lại mối quan hệ và cập nhật thống kê (không reload toàn bộ)
                        LoadMoiQuanHeForCurrentPageHocSinh();
                        SetupHeaderAndStats();
                        
                        // ✅ Cập nhật lại số trang
                        ForceUpdatePaginationLabel();

                        // ✅ Scroll xuống cuối để hiển thị học sinh mới
                        if (tableHocSinh.Rows.Count > 0)
                        {
                            tableHocSinh.FirstDisplayedScrollingRowIndex = Math.Max(0, tableHocSinh.Rows.Count - 1);
                            tableHocSinh.Rows[tableHocSinh.Rows.Count - 1].Selected = true;
                        }

                        MessageBox.Show(
                            "✅ Nhập dữ liệu từ Excel thành công!\n\n" +
                            "📌 TỰ ĐỘNG TẠO TÀI KHOẢN: \n" +
                            "- Hệ thống đã tự động tạo tài khoản cho các học sinh mới\n" +
                            "- Tên đăng nhập: hs001, hs002, hs003...\n" +
                            "- Mật khẩu mặc định: 123456\n" +
                            "- Học sinh nên đổi mật khẩu sau lần đăng nhập đầu tiên\n\n" +
                            "💡 Danh sách đã tự động cuộn xuống cuối để hiển thị học sinh mới nhất!",
                            "Nhập Excel thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"❌ Lỗi khi nhập Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Hàm chính để đọc file Excel và nhập vào 3 bảng: HocSinh, PhuHuynh, MoiQuanHe
        /// </summary>
        private void ImportAllDataFromExcel(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Kiểm tra xem file có ít nhất 3 worksheet không
                if (package.Workbook.Worksheets.Count < 3)
                {
                    throw new Exception("File Excel phải có ít nhất 3 worksheet: HocSinh, PhuHuynh, MoiQuanHe");
                }

                // Đọc từng worksheet
                var wsHocSinh = package.Workbook.Worksheets["HocSinh"] ?? package.Workbook.Worksheets[0];
                var wsPhuHuynh = package.Workbook.Worksheets["PhuHuynh"] ?? package.Workbook.Worksheets[1];
                var wsMoiQuanHe = package.Workbook.Worksheets["MoiQuanHe"] ?? package.Workbook.Worksheets[2];

                // 1. Nhập Học Sinh (TenDangNhap = NULL)
                ImportHocSinhFromWorksheet(wsHocSinh);

                // 2. Nhập Phụ Huynh
                ImportPhuHuynhFromWorksheet(wsPhuHuynh);

                // 3. Nhập Mối Quan Hệ (sau khi đã có HS và PH)
                ImportMoiQuanHeFromWorksheet(wsMoiQuanHe);
            }
        }

        /// <summary>
        /// Nhập dữ liệu từ Excel cho học sinh "Đang học" và thêm trực tiếp vào bindingList/table (không reload)
        /// Chỉ nhận học sinh có trạng thái "Đang học", nếu học sinh không thêm được thì phụ huynh và mối quan hệ cũng không được thêm
        /// </summary>
        private void ImportAllDataFromExcelWithBinding(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Kiểm tra xem file có ít nhất 3 worksheet không
                if (package.Workbook.Worksheets.Count < 3)
                {
                    throw new Exception("File Excel phải có ít nhất 3 worksheet: HocSinh, PhuHuynh, MoiQuanHe");
                }

                // Đọc từng worksheet
                var wsHocSinh = package.Workbook.Worksheets["HocSinh"] ?? package.Workbook.Worksheets[0];
                var wsPhuHuynh = package.Workbook.Worksheets["PhuHuynh"] ?? package.Workbook.Worksheets[1];
                var wsMoiQuanHe = package.Workbook.Worksheets["MoiQuanHe"] ?? package.Workbook.Worksheets[2];

                // 1. Nhập Học Sinh CHỈ với trạng thái "Đang học" và thêm vào bindingList/table
                // Trả về Dictionary: tên học sinh -> (mã học sinh, dòng Excel) để track học sinh thành công
                Dictionary<string, (int maHS, int excelRow)> hocSinhThanhCong = 
                    ImportHocSinhFromWorksheetDangHocWithBinding(wsHocSinh);

                // 2. Chỉ nhập Phụ Huynh của học sinh đã nhập thành công
                // Nếu phụ huynh lỗi thì rollback học sinh
                Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong = 
                    ImportPhuHuynhFromWorksheetDangHoc(wsPhuHuynh, hocSinhThanhCong);

                // 3. Chỉ nhập Mối Quan Hệ của học sinh đã nhập thành công
                ImportMoiQuanHeFromWorksheetDangHoc(wsMoiQuanHe, hocSinhThanhCong, phuHuynhThanhCong);
            }
        }


        /// <summary>
        /// Nhập dữ liệu Học Sinh từ Worksheet (TenDangNhap để NULL)
        /// </summary>
        private void ImportHocSinhFromWorksheet(ExcelWorksheet ws)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            int errorCount = 0;
            int successCount = 0;
            StringBuilder errors = new StringBuilder();
            // ✅ Track SĐT và Email đã nhập trong file Excel để kiểm tra trùng trong cùng file
            HashSet<string> sdtDaNhap = new HashSet<string>();
            HashSet<string> emailDaNhap = new HashSet<string>();
            
            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    // Đọc dữ liệu từ các cột (bỏ qua cột Mã HS vì auto increment)
                    string hoTen = ws.Cells[row, 2].Text.Trim(); // Cột B
                    string ngaySinhStr = ws.Cells[row, 3].Text.Trim(); // Cột C
                    string gioiTinh = ws.Cells[row, 4].Text.Trim(); // Cột D
                    string sdtHS = ws.Cells[row, 5].Text.Trim(); // Cột E - ✅ ĐỌC SĐT
                    string email = ws.Cells[row, 6].Text.Trim(); // Cột F - ✅ ĐỌC EMAIL
                    string trangThai = ws.Cells[row, 7].Text.Trim(); // Cột G

                    // Nếu tất cả các ô đều rỗng thì bỏ qua dòng này (không báo lỗi)
                    if (string.IsNullOrWhiteSpace(hoTen)
                        && string.IsNullOrWhiteSpace(ngaySinhStr)
                        && string.IsNullOrWhiteSpace(gioiTinh)
                        && string.IsNullOrWhiteSpace(sdtHS)
                        && string.IsNullOrWhiteSpace(email)
                        && string.IsNullOrWhiteSpace(trangThai))
                    {
                        continue;
                    }

                    // Kiểm tra dữ liệu bắt buộc
                    if (string.IsNullOrWhiteSpace(hoTen))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Thiếu họ tên");
                        errorCount++;
                        continue;
                    }

                    // ✅ Parse ngày sinh với nhiều format khác nhau hoặc dạng số serial Excel
                    DateTime ngaySinh = DateTime.MinValue;
                    bool parsedDate = false;
                    // Nếu ô là số (Excel lưu ngày tháng dạng serial)
                    var cellNgaySinh = ws.Cells[row, 3];
                    if (cellNgaySinh.Value != null && double.TryParse(cellNgaySinh.Value.ToString(), out double serialValue))
                    {
                        try
                        {
                            ngaySinh = DateTime.FromOADate(serialValue);
                            parsedDate = true;
                        }
                        catch { /* Nếu lỗi thì thử tiếp các cách khác */ }
                    }
                    if (!parsedDate)
                    {
                        // Thử các format phổ biến
                        string[] dateFormats = {
                            "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy",
                            "yyyy-MM-dd", "dd/MM/yy", "d/M/yy"
                        };
                        foreach (string format in dateFormats)
                        {
                            if (DateTime.TryParseExact(ngaySinhStr, format,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out ngaySinh))
                            {
                                parsedDate = true;
                                break;
                            }
                        }
                    }
                    // Nếu vẫn chưa parse được, thử parse tự động
                    if (!parsedDate && DateTime.TryParse(ngaySinhStr, out ngaySinh))
                    {
                        parsedDate = true;
                    }
                    if (!parsedDate)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Ngày sinh không hợp lệ ({ngaySinhStr})");
                        errorCount++;
                        continue;
                    }

                    // ✅ KIỂM TRA TRÙNG SĐT/EMAIL TRONG CÙNG FILE EXCEL
                    if (!string.IsNullOrWhiteSpace(sdtHS))
                    {
                        if (sdtDaNhap.Contains(sdtHS))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Số điện thoại '{sdtHS}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            continue;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        if (emailDaNhap.Contains(email))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Email '{email}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            continue;
                        }
                    }

                    // Tạo DTO và thêm vào DB
                    HocSinhDTO hs = new HocSinhDTO
                    {
                        HoTen = hoTen,
                        NgaySinh = ngaySinh,
                        GioiTinh = gioiTinh,
                        SdtHS = sdtHS,
                        Email = email,
                        TrangThai = string.IsNullOrWhiteSpace(trangThai) ? "Đang học" : trangThai,
                        TenDangNhap = null
                    };

                    int newMaHS = hocSinhBLL.AddHocSinh(hs);
                    if (newMaHS > 0)
                    {
                        hs.MaHS = newMaHS;
                        danhSachHocSinhFull.Add(hs);
                        string username = $"HS{newMaHS:D3}";
                        if (!nguoiDungBLL.CheckTenDangNhapExists(username))
                        {
                            var nguoiDung = new NguoiDungDTO
                            {
                                TenDangNhap = username,
                                MatKhau = "123456",
                                VaiTro = "HocSinh"
                            };
                            nguoiDungBLL.AddNguoiDungNoCheck(nguoiDung);
                        }
                        successCount++;
                    }
                    else
                    {
                        errors.AppendLine($"Dòng {row - 1}: Không thể thêm học sinh {hoTen}");
                        errorCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                }
            }

            // Hiển thị kết quả
            if (errorCount > 0)
            {
                MessageBox.Show($"Nhập Học Sinh:\n- Thêm mới: {successCount}\n- Lỗi: {errorCount}\n\nChi tiết lỗi:\n{errors}",
                    "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (successCount > 0)
            {
                MessageBox.Show($"✅ Nhập Học Sinh thành công!\n- Đã thêm mới: {successCount} học sinh",
                    "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ℹ️ Không có học sinh mới nào được thêm vào.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // ✅ ĐÃ XÓA: ImportHocSinhFromWorksheetChuyenTruongWithBinding - Chuyển logic sang PhanLop.cs

        /// <summary>
        /// Nhập dữ liệu Học Sinh từ Worksheet CHỈ cho học sinh có trạng thái "Đang học" và thêm vào bindingList/table
        /// </summary>
        private Dictionary<string, (int maHS, int excelRow)> ImportHocSinhFromWorksheetDangHocWithBinding(ExcelWorksheet ws)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            int errorCount = 0;
            int successCount = 0;
            int skippedCount = 0;
            StringBuilder errors = new StringBuilder();
            StringBuilder skipped = new StringBuilder();
            // ✅ Dictionary để track học sinh thành công: tên học sinh -> (mã học sinh, dòng Excel)
            Dictionary<string, (int maHS, int excelRow)> hocSinhThanhCong = new Dictionary<string, (int, int)>();
            // ✅ Track SĐT và Email đã nhập trong file Excel để kiểm tra trùng trong cùng file
            HashSet<string> sdtDaNhap = new HashSet<string>();
            HashSet<string> emailDaNhap = new HashSet<string>();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    // Đọc dữ liệu từ các cột
                    string hoTen = ws.Cells[row, 2].Text.Trim();
                    string ngaySinhStr = ws.Cells[row, 3].Text.Trim();
                    string gioiTinh = ws.Cells[row, 4].Text.Trim();
                    string sdtHS = ws.Cells[row, 5].Text.Trim();
                    string email = ws.Cells[row, 6].Text.Trim();
                    string trangThai = ws.Cells[row, 7].Text.Trim();

                    // Bỏ qua dòng trống
                    if (string.IsNullOrWhiteSpace(hoTen)
                        && string.IsNullOrWhiteSpace(ngaySinhStr)
                        && string.IsNullOrWhiteSpace(gioiTinh)
                        && string.IsNullOrWhiteSpace(sdtHS)
                        && string.IsNullOrWhiteSpace(email)
                        && string.IsNullOrWhiteSpace(trangThai))
                    {
                        continue;
                    }

                    // ✅ CHỈ THÊM HỌC SINH CÓ TRẠNG THÁI "ĐANG HỌC"
                    if (string.IsNullOrWhiteSpace(trangThai) || trangThai.Trim() != "Đang học")
                    {
                        skipped.AppendLine($"Dòng {row - 1}: {hoTen} - Bỏ qua (Trạng thái: '{trangThai}' - Chỉ nhận 'Đang học')");
                        skippedCount++;
                        continue;
                    }

                    // Validate dữ liệu
                    if (string.IsNullOrWhiteSpace(hoTen))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Thiếu họ tên");
                        errorCount++;
                        continue;
                    }

                    // Parse ngày sinh
                    DateTime ngaySinh = DateTime.MinValue;
                    bool parsedDate = false;
                    var cellNgaySinh = ws.Cells[row, 3];
                    if (cellNgaySinh.Value != null && double.TryParse(cellNgaySinh.Value.ToString(), out double serialValue))
                    {
                        try
                        {
                            ngaySinh = DateTime.FromOADate(serialValue);
                            parsedDate = true;
                        }
                        catch { }
                    }
                    if (!parsedDate)
                    {
                        string[] dateFormats = {
                            "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy",
                            "yyyy-MM-dd", "dd/MM/yy", "d/M/yy"
                        };
                        foreach (string format in dateFormats)
                        {
                            if (DateTime.TryParseExact(ngaySinhStr, format,
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out ngaySinh))
                            {
                                parsedDate = true;
                                break;
                            }
                        }
                    }
                    if (!parsedDate && DateTime.TryParse(ngaySinhStr, out ngaySinh))
                    {
                        parsedDate = true;
                    }
                    if (!parsedDate)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Ngày sinh không hợp lệ ({ngaySinhStr})");
                        errorCount++;
                        continue;
                    }

                    // Validate giới tính
                    if (!string.IsNullOrWhiteSpace(gioiTinh) && gioiTinh != "Nam" && gioiTinh != "Nữ")
                    {
                        errors.AppendLine($"Dòng {row - 1}: Giới tính không hợp lệ ({gioiTinh})");
                        errorCount++;
                        continue;
                    }

                    // Validate email
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        try
                        {
                            var emailAddr = new System.Net.Mail.MailAddress(email);
                        }
                        catch
                        {
                            errors.AppendLine($"Dòng {row - 1}: Email không hợp lệ ({email})");
                            errorCount++;
                            continue;
                        }
                    }

                    // Validate SĐT
                    if (!string.IsNullOrWhiteSpace(sdtHS) && !System.Text.RegularExpressions.Regex.IsMatch(sdtHS, @"^\d+$"))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Số điện thoại không hợp lệ ({sdtHS})");
                        errorCount++;
                        continue;
                    }

                    // ✅ KIỂM TRA TRÙNG SĐT/EMAIL TRONG CÙNG FILE EXCEL
                    if (!string.IsNullOrWhiteSpace(sdtHS))
                    {
                        if (sdtDaNhap.Contains(sdtHS))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Số điện thoại '{sdtHS}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            continue;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        if (emailDaNhap.Contains(email))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Email '{email}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            continue;
                        }
                    }

                    // Tạo DTO và thêm vào DB
                    HocSinhDTO hs = new HocSinhDTO
                    {
                        HoTen = hoTen,
                        NgaySinh = ngaySinh,
                        GioiTinh = gioiTinh,
                        SdtHS = sdtHS,
                        Email = email,
                        TrangThai = "Đang học",
                        TenDangNhap = null
                    };

                    int newMaHS = hocSinhBLL.AddHocSinh(hs);
                    if (newMaHS > 0)
                    {
                        hs.MaHS = newMaHS;
                        
                        // ✅ Đánh dấu SĐT và Email đã nhập thành công
                        if (!string.IsNullOrWhiteSpace(sdtHS))
                            sdtDaNhap.Add(sdtHS);
                        if (!string.IsNullOrWhiteSpace(email))
                            emailDaNhap.Add(email);
                        
                        // ✅ Thêm vào danh sách full
                        danhSachHocSinhFull.Add(hs);
                        
                        // ✅ Thêm vào danhSachHocSinhFiltered nếu đang hiển thị học sinh
                        if (isShowingHocSinh)
                        {
                            danhSachHocSinhFiltered.Add(hs);
                        }

                        // ✅ Tạo tài khoản
                        string username = $"HS{newMaHS:D3}";
                        if (!nguoiDungBLL.CheckTenDangNhapExists(username))
                        {
                            var nguoiDung = new NguoiDungDTO
                            {
                                TenDangNhap = username,
                                MatKhau = "123456",
                                VaiTro = "HocSinh"
                            };
                            nguoiDungBLL.AddNguoiDungNoCheck(nguoiDung);
                        }

                        // ✅ Thêm vào bindingList và table nếu học sinh này nằm trong trang hiện tại
                        if (isShowingHocSinh)
                        {
                            // Kiểm tra xem học sinh này có nằm trong trang hiện tại không
                            int totalRecords = danhSachHocSinhFiltered.Count;
                            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizeHocSinh);
                            int startIndex = (currentPageHocSinh - 1) * pageSizeHocSinh;
                            int endIndex = startIndex + pageSizeHocSinh;
                            int currentIndex = danhSachHocSinhFiltered.Count - 1; // Index của học sinh vừa thêm

                            if (currentIndex >= startIndex && currentIndex < endIndex)
                            {
                                bindingListHocSinh.Add(hs);
                                tableHocSinh.Rows.Add(hs.MaHS, hs.HoTen, hs.NgaySinh.ToString("dd/MM/yyyy"),
                                                     hs.GioiTinh, hs.SdtHS ?? "", hs.Email ?? "", hs.TrangThai, "");
                            }
                        }

                        // ✅ Lưu học sinh thành công vào Dictionary
                        hocSinhThanhCong[hoTen.Trim()] = (newMaHS, row);
                        successCount++;
                    }
                    else
                    {
                        errors.AppendLine($"Dòng {row - 1}: Không thể thêm học sinh {hoTen}");
                        errorCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                }
            }

            // Hiển thị kết quả
            StringBuilder resultMessage = new StringBuilder();
            if (successCount > 0)
            {
                resultMessage.AppendLine($"✅ Đã thêm mới: {successCount} học sinh đang học");
            }
            if (skippedCount > 0)
            {
                resultMessage.AppendLine($"⚠️ Đã bỏ qua: {skippedCount} học sinh (không phải trạng thái 'Đang học')");
            }
            if (errorCount > 0)
            {
                resultMessage.AppendLine($"❌ Lỗi: {errorCount} học sinh");
            }

            if (errorCount > 0 || skippedCount > 0)
            {
                string detailMessage = resultMessage.ToString();
                if (skippedCount > 0 && skipped.Length > 0)
                {
                    detailMessage += $"\n\nChi tiết bỏ qua:\n{skipped}";
                }
                if (errorCount > 0 && errors.Length > 0)
                {
                    detailMessage += $"\n\nChi tiết lỗi:\n{errors}";
                }
                MessageBox.Show(detailMessage,
                    "Kết quả nhập học sinh đang học", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (successCount > 0)
            {
                // Không hiển thị message box nếu thành công hoàn toàn (đã có message box ở hàm gọi)
            }

            // ✅ Trả về Dictionary học sinh thành công
            return hocSinhThanhCong;
        }

        /// <summary>
        /// Nhập dữ liệu Phụ Huynh từ Worksheet
        /// </summary>
        private void ImportPhuHuynhFromWorksheet(ExcelWorksheet ws)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            if (rowCount < 2) return;

            int successCount = 0;
            int skippedCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();
            // ✅ Track SĐT và Email đã nhập trong file Excel để kiểm tra trùng trong cùng file
            HashSet<string> sdtDaNhap = new HashSet<string>();
            HashSet<string> emailDaNhap = new HashSet<string>();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    // Mapping đúng: Họ và Tên = cột 2 (B), SĐT = cột 3 (C), Email = cột 4 (D), Địa chỉ = cột 5 (E)
                    string hoTen = ws.Cells[row, 2].Text.Trim();
                    string sdt = ws.Cells[row, 3].Text.Trim();
                    string email = ws.Cells[row, 4].Text.Trim();
                    string diaChi = ws.Cells[row, 5].Text.Trim();

                    // Bỏ qua dòng trống hoàn toàn
                    if (string.IsNullOrWhiteSpace(hoTen)
                        && string.IsNullOrWhiteSpace(sdt)
                        && string.IsNullOrWhiteSpace(email)
                        && string.IsNullOrWhiteSpace(diaChi))
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(hoTen))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Thiếu họ tên");
                        errorCount++;
                        continue;
                    }

                    // ✅ KIỂM TRA TRÙNG SĐT/EMAIL TRONG CÙNG FILE EXCEL
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        if (sdtDaNhap.Contains(sdt))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Số điện thoại '{sdt}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            continue;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        if (emailDaNhap.Contains(email))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Email '{email}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            continue;
                        }
                    }

                    PhuHuynhDTO ph = new PhuHuynhDTO
                    {
                        HoTen = hoTen,
                        SoDienThoai = sdt,
                        Email = email,
                        DiaChi = diaChi
                    };

                    // ✅ Kiểm tra xem phụ huynh đã tồn tại trong DB không (kiểm tra cả SĐT và Email)
                    PhuHuynhDTO existing = null;
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        try { existing = phuHuynhBLL.GetPhuHuynhBySdt(sdt); } catch { existing = null; }
                    }
                    // Nếu không tìm thấy qua SĐT, kiểm tra qua Email
                    if (existing == null && !string.IsNullOrWhiteSpace(email))
                    {
                        try 
                        { 
                            var danhSachPH = phuHuynhBLL.GetAllPhuHuynh();
                            existing = danhSachPH.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.Email) && p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                        } 
                        catch { existing = null; }
                    }

                    if (existing != null)
                    {
                        // ✅ Đã tồn tại trong DB: bỏ qua (không tính là thành công hay lỗi)
                        skippedCount++;
                        // Đảm bảo danh sách in-memory có bản ghi này
                        if (!danhSachPhuHuynhFull.Any(p => p.MaPhuHuynh == existing.MaPhuHuynh))
                        {
                            danhSachPhuHuynhFull.Add(existing);
                        }
                        // ✅ Đánh dấu SĐT và Email đã xử lý (dù là bỏ qua)
                        if (!string.IsNullOrWhiteSpace(sdt))
                            sdtDaNhap.Add(sdt);
                        if (!string.IsNullOrWhiteSpace(email))
                            emailDaNhap.Add(email);
                    }
                    else
                    {
                        // Chưa tồn tại: thử thêm mới
                        try
                        {
                            bool success = phuHuynhBLL.AddPhuHuynh(ph);
                            if (success)
                            {
                                successCount++;
                                // ✅ Đánh dấu SĐT và Email đã nhập thành công
                                if (!string.IsNullOrWhiteSpace(sdt))
                                    sdtDaNhap.Add(sdt);
                                if (!string.IsNullOrWhiteSpace(email))
                                    emailDaNhap.Add(email);
                                
                                // Làm mới danh sách toàn cục để có MaPhuHuynh mới
                                try { danhSachPhuHuynhFull = phuHuynhBLL.GetAllPhuHuynh(); } catch { /* ignore */ }
                            }
                            else
                            {
                                errors.AppendLine($"Dòng {row - 1}: Không thể thêm phụ huynh {hoTen}");
                                errorCount++;
                            }
                        }
                        catch (ArgumentException vex)
                        {
                            errors.AppendLine($"Dòng {row - 1}: {vex.Message}");
                            errorCount++;
                        }
                        catch (Exception ex)
                        {
                            errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                            errorCount++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                }
            }

            // Thông báo kết quả chỉ 1 lần ở cuối hàm
            if (errorCount > 0)
            {
                MessageBox.Show($"Nhập Phụ Huynh:\n- Thêm mới: {successCount}\n- Bỏ qua (đã tồn tại): {skippedCount}\n- Lỗi: {errorCount}\n\nChi tiết lỗi:\n{errors}",
                    "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (successCount > 0)
            {
                string msg = $"✅ Nhập Phụ Huynh thành công!\n- Đã thêm mới: {successCount} phụ huynh.";
                if (skippedCount > 0)
                    msg += skippedCount == 1
                        ? "\n- 1 phụ huynh trong file đã tồn tại nên không thêm lại."
                        : $"\n- {skippedCount} phụ huynh trong file đã tồn tại nên không thêm lại.";
                MessageBox.Show(msg, "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (skippedCount > 0)
            {
                string msg = skippedCount == 1
                    ? "Phụ huynh này đã tồn tại trong hệ thống. Không có dữ liệu mới được thêm."
                    : $"Tất cả {skippedCount} phụ huynh trong file đã tồn tại trong hệ thống. Không có dữ liệu mới được thêm.";
                MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Nhập dữ liệu Mối Quan Hệ từ Worksheet
        /// LƯU Ý: Phải nhập sau khi đã có Học Sinh và Phụ Huynh
        /// </summary>
        private void ImportMoiQuanHeFromWorksheet(ExcelWorksheet ws)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            if (rowCount < 2) return;

            int successCount = 0;
            int skippedCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();
            var warnings = new StringBuilder();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string tenHS = ws.Cells[row, 1].Text.Trim();
                    string tenPH = ws.Cells[row, 2].Text.Trim();
                    string moiQuanHe = ws.Cells[row, 3].Text.Trim();

                    // Bỏ qua dòng trống hoàn toàn
                    if (string.IsNullOrWhiteSpace(tenHS)
                        && string.IsNullOrWhiteSpace(tenPH)
                        && string.IsNullOrWhiteSpace(moiQuanHe))
                    {
                        continue;
                    }

                    // Validate mối quan hệ hợp lệ
                    if (moiQuanHe != "Cha" && moiQuanHe != "Mẹ" && moiQuanHe != "Ông" &&
                        moiQuanHe != "Bà" && moiQuanHe != "Người giám hộ")
                    {
                        errors.AppendLine($"Dòng {row - 1}: Mối quan hệ không hợp lệ ({moiQuanHe}). Chỉ chấp nhận: Cha, Mẹ, Ông, Bà, Người giám hộ");
                        errorCount++;
                        continue;
                    }

                    // ✅ Tìm học sinh theo tên
                    var danhSachHSTrung = danhSachHocSinhFull.Where(h => h.HoTen.Equals(tenHS, StringComparison.OrdinalIgnoreCase)).ToList();
                    var danhSachPHTrung = danhSachPhuHuynhFull.Where(p => p.HoTen.Equals(tenPH, StringComparison.OrdinalIgnoreCase)).ToList();

                    HocSinhDTO hs = null;
                    PhuHuynhDTO ph = null;

                    // Xử lý học sinh
                    if (danhSachHSTrung.Count == 0)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Không tìm thấy học sinh '{tenHS}'");
                        errorCount++;
                        continue;
                    }
                    else if (danhSachHSTrung.Count == 1)
                    {
                        hs = danhSachHSTrung[0];
                    }
                    else
                    {
                        // ⚠️ TRÙNG TÊN - Lấy người đầu tiên + cảnh báo
                        hs = danhSachHSTrung[0];
                        string danhSachHS = string.Join("\n  ", danhSachHSTrung.Select(h =>
                            $"- MaHS {h.MaHS} (Ngày sinh: {h.NgaySinh:dd/MM/yyyy}, SĐT: {h.SdtHS})"));
                        warnings.AppendLine($"⚠️ Dòng {row - 1}: Có {danhSachHSTrung.Count} học sinh tên '{tenHS}':\n  {danhSachHS}\n  → Đã chọn MaHS {hs.MaHS}");
                    }

                    // Xử lý phụ huynh
                    if (danhSachPHTrung.Count == 0)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Không tìm thấy phụ huynh '{tenPH}'");
                        errorCount++;
                        continue;
                    }
                    else if (danhSachPHTrung.Count == 1)
                    {
                        ph = danhSachPHTrung[0];
                    }
                    else
                    {
                        // ⚠️ TRÙNG TÊN - Lấy người đầu tiên + cảnh báo
                        ph = danhSachPHTrung[0];
                        string danhSachPH = string.Join("\n  ", danhSachPHTrung.Select(p =>
                            $"- MaPH {p.MaPhuHuynh} (SĐT: {p.SoDienThoai}, Email: {p.Email})"));
                        warnings.AppendLine($"⚠️ Dòng {row - 1}: Có {danhSachPHTrung.Count} phụ huynh tên '{tenPH}':\n  {danhSachPH}\n  → Đã chọn MaPH {ph.MaPhuHuynh}");
                    }

                    // Thêm mối quan hệ
                    bool success = hocSinhPhuHuynhBLL.AddQuanHe(hs.MaHS, ph.MaPhuHuynh, moiQuanHe);
                    if (success)
                    {
                        successCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ Hiển thị kết quả với cảnh báo trùng tên (nếu có)
            StringBuilder result = new StringBuilder();
            if (errorCount > 0)
            {
                result.AppendLine($"Nhập Mối Quan Hệ:");
                result.AppendLine($"- Thêm mới: {successCount}");
                result.AppendLine($"- Bỏ qua (đã tồn tại): {skippedCount}");
                result.AppendLine($"- Lỗi: {errorCount}");
                result.AppendLine();
                result.AppendLine("Chi tiết lỗi:");
                result.Append(errors);
                if (warnings.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                    result.AppendLine("⚠️ CẢNH BÁO TRÙNG TÊN:");
                    result.Append(warnings);
                }
                MessageBox.Show(result.ToString(), "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (successCount > 0 || skippedCount > 0)
            {
                result.AppendLine($"✅ Nhập Mối Quan Hệ:");
                if (successCount > 0)
                    result.AppendLine($"- Thêm mới: {successCount}");
                if (skippedCount > 0)
                    result.AppendLine($"- Bỏ qua (đã tồn tại): {skippedCount}");
                if (warnings.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                    result.AppendLine("⚠️ CẢNH BÁO TRÙNG TÊN:");
                    result.Append(warnings);
                    result.AppendLine();
                    result.AppendLine("💡 Vui lòng kiểm tra lại trong database để đảm bảo đúng người!");
                    MessageBox.Show(result.ToString(), "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(result.ToString(), "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // ✅ ĐÃ XÓA: ImportPhuHuynhFromWorksheetChuyenTruong - Chuyển logic sang PhanLop.cs
        // ✅ ĐÃ XÓA: ImportMoiQuanHeFromWorksheetChuyenTruong - Chuyển logic sang PhanLop.cs

        /// <summary>
        /// Nhập dữ liệu Phụ Huynh từ Worksheet CHỈ cho học sinh đã nhập thành công (trạng thái "Đang học")
        /// Nếu phụ huynh lỗi thì rollback học sinh
        /// </summary>
        private Dictionary<string, (int maPH, int excelRow)> ImportPhuHuynhFromWorksheetDangHoc(
            ExcelWorksheet ws, 
            Dictionary<string, (int maHS, int excelRow)> hocSinhThanhCong)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            if (rowCount < 2) return new Dictionary<string, (int, int)>();

            int successCount = 0;
            int skippedCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();
            var warnings = new StringBuilder();
            var skipped = new StringBuilder();
            Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong = new Dictionary<string, (int, int)>();
            List<int> hocSinhCanRollback = new List<int>();
            HashSet<string> sdtDaNhap = new HashSet<string>();
            HashSet<string> emailDaNhap = new HashSet<string>();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string hoTen = ws.Cells[row, 2].Text.Trim();
                    string sdt = ws.Cells[row, 3].Text.Trim();
                    string email = ws.Cells[row, 4].Text.Trim();
                    string diaChi = ws.Cells[row, 5].Text.Trim();

                    if (string.IsNullOrWhiteSpace(hoTen)
                        && string.IsNullOrWhiteSpace(sdt)
                        && string.IsNullOrWhiteSpace(email)
                        && string.IsNullOrWhiteSpace(diaChi))
                    {
                        continue;
                    }

                    // ✅ KIỂM TRA: Chỉ nhập phụ huynh nếu có học sinh tương ứng ở cùng dòng Excel
                    bool coHocSinhTuongUng = false;
                    int maHSTuongUng = 0;
                    foreach (var kvp in hocSinhThanhCong)
                    {
                        if (kvp.Value.excelRow == row)
                        {
                            coHocSinhTuongUng = true;
                            maHSTuongUng = kvp.Value.maHS;
                            break;
                        }
                    }

                    if (!coHocSinhTuongUng)
                    {
                        skipped.AppendLine($"Dòng {row - 1}: {hoTen} - Bỏ qua (Không có học sinh tương ứng ở dòng {row - 1})");
                        skippedCount++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(hoTen))
                    {
                        errors.AppendLine($"Dòng {row - 1}: Thiếu họ tên phụ huynh");
                        errorCount++;
                        hocSinhCanRollback.Add(maHSTuongUng);
                        continue;
                    }

                    // ✅ KIỂM TRA TRÙNG SĐT/EMAIL TRONG CÙNG FILE EXCEL
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        if (sdtDaNhap.Contains(sdt))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Số điện thoại '{sdt}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            hocSinhCanRollback.Add(maHSTuongUng);
                            continue;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        if (emailDaNhap.Contains(email))
                        {
                            errors.AppendLine($"Dòng {row - 1}: Email '{email}' đã được sử dụng ở dòng trước đó trong file Excel");
                            errorCount++;
                            hocSinhCanRollback.Add(maHSTuongUng);
                            continue;
                        }
                    }

                    PhuHuynhDTO ph = new PhuHuynhDTO
                    {
                        HoTen = hoTen,
                        SoDienThoai = sdt,
                        Email = email,
                        DiaChi = diaChi
                    };

                    // ✅ Kiểm tra phụ huynh đã tồn tại trong DB không
                    PhuHuynhDTO existing = null;
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        try { existing = phuHuynhBLL.GetPhuHuynhBySdt(sdt); } catch { existing = null; }
                    }
                    if (existing == null && !string.IsNullOrWhiteSpace(email))
                    {
                        try 
                        { 
                            var danhSachPH = phuHuynhBLL.GetAllPhuHuynh();
                            existing = danhSachPH.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.Email) && p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                        } 
                        catch { existing = null; }
                    }

                    if (existing != null)
                    {
                        skippedCount++;
                        if (!danhSachPhuHuynhFull.Any(p => p.MaPhuHuynh == existing.MaPhuHuynh))
                        {
                            danhSachPhuHuynhFull.Add(existing);
                        }
                        phuHuynhThanhCong[hoTen.Trim()] = (existing.MaPhuHuynh, row);
                        if (!string.IsNullOrWhiteSpace(sdt))
                            sdtDaNhap.Add(sdt);
                        if (!string.IsNullOrWhiteSpace(email))
                            emailDaNhap.Add(email);
                    }
                    else
                    {
                        try
                        {
                            bool success = phuHuynhBLL.AddPhuHuynh(ph);
                            if (success)
                            {
                                if (!string.IsNullOrWhiteSpace(sdt))
                                    sdtDaNhap.Add(sdt);
                                if (!string.IsNullOrWhiteSpace(email))
                                    emailDaNhap.Add(email);
                                
                                try 
                                { 
                                    danhSachPhuHuynhFull = phuHuynhBLL.GetAllPhuHuynh();
                                    var phMoi = danhSachPhuHuynhFull.FirstOrDefault(p => 
                                        p.HoTen == hoTen && 
                                        (string.IsNullOrWhiteSpace(sdt) || p.SoDienThoai == sdt));
                                    if (phMoi != null)
                                    {
                                        phuHuynhThanhCong[hoTen.Trim()] = (phMoi.MaPhuHuynh, row);
                                        successCount++;
                                    }
                                } 
                                catch { }
                            }
                            else
                            {
                                errors.AppendLine($"Dòng {row - 1}: Không thể thêm phụ huynh {hoTen}");
                                errorCount++;
                                hocSinhCanRollback.Add(maHSTuongUng);
                            }
                        }
                        catch (ArgumentException vex)
                        {
                            errors.AppendLine($"Dòng {row - 1}: {vex.Message}");
                            errorCount++;
                            hocSinhCanRollback.Add(maHSTuongUng);
                        }
                        catch (Exception ex)
                        {
                            errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                            errorCount++;
                            hocSinhCanRollback.Add(maHSTuongUng);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                }
            }

            // ✅ ROLLBACK học sinh nếu phụ huynh lỗi
            foreach (int maHS in hocSinhCanRollback)
            {
                try
                {
                    hocSinhBLL.DeleteHocSinh(maHS);
                    var hsToRemove = danhSachHocSinhFull.FirstOrDefault(h => h.MaHS == maHS);
                    if (hsToRemove != null)
                    {
                        danhSachHocSinhFull.Remove(hsToRemove);
                        danhSachHocSinhFiltered.RemoveAll(h => h.MaHS == maHS);
                        bindingListHocSinh.Remove(hsToRemove);
                    }
                    string username = $"HS{maHS:D3}";
                    try { nguoiDungBLL.DeleteNguoiDung(username); } catch { }
                }
                catch { }
            }

            if (errorCount > 0 || skippedCount > 0 || hocSinhCanRollback.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine($"Nhập Phụ Huynh (đang học):");
                if (successCount > 0)
                    result.AppendLine($"- Thêm mới: {successCount}");
                if (skippedCount > 0)
                    result.AppendLine($"- Bỏ qua (đã tồn tại hoặc không có học sinh tương ứng): {skippedCount}");
                if (errorCount > 0)
                    result.AppendLine($"- Lỗi: {errorCount}");
                if (hocSinhCanRollback.Count > 0)
                    result.AppendLine($"- ⚠️ Đã rollback {hocSinhCanRollback.Count} học sinh do phụ huynh lỗi");
                if (skipped.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Chi tiết bỏ qua:");
                    result.Append(skipped);
                }
                if (errors.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Chi tiết lỗi:");
                    result.Append(errors);
                }
                MessageBox.Show(result.ToString(), "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return phuHuynhThanhCong;
        }

        /// <summary>
        /// Nhập dữ liệu Mối Quan Hệ từ Worksheet CHỈ cho học sinh đã nhập thành công (trạng thái "Đang học")
        /// </summary>
        private void ImportMoiQuanHeFromWorksheetDangHoc(
            ExcelWorksheet ws,
            Dictionary<string, (int maHS, int excelRow)> hocSinhThanhCong,
            Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            if (rowCount < 2) return;

            int successCount = 0;
            int skippedCount = 0;
            int errorCount = 0;
            var errors = new StringBuilder();
            var warnings = new StringBuilder();
            var skipped = new StringBuilder();

            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string tenHS = ws.Cells[row, 1].Text.Trim();
                    string tenPH = ws.Cells[row, 2].Text.Trim();
                    string moiQuanHe = ws.Cells[row, 3].Text.Trim();

                    if (string.IsNullOrWhiteSpace(tenHS)
                        && string.IsNullOrWhiteSpace(tenPH)
                        && string.IsNullOrWhiteSpace(moiQuanHe))
                    {
                        continue;
                    }

                    // ✅ KIỂM TRA: Chỉ nhập mối quan hệ nếu học sinh đã nhập thành công
                    if (!hocSinhThanhCong.ContainsKey(tenHS))
                    {
                        skipped.AppendLine($"Dòng {row - 1}: Bỏ qua (Học sinh '{tenHS}' không có trong danh sách nhập thành công)");
                        skippedCount++;
                        continue;
                    }

                    if (moiQuanHe != "Cha" && moiQuanHe != "Mẹ" && moiQuanHe != "Ông" &&
                        moiQuanHe != "Bà" && moiQuanHe != "Người giám hộ")
                    {
                        errors.AppendLine($"Dòng {row - 1}: Mối quan hệ không hợp lệ ({moiQuanHe})");
                        errorCount++;
                        continue;
                    }

                    int maHS = hocSinhThanhCong[tenHS].maHS;
                    var hs = danhSachHocSinhFull.FirstOrDefault(h => h.MaHS == maHS);
                    if (hs == null)
                    {
                        errors.AppendLine($"Dòng {row - 1}: Không tìm thấy học sinh '{tenHS}'");
                        errorCount++;
                        continue;
                    }

                    PhuHuynhDTO ph = null;
                    if (phuHuynhThanhCong.ContainsKey(tenPH))
                    {
                        int maPH = phuHuynhThanhCong[tenPH].maPH;
                        ph = danhSachPhuHuynhFull.FirstOrDefault(p => p.MaPhuHuynh == maPH);
                    }
                    
                    if (ph == null)
                    {
                        var danhSachPHTrung = danhSachPhuHuynhFull.Where(p => 
                            p.HoTen.Equals(tenPH, StringComparison.OrdinalIgnoreCase)).ToList();
                        
                        if (danhSachPHTrung.Count == 0)
                        {
                            errors.AppendLine($"Dòng {row - 1}: Không tìm thấy phụ huynh '{tenPH}'");
                            errorCount++;
                            continue;
                        }
                        else if (danhSachPHTrung.Count == 1)
                        {
                            ph = danhSachPHTrung[0];
                        }
                        else
                        {
                            ph = danhSachPHTrung[0];
                            warnings.AppendLine($"⚠️ Dòng {row - 1}: Có {danhSachPHTrung.Count} phụ huynh tên '{tenPH}' - Đã chọn MaPH {ph.MaPhuHuynh}");
                        }
                    }

                    bool success = hocSinhPhuHuynhBLL.AddQuanHe(maHS, ph.MaPhuHuynh, moiQuanHe);
                    if (success)
                    {
                        successCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row - 1}: {ex.Message}");
                    errorCount++;
                }
            }

            if (errorCount > 0 || skippedCount > 0)
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine($"Nhập Mối Quan Hệ (đang học):");
                if (successCount > 0)
                    result.AppendLine($"- Thêm mới: {successCount}");
                if (skippedCount > 0)
                    result.AppendLine($"- Bỏ qua (đã tồn tại hoặc không có học sinh tương ứng): {skippedCount}");
                if (errorCount > 0)
                    result.AppendLine($"- Lỗi: {errorCount}");
                if (skipped.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Chi tiết bỏ qua:");
                    result.Append(skipped);
                }
                if (errors.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Chi tiết lỗi:");
                    result.Append(errors);
                }
                if (warnings.Length > 0)
                {
                    result.AppendLine();
                    result.AppendLine("Cảnh báo:");
                    result.Append(warnings);
                }
                MessageBox.Show(result.ToString(), "Kết quả nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Phân trang và Tìm kiếm

        // ✅ Load danh sách lớp vào combobox
        private void LoadDanhSachLop()
        {
            try
            {
                cboLop.Items.Clear();
                
                // Thêm mục "Tất cả lớp"
                cboLop.Items.Add(new ComboBoxItem { Text = "Tất cả lớp", Value = null });

                // Lấy danh sách lớp từ database
                List<LopDTO> danhSachLop = lopHocBUS.DocDSLop();
                
                if (danhSachLop != null && danhSachLop.Count > 0)
                {
                    foreach (var lop in danhSachLop.OrderBy(l => l.tenLop))
                    {
                        cboLop.Items.Add(new ComboBoxItem { Text = lop.tenLop, Value = lop.maLop });
                    }
                }

                // Chọn "Tất cả lớp" mặc định
                if (cboLop.Items.Count > 0)
                {
                    cboLop.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Class hỗ trợ cho ComboBox
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        // ✅ Áp dụng lọc theo lớp
        private void ApplyLopFilter()
        {
            if (selectedMaLop == null)
            {
                // Nếu chọn "Tất cả lớp", hiển thị tất cả
                danhSachHocSinhFiltered = danhSachHocSinhFull.ToList();
            }
            else
            {
                // Lọc học sinh theo lớp được chọn
                danhSachHocSinhFiltered = new List<HocSinhDTO>();
                
                if (maHocKyHienTai > 0)
                {
                    foreach (var hs in danhSachHocSinhFull)
                    {
                        try
                        {
                            int maLop = phanLopBLL.GetLopByHocSinh(hs.MaHS, maHocKyHienTai);
                            if (maLop == selectedMaLop.Value)
                            {
                                danhSachHocSinhFiltered.Add(hs);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi khi lấy lớp của học sinh {hs.MaHS}: {ex.Message}");
                        }
                    }
                }
            }
        }

        // ✅ Xử lý khi chọn lớp trong combobox
        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingData) return; // Bỏ qua khi đang load dữ liệu ban đầu

            try
            {
                if (cboLop.SelectedItem is ComboBoxItem item)
                {
                    selectedMaLop = item.Value as int?;

                    // Áp dụng lọc
                    ApplyLopFilter();

                    // Reset về trang 1 và load lại
                    currentPageHocSinh = 1;
                    LoadPagedDataHocSinh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc theo lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Sự kiện tìm kiếm realtime trong TextBox (TẮT để dùng nút Tìm)
        // Nếu bạn muốn tìm kiếm realtime khi gõ, uncomment code bên dưới
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // ⚠️ COMMENT LẠI ĐỂ DÙNG NÚT TÌM THAY VÌ TÌM REALTIME
            // Nếu muốn tìm kiếm realtime (không cần nhấn nút), uncomment phần này:

            /*
            string keyword = txtTimKiem.Text.Trim().ToLower();
            
            if (string.IsNullOrWhiteSpace(keyword))
            {
                // Nếu ô tìm kiếm trống, hiển thị tất cả
                danhSachHocSinhFiltered = new List<HocSinhDTO>(danhSachHocSinhFull);
            }
            else
            {
                // Lọc theo Mã HS, Họ Tên
                danhSachHocSinhFiltered = danhSachHocSinhFull
                    .Where(hs => 
                        hs.MaHS.ToString().Contains(keyword) ||
                        hs.HoTen.ToLower().Contains(keyword)
                    )
                    .ToList();
            }
            
            // Reset về trang 1 và load lại
            currentPageHocSinh = 1;
            LoadPagedDataHocSinh();
            */
        }

        // ✅ Sự kiện nút "Tìm" - Tìm kiếm theo từ khóa trong TextBox
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Lấy từ khóa từ TextBox
                string keyword = txtTimKiem.Text.Trim().ToLower();

                if (isShowingHocSinh)
                {
                    // ===== TÌM KIẾM HỌC SINH =====
                    if (string.IsNullOrWhiteSpace(keyword))
                    {
                        danhSachHocSinhFiltered = new List<HocSinhDTO>(danhSachHocSinhFull);
                        MessageBox.Show(
                            "Chưa nhập từ khóa tìm kiếm!\nHiển thị tất cả học sinh.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        // ✅ Lọc theo từ khóa, nhưng vẫn giữ lọc theo lớp nếu có
                        var filteredByKeyword = danhSachHocSinhFull
                            .Where(hs =>
                                hs.MaHS.ToString().Contains(keyword) ||
                                hs.HoTen.ToLower().Contains(keyword)
                            )
                            .ToList();

                        // Nếu có lọc theo lớp, áp dụng thêm lọc lớp
                        if (selectedMaLop != null && maHocKyHienTai > 0)
                        {
                            danhSachHocSinhFiltered = new List<HocSinhDTO>();
                            foreach (var hs in filteredByKeyword)
                            {
                                try
                                {
                                    int maLop = phanLopBLL.GetLopByHocSinh(hs.MaHS, maHocKyHienTai);
                                    if (maLop == selectedMaLop.Value)
                                    {
                                        danhSachHocSinhFiltered.Add(hs);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Lỗi khi lấy lớp của học sinh {hs.MaHS}: {ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            danhSachHocSinhFiltered = filteredByKeyword;
                        }

                        if (danhSachHocSinhFiltered.Count == 0)
                        {
                            MessageBox.Show(
                                $"Không tìm thấy học sinh nào với từ khóa: \"{txtTimKiem.Text}\"",
                                "Kết quả tìm kiếm",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                            danhSachHocSinhFiltered = new List<HocSinhDTO>(danhSachHocSinhFull);
                        }
                        else
                        {
                            MessageBox.Show(
                                $"✅ Tìm thấy {danhSachHocSinhFiltered.Count} học sinh",
                                "Kết quả tìm kiếm",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                    }

                    currentPageHocSinh = 1;
                    LoadPagedDataHocSinh();
                }
                else
                {
                    // ===== TÌM KIẾM PHỤ HUYNH =====
                    if (string.IsNullOrWhiteSpace(keyword))
                    {
                        danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>(danhSachPhuHuynhFull);
                        MessageBox.Show(
                            "Chưa nhập từ khóa tìm kiếm!\nHiển thị tất cả phụ huynh.",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        danhSachPhuHuynhFiltered = danhSachPhuHuynhFull
                            .Where(ph =>
                                ph.MaPhuHuynh.ToString().Contains(keyword) ||
                                ph.HoTen.ToLower().Contains(keyword) ||
                                ph.SoDienThoai.Contains(keyword) ||
                                (ph.Email != null && ph.Email.ToLower().Contains(keyword))
                            )
                            .ToList();

                        if (danhSachPhuHuynhFiltered.Count == 0)
                        {
                            MessageBox.Show(
                                $"Không tìm thấy phụ huynh nào với từ khóa: \"{txtTimKiem.Text}\"",
                                "Kết quả tìm kiếm",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                            danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>(danhSachPhuHuynhFull);
                        }
                        else
                        {
                            MessageBox.Show(
                                $"✅ Tìm thấy {danhSachPhuHuynhFiltered.Count} phụ huynh",
                                "Kết quả tìm kiếm",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                    }

                    currentPagePhuHuynh = 1;
                    LoadPagedDataPhuHuynh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tìm kiếm: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // ✅ Sự kiện nút "Trang sau" (Hàm đúng theo Designer)
        private void btnTrangSau_Click_1(object sender, EventArgs e)
        {
            if (isShowingHocSinh)
            {
                // Phân trang Học Sinh
                int totalPages = (int)Math.Ceiling((double)danhSachHocSinhFiltered.Count / pageSizeHocSinh);
                if (currentPageHocSinh < totalPages)
                {
                    currentPageHocSinh++;
                    LoadPagedDataHocSinh();
                }
            }
            else
            {
                // Phân trang Phụ Huynh
                int totalPages = (int)Math.Ceiling((double)danhSachPhuHuynhFiltered.Count / pageSizePhuHuynh);
                if (currentPagePhuHuynh < totalPages)
                {
                    currentPagePhuHuynh++;
                    LoadPagedDataPhuHuynh();
                }
            }
        }

        // Sự kiện nút "Trang trước" (phân trang lùi)
        private void btnTrangTruoc_Click_1(object sender, EventArgs e)
        {
            if (isShowingHocSinh)
            {
                if (currentPageHocSinh > 1)
                {
                    currentPageHocSinh--;
                    LoadPagedDataHocSinh();
                }
            }
            else
            {
                if (currentPagePhuHuynh > 1)
                {
                    currentPagePhuHuynh--;
                    LoadPagedDataPhuHuynh();
                }
            }
        }



        #endregion

        private void btnPhanLopChuyenTruong_Click(object sender, EventArgs e)
        {
            // Mở form Phân lớp (chứa chức năng phân lớp chuyển trường)
            GUI.HocSinh.PhanLop frm = new GUI.HocSinh.PhanLop();
            frm.StartPosition = FormStartPosition.CenterScreen;

            // Hiển thị form dưới dạng Dialog và chờ kết quả
            DialogResult result = frm.ShowDialog();

            // Kiểm tra kết quả trả về từ form
            if (result == DialogResult.OK)
            {
                try
                {
                    // ✅ Reload lại TẤT CẢ dữ liệu từ DB để cập nhật số lượng chính xác
                    // (Sau khi chuyển trường thành công, có thể có học sinh mới, phụ huynh mới, mối quan hệ mới)
                    
                    // 1. Reload danh sách học sinh đầy đủ từ DB
                    LoadSampleDataHocSinh();
                    
                    // 2. Reload danh sách phụ huynh đầy đủ từ DB
                    LoadSampleDataPhuHuynh();
                    
                    // 3. Reload danh sách mối quan hệ đầy đủ từ DB
                    LoadSampleDataMoiQuanHe();
                    
                    // 4. Cập nhật lại các thẻ thống kê (số lượng học sinh, phụ huynh, etc.)
                    SetupHeaderAndStats();
                    
                    // 5. Cập nhật lại label phân trang
                    ForceUpdatePaginationLabel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ✅ ĐÃ XÓA: btnNhapExcelChuyenTruong_Click - Chuyển logic sang PhanLop.cs
        private void tablePhuHuynh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableHocSinh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ✅ Event handlers cho phân trang Phụ Huynh
        private void btnTrangSauPhuHuynh_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)danhSachPhuHuynhFiltered.Count / pageSizePhuHuynh);
            if (currentPagePhuHuynh < totalPages)
            {
                currentPagePhuHuynh++;
                LoadPagedDataPhuHuynh();
                UpdatePaginationLabelPhuHuynh(totalPages, danhSachPhuHuynhFiltered.Count);
                UpdatePaginationButtonsPhuHuynh(totalPages);
            }
        }

        private void btnTrangTruocPhuHuynh_Click(object sender, EventArgs e)
        {
            if (currentPagePhuHuynh > 1)
            {
                currentPagePhuHuynh--;
                LoadPagedDataPhuHuynh();
                int totalPages = (int)Math.Ceiling((double)danhSachPhuHuynhFiltered.Count / pageSizePhuHuynh);
                UpdatePaginationLabelPhuHuynh(totalPages, danhSachPhuHuynhFiltered.Count);
                UpdatePaginationButtonsPhuHuynh(totalPages);
            }
        }

        // ✅ Cập nhật label phân trang cho Phụ Huynh
        private void UpdatePaginationLabelPhuHuynh(int totalPages, int totalRecords)
        {
            if (lblTrangHienTaiPhuHuynh != null)
            {
                if (totalPages == 0)
                    lblTrangHienTaiPhuHuynh.Text = $"Trang 0/0 (0 phụ huynh)";
                else
                    lblTrangHienTaiPhuHuynh.Text = $"Trang {currentPagePhuHuynh}/{totalPages} ({totalRecords} phụ huynh)";
            }
        }

        // ✅ Cập nhật nút phân trang cho Phụ Huynh
        private void UpdatePaginationButtonsPhuHuynh(int totalPages)
        {
            if (btnTrangTruocPhuHuynh != null)
            {
                btnTrangTruocPhuHuynh.Enabled = (currentPagePhuHuynh > 1);
            }
            if (btnTrangSauPhuHuynh != null)
            {
                btnTrangSauPhuHuynh.Enabled = (currentPagePhuHuynh < totalPages);
            }
        }
    }
}

