using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class HocSinhDTO
    {
        private string maHocSinh;
        private string hoTen;
        private DateTime? ngaySinh;
        private string gioiTinh;
        private string sdths;
        private string email;
        private string trangThai;

        public HocSinhDTO() { }

        public HocSinhDTO(string maHocSinh, string hoTen, DateTime? ngaySinh,
                          string gioiTinh, string sdths, string email, string trangThai)
        {
            this.maHocSinh = maHocSinh;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.gioiTinh = gioiTinh;
            this.sdths = sdths;
            this.email = email;
            this.trangThai = trangThai;
        }

        ~HocSinhDTO()
        {
            Console.WriteLine("Huy doi tuong HocSinhDTO");
        }

        public string MaHocSinh
        {
            get { return maHocSinh; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Ma hoc sinh khong duoc de trong");
                }
                else
                {
                    maHocSinh = value;
                }
            }
        }

        public string HoTen
        {
            get { return hoTen; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Ho ten khong duoc de trong");
                }
                else
                {
                    hoTen = value;
                }
            }
        }

        public DateTime? NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        public string SDTHS
        {
            get { return sdths; }
            set { sdths = value; }
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