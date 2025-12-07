
using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO cho bảng NguoiNhanThongBao - Theo dõi người nhận và trạng thái đọc
    /// </summary>
    public class NguoiNhanThongBaoDTO
    {
        #region Properties

        /// <summary>
        /// Mã thông báo
        /// </summary>
        public int MaThongBao { get; set; }

        /// <summary>
        /// Tên đăng nhập của người nhận
        /// </summary>
        public string TenDangNhap { get; set; }

        /// <summary>
        /// Đã đọc hay chưa
        /// </summary>
        public bool DaDoc { get; set; }

        /// <summary>
        /// Thời gian đọc
        /// </summary>
        public DateTime? NgayDoc { get; set; }

        /// <summary>
        /// Người nhận đã xóa khỏi hộp thư chưa
        /// </summary>
        public bool DaXoa { get; set; }

        /// <summary>
        /// Thời gian nhận thông báo
        /// </summary>
        public DateTime NgayNhan { get; set; }

        #endregion

        #region Extended Properties (dùng để hiển thị)

        /// <summary>
        /// Họ tên người nhận (JOIN)
        /// </summary>
        public string HoTenNguoiNhan { get; set; }

        /// <summary>
        /// Vai trò của người nhận
        /// </summary>
        public string VaiTro { get; set; }

        /// <summary>
        /// Tiêu đề thông báo (JOIN)
        /// </summary>
        public string TieuDeThongBao { get; set; }

        /// <summary>
        /// Loại thông báo (JOIN)
        /// </summary>
        public string LoaiThongBao { get; set; }

        #endregion

        #region Constructors

        public NguoiNhanThongBaoDTO()
        {
            DaDoc = false;
            DaXoa = false;
            NgayNhan = DateTime.Now;
        }

        public NguoiNhanThongBaoDTO(int maThongBao, string tenDangNhap)
        {
            MaThongBao = maThongBao;
            TenDangNhap = tenDangNhap;
            DaDoc = false;
            DaXoa = false;
            NgayNhan = DateTime.Now;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Lấy trạng thái hiển thị
        /// </summary>
        public string GetTrangThaiText()
        {
            if (DaXoa) return "Đã xóa";
            if (DaDoc) return "Đã đọc";
            return "Chưa đọc";
        }

        /// <summary>
        /// Đánh dấu đã đọc
        /// </summary>
        public void MarkAsRead()
        {
            if (!DaDoc)
            {
                DaDoc = true;
                NgayDoc = DateTime.Now;
            }
        }

        #endregion
    }

    /// <summary>
    /// DTO thống kê thông báo cho Dashboard
    /// </summary>
    public class ThongKeThongBaoDTO
    {
        /// <summary>
        /// Tổng số thông báo
        /// </summary>
        public int TongThongBao { get; set; }

        /// <summary>
        /// Số thông báo chưa đọc
        /// </summary>
        public int ChuaDoc { get; set; }

        /// <summary>
        /// Số thông báo đã đọc
        /// </summary>
        public int DaDoc { get; set; }

        /// <summary>
        /// Số thông báo gửi cho giáo viên
        /// </summary>
        public int GuiGiaoVien { get; set; }

        /// <summary>
        /// Số thông báo gửi cho học sinh
        /// </summary>
        public int GuiHocSinh { get; set; }

        /// <summary>
        /// Số thông báo gửi toàn trường
        /// </summary>
        public int GuiToanTruong { get; set; }

        /// <summary>
        /// Số thông báo khẩn cấp
        /// </summary>
        public int KhanCap { get; set; }

        /// <summary>
        /// Số thông báo quan trọng
        /// </summary>
        public int QuanTrong { get; set; }

        public ThongKeThongBaoDTO()
        {
            TongThongBao = 0;
            ChuaDoc = 0;
            DaDoc = 0;
            GuiGiaoVien = 0;
            GuiHocSinh = 0;
            GuiToanTruong = 0;
            KhanCap = 0;
            QuanTrong = 0;
        }
    }
}
