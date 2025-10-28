# BUGS - BÁO CÁO LỖI

**Project:** Student Management System CSharp SGU2025  
**Date:** ${new Date().toISOString().split('T')[0]}

---

## 1. LỖI NGHIÊM TRỌNG (Critical)

### Bug #1: DiemTrungBinh không tự động tính

**Severity:** Critical  
**Priority:** P0  
**Status:** Open

**Mô tả:**
- Khi nhập điểm (DiemMieng, Diem15Phut, DiemGiuaKy, DiemCuoiKy), DiemTrungBinh không tự động tính
- Công thức: 10% Miệng + 20% 15ph + 30% GK + 40% CK

**Reproduction Steps:**
1. Vào "Điểm số"
2. Chọn lớp, môn, học kỳ
3. Nhập điểm cho học sinh
4. Click "Lưu"
5. Kết quả: DiemTrungBinh = 0 hoặc NULL

**Expected Behavior:**
- DiemTrungBinh tự động tính và hiển thị

**Actual Behavior:**
- DiemTrungBinh không được tính

**File Location:**
- `bus/NhapDiemBUS.cs`
- `dao/DiemSoDAO.cs`

**Proposed Fix:**
```csharp
// Add to BUS layer
public float CalculateDiemTrungBinh(float diemMieng, float diem15Phut, float diemGiuaKy, float diemCuoiKy)
{
    return diemMieng * 0.1f + 
           diem15Phut * 0.2f + 
           diemGiuaKy * 0.3f + 
           diemCuoiKy * 0.4f;
}

// Or use DB trigger
```

**Assigned to:** Backend Developer  
**Estimated Effort:** 2 hours

---

### Bug #2: HocLuc không tự động tính

**Severity:** Critical  
**Priority:** P0  
**Status:** Open

**Mô tả:**
- Sau khi có DiemTrungBinh, HocLuc không tự động tính
- Criteria:
  - ĐTB >= 8.5 → "Giỏi"
  - ĐTB >= 7.0 → "Khá"
  - ĐTB >= 5.5 → "Trung bình"
  - ĐTB >= 3.5 → "Yếu"
  - ĐTB < 3.5 → "Kém"

**Reproduction Steps:**
1. Nhập điểm cho học sinh
2. Tính DiemTrungBinh
3. Kiểm tra XepLoai.HocLuc
4. Kết quả: NULL hoặc empty

**Expected Behavior:**
- HocLuc tự động tính dựa trên DiemTrungBinh

**File Location:**
- `bus/HanhKiemBUS.cs` (or create new XepLoaiBUS.cs)
- `GUI/XepLoai/ucXepLoai.cs`

**Proposed Fix:**
```csharp
public string CalculateHocLuc(float diemTrungBinh)
{
    if (diemTrungBinh >= 8.5f) return "Giỏi";
    if (diemTrungBinh >= 7.0f) return "Khá";
    if (diemTrungBinh >= 5.5f) return "Trung bình";
    if (diemTrungBinh >= 3.5f) return "Yếu";
    return "Kém";
}
```

**Assigned to:** Backend Developer  
**Estimated Effort:** 2 hours

---

### Bug #3: HanhKiem không tự động xếp loại

**Severity:** Critical  
**Priority:** P0  
**Status:** Open

**Mô tả:**
- HanhKiem.XepLoai không tự động xếp dựa trên ĐTB và hành vi
- Criteria:
  - ĐTB >= 8.0 → "Tốt"
  - ĐTB >= 6.5 → "Khá"
  - ĐTB >= 5.0 → "Trung bình"
  - ĐTB < 5.0 → "Yếu"

**Reproduction Steps:**
1. Nhập hạnh kiểm cho học sinh
2. Kết quả: XepLoai = NULL hoặc empty

**Expected Behavior:**
- XepLoai tự động xếp dựa trên ĐTB

**File Location:**
- `bus/HanhKiemBUS.cs`
- `GUI/HanhKiem/HanhKiem.cs`

**Proposed Fix:**
```csharp
public string ClassifyHanhKiem(float diemTrungBinh)
{
    if (diemTrungBinh >= 8.0f) return "Tốt";
    if (diemTrungBinh >= 6.5f) return "Khá";
    if (diemTrungBinh >= 5.0f) return "Trung bình";
    return "Yếu";
}
```

**Assigned to:** Backend Developer  
**Estimated Effort:** 2 hours

---

## 2. LỖI NGHIỆM TRỌNG (High)

### Bug #4: Không check conflict phân công giảng dạy

**Severity:** High  
**Priority:** P1  
**Status:** Open

**Mô tả:**
- Khi phân công giáo viên dạy lớp, không check xem GV có đang dạy lớp khác cùng thứ/tiết không
- Có thể phân công trùng lịch

**Reproduction Steps:**
1. Tạo phân công: GV001 dạy Toán cho 10A1, Thứ 2, Tiết 1
2. Tạo phân công khác: GV001 dạy Lý cho 10A2, Thứ 2, Tiết 1
3. Kết quả: Cả 2 đều được tạo (trùng lịch)

**Expected Behavior:**
- Hiển thị lỗi "GV đang bận dạy lớp khác cùng lúc"

**File Location:**
- `bus/PhanCongGiangDayBUS.cs`
- `GUI/PhanCongGiangDay/PhanCongGiangDay.cs`

**Proposed Fix:**
```csharp
public bool CheckConflict(PhanCongGiangDayDTO phanCong)
{
    // Query: Check if GV dạy lớp khác cùng thứ, cùng tiết
    // Return true if conflict
}
```

**Assigned to:** Backend Developer  
**Estimated Effort:** 3 hours

---

### Bug #5: Passwords stored in plaintext

**Severity:** High  
**Priority:** P1  
**Status:** Open

**Mô tả:**
- Passwords được lưu dạng plaintext trong database
- Security risk rất cao

**Evidence:**
```sql
INSERT INTO NguoiDung (TenDangNhap, MatKhau, TrangThai) 
VALUES ('admin', '12345678', 'Hoạt động');
```

**Expected Behavior:**
- Passwords được hash (SHA256 or BCrypt)

**File Location:**
- `ConnectDatabase/QuanLyHocSinh.sql` (seed data)
- Login logic

**Proposed Fix:**
```csharp
// Hash password on insert/update
string hashedPassword = SHA256.HashData(Encoding.UTF8.GetBytes(password));

// Verify on login
bool isValid = VerifyHash(password, storedHash);
```

**Assigned to:** Backend Developer  
**Estimated Effort:** 2 hours

---

### Bug #6: RBAC không hoạt động đầy đủ

**Severity:** High  
**Priority:** P1  
**Status:** Open

**Mô tả:**
- Menu không ẩn/hiện theo role của user
- Tất cả user đều thấy tất cả menu

**Reproduction Steps:**
1. Đăng nhập với role "student"
2. Kết quả: Vẫn thấy menu "Quản lý giáo viên", "Phân công", etc.

**Expected Behavior:**
- Chỉ thấy menu phù hợp với role

**File Location:**
- `GUI/Forms/frmMain.cs`
- `GUI/Controls/ucSidebar.cs`

**Proposed Fix:**
```csharp
private void SetupMenuVisibility(string userRole)
{
    switch(userRole)
    {
        case "student":
            // Hide admin menus
            break;
        case "teacher":
            // Show teacher menus
            break;
    }
}
```

**Assigned to:** Frontend Developer  
**Estimated Effort:** 4 hours

---

## 3. LỖI TRUNG BÌNH (Medium)

### Bug #7: Thiếu PDF export

**Severity:** Medium  
**Priority:** P2  
**Status:** Open

**Mô tả:**
- iTextSharp đã có trong packages nhưng chưa dùng
- Chỉ có export Excel

**Expected Behavior:**
- Có option export PDF

**Proposed Fix:**
- Implement PDF export sử dụng iTextSharp

**Assigned to:** Frontend Developer  
**Estimated Effort:** 4 hours

---

### Bug #8: Thiếu seed data

**Severity:** Medium  
**Priority:** P2  
**Status:** Open

**Mô tả:**
- Hiện chỉ có 20 HS trong seed data
- Thiếu: 500 HS, DiemSo, ThoiKhoaBieu, etc.

**Impact:**
- Khó test đầy đủ
- Demo không hấp dẫn

**Proposed Fix:**
- Create comprehensive seed data script

**Assigned to:** Backend Developer  
**Estimated Effort:** 6 hours

---

### Bug #9: Connection string hard-coded

**Severity:** Medium  
**Priority:** P2  
**Status:** Open

**Mô tả:**
- Connection string hard-coded trong code
- Khó thay đổi khi deploy

**File Location:**
- `ConnectDatabase/ConnectionDatabase.cs`

**Proposed Fix:**
```csharp
// Read from App.config
string connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;
```

**Assigned to:** Backend Developer  
**Estimated Effort:** 1 hour

---

## 4. LỖI NHỎ (Low)

### Bug #10: Thiếu loading indicator

**Severity:** Low  
**Priority:** P3  
**Status:** Open

**Mô tả:**
- Khi load data lớn, không có loading indicator
- User không biết app đang làm gì

**Proposed Fix:**
- Add loading spinner/p progress bar

**Assigned to:** Frontend Developer  
**Estimated Effort:** 3 hours

---

### Bug #11: Error messages không nhất quán

**Severity:** Low  
**Priority:** P3  
**Status:** Open

**Mô tả:**
- Một số error messages bằng tiếng Việt, một số bằng tiếng Anh
- Một số hiển thị technical details, một số không

**Proposed Fix:**
- Standardize error messages
- User-friendly messages

**Assigned to:** Frontend Developer  
**Estimated Effort:** 4 hours

---

### Bug #12: Thiếu tooltips

**Severity:** Low  
**Priority:** P3  
**Status:** Open

**Mô tả:**
- Nhiều button không có tooltip
- User phải đoán chức năng

**Proposed Fix:**
- Add tooltips cho tất cả buttons

**Assigned to:** Frontend Developer  
**Estimated Effort:** 2 hours

---

## 5. TỔNG KẾT

### 5.1 Bug Statistics

| Severity | Count | Fixed | Open |
|----------|-------|-------|------|
| Critical | 3 | 0 | 3 |
| High | 3 | 0 | 3 |
| Medium | 3 | 0 | 3 |
| Low | 3 | 0 | 3 |

**Total:** 12 bugs  
**Fixed:** 0  
**Open:** 12  
**Fixed Rate:** 0%

### 5.2 Priority Distribution

- **P0 (Critical):** 3 bugs
- **P1 (High):** 3 bugs
- **P2 (Medium):** 3 bugs
- **P3 (Low):** 3 bugs

### 5.3 Top 5 Bugs to Fix First

1. ❌ Bug #1: DiemTrungBinh không tự tính
2. ❌ Bug #2: HocLuc không tự tính
3. ❌ Bug #3: HanhKiem không tự xếp loại
4. ❌ Bug #5: Passwords plaintext
5. ❌ Bug #6: RBAC không hoạt động

**Estimated Total Effort:** ~30 hours

---

## 6. BUG REPORT TEMPLATE

### Template for New Bugs

```markdown
### Bug #[NUMBER]: [Title]

**Severity:** Critical/High/Medium/Low  
**Priority:** P0/P1/P2/P3  
**Status:** Open/In Progress/Fixed/Closed

**Mô tả:**
[Describe the bug]

**Reproduction Steps:**
1. [Step 1]
2. [Step 2]
3. [Step 3]

**Expected Behavior:**
[What should happen]

**Actual Behavior:**
[What actually happens]

**File Location:**
[File paths]

**Proposed Fix:**
[How to fix]

**Assigned to:** [Person]  
**Estimated Effort:** [Hours]
```
