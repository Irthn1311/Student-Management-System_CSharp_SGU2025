using System;
using System.Collections.Generic;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class ThemDiemBUS
    {
        private LopDAO lopHocDAO;
        private PhanLopDAO phanLopDAO;
        private HocKyDAO hocKyDAO;
        private MonHocDAO monHocDAO;
        private DiemSoDAO diemSoDAO;

        public ThemDiemBUS()
        {
            lopHocDAO = new LopDAO();
            phanLopDAO = new PhanLopDAO();
            hocKyDAO = new HocKyDAO();
            monHocDAO = new MonHocDAO();
            diemSoDAO = new DiemSoDAO();
        }

        /// <summary>
        /// Lấy danh sách lớp học
        /// </summary>
        public List<LopHocDTO> GetDanhSachLop()
        {
            try
            {
                return lopHocDAO.GetDanhSachLopCoHocSinh();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách lớp: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh theo lớp
        /// </summary>
        public List<HocSinhDTO> GetHocSinhTheoLop(int maLop)
        {
            try
            {
                return phanLopDAO.GetHocSinhTheoLop(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy danh sách học sinh: " + ex.Message);
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
        /// Kiểm tra điểm hợp lệ
        /// </summary>
        public bool KiemTraDiemHopLe(float? diem)
        {
            if (!diem.HasValue)
                return true;
            return diem >= 0 && diem <= 10;
        }

        /// <summary>
        /// Kiểm tra điểm đã tồn tại
        /// </summary>
        public bool KiemTraDiemDaTonTai(string maHocSinh, int maMonHoc, int maHocKy)
        {
            try
            {
                return diemSoDAO.KiemTraDiemTonTai(maHocSinh, maMonHoc, maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi kiểm tra điểm tồn tại: " + ex.Message);
            }
        }

    

        /// <summary>
        /// Lưu điểm cho học sinh (logic mới: chỉ chặn khi đã đủ 3 điểm)
        /// </summary>
        public bool LuuDiem(string maHocSinh, int maMonHoc, int maHocKy,
                           float? diemTX, float? diemGK, float? diemCK)
        {
            try
            {
                // Kiểm tra điểm đã đầy đủ chưa (có đủ cả 3 cột)
                if (diemSoDAO.KiemTraDiemDayDu(maHocSinh, maMonHoc, maHocKy))
                {
                    throw new Exception("DIEM_DA_DAY_DU");
                }

                // Tính điểm trung bình
                float? diemTB = null;
                if (diemTX.HasValue && diemGK.HasValue && diemCK.HasValue)
                {
                    diemTB = (diemTX.Value + diemGK.Value * 2 + diemCK.Value * 3) / 6;
                    diemTB = (float)Math.Round(diemTB.Value, 1);
                }

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

                return diemSoDAO.ThemDiemThongMinh(diem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sửa điểm cho học sinh (chỉ sửa điểm TX, giữ nguyên GK và CK)
        /// </summary>
        public bool SuaDiem(string maHocSinh, int maMonHoc, int maHocKy,
                           float diemTX, float? diemGK, float? diemCK)
        {
            try
            {
                // Tính lại điểm trung bình
                float? diemTB = null;
                if (diemGK.HasValue && diemCK.HasValue)
                {
                    diemTB = (diemTX + diemGK.Value * 2 + diemCK.Value * 3) / 6;
                    diemTB = (float)Math.Round(diemTB.Value, 1);
                }

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
                throw new Exception("Lỗi nghiệp vụ khi sửa điểm: " + ex.Message);
            }
        }

    }
}