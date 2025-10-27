using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class PhanCongGiangDayDTO
    {
        private int maPhanCong;
        private int maLop;
        private string maGiaoVien;
        private int maMonHoc;
        private int maHocKy;
        private DateTime ngayBatDau;
        private DateTime ngayKetThuc;

        // Constructor mặc định
        public PhanCongGiangDayDTO() { }

        // Constructor đầy đủ tham số
        public PhanCongGiangDayDTO(int maPhanCong, int maLop, string maGiaoVien, int maMonHoc,
                                   int maHocKy, DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            this.maPhanCong = maPhanCong;
            this.maLop = maLop;
            this.maGiaoVien = maGiaoVien;
            this.maMonHoc = maMonHoc;
            this.maHocKy = maHocKy;
            this.ngayBatDau = ngayBatDau;
            this.ngayKetThuc = ngayKetThuc;
        }

        // Properties với validation
        public int MaPhanCong
        {
            get { return maPhanCong; }
            set
            {
                if (value > 0)
                    maPhanCong = value;
                else
                    throw new ArgumentException("Mã phân công phải lớn hơn 0");
            }
        }

        public int MaLop
        {
            get { return maLop; }
            set
            {
                if (value > 0)
                    maLop = value;
                else
                    throw new ArgumentException("Mã lớp phải lớn hơn 0");
            }
        }

        public string MaGiaoVien
        {
            get { return maGiaoVien; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    maGiaoVien = value;
                else
                    throw new ArgumentException("Mã giáo viên không được để trống");
            }
        }

        public int MaMonHoc
        {
            get { return maMonHoc; }
            set
            {
                if (value > 0)
                    maMonHoc = value;
                else
                    throw new ArgumentException("Mã môn học phải lớn hơn 0");
            }
        }

        public int MaHocKy
        {
            get { return maHocKy; }
            set
            {
                if (value > 0)
                    maHocKy = value;
                else
                    throw new ArgumentException("Mã học kỳ phải lớn hơn 0");
            }
        }

        public DateTime NgayBatDau
        {
            get { return ngayBatDau; }
            set { ngayBatDau = value; }
        }

        public DateTime NgayKetThuc
        {
            get { return ngayKetThuc; }
            set
            {
                if (value >= ngayBatDau)
                    ngayKetThuc = value;
                else
                    throw new ArgumentException("Ngày kết thúc phải sau ngày bắt đầu");
            }
        }
    }
}