-- =====================================================================
-- CÁC CÂU LỆNH SQL ĐỂ KIỂM TRA ĐIỂM SỐ, HẠNH KIỂM VÀ XẾP LOẠI
-- Cho học sinh chuyển trường (Trạng thái = "Đang học(CT)")
-- =====================================================================

-- =====================================================================
-- 1. KIỂM TRA ĐIỂM SỐ (DiemSo)
-- =====================================================================

-- 1.1. Kiểm tra điểm số của 2 học sinh khối 10 (Mã HS: 512, 513)
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    mh.TenMonHoc AS 'Tên môn học',
    ds.DiemThuongXuyen AS 'Điểm thường xuyên',
    ds.DiemGiuaKy AS 'Điểm giữa kỳ',
    ds.DiemCuoiKy AS 'Điểm cuối kỳ',
    ds.DiemTrungBinh AS 'Điểm trung bình'
FROM HocSinh hs
LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh
LEFT JOIN HocKy hk ON ds.MaHocKy = hk.MaHocKy
LEFT JOIN MonHoc mh ON ds.MaMonHoc = mh.MaMonHoc
WHERE hs.MaHocSinh IN (512, 513)
ORDER BY hs.MaHocSinh, hk.MaHocKy, mh.MaMonHoc;

-- 1.2. Tổng hợp số lượng điểm số của từng học sinh
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    COUNT(ds.MaMonHoc) AS 'Số môn có điểm',
    COUNT(DISTINCT ds.MaHocKy) AS 'Số học kỳ có điểm'
FROM HocSinh hs
LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh
WHERE hs.MaHocSinh IN (512, 513)
GROUP BY hs.MaHocSinh, hs.HoTen;

-- 1.3. Kiểm tra điểm số của TẤT CẢ học sinh chuyển trường
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    COUNT(ds.MaMonHoc) AS 'Số môn có điểm',
    AVG(ds.DiemTrungBinh) AS 'Điểm TB chung'
FROM HocSinh hs
LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh
LEFT JOIN HocKy hk ON ds.MaHocKy = hk.MaHocKy
WHERE hs.TrangThai = 'Đang học(CT)'
GROUP BY hs.MaHocSinh, hs.HoTen, hk.MaHocKy, hk.TenHocKy, hk.MaNamHoc
ORDER BY hs.MaHocSinh, hk.MaHocKy;

-- =====================================================================
-- 2. KIỂM TRA HẠNH KIỂM (HanhKiem)
-- =====================================================================

-- 2.1. Kiểm tra hạnh kiểm của 2 học sinh khối 10 (Mã HS: 512, 513)
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    hk.TrangThai AS 'Trạng thái học kỳ',
    hk.NgayBD AS 'Ngày bắt đầu',
    hk.NgayKT AS 'Ngày kết thúc',
    hk2.XepLoai AS 'Xếp loại hạnh kiểm',
    hk2.NhanXet AS 'Nhận xét'
FROM HocSinh hs
LEFT JOIN HanhKiem hk2 ON hs.MaHocSinh = hk2.MaHocSinh
LEFT JOIN HocKy hk ON hk2.MaHocKy = hk.MaHocKy
WHERE hs.MaHocSinh IN (512, 513)
ORDER BY hs.MaHocSinh, hk.MaHocKy;

-- 2.2. Tổng hợp số lượng hạnh kiểm của từng học sinh
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    COUNT(hk2.MaHocKy) AS 'Số học kỳ có hạnh kiểm'
FROM HocSinh hs
LEFT JOIN HanhKiem hk2 ON hs.MaHocSinh = hk2.MaHocSinh
WHERE hs.MaHocSinh IN (512, 513)
GROUP BY hs.MaHocSinh, hs.HoTen;

-- 2.3. Kiểm tra hạnh kiểm của TẤT CẢ học sinh chuyển trường
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    hk2.XepLoai AS 'Xếp loại hạnh kiểm',
    hk2.NhanXet AS 'Nhận xét'
FROM HocSinh hs
LEFT JOIN HanhKiem hk2 ON hs.MaHocSinh = hk2.MaHocSinh
LEFT JOIN HocKy hk ON hk2.MaHocKy = hk.MaHocKy
WHERE hs.TrangThai = 'Đang học(CT)'
ORDER BY hs.MaHocSinh, hk.MaHocKy;

-- =====================================================================
-- 3. KIỂM TRA XẾP LOẠI (XepLoai)
-- =====================================================================

-- 3.1. Kiểm tra xếp loại của 2 học sinh khối 10 (Mã HS: 512, 513)
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    hk.TrangThai AS 'Trạng thái học kỳ',
    hk.NgayBD AS 'Ngày bắt đầu',
    hk.NgayKT AS 'Ngày kết thúc',
    xl.HocLuc AS 'Học lực',
    xl.GhiChu AS 'Ghi chú'
FROM HocSinh hs
LEFT JOIN XepLoai xl ON hs.MaHocSinh = xl.MaHocSinh
LEFT JOIN HocKy hk ON xl.MaHocKy = hk.MaHocKy
WHERE hs.MaHocSinh IN (512, 513)
ORDER BY hs.MaHocSinh, hk.MaHocKy;

-- 3.2. Tổng hợp số lượng xếp loại của từng học sinh
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    COUNT(xl.MaHocKy) AS 'Số học kỳ có xếp loại'
FROM HocSinh hs
LEFT JOIN XepLoai xl ON hs.MaHocSinh = xl.MaHocSinh
WHERE hs.MaHocSinh IN (512, 513)
GROUP BY hs.MaHocSinh, hs.HoTen;

-- 3.3. Kiểm tra xếp loại của TẤT CẢ học sinh chuyển trường
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    xl.HocLuc AS 'Học lực',
    xl.GhiChu AS 'Ghi chú'
FROM HocSinh hs
LEFT JOIN XepLoai xl ON hs.MaHocSinh = xl.MaHocSinh
LEFT JOIN HocKy hk ON xl.MaHocKy = hk.MaHocKy
WHERE hs.TrangThai = 'Đang học(CT)'
ORDER BY hs.MaHocSinh, hk.MaHocKy;

-- =====================================================================
-- 4. TỔNG HỢP TẤT CẢ THÔNG TIN (Điểm + Hạnh kiểm + Xếp loại)
-- =====================================================================

-- 4.1. Tổng hợp đầy đủ cho 2 học sinh khối 10 (Mã HS: 512, 513)
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hs.TrangThai AS 'Trạng thái',
    pl.MaLop AS 'Mã lớp',
    l.TenLop AS 'Tên lớp',
    l.MaKhoi AS 'Khối',
    -- Điểm số
    COUNT(DISTINCT ds.MaMonHoc) AS 'Số môn có điểm',
    COUNT(DISTINCT ds.MaHocKy) AS 'Số học kỳ có điểm',
    -- Hạnh kiểm
    COUNT(DISTINCT hk2.MaHocKy) AS 'Số học kỳ có hạnh kiểm',
    -- Xếp loại
    COUNT(DISTINCT xl.MaHocKy) AS 'Số học kỳ có xếp loại'
FROM HocSinh hs
LEFT JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
    AND pl.MaHocKy = (SELECT MaHocKy FROM HocKy WHERE TrangThai = 'Đang diễn ra' LIMIT 1)
LEFT JOIN LopHoc l ON pl.MaLop = l.MaLop
LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh
LEFT JOIN HanhKiem hk2 ON hs.MaHocSinh = hk2.MaHocSinh
LEFT JOIN XepLoai xl ON hs.MaHocSinh = xl.MaHocSinh
WHERE hs.MaHocSinh IN (512, 513)
GROUP BY hs.MaHocSinh, hs.HoTen, hs.TrangThai, pl.MaLop, l.TenLop, l.MaKhoi;

-- 4.2. Tổng hợp đầy đủ cho TẤT CẢ học sinh chuyển trường
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hs.TrangThai AS 'Trạng thái',
    pl.MaLop AS 'Mã lớp',
    l.TenLop AS 'Tên lớp',
    l.MaKhoi AS 'Khối',
    -- Điểm số
    COUNT(DISTINCT ds.MaMonHoc) AS 'Số môn có điểm',
    COUNT(DISTINCT ds.MaHocKy) AS 'Số học kỳ có điểm',
    -- Hạnh kiểm
    COUNT(DISTINCT hk2.MaHocKy) AS 'Số học kỳ có hạnh kiểm',
    -- Xếp loại
    COUNT(DISTINCT xl.MaHocKy) AS 'Số học kỳ có xếp loại'
FROM HocSinh hs
LEFT JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
    AND pl.MaHocKy = (SELECT MaHocKy FROM HocKy WHERE TrangThai = 'Đang diễn ra' LIMIT 1)
LEFT JOIN LopHoc l ON pl.MaLop = l.MaLop
LEFT JOIN DiemSo ds ON hs.MaHocSinh = ds.MaHocSinh
LEFT JOIN HanhKiem hk2 ON hs.MaHocSinh = hk2.MaHocSinh
LEFT JOIN XepLoai xl ON hs.MaHocSinh = xl.MaHocSinh
WHERE hs.TrangThai = 'Đang học(CT)'
GROUP BY hs.MaHocSinh, hs.HoTen, hs.TrangThai, pl.MaLop, l.TenLop, l.MaKhoi
ORDER BY hs.MaHocSinh;

-- =====================================================================
-- 5. KIỂM TRA HỌC KỲ HIỆN TẠI (Đang diễn ra)
-- =====================================================================

-- 5.1. Thông tin học kỳ đang diễn ra
SELECT 
    hk.MaHocKy AS 'Mã học kỳ',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    hk.TrangThai AS 'Trạng thái',
    hk.NgayBD AS 'Ngày bắt đầu',
    hk.NgayKT AS 'Ngày kết thúc'
FROM HocKy hk
WHERE hk.TrangThai = 'Đang diễn ra'
LIMIT 1;

-- =====================================================================
-- HƯỚNG DẪN SỬ DỤNG:
-- =====================================================================
-- 1. Chạy Query 1.1 để xem chi tiết điểm số của 2 học sinh khối 10
-- 2. Chạy Query 1.2 để xem tổng hợp số lượng điểm số
-- 3. Chạy Query 2.1 để xem chi tiết hạnh kiểm của 2 học sinh khối 10
-- 4. Chạy Query 2.2 để xem tổng hợp số lượng hạnh kiểm
-- 5. Chạy Query 3.1 để xem chi tiết xếp loại của 2 học sinh khối 10
-- 6. Chạy Query 3.2 để xem tổng hợp số lượng xếp loại
-- 7. Chạy Query 4.1 để xem tổng hợp đầy đủ cho 2 học sinh khối 10
-- 8. Chạy Query 5.1 để xác định học kỳ đang diễn ra
--
-- LƯU Ý:
-- - Với học sinh khối 10, HK1 đang diễn ra → KHÔNG cần học kỳ nào
-- - Do đó, điểm số, hạnh kiểm, xếp loại sẽ KHÔNG được lưu vào database
-- - Các query trên sẽ trả về NULL hoặc 0 nếu không có dữ liệu
-- =====================================================================

