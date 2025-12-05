-- =====================================================================
-- CÁC CÂU LỆNH SQL ĐỂ LẤY THÔNG TIN BỔ SUNG CHO EXCEL CHUYỂN TRƯỜNG
-- =====================================================================

-- 1. LẤY THÔNG TIN HỌC SINH CHUYỂN TRƯỜNG (Trạng thái = "Đang học(CT)")
--    Để biết Mã HS, Tên, Khối của từng học sinh
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    pl.MaLop,
    l.TenLop AS 'Lớp',
    l.MaKhoi AS 'Khối',
    hs.TrangThai AS 'Trạng thái'
FROM HocSinh hs
LEFT JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
LEFT JOIN LopHoc l ON pl.MaLop = l.MaLop
WHERE hs.TrangThai = 'Đang học(CT)'
ORDER BY hs.MaHocSinh;

-- 2. LẤY THÔNG TIN HỌC KỲ CẦN THIẾT
--    Để biết Mã học kỳ, Tên học kỳ, Năm học cần nhập điểm
SELECT 
    hk.MaHocKy AS 'Mã học kỳ',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    hk.TrangThai AS 'Trạng thái',
    hk.NgayBD AS 'Ngày bắt đầu',
    hk.NgayKT AS 'Ngày kết thúc'
FROM HocKy hk
ORDER BY hk.MaNamHoc, hk.MaHocKy;

-- 3. LẤY DANH SÁCH MÔN HỌC (13 môn)
--    Để biết Mã môn học, Tên môn học cần nhập điểm
SELECT 
    mh.MaMonHoc AS 'Mã môn học',
    mh.TenMonHoc AS 'Tên môn học',
    mh.SoTiet AS 'Số tiết',
    mh.GhiChu AS 'Ghi chú'
FROM MonHoc mh
ORDER BY mh.MaMonHoc;

-- 4. LẤY THÔNG TIN HỌC KỲ HIỆN TẠI (Đang diễn ra)
--    Để xác định học kỳ nào đang diễn ra
SELECT 
    hk.MaHocKy AS 'Mã học kỳ',
    hk.TenHocKy AS 'Tên học kỳ',
    hk.MaNamHoc AS 'Năm học',
    hk.TrangThai AS 'Trạng thái'
FROM HocKy hk
WHERE hk.TrangThai = 'Đang diễn ra'
LIMIT 1;

-- 5. LẤY THÔNG TIN HỌC SINH CHUYỂN TRƯỜNG KÈM KHỐI (Chi tiết hơn)
--    Nếu học sinh chưa được phân lớp, vẫn lấy được khối từ dữ liệu Excel đã nhập
--    (Cần xem lại dữ liệu Excel hoặc bảng tạm nếu có)
SELECT 
    hs.MaHocSinh AS 'Mã HS',
    hs.HoTen AS 'Họ và tên',
    hs.NgaySinh AS 'Ngày sinh',
    hs.GioiTinh AS 'Giới tính',
    hs.SDTHS AS 'SĐT',
    hs.Email AS 'Email',
    hs.TrangThai AS 'Trạng thái',
    pl.MaLop,
    l.TenLop AS 'Lớp',
    l.MaKhoi AS 'Khối'
FROM HocSinh hs
LEFT JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
    AND pl.MaHocKy = (SELECT MaHocKy FROM HocKy WHERE TrangThai = 'Đang diễn ra' LIMIT 1)
LEFT JOIN LopHoc l ON pl.MaLop = l.MaLop
WHERE hs.TrangThai = 'Đang học(CT)'
ORDER BY hs.MaHocSinh;

-- =====================================================================
-- HƯỚNG DẪN SỬ DỤNG:
-- =====================================================================
-- 1. Chạy Query 1 để lấy danh sách học sinh chuyển trường và khối của họ
-- 2. Chạy Query 4 để xác định học kỳ hiện tại (Đang diễn ra)
-- 3. Dựa vào khối và học kỳ hiện tại, xác định các học kỳ cần thiết:
--    - Khối 10, HK1 đang diễn ra → Không cần học kỳ nào
--    - Khối 10, HK2 đang diễn ra → Cần HK1 của năm học hiện tại
--    - Khối 11, HK1 đang diễn ra → Cần HK1, HK2 của năm học trước
--    - Khối 11, HK2 đang diễn ra → Cần HK1 của năm học hiện tại + HK1, HK2 của năm học trước
--    - Khối 12, HK1 đang diễn ra → Cần HK1, HK2 của 2 năm học trước
--    - Khối 12, HK2 đang diễn ra → Cần HK1 của năm học hiện tại + HK1, HK2 của 2 năm học trước
-- 4. Chạy Query 2 để lấy danh sách học kỳ cần thiết
-- 5. Chạy Query 3 để lấy danh sách 13 môn học
-- 6. Tạo dữ liệu Excel với:
--    - Mỗi học sinh × Mỗi học kỳ cần thiết × 13 môn học = Số dòng điểm
--    - Mỗi học sinh × Mỗi học kỳ cần thiết = Số dòng hạnh kiểm
--    - Mỗi học sinh × Mỗi học kỳ cần thiết = Số dòng xếp loại
-- =====================================================================

