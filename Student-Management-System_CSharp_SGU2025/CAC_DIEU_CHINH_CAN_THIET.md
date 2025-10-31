# CÁC ĐIỀU CHỈNH CẦN THIẾT ĐỂ HOÀN THIỆN PHÂN LỚP TỰ ĐỘNG

## ✅ ĐÃ HOÀN THÀNH

### 1. Thêm method `LayDiemTrungBinhMonTheoHocKy()` vào DiemSoDAO.cs
```csharp
public Dictionary<int, float?> LayDiemTrungBinhMonTheoHocKy(int maHocSinh, int maHocKy)
```
- ✅ Đã thêm vào `DiemSoDAO.cs` (dòng 384+)
- ✅ Đã thêm wrapper vào `NhapDiemBUS.cs`

### 2. File PhanLopTuDongBLL.cs
- ✅ Đã có sẵn code đầy đủ với logic HK I → HK II và HK II → HK I năm sau
- ✅ Sử dụng `NhapDiemBUS` (đã đổi từ `DiemSoBUS` không tồn tại)

---

## ⚠️ CẦN BỔ SUNG (TÙY CHỌN)

### 1. Thêm method vào HocKyBUS.cs

Nếu muốn **tự động tạo năm học mới** khi phân lớp HK II → HK I năm sau, cần thêm:

```csharp
// File: d:\C#\QLHS\Student-Management-System_CSharp_SGU2025\bus\HocKyBUS.cs

/// <summary>
/// Lấy thông tin năm học theo mã
/// </summary>
public NamHocDTO LayNamHocTheoMa(string maNamHoc)
{
    try
    {
        // Gọi hàm từ DAO (cần tạo thêm trong NamHocDAO hoặc HocKyDAO)
        return hocKyDAO.LayNamHocTheoMa(maNamHoc);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi BUS LayNamHocTheoMa: {ex.Message}");
        throw;
    }
}

/// <summary>
/// Thêm năm học mới
/// </summary>
public bool ThemNamHoc(string maNamHoc, string tenNamHoc, DateTime ngayBD, DateTime ngayKT)
{
    try
    {
        // Tạo DTO
        NamHocDTO namHoc = new NamHocDTO
        {
            MaNamHoc = maNamHoc,
            TenNamHoc = tenNamHoc,
            NgayBatDau = ngayBD,
            NgayKetThuc = ngayKT
        };

        // Gọi hàm từ DAO (cần tạo thêm trong NamHocDAO hoặc HocKyDAO)
        return hocKyDAO.ThemNamHoc(namHoc);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Lỗi BUS ThemNamHoc: {ex.Message}");
        throw;
    }
}
```

**LƯU Ý:** Nếu không muốn tự động tạo năm học, có thể **bỏ qua** phần này và **thêm năm học thủ công** qua GUI trước khi phân lớp.

---

### 2. Thêm DTO NamHocDTO (nếu chưa có)

Kiểm tra xem file `NamHocDTO.cs` đã tồn tại chưa. Nếu chưa có, tạo mới:

```csharp
// File: d:\C#\QLHS\Student-Management-System_CSharp_SGU2025\DTO\NamHocDTO.cs

using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class NamHocDTO
    {
        public string MaNamHoc { get; set; }
        public string TenNamHoc { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}
```

---

### 3. Thêm method vào HocKyDAO.cs (DAO Layer)

```csharp
// File: d:\C#\QLHS\Student-Management-System_CSharp_SGU2025\dao\HocKyDAO.cs

using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;

/// <summary>
/// Lấy thông tin năm học theo mã
/// </summary>
public NamHocDTO LayNamHocTheoMa(string maNamHoc)
{
    NamHocDTO namHoc = null;
    MySqlConnection conn = null;
    try
    {
        conn = ConnectionDatabase.GetConnection();
        conn.Open();
        
        string query = "SELECT MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc FROM NamHoc WHERE MaNamHoc = @MaNamHoc";
        
        using (MySqlCommand cmd = new MySqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
            
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    namHoc = new NamHocDTO
                    {
                        MaNamHoc = reader["MaNamHoc"].ToString(),
                        TenNamHoc = reader["TenNamHoc"].ToString(),
                        NgayBatDau = Convert.ToDateTime(reader["NgayBatDau"]),
                        NgayKetThuc = Convert.ToDateTime(reader["NgayKetThuc"])
                    };
                }
            }
        }
    }
    catch (Exception ex)
    {
        throw new Exception("Lỗi khi lấy năm học: " + ex.Message);
    }
    finally
    {
        ConnectionDatabase.CloseConnection(conn);
    }
    
    return namHoc;
}

/// <summary>
/// Thêm năm học mới
/// </summary>
public bool ThemNamHoc(NamHocDTO namHoc)
{
    MySqlConnection conn = null;
    try
    {
        conn = ConnectionDatabase.GetConnection();
        conn.Open();
        
        string query = @"INSERT INTO NamHoc (MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc) 
                        VALUES (@MaNamHoc, @TenNamHoc, @NgayBatDau, @NgayKetThuc)";
        
        using (MySqlCommand cmd = new MySqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@MaNamHoc", namHoc.MaNamHoc);
            cmd.Parameters.AddWithValue("@TenNamHoc", namHoc.TenNamHoc);
            cmd.Parameters.AddWithValue("@NgayBatDau", namHoc.NgayBatDau);
            cmd.Parameters.AddWithValue("@NgayKetThuc", namHoc.NgayKetThuc);
            
            return cmd.ExecuteNonQuery() > 0;
        }
    }
    catch (Exception ex)
    {
        throw new Exception("Lỗi khi thêm năm học: " + ex.Message);
    }
    finally
    {
        ConnectionDatabase.CloseConnection(conn);
    }
}
```

---

## 🔧 CÁCH SỬ DỤNG

### Option 1: Test nhanh KHÔNG tự động tạo năm học

1. **Thêm năm học thủ công** qua GUI hoặc SQL:
   ```sql
   INSERT INTO NamHoc (MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc) 
   VALUES ('2026-2027', 'Năm học 2026-2027', '2026-09-01', '2027-05-31');
   
   INSERT INTO HocKy (TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT) VALUES
   ('Học kỳ I', '2026-2027', 'Chưa bắt đầu', '2026-09-01', '2027-01-15'),
   ('Học kỳ II', '2026-2027', 'Chưa bắt đầu', '2027-01-16', '2027-05-31');
   ```

2. **Comment code tự động tạo năm học** trong `PhanLopTuDongBLL.cs` (dòng ~196-210):
   ```csharp
   if (hocKyTiepTheo == null)
   {
       // Comment dòng này nếu không muốn tự động tạo năm học
       // if (!hocKyHienTai.TenHocKy.ToLower().Contains("i"))
       // {
       //     var taoHK = TaoHocKyMoi(maHocKyHienTai);
       //     ...
       // }
       
       // Thay bằng:
       return (false, "Chưa có học kỳ tiếp theo. Vui lòng tạo năm học mới trước.", 0);
   }
   ```

### Option 2: Sử dụng đầy đủ tính năng tự động tạo năm học

1. Thêm code ở mục **"CẦN BỔ SUNG"** vào các file tương ứng
2. Test phân lớp từ HK II → HK I năm sau
3. Hệ thống sẽ tự động tạo năm học mới nếu chưa có

---

## 📋 CHECKLIST TRƯỚC KHI TEST

- [x] DiemSoDAO.LayDiemTrungBinhMonTheoHocKy() đã được thêm
- [x] NhapDiemBUS.LayDiemTrungBinhMonTheoHocKy() đã được thêm
- [x] PhanLopBLL.GetHocSinhChuaPhanLop() đã tồn tại
- [x] PhanLopBLL.LayHocSinhTheoHocKy() đã tồn tại
- [x] HocKyBUS.LayDanhSachHocKyTheoNamHoc() đã tồn tại
- [ ] (Tùy chọn) HocKyBUS.LayNamHocTheoMa() được thêm
- [ ] (Tùy chọn) HocKyBUS.ThemNamHoc() được thêm
- [ ] (Tùy chọn) HocKyDAO.LayNamHocTheoMa() được thêm
- [ ] (Tùy chọn) HocKyDAO.ThemNamHoc() được thêm
- [ ] (Tùy chọn) NamHocDTO.cs được tạo

---

## 🎯 HƯỚNG DẪN TEST

### Test HK I → HK II (Giữ nguyên lớp)

1. Import dữ liệu từ `data_DB.sql`
2. Đảm bảo HK II có `TrangThai = 'Chưa bắt đầu'`
3. Trong form PhanLop, chọn **Học kỳ I**
4. Click **"Phân lớp tự động"**
5. Kiểm tra:
   ```sql
   SELECT COUNT(*) FROM PhanLop WHERE MaHocKy = 2; -- Phải = 87
   ```

### Test HK II → HK I năm sau (Xét lên lớp)

**LƯU Ý:** Cần có dữ liệu đầy đủ cho HK I và HK II của năm 2025-2026 trước.

1. **Option A - Tự động tạo năm học:**
   - Implement đầy đủ code ở mục "CẦN BỔ SUNG"
   - Test ngay

2. **Option B - Thủ công:**
   - Tạo năm học 2026-2027 qua SQL (xem Option 1 ở trên)
   - Comment code tự động tạo năm học
   - Test

3. Trong form PhanLop, chọn **Học kỳ II (2025-2026)**
4. Click **"Phân lớp tự động"**
5. Kiểm tra logic lên lớp:
   ```sql
   -- Xem học sinh lên khối 11
   SELECT hs.HoTen, lh.TenLop
   FROM PhanLop pl
   JOIN HocSinh hs ON pl.MaHocSinh = hs.MaHocSinh
   JOIN LopHoc lh ON pl.MaLop = lh.MaLop
   WHERE pl.MaHocKy = 3 AND lh.MaKhoi = 11;
   ```

---

## 🚨 XỬ LÝ LỖI THƯỜNG GẶP

### Lỗi: "'NamHocDTO' could not be found"
- **Nguyên nhân:** Chưa tạo file NamHocDTO.cs
- **Giải pháp:** Tạo file theo mục "CẦN BỔ SUNG" → Mục 2

### Lỗi: "Không tìm thấy học kỳ tiếp theo"
- **Nguyên nhân:** Chưa có năm học mới trong database
- **Giải pháp:** 
  - Option A: Implement code tự động tạo
  - Option B: Thêm thủ công qua SQL

### Lỗi: "0 học sinh được phân lớp"
- **Nguyên nhân:** Thiếu dữ liệu DiemSo hoặc HanhKiem
- **Giải pháp:** Kiểm tra:
  ```sql
  -- Kiểm tra điểm
  SELECT COUNT(*) FROM DiemSo WHERE MaHocKy = 1;
  
  -- Kiểm tra hạnh kiểm
  SELECT COUNT(*) FROM HanhKiem WHERE MaHocKy = 1;
  ```

---

**Tạo bởi:** GitHub Copilot  
**Ngày:** 2025-10-30  
**Phiên bản:** 1.0
