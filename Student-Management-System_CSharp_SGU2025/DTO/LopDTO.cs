using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class LopDTO
    {
        public int maLop;
        public string tenLop;
        
        public int maKhoi;
        public string maGVCN;
        public LopDTO() { } 
        public LopDTO(int maLop, string tenLop, int maKhoi, string maGVCN)
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
        public int MaLop
        {
            get { return maLop; }
            set
            {
                if (maLop >0)
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
