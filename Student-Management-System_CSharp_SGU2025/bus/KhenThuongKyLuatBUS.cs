using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    /// <summary>
    /// Business Logic Layer cho Khen Thưởng Kỷ Luật
    /// </summary>
    public class KhenThuongKyLuatBUS
    {
        private KhenThuongKyLuatDAO ktklDAO;
        private HocSinhDAO hocSinhDAO;
        private PhanLopDAO phanLopDAO;

        public KhenThuongKyLuatBUS()
        {
            ktklDAO = new KhenThuongKyLuatDAO();
            hocSinhDAO = new HocSinhDAO();
            phanLopDAO = new PhanLopDAO();
        }

        #region Validation Methods

        /// <summary>
        /// Validate dữ liệu đầu vào khi thêm/sửa đánh giá
        /// </summary>
        public ValidationResult ValidateKhenThuongKyLuat(
            int loaiDanhGiaIndex,
            int hocKyIndex,
            int lopIndex,
            int hocSinhIndex,
            string noiDung,
            string loaiDanhGia,
            int capKhenThuongIndex,
            string capKhenThuongValue,
            int mucXuLyIndex,
            string mucXuLyValue,
            bool isEditMode)
        {
            // Kiểm tra loại đánh giá
            if (loaiDanhGiaIndex <= 0)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Vui lòng chọn loại đánh giá!",
                    FieldName = "cbLoaiDanhGia"
                };
            }

            // Kiểm tra các trường bắt buộc khi thêm mới
            if (!isEditMode)
            {
                if (hocKyIndex < 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Vui lòng chọn học kỳ!",
                        FieldName = "cbHocKy"
                    };
                }

                if (lopIndex < 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Vui lòng chọn lớp!",
                        FieldName = "cbLop"
                    };
                }

                if (hocSinhIndex < 0)
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Vui lòng chọn học sinh!",
                        FieldName = "cbHocSinh"
                    };
                }
            }

            // Kiểm tra nội dung
            if (string.IsNullOrWhiteSpace(noiDung))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Vui lòng nhập nội dung!",
                    FieldName = "txtNoiDung"
                };
            }

            // Validate theo loại
            if (loaiDanhGia == "Khen thưởng")
            {
                if (capKhenThuongIndex < 0 || capKhenThuongValue == "Cấp khen thưởng")
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Vui lòng chọn cấp khen thưởng hợp lệ!",
                        FieldName = "cbCapKhenThuong"
                    };
                }
            }
            else if (loaiDanhGia == "Kỷ luật")
            {
                if (mucXuLyIndex < 0 || mucXuLyValue == "Mức xử lý")
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Vui lòng chọn mức xử lý hợp lệ!",
                        FieldName = "cbMucXuLy"
                    };
                }
            }

            return new ValidationResult { IsValid = true };
        }

        #endregion

        #region Business Logic Methods

        /// <summary>
        /// Thêm mới khen thưởng/kỷ luật
        /// </summary>
        public OperationResult ThemKhenThuongKyLuat(
            int maHocSinh,
            string loai,
            string noiDung,
            string capKhenThuong,
            string mucXuLy,
            DateTime ngayApDung,
            string nguoiLapID = null)
        {
            try
            {
                // Kiểm tra học sinh có tồn tại không
                var hocSinh = hocSinhDAO.TimHocSinhTheoMa(maHocSinh);
                if (hocSinh == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Học sinh không tồn tại trong hệ thống!"
                    };
                }

                // Tạo DTO
                KhenThuongKyLuatDTO kt = new KhenThuongKyLuatDTO(
                    maHocSinh,
                    loai,
                    noiDung,
                    capKhenThuong,
                    mucXuLy,
                    ngayApDung,
                    nguoiLapID,
                    "Chưa duyệt"
                );

                bool result = ktklDAO.ThemKhenThuongKyLuat(kt);

                return new OperationResult
                {
                    Success = result,
                    Message = result ? $"Thêm {loai.ToLower()} thành công!" : "Không thể thêm đánh giá. Vui lòng thử lại!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message
                };
            }
        }

        /// <summary>
        /// Cập nhật khen thưởng/kỷ luật
        /// </summary>
        public OperationResult CapNhatKhenThuongKyLuat(
            int maKTKL,
            string loai,
            string noiDung,
            string capKhenThuong,
            string mucXuLy,
            DateTime ngayApDung)
        {
            try
            {
                // Kiểm tra bản ghi có tồn tại không
                var existing = ktklDAO.LayTheoMa(maKTKL);
                if (existing == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Không tìm thấy bản ghi cần cập nhật!"
                    };
                }

                bool result = ktklDAO.CapNhatKhenThuongKyLuat(
                    maKTKL,
                    noiDung,
                    capKhenThuong,
                    mucXuLy,
                    ngayApDung
                );

                return new OperationResult
                {
                    Success = result,
                    Message = result ? $"Cập nhật {loai.ToLower()} thành công!" : "Không thể cập nhật đánh giá. Vui lòng thử lại!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message
                };
            }
        }

        /// <summary>
        /// Xóa khen thưởng/kỷ luật
        /// </summary>
        public OperationResult XoaKhenThuongKyLuat(int maKTKL)
        {
            try
            {
                // Kiểm tra bản ghi có tồn tại không
                var existing = ktklDAO.LayTheoMa(maKTKL);
                if (existing == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Không tìm thấy bản ghi cần xóa!"
                    };
                }

                bool result = ktklDAO.XoaKhenThuongKyLuat(maKTKL);

                return new OperationResult
                {
                    Success = result,
                    Message = result ? "Xóa thành công!" : "Không thể xóa. Vui lòng thử lại!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message
                };
            }
        }

        /// <summary>
        /// Duyệt đánh giá
        /// </summary>
        public OperationResult DuyetDanhGia(int maKTKL, string tenHocSinh)
        {
            try
            {
                // Kiểm tra bản ghi
                var existing = ktklDAO.LayTheoMa(maKTKL);
                if (existing == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Không tìm thấy đánh giá cần duyệt!"
                    };
                }

                // Kiểm tra trạng thái hiện tại
                if (existing.TrangThaiDuyet == "Đã duyệt")
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = $"Đánh giá của {tenHocSinh} đã được duyệt rồi!",
                        IsWarning = true
                    };
                }

                // Cập nhật trạng thái
                bool result = ktklDAO.CapNhatTrangThaiDuyet(maKTKL, "Đã duyệt");

                return new OperationResult
                {
                    Success = result,
                    Message = result ? "Duyệt thành công!" : "Không thể duyệt. Vui lòng thử lại!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = "Lỗi: " + ex.Message
                };
            }
        }

        /// <summary>
        /// Lấy danh sách khen thưởng/kỷ luật có lọc
        /// </summary>
        public List<KhenThuongKyLuatDTO> LayDanhSachCoLoc(
            string loai,
            int selectedMaHocKy,
            int selectedMaLop,
            string searchKeyword)
        {
            try
            {
                List<KhenThuongKyLuatDTO> danhSach = ktklDAO.LayDanhSachTheoLoai(loai);

                // Lọc theo học kỳ và lớp
                if (selectedMaHocKy != -1 || selectedMaLop != -1)
                {
                    danhSach = danhSach.Where(kt =>
                        CheckFilterMatch(kt.MaHocSinh, selectedMaHocKy, selectedMaLop)
                    ).ToList();
                }

                // Lọc theo từ khóa tìm kiếm
                if (!string.IsNullOrWhiteSpace(searchKeyword))
                {
                    string keyword = searchKeyword.ToLower();
                    danhSach = danhSach.Where(kt =>
                    {
                        HocSinhDTO hs = hocSinhDAO.TimHocSinhTheoMa(kt.MaHocSinh);
                        string hoTen = hs?.HoTen ?? "";
                        string capKhen = kt.CapKhenThuong ?? "";
                        string mucXuLy = kt.MucXuLy ?? "";
                        string trangThai = kt.TrangThaiDuyet;
                        string ngay = kt.NgayApDung.ToString("dd/MM/yyyy");

                        return hoTen.ToLower().Contains(keyword) ||
                               kt.NoiDung.ToLower().Contains(keyword) ||
                               capKhen.ToLower().Contains(keyword) ||
                               mucXuLy.ToLower().Contains(keyword) ||
                               trangThai.ToLower().Contains(keyword) ||
                               ngay.Contains(keyword);
                    }).ToList();
                }

                return danhSach;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra học sinh có khớp với filter không
        /// </summary>
        private bool CheckFilterMatch(int maHocSinh, int selectedMaHocKy, int selectedMaLop)
        {
            // Nếu không có filter nào, hiển thị tất cả
            if (selectedMaHocKy == -1 && selectedMaLop == -1)
                return true;

            // Lấy danh sách phân lớp của học sinh
            var dsPhanLop = phanLopDAO.LayTatCaPhanLop();
            var phanLopHS = dsPhanLop.Where(pl => pl.maHocSinh == maHocSinh);

            // Nếu chỉ filter theo học kỳ
            if (selectedMaHocKy != -1 && selectedMaLop == -1)
            {
                return phanLopHS.Any(pl => pl.maHocKy == selectedMaHocKy);
            }

            // Nếu chỉ filter theo lớp
            if (selectedMaHocKy == -1 && selectedMaLop != -1)
            {
                return phanLopHS.Any(pl => pl.maLop == selectedMaLop);
            }

            // Filter theo cả học kỳ và lớp
            return phanLopHS.Any(pl => pl.maHocKy == selectedMaHocKy && pl.maLop == selectedMaLop);
        }

        /// <summary>
        /// Lấy thông tin chi tiết
        /// </summary>
        public KhenThuongKyLuatDTO LayTheoMa(int maKTKL)
        {
            return ktklDAO.LayTheoMa(maKTKL);
        }

        #endregion
    }

    #region Result Classes

    /// <summary>
    /// Kết quả validation
    /// </summary>
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public string FieldName { get; set; }
    }

    /// <summary>
    /// Kết quả thực hiện operation
    /// </summary>
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool IsWarning { get; set; } = false;
    }

    #endregion
}