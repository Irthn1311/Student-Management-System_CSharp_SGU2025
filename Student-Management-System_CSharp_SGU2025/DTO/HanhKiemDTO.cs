using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
 class HanhKiemDTO
    {
        public string MaHocSinh { get; set; }
        public int MaHocKy { get; set; }
        public string XepLoai { get; set; } // Ánh xạ trường XepLoai (NVARCHAR)
        public string NhanXet { get; set; } // Ánh xạ trường NhanXet (TEXT)

        // Thuộc tính bổ sung/hiển thị (Dữ liệu từ JOIN)
        public string HoTenHocSinh { get; set; }
        public string TenHocKy { get; set; }
    }
}
