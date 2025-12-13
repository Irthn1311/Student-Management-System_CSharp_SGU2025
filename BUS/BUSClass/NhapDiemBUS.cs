using System;
using System.Collections.Generic;
using System.Linq;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Services;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class NhapDiemBUS
    {
        private NhapDiemDAO nhapDiemDAO;
        private DiemSoDAO diemSoDAO;
        private LopDAO lopDAO;
        private MonHocDAO monHocDAO;
        private HocKyDAO hocKyDAO;
        private MonHoc_NamHoc_KhoiBUS monHocNamHocKhoiBUS;
        private MonHocFilterService monHocFilterService;
        private PhanLopDAO phanLopDAO;

        public NhapDiemBUS()
        {
            nhapDiemDAO = new NhapDiemDAO();
            diemSoDAO = new DiemSoDAO();
            lopDAO = new LopDAO();
            monHocDAO = new MonHocDAO();
            hocKyDAO = new HocKyDAO();
            monHocNamHocKhoiBUS = new MonHoc_NamHoc_KhoiBUS();
            monHocFilterService = new MonHocFilterService();
            phanLopDAO = new PhanLopDAO();
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
        /// ✅ Chỉ lấy các môn học hợp lệ từ MonHoc_NamHoc_Khoi
        /// </summary>
        public ChiTietDiemDTO GetChiTietDiem(string maHocSinh, int maHocKy)
        {
            try
            {
                var chiTietDiem = nhapDiemDAO.GetChiTietDiem(maHocSinh, maHocKy);
                
                if (chiTietDiem == null)
                {
                    return null;
                }

                // ✅ Lấy lớp của học sinh trong học kỳ này
                int maLop = phanLopDAO.LayLopCuaHocSinh(int.Parse(maHocSinh), maHocKy);
                if (maLop <= 0)
                {
                    // Nếu không có phân lớp, trả về dữ liệu gốc (có thể học sinh chưa được phân lớp)
                    return chiTietDiem;
                }

                // ✅ Lấy danh sách môn học hợp lệ cho lớp và học kỳ
                var danhSachMonHocHopLe = monHocFilterService.GetSubjectsForSemesterAndClass(maHocKy, maLop);
                if (danhSachMonHocHopLe == null || danhSachMonHocHopLe.Count == 0)
                {
                    // Nếu không có môn học hợp lệ, trả về dữ liệu gốc
                    return chiTietDiem;
                }

                // ✅ Tạo lại DiemCacMon với TẤT CẢ môn học hợp lệ (kể cả chưa có điểm)
                var diemCacMonFiltered = new Dictionary<int, DiemMonHocDTO>();
                foreach (var monHoc in danhSachMonHocHopLe)
                {
                    // Nếu môn học đã có điểm, lấy điểm đó
                    if (chiTietDiem.DiemCacMon.ContainsKey(monHoc.maMon))
                    {
                        diemCacMonFiltered[monHoc.maMon] = chiTietDiem.DiemCacMon[monHoc.maMon];
                    }
                    else
                    {
                        // Nếu môn học chưa có điểm, tạo mới với DiemTrungBinh = null
                        diemCacMonFiltered[monHoc.maMon] = new DiemMonHocDTO
                        {
                            MaMonHoc = monHoc.maMon,
                            TenMonHoc = monHoc.tenMon,
                            DiemTrungBinh = null
                        };
                    }
                }
                chiTietDiem.DiemCacMon = diemCacMonFiltered;

                // ✅ Tính lại điểm TB chỉ dựa trên các môn học hợp lệ
                if (diemCacMonFiltered.Count > 0 && danhSachMonHocHopLe != null && danhSachMonHocHopLe.Count > 0)
                {
                    var diemHopLe = diemCacMonFiltered.Values
                        .Where(d => d.DiemTrungBinh.HasValue)
                        .Select(d => d.DiemTrungBinh.Value)
                        .ToList();
                    
                    if (diemHopLe.Count == danhSachMonHocHopLe.Count)
                    {
                        // Đủ điểm tất cả môn học hợp lệ
                        chiTietDiem.DiemTB = diemHopLe.Average();
                    }
                    else
                    {
                        // Chưa đủ điểm
                        chiTietDiem.DiemTB = null;
                    }
                }
                else
                {
                    chiTietDiem.DiemTB = null;
                }

                return chiTietDiem;
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
        /// Lấy điểm trung bình các môn học của học sinh theo học kỳ
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh</param>
        /// <param name="maHocKy">Mã học kỳ</param>
        /// <returns>Dictionary với key là MaMonHoc, value là điểm trung bình môn đó</returns>
        public Dictionary<int, float?> LayDiemTrungBinhMonTheoHocKy(int maHocSinh, int maHocKy)
        {
            try
            {
                return diemSoDAO.LayDiemTrungBinhMonTheoHocKy(maHocSinh, maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy điểm TB môn học của HS {maHocSinh} trong HK {maHocKy}: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy bảng điểm theo học kỳ và lớp
        /// ✅ Tính điểm TB chỉ dựa trên các môn học hợp lệ từ MonHoc_NamHoc_Khoi
        /// </summary>
        public List<XemBangDiemDTO> GetBangDiemTheoHocKyVaLop(int maHocKy, int? maLop = null)
        {
            try
            {
                var bangDiem = nhapDiemDAO.GetBangDiemTheoHocKyVaLop(maHocKy, maLop);
                
                if (bangDiem == null || bangDiem.Count == 0)
                {
                    return bangDiem ?? new List<XemBangDiemDTO>();
                }

                // ✅ Lấy năm học từ học kỳ
                var hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
                if (hocKy == null || string.IsNullOrEmpty(hocKy.MaNamHoc))
                {
                    return bangDiem;
                }

                string maNamHoc = hocKy.MaNamHoc;

                // ✅ Với mỗi học sinh, lấy lớp và filter môn học hợp lệ
                foreach (var diem in bangDiem)
                {
                    // Lấy lớp của học sinh trong học kỳ này
                    int maLopHS = phanLopDAO.LayLopCuaHocSinh(int.Parse(diem.MaHocSinh), maHocKy);
                    if (maLopHS <= 0)
                    {
                        // Không có phân lớp, giữ nguyên điểm TB
                        continue;
                    }

                    // Lấy khối của lớp
                    var lop = lopDAO.LayLopTheoId(maLopHS);
                    if (lop == null)
                    {
                        continue;
                    }

                    // ✅ Lấy danh sách môn học hợp lệ cho năm học và khối
                    var danhSachMonHocHopLe = monHocNamHocKhoiBUS.LayDanhSachMonHocTheoNamHocKhoi(maNamHoc, lop.maKhoi);
                    var maMonHocHopLe = danhSachMonHocHopLe?.Select(m => m.maMon).ToHashSet() ?? new HashSet<int>();

                    // ✅ Filter lại DiemCacMon chỉ giữ các môn học hợp lệ
                    var diemCacMonFiltered = new Dictionary<int, float?>();
                    foreach (var kvp in diem.DiemCacMon)
                    {
                        if (maMonHocHopLe.Contains(kvp.Key))
                        {
                            diemCacMonFiltered[kvp.Key] = kvp.Value;
                        }
                    }
                    diem.DiemCacMon = diemCacMonFiltered;

                    // ✅ Tính lại điểm TB chỉ dựa trên các môn học hợp lệ
                    if (diemCacMonFiltered.Count > 0 && danhSachMonHocHopLe != null && danhSachMonHocHopLe.Count > 0)
                    {
                        var diemHopLe = diemCacMonFiltered.Values
                            .Where(d => d.HasValue)
                            .Select(d => d.Value)
                            .ToList();
                        
                        if (diemHopLe.Count == danhSachMonHocHopLe.Count)
                        {
                            // Đủ điểm tất cả môn học hợp lệ
                            diem.DiemTB = diemHopLe.Average();
                        }
                        else
                        {
                            // Chưa đủ điểm
                            diem.DiemTB = null;
                        }
                    }
                    else
                    {
                        diem.DiemTB = null;
                    }
                }

                return bangDiem;
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

        /// <summary>
        /// Lấy tất cả điểm số trong hệ thống
        /// Dùng cho logic phân lớp tự động
        /// </summary>
        public List<DiemSoDTO> GetAllDiemSo()
        {
            try
            {
                return diemSoDAO.GetAllDiemSo();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy tất cả điểm số: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy học kỳ mới nhất có dữ liệu điểm số
        /// </summary>
        public HocKyDTO GetHocKyMoiNhatCoDuLieu()
        {
            try
            {
                return hocKyDAO.LayHocKyMoiNhatCoDuLieu();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy học kỳ mới nhất: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra học kỳ có dữ liệu điểm số hay không
        /// </summary>
        public bool KiemTraHocKyCoDiemSo(int maHocKy)
        {
            try
            {
                return hocKyDAO.KiemTraHocKyCoDiemSo(maHocKy);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi kiểm tra điểm số: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy học kỳ I của năm học mới nhất (dùng khi không có học kỳ nào có dữ liệu điểm số)
        /// </summary>
        public HocKyDTO LayHocKyIDauTienCuaNamHocMoiNhat()
        {
            try
            {
                return hocKyDAO.LayHocKyIDauTienCuaNamHocMoiNhat();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy học kỳ I đầu tiên: " + ex.Message);
            }
        }

    }
}