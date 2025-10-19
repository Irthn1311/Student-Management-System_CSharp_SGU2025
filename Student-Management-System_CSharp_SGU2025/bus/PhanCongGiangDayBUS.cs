using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.DAO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class PhanCongGiangDayBUS
    {
        private PhanCongGiangDayDAO phanCongDAO;
        private LopHocDAO lopDAO;
        private MonHocDAO monHocDAO;
        // Giả sử có HocKyDAO và GiaoVienDAO nếu cần validate existence

        public PhanCongGiangDayBUS()
        {
            phanCongDAO = new PhanCongGiangDayDAO();
            lopDAO = new LopHocDAO();
            monHocDAO = new MonHocDAO();
        }

        // Thêm phân công giảng dạy với validation
        public bool ThemPhanCongGiangDay(PhanCongGiangDayDTO pcgd)
        {
            // Validation dữ liệu cơ bản
            if (pcgd.MaLop <= 0)
            {
                throw new ArgumentException("Mã lớp phải lớn hơn 0.");
            }

            if (string.IsNullOrWhiteSpace(pcgd.MaGiaoVien))
            {
                throw new ArgumentException("Mã giáo viên không được để trống.");
            }

            if (pcgd.MaMonHoc <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            if (pcgd.MaHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ phải lớn hơn 0.");
            }

            if (pcgd.TuNgay >= pcgd.DenNgay)
            {
                throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
            }

            // Kiểm tra sự tồn tại của lớp, môn học (giả sử có DAO cho HocKy và GiaoVien)
            if (lopDAO.LayLopTheoId(pcgd.MaLop) == null)
            {
                throw new ArgumentException("Không tìm thấy lớp với mã này.");
            }

            if (monHocDAO.LayDSMonHocTheoId(pcgd.MaMonHoc) == null)
            {
                throw new ArgumentException("Không tìm thấy môn học với mã này.");
            }

            // Kiểm tra không phân công trùng môn cho cùng GV cùng lớp (trong cùng học kỳ)
            if (KiemTraPhanCongTrung(pcgd.MaLop, pcgd.MaGiaoVien, pcgd.MaMonHoc, pcgd.MaHocKy))
            {
                throw new ArgumentException("Không thể phân công trùng môn học cho cùng giáo viên trong cùng lớp và học kỳ.");
            }

            return phanCongDAO.ThemPhanCongGiangDay(pcgd);
        }

        // Đọc danh sách phân công giảng dạy
        public List<PhanCongGiangDayDTO> DocDSPhanCong()
        {
            return phanCongDAO.DocDSPhanCong();
        }

        // Lấy phân công theo ID
        public PhanCongGiangDayDTO LayPhanCongTheoId(int maPhanCong)
        {
            if (maPhanCong <= 0)
            {
                throw new ArgumentException("Mã phân công phải lớn hơn 0.");
            }

            return phanCongDAO.LayPhanCongTheoId(maPhanCong);
        }

        // Xóa phân công với validation
        public bool XoaPhanCong(int maPhanCong)
        {
            if (maPhanCong <= 0)
            {
                throw new ArgumentException("Mã phân công phải lớn hơn 0.");
            }

            PhanCongGiangDayDTO pc = phanCongDAO.LayPhanCongTheoId(maPhanCong);
            if (pc == null)
            {
                throw new ArgumentException("Không tìm thấy phân công với mã này.");
            }

            // Có thể thêm kiểm tra xem phân công có đang được sử dụng không trước khi xóa

            return phanCongDAO.XoaPhanCong(maPhanCong);
        }

        // Phương thức hỗ trợ: Kiểm tra phân công trùng
        private bool KiemTraPhanCongTrung(int maLop, string maGiaoVien, int maMonHoc, int maHocKy)
        {
            List<PhanCongGiangDayDTO> ds = phanCongDAO.DocDSPhanCong();
            return ds.Any(pc => pc.MaLop == maLop && pc.MaGiaoVien == maGiaoVien && pc.MaMonHoc == maMonHoc && pc.MaHocKy == maHocKy);
        }
    }
}