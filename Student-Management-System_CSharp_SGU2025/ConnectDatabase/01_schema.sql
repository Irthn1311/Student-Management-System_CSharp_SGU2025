-- =====================================================================
-- FILE 01: SCHEMA DEFINITION
-- Mục đích: Tạo database và các bảng cơ bản (DDL thuần túy)
-- Chạy: mysql -u root -p < 01_schema.sql
-- =====================================================================

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE ChucNang (
    MaChucNang VARCHAR(50) PRIMARY KEY,
    TenChucNang NVARCHAR(100) NOT NULL,
    MoTa TEXT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE VaiTroChucNang (
    MaVaiTro VARCHAR(10),
    MaChucNang VARCHAR(50),
    PRIMARY KEY (MaVaiTro, MaChucNang),
    FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro),
    FOREIGN KEY (MaChucNang) REFERENCES ChucNang(MaChucNang)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE NguoiDung (
    TenDangNhap VARCHAR(20) PRIMARY KEY,
    MatKhau VARCHAR(255) NOT NULL,
    TrangThai VARCHAR(50) DEFAULT 'Hoạt động'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE NguoiDungVaiTro (
    TenDangNhap VARCHAR(20),
    MaVaiTro VARCHAR(10),
    PRIMARY KEY (TenDangNhap, MaVaiTro),
    FOREIGN KEY (TenDangNhap) REFERENCES NguoiDung(TenDangNhap),
    FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =====================================================================
-- PHẦN 2: CÁC BẢNG DANH MỤC VÀ THÔNG TIN CỐT LÕI
-- =====================================================================

CREATE TABLE NamHoc (
    MaNamHoc VARCHAR(10) PRIMARY KEY,
    TenNamHoc VARCHAR(50) NOT NULL,
    NgayBatDau DATE,
    NgayKetThuc DATE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE HocKy (
    MaHocKy INT PRIMARY KEY,
    TenHocKy NVARCHAR(50) NOT NULL,
    MaNamHoc VARCHAR(10),
    TrangThai VARCHAR(50) DEFAULT 'Chưa bắt đầu',
    NgayBD DATE,
    NgayKT DATE,
    FOREIGN KEY (MaNamHoc) REFERENCES NamHoc(MaNamHoc)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE KhoiLop (
    MaKhoi INT PRIMARY KEY,
    TenKhoi NVARCHAR(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE MonHoc (
    MaMonHoc INT PRIMARY KEY,
    TenMonHoc NVARCHAR(100) NOT NULL,
    SoTiet INT,
    GhiChu VARCHAR(50)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE GiaoVien (
    MaGiaoVien VARCHAR(15) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(255),
    SoDienThoai VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    TrangThai VARCHAR(50) DEFAULT 'Đang giảng dạy'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE GiaoVienChuyenMon (
    MaGiaoVien VARCHAR(15),
    MaMonHoc INT,
    LaChuyenMonChinh BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (MaGiaoVien, MaMonHoc),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE HocSinh (
    MaHocSinh INT PRIMARY KEY AUTO_INCREMENT,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    SDTHS VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    TrangThai VARCHAR(50) DEFAULT 'Đang học'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE PhuHuynh (
    MaPhuHuynh INT PRIMARY KEY AUTO_INCREMENT,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    DiaChi NVARCHAR(255)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE LopHoc (
    MaLop INT PRIMARY KEY AUTO_INCREMENT,
    TenLop VARCHAR(50) NOT NULL,
    MaKhoi INT,
    SiSo INT,
    MaGiaoVienChuNhiem VARCHAR(15),
    FOREIGN KEY (MaKhoi) REFERENCES KhoiLop(MaKhoi),
    FOREIGN KEY (MaGiaoVienChuNhiem) REFERENCES GiaoVien(MaGiaoVien)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE PhanLop (
    MaHocSinh INT,
    MaLop INT,
    MaHocKy INT,
    PRIMARY KEY (MaHocSinh, MaLop, MaHocKy),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE GiaoVien_MonHoc (
    MaGiaoVien VARCHAR(15),
    MaMonHoc INT,
    LaMonChinh BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (MaGiaoVien, MaMonHoc),
    FOREIGN KEY (MaGiaoVien) REFERENCES GiaoVien(MaGiaoVien),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE ThoiKhoaBieu (
    MaThoiKhoaBieu INT PRIMARY KEY AUTO_INCREMENT,
    MaPhanCong INT,
    ThuTrongTuan NVARCHAR(20),
    TietBatDau INT,
    SoTiet INT,
    PhongHoc VARCHAR(50),
    FOREIGN KEY (MaPhanCong) REFERENCES PhanCongGiangDay(MaPhanCong)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE HanhKiem (
    MaHocSinh INT,
    MaHocKy INT,
    XepLoai NVARCHAR(50),
    NhanXet TEXT,
    PRIMARY KEY (MaHocSinh, MaHocKy),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE XepLoai (
    MaHocSinh INT,
    MaHocKy INT,
    HocLuc NVARCHAR(50),
    GhiChu TEXT,
    PRIMARY KEY (MaHocSinh, MaHocKy),
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE ThongBao (
    MaThongBao INT PRIMARY KEY AUTO_INCREMENT,
    TieuDe VARCHAR(255) NOT NULL,
    NoiDung TEXT NULL,
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    LoaiThongBao VARCHAR(50) NOT NULL,
    DoiTuongNhan VARCHAR(255) NOT NULL,
    NgayHetHan DATETIME NULL,
    MaNguoiTao VARCHAR(20),
    FOREIGN KEY (MaNguoiTao) REFERENCES NguoiDung(TenDangNhap)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =====================================================================
-- INDEXES CƠ BẢN
-- =====================================================================

-- Indexes cho Email để tối ưu tìm kiếm
ALTER TABLE HocSinh ADD INDEX idx_email_hs (Email);
ALTER TABLE GiaoVien ADD INDEX idx_email_gv (Email);
ALTER TABLE PhuHuynh ADD INDEX idx_email_ph (Email);

-- Indexes cho tìm kiếm theo tên
ALTER TABLE HocSinh ADD INDEX idx_hoten_hs (HoTen);
ALTER TABLE GiaoVien ADD INDEX idx_hoten_gv (HoTen);
ALTER TABLE PhuHuynh ADD INDEX idx_hoten_ph (HoTen);

-- Indexes cho các khóa ngoại quan trọng
ALTER TABLE PhanLop ADD INDEX idx_hocky_phanlop (MaHocKy);
ALTER TABLE PhanCongGiangDay ADD INDEX idx_hocky_phancong (MaHocKy);
ALTER TABLE ThoiKhoaBieu ADD INDEX idx_phancong_tkb (MaPhanCong);

SELECT 'Schema creation completed successfully' AS Status;
