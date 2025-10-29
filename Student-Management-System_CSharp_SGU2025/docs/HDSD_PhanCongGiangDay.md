# Hướng dẫn sử dụng - Phân công giảng dạy

## Mục lục

1. [Tổng quan](#tổng-quan)
2. [Giao diện chính](#giao-diện-chính)
3. [Chức năng CRUD](#chức-năng-crud)
4. [Phân công tự động](#phân-công-tự-động)
5. [Lọc và tìm kiếm](#lọc-và-tìm-kiếm)
6. [Xuất báo cáo](#xuất-báo-cáo)
7. [Quy tắc nghiệp vụ](#quy-tắc-nghiệp-vụ)

---

## Tổng quan

Module **Phân công giảng dạy** cho phép quản lý việc phân công giáo viên dạy các môn học cho từng lớp trong mỗi học kỳ.

### Tính năng chính:

- ✅ Thêm, sửa, xóa phân công thủ công
- ✅ Tự động tạo phân công dựa trên heuristic thông minh
- ✅ Phân quyền theo học kỳ (quá khứ = readonly)
- ✅ Lọc theo lớp, môn học, giáo viên, học kỳ
- ✅ Kiểm tra chuyên môn giáo viên
- ✅ Phát hiện trùng lặp
- ✅ Xuất báo cáo Excel

---

## Giao diện chính

Giao diện được chia thành 2 phần:

### Bên trái - Form nhập liệu

- **Học kỳ**: Chọn học kỳ cần phân công
- **Lớp**: Chọn lớp học
- **Môn học**: Chọn môn cần dạy
- **Giáo viên**: Chọn giáo viên phụ trách
- **Ngày bắt đầu/kết thúc**: Thời gian phân công

### Bên phải - Danh sách phân công

- Hiển thị tất cả phân công hiện có
- Bộ lọc nhanh theo nhiều tiêu chí
- Badge trạng thái học kỳ (Đang diễn ra/Đã kết thúc/Chưa bắt đầu)

---

## Chức năng CRUD

### 1. Thêm phân công mới

**Các bước:**

1. Nhấn nút **"Thêm"**
2. Chọn thông tin: Học kỳ, Lớp, Môn học, Giáo viên
3. Chọn ngày bắt đầu và kết thúc
4. Nhấn **"Lưu"**

**Lưu ý:**

- Hệ thống tự động kiểm tra:
  - ✅ Giáo viên có chuyên môn phù hợp với môn
  - ✅ Không trùng (Lớp - Môn - Học kỳ)
  - ✅ Học kỳ chưa kết thúc

### 2. Sửa phân công

**Các bước:**

1. Chọn dòng cần sửa trong bảng
2. Nhấn **"Sửa"**
3. Thay đổi thông tin cần thiết
4. Nhấn **"Lưu"**

**Giới hạn:**

- ⚠️ Không thể sửa phân công của học kỳ đã kết thúc

### 3. Xóa phân công

**Các bước:**

1. Chọn dòng cần xóa
2. Nhấn **"Xóa"**
3. Xác nhận trong hộp thoại

**Giới hạn:**

- ⚠️ Không thể xóa phân công của học kỳ đã kết thúc

---

## Phân công tự động

### Cách sử dụng

1. Nhấn nút **"🤖 Tạo tự động"**
2. Một cửa sổ mới hiện ra với các tùy chọn:

   - **Khối**: Chọn khối 10, 11, 12 hoặc tất cả
   - **Môn**: Lọc theo môn cụ thể (tùy chọn)
   - **Max tiết/tuần**: Giới hạn số tiết tối đa cho mỗi GV
   - **Cho phép trái chuyên môn**: Bật/tắt

3. Nhấn **"Auto Generate"**
4. Xem danh sách đề xuất
5. Có thể:
   - **Kiểm tra**: Validate tính hợp lệ
   - **Lưu tạm**: Lưu vào bảng tạm
   - **Chấp nhận**: Lưu vào DB chính thức
   - **Hủy tạm**: Xóa bảng tạm

### Thuật toán

Hệ thống ưu tiên:

1. 🎯 **Giáo viên chủ nhiệm** (nếu có chuyên môn)
2. ⚖️ **Cân bằng tải** giữa các giáo viên
3. ✅ **Chuyên môn chính** trước, chuyên môn phụ sau
4. 🚫 **Không quá tải** (tuân thủ max tiết/tuần)

---

## Lọc và tìm kiếm

### Bộ lọc

- **Học kỳ**: Lọc theo học kỳ cụ thể
- **Lớp**: Lọc theo lớp
- **Môn**: Lọc theo môn học
- **Giáo viên**: Lọc theo giáo viên

### Cách sử dụng

1. Chọn tiêu chí lọc (có thể kết hợp nhiều tiêu chí)
2. Nhấn **"Lọc"**
3. Kết quả hiển thị ngay lập tức

**Mẹo:**

- Chọn "-- Tất cả --" để bỏ lọc tiêu chí đó
- Nhấn **"Làm mới"** để reset tất cả bộ lọc

---

## Xuất báo cáo

### Xuất Excel

1. Nhấn nút **"Xuất Excel"**
2. Chọn vị trí lưu file
3. File Excel được tạo với:
   - Header đẹp mắt
   - Dữ liệu đầy đủ
   - Định dạng chuyên nghiệp

**Định dạng file:**

- Tên: `PhanCongGiangDay_YYYYMMDD_HHmmss.xlsx`
- Sheet: "Phân công giảng dạy"

---

## Quy tắc nghiệp vụ

### 1. Kiểm tra chuyên môn

✅ **Hợp lệ:**

- Giáo viên có trong bảng `GiaoVienChuyenMon` hoặc `GiaoVien_MonHoc`

❌ **Không hợp lệ:**

- Giáo viên không có chuyên môn môn học đó

### 2. Kiểm tra trùng lặp

Một bộ (Lớp - Môn - Học kỳ) chỉ được phân công cho **DUY NHẤT** một giáo viên.

❌ **Vi phạm:**

```
Lớp 10A1 - Toán - HK1/2024-2025 -> GV A
Lớp 10A1 - Toán - HK1/2024-2025 -> GV B  ❌ (Trùng)
```

### 3. Phân quyền theo học kỳ

| Trạng thái học kỳ | Thêm | Sửa | Xóa |
| ----------------- | ---- | --- | --- |
| Đã kết thúc       | ❌   | ❌  | ❌  |
| Đang diễn ra      | ✅   | ✅  | ✅  |
| Chưa bắt đầu      | ✅   | ✅  | ✅  |

**Logic:**

```
Ngày kết thúc học kỳ >= Ngày hiện tại → Cho phép chỉnh sửa
Ngày kết thúc học kỳ < Ngày hiện tại → Chỉ đọc
```

### 4. Validate ngày tháng

- Ngày kết thúc phải **SAU** ngày bắt đầu
- Ngày bắt đầu nên nằm trong khoảng học kỳ

---

## Xử lý lỗi

### Lỗi thường gặp

1. **"Giáo viên không có chuyên môn phù hợp"**

   - ➡️ Kiểm tra bảng `GiaoVienChuyenMon` hoặc `GiaoVien_MonHoc`
   - ➡️ Thêm chuyên môn cho GV nếu cần

2. **"Phân công này đã tồn tại (trùng Lớp-Môn-Học kỳ)"**

   - ➡️ Kiểm tra danh sách phân công hiện có
   - ➡️ Sửa phân công cũ thay vì thêm mới

3. **"Không thể sửa phân công cho học kỳ đã kết thúc"**

   - ➡️ Chỉ có thể xem, không thể thay đổi
   - ➡️ Liên hệ admin nếu thực sự cần sửa

4. **"Ngày kết thúc phải sau ngày bắt đầu"**
   - ➡️ Kiểm tra lại ngày tháng đã chọn

---

## Tips & Tricks

### 💡 Mẹo sử dụng hiệu quả

1. **Phân công theo từng học kỳ**

   - Nên lọc theo học kỳ trước khi làm việc
   - Tránh nhầm lẫn giữa các học kỳ

2. **Sử dụng Auto Generate**

   - Dùng cho phân công hàng loạt
   - Tiết kiệm thời gian đáng kể
   - Đảm bảo cân bằng tải

3. **Kiểm tra trước khi lưu**

   - Nhấn "Kiểm tra" trong Auto Generate
   - Xem báo cáo vi phạm (nếu có)

4. **Lưu tạm trước khi chấp nhận**
   - Cho phép xem lại và chỉnh sửa
   - An toàn hơn khi làm việc với nhiều bản ghi

---

## Liên hệ hỗ trợ

Nếu gặp vấn đề, vui lòng liên hệ:

- 📧 Email: support@school.edu.vn
- ☎️ Hotline: 1900-xxxx
- 🌐 Tài liệu: https://docs.school.edu.vn

---

**Phiên bản:** 1.0  
**Cập nhật:** 28/10/2025  
**Tác giả:** Dev Team
