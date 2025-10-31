-- =====================================================================
-- XÓA DỮ LIỆU CŨ (nếu có) - Thực hiện theo thứ tự để tránh lỗi khóa ngoại
-- =====================================================================

DELETE FROM XepLoai;
DELETE FROM HanhKiem;
DELETE FROM DiemSo;
DELETE FROM PhanLop;
DELETE FROM HocSinhPhuHuynh;
DELETE FROM PhanCongGiangDay;
DELETE FROM ThoiKhoaBieu;
DELETE FROM GiaoVienChuyenMon;
DELETE FROM KhenThuongKyLuat;
DELETE FROM ThongBao;

DELETE FROM NguoiDungVaiTro;
DELETE FROM NguoiDung;
DELETE FROM VaiTroChucNang;
DELETE FROM ChucNang;
DELETE FROM VaiTro;

DELETE FROM LopHoc;
DELETE FROM HocSinh;
DELETE FROM PhuHuynh;
DELETE FROM GiaoVien;

DELETE FROM HocKy;
DELETE FROM NamHoc;
DELETE FROM MonHoc;
DELETE FROM KhoiLop;

-- =====================================================================
-- THÊM DỮ LIỆU MẪU - THEO THỨ TỰ KHÓA NGOẠI
-- =====================================================================

-- 1. INSERT MonHoc (không phụ thuộc bảng nào)
INSERT INTO MonHoc (MaMonHoc, TenMonHoc, SoTiet, GhiChu) VALUES
(1, 'Ngữ văn', 5, 'Bắt buộc'),
(2, 'Toán', 5, 'Bắt buộc'),
(3, 'Tiếng Anh', 3, 'Bắt buộc'),
(4, 'Lịch sử', 2, 'Bắt buộc'),
(5, 'Địa lý', 2, 'Bắt buộc'),
(6, 'GDCD', 1, 'Bắt buộc'),
(7, 'Vật lý', 3, 'Tự nhiên'),
(8, 'Hóa học', 3, 'Tự nhiên'),
(9, 'Sinh học', 2, 'Tự nhiên'),
(10, 'Tin học', 2, 'Tự chọn'),
(11, 'Công nghệ', 2, 'Tự chọn'),
(12, 'Thể dục', 2, 'Bắt buộc'),
(13, 'Âm nhạc', 1, 'Nghệ thuật');

-- 2. INSERT NamHoc và HocKy
INSERT INTO NamHoc (MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc) VALUES
('2025-2026', 'Năm học 2025-2026', '2025-09-01', '2026-05-31');

INSERT INTO HocKy (MaHocKy, TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT) VALUES
(1, 'Học kỳ I', '2025-2026', 'Đang diễn ra', '2025-09-01', '2026-01-15'),
(2, 'Học kỳ II', '2025-2026', 'Chưa bắt đầu', '2026-01-16', '2026-05-31');

-- 3. INSERT KhoiLop
INSERT INTO KhoiLop (MaKhoi, TenKhoi) VALUES
(10, 'Khối 10'),
(11, 'Khối 11'),
(12, 'Khối 12');

-- 4. INSERT GiaoVien
INSERT INTO GiaoVien (MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, TrangThai) VALUES
('GV001', 'Nguyễn Văn An', '1985-03-15', 'Nam', 'HCMC', '0901234567', 'nguyen.van.an@gmail.com', 'Đang giảng dạy'),
('GV002', 'Trần Thị Bình', '1987-05-20', 'Nữ', 'HCMC', '0902345678', 'tran.thi.binh@gmail.com', 'Đang giảng dạy'),
('GV003', 'Lê Văn Cường', '1990-07-10', 'Nam', 'HCMC', '0903456789', 'le.van.cuong@gmail.com', 'Đang giảng dạy'),
('GV004', 'Phạm Thị Dung', '1988-09-25', 'Nữ', 'HCMC', '0904567890', 'pham.thi.dung@gmail.com', 'Đang giảng dạy'),
('GV005', 'Hoàng Văn Em', '1992-11-30', 'Nam', 'HCMC', '0905678901', 'hoang.van.em@gmail.com', 'Đang giảng dạy'),
('GV006', 'Vũ Thị Phương', '1986-04-12', 'Nữ', 'HCMC', '0906789012', 'vu.thi.phuong@gmail.com', 'Đang giảng dạy'),
('GV007', 'Đỗ Văn Giang', '1989-08-25', 'Nam', 'HCMC', '0907890123', 'do.van.giang@gmail.com', 'Đang giảng dạy'),
('GV008', 'Bùi Thị Hà', '1991-12-18', 'Nữ', 'HCMC', '0908901234', 'bui.thi.ha@gmail.com', 'Đang giảng dạy'),
('GV009', 'Ngô Văn Hùng', '1984-06-30', 'Nam', 'HCMC', '0909012345', 'ngo.van.hung@gmail.com', 'Đang giảng dạy');

-- 5. INSERT GiaoVienChuyenMon (phụ thuộc GiaoVien, MonHoc)
INSERT INTO GiaoVienChuyenMon (MaGiaoVien, MaMonHoc, LaChuyenMonChinh) VALUES
('GV001', 1, TRUE),  -- Ngữ văn
('GV001', 2, FALSE), -- Toán
('GV002', 3, TRUE),  -- Tiếng Anh
('GV002', 4, FALSE), -- Lịch sử
('GV003', 7, TRUE),  -- Vật lý
('GV003', 8, FALSE), -- Hóa học
('GV004', 5, TRUE),  -- Địa lý
('GV004', 6, FALSE), -- GDCD
('GV005', 10, TRUE), -- Tin học
('GV005', 11, FALSE),-- Công nghệ
('GV006', 2, TRUE),  -- Toán
('GV007', 9, TRUE),  -- Sinh học
('GV008', 12, TRUE), -- Thể dục
('GV009', 13, TRUE); -- Âm nhạc

-- 6. INSERT LopHoc (3 khối x 3 lớp = 9 lớp)
INSERT INTO LopHoc (TenLop, MaKhoi, SiSo, MaGiaoVienChuNhiem) VALUES
-- Khối 10
('10A1', 10, 10, 'GV001'),
('10A2', 10, 10, 'GV002'),
('10A3', 10, 10, 'GV003'),
-- Khối 11
('11A1', 11, 10, 'GV004'),
('11A2', 11, 10, 'GV005'),
('11A3', 11, 10, 'GV006'),
-- Khối 12
('12A1', 12, 10, 'GV007'),
('12A2', 12, 10, 'GV008'),
('12A3', 12, 10, 'GV009');

-- 7. INSERT HocSinh (90 HS - mỗi lớp 10 HS)
INSERT INTO HocSinh (HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai) VALUES
-- Lớp 10A1: 8 Đang học, 1 Nghỉ học, 1 Bảo lưu
('Nguyễn Văn An', '2010-01-15', 'Nam', '0911111111', 'nguyen.van.an.hs01@gmail.com', 'Đang học'),
('Trần Thị Bích', '2010-02-20', 'Nữ', '0911111112', 'tran.thi.bich.hs02@gmail.com', 'Đang học'),
('Lê Văn Cường', '2010-03-25', 'Nam', '0911111113', 'le.van.cuong.hs03@gmail.com', 'Đang học'),
('Phạm Thị Dung', '2010-04-10', 'Nữ', '0911111114', 'pham.thi.dung.hs04@gmail.com', 'Đang học'),
('Hoàng Văn Em', '2010-05-15', 'Nam', '0911111115', 'hoang.van.em.hs05@gmail.com', 'Đang học'),
('Vũ Thị Phương', '2010-06-20', 'Nữ', '0911111116', 'vu.thi.phuong.hs06@gmail.com', 'Đang học'),
('Đỗ Văn Giang', '2010-07-25', 'Nam', '0911111117', 'do.van.giang.hs07@gmail.com', 'Đang học'),
('Bùi Thị Hà', '2010-08-30', 'Nữ', '0911111118', 'bui.thi.ha.hs08@gmail.com', 'Đang học'),
('Ngô Văn Hùng', '2010-09-05', 'Nam', '0911111119', 'ngo.van.hung.hs09@gmail.com', 'Nghỉ học'),
('Lý Thị Lan', '2010-10-10', 'Nữ', '0911111120', 'ly.thi.lan.hs10@gmail.com', 'Bảo lưu'),

-- Lớp 10A2: 9 Đang học, 1 Nghỉ học
('Phan Văn Minh', '2010-01-12', 'Nam', '0911111121', 'phan.van.minh.hs11@gmail.com', 'Đang học'),
('Võ Thị Nga', '2010-02-17', 'Nữ', '0911111122', 'vo.thi.nga.hs12@gmail.com', 'Đang học'),
('Đinh Văn Oanh', '2010-03-22', 'Nam', '0911111123', 'dinh.van.oanh.hs13@gmail.com', 'Đang học'),
('Trịnh Thị Phượng', '2010-04-27', 'Nữ', '0911111124', 'trinh.thi.phuong.hs14@gmail.com', 'Đang học'),
('Hồ Văn Quân', '2010-05-02', 'Nam', '0911111125', 'ho.van.quan.hs15@gmail.com', 'Đang học'),
('Mai Thị Rượu', '2010-06-07', 'Nữ', '0911111126', 'mai.thi.ruou.hs16@gmail.com', 'Đang học'),
('Cao Văn Sơn', '2010-07-12', 'Nam', '0911111127', 'cao.van.son.hs17@gmail.com', 'Đang học'),
('Đặng Thị Tâm', '2010-08-17', 'Nữ', '0911111128', 'dang.thi.tam.hs18@gmail.com', 'Đang học'),
('Lương Văn Út', '2010-09-22', 'Nam', '0911111129', 'luong.van.ut.hs19@gmail.com', 'Đang học'),
('Phùng Thị Vân', '2010-10-27', 'Nữ', '0911111130', 'phung.thi.van.hs20@gmail.com', 'Nghỉ học'),

-- Lớp 10A3: 10 Đang học
('Trần Văn Xuân', '2010-01-08', 'Nam', '0911111131', 'tran.van.xuan.hs21@gmail.com', 'Đang học'),
('Nguyễn Thị Yến', '2010-02-13', 'Nữ', '0911111132', 'nguyen.thi.yen.hs22@gmail.com', 'Đang học'),
('Lê Văn Zơ', '2010-03-18', 'Nam', '0911111133', 'le.van.zo.hs23@gmail.com', 'Đang học'),
('Phạm Thị An', '2010-04-23', 'Nữ', '0911111134', 'pham.thi.an.hs24@gmail.com', 'Đang học'),
('Hoàng Văn Bảo', '2010-05-28', 'Nam', '0911111135', 'hoang.van.bao.hs25@gmail.com', 'Đang học'),
('Vũ Thị Châu', '2010-06-02', 'Nữ', '0911111136', 'vu.thi.chau.hs26@gmail.com', 'Đang học'),
('Đỗ Văn Đức', '2010-07-07', 'Nam', '0911111137', 'do.van.duc.hs27@gmail.com', 'Đang học'),
('Bùi Thị Hoa', '2010-08-12', 'Nữ', '0911111138', 'bui.thi.hoa.hs28@gmail.com', 'Đang học'),
('Ngô Văn Khoa', '2010-09-17', 'Nam', '0911111139', 'ngo.van.khoa.hs29@gmail.com', 'Đang học'),
('Lý Thị Linh', '2010-10-22', 'Nữ', '0911111140', 'ly.thi.linh.hs30@gmail.com', 'Đang học'),

-- Lớp 11A1: 10 Đang học (sinh năm 2009)
('Phan Văn Mạnh', '2009-01-05', 'Nam', '0911111141', 'phan.van.manh.hs31@gmail.com', 'Đang học'),
('Võ Thị Nhi', '2009-02-10', 'Nữ', '0911111142', 'vo.thi.nhi.hs32@gmail.com', 'Đang học'),
('Đinh Văn Phú', '2009-03-15', 'Nam', '0911111143', 'dinh.van.phu.hs33@gmail.com', 'Đang học'),
('Trịnh Thị Quế', '2009-04-20', 'Nữ', '0911111144', 'trinh.thi.que.hs34@gmail.com', 'Đang học'),
('Hồ Văn Tài', '2009-05-25', 'Nam', '0911111145', 'ho.van.tai.hs35@gmail.com', 'Đang học'),
('Mai Thị Uyên', '2009-06-30', 'Nữ', '0911111146', 'mai.thi.uyen.hs36@gmail.com', 'Đang học'),
('Cao Văn Vinh', '2009-07-05', 'Nam', '0911111147', 'cao.van.vinh.hs37@gmail.com', 'Đang học'),
('Đặng Thị Xuân', '2009-08-10', 'Nữ', '0911111148', 'dang.thi.xuan.hs38@gmail.com', 'Đang học'),
('Lương Văn Ý', '2009-09-15', 'Nam', '0911111149', 'luong.van.y.hs39@gmail.com', 'Đang học'),
('Phùng Thị Anh', '2009-10-20', 'Nữ', '0911111150', 'phung.thi.anh.hs40@gmail.com', 'Đang học'),

-- Lớp 11A2: 10 Đang học
('Trần Văn Bình', '2009-01-03', 'Nam', '0911111151', 'tran.van.binh.hs41@gmail.com', 'Đang học'),
('Nguyễn Thị Chi', '2009-02-08', 'Nữ', '0911111152', 'nguyen.thi.chi.hs42@gmail.com', 'Đang học'),
('Lê Văn Duy', '2009-03-13', 'Nam', '0911111153', 'le.van.duy.hs43@gmail.com', 'Đang học'),
('Phạm Thị Hằng', '2009-04-18', 'Nữ', '0911111154', 'pham.thi.hang.hs44@gmail.com', 'Đang học'),
('Hoàng Văn Khánh', '2009-05-23', 'Nam', '0911111155', 'hoang.van.khanh.hs45@gmail.com', 'Đang học'),
('Vũ Thị Loan', '2009-06-28', 'Nữ', '0911111156', 'vu.thi.loan.hs46@gmail.com', 'Đang học'),
('Đỗ Văn Nam', '2009-07-03', 'Nam', '0911111157', 'do.van.nam.hs47@gmail.com', 'Đang học'),
('Bùi Thị Oanh', '2009-08-08', 'Nữ', '0911111158', 'bui.thi.oanh.hs48@gmail.com', 'Đang học'),
('Ngô Văn Phong', '2009-09-13', 'Nam', '0911111159', 'ngo.van.phong.hs49@gmail.com', 'Đang học'),
('Lý Thị Quyên', '2009-10-18', 'Nữ', '0911111160', 'ly.thi.quyen.hs50@gmail.com', 'Đang học'),

-- Lớp 11A3: 10 Đang học
('Nguyễn Văn Sáng', '2009-01-01', 'Nam', '0911111161', 'nguyen.van.sang.hs51@gmail.com', 'Đang học'),
('Trần Thị Thảo', '2009-02-05', 'Nữ', '0911111162', 'tran.thi.thao.hs52@gmail.com', 'Đang học'),
('Lê Văn Toàn', '2009-03-10', 'Nam', '0911111163', 'le.van.toan.hs53@gmail.com', 'Đang học'),
('Phạm Thị Út', '2009-04-15', 'Nữ', '0911111164', 'pham.thi.ut.hs54@gmail.com', 'Đang học'),
('Hoàng Văn Vũ', '2009-05-20', 'Nam', '0911111165', 'hoang.van.vu.hs55@gmail.com', 'Đang học'),
('Vũ Thị Xuân', '2009-06-25', 'Nữ', '0911111166', 'vu.thi.xuan.hs56@gmail.com', 'Đang học'),
('Đỗ Văn Yên', '2009-07-30', 'Nam', '0911111167', 'do.van.yen.hs57@gmail.com', 'Đang học'),
('Bùi Thị An', '2009-08-05', 'Nữ', '0911111168', 'bui.thi.an.hs58@gmail.com', 'Đang học'),
('Ngô Văn Bảo', '2009-09-10', 'Nam', '0911111169', 'ngo.van.bao.hs59@gmail.com', 'Đang học'),
('Lý Thị Châu', '2009-10-15', 'Nữ', '0911111170', 'ly.thi.chau.hs60@gmail.com', 'Đang học'),

-- Lớp 12A1: 10 Đang học (sinh năm 2008)
('Phan Văn Đức', '2008-01-20', 'Nam', '0911111171', 'phan.van.duc.hs61@gmail.com', 'Đang học'),
('Võ Thị Hoa', '2008-02-25', 'Nữ', '0911111172', 'vo.thi.hoa.hs62@gmail.com', 'Đang học'),
('Đinh Văn Khoa', '2008-03-30', 'Nam', '0911111173', 'dinh.van.khoa.hs63@gmail.com', 'Đang học'),
('Trịnh Thị Linh', '2008-04-05', 'Nữ', '0911111174', 'trinh.thi.linh.hs64@gmail.com', 'Đang học'),
('Hồ Văn Mạnh', '2008-05-10', 'Nam', '0911111175', 'ho.van.manh.hs65@gmail.com', 'Đang học'),
('Mai Thị Nga', '2008-06-15', 'Nữ', '0911111176', 'mai.thi.nga.hs66@gmail.com', 'Đang học'),
('Cao Văn Oanh', '2008-07-20', 'Nam', '0911111177', 'cao.van.oanh.hs67@gmail.com', 'Đang học'),
('Đặng Thị Phương', '2008-08-25', 'Nữ', '0911111178', 'dang.thi.phuong.hs68@gmail.com', 'Đang học'),
('Lương Văn Quân', '2008-09-30', 'Nam', '0911111179', 'luong.van.quan.hs69@gmail.com', 'Đang học'),
('Phùng Thị Rượu', '2008-10-05', 'Nữ', '0911111180', 'phung.thi.ruou.hs70@gmail.com', 'Đang học'),

-- Lớp 12A2: 10 Đang học
('Trần Văn Sơn', '2008-01-15', 'Nam', '0911111181', 'tran.van.son.hs71@gmail.com', 'Đang học'),
('Nguyễn Thị Tâm', '2008-02-20', 'Nữ', '0911111182', 'nguyen.thi.tam.hs72@gmail.com', 'Đang học'),
('Lê Văn Út', '2008-03-25', 'Nam', '0911111183', 'le.van.ut.hs73@gmail.com', 'Đang học'),
('Phạm Thị Vân', '2008-04-30', 'Nữ', '0911111184', 'pham.thi.van.hs74@gmail.com', 'Đang học'),
('Hoàng Văn Xuân', '2008-05-05', 'Nam', '0911111185', 'hoang.van.xuan.hs75@gmail.com', 'Đang học'),
('Vũ Thị Yến', '2008-06-10', 'Nữ', '0911111186', 'vu.thi.yen.hs76@gmail.com', 'Đang học'),
('Đỗ Văn An', '2008-07-15', 'Nam', '0911111187', 'do.van.an.hs77@gmail.com', 'Đang học'),
('Bùi Thị Bích', '2008-08-20', 'Nữ', '0911111188', 'bui.thi.bich.hs78@gmail.com', 'Đang học'),
('Ngô Văn Cường', '2008-09-25', 'Nam', '0911111189', 'ngo.van.cuong.hs79@gmail.com', 'Đang học'),
('Lý Thị Dung', '2008-10-30', 'Nữ', '0911111190', 'ly.thi.dung.hs80@gmail.com', 'Đang học'),

-- Lớp 12A3: 10 Đang học
('Phan Văn Em', '2008-01-10', 'Nam', '0911111191', 'phan.van.em.hs81@gmail.com', 'Đang học'),
('Võ Thị Phương', '2008-02-15', 'Nữ', '0911111192', 'vo.thi.phuong.hs82@gmail.com', 'Đang học'),
('Đinh Văn Giang', '2008-03-20', 'Nam', '0911111193', 'dinh.van.giang.hs83@gmail.com', 'Đang học'),
('Trịnh Thị Hà', '2008-04-25', 'Nữ', '0911111194', 'trinh.thi.ha.hs84@gmail.com', 'Đang học'),
('Hồ Văn Hùng', '2008-05-30', 'Nam', '0911111195', 'ho.van.hung.hs85@gmail.com', 'Đang học'),
('Mai Thị Kiều', '2008-06-05', 'Nữ', '0911111196', 'mai.thi.kieu.hs86@gmail.com', 'Đang học'),
('Cao Văn Long', '2008-07-10', 'Nam', '0911111197', 'cao.van.long.hs87@gmail.com', 'Đang học'),
('Đặng Thị Mai', '2008-08-15', 'Nữ', '0911111198', 'dang.thi.mai.hs88@gmail.com', 'Đang học'),
('Lương Văn Nam', '2008-09-20', 'Nam', '0911111199', 'luong.van.nam.hs89@gmail.com', 'Đang học'),
('Phùng Thị Oanh', '2008-10-25', 'Nữ', '0911111200', 'phung.thi.oanh.hs90@gmail.com', 'Đang học');

-- 8. INSERT PhuHuynh
INSERT INTO PhuHuynh (HoTen, SoDienThoai, Email, DiaChi) VALUES
('Nguyễn Văn Hòa', '0921111111', 'nguyen.van.hoa.ph01@gmail.com', 'HCMC'),
('Trần Thị Lan', '0921111112', 'tran.thi.lan.ph02@gmail.com', 'HCMC'),
('Lê Văn Mạnh', '0921111113', 'le.van.manh.ph03@gmail.com', 'HCMC'),
('Phạm Thị Nga', '0921111114', 'pham.thi.nga.ph04@gmail.com', 'HCMC'),
('Hoàng Văn Phú', '0921111115', 'hoang.van.phu.ph05@gmail.com', 'HCMC'),
('Vũ Thị Quyên', '0921111116', 'vu.thi.quyen.ph06@gmail.com', 'HCMC'),
('Đỗ Văn Sơn', '0921111117', 'do.van.son.ph07@gmail.com', 'HCMC'),
('Bùi Thị Tâm', '0921111118', 'bui.thi.tam.ph08@gmail.com', 'HCMC'),
('Ngô Văn Út', '0921111119', 'ngo.van.ut.ph09@gmail.com', 'HCMC'),
('Lý Thị Vân', '0921111120', 'ly.thi.van.ph10@gmail.com', 'HCMC'),
('Phan Văn Xuân', '0921111121', 'phan.van.xuan.ph11@gmail.com', 'HCMC'),
('Võ Thị Yến', '0921111122', 'vo.thi.yen.ph12@gmail.com', 'HCMC'),
('Đinh Văn An', '0921111123', 'dinh.van.an.ph13@gmail.com', 'HCMC'),
('Trịnh Thị Bình', '0921111124', 'trinh.thi.binh.ph14@gmail.com', 'HCMC'),
('Hồ Văn Cường', '0921111125', 'ho.van.cuong.ph15@gmail.com', 'HCMC'),
('Mai Thị Dung', '0921111126', 'mai.thi.dung.ph16@gmail.com', 'HCMC'),
('Cao Văn Em', '0921111127', 'cao.van.em.ph17@gmail.com', 'HCMC'),
('Đặng Thị Phương', '0921111128', 'dang.thi.phuong.ph18@gmail.com', 'HCMC'),
('Lương Văn Giang', '0921111129', 'luong.van.giang.ph19@gmail.com', 'HCMC'),
('Phùng Thị Hà', '0921111130', 'phung.thi.ha.ph20@gmail.com', 'HCMC'),
('Trần Văn Hùng', '0921111131', 'tran.van.hung.ph21@gmail.com', 'HCMC'),
('Nguyễn Thị Kiều', '0921111132', 'nguyen.thi.kieu.ph22@gmail.com', 'HCMC'),
('Lê Văn Long', '0921111133', 'le.van.long.ph23@gmail.com', 'HCMC'),
('Phạm Thị Mai', '0921111134', 'pham.thi.mai.ph24@gmail.com', 'HCMC'),
('Hoàng Văn Nam', '0921111135', 'hoang.van.nam.ph25@gmail.com', 'HCMC'),
('Vũ Thị Oanh', '0921111136', 'vu.thi.oanh.ph26@gmail.com', 'HCMC'),
('Đỗ Văn Phát', '0921111137', 'do.van.phat.ph27@gmail.com', 'HCMC'),
('Bùi Thị Quỳnh', '0921111138', 'bui.thi.quynh.ph28@gmail.com', 'HCMC'),
('Ngô Văn Tài', '0921111139', 'ngo.van.tai.ph29@gmail.com', 'HCMC'),
('Lý Thị Uyên', '0921111140', 'ly.thi.uyen.ph30@gmail.com', 'HCMC'),
('Phan Văn Vinh', '0921111141', 'phan.van.vinh.ph31@gmail.com', 'HCMC'),
('Võ Thị Xuân', '0921111142', 'vo.thi.xuan.ph32@gmail.com', 'HCMC'),
('Đinh Văn Ý', '0921111143', 'dinh.van.y.ph33@gmail.com', 'HCMC'),
('Trịnh Thị Anh', '0921111144', 'trinh.thi.anh.ph34@gmail.com', 'HCMC'),
('Hồ Văn Bình', '0921111145', 'ho.van.binh.ph35@gmail.com', 'HCMC'),
('Mai Thị Chi', '0921111146', 'mai.thi.chi.ph36@gmail.com', 'HCMC'),
('Cao Văn Duy', '0921111147', 'cao.van.duy.ph37@gmail.com', 'HCMC'),
('Đặng Thị Hằng', '0921111148', 'dang.thi.hang.ph38@gmail.com', 'HCMC'),
('Lương Văn Khang', '0921111149', 'luong.van.khang.ph39@gmail.com', 'HCMC'),
('Phùng Thị Linh', '0921111150', 'phung.thi.linh.ph40@gmail.com', 'HCMC'),
('Trần Văn Minh', '0921111151', 'tran.van.minh.ph41@gmail.com', 'HCMC'),
('Nguyễn Thị Nga', '0921111152', 'nguyen.thi.nga.ph42@gmail.com', 'HCMC'),
('Lê Văn Oanh', '0921111153', 'le.van.oanh.ph43@gmail.com', 'HCMC'),
('Phạm Thị Phương', '0921111154', 'pham.thi.phuong.ph44@gmail.com', 'HCMC'),
('Hoàng Văn Quân', '0921111155', 'hoang.van.quan.ph45@gmail.com', 'HCMC'),
('Vũ Thị Rượu', '0921111156', 'vu.thi.ruou.ph46@gmail.com', 'HCMC'),
('Đỗ Văn Sáng', '0921111157', 'do.van.sang.ph47@gmail.com', 'HCMC'),
('Bùi Thị Thảo', '0921111158', 'bui.thi.thao.ph48@gmail.com', 'HCMC'),
('Ngô Văn Út', '0921111159', 'ngo.van.ut.ph49@gmail.com', 'HCMC'),
('Lý Thị Vân', '0921111160', 'ly.thi.van.ph50@gmail.com', 'HCMC');

-- 9. INSERT HocSinhPhuHuynh (phụ thuộc HocSinh, PhuHuynh)
INSERT INTO HocSinhPhuHuynh (MaHocSinh, MaPhuHuynh, MoiQuanHe)
SELECT MaHocSinh, MaPhuHuynh, 
    CASE WHEN MaHocSinh % 2 = 0 THEN 'Mẹ' ELSE 'Cha' END
FROM HocSinh, PhuHuynh 
WHERE HocSinh.MaHocSinh = PhuHuynh.MaPhuHuynh
LIMIT 50;

-- 10. INSERT VaiTro, ChucNang, VaiTroChucNang
INSERT INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES
('student', 'Học sinh', 'Học sinh'),
('parent', 'Phụ huynh', 'Phụ huynh'),
('teacher', 'Giáo viên', 'Giáo viên'),
('admin', 'Quản trị viên', 'Quản trị viên');

INSERT INTO ChucNang (MaChucNang, TenChucNang, MoTa) VALUES
('qlhs', 'Quản lý học sinh', 'Quản lý học sinh'),
('qlphuhuynh', 'Quản lý phụ huynh', 'Quản lý phụ huynh'),
('qlgiaovien', 'Quản lý giáo viên', 'Quản lý giáo viên');

INSERT INTO VaiTroChucNang (MaVaiTro, MaChucNang) VALUES
('student', 'qlhs'),
('parent', 'qlphuhuynh'),
('teacher', 'qlgiaovien'),
('admin', 'qlhs'),
('admin', 'qlphuhuynh'),
('admin', 'qlgiaovien');

-- 11. INSERT NguoiDung và NguoiDungVaiTro
INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) VALUES
('HS1', '12345678', 'Hoạt động'),
('HS2', '12345678', 'Hoạt động'),
('HS3', '12345678', 'Hoạt động'),
('HS4', '12345678', 'Hoạt động'),
('HS5', '12345678', 'Hoạt động'),
('GV001', '12345678', 'Hoạt động'),
('GV002', '12345678', 'Hoạt động'),
('GV003', '12345678', 'Hoạt động'),
('GV004', '12345678', 'Hoạt động'),
('GV005', '12345678', 'Hoạt động'),
('admin', '12345678', 'Hoạt động');

INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) VALUES
('HS1', 'student'),
('HS2', 'student'),
('HS3', 'student'),
('HS4', 'student'),
('HS5', 'student'),
('GV001', 'teacher'),
('GV002', 'teacher'),
('GV003', 'teacher'),
('GV004', 'teacher'),
('GV005', 'teacher'),
('admin', 'admin');

-- =====================================================================
-- 12. PHÂN LỚP - HỌC SINH ĐANG HỌC (HK I năm 2025-2026)
-- Phân bổ: 10 HS/lớp × 9 lớp = ~80 HS (chỉ phân lớp HS có TrangThai='Đang học')
-- Khối 10: 27 HS "Đang học" → 10A1, 10A2, 10A3
-- Khối 11: 30 HS "Đang học" → 11A1, 11A2, 11A3
-- Khối 12: 30 HS "Đang học" → 12A1, 12A2, 12A3
-- =====================================================================
INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy)
SELECT 
    ranked.MaHocSinh,
    CASE 
        -- Khối 10 (HS thứ 1-27)
        WHEN ranked.RowNum BETWEEN 1 AND 9 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '10A1')
        WHEN ranked.RowNum BETWEEN 10 AND 18 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '10A2')
        WHEN ranked.RowNum BETWEEN 19 AND 27 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '10A3')
        -- Khối 11 (HS thứ 28-57)
        WHEN ranked.RowNum BETWEEN 28 AND 37 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '11A1')
        WHEN ranked.RowNum BETWEEN 38 AND 47 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '11A2')
        WHEN ranked.RowNum BETWEEN 48 AND 57 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '11A3')
        -- Khối 12 (HS thứ 58-87)
        WHEN ranked.RowNum BETWEEN 58 AND 67 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '12A1')
        WHEN ranked.RowNum BETWEEN 68 AND 77 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '12A2')
        WHEN ranked.RowNum BETWEEN 78 AND 87 THEN (SELECT MaLop FROM LopHoc WHERE TenLop = '12A3')
    END AS MaLop,
    1 AS MaHocKy
FROM (
    SELECT 
        MaHocSinh,
        @row_num := @row_num + 1 AS RowNum
    FROM HocSinh, (SELECT @row_num := 0) AS init
    WHERE TrangThai = 'Đang học'
    ORDER BY MaHocSinh
) AS ranked
WHERE ranked.RowNum <= 87;

-- =====================================================================
-- 13. ĐIỂM SỐ - ĐA DẠNG TỪ THẤP ĐẾN CAO (3-10 điểm)
-- =====================================================================
INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, DiemMieng, Diem15Phut, DiemGiuaKy, DiemCuoiKy, DiemTrungBinh)
SELECT 
    hs.MaHocSinh,
    mh.MaMonHoc,
    1 AS MaHocKy,
    -- Phân phối điểm theo nhóm: 20% Yếu, 30% TB, 30% Khá, 20% Giỏi
    CASE 
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 0 THEN ROUND(3 + (RAND() * 2), 1)   -- Yếu (3-5)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 1 THEN ROUND(5 + (RAND() * 1.5), 1) -- TB (5-6.5)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 2 THEN ROUND(6.5 + (RAND() * 1.5), 1) -- Khá (6.5-8)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 3 THEN ROUND(8 + (RAND() * 2), 1)   -- Giỏi (8-10)
        ELSE ROUND(4 + (RAND() * 6), 1)                                              -- Random (4-10)
    END AS DiemMieng,
    CASE 
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 0 THEN ROUND(3 + (RAND() * 2), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 1 THEN ROUND(5 + (RAND() * 1.5), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 2 THEN ROUND(6.5 + (RAND() * 1.5), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 3 THEN ROUND(8 + (RAND() * 2), 1)
        ELSE ROUND(4 + (RAND() * 6), 1)
    END AS Diem15Phut,
    CASE 
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 0 THEN ROUND(3 + (RAND() * 2), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 1 THEN ROUND(5 + (RAND() * 1.5), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 2 THEN ROUND(6.5 + (RAND() * 1.5), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 3 THEN ROUND(8 + (RAND() * 2), 1)
        ELSE ROUND(4 + (RAND() * 6), 1)
    END AS DiemGiuaKy,
    CASE 
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 0 THEN ROUND(3 + (RAND() * 2), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 1 THEN ROUND(5 + (RAND() * 1.5), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 2 THEN ROUND(6.5 + (RAND() * 1.5), 1)
        WHEN (hs.MaHocSinh + mh.MaMonHoc) % 5 = 3 THEN ROUND(8 + (RAND() * 2), 1)
        ELSE ROUND(4 + (RAND() * 6), 1)
    END AS DiemCuoiKy,
    NULL AS DiemTrungBinh
FROM HocSinh hs
CROSS JOIN MonHoc mh
WHERE hs.TrangThai = 'Đang học';

-- Cập nhật điểm trung bình
UPDATE DiemSo
SET DiemTrungBinh = ROUND(
    (DiemMieng + Diem15Phut + DiemGiuaKy * 2 + DiemCuoiKy * 3) / 7, 
    1
)
WHERE MaHocKy = 1;

-- =====================================================================
-- 14. HẠNH KIỂM - ĐA DẠNG TỪ YẾU ĐẾN TỐT
-- =====================================================================
INSERT INTO HanhKiem (MaHocSinh, MaHocKy, XepLoai, NhanXet)
SELECT 
    MaHocSinh,
    1 AS MaHocKy,
    CASE 
        WHEN MaHocSinh % 7 = 0 THEN 'Yếu'
        WHEN MaHocSinh % 7 IN (1, 2) THEN 'Trung bình'
        WHEN MaHocSinh % 7 IN (3, 4) THEN 'Khá'
        ELSE 'Tốt'
    END AS XepLoai,
    CASE 
        WHEN MaHocSinh % 7 = 0 THEN 'Học sinh cần cải thiện hạnh kiểm'
        WHEN MaHocSinh % 7 IN (1,2) THEN 'Học sinh có hạnh kiểm trung bình'
        WHEN MaHocSinh % 7 IN (3,4) THEN 'Học sinh có hạnh kiểm khá tốt'
        ELSE 'Học sinh có hạnh kiểm tốt, gương mẫu'
    END AS NhanXet
FROM HocSinh
WHERE TrangThai = 'Đang học';

-- =====================================================================
-- 15. XẾP LOẠI - ĐA DẠNG TỪ YẾU ĐẾN GIỎI (dựa trên điểm TB thực tế)
-- =====================================================================
INSERT INTO XepLoai (MaHocSinh, MaHocKy, HocLuc, GhiChu)
SELECT 
    ds.MaHocSinh,
    1 AS MaHocKy,
    CASE 
        WHEN AVG(ds.DiemTrungBinh) >= 8.0 THEN 'Giỏi'
        WHEN AVG(ds.DiemTrungBinh) >= 6.5 THEN 'Khá'
        WHEN AVG(ds.DiemTrungBinh) >= 5.0 THEN 'Trung bình'
        ELSE 'Yếu'
    END AS HocLuc,
    CASE 
        WHEN AVG(ds.DiemTrungBinh) >= 8.0 THEN 'Học sinh có kết quả học tập xuất sắc'
        WHEN AVG(ds.DiemTrungBinh) >= 6.5 THEN 'Học sinh có kết quả học tập khá tốt'
        WHEN AVG(ds.DiemTrungBinh) >= 5.0 THEN 'Học sinh có kết quả học tập trung bình'
        ELSE 'Học sinh cần cố gắng hơn trong học tập'
    END AS GhiChu
FROM DiemSo ds
WHERE ds.MaHocKy = 1
GROUP BY ds.MaHocSinh;

-- =====================================================================
-- KẾT THÚC - DỮ LIỆU MẪU ĐÃ HOÀN TẤT
-- =====================================================================