using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class CaiDatBUS
    {
        private HocSinhDAO hocSinhDAO;
        private PhuHuynhDAO phuHuynhDAO;
        private GiaoVienDAO giaoVienDAO;
        private HoSoNguoiDungDAO hoSoNguoiDungDAO;

        public CaiDatBUS()
        {
            hocSinhDAO = new HocSinhDAO();
            phuHuynhDAO = new PhuHuynhDAO();
            giaoVienDAO = new GiaoVienDAO();
            hoSoNguoiDungDAO = new HoSoNguoiDungDAO();
        }

        /// <summary>
        /// Kiểm tra email trùng lặp cho học sinh (loại trừ chính mình)
        /// </summary>
        public bool KiemTraEmailTrungHocSinh(string email, int maHocSinhLoaiTru)
        {
            return hocSinhDAO.KiemTraTrungEmail(email, maHocSinhLoaiTru);
        }

        /// <summary>
        /// Kiểm tra SĐT trùng lặp cho học sinh (loại trừ chính mình)
        /// </summary>
        public bool KiemTraSdtTrungHocSinh(string sdt, int maHocSinhLoaiTru)
        {
            return hocSinhDAO.KiemTraTrungSdt(sdt, maHocSinhLoaiTru);
        }

        /// <summary>
        /// Kiểm tra email trùng lặp cho phụ huynh (loại trừ chính mình)
        /// </summary>
        public bool KiemTraEmailTrungPhuHuynh(string email, int maPhuHuynhLoaiTru)
        {
            return phuHuynhDAO.KiemTraTrungEmail(email, maPhuHuynhLoaiTru);
        }

        /// <summary>
        /// Kiểm tra SĐT trùng lặp cho phụ huynh (loại trừ chính mình)
        /// </summary>
        public bool KiemTraSdtTrungPhuHuynh(string sdt, int maPhuHuynhLoaiTru)
        {
            return phuHuynhDAO.KiemTraTrungSdt(sdt, maPhuHuynhLoaiTru);
        }

        /// <summary>
        /// Kiểm tra email trùng lặp cho giáo viên (loại trừ chính mình)
        /// </summary>
        public bool KiemTraEmailTrungGiaoVien(string email, string maGiaoVienLoaiTru)
        {
            return giaoVienDAO.KiemTraEmailTonTai(email, maGiaoVienLoaiTru);
        }

        /// <summary>
        /// Kiểm tra email trùng lặp cho hồ sơ người dùng (loại trừ chính mình)
        /// </summary>
        public bool KiemTraEmailTrungHoSo(string email, string tenDangNhapLoaiTru)
        {
            return hoSoNguoiDungDAO.KiemTraEmailTonTai(email, tenDangNhapLoaiTru);
        }

        /// <summary>
        /// ✅ Kiểm tra trùng lặp email/SĐT theo loại đối tượng
        /// </summary>
        /// <param name="email">Email cần kiểm tra</param>
        /// <param name="sdt">Số điện thoại cần kiểm tra</param>
        /// <param name="loaiDoiTuong">Loại đối tượng: "HocSinh", "PhuHuynh", "GiaoVien", "HoSoNguoiDung"</param>
        /// <param name="errorMessage">Thông báo lỗi chi tiết (nếu có)</param>
        /// <param name="maDoiTuong">Mã đối tượng (int cho HS/PH, null nếu không có)</param>
        /// <param name="maDoiTuongStr">Mã đối tượng (string cho GV/HoSo, null nếu không có)</param>
        /// <returns>True nếu không trùng, False nếu có trùng</returns>
        public bool KiemTraTrungLapThongTin(
            string email,
            string sdt,
            string loaiDoiTuong,
            out string errorMessage,
            int? maDoiTuong = null,
            string maDoiTuongStr = null)
        {
            errorMessage = null;

            try
            {
                string sdtChuanHoa = ValidationHelper.ConvertToVietnameseFormat(sdt);

                switch (loaiDoiTuong)
                {
                    case "HocSinh":
                        if (!maDoiTuong.HasValue)
                        {
                            errorMessage = "Mã học sinh không hợp lệ.";
                            return false;
                        }

                        // ✅ Lấy thông tin hiện tại của học sinh
                        var hocSinhHienTai = hocSinhDAO.TimHocSinhTheoMa(maDoiTuong.Value);
                        if (hocSinhHienTai == null)
                        {
                            errorMessage = "Không tìm thấy học sinh.";
                            return false;
                        }

                        // ✅ CHỈ kiểm tra email nếu khác email hiện tại
                        if (!string.Equals(email.Trim(), hocSinhHienTai.Email?.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (KiemTraEmailTrungHocSinh(email, maDoiTuong.Value))
                            {
                                errorMessage = "Email này đã được sử dụng bởi học sinh khác!";
                                return false;
                            }
                        }

                        // ✅ Chuẩn hóa SĐT hiện tại để so sánh
                        string sdtHienTaiChuanHoa = ValidationHelper.ConvertToVietnameseFormat(hocSinhHienTai.SdtHS ?? "");

                        // ✅ CHỈ kiểm tra SĐT nếu khác SĐT hiện tại
                        if (!string.Equals(sdtChuanHoa, sdtHienTaiChuanHoa, StringComparison.OrdinalIgnoreCase))
                        {
                            if (KiemTraSdtTrungHocSinh(sdtChuanHoa, maDoiTuong.Value))
                            {
                                errorMessage = "Số điện thoại này đã được sử dụng bởi học sinh khác!";
                                return false;
                            }
                        }
                        break;

                    case "PhuHuynh":
                        if (!maDoiTuong.HasValue)
                        {
                            errorMessage = "Mã phụ huynh không hợp lệ.";
                            return false;
                        }

                        // ✅ Lấy thông tin hiện tại của phụ huynh
                        var phuHuynhHienTai = phuHuynhDAO.TimPhuHuynhTheoMa(maDoiTuong.Value);
                        if (phuHuynhHienTai == null)
                        {
                            errorMessage = "Không tìm thấy phụ huynh.";
                            return false;
                        }

                        // ✅ CHỈ kiểm tra email nếu khác email hiện tại
                        if (!string.Equals(email.Trim(), phuHuynhHienTai.Email?.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (KiemTraEmailTrungPhuHuynh(email, maDoiTuong.Value))
                            {
                                errorMessage = "Email này đã được sử dụng bởi phụ huynh khác!";
                                return false;
                            }
                        }

                        // ✅ CHỈ kiểm tra SĐT nếu khác SĐT hiện tại
                        if (!string.Equals(sdt.Trim(), phuHuynhHienTai.SoDienThoai?.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (KiemTraSdtTrungPhuHuynh(sdt, maDoiTuong.Value))
                            {
                                errorMessage = "Số điện thoại này đã được sử dụng bởi phụ huynh khác!";
                                return false;
                            }
                        }
                        break;

                    case "GiaoVien":
                        if (string.IsNullOrEmpty(maDoiTuongStr))
                        {
                            errorMessage = "Mã giáo viên không hợp lệ.";
                            return false;
                        }

                        // ✅ Lấy thông tin hiện tại của giáo viên
                        var giaoVienHienTai = giaoVienDAO.LayGiaoVienTheoMa(maDoiTuongStr);
                        if (giaoVienHienTai == null)
                        {
                            errorMessage = "Không tìm thấy giáo viên.";
                            return false;
                        }

                        // ✅ CHỈ kiểm tra email nếu khác email hiện tại
                        if (!string.Equals(email.Trim(), giaoVienHienTai.Email?.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (KiemTraEmailTrungGiaoVien(email, maDoiTuongStr))
                            {
                                errorMessage = "Email này đã được sử dụng bởi giáo viên khác!";
                                return false;
                            }
                        }
                        break;

                    case "HoSoNguoiDung":
                        if (string.IsNullOrEmpty(maDoiTuongStr))
                        {
                            errorMessage = "Tên đăng nhập không hợp lệ.";
                            return false;
                        }

                        // ✅ Lấy thông tin hiện tại của hồ sơ người dùng
                        var hoSoHienTai = hoSoNguoiDungDAO.GetHoSoByTenDangNhap(maDoiTuongStr);
                        if (hoSoHienTai == null)
                        {
                            errorMessage = "Không tìm thấy hồ sơ người dùng.";
                            return false;
                        }

                        // ✅ CHỈ kiểm tra email nếu khác email hiện tại
                        if (!string.Equals(email.Trim(), hoSoHienTai.Email?.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            if (KiemTraEmailTrungHoSo(email, maDoiTuongStr))
                            {
                                errorMessage = "Email này đã được sử dụng bởi người dùng khác!";
                                return false;
                            }
                        }
                        break;

                    default:
                        errorMessage = "Loại đối tượng không hợp lệ.";
                        return false;
                }

                return true; // Không có trùng lặp
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi kiểm tra trùng lặp: {ex.Message}";
                Console.WriteLine($"[ERROR] CaiDatBUS.KiemTraTrungLapThongTin: {ex.Message}");
                return false;
            }
        }
    }
}