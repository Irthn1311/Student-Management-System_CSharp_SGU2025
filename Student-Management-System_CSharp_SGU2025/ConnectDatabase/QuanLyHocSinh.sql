DROP DATABASE IF EXISTS QuanLyHocSinh;

CREATE DATABASE QuanLyHocSinh CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE QuanLyHocSinh;

CREATE TABLE NguoiDung (
    Ma_Nguoi_Dung INT PRIMARY KEY AUTO_INCREMENT,
    Ho_Ten VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE,
    So_Dien_Thoai VARCHAR(15),
    Ten_Dang_Nhap VARCHAR(50) NOT NULL UNIQUE,
    Mat_Khau VARCHAR(255) NOT NULL, -- Nên lưu mật khẩu đã được mã hóa
    Vai_Tro VARCHAR(50) NOT NULL, -- Ví dụ: 'Admin', 'Giáo Viên', 'Phụ Huynh'
    Trang_Thai VARCHAR(50) DEFAULT 'Hoạt động'
);

CREATE TABLE NamHoc (
    Ma_Nam_Hoc INT PRIMARY KEY AUTO_INCREMENT,
    Ten_Nam_Hoc VARCHAR(50) NOT NULL, -- Ví dụ: '2024-2025'
    Ngay_Bat_Dau DATE,
    Ngay_Ket_Thuc DATE
);

CREATE TABLE HocKy (
    Ma_Hoc_Ky INT PRIMARY KEY AUTO_INCREMENT,
    Ten_Hoc_Ky VARCHAR(50) NOT NULL, -- Ví dụ: 'Học Kỳ 1', 'Học Kỳ 2'
    Ma_Nam_Hoc INT,
    FOREIGN KEY (Ma_Nam_Hoc) REFERENCES NamHoc(Ma_Nam_Hoc)
);

CREATE TABLE KhoiLop (
    Ma_Khoi INT PRIMARY KEY AUTO_INCREMENT,
    Ten_Khoi VARCHAR(50) NOT NULL -- Ví dụ: 'Khối 10'
);

CREATE TABLE GiaoVien (
    Ma_Giao_Vien INT PRIMARY KEY AUTO_INCREMENT,
    Ho_Ten VARCHAR(100) NOT NULL,
    Ngay_Sinh DATE,
    Gioi_Tinh VARCHAR(10),
    Dia_Chi VARCHAR(255),
    So_Dien_Thoai VARCHAR(15),
    Email VARCHAR(100) UNIQUE
);

CREATE TABLE LopHoc (
    Ma_Lop INT PRIMARY KEY AUTO_INCREMENT,
    Ten_Lop VARCHAR(50) NOT NULL,
    Ma_Khoi INT,
    Ma_Giao_Vien_Chu_Nhiem INT,
    FOREIGN KEY (Ma_Khoi) REFERENCES KhoiLop(Ma_Khoi),
    FOREIGN KEY (Ma_Giao_Vien_Chu_Nhiem) REFERENCES GiaoVien(Ma_Giao_Vien)
);

CREATE TABLE HocSinh (
    Ma_Hoc_Sinh INT PRIMARY KEY AUTO_INCREMENT,
    Ho_Ten VARCHAR(100) NOT NULL,
    Ngay_Sinh DATE,
    Gioi_Tinh VARCHAR(10),
    SDT_HS VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    Trang_Thai VARCHAR(50) DEFAULT 'Đang học'
);

CREATE TABLE PhuHuynh (
    Ma_Phu_Huynh INT PRIMARY KEY AUTO_INCREMENT,
    Ho_Ten VARCHAR(100) NOT NULL,
    So_Dien_Thoai VARCHAR(15),
    Email VARCHAR(100) UNIQUE,
    Dia_Chi VARCHAR(255)
);

CREATE TABLE MonHoc (
    Ma_Mon_Hoc INT PRIMARY KEY AUTO_INCREMENT,
    Ten_Mon_Hoc VARCHAR(100) NOT NULL,
    So_Tiet INT
);

CREATE TABLE HocSinh_PhuHuynh (
    Ma_Hoc_Sinh INT,
    Ma_Phu_Huynh INT,
    Moi_Quan_He VARCHAR(50), -- Ví dụ: 'Bố', 'Mẹ', 'Người giám hộ'
    PRIMARY KEY (Ma_Hoc_Sinh, Ma_Phu_Huynh),
    FOREIGN KEY (Ma_Hoc_Sinh) REFERENCES HocSinh(Ma_Hoc_Sinh),
    FOREIGN KEY (Ma_Phu_Huynh) REFERENCES PhuHuynh(Ma_Phu_Huynh)
);

CREATE TABLE PhanLop (
    Ma_Hoc_Sinh INT,
    Ma_Lop INT,
    Ma_Hoc_Ky INT,
    Ma_Nam_Hoc INT,
    PRIMARY KEY (Ma_Hoc_Sinh, Ma_Lop, Ma_Hoc_Ky, Ma_Nam_Hoc),
    FOREIGN KEY (Ma_Hoc_Sinh) REFERENCES HocSinh(Ma_Hoc_Sinh),
    FOREIGN KEY (Ma_Lop) REFERENCES LopHoc(Ma_Lop),
    FOREIGN KEY (Ma_Hoc_Ky) REFERENCES HocKy(Ma_Hoc_Ky),
    FOREIGN KEY (Ma_Nam_Hoc) REFERENCES NamHoc(Ma_Nam_Hoc)
);


CREATE TABLE PhanCongGiangDay (
    Ma_Phan_Cong INT PRIMARY KEY AUTO_INCREMENT,
    Ma_Lop INT,
    Ma_Giao_Vien INT,
    Ma_Mon_Hoc INT,
    Ma_Hoc_Ky INT,
    Tu_Ngay DATE,
    Den_Ngay DATE,
    Ghi_Chu TEXT,
    UNIQUE (Ma_Lop, Ma_Giao_Vien, Ma_Mon_Hoc, Ma_Hoc_Ky), -- Đảm bảo không có phân công trùng lặp
    FOREIGN KEY (Ma_Lop) REFERENCES LopHoc(Ma_Lop),
    FOREIGN KEY (Ma_Giao_Vien) REFERENCES GiaoVien(Ma_Giao_Vien),
    FOREIGN KEY (Ma_Mon_Hoc) REFERENCES MonHoc(Ma_Mon_Hoc),
    FOREIGN KEY (Ma_Hoc_Ky) REFERENCES HocKy(Ma_Hoc_Ky)
);

CREATE TABLE ThoiKhoaBieu (
    Ma_Thoi_Khoa_Bieu INT PRIMARY KEY AUTO_INCREMENT,
    Ma_Phan_Cong INT,
    Thu_Trong_Tuan VARCHAR(20), -- Ví dụ: 'Thứ Hai', 'Thứ Ba'
    Tiet_Bat_Dau INT,
    So_Tiet INT,
    Phong_Hoc VARCHAR(50),
    FOREIGN KEY (Ma_Phan_Cong) REFERENCES PhanCongGiangDay(Ma_Phan_Cong)
);


CREATE TABLE DiemSo (
    Ma_Hoc_Sinh INT,
    Ma_Mon_Hoc INT,
    Ma_Hoc_Ky INT,
    Diem_Mieng FLOAT,
    Diem_15_Phut FLOAT,
    Diem_Giua_Ky FLOAT,
    Diem_Cuoi_Ky FLOAT,
    Diem_Trung_Binh FLOAT,
    PRIMARY KEY (Ma_Hoc_Sinh, Ma_Mon_Hoc, Ma_Hoc_Ky),
    FOREIGN KEY (Ma_Hoc_Sinh) REFERENCES HocSinh(Ma_Hoc_Sinh),
    FOREIGN KEY (Ma_Mon_Hoc) REFERENCES MonHoc(Ma_Mon_Hoc),
    FOREIGN KEY (Ma_Hoc_Ky) REFERENCES HocKy(Ma_Hoc_Ky)
);

CREATE TABLE HanhKiem (
    Ma_Hanh_Kiem INT PRIMARY KEY AUTO_INCREMENT,
    Ma_Hoc_Sinh INT,
    Ma_Hoc_Ky INT,
    Xep_Loai VARCHAR(50), -- Ví dụ: 'Tốt', 'Khá', 'Trung bình'
    Nhan_Xet TEXT,
    UNIQUE (Ma_Hoc_Sinh, Ma_Hoc_Ky),
    FOREIGN KEY (Ma_Hoc_Sinh) REFERENCES HocSinh(Ma_Hoc_Sinh),
    FOREIGN KEY (Ma_Hoc_Ky) REFERENCES HocKy(Ma_Hoc_Ky)
);

CREATE TABLE XepLoai (
    Ma_Hoc_Sinh INT,
    Ma_Hoc_Ky INT,
    Hoc_Luc VARCHAR(50), -- Ví dụ: 'Giỏi', 'Khá', 'Trung bình'
    Ghi_Chu TEXT,
    PRIMARY KEY (Ma_Hoc_Sinh, Ma_Hoc_Ky),
    FOREIGN KEY (Ma_Hoc_Sinh) REFERENCES HocSinh(Ma_Hoc_Sinh),
    FOREIGN KEY (Ma_Hoc_Ky) REFERENCES HocKy(Ma_Hoc_Ky)
);

CREATE TABLE KhenThuong_KyLuat (
    Ma_KTKL INT PRIMARY KEY AUTO_INCREMENT,
    Ma_Hoc_Sinh INT,
    Loai VARCHAR(20) NOT NULL, -- 'Khen Thưởng' hoặc 'Kỷ Luật'
    Noi_Dung TEXT NOT NULL,
    Cap_Khen_Thuong VARCHAR(100), -- Áp dụng cho khen thưởng
    Muc_Xu_Ly VARCHAR(100), -- Áp dụng cho kỷ luật
    Ngay_Ap_Dung DATE,
    Ma_Nguoi_Lap INT, -- Tham chiếu đến giáo viên hoặc người quản lý trong bảng NguoiDung
    Trang_Thai_Duyet VARCHAR(50) DEFAULT 'Chờ duyệt',
    FOREIGN KEY (Ma_Hoc_Sinh) REFERENCES HocSinh(Ma_Hoc_Sinh),
    FOREIGN KEY (Ma_Nguoi_Lap) REFERENCES NguoiDung(Ma_Nguoi_Dung)
);

