# HƯỚNG DẪN TEST PHÂN LỚP TỰ ĐỘNG

## 📋 Tình trạng hiện tại

Hệ thống đã có:

- ✅ 90 học sinh (87 "Đang học", 3 "Nghỉ học/Bảo lưu")
- ✅ 9 lớp (3 khối × 3 lớp/khối)
- ✅ Điểm số đầy đủ (DiemSo)
- ✅ Hạnh kiểm (HanhKiem)
- ✅ Xếp loại (XepLoai)
- ✅ 2 học kỳ:
  - **HK I (MaHocKy=1)**: TrangThai = **"Đang diễn ra"**
  - **HK II (MaHocKy=2)**: TrangThai = **"Chưa bắt đầu"**

---

## 🎯 CÁCH 1: Test với điều kiện CHUẨN (Khuyến nghị)

### Bước 1: Kiểm tra trạng thái học kỳ trong database

Chạy query SQL sau để xác nhận:

```sql
SELECT * FROM HocKy;
```

**Kết quả mong đợi:**
| MaHocKy | TenHocKy | MaNamHoc | TrangThai | NgayBD | NgayKT |
|---------|----------|----------|-----------|--------|--------|
| 1 | Học kỳ I | 2025-2026 | Đang diễn ra | 2025-09-01 | 2026-01-15 |
| 2 | Học kỳ II | 2025-2026 | **Chưa bắt đầu** | 2026-01-16 | 2026-05-31 |

### Bước 2: Test phân lớp trong ứng dụng C#

1. Mở form **PhanLop**
2. Chọn **Học kỳ I (2025-2026)** từ ComboBox
3. Click nút **"Phân lớp tự động"** (hoặc `btnThemPhanLop`)
4. Hệ thống sẽ:
   - ✅ Kiểm tra HK II có trạng thái "Chưa bắt đầu" → **PASS**
   - ✅ Hiển thị preview kết quả
   - ✅ Khi confirm → Phân ~87 học sinh từ HK I → HK II (giữ nguyên lớp)

### Bước 3: Xác minh kết quả

Chạy query sau để kiểm tra:

```sql
-- Kiểm tra số lượng học sinh đã được phân lớp HK II
SELECT COUNT(*) AS SoHocSinhHK2
FROM PhanLop
WHERE MaHocKy = 2;

-- Xem chi tiết phân lớp HK II
SELECT
    pl.MaHocSinh,
    hs.HoTen,
    lh.TenLop,
    hk.TenHocKy
FROM PhanLop pl
JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
JOIN LopHoc lh ON pl.MaLop = lh.MaLop
JOIN HocKy hk ON pl.MaHocKy = hk.MaHocKy
WHERE pl.MaHocKy = 2
ORDER BY lh.TenLop, hs.HoTen;
```

---

## 🧪 CÁCH 2: Test MODE (Bỏ qua kiểm tra điều kiện)

### Khi nào dùng:

- ✅ Môi trường development/testing
- ✅ Muốn test nhanh logic phân lớp mà không cần chỉnh database
- ✅ HK II không ở trạng thái "Chưa bắt đầu"

### Cách sử dụng:

#### Option A: Sửa code form PhanLop.cs (tạm thời)

Tìm dòng 167 trong file `PhanLop.cs`:

```csharp
// CŨ (Line 167)
var ketQua = phanLopTuDongBLL.ThucHienPhanLopTuDong(maHocKyHienTai);

// MỚI - Thay bằng:
var ketQua = phanLopTuDongBLL.ThucHienPhanLopTuDong_TEST(maHocKyHienTai);
```

**Lưu ý:** Sau khi test xong, nhớ đổi lại về `ThucHienPhanLopTuDong()` (không có `_TEST`)!

#### Option B: Thêm button riêng cho TEST mode

Thêm button vào form với code:

```csharp
private void btnTestPhanLop_Click(object sender, EventArgs e)
{
    if (cbHocKyNamHoc.SelectedIndex <= 0)
    {
        MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    int maHocKyHienTai = danhSachHocKy[cbHocKyNamHoc.SelectedIndex - 1].MaHocKy;

    var result = MessageBox.Show(
        $"[TEST MODE] Bạn có chắc muốn thực hiện phân lớp TỰ ĐỘNG?\n\n" +
        $"⚠️ Chức năng này BỎ QUA kiểm tra điều kiện!\n" +
        $"Chỉ dùng cho testing/development.\n\n" +
        $"Tiếp tục?",
        "Xác nhận phân lớp TEST",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

    if (result == DialogResult.Yes)
    {
        var ketQua = phanLopTuDongBLL.ThucHienPhanLopTuDong_TEST(maHocKyHienTai);

        if (ketQua.success)
        {
            MessageBox.Show(
                ketQua.message,
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            LoadData();
        }
        else
        {
            MessageBox.Show(
                $"Lỗi: {ketQua.message}",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
```

---

## 🔍 Kiểm tra logic phân lớp

### HK I → HK II (Giữ nguyên lớp)

Query kiểm tra học sinh có giữ nguyên lớp không:

```sql
SELECT
    hs.MaHocSinh,
    hs.HoTen,
    lh1.TenLop AS LopHK1,
    lh2.TenLop AS LopHK2,
    CASE
        WHEN lh1.TenLop = lh2.TenLop THEN '✅ Đúng'
        ELSE '❌ Sai - Không giữ nguyên lớp!'
    END AS KiemTra
FROM HocSinh hs
JOIN PhanLop pl1 ON hs.MaHocSinh = pl1.MaHocSinh AND pl1.MaHocKy = 1
JOIN LopHoc lh1 ON pl1.MaLop = lh1.MaLop
JOIN PhanLop pl2 ON hs.MaHocSinh = pl2.MaHocSinh AND pl2.MaHocKy = 2
JOIN LopHoc lh2 ON pl2.MaLop = lh2.MaLop
WHERE hs.TrangThai = 'Đang học'
ORDER BY hs.MaHocSinh;
```

### HK II → HK I năm sau (Xét lên lớp)

Query kiểm tra học sinh có lên lớp đúng theo xếp loại không:

```sql
SELECT
    hs.MaHocSinh,
    hs.HoTen,
    lh1.TenLop AS LopHK2,
    xl.HocLuc AS XepLoaiHK2,
    lh2.TenLop AS LopHK1NamSau,
    CASE
        WHEN xl.HocLuc IN ('Giỏi', 'Khá', 'Trung bình') AND
             SUBSTRING(lh2.TenLop, 1, 2) = CAST(CAST(SUBSTRING(lh1.TenLop, 1, 2) AS INT) + 1 AS CHAR(2))
        THEN '✅ Lên lớp đúng'
        WHEN xl.HocLuc IN ('Yếu', 'Kém') AND lh1.TenLop = lh2.TenLop
        THEN '✅ Ở lại lớp đúng'
        ELSE '❌ Sai logic!'
    END AS KiemTra
FROM HocSinh hs
JOIN PhanLop pl1 ON hs.MaHocSinh = pl1.MaHocSinh AND pl1.MaHocKy = 2
JOIN LopHoc lh1 ON pl1.MaLop = lh1.MaLop
JOIN XepLoai xl ON hs.MaHocSinh = xl.MaHocSinh AND xl.MaHocKy = 2
LEFT JOIN PhanLop pl2 ON hs.MaHocSinh = pl2.MaHocSinh AND pl2.MaHocKy = 3
LEFT JOIN LopHoc lh2 ON pl2.MaLop = lh2.MaLop
WHERE hs.TrangThai = 'Đang học'
ORDER BY hs.MaHocSinh;
```

---

## ❗ Troubleshooting

### Lỗi: "Học kỳ tiếp theo chưa ở trạng thái 'Chưa bắt đầu'"

**Nguyên nhân:** Database chưa được import lại sau khi sửa `data_DB.sql`

**Giải pháp:**

```sql
-- Cách 1: Update trực tiếp
UPDATE HocKy
SET TrangThai = 'Chưa bắt đầu'
WHERE MaHocKy = 2;

-- Cách 2: Import lại toàn bộ data_DB.sql
SOURCE d:\C#\QLHS\Student-Management-System_CSharp_SGU2025\ConnectDatabase\data_DB.sql;
```

### Lỗi: "Không tìm thấy học kỳ tiếp theo"

**Nguyên nhân:** Thiếu dữ liệu HK II trong database

**Giải pháp:**

```sql
-- Kiểm tra xem HK II có tồn tại không
SELECT * FROM HocKy WHERE MaHocKy = 2;

-- Nếu không có, thêm thủ công:
INSERT INTO HocKy (MaHocKy, TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT)
VALUES (2, 'Học kỳ II', '2025-2026', 'Chưa bắt đầu', '2026-01-16', '2026-05-31');
```

### Lỗi: "0 học sinh được phân lớp"

**Nguyên nhân:** Không có học sinh "Đang học" trong HK I

**Giải pháp:**

```sql
-- Kiểm tra số lượng học sinh đang học trong HK I
SELECT COUNT(*)
FROM PhanLop pl
JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
WHERE pl.MaHocKy = 1 AND hs.TrangThai = 'Đang học';

-- Nếu = 0, import lại data_DB.sql
```

---

## 📊 Kết quả mong đợi

Sau khi phân lớp HK I → HK II thành công:

- ✅ **87 học sinh** được phân lớp vào HK II
- ✅ **Giữ nguyên lớp** (10A1 HK I → 10A1 HK II)
- ✅ **Không phân lớp** 3 học sinh "Nghỉ học"/"Bảo lưu"

Thống kê theo lớp:

| Lớp       | HK I   | HK II  | Ghi chú                     |
| --------- | ------ | ------ | --------------------------- |
| 10A1      | 8      | 8      | 2 HS bị loại (Nghỉ/Bảo lưu) |
| 10A2      | 9      | 9      | 1 HS nghỉ học               |
| 10A3      | 10     | 10     | Đủ                          |
| 11A1-11A3 | 30     | 30     | Đủ                          |
| 12A1-12A3 | 30     | 30     | Đủ                          |
| **Tổng**  | **87** | **87** |                             |

---

## 🎓 Ghi chú

1. **Sau khi test xong với `_TEST` mode**, nhớ xóa/comment code test để không ảnh hưởng production
2. **Backup database** trước khi test để có thể rollback
3. **Check logs** nếu có lỗi không mong muốn

---

**Tạo bởi:** GitHub Copilot  
**Ngày:** 2025-10-29  
**Version:** 1.0
