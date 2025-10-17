using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class PhanCongGiangDay
    {
        private int maPhanCong;
        private int maLop;
        private int maGiaoVien;
        private int maMonHoc;
        private int maHocKy;
        private DateTime tuNgay;
        private DateTime denNgay;
       
        public PhanCongGiangDay() { }
        public PhanCongGiangDay(int maPhanCong, int maLop, int maGiaoVien, int maMonHoc, int maHocKy, DateTime tuNgay, DateTime denNgay)
        {
            this.maPhanCong = maPhanCong;
            this.maLop = maLop;
            this.maGiaoVien = maGiaoVien;
            this.maMonHoc = maMonHoc;
            this.maHocKy = maHocKy;
            this.tuNgay = tuNgay;
            this.denNgay = denNgay;
            
        }
        ~PhanCongGiangDay()
        {
            Console.WriteLine("Huy doi tuong PhanCongGiangDay");
        }
        public int MaPhanCong
        {
            get { return maPhanCong; }
            set
            {
                if (maPhanCong > 0)
                {
                    maPhanCong = value;
                }
                else
                {
                    Console.WriteLine("Ma phan cong phai lon hon 0");
                }
            }
        }
        public int MaLop
        {
            get { return maLop; }
            set
            {
                if (maLop > 0)
                {
                    maLop = value;
                }
                else
                {
                    Console.WriteLine("Ma lop phai lon hon 0");
                }
            }
        }
        public int MaGiaoVien
        {
            get { return maGiaoVien; }
            set
            {
                if (maGiaoVien > 0)
                {
                    maGiaoVien = value;
                }
                else
                {
                    Console.WriteLine("Ma giao vien phai lon hon 0");
                }
            }
        }
        public int MaMonHoc
        {
            get { return maMonHoc; }
            set
            {
                if (maMonHoc > 0)
                {
                    maMonHoc = value;
                }
                else
                {
                    Console.WriteLine("Ma mon hoc phai lon hon 0");
                }
            }
        }
        public int MaHocKy
        {
            get { return maHocKy; }
            set
            {
                if (maHocKy > 0)
                {
                    maHocKy = value;
                }
                else
                {
                    Console.WriteLine("Ma hoc ky phai lon hon 0");
                }
            }
        }
        public DateTime TuNgay
        {
            get { return tuNgay; }
            set
            {
                if (tuNgay < denNgay)
                {
                    tuNgay = value;
                }
                else
                {
                    Console.WriteLine("Tu ngay phai truoc den ngay");
                }
            }
        }
        public DateTime DenNgay
        {
            get { return denNgay; }
            set
            {
                if (denNgay > tuNgay)
                {
                    denNgay = value;
                }
                else
                {
                    Console.WriteLine("Den ngay phai sau tu ngay");

                }
            }
        }
       
    }
    
}
