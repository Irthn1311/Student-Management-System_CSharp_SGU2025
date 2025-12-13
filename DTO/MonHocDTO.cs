using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class MonHocDTO
    {
        public int maMon;
        public string tenMon;
        public int soTiet;
        public string ghiChu;
        public string namHocBatDau; // Năm học bắt đầu áp dụng môn học này

        public MonHocDTO() { }
        public MonHocDTO(int maMon, string tenMon, int soTiet, string ghiChu)
        {
            this.maMon = maMon;
            this.tenMon = tenMon;
            this.soTiet = soTiet;
            this.ghiChu = ghiChu;
            this.namHocBatDau = null;
        }

        public MonHocDTO(int maMon, string tenMon, int soTiet, string ghiChu, string namHocBatDau)
        {
            this.maMon = maMon;
            this.tenMon = tenMon;
            this.soTiet = soTiet;
            this.ghiChu = ghiChu;
            this.namHocBatDau = namHocBatDau;
        }
        public int MaMon
        {
            get { return maMon; }
            set
            {
                if (maMon > 0)
                {
                    maMon = value;
                }
                else
                {
                    Console.WriteLine("Ma mon khong duoc de trong");
                }
            }
        }
        public string TenMon
        {
            get { return tenMon; }
            set
            {
                if (tenMon == "")
                {
                    tenMon = value;
                }
                else
                {
                    Console.WriteLine("Ten mon khong duoc de trong");
                }
            }
        }
        public int SoTiet
        {
            get { return soTiet; }
            set
            {
                if (soTiet > 0)
                {
                    soTiet = value;
                }
                else
                {
                    Console.WriteLine("So tiet phai lon hon 0");
                }
            }
        }
        public string GhiChu
        {
            get { return ghiChu; }
            set
            {
                ghiChu = value;
            }
        }

        public string NamHocBatDau
        {
            get { return namHocBatDau; }
            set
            {
                namHocBatDau = value; // Có thể null hoặc rỗng
            }
        }
    }
}
