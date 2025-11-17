# 🔐 Hướng Dẫn Cấu Hình và Sử Dụng Tính Năng Quên Mật Khẩu

## 📋 Tổng Quan

Tính năng "Quên mật khẩu" cho phép học sinh khôi phục mật khẩu thông qua email đã đăng ký. Hệ thống sẽ gửi mã OTP (6 chữ số) đến email của học sinh để xác thực.

### Luồng hoạt động:

1. Người dùng click "Quên mật khẩu?" trên form đăng nhập
2. Nhập tên đăng nhập (VD: HS101)
3. Hệ thống kiểm tra:
   - Tên đăng nhập có tồn tại không?
   - Có phải tài khoản học sinh không? (bắt đầu bằng "HS")
   - Email đã đăng ký chưa?
4. Tạo mã OTP ngẫu nhiên 6 chữ số
5. Gửi OTP qua email
6. Người dùng nhập OTP (có 10 phút để nhập)
7. Sau khi xác thực thành công, cho phép đổi mật khẩu mới
8. Gửi email thông báo đổi mật khẩu thành công

---

## ⚙️ BƯỚC 1: Cấu Hình Gmail SMTP (BẮT BUỘC)

### 1.1. Tại sao cần App Password?

- Gmail không cho phép dùng mật khẩu thông thường để gửi email qua SMTP
- Phải dùng **App Password** (mật khẩu ứng dụng) để bảo mật

### 1.2. Cách tạo Gmail App Password:

#### Bước 1: Bật xác thực 2 bước

1. Đăng nhập Gmail của bạn
2. Vào: https://myaccount.google.com/security
3. Tìm mục **"2-Step Verification"** (Xác minh 2 bước)
4. Click **"Get Started"** và làm theo hướng dẫn
5. Xác thực bằng số điện thoại hoặc ứng dụng Authenticator

#### Bước 2: Tạo App Password

1. Sau khi bật 2-Step Verification, vào: https://myaccount.google.com/apppasswords
2. Hoặc search "App passwords" trong Google Account
3. Click **"Select app"** → chọn **"Mail"**
4. Click **"Select device"** → chọn **"Windows Computer"**
5. Click **"Generate"**
6. Google sẽ hiển thị mã 16 ký tự (VD: `abcd efgh ijkl mnop`)
7. **Lưu ý**: Copy mã này ngay, vì chỉ hiển thị 1 lần!

### 1.3. Cập nhật Code:

Mở file: `GUI/DangNhap/FrmDangNhap.cs`

Tìm dòng **279-280** (hoặc search "TODO: QUAN TRỌNG"):

```csharp
// TODO: QUAN TRỌNG - Bạn cần cấu hình Gmail SMTP trước khi test!
string GMAIL_ADDRESS = "your-email@gmail.com";  // ← Thay bằng email của bạn
string GMAIL_APP_PASSWORD = "xxxx xxxx xxxx xxxx";  // ← Thay bằng App Password 16 ký tự
```

**Thay đổi thành:**

```csharp
string GMAIL_ADDRESS = "example@gmail.com";  // Email Gmail của bạn
string GMAIL_APP_PASSWORD = "abcd efgh ijkl mnop";  // App Password vừa tạo (16 ký tự, có khoảng trắng)
```

**Ví dụ thực tế:**

```csharp
string GMAIL_ADDRESS = "thptirthn@gmail.com";
string GMAIL_APP_PASSWORD = "wxyz 1234 abcd 5678";
```

### 1.4. Build lại Project:

- Nhấn **Ctrl + Shift + B** trong Visual Studio
- Hoặc menu **Build → Build Solution**

---

## 🧪 BƯỚC 2: Test Tính Năng

### 2.1. Chuẩn bị dữ liệu test:

Đảm bảo có ít nhất 1 học sinh có email trong database:

```sql
-- Kiểm tra email học sinh
SELECT MaHocSinh, HoTen, Email, TenDangNhap
FROM HocSinh
WHERE TenDangNhap IS NOT NULL
LIMIT 5;

-- Nếu chưa có email, cập nhật:
UPDATE HocSinh
SET Email = 'your-test-email@gmail.com'  -- Thay bằng email test của bạn
WHERE MaHocSinh = 101;
```

### 2.2. Các Scenario Test:

#### Test 1: Flow hoàn chỉnh (Happy Path)

1. Chạy ứng dụng
2. Click link **"Quên mật khẩu ?"**
3. Nhập username: `HS101`
4. Click **"Xác nhận"**
5. Kiểm tra console log:
   ```
   [INFO] Người dùng click vào 'Quên mật khẩu'
   [DEBUG] Tên đăng nhập nhập vào: HS101
   [SUCCESS] Tìm thấy tài khoản: HS101
   [INFO] Đây là tài khoản học sinh
   [DEBUG] Mã học sinh: 101
   [BLL] Tìm thấy email: example@gmail.com cho học sinh Nguyễn Văn A
   [SUCCESS] Tìm thấy email: example@gmail.com
   [INFO] Đang tạo mã OTP...
   [OTPManager] Đã tạo mã OTP: 123456
   [INFO] Đang gửi email OTP...
   [EmailService] Đang gửi email đến: example@gmail.com
   [EmailService] ✅ Gửi email thành công
   ```
6. Kiểm tra email → Sẽ nhận được email với mã OTP
7. Form "Xác thực OTP" hiển thị
8. Nhập mã OTP (6 chữ số) → Click **"Xác nhận"**
9. Form "Đổi mật khẩu" hiển thị
10. Nhập mật khẩu mới (ít nhất 6 ký tự)
11. Nhập xác nhận mật khẩu
12. Click **"Xác nhận"**
13. Thông báo thành công → Nhận email thông báo đổi mật khẩu
14. Đăng nhập bằng mật khẩu mới

#### Test 2: Username không tồn tại

- Nhập: `HS999` (không có trong DB)
- Kết quả: "Tên đăng nhập không tồn tại trong hệ thống!"

#### Test 3: Tài khoản không phải học sinh

- Nhập: `admin` hoặc `GV001`
- Kết quả: "Chức năng khôi phục mật khẩu hiện chỉ hỗ trợ cho học sinh."

#### Test 4: Học sinh không có email

- Nhập username của học sinh không có email
- Kết quả: "Tài khoản này chưa có email đăng ký!"

#### Test 5: OTP sai

- Nhập OTP sai nhiều lần
- Kết quả: "Mã OTP không đúng hoặc đã hết hạn!"

#### Test 6: OTP hết hạn

- Đợi 10 phút sau khi nhận OTP
- Kết quả: Form tự động disable, thông báo hết hạn
- Click **"Gửi lại"** để nhận OTP mới

#### Test 7: Mật khẩu không khớp

- Nhập mật khẩu mới: `123456`
- Xác nhận: `654321`
- Kết quả: "Mật khẩu xác nhận không khớp!"

#### Test 8: Mật khẩu quá ngắn

- Nhập mật khẩu: `123` (< 6 ký tự)
- Kết quả: "Mật khẩu phải có ít nhất 6 ký tự!"

---

## 📊 BƯỚC 3: Kiểm Tra Logs

### Console Logs Quan Trọng:

#### ✅ Thành công:

```
[EmailService] ✅ Gửi email thành công đến example@gmail.com
[OTPManager] ✅ OTP hợp lệ cho HS101
[FrmDoiMatKhau] ✅ Đổi mật khẩu thành công
```

#### ❌ Lỗi thường gặp:

**1. Lỗi SMTP Authentication:**

```
[EmailService] ❌ Lỗi SMTP: AuthenticationFailed
```

**Giải pháp:**

- Kiểm tra lại App Password (16 ký tự)
- Đảm bảo đã bật 2-Step Verification
- Thử tạo App Password mới

**2. Lỗi kết nối:**

```
[EmailService] ❌ Lỗi SMTP: Unable to connect
```

**Giải pháp:**

- Kiểm tra kết nối Internet
- Firewall có chặn port 587 không?
- Thử đổi sang port 465 (SSL)

**3. Email không đến:**

```
[EmailService] ✅ Gửi email thành công (nhưng không nhận được)
```

**Giải pháp:**

- Kiểm tra thư mục **Spam/Junk**
- Kiểm tra email có đúng không?
- Đợi 1-2 phút (đôi khi bị delay)

---

## 🎨 BƯỚC 4: Tùy Chỉnh (Optional)

### 4.1. Thay đổi thời gian OTP:

File: `Services/OTPManager.cs` - Dòng 16

```csharp
private const int OTP_VALIDITY_MINUTES = 10; // Đổi thành 5, 15, 20...
```

### 4.2. Thay đổi độ dài OTP:

File: `Services/OTPManager.cs` - Dòng 19

```csharp
private const int OTP_LENGTH = 6; // Đổi thành 4, 8...
```

### 4.3. Thay đổi template email:

File: `Services/EmailService.cs` - Method `GuiOTP()`

- Tùy chỉnh HTML, CSS, nội dung email

### 4.4. Thay đổi validation mật khẩu:

File: `GUI/DangNhap/FrmDoiMatKhau.cs` - Method `BtnXacNhan_Click`

```csharp
// Thêm yêu cầu mật khẩu mạnh hơn:
if (!Regex.IsMatch(matKhauMoi, @"^(?=.*[A-Z])(?=.*\d).{8,}$"))
{
    MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự, 1 chữ hoa, 1 số!");
    return;
}
```

---

## 🔧 Troubleshooting

### Vấn đề 1: "The type or namespace name 'EmailService' could not be found"

**Giải pháp:**

- Đảm bảo đã tạo file `Services/EmailService.cs`
- Build lại project (**Ctrl + Shift + B**)
- Kiểm tra namespace: `Student_Management_System_CSharp_SGU2025.Services`

### Vấn đề 2: Form không hiển thị

**Giải pháp:**

- Kiểm tra Designer.cs của form có lỗi không
- Rebuild project
- Clean solution (**Build → Clean Solution**) rồi Build lại

### Vấn đề 3: Email đến Spam

**Giải pháp:**

- Đánh dấu "Not Spam" trong Gmail
- Thêm email gửi vào danh bạ
- Sử dụng domain email chuyên nghiệp (không dùng Gmail cá nhân)

### Vấn đề 4: OTP không khớp

**Giải pháp:**

- Kiểm tra console log để xem OTP được tạo
- Đảm bảo nhập đúng 6 chữ số
- Không có khoảng trắng ở đầu/cuối

---

## 📁 Cấu Trúc Files

```
Student-Management-System_CSharp_SGU2025/
│
├── Services/
│   ├── EmailService.cs          ✅ Service gửi email qua Gmail SMTP
│   └── OTPManager.cs             ✅ Quản lý OTP (tạo, xác thực, xóa)
│
├── BUS/
│   ├── LoginBUS.cs               ✅ Xử lý đăng nhập, cập nhật mật khẩu
│   └── HocSinhBLL.cs             ✅ Thêm method LayEmailTheoMaHocSinh()
│
└── GUI/DangNhap/
    ├── FrmDangNhap.cs            ✅ Form đăng nhập + link quên mật khẩu
    ├── FrmXacThucOTP.cs          ✅ Form nhập OTP với countdown timer
    └── FrmDoiMatKhau.cs          ✅ Form đổi mật khẩu mới
```

---

## 📞 Hỗ Trợ

### Nếu gặp vấn đề:

1. **Kiểm tra Console Log** (Output window trong Visual Studio)
2. **Kiểm tra Error List** (View → Error List)
3. **Test từng bước** theo hướng dẫn Test ở trên

### Thông tin SMTP Gmail:

- **Host:** smtp.gmail.com
- **Port:** 587 (TLS) hoặc 465 (SSL)
- **Authentication:** Required (App Password)
- **Timeout:** 20 seconds

---

## ✅ Checklist Triển Khai

- [ ] Đã tạo Gmail App Password
- [ ] Đã cập nhật `GMAIL_ADDRESS` và `GMAIL_APP_PASSWORD` trong code
- [ ] Đã build lại project
- [ ] Đã test với 1 tài khoản học sinh có email
- [ ] Email OTP đã nhận được
- [ ] Có thể nhập OTP và xác thực
- [ ] Có thể đổi mật khẩu thành công
- [ ] Có thể đăng nhập bằng mật khẩu mới
- [ ] Nhận được email thông báo đổi mật khẩu

---

**🎉 Chúc mừng! Tính năng Quên Mật Khẩu đã hoàn thiện!**

_Version: 1.0 - Created: November 9, 2025_
