using System;
using System.Text;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class HanhKiem
    {
        // Fields (Thuộc tính cơ bản)
        private string maHocSinh;
        private int maHocKy;
        private string xepLoai;
        private string nhanXet;

        // Constructors (Hàm khởi tạo)
        public HanhKiem() { }

        public HanhKiem(string maHocSinh, int maHocKy, string xepLoai, string nhanXet)
        {
            this.MaHocSinh = maHocSinh;
            this.MaHocKy = maHocKy;
            this.XepLoai = xepLoai;
            this.NhanXet = nhanXet;
        }

        // Destructor (Hàm hủy)
        ~HanhKiem()
        {
            // Console.WriteLine("Huy doi tuong HanhKiem");
        }

        // Properties (Thuộc tính)
        public string MaHocSinh
        {
            get { return maHocSinh; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    maHocSinh = value;
                }
                else
                {
                    // Logic xử lý lỗi: In ra console (Giống ví dụ KhoiLop)
                    Console.WriteLine("Ma hoc sinh khong duoc de trong.");
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