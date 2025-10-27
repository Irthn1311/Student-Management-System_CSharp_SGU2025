using Student_Management_System_CSharp_SGU2025.DAO; // Namespace chứa các DAO
using Student_Management_System_CSharp_SGU2025.DTO; // Namespace chứa các DTO
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class HocSinhPhuHuynhBLL
    {
        private HocSinhPhuHuynhDAO hocSinhPhuHuynhDAO;
        private HocSinhDAO hocSinhDAO;     // Cần để kiểm tra HS tồn tại
        private PhuHuynhDAO phuHuynhDAO; // Cần để kiểm tra PH tồn tại

        public HocSinhPhuHuynhBLL()
        {
            hocSinhPhuHuynhDAO = new HocSinhPhuHuynhDAO();
            hocSinhDAO = new HocSinhDAO();
            phuHuynhDAO = new PhuHuynhDAO();
        }

        /// <summary>
        /// Thêm mối quan hệ HS-PH sau khi kiểm tra.
        /// </summary>
        /// <returns>True nếu thành công.</returns>
        public bool AddQuanHe(int maHocSinh, int maPhuHuynh, string moiQuanHe)
        {
            // --- Validation ---
            if (string.IsNullOrWhiteSpace(moiQuanHe))
            {
                throw new ArgumentException("Mối quan hệ không được để trống.");
            }
            // Kiểm tra xem HS và PH có tồn tại không
            if (!hocSinhDAO.KiemTraTonTai(maHocSinh))
            {
                throw new ArgumentException($"Học sinh với mã {maHocSinh} không tồn tại.");
            }
            if (!phuHuynhDAO.KiemTraTonTai(maPhuHuynh))
            {
                throw new ArgumentException($"Phụ huynh với mã {maPhuHuynh} không tồn tại.");
            }

            // --- Gọi DAO ---
            try
            {
                return hocSinhPhuHuynhDAO.ThemQuanHe(maHocSinh, maPhuHuynh, moiQuanHe);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL AddQuanHe: " + ex.Message);
                return false; // Hoặc ném lại lỗi
            }
        }

        /// <summary>
        /// Xóa mối quan hệ HS-PH.
        /// </summary>
        /// <returns>True nếu thành công.</returns>
        public bool DeleteQuanHe(int maHocSinh, int maPhuHuynh)
        {
            // Không cần kiểm tra tồn tại HS/PH vì nếu QH tồn tại thì HS/PH phải tồn tại
            // Có thể kiểm tra quan hệ tồn tại trước khi xóa nếu muốn thông báo rõ hơn
            // if (!hocSinhPhuHuynhDAO.KiemTraQuanHeTonTai(maHocSinh, maPhuHuynh)) { ... }

            try
            {
                return hocSinhPhuHuynhDAO.XoaQuanHe(maHocSinh, maPhuHuynh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL DeleteQuanHe: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cập nhật mối quan hệ HS-PH.
        /// </summary>
        /// <returns>True nếu thành công.</returns>
        public bool UpdateQuanHe(int maHocSinh, int maPhuHuynh, string moiQuanHeMoi)
        {
            // --- Validation ---
            if (string.IsNullOrWhiteSpace(moiQuanHeMoi))
            {
                throw new ArgumentException("Mối quan hệ không được để trống.");
            }
            // Kiểm tra xem mối quan hệ có tồn tại để cập nhật không
            if (!hocSinhPhuHuynhDAO.KiemTraQuanHeTonTai(maHocSinh, maPhuHuynh))
            {
                throw new ArgumentException($"Không tìm thấy mối quan hệ giữa HS {maHocSinh} và PH {maPhuHuynh} để cập nhật.");
            }

            // --- Gọi DAO ---
            try
            {
                return hocSinhPhuHuynhDAO.CapNhatQuanHe(maHocSinh, maPhuHuynh, moiQuanHeMoi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL UpdateQuanHe: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách Phụ huynh và mối quan hệ của một Học sinh.
        /// </summary>
        public List<(PhuHuynhDTO phuHuynh, string moiQuanHe)> GetPhuHuynhByHocSinh(int maHocSinh)
        {
            // Kiểm tra HS tồn tại
            if (!hocSinhDAO.KiemTraTonTai(maHocSinh))
            {
                throw new ArgumentException($"Học sinh với mã {maHocSinh} không tồn tại.");
            }
            try
            {
                return hocSinhPhuHuynhDAO.LayPhuHuynhCuaHocSinh(maHocSinh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetPhuHuynhByHocSinh: " + ex.Message);
                throw; // Hoặc trả về list rỗng
            }
        }

        /// <summary>
        /// Lấy danh sách Học sinh và mối quan hệ của một Phụ huynh.
        /// </summary>
        public List<(HocSinhDTO hocSinh, string moiQuanHe)> GetHocSinhByPhuHuynh(int maPhuHuynh)
        {
            // Kiểm tra PH tồn tại
            if (!phuHuynhDAO.KiemTraTonTai(maPhuHuynh))
            {
                throw new ArgumentException($"Phụ huynh với mã {maPhuHuynh} không tồn tại.");
            }
            try
            {
                return hocSinhPhuHuynhDAO.LayHocSinhCuaPhuHuynh(maPhuHuynh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetHocSinhByPhuHuynh: " + ex.Message);
                throw; // Hoặc trả về list rỗng
            }
        }

        /// <summary>
        /// Kiểm tra xem mối quan hệ đã tồn tại chưa.
        /// </summary>
        public bool CheckQuanHeExists(int maHocSinh, int maPhuHuynh)
        {
            try
            {
                return hocSinhPhuHuynhDAO.KiemTraQuanHeTonTai(maHocSinh, maPhuHuynh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL CheckQuanHeExists: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Lấy tất cả mối quan hệ HS-PH.
        /// </summary>
        public List<(int maHocSinh, int maPhuHuynh, string moiQuanHe)> GetAllQuanHe()
        {
            try
            {
                return hocSinhPhuHuynhDAO.LayTatCaQuanHe();
            }
            catch (Exception ex) { 
                Console.WriteLine("Lỗi BLL GetAllQuanHe: " + ex.Message);
                throw; 
            }
        }

        /// <summary>
        /// Xóa tất cả mối quan hệ liên quan đến một Học sinh.
        /// </summary>
        public bool DeleteQuanHeByHocSinh(int maHocSinh)
        {
            try
            {
                // Không cần kiểm tra HS tồn tại vì nếu không tồn tại sẽ không có QH nào để xóa
                return hocSinhPhuHuynhDAO.XoaQuanHeTheoMaHocSinh(maHocSinh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL DeleteQuanHeByHocSinh: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Xóa tất cả mối quan hệ liên quan đến một Phụ huynh.
        /// </summary>
        public bool DeleteQuanHeByPhuHuynh(int maPhuHuynh)
        {
            try
            {
                return hocSinhPhuHuynhDAO.XoaQuanHeTheoMaPhuHuynh(maPhuHuynh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL DeleteQuanHeByPhuHuynh: " + ex.Message);
                return false;
            }
        }
    }
}