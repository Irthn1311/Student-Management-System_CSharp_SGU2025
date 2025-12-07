using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO cho bảng ThongBao - Lưu trữ thông tin thông báo
    /// </summary>
    public class ThongBaoDTO
    {
        #region Properties

        /// <summary>
        /// Mã thông báo (Auto increment)
        /// </summary>
        public int MaThongBao { get; set; }

        /// <summary>
        /// Tiêu đề thông báo
        /// </summary>
        public string TieuDe { get; set; }

        /// <summary>
        /// Nội dung chi tiết thông báo
        /// </summary>
        public string NoiDung { get; set; }

        /// <summary>
        /// Ngày tạo thông báo
        /// </summary>
        public DateTime NgayTao { get; set; }

        /// <summary>
        /// Loại thông báo: HE_THONG, HOC_TAP, HOP_PHU_HUYNH, LICH_TRINH, KHEN_THUONG, KY_LUAT, SU_KIEN, CHUNG
        /// </summary>
        public string LoaiThongBao { get; set; }

        /// <summary>
        /// Mô tả đối tượng nhận (để hiển thị)
        /// </summary>
        public string DoiTuongNhan { get; set; }

        /// <summary>
        /// Ngày hết hạn thông báo (có thể null nếu không có hạn)
        /// </summary>
        public DateTime? NgayHetHan { get; set; }

        /// <summary>
        /// Tên đăng nhập của người tạo thông báo
        /// </summary>
        public string MaNguoiTao { get; set; }

        /// <summary>
        /// Phạm vi gửi: ALL, VAI_TRO, LOP, KHOI, CA_NHAN
        /// </summary>
        public string PhamVi { get; set; }

        /// <summary>
        /// Mã vai trò nhận (nếu PhamVi = VAI_TRO)
        /// </summary>
        public string MaVaiTroNhan { get; set; }

        /// <summary>
        /// Mã lớp nhận (nếu PhamVi = LOP)
        /// </summary>
        public int? MaLop { get; set; }

        /// <summary>
        /// Mã khối nhận (nếu PhamVi = KHOI)
        /// </summary>
        public int? MaKhoi { get; set; }

        /// <summary>
        /// Độ ưu tiên: BINH_THUONG, QUAN_TRONG, KHAN_CAP
        /// </summary>
        public string DoUuTien { get; set; }

        /// <summary>
        /// Trạng thái: HIEN_THI, AN, DA_XOA
        /// </summary>
        public string TrangThai { get; set; }

        #endregion

        #region Extended Properties (dùng để hiển thị)

        /// <summary>
        /// Tên người tạo (JOIN từ bảng NguoiDung/GiaoVien)
        /// </summary>
        public string TenNguoiTao { get; set; }

        /// <summary>
        /// Tên lớp (JOIN từ bảng LopHoc)
        /// </summary>
        public string TenLop { get; set; }

        /// <summary>
        /// Tên khối (JOIN từ bảng KhoiLop)
        /// </summary>
        public string TenKhoi { get; set; }

        /// <summary>
        /// Tên loại thông báo (JOIN từ bảng LoaiThongBao)
        /// </summary>
        public string TenLoaiThongBao { get; set; }

        /// <summary>
        /// Màu sắc của loại thông báo
        /// </summary>
        public string MauSacLoai { get; set; }

        /// <summary>
        /// Số người đã đọc
        /// </summary>
        public int SoNguoiDaDoc { get; set; }

        /// <summary>
        /// Tổng số người nhận
        /// </summary>
        public int TongNguoiNhan { get; set; }

        /// <summary>
        /// Trạng thái đã đọc của người dùng hiện tại
        /// </summary>
        public bool DaDoc { get; set; }

        /// <summary>
        /// Ngày đọc của người dùng hiện tại
        /// </summary>
        public DateTime? NgayDoc { get; set; }

        #endregion

        #region Constructors

        public ThongBaoDTO()
        {
            NgayTao = DateTime.Now;
            PhamVi = "ALL";
            DoUuTien = "BINH_THUONG";
            TrangThai = "HIEN_THI";
            LoaiThongBao = "CHUNG";
        }

        public ThongBaoDTO(string tieuDe, string noiDung, string loaiThongBao, string maNguoiTao)
        {
            TieuDe = tieuDe;
            NoiDung = noiDung;
            LoaiThongBao = loaiThongBao;
            MaNguoiTao = maNguoiTao;
            NgayTao = DateTime.Now;
            PhamVi = "ALL";
            DoUuTien = "BINH_THUONG";
            TrangThai = "HIEN_THI";
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Lấy text hiển thị cho độ ưu tiên
        /// </summary>
        public string GetDoUuTienText()
        {
            switch (DoUuTien)
            {
                case "KHAN_CAP": return "Khẩn cấp";
                case "QUAN_TRONG": return "Quan trọng";
                default: return "Bình thường";
            }
        }

        /// <summary>
        /// Lấy text hiển thị cho phạm vi
        /// </summary>
        public string GetPhamViText()
        {
            switch (PhamVi)
            {
                case "ALL": return "Toàn trường";
                case "VAI_TRO": return "Theo vai trò";
                case "LOP": return "Theo lớp";
                case "KHOI": return "Theo khối";
                case "CA_NHAN": return "Cá nhân";
                default: return PhamVi;
            }
        }

        /// <summary>
        /// Kiểm tra thông báo có hết hạn chưa
        /// </summary>
        public bool IsExpired()
        {
            if (!NgayHetHan.HasValue) return false;
            return NgayHetHan.Value < DateTime.Now;
        }

        /// <summary>
        /// Lấy thời gian tương đối (vd: "2 giờ trước", "3 ngày trước")
        /// </summary>
        public string GetRelativeTime()
        {
            var diff = DateTime.Now - NgayTao;
            
            if (diff.TotalMinutes < 1) return "Vừa xong";
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} phút trước";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} giờ trước";
            if (diff.TotalDays < 7) return $"{(int)diff.TotalDays} ngày trước";
            if (diff.TotalDays < 30) return $"{(int)(diff.TotalDays / 7)} tuần trước";
            if (diff.TotalDays < 365) return $"{(int)(diff.TotalDays / 30)} tháng trước";
            return $"{(int)(diff.TotalDays / 365)} năm trước";
        }

        #endregion
    }

    /// <summary>
    /// DTO cho bảng LoaiThongBao - Danh mục loại thông báo
    /// </summary>
    public class LoaiThongBaoDTO
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public string MauSac { get; set; }
        public string Icon { get; set; }
        public int ThuTu { get; set; }

        public LoaiThongBaoDTO() { }

        public LoaiThongBaoDTO(string maLoai, string tenLoai)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
        }
    }
}
