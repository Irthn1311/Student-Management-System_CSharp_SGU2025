-- =====================================================================
-- SCRIPT TẠO UNIQUE INDEXES CHO TKB
-- Mục đích: Enforce hard constraints cho Thời khóa biểu
-- =====================================================================

USE QuanLyHocSinh;

-- Kiểm tra và tạo index ux_tkb_lop (Lớp không thể có 2 môn trùng Học kỳ, Thứ, Tiết)
SET @index_exists = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'ThoiKhoaBieu'
      AND INDEX_NAME = 'ux_tkb_lop'
);

SET @sql_create_lop_index = IF(
    @index_exists = 0,
    'CREATE UNIQUE INDEX ux_tkb_lop ON ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau)',
    'SELECT "Index ux_tkb_lop already exists" AS Status'
);

PREPARE stmt FROM @sql_create_lop_index;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Kiểm tra và tạo index ux_tkb_gv (GV không thể dạy 2 lớp trùng Học kỳ, Thứ, Tiết)
SET @index_exists_gv = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'ThoiKhoaBieu'
      AND INDEX_NAME = 'ux_tkb_gv'
);

SET @sql_create_gv_index = IF(
    @index_exists_gv = 0,
    'CREATE UNIQUE INDEX ux_tkb_gv ON ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau)',
    'SELECT "Index ux_tkb_gv already exists" AS Status'
);

PREPARE stmt2 FROM @sql_create_gv_index;
EXECUTE stmt2;
DEALLOCATE PREPARE stmt2;

-- =====================================================================
-- LƯU Ý:
-- Do cấu trúc ThoiKhoaBieu hiện tại dùng MaPhanCong làm FK,
-- các unique constraints sẽ được enforce qua bảng tạm TKB_Temp
-- và validation logic trong C# code (HasConflict method).
-- 
-- Nếu muốn enforce trực tiếp trên ThoiKhoaBieu, cần thêm các cột:
-- - MaHocKy
-- - MaLop
-- - MaGiaoVien
-- vào bảng ThoiKhoaBieu (denormalized) hoặc sử dụng trigger.
-- =====================================================================

SELECT 'Unique indexes verification/creation completed' AS Result;

