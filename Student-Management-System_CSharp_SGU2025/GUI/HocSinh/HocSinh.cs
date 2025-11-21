using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO; // Cần cho FileInfo
using OfficeOpenXml; // Thư viện EPPlus
using OfficeOpenXml.Style; // Cần cho định dạng (tô màu, in đậm)

using Student_Management_System_CSharp_SGU2025.BUS; 
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.DAO; // ✅ Thêm để sử dụng NguoiDungDAO (nếu cần)
using Student_Management_System_CSharp_SGU2025.DAO; // ✅ Thêm để sử dụng NguoiDungDAO (nếu cần)

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

        // ✅ PHÂN TRANG - Biến quản lý
        private int currentPageHocSinh = 1; // Trang hiện tại
        private int pageSizeHocSinh = 50; // Số dòng mỗi trang
        private List<HocSinhDTO> danhSachHocSinhFiltered; // Danh sách sau khi tìm kiếm/lọc

        // ✅ PHÂN TRANG PHỤ HUYNH - Biến quản lý
        private int currentPagePhuHuynh = 1; // Trang hiện tại
        private int pageSizePhuHuynh = 50; // Số dòng mỗi trang
        private List<PhuHuynhDTO> danhSachPhuHuynhFiltered; // Danh sách sau khi tìm kiếm/lọc
        

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
            isLoadingData = true; // Bắt đầu load dữ liệu
            
            // --- Thiết lập giao diện ban đầu ---
            SetInitialView();

            // --- Cấu hình các bảng ---
            SetupTableHocSinh();
            SetupTablePhuHuynh();
            SetupTableMoiQuanHe(); 

            // --- Nạp dữ liệu mẫu ---
            LoadSampleDataHocSinh(); // ✅ Load trực tiếp, không cần FilterAndLoadHocSinh nữa
            LoadSampleDataHocSinh(); // ✅ Load trực tiếp, không cần FilterAndLoadHocSinh nữa
            LoadSampleDataPhuHuynh();
            LoadSampleDataMoiQuanHe(); 

            // --- Cấu hình Header và Thẻ Thống kê ---
            SetupHeaderAndStats();
            
            isLoadingData = false; // Kết thúc load dữ liệu
            
            // ✅ Force update label sau khi load xong (fix bug hiển thị 500 lần đầu)
            ForceUpdatePaginationLabel();
            
            // ✅ Force update label sau khi load xong (fix bug hiển thị 500 lần đầu)
            ForceUpdatePaginationLabel();
        }

        private void SetInitialView()
        {
            isShowingHocSinh = true;
            UpdateView();
        }

        private void UpdateView()
        {
            if (isShowingHocSinh)
            {
                tableHocSinh.Visible = true;
                btnThemHocSinh.Visible = true;
                tablePhuHuynh.Visible = false;
                btnThemPhuHuynh.Visible = false;
                btnPhuHuynh.Text = "Phụ Huynh";
                headerQuanLiHocSinh.lbHeader.Text = "Hồ sơ Học sinh"; 
                headerQuanLiHocSinh.lbGhiChu.Text = "Trang chủ / Hồ sơ học sinh";
                
                // ✅ Đổi placeholder TextBox
                if (txtTimKiem != null)
                {
                    txtTimKiem.PlaceholderText = "Tìm học sinh ...";
                }
                
                // ✅ Đổi placeholder TextBox
                if (txtTimKiem != null)
                {
                    txtTimKiem.PlaceholderText = "Tìm học sinh ...";
                }
            }
            else
            {
                tableHocSinh.Visible = false;
                btnThemHocSinh.Visible = false;
                tablePhuHuynh.Visible = true;
                btnThemPhuHuynh.Visible = true;
                btnPhuHuynh.Text = "Học Sinh";
                headerQuanLiHocSinh.lbHeader.Text = "Thông tin Phụ huynh"; 
                headerQuanLiHocSinh.lbGhiChu.Text = "Trang chủ / Phụ huynh";
                
                // ✅ Đổi placeholder TextBox
                if (txtTimKiem != null)
                {
                    txtTimKiem.PlaceholderText = "Tìm phụ huynh ...";
                }
                
                // ✅ Đổi placeholder TextBox
                if (txtTimKiem != null)
                {
                    txtTimKiem.PlaceholderText = "Tìm phụ huynh ...";
                }
            }
            
            // Reset bảng Mối Quan Hệ về hiển thị tất cả khi chuyển view
            LoadSampleDataMoiQuanHe();
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
            int tongHocSinhNghiHoc = tongHocSinh - tongHocSinhDangHoc;

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
            int tongLop = lopHocBUS.GetTotalLopHoc();
            if (tongLop > 0)
            {
                double siSoTB = Math.Round((double)tongHocSinh / tongLop, 1);
                statCardTongHocSinh.lbCardNote.Text = $"TB: {siSoTB} HS/lớp khi chia đều - Tổng lớp :{tongLop}";
            }

            statCardNam.lbCardTitle.Text = "Nam";
            statCardNam.lbCardValue.Text = tongHocSinhNam.ToString("N0");
            statCardNam.lbCardNote.Text = $"{phanTramNam}% tổng số";

            statCardNu.lbCardTitle.Text = "Nữ";
            statCardNu.lbCardValue.Text = tongHocSinhNu.ToString("N0");
            statCardNu.lbCardNote.Text = $"{phanTramNu}% tổng số";

            statCardDangHoc.lbCardTitle.Text = "Đang học";
            statCardDangHoc.lbCardValue.Text = tongHocSinhDangHoc.ToString("N0");
            
            statCardDangHoc.lbCardNote.Text = $"{tongHocSinhNghiHoc} nghỉ học";

            // --- Gán màu sắc (giữ nguyên) ---
            statCardTongHocSinh.lbCardNote.ForeColor = Color.FromArgb(22, 163, 74);
            statCardNam.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardNu.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardDangHoc.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardDangHoc.lbCardNote.ForeColor = Color.FromArgb(220, 38, 38);


        }

        #region Bảng Học Sinh

        private void SetupTableHocSinh()
        {
            // --- Xóa cột cũ và cấu hình chung ---
            tableHocSinh.Columns.Clear();
            ApplyBaseTableStyle(tableHocSinh); // Áp dụng style chung

            // --- Thêm cột mới (THÊM CỘT SDTHS VÀ EMAIL) ---
            // --- Thêm cột mới (THÊM CỘT SDTHS VÀ EMAIL) ---
            tableHocSinh.Columns.Add("MaHS", "Mã HS");
            tableHocSinh.Columns.Add("HoTen", "Họ và tên");
            tableHocSinh.Columns.Add("NgaySinh", "Ngày sinh");
            tableHocSinh.Columns.Add("GioiTinh", "Giới tính");
            tableHocSinh.Columns.Add("SDTHS", "SĐT"); // ✅ Thêm cột SĐT
            tableHocSinh.Columns.Add("Email", "Email"); // ✅ Thêm cột Email
            tableHocSinh.Columns.Add("SDTHS", "SĐT"); // ✅ Thêm cột SĐT
            tableHocSinh.Columns.Add("Email", "Email"); // ✅ Thêm cột Email
            tableHocSinh.Columns.Add("TrangThai", "Trạng thái");
            tableHocSinh.Columns.Add("ThaoTacHS", "Thao tác"); // <-- Cột thao tác mới

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tableHocSinh);
            tableHocSinh.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableHocSinh.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableHocSinh.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- Tùy chỉnh kích thước ---
            tableHocSinh.Columns["MaHS"].FillWeight = 8; tableHocSinh.Columns["MaHS"].MinimumWidth = 50;
            tableHocSinh.Columns["MaHS"].FillWeight = 8; tableHocSinh.Columns["MaHS"].MinimumWidth = 50;
            tableHocSinh.Columns["HoTen"].FillWeight = 25; tableHocSinh.Columns["HoTen"].MinimumWidth = 150;
            tableHocSinh.Columns["NgaySinh"].FillWeight = 12; tableHocSinh.Columns["NgaySinh"].MinimumWidth = 100;
            tableHocSinh.Columns["GioiTinh"].FillWeight = 10; tableHocSinh.Columns["GioiTinh"].MinimumWidth = 70;
            tableHocSinh.Columns["SDTHS"].FillWeight = 12; tableHocSinh.Columns["SDTHS"].MinimumWidth = 100;
            tableHocSinh.Columns["Email"].FillWeight = 18; tableHocSinh.Columns["Email"].MinimumWidth = 120;
            tableHocSinh.Columns["TrangThai"].FillWeight = 12; tableHocSinh.Columns["TrangThai"].MinimumWidth = 90;
            tableHocSinh.Columns["GioiTinh"].FillWeight = 10; tableHocSinh.Columns["GioiTinh"].MinimumWidth = 70;
            tableHocSinh.Columns["SDTHS"].FillWeight = 12; tableHocSinh.Columns["SDTHS"].MinimumWidth = 100;
            tableHocSinh.Columns["Email"].FillWeight = 18; tableHocSinh.Columns["Email"].MinimumWidth = 120;
            tableHocSinh.Columns["TrangThai"].FillWeight = 12; tableHocSinh.Columns["TrangThai"].MinimumWidth = 90;
            tableHocSinh.Columns["ThaoTacHS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableHocSinh.Columns["ThaoTacHS"].Width = 100; // Độ rộng cột thao tác

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
            
            // ✅ QUAN TRỌNG: Tạo list mới hoàn toàn để tránh reference cũ
            danhSachHocSinhFiltered = danhSachHocSinhFull.ToList();
            
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
            
            // ✅ Load tất cả từ DB
            danhSachHocSinhFull = hocSinhBLL.GetAllHocSinh();
            
            // ✅ QUAN TRỌNG: Tạo list mới hoàn toàn để tránh reference cũ
            danhSachHocSinhFiltered = danhSachHocSinhFull.ToList();
            
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

                // Thêm vào bảng
                foreach (HocSinhDTO hs in pagedData)
                {
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

                // Thêm vào bảng
                foreach (HocSinhDTO hs in pagedData)
                {
                    bindingListHocSinh.Add(hs);
                    tableHocSinh.Rows.Add(
                        hs.MaHS, 
                        hs.HoTen, 
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
                    tableHocSinh.Rows.Add(
                        hs.MaHS, 
                        hs.HoTen, 
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
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] ForceUpdatePaginationLabel: {ex.Message}");
            }
        }

        // Vẽ icon cho bảng Học Sinh
        private void tableHocSinh_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableHocSinh.Columns["ThaoTacHS"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                Image editIcon = Properties.Resources.repair; // Đổi icon nếu cần
                Image deleteIcon = Properties.Resources.bin; // Đổi icon nếu cần

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle editRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);

                e.Graphics.DrawImage(editIcon, editRect);
                e.Graphics.DrawImage(deleteIcon, deleteRect);

                e.Handled = true;
            }
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
            tablePhuHuynh.Columns.Add("ThaoTacPH", "Thao tác"); 

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tablePhuHuynh);
            tablePhuHuynh.Columns["HoTenPH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["DiaChi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- Tùy chỉnh kích thước ---
            tablePhuHuynh.Columns["MaPH"].FillWeight = 10; tablePhuHuynh.Columns["MaPH"].MinimumWidth = 50;
            tablePhuHuynh.Columns["HoTenPH"].FillWeight = 20; tablePhuHuynh.Columns["HoTenPH"].MinimumWidth = 90;
            tablePhuHuynh.Columns["Sdt"].FillWeight = 12; tablePhuHuynh.Columns["Sdt"].MinimumWidth = 80;
            tablePhuHuynh.Columns["Email"].FillWeight = 18; tablePhuHuynh.Columns["Email"].MinimumWidth = 100;
            tablePhuHuynh.Columns["DiaChi"].FillWeight = 25; tablePhuHuynh.Columns["DiaChi"].MinimumWidth = 150;
            tablePhuHuynh.Columns["Email"].FillWeight = 18; tablePhuHuynh.Columns["Email"].MinimumWidth = 100;
            tablePhuHuynh.Columns["DiaChi"].FillWeight = 25; tablePhuHuynh.Columns["DiaChi"].MinimumWidth = 150;
            tablePhuHuynh.Columns["ThaoTacPH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablePhuHuynh.Columns["ThaoTacPH"].Width = 80; 
            tablePhuHuynh.Columns["ThaoTacPH"].Width = 80; 

            // --- Gắn sự kiện ---
            tablePhuHuynh.CellPainting += tablePhuHuynh_CellPainting;
            tablePhuHuynh.CellClick += tablePhuHuynh_CellClick;
            tablePhuHuynh.SelectionChanged -= tablePhuHuynh_SelectionChanged;
            tablePhuHuynh.SelectionChanged -= tablePhuHuynh_SelectionChanged;
            tablePhuHuynh.SelectionChanged += tablePhuHuynh_SelectionChanged;
        }

        private void LoadSampleDataPhuHuynh()
        {
            tablePhuHuynh.Rows.Clear();
            danhSachPhuHuynhFull = phuHuynhBLL.GetAllPhuHuynh(); // ✅ Load tất cả từ DB
            danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>(danhSachPhuHuynhFull); // ✅ Copy sang filtered list
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
                    tablePhuHuynh.Rows.Add(ph.MaPhuHuynh, ph.HoTen, ph.SoDienThoai, 
                                          ph.Email, ph.DiaChi, "");
                }

                // ✅ Cập nhật label phân trang
                UpdatePaginationLabel(totalPages, totalRecords);
                
                // ✅ Enable/Disable nút
                UpdatePaginationButtons(totalPages);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu phụ huynh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    tablePhuHuynh.Rows.Add(ph.MaPhuHuynh, ph.HoTen, ph.SoDienThoai, 
                                          ph.Email, ph.DiaChi, "");
                }

                // ✅ Cập nhật label phân trang
                UpdatePaginationLabel(totalPages, totalRecords);
                
                // ✅ Enable/Disable nút
                UpdatePaginationButtons(totalPages);
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

                Image editIcon = Properties.Resources.repair;
                Image deleteIcon = Properties.Resources.bin;

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle editRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);

                e.Graphics.DrawImage(editIcon, editRect);
                e.Graphics.DrawImage(deleteIcon, deleteRect);

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
        private void HandleIconClick(Guna.UI2.WinForms.Guna2DataGridView dgv, int rowIndex, string idColumnName)
        {
            // Lấy ô thao tác dựa trên tên cột của từng bảng
            string thaoTacColName = "";
            if (dgv == tableHocSinh) thaoTacColName = "ThaoTacHS";
            else if (dgv == tablePhuHuynh) thaoTacColName = "ThaoTacPH";
            else return; // Bảng không xác định

            Rectangle cellBounds = dgv.GetCellDisplayRectangle(dgv.Columns[thaoTacColName].Index, rowIndex, false);
            Point clickPosInCell = dgv.PointToClient(Cursor.Position);
            int xClick = clickPosInCell.X - cellBounds.Left;

            int iconSize = 18;
            int spacing = 15;
            int totalWidth = iconSize * 2 + spacing;
            int startXInCell = (cellBounds.Width - totalWidth) / 2;

            int editIconEndX = startXInCell + iconSize;
            int deleteIconStartX = startXInCell + iconSize + spacing;
            int deleteIconEndX = deleteIconStartX + iconSize;

            // Lấy ID chính của dòng (HS hoặc PH)
            string idValueStr = dgv.Rows[rowIndex].Cells[idColumnName].Value?.ToString();

            // Xử lý Click Sửa
            if (xClick >= startXInCell && xClick < editIconEndX)
            {
                int maToEdit;
                if (int.TryParse(idValueStr, out maToEdit))
                {
                    if (dgv == tableHocSinh) // Nếu là bảng Học Sinh
                    {
                        // Mở form Chỉnh sửa Học Sinh, truyền Mã HS vào constructor
                        ChinhSuaHocSinh frmEditHS = new ChinhSuaHocSinh(maToEdit);
                        frmEditHS.StartPosition = FormStartPosition.CenterParent; // Hiện giữa form cha

                        // Hiển thị form và kiểm tra kết quả sau khi đóng
                        DialogResult result = frmEditHS.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            // ✅ Lấy học sinh đã cập nhật từ form
                            HocSinhDTO updatedHS = frmEditHS.UpdatedHocSinh;

                            if (updatedHS != null)
                            {
                                // ✅ Cập nhật trong BindingList
                                var hsInList = bindingListHocSinh.FirstOrDefault(hs => hs.MaHS == maToEdit);
                                if (hsInList != null)
                                {
                                    int index = bindingListHocSinh.IndexOf(hsInList);
                                    bindingListHocSinh[index] = updatedHS;
                                }

                                // ✅ Cập nhật trong Full list
                                var hsInFullList = danhSachHocSinhFull.FirstOrDefault(hs => hs.MaHS == maToEdit);
                                if (hsInFullList != null)
                                {
                                    int index = danhSachHocSinhFull.IndexOf(hsInFullList);
                                    danhSachHocSinhFull[index] = updatedHS;
                                }

                                // ✅ Cập nhật dòng trong bảng thay vì reload (THÊM CỘT SDTHS VÀ EMAIL)
                                // ✅ Cập nhật dòng trong bảng thay vì reload (THÊM CỘT SDTHS VÀ EMAIL)
                                dgv.Rows[rowIndex].SetValues(
                                    updatedHS.MaHS, 
                                    updatedHS.HoTen, 
                                    updatedHS.NgaySinh.ToString("dd/MM/yyyy"), 
                                    updatedHS.GioiTinh,
                                    updatedHS.SdtHS ?? "", // ✅ SĐT
                                    updatedHS.Email ?? "", // ✅ Email
                                    updatedHS.GioiTinh,
                                    updatedHS.SdtHS ?? "", // ✅ SĐT
                                    updatedHS.Email ?? "", // ✅ Email
                                    updatedHS.TrangThai, 
                                    ""
                                );
                            }

                            LoadSampleDataMoiQuanHe();
                            SetupHeaderAndStats();
                        }
                    }
                    else if (dgv == tablePhuHuynh) // Nếu là bảng Phụ Huynh
                    {
                        ChinhSuaPhuHuynh frmEditPH = new ChinhSuaPhuHuynh(maToEdit);
                        frmEditPH.StartPosition = FormStartPosition.CenterParent; // Hiện giữa form cha

                        // Hiển thị form và kiểm tra kết quả sau khi đóng
                        DialogResult result = frmEditPH.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            // ✅ Lấy phụ huynh đã cập nhật từ form
                            PhuHuynhDTO updatedPH = frmEditPH.UpdatedPhuHuynh;

                            if (updatedPH != null)
                            {
                                // ✅ Cập nhật trong BindingList
                                var phInList = bindingListPhuHuynh.FirstOrDefault(ph => ph.MaPhuHuynh == maToEdit);
                                if (phInList != null)
                                {
                                    int index = bindingListPhuHuynh.IndexOf(phInList);
                                    bindingListPhuHuynh[index] = updatedPH;
                                }

                                // ✅ Cập nhật trong Full list
                                var phInFullList = danhSachPhuHuynhFull.FirstOrDefault(ph => ph.MaPhuHuynh == maToEdit);
                                if (phInFullList != null)
                                {
                                    int index = danhSachPhuHuynhFull.IndexOf(phInFullList);
                                    danhSachPhuHuynhFull[index] = updatedPH;
                                }

                                // ✅ Cập nhật dòng trong bảng thay vì reload
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
            // Xử lý Click Xóa
            else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
            {
                if (dgv == tableHocSinh) // Xóa Học Sinh
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

                            if (deleteHSSuccess) // Chỉ cần kiểm tra xóa HS thành công
                            {
                                // Hiển thị thông báo trạng thái tài khoản (nếu có)
                                if (!string.IsNullOrWhiteSpace(accountStatusMsg))
                                {
                                    MessageBox.Show(accountStatusMsg, "Cập nhật tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                // ✅ Xóa khỏi BindingList và Full list thay vì reload
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

                                // ✅ Xóa dòng khỏi bảng thay vì reload
                                tableHocSinh.Rows.RemoveAt(rowIndex);

                                LoadSampleDataMoiQuanHe(); // Nạp lại bảng MQH
                                SetupHeaderAndStats();      // Cập nhật lại các thẻ thống kê

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
                else if (dgv == tablePhuHuynh) // Xóa Phụ Huynh
                {
                    if (MessageBox.Show($"Bạn có chắc muốn xóa phụ huynh {idValueStr}?\nTất cả mối quan hệ với học sinh liên quan cũng sẽ bị xóa.",
                                       "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int maPH;
                        if (int.TryParse(idValueStr, out maPH))
                        {
                            bool deleteQuanHeSuccess = hocSinhPhuHuynhBLL.DeleteQuanHeByPhuHuynh(maPH); // Xóa QH trước
                            bool deletePHSuccess = phuHuynhBLL.DeletePhuHuynh(maPH); // Xóa PH sau

                            if (deletePHSuccess)
                            {
                                // ✅ Xóa khỏi BindingList và Full list thay vì reload
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

                                // ✅ Xóa dòng khỏi bảng thay vì reload
                                tablePhuHuynh.Rows.RemoveAt(rowIndex);

                                LoadSampleDataMoiQuanHe(); // Nạp lại bảng MQH
                                SetupHeaderAndStats();      // Cập nhật lại các thẻ thống kê
                                
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

        // Hàm định dạng ô Trạng thái
        private void FormatStatusCell(DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold); // Hơi đậm hơn
            e.CellStyle.Padding = new Padding(5, 3, 5, 3); // Thêm padding nhẹ

            if (e.Value.ToString() == "Đang học")
            {
                e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52); // Đậm hơn
              
            }
            else // Nghỉ học hoặc trạng thái khác
            {
                e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27); // Đậm hơn
              
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
                        // ✅ Thêm trực tiếp vào BindingList thay vì load lại toàn bộ
                        bindingListHocSinh.Add(newHS);
                        danhSachHocSinhFull.Add(newHS);

                        // ✅ Thêm dòng mới vào bảng thay vì load lại toàn bộ (BỎ CỘT LỚP)
                        // ✅ Thêm dòng mới vào bảng thay vì load lại toàn bộ (BỎ CỘT LỚP)
                        tableHocSinh.Rows.Add(newHS.MaHS, newHS.HoTen, newHS.NgaySinh.ToString("dd/MM/yyyy"), 
                                             newHS.GioiTinh, newHS.TrangThai, "");
                                             newHS.GioiTinh, newHS.TrangThai, "");
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
                        // ✅ Thêm trực tiếp vào BindingList thay vì load lại toàn bộ
                        bindingListPhuHuynh.Add(newPH);
                        danhSachPhuHuynhFull.Add(newPH);

                        // ✅ Thêm dòng mới vào bảng thay vì load lại toàn bộ
                        tablePhuHuynh.Rows.Add(newPH.MaPhuHuynh, newPH.HoTen, newPH.SoDienThoai, 
                                              newPH.Email, newPH.DiaChi, "");
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
                // ✅ 1. Xuất TẤT CẢ Học Sinh từ danhSachHocSinhFull
                ExportHocSinhToWorksheet(package, "HocSinh");

                // ✅ 2. Xuất TẤT CẢ Phụ Huynh từ danhSachPhuHuynhFull
                ExportPhuHuynhToWorksheet(package, "PhuHuynh");
                // ✅ 2. Xuất TẤT CẢ Phụ Huynh từ danhSachPhuHuynhFull
                ExportPhuHuynhToWorksheet(package, "PhuHuynh");

                // ✅ 3. Xuất TẤT CẢ Mối Quan Hệ từ danhSachMoiQuanHe
                ExportMoiQuanHeToWorksheet(package, "MoiQuanHe");
                // ✅ 3. Xuất TẤT CẢ Mối Quan Hệ từ danhSachMoiQuanHe
                ExportMoiQuanHeToWorksheet(package, "MoiQuanHe");

                // Lưu file
                package.Save();
            }
        }

        /// <summary>
        /// ✅ Xuất TẤT CẢ Học Sinh từ danhSachHocSinhFull (không phải từ DataGridView)
        /// ✅ Xuất TẤT CẢ Học Sinh từ danhSachHocSinhFull (không phải từ DataGridView)
        /// </summary>
        private void ExportHocSinhToWorksheet(ExcelPackage package, string sheetName)
        private void ExportHocSinhToWorksheet(ExcelPackage package, string sheetName)
        {
            var ws = package.Workbook.Worksheets.Add(sheetName);
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
                if (hs.TrangThai == "Đang học")
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
                if (hs.TrangThai == "Đang học")
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
        // ✅ KHÔNG CẦN NỮA - BỎ COMBOBOX
        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không làm gì - ComboBox đã bị loại bỏ
            // Không làm gì - ComboBox đã bị loại bỏ
        }

        // ✅ KHÔNG CẦN NỮA - BỎ COMBOBOX
        // ✅ KHÔNG CẦN NỮA - BỎ COMBOBOX
        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không làm gì - ComboBox đã bị loại bỏ
            // Không làm gì - ComboBox đã bị loại bỏ
        }

        // ✅ KHÔNG CẦN NỮA - BỎ HÀM LỌC
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

                        ImportAllDataFromExcel(ofd.FileName);

                        // Reload lại dữ liệu
                        LoadSampleDataHocSinh();
                        LoadSampleDataPhuHuynh();
                        LoadSampleDataMoiQuanHe();
                        SetupHeaderAndStats();

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
        /// Nhập dữ liệu Học Sinh từ Worksheet (TenDangNhap để NULL)
        /// </summary>
        private void ImportHocSinhFromWorksheet(ExcelWorksheet ws)
        {
            int rowCount = ws.Dimension?.Rows ?? 0;
            int errorCount = 0;
            int successCount = 0;
            StringBuilder errors = new StringBuilder();
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
                        errors.AppendLine($"Dòng {row}: Thiếu họ tên");
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
                        errors.AppendLine($"Dòng {row}: Ngày sinh không hợp lệ ({ngaySinhStr})");
                        errorCount++;
                        continue;
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
                        errors.AppendLine($"Dòng {row}: Không thể thêm học sinh {hoTen}");
                        errorCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row}: {ex.Message}");
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
                        errors.AppendLine($"Dòng {row}: Thiếu họ tên");
                        errorCount++;
                        continue;
                    }

                    PhuHuynhDTO ph = new PhuHuynhDTO
                    {
                        HoTen = hoTen,
                        SoDienThoai = sdt,
                        Email = email,
                        DiaChi = diaChi
                    };

                    // Nếu có SĐT, kiểm tra xem phụ huynh đã tồn tại không
                    PhuHuynhDTO existing = null;
                    if (!string.IsNullOrWhiteSpace(sdt))
                    {
                        try { existing = phuHuynhBLL.GetPhuHuynhBySdt(sdt); } catch { existing = null; }
                    }

                    if (existing != null)
                    {
                        // ✅ Đã tồn tại: bỏ qua (không tính là thành công hay lỗi)
                        skippedCount++;
                        // Đảm bảo danh sách in-memory có bản ghi này
                        if (!danhSachPhuHuynhFull.Any(p => p.MaPhuHuynh == existing.MaPhuHuynh))
                        {
                            danhSachPhuHuynhFull.Add(existing);
                        }
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
                                // Làm mới danh sách toàn cục để có MaPhuHuynh mới
                                try { danhSachPhuHuynhFull = phuHuynhBLL.GetAllPhuHuynh(); } catch { /* ignore */ }
                            }
                            else
                            {
                                errors.AppendLine($"Dòng {row}: Không thể thêm phụ huynh {hoTen}");
                                errorCount++;
                            }
                        }
                        catch (ArgumentException vex)
                        {
                            errors.AppendLine($"Dòng {row}: {vex.Message}");
                            errorCount++;
                        }
                        catch (Exception ex)
                        {
                            errors.AppendLine($"Dòng {row}: {ex.Message}");
                            errorCount++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.AppendLine($"Dòng {row}: {ex.Message}");
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
                        errors.AppendLine($"Dòng {row}: Mối quan hệ không hợp lệ ({moiQuanHe}). Chỉ chấp nhận: Cha, Mẹ, Ông, Bà, Người giám hộ");
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
                        errors.AppendLine($"Dòng {row}: Không tìm thấy học sinh '{tenHS}'");
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
                        warnings.AppendLine($"⚠️ Dòng {row}: Có {danhSachHSTrung.Count} học sinh tên '{tenHS}':\n  {danhSachHS}\n  → Đã chọn MaHS {hs.MaHS}");
                    }

                    // Xử lý phụ huynh
                    if (danhSachPHTrung.Count == 0)
                    {
                        errors.AppendLine($"Dòng {row}: Không tìm thấy phụ huynh '{tenPH}'");
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
                        warnings.AppendLine($"⚠️ Dòng {row}: Có {danhSachPHTrung.Count} phụ huynh tên '{tenPH}':\n  {danhSachPH}\n  → Đã chọn MaPH {ph.MaPhuHuynh}");
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
                    errors.AppendLine($"Dòng {row}: {ex.Message}");
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

        #region Phân trang và Tìm kiếm

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
                        danhSachHocSinhFiltered = danhSachHocSinhFull
                            .Where(hs => 
                                hs.MaHS.ToString().Contains(keyword) ||
                                hs.HoTen.ToLower().Contains(keyword)
                            )
                            .ToList();
                        
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

        private void btnNhapExcel_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}


