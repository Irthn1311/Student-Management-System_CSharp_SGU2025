// HanhKiemBUS.cs
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    internal class HanhKiemBUS
    {
        private HanhKiemDAO hanhKiemDAO;
        private KhenThuongKyLuatDAO ktklDAO;
        private XepLoaiDAO xepLoaiDAO;

        public HanhKiemBUS()
        {
            hanhKiemDAO = new HanhKiemDAO();
            ktklDAO = new KhenThuongKyLuatDAO();
            xepLoaiDAO = new XepLoaiDAO();
        }

        public string TinhHanhKiemTuDong(int maHocSinh, int maHocKy)
        {
            try
            {
                // Lấy học lực - NHANH HƠN vì không query toàn bộ
                string hocLuc = LayHocLucHocSinh(maHocSinh, maHocKy);

                // Nếu chưa có học lực thì BỎ QUA LUÔN
                if (string.IsNullOrEmpty(hocLuc))
                {
                    return "";
                }

                // Lấy mức kỷ luật cao nhất (chỉ tính kỷ luật đã duyệt)
                string mucKyLuatCaoNhat = LayMucKyLuatCaoNhat(maHocSinh, maHocKy);

                // Áp dụng logic xếp loại
                return XepLoaiHanhKiem(hocLuc, mucKyLuatCaoNhat);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tính hạnh kiểm tự động: " + ex.Message);
                return "";
            }
        }
        private string LayHocLucHocSinh(int maHocSinh, int maHocKy)
        {
            try
            {
                // TỐI ƯU: Query trực tiếp 1 học sinh thay vì lấy toàn bộ danh sách
                List<XepLoaiDTO> dsXepLoai = xepLoaiDAO.GetDanhSachXepLoai(maHocKy, null);
                XepLoaiDTO xepLoai = dsXepLoai.FirstOrDefault(x => x.MaHocSinh == maHocSinh);

                if (xepLoai != null && !string.IsNullOrEmpty(xepLoai.HocLuc))
                {
                    return xepLoai.HocLuc;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy mức kỷ luật cao nhất của học sinh trong học kỳ (CHỈ TÍNH KỶ LUẬT ĐÃ DUYỆT)
        /// </summary>
        private string LayMucKyLuatCaoNhat(int maHocSinh, int maHocKy)
        {
            try
            {
                // Lấy danh sách kỷ luật ĐÃ DUYỆT của học sinh trong học kỳ
                List<KhenThuongKyLuatDTO> dsKyLuat = ktklDAO.LayDanhSachKyLuatDaDuyet(maHocSinh, maHocKy);

                if (dsKyLuat.Count == 0)
                    return null;

                // Tìm mức kỷ luật cao nhất theo thứ tự: Kỷ luật > Khiển trách > Cảnh cáo > Nhắc nhở
                if (dsKyLuat.Any(kl => kl.MucXuLy != null && kl.MucXuLy.Contains("Kỷ luật")))
                    return "Kỷ luật";

                if (dsKyLuat.Any(kl => kl.MucXuLy != null && kl.MucXuLy.Contains("Khiển trách")))
                    return "Khiển trách";

                if (dsKyLuat.Any(kl => kl.MucXuLy != null && kl.MucXuLy.Contains("Cảnh cáo")))
                    return "Cảnh cáo";

                if (dsKyLuat.Any(kl => kl.MucXuLy != null && kl.MucXuLy.Contains("Nhắc nhở")))
                    return "Nhắc nhở";

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Logic xếp loại hạnh kiểm dựa trên học lực và kỷ luật
        /// </summary>
        private string XepLoaiHanhKiem(string hocLuc, string mucKyLuat)
        {
            // Mức kỷ luật cao nhất -> Yếu
            if (mucKyLuat == "Kỷ luật")
                return "Yếu";
            // Nếu có mức khiển trách -> Yếu
            if (mucKyLuat == "Khiển trách")
                return "Trung bình";

            // Nếu có mức cảnh cáo -> Trung bình
            if (mucKyLuat == "Cảnh cáo")
                return "Khá";

            // Nếu có mức nhắc nhở hoặc không vi phạm
            if (mucKyLuat == "Nhắc nhở" || string.IsNullOrEmpty(mucKyLuat))
            {
                // Học lực Giỏi -> Tốt
                if (hocLuc == "Giỏi")
                    return "Tốt";

                // Học lực Khá -> Khá
                if (hocLuc == "Khá")
                    return "Tốt";

                // Học lực Trung bình, không vi phạm -> Khá
                if (hocLuc == "Trung bình" && string.IsNullOrEmpty(mucKyLuat))
                    return "Khá";

                // Học lực Trung bình, có nhắc nhở -> Khá
                if (hocLuc == "Trung bình" && mucKyLuat == "Nhắc nhở")
                    return "Khá";

                // Học lực Yếu hoặc Kém, không vi phạm -> Trung bình
                if (hocLuc == "Yếu")
                    return "Trung bình";

                // Học lực Trung bình, có nhắc nhở -> Khá
                if (hocLuc == "Yếu" && mucKyLuat == "Khiển trách")
                    return "Yếu";

                // Học lực Yếu hoặc Kém, có nhắc nhở -> Trung bình
                if ( hocLuc == "Kém")
                    return "Yếu";
            }

            // Mặc định -> Khá
            return "Khá";
        }

        /// <summary>
        /// Lưu hạnh kiểm
        /// </summary>
        public bool LuuHanhKiem(HanhKiemDTO hk)
        {
            return hanhKiemDAO.LuuHanhKiem(hk);
        }

        /// <summary>
        /// Lấy hoặc tạo mới hạnh kiểm
        /// </summary>
        public HanhKiemDTO LayHanhKiem(int maHocSinh, int maHocKy)
        {

            return hanhKiemDAO.LayHanhKiem(maHocSinh, maHocKy);

        }
    }
}