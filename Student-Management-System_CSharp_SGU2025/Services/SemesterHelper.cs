using System;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.Services
{
    /// <summary>
    /// Helper class để xử lý logic liên quan đến học kỳ
    /// </summary>
    public static class SemesterHelper
    {
        /// <summary>
        /// Kiểm tra học kỳ có thể chỉnh sửa được không (hiện tại hoặc tương lai)
        /// </summary>
        public static bool IsEditable(int maHocKy)
        {
            var hocKyDao = new HocKyDAO();
            var hocKy = hocKyDao.LayHocKyTheoMa(maHocKy);
            
            if (hocKy == null || !hocKy.NgayKT.HasValue)
                return false;
            
            // Học kỳ đã kết thúc = READ ONLY
            // Học kỳ hiện tại hoặc tương lai = CÓ THỂ CHỈNH SỬA
            return hocKy.NgayKT.Value.Date >= DateTime.Now.Date;
        }

        /// <summary>
        /// Kiểm tra học kỳ đã kết thúc chưa (READ ONLY)
        /// </summary>
        public static bool IsPast(int maHocKy)
        {
            return !IsEditable(maHocKy);
        }

        /// <summary>
        /// Lấy trạng thái học kỳ
        /// </summary>
        public static string GetStatus(int maHocKy)
        {
            var hocKyDao = new HocKyDAO();
            var hocKy = hocKyDao.LayHocKyTheoMa(maHocKy);
            
            if (hocKy == null || !hocKy.NgayBD.HasValue || !hocKy.NgayKT.HasValue)
                return "Chưa xác định";
            
            DateTime now = DateTime.Now.Date;
            DateTime batDau = hocKy.NgayBD.Value.Date;
            DateTime ketThuc = hocKy.NgayKT.Value.Date;

            if (now >= batDau && now <= ketThuc)
                return "Đang diễn ra";
            else if (now < batDau)
                return "Chưa bắt đầu";
            else
                return "Đã kết thúc";
        }

        /// <summary>
        /// Lấy học kỳ hiện tại
        /// </summary>
        public static HocKyDTO GetCurrentSemester()
        {
            var hocKyDao = new HocKyDAO();
            var allSemesters = hocKyDao.DocDSHocKy();
            
            DateTime now = DateTime.Now.Date;
            
            foreach (var hk in allSemesters)
            {
                if (hk.NgayBD.HasValue && hk.NgayKT.HasValue)
                {
                    if (now >= hk.NgayBD.Value.Date && now <= hk.NgayKT.Value.Date)
                    {
                        return hk;
                    }
                }
            }
            
            return null;
        }

        /// <summary>
        /// Lấy danh sách học kỳ có thể chọn (hiện tại + tương lai)
        /// </summary>
        public static System.Collections.Generic.List<HocKyDTO> GetEditableSemesters()
        {
            var hocKyDao = new HocKyDAO();
            var allSemesters = hocKyDao.DocDSHocKy();
            var result = new System.Collections.Generic.List<HocKyDTO>();
            
            DateTime now = DateTime.Now.Date;
            
            foreach (var hk in allSemesters)
            {
                if (hk.NgayKT.HasValue && hk.NgayKT.Value.Date >= now)
                {
                    result.Add(hk);
                }
            }
            
            return result;
        }
    }
}