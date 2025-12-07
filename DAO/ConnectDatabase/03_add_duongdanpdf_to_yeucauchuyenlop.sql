-- Script để thêm cột DuongDanPDF vào bảng YeuCauChuyenLop
-- Chạy script này để cập nhật database schema

USE QuanLyHocSinh;

-- Thêm cột DuongDanPDF vào bảng YeuCauChuyenLop
ALTER TABLE YeuCauChuyenLop 
ADD COLUMN DuongDanPDF VARCHAR(500) NULL 
COMMENT 'Đường dẫn file PDF của yêu cầu chuyển lớp' 
AFTER MaLopDuocDuyet;

-- Thêm index để tối ưu tìm kiếm (nếu cần)
-- ALTER TABLE YeuCauChuyenLop ADD INDEX idx_duongdanpdf (DuongDanPDF(255));

