-- =====================================================================
-- FILE 03: SAMPLE SEED DATA
-- Mục đích: Dữ liệu mẫu đầy đủ để test hệ thống
-- Chạy: mysql -u root -p < 03_sample_seed.sql
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
INSERT IGNORE INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES
('student', 'Học sinh', 'Học sinh trong hệ thống'),
('parent', 'Phụ huynh', 'Phụ huynh học sinh'),
('teacher', 'Giáo viên', 'Giáo viên giảng dạy'),
('admin', 'Quản trị viên', 'Quản trị hệ thống');

-- Chức năng hệ thống
INSERT IGNORE INTO ChucNang (MaChucNang, TenChucNang, MoTa) VALUES
('qlhs', 'Quản lý học sinh', 'Quản lý thông tin học sinh'),
('qlphuhuynh', 'Quản lý phụ huynh', 'Quản lý thông tin phụ huynh'),
('qlgiaovien', 'Quản lý giáo viên', 'Quản lý thông tin giáo viên'),
('qllophoc', 'Quản lý lớp học', 'Quản lý lớp học và phân lớp'),
('qlmonhoc', 'Quản lý môn học', 'Quản lý môn học và phân công'),
('qltkb', 'Quản lý thời khóa biểu', 'Quản lý thời khóa biểu'),
('qldiem', 'Quản lý điểm số', 'Quản lý điểm số học sinh'),
('qlthongbao', 'Quản lý thông báo', 'Quản lý thông báo hệ thống');

-- Phân quyền vai trò - chức năng
INSERT IGNORE INTO VaiTroChucNang (MaVaiTro, MaChucNang) VALUES
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
-- PHẦN 3: GIÁO VIÊN (65-70 giáo viên)
-- =====================================================================

INSERT INTO GiaoVien (MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaMonChuyenMon, TrangThai) VALUES
-- Tổ Toán (7 giáo viên)
('GV001', 'Nguyễn Văn Toán', '1985-03-15', 'Nam', '123 Nguyễn Huệ, Q1, TP.HCM', '0901234567', 'nguyen.toan.gv001@school.edu.vn', 2, 'Đang giảng dạy'),
('GV002', 'Trần Thị Hương', '1987-07-22', 'Nữ', '456 Lê Lợi, Q1, TP.HCM', '0901234568', 'tran.huong.gv002@school.edu.vn', 2, 'Đang giảng dạy'),
('GV003', 'Lê Minh Tuấn', '1983-11-08', 'Nam', '789 Đồng Khởi, Q1, TP.HCM', '0901234569', 'le.tuan.gv003@school.edu.vn', 2, 'Đang giảng dạy'),
('GV004', 'Phạm Thị Mai', '1986-05-14', 'Nữ', '321 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234570', 'pham.mai.gv004@school.edu.vn', 2, 'Đang giảng dạy'),
('GV005', 'Hoàng Văn Đức', '1984-09-30', 'Nam', '654 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234571', 'hoang.duc.gv005@school.edu.vn', 2, 'Đang giảng dạy'),
('GV006', 'Võ Thị Lan', '1988-01-18', 'Nữ', '987 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234572', 'vo.lan.gv006@school.edu.vn', 2, 'Đang giảng dạy'),
('GV007', 'Đặng Minh Khang', '1982-12-25', 'Nam', '147 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234573', 'dang.khang.gv007@school.edu.vn', 2, 'Đang giảng dạy'),

-- Tổ Văn (7 giáo viên)
('GV008', 'Bùi Thị Hoa', '1985-04-12', 'Nữ', '258 Trần Hưng Đạo, Q5, TP.HCM', '0901234574', 'bui.hoa.gv008@school.edu.vn', 1, 'Đang giảng dạy'),
('GV009', 'Ngô Văn Nam', '1987-08-19', 'Nam', '369 Lý Tự Trọng, Q1, TP.HCM', '0901234575', 'ngo.nam.gv009@school.edu.vn', 1, 'Đang giảng dạy'),
('GV010', 'Đinh Thị Thu', '1983-06-07', 'Nữ', '741 Pasteur, Q3, TP.HCM', '0901234576', 'dinh.thu.gv010@school.edu.vn', 1, 'Đang giảng dạy'),
('GV011', 'Phan Văn Hùng', '1986-02-28', 'Nam', '852 Nguyễn Du, Q1, TP.HCM', '0901234577', 'phan.hung.gv011@school.edu.vn', 1, 'Đang giảng dạy'),
('GV012', 'Lý Thị Nga', '1984-10-15', 'Nữ', '963 Đinh Tiên Hoàng, Q.Bình Thạnh, TP.HCM', '0901234578', 'ly.nga.gv012@school.edu.vn', 1, 'Đang giảng dạy'),
('GV013', 'Vũ Minh Tuấn', '1988-12-03', 'Nam', '159 Nguyễn Thị Thập, Q7, TP.HCM', '0901234579', 'vu.tuan.gv013@school.edu.vn', 1, 'Đang giảng dạy'),
('GV014', 'Trịnh Thị Linh', '1982-09-21', 'Nữ', '357 Võ Văn Tần, Q3, TP.HCM', '0901234580', 'trinh.linh.gv014@school.edu.vn', 1, 'Đang giảng dạy'),

-- Tổ Anh (6 giáo viên)
('GV015', 'Cao Thị Hạnh', '1985-01-16', 'Nữ', '468 Nguyễn Đình Chiểu, Q3, TP.HCM', '0901234581', 'cao.hanh.gv015@school.edu.vn', 3, 'Đang giảng dạy'),
('GV016', 'Lâm Văn Phong', '1987-05-24', 'Nam', '579 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234582', 'lam.phong.gv016@school.edu.vn', 3, 'Đang giảng dạy'),
('GV017', 'Hồ Thị Yến', '1983-11-11', 'Nữ', '680 Nguyễn Văn Linh, Q7, TP.HCM', '0901234583', 'ho.yen.gv017@school.edu.vn', 3, 'Đang giảng dạy'),
('GV018', 'Dương Minh Đức', '1986-07-08', 'Nam', '791 Lê Văn Việt, Q9, TP.HCM', '0901234584', 'duong.duc.gv018@school.edu.vn', 3, 'Đang giảng dạy'),
('GV019', 'Tôn Thị Lan', '1984-03-26', 'Nữ', '802 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234585', 'ton.lan.gv019@school.edu.vn', 3, 'Đang giảng dạy'),
('GV020', 'Chu Văn Thành', '1988-09-13', 'Nam', '913 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234586', 'chu.thanh.gv020@school.edu.vn', 3, 'Đang giảng dạy'),

-- Tổ Lý (5 giáo viên)
('GV021', 'Đỗ Thị Hương', '1985-08-05', 'Nữ', '124 Nguyễn Huệ, Q1, TP.HCM', '0901234587', 'do.huong.gv021@school.edu.vn', 7, 'Đang giảng dạy'),
('GV022', 'Nguyễn Minh Tâm', '1987-04-22', 'Nam', '235 Lê Lợi, Q1, TP.HCM', '0901234588', 'nguyen.tam.gv022@school.edu.vn', 7, 'Đang giảng dạy'),
('GV023', 'Trần Thị Ngọc', '1983-12-09', 'Nữ', '346 Đồng Khởi, Q1, TP.HCM', '0901234589', 'tran.ngoc.gv023@school.edu.vn', 7, 'Đang giảng dạy'),
('GV024', 'Lê Văn Hải', '1986-06-16', 'Nam', '457 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234590', 'le.hai.gv024@school.edu.vn', 7, 'Đang giảng dạy'),
('GV025', 'Phạm Thị Thu', '1984-02-03', 'Nữ', '568 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234591', 'pham.thu.gv025@school.edu.vn', 7, 'Đang giảng dạy'),

-- Tổ Hóa (5 giáo viên)
('GV026', 'Hoàng Văn Long', '1985-10-20', 'Nam', '679 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234592', 'hoang.long.gv026@school.edu.vn', 8, 'Đang giảng dạy'),
('GV027', 'Võ Thị Hoa', '1987-07-17', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234593', 'vo.hoa.gv027@school.edu.vn', 8, 'Đang giảng dạy'),
('GV028', 'Đặng Minh Tuấn', '1983-05-04', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234594', 'dang.tuan.gv028@school.edu.vn', 8, 'Đang giảng dạy'),
('GV029', 'Bùi Thị Mai', '1986-01-11', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234595', 'bui.mai.gv029@school.edu.vn', 8, 'Đang giảng dạy'),
('GV030', 'Ngô Văn Đức', '1984-09-28', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234596', 'ngo.duc.gv030@school.edu.vn', 8, 'Đang giảng dạy'),

-- Tổ Sinh (4 giáo viên)
('GV031', 'Đinh Thị Hoa', '1985-06-13', 'Nữ', '134 Nguyễn Du, Q1, TP.HCM', '0901234597', 'dinh.hoa.gv031@school.edu.vn', 9, 'Đang giảng dạy'),
('GV032', 'Phan Văn Nam', '1987-02-20', 'Nam', '245 Lê Văn Việt, Q9, TP.HCM', '0901234598', 'phan.nam.gv032@school.edu.vn', 9, 'Đang giảng dạy'),
('GV033', 'Lý Thị Thu', '1983-10-07', 'Nữ', '356 Nguyễn Văn Linh, Q7, TP.HCM', '0901234599', 'ly.thu.gv033@school.edu.vn', 9, 'Đang giảng dạy'),
('GV034', 'Vũ Minh Hùng', '1986-08-14', 'Nam', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234600', 'vu.hung.gv034@school.edu.vn', 9, 'Đang giảng dạy'),

-- Tổ Sử (4 giáo viên)
('GV035', 'Trịnh Thị Lan', '1984-04-01', 'Nữ', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234601', 'trinh.lan.gv035@school.edu.vn', 4, 'Đang giảng dạy'),
('GV036', 'Cao Văn Đức', '1988-12-18', 'Nam', '689 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234602', 'cao.duc.gv036@school.edu.vn', 4, 'Đang giảng dạy'),
('GV037', 'Lâm Thị Mai', '1982-08-05', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234603', 'lam.mai.gv037@school.edu.vn', 4, 'Đang giảng dạy'),
('GV038', 'Hồ Minh Tuấn', '1985-06-22', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234604', 'ho.tuan.gv038@school.edu.vn', 4, 'Đang giảng dạy'),

-- Tổ Địa (4 giáo viên)
('GV039', 'Dương Thị Hương', '1987-02-09', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234605', 'duong.huong.gv039@school.edu.vn', 5, 'Đang giảng dạy'),
('GV040', 'Tôn Văn Khang', '1983-10-26', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234606', 'ton.khang.gv040@school.edu.vn', 5, 'Đang giảng dạy'),
('GV041', 'Chu Thị Ngọc', '1986-08-13', 'Nữ', '134 Nguyễn Du, Q1, TP.HCM', '0901234607', 'chu.ngoc.gv041@school.edu.vn', 5, 'Đang giảng dạy'),
('GV042', 'Đỗ Minh Hải', '1984-04-30', 'Nam', '245 Lê Văn Việt, Q9, TP.HCM', '0901234608', 'do.hai.gv042@school.edu.vn', 5, 'Đang giảng dạy'),

-- Tổ GDCD (4 giáo viên)
('GV043', 'Nguyễn Thị Thu', '1985-12-17', 'Nữ', '356 Nguyễn Văn Linh, Q7, TP.HCM', '0901234609', 'nguyen.thu.gv043@school.edu.vn', 6, 'Đang giảng dạy'),
('GV044', 'Trần Văn Long', '1987-08-04', 'Nam', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234610', 'tran.long.gv044@school.edu.vn', 6, 'Đang giảng dạy'),
('GV045', 'Lê Thị Hoa', '1983-06-21', 'Nữ', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234611', 'le.hoa.gv045@school.edu.vn', 6, 'Đang giảng dạy'),
('GV046', 'Phạm Minh Đức', '1986-02-08', 'Nam', '689 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234612', 'pham.duc.gv046@school.edu.vn', 6, 'Đang giảng dạy'),

-- Tổ Công nghệ (4 giáo viên)
('GV047', 'Hoàng Thị Lan', '1984-10-25', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234613', 'hoang.lan.gv047@school.edu.vn', 10, 'Đang giảng dạy'),
('GV048', 'Võ Minh Tuấn', '1988-08-12', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234614', 'vo.tuan.gv048@school.edu.vn', 10, 'Đang giảng dạy'),
('GV049', 'Đặng Thị Mai', '1982-06-29', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234615', 'dang.mai.gv049@school.edu.vn', 10, 'Đang giảng dạy'),
('GV050', 'Bùi Văn Hùng', '1985-04-16', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234616', 'bui.hung.gv050@school.edu.vn', 10, 'Đang giảng dạy'),

-- Tổ Tin học (5 giáo viên)
('GV051', 'Ngô Thị Hương', '1987-12-03', 'Nữ', '134 Nguyễn Du, Q1, TP.HCM', '0901234617', 'ngo.huong.gv051@school.edu.vn', 11, 'Đang giảng dạy'),
('GV052', 'Đinh Minh Nam', '1983-10-20', 'Nam', '245 Lê Văn Việt, Q9, TP.HCM', '0901234618', 'dinh.nam.gv052@school.edu.vn', 11, 'Đang giảng dạy'),
('GV053', 'Phan Thị Thu', '1986-08-07', 'Nữ', '356 Nguyễn Văn Linh, Q7, TP.HCM', '0901234619', 'phan.thu.gv053@school.edu.vn', 11, 'Đang giảng dạy'),
('GV054', 'Lý Minh Đức', '1984-06-24', 'Nam', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234620', 'ly.duc.gv054@school.edu.vn', 11, 'Đang giảng dạy'),
('GV055', 'Vũ Thị Lan', '1988-04-11', 'Nữ', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234621', 'vu.lan.gv055@school.edu.vn', 11, 'Đang giảng dạy'),

-- Tổ Thể dục (4 giáo viên)
('GV056', 'Trịnh Văn Hải', '1982-12-28', 'Nam', '689 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234622', 'trinh.hai.gv056@school.edu.vn', 12, 'Đang giảng dạy'),
('GV057', 'Cao Thị Ngọc', '1985-10-15', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234623', 'cao.ngoc.gv057@school.edu.vn', 12, 'Đang giảng dạy'),
('GV058', 'Lâm Minh Tuấn', '1987-08-02', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234624', 'lam.tuan.gv058@school.edu.vn', 12, 'Đang giảng dạy'),
('GV059', 'Hồ Thị Hoa', '1983-06-19', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234625', 'ho.hoa.gv059@school.edu.vn', 12, 'Đang giảng dạy'),

-- Tổ GDQP-AN (3 giáo viên)
('GV060', 'Dương Văn Long', '1986-04-06', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234626', 'duong.long.gv060@school.edu.vn', 13, 'Đang giảng dạy'),
('GV061', 'Tôn Thị Mai', '1984-12-23', 'Nữ', '134 Nguyễn Du, Q1, TP.HCM', '0901234627', 'ton.mai.gv061@school.edu.vn', 13, 'Đang giảng dạy'),
('GV062', 'Chu Minh Đức', '1988-10-10', 'Nam', '245 Lê Văn Việt, Q9, TP.HCM', '0901234628', 'chu.duc.gv062@school.edu.vn', 13, 'Đang giảng dạy'),

-- Giáo viên bổ sung (8 giáo viên)
('GV063', 'Đỗ Thị Hương', '1982-08-27', 'Nữ', '356 Nguyễn Văn Linh, Q7, TP.HCM', '0901234629', 'do.huong.gv063@school.edu.vn', 1, 'Đang giảng dạy'),
('GV064', 'Nguyễn Văn Nam', '1985-06-14', 'Nam', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234630', 'nguyen.nam.gv064@school.edu.vn', 2, 'Đang giảng dạy'),
('GV065', 'Trần Thị Thu', '1987-04-01', 'Nữ', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234631', 'tran.thu.gv065@school.edu.vn', 3, 'Đang giảng dạy'),
('GV066', 'Lê Minh Hùng', '1983-02-18', 'Nam', '689 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234632', 'le.hung.gv066@school.edu.vn', 7, 'Đang giảng dạy'),
('GV067', 'Phạm Thị Lan', '1986-12-05', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234633', 'pham.lan.gv067@school.edu.vn', 8, 'Đang giảng dạy'),
('GV068', 'Hoàng Văn Đức', '1984-10-22', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234634', 'hoang.duc.gv068@school.edu.vn', 9, 'Đang giảng dạy'),
('GV069', 'Võ Thị Ngọc', '1988-08-09', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234635', 'vo.ngoc.gv069@school.edu.vn', 11, 'Đang giảng dạy'),
('GV070', 'Đặng Minh Hải', '1982-06-26', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234636', 'dang.hai.gv070@school.edu.vn', 12, 'Đang giảng dạy');

SELECT 'Teachers insertion completed (70/70)' AS Status;

-- =====================================================================
-- PHẦN 4: LỚP HỌC (24 lớp: 8 lớp/khối)
-- =====================================================================

INSERT INTO LopHoc (TenLop, MaKhoi, SiSo, MaGiaoVienChuNhiem) VALUES
-- Khối 10 (8 lớp)
('10A1', 10, 42, 'GV001'),
('10A2', 10, 42, 'GV002'),
('10A3', 10, 42, 'GV003'),
('10A4', 10, 42, 'GV004'),
('10A5', 10, 42, 'GV005'),
('10A6', 10, 42, 'GV006'),
('10A7', 10, 42, 'GV007'),
('10A8', 10, 42, 'GV008'),

-- Khối 11 (8 lớp)
('11A1', 11, 42, 'GV009'),
('11A2', 11, 42, 'GV010'),
('11A3', 11, 42, 'GV011'),
('11A4', 11, 42, 'GV012'),
('11A5', 11, 42, 'GV013'),
('11A6', 11, 42, 'GV014'),
('11A7', 11, 42, 'GV015'),
('11A8', 11, 42, 'GV016'),

-- Khối 12 (8 lớp)
('12A1', 12, 42, 'GV017'),
('12A2', 12, 42, 'GV018'),
('12A3', 12, 42, 'GV019'),
('12A4', 12, 42, 'GV020'),
('12A5', 12, 42, 'GV021'),
('12A6', 12, 42, 'GV022'),
('12A7', 12, 42, 'GV023'),
('12A8', 12, 42, 'GV024');

SELECT 'Classes insertion completed (24 classes)' AS Status;

-- =====================================================================
-- PHẦN 5: HỌC SINH MẪU (100 học sinh đầu tiên)
-- =====================================================================

INSERT INTO HocSinh (HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai) VALUES
-- Lớp 10A1 (42 học sinh)
('Nguyễn Văn An', '2009-03-15', 'Nam', '0901235001', 'nguyen.an.hs001@student.edu.vn', 'Đang học'),
('Trần Thị Bình', '2009-07-22', 'Nữ', '0901235002', 'tran.binh.hs002@student.edu.vn', 'Đang học'),
('Lê Minh Cường', '2009-11-08', 'Nam', '0901235003', 'le.cuong.hs003@student.edu.vn', 'Đang học'),
('Phạm Thị Dung', '2009-05-14', 'Nữ', '0901235004', 'pham.dung.hs004@student.edu.vn', 'Đang học'),
('Hoàng Văn Em', '2009-09-30', 'Nam', '0901235005', 'hoang.em.hs005@student.edu.vn', 'Đang học'),
('Võ Thị Phương', '2009-01-18', 'Nữ', '0901235006', 'vo.phuong.hs006@student.edu.vn', 'Đang học'),
('Đặng Minh Giang', '2009-12-25', 'Nam', '0901235007', 'dang.giang.hs007@student.edu.vn', 'Đang học'),
('Bùi Thị Hoa', '2009-04-12', 'Nữ', '0901235008', 'bui.hoa.hs008@student.edu.vn', 'Đang học'),
('Ngô Văn Ích', '2009-08-19', 'Nam', '0901235009', 'ngo.ich.hs009@student.edu.vn', 'Đang học'),
('Đinh Thị Kim', '2009-06-07', 'Nữ', '0901235010', 'dinh.kim.hs010@student.edu.vn', 'Đang học'),
('Phan Văn Long', '2009-02-28', 'Nam', '0901235011', 'phan.long.hs011@student.edu.vn', 'Đang học'),
('Lý Thị Mai', '2009-10-15', 'Nữ', '0901235012', 'ly.mai.hs012@student.edu.vn', 'Đang học'),
('Vũ Minh Nam', '2009-12-03', 'Nam', '0901235013', 'vu.nam.hs013@student.edu.vn', 'Đang học'),
('Trịnh Thị Oanh', '2009-09-21', 'Nữ', '0901235014', 'trinh.oanh.hs014@student.edu.vn', 'Đang học'),
('Cao Văn Phúc', '2009-01-16', 'Nam', '0901235015', 'cao.phuc.hs015@student.edu.vn', 'Đang học'),
('Lâm Thị Quỳnh', '2009-05-24', 'Nữ', '0901235016', 'lam.quynh.hs016@student.edu.vn', 'Đang học'),
('Hồ Minh Rồng', '2009-11-11', 'Nam', '0901235017', 'ho.rong.hs017@student.edu.vn', 'Đang học'),
('Dương Thị Sương', '2009-07-08', 'Nữ', '0901235018', 'duong.suong.hs018@student.edu.vn', 'Đang học'),
('Tôn Văn Tâm', '2009-03-26', 'Nam', '0901235019', 'ton.tam.hs019@student.edu.vn', 'Đang học'),
('Chu Thị Uyên', '2009-09-13', 'Nữ', '0901235020', 'chu.uyen.hs020@student.edu.vn', 'Đang học'),
('Đỗ Minh Vinh', '2009-08-05', 'Nam', '0901235021', 'do.vinh.hs021@student.edu.vn', 'Đang học'),
('Nguyễn Thị Xuân', '2009-04-22', 'Nữ', '0901235022', 'nguyen.xuan.hs022@student.edu.vn', 'Đang học'),
('Trần Văn Yên', '2009-12-09', 'Nam', '0901235023', 'tran.yen.hs023@student.edu.vn', 'Đang học'),
('Lê Thị Zara', '2009-06-16', 'Nữ', '0901235024', 'le.zara.hs024@student.edu.vn', 'Đang học'),
('Phạm Minh Anh', '2009-02-03', 'Nam', '0901235025', 'pham.anh.hs025@student.edu.vn', 'Đang học'),
('Hoàng Thị Bảo', '2009-10-20', 'Nữ', '0901235026', 'hoang.bao.hs026@student.edu.vn', 'Đang học'),
('Võ Văn Cường', '2009-07-17', 'Nam', '0901235027', 'vo.cuong.hs027@student.edu.vn', 'Đang học'),
('Đặng Thị Dung', '2009-05-04', 'Nữ', '0901235028', 'dang.dung.hs028@student.edu.vn', 'Đang học'),
('Bùi Minh Em', '2009-01-11', 'Nam', '0901235029', 'bui.em.hs029@student.edu.vn', 'Đang học'),
('Ngô Thị Phương', '2009-09-28', 'Nữ', '0901235030', 'ngo.phuong.hs030@student.edu.vn', 'Đang học'),
('Đinh Văn Giang', '2009-08-15', 'Nam', '0901235031', 'dinh.giang.hs031@student.edu.vn', 'Đang học'),
('Phan Thị Hoa', '2009-04-02', 'Nữ', '0901235032', 'phan.hoa.hs032@student.edu.vn', 'Đang học'),
('Lý Minh Ích', '2009-12-19', 'Nam', '0901235033', 'ly.ich.hs033@student.edu.vn', 'Đang học'),
('Vũ Thị Kim', '2009-06-06', 'Nữ', '0901235034', 'vu.kim.hs034@student.edu.vn', 'Đang học'),
('Trịnh Văn Long', '2009-02-23', 'Nam', '0901235035', 'trinh.long.hs035@student.edu.vn', 'Đang học'),
('Cao Thị Mai', '2009-10-10', 'Nữ', '0901235036', 'cao.mai.hs036@student.edu.vn', 'Đang học'),
('Lâm Minh Nam', '2009-08-27', 'Nam', '0901235037', 'lam.nam.hs037@student.edu.vn', 'Đang học'),
('Hồ Thị Oanh', '2009-04-14', 'Nữ', '0901235038', 'ho.oanh.hs038@student.edu.vn', 'Đang học'),
('Dương Văn Phúc', '2009-12-01', 'Nam', '0901235039', 'duong.phuc.hs039@student.edu.vn', 'Đang học'),
('Tôn Thị Quỳnh', '2009-06-18', 'Nữ', '0901235040', 'ton.quynh.hs040@student.edu.vn', 'Đang học'),
('Chu Minh Rồng', '2009-02-05', 'Nam', '0901235041', 'chu.rong.hs041@student.edu.vn', 'Đang học'),
('Đỗ Thị Sương', '2009-10-22', 'Nữ', '0901235042', 'do.suong.hs042@student.edu.vn', 'Đang học'),

-- Lớp 10A2 (42 học sinh)
('Nguyễn Văn Anh', '2009-03-16', 'Nam', '0901235043', 'nguyen.anh.hs043@student.edu.vn', 'Đang học'),
('Trần Thị Bình', '2009-07-23', 'Nữ', '0901235044', 'tran.binh.hs044@student.edu.vn', 'Đang học'),
('Lê Minh Cường', '2009-11-09', 'Nam', '0901235045', 'le.cuong.hs045@student.edu.vn', 'Đang học'),
('Phạm Thị Dung', '2009-05-15', 'Nữ', '0901235046', 'pham.dung.hs046@student.edu.vn', 'Đang học'),
('Hoàng Văn Em', '2009-10-01', 'Nam', '0901235047', 'hoang.em.hs047@student.edu.vn', 'Đang học'),
('Võ Thị Phương', '2009-01-19', 'Nữ', '0901235048', 'vo.phuong.hs048@student.edu.vn', 'Đang học'),
('Đặng Minh Giang', '2009-12-26', 'Nam', '0901235049', 'dang.giang.hs049@student.edu.vn', 'Đang học'),
('Bùi Thị Hoa', '2009-04-13', 'Nữ', '0901235050', 'bui.hoa.hs050@student.edu.vn', 'Đang học'),
('Ngô Văn Ích', '2009-08-20', 'Nam', '0901235051', 'ngo.ich.hs051@student.edu.vn', 'Đang học'),
('Đinh Thị Kim', '2009-06-08', 'Nữ', '0901235052', 'dinh.kim.hs052@student.edu.vn', 'Đang học'),
('Phan Văn Long', '2009-03-01', 'Nam', '0901235053', 'phan.long.hs053@student.edu.vn', 'Đang học'),
('Lý Thị Mai', '2009-10-16', 'Nữ', '0901235054', 'ly.mai.hs054@student.edu.vn', 'Đang học'),
('Vũ Minh Nam', '2009-12-04', 'Nam', '0901235055', 'vu.nam.hs055@student.edu.vn', 'Đang học'),
('Trịnh Thị Oanh', '2009-09-22', 'Nữ', '0901235056', 'trinh.oanh.hs056@student.edu.vn', 'Đang học'),
('Cao Văn Phúc', '2009-01-17', 'Nam', '0901235057', 'cao.phuc.hs057@student.edu.vn', 'Đang học'),
('Lâm Thị Quỳnh', '2009-05-25', 'Nữ', '0901235058', 'lam.quynh.hs058@student.edu.vn', 'Đang học'),
('Hồ Minh Rồng', '2009-11-12', 'Nam', '0901235059', 'ho.rong.hs059@student.edu.vn', 'Đang học'),
('Dương Thị Sương', '2009-07-09', 'Nữ', '0901235060', 'duong.suong.hs060@student.edu.vn', 'Đang học'),
('Tôn Văn Tâm', '2009-03-27', 'Nam', '0901235061', 'ton.tam.hs061@student.edu.vn', 'Đang học'),
('Chu Thị Uyên', '2009-09-14', 'Nữ', '0901235062', 'chu.uyen.hs062@student.edu.vn', 'Đang học'),
('Đỗ Minh Vinh', '2009-08-06', 'Nam', '0901235063', 'do.vinh.hs063@student.edu.vn', 'Đang học'),
('Nguyễn Thị Xuân', '2009-04-23', 'Nữ', '0901235064', 'nguyen.xuan.hs064@student.edu.vn', 'Đang học'),
('Trần Văn Yên', '2009-12-10', 'Nam', '0901235065', 'tran.yen.hs065@student.edu.vn', 'Đang học'),
('Lê Thị Zara', '2009-06-17', 'Nữ', '0901235066', 'le.zara.hs066@student.edu.vn', 'Đang học'),
('Phạm Minh Anh', '2009-02-04', 'Nam', '0901235067', 'pham.anh.hs067@student.edu.vn', 'Đang học'),
('Hoàng Thị Bảo', '2009-10-21', 'Nữ', '0901235068', 'hoang.bao.hs068@student.edu.vn', 'Đang học'),
('Võ Văn Cường', '2009-07-18', 'Nam', '0901235069', 'vo.cuong.hs069@student.edu.vn', 'Đang học'),
('Đặng Thị Dung', '2009-05-05', 'Nữ', '0901235070', 'dang.dung.hs070@student.edu.vn', 'Đang học'),
('Bùi Minh Em', '2009-01-12', 'Nam', '0901235071', 'bui.em.hs071@student.edu.vn', 'Đang học'),
('Ngô Thị Phương', '2009-09-29', 'Nữ', '0901235072', 'ngo.phuong.hs072@student.edu.vn', 'Đang học'),
('Đinh Văn Giang', '2009-08-16', 'Nam', '0901235073', 'dinh.giang.hs073@student.edu.vn', 'Đang học'),
('Phan Thị Hoa', '2009-04-03', 'Nữ', '0901235074', 'phan.hoa.hs074@student.edu.vn', 'Đang học'),
('Lý Minh Ích', '2009-12-20', 'Nam', '0901235075', 'ly.ich.hs075@student.edu.vn', 'Đang học'),
('Vũ Thị Kim', '2009-06-07', 'Nữ', '0901235076', 'vu.kim.hs076@student.edu.vn', 'Đang học'),
('Trịnh Văn Long', '2009-02-24', 'Nam', '0901235077', 'trinh.long.hs077@student.edu.vn', 'Đang học'),
('Cao Thị Mai', '2009-10-11', 'Nữ', '0901235078', 'cao.mai.hs078@student.edu.vn', 'Đang học'),
('Lâm Minh Nam', '2009-08-28', 'Nam', '0901235079', 'lam.nam.hs079@student.edu.vn', 'Đang học'),
('Hồ Thị Oanh', '2009-04-15', 'Nữ', '0901235080', 'ho.oanh.hs080@student.edu.vn', 'Đang học'),
('Dương Văn Phúc', '2009-12-02', 'Nam', '0901235081', 'duong.phuc.hs081@student.edu.vn', 'Đang học'),
('Tôn Thị Quỳnh', '2009-06-19', 'Nữ', '0901235082', 'ton.quynh.hs082@student.edu.vn', 'Đang học'),
('Chu Minh Rồng', '2009-02-06', 'Nam', '0901235083', 'chu.rong.hs083@student.edu.vn', 'Đang học'),
('Đỗ Thị Sương', '2009-10-23', 'Nữ', '0901235084', 'do.suong.hs084@student.edu.vn', 'Đang học');

-- Lớp 10A3 - 10A8, 11A1 - 11A8, 12A1 - 12A8 (nhập tối giản)
-- Tạo thêm 918 học sinh (≈42 HS/lớp × 22 lớp)
-- Tổng cộng: 84 + 918 = 1002 học sinh

INSERT INTO HocSinh (HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai)
SELECT 
    CONCAT('Học sinh ', 85 + (t.n * 1) + (r.n * 42)) as HoTen,
    DATE_ADD('2009-01-01', INTERVAL FLOOR(RAND() * 365) DAY) as NgaySinh,
    IF(RAND() > 0.5, 'Nam', 'Nữ') as GioiTinh,
    CONCAT('09', LPAD(585001 + t.n * 1 + r.n * 42, 7, '0')) as SDTHS,
    CONCAT('hs', LPAD(85 + t.n * 1 + r.n * 42, 6, '0'), '@student.edu.vn') as Email,
    'Đang học' as TrangThai
FROM (SELECT 0 n UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12 UNION SELECT 13 UNION SELECT 14 UNION SELECT 15 UNION SELECT 16 UNION SELECT 17 UNION SELECT 18 UNION SELECT 19 UNION SELECT 20 UNION SELECT 21 UNION SELECT 22 UNION SELECT 23 UNION SELECT 24 UNION SELECT 25 UNION SELECT 26 UNION SELECT 27 UNION SELECT 28 UNION SELECT 29 UNION SELECT 30 UNION SELECT 31 UNION SELECT 32 UNION SELECT 33 UNION SELECT 34 UNION SELECT 35 UNION SELECT 36 UNION SELECT 37 UNION SELECT 38 UNION SELECT 39 UNION SELECT 40 UNION SELECT 41) t
CROSS JOIN (SELECT 0 n UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12 UNION SELECT 13 UNION SELECT 14 UNION SELECT 15 UNION SELECT 16 UNION SELECT 17 UNION SELECT 18 UNION SELECT 19 UNION SELECT 20 UNION SELECT 21) r
LIMIT 924;

SELECT 'Students insertion completed (1008 students)' AS Status;

-- =====================================================================
-- PHẦN 5B: TẠO TÀI KHOẢN ĐĂNG NHẬP CHO TẤT CẢ HỌC SINH
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

SELECT 'Student accounts created (1008 accounts)' AS Status;

-- Cập nhật TenDangNhap cho TẤT CẢ học sinh
UPDATE HocSinh 
SET TenDangNhap = CONCAT('HS', MaHocSinh);

SELECT 'Student TenDangNhap updated (1008 students)' AS Status;

-- Gán vai trò 'student' cho TẤT CẢ tài khoản học sinh
INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro)
SELECT 
    CONCAT('HS', MaHocSinh) as TenDangNhap,
    'student' as MaVaiTro
FROM HocSinh
ORDER BY MaHocSinh;

SELECT 'Student roles assigned (1008 roles)' AS Status;

-- =====================================================================
-- PHẦN 6: PHỤ HUYNH VÀ LIÊN KẾT
-- =====================================================================

-- Phụ huynh (1 phụ huynh cho mỗi học sinh) - phần tối giản cho 1008 HS
INSERT INTO PhuHuynh (HoTen, SoDienThoai, Email, DiaChi) VALUES
('Nguyễn Văn Anh', '0901236001', 'nguyen.anh.ph001@parent.edu.vn', '123 Nguyễn Huệ, Q1, TP.HCM'),
('Trần Thị Bình', '0901236002', 'tran.binh.ph002@parent.edu.vn', '456 Lê Lợi, Q1, TP.HCM'),
('Lê Minh Cường', '0901236003', 'le.cuong.ph003@parent.edu.vn', '789 Đồng Khởi, Q1, TP.HCM'),
('Phạm Thị Dung', '0901236004', 'pham.dung.ph004@parent.edu.vn', '321 Nguyễn Thị Minh Khai, Q3, TP.HCM'),
('Hoàng Văn Em', '0901236005', 'hoang.em.ph005@parent.edu.vn', '654 Cách Mạng Tháng 8, Q10, TP.HCM'),
('Võ Thị Phương', '0901236006', 'vo.phuong.ph006@parent.edu.vn', '987 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM'),
('Đặng Minh Giang', '0901236007', 'dang.giang.ph007@parent.edu.vn', '147 Nguyễn Văn Cừ, Q5, TP.HCM'),
('Bùi Thị Hoa', '0901236008', 'bui.hoa.ph008@parent.edu.vn', '258 Trần Hưng Đạo, Q5, TP.HCM'),
('Ngô Văn Ích', '0901236009', 'ngo.ich.ph009@parent.edu.vn', '369 Lý Tự Trọng, Q1, TP.HCM'),
('Đinh Thị Kim', '0901236010', 'dinh.kim.ph010@parent.edu.vn', '741 Pasteur, Q3, TP.HCM'),
('Phan Văn Long', '0901236011', 'phan.long.ph011@parent.edu.vn', '852 Nguyễn Du, Q1, TP.HCM'),
('Lý Thị Mai', '0901236012', 'ly.mai.ph012@parent.edu.vn', '963 Đinh Tiên Hoàng, Q.Bình Thạnh, TP.HCM'),
('Vũ Minh Nam', '0901236013', 'vu.nam.ph013@parent.edu.vn', '159 Nguyễn Thị Thập, Q7, TP.HCM'),
('Trịnh Thị Oanh', '0901236014', 'trinh.oanh.ph014@parent.edu.vn', '357 Võ Văn Tần, Q3, TP.HCM'),
('Cao Văn Phúc', '0901236015', 'cao.phuc.ph015@parent.edu.vn', '468 Nguyễn Đình Chiểu, Q3, TP.HCM'),
('Lâm Thị Quỳnh', '0901236016', 'lam.quynh.ph016@parent.edu.vn', '579 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM'),
('Hồ Minh Rồng', '0901236017', 'ho.rong.ph017@parent.edu.vn', '680 Nguyễn Văn Linh, Q7, TP.HCM'),
('Dương Thị Sương', '0901236018', 'duong.suong.ph018@parent.edu.vn', '791 Lê Văn Việt, Q9, TP.HCM'),
('Tôn Văn Tâm', '0901236019', 'ton.tam.ph019@parent.edu.vn', '802 Nguyễn Thị Minh Khai, Q3, TP.HCM'),
('Chu Thị Uyên', '0901236020', 'chu.uyen.ph020@parent.edu.vn', '913 Cách Mạng Tháng 8, Q10, TP.HCM'),
('Đỗ Minh Vinh', '0901236021', 'do.vinh.ph021@parent.edu.vn', '124 Nguyễn Huệ, Q1, TP.HCM'),
('Nguyễn Thị Xuân', '0901236022', 'nguyen.xuan.ph022@parent.edu.vn', '235 Lê Lợi, Q1, TP.HCM'),
('Trần Văn Yên', '0901236023', 'tran.yen.ph023@parent.edu.vn', '346 Đồng Khởi, Q1, TP.HCM'),
('Lê Thị Zara', '0901236024', 'le.zara.ph024@parent.edu.vn', '457 Nguyễn Thị Minh Khai, Q3, TP.HCM'),
('Phạm Minh Anh', '0901236025', 'pham.anh.ph025@parent.edu.vn', '568 Cách Mạng Tháng 8, Q10, TP.HCM'),
('Hoàng Thị Bảo', '0901236026', 'hoang.bao.ph026@parent.edu.vn', '679 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM'),
('Võ Văn Cường', '0901236027', 'vo.cuong.ph027@parent.edu.vn', '790 Nguyễn Văn Cừ, Q5, TP.HCM'),
('Đặng Thị Dung', '0901236028', 'dang.dung.ph028@parent.edu.vn', '801 Trần Hưng Đạo, Q5, TP.HCM'),
('Bùi Minh Em', '0901236029', 'bui.em.ph029@parent.edu.vn', '912 Lý Tự Trọng, Q1, TP.HCM'),
('Ngô Thị Phương', '0901236030', 'ngo.phuong.ph030@parent.edu.vn', '023 Pasteur, Q3, TP.HCM'),
('Đinh Văn Giang', '0901236031', 'dinh.giang.ph031@parent.edu.vn', '134 Nguyễn Du, Q1, TP.HCM'),
('Phan Thị Hoa', '0901236032', 'phan.hoa.ph032@parent.edu.vn', '245 Lê Văn Việt, Q9, TP.HCM'),
('Lý Minh Ích', '0901236033', 'ly.ich.ph033@parent.edu.vn', '356 Nguyễn Văn Linh, Q7, TP.HCM'),
('Vũ Thị Kim', '0901236034', 'vu.kim.ph034@parent.edu.vn', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM'),
('Trịnh Văn Long', '0901236035', 'trinh.long.ph035@parent.edu.vn', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM'),
('Cao Thị Mai', '0901236036', 'cao.mai.ph036@parent.edu.vn', '689 Cách Mạng Tháng 8, Q10, TP.HCM'),
('Lâm Minh Nam', '0901236037', 'lam.nam.ph037@parent.edu.vn', '790 Nguyễn Văn Cừ, Q5, TP.HCM'),
('Hồ Thị Oanh', '0901236038', 'ho.oanh.ph038@parent.edu.vn', '801 Trần Hưng Đạo, Q5, TP.HCM'),
('Dương Văn Phúc', '0901236039', 'duong.phuc.ph039@parent.edu.vn', '912 Lý Tự Trọng, Q1, TP.HCM'),
('Tôn Thị Quỳnh', '0901236040', 'ton.quynh.ph040@parent.edu.vn', '023 Pasteur, Q3, TP.HCM'),
('Chu Minh Rồng', '0901236041', 'chu.rong.ph041@parent.edu.vn', '134 Nguyễn Du, Q1, TP.HCM'),
('Đỗ Thị Sương', '0901236042', 'do.suong.ph042@parent.edu.vn', '245 Lê Văn Việt, Q9, TP.HCM'),

-- Phụ huynh lớp 10A2 (42 phụ huynh)
('Nguyễn Văn Anh', '0901236043', 'nguyen.anh.ph043@parent.edu.vn', '123 Nguyễn Huệ, Q1, TP.HCM'),
('Trần Thị Bình', '0901236044', 'tran.binh.ph044@parent.edu.vn', '456 Lê Lợi, Q1, TP.HCM'),
('Lê Minh Cường', '0901236045', 'le.cuong.ph045@parent.edu.vn', '789 Đồng Khởi, Q1, TP.HCM'),
('Phạm Thị Dung', '0901236046', 'pham.dung.ph046@parent.edu.vn', '321 Nguyễn Thị Minh Khai, Q3, TP.HCM'),
('Hoàng Văn Em', '0901236047', 'hoang.em.ph047@parent.edu.vn', '654 Cách Mạng Tháng 8, Q10, TP.HCM'),
('Võ Thị Phương', '0901236048', 'vo.phuong.ph048@parent.edu.vn', '987 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM'),
('Đặng Minh Giang', '0901236049', 'dang.giang.ph049@parent.edu.vn', '147 Nguyễn Văn Cừ, Q5, TP.HCM'),
('Bùi Thị Hoa', '0901236050', 'bui.hoa.ph050@parent.edu.vn', '258 Trần Hưng Đạo, Q5, TP.HCM'),
('Ngô Văn Ích', '0901236051', 'ngo.ich.ph051@parent.edu.vn', '369 Lý Tự Trọng, Q1, TP.HCM'),
('Đinh Thị Kim', '0901236052', 'dinh.kim.ph052@parent.edu.vn', '741 Pasteur, Q3, TP.HCM'),
('Phan Văn Long', '0901236053', 'phan.long.ph053@parent.edu.vn', '852 Nguyễn Du, Q1, TP.HCM'),
('Lý Thị Mai', '0901236054', 'ly.mai.ph054@parent.edu.vn', '963 Đinh Tiên Hoàng, Q.Bình Thạnh, TP.HCM'),
('Vũ Minh Nam', '0901236055', 'vu.nam.ph055@parent.edu.vn', '159 Nguyễn Thị Thập, Q7, TP.HCM'),
('Trịnh Thị Oanh', '0901236056', 'trinh.oanh.ph056@parent.edu.vn', '357 Võ Văn Tần, Q3, TP.HCM'),
('Cao Văn Phúc', '0901236057', 'cao.phuc.ph057@parent.edu.vn', '468 Nguyễn Đình Chiểu, Q3, TP.HCM'),
('Lâm Thị Quỳnh', '0901236058', 'lam.quynh.ph058@parent.edu.vn', '579 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM'),
('Hồ Minh Rồng', '0901236059', 'ho.rong.ph059@parent.edu.vn', '680 Nguyễn Văn Linh, Q7, TP.HCM'),
('Dương Thị Sương', '0901236060', 'duong.suong.ph060@parent.edu.vn', '791 Lê Văn Việt, Q9, TP.HCM'),
('Tôn Văn Tâm', '0901236061', 'ton.tam.ph061@parent.edu.vn', '802 Nguyễn Thị Minh Khai, Q3, TP.HCM'),
('Chu Thị Uyên', '0901236062', 'chu.uyen.ph062@parent.edu.vn', '913 Cách Mạng Tháng 8, Q10, TP.HCM'),
('Đỗ Minh Vinh', '0901236063', 'do.vinh.ph063@parent.edu.vn', '124 Nguyễn Huệ, Q1, TP.HCM'),
('Nguyễn Thị Xuân', '0901236064', 'nguyen.xuan.ph064@parent.edu.vn', '235 Lê Lợi, Q1, TP.HCM'),
('Trần Văn Yên', '0901236065', 'tran.yen.ph065@parent.edu.vn', '346 Đồng Khởi, Q1, TP.HCM'),
('Lê Thị Zara', '0901236066', 'le.zara.ph066@parent.edu.vn', '457 Nguyễn Thị Minh Khai, Q3, TP.HCM'),
('Phạm Minh Anh', '0901236067', 'pham.anh.ph067@parent.edu.vn', '568 Cách Mạng Tháng 8, Q10, TP.HCM'),
('Hoàng Thị Bảo', '0901236068', 'hoang.bao.ph068@parent.edu.vn', '679 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM'),
('Võ Văn Cường', '0901236069', 'vo.cuong.ph069@parent.edu.vn', '790 Nguyễn Văn Cừ, Q5, TP.HCM'),
('Đặng Thị Dung', '0901236070', 'dang.dung.ph070@parent.edu.vn', '801 Trần Hưng Đạo, Q5, TP.HCM'),
('Bùi Minh Em', '0901236071', 'bui.em.ph071@parent.edu.vn', '912 Lý Tự Trọng, Q1, TP.HCM'),
('Ngô Thị Phương', '0901236072', 'ngo.phuong.ph072@parent.edu.vn', '023 Pasteur, Q3, TP.HCM'),
('Đinh Văn Giang', '0901236073', 'dinh.giang.ph073@parent.edu.vn', '134 Nguyễn Du, Q1, TP.HCM'),
('Phan Thị Hoa', '0901236074', 'phan.hoa.ph074@parent.edu.vn', '245 Lê Văn Việt, Q9, TP.HCM'),
('Lý Minh Ích', '0901236075', 'ly.ich.ph075@parent.edu.vn', '356 Nguyễn Văn Linh, Q7, TP.HCM'),
('Vũ Thị Kim', '0901236076', 'vu.kim.ph076@parent.edu.vn', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM'),
('Trịnh Văn Long', '0901236077', 'trinh.long.ph077@parent.edu.vn', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM'),
('Cao Thị Mai', '0901236078', 'cao.mai.ph078@parent.edu.vn', '689 Cách Mạng Tháng 8, Q10, TP.HCM'),
('Lâm Minh Nam', '0901236079', 'lam.nam.ph079@parent.edu.vn', '790 Nguyễn Văn Cừ, Q5, TP.HCM'),
('Hồ Thị Oanh', '0901236080', 'ho.oanh.ph080@parent.edu.vn', '801 Trần Hưng Đạo, Q5, TP.HCM'),
('Dương Văn Phúc', '0901236081', 'duong.phuc.ph081@parent.edu.vn', '912 Lý Tự Trọng, Q1, TP.HCM'),
('Tôn Thị Quỳnh', '0901236082', 'ton.quynh.ph082@parent.edu.vn', '023 Pasteur, Q3, TP.HCM'),
('Chu Minh Rồng', '0901236083', 'chu.rong.ph083@parent.edu.vn', '134 Nguyễn Du, Q1, TP.HCM'),
('Đỗ Thị Sương', '0901236084', 'do.suong.ph084@parent.edu.vn', '245 Lê Văn Việt, Q9, TP.HCM'),

-- Phụ huynh cho 924 học sinh còn lại (tạo tự động)
('Phụ huynh HS085', '0901236085', 'ph.ph085@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS086', '0901236086', 'ph.ph086@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS087', '0901236087', 'ph.ph087@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS088', '0901236088', 'ph.ph088@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS089', '0901236089', 'ph.ph089@parent.edu.vn', 'TP.HCM'),
('Phụ huynh HS090', '0901236090', 'ph.ph090@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS091', '0901236091', 'ph.ph091@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS092', '0901236092', 'ph.ph092@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS093', '0901236093', 'ph.ph093@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS094', '0901236094', 'ph.ph094@parent.edu.vn', 'TP.HCM'),
('Phụ huynh HS095', '0901236095', 'ph.ph095@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS096', '0901236096', 'ph.ph096@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS097', '0901236097', 'ph.ph097@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS098', '0901236098', 'ph.ph098@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS099', '0901236099', 'ph.ph099@parent.edu.vn', 'TP.HCM'),
('Phụ huynh HS100', '0901236100', 'ph.ph100@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS101', '0901236101', 'ph.ph101@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS102', '0901236102', 'ph.ph102@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS103', '0901236103', 'ph.ph103@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS104', '0901236104', 'ph.ph104@parent.edu.vn', 'TP.HCM'),
('Phụ huynh HS105', '0901236105', 'ph.ph105@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS106', '0901236106', 'ph.ph106@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS107', '0901236107', 'ph.ph107@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS108', '0901236108', 'ph.ph108@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS109', '0901236109', 'ph.ph109@parent.edu.vn', 'TP.HCM'),
('Phụ huynh HS110', '0901236110', 'ph.ph110@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS111', '0901236111', 'ph.ph111@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS112', '0901236112', 'ph.ph112@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS113', '0901236113', 'ph.ph113@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS114', '0901236114', 'ph.ph114@parent.edu.vn', 'TP.HCM'),
('Phụ huynh HS115', '0901236115', 'ph.ph115@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS116', '0901236116', 'ph.ph116@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS117', '0901236117', 'ph.ph117@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS118', '0901236118', 'ph.ph118@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS119', '0901236119', 'ph.ph119@parent.edu.vn', 'TP.HCM'),
('Phụ huynh HS120', '0901236120', 'ph.ph120@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS121', '0901236121', 'ph.ph121@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS122', '0901236122', 'ph.ph122@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS123', '0901236123', 'ph.ph123@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS124', '0901236124', 'ph.ph124@parent.edu.vn', 'TP.HCM'),
('Phụ huynh HS125', '0901236125', 'ph.ph125@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS126', '0901236126', 'ph.ph126@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS127', '0901236127', 'ph.ph127@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS128', '0901236128', 'ph.ph128@parent.edu.vn', 'TP.HCM'), ('Phụ huynh HS129', '0901236129', 'ph.ph129@parent.edu.vn', 'TP.HCM');

-- Tạo tự động phụ huynh còn lại (885 phụ huynh)
INSERT INTO PhuHuynh (HoTen, SoDienThoai, Email, DiaChi)
SELECT 
    CONCAT('Phụ huynh HS', LPAD(130 + t.n + r.n * 42, 6, '0')) as HoTen,
    CONCAT('09', LPAD(586130 + t.n + r.n * 42, 7, '0')) as SoDienThoai,
    CONCAT('ph.ph', LPAD(130 + t.n + r.n * 42, 6, '0'), '@parent.edu.vn') as Email,
    'TP.HCM' as DiaChi
FROM (SELECT 0 n UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12 UNION SELECT 13 UNION SELECT 14 UNION SELECT 15 UNION SELECT 16 UNION SELECT 17 UNION SELECT 18 UNION SELECT 19 UNION SELECT 20 UNION SELECT 21 UNION SELECT 22 UNION SELECT 23 UNION SELECT 24 UNION SELECT 25 UNION SELECT 26 UNION SELECT 27 UNION SELECT 28 UNION SELECT 29 UNION SELECT 30 UNION SELECT 31 UNION SELECT 32 UNION SELECT 33 UNION SELECT 34 UNION SELECT 35 UNION SELECT 36 UNION SELECT 37 UNION SELECT 38 UNION SELECT 39 UNION SELECT 40 UNION SELECT 41) t
CROSS JOIN (SELECT 0 n UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12 UNION SELECT 13 UNION SELECT 14 UNION SELECT 15 UNION SELECT 16 UNION SELECT 17 UNION SELECT 18 UNION SELECT 19 UNION SELECT 20) r
LIMIT 840;

-- Liên kết học sinh - phụ huynh
INSERT INTO HocSinhPhuHuynh (MaHocSinh, MaPhuHuynh, MoiQuanHe) VALUES
(1, 1, 'Cha/Mẹ'), (2, 2, 'Cha/Mẹ'), (3, 3, 'Cha/Mẹ'), (4, 4, 'Cha/Mẹ'), (5, 5, 'Cha/Mẹ'),
(6, 6, 'Cha/Mẹ'), (7, 7, 'Cha/Mẹ'), (8, 8, 'Cha/Mẹ'), (9, 9, 'Cha/Mẹ'), (10, 10, 'Cha/Mẹ'),
(11, 11, 'Cha/Mẹ'), (12, 12, 'Cha/Mẹ'), (13, 13, 'Cha/Mẹ'), (14, 14, 'Cha/Mẹ'), (15, 15, 'Cha/Mẹ'),
(16, 16, 'Cha/Mẹ'), (17, 17, 'Cha/Mẹ'), (18, 18, 'Cha/Mẹ'), (19, 19, 'Cha/Mẹ'), (20, 20, 'Cha/Mẹ'),
(21, 21, 'Cha/Mẹ'), (22, 22, 'Cha/Mẹ'), (23, 23, 'Cha/Mẹ'), (24, 24, 'Cha/Mẹ'), (25, 25, 'Cha/Mẹ'),
(26, 26, 'Cha/Mẹ'), (27, 27, 'Cha/Mẹ'), (28, 28, 'Cha/Mẹ'), (29, 29, 'Cha/Mẹ'), (30, 30, 'Cha/Mẹ'),
(31, 31, 'Cha/Mẹ'), (32, 32, 'Cha/Mẹ'), (33, 33, 'Cha/Mẹ'), (34, 34, 'Cha/Mẹ'), (35, 35, 'Cha/Mẹ'),
(36, 36, 'Cha/Mẹ'), (37, 37, 'Cha/Mẹ'), (38, 38, 'Cha/Mẹ'), (39, 39, 'Cha/Mẹ'), (40, 40, 'Cha/Mẹ'),
(41, 41, 'Cha/Mẹ'), (42, 42, 'Cha/Mẹ'),

-- Liên kết học sinh lớp 10A2 - phụ huynh
(43, 43, 'Cha/Mẹ'), (44, 44, 'Cha/Mẹ'), (45, 45, 'Cha/Mẹ'), (46, 46, 'Cha/Mẹ'), (47, 47, 'Cha/Mẹ'),
(48, 48, 'Cha/Mẹ'), (49, 49, 'Cha/Mẹ'), (50, 50, 'Cha/Mẹ'), (51, 51, 'Cha/Mẹ'), (52, 52, 'Cha/Mẹ'),
(53, 53, 'Cha/Mẹ'), (54, 54, 'Cha/Mẹ'), (55, 55, 'Cha/Mẹ'), (56, 56, 'Cha/Mẹ'), (57, 57, 'Cha/Mẹ'),
(58, 58, 'Cha/Mẹ'), (59, 59, 'Cha/Mẹ'), (60, 60, 'Cha/Mẹ'), (61, 61, 'Cha/Mẹ'), (62, 62, 'Cha/Mẹ'),
(63, 63, 'Cha/Mẹ'), (64, 64, 'Cha/Mẹ'), (65, 65, 'Cha/Mẹ'), (66, 66, 'Cha/Mẹ'), (67, 67, 'Cha/Mẹ'),
(68, 68, 'Cha/Mẹ'), (69, 69, 'Cha/Mẹ'), (70, 70, 'Cha/Mẹ'), (71, 71, 'Cha/Mẹ'), (72, 72, 'Cha/Mẹ'),
(73, 73, 'Cha/Mẹ'), (74, 74, 'Cha/Mẹ'), (75, 75, 'Cha/Mẹ'), (76, 76, 'Cha/Mẹ'), (77, 77, 'Cha/Mẹ'),
(78, 78, 'Cha/Mẹ'), (79, 79, 'Cha/Mẹ'), (80, 80, 'Cha/Mẹ'), (81, 81, 'Cha/Mẹ'), (82, 82, 'Cha/Mẹ'),
(83, 83, 'Cha/Mẹ'), (84, 84, 'Cha/Mẹ');

-- Phân lớp học sinh vào lớp 10A1
INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy) VALUES
(1, 1, 1), (2, 1, 1), (3, 1, 1), (4, 1, 1), (5, 1, 1), (6, 1, 1), (7, 1, 1), (8, 1, 1),
(9, 1, 1), (10, 1, 1), (11, 1, 1), (12, 1, 1), (13, 1, 1), (14, 1, 1), (15, 1, 1), (16, 1, 1),
(17, 1, 1), (18, 1, 1), (19, 1, 1), (20, 1, 1), (21, 1, 1), (22, 1, 1), (23, 1, 1), (24, 1, 1),
(25, 1, 1), (26, 1, 1), (27, 1, 1), (28, 1, 1), (29, 1, 1), (30, 1, 1), (31, 1, 1), (32, 1, 1),
(33, 1, 1), (34, 1, 1), (35, 1, 1), (36, 1, 1), (37, 1, 1), (38, 1, 1), (39, 1, 1), (40, 1, 1),
(41, 1, 1), (42, 1, 1);

-- Phân lớp học sinh vào lớp 10A2
INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy) VALUES
(43, 2, 1), (44, 2, 1), (45, 2, 1), (46, 2, 1), (47, 2, 1), (48, 2, 1), (49, 2, 1), (50, 2, 1),
(51, 2, 1), (52, 2, 1), (53, 2, 1), (54, 2, 1), (55, 2, 1), (56, 2, 1), (57, 2, 1), (58, 2, 1),
(59, 2, 1), (60, 2, 1), (61, 2, 1), (62, 2, 1), (63, 2, 1), (64, 2, 1), (65, 2, 1), (66, 2, 1),
(67, 2, 1), (68, 2, 1), (69, 2, 1), (70, 2, 1), (71, 2, 1), (72, 2, 1), (73, 2, 1), (74, 2, 1),
(75, 2, 1), (76, 2, 1), (77, 2, 1), (78, 2, 1), (79, 2, 1), (80, 2, 1), (81, 2, 1), (82, 2, 1),
(83, 2, 1), (84, 2, 1);

-- Phân lớp cho các lớp còn lại (10A3-10A8, 11A1-11A8, 12A1-12A8)
-- Mỗi lớp 42 học sinh
INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy)
SELECT 
    (85 + t.n + r.n * 42) as MaHocSinh,
    (3 + FLOOR((85 + t.n + r.n * 42 - 85) / 42)) as MaLop,  -- 3-24: 10A3-12A8
    1 as MaHocKy
FROM (SELECT 0 n UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12 UNION SELECT 13 UNION SELECT 14 UNION SELECT 15 UNION SELECT 16 UNION SELECT 17 UNION SELECT 18 UNION SELECT 19 UNION SELECT 20 UNION SELECT 21 UNION SELECT 22 UNION SELECT 23 UNION SELECT 24 UNION SELECT 25 UNION SELECT 26 UNION SELECT 27 UNION SELECT 28 UNION SELECT 29 UNION SELECT 30 UNION SELECT 31 UNION SELECT 32 UNION SELECT 33 UNION SELECT 34 UNION SELECT 35 UNION SELECT 36 UNION SELECT 37 UNION SELECT 38 UNION SELECT 39 UNION SELECT 40 UNION SELECT 41) t
CROSS JOIN (SELECT 0 n UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12 UNION SELECT 13 UNION SELECT 14 UNION SELECT 15 UNION SELECT 16 UNION SELECT 17 UNION SELECT 18 UNION SELECT 19 UNION SELECT 20 UNION SELECT 21) r
LIMIT 924;

-- Liên kết học sinh - phụ huynh cho tất cả học sinh (85-1008)
-- Mỗi HS có PH tương ứng với cùng ID
INSERT INTO HocSinhPhuHuynh (MaHocSinh, MaPhuHuynh, MoiQuanHe)
SELECT 
    h.MaHocSinh,
    p.MaPhuHuynh,
    'Cha/Mẹ' as MoiQuanHe
FROM HocSinh h
INNER JOIN PhuHuynh p ON h.MaHocSinh = p.MaPhuHuynh
WHERE h.MaHocSinh BETWEEN 85 AND 1008;

-- =====================================================================
-- PHẦN 7: NGƯỜI DÙNG VÀ PHÂN QUYỀN
-- =====================================================================
-- LƯU Ý: Tài khoản học sinh đã được tạo ở PHẦN 5B, không tạo lại ở đây

-- Người dùng hệ thống (chỉ Admin và Giáo viên)
INSERT IGNORE INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) VALUES
-- Admin
('admin', '12345678', 'Hoạt động'),
-- Giáo viên (70 tài khoản)
('GV001', '12345678', 'Hoạt động'), ('GV002', '12345678', 'Hoạt động'), ('GV003', '12345678', 'Hoạt động'),
('GV004', '12345678', 'Hoạt động'), ('GV005', '12345678', 'Hoạt động'), ('GV006', '12345678', 'Hoạt động'),
('GV007', '12345678', 'Hoạt động'), ('GV008', '12345678', 'Hoạt động'), ('GV009', '12345678', 'Hoạt động'),
('GV010', '12345678', 'Hoạt động'), ('GV011', '12345678', 'Hoạt động'), ('GV012', '12345678', 'Hoạt động'),
('GV013', '12345678', 'Hoạt động'), ('GV014', '12345678', 'Hoạt động'), ('GV015', '12345678', 'Hoạt động'),
('GV016', '12345678', 'Hoạt động'), ('GV017', '12345678', 'Hoạt động'), ('GV018', '12345678', 'Hoạt động'),
('GV019', '12345678', 'Hoạt động'), ('GV020', '12345678', 'Hoạt động'), ('GV021', '12345678', 'Hoạt động'),
('GV022', '12345678', 'Hoạt động'), ('GV023', '12345678', 'Hoạt động'), ('GV024', '12345678', 'Hoạt động'),
('GV025', '12345678', 'Hoạt động'), ('GV026', '12345678', 'Hoạt động'), ('GV027', '12345678', 'Hoạt động'),
('GV028', '12345678', 'Hoạt động'), ('GV029', '12345678', 'Hoạt động'), ('GV030', '12345678', 'Hoạt động'),
('GV031', '12345678', 'Hoạt động'), ('GV032', '12345678', 'Hoạt động'), ('GV033', '12345678', 'Hoạt động'),
('GV034', '12345678', 'Hoạt động'), ('GV035', '12345678', 'Hoạt động'), ('GV036', '12345678', 'Hoạt động'),
('GV037', '12345678', 'Hoạt động'), ('GV038', '12345678', 'Hoạt động'), ('GV039', '12345678', 'Hoạt động'),
('GV040', '12345678', 'Hoạt động'), ('GV041', '12345678', 'Hoạt động'), ('GV042', '12345678', 'Hoạt động'),
('GV043', '12345678', 'Hoạt động'), ('GV044', '12345678', 'Hoạt động'), ('GV045', '12345678', 'Hoạt động'),
('GV046', '12345678', 'Hoạt động'), ('GV047', '12345678', 'Hoạt động'), ('GV048', '12345678', 'Hoạt động'),
('GV049', '12345678', 'Hoạt động'), ('GV050', '12345678', 'Hoạt động'), ('GV051', '12345678', 'Hoạt động'),
('GV052', '12345678', 'Hoạt động'), ('GV053', '12345678', 'Hoạt động'), ('GV054', '12345678', 'Hoạt động'),
('GV055', '12345678', 'Hoạt động'), ('GV056', '12345678', 'Hoạt động'), ('GV057', '12345678', 'Hoạt động'),
('GV058', '12345678', 'Hoạt động'), ('GV059', '12345678', 'Hoạt động'), ('GV060', '12345678', 'Hoạt động'),
('GV061', '12345678', 'Hoạt động'), ('GV062', '12345678', 'Hoạt động'), ('GV063', '12345678', 'Hoạt động'),
('GV064', '12345678', 'Hoạt động'), ('GV065', '12345678', 'Hoạt động'), ('GV066', '12345678', 'Hoạt động'),
('GV067', '12345678', 'Hoạt động'), ('GV068', '12345678', 'Hoạt động'), ('GV069', '12345678', 'Hoạt động'),
('GV070', '12345678', 'Hoạt động');

SELECT 'Admin and teacher accounts created (71 accounts)' AS Status;

-- Phân quyền người dùng (chỉ Admin và Giáo viên)
-- LƯU Ý: Phân quyền học sinh đã được thực hiện ở PHẦN 5B
INSERT IGNORE INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) VALUES
-- Admin
('admin', 'admin'),
-- Giáo viên
('GV001', 'teacher'), ('GV002', 'teacher'), ('GV003', 'teacher'), ('GV004', 'teacher'), ('GV005', 'teacher'),
('GV006', 'teacher'), ('GV007', 'teacher'), ('GV008', 'teacher'), ('GV009', 'teacher'), ('GV010', 'teacher'),
('GV011', 'teacher'), ('GV012', 'teacher'), ('GV013', 'teacher'), ('GV014', 'teacher'), ('GV015', 'teacher'),
('GV016', 'teacher'), ('GV017', 'teacher'), ('GV018', 'teacher'), ('GV019', 'teacher'), ('GV020', 'teacher'),
('GV021', 'teacher'), ('GV022', 'teacher'), ('GV023', 'teacher'), ('GV024', 'teacher'), ('GV025', 'teacher'),
('GV026', 'teacher'), ('GV027', 'teacher'), ('GV028', 'teacher'), ('GV029', 'teacher'), ('GV030', 'teacher'),
('GV031', 'teacher'), ('GV032', 'teacher'), ('GV033', 'teacher'), ('GV034', 'teacher'), ('GV035', 'teacher'),
('GV036', 'teacher'), ('GV037', 'teacher'), ('GV038', 'teacher'), ('GV039', 'teacher'), ('GV040', 'teacher'),
('GV041', 'teacher'), ('GV042', 'teacher'), ('GV043', 'teacher'), ('GV044', 'teacher'), ('GV045', 'teacher'),
('GV046', 'teacher'), ('GV047', 'teacher'), ('GV048', 'teacher'), ('GV049', 'teacher'), ('GV050', 'teacher'),
('GV051', 'teacher'), ('GV052', 'teacher'), ('GV053', 'teacher'), ('GV054', 'teacher'), ('GV055', 'teacher'),
('GV056', 'teacher'), ('GV057', 'teacher'), ('GV058', 'teacher'), ('GV059', 'teacher'), ('GV060', 'teacher'),
('GV061', 'teacher'), ('GV062', 'teacher'), ('GV063', 'teacher'), ('GV064', 'teacher'), ('GV065', 'teacher'),
('GV066', 'teacher'), ('GV067', 'teacher'), ('GV068', 'teacher'), ('GV069', 'teacher'), ('GV070', 'teacher');

SELECT 'Admin and teacher roles assigned (71 roles)' AS Status;

-- =====================================================================
-- PHẦN 8: PHÂN CÔNG GIẢNG DẠY
-- =====================================================================

-- Phân công giảng dạy mẫu cho lớp 10A1 (một số môn chính)
-- INSERT INTO PhanCongGiangDay (MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) VALUES
-- -- Lớp 10A1 - Các môn chính
-- (1, 'GV001', 2, 1, '2024-09-01', '2024-12-31'), -- Toán
-- (1, 'GV008', 1, 1, '2024-09-01', '2024-12-31'), -- Văn
-- (1, 'GV015', 3, 1, '2024-09-01', '2024-12-31'), -- Anh
-- (1, 'GV021', 7, 1, '2024-09-01', '2024-12-31'), -- Lý
-- (1, 'GV026', 8, 1, '2024-09-01', '2024-12-31'), -- Hóa
-- (1, 'GV031', 9, 1, '2024-09-01', '2024-12-31'), -- Sinh
-- (1, 'GV035', 4, 1, '2024-09-01', '2024-12-31'), -- Sử
-- (1, 'GV039', 5, 1, '2024-09-01', '2024-12-31'), -- Địa
-- (1, 'GV043', 6, 1, '2024-09-01', '2024-12-31'), -- GDCD
-- (1, 'GV047', 10, 1, '2024-09-01', '2024-12-31'), -- Công nghệ
-- (1, 'GV051', 11, 1, '2024-09-01', '2024-12-31'), -- Tin học
-- (1, 'GV056', 12, 1, '2024-09-01', '2024-12-31'), -- Thể dục
-- (1, 'GV060', 13, 1, '2024-09-01', '2024-12-31'); -- GDQP-AN

-- Thời khóa biểu mẫu cho lớp 10A1 (một số tiết)
-- INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc) VALUES
-- -- Thứ 2
-- (1, 'Thứ 2', 1, 2, 'A101'), -- Toán tiết 1-2
-- (2, 'Thứ 2', 3, 2, 'A101'), -- Văn tiết 3-4
-- (3, 'Thứ 2', 5, 1, 'A101'), -- Anh tiết 5
-- -- Thứ 3
-- (4, 'Thứ 3', 1, 2, 'A102'), -- Lý tiết 1-2
-- (5, 'Thứ 3', 3, 2, 'A102'), -- Hóa tiết 3-4
-- (6, 'Thứ 3', 5, 1, 'A102'), -- Sinh tiết 5
-- -- Thứ 4
-- (7, 'Thứ 4', 1, 2, 'A101'), -- Sử tiết 1-2
-- (8, 'Thứ 4', 3, 2, 'A101'), -- Địa tiết 3-4
-- (9, 'Thứ 4', 5, 1, 'A101'), -- GDCD tiết 5
-- -- Thứ 5
-- (10, 'Thứ 5', 1, 2, 'A103'), -- Công nghệ tiết 1-2
-- (11, 'Thứ 5', 3, 2, 'A103'), -- Tin học tiết 3-4
-- (12, 'Thứ 5', 5, 1, 'A103'), -- Thể dục tiết 5
-- -- Thứ 6
-- (13, 'Thứ 6', 1, 1, 'A104'); -- GDQP-AN tiết 1

-- SELECT 'Assignments completed' AS Status;

-- =====================================================================
-- HOÀN THÀNH SEED DATA
-- =====================================================================

COMMIT;
SET FOREIGN_KEY_CHECKS = 1;

SELECT 'Sample seed data insertion completed successfully!' AS Final_Status;
