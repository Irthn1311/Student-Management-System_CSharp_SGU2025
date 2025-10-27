using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO cho thống kê điểm số
    /// </summary>
    public class ThongKeDTO
    {
        // Backing fields
        private float _diemTBChung;
        private float _diemCaoNhat;
        private float _diemThapNhat;
        private int _tongHocSinh;
        private int _hocSinhDaNhapDiem;
        private int _hocSinhChuaNhapDiem;
        private string _hocSinhDiemCaoNhat;
        private string _hocSinhDiemThapNhat;
        private float _diemTBChungKyTruoc;

        /// <summary>
        /// Điểm trung bình chung của tất cả học sinh
        /// </summary>
        public float DiemTBChung
        {
            get { return _diemTBChung; }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    _diemTBChung = value;
                }
                else
                {
                    throw new ArgumentException("Điểm trung bình chung phải nằm trong khoảng 0-10");
                }
            }
        }

        /// <summary>
        /// Điểm cao nhất trong học kỳ
        /// </summary>
        public float DiemCaoNhat
        {
            get { return _diemCaoNhat; }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    _diemCaoNhat = value;
                }
                else
                {
                    throw new ArgumentException("Điểm cao nhất phải nằm trong khoảng 0-10");
                }
            }
        }

        /// <summary>
        /// Điểm thấp nhất trong học kỳ
        /// </summary>
        public float DiemThapNhat
        {
            get { return _diemThapNhat; }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    _diemThapNhat = value;
                }
                else
                {
                    throw new ArgumentException("Điểm thấp nhất phải nằm trong khoảng 0-10");
                }
            }
        }

        /// <summary>
        /// Tổng số học sinh trong hệ thống
        /// </summary>
        public int TongHocSinh
        {
            get { return _tongHocSinh; }
            set
            {
                if (value >= 0)
                {
                    _tongHocSinh = value;
                }
                else
                {
                    throw new ArgumentException("Tổng số học sinh không được âm");
                }
            }
        }

        /// <summary>
        /// Số học sinh đã nhập điểm
        /// </summary>
        public int HocSinhDaNhapDiem
        {
            get { return _hocSinhDaNhapDiem; }
            set
            {
                if (value >= 0 && value <= _tongHocSinh)
                {
                    _hocSinhDaNhapDiem = value;
                }
                else
                {
                    throw new ArgumentException("Số học sinh đã nhập điểm không hợp lệ");
                }
            }
        }

        /// <summary>
        /// Số học sinh chưa nhập điểm
        /// </summary>
        public int HocSinhChuaNhapDiem
        {
            get { return _hocSinhChuaNhapDiem; }
            set
            {
                if (value >= 0 && value <= _tongHocSinh)
                {
                    _hocSinhChuaNhapDiem = value;
                }
                else
                {
                    throw new ArgumentException("Số học sinh chưa nhập điểm không hợp lệ");
                }
            }
        }

        /// <summary>
        /// Tên học sinh có điểm cao nhất
        /// </summary>
        public string HocSinhDiemCaoNhat
        {
            get { return _hocSinhDiemCaoNhat; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _hocSinhDiemCaoNhat = value.Trim();
                }
                else
                {
                    _hocSinhDiemCaoNhat = "Chưa có";
                }
            }
        }

        /// <summary>
        /// Tên học sinh có điểm thấp nhất
        /// </summary>
        public string HocSinhDiemThapNhat
        {
            get { return _hocSinhDiemThapNhat; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _hocSinhDiemThapNhat = value.Trim();
                }
                else
                {
                    _hocSinhDiemThapNhat = "Chưa có";
                }
            }
        }

        /// <summary>
        /// Điểm trung bình chung của học kỳ trước
        /// </summary>
        public float DiemTBChungKyTruoc
        {
            get { return _diemTBChungKyTruoc; }
            set
            {
                if (value >= 0 && value <= 10)
                {
                    _diemTBChungKyTruoc = value;
                }
                else
                {
                    _diemTBChungKyTruoc = 0; // Mặc định là 0 nếu không có dữ liệu
                }
            }
        }

        /// <summary>
        /// Constructor mặc định
        /// </summary>
        public ThongKeDTO()
        {
            _diemTBChung = 0f;
            _diemCaoNhat = 0f;
            _diemThapNhat = 0f;
            _tongHocSinh = 0;
            _hocSinhDaNhapDiem = 0;
            _hocSinhChuaNhapDiem = 0;
            _hocSinhDiemCaoNhat = "Chưa có";
            _hocSinhDiemThapNhat = "Chưa có";
            _diemTBChungKyTruoc = 0f;
        }

        /// <summary>
        /// Constructor với tham số
        /// </summary>
        public ThongKeDTO(float diemTBChung, float diemCaoNhat, float diemThapNhat, 
                         int tongHocSinh, int hocSinhDaNhapDiem, int hocSinhChuaNhapDiem,
                         string hocSinhDiemCaoNhat, string hocSinhDiemThapNhat, float diemTBChungKyTruoc)
        {
            DiemTBChung = diemTBChung;
            DiemCaoNhat = diemCaoNhat;
            DiemThapNhat = diemThapNhat;
            TongHocSinh = tongHocSinh;
            HocSinhDaNhapDiem = hocSinhDaNhapDiem;
            HocSinhChuaNhapDiem = hocSinhChuaNhapDiem;
            HocSinhDiemCaoNhat = hocSinhDiemCaoNhat;
            HocSinhDiemThapNhat = hocSinhDiemThapNhat;
            DiemTBChungKyTruoc = diemTBChungKyTruoc;
        }

        /// <summary>
        /// Tính phần trăm học sinh đã nhập điểm
        /// </summary>
        public float TinhPhanTramDaNhapDiem()
        {
            if (_tongHocSinh == 0)
                return 0f;
            
            return (float)_hocSinhDaNhapDiem / _tongHocSinh * 100;
        }

        /// <summary>
        /// Tính phần trăm học sinh chưa nhập điểm
        /// </summary>
        public float TinhPhanTramChuaNhapDiem()
        {
            if (_tongHocSinh == 0)
                return 0f;
            
            return (float)_hocSinhChuaNhapDiem / _tongHocSinh * 100;
        }

        /// <summary>
        /// Tính chênh lệch điểm so với học kỳ trước
        /// </summary>
        public float TinhChenhLechDiemKyTruoc()
        {
            return _diemTBChung - _diemTBChungKyTruoc;
        }

        /// <summary>
        /// Kiểm tra xem có cải thiện điểm so với học kỳ trước không
        /// </summary>
        public bool CoCaiThienDiem()
        {
            return TinhChenhLechDiemKyTruoc() > 0;
        }

        /// <summary>
        /// Kiểm tra xem có giảm điểm so với học kỳ trước không
        /// </summary>
        public bool CoGiamDiem()
        {
            return TinhChenhLechDiemKyTruoc() < 0;
        }

        /// <summary>
        /// Lấy mô tả trạng thái điểm
        /// </summary>
        public string LayMoTaTrangThaiDiem()
        {
            if (_diemTBChung >= 8.0f)
                return "Xuất sắc";
            else if (_diemTBChung >= 6.5f)
                return "Khá";
            else if (_diemTBChung >= 5.0f)
                return "Trung bình";
            else if (_diemTBChung > 0f)
                return "Yếu";
            else
                return "Chưa có dữ liệu";
        }

        /// <summary>
        /// Override ToString để hiển thị thông tin thống kê
        /// </summary>
        public override string ToString()
        {
            return $"Thống kê điểm học kỳ:\n" +
                   $"- Điểm TB chung: {_diemTBChung:F1}\n" +
                   $"- Điểm cao nhất: {_diemCaoNhat:F1} ({_hocSinhDiemCaoNhat})\n" +
                   $"- Điểm thấp nhất: {_diemThapNhat:F1} ({_hocSinhDiemThapNhat})\n" +
                   $"- Tổng học sinh: {_tongHocSinh}\n" +
                   $"- Đã nhập điểm: {_hocSinhDaNhapDiem} ({TinhPhanTramDaNhapDiem():F1}%)\n" +
                   $"- Chưa nhập điểm: {_hocSinhChuaNhapDiem} ({TinhPhanTramChuaNhapDiem():F1}%)";
        }
    }
}
