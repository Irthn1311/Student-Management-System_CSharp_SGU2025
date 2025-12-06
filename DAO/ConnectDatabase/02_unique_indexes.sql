-- =====================================================================
-- FILE 02: UNIQUE INDEXES AND CONSTRAINTS
-- Mục đích: Tạo các unique constraints và indexes nâng cao cho TKB
-- Chạy: mysql -u root -p < 02_unique_indexes.sql
-- =====================================================================

USE QuanLyHocSinh;

-- =====================================================================
-- UNIQUE CONSTRAINTS CHO THỜI KHÓA BIỂU
-- =====================================================================

-- Kiểm tra và tạo unique index cho lớp học (không thể có 2 môn trùng thứ/tiết)
SET @index_exists_lop = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'ThoiKhoaBieu'
      AND INDEX_NAME = 'ux_tkb_lop_slot'
);

SET @sql_create_lop_index = IF(
    @index_exists_lop = 0,
    'CREATE UNIQUE INDEX ux_tkb_lop_slot ON ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau)',
    'SELECT "Index ux_tkb_lop_slot already exists" AS Status'
);

PREPARE stmt_lop FROM @sql_create_lop_index;
EXECUTE stmt_lop;
DEALLOCATE PREPARE stmt_lop;

-- Kiểm tra và tạo unique index cho giáo viên (không thể dạy 2 lớp cùng lúc)
-- Lưu ý: Do cấu trúc hiện tại dùng MaPhanCong làm FK, constraint này sẽ được enforce
-- thông qua validation logic trong C# code hoặc trigger
SET @index_exists_gv = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'ThoiKhoaBieu'
      AND INDEX_NAME = 'ux_tkb_gv_slot'
);

SET @sql_create_gv_index = IF(
    @index_exists_gv = 0,
    'CREATE INDEX idx_tkb_gv_slot ON ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau)',
    'SELECT "Index idx_tkb_gv_slot already exists" AS Status'
);

PREPARE stmt_gv FROM @sql_create_gv_index;
EXECUTE stmt_gv;
DEALLOCATE PREPARE stmt_gv;

-- =====================================================================
-- ADDITIONAL INDEXES CHO PERFORMANCE
-- =====================================================================

-- Index cho tìm kiếm theo thứ trong tuần
SET @index_exists_thu = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'ThoiKhoaBieu'
      AND INDEX_NAME = 'idx_thu_tiet'
);

SET @sql_create_thu_index = IF(
    @index_exists_thu = 0,
    'CREATE INDEX idx_thu_tiet ON ThoiKhoaBieu (ThuTrongTuan, TietBatDau)',
    'SELECT "Index idx_thu_tiet already exists" AS Status'
);

PREPARE stmt_thu FROM @sql_create_thu_index;
EXECUTE stmt_thu;
DEALLOCATE PREPARE stmt_thu;

-- Index cho tìm kiếm theo phòng học
SET @index_exists_phong = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'ThoiKhoaBieu'
      AND INDEX_NAME = 'idx_phong_hoc'
);

SET @sql_create_phong_index = IF(
    @index_exists_phong = 0,
    'CREATE INDEX idx_phong_hoc ON ThoiKhoaBieu (PhongHoc)',
    'SELECT "Index idx_phong_hoc already exists" AS Status'
);

PREPARE stmt_phong FROM @sql_create_phong_index;
EXECUTE stmt_phong;
DEALLOCATE PREPARE stmt_phong;

-- =====================================================================
-- UNIQUE CONSTRAINTS CHO CÁC BẢNG KHÁC
-- =====================================================================

-- Đảm bảo không có học sinh nào được phân vào 2 lớp cùng học kỳ
SET @index_exists_phanlop = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'PhanLop'
      AND INDEX_NAME = 'ux_hs_hocky_unique'
);

SET @sql_create_phanlop_index = IF(
    @index_exists_phanlop = 0,
    'CREATE UNIQUE INDEX ux_hs_hocky_unique ON PhanLop (MaHocSinh, MaHocKy)',
    'SELECT "Index ux_hs_hocky_unique already exists" AS Status'
);

PREPARE stmt_phanlop FROM @sql_create_phanlop_index;
EXECUTE stmt_phanlop;
DEALLOCATE PREPARE stmt_phanlop;

-- =====================================================================
-- PERFORMANCE INDEXES CHO CÁC BẢNG QUAN TRỌNG
-- =====================================================================

-- Index cho DiemSo để tìm kiếm nhanh
SET @index_exists_diem = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'DiemSo'
      AND INDEX_NAME = 'idx_diem_hocky'
);

SET @sql_create_diem_index = IF(
    @index_exists_diem = 0,
    'CREATE INDEX idx_diem_hocky ON DiemSo (MaHocKy, MaMonHoc)',
    'SELECT "Index idx_diem_hocky already exists" AS Status'
);

PREPARE stmt_diem FROM @sql_create_diem_index;
EXECUTE stmt_diem;
DEALLOCATE PREPARE stmt_diem;

-- Index cho HanhKiem
SET @index_exists_hanhkiem = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'HanhKiem'
      AND INDEX_NAME = 'idx_hanhkiem_hocky'
);

SET @sql_create_hanhkiem_index = IF(
    @index_exists_hanhkiem = 0,
    'CREATE INDEX idx_hanhkiem_hocky ON HanhKiem (MaHocKy)',
    'SELECT "Index idx_hanhkiem_hocky already exists" AS Status'
);

PREPARE stmt_hanhkiem FROM @sql_create_hanhkiem_index;
EXECUTE stmt_hanhkiem;
DEALLOCATE PREPARE stmt_hanhkiem;

-- Index cho ThongBao
SET @index_exists_thongbao = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'ThongBao'
      AND INDEX_NAME = 'idx_thongbao_ngay'
);

SET @sql_create_thongbao_index = IF(
    @index_exists_thongbao = 0,
    'CREATE INDEX idx_thongbao_ngay ON ThongBao (NgayTao, LoaiThongBao)',
    'SELECT "Index idx_thongbao_ngay already exists" AS Status'
);

PREPARE stmt_thongbao FROM @sql_create_thongbao_index;
EXECUTE stmt_thongbao;
DEALLOCATE PREPARE stmt_thongbao;

-- =====================================================================
-- INDEXES CHO BẢNG YÊU CẦU CHUYỂN LỚP
-- =====================================================================

-- Index cho tìm kiếm yêu cầu theo học sinh và học kỳ
SET @index_exists_yc_hs_hk = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'YeuCauChuyenLop'
      AND INDEX_NAME = 'idx_yc_hs_hocky'
);

SET @sql_create_yc_hs_hk_index = IF(
    @index_exists_yc_hs_hk = 0,
    'CREATE INDEX idx_yc_hs_hocky ON YeuCauChuyenLop (MaHocSinh, MaHocKy)',
    'SELECT "Index idx_yc_hs_hocky already exists" AS Status'
);

PREPARE stmt_yc_hs_hk FROM @sql_create_yc_hs_hk_index;
EXECUTE stmt_yc_hs_hk;
DEALLOCATE PREPARE stmt_yc_hs_hk;

-- Index cho tìm kiếm yêu cầu theo lớp hiện tại
SET @index_exists_yc_lop = (
    SELECT COUNT(*)
    FROM information_schema.STATISTICS
    WHERE TABLE_SCHEMA = 'QuanLyHocSinh'
      AND TABLE_NAME = 'YeuCauChuyenLop'
      AND INDEX_NAME = 'idx_yc_lophientai'
);

SET @sql_create_yc_lop_index = IF(
    @index_exists_yc_lop = 0,
    'CREATE INDEX idx_yc_lophientai ON YeuCauChuyenLop (MaLopHienTai, TrangThai)',
    'SELECT "Index idx_yc_lophientai already exists" AS Status'
);

PREPARE stmt_yc_lop FROM @sql_create_yc_lop_index;
EXECUTE stmt_yc_lop;
DEALLOCATE PREPARE stmt_yc_lop;

-- =====================================================================
-- LƯU Ý QUAN TRỌNG:
-- =====================================================================
-- 1. Unique constraint cho giáo viên không dạy trùng giờ sẽ được enforce
--    thông qua validation logic trong C# code hoặc trigger
-- 2. Các index được tạo với IF NOT EXISTS để tránh lỗi khi chạy lại
-- 3. Index ux_hs_hocky_unique đảm bảo mỗi học sinh chỉ thuộc 1 lớp trong 1 học kỳ
-- 4. Indexes cho YeuCauChuyenLop giúp tối ưu truy vấn theo học sinh, lớp và trạng thái
-- =====================================================================

SELECT 'Unique indexes and constraints creation completed' AS Status;
