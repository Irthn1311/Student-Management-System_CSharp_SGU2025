# HƯỚNG DẪN TẠO FILE EXCEL MẪU CHO PHÂN LỚP CHUYỂN TRƯỜNG

## YÊU CẦU TẠO FILE EXCEL

Tạo một file Excel với **6 worksheet** có tên chính xác như sau:

1. **"HocSinh"** - Thông tin học sinh
2. **"PhuHuynh"** - Thông tin phụ huynh
3. **"MoiQuanHe"** - Mối quan hệ giữa học sinh và phụ huynh
4. **"Diem"** - Điểm số các học kỳ trước
5. **"HanhKiem"** - Hạnh kiểm các học kỳ trước
6. **"XepLoai"** - Xếp loại học lực các học kỳ trước

---

## 1. WORKSHEET "HocSinh"

### Cấu trúc cột (từ A đến J):

| Cột | Tên cột | Mô tả | Bắt buộc | Ví dụ |
|-----|---------|-------|----------|-------|
| A | Mã HS | Mã học sinh (có thể để trống) | Không | 1, 2, 3... |
| B | Họ và tên | Họ và tên đầy đủ | **Có** | Nguyễn Văn A |
| C | Ngày sinh | dd/MM/yyyy | **Có** | 15/05/2008 |
| D | Giới tính | "Nam" hoặc "Nữ" | **Có** | Nam |
| E | SĐT | Số điện thoại (chỉ số) | Không | 0123456789 |
| F | Email | Địa chỉ email | Không | email@example.com |
| G | Trạng thái | (Có thể để trống, hệ thống sẽ tự động đặt "Đang học") | Không | (để trống) |
| H | Khối | "10", "11", hoặc "12" | **Có** | 10 |
| I | Ngày chuyển vào | dd/MM/yyyy | **Có** | 01/09/2024 |
| J | Nguyện vọng chuyển lớp | Tên lớp (có thể để trống) | Không | 10A1 |

**Lưu ý quan trọng**: Học sinh được nhập từ Excel này sẽ có trạng thái **"Đang học"** sau khi nhập thành công, không phải "Chuyển trường".

### Dòng đầu tiên (Header):
```
A1: Mã HS
B1: Họ và tên
C1: Ngày sinh
D1: Giới tính
E1: SĐT
F1: Email
G1: Trạng thái
H1: Khối
I1: Ngày chuyển vào
J1: Nguyện vọng chuyển lớp
```

### Ví dụ dữ liệu (từ dòng 2):
```
A2: (để trống)
B2: Nguyễn Văn A
C2: 15/05/2008
D2: Nam
E2: 0123456789
F2: nguyenvana@email.com
G2: Chuyển trường
H2: 10
I2: 01/09/2024
J2: 10A1
```

---

## 2. WORKSHEET "PhuHuynh"

### Cấu trúc cột (từ A đến D):

| Cột | Tên cột | Mô tả | Bắt buộc | Ví dụ |
|-----|---------|-------|----------|-------|
| A | Họ và tên | Họ và tên đầy đủ của phụ huynh | **Có** | Nguyễn Văn B |
| B | SĐT | Số điện thoại (chỉ số) | **Có** | 0123456789 |
| C | Email | Địa chỉ email | Không | email@example.com |
| D | Địa chỉ | Địa chỉ của phụ huynh | Không | 123 Đường ABC |

### Dòng đầu tiên (Header):
```
A1: Họ và tên
B1: SĐT
C1: Email
D1: Địa chỉ
```

### Ví dụ dữ liệu (từ dòng 2):
```
A2: Nguyễn Văn B
B2: 0987654321
C2: nguyenvanb@email.com
D2: 456 Đường XYZ
```

**Lưu ý**: 
- SĐT và Email không được trùng với dữ liệu đã có trong hệ thống hoặc trong cùng file Excel.
- Nếu phụ huynh đã tồn tại (theo SĐT hoặc Email), hệ thống sẽ sử dụng phụ huynh đã có.

---

## 3. WORKSHEET "MoiQuanHe"

### Cấu trúc cột (từ A đến C):

| Cột | Tên cột | Mô tả | Bắt buộc | Ví dụ |
|-----|---------|-------|----------|-------|
| A | Họ và tên HS | Họ và tên học sinh (khớp với HocSinh) | **Có** | Nguyễn Văn A |
| B | Họ và tên PH | Họ và tên phụ huynh (khớp với PhuHuynh) | **Có** | Nguyễn Văn B |
| C | Mối quan hệ | "Cha", "Mẹ", "Ông", "Bà", "Người giám hộ" | **Có** | Cha |

### Dòng đầu tiên (Header):
```
A1: Họ và tên HS
B1: Họ và tên PH
C1: Mối quan hệ
```

### Ví dụ dữ liệu (từ dòng 2):
```
A2: Nguyễn Văn A
B2: Nguyễn Văn B
C2: Cha
```

**Lưu ý**: 
- Họ và tên học sinh và phụ huynh phải khớp chính xác với dữ liệu trong worksheet "HocSinh" và "PhuHuynh".
- Mối quan hệ phải là một trong các giá trị: "Cha", "Mẹ", "Ông", "Bà", "Người giám hộ".

---

## 4. WORKSHEET "Diem"

### Cấu trúc cột (từ A đến I):

| Cột | Tên cột | Mô tả | Bắt buộc | Ví dụ |
|-----|---------|-------|----------|-------|
| A | Mã HS | Mã học sinh (khớp với HocSinh) | **Có** | 1, 2, 3... |
| B | Họ và tên | Họ và tên (để đối chiếu) | **Có** | Nguyễn Văn A |
| C | Mã học kỳ | Mã học kỳ | **Có** | 1, 2, 3... |
| D | Mã môn học | Mã môn học | **Có** | 1, 2, 3... |
| E | Tên môn học | Tên môn học | **Có** | Toán, Văn, Anh... |
| F | Điểm thường xuyên | 0-10 | **Có** | 8.5 |
| G | Điểm giữa kỳ | 0-10 | **Có** | 7.0 |
| H | Điểm cuối kỳ | 0-10 | **Có** | 9.0 |
| I | Điểm trung bình | 0-10 | **Có** | 8.2 |

### Dòng đầu tiên (Header):
```
A1: Mã HS
B1: Họ và tên
C1: Mã học kỳ
D1: Mã môn học
E1: Tên môn học
F1: Điểm thường xuyên
G1: Điểm giữa kỳ
H1: Điểm cuối kỳ
I1: Điểm trung bình
```

### Ví dụ dữ liệu (từ dòng 2):
```
A2: 1
B2: Nguyễn Văn A
C2: 1
D2: 1
E2: Toán
F2: 8.5
G2: 7.0
H2: 9.0
I2: 8.2
```

**Lưu ý**: Mỗi học sinh phải có điểm của **TẤT CẢ** môn học trong **TẤT CẢ** học kỳ trước học kỳ hiện tại.

---

## 5. WORKSHEET "HanhKiem"

### Cấu trúc cột (từ A đến E):

| Cột | Tên cột | Mô tả | Bắt buộc | Ví dụ |
|-----|---------|-------|----------|-------|
| A | Mã HS | Mã học sinh (khớp với HocSinh) | **Có** | 1, 2, 3... |
| B | Họ và tên | Họ và tên (để đối chiếu) | **Có** | Nguyễn Văn A |
| C | Mã học kỳ | Mã học kỳ | **Có** | 1, 2, 3... |
| D | Xếp loại | "Tốt", "Khá", "Trung bình", "Yếu" | **Có** | Tốt |
| E | Nhận xét | Nhận xét (có thể để trống) | Không | Học sinh ngoan... |

### Dòng đầu tiên (Header):
```
A1: Mã HS
B1: Họ và tên
C1: Mã học kỳ
D1: Xếp loại
E1: Nhận xét
```

### Ví dụ dữ liệu (từ dòng 2):
```
A2: 1
B2: Nguyễn Văn A
C2: 1
D2: Tốt
E2: Học sinh ngoan, chăm chỉ
```

**Lưu ý**: Mỗi học sinh phải có hạnh kiểm của **TẤT CẢ** học kỳ trước học kỳ hiện tại.

---

## 6. WORKSHEET "XepLoai"

### Cấu trúc cột (từ A đến E):

| Cột | Tên cột | Mô tả | Bắt buộc | Ví dụ |
|-----|---------|-------|----------|-------|
| A | Mã HS | Mã học sinh (khớp với HocSinh) | **Có** | 1, 2, 3... |
| B | Họ và tên | Họ và tên (để đối chiếu) | **Có** | Nguyễn Văn A |
| C | Mã học kỳ | Mã học kỳ | **Có** | 1, 2, 3... |
| D | Học lực | "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" | **Có** | Khá |
| E | Ghi chú | Ghi chú (có thể để trống) | Không | Học sinh có tiến bộ... |

### Dòng đầu tiên (Header):
```
A1: Mã HS
B1: Họ và tên
C1: Mã học kỳ
D1: Học lực
E1: Ghi chú
```

### Ví dụ dữ liệu (từ dòng 2):
```
A2: 1
B2: Nguyễn Văn A
C2: 1
D2: Khá
E2: Học sinh có tiến bộ
```

**Lưu ý quan trọng**: 
- Mỗi học sinh phải có xếp loại của **TẤT CẢ** học kỳ trước học kỳ hiện tại.
- Học lực **KHÔNG ĐƯỢC** là "Yếu" hoặc "Kém" ở bất kỳ học kỳ nào.

---

## PROMPT ĐỂ TẠO FILE EXCEL (Dành cho AI khác)

```
Tạo một file Excel với 6 worksheet có tên chính xác: "HocSinh", "PhuHuynh", "MoiQuanHe", "Diem", "HanhKiem", "XepLoai".

### Worksheet "HocSinh":
- Dòng 1 (Header): Mã HS | Họ và tên | Ngày sinh | Giới tính | SĐT | Email | Trạng thái | Khối | Ngày chuyển vào | Nguyện vọng chuyển lớp
- Từ dòng 2: Tạo 5 học sinh mẫu với:
  - Trạng thái: (có thể để trống, hệ thống sẽ tự động đặt "Đang học")
  - Khối: "10", "11", hoặc "12"
  - Ngày chuyển vào: dd/MM/yyyy (phải trước 1/3 thời gian học kỳ)
  - Nguyện vọng: có thể để trống hoặc tên lớp (ví dụ: "10A1")

### Worksheet "PhuHuynh":
- Dòng 1 (Header): Họ và tên | SĐT | Email | Địa chỉ
- Từ dòng 2: Tạo 5 phụ huynh mẫu tương ứng với 5 học sinh ở trên
- SĐT và Email không được trùng

### Worksheet "MoiQuanHe":
- Dòng 1 (Header): Họ và tên HS | Họ và tên PH | Mối quan hệ
- Từ dòng 2: Tạo mối quan hệ giữa học sinh và phụ huynh
- Mối quan hệ: "Cha", "Mẹ", "Ông", "Bà", hoặc "Người giám hộ"
- Họ và tên phải khớp chính xác với worksheet "HocSinh" và "PhuHuynh"

### Worksheet "Diem":
- Dòng 1 (Header): Mã HS | Họ và tên | Mã học kỳ | Mã môn học | Tên môn học | Điểm thường xuyên | Điểm giữa kỳ | Điểm cuối kỳ | Điểm trung bình
- Tạo điểm cho 5 học sinh ở trên, mỗi học sinh có điểm của ít nhất 3 môn học (Toán, Văn, Anh) trong ít nhất 1 học kỳ trước.
- Điểm từ 0-10.

### Worksheet "HanhKiem":
- Dòng 1 (Header): Mã HS | Họ và tên | Mã học kỳ | Xếp loại | Nhận xét
- Tạo hạnh kiểm cho 5 học sinh ở trên, mỗi học sinh có hạnh kiểm của ít nhất 1 học kỳ trước.
- Xếp loại: "Tốt", "Khá", "Trung bình", hoặc "Yếu"

### Worksheet "XepLoai":
- Dòng 1 (Header): Mã HS | Họ và tên | Mã học kỳ | Học lực | Ghi chú
- Tạo xếp loại cho 5 học sinh ở trên, mỗi học sinh có xếp loại của ít nhất 1 học kỳ trước.
- Học lực: "Giỏi", "Khá", "Trung bình" (KHÔNG được "Yếu" hoặc "Kém")

Lưu ý:
- Mã HS trong các worksheet phải khớp với nhau
- Họ và tên trong các worksheet phải khớp với nhau
- Tất cả dữ liệu phải hợp lệ theo yêu cầu
```

---

## KIỂM TRA FILE EXCEL

Sau khi tạo file Excel, kiểm tra:

1. ✅ File có đúng 6 worksheet với tên chính xác
2. ✅ Dòng đầu tiên là header (tên cột)
3. ✅ Dữ liệu từ dòng 2 trở đi
4. ✅ Khối lớp phải là "10", "11", hoặc "12"
5. ✅ Ngày chuyển vào phải hợp lệ (dd/MM/yyyy)
6. ✅ SĐT và Email không trùng trong file Excel
7. ✅ Mối quan hệ: "Cha", "Mẹ", "Ông", "Bà", hoặc "Người giám hộ"
8. ✅ Họ và tên khớp giữa các worksheet (HocSinh, PhuHuynh, MoiQuanHe)
9. ✅ Điểm từ 0-10
10. ✅ Hạnh kiểm: "Tốt", "Khá", "Trung bình", "Yếu"
11. ✅ Học lực: "Giỏi", "Khá", "Trung bình" (KHÔNG "Yếu" hoặc "Kém")
12. ✅ Mã HS và Họ tên khớp giữa các worksheet

---

**Ngày tạo**: [Ngày hiện tại]  
**Phiên bản**: 1.0  
**Tác giả**: Hệ thống Quản lý Học sinh

