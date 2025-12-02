-- =====================================================================
-- FILE 03: SAMPLE SEED DATA (CONSOLIDATED FINAL)
-- Includes: 70 Teachers, Teacher Expertise, 500 Students, TKB, HK2 Data
-- =====================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;
USE QuanLyHocSinh;

-- =====================================================================
-- PHẦN 1: DỮ LIỆU CƠ BẢN
-- =====================================================================

-- Khối lớp

-- Môn học
INSERT INTO MonHoc (MaMonHoc, TenMonHoc, SoTiet, GhiChu) VALUES
(1, 'Ngữ văn', 50, 'Môn chính'), (2, 'Toán', 60, 'Môn chính'), (3, 'Tiếng Anh', 41, 'Môn chính'),
(4, 'Lịch sử', 36, 'Khoa học xã hội'), (5, 'Địa lý', 50, 'Khoa học xã hội'), (6, 'GD Kinh tế & Pháp luật', 35, 'Khoa học xã hội'),
(7, 'Vật lý', 35, 'Khoa học tự nhiên'), (8, 'Hóa học', 41, 'Khoa học tự nhiên'), (9, 'Sinh học', 23, 'Khoa học tự nhiên'),
(10, 'Công nghệ', 41, 'Khoa học xã hội'), (11, 'Tin học', 53, 'Kỹ năng khác'), (12, 'Giáo dục thể chất', 35, 'Kỹ năng khác'),
(13, 'GDQP-AN', 26, 'Kỹ năng khác');

-- Vai trò & Chức năng
INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES
('student', 'Học sinh', 'Học sinh'), ('parent', 'Phụ huynh', 'Phụ huynh'),
('teacher', 'Giáo viên', 'Giáo viên'), ('admin', 'Quản trị viên', 'Quản trị hệ thống');

INSERT INTO ChucNang (MaChucNang, TenChucNang, MoTa) VALUES
('qlhs', 'Quản lý học sinh', 'QLHS'), ('qlphuhuynh', 'Quản lý phụ huynh', 'QLPH'),
('qlgiaovien', 'Quản lý giáo viên', 'QLGV'), ('qllophoc', 'Quản lý lớp học', 'QLLH'),
('qlmonhoc', 'Quản lý môn học', 'QLMH'), ('qltkb', 'Quản lý thời khóa biểu', 'QLTKB'),
('qldiem', 'Quản lý điểm số', 'QLD'), ('qlthongbao', 'Quản lý thông báo', 'QLTB');

INSERT INTO VaiTroChucNang (MaVaiTro, MaChucNang) VALUES
('student', 'qlhs'), ('parent', 'qlhs'), ('parent', 'qlphuhuynh'),
('teacher', 'qlgiaovien'), ('teacher', 'qllophoc'), ('teacher', 'qlmonhoc'), ('teacher', 'qltkb'), ('teacher', 'qldiem'),
('admin', 'qlhs'), ('admin', 'qlphuhuynh'), ('admin', 'qlgiaovien'), ('admin', 'qllophoc'),
('admin', 'qlmonhoc'), ('admin', 'qltkb'), ('admin', 'qldiem'), ('admin', 'qlthongbao');

-- Năm học & Học kỳ
INSERT INTO NamHoc (MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc) VALUES
('2024-2025', 'Năm học 2024-2025', '2024-09-01', '2025-05-31'),
('2025-2026', 'Năm học 2025-2026', '2025-09-01', '2026-05-31'),
('2026-2027', 'Năm học 2026-2027', '2026-09-01', '2027-05-31');

INSERT INTO HocKy (MaHocKy, TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT) VALUES
(1, 'Học kỳ I', '2025-2026', 'Đang diễn ra', '2025-09-01', '2026-01-15'),
(2, 'Học kỳ II', '2025-2026', 'Chưa bắt đầu', '2026-01-16', '2026-05-31'),
(3, 'Học kỳ I', '2026-2027', 'Chưa bắt đầu', '2026-09-01', '2027-01-15'),
(4, 'Học kỳ II', '2026-2027', 'Chưa bắt đầu', '2027-01-16', '2027-05-31');

-- =====================================================================
-- PHẦN 2: GIÁO VIÊN (70 GV)
-- =====================================================================
INSERT INTO GiaoVien (MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, TrangThai) VALUES
-- Tổ Toán (7 giáo viên)
('GV001', 'Nguyễn Văn Toán', '1985-03-15', 'Nam', '123 Nguyễn Huệ, Q1, TP.HCM', '0901234567', 'gv001@school.edu.vn', 'Đang giảng dạy'),
('GV002', 'Trần Thị Hương', '1987-07-22', 'Nữ', '456 Lê Lợi, Q1, TP.HCM', '0901234568', 'gv002@school.edu.vn', 'Đang giảng dạy'),
('GV003', 'Lê Minh Tuấn', '1983-11-08', 'Nam', '789 Đồng Khởi, Q1, TP.HCM', '0901234569', 'gv003@school.edu.vn', 'Đang giảng dạy'),
('GV004', 'Phạm Thị Mai', '1986-05-14', 'Nữ', '321 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234570', 'gv004@school.edu.vn', 'Đang giảng dạy'),
('GV005', 'Hoàng Văn Đức', '1984-09-30', 'Nam', '654 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234571', 'gv005@school.edu.vn', 'Đang giảng dạy'),
('GV006', 'Võ Thị Lan', '1988-01-18', 'Nữ', '987 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234572', 'gv006@school.edu.vn', 'Đang giảng dạy'),
('GV007', 'Đặng Minh Khang', '1982-12-25', 'Nam', '147 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234573', 'gv007@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Văn (7 giáo viên)
('GV008', 'Bùi Thị Hoa', '1985-04-12', 'Nữ', '258 Trần Hưng Đạo, Q5, TP.HCM', '0901234574', 'gv008@school.edu.vn', 'Đang giảng dạy'),
('GV009', 'Ngô Văn Nam', '1987-08-19', 'Nam', '369 Lý Tự Trọng, Q1, TP.HCM', '0901234575', 'gv009@school.edu.vn', 'Đang giảng dạy'),
('GV010', 'Đinh Thị Thu', '1983-06-07', 'Nữ', '741 Pasteur, Q3, TP.HCM', '0901234576', 'gv010@school.edu.vn', 'Đang giảng dạy'),
('GV011', 'Phan Văn Hùng', '1986-02-28', 'Nam', '852 Nguyễn Du, Q1, TP.HCM', '0901234577', 'gv011@school.edu.vn', 'Đang giảng dạy'),
('GV012', 'Lý Thị Nga', '1984-10-15', 'Nữ', '963 Đinh Tiên Hoàng, Q.Bình Thạnh, TP.HCM', '0901234578', 'gv012@school.edu.vn', 'Đang giảng dạy'),
('GV013', 'Vũ Minh Tuấn', '1988-12-03', 'Nam', '159 Nguyễn Thị Thập, Q7, TP.HCM', '0901234579', 'gv013@school.edu.vn', 'Đang giảng dạy'),
('GV014', 'Trịnh Thị Linh', '1982-09-21', 'Nữ', '357 Võ Văn Tần, Q3, TP.HCM', '0901234580', 'gv014@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Anh (6 giáo viên)
('GV015', 'Cao Thị Hạnh', '1985-01-16', 'Nữ', '468 Nguyễn Đình Chiểu, Q3, TP.HCM', '0901234581', 'gv015@school.edu.vn', 'Đang giảng dạy'),
('GV016', 'Lâm Văn Phong', '1987-05-24', 'Nam', '579 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234582', 'gv016@school.edu.vn', 'Đang giảng dạy'),
('GV017', 'Hồ Thị Yến', '1983-11-11', 'Nữ', '680 Nguyễn Văn Linh, Q7, TP.HCM', '0901234583', 'gv017@school.edu.vn', 'Đang giảng dạy'),
('GV018', 'Dương Minh Đức', '1986-07-08', 'Nam', '791 Lê Văn Việt, Q9, TP.HCM', '0901234584', 'gv018@school.edu.vn', 'Đang giảng dạy'),
('GV019', 'Tôn Thị Lan', '1984-03-26', 'Nữ', '802 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234585', 'gv019@school.edu.vn', 'Đang giảng dạy'),
('GV020', 'Chu Văn Thành', '1988-09-13', 'Nam', '913 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234586', 'gv020@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Lý (5 giáo viên)
('GV021', 'Đỗ Thị Hương', '1985-08-05', 'Nữ', '124 Nguyễn Huệ, Q1, TP.HCM', '0901234587', 'gv021@school.edu.vn', 'Đang giảng dạy'),
('GV022', 'Nguyễn Minh Tâm', '1987-04-22', 'Nam', '235 Lê Lợi, Q1, TP.HCM', '0901234588', 'gv022@school.edu.vn', 'Đang giảng dạy'),
('GV023', 'Trần Thị Ngọc', '1983-12-09', 'Nữ', '346 Đồng Khởi, Q1, TP.HCM', '0901234589', 'gv023@school.edu.vn', 'Đang giảng dạy'),
('GV024', 'Lê Văn Hải', '1986-06-16', 'Nam', '457 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234590', 'gv024@school.edu.vn', 'Đang giảng dạy'),
('GV025', 'Phạm Thị Thu', '1984-02-03', 'Nữ', '568 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234591', 'gv025@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Hóa (5 giáo viên)
('GV026', 'Hoàng Văn Long', '1985-10-20', 'Nam', '679 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234592', 'gv026@school.edu.vn', 'Đang giảng dạy'),
('GV027', 'Võ Thị Hoa', '1987-07-17', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234593', 'gv027@school.edu.vn', 'Đang giảng dạy'),
('GV028', 'Đặng Minh Tuấn', '1983-05-04', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234594', 'gv028@school.edu.vn', 'Đang giảng dạy'),
('GV029', 'Bùi Thị Mai', '1986-01-11', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234595', 'gv029@school.edu.vn', 'Đang giảng dạy'),
('GV030', 'Ngô Văn Đức', '1984-09-28', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234596', 'gv030@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Sinh (4 giáo viên)
('GV031', 'Đinh Thị Hoa', '1985-06-13', 'Nữ', '134 Nguyễn Du, Q1, TP.HCM', '0901234597', 'gv031@school.edu.vn', 'Đang giảng dạy'),
('GV032', 'Phan Văn Nam', '1987-02-20', 'Nam', '245 Lê Văn Việt, Q9, TP.HCM', '0901234598', 'gv032@school.edu.vn', 'Đang giảng dạy'),
('GV033', 'Lý Thị Thu', '1983-10-07', 'Nữ', '356 Nguyễn Văn Linh, Q7, TP.HCM', '0901234599', 'gv033@school.edu.vn', 'Đang giảng dạy'),
('GV034', 'Vũ Minh Hùng', '1986-08-14', 'Nam', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234600', 'gv034@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Sử (4 giáo viên)
('GV035', 'Trịnh Thị Lan', '1984-04-01', 'Nữ', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234601', 'gv035@school.edu.vn', 'Đang giảng dạy'),
('GV036', 'Cao Văn Đức', '1988-12-18', 'Nam', '689 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234602', 'gv036@school.edu.vn', 'Đang giảng dạy'),
('GV037', 'Lâm Thị Mai', '1982-08-05', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234603', 'gv037@school.edu.vn', 'Đang giảng dạy'),
('GV038', 'Hồ Minh Tuấn', '1985-06-22', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234604', 'gv038@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Địa (4 giáo viên)
('GV039', 'Dương Thị Hương', '1987-02-09', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234605', 'gv039@school.edu.vn', 'Đang giảng dạy'),
('GV040', 'Tôn Văn Khang', '1983-10-26', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234606', 'gv040@school.edu.vn', 'Đang giảng dạy'),
('GV041', 'Chu Thị Ngọc', '1986-08-13', 'Nữ', '134 Nguyễn Du, Q1, TP.HCM', '0901234607', 'gv041@school.edu.vn', 'Đang giảng dạy'),
('GV042', 'Đỗ Minh Hải', '1984-04-30', 'Nam', '245 Lê Văn Việt, Q9, TP.HCM', '0901234608', 'gv042@school.edu.vn', 'Đang giảng dạy'),
-- Tổ GDCD (4 giáo viên)
('GV043', 'Nguyễn Thị Thu', '1985-12-17', 'Nữ', '356 Nguyễn Văn Linh, Q7, TP.HCM', '0901234609', 'gv043@school.edu.vn', 'Đang giảng dạy'),
('GV044', 'Trần Văn Long', '1987-08-04', 'Nam', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234610', 'gv044@school.edu.vn', 'Đang giảng dạy'),
('GV045', 'Lê Thị Hoa', '1983-06-21', 'Nữ', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234611', 'gv045@school.edu.vn', 'Đang giảng dạy'),
('GV046', 'Phạm Minh Đức', '1986-02-08', 'Nam', '689 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234612', 'gv046@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Công nghệ (4 giáo viên)
('GV047', 'Hoàng Thị Lan', '1984-10-25', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234613', 'gv047@school.edu.vn', 'Đang giảng dạy'),
('GV048', 'Võ Minh Tuấn', '1988-08-12', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234614', 'gv048@school.edu.vn', 'Đang giảng dạy'),
('GV049', 'Đặng Thị Mai', '1982-06-29', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234615', 'gv049@school.edu.vn', 'Đang giảng dạy'),
('GV050', 'Bùi Văn Hùng', '1985-04-16', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234616', 'gv050@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Tin học (5 giáo viên)
('GV051', 'Ngô Thị Hương', '1987-12-03', 'Nữ', '134 Nguyễn Du, Q1, TP.HCM', '0901234617', 'gv051@school.edu.vn', 'Đang giảng dạy'),
('GV052', 'Đinh Minh Nam', '1983-10-20', 'Nam', '245 Lê Văn Việt, Q9, TP.HCM', '0901234618', 'gv052@school.edu.vn', 'Đang giảng dạy'),
('GV053', 'Phan Thị Thu', '1986-08-07', 'Nữ', '356 Nguyễn Văn Linh, Q7, TP.HCM', '0901234619', 'gv053@school.edu.vn', 'Đang giảng dạy'),
('GV054', 'Lý Minh Đức', '1984-06-24', 'Nam', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234620', 'gv054@school.edu.vn', 'Đang giảng dạy'),
('GV055', 'Vũ Thị Lan', '1988-04-11', 'Nữ', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234621', 'gv055@school.edu.vn', 'Đang giảng dạy'),
-- Tổ Thể dục (4 giáo viên)
('GV056', 'Trịnh Văn Hải', '1982-12-28', 'Nam', '689 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234622', 'gv056@school.edu.vn', 'Đang giảng dạy'),
('GV057', 'Cao Thị Ngọc', '1985-10-15', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234623', 'gv057@school.edu.vn', 'Đang giảng dạy'),
('GV058', 'Lâm Minh Tuấn', '1987-08-02', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234624', 'gv058@school.edu.vn', 'Đang giảng dạy'),
('GV059', 'Hồ Thị Hoa', '1983-06-19', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234625', 'gv059@school.edu.vn', 'Đang giảng dạy'),
-- Tổ GDQP-AN (3 giáo viên)
('GV060', 'Dương Văn Long', '1986-04-06', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234626', 'gv060@school.edu.vn', 'Đang giảng dạy'),
('GV061', 'Tôn Thị Mai', '1984-12-23', 'Nữ', '134 Nguyễn Du, Q1, TP.HCM', '0901234627', 'gv061@school.edu.vn', 'Đang giảng dạy'),
('GV062', 'Chu Minh Đức', '1988-10-10', 'Nam', '245 Lê Văn Việt, Q9, TP.HCM', '0901234628', 'gv062@school.edu.vn', 'Đang giảng dạy'),
-- Giáo viên bổ sung (8 giáo viên)
('GV063', 'Đỗ Thị Hương', '1982-08-27', 'Nữ', '356 Nguyễn Văn Linh, Q7, TP.HCM', '0901234629', 'gv063@school.edu.vn', 'Đang giảng dạy'),
('GV064', 'Nguyễn Văn Nam', '1985-06-14', 'Nam', '467 Điện Biên Phủ, Q.Bình Thạnh, TP.HCM', '0901234630', 'gv064@school.edu.vn', 'Đang giảng dạy'),
('GV065', 'Trần Thị Thu', '1987-04-01', 'Nữ', '578 Nguyễn Thị Minh Khai, Q3, TP.HCM', '0901234631', 'gv065@school.edu.vn', 'Đang giảng dạy'),
('GV066', 'Lê Minh Hùng', '1983-02-18', 'Nam', '689 Cách Mạng Tháng 8, Q10, TP.HCM', '0901234632', 'gv066@school.edu.vn', 'Đang giảng dạy'),
('GV067', 'Phạm Thị Lan', '1986-12-05', 'Nữ', '790 Nguyễn Văn Cừ, Q5, TP.HCM', '0901234633', 'gv067@school.edu.vn', 'Đang giảng dạy'),
('GV068', 'Hoàng Văn Đức', '1984-10-22', 'Nam', '801 Trần Hưng Đạo, Q5, TP.HCM', '0901234634', 'gv068@school.edu.vn', 'Đang giảng dạy'),
('GV069', 'Võ Thị Ngọc', '1988-08-09', 'Nữ', '912 Lý Tự Trọng, Q1, TP.HCM', '0901234635', 'gv069@school.edu.vn', 'Đang giảng dạy'),
('GV070', 'Đặng Minh Hải', '1982-06-26', 'Nam', '023 Pasteur, Q3, TP.HCM', '0901234636', 'gv070@school.edu.vn', 'Đang giảng dạy');

-- =====================================================================
-- PHẦN 3: LỚP HỌC (24 lớp)
-- =====================================================================
INSERT INTO LopHoc (TenLop, MaKhoi, SiSo, MaGiaoVienChuNhiem) VALUES
('10A1', 10, 21, 'GV001'), ('10A2', 10, 21, 'GV002'), ('10A3', 10, 21, 'GV003'), ('10A4', 10, 21, 'GV004'),
('10A5', 10, 21, 'GV005'), ('10A6', 10, 21, 'GV006'), ('10A7', 10, 21, 'GV007'), ('10A8', 10, 21, 'GV008'),
('11A1', 11, 21, 'GV009'), ('11A2', 11, 21, 'GV010'), ('11A3', 11, 21, 'GV011'), ('11A4', 11, 21, 'GV012'),
('11A5', 11, 21, 'GV013'), ('11A6', 11, 21, 'GV014'), ('11A7', 11, 21, 'GV015'), ('11A8', 11, 21, 'GV016'),
('12A1', 12, 21, 'GV017'), ('12A2', 12, 21, 'GV018'), ('12A3', 12, 21, 'GV019'), ('12A4', 12, 21, 'GV020'),
('12A5', 12, 21, 'GV021'), ('12A6', 12, 21, 'GV022'), ('12A7', 12, 21, 'GV023'), ('12A8', 12, 21, 'GV024');

-- =====================================================================
-- PHẦN 4: HỌC SINH & TÀI KHOẢN (500 HS)
-- =====================================================================
INSERT INTO HocSinh (HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai)
SELECT 
    CONCAT(ELT(FLOOR(1 + (seq MOD 20)), 'Nguyễn', 'Trần', 'Lê', 'Phạm', 'Hoàng', 'Phan', 'Vũ', 'Đặng', 'Bùi', 'Đỗ', 'Hồ', 'Ngô', 'Dương', 'Lý', 'Võ', 'Đinh', 'Trịnh', 'Cao', 'Lâm', 'Tôn'), ' ', ELT(FLOOR(1 + ((seq * 3) MOD 15)), 'Văn', 'Thị', 'Minh', 'Công', 'Quốc', 'Thanh', 'Hữu', 'Đức', 'Ngọc', 'Thu', 'Mai', 'Hương', 'Phương', 'Anh', 'Tuấn'), ' ', ELT(FLOOR(1 + ((seq * 7) MOD 20)), 'An', 'Bình', 'Cường', 'Dũng', 'Giang', 'Hải', 'Hùng', 'Khoa', 'Long', 'Nam', 'Phúc', 'Quân', 'Sơn', 'Tài', 'Thắng', 'Toàn', 'Trí', 'Tùng', 'Tuấn', 'Vinh')) as HoTen,
    DATE_ADD('2006-01-01', INTERVAL (seq MOD 2555) DAY) as NgaySinh,
    IF((seq MOD 2) = 0, 'Nam', 'Nữ') as GioiTinh,
    CONCAT('09', LPAD(12340000 + seq, 8, '0')) as SDTHS,
    CONCAT('hs', LPAD(seq, 3, '0'), '@student.edu.vn') as Email,
    CASE WHEN seq <= 450 THEN 'Đang học' WHEN seq <= 465 THEN 'Nghỉ học' WHEN seq <= 470 THEN 'Bảo lưu' ELSE 'Thôi học' END as TrangThai
FROM (SELECT @row := @row + 1 as seq FROM (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t1, (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t2, (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t3, (SELECT @row := 0) r LIMIT 500) numbers;

-- Tạo tài khoản
INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai)
SELECT CONCAT('HS', MaHocSinh), '123456', IF(TrangThai IN ('Đang học', 'Nghỉ học'), 'Hoạt động', 'Tạm khóa') FROM HocSinh;

UPDATE HocSinh SET TenDangNhap = CONCAT('HS', MaHocSinh) WHERE MaHocSinh > 0;
INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) SELECT CONCAT('HS', MaHocSinh), 'student' FROM HocSinh;

-- =====================================================================
-- PHẦN 5: PHỤ HUYNH & LIÊN KẾT
-- =====================================================================
INSERT INTO PhuHuynh (HoTen, SoDienThoai, Email, DiaChi)
SELECT 
    CONCAT('PH của HS', LPAD(seq, 3, '0')),
    CONCAT('09', LPAD(23450000 + seq, 8, '0')),
    CONCAT('ph', LPAD(seq, 3, '0'), '@parent.edu.vn'),
    CONCAT('TP.HCM')
FROM (SELECT @row2 := @row2 + 1 as seq FROM (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t1, (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t2, (SELECT 0 UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) t3, (SELECT @row2 := 0) r LIMIT 500) numbers;

INSERT INTO HocSinhPhuHuynh (MaHocSinh, MaPhuHuynh, MoiQuanHe)
SELECT hs.MaHocSinh, ph.MaPhuHuynh, IF(RAND() > 0.5, 'Cha', 'Mẹ')
FROM (SELECT MaHocSinh, @hs_row := @hs_row + 1 as hs_num FROM HocSinh, (SELECT @hs_row := 0) r WHERE TrangThai = 'Đang học' ORDER BY MaHocSinh LIMIT 475) hs
JOIN (SELECT MaPhuHuynh, @ph_row := @ph_row + 1 as ph_num FROM PhuHuynh, (SELECT @ph_row := 0) r2 ORDER BY MaPhuHuynh LIMIT 475) ph ON hs.hs_num = ph.ph_num;

-- =====================================================================
-- PHẦN 6: NGƯỜI DÙNG VÀ PHÂN QUYỀN (ADMIN & TEACHER)
-- =====================================================================
INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) VALUES ('admin', '12345678', 'Hoạt động');
INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai)
SELECT MaGiaoVien, '12345678', 'Hoạt động' FROM GiaoVien;

INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) VALUES ('admin', 'admin');
INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro)
SELECT MaGiaoVien, 'teacher' FROM GiaoVien;

-- =====================================================================
-- PHẦN 7: CHUYÊN MÔN GIÁO VIÊN
-- =====================================================================
INSERT INTO GiaoVienChuyenMon (MaGiaoVien, MaMonHoc, LaChuyenMonChinh) VALUES
-- Tổ Toán (7 GV)
('GV001', 2, TRUE), ('GV002', 2, TRUE), ('GV003', 2, TRUE), ('GV004', 2, TRUE), ('GV005', 2, TRUE), ('GV006', 2, TRUE), ('GV007', 2, TRUE),
-- Tổ Văn (7 GV)
('GV008', 1, TRUE), ('GV009', 1, TRUE), ('GV010', 1, TRUE), ('GV011', 1, TRUE), ('GV012', 1, TRUE), ('GV013', 1, TRUE), ('GV014', 1, TRUE),
-- Tổ Anh (6 GV)
('GV015', 3, TRUE), ('GV016', 3, TRUE), ('GV017', 3, TRUE), ('GV018', 3, TRUE), ('GV019', 3, TRUE), ('GV020', 3, TRUE),
-- Tổ Lý (5 GV)
('GV021', 7, TRUE), ('GV022', 7, TRUE), ('GV023', 7, TRUE), ('GV024', 7, TRUE), ('GV025', 7, TRUE),
-- Tổ Hóa (5 GV)
('GV026', 8, TRUE), ('GV027', 8, TRUE), ('GV028', 8, TRUE), ('GV029', 8, TRUE), ('GV030', 8, TRUE),
-- Tổ Sinh (4 GV)
('GV031', 9, TRUE), ('GV032', 9, TRUE), ('GV033', 9, TRUE), ('GV034', 9, TRUE),
-- Tổ Sử (4 GV)
('GV035', 4, TRUE), ('GV036', 4, TRUE), ('GV037', 4, TRUE), ('GV038', 4, TRUE),
-- Tổ Địa (4 GV)
('GV039', 5, TRUE), ('GV040', 5, TRUE), ('GV041', 5, TRUE), ('GV042', 5, TRUE),
-- Tổ GDCD (4 GV)
('GV043', 6, TRUE), ('GV044', 6, TRUE), ('GV045', 6, TRUE), ('GV046', 6, TRUE),
-- Tổ Công nghệ (4 GV)
('GV047', 10, TRUE), ('GV048', 10, TRUE), ('GV049', 10, TRUE), ('GV050', 10, TRUE),
-- Tổ Tin học (5 GV)
('GV051', 11, TRUE), ('GV052', 11, TRUE), ('GV053', 11, TRUE), ('GV054', 11, TRUE), ('GV055', 11, TRUE),
-- Tổ Thể dục (4 GV)
('GV056', 12, TRUE), ('GV057', 12, TRUE), ('GV058', 12, TRUE), ('GV059', 12, TRUE),
-- Tổ GDQP-AN (3 GV)
('GV060', 13, TRUE), ('GV061', 13, TRUE), ('GV062', 13, TRUE),
-- GV bổ sung (8 GV)
('GV063', 1, TRUE), ('GV064', 2, TRUE), ('GV065', 3, TRUE), ('GV066', 7, TRUE),
('GV067', 8, TRUE), ('GV068', 9, TRUE), ('GV069', 11, TRUE), ('GV070', 12, TRUE);

-- =====================================================================
-- PHẦN 8: PHÂN LỚP HK1
-- =====================================================================
INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy)
SELECT hs.MaHocSinh, lh.MaLop, 1
FROM (SELECT MaHocSinh, @row_num := @row_num + 1 as row_num FROM HocSinh, (SELECT @row_num := 0) r WHERE TrangThai = 'Đang học' ORDER BY MaHocSinh LIMIT 475) hs
JOIN (SELECT MaLop, @lop_row := @lop_row + 1 as lop_num FROM LopHoc, (SELECT @lop_row := 0) r2 ORDER BY MaLop) lh ON lh.lop_num = CEILING(hs.row_num / 19.79);

-- =====================================================================
-- PHẦN 9: ĐIỂM SỐ, HẠNH KIỂM, XẾP LOẠI HK1
-- =====================================================================
INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, DiemThuongXuyen, DiemGiuaKy, DiemCuoiKy, DiemTrungBinh)
SELECT pl.MaHocSinh, mh.MaMonHoc, 1, ROUND(5+RAND()*5,1), ROUND(5+RAND()*5,1), ROUND(5+RAND()*5,1), ROUND(5+RAND()*5,1)
FROM PhanLop pl CROSS JOIN MonHoc mh WHERE pl.MaHocKy = 1;

INSERT INTO HanhKiem (MaHocSinh, MaHocKy, XepLoai, NhanXet)
SELECT pl.MaHocSinh, 1, ELT(CEILING(RAND()*4), 'Tốt', 'Khá', 'Trung bình', 'Yếu'), 'Tự động'
FROM PhanLop pl WHERE pl.MaHocKy = 1;

INSERT INTO XepLoai (MaHocSinh, MaHocKy, HocLuc, GhiChu)
SELECT hs.MaHocSinh, 1, 'Khá', 'Tự động' FROM PhanLop pl JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh WHERE pl.MaHocKy = 1;

-- =====================================================================
-- PHẦN 10: PHÂN CÔNG & TKB (HK1) - 10A1, 10A2, 10A3
-- =====================================================================
-- 10A1
INSERT INTO PhanCongGiangDay (MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) VALUES
(1, 'GV001', 2, 1, '2025-09-01', '2026-01-15'), (1, 'GV008', 1, 1, '2025-09-01', '2026-01-15'), (1, 'GV015', 3, 1, '2025-09-01', '2026-01-15'),
(1, 'GV021', 7, 1, '2025-09-01', '2026-01-15'), (1, 'GV026', 8, 1, '2025-09-01', '2026-01-15'), (1, 'GV031', 9, 1, '2025-09-01', '2026-01-15'),
(1, 'GV035', 4, 1, '2025-09-01', '2026-01-15'), (1, 'GV039', 5, 1, '2025-09-01', '2026-01-15'), (1, 'GV043', 6, 1, '2025-09-01', '2026-01-15'),
(1, 'GV047', 10, 1, '2025-09-01', '2026-01-15'), (1, 'GV051', 11, 1, '2025-09-01', '2026-01-15'), (1, 'GV056', 12, 1, '2025-09-01', '2026-01-15'),
(1, 'GV060', 13, 1, '2025-09-01', '2026-01-15');

INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 1, 2, 'A101', 1, 1, 'GV001' FROM PhanCongGiangDay WHERE MaLop=1 AND MaMonHoc=2;
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 3, 2, 'A101', 1, 1, 'GV008' FROM PhanCongGiangDay WHERE MaLop=1 AND MaMonHoc=1;
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 5, 1, 'A101', 1, 1, 'GV015' FROM PhanCongGiangDay WHERE MaLop=1 AND MaMonHoc=3;

-- 10A2
INSERT INTO PhanCongGiangDay (MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) VALUES
(2, 'GV002', 2, 1, '2025-09-01', '2026-01-15'), (2, 'GV009', 1, 1, '2025-09-01', '2026-01-15'), (2, 'GV016', 3, 1, '2025-09-01', '2026-01-15'),
(2, 'GV022', 7, 1, '2025-09-01', '2026-01-15'), (2, 'GV027', 8, 1, '2025-09-01', '2026-01-15'), (2, 'GV032', 9, 1, '2025-09-01', '2026-01-15'),
(2, 'GV036', 4, 1, '2025-09-01', '2026-01-15'), (2, 'GV040', 5, 1, '2025-09-01', '2026-01-15'), (2, 'GV044', 6, 1, '2025-09-01', '2026-01-15'),
(2, 'GV048', 10, 1, '2025-09-01', '2026-01-15'), (2, 'GV052', 11, 1, '2025-09-01', '2026-01-15'), (2, 'GV057', 12, 1, '2025-09-01', '2026-01-15'),
(2, 'GV061', 13, 1, '2025-09-01', '2026-01-15');

INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 1, 2, 'A201', 1, 2, 'GV009' FROM PhanCongGiangDay WHERE MaLop=2 AND MaMonHoc=1;
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 3, 2, 'A201', 1, 2, 'GV002' FROM PhanCongGiangDay WHERE MaLop=2 AND MaMonHoc=2;
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 5, 1, 'A201', 1, 2, 'GV016' FROM PhanCongGiangDay WHERE MaLop=2 AND MaMonHoc=3;

-- 10A3
INSERT INTO PhanCongGiangDay (MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) VALUES
(3, 'GV003', 2, 1, '2025-09-01', '2026-01-15'), (3, 'GV010', 1, 1, '2025-09-01', '2026-01-15'), (3, 'GV017', 3, 1, '2025-09-01', '2026-01-15'),
(3, 'GV023', 7, 1, '2025-09-01', '2026-01-15'), (3, 'GV028', 8, 1, '2025-09-01', '2026-01-15'), (3, 'GV033', 9, 1, '2025-09-01', '2026-01-15'),
(3, 'GV037', 4, 1, '2025-09-01', '2026-01-15'), (3, 'GV041', 5, 1, '2025-09-01', '2026-01-15'), (3, 'GV045', 6, 1, '2025-09-01', '2026-01-15'),
(3, 'GV049', 10, 1, '2025-09-01', '2026-01-15'), (3, 'GV053', 11, 1, '2025-09-01', '2026-01-15'), (3, 'GV058', 12, 1, '2025-09-01', '2026-01-15'),
(3, 'GV062', 13, 1, '2025-09-01', '2026-01-15');

INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 1, 2, 'A301', 1, 3, 'GV017' FROM PhanCongGiangDay WHERE MaLop=3 AND MaMonHoc=3;
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 3, 2, 'A301', 1, 3, 'GV003' FROM PhanCongGiangDay WHERE MaLop=3 AND MaMonHoc=2;
INSERT INTO ThoiKhoaBieu (MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc, MaHocKy, MaLop, MaGiaoVien) 
SELECT MaPhanCong, 'Thứ 2', 5, 1, 'A301', 1, 3, 'GV010' FROM PhanCongGiangDay WHERE MaLop=3 AND MaMonHoc=1;

-- =====================================================================
-- PHẦN 11: DỮ LIỆU HK2
-- =====================================================================
INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy) SELECT MaHocSinh, MaLop, 2 FROM PhanLop WHERE MaHocKy = 1;

INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, DiemThuongXuyen, DiemGiuaKy, DiemCuoiKy, DiemTrungBinh)
SELECT pl.MaHocSinh, mh.MaMonHoc, 2, ROUND(5+RAND()*5,1), ROUND(5+RAND()*5,1), ROUND(5+RAND()*5,1), ROUND(5+RAND()*5,1)
FROM PhanLop pl CROSS JOIN MonHoc mh WHERE pl.MaHocKy = 2;

INSERT INTO HanhKiem (MaHocSinh, MaHocKy, XepLoai, NhanXet)
SELECT pl.MaHocSinh, 2, ELT(CEILING(RAND()*4), 'Tốt', 'Khá', 'Trung bình', 'Yếu'), 'HK2'
FROM PhanLop pl WHERE pl.MaHocKy = 2;

INSERT INTO XepLoai (MaHocSinh, MaHocKy, HocLuc, GhiChu)
SELECT hs.MaHocSinh, 2, 'Khá', 'HK2' FROM PhanLop pl JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh WHERE pl.MaHocKy = 2;

SET FOREIGN_KEY_CHECKS = 1;
SELECT 'IMPORT COMPLETED' AS Status;
