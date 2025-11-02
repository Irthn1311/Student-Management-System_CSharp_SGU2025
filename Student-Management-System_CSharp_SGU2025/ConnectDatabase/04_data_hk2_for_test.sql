-- =====================================================================
-- FILE 04: DỮ LIỆU MẪU HỌC KỲ 2 (ĐỂ TEST KỊCH BẢN HK2 -> HK1 NĂM SAU)
-- Mục đích: Tạo dữ liệu điểm, hạnh kiểm, xếp loại HK2 để test phân lớp lên lớp
-- Chạy: mysql -u root -p QuanLyHocSinh < 04_data_hk2_for_test.sql
-- =====================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
USE QuanLyHocSinh;
START TRANSACTION;

-- =====================================================================
-- PHẦN 0: XÓA DỮ LIỆU HK2 CŨ (NẾU CÓ)
-- =====================================================================

DELETE FROM PhanLop WHERE MaHocKy = 2;
DELETE FROM DiemSo WHERE MaHocKy = 2;
DELETE FROM HanhKiem WHERE MaHocKy = 2;
DELETE FROM XepLoai WHERE MaHocKy = 2;

SELECT 'Da xoa du lieu HK2 cu (neu co)' AS Status;

-- =====================================================================
-- PHẦN 1: PHÂN LỚP CHO HỌC KỲ 2
-- =====================================================================
-- Giữ nguyên lớp từ HK1 sang HK2

INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy)
SELECT 
    MaHocSinh,
    MaLop,
    2 as MaHocKy  -- Học kỳ 2
FROM PhanLop
WHERE MaHocKy = 1;

SELECT 'Da phan lop HK2 cho 475 hoc sinh (giu nguyen lop HK1)' AS Status;

-- =====================================================================
-- PHẦN 2: ĐIỂM SỐ HỌC KỲ 2
-- =====================================================================
-- Tạo điểm HK2 với phân bố:
-- - 80% học sinh có điểm >= 5.0 (được lên lớp)
-- - 20% học sinh có điểm < 5.0 (ở lại)

INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, DiemThuongXuyen, DiemGiuaKy, DiemCuoiKy, DiemTrungBinh)
SELECT 
    pl.MaHocSinh,
    mh.MaMonHoc,
    2 as MaHocKy,
    -- 80% đầu có điểm cao (5.0-10), 20% cuối có điểm thấp (3.0-6.0)
    ROUND(
        IF(pl.MaHocSinh MOD 5 = 0, 
            3.0 + (RAND() * 3.0),  -- 20% có điểm thấp 3.0-6.0
            5.0 + (RAND() * 5.0)   -- 80% có điểm cao 5.0-10.0
        ), 1
    ) as DiemThuongXuyen,
    ROUND(
        IF(pl.MaHocSinh MOD 5 = 0, 
            3.0 + (RAND() * 3.0), 
            5.0 + (RAND() * 5.0)
        ), 1
    ) as DiemGiuaKy,
    ROUND(
        IF(pl.MaHocSinh MOD 5 = 0, 
            3.0 + (RAND() * 3.0), 
            5.0 + (RAND() * 5.0)
        ), 1
    ) as DiemCuoiKy,
    ROUND(
        IF(pl.MaHocSinh MOD 5 = 0, 
            3.0 + (RAND() * 3.0), 
            5.0 + (RAND() * 5.0)
        ), 1
    ) as DiemTrungBinh
FROM PhanLop pl
CROSS JOIN MonHoc mh
WHERE pl.MaHocKy = 2;

SELECT 'Diem so HK2 da duoc tao (6,175 ban ghi)' AS Status;

-- =====================================================================
-- PHẦN 3: HẠNH KIỂM HỌC KỲ 2
-- =====================================================================
-- Phân bố hạnh kiểm:
-- - 70% Tốt/Khá (được lên lớp)
-- - 20% Trung bình (được lên lớp nếu điểm tốt)
-- - 10% Yếu (ở lại)

INSERT INTO HanhKiem (MaHocSinh, MaHocKy, XepLoai, NhanXet)
SELECT 
    pl.MaHocSinh,
    2 as MaHocKy,
    CASE 
        WHEN pl.MaHocSinh MOD 10 < 4 THEN 'Tốt'           -- 40%
        WHEN pl.MaHocSinh MOD 10 < 7 THEN 'Khá'           -- 30%
        WHEN pl.MaHocSinh MOD 10 < 9 THEN 'Trung bình'    -- 20%
        ELSE 'Yếu'                                        -- 10%
    END as XepLoai,
    'HK2 - Tự động tạo' as NhanXet
FROM PhanLop pl
WHERE pl.MaHocKy = 2;

SELECT 'Hanh kiem HK2 da duoc tao (475 ban ghi)' AS Status;

-- =====================================================================
-- PHẦN 4: XẾP LOẠI HỌC KỲ 2
-- =====================================================================
-- Xếp loại học lực tương ứng với điểm

INSERT INTO XepLoai (MaHocSinh, MaHocKy, HocLuc, GhiChu)
SELECT 
    pl.MaHocSinh,
    2 as MaHocKy,
    CASE 
        WHEN pl.MaHocSinh MOD 5 = 0 THEN 'Yếu'            -- 20% yếu (ở lại)
        WHEN pl.MaHocSinh MOD 4 = 0 THEN 'Trung bình'     -- 20%
        WHEN pl.MaHocSinh MOD 3 = 0 THEN 'Khá'            -- 27%
        ELSE 'Giỏi'                                       -- 33%
    END as HocLuc,
    'HK2 - Tự động tạo' as GhiChu
FROM PhanLop pl
WHERE pl.MaHocKy = 2;

SELECT 'Xep loai HK2 da duoc tao (475 ban ghi)' AS Status;

-- =====================================================================
-- HOÀN TẤT
-- =====================================================================

SET FOREIGN_KEY_CHECKS = 1;
COMMIT;

SELECT '=== IMPORT DU LIEU HK2 HOAN THANH ===' AS Status;
SELECT '475 hoc sinh duoc phan lop HK2' AS PhanLop;
SELECT '6,175 ban ghi diem HK2 (475 HS × 13 mon)' AS DiemSo;
SELECT '475 ban ghi hanh kiem HK2' AS HanhKiem;
SELECT '475 ban ghi xep loai HK2' AS XepLoai;
SELECT 'DU KIEN: ~380 HS len lop (80%), ~95 HS o lai (20%)' AS DuKien;
SELECT 'SAN SANG TEST KICH BAN HK2 -> HK1 NAM SAU!' AS KetLuan;
