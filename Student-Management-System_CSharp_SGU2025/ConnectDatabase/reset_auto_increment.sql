-- =====================================================================
-- Reset AUTO_INCREMENT về giá trị tiếp theo sau MAX hiện tại
-- Chạy script này nếu muốn AUTO_INCREMENT bắt đầu từ 501
-- =====================================================================

USE QuanLyHocSinh;

-- 1. Kiểm tra giá trị MAX hiện tại
SELECT MAX(MaHocSinh) as MaxHS FROM HocSinh;
SELECT MAX(MaPhuHuynh) as MaxPH FROM PhuHuynh;

-- 2. Reset AUTO_INCREMENT về 501 (hoặc giá trị bạn muốn)
-- Lưu ý: Chỉ chạy nếu bạn chắc chắn không có dữ liệu từ 501 trở đi
ALTER TABLE HocSinh AUTO_INCREMENT = 501;
ALTER TABLE PhuHuynh AUTO_INCREMENT = 501;

-- 3. Kiểm tra lại
SHOW TABLE STATUS WHERE Name = 'HocSinh';
SHOW TABLE STATUS WHERE Name = 'PhuHuynh';

-- =====================================================================
-- HOẶC nếu bạn muốn reset về giá trị MAX + 1 (an toàn hơn)
-- =====================================================================
-- SET @max_hs = (SELECT COALESCE(MAX(MaHocSinh), 0) + 1 FROM HocSinh);
-- SET @max_ph = (SELECT COALESCE(MAX(MaPhuHuynh), 0) + 1 FROM PhuHuynh);
-- SET @sql1 = CONCAT('ALTER TABLE HocSinh AUTO_INCREMENT = ', @max_hs);
-- SET @sql2 = CONCAT('ALTER TABLE PhuHuynh AUTO_INCREMENT = ', @max_ph);
-- PREPARE stmt1 FROM @sql1; EXECUTE stmt1; DEALLOCATE PREPARE stmt1;
-- PREPARE stmt2 FROM @sql2; EXECUTE stmt2; DEALLOCATE PREPARE stmt2;
