-- =====================================================================
-- FILE 03: SAMPLE SEED DATA (OPTIMIZED VERSION - 500 students)
-- Mục đích: Dữ liệu mẫu tối ưu để test hệ thống phân lớp
-- Chạy: mysql -u root -p QuanLyHocSinh < 03_sample_seed_optimized.sql
-- =====================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
USE QuanLyHocSinh;
START TRANSACTION;

-- =====================================================================
-- PHẦN 1: DỮ LIỆU CƠ BẢN (KhoiLop, MonHoc, VaiTro, ChucNang)
-- =====================================================================

-- Khối lớp (3 khối: 10, 11, 12)
INSERT INTO KhoiLop (MaKhoi, TenKhoi) VALUES 
(10, 'Khối 10'),
(11, 'Khối 11'),
(12, 'Khối 12');

-- Môn học (13 môn theo chương trình THPT)
INSERT INTO MonHoc (MaMonHoc, TenMonHoc, SoTiet, GhiChu) VALUES
(1, 'Ngữ văn', 50, 'Môn chính'),
(2, 'Toán', 60, 'Môn chính'),
(3, 'Tiếng Anh', 41, 'Môn chính'),
(4, 'Lịch sử', 36, 'Khoa học xã hội'),
(5, 'Địa lý', 50, 'Khoa học xã hội'),
(6, 'GD Kinh tế & Pháp luật', 35, 'Khoa học xã hội'),
(7, 'Vật lý', 35, 'Khoa học tự nhiên'),
(8, 'Hóa học', 41, 'Khoa học tự nhiên'),
(9, 'Sinh học', 23, 'Khoa học tự nhiên'),
(10, 'Công nghệ', 41, 'Khoa học xã hội'),
(11, 'Tin học', 53, 'Kỹ năng khác'),
(12, 'Giáo dục thể chất', 35, 'Kỹ năng khác'),
(13, 'GDQP-AN', 26, 'Kỹ năng khác');

-- Vai trò hệ thống
INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES
('student', 'Học sinh', 'Học sinh trong hệ thống'),
('parent', 'Phụ huynh', 'Phụ huynh học sinh'),
('teacher', 'Giáo viên', 'Giáo viên giảng dạy'),
('admin', 'Quản trị viên', 'Quản trị hệ thống');

-- Chức năng hệ thống
INSERT INTO ChucNang (MaChucNang, TenChucNang, MoTa) VALUES
('qlhs', 'Quản lý học sinh', 'Quản lý thông tin học sinh'),
('qlphuhuynh', 'Quản lý phụ huynh', 'Quản lý thông tin phụ huynh'),
('qlgiaovien', 'Quản lý giáo viên', 'Quản lý thông tin giáo viên'),
('qllophoc', 'Quản lý lớp học', 'Quản lý lớp học và phân lớp'),
('qlmonhoc', 'Quản lý môn học', 'Quản lý môn học và phân công'),
('qltkb', 'Quản lý thời khóa biểu', 'Quản lý thời khóa biểu'),
('qldiem', 'Quản lý điểm số', 'Quản lý điểm số học sinh'),
('qlthongbao', 'Quản lý thông báo', 'Quản lý thông báo hệ thống');

-- Phân quyền vai trò - chức năng
INSERT INTO VaiTroChucNang (MaVaiTro, MaChucNang) VALUES
-- Học sinh
('student', 'qlhs'),
-- Phụ huynh
('parent', 'qlhs'),
('parent', 'qlphuhuynh'),
-- Giáo viên
('teacher', 'qlgiaovien'),
('teacher', 'qllophoc'),
('teacher', 'qlmonhoc'),
('teacher', 'qltkb'),
('teacher', 'qldiem'),
-- Admin (tất cả quyền)
('admin', 'qlhs'),
('admin', 'qlphuhuynh'),
('admin', 'qlgiaovien'),
('admin', 'qllophoc'),
('admin', 'qlmonhoc'),
('admin', 'qltkb'),
('admin', 'qldiem'),
('admin', 'qlthongbao');

-- =====================================================================
-- PHẦN 2: NĂM HỌC VÀ HỌC KỲ
-- =====================================================================

-- Năm học
INSERT INTO NamHoc (MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc) VALUES
('2024-2025', 'Năm học 2024-2025', '2024-09-01', '2025-05-31'),
('2025-2026', 'Năm học 2025-2026', '2025-09-01', '2026-05-31'),
('2026-2027', 'Năm học 2026-2027', '2026-09-01', '2027-05-31');

-- Học kỳ (4 học kỳ cho test 2 kịch bản)
INSERT INTO HocKy (MaHocKy, TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT) VALUES
-- Năm 2025-2026: Test kịch bản HK1 -> HK2
(1, 'Học kỳ I', '2025-2026', 'Đang diễn ra', '2025-09-01', '2026-01-15'),
(2, 'Học kỳ II', '2025-2026', 'Chưa bắt đầu', '2026-01-16', '2026-05-31'),
-- Năm 2026-2027: Sẵn sàng cho test kịch bản HK2 -> HK1 năm sau
(3, 'Học kỳ I', '2026-2027', 'Chưa bắt đầu', '2026-09-01', '2027-01-15'),
(4, 'Học kỳ II', '2026-2027', 'Chưa bắt đầu', '2027-01-16', '2027-05-31');

-- =====================================================================
-- PHẦN 3: GIÁO VIÊN (30 giáo viên)
-- =====================================================================

INSERT INTO GiaoVien (MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, TrangThai) VALUES
-- Tổ Toán (3 giáo viên)
('GV001', 'Nguyễn Văn Toán', '1985-03-15', 'Nam', '123 Nguyễn Huệ, Q1, TP.HCM', '0901234567', 'nguyen.toan.gv001@school.edu.vn', 'Đang giảng dạy'),
('GV002', 'Trần Thị Hương', '1987-07-22', 'Nữ', '456 Lê Lợi, Q1, TP.HCM', '0901234568', 'tran.huong.gv002@school.edu.vn', 'Đang giảng dạy'),
('GV003', 'Lê Minh Tuấn', '1983-11-08', 'Nam', '789 Đồng Khởi, Q1, TP.HCM', '0901234569', 'le.tuan.gv003@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Văn (3 giáo viên)
('GV004', 'Phạm Thị Mai', '1986-05-14', 'Nữ', '321 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234570', 'pham.mai.gv004@school.edu.vn', 'Đang giảng dạy'),
('GV005', 'Hoàng Văn Đức', '1984-09-30', 'Nam', '654 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234571', 'hoang.duc.gv005@school.edu.vn', 'Đang giảng dạy'),
('GV006', 'Võ Thị Lan', '1988-01-18', 'Nữ', '987 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234572', 'vo.lan.gv006@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Anh (3 giáo viên)
('GV007', 'Đặng Minh Khang', '1982-12-25', 'Nam', '147 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234573', 'dang.khang.gv007@school.edu.vn', 'Đang giảng dạy'),
('GV008', 'Bùi Thị Hoa', '1985-04-12', 'Nữ', '258 Trần Hưng Đạo, Q5, TP.HCM', '0901234574', 'bui.hoa.gv008@school.edu.vn', 'Đang giảng dạy'),
('GV009', 'Ngô Văn Nam', '1987-08-19', 'Nam', '369 Lý Tự Trọng, Q1, TP.HCM', '0901234575', 'ngo.nam.gv009@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Lý (2 giáo viên)
('GV010', 'Đinh Thị Thu', '1983-06-07', 'Nữ', '741 Pasteur, Q3, TP.HCM', '0901234576', 'dinh.thu.gv010@school.edu.vn', 'Đang giảng dạy'),
('GV011', 'Phan Văn Hùng', '1986-02-28', 'Nam', '852 Nguyễn Du, Q1, TP.HCM', '0901234577', 'phan.hung.gv011@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Hóa (2 giáo viên)
('GV012', 'Lý Thị Nga', '1984-10-15', 'Nữ', '963 Đinh Tiên Hoàng, Q.Bình Thạnh, TP.HCM', '0901234578', 'ly.nga.gv012@school.edu.vn', 'Đang giảng dạy'),
('GV013', 'Vũ Minh Tuấn', '1988-12-03', 'Nam', '159 Nguyễn Thị Thập, Q7, TP.HCM', '0901234579', 'vu.tuan.gv013@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Sinh (2 giáo viên)
('GV014', 'Trịnh Thị Linh', '1982-09-21', 'Nữ', '357 Võ Văn Tần, Q3, TP.HCM', '0901234580', 'trinh.linh.gv014@school.edu.vn', 'Đang giảng dạy'),
('GV015', 'Cao Thị Hạnh', '1985-01-16', 'Nữ', '468 Nguyễn Đình Chiểu, Q3, TP.HCM', '0901234581', 'cao.hanh.gv015@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Sử (2 giáo viên)
('GV016', 'Lâm Văn Phong', '1987-05-24', 'Nam', '579 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234582', 'lam.phong.gv016@school.edu.vn', 'Đang giảng dạy'),
('GV017', 'Hồ Thị Yến', '1983-11-11', 'Nữ', '680 Nguyễn Văn Linh, Q7, TP.HCM', '0901234583', 'ho.yen.gv017@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Địa (2 giáo viên)
('GV018', 'Dương Minh Đức', '1986-07-08', 'Nam', '791 Lê Văn Việt, Q9, TP.HCM', '0901234584', 'duong.duc.gv018@school.edu.vn', 'Đang giảng dạy'),
('GV019', 'Tôn Thị Lan', '1984-03-26', 'Nữ', '802 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234585', 'ton.lan.gv019@school.edu.vn', 'Đang giảng dạy'),

-- Tổ GDCD (2 giáo viên)
('GV020', 'Chu Văn Thành', '1988-09-13', 'Nam', '913 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234586', 'chu.thanh.gv020@school.edu.vn', 'Đang giảng dạy'),
('GV021', 'Đỗ Thị Hương', '1985-08-05', 'Nữ', '124 Nguyễn Huệ, Q1, TP.HCM', '0901234587', 'do.huong.gv021@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Công nghệ (2 giáo viên)
('GV022', 'Nguyễn Minh Tâm', '1987-04-22', 'Nam', '235 Lê Lợi, Q1, TP.HCM', '0901234588', 'nguyen.tam.gv022@school.edu.vn', 'Đang giảng dạy'),
('GV023', 'Trần Thị Ngọc', '1983-12-09', 'Nữ', '346 Đồng Khởi, Q1, TP.HCM', '0901234589', 'tran.ngoc.gv023@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Tin học (3 giáo viên)
('GV024', 'Lê Văn Hải', '1986-06-16', 'Nam', '457 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234590', 'le.hai.gv024@school.edu.vn', 'Đang giảng dạy'),
('GV025', 'Phạm Thị Thu', '1984-02-03', 'Nữ', '568 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234591', 'pham.thu.gv025@school.edu.vn', 'Đang giảng dạy'),
('GV026', 'Hoàng Văn Long', '1985-10-20', 'Nam', '679 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234592', 'hoang.long.gv026@school.edu.vn', 'Đang giảng dạy'),

-- Tổ Thể dục (2 giáo viên)
('GV027', 'Võ Thị Hoa', '1987-07-17', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234593', 'vo.hoa.gv027@school.edu.vn', 'Đang giảng dạy'),
('GV028', 'Đặng Minh Tuấn', '1983-05-04', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234594', 'dang.tuan.gv028@school.edu.vn', 'Đang giảng dạy'),

-- Tổ GDQP-AN (2 giáo viên)
('GV029', 'Bùi Thị Mai', '1986-01-11', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234595', 'bui.mai.gv029@school.edu.vn', 'Đang giảng dạy'),
('GV030', 'Ngô Văn Đức', '1984-09-28', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234596', 'ngo.duc.gv030@school.edu.vn', 'Đang giảng dạy');

SELECT 'Teachers insertion completed (30/30)' AS Status;

-- =====================================================================
-- PHẦN 4: LỚP HỌC (24 lớp: 8 lớp/khối)
-- =====================================================================

INSERT INTO LopHoc (TenLop, MaKhoi, SiSo, MaGiaoVienChuNhiem) VALUES
-- Khối 10 (8 lớp - mỗi lớp ~21 học sinh = 168 HS)
('10A1', 10, 21, 'GV001'), ('10A2', 10, 21, 'GV002'), ('10A3', 10, 21, 'GV003'), ('10A4', 10, 21, 'GV004'),
('10A5', 10, 21, 'GV005'), ('10A6', 10, 21, 'GV006'), ('10A7', 10, 21, 'GV007'), ('10A8', 10, 21, 'GV008'),
-- Khối 11 (8 lớp - mỗi lớp ~21 học sinh = 168 HS)
('11A1', 11, 21, 'GV009'), ('11A2', 11, 21, 'GV010'), ('11A3', 11, 21, 'GV011'), ('11A4', 11, 21, 'GV012'),
('11A5', 11, 21, 'GV013'), ('11A6', 11, 21, 'GV014'), ('11A7', 11, 21, 'GV015'), ('11A8', 11, 21, 'GV016'),
-- Khối 12 (8 lớp - mỗi lớp ~21 học sinh = 168 HS)
('12A1', 12, 21, 'GV017'), ('12A2', 12, 21, 'GV018'), ('12A3', 12, 21, 'GV019'), ('12A4', 12, 21, 'GV020'),
('12A5', 12, 21, 'GV021'), ('12A6', 12, 21, 'GV022'), ('12A7', 12, 21, 'GV023'), ('12A8', 12, 21, 'GV024');

SELECT 'Lop hoc da duoc tao (24 lop)' AS Status;

-- =====================================================================
-- PHẦN 5: HỌC SINH (500 học sinh: 475 active + 25 inactive)
-- =====================================================================

INSERT INTO HocSinh (HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai)
SELECT 
    CONCAT(
        ELT(FLOOR(1 + (seq MOD 20)), 'Nguyễn', 'Trần', 'Lê', 'Phạm', 'Hoàng', 'Phan', 'Vũ', 'Đặng', 'Bùi', 'Đỗ', 
            'Hồ', 'Ngô', 'Dương', 'Lý', 'Võ', 'Đinh', 'Trịnh', 'Cao', 'Lâm', 'Tôn'),
        ' ',
        ELT(FLOOR(1 + ((seq * 3) MOD 15)), 'Văn', 'Thị', 'Minh', 'Công', 'Quốc', 'Thanh', 'Hữu', 'Đức', 'Ngọc', 'Thu', 'Mai', 'Hương', 'Phương', 'Anh', 'Tuấn'),
        ' ',
        ELT(FLOOR(1 + ((seq * 7) MOD 20)), 'An', 'Bình', 'Cường', 'Dũng', 'Giang', 'Hải', 'Hùng', 'Khoa', 'Long', 'Nam', 
            'Phúc', 'Quân', 'Sơn', 'Tài', 'Thắng', 'Toàn', 'Trí', 'Tùng', 'Tuấn', 'Vinh')
    ) as HoTen,
    -- Ngày sinh từ 2006-2012 để phân lớp đủ 3 khối (10, 11, 12)
    -- Khối 10: 2008-2012 | Khối 11: 2007-2011 | Khối 12: 2006-2010
    DATE_ADD('2006-01-01', INTERVAL (seq MOD 2555) DAY) as NgaySinh,  -- Từ 2006-01-01 đến 2012-12-31 (7 năm)
    IF((seq MOD 2) = 0, 'Nam', 'Nữ') as GioiTinh,
    CONCAT('09', LPAD(12340000 + seq, 8, '0')) as SDTHS,
    CONCAT('hs', LPAD(seq, 3, '0'), '@student.edu.vn') as Email,
    -- Trạng thái: 450 Đang học, 15 Nghỉ học, 5 Bảo lưu, 30 Thôi học
    CASE 
        WHEN seq <= 450 THEN 'Đang học'
        WHEN seq <= 465 THEN 'Nghỉ học'
        WHEN seq <= 470 THEN 'Bảo lưu'
        ELSE 'Thôi học'
    END as TrangThai
FROM (
    SELECT @row := @row + 1 as seq
    FROM (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t1,
         (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t2,
         (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t3,
         (SELECT @row := 0) r
    LIMIT 500
) numbers;

SELECT 'Hoc sinh da duoc tao (500 hoc sinh)' AS Status;

-- =====================================================================
-- PHẦN 5B: TẠO TÀI KHOẢN ĐĂNG NHẬP CHO TẤT CẢ HỌC SINH (500 accounts)
-- =====================================================================

-- Tạo tài khoản NguoiDung cho TẤT CẢ học sinh (username = HS{MaHocSinh}, password = 123456)
-- TrangThai: 'Hoạt động' nếu HS đang học/nghỉ học, 'Tạm khóa' nếu thôi học/bảo lưu
INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai)
SELECT 
    CONCAT('HS', MaHocSinh) as TenDangNhap,
    '123456' as MatKhau,
    IF(HocSinh.TrangThai IN ('Đang học', 'Nghỉ học'), 'Hoạt động', 'Tạm khóa') as TrangThai
FROM HocSinh
ORDER BY MaHocSinh;

SELECT 'Student accounts created (500 accounts)' AS Status;

-- Cập nhật TenDangNhap cho TẤT CẢ học sinh
UPDATE HocSinh 
SET TenDangNhap = CONCAT('HS', MaHocSinh);

SELECT 'Student TenDangNhap updated (500 students)' AS Status;

-- Gán vai trò 'student' cho TẤT CẢ tài khoản học sinh
INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro)
SELECT 
    CONCAT('HS', MaHocSinh) as TenDangNhap,
    'student' as MaVaiTro
FROM HocSinh
ORDER BY MaHocSinh;

SELECT 'Student roles assigned (500 roles)' AS Status;

-- =====================================================================
-- PHẦN 6: PHỤ HUYNH (500 phụ huynh)
-- =====================================================================

INSERT INTO PhuHuynh (HoTen, SoDienThoai, Email, DiaChi)
SELECT 
    CONCAT('PH của HS', LPAD(seq, 3, '0')) as HoTen,
    CONCAT('09', LPAD(23450000 + seq, 8, '0')) as SoDienThoai,
    CONCAT('ph', LPAD(seq, 3, '0'), '@parent.edu.vn') as Email,
    CONCAT(
        FLOOR(1 + ((seq * 13) MOD 999)), ' ',
        ELT(FLOOR(1 + ((seq * 17) MOD 20)), 'Nguyễn Huệ', 'Lê Lợi', 'Trần Hưng Đạo', 'Lý Tự Trọng', 'Hai Bà Trưng', 
            'Võ Văn Tần', 'Cách Mạng Tháng 8', 'Điện Biên Phủ', 'Pasteur', 'Nam Kỳ Khởi Nghĩa',
            'Lạc Long Quân', 'Âu Cơ', 'Quang Trung', 'Phan Văn Trị', 'Hoàng Văn Thụ',
            'Cộng Hòa', 'Trường Chinh', 'Tôn Đức Thắng', 'Võ Thị Sáu', 'Yersin'),
        ', Q', FLOOR(1 + ((seq * 11) MOD 12)), ', TP.HCM'
    ) as DiaChi
FROM (
    SELECT @row := @row + 1 as seq
    FROM (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t1,
         (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t2,
         (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t3,
         (SELECT @row := 0) r
    LIMIT 500
) numbers;

SELECT 'Phu huynh da duoc tao (500 phu huynh)' AS Status;

-- Liên kết học sinh - phụ huynh (1-1, HS active với PH tương ứng)
INSERT INTO HocSinhPhuHuynh (MaHocSinh, MaPhuHuynh, MoiQuanHe)
SELECT 
    hs.MaHocSinh,
    ph.MaPhuHuynh,
    'Cha/Mẹ' as MoiQuanHe
FROM (
    SELECT MaHocSinh, @hs_row := @hs_row + 1 as hs_num
    FROM HocSinh, (SELECT @hs_row := 0) r
    WHERE TrangThai = 'Đang học'
    ORDER BY MaHocSinh
    LIMIT 475
) hs
JOIN (
    SELECT MaPhuHuynh, @ph_row := @ph_row + 1 as ph_num
    FROM PhuHuynh, (SELECT @ph_row := 0) r2
    ORDER BY MaPhuHuynh
    LIMIT 475
) ph ON hs.hs_num = ph.ph_num;

SELECT 'Lien ket hoc sinh - phu huynh hoan thanh (475 lien ket)' AS Status;

-- =====================================================================
-- PHẦN 7: PHÂN LỚP (Phân 475 học sinh active vào 24 lớp)
-- =====================================================================

-- Phân lớp cho học kỳ 1 (2025-2026)
INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy)
SELECT 
    hs.MaHocSinh,
    lh.MaLop,
    1 as MaHocKy
FROM (
    SELECT MaHocSinh, @row_num := @row_num + 1 as row_num
    FROM HocSinh, (SELECT @row_num := 0) r
    WHERE TrangThai = 'Đang học'
    ORDER BY MaHocSinh
    LIMIT 475
) hs
JOIN (
    SELECT MaLop, MaKhoi, @lop_row := @lop_row + 1 as lop_num
    FROM LopHoc, (SELECT @lop_row := 0) r2
    ORDER BY MaLop
) lh ON lh.lop_num = CEILING(hs.row_num / 19.79);

SELECT 'Phan lop hoc ky 1 hoan thanh (475 hoc sinh)' AS Status;

-- =====================================================================
-- PHẦN 8: DỮ LIỆU TEST - ĐIỂM SỐ HỌC KỲ 1
-- =====================================================================

-- Tạo điểm cho 475 học sinh × 13 môn = 6,175 bản ghi
INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, DiemThuongXuyen, DiemGiuaKy, DiemCuoiKy, DiemTrungBinh)
SELECT 
    pl.MaHocSinh,
    mh.MaMonHoc,
    1 as MaHocKy,
    ROUND(5.0 + (RAND() * 5.0), 1) as DiemThuongXuyen,
    ROUND(5.0 + (RAND() * 5.0), 1) as DiemGiuaKy,
    ROUND(5.0 + (RAND() * 5.0), 1) as DiemCuoiKy,
    ROUND(5.0 + (RAND() * 5.0), 1) as DiemTrungBinh
FROM PhanLop pl
CROSS JOIN MonHoc mh
WHERE pl.MaHocKy = 1;

SELECT 'Diem so HK1 da duoc tao (6,175 ban ghi)' AS Status;

-- =====================================================================
-- PHẦN 9: DỮ LIỆU TEST - HẠNH KIỂM HỌC KỲ 1
-- =====================================================================

-- Tạo hạnh kiểm cho 475 học sinh
INSERT INTO HanhKiem (MaHocSinh, MaHocKy, XepLoai, NhanXet)
SELECT 
    pl.MaHocSinh,
    1 as MaHocKy,
    ELT(CEILING(RAND() * 4), 'Tốt', 'Khá', 'Trung bình', 'Yếu') as XepLoai,
    'Tự động tạo' as NhanXet
FROM PhanLop pl
WHERE pl.MaHocKy = 1;

SELECT 'Hanh kiem HK1 da duoc tao (475 ban ghi)' AS Status;

-- =====================================================================
-- PHẦN 10: DỮ LIỆU TEST - XẾP LOẠI HỌC KỲ 1
-- =====================================================================

-- Tạo xếp loại cho 475 học sinh
INSERT INTO XepLoai (MaHocSinh, MaHocKy, HocLuc, GhiChu)
SELECT
    hs.MaHocSinh,
    1 AS MaHocKy,
    -- Xếp loại cuối cùng: lấy bậc thấp hơn giữa học lực và hạnh kiểm
    CASE
        WHEN
            (CASE
                WHEN tb_all >= 8.0 AND tb_main >= 8.0 AND min_mon >= 6.5 THEN 1
                WHEN tb_all >= 6.5 AND tb_main >= 6.5 AND min_mon >= 5.0 THEN 2
                WHEN tb_all >= 5.0 AND tb_main >= 5.0 AND min_mon >= 3.5 THEN 3
                WHEN tb_all >= 3.5 AND min_mon >= 2.0 THEN 4
                ELSE 5
            END)
            >
            (CASE
                WHEN hk.HocLuc = 'Giỏi' THEN 1
                WHEN hk.HocLuc = 'Khá' THEN 2
                WHEN hk.HocLuc = 'Trung bình' THEN 3
                WHEN hk.HocLuc = 'Yếu' THEN 4
                ELSE 5
            END)
        THEN
            CASE
                WHEN hk.HocLuc = 'Giỏi' THEN 'Giỏi'
                WHEN hk.HocLuc = 'Khá' THEN 'Khá'
                WHEN hk.HocLuc = 'Trung bình' THEN 'Trung bình'
                WHEN hk.HocLuc = 'Yếu' THEN 'Yếu'
                ELSE 'Kém'
            END
        ELSE
            CASE
                WHEN tb_all >= 8.0 AND tb_main >= 8.0 AND min_mon >= 6.5 THEN 'Giỏi'
                WHEN tb_all >= 6.5 AND tb_main >= 6.5 AND min_mon >= 5.0 THEN 'Khá'
                WHEN tb_all >= 5.0 AND tb_main >= 5.0 AND min_mon >= 3.5 THEN 'Trung bình'
                WHEN tb_all >= 3.5 AND min_mon >= 2.0 THEN 'Yếu'
                ELSE 'Kém'
            END
    END AS HocLuc,
    'HK1 - Tự động tính theo quy tắc' as GhiChu
FROM
    PhanLop pl
    JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
    LEFT JOIN (
        SELECT MaHocSinh, MaHocKy, XepLoai
        FROM HanhKiem
        WHERE MaHocKy = 1
    ) hk ON hk.MaHocSinh = hs.MaHocSinh
    -- Tính điểm trung bình các môn, điểm TB Toán/Văn/Anh, điểm thấp nhất
    JOIN (
        SELECT
            ds.MaHocSinh,
            AVG(ds.DiemTrungBinh) AS tb_all,
            MAX(CASE WHEN mh.TenMonHoc IN ('Toán', 'Ngữ văn', 'Tiếng Anh') THEN ds.DiemTrungBinh ELSE 0 END) AS tb_main,
            MIN(ds.DiemTrungBinh) AS min_mon
        FROM DiemSo ds
        JOIN MonHoc mh ON ds.MaMonHoc = mh.MaMonHoc
        WHERE ds.MaHocKy = 1
        GROUP BY ds.MaHocSinh
    ) diem ON diem.MaHocSinh = hs.MaHocSinh
WHERE pl.MaLop BETWEEN 17 AND 24 AND pl.MaHocKy = 1;

SELECT 'Xep loai HK1 da duoc tao (475 ban ghi)' AS Status;

-- =====================================================================
-- PHẦN 11: PHÂN CÔNG GIẢNG DẠY (30 GV × 13 môn = 390 phân công)
-- =====================================================================

INSERT INTO PhanCongGiangDay (MaGiaoVien, MaMonHoc, MaHocKy)
SELECT 
    gv.MaGiaoVien,
    mh.MaMonHoc,
    1 as MaHocKy
FROM GiaoVien gv
CROSS JOIN MonHoc mh
LIMIT 390;

SELECT 'Phan cong giang day hoan thanh (390 phan cong)' AS Status;

-- =====================================================================
-- HOÀN TẤT
-- =====================================================================

SET FOREIGN_KEY_CHECKS = 1;
COMMIT;

SELECT '=== IMPORT HOAN THANH ===' AS Status;
SELECT '500 hoc sinh (475 active, 25 inactive)' AS HocSinh;
SELECT '500 phu huynh' AS PhuHuynh;
SELECT '30 giao vien' AS GiaoVien;
SELECT '24 lop hoc (8 lop/khoi)' AS LopHoc;
SELECT '475 hoc sinh da duoc phan lop HK1' AS PhanLop;
SELECT '6,175 ban ghi diem (475 HS × 13 mon)' AS DiemSo;
SELECT '475 ban ghi hanh kiem' AS HanhKiem;
SELECT '475 ban ghi xep loai' AS XepLoai;
SELECT 'SAN SANG TEST PHAN LOP TU DONG!' AS KetLuan;
