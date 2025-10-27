using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class NamHocDTO
    {
        private string maNamHoc;
        private string tenNamHoc;
        private DateTime ngayBD;
        private DateTime ngayKT;

        public NamHocDTO() { }

        public NamHocDTO(string maNamHoc, string tenNamHoc, DateTime ngayBD, DateTime ngayKT)
        {
            this.maNamHoc = maNamHoc;
            this.tenNamHoc = tenNamHoc;
            this.ngayBD = ngayBD;
            this.ngayKT = ngayKT;
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
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    tenNamHoc = value;
                else
                    throw new ArgumentException("Tên năm học không được để trống");
            }
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