using System;
using System.Collections.Generic;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class NhapDiemBUS
    {
        private NhapDiemDAO nhapDiemDAO;
        private DiemSoDAO diemSoDAO;
        private LopDAO lopDAO;
        private MonHocDAO monHocDAO;
        private HocKyDAO hocKyDAO;

        public NhapDiemBUS()
        {
            nhapDiemDAO = new NhapDiemDAO();
            diemSoDAO = new DiemSoDAO();
            lopDAO = new LopDAO();
            monHocDAO = new MonHocDAO();
            hocKyDAO = new HocKyDAO();
        }


        /// <summary>
        /// Lấy danh sách lớp học
        /// </summary>
        public List<LopDTO> GetDanhSachLop()
        {
            try
            {
                return lopDAO.GetDanhSachLopCoHocSinh();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách lớp: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách môn học
        /// </summary>
        public List<MonHocDTO> GetDanhSachMonHoc()
        {
            try
            {
                return monHocDAO.GetAllMonHoc();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách môn học: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách học kỳ
        /// </summary>
        public List<HocKyDTO> GetDanhSachHocKy()
        {
            try
            {
                return hocKyDAO.GetAllHocKy();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách học kỳ: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách nhập điểm theo lớp
        /// </summary>
        public List<NhapDiemDTO> GetDanhSachNhapDiemTheoLop(int maLop, int maMonHoc, int maHocKy)
        {
            try
            {
                return nhapDiemDAO.GetDanhSachNhapDiemTheoLop(maLop, maMonHoc, maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách nhập điểm theo lớp: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh kèm điểm để hiển thị
        /// </summary>
        public List<NhapDiemDTO> GetDanhSachNhapDiem(int maMonHoc, int maHocKy)
        {
            try
            {
                return nhapDiemDAO.GetDanhSachNhapDiem(maMonHoc, maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách nhập điểm: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy tất cả học sinh (không cần điểm)
        /// </summary>
        public List<NhapDiemDTO> GetAllHocSinhForNhapDiem()
        {
            try
            {
                return nhapDiemDAO.GetAllHocSinhForNhapDiem();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách học sinh: " + ex.Message);
            }
        }

        /// <summary>
        /// Tính điểm trung bình theo công thức: (DiemTX + DiemGK*2 + DiemCK*3) / 6
        /// </summary>
        public float? TinhDiemTrungBinh(float? diemTX, float? diemGK, float? diemCK)
        {
            if (!diemTX.HasValue || !diemGK.HasValue || !diemCK.HasValue)
            {
                return null;
            }

            float diemTB = (diemTX.Value + diemGK.Value * 2 + diemCK.Value * 3) / 6;
            return (float)Math.Round(diemTB, 1);
        }

        /// <summary>
        /// Lưu điểm cho một học sinh
        /// </summary>
        public bool LuuDiem(string maHocSinh, int maMonHoc, int maHocKy,
                           float? diemTX, float? diemGK, float? diemCK)
        {
            try
            {
                // Tính điểm trung bình
                float? diemTB = TinhDiemTrungBinh(diemTX, diemGK, diemCK);

                // Tạo đối tượng DiemSoDTO
                DiemSoDTO diem = new DiemSoDTO
                {
                    MaHocSinh = maHocSinh,
                    MaMonHoc = maMonHoc,
                    MaHocKy = maHocKy,
                    DiemThuongXuyen = diemTX,
                    DiemGiuaKy = diemGK,
                    DiemCuoiKy = diemCK,
                    DiemTrungBinh = diemTB
                };

                return diemSoDAO.UpsertDiemSo(diem);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lưu điểm: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của điểm
        /// </summary>
        public bool KiemTraDiemHopLe(float? diem)
        {
            if (!diem.HasValue)
                return true; // Null là hợp lệ

            return diem >= 0 && diem <= 10;
        }

        /// <summary>
        /// Kiểm tra tất cả điểm hợp lệ
        /// </summary>
        public bool KiemTraTatCaDiemHopLe(float? diemTX, float? diemGK, float? diemCK)
        {
            return KiemTraDiemHopLe(diemTX) &&
                   KiemTraDiemHopLe(diemGK) &&
                   KiemTraDiemHopLe(diemCK);
        }

        /// <summary>
        /// Lấy mã lớp của học sinh từ bảng PhanLop
        /// </summary>
        public int? GetMaLopByMaHocSinh(string maHocSinh)
        {
            try
            {
                return nhapDiemDAO.GetMaLopByMaHocSinh(maHocSinh);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy mã lớp: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy tên lớp theo mã lớp
        /// </summary>
        public string GetTenLopByMaLop(int maLop)
        {
            try
            {
                return lopDAO.GetTenLopByMaLop(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy tên lớp: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy bảng điểm theo học kỳ
        /// </summary>
        public List<XemBangDiemDTO> GetBangDiemTheoHocKy(int maHocKy)
        {
            try
            {
                return nhapDiemDAO.GetBangDiemTheoHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy bảng điểm: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy chi tiết điểm đầy đủ của một học sinh
        /// </summary>
        public ChiTietDiemDTO GetChiTietDiem(string maHocSinh, int maHocKy)
        {
            try
            {
                return nhapDiemDAO.GetChiTietDiem(maHocSinh, maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy chi tiết điểm: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy thống kê điểm theo học kỳ
        /// </summary>
        public ThongKeDTO GetThongKeDiemTheoHocKy(int maHocKy)
        {
            try
            {
                return nhapDiemDAO.GetThongKeDiemTheoHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy thống kê điểm: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách lớp có điểm số
        /// </summary>
        public List<LopDTO> GetDanhSachLopCoDiem(int maHocKy)
        {
            try
            {
                return nhapDiemDAO.GetDanhSachLopCoDiem(maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách lớp có điểm: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy bảng điểm theo học kỳ và lớp
        /// </summary>
        public List<XemBangDiemDTO> GetBangDiemTheoHocKyVaLop(int maHocKy, int? maLop = null)
        {
            try
            {
                return nhapDiemDAO.GetBangDiemTheoHocKyVaLop(maHocKy, maLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy bảng điểm: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách lớp theo học kỳ
        /// </summary>
        public List<LopDTO> GetDanhSachLopTheoHocKy(int maHocKy)
        {
            try
            {
                return lopDAO.GetDanhSachLopTheoHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách lớp theo học kỳ: " + ex.Message);
            }
        }

    }
}