-- =====================================================================
-- FILE 05: THÊM DỮ LIỆU THỜI KHÓA BIỂU VÀ PHÂN CÔNG ĐẦY ĐỦ
-- Mục đích: Thêm dữ liệu mẫu đầy đủ cho PhanCongGiangDay và ThoiKhoaBieu
-- Chạy: mysql -u root -p < 05_add_tkb_phancong_data.sql
-- =====================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
USE QuanLyHocSinh;
START TRANSACTION;

-- =====================================================================
-- PHẦN 1: PHÂN CÔNG GIẢNG DẠY CHO CÁC LỚP
-- =====================================================================

-- Xóa dữ liệu cũ (nếu có)
DELETE FROM ThoiKhoaBieu;
DELETE FROM PhanCongGiangDay;

-- Phân công giảng dạy cho các lớp khối 10 (Lớp 1-5: 10A1-10A5)
-- Mỗi lớp có 13 môn học

-- Lớp 10A1 (MaLop = 1) - Học kỳ 1 (MaHocKy = 1)
INSERT INTO PhanCongGiangDay (MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) VALUES
(1, 'GV001', 2, 1, '2025-09-01', '2026-01-15'), -- Toán
(1, 'GV008', 1, 1, '2025-09-01', '2026-01-15'), -- Văn
(1, 'GV015', 3, 1, '2025-09-01', '2026-01-15'), -- Anh
(1, 'GV021', 7, 1, '2025-09-01', '2026-01-15'), -- Lý
(1, 'GV026', 8, 1, '2025-09-01', '2026-01-15'), -- Hóa
(1, 'GV031', 9, 1, '2025-09-01', '2026-01-15'), -- Sinh
(1, 'GV035', 4, 1, '2025-09-01', '2026-01-15'), -- Sử
(1, 'GV039', 5, 1, '2025-09-01', '2026-01-15'), -- Địa
(1, 'GV043', 6, 1, '2025-09-01', '2026-01-15'), -- GDCD
(1, 'GV047', 10, 1, '2025-09-01', '2026-01-15'), -- Công nghệ
(1, 'GV051', 11, 1, '2025-09-01', '2026-01-15'), -- Tin học
(1, 'GV056', 12, 1, '2025-09-01', '2026-01-15'), -- Thể dục
(1, 'GV060', 13, 1, '2025-09-01', '2026-01-15'); -- GDQP-AN

-- Lớp 10A2 (MaLop = 2) - Học kỳ 1
INSERT INTO PhanCongGiangDay (MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) VALUES
(2, 'GV002', 2, 1, '2025-09-01', '2026-01-15'), -- Toán
(2, 'GV009', 1, 1, '2025-09-01', '2026-01-15'), -- Văn
(2, 'GV016', 3, 1, '2025-09-01', '2026-01-15'), -- Anh
(2, 'GV022', 7, 1, '2025-09-01', '2026-01-15'), -- Lý
(2, 'GV027', 8, 1, '2025-09-01', '2026-01-15'), -- Hóa
(2, 'GV032', 9, 1, '2025-09-01', '2026-01-15'), -- Sinh
(2, 'GV036', 4, 1, '2025-09-01', '2026-01-15'), -- Sử
(2, 'GV040', 5, 1, '2025-09-01', '2026-01-15'), -- Địa
(2, 'GV044', 6, 1, '2025-09-01', '2026-01-15'), -- GDCD
(2, 'GV048', 10, 1, '2025-09-01', '2026-01-15'), -- Công nghệ
(2, 'GV052', 11, 1, '2025-09-01', '2026-01-15'), -- Tin học
(2, 'GV057', 12, 1, '2025-09-01', '2026-01-15'), -- Thể dục
(2, 'GV061', 13, 1, '2025-09-01', '2026-01-15'); -- GDQP-AN

-- Lớp 10A3 (MaLop = 3) - Học kỳ 1
INSERT INTO PhanCongGiangDay (MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) VALUES
(3, 'GV003', 2, 1, '2025-09-01', '2026-01-15'), -- Toán
(3, 'GV010', 1, 1, '2025-09-01', '2026-01-15'), -- Văn
(3, 'GV017', 3, 1, '2025-09-01', '2026-01-15'), -- Anh
(3, 'GV023', 7, 1, '2025-09-01', '2026-01-15'), -- Lý
(3, 'GV028', 8, 1, '2025-09-01', '2026-01-15'), -- Hóa
(3, 'GV033', 9, 1, '2025-09-01', '2026-01-15'), -- Sinh
(3, 'GV037', 4, 1, '2025-09-01', '2026-01-15'), -- Sử
(3, 'GV041', 5, 1, '2025-09-01', '2026-01-15'), -- Địa
(3, 'GV045', 6, 1, '2025-09-01', '2026-01-15'), -- GDCD
(3, 'GV049', 10, 1, '2025-09-01', '2026-01-15'), -- Công nghệ
(3, 'GV053', 11, 1, '2025-09-01', '2026-01-15'), -- Tin học
(3, 'GV058', 12, 1, '2025-09-01', '2026-01-15'), -- Thể dục
(3, 'GV062', 13, 1, '2025-09-01', '2026-01-15'); -- GDQP-AN

-- =====================================================================
-- PHẦN 2: THỜI KHÓA BIỂU CHO CÁC LỚP
-- =====================================================================

-- Lấy MaPhanCong từ PhanCongGiangDay vừa insert
-- Lớp 10A1 (MaLop = 1) - Thời khóa biểu đầy đủ (5 ngày x 5 tiết)

-- Thứ 2
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 1, 2, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 2 AND MaHocKy = 1; -- Toán
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 3, 2, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 1 AND MaHocKy = 1; -- Văn
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 5, 1, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 3 AND MaHocKy = 1; -- Anh

-- Thứ 3
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 1, 2, 'A102' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 7 AND MaHocKy = 1; -- Lý
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 3, 2, 'A102' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 8 AND MaHocKy = 1; -- Hóa
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 5, 1, 'A102' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 9 AND MaHocKy = 1; -- Sinh

-- Thứ 4
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 1, 2, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 4 AND MaHocKy = 1; -- Sử
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 3, 2, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 5 AND MaHocKy = 1; -- Địa
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 5, 1, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 6 AND MaHocKy = 1; -- GDCD

-- Thứ 5
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 1, 2, 'A103' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 10 AND MaHocKy = 1; -- Công nghệ
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 3, 2, 'A103' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 11 AND MaHocKy = 1; -- Tin học
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 5, 1, 'A103' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 12 AND MaHocKy = 1; -- Thể dục

-- Thứ 6
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 1, 1, 'A104' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 13 AND MaHocKy = 1; -- GDQP-AN
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 2, 2, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 2 AND MaHocKy = 1; -- Toán
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 4, 1, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 1 AND MaHocKy = 1; -- Văn
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 5, 1, 'A101' FROM PhanCongGiangDay WHERE MaLop = 1 AND MaMonHoc = 3 AND MaHocKy = 1; -- Anh

-- Lớp 10A2 (MaLop = 2) - Thời khóa biểu đầy đủ

-- Thứ 2
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 1, 2, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 1 AND MaHocKy = 1; -- Văn
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 3, 2, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 2 AND MaHocKy = 1; -- Toán
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 5, 1, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 3 AND MaHocKy = 1; -- Anh

-- Thứ 3
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 1, 2, 'A202' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 7 AND MaHocKy = 1; -- Lý
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 3, 2, 'A202' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 8 AND MaHocKy = 1; -- Hóa
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 5, 1, 'A202' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 9 AND MaHocKy = 1; -- Sinh

-- Thứ 4
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 1, 2, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 4 AND MaHocKy = 1; -- Sử
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 3, 2, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 5 AND MaHocKy = 1; -- Địa
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 5, 1, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 6 AND MaHocKy = 1; -- GDCD

-- Thứ 5
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 1, 2, 'A203' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 10 AND MaHocKy = 1; -- Công nghệ
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 3, 2, 'A203' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 11 AND MaHocKy = 1; -- Tin học
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 5, 1, 'A203' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 12 AND MaHocKy = 1; -- Thể dục

-- Thứ 6
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 1, 1, 'A204' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 13 AND MaHocKy = 1; -- GDQP-AN
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 2, 2, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 2 AND MaHocKy = 1; -- Toán
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 4, 1, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 1 AND MaHocKy = 1; -- Văn
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 5, 1, 'A201' FROM PhanCongGiangDay WHERE MaLop = 2 AND MaMonHoc = 3 AND MaHocKy = 1; -- Anh

-- Lớp 10A3 (MaLop = 3) - Thời khóa biểu đầy đủ

-- Thứ 2
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 1, 2, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 3 AND MaHocKy = 1; -- Anh
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 3, 2, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 2 AND MaHocKy = 1; -- Toán
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 2', 5, 1, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 1 AND MaHocKy = 1; -- Văn

-- Thứ 3
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 1, 2, 'A302' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 7 AND MaHocKy = 1; -- Lý
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 3, 2, 'A302' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 8 AND MaHocKy = 1; -- Hóa
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 3', 5, 1, 'A302' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 9 AND MaHocKy = 1; -- Sinh

-- Thứ 4
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 1, 2, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 4 AND MaHocKy = 1; -- Sử
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 3, 2, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 5 AND MaHocKy = 1; -- Địa
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 4', 5, 1, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 6 AND MaHocKy = 1; -- GDCD

-- Thứ 5
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 1, 2, 'A303' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 10 AND MaHocKy = 1; -- Công nghệ
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 3, 2, 'A303' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 11 AND MaHocKy = 1; -- Tin học
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 5', 5, 1, 'A303' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 12 AND MaHocKy = 1; -- Thể dục

-- Thứ 6
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 1, 1, 'A304' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 13 AND MaHocKy = 1; -- GDQP-AN
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 2, 2, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 2 AND MaHocKy = 1; -- Toán
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 4, 1, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 1 AND MaHocKy = 1; -- Văn
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) 
SELECT MaPhanCong, 'Thứ 6', 5, 1, 'A301' FROM PhanCongGiangDay WHERE MaLop = 3 AND MaMonHoc = 3 AND MaHocKy = 1; -- Anh

-- =====================================================================
-- HOÀN THÀNH
-- =====================================================================

COMMIT;
SET FOREIGN_KEY_CHECKS = 1;

-- Kiểm tra kết quả
SELECT 
    'Phân công giảng dạy' AS Loai,
    COUNT(*) AS SoLuong 
FROM PhanCongGiangDay
UNION ALL
SELECT 
    'Thời khóa biểu' AS Loai,
    COUNT(*) AS SoLuong 
FROM ThoiKhoaBieu;

SELECT 'Dữ liệu thời khóa biểu và phân công đã được thêm thành công!' AS Status;

