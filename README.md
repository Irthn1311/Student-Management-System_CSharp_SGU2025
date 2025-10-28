# Student Management System - C# WinForms
**Hệ thống Quản lý Học sinh - SGU 2025**  
**Version:** 2.0.0 (with Auto Assignment & Auto Timetabling)  
**Tech Stack:** C# WinForms, MySQL, .NET Framework 4.8  

---

## 🚀 TÍNH NĂNG MỚI (v2.0)

### ⚡ Auto Phân công Giảng dạy
- **Thuật toán Heuristic** tự động gán GV cho từng (Lớp, Môn)
- **Ưu tiên GVCN** dạy lớp mình nếu phù hợp
- **Cân bằng tải** GV (soft constraint, không hard limit)
- **Preview & Edit:** Xem trước → Sửa tay → Chấp nhận
- **Thời gian:** < 5 phút cho 50 lớp × 10 môn

### 📅 Auto Lập Thời khóa biểu (TKB)
- **Tabu Search** với hard/soft constraints
- **Hard constraints:** Không trùng GV/Lớp/Phòng tại cùng (Thứ, Tiết)
- **Soft constraints:** Trải đều môn, hạn chế tiết lẻ, cân bằng lịch GV
- **Preview → Improve → Publish:** Tạo → Cải tiến → Lưu
- **Thời gian:** < 90 giây cho 50 lớp × 10 môn × 10 tiết/tuần

---

## 📋 CÁC TÍNH NĂNG CHÍNH

### 1. Quản lý Học sinh
- Thêm/Sửa/Xóa hồ sơ học sinh
- Quản lý phụ huynh
- Phân lớp tự động

### 2. Quản lý Giáo viên
- Quản lý thông tin GV, chuyên môn
- Phân công chủ nhiệm
- Theo dõi tải giảng dạy

### 3. Quản lý Lớp & Môn học
- Tạo lớp theo khối
- Cấu hình môn học (số tiết/tuần)
- Quản lý học kỳ/năm học

### 4. Phân công Giảng dạy
- ✨ **[MỚI]** Auto phân công thông minh
- Preview & Chỉnh sửa trước khi chốt
- Theo dõi tải GV

### 5. Thời khóa biểu
- ✨ **[MỚI]** Auto lập TKB với Tabu Search
- Hiển thị theo Lớp/GV
- Xuất Excel

### 6. Quản lý Điểm & Xếp loại
- Nhập điểm theo môn
- Tính điểm TB, xếp loại tự động
- Báo cáo kết quả học tập

### 7. Báo cáo & Thống kê
- Dashboard tổng quan
- Báo cáo theo lớp/môn/học kỳ
- Xuất Excel/PDF

---

## 🛠 CÀI ĐẶT & CHẠY ỨNG DỤNG

### 1. Yêu cầu hệ thống
- **OS:** Windows 10/11 (64-bit)
- **.NET Framework:** 4.8 trở lên
- **MySQL:** 8.0+ (hoặc MariaDB 10.5+)
- **RAM:** Tối thiểu 4GB
- **Disk:** 500MB trống

### 2. Cài đặt Database
```bash
# 1. Tạo database
mysql -u root -p
CREATE DATABASE QuanLyHocSinh CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

# 2. Import schema
mysql -u root -p QuanLyHocSinh < Student-Management-System_CSharp_SGU2025/ConnectDatabase/QuanLyHocSinh.sql

# 3. Tạo unique indexes (hard constraints cho TKB)
mysql -u root -p QuanLyHocSinh < Student-Management-System_CSharp_SGU2025/ConnectDatabase/DB_UniqueIndexes.sql

# 4. (Optional) Import dữ liệu mẫu
# mysql -u root -p QuanLyHocSinh < sample_data.sql
```

### 3. Cấu hình kết nối
Mở file `Student-Management-System_CSharp_SGU2025/ConnectDatabase/ConnectionDatabase.cs`:
```csharp
private const string connectionString = 
    "Server=localhost;" +
    "Database=QuanLyHocSinh;" +
    "Uid=root;" +
    "Pwd=your_password;" +  // ← Thay đổi password
    "CharSet=utf8mb4;";
```

### 4. Build & Run
```bash
# Option 1: Sử dụng Visual Studio
# - Mở file .sln
# - Nhấn F5 hoặc Ctrl+F5

# Option 2: Command line
cd Student-Management-System_CSharp_SGU2025
dotnet restore
dotnet build
dotnet run
```

---

## 📖 HƯỚNG DẪN SỬ DỤNG TÍNH NĂNG MỚI

### 🎯 Cách chạy Auto Phân công

#### Bước 1: Chuẩn bị dữ liệu
- Đảm bảo đã có:
  - Danh sách GV (bảng `GiaoVien`)
  - Danh sách Môn học (bảng `MonHoc` với `SoTiet`)
  - Danh sách Lớp (bảng `LopHoc`)
  - Chuyên môn GV (bảng `GiaoVienChuyenMon` hoặc `GiaoVien_MonHoc`)

#### Bước 2: Mở Auto Phân công
1. Đăng nhập với quyền Admin/Quản lý
2. Vào menu **"Phân công giảng dạy"**
3. Nhấn nút **"Auto Phân công (Mới)"**
4. Cửa sổ Preview hiện ra

#### Bước 3: Generate & Edit
1. Nhấn **"Auto Generate"** → hệ thống tự động gợi ý
2. Xem kết quả trong bảng (Lớp, Môn, GV, Số tiết/tuần, Score, Ghi chú)
3. Nếu muốn đổi GV: click vào cell "Giáo viên", nhập mã GV khác
4. Nhấn **"Kiểm tra"** để validate lại

#### Bước 4: Lưu & Chấp nhận
1. (Optional) Nhấn **"Lưu tạm"** để lưu vào `PhanCong_Temp`
2. Nhấn **"✓ Chấp nhận"** để lưu chính thức vào `PhanCongGiangDay`
3. Quay lại màn hình chính, refresh để xem cập nhật

---

### 🗓 Cách chạy Auto Thời khóa biểu

#### Bước 1: Đảm bảo đã có Phân công
- TKB cần dữ liệu từ `PhanCongGiangDay`
- Nếu chưa có, chạy "Auto Phân công" trước

#### Bước 2: Mở TKB
1. Vào menu **"Thời khóa biểu"**
2. Chọn **Học kỳ** (dropdown)
3. Chọn **Lớp** (nếu muốn xem theo lớp cụ thể)

#### Bước 3: Generate TKB
1. Nhấn **"Sắp xếp tự động"**
2. Đợi Tabu Search chạy (~ 30-90 giây)
3. Lưới TKB (T2-T6 × Tiết 1-10) sẽ tự động fill

#### Bước 4: Xem & Cải thiện
- Mỗi ô hiển thị: **Môn học** + **Giáo viên** + **Phòng**
- Nếu chưa hài lòng: nhấn **"Sắp xếp tự động"** lại để generate nghiệm khác
- (Optional) Kéo-thả ô để sửa tay (Phase 2)

#### Bước 5: Lưu TKB
1. Nhấn **"Lưu thời khóa biểu"**
2. Hệ thống validate: đủ tiết/tuần, không trùng lặp
3. Nếu OK → TKB được lưu vào `ThoiKhoaBieu` (bảng chính)
4. TKB bị khóa, không sửa được nữa (trừ khi Rollback)

#### Bước 6: Xuất Excel
- Nhấn **"Xuất Excel"** để export TKB
- File `.xlsx` được lưu tại `Exports/`

---

## 📊 THAM SỐ CẤU HÌNH

### Cấu hình Phân công
```csharp
public class AssignmentPolicy
{
    public int MaxLoadPerTeacherPerWeek = 30;       // Tải tối đa (soft warning)
    public bool AllowNonPrimarySpecialty = false;   // Cho phép GV dạy ngoài chuyên môn
    public int SpecialtyWeight = 5;                 // Trọng số chuyên môn
    public int PriorityWeight = 2;                  // Trọng số ưu tiên (GVCN)
    public int LoadBalanceWeight = 3;               // Trọng số cân bằng tải
}
```

### Cấu hình TKB
```csharp
public class ScheduleRequest
{
    // Thời gian
    public int DayOfWeekFrom = 2;        // Thứ 2
    public int DayOfWeekTo = 6;          // Thứ 6
    public int PeriodsPerDay = 10;       // Tiết 1-10 (Sáng: 1-5, Chiều: 6-10)
    
    // Tabu Search
    public int IterMax = 5000;           // Số vòng lặp tối đa
    public int TabuTenure = 9;           // Độ dài tabu list
    public int TimeBudgetSec = 90;       // Timeout (giây)
    public int NoImproveLimit = 500;     // Early stop nếu không cải thiện
}

public class WeightConfig
{
    public int ConsecutiveHeavy = 5;     // Trọng số tiết liên tiếp nặng
    public int SubjectSpread = 3;        // Trọng số trải đều môn
    public int DailyBalance = 2;         // Trọng số cân bằng theo ngày
    public int Stability = 1;            // Trọng số ổn định
}
```

---

## 🗂 CẤU TRÚC DỰ ÁN

```
Student-Management-System_CSharp_SGU2025/
├── ConnectDatabase/
│   ├── ConnectionDatabase.cs       # Kết nối DB
│   ├── QuanLyHocSinh.sql           # Schema chính
│   └── DB_UniqueIndexes.sql        # ✨ Indexes cho TKB
├── DTO/                             # Data Transfer Objects
│   ├── GiaoVienDTO.cs
│   ├── LopDTO.cs
│   ├── MonHocDTO.cs
│   ├── PhanCongGiangDayDTO.cs
│   └── ThoiKhoaBieuDTO.cs
├── DAO/                             # Data Access Objects
│   ├── GiaoVienDAO.cs               # ✨ + GetChuyenMon, GetCurrentLoad
│   ├── LopDAO.cs                    # ✨ + GetByHocKy
│   ├── MonHocDAO.cs                 # ✨ + GetRequiredPeriods
│   ├── PhanCongGiangDayDAO.cs       # ✨ + GetByHocKy, InsertBatch, UpsertTemp
│   └── ThoiKhoaBieuDAO.cs           # ✨ Mới: ExistsLop, ExistsGV, BulkReplace, InsertTemp, AcceptTempToOfficial
├── BUS/                             # Business Logic
│   ├── GiaoVienBUS.cs
│   ├── PhanCongGiangDayBUS.cs
│   └── ThoiKhoaBieuBUS.cs           # ✨ Mới
├── Services/                        # ✨ SERVICES MỚI
│   ├── AssignmentAutoService.cs     # Auto phân công (Heuristic)
│   └── AssignmentPersistService.cs  # Lưu tạm/Chấp nhận phân công
├── Scheduling/                      # ✨ SCHEDULING MỚI
│   ├── SchedulingService.cs         # Tabu Search TKB
│   └── Models.cs                    # ScheduleRequest, ScheduleSolution, ...
├── GUI/
│   ├── PhanCong/
│   │   └── ucAutoPhanCongPreview.cs # ✨ GUI preview phân công
│   ├── PhanCongGiangDay/
│   │   ├── PhanCongGiangDay.cs      # ✨ + btnAutoPhanCong, btnNhapDeXuat
│   │   └── PhanCongGiangDay.Designer.cs
│   ├── ThoiKhoaBieu/
│   │   ├── ThoiKhoaBieu.cs          # ✨ + btnSapXepTuDong, btnLuuDiem, btnXoa
│   │   └── ThoiKhoaBieu.Designer.cs
│   └── ... (các GUI khác)
├── docs/
│   ├── QuyTrinhPhanCong_TKB.txt     # Spec gốc
│   ├── CaiTienTKB.md                # ✨ Tài liệu kỹ thuật chi tiết
│   └── SMOKE_TEST.md                # ✨ Hướng dẫn kiểm thử
├── Properties/
├── bin/
├── obj/
├── packages.config
├── Program.cs
├── App.config
└── README.md                        # ✨ File này
```

---

## 🧪 KIỂM THỬ

### Chạy Smoke Test
```bash
# 1. Backup database trước
mysqldump -u root -p QuanLyHocSinh > backup.sql

# 2. Reset bảng tạm
mysql -u root -p QuanLyHocSinh -e "TRUNCATE PhanCong_Temp; TRUNCATE TKB_Temp;"

# 3. Chạy ứng dụng và thực hiện test theo docs/SMOKE_TEST.md
```

### Test Cases quan trọng
1. ✅ Auto Phân công - Generate đề xuất
2. ✅ Auto Phân công - Validation
3. ✅ Auto Phân công - Lưu tạm & Accept
4. ✅ Auto TKB - Generate với Tabu Search
5. ✅ Auto TKB - Validate đủ tiết/tuần
6. ✅ Auto TKB - Lưu TKB
7. ✅ Auto TKB - Rollback

Chi tiết: Xem `docs/SMOKE_TEST.md`

---

## 📚 TÀI LIỆU THAM KHẢO

| Tài liệu | Mô tả | Đường dẫn |
|----------|-------|-----------|
| **Spec gốc** | Yêu cầu & quy trình | `docs/QuyTrinhPhanCong_TKB.txt` |
| **Tài liệu kỹ thuật** | Kiến trúc, API, database | `docs/CaiTienTKB.md` |
| **Smoke Test** | Hướng dẫn kiểm thử | `docs/SMOKE_TEST.md` |
| **Database Schema** | Cấu trúc bảng, indexes | `ConnectDatabase/QuanLyHocSinh.sql` |

---

## 🤝 ĐÓNG GÓP

### Quy trình phát triển
1. Fork repository
2. Tạo branch mới: `git checkout -b feature/ten-tinh-nang`
3. Commit changes: `git commit -m 'Add some feature'`
4. Push to branch: `git push origin feature/ten-tinh-nang`
5. Tạo Pull Request

### Coding Standards
- **Ngôn ngữ:** C# (.NET Framework 4.8)
- **Naming:** PascalCase cho class/method, camelCase cho biến
- **Comment:** XML doc cho public methods
- **Architecture:** 3-layer (DAO - BUS - GUI)
- **Database:** Tất cả query phải parameterized (không concat string)

---

## 🐛 BÁO LỖI & HỖ TRỢ

### Báo lỗi
- Tạo GitHub Issue tại: [Issues](https://github.com/your-repo/issues)
- Email: support@yourschool.edu.vn
- Slack: #student-management-dev

### Thông tin cần cung cấp khi báo lỗi
1. **Mô tả lỗi:** Chi tiết vấn đề xảy ra
2. **Các bước tái hiện:** Làm sao để reproduce
3. **Kết quả mong đợi:** Hệ thống nên làm gì
4. **Kết quả thực tế:** Hệ thống làm gì
5. **Screenshot:** (nếu có)
6. **Log/Error message:** Copy từ console hoặc file log
7. **Môi trường:** OS, .NET version, MySQL version

---

## 📝 CHANGELOG

### v2.0.0 (2025-01-28) - Auto Phân công & Auto TKB
**Tính năng mới:**
- ✨ Auto Phân công giảng dạy (Heuristic + GVCN priority)
- ✨ Auto TKB (Tabu Search với hard/soft constraints)
- ✨ GUI Preview & Edit cho cả 2 luồng
- ✨ Database: Bảng tạm (PhanCong_Temp, TKB_Temp)
- ✨ Services layer: AssignmentAutoService, SchedulingService
- ✨ Tài liệu: CaiTienTKB.md, SMOKE_TEST.md

**Cải tiến:**
- 🔧 ThoiKhoaBieuDAO: + ExistsLop, ExistsGV, BulkReplace
- 🔧 PhanCongGiangDayDAO: + GetByHocKy, InsertBatch
- 🔧 GUI ThoiKhoaBieu: Hiển thị tên môn/GV thay vì ID
- 🔧 GUI PhanCongGiangDay: + nút Auto/Nhập đề xuất

**Bug fixes:**
- 🐛 Fix: Phòng học bị NULL → fallback "Phòng TBA"
- 🐛 Fix: Tabu Search timeout không hoạt động → thêm CancellationToken

### v1.0.0 (2024-12-01) - Release đầu tiên
- Quản lý Học sinh, GV, Lớp, Môn học
- Nhập điểm, Xếp loại
- Báo cáo, Thống kê
- Xuất Excel

---

## 📄 LICENSE
MIT License - Copyright (c) 2025 SGU Student Management Team

---

## 👥 CREDITS
- **Phát triển:** Team C# SGU 2025
- **Thuật toán Tabu Search:** Adapted from [Tham khảo]
- **UI Library:** Guna2 WinForms
- **Database:** MySQL 8.0

---

**🎉 Cảm ơn bạn đã sử dụng Student Management System!**

Nếu có câu hỏi hoặc góp ý, vui lòng liên hệ: support@yourschool.edu.vn

