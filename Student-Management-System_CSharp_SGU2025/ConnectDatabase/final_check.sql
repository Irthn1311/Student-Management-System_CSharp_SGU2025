USE QuanLyHocSinh;

-- =====================================================================
-- KIỂM TRA CUỐI CÙNG: TẠI SAO 474 vs 384?
-- =====================================================================

-- 1. Đếm THẬT SỰ có bao nhiêu học sinh được phân
SELECT 
    'TỔNG HỌC SINH ĐƯỢC PHÂN LỚP HK3' as ThongKe,
    COUNT(DISTINCT MaHocSinh) as SoLuong
FROM PhanLop 
WHERE MaHocKy = 3;

-- 2. Kiểm tra trạng thái của học sinh được phân HK3
SELECT 
    hs.TrangThai,
    COUNT(DISTINCT pl.MaHocSinh) as SoLuong
FROM PhanLop pl
JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
WHERE pl.MaHocKy = 3
GROUP BY hs.TrangThai;

-- 3. Tìm học sinh "Đang học" có HK2 và HK3
SELECT 
    COUNT(DISTINCT hs.MaHocSinh) as SoHocSinhDangHoc_CoHK2vaHK3
FROM HocSinh hs
JOIN PhanLop pl2 ON hs.MaHocSinh = pl2.MaHocSinh AND pl2.MaHocKy = 2
JOIN PhanLop pl3 ON hs.MaHocSinh = pl3.MaHocSinh AND pl3.MaHocKy = 3
WHERE hs.TrangThai = 'Đang học';

-- 4. So sánh chi tiết
SELECT 
    'HK2 (Đang học)' as Nhom,
    COUNT(DISTINCT hs.MaHocSinh) as SoLuong
FROM HocSinh hs
JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh AND pl.MaHocKy = 2
WHERE hs.TrangThai = 'Đang học'

UNION ALL

SELECT 
    'HK3 (Đang học)' as Nhom,
    COUNT(DISTINCT hs.MaHocSinh) as SoLuong
FROM HocSinh hs
JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh AND pl.MaHocKy = 3
WHERE hs.TrangThai = 'Đang học'

UNION ALL

SELECT 
    'HK3 (Tất cả)' as Nhom,
    COUNT(DISTINCT MaHocSinh) as SoLuong
FROM PhanLop
WHERE MaHocKy = 3;

-- 5. Kiểm tra xem có học sinh nào BỊ TRÙNG trong HK3 không
SELECT 
    MaHocSinh,
    COUNT(*) as SoLanPhanLop,
    GROUP_CONCAT(MaLop) as DanhSachLop
FROM PhanLop
WHERE MaHocKy = 3
GROUP BY MaHocSinh
HAVING COUNT(*) > 1;
