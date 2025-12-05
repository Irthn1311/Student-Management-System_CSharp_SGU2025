# ĐIỀU KIỆN PHÂN LỚP CHO HỌC SINH CHUYỂN TRƯỜNG

## Tổng quan

Tài liệu này mô tả các điều kiện cần thiết để một học sinh có trạng thái "Chuyển trường" có thể được phân lớp vào học kỳ hiện tại trong hệ thống quản lý học sinh.

---

## 1. ĐIỀU KIỆN VỀ HỌC SINH

### 1.1. Trạng thái học sinh

- **Bắt buộc**: Học sinh phải có trạng thái **"Chuyển trường"**
- **Kiểm tra**: Lấy từ bảng `HocSinh`, cột `TrangThai`
- **Lỗi nếu**: Trạng thái khác "Chuyển trường" (ví dụ: "Đang học", "Nghỉ học", "Bảo lưu", "Thôi học")

### 1.2. Thông tin học sinh

- **Mã học sinh (MaHS)**: Phải tồn tại trong hệ thống
- **Họ và tên**: Phải có dữ liệu
- **Ngày sinh**: Phải hợp lệ
- **Giới tính**: Phải là "Nam" hoặc "Nữ"
- **Số điện thoại**: Có thể để trống hoặc phải đúng định dạng
- **Email**: Có thể để trống hoặc phải đúng định dạng

### 1.3. Tài khoản học sinh

- Học sinh phải có tài khoản đăng nhập trong hệ thống (tự động tạo khi thêm học sinh)
- Tên đăng nhập: `HS{MaHS}` (ví dụ: HS001, HS002)

---

## 2. ĐIỀU KIỆN VỀ HỌC KỲ

### 2.1. Trạng thái học kỳ

- **Bắt buộc**: Học kỳ phải có trạng thái **"Đang diễn ra"**
- **Cách xác định**:
  - Lấy từ bảng `HocKy`
  - Kiểm tra: `NgayBD <= NgayHienTai <= NgayKT`
  - Hoặc sử dụng `SemesterHelper.GetStatus(maHocKy)` trả về "Đang diễn ra"

### 2.2. Thông tin học kỳ

- **Mã học kỳ (MaHocKy)**: Phải tồn tại trong hệ thống
- **Tên học kỳ**: Phải có dữ liệu
- **Mã năm học (MaNamHoc)**: Phải tồn tại và liên kết với bảng `NamHoc`
- **Ngày bắt đầu (NgayBD)**: Phải hợp lệ
- **Ngày kết thúc (NgayKT)**: Phải hợp lệ và >= NgayBD

### 2.3. Phân lớp trong học kỳ

- **Điều kiện**: Học sinh **CHƯA** được phân lớp trong học kỳ này
- **Kiểm tra**:
  - Truy vấn bảng `PhanLop`
  - Điều kiện: `MaHocSinh = {maHS} AND MaHocKy = {maHK}`
  - Nếu đã tồn tại → **KHÔNG THỂ** phân lớp lại

---

## 3. ĐIỀU KIỆN VỀ LỚP HỌC

### 3.1. Lớp học tồn tại

- **Mã lớp (MaLop)**: Phải tồn tại trong hệ thống
- **Tên lớp**: Phải có dữ liệu
- **Mã khối (MaKhoi)**: Phải tồn tại và liên kết với bảng `Khoi`

### 3.2. Sĩ số lớp

- **Sĩ số hiện tại**: Số học sinh đã được phân vào lớp trong học kỳ hiện tại
- **Sĩ số tối đa**: Lấy từ bảng `LopHoc`, cột `SiSo` (mặc định: 40 học sinh)
- **Điều kiện**: `Sĩ số hiện tại < Sĩ số tối đa`
- **Lỗi nếu**: Lớp đã đầy (sĩ số hiện tại >= sĩ số tối đa)

### 3.3. Giáo viên chủ nhiệm

- **Mã giáo viên chủ nhiệm (MaGiaoVienChuNhiem)**: Có thể để trống hoặc phải tồn tại trong hệ thống
- **Không bắt buộc** cho việc phân lớp

---

## 4. ĐIỀU KIỆN VỀ DỮ LIỆU NHẬP EXCEL

### 4.1. Cấu trúc file Excel

File Excel phải có **ít nhất 6 worksheet** với các tên sau:

1. **"HocSinh"** - Thông tin học sinh (bắt buộc)
2. **"PhuHuynh"** - Thông tin phụ huynh (bắt buộc)
3. **"MoiQuanHe"** - Mối quan hệ giữa học sinh và phụ huynh (bắt buộc)
4. **"Diem"** - Điểm số các học kỳ trước (bắt buộc)
5. **"HanhKiem"** - Hạnh kiểm các học kỳ trước (bắt buộc)
6. **"XepLoai"** - Xếp loại học lực các học kỳ trước (bắt buộc)

**Lưu ý**: Nếu không tìm thấy worksheet theo tên, hệ thống sẽ sử dụng worksheet theo thứ tự (0, 1, 2, 3, 4, 5).

### 4.2. Cấu trúc bảng "HocSinh"

Bảng phải có các cột sau (theo thứ tự):

| Cột | Tên cột                | Mô tả                                       | Bắt buộc | Ví dụ             |
| --- | ---------------------- | ------------------------------------------- | -------- | ----------------- |
| A   | Mã HS                  | Mã học sinh (tự động tạo, có thể để trống)  | Không    | 1, 2, 3...        |
| B   | Họ và tên              | Họ và tên đầy đủ của học sinh               | **Có**   | Nguyễn Văn A      |
| C   | Ngày sinh              | Ngày sinh (dd/MM/yyyy hoặc định dạng Excel) | **Có**   | 15/05/2008        |
| D   | Giới tính              | "Nam" hoặc "Nữ"                             | **Có**   | Nam               |
| E   | SĐT                    | Số điện thoại (chỉ chứa số)                 | Không    | 0123456789        |
| F   | Email                  | Địa chỉ email hợp lệ                        | Không    | email@example.com |
| G   | Trạng thái             | Phải là "Chuyển trường"                     | **Có**   | Chuyển trường     |
| H   | Khối                   | Tên khối lớp (10, 11, 12)                   | **Có**   | 10                |
| I   | Ngày chuyển vào        | Ngày chuyển vào trường (dd/MM/yyyy)         | **Có**   | 01/09/2024        |
| J   | Nguyện vọng chuyển lớp | Tên lớp mong muốn (có thể để trống)         | Không    | 10A1              |

**Lưu ý quan trọng**: Học sinh được nhập từ Excel này sẽ có trạng thái **"Đang học"** sau khi nhập thành công, không phải "Chuyển trường".

### 4.3. Cấu trúc bảng "PhuHuynh"

Bảng phải có các cột sau (theo thứ tự):

| Cột | Tên cột   | Mô tả                          | Bắt buộc | Ví dụ             |
| --- | --------- | ------------------------------ | -------- | ----------------- |
| A   | Họ và tên | Họ và tên đầy đủ của phụ huynh | **Có**   | Nguyễn Văn B      |
| B   | SĐT       | Số điện thoại (chỉ chứa số)    | **Có**   | 0123456789        |
| C   | Email     | Địa chỉ email hợp lệ           | Không    | email@example.com |
| D   | Địa chỉ   | Địa chỉ của phụ huynh          | Không    | 123 Đường ABC     |

**Lưu ý**:

- SĐT và Email không được trùng với dữ liệu đã có trong hệ thống hoặc trong cùng file Excel.
- Nếu phụ huynh đã tồn tại (theo SĐT hoặc Email), hệ thống sẽ sử dụng phụ huynh đã có.

### 4.4. Cấu trúc bảng "MoiQuanHe"

Bảng phải có các cột sau (theo thứ tự):

| Cột | Tên cột      | Mô tả                                        | Bắt buộc | Ví dụ        |
| --- | ------------ | -------------------------------------------- | -------- | ------------ |
| A   | Họ và tên HS | Họ và tên học sinh (phải khớp với HocSinh)   | **Có**   | Nguyễn Văn A |
| B   | Họ và tên PH | Họ và tên phụ huynh (phải khớp với PhuHuynh) | **Có**   | Nguyễn Văn B |
| C   | Mối quan hệ  | "Cha", "Mẹ", "Ông", "Bà", "Người giám hộ"    | **Có**   | Cha          |

**Lưu ý**:

- Họ và tên học sinh và phụ huynh phải khớp chính xác với dữ liệu trong worksheet "HocSinh" và "PhuHuynh".
- Mối quan hệ phải là một trong các giá trị: "Cha", "Mẹ", "Ông", "Bà", "Người giám hộ".

### 4.5. Dòng đầu tiên

- Dòng 1: **Header** (tên các cột) - không được tính vào dữ liệu
- Dòng 2 trở đi: Dữ liệu học sinh

### 4.4. Validation dữ liệu Excel

#### 4.4.1. Trạng thái

- **Bắt buộc**: Cột G (Trạng thái) phải là **"Chuyển trường"** (chính xác, không phân biệt hoa thường)
- **Bỏ qua**: Nếu trạng thái khác "Chuyển trường" → không nhập học sinh đó

#### 4.4.2. Họ và tên

- **Bắt buộc**: Không được để trống
- **Định dạng**: Không được chứa số

#### 4.4.3. Ngày sinh

- **Bắt buộc**: Không được để trống
- **Định dạng chấp nhận**:
  - `dd/MM/yyyy` (ví dụ: 15/05/2008)
  - `d/M/yyyy` (ví dụ: 5/5/2008)
  - `dd-MM-yyyy` (ví dụ: 15-05-2008)
  - `yyyy-MM-dd` (ví dụ: 2008-05-15)
  - Định dạng số serial của Excel (tự động chuyển đổi)

#### 4.4.4. Giới tính

- **Bắt buộc**: Phải là "Nam" hoặc "Nữ" (chính xác)
- **Lỗi nếu**: Giá trị khác

#### 4.4.5. Số điện thoại

- **Không bắt buộc**: Có thể để trống
- **Định dạng**: Chỉ chứa số (0-9)
- **Độ dài**: 10-11 số
- **Kiểm tra trùng**: Không được trùng với SĐT đã có trong hệ thống hoặc trong cùng file Excel

#### 4.4.6. Email

- **Không bắt buộc**: Có thể để trống
- **Định dạng**: Phải đúng định dạng email (có @ và domain)
- **Kiểm tra trùng**: Không được trùng với Email đã có trong hệ thống hoặc trong cùng file Excel

#### 4.4.7. Khối

- **Bắt buộc**: Phải có dữ liệu
- **Giá trị hợp lệ**: "10", "11", "12" (hoặc tên khối tương ứng trong hệ thống)
- **Mục đích**: Xác định khối lớp để phân lớp tự động vào lớp cùng khối

#### 4.4.8. Ngày chuyển vào

- **Bắt buộc**: Phải có dữ liệu
- **Định dạng**: `dd/MM/yyyy` hoặc định dạng Excel
- **Điều kiện**: Phải **TRƯỚC** 1/3 thời gian học kỳ hiện tại
  - Công thức: `NgayChuyenVao < (NgayBD + (NgayKT - NgayBD) / 3)`
  - Ví dụ: Học kỳ từ 01/09/2024 đến 31/12/2024 → Ngày chuyển vào phải < 01/10/2024 (khoảng 1/3 thời gian)
- **Lỗi nếu**: Ngày chuyển vào >= 1/3 thời gian học kỳ

#### 4.4.9. Nguyện vọng chuyển lớp

- **Không bắt buộc**: Có thể để trống
- **Định dạng**: Tên lớp (ví dụ: "10A1", "11B2", "12C3")
- **Điều kiện**:
  - Nếu **có dữ liệu**: Lớp phải cùng khối với khối đã chọn ở cột H
  - Nếu **có dữ liệu**: Kiểm tra sĩ số lớp đó còn dư không
    - Nếu lớp còn chỗ (sĩ số hiện tại < sĩ số tối đa) → Phân vào lớp nguyện vọng
    - Nếu lớp đã đầy → Tự động phân lớp vào lớp khác cùng khối
  - Nếu **trống**: Tự động phân lớp vào lớp cùng khối còn thiếu sĩ số

### 4.6. Cấu trúc bảng "Diem"

Bảng chứa điểm số của học sinh ở các học kỳ cần thiết (theo khối).

| Cột | Tên cột           | Mô tả                                                | Bắt buộc | Ví dụ             |
| --- | ----------------- | ---------------------------------------------------- | -------- | ----------------- |
| A   | Họ và tên         | Họ và tên học sinh (để đối chiếu)                    | **Có**   | Nguyễn Văn A      |
| B   | Mã học kỳ         | Mã học kỳ (phải khớp với database)                   | **Có**   | 1, 2, 3...        |
| C   | Mã môn học        | Mã môn học                                           | **Có**   | 1, 2, 3...        |
| D   | Tên môn học       | Tên môn học (để đối chiếu)                           | **Có**   | Toán, Văn, Anh... |
| E   | Điểm thường xuyên | Điểm thường xuyên (0-10)                             | **Có**   | 8.5               |
| F   | Điểm giữa kỳ      | Điểm giữa kỳ (0-10)                                  | **Có**   | 7.0               |
| G   | Điểm cuối kỳ      | Điểm cuối kỳ (0-10)                                  | **Có**   | 9.0               |
| H   | Điểm trung bình   | Điểm trung bình (tự động tính hoặc nhập)             | **Có**   | 8.2               |

**Lưu ý QUAN TRỌNG**:

- **Logic điểm số**: Nếu học sinh chuyển vào khối X, học kỳ Y
  - Cần điểm của: **Tất cả học kỳ của khối X-1** + **Tất cả học kỳ của khối X trước học kỳ Y**
  - Ví dụ: Khối 11, HK2 → cần điểm HK1, HK2 của năm học trước (khối 10) + HK1 của năm học hiện tại (khối 11)
- Phải có điểm của **TẤT CẢ** các học kỳ cần thiết (theo khối)
- Phải có điểm của **TẤT CẢ** các môn học bắt buộc (13 môn)
- Điểm phải trong khoảng 0-10
- Mỗi học sinh có thể có nhiều dòng (mỗi dòng = 1 môn học trong 1 học kỳ)
- Dữ liệu phải khớp với học sinh trong worksheet "HocSinh" (Họ tên)

#### 4.5.1. Validation bảng "Diem"

- **Họ và tên**: Phải khớp với Họ và tên trong worksheet "HocSinh" (để đối chiếu)
- **Mã học kỳ**: Phải là học kỳ cần thiết theo logic:
  - Nếu học sinh chuyển vào khối X, học kỳ Y
  - Cần điểm của: Tất cả học kỳ của khối X-1 + Tất cả học kỳ của khối X trước học kỳ Y
  - Ví dụ: Khối 11, HK2 → cần điểm HK1, HK2 của năm học trước (khối 10) + HK1 của năm học hiện tại (khối 11)
- **Mã môn học**: Phải tồn tại trong hệ thống
- **Tên môn học**: Phải khớp với Mã môn học (để đối chiếu)
- **Điểm thường xuyên**: Phải trong khoảng 0-10 (có thể là số thập phân)
- **Điểm giữa kỳ**: Phải trong khoảng 0-10 (có thể là số thập phân)
- **Điểm cuối kỳ**: Phải trong khoảng 0-10 (có thể là số thập phân)
- **Điểm trung bình**: Phải trong khoảng 0-10 (có thể tự động tính hoặc nhập thủ công)
- **Kiểm tra đầy đủ**: Mỗi học sinh phải có điểm của **TẤT CẢ** môn học (13 môn) trong **TẤT CẢ** học kỳ cần thiết (theo khối)

### 4.7. Cấu trúc bảng "HanhKiem"

Bảng chứa hạnh kiểm của học sinh ở các học kỳ cần thiết (theo khối).

| Cột | Tên cột   | Mô tả                                                | Bắt buộc | Ví dụ                       |
| --- | --------- | ---------------------------------------------------- | -------- | --------------------------- |
| A   | Họ và tên | Họ và tên học sinh (để đối chiếu)                    | **Có**   | Nguyễn Văn A                |
| B   | Mã học kỳ | Mã học kỳ (phải khớp với database)                   | **Có**   | 1, 2, 3...                  |
| C   | Xếp loại  | Xếp loại hạnh kiểm                                   | **Có**   | Tốt, Khá, Trung bình, Yếu   |
| D   | Nhận xét  | Nhận xét về hạnh kiểm                                | Không    | Học sinh ngoan, chăm chỉ... |

**Lưu ý**:

- **Logic**: Cần hạnh kiểm của **TẤT CẢ** các học kỳ cần thiết (theo khối)
  - Nếu học sinh chuyển vào khối X, học kỳ Y
  - Cần hạnh kiểm của: Tất cả học kỳ của khối X-1 + Tất cả học kỳ của khối X trước học kỳ Y
- Xếp loại hợp lệ: "Tốt", "Khá", "Trung bình", "Yếu"
- Mỗi học sinh có thể có nhiều dòng (mỗi dòng = 1 học kỳ)
- Dữ liệu phải khớp với học sinh trong worksheet "HocSinh" (Họ tên)

#### 4.6.1. Validation bảng "HanhKiem"

- **Họ và tên**: Phải khớp với Họ và tên trong worksheet "HocSinh" (để đối chiếu)
- **Mã học kỳ**: Phải là học kỳ cần thiết theo logic:
  - Nếu học sinh chuyển vào khối X, học kỳ Y
  - Cần hạnh kiểm của: Tất cả học kỳ của khối X-1 + Tất cả học kỳ của khối X trước học kỳ Y
- **Xếp loại**: Phải là một trong các giá trị: "Tốt", "Khá", "Trung bình", "Yếu" (chính xác)
- **Nhận xét**: Có thể để trống hoặc có nội dung
- **Kiểm tra đầy đủ**: Mỗi học sinh phải có hạnh kiểm của **TẤT CẢ** học kỳ cần thiết (theo khối)
- **Không trùng lặp**: Mỗi học sinh chỉ có 1 hạnh kiểm cho mỗi học kỳ

### 4.8. Cấu trúc bảng "XepLoai"

Bảng chứa xếp loại học lực của học sinh ở các học kỳ cần thiết (theo khối).

| Cột | Tên cột   | Mô tả                                                | Bắt buộc | Ví dụ                           |
| --- | --------- | ---------------------------------------------------- | -------- | ------------------------------- |
| A   | Họ và tên | Họ và tên học sinh (để đối chiếu)                    | **Có**   | Nguyễn Văn A                    |
| B   | Mã học kỳ | Mã học kỳ (phải khớp với database)                   | **Có**   | 1, 2, 3...                      |
| C   | Học lực   | Học lực                                              | **Có**   | Giỏi, Khá, Trung bình, Yếu, Kém |
| D   | Ghi chú   | Ghi chú về học lực                                   | Không    | Học sinh có tiến bộ...          |

**Lưu ý**:

- **Logic**: Cần xếp loại của **TẤT CẢ** các học kỳ cần thiết (theo khối)
  - Nếu học sinh chuyển vào khối X, học kỳ Y
  - Cần xếp loại của: Tất cả học kỳ của khối X-1 + Tất cả học kỳ của khối X trước học kỳ Y
- Học lực hợp lệ: "Giỏi", "Khá", "Trung bình", "Yếu", "Kém"
- **ĐIỀU KIỆN QUAN TRỌNG**: Học lực **KHÔNG ĐƯỢC** là "Yếu" hoặc "Kém" ở bất kỳ học kỳ nào
- Mỗi học sinh có thể có nhiều dòng (mỗi dòng = 1 học kỳ)
- Dữ liệu phải khớp với học sinh trong worksheet "HocSinh" (Họ tên)

#### 4.7.1. Validation bảng "XepLoai"

- **Họ và tên**: Phải khớp với Họ và tên trong worksheet "HocSinh" (để đối chiếu)
- **Mã học kỳ**: Phải là học kỳ cần thiết theo logic:
  - Nếu học sinh chuyển vào khối X, học kỳ Y
  - Cần xếp loại của: Tất cả học kỳ của khối X-1 + Tất cả học kỳ của khối X trước học kỳ Y
- **Học lực**: Phải là một trong các giá trị: "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" (chính xác)
- **Ghi chú**: Có thể để trống hoặc có nội dung
- **Kiểm tra đầy đủ**: Mỗi học sinh phải có xếp loại của **TẤT CẢ** học kỳ cần thiết (theo khối)
- **Không trùng lặp**: Mỗi học sinh chỉ có 1 xếp loại cho mỗi học kỳ
- **ĐIỀU KIỆN BẮT BUỘC**: Học lực **KHÔNG ĐƯỢC** là "Yếu" hoặc "Kém" ở bất kỳ học kỳ nào
  - Nếu có học kỳ nào có học lực "Yếu" hoặc "Kém" → **KHÔNG THỂ** phân lớp

---

## 5. QUY TRÌNH KIỂM TRA ĐIỀU KIỆN

### 5.1. Bước 1: Kiểm tra học sinh

1. ✅ Học sinh có trạng thái "Chuyển trường"?
2. ✅ Học sinh tồn tại trong hệ thống?
3. ✅ Thông tin học sinh đầy đủ và hợp lệ?
4. ✅ Khối lớp đã được chỉ định?

### 5.2. Bước 2: Kiểm tra học kỳ

1. ✅ Học kỳ có trạng thái "Đang diễn ra"?
2. ✅ Học kỳ tồn tại trong hệ thống?
3. ✅ Học sinh chưa được phân lớp trong học kỳ này?
4. ✅ Ngày chuyển vào < 1/3 thời gian học kỳ?

### 5.3. Bước 3: Kiểm tra điểm, hạnh kiểm, xếp loại (nếu nhập từ Excel)

1. ✅ Có đầy đủ điểm của tất cả học kỳ trước?
2. ✅ Có đầy đủ hạnh kiểm của tất cả học kỳ trước?
3. ✅ Có đầy đủ xếp loại của tất cả học kỳ trước?
4. ✅ Xếp loại học lực không có "Yếu" hoặc "Kém" ở bất kỳ học kỳ nào?

### 5.4. Bước 4: Kiểm tra lớp học

1. ✅ Lớp học tồn tại trong hệ thống?
2. ✅ Lớp học cùng khối với khối đã chọn?
3. ✅ Lớp học chưa đầy (sĩ số hiện tại < sĩ số tối đa)?
4. ✅ Nếu có nguyện vọng:
   - Lớp nguyện vọng cùng khối?
   - Lớp nguyện vọng còn chỗ?
   - Nếu không còn chỗ → Tự động phân lớp vào lớp khác cùng khối

### 5.5. Bước 5: Kiểm tra dữ liệu Excel (nếu nhập từ Excel)

1. ✅ File Excel có đúng cấu trúc (6 worksheet: HocSinh, PhuHuynh, MoiQuanHe, Diem, HanhKiem, XepLoai)?
2. ✅ Dữ liệu trong Excel hợp lệ?
3. ✅ Không có trùng lặp SĐT/Email trong file Excel?
4. ✅ Không có trùng lặp SĐT/Email với dữ liệu trong hệ thống?
5. ✅ Dữ liệu giữa các worksheet khớp với nhau (Mã HS, Họ tên)?
6. ✅ Học sinh, phụ huynh và mối quan hệ được nhập thành công (atomic transaction)?

---

## 6. THÔNG BÁO KẾT QUẢ

### 6.1. Hiển thị trong txtKetQua

Sau khi kiểm tra điều kiện, hệ thống sẽ hiển thị kết quả trong `txtKetQua` với các trạng thái:

#### 6.1.1. Thành công

```
✅ ĐIỀU KIỆN ĐẠT: Học sinh có thể được phân lớp
- Học sinh: [Họ và tên] (MaHS: [Mã])
- Học kỳ: [Tên học kỳ] - [Năm học]
- Lớp: [Tên lớp]
- Sĩ số hiện tại: [X]/[Y] học sinh
```

#### 6.1.2. Lỗi - Học sinh

```
❌ LỖI: Học sinh không đủ điều kiện
- Học sinh: [Họ và tên] (MaHS: [Mã])
- Lý do: [Chi tiết lỗi]
  + Trạng thái không phải "Chuyển trường"
  + Học sinh không tồn tại
  + Thông tin học sinh không đầy đủ
```

#### 6.1.3. Lỗi - Học kỳ

```
❌ LỖI: Học kỳ không đủ điều kiện
- Học kỳ: [Tên học kỳ] - [Năm học]
- Lý do: [Chi tiết lỗi]
  + Học kỳ không có trạng thái "Đang diễn ra"
  + Học kỳ không tồn tại
  + Học sinh đã được phân lớp trong học kỳ này
```

#### 6.1.4. Lỗi - Lớp học

```
❌ LỖI: Lớp học không đủ điều kiện
- Lớp: [Tên lớp]
- Lý do: [Chi tiết lỗi]
  + Lớp không tồn tại
  + Lớp đã đầy (sĩ số: [X]/[Y])
```

#### 6.1.5. Lỗi - Dữ liệu Excel

```
❌ LỖI: Dữ liệu Excel không hợp lệ
- Dòng [X]: [Chi tiết lỗi]
  + Thiếu họ tên
  + Ngày sinh không hợp lệ
  + Giới tính không hợp lệ
  + SĐT/Email trùng lặp
  + Thiếu khối lớp
  + Ngày chuyển vào >= 1/3 thời gian học kỳ
  + Lớp nguyện vọng không cùng khối
  + Phụ huynh không thể thêm (SĐT/Email trùng)
  + Mối quan hệ không hợp lệ
```

#### 6.1.6. Lỗi - Điểm, hạnh kiểm, xếp loại

```
❌ LỖI: Thiếu dữ liệu học tập
- Học sinh: [Họ và tên] (MaHS: [Mã])
- Lý do: [Chi tiết lỗi]
  + Thiếu điểm của học kỳ [Tên học kỳ]
  + Thiếu hạnh kiểm của học kỳ [Tên học kỳ]
  + Thiếu xếp loại của học kỳ [Tên học kỳ]
  + Xếp loại học lực "Yếu" hoặc "Kém" ở học kỳ [Tên học kỳ]
```

#### 6.1.7. Lỗi - Nguyện vọng

```
❌ LỖI: Lớp nguyện vọng không hợp lệ
- Học sinh: [Họ và tên] (MaHS: [Mã])
- Lớp nguyện vọng: [Tên lớp]
- Lý do: [Chi tiết lỗi]
  + Lớp không tồn tại
  + Lớp không cùng khối
  + Lớp đã đầy (sĩ số: [X]/[Y])
  → Hệ thống sẽ tự động phân lớp vào lớp khác cùng khối
```

---

## 7. VÍ DỤ KIỂM TRA

### 7.1. Ví dụ thành công

```
Học sinh: Nguyễn Văn A (MaHS: 101)
- Trạng thái: "Chuyển trường" ✅
- Học kỳ: "Học kỳ 1" - "2024-2025" (Đang diễn ra) ✅
- Chưa được phân lớp trong học kỳ này ✅
- Lớp: "10A1" (Sĩ số: 35/40) ✅

→ KẾT QUẢ: ĐỦ ĐIỀU KIỆN PHÂN LỚP
```

### 7.2. Ví dụ lỗi - Trạng thái sai

```
Học sinh: Trần Thị B (MaHS: 102)
- Trạng thái: "Đang học" ❌

→ KẾT QUẢ: KHÔNG ĐỦ ĐIỀU KIỆN (Trạng thái không phải "Chuyển trường")
```

### 7.3. Ví dụ lỗi - Đã phân lớp

```
Học sinh: Lê Văn C (MaHS: 103)
- Trạng thái: "Chuyển trường" ✅
- Học kỳ: "Học kỳ 1" - "2024-2025" ✅
- Đã được phân lớp vào lớp "10A2" trong học kỳ này ❌

→ KẾT QUẢ: KHÔNG ĐỦ ĐIỀU KIỆN (Đã được phân lớp)
```

### 7.4. Ví dụ lỗi - Lớp đầy

```
Học sinh: Phạm Thị D (MaHS: 104)
- Trạng thái: "Chuyển trường" ✅
- Học kỳ: "Học kỳ 1" - "2024-2025" ✅
- Chưa được phân lớp ✅
- Lớp: "10A1" (Sĩ số: 40/40) ❌

→ KẾT QUẢ: KHÔNG ĐỦ ĐIỀU KIỆN (Lớp đã đầy)
```

### 7.5. Ví dụ lỗi - Xếp loại "Yếu"

```
Học sinh: Hoàng Văn E (MaHS: 105)
- Trạng thái: "Chuyển trường" ✅
- Học kỳ: "Học kỳ 1" - "2024-2025" ✅
- Chưa được phân lớp ✅
- Xếp loại học kỳ trước: "Yếu" ❌

→ KẾT QUẢ: KHÔNG ĐỦ ĐIỀU KIỆN (Xếp loại "Yếu" hoặc "Kém")
```

### 7.6. Ví dụ lỗi - Ngày chuyển vào muộn

```
Học sinh: Võ Thị F (MaHS: 106)
- Trạng thái: "Chuyển trường" ✅
- Học kỳ: "Học kỳ 1" - "2024-2025" (01/09/2024 - 31/12/2024) ✅
- Ngày chuyển vào: 15/10/2024 ❌
- 1/3 thời gian học kỳ: 01/10/2024

→ KẾT QUẢ: KHÔNG ĐỦ ĐIỀU KIỆN (Ngày chuyển vào >= 1/3 thời gian học kỳ)
```

### 7.7. Ví dụ thành công - Có nguyện vọng

```
Học sinh: Đặng Văn G (MaHS: 107)
- Trạng thái: "Chuyển trường" ✅
- Học kỳ: "Học kỳ 1" - "2024-2025" ✅
- Khối: "10" ✅
- Nguyện vọng: "10A1" ✅
- Lớp 10A1 cùng khối và còn chỗ (35/40) ✅
- Đầy đủ điểm, hạnh kiểm, xếp loại các học kỳ trước ✅
- Xếp loại: "Khá" (không có "Yếu" hoặc "Kém") ✅

→ KẾT QUẢ: ĐỦ ĐIỀU KIỆN PHÂN LỚP VÀO LỚP NGUYỆN VỌNG
```

### 7.8. Ví dụ thành công - Tự động phân lớp

```
Học sinh: Bùi Thị H (MaHS: 108)
- Trạng thái: "Chuyển trường" ✅
- Học kỳ: "Học kỳ 1" - "2024-2025" ✅
- Khối: "10" ✅
- Nguyện vọng: (Trống) → Tự động phân lớp ✅
- Lớp 10A2 cùng khối và còn thiếu sĩ số (30/40) ✅
- Đầy đủ điểm, hạnh kiểm, xếp loại các học kỳ trước ✅

→ KẾT QUẢ: ĐỦ ĐIỀU KIỆN, TỰ ĐỘNG PHÂN LỚP VÀO 10A2
```

### 7.9. Ví dụ - Nguyện vọng đầy, tự động chuyển

```
Học sinh: Lý Văn I (MaHS: 109)
- Trạng thái: "Chuyển trường" ✅
- Học kỳ: "Học kỳ 1" - "2024-2025" ✅
- Khối: "10" ✅
- Nguyện vọng: "10A1" (Lớp đã đầy 40/40) ⚠️
- Lớp 10A2 cùng khối và còn chỗ (35/40) ✅

→ KẾT QUẢ: ĐỦ ĐIỀU KIỆN, TỰ ĐỘNG PHÂN LỚP VÀO 10A2 (Lớp nguyện vọng đã đầy)
```

---

## 8. LƯU Ý QUAN TRỌNG

1. **Chỉ phân lớp cho học sinh "Chuyển trường"**: Hệ thống chỉ cho phép phân lớp học sinh có trạng thái "Chuyển trường" vào học kỳ hiện tại.

2. **Học kỳ phải "Đang diễn ra"**: Chỉ có thể phân lớp vào học kỳ đang diễn ra, không thể phân lớp vào học kỳ đã kết thúc hoặc chưa bắt đầu.

3. **Không phân lớp trùng**: Mỗi học sinh chỉ có thể được phân vào 1 lớp trong 1 học kỳ.

4. **Kiểm tra sĩ số**: Trước khi phân lớp, hệ thống phải kiểm tra sĩ số lớp để đảm bảo lớp chưa đầy.

5. **Validation dữ liệu Excel**: Khi nhập từ Excel, hệ thống sẽ kiểm tra từng dòng và chỉ nhập những học sinh đủ điều kiện.

6. **Thông báo rõ ràng**: Tất cả các lỗi và cảnh báo phải được hiển thị rõ ràng trong `txtKetQua` để người dùng biết lý do không thể phân lớp.

7. **Điều kiện về điểm, hạnh kiểm, xếp loại**:

   - **Logic điểm số**: Nếu học sinh chuyển vào khối X, học kỳ Y
     - Cần điểm của: **Tất cả học kỳ của khối X-1** + **Tất cả học kỳ của khối X trước học kỳ Y**
     - Ví dụ: Khối 11, HK2 → cần điểm HK1, HK2 của năm học trước (khối 10) + HK1 của năm học hiện tại (khối 11)
   - Học sinh phải có đầy đủ điểm, hạnh kiểm, xếp loại của **TẤT CẢ** các học kỳ cần thiết (theo khối).
   - Xếp loại học lực **KHÔNG ĐƯỢC** là "Yếu" hoặc "Kém" ở bất kỳ học kỳ nào.

8. **Khối lớp**:

   - Phải chỉ định khối lớp cho học sinh.
   - Chỉ có thể phân lớp vào lớp cùng khối.

9. **Nguyện vọng chuyển lớp**:

   - Nếu có nguyện vọng: Kiểm tra lớp nguyện vọng cùng khối và còn chỗ. Nếu không còn chỗ, tự động phân lớp vào lớp khác cùng khối.
   - Nếu không có nguyện vọng: Tự động phân lớp vào lớp cùng khối còn thiếu sĩ số.

10. **Ngày chuyển vào**:
    - Phải **TRƯỚC** 1/3 thời gian học kỳ hiện tại.
    - Công thức: `NgayChuyenVao < (NgayBD + (NgayKT - NgayBD) / 3)`
    - Nếu ngày chuyển vào >= 1/3 thời gian học kỳ → **KHÔNG THỂ** phân lớp.

---

## 9. CẤU TRÚC DỮ LIỆU

### 9.1. Bảng HocSinh

- `MaHocSinh` (INT, PRIMARY KEY)
- `HoTen` (VARCHAR)
- `NgaySinh` (DATE)
- `GioiTinh` (VARCHAR) - "Nam" hoặc "Nữ"
- `SDTHS` (VARCHAR, NULLABLE)
- `Email` (VARCHAR, NULLABLE)
- `TrangThai` (VARCHAR) - "Đang học", "Nghỉ học", "Bảo lưu", "Thôi học", **"Chuyển trường"**

### 9.2. Bảng HocKy

- `MaHocKy` (INT, PRIMARY KEY)
- `TenHocKy` (VARCHAR)
- `MaNamHoc` (VARCHAR, FOREIGN KEY)
- `NgayBD` (DATE)
- `NgayKT` (DATE)
- `TrangThai` (VARCHAR) - "Đang diễn ra", "Chưa bắt đầu", "Đã kết thúc"

### 9.3. Bảng LopHoc

- `MaLop` (INT, PRIMARY KEY)
- `TenLop` (VARCHAR)
- `MaKhoi` (INT, FOREIGN KEY)
- `SiSo` (INT) - Sĩ số tối đa
- `MaGiaoVienChuNhiem` (VARCHAR, NULLABLE, FOREIGN KEY)

### 9.4. Bảng PhanLop

- `MaHocSinh` (INT, FOREIGN KEY)
- `MaLop` (INT, FOREIGN KEY)
- `MaHocKy` (INT, FOREIGN KEY)
- PRIMARY KEY (MaHocSinh, MaLop, MaHocKy)

### 9.5. Bảng DiemSo

- `MaHocSinh` (INT, FOREIGN KEY)
- `MaMonHoc` (INT, FOREIGN KEY)
- `MaHocKy` (INT, FOREIGN KEY)
- `DiemThuongXuyen` (FLOAT) - Điểm thường xuyên (0-10)
- `DiemGiuaKy` (FLOAT) - Điểm giữa kỳ (0-10)
- `DiemCuoiKy` (FLOAT) - Điểm cuối kỳ (0-10)
- `DiemTrungBinh` (FLOAT) - Điểm trung bình (tự động tính)
- PRIMARY KEY (MaHocSinh, MaMonHoc, MaHocKy)

### 9.6. Bảng HanhKiem

- `MaHocSinh` (INT, FOREIGN KEY)
- `MaHocKy` (INT, FOREIGN KEY)
- `XepLoai` (VARCHAR) - "Tốt", "Khá", "Trung bình", "Yếu"
- `NhanXet` (TEXT, NULLABLE)
- PRIMARY KEY (MaHocSinh, MaHocKy)

### 9.7. Bảng XepLoai

- `MaHocSinh` (INT, FOREIGN KEY)
- `MaHocKy` (INT, FOREIGN KEY)
- `HocLuc` (VARCHAR) - "Giỏi", "Khá", "Trung bình", "Yếu", "Kém"
- `GhiChu` (TEXT, NULLABLE)
- PRIMARY KEY (MaHocSinh, MaHocKy)

### 9.8. Bảng KhoiLop

- `MaKhoi` (INT, PRIMARY KEY)
- `TenKhoi` (VARCHAR) - "10", "11", "12"

---

## 10. TÓM TẮT ĐIỀU KIỆN

| Điều kiện                  | Mô tả                                              | Bắt buộc            |
| -------------------------- | -------------------------------------------------- | ------------------- |
| Trạng thái học sinh        | Phải là "Chuyển trường"                            | ✅                  |
| Học kỳ                     | Phải có trạng thái "Đang diễn ra"                  | ✅                  |
| Phân lớp                   | Học sinh chưa được phân lớp trong học kỳ này       | ✅                  |
| Lớp học                    | Lớp tồn tại và chưa đầy                            | ✅                  |
| Thông tin học sinh         | Đầy đủ và hợp lệ                                   | ✅                  |
| Khối lớp                   | Phải chỉ định khối lớp                             | ✅                  |
| Ngày chuyển vào            | Phải trước 1/3 thời gian học kỳ                    | ✅                  |
| Điểm các học kỳ trước      | Phải có đầy đủ điểm của tất cả học kỳ trước        | ✅ (nếu nhập Excel) |
| Hạnh kiểm các học kỳ trước | Phải có đầy đủ hạnh kiểm của tất cả học kỳ trước   | ✅ (nếu nhập Excel) |
| Xếp loại các học kỳ trước  | Phải có đầy đủ xếp loại của tất cả học kỳ trước    | ✅ (nếu nhập Excel) |
| Xếp loại học lực           | Không được có "Yếu" hoặc "Kém" ở bất kỳ học kỳ nào | ✅ (nếu nhập Excel) |
| Nguyện vọng                | Lớp nguyện vọng phải cùng khối và còn chỗ (nếu có) | ⚠️ (nếu có)         |
| Dữ liệu Excel              | Đúng cấu trúc (6 worksheet) và không trùng lặp     | ✅ (nếu nhập Excel) |
| Phụ huynh                  | Phải có thông tin phụ huynh và mối quan hệ         | ✅ (nếu nhập Excel) |

---

## 11. LOGIC PHÂN LỚP TỰ ĐỘNG

### 11.1. Khi có nguyện vọng

1. Kiểm tra lớp nguyện vọng có tồn tại không
2. Kiểm tra lớp nguyện vọng có cùng khối không
3. Kiểm tra sĩ số lớp nguyện vọng:
   - Nếu còn chỗ (sĩ số hiện tại < sĩ số tối đa) → Phân vào lớp nguyện vọng
   - Nếu đã đầy → Chuyển sang logic tự động phân lớp

### 11.2. Khi không có nguyện vọng hoặc lớp nguyện vọng đầy

1. Lấy danh sách tất cả lớp cùng khối
2. Sắp xếp lớp theo sĩ số hiện tại (tăng dần)
3. Chọn lớp có sĩ số thấp nhất và còn chỗ
4. Phân học sinh vào lớp đó

### 11.3. Ưu tiên phân lớp

1. **Ưu tiên 1**: Lớp nguyện vọng (nếu có và còn chỗ)
2. **Ưu tiên 2**: Lớp cùng khối có sĩ số thấp nhất
3. **Ưu tiên 3**: Lớp cùng khối bất kỳ còn chỗ

### 11.4. Tính toán thời gian 1/3 học kỳ

- **Công thức**: `ThoiGianMotPhanBa = NgayBD + (NgayKT - NgayBD) / 3`
- **Ví dụ**:
  - Học kỳ: 01/09/2024 - 31/12/2024
  - Thời gian 1/3: 01/09/2024 + (31/12/2024 - 01/09/2024) / 3 = 01/10/2024 (khoảng)
  - Ngày chuyển vào phải < 01/10/2024

### 11.5. Kiểm tra sĩ số khối

- **Mục đích**: Xác định khối lớp có còn thiếu sĩ số không để có thể phân lớp tự động
- **Công thức**:
  - Lấy tổng sĩ số hiện tại của tất cả lớp trong khối: `SUM(Sĩ số hiện tại của từng lớp)`
  - Lấy tổng sĩ số tối đa của tất cả lớp trong khối: `SUM(Sĩ số tối đa của từng lớp)`
  - Tính phần trăm sĩ số: `(Tổng sĩ số hiện tại / Tổng sĩ số tối đa) * 100`
- **Điều kiện**:
  - Nếu tổng sĩ số hiện tại < tổng sĩ số tối đa → Khối còn thiếu sĩ số → **CÓ THỂ** phân lớp tự động
  - Nếu tổng sĩ số hiện tại >= tổng sĩ số tối đa → Khối đã đầy → **KHÔNG THỂ** phân lớp tự động
- **Ví dụ**:
  - Khối 10 có 5 lớp, mỗi lớp tối đa 40 học sinh → Tổng sĩ số tối đa = 200
  - Hiện tại có 180 học sinh → Còn thiếu 20 chỗ → Có thể phân lớp tự động
  - Hiện tại có 200 học sinh → Đã đầy → Không thể phân lớp tự động

### 11.6. Quy trình phân lớp tự động chi tiết

#### 11.6.1. Bước 1: Kiểm tra nguyện vọng

- Nếu có nguyện vọng:

  1. Tìm lớp theo tên lớp nguyện vọng
  2. Kiểm tra lớp có tồn tại không
  3. Kiểm tra lớp có cùng khối không
  4. Kiểm tra sĩ số lớp: `Sĩ số hiện tại < Sĩ số tối đa`
  5. Nếu tất cả điều kiện đạt → Phân vào lớp nguyện vọng
  6. Nếu không đạt → Chuyển sang Bước 2

- Nếu không có nguyện vọng → Chuyển sang Bước 2

#### 11.6.2. Bước 2: Phân lớp tự động

1. Lấy danh sách tất cả lớp cùng khối với khối đã chọn
2. Lọc các lớp còn chỗ: `Sĩ số hiện tại < Sĩ số tối đa`
3. Sắp xếp lớp theo sĩ số hiện tại (tăng dần) - ưu tiên lớp có ít học sinh nhất
4. Chọn lớp đầu tiên trong danh sách đã sắp xếp
5. Phân học sinh vào lớp đó

#### 11.6.3. Bước 3: Kiểm tra kết quả

- Nếu tìm được lớp phù hợp → Phân lớp thành công
- Nếu không tìm được lớp nào còn chỗ → Báo lỗi: "Khối [Tên khối] đã đầy, không thể phân lớp tự động"

### 11.7. Ví dụ phân lớp tự động

#### Ví dụ 1: Có nguyện vọng và lớp còn chỗ

```
Học sinh: Nguyễn Văn A
- Khối: "10"
- Nguyện vọng: "10A1"
- Lớp 10A1: Sĩ số 35/40 (còn 5 chỗ) ✅
- Kết quả: Phân vào lớp 10A1
```

#### Ví dụ 2: Có nguyện vọng nhưng lớp đầy

```
Học sinh: Trần Thị B
- Khối: "10"
- Nguyện vọng: "10A1"
- Lớp 10A1: Sĩ số 40/40 (đã đầy) ❌
- Lớp 10A2: Sĩ số 30/40 (còn 10 chỗ) ✅
- Kết quả: Tự động phân vào lớp 10A2 (lớp nguyện vọng đã đầy)
```

#### Ví dụ 3: Không có nguyện vọng

```
Học sinh: Lê Văn C
- Khối: "10"
- Nguyện vọng: (Trống)
- Danh sách lớp khối 10:
  - 10A1: 35/40
  - 10A2: 30/40 ← Sĩ số thấp nhất
  - 10A3: 38/40
- Kết quả: Tự động phân vào lớp 10A2 (sĩ số thấp nhất)
```

#### Ví dụ 4: Khối đã đầy

```
Học sinh: Phạm Thị D
- Khối: "10"
- Danh sách lớp khối 10:
  - 10A1: 40/40 (đầy)
  - 10A2: 40/40 (đầy)
  - 10A3: 40/40 (đầy)
- Kết quả: ❌ Lỗi - Khối 10 đã đầy, không thể phân lớp tự động
```

---

**Ngày tạo**: [Ngày hiện tại]  
**Phiên bản**: 1.0  
**Tác giả**: Hệ thống Quản lý Học sinh
