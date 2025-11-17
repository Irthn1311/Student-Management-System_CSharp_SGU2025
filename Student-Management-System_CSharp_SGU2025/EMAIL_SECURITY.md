# ⚠️ BẢO MẬT THÔNG TIN EMAIL

## Files KHÔNG được push lên GitHub:

```
App.config.local
*.local
```

## Cách sử dụng:

### 1. File `App.config` (Push lên GitHub - AN TOÀN)

- Chứa giá trị mặc định: `your-email@gmail.com` và `xxxx xxxx xxxx xxxx`
- **AN TOÀN** để push lên GitHub

### 2. File `App.config.local` (KHÔNG push - BÍ MẬT)

- Chứa thông tin THẬT của bạn
- Đã được thêm vào `.gitignore`
- **KHÔNG BAO GIỜ** push file này!

### 3. Khi làm việc trên máy local:

```bash
# Copy thông tin thật vào App.config.local
# Hoặc sửa trực tiếp App.config (nhưng KHÔNG commit)
```

---

## 🔒 QUAN TRỌNG:

**TRƯỚC KHI COMMIT:**

1. Kiểm tra `App.config` chỉ có giá trị mẫu
2. Kiểm tra `.gitignore` đã có `App.config.local`
3. **KHÔNG BAO GIỜ** commit file chứa App Password thật!

---

## 🚨 NẾU ĐÃ PUSH NHẦM:

1. **Xóa App Password ngay** tại: https://myaccount.google.com/apppasswords
2. Tạo App Password mới
3. Xóa commit chứa password (hoặc force push)
4. Cập nhật lại code với cách bảo mật này

---

## 💡 Lưu ý cho nhóm:

- Mỗi thành viên dùng Gmail riêng của mình
- Tạo `App.config.local` riêng trên máy
- **KHÔNG share** App Password qua chat/email
