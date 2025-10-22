using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class HocKyDTO
    {
        private int maHocKy;
        private string tenHocKy;
        private string maNamHoc;
        private string tenNamHoc;
        private string trangThai;
        private DateTime ngayBD;
        private DateTime ngayKT;

        public HocKyDTO() { }

        public HocKyDTO(int maHocKy, string tenHocKy, string maNamHoc, string tenNamHoc, DateTime ngayBD, DateTime ngayKT)
        {
            this.maHocKy = maHocKy;
            this.tenHocKy = tenHocKy;
            this.maNamHoc = maNamHoc;
            this.tenNamHoc = tenNamHoc;
            this.ngayBD = ngayBD;
            this.ngayKT = ngayKT;
        }

        public int MaHocKy
        {
            get { return maHocKy; }
            set { maHocKy = value; }
        }

        public string TenHocKy
        {
            get { return tenHocKy; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    tenHocKy = value;
                else
                    throw new ArgumentException("Tên học kỳ không được để trống");
            }
        }

        public string MaNamHoc
        {
            get { return maNamHoc; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    maNamHoc = value;
                else
                    throw new ArgumentException("Mã năm học không được để trống");
            }
        }

        public string TenNamHoc
        {
            get { return tenNamHoc; }
            set { tenNamHoc = value; }
        }

        public DateTime NgayBD
        {
            get { return ngayBD; }
            set { ngayBD = value; }
        }

        public DateTime NgayKT
        {
            get { return ngayKT; }
            set { ngayKT = value; }
        }
    }
}