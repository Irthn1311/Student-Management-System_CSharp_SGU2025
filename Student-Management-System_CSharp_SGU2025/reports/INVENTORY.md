# INVENTORY - KIỂM KÊ MÃ NGUỒN DỰ ÁN

**Dự án:** Student Management System CSharp SGU2025  
**Ngôn ngữ:** C# (.NET Framework 4.7.2)  
**Database:** MySQL  
**UI Framework:** WinForms (Guna.UI2)  
**Ngày kiểm kê:** ${new Date().toISOString().split('T')[0]}

---

## 1. CẤU TRÚC TỔNG QUAN

### 1.1 Thống kê
- **Tổng số file:** 180+ files
- **Các thư mục chính:**
  - `dao/` - 13 files (Data Access Object)
  - `bus/` - 13 files (Business Logic Layer)  
  - `DTO/` - 15 files (Data Transfer Object)
  - `GUI/` - 100+ files (Forms, UserControls)
  - `ConnectDatabase/` - Connection management

### 1.2 Packages/NuGet
```
- MySql.Data (9.4.0) - Database connection
- Guna.UI2.WinForms (2.0.4.7) - UI Components
- EPPlus (7.0.9) - Excel export
- iTextSharp (5.5.13.4) - PDF generation
- ClosedXML (0.105.0) - Excel processing
```

---

## 2. DANH SÁCH LỚP DAO (Data Access Object)

| Lớp | Chức năng | Trạng thái |
|-----|-----------|------------|
| `DiemSoDAO.cs` | CRUD điểm số, thống kê điểm | ✅ Implemented |
| `GiaoVienDAO.cs` | CRUD giáo viên, kiểm tra trùng | ✅ Implemented |
| `HanhKiemDAO.cs` | CRUD hạnh kiểm theo học kỳ | ✅ Implemented |
| `HocKyDAO.cs` | CRUD học kỳ | ✅ Implemented |
| `HocSinhDAO.cs` | CRUD học sinh, kiểm tra trùng SĐT/Email | ✅ Implemented |
| `HocSinhPhuHuynhDAO.cs` | Liên kết HS-PH | ✅ Implemented |
| `LopHocDAO.cs` | CRUD lớp học | ✅ Implemented |
| `MonHocDAO.cs` | CRUD môn học | ✅ Implemented |
| `NamHocDAO.cs` | CRUD năm học | ✅ Implemented |
| `NhapDiemDAO.cs` | Nhập điểm vào DB | ✅ Implemented |
| `PhanCongGiangDayDAO.cs` | Phân công giảng dạy | ✅ Implemented |
| `PhanLopDAO.cs` | Phân lớp học sinh | ✅ Implemented |
| `PhuHuynhDAO.cs` | CRUD phụ huynh | ✅ Implemented |

### 2.1 Đánh giá DAO
✅ **Ưu điểm:**
- Sử dụng MySqlConnection với `using` pattern (tự động dispose)
- Có parameterized queries (tránh SQL injection)
- Có transaction support ở HocSinhDAO.ThemHocSinh()

⚠️ **Vấn đề:**
- Không có connection pooling (mỗi call mở/đóng connection)
- Một số method không có try-catch đầy đủ
- Chưa có logging/audit trail

---

## 3. DANH SÁCH LỚP BUS/BLL (Business Logic Layer)

| Lớp | Chức năng | Validation | Trạng thái |
|-----|-----------|------------|------------|
| `GiaoVienBUS.cs` | Quản lý GV, check tuổi ≥22, email/SĐT | ✅ | ✅ Implemented |
| `HanhKiemBUS.cs` | Xếp loại hạnh kiểm | ⚠️ Partial | ⚠️ In Progress |
| `HocKyBUS.cs` | Quản lý học kỳ | ✅ | ✅ Implemented |
| `HocSinhBLL.cs` | Quản lý HS, validation tuổi 16-18, check trùng | ✅ | ✅ Implemented |
| `HocSinhPhuHuynhBLL.cs` | Liên kết HS-PH | ⚠️ | ⚠️ In Progress |
| `LopHocBUS.cs` | Quản lý lớp học | ✅ | ✅ Implemented |
| `MonHocBUS.cs` | Quản lý môn học | ✅ | ✅ Implemented |
| `NamHocBUS.cs` | Quản lý năm học | ✅ | ✅ Implemented |
| `NhapDiemBUS.cs` | Logic nhập điểm | ⚠️ | ⚠️ In Progress |
| `PhanCongGiangDayBUS.cs` | Phân công giảng dạy | ⚠️ Partial | ⚠️ In Progress |
| `PhanLopBLL.cs` | Logic phân lớp | ⚠️ | ⚠️ In Progress |
| `PhuHuynhBLL.cs` | Quản lý phụ huynh | ✅ | ✅ Implemented |
| `ThemDiemBUS.cs` | Thêm điểm | ⚠️ | ⚠️ In Progress |

### 3.1 Đánh giá BUS
✅ **Ưu điểm:**
- HocSinhBLL có validation chi tiết (tuổi, email, SĐT)
- GiaoVienBUS có business rules rõ ràng
- Có kiểm tra trùng lặp

⚠️ **Vấn đề:**
- Một số BUS thiếu validation (NhapDiemBUS, HanhKiemBUS)
- Chưa có tính toán điểm TB tự động
- Chưa có ràng buộc phân công (giờ dạy không trùng)

---

## 4. DANH SÁCH LỚP DTO (Data Transfer Object)

| Lớp | Số Properties | Validation | Trạng thái |
|-----|--------------|------------|------------|
| `ChiTietDiemDTO.cs` | 8+ | ✅ | ✅ Implemented |
| `DiemSoDTO.cs` | 8 | ✅ | ✅ Implemented |
| `GiaoVienDTO.cs` | 8 | ✅ | ✅ Implemented |
| `HanhKiemDTO.cs` | 4 | ✅ | ✅ Implemented |
| `HocKyDTO.cs` | 5 | ✅ | ✅ Implemented |
| `HocSinhDTO.cs` | 7 | ✅ Strong | ✅ Implemented |
| `KhoiLop.cs` | 2 | ✅ | ✅ Implemented |
| `LopDTO.cs` | 3 | ✅ | ✅ Implemented |
| `LopHocDTO.cs` | 4+ | ✅ | ✅ Implemented |
| `MonHocDTO.cs` | 4 | ✅ | ✅ Implemented |
| `NamHocDTO.cs` | 4 | ✅ | ✅ Implemented |
| `NhapDiemDTO.cs` | 7+ | ✅ | ✅ Implemented |
| `PhanCongGiangDayDTO.cs` | 7 | ⚠️ Partial | ⚠️ |
| `PhuHuynhDTO.cs` | 5 | ✅ | ✅ Implemented |
| `ThoiKhoaBieuDTO.cs` | 6 | ✅ | ✅ Implemented |
| `ThongKeDTO.cs` | Variable | ⚠️ | ⚠️ |

### 4.1 Đánh giá DTO
✅ **Ưu điểm:**
- Most DTOs có validation trong setters (ArgumentExceptions)
- HocSinhDTO có logic phức tạp (check tuổi, email format)

⚠️ **Vấn đề:**
- Một số DTO chưa có validation (ThongKeDTO)
- Cần thêm nullable types ở một số field

---

## 5. DANH SÁCH FORMS & USERCONTROLS

### 5.1 Authentication & Main
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `FrmDangNhap.cs` | Form | Đăng nhập | ✅ Click | ✅ |
| `frmMain.cs` | Form | Form chính với sidebar | ✅ Navigation | ✅ |
| `ucHeader.cs` | UserControl | Header với breadcrumb | - | ✅ |
| `ucSidebar.cs` | UserControl | Sidebar navigation | ✅ All buttons | ✅ |

### 5.2 Dashboard
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `ucDashboard.cs` | UserControl | Dashboard tổng quan | ⚠️ Partial | ⚠️ |
| `CardHoatDongNoiBatDashboard.cs` | UC | Card hoạt động | ⚠️ | ⚠️ |
| `RecentActivityItem.cs` | UC | Recent activity | ⚠️ | ⚠️ |
| `StatCard.cs` | UC | Stat card | ✅ | ✅ |
| `ThongKeCard.cs` | UC | Thống kê card | ✅ | ✅ |

### 5.3 Học sinh
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `HocSinh.cs` | UserControl | Quản lý học sinh | ✅ CRUD | ✅ |
| `ThemHoSoHocSinh.cs` | Form | Thêm hồ sơ HS | ✅ Submit | ✅ |
| `ChinhSuaHocSinh.cs` | Form | Sửa hồ sơ HS | ✅ Submit | ✅ |
| `PhanLop.cs` | Form | Phân lớp | ✅ | ✅ |
| `HocSinhPhuHuynh` | Logic | Liên kết HS-PH | ⚠️ Partial | ⚠️ |

### 5.4 Giáo viên
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `GiaoVien.cs` | UserControl | Quản lý GV | ✅ CRUD | ✅ |

### 5.5 Điểm số
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `DiemSo_NhapDiem.cs` | UserControl | Nhập điểm | ⚠️ Partial | ⚠️ |
| `ChiTietDiem.cs` | Form | Chi tiết điểm | ✅ | ✅ |
| `ThemDiem.cs` | Form | Thêm điểm | ⚠️ | ⚠️ |

### 5.6 Hạnh kiểm & Xếp loại
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `HanhKiem.cs` | UserControl | Quản lý hạnh kiểm | ⚠️ | ⚠️ In Progress |
| `ucXepLoai.cs` | UserControl | Xếp loại HS | ⚠️ | ⚠️ In Progress |

### 5.7 Lớp học & Môn học
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `LopKhoi.cs` | UserControl | Lớp & Khối | ✅ | ✅ |
| `FrmMonHoc.cs` | UserControl | Môn học | ✅ | ✅ |
| `ThemLopHoc.cs` | Form | Thêm lớp | ✅ | ✅ |
| `SuaLopHoc.cs` | Form | Sửa lớp | ✅ | ✅ |
| `FrmThemMonHoc.cs` | Form | Thêm môn | ✅ | ✅ |
| `FrmSuaMonHoc.cs` | Form | Sửa môn | ✅ | ✅ |

### 5.8 Phân công & Thời khóa biểu
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `PhanCongGiangDay.cs` | UserControl | Phân công GV | ⚠️ Partial | ⚠️ |
| `ThoiKhoaBieu.cs` | UserControl | Hiển thị TKB | ⚠️ | ⚠️ In Progress |
| `FrmThemPhanCongGiangDay.cs` | Form | Thêm phân công | ⚠️ | ⚠️ |

### 5.9 Báo cáo & Thống kê
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `ucBaoCao.cs` | UserControl | Báo cáo chính | ⚠️ | ⚠️ Partial |
| `ucBaoCaoBangDiem.cs` | UC | Bảng điểm | ⚠️ | ⚠️ |
| `ucBaoCaoThongKeHocLuc.cs` | UC | Thống kê học lực | ⚠️ | ⚠️ |

### 5.10 Năm học & Học kỳ
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `ucNamHoc.cs` | UserControl | Năm học | ✅ | ✅ |
| `frmThemNamHoc.cs` | Form | Thêm năm học | ✅ | ✅ |
| `frmThemHocKy.cs` | Form | Thêm học kỳ | ✅ | ✅ |

### 5.11 Tài khoản & Quyền
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `FrmTaiKhoan.cs` | UserControl | Quản lý TK | ⚠️ | ⚠️ Partial |
| `frmPhanQuyen.cs` | Form | Phân quyền | ⚠️ | ⚠️ |
| `frmAddPhanQuyen.cs` | Form | Thêm phân quyền | ⚠️ | ⚠️ |
| `RoleItem.cs` | UC | Role item | ✅ | ✅ |

### 5.12 Thông báo & Cài đặt
| File | Loại | Mô tả | Handler | Trạng thái |
|------|------|-------|---------|------------|
| `ThongBao.cs` | UserControl | Thông báo | ⚠️ | ⚠️ In Progress |
| `CaiDat.cs` | UserControl | Cài đặt | ⚠️ | ⚠️ In Progress |
| `DanhGia.cs` | UserControl | Đánh giá | ⚠️ | ⚠️ |

---

## 6. CANDIDATES FOR REMOVAL

### 6.1 File thừa/Duplicate/Unused
| File | Lý do | Bằng chứng |
|------|-------|-----------|
| `GUI/GiaBao/` | Duplicate sub-project | Có folder riêng với Program.cs, csproj riêng |
| `Form2.resx` | File không được sử dụng | Không có Form2.cs |
| Một số file `.resx` không khớp | Thiếu source file hoặc có file .resx không dùng | Cần kiểm tra chi tiết |
| `Images/` (root) vs `GUI/Images/` | Duplicate - có 2 thư mục ảnh | Nên merge vào GUI/Images |

### 6.2 Kết luận
⚠️ **Cần điều tra thêm:**
- Folder `GUI/GiaBao/` có file riêng (Program.cs) - có thể là project con hoặc duplicate
- Một số file `.resx` có thể không khớp với `.cs`

✅ **Nên giữ:**
- Tất cả DAO/BUS/DTO files - đều có sử dụng
- Phần lớn GUI files - cần thiết cho hệ thống

---

## 7. TIỆN ÍCH VÀ CLASS HỖ TRỢ

| File | Chức năng | Trạng thái |
|------|-----------|------------|
| `ConnectionDatabase.cs` | Quản lý kết nối MySQL | ✅ Good |
| `Program.cs` | Entry point, check DB connection | ✅ |

### 7.1 Đánh giá ConnectionDatabase.cs
✅ **Ưu điểm:**
- Có TestConnection(), TestConnectionWithMessage()
- Có CheckDatabaseStructure() 
- Error handling tốt với MySQL error codes
- Có debug logging

⚠️ **Vấn đề:**
- Connection string hard-coded: `"Server=localhost;Database=QuanLyHocSinh;Uid=root;Pwd=;"`
- Nên đọc từ App.config hoặc Settings
- Không có connection pooling

---

## 8. TỔNG KẾT

### 8.1 Points mạnh
✅ Kiến trúc rõ ràng (DAO-BUS-GUI)  
✅ Validation tốt ở BUS layer  
✅ Có transaction support  
✅ UI components đẹp (Guna.UI2)  
✅ Database schema đầy đủ  

### 8.2 Điểm yếu
⚠️ Chưa có logging system  
⚠️ Connection string hard-coded  
⚠️ Một số form chưa hoàn thiện (Điểm số, Hạnh kiểm, TKB)  
⚠️ Chưa có unit tests  
⚠️ Chưa có error logging  
⚠️ Chưa có backup/restore data

### 8.3 Các module hoàn thiện
- ✅ Học sinh (DAO/BUS/GUI đầy đủ)
- ✅ Giáo viên (DAO/BUS/GUI đầy đủ)  
- ✅ Môn học (DAO/BUS/GUI đầy đủ)
- ✅ Lớp học (DAO/BUS/GUI đầy đủ)
- ✅ Năm học/Học kỳ (DAO/BUS/GUI đầy đủ)

### 8.4 Các module cần bổ sung
- ⚠️ Điểm số (thiếu validation tự động)
- ⚠️ Hạnh kiểm (thiếu logic xếp loại tự động)
- ⚠️ Xếp loại (thiếu tính toán học lực)
- ⚠️ Thời khóa biểu (thiếu auto-schedule)
- ⚠️ Báo cáo (thiếu export PDF/Excel)
- ⚠️ RBAC (chưa implement fully)

---

## 9. Scheduling Assets
- Thư mục `Scheduling/`
  - `Scheduling/Models.cs`: `ScheduleRequest`, `ScheduleSolution`, `AssignmentSlot`, `WeightConfig`, `SoftCounts`, `ConflictReport`, `AssignmentRequirement`, `SlotsConfig`.
  - `Scheduling/SchedulingService.cs`: `GenerateSchedule`, `BuildRequestFromDatabase`, `ValidateHardConstraints`, `AnalyzeConflicts`, `EvaluateCost`, `PersistToTemp`, `AcceptToOfficial`, `RollbackTemp`.
- DAO/BUS
  - `dao/ThoiKhoaBieuDAO.cs`: `ClearTemp()`, `InsertTemp(semesterId, weekNo, solution)`, `AcceptTempToOfficial(semesterId, weekNo)`, `GetWeek(semesterId, weekNo)`, `HasConflict(...)`.
  - `bus/ThoiKhoaBieuBUS.cs`: Bridge methods gọi DAO tương ứng.
  - `bus/PhanCongGiangDayBUS.cs`: Thêm `GetBySemester(int)`, `GetRequiredPeriods(int maLop, int maMon, int semesterId)`.
  - `dao/MonHocDAO.cs`: đã có `SoTiet` làm mặc định số tiết/tuần.
- GUI
  - `GUI/ThoiKhoaBieu/ThoiKhoaBieu.cs`: thêm handler `btnGenerateAuto_Click`, `btnAccept_Click`, `btnRollback_Click`, `RenderFromTemp(...)`.
  - `GUI/ThoiKhoaBieu/ThoiKhoaBieu.Designer.cs`: gán Click cho nút "Sắp xếp tự động", "Lưu thời khóa biểu", "Xóa" tương ứng.
- SQL
  - `ConnectDatabase/QuanLyHocSinh.sql`: thêm bảng `TKB_Temp` (SemesterId, WeekNo, MaLop, Thu, Tiet, MaMon, MaGV, Phong) và chỉ mục.