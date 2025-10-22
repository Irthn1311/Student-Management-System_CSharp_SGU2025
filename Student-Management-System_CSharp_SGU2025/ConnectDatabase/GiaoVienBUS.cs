using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class GiaoVienBUS
    {
        private GiaoVienDAO giaoVienDAO = new GiaoVienDAO();

        // === 1. Hiển thị: Đọc Danh Sách Giáo Viên (Đầy đủ chuyên môn) ===
        public List<GiaoVien> DocDSGiaoVien()
        {
            // Gọi hàm JOIN từ DAO để lấy danh sách đầy đủ (GV + Chuyên môn)
            return giaoVienDAO.DocDSGiaoVienDayDu();
        }

        // === 2. CRUD + Validate: Thêm Giáo Viên Kèm Chuyên Môn ===
        // danhSachMonChuyenMon: Key là MaMonHoc, Value là LaChuyenMonChinh (boolean)
        public string ThemGiaoVien(GiaoVien gv, Dictionary<int, bool> danhSachMonChuyenMon)
        {
            // Validation 1: Dữ liệu cơ bản
            if (string.IsNullOrEmpty(gv.MaGiaoVien) || string.IsNullOrEmpty(gv.HoTen))
            {
                return "Mã và Họ tên giáo viên không được để trống.";
            }
            // Validation 2: Mã GV đã tồn tại
            if (giaoVienDAO.LayGiaoVienTheoMa(gv.MaGiaoVien) != null)
            {
                return "Mã giáo viên đã tồn tại trong hệ thống.";
            }
            // Validation 3: Cần có ít nhất 1 môn chuyên môn
            if (danhSachMonChuyenMon == null || danhSachMonChuyenMon.Count(x => x.Key > 0) == 0)
            {
                return "Giáo viên phải có ít nhất một môn chuyên môn.";
            }

            if (giaoVienDAO.ThemGiaoVien(gv))
            {
                // Thêm chuyên môn
                foreach (var item in danhSachMonChuyenMon)
                {
                    if (item.Key > 0)
                    {
                        giaoVienDAO.ThemChuyenMon(gv.MaGiaoVien, item.Key, item.Value);
                    }
                }
                return "Thêm giáo viên thành công.";
            }
            return "Thêm giáo viên thất bại (Lỗi CSDL).";
        }

        // === 3. CRUD + Validate: Cập nhật Giáo Viên (Kèm Cập nhật chuyên môn) ===
        public string CapNhatGiaoVien(GiaoVien gv, Dictionary<int, bool> danhSachMonChuyenMon)
        {
            if (string.IsNullOrEmpty(gv.MaGiaoVien))
            {
                return "Mã giáo viên không hợp lệ.";
            }

            // Cập nhật thông tin cơ bản
            if (giaoVienDAO.CapNhatGiaoVien(gv))
            {
                // Logic nghiệp vụ: Cập nhật chuyên môn
                // 1. Xóa chuyên môn cũ
                giaoVienDAO.XoaTatCaChuyenMonTheoGV(gv.MaGiaoVien);

                // 2. Thêm lại chuyên môn mới
                foreach (var item in danhSachMonChuyenMon)
                {
                    if (item.Key > 0)
                    {
                        giaoVienDAO.ThemChuyenMon(gv.MaGiaoVien, item.Key, item.Value);
                    }
                }
                return "Cập nhật giáo viên thành công.";
            }
            return "Cập nhật giáo viên thất bại (Lỗi CSDL).";
        }

        // === 4. CRUD + Validate: Xóa Giáo Viên ===
        public string XoaGiaoVien(string maGiaoVien)
        {
            // Logic nghiệp vụ: Kiểm tra nếu GV đang chủ nhiệm/giảng dạy thì không cho xóa
            // (Giả định có PhanCongDAO để kiểm tra)
            // if (PhanCongDAO.KiemTraGVGiangDay(maGiaoVien)) { return "Giáo viên đang giảng dạy/chủ nhiệm."; }

            // Xóa chuyên môn liên quan trước (để tránh lỗi FK)
            giaoVienDAO.XoaTatCaChuyenMonTheoGV(maGiaoVien);

            if (giaoVienDAO.XoaGiaoVien(maGiaoVien))
            {
                return "Xóa giáo viên thành công.";
            }
            return "Xóa giáo viên thất bại.";
        }
    }
}