using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class YeuCauChuyenLopBLL
    {
        private YeuCauChuyenLopDAO yeuCauDAO;
        private PhanLopDAO phanLopDAO;
        private LopDAO lopDAO;
        private HocSinhDAO hocSinhDAO;

        public YeuCauChuyenLopBLL()
        {
            yeuCauDAO = new YeuCauChuyenLopDAO();
            phanLopDAO = new PhanLopDAO();
            lopDAO = new LopDAO();
            hocSinhDAO = new HocSinhDAO();
        }

        /// <summary>
        /// Phụ huynh gửi yêu cầu chuyển lớp
        /// </summary>
        public bool GuiYeuCau(YeuCauChuyenLopDTO yeuCau)
        {
            // Validation
            if (yeuCau.MaHocSinh <= 0)
            {
                throw new ArgumentException("Mã học sinh không hợp lệ.");
            }

            if (yeuCau.MaLopHienTai <= 0)
            {
                throw new ArgumentException("Mã lớp hiện tại không hợp lệ.");
            }

            if (yeuCau.MaHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ không hợp lệ.");
            }

            if (string.IsNullOrWhiteSpace(yeuCau.LyDoYeuCau))
            {
                throw new ArgumentException("Vui lòng nhập lý do chuyển lớp.");
            }

            if (string.IsNullOrWhiteSpace(yeuCau.NguoiTao))
            {
                throw new ArgumentException("Người tạo yêu cầu không hợp lệ.");
            }

            // Kiểm tra học sinh tồn tại
            if (!hocSinhDAO.KiemTraTonTai(yeuCau.MaHocSinh))
            {
                throw new ArgumentException("Học sinh không tồn tại.");
            }

            // Kiểm tra lớp hiện tại tồn tại
            LopDTO lopHienTai = lopDAO.LayLopTheoId(yeuCau.MaLopHienTai);
            if (lopHienTai == null)
            {
                throw new ArgumentException("Lớp hiện tại không tồn tại.");
            }

            // Kiểm tra lớp mong muốn (nếu có)
            if (yeuCau.MaLopMongMuon.HasValue && yeuCau.MaLopMongMuon.Value > 0)
            {
                LopDTO lopMongMuon = lopDAO.LayLopTheoId(yeuCau.MaLopMongMuon.Value);
                if (lopMongMuon == null)
                {
                    throw new ArgumentException("Lớp mong muốn không tồn tại.");
                }

                // Kiểm tra cùng khối
                if (lopMongMuon.maKhoi != lopHienTai.maKhoi)
                {
                    throw new ArgumentException("Chỉ được chuyển sang lớp cùng khối.");
                }

                // Kiểm tra lớp mong muốn có đầy không
                int siSoHienTai = phanLopDAO.DemSoLuongHocSinhTrongLop(yeuCau.MaLopMongMuon.Value, yeuCau.MaHocKy);
                int siSoToiDa = lopMongMuon.siSo > 0 ? lopMongMuon.siSo : siSoHienTai;
                
                if (siSoHienTai >= siSoToiDa)
                {
                    throw new ArgumentException($"Lớp {lopMongMuon.tenLop} đã đầy sĩ số ({siSoHienTai}/{siSoToiDa}).");
                }
            }

            // Kiểm tra học sinh đã có yêu cầu "Chờ duyệt" nào chưa
            List<YeuCauChuyenLopDTO> dsYeuCauCu = yeuCauDAO.LayYeuCauTheoHocSinh(yeuCau.MaHocSinh);
            foreach (var yc in dsYeuCauCu)
            {
                if (yc.TrangThai == "Chờ duyệt" && yc.MaHocKy == yeuCau.MaHocKy)
                {
                    throw new ArgumentException("Học sinh đã có yêu cầu chuyển lớp đang chờ duyệt trong học kỳ này.");
                }
            }

            // Thêm yêu cầu
            return yeuCauDAO.ThemYeuCau(yeuCau);
        }

        /// <summary>
        /// Admin duyệt yêu cầu chuyển lớp và thực hiện chuyển lớp
        /// </summary>
        public bool DuyetYeuCau(int maYeuCau, int maLopDuocDuyet, string nguoiXuLy, string ghiChuAdmin)
        {
            // Lấy thông tin yêu cầu
            YeuCauChuyenLopDTO yeuCau = yeuCauDAO.LayYeuCauTheoMa(maYeuCau);
            if (yeuCau == null)
            {
                throw new ArgumentException("Yêu cầu không tồn tại.");
            }

            if (yeuCau.TrangThai != "Chờ duyệt")
            {
                throw new ArgumentException("Yêu cầu đã được xử lý trước đó.");
            }

            // Kiểm tra lớp được duyệt
            LopDTO lopDuocDuyet = lopDAO.LayLopTheoId(maLopDuocDuyet);
            if (lopDuocDuyet == null)
            {
                throw new ArgumentException("Lớp được duyệt không tồn tại.");
            }

            // Kiểm tra cùng khối
            LopDTO lopHienTai = lopDAO.LayLopTheoId(yeuCau.MaLopHienTai);
            if (lopDuocDuyet.maKhoi != lopHienTai.maKhoi)
            {
                throw new ArgumentException("Chỉ được duyệt chuyển sang lớp cùng khối.");
            }

            // Kiểm tra sĩ số lớp được duyệt
            int siSoHienTai = phanLopDAO.DemSoLuongHocSinhTrongLop(maLopDuocDuyet, yeuCau.MaHocKy);
            int siSoToiDa = lopDuocDuyet.siSo > 0 ? lopDuocDuyet.siSo : siSoHienTai;
            
            if (siSoHienTai >= siSoToiDa)
            {
                throw new ArgumentException($"Lớp {lopDuocDuyet.tenLop} đã đầy sĩ số ({siSoHienTai}/{siSoToiDa}).");
            }

            // Thực hiện chuyển lớp
            try
            {
                // 1. Xóa học sinh khỏi lớp cũ trong học kỳ này
                bool xoaThanhCong = phanLopDAO.XoaPhanLop(yeuCau.MaHocSinh, yeuCau.MaLopHienTai, yeuCau.MaHocKy);
                if (!xoaThanhCong)
                {
                    throw new Exception("Không thể xóa học sinh khỏi lớp cũ.");
                }

                // 2. Thêm học sinh vào lớp mới trong học kỳ này
                bool themThanhCong = phanLopDAO.ThemPhanLop(yeuCau.MaHocSinh, maLopDuocDuyet, yeuCau.MaHocKy);
                if (!themThanhCong)
                {
                    // Rollback: thêm lại vào lớp cũ
                    phanLopDAO.ThemPhanLop(yeuCau.MaHocSinh, yeuCau.MaLopHienTai, yeuCau.MaHocKy);
                    throw new Exception("Không thể thêm học sinh vào lớp mới.");
                }

                // 3. Cập nhật trạng thái yêu cầu
                bool capNhatThanhCong = yeuCauDAO.DuyetYeuCau(maYeuCau, maLopDuocDuyet, nguoiXuLy, ghiChuAdmin);
                if (!capNhatThanhCong)
                {
                    // Rollback: hoàn tác chuyển lớp
                    phanLopDAO.XoaPhanLop(yeuCau.MaHocSinh, maLopDuocDuyet, yeuCau.MaHocKy);
                    phanLopDAO.ThemPhanLop(yeuCau.MaHocSinh, yeuCau.MaLopHienTai, yeuCau.MaHocKy);
                    throw new Exception("Không thể cập nhật trạng thái yêu cầu.");
                }

                // 4. Lưu log vào bảng LichSuChuyenLop (nếu có)
                try
                {
                    LichSuChuyenLopDAO lichSuDAO = new LichSuChuyenLopDAO();
                    lichSuDAO.ThemLichSu(new LichSuChuyenLopDTO
                    {
                        MaHocSinh = yeuCau.MaHocSinh,
                        MaLopCu = yeuCau.MaLopHienTai,
                        MaLopMoi = maLopDuocDuyet,
                        MaHocKy = yeuCau.MaHocKy,
                        NgayChuyen = DateTime.Now,
                        LyDo = yeuCau.LyDoYeuCau,
                        NguoiThucHien = nguoiXuLy
                    });
                }
                catch (Exception ex)
                {
                    // Log lỗi nhưng không rollback vì chuyển lớp đã thành công
                    Console.WriteLine("Lỗi khi lưu lịch sử chuyển lớp: " + ex.Message);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi duyệt yêu cầu: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Admin từ chối yêu cầu chuyển lớp
        /// </summary>
        public bool TuChoiYeuCau(int maYeuCau, string nguoiXuLy, string ghiChuAdmin)
        {
            // Lấy thông tin yêu cầu
            YeuCauChuyenLopDTO yeuCau = yeuCauDAO.LayYeuCauTheoMa(maYeuCau);
            if (yeuCau == null)
            {
                throw new ArgumentException("Yêu cầu không tồn tại.");
            }

            if (yeuCau.TrangThai != "Chờ duyệt")
            {
                throw new ArgumentException("Yêu cầu đã được xử lý trước đó.");
            }

            if (string.IsNullOrWhiteSpace(ghiChuAdmin))
            {
                throw new ArgumentException("Vui lòng nhập lý do từ chối.");
            }

            return yeuCauDAO.TuChoiYeuCau(maYeuCau, nguoiXuLy, ghiChuAdmin);
        }

        /// <summary>
        /// Lấy tất cả yêu cầu
        /// </summary>
        public List<YeuCauChuyenLopDTO> LayTatCaYeuCau()
        {
            return yeuCauDAO.LayTatCaYeuCau();
        }

        /// <summary>
        /// Lấy yêu cầu theo trạng thái
        /// </summary>
        public List<YeuCauChuyenLopDTO> LayYeuCauTheoTrangThai(string trangThai)
        {
            return yeuCauDAO.LayYeuCauTheoTrangThai(trangThai);
        }

        /// <summary>
        /// Lấy yêu cầu theo học sinh
        /// </summary>
        public List<YeuCauChuyenLopDTO> LayYeuCauTheoHocSinh(int maHocSinh)
        {
            return yeuCauDAO.LayYeuCauTheoHocSinh(maHocSinh);
        }

        /// <summary>
        /// Xóa yêu cầu (chỉ cho phép xóa yêu cầu "Chờ duyệt")
        /// </summary>
        public bool XoaYeuCau(int maYeuCau)
        {
            YeuCauChuyenLopDTO yeuCau = yeuCauDAO.LayYeuCauTheoMa(maYeuCau);
            if (yeuCau == null)
            {
                throw new ArgumentException("Yêu cầu không tồn tại.");
            }

            if (yeuCau.TrangThai != "Chờ duyệt")
            {
                throw new ArgumentException("Chỉ được xóa yêu cầu đang chờ duyệt.");
            }

            return yeuCauDAO.XoaYeuCau(maYeuCau);
        }
    }

    // DTO cho lịch sử chuyển lớp (để tích hợp với bảng LichSuChuyenLop)
    internal class LichSuChuyenLopDTO
    {
        public int MaLichSu { get; set; }
        public int MaHocSinh { get; set; }
        public int MaLopCu { get; set; }
        public int MaLopMoi { get; set; }
        public int MaHocKy { get; set; }
        public DateTime NgayChuyen { get; set; }
        public string LyDo { get; set; }
        public string NguoiThucHien { get; set; }
    }

    // DAO cho lịch sử chuyển lớp (giả sử chưa có)
    internal class LichSuChuyenLopDAO
    {
        public bool ThemLichSu(LichSuChuyenLopDTO lichSu)
        {
            string sql = @"INSERT INTO LichSuChuyenLop 
                (MaHocSinh, MaLopCu, MaLopMoi, MaHocKy, NgayChuyen, LyDo, NguoiThucHien) 
                VALUES (@maHocSinh, @maLopCu, @maLopMoi, @maHocKy, @ngayChuyen, @lyDo, @nguoiThucHien)";

            using (var conn = Student_Management_System_CSharp_SGU2025.ConnectDatabase.ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHocSinh", lichSu.MaHocSinh);
                        cmd.Parameters.AddWithValue("@maLopCu", lichSu.MaLopCu);
                        cmd.Parameters.AddWithValue("@maLopMoi", lichSu.MaLopMoi);
                        cmd.Parameters.AddWithValue("@maHocKy", lichSu.MaHocKy);
                        cmd.Parameters.AddWithValue("@ngayChuyen", lichSu.NgayChuyen);
                        cmd.Parameters.AddWithValue("@lyDo", lichSu.LyDo ?? "");
                        cmd.Parameters.AddWithValue("@nguoiThucHien", lichSu.NguoiThucHien ?? "");

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                finally
                {
                    Student_Management_System_CSharp_SGU2025.ConnectDatabase.ConnectionDatabase.CloseConnection(conn);
                }
            }
        }
    }
}

