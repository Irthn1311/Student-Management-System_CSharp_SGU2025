
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Student_Management_System_CSharp_SGU2025.DAO;
namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class LopHocBUS
    {
        private LopDAO lopDAO;

        public LopHocBUS()
        {
            lopDAO = new LopDAO();
        }

        // Thêm lớp học với validation (ném lỗi)
        public bool ThemLop(LopDTO lop)
        {
            string _; string __;
            return ThemLop(lop, out _, out __);
        }

        // Thêm lớp học với validation (trả về thông báo)
        public bool ThemLop(LopDTO lop, out string message, out string errorField)
        {
            message = string.Empty;
            errorField = string.Empty;

            if (lop == null)
            {
                message = "Dữ liệu lớp không hợp lệ.";
                errorField = "lop";
                return false;
            }

            if (string.IsNullOrWhiteSpace(lop.tenLop))
            {
                message = "Tên lớp không được để trống.";
                errorField = "tenLop";
                return false;
            }
            if (lop.maKhoi <= 0)
            {
                message = "Mã khối phải lớn hơn 0.";
                errorField = "maKhoi";
                return false;
            }
            if (lop.siSo < 0)
            {
                message = "Sĩ số không được nhỏ hơn 0.";
                errorField = "siSo";
                return false;
            }
            if (string.IsNullOrWhiteSpace(lop.maGVCN))
            {
                message = "Mã giáo viên chủ nhiệm không được để trống.";
                errorField = "maGVCN";
                return false;
            }

            if (lopDAO.LayLopTheoTen(lop.tenLop) != null)
            {
                message = "Lớp với tên này đã tồn tại.";
                errorField = "tenLop";
                return false;
            }

            if (KiemTraGVCNDaDuocPhanCong(lop.maGVCN))
            {
                message = "Giáo viên này đã được phân công làm GVCN cho một lớp khác.";
                errorField = "maGVCN";
                return false;
            }

            bool ok = lopDAO.ThemLop(lop);
            if (!ok)
            {
                message = "Thêm lớp học thất bại.";
                errorField = string.Empty;
            }
            return ok;
        }

        // Đọc danh sách lớp học
        public List<LopDTO> DocDSLop()
        {
            return lopDAO.DocDSLop();
        }

        // Lấy lớp theo ID
        public LopDTO LayLopTheoId(int maLop)
        {
            if (maLop <= 0)
            {
                throw new ArgumentException("Mã lớp phải lớn hơn 0.");
            }

            return lopDAO.LayLopTheoId(maLop);
        }

        // Lấy lớp theo tên
        public LopDTO LayLopTheoTen(string tenLop)
        {
            if (string.IsNullOrWhiteSpace(tenLop))
            {
                throw new ArgumentException("Tên lớp không được để trống.");
            }

            return lopDAO.LayLopTheoTen(tenLop);
        }

        // Cập nhật lớp học với validation (ném lỗi)
        public bool CapNhatLop(LopDTO lop)
        {
            string _; string __;
            return CapNhatLop(lop, out _, out __);
        }

        // Cập nhật lớp học với validation (trả về thông báo)
        public bool CapNhatLop(LopDTO lop, out string message, out string errorField)
        {
            message = string.Empty;
            errorField = string.Empty;

            if (lop == null)
            {
                message = "Dữ liệu lớp không hợp lệ.";
                errorField = "lop";
                return false;
            }
            if (lop.maLop <= 0)
            {
                message = "Mã lớp phải lớn hơn 0.";
                errorField = "maLop";
                return false;
            }
            if (string.IsNullOrWhiteSpace(lop.tenLop))
            {
                message = "Tên lớp không được để trống.";
                errorField = "tenLop";
                return false;
            }
            if (lop.maKhoi <= 0)
            {
                message = "Mã khối phải lớn hơn 0.";
                errorField = "maKhoi";
                return false;
            }
            if (lop.siSo < 0)
            {
                message = "Sĩ số không được nhỏ hơn 0.";
                errorField = "siSo";
                return false;
            }
            if (string.IsNullOrWhiteSpace(lop.maGVCN))
            {
                message = "Mã giáo viên chủ nhiệm không được để trống.";
                errorField = "maGVCN";
                return false;
            }

            LopDTO lopHienTai = lopDAO.LayLopTheoId(lop.maLop);
            if (lopHienTai == null)
            {
                message = "Không tìm thấy lớp để cập nhật.";
                errorField = "maLop";
                return false;
            }
            if (lopHienTai.tenLop != lop.tenLop && lopDAO.LayLopTheoTen(lop.tenLop) != null)
            {
                message = "Tên lớp này đã tồn tại cho một lớp khác.";
                errorField = "tenLop";
                return false;
            }
            if (lopHienTai.maGVCN != lop.maGVCN && KiemTraGVCNDaDuocPhanCong(lop.maGVCN))
            {
                message = "Giáo viên này đã được phân công làm GVCN cho một lớp khác.";
                errorField = "maGVCN";
                return false;
            }

            bool ok = lopDAO.CapNhatLop(lop);
            if (!ok)
            {
                message = "Cập nhật lớp học thất bại.";
                errorField = string.Empty;
            }
            return ok;
        }
        // ✅ THÊM METHOD MỚI vào class LopHocBUS
        public int LayMaLopTiepTheo()
        {
            return lopDAO.LayMaLopTiepTheo();
        }

        // Xóa lớp học với validation
        public bool XoaLop(int maLop)
        {
            if (maLop <= 0)
            {
                throw new ArgumentException("Mã lớp phải lớn hơn 0.");
            }

            LopDTO lop = lopDAO.LayLopTheoId(maLop);
            if (lop == null)
            {
                throw new ArgumentException("Không tìm thấy lớp với mã này.");
            }

            // Có thể thêm kiểm tra xem lớp có học sinh hoặc phân công không trước khi xóa (tùy theo yêu cầu hệ thống)
            // Ví dụ: Kiểm tra số học sinh trong lớp > 0 thì không cho xóa.

            return lopDAO.XoaLop(maLop);
        }

        // Phương thức hỗ trợ: Kiểm tra GVCN đã được phân công chưa
        private bool KiemTraGVCNDaDuocPhanCong(string maGVCN)
        {
            if (string.IsNullOrWhiteSpace(maGVCN)) return false;
            List<LopDTO> dsLop = lopDAO.DocDSLop();
            if (dsLop == null) return false;
            return dsLop.Any(l => l.maGVCN == maGVCN);
        }

        // Phương thức hỗ trợ: Kiểm tra phân công môn trùng (giả sử có DAO cho phân công, nhưng vì chưa có, chỉ note)
        // public bool KiemTraPhanCongTrungMon(string maGV, int maLop, string maMon)
        // {
        //     // Gọi DAO PhânCông để kiểm tra
        //     // return false nếu không trùng
        // }

        public List<string> LayDanhSachMaGVCNDangPhanCong()
        {
            return lopDAO.LayDanhSachMaGVCNDangPhanCong();
        }

        /// <summary>
        /// Lấy tổng số lượng lớp học.
        /// </summary>
        /// <returns>Tổng số lớp.</returns>
        public int GetTotalLopHoc()
        {
            try
            {
                // Gọi hàm DocDSLop() đã có sẵn trong BUS
                List<LopDTO> dsLop = this.DocDSLop();

                // Trả về số lượng phần tử trong danh sách
                if (dsLop != null)
                {
                    return dsLop.Count;
                }
                else
                {
                    return 0; // Trả về 0 nếu danh sách là null
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi (nếu có hệ thống logging)
                Console.WriteLine("Lỗi khi lấy tổng số lớp học: " + ex.Message);
                return 0; // Trả về 0 nếu có lỗi
            }
        }

        /// <summary>
        /// Lấy danh sách lớp theo năm học (thông qua HocKy)
        /// </summary>
        public List<LopDTO> DocDSLopTheoNamHoc(string maNamHoc)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maNamHoc))
                {
                    // Nếu không chọn năm học, trả về tất cả lớp
                    return DocDSLop();
                }
                return lopDAO.DocDSLopTheoNamHoc(maNamHoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS DocDSLopTheoNamHoc: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Lấy danh sách lớp kèm năm học (cho hiển thị "Tất cả năm học")
        /// Trả về Dictionary với MaLop làm key
        /// </summary>
        public Dictionary<int, string> DocDSLopVoiNamHoc()
        {
            try
            {
                return lopDAO.DocDSLopVoiNamHoc();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS DocDSLopVoiNamHoc: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Sao chép danh sách lớp từ năm học này sang năm học khác
        /// </summary>
        public bool SaoChepLopTuNamHoc(string maNamHocNguon, string maNamHocDich)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maNamHocNguon))
                {
                    throw new ArgumentException("Mã năm học nguồn không được để trống.");
                }
                if (string.IsNullOrWhiteSpace(maNamHocDich))
                {
                    throw new ArgumentException("Mã năm học đích không được để trống.");
                }
                if (maNamHocNguon == maNamHocDich)
                {
                    throw new ArgumentException("Năm học nguồn và năm học đích không được giống nhau.");
                }
                return lopDAO.SaoChepLopTuNamHoc(maNamHocNguon, maNamHocDich);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS SaoChepLopTuNamHoc: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Lấy danh sách lớp đã được phân lớp trong học kỳ cụ thể
        /// </summary>
        public List<LopDTO> GetDanhSachLopTheoHocKy(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                {
                    throw new ArgumentException("Mã học kỳ không hợp lệ.");
                }

                return lopDAO.GetDanhSachLopTheoHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS GetDanhSachLopTheoHocKy: {ex.Message}");
                throw;
            }
        }

    }
}
