using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class PhanLopBLL
    {
        private PhanLopDAO phanLopDAO;
        private HocSinhDAO hocSinhDAO;
        private LopDAO lopHocDAO;
        private HocKyDAO hocKyDAO;

        public PhanLopBLL()
        {
            phanLopDAO = new PhanLopDAO();
            hocSinhDAO = new HocSinhDAO();
            lopHocDAO = new LopDAO();
            hocKyDAO = new HocKyDAO();
        }

        /// <summary>
        /// Thêm học sinh vào lớp trong học kỳ cụ thể sau khi kiểm tra.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu thành công.</returns>
        public bool AddPhanLop(int maHocSinh, int maLop, int maHocKy)
        {
            // --- Validation ---
            if (maHocSinh <= 0)
            {
                throw new ArgumentException("Mã học sinh không hợp lệ.");
            }
            if (maLop <= 0)
            {
                throw new ArgumentException("Mã lớp không hợp lệ.");
            }
            if (maHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ không hợp lệ.");
            }

            // Kiểm tra học sinh tồn tại
            if (!hocSinhDAO.KiemTraTonTai(maHocSinh))
            {
                throw new ArgumentException($"Học sinh với mã {maHocSinh} không tồn tại.");
            }

            // Kiểm tra lớp tồn tại
            LopDTO lop = lopHocDAO.LayLopTheoId(maLop);
            if (lop == null)
            {
                throw new ArgumentException($"Lớp với mã {maLop} không tồn tại.");
            }

            // Kiểm tra học kỳ tồn tại
            HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
            if (hocKy == null)
            {
                throw new ArgumentException($"Học kỳ với mã {maHocKy} không tồn tại.");
            }

            // Kiểm tra học sinh đã được phân lớp trong học kỳ này chưa
            if (phanLopDAO.KiemTraHocSinhDaPhanLop(maHocSinh, maHocKy))
            {
                throw new ArgumentException($"Học sinh {maHocSinh} đã được phân lớp trong học kỳ {maHocKy}.");
            }

            // --- Gọi DAO ---
            try
            {
                return phanLopDAO.ThemPhanLop(maHocSinh, maLop, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL AddPhanLop: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Xóa phân lớp của học sinh khỏi lớp trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu thành công.</returns>
        public bool DeletePhanLop(int maHocSinh, int maLop, int maHocKy)
        {
            // --- Validation ---
            if (maHocSinh <= 0 || maLop <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Các tham số không hợp lệ.");
            }

            // Kiểm tra phân lớp tồn tại
            if (!phanLopDAO.KiemTraPhanLopTonTai(maHocSinh, maLop, maHocKy))
            {
                throw new ArgumentException($"Không tìm thấy phân lớp của học sinh {maHocSinh} trong lớp {maLop}, học kỳ {maHocKy}.");
            }

            try
            {
                return phanLopDAO.XoaPhanLop(maHocSinh, maLop, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL DeletePhanLop: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Chuyển học sinh từ lớp này sang lớp khác trong cùng học kỳ và lưu lịch sử.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maLopCu">Mã lớp cũ.</param>
        /// <param name="maLopMoi">Mã lớp mới.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <param name="lyDo">Lý do chuyển lớp (tùy chọn).</param>
        /// <param name="nguoiThucHien">Người thực hiện (tùy chọn).</param>
        /// <returns>True nếu thành công.</returns>
        public bool ChuyenLop(int maHocSinh, int maLopCu, int maLopMoi, int maHocKy, string lyDo = null, string nguoiThucHien = null)
        {
            // --- Validation ---
            if (maHocSinh <= 0 || maLopCu <= 0 || maLopMoi <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Các tham số không hợp lệ.");
            }

            if (maLopCu == maLopMoi)
            {
                throw new ArgumentException("Lớp cũ và lớp mới không được giống nhau.");
            }

            // Kiểm tra học sinh tồn tại
            if (!hocSinhDAO.KiemTraTonTai(maHocSinh))
            {
                throw new ArgumentException($"Học sinh với mã {maHocSinh} không tồn tại.");
            }

            // Kiểm tra lớp cũ tồn tại
            LopDTO lopCu = lopHocDAO.LayLopTheoId(maLopCu);
            if (lopCu == null)
            {
                throw new ArgumentException($"Lớp cũ với mã {maLopCu} không tồn tại.");
            }

            // Kiểm tra lớp mới tồn tại
            LopDTO lopMoi = lopHocDAO.LayLopTheoId(maLopMoi);
            if (lopMoi == null)
            {
                throw new ArgumentException($"Lớp mới với mã {maLopMoi} không tồn tại.");
            }

            // Kiểm tra học kỳ tồn tại
            HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
            if (hocKy == null)
            {
                throw new ArgumentException($"Học kỳ với mã {maHocKy} không tồn tại.");
            }

            // Kiểm tra học sinh có trong lớp cũ không
            if (!phanLopDAO.KiemTraPhanLopTonTai(maHocSinh, maLopCu, maHocKy))
            {
                throw new ArgumentException($"Học sinh {maHocSinh} không có trong lớp {maLopCu}, học kỳ {maHocKy}.");
            }

            // Kiểm tra lớp mới có đầy không (tùy chọn - có thể bỏ qua)
            int soLuongHocSinhTrongLopMoi = phanLopDAO.DemSoLuongHocSinhTrongLop(maLopMoi, maHocKy);
            if (soLuongHocSinhTrongLopMoi >= 40) // Giả sử mỗi lớp tối đa 40 học sinh
            {
                throw new ArgumentException($"Lớp {maLopMoi} đã đầy ({soLuongHocSinhTrongLopMoi} học sinh).");
            }

            try
            {
                return phanLopDAO.ChuyenLop(maHocSinh, maLopCu, maLopMoi, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL ChuyenLop: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh trong lớp cụ thể của học kỳ.
        /// </summary>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Danh sách học sinh.</returns>
        public List<HocSinhDTO> GetHocSinhByLop(int maLop, int maHocKy)
        {
            // --- Validation ---
            if (maLop <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Mã lớp và mã học kỳ không hợp lệ.");
            }

            // Kiểm tra lớp tồn tại
            LopDTO lop = lopHocDAO.LayLopTheoId(maLop);
            if (lop == null)
            {
                throw new ArgumentException($"Lớp với mã {maLop} không tồn tại.");
            }

            // Kiểm tra học kỳ tồn tại
            HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
            if (hocKy == null)
            {
                throw new ArgumentException($"Học kỳ với mã {maHocKy} không tồn tại.");
            }

            try
            {
                return phanLopDAO.LayDanhSachHocSinhTrongLop(maLop, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetHocSinhByLop: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Lấy lớp hiện tại của học sinh trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Mã lớp nếu tìm thấy, -1 nếu không tìm thấy.</returns>
        public int GetLopByHocSinh(int maHocSinh, int maHocKy)
        {
            // --- Validation ---
            if (maHocSinh <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Mã học sinh và mã học kỳ không hợp lệ.");
            }

            // Kiểm tra học sinh tồn tại
            if (!hocSinhDAO.KiemTraTonTai(maHocSinh))
            {
                throw new ArgumentException($"Học sinh với mã {maHocSinh} không tồn tại.");
            }

            // Kiểm tra học kỳ tồn tại
            HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
            if (hocKy == null)
            {
                throw new ArgumentException($"Học kỳ với mã {maHocKy} không tồn tại.");
            }

            try
            {
                return phanLopDAO.LayLopCuaHocSinh(maHocSinh, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetLopByHocSinh: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Đếm số lượng học sinh trong lớp cụ thể của học kỳ.
        /// </summary>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Số lượng học sinh.</returns>
        public int CountHocSinhInLop(int maLop, int maHocKy)
        {
            // --- Validation ---
            if (maLop <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Mã lớp và mã học kỳ không hợp lệ.");
            }

            try
            {
                return phanLopDAO.DemSoLuongHocSinhTrongLop(maLop, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL CountHocSinhInLop: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Kiểm tra học sinh đã được phân lớp trong học kỳ cụ thể chưa.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu đã được phân lớp.</returns>
        public bool CheckHocSinhDaPhanLop(int maHocSinh, int maHocKy)
        {
            // --- Validation ---
            if (maHocSinh <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Mã học sinh và mã học kỳ không hợp lệ.");
            }

            try
            {
                return phanLopDAO.KiemTraHocSinhDaPhanLop(maHocSinh, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL CheckHocSinhDaPhanLop: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra phân lớp cụ thể có tồn tại không.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu tồn tại.</returns>
        public bool CheckPhanLopExists(int maHocSinh, int maLop, int maHocKy)
        {
            // --- Validation ---
            if (maHocSinh <= 0 || maLop <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Các tham số không hợp lệ.");
            }

            try
            {
                return phanLopDAO.KiemTraPhanLopTonTai(maHocSinh, maLop, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL CheckPhanLopExists: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Xóa tất cả phân lớp của học sinh trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu thành công.</returns>
        public bool DeletePhanLopByHocSinh(int maHocSinh, int maHocKy)
        {
            // --- Validation ---
            if (maHocSinh <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Mã học sinh và mã học kỳ không hợp lệ.");
            }

            try
            {
                return phanLopDAO.XoaPhanLopTheoHocSinh(maHocSinh, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL DeletePhanLopByHocSinh: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Lấy tất cả phân lớp trong hệ thống.
        /// </summary>
        /// <returns>Danh sách phân lớp.</returns>
        public List<(int maHocSinh, int maLop, int maHocKy)> GetAllPhanLop()
        {
            try
            {
                return phanLopDAO.LayTatCaPhanLop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetAllPhanLop: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh chưa được phân lớp trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Danh sách học sinh chưa phân lớp.</returns>
        public List<HocSinhDTO> GetHocSinhChuaPhanLop(int maHocKy)
        {
            // --- Validation ---
            if (maHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ không hợp lệ.");
            }

            // Kiểm tra học kỳ tồn tại
            HocKyDTO hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
            if (hocKy == null)
            {
                throw new ArgumentException($"Học kỳ với mã {maHocKy} không tồn tại.");
            }

            try
            {
                return phanLopDAO.LayHocSinhChuaPhanLop(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetHocSinhChuaPhanLop: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Phân lớp tự động cho học sinh chưa được phân lớp.
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <param name="soLuongHocSinhToiDaMoiLop">Số lượng học sinh tối đa mỗi lớp (mặc định 40).</param>
        /// <returns>Số lượng học sinh đã được phân lớp.</returns>
        public int PhanLopTuDong(int maHocKy, int soLuongHocSinhToiDaMoiLop = 40)
        {
            // --- Validation ---
            if (maHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ không hợp lệ.");
            }
            if (soLuongHocSinhToiDaMoiLop <= 0)
            {
                throw new ArgumentException("Số lượng học sinh tối đa mỗi lớp phải lớn hơn 0.");
            }

            try
            {
                // Lấy danh sách học sinh chưa phân lớp
                List<HocSinhDTO> dsHocSinhChuaPhanLop = GetHocSinhChuaPhanLop(maHocKy);
                
                // Lấy danh sách lớp học
                List<LopDTO> dsLop = lopHocDAO.DocDSLop();
                
                int soLuongDaPhanLop = 0;
                int lopHienTai = 0;

                foreach (HocSinhDTO hs in dsHocSinhChuaPhanLop)
                {
                    // Tìm lớp có chỗ trống
                    bool daPhanLop = false;
                    int soLopDaKiemTra = 0;

                    while (!daPhanLop && soLopDaKiemTra < dsLop.Count)
                    {
                        LopDTO lop = dsLop[lopHienTai];
                        int soLuongHienTai = CountHocSinhInLop(lop.maLop, maHocKy);

                        if (soLuongHienTai < soLuongHocSinhToiDaMoiLop)
                        {
                            // Phân lớp cho học sinh này
                            if (AddPhanLop(hs.MaHS, lop.maLop, maHocKy))
                            {
                                soLuongDaPhanLop++;
                                daPhanLop = true;
                            }
                        }

                        lopHienTai = (lopHienTai + 1) % dsLop.Count;
                        soLopDaKiemTra++;
                    }

                    if (!daPhanLop)
                    {
                        Console.WriteLine($"Không thể phân lớp cho học sinh {hs.MaHS} - {hs.HoTen}. Tất cả lớp đã đầy.");
                    }
                }

                return soLuongDaPhanLop;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL PhanLopTuDong: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Lấy thống kê phân lớp theo học kỳ.
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Dictionary với key là mã lớp, value là số lượng học sinh.</returns>
        public Dictionary<int, int> GetThongKePhanLop(int maHocKy)
        {
            // --- Validation ---
            if (maHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ không hợp lệ.");
            }

            try
            {
                Dictionary<int, int> thongKe = new Dictionary<int, int>();
                List<LopDTO> dsLop = lopHocDAO.DocDSLop();

                foreach (LopDTO lop in dsLop)
                {
                    int soLuong = CountHocSinhInLop(lop.maLop, maHocKy);
                    thongKe.Add(lop.maLop, soLuong);
                }

                return thongKe;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL GetThongKePhanLop: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh theo học kỳ (có phân lớp hoặc chưa)
        /// </summary>
        public List<HocSinhDTO> LayHocSinhTheoHocKy(int maHocKy)
        {
            try
            {
                // Lấy tất cả học sinh đã phân lớp trong học kỳ
                List<(int maHocSinh, int maLop, int maHocKy)> phanLopList = phanLopDAO.LayTatCaPhanLop();
                
                // Lọc theo học kỳ
                var hocSinhIds = phanLopList
                    .Where(pl => pl.maHocKy == maHocKy)
                    .Select(pl => pl.maHocSinh)
                    .Distinct()
                    .ToList();

                // Lấy thông tin chi tiết học sinh
                List<HocSinhDTO> result = new List<HocSinhDTO>();
                foreach (int maHocSinh in hocSinhIds)
                {
                    HocSinhDTO hs = hocSinhDAO.TimHocSinhTheoMa(maHocSinh);
                    if (hs != null)
                    {
                        result.Add(hs);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL LayHocSinhTheoHocKy: " + ex.Message);
                return new List<HocSinhDTO>();
            }
        }

        /// <summary>
        /// Đếm số lượng học sinh đã được phân lớp trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Số lượng học sinh.</returns>
        public int CountHocSinhInHocKy(int maHocKy)
        {
            if (maHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ không hợp lệ.");
            }

            try
            {
                List<(int maHocSinh, int maLop, int maHocKy)> phanLopList = phanLopDAO.LayTatCaPhanLop();
                return phanLopList.Count(pl => pl.maHocKy == maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL CountHocSinhInHocKy: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Xóa tất cả phân lớp trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu thành công.</returns>
        public bool DeleteAllPhanLopByHocKy(int maHocKy)
        {
            if (maHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ không hợp lệ.");
            }

            try
            {
                return phanLopDAO.XoaTatCaPhanLopTheoHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL DeleteAllPhanLopByHocKy: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh trong lớp theo học kỳ cụ thể.
        /// </summary>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Danh sách học sinh trong lớp.</returns>
        public List<HocSinhDTO> LayDanhSachHocSinhTheoLopVaHocKy(int maLop, int maHocKy)
        {
            if (maLop <= 0 || maHocKy <= 0)
            {
                throw new ArgumentException("Mã lớp và mã học kỳ không hợp lệ.");
            }

            try
            {
                return phanLopDAO.LayDanhSachHocSinhTrongLop(maLop, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi BLL LayDanhSachHocSinhTheoLopVaHocKy: " + ex.Message);
                throw;
            }
        }
    }
}