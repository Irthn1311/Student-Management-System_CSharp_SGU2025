using System;
using System.Text;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    // Đổi tên class HanhKiem thành HanhKiemDTO cho nhất quán
    public class HanhKiemDTO
    {
        // Auto-implemented properties (Thuộc tính tự động)
        public int MaHocSinh { get; set; }
        public int MaHocKy { get; set; }
        public string XepLoai { get; set; }
        public string NhanXet { get; set; }

        // Constructors (Hàm khởi tạo)
        public HanhKiemDTO() { }

        public HanhKiemDTO(int maHocSinh, int maHocKy, string xepLoai, string nhanXet)
        {
            this.MaHocSinh = maHocSinh;
            this.MaHocKy = maHocKy;
            this.XepLoai = xepLoai;
            this.NhanXet = nhanXet;
        }
    }
}
