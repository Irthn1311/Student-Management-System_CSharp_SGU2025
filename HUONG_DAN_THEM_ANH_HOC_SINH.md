# Hướng Dẫn Thêm Chức Năng Hình Ảnh Học Sinh

## Tóm tắt các thay đổi đã thực hiện:

### 1. ✅ Script SQL - Cập nhật ảnh cho tất cả học sinh
- File: `ConnectDatabase/02_update_anh_hocsinh.sql`
- Script đã được cập nhật để phân bổ 4 hình ảnh (hs1.jpg, hs2.jpg, hs3.jpg, hs4.jpg) cho TẤT CẢ học sinh dựa trên MaHocSinh
- Chạy script này để cập nhật ảnh cho tất cả học sinh trong database

### 2. ✅ Form XemChiTietHocSinh
- Đã cải thiện hàm `LoadAnhHocSinh()` để:
  - Tự động phân bổ ảnh nếu chưa có trong database
  - Xử lý memory leak khi load ảnh
  - Hỗ trợ cả absolute và relative path

### 3. ✅ Form ChinhSuaHocSinh
- Đã thêm code xử lý ảnh:
  - Biến `newImagePath` để lưu đường dẫn ảnh mới
  - Hàm `LoadAnhHocSinh()` để load ảnh hiện tại
  - Hàm `btnChonAnh_Click()` để xử lý khi chọn ảnh mới
  - Helper class `ChinhSuaHocSinhImageHelper` để xử lý ảnh

## Cách thêm controls vào form ChinhSuaHocSinh (Designer):

### Bước 1: Mở Form Designer
1. Mở file `ChinhSuaHocSinh.Designer.cs` trong Visual Studio
2. Chuyển sang chế độ Design (click vào form)

### Bước 2: Thêm PictureBox để hiển thị ảnh
1. Kéo `PictureBox` từ Toolbox vào form
2. Đặt tên: `picAnhHocSinh`
3. Vị trí đề xuất: Bên cạnh các TextBox thông tin (ví dụ: bên phải txtHovaTen)
4. Kích thước: Width = 140, Height = 180
5. Properties:
   - `SizeMode`: Zoom
   - `BorderStyle`: FixedSingle
   - `BackColor`: White

### Bước 3: Thêm Button để chọn ảnh
1. Kéo `Button` (hoặc `Guna2Button` nếu dùng Guna UI) vào form
2. Đặt tên: `btnChonAnh`
3. Text: "Chọn ảnh"
4. Đặt ngay dưới PictureBox
5. Kích thước: Width = 140, Height = 30

### Bước 4: Kết nối Event Handler
1. Chọn button `btnChonAnh`
2. Trong Properties, tìm Events (biểu tượng tia sét)
3. Double-click vào `Click` event
4. Visual Studio sẽ tự động tạo event handler và kết nối với hàm `btnChonAnh_Click()` đã có sẵn

### Bước 5: Kiểm tra lại
1. Build project (F6)
2. Mở form ChinhSuaHocSinh
3. Ảnh học sinh sẽ tự động load
4. Click "Chọn ảnh" để chọn ảnh mới

## Lưu ý:

1. **Đường dẫn ảnh**: Ảnh sẽ được lưu trong thư mục `Images/Students/` với tên format: `hs_{MaHS}_{timestamp}.jpg`

2. **Kích thước ảnh**: Nên sử dụng ảnh có kích thước phù hợp (ví dụ: 140x180px hoặc tỷ lệ tương đương)

3. **Định dạng ảnh**: Hỗ trợ .jpg, .jpeg, .png, .bmp

4. **Chạy SQL Script**: Nhớ chạy script SQL `02_update_anh_hocsinh.sql` để phân bổ ảnh cho tất cả học sinh hiện có

## Ví dụ vị trí controls trong Designer:

```
┌─────────────────────────────────────────────────┐
│  [Panel Header]                                 │
├─────────────────────────────────────────────────┤
│                                                 │
│  Họ và Tên: [TextBox]    ┌──────────────┐      │
│                           │              │      │
│  Ngày sinh: [DatePicker]  │  PictureBox  │      │
│                           │  (140x180)   │      │
│  Giới tính: [Radio]       │              │      │
│                           └──────────────┘      │
│                           [Chọn ảnh Button]     │
│                                                 │
└─────────────────────────────────────────────────┘
```

## Code đã có sẵn (không cần viết thêm):

- ✅ `LoadAnhHocSinh()` - Tự động load khi form mở
- ✅ `btnChonAnh_Click()` - Xử lý khi chọn ảnh mới
- ✅ `ChinhSuaHocSinhImageHelper` - Helper class xử lý ảnh
- ✅ Cập nhật `AnhDaiDien` khi lưu học sinh

## Troubleshooting:

1. **Ảnh không hiển thị**: Kiểm tra đường dẫn ảnh trong database và đảm bảo file tồn tại trong thư mục `Images/Students/`

2. **Button không hoạt động**: Kiểm tra event handler đã được kết nối chưa (xem Properties > Events)

3. **Lỗi khi chọn ảnh**: Kiểm tra quyền ghi vào thư mục `Images/Students/`

