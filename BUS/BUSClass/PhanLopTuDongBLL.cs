using Student_Management_System_CSharp_SGU2025.DAO; // Giả sử bạn có DAO ở đây
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class PhanLopTuDongBLL
    {
        private PhanLopBLL phanLopBLL;
        private PhanLopDAO phanLopDAO;
        private HocSinhBLL hocSinhBLL;
        private HocKyBUS hocKyBUS;
        private LopHocBUS lopHocBUS;

        private NhapDiemBUS diemSoBUS;
        private HanhKiemBUS hanhKiemBUS;
        private XepLoaiBUS xepLoaiBUS;

        public PhanLopTuDongBLL()
        {
            phanLopBLL = new PhanLopBLL();
            phanLopDAO = new PhanLopDAO();
            hocSinhBLL = new HocSinhBLL();
            hocKyBUS = new HocKyBUS();
            lopHocBUS = new LopHocBUS();
            // Khởi tạo các BLL mới
            diemSoBUS = new NhapDiemBUS();
            hanhKiemBUS = new HanhKiemBUS();
            xepLoaiBUS = new XepLoaiBUS();
        }

        #region Phân lớp tự động (Core Logic)

        // Trong file PhanLopTuDongBLL.cs
        // HÃY THAY THẾ TOÀN BỘ HÀM NÀY

        // Trong file PhanLopTuDongBLL.cs
        // HÃY THAY THẾ TOÀN BỘ HÀM NÀY

        public (bool success, string message, int soHocSinhDaPhanLop) ThucHienPhanLopTuDong(int maHocKyCanPhanLop, bool boQuaKiemTra = false)
        {
            try
            {
                // 1. LẤY THÔNG TIN HỌC KỲ CẦN PHÂN LỚP
                HocKyDTO hocKyCanPhanLop = hocKyBUS.LayHocKyTheoMa(maHocKyCanPhanLop);
                if (hocKyCanPhanLop == null) return (false, "Không tìm thấy học kỳ cần phân lớp", 0);

                Console.WriteLine($"=== BẮT ĐẦU PHÂN LỚP CHO NĂM HỌC {hocKyCanPhanLop.MaNamHoc} ===");
                Console.WriteLine($"📌 Học kỳ được chọn: {hocKyCanPhanLop.TenHocKy}");

                // ✅ XÁC ĐỊNH NĂM HỌC CẦN PHÂN LỚP
                string maNamHocCanPhanLop = hocKyCanPhanLop.MaNamHoc;
                
                // Lấy cả HK1 và HK2 của năm học này
                var dsHocKyNamHoc = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocCanPhanLop);
                HocKyDTO hocKy1 = dsHocKyNamHoc?.FirstOrDefault(hk =>
                    (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                    (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));
                HocKyDTO hocKy2 = dsHocKyNamHoc?.FirstOrDefault(hk =>
                    hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));

                if (hocKy1 == null || hocKy2 == null)
                {
                    return (false, $"Năm học {maNamHocCanPhanLop} phải có đầy đủ HK1 và HK2!", 0);
                }

                Console.WriteLine($"   → HK1: {hocKy1.TenHocKy} (MaHocKy: {hocKy1.MaHocKy})");
                Console.WriteLine($"   → HK2: {hocKy2.TenHocKy} (MaHocKy: {hocKy2.MaHocKy})");

                // 1.5. KIỂM TRA NĂM HỌC ĐÃ PHÂN LỚP CHƯA (kiểm tra cả HK1 và HK2)
                List<(int maHocSinh, int maLop, int maHocKy)> allPhanLopCheck = phanLopBLL.GetAllPhanLop();
                bool daPhanLopHK1 = allPhanLopCheck.Any(p => p.maHocKy == hocKy1.MaHocKy);
                bool daPhanLopHK2 = allPhanLopCheck.Any(p => p.maHocKy == hocKy2.MaHocKy);
                
                if ((daPhanLopHK1 || daPhanLopHK2) && !boQuaKiemTra)
                {
                    string thongBaoLoi = $"Năm học '{maNamHocCanPhanLop}' đã được phân lớp tự động.\n";
                    if (daPhanLopHK1) thongBaoLoi += $"- HK1 ({hocKy1.TenHocKy}) đã có phân lớp\n";
                    if (daPhanLopHK2) thongBaoLoi += $"- HK2 ({hocKy2.TenHocKy}) đã có phân lớp\n";
                    thongBaoLoi += "\nKhông thể phân lớp lại!\n\nNếu muốn phân lớp lại, vui lòng xóa dữ liệu phân lớp cũ trước.";
                    return (false, thongBaoLoi, 0);
                }

                // 2. XÁC ĐỊNH KỊCH BẢN & TÌM HỌC KỲ NGUỒN (TỪ NĂM HỌC TRƯỚC)
                string kichBan = "";
                HocKyDTO hocKy1NamTruoc = null; // HK1 năm trước
                HocKyDTO hocKy2NamTruoc = null; // HK2 năm trước
                string maNamHocTruoc = ""; // Khai báo ở scope cao hơn

                // Tìm năm học trước
                string[] partsNamHoc = maNamHocCanPhanLop.Split('-');
                if (partsNamHoc.Length == 2 && int.TryParse(partsNamHoc[0], out int namBatDau))
                {
                    maNamHocTruoc = $"{namBatDau - 1}-{namBatDau}";
                    var dsHocKyNamTruoc = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocTruoc);
                    
                    if (dsHocKyNamTruoc != null && dsHocKyNamTruoc.Count > 0)
                    {
                        hocKy1NamTruoc = dsHocKyNamTruoc.FirstOrDefault(hk =>
                            (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                            (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));
                        hocKy2NamTruoc = dsHocKyNamTruoc.FirstOrDefault(hk =>
                            hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));
                    }
                }

                if (hocKy1NamTruoc != null && hocKy2NamTruoc != null)
                {
                    kichBan = "NEXT_YEAR"; // Phân lớp cho năm học mới dựa trên năm học trước
                    Console.WriteLine($"📌 Kịch bản: NEXT_YEAR (Từ năm học {maNamHocTruoc} → {maNamHocCanPhanLop})");
                    Console.WriteLine($"   → Xét điều kiện từ cả HK1 và HK2 của năm học trước");
                }
                else
                {
                    kichBan = "FIRST_TIME"; // Phân lớp lần đầu
                    Console.WriteLine($"📌 Kịch bản: FIRST_TIME (Phân lớp lần đầu cho năm học {maNamHocCanPhanLop})");
                }

                // 3. LẤY DỮ LIỆU CẦN THIẾT
                // Lấy học sinh "Đang học", "Đang học(CT)" (chuyển trường) HOẶC "Nghỉ học" (cho phép phân lớp)
                List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                    .Where(hs => hs.TrangThai == "Đang học" || hs.TrangThai == "Đang học(CT)" || hs.TrangThai == "Nghỉ học")
                    .ToList();
                List<(int maHocSinh, int maLop, int maHocKy)> allPhanLopHist = phanLopBLL.GetAllPhanLop();
                List<LopDTO> allLop = lopHocBUS.DocDSLop();
                List<DiemSoDTO> allDiem = diemSoBUS.GetAllDiemSo();
                List<HanhKiemDTO> allHanhKiem = hanhKiemBUS.GetAllHanhKiem();
                List<XepLoaiDTO> allXepLoai = xepLoaiBUS.GetAllXepLoai();
                // Danh sách tạm để theo dõi phân lớp mới thêm (không cần DTO phức tạp)
                List<(int maHocSinh, int maLop, int maHocKy)> danhSachPhanLopTam = new List<(int, int, int)>();

                // 4. XỬ LÝ THEO KỊCH BẢN
                List<string> danhSachLoi = new List<string>();
                int soHocSinhDaPhanLop = 0;

                if (kichBan == "NEXT_YEAR")
                {
                    // =================================================================
                    // KỊCH BẢN 2: PHÂN LỚP CHO NĂM HỌC MỚI (XÉT ĐIỀU KIỆN TỪ CẢ HK1 VÀ HK2 NĂM TRƯỚC)
                    // =================================================================
                    Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║   KỊCH BẢN 2: NEXT_YEAR (Từ năm học trước → Năm học mới) ║");
                    Console.WriteLine("╚══════════════════════════════════════════════════════════╝");

                    Console.WriteLine($"→ HK1 năm trước: {hocKy1NamTruoc.TenHocKy} {hocKy1NamTruoc.MaNamHoc}");
                    Console.WriteLine($"→ HK2 năm trước: {hocKy2NamTruoc.TenHocKy} {hocKy2NamTruoc.MaNamHoc}");
                    Console.WriteLine($"→ Sẽ phân lớp cho cả HK1 và HK2 năm học mới ({maNamHocCanPhanLop})");

                    // ✅ Lấy TẤT CẢ học sinh "Đang học"
                    var hocSinhDangHocNamTruoc = danhSachHocSinhDangHoc.ToList();

                    Console.WriteLine($"→ Tìm thấy {hocSinhDangHocNamTruoc.Count} học sinh 'Đang học' cần kiểm tra");

                    // ✅ Danh sách học sinh chưa có phân lớp (mới nhập từ Excel) - sẽ phân vào khối 10
                    List<HocSinhDTO> hocSinhChuaPhanLop = new List<HocSinhDTO>();

                    foreach (var hs in hocSinhDangHocNamTruoc)
                    {
                        try
                        {
                            // BƯỚC 1: Lấy điểm HK1 và HK2 năm trước
                            var diemHK1 = allDiem
                                .Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKy1NamTruoc.MaHocKy)
                                .ToList();

                            var diemHK2 = allDiem
                                .Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKy2NamTruoc.MaHocKy)
                                .ToList();

                            // ✅ Kiểm tra học sinh chưa có phân lớp (mới nhập từ Excel)
                            var phanLopHK2NamTruoc = allPhanLopHist
                                .FirstOrDefault(p => p.maHocSinh == hs.MaHS && p.maHocKy == hocKy2NamTruoc.MaHocKy);
                            var phanLopHK1NamTruoc = allPhanLopHist
                                .FirstOrDefault(p => p.maHocSinh == hs.MaHS && p.maHocKy == hocKy1NamTruoc.MaHocKy);
                            
                            bool chuaCoPhanLop = (phanLopHK2NamTruoc.maHocSinh == 0 && phanLopHK1NamTruoc.maHocSinh == 0);

                            if (diemHK1 == null || diemHK1.Count == 0 || diemHK2 == null || diemHK2.Count == 0 || chuaCoPhanLop)
                            {
                                // ✅ Học sinh chưa có phân lớp (mới nhập từ Excel) - sẽ phân vào khối 10 sau
                                if (chuaCoPhanLop)
                                {
                                    hocSinhChuaPhanLop.Add(hs);
                                    Console.WriteLine($"  ℹ️ HS {hs.HoTen} (ID: {hs.MaHS}): Chưa có phân lớp trước đó → Sẽ phân vào khối 10");
                                }
                                else
                                {
                                    string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Chưa có đủ điểm HK1/HK2 năm trước";
                                    Console.WriteLine($"  ⚠️ {loi}");
                                    danhSachLoi.Add(loi);
                                }
                                continue;
                            }

                            // BƯỚC 2: Lấy hạnh kiểm HK1 và HK2
                            var hanhKiemHK1 = allHanhKiem
                                .FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKy1NamTruoc.MaHocKy);

                            var hanhKiemHK2 = allHanhKiem
                                .FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKy2NamTruoc.MaHocKy);

                            if (hanhKiemHK1 == null || hanhKiemHK2 == null)
                            {
                                string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Chưa có đủ hạnh kiểm cả năm";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // BƯỚC 3: TÍNH ĐIỂM TRUNG BÌNH CẢ NĂM
                            double dtbHK1 = diemHK1.Average(d => d.DiemTrungBinh ?? 0);
                            double dtbHK2 = diemHK2.Average(d => d.DiemTrungBinh ?? 0);
                            double dtbCaNam = (dtbHK1 * 1 + dtbHK2 * 2) / 3.0; // HK2 hệ số 2

                            Console.WriteLine($"  → {hs.HoTen}: ĐTB HK1={dtbHK1:0.00}, HK2={dtbHK2:0.00}, Cả năm={dtbCaNam:0.00}");

                            // BƯỚC 4: XÉT HẠNH KIỂM CẢ NĂM
                            string[] thuTuHanhKiem = { "Yếu", "Trung Bình", "Khá", "Tốt" };
                            int indexHK1 = Array.IndexOf(thuTuHanhKiem, hanhKiemHK1.XepLoai);
                            int indexHK2 = Array.IndexOf(thuTuHanhKiem, hanhKiemHK2.XepLoai);

                            if (indexHK1 == -1) indexHK1 = 0;
                            if (indexHK2 == -1) indexHK2 = 0;

                            int indexMin = Math.Min(indexHK1, indexHK2);
                            string hanhKiemCaNam = thuTuHanhKiem[indexMin];

                            Console.WriteLine($"       Hạnh kiểm: HK1={hanhKiemHK1.XepLoai}, HK2={hanhKiemHK2.XepLoai}, Cả năm={hanhKiemCaNam}");

                            // BƯỚC 5: ĐẾM MÔN KÉM VÀ YẾU
                            var tatCaDiemCaNam = diemHK1.Concat(diemHK2).ToList();

                            // Nhóm theo môn học
                            var diemTheoMon = tatCaDiemCaNam
                                .GroupBy(d => d.MaMonHoc)
                                .Select(g => new
                                {
                                    MaMon = g.Key,
                                    DiemTBMon = g.Average(d => d.DiemTrungBinh ?? 0)
                                })
                                .ToList();

                            int soMonKem = diemTheoMon.Count(m => m.DiemTBMon < 3.5);
                            int soMonYeu = diemTheoMon.Count(m => m.DiemTBMon >= 3.5 && m.DiemTBMon < 5.0);

                            Console.WriteLine($"       Môn Kém: {soMonKem}, Môn Yếu: {soMonYeu}");

                            // BƯỚC 6: KIỂM TRA ĐIỀU KIỆN LÊN LỚP
                            bool duDieuKienLenLop = true;
                            List<string> lyDoKhongLenLop = new List<string>();

                            // Điều kiện 1: ĐTB cả năm >= 5.0
                            if (dtbCaNam < 5.0)
                            {
                                duDieuKienLenLop = false;
                                lyDoKhongLenLop.Add($"ĐTB cả năm {dtbCaNam:0.00} < 5.0");
                            }

                            // Điều kiện 2: Hạnh kiểm >= Trung Bình
                            if (indexMin < 1) // Yếu
                            {
                                duDieuKienLenLop = false;
                                lyDoKhongLenLop.Add($"Hạnh kiểm '{hanhKiemCaNam}' < Trung Bình");
                            }

                            // Điều kiện 3: Không có môn Kém
                            if (soMonKem > 0)
                            {
                                duDieuKienLenLop = false;
                                lyDoKhongLenLop.Add($"Có {soMonKem} môn Kém");
                            }

                            // Điều kiện 4: Tối đa 2 môn Yếu
                            if (soMonYeu > 2)
                            {
                                duDieuKienLenLop = false;
                                lyDoKhongLenLop.Add($"Có {soMonYeu} môn Yếu (> 2)");
                            }

                            // BƯỚC 7: LẤY LỚP CŨ VÀ XÁC ĐỊNH LỚP MỚI (ưu tiên lấy từ HK2 năm trước, nếu không có thì lấy từ HK1)
                            // ✅ Biến phanLopHK2NamTruoc và phanLopHK1NamTruoc đã được khai báo ở trên (dòng 173-176)
                            var phanLopNamTruoc = phanLopHK2NamTruoc.maHocSinh != 0 ? phanLopHK2NamTruoc : phanLopHK1NamTruoc;

                            // ✅ Đã kiểm tra ở trên, nên ở đây chắc chắn có phân lớp
                            var lopCu = allLop.FirstOrDefault(l => l.MaLop == phanLopNamTruoc.maLop);
                            if (lopCu == null)
                            {
                                string loi = $"HS {hs.HoTen}: Không tìm thấy thông tin lớp cũ (ID: {phanLopNamTruoc.maLop})";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            int khoiCu = lopCu.MaKhoi;
                            int khoiMoi;

                            if (duDieuKienLenLop)
                            {
                                // Lên khối cao hơn
                                khoiMoi = khoiCu + 1;
                                if (khoiMoi > 12)
                                {
                                    // ✅ CẬP NHẬT TRẠNG THÁI "ĐÃ TỐT NGHIỆP" VÀO SQL
                                    var hocSinhDAO = new HocSinhDAO();
                                    bool capNhatThanhCong = hocSinhDAO.CapNhatTrangThaiHocSinh(hs.MaHS, "Đã tốt nghiệp");
                                    
                                    string loi = $"HS {hs.HoTen}: Đã tốt nghiệp (khối 12), không thể lên lớp";
                                    if (capNhatThanhCong)
                                    {
                                        loi += " → Đã cập nhật trạng thái 'Đã tốt nghiệp'";
                                        Console.WriteLine($"  ✓ {loi}");
                                    }
                                    else
                                    {
                                        loi += " → Lỗi khi cập nhật trạng thái";
                                        Console.WriteLine($"  ❌ {loi}");
                                    }
                                    
                                    danhSachLoi.Add(loi);
                                    continue;
                                }

                                Console.WriteLine($"  ✓ {hs.HoTen}: ĐỦ điều kiện lên lớp (Khối {khoiCu} → Khối {khoiMoi})");
                            }
                            else
                            {
                                // Ở lại khối cũ (học lại)
                                khoiMoi = khoiCu;
                                Console.WriteLine($"  ⚠️ {hs.HoTen}: HỌC LẠI Khối {khoiCu}");
                                Console.WriteLine($"       Lý do: {string.Join(", ", lyDoKhongLenLop)}");
                            }

                            // BƯỚC 8: TÌM LỚP CÓ CHỖ TRỐNG Ở KHỐI MỚI (CHO CẢ HK1 VÀ HK2 NĂM MỚI)
                            var dsLopKhoiMoi = allLop.Where(l => l.MaKhoi == khoiMoi).ToList();

                            if (dsLopKhoiMoi.Count == 0)
                            {
                                string loi = $"HS {hs.HoTen}: Không có lớp nào ở Khối {khoiMoi}";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // Đếm số học sinh trong từng lớp (tổng của cả HK1 và HK2 năm mới)
                            var soLuongHocSinhTrongLop = new Dictionary<int, int>();
                            
                            // Đếm từ database (cả HK1 và HK2)
                            var phanLopHK1Moi = allPhanLopHist.Where(p => p.maHocKy == hocKy1.MaHocKy);
                            var phanLopHK2Moi = allPhanLopHist.Where(p => p.maHocKy == hocKy2.MaHocKy);
                            
                            foreach (var pl in phanLopHK1Moi.Concat(phanLopHK2Moi))
                            {
                                if (!soLuongHocSinhTrongLop.ContainsKey(pl.maLop))
                                    soLuongHocSinhTrongLop[pl.maLop] = 0;
                                soLuongHocSinhTrongLop[pl.maLop]++;
                            }

                            // Thêm số lượng tạm của học sinh vừa phân (cả HK1 và HK2)
                            foreach (var pl in danhSachPhanLopTam.Where(p => p.maHocKy == hocKy1.MaHocKy || p.maHocKy == hocKy2.MaHocKy))
                            {
                                if (!soLuongHocSinhTrongLop.ContainsKey(pl.maLop))
                                    soLuongHocSinhTrongLop[pl.maLop] = 0;
                                soLuongHocSinhTrongLop[pl.maLop]++;
                            }

                            // Tìm lớp có ít học sinh nhất
                            LopDTO lopPhuHop = null;
                            int soHocSinhItNhat = int.MaxValue;

                            foreach (var lop in dsLopKhoiMoi)
                            {
                                int soHS = soLuongHocSinhTrongLop.ContainsKey(lop.MaLop) ? soLuongHocSinhTrongLop[lop.MaLop] : 0;
                                if (soHS < soHocSinhItNhat)
                                {
                                    soHocSinhItNhat = soHS;
                                    lopPhuHop = lop;
                                }
                            }

                            if (lopPhuHop == null)
                            {
                                string loi = $"HS {hs.HoTen}: Không tìm thấy lớp phù hợp ở Khối {khoiMoi}";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // BƯỚC 9: THÊM VÀO LỚP MỚI CHO CẢ HK1 VÀ HK2 (CÙNG LỚP)
                            bool themHK1ThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
                            bool themHK2ThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);

                            if (themHK1ThanhCong && themHK2ThanhCong)
                            {
                                soHocSinhDaPhanLop += 2; // Đếm cả HK1 và HK2

                                // Thêm vào danh sách tạm để cập nhật số lượng
                                danhSachPhanLopTam.Add((hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy));
                                danhSachPhanLopTam.Add((hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy));

                                Console.WriteLine($"  ✓ {hs.HoTen} → Lớp {lopPhuHop.TenLop} (Khối {khoiMoi}) - HK1 & HK2");
                            }
                            else
                            {
                                string loi = $"HS {hs.HoTen}: Lỗi khi thêm vào lớp {lopPhuHop.TenLop}";
                                if (!themHK1ThanhCong) loi += " (HK1 thất bại)";
                                if (!themHK2ThanhCong) loi += " (HK2 thất bại)";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                                
                                // Rollback nếu một trong hai thất bại
                                if (themHK1ThanhCong)
                                {
                                    phanLopDAO.XoaPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
                                }
                                if (themHK2ThanhCong)
                                {
                                    phanLopDAO.XoaPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string loi = $"HS {hs.HoTen}: Lỗi xử lý - {ex.Message}";
                            Console.WriteLine($"  ❌ {loi}");
                            danhSachLoi.Add(loi);
                        }
                    }

                    // ✅ XỬ LÝ HỌC SINH CHƯA CÓ PHÂN LỚP (MỚI NHẬP TỪ EXCEL) - PHÂN VÀO KHỐI 10
                    if (hocSinhChuaPhanLop.Count > 0)
                    {
                        Console.WriteLine($"\n╔══════════════════════════════════════════════════════════╗");
                        Console.WriteLine($"║   XỬ LÝ HỌC SINH CHƯA CÓ PHÂN LỚP (Phân vào Khối 10)      ║");
                        Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
                        Console.WriteLine($"→ Tìm thấy {hocSinhChuaPhanLop.Count} học sinh chưa có phân lớp trước đó");
                        Console.WriteLine($"→ Tất cả học sinh này sẽ được phân vào KHỐI 10");
                        Console.WriteLine($"→ Phân lớp theo chữ cái đầu tiên của tên, phân đều vào các lớp");

                        int khoiCanPhanLop = 10;
                        var dsLopKhoi10 = allLop
                            .Where(l => l.MaKhoi == khoiCanPhanLop)
                            .OrderBy(l => l.MaLop)
                            .ToList();

                        if (dsLopKhoi10.Count == 0)
                        {
                            string loi = $"Không có lớp nào cho Khối {khoiCanPhanLop}";
                            Console.WriteLine($"  ❌ {loi}");
                            foreach (var hs in hocSinhChuaPhanLop)
                            {
                                danhSachLoi.Add($"HS {hs.HoTen}: {loi}");
                            }
                        }
                        else
                        {
                            // Đếm số học sinh đã có trong từng lớp (tổng cả HK1 và HK2 - bao gồm cả tạm)
                            var soLuongHocSinhTrongLop = new Dictionary<int, int>();

                            foreach (var lop in dsLopKhoi10)
                            {
                                int soHSHK1 = allPhanLopHist.Count(p => p.maLop == lop.MaLop && p.maHocKy == hocKy1.MaHocKy);
                                int soHSHK2 = allPhanLopHist.Count(p => p.maLop == lop.MaLop && p.maHocKy == hocKy2.MaHocKy);
                                soLuongHocSinhTrongLop[lop.MaLop] = soHSHK1 + soHSHK2;
                            }

                            // Cộng thêm số tạm (cả HK1 và HK2)
                            foreach (var phanLopTam in danhSachPhanLopTam)
                            {
                                if (phanLopTam.maHocKy == hocKy1.MaHocKy || phanLopTam.maHocKy == hocKy2.MaHocKy)
                                {
                                    if (soLuongHocSinhTrongLop.ContainsKey(phanLopTam.maLop))
                                        soLuongHocSinhTrongLop[phanLopTam.maLop]++;
                                    else
                                        soLuongHocSinhTrongLop[phanLopTam.maLop] = 1;
                                }
                            }

                            // Nhóm học sinh theo chữ cái đầu tiên của tên
                            var hocSinhTheoChuCai = new Dictionary<char, List<HocSinhDTO>>();

                            foreach (var hs in hocSinhChuaPhanLop)
                            {
                                char chuCaiDau = '?';
                                if (!string.IsNullOrWhiteSpace(hs.HoTen))
                                {
                                    string tenTrimmed = hs.HoTen.Trim();
                                    if (tenTrimmed.Length > 0)
                                    {
                                        chuCaiDau = char.ToUpper(tenTrimmed[0]);
                                        if (!char.IsLetter(chuCaiDau))
                                        {
                                            chuCaiDau = '?';
                                        }
                                    }
                                }

                                if (!hocSinhTheoChuCai.ContainsKey(chuCaiDau))
                                {
                                    hocSinhTheoChuCai[chuCaiDau] = new List<HocSinhDTO>();
                                }
                                hocSinhTheoChuCai[chuCaiDau].Add(hs);
                            }

                            Console.WriteLine($"\n→ Đã nhóm học sinh theo chữ cái đầu tiên:");
                            foreach (var kvp in hocSinhTheoChuCai.OrderBy(x => x.Key))
                            {
                                Console.WriteLine($"  → Chữ '{kvp.Key}': {kvp.Value.Count} học sinh");
                            }

                            // Phân đều học sinh theo từng nhóm chữ cái vào các lớp khối 10
                            Console.WriteLine($"\n→ Bắt đầu phân bổ học sinh vào {dsLopKhoi10.Count} lớp khối 10...");

                            var danhSachChuCai = hocSinhTheoChuCai.Keys
                                .OrderBy(c => c == '?' ? 999 : (int)c)
                                .ToList();

                            foreach (var chuCai in danhSachChuCai)
                            {
                                List<HocSinhDTO> dsHSTheoChuCai = hocSinhTheoChuCai[chuCai];

                                Console.WriteLine($"\n  → Phân bổ {dsHSTheoChuCai.Count} học sinh tên bắt đầu bằng chữ '{chuCai}':");

                                foreach (var hs in dsHSTheoChuCai)
                                {
                                    try
                                    {
                                        // Tìm lớp có ít học sinh nhất
                                        var lopPhuHop = dsLopKhoi10
                                            .OrderBy(lop => soLuongHocSinhTrongLop.ContainsKey(lop.MaLop) ? soLuongHocSinhTrongLop[lop.MaLop] : 0)
                                            .ThenBy(lop => lop.MaLop)
                                            .First();

                                        // Thêm vào lớp cho CẢ HK1 và HK2 (cùng lớp)
                                        bool themHK1ThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
                                        bool themHK2ThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);

                                        if (themHK1ThanhCong && themHK2ThanhCong)
                                        {
                                            soHocSinhDaPhanLop += 2; // Đếm cả HK1 và HK2
                                            danhSachPhanLopTam.Add((hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy));
                                            danhSachPhanLopTam.Add((hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy));

                                            // Cập nhật số lượng (tổng cả HK1 và HK2)
                                            if (soLuongHocSinhTrongLop.ContainsKey(lopPhuHop.MaLop))
                                                soLuongHocSinhTrongLop[lopPhuHop.MaLop] += 2;
                                            else
                                                soLuongHocSinhTrongLop[lopPhuHop.MaLop] = 2;

                                            Console.WriteLine($"    ✓ {hs.HoTen} → Lớp {lopPhuHop.TenLop} (HK1 & HK2 - Sĩ số: {soLuongHocSinhTrongLop[lopPhuHop.MaLop]})");
                                        }
                                        else
                                        {
                                            string loi = $"HS {hs.HoTen}: Lỗi khi thêm vào lớp {lopPhuHop.TenLop}";
                                            if (!themHK1ThanhCong) loi += " (HK1 thất bại)";
                                            if (!themHK2ThanhCong) loi += " (HK2 thất bại)";
                                            Console.WriteLine($"    ❌ {loi}");
                                            danhSachLoi.Add(loi);

                                            // Rollback
                                            if (themHK1ThanhCong)
                                            {
                                                phanLopDAO.XoaPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
                                            }
                                            if (themHK2ThanhCong)
                                            {
                                                phanLopDAO.XoaPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string loi = $"HS {hs.HoTen}: Lỗi xử lý - {ex.Message}";
                                        Console.WriteLine($"    ❌ {loi}");
                                        danhSachLoi.Add(loi);
                                    }
                                }
                            }

                            // Hiển thị kết quả phân lớp cho khối 10
                            Console.WriteLine($"\n  → Kết quả phân lớp Khối {khoiCanPhanLop}:");
                            foreach (var lop in dsLopKhoi10)
                            {
                                int siSo = soLuongHocSinhTrongLop.ContainsKey(lop.MaLop) ? soLuongHocSinhTrongLop[lop.MaLop] : 0;
                                Console.WriteLine($"     • {lop.TenLop}: {siSo} học sinh");
                            }
                        }
                    }
                }
                else if (kichBan == "FIRST_TIME")
                {
                    // =================================================================
                    // KỊCH BẢN 3: PHÂN LỚP LẦN ĐẦU (PHÂN ĐỀU VÀO CÁC LỚP KHỐI 10 THEO BẢNG CHỮ CÁI)
                    // =================================================================
                    Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║   KỊCH BẢN 3: FIRST_TIME (Phân lớp lần đầu)                ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

                    // Lấy TẤT CẢ học sinh "Đang học"
                    var hocSinhCanPhanLop = danhSachHocSinhDangHoc.ToList();

                    Console.WriteLine($"→ Tìm thấy {hocSinhCanPhanLop.Count} học sinh 'Đang học' cần phân lớp");
                    Console.WriteLine($"→ Tất cả học sinh sẽ được phân vào KHỐI 10");
                    Console.WriteLine($"→ Phân lớp theo chữ cái đầu tiên của tên, phân đều vào các lớp");

                    // ✅ TẤT CẢ học sinh đều vào khối 10 (không xét năm sinh)
                    int khoiCanPhanLop = 10;

                    // Lấy danh sách lớp của khối 10 - SẮP XẾP THEO MÃ LỚP
                    var dsLopKhoi10 = allLop
                        .Where(l => l.MaKhoi == khoiCanPhanLop)
                        .OrderBy(l => l.MaLop)  // Sắp xếp theo MaLop
                        .ToList();

                    if (dsLopKhoi10.Count == 0)
                    {
                        string loi = $"Không có lớp nào cho Khối {khoiCanPhanLop}";
                        Console.WriteLine($"  ❌ {loi}");
                        foreach (var hs in hocSinhCanPhanLop)
                        {
                            danhSachLoi.Add($"HS {hs.HoTen}: {loi}");
                        }
                        return (false, loi, 0);
                    }

                    Console.WriteLine($"  → Số lớp khối 10: {dsLopKhoi10.Count} lớp ({string.Join(", ", dsLopKhoi10.Select(l => l.TenLop))})");

                    // ✅ Đếm số học sinh đã có trong từng lớp (tổng cả HK1 và HK2 - bao gồm cả tạm)
                    var soLuongHocSinhTrongLop = new Dictionary<int, int>();

                    // Đếm từ database (cả HK1 và HK2)
                    foreach (var lop in dsLopKhoi10)
                    {
                        int soHSHK1 = allPhanLopHist.Count(p => p.maLop == lop.MaLop && p.maHocKy == hocKy1.MaHocKy);
                        int soHSHK2 = allPhanLopHist.Count(p => p.maLop == lop.MaLop && p.maHocKy == hocKy2.MaHocKy);
                        soLuongHocSinhTrongLop[lop.MaLop] = soHSHK1 + soHSHK2;
                    }

                    // Cộng thêm số tạm (cả HK1 và HK2)
                    foreach (var phanLopTam in danhSachPhanLopTam)
                    {
                        if (phanLopTam.maHocKy == hocKy1.MaHocKy || phanLopTam.maHocKy == hocKy2.MaHocKy)
                        {
                            if (soLuongHocSinhTrongLop.ContainsKey(phanLopTam.maLop))
                                soLuongHocSinhTrongLop[phanLopTam.maLop]++;
                            else
                                soLuongHocSinhTrongLop[phanLopTam.maLop] = 1;
                        }
                    }

                    // ✅ Nhóm học sinh theo chữ cái đầu tiên của tên (bỏ qua khoảng trắng, lấy chữ cái đầu tiên)
                    var hocSinhTheoChuCai = new Dictionary<char, List<HocSinhDTO>>();

                    foreach (var hs in hocSinhCanPhanLop)
                    {
                        // Lấy chữ cái đầu tiên của tên (bỏ qua khoảng trắng, chuyển thành chữ hoa)
                        char chuCaiDau = '?';
                        if (!string.IsNullOrWhiteSpace(hs.HoTen))
                        {
                            string tenTrimmed = hs.HoTen.Trim();
                            if (tenTrimmed.Length > 0)
                            {
                                chuCaiDau = char.ToUpper(tenTrimmed[0]);
                                // Nếu không phải chữ cái, gán thành '?' để nhóm các ký tự đặc biệt
                                if (!char.IsLetter(chuCaiDau))
                                {
                                    chuCaiDau = '?';
                                }
                            }
                        }

                        if (!hocSinhTheoChuCai.ContainsKey(chuCaiDau))
                        {
                            hocSinhTheoChuCai[chuCaiDau] = new List<HocSinhDTO>();
                        }
                        hocSinhTheoChuCai[chuCaiDau].Add(hs);
                    }

                    Console.WriteLine($"\n→ Đã nhóm học sinh theo chữ cái đầu tiên:");
                    foreach (var kvp in hocSinhTheoChuCai.OrderBy(x => x.Key))
                    {
                        Console.WriteLine($"  → Chữ '{kvp.Key}': {kvp.Value.Count} học sinh");
                    }

                    // ✅ Phân đều học sinh theo từng nhóm chữ cái vào các lớp khối 10
                    Console.WriteLine($"\n→ Bắt đầu phân bổ học sinh vào {dsLopKhoi10.Count} lớp khối 10...");

                    // Sắp xếp các chữ cái để xử lý theo thứ tự A-Z, sau đó là ký tự đặc biệt
                    var danhSachChuCai = hocSinhTheoChuCai.Keys
                        .OrderBy(c => c == '?' ? 999 : (int)c) // Ký tự đặc biệt xử lý sau cùng
                        .ToList();

                    int lopIndex = 0; // Index để phân vòng tròn cho toàn bộ quá trình

                    foreach (var chuCai in danhSachChuCai)
                    {
                        List<HocSinhDTO> dsHSTheoChuCai = hocSinhTheoChuCai[chuCai];

                        Console.WriteLine($"\n  → Phân bổ {dsHSTheoChuCai.Count} học sinh tên bắt đầu bằng chữ '{chuCai}':");

                        // Phân đều nhóm chữ cái này vào các lớp
                        foreach (var hs in dsHSTheoChuCai)
                        {
                            try
                            {
                                // ✅ Lấy lớp theo thứ tự vòng tròn, nhưng ưu tiên lớp có ít học sinh hơn để cân bằng sĩ số
                                // Tìm lớp có ít học sinh nhất trong danh sách
                                var lopPhuHop = dsLopKhoi10
                                    .OrderBy(lop => soLuongHocSinhTrongLop.ContainsKey(lop.MaLop) ? soLuongHocSinhTrongLop[lop.MaLop] : 0)
                                    .ThenBy(lop => lop.MaLop) // Nếu bằng nhau thì ưu tiên MaLop nhỏ hơn
                                    .First();

                                // ✅ Thêm vào lớp cho CẢ HK1 và HK2 (cùng lớp)
                                bool themHK1ThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
                                bool themHK2ThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);

                                if (themHK1ThanhCong && themHK2ThanhCong)
                                {
                                    soHocSinhDaPhanLop += 2; // Đếm cả HK1 và HK2
                                    danhSachPhanLopTam.Add((hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy));
                                    danhSachPhanLopTam.Add((hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy));

                                    // Cập nhật số lượng (tổng cả HK1 và HK2)
                                    if (soLuongHocSinhTrongLop.ContainsKey(lopPhuHop.MaLop))
                                        soLuongHocSinhTrongLop[lopPhuHop.MaLop] += 2; // +2 vì thêm cả HK1 và HK2
                                    else
                                        soLuongHocSinhTrongLop[lopPhuHop.MaLop] = 2;

                                    Console.WriteLine($"    ✓ {hs.HoTen} → Lớp {lopPhuHop.TenLop} (HK1 & HK2 - Sĩ số: {soLuongHocSinhTrongLop[lopPhuHop.MaLop]})");
                                }
                                else
                                {
                                    string loi = $"HS {hs.HoTen}: Lỗi khi thêm vào lớp {lopPhuHop.TenLop}";
                                    if (!themHK1ThanhCong) loi += " (HK1 thất bại)";
                                    if (!themHK2ThanhCong) loi += " (HK2 thất bại)";
                                    Console.WriteLine($"    ❌ {loi}");
                                    danhSachLoi.Add(loi);
                                    
                                    // Rollback nếu một trong hai thất bại
                                    if (themHK1ThanhCong)
                                    {
                                        phanLopDAO.XoaPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
                                    }
                                    if (themHK2ThanhCong)
                                    {
                                        phanLopDAO.XoaPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                string loi = $"HS {hs.HoTen}: Lỗi xử lý - {ex.Message}";
                                Console.WriteLine($"    ❌ {loi}");
                                danhSachLoi.Add(loi);
                            }
                        }
                    }

                    // Hiển thị kết quả phân lớp cho khối 10
                    Console.WriteLine($"\n  → Kết quả phân lớp Khối {khoiCanPhanLop}:");
                    foreach (var lop in dsLopKhoi10)
                    {
                        int siSo = soLuongHocSinhTrongLop.ContainsKey(lop.MaLop) ? soLuongHocSinhTrongLop[lop.MaLop] : 0;
                        Console.WriteLine($"     • {lop.TenLop}: {siSo} học sinh");
                    }
                }

                // 5. KẾT QUẢ
                Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine("  ║                   KẾT QUẢ PHÂN LỚP                       ║");
                Console.WriteLine("  ╚══════════════════════════════════════════════════════════╝");
                Console.WriteLine($"✓ Đã phân lớp thành công: {soHocSinhDaPhanLop} học sinh");

                if (danhSachLoi.Count > 0)
                {
                    Console.WriteLine($"⚠️ Số lỗi/cảnh báo: {danhSachLoi.Count}");
                    Console.WriteLine("\nChi tiết lỗi:");
                    foreach (var loi in danhSachLoi.Take(10))
                    {
                        Console.WriteLine($"  - {loi}");
                    }
                    if (danhSachLoi.Count > 10)
                    {
                        Console.WriteLine($"  ... và {danhSachLoi.Count - 10} lỗi khác");
                    }
                }

                // TẠO THÔNG BÁO CHI TIẾT
                string thongBao = $"╔════════════════════════════════════════════════╗\n";
                thongBao +=       $"║        KẾT QUẢ PHÂN LỚP TỰ ĐỘNG                ║\n";
                thongBao +=       $"╚════════════════════════════════════════════════╝\n\n";

                // Thông tin năm học
                thongBao += $"📅 Năm học: {maNamHocCanPhanLop}\n";
                thongBao += $"   → Phân lớp cho cả HK1 ({hocKy1.TenHocKy}) và HK2 ({hocKy2.TenHocKy})\n\n";

                // Kịch bản
                if (kichBan == "NEXT_YEAR")
                {
                    thongBao += $"📋 Kịch bản: Phân lớp cho năm học mới\n";
                    thongBao += $"   → Xét điều kiện từ cả HK1 và HK2 năm học trước\n";
                    if (hocKy1NamTruoc != null && hocKy2NamTruoc != null)
                    {
                        thongBao += $"   Nguồn: HK1 {hocKy1NamTruoc.MaNamHoc} và HK2 {hocKy2NamTruoc.MaNamHoc}\n\n";
                    }
                }
                else if (kichBan == "FIRST_TIME")
                {
                    thongBao += $"📋 Kịch bản: Phân lớp lần đầu\n";
                    thongBao += $"   → Tất cả học sinh vào Khối 10\n";
                    thongBao += $"   → Phân lớp theo chữ cái đầu tiên, phân đều vào các lớp\n\n";
                }

                // Kết quả phân lớp
                thongBao += $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
                if (kichBan == "FIRST_TIME" || kichBan == "NEXT_YEAR")
                {
                    // Đếm số học sinh (chia 2 vì mỗi học sinh được phân cho cả HK1 và HK2)
                    int soHocSinhThucTe = soHocSinhDaPhanLop / 2;
                    thongBao += $"✅ THÀNH CÔNG: {soHocSinhThucTe} học sinh\n";
                    thongBao += $"   → Đã phân lớp cho cả HK1 và HK2 ({soHocSinhDaPhanLop} bản ghi phân lớp)\n\n";
                }
                else
                {
                    thongBao += $"✅ THÀNH CÔNG: {soHocSinhDaPhanLop} học sinh\n";
                }

                // Thống kê theo kịch bản
                if (kichBan == "NEXT_YEAR")
                {
                    // Đếm số học sinh lên lớp / ở lại
                    int soHSLenLop = 0;
                    int soHSOLai = 0;

                    foreach (var hs in danhSachHocSinhDangHoc)
                    {
                        // Lấy phân lớp mới (từ HK1 hoặc HK2 năm mới, cả hai đều cùng lớp)
                        var phanLopMoi = danhSachPhanLopTam.FirstOrDefault(p => p.maHocSinh == hs.MaHS && (p.maHocKy == hocKy1.MaHocKy || p.maHocKy == hocKy2.MaHocKy));
                        if (phanLopMoi.maHocSinh != 0) // Đã phân lớp
                        {
                            // Lấy phân lớp cũ từ năm học trước (ưu tiên HK2, nếu không có thì lấy HK1)
                            var phanLopHK2Cu = allPhanLopHist.FirstOrDefault(p => p.maHocSinh == hs.MaHS && p.maHocKy == hocKy2NamTruoc.MaHocKy);
                            var phanLopHK1Cu = allPhanLopHist.FirstOrDefault(p => p.maHocSinh == hs.MaHS && p.maHocKy == hocKy1NamTruoc.MaHocKy);
                            var phanLopCu = phanLopHK2Cu.maHocSinh != 0 ? phanLopHK2Cu : phanLopHK1Cu;
                            
                            if (phanLopCu.maHocSinh != 0)
                            {
                                var lopCu = allLop.FirstOrDefault(l => l.MaLop == phanLopCu.maLop);
                                var lopMoi = allLop.FirstOrDefault(l => l.MaLop == phanLopMoi.maLop);

                                if (lopCu != null && lopMoi != null)
                                {
                                    if (lopMoi.MaKhoi > lopCu.MaKhoi) soHSLenLop++;
                                    else if (lopMoi.MaKhoi == lopCu.MaKhoi) soHSOLai++;
                                }
                            }
                        }
                    }

                    thongBao += $"   • Lên lớp: {soHSLenLop} học sinh\n";
                    thongBao += $"   • Ở lại (học lại): {soHSOLai} học sinh\n";

                    if (soHocSinhDaPhanLop > 0)
                    {
                        double tyLe = (double)soHSLenLop / soHocSinhDaPhanLop * 100;
                        thongBao += $"   • Tỷ lệ lên lớp: {tyLe:0.0}%\n";
                    }
                }

                // Lỗi/Cảnh báo
                if (danhSachLoi.Count > 0)
                {
                    thongBao += $"\n⚠️ LỖI/CẢNH BÁO: {danhSachLoi.Count} trường hợp\n";
                    thongBao += $"\nChi tiết (tất cả {danhSachLoi.Count} lỗi):\n";

                    // ✅ HIỂN THỊ TẤT CẢ CÁC LỖI (không giới hạn 5)
                    for (int i = 0; i < danhSachLoi.Count; i++)
                    {
                        thongBao += $"   {i + 1}. {danhSachLoi[i]}\n";
                    }
                }

                thongBao += $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";

                return (true, thongBao, soHocSinhDaPhanLop);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi nghiêm trọng trong quá trình phân lớp: {ex.Message}\n{ex.StackTrace}", 0);
            }
        }

        // Hàm helper phân bổ học sinh vào lớp
        private int PhanBoHocSinhVaoLop(Dictionary<int, List<HocSinhDTO>> hocSinhTheoKhoiMoi,
                                        Dictionary<int, List<LopDTO>> lopTheoKhoiMoi,
                                        int maHocKyMoi, List<string> hocSinhGapLoi)
        {
            int soHocSinhDaPhanLop = 0;
            foreach (var kvp in hocSinhTheoKhoiMoi)
            {
                int khoi = kvp.Key;
                List<HocSinhDTO> dsHS = kvp.Value;

                if (!lopTheoKhoiMoi.ContainsKey(khoi) || lopTheoKhoiMoi[khoi].Count == 0)
                {
                    foreach (var hs in dsHS) hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Không có lớp cho khối {khoi}");
                    continue;
                }

                List<LopDTO> dsLop = lopTheoKhoiMoi[khoi];
                int lopIndex = 0;

                foreach (var hs in dsHS)
                {
                    // Chỉ thêm nếu chưa có
                    if (phanLopBLL.CheckHocSinhDaPhanLop(hs.MaHS, maHocKyMoi))
                    {
                        Console.WriteLine($"HS {hs.MaHS} đã tồn tại trong HK mới, bỏ qua.");
                        soHocSinhDaPhanLop++; // Tính là đã phân lớp
                        continue;
                    }

                    bool daPhanLop = false;
                    int soLanThu = 0;
                    while (!daPhanLop && soLanThu < dsLop.Count)
                    {
                        LopDTO lop = dsLop[lopIndex];
                        int siSoHienTai = phanLopBLL.CountHocSinhInLop(lop.MaLop, maHocKyMoi);

                        if (siSoHienTai < 30) // Giới hạn sĩ số
                        {
                            try
                            {
                                if (phanLopBLL.AddPhanLop(hs.MaHS, lop.MaLop, maHocKyMoi))
                                {
                                    soHocSinhDaPhanLop++;
                                    daPhanLop = true;
                                }
                                else
                                {
                                    // Lỗi không mong muốn từ AddPhanLop
                                    hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Lỗi AddPhanLop vào lớp {lop.TenLop}");
                                }
                            }
                            catch (ArgumentException argEx)
                            {
                                // Bắt lỗi nếu học sinh đã tồn tại (dù Check ở trên)
                                hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: {argEx.Message}");
                                daPhanLop = true; // Coi như đã xử lý, không thử lớp khác nữa
                            }
                            catch (Exception addEx)
                            {
                                hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Lỗi nghiêm trọng khi thêm vào lớp {lop.TenLop} ({addEx.Message})");
                                daPhanLop = true; // Dừng thử
                            }
                        }

                        lopIndex = (lopIndex + 1) % dsLop.Count;
                        soLanThu++;
                    }
                    if (!daPhanLop)
                    {
                        hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Không tìm được lớp phù hợp (có thể các lớp khối {khoi} đã đầy)");
                    }
                }
            }
            return soHocSinhDaPhanLop;
        }


        // Hàm helper kiểm tra Học lực HK1 có đủ để lên HK2 không
        private bool IsHocLucDuDieuKienHK2(string hocLuc)
        {
            if (string.IsNullOrEmpty(hocLuc)) return false; // Cần có xếp loại
            string hlLower = hocLuc.Trim().ToLower();
            // Chỉ cần không phải là "Kém" (hoặc logic khác nếu trường yêu cầu)
            return hlLower != "kém";
            // Hoặc return hlLower == "yếu" || hlLower == "trung bình" || hlLower == "khá" || hlLower == "giỏi";
        }

        // Hàm helper kiểm tra Hạnh kiểm HK1 có đủ để lên HK2 không
        private bool IsHanhKiemDuDieuKienHK2(string hanhKiem)
        {
            if (string.IsNullOrEmpty(hanhKiem)) return false; // Cần có hạnh kiểm
            string hkLower = hanhKiem.Trim().ToLower();
            return hkLower == "trung bình" || hkLower == "khá" || hkLower == "tốt";
        }

        // Hàm helper kiểm tra Hạnh kiểm cả năm có đủ để lên lớp không
        private bool IsHanhKiemDuDieuKienLenLop(string hanhKiemCaNam)
        {
            if (string.IsNullOrEmpty(hanhKiemCaNam)) return false;
            string hkLower = hanhKiemCaNam.ToLower();
            // Phải từ Khá trở lên
            return hkLower == "trung bình" || hkLower == "khá" || hkLower == "tốt";
        }


        // Hàm helper tính ĐTB cả năm
        private float TinhDTBCaNam(Dictionary<int, float?> diemHK1, Dictionary<int, float?> diemHK2)
        {
            // Lấy danh sách mã môn học có ở CẢ 2 HỌC KỲ
            var maMonHocChung = diemHK1.Keys.Intersect(diemHK2.Keys).ToList();

            // Lấy danh sách mã môn học CHỈ CÓ ở HK1
            var maMonChiHK1 = diemHK1.Keys.Except(diemHK2.Keys).ToList();

            // Lấy danh sách mã môn học CHỈ CÓ ở HK2
            var maMonChiHK2 = diemHK2.Keys.Except(diemHK1.Keys).ToList();

            if (maMonHocChung.Count == 0 && maMonChiHK1.Count == 0 && maMonChiHK2.Count == 0)
                return 0f; // Không có môn nào để tính

            float tongDiemTheoTrongSo = 0;
            float tongTrongSo = 0; // Trọng số ở đây là số lượng môn học

            // 1. Tính các môn có cả 2 kỳ (ĐTB Môn = (HK1 + HK2*2)/3)
            foreach (int maMon in maMonHocChung)
            {
                float? d1 = diemHK1[maMon];
                float? d2 = diemHK2[maMon];

                if (d1.HasValue && d2.HasValue)
                {
                    float dtbMonCaNam = (d1.Value + d2.Value * 2) / 3.0f;
                    tongDiemTheoTrongSo += dtbMonCaNam;
                    tongTrongSo += 1; // 1 môn
                }
            }

            // 2. Tính các môn chỉ có ở HK1 (ĐTB Môn = HK1)
            foreach (int maMon in maMonChiHK1)
            {
                float? d1 = diemHK1[maMon];
                if (d1.HasValue)
                {
                    tongDiemTheoTrongSo += d1.Value; // Điểm môn đó = điểm HK1
                    tongTrongSo += 1; // 1 môn
                }
            }

            // 3. Tính các môn chỉ có ở HK2 (ĐTB Môn = HK2)
            foreach (int maMon in maMonChiHK2)
            {
                float? d2 = diemHK2[maMon];
                if (d2.HasValue)
                {
                    tongDiemTheoTrongSo += d2.Value; // Điểm môn đó = điểm HK2
                    tongTrongSo += 1; // 1 môn
                }
            }

            // Trả về ĐTB chung của tất cả các môn
            return (tongTrongSo > 0) ? (tongDiemTheoTrongSo / tongTrongSo) : 0f;
        }

        // Hàm helper xét hạnh kiểm cả năm (logic đơn giản: lấy mức thấp hơn)
        private string XetHanhKiemCaNam(string hk1, string hk2)
        {
            if (string.IsNullOrEmpty(hk1) || string.IsNullOrEmpty(hk2)) return "Chưa có"; // Hoặc null

            int level1 = HanhKiemLevel(hk1);
            int level2 = HanhKiemLevel(hk2);

            int minLevel = Math.Min(level1, level2);

            switch (minLevel)
            {
                case 3: return "Tốt";
                case 2: return "Khá";
                case 1: return "Trung bình";
                default: return "Yếu";
            }
        }

        private int HanhKiemLevel(string hanhKiem)
        {
            if (string.IsNullOrEmpty(hanhKiem)) return 0;
            string lower = hanhKiem.ToLower();
            if (lower == "tốt") return 3;
            if (lower == "khá") return 2;
            if (lower == "trung bình") return 1;
            return 0; // Yếu hoặc không xác định
        }


        #endregion

        #region Tạo preview (PREVIEW CHÍNH XÁC)

        public Dictionary<string, object> TaoPreviewPhanLop(int maHocKyCanPhanLop)
        {
            Dictionary<string, object> preview = new Dictionary<string, object>();
            try
            {
                // 1. LẤY THÔNG TIN HỌC KỲ CẦN PHÂN LỚP
                HocKyDTO hocKyCanPhanLop = hocKyBUS.LayHocKyTheoMa(maHocKyCanPhanLop);
                if (hocKyCanPhanLop == null)
                {
                    preview["Loi"] = "Không tìm thấy học kỳ cần phân lớp";
                    return preview;
                }

                // ✅ XÁC ĐỊNH NĂM HỌC CẦN PHÂN LỚP
                string maNamHocCanPhanLop = hocKyCanPhanLop.MaNamHoc;
                
                // Lấy cả HK1 và HK2 của năm học này
                var dsHocKyNamHoc = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocCanPhanLop);
                HocKyDTO hocKy1 = dsHocKyNamHoc?.FirstOrDefault(hk =>
                    (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                    (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));
                HocKyDTO hocKy2 = dsHocKyNamHoc?.FirstOrDefault(hk =>
                    hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));

                if (hocKy1 == null || hocKy2 == null)
                {
                    preview["Loi"] = $"Năm học {maNamHocCanPhanLop} phải có đầy đủ HK1 và HK2!";
                    return preview;
                }

                // 2. XÁC ĐỊNH KỊCH BẢN & TÌM HỌC KỲ NGUỒN (TỪ NĂM HỌC TRƯỚC)
                string kichBan = "";
                HocKyDTO hocKy1NamTruoc = null;
                HocKyDTO hocKy2NamTruoc = null;

                // Tìm năm học trước
                string[] parts = maNamHocCanPhanLop.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                {
                    string maNamHocTruoc = $"{namBatDau - 1}-{namBatDau}";
                    var dsHocKyNamTruoc = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocTruoc);
                    
                    if (dsHocKyNamTruoc != null && dsHocKyNamTruoc.Count > 0)
                    {
                        hocKy1NamTruoc = dsHocKyNamTruoc.FirstOrDefault(hk =>
                            (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                            (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));
                        hocKy2NamTruoc = dsHocKyNamTruoc.FirstOrDefault(hk =>
                            hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));
                    }
                }

                if (hocKy1NamTruoc != null && hocKy2NamTruoc != null)
                {
                    kichBan = "NEXT_YEAR";
                    preview["LoaiPhanLop"] = "Phân lớp cho năm học mới (Xét từ năm học trước)";
                    preview["HocKyNguon"] = $"HK1 & HK2 {hocKy1NamTruoc.MaNamHoc}";
                }
                else
                {
                    kichBan = "FIRST_TIME";
                    preview["LoaiPhanLop"] = "Phân lớp lần đầu (Theo chữ cái, Khối 10)";
                    preview["HocKyNguon"] = "Không có (Phân lớp mới)";
                }

                // 3. LẤY DỮ LIỆU
                // Lấy học sinh "Đang học", "Đang học(CT)" (chuyển trường) HOẶC "Nghỉ học" (cho phép phân lớp)
                List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                    .Where(hs => hs.TrangThai == "Đang học" || hs.TrangThai == "Đang học(CT)" || hs.TrangThai == "Nghỉ học")
                    .ToList();

                List<(int maHocSinh, int maLop, int maHocKy)> allPhanLopHist = phanLopBLL.GetAllPhanLop();
                List<LopDTO> allLop = lopHocBUS.DocDSLop();
                List<DiemSoDTO> allDiem = diemSoBUS.GetAllDiemSo();
                List<HanhKiemDTO> allHanhKiem = hanhKiemBUS.GetAllHanhKiem();

                // 4. TÍNH TOÁN PREVIEW THEO KỊCH BẢN
                int soHSDuDieuKien = 0;
                int soHSKhongDuDieuKien = 0;
                int soHSLenLop = 0;
                int soHSOLai = 0;
                int soHSLoiDuLieu = 0;

                if (kichBan == "NEXT_YEAR")
                {
                    // KỊCH BẢN NEXT_YEAR: Đếm số HS lên lớp / ở lại (xét từ cả HK1 và HK2 năm trước)
                    var hocSinhDangHocNamTruoc = danhSachHocSinhDangHoc.ToList();

                    foreach (var hs in hocSinhDangHocNamTruoc)
                    {
                        try
                        {
                            // Lấy điểm HK1 và HK2 năm trước
                            var diemHK1 = allDiem.Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKy1NamTruoc.MaHocKy).ToList();
                            var diemHK2 = allDiem.Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKy2NamTruoc.MaHocKy).ToList();
                            var hanhKiemHK1 = allHanhKiem.FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKy1NamTruoc.MaHocKy);
                            var hanhKiemHK2 = allHanhKiem.FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKy2NamTruoc.MaHocKy);

                            if (diemHK1 == null || diemHK1.Count == 0 || diemHK2 == null || diemHK2.Count == 0 ||
                                hanhKiemHK1 == null || hanhKiemHK2 == null)
                            {
                                soHSLoiDuLieu++;
                                continue;
                            }

                            // Tính ĐTB cả năm
                            double dtbHK1 = diemHK1.Average(d => d.DiemTrungBinh ?? 0);
                            double dtbHK2 = diemHK2.Average(d => d.DiemTrungBinh ?? 0);
                            double dtbCaNam = (dtbHK1 * 1 + dtbHK2 * 2) / 3.0;

                            // Xét hạnh kiểm
                            string[] thuTuHanhKiem = { "Yếu", "Trung Bình", "Khá", "Tốt" };
                            int indexHK1 = Array.IndexOf(thuTuHanhKiem, hanhKiemHK1.XepLoai);
                            int indexHK2 = Array.IndexOf(thuTuHanhKiem, hanhKiemHK2.XepLoai);
                            if (indexHK1 == -1) indexHK1 = 0;
                            if (indexHK2 == -1) indexHK2 = 0;
                            int indexMin = Math.Min(indexHK1, indexHK2);

                            // Đếm môn kém/yếu
                            var tatCaDiemCaNam = diemHK1.Concat(diemHK2).ToList();
                            var diemTheoMon = tatCaDiemCaNam.GroupBy(d => d.MaMonHoc)
                                .Select(g => new { MaMon = g.Key, DiemTBMon = g.Average(d => d.DiemTrungBinh ?? 0) })
                                .ToList();

                            int soMonKem = diemTheoMon.Count(m => m.DiemTBMon < 3.5);
                            int soMonYeu = diemTheoMon.Count(m => m.DiemTBMon >= 3.5 && m.DiemTBMon < 5.0);

                            // Kiểm tra điều kiện lên lớp
                            bool duDieuKienLenLop = dtbCaNam >= 5.0 && indexMin >= 1 && soMonKem == 0 && soMonYeu <= 2;

                            if (duDieuKienLenLop)
                            {
                                soHSLenLop++;
                            }
                            else
                            {
                                soHSOLai++;
                            }
                        }
                        catch
                        {
                            soHSLoiDuLieu++;
                        }
                    }

                    preview["TongSoHocSinh"] = hocSinhDangHocNamTruoc.Count;
                    preview["SoHSLenLop"] = soHSLenLop;
                    preview["SoHSOLai"] = soHSOLai;
                    preview["TyLeLenLop"] = (hocSinhDangHocNamTruoc.Count > 0) ?
                        ((double)soHSLenLop / hocSinhDangHocNamTruoc.Count * 100) : 0;
                }
                else if (kichBan == "FIRST_TIME")
                {
                    // KỊCH BẢN 3: Đếm số HS theo khối
                    var hocSinhCanPhanLop = danhSachHocSinhDangHoc.ToList();

                    // Xác định năm học
                    string[] partsNamHocPreview = hocKyCanPhanLop.MaNamHoc.Split('-');
                    if (partsNamHocPreview.Length != 2 || !int.TryParse(partsNamHocPreview[0], out int namHocBatDau))
                    {
                        preview["Loi"] = $"Không thể xác định năm học từ '{hocKyCanPhanLop.MaNamHoc}'";
                        return preview;
                    }

                    // Xác định năm sinh chuẩn cho từng khối
                    int namSinhKhoi10 = namHocBatDau - 15;
                    int namSinhKhoi11 = namHocBatDau - 16;
                    int namSinhKhoi12 = namHocBatDau - 17;

                    // ✅ TẤT CẢ học sinh đều vào khối 10 (không xét năm sinh)
                    var hocSinhTheoKhoi = new Dictionary<int, int>();
                    hocSinhTheoKhoi[10] = hocSinhCanPhanLop.Count;

                    // Nhóm học sinh theo chữ cái đầu tiên của tên
                    var hocSinhTheoChuCai = new Dictionary<char, int>();

                    foreach (var hs in hocSinhCanPhanLop)
                    {
                        // Lấy chữ cái đầu tiên của tên
                        char chuCaiDau = '?';
                        if (!string.IsNullOrWhiteSpace(hs.HoTen))
                        {
                            string tenTrimmed = hs.HoTen.Trim();
                            if (tenTrimmed.Length > 0)
                            {
                                chuCaiDau = char.ToUpper(tenTrimmed[0]);
                                if (!char.IsLetter(chuCaiDau))
                                {
                                    chuCaiDau = '?';
                                }
                            }
                        }

                        if (!hocSinhTheoChuCai.ContainsKey(chuCaiDau))
                            hocSinhTheoChuCai[chuCaiDau] = 0;

                        hocSinhTheoChuCai[chuCaiDau]++;
                    }

                    preview["TongSoHocSinh"] = hocSinhCanPhanLop.Count;
                    preview["HocSinhTheoKhoi"] = hocSinhTheoKhoi;
                    preview["HocSinhTheoChuCai"] = hocSinhTheoChuCai;
                    preview["PhuongPhapPhanLop"] = "Phân lớp theo chữ cái đầu tiên của tên, phân đều vào các lớp khối 10";
                }

                if (soHSLoiDuLieu > 0)
                {
                    preview["SoHSGapLoi"] = soHSLoiDuLieu;
                }

                return preview;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi TaoPreviewPhanLop: {ex.Message}");
                preview["Loi"] = ex.Message;
                return preview;
            }
        }

        #endregion
    }
}