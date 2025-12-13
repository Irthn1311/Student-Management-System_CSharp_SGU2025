-- Script để fix lỗi AUTO_INCREMENT cho bảng MonHoc
-- Chạy script này nếu gặp lỗi "Field 'MaMonHoc' doesn't have a default value"

USE QuanLyHocSinh;

-- Kiểm tra và thêm AUTO_INCREMENT cho MaMonHoc
-- Lưu ý: Nếu bảng đã có dữ liệu, cần đảm bảo không có giá trị NULL hoặc trùng lặp

-- Bước 1: Kiểm tra xem MaMonHoc đã có AUTO_INCREMENT chưa
SELECT 
    COLUMN_NAME,
    COLUMN_TYPE,
    EXTRA
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
AND TABLE_NAME = 'MonHoc'
AND COLUMN_NAME = 'MaMonHoc';

-- Bước 2: Thêm AUTO_INCREMENT (chỉ chạy nếu chưa có)
-- Nếu bảng đã có dữ liệu, cần set giá trị AUTO_INCREMENT tiếp theo
SET @maxId = (SELECT IFNULL(MAX(MaMonHoc), 0) FROM MonHoc);
SET @sql = CONCAT('ALTER TABLE MonHoc MODIFY COLUMN MaMonHoc INT AUTO_INCREMENT, AUTO_INCREMENT = ', @maxId + 1);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Kiểm tra lại
SELECT 
    COLUMN_NAME,
    COLUMN_TYPE,
    EXTRA
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
AND TABLE_NAME = 'MonHoc'
AND COLUMN_NAME = 'MaMonHoc';

SELECT 'Fix completed! MaMonHoc now has AUTO_INCREMENT' AS Status;

