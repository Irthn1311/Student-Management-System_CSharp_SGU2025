# SMOKE TEST - Auto Phân công & Auto TKB
**Ngày tạo:** 2025-01-28  
**Phiên bản:** 1.0  
**Môi trường:** Student Management System C# WinForms + MySQL  

---

## I. MỤC ĐÍCH
Kiểm tra nhanh các chức năng chính của hệ thống Auto Phân công và Auto TKB sau khi triển khai.

---

## II. CÁC TEST CASE

### TEST 1: Auto Phân công - Generate đề xuất

**Tiêu đề:** Tạo đề xuất phân công tự động  
**Mục tiêu:** Đảm bảo thuật toán Heuristic hoạt động, ưu tiên GVCN, cân bằng tải  

**Điều kiện tiên quyết:**
- Có ít nhất 3 lớp trong database
- Có ít nhất 5 môn học
- Có ít nhất 10 giáo viên với chuyên môn đã cập nhật (`GiaoVienChuyenMon` hoặc `GiaoVien_MonHoc`)
- Đã đăng nhập với quyền Admin/Quản lý

**Các bước thực hiện:**
1. Mở màn hình **Phân công giảng dạy**
2. Nhấn nút **"Auto Phân công (Mới)"**
3. Cửa sổ `ucAutoPhanCongPreview` hiện ra
4. Nhấn nút **"Auto Generate"**
5. Quan sát progress bar và kết quả

**Kết quả mong đợi:**
- ✅ Progress bar chạy đến 100%
- ✅ DataGridView hiển thị danh sách phân công (ít nhất 1 row)
- ✅ Mỗi row có đầy đủ: `MaLop`, `MaMonHoc`, `MaGiaoVien`, `SoTietTuan`, `Score`, `Note`
- ✅ Cột `Note` hiển thị tên đầy đủ: "Lớp 10A1 - Toán học - Nguyễn Văn A"
- ✅ Nếu lớp có GVCN và GVCN dạy được môn → `Note` chứa "(GVCN)"
- ✅ Status label hiển thị: "✓ Đã tạo X đề xuất phân công thành công!"
- ✅ Các nút `Kiểm tra`, `Lưu tạm`, `Chấp nhận` được enable

**Tiêu chí PASS:**
- Không có exception/crash
- Có ít nhất 1 phân công được tạo ra
- GVCN được ưu tiên (kiểm tra cột Note)

---

### TEST 2: Auto Phân công - Validation

**Tiêu đề:** Kiểm tra validation đề xuất phân công  
**Mục tiêu:** Phát hiện trùng lặp hoặc vi phạm constraint  

**Điều kiện tiên quyết:**
- Đã thực hiện TEST 1 thành công
- Có dữ liệu trong grid

**Các bước thực hiện:**
1. Từ màn hình `ucAutoPhanCongPreview`
2. Nhấn nút **"Kiểm tra"**
3. Quan sát message box

**Kết quả mong đợi:**
- ✅ MessageBox hiển thị "Tất cả phân công đều hợp lệ!"
- ✅ Hoặc nếu có lỗi: liệt kê chi tiết "Duplicate đề xuất: X|Y|Z"

**Tiêu chí PASS:**
- Validate logic hoạt động đúng
- Không crash

---

### TEST 3: Auto Phân công - Lưu tạm & Accept

**Tiêu đề:** Lưu tạm đề xuất và chấp nhận vào bảng chính  
**Mục tiêu:** Đảm bảo transaction an toàn, dữ liệu được ghi đúng  

**Điều kiện tiên quyết:**
- Đã thực hiện TEST 2 và validation PASS

**Các bước thực hiện:**
1. Nhấn nút **"Lưu tạm"**
2. Kiểm tra database: `SELECT * FROM PhanCong_Temp`
3. Nhấn nút **"✓ Chấp nhận"**
4. Kiểm tra database: `SELECT * FROM PhanCongGiangDay WHERE MaHocKy = 1`
5. Quay lại màn hình chính, refresh danh sách

**Kết quả mong đợi:**
- ✅ Sau "Lưu tạm": bảng `PhanCong_Temp` có dữ liệu
- ✅ Sau "Chấp nhận": bảng `PhanCongGiangDay` có thêm rows mới
- ✅ Bảng `PhanCong_Temp` bị xóa sạch
- ✅ Màn hình chính hiển thị đúng số phân công mới

**Tiêu chí PASS:**
- Transaction commit thành công
- Không có data loss
- Các constraint (UNIQUE, FK) không bị vi phạm

---

### TEST 4: Auto TKB - Generate TKB với Tabu Search

**Tiêu đề:** Tạo thời khóa biểu tự động bằng Tabu Search  
**Mục tiêu:** Đảm bảo Tabu Search tìm được nghiệm có Hard = 0  

**Điều kiện tiên quyết:**
- Đã có dữ liệu trong `PhanCongGiangDay` (từ TEST 3)
- Mở màn hình **Thời khóa biểu**
- Chọn Học kỳ phù hợp

**Các bước thực hiện:**
1. Nhấn nút **"Sắp xếp tự động"**
2. Đợi Tabu Search chạy (~ 30-90 giây)
3. Quan sát lưới TKB hiển thị

**Kết quả mong đợi:**
- ✅ Thanh loading/progress hiển thị (hoặc cursor = WaitCursor)
- ✅ Sau khi hoàn thành, lưới TKB (T2-T6 × Tiết 1-10) được fill
- ✅ Mỗi ô hiển thị: **Tên môn** + **Tên GV** + **Phòng** (không phải ID)
- ✅ Không có ô nào bị trùng lặp (cùng GV hoặc cùng Lớp tại cùng (Thứ, Tiết))
- ✅ MessageBox không hiện "Còn vi phạm cứng" (hoặc chỉ là warning nhẹ)

**Tiêu chí PASS:**
- Tabu Search không crash
- Tìm được nghiệm có Hard = 0 (hoặc rất nhỏ < 5)
- TKB hiển thị đầy đủ, không có slot trống quá nhiều

---

### TEST 5: Auto TKB - Validate đủ tiết/tuần

**Tiêu đề:** Kiểm tra mỗi (Lớp, Môn) đủ số tiết/tuần theo cấu hình  
**Mục tiêu:** Đảm bảo TKB không thiếu tiết  

**Điều kiện tiên quyết:**
- Đã thực hiện TEST 4, TKB đã hiển thị

**Các bước thực hiện:**
1. Kiểm tra database: 
   ```sql
   SELECT pc.MaLop, pc.MaMonHoc, m.SoTiet AS Required, COUNT(tkb.Id) AS Actual
   FROM PhanCongGiangDay pc
   JOIN MonHoc m ON pc.MaMonHoc = m.MaMonHoc
   LEFT JOIN TKB_Temp tkb ON tkb.MaLop = pc.MaLop AND tkb.MaMon = pc.MaMonHoc
   WHERE pc.MaHocKy = 1
   GROUP BY pc.MaLop, pc.MaMonHoc
   HAVING Required != Actual;
   ```
2. Nếu có kết quả → TKB chưa đủ tiết

**Kết quả mong đợi:**
- ✅ Query trên không trả về row nào (hoặc rất ít)
- ✅ Mọi môn đều đủ số tiết/tuần (Toán 4 tiết → có 4 slot trong TKB)

**Tiêu chí PASS:**
- Ít nhất 90% (Lớp, Môn) đủ tiết
- Nếu thiếu tiết → có cảnh báo rõ ràng

---

### TEST 6: Auto TKB - Lưu thời khóa biểu

**Tiêu đề:** Publish TKB từ bảng tạm sang bảng chính  
**Mục tiêu:** Đảm bảo BulkReplace transaction an toàn  

**Điều kiện tiên quyết:**
- Đã thực hiện TEST 5, validation PASS

**Các bước thực hiện:**
1. Nhấn nút **"Lưu thời khóa biểu"**
2. Xác nhận trong dialog popup
3. Kiểm tra database: `SELECT * FROM ThoiKhoaBieu WHERE ...`
4. Kiểm tra `TKB_Temp` đã bị xóa chưa

**Kết quả mong đợi:**
- ✅ Bảng `ThoiKhoaBieu` có thêm rows mới (ứng với mỗi slot trong TKB)
- ✅ Bảng `TKB_Temp` bị xóa sạch
- ✅ MessageBox hiển thị "Đã lưu thời khóa biểu chính thức"
- ✅ TKB không thể sửa được nữa (locked)

**Tiêu chí PASS:**
- Transaction commit thành công
- Không có data loss
- FK `MaPhanCong` đúng, không bị NULL

---

### TEST 7: Auto TKB - Rollback

**Tiêu đề:** Xóa TKB tạm và tạo lại  
**Mục tiêu:** Đảm bảo có thể hủy bỏ và thử lại  

**Điều kiện tiên quyết:**
- Có TKB trong `TKB_Temp` (chưa Publish)

**Các bước thực hiện:**
1. Nhấn nút **"Xóa"**
2. Xác nhận trong dialog
3. Kiểm tra `TKB_Temp` → nên rỗng
4. Nhấn **"Sắp xếp tự động"** lại
5. TKB mới hiển thị (khác với TKB cũ do random seed)

**Kết quả mong đợi:**
- ✅ `TKB_Temp` bị xóa sạch
- ✅ Có thể generate TKB mới thành công
- ✅ TKB chính thức (`ThoiKhoaBieu`) không bị ảnh hưởng

**Tiêu chí PASS:**
- Rollback không crash
- Có thể tạo lại TKB nhiều lần

---

### TEST 8: Drag & Drop (Manual Edit) - TKB

**Tiêu đề:** Kéo-thả tiết học trong TKB để sửa tay  
**Mục tiêu:** Kiểm tra validation khi di chuyển tiết  

**Điều kiện tiên quyết:**
- TKB đã hiển thị trong lưới (chưa Publish)

**Các bước thực hiện:**
1. Click vào 1 ô TKB (ví dụ: Toán, Thứ 2, Tiết 1)
2. Kéo sang ô khác (ví dụ: Thứ 3, Tiết 5)
3. Nếu Thứ 3, Tiết 5 đã có GV hoặc Lớp trùng → tooltip hiển thị lỗi
4. Nếu hợp lệ → ô được di chuyển

**Kết quả mong đợi:**
- ✅ Nếu vi phạm hard constraint → tooltip "⚠ Trùng GV/Lớp tại slot này"
- ✅ Nếu hợp lệ → ô được update vị trí mới
- ✅ Database `TKB_Temp` cũng được update theo

**Tiêu chí PASS:**
- Validation hoạt động đúng
- UI không bị lag khi drag
- **Note:** Chức năng này có thể chưa triển khai đầy đủ trong Phase 1 → mark as OPTIONAL

---

## III. SMOKE TEST CHECKLIST

| # | Test Case | Status | Ghi chú |
|---|-----------|--------|---------|
| 1 | Auto Phân công - Generate | ⬜ PENDING | Chạy lần đầu |
| 2 | Auto Phân công - Validation | ⬜ PENDING | |
| 3 | Auto Phân công - Lưu tạm & Accept | ⬜ PENDING | |
| 4 | Auto TKB - Generate | ⬜ PENDING | |
| 5 | Auto TKB - Validate đủ tiết | ⬜ PENDING | |
| 6 | Auto TKB - Lưu TKB | ⬜ PENDING | |
| 7 | Auto TKB - Rollback | ⬜ PENDING | |
| 8 | Drag & Drop (Manual Edit) | ⬜ OPTIONAL | Phase 2 |

**Ký hiệu:**
- ⬜ PENDING: Chưa test
- ✅ PASS: Test thành công
- ❌ FAIL: Test thất bại
- ⚠ WARNING: Pass nhưng có cảnh báo
- 🔄 SKIP: Tạm skip do điều kiện tiên quyết chưa đủ

---

## IV. CÁCH CHẠY SMOKE TEST

### 4.1. Chuẩn bị môi trường
```sql
-- 1. Backup database trước khi test
mysqldump -u root -p QuanLyHocSinh > backup_before_smoke_test.sql

-- 2. Reset bảng tạm (nếu cần)
TRUNCATE TABLE PhanCong_Temp;
TRUNCATE TABLE TKB_Temp;

-- 3. Seed dữ liệu test (nếu database trống)
-- INSERT test data: GiaoVien, MonHoc, LopHoc, GiaoVienChuyenMon...
```

### 4.2. Chạy từng test case
- Mở ứng dụng C# WinForms
- Đăng nhập với tài khoản Admin
- Thực hiện từng test case theo thứ tự (1 → 7)
- Ghi lại kết quả vào Checklist

### 4.3. Báo cáo kết quả
- Cập nhật Status column trong Checklist
- Nếu FAIL: ghi chi tiết lỗi vào cột "Ghi chú"
- Chụp screenshot nếu cần
- Tạo GitHub Issue nếu phát hiện bug nghiêm trọng

---

## V. KẾT LUẬN

**Tiêu chí DoD tổng:**
- ✅ **ALL PASS:** Tất cả test case đều PASS → Sẵn sàng release
- ⚠ **PARTIAL PASS:** 1-2 test FAIL nhưng không critical → Ghi nhận bug, release với disclaimer
- ❌ **FAIL:** > 2 test FAIL hoặc có critical bug → Không release, fix bug trước

**Người thực hiện smoke test:** _______________  
**Ngày test:** _______________  
**Kết quả:** ✅ PASS / ⚠ PARTIAL PASS / ❌ FAIL  

---

**Tài liệu tham khảo:**
- `docs/CaiTienTKB.md` (Tài liệu kỹ thuật chi tiết)
- `docs/QuyTrinhPhanCong_TKB.txt` (Spec gốc)

