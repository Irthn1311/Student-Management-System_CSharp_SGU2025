using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class MonHocDTO
    {
        public int maMon;
        public string tenMon;
        public int soTiet;
        public MonHocDTO() { }
        public MonHocDTO(int maMon, string tenMon, int soTiet)
        {
            this.maMon = maMon;
            this.tenMon = tenMon;
            this.soTiet = soTiet;
        }
        ~MonHocDTO()
        {
            Console.WriteLine("Huy doi tuong MonHocDTO");
        }
        public int MaMon
        {
            get { return maMon; }
            set
            {
                if (maMon >0)
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
                if(tenMon == "")
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
    }
}
