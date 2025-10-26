using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class ThongKeDTO
    {
        public float DiemTBChung { get; set; }
        public float DiemTBChungKyTruoc { get; set; }
        public float DiemCaoNhat { get; set; }
        public string HocSinhDiemCaoNhat { get; set; }
        public float DiemThapNhat { get; set; }
        public string HocSinhDiemThapNhat { get; set; }
        public int TongHocSinh { get; set; }
        public int HocSinhDaNhapDiem { get; set; }
        public int HocSinhChuaNhapDiem { get; set; }
    }
}
