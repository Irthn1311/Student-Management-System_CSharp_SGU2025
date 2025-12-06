using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class ChiTietDiemDTO
    {
        public string MaHocSinh { get; set; }
        public string HoTen { get; set; }
        public float? DiemTB { get; set; }

        // Dictionary để lưu điểm theo mã môn học
        // Key: MaMonHoc, Value: DiemMonHocDTO
        public Dictionary<int, DiemMonHocDTO> DiemCacMon { get; set; }

        public ChiTietDiemDTO()
        {
            DiemCacMon = new Dictionary<int, DiemMonHocDTO>();
        }
    }

    // DTO phụ để lưu thông tin điểm của từng môn
    public class DiemMonHocDTO
    {
        public int MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public float? DiemTrungBinh { get; set; }
    }
}