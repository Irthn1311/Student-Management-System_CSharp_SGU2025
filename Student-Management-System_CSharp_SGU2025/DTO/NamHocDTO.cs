using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class NamHocDTO
    {
        private int maNamHoc;
        private string tenNamHoc;
        private DateTime ngayBD;
        private DateTime ngayKT;

        public NamHocDTO() { }

        public NamHocDTO(int maNamHoc, string tenNamHoc, DateTime ngayBD, DateTime ngayKT)
        {
            this.maNamHoc = maNamHoc;
            this.tenNamHoc = tenNamHoc;
            this.ngayBD = ngayBD;
            this.ngayKT = ngayKT;
        }

        // Destructor
        ~NamHocDTO()
        {
            Console.WriteLine("Hủy đối tượng NamHocDTO");
        }

        public int MaNamHoc
        {
            get { return maNamHoc; }
            set
            {
                if (value > 0)
                    maNamHoc = value;
                else
                    throw new ArgumentException("Mã năm học phải lớn hơn 0");
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
            set
            {
                if (value < ngayKT || ngayKT == default)
                    ngayBD = value;
                else
                    throw new ArgumentException("Ngày bắt đầu phải trước ngày kết thúc");
            }
        }

        public DateTime NgayKT
        {
            get { return ngayKT; }
            set
            {
                if (value > ngayBD || ngayBD == default)
                    ngayKT = value;
                else
                    throw new ArgumentException("Ngày kết thúc phải sau ngày bắt đầu");
            }
        }
    }
}
