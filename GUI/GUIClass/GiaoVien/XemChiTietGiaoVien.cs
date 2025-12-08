using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class XemChiTietGiaoVien : Form
    {
        private GiaoVienBUS giaoVienBUS;
        private LopHocBUS lopHocBUS;
        private PhanCongGiangDayBUS phanCongBUS;
        private MonHocBUS monHocBUS;
        private HocKyBUS hocKyBUS;
        private NguoiDungBLL nguoiDungBLL;

        private string maGiaoVien;
        private GiaoVienDTO giaoVien;
        private List<LopDTO> danhSachLopGVCN;
        private List<PhanCongGiangDayDTO> danhSachPhanCong;

        public XemChiTietGiaoVien(string maGiaoVien)
        {
            InitializeComponent();
            this.maGiaoVien = maGiaoVien;

            giaoVienBUS = new GiaoVienBUS();
            lopHocBUS = new LopHocBUS();
            phanCongBUS = new PhanCongGiangDayBUS();
            monHocBUS = new MonHocBUS();
            hocKyBUS = new HocKyBUS();
            nguoiDungBLL = new NguoiDungBLL();

            danhSachLopGVCN = new List<LopDTO>();
            danhSachPhanCong = new List<PhanCongGiangDayDTO>();

            LoadThongTinGiaoVien();
        }

        private void LoadThongTinGiaoVien()
        {
            try
            {
                // Load thông tin giáo viên
                giaoVien = giaoVienBUS.LayGiaoVienTheoMa(maGiaoVien);
                if (giaoVien == null)
                {
                    MessageBox.Show("Không tìm thấy giáo viên với mã này!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Hiển thị thông tin cơ bản
                DisplayThongTinCoBan();

                // Load và hiển thị lớp làm GVCN
                LoadLopGVCN();

                // Load và hiển thị phân công giảng dạy
                LoadPhanCongGiangDay();

                // Load thông tin tài khoản
                LoadThongTinTaiKhoan();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayThongTinCoBan()
        {
            // Header
            lblTieuDe.Text = $"Chi tiết giáo viên - {giaoVien.MaGiaoVien}";
            lblHoTen.Text = giaoVien.HoTen;
            lblMaGiaoVien.Text = $"Mã: {giaoVien.MaGiaoVien}";

            // Thông tin cá nhân
            lblNgaySinh.Text = giaoVien.NgaySinh != DateTime.MinValue 
                ? giaoVien.NgaySinh.ToString("dd/MM/yyyy") 
                : "Chưa cập nhật";
            
            lblGioiTinh.Text = !string.IsNullOrEmpty(giaoVien.GioiTinh) 
                ? giaoVien.GioiTinh 
                : "Chưa cập nhật";
            
            lblDiaChi.Text = !string.IsNullOrEmpty(giaoVien.DiaChi) 
                ? giaoVien.DiaChi 
                : "Chưa cập nhật";
            
            lblSoDienThoai.Text = !string.IsNullOrEmpty(giaoVien.SoDienThoai) 
                ? giaoVien.SoDienThoai 
                : "Chưa cập nhật";
            
            lblEmail.Text = !string.IsNullOrEmpty(giaoVien.Email) 
                ? giaoVien.Email 
                : "Chưa cập nhật";

            // Chuyên môn
            lblChuyenMon.Text = !string.IsNullOrEmpty(giaoVien.TenMonChuyenMon) 
                ? giaoVien.TenMonChuyenMon 
                : "Chưa phân công";

            // Trạng thái
            lblTrangThai.Text = !string.IsNullOrEmpty(giaoVien.TrangThai) 
                ? giaoVien.TrangThai 
                : "Đang giảng dạy";
            
            // Format màu trạng thái
            if (lblTrangThai.Text == "Đang giảng dạy")
            {
                lblTrangThai.ForeColor = Color.FromArgb(52, 168, 83);
            }
            else
            {
                lblTrangThai.ForeColor = Color.Gray;
            }
        }

        private void LoadLopGVCN()
        {
            try
            {
                var danhSachLop = lopHocBUS.DocDSLop();
                danhSachLopGVCN = danhSachLop
                    .Where(lop => lop.maGVCN == maGiaoVien)
                    .ToList();

                if (danhSachLopGVCN.Count > 0)
                {
                    lblSoLopGVCN.Text = $"Đang làm GVCN {danhSachLopGVCN.Count} lớp";
                    dgvLopGVCN.DataSource = danhSachLopGVCN.Select(lop => new
                    {
                        TenLop = lop.tenLop,
                        MaKhoi = lop.maKhoi,
                        SiSo = lop.siSo,
                        NamHoc = lop.TenNamHoc ?? "Chưa xác định"
                    }).ToList();
                }
                else
                {
                    lblSoLopGVCN.Text = "Chưa làm GVCN lớp nào";
                    dgvLopGVCN.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load lớp GVCN: {ex.Message}");
                lblSoLopGVCN.Text = "Không thể tải dữ liệu";
            }
        }

        private void LoadPhanCongGiangDay()
        {
            try
            {
                danhSachPhanCong = phanCongBUS.LayPhanCongTheoGiaoVien(maGiaoVien);
                var danhSachMonHoc = monHocBUS.DocDSMH();
                var danhSachLop = lopHocBUS.DocDSLop();
                var danhSachHocKy = hocKyBUS.DocDSHocKy();

                if (danhSachPhanCong.Count > 0)
                {
                    lblSoPhanCong.Text = $"Có {danhSachPhanCong.Count} phân công giảng dạy";
                    
                    var dataSource = danhSachPhanCong.Select(pc => new
                    {
                        TenLop = danhSachLop.FirstOrDefault(l => l.maLop == pc.MaLop)?.tenLop ?? $"Lớp {pc.MaLop}",
                        TenMon = danhSachMonHoc.FirstOrDefault(m => m.maMon == pc.MaMonHoc)?.tenMon ?? $"Môn {pc.MaMonHoc}",
                        HocKy = danhSachHocKy.FirstOrDefault(hk => hk.MaHocKy == pc.MaHocKy)?.TenHocKy ?? $"HK {pc.MaHocKy}",
                        NgayBatDau = pc.NgayBatDau != DateTime.MinValue ? pc.NgayBatDau.ToString("dd/MM/yyyy") : "N/A",
                        NgayKetThuc = pc.NgayKetThuc != DateTime.MinValue ? pc.NgayKetThuc.ToString("dd/MM/yyyy") : "N/A"
                    }).ToList();

                    dgvPhanCong.DataSource = dataSource;
                }
                else
                {
                    lblSoPhanCong.Text = "Chưa có phân công giảng dạy";
                    dgvPhanCong.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load phân công: {ex.Message}");
                lblSoPhanCong.Text = "Không thể tải dữ liệu";
            }
        }

        private void LoadThongTinTaiKhoan()
        {
            try
            {
                // Kiểm tra tài khoản
                string tenDangNhap = maGiaoVien; // Mã giáo viên thường dùng làm tên đăng nhập
                if (nguoiDungBLL.CheckTenDangNhapExists(tenDangNhap))
                {
                    var nguoiDung = nguoiDungBLL.GetNguoiDungByTenDangNhap(tenDangNhap);
                    if (nguoiDung != null)
                    {
                        lblTenDangNhap.Text = nguoiDung.TenDangNhap;
                        lblTrangThaiTaiKhoan.Text = nguoiDung.TrangThai ?? "Hoạt động";
                        
                        if (lblTrangThaiTaiKhoan.Text == "Hoạt động")
                        {
                            lblTrangThaiTaiKhoan.ForeColor = Color.FromArgb(52, 168, 83);
                        }
                        else
                        {
                            lblTrangThaiTaiKhoan.ForeColor = Color.Gray;
                        }
                    }
                    else
                    {
                        lblTenDangNhap.Text = "Không tìm thấy";
                        lblTrangThaiTaiKhoan.Text = "N/A";
                    }
                }
                else
                {
                    lblTenDangNhap.Text = "Chưa có tài khoản";
                    lblTrangThaiTaiKhoan.Text = "N/A";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load thông tin tài khoản: {ex.Message}");
                lblTenDangNhap.Text = "Lỗi";
                lblTrangThaiTaiKhoan.Text = "Lỗi";
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var formChinhSua = new ChinhSuaGiaoVien(maGiaoVien, readOnly: false);
            if (formChinhSua.ShowDialog() == DialogResult.OK)
            {
                LoadThongTinGiaoVien();
            }
        }
    }
}
