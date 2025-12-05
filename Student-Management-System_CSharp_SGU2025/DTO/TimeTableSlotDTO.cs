using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO cho ô thời khóa biểu (TimeTable Slot)
    /// Dùng để hiển thị từng ô (Cell) trên lưới TKB
    /// </summary>
    public class TimeTableSlotDTO
    {
        // Backing fields
        private int _maThoiKhoaBieu;
        private int _maPhanCong;
        private int _thu;   // 2->6 (Thứ 2 đến Thứ 6)
        private int _tiet;  // 1->10 (Tiết 1 đến Tiết 10)
        private string _tenLop;
        private string _tenMon;
        private string _tenGiaoVien;
        private string _maGiaoVien; // Dùng để check logic
        private int _maLop;         // Dùng để check logic

        // Constructor mặc định
        public TimeTableSlotDTO() { }

        // Constructor đầy đủ tham số
        public TimeTableSlotDTO(int maThoiKhoaBieu, int maPhanCong, int thu, int tiet,
                               string tenLop, string tenMon, string tenGiaoVien,
                               string maGiaoVien, int maLop)
        {
            this._maThoiKhoaBieu = maThoiKhoaBieu;
            this._maPhanCong = maPhanCong;
            this._thu = thu;
            this._tiet = tiet;
            this._tenLop = tenLop;
            this._tenMon = tenMon;
            this._tenGiaoVien = tenGiaoVien;
            this._maGiaoVien = maGiaoVien;
            this._maLop = maLop;
        }

        // Full Properties
        public int MaThoiKhoaBieu
        {
            get { return _maThoiKhoaBieu; }
            set
            {
                if (value >= 0)
                    _maThoiKhoaBieu = value;
                else
                    throw new ArgumentException("Mã thời khóa biểu phải lớn hơn hoặc bằng 0");
            }
        }

        public int MaPhanCong
        {
            get { return _maPhanCong; }
            set
            {
                if (value > 0)
                    _maPhanCong = value;
                else
                    throw new ArgumentException("Mã phân công phải lớn hơn 0");
            }
        }

        public int Thu
        {
            get { return _thu; }
            set
            {
                // Cho phép Thứ 2 đến Thứ 7 (2..7) để hỗ trợ 6 ngày học/tuần
                if (value >= 2 && value <= 7)
                    _thu = value;
                else
                    throw new ArgumentException("Thứ phải từ 2 đến 7 (Thứ 2 đến Thứ 7)");
            }
        }

        public int Tiet
        {
            get { return _tiet; }
            set
            {
                if (value >= 1 && value <= 10)
                    _tiet = value;
                else
                    throw new ArgumentException("Tiết phải từ 1 đến 10");
            }
        }

        public string TenLop
        {
            get { return _tenLop ?? string.Empty; }
            set { _tenLop = value ?? string.Empty; }
        }

        public string TenMon
        {
            get { return _tenMon ?? string.Empty; }
            set { _tenMon = value ?? string.Empty; }
        }

        public string TenGiaoVien
        {
            get { return _tenGiaoVien ?? string.Empty; }
            set { _tenGiaoVien = value ?? string.Empty; }
        }

        public string MaGiaoVien
        {
            get { return _maGiaoVien ?? string.Empty; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _maGiaoVien = value;
                else
                    throw new ArgumentException("Mã giáo viên không được để trống");
            }
        }

        public int MaLop
        {
            get { return _maLop; }
            set
            {
                if (value > 0)
                    _maLop = value;
                else
                    throw new ArgumentException("Mã lớp phải lớn hơn 0");
            }
        }
    }
}

