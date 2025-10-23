using System;

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

        public GiaoVienDTO() { }

        public GiaoVienDTO(string maGiaoVien, string hoTen, DateTime ngaySinh, string gioiTinh, 
                          string diaChi, string soDienThoai, string email, string trangThai)
        {
            this.maGiaoVien = maGiaoVien;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.gioiTinh = gioiTinh;
            this.diaChi = diaChi;
            this.soDienThoai = soDienThoai;
            this.email = email;
            this.trangThai = trangThai;
        }

        public string MaGiaoVien
        {
            get { return maGiaoVien; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    maGiaoVien = value;
                else
                    throw new ArgumentException("Mã giáo viên không được để trống");
            }
        }

        public string HoTen
        {
            get { return hoTen; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    hoTen = value;
                else
                    throw new ArgumentException("Họ tên không được để trống");
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
    }
}