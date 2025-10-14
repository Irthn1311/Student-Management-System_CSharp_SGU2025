using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class KhoiLop
    {
        public int maKhoi;
        public string tenKhoi;
        public KhoiLop() { }
        public KhoiLop(int maKhoi, string tenKhoi)
        {
            this.maKhoi = maKhoi;
            this.tenKhoi = tenKhoi;
        }
        ~KhoiLop()
        {
            Console.WriteLine("Huy doi tuong KhoiLop");
        }
        public int MaKhoi
        {
            get { return maKhoi; }
            set
            {
                if (maKhoi >0)
                {
                    maKhoi = value;
                }
                else
                {
                    Console.WriteLine("Ma khoi khong duoc de trong");
                }
            }
        }
        public string TenKhoi
        {
            get { return tenKhoi; }
            set
            {
                if (tenKhoi == "")
                {
                    tenKhoi = value;
                }
                else
                {
                    Console.WriteLine("Ten khoi khong duoc de trong");
                }
            }
        }
    }
}
