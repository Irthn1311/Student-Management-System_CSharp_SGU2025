# PHÂN TÍCH CHI TIẾT PHẦN PHÂN LỚP

## 1. TỔNG QUAN

### 1.1. Mục đích

Phần **Phân lớp** quản lý việc gán học sinh vào các lớp học trong từng học kỳ, hỗ trợ cả phân lớp tự động và thủ công.

### 1.2. Kiến trúc

- **GUI Layer**: `GUI/GUIClass/HocSinh/PhanLop.cs` - Form giao diện người dùng
- **Business Logic Layer**:
  - `BUS/BUSClass/PhanLopBLL.cs` - Logic nghiệp vụ cơ bản
  - `BUS/BUSClass/PhanLopTuDongBLL.cs` - Thuật toán phân lớp tự động
- **Data Access Layer**: `DAO/DAOClass/PhanLopDAO.cs` - Truy cập cơ sở dữ liệu

### 1.3. Bảng dữ liệu

- **PhanLop**: Lưu thông tin phân lớp (MaHocSinh, MaLop, MaHocKy)

---

## 2. 4 NÚT QUAN TRỌNG TRONG GIAO DIỆN PHÂN LỚP

Giao diện phân lớp có **4 nút chức năng chính**:

1. **Nút Thêm học sinh thủ công** (`btnThemHocSinh`) - Mở form thêm học sinh từng người một
2. **Nút Nhập Excel** (`btnNhapExcel`) - Nhập nhiều học sinh từ file Excel (tuyển sinh)
3. **Nút Phân lớp chuyển trường** (`btnPhanLopChuyenTruong`) - Nhập học sinh chuyển trường từ Excel và tự động phân lớp
4. **Nút Phân lớp tự động** (`btnThemPhanLop`) - Tự động phân tất cả học sinh vào lớp theo thuật toán

---

## 3. CHI TIẾT TỪNG NÚT CHỨC NĂNG

### 3.1. NÚT 1: THÊM HỌC SINH THỦ CÔNG

**Tên nút**: `btnThemHocSinh`  
**Vị trí code**: `GUI/GUIClass/HocSinh/PhanLop.cs` - **Dòng 1148-1233**  
**Method**: `btnThemHocSinh_Click(object sender, EventArgs e)`

#### Mô tả chức năng

Nút này mở form **Thêm hồ sơ học sinh** để thêm học sinh từng người một vào hệ thống. Học sinh được thêm sẽ có trạng thái "Đang học" và sẽ được phân vào lớp 10 khi thực hiện "Phân lớp tự động".

#### Cách hoạt động chi tiết

**Bước 1: Kiểm tra điều kiện** (Dòng 1152-1168)

```csharp
// Kiểm tra đã chọn năm học chưa
string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
if (string.IsNullOrEmpty(selectedNamHoc) || !danhSachNamHoc.ContainsKey(selectedNamHoc))
{
    MessageBox.Show("Vui lòng chọn năm học.", "Thông báo", ...);
    return;
}

// Lấy HK1 và HK2 của năm học được chọn
var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
if (hk1 == null || hk2 == null)
{
    MessageBox.Show($"Năm học {selectedNamHoc} phải có đầy đủ HK1 và HK2!", "Lỗi", ...);
    return;
}
```

**Bước 2: Kiểm tra đã phân lớp tự động chưa** (Dòng 1170-1189)

```csharp
// ✅ Kiểm tra: Nếu đã phân lớp tự động cho cả HK1 và HK2 → không cho thêm học sinh
var allPhanLop = phanLopBLL.GetAllPhanLop();
int soHocSinhDaPhanLopHK1 = allPhanLop.Count(p => p.maHocKy == hk1.MaHocKy);
int soHocSinhDaPhanLopHK2 = allPhanLop.Count(p => p.maHocKy == hk2.MaHocKy);

int tongSoHocSinhDangHoc = hocSinhBus.GetTotalHocSinhDangHoc();
int nguongToiThieu = Math.Max(50, (int)(tongSoHocSinhDangHoc * 0.3));

bool daPhanLopTuDongHK1 = soHocSinhDaPhanLopHK1 >= nguongToiThieu;
bool daPhanLopTuDongHK2 = soHocSinhDaPhanLopHK2 >= nguongToiThieu;

if (daPhanLopTuDongHK1 && daPhanLopTuDongHK2)
{
    MessageBox.Show("⚠️ KHÔNG THỂ THÊM HỌC SINH!\n\n" +
                   $"Năm học {selectedNamHoc} đã được phân lớp tự động.\n\n" +
                   $"Vui lòng chọn năm học khác (chưa phân lớp) để thêm học sinh tuyển sinh.",
                   "Không thể thêm học sinh", ...);
    return;
}
```

**Logic kiểm tra**:

- Tính ngưỡng tối thiểu = Max(50, 30% tổng số học sinh đang học)
- Nếu số học sinh đã phân lớp ≥ ngưỡng → Coi như đã phân lớp tự động
- **Chặn thêm học sinh** nếu đã phân lớp tự động (tránh làm rối dữ liệu)

**Bước 3: Mở form thêm học sinh** (Dòng 1191-1196)

```csharp
// Mở form thêm học sinh
ThemHoSoHocSinh frmThemHocSinh = new ThemHoSoHocSinh();
frmThemHocSinh.StartPosition = FormStartPosition.CenterScreen;

// Hiển thị form dưới dạng Dialog và chờ kết quả
DialogResult result = frmThemHocSinh.ShowDialog(this);
```

**Bước 4: Xử lý kết quả** (Dòng 1198-1226)

```csharp
// Kiểm tra kết quả trả về từ form
if (result == DialogResult.OK)
{
    // Lấy học sinh vừa tạo từ form
    HocSinhDTO newHS = frmThemHocSinh.NewHocSinh;

    if (newHS != null)
    {
        // Refresh lại bảng phân lớp để hiển thị học sinh vừa thêm
        FilterTablePhanLop();

        // Cập nhật trạng thái nút sau khi thêm học sinh
        UpdateButtonStates();

        MessageBox.Show($"✅ Thêm học sinh thành công!\n\n" +
                       $"Học sinh '{newHS.HoTen}' đã được thêm vào hệ thống.\n\n" +
                       $"Học sinh này sẽ được hiển thị trong bảng phân lớp và sẽ được phân vào lớp 10 khi thực hiện 'Phân lớp tự động'.",
                       "Thêm học sinh thành công", ...);
    }
}
```

#### Kết quả

- Học sinh được thêm vào hệ thống với trạng thái "Đang học"
- Học sinh sẽ xuất hiện trong bảng phân lớp (chưa có lớp)
- Khi thực hiện "Phân lớp tự động", học sinh này sẽ được phân vào lớp 10

---

### 3.2. NÚT 2: NHẬP EXCEL (Tuyển sinh)

**Tên nút**: `btnNhapExcel`  
**Vị trí code**: `GUI/GUIClass/HocSinh/PhanLop.cs` - **Dòng 1235-1323**  
**Method**: `btnNhapExcel_Click(object sender, EventArgs e)`

#### Mô tả chức năng

Nút này cho phép nhập **nhiều học sinh** từ file Excel vào hệ thống. File Excel phải có **3 worksheet**: HocSinh, PhuHuynh, MoiQuanHe. Học sinh được nhập sẽ có trạng thái "Đang học" và sẽ được phân vào lớp 10 khi thực hiện "Phân lớp tự động".

#### Cách hoạt động chi tiết

**Bước 1: Kiểm tra điều kiện** (Dòng 1239-1283)

```csharp
// Kiểm tra đã chọn năm học chưa
string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
// ... validate năm học ...

// Kiểm tra trạng thái học kỳ
string trangThaiHK1 = SemesterHelper.GetStatus(hk1.MaHocKy);
string trangThaiHK2 = SemesterHelper.GetStatus(hk2.MaHocKy);
bool hk1DangDienRa = trangThaiHK1 == "Đang diễn ra";
bool hk2DangDienRa = trangThaiHK2 == "Đang diễn ra";
bool coHocKyDangDienRa = hk1DangDienRa || hk2DangDienRa;

// ✅ KIỂM TRA: Nếu HK1 & HK2 "Đang diễn ra" và đã phân lớp → không cho nhập Excel
var allPhanLop = phanLopBLL.GetAllPhanLop();
int soHocSinhDaPhanLopHK1 = allPhanLop.Count(p => p.maHocKy == hk1.MaHocKy);
int soHocSinhDaPhanLopHK2 = allPhanLop.Count(p => p.maHocKy == hk2.MaHocKy);

int tongSoHocSinhDangHoc = hocSinhBus.GetTotalHocSinhDangHoc();
int nguongToiThieu = Math.Max(50, (int)(tongSoHocSinhDangHoc * 0.3));

bool daPhanLopTuDongHK1 = soHocSinhDaPhanLopHK1 >= nguongToiThieu;
bool daPhanLopTuDongHK2 = soHocSinhDaPhanLopHK2 >= nguongToiThieu;

if (coHocKyDangDienRa && (daPhanLopTuDongHK1 || daPhanLopTuDongHK2))
{
    MessageBox.Show("⚠️ KHÔNG THỂ NHẬP EXCEL!\n\n" +
                   $"Năm học {selectedNamHoc} đang diễn ra và đã được phân lớp tự động.\n\n" +
                   $"Vui lòng chọn năm học khác (chưa phân lớp) để nhập Excel.",
                   "Không thể nhập Excel", ...);
    return;
}
```

**Bước 2: Chọn file Excel** (Dòng 1285-1297)

```csharp
// Mở file dialog để chọn file Excel
OpenFileDialog openFileDialog = new OpenFileDialog
{
    Filter = "Excel Files|*.xlsx;*.xls",
    Title = "Chọn file Excel để nhập học sinh tuyển sinh (HocSinh, PhuHuynh, MoiQuanHe)"
};

if (openFileDialog.ShowDialog() != DialogResult.OK)
{
    return; // Người dùng hủy
}

string filePath = openFileDialog.FileName;
```

**Bước 3: Gọi hàm nhập Excel** (Dòng 1303-1307)

```csharp
// Lấy học kỳ để nhập (ưu tiên HK1, nếu không có thì HK2)
var hocKyDeNhap = hk1DangDienRa ? hk1 : (hk2DangDienRa ? hk2 : hk1);

// Gọi hàm nhập Excel cho học sinh tuyển sinh
ImportExcelTuyenSinh(filePath, hocKyDeNhap, hk1, hk2);
```

**Vị trí hàm ImportExcelTuyenSinh**: **Dòng 4260-4338**

#### Chi tiết hàm ImportExcelTuyenSinh

**Bước 1: Đọc file Excel** (Dòng 4271-4282)

```csharp
using (var package = new ExcelPackage(new FileInfo(filePath)))
{
    // Kiểm tra xem file có ít nhất 3 worksheet không
    if (package.Workbook.Worksheets.Count < 3)
    {
        throw new Exception("File Excel phải có ít nhất 3 worksheet: HocSinh, PhuHuynh, MoiQuanHe");
    }

    // Đọc từng worksheet
    var wsHocSinh = package.Workbook.Worksheets["HocSinh"] ?? package.Workbook.Worksheets[0];
    var wsPhuHuynh = package.Workbook.Worksheets["PhuHuynh"] ?? package.Workbook.Worksheets[1];
    var wsMoiQuanHe = package.Workbook.Worksheets["MoiQuanHe"] ?? package.Workbook.Worksheets[2];
}
```

**Bước 2: Nhập Học Sinh** (Dòng 4284-4293)

```csharp
// 1. Nhập Học Sinh với trạng thái "Đang học"
Dictionary<string, (int maHS, int excelRow)> hocSinhThanhCong =
    ImportHocSinhFromWorksheetTuyenSinh(wsHocSinh, hocSinhDaThem);

if (hocSinhThanhCong.Count == 0)
{
    MessageBox.Show("Không có học sinh nào được nhập thành công. Vui lòng kiểm tra lại dữ liệu Excel.",
        "Thông báo", ...);
    return;
}
```

**Vị trí hàm ImportHocSinhFromWorksheetTuyenSinh**: **Dòng 4384-4500**

**Bước 3: Nhập Phụ Huynh** (Dòng 4295-4307)

```csharp
// 2. Nhập Phụ Huynh của học sinh đã nhập thành công
// ✅ Nếu có lỗi, sẽ rollback học sinh
Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong =
    ImportPhuHuynhFromWorksheetTuyenSinh(wsPhuHuynh, hocSinhThanhCong, hocSinhDaThem, phuHuynhDaThem);

// ✅ Kiểm tra: Nếu sau khi nhập phụ huynh, không còn học sinh nào thì rollback
if (hocSinhThanhCong.Count == 0)
{
    RollbackTuyenSinh(hocSinhDaThem, phuHuynhDaThem);
    MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập phụ huynh. Đã rollback toàn bộ dữ liệu.",
        "Thông báo", ...);
    return;
}
```

**Bước 4: Nhập Mối Quan Hệ** (Dòng 4309-4320)

```csharp
// 3. Nhập Mối Quan Hệ của học sinh đã nhập thành công
// ✅ Nếu có lỗi, sẽ rollback học sinh và phụ huynh
ImportMoiQuanHeFromWorksheetTuyenSinh(wsMoiQuanHe, hocSinhThanhCong, phuHuynhThanhCong, hocSinhDaThem, phuHuynhDaThem);

// ✅ Kiểm tra: Nếu sau khi nhập mối quan hệ, không còn học sinh nào thì rollback
if (hocSinhThanhCong.Count == 0)
{
    RollbackTuyenSinh(hocSinhDaThem, phuHuynhDaThem);
    MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập mối quan hệ. Đã rollback toàn bộ dữ liệu.",
        "Thông báo", ...);
    return;
}
```

**Bước 5: Rollback nếu có lỗi** (Dòng 4332-4337)

```csharp
catch (Exception ex)
{
    // ✅ ROLLBACK TOÀN BỘ nếu có bất kỳ lỗi nào
    RollbackTuyenSinh(hocSinhDaThem, phuHuynhDaThem);
    throw new Exception($"Lỗi khi nhập Excel: {ex.Message}\n\nĐã rollback toàn bộ dữ liệu.", ex);
}
```

**Vị trí hàm RollbackTuyenSinh**: **Dòng 4343-4379**

#### Đặc điểm quan trọng

- **Transaction**: Tất cả hoặc không có gì - Nếu có lỗi ở bất kỳ bước nào, rollback toàn bộ
- **File Excel yêu cầu**: 3 worksheet (HocSinh, PhuHuynh, MoiQuanHe)
- **Trạng thái học sinh**: "Đang học" (không phải "Đang học(CT)")
- **Không tự động phân lớp**: Học sinh sẽ được phân lớp khi thực hiện "Phân lớp tự động"

---

### 3.3. NÚT 3: PHÂN LỚP CHUYỂN TRƯỜNG

**Tên nút**: `btnPhanLopChuyenTruong`  
**Vị trí code**: `GUI/GUIClass/HocSinh/PhanLop.cs` - **Dòng 1325-1438**  
**Method**: `btnPhanLopChuyenTruong_Click(object sender, EventArgs e)`

#### Mô tả chức năng

Nút này nhập học sinh **chuyển trường** từ file Excel và **tự động phân lớp** cho học sinh đó. File Excel phải có **6 worksheet**: HocSinh, PhuHuynh, MoiQuanHe, Diem, HanhKiem, XepLoai. Học sinh được nhập sẽ có trạng thái "Đang học(CT)" và được phân lớp ngay lập tức (ưu tiên nguyện vọng).

#### Cách hoạt động chi tiết

**Bước 1: Kiểm tra điều kiện** (Dòng 1329-1398)

```csharp
// Kiểm tra đã chọn năm học chưa
string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
// ... validate năm học ...

// Kiểm tra trạng thái học kỳ
string trangThaiHK1 = SemesterHelper.GetStatus(hk1.MaHocKy);
string trangThaiHK2 = SemesterHelper.GetStatus(hk2.MaHocKy);

if (trangThaiHK1 != "Đang diễn ra" && trangThaiHK2 != "Đang diễn ra")
{
    MessageBox.Show($"Cả HK1 và HK2 của năm học {selectedNamHoc} đều không phải 'Đang diễn ra'.\n\nVui lòng kiểm tra lại cấu hình học kỳ.",
        "Lỗi", ...);
    return;
}

// ✅ KIỂM TRA: Cả HK1 và HK2 PHẢI đã được phân lớp tự động rồi mới cho phép phân lớp chuyển trường
var allPhanLop = phanLopBLL.GetAllPhanLop();
int soHocSinhDaPhanLopHK1 = allPhanLop.Count(p => p.maHocKy == hk1.MaHocKy);
int soHocSinhDaPhanLopHK2 = allPhanLop.Count(p => p.maHocKy == hk2.MaHocKy);

int tongSoHocSinhDangHoc = hocSinhBus.GetTotalHocSinhDangHoc();
int nguongToiThieu = Math.Max(50, (int)(tongSoHocSinhDangHoc * 0.3));

bool daPhanLopTuDongHK1 = soHocSinhDaPhanLopHK1 >= nguongToiThieu;
bool daPhanLopTuDongHK2 = soHocSinhDaPhanLopHK2 >= nguongToiThieu;

if (!daPhanLopTuDongHK1 || !daPhanLopTuDongHK2)
{
    string thongBao = $"⚠️ CHƯA THỂ PHÂN LỚP CHUYỂN TRƯỜNG!\n\n";
    thongBao += $"Năm học: {selectedNamHoc}\n\n";
    thongBao += $"HK1 ({hk1.TenHocKy}): {soHocSinhDaPhanLopHK1} học sinh (Ngưỡng: {nguongToiThieu})\n";
    thongBao += $"HK2 ({hk2.TenHocKy}): {soHocSinhDaPhanLopHK2} học sinh (Ngưỡng: {nguongToiThieu})\n\n";

    if (!daPhanLopTuDongHK1 && !daPhanLopTuDongHK2)
    {
        thongBao += "❌ Cả HK1 và HK2 đều chưa được phân lớp tự động!\n\n";
    }
    else if (!daPhanLopTuDongHK1)
    {
        thongBao += "❌ HK1 chưa được phân lớp tự động!\n\n";
    }
    else
    {
        thongBao += "❌ HK2 chưa được phân lớp tự động!\n\n";
    }

    thongBao += "Vui lòng thực hiện 'Phân lớp tự động' cho cả HK1 và HK2 trước khi phân lớp chuyển trường.";

    MessageBox.Show(thongBao, "Chưa thể phân lớp chuyển trường", ...);
    return; // CHẶN NGAY, KHÔNG CHO PHÂN LỚP CHUYỂN TRƯỜNG
}
```

**Điều kiện bắt buộc**:

- Phải có học kỳ "Đang diễn ra"
- **Phải đã phân lớp tự động** cho cả HK1 và HK2 (đạt ngưỡng)
- Mục đích: Đảm bảo đã có phân lớp tự động, không phải chỉ 1-2 học sinh chuyển trường

**Bước 2: Chọn file Excel** (Dòng 1400-1415)

```csharp
// Lấy học kỳ đang diễn ra để nhập Excel (ưu tiên HK1, nếu không có thì HK2)
var hocKyHienTai = trangThaiHK1 == "Đang diễn ra" ? hk1 : hk2;

// Mở file dialog để chọn file Excel
OpenFileDialog openFileDialog = new OpenFileDialog
{
    Filter = "Excel Files|*.xlsx;*.xls",
    Title = "Chọn file Excel để nhập dữ liệu phân lớp chuyển trường"
};

if (openFileDialog.ShowDialog() != DialogResult.OK)
{
    return; // Người dùng hủy
}

string filePath = openFileDialog.FileName;
```

**Bước 3: Gọi hàm nhập Excel** (Dòng 1421-1422)

```csharp
// Gọi hàm nhập Excel
ImportExcelPhanLopChuyenTruong(filePath, hocKyHienTai);
```

**Vị trí hàm ImportExcelPhanLopChuyenTruong**: **Dòng 1444-1571**

#### Chi tiết hàm ImportExcelPhanLopChuyenTruong

**Bước 1: Đọc file Excel** (Dòng 1449-1463)

```csharp
using (var package = new ExcelPackage(new FileInfo(filePath)))
{
    // Kiểm tra xem file có ít nhất 6 worksheet không
    if (package.Workbook.Worksheets.Count < 6)
    {
        throw new Exception("File Excel phải có ít nhất 6 worksheet: HocSinh, PhuHuynh, MoiQuanHe, Diem, HanhKiem, XepLoai");
    }

    // Đọc từng worksheet
    var wsHocSinh = package.Workbook.Worksheets["HocSinh"] ?? package.Workbook.Worksheets[0];
    var wsPhuHuynh = package.Workbook.Worksheets["PhuHuynh"] ?? package.Workbook.Worksheets[1];
    var wsMoiQuanHe = package.Workbook.Worksheets["MoiQuanHe"] ?? package.Workbook.Worksheets[2];
    var wsDiem = package.Workbook.Worksheets["Diem"] ?? package.Workbook.Worksheets[3];
    var wsHanhKiem = package.Workbook.Worksheets["HanhKiem"] ?? package.Workbook.Worksheets[4];
    var wsXepLoai = package.Workbook.Worksheets["XepLoai"] ?? package.Workbook.Worksheets[5];
}
```

**Bước 2: Kiểm tra học kỳ trước khi thêm dữ liệu** (Dòng 1465-1479)

```csharp
// ✅ BƯỚC 0: KIỂM TRA HỌC KỲ TRƯỚC KHI THÊM BẤT KỲ DỮ LIỆU NÀO
// Đọc dữ liệu học sinh từ Excel (chưa thêm vào DB) để kiểm tra học kỳ
Dictionary<string, (int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhTuExcel =
    DocDuLieuHocSinhTuExcel(wsHocSinh, hocKyHienTai);

// Lọc ra danh sách học sinh đủ điều kiện (có đủ học kỳ cần thiết)
// Học sinh không đủ điều kiện sẽ bị loại bỏ, KHÔNG được thêm vào DB
HashSet<string> hocSinhDuDieuKien = LocHocSinhDuDieuKien(hocSinhTuExcel, hocKyHienTai);

if (hocSinhDuDieuKien.Count == 0)
{
    MessageBox.Show("Không có học sinh nào đủ điều kiện chuyển trường. Vui lòng kiểm tra lại dữ liệu Excel và cấu hình học kỳ.",
        "Thông báo", ...);
    return;
}
```

**Bước 3: Nhập Học Sinh** (Dòng 1481-1492)

```csharp
// 1. Nhập Học Sinh với trạng thái "Đang học(CT)"
// CHỈ nhập những học sinh đã được xác nhận đủ điều kiện
Dictionary<string, (int maHS, int excelRow, string khoi, DateTime ngayChuyenVao, string nguyenVong)> hocSinhThanhCong =
    ImportHocSinhFromWorksheetChuyenTruong(wsHocSinh, hocKyHienTai, hocSinhDuDieuKien);

if (hocSinhThanhCong.Count == 0)
{
    MessageBox.Show("Không có học sinh nào được nhập thành công. Vui lòng kiểm tra lại dữ liệu Excel.",
        "Thông báo", ...);
    return;
}
```

**Vị trí hàm ImportHocSinhFromWorksheetChuyenTruong**: **Dòng 1578-1908**

**Bước 4: Nhập Phụ Huynh và Mối Quan Hệ** (Dòng 1494-1517)

```csharp
// 2. Chỉ nhập Phụ Huynh của học sinh đã nhập thành công
HashSet<int> phuHuynhMoiTao = new HashSet<int>();
Dictionary<string, (int maPH, int excelRow)> phuHuynhThanhCong =
    ImportPhuHuynhFromWorksheetChuyenTruong(wsPhuHuynh, hocSinhThanhCong, out phuHuynhMoiTao);

// ✅ KIỂM TRA: Nếu sau khi nhập phụ huynh, không còn học sinh nào thì DỪNG LẠI
if (hocSinhThanhCong.Count == 0)
{
    MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập phụ huynh. Quá trình nhập Excel đã dừng lại.",
        "Thông báo", ...);
    return;
}

// 3. Chỉ nhập Mối Quan Hệ của học sinh đã nhập thành công
ImportMoiQuanHeFromWorksheetChuyenTruong(wsMoiQuanHe, hocSinhThanhCong, phuHuynhThanhCong);

// ✅ KIỂM TRA: Nếu sau khi nhập mối quan hệ, không còn học sinh nào thì DỪNG LẠI
if (hocSinhThanhCong.Count == 0)
{
    MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập mối quan hệ. Quá trình nhập Excel đã dừng lại.",
        "Thông báo", ...);
    return;
}
```

**Bước 5: Nhập Điểm, Hạnh kiểm, Xếp loại** (Dòng 1519-1528)

```csharp
// 4. Nhập Điểm, Hạnh kiểm, Xếp loại cho học sinh đã nhập thành công
ImportDiemHanhKiemXepLoaiFromExcel(wsDiem, wsHanhKiem, wsXepLoai, hocSinhThanhCong, hocKyHienTai, phuHuynhThanhCong);

// ✅ KIỂM TRA: Nếu sau khi nhập điểm/hạnh kiểm/xếp loại, không còn học sinh nào thì DỪNG LẠI
if (hocSinhThanhCong.Count == 0)
{
    MessageBox.Show("Không có học sinh nào đủ điều kiện sau khi nhập điểm, hạnh kiểm, xếp loại. Quá trình nhập Excel đã dừng lại.",
        "Thông báo", ...);
    return;
}
```

**Bước 6: Tự động phân lớp** (Dòng 1530-1562)

```csharp
// 5. Kiểm tra điều kiện và tự động phân lớp cho CẢ HK1 và HK2
// ✅ Lấy HK1 và HK2 của năm học từ dropdown
string selectedNamHoc = cbHocKyNamHoc.SelectedItem?.ToString();
HocKyDTO hk1ForPhanLop = null;
HocKyDTO hk2ForPhanLop = null;

if (!string.IsNullOrEmpty(selectedNamHoc) && danhSachNamHoc.ContainsKey(selectedNamHoc))
{
    var (hk1, hk2) = danhSachNamHoc[selectedNamHoc];
    hk1ForPhanLop = hk1;
    hk2ForPhanLop = hk2;
}

if (hk1ForPhanLop != null && hk2ForPhanLop != null)
{
    PhanLopTuDongChoHocSinhChuyenTruong(hocSinhThanhCong, hk1ForPhanLop, hk2ForPhanLop);
}
```

**Vị trí hàm PhanLopTuDongChoHocSinhChuyenTruong**: **Dòng 3844-4253**

#### Thuật toán phân lớp cho học sinh chuyển trường

**Vị trí code**: `PhanLop.cs` - **Dòng 3844-4253**  
**Method**: `PhanLopTuDongChoHocSinhChuyenTruong(...)`

**Bước 1: Parse khối và lấy danh sách lớp** (Dòng 3882-3899)

```csharp
// Parse khối
if (!int.TryParse(khoi, out int maKhoi))
{
    errors.AppendLine($"Học sinh {tenHS}: Khối không hợp lệ ({khoi})");
    errorCount++;
    hocSinhKhongPhanLopDuoc.Add(maHS);
    continue;
}

// Lấy danh sách lớp cùng khối
var lopCungKhoi = allLop.Where(l => l.maKhoi == maKhoi).ToList();
```

**Bước 2: Kiểm tra nguyện vọng** (Dòng 3903-3940)

```csharp
// Nếu có nguyện vọng
if (!string.IsNullOrWhiteSpace(nguyenVong))
{
    // Tìm lớp nguyện vọng
    var lopNguyenVong = lopHocBus.LayLopTheoTen(nguyenVong);
    if (lopNguyenVong != null)
    {
        // ✅ Kiểm tra lớp nguyện vọng cùng khối
        if (lopNguyenVong.maKhoi == maKhoi)
        {
            // ✅ Kiểm tra sĩ số cho CẢ HK1 và HK2 (lấy max để đảm bảo cả 2 học kỳ đều còn chỗ)
            int siSoHK1 = phanLopBLL.CountHocSinhInLop(lopNguyenVong.maLop, hk1.MaHocKy);
            int siSoHK2 = phanLopBLL.CountHocSinhInLop(lopNguyenVong.maLop, hk2.MaHocKy);
            int siSoHienTai = Math.Max(siSoHK1, siSoHK2);

            if (siSoHienTai < lopNguyenVong.siSo)
            {
                lopDuocPhan = lopNguyenVong; // ✅ Có thể phân vào lớp nguyện vọng
            }
        }
        else
        {
            // ❌ Lỗi: Khối không khớp với nguyện vọng
            errors.AppendLine($"Học sinh {tenHS}: Khối '{khoi}' không khớp với nguyện vọng chuyển lớp '{nguyenVong}'");
            errorCount++;
            hocSinhKhongPhanLopDuoc.Add(maHS);
            continue;
        }
    }
}
```

**Bước 3: Tự động phân lớp (nếu không có nguyện vọng)** (Dòng 3943-3974)

```csharp
// Nếu không có lớp nguyện vọng phù hợp, tự động phân lớp
if (lopDuocPhan == null)
{
    // ✅ Sắp xếp lớp theo sĩ số hiện tại (tăng dần) - ưu tiên lớp có ít học sinh nhất
    // ✅ Lấy max sĩ số giữa HK1 và HK2 để đảm bảo cả 2 học kỳ đều còn chỗ
    var lopConCho = lopCungKhoi
        .Select(l => new
        {
            Lop = l,
            SiSoHK1 = phanLopBLL.CountHocSinhInLop(l.maLop, hk1.MaHocKy),
            SiSoHK2 = phanLopBLL.CountHocSinhInLop(l.maLop, hk2.MaHocKy),
            SiSoHienTai = Math.Max(
                phanLopBLL.CountHocSinhInLop(l.maLop, hk1.MaHocKy),
                phanLopBLL.CountHocSinhInLop(l.maLop, hk2.MaHocKy)
            )
        })
        .Where(x => x.SiSoHienTai < x.Lop.siSo) // Chỉ lấy lớp còn chỗ
        .OrderBy(x => x.SiSoHienTai) // Sắp xếp theo sĩ số tăng dần
        .ThenBy(x => x.Lop.tenLop)
        .ToList();

    if (lopConCho.Count > 0)
    {
        lopDuocPhan = lopConCho[0].Lop; // Lớp có ít học sinh nhất
    }
}
```

**Bước 4: Phân lớp cho cả HK1 và HK2** (Dòng 3991-4027)

```csharp
// ✅ Phân lớp cho CẢ HK1 và HK2
bool successHK1 = false;
bool successHK2 = false;

// Phân lớp cho HK1 (nếu chưa có)
if (!daPhanLopHK1)
{
    successHK1 = phanLopBLL.AddPhanLop(maHS, lopDuocPhan.maLop, hk1.MaHocKy);
}

// Phân lớp cho HK2 (nếu chưa có)
if (!daPhanLopHK2)
{
    successHK2 = phanLopBLL.AddPhanLop(maHS, lopDuocPhan.maLop, hk2.MaHocKy);
}

if (successHK1 && successHK2)
{
    successCount++;
    // ✅ Lưu thông tin lớp đã phân
    bool laNguyenVong = !string.IsNullOrWhiteSpace(nguyenVong) &&
                         lopDuocPhan.tenLop.Equals(nguyenVong, StringComparison.OrdinalIgnoreCase);
    lopDaPhan[maHS] = (lopDuocPhan.tenLop, nguyenVong, laNguyenVong);
}
```

#### Đặc điểm quan trọng

- **File Excel yêu cầu**: 6 worksheet (HocSinh, PhuHuynh, MoiQuanHe, Diem, HanhKiem, XepLoai)
- **Trạng thái học sinh**: "Đang học(CT)" (chuyển trường)
- **Tự động phân lớp**: Học sinh được phân lớp ngay sau khi nhập
- **Ưu tiên nguyện vọng**: Nếu học sinh có nguyện vọng lớp và lớp còn chỗ → Phân vào lớp nguyện vọng
- **Điều kiện bắt buộc**: Phải đã phân lớp tự động cho cả HK1 và HK2
- **Phân lớp cho cả HK1 và HK2**: Mỗi học sinh được phân vào cùng một lớp cho cả 2 học kỳ

---

### 3.4. NÚT 4: PHÂN LỚP TỰ ĐỘNG

**Tên nút**: `btnThemPhanLop` (trong code là `btnPhanLopTuDong`)  
**Vị trí code**: `GUI/GUIClass/HocSinh/PhanLop.cs` - **Dòng 166-331**  
**Method**: `btnThemPhanLop_Click(object sender, EventArgs e)`

#### Mô tả chức năng

Nút này thực hiện **phân lớp tự động** cho tất cả học sinh "Đang học" chưa được phân lớp. Thuật toán sẽ xét điều kiện lên lớp (nếu có năm học trước) hoặc phân vào khối 10 (nếu là lần đầu).

#### Cách hoạt động chi tiết

**Bước 1: Kiểm tra điều kiện** (Dòng 172-203)

```csharp
// ✅ Kiểm tra đã chọn học kỳ chưa
if (cbHocKyNamHoc.SelectedItem == null)
{
    MessageBox.Show("Không có học kỳ để phân lớp tự động.", "Thông báo", ...);
    return;
}

// ✅ Lấy năm học được chọn (dạng "Học kỳ I & II - 2025-2026")
string namHocChon = cbHocKyNamHoc.SelectedItem?.ToString();
// ... validate năm học ...

// Lấy HK1 và HK2 của năm học được chọn
var (hk1, hk2) = danhSachNamHoc[namHocChon];
```

**Bước 2: Kiểm tra đã phân lớp chưa** (Dòng 205-223)

```csharp
// *** KIỂM TRA NĂM HỌC ĐÃ ĐƯỢC PHÂN LỚP CHƯA (kiểm tra cả HK1 và HK2) ***
int soHocSinhDaPhanLopHK1 = phanLopBLL.CountHocSinhInHocKy(hk1.MaHocKy);
int soHocSinhDaPhanLopHK2 = phanLopBLL.CountHocSinhInHocKy(hk2.MaHocKy);

if (soHocSinhDaPhanLopHK1 > 0 || soHocSinhDaPhanLopHK2 > 0)
{
    string thongBao = $"⚠️ NĂM HỌC ĐÃ ĐƯỢC PHÂN LỚP!\n\n";
    thongBao += $"Năm học: {namHocChon}\n\n";
    if (soHocSinhDaPhanLopHK1 > 0)
        thongBao += $"   • HK1 ({hk1.TenHocKy}): {soHocSinhDaPhanLopHK1} học sinh\n";
    if (soHocSinhDaPhanLopHK2 > 0)
        thongBao += $"   • HK2 ({hk2.TenHocKy}): {soHocSinhDaPhanLopHK2} học sinh\n";
    thongBao += "\n❌ Không thể phân lớp tự động lại!\n\n";
    thongBao += "Nếu muốn phân lớp lại, bạn cần xóa dữ liệu phân lớp cũ trước.";

    MessageBox.Show(thongBao, "Không thể phân lớp lại", ...);
    return; // CHẶN NGAY, KHÔNG CHO PHÂN LỚP LẠI
}
```

**Logic**: Chặn phân lớp lại nếu đã có học sinh được phân lớp (tránh trùng lặp)

**Bước 3: Hiển thị preview** (Dòng 225-315)

```csharp
// ✅ Truyền HK1 để phân lớp (logic sẽ tự động phân cho cả HK1 và HK2)
// Hiển thị preview trước khi thực hiện
var preview = phanLopTuDongBLL.TaoPreviewPhanLop(hk1.MaHocKy);

// Kiểm tra lỗi
if (preview.ContainsKey("Loi"))
{
    MessageBox.Show($"Không thể tạo preview:\n\n{preview["Loi"]}", "Lỗi", ...);
    return;
}

// TẠO THÔNG BÁO PREVIEW CHI TIẾT
string previewMessage = "╔════════════════════════════════════════════════╗\n";
previewMessage += "║      XEM TRƯỚC KẾT QUẢ PHÂN LỚP TỰ ĐỘNG       ║\n";
previewMessage += "╚════════════════════════════════════════════════╝\n\n";

// Loại phân lớp
previewMessage += $"📋 Kịch bản: {preview["LoaiPhanLop"]}\n";
// ... thêm thông tin preview ...

previewMessage += "Bạn có muốn tiếp tục phân lớp tự động không?";

DialogResult result = MessageBox.Show(previewMessage, "Xác nhận phân lớp tự động",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
```

**Vị trí hàm TaoPreviewPhanLop**: `PhanLopTuDongBLL.cs` - **Dòng 1120-1338**

**Bước 4: Thực hiện phân lớp tự động** (Dòng 317-321)

```csharp
if (result == DialogResult.Yes)
{
    // ✅ Chạy phân lớp tự động trên background thread để không block UI
    // Truyền HK1, logic sẽ tự động phân cho cả HK1 và HK2
    _ = PhanLopTuDongAsync(hk1.MaHocKy);
}
```

**Vị trí hàm PhanLopTuDongAsync**: **Dòng 336-451**

**Bước 5: Xử lý kết quả** (Dòng 356-428)

```csharp
// Thực hiện phân lớp tự động trên background thread
var ketQua = await Task.Run(() => phanLopTuDongBLL.ThucHienPhanLopTuDong(maHocKyHienTai));

// ✅ Update UI trên UI thread
if (ketQua.success)
{
    string thongBaoThanhCong = $"✓ Phân lớp tự động thành công!\n\n" +
                   $"Đã phân lớp: {ketQua.soHocSinhDaPhanLop} học sinh\n\n" +
                   $"{ketQua.message}";

    // Sử dụng ScrollableMessageBox để xem đầy đủ thông tin
    ScrollableMessageBox.Show("Thành công", thongBaoThanhCong, MessageBoxIcon.Information);

    // ✅ Chỉ refresh lại bảng phân lớp của học kỳ vừa phân lớp
    FilterTablePhanLop();

    // ✅ Cập nhật trạng thái nút sau khi phân lớp tự động thành công
    UpdateButtonStates();

    // Tự động chuyển sang tab Phân lớp để xem kết quả
    btnPhanLop_Click(null, null);
}
```

**Vị trí hàm ThucHienPhanLopTuDong**: `PhanLopTuDongBLL.cs` - **Dòng 43-920**

#### Thuật toán phân lớp tự động

Xem chi tiết thuật toán ở **Mục 4** bên dưới.

---

## 4. THUẬT TOÁN PHÂN LỚP TỰ ĐỘNG (CHI TIẾT)

**File**: `BUS/BUSClass/PhanLopTuDongBLL.cs`  
**Method**: `ThucHienPhanLopTuDong(int maHocKyCanPhanLop)`  
**Vị trí code**: **Dòng 43-920**

Thuật toán có **2 kịch bản chính**:

### 4.1. KỊCH BẢN 1: NEXT_YEAR (Phân lớp cho năm học mới)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 139-618**

#### Điều kiện

- Có năm học trước với đầy đủ HK1 và HK2
- Logic: Xét điều kiện lên lớp từ cả HK1 và HK2 năm trước

#### Các bước thuật toán

**Bước 1: Lấy điểm HK1 và HK2 năm trước** (Dòng 164-171)

```csharp
var diemHK1 = allDiem
    .Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKy1NamTruoc.MaHocKy)
    .ToList();

var diemHK2 = allDiem
    .Where(d => d.MaHocSinh == hs.MaHS.ToString() && d.MaHocKy == hocKy2NamTruoc.MaHocKy)
    .ToList();
```

**Bước 2: Tính điểm trung bình cả năm** (Dòng 213-216)

```csharp
double dtbHK1 = diemHK1.Average(d => d.DiemTrungBinh ?? 0);
double dtbHK2 = diemHK2.Average(d => d.DiemTrungBinh ?? 0);
double dtbCaNam = (dtbHK1 * 1 + dtbHK2 * 2) / 3.0; // HK2 hệ số 2
```

**Công thức**: `ĐTB cả năm = (ĐTB HK1 × 1 + ĐTB HK2 × 2) / 3`

**Bước 3: Xét hạnh kiểm cả năm** (Dòng 220-229)

```csharp
string[] thuTuHanhKiem = { "Yếu", "Trung Bình", "Khá", "Tốt" };
int indexHK1 = Array.IndexOf(thuTuHanhKiem, hanhKiemHK1.XepLoai);
int indexHK2 = Array.IndexOf(thuTuHanhKiem, hanhKiemHK2.XepLoai);
int indexMin = Math.Min(indexHK1, indexHK2);
string hanhKiemCaNam = thuTuHanhKiem[indexMin];
```

**Logic**: Hạnh kiểm cả năm = Hạnh kiểm thấp hơn giữa HK1 và HK2

**Bước 4: Đếm môn Kém và Yếu** (Dòng 233-247)

```csharp
var diemTheoMon = tatCaDiemCaNam
    .GroupBy(d => d.MaMonHoc)
    .Select(g => new
    {
        MaMon = g.Key,
        DiemTBMon = g.Average(d => d.DiemTrungBinh ?? 0)
    })
    .ToList();

int soMonKem = diemTheoMon.Count(m => m.DiemTBMon < 3.5);
int soMonYeu = diemTheoMon.Count(m => m.DiemTBMon >= 3.5 && m.DiemTBMon < 5.0);
```

**Định nghĩa**:

- **Môn Kém**: ĐTB môn < 3.5
- **Môn Yếu**: 3.5 ≤ ĐTB môn < 5.0

**Bước 5: Kiểm tra điều kiện lên lớp** (Dòng 251-281)

```csharp
bool duDieuKienLenLop = true;

// Điều kiện 1: ĐTB cả năm >= 5.0
if (dtbCaNam < 5.0)
{
    duDieuKienLenLop = false;
    lyDoKhongLenLop.Add($"ĐTB cả năm {dtbCaNam:0.00} < 5.0");
}

// Điều kiện 2: Hạnh kiểm >= Trung Bình
if (indexMin < 1) // Yếu
{
    duDieuKienLenLop = false;
    lyDoKhongLenLop.Add($"Hạnh kiểm '{hanhKiemCaNam}' < Trung Bình");
}

// Điều kiện 3: Không có môn Kém
if (soMonKem > 0)
{
    duDieuKienLenLop = false;
    lyDoKhongLenLop.Add($"Có {soMonKem} môn Kém");
}

// Điều kiện 4: Tối đa 2 môn Yếu
if (soMonYeu > 2)
{
    duDieuKienLenLop = false;
    lyDoKhongLenLop.Add($"Có {soMonYeu} môn Yếu (> 2)");
}
```

**4 Điều kiện lên lớp**:

1. ✅ ĐTB cả năm ≥ 5.0
2. ✅ Hạnh kiểm ≥ Trung Bình
3. ✅ Không có môn Kém
4. ✅ Tối đa 2 môn Yếu

**Bước 6: Xác định khối mới** (Dòng 300-361)

```csharp
if (duDieuKienLenLop)
{
    // Lên khối cao hơn
    khoiMoi = khoiCu + 1;
    if (khoiMoi > 12)
    {
        // Xử lý tốt nghiệp (không thể lên lớp)
        // Cập nhật trạng thái "Đã tốt nghiệp" nếu học kỳ đã kết thúc
    }
}
else
{
    // Ở lại khối cũ (học lại)
    khoiMoi = khoiCu;
}
```

**Logic**:

- **Đủ điều kiện**: `khoiMoi = khoiCu + 1` (lên lớp)
- **Không đủ điều kiện**: `khoiMoi = khoiCu` (học lại)
- **Khối 12**: Xử lý tốt nghiệp

**Bước 7: Tìm lớp có chỗ trống** (Dòng 363-416)

```csharp
var dsLopKhoiMoi = allLop.Where(l => l.MaKhoi == khoiMoi).ToList();

// Đếm số học sinh trong từng lớp (cả HK1 và HK2)
var soLuongHocSinhTrongLop = new Dictionary<int, int>();
// ... đếm từ database và danh sách tạm ...

// Tìm lớp có ít học sinh nhất
LopDTO lopPhuHop = null;
int soHocSinhItNhat = int.MaxValue;

foreach (var lop in dsLopKhoiMoi)
{
    int soHS = soLuongHocSinhTrongLop.ContainsKey(lop.MaLop)
        ? soLuongHocSinhTrongLop[lop.MaLop] : 0;
    if (soHS < soHocSinhItNhat)
    {
        soHocSinhItNhat = soHS;
        lopPhuHop = lop;
    }
}
```

**Thuật toán**: Chọn lớp có **ít học sinh nhất** trong khối để cân bằng sĩ số

**Bước 8: Phân lớp cho cả HK1 và HK2** (Dòng 418-449)

```csharp
bool themHK1ThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
bool themHK2ThanhCong = phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);

if (themHK1ThanhCong && themHK2ThanhCong)
{
    soHocSinhDaPhanLop += 2; // Đếm cả HK1 và HK2
    // Lưu vào danh sách tạm để cập nhật số lượng
}
else
{
    // Rollback nếu một trong hai thất bại
    if (themHK1ThanhCong)
        phanLopDAO.XoaPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
    if (themHK2ThanhCong)
        phanLopDAO.XoaPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);
}
```

**Đặc điểm**: Mỗi học sinh được phân vào **cùng một lớp** cho cả HK1 và HK2

### 4.2. KỊCH BẢN 2: FIRST_TIME (Phân lớp lần đầu)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 619-795**

#### Điều kiện

- Không có năm học trước
- Logic: Phân tất cả học sinh vào khối 10, theo chữ cái đầu tiên của tên

#### Các bước thuật toán

**Bước 1: Lấy tất cả học sinh "Đang học"** (Dòng 628-629)

```csharp
var hocSinhCanPhanLop = danhSachHocSinhDangHoc.ToList();
```

**Bước 2: Xác định khối (luôn là khối 10)** (Dòng 635-642)

```csharp
int khoiCanPhanLop = 10;
var dsLopKhoi10 = allLop
    .Where(l => l.MaKhoi == khoiCanPhanLop)
    .OrderBy(l => l.MaLop)
    .ToList();
```

**Bước 3: Nhóm học sinh theo chữ cái đầu tiên** (Dòng 680-706)

```csharp
var hocSinhTheoChuCai = new Dictionary<char, List<HocSinhDTO>>();

foreach (var hs in hocSinhCanPhanLop)
{
    char chuCaiDau = '?';
    if (!string.IsNullOrWhiteSpace(hs.HoTen))
    {
        string tenTrimmed = hs.HoTen.Trim();
        if (tenTrimmed.Length > 0)
        {
            chuCaiDau = char.ToUpper(tenTrimmed[0]);
            if (!char.IsLetter(chuCaiDau))
                chuCaiDau = '?'; // Ký tự đặc biệt
        }
    }

    if (!hocSinhTheoChuCai.ContainsKey(chuCaiDau))
        hocSinhTheoChuCai[chuCaiDau] = new List<HocSinhDTO>();
    hocSinhTheoChuCai[chuCaiDau].Add(hs);
}
```

**Logic**: Lấy **chữ cái đầu tiên** của tên (bỏ qua khoảng trắng), chuyển thành chữ hoa

**Bước 4: Phân đều vào các lớp** (Dòng 714-785)

```csharp
foreach (var chuCai in danhSachChuCai) // Sắp xếp A-Z, sau đó ký tự đặc biệt
{
    List<HocSinhDTO> dsHSTheoChuCai = hocSinhTheoChuCai[chuCai];

    foreach (var hs in dsHSTheoChuCai)
    {
        // Tìm lớp có ít học sinh nhất
        var lopPhuHop = dsLopKhoi10
            .OrderBy(lop => soLuongHocSinhTrongLop.ContainsKey(lop.MaLop)
                ? soLuongHocSinhTrongLop[lop.MaLop] : 0)
            .ThenBy(lop => lop.MaLop) // Nếu bằng nhau, ưu tiên MaLop nhỏ hơn
            .First();

        // Phân lớp cho cả HK1 và HK2
        phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
        phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);

        // Cập nhật số lượng
        soLuongHocSinhTrongLop[lopPhuHop.MaLop] += 2;
    }
}
```

**Thuật toán**:

1. Sắp xếp chữ cái: A-Z trước, ký tự đặc biệt sau
2. Với mỗi nhóm chữ cái, phân đều vào các lớp
3. Ưu tiên lớp có **ít học sinh nhất** để cân bằng sĩ số
4. Mỗi học sinh được phân vào **cùng một lớp** cho cả HK1 và HK2

---

## 5. THUẬT TOÁN ĐƯỢC SỬ DỤNG TRONG PHÂN LỚP TỰ ĐỘNG

### 5.1. Tổng quan thuật toán

Phân lớp tự động sử dụng **nhiều thuật toán** kết hợp để đảm bảo phân lớp công bằng và cân bằng sĩ số:

1. **Thuật toán tính điểm trung bình** (Weighted Average)
2. **Thuật toán xét điều kiện lên lớp** (Rule-based Decision)
3. **Thuật toán tìm lớp ít học sinh nhất** (Greedy - Minimum Selection)
4. **Thuật toán nhóm theo chữ cái** (Grouping Algorithm)
5. **Thuật toán phân đều** (Round-robin Distribution)

---

### 5.2. Thuật toán 1: Tính điểm trung bình cả năm (Weighted Average)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 213-216**

**Thuật toán**:

```csharp
double dtbHK1 = diemHK1.Average(d => d.DiemTrungBinh ?? 0);
double dtbHK2 = diemHK2.Average(d => d.DiemTrungBinh ?? 0);
double dtbCaNam = (dtbHK1 * 1 + dtbHK2 * 2) / 3.0; // HK2 hệ số 2
```

**Công thức**:

```
ĐTB cả năm = (ĐTB HK1 × 1 + ĐTB HK2 × 2) / 3
```

**Giải thích**:

- HK1 có hệ số 1
- HK2 có hệ số 2 (quan trọng hơn)
- Tổng hệ số = 3

**Ví dụ**:

- ĐTB HK1 = 6.5, ĐTB HK2 = 7.0
- ĐTB cả năm = (6.5 × 1 + 7.0 × 2) / 3 = 20.5 / 3 = 6.83

**Độ phức tạp**: O(n) với n là số môn học

---

### 5.3. Thuật toán 2: Xét hạnh kiểm cả năm (Minimum Selection)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 220-229**

**Thuật toán**:

```csharp
string[] thuTuHanhKiem = { "Yếu", "Trung Bình", "Khá", "Tốt" };
int indexHK1 = Array.IndexOf(thuTuHanhKiem, hanhKiemHK1.XepLoai);
int indexHK2 = Array.IndexOf(thuTuHanhKiem, hanhKiemHK2.XepLoai);
int indexMin = Math.Min(indexHK1, indexHK2);
string hanhKiemCaNam = thuTuHanhKiem[indexMin];
```

**Logic**:

- Chuyển hạnh kiểm thành index (Yếu=0, Trung Bình=1, Khá=2, Tốt=3)
- Hạnh kiểm cả năm = Hạnh kiểm có index thấp hơn (tức là xấu hơn)

**Ví dụ**:

- HK1 = "Khá" (index=2), HK2 = "Tốt" (index=3)
- indexMin = min(2, 3) = 2
- Hạnh kiểm cả năm = "Khá"

**Độ phức tạp**: O(1)

---

### 5.4. Thuật toán 3: Đếm môn Kém và Yếu (Grouping & Counting)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 233-247**

**Thuật toán**:

```csharp
var diemTheoMon = tatCaDiemCaNam
    .GroupBy(d => d.MaMonHoc)
    .Select(g => new
    {
        MaMon = g.Key,
        DiemTBMon = g.Average(d => d.DiemTrungBinh ?? 0)
    })
    .ToList();

int soMonKem = diemTheoMon.Count(m => m.DiemTBMon < 3.5);
int soMonYeu = diemTheoMon.Count(m => m.DiemTBMon >= 3.5 && m.DiemTBMon < 5.0);
```

**Các bước**:

1. **GroupBy**: Nhóm điểm theo môn học
2. **Select**: Tính ĐTB môn từ tất cả điểm của môn đó
3. **Count**: Đếm số môn Kém (< 3.5) và Yếu (3.5-5.0)

**Độ phức tạp**: O(n) với n là số điểm

---

### 5.5. Thuật toán 4: Xét điều kiện lên lớp (Rule-based Decision)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 251-281**

**Thuật toán**: Sử dụng **4 quy tắc** để quyết định:

```csharp
bool duDieuKienLenLop = true;

// Quy tắc 1: ĐTB cả năm >= 5.0
if (dtbCaNam < 5.0) duDieuKienLenLop = false;

// Quy tắc 2: Hạnh kiểm >= Trung Bình
if (indexMin < 1) duDieuKienLenLop = false;

// Quy tắc 3: Không có môn Kém
if (soMonKem > 0) duDieuKienLenLop = false;

// Quy tắc 4: Tối đa 2 môn Yếu
if (soMonYeu > 2) duDieuKienLenLop = false;
```

**Logic**: Tất cả 4 điều kiện phải thỏa mãn → Mới đủ điều kiện lên lớp

**Độ phức tạp**: O(1)

---

### 5.6. Thuật toán 5: Tìm lớp có ít học sinh nhất (Greedy - Minimum Selection)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 396-408** (NEXT_YEAR), **Dòng 737-740** (FIRST_TIME)

**Thuật toán**:

```csharp
// Đếm số học sinh trong từng lớp
var soLuongHocSinhTrongLop = new Dictionary<int, int>();
// ... đếm từ database và danh sách tạm ...

// Tìm lớp có ít học sinh nhất
LopDTO lopPhuHop = null;
int soHocSinhItNhat = int.MaxValue;

foreach (var lop in dsLopKhoiMoi)
{
    int soHS = soLuongHocSinhTrongLop.ContainsKey(lop.MaLop)
        ? soLuongHocSinhTrongLop[lop.MaLop] : 0;
    if (soHS < soHocSinhItNhat)
    {
        soHocSinhItNhat = soHS;
        lopPhuHop = lop;
    }
}
```

**Hoặc sử dụng LINQ** (FIRST_TIME):

```csharp
var lopPhuHop = dsLopKhoi10
    .OrderBy(lop => soLuongHocSinhTrongLop.ContainsKey(lop.MaLop)
        ? soLuongHocSinhTrongLop[lop.MaLop] : 0)
    .ThenBy(lop => lop.MaLop) // Nếu bằng nhau, ưu tiên MaLop nhỏ hơn
    .First();
```

**Logic**:

- Duyệt qua tất cả lớp trong khối
- Tìm lớp có số học sinh ít nhất
- Nếu nhiều lớp có cùng số học sinh → Ưu tiên lớp có MaLop nhỏ hơn

**Mục đích**: Cân bằng sĩ số giữa các lớp

**Độ phức tạp**: O(m) với m là số lớp trong khối

---

### 5.7. Thuật toán 6: Nhóm học sinh theo chữ cái (Grouping Algorithm)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 508-532** (NEXT_YEAR), **Dòng 680-706** (FIRST_TIME)

**Thuật toán**:

```csharp
var hocSinhTheoChuCai = new Dictionary<char, List<HocSinhDTO>>();

foreach (var hs in hocSinhCanPhanLop)
{
    char chuCaiDau = '?';
    if (!string.IsNullOrWhiteSpace(hs.HoTen))
    {
        string tenTrimmed = hs.HoTen.Trim();
        if (tenTrimmed.Length > 0)
        {
            chuCaiDau = char.ToUpper(tenTrimmed[0]);
            if (!char.IsLetter(chuCaiDau))
                chuCaiDau = '?'; // Ký tự đặc biệt
        }
    }

    if (!hocSinhTheoChuCai.ContainsKey(chuCaiDau))
        hocSinhTheoChuCai[chuCaiDau] = new List<HocSinhDTO>();
    hocSinhTheoChuCai[chuCaiDau].Add(hs);
}
```

**Các bước**:

1. Lấy chữ cái đầu tiên của tên (bỏ qua khoảng trắng)
2. Chuyển thành chữ hoa
3. Nếu không phải chữ cái → Gán '?' (nhóm ký tự đặc biệt)
4. Nhóm học sinh vào Dictionary theo chữ cái

**Ví dụ**:

- "Nguyễn Văn A" → Nhóm 'N'
- "Trần Thị B" → Nhóm 'T'
- "123 Học Sinh" → Nhóm '?'

**Độ phức tạp**: O(n) với n là số học sinh

---

### 5.8. Thuật toán 7: Phân đều học sinh vào các lớp (Round-robin với Greedy)

**Vị trí code**: `PhanLopTuDongBLL.cs` - **Dòng 547-606** (NEXT_YEAR), **Dòng 724-785** (FIRST_TIME)

**Thuật toán**:

```csharp
// Sắp xếp chữ cái: A-Z trước, ký tự đặc biệt sau
var danhSachChuCai = hocSinhTheoChuCai.Keys
    .OrderBy(c => c == '?' ? 999 : (int)c)
    .ToList();

foreach (var chuCai in danhSachChuCai)
{
    List<HocSinhDTO> dsHSTheoChuCai = hocSinhTheoChuCai[chuCai];

    foreach (var hs in dsHSTheoChuCai)
    {
        // Tìm lớp có ít học sinh nhất
        var lopPhuHop = dsLopKhoi10
            .OrderBy(lop => soLuongHocSinhTrongLop.ContainsKey(lop.MaLop)
                ? soLuongHocSinhTrongLop[lop.MaLop] : 0)
            .ThenBy(lop => lop.MaLop)
            .First();

        // Phân lớp cho cả HK1 và HK2
        phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy1.MaHocKy);
        phanLopDAO.ThemPhanLop(hs.MaHS, lopPhuHop.MaLop, hocKy2.MaHocKy);

        // Cập nhật số lượng
        soLuongHocSinhTrongLop[lopPhuHop.MaLop] += 2;
    }
}
```

**Logic**:

1. Sắp xếp chữ cái: A-Z trước, '?' sau
2. Với mỗi nhóm chữ cái:
   - Với mỗi học sinh trong nhóm:
     - Tìm lớp có ít học sinh nhất (Greedy)
     - Phân học sinh vào lớp đó
     - Cập nhật số lượng học sinh của lớp

**Đặc điểm**:

- **Greedy**: Luôn chọn lớp có ít học sinh nhất tại thời điểm hiện tại
- **Dynamic**: Số lượng học sinh được cập nhật sau mỗi lần phân lớp
- **Cân bằng**: Đảm bảo sĩ số các lớp gần bằng nhau

**Độ phức tạp**: O(n × m) với n là số học sinh, m là số lớp

---

### 5.9. Tóm tắt thuật toán

| Thuật toán                 | Vị trí code           | Độ phức tạp | Mục đích                 |
| -------------------------- | --------------------- | ----------- | ------------------------ |
| Weighted Average           | Dòng 213-216          | O(n)        | Tính ĐTB cả năm          |
| Minimum Selection          | Dòng 220-229          | O(1)        | Xét hạnh kiểm cả năm     |
| Grouping & Counting        | Dòng 233-247          | O(n)        | Đếm môn Kém/Yếu          |
| Rule-based Decision        | Dòng 251-281          | O(1)        | Xét điều kiện lên lớp    |
| Greedy - Minimum Selection | Dòng 396-408, 737-740 | O(m)        | Tìm lớp ít học sinh nhất |
| Grouping Algorithm         | Dòng 508-532, 680-706 | O(n)        | Nhóm theo chữ cái        |
| Round-robin với Greedy     | Dòng 547-606, 724-785 | O(n × m)    | Phân đều vào các lớp     |

**Tổng độ phức tạp**: O(n × m) với n là số học sinh, m là số lớp

---

## 6. KẾT LUẬN

Phần **Phân lớp** là một module quan trọng với 4 nút chức năng chính:

1. **Thêm học sinh thủ công**: Thêm từng học sinh một, trạng thái "Đang học"
2. **Nhập Excel (Tuyển sinh)**: Nhập nhiều học sinh từ Excel, trạng thái "Đang học"
3. **Phân lớp chuyển trường**: Nhập học sinh chuyển trường từ Excel và tự động phân lớp, trạng thái "Đang học(CT)"
4. **Phân lớp tự động**: Tự động phân tất cả học sinh vào lớp dựa trên thuật toán thông minh

Thuật toán phân lớp tự động đảm bảo tính công bằng, minh bạch và tự động hóa cao trong quá trình phân lớp, với khả năng xét điều kiện lên lớp và cân bằng sĩ số các lớp.
