using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class MonHocBUS
    {
        private MonHocDAO monHocDAO;
        private MonHoc_NamHoc_KhoiBUS monHocNamHocKhoiBUS;
        private NamHocDAO namHocDAO;

        public MonHocBUS()
        {
            monHocDAO = new MonHocDAO();
            monHocNamHocKhoiBUS = new MonHoc_NamHoc_KhoiBUS();
            namHocDAO = new NamHocDAO();
        }

        // Thêm môn học với validation
        public bool ThemMonHoc(MonHocDTO monHoc)
        {
            // Validation dữ liệu
            if (string.IsNullOrWhiteSpace(monHoc.tenMon))
            {
                throw new ArgumentException("Tên môn học không được để trống.");
            }

            if (monHoc.soTiet <= 0)
            {
                throw new ArgumentException("Số tiết môn học phải lớn hơn 0.");
            }

            // Kiểm tra xem môn học với tên này đã tồn tại chưa
            if (monHocDAO.LayDSMonHocTheoTen(monHoc.tenMon) != null)
            {
                throw new ArgumentException("Môn học với tên này đã tồn tại.");
            }

            return monHocDAO.ThemMonHoc(monHoc);
        }

        // Đọc danh sách môn học
        public List<MonHocDTO> DocDSMH()
        {
            return monHocDAO.DocDSMH();
        }

        // Lấy môn học theo ID
        public MonHocDTO LayDSMonHocTheoId(int maMonHoc)
        {
            if (maMonHoc <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            return monHocDAO.LayDSMonHocTheoId(maMonHoc);
        }


        public int ThemMonHocVaLayId(MonHocDTO mh)
        {
            return monHocDAO.ThemMonHocVaLayId(mh);
        }
        // Lấy môn học theo tên
        public MonHocDTO LayDSMonHocTheoTen(string tenMonHoc)
        {
            if (string.IsNullOrWhiteSpace(tenMonHoc))
            {
                throw new ArgumentException("Tên môn học không được để trống.");
            }

            return monHocDAO.LayDSMonHocTheoTen(tenMonHoc);
        }

        // Cập nhật môn học với validation
        public bool UpdateMonHoc(MonHocDTO monHoc)
        {
            // Validation dữ liệu
            if (monHoc.maMon <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            if (string.IsNullOrWhiteSpace(monHoc.tenMon))
            {
                throw new ArgumentException("Tên môn học không được để trống.");
            }

            if (monHoc.soTiet <= 0)
            {
                throw new ArgumentException("Số tiết môn học phải lớn hơn 0.");
            }

            // Kiểm tra xem tên môn học mới có bị trùng với môn khác không (trừ môn hiện tại)
            MonHocDTO monHocHienTai = monHocDAO.LayDSMonHocTheoId(monHoc.maMon);
            if (monHocHienTai != null && monHocHienTai.tenMon != monHoc.tenMon && monHocDAO.LayDSMonHocTheoTen(monHoc.tenMon) != null)
            {
                throw new ArgumentException("Tên môn học này đã tồn tại cho một môn khác.");
            }

            return monHocDAO.UpdateMonHoc(monHoc);
        }

        // Xóa môn học với validation
        public bool DeleteMonHoc(int maMonHoc)
        {
            if (maMonHoc <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            MonHocDTO monHoc = monHocDAO.LayDSMonHocTheoId(maMonHoc);
            if (monHoc == null)
            {
                throw new ArgumentException("Không tìm thấy môn học với mã này.");
            }

            // Có thể thêm kiểm tra xem môn học có đang được phân công hoặc sử dụng ở đâu không trước khi xóa (tùy theo yêu cầu hệ thống)
            // Ví dụ: Kiểm tra trong bảng phân công môn học.

            return monHocDAO.DeleteMonHoc(maMonHoc);
        }

        /// <summary>
        /// Thêm môn học và liên kết với năm học và khối (nếu có)
        /// </summary>
        public int ThemMonHocVaLienKetNamHocKhoi(MonHocDTO monHoc, string maNamHoc, bool apDungChoTatCaKhoi)
        {
            // Validation dữ liệu
            if (string.IsNullOrWhiteSpace(monHoc.tenMon))
            {
                throw new ArgumentException("Tên môn học không được để trống.");
            }

            if (monHoc.soTiet <= 0)
            {
                throw new ArgumentException("Số tiết môn học phải lớn hơn 0.");
            }

            // Kiểm tra xem môn học với tên này đã tồn tại chưa
            if (monHocDAO.LayDSMonHocTheoTen(monHoc.tenMon) != null)
            {
                throw new ArgumentException("Môn học với tên này đã tồn tại.");
            }

            // Validate năm học nếu có
            if (!string.IsNullOrWhiteSpace(maNamHoc))
            {
                var namHoc = namHocDAO.LayNamHocTheoMa(maNamHoc);
                if (namHoc == null)
                {
                    throw new ArgumentException("Năm học không tồn tại.");
                }
                monHoc.namHocBatDau = maNamHoc;
            }

            // Thêm môn học và lấy ID
            int maMonMoi = monHocDAO.ThemMonHocVaLayId(monHoc);
            
            if (maMonMoi > 0 && !string.IsNullOrWhiteSpace(maNamHoc))
            {
                // Liên kết với năm học và khối
                if (apDungChoTatCaKhoi)
                {
                    // Thêm cho tất cả khối (10, 11, 12)
                    monHocNamHocKhoiBUS.ThemMonHocChoTatCaKhoi(maMonMoi, maNamHoc);
                }
            }

            return maMonMoi;
        }

        /// <summary>
        /// Lấy danh sách môn học theo năm học bắt đầu
        /// </summary>
        public List<MonHocDTO> LayMonHocTheoNamHocBatDau(string maNamHoc)
        {
            if (string.IsNullOrWhiteSpace(maNamHoc))
            {
                return new List<MonHocDTO>();
            }

            return monHocDAO.LayMonHocTheoNamHocBatDau(maNamHoc);
        }
    }
}