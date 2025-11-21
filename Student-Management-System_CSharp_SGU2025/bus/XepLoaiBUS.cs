// XepLoaiBUS.cs
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Lấy tất cả xếp loại trong hệ thống
        /// Dùng cho logic phân lớp tự động
        /// </summary>
        public List<XepLoaiDTO> GetAllXepLoai()
        {
            try
            {
                // Lấy tất cả học kỳ và lấy xếp loại cho từng học kỳ
                List<XepLoaiDTO> allXepLoai = new List<XepLoaiDTO>();
                HocKyDAO hocKyDAO = new HocKyDAO();
                List<HocKyDTO> allHocKy = hocKyDAO.GetAllHocKy();

                foreach (var hk in allHocKy)
                {
                    List<XepLoaiDTO> xepLoaiTheoHocKy = xepLoaiDAO.GetDanhSachXepLoai(hk.MaHocKy, null);
                    // Thêm MaHocKy vào mỗi item
                    foreach (var item in xepLoaiTheoHocKy)
                    {
                        item.MaHocKy = hk.MaHocKy;
                    }
                    allXepLoai.AddRange(xepLoaiTheoHocKy);
                }

                return allXepLoai;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy tất cả xếp loại: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy xếp loại của một học sinh theo học kỳ
        /// </summary>
        public XepLoaiDTO GetXepLoaiByStudent(int maHocSinh, int maHocKy)
        {
            try
            {
                List<XepLoaiDTO> dsXepLoai = xepLoaiDAO.GetDanhSachXepLoai(maHocKy, null);
                XepLoaiDTO xepLoai = dsXepLoai.FirstOrDefault(x => x.MaHocSinh == maHocSinh);
                
                if (xepLoai != null)
                {
                    xepLoai.MaHocKy = maHocKy;
                }

                return xepLoai;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi nghiệp vụ khi lấy xếp loại học sinh {maHocSinh} học kỳ {maHocKy}: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy thống kê xếp loại tổng kết theo học kỳ và lớp
        /// Kết hợp cả học lực và hạnh kiểm để ra xếp loại tổng kết
        /// </summary>
        public Dictionary<string, int> ThongKeXepLoaiTongKet(int maHocKy, int? maLop = null)
        {
            Dictionary<string, int> thongKe = new Dictionary<string, int>
    {
        { "Giỏi", 0 },
        { "Khá", 0 },
        { "Trung bình", 0 },
        { "Yếu", 0 },
        { "Kém", 0 }
    };

            try
            {
                // Lấy danh sách xếp loại đầy đủ (có cả học lực và hạnh kiểm)
                List<XepLoaiDTO> dsXepLoai = LayDanhSachXepLoaiDayDu(maHocKy, maLop);

                foreach (var item in dsXepLoai)
                {
                    // Chỉ thống kê những học sinh có xếp loại tổng kết
                    if (!string.IsNullOrEmpty(item.XepLoaiTongKet) && thongKe.ContainsKey(item.XepLoaiTongKet))
                    {
                        thongKe[item.XepLoaiTongKet]++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thống kê xếp loại tổng kết: " + ex.Message);
            }

            return thongKe;
        }
        /// <summary>
        /// Thống kê xếp loại tổng kết theo khối và học kỳ
        /// Kết hợp cả học lực và hạnh kiểm để tính xếp loại tổng kết
        /// </summary>
        public Dictionary<string, int> ThongKeXepLoaiTongKetTheoKhoi(int maHocKy, int maKhoi)
        {
            Dictionary<string, int> thongKe = new Dictionary<string, int>
    {
        { "Giỏi", 0 },
        { "Khá", 0 },
        { "Trung bình", 0 },
        { "Yếu", 0 },
        { "Kém", 0 }
    };

            try
            {
                // Lấy danh sách học sinh có học lực theo khối
                List<XepLoaiDTO> dsXepLoai = xepLoaiDAO.GetDanhSachXepLoaiTheoKhoi(maHocKy, maKhoi);

                // Với mỗi học sinh, lấy hạnh kiểm và tính xếp loại tổng kết
                foreach (var item in dsXepLoai)
                {
                    // Lấy hạnh kiểm
                    HanhKiemDTO hk = hanhKiemDAO.LayHanhKiem(item.MaHocSinh, maHocKy);

                    // Chỉ tính xếp loại nếu có đầy đủ cả học lực và hạnh kiểm
                    if (!string.IsNullOrEmpty(item.HocLuc) && hk != null && !string.IsNullOrEmpty(hk.XepLoai))
                    {
                        string xepLoaiTongKet = TinhXepLoaiTongKet(item.HocLuc, hk.XepLoai);

                        if (!string.IsNullOrEmpty(xepLoaiTongKet) && thongKe.ContainsKey(xepLoaiTongKet))
                        {
                            thongKe[xepLoaiTongKet]++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thống kê xếp loại tổng kết theo khối: " + ex.Message);
            }

            return thongKe;
        }

        /// <summary>
        /// Đếm tổng số học sinh theo khối và học kỳ (bao gồm cả chưa có xếp loại)
        /// </summary>
        public int DemTongSoHocSinhTheoKhoi(int maHocKy, int maKhoi)
        {
            return xepLoaiDAO.DemTongSoHocSinhTheoKhoi(maHocKy, maKhoi);
        }

        /// <summary>
        /// Lấy tất cả xếp loại trong hệ thống
        /// Dùng cho logic phân lớp tự động
        /// </summary>
        public List<XepLoaiDTO> GetAllXepLoai()
        {
            try
            {
                return xepLoaiDAO.GetAllXepLoai();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi nghiệp vụ khi lấy tất cả xếp loại: " + ex.Message);
            }
        }
    }
}
