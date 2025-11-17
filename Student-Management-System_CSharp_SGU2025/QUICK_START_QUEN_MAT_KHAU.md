# 🚀 Quick Start - Tính Năng Quên Mật Khẩu

## ✅ Đã hoàn thành:

### 1. Backend Services

- ✅ `EmailService.cs` - Gửi email qua Gmail SMTP
- ✅ `OTPManager.cs` - Tạo và xác thực OTP
- ✅ `HocSinhBLL.cs` - Method lấy email học sinh

### 2. Frontend Forms

- ✅ `FrmDangNhap.cs` - Xử lý link "Quên mật khẩu?"
- ✅ `FrmXacThucOTP.cs` - Nhập OTP với countdown timer
- ✅ `FrmDoiMatKhau.cs` - Đổi mật khẩu mới

---

## ⚡ Chỉ cần 3 bước để test:

### Bước 1: Tạo Gmail App Password (2 phút)

1. Vào: https://myaccount.google.com/apppasswords
2. Tạo App Password cho "Mail"
3. Copy mã 16 ký tự

### Bước 2: Cập nhật Code (30 giây)

Mở file: `GUI/DangNhap/FrmDangNhap.cs` (dòng ~279-280)

Thay đổi:

```csharp
string GMAIL_ADDRESS = "your-email@gmail.com";  // ← Email của bạn
string GMAIL_APP_PASSWORD = "xxxx xxxx xxxx xxxx";  // ← App Password vừa copy
```

### Bước 3: Build & Test (1 phút)

1. **Ctrl + Shift + B** (Build)
2. Chạy ứng dụng
3. Click **"Quên mật khẩu ?"**
4. Nhập: `HS101` (hoặc username học sinh có email)
5. Kiểm tra email → Nhập OTP → Đổi mật khẩu

---

## 📧 Lưu ý về Email Test:

**Cách 1: Test với chính email của bạn**

```sql
UPDATE HocSinh
SET Email = 'your-email@gmail.com'
WHERE MaHocSinh = 101;
```

**Cách 2: Test với email tạm**

- Dùng: https://temp-mail.org/ (email tạm 10 phút)
- Hoặc: https://10minutemail.com/

---

## 🔍 Kiểm Tra Nhanh:

### Console Log phải hiển thị:

```
[INFO] Người dùng click vào 'Quên mật khẩu'
[OTPManager] Đã tạo mã OTP: 123456
[EmailService] ✅ Gửi email thành công
```

### Nếu thấy lỗi:

- `❌ Lỗi SMTP: AuthenticationFailed` → Sai App Password
- `❌ Unable to connect` → Lỗi Internet/Firewall
- `⚠️ Chức năng gửi email chưa được cấu hình` → Chưa đổi code

---

## 📚 Hướng Dẫn Chi Tiết:

Xem file: `docs/HUONG_DAN_QUEN_MAT_KHAU.md`

---

**Thời gian ước tính: < 5 phút**
