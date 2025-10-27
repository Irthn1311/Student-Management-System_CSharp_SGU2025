using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class HocKyDTO
    {
        private int maHocKy;
        private string tenHocKy;
        private string maNamHoc;
        private string trangThai;
        private DateTime? ngayBD;
        private DateTime? ngayKT;

        public HocKyDTO() { }

        public HocKyDTO(int maHocKy, string tenHocKy, string maNamHoc,
                        string trangThai, DateTime? ngayBD, DateTime? ngayKT)
        {
            this.maHocKy = maHocKy;
            this.tenHocKy = tenHocKy;
            this.maNamHoc = maNamHoc;
            this.trangThai = trangThai;
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
            set { tenHocKy = value; }
        }

        public string MaNamHoc
        {
            get { return maNamHoc; }
            set { maNamHoc = value; }
        }

        public string TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }

        public DateTime? NgayBD
        {
            get { return ngayBD; }
            set { ngayBD = value; }
        }

        public DateTime? NgayKT
        {
            get { return ngayKT; }
            set { ngayKT = value; }
        }
    }
}