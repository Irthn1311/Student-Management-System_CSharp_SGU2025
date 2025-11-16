using System;
using System.Text;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class HanhKiemDTO
    {
        // Fields (Thuộc tính cơ bản)
        private int maHocSinh;
        private int maHocKy;
        private string xepLoai;
        private string nhanXet;

        // Constructors (Hàm khởi tạo)
        public HanhKiemDTO() { }

        public HanhKiemDTO(int maHocSinh, int maHocKy, string xepLoai, string nhanXet)
        {
            this.MaHocSinh = maHocSinh;
            this.MaHocKy = maHocKy;
            this.XepLoai = xepLoai;
            this.NhanXet = nhanXet;
        }

        // Destructor (Hàm hủy)
        ~HanhKiemDTO()
        {
            // Console.WriteLine("Huy doi tuong HanhKiemDTO");
        }

        // Properties (Thuộc tính)
        public int MaHocSinh
        {
            get { return maHocSinh; }
            set
            {
                // int is a value type and cannot be null or empty, so just check for a valid value (e.g., > 0)
                if (value > 0)
                {
                    maHocSinh = value;
                }
                else
                {
                    // Logic xử lý lỗi: In ra console (Giống ví dụ KhoiLop)
                    Console.WriteLine("Ma hoc sinh phai lon hon 0.");
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
                    Console.WriteLine("Ma hoc ky phai lon hon 0.");
                }
            }
        }

        public string XepLoai
        {
            get { return xepLoai; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    xepLoai = value;
                }
                else
                {
                    Console.WriteLine("Xep loai hanh kiem khong duoc de trong.");
                }
            }
        }

        public string NhanXet
        {
            get { return nhanXet; }
            set
            {
                // Cho phép null hoặc empty nếu không bắt buộc
                nhanXet = value;
            }
        }
    }
}