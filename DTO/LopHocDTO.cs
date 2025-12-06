using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class LopHocDTO
    {
        private int maLop;
        private string tenLop;
        private int maKhoi;
        private string maGiaoVienChuNhiem;

        public LopHocDTO() { }

        public LopHocDTO(int maLop, string tenLop, int maKhoi, string maGiaoVienChuNhiem)
        {
            this.maLop = maLop;
            this.tenLop = tenLop;
            this.maKhoi = maKhoi;
            this.maGiaoVienChuNhiem = maGiaoVienChuNhiem;
        }

        public int MaLop
        {
            get { return maLop; }
            set { maLop = value; }
        }

        public string TenLop
        {
            get { return tenLop; }
            set { tenLop = value; }
        }

        public int MaKhoi
        {
            get { return maKhoi; }
            set { maKhoi = value; }
        }

        public string MaGiaoVienChuNhiem
        {
            get { return maGiaoVienChuNhiem; }
            set { maGiaoVienChuNhiem = value; }
        }
    }
}