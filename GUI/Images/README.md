# HƯỚNG DẪN SỬ DỤNG TÍNH NĂNG ẢNH ĐẠI DIỆN HỌC SINH

## 📋 Tổng quan

Hệ thống đã được nâng cấp để hỗ trợ ảnh đại diện cho học sinh.

## 🗂️ Cấu trúc thư mục

```
Student-Management-System_CSharp_SGU2025/
├── Images/
│   └── Students/
│       ├── default-avatar.png          # Ảnh mặc định
│       ├── HS_1.jpg                    # Ảnh học sinh mã 1
│       ├── HS_2.png                    # Ảnh học sinh mã 2
│       └── ...
```

## 🗄️ Thay đổi Database

### 1. Chạy script SQL để thêm cột ảnh

```bash
mysql -u root -p < ConnectDatabase/add_avatar_column.sql
```

Hoặc chạy trực tiếp trong MySQL:

```sql
USE QuanLyHocSinh;

ALTER TABLE HocSinh 
ADD COLUMN AnhDaiDien VARCHAR(255) NULL 
COMMENT 'Đường dẫn ảnh đại diện của học sinh' 
AFTER Email;
```

### 2. Cấu trúc bảng HocSinh sau khi cập nhật

| Cột | Kiểu | Mô tả |
|-----|------|-------|
| MaHocSinh | INT | Mã học sinh (PK) |
| HoTen | NVARCHAR(100) | Họ và tên |
| NgaySinh | DATE | Ngày sinh |
| GioiTinh | NVARCHAR(10) | Giới tính |
| SDTHS | VARCHAR(15) | Số điện thoại |
| Email | VARCHAR(100) | Email |
| **AnhDaiDien** | **VARCHAR(255)** | **Đường dẫn ảnh (mới)** |
| TrangThai | VARCHAR(50) | Trạng thái |
| TenDangNhap | VARCHAR(20) | Tên đăng nhập |

## 💻 Sử dụng trong Code

### 1. Khởi tạo folder ảnh khi ứng dụng khởi động

```csharp
// Trong Program.cs hoặc Form chính
ImageHelper.InitializeImageFolder();
```

### 2. Thêm/Cập nhật ảnh cho học sinh

```csharp
// Chọn ảnh từ máy tính
string selectedImagePath = ImageHelper.SelectImageFile();
if (selectedImagePath != null)
{
    // Lưu ảnh và lấy đường dẫn tương đối
    string relativePath = ImageHelper.SaveStudentAvatar(selectedImagePath, hocSinh.MaHS);
    
    if (relativePath != null)
    {
        // Cập nhật vào DTO
        hocSinh.AnhDaiDien = relativePath;
        
        // Lưu vào database
        hocSinhBLL.CapNhatHocSinh(hocSinh);
    }
}
```

### 3. Hiển thị ảnh trong PictureBox

```csharp
// Load ảnh từ đường dẫn tương đối
pictureBoxAvatar.Image = ImageHelper.LoadStudentAvatar(hocSinh.AnhDaiDien);
pictureBoxAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
```

### 4. Xóa ảnh cũ khi cập nhật ảnh mới

```csharp
// Lưu đường dẫn ảnh cũ
string oldImagePath = hocSinh.AnhDaiDien;

// Lưu ảnh mới
string newImagePath = ImageHelper.SaveStudentAvatar(selectedImagePath, hocSinh.MaHS);
if (newImagePath != null)
{
    // Xóa ảnh cũ
    if (!string.IsNullOrEmpty(oldImagePath))
    {
        ImageHelper.DeleteStudentAvatar(oldImagePath);
    }
    
    // Cập nhật DTO
    hocSinh.AnhDaiDien = newImagePath;
}
```

## 📸 Quy định về ảnh

- **Định dạng hỗ trợ**: JPG, JPEG, PNG, BMP, GIF
- **Kích thước tối đa**: 5MB
- **Kích thước ảnh**: Tự động resize về tối đa 800x800px (giữ nguyên tỷ lệ)
- **Tên file**: Tự động đặt theo format `HS_{MaHocSinh}.{extension}`

## 🎨 Tích hợp vào Form

### Form Thêm/Sửa học sinh

```csharp
public partial class frmThemSuaHocSinh : Form
{
    private HocSinhDTO hocSinh;
    private string selectedImagePath = null;

    private void btnChonAnh_Click(object sender, EventArgs e)
    {
        selectedImagePath = ImageHelper.SelectImageFile();
        if (selectedImagePath != null)
        {
            // Hiển thị preview
            pictureBoxAvatar.Image = Image.FromFile(selectedImagePath);
        }
    }

    private void btnLuu_Click(object sender, EventArgs e)
    {
        // ... validate các trường khác ...

        // Lưu ảnh nếu có chọn
        if (selectedImagePath != null)
        {
            string relativePath = ImageHelper.SaveStudentAvatar(selectedImagePath, hocSinh.MaHS);
            if (relativePath != null)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(hocSinh.AnhDaiDien))
                {
                    ImageHelper.DeleteStudentAvatar(hocSinh.AnhDaiDien);
                }
                
                hocSinh.AnhDaiDien = relativePath;
            }
        }

        // Lưu vào database
        hocSinhBLL.CapNhatHocSinh(hocSinh);
    }
}
```

### DataGridView với ảnh

```csharp
// Thêm cột ảnh vào DataGridView
DataGridViewImageColumn colImage = new DataGridViewImageColumn();
colImage.Name = "Avatar";
colImage.HeaderText = "Ảnh";
colImage.Width = 80;
colImage.ImageLayout = DataGridViewImageCellLayout.Zoom;
dgvHocSinh.Columns.Add(colImage);

// Load dữ liệu
foreach (var hs in danhSachHocSinh)
{
    int rowIndex = dgvHocSinh.Rows.Add();
    DataGridViewRow row = dgvHocSinh.Rows[rowIndex];
    
    row.Cells["Avatar"].Value = ImageHelper.LoadStudentAvatar(hs.AnhDaiDien);
    row.Cells["MaHS"].Value = hs.MaHS;
    row.Cells["HoTen"].Value = hs.HoTen;
    // ... các cột khác ...
}
```

## 🔧 Troubleshooting

### Lỗi: Không tìm thấy folder Images/Students

```csharp
// Khởi tạo folder khi khởi động app
ImageHelper.InitializeImageFolder();
```

### Lỗi: Ảnh không hiển thị

1. Kiểm tra đường dẫn trong database
2. Kiểm tra file ảnh có tồn tại không
3. Sử dụng `ImageHelper.LoadStudentAvatar()` để tự động fallback về ảnh mặc định

### Lỗi: File quá lớn

- Giảm kích thước file về dưới 5MB
- Hoặc điều chỉnh `MAX_FILE_SIZE` trong `ImageHelper.cs`

## 📝 Checklist triển khai

- [ ] Chạy script SQL `add_avatar_column.sql`
- [ ] Đặt file `default-avatar.png` vào `Images/Students/`
- [ ] Thêm `ImageHelper.InitializeImageFolder()` vào `Program.cs`
- [ ] Cập nhật form thêm/sửa học sinh để có nút chọn ảnh
- [ ] Test thêm/sửa/xóa ảnh
- [ ] Test hiển thị ảnh trong DataGridView
- [ ] Test hiển thị ảnh trong form chi tiết

## 🎯 Tính năng tương lai

- [ ] Crop ảnh trước khi lưu
- [ ] Rotate ảnh
- [ ] Upload từ camera/webcam
- [ ] Lưu ảnh vào database (BLOB) thay vì file system
- [ ] Compress ảnh tự động

---
**Lưu ý**: Backup dữ liệu trước khi chạy script SQL!
