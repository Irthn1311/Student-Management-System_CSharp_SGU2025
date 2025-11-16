using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class XepLoaiDTO
    {
        public int MaHocSinh { get; set; }
        public string HoTen { get; set; }
        public string TenLop { get; set; }
        public float? DiemTB { get; set; }
        public string HocLuc { get; set; }
        public float? DiemToan { get; set; }
        public float? DiemVan { get; set; }
        public float? DiemAnh { get; set; }
        public float? DiemThapNhat { get; set; }
        // Thêm vào XepLoaiDTO
        public string HanhKiem { get; set; }
        public string XepLoaiTongKet { get; set; }
        public string GhiChu { get; set; }
        public XepLoaiDTO()
        {
        }

        public XepLoaiDTO(int maHocSinh, string hoTen, string tenLop, float? diemTB,
            string hocLuc, float? diemToan, float? diemVan, float? diemAnh, float? diemThapNhat)
        {
            MaHocSinh = maHocSinh;
            HoTen = hoTen;
            TenLop = tenLop;
            DiemTB = diemTB;
            HocLuc = hocLuc;
            DiemToan = diemToan;
            DiemVan = diemVan;
            DiemAnh = diemAnh;
            DiemThapNhat = diemThapNhat;
        }
    }
}