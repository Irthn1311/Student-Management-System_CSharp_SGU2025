using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class MonHoc_NamHoc_KhoiBUS
    {
        private MonHoc_NamHoc_KhoiDAO monHocNamHocKhoiDAO;
        private MonHocDAO monHocDAO;
        private NamHocDAO namHocDAO;

        public MonHoc_NamHoc_KhoiBUS()
        {
            monHocNamHocKhoiDAO = new MonHoc_NamHoc_KhoiDAO();
            monHocDAO = new MonHocDAO();
            namHocDAO = new NamHocDAO();
        }

        /// <summary>
        /// Thêm môn học cho năm học và khối cụ thể với validation
        /// </summary>
        public bool ThemMonHocChoNamHocKhoi(int maMonHoc, string maNamHoc, int maKhoi)
        {
            // Validation
            if (maMonHoc <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            if (string.IsNullOrWhiteSpace(maNamHoc))
            {
                throw new ArgumentException("Mã năm học không được để trống.");
            }

            if (maKhoi <= 0)
            {
                throw new ArgumentException("Mã khối phải lớn hơn 0.");
            }

            // Kiểm tra môn học có tồn tại không
            var monHoc = monHocDAO.LayDSMonHocTheoId(maMonHoc);
            if (monHoc == null)
            {
                throw new ArgumentException("Môn học không tồn tại.");
            }

            // Kiểm tra năm học có tồn tại không
            var namHoc = namHocDAO.LayNamHocTheoMa(maNamHoc);
            if (namHoc == null)
            {
                throw new ArgumentException("Năm học không tồn tại.");
            }

            // Kiểm tra khối có hợp lệ không (10, 11, 12)
            if (maKhoi != 10 && maKhoi != 11 && maKhoi != 12)
            {
                throw new ArgumentException("Khối lớp không hợp lệ. Chỉ chấp nhận khối 10, 11, hoặc 12.");
            }

            // Kiểm tra đã tồn tại chưa
            if (monHocNamHocKhoiDAO.KiemTraMonHocTonTaiTrongNamHocKhoi(maMonHoc, maNamHoc, maKhoi))
            {
                throw new ArgumentException("Môn học đã được gán cho khối này trong năm học này.");
            }

            return monHocNamHocKhoiDAO.ThemMonHocChoNamHocKhoi(maMonHoc, maNamHoc, maKhoi);
        }

        /// <summary>
        /// Xóa môn học khỏi năm học và khối
        /// </summary>
        public bool XoaMonHocKhoiNamHoc(int maMonHoc, string maNamHoc, int maKhoi)
        {
            if (maMonHoc <= 0 || string.IsNullOrWhiteSpace(maNamHoc) || maKhoi <= 0)
            {
                throw new ArgumentException("Thông tin không hợp lệ.");
            }

            return monHocNamHocKhoiDAO.XoaMonHocKhoiNamHoc(maMonHoc, maNamHoc, maKhoi);
        }

        /// <summary>
        /// Lấy danh sách môn học theo năm học và khối
        /// </summary>
        public List<MonHocDTO> LayDanhSachMonHocTheoNamHocKhoi(string maNamHoc, int maKhoi)
        {
            if (string.IsNullOrWhiteSpace(maNamHoc) || maKhoi <= 0)
            {
                return new List<MonHocDTO>();
            }

            return monHocNamHocKhoiDAO.LayDanhSachMonHocTheoNamHocKhoi(maNamHoc, maKhoi);
        }

        /// <summary>
        /// Lấy danh sách môn học theo năm học (tất cả khối)
        /// </summary>
        public List<MonHocDTO> LayDanhSachMonHocTheoNamHoc(string maNamHoc)
        {
            if (string.IsNullOrWhiteSpace(maNamHoc))
            {
                return new List<MonHocDTO>();
            }

            return monHocNamHocKhoiDAO.LayDanhSachMonHocTheoNamHoc(maNamHoc);
        }

        /// <summary>
        /// Thêm môn học cho tất cả khối trong một năm học
        /// </summary>
        public bool ThemMonHocChoTatCaKhoi(int maMonHoc, string maNamHoc)
        {
            if (maMonHoc <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            if (string.IsNullOrWhiteSpace(maNamHoc))
            {
                throw new ArgumentException("Mã năm học không được để trống.");
            }

            // Kiểm tra môn học có tồn tại không
            var monHoc = monHocDAO.LayDSMonHocTheoId(maMonHoc);
            if (monHoc == null)
            {
                throw new ArgumentException("Môn học không tồn tại.");
            }

            // Kiểm tra năm học có tồn tại không
            var namHoc = namHocDAO.LayNamHocTheoMa(maNamHoc);
            if (namHoc == null)
            {
                throw new ArgumentException("Năm học không tồn tại.");
            }

            return monHocNamHocKhoiDAO.ThemMonHocChoTatCaKhoi(maMonHoc, maNamHoc);
        }

        /// <summary>
        /// Kiểm tra môn học có tồn tại trong năm học và khối không
        /// </summary>
        public bool KiemTraMonHocTonTaiTrongNamHocKhoi(int maMonHoc, string maNamHoc, int maKhoi)
        {
            if (maMonHoc <= 0 || string.IsNullOrWhiteSpace(maNamHoc) || maKhoi <= 0)
            {
                return false;
            }

            return monHocNamHocKhoiDAO.KiemTraMonHocTonTaiTrongNamHocKhoi(maMonHoc, maNamHoc, maKhoi);
        }

        /// <summary>
        /// Lấy danh sách năm học mà môn học đang được sử dụng
        /// </summary>
        public List<string> LayDanhSachNamHocTheoMonHoc(int maMonHoc)
        {
            if (maMonHoc <= 0)
            {
                return new List<string>();
            }

            return monHocNamHocKhoiDAO.LayDanhSachNamHocTheoMonHoc(maMonHoc);
        }

        /// <summary>
        /// Copy tất cả môn học từ năm học nguồn sang năm học đích
        /// </summary>
        public bool CopyMonHocTuNamHocNaySangNamHocKhac(string maNamHocNguon, string maNamHocDich)
        {
            if (string.IsNullOrWhiteSpace(maNamHocNguon) || string.IsNullOrWhiteSpace(maNamHocDich))
            {
                throw new ArgumentException("Mã năm học không được để trống.");
            }

            if (maNamHocNguon == maNamHocDich)
            {
                throw new ArgumentException("Năm học nguồn và năm học đích không được giống nhau.");
            }

            // Kiểm tra năm học có tồn tại không
            var namHocNguon = namHocDAO.LayNamHocTheoMa(maNamHocNguon);
            if (namHocNguon == null)
            {
                throw new ArgumentException("Năm học nguồn không tồn tại.");
            }

            var namHocDich = namHocDAO.LayNamHocTheoMa(maNamHocDich);
            if (namHocDich == null)
            {
                throw new ArgumentException("Năm học đích không tồn tại.");
            }

            return monHocNamHocKhoiDAO.CopyMonHocTuNamHocNaySangNamHocKhac(maNamHocNguon, maNamHocDich);
        }
    }
}

