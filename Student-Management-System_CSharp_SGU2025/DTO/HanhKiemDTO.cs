using System;
using System.Text;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    // Đổi tên class HanhKiem thành HanhKiemDTO cho nhất quán
    internal class HanhKiemDTO
    {
        // Fields (Thuộc tính cơ bản)
        private int maHocSinh; // Sửa từ string sang int
        private int maHocKy;
        private string xepLoai;
        private string nhanXet;

        // Constructors (Hàm khởi tạo)
        public HanhKiemDTO() { }

        public HanhKiemDTO(int maHocSinh, int maHocKy, string xepLoai, string nhanXet) // Sửa string sang int
        {
            this.MaHocSinh = maHocSinh;
            this.MaHocKy = maHocKy;
            this.XepLoai = xepLoai;
            this.NhanXet = nhanXet;
        }

        // Destructor (Hàm hủy)
        ~HanhKiemDTO()
        {
        }

        // Properties (Thuộc tính)
        public int MaHocSinh // Sửa từ string sang int
        {
            get { return maHocSinh; }
            set
            {
                if (value > 0)
                {
                    maHocSinh = value;
                }
                else
                {
                    // Sửa Console.WriteLine thành throw Exception
                    throw new ArgumentException("Mã học sinh phải là số dương.");
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
                    // Sửa Console.WriteLine thành throw Exception
                    throw new ArgumentException("Mã học kỳ phải là số dương.");
                }
            }
        }

        public string XepLoai
        {
            get { return xepLoai; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    xepLoai = value.Trim();
                }
                else
                {
                    // Sửa Console.WriteLine thành throw Exception
                    throw new ArgumentException("Xếp loại hạnh kiểm không được để trống.");
                }
            }
        }

        public string NhanXet
        {
            get { return nhanXet; }
            set
            {
                // Cho phép null, nhưng nếu có giá trị thì Trim()
                nhanXet = value?.Trim();
            }
        }
    }
}