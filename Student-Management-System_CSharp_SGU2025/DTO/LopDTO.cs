using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class LopDTO
    {
        public string maLop;
        public string tenLop;
        public string maKhoi;
        public string maGVCN;
        public LopDTO() { } 
        public LopDTO(string maLop, string tenLop, string maKhoi, string maGVCN)
        {
            this.maLop = maLop;
            this.tenLop = tenLop;
            this.maKhoi = maKhoi;
            this.maGVCN = maGVCN;
        }
        ~LopDTO()
        {
            Console.WriteLine("Huy doi tuong LopDTO");
        }
        public string MaLop
        {
            get { return maLop; }
            set
            {
                if (maLop == "")
                {
                    maLop = value;
                }
                else
                {
                    Console.WriteLine("Ma lop khong duoc de trong");
                }
            }
        }
        public string TenLop
        {
            get { return tenLop; }
            set
            {
                if (tenLop == "")
                {
                    tenLop = value;
                }
                else
                {
                    Console.WriteLine("Ten lop khong duoc de trong");
                }
            }
        }   
        public string MaKhoi
        {
            get { return maKhoi; }
            set
            {
                if (maKhoi == "")
                {
                    maKhoi = value;
                }
                else
                {
                    Console.WriteLine("Ma khoi khong duoc de trong");
                }
            }
        }
        public string MaGVCN
        {
            get { return maGVCN; }
            set
            {
                if (maGVCN == "")
                {
                    maGVCN = value;
                }
                else
                {
                    Console.WriteLine("Ma giao vien khong duoc de trong");
                }
            }
        }
    }
}
