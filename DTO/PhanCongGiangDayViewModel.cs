namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// ViewModel cho hiển thị phân công giảng dạy trên DataGridView
    /// </summary>
    public class PhanCongGiangDayViewModel
    {
        public int MaPhanCong { get; set; }
        public string GiaoVien { get; set; }
        public string MonHoc { get; set; }
        public string Lop { get; set; }
        public string HocKy { get; set; }
        public string ThoiGian { get; set; }
        public string ThaoTac { get; set; } = "";
        
        // Lưu các mã gốc để dùng khi cần
        public string MaGiaoVien { get; set; }
        public int MaMonHoc { get; set; }
        public int MaLop { get; set; }
        public int MaHocKy { get; set; }
    }
}

