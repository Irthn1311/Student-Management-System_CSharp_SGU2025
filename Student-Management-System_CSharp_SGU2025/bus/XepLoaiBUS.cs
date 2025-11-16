// XepLoaiBUS.cs
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class XepLoaiBUS
    {
        private XepLoaiDAO xepLoaiDAO;
        private HanhKiemDAO hanhKiemDAO;

        public XepLoaiBUS()
        {
            xepLoaiDAO = new XepLoaiDAO();
            hanhKiemDAO = new HanhKiemDAO();
        }

        /// <summary>
        /// Tính xếp loại tổng kết dựa trên học lực và hạnh kiểm
        /// Quy tắc: Bậc nào thấp hơn sẽ kéo bậc còn lại xuống
        /// </summary>
        public string TinhXepLoaiTongKet(string hocLuc, string hanhKiem)
        {
            if (string.IsNullOrEmpty(hocLuc) || string.IsNullOrEmpty(hanhKiem))
            {
                return "";
            }

            // Định nghĩa thứ tự ưu tiên (số càng nhỏ càng cao)
            Dictionary<string, int> thuTu = new Dictionary<string, int>
            {
                { "Giỏi", 1 },
                { "Tốt", 1 },
                { "Khá", 2 },
                { "Trung bình", 3 },
                { "Trung Bình", 3 },
                { "Yếu", 4 },
                { "Kém", 5 }
            };

            // Lấy bậc của học lực và hạnh kiểm
            int bacHocLuc = thuTu.ContainsKey(hocLuc) ? thuTu[hocLuc] : 999;
            int bacHanhKiem = thuTu.ContainsKey(hanhKiem) ? thuTu[hanhKiem] : 999;

            // Lấy bậc thấp hơn (số lớn hơn)
            int bacThapNhat = Math.Max(bacHocLuc, bacHanhKiem);

            // Chuyển đổi bậc thành xếp loại
            switch (bacThapNhat)
            {
                case 1:
                    return "Giỏi";
                case 2:
                    return "Khá";
                case 3:
                    return "Trung bình";
                case 4:
                    return "Yếu";
                case 5:
                    return "Kém";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Lưu xếp loại tổng kết vào database
        /// </summary>
        public bool LuuXepLoai(int maHocSinh, int maHocKy, string xepLoai, string ghiChu = "")
        {
            return xepLoaiDAO.LuuXepLoai(maHocSinh, maHocKy, xepLoai, ghiChu);
        }

        /// <summary>
        /// Lấy danh sách xếp loại kèm hạnh kiểm
        /// </summary>
        public List<XepLoaiDTO> LayDanhSachXepLoaiDayDu(int maHocKy, int? maLop = null)
        {
            // Lấy danh sách học lực
            List<XepLoaiDTO> dsXepLoai = xepLoaiDAO.GetDanhSachXepLoai(maHocKy, maLop);

            // Bổ sung thông tin hạnh kiểm và xếp loại tổng kết
            foreach (var item in dsXepLoai)
            {
                // Lấy hạnh kiểm
                HanhKiemDTO hk = hanhKiemDAO.LayHanhKiem(item.MaHocSinh, maHocKy);
                if (hk != null)
                {
                    item.HanhKiem = hk.XepLoai;
                }

                // Tính xếp loại tổng kết
                if (!string.IsNullOrEmpty(item.HocLuc) && !string.IsNullOrEmpty(item.HanhKiem))
                {
                    item.XepLoaiTongKet = TinhXepLoaiTongKet(item.HocLuc, item.HanhKiem);
                }
            }

            return dsXepLoai;
        }
    }
}