-- Migration script for MonHoc_NamHoc_Khoi table
-- This script migrates existing data to the new structure

-- Step 0: Add AUTO_INCREMENT to MaMonHoc if not exists
-- Check if MaMonHoc already has AUTO_INCREMENT
SET @hasAutoIncrement = (
    SELECT COUNT(*) 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_SCHEMA = DATABASE() 
    AND TABLE_NAME = 'MonHoc' 
    AND COLUMN_NAME = 'MaMonHoc'
    AND EXTRA LIKE '%auto_increment%'
);

-- If no AUTO_INCREMENT, add it
SET @sql = IF(@hasAutoIncrement = 0,
    'ALTER TABLE MonHoc MODIFY COLUMN MaMonHoc INT AUTO_INCREMENT;',
    'SELECT ''MaMonHoc already has AUTO_INCREMENT'' AS Result;'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Step 1: Create MonHoc_NamHoc_Khoi table (if not exists)
CREATE TABLE IF NOT EXISTS MonHoc_NamHoc_Khoi (
    MaMonHoc INT,
    MaNamHoc VARCHAR(10),
    MaKhoi INT,
    PRIMARY KEY (MaMonHoc, MaNamHoc, MaKhoi),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc) ON DELETE CASCADE,
    FOREIGN KEY (MaNamHoc) REFERENCES NamHoc(MaNamHoc) ON DELETE CASCADE,
    FOREIGN KEY (MaKhoi) REFERENCES KhoiLop(MaKhoi) ON DELETE CASCADE,
    INDEX idx_namhoc_khoi (MaNamHoc, MaKhoi)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Step 2: Add NamHocBatDau column to MonHoc table (if not exists)
-- Note: This assumes the column doesn't exist. If it exists, this will fail gracefully.
SET @sql = (SELECT IF(
    (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
     WHERE TABLE_SCHEMA = DATABASE() 
     AND TABLE_NAME = 'MonHoc' 
     AND COLUMN_NAME = 'NamHocBatDau') > 0,
    'SELECT ''Column NamHocBatDau already exists'' AS Result;',
    'ALTER TABLE MonHoc ADD COLUMN NamHocBatDau VARCHAR(10) NULL COMMENT ''Năm học bắt đầu áp dụng môn học này'', ADD FOREIGN KEY (NamHocBatDau) REFERENCES NamHoc(MaNamHoc) ON DELETE SET NULL;'
));
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Step 3: Populate MonHoc_NamHoc_Khoi with existing subjects for all years and grades
-- This ensures backward compatibility - all existing subjects are available for all years/grades
INSERT INTO MonHoc_NamHoc_Khoi (MaMonHoc, MaNamHoc, MaKhoi)
SELECT DISTINCT 
    mh.MaMonHoc,
    nh.MaNamHoc,
    kl.MaKhoi
FROM MonHoc mh
CROSS JOIN NamHoc nh
CROSS JOIN KhoiLop kl
WHERE NOT EXISTS (
    SELECT 1 FROM MonHoc_NamHoc_Khoi mhnk
    WHERE mhnk.MaMonHoc = mh.MaMonHoc
    AND mhnk.MaNamHoc = nh.MaNamHoc
    AND mhnk.MaKhoi = kl.MaKhoi
);

-- Step 4: Set NamHocBatDau for existing subjects to the earliest year in database
UPDATE MonHoc mh
SET mh.NamHocBatDau = (
    SELECT MIN(nh.MaNamHoc)
    FROM NamHoc nh
)
WHERE mh.NamHocBatDau IS NULL;

SELECT 'Migration completed successfully' AS Status;

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

