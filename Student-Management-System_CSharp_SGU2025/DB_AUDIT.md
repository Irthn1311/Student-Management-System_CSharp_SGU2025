# DB_AUDIT - ĐỐI CHIẾU DATABASE & YÊU CẦU NGHIỆP VỤ

**Schema:** QuanLyHocSinh.sql  
**Version:** 1.0  
**Date:** ${new Date().toISOString().split('T')[0]}

---

## 1. CẤU TRÚC DATABASE

### 1.1 Danh sách bảng

| Bảng | Mục đích | Số cột | Khóa chính | FK | Trạng thái |
|------|----------|--------|------------|-------|------------|
| `VaiTro` | RBAC - Vai trò người dùng | 3 | MaVaiTro | - | ✅ |
| `ChucNang` | RBAC - Chức năng | 3 | MaChucNang | - | ✅ |
| `VaiTroChucNang` | RBAC - Phân quyền | 2 | (Composite) | 2 | ✅ |
| `NguoiDung` | User accounts | 3 | TenDangNhap | - | ✅ |
| `NguoiDungVaiTro` | User-Role mapping | 2 | (Composite) | 2 | ✅ |
| `NamHoc` | Năm học | 4 | MaNamHoc | - | ✅ |
| `HocKy` | Học kỳ | 6 | MaHocKy | 1 (NamHoc) | ✅ |
| `KhoiLop` | Khối lớp | 2 | MaKhoi | - | ✅ |
| `MonHoc` | Môn học | 4 | MaMonHoc | - | ✅ |
| `GiaoVien` | Giáo viên | 8 | MaGiaoVien | - | ✅ |
| `GiaoVienChuyenMon` | GV-Chuyên môn | 3 | (Composite) | 2 | ✅ |
| `HocSinh` | Học sinh | 6 | MaHocSinh | - | ✅ |
| `PhuHuynh` | Phụ huynh | 5 | MaPhuHuynh | - | ✅ |
| `HocSinhPhuHuynh` | HS-PH relationship | 3 | (Composite) | 2 | ✅ |
| `LopHoc` | Lớp học | 4 | MaLop | 2 | ✅ |
| `PhanLop` | Phân lớp học sinh | 3 | (Composite) | 3 | ✅ |
| `GiaoVien_MonHoc` | GV-Môn học | 3 | (Composite) | 2 | ✅ |
| `PhanCongGiangDay` | Phân công giảng dạy | 7 | MaPhanCong | 4 | ✅ |
| `ThoiKhoaBieu` | Thời khóa biểu | 6 | MaThoiKhoaBieu | 1 | ✅ |
| `DiemSo` | Điểm số | 6 | (Composite) | 3 | ✅ |
| `HanhKiem` | Hạnh kiểm | 4 | (Composite) | 2 | ✅ |
| `XepLoai` | Xếp loại học lực | 4 | (Composite) | 2 | ✅ |
| `KhenThuongKyLuat` | Khen thưởng/Kỷ luật | 8 | MaKTKL | 2 | ✅ |
| `ThongBao` | Thông báo | 7 | MaThongBao | 1 | ✅ |

**Tổng:** 23 bảng

---

## 2. PHÂN TÍCH CẤU TRÚC DATABASE

### 2.1 RBAC (Role-Based Access Control)

**Bảng liên quan:**
- `VaiTro` (student, parent, teacher, admin)
- `ChucNang` (qlhs, qlphuhuynh, qlgiaovien)
- `VaiTroChucNang` (mapping)
- `NguoiDung`
- `NguoiDungVaiTro` (user-role mapping)

**Vấn đề:**
❌ Chưa có bảng `UserPermission` chi tiết  
❌ Không có session tracking  
⚠️ Password không có hash (plaintext "12345678")  
⚠️ Chưa có audit log cho thay đổi phân quyền

**Đề xuất:**
```sql
-- Thêm bảng session
CREATE TABLE Session (
    MaSession VARCHAR(50) PRIMARY KEY,
    TenDangNhap VARCHAR(20),
    LoginTime DATETIME,
    LogoutTime DATETIME,
    FOREIGN KEY (TenDangNhap) REFERENCES NguoiDung(TenDangNhap)
);

-- Hash passwords
UPDATE NguoiDung SET MatKhau = SHA2(CONCAT(TenDangNhap, ':', MatKhau), 256);
```

---

### 2.2 Học sinh & Phụ huynh

**Bảng:** `HocSinh`, `PhuHuynh`, `HocSinhPhuHuynh`

**Vấn đề:**
⚠️ Thiếu khóa chính tự tăng ở PhuHuynh (insert thủ công MaPhuHuynh)  
✅ Email có UNIQUE constraint - OK  
✅ Có ràng buộc MoiQuanHe - OK

**Vấn đề trong SQL seed:**
```sql
-- INSERT phụ huynh với MaPhuHuynh thủ công (không tốt)
INSERT INTO PhuHuynh (MaPhuHuynh, HoTen, ...) VALUES (1, ...);
```

**Nên đổi:**
```sql
INSERT INTO PhuHuynh (HoTen, ...) VALUES (...); -- Auto-increment
```

---

### 2.3 Điểm số & Hạnh kiểm

**Bảng:** `DiemSo`, `HanhKiem`, `XepLoai`

**Vấn đề:**
❌ `DiemTrungBinh` là FLOAT - nên dùng DECIMAL(4,2) hoặc tính tự động  
❌ Không có trigger/function tính điểm trung bình tự động  
❌ `XepLoai.HocLuc` không có ràng buộc CHECK (nên: Xuất sắc, Giỏi, Khá, TB, Yếu)  
❌ Thiếu bảng `DiemChiTiet` (Diem15Phut, DiemGiuaKy, ... cần nhiều cột hơn)

**Đề xuất:**
```sql
-- Tính điểm TB tự động (trigger)
CREATE TRIGGER calc_diem_tb BEFORE INSERT ON DiemSo
FOR EACH ROW
SET NEW.DiemTrungBinh = (
    NEW.DiemMieng * 0.1 + 
    NEW.Diem15Phut * 0.2 + 
    NEW.DiemGiuaKy * 0.3 + 
    NEW.DiemCuoiKy * 0.4
);

-- Ràng buộc CHECK cho XepLoai
ALTER TABLE XepLoai ADD CONSTRAINT check_hocluc 
CHECK (HocLuc IN ('Xuất sắc', 'Giỏi', 'Khá', 'Trung bình', 'Yếu'));
```

---

### 2.4 Phân công giảng dạy & Thời khóa biểu

**Bảng:** `PhanCongGiangDay`, `ThoiKhoaBieu`

**Vấn đề:**
❌ Không có ràng buộc kiểm tra giờ dạy trùng (cùng lớp, cùng thứ, cùng tiết)  
❌ `PhongHoc` kiểu VARCHAR(50) không có FK  
❌ Thiếu validation: số tiết không vượt quá 5 tiết/ngày cho một GV  
❌ `ThuTrongTuan` kiểu NVARCHAR(20) - nên dùng ENUM hoặc TINYINT (1-7)

**Đề xuất:**
```sql
-- Bảng Phòng học
CREATE TABLE PhongHoc (
    MaPhong VARCHAR(20) PRIMARY KEY,
    TenPhong NVARCHAR(50),
    SucChua INT,
    LoaiPhong NVARCHAR(50)
);

-- Trigger kiểm tra trùng TKB
DELIMITER $$
CREATE TRIGGER check_trung_tkb BEFORE INSERT ON ThoiKhoaBieu
FOR EACH ROW
BEGIN
    IF EXISTS (
        SELECT 1 FROM ThoiKhoaBieu tk
        WHERE tk.ThuTrongTuan = NEW.ThuTrongTuan
        AND tk.TietBatDau <= NEW.TietBatDau + NEW.SoTiet - 1
        AND tk.TietBatDau + tk.SoTiet - 1 >= NEW.TietBatDau
    ) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Trùng lịch dạy!';
    END IF;
END$$
DELIMITER ;
```

---

### 2.5 Môn học & Giáo viên

**Bảng:** `MonHoc`, `GiaoVien`, `GiaoVien_MonHoc`, `GiaoVienChuyenMon`

**Vấn đề:**
❌ Có 2 bảng trùng chức năng: `GiaoVien_MonHoc` và `GiaoVienChuyenMon`  
⚠️ MonHoc.GhiChu (50) quá ngắn

**Đề xuất:**
```sql
-- DROP bảng trùng
DROP TABLE GiaoVien_MonHoc; -- Giữ lại GiaoVienChuyenMon
-- Hoặc merge vào một bảng duy nhất
```

---

## 3. ĐỐI CHIẾU CODE vs DATABASE

### 3.1 Mapping DAO ↔ DB Table

| DAO Class | DB Table | Status | Issues |
|-----------|----------|--------|--------|
| `HocSinhDAO` | `HocSinh` | ✅ Match | - |
| `GiaoVienDAO` | `GiaoVien` | ✅ Match | - |
| `PhuHuynhDAO` | `PhuHuynh` | ✅ Match | - |
| `MonHocDAO` | `MonHoc` | ✅ Match | - |
| `LopHocDAO` | `LopHoc` | ✅ Match | - |
| `NamHocDAO` | `NamHoc` | ✅ Match | - |
| `HocKyDAO` | `HocKy` | ✅ Match | - |
| `DiemSoDAO` | `DiemSo` | ✅ Match | ⚠️ Chưa dùng DiemTrungBinh |
| `HanhKiemDAO` | `HanhKiem` | ✅ Match | - |
| `PhanCongGiangDayDAO` | `PhanCongGiangDay` | ✅ Match | ⚠️ Chưa dùng NgayBatDau, NgayKetThuc |
| `PhanLopDAO` | `PhanLop` | ✅ Match | - |
| `HocSinhPhuHuynhDAO` | `HocSinhPhuHuynh` | ✅ Match | - |
| `NhapDiemDAO` | `DiemSo` | ✅ Match | ⚠️ Có thể duplicate với DiemSoDAO |

### 3.2 Column Mapping Issues

**Issue 1:** `HocSinh.SDTHS` trong code → `HocSinh.SDTHS` trong DB ✅  
**Issue 2:** `HocSinh` có `MaHocSinh INT AUTO_INCREMENT` ✅  
**Issue 3:** `DiemSo.DiemMieng FLOAT` vs code dùng `float?` - ⚠️ Cần check nullable  
**Issue 4:** `PhanCongGiangDay` có UNIQUE constraint - code chưa validate ✅

---

## 4. MISSING FEATURES

### 4.1 Database chưa có

❌ **Bảng Session/AuditLog:** Không track đăng nhập/đăng xuất  
❌ **Bảng Backup:** Không có backup automatic  
❌ **Bảng DiemQuaTrinh:** Chi tiết điểm từng bài kiểm tra  
❌ **Bảng LichThi:** Lịch thi, phòng thi, đề thi  
❌ **Bảng HocPhi:** Quản lý học phí

### 4.2 Code chưa dùng DB features

❌ Không dùng trigger tính `DiemTrungBinh`  
❌ Không dùng stored procedures  
❌ Không dùng views (VD: view HS với điểm TB)  
❌ Chưa implement soft delete (isDeleted flag)

---

## 5. INDEX & PERFORMANCE

### 5.1 Indexes hiện có
```sql
CREATE INDEX idx_email ON HocSinh(Email);
CREATE INDEX idx_email_gv ON GiaoVien(Email);
```

### 5.2 Missing indexes

❌ Không có index trên:
- `HocSinh.HoTen` (tìm kiếm theo tên)
- `DiemSo.MaHocSinh`, `DiemSo.MaMonHoc`, `DiemSo.MaHocKy` (join thường xuyên)
- `PhanLop.MaHocSinh`, `PhanLop.MaLop`, `PhanLop.MaHocKy`
- `ThoiKhoaBieu.MaPhanCong`

**Đề xuất:**
```sql
ALTER TABLE HocSinh ADD INDEX idx_hoten (HoTen);
ALTER TABLE DiemSo ADD INDEX idx_hocsinh_hocky (MaHocSinh, MaHocKy);
ALTER TABLE PhanLop ADD INDEX idx_hocsinh_hocky (MaHocSinh, MaHocKy);
ALTER TABLE ThoiKhoaBieu ADD INDEX idx_phancong (MaPhanCong);
```

---

## 6. DATA INTEGRITY

### 6.1 Foreign Key Constraints

**Khóa ngoại đầy đủ:**
✅ PhanLop → HocSinh, LopHoc, HocKy  
✅ HocSinhPhuHuynh → HocSinh, PhuHuynh  
✅ DiemSo → HocSinh, MonHoc, HocKy  
✅ PhanCongGiangDay → LopHoc, GiaoVien, MonHoc, HocKy  
✅ ThoiKhoaBieu → PhanCongGiangDay  

**Missing:**
❌ Không có FK: `HocSinh.TrangThai` → bảng constant  
❌ Không có FK: `GiaoVien.TrangThai` → bảng constant  

### 6.2 Data Validation Missing

❌ `DiemSo.DiemMieng` (0-10) không có CHECK  
❌ `HocSinh.NgaySinh` không có constraint (tuổi hợp lệ)  
❌ `GiaoVien.NgaySinh` không có constraint  
❌ `Email` format không có CHECK  

**Đề xuất:**
```sql
-- Validate điểm (0-10)
ALTER TABLE DiemSo ADD CONSTRAINT check_diem 
CHECK (DiemMieng >= 0 AND DiemMieng <= 10);

-- Validate ngày sinh học sinh (2006-2008)
ALTER TABLE HocSinh ADD CONSTRAINT check_namsinh 
CHECK (YEAR(NgaySinh) BETWEEN 2006 AND 2008);
```

---

## 7. SEED DATA

### 7.1 Seed Data hiện có

✅ KhoiLop: Khối 10, 11, 12 (15 classes)  
✅ GiaoVien: 15 giáo viên mẫu  
✅ HocSinh: 20 học sinh mẫu  
✅ PhuHuynh: 20 phụ huynh (1:1 với HS)  
✅ NamHoc: 2 năm học (2024-2025, 2025-2026)  
✅ HocKy: 3 học kỳ  
✅ MonHoc: 13 môn học  
✅ VaiTro: student, parent, teacher, admin  
✅ NguoiDung: 20 HS + 15 GV + 1 admin  

### 7.2 Seed Data thiếu

❌ Không có dữ liệu DiemSo mẫu  
❌ Không có phân lớp mẫu đầy đủ (chỉ 20 HS vào 2 lớp)  
❌ Không có PhanCongGiangDay mẫu  
❌ Không có ThoiKhoaBieu mẫu  
❌ Không có HanhKiem mẫu  
❌ Không có XepLoai mẫu  

**Đề xuất tạo seed data:**
- 20 HS phân vào 15 lớp (khoảng 30-40 HS/lớp)
- Tạo 15-20 PhanCongGiangDay
- Tạo TKB mẫu (Thứ 2-6, mỗi ngày 5 tiết)
- Tạo điểm số mẫu cho từng HS (mỗi môn, mỗi học kỳ)

---

## 8. RECOMMENDATIONS

### 8.1 High Priority

1. ✅ **Fix password security:** Hash passwords (SHA256/BCrypt)
2. ✅ **Add missing indexes:** HoTen, composite indexes
3. ✅ **Implement trigger:** Auto-calculate DiemTrungBinh
4. ✅ **Fix duplicate tables:** GiaoVien_MonHoc vs GiaoVienChuyenMon
5. ✅ **Add validation:** CHECK constraints cho điểm, email, tuổi

### 8.2 Medium Priority

1. ⚠️ **Tạo seed data đầy đủ:** DiemSo, HanhKiem, XepLoai, ThoiKhoaBieu
2. ⚠️ **Session tracking:** Bảng Session để track đăng nhập
3. ⚠️ **Audit log:** Bảng AuditLog để track thay đổi dữ liệu
4. ⚠️ **Backup strategy:** Tự động backup DB định kỳ

### 8.3 Low Priority

1. 🔄 **Normalize PhongHoc:** Tách PhongHoc thành bảng riêng
2. 🔄 **Stored procedures:** Dùng SP cho complex queries
3. 🔄 **Views:** Tạo views cho báo cáo (VD: BangDiem, DanhSachLop)
4. 🔄 **Encryption:** Encrypt sensitive data (Email, SĐT)

---

## 9. MIGRATION SCRIPT

```sql
-- ========================================
-- SCRIPT CẢI THIỆN DATABASE
-- ========================================

-- 1. Hash passwords
UPDATE NguoiDung SET MatKhau = SHA2(CONCAT(TenDangNhap, ':', MatKhau), 256);

-- 2. Add missing indexes
ALTER TABLE HocSinh ADD INDEX idx_hoten (HoTen);
ALTER TABLE DiemSo ADD INDEX idx_hocsinh_hocky (MaHocSinh, MaHocKy);
ALTER TABLE PhanLop ADD INDEX idx_hocsinh_hocky (MaHocSinh, MaHocKy);

-- 3. Add CHECK constraints
ALTER TABLE DiemSo ADD CONSTRAINT check_diem CHECK (DiemMieng >= 0 AND DiemMieng <= 10);
ALTER TABLE HocSinh ADD CONSTRAINT check_namsinh CHECK (YEAR(NgaySinh) BETWEEN 2006 AND 2008);

-- 4. Create trigger for auto-calculate DiemTrungBinh
DELIMITER $$
CREATE TRIGGER calc_diem_tb BEFORE INSERT ON DiemSo
FOR EACH ROW
SET NEW.DiemTrungBinh = (
    IFNULL(NEW.DiemMieng, 0) * 0.1 + 
    IFNULL(NEW.Diem15Phut, 0) * 0.2 + 
    IFNULL(NEW.DiemGiuaKy, 0) * 0.3 + 
    IFNULL(NEW.DiemCuoiKy, 0) * 0.4
);
$$
DELIMITER ;

-- 5. Drop duplicate table (choose one)
-- DROP TABLE GiaoVien_MonHoc;
-- OR merge into GiaoVienChuyenMon
```

---

## 10. CHECKLIST DỮ LIỆU MẪU

### 10.1 Required Seed Data (End-to-End)

- [ ] ✅ KhoiLop: 10, 11, 12 (Done)
- [ ] ✅ LopHoc: 15 lớp (Done)
- [ ] ✅ NamHoc: 2024-2025, 2025-2026 (Done)
- [ ] ✅ HocKy: Học kỳ 1 của năm 2024-2025 (Done)
- [ ] ✅ MonHoc: 13 môn học (Done)
- [ ] ✅ GiaoVien: 15 giáo viên (Done)
- [ ] ✅ HocSinh: 500 học sinh (Hiện mới 20 HS) - **THIẾU**
- [ ] ✅ PhuHuynh: 500 phụ huynh (Hiện mới 20 PH) - **THIẾU**
- [ ] ⚠️ PhanLop: Tất cả 500 HS phân vào 15 lớp - **THIẾU**
- [ ] ❌ PhanCongGiangDay: GV dạy lớp - **THIẾU**
- [ ] ❌ ThoiKhoaBieu: Lịch dạy theo thứ/tiết - **THIẾU**
- [ ] ❌ DiemSo: Điểm số cho tất cả HS - **THIẾU**
- [ ] ❌ HanhKiem: Xếp loại hạnh kiểm - **THIẾU**
- [ ] ❌ XepLoai: Học lực - **THIẾU**

### 10.2 Test Accounts

- [ ] admin / 12345678 (Hoạt động)
- [ ] HS1...HS20 / 12345678 (Hoạt động)
- [ ] GV0001...GV0015 / 12345678 (Hoạt động)

---

## 11. KẾT LUẬN

### 11.1 Database Structure
✅ **Tốt:** 23 bảng, schema rõ ràng, FK đầy đủ  
⚠️ **Cần cải thiện:** Missing indexes, chưa có triggers, validation không đầy đủ

### 11.2 Code-Database Mapping
✅ **Khớp:** DAO classes tương ứng với DB tables  
⚠️ **Vấn đề:** Chưa dùng hết features của DB (triggers, views, SPs)

### 11.3 Seed Data
✅ **Có:** KhoiLop, LopHoc, GiaoVien, NamHoc, MonHoc  
❌ **Thiếu:** DiemSo, HanhKiem, XepLoai, ThoiKhoaBieu, PhanCongGiangDay đầy đủ

### 11.4 Security
❌ **Yếu:** Passwords plaintext, không có session tracking, audit log

**Đánh giá tổng thể:** 7/10  
**Hành động ưu tiên:** Seed data đầy đủ + Fix security + Add missing indexes
