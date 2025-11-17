# Hướng dẫn khắc phục lỗi NuGet Package Restore

## Vấn đề
Lỗi: "Could not find a part of the path '...\packages\...\buildTransitive\...'"

## Nguyên nhân
- Packages được restore vào thư mục sai
- Visual Studio cache lỗi cũ
- Cấu hình NuGet không đồng bộ

## Giải pháp triệt để

### Bước 1: Chạy script restore-packages.bat
```bash
restore-packages.bat
```

### Bước 2: Clean và Rebuild trong Visual Studio
1. Đóng Visual Studio
2. Xóa thư mục `.vs`, `bin`, `obj` (nếu có)
3. Mở lại Visual Studio
4. Right-click vào Solution → Clean Solution
5. Right-click vào Solution → Rebuild Solution

### Bước 3: Nếu vẫn còn lỗi
Chạy các lệnh sau trong PowerShell:

```powershell
# Di chuyển đến thư mục project
cd "D:\CodeCsharp\Student-Management-System_CSharp_SGU2025\Student-Management-System_CSharp_SGU2025\Student-Management-System_CSharp_SGU2025"

# Xóa packages cũ
Remove-Item -Path "packages" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "..\packages" -Recurse -Force -ErrorAction SilentlyContinue

# Xóa cache
Remove-Item -Path "obj" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "bin" -Recurse -Force -ErrorAction SilentlyContinue

# Download NuGet.exe
Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile "nuget.exe"

# Restore packages
.\nuget.exe restore packages.config -PackagesDirectory "..\packages"

# Clean up
Remove-Item "nuget.exe" -Force

# Rebuild
msbuild "Student-Management-System_CSharp_SGU2025.csproj" /t:Rebuild /p:Configuration=Debug
```

## Cấu trúc thư mục đúng

```
Student-Management-System_CSharp_SGU2025/
├── Student-Management-System_CSharp_SGU2025/
│   ├── Student-Management-System_CSharp_SGU2025/
│   │   ├── Student-Management-System_CSharp_SGU2025.csproj
│   │   ├── packages.config
│   │   ├── NuGet.Config
│   │   └── restore-packages.bat
│   └── packages/                    ← Packages phải ở đây!
│       ├── BouncyCastle.Cryptography.2.5.1/
│       ├── Guna.UI2.WinForms.2.0.4.7/
│       └── ...
```

## Lưu ý quan trọng
- File `.csproj` có HintPath là `..\packages\...`
- Packages phải nằm trong thư mục cha của project
- Không commit thư mục `packages/` vào Git
- File `NuGet.Config` đã được cấu hình đúng

## Kiểm tra
Chạy lệnh sau để kiểm tra packages đã được restore đúng chưa:

```powershell
Test-Path "..\packages\Guna.UI2.WinForms.2.0.4.7"
Test-Path "..\packages\System.Configuration.ConfigurationManager.8.0.0"
```

Kết quả phải là `True` cho cả hai.
