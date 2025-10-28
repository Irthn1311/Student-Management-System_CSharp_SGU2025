
DROP DATABASE IF EXISTS QuanLyHocSinh;
CREATE DATABASE QuanLyHocSinh CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE QuanLyHocSinh;

-- =====================================================================
-- PHẦN 1: CÁC BẢNG HỆ THỐNG VÀ PHÂN QUYỀN (RBAC)
-- =====================================================================

CREATE TABLE VaiTro (
    MaVaiTro VARCHAR(10) PRIMARY KEY,
    TenVaiTro NVARCHAR(50) NOT NULL UNIQUE,
    MoTa TEXT
);

CREATE TABLE ChucNang (
    MaChucNang VARCHAR(50) PRIMARY KEY,
    TenChucNang NVARCHAR(100) NOT NULL,
    MoTa TEXT
);

CREATE TABLE VaiTroChucNang (
    MaVaiTro VARCHAR(10),
    MaChucNang VARCHAR(50),
    PRIMARY KEY (MaVaiTro, MaChucNang),
    FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro),
    FOREIGN KEY (MaChucNang) REFERENCES ChucNang(MaChucNang)
);

CREATE TABLE NguoiDung (
    TenDangNhap VARCHAR(20) PRIMARY KEY,
    MatKhau VARCHAR(255) NOT NULL,
    TrangThai VARCHAR(50) DEFAULT 'Hoạt động'
);

CREATE TABLE NguoiDungVaiTro (
    TenDangNhap VARCHAR(20),
    MaVaiTro VARCHAR(10),
    PRIMARY KEY (TenDangNhap, MaVaiTro),
    FOREIGN KEY (TenDangNhap) REFERENCES NguoiDung(TenDangNhap),
    FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro)
);

-- =====================================================================
-- PHẦN 2: CÁC BẢNG DANH MỤC VÀ THÔNG TIN CỐT LÕI
-- =====================================================================

CREATE TABLE NamHoc (
    MaNamHoc VARCHAR(10) PRIMARY KEY,
    TenNamHoc VARCHAR(50) NOT NULL,
    NgayBatDau DATE,
    NgayKetThuc DATE
);

CREATE TABLE HocKy (
    MaHocKy INT PRIMARY KEY,
    TenHocKy NVARCHAR(50) NOT NULL,
    MaNamHoc VARCHAR(10),
    TrangThai VARCHAR(50) DEFAULT 'Chưa bắt đầu',
    NgayBD DATE,
    NgayKT DATE,
    FOREIGN KEY (MaNamHoc) REFERENCES NamHoc(MaNamHoc)
);

CREATE TABLE KhoiLop (
    MaKhoi INT PRIMARY KEY,
    TenKhoi NVARCHAR(50) NOT NULL
);

CREATE TABLE MonHoc (
    MaMonHoc INT PRIMARY KEY,
    TenMonHoc NVARCHAR(100) NOT NULL,
    SoTiet INT,
    GhiChu varchar(50)
);

CREATE TABLE GiaoVien (
    MaGiaoVien VARCHAR(15) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(255),
    SoDienThoai VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    TrangThai VARCHAR(50) DEFAULT 'Đang giảng dạy'
);

-- Bảng mới: GiaoVien_ChuyenMon để quản lý các môn học giáo viên có thể dạy
CREATE TABLE GiaoVienChuyenMon (
    MaGiaoVien VARCHAR(15),
    MaMonHoc INT,
    LaChuyenMonChinh BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (MaGiaoVien, MaMonHoc),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc)
);


CREATE TABLE HocSinh (
    MaHocSinh INT PRIMARY KEY AUTO_INCREMENT,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    SDTHS VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    TrangThai VARCHAR(50) DEFAULT 'Đang học'
);

CREATE TABLE PhuHuynh (
    MaPhuHuynh INT PRIMARY KEY AUTO_INCREMENT,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    DiaChi NVARCHAR(255)
);

CREATE TABLE LopHoc (
    MaLop INT PRIMARY KEY AUTO_INCREMENT,
    TenLop VARCHAR(50) NOT NULL,
    MaKhoi INT,
    MaGiaoVienChuNhiem VARCHAR(15),
    FOREIGN KEY (MaKhoi) REFERENCES KhoiLop(MaKhoi),
    FOREIGN KEY (MaGiaoVienChuNhiem) REFERENCES GiaoVien(MaGiaoVien)
);

-- =====================================================================
-- PHẦN 3: CÁC BẢNG NGHIỆP VỤ VÀ GIAO DỊCH
-- =====================================================================

CREATE TABLE HocSinhPhuHuynh (
    MaHocSinh INT,
    MaPhuHuynh INT,
    MoiQuanHe NVARCHAR(50),
    PRIMARY KEY (MaHocSinh, MaPhuHuynh),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaPhuHuynh) REFERENCES PhuHuynh(MaPhuHuynh)
);

CREATE TABLE PhanLop (
    MaHocSinh INT,
    MaLop INT,
    MaHocKy INT,
    PRIMARY KEY (MaHocSinh, MaLop, MaHocKy),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

CREATE TABLE GiaoVien_MonHoc (
    MaGiaoVien VARCHAR(15),
    MaMonHoc INT,
    LaMonChinh BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (MaGiaoVien, MaMonHoc),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc)
);

CREATE TABLE PhanCongGiangDay (
    MaPhanCong INT PRIMARY KEY AUTO_INCREMENT,
    MaLop INT,
    MaGiaoVien VARCHAR(15),
    MaMonHoc INT,
    MaHocKy INT,
    NgayBatDau DATE,
    NgayKetThuc DATE,
    UNIQUE (MaLop, MaGiaoVien, MaMonHoc, MaHocKy),
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

CREATE TABLE ThoiKhoaBieu (
    MaThoiKhoaBieu INT PRIMARY KEY AUTO_INCREMENT,
    MaPhanCong INT,
    ThuTrongTuan NVARCHAR(20),
    TietBatDau INT,
    SoTiet INT,
    PhongHoc VARCHAR(50),
    FOREIGN KEY (MaPhanCong) REFERENCES PhanCongGiangDay(MaPhanCong)
);

-- Bảng tạm cho Auto Phân công
CREATE TABLE IF NOT EXISTS PhanCong_Temp (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    MaLop INT NOT NULL,
    MaGiaoVien VARCHAR(15) NOT NULL,
    MaMonHoc INT NOT NULL,
    MaHocKy INT NOT NULL,
    SoTietTuan INT NOT NULL,
    Score INT NULL,
    Note NVARCHAR(255) NULL,
    UNIQUE KEY uq_pc_temp (MaLop, MaGiaoVien, MaMonHoc, MaHocKy),
    INDEX idx_pc_temp_hk (MaHocKy)
);

-- Bảng tạm để lưu lịch sinh tự động trước khi chấp nhận chính thức
CREATE TABLE IF NOT EXISTS TKB_Temp (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    SemesterId INT NOT NULL,
    WeekNo INT NOT NULL,
    MaLop INT NOT NULL,
    Thu INT NOT NULL,
    Tiet INT NOT NULL,
    MaMon INT NOT NULL,
    MaGV VARCHAR(15) NOT NULL,
    Phong VARCHAR(50) NULL,
    UNIQUE KEY uq_temp_slot (SemesterId, WeekNo, MaLop, Thu, Tiet),
    INDEX idx_temp_teacher (SemesterId, WeekNo, MaGV, Thu, Tiet)
);

CREATE TABLE DiemSo (
    MaHocSinh INT,
    MaMonHoc INT,
    MaHocKy INT,
    DiemMieng FLOAT,
    Diem15Phut FLOAT,
    DiemGiuaKy FLOAT,
    DiemCuoiKy FLOAT,
    DiemTrungBinh FLOAT,
    PRIMARY KEY (MaHocSinh, MaMonHoc, MaHocKy),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

CREATE TABLE HanhKiem (
    MaHocSinh INT,
    MaHocKy INT,
    XepLoai NVARCHAR(50),
    NhanXet TEXT,
    PRIMARY KEY (MaHocSinh, MaHocKy),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

CREATE TABLE XepLoai (
    MaHocSinh INT,
    MaHocKy INT,
    HocLuc NVARCHAR(50),
    GhiChu TEXT,
    PRIMARY KEY (MaHocSinh, MaHocKy),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

CREATE TABLE KhenThuongKyLuat (
    MaKTKL INT PRIMARY KEY AUTO_INCREMENT,
    MaHocSinh INT,
    Loai NVARCHAR(20) NOT NULL,
    NoiDung TEXT NOT NULL,
    CapKhenThuong NVARCHAR(100),
    MucXuLy NVARCHAR(100),
    NgayApDung DATE,
    NguoiLapID VARCHAR(20),
    TrangThaiDuyet VARCHAR(50) DEFAULT 'Chờ duyệt',
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (NguoiLapID) REFERENCES NguoiDung(TenDangNhap)
);

CREATE TABLE ThongBao (
    MaThongBao INT PRIMARY KEY AUTO_INCREMENT,
    TieuDe VARCHAR(255) NOT NULL,
    NoiDung TEXT NULL, -- Nội dung có thể để trống
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP, -- Tự động lấy ngày giờ hiện tại
    LoaiThongBao VARCHAR(50) NOT NULL, -- Ví dụ: Chung, LichThi, SuKien, KhenThuong, HocPhi...
    DoiTuongNhan VARCHAR(255) NOT NULL, -- Ví dụ: ALL, 10A1, 10A2, GiaoVien, HocSinh...
    NgayHetHan DATETIME NULL, -- Có thể để trống nếu không có ngày hết hạn
    MaNguoiTao VARCHAR(20), -- Người tạo thông báo
    FOREIGN KEY (MaNguoiTao) REFERENCES NguoiDung(TenDangNhap)
);
-- =====================================================================
-- Kết thúc script
-- =====================================================================


-- INSERT khối và lớp
INSERT INTO KhoiLop (MaKhoi, TenKhoi) VALUES (10, 'Khối 10'), (11, 'Khối 11'), (12, 'Khối 12');

-- INSERT giáo viên (60)
INSERT INTO GiaoVien (MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, TrangThai) VALUES
('GV001', 'Đỗ Văn An', '1993-04-05', 'Nam', 'HCMC', '0392458591', 'dỗ.văn.an.gv001@hotmail.com', 'Đang giảng dạy'),
('GV002', 'Bùi Quốc Bảo', '1991-04-08', 'Nam', 'HCMC', '0394335942', 'bùi.quốc.bảo.gv002@gmail.com', 'Đang giảng dạy'),
('GV003', 'Trịnh Thảo Vy', '1993-08-19', 'Nữ', 'HCMC', '0903678638', 'trịnh.thảo.vy.gv003@student.edu.vn', 'Đang giảng dạy'),
('GV004', 'Trịnh Quốc Nam', '1992-04-25', 'Nữ', 'HCMC', '0932556017', 'trịnh.quốc.nam.gv004@hotmail.com', 'Đang giảng dạy'),
('GV005', 'Vũ Văn Nhật', '1999-05-26', 'Nam', 'HCMC', '0369996414', 'vũ.văn.nhật.gv005@hotmail.com', 'Đang giảng dạy'),
('GV006', 'Trần Chí Phúc', '1998-05-27', 'Nữ', 'HCMC', '0962166941', 'trần.chí.phúc.gv006@gmail.com', 'Đang giảng dạy'),
('GV007', 'Nguyễn Thảo Long', '1991-04-28', 'Nam', 'HCMC', '0345663623', 'nguyễn.thảo.long.gv007@hotmail.com', 'Đang giảng dạy'),
('GV008', 'Võ Thảo Nhật', '1995-06-07', 'Nữ', 'HCMC', '0923871230', 'võ.thảo.nhật.gv008@yahoo.com', 'Đang giảng dạy'),
('GV009', 'Đặng Hồng Long', '1997-07-09', 'Nam', 'HCMC', '0321938483', 'dặng.hồng.long.gv009@yahoo.com', 'Đang giảng dạy'),
('GV010', 'Phạm Hoàng Bảo', '1996-05-03', 'Nam', 'HCMC', '0324567281', 'phạm.hoàng.bảo.gv010@hotmail.com', 'Đang giảng dạy'),
('GV011', 'Đỗ Tuấn Phúc', '1992-05-05', 'Nam', 'HCMC', '0395408072', 'dỗ.tuấn.phúc.gv011@student.edu.vn', 'Đang giảng dạy'),
('GV012', 'Trịnh Trung Quỳnh', '1995-04-05', 'Nữ', 'HCMC', '0921790481', 'trịnh.trung.quỳnh.gv012@student.edu.vn', 'Đang giảng dạy'),
('GV013', 'Võ Thảo Nhật', '1995-06-07', 'Nữ', 'HCMC', '0923871230', 'võ.thảo.nhật.gv013@yahoo.com', 'Đang giảng dạy'),
('GV014', 'Đặng Hồng Long', '1997-07-09', 'Nam', 'HCMC', '0321938483', 'dặng.hồng.long.gv014@yahoo.com', 'Đang giảng dạy'),
('GV015', 'Phạm Hoàng Bảo', '1996-05-03', 'Nam', 'HCMC', '0324567281', 'phạm.hoàng.bảo.gv015@hotmail.com', 'Đang giảng dạy');


INSERT INTO LopHoc (TenLop, MaKhoi, MaGiaoVienChuNhiem) VALUES
('10A1', 10, 'GV001'),
('10A2', 10, 'GV002'),
('10A3', 10, 'GV003'),
('10A4', 10, 'GV004'),
('10A5', 10, 'GV005'),
('11A1', 11, 'GV006'),
('11A2', 11, 'GV007'),
('11A3', 11, 'GV008'),
('11A4', 11, 'GV009'),
('11A5', 11, 'GV010'),
('12A1', 12, 'GV011'),
('12A2', 12, 'GV012'),
('12A3', 12, 'GV013'),
('12A4', 12, 'GV014'),
('12A5', 12, 'GV015');


-- INSERT học sinh (500)
INSERT INTO HocSinh (HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai) VALUES
('Trương Hồng Thu', '2009-01-18', 'Nam', '0938658158', 'trương.hồng.thu.hs0001@yahoo.com', 'Đang học'),
('Vũ Hoàng Thi', '2009-03-24', 'Nữ', '0365348512', 'vũ.hoàng.thi.hs0002@yahoo.com', 'Đang học'),
('Trần Hồng Mai', '2010-05-11', 'Nữ', '0392351868', 'trần.hồng.mai.hs0003@yahoo.com', 'Đang học'),
('Phan Anh Tâm', '2009-01-07', 'Nữ', '0341327691', 'phan.anh.tâm.hs0004@student.edu.vn', 'Đang học'),
('Trương Trung Long', '2009-04-09', 'Nữ', '0371486955', 'trương.trung.long.hs0005@student.edu.vn', 'Đang học'),
('Đặng Thị Phúc', '2010-02-21', 'Nữ', '0948746014', 'dặng.thị.phúc.hs0006@yahoo.com', 'Đang học'),
('Trương Chí Phúc', '2009-07-09', 'Nam', '0371325197', 'trương.chí.phúc.hs0007@gmail.com', 'Đang học'),
('Hồ Thị Trung', '2010-04-05', 'Nữ', '0934656838', 'hồ.thị.trung.hs0008@student.edu.vn', 'Đang học'),
('Huỳnh Văn Trâm', '2010-05-19', 'Nữ', '0344327421', 'huỳnh.văn.trâm.hs0009@gmail.com', 'Đang học'),
('Trương Văn Trâm', '2010-06-18', 'Nữ', '0332156902', 'trương.văn.trâm.hs0010@hotmail.com', 'Đang học'),
('Trịnh Hữu Quỳnh', '2010-12-17', 'Nữ', '0399111742', 'trịnh.hữu.quỳnh.hs0011@student.edu.vn', 'Đang học'),
('Ngô Tuấn Long', '2009-10-20', 'Nữ', '0321481506', 'ngô.tuấn.long.hs0012@student.edu.vn', 'Đang học'),
('Huỳnh Chí Trung', '2009-09-01', 'Nam', '0925049282', 'huỳnh.chí.trung.hs0013@student.edu.vn', 'Đang học'),
('Trần Minh Long', '2009-12-08', 'Nữ', '0338939993', 'trần.minh.long.hs0014@hotmail.com', 'Đang học'),
('Huỳnh Ngọc Dũng', '2010-06-04', 'Nam', '0964630331', 'huỳnh.ngọc.dũng.hs0015@student.edu.vn', 'Đang học'),
('Phạm Thanh Khanh', '2009-01-23', 'Nam', '0981763626', 'phạm.thanh.khanh.hs0016@gmail.com', 'Đang học'),
('Huỳnh Tuấn Thảo', '2009-06-06', 'Nam', '0989014965', 'huỳnh.tuấn.thảo.hs0017@gmail.com', 'Đang học'),
('Trương Trung Mai', '2010-04-04', 'Nữ', '0979767312', 'trương.trung.mai.hs0018@student.edu.vn', 'Đang học'),
('Nguyễn Trung Yến', '2010-04-21', 'Nam', '0982362099', 'nguyễn.trung.yến.hs0019@yahoo.com', 'Đang học'),
('Trịnh Trung Thảo', '2009-01-08', 'Nữ', '0998617381', 'trịnh.trung.thảo.hs0020@gmail.com', 'Đang học');

-- INSERT phụ huynh và liên kết HS-Phụ huynh (1 parent per student)
INSERT INTO PhuHuynh (MaPhuHuynh, HoTen, SoDienThoai, Email, DiaChi) VALUES
(1, 'Trương Tuấn Vinh', '0386323376', 'trương.tuấn.vinh.ph0001@student.edu.vn', '157 Đường Phan Đình Phùng'),
(2, 'Hồ Thảo Linh', '0389130188', 'hồ.thảo.linh.ph0002@yahoo.com', '71 Đường Nguyễn Thị Minh Khai'),
(3, 'Lê Ngọc Phúc', '0944589533', 'lê.ngọc.phúc.ph0003@gmail.com', '107 Đường Nguyễn Thị Minh Khai'),
(4, 'Võ Thị Nhật', '0997542890', 'võ.thị.nhật.ph0004@student.edu.vn', '138 Đường Phan Đình Phùng'),
(5, 'Phan Thảo Vinh', '0343768991', 'phan.thảo.vinh.ph0005@student.edu.vn', '33 Đường Phan Đình Phùng'),
(6, 'Nguyễn Minh Phúc', '0324551070', 'nguyễn.minh.phúc.ph0006@student.edu.vn', '84 Đường Trần Hưng Đạo'),
(7, 'Phan Ngọc Tuấn', '0921675419', 'phan.ngọc.tuấn.ph0007@gmail.com', '64 Đường Hoàng Văn Thụ'),
(8, 'Trịnh Minh Hà', '0333815034', 'trịnh.minh.hà.ph0008@gmail.com', '200 Đường Hoàng Văn Thụ'),
(9, 'Bùi Hồng Tuấn', '0972709620', 'bùi.hồng.tuấn.ph0009@hotmail.com', '176 Đường Phan Đình Phùng'),
(10, 'Nguyễn Hoàng Quỳnh', '0372770648', 'nguyễn.hoàng.quỳnh.ph0010@student.edu.vn', '93 Đường Nguyễn Thị Minh Khai'),
(11, 'Vũ Hoàng Yến', '0986407373', 'vũ.hoàng.yến.ph0011@yahoo.com', '23 Đường Trần Hưng Đạo'),
(12, 'Hồ Thanh Khanh', '0374558780', 'hồ.thanh.khanh.ph0012@hotmail.com', '67 Đường Trần Hưng Đạo'),
(13, 'Võ Anh Hà', '0978987516', 'võ.anh.hà.ph0013@student.edu.vn', '115 Đường Lê Lợi'),
(14, 'Vũ Hồng Thi', '0326902401', 'vũ.hồng.thi.ph0014@student.edu.vn', '70 Đường Trần Hưng Đạo'),
(15, 'Huỳnh Hồng Trâm', '0385747930', 'huỳnh.hồng.trâm.ph0015@gmail.com', '50 Đường Trần Hưng Đạo'),
(16, 'Đặng Minh Vy', '0949235577', 'dặng.minh.vy.ph0016@gmail.com', '4 Đường Phan Đình Phùng'),
(17, 'Hồ Văn Phúc', '0372242892', 'hồ.văn.phúc.ph0017@gmail.com', '39 Đường Hoàng Văn Thụ'),
(18, 'Võ Chí Tâm', '0998194570', 'võ.chí.tâm.ph0018@hotmail.com', '146 Đường Phan Đình Phùng'),
(19, 'Phạm Hữu Thi', '0923626181', 'phạm.hữu.thi.ph0019@gmail.com', '105 Đường Nguyễn Thị Minh Khai'),
(20, 'Đỗ Ngọc Linh', '0968132648', 'dỗ.ngọc.linh.ph0020@gmail.com', '140 Đường Hoàng Văn Thụ');
INSERT INTO HocSinhPhuHuynh (MaHocSinh, MaPhuHuynh, MoiQuanHe) VALUES
(1, 1, 'Cha/Mẹ'),
(2, 2, 'Cha/Mẹ'),
(3, 3, 'Cha/Mẹ'),
(4, 4, 'Cha/Mẹ'),
(5, 5, 'Cha/Mẹ'),
(6, 6, 'Cha/Mẹ'),
(7, 7, 'Cha/Mẹ'),
(8, 8, 'Cha/Mẹ'),
(9, 9, 'Cha/Mẹ'),
(10, 10, 'Cha/Mẹ'),
(11, 11, 'Cha/Mẹ'),
(12, 12, 'Cha/Mẹ'),
(13, 13, 'Cha/Mẹ'),
(14, 14, 'Cha/Mẹ'),
(15, 15, 'Cha/Mẹ'),
(16, 16, 'Cha/Mẹ'),
(17, 17, 'Cha/Mẹ'),
(18, 18, 'Cha/Mẹ'),
(19, 19, 'Cha/Mẹ'),
(20, 20, 'Cha/Mẹ');


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

-- INSERT users: students use their student account (and parents will login using student's account).
INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) VALUES
('HS1', '12345678', 'Hoạt động'),
('HS2', '12345678', 'Hoạt động'),
('HS3', '12345678', 'Hoạt động'),
('HS4', '12345678', 'Hoạt động'),
('HS5', '12345678', 'Hoạt động'),
('HS6', '12345678', 'Hoạt động'),
('HS7', '12345678', 'Hoạt động'),
('HS8', '12345678', 'Hoạt động'),
('HS9', '12345678', 'Hoạt động'),
('HS10', '12345678', 'Hoạt động'),
('HS11', '12345678', 'Hoạt động'),
('HS12', '12345678', 'Hoạt động'),
('HS13', '12345678', 'Hoạt động'),
('HS14', '12345678', 'Hoạt động'),
('HS15', '12345678', 'Hoạt động'),
('HS16', '12345678', 'Hoạt động'),
('HS17', '12345678', 'Hoạt động'),
('HS18', '12345678', 'Hoạt động'),
('HS19', '12345678', 'Hoạt động'),
('HS20', '12345678', 'Hoạt động'),
('GV0001', '12345678',  'Hoạt động'),
('GV0002', '12345678',  'Hoạt động'),
('GV0003', '12345678',  'Hoạt động'),
('GV0004', '12345678',  'Hoạt động'),
('GV0005', '12345678',  'Hoạt động'),
('GV0006', '12345678',  'Hoạt động'),
('GV0007', '12345678',  'Hoạt động'),
('GV0008', '12345678',  'Hoạt động'),
('GV0009', '12345678',  'Hoạt động'),
('GV0010', '12345678',  'Hoạt động'),
('GV0011', '12345678',  'Hoạt động'),
('GV0012', '12345678',  'Hoạt động'),
('GV0013', '12345678',  'Hoạt động'),
('GV0014', '12345678',  'Hoạt động'),
('GV0015', '12345678',  'Hoạt động'),
('admin', '12345678', 'Hoạt động');

INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) VALUES
('HS1', 'student'),
('HS2', 'student'),
('HS3', 'student'),
('HS4', 'student'),
('HS5', 'student'),
('HS6', 'student'),
('HS7', 'student'),
('HS8', 'student'),
('HS9', 'student'),
('HS10', 'student'),
('HS11', 'student'),
('HS12', 'student'),
('HS13', 'student'),
('HS14', 'student'),
('HS15', 'student'),
('HS16', 'student'),
('HS17', 'student'),
('HS18', 'student'),
('HS19', 'student'),
('HS20', 'student'),
('GV0001', 'teacher'),
('GV0002', 'teacher'),
('GV0003', 'teacher'),
('GV0004', 'teacher'),
('GV0005', 'teacher'),
('GV0006', 'teacher'),
('GV0007', 'teacher'),
('GV0008', 'teacher'),
('GV0009', 'teacher'),
('GV0010', 'teacher'),
('GV0011', 'teacher'),
('GV0012', 'teacher'),
('GV0013', 'teacher'),
('GV0014', 'teacher'),
('GV0015', 'teacher'),
('admin', 'admin');

-- Thêm năm học và học kỳ
INSERT INTO NamHoc (MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc) VALUES
('2024-2025', 'Năm học 2024-2025', '2024-09-01', '2025-05-31'),
('2025-2026', 'Năm học 2025-2065', '2025-09-01', '2026-05-31');

INSERT INTO HocKy (MaHocKy, TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT) VALUES
(1, 'Học kỳ 1', '2024-2025', 'Đang diễn ra', '2024-01-01', '2024-12-31'),
(2, 'Học kỳ I', '2025-2026', 'Chưa bắt đầu', '2025-09-01', '2025-12-31'),
(3, 'Học kỳ II', '2025-2026', 'Chưa bắt đầu', '2025-12-12', '2026-05-15');

INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy) VALUES
(1, 1, 1),
(2, 1, 1),
(3, 1, 1),
(4, 1, 1),
(5, 11, 1),
(6, 1, 1),
(7, 1, 1),
(8, 1, 1),
(9, 1, 1),
(10, 1, 1),
(11, 6, 1),
(12, 1, 1),
(13, 1, 1),
(14, 1, 1),
(15, 1, 1),
(16, 1, 1),
(17, 1, 1),
(18, 1, 1),
(19, 1, 1),
(20, 1, 1);

-- Môn học và phân công chuyên môn (ví dụ)
INSERT INTO MonHoc (MaMonHoc ,TenMonHoc, SoTiet, GhiChu) VALUES
(1, 'Ngữ văn', 50, 'Môn chính'),
(2, 'Toán', 60, 'Môn chính'),
(3, 'Tiếng Anh', 41, 'Môn chính'),
(4, 'Lịch sử', 36, 'Khoa học xã hội'),
(5, 'Giáo dục thể chất', 35, 'Kỹ năng khác'),
(6, 'Giáo dục Quốc phòng và An ninh', 26, 'Kỹ năng khác'),
(7, 'Địa lý', 50, 'Khoa học xã hội'),
(8, 'Vật lý', 35, 'Khoa học tự nhiên'),
(9, 'Hóa học', 41, 'Khoa học tự nhiên'),
(10, 'Sinh học', 23, 'Khoa học tự nhiên'),
(11, 'Công nghệ', 41, 'Khoa học xã hội'),
(12, 'Tin học', 53, 'Kỹ năng khác'),
(13, 'Giáo Dục Công Dân', 70, 'Khoa học xã hội');
INSERT INTO GiaoVienChuyenMon (MaGiaoVien, MaMonHoc, LaChuyenMonChinh) VALUES
('GV001', 2, 0),
('GV002', 8, 0),
('GV002', 11, 0),
('GV002', 10, 0),
('GV003', 1, 1),
('GV004', 6, 0),
('GV004', 13, 0),
('GV005', 13, 0),
('GV006', 5, 0),
('GV007', 7, 0),
('GV007', 2, 1),
('GV007', 9, 1),
('GV008', 7, 0),
('GV008', 2, 0),
('GV009', 4, 0),
('GV009', 13, 0),
('GV010', 8, 0),
('GV011', 6, 0),
('GV012', 9, 0),
('GV013', 12, 0),
('GV014', 1, 0),
('GV015', 10, 0);


-- Indexes
ALTER TABLE HocSinh ADD INDEX idx_email (Email);
ALTER TABLE GiaoVien ADD INDEX idx_email_gv (Email);

-- SUMMARY: 500 students, 60 teachers, 15 classes created.
