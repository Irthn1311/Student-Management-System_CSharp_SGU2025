using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    class ThoiKhoaBieuDTO
    {
        public int MaThoiKhoaBieu { get; set; }
        public int MaPhanCong { get; set; }
        public string ThuTrongTuan { get; set; }
        public int TietBatDau { get; set; }
        public int SoTiet { get; set; }
        public string PhongHoc { get; set; }

        public string TenMonHoc { get; set; }
        public string TenGiaoVien { get; set; }
        public string TenLop { get; set; }
        public string TenHocKy { get; set; }
    }
}
