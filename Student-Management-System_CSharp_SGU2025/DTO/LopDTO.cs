using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class LopDTO
    {
        public int maLop;
        public string tenLop;
       
        public int maKhoi;
        public int siSo;
        public string maGVCN;
        public LopDTO() { }
        public LopDTO(int maLop, string tenLop, int maKhoi, int siSo, string maGVCN)
        {
            this.maLop = maLop;
            this.tenLop = tenLop;
            this.maKhoi = maKhoi;
            this.siSo = siSo;
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
                if (value > 0)
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
                if (!string.IsNullOrEmpty(value))
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
                if (value > 0)
                {
                    maKhoi = value;
                }
                else
                {
                    Console.WriteLine("Ma khoi khong duoc de trong");
                }
            }
        }
        public int SiSo
        {
            get { return siSo; }
            set
            {
                if (value >= 0)
                {
                    siSo = value;
                }
                else
                {
                    Console.WriteLine("Si so khong duoc nho hon 0");
                }
            }
        }
        public string MaGVCN
        {
            get { return maGVCN; }
            set
            {
                if (!string.IsNullOrEmpty(value))
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
