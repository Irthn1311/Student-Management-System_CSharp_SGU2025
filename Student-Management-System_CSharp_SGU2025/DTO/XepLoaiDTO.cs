using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO cho bảng XepLoai - Xếp loại học lực của học sinh theo học kỳ
    /// </summary>
    public class XepLoaiDTO
    {
        // Khóa chính phức hợp (Composite Primary Key)
        public int MaHocSinh { get; set; }
        public int MaHocKy { get; set; }

        // Thông tin xếp loại
        public string HocLuc { get; set; }  // Giỏi, Khá, Trung bình, Yếu, Kém
        public string GhiChu { get; set; }

        // Constructor mặc định
        public XepLoaiDTO()
        {
            HocLuc = string.Empty;
            GhiChu = string.Empty;
        }

        // Constructor đầy đủ
        public XepLoaiDTO(int maHocSinh, int maHocKy, string hocLuc, string ghiChu = "")
        {
            MaHocSinh = maHocSinh;
            MaHocKy = maHocKy;
            HocLuc = hocLuc;
            GhiChu = ghiChu;
        }

        /// <summary>
        /// Kiểm tra xem học lực có hợp lệ không
        /// </summary>
        public bool IsValidHocLuc()
        {
            string[] validHocLuc = { "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" };
            return Array.Exists(validHocLuc, hl => hl.Equals(HocLuc, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Chuyển đổi học lực sang thang điểm 4
        /// </summary>
        public float GetGradePoint()
        {
            switch (HocLuc)
            {
                case "Giỏi": return 4.0f;
                case "Khá": return 3.0f;
                case "Trung bình": return 2.0f;
                case "Yếu": return 1.0f;
                case "Kém": return 0.0f;
                default: return 0.0f;
            }
        }

        public override string ToString()
        {
            return $"HS{MaHocSinh} - HK{MaHocKy}: {HocLuc}";
        }
    }
}
