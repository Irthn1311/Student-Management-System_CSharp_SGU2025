# 📋 Cập nhật: Quy tắc trạng thái hợp lệ khi phân lớp tự động

## ✅ Trạng thái HỢP LỆ (được phân lớp)

Chỉ có **1 trạng thái duy nhất** được phép tham gia phân lớp tự động:

- **"Đang học"** ✓

## ❌ Trạng thái KHÔNG HỢP LỆ (bị bỏ qua)

Các học sinh có các trạng thái sau sẽ **KHÔNG** được phân lớp tự động:

1. **"Nghỉ học"** ❌
2. **"Đã tốt nghiệp"** ❌
3. **"Bảo lưu"** ❌

---

## 📊 Thông báo hiển thị

### **Bảng thông báo 1: Preview (Xem trước)**

```
📚 Loại phân lớp: HK1 → HK2 (Giữ nguyên lớp)
👥 Tổng số học sinh: 376 (chỉ "Đang học")

⚠️ Có 125 học sinh không hợp lệ (Nghỉ học/Đã tốt nghiệp/Bảo lưu) sẽ bị bỏ qua
   - 99 đã tốt nghiệp
   - 20 nghỉ học
   - 6 bảo lưu

✅ Số học sinh đủ điều kiện: 350
❌ Số học sinh thiếu điểm/hạnh kiểm: 26
```

### **Bảng thông báo 2: Kết quả sau khi phân lớp**

```
✅ Đã phân lớp thành công: 350 học sinh.

⚠️ Có 99 đã tốt nghiệp, 20 nghỉ học, 6 bảo lưu - Không hợp lệ, đã bỏ qua.

❌ Có 26 học sinh gặp lỗi khi xử lý:
1-Nguyễn Văn A: Chưa có điểm HK1, không thể chuyển sang HK2
2-Trần Thị B: Chưa có hạnh kiểm HK1, không thể chuyển sang HK2
...
```

### **Bảng thông báo 3 (nếu học kỳ đã phân lớp rồi)**

```
⚠️ HỌC KỲ TIẾP THEO ĐÃ ĐƯỢC PHÂN LỚP!

📚 Học kỳ hiện tại: Học kỳ II Năm học 2024-2025
📚 Học kỳ tiếp theo: Học kỳ I Năm học 2025-2026
👥 Số học sinh đã phân lớp: 350

🔄 Bạn có muốn XÓA dữ liệu phân lớp cũ và PHÂN LỚP LẠI không?

⚠️ Lưu ý: Tất cả dữ liệu phân lớp của học kỳ tiếp theo sẽ bị xóa!

[Yes] [No]
```

---

## 🔧 Các thay đổi trong code

### 1️⃣ **File: `PhanLopTuDongBLL.cs`**

#### **Hàm `ThucHienPhanLopTuDong()` - Dòng ~242**

**Trước:**

```csharp
List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                                            .Where(hs => hs.TrangThai == "Đang học")
                                            .ToList();
```

**Sau:**

```csharp
// ✅ CHỈ LẤY HỌC SINH CÓ TRẠNG THÁI HỢP LỆ: "Đang học"
// ❌ LOẠI BỎ: "Nghỉ học", "Đã tốt nghiệp", "Bảo lưu"
List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                                            .Where(hs => hs.TrangThai == "Đang học")
                                            .ToList();

Console.WriteLine($"📊 Tổng số học sinh 'Đang học': {danhSachHocSinhDangHoc.Count}");
```

#### **Hàm `ThucHienPhanLopTuDong()` - Phần kết quả (Dòng ~635)**

**Trước:**

```csharp
string finalMessage = $"{(isChuyenSangHK2 ? "HK1→HK2" : "HK2→HK1 Năm sau")}. ";
finalMessage += $"Đã phân lớp thành công: {soHocSinhDaPhanLop} học sinh.";

// Thêm thông tin số học sinh "Đã tốt nghiệp"
var allPhanLopHK = phanLopBLL.GetAllPhanLop().Where(p => p.maHocKy == maHocKyHienTai).ToList();
int soHocSinhTotNghiep = hocSinhBLL.GetAllHocSinh()
                                    .Where(hs => hs.TrangThai == "Đã tốt nghiệp" &&
                                                 allPhanLopHK.Any(p => p.maHocSinh == hs.MaHS))
                                    .Count();
if (soHocSinhTotNghiep > 0)
{
    finalMessage += $"\r\n\r\n⚠️ Có {soHocSinhTotNghiep} học sinh đã tốt nghiệp, không cần phân lớp.";
}
```

**Sau:**

```csharp
string finalMessage = $"{(isChuyenSangHK2 ? "HK1→HK2" : "HK2→HK1 Năm sau")}. ";
finalMessage += $"✅ Đã phân lớp thành công: {soHocSinhDaPhanLop} học sinh.";

// Đếm và thông báo các học sinh có trạng thái KHÔNG HỢP LỆ
var allHocSinh = hocSinhBLL.GetAllHocSinh();
int soHSNghiHoc = allHocSinh.Count(hs => hs.TrangThai == "Nghỉ học");
int soHSTotNghiep = allHocSinh.Count(hs => hs.TrangThai == "Đã tốt nghiệp");
int soHSBaoLuu = allHocSinh.Count(hs => hs.TrangThai == "Bảo lưu");

List<string> thongBaoKhongHopLe = new List<string>();
if (soHSTotNghiep > 0) thongBaoKhongHopLe.Add($"{soHSTotNghiep} đã tốt nghiệp");
if (soHSNghiHoc > 0) thongBaoKhongHopLe.Add($"{soHSNghiHoc} nghỉ học");
if (soHSBaoLuu > 0) thongBaoKhongHopLe.Add($"{soHSBaoLuu} bảo lưu");

if (thongBaoKhongHopLe.Count > 0)
{
    finalMessage += $"\r\n\r\n⚠️ Có {string.Join(", ", thongBaoKhongHopLe)} - Không hợp lệ, đã bỏ qua.";
}
```

#### **Hàm `TaoPreviewPhanLop()` - Dòng ~790**

**Trước:**

```csharp
// LẤY ĐÚNG SỐ HỌC SINH SẼ ĐƯỢC XỬ LÝ: CHỈ "Đang học"
List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                                        .Where(hs => hs.TrangThai == "Đang học")
                                        .ToList();
preview["TongSoHocSinh"] = danhSachHocSinhDangHoc.Count;
```

**Sau:**

```csharp
// ✅ CHỈ LẤY HỌC SINH CÓ TRẠNG THÁI HỢP LỆ: "Đang học"
// ❌ LOẠI BỎ: "Nghỉ học", "Đã tốt nghiệp", "Bảo lưu"
List<HocSinhDTO> danhSachHocSinhDangHoc = hocSinhBLL.GetAllHocSinh()
                                        .Where(hs => hs.TrangThai == "Đang học")
                                        .ToList();
preview["TongSoHocSinh"] = danhSachHocSinhDangHoc.Count;

// Đếm số học sinh có trạng thái KHÔNG HỢP LỆ (để hiển thị cảnh báo)
var allHocSinh = hocSinhBLL.GetAllHocSinh();
int soHocSinhKhongHopLe = allHocSinh.Count(hs =>
    hs.TrangThai == "Nghỉ học" ||
    hs.TrangThai == "Đã tốt nghiệp" ||
    hs.TrangThai == "Bảo lưu"
);

if (soHocSinhKhongHopLe > 0)
{
    preview["SoHSKhongHopLe"] = soHocSinhKhongHopLe;
    preview["ThongBaoKhongHopLe"] = $"⚠️ Có {soHocSinhKhongHopLe} học sinh không hợp lệ (Nghỉ học/Đã tốt nghiệp/Bảo lưu) sẽ bị bỏ qua";
}
```

---

## 🎯 Kết quả

### ✅ Điều đã làm được:

1. **Lọc chính xác**: Chỉ học sinh "Đang học" mới được phân lớp
2. **Thông báo rõ ràng**: Hiển thị số lượng từng loại trạng thái không hợp lệ
3. **Preview chính xác**: Đếm đúng số học sinh sẽ được xử lý
4. **Tách biệt lỗi xử lý vs không hợp lệ**:
   - Học sinh không hợp lệ (trạng thái) → Thông báo cảnh báo
   - Học sinh gặp lỗi (thiếu điểm/hạnh kiểm) → Danh sách lỗi chi tiết

### 📌 Lưu ý quan trọng:

- **"Đã tốt nghiệp"** sẽ bị bỏ qua NGAY TỪ ĐẦU (không vào logic xử lý)
- Khác với trước: Học sinh lớp 12 đủ điều kiện mới được đổi sang "Đã tốt nghiệp" TRONG quá trình phân lớp
- Bây giờ: Nếu đã có sẵn trạng thái "Đã tốt nghiệp" → Không xử lý gì cả

---

## ✅ Hoàn tất!

Bây giờ hệ thống sẽ:

- ✅ Chỉ phân lớp học sinh "Đang học"
- ❌ Bỏ qua "Nghỉ học", "Đã tốt nghiệp", "Bảo lưu"
- 📊 Hiển thị rõ ràng số lượng từng loại
- 💬 Thông báo chi tiết trong cả preview và kết quả
