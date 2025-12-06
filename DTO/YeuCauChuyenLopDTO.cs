using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class YeuCauChuyenLopDTO
    {
        public int MaYeuCau { get; set; }
        public int MaHocSinh { get; set; }
        public int MaLopHienTai { get; set; }
        public int? MaLopMongMuon { get; set; }  // Nullable - phụ huynh có thể không chọn lớp cụ thể
        public int MaHocKy { get; set; }
        public string LyDoYeuCau { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public string TrangThai { get; set; }    // "Chờ duyệt", "Đã duyệt", "Từ chối"
        public DateTime? NgayXuLy { get; set; }   // Nullable - chưa xử lý thì null
        public string NguoiXuLy { get; set; }     // Nullable - chưa xử lý thì null
        public string GhiChuAdmin { get; set; }   // Nullable
        public int? MaLopDuocDuyet { get; set; }  // Nullable - admin có thể duyệt lớp khác

        // Các thuộc tính mở rộng để hiển thị (không map trực tiếp từ DB)
        public string TenHocSinh { get; set; }
        public string TenLopHienTai { get; set; }
        public string TenLopMongMuon { get; set; }
        public string TenLopDuocDuyet { get; set; }
        public string TenNguoiTao { get; set; }
        public string TenNguoiXuLy { get; set; }
        public string TenHocKy { get; set; }
        public string TenNamHoc { get; set; }

        public YeuCauChuyenLopDTO()
        {
            TrangThai = "Chờ duyệt";
            NgayTao = DateTime.Now;
        }

        public YeuCauChuyenLopDTO(int maHocSinh, int maLopHienTai, int? maLopMongMuon, int maHocKy, string lyDoYeuCau, string nguoiTao)
        {
            MaHocSinh = maHocSinh;
            MaLopHienTai = maLopHienTai;
            MaLopMongMuon = maLopMongMuon;
            MaHocKy = maHocKy;
            LyDoYeuCau = lyDoYeuCau;
            NguoiTao = nguoiTao;
            TrangThai = "Chờ duyệt";
            NgayTao = DateTime.Now;
        }
    }
}

