# HỆ THỐNG PHÂN QUYỀN - STUDENT MANAGEMENT SYSTEM

## 📋 MỤC LỤC
1. [Tổng quan hệ thống phân quyền](#1-tổng-quan-hệ-thống-phân-quyền)
2. [Cấu trúc Database](#2-cấu-trúc-database)
3. [Kiến trúc phân quyền](#3-kiến-trúc-phân-quyền)
4. [Luồng xử lý phân quyền](#4-luồng-xử-lý-phân-quyền)
5. [Các thành phần chính](#5-các-thành-phần-chính)
6. [Ví dụ cụ thể](#6-ví-dụ-cụ-thể)
7. [Best Practices](#7-best-practices)

---

## 1. TỔNG QUAN HỆ THỐNG PHÂN QUYỀN

### 1.1. Khái niệm
Hệ thống phân quyền được thiết kế theo mô hình **Role-Based Access Control (RBAC)** với 4 cấp độ:

```
Người dùng (User) 
    ↓
Vai trò (Role) 
    ↓
Chức năng (Function) 
    ↓
Hành động (Action: Read, Create, Update, Delete)
```

### 1.2. Các thành phần cốt lõi

- **Vai trò (Role)**: Nhóm quyền được gán cho người dùng (VD: Admin, Giáo vụ, Giáo viên, Học sinh)
- **Chức năng (Function)**: Module trong hệ thống (VD: Quản lý điểm, Quản lý học sinh)
- **Hành động (Action)**: Thao tác cụ thể trên chức năng:
  - `read`: Xem dữ liệu
  - `create`: Thêm mới
  - `update`: Chỉnh sửa
  - `delete`: Xóa

---

## 2. CẤU TRÚC DATABASE

### 2.1. Các bảng chính

```sql
-- Bảng vai trò
VaiTro (MaVaiTro, TenVaiTro, MoTa)

-- Bảng chức năng
ChucNang (MaChucNang, TenChucNang, MoTa)

-- Bảng liên kết vai trò - chức năng
VaiTroChucNang (MaVaiTro, MaChucNang)

-- Bảng hành động của chức năng
ChucNangHanhDong (MaChucNang, HanhDong)

-- Bảng chi tiết quyền (Vai trò + Chức năng + Hành động)
VaiTroChucNangHanhDong (MaVaiTro, MaChucNang, HanhDong)

-- Bảng gán vai trò cho người dùng
NguoiDungVaiTro (TenDangNhap, MaVaiTro)
```

### 2.2. Mối quan hệ

```
NguoiDung (1) ──→ (N) NguoiDungVaiTro (N) ──→ (1) VaiTro
                                                      │
                                                      │
VaiTroChucNangHanhDong (N) ──→ (1) VaiTro
VaiTroChucNangHanhDong (N) ──→ (1) ChucNang
VaiTroChucNangHanhDong (N) ──→ (1) ChucNangHanhDong
```

---

## 3. KIẾN TRÚC PHÂN QUYỀN

### 3.1. SessionManager - Quản lý phiên đăng nhập

**File**: `BUS/Utils/SessionManager.cs`

**Chức năng**: Lưu trữ thông tin người dùng hiện tại trong session

```csharp
public static class SessionManager
{
    public static string TenDangNhap { get; set; }
    public static string HoTen { get; set; }
    public static string Email { get; set; }
    public static string VaiTro { get; private set; }
    public static List<string> DanhSachVaiTro { get; private set; }
    
    // Kiểm tra đăng nhập
    public static bool IsLoggedIn()
    
    // Đăng nhập - lưu thông tin
    public static void Login(string tenDangNhap, ...)
    
    // Đăng xuất - xóa thông tin
    public static void Logout()
}
```

**Luồng hoạt động**:
1. Khi người dùng đăng nhập thành công → `SessionManager.Login()` được gọi
2. Thông tin người dùng được lưu vào static properties
3. Các form khác sử dụng `SessionManager.TenDangNhap` để kiểm tra quyền

---

### 3.2. PermissionHelper - Trung tâm kiểm tra quyền

**File**: `BUS/Utils/PermissionHelper.cs`

**Chức năng**: Cung cấp các phương thức kiểm tra và áp dụng phân quyền

#### 3.2.1. Các hằng số định nghĩa

```csharp
// Mã chức năng
public const string QLDIEM = "qldiem";
public const string QLHOCSINH = "qlhocsinh";
public const string QLTAIKHOAN = "qltaikhoan";
// ... (14 chức năng tổng cộng)

// Hành động
public const string READ = "read";
public const string CREATE = "create";
public const string UPDATE = "update";
public const string DELETE = "delete";
```

#### 3.2.2. Các phương thức kiểm tra quyền

**a) `HasPermission(maChucNang, hanhDong)`**
- **Mục đích**: Kiểm tra người dùng có quyền thực hiện hành động cụ thể trên chức năng không
- **Luồng xử lý**:
  1. Kiểm tra đã đăng nhập chưa (`SessionManager.IsLoggedIn()`)
  2. Lấy danh sách vai trò của người dùng (`phanQuyenBUS.GetVaiTroByNguoiDung()`)
  3. Với mỗi vai trò, kiểm tra có quyền trong `VaiTroChucNangHanhDong` không
  4. Trả về `true` nếu có ít nhất 1 vai trò có quyền

```csharp
public static bool HasPermission(string maChucNang, string hanhDong)
{
    if (!SessionManager.IsLoggedIn())
        return false;
    
    return phanQuyenBUS.KiemTraQuyenNguoiDung(
        SessionManager.TenDangNhap,
        maChucNang,
        hanhDong);
}
```

**b) `HasAccessToFunction(maChucNang)`**
- **Mục đích**: Kiểm tra người dùng có quyền TRUY CẬP chức năng không (chỉ cần có 1 trong 4 quyền: read/create/update/delete)
- **Luồng xử lý**:
  1. Lấy danh sách vai trò của người dùng
  2. Kiểm tra từng vai trò có bất kỳ quyền nào trên chức năng không
  3. Trả về `true` nếu có ít nhất 1 quyền

```csharp
public static bool HasAccessToFunction(string maChucNang)
{
    // Kiểm tra có bất kỳ quyền nào (READ/CREATE/UPDATE/DELETE)
    if (HasPermission(maChucNang, READ) ||
        HasPermission(maChucNang, CREATE) ||
        HasPermission(maChucNang, UPDATE) ||
        HasPermission(maChucNang, DELETE))
    {
        return true;
    }
    return false;
}
```

#### 3.2.3. Các phương thức áp dụng quyền lên UI

**a) `SetButtonPermission(button, maChucNang, hanhDong)`**
- **Mục đích**: Ẩn/hiện và vô hiệu hóa button dựa trên quyền
- **Cách hoạt động**:
  - Nếu có quyền → `Visible = true`, `Enabled = true`
  - Nếu không có quyền → `Visible = false`, `Enabled = false`

```csharp
public static void SetButtonPermission(Control button, string maChucNang, string hanhDong)
{
    bool hasPermission = HasPermission(maChucNang, hanhDong);
    button.Visible = hasPermission;
    button.Enabled = hasPermission;
}
```

**b) `ApplyPermissionTaiKhoan(...)` - Ví dụ áp dụng cho form**
- **Mục đích**: Áp dụng phân quyền cho toàn bộ form Tài khoản
- **Cách hoạt động**:
  1. Ẩn/hiện button "Thêm tài khoản" (CREATE)
  2. Ẩn/hiện button "Quản lý vai trò" (CREATE)
  3. Lưu quyền vào `Tag` của DataGridView để xử lý trong `CellPainting`

```csharp
public static void ApplyPermissionTaiKhoan(
    Control btnAddAcc,
    Control btnVaiTro,
    DataGridView tbTaiKhoan)
{
    // Ẩn/hiện nút Thêm tài khoản
    SetButtonPermission(btnAddAcc, QLTAIKHOAN, CREATE);
    
    // Ẩn/hiện nút Quản lý vai trò
    SetButtonPermission(btnVaiTro, QLTAIKHOAN, CREATE);
    
    // Lưu quyền vào Tag của DataGridView
    if (tbTaiKhoan != null)
    {
        tbTaiKhoan.Tag = new
        {
            CanUpdate = HasPermission(QLTAIKHOAN, UPDATE),
            CanDelete = HasPermission(QLTAIKHOAN, DELETE)
        };
    }
}
```

---

### 3.3. PhanQuyenBUS - Business Logic Layer

**File**: `BUS/BUSClass/PhanQuyenBUS.cs`

**Chức năng**: Xử lý logic nghiệp vụ phân quyền

#### 3.3.1. Các phương thức quan trọng

**a) `KiemTraQuyenNguoiDung(tenDangNhap, maChucNang, hanhDong)`**
- **Mục đích**: Kiểm tra người dùng có quyền không
- **Luồng xử lý**:
  1. Lấy danh sách vai trò của người dùng (`GetVaiTroByNguoiDung()`)
  2. Với mỗi vai trò, kiểm tra có quyền trong database không (`KiemTraQuyen()`)
  3. Trả về `true` nếu có ít nhất 1 vai trò có quyền

```csharp
public bool KiemTraQuyenNguoiDung(string tenDangNhap, string maChucNang, string hanhDong)
{
    List<string> danhSachVaiTro = GetVaiTroByNguoiDung(tenDangNhap);
    
    foreach (string maVaiTro in danhSachVaiTro)
    {
        if (KiemTraQuyen(maVaiTro, maChucNang, hanhDong))
        {
            return true;
        }
    }
    
    return false;
}
```

**b) `GetVaiTroByNguoiDung(tenDangNhap)`**
- **Mục đích**: Lấy danh sách mã vai trò của người dùng
- **SQL Query**:
```sql
SELECT MaVaiTro 
FROM NguoiDungVaiTro 
WHERE TenDangNhap = @TenDangNhap
```

**c) `KiemTraQuyen(maVaiTro, maChucNang, hanhDong)`**
- **Mục đích**: Kiểm tra vai trò có quyền cụ thể không
- **SQL Query**:
```sql
SELECT COUNT(*) 
FROM VaiTroChucNangHanhDong 
WHERE MaVaiTro = @MaVaiTro 
  AND MaChucNang = @MaChucNang 
  AND HanhDong = @HanhDong
```

---

### 3.4. PhanQuyenDAO - Data Access Layer

**File**: `DAO/DAOClass/PhanQuyenDAO.cs`

**Chức năng**: Truy vấn database để lấy thông tin phân quyền

#### 3.4.1. Các phương thức quan trọng

**a) `GetVaiTroByNguoiDung(tenDangNhap)`**
- Trả về danh sách mã vai trò của người dùng từ bảng `NguoiDungVaiTro`

**b) `KiemTraQuyen(maVaiTro, maChucNang, hanhDong)`**
- Kiểm tra trong bảng `VaiTroChucNangHanhDong` xem có quyền không

**c) `GetChiTietVaiTro(maVaiTro)`**
- Lấy chi tiết đầy đủ quyền của vai trò (chức năng + hành động)

---

## 4. LUỒNG XỬ LÝ PHÂN QUYỀN

### 4.1. Luồng kiểm tra quyền từ UI đến Database

```
┌─────────────────────────────────────────────────────────────┐
│ 1. USER CLICKS BUTTON / ACCESSES FORM                       │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 2. FORM LOADS → ApplyPermissions()                          │
│    - FrmTaiKhoan.cs: ApplyPermissions()                    │
│    - Calls: PermissionHelper.ApplyPermissionTaiKhoan()    │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 3. PermissionHelper.CheckPermission()                       │
│    - HasPermission(QLTAIKHOAN, CREATE)                     │
│    - HasAccessToFunction(QLTAIKHOAN)                       │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 4. PhanQuyenBUS.KiemTraQuyenNguoiDung()                    │
│    - GetVaiTroByNguoiDung(SessionManager.TenDangNhap)     │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 5. PhanQuyenDAO.KiemTraQuyen()                              │
│    - Query: SELECT COUNT(*) FROM VaiTroChucNangHanhDong... │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 6. RETURN RESULT                                            │
│    - true: Có quyền → Show button / Enable action           │
│    - false: Không có quyền → Hide button / Disable action  │
└─────────────────────────────────────────────────────────────┘
```

### 4.2. Luồng áp dụng quyền lên UI

```
┌─────────────────────────────────────────────────────────────┐
│ FORM LOAD                                                  │
│   ↓                                                        │
│ ApplyPermissions()                                         │
│   ↓                                                        │
│ PermissionHelper.ApplyPermissionXXX()                      │
│   ├─→ SetButtonPermission() → Ẩn/hiện button            │
│   ├─→ Set DataGridView.Tag → Lưu quyền                    │
│   └─→ CellPainting → Vẽ icon theo quyền                  │
└─────────────────────────────────────────────────────────────┘
```

---

## 5. CÁC THÀNH PHẦN CHÍNH

### 5.1. Form Level - Kiểm tra quyền truy cập

**Ví dụ**: `frmMain.cs` - Kiểm tra trước khi mở form

```csharp
private void ShowTaiKhoan()
{
    // ✅ KIỂM TRA QUYỀN TRUY CẬP TRƯỚC KHI LOAD FORM
    if (!PermissionHelper.HasAccessToFunction(PermissionHelper.QLTAIKHOAN))
    {
        MessageBox.Show("Bạn không có quyền truy cập chức năng 'Quản lý tài khoản'!",
                       "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return; // ⛔ DỪNG LẠI - KHÔNG LOAD FORM
    }
    
    ucHeader1.UpdateHeader("Tài khoản", "Trang chủ / Tài khoản");
    LoadControlToPanel<FrmTaiKhoan>();
}
```

**Luồng**:
1. User click button "Tài khoản" trên sidebar
2. `ShowTaiKhoan()` được gọi
3. Kiểm tra `HasAccessToFunction(QLTAIKHOAN)`
4. Nếu không có quyền → Hiển thị thông báo và return
5. Nếu có quyền → Load form

---

### 5.2. Control Level - Ẩn/hiện button

**Ví dụ**: `FrmTaiKhoan.cs` - Áp dụng quyền cho button

```csharp
private void ApplyPermissions()
{
    // ✅ ÁP DỤNG PHÂN QUYỀN CHO FORM
    PermissionHelper.ApplyPermissionTaiKhoan(
        btnAddAcc,      // Button thêm tài khoản
        btnVaiTro,      // Button quản lý vai trò
        tbTaiKhoan      // DataGridView hiển thị danh sách
    );
}
```

**Kết quả**:
- Nếu có quyền `CREATE` → Button "Thêm tài khoản" hiển thị
- Nếu không có quyền → Button bị ẩn

---

### 5.3. DataGridView Level - Icon Sửa/Xóa

**Ví dụ**: `FrmTaiKhoan.cs` - Vẽ icon theo quyền trong `CellPainting`

```csharp
private void TbTaiKhoan_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
{
    if (e.RowIndex >= 0 && e.ColumnIndex == tbTaiKhoan.Columns["thaoTac"].Index)
    {
        // ✅ LẤY QUYỀN TRỰC TIẾP TỪ PermissionHelper
        bool canUpdate = PermissionHelper.HasPermission(
            PermissionHelper.QLTAIKHOAN, 
            PermissionHelper.UPDATE
        );
        bool canDelete = PermissionHelper.HasPermission(
            PermissionHelper.QLTAIKHOAN, 
            PermissionHelper.DELETE
        );
        
        // ✅ VẼ ICON THEO QUYỀN
        if (canUpdate)
        {
            // Vẽ icon shield (sửa) rõ nét
            e.Graphics.DrawImage(shield, ...);
        }
        else
        {
            // Vẽ icon shield mờ 30%
            e.Graphics.DrawImage(shield, ..., opacity: 0.3f);
        }
        
        // Tương tự với icon lock và bin
    }
}
```

**Luồng**:
1. `CellPainting` được gọi khi vẽ mỗi cell
2. Kiểm tra quyền `UPDATE` và `DELETE`
3. Vẽ icon rõ nét nếu có quyền, mờ nếu không có quyền

---

### 5.4. Action Level - Kiểm tra khi click

**Ví dụ**: `FrmTaiKhoan.cs` - Kiểm tra quyền khi click icon

```csharp
private void TbTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex >= 0 && e.ColumnIndex == tbTaiKhoan.Columns["thaoTac"].Index)
    {
        // ✅ KIỂM TRA QUYỀN TRƯỚC KHI THỰC HIỆN HÀNH ĐỘNG
        bool canUpdate = PermissionHelper.HasPermission(
            PermissionHelper.QLTAIKHOAN, 
            PermissionHelper.UPDATE
        );
        
        // Xác định icon nào được click
        if (x < shieldRight) // Icon shield (sửa)
        {
            if (!canUpdate)
            {
                MessageBox.Show("Bạn không có quyền chỉnh sửa...");
                return; // ⛔ DỪNG LẠI
            }
            
            // ✅ CÓ QUYỀN → THỰC HIỆN HÀNH ĐỘNG
            // Mở form sửa tài khoản
        }
    }
}
```

**Luồng**:
1. User click icon trên DataGridView
2. `CellClick` được gọi
3. Kiểm tra quyền tương ứng (UPDATE/DELETE)
4. Nếu không có quyền → Hiển thị thông báo và return
5. Nếu có quyền → Thực hiện hành động

---

## 6. VÍ DỤ CỤ THỂ

### 6.1. Ví dụ 1: Form Tài khoản

**File**: `GUI/GUIClass/TaiKhoan/FrmTaiKhoan.cs`

#### Bước 1: Form Load
```csharp
private void TaiKhoan_Load(object sender, EventArgs e)
{
    SetupThongKeCards();
    SetupTaiKhoanTable();
    LoadTaiKhoanData();
    ApplyPermissions(); // ✅ ÁP DỤNG PHÂN QUYỀN
}
```

#### Bước 2: Áp dụng quyền
```csharp
private void ApplyPermissions()
{
    // Kiểm tra quyền truy cập
    if (!PermissionHelper.HasAccessToFunction(PermissionHelper.QLTAIKHOAN))
    {
        MessageBox.Show("Bạn không có quyền...");
        this.Enabled = false;
        return;
    }
    
    // Áp dụng quyền cho các control
    PermissionHelper.ApplyPermissionTaiKhoan(
        btnAddAcc,
        btnVaiTro,
        tbTaiKhoan
    );
}
```

#### Bước 3: Vẽ icon trong CellPainting
```csharp
private void TbTaiKhoan_CellPainting(...)
{
    // Lấy quyền
    bool canUpdate = PermissionHelper.HasPermission(QLTAIKHOAN, UPDATE);
    bool canDelete = PermissionHelper.HasPermission(QLTAIKHOAN, DELETE);
    
    // Vẽ icon theo quyền
    if (canUpdate)
        e.Graphics.DrawImage(shield, ...); // Rõ nét
    else
        e.Graphics.DrawImage(shield, ..., opacity: 0.3f); // Mờ
}
```

#### Bước 4: Kiểm tra khi click
```csharp
private void TbTaiKhoan_CellClick(...)
{
    bool canUpdate = PermissionHelper.HasPermission(QLTAIKHOAN, UPDATE);
    
    if (!canUpdate)
    {
        MessageBox.Show("Bạn không có quyền...");
        return;
    }
    
    // Thực hiện hành động
}
```

---

### 6.2. Ví dụ 2: Sidebar - Ẩn/hiện menu

**File**: `GUI/GUIClass/Forms/frmMain.cs`

```csharp
private void ApplySidebarPermissions()
{
    // Quản lý tài khoản
    if (ucSidebar1.TaiKhoanButton != null)
    {
        bool hasAccess = PermissionHelper.HasAccessToFunction(
            PermissionHelper.QLTAIKHOAN
        );
        ucSidebar1.TaiKhoanButton.Visible = hasAccess;
        ucSidebar1.TaiKhoanButton.Enabled = hasAccess;
    }
    
    // Tương tự cho các menu khác...
}
```

**Kết quả**:
- Nếu user không có quyền truy cập "Quản lý tài khoản" → Button "Tài khoản" bị ẩn trên sidebar
- Nếu user có quyền → Button hiển thị bình thường

---

### 6.3. Ví dụ 3: Form Thêm phân quyền

**File**: `GUI/GUIClass/TaiKhoan/frmAddPhanQuyen.cs`

**Chức năng**: Cho phép Admin tạo vai trò mới và gán quyền

```csharp
private void btnAddQuyen_Click(object sender, EventArgs e)
{
    // 1. Validate input
    string tenVaiTro = txtTenPhanQuyen.Text.Trim();
    
    // 2. Thu thập quyền từ checkbox
    Dictionary<string, List<string>> danhSachQuyen = new Dictionary<string, List<string>>();
    
    foreach (var item in checkBoxDict)
    {
        string maChucNang = item.Key;
        var checkBoxes = item.Value;
        List<string> hanhDongs = new List<string>();
        
        // Xử lý checkbox "Xem" → "read"
        if (checkBoxes["xem"].Checked)
            hanhDongs.Add("read");
        
        // Xử lý checkbox "Thêm" → "create"
        if (checkBoxes["them"].Checked)
            hanhDongs.Add("create");
        
        // Tương tự cho "Sửa" và "Xóa"
        
        // Thêm vào danh sách
        if (hanhDongs.Count > 0)
            danhSachQuyen.Add(maChucNang, hanhDongs);
    }
    
    // 3. Lưu vào database
    bool success = phanQuyenBUS.ThemVaiTroVoiQuyen(tenVaiTro, danhSachQuyen);
}
```

**Luồng**:
1. Admin chọn các checkbox quyền cho từng chức năng
2. Click "Thêm quyền"
3. Hệ thống thu thập quyền đã chọn
4. Lưu vào database:
   - Thêm vào bảng `VaiTro`
   - Thêm vào bảng `VaiTroChucNang`
   - Thêm vào bảng `VaiTroChucNangHanhDong`

---

## 7. BEST PRACTICES

### 7.1. Kiểm tra quyền ở nhiều lớp

✅ **NÊN**:
- Kiểm tra ở **Form Level** (trước khi load form)
- Kiểm tra ở **Control Level** (ẩn/hiện button)
- Kiểm tra ở **Action Level** (trước khi thực hiện hành động)

❌ **KHÔNG NÊN**:
- Chỉ kiểm tra ở 1 lớp duy nhất
- Tin tưởng hoàn toàn vào UI (có thể bị bypass)

---

### 7.2. Sử dụng PermissionHelper thống nhất

✅ **NÊN**:
```csharp
// Sử dụng PermissionHelper
bool canUpdate = PermissionHelper.HasPermission(QLTAIKHOAN, UPDATE);
```

❌ **KHÔNG NÊN**:
```csharp
// Truy vấn trực tiếp database trong form
bool canUpdate = phanQuyenBUS.KiemTraQuyenNguoiDung(...);
```

**Lý do**: 
- PermissionHelper đã xử lý logic kiểm tra session
- Dễ bảo trì và thống nhất trong toàn bộ ứng dụng

---

### 7.3. Lưu quyền vào Tag của DataGridView

✅ **NÊN**:
```csharp
// Lưu quyền vào Tag
tbTaiKhoan.Tag = new
{
    CanUpdate = HasPermission(QLTAIKHOAN, UPDATE),
    CanDelete = HasPermission(QLTAIKHOAN, DELETE)
};

// Sử dụng trong CellPainting
dynamic permissions = tbTaiKhoan.Tag;
bool canUpdate = permissions?.CanUpdate ?? false;
```

**Lý do**:
- Tránh gọi `HasPermission()` nhiều lần trong `CellPainting` (ảnh hưởng performance)
- Dễ truy cập trong các event handler

---

### 7.4. Kiểm tra quyền READ riêng biệt

✅ **NÊN**:
```csharp
// Kiểm tra quyền READ độc lập
if (HasPermission(QLTAIKHOAN, READ))
{
    // Hiển thị dữ liệu
}
```

**Lý do**:
- Quyền READ có thể độc lập với CREATE/UPDATE/DELETE
- User có thể chỉ có quyền xem mà không có quyền chỉnh sửa

---

### 7.5. Xử lý lỗi khi kiểm tra quyền

✅ **NÊN**:
```csharp
try
{
    bool hasPermission = PermissionHelper.HasPermission(QLTAIKHOAN, UPDATE);
    // Xử lý...
}
catch (Exception ex)
{
    // Log lỗi
    Console.WriteLine($"[ERROR] {ex.Message}");
    // Mặc định không cho phép
    return false;
}
```

**Lý do**:
- Tránh crash ứng dụng khi có lỗi database
- Mặc định không cho phép nếu có lỗi (an toàn hơn)

---

## 8. TÓM TẮT

### 8.1. Các điểm quan trọng

1. **SessionManager**: Quản lý thông tin người dùng hiện tại
2. **PermissionHelper**: Trung tâm kiểm tra và áp dụng quyền
3. **PhanQuyenBUS**: Xử lý logic nghiệp vụ phân quyền
4. **PhanQuyenDAO**: Truy vấn database

### 8.2. Luồng kiểm tra quyền

```
UI Event 
  → PermissionHelper.HasPermission() 
    → PhanQuyenBUS.KiemTraQuyenNguoiDung() 
      → PhanQuyenDAO.KiemTraQuyen() 
        → Database Query
```

### 8.3. Các mức độ phân quyền

1. **Form Level**: Kiểm tra trước khi load form
2. **Control Level**: Ẩn/hiện button, menu
3. **DataGridView Level**: Vẽ icon theo quyền
4. **Action Level**: Kiểm tra trước khi thực hiện hành động

---

## 9. KẾT LUẬN

Hệ thống phân quyền được thiết kế theo mô hình **RBAC** với 4 cấp độ:
- **Người dùng** → **Vai trò** → **Chức năng** → **Hành động**

Việc kiểm tra quyền được thực hiện ở **nhiều lớp** để đảm bảo an toàn:
- Form Level: Chặn truy cập form
- Control Level: Ẩn/hiện UI
- Action Level: Chặn hành động cụ thể

Tất cả logic phân quyền được tập trung trong **PermissionHelper** để dễ bảo trì và thống nhất trong toàn bộ ứng dụng.

---

**Tác giả**: AI Assistant  
**Ngày tạo**: 2025  
**Phiên bản**: 1.0

