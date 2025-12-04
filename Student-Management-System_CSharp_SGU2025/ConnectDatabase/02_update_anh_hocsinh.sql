-- Script cập nhật ảnh đại diện cho học sinh
-- Phân bổ 4 ảnh (hs1.jpg, hs2.jpg, hs3.jpg, hs4.jpg) cho tất cả học sinh

USE QuanLyHocSinh;

-- Kiểm tra và thêm cột AnhDaiDien nếu chưa có
-- Sử dụng stored procedure để kiểm tra cột trước khi thêm
DELIMITER $$

DROP PROCEDURE IF EXISTS AddAnhDaiDienColumn$$

CREATE PROCEDURE AddAnhDaiDienColumn()
BEGIN
    DECLARE column_exists INT DEFAULT 0;
    
    -- Kiểm tra xem cột AnhDaiDien đã tồn tại chưa
    SELECT COUNT(*) INTO column_exists
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'HocSinh'
      AND COLUMN_NAME = 'AnhDaiDien';
    
    -- Nếu chưa có thì thêm cột
    IF column_exists = 0 THEN
        ALTER TABLE HocSinh 
        ADD COLUMN AnhDaiDien VARCHAR(255) NULL 
        COMMENT 'Đường dẫn ảnh đại diện của học sinh';
        SELECT 'Đã thêm cột AnhDaiDien' AS Result;
    ELSE
        SELECT 'Cột AnhDaiDien đã tồn tại' AS Result;
    END IF;
END$$

DELIMITER ;

-- Chạy procedure để thêm cột
CALL AddAnhDaiDienColumn();

-- Xóa procedure sau khi dùng
DROP PROCEDURE IF EXISTS AddAnhDaiDienColumn;

-- Cập nhật ảnh cho tất cả học sinh dựa trên MaHocSinh
-- Sử dụng modulo để phân bổ đều: (MaHocSinh - 1) % 4 + 1
UPDATE HocSinh 
SET AnhDaiDien = CONCAT('Images/Students/hs', ((MaHocSinh - 1) % 4) + 1, '.jpg')
WHERE AnhDaiDien IS NULL OR AnhDaiDien = '';

-- Kiểm tra kết quả
SELECT 
    MaHocSinh, 
    HoTen, 
    AnhDaiDien,
    CASE 
        WHEN ((MaHocSinh - 1) % 4) + 1 = 1 THEN 'hs1.jpg'
        WHEN ((MaHocSinh - 1) % 4) + 1 = 2 THEN 'hs2.jpg'
        WHEN ((MaHocSinh - 1) % 4) + 1 = 3 THEN 'hs3.jpg'
        WHEN ((MaHocSinh - 1) % 4) + 1 = 4 THEN 'hs4.jpg'
    END AS AnhDuocPhanBo
FROM HocSinh
ORDER BY MaHocSinh
LIMIT 20;

