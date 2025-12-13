# PHÂN TÍCH CHI TIẾT PHẦN PHÂN TRANG HỌC SINH

## 1. TỔNG QUAN

### 1.1. Mục đích

Phần **Phân trang học sinh** cho phép hiển thị danh sách học sinh và phụ huynh theo từng trang, giúp tối ưu hiệu suất khi có lượng dữ liệu lớn.

### 1.2. Vị trí code

- **File chính**: `GUI/GUIClass/HocSinh/HocSinh.cs`
- **Các method liên quan**:
  - `LoadPagedDataHocSinh()` - Dòng 409-487
  - `LoadPagedDataPhuHuynh()` - Dòng 858-912
  - `UpdatePaginationLabel()` - Dòng 490-534
  - `UpdatePaginationButtons()` - Dòng 537-586
  - `btnTrangSau_Click_1()` - Dòng 3815-3837
  - `btnTrangTruoc_Click_1()` - Dòng 3840-3858

---

## 2. CẤU TRÚC DỮ LIỆU

### 2.1. Biến quản lý phân trang

**Vị trí**: Dòng 46-54

```csharp
// ✅ PHÂN TRANG - Biến quản lý
private int currentPageHocSinh = 1; // Trang hiện tại
private int pageSizeHocSinh = 50; // Số dòng mỗi trang
private List<HocSinhDTO> danhSachHocSinhFiltered; // Danh sách sau khi tìm kiếm/lọc

// ✅ PHÂN TRANG PHỤ HUYNH - Biến quản lý
private int currentPagePhuHuynh = 1; // Trang hiện tại
private int pageSizePhuHuynh = 50; // Số dòng mỗi trang
private List<PhuHuynhDTO> danhSachPhuHuynhFiltered; // Danh sách sau khi tìm kiếm/lọc
```

**Giải thích**:

- `currentPageHocSinh/PhuHuynh`: Trang hiện tại đang xem (bắt đầu từ 1)
- `pageSizeHocSinh/PhuHuynh`: Số lượng bản ghi hiển thị trên mỗi trang (mặc định 50)
- `danhSachHocSinhFiltered/PhuHuynhFiltered`: Danh sách đã được lọc/tìm kiếm, đây là dữ liệu nguồn để phân trang

---

## 3. THUẬT TOÁN PHÂN TRANG

### 3.1. Load dữ liệu theo trang (Học sinh)

**Method**: `LoadPagedDataHocSinh()`  
**Vị trí**: Dòng 409-487

#### Bước 1: Tính toán số trang

```csharp
int totalRecords = danhSachHocSinhFiltered.Count;
int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizeHocSinh);
```

**Công thức**:

- `totalPages = ⌈totalRecords / pageSize⌉` (làm tròn lên)
- Ví dụ: 127 bản ghi, pageSize = 50 → totalPages = 3

#### Bước 2: Kiểm tra và điều chỉnh trang hiện tại

```csharp
if (currentPageHocSinh < 1) currentPageHocSinh = 1;
if (currentPageHocSinh > totalPages && totalPages > 0) currentPageHocSinh = totalPages;
```

**Logic**:

- Nếu trang hiện tại < 1 → Đặt về trang 1
- Nếu trang hiện tại > tổng số trang → Đặt về trang cuối

#### Bước 3: Lấy dữ liệu của trang hiện tại (LINQ Skip/Take)

```csharp
var pagedData = danhSachHocSinhFiltered
    .Skip((currentPageHocSinh - 1) * pageSizeHocSinh)
    .Take(pageSizeHocSinh)
    .ToList();
```

**Thuật toán**:

- **Skip(n)**: Bỏ qua n bản ghi đầu tiên
  - Trang 1: Skip(0) → Bỏ qua 0 bản ghi
  - Trang 2: Skip(50) → Bỏ qua 50 bản ghi đầu
  - Trang 3: Skip(100) → Bỏ qua 100 bản ghi đầu
- **Take(m)**: Lấy m bản ghi tiếp theo
  - Luôn lấy `pageSize` bản ghi (50)

**Công thức tổng quát**:

```
startIndex = (currentPage - 1) * pageSize
endIndex = startIndex + pageSize
```

**Ví dụ**:

- Trang 1: Skip(0), Take(50) → Hiển thị bản ghi 1-50
- Trang 2: Skip(50), Take(50) → Hiển thị bản ghi 51-100
- Trang 3: Skip(100), Take(50) → Hiển thị bản ghi 101-127

#### Bước 4: Hiển thị dữ liệu lên DataGridView

```csharp
foreach (HocSinhDTO hs in pagedData)
{
    bindingListHocSinh.Add(hs);
    // ... Thêm vào tableHocSinh.Rows
}
```

#### Bước 5: Cập nhật UI phân trang

```csharp
UpdatePaginationLabel(totalPages, totalRecords);
UpdatePaginationButtons(totalPages);
```

---

### 3.2. Load dữ liệu theo trang (Phụ huynh)

**Method**: `LoadPagedDataPhuHuynh()`  
**Vị trí**: Dòng 858-912

**Thuật toán tương tự** như `LoadPagedDataHocSinh()`, chỉ khác:

- Sử dụng `currentPagePhuHuynh` và `pageSizePhuHuynh`
- Lấy dữ liệu từ `danhSachPhuHuynhFiltered`
- Gọi `UpdatePaginationLabelPhuHuynh()` và `UpdatePaginationButtonsPhuHuynh()`

---

## 4. CẬP NHẬT GIAO DIỆN PHÂN TRANG

### 4.1. Cập nhật Label hiển thị trang

**Method**: `UpdatePaginationLabel(int totalPages, int totalRecords)`  
**Vị trí**: Dòng 490-534

#### Chức năng

Hiển thị thông tin trang hiện tại dạng: **"Trang X/Y (Z học sinh)"**

#### Cách hoạt động

```csharp
int currentPage = isShowingHocSinh ? currentPageHocSinh : currentPagePhuHuynh;
string entityName = isShowingHocSinh ? "học sinh" : "phụ huynh";

// Tìm label trong form
foreach (Control ctrl in this.Controls)
{
    if (ctrl is Label && (ctrl.Name.Contains("Trang") || ctrl.Name.Contains("lblPaging")))
    {
        if (totalPages == 0)
            ctrl.Text = $"Trang 0/0 (0 {entityName})";
        else
            ctrl.Text = $"Trang {currentPage}/{totalPages} ({totalRecords} {entityName})";
        return;
    }
}

// Tìm đệ quy trong các container con
FindAndUpdateLabel(this, totalPages, totalRecords, currentPage, entityName);
```

**Logic**:

1. Xác định đang ở tab Học sinh hay Phụ huynh
2. Tìm label có tên chứa "Trang" hoặc "lblPaging"
3. Cập nhật text theo format: `"Trang {currentPage}/{totalPages} ({totalRecords} {entityName})"`
4. Nếu không tìm thấy, tìm đệ quy trong các container con

**Ví dụ hiển thị**:

- `"Trang 1/3 (127 học sinh)"`
- `"Trang 2/3 (127 học sinh)"`
- `"Trang 0/0 (0 học sinh)"` (khi không có dữ liệu)

---

### 4.2. Cập nhật nút điều hướng

**Method**: `UpdatePaginationButtons(int totalPages)`  
**Vị trí**: Dòng 537-586

#### Chức năng

Enable/Disable các nút "Trang trước" và "Trang sau" dựa trên vị trí trang hiện tại.

#### Cách hoạt động

```csharp
int currentPage = isShowingHocSinh ? currentPageHocSinh : currentPagePhuHuynh;

// Tìm nút "Trang Trước"
foreach (Control ctrl in this.Controls)
{
    if (ctrl is Button && (ctrl.Name.ToLower().Contains("truoc") || ctrl.Name.ToLower().Contains("prev")))
    {
        ctrl.Enabled = (currentPage > 1); // Enable nếu không phải trang đầu
    }

    if (ctrl is Button && (ctrl.Name.ToLower().Contains("sau") || ctrl.Name.ToLower().Contains("next")))
    {
        ctrl.Enabled = (currentPage < totalPages); // Enable nếu không phải trang cuối
    }
}
```

**Logic**:

- **Nút "Trang trước"**:
  - Enable khi `currentPage > 1` (không phải trang đầu)
  - Disable khi `currentPage = 1` (đang ở trang đầu)
- **Nút "Trang sau"**:
  - Enable khi `currentPage < totalPages` (không phải trang cuối)
  - Disable khi `currentPage = totalPages` (đang ở trang cuối)

**Ví dụ**:

- Trang 1/3: "Trang trước" = Disabled, "Trang sau" = Enabled
- Trang 2/3: "Trang trước" = Enabled, "Trang sau" = Enabled
- Trang 3/3: "Trang trước" = Enabled, "Trang sau" = Disabled

---

## 5. ĐIỀU HƯỚNG TRANG

### 5.1. Nút "Trang sau"

**Method**: `btnTrangSau_Click_1(object sender, EventArgs e)`  
**Vị trí**: Dòng 3815-3837

```csharp
if (isShowingHocSinh)
{
    int totalPages = (int)Math.Ceiling((double)danhSachHocSinhFiltered.Count / pageSizeHocSinh);
    if (currentPageHocSinh < totalPages)
    {
        currentPageHocSinh++;
        LoadPagedDataHocSinh();
    }
}
else
{
    // Tương tự cho Phụ huynh
    int totalPages = (int)Math.Ceiling((double)danhSachPhuHuynhFiltered.Count / pageSizePhuHuynh);
    if (currentPagePhuHuynh < totalPages)
    {
        currentPagePhuHuynh++;
        LoadPagedDataPhuHuynh();
    }
}
```

**Logic**:

1. Kiểm tra có phải trang cuối không (`currentPage < totalPages`)
2. Nếu không phải trang cuối → Tăng `currentPage` lên 1
3. Gọi `LoadPagedDataHocSinh()` để load dữ liệu trang mới

---

### 5.2. Nút "Trang trước"

**Method**: `btnTrangTruoc_Click_1(object sender, EventArgs e)`  
**Vị trí**: Dòng 3840-3858

```csharp
if (isShowingHocSinh)
{
    if (currentPageHocSinh > 1)
    {
        currentPageHocSinh--;
        LoadPagedDataHocSinh();
    }
}
else
{
    // Tương tự cho Phụ huynh
    if (currentPagePhuHuynh > 1)
    {
        currentPagePhuHuynh--;
        LoadPagedDataPhuHuynh();
    }
}
```

**Logic**:

1. Kiểm tra có phải trang đầu không (`currentPage > 1`)
2. Nếu không phải trang đầu → Giảm `currentPage` xuống 1
3. Gọi `LoadPagedDataHocSinh()` để load dữ liệu trang mới

---

## 6. TƯƠNG TÁC VỚI TÌM KIẾM VÀ LỌC

### 6.1. Reset về trang 1 khi tìm kiếm

**Vị trí**: Dòng 404, 853

Khi người dùng thực hiện tìm kiếm hoặc lọc dữ liệu:

```csharp
currentPageHocSinh = 1; // Reset về trang 1
LoadPagedDataHocSinh(); // Load trang đầu tiên
```

**Lý do**: Sau khi lọc, danh sách `danhSachHocSinhFiltered` thay đổi, cần reset về trang 1 để hiển thị kết quả từ đầu.

---

### 6.2. Điều chỉnh trang khi xóa dữ liệu

**Vị trí**: Dòng 1623-1627, 1680-1684

Khi xóa học sinh/phụ huynh, nếu trang hiện tại vượt quá tổng số trang mới:

```csharp
int totalPages = (int)Math.Ceiling((double)totalRecords / pageSizeHocSinh);
if (totalPages > 0 && currentPageHocSinh > totalPages)
{
    currentPageHocSinh = totalPages; // Chuyển về trang cuối
    LoadPagedDataHocSinh();
}
```

**Ví dụ**:

- Trước khi xóa: 127 bản ghi, đang ở trang 3/3
- Sau khi xóa: 95 bản ghi, chỉ còn 2 trang
- → Tự động chuyển về trang 2/2

---

## 7. SƠ ĐỒ HOẠT ĐỘNG

```
┌─────────────────────────────────────────────────────────┐
│ 1. Load dữ liệu ban đầu                                 │
│    → LoadSampleDataHocSinh()                            │
│    → danhSachHocSinhFiltered = [danh sách đã lọc]      │
└─────────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────────┐
│ 2. Tính toán phân trang                                 │
│    → totalPages = ⌈totalRecords / pageSize⌉            │
│    → currentPage = 1 (reset về trang đầu)              │
└─────────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────────┐
│ 3. Lấy dữ liệu trang hiện tại                           │
│    → startIndex = (currentPage - 1) * pageSize        │
│    → pagedData = danhSachFiltered                      │
│         .Skip(startIndex)                              │
│         .Take(pageSize)                                │
└─────────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────────┐
│ 4. Hiển thị lên DataGridView                            │
│    → bindingList.Add(pagedData)                         │
│    → tableHocSinh.Rows.Add(...)                        │
└─────────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────────┐
│ 5. Cập nhật UI phân trang                               │
│    → UpdatePaginationLabel()                            │
│    → UpdatePaginationButtons()                         │
└─────────────────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────────────────┐
│ 6. Người dùng click "Trang sau/trước"                   │
│    → currentPage++ hoặc currentPage--                  │
│    → Quay lại bước 3                                    │
└─────────────────────────────────────────────────────────┘
```

---

## 8. TÓM TẮT THUẬT TOÁN

### 8.1. Công thức tính số trang

```
totalPages = ⌈totalRecords / pageSize⌉
```

### 8.2. Công thức lấy dữ liệu trang

```
startIndex = (currentPage - 1) * pageSize
pagedData = danhSachFiltered.Skip(startIndex).Take(pageSize)
```

### 8.3. Điều kiện Enable/Disable nút

- **Nút "Trang trước"**: `Enabled = (currentPage > 1)`
- **Nút "Trang sau"**: `Enabled = (currentPage < totalPages)`

---

## 9. ĐIỂM MẠNH VÀ HẠN CHẾ

### 9.1. Điểm mạnh

✅ **Hiệu suất tốt**: Chỉ load và hiển thị 50 bản ghi mỗi trang, không load toàn bộ dữ liệu  
✅ **Dễ sử dụng**: Giao diện đơn giản với 2 nút điều hướng  
✅ **Tự động điều chỉnh**: Tự động reset về trang 1 khi tìm kiếm/lọc  
✅ **Hỗ trợ cả Học sinh và Phụ huynh**: Cùng một cơ chế phân trang

### 9.2. Hạn chế

⚠️ **Không có nhảy trang**: Chỉ có nút "Trang trước" và "Trang sau", không thể nhảy đến trang cụ thể  
⚠️ **PageSize cố định**: Không cho phép người dùng thay đổi số lượng bản ghi mỗi trang  
⚠️ **Phân trang client-side**: Dữ liệu được load toàn bộ vào memory, sau đó mới phân trang (không phải server-side pagination)

---

## 10. KẾT LUẬN

Phần phân trang học sinh sử dụng **thuật toán phân trang client-side** với LINQ `Skip()` và `Take()`, giúp tối ưu hiệu suất hiển thị khi có lượng dữ liệu lớn. Thuật toán đơn giản, dễ hiểu và phù hợp với nhu cầu của ứng dụng quản lý học sinh.
