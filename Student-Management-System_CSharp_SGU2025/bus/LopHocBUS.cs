
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Student_Management_System_CSharp_SGU2025.DAO;
namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class LopHocBUS
    {
        private LopHocDAO lopDAO;

        public LopHocBUS()
        {
            lopDAO = new LopHocDAO();
        }

        // Thêm lớp học với validation
        public bool ThemLop(LopDTO lop)
        {
            // Validation dữ liệu
            if (string.IsNullOrWhiteSpace(lop.tenLop))
            {
                throw new ArgumentException("Tên lớp không được để trống.");
            }

            if (lop.maKhoi <= 0)
            {
                throw new ArgumentException("Mã khối phải lớn hơn 0.");
            }

            if (string.IsNullOrWhiteSpace(lop.maGVCN))
            {
                throw new ArgumentException("Mã giáo viên chủ nhiệm không được để trống.");
            }

            // Kiểm tra xem lớp với tên này đã tồn tại chưa
            if (lopDAO.LayLopTheoTen(lop.tenLop) != null)
            {
                throw new ArgumentException("Lớp với tên này đã tồn tại.");
            }

            // Kiểm tra xem GVCN có bị trùng với lớp khác không (mỗi GV chỉ làm GVCN cho 1 lớp)
            if (KiemTraGVCNDaDuocPhanCong(lop.maGVCN))
            {
                throw new ArgumentException("Giáo viên này đã được phân công làm GVCN cho một lớp khác.");
            }

            return lopDAO.ThemLop(lop);
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

        // Cập nhật lớp học với validation
        public bool CapNhatLop(LopDTO lop)
        {
            // Validation dữ liệu
            if (lop.maLop <= 0)
            {
                throw new ArgumentException("Mã lớp phải lớn hơn 0.");
            }

            if (string.IsNullOrWhiteSpace(lop.tenLop))
            {
                throw new ArgumentException("Tên lớp không được để trống.");
            }

            if (lop.maKhoi <= 0)
            {
                throw new ArgumentException("Mã khối phải lớn hơn 0.");
            }

            if (string.IsNullOrWhiteSpace(lop.maGVCN))
            {
                throw new ArgumentException("Mã giáo viên chủ nhiệm không được để trống.");
            }

            // Kiểm tra xem tên lớp mới có bị trùng với lớp khác không (trừ lớp hiện tại)
            LopDTO lopHienTai = lopDAO.LayLopTheoId(lop.maLop);
            if (lopHienTai != null && lopHienTai.tenLop != lop.tenLop && lopDAO.LayLopTheoTen(lop.tenLop) != null)
            {
                throw new ArgumentException("Tên lớp này đã tồn tại cho một lớp khác.");
            }

            // Kiểm tra GVCN mới có bị trùng không (nếu thay đổi GVCN)
            if (lopHienTai.maGVCN != lop.maGVCN && KiemTraGVCNDaDuocPhanCong(lop.maGVCN))
            {
                throw new ArgumentException("Giáo viên này đã được phân công làm GVCN cho một lớp khác.");
            }

            // Lưu ý: Validation về phân công môn học (không trùng môn cho cùng GV cùng lớp) cần được xử lý ở BUS riêng cho phân công,
            // vì lớp Lop không trực tiếp quản lý phân công môn. Giả sử có BUS PhânCôngBUS để kiểm tra điều này nếu cần.

            return lopDAO.CapNhatLop(lop);
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
            List<LopDTO> dsLop = lopDAO.DocDSLop();
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
    }
}
