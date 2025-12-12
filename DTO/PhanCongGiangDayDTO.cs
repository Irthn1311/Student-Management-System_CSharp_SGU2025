using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO chính cho phân công giảng dạy - chuyển dữ liệu giữa các tầng
    /// </summary>
    public class PhanCongGiangDayDTO
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

    /// <summary>
    /// ViewModel cho hiển thị phân công giảng dạy trên DataGridView
    /// </summary>
    public class PhanCongGiangDayViewModel
    {
        public int MaPhanCong { get; set; }
        public string GiaoVien { get; set; }
        public string MonHoc { get; set; }
        public string Lop { get; set; }
        public string HocKy { get; set; }
        public string ThoiGian { get; set; }
        public string ThaoTac { get; set; } = "";
        
        // Lưu các mã gốc để dùng khi cần
        public string MaGiaoVien { get; set; }
        public int MaMonHoc { get; set; }
        public int MaLop { get; set; }
        public int MaHocKy { get; set; }
    }

    /// <summary>
    /// DTO for PhanCongCandidate - used for temporary assignment storage
    /// </summary>
    public class PhanCongCandidateDTO
    {
        public int MaLop { get; set; }
        public int MaMonHoc { get; set; }
        public string MaGiaoVien { get; set; } = string.Empty;
        public int SoTietTuan { get; set; }
        public int Score { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}