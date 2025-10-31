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
        private HocSinhBLL hocSinhBLL;
        private HocKyBUS hocKyBUS;
        private LopHocBUS lopHocBUS;
        // Thêm các BLL cần thiết
        private NhapDiemBUS diemSoBUS;     // Giả định bạn có BLL này
        private HanhKiemBUS hanhKiemBUS; // Giả định bạn có BLL này
        private XepLoaiBUS xepLoaiBUS;   // Giả định bạn có BLL này

        public PhanLopTuDongBLL()
        {
            phanLopBLL = new PhanLopBLL();
            hocSinhBLL = new HocSinhBLL();
            hocKyBUS = new HocKyBUS();
            lopHocBUS = new LopHocBUS();
            // Khởi tạo các BLL mới
            diemSoBUS = new NhapDiemBUS();         // Thay DiemSoDAO() bằng BUS tương ứng nếu có
            hanhKiemBUS = new HanhKiemBUS();     // Thay HanhKiemDAO() bằng BUS tương ứng nếu có
            xepLoaiBUS = new XepLoaiBUS();       // Đã có XepLoaiBUS
        }

        #region Kiểm tra điều kiện & Tạo học kỳ mới (Giữ nguyên)

        public (bool success, string message) KiemTraDieuKienPhanLop(int maHocKy)
        {
            try
            {
                HocKyDTO hocKyHienTai = hocKyBUS.LayHocKyTheoMa(maHocKy);
                if (hocKyHienTai == null) return (false, "Không tìm thấy thông tin học kỳ");

                HocKyDTO hocKyTiepTheo = TimHocKyTiepTheo(hocKyHienTai);

                if (hocKyTiepTheo == null) return (false, "Chưa có học kỳ tiếp theo trong hệ thống");
                if (hocKyTiepTheo.TrangThai != "Chưa bắt đầu") return (false, $"Học kỳ tiếp theo phải ở trạng thái 'Chưa bắt đầu' (hiện tại: {hocKyTiepTheo.TrangThai})");

                return (true, "Đủ điều kiện phân lớp");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi kiểm tra điều kiện: {ex.Message}");
            }
        }

        private (bool success, int maHocKyMoi, string message) TaoHocKyMoi(int maHocKyHienTai)
        {
            try
            {
                HocKyDTO hocKyHienTai = hocKyBUS.LayHocKyTheoMa(maHocKyHienTai);
                if (hocKyHienTai == null)
                    return (false, 0, "Không tìm thấy học kỳ hiện tại");

                string[] parts = hocKyHienTai.MaNamHoc.Split('-');
                if (parts.Length != 2 || !int.TryParse(parts[0], out int namBatDau) || !int.TryParse(parts[1], out int namKetThuc))
                    return (false, 0, "Định dạng mã năm học không hợp lệ");

                string maNamHocMoi = $"{namKetThuc}-{namKetThuc + 1}";
                string tenNamHocMoi = $"Năm học {namKetThuc}-{namKetThuc + 1}";

                // Kiểm tra/Tạo Năm học mới
                var namHocMoi = hocKyBUS.LayNamHocTheoMa(maNamHocMoi); // Giả sử có hàm này
                if (namHocMoi == null)
                {
                    // Giả sử có hàm này trong HocKyBUS hoặc NamHocBUS
                    bool created = hocKyBUS.ThemNamHoc(maNamHocMoi, tenNamHocMoi, new DateTime(namKetThuc, 9, 1), new DateTime(namKetThuc + 1, 5, 31));
                    if (!created) return (false, 0, "Không thể tạo năm học mới");
                }


                List<HocKyDTO> dsHocKyMoi = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocMoi);
                HocKyDTO hocKy1Moi = dsHocKyMoi?.FirstOrDefault(hk => hk.TenHocKy.Contains("I") || hk.TenHocKy.Contains("1"));

                if (hocKy1Moi == null)
                {
                    // Tạo học kỳ 1 mới
                    HocKyDTO newHK1 = new HocKyDTO
                    {
                        TenHocKy = "Học kỳ I",
                        MaNamHoc = maNamHocMoi,
                        TrangThai = "Chưa bắt đầu",
                        NgayBD = new DateTime(namKetThuc, 9, 1),
                        NgayKT = new DateTime(namKetThuc + 1, 1, 15)
                    };
                    if (!hocKyBUS.ThemHocKy(newHK1)) return (false, 0, "Không thể tạo học kỳ I mới");

                    // Lấy lại mã học kỳ vừa tạo
                    dsHocKyMoi = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocMoi);
                    hocKy1Moi = dsHocKyMoi?.FirstOrDefault(hk => hk.TenHocKy.Contains("I") || hk.TenHocKy.Contains("1"));
                    if (hocKy1Moi == null) return (false, 0, "Không lấy được mã học kỳ I vừa tạo");

                    // Tạo luôn học kỳ 2
                    HocKyDTO newHK2 = new HocKyDTO
                    {
                        TenHocKy = "Học kỳ II",
                        MaNamHoc = maNamHocMoi,
                        TrangThai = "Chưa bắt đầu",
                        NgayBD = new DateTime(namKetThuc + 1, 1, 16),
                        NgayKT = new DateTime(namKetThuc + 1, 5, 31) // Sửa ngày KT
                    };
                    hocKyBUS.ThemHocKy(newHK2);
                }

                return (true, hocKy1Moi.MaHocKy, $"Tạo/Sử dụng năm học {tenNamHocMoi}, Học kỳ I (Mã: {hocKy1Moi.MaHocKy})");
            }
            catch (Exception ex)
            {
                return (false, 0, $"Lỗi tạo học kỳ mới: {ex.Message}");
            }
        }

        // Hàm helper tìm học kỳ tiếp theo
        // Hàm helper tìm học kỳ tiếp theo
        private HocKyDTO TimHocKyTiepTheo(HocKyDTO hocKyHienTai)
        {
            if (hocKyHienTai == null) return null;

            string tenHkLower = hocKyHienTai.TenHocKy.ToLower();

            // === SỬA LỖI LOGIC TẠI ĐÂY ===
            // Logic cũ: .Contains("i") -> Sai, vì "ii" cũng chứa "i".
            // Logic mới: (Chứa "i" VÀ KHÔNG chứa "ii") HOẶC (Chứa "1" VÀ KHÔNG chứa "2")
            bool isHK1 = (tenHkLower.Contains("i") && !tenHkLower.Contains("ii")) ||
                         (tenHkLower.Contains("1") && !tenHkLower.Contains("2"));
            // =============================

            if (isHK1)
            {
                // Tìm HK2 trong cùng năm học
                List<HocKyDTO> dsHocKy = hocKyBUS.LayDanhSachHocKyTheoNamHoc(hocKyHienTai.MaNamHoc);
                return dsHocKy?.FirstOrDefault(hk => hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));
            }
            else
            {
                // Tìm HK1 của năm học tiếp theo
                string[] parts = hocKyHienTai.MaNamHoc.Split('-');
                if (parts.Length != 2 || !int.TryParse(parts[1], out int namKetThuc)) return null;

                string maNamHocMoi = $"{namKetThuc}-{namKetThuc + 1}";
                List<HocKyDTO> dsHocKy = hocKyBUS.LayDanhSachHocKyTheoNamHoc(maNamHocMoi);
                return dsHocKy?.FirstOrDefault(hk =>
                    (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                    (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2"))
                );
            }
        }

        #endregion

        #region Phân lớp tự động (Core Logic)

        // Trong file PhanLopTuDongBLL.cs
        // HÃY THAY THẾ TOÀN BỘ HÀM NÀY

        // Trong file PhanLopTuDongBLL.cs
        // HÃY THAY THẾ TOÀN BỘ HÀM NÀY

        public (bool success, string message, int soHocSinhDaPhanLop) ThucHienPhanLopTuDong(int maHocKyHienTai, bool boQuaKiemTra = false)
        {
            try
            {
                // 1. KIỂM TRA ĐIỀU KIỆN VÀ HỌC KỲ
                if (!boQuaKiemTra)
                {
                    var kiemTra = KiemTraDieuKienPhanLop(maHocKyHienTai);
                    if (!kiemTra.success)
                    {
                        return (false, kiemTra.message, 0);
                    }
                }

                HocKyDTO hocKyHienTai = hocKyBUS.LayHocKyTheoMa(maHocKyHienTai);
                if (hocKyHienTai == null) return (false, "Không tìm thấy học kỳ hiện tại", 0);

                HocKyDTO hocKyTiepTheo = TimHocKyTiepTheo(hocKyHienTai);

                if (hocKyTiepTheo == null)
                {
                    string tenHkLower = hocKyHienTai.TenHocKy.ToLower();
                    bool isHK1 = (tenHkLower.Contains("i") && !tenHkLower.Contains("ii")) || (tenHkLower.Contains("1") && !tenHkLower.Contains("2"));

                    if (!isHK1) // Nếu là HK2
                    {
                        var taoHK = TaoHocKyMoi(maHocKyHienTai);
                        if (!taoHK.success) return (false, $"Lỗi khi tạo học kỳ mới: {taoHK.message}", 0);
                        hocKyTiepTheo = hocKyBUS.LayHocKyTheoMa(taoHK.maHocKyMoi);
                        if (hocKyTiepTheo == null) return (false, "Không thể lấy thông tin học kỳ vừa tạo", 0);
                        Console.WriteLine($"✓ {taoHK.message}");
                    }
                    else
                    {
                        return (false, "Không tìm thấy học kỳ II trong cùng năm học.", 0);
                    }
                }

                bool isChuyenSangHK2 = hocKyTiepTheo.TenHocKy.ToLower().Contains("ii") || hocKyTiepTheo.TenHocKy.ToLower().Contains("2");

                // 2. LẤY DỮ LIỆU CẦN THIẾT
                List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                                                            .Where(hs => hs.TrangThai == "Đang học")
                                                            .ToList();
                List<(int maHocSinh, int maLop, int maHocKy)> allPhanLopHist = phanLopBLL.GetAllPhanLop();
                var lopTheoKhoi = lopHocBUS.DocDSLop().GroupBy(l => l.MaKhoi).ToDictionary(g => g.Key, g => g.ToList());
                var lopIndexByKhoi = new Dictionary<int, int> { { 10, 0 }, { 11, 0 }, { 12, 0 } };

                // *** SỬA LỖI KHAI BÁO BIẾN ***
                // Khai báo danh sách học sinh mới ở phạm vi ngoài
                List<HocSinhDTO> hocSinhMoiChuaPhanLop = new List<HocSinhDTO>();
                // Lấy danh sách học sinh đã từng được phân lớp (để lọc ra học sinh mới)
                HashSet<int> hocSinhDaTungPhanLop = new HashSet<int>(allPhanLopHist.Select(p => p.maHocSinh));

                foreach (var hs in danhSachHocSinhDangHoc)
                {
                    if (!hocSinhDaTungPhanLop.Contains(hs.MaHS))
                    {
                        hocSinhMoiChuaPhanLop.Add(hs);
                    }
                }
                // *** KẾT THÚC SỬA LỖI ***

                int soHocSinhDaPhanLop = 0;
                List<string> hocSinhGapLoi = new List<string>();

                // 3. XỬ LÝ THEO KỊCH BẢN
                if (isChuyenSangHK2) // Kịch bản 1: HK1 -> HK2
                {
                    Console.WriteLine($"=== Bắt đầu phân lớp HK1 ({maHocKyHienTai}) -> HK2 ({hocKyTiepTheo.MaHocKy}) ===");

                    // Lọc ra danh sách HS đã có trong HK1
                    List<HocSinhDTO> hsDaCoLopHK1 = danhSachHocSinhDangHoc
                        .Where(hs => hocSinhDaTungPhanLop.Contains(hs.MaHS) && // Đã từng phân lớp
                                     phanLopBLL.CheckHocSinhDaPhanLop(hs.MaHS, maHocKyHienTai)) // Có trong HK1
                        .ToList();

                    foreach (var hs in hsDaCoLopHK1) // Chỉ xử lý HS đã có lớp HK1
                    {
                        try
                        {
                            Console.WriteLine($"\n--- Xử lý HS (cũ) {hs.MaHS} - {hs.HoTen} ---");

                            if (phanLopBLL.CheckHocSinhDaPhanLop(hs.MaHS, hocKyTiepTheo.MaHocKy))
                            {
                                Console.WriteLine($"  ⚠ Đã tồn tại trong HK2, bỏ qua.");
                                soHocSinhDaPhanLop++;
                                continue;
                            }

                            int maLopHK1 = phanLopBLL.GetLopByHocSinh(hs.MaHS, maHocKyHienTai);
                            if (maLopHK1 > 0)
                            {
                                Console.WriteLine($"  Chuyển sang HK2: Lớp {maLopHK1} (giữ nguyên)");
                                if (phanLopBLL.AddPhanLop(hs.MaHS, maLopHK1, hocKyTiepTheo.MaHocKy))
                                {
                                    soHocSinhDaPhanLop++;
                                    Console.WriteLine($"  ✓ Đã chuyển HK2 thành công (Tổng: {soHocSinhDaPhanLop})");
                                }
                                else
                                {
                                    hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Lỗi thêm PL HK2 (dù đã check)");
                                }
                            }
                            else
                            {
                                // Trường hợp này hiếm khi xảy ra do đã lọc ở trên
                                hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Đang học nhưng không có phân lớp HK1.");
                            }
                        }
                        catch (Exception ex)
                        {
                            hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Lỗi xử lý ({ex.Message})");
                        }
                    }

                    // Xử lý học sinh MỚI (chưa từng phân lớp) theo kịch bản của bạn
                    Console.WriteLine($"Phân lớp cho {hocSinhMoiChuaPhanLop.Count} học sinh mới...");
                    foreach (var hsMoi in hocSinhMoiChuaPhanLop)
                    {
                        try
                        {
                            Console.WriteLine($"\n--- Xử lý HS (mới) {hsMoi.MaHS} - {hsMoi.HoTen} ---");
                            int maLopMoi = -1;
                            int khoi = 0;
                            int namSinh = hsMoi.NgaySinh.Year;
                            int namHocHienTai = 2025;
                            if (hocKyHienTai.MaNamHoc.StartsWith("20")) int.TryParse(hocKyHienTai.MaNamHoc.Substring(0, 4), out namHocHienTai);

                            if (namSinh == (namHocHienTai - 15)) khoi = 10;
                            else if (namSinh == (namHocHienTai - 16)) khoi = 11;
                            else if (namSinh == (namHocHienTai - 17)) khoi = 12;

                            if (khoi > 0 && lopTheoKhoi.ContainsKey(khoi))
                            {
                                var dsLopKhoi = lopTheoKhoi[khoi];
                                int lopIdx = lopIndexByKhoi[khoi];
                                maLopMoi = dsLopKhoi[lopIdx % dsLopKhoi.Count].MaLop;
                                lopIndexByKhoi[khoi] = (lopIdx + 1) % dsLopKhoi.Count;

                                // Gán vào HK1
                                phanLopBLL.AddPhanLop(hsMoi.MaHS, maLopMoi, maHocKyHienTai);
                                Console.WriteLine($"  ✓ Đã gán vào HK1: Lớp {maLopMoi} (khối {khoi})");

                                // Gán vào HK2
                                phanLopBLL.AddPhanLop(hsMoi.MaHS, maLopMoi, hocKyTiepTheo.MaHocKy);
                                Console.WriteLine($"  ✓ Đã gán vào HK2: Lớp {maLopMoi} (khối {khoi})");
                                soHocSinhDaPhanLop++;
                            }
                            else
                            {
                                hocSinhGapLoi.Add($"{hsMoi.MaHS}-{hsMoi.HoTen}: HS mới, không xác định được khối (Năm sinh: {namSinh})");
                            }
                        }
                        catch (ArgumentException argEx)
                        {
                            hocSinhGapLoi.Add($"{hsMoi.MaHS}-{hsMoi.HoTen}: {argEx.Message}");
                            // Nếu đã tồn tại ở HK1 (do chạy lại), thử thêm vào HK2
                            if (argEx.Message.Contains($"học kỳ {maHocKyHienTai}"))
                            {
                                try
                                {
                                    int maLopDaCo = phanLopBLL.GetLopByHocSinh(hsMoi.MaHS, maHocKyHienTai);
                                    if (maLopDaCo > 0 && !phanLopBLL.CheckHocSinhDaPhanLop(hsMoi.MaHS, hocKyTiepTheo.MaHocKy))
                                    {
                                        phanLopBLL.AddPhanLop(hsMoi.MaHS, maLopDaCo, hocKyTiepTheo.MaHocKy);
                                        soHocSinhDaPhanLop++;
                                        Console.WriteLine($"  ✓ Đã gán vào HK2 (sau khi bắt lỗi tồn tại HK1)");
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    hocSinhGapLoi.Add($"{hsMoi.MaHS}-{hsMoi.HoTen}: Lỗi gán HK2 sau khi bắt lỗi ({ex2.Message})");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            hocSinhGapLoi.Add($"{hsMoi.MaHS}-{hsMoi.HoTen}: Lỗi xử lý HS Mới ({ex.Message})");
                        }
                    }

                    Console.WriteLine("=== Kết thúc phân lớp HK1 -> HK2 ===");
                }
                else // Kịch bản 2: HK2 -> HK1 Năm sau
                {
                    Console.WriteLine("=== Bắt đầu phân lớp HK2 -> HK1 Năm sau ===");
                    var hocSinhTheoKhoiMoi = new Dictionary<int, List<HocSinhDTO>>
            {
                { 10, new List<HocSinhDTO>() },
                { 11, new List<HocSinhDTO>() },
                { 12, new List<HocSinhDTO>() }
            };

                    // Lọc ra DS học sinh đã có phân lớp trong HK2
                    List<HocSinhDTO> hsCanXetLenLop = danhSachHocSinhDangHoc
                        .Where(hs => hocSinhDaTungPhanLop.Contains(hs.MaHS) && // Đã từng phân lớp
                                     phanLopBLL.CheckHocSinhDaPhanLop(hs.MaHS, maHocKyHienTai)) // Có trong HK2
                        .ToList();

                    foreach (var hs in hsCanXetLenLop)
                    {
                        try
                        {
                            int maLopHienTai = phanLopBLL.GetLopByHocSinh(hs.MaHS, maHocKyHienTai);
                            LopDTO lopHienTai = lopHocBUS.LayLopTheoId(maLopHienTai);
                            if (lopHienTai == null)
                            {
                                hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Lớp {maLopHienTai} không tồn tại.");
                                continue;
                            }
                            int khoiHienTai = lopHienTai.MaKhoi;

                            string tenHK1 = (hocKyHienTai.TenHocKy.Contains("II")) ?
                                            hocKyHienTai.TenHocKy.Replace("II", "I") :
                                            hocKyHienTai.TenHocKy.Replace("2", "1");

                            HocKyDTO hocKy1CungNam = hocKyBUS.LayDanhSachHocKyTheoNamHoc(hocKyHienTai.MaNamHoc)
                                                          ?.FirstOrDefault(hk => hk.TenHocKy == tenHK1);

                            if (hocKy1CungNam == null) { hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Không tìm thấy HK1 cùng năm"); continue; }
                            int maHK1CungNam = hocKy1CungNam.MaHocKy;

                            var diemHK1 = diemSoBUS.LayDiemTrungBinhMonTheoHocKy(hs.MaHS, maHK1CungNam);
                            var diemHK2 = diemSoBUS.LayDiemTrungBinhMonTheoHocKy(hs.MaHS, maHocKyHienTai);
                            var hanhKiemHK1 = hanhKiemBUS.GetHanhKiemByStudent(hs.MaHS, maHK1CungNam);
                            var hanhKiemHK2 = hanhKiemBUS.GetHanhKiemByStudent(hs.MaHS, maHocKyHienTai);

                            if (diemHK1 == null || diemHK2 == null || hanhKiemHK1 == null || hanhKiemHK2 == null || diemHK1.Count == 0 || diemHK2.Count == 0)
                            {
                                hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Thiếu điểm hoặc hạnh kiểm HK1/HK2");
                                continue;
                            }

                            float dtbNam = TinhDTBCaNam(diemHK1, diemHK2);
                            string hanhKiemNam = XetHanhKiemCaNam(hanhKiemHK1.XepLoai, hanhKiemHK2.XepLoai);

                            // === SỬA LOGIC ĐẾM MÔN KÉM/YẾU ===
                            // Tính ĐTB Cả Năm của TỪNG MÔN
                            List<float> dtbTungMonCaNam = new List<float>();
                            var maMonHocChung = diemHK1.Keys.Intersect(diemHK2.Keys).ToList();
                            var maMonChiHK1 = diemHK1.Keys.Except(diemHK2.Keys).ToList();
                            var maMonChiHK2 = diemHK2.Keys.Except(diemHK1.Keys).ToList();

                            foreach (int maMon in maMonHocChung)
                            {
                                if (diemHK1[maMon].HasValue && diemHK2[maMon].HasValue)
                                {
                                    dtbTungMonCaNam.Add((diemHK1[maMon].Value + diemHK2[maMon].Value * 2) / 3.0f);
                                }
                            }
                            foreach (int maMon in maMonChiHK1)
                            {
                                if (diemHK1[maMon].HasValue) dtbTungMonCaNam.Add(diemHK1[maMon].Value);
                            }
                            foreach (int maMon in maMonChiHK2)
                            {
                                if (diemHK2[maMon].HasValue) dtbTungMonCaNam.Add(diemHK2[maMon].Value);
                            }

                            // Đếm số môn Kém/Yếu DỰA TRÊN ĐTB CẢ NĂM CỦA MÔN ĐÓ
                            bool coMonKem = dtbTungMonCaNam.Any(d => d < 3.5f);
                            int soMonYeu = dtbTungMonCaNam.Count(d => d >= 3.5f && d < 5.0f);
                            // === KẾT THÚC SỬA LOGIC ===

                            bool duocLenLop = dtbNam >= 5.0f &&
                                              IsHanhKiemDuDieuKienLenLop(hanhKiemNam) &&
                                              !coMonKem &&
                                              soMonYeu <= 2;

                            if (duocLenLop)
                            {
                                if (khoiHienTai == 12)
                                {
                                    hs.TrangThai = "Đã tốt nghiệp";
                                    hocSinhBLL.UpdateHocSinh(hs);
                                    Console.WriteLine($"HS {hs.MaHS}-{hs.HoTen} đã tốt nghiệp.");
                                }
                                else
                                {
                                    hocSinhTheoKhoiMoi[khoiHienTai + 1].Add(hs);
                                }
                            }
                            else
                            {
                                hocSinhTheoKhoiMoi[khoiHienTai].Add(hs);
                                Console.WriteLine($"HS {hs.MaHS}-{hs.HoTen} ở lại khối {khoiHienTai}.");
                            }
                        }
                        catch (Exception ex)
                        {
                            hocSinhGapLoi.Add($"{hs.MaHS}-{hs.HoTen}: Lỗi xét lên lớp ({ex.Message})");
                        }
                    }

                    // Phân bổ học sinh cũ (lên lớp/ở lại)
                    soHocSinhDaPhanLop = PhanBoHocSinhVaoLop(hocSinhTheoKhoiMoi, lopTheoKhoi, hocKyTiepTheo.MaHocKy, hocSinhGapLoi);

                    // Xử lý học sinh mới (chưa có trong lịch sử)
                    Console.WriteLine($"Phân lớp cho {hocSinhMoiChuaPhanLop.Count} học sinh mới...");
                    if (hocSinhMoiChuaPhanLop.Count > 0 && lopTheoKhoi.ContainsKey(10))
                    {
                        var lopKhoi10 = lopTheoKhoi[10];
                        int lopIndex = 0;
                        foreach (var hsMoi in hocSinhMoiChuaPhanLop)
                        {
                            bool daPhanLopMoi = false;
                            int soLanThu = 0;
                            while (!daPhanLopMoi && soLanThu < lopKhoi10.Count)
                            {
                                LopDTO lopTarget = lopKhoi10[lopIndex];
                                int siSoHienTai = phanLopBLL.CountHocSinhInLop(lopTarget.MaLop, hocKyTiepTheo.MaHocKy);
                                if (siSoHienTai < 30)
                                {
                                    try
                                    {
                                        // Gán cho HK1 (mới) của năm sau
                                        if (phanLopBLL.AddPhanLop(hsMoi.MaHS, lopTarget.MaLop, hocKyTiepTheo.MaHocKy))
                                        {
                                            soHocSinhDaPhanLop++;
                                            daPhanLopMoi = true;
                                        }
                                        else
                                        {
                                            hocSinhGapLoi.Add($"{hsMoi.MaHS}-{hsMoi.HoTen}: Lỗi AddPhanLop vào lớp {lopTarget.TenLop}");
                                        }
                                    }
                                    catch (ArgumentException argEx)
                                    {
                                        hocSinhGapLoi.Add($"{hsMoi.MaHS}-{hsMoi.HoTen}: {argEx.Message}");
                                        daPhanLopMoi = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        hocSinhGapLoi.Add($"{hsMoi.MaHS}-{hsMoi.HoTen}: Lỗi nghiêm trọng khi thêm ({ex.Message})");
                                        daPhanLopMoi = true;
                                    }
                                }
                                lopIndex = (lopIndex + 1) % lopKhoi10.Count;
                                soLanThu++;
                            }
                            if (!daPhanLopMoi) hocSinhGapLoi.Add($"{hsMoi.MaHS}-{hsMoi.HoTen}: Không phân được lớp 10 (có thể lớp đầy)");
                        }
                    }
                    Console.WriteLine("=== Kết thúc phân lớp HK2 -> HK1 Năm sau ===");
                }

                // 5. KẾT QUẢ
                string finalMessage = $"{(isChuyenSangHK2 ? "HK1→HK2" : "HK2→HK1 Năm sau")}. ";
                finalMessage += $"Đã phân lớp thành công: {soHocSinhDaPhanLop} học sinh.";
                if (hocSinhGapLoi.Count > 0)
                {
                    finalMessage += $"\nCó {hocSinhGapLoi.Count} học sinh gặp lỗi/bị bỏ qua:\n - {string.Join("\n - ", hocSinhGapLoi)}";
                    return (false, finalMessage, soHocSinhDaPhanLop);
                }

                return (true, finalMessage, soHocSinhDaPhanLop);
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

        #region Tạo preview (Cần cập nhật)

        public Dictionary<string, object> TaoPreviewPhanLop(int maHocKyHienTai)
        {
            Dictionary<string, object> preview = new Dictionary<string, object>();
            try
            {
                HocKyDTO hocKyHienTai = hocKyBUS.LayHocKyTheoMa(maHocKyHienTai);
                if (hocKyHienTai == null) { preview["Loi"] = "Không tìm thấy học kỳ"; return preview; }

                string tenHkLower = hocKyHienTai.TenHocKy.ToLower();
                bool isHK1 = (tenHkLower.Contains("i") && !tenHkLower.Contains("ii")) || (tenHkLower.Contains("1") && !tenHkLower.Contains("2"));
                preview["LoaiPhanLop"] = isHK1 ? "HK1 → HK2 (Giữ nguyên lớp)" : "HK2 → HK1 Năm sau (Lên lớp/Ở lại)";

                // LẤY TẤT CẢ HỌC SINH ĐANG HỌC (không lọc theo học kỳ)
                List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                                                        .Where(hs => hs.TrangThai == "Đang học")
                                                        .ToList();
                preview["TongSoHocSinh"] = danhSachHocSinhDangHoc.Count;


                int duDieuKien = 0;
                int khongDuDieuKien = 0;
                int loiXuLy = 0;

                if (isHK1)
                {
                    foreach (var hs in danhSachHocSinhDangHoc)
                    {
                        try
                        {
                            XepLoaiDTO xepLoaiHK1 = xepLoaiBUS.GetXepLoaiByStudent(hs.MaHS, maHocKyHienTai);
                            HanhKiemDTO hanhKiemHK1 = hanhKiemBUS.GetHanhKiemByStudent(hs.MaHS, maHocKyHienTai);
                            if (xepLoaiHK1 != null && IsHocLucDuDieuKienHK2(xepLoaiHK1.HocLuc) &&
                                hanhKiemHK1 != null && IsHanhKiemDuDieuKienHK2(hanhKiemHK1.XepLoai))
                            {
                                duDieuKien++;
                            }
                            else
                            {
                                khongDuDieuKien++;
                            }
                        }
                        catch { loiXuLy++; }
                    }
                    preview["SoHSDuDieuKien"] = duDieuKien;
                    preview["SoHSKhongDuDieuKien"] = khongDuDieuKien;
                }
                else // HK2 -> HK1
                {
                    int lenLop = 0;
                    int oLai = 0;
                    foreach (var hs in danhSachHocSinhDangHoc)
                    {
                        try
                        {
                            HocKyDTO hocKy1CungNam = hocKyBUS.LayDanhSachHocKyTheoNamHoc(hocKyHienTai.MaNamHoc)
                                                       ?.FirstOrDefault(hk => hk.TenHocKy.Contains("I"));
                            if (hocKy1CungNam == null) { loiXuLy++; continue; }

                            var diemHK1 = diemSoBUS.LayDiemTrungBinhMonTheoHocKy(hs.MaHS, hocKy1CungNam.MaHocKy);
                            var diemHK2 = diemSoBUS.LayDiemTrungBinhMonTheoHocKy(hs.MaHS, maHocKyHienTai);
                            var hanhKiemHK1 = hanhKiemBUS.GetHanhKiemByStudent(hs.MaHS, hocKy1CungNam.MaHocKy);
                            var hanhKiemHK2 = hanhKiemBUS.GetHanhKiemByStudent(hs.MaHS, maHocKyHienTai);

                            if (diemHK1 == null || diemHK2 == null || hanhKiemHK1 == null || hanhKiemHK2 == null)
                            { loiXuLy++; continue; }

                            float dtbNam = TinhDTBCaNam(diemHK1, diemHK2);
                            string hanhKiemNam = XetHanhKiemCaNam(hanhKiemHK1.XepLoai, hanhKiemHK2.XepLoai);

                            // === SỬA LOGIC ĐẾM MÔN KÉM/YẾU ===
                            // Tính ĐTB Cả Năm của TỪNG MÔN
                            List<float> dtbTungMonCaNam = new List<float>();
                            var maMonHocChung = diemHK1.Keys.Intersect(diemHK2.Keys).ToList();
                            var maMonChiHK1 = diemHK1.Keys.Except(diemHK2.Keys).ToList();
                            var maMonChiHK2 = diemHK2.Keys.Except(diemHK1.Keys).ToList();

                            foreach (int maMon in maMonHocChung)
                            {
                                if (diemHK1[maMon].HasValue && diemHK2[maMon].HasValue)
                                {
                                    dtbTungMonCaNam.Add((diemHK1[maMon].Value + diemHK2[maMon].Value * 2) / 3.0f);
                                }
                            }
                            foreach (int maMon in maMonChiHK1)
                            {
                                if (diemHK1[maMon].HasValue) dtbTungMonCaNam.Add(diemHK1[maMon].Value);
                            }
                            foreach (int maMon in maMonChiHK2)
                            {
                                if (diemHK2[maMon].HasValue) dtbTungMonCaNam.Add(diemHK2[maMon].Value);
                            }

                            // Đếm số môn Kém/Yếu DỰA TRÊN ĐTB CẢ NĂM CỦA MÔN ĐÓ
                            bool coMonKem = dtbTungMonCaNam.Any(d => d < 3.5f);
                            int soMonYeu = dtbTungMonCaNam.Count(d => d >= 3.5f && d < 5.0f);
                            // === KẾT THÚC SỬA LOGIC ===

                            bool duocLenLop = dtbNam >= 5.0f &&
                                              IsHanhKiemDuDieuKienLenLop(hanhKiemNam) &&
                                              !coMonKem &&
                                              soMonYeu <= 2;

                            if (duocLenLop) lenLop++; else oLai++;

                        }
                        catch { loiXuLy++; }
                    }
                    preview["SoHSDuocLenLop"] = lenLop;
                    preview["SoHSOLai"] = oLai;
                    preview["TyLeLenLop"] = (danhSachHocSinhDangHoc.Count > 0) ? ((float)lenLop / danhSachHocSinhDangHoc.Count * 100) : 0;
                }
                if (loiXuLy > 0) preview["SoHSGapLoi"] = loiXuLy;

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