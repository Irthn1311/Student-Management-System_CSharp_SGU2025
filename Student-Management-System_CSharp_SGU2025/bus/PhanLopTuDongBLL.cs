using Student_Management_System_CSharp_SGU2025.DAO; // Giả sử bạn có DAO ở đây
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI;
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

                string tenHkLower = hocKyCanPhanLop.TenHocKy.ToLower();
                bool isHK1 = (tenHkLower.Contains("i") && !tenHkLower.Contains("ii")) ||
                             (tenHkLower.Contains("1") && !tenHkLower.Contains("2"));

                Console.WriteLine($"=== BẮT ĐẦU PHÂN LỚP CHO {hocKyCanPhanLop.TenHocKy} - {hocKyCanPhanLop.MaNamHoc} ===");

                // 2. XÁC ĐỊNH KỊCH BẢN & TÌM HỌC KỲ NGUỒN
                string kichBan = "";
                HocKyDTO hocKyNguon = null; // Học kỳ nguồn để lấy dữ liệu

                if (isHK1) // Phân lớp cho HK1
                {
                    // Tìm HK2 của năm học TRƯỚC ĐÓ để xét lên lớp
                    string[] parts = hocKyCanPhanLop.MaNamHoc.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                    {
                        string maNamHocTruoc = $"{namBatDau - 1}-{namBatDau}";
                        var dsHocKyNamTruoc = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocTruoc);
                        hocKyNguon = dsHocKyNamTruoc?.FirstOrDefault(hk =>
                            hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));
                    }

                    if (hocKyNguon != null)
                    {
                        kichBan = "HK2_NAM_TRUOC_TO_HK1"; // Xét lên lớp từ HK2 năm trước
                        Console.WriteLine($"📌 Kịch bản: HK2 {hocKyNguon.MaNamHoc} → HK1 {hocKyCanPhanLop.MaNamHoc} (Xét lên lớp)");
                    }
                    else
                    {
                        kichBan = "FIRST_TIME"; // Phân lớp lần đầu (theo năm sinh)
                        Console.WriteLine($"📌 Kịch bản: Phân lớp lần đầu cho HK1 {hocKyCanPhanLop.MaNamHoc}");
                        // Không return nữa, sẽ xử lý ở dưới
                    }
                }
                else // Phân lớp cho HK2
                {
                    // Tìm HK1 cùng năm học
                    var dsHocKyCungNam = hocKyBUS.LayDanhSachHocKyTheoNamHoc(hocKyCanPhanLop.MaNamHoc);
                    hocKyNguon = dsHocKyCungNam?.FirstOrDefault(hk =>
                        (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                        (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));

                    if (hocKyNguon != null)
                    {
                        kichBan = "HK1_TO_HK2"; // Copy từ HK1 sang HK2
                        Console.WriteLine($"📌 Kịch bản: HK1 → HK2 cùng năm {hocKyCanPhanLop.MaNamHoc} (Giữ nguyên lớp)");
                    }
                    else
                    {
                        return (false, $"Không tìm thấy HK1 của năm học {hocKyCanPhanLop.MaNamHoc}. Cần phân lớp HK1 trước!", 0);
                    }
                }

                // 3. LẤY DỮ LIỆU CẦN THIẾT
                // Lấy học sinh "Đang học" HOẶC "Nghỉ học" (cho phép phân lớp)
                List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                    .Where(hs => hs.TrangThai == "Đang học" || hs.TrangThai == "Nghỉ học")
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

                if (kichBan == "HK1_TO_HK2")
                {
                    // =================================================================
                    // KỊCH BẢN 1: HK1 → HK2 (COPY VỚI KIỂM TRA ĐỦ DỮ LIỆU)
                    // =================================================================
                    Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║   KỊCH BẢN 1: HK1 → HK2 (Giữ nguyên lớp)                ║");
                    Console.WriteLine("╚══════════════════════════════════════════════════════════╝");

                    // ✅ SỬA: Lấy TẤT CẢ học sinh "Đang học" (không cần kiểm tra đã phân lớp HK1)
                    // Vì có thể HK1 đã bị xóa trước đó
                    var hocSinhDangHocHK1 = danhSachHocSinhDangHoc.ToList();

                    Console.WriteLine($"→ Tìm thấy {hocSinhDangHocHK1.Count} học sinh 'Đang học' cần kiểm tra");

                    foreach (var hs in hocSinhDangHocHK1)
                    {
                        try
                        {
                            // BƯỚC 1: Kiểm tra đã có điểm chưa
                            var diemHK1 = allDiem
                                .Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKyNguon.MaHocKy)
                                .ToList();

                            if (diemHK1 == null || diemHK1.Count == 0)
                            {
                                string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Chưa có điểm HK1";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // BƯỚC 2: Kiểm tra đã có hạnh kiểm chưa
                            var hanhKiemHK1 = allHanhKiem
                                .FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKyNguon.MaHocKy);

                            if (hanhKiemHK1 == null || string.IsNullOrEmpty(hanhKiemHK1.XepLoai))
                            {
                                string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Chưa có hạnh kiểm HK1";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // BƯỚC 3: Kiểm tra đã có xếp loại chưa
                            var xepLoaiHK1 = allXepLoai
                                .FirstOrDefault(xl => xl.MaHocSinh == hs.MaHS && xl.MaHocKy == hocKyNguon.MaHocKy);

                            if (xepLoaiHK1 == null || string.IsNullOrEmpty(xepLoaiHK1.HocLuc))
                            {
                                string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Chưa có xếp loại HK1";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // BƯỚC 4: ĐỦ DỮ LIỆU → COPY SANG HK2
                            var phanLopHK1 = allPhanLopHist
                                .FirstOrDefault(p => p.maHocSinh == hs.MaHS && p.maHocKy == hocKyNguon.MaHocKy);

                            if (phanLopHK1.maHocSinh == 0) // Tuple default
                            {
                                string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Không tìm thấy thông tin phân lớp HK1";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            int maLopHK1 = phanLopHK1.maLop;
                            var lopHK1 = allLop.FirstOrDefault(l => l.MaLop == maLopHK1);

                            if (lopHK1 == null)
                            {
                                string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Không tìm thấy lớp HK1 (ID: {maLopHK1})";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // Thêm vào HK2 với CÙNG LỚP
                            bool themThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, maLopHK1, maHocKyCanPhanLop);

                            if (themThanhCong)
                            {
                                soHocSinhDaPhanLop++;
                                Console.WriteLine($"  ✓ {hs.HoTen} → Lớp {lopHK1.TenLop} (HK2)");
                            }
                            else
                            {
                                string loi = $"HS {hs.HoTen}: Lỗi khi thêm vào lớp {lopHK1.TenLop} HK2";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                            }
                        }
                        catch (Exception ex)
                        {
                            string loi = $"HS {hs.HoTen}: Lỗi xử lý - {ex.Message}";
                            Console.WriteLine($"  ❌ {loi}");
                            danhSachLoi.Add(loi);
                        }
                    }
                }
                else if (kichBan == "HK2_NAM_TRUOC_TO_HK1")
                {
                    // =================================================================
                    // KỊCH BẢN 2: HK2 NĂM TRƯỚC → HK1 NĂM SAU (XÉT LÊN LỚP)
                    // =================================================================
                    Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║   KỊCH BẢN 2: HK2 năm trước → HK1 năm sau (Xét lên lớp)║");
                    Console.WriteLine("╚══════════════════════════════════════════════════════════╝");

                    // Tìm HK1 của năm học CÙNG VỚI HK2 nguồn
                    var dsHocKyCungNamVoiHK2 = hocKyBUS.LayDanhSachHocKyTheoNamHoc(hocKyNguon.MaNamHoc);
                    HocKyDTO hocKy1NamTruoc = dsHocKyCungNamVoiHK2?.FirstOrDefault(hk =>
                        (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                        (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));

                    if (hocKy1NamTruoc == null)
                    {
                        return (false, $"Không tìm thấy HK1 của năm học {hocKyNguon.MaNamHoc} để xét lên lớp!", 0);
                    }

                    Console.WriteLine($"→ HK1 năm trước: {hocKy1NamTruoc.TenHocKy} {hocKy1NamTruoc.MaNamHoc}");
                    Console.WriteLine($"→ HK2 năm trước: {hocKyNguon.TenHocKy} {hocKyNguon.MaNamHoc}");

                    // ✅ SỬA: Lấy TẤT CẢ học sinh "Đang học" (không cần kiểm tra đã phân lớp HK2)
                    // Vì có thể HK2 năm trước đã bị xóa
                    var hocSinhDangHocHK2NamTruoc = danhSachHocSinhDangHoc.ToList();

                    Console.WriteLine($"→ Tìm thấy {hocSinhDangHocHK2NamTruoc.Count} học sinh 'Đang học' cần kiểm tra");

                    foreach (var hs in hocSinhDangHocHK2NamTruoc)
                    {
                        try
                        {
                            // BƯỚC 1: Lấy điểm HK1 và HK2 năm trước
                            var diemHK1 = allDiem
                                .Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKy1NamTruoc.MaHocKy)
                                .ToList();

                            var diemHK2 = allDiem
                                .Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKyNguon.MaHocKy)
                                .ToList();

                            if (diemHK1 == null || diemHK1.Count == 0)
                            {
                                string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Chưa có điểm HK1 năm trước";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            if (diemHK2 == null || diemHK2.Count == 0)
                            {
                                string loi = $"HS {hs.HoTen} (ID: {hs.MaHS}): Chưa có điểm HK2 năm trước";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // BƯỚC 2: Lấy hạnh kiểm HK1 và HK2
                            var hanhKiemHK1 = allHanhKiem
                                .FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKy1NamTruoc.MaHocKy);

                            var hanhKiemHK2 = allHanhKiem
                                .FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKyNguon.MaHocKy);

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

                            // BƯỚC 7: LẤY LỚP CŨ VÀ XÁC ĐỊNH LỚP MỚI
                            var phanLopHK2NamTruoc = allPhanLopHist
                                .FirstOrDefault(p => p.maHocSinh == hs.MaHS && p.maHocKy == hocKyNguon.MaHocKy);

                            if (phanLopHK2NamTruoc.maHocSinh == 0) // Tuple default có maHocSinh = 0
                            {
                                string loi = $"HS {hs.HoTen}: Không tìm thấy lớp HK2 năm trước";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            var lopCu = allLop.FirstOrDefault(l => l.MaLop == phanLopHK2NamTruoc.maLop);
                            if (lopCu == null)
                            {
                                string loi = $"HS {hs.HoTen}: Không tìm thấy thông tin lớp cũ (ID: {phanLopHK2NamTruoc.maLop})";
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

                            // BƯỚC 8: TÌM LỚP CÓ CHỖ TRỐNG Ở KHỐI MỚI (HK1 NĂM SAU)
                            var dsLopKhoiMoi = allLop.Where(l => l.MaKhoi == khoiMoi).ToList();

                            if (dsLopKhoiMoi.Count == 0)
                            {
                                string loi = $"HS {hs.HoTen}: Không có lớp nào ở Khối {khoiMoi}";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                                continue;
                            }

                            // Đếm số học sinh trong từng lớp (trong HK1 năm mới)
                            var soLuongHocSinhTrongLop = allPhanLopHist
                                .Where(p => p.maHocKy == maHocKyCanPhanLop)
                                .GroupBy(p => p.maLop)
                                .ToDictionary(g => g.Key, g => g.Count());

                            // Thêm số lượng tạm của học sinh vừa phân
                            var phanLopTam = danhSachPhanLopTam
                                .Where(p => p.maHocKy == maHocKyCanPhanLop)
                                .GroupBy(p => p.maLop)
                                .ToDictionary(g => g.Key, g => g.Count());

                            foreach (var kvp in phanLopTam)
                            {
                                if (soLuongHocSinhTrongLop.ContainsKey(kvp.Key))
                                    soLuongHocSinhTrongLop[kvp.Key] += kvp.Value;
                                else
                                    soLuongHocSinhTrongLop[kvp.Key] = kvp.Value;
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

                            // BƯỚC 9: THÊM VÀO LỚP MỚI
                            bool themThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, maHocKyCanPhanLop);

                            if (themThanhCong)
                            {
                                soHocSinhDaPhanLop++;

                                // Thêm vào danh sách tạm để cập nhật số lượng
                                danhSachPhanLopTam.Add((hs.MaHS, lopPhuHop.MaLop, maHocKyCanPhanLop));

                                Console.WriteLine($"  ✓ {hs.HoTen} → Lớp {lopPhuHop.TenLop} (Khối {khoiMoi})");
                            }
                            else
                            {
                                string loi = $"HS {hs.HoTen}: Lỗi khi thêm vào lớp {lopPhuHop.TenLop}";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                            }
                        }
                        catch (Exception ex)
                        {
                            string loi = $"HS {hs.HoTen}: Lỗi xử lý - {ex.Message}";
                            Console.WriteLine($"  ❌ {loi}");
                            danhSachLoi.Add(loi);
                        }
                    }
                }
                else if (kichBan == "FIRST_TIME")
                {
                    // =================================================================
                    // KỊCH BẢN 3: PHÂN LỚP LẦN ĐẦU (PHÂN ĐỀU VÀO CÁC LỚP)
                    // =================================================================
                    Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║   KỊCH BẢN 3: FIRST_TIME (Phân lớp lần đầu)                ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

                    // Lấy TẤT CẢ học sinh "Đang học"
                    var hocSinhCanPhanLop = danhSachHocSinhDangHoc.ToList();

                    Console.WriteLine($"→ Tìm thấy {hocSinhCanPhanLop.Count} học sinh 'Đang học' cần phân lớp");

                    // Xác định khối của học kỳ này (từ tên năm học)
                    // VD: Năm 2025-2026 → Học sinh sinh năm 2010 → Khối 10
                    string[] parts = hocKyCanPhanLop.MaNamHoc.Split('-');
                    if (parts.Length != 2 || !int.TryParse(parts[0], out int namHocBatDau))
                    {
                        return (false, $"Không thể xác định năm học từ '{hocKyCanPhanLop.MaNamHoc}'", 0);
                    }

                    // Xác định năm sinh chuẩn cho từng khối
                    // VD: Năm học 2025-2026 → Khối 10 sinh năm 2010, Khối 11 sinh 2009, Khối 12 sinh 2008
                    int namSinhKhoi10 = namHocBatDau - 15; // Khối 10 khoảng 15 tuổi
                    int namSinhKhoi11 = namHocBatDau - 16; // Khối 11 khoảng 16 tuổi
                    int namSinhKhoi12 = namHocBatDau - 17; // Khối 12 khoảng 17 tuổi

                    Console.WriteLine($"→ Năm sinh chuẩn: Khối 10={namSinhKhoi10}, Khối 11={namSinhKhoi11}, Khối 12={namSinhKhoi12}");

                    // Nhóm học sinh theo năm sinh để xác định khối
                    var hocSinhTheoKhoi = new Dictionary<int, List<HocSinhDTO>>();
                    var hocSinhKhongXacDinhDuocKhoi = new List<HocSinhDTO>();

                    foreach (var hs in hocSinhCanPhanLop)
                    {
                        try
                        {
                            int namSinh = hs.NgaySinh.Year;
                            int khoi = 0;

                            // ✅ SỬA: Xác định khối dựa vào khoảng năm sinh (cho phép sai lệch ±2 năm)
                            // Điều này bao quát cả trường hợp học sinh nhảy lớp hoặc học lại 1-2 năm
                            if (Math.Abs(namSinh - namSinhKhoi10) <= 2)
                            {
                                khoi = 10;
                            }
                            else if (Math.Abs(namSinh - namSinhKhoi11) <= 2)
                            {
                                khoi = 11;
                            }
                            else if (Math.Abs(namSinh - namSinhKhoi12) <= 2)
                            {
                                khoi = 12;
                            }
                            else
                            {
                                // Không xác định được khối → Bỏ qua hoặc gán vào khối mặc định
                                string loi = $"HS {hs.HoTen} (sinh {namSinh}): Năm sinh không phù hợp với THPT (cần sinh từ {namSinhKhoi12 - 2} đến {namSinhKhoi10 + 2})";
                                Console.WriteLine($"  ⚠️ {loi}");
                                danhSachLoi.Add(loi);
                                hocSinhKhongXacDinhDuocKhoi.Add(hs);
                                continue;
                            }

                            if (!hocSinhTheoKhoi.ContainsKey(khoi))
                            {
                                hocSinhTheoKhoi[khoi] = new List<HocSinhDTO>();
                            }
                            hocSinhTheoKhoi[khoi].Add(hs);

                            int tuoi = namHocBatDau - namSinh;
                            Console.WriteLine($"  → {hs.HoTen} (sinh {namSinh}, {tuoi} tuổi) → Khối {khoi}");
                        }
                        catch (Exception ex)
                        {
                            string loi = $"HS {hs.HoTen}: Lỗi xác định khối - {ex.Message}";
                            Console.WriteLine($"  ❌ {loi}");
                            danhSachLoi.Add(loi);
                        }
                    }

                    // Thông báo nếu có học sinh không xác định được khối
                    if (hocSinhKhongXacDinhDuocKhoi.Count > 0)
                    {
                        Console.WriteLine($"\n⚠️ Có {hocSinhKhongXacDinhDuocKhoi.Count} học sinh không xác định được khối (năm sinh không phù hợp)");
                    }

                    // Phân bổ học sinh vào từng lớp của mỗi khối
                    foreach (var kvp in hocSinhTheoKhoi)
                    {
                        int khoi = kvp.Key;
                        List<HocSinhDTO> dsHS = kvp.Value;

                        Console.WriteLine($"\n→ Xử lý Khối {khoi}: {dsHS.Count} học sinh");

                        // Lấy danh sách lớp của khối này - SẮP XẾP THEO MÃ LỚP (không phải tên)
                        var dsLopKhoi = allLop
                            .Where(l => l.MaKhoi == khoi)
                            .OrderBy(l => l.MaLop)  // ✅ Sắp xếp theo MaLop thay vì TenLop
                            .ToList();

                        if (dsLopKhoi.Count == 0)
                        {
                            string loi = $"Không có lớp nào cho Khối {khoi}";
                            Console.WriteLine($"  ❌ {loi}");
                            foreach (var hs in dsHS)
                            {
                                danhSachLoi.Add($"HS {hs.HoTen}: {loi}");
                            }
                            continue;
                        }

                        Console.WriteLine($"  → Số lớp khả dụng: {dsLopKhoi.Count} lớp ({string.Join(", ", dsLopKhoi.Select(l => l.TenLop))})");

                        // Đếm số học sinh đã có trong từng lớp (bao gồm cả tạm)
                        var soLuongHocSinhTrongLop = new Dictionary<int, int>();

                        // Đếm từ database
                        foreach (var lop in dsLopKhoi)
                        {
                            int soHS = allPhanLopHist.Count(p => p.maLop == lop.MaLop && p.maHocKy == maHocKyCanPhanLop);
                            soLuongHocSinhTrongLop[lop.MaLop] = soHS;
                        }

                        // Cộng thêm số tạm
                        foreach (var phanLopTam in danhSachPhanLopTam)
                        {
                            if (phanLopTam.maHocKy == maHocKyCanPhanLop)
                            {
                                if (soLuongHocSinhTrongLop.ContainsKey(phanLopTam.maLop))
                                    soLuongHocSinhTrongLop[phanLopTam.maLop]++;
                                else
                                    soLuongHocSinhTrongLop[phanLopTam.maLop] = 1;
                            }
                        }

                        // ✅ Phân đều học sinh vào các lớp theo Round-Robin (không cần sắp xếp theo tên)
                        Console.WriteLine($"  → Bắt đầu phân bổ {dsHS.Count} học sinh vào {dsLopKhoi.Count} lớp...");

                        // ✅ Phân đều học sinh vào các lớp theo Round-Robin
                        int lopIndex = 0; // Index để phân vòng tròn
                        foreach (var hs in dsHS)
                        {
                            try
                            {
                                // ✅ Lấy lớp theo thứ tự vòng tròn (10A1 → 10A2 → ... → 10A8 → lại 10A1)
                                var lopPhuHop = dsLopKhoi[lopIndex % dsLopKhoi.Count];

                                // Thêm vào lớp
                                bool themThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, maHocKyCanPhanLop);

                                if (themThanhCong)
                                {
                                    soHocSinhDaPhanLop++;
                                    danhSachPhanLopTam.Add((hs.MaHS, lopPhuHop.MaLop, maHocKyCanPhanLop));

                                    // Cập nhật số lượng
                                    if (soLuongHocSinhTrongLop.ContainsKey(lopPhuHop.MaLop))
                                        soLuongHocSinhTrongLop[lopPhuHop.MaLop]++;
                                    else
                                        soLuongHocSinhTrongLop[lopPhuHop.MaLop] = 1;

                                    Console.WriteLine($"  ✓ {hs.HoTen} → Lớp {lopPhuHop.TenLop} (Sĩ số: {soLuongHocSinhTrongLop[lopPhuHop.MaLop]})");

                                    // ✅ Chuyển sang lớp tiếp theo
                                    lopIndex++;
                                }
                                else
                                {
                                    string loi = $"HS {hs.HoTen}: Lỗi khi thêm vào lớp {lopPhuHop.TenLop}";
                                    Console.WriteLine($"  ❌ {loi}");
                                    danhSachLoi.Add(loi);
                                }
                            }
                            catch (Exception ex)
                            {
                                string loi = $"HS {hs.HoTen}: Lỗi xử lý - {ex.Message}";
                                Console.WriteLine($"  ❌ {loi}");
                                danhSachLoi.Add(loi);
                            }
                        }

                        // Hiển thị kết quả phân lớp cho khối này
                        Console.WriteLine($"\n  → Kết quả phân lớp Khối {khoi}:");
                        foreach (var lop in dsLopKhoi)
                        {
                            int siSo = soLuongHocSinhTrongLop.ContainsKey(lop.MaLop) ? soLuongHocSinhTrongLop[lop.MaLop] : 0;
                            Console.WriteLine($"     • {lop.TenLop}: {siSo} học sinh");
                        }
                    }
                }

                // 5. KẾT QUẢ
                Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                   KẾT QUẢ PHÂN LỚP                      ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
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
                thongBao += $"║        KẾT QUẢ PHÂN LỚP TỰ ĐỘNG               ║\n";
                thongBao += $"╚════════════════════════════════════════════════╝\n\n";

                // Thông tin học kỳ
                thongBao += $"📅 Học kỳ: {hocKyCanPhanLop.TenHocKy} - {hocKyCanPhanLop.MaNamHoc}\n\n";

                // Kịch bản
                if (kichBan == "HK1_TO_HK2")
                {
                    thongBao += $"📋 Kịch bản: HK1 → HK2 (Giữ nguyên lớp)\n";
                    thongBao += $"   Nguồn: {hocKyNguon.TenHocKy} {hocKyNguon.MaNamHoc}\n\n";
                }
                else if (kichBan == "HK2_NAM_TRUOC_TO_HK1")
                {
                    thongBao += $"📋 Kịch bản: HK2 năm trước → HK1 năm sau (Xét lên lớp)\n";
                    thongBao += $"   Nguồn: {hocKyNguon.TenHocKy} {hocKyNguon.MaNamHoc}\n\n";
                }
                else if (kichBan == "FIRST_TIME")
                {
                    thongBao += $"📋 Kịch bản: Phân lớp lần đầu (Dựa vào năm sinh)\n";
                    thongBao += $"   Phân đều học sinh vào các lớp theo khối\n\n";
                }

                // Kết quả phân lớp
                thongBao += $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
                thongBao += $"✅ THÀNH CÔNG: {soHocSinhDaPhanLop} học sinh\n";

                // Thống kê theo kịch bản
                if (kichBan == "HK2_NAM_TRUOC_TO_HK1")
                {
                    // Đếm số học sinh lên lớp / ở lại
                    int soHSLenLop = 0;
                    int soHSOLai = 0;

                    foreach (var hs in danhSachHocSinhDangHoc)
                    {
                        var phanLopMoi = danhSachPhanLopTam.FirstOrDefault(p => p.maHocSinh == hs.MaHS);
                        if (phanLopMoi.maHocSinh != 0) // Đã phân lớp
                        {
                            var phanLopCu = allPhanLopHist.FirstOrDefault(p => p.maHocSinh == hs.MaHS && p.maHocKy == hocKyNguon.MaHocKy);
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

                string tenHkLower = hocKyCanPhanLop.TenHocKy.ToLower();
                bool isHK1 = (tenHkLower.Contains("i") && !tenHkLower.Contains("ii")) ||
                             (tenHkLower.Contains("1") && !tenHkLower.Contains("2"));

                // 2. XÁC ĐỊNH KỊCH BẢN & TÌM HỌC KỲ NGUỒN
                string kichBan = "";
                HocKyDTO hocKyNguon = null;

                if (isHK1)
                {
                    // Tìm HK2 năm trước
                    string[] parts = hocKyCanPhanLop.MaNamHoc.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int namBatDau))
                    {
                        string maNamHocTruoc = $"{namBatDau - 1}-{namBatDau}";
                        var dsHocKyNamTruoc = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocTruoc);
                        hocKyNguon = dsHocKyNamTruoc?.FirstOrDefault(hk =>
                            hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));
                    }

                    if (hocKyNguon != null)
                    {
                        kichBan = "HK2_NAM_TRUOC_TO_HK1";
                        preview["LoaiPhanLop"] = $"HK2 năm trước → HK1 năm sau (Xét lên lớp)";
                        preview["HocKyNguon"] = $"{hocKyNguon.TenHocKy} {hocKyNguon.MaNamHoc}";
                    }
                    else
                    {
                        kichBan = "FIRST_TIME";
                        preview["LoaiPhanLop"] = "Phân lớp lần đầu (Dựa vào năm sinh)";
                        preview["HocKyNguon"] = "Không có (Phân lớp mới)";
                    }
                }
                else // HK2
                {
                    // Tìm HK1 cùng năm
                    var dsHocKyCungNam = hocKyBUS.LayDanhSachHocKyTheoNamHoc(hocKyCanPhanLop.MaNamHoc);
                    hocKyNguon = dsHocKyCungNam?.FirstOrDefault(hk =>
                        (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                        (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));

                    if (hocKyNguon != null)
                    {
                        kichBan = "HK1_TO_HK2";
                        preview["LoaiPhanLop"] = $"HK1 → HK2 cùng năm (Giữ nguyên lớp)";
                        preview["HocKyNguon"] = $"{hocKyNguon.TenHocKy} {hocKyNguon.MaNamHoc}";
                    }
                    else
                    {
                        preview["Loi"] = $"Không tìm thấy HK1 của năm học {hocKyCanPhanLop.MaNamHoc}";
                        return preview;
                    }
                }

                // 3. LẤY DỮ LIỆU
                // Lấy học sinh "Đang học" HOẶC "Nghỉ học" (cho phép phân lớp)
                List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                    .Where(hs => hs.TrangThai == "Đang học" || hs.TrangThai == "Nghỉ học")
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

                if (kichBan == "HK1_TO_HK2")
                {
                    // KỊCH BẢN 1: Đếm số HS đủ dữ liệu trong HK1
                    // ✅ SỬA: Lấy TẤT CẢ học sinh "Đang học" (không cần kiểm tra đã phân lớp HK1)
                    var hocSinhDangHocHK1 = danhSachHocSinhDangHoc.ToList();

                    foreach (var hs in hocSinhDangHocHK1)
                    {
                        try
                        {
                            // Kiểm tra đủ dữ liệu: Điểm, Hạnh kiểm, Xếp loại
                            var diemHK1 = allDiem.Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKyNguon.MaHocKy).ToList();
                            var hanhKiemHK1 = allHanhKiem.FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKyNguon.MaHocKy);
                            var xepLoaiHK1 = xepLoaiBUS.GetXepLoaiByStudent(hs.MaHS, hocKyNguon.MaHocKy);

                            if (diemHK1 != null && diemHK1.Count > 0 &&
                                hanhKiemHK1 != null && !string.IsNullOrEmpty(hanhKiemHK1.XepLoai) &&
                                xepLoaiHK1 != null && !string.IsNullOrEmpty(xepLoaiHK1.HocLuc))
                            {
                                soHSDuDieuKien++;
                            }
                            else
                            {
                                soHSKhongDuDieuKien++;
                            }
                        }
                        catch
                        {
                            soHSLoiDuLieu++;
                        }
                    }

                    preview["TongSoHocSinh"] = hocSinhDangHocHK1.Count;
                    preview["SoHSDuDieuKien"] = soHSDuDieuKien;
                    preview["SoHSKhongDuDieuKien"] = soHSKhongDuDieuKien;
                }
                else if (kichBan == "HK2_NAM_TRUOC_TO_HK1")
                {
                    // KỊCH BẢN 2: Đếm số HS lên lớp / ở lại
                    // Tìm HK1 năm trước
                    var dsHocKyCungNamVoiHK2 = hocKyBUS.LayDanhSachHocKyTheoNamHoc(hocKyNguon.MaNamHoc);
                    HocKyDTO hocKy1NamTruoc = dsHocKyCungNamVoiHK2?.FirstOrDefault(hk =>
                        (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                        (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));

                    if (hocKy1NamTruoc == null)
                    {
                        preview["Loi"] = "Không tìm thấy HK1 năm trước để xét lên lớp";
                        return preview;
                    }

                    // ✅ SỬA: Lấy TẤT CẢ học sinh "Đang học" (không cần kiểm tra đã phân lớp HK2)
                    var hocSinhDangHocHK2NamTruoc = danhSachHocSinhDangHoc.ToList();

                    foreach (var hs in hocSinhDangHocHK2NamTruoc)
                    {
                        try
                        {
                            // Lấy điểm HK1 và HK2
                            var diemHK1 = allDiem.Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKy1NamTruoc.MaHocKy).ToList();
                            var diemHK2 = allDiem.Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKyNguon.MaHocKy).ToList();
                            var hanhKiemHK1 = allHanhKiem.FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKy1NamTruoc.MaHocKy);
                            var hanhKiemHK2 = allHanhKiem.FirstOrDefault(hk => hk.MaHocSinh == hs.MaHS && hk.MaHocKy == hocKyNguon.MaHocKy);

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

                    preview["TongSoHocSinh"] = hocSinhDangHocHK2NamTruoc.Count;
                    preview["SoHSLenLop"] = soHSLenLop;
                    preview["SoHSOLai"] = soHSOLai;
                    preview["TyLeLenLop"] = (hocSinhDangHocHK2NamTruoc.Count > 0) ?
                        ((double)soHSLenLop / hocSinhDangHocHK2NamTruoc.Count * 100) : 0;
                }
                else if (kichBan == "FIRST_TIME")
                {
                    // KỊCH BẢN 3: Đếm số HS theo khối
                    var hocSinhCanPhanLop = danhSachHocSinhDangHoc.ToList();

                    // Xác định năm học
                    string[] parts = hocKyCanPhanLop.MaNamHoc.Split('-');
                    if (parts.Length != 2 || !int.TryParse(parts[0], out int namHocBatDau))
                    {
                        preview["Loi"] = $"Không thể xác định năm học từ '{hocKyCanPhanLop.MaNamHoc}'";
                        return preview;
                    }

                    // Xác định năm sinh chuẩn cho từng khối
                    int namSinhKhoi10 = namHocBatDau - 15;
                    int namSinhKhoi11 = namHocBatDau - 16;
                    int namSinhKhoi12 = namHocBatDau - 17;

                    // Nhóm học sinh theo khối
                    var hocSinhTheoKhoi = new Dictionary<int, int>();
                    int soHSKhongPhuHop = 0;

                    foreach (var hs in hocSinhCanPhanLop)
                    {
                        try
                        {
                            int namSinh = hs.NgaySinh.Year;
                            int khoi = 0;

                            // ✅ SỬA: Xác định khối với sai lệch ±2 năm (giống logic chính)
                            if (Math.Abs(namSinh - namSinhKhoi10) <= 2)
                            {
                                khoi = 10;
                            }
                            else if (Math.Abs(namSinh - namSinhKhoi11) <= 2)
                            {
                                khoi = 11;
                            }
                            else if (Math.Abs(namSinh - namSinhKhoi12) <= 2)
                            {
                                khoi = 12;
                            }
                            else
                            {
                                // Năm sinh không phù hợp
                                soHSKhongPhuHop++;
                                continue;
                            }

                            if (!hocSinhTheoKhoi.ContainsKey(khoi))
                                hocSinhTheoKhoi[khoi] = 0;

                            hocSinhTheoKhoi[khoi]++;
                        }
                        catch
                        {
                            soHSLoiDuLieu++;
                        }
                    }

                    preview["TongSoHocSinh"] = hocSinhCanPhanLop.Count;
                    preview["HocSinhTheoKhoi"] = hocSinhTheoKhoi;
                    if (soHSKhongPhuHop > 0)
                    {
                        preview["SoHSKhongPhuHop"] = soHSKhongPhuHop;
                        preview["CanhBao"] = $"Có {soHSKhongPhuHop} học sinh có năm sinh không phù hợp với THPT";
                    }
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