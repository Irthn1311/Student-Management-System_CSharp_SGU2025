-- =====================================================================
-- DEBUG: TÌM 98 HỌC SINH THIẾU
-- =====================================================================

USE QuanLyHocSinh;

-- 1. Kiểm tra TrangThai của học sinh
SELECT 
    TrangThai,
    COUNT(*) as SoLuong
FROM HocSinh
GROUP BY TrangThai;

-- 2. Đếm học sinh theo MaHocKy (bao gồm cả TrangThai)
SELECT 
    pl.MaHocKy,
    hs.TrangThai,
    COUNT(DISTINCT pl.MaHocSinh) as SoHocSinh
FROM PhanLop pl
JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
GROUP BY pl.MaHocKy, hs.TrangThai
ORDER BY pl.MaHocKy, hs.TrangThai;

-- 3. Tìm học sinh có HK2 nhưng KHÔNG có HK1 năm sau (đơn giản)
SELECT 
    hs.MaHocSinh,
    hs.HoTen,
    hs.TrangThai,
    pl2.MaLop as LopHK2,
    pl3.MaLop as LopHK1NamSau
FROM HocSinh hs
JOIN PhanLop pl2 ON hs.MaHocSinh = pl2.MaHocSinh AND pl2.MaHocKy = 2
LEFT JOIN PhanLop pl3 ON hs.MaHocSinh = pl3.MaHocSinh AND pl3.MaHocKy = 3
WHERE pl3.MaLop IS NULL
ORDER BY hs.MaHocSinh
LIMIT 50;

-- 4. Đếm số học sinh thiếu
SELECT 
    COUNT(*) as SoHocSinhThieu
FROM HocSinh hs
JOIN PhanLop pl2 ON hs.MaHocSinh = pl2.MaHocSinh AND pl2.MaHocKy = 2
LEFT JOIN PhanLop pl3 ON hs.MaHocSinh = pl3.MaHocSinh AND pl3.MaHocKy = 3
WHERE pl3.MaLop IS NULL;

-- 5. So sánh TrangThai giữa HK2 và HK1 năm sau
SELECT 
    'HK2' as HocKy,
    hs.TrangThai,
    COUNT(DISTINCT pl.MaHocSinh) as SoHocSinh
FROM PhanLop pl
JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
WHERE pl.MaHocKy = 2
GROUP BY hs.TrangThai

UNION ALL

SELECT 
    'HK1 năm sau' as HocKy,
    hs.TrangThai,
    COUNT(DISTINCT pl.MaHocSinh) as SoHocSinh
FROM PhanLop pl
JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
WHERE pl.MaHocKy = 3
GROUP BY hs.TrangThai;
