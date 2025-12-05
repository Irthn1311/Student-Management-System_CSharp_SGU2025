# 📍 DANH SÁCH CÁC NƠI CẤU HÌNH DATABASE

## Tổng quan
Project này có **4 nơi** chứa thông tin cấu hình database (Server, Database, UserId, Password).

---

## 1. ✅ **Config/database_config.json** (FILE CHÍNH - ĐANG SỬ DỤNG)

**Đường dẫn:** `Student-Management-System_CSharp_SGU2025/Config/database_config.json`

**Trạng thái:** ✅ **ĐANG ĐƯỢC SỬ DỤNG** - File này được ưu tiên nhất

**Nội dung hiện tại:**
```json
{
  "Database": {
    "Server": "127.0.0.1",
    "Database": "QuanLyHocSinh",
    "UserId": "root",
    "Password": "123456789",
    "Port": 3306,
    "ConnectionTimeout": 30
  }
}
```

**Được đọc bởi:**
- `DatabaseConfig.GetAdoNetConnectionString()` - Cho ADO.NET
- `DatabaseConfig.GetEntityFrameworkConnectionString()` - Cho Entity Framework
- `ConnectionDatabase.GetConnection()` - Fallback cho ADO.NET
- `SchoolDbContext` - Fallback cho Entity Framework

**Ghi chú:** 
- ✅ File này được ưu tiên sử dụng
- ⚠️ Password hiện tại: `123456789`
- 📝 Để thay đổi cấu hình, chỉ cần sửa file này

---

## 2. ⚠️ **App.config** (FALLBACK)

**Đường dẫn:** `Student-Management-System_CSharp_SGU2025/App.config`

**Trạng thái:** ⚠️ **FALLBACK** - Chỉ dùng khi không đọc được `database_config.json`

**Dòng:** 15

**Nội dung hiện tại:**
```xml
<add name="SchoolDbContext" connectionString="server=127.0.0.1;database=QuanLyHocSinh;user id=root;password=123456789;Connection Timeout=30;" providerName="MySql.Data.MySqlClient" />
```

**Được đọc bởi:**
- `SchoolDbContext.GetConnectionStringOrName()` - Fallback cho Entity Framework

**Ghi chú:**
- ⚠️ Password hiện tại: `123456789`
- 📝 Chỉ dùng khi `database_config.json` không tồn tại hoặc lỗi
- 🔄 Nên đồng bộ với `database_config.json`

---

## 3. ⚠️ **Config/DatabaseConfig.cs** (DEFAULT HARDCODED)

**Đường dẫn:** `Student-Management-System_CSharp_SGU2025/Config/DatabaseConfig.cs`

**Trạng thái:** ⚠️ **DEFAULT HARDCODED** - Dùng khi tạo file `database_config.json` mặc định

**Dòng:** 89-92

**Nội dung hiện tại:**
```csharp
Server = "127.0.0.1",
Database = "QuanLyHocSinh",
UserId = "root",
Password = "12345678",  // ⚠️ KHÁC với database_config.json!
Port = 3306,
ConnectionTimeout = 30
```

**Được sử dụng bởi:**
- `DatabaseConfig.CreateDefaultConfig()` - Tạo file mặc định nếu chưa tồn tại

**Ghi chú:**
- ⚠️ Password: `12345678` (KHÁC với các file khác!)
- 📝 Chỉ dùng khi tạo file mặc định lần đầu
- 🔄 Nên đồng bộ với `database_config.json`

---

## 4. ⚠️ **ConnectDatabase/ConnectionDatabase.cs** (FALLBACK HARDCODED)

**Đường dẫn:** `Student-Management-System_CSharp_SGU2025/ConnectDatabase/ConnectionDatabase.cs`

**Trạng thái:** ⚠️ **FALLBACK HARDCODED** - Dùng khi tất cả các nguồn khác đều lỗi

**Dòng:** 29

**Nội dung hiện tại:**
```csharp
return "Server=127.0.0.1;Database=QuanLyHocSinh;Uid=root;Pwd=12345678;";
```

**Được sử dụng bởi:**
- `ConnectionDatabase.GetConnectionString()` - Fallback cuối cùng cho ADO.NET

**Ghi chú:**
- ⚠️ Password: `12345678` (KHÁC với database_config.json!)
- 📝 Chỉ dùng khi tất cả các nguồn khác đều lỗi
- 🔄 Nên đồng bộ với `database_config.json`

---

## 📊 TÓM TẮT

| # | File | Server | Database | UserId | Password | Trạng thái |
|---|------|--------|----------|--------|----------|------------|
| 1 | `database_config.json` | 127.0.0.1 | QuanLyHocSinh | root | **123456789** | ✅ ĐANG DÙNG |
| 2 | `App.config` | 127.0.0.1 | QuanLyHocSinh | root | **123456789** | ⚠️ Fallback |
| 3 | `DatabaseConfig.cs` | 127.0.0.1 | QuanLyHocSinh | root | **12345678** | ⚠️ Default |
| 4 | `ConnectionDatabase.cs` | 127.0.0.1 | QuanLyHocSinh | root | **12345678** | ⚠️ Fallback |

---

## ⚠️ VẤN ĐỀ PHÁT HIỆN

### 1. Password không đồng bộ
- `database_config.json` và `App.config`: `123456789`
- `DatabaseConfig.cs` và `ConnectionDatabase.cs`: `12345678`

### 2. Khuyến nghị
1. ✅ **Chỉ sửa file `database_config.json`** - Đây là file chính
2. 🔄 **Đồng bộ các file fallback** - Cập nhật password trong:
   - `App.config` (dòng 15)
   - `DatabaseConfig.cs` (dòng 92)
   - `ConnectionDatabase.cs` (dòng 29)
3. 📝 **Hoặc** giữ nguyên và chỉ dùng `database_config.json` (khuyến nghị)

---

## 🎯 HƯỚNG DẪN THAY ĐỔI CẤU HÌNH

### Cách 1: Chỉ sửa file chính (Khuyến nghị)
1. Mở `Config/database_config.json`
2. Sửa các giá trị cần thiết
3. Lưu file
4. Khởi động lại ứng dụng

### Cách 2: Đồng bộ tất cả các file
1. Sửa `Config/database_config.json`
2. Sửa `App.config` (dòng 15)
3. Sửa `Config/DatabaseConfig.cs` (dòng 89-92)
4. Sửa `ConnectDatabase/ConnectionDatabase.cs` (dòng 29)
5. Rebuild project

---

## 📝 LƯU Ý

- ⚠️ **Bảo mật:** Không commit password thật lên Git
- ✅ **Ưu tiên:** File `database_config.json` được ưu tiên nhất
- 🔄 **Đồng bộ:** Các file fallback nên có cùng giá trị
- 📍 **Vị trí:** Tất cả các file đều trong thư mục `Student-Management-System_CSharp_SGU2025/`

---

*Tài liệu được tạo tự động - Cập nhật: $(Get-Date)*
