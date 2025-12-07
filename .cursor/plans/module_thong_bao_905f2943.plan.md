---
name: Module Thong Bao
overview: Triển khai module Thông Báo với hệ thống thông báo đa cấp (Admin -> GV/Giáo vụ -> HS/PH), hỗ trợ nhiều loại thông báo, theo dõi trạng thái đọc, và tích hợp notification bell vào header.
todos:
  - id: db-schema
    content: Tạo file SQL mở rộng schema ThongBao + NguoiNhanThongBao
    status: completed
  - id: dto
    content: Tạo ThongBaoDTO.cs và NguoiNhanThongBaoDTO.cs
    status: completed
  - id: dao
    content: Tạo ThongBaoDAO.cs với các phương thức CRUD
    status: completed
  - id: bus
    content: Tạo ThongBaoBUS.cs với business logic và validation
    status: completed
  - id: gui-main
    content: Nâng cấp ThongBao.cs kết nối database, phân trang, lọc
    status: completed
  - id: gui-add
    content: Tạo FrmThemThongBao.cs cho form thêm/sửa thông báo
    status: completed
  - id: gui-detail
    content: Tạo FrmChiTietThongBao.cs xem chi tiết
    status: completed
  - id: notification-bell
    content: Tạo ucNotificationBell.cs và tích hợp vào ucHeader
    status: completed
  - id: permission
    content: Cập nhật PermissionHelper.cs với quyền thông báo theo vai trò
    status: completed
---

# Kế hoach triển khai Module Thông Báo

## Tổng quan hệ thống hiện tại

**Cấu trúc project:** 4-layer (DTO -> DAO -> BUS -> GUI) với MySQL database, WinForms + Guna2 components.

**Các vai trò hiện có:** `admin`, `teacher`, `student`, `parent`

**Database ThongBao hiện tại** (`01_schema.sql` line 413-423):

```sql
CREATE TABLE ThongBao (
    MaThongBao INT PRIMARY KEY AUTO_INCREMENT,
    TieuDe VARCHAR(255) NOT NULL,
    NoiDung TEXT NULL,
    NgayTao DATETIME DEFAULT CURRENT_TIMESTAMP,
    LoaiThongBao VARCHAR(50) NOT NULL,
    DoiTuongNhan VARCHAR(255) NOT NULL,  -- Hạn chế: chỉ lưu text
    NgayHetHan DATETIME NULL,
    MaNguoiTao VARCHAR(20),
    FOREIGN KEY (MaNguoiTao) REFERENCES NguoiDung(TenDangNhap)
);
```

**GUI hiện tại** (`ThongBao.cs`): Chỉ có dữ liệu mẫu hardcoded, chưa kết nối database.

---

## Thiết kế mới

### 1. Mở rộng Database Schema

**Bảng NguoiNhanThongBao** (theo dõi người nhận cụ thể + trạng thái đọc):

```sql
CREATE TABLE NguoiNhanThongBao (
    MaThongBao INT,
    TenDangNhap VARCHAR(20),  -- Người nhận
    DaDoc BOOLEAN DEFAULT FALSE,
    NgayDoc DATETIME NULL,
    PRIMARY KEY (MaThongBao, TenDangNhap),
    FOREIGN KEY (MaThongBao) REFERENCES ThongBao(MaThongBao),
    FOREIGN KEY (TenDangNhap) REFERENCES NguoiDung(TenDangNhap)
);
```

**Cập nhật bảng ThongBao:**

- Thêm cột `PhamVi`: `'ALL'`, `'VAI_TRO'`, `'LOP'`, `'KHOI'`, `'CA_NHAN'`
- Thêm cột `MaVaiTroNhan`: để lọc vai trò (teacher, student)
- Thêm cột `MaLop`, `MaKhoi`: để gửi theo lớp/khối cụ thể
- Thêm cột `DoUuTien`: `'BINH_THUONG'`, `'QUAN_TRONG'`, `'KHAN_CAP'`

### 2. Luồng thông báo theo vai trò

| Người gửi | Có thể gửi đến |

|-----------|----------------|

| Admin | Tất cả (GV, HS, PH, toàn trường) |

| Giáo viên CN | Lớp chủ nhiệm (HS + PH trong lớp) |

| Giáo viên BM | HS trong lớp được phân công dạy |

### 3. Các loại thông báo

- **Hệ thống**: Thông báo từ Admin (nghỉ lễ, kế hoạch, sự kiện)
- **Học tập**: Lịch thi, nhập điểm, báo cáo kết quả
- **Họp PH**: Thông báo họp phụ huynh từ GVCN
- **Kỷ luật/Khen thưởng**: Tự động từ module DanhGia
- **Chung**: Các thông báo khác

---

## Danh sách file cần tạo/sửa

### DTO Layer

- `DTO/ThongBaoDTO.cs` (mới)
- `DTO/NguoiNhanThongBaoDTO.cs` (mới)

### DAO Layer

- `DAO/DAOClass/ThongBaoDAO.cs` (mới)
- `DAO/ConnectDatabase/04_thongbao_schema.sql` (mới)

### BUS Layer

- `BUS/BUSClass/ThongBaoBUS.cs` (mới)

### GUI Layer

- `GUI/GUIClass/ThongBao/ThongBao.cs` (sửa toàn bộ)
- `GUI/GUIClass/ThongBao/FrmThemThongBao.cs` (mới)
- `GUI/GUIClass/ThongBao/FrmChiTietThongBao.cs` (mới)
- `GUI/GUIClass/ThongBao/ucThongBaoItem.cs` (mới - item trong danh sách)
- `GUI/GUIClass/Controls/ucNotificationBell.cs` (mới - notification icon)
- `GUI/GUIClass/Controls/ucHeader.cs` (sửa - thêm notification bell)

### Các file khác

- `BUS/Utils/PermissionHelper.cs` (sửa - thêm quyền thông báo)

---

## Chi tiết các chức năng chính

### A. Quản lý thông báo (Admin/GV)

1. Xem danh sách thông báo đã gửi/nhận (phân trang, lọc, tìm kiếm)
2. Tạo thông báo mới (chọn đối tượng nhận, loại, độ ưu tiên)
3. Sửa/Xóa thông báo (chỉ người tạo hoặc admin)
4. Thống kê (tổng số, đã đọc, chưa đọc)

### B. Xem thông báo (Tất cả user)

1. Danh sách thông báo nhận được (mới nhất lên đầu)
2. Đánh dấu đã đọc (click vào xem chi tiết)
3. Lọc theo loại, trạng thái đọc
4. Notification bell với badge số thông báo mới

### C. Notification Bell (Header)

1. Icon chuông với badge số đỏ khi có thông báo mới
2. Dropdown hiện 5 thông báo gần nhất
3. Click vào để mở chi tiết hoặc xem tất cả

---

## Ưu tiên triển khai

**Phase 1 (Core):**

- Database schema + seed data
- DTO, DAO, BUS cơ bản
- GUI danh sách thông báo (Admin view)

**Phase 2 (CRUD):**

- Form thêm/sửa thông báo
- Phân quyền theo vai trò
- Lọc, tìm kiếm, phân trang

**Phase 3 (User Experience):**

- Notification bell trong header
- Đánh dấu đã đọc/chưa đọc
- Thông báo theo lớp/khối cụ thể

**Phase 4 (Advanced):**

- Tự động tạo thông báo (khi nhập điểm, khen thưởng)
- Export/Print thông báo
- Thống kê chi tiết