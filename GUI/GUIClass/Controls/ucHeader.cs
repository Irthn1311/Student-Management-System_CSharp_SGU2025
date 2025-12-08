using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ucHeader : UserControl
    {
        // Event để thông báo khi cần chuyển màn hình
        public event Action<string> OnNavigationRequested;

        // Dictionary để map từ khóa tìm kiếm với tên chức năng
        private Dictionary<string, string> searchKeywords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Bảng tin / Dashboard
            { "bang tin", "BangTin" },
            { "bangtin", "BangTin" },
            { "dashboard", "BangTin" },
            { "trang chu", "BangTin" },
            { "trangchu", "BangTin" },
            
            // Học sinh
            { "hoc sinh", "HocSinh" },
            { "hocsinh", "HocSinh" },
            { "hs", "HocSinh" },
            { "danh sach hoc sinh", "HocSinh" },
            { "danhsachhocsinh", "HocSinh" },
            
            // Giáo viên
            { "giao vien", "GiaoVien" },
            { "giaovien", "GiaoVien" },
            { "gv", "GiaoVien" },
            { "danh sach giao vien", "GiaoVien" },
            { "danhsachgiaovien", "GiaoVien" },
            
            // Lớp học
            { "lop hoc", "LopHoc" },
            { "lophoc", "LopHoc" },
            { "lop", "LopHoc" },
            { "danh sach lop", "LopHoc" },
            { "danhsachlop", "LopHoc" },
            
            // Môn học
            { "mon hoc", "MonHoc" },
            { "monhoc", "MonHoc" },
            { "mon", "MonHoc" },
            { "danh sach mon hoc", "MonHoc" },
            
            // Điểm số
            { "diem so", "DiemSo" },
            { "diemso", "DiemSo" },
            { "diem", "DiemSo" },
            { "nhap diem", "DiemSo" },
            { "nhapdiem", "DiemSo" },
            
            // Xếp loại
            { "xep loai", "XepLoai" },
            { "xeploai", "XepLoai" },
            { "hoc luc", "XepLoai" },
            { "hocluc", "XepLoai" },
            
            // Hạnh kiểm
            { "hanh kiem", "HanhKiem" },
            { "hanhkiem", "HanhKiem" },
            { "danh gia hanh kiem", "HanhKiem" },
            
            // Khen thưởng / Đánh giá
            { "khen thuong", "DanhGia" },
            { "khenthuong", "DanhGia" },
            { "ky luat", "DanhGia" },
            { "kyluat", "DanhGia" },
            { "danh gia", "DanhGia" },
            { "danhgia", "DanhGia" },
            
            // Phân công giảng dạy
            { "phan cong", "PhanCong" },
            { "phancong", "PhanCong" },
            { "phan cong giang day", "PhanCong" },
            { "phanconggiangday", "PhanCong" },
            
            // Thời khóa biểu
            { "thoi khoa bieu", "ThoiKhoaBieu" },
            { "thoikhoabieu", "ThoiKhoaBieu" },
            { "tkb", "ThoiKhoaBieu" },
            { "lich hoc", "ThoiKhoaBieu" },
            
            // Thông báo
            { "thong bao", "ThongBao" },
            { "thongbao", "ThongBao" },
            { "tb", "ThongBao" },
            
            // Năm học
            { "nam hoc", "NamHoc" },
            { "namhoc", "NamHoc" },
            
            // Báo cáo
            { "bao cao", "BaoCao" },
            { "baocao", "BaoCao" },
            { "thong ke", "BaoCao" },
            { "thongke", "BaoCao" },
            
            // Tài khoản
            { "tai khoan", "TaiKhoan" },
            { "taikhoan", "TaiKhoan" },
            { "nguoi dung", "TaiKhoan" },
            { "nguoidung", "TaiKhoan" },
            
            // Cài đặt
            { "cai dat", "CaiDat" },
            { "caidat", "CaiDat" },
            { "thiet lap", "CaiDat" },
            { "thietlap", "CaiDat" }
        };

        public ucHeader()
        {
            InitializeComponent();
            this.Load += ucHeader_Load;
            
            // Đăng ký sự kiện cho textbox search
            txtSearch.KeyDown += TxtSearch_KeyDown;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            
            // Đăng ký sự kiện cho nút thông báo
            if (btnNotifications != null)
            {
                btnNotifications.Click += BtnNotifications_Click;
            }
        }

        // Public methods để update header text
        public void UpdateHeader(string title, string breadcrumb)
        {
            lblTitle.Text = title;
            lblBreadcrumb.Text = breadcrumb;
        }

        public void SetTitle(string title)
        {
            lblTitle.Text = title;
        }

        public void SetBreadcrumb(string breadcrumb)
        {
            lblBreadcrumb.Text = breadcrumb;
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlHeader_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void ucHeader_Load(object sender, EventArgs e)
        {
            UpdateUserInfo();
        }

        /// <summary>
        /// ✅ Cập nhật thông tin người dùng từ SessionManager
        /// </summary>
        public void UpdateUserInfo()
        {
            try
            {
                if (SessionManager.IsLoggedIn())
                {
                    Console.WriteLine($"[DEBUG] UpdateUserInfo - TenDangNhap: {SessionManager.TenDangNhap}");
                    Console.WriteLine($"[DEBUG] UpdateUserInfo - HoTen: {SessionManager.HoTen}");
                    Console.WriteLine($"[DEBUG] UpdateUserInfo - VaiTro: {SessionManager.VaiTro}");

                    // ✅ LUÔN HIỂN THỊ TÊN ĐĂNG NHẬP (không dùng HoTen)
                    if (this.Controls.Find("tenDangNhap", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2HtmlLabel lblUserName)
                    {
                        lblUserName.Text = SessionManager.TenDangNhap; // ✅ Luôn dùng TenDangNhap
                        Console.WriteLine($"[SUCCESS] Đã set tenDangNhap = {SessionManager.TenDangNhap}");
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] Không tìm thấy control 'tenDangNhap'");
                    }

                    // ✅ HIỂN THỊ VAI TRÒ
                    if (this.Controls.Find("lblLogRole", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2HtmlLabel lblRole)
                    {
                        string role = SessionManager.GetDisplayRole();
                        lblRole.Text = role;
                        Console.WriteLine($"[SUCCESS] Đã set lblLogRole = {role}");
                    }
                    else
                    {
                        Console.WriteLine("[WARNING] Không tìm thấy control 'lblLogRole'");
                    }
                }
                else
                {
                    Console.WriteLine("[INFO] Chưa đăng nhập");

                    if (this.Controls.Find("tenDangNhap", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2HtmlLabel lblUserName)
                    {
                        lblUserName.Text = "Guest";
                    }

                    if (this.Controls.Find("lblLogRole", true).FirstOrDefault() is Guna.UI2.WinForms.Guna2HtmlLabel lblRole)
                    {
                        lblRole.Text = "Khách";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi cập nhật thông tin người dùng: {ex.Message}");
                Console.WriteLine($"[ERROR] Stack trace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// ✅ Public method để refresh thông tin người dùng từ bên ngoài
        /// </summary>
        public void RefreshUserInfo()
        {
            UpdateUserInfo();
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn Enter trong ô tìm kiếm
        /// </summary>
        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn tiếng beep
                PerformSearch();
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi text thay đổi (có thể dùng để gợi ý)
        /// </summary>
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Có thể thêm logic gợi ý tìm kiếm ở đây nếu cần
        }

        /// <summary>
        /// Thực hiện tìm kiếm và chuyển đến màn hình tương ứng
        /// </summary>
        private void PerformSearch()
        {
            string searchText = txtSearch.Text?.Trim();
            
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return;
            }

            // Chuẩn hóa chuỗi tìm kiếm: loại bỏ dấu và chuyển thành chữ thường
            string normalizedSearch = RemoveVietnameseTones(searchText).ToLower();

            // Debug: In ra console để kiểm tra
            Console.WriteLine($"[DEBUG] Tìm kiếm: '{searchText}' -> Chuẩn hóa: '{normalizedSearch}'");

            // Tìm kiếm chính xác (không dấu)
            var exactMatch = searchKeywords.Keys.FirstOrDefault(k => 
                RemoveVietnameseTones(k).ToLower() == normalizedSearch);

            if (exactMatch != null)
            {
                Console.WriteLine($"[DEBUG] Tìm thấy chính xác: {exactMatch} -> {searchKeywords[exactMatch]}");
                NavigateToScreen(searchKeywords[exactMatch]);
                txtSearch.Clear();
                return;
            }

            // Tìm kiếm gần đúng (chứa từ khóa, không dấu)
            var partialMatch = searchKeywords.Keys.FirstOrDefault(k =>
            {
                string normalizedKey = RemoveVietnameseTones(k).ToLower();
                return normalizedKey.Contains(normalizedSearch) || normalizedSearch.Contains(normalizedKey);
            });

            if (partialMatch != null)
            {
                Console.WriteLine($"[DEBUG] Tìm thấy gần đúng: {partialMatch} -> {searchKeywords[partialMatch]}");
                NavigateToScreen(searchKeywords[partialMatch]);
                txtSearch.Clear();
            }
            else
            {
                // Không tìm thấy
                Console.WriteLine($"[DEBUG] Không tìm thấy kết quả cho: '{searchText}'");
                MessageBox.Show(
                    $"Không tìm thấy chức năng phù hợp với '{txtSearch.Text}'.\n\n" +
                    "Gợi ý: Học sinh, Giáo viên, Lớp học, Môn học, Điểm số, Xếp loại, Hạnh kiểm, Phân công, Thời khóa biểu, Thông báo...",
                    "Tìm kiếm",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        /// <summary>
        /// Loại bỏ dấu tiếng Việt để so sánh chuỗi
        /// </summary>
        private string RemoveVietnameseTones(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            string[] vietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };

            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                {
                    text = text.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
                }
            }

            return text;
        }

        /// <summary>
        /// Chuyển đến màn hình được chỉ định
        /// </summary>
        private void NavigateToScreen(string screenName)
        {
            OnNavigationRequested?.Invoke(screenName);
        }

        /// <summary>
        /// Xử lý sự kiện click vào nút thông báo
        /// </summary>
        private void BtnNotifications_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở form xem thông báo
                var frmThongBao = new Student_Management_System_CSharp_SGU2025.GUI.GUIClass.ThongBao.FrmXemThongBao();
                frmThongBao.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở thông báo: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
