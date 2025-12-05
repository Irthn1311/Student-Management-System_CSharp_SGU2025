# 📋 QUY TRÌNH NHẬP EXCEL CHUYỂN TRƯỜNG - CHI TIẾT TỪNG BƯỚC

## 🎯 TỔNG QUAN

Khi người dùng nhấn nút **"Phân lớp chuyển trường"** và chọn file Excel, hệ thống sẽ thực hiện quy trình nhập dữ liệu theo **6 bước tuần tự**. Nếu **BẤT KỲ BƯỚC NÀO** gặp lỗi, toàn bộ quá trình sẽ **DỪNG LẠI** và **ROLLBACK** (xóa) các dữ liệu đã nhập.

---

## 📝 CẤU TRÚC FILE EXCEL

File Excel **PHẢI** có **6 worksheet** với tên chính xác:
1. **HocSinh** - Thông tin học sinh
2. **PhuHuynh** - Thông tin phụ huynh
3. **MoiQuanHe** - Mối quan hệ giữa học sinh và phụ huynh
4. **Diem** - Điểm số các môn học (chỉ để kiểm tra điều kiện, KHÔNG lưu vào DB)
5. **HanhKiem** - Hạnh kiểm (chỉ để kiểm tra điều kiện, KHÔNG lưu vào DB)
6. **XepLoai** - Xếp loại học lực (chỉ để kiểm tra điều kiện, KHÔNG lưu vào DB)

---

## 🔄 QUY TRÌNH CHI TIẾT TỪNG BƯỚC

### **BƯỚC 0: KIỂM TRA HỌC KỲ HIỆN TẠI**

**Mục đích:** Xác định học kỳ đang diễn ra để tính toán các học kỳ cần kiểm tra điểm.

**Thực hiện:**
1. Gọi `SemesterHelper.GetCurrentSemester()` để lấy học kỳ có trạng thái **"Đang diễn ra"**
2. Kiểm tra:
   - Nếu không tìm thấy → Hiển thị lỗi và **DỪNG**
   - Nếu trạng thái không phải "Đang diễn ra" → Hiển thị lỗi và **DỪNG**

**Kết quả:** Lưu `hocKyHienTai` (ví dụ: Học kỳ I, 2025-2026, MaHocKy = 3)

---

### **BƯỚC 1: NHẬP HỌC SINH (Worksheet "HocSinh")**

**Mục đích:** Nhập thông tin học sinh chuyển trường vào database.

**Thực hiện:**
1. **Đọc header row** (dòng 1) để tự động phát hiện vị trí các cột:
   - "Họ và tên", "Ngày sinh", "Giới tính", "SĐT", "Email", "Trạng thái", "Khối", "Ngày chuyển vào", "Nguyện vọng chuyển lớp"
2. **Đọc từng dòng** (từ dòng 2 trở đi):
   - Parse ngày sinh (hỗ trợ nhiều định dạng: Excel serial number, DateTime object, text formats)
   - Parse ngày chuyển vào
   - **Kiểm tra điều kiện:** Ngày chuyển vào phải ≤ 1/3 thời gian học kỳ hiện tại
   - **Kiểm tra trùng:** SĐT và Email không được trùng trong cùng file Excel
   - **Kiểm tra trùng với DB:** SĐT và Email không được trùng với học sinh đã có trong database
   - **Tự động set:** `TrangThai = "Đang học(CT)"` (không đọc từ Excel)
   - Tạo `HocSinhDTO` và gọi `hocSinhBus.AddHocSinh()`
   - Tạo tài khoản `NguoiDung` tự động (username = SĐT, password = ngày sinh)
3. **Lưu danh sách học sinh thành công:**
   - Dictionary: `hocSinhThanhCong[tên học sinh] = (maHS, excelRow, khoi, ngayChuyenVao, nguyenVong)`

**Kết quả:**
- ✅ **Thành công:** Trả về `hocSinhThanhCong` (Dictionary chứa các học sinh đã nhập thành công)
- ❌ **Lỗi:** Nếu `hocSinhThanhCong.Count == 0` → Hiển thị thông báo và **DỪNG** (không tiếp tục các bước sau)

**Lưu ý:**
- Nếu một học sinh lỗi, chỉ học sinh đó bị bỏ qua, các học sinh khác vẫn tiếp tục
- Nếu TẤT CẢ học sinh đều lỗi → DỪNG

---

### **BƯỚC 2: NHẬP PHỤ HUYNH (Worksheet "PhuHuynh")**

**Mục đích:** Nhập thông tin phụ huynh của các học sinh đã nhập thành công ở Bước 1.

**Thực hiện:**
1. **Đọc header row** để tự động phát hiện vị trí các cột:
   - "Họ và tên", "SĐT", "Email", "Địa chỉ"
2. **Đọc từng dòng:**
   - **Kiểm tra:** Phụ huynh phải tương ứng với một học sinh ở **cùng dòng Excel** (dựa vào `excelRow`)
   - Nếu không có học sinh tương ứng → Bỏ qua dòng này
   - **Kiểm tra trùng:** SĐT và Email không được trùng trong cùng file Excel
   - **Kiểm tra trùng với DB:** Nếu SĐT/Email đã tồn tại trong DB → Lấy `MaPhuHuynh` từ DB (không tạo mới)
   - Nếu chưa tồn tại → Tạo mới `PhuHuynhDTO` và gọi `phuHuynhBLL.AddPhuHuynh()`
   - **Track phụ huynh mới tạo:** Lưu `MaPhuHuynh` vào `phuHuynhMoiTao` (HashSet) để rollback sau này
3. **Lưu danh sách phụ huynh thành công:**
   - Dictionary: `phuHuynhThanhCong[tên phụ huynh] = (maPH, excelRow)`

**Rollback nếu lỗi:**
- Nếu một phụ huynh lỗi → **Rollback học sinh tương ứng:**
  - Xóa `HocSinh` record
  - Xóa `NguoiDung` record
  - Xóa khỏi `hocSinhThanhCong`
- Nếu phụ huynh là **mới tạo** và không còn học sinh nào khác sử dụng → Xóa `PhuHuynh` record

**Kết quả:**
- ✅ **Thành công:** Tiếp tục với `hocSinhThanhCong` (có thể đã giảm số lượng do rollback)
- ❌ **Lỗi:** Nếu `hocSinhThanhCong.Count == 0` → Hiển thị thông báo và **DỪNG**

---

### **BƯỚC 3: NHẬP MỐI QUAN HỆ (Worksheet "MoiQuanHe")**

**Mục đích:** Tạo mối quan hệ giữa học sinh và phụ huynh.

**Thực hiện:**
1. **Đọc header row** để tự động phát hiện vị trí các cột:
   - "Họ và tên" (học sinh), "Tên PH" (phụ huynh), "Mối quan hệ"
2. **Đọc từng dòng:**
   - **Match học sinh:** Tìm học sinh trong `hocSinhThanhCong` (ưu tiên match theo `excelRow`, sau đó match theo tên)
   - **Match phụ huynh:** Tìm phụ huynh trong `phuHuynhThanhCong` (ưu tiên match theo `excelRow`, sau đó match theo tên)
   - **Kiểm tra:** Mối quan hệ phải hợp lệ ("Cha", "Mẹ", "Người giám hộ", v.v.)
   - Gọi `hocSinhPhuHuynhBLL.AddQuanHe()` để tạo mối quan hệ

**Rollback nếu lỗi:**
- Nếu một mối quan hệ lỗi → **Rollback học sinh tương ứng:**
  - Xóa `HocSinh` record
  - Xóa `NguoiDung` record
  - Xóa tất cả `HocSinhPhuHuynh` records của học sinh này
  - Xóa khỏi `hocSinhThanhCong`
- Nếu phụ huynh là **mới tạo** và không còn học sinh nào khác sử dụng → Xóa `PhuHuynh` record

**Kết quả:**
- ✅ **Thành công:** Tiếp tục với `hocSinhThanhCong` (có thể đã giảm số lượng do rollback)
- ❌ **Lỗi:** Nếu `hocSinhThanhCong.Count == 0` → Hiển thị thông báo và **DỪNG**

---

### **BƯỚC 4: KIỂM TRA ĐIỂM, HẠNH KIỂM, XẾP LOẠI (Worksheets "Diem", "HanhKiem", "XepLoai")**

**Mục đích:** Kiểm tra điều kiện về điểm, hạnh kiểm, xếp loại. **KHÔNG LƯU VÀO DATABASE**, chỉ kiểm tra.

**Thực hiện:**

#### **4.1. Tính toán học kỳ cần thiết cho từng học sinh:**

**Logic theo khối và học kỳ hiện tại:**

- **Khối 10:**
  - Nếu HK1 đang diễn ra → **KHÔNG cần** học kỳ nào
  - Nếu HK2 đang diễn ra → Cần **HK1 của năm học hiện tại**

- **Khối 11:**
  - Nếu HK1 đang diễn ra → Cần **HK1, HK2 của năm học trước** (khối 10)
  - Nếu HK2 đang diễn ra → Cần **HK1 của năm học hiện tại** (khối 11) + **HK1, HK2 của năm học trước** (khối 10)

- **Khối 12:**
  - Nếu HK1 đang diễn ra → Cần **HK1, HK2 của 2 năm học trước** (khối 10 và khối 11)
  - Nếu HK2 đang diễn ra → Cần **HK1 của năm học hiện tại** (khối 12) + **HK1, HK2 của 2 năm học trước** (khối 10 và khối 11)

**Ví dụ:**
- Học kỳ hiện tại: **Học kỳ I, 2025-2026** (MaHocKy = 3)
- Học sinh **Lê Văn C (Khối 11)** → Cần: **Học kỳ I, 2024-2025** (MaHocKy = 1) và **Học kỳ II, 2024-2025** (MaHocKy = 2)

#### **4.2. Đọc điểm từ Excel (Worksheet "Diem"):**

1. **Đọc header row** để tự động phát hiện vị trí các cột:
   - "Họ và tên", "Tên học kỳ", "Năm học", "Mã môn học", "Tên môn học", "Điểm thường xuyên", "Điểm giữa kỳ", "Điểm cuối kỳ", "Điểm trung bình"
2. **Đọc từng dòng:**
   - Match học sinh trong `hocSinhThanhCong`
   - **Tìm MaHocKy từ "Tên học kỳ" và "Năm học":**
     - Query `allHocKy` để tìm học kỳ có `TenHocKy` và `MaNamHoc` khớp
   - **Kiểm tra:** Học kỳ này có trong danh sách `hocKyCanThiet[maHS]` không?
     - Nếu **KHÔNG** → Bỏ qua dòng này (không lưu vào `diemTheoHS`)
     - Nếu **CÓ** → Lưu vào `diemTheoHS[maHS][maHocKy][maMonHoc]`
3. **Lưu vào Dictionary:** `diemTheoHS[maHS][maHocKy][maMonHoc] = DiemSoDTO`
   - **Lưu ý:** Chỉ lưu vào memory, **KHÔNG gọi** `diemSoDAO.UpsertDiemSo()` (không lưu vào DB)

#### **4.3. Đọc hạnh kiểm từ Excel (Worksheet "HanhKiem"):**

1. Tương tự như đọc điểm
2. Lưu vào `hanhKiemTheoHS[maHS][maHocKy]`
3. **KHÔNG lưu vào DB**

#### **4.4. Đọc xếp loại từ Excel (Worksheet "XepLoai"):**

1. Tương tự như đọc điểm
2. Lưu vào `xepLoaiTheoHS[maHS][maHocKy]`
3. **Kiểm tra điều kiện:** Học lực không được "Yếu" hoặc "Kém"
4. **KHÔNG lưu vào DB**

#### **4.5. Kiểm tra điều kiện:**

Với mỗi học sinh trong `hocSinhThanhCong`:
1. Lấy danh sách học kỳ cần thiết: `hocKyCanThiet[maHS]`
2. Nếu danh sách rỗng (ví dụ: khối 10, HK1) → **Thỏa điều kiện** (không cần check gì)
3. Với mỗi học kỳ cần thiết:
   - Kiểm tra có điểm trong `diemTheoHS[maHS][maHocKy]` không?
     - Nếu **KHÔNG** → **Không thỏa điều kiện** → Rollback học sinh
   - Kiểm tra có đủ **13 môn học** không?
     - Nếu thiếu môn nào → **Không thỏa điều kiện** → Rollback học sinh
   - Kiểm tra có hạnh kiểm trong `hanhKiemTheoHS[maHS][maHocKy]` không?
     - Nếu **KHÔNG** → **Không thỏa điều kiện** → Rollback học sinh
   - Kiểm tra có xếp loại trong `xepLoaiTheoHS[maHS][maHocKy]` không?
     - Nếu **KHÔNG** → **Không thỏa điều kiện** → Rollback học sinh

**Rollback nếu lỗi:**
- Nếu một học sinh không thỏa điều kiện → **Rollback:**
  - Xóa `HocSinh` record
  - Xóa `NguoiDung` record
  - Xóa tất cả `HocSinhPhuHuynh` records
  - Xóa khỏi `hocSinhThanhCong`
- Nếu phụ huynh là **mới tạo** và không còn học sinh nào khác sử dụng → Xóa `PhuHuynh` record

**Kết quả:**
- ✅ **Thành công:** Tiếp tục với `hocSinhThanhCong` (có thể đã giảm số lượng do rollback)
- ❌ **Lỗi:** Nếu `hocSinhThanhCong.Count == 0` → Hiển thị thông báo và **DỪNG**

**Hiển thị thông báo chi tiết:**
- Tổng số học sinh thỏa/không thỏa điều kiện
- Danh sách học sinh thành công
- Chi tiết lỗi cho từng học sinh không thỏa điều kiện

---

### **BƯỚC 5: TỰ ĐỘNG PHÂN LỚP**

**Mục đích:** Tự động phân lớp cho các học sinh đã vượt qua tất cả các bước trên.

**Thực hiện:**
1. Với mỗi học sinh trong `hocSinhThanhCong`:
   - Lấy `khoi` và `nguyenVong` (nguyện vọng chuyển lớp)
   - **Ưu tiên 1:** Nếu có `nguyenVong` và lớp đó:
     - Cùng khối với học sinh
     - Còn chỗ trống (số học sinh < sức chứa)
     - → Phân vào lớp nguyện vọng
   - **Ưu tiên 2:** Nếu không có nguyện vọng hoặc lớp nguyện vọng không đáp ứng:
     - Tìm tất cả các lớp cùng khối
     - Chọn lớp có **ít học sinh nhất** (ưu tiên lớp có sức chứa còn trống)
     - → Phân vào lớp đó
   - Gọi `phanLopBLL.AddPhanLop()` để lưu phân lớp

**Rollback nếu lỗi:**
- Nếu không thể phân lớp (ví dụ: không có lớp nào còn chỗ) → **Rollback học sinh:**
  - Xóa `HocSinh` record
  - Xóa `NguoiDung` record
  - Xóa tất cả `HocSinhPhuHuynh` records
  - Xóa khỏi `hocSinhThanhCong`

**Kết quả:**
- ✅ **Thành công:** Hiển thị thông báo với danh sách học sinh đã phân lớp
- ❌ **Lỗi:** Hiển thị thông báo với danh sách học sinh không thể phân lớp

---

## ⚠️ LƯU Ý QUAN TRỌNG

### **1. Quy trình ATOMIC (Tất cả hoặc không có gì):**
- Nếu **BẤT KỲ BƯỚC NÀO** gặp lỗi và `hocSinhThanhCong.Count == 0` → **DỪNG NGAY LẬP TỨC**
- Các học sinh đã nhập ở các bước trước sẽ được **ROLLBACK** (xóa khỏi database)

### **2. Rollback thông minh:**
- Chỉ rollback học sinh **bị lỗi**
- Phụ huynh mới tạo chỉ bị xóa nếu **KHÔNG còn học sinh nào khác** sử dụng

### **3. Điểm, hạnh kiểm, xếp loại KHÔNG lưu vào DB:**
- Chỉ dùng để **kiểm tra điều kiện**
- Không gọi `diemSoDAO.UpsertDiemSo()`, `hanhKiemDAO.LuuHanhKiem()`, `xepLoaiDAO.LuuXepLoai()`

### **4. Dynamic Column Mapping:**
- Tự động phát hiện vị trí cột bằng cách đọc header row
- Không cần cột "Mã HS" hoặc "Mã PH" (vì là auto-increment)
- Hỗ trợ nhiều tên cột khác nhau (ví dụ: "SĐT", "SDT", "Điện thoại")

### **5. Xử lý trùng tên:**
- Ưu tiên match theo `excelRow` (dòng Excel)
- Nếu không match được theo `excelRow`, mới match theo tên
- Cảnh báo nếu có nhiều học sinh/phụ huynh trùng tên

---

## 🔍 DEBUG MODE

Để bật debug mode và xem chi tiết quy trình, uncomment các dòng `MessageBox.Show()` trong code:
- Dòng ~1835: Hiển thị học kỳ cần thiết cho từng học sinh
- Dòng ~1920: Hiển thị giá trị đọc từ Excel
- Dòng ~1965: Hiển thị khi học kỳ không cần thiết
- Dòng ~2005: Hiển thị khi lưu điểm thành công
- Dòng ~2265: Hiển thị khi thiếu điểm

---

## 📊 VÍ DỤ QUY TRÌNH

**Input:**
- File Excel với 5 học sinh: Nguyễn Văn A (Khối 10), Trần Thị B (Khối 10), Lê Văn C (Khối 11), Phạm Thị D (Khối 11), Hoàng Văn E (Khối 12)
- Học kỳ hiện tại: Học kỳ I, 2025-2026

**Quy trình:**
1. ✅ Nhập 5 học sinh → `hocSinhThanhCong.Count = 5`
2. ✅ Nhập 5 phụ huynh → `hocSinhThanhCong.Count = 5`
3. ✅ Nhập 5 mối quan hệ → `hocSinhThanhCong.Count = 5`
4. ✅ Kiểm tra điểm:
   - Nguyễn Văn A, Trần Thị B (Khối 10) → Không cần học kỳ nào → Thỏa điều kiện
   - Lê Văn C, Phạm Thị D (Khối 11) → Cần HK1, HK2 năm 2024-2025 → Kiểm tra có đủ điểm → Thỏa điều kiện
   - Hoàng Văn E (Khối 12) → Cần HK1, HK2 năm 2024-2025 → Kiểm tra có đủ điểm → Thỏa điều kiện
   - `hocSinhThanhCong.Count = 5`
5. ✅ Phân lớp cho 5 học sinh → Thành công

**Kết quả:** 5 học sinh đã được nhập và phân lớp thành công!

---

## 🎯 KẾT LUẬN

Quy trình nhập Excel chuyển trường là một quy trình **phức tạp, nhiều bước, và nghiêm ngặt**. Mỗi bước đều có kiểm tra và rollback để đảm bảo tính nhất quán của dữ liệu. Nếu bất kỳ bước nào thất bại, toàn bộ quá trình sẽ dừng lại và các dữ liệu đã nhập sẽ được rollback.

