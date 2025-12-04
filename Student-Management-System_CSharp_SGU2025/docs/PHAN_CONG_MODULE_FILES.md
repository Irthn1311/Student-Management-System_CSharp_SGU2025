# Danh sách các file liên quan đến module Phân Công Giảng Dạy

**Ngày cập nhật:** 2025-01-23  
**Module:** Phân Công Giảng Dạy (Teaching Assignment)

---

## 📋 Tổng quan

Module Phân Công Giảng Dạy bao gồm các thành phần sau:
- **Database Schema**: Bảng chính và bảng tạm
- **DTO**: Data Transfer Objects
- **DAO**: Data Access Objects
- **BUS**: Business Logic Layer
- **Services**: Các service hỗ trợ (Auto Assignment, Persist, Seeding)
- **GUI**: Giao diện người dùng (Forms, UserControls)
- **Documentation**: Tài liệu hướng dẫn và báo cáo

---

## 🗄️ Database Schema Files

### 1. **01_schema.sql**
- **Vị trí:** `ConnectDatabase/01_schema.sql`
- **Mô tả:** Định nghĩa schema cho các bảng:
  - `PhanCongGiangDay` - Bảng phân công chính thức
  - `PhanCong_Temp` - Bảng phân công tạm thời (preview)
  - `ThoiKhoaBieu` - Bảng thời khóa biểu chính thức
  - `TKB_Temp` - Bảng thời khóa biểu tạm thời
  - `GiaoVien` - Bảng giáo viên (có `MaMonChuyenMon`)
  - `LopHoc` - Bảng lớp học (có `MaGiaoVienChuNhiem`)
  - `MonHoc` - Bảng môn học
  - `HocKy` - Bảng học kỳ

### 2. **02_unique_indexes.sql**
- **Vị trí:** `ConnectDatabase/02_unique_indexes.sql`
- **Mô tả:** Định nghĩa các unique constraints và indexes cho module

### 3. **03_sample_seed.sql**
- **Vị trí:** `ConnectDatabase/03_sample_seed.sql`
- **Mô tả:** Dữ liệu mẫu cho các bảng liên quan

### 4. **04_full_assignment_seed.sql**
- **Vị trí:** `ConnectDatabase/04_full_assignment_seed.sql`
- **Mô tả:** Script seed dữ liệu phân công đầy đủ cho testing

---

## 📦 DTO (Data Transfer Objects)

### 1. **PhanCongGiangDayDTO.cs**
- **Vị trí:** `DTO/PhanCongGiangDayDTO.cs`
- **Mô tả:** DTO cho phân công giảng dạy
- **Properties:**
  - `MaPhanCong` (int)
  - `MaLop` (int)
  - `MaGiaoVien` (string)
  - `MaMonHoc` (int)
  - `MaHocKy` (int)
  - `NgayBatDau` (DateTime)
  - `NgayKetThuc` (DateTime)

### 2. **GiaoVienDTO.cs**
- **Vị trí:** `DTO/GiaoVienDTO.cs`
- **Mô tả:** DTO cho giáo viên (có `MaMonChuyenMon` và `TenMonChuyenMon`)
- **Liên quan:** Module sử dụng để kiểm tra chuyên môn giáo viên

### 3. **LopDTO.cs** / **LopHocDTO.cs**
- **Vị trí:** `DTO/LopDTO.cs`, `DTO/LopHocDTO.cs`
- **Mô tả:** DTO cho lớp học (có `MaGiaoVienChuNhiem`)
- **Liên quan:** Module sử dụng để hiển thị giáo viên chủ nhiệm

### 4. **TimeTableSlotDTO.cs**
- **Vị trí:** `DTO/TimeTableSlotDTO.cs`
- **Mô tả:** DTO cho hiển thị slot thời khóa biểu (liên quan đến phân công)

---

## 🔌 DAO (Data Access Objects)

### 1. **PhanCongGiangDayDAO.cs**
- **Vị trí:** `dao/PhanCongGiangDayDAO.cs`
- **Mô tả:** Data Access Layer cho phân công giảng dạy
- **Chức năng chính:**
  - CRUD operations cho `PhanCongGiangDay`
  - CRUD operations cho `PhanCong_Temp`
  - Kiểm tra trùng lặp và validation
  - Lấy phân công theo lớp/GV/môn/học kỳ
  - Kiểm tra trạng thái học kỳ (có phân công chính thức/tạm thời)

### 2. **GiaoVienDAO.cs**
- **Vị trí:** `dao/GiaoVienDAO.cs`
- **Mô tả:** Data Access Layer cho giáo viên
- **Liên quan:** Module sử dụng để:
  - Lấy danh sách giáo viên có chuyên môn
  - Kiểm tra `MaMonChuyenMon`
  - Lấy giáo viên chủ nhiệm

### 3. **LopHocDAO.cs**
- **Vị trí:** `dao/LopHocDAO.cs`
- **Mô tả:** Data Access Layer cho lớp học
- **Liên quan:** Module sử dụng để lấy thông tin lớp và giáo viên chủ nhiệm

### 4. **ThoiKhoaBieuDAO.cs**
- **Vị trí:** `dao/ThoiKhoaBieuDAO.cs`
- **Mô tả:** Data Access Layer cho thời khóa biểu
- **Liên quan:** Module phân công liên kết với thời khóa biểu qua `MaPhanCong`

---

## 💼 BUS (Business Logic Layer)

### 1. **PhanCongGiangDayBUS.cs**
- **Vị trí:** `bus/PhanCongGiangDayBUS.cs`
- **Mô tả:** Business Logic Layer cho phân công giảng dạy
- **Chức năng chính:**
  - CRUD operations với validation
  - Kiểm tra chuyên môn giáo viên
  - Kiểm tra trùng lặp phân công
  - Kiểm tra học kỳ cho phép chỉnh sửa
  - Wrapper methods cho semester status checking

### 2. **GiaoVienBUS.cs**
- **Vị trí:** `bus/GiaoVienBUS.cs`
- **Mô tả:** Business Logic Layer cho giáo viên
- **Liên quan:** Module sử dụng để validate và lấy thông tin giáo viên

### 3. **LopHocBUS.cs**
- **Vị trí:** `bus/LopHocBUS.cs`
- **Mô tả:** Business Logic Layer cho lớp học
- **Liên quan:** Module sử dụng để lấy thông tin lớp

### 4. **HocKyBUS.cs**
- **Vị trí:** `bus/HocKyBUS.cs`
- **Mô tả:** Business Logic Layer cho học kỳ
- **Liên quan:** Module sử dụng để lấy danh sách học kỳ và kiểm tra học kỳ hiện tại

### 5. **MonHocBUS.cs**
- **Vị trí:** `bus/MonHocBUS.cs`
- **Mô tả:** Business Logic Layer cho môn học
- **Liên quan:** Module sử dụng để lấy danh sách môn học

---

## ⚙️ Services (Supporting Services)

### 1. **AssignmentAutoService.cs**
- **Vị trí:** `Services/AssignmentAutoService.cs`
- **Mô tả:** Service tự động tạo phân công giảng dạy
- **Chức năng:**
  - Generate auto assignments dựa trên heuristic
  - Validate assignments
  - Scoring và ranking giáo viên
  - Filter theo khối, môn học, học kỳ
  - Chỉ phân công giáo viên có chuyên môn đúng

### 2. **AssignmentPersistService.cs**
- **Vị trí:** `Services/AssignmentPersistService.cs`
- **Mô tả:** Service lưu trữ phân công (tạm thời và chính thức)
- **Chức năng:**
  - Lưu vào `PhanCong_Temp`
  - Chấp nhận từ `PhanCong_Temp` → `PhanCongGiangDay`
  - Rollback từ `PhanCong_Temp`
  - Transaction management

### 3. **SeedingService.cs**
- **Vị trí:** `Services/SeedingService.cs`
- **Mô tả:** Service tạo dữ liệu mẫu cho testing
- **Chức năng:**
  - Seed phân công đầy đủ cho một học kỳ
  - Tạo dữ liệu test

### 4. **TimetableHybridService.cs**
- **Vị trí:** `Services/TimetableHybridService.cs`
- **Mô tả:** Service tích hợp phân công và thời khóa biểu
- **Liên quan:** Module phân công cung cấp dữ liệu cho thời khóa biểu

### 5. **TKBExportService.cs**
- **Vị trí:** `Services/TKBExportService.cs`
- **Mô tả:** Service xuất thời khóa biểu (có thể liên quan đến phân công)
- **Liên quan:** Xuất báo cáo dựa trên phân công

---

## 🎨 GUI (User Interface)

### 1. **PhanCongGiangDay.cs** (UserControl)
- **Vị trí:** `GUI/PhanCongGiangDay/PhanCongGiangDay.cs`
- **Designer:** `GUI/PhanCongGiangDay/PhanCongGiangDay.Designer.cs`
- **Resource:** `GUI/PhanCongGiangDay/PhanCongGiangDay.resx`
- **Mô tả:** UserControl chính để quản lý phân công giảng dạy
- **Chức năng:**
  - Hiển thị danh sách phân công trong DataGridView
  - Filter theo học kỳ, khối, lớp, môn học
  - Thêm/sửa/xóa phân công
  - Mở form auto assignment
  - Stat cards hiển thị thống kê
  - Sử dụng `BindingList` để tối ưu performance

### 2. **frmAutoPhanCongPreview.cs** (Form)
- **Vị trí:** `GUI/PhanCongGiangDay/frmAutoPhanCongPreview.cs`
- **Designer:** `GUI/PhanCongGiangDay/frmAutoPhanCongPreview.Designer.cs`
- **Resource:** `GUI/PhanCongGiangDay/frmAutoPhanCongPreview.resx`
- **Mô tả:** Form preview và quản lý phân công tự động
- **Chức năng:**
  - Filter theo học kỳ, khối, môn học
  - Generate auto assignments
  - Validate assignments
  - Lưu tạm vào `PhanCong_Temp`
  - Chấp nhận từ `PhanCong_Temp` → `PhanCongGiangDay`
  - Rollback

### 3. **FrmThemPhanCongGiangDay.cs** (Form)
- **Vị trí:** `GUI/ThemSua(Phuc)/FrmThemPhanCongGiangDay.cs`
- **Designer:** `GUI/ThemSua(Phuc)/FrmThemPhanCongGiangDay.Designer.cs`
- **Mô tả:** Form thêm phân công giảng dạy thủ công
- **Chức năng:**
  - Chọn học kỳ → Lớp → Giáo viên
  - Tự động lấy môn học từ `MaMonChuyenMon` của giáo viên
  - Hiển thị giáo viên chủ nhiệm ở đầu danh sách
  - Validate và thêm phân công

### 4. **StatCardPhanCongGiangDay.cs** (UserControl)
- **Vị trí:** `GUI/statcardLHP/StatCardPhanCongGiangDay.cs`
- **Designer:** `GUI/statcardLHP/StatCardPhanCongGiangDay.Designer.cs`
- **Mô tả:** UserControl hiển thị thống kê phân công (có thể được sử dụng trong dashboard)

---

## 📄 Supporting Files

### 1. **MoveResult.cs**
- **Vị trí:** `bus/MoveResult.cs`
- **Mô tả:** Class kết quả khi move data từ temp sang official
- **Liên quan:** Sử dụng trong `AssignmentPersistService`

---

## 📚 Documentation Files

### 1. **TEACHING_ASSIGNMENT_SURVEY_REPORT.md**
- **Vị trí:** `TEACHING_ASSIGNMENT_SURVEY_REPORT.md`
- **Mô tả:** Báo cáo khảo sát implementation hiện tại

### 2. **HDSD_PhanCongGiangDay.md**
- **Vị trí:** `docs/HDSD_PhanCongGiangDay.md`
- **Mô tả:** Hướng dẫn sử dụng module phân công giảng dạy

### 3. **BUS_REVIEW_PhanCongGiangDayBUS.md**
- **Vị trí:** `BUS_REVIEW_PhanCongGiangDayBUS.md`
- **Mô tả:** Code review cho BUS layer

### 4. **CODE_REVIEW_PhanCongGiangDayDAO.md**
- **Vị trí:** `CODE_REVIEW_PhanCongGiangDayDAO.md`
- **Mô tả:** Code review cho DAO layer

### 5. **DRY_RUN_ANALYSIS_AssignmentAutoService.md**
- **Vị trí:** `DRY_RUN_ANALYSIS_AssignmentAutoService.md`
- **Mô tả:** Phân tích dry run cho auto assignment service

### 6. **PERFORMANCE_REVIEW_TimetableModule.md**
- **Vị trí:** `PERFORMANCE_REVIEW_TimetableModule.md`
- **Mô tả:** Review performance cho module thời khóa biểu (liên quan)

### 7. **PERFORMANCE_FIXES_IMPLEMENTED.md**
- **Vị trí:** `PERFORMANCE_FIXES_IMPLEMENTED.md`
- **Mô tả:** Các fix performance đã implement

---

## 📊 Tổng kết

### Số lượng file theo loại:
- **Database Schema:** 4 files
- **DTO:** 4 files
- **DAO:** 4 files
- **BUS:** 5 files
- **Services:** 5 files
- **GUI:** 7 files (3 forms + 1 UserControl + 1 StatCard)
- **Supporting:** 1 file
- **Documentation:** 7 files

### **Tổng cộng:** ~37 files liên quan trực tiếp

---

## 🔗 Dependencies

Module Phân Công Giảng Dạy phụ thuộc vào:
- Module **Giáo viên** (GiaoVienDTO, GiaoVienDAO, GiaoVienBUS)
- Module **Lớp học** (LopDTO, LopHocDAO, LopHocBUS)
- Module **Môn học** (MonHocDTO, MonHocDAO, MonHocBUS)
- Module **Học kỳ** (HocKyDTO, HocKyDAO, HocKyBUS)
- Module **Thời khóa biểu** (ThoiKhoaBieuDAO, ThoiKhoaBieuBUS) - liên kết qua `MaPhanCong`

---

## ✅ Checklist kiểm tra

Khi làm việc với module này, cần kiểm tra:
- [ ] Database schema đã được cập nhật (`MaMonChuyenMon` trong `GiaoVien`)
- [ ] DTO đã có đầy đủ properties cần thiết
- [ ] DAO đã implement đầy đủ CRUD và helper methods
- [ ] BUS đã có validation logic
- [ ] Services đã hoạt động đúng với policy mới (chỉ GV có chuyên môn)
- [ ] GUI đã cập nhật workflow mới (Học kỳ → Lớp → Giáo viên)
- [ ] Event handlers đã được gắn đúng
- [ ] Error handling đã đầy đủ
- [ ] Performance đã được tối ưu (BindingList, caching)

---

**Lưu ý:** File này nên được cập nhật khi có thay đổi trong module.

