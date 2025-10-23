using System;
using System.Text;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class GiaoVienDTO
    {
        private string maGiaoVien;
        private string hoTen;
        private DateTime ngaySinh;
        private string gioiTinh;
        private string diaChi;
        private string soDienThoai;
        private string email;
        private string trangThai;

        // **********************************************
        // ⬅️ FIELD MỚI: Chứa chuỗi tên các môn chuyên môn đã JOIN
        private string danhSachChuyenMon;
        // **********************************************

        // Constructors (Hàm khởi tạo)
        public GiaoVienDTO() { }

        // Hàm khởi tạo cũ
        public GiaoVienDTO(string maGiaoVien, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string soDienThoai, string email, string trangThai)
        {
            this.MaGiaoVien = maGiaoVien;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
            this.Email = email;
            this.TrangThai = trangThai;
            // Thuộc tính mới không cần thiết trong constructor cơ bản
        }

        // Hàm khởi tạo đầy đủ hơn (có cả DanhSachChuyenMon)
        public GiaoVien(string maGiaoVien, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string soDienThoai, string email, string trangThai, string danhSachChuyenMon)
        {
            this.MaGiaoVien = maGiaoVien;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
            this.Email = email;
            this.TrangThai = trangThai;
            this.DanhSachChuyenMon = danhSachChuyenMon;
        }


        // Destructor (Hàm hủy)
        ~GiaoVienDTO()
        {
            // Console.WriteLine("Huy doi tuong GiaoVien");
        }

        // Properties (Thuộc tính)
        public string MaGiaoVien
        {
            get { return maGiaoVien; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    maGiaoVien = value;
                }
                else
                {
                    Console.WriteLine("Ma giao vien khong duoc de trong.");
                }
            }
        }

        public string HoTen
        {
            get { return hoTen; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    hoTen = value;
                }
                else
                {
                    Console.WriteLine("Ho ten giao vien khong duoc de trong.");
                }
            }
        }

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        public string SoDienThoai
        {
            get { return soDienThoai; }
            set { soDienThoai = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }

        // **********************************************
        // ⬅️ PROPERTY MỚI: Thuộc tính này sẽ được gán chuỗi chuyên môn từ DAO
        public string DanhSachChuyenMon
        {
            get { return danhSachChuyenMon; }
            set { danhSachChuyenMon = value; }
        }
        // **********************************************
    }
}