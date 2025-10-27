using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class DiemSoDTO
    {
        private string maHocSinh;
        private int maMonHoc;
        private int maHocKy;
        private float? diemThuongXuyen;
        private float? diemGiuaKy;
        private float? diemCuoiKy;
        private float? diemTrungBinh;

        public DiemSoDTO() { }

        public DiemSoDTO(string maHocSinh, int maMonHoc, int maHocKy,
                        float? diemThuongXuyen, float? diem15Phut, float? diemGiuaKy,
                        float? diemCuoiKy, float? diemTrungBinh)
        {
            this.maHocSinh = maHocSinh;
            this.maMonHoc = maMonHoc;
            this.maHocKy = maHocKy;
            this.diemThuongXuyen = diemThuongXuyen;
            this.diemGiuaKy = diemGiuaKy;
            this.diemCuoiKy = diemCuoiKy;
            this.diemTrungBinh = diemTrungBinh;
        }

        ~DiemSoDTO()
        {
            Console.WriteLine("Huy doi tuong DiemSoDTO");
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

        public int MaMonHoc
        {
            get { return maMonHoc; }
            set
            {
                if (value > 0)
                {
                    maMonHoc = value;
                }
                else
                {
                    Console.WriteLine("Ma mon hoc phai lon hon 0");
                }
            }
        }

        public int MaHocKy
        {
            get { return maHocKy; }
            set
            {
                if (value > 0)
                {
                    maHocKy = value;
                }
                else
                {
                    Console.WriteLine("Ma hoc ky phai lon hon 0");
                }
            }
        }

        public float? DiemThuongXuyen
        {
            get { return diemThuongXuyen; }
            set
            {
                if (value.HasValue && (value < 0 || value > 10))
                {
                    Console.WriteLine("Diem mieng phai nam trong khoang 0-10");
                }
                else
                {
                    diemThuongXuyen = value;
                }
            }
        }

        public float? DiemGiuaKy
        {
            get { return diemGiuaKy; }
            set
            {
                if (value.HasValue && (value < 0 || value > 10))
                {
                    Console.WriteLine("Diem giua ky phai nam trong khoang 0-10");
                }
                else
                {
                    diemGiuaKy = value;
                }
            }
        }

        public float? DiemCuoiKy
        {
            get { return diemCuoiKy; }
            set
            {
                if (value.HasValue && (value < 0 || value > 10))
                {
                    Console.WriteLine("Diem cuoi ky phai nam trong khoang 0-10");
                }
                else
                {
                    diemCuoiKy = value;
                }
            }
        }

        public float? DiemTrungBinh
        {
            get { return diemTrungBinh; }
            set
            {
                if (value.HasValue && (value < 0 || value > 10))
                {
                    Console.WriteLine("Diem trung binh phai nam trong khoang 0-10");
                }
                else
                {
                    diemTrungBinh = value;
                }
            }
        }
    }
}