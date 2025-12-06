using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Services;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using System;
using System.Configuration;
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
    public partial class CaiDat : UserControl
    {
        private NguoiDungBLL nguoiDungBLL;
        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private GiaoVienBUS giaoVienBUS;
        private ThongTinNguoiDung thongTinHienTai;
        private CaiDatBUS caiDatBUS;
        public CaiDat()
        {
            InitializeComponent();
            nguoiDungBLL = new NguoiDungBLL();
            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            giaoVienBUS = new GiaoVienBUS();
            caiDatBUS = new CaiDatBUS();

        }

        private void CaiDat_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
            LoadThongTinChiTiet();
            if (btnDoiMatKhau != null)
            {
                btnDoiMatKhau.Click += btnDoiMatKhau_Click;
            }

            // ✅ Đặt PasswordChar cho các TextBox mật khẩu
            if (txtPW != null)
                txtPW.PasswordChar = '●';

            if (txtNewPW != null)
                txtNewPW.PasswordChar = '●';

            if (txtVeriNewPW != null)
                txtVeriNewPW.PasswordChar = '●';
        }


        /// <summary>
        /// ✅ Load thông tin người dùng đã đăng nhập (phần header)
        /// </summary>
        private void LoadUserInfo()
        {
            try
            {
                if (!SessionManager.IsLoggedIn())
                {
                    lblLogName.Text = "Chưa đăng nhập";
                    lblUserName.Text = "N/A";
                    lblRoleDetail.Text = "N/A";
                    lblUserRole.Text = "N/A";
                    lblLastLogin.Text = "N/A";
                    return;
                }

                lblLogName.Text = SessionManager.TenDangNhap;

                var nguoiDung = nguoiDungBLL.GetNguoiDungByTenDangNhap(SessionManager.TenDangNhap);

                if (nguoiDung != null)
                {
                    string vaiTro = "Chưa có vai trò";
                    string maVaiTro = "";

                    if (!string.IsNullOrEmpty(nguoiDung.VaiTro))
                    {
                        vaiTro = nguoiDung.VaiTro;
                        lblRoleDetail.Text = vaiTro;
                    }
                    else
                    {
                        lblRoleDetail.Text = vaiTro;
                    }

                    maVaiTro = nguoiDung.MaVaiTro ?? "";
                    lblUserRole.Text = vaiTro;

                    string hoTen = LayHoTenTheoVaiTro(SessionManager.TenDangNhap, maVaiTro);
                    lblUserName.Text = hoTen;

                    if (nguoiDung.LanDangNhapCuoi.HasValue)
                    {
                        lblLastLogin.Text = nguoiDung.LanDangNhapCuoi.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    else
                    {
                        lblLastLogin.Text = "Chưa có thông tin";
                    }
                }
                else
                {
                    lblRoleDetail.Text = SessionManager.GetDisplayRole();
                    lblUserRole.Text = SessionManager.GetDisplayRole();
                    lblUserName.Text = SessionManager.TenDangNhap;
                    lblLastLogin.Text = "N/A";
                }

                Console.WriteLine($"[INFO] Đã load thông tin người dùng: {SessionManager.TenDangNhap}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi load thông tin người dùng: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải thông tin người dùng: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Lấy họ tên dựa trên vai trò của người dùng
        /// </summary>
        private string LayHoTenTheoVaiTro(string tenDangNhap, string maVaiTro)
        {
            try
            {
                var danhSachMaVaiTro = maVaiTro.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(v => v.Trim().ToLower())
                                              .ToList();

                // TRƯỜNG HỢP 1: Vai trò STUDENT (học sinh)
                if (danhSachMaVaiTro.Any(v => v.Contains("student") || v.Contains("hs")))
                {
                    Console.WriteLine($"[INFO] Đang tìm họ tên học sinh cho: {tenDangNhap}");

                    if (tenDangNhap.StartsWith("HS", StringComparison.OrdinalIgnoreCase))
                    {
                        string maHocSinhStr = tenDangNhap.Substring(2);
                        if (int.TryParse(maHocSinhStr, out int maHocSinh))
                        {
                            var hocSinh = hocSinhBLL.GetHocSinhById(maHocSinh);
                            if (hocSinh != null && !string.IsNullOrEmpty(hocSinh.HoTen))
                            {
                                Console.WriteLine($"[SUCCESS] Tìm thấy họ tên học sinh: {hocSinh.HoTen}");
                                return hocSinh.HoTen;
                            }
                        }
                    }
                }

                // TRƯỜNG HỢP 2: Vai trò PARENT (phụ huynh)
                if (danhSachMaVaiTro.Any(v => v.Contains("parent") || v.Contains("ph")))
                {
                    Console.WriteLine($"[INFO] Đang tìm họ tên phụ huynh cho: {tenDangNhap}");

                    if (tenDangNhap.StartsWith("PH", StringComparison.OrdinalIgnoreCase))
                    {
                        string maPhuHuynhStr = tenDangNhap.Substring(2);
                        if (int.TryParse(maPhuHuynhStr, out int maPhuHuynh))
                        {
                            var phuHuynh = phuHuynhBLL.GetPhuHuynhById(maPhuHuynh);
                            if (phuHuynh != null && !string.IsNullOrEmpty(phuHuynh.HoTen))
                            {
                                Console.WriteLine($"[SUCCESS] Tìm thấy họ tên phụ huynh: {phuHuynh.HoTen}");
                                return phuHuynh.HoTen;
                            }
                        }
                    }
                }

                // TRƯỜNG HỢP 3: Vai trò TEACHER (giáo viên)
                if (danhSachMaVaiTro.Any(v => v.Contains("teacher") || v.Contains("gv") || v.Contains("giaovien")))
                {
                    Console.WriteLine($"[INFO] Đang tìm họ tên giáo viên cho: {tenDangNhap}");

                    if (tenDangNhap.StartsWith("GV", StringComparison.OrdinalIgnoreCase))
                    {
                        string maGiaoVien = tenDangNhap;
                        var giaoVien = giaoVienBUS.LayGiaoVienTheoMa(maGiaoVien);
                        if (giaoVien != null && !string.IsNullOrEmpty(giaoVien.HoTen))
                        {
                            Console.WriteLine($"[SUCCESS] Tìm thấy họ tên giáo viên: {giaoVien.HoTen}");
                            return giaoVien.HoTen;
                        }
                    }
                }

                // TRƯỜNG HỢP 4: Các vai trò khác → Lấy từ HoSoNguoiDung
                Console.WriteLine($"[INFO] Đang tìm họ tên trong HoSoNguoiDung cho: {tenDangNhap}");
                var hoSo = nguoiDungBLL.GetHoSoByTenDangNhap(tenDangNhap);
                if (hoSo != null && !string.IsNullOrEmpty(hoSo.HoTen))
                {
                    Console.WriteLine($"[SUCCESS] Tìm thấy họ tên trong hồ sơ: {hoSo.HoTen}");
                    return hoSo.HoTen;
                }

                Console.WriteLine($"[WARNING] Không tìm thấy họ tên, sử dụng tên đăng nhập: {tenDangNhap}");
                return tenDangNhap;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi lấy họ tên theo vai trò: {ex.Message}");
                return tenDangNhap;
            }
        }

        /// <summary>
        /// ✅ Load thông tin chi tiết để chỉnh sửa (các TextBox)
        /// </summary>
        private void LoadThongTinChiTiet()
        {
            try
            {
                if (!SessionManager.IsLoggedIn())
                {
                    ClearFormFields();
                    return;
                }

                var nguoiDung = nguoiDungBLL.GetNguoiDungByTenDangNhap(SessionManager.TenDangNhap);
                if (nguoiDung == null)
                {
                    ClearFormFields();
                    return;
                }

                string maVaiTro = nguoiDung.MaVaiTro ?? "";
                thongTinHienTai = LayThongTinChiTietTheoVaiTro(SessionManager.TenDangNhap, maVaiTro);

                if (thongTinHienTai != null)
                {
                    // ✅ Hiển thị thông tin lên form
                    txtUserName.Text = thongTinHienTai.HoTen ?? SessionManager.TenDangNhap;
                    txtEmail.Text = thongTinHienTai.Email ?? "";
                    txtSDT.Text = thongTinHienTai.SoDienThoai ?? "";
                    txtAddress.Text = thongTinHienTai.DiaChi ?? "";

                    if (thongTinHienTai.NgaySinh.HasValue)
                    {
                        dateNgaySinh.Value = thongTinHienTai.NgaySinh.Value;
                    }
                    else
                    {
                        dateNgaySinh.Value = DateTime.Now.AddYears(-20);
                    }

                    // ✅ Vô hiệu hóa các trường không được chỉnh sửa
                    txtUserName.ReadOnly = true;
                    txtUserName.Enabled = false;
                    dateNgaySinh.Enabled = false;

                    Console.WriteLine($"[SUCCESS] Đã load thông tin chi tiết cho: {SessionManager.TenDangNhap}");
                }
                else
                {
                    // Không có thông tin chi tiết
                    ClearFormFields();
                    Console.WriteLine($"[WARNING] Không tìm thấy thông tin chi tiết cho: {SessionManager.TenDangNhap}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi load thông tin chi tiết: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải thông tin chi tiết: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Lấy thông tin chi tiết theo vai trò
        /// </summary>
        private ThongTinNguoiDung LayThongTinChiTietTheoVaiTro(string tenDangNhap, string maVaiTro)
        {
            try
            {
                var danhSachMaVaiTro = maVaiTro.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(v => v.Trim().ToLower())
                                              .ToList();

                // TRƯỜNG HỢP 1: Vai trò STUDENT (học sinh)
                if (danhSachMaVaiTro.Any(v => v.Contains("student") || v.Contains("hs")))
                {
                    if (tenDangNhap.StartsWith("HS", StringComparison.OrdinalIgnoreCase))
                    {
                        string maHocSinhStr = tenDangNhap.Substring(2);
                        if (int.TryParse(maHocSinhStr, out int maHocSinh))
                        {
                            var hocSinh = hocSinhBLL.GetHocSinhById(maHocSinh);
                            if (hocSinh != null)
                            {
                                // ✅ Lấy địa chỉ của phụ huynh liên quan
                                string diaChiPhuHuynh = LayDiaChiPhuHuynhDauTien(maHocSinh);

                                return new ThongTinNguoiDung
                                {
                                    HoTen = hocSinh.HoTen,
                                    Email = hocSinh.Email,
                                    SoDienThoai = hocSinh.SdtHS,
                                    NgaySinh = hocSinh.NgaySinh,
                                    DiaChi = diaChiPhuHuynh, // ✅ Hiển thị địa chỉ phụ huynh
                                    LoaiDoiTuong = "HocSinh",
                                    MaDoiTuong = maHocSinh
                                };
                            }
                        }
                    }
                }

                // TRƯỜNG HỢP 2: Vai trò PARENT (phụ huynh)
                if (danhSachMaVaiTro.Any(v => v.Contains("parent") || v.Contains("ph")))
                {
                    if (tenDangNhap.StartsWith("PH", StringComparison.OrdinalIgnoreCase))
                    {
                        string maPhuHuynhStr = tenDangNhap.Substring(2);
                        if (int.TryParse(maPhuHuynhStr, out int maPhuHuynh))
                        {
                            var phuHuynh = phuHuynhBLL.GetPhuHuynhById(maPhuHuynh);
                            if (phuHuynh != null)
                            {
                                return new ThongTinNguoiDung
                                {
                                    HoTen = phuHuynh.HoTen,
                                    Email = phuHuynh.Email,
                                    SoDienThoai = phuHuynh.SoDienThoai,
                                    NgaySinh = null, // Phụ huynh không có ngày sinh
                                    DiaChi = phuHuynh.DiaChi,
                                    LoaiDoiTuong = "PhuHuynh",
                                    MaDoiTuong = maPhuHuynh
                                };
                            }
                        }
                    }
                }

                // TRƯỜNG HỢP 3: Vai trò TEACHER (giáo viên)
                if (danhSachMaVaiTro.Any(v => v.Contains("teacher") || v.Contains("gv") || v.Contains("giaovien")))
                {
                    if (tenDangNhap.StartsWith("GV", StringComparison.OrdinalIgnoreCase))
                    {
                        string maGiaoVien = tenDangNhap;
                        var giaoVien = giaoVienBUS.LayGiaoVienTheoMa(maGiaoVien);
                        if (giaoVien != null)
                        {
                            return new ThongTinNguoiDung
                            {
                                HoTen = giaoVien.HoTen,
                                Email = giaoVien.Email,
                                SoDienThoai = giaoVien.SoDienThoai,
                                NgaySinh = giaoVien.NgaySinh,
                                DiaChi = giaoVien.DiaChi,
                                LoaiDoiTuong = "GiaoVien",
                                MaDoiTuongStr = maGiaoVien
                            };
                        }
                    }
                }

                // TRƯỜNG HỢP 4: Các vai trò khác → Lấy từ HoSoNguoiDung
                var hoSo = nguoiDungBLL.GetHoSoByTenDangNhap(tenDangNhap);
                if (hoSo != null)
                {
                    return new ThongTinNguoiDung
                    {
                        HoTen = hoSo.HoTen,
                        Email = hoSo.Email,
                        SoDienThoai = hoSo.SoDienThoai,
                        NgaySinh = hoSo.NgaySinh,
                        DiaChi = hoSo.DiaChi,
                        LoaiDoiTuong = "HoSoNguoiDung",
                        MaDoiTuong = hoSo.MaHoSo
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi lấy thông tin chi tiết: {ex.Message}");
                return null;
            }
        }

        // ✅ THÊM HÀM MỚI - SAU HÀM IsValidPhoneNumber()

        /// <summary>
        /// ✅ Lấy địa chỉ của phụ huynh đầu tiên liên quan đến học sinh
        /// </summary>
        private string LayDiaChiPhuHuynhDauTien(int maHocSinh)
        {
            try
            {
                HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();

                // Lấy danh sách phụ huynh của học sinh
                var dsPhuHuynh = hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(maHocSinh);

                if (dsPhuHuynh != null && dsPhuHuynh.Count > 0)
                {
                    // Ưu tiên lấy địa chỉ của Cha hoặc Mẹ
                    var phuHuynhUuTien = dsPhuHuynh.FirstOrDefault(p =>
                        p.moiQuanHe.Equals("Cha", StringComparison.OrdinalIgnoreCase) ||
                        p.moiQuanHe.Equals("Mẹ", StringComparison.OrdinalIgnoreCase) ||
                        p.moiQuanHe.Equals("Ba", StringComparison.OrdinalIgnoreCase) ||
                        p.moiQuanHe.Equals("Mẹ", StringComparison.OrdinalIgnoreCase));

                    if (phuHuynhUuTien.phuHuynh != null)
                    {
                        string diaChi = phuHuynhUuTien.phuHuynh.DiaChi;
                        Console.WriteLine($"[INFO] Lấy địa chỉ từ phụ huynh: {phuHuynhUuTien.phuHuynh.HoTen} ({phuHuynhUuTien.moiQuanHe}) - {diaChi}");
                        return diaChi;
                    }

                    // Nếu không có Cha/Mẹ, lấy phụ huynh đầu tiên
                    var phuHuynhDauTien = dsPhuHuynh.First();
                    string diaChiDauTien = phuHuynhDauTien.phuHuynh.DiaChi;
                    Console.WriteLine($"[INFO] Lấy địa chỉ từ phụ huynh đầu tiên: {phuHuynhDauTien.phuHuynh.HoTen} ({phuHuynhDauTien.moiQuanHe}) - {diaChiDauTien}");
                    return diaChiDauTien;
                }

                Console.WriteLine($"[WARNING] Học sinh {maHocSinh} không có phụ huynh nào trong hệ thống.");
                return null; // Không có phụ huynh
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi lấy địa chỉ phụ huynh: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// ✅ Xóa các trường trong form
        /// </summary>
        private void ClearFormFields()
        {
            txtUserName.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtAddress.Text = "";
            dateNgaySinh.Value = DateTime.Now.AddYears(-20);

            txtUserName.ReadOnly = true;
            txtUserName.Enabled = false;
            dateNgaySinh.Enabled = false;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void lastLogin_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void lblLastLogin_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void tenDangNhap_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đăng nhập
                if (!SessionManager.IsLoggedIn() || thongTinHienTai == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy dữ liệu đầu vào
                string email = txtEmail.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string diaChi = txtAddress.Text.Trim();

                // ✅ Validate email (đã bao gồm cả kiểm tra rỗng)
                string emailErrorMessage;
                if (!ValidationHelper.ValidateEmailWithMessage(email, out emailErrorMessage))
                {
                    MessageBox.Show($"Email không hợp lệ!\n\n{emailErrorMessage}",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // ✅ Validate số điện thoại (đã bao gồm cả kiểm tra rỗng)
                string phoneErrorMessage;
                if (!ValidationHelper.ValidatePhoneNumberWithMessage(sdt, out phoneErrorMessage))
                {
                    MessageBox.Show($"Số điện thoại không hợp lệ!\n\n{phoneErrorMessage}",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDT.Focus();
                    return;
                }

                // ✅ Chuẩn hóa số điện thoại về định dạng Việt Nam (chuyển +84 → 0)
                string sdtChuanHoa = ValidationHelper.ConvertToVietnameseFormat(sdt);

                // ✅ Kiểm tra trùng lặp (dùng số đã chuẩn hóa)
                if (!KiemTraTrungLapThongTin(email, sdtChuanHoa))
                {
                    return;
                }

                // Xác nhận cập nhật
                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn cập nhật thông tin không?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return;

                // Cập nhật theo loại đối tượng (dùng số đã chuẩn hóa)
                bool ketQua = false;
                string thongBao = "";

                switch (thongTinHienTai.LoaiDoiTuong)
                {
                    case "HocSinh":
                        ketQua = CapNhatHocSinh(email, sdtChuanHoa);
                        thongBao = "học sinh";
                        break;

                    case "PhuHuynh":
                        ketQua = CapNhatPhuHuynh(email, sdtChuanHoa, diaChi);
                        thongBao = "phụ huynh";
                        break;

                    case "GiaoVien":
                        ketQua = CapNhatGiaoVien(email, sdtChuanHoa, diaChi);
                        thongBao = "giáo viên";
                        break;

                    case "HoSoNguoiDung":
                        ketQua = CapNhatHoSoNguoiDung(email, sdtChuanHoa, diaChi);
                        thongBao = "hồ sơ người dùng";
                        break;

                    default:
                        MessageBox.Show("Không xác định được loại tài khoản!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Hiển thị kết quả
                if (ketQua)
                {
                    if (thongTinHienTai.LoaiDoiTuong == "HocSinh" && !string.IsNullOrWhiteSpace(diaChi))
                    {
                        MessageBox.Show(
                            $"Cập nhật thông tin {thongBao} thành công!\n\n" +
                            "Địa chỉ cũng đã được cập nhật cho các phụ huynh liên quan.",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Cập nhật thông tin {thongBao} thành công!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LoadThongTinChiTiet();
                }
                else
                {
                    MessageBox.Show($"Không thể cập nhật thông tin {thongBao}!\n\nVui lòng kiểm tra lại dữ liệu hoặc xem log để biết thêm chi tiết.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi cập nhật thông tin: {ex.Message}");
                MessageBox.Show($"Lỗi khi cập nhật thông tin:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Cập nhật thông tin học sinh và địa chỉ phụ huynh liên quan
        /// </summary>
        private bool CapNhatHocSinh(string email, string sdt)
        {
            try
            {
                // 1. Cập nhật thông tin học sinh
                var hocSinh = hocSinhBLL.GetHocSinhById(thongTinHienTai.MaDoiTuong);
                if (hocSinh == null) return false;

                hocSinh.Email = email;
                hocSinh.SdtHS = sdt;

                bool capNhatHocSinhThanhCong = hocSinhBLL.UpdateHocSinh(hocSinh);

                if (!capNhatHocSinhThanhCong)
                {
                    return false;
                }

                // 2. ✅ Cập nhật địa chỉ cho tất cả phụ huynh liên quan (nếu có nhập địa chỉ)
                string diaChi = txtAddress.Text.Trim();
                if (!string.IsNullOrWhiteSpace(diaChi))
                {
                    CapNhatDiaChiPhuHuynhLienQuan(thongTinHienTai.MaDoiTuong, diaChi);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi cập nhật học sinh: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// ✅ Cập nhật địa chỉ cho tất cả phụ huynh có quan hệ với học sinh
        /// </summary>
        private void CapNhatDiaChiPhuHuynhLienQuan(int maHocSinh, string diaChiMoi)
        {
            try
            {
                HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();

                // Lấy danh sách phụ huynh của học sinh
                var dsPhuHuynh = hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(maHocSinh);

                if (dsPhuHuynh == null || dsPhuHuynh.Count == 0)
                {
                    Console.WriteLine($"[INFO] Học sinh {maHocSinh} không có phụ huynh nào trong hệ thống.");
                    return;
                }

                int soLuongCapNhat = 0;
                int soLuongThatBai = 0;

                foreach (var (phuHuynh, moiQuanHe) in dsPhuHuynh)
                {
                    try
                    {
                        // Cập nhật địa chỉ cho phụ huynh
                        phuHuynh.DiaChi = diaChiMoi;

                        bool ketQua = phuHuynhBLL.UpdatePhuHuynh(phuHuynh);

                        if (ketQua)
                        {
                            soLuongCapNhat++;
                            Console.WriteLine($"[SUCCESS] Đã cập nhật địa chỉ cho phụ huynh {phuHuynh.HoTen} ({moiQuanHe})");
                        }
                        else
                        {
                            soLuongThatBai++;
                            Console.WriteLine($"[WARNING] Không thể cập nhật địa chỉ cho phụ huynh {phuHuynh.HoTen}");
                        }
                    }
                    catch (Exception ex)
                    {
                        soLuongThatBai++;
                        Console.WriteLine($"[ERROR] Lỗi cập nhật phụ huynh {phuHuynh.HoTen}: {ex.Message}");
                    }
                }

                // Hiển thị thông báo tổng kết
                if (soLuongCapNhat > 0)
                {
                    string thongBao = $"Đã cập nhật địa chỉ cho {soLuongCapNhat} phụ huynh liên quan.";
                    if (soLuongThatBai > 0)
                    {
                        thongBao += $"\nKhông cập nhật được {soLuongThatBai} phụ huynh.";
                    }

                    Console.WriteLine($"[INFO] {thongBao}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi khi cập nhật địa chỉ phụ huynh liên quan: {ex.Message}");
            }
        }

        /// <summary>
        /// ✅ Cập nhật thông tin phụ huynh
        /// </summary>
        private bool CapNhatPhuHuynh(string email, string sdt, string diaChi)
        {
            var phuHuynh = phuHuynhBLL.GetPhuHuynhById(thongTinHienTai.MaDoiTuong);
            if (phuHuynh == null) return false;

            phuHuynh.Email = email;
            phuHuynh.SoDienThoai = sdt;
            phuHuynh.DiaChi = diaChi;

            return phuHuynhBLL.UpdatePhuHuynh(phuHuynh);
        }

        /// <summary>
        /// ✅ Cập nhật thông tin giáo viên
        /// </summary>
        private bool CapNhatGiaoVien(string email, string sdt, string diaChi)
        {
            var giaoVien = giaoVienBUS.LayGiaoVienTheoMa(thongTinHienTai.MaDoiTuongStr);
            if (giaoVien == null) return false;

            giaoVien.Email = email;
            giaoVien.SoDienThoai = sdt;
            giaoVien.DiaChi = diaChi;

            return giaoVienBUS.CapNhatGiaoVien(giaoVien);
        }

        /// <summary>
        /// ✅ Cập nhật thông tin hồ sơ người dùng
        /// </summary>
        private bool CapNhatHoSoNguoiDung(string email, string sdt, string diaChi)
        {
            try
            {
                HoSoNguoiDungDAO hoSoDAO = new HoSoNguoiDungDAO();
                var hoSo = hoSoDAO.GetHoSoByTenDangNhap(SessionManager.TenDangNhap);

                if (hoSo == null) return false;

                hoSo.Email = email;
                hoSo.SoDienThoai = sdt;
                hoSo.DiaChi = diaChi;

                return hoSoDAO.UpdateHoSo(hoSo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Lỗi cập nhật hồ sơ: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// ✅ Kiểm tra trùng lặp email/SĐT với người dùng khác
        /// </summary>
        private bool KiemTraTrungLapThongTin(string email, string sdt)
        {
            string errorMessage;

            // ✅ Cách gọi đúng: Không dùng named parameters cho out parameter
            bool isValid = caiDatBUS.KiemTraTrungLapThongTin(
        email,
        sdt,
        thongTinHienTai.LoaiDoiTuong,
        out errorMessage,
        thongTinHienTai.MaDoiTuong,
        thongTinHienTai.MaDoiTuongStr ?? SessionManager.TenDangNhap
    );

            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Thông tin trùng lặp",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Focus vào control tương ứng
                if (errorMessage.Contains("Email"))
                    txtEmail.Focus();
                else if (errorMessage.Contains("điện thoại"))
                    txtSDT.Focus();
            }

            return isValid;
        }


        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đăng nhập
                if (!SessionManager.IsLoggedIn())
                {
                    MessageBox.Show("Bạn chưa đăng nhập!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy dữ liệu đầu vào
                string matKhauCu = txtPW.Text.Trim();
                string matKhauMoi = txtNewPW.Text.Trim();
                string xacNhanMatKhau = txtVeriNewPW.Text.Trim();

                // ✅ Validate input
                if (string.IsNullOrWhiteSpace(matKhauCu))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPW.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(matKhauMoi))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPW.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(xacNhanMatKhau))
                {
                    MessageBox.Show("Vui lòng xác nhận mật khẩu mới!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVeriNewPW.Focus();
                    return;
                }

                // ✅ Kiểm tra độ dài mật khẩu mới
                if (matKhauMoi.Length < 6)
                {
                    MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPW.Focus();
                    return;
                }

                // ✅ Kiểm tra mật khẩu mới và xác nhận khớp nhau
                if (matKhauMoi != xacNhanMatKhau)
                {
                    MessageBox.Show("Mật khẩu mới và xác nhận không khớp!\nVui lòng nhập lại.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVeriNewPW.Clear();
                    txtVeriNewPW.Focus();
                    return;
                }

                // ✅ Kiểm tra mật khẩu mới không trùng mật khẩu cũ
                if (matKhauCu == matKhauMoi)
                {
                    MessageBox.Show("Mật khẩu mới phải khác mật khẩu hiện tại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPW.Focus();
                    return;
                }

                // ✅ Xác nhận với người dùng
                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc chắn muốn đổi mật khẩu cho tài khoản:\n{SessionManager.TenDangNhap}?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return;

                Console.WriteLine($"[INFO] Đang đổi mật khẩu cho tài khoản: {SessionManager.TenDangNhap}");

                // ✅ Thực hiện đổi mật khẩu
                bool ketQua = nguoiDungBLL.DoiMatKhau(SessionManager.TenDangNhap, matKhauCu, matKhauMoi);

                if (ketQua)
                {
                    Console.WriteLine($"[SUCCESS] Đổi mật khẩu thành công");

                    // ✅ Gửi email thông báo (nếu có cấu hình)
                    try
                    {
                        string email = nguoiDungBLL.LayEmailTaiKhoan(SessionManager.TenDangNhap);

                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            // Đọc cấu hình email từ App.config
                            string GMAIL_ADDRESS = System.Configuration.ConfigurationManager.AppSettings["GmailAddress"];
                            string GMAIL_APP_PASSWORD = System.Configuration.ConfigurationManager.AppSettings["GmailAppPassword"];

                            if (!string.IsNullOrEmpty(GMAIL_ADDRESS) &&
                                !string.IsNullOrEmpty(GMAIL_APP_PASSWORD) &&
                                GMAIL_ADDRESS != "your-email@gmail.com")
                            {
                                EmailService emailService = new EmailService(GMAIL_ADDRESS, GMAIL_APP_PASSWORD, "THPT TTPT");
                                bool emailSent = emailService.GuiThongBaoDoiMatKhauThanhCong(email, SessionManager.HoTen);

                                if (emailSent)
                                {
                                    Console.WriteLine($"[SUCCESS] Đã gửi email thông báo đến: {email}");
                                }
                                else
                                {
                                    Console.WriteLine($"[WARNING] Không gửi được email thông báo");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"[WARNING] Chức năng gửi email chưa được cấu hình");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[WARNING] Không tìm thấy email của tài khoản");
                        }
                    }
                    catch (Exception emailEx)
                    {
                        Console.WriteLine($"[WARNING] Lỗi khi gửi email thông báo: {emailEx.Message}");
                        // Không hiển thị lỗi cho user vì đã đổi mật khẩu thành công
                    }

                    // ✅ Hiển thị thông báo thành công
                    MessageBox.Show(
                        "✅ Đổi mật khẩu thành công!\n\n" +
                        "Bạn có thể sử dụng mật khẩu mới để đăng nhập.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // ✅ XÓA CÁC Ô NHẬP LIỆU SAU KHI HIỂN THỊ THÔNG BÁO
                    txtPW.Clear();
                    txtNewPW.Clear();
                    txtVeriNewPW.Clear();

                    // ✅ KHÔNG GỌI Focus() NGAY - Chờ một chút
                    // Sử dụng BeginInvoke để đảm bảo focus sau khi MessageBox đóng hoàn toàn
                    this.BeginInvoke(new Action(() =>
                    {
                        if (txtPW != null && !txtPW.IsDisposed)
                        {
                            txtPW.Focus();
                        }
                    }));
                }
                else
                {
                    Console.WriteLine($"[ERROR] Đổi mật khẩu thất bại");

                    MessageBox.Show("Không thể đổi mật khẩu!\nVui lòng thử lại sau.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Mật khẩu hiện tại không đúng"))
                {
                    MessageBox.Show("❌ Mật khẩu hiện tại không đúng!\n\nVui lòng kiểm tra lại.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPW.Clear();
                    txtPW.Focus();
                }
                else
                {
                    Console.WriteLine($"[ERROR] Lỗi khi đổi mật khẩu: {ex.Message}");
                    MessageBox.Show($"Lỗi khi đổi mật khẩu:\n{ex.Message}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    /// <summary>
    /// ✅ Class helper để lưu thông tin người dùng
    /// </summary>
    internal class ThongTinNguoiDung
    {
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string LoaiDoiTuong { get; set; } // "HocSinh", "PhuHuynh", "GiaoVien", "HoSoNguoiDung"
        public int MaDoiTuong { get; set; } // Mã học sinh, phụ huynh, hồ sơ
        public string MaDoiTuongStr { get; set; } // Mã giáo viên (string)
    }

}
