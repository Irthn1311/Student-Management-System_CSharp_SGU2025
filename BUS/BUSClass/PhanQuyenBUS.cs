using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class PhanQuyenBUS
    {
        private readonly PhanQuyenDAO phanQuyenDAO;

        public PhanQuyenBUS()
        {
            phanQuyenDAO = new PhanQuyenDAO();
        }

        #region Chức năng

        /// <summary>
        /// Lấy tất cả chức năng
        /// </summary>
        public List<ChucNangDTO> GetAllChucNang()
        {
            return phanQuyenDAO.GetAllChucNang();
        }

        #endregion

        #region Vai trò

        /// <summary>
        /// Lấy tất cả vai trò
        /// </summary>
        public List<VaiTroDTO> GetAllVaiTro()
        {
            return phanQuyenDAO.GetAllVaiTro();
        }

        /// <summary>
        /// Tạo mã vai trò từ tên vai trò
        /// Ví dụ: "Giáo Vụ" -> "giaovu"
        /// </summary>
        public string TaoMaVaiTro(string tenVaiTro)
        {
            if (string.IsNullOrWhiteSpace(tenVaiTro))
                return "";

            // Chuyển về chữ thường và loại bỏ dấu
            string result = tenVaiTro.ToLower().Trim();

            // Loại bỏ dấu tiếng Việt
            result = RemoveVietnameseTone(result);

            // Loại bỏ khoảng trắng
            result = result.Replace(" ", "");

            // Chỉ giữ lại chữ cái và số
            result = Regex.Replace(result, "[^a-z0-9]", "");

            return result;
        }

        /// <summary>
        /// Loại bỏ dấu tiếng Việt
        /// </summary>
        private string RemoveVietnameseTone(string text)
        {
            string[] vietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };

            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                {
                    text = text.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
                }
            }

            return text;
        }

        /// <summary>
        /// Kiểm tra tên vai trò đã tồn tại chưa
        /// </summary>
        public bool KiemTraTenVaiTroTonTai(string tenVaiTro)
        {
            var danhSach = phanQuyenDAO.GetAllVaiTro();
            return danhSach.Any(x => x.TenVaiTro.Equals(tenVaiTro, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Xóa vai trò
        /// </summary>
        public bool XoaVaiTro(string maVaiTro)
        {
            return phanQuyenDAO.DeleteVaiTro(maVaiTro);
        }

        #endregion

        #region Thêm vai trò với quyền

        /// <summary>
        /// Thêm vai trò với các quyền được chọn
        /// </summary>
        /// <param name="tenVaiTro">Tên vai trò</param>
        /// <param name="danhSachQuyen">Danh sách quyền: Key = MaChucNang, Value = List hành động (read, create, update, delete)</param>
        public bool ThemVaiTroVoiQuyen(string tenVaiTro, Dictionary<string, List<string>> danhSachQuyen)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(tenVaiTro))
                    throw new Exception("Tên vai trò không được để trống!");

                if (KiemTraTenVaiTroTonTai(tenVaiTro))
                    throw new Exception("Tên vai trò đã tồn tại!");

                if (danhSachQuyen == null || danhSachQuyen.Count == 0)
                    throw new Exception("Vui lòng chọn ít nhất một quyền!");

                // Tạo mã vai trò
                string maVaiTro = TaoMaVaiTro(tenVaiTro);

                // Tạo DTO vai trò
                VaiTroDTO vaiTro = new VaiTroDTO
                {
                    MaVaiTro = maVaiTro,
                    TenVaiTro = tenVaiTro,
                    MoTa = null
                };

                // Tạo danh sách quyền chi tiết
                List<VaiTroChucNangHanhDongDTO> listQuyen = new List<VaiTroChucNangHanhDongDTO>();

                foreach (var item in danhSachQuyen)
                {
                    string maChucNang = item.Key;
                    List<string> hanhDongs = item.Value;

                    foreach (string hanhDong in hanhDongs)
                    {
                        listQuyen.Add(new VaiTroChucNangHanhDongDTO
                        {
                            MaVaiTro = maVaiTro,
                            MaChucNang = maChucNang,
                            HanhDong = hanhDong
                        });
                    }
                }

                // Thêm vào database
                return phanQuyenDAO.ThemVaiTroVoiQuyen(vaiTro, listQuyen);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Lấy thông tin vai trò

        /// <summary>
        /// Lấy danh sách tên chức năng của vai trò
        /// </summary>
        public List<string> GetTenChucNangByVaiTro(string maVaiTro)
        {
            return phanQuyenDAO.GetChucNangByVaiTro(maVaiTro);
        }

        /// <summary>
        /// Kiểm tra quyền
        /// </summary>
        public bool KiemTraQuyen(string maVaiTro, string maChucNang, string hanhDong)
        {
            return phanQuyenDAO.KiemTraQuyen(maVaiTro, maChucNang, hanhDong);
        }

        /// <summary>
        /// Lấy danh sách vai trò của người dùng
        /// </summary>
        public List<string> GetVaiTroByNguoiDung(string tenDangNhap)
        {
            return phanQuyenDAO.GetVaiTroByNguoiDung(tenDangNhap);
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền trên chức năng và hành động
        /// </summary>
        public bool KiemTraQuyenNguoiDung(string tenDangNhap, string maChucNang, string hanhDong)
        {
            List<string> danhSachVaiTro = GetVaiTroByNguoiDung(tenDangNhap);

            foreach (string maVaiTro in danhSachVaiTro)
            {
                if (KiemTraQuyen(maVaiTro, maChucNang, hanhDong))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Mapping CheckBox sang HanhDong

        /// <summary>
        /// Map từ tên checkbox sang tên hành động trong database
        /// cbXem -> read
        /// cbThem -> create
        /// cbSua -> update
        /// cbXoa -> delete
        /// </summary>
        public string MapCheckBoxToHanhDong(string checkBoxName)
        {
            switch (checkBoxName.ToLower())
            {
                case "them":
                    return "create";
                case "sua":
                    return "update";
                case "xoa":
                    return "delete";
                default:
                    return "";
            }
        }
        #endregion

        /// <summary>
        /// Kiểm tra vai trò có thể xóa không
        /// </summary>
        public bool KiemTraCoTheXoaVaiTro(string maVaiTro)
        {
            return !phanQuyenDAO.KiemTraVaiTroDuocGan(maVaiTro);
        }

        /// <summary>
        /// Lấy danh sách người dùng đang có vai trò này
        /// </summary>
        public List<string> GetNguoiDungByVaiTro(string maVaiTro)
        {
            return phanQuyenDAO.GetNguoiDungByVaiTro(maVaiTro);
        }


        /// <summary>
        /// Lấy chi tiết vai trò với format hiển thị
        /// </summary>
        public Dictionary<string, List<string>> GetChiTietVaiTro(string maVaiTro)
        {
            return phanQuyenDAO.GetChiTietVaiTro(maVaiTro);
        }

        /// <summary>
        /// Map tên hành động từ database sang tiếng Việt
        /// </summary>
        public string MapHanhDongToVietnamese(string hanhDong)
        {
            switch (hanhDong.ToLower())
            {
                case "create":
                    return "Thêm";
                case "update":
                    return "Sửa";
                case "delete":
                    return "Xóa";
                default:
                    return hanhDong;
            }
        }

    }
}