using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS.Services
{
    /// <summary>
    /// Service để lọc môn học theo năm học và khối lớp
    /// </summary>
    public class MonHocFilterService
    {
        private MonHoc_NamHoc_KhoiDAO monHocNamHocKhoiDAO;
        private LopDAO lopDAO;
        private HocKyDAO hocKyDAO;

        public MonHocFilterService()
        {
            monHocNamHocKhoiDAO = new MonHoc_NamHoc_KhoiDAO();
            lopDAO = new LopDAO();
            hocKyDAO = new HocKyDAO();
        }

        /// <summary>
        /// Lấy danh sách môn học cho một lớp cụ thể và năm học
        /// </summary>
        public List<MonHocDTO> GetSubjectsForClassAndYear(int maLop, string maNamHoc)
        {
            if (maLop <= 0 || string.IsNullOrWhiteSpace(maNamHoc))
            {
                return new List<MonHocDTO>();
            }

            // Lấy khối của lớp
            var lop = lopDAO.LayLopTheoId(maLop);
            if (lop == null)
            {
                return new List<MonHocDTO>();
            }

            int maKhoi = lop.maKhoi;

            // Lấy danh sách môn học theo năm học và khối
            return monHocNamHocKhoiDAO.LayDanhSachMonHocTheoNamHocKhoi(maNamHoc, maKhoi);
        }

        /// <summary>
        /// Lấy danh sách môn học cho một khối và năm học
        /// </summary>
        public List<MonHocDTO> GetSubjectsForGradeAndYear(int maKhoi, string maNamHoc)
        {
            if (maKhoi <= 0 || string.IsNullOrWhiteSpace(maNamHoc))
            {
                return new List<MonHocDTO>();
            }

            return monHocNamHocKhoiDAO.LayDanhSachMonHocTheoNamHocKhoi(maNamHoc, maKhoi);
        }

        /// <summary>
        /// Kiểm tra môn học có hợp lệ cho lớp và năm học không
        /// </summary>
        public bool IsSubjectValidForClass(int maMonHoc, int maLop, string maNamHoc)
        {
            if (maMonHoc <= 0 || maLop <= 0 || string.IsNullOrWhiteSpace(maNamHoc))
            {
                return false;
            }

            // Lấy khối của lớp
            var lop = lopDAO.LayLopTheoId(maLop);
            if (lop == null)
            {
                return false;
            }

            int maKhoi = lop.maKhoi;

            // Kiểm tra môn học có trong năm học và khối không
            return monHocNamHocKhoiDAO.KiemTraMonHocTonTaiTrongNamHocKhoi(maMonHoc, maNamHoc, maKhoi);
        }

        /// <summary>
        /// Lấy danh sách môn học cho một học kỳ (dựa trên năm học của học kỳ và khối của lớp)
        /// </summary>
        public List<MonHocDTO> GetSubjectsForSemesterAndClass(int maHocKy, int maLop)
        {
            if (maHocKy <= 0 || maLop <= 0)
            {
                return new List<MonHocDTO>();
            }

            // Lấy học kỳ để lấy năm học
            var hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
            if (hocKy == null || string.IsNullOrWhiteSpace(hocKy.MaNamHoc))
            {
                return new List<MonHocDTO>();
            }

            string maNamHoc = hocKy.MaNamHoc;

            // Lấy danh sách môn học cho lớp và năm học
            return GetSubjectsForClassAndYear(maLop, maNamHoc);
        }

        /// <summary>
        /// Lấy danh sách môn học cho một học sinh trong một học kỳ
        /// (dựa trên lớp của học sinh trong học kỳ đó và năm học của học kỳ)
        /// </summary>
        public List<MonHocDTO> GetSubjectsForStudentAndSemester(int maHocSinh, int maHocKy)
        {
            if (maHocSinh <= 0 || maHocKy <= 0)
            {
                return new List<MonHocDTO>();
            }

            // Lấy phân lớp để lấy lớp của học sinh trong học kỳ
            var phanLopDAO = new PhanLopDAO();
            var phanLopList = phanLopDAO.LayDanhSachHocSinhTrongLop(0, maHocKy); // This won't work, need different approach
            
            // Query directly for PhanLop
            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                string query = @"SELECT pl.MaLop 
                                FROM PhanLop pl 
                                WHERE pl.MaHocSinh = @MaHocSinh AND pl.MaHocKy = @MaHocKy 
                                LIMIT 1";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    
                    object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        return new List<MonHocDTO>();
                    }
                    
                    int maLop = Convert.ToInt32(result);
                    return GetSubjectsForSemesterAndClass(maHocKy, maLop);
                }
            }
        }
    }
}

