# 📅 HƯỚNG DẪN SỬ DỤNG THỜI KHÓA BIỂU (TKB)
**Phiên bản:** 2.0.0  
**Cập nhật:** 2025-01-28  
**Dành cho:** Người dùng cuối (Admin, Quản lý)  

---

## 🎯 TỔNG QUAN

### Tính năng mới (v2.0)
✨ **Auto tạo TKB bằng thuật toán Tabu Search**  
✨ **Lọc theo Học kỳ và Lớp**  
✨ **Preview trước khi lưu chính thức**  
✨ **Hiển thị tên Môn/GV thay vì ID**  
✨ **Validation tự động: không trùng GV/Lớp**  

### Quy trình 5 bước
```
[1. Chọn Học kỳ] → [2. Tạo TKB tự động] → [3. Xem Preview] 
     → [4. Lưu chính thức] → [5. Xuất Excel]
```

---

## 📋 HƯỚNG DẪN CHI TIẾT

### Bước 0: Chuẩn bị (QUAN TRỌNG!)

**❗ Điều kiện tiên quyết:**
- ✅ Đã có dữ liệu **Phân công giảng dạy** cho học kỳ đó
- ✅ Nếu chưa có: Vào menu **"Phân công giảng dạy"** → **"Auto Phân công (Mới)"** → Tạo trước

**Cách kiểm tra:**
1. Vào menu **"Phân công giảng dạy"**
2. Xem bảng có dữ liệu chưa
3. Nếu trống → Nhấn **"Auto Phân công (Mới)"** và thực hiện phân công trước

---

### Bước 1: Mở màn hình Thời khóa biểu

1. Đăng nhập với tài khoản **Admin** hoặc **Quản lý**
2. Vào menu **"Thời khóa biểu"** (sidebar)
3. Màn hình TKB hiển thị

---

### Bước 2: Chọn Học kỳ

**ComboBox "Học kỳ"** (góc trên bên phải):
1. Click vào dropdown **"Chọn học kỳ"**
2. Chọn học kỳ muốn tạo TKB (ví dụ: "Học Kỳ I - 2024 - 2025")
3. Tiêu đề thay đổi thành: **"Thời khóa biểu - Học Kỳ I - 2024 - 2025"**

**Nếu đã có TKB:**
- Lưới TKB tự động hiển thị
- Các nút "Lưu" và "Xóa" được enable

**Nếu chưa có TKB:**
- Lưới trống
- Cần nhấn "Sắp xếp tự động" để tạo

---

### Bước 3: Tạo TKB tự động

**Nút "Sắp xếp tự động"** (góc trên bên trái):
1. Nhấn nút **"Sắp xếp tự động"**
2. Dialog xác nhận hiện ra:
   ```
   Bạn có chắc muốn tạo Thời khóa biểu tự động cho học kỳ này?
   
   ⏱ Quá trình có thể mất 30-90 giây.
   💡 Hệ thống sẽ sử dụng thuật toán Tabu Search để tối ưu.
   ```
3. Nhấn **"Yes"** để tiếp tục

**Quá trình tạo TKB:**
- ⏳ Tiêu đề hiển thị: **"⏳ Đang tạo TKB cho Học Kỳ I..."**
- 🔄 Cursor chuyển thành loading (hourglass)
- ⚙ Thuật toán Tabu Search chạy (30-90 giây)
- ✅ Khi xong, TKB hiển thị trong lưới

**Nếu thành công:**
```
✅ Đã tạo Thời khóa biểu thành công!

📊 Tổng số tiết: 250
💯 Điểm soft: 1234

Bạn có thể xem lại và nhấn 'Lưu' để chốt.
```

**Nếu có cảnh báo:**
```
⚠ Còn 3 vi phạm cứng.

Hệ thống vẫn sẽ lưu vào tạm để bạn xem và chỉnh sửa.

Chi tiết:
- Trùng GV: GV001|2|3
- Trùng Lớp: 10A1|4|5
```

**Nếu thiếu dữ liệu phân công:**
```
Chưa có dữ liệu phân công giảng dạy trong học kỳ này.

Vui lòng thực hiện 'Auto Phân công' trước!
```
→ Quay lại **Bước 0** để tạo Phân công trước.

---

### Bước 4: Xem và hiểu TKB

**Lưới TKB hiển thị:**
- **Cột:** Thứ 2, Thứ 3, Thứ 4, Thứ 5, Thứ 6 (không có Thứ 7 - theo quy định)
- **Hàng:** Tiết 1, Tiết 2, ..., Tiết 10
  - **Sáng:** Tiết 1-5
  - **Chiều:** Tiết 6-10

**Mỗi ô hiển thị:**
```
┌─────────────────┐
│ Toán học        │ ← Tên môn (có màu)
│ Nguyễn Văn A    │ ← Tên giáo viên
│ Phòng 101       │ ← Phòng học
└─────────────────┘
```

**Màu sắc môn học:**
- 🔵 **Toán học:** Xanh dương
- 🟠 **Vật lý:** Cam
- 🟣 **Tiếng Anh:** Tím
- 🟢 **Sinh học:** Xanh ngọc
- 🔴 **Hóa học:** Hồng
- 🟢 **Ngữ văn:** Xanh lá
- 🟡 **Lịch sử:** Vàng
- 🔵 **Địa lý:** Xanh chàm
- ... (mỗi môn có màu riêng)

---

### Bước 5: Lưu TKB chính thức

**Nút "Lưu thời khóa biểu"** (giữa):
1. Kiểm tra lại TKB trong lưới
2. Nhấn **"Lưu thời khóa biểu"**
3. Dialog xác nhận:
   ```
   Bạn có chắc chắn muốn lưu Thời khóa biểu này vào hệ thống chính thức?
   
   ⚠ Sau khi lưu, TKB sẽ được áp dụng và không thể chỉnh sửa dễ dàng.
   ```
4. Nhấn **"Yes"**

**Kết quả:**
```
✅ Đã lưu Thời khóa biểu chính thức!

TKB đã được publish và có thể xem/in ấn.
```

**Sau khi lưu:**
- 🔒 TKB bị **khóa**, không sửa được nữa
- 🔘 Nút "Lưu" và "Xóa" bị **disable**
- 📊 TKB được lưu vào bảng `ThoiKhoaBieu` (chính thức)

---

### Bước 6: Xuất Excel (Optional)

**Nút "Xuất Excel"** (góc trên phải):
1. Nhấn **"Xuất Excel"**
2. Chọn nơi lưu file
3. File `.xlsx` được export với format đẹp

**File Excel bao gồm:**
- Sheet 1: TKB theo Lớp
- Sheet 2: TKB theo Giáo viên (nếu có)
- Format: Màu sắc, borders, merge cells

---

## 🔄 CÁC THAO TÁC NÂNG CAO

### Tạo lại TKB (nếu chưa hài lòng)

**Trước khi lưu chính thức:**
1. Nhấn **"Xóa"** (nút đỏ) để xóa lịch tạm
2. Nhấn **"Sắp xếp tự động"** lại
3. TKB mới sẽ được tạo (khác TKB cũ do Tabu Search có random seed)

**Sau khi đã lưu chính thức:**
- ⚠ Cần quyền Admin cao cấp để rollback
- Hoặc: Tạo TKB cho học kỳ mới

### Xem TKB theo Lớp (Filter)

**ComboBox "Lớp"** (góc trên phải):
1. Chọn **"Tất cả lớp"**: Hiển thị TKB của tất cả lớp (overlaid)
2. Chọn **"Lớp 10A1"**: Hiển thị TKB chỉ của lớp 10A1
3. Chọn **"Lớp 10A2"**: Hiển thị TKB chỉ của lớp 10A2

**Lưu ý:** Hiện tại filter theo lớp chưa implement đầy đủ → sẽ có trong Phase 2.

---

## ❓ FAQ (Câu hỏi thường gặp)

### 1. Tại sao không nhấn được "Sắp xếp tự động"?

**Nguyên nhân:**
- Chưa chọn Học kỳ

**Giải pháp:**
- Chọn Học kỳ từ dropdown trước

### 2. Tạo TKB báo lỗi "Chưa có dữ liệu phân công"?

**Nguyên nhân:**
- Chưa thực hiện Auto Phân công cho học kỳ đó

**Giải pháp:**
1. Vào menu **"Phân công giảng dạy"**
2. Nhấn **"Auto Phân công (Mới)"**
3. Tạo phân công cho học kỳ đó
4. Quay lại TKB và thử lại

### 3. TKB có vi phạm cứng, làm sao?

**Nguyên nhân:**
- Dữ liệu phân công có vấn đề (ví dụ: GV phải dạy quá nhiều lớp cùng 1 tiết)
- Thuật toán chưa tìm được nghiệm tối ưu

**Giải pháp:**
- **Option 1:** Nhấn **"Xóa"** → **"Sắp xếp tự động"** lại (thử nghiệm khác)
- **Option 2:** Chỉnh sửa Phân công (giảm tải GV)
- **Option 3:** Liên hệ Admin để điều chỉnh tham số Tabu Search

### 4. Muốn thay đổi TKB đã lưu?

**Nếu chưa Publish (vẫn ở tạm):**
- Nhấn **"Xóa"** → Tạo lại

**Nếu đã Publish:**
- ⚠ Khó rollback
- Nên tạo TKB cho học kỳ mới
- Hoặc liên hệ IT Admin để xóa trực tiếp DB (nguy hiểm!)

### 5. TKB hiển thị "Phòng TBA" là gì?

**Ý nghĩa:**
- TBA = To Be Assigned (Chưa gán phòng)
- Do hệ thống chưa quản lý Phòng học riêng

**Giải pháp:**
- Phòng học mặc định gắn theo Lớp
- Hoặc đợi Phase 2 có quản lý Phòng học độc lập

### 6. Làm sao biết TKB đã đủ số tiết/tuần?

**Kiểm tra:**
- Sau khi tạo TKB, hệ thống tự động validate
- Nếu thiếu tiết, sẽ có cảnh báo: **"Môn Toán của Lớp 10A1 thiếu 1 tiết"**
- Nếu OK, nút **"Lưu"** sẽ được enable

**Quy tắc:**
- Mỗi môn có số tiết/tuần cấu hình trong bảng `MonHoc` (cột `SoTiet`)
- Ví dụ: Toán = 4 tiết/tuần → phải có 4 ô Toán trong TKB (bất kỳ vị trí nào trong T2-T6)

---

## 🎨 GIAO DIỆN & CHỨC NĂNG

### Layout chính
```
┌─────────────────────────────────────────────────────────┐
│ [Sắp xếp tự động] [Lưu TKB] [Xóa]    [Học kỳ▼] [Lớp▼] │
├─────────────────────────────────────────────────────────┤
│  Thời khóa biểu - Học Kỳ I - 2024-2025                 │
├─────┬────────┬────────┬────────┬────────┬────────┬─────┤
│Tiết │ Thứ 2  │ Thứ 3  │ Thứ 4  │ Thứ 5  │ Thứ 6  │Thứ 7│
├─────┼────────┼────────┼────────┼────────┼────────┼─────┤
│  1  │ Toán   │ Văn    │ Anh    │ Hóa    │ Lý     │     │
│     │ GV: A  │ GV: B  │ GV: C  │ GV: D  │ GV: E  │     │
├─────┼────────┼────────┼────────┼────────┼────────┼─────┤
│  2  │ ...    │ ...    │ ...    │ ...    │ ...    │     │
│ ... │        │        │        │        │        │     │
│ 10  │ ...    │ ...    │ ...    │ ...    │ ...    │     │
└─────┴────────┴────────┴────────┴────────┴────────┴─────┘
```

### Các nút chức năng

| Nút | Màu | Chức năng | Khi nào enable? |
|-----|-----|-----------|-----------------|
| **Sắp xếp tự động** | Xám | Tạo TKB tự động (Tabu Search) | Luôn (nếu đã chọn Học kỳ) |
| **Lưu thời khóa biểu** | Xanh | Lưu TKB vào hệ thống chính | Khi đã có TKB tạm |
| **Xóa** | Đỏ | Xóa TKB tạm để tạo lại | Khi đã có TKB tạm |
| **Xuất Excel** | Xanh lá | Export TKB ra file .xlsx | Khi đã có TKB (tạm hoặc chính thức) |

---

## ⚠️ LƯU Ý QUAN TRỌNG

### DO's ✅
- ✅ Chọn Học kỳ trước khi tạo TKB
- ✅ Kiểm tra Phân công đã có chưa
- ✅ Xem kỹ Preview trước khi Lưu
- ✅ Backup database trước khi Publish TKB quan trọng
- ✅ Thử "Sắp xếp tự động" nhiều lần để tìm nghiệm tốt hơn

### DON'Ts ❌
- ❌ KHÔNG lưu TKB khi còn vi phạm cứng (trùng GV/Lớp)
- ❌ KHÔNG xóa TKB đã Publish (trừ khi backup DB)
- ❌ KHÔNG tạo TKB khi Phân công chưa xong
- ❌ KHÔNG tắt ứng dụng khi đang "Sắp xếp tự động" (làm mất dữ liệu)

---

## 🐛 XỬ LÝ LỖI

### Lỗi: "Lỗi khi load học kỳ"

**Nguyên nhân:** Kết nối database thất bại hoặc bảng `HocKy` trống

**Giải pháp:**
1. Kiểm tra MySQL service đã chạy chưa
2. Kiểm tra connection string trong `App.config`
3. Kiểm tra bảng `HocKy` có dữ liệu chưa:
   ```sql
   SELECT * FROM HocKy;
   ```
4. Nếu trống, thêm học kỳ mẫu:
   ```sql
   INSERT INTO HocKy (TenHocKy, NamHoc, NgayBatDau, NgayKetThuc)
   VALUES ('Học Kỳ I - 2024-2025', '2024-2025', '2024-09-01', '2025-01-15');
   ```

### Lỗi: "Lỗi khi sinh TKB: ..."

**Nguyên nhân:** Thuật toán Tabu Search gặp exception

**Giải pháp:**
1. Xem chi tiết trong MessageBox (có stack trace)
2. Kiểm tra log file (nếu có)
3. Thử giảm số lớp/môn để test
4. Liên hệ IT support với screenshot lỗi

### Lỗi: Database constraint violation

**Ví dụ:**
```
Duplicate entry '1-Thu 2-3' for key 'ux_tkb_lop'
```

**Nguyên nhân:** Đang cố gắng insert TKB trùng lặp

**Giải pháp:**
1. Nhấn **"Xóa"** để clear bảng tạm
2. Tạo lại từ đầu

---

## 📖 THUẬT NGỮ & ĐỊNH NGHĨA

| Thuật ngữ | Nghĩa |
|-----------|-------|
| **TKB** | Thời khóa biểu |
| **Học kỳ** | Một kỳ học (HK I hoặc HK II) |
| **Tiết** | 1 tiết học (45 phút) |
| **Phân công** | Gán GV dạy môn cho lớp |
| **Tabu Search** | Thuật toán tối ưu hóa metaheuristic |
| **Hard constraint** | Ràng buộc cứng (bắt buộc phải đúng) |
| **Soft constraint** | Ràng buộc mềm (nên đúng, không bắt buộc) |
| **Preview** | Xem trước (chưa lưu chính thức) |
| **Publish** | Lưu chính thức (khóa không sửa được) |
| **TKB tạm** | Lưu trong bảng `TKB_Temp` (có thể xóa/sửa) |
| **TKB chính thức** | Lưu trong bảng `ThoiKhoaBieu` (đã publish) |

---

## 📞 HỖ TRỢ

**Nếu gặp khó khăn:**
- 📧 Email: support@yourschool.edu.vn
- 💬 Slack: #student-management-support
- 📱 Hotline: 0123-456-789 (8h-17h, T2-T6)

**Khi báo lỗi, cung cấp:**
1. Screenshot màn hình lỗi
2. Thông điệp lỗi đầy đủ
3. Các bước đã thực hiện
4. Học kỳ và Lớp đang chọn
5. Số lượng lớp/môn trong hệ thống

---

**🎉 Chúc bạn sử dụng hiệu quả!**

Tài liệu kỹ thuật chi tiết: `docs/CaiTienTKB.md`  
Hướng dẫn test: `docs/SMOKE_TEST.md`

