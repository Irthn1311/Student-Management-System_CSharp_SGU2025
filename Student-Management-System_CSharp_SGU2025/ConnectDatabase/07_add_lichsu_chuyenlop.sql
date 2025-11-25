-- =====================================================================
-- FILE 07: THÊM BẢNG LỊCH SỬ CHUYỂN LỚP
-- Mục đích: Lưu lịch sử chuyển lớp của học sinh
-- Chạy: mysql -u root -p < 07_add_lichsu_chuyenlop.sql
-- =====================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
USE QuanLyHocSinh;

-- Tạo bảng lịch sử chuyển lớp
CREATE TABLE IF NOT EXISTS LichSuChuyenLop (
    MaLichSu INT PRIMARY KEY AUTO_INCREMENT,
    MaHocSinh INT NOT NULL,
    MaLopCu INT NOT NULL,
    MaLopMoi INT NOT NULL,
    MaHocKy INT NOT NULL,
    NgayChuyen DATE NOT NULL DEFAULT (CURRENT_DATE),
    LyDo NVARCHAR(500),
    NguoiThucHien VARCHAR(20),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh) ON DELETE CASCADE,
    FOREIGN KEY (MaLopCu) REFERENCES LopHoc(MaLop) ON DELETE CASCADE,
    FOREIGN KEY (MaLopMoi) REFERENCES LopHoc(MaLop) ON DELETE CASCADE,
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy) ON DELETE CASCADE,
    FOREIGN KEY (NguoiThucHien) REFERENCES NguoiDung(TenDangNhap) ON DELETE SET NULL,
    INDEX idx_hocsinh (MaHocSinh),
    INDEX idx_hocky (MaHocKy),
    INDEX idx_ngaychuyen (NgayChuyen)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

SET FOREIGN_KEY_CHECKS = 1;

SELECT 'Bảng LichSuChuyenLop đã được tạo thành công!' AS Status;

