-- =====================================================
-- Script: Xóa và sửa các mối quan hệ không hợp lệ
-- Mục đích: 
--   1. Xóa các record có MoiQuanHe = "Chọn mối quan hệ"
--   2. Chuẩn hóa MoiQuanHe chỉ còn: Cha, Mẹ, Ông, Bà, Người giám hộ
-- 
-- LƯU Ý: 
--   - File 03_sample_seed.sql có nhiều dòng INSERT với 'Cha/Mẹ'
--   - Nếu chạy lại seed data, cần sửa tất cả 'Cha/Mẹ' thành 'Cha' hoặc 'Mẹ'
--   - Hoặc chạy script này sau khi seed để clean dữ liệu
-- =====================================================

USE QuanLyHocSinh;

-- Xem các record không hợp lệ trước khi xóa/sửa
SELECT 
    hs.MaHocSinh,
    hs.HoTen AS HocSinh,
    ph.MaPhuHuynh,
    ph.HoTen AS PhuHuynh,
    hph.MoiQuanHe
FROM HocSinhPhuHuynh hph
INNER JOIN HocSinh hs ON hph.MaHocSinh = hs.MaHocSinh
INNER JOIN PhuHuynh ph ON hph.MaPhuHuynh = ph.MaPhuHuynh
WHERE hph.MoiQuanHe = 'Chọn mối quan hệ' 
   OR hph.MoiQuanHe = 'Cha/Mẹ'
   OR hph.MoiQuanHe NOT IN ('Cha', 'Mẹ', 'Ông', 'Bà', 'Người giám hộ');

-- 1. Xóa các record có "Chọn mối quan hệ"
DELETE FROM HocSinhPhuHuynh
WHERE MoiQuanHe = 'Chọn mối quan hệ';

-- 2. Sửa "Cha/Mẹ" thành "Cha" hoặc "Mẹ" (mặc định là "Cha")
UPDATE HocSinhPhuHuynh
SET MoiQuanHe = 'Cha'
WHERE MoiQuanHe = 'Cha/Mẹ';

-- 3. Kiểm tra lại sau khi clean
SELECT COUNT(*) AS SoRecordKhongHopLe
FROM HocSinhPhuHuynh
WHERE MoiQuanHe NOT IN ('Cha', 'Mẹ', 'Ông', 'Bà', 'Người giám hộ');

-- Hiển thị tất cả mối quan hệ hợp lệ còn lại
SELECT 
    hs.MaHocSinh,
    hs.HoTen AS HocSinh,
    ph.MaPhuHuynh,
    ph.HoTen AS PhuHuynh,
    hph.MoiQuanHe
FROM HocSinhPhuHuynh hph
INNER JOIN HocSinh hs ON hph.MaHocSinh = hs.MaHocSinh
INNER JOIN PhuHuynh ph ON hph.MaPhuHuynh = ph.MaPhuHuynh
ORDER BY hs.MaHocSinh;

-- Thống kê số lượng theo từng loại mối quan hệ
SELECT 
    MoiQuanHe,
    COUNT(*) AS SoLuong
FROM HocSinhPhuHuynh
GROUP BY MoiQuanHe
ORDER BY SoLuong DESC;
