-- =====================================================================
-- FILE 00: SMOKE TEST
-- Mục đích: Kiểm tra dữ liệu sau khi import
-- Chạy: mysql -u root -p < 00_smoke_test.sql
-- =====================================================================

USE QuanLyHocSinh;

-- =====================================================================
-- KIỂM TRA CƠ BẢN
-- =====================================================================

SELECT '=== KIỂM TRA CƠ BẢN ===' AS Test_Section;

-- Kiểm tra số lượng bảng
SELECT COUNT(*) AS so_bang FROM information_schema.tables 
WHERE table_schema = 'QuanLyHocSinh';

-- Kiểm tra số lượng khối lớp
SELECT COUNT(*) AS so_khoi FROM KhoiLop;

-- Kiểm tra số lượng môn học
SELECT COUNT(*) AS so_mon_hoc FROM MonHoc;

-- Kiểm tra số lượng năm học
SELECT COUNT(*) AS so_nam_hoc FROM NamHoc;

-- Kiểm tra số lượng học kỳ
SELECT COUNT(*) AS so_hoc_ky FROM HocKy;

-- =====================================================================
-- KIỂM TRA GIÁO VIÊN
-- =====================================================================

SELECT '=== KIỂM TRA GIÁO VIÊN ===' AS Test_Section;

-- Tổng số giáo viên
SELECT COUNT(*) AS tong_so_giao_vien FROM GiaoVien;

-- Kiểm tra email giáo viên không trùng
SELECT COUNT(*) AS so_email_trung FROM (
    SELECT Email, COUNT(*) as cnt FROM GiaoVien 
    WHERE Email IS NOT NULL 
    GROUP BY Email 
    HAVING cnt > 1
) AS duplicates;

-- Kiểm tra số điện thoại giáo viên không trùng
SELECT COUNT(*) AS so_sdt_trung FROM (
    SELECT SoDienThoai, COUNT(*) as cnt FROM GiaoVien 
    WHERE SoDienThoai IS NOT NULL 
    GROUP BY SoDienThoai 
    HAVING cnt > 1
) AS duplicates;

-- Phân bố giáo viên theo giới tính
SELECT GioiTinh, COUNT(*) AS so_luong FROM GiaoVien GROUP BY GioiTinh;

-- =====================================================================
-- KIỂM TRA LỚP HỌC
-- =====================================================================

SELECT '=== KIỂM TRA LỚP HỌC ===' AS Test_Section;

-- Tổng số lớp học
SELECT COUNT(*) AS tong_so_lop FROM LopHoc;

-- Phân bố lớp theo khối
SELECT MaKhoi, COUNT(*) AS so_lop FROM LopHoc GROUP BY MaKhoi ORDER BY MaKhoi;

-- Kiểm tra sĩ số lớp
SELECT TenLop, SiSo FROM LopHoc ORDER BY MaKhoi, TenLop;

-- Kiểm tra lớp có giáo viên chủ nhiệm
SELECT COUNT(*) AS lop_co_chu_nhiem FROM LopHoc WHERE MaGiaoVienChuNhiem IS NOT NULL;

-- =====================================================================
-- KIỂM TRA HỌC SINH
-- =====================================================================

SELECT '=== KIỂM TRA HỌC SINH ===' AS Test_Section;

-- Tổng số học sinh
SELECT COUNT(*) AS tong_so_hoc_sinh FROM HocSinh;

-- Phân bố học sinh theo giới tính
SELECT GioiTinh, COUNT(*) AS so_luong FROM HocSinh GROUP BY GioiTinh;

-- Kiểm tra email học sinh không trùng
SELECT COUNT(*) AS so_email_trung FROM (
    SELECT Email, COUNT(*) as cnt FROM HocSinh 
    WHERE Email IS NOT NULL 
    GROUP BY Email 
    HAVING cnt > 1
) AS duplicates;

-- Kiểm tra số điện thoại học sinh không trùng
SELECT COUNT(*) AS so_sdt_trung FROM (
    SELECT SDTHS, COUNT(*) as cnt FROM HocSinh 
    WHERE SDTHS IS NOT NULL 
    GROUP BY SDTHS 
    HAVING cnt > 1
) AS duplicates;

-- =====================================================================
-- KIỂM TRA PHỤ HUYNH
-- =====================================================================

SELECT '=== KIỂM TRA PHỤ HUYNH ===' AS Test_Section;

-- Tổng số phụ huynh
SELECT COUNT(*) AS tong_so_phu_huynh FROM PhuHuynh;

-- Kiểm tra email phụ huynh không trùng
SELECT COUNT(*) AS so_email_trung FROM (
    SELECT Email, COUNT(*) as cnt FROM PhuHuynh 
    WHERE Email IS NOT NULL 
    GROUP BY Email 
    HAVING cnt > 1
) AS duplicates;

-- Kiểm tra số điện thoại phụ huynh không trùng
SELECT COUNT(*) AS so_sdt_trung FROM (
    SELECT SoDienThoai, COUNT(*) as cnt FROM PhuHuynh 
    WHERE SoDienThoai IS NOT NULL 
    GROUP BY SoDienThoai 
    HAVING cnt > 1
) AS duplicates;

-- =====================================================================
-- KIỂM TRA PHÂN LỚP
-- =====================================================================

SELECT '=== KIỂM TRA PHÂN LỚP ===' AS Test_Section;

-- Tổng số phân lớp
SELECT COUNT(*) AS tong_so_phan_lop FROM PhanLop;

-- Sĩ số từng lớp trong học kỳ 1
SELECT l.TenLop, COUNT(p.MaHocSinh) AS si_so_thuc_te, l.SiSo AS si_so_ghi_nhan
FROM LopHoc l
LEFT JOIN PhanLop p ON l.MaLop = p.MaLop AND p.MaHocKy = 1
GROUP BY l.MaLop, l.TenLop, l.SiSo
ORDER BY l.MaKhoi, l.TenLop;

-- Kiểm tra học sinh có phân lớp trùng
SELECT COUNT(*) AS hs_phan_lop_trung FROM (
    SELECT MaHocSinh, COUNT(*) as cnt FROM PhanLop 
    GROUP BY MaHocSinh 
    HAVING cnt > 1
) AS duplicates;

-- =====================================================================
-- KIỂM TRA CHUYÊN MÔN GIÁO VIÊN
-- =====================================================================

SELECT '=== KIỂM TRA CHUYÊN MÔN GIÁO VIÊN ===' AS Test_Section;

-- Tổng số chuyên môn
SELECT COUNT(*) AS tong_so_chuyen_mon FROM GiaoVienChuyenMon;

-- Phân bố giáo viên theo môn học
SELECT m.TenMonHoc, COUNT(gc.MaGiaoVien) AS so_giao_vien
FROM MonHoc m
LEFT JOIN GiaoVienChuyenMon gc ON m.MaMonHoc = gc.MaMonHoc
GROUP BY m.MaMonHoc, m.TenMonHoc
ORDER BY m.MaMonHoc;

-- Giáo viên có chuyên môn chính
SELECT COUNT(*) AS gv_co_chuyen_mon_chinh FROM GiaoVienChuyenMon WHERE LaChuyenMonChinh = TRUE;

-- =====================================================================
-- KIỂM TRA PHÂN CÔNG GIẢNG DẠY
-- =====================================================================

SELECT '=== KIỂM TRA PHÂN CÔNG GIẢNG DẠY ===' AS Test_Section;

-- Tổng số phân công
SELECT COUNT(*) AS tong_so_phan_cong FROM PhanCongGiangDay;

-- Phân công theo lớp
SELECT l.TenLop, COUNT(pc.MaPhanCong) AS so_mon_phan_cong
FROM LopHoc l
LEFT JOIN PhanCongGiangDay pc ON l.MaLop = pc.MaLop
GROUP BY l.MaLop, l.TenLop
ORDER BY l.MaKhoi, l.TenLop;

-- Kiểm tra phân công trùng (vi phạm UNIQUE)
SELECT COUNT(*) AS phan_cong_trung FROM (
    SELECT MaLop, MaGiaoVien, MaMonHoc, MaHocKy, COUNT(*) as cnt 
    FROM PhanCongGiangDay 
    GROUP BY MaLop, MaGiaoVien, MaMonHoc, MaHocKy 
    HAVING cnt > 1
) AS duplicates;

-- =====================================================================
-- KIỂM TRA THỜI KHÓA BIỂU
-- =====================================================================

SELECT '=== KIỂM TRA THỜI KHÓA BIỂU ===' AS Test_Section;

-- Tổng số tiết trong TKB
SELECT COUNT(*) AS tong_so_tiet FROM ThoiKhoaBieu;

-- Phân bố theo thứ trong tuần
SELECT ThuTrongTuan, COUNT(*) AS so_tiet FROM ThoiKhoaBieu GROUP BY ThuTrongTuan ORDER BY ThuTrongTuan;

-- Phân bố theo phòng học
SELECT PhongHoc, COUNT(*) AS so_tiet FROM ThoiKhoaBieu GROUP BY PhongHoc ORDER BY PhongHoc;

-- =====================================================================
-- KIỂM TRA NGƯỜI DÙNG VÀ PHÂN QUYỀN
-- =====================================================================

SELECT '=== KIỂM TRA NGƯỜI DÙNG VÀ PHÂN QUYỀN ===' AS Test_Section;

-- Tổng số người dùng
SELECT COUNT(*) AS tong_so_nguoi_dung FROM NguoiDung;

-- Phân bố theo vai trò
SELECT vt.TenVaiTro, COUNT(ndvt.TenDangNhap) AS so_luong
FROM VaiTro vt
LEFT JOIN NguoiDungVaiTro ndvt ON vt.MaVaiTro = ndvt.MaVaiTro
GROUP BY vt.MaVaiTro, vt.TenVaiTro
ORDER BY vt.MaVaiTro;

-- Kiểm tra người dùng có vai trò
SELECT COUNT(*) AS nd_co_vai_tro FROM NguoiDungVaiTro;

-- =====================================================================
-- KIỂM TRA LIÊN KẾT HỌC SINH - PHỤ HUYNH
-- =====================================================================

SELECT '=== KIỂM TRA LIÊN KẾT HỌC SINH - PHỤ HUYNH ===' AS Test_Section;

-- Tổng số liên kết
SELECT COUNT(*) AS tong_so_lien_ket FROM HocSinhPhuHuynh;

-- Học sinh có phụ huynh
SELECT COUNT(DISTINCT MaHocSinh) AS hs_co_phu_huynh FROM HocSinhPhuHuynh;

-- Phụ huynh có học sinh
SELECT COUNT(DISTINCT MaPhuHuynh) AS ph_co_hoc_sinh FROM HocSinhPhuHuynh;

-- =====================================================================
-- TỔNG KẾT
-- =====================================================================

SELECT '=== TỔNG KẾT ===' AS Test_Section;

SELECT 
    'Database QuanLyHocSinh đã được tạo thành công!' AS Status,
    (SELECT COUNT(*) FROM GiaoVien) AS So_Giao_Vien,
    (SELECT COUNT(*) FROM HocSinh) AS So_Hoc_Sinh,
    (SELECT COUNT(*) FROM LopHoc) AS So_Lop_Hoc,
    (SELECT COUNT(*) FROM PhuHuynh) AS So_Phu_Huynh,
    (SELECT COUNT(*) FROM MonHoc) AS So_Mon_Hoc,
    (SELECT COUNT(*) FROM PhanCongGiangDay) AS So_Phan_Cong,
    (SELECT COUNT(*) FROM ThoiKhoaBieu) AS So_Tiet_TKB;

SELECT 'Smoke test completed successfully!' AS Final_Status;
