using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    /// <summary>
    /// Business Logic Layer cho Thông Báo
    /// </summary>
    public class ThongBaoBUS
    {
        private ThongBaoDAO thongBaoDAO;
        private NguoiDungDAO nguoiDungDAO;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;

        public ThongBaoBUS()
        {
            thongBaoDAO = new ThongBaoDAO();
            nguoiDungDAO = new NguoiDungDAO();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
        }

        #region Validation Methods

        /// <summary>
        /// Validate dữ liệu khi thêm/sửa thông báo
        /// </summary>
        public ValidationResult ValidateThongBao(
            string tieuDe,
            string noiDung,
            string loaiThongBao,
            string phamVi,
            string maNguoiTao,
            int? maLop = null,
            int? maKhoi = null,
            string maVaiTro = null)
        {
            // Kiểm tra tiêu đề
            if (string.IsNullOrWhiteSpace(tieuDe))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Vui lòng nhập tiêu đề thông báo!",
                    FieldName = "txtTieuDe"
                };
            }

            if (tieuDe.Length > 255)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Tiêu đề không được quá 255 ký tự!",
                    FieldName = "txtTieuDe"
                };
            }

            // Kiểm tra nội dung
            if (string.IsNullOrWhiteSpace(noiDung))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Vui lòng nhập nội dung thông báo!",
                    FieldName = "txtNoiDung"
                };
            }

            // Kiểm tra loại thông báo
            if (string.IsNullOrWhiteSpace(loaiThongBao))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Vui lòng chọn loại thông báo!",
                    FieldName = "cbLoaiThongBao"
                };
            }

            // Kiểm tra phạm vi
            if (string.IsNullOrWhiteSpace(phamVi))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Vui lòng chọn phạm vi gửi!",
                    FieldName = "cbPhamVi"
                };
            }

            // Validate theo phạm vi
            switch (phamVi)
            {
                case "LOP":
                    if (!maLop.HasValue || maLop.Value <= 0)
                    {
                        return new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = "Vui lòng chọn lớp cụ thể!",
                            FieldName = "cbLop"
                        };
                    }
                    break;

                case "KHOI":
                    if (!maKhoi.HasValue || maKhoi.Value <= 0)
                    {
                        return new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = "Vui lòng chọn khối cụ thể!",
                            FieldName = "cbKhoi"
                        };
                    }
                    break;

                case "VAI_TRO":
                    if (string.IsNullOrWhiteSpace(maVaiTro))
                    {
                        return new ValidationResult
                        {
                            IsValid = false,
                            ErrorMessage = "Vui lòng chọn vai trò!",
                            FieldName = "cbVaiTro"
                        };
                    }
                    break;
            }

            // Kiểm tra người tạo
            if (string.IsNullOrWhiteSpace(maNguoiTao))
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Không xác định được người tạo thông báo!",
                    FieldName = ""
                };
            }

            return new ValidationResult { IsValid = true };
        }

        /// <summary>
        /// Kiểm tra quyền gửi thông báo theo vai trò
        /// </summary>
        public ValidationResult CheckPermissionToSend(string tenDangNhap, string phamVi, int? maLop = null)
        {
            // Lấy vai trò của người dùng
            var vaiTros = new PhanQuyenDAO().GetVaiTroByNguoiDung(tenDangNhap);
            
            if (vaiTros.Contains("admin"))
            {
                // Admin có toàn quyền
                return new ValidationResult { IsValid = true };
            }

            if (vaiTros.Contains("teacher"))
            {
                // Giáo viên chỉ được gửi theo lớp
                if (phamVi == "LOP")
                {
                    // TODO: Kiểm tra giáo viên có phụ trách lớp này không
                    return new ValidationResult { IsValid = true };
                }
                else if (phamVi == "VAI_TRO")
                {
                    // Giáo viên có thể gửi cho học sinh
                    return new ValidationResult { IsValid = true };
                }
                else
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Giáo viên chỉ được gửi thông báo theo lớp hoặc cho học sinh!",
                        FieldName = ""
                    };
                }
            }

            // Học sinh và phụ huynh không được gửi thông báo
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Bạn không có quyền gửi thông báo!",
                FieldName = ""
            };
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Thêm thông báo mới
        /// </summary>
        public OperationResult ThemThongBao(ThongBaoDTO thongBao)
        {
            try
            {
                // Validate dữ liệu
                var validationResult = ValidateThongBao(
                    thongBao.TieuDe,
                    thongBao.NoiDung,
                    thongBao.LoaiThongBao,
                    thongBao.PhamVi,
                    thongBao.MaNguoiTao,
                    thongBao.MaLop,
                    thongBao.MaKhoi,
                    thongBao.MaVaiTroNhan
                );

                if (!validationResult.IsValid)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = validationResult.ErrorMessage
                    };
                }

                // Kiểm tra quyền
                var permissionResult = CheckPermissionToSend(
                    thongBao.MaNguoiTao,
                    thongBao.PhamVi,
                    thongBao.MaLop
                );

                if (!permissionResult.IsValid)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = permissionResult.ErrorMessage
                    };
                }

                // Thực hiện thêm
                int maThongBao = thongBaoDAO.ThemThongBao(thongBao);

                if (maThongBao > 0)
                {
                    // Tự động gửi thông báo đến người nhận dựa trên phạm vi
                    int soNguoiNhan = 0;
                    try
                    {
                        soNguoiNhan = GuiThongBaoTheoPhamVi(maThongBao, thongBao);
                    }
                    catch (Exception ex)
                    {
                        // Log lỗi nhưng không fail toàn bộ operation
                        Console.WriteLine("Lỗi khi gửi thông báo: " + ex.Message);
                    }

                    return new OperationResult
                    {
                        Success = true,
                        Message = $"Thêm thông báo thành công! Đã gửi đến {soNguoiNhan} người nhận.",
                        Data = maThongBao
                    };
                }

                return new OperationResult
                {
                    Success = false,
                    Message = "Không thể thêm thông báo. Vui lòng thử lại!"
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
        /// Cập nhật thông báo
        /// </summary>
        public OperationResult CapNhatThongBao(ThongBaoDTO thongBao)
        {
            try
            {
                // Validate dữ liệu
                var validationResult = ValidateThongBao(
                    thongBao.TieuDe,
                    thongBao.NoiDung,
                    thongBao.LoaiThongBao,
                    thongBao.PhamVi,
                    thongBao.MaNguoiTao,
                    thongBao.MaLop,
                    thongBao.MaKhoi,
                    thongBao.MaVaiTroNhan
                );

                if (!validationResult.IsValid)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = validationResult.ErrorMessage
                    };
                }

                // Kiểm tra thông báo có tồn tại không
                var existing = thongBaoDAO.LayThongBaoTheoMa(thongBao.MaThongBao);
                if (existing == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Không tìm thấy thông báo cần cập nhật!"
                    };
                }

                // Thực hiện cập nhật
                bool result = thongBaoDAO.CapNhatThongBao(thongBao);

                return new OperationResult
                {
                    Success = result,
                    Message = result ? "Cập nhật thông báo thành công!" : "Không thể cập nhật. Vui lòng thử lại!"
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
        /// Xóa thông báo (soft delete)
        /// </summary>
        public OperationResult XoaThongBao(int maThongBao, string tenDangNhap)
        {
            try
            {
                // Kiểm tra thông báo có tồn tại không
                var thongBao = thongBaoDAO.LayThongBaoTheoMa(maThongBao);
                if (thongBao == null)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Không tìm thấy thông báo cần xóa!"
                    };
                }

                // Kiểm tra quyền xóa (chỉ người tạo hoặc admin mới được xóa)
                var vaiTros = new PhanQuyenDAO().GetVaiTroByNguoiDung(tenDangNhap);
                if (thongBao.MaNguoiTao != tenDangNhap && !vaiTros.Contains("admin"))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Bạn không có quyền xóa thông báo này!"
                    };
                }

                // Thực hiện xóa (soft delete)
                bool result = thongBaoDAO.XoaThongBao(maThongBao);

                return new OperationResult
                {
                    Success = result,
                    Message = result ? "Xóa thông báo thành công!" : "Không thể xóa. Vui lòng thử lại!"
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

        #endregion

        #region Query Methods

        /// <summary>
        /// Lấy danh sách thông báo theo người dùng
        /// </summary>
        public List<ThongBaoDTO> LayDanhSachThongBao(
            string tenDangNhap,
            string loaiThongBao = null,
            bool? daDoc = null,
            string timKiem = null,
            int pageNumber = 1,
            int pageSize = 50)
        {
            try
            {
                // ✅ Kiểm tra quyền CREATE/UPDATE/DELETE để cho phép xem tất cả thông báo
                var phanQuyenBUS = new PhanQuyenBUS();
                bool hasCreate = phanQuyenBUS.KiemTraQuyenNguoiDung(tenDangNhap, "qlthongbao", "create");
                bool hasUpdate = phanQuyenBUS.KiemTraQuyenNguoiDung(tenDangNhap, "qlthongbao", "update");
                bool hasDelete = phanQuyenBUS.KiemTraQuyenNguoiDung(tenDangNhap, "qlthongbao", "delete");
                bool hasManagePermission = hasCreate || hasUpdate || hasDelete;
                
                // Kiểm tra nếu là admin hoặc có quyền quản lý (CREATE/UPDATE/DELETE), lấy tất cả thông báo
                var vaiTros = new PhanQuyenDAO().GetVaiTroByNguoiDung(tenDangNhap);
                bool isAdminOrGiaoVu = vaiTros != null && vaiTros.Any(v => v.Equals("ADMIN", StringComparison.OrdinalIgnoreCase) || v.Equals("GIAO_VU", StringComparison.OrdinalIgnoreCase));
                
                if (isAdminOrGiaoVu || hasManagePermission)
                {
                    // Admin hoặc người có quyền quản lý xem tất cả thông báo
                    var tatCaThongBao = thongBaoDAO.LayTatCaThongBao("HIEN_THI");
                    
                    // Thêm thông tin đã đọc cho admin (nếu có trong NguoiNhanThongBao)
                    // Lấy tất cả thông tin đã đọc trong một query
                    string sqlDoc = @"SELECT MaThongBao, DaDoc, NgayDoc FROM NguoiNhanThongBao 
                        WHERE TenDangNhap = @tenDangNhap";
                    
                    Dictionary<int, (bool DaDoc, DateTime? NgayDoc)> dictDaDoc = new Dictionary<int, (bool, DateTime?)>();
                    
                    using (var conn = DAO.ConnectDatabase.ConnectionDatabase.GetConnection())
                    {
                        try
                        {
                            conn.Open();
                            using (var cmd = new MySqlCommand(sqlDoc, conn))
                            {
                                cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        int maTB = reader.GetInt32("MaThongBao");
                                        bool isDaDoc = reader.GetBoolean("DaDoc");
                                        DateTime? ngayDoc = reader.IsDBNull(reader.GetOrdinal("NgayDoc")) 
                                            ? (DateTime?)null 
                                            : reader.GetDateTime("NgayDoc");
                                        dictDaDoc[maTB] = (isDaDoc, ngayDoc);
                                    }
                                }
                            }
                        }
                        catch { }
                        finally
                        {
                            DAO.ConnectDatabase.ConnectionDatabase.CloseConnection(conn);
                        }
                    }
                    
                    // Gán thông tin đã đọc cho từng thông báo
                    foreach (var tb in tatCaThongBao)
                    {
                        if (dictDaDoc.ContainsKey(tb.MaThongBao))
                        {
                            tb.DaDoc = dictDaDoc[tb.MaThongBao].DaDoc;
                            tb.NgayDoc = dictDaDoc[tb.MaThongBao].NgayDoc;
                        }
                        else
                        {
                            tb.DaDoc = false;
                            tb.NgayDoc = null;
                        }
                    }

                    // Filter theo loại
                    if (!string.IsNullOrEmpty(loaiThongBao))
                    {
                        tatCaThongBao = tatCaThongBao.Where(tb => tb.LoaiThongBao == loaiThongBao).ToList();
                    }

                    // Filter theo trạng thái đọc
                    if (daDoc.HasValue)
                    {
                        tatCaThongBao = tatCaThongBao.Where(tb => tb.DaDoc == daDoc.Value).ToList();
                    }

                    // Tìm kiếm
                    if (!string.IsNullOrWhiteSpace(timKiem))
                    {
                        string keyword = timKiem.ToLower();
                        tatCaThongBao = tatCaThongBao.Where(tb =>
                            tb.TieuDe.ToLower().Contains(keyword) ||
                            (tb.NoiDung != null && tb.NoiDung.ToLower().Contains(keyword)) ||
                            (tb.DoiTuongNhan != null && tb.DoiTuongNhan.ToLower().Contains(keyword))
                        ).ToList();
                    }

                    // Phân trang
                    return tatCaThongBao.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    // Kiểm tra nếu là giáo viên
                    var vaiTrosUser = new PhanQuyenDAO().GetVaiTroByNguoiDung(tenDangNhap);
                    bool isTeacher = vaiTrosUser != null && vaiTrosUser.Any(v => v.Equals("teacher", StringComparison.OrdinalIgnoreCase));
                    
                    if (isTeacher)
                    {
                        // Giáo viên: Lấy thông báo từ NguoiNhanThongBao + thông báo gửi cho giáo viên (VAI_TRO = teacher)
                        var danhSachTuNguoiNhan = thongBaoDAO.LayDanhSachThongBaoTheoNguoiDung(
                            tenDangNhap,
                            loaiThongBao,
                            daDoc,
                            timKiem,
                            1,
                            1000  // Lấy tất cả để merge
                        );
                        
                        // Lấy thêm thông báo gửi cho giáo viên (PhamVi = VAI_TRO, MaVaiTroNhan = teacher)
                        // nhưng chưa có trong NguoiNhanThongBao (có thể do thông báo cũ)
                        var tatCaThongBao = thongBaoDAO.LayTatCaThongBao("HIEN_THI");
                        var thongBaoChoGiaoVien = tatCaThongBao
                            .Where(tb => 
                                (tb.PhamVi == "VAI_TRO" && tb.MaVaiTroNhan == "teacher") ||
                                (tb.PhamVi == "ALL") ||
                                (tb.DoiTuongNhan != null && (
                                    tb.DoiTuongNhan.Contains("Giáo viên") ||
                                    tb.DoiTuongNhan.Contains("Toàn trường") ||
                                    tb.DoiTuongNhan.Contains("Tất cả")
                                ))
                            )
                            .Where(tb => !danhSachTuNguoiNhan.Any(d => d.MaThongBao == tb.MaThongBao)) // Loại bỏ đã có
                            .ToList();
                        
                        // Merge 2 danh sách
                        var danhSachMerged = danhSachTuNguoiNhan.Concat(thongBaoChoGiaoVien).ToList();
                        
                        Console.WriteLine($"[ThongBaoBUS] Giáo viên {tenDangNhap}: {danhSachTuNguoiNhan.Count} từ NguoiNhanThongBao + {thongBaoChoGiaoVien.Count} thông báo cho giáo viên = {danhSachMerged.Count} tổng");
                        
                        // Filter theo loại
                        if (!string.IsNullOrEmpty(loaiThongBao))
                        {
                            danhSachMerged = danhSachMerged.Where(tb => tb.LoaiThongBao == loaiThongBao).ToList();
                        }

                        // Filter theo trạng thái đọc
                        if (daDoc.HasValue)
                        {
                            danhSachMerged = danhSachMerged.Where(tb => tb.DaDoc == daDoc.Value).ToList();
                        }

                        // Tìm kiếm
                        if (!string.IsNullOrWhiteSpace(timKiem))
                        {
                            string keyword = timKiem.ToLower();
                            danhSachMerged = danhSachMerged.Where(tb =>
                                tb.TieuDe.ToLower().Contains(keyword) ||
                                (tb.NoiDung != null && tb.NoiDung.ToLower().Contains(keyword)) ||
                                (tb.DoiTuongNhan != null && tb.DoiTuongNhan.ToLower().Contains(keyword))
                            ).ToList();
                        }

                        // Phân trang
                        return danhSachMerged.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                    else
                    {
                        // User thường (học sinh, phụ huynh) chỉ xem thông báo của mình
                        return thongBaoDAO.LayDanhSachThongBaoTheoNguoiDung(
                            tenDangNhap,
                            loaiThongBao,
                            daDoc,
                            timKiem,
                            pageNumber,
                            pageSize
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách thông báo: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách thông báo đã gửi (dành cho Admin/GV)
        /// </summary>
        public List<ThongBaoDTO> LayDanhSachThongBaoDaGui(
            string tenDangNhap,
            string loaiThongBao = null,
            string timKiem = null,
            int pageNumber = 1,
            int pageSize = 50)
        {
            try
            {
                return thongBaoDAO.LayDanhSachThongBaoDaGui(
                    tenDangNhap,
                    loaiThongBao,
                    timKiem,
                    pageNumber,
                    pageSize
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách thông báo đã gửi: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy chi tiết thông báo
        /// </summary>
        public ThongBaoDTO LayChiTietThongBao(int maThongBao, string tenDangNhap)
        {
            try
            {
                var thongBao = thongBaoDAO.LayThongBaoChiTiet(maThongBao, tenDangNhap);
                
                // Đánh dấu đã đọc khi xem chi tiết
                if (thongBao != null && !thongBao.DaDoc)
                {
                    thongBaoDAO.DanhDauDaDoc(maThongBao, tenDangNhap);
                }

                return thongBao;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chi tiết thông báo: " + ex.Message);
            }
        }

        /// <summary>
        /// Đếm số thông báo chưa đọc
        /// </summary>
        public int DemThongBaoChuaDoc(string tenDangNhap)
        {
            try
            {
                return thongBaoDAO.DemThongBaoChuaDoc(tenDangNhap);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đếm thông báo chưa đọc: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy thống kê thông báo
        /// </summary>
        public ThongKeThongBaoDTO LayThongKe(string tenDangNhap)
        {
            try
            {
                return thongBaoDAO.LayThongKeThongBao(tenDangNhap);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thống kê thông báo: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách loại thông báo
        /// </summary>
        public List<LoaiThongBaoDTO> LayDanhSachLoaiThongBao()
        {
            try
            {
                return thongBaoDAO.LayDanhSachLoaiThongBao();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách loại thông báo: " + ex.Message);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Tạo text mô tả đối tượng nhận
        /// </summary>
        public string TaoTextDoiTuongNhan(
            string phamVi,
            string maVaiTro = null,
            int? maLop = null,
            int? maKhoi = null)
        {
            switch (phamVi)
            {
                case "ALL":
                    return "Toàn trường";

                case "VAI_TRO":
                    if (maVaiTro == "teacher") return "Giáo viên";
                    if (maVaiTro == "student") return "Học sinh";
                    if (maVaiTro == "parent") return "Phụ huynh";
                    return "Theo vai trò";

                case "LOP":
                    if (maLop.HasValue)
                    {
                        var lop = lopHocBUS.LayLopTheoId(maLop.Value);
                        return lop != null ? $"Lớp {lop.tenLop}" : "Theo lớp";
                    }
                    return "Theo lớp";

                case "KHOI":
                    if (maKhoi.HasValue)
                    {
                        return $"Khối {maKhoi.Value}";
                    }
                    return "Theo khối";

                case "CA_NHAN":
                    return "Cá nhân";

                default:
                    return phamVi;
            }
        }

        /// <summary>
        /// Tổng số thông báo (có lọc)
        /// </summary>
        public int DemTongThongBao(
            string tenDangNhap,
            string loaiThongBao = null,
            bool? daDoc = null,
            string timKiem = null)
        {
            try
            {
                return thongBaoDAO.DemTongThongBao(tenDangNhap, loaiThongBao, daDoc, timKiem);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đếm tổng thông báo: " + ex.Message);
            }
        }

        /// <summary>
        /// Gửi thông báo đến người nhận dựa trên phạm vi
        /// </summary>
        private int GuiThongBaoTheoPhamVi(int maThongBao, ThongBaoDTO thongBao)
        {
            int soNguoiNhan = 0;

            switch (thongBao.PhamVi)
            {
                case "ALL":
                    // Gửi toàn trường
                    soNguoiNhan = thongBaoDAO.GuiToanTruong(maThongBao);
                    break;

                case "VAI_TRO":
                    // Gửi theo vai trò
                    if (!string.IsNullOrEmpty(thongBao.MaVaiTroNhan))
                    {
                        soNguoiNhan = thongBaoDAO.GuiTheoVaiTro(maThongBao, thongBao.MaVaiTroNhan);
                        
                        // Nếu gửi cho học sinh, cũng gửi cho phụ huynh của họ
                        if (thongBao.MaVaiTroNhan == "student")
                        {
                            // TODO: Thêm logic gửi cho phụ huynh
                            // soNguoiNhan += thongBaoDAO.GuiChoPhuHuynhCuaHocSinh(maThongBao);
                        }
                    }
                    break;

                case "LOP":
                    // Gửi theo lớp - cần học kỳ hiện tại
                    if (thongBao.MaLop.HasValue)
                    {
                        // Lấy học kỳ hiện tại
                        var dsHocKy = hocKyBUS.DocDSHocKy();
                        var hocKyHienTai = dsHocKy.FirstOrDefault(hk => hk.TrangThai == "Đang diễn ra");
                        
                        if (hocKyHienTai != null)
                        {
                            // Gửi cho học sinh trong lớp
                            soNguoiNhan = thongBaoDAO.GuiTheoLop(maThongBao, thongBao.MaLop.Value, hocKyHienTai.MaHocKy);
                            
                            // TODO: Gửi cho phụ huynh của học sinh trong lớp
                            // soNguoiNhan += thongBaoDAO.GuiChoPhuHuynhCuaLop(maThongBao, thongBao.MaLop.Value, hocKyHienTai.MaHocKy);
                        }
                        else
                        {
                            throw new Exception("Không tìm thấy học kỳ đang diễn ra!");
                        }
                    }
                    break;

                case "KHOI":
                    // Gửi theo khối - cần học kỳ hiện tại
                    if (thongBao.MaKhoi.HasValue)
                    {
                        var dsHocKy = hocKyBUS.DocDSHocKy();
                        var hocKyHienTai = dsHocKy.FirstOrDefault(hk => hk.TrangThai == "Đang diễn ra");
                        
                        if (hocKyHienTai != null)
                        {
                            soNguoiNhan = thongBaoDAO.GuiTheoKhoi(maThongBao, thongBao.MaKhoi.Value, hocKyHienTai.MaHocKy);
                            
                            // TODO: Gửi cho phụ huynh
                        }
                        else
                        {
                            throw new Exception("Không tìm thấy học kỳ đang diễn ra!");
                        }
                    }
                    break;
            }

            return soNguoiNhan;
        }

        #endregion
    }
}
