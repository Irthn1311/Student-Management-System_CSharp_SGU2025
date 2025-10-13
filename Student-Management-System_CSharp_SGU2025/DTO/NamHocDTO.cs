using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class NamHocDTO
    {
        public string maNamHoc;
        public string tenNamHoc;
        public DateTime ngayBD;
        public DateTime ngayKT;
        public NamHocDTO() { }
        public NamHocDTO(string maNamHoc, string tenNamHoc, DateTime ngayBD, DateTime ngayKT)
        {
            this.maNamHoc = maNamHoc;
            this.tenNamHoc = tenNamHoc;
            this.ngayBD = ngayBD;
            this.ngayKT = ngayKT;
        }   
        ~NamHocDTO()
        {
            Console.WriteLine("Huy doi tuong NamHocDTO");
        }
        public string MaNamHoc
        {
            get { return maNamHoc; }
            set
            {
                if (maNamHoc == "")
                {
                    maNamHoc = value;
                }
                else
                {
                    Console.WriteLine("Ma nam hoc khong duoc de trong");
                }
            }
        }
        public string TenNamHoc
        {
            get { return tenNamHoc; }
            set
            {
                if (tenNamHoc == "")
                {
                    tenNamHoc = value;
                }
                else
                {
                    Console.WriteLine("Ten nam hoc khong duoc de trong");
                }
            }
        }
        public DateTime NgayBD
        {
            get { return ngayBD; }
            set
            {
                if (ngayBD < ngayKT)
                {
                    ngayBD = value;
                }
                else
                {
                    Console.WriteLine("Ngay bat dau phai truoc ngay ket thuc");
                }
            }
        }
        public DateTime NgayKT {             get { return ngayKT; }
            set
            {
                if (ngayKT > ngayBD)
                {
                    ngayKT = value;
                }
                else
                {
                    Console.WriteLine("Ngay ket thuc phai sau ngay bat dau");
                }
            }
        }
    }
}
