-- =====================================================================
-- CÁC QUERY KIỂM TRA KẾT QUẢ PHÂN LỚP HK2 -> HK1 NĂM SAU
-- =====================================================================

USE QuanLyHocSinh;

-- =====================================================================
-- 1. XEM TỔNG QUAN TRƯỚC KHI PHÂN LỚP
-- =====================================================================

-- Tổng số học sinh đang học
SELECT 
    'Tổng học sinh đang học' as ThongKe,
    COUNT(*) as SoLuong
FROM HocSinh 
WHERE TrangThai = 'Đang học';

-- Phân bố học sinh theo khối (HK2 năm 2025-2026)
SELECT 
    lh.MaKhoi,
    CASE 
        WHEN lh.MaKhoi = 10 THEN 'Khối 10'
        WHEN lh.MaKhoi = 11 THEN 'Khối 11'
        WHEN lh.MaKhoi = 12 THEN 'Khối 12'
    END as TenKhoi,
    COUNT(DISTINCT pl.MaHocSinh) as SoHocSinh
FROM PhanLop pl
JOIN LopHoc lh ON pl.MaLop = lh.MaLop
WHERE pl.MaHocKy = 2  -- HK2
GROUP BY lh.MaKhoi
ORDER BY lh.MaKhoi;

-- =====================================================================
-- 2. DANH SÁCH HỌC SINH ĐỦ ĐIỀU KIỆN LÊN LỚP
-- =====================================================================

SELECT 
    hs.MaHocSinh,
    hs.HoTen,
    lh.TenLop as LopHienTai,
    lh.MaKhoi as KhoiHienTai,
    ROUND(AVG(ds1.DiemTrungBinh), 2) as DiemTB_HK1,
    ROUND(AVG(ds2.DiemTrungBinh), 2) as DiemTB_HK2,
    ROUND((AVG(ds1.DiemTrungBinh) + AVG(ds2.DiemTrungBinh) * 2) / 3, 2) as DiemTB_CaNam,
    hk1.XepLoai as HanhKiem_HK1,
    hk2.XepLoai as HanhKiem_HK2,
    COUNT(CASE WHEN ds2.DiemTrungBinh < 5 THEN 1 END) as SoMonDuoi5,
    COUNT(CASE WHEN ds2.DiemTrungBinh = 0 THEN 1 END) as SoMonKem,
    CASE 
        WHEN (AVG(ds1.DiemTrungBinh) + AVG(ds2.DiemTrungBinh) * 2) / 3 >= 5.0 
             AND hk2.XepLoai IN ('Tốt', 'Khá', 'Trung bình')
             AND COUNT(CASE WHEN ds2.DiemTrungBinh = 0 THEN 1 END) = 0
             AND COUNT(CASE WHEN ds2.DiemTrungBinh < 5 THEN 1 END) <= 2
        THEN 'LÊN LỚP'
        ELSE 'Ở LẠI'
    END as KetQua
FROM HocSinh hs
JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh AND pl.MaHocKy = 2
JOIN LopHoc lh ON pl.MaLop = lh.MaLop
LEFT JOIN DiemSo ds1 ON hs.MaHocSinh = ds1.MaHocSinh AND ds1.MaHocKy = 1
LEFT JOIN DiemSo ds2 ON hs.MaHocSinh = ds2.MaHocSinh AND ds2.MaHocKy = 2
LEFT JOIN HanhKiem hk1 ON hs.MaHocSinh = hk1.MaHocSinh AND hk1.MaHocKy = 1
LEFT JOIN HanhKiem hk2 ON hs.MaHocSinh = hk2.MaHocSinh AND hk2.MaHocKy = 2
WHERE hs.TrangThai = 'Đang học'
GROUP BY hs.MaHocSinh, hs.HoTen, lh.TenLop, lh.MaKhoi, hk1.XepLoai, hk2.XepLoai
ORDER BY KetQua DESC, lh.MaKhoi, hs.MaHocSinh;

-- =====================================================================
-- 3. THỐNG KÊ SỐ LƯỢNG LÊN LỚP / Ở LẠI THEO KHỐI
-- =====================================================================

-- Dùng subquery để tính trước điều kiện cho từng học sinh
SELECT 
    KhoiHoc,
    CASE 
        WHEN KhoiHoc = 10 THEN 'Khối 10'
        WHEN KhoiHoc = 11 THEN 'Khối 11'
        WHEN KhoiHoc = 12 THEN 'Khối 12'
    END as TenKhoi,
    COUNT(*) as TongSo,
    SUM(CASE WHEN DuDieuKien = 1 THEN 1 ELSE 0 END) as SoHSLenLop,
    SUM(CASE WHEN DuDieuKien = 0 THEN 1 ELSE 0 END) as SoHSOLai,
    ROUND(SUM(CASE WHEN DuDieuKien = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*), 2) as TyLeLenLop
FROM (
    SELECT 
        hs.MaHocSinh,
        lh.MaKhoi as KhoiHoc,
        ROUND((AVG(ds1.DiemTrungBinh) + AVG(ds2.DiemTrungBinh) * 2) / 3, 2) as DiemTB_CaNam,
        hk2.XepLoai,
        COUNT(CASE WHEN ds2.DiemTrungBinh < 5 THEN 1 END) as SoMonDuoi5,
        COUNT(CASE WHEN ds2.DiemTrungBinh = 0 THEN 1 END) as SoMonKem,
        CASE 
            WHEN (AVG(ds1.DiemTrungBinh) + AVG(ds2.DiemTrungBinh) * 2) / 3 >= 5.0 
                 AND hk2.XepLoai IN ('Tốt', 'Khá', 'Trung bình')
                 AND COUNT(CASE WHEN ds2.DiemTrungBinh = 0 THEN 1 END) = 0
                 AND COUNT(CASE WHEN ds2.DiemTrungBinh < 5 THEN 1 END) <= 2
            THEN 1 
            ELSE 0 
        END as DuDieuKien
    FROM HocSinh hs
    JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh AND pl.MaHocKy = 2
    JOIN LopHoc lh ON pl.MaLop = lh.MaLop
    LEFT JOIN DiemSo ds1 ON hs.MaHocSinh = ds1.MaHocSinh AND ds1.MaHocKy = 1
    LEFT JOIN DiemSo ds2 ON hs.MaHocSinh = ds2.MaHocSinh AND ds2.MaHocKy = 2
    LEFT JOIN HanhKiem hk2 ON hs.MaHocSinh = hk2.MaHocSinh AND hk2.MaHocKy = 2
    WHERE hs.TrangThai = 'Đang học'
    GROUP BY hs.MaHocSinh, lh.MaKhoi, hk2.XepLoai
) as KetQuaHocSinh
GROUP BY KhoiHoc
ORDER BY KhoiHoc;

-- =====================================================================
-- 4. SAU KHI CHẠY PHÂN LỚP - KIỂM TRA KẾT QUẢ
-- =====================================================================

-- Số học sinh được phân lớp vào HK1 năm sau (HocKy 3)
SELECT 
    'Đã phân lớp HK1 năm sau' as ThongKe,
    COUNT(DISTINCT MaHocSinh) as SoLuong
FROM PhanLop
WHERE MaHocKy = 3;

-- Phân bố theo khối sau khi phân lớp
SELECT 
    lh.MaKhoi,
    CASE 
        WHEN lh.MaKhoi = 10 THEN 'Khối 10 (Ở lại)'
        WHEN lh.MaKhoi = 11 THEN 'Khối 11 (Từ 10 lên)'
        WHEN lh.MaKhoi = 12 THEN 'Khối 12 (Từ 11 lên)'
    END as MoTa,
    COUNT(DISTINCT pl.MaHocSinh) as SoHocSinh
FROM PhanLop pl
JOIN LopHoc lh ON pl.MaLop = lh.MaLop
WHERE pl.MaHocKy = 3  -- HK1 năm sau
GROUP BY lh.MaKhoi
ORDER BY lh.MaKhoi;

-- So sánh khối trước và sau
SELECT 
    hs.MaHocSinh,
    hs.HoTen,
    lh_cu.MaKhoi as KhoiCu,
    lh_moi.MaKhoi as KhoiMoi,
    lh_cu.TenLop as LopCu_HK2,
    lh_moi.TenLop as LopMoi_HK1,
    CASE 
        WHEN lh_moi.MaKhoi > lh_cu.MaKhoi THEN 'LÊN LỚP'
        WHEN lh_moi.MaKhoi = lh_cu.MaKhoi THEN 'Ở LẠI'
        ELSE 'LỖI'
    END as KetQua
FROM HocSinh hs
JOIN PhanLop pl_cu ON hs.MaHocSinh = pl_cu.MaHocSinh AND pl_cu.MaHocKy = 2
JOIN LopHoc lh_cu ON pl_cu.MaLop = lh_cu.MaLop
LEFT JOIN PhanLop pl_moi ON hs.MaHocSinh = pl_moi.MaHocSinh AND pl_moi.MaHocKy = 3
LEFT JOIN LopHoc lh_moi ON pl_moi.MaLop = lh_moi.MaLop
WHERE hs.TrangThai = 'Đang học'
ORDER BY KetQua DESC, hs.MaHocSinh;

-- =====================================================================
-- 5. HỌC SINH KHÔNG ĐƯỢC PHÂN LỚP (LỖI)
-- =====================================================================

SELECT 
    hs.MaHocSinh,
    hs.HoTen,
    lh.TenLop as LopHK2,
    'CHƯA PHÂN LỚP HK1 NĂM SAU' as LyDo
FROM HocSinh hs
JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh AND pl.MaHocKy = 2
JOIN LopHoc lh ON pl.MaLop = lh.MaLop
WHERE hs.TrangThai = 'Đang học'
  AND NOT EXISTS (
      SELECT 1 FROM PhanLop pl2 
      WHERE pl2.MaHocSinh = hs.MaHocSinh 
        AND pl2.MaHocKy = 3
  )
ORDER BY hs.MaHocSinh;

-- =====================================================================
-- 6. HỌC SINH Ở LẠI CÓ GIỮ NGUYÊN LỚP KHÔNG?
-- =====================================================================

SELECT 
    hs.MaHocSinh,
    hs.HoTen,
    lh_cu.TenLop as LopCu,
    lh_moi.TenLop as LopMoi,
    CASE 
        WHEN lh_cu.MaLop = lh_moi.MaLop THEN '✓ Giữ nguyên lớp'
        ELSE '✗ ĐỔI LỚP (Sai!)'
    END as KiemTra
FROM HocSinh hs
JOIN PhanLop pl_cu ON hs.MaHocSinh = pl_cu.MaHocSinh AND pl_cu.MaHocKy = 2
JOIN LopHoc lh_cu ON pl_cu.MaLop = lh_cu.MaLop
JOIN PhanLop pl_moi ON hs.MaHocSinh = pl_moi.MaHocSinh AND pl_moi.MaHocKy = 3
JOIN LopHoc lh_moi ON pl_moi.MaLop = lh_moi.MaLop
WHERE hs.TrangThai = 'Đang học'
  AND lh_cu.MaKhoi = lh_moi.MaKhoi  -- Cùng khối = ở lại
ORDER BY KiemTra DESC, hs.MaHocSinh;
