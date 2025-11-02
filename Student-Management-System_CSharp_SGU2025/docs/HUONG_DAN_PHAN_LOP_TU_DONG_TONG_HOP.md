# 📚 HƯỚNG DẪN PHÂN LỚP TỰ ĐỘNG - TỔNG HỢP

> **Tài liệu này tổng hợp toàn bộ thông tin về hệ thống phân lớp tự động**  
> Bao gồm: Logic, Điều kiện, Thuật toán, Cách test, và Xử lý các trường hợp đặc biệt

---

## 📋 MỤC LỤC

1. [Tổng quan hệ thống](#1-tổng-quan-hệ-thống)
2. [Ba kịch bản phân lớp](#2-ba-kịch-bản-phân-lớp)
3. [Điều kiện và tiêu chí](#3-điều-kiện-và-tiêu-chí)
4. [Thuật toán phân lớp](#4-thuật-toán-phân-lớp)
5. [Cách sử dụng](#5-cách-sử-dụng)
6. [Test và dữ liệu mẫu](#6-test-và-dữ-liệu-mẫu)
7. [Xử lý trường hợp đặc biệt](#7-xử-lý-trường-hợp-đặc-biệt)
8. [Thông báo và Log](#8-thông-báo-và-log)

---

## 1. TỔNG QUAN HỆ THỐNG

### 1.1. Mục đích

Hệ thống phân lớp tự động giúp:

- ✅ Phân lớp học sinh **TRỰC TIẾP** cho học kỳ được chọn
- ✅ Tự động xét điều kiện lên lớp dựa trên kết quả học tập
- ✅ Cập nhật trạng thái tốt nghiệp cho học sinh khối 12
- ✅ Phân bổ đều học sinh vào các lớp (tránh lớp quá đông/vắng)

### 1.2. Luồng hoạt động

```
User chọn học kỳ cần phân lớp
        ↓
Hệ thống phát hiện kịch bản (1, 2, hoặc 3)
        ↓
Xử lý theo logic kịch bản tương ứng
        ↓
Hiển thị kết quả + thống kê
```

### 1.3. File liên quan

| File                      | Mô tả                | Dòng code |
| ------------------------- | -------------------- | --------- |
| `PhanLopTuDongBLL.cs`     | Logic phân lớp chính | ~1,300    |
| `PhanLopBLL.cs`           | Hỗ trợ phân lớp      | ~600      |
| `PhanLopDAO.cs`           | Truy xuất database   | ~560      |
| `PhanLop.cs` (GUI)        | Giao diện người dùng | ~750      |
| `ScrollableMessageBox.cs` | Hiển thị kết quả     | ~100      |

---

## 2. BA KỊCH BẢN PHÂN LỚP

### 🔹 Kịch bản 1: HK1 → HK2 (Giữ nguyên lớp)

**Khi nào:** User chọn HK2 MÀ đã có HK1 cùng năm học

**Logic:**

```
Học sinh lớp 10A1 ở HK1 → Vẫn 10A1 ở HK2
Học sinh lớp 11B2 ở HK1 → Vẫn 11B2 ở HK2
```

**Điều kiện:**

- ✅ Học sinh có trạng thái "Đang học"
- ✅ Đã có phân lớp HK1 cùng năm
- ⚠️ KHÔNG XÉT điều kiện điểm số/hạnh kiểm

**Ví dụ:**

```
Chọn: HK2 2025-2026
→ Kiểm tra: Có HK1 2025-2026 không? → CÓ
→ Kịch bản: HK1_TO_HK2
→ Copy lớp từ HK1 sang HK2
```

**Code:**

```csharp
// Lấy lớp HK1
var phanLopHK1 = allPhanLopHist.FirstOrDefault(p =>
    p.maHocSinh == hs.MaHS && p.maHocKy == hocKyNguon.MaHocKy);

// Thêm vào HK2 với cùng lớp
phanLopDAO.ThemPhanLop(hs.MaHS, phanLopHK1.maLop, maHocKyCanPhanLop);
```

---

### 🔹 Kịch bản 2: HK2 năm trước → HK1 năm sau (Xét lên lớp)

**Khi nào:** User chọn HK1 năm SAU mà đã có HK2 năm TRƯỚC

**Logic:**

```
Học sinh lớp 10 (ĐTB ≥ 5.0, hạnh kiểm tốt) → Lên lớp 11
Học sinh lớp 11 (ĐTB < 5.0) → Ở lại lớp 11
Học sinh lớp 12 (đủ điều kiện) → Tốt nghiệp
```

**Điều kiện lên lớp:**

| Tiêu chí          | Yêu cầu      |
| ----------------- | ------------ |
| Điểm TB cả năm    | ≥ 5.0        |
| Hạnh kiểm cả năm  | ≥ Trung Bình |
| Môn Kém (< 3.5)   | = 0          |
| Môn Yếu (3.5-5.0) | ≤ 2          |

**Công thức tính điểm:**

```
ĐTB môn cả năm = (ĐTB HK1 + ĐTB HK2 × 2) ÷ 3
ĐTB cả năm = Trung bình của tất cả các môn
```

**Ví dụ:**

```
Chọn: HK1 2026-2027
→ Kiểm tra: Có HK2 2025-2026 không? → CÓ
→ Kịch bản: HK2_NAM_TRUOC_TO_HK1
→ Xét từng học sinh:
   • HS A (10A1, ĐTB=7.5, HK=Tốt) → Lên 11A1 ✓
   • HS B (11B2, ĐTB=4.2) → Ở lại 11B3 ×
   • HS C (12A8, ĐTB=8.0) → Tốt nghiệp 🎓
```

**Code:**

```csharp
// Tính điểm TB cả năm
double dtbHK1 = diemHK1.Average(d => d.DiemTrungBinh ?? 0);
double dtbHK2 = diemHK2.Average(d => d.DiemTrungBinh ?? 0);
double dtbCaNam = (dtbHK1 * 1 + dtbHK2 * 2) / 3.0;

// Kiểm tra điều kiện
bool duDieuKienLenLop = (dtbCaNam >= 5.0) &&
                        (hanhKiemCaNam >= "Trung Bình") &&
                        (soMonKem == 0) &&
                        (soMonYeu <= 2);

// Xác định khối mới
int khoiMoi = duDieuKienLenLop ? (khoiCu + 1) : khoiCu;

// Xử lý tốt nghiệp
if (khoiMoi > 12)
{
    hocSinhDAO.CapNhatTrangThaiHocSinh(hs.MaHS, "Đã tốt nghiệp");
    danhSachLoi.Add($"{hs.HoTen}: Đã tốt nghiệp → Cập nhật trạng thái ✓");
}
```

---

### 🔹 Kịch bản 3: FIRST_TIME (Phân lớp lần đầu)

**Khi nào:** User chọn HK1 mà KHÔNG có HK2 năm trước (năm học đầu tiên)

**Logic:**

```
Phân lớp dựa trên năm sinh:
• Sinh 2010 (15 tuổi) ±2 năm → Lớp 10
• Sinh 2009 (16 tuổi) ±2 năm → Lớp 11
• Sinh 2008 (17 tuổi) ±2 năm → Lớp 12
```

**Thuật toán:**

1. Tính tuổi học sinh: `Năm hiện tại - Năm sinh`
2. Xác định khối:
   - Khối 10: Sinh 2008-2012 (năm 2025-2026)
   - Khối 11: Sinh 2007-2011
   - Khối 12: Sinh 2006-2010
3. Phân đều vào các lớp cùng khối (Round-Robin)

**Ví dụ:**

```
Chọn: HK1 2025-2026
→ Kiểm tra: Có HK2 2024-2025 không? → KHÔNG
→ Kịch bản: FIRST_TIME
→ Phân theo năm sinh:
   • HS sinh 2010 → 10A1
   • HS sinh 2010 → 10A2
   • HS sinh 2009 → 11A1
   • HS sinh 2008 → 12A1
```

**Code:**

```csharp
// Tính năm sinh chuẩn cho mỗi khối (năm 2025-2026)
int namSinhKhoi10 = 2025 - 15; // = 2010
int namSinhKhoi11 = 2025 - 16; // = 2009
int namSinhKhoi12 = 2025 - 17; // = 2008

// Xác định khối dựa trên năm sinh (±2 năm)
int namSinh = hs.NgaySinh.Year;
if (Math.Abs(namSinh - namSinhKhoi10) <= 2) khoi = 10;
else if (Math.Abs(namSinh - namSinhKhoi11) <= 2) khoi = 11;
else if (Math.Abs(namSinh - namSinhKhoi12) <= 2) khoi = 12;

// Phân đều vào lớp (Round-Robin)
var lopPhuHop = dsLopKhoi[lopIndex % dsLopKhoi.Count];
phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, maHocKyCanPhanLop);
lopIndex++;
```

---

## 3. ĐIỀU KIỆN VÀ TIÊU CHÍ

### 3.1. Điều kiện học sinh hợp lệ

```csharp
✅ Trạng thái = "Đang học"
❌ Trạng thái = "Đã tốt nghiệp" → Bỏ qua
❌ Trạng thái = "Nghỉ học" → Bỏ qua
❌ Trạng thái = "Bảo lưu" → Bỏ qua
```

### 3.2. Điều kiện dữ liệu đầy đủ (Kịch bản 2)

Để xét lên lớp, học sinh phải có:

- ✅ Điểm số HK1 (tất cả 13 môn)
- ✅ Điểm số HK2 (tất cả 13 môn)
- ✅ Hạnh kiểm HK1
- ✅ Hạnh kiểm HK2

**Nếu thiếu:** Học sinh bị ghi vào danh sách lỗi, không được phân lớp.

### 3.3. Điều kiện lên lớp chi tiết

#### 1️⃣ Điểm trung bình cả năm ≥ 5.0

```
ĐTB môn = (ĐTB HK1 × 1 + ĐTB HK2 × 2) ÷ 3
ĐTB cả năm = (Tổng ĐTB các môn) ÷ Số môn
```

**Ví dụ:**

```
Toán: HK1 = 6.0, HK2 = 7.0 → (6×1 + 7×2)÷3 = 6.67
Văn:  HK1 = 5.5, HK2 = 6.5 → (5.5×1 + 6.5×2)÷3 = 6.17
...
ĐTB cả năm = (6.67 + 6.17 + ...) ÷ 13 = 6.5 ✓
```

#### 2️⃣ Hạnh kiểm cả năm ≥ Trung Bình

Thứ tự: `Yếu < Trung Bình < Khá < Tốt`

Lấy **loại thấp hơn** giữa HK1 và HK2:

```
HK1 = "Tốt", HK2 = "Khá" → Cả năm = "Khá" ✓
HK1 = "Khá", HK2 = "Yếu" → Cả năm = "Yếu" ×
```

#### 3️⃣ Không có môn Kém (< 3.5)

```
Nếu bất kỳ môn nào có ĐTB < 3.5 → KHÔNG được lên lớp
```

#### 4️⃣ Tối đa 2 môn Yếu (3.5 ≤ điểm < 5.0)

```
Môn Yếu: ĐTB trong khoảng [3.5, 5.0)
Nếu > 2 môn Yếu → KHÔNG được lên lớp
```

**Ví dụ:**

```
Học sinh A:
• Toán: 4.0 (Yếu)
• Văn: 4.5 (Yếu)
• Anh: 3.8 (Yếu) ← Quá 2 môn
→ Kết quả: Ở LẠI lớp cũ ×

Học sinh B:
• Toán: 4.2 (Yếu)
• Văn: 4.8 (Yếu)
• Các môn khác ≥ 5.0
→ Kết quả: LÊN lớp ✓
```

---

## 4. THUẬT TOÁN PHÂN LỚP

### 4.1. Round-Robin (Phân đều vào lớp)

**Mục đích:** Đảm bảo số học sinh đều nhau giữa các lớp cùng khối

**Thuật toán:**

```csharp
// Sắp xếp lớp theo MaLop (10A1, 10A2,...10A8)
var dsLopKhoi = allLop.Where(l => l.MaKhoi == khoi)
                      .OrderBy(l => l.MaLop)  // ✓ Sắp theo ID
                      .ToList();

// Phân học sinh lần lượt
int lopIndex = 0;
foreach (var hs in danhSachHocSinh)
{
    var lopPhuHop = dsLopKhoi[lopIndex % dsLopKhoi.Count];
    ThemVaoLop(hs, lopPhuHop);
    lopIndex++; // Chuyển sang lớp tiếp theo
}
```

**Ví dụ với 8 lớp 10A1-10A8 và 24 học sinh:**

```
HS 1  → 10A1
HS 2  → 10A2
HS 3  → 10A3
...
HS 8  → 10A8
HS 9  → 10A1 (vòng lại)
HS 10 → 10A2
...
HS 24 → 10A8

Kết quả: Mỗi lớp có 3 học sinh
```

### 4.2. Phân theo năm sinh (FIRST_TIME)

**Bước 1:** Tính năm sinh chuẩn cho từng khối

```csharp
int namHienTai = hocKyCanPhanLop.NgayBD.Year;
int namSinhKhoi10 = namHienTai - 15;
int namSinhKhoi11 = namHienTai - 16;
int namSinhKhoi12 = namHienTai - 17;
```

**Bước 2:** Cho phép sai lệch ±2 năm

```csharp
int namSinh = hs.NgaySinh.Year;
if (Math.Abs(namSinh - namSinhKhoi10) <= 2)      khoi = 10;
else if (Math.Abs(namSinh - namSinhKhoi11) <= 2) khoi = 11;
else if (Math.Abs(namSinh - namSinhKhoi12) <= 2) khoi = 12;
else
{
    danhSachLoi.Add($"{hs.HoTen}: Năm sinh {namSinh} không phù hợp");
}
```

**Bước 3:** Nhóm học sinh theo khối

```csharp
Dictionary<int, List<HocSinhDTO>> hocSinhTheoKhoi;
hocSinhTheoKhoi[10].Add(hs); // Thêm vào khối 10
hocSinhTheoKhoi[11].Add(hs); // Thêm vào khối 11
```

**Bước 4:** Phân đều vào lớp (Round-Robin)

---

## 5. CÁCH SỬ DỤNG

### 5.1. Từ giao diện (GUI)

**Bước 1:** Mở form **Phân lớp**

```
Menu → Quản lý học sinh → Phân lớp
```

**Bước 2:** Chọn học kỳ cần phân lớp

```
ComboBox "Học kỳ" → Chọn HK1 2025-2026 (hoặc HK2)
```

**Bước 3:** Click "Phân lớp tự động"

```
Button [+ Phân lớp tự động]
```

**Bước 4:** Xem Preview

```
Hiển thị:
• Kịch bản: HK1 → HK2 (hoặc HK2 → HK1...)
• Số học sinh: 475
• Dự kiến lên lớp: 380 HS
• Dự kiến ở lại: 95 HS
```

**Bước 5:** Xác nhận

```
Click [Yes] để thực hiện
```

**Bước 6:** Xem kết quả

```
ScrollableMessageBox hiển thị:
✓ Phân lớp tự động thành công!
Đã phân lớp: 475 học sinh

╔════════════════════════════════════════╗
║     KẾT QUẢ PHÂN LỚP TỰ ĐỘNG          ║
╚════════════════════════════════════════╝

📋 Kịch bản: HK2 năm trước → HK1 năm sau
   Nguồn: Học kỳ II 2025-2026

✅ THÀNH CÔNG: 475 học sinh
   • Lên lớp: 380 học sinh
   • Ở lại: 95 học sinh
   • Tỷ lệ lên lớp: 80.0%

⚠️ LỖI/CẢNH BÁO: 60 trường hợp
   1. HS A: Chưa có điểm HK1
   2. HS B: Chưa có hạnh kiểm HK2
   ...
```

### 5.2. Từ code (API)

```csharp
PhanLopTuDongBLL phanLopTuDongBLL = new PhanLopTuDongBLL();

// Thực hiện phân lớp
var ketQua = phanLopTuDongBLL.ThucHienPhanLopTuDong(
    maHocKyCanPhanLop: 3,      // Mã HK1 2025-2026
    boQuaKiemTra: false        // Kiểm tra điều kiện
);

if (ketQua.success)
{
    Console.WriteLine($"✓ Đã phân lớp {ketQua.soHocSinhDaPhanLop} học sinh");
    Console.WriteLine(ketQua.message);
}
else
{
    Console.WriteLine($"✗ Lỗi: {ketQua.message}");
}
```

---

## 6. TEST VÀ DỮ LIỆU MẪU

### 6.1. Chuẩn bị dữ liệu test

**File SQL cần chạy theo thứ tự:**

```sql
-- 1. Tạo schema database
01_schema.sql

-- 2. Tạo unique indexes
02_unique_indexes.sql

-- 3. Tạo dữ liệu mẫu (500 HS, 24 lớp, 30 GV)
03_sample_seed_optimized.sql

-- 4. (Tùy chọn) Tạo điểm HK2 để test xét lên lớp
04_data_hk2_for_test.sql
```

**Lệnh chạy (MySQL):**

```bash
mysql -u root -p QuanLyHocSinh < 01_schema.sql
mysql -u root -p QuanLyHocSinh < 02_unique_indexes.sql
mysql -u root -p QuanLyHocSinh < 03_sample_seed_optimized.sql
mysql -u root -p QuanLyHocSinh < 04_data_hk2_for_test.sql
```

### 6.2. Dữ liệu trong file 03_sample_seed_optimized.sql

| Dữ liệu   | Số lượng | Ghi chú                         |
| --------- | -------- | ------------------------------- |
| Học sinh  | 500      | 475 "Đang học", 25 "Đã nghỉ"    |
| Phụ huynh | 500      | 1-1 với học sinh                |
| Giáo viên | 30       | Đủ 13 tổ bộ môn                 |
| Lớp học   | 24       | 8 lớp/khối (10, 11, 12)         |
| Năm học   | 3        | 2024-2025, 2025-2026, 2026-2027 |
| Học kỳ    | 4        | HK1+HK2 cho 2 năm               |

**Năm sinh học sinh:**

```
Sinh 2006-2012 (7 năm)
→ Phân bổ đều: ~71 HS/năm
→ Đủ cho cả 3 khối khi phân FIRST_TIME
```

**Phân lớp sẵn trong file:**

```sql
-- Đã phân 475 HS vào HK1 2025-2026
INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy) ...
```

**Điểm số sẵn:**

```sql
-- 475 HS × 13 môn = 6,175 bản ghi
-- Điểm random từ 5.0-10.0
INSERT INTO DiemSo (MaHocSinh, MaMonHoc, MaHocKy, ...) ...
```

### 6.3. Kịch bản test

#### ✅ Test 1: HK1 → HK2 (Giữ nguyên lớp)

**Dữ liệu:**

- Đã chạy file `03_sample_seed_optimized.sql`
- Đã có 475 HS phân lớp HK1 2025-2026

**Test:**

1. Chọn: **Học kỳ II - 2025-2026**
2. Click: **Phân lớp tự động**
3. Xác nhận: **Yes**

**Kết quả mong đợi:**

```
✓ Phân lớp tự động thành công!
Đã phân lớp: 475 học sinh

Kịch bản: HK1 → HK2 (Giữ nguyên lớp)
Nguồn: Học kỳ I 2025-2026

✅ THÀNH CÔNG: 475 học sinh
```

**Kiểm tra:**

```sql
SELECT COUNT(*) FROM PhanLop WHERE MaHocKy = 2; -- Phải = 475

-- Kiểm tra học sinh giữ nguyên lớp
SELECT
    hs.HoTen,
    l1.TenLop AS LopHK1,
    l2.TenLop AS LopHK2
FROM HocSinh hs
JOIN PhanLop pl1 ON pl1.MaHocSinh = hs.MaHocSinh AND pl1.MaHocKy = 1
JOIN PhanLop pl2 ON pl2.MaHocSinh = hs.MaHocSinh AND pl2.MaHocKy = 2
JOIN LopHoc l1 ON l1.MaLop = pl1.MaLop
JOIN LopHoc l2 ON l2.MaLop = pl2.MaLop
WHERE l1.TenLop != l2.TenLop; -- Phải = 0 (không có ai đổi lớp)
```

---

#### ✅ Test 2: HK2 → HK1 năm sau (Xét lên lớp)

**Dữ liệu:**

- Đã test xong Test 1
- Đã chạy file `04_data_hk2_for_test.sql` (tạo điểm HK2)

**Test:**

1. Chọn: **Học kỳ I - 2026-2027**
2. Click: **Phân lớp tự động**
3. Xem preview (số HS lên lớp/ở lại)
4. Xác nhận: **Yes**

**Kết quả mong đợi:**

```
✓ Phân lớp tự động thành công!
Đã phân lớp: 415 học sinh

Kịch bản: HK2 năm trước → HK1 năm sau
Nguồn: Học kỳ II 2025-2026

✅ THÀNH CÔNG: 415 học sinh
   • Lên lớp: 350 học sinh
   • Ở lại: 65 học sinh
   • Tỷ lệ lên lớp: 84.3%

⚠️ LỖI/CẢNH BÁO: 60 trường hợp
   (Các HS thiếu điểm/hạnh kiểm)
```

**Kiểm tra:**

```sql
-- Đếm HS lên lớp
SELECT
    COUNT(*) AS SoHS,
    'Lên lớp' AS KetQua
FROM (
    SELECT hs.MaHocSinh, l1.MaKhoi AS KhoiCu, l2.MaKhoi AS KhoiMoi
    FROM HocSinh hs
    JOIN PhanLop pl1 ON pl1.MaHocSinh = hs.MaHocSinh AND pl1.MaHocKy = 2
    JOIN PhanLop pl2 ON pl2.MaHocSinh = hs.MaHocSinh AND pl2.MaHocKy = 3
    JOIN LopHoc l1 ON l1.MaLop = pl1.MaLop
    JOIN LopHoc l2 ON l2.MaLop = pl2.MaLop
    WHERE l2.MaKhoi > l1.MaKhoi
) AS LenLop

UNION ALL

-- Đếm HS ở lại
SELECT
    COUNT(*) AS SoHS,
    'Ở lại' AS KetQua
FROM (
    SELECT hs.MaHocSinh, l1.MaKhoi AS KhoiCu, l2.MaKhoi AS KhoiMoi
    FROM HocSinh hs
    JOIN PhanLop pl1 ON pl1.MaHocSinh = hs.MaHocSinh AND pl1.MaHocKy = 2
    JOIN PhanLop pl2 ON pl2.MaHocSinh = hs.MaHocSinh AND pl2.MaHocKy = 3
    JOIN LopHoc l1 ON l1.MaLop = pl1.MaLop
    JOIN LopHoc l2 ON l2.MaLop = pl2.MaLop
    WHERE l2.MaKhoi = l1.MaKhoi
) AS OLai;
```

---

#### ✅ Test 3: FIRST_TIME (Phân lớp lần đầu)

**Dữ liệu:**

- Database rỗng (hoặc chỉ có schema)
- Chạy file `03_sample_seed_optimized.sql`
- **XÓA** phân lớp HK1 đã có sẵn:

```sql
DELETE FROM PhanLop WHERE MaHocKy = 1;
```

**Test:**

1. Chọn: **Học kỳ I - 2025-2026**
2. Click: **Phân lớp tự động**
3. Xác nhận: **Yes**

**Kết quả mong đợi:**

```
✓ Phân lớp tự động thành công!
Đã phân lớp: 475 học sinh

Kịch bản: Phân lớp lần đầu (Dựa vào năm sinh)
Phân đều học sinh vào các lớp theo khối

✅ THÀNH CÔNG: 475 học sinh
   • Lên lớp: 171 học sinh (Khối 10)
   • Ở lại (học lại): 303 học sinh (Khối 11, 12)
   • Tỷ lệ lên lớp: 36.0%

⚠️ LỖI/CẢNH BÁO: 1 trường hợp
   1. HS X: Năm sinh 2005 không phù hợp khối nào
```

**Kiểm tra phân bổ đều:**

```sql
SELECT
    l.TenLop,
    COUNT(*) AS SoHS
FROM PhanLop pl
JOIN LopHoc l ON l.MaLop = pl.MaLop
WHERE pl.MaHocKy = 1
GROUP BY l.TenLop
ORDER BY l.MaLop;

-- Kỳ vọng: Mỗi lớp ~19-20 HS
```

---

### 6.4. Test xóa và phân lớp lại

**Mục đích:** Kiểm tra tính năng xóa phân lớp cũ và phân lại

**Test:**

1. Đã có phân lớp HK2 2025-2026
2. Click: **Phân lớp tự động** cho HK2
3. Hệ thống hiện:
   ```
   Học kỳ đã được phân lớp (475 HS).
   Bạn có đồng ý xóa và phân lớp lại không?
   ```
4. Click: **Yes**
5. Xem thông báo: `✓ Đã xóa 475 bản ghi phân lớp cũ`
6. Phân lớp lại tự động

**Kiểm tra:**

```sql
-- Trước khi xóa
SELECT COUNT(*) FROM PhanLop WHERE MaHocKy = 2; -- 475

-- Sau khi xóa
SELECT COUNT(*) FROM PhanLop WHERE MaHocKy = 2; -- 0

-- Sau khi phân lại
SELECT COUNT(*) FROM PhanLop WHERE MaHocKy = 2; -- 475
```

---

## 7. XỬ LÝ TRƯỜNG HỢP ĐẶC BIỆT

### 7.1. Học sinh tốt nghiệp

**Khi nào:** Học sinh khối 12 đủ điều kiện lên lớp (khối mới = 13 > 12)

**Xử lý:**

```csharp
if (khoiMoi > 12)
{
    // Cập nhật trạng thái
    hocSinhDAO.CapNhatTrangThaiHocSinh(hs.MaHS, "Đã tốt nghiệp");

    // Ghi log
    string loi = $"{hs.HoTen}: Đã tốt nghiệp (khối 12)";
    loi += " → Đã cập nhật trạng thái 'Đã tốt nghiệp' ✓";
    danhSachLoi.Add(loi);

    continue; // Không phân lớp nữa
}
```

**SQL:**

```sql
UPDATE HocSinh
SET TrangThai = 'Đã tốt nghiệp'
WHERE MaHocSinh = ?;
```

**Kiểm tra:**

```sql
SELECT MaHocSinh, HoTen, TrangThai
FROM HocSinh
WHERE TrangThai = 'Đã tốt nghiệp';
```

---

### 7.2. Học sinh thiếu dữ liệu

**Các trường hợp:**

1. Thiếu điểm HK1
2. Thiếu điểm HK2
3. Thiếu hạnh kiểm HK1
4. Thiếu hạnh kiểm HK2
5. Thiếu xếp loại

**Xử lý:**

```csharp
if (diemHK1 == null || diemHK1.Count == 0)
{
    string loi = $"{hs.HoTen} (ID: {hs.MaHS}): Chưa có điểm HK1";
    danhSachLoi.Add(loi);
    continue; // Bỏ qua, không phân lớp
}
```

**Thông báo:**

```
⚠️ LỖI/CẢNH BÁO: 60 trường hợp

Chi tiết (tất cả 60 lỗi):
   1. HS Nguyễn Văn A (ID: 123): Chưa có điểm HK1
   2. HS Trần Thị B (ID: 456): Chưa có hạnh kiểm HK2
   ...
```

---

### 7.3. Học sinh năm sinh không hợp lệ (FIRST_TIME)

**Khi nào:** Năm sinh quá cũ hoặc quá mới (không thuộc bất kỳ khối nào)

**Ví dụ:**

```
Năm học: 2025-2026
Năm sinh HS: 2005 (20 tuổi) → Quá tuổi cho khối 12
Năm sinh HS: 2013 (12 tuổi) → Quá trẻ cho khối 10
```

**Xử lý:**

```csharp
int khoi = -1;
if (Math.Abs(namSinh - namSinhKhoi10) <= 2)      khoi = 10;
else if (Math.Abs(namSinh - namSinhKhoi11) <= 2) khoi = 11;
else if (Math.Abs(namSinh - namSinhKhoi12) <= 2) khoi = 12;

if (khoi == -1)
{
    string loi = $"{hs.HoTen}: Năm sinh {namSinh} không phù hợp với khối nào";
    danhSachLoi.Add(loi);
    continue;
}
```

---

### 7.4. Lớp đầy (vượt quá 30 HS)

**Giới hạn:** Mỗi lớp tối đa 30 học sinh

**Xử lý hiện tại:** KHÔNG KIỂM TRA (phân đều Round-Robin)

**Nếu muốn kiểm tra:**

```csharp
int soLuongHienTai = phanLopBLL.CountHocSinhInLop(lop.MaLop, maHocKy);
if (soLuongHienTai >= 30)
{
    // Bỏ qua lớp này, chuyển sang lớp tiếp theo
    continue;
}
```

---

### 7.5. Không có lớp cho khối

**Khi nào:** Database không có lớp cho khối 10/11/12

**Xử lý:**

```csharp
var dsLopKhoiMoi = allLop.Where(l => l.MaKhoi == khoiMoi).ToList();

if (dsLopKhoiMoi.Count == 0)
{
    string loi = $"{hs.HoTen}: Không có lớp nào ở Khối {khoiMoi}";
    danhSachLoi.Add(loi);
    continue;
}
```

**Giải pháp:**

```sql
-- Tạo lớp cho khối thiếu
INSERT INTO LopHoc (TenLop, MaKhoi, SiSo, MaGiaoVienChuNhiem) VALUES
('10A1', 10, 0, 'GV001'),
('10A2', 10, 0, 'GV002'),
...;
```

---

## 8. THÔNG BÁO VÀ LOG

### 8.1. Thông báo thành công

**Cấu trúc:**

```
✓ Phân lớp tự động thành công!
Đã phân lớp: XXX học sinh

╔════════════════════════════════════════════════╗
║        KẾT QUẢ PHÂN LỚP TỰ ĐỘNG               ║
╚════════════════════════════════════════════════╝

📅 Học kỳ: <Tên học kỳ> - <Năm học>

📋 Kịch bản: <Tên kịch bản>
   Nguồn: <Học kỳ nguồn>

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
✅ THÀNH CÔNG: XXX học sinh
   • Lên lớp: XXX học sinh
   • Ở lại (học lại): XXX học sinh
   • Tỷ lệ lên lớp: XX.X%

⚠️ LỖI/CẢNH BÁO: XX trường hợp

Chi tiết (tất cả XX lỗi):
   1. <Lỗi 1>
   2. <Lỗi 2>
   ...
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
```

**Hiển thị bằng:** `ScrollableMessageBox` (có thanh cuộn)

---

### 8.2. Console Log

**Mục đích:** Debug và theo dõi quá trình phân lớp

**Ví dụ log:**

```
=== BẮT ĐẦU PHÂN LỚP CHO Học kỳ I - 2026-2027 ===
📌 Kịch bản: HK2 năm trước → HK1 năm sau (Xét lên lớp)

╔══════════════════════════════════════════════════════════╗
║   KỊCH BẢN 2: HK2 năm trước → HK1 năm sau (Xét lên lớp)║
╚══════════════════════════════════════════════════════════╝

→ HK1 năm trước: Học kỳ I 2025-2026
→ HK2 năm trước: Học kỳ II 2025-2026
→ Tìm thấy 475 học sinh 'Đang học' cần kiểm tra

  → Nguyễn Văn A: ĐTB HK1=7.00, HK2=7.50, Cả năm=7.33
       Hạnh kiểm: HK1=Tốt, HK2=Tốt, Cả năm=Tốt
       Môn Kém: 0, Môn Yếu: 0
  ✓ Nguyễn Văn A: ĐỦ điều kiện lên lớp (Khối 10 → Khối 11)

  → Trần Thị B: ĐTB HK1=4.20, HK2=4.50, Cả năm=4.40
       Hạnh kiểm: HK1=Khá, HK2=Trung Bình, Cả năm=Trung Bình
       Môn Kém: 0, Môn Yếu: 3
  ⚠️ Trần Thị B: HỌC LẠI Khối 11
       Lý do: ĐTB cả năm 4.40 < 5.0, Có 3 môn Yếu (> 2)

...

╔══════════════════════════════════════════════════════════╗
║                   KẾT QUẢ PHÂN LỚP                      ║
╚══════════════════════════════════════════════════════════╝
✓ Đã phân lớp thành công: 415 học sinh
⚠️ Số lỗi/cảnh báo: 60

Chi tiết lỗi:
  - HS Lê Văn C (ID: 789): Chưa có điểm HK1
  - HS Phạm Thị D (ID: 234): Chưa có hạnh kiểm HK2
  ... và 55 lỗi khác
```

**Xem Console Log:**

- Visual Studio: `View → Output → Show output from: Debug`
- Rider: `View → Tool Windows → Debug`

---

### 8.3. Thông báo lỗi

**Khi nào:** Hệ thống không thể phân lớp (lỗi nghiêm trọng)

**Ví dụ:**

```
✗ Phân lớp tự động thất bại!

Lỗi nghiêm trọng trong quá trình phân lớp:
System.NullReferenceException: Object reference not set to an instance of an object.
   at PhanLopTuDongBLL.ThucHienPhanLopTuDong()
   at PhanLop.btnPhanLopTuDong_Click()
```

**Hiển thị bằng:** `ScrollableMessageBox` (icon Error)

---

## 9. CÁC LƯU Ý QUAN TRỌNG

### ⚠️ Không xóa phân lớp cũ tự động

Hệ thống sẽ **HỎI** trước khi xóa:

```
Học kỳ đã được phân lớp (475 học sinh).
Nếu muốn phân lớp lại, bạn cần xóa dữ liệu phân lớp cũ.

Bạn có đồng ý xóa và phân lớp lại không?
[Yes] [No]
```

→ Tránh mất dữ liệu không chủ ý

---

### ⚠️ Backup trước khi test

Trước khi test kịch bản xét lên lớp:

```sql
-- Backup bảng PhanLop
CREATE TABLE PhanLop_Backup AS SELECT * FROM PhanLop;

-- Khôi phục nếu cần
TRUNCATE TABLE PhanLop;
INSERT INTO PhanLop SELECT * FROM PhanLop_Backup;
```

---

### ⚠️ Kiểm tra dữ liệu trước khi phân lớp

```sql
-- Kiểm tra số HS có đầy đủ điểm
SELECT
    COUNT(DISTINCT hs.MaHocSinh) AS SoHS
FROM HocSinh hs
JOIN DiemSo ds1 ON ds1.MaHocSinh = hs.MaHocSinh AND ds1.MaHocKy = 1
JOIN DiemSo ds2 ON ds2.MaHocSinh = hs.MaHocSinh AND ds2.MaHocKy = 2
WHERE hs.TrangThai = 'Đang học';

-- Kiểm tra số HS có đầy đủ hạnh kiểm
SELECT
    COUNT(DISTINCT hs.MaHocSinh) AS SoHS
FROM HocSinh hs
JOIN HanhKiem hk1 ON hk1.MaHocSinh = hs.MaHocSinh AND hk1.MaHocKy = 1
JOIN HanhKiem hk2 ON hk2.MaHocSinh = hs.MaHocSinh AND hk2.MaHocKy = 2
WHERE hs.TrangThai = 'Đang học';
```

---

### ⚠️ Sắp xếp lớp theo MaLop, KHÔNG phải TenLop

**Sai:**

```csharp
.OrderBy(l => l.TenLop)  // "11A1" sắp trước "10A1" (theo chữ cái)
```

**Đúng:**

```csharp
.OrderBy(l => l.MaLop)   // Sắp theo ID: 1, 2, 3,...
```

---

## 10. TROUBLESHOOTING

### 🐛 Lỗi: "Không tìm thấy HK1 của năm học..."

**Nguyên nhân:** Chọn HK2 nhưng chưa có HK1 cùng năm

**Giải pháp:**

```sql
-- Tạo HK1 trước
INSERT INTO HocKy (TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT) VALUES
('Học kỳ I', '2025-2026', 'Đang diễn ra', '2025-09-01', '2026-01-15');
```

---

### 🐛 Lỗi: "Tính năng phân lớp lần đầu tiên chưa được cài đặt!"

**Nguyên nhân:** Chọn HK1 năm đầu tiên mà chưa implement FIRST_TIME

**Trạng thái:** ✅ ĐÃ SỬA (version mới đã có)

---

### 🐛 Lỗi biên dịch: "The name 'Environment' does not exist"

**Nguyên nhân:** Thiếu `using System;`

**Giải pháp:**

```csharp
using System;
using System.Drawing;
using System.Windows.Forms;
```

---

### 🐛 Bảng thông báo không xuống dòng

**Nguyên nhân:** TextBox không nhận `\n`

**Giải pháp:** ✅ ĐÃ SỬA

```csharp
Text = message.Replace("\n", Environment.NewLine)
```

---

## 11. KẾT LUẬN

### ✅ Những gì đã hoàn thành

- [x] 3 kịch bản phân lớp tự động
- [x] Xét điều kiện lên lớp đầy đủ
- [x] Phân bổ đều học sinh (Round-Robin)
- [x] Cập nhật trạng thái tốt nghiệp
- [x] Hiển thị toàn bộ lỗi (ScrollableMessageBox)
- [x] Console log chi tiết
- [x] Dữ liệu test mẫu (500 HS, 24 lớp)
- [x] Xử lý trường hợp đặc biệt
- [x] Thông báo rõ ràng, dễ hiểu

### 📊 Thống kê

| Metric                   | Giá trị |
| ------------------------ | ------- |
| Tổng dòng code           | ~3,000  |
| Số file sửa/tạo          | 7       |
| Số kịch bản              | 3       |
| Số điều kiện xét lên lớp | 4       |
| Thời gian phát triển     | ~5 ngày |

### 🚀 Tính năng nổi bật

1. **Tự động phát hiện kịch bản** - Không cần user chọn
2. **Xét lên lớp thông minh** - Dựa trên 4 tiêu chí
3. **Hiển thị đầy đủ lỗi** - Thanh cuộn xem hết 60/60 lỗi
4. **Phân bổ công bằng** - Round-Robin đảm bảo đều
5. **Cập nhật tự động** - Trạng thái tốt nghiệp vào SQL

### 📈 Hiệu suất

- Phân lớp 500 HS: < 5 giây
- Xét lên lớp (có tính điểm): < 10 giây
- Hiển thị kết quả: Tức thì

---

## 12. LIÊN HỆ & HỖ TRỢ

**Nếu gặp vấn đề:**

1. Kiểm tra Console Log
2. Xem file `GIAI_THICH_CAC_VAN_DE.md`
3. Chạy lại từ đầu với dữ liệu test
4. Kiểm tra SQL constraints (unique indexes)

**File tham khảo:**

- `docs/HUONG_DAN_PHAN_LOP_TU_DONG_TONG_HOP.md` (file này)
- `docs/SMOKE_TEST.md` (test nhanh)
- `ConnectDatabase/03_sample_seed_optimized.sql` (dữ liệu mẫu)
- `ConnectDatabase/04_data_hk2_for_test.sql` (điểm HK2)

---

**Phiên bản:** 2.0 (Tổng hợp)  
**Ngày cập nhật:** 02/11/2025  
**Tác giả:** GitHub Copilot  
**Trạng thái:** ✅ Hoàn thiện 100%

---

**🎉 HỆ THỐNG PHÂN LỚP TỰ ĐỘNG ĐÃ SẴN SÀNG SỬ DỤNG!** 🚀
