-- =====================================================================
-- FILE 04: THÔNG BÁO SCHEMA - Mở rộng hệ thống thông báo
-- Mục đích: Thêm bảng và cập nhật schema cho module Thông Báo
-- Chạy: mysql -u root -p < 04_thongbao_schema.sql
-- =====================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
USE QuanLyHocSinh;

-- =====================================================================
-- PHẦN 1: CẬP NHẬT BẢNG THONGBAO
-- =====================================================================

-- Thêm các cột mới vào bảng ThongBao nếu chưa có
ALTER TABLE ThongBao 
ADD COLUMN IF NOT EXISTS PhamVi VARCHAR(20) DEFAULT 'ALL' 
    COMMENT 'Phạm vi gửi: ALL, VAI_TRO, LOP, KHOI, CA_NHAN',
ADD COLUMN IF NOT EXISTS MaVaiTroNhan VARCHAR(10) NULL 
    COMMENT 'Vai trò nhận thông báo (teacher, student, parent)',
ADD COLUMN IF NOT EXISTS MaLop INT NULL 
    COMMENT 'Mã lớp nhận thông báo (nếu PhamVi = LOP)',
ADD COLUMN IF NOT EXISTS MaKhoi INT NULL 
    COMMENT 'Mã khối nhận thông báo (nếu PhamVi = KHOI)',
ADD COLUMN IF NOT EXISTS DoUuTien VARCHAR(20) DEFAULT 'BINH_THUONG' 
    COMMENT 'Độ ưu tiên: BINH_THUONG, QUAN_TRONG, KHAN_CAP',
ADD COLUMN IF NOT EXISTS TrangThai VARCHAR(20) DEFAULT 'HIEN_THI' 
    COMMENT 'Trạng thái: HIEN_THI, AN, DA_XOA';

-- Thêm foreign key cho MaLop và MaKhoi
ALTER TABLE ThongBao
ADD CONSTRAINT IF NOT EXISTS fk_thongbao_lop 
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop) ON DELETE SET NULL,
ADD CONSTRAINT IF NOT EXISTS fk_thongbao_khoi 
    FOREIGN KEY (MaKhoi) REFERENCES KhoiLop(MaKhoi) ON DELETE SET NULL,
ADD CONSTRAINT IF NOT EXISTS fk_thongbao_vaitro 
    FOREIGN KEY (MaVaiTroNhan) REFERENCES VaiTro(MaVaiTro) ON DELETE SET NULL;

-- Index để tối ưu truy vấn
ALTER TABLE ThongBao
ADD INDEX IF NOT EXISTS idx_thongbao_phamvi (PhamVi),
ADD INDEX IF NOT EXISTS idx_thongbao_ngaytao (NgayTao),
ADD INDEX IF NOT EXISTS idx_thongbao_nguoitao (MaNguoiTao),
ADD INDEX IF NOT EXISTS idx_thongbao_loai (LoaiThongBao),
ADD INDEX IF NOT EXISTS idx_thongbao_trangthai (TrangThai);

-- =====================================================================
-- PHẦN 2: TẠO BẢNG NGUOINHAN THONGBAO (Theo dõi người nhận cụ thể)
-- =====================================================================

DROP TABLE IF EXISTS NguoiNhanThongBao;

CREATE TABLE NguoiNhanThongBao (
    MaThongBao INT NOT NULL,
    TenDangNhap VARCHAR(20) NOT NULL,
    DaDoc BOOLEAN DEFAULT FALSE COMMENT 'Đã đọc chưa',
    NgayDoc DATETIME NULL COMMENT 'Thời gian đọc',
    DaXoa BOOLEAN DEFAULT FALSE COMMENT 'Người nhận đã xóa khỏi hộp thư',
    NgayNhan DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Thời gian nhận',
    
    PRIMARY KEY (MaThongBao, TenDangNhap),
    
    CONSTRAINT fk_nguoinhan_thongbao 
        FOREIGN KEY (MaThongBao) REFERENCES ThongBao(MaThongBao) 
        ON DELETE CASCADE,
    CONSTRAINT fk_nguoinhan_nguoidung 
        FOREIGN KEY (TenDangNhap) REFERENCES NguoiDung(TenDangNhap) 
        ON DELETE CASCADE,
    
    INDEX idx_nguoinhan_tendangnhap (TenDangNhap),
    INDEX idx_nguoinhan_dadoc (DaDoc),
    INDEX idx_nguoinhan_ngaynhan (NgayNhan)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =====================================================================
-- PHẦN 3: TẠO BẢNG LOAI THONGBAO (Danh mục loại thông báo)
-- =====================================================================

DROP TABLE IF EXISTS LoaiThongBao;

CREATE TABLE LoaiThongBao (
    MaLoai VARCHAR(30) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255) NULL,
    MauSac VARCHAR(20) DEFAULT '#3B82F6' COMMENT 'Mã màu hiển thị',
    Icon VARCHAR(50) NULL COMMENT 'Tên icon hiển thị',
    ThuTu INT DEFAULT 0 COMMENT 'Thứ tự hiển thị'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Thêm dữ liệu loại thông báo mặc định
INSERT INTO LoaiThongBao (MaLoai, TenLoai, MoTa, MauSac, Icon, ThuTu) VALUES
('HE_THONG', 'Hệ thống', 'Thông báo từ hệ thống, quản trị viên', '#EF4444', 'notification_bell', 1),
('HOC_TAP', 'Học tập', 'Thông báo về điểm số, lịch thi, kết quả học tập', '#3B82F6', 'book', 2),
('HOP_PHU_HUYNH', 'Họp phụ huynh', 'Thông báo họp phụ huynh từ GVCN', '#10B981', 'people', 3),
('LICH_TRINH', 'Lịch trình', 'Thông báo về lịch học, thời khóa biểu', '#F59E0B', 'calendar', 4),
('KHEN_THUONG', 'Khen thưởng', 'Thông báo khen thưởng học sinh', '#8B5CF6', 'medal', 5),
('KY_LUAT', 'Kỷ luật', 'Thông báo kỷ luật học sinh', '#DC2626', 'shield', 6),
('SU_KIEN', 'Sự kiện', 'Thông báo sự kiện, hoạt động ngoại khóa', '#EC4899', 'badge', 7),
('CHUNG', 'Chung', 'Các thông báo khác', '#6B7280', 'notes', 8);

-- =====================================================================
-- PHẦN 4: DỮ LIỆU MẪU CHO THÔNG BÁO
-- =====================================================================

-- Xóa dữ liệu cũ (nếu có)
DELETE FROM NguoiNhanThongBao;
DELETE FROM ThongBao;

-- Thêm thông báo mẫu
INSERT INTO ThongBao (TieuDe, NoiDung, NgayTao, LoaiThongBao, DoiTuongNhan, NgayHetHan, MaNguoiTao, PhamVi, MaVaiTroNhan, MaLop, MaKhoi, DoUuTien, TrangThai) VALUES
-- Thông báo hệ thống (Admin -> Tất cả)
('Thông báo nghỉ lễ Quốc khánh 2/9', 
 'Toàn thể cán bộ, giáo viên và học sinh được nghỉ lễ Quốc khánh từ ngày 01/09/2025 đến 03/09/2025. Học sinh quay lại học bình thường vào ngày 04/09/2025.',
 '2025-08-25 08:00:00', 'HE_THONG', 'Toàn trường', '2025-09-03 23:59:59', 'admin', 'ALL', NULL, NULL, NULL, 'QUAN_TRONG', 'HIEN_THI'),

('Kế hoạch năm học 2025-2026',
 'Nhà trường xin thông báo kế hoạch năm học 2025-2026:\n- Khai giảng: 05/09/2025\n- Học kỳ I: 05/09/2025 - 15/01/2026\n- Học kỳ II: 16/01/2026 - 31/05/2026\n- Thi cuối năm: 15/05/2026 - 25/05/2026',
 '2025-08-20 09:00:00', 'HE_THONG', 'Toàn trường', NULL, 'admin', 'ALL', NULL, NULL, NULL, 'KHAN_CAP', 'HIEN_THI'),

-- Thông báo cho giáo viên
('Họp hội đồng sư phạm tháng 9',
 'Kính mời toàn thể giáo viên tham dự cuộc họp hội đồng sư phạm tháng 9/2025.\n- Thời gian: 14h00 ngày 10/09/2025\n- Địa điểm: Hội trường A\n- Nội dung: Triển khai kế hoạch năm học mới',
 '2025-09-05 10:00:00', 'HE_THONG', 'Giáo viên', '2025-09-10 14:00:00', 'admin', 'VAI_TRO', 'teacher', NULL, NULL, 'QUAN_TRONG', 'HIEN_THI'),

('Nhập điểm thường xuyên tháng 9',
 'Đề nghị các giáo viên bộ môn hoàn thành việc nhập điểm thường xuyên tháng 9 trước ngày 30/09/2025. Truy cập module Điểm số để thực hiện.',
 '2025-09-20 08:00:00', 'HOC_TAP', 'Giáo viên', '2025-09-30 23:59:59', 'admin', 'VAI_TRO', 'teacher', NULL, NULL, 'BINH_THUONG', 'HIEN_THI'),

-- Thông báo cho học sinh
('Lịch thi giữa kỳ I năm học 2025-2026',
 'Nhà trường thông báo lịch thi giữa kỳ I:\n- Thời gian: 20/10/2025 - 25/10/2025\n- Học sinh chuẩn bị ôn tập theo đề cương các môn đã phát.\n- Mang đầy đủ dụng cụ học tập và thẻ học sinh khi đi thi.',
 '2025-10-01 07:30:00', 'HOC_TAP', 'Học sinh', '2025-10-25 17:00:00', 'admin', 'VAI_TRO', 'student', NULL, NULL, 'QUAN_TRONG', 'HIEN_THI'),

('Đăng ký tham gia câu lạc bộ năm học 2025-2026',
 'Nhà trường mở đăng ký tham gia các câu lạc bộ:\n- CLB Tin học\n- CLB Tiếng Anh\n- CLB Văn học\n- CLB Thể thao\n\nHọc sinh đăng ký trực tiếp với giáo viên phụ trách hoặc tại phòng Giáo vụ.',
 '2025-09-10 08:00:00', 'SU_KIEN', 'Học sinh', '2025-09-25 17:00:00', 'admin', 'VAI_TRO', 'student', NULL, NULL, 'BINH_THUONG', 'HIEN_THI'),

-- Thông báo theo lớp (GVCN -> Lớp cụ thể)
('Họp phụ huynh lớp 10A1 học kỳ I',
 'Kính mời quý phụ huynh lớp 10A1 tham dự buổi họp phụ huynh đầu năm học.\n- Thời gian: 8h00 Chủ nhật, ngày 15/09/2025\n- Địa điểm: Phòng học 10A1\n- Nội dung: Thông báo kế hoạch học tập, giới thiệu ban cán sự lớp, thu các khoản đầu năm.',
 '2025-09-08 14:00:00', 'HOP_PHU_HUYNH', 'Phụ huynh lớp 10A1', '2025-09-15 12:00:00', 'GV001', 'LOP', NULL, 1, NULL, 'KHAN_CAP', 'HIEN_THI'),

('Thông báo đã nhập điểm Toán - Lớp 10A1',
 'Điểm kiểm tra thường xuyên môn Toán lần 1 đã được cập nhật. Học sinh và phụ huynh có thể xem điểm trong hệ thống.',
 '2025-09-25 16:00:00', 'HOC_TAP', 'Học sinh lớp 10A1', NULL, 'GV001', 'LOP', NULL, 1, NULL, 'BINH_THUONG', 'HIEN_THI'),

-- Thông báo theo khối
('Hướng nghiệp cho học sinh khối 12',
 'Nhà trường tổ chức buổi tư vấn hướng nghiệp dành cho học sinh khối 12.\n- Thời gian: 14h00 ngày 20/10/2025\n- Địa điểm: Hội trường lớn\n- Khách mời: Đại diện các trường Đại học top đầu',
 '2025-10-10 09:00:00', 'SU_KIEN', 'Học sinh khối 12', '2025-10-20 14:00:00', 'admin', 'KHOI', NULL, NULL, 12, 'QUAN_TRONG', 'HIEN_THI'),

-- Thông báo khen thưởng
('Khen thưởng học sinh giỏi tháng 9',
 'Nhà trường tuyên dương các em học sinh đạt thành tích xuất sắc trong tháng 9/2025. Danh sách chi tiết đã được niêm yết tại bảng tin trường.',
 '2025-10-05 10:00:00', 'KHEN_THUONG', 'Toàn trường', NULL, 'admin', 'ALL', NULL, NULL, NULL, 'BINH_THUONG', 'HIEN_THI');

-- =====================================================================
-- PHẦN 5: TẠO DỮ LIỆU NGƯỜI NHẬN CHO CÁC THÔNG BÁO
-- =====================================================================

-- Gửi thông báo "ALL" đến tất cả người dùng (admin, giáo viên, học sinh)
INSERT INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayDoc)
SELECT 
    tb.MaThongBao,
    nd.TenDangNhap,
    CASE WHEN RAND() > 0.5 THEN TRUE ELSE FALSE END as DaDoc,
    CASE WHEN RAND() > 0.5 THEN DATE_ADD(tb.NgayTao, INTERVAL FLOOR(RAND() * 7) DAY) ELSE NULL END as NgayDoc
FROM ThongBao tb
CROSS JOIN NguoiDung nd
WHERE tb.PhamVi = 'ALL'
AND nd.TrangThai = 'Hoạt động'
LIMIT 500;

-- Gửi thông báo theo vai trò (teacher)
INSERT INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayDoc)
SELECT 
    tb.MaThongBao,
    ndvt.TenDangNhap,
    CASE WHEN RAND() > 0.6 THEN TRUE ELSE FALSE END as DaDoc,
    CASE WHEN RAND() > 0.6 THEN DATE_ADD(tb.NgayTao, INTERVAL FLOOR(RAND() * 3) DAY) ELSE NULL END as NgayDoc
FROM ThongBao tb
JOIN NguoiDungVaiTro ndvt ON tb.MaVaiTroNhan = ndvt.MaVaiTro
WHERE tb.PhamVi = 'VAI_TRO' AND tb.MaVaiTroNhan = 'teacher'
ON DUPLICATE KEY UPDATE DaDoc = DaDoc;

-- Gửi thông báo theo vai trò (student)
INSERT INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayDoc)
SELECT 
    tb.MaThongBao,
    ndvt.TenDangNhap,
    CASE WHEN RAND() > 0.7 THEN TRUE ELSE FALSE END as DaDoc,
    CASE WHEN RAND() > 0.7 THEN DATE_ADD(tb.NgayTao, INTERVAL FLOOR(RAND() * 5) DAY) ELSE NULL END as NgayDoc
FROM ThongBao tb
JOIN NguoiDungVaiTro ndvt ON tb.MaVaiTroNhan = ndvt.MaVaiTro
WHERE tb.PhamVi = 'VAI_TRO' AND tb.MaVaiTroNhan = 'student'
LIMIT 1000
ON DUPLICATE KEY UPDATE DaDoc = DaDoc;

-- Gửi thông báo theo lớp (10A1 - MaLop = 1)
INSERT INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayDoc)
SELECT 
    tb.MaThongBao,
    hs.TenDangNhap,
    CASE WHEN RAND() > 0.4 THEN TRUE ELSE FALSE END as DaDoc,
    CASE WHEN RAND() > 0.4 THEN DATE_ADD(tb.NgayTao, INTERVAL FLOOR(RAND() * 2) DAY) ELSE NULL END as NgayDoc
FROM ThongBao tb
JOIN PhanLop pl ON tb.MaLop = pl.MaLop
JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
WHERE tb.PhamVi = 'LOP' 
AND tb.MaLop = 1 
AND hs.TenDangNhap IS NOT NULL
ON DUPLICATE KEY UPDATE DaDoc = DaDoc;

-- Gửi thông báo theo khối (Khối 12)
INSERT INTO NguoiNhanThongBao (MaThongBao, TenDangNhap, DaDoc, NgayDoc)
SELECT 
    tb.MaThongBao,
    hs.TenDangNhap,
    FALSE as DaDoc,
    NULL as NgayDoc
FROM ThongBao tb
JOIN LopHoc lh ON tb.MaKhoi = lh.MaKhoi
JOIN PhanLop pl ON lh.MaLop = pl.MaLop
JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
WHERE tb.PhamVi = 'KHOI' 
AND tb.MaKhoi = 12 
AND hs.TenDangNhap IS NOT NULL
ON DUPLICATE KEY UPDATE DaDoc = DaDoc;

-- =====================================================================
-- PHẦN 6: CẬP NHẬT PHÂN QUYỀN CHO MODULE THÔNG BÁO
-- =====================================================================

-- Thêm chức năng thông báo nếu chưa có
INSERT IGNORE INTO ChucNang (MaChucNang, TenChucNang, MoTa) VALUES
('qlthongbao', 'Quản lý thông báo', 'Quản lý và gửi thông báo trong hệ thống');

-- Thêm các hành động cho chức năng thông báo
INSERT IGNORE INTO ChucNangHanhDong (MaChucNang, HanhDong) VALUES
('qlthongbao', 'read'),
('qlthongbao', 'create'),
('qlthongbao', 'update'),
('qlthongbao', 'delete');

-- Gán quyền thông báo cho Admin (tất cả quyền)
INSERT IGNORE INTO VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong) VALUES
('admin', 'qlthongbao', 'read'),
('admin', 'qlthongbao', 'create'),
('admin', 'qlthongbao', 'update'),
('admin', 'qlthongbao', 'delete');

-- Gán quyền thông báo cho Giáo viên (read + create cho lớp mình)
INSERT IGNORE INTO VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong) VALUES
('teacher', 'qlthongbao', 'read'),
('teacher', 'qlthongbao', 'create');

-- Gán quyền thông báo cho Học sinh (chỉ read)
INSERT IGNORE INTO VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong) VALUES
('student', 'qlthongbao', 'read');

-- Gán quyền thông báo cho Phụ huynh (chỉ read)
INSERT IGNORE INTO VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong) VALUES
('parent', 'qlthongbao', 'read');

-- Gán chức năng thông báo vào các vai trò
INSERT IGNORE INTO VaiTroChucNang (MaVaiTro, MaChucNang) VALUES
('admin', 'qlthongbao'),
('teacher', 'qlthongbao'),
('student', 'qlthongbao'),
('parent', 'qlthongbao');

-- =====================================================================
-- HOÀN THÀNH
-- =====================================================================

SET FOREIGN_KEY_CHECKS = 1;

SELECT 'Thông báo schema created successfully!' AS Status;
SELECT CONCAT('Số thông báo: ', COUNT(*)) AS ThongBao FROM ThongBao;
SELECT CONCAT('Số người nhận: ', COUNT(*)) AS NguoiNhan FROM NguoiNhanThongBao;
SELECT CONCAT('Số loại thông báo: ', COUNT(*)) AS LoaiThongBao FROM LoaiThongBao;
