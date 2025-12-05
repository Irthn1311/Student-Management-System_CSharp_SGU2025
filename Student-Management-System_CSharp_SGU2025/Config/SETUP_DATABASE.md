# 🗄️ HƯỚNG DẪN CẤU HÌNH DATABASE

## 📍 File cấu hình duy nhất

**Tất cả cấu hình database chỉ ở 1 nơi:** `Config/database_config.json`

---

## ⚙️ Cấu hình cho từng môi trường

### 1. Localhost có password (Mặc định)

Mở file `Config/database_config.json` và sửa:

```json
{
  "Database": {
    "Server": "127.0.0.1",
    "Database": "QuanLyHocSinh",
    "UserId": "root",
    "Password": "12345678",
    "Port": 3306,
    "ConnectionTimeout": 30
  }
}
```

### 2. Localhost KHÔNG có password

Mở file `Config/database_config.json` và sửa:

```json
{
  "Database": {
    "Server": "127.0.0.1",
    "Database": "QuanLyHocSinh",
    "UserId": "root",
    "Password": "",
    "Port": 3306,
    "ConnectionTimeout": 30
  }
}
```

**Lưu ý:** Để `Password` là chuỗi rỗng `""` nếu MySQL localhost không có password.

### 3. Server từ xa

```json
{
  "Database": {
    "Server": "192.168.1.100",
    "Database": "QuanLyHocSinh",
    "UserId": "admin",
    "Password": "your_password",
    "Port": 3306,
    "ConnectionTimeout": 30
  }
}
```

---

## ✅ Sau khi sửa

1. Lưu file `Config/database_config.json`
2. Khởi động lại ứng dụng
3. Không cần rebuild project

---

## 🔒 Bảo mật

- ⚠️ **KHÔNG commit** file `database_config.json` lên Git nếu chứa password thật
- ✅ Thêm vào `.gitignore` nếu cần
- 📝 Tạo file `database_config.json.example` với giá trị mẫu để commit

---

## 🆘 Xử lý lỗi

Nếu gặp lỗi "Không thể đọc cấu hình database":
1. Kiểm tra file `Config/database_config.json` tồn tại
2. Kiểm tra format JSON hợp lệ (dùng JSON validator)
3. Đảm bảo có đầy đủ các trường: Server, Database, UserId, Password, Port, ConnectionTimeout

---

## 📝 Lưu ý

- Tất cả các file khác (`App.config`, `DatabaseConfig.cs`, `ConnectionDatabase.cs`) đã được cấu hình để **chỉ đọc từ `database_config.json`**
- Không cần sửa code, chỉ cần sửa file JSON
- Hỗ trợ password rỗng cho localhost không có password
