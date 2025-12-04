using System;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.Utils;

namespace Student_Management_System_CSharp_SGU2025.Utils
{
    /// <summary>
    /// Helper class để kiểm tra và áp dụng phân quyền
    /// </summary>
    public static class PermissionHelper
    {
        private static PhanQuyenBUS phanQuyenBUS = new PhanQuyenBUS();

        // Danh sách mã chức năng
        public const string QLDIEM = "qldiem";
        public const string QLHOCSINH = "qlhocsinh";
        public const string QLLOPHOC = "qllophoc";
        public const string QLGIAOVIEN = "qlgiaovien";
        public const string QLMONHOC = "qlmonhoc";
        public const string QLPHANCONG = "qlphancong";
        public const string QLTKB = "qltkb";
        public const string QLHANHKIEM = "qlhanhkiem";
        public const string QLBAOCAO = "qlbaocao";
        public const string QLTAIKHOAN = "qltaikhoan";
        public const string QLTHONGBAO = "qlthongbao";
        public const string QLNAMHOC = "qlnamhoc";
        public const string QLCAIDAT = "qlcaidat";
        public const string QLDANHGIA = "qldanhgia";
        public const string QLXEPLOAI = "qlxeploai";
        public const string QLYEUCAUCHUYENLOP = "qlyeucau_chuyenlop"; // ✅ Quản lý yêu cầu chuyển lớp

        // Danh sách hành động 
        public const string READ = "read";
        public const string CREATE = "create";
        public const string UPDATE = "update";
        public const string DELETE = "delete";

        /// <summary>
        /// Kiểm tra người dùng hiện tại có quyền hay không
        /// </summary>
        public static bool HasPermission(string maChucNang, string hanhDong)
        {
            if (!SessionManager.IsLoggedIn())
                return false;

            try
            {
                return phanQuyenBUS.KiemTraQuyenNguoiDung(
                    SessionManager.TenDangNhap,
                    maChucNang,
                    hanhDong);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra có quyền truy cập chức năng (dựa vào vai trò có chức năng đó hay không)
        /// </summary>
        public static bool HasAccessToFunction(string maChucNang)
        {
            if (!SessionManager.IsLoggedIn())
                return false;

            try
            {
                // Kiểm tra xem vai trò có được gán chức năng này không
                var danhSachVaiTro = phanQuyenBUS.GetVaiTroByNguoiDung(SessionManager.TenDangNhap);

                foreach (var maVaiTro in danhSachVaiTro)
                {
                    var danhSachChucNang = phanQuyenBUS.GetTenChucNangByVaiTro(maVaiTro);
                    if (danhSachChucNang.Count > 0)
                    {
                        // Nếu vai trò có bất kỳ quyền nào trên chức năng này
                        if (HasPermission(maChucNang, READ) ||      // ✅ THÊM READ
                             HasPermission(maChucNang, CREATE) ||
                             HasPermission(maChucNang, UPDATE) ||
                             HasPermission(maChucNang, DELETE))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Ẩn/hiện button dựa trên quyền
        /// </summary>
        public static void SetButtonPermission(Control button, string maChucNang, string hanhDong)
        {
            if (button == null) return;

            bool hasPermission = HasPermission(maChucNang, hanhDong);
            button.Visible = hasPermission;
            button.Enabled = hasPermission;
        }

        /// <summary>
        /// Kiểm tra quyền truy cập - nếu không có thì thông báo và return false
        /// </summary>
        public static bool CheckAccessPermission(string maChucNang, string tenChucNang)
        {
            if (!HasAccessToFunction(maChucNang))
            {
                MessageBox.Show(
                    $"Bạn không có quyền truy cập chức năng '{tenChucNang}'!",
                    "Không có quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra quyền thêm - nếu không có thì thông báo và return false
        /// </summary>
        public static bool CheckCreatePermission(string maChucNang, string tenChucNang)
        {
            if (!HasPermission(maChucNang, CREATE))
            {
                MessageBox.Show(
                    $"Bạn không có quyền thêm mới trong chức năng '{tenChucNang}'!",
                    "Không có quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra quyền sửa - nếu không có thì thông báo và return false
        /// </summary>
        public static bool CheckUpdatePermission(string maChucNang, string tenChucNang)
        {
            if (!HasPermission(maChucNang, UPDATE))
            {
                MessageBox.Show(
                    $"Bạn không có quyền chỉnh sửa trong chức năng '{tenChucNang}'!",
                    "Không có quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra quyền xóa - nếu không có thì thông báo và return false
        /// </summary>
        public static bool CheckDeletePermission(string maChucNang, string tenChucNang)
        {
            if (!HasPermission(maChucNang, DELETE))
            {
                MessageBox.Show(
                    $"Bạn không có quyền xóa trong chức năng '{tenChucNang}'!",
                    "Không có quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Áp dụng phân quyền cho form Điểm Số
        /// </summary>
        public static void ApplyPermissionDiemSo(
            Control btnThemDiem,
            Control btnSuaDiem,
            Control btnDiemSo)
        {
            // Ẩn/hiện nút Thêm điểm
            SetButtonPermission(btnThemDiem, QLDIEM, CREATE);

            // Ẩn/hiện nút Sửa điểm
            SetButtonPermission(btnSuaDiem, QLDIEM, UPDATE);

            // Ẩn/hiện nút Điểm số trên sidebar
            if (btnDiemSo != null)
            {
                bool hasAccess = HasAccessToFunction(QLDIEM);
                btnDiemSo.Visible = hasAccess;
                btnDiemSo.Enabled = hasAccess;
            }
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Học Sinh
        /// </summary>
        public static void ApplyPermissionHocSinh(
            Control btnThemHocSinh,
            Control btnThemPhuHuynh,
            DataGridView tableHocSinh,
            DataGridView tablePhuHuynh)
        {
            // Ẩn/hiện nút Thêm học sinh
            SetButtonPermission(btnThemHocSinh, QLHOCSINH, CREATE);

            // Ẩn/hiện nút Thêm phụ huynh
            SetButtonPermission(btnThemPhuHuynh, QLHOCSINH, CREATE);

            // Vô hiệu hóa icon Sửa/Xóa trên DataGridView (xử lý trong CellPainting)
            if (tableHocSinh != null)
            {
                tableHocSinh.Tag = new
                {
                    CanUpdate = HasPermission(QLHOCSINH, UPDATE),
                    CanDelete = HasPermission(QLHOCSINH, DELETE)
                };
            }

            if (tablePhuHuynh != null)
            {
                tablePhuHuynh.Tag = new
                {
                    CanUpdate = HasPermission(QLHOCSINH, UPDATE),
                    CanDelete = HasPermission(QLHOCSINH, DELETE)
                };
            }
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Đánh Giá
        /// </summary>
        public static void ApplyPermissionDanhGia(
            Control btnAddDanhGia,
            Control btnDuyetDanhGia,
            DataGridView tbKhenThuong,
            DataGridView tbKyLuat)
        {
            // Ẩn/hiện nút Thêm đánh giá
            SetButtonPermission(btnAddDanhGia, QLDANHGIA, CREATE);

            // Nút Duyệt cần quyền UPDATE
            SetButtonPermission(btnDuyetDanhGia, QLDANHGIA, UPDATE);

            // Vô hiệu hóa icon Sửa/Xóa trên DataGridView
            if (tbKhenThuong != null)
            {
                tbKhenThuong.Tag = new
                {
                    CanUpdate = HasPermission(QLDANHGIA, UPDATE),
                    CanDelete = HasPermission(QLDANHGIA, DELETE)
                };
            }

            if (tbKyLuat != null)
            {
                tbKyLuat.Tag = new
                {
                    CanUpdate = HasPermission(QLDANHGIA, UPDATE),
                    CanDelete = HasPermission(QLDANHGIA, DELETE)
                };
            }
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Năm học và Học kỳ
        /// </summary>
        public static void ApplyPermissionNamHoc(
            Control btnAddNamHoc,
            Control btnAddHocKy,
            DataGridView tbNamHoc,
            DataGridView tbHocKy)
        {
            // Ẩn/hiện nút Thêm năm học
            SetButtonPermission(btnAddNamHoc, QLNAMHOC, CREATE);

            // Ẩn/hiện nút Thêm học kỳ
            SetButtonPermission(btnAddHocKy, QLNAMHOC, CREATE);

            // ✅ Set Tag cho DataGridView Năm học (CHỈ CÓ CREATE VÀ DELETE)
            if (tbNamHoc != null)
            {
                tbNamHoc.Tag = new
                {
                    CanCreate = HasPermission(QLNAMHOC, CREATE),
                    CanUpdate = false, // ❌ KHÔNG CÓ QUYỀN SỬA
                    CanDelete = HasPermission(QLNAMHOC, DELETE)
                };
            }

            // ✅ Set Tag cho DataGridView Học kỳ (CHỈ CÓ CREATE VÀ DELETE)
            if (tbHocKy != null)
            {
                tbHocKy.Tag = new
                {
                    CanCreate = HasPermission(QLNAMHOC, CREATE),
                    CanUpdate = false, // ❌ KHÔNG CÓ QUYỀN SỬA
                    CanDelete = HasPermission(QLNAMHOC, DELETE)
                };
            }
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Môn học
        /// </summary>
        public static void ApplyPermissionMonHoc(
            Control btnThemMonHoc,
            Control btnSua,
            Control btnXoa)
        {
            // Ẩn/hiện nút Thêm môn học
            SetButtonPermission(btnThemMonHoc, QLMONHOC, CREATE);

            // Ẩn/hiện nút Sửa môn học
            SetButtonPermission(btnSua, QLMONHOC, UPDATE);

            // Ẩn/hiện nút Xóa môn học
            SetButtonPermission(btnXoa, QLMONHOC, DELETE);
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Phân công giảng dạy
        /// </summary>
        public static void ApplyPermissionPhanCong(
            Control btnPhanCongMoi,
            Control btnAutoPhanCong,
            DataGridView dgvPhanCong)
        {
            // Ẩn/hiện nút Thêm phân công mới (thủ công)
            SetButtonPermission(btnPhanCongMoi, QLPHANCONG, CREATE);

            // Ẩn/hiện nút Auto phân công
            SetButtonPermission(btnAutoPhanCong, QLPHANCONG, CREATE);

            // ✅ Set Tag cho DataGridView (CHỈ CÓ DELETE, KHÔNG CÓ UPDATE)
            if (dgvPhanCong != null)
            {
                dgvPhanCong.Tag = new
                {
                    CanCreate = HasPermission(QLPHANCONG, CREATE),
                    CanUpdate = false, // ❌ KHÔNG CÓ QUYỀN SỬA
                    CanDelete = HasPermission(QLPHANCONG, DELETE)
                };
            }
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Lớp Học
        /// </summary>
        public static void ApplyPermissionLopHoc(
            Control btnThemLop,
            DataGridView dgvLop)
        {
            // Ẩn/hiện nút Thêm lớp học
            SetButtonPermission(btnThemLop, QLLOPHOC, CREATE);

            // ✅ Set Tag cho DataGridView để kiểm tra quyền Sửa/Xóa
            if (dgvLop != null)
            {
                dgvLop.Tag = new
                {
                    CanUpdate = HasPermission(QLLOPHOC, UPDATE),
                    CanDelete = HasPermission(QLLOPHOC, DELETE)
                };
            }
        }
        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Hạnh Kiểm
        /// </summary>
        public static void ApplyPermissionHanhKiem(
            Control btnXepHanhKiem,
            Control btnLuuHanhKiem)
        {
            // Ẩn/hiện nút Xếp hạnh kiểm tự động (CREATE)
            SetButtonPermission(btnXepHanhKiem, QLHANHKIEM, CREATE);

            // Ẩn/hiện nút Lưu hạnh kiểm (UPDATE)
            SetButtonPermission(btnLuuHanhKiem, QLHANHKIEM, UPDATE);
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Xếp Loại
        /// </summary>
        public static void ApplyPermissionXepLoai(Control btnLuuTongKet)
        {
            // Ẩn/hiện nút Lưu tổng kết (UPDATE)
            SetButtonPermission(btnLuuTongKet, QLXEPLOAI, UPDATE);
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Thời khóa biểu
        /// </summary>
        public static void ApplyPermissionThoiKhoaBieu(
            Control btnSapXepTuDong,
            Control btnLuuDiem,
            Control btnXoa)
        {
            // Ẩn/hiện nút Sắp xếp tự động (CREATE)
            SetButtonPermission(btnSapXepTuDong, QLTKB, CREATE);

            // Nút Lưu TKB cần quyền CREATE (vì là publish TKB mới tạo)
            SetButtonPermission(btnLuuDiem, QLTKB, CREATE);

            // Ẩn/hiện nút Xóa TKB (DELETE)
            SetButtonPermission(btnXoa, QLTKB, DELETE);
        }

        /// <summary>
        /// ✅ Áp dụng phân quyền cho form Tài khoản
        /// </summary>
        public static void ApplyPermissionTaiKhoan(
            Control btnAddAcc,
            Control btnVaiTro,
            DataGridView tbTaiKhoan)
        {
            // Ẩn/hiện nút Thêm tài khoản (CREATE)
            SetButtonPermission(btnAddAcc, QLTAIKHOAN, CREATE);

            // Ẩn/hiện nút Quản lý vai trò (CREATE - vì tạo vai trò mới)
            SetButtonPermission(btnVaiTro, QLTAIKHOAN, CREATE);

            // ✅ Set Tag cho DataGridView để kiểm tra quyền Sửa/Xóa/Khóa
            if (tbTaiKhoan != null)
            {
                tbTaiKhoan.Tag = new
                {
                    CanUpdate = HasPermission(QLTAIKHOAN, UPDATE),
                    CanDelete = HasPermission(QLTAIKHOAN, DELETE)
                };
            }
        }

        /// <summary>
        /// ✅ Kiểm tra quyền cho icon trên DataGridView (gọi trong CellClick)
        /// </summary>
        public static bool CheckDataGridIconPermission(DataGridView dgv, string iconType, string tenChucNang)
        {
            string hanhDong = iconType == "edit" ? UPDATE : DELETE;
            string tenHanhDong = iconType == "edit" ? "chỉnh sửa" : "xóa";

            dynamic permissions = dgv.Tag;
            if (permissions == null)
                return true; // Nếu chưa set permission thì cho phép (backward compatible)

            bool canPerform = iconType == "edit" ? permissions.CanUpdate : permissions.CanDelete;

            if (!canPerform)
            {
                MessageBox.Show(
                    $"Bạn không có quyền {tenHanhDong} trong chức năng '{tenChucNang}'!",
                    "Không có quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}