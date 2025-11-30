using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO cho b?ng HoSoNguoiDung
    /// </summary>
    public class HoSoNguoiDungDTO
    {
        public int MaHoSo { get; set; }
        public string TenDangNhap { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string LoaiDoiTuong { get; set; } // 'hocsinh', 'phuhuynh', 'giaovien', 'nhanvien'

        public HoSoNguoiDungDTO()
        {
        }

        public HoSoNguoiDungDTO(string tenDangNhap, string hoTen, string email, string soDienThoai, 
            DateTime? ngaySinh, string gioiTinh, string diaChi, string loaiDoiTuong)
        {
            TenDangNhap = tenDangNhap;
            HoTen = hoTen;
            Email = email;
            SoDienThoai = soDienThoai;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            DiaChi = diaChi;
            LoaiDoiTuong = loaiDoiTuong;
        }
    }
}