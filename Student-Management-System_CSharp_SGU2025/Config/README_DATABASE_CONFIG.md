# Hướng dẫn cấu hình Database

## Tổng quan

Từ phiên bản này, cấu hình database được tập trung vào file **`database_config.json`** trong thư mục `Config/`.

Cả **LINQ (Entity Framework)** và **ADO.NET** đều đọc connection string từ cùng một file này.

## File cấu hình

**Đường dẫn:** `Config/database_config.json`

**Cấu trúc:**
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

## Cách sử dụng

### 1. Thay đổi cấu hình database

Chỉ cần chỉnh sửa file `Config/database_config.json` và lưu lại. Ứng dụng sẽ tự động đọc cấu hình mới khi khởi động lại.

**Ví dụ:**
```json
{
  "Database": {
    "Server": "192.168.1.100",
    "Database": "QuanLyHocSinh",
    "UserId": "admin",
    "Password": "newpassword",
    "Port": 3306,
    "ConnectionTimeout": 60
  }
}
```

### 2. File tự động tạo

Nếu file `database_config.json` chưa tồn tại, ứng dụng sẽ tự động tạo file với cấu hình mặc định khi chạy lần đầu.

### 3. Fallback

Nếu không đọc được file JSON, hệ thống sẽ tự động fallback về:
1. Connection string trong `App.config` (nếu có)
2. Connection string mặc định (hardcoded)

## Lợi ích

✅ **Tập trung:** Tất cả cấu hình database ở một nơi  
✅ **Đồng bộ:** LINQ và ADO.NET dùng cùng cấu hình  
✅ **Dễ quản lý:** Chỉ cần sửa một file  
✅ **Linh hoạt:** Có thể thay đổi mà không cần rebuild project  

## Lưu ý

⚠️ **Bảo mật:** File `database_config.json` chứa thông tin nhạy cảm (password).  
⚠️ **Git:** Nên thêm file này vào `.gitignore` nếu chứa thông tin production.  
⚠️ **Backup:** Luôn backup file config trước khi thay đổi.  

## API sử dụng

### Đọc cấu hình
```csharp
using Student_Management_System_CSharp_SGU2025.Config;

// Lấy cấu hình
var config = DatabaseConfig.GetConfig();
string server = config.Database.Server;

// Lấy connection string cho ADO.NET
string adoNetConnString = DatabaseConfig.GetAdoNetConnectionString();

// Lấy connection string cho Entity Framework
string efConnString = DatabaseConfig.GetEntityFrameworkConnectionString();
```

### Lưu cấu hình
```csharp
var config = DatabaseConfig.GetConfig();
config.Database.Password = "newpassword";
DatabaseConfig.SaveConfig(config);
```

### Reload cấu hình
```csharp
// Reload từ file (khi file được thay đổi bên ngoài)
DatabaseConfig.ReloadConfig();
```

