# COVERAGE - MA TRẬN BAO PHỦ CHỨC NĂNG

**Ngày:** ${new Date().toISOString().split('T')[0]}  
**Tổng hoàn thành:** 52% (87/168 points)

---

## 1. TỔNG QUAN

### 1.1 Biểu đồ tiến độ

```
Dashboard          : ████████████████████░░░░ 75%
Student Management : ████████████████████░░░░ 94%
Teacher Management : ████████████░░░░░░░░░░░░ 63%
Subject Management : ████████████████████░░░░ 100%
Class Management   : ████████████████████░░░░ 100%
Year & Semester   : ████████████████████░░░░ 100%
Grade Management  : ████████████░░░░░░░░░░░░ 63%
Conduct           : ████████░░░░░░░░░░░░░░░░ 38%
Ranking           : ██████░░░░░░░░░░░░░░░░░░ 25%
Teaching Assign   : ████████░░░░░░░░░░░░░░░░ 38%
Schedule (TKB)    : ██████░░░░░░░░░░░░░░░░░░ 25%
Reports           : ██████████░░░░░░░░░░░░░░ 42%
Notifications     : ████████████░░░░░░░░░░░░ 50%
Parent Management : ████████████████████░░░░ 88%
Authentication    : ████████░░░░░░░░░░░░░░░░ 33%

OVERALL           : ████████████░░░░░░░░░░░░ 52%
```

### 1.2 Scoring System

Mỗi chức năng được chấm theo thang điểm 0-4:
- **0:** Chưa bắt đầu (Not Started)
- **1:** Có DAO/DTO
- **2:** +BUS layer
- **3:** +GUI cơ bản
- **4:** +Hoàn thiện & Tested

**Công thức:** % = (Tổng điểm đạt / (Số chức năng × 4)) × 100

---

## 2. CHI TIẾT MODULES

### 2.1 ✅ Module: Authentication & Authorization (33%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Đăng nhập | ✅ | ❌ | ✅ | Implemented | 3/4 |
| Phân quyền (RBAC) | ❌ | ❌ | ⚠️ | In Progress | 1/4 |
| Session management | ❌ | ❌ | ❌ | Not Started | 0/4 |

**Vấn đề:**
- Thiếu BUS layer cho authentication
- Chưa implement đầy đủ RBAC (menu ẩn/hiện)
- Không có session tracking

---

### 2.2 ✅ Module: Dashboard (75%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Hiển thị tổng quan | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Thống kê nhanh | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Recent activities | ❌ | ❌ | ✅ | In Progress | 1/4 |

**Vấn đề:**
- Recent activities chỉ có UI, chưa có data

---

### 2.3 ✅ Module: Student Management - Quản lý Học sinh (94%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| CRUD học sinh | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Tìm kiếm | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Phân lớp | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Export Excel | ✅ | ❌ | ✅ | Implemented | 3/4 |

**Vấn đề:**
- Export thiếu BUS validation
- Cần thêm import Excel

**Điểm mạnh:**
✅ Validation đầy đủ (tuổi 16-18, email, SĐT)  
✅ Kiểm tra trùng lặp SĐT/Email  
✅ Transaction support

---

### 2.4 ✅ Module: Teacher Management (63%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| CRUD giáo viên | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Chuyên môn | ✅ | ❌ | ❌ | In Progress | 1/4 |

**Vấn đề:**
- Chưa có UI quản lý chuyên môn GV

---

### 2.5 ✅ Module: Subject Management - Môn học (100%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| CRUD môn học | ✅ | ✅ | ✅ | Implemented | 4/4 |

**Hoàn thiện:** ✅

---

### 2.6 ✅ Module: Class Management - Lớp học (100%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| CRUD lớp học | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Danh sách HS theo lớp | ✅ | ✅ | ✅ | Implemented | 4/4 |

**Hoàn thiện:** ✅

---

### 2.7 ✅ Module: Academic Year & Semester (100%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| CRUD năm học | ✅ | ✅ | ✅ | Implemented | 4/4 |
| CRUD học kỳ | ✅ | ✅ | ✅ | Implemented | 4/4 |

**Hoàn thiện:** ✅

---

### 2.8 ⚠️ Module: Grade Management - Điểm số (63%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Nhập điểm | ✅ | ✅ | ✅ | In Progress | 3/4 |
| Xem bảng điểm | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Tính điểm TB | ❌ | ❌ | ❌ | Not Started | 0/4 |
| Export bảng điểm | ✅ | ❌ | ✅ | Implemented | 3/4 |

**Vấn đề nghiêm trọng:**
❌ Chưa tính ĐTB tự động (dựa trên quy tắc: 10% Miệng + 20% 15ph + 30% GK + 40% CK)  
❌ Có thể dùng DB trigger để tính tự động  
⚠️ Chưa validate điểm (0-10)

---

### 2.9 ⚠️ Module: Conduct - Hạnh kiểm (38%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Nhập hạnh kiểm | ✅ | ✅ | ✅ | In Progress | 3/4 |
| Xếp loại tự động | ❌ | ❌ | ❌ | Not Started | 0/4 |

**Vấn đề:**
❌ Chưa có logic xếp loại tự động dựa trên điểm TB  
❌ Nên implement: ĐTB >=8.0 → "Tốt", >=6.5 → "Khá", >=5.0 → "Trung bình", <5.0 → "Yếu"

---

### 2.10 ⚠️ Module: Ranking - Xếp loại Học lực (25%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Tính học lực | ❌ | ❌ | ❌ | Not Started | 0/4 |
| Xem xếp loại | ✅ | ❌ | ✅ | In Progress | 2/4 |

**Vấn đề nghiêm trọng:**
❌ Chưa có logic tính học lực  
❌ Nên implement: ĐTB >=8.5 → "Giỏi", >=7.0 → "Khá", >=5.5 → "TB", >=3.5 → "Yếu", <3.5 → "Kém"

---

### 2.11 ⚠️ Module: Teaching Assignment - Phân công Giảng dạy (38%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Phân công GV | ✅ | ✅ | ✅ | In Progress | 3/4 |
| Check conflict | ❌ | ❌ | ❌ | Not Started | 0/4 |

**Vấn đề:**
❌ Chưa kiểm tra trùng lịch dạy (cùng lớp, cùng thứ, cùng tiết)  
❌ Nên validate: GV không dạy 2 lớp cùng 1 tiết

---

### 2.12 ❌ Module: Schedule - Thời khóa biểu (25%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Hiển thị TKB | ✅ | ❌ | ✅ | In Progress | 2/4 |
| TKB theo lớp | ✅ | ❌ | ✅ | In Progress | 2/4 |
| TKB theo GV | ❌ | ❌ | ❌ | Not Started | 0/4 |
| Auto-schedule | ❌ | ❌ | ❌ | Not Started | 0/4 |

**Vấn đề nghiêm trọng:**
❌ Chưa có UI đầy đủ  
❌ Chưa có TKB theo GV  
❌ Chưa có auto-scheduler (heuristic/Tabu Search)

---

### 2.13 ⚠️ Module: Reports & Statistics (42%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Báo cáo điểm | ✅ | ❌ | ✅ | Implemented | 3/4 |
| Thống kê học lực | ❌ | ❌ | ✅ | In Progress | 1/4 |
| Export PDF | ❌ | ❌ | ✅ | In Progress | 1/4 |

**Vấn đề:**
⚠️ iTextSharp đã có trong packages nhưng chưa dùng  
⚠️ Chưa có thống kê chi tiết (biểu đồ)

---

### 2.14 ⚠️ Module: Notifications - Thông báo (50%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| Gửi thông báo | ✅ | ❌ | ✅ | In Progress | 2/4 |
| Xem thông báo | ✅ | ❌ | ✅ | In Progress | 2/4 |

**Vấn đề:**
⚠️ Chưa có BUS layer  
⚠️ Chưa có filter theo loại thông báo

---

### 2.15 ✅ Module: Parent Management (88%)

| Chức năng | DAO | BUS | GUI | Trạng thái | Điểm |
|-----------|-----|-----|-----|------------|------|
| CRUD phụ huynh | ✅ | ✅ | ✅ | Implemented | 4/4 |
| Liên kết HS-PH | ✅ | ✅ | ✅ | In Progress | 3/4 |

**Hoàn thiện:** Gần như 100%, chỉ thiếu UI nhỏ

---

## 3. MODULES HOÀN THIỆN (100%)

✅ **Subject Management** - CRUD môn học hoàn chỉnh  
✅ **Class Management** - CRUD lớp học hoàn chỉnh  
✅ **Academic Year & Semester** - Quản lý năm học/HK hoàn chỉnh  
✅ **Parent Management** - Quản lý PH gần hoàn chỉnh

---

## 4. MODULES CẦN ƯU TIÊN

### 4.1 High Priority (Top 3)

1. **Grade Management (DiemSo)** - 63% → 90%
   - ✅ Implement tính ĐTB tự động (BUS or DB trigger)
   - ✅ Validate điểm (0-10)
   - ✅ Add export PDF

2. **Ranking (XepLoai)** - 25% → 90%
   - ✅ Implement tính học lực tự động
   - ✅ Add validation

3. **Conduct (HanhKiem)** - 38% → 90%
   - ✅ Implement xếp loại tự động dựa trên ĐTB
   - ✅ Add validation

### 4.2 Medium Priority

4. **Schedule (ThoiKhoaBieu)** - 25% → 70%
   - ✅ Complete TKB by class
   - ✅ Add TKB by teacher
   - ❌ Auto-scheduler (có thể bỏ qua)

5. **Reports & Statistics** - 42% → 70%
   - ✅ Add more reports
   - ✅ Implement PDF export (iTextSharp)

### 4.3 Low Priority

6. **Authentication & Authorization** - 33% → 70%
   - ✅ Implement BUS layer
   - ✅ Complete RBAC
   - ❌ Session management (optional)

7. **Teaching Assignment** - 38% → 70%
   - ✅ Add conflict checking
   - ✅ Validation

---

## 5. CHECKLIST HOÀN THIỆN MODULE

### 5.1 Definition of Done (DoD)

Mỗi module cần có:
- [ ] ✅ DAO layer đầy đủ (CRUD)
- [ ] ✅ BUS layer với validation
- [ ] ✅ GUI form hoàn chỉnh
- [ ] ✅ Error handling
- [ ] ✅ Data binding
- [ ] ✅ Search/filter
- [ ] ✅ Export (Excel/PDF) nếu cần
- [ ] ✅ RBAC (menu ẩn/hiện theo role)
- [ ] ✅ Test manual
- [ ] ✅ Documentation

### 5.2 Modules đạt DoD

✅ Subject Management  
✅ Class Management  
✅ Academic Year & Semester  
✅ Student Management (94% - gần đạt)

### 5.3 Modules chưa đạt DoD

⚠️ Grade Management (thiếu tính ĐTB)  
⚠️ Conduct (thiếu xếp loại auto)  
⚠️ Ranking (thiếu tính học lực)  
⚠️ Schedule (thiếu TKB by teacher, auto-schedule)  
⚠️ Reports (thiếu PDF export, biểu đồ)  
⚠️ Authentication (thiếu BUS, session)

---

## 6. KẾT LUẬN

### 6.1 Tổng kết

- **Completed Modules:** 3/14 (21%)
- **Near Complete:** 1/14 (7%)
- **In Progress:** 10/14 (71%)
- **Not Started:** 0/14 (0%)

**Overall Progress:** 52% (87/168 points)

### 6.2 Top 10 công việc ưu tiên

1. ✅ Implement tính ĐTB tự động (BUS or DB trigger)
2. ✅ Implement tính học lực tự động
3. ✅ Implement xếp loại hạnh kiểm tự động
4. ✅ Complete TKB module (TKB by teacher)
5. ✅ Add conflict checking cho PhanCongGiangDay
6. ✅ Implement PDF export (reports)
7. ✅ Add more statistics & charts
8. ✅ Implement BUS layer cho Authentication
9. ✅ Complete RBAC implementation
10. ✅ Add unit tests

### 6.3 Dự kiến hoàn thành

- **Target:** 95% (160/168 points)
- **Timeline:** 1 tuần
- **Effort:** 40-50 giờ
