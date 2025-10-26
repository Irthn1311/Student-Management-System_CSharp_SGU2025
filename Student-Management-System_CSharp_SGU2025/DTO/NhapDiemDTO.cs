using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO để hiển thị dữ liệu trên bảng Nhập Điểm
    /// </summary>
    public class NhapDiemDTO
    {
        private string maHocSinh;
        private string hoTen;
        private float? diemTX;  // Điểm thường xuyên (trung bình DiemMieng + Diem15Phut)
        private float? diemGK;   // Điểm giữa kỳ
        private float? diemCK;   // Điểm cuối kỳ
        private float? diemTB;   // Điểm trung bình

        public NhapDiemDTO() { }

        public NhapDiemDTO(string maHocSinh, string hoTen, float? diemTX,
                          float? diemGK, float? diemCK, float? diemTB)
        {
            this.maHocSinh = maHocSinh;
            this.hoTen = hoTen;
            this.diemTX = diemTX;
            this.diemGK = diemGK;
            this.diemCK = diemCK;
            this.diemTB = diemTB;
        }

        ~NhapDiemDTO()
        {
            Console.WriteLine("Huy doi tuong NhapDiemDTO");
        }

        public string MaHocSinh
        {
            get { return maHocSinh; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Ma hoc sinh khong duoc de trong");
                }
                else
                {
                    maHocSinh = value;
                }
            }
        }

        public string HoTen
        {
            get { return hoTen; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Ho ten khong duoc de trong");
                }
                else
                {
                    hoTen = value;
                }
            }
        }

        public float? DiemTX
        {
            get { return diemTX; }
            set
            {
                if (value.HasValue && (value < 0 || value > 10))
                {
                    Console.WriteLine("Diem TX phai nam trong khoang 0-10");
                }
                else
                {
                    diemTX = value;
                }
            }
        }

        public float? DiemGK
        {
            get { return diemGK; }
            set
            {
                if (value.HasValue && (value < 0 || value > 10))
                {
                    Console.WriteLine("Diem GK phai nam trong khoang 0-10");
                }
                else
                {
                    diemGK = value;
                }
            }
        }

        public float? DiemCK
        {
            get { return diemCK; }
            set
            {
                if (value.HasValue && (value < 0 || value > 10))
                {
                    Console.WriteLine("Diem CK phai nam trong khoang 0-10");
                }
                else
                {
                    diemCK = value;
                }
            }
        }

        public float? DiemTB
        {
            get { return diemTB; }
            set
            {
                if (value.HasValue && (value < 0 || value > 10))
                {
                    Console.WriteLine("Diem TB phai nam trong khoang 0-10");
                }
                else
                {
                    diemTB = value;
                }
            }
        }
    }

    public class XemBangDiemDTO
    {
        public string MaHocSinh { get; set; }
        public string HoTen { get; set; }
        public float? DiemToan { get; set; }
        public float? DiemVan { get; set; }
        public float? DiemAnh { get; set; }
        public float? DiemLy { get; set; }
        public float? DiemHoa { get; set; }
        public float? DiemTB { get; set; }
    }

}