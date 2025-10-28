# SMOKE TEST - KIỂM TRA NHANH HỆ THỐNG

**Project:** Student Management System CSharp SGU2025  
**Date:** ${new Date().toISOString().split('T')[0]}

---

## 1. MỤC ĐÍCH

Smoke testing nhằm kiểm tra nhanh các chức năng cốt lõi của hệ thống trước khi release.

**Mục tiêu:** Đảm bảo hệ thống có thể chạy được end-to-end không có crash.

---

## 2. MÔI TRƯỜNG CÀI ĐẶT

### 2.1 Requirements

- **OS:** Windows 10/11
- **.NET Framework:** 4.7.2
- **MySQL:** 8.0+
- **Visual Studio:** 2019+ (for build)

### 2.2 Setup Steps

1. **Install MySQL Server**
   ```bash
   # Download from https://dev.mysql.com/downloads/installer/
   # Install with default settings
   # Set root password (can be empty for local dev)
   ```

2. **Import Database**
   ```bash
   # Run MySQL Workbench or command line
   mysql -u root -p < ConnectDatabase/QuanLyHocSinh.sql
   ```

3. **Verify Connection**
   - Open `ConnectDatabase/ConnectionDatabase.cs`
   - Update connection string if needed
   - Build solution
   - Run application

---

## 3. KỊCH BẢN KIỂM TRA
### 3.0 Auto Phân công → Review → Chấp nhận → Sinh TKB

**Steps:**
1. Mở menu “Auto Phân công (Mới)” → UC preview.
2. Nhấn Regenerate → xem đề xuất.
3. Sửa tay 3 dòng (nếu cần), nhấn Lưu tạm.
4. Nhấn Chấp nhận → dữ liệu vào `PhanCongGiangDay`.
5. Mở “Thời khóa biểu” → Sắp xếp tự động → Lưu thời khóa biểu.

**Expected:**
- ✅ Không vượt tải GV theo mặc định; có cảnh báo nếu thiếu chuyên môn.
- ✅ Chốt phân công thành công và sinh TKB hiển thị.


### 3.1 Login Test

**Mục đích:** Kiểm tra authentication

**Steps:**
1. Mở ứng dụng
2. Nhập username: `admin`, password: `12345678`
3. Click "Đăng nhập"
4. Kỳ vọng: Vào được Dashboard

**Expected Result:**
- ✅ Hiển thị Dashboard
- ✅ Menu sidebar hiển thị đầy đủ

**Actual Result:**
- ⚠️ [Ghi kết quả]

**Notes:**
- [Ghi chú]

---

### 3.2 Dashboard Test

**Mục đích:** Kiểm tra tổng quan

**Steps:**
1. Vào Dashboard (tự động sau khi login)
2. Kiểm tra các stat cards
3. Kiểm tra recent activities

**Expected Result:**
- ✅ Hiển thị số học sinh, giáo viên, lớp học
- ✅ Thống kê học sinh (Tổng, Nam, Nữ, Đang học)

**Actual Result:**
- ⚠️ [Ghi kết quả]

**Notes:**
- ⚠️ Recent activities có thể chưa có data

---

### 3.3 Student Management Test

**Mục đích:** CRUD học sinh

**Steps:**
1. Click "Học sinh" trong sidebar
2. Kiểm tra danh sách hiển thị
3. Click "Thêm học sinh"
4. Nhập thông tin:
   - Họ tên: Nguyễn Văn A
   - Ngày sinh: 01/01/2008
   - Giới tính: Nam
   - Email: nguyenvana@gmail.com
   - SĐT: 0123456789
   - Trạng thái: Đang học
5. Click "Lưu"
6. Kiểm tra học sinh mới xuất hiện trong danh sách
7. Click "Sửa" trên 1 học sinh
8. Thay đổi thông tin
9. Click "Lưu"
10. Kiểm tra thông tin đã update

**Expected Result:**
- ✅ Danh sách hiển thị
- ✅ Thêm thành công
- ✅ Sửa thành công
- ✅ Validation hoạt động (tuổi 16-18, email format, SĐT)

**Actual Result:**
- ⚠️ [Ghi kết quả]

**Notes:**
- ⚠️ Validation chưa hoàn chỉnh

---

### 3.4 Class Management Test

**Mục đích:** CRUD lớp học

**Steps:**
1. Click "Lớp & Khối" trong sidebar
2. Kiểm tra danh sách hiển thị
3. Click "Thêm lớp"
4. Nhập thông tin:
   - Tên lớp: 10A6
   - Khối: 10
   - GVCN: GV001
5. Click "Lưu"
6. Kiểm tra lớp mới xuất hiện

**Expected Result:**
- ✅ Thêm thành công

**Actual Result:**
- ⚠️ [Ghi kết quả]

**Notes:**
- [Ghi chú]

---

### 3.5 Grade Management Test

**Mục đích:** Nhập và xem điểm

**Steps:**
1. Click "Điểm số" trong sidebar
2. Chọn lớp, môn học, học kỳ
3. Click "Nhập điểm"
4. Nhập điểm cho học sinh:
   - Miệng: 8
   - 15 phút: 7
   - Giữa kỳ: 9
   - Cuối kỳ: 8
5. Click "Lưu"
6. Kiểm tra điểm TB được tính tự động

**Expected Result:**
- ✅ Nhập điểm thành công
- ✅ Điểm TB được tính (8.0 × 0.1 + 7.0 × 0.2 + 9.0 × 0.3 + 8.0 × 0.4 = 8.0)
- ⚠️ **CHƯA CÓ** - cần implement

**Actual Result:**
- ❌ Chưa tính ĐTB tự động

**Notes:**
- ⚠️ Cần fix: DiemTrungBinh không tự tính

---

### 3.6 Conduct Test

**Mục đích:** Nhập hạnh kiểm

**Steps:**
1. Click "Hạnh kiểm" trong sidebar
2. Chọn học sinh, học kỳ
3. Nhập hạnh kiểm
4. Click "Lưu"
5. Kiểm tra xếp loại

**Expected Result:**
- ✅ Lưu thành công
- ⚠️ **CHƯA CÓ** - xếp loại tự động

**Actual Result:**
- ❌ Chưa có xếp loại tự động

**Notes:**
- ⚠️ Cần implement xếp loại dựa trên ĐTB

---

### 3.7 Ranking Test

**Mục đích:** Xem xếp loại học lực

**Steps:**
1. Click "Xếp loại" trong sidebar
2. Chọn học kỳ
3. Kiểm tra danh sách xếp loại

**Expected Result:**
- ✅ Hiển thị danh sách
- ⚠️ **CHƯA CÓ** - tính học lực tự động

**Actual Result:**
- ❌ Chưa tính học lực tự động

**Notes:**
- ⚠️ Cần implement tính HocLuc dựa trên ĐTB

---

### 3.8 Schedule (TKB) Test

**Mục đích:** Sinh và xem thời khóa biểu

**Steps:**
1. Import DB: `mysql -u root -p < ConnectDatabase/QuanLyHocSinh.sql` (đảm bảo có bảng `TKB_Temp`).
2. Nhập phân công tối thiểu (bảng `PhanCongGiangDay`), hoặc dùng form "Phân công giảng dạy" để thêm.
3. Mở tab "Thời khóa biểu".
4. Nhấn nút "Sắp xếp tự động" → hệ thống sinh lịch và lưu vào `TKB_Temp`.
5. Kiểm tra lưới hiển thị TKB từ temp.
6. Nhấn "Lưu thời khóa biểu" để Accept → copy sang bảng chính `ThoiKhoaBieu`.
7. (Tuỳ chọn) Nhấn "Xóa" để Rollback (xóa temp).

**Expected Result:**
- ✅ Sinh lịch trong ≤ 3 phút với dữ liệu demo
- ✅ Hard violations = 0 (không trùng GV/Lớp tại cùng Thu/Tiet)
- ✅ Hiển thị TKB theo lớp
- ✅ Accept thành công và dữ liệu có trong bảng `ThoiKhoaBieu`

**Actual Result:**
- [Ghi kết quả]

**Notes:**
- Nếu hard violations > 0, xem lại dữ liệu phân công hoặc giảm số tiết/tuần.

---

### 3.9 Teaching Assignment Test

**Mục đích:** Phân công giảng dạy

**Steps:**
1. Click "Phân công giảng dạy" trong sidebar
2. Click "Thêm phân công"
3. Chọn:
   - Lớp: 10A1
   - GV: GV001
   - Môn: Toán
   - Học kỳ: HK 1
4. Click "Lưu"
5. Kiểm tra phân công xuất hiện

**Expected Result:**
- ✅ Thêm thành công
- ⚠️ **CHƯA CÓ** - check conflict

**Actual Result:**
- ⚠️ Chưa check trùng lịch

**Notes:**
- ⚠️ Cần implement conflict checking

---

### 3.10 Reports Test

**Mục đích:** Xuất báo cáo

**Steps:**
1. Click "Báo cáo" trong sidebar
2. Chọn loại báo cáo: "Bảng điểm"
3. Chọn lớp, học kỳ
4. Click "Xuất Excel"
5. Kiểm tra file Excel được tạo

**Expected Result:**
- ✅ File Excel được tạo
- ✅ Dữ liệu chính xác
- ⚠️ **CHƯA CÓ** - PDF export

**Actual Result:**
- ✅ Export Excel hoạt động
- ❌ Chưa có PDF export

**Notes:**
- ⚠️ Cần implement PDF export (iTextSharp)

---

### 3.11 RBAC Test

**Mục đích:** Kiểm tra phân quyền

**Steps:**
1. Đăng xuất
2. Đăng nhập với username: `HS1`, password: `12345678`
3. Kiểm tra menu hiển thị
4. Kiểm tra các chức năng HS có thể truy cập

**Expected Result:**
- ✅ Menu chỉ hiển thị chức năng HS được phép
- ✅ Không thấy các chức năng admin

**Actual Result:**
- ⚠️ [Ghi kết quả]

**Notes:**
- ⚠️ Cần implement menu visibility by role

---

## 4. KẾT QUẢ

### 4.1 Test Summary

| Test Case | Status | Notes |
|-----------|--------|-------|
| Login | ⚠️ Pending | |
| Dashboard | ⚠️ Pending | |
| Student CRUD | ✅ Pass | Minor issues |
| Class CRUD | ⚠️ Pending | |
| Grade Input | ⚠️ Fail | No auto-calculate |
| Conduct Input | ⚠️ Fail | No auto-classify |
| Ranking View | ⚠️ Fail | No calculation |
| Schedule View | ⚠️ Pending | Need data |
| Teaching Assignment | ⚠️ Fail | No conflict check |
| Reports | ⚠️ Partial | No PDF |
| RBAC | ⚠️ Fail | Not implemented |

### 4.2 Pass Rate

**Total Tests:** 11  
**Passed:** 1  
**Failed:** 5  
**Pending:** 5  

**Pass Rate:** 9%

### 4.3 Critical Issues

1. ❌ **DiemTrungBinh không tự tính** - High Priority
2. ❌ **Xếp loại học lực chưa có** - High Priority
3. ❌ **Xếp loại hạnh kiểm chưa có** - High Priority
4. ❌ **Conflict checking chưa có** - Medium Priority
5. ❌ **RBAC chưa implement đầy đủ** - Medium Priority
6. ❌ **PDF export chưa có** - Low Priority

---

## 5. HƯỚNG DẪN CHẠY

### 5.1 Quick Start

```bash
# 1. Setup database
mysql -u root -p < ConnectDatabase/QuanLyHocSinh.sql

# 2. Update connection string (if needed)
# Edit: ConnectDatabase/ConnectionDatabase.cs
# connectionString = "Server=localhost;Database=QuanLyHocSinh;Uid=root;Pwd=;"

# 3. Build solution
# Open in Visual Studio, press F5

# 4. Run tests
# Follow test cases above
```

### 5.2 Test Accounts

| Role | Username | Password |
|------|----------|----------|
| Admin | `admin` | `12345678` |
| Student | `HS1` | `12345678` |
| Teacher | `GV0001` | `12345678` |

---

## 6. GHI CHÚ

### 6.1 Known Issues

- ⚠️ Điểm TB không tự tính
- ⚠️ Xếp loại chưa có
- ⚠️ Thiếu seed data (TKB, DiemSo)
- ⚠️ RBAC chưa hoàn chỉnh

### 6.2 Next Steps

1. Fix critical issues (DiemTrungBinh, HocLuc, HanhKiem)
2. Create comprehensive seed data
3. Complete RBAC
4. Re-test
5. Pass smoke tests

---

## 7. CHECKLIST

### 7.1 Pre-Release Checklist

- [ ] All smoke tests passing
- [ ] No console errors
- [ ] All CRUD operations working
- [ ] Export functions working
- [ ] RBAC working
- [ ] No data loss
- [ ] Performance acceptable

### 7.2 Post-Release Checklist

- [ ] User training complete
- [ ] Documentation ready
- [ ] Support ready
- [ ] Backup strategy in place
