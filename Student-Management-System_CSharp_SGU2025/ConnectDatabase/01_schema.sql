CREATE DATABASE IF NOT EXISTS QuanLyHocSinh;
USE QuanLyHocSinh;

SET FOREIGN_KEY_CHECKS = 0;

-- Bảng tạm (không có foreign key)
DROP TABLE IF EXISTS TKB_Temp;
DROP TABLE IF EXISTS PhanCong_Temp;
DROP TABLE IF EXISTS HoSoNguoiDung;

DROP TABLE IF EXISTS VaiTroChucNangHanhDong;
DROP TABLE IF EXISTS ChucNangHanhDong;


-- Bảng có foreign key phức tạp nhất (nhiều bảng con)
DROP TABLE IF EXISTS ThoiKhoaBieu;
DROP TABLE IF EXISTS PhanCongGiangDay;
DROP TABLE IF EXISTS DiemSo;
DROP TABLE IF EXISTS HanhKiem;
DROP TABLE IF EXISTS XepLoai;
DROP TABLE IF EXISTS KhenThuongKyLuat;
DROP TABLE IF EXISTS ThongBao;
DROP TABLE IF EXISTS YeuCauChuyenLop;
DROP TABLE IF EXISTS PhanLop;
DROP TABLE IF EXISTS HocSinhPhuHuynh;

-- Bảng trung gian
DROP TABLE IF EXISTS LopHoc;
DROP TABLE IF EXISTS HocKy;

-- Bảng có foreign key đến NguoiDung hoặc VaiTro
DROP TABLE IF EXISTS NguoiDungVaiTro;
DROP TABLE IF EXISTS VaiTroChucNang;
DROP TABLE IF EXISTS HocSinh;

-- Bảng cha (không có foreign key hoặc chỉ có foreign key đến bảng đã drop)
DROP TABLE IF EXISTS PhuHuynh;
DROP TABLE IF EXISTS GiaoVien;
DROP TABLE IF EXISTS MonHoc;
DROP TABLE IF EXISTS KhoiLop;
DROP TABLE IF EXISTS NamHoc;
DROP TABLE IF EXISTS NguoiDung;
DROP TABLE IF EXISTS ChucNang;
DROP TABLE IF EXISTS VaiTro;

SET FOREIGN_KEY_CHECKS = 1;


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

-- Các chức năng phân quyền linh động và dữ liệu cần thêm vào

CREATE TABLE ChucNangHanhDong (
    MaChucNang VARCHAR(50),
    HanhDong VARCHAR(20),   -- 'create', 'read', 'update', 'delete'
    PRIMARY KEY (MaChucNang, HanhDong),
    FOREIGN KEY (MaChucNang) REFERENCES ChucNang(MaChucNang)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE VaiTroChucNangHanhDong (
    MaVaiTro VARCHAR(10),
    MaChucNang VARCHAR(50),
    HanhDong VARCHAR(20),
    PRIMARY KEY (MaVaiTro, MaChucNang, HanhDong),
    FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro),
    FOREIGN KEY (MaChucNang) REFERENCES ChucNang(MaChucNang)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE HoSoNguoiDung (
    MaHoSo INT PRIMARY KEY AUTO_INCREMENT,
    TenDangNhap VARCHAR(20) UNIQUE,   -- liên kết với NguoiDung
    HoTen NVARCHAR(100),
    Email VARCHAR(100),
    SoDienThoai VARCHAR(20),
    NgaySinh DATE,
    GioiTinh VARCHAR(10),
    DiaChi NVARCHAR(255),
    LoaiDoiTuong VARCHAR(20) -- 'hocsinh', 'phuhuynh', 'giaovien', 'nhanvien'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

ALTER TABLE NguoiDung 
ADD COLUMN LanDangNhapCuoi DATETIME NULL 
COMMENT 'Thời gian đăng nhập gần nhất';


INSERT INTO ChucNang (MaChucNang, TenChucNang, MoTa) VALUES
('qldiem', 'Quản lý điểm số', 'Quản lý việc nhập, sửa và xem điểm của học sinh'),
('qlhocsinh', 'Quản lý học sinh', 'Quản lý thông tin học sinh'),
('qllophoc', 'Quản lý lớp học', 'Quản lý thông tin lớp học'),
('qlgiaovien', 'Quản lý giáo viên', 'Quản lý thông tin giáo viên'),
('qlmonhoc', 'Quản lý môn học', 'Quản lý danh mục môn học'),
('qlphancong', 'Quản lý phân công', 'Quản lý phân công giảng dạy'),
('qltkb', 'Quản lý thời khóa biểu', 'Quản lý thời khóa biểu'),
('qlhanhkiem', 'Quản lý hạnh kiểm', 'Quản lý đánh giá hạnh kiểm'),
('qlbaocao', 'Quản lý báo cáo', 'Xem và xuất các báo cáo'),
('qltaikhoan', 'Quản lý tài khoản', 'Quản lý tài khoản người dùng'),
('qlthongbao', 'Quản lý thông báo', 'Quản lý thông báo hệ thống'),
('qlnamhoc', 'Quản lý năm học', 'Quản lý năm học và học kỳ'),
('qlcaidat', 'Quản lý cài đặt', 'Cài đặt hệ thống'),
('qldanhgia', 'Quản lý đánh giá', 'Quản lý khen thưởng và đánh giá'),
('qlxeploai', 'Quản lý xếp loại', 'Xếp loại và tổng kết học sinh'),
('qlyeucau_chuyenlop', 'Quản lý yêu cầu chuyển lớp', 'Quản lý và duyệt yêu cầu chuyển lớp từ phụ huynh/học sinh');

INSERT IGNORE INTO ChucNangHanhDong (MaChucNang, HanhDong) VALUES
('qlhocsinh', 'read'), ('qlhocsinh', 'create'), ('qlhocsinh', 'update'), ('qlhocsinh', 'delete'),
('qllophoc', 'read'), ('qllophoc', 'create'), ('qllophoc', 'update'), ('qllophoc', 'delete'),
('qlgiaovien', 'read'), ('qlgiaovien', 'create'), ('qlgiaovien', 'update'), ('qlgiaovien', 'delete'),
('qlmonhoc', 'read'), ('qlmonhoc', 'create'), ('qlmonhoc', 'update'), ('qlmonhoc', 'delete'),
('qlphancong', 'read'), ('qlphancong', 'create'), ('qlphancong', 'update'), ('qlphancong', 'delete'),
('qltkb', 'read'), ('qltkb', 'create'), ('qltkb', 'update'), ('qltkb', 'delete'),
('qlhanhkiem', 'read'), ('qlhanhkiem', 'create'), ('qlhanhkiem', 'update'), ('qlhanhkiem', 'delete'),
('qlbaocao', 'read'),
('qltaikhoan', 'read'), ('qltaikhoan', 'create'), ('qltaikhoan', 'update'), ('qltaikhoan', 'delete'),
('qlthongbao', 'read'), ('qlthongbao', 'create'), ('qlthongbao', 'update'), ('qlthongbao', 'delete'),
('qlnamhoc', 'read'), ('qlnamhoc', 'create'), ('qlnamhoc', 'update'), ('qlnamhoc', 'delete'),
('qlcaidat', 'read'), ('qlcaidat', 'update'),
('qldanhgia', 'read'), ('qldanhgia', 'create'), ('qldanhgia', 'update'), ('qldanhgia', 'delete'),
('qlxeploai', 'read'),
('qlyeucau_chuyenlop', 'read'), ('qlyeucau_chuyenlop', 'create'), ('qlyeucau_chuyenlop', 'update'), ('qlyeucau_chuyenlop', 'delete');

INSERT IGNORE INTO ChucNangHanhDong (MaChucNang, HanhDong) VALUES
('qldiem', 'read'),
('qldiem', 'create'),
('qldiem', 'update');

-- Tạo vai trò admin
INSERT IGNORE INTO VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES
('admin', 'Quản trị viên', 'Vai trò quản trị hệ thống với đầy đủ quyền hạn');

-- Tạo tài khoản admin (mật khẩu mặc định: admin - nên được thay đổi sau khi đăng nhập lần đầu)
-- Lưu ý: Mật khẩu này nên được hash bằng bcrypt hoặc phương pháp tương tự trong ứng dụng
INSERT IGNORE INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) VALUES
('admin', 'admin123', 'Hoạt động');

INSERT IGNORE INTO VaiTroChucNang (MaVaiTro, MaChucNang) VALUES 
('admin', 'qldiem'),
('admin', 'qlhocsinh'),
('admin', 'qllophoc'),
('admin', 'qlgiaovien'),
('admin', 'qlmonhoc'),
('admin', 'qlphancong'),
('admin', 'qltkb'),
('admin', 'qlhanhkiem'),
('admin', 'qlbaocao'),
('admin', 'qltaikhoan'),
('admin', 'qlthongbao'),
('admin', 'qlnamhoc'),
('admin', 'qlcaidat'),
('admin', 'qldanhgia'),
('admin', 'qlxeploai'),
('admin', 'qlyeucau_chuyenlop');

INSERT IGNORE INTO VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong) VALUES
('admin', 'qldiem', 'read'),
('admin', 'qldiem', 'create'),
('admin', 'qldiem', 'update');

INSERT INTO NguoiDungVaiTro (TenDangNhap, MaVaiTro) VALUES ('admin', 'admin')
ON DUPLICATE KEY UPDATE MaVaiTro = VALUES(MaVaiTro);

INSERT IGNORE INTO VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong)
SELECT 
    'admin' AS MaVaiTro,
    c.MaChucNang,
    h.HanhDong
FROM ChucNang c
CROSS JOIN (
    SELECT 'create' AS HanhDong
    UNION ALL SELECT 'read'
    UNION ALL SELECT 'update'
    UNION ALL SELECT 'delete'
) h
WHERE c.MaChucNang <> 'qlcaidat';

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
    MaMonChuyenMon INT NULL COMMENT 'Môn chuyên môn chính của giáo viên',
    TrangThai VARCHAR(50) DEFAULT 'Đang giảng dạy',
    FOREIGN KEY (MaMonChuyenMon) REFERENCES MonHoc(MaMonHoc)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE HocSinh (
    MaHocSinh INT PRIMARY KEY AUTO_INCREMENT,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    SDTHS VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    TrangThai VARCHAR(50) DEFAULT 'Đang học',
    TenDangNhap VARCHAR(20) UNIQUE COMMENT 'Tên đăng nhập liên kết với bảng NguoiDung',
    FOREIGN KEY (TenDangNhap) REFERENCES NguoiDung(TenDangNhap) ON DELETE SET NULL ON UPDATE CASCADE
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
    DiemThuongXuyen FLOAT,
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

CREATE TABLE YeuCauChuyenLop (
    MaYeuCau INT PRIMARY KEY AUTO_INCREMENT,
    MaHocSinh INT NOT NULL,
    MaLopHienTai INT NOT NULL,
    MaLopMongMuon INT NULL,          -- Có thể null nếu phụ huynh không chọn lớp cụ thể
    MaHocKy INT NOT NULL,
    LyDoYeuCau NVARCHAR(1000) NOT NULL,
    NgayTao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    NguoiTao VARCHAR(20) NOT NULL,   -- TenDangNhap của phụ huynh hoặc admin tạo
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Chờ duyệt',  -- 'Chờ duyệt', 'Đã duyệt', 'Từ chối'
    NgayXuLy DATETIME NULL,
    NguoiXuLy VARCHAR(20) NULL,      -- TenDangNhap của admin xử lý
    GhiChuAdmin NVARCHAR(1000) NULL, -- Ghi chú của admin khi duyệt/từ chối
    MaLopDuocDuyet INT NULL,         -- Lớp được admin duyệt (có thể khác lớp mong muốn)
    
    FOREIGN KEY (MaHocSinh) REFERENCES HocSinh(MaHocSinh) ON DELETE CASCADE,
    FOREIGN KEY (MaLopHienTai) REFERENCES LopHoc(MaLop) ON DELETE CASCADE,
    FOREIGN KEY (MaLopMongMuon) REFERENCES LopHoc(MaLop) ON DELETE SET NULL,
    FOREIGN KEY (MaLopDuocDuyet) REFERENCES LopHoc(MaLop) ON DELETE SET NULL,
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy) ON DELETE CASCADE,
    FOREIGN KEY (NguoiTao) REFERENCES NguoiDung(TenDangNhap) ON DELETE CASCADE,
    FOREIGN KEY (NguoiXuLy) REFERENCES NguoiDung(TenDangNhap) ON DELETE SET NULL,
    
    INDEX idx_hocsinh (MaHocSinh),
    INDEX idx_trangthai (TrangThai),
    INDEX idx_ngaytao (NgayTao),
    INDEX idx_nguoitao (NguoiTao)
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

-- Indexes cho YeuCauChuyenLop (các indexes cơ bản đã được tạo trong CREATE TABLE)
-- Các indexes nâng cao sẽ được tạo trong file 02_unique_indexes.sql

SELECT 'Schema creation completed successfully' AS Status;
