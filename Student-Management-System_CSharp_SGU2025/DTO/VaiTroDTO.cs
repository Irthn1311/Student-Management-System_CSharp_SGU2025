using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class VaiTroDTO
    {
        public string MaVaiTro { get; set; }
        public string TenVaiTro { get; set; }
        public string MoTa { get; set; }
    }

    public class ChucNangDTO
    {
        public string MaChucNang { get; set; }
        public string TenChucNang { get; set; }
        public string MoTa { get; set; }
    }

    public class HanhDongDTO
    {
        public string HanhDong { get; set; }
        public bool IsChecked { get; set; }
    }

    public class VaiTroChucNangHanhDongDTO
    {
        public string MaVaiTro { get; set; }
        public string MaChucNang { get; set; }
        public string HanhDong { get; set; }
    }

    public class ChucNangHanhDongDTO
    {
        public string MaChucNang { get; set; }
        public string TenChucNang { get; set; }
        public bool CoQuyenXem { get; set; }
        public bool CoQuyenThem { get; set; }
        public bool CoQuyenSua { get; set; }
        public bool CoQuyenXoa { get; set; }
    }
}