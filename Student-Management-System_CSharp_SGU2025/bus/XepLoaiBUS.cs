using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class XepLoaiBUS
    {
        private XepLoaiDAO xepLoaiDAO;

        public XepLoaiBUS()
        {
            xepLoaiDAO = new XepLoaiDAO();
        }

        /// <summary>
        /// Lấy xếp loại của học sinh trong học kỳ
        /// </summary>
        public XepLoaiDTO GetXepLoaiByStudent(int maHocSinh, int maHocKy)
        {
            try
            {
                if (maHocSinh <= 0)
                    throw new ArgumentException("Mã học sinh không hợp lệ");
                if (maHocKy <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                return xepLoaiDAO.GetXepLoaiByHocSinhVaHocKy(maHocSinh, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS GetXepLoaiByStudent: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Lấy tất cả xếp loại của học sinh
        /// </summary>
        public List<XepLoaiDTO> GetAllXepLoaiByHocSinh(int maHocSinh)
        {
            try
            {
                if (maHocSinh <= 0)
                    throw new ArgumentException("Mã học sinh không hợp lệ");

                return xepLoaiDAO.GetAllXepLoaiByHocSinh(maHocSinh);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS GetAllXepLoaiByHocSinh: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Lấy tất cả xếp loại trong học kỳ
        /// </summary>
        public List<XepLoaiDTO> GetAllXepLoaiByHocKy(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                return xepLoaiDAO.GetAllXepLoaiByHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS GetAllXepLoaiByHocKy: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Thêm hoặc cập nhật xếp loại
        /// </summary>
        public bool AddOrUpdateXepLoai(XepLoaiDTO xepLoai)
        {
            try
            {
                // Validate
                if (!ValidateXepLoai(xepLoai))
                    return false;

                // Kiểm tra đã tồn tại chưa
                bool exists = xepLoaiDAO.KiemTraTonTai(xepLoai.MaHocSinh, xepLoai.MaHocKy);

                if (exists)
                {
                    return xepLoaiDAO.UpdateXepLoai(xepLoai);
                }
                else
                {
                    return xepLoaiDAO.InsertXepLoai(xepLoai);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS AddOrUpdateXepLoai: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa xếp loại
        /// </summary>
        public bool DeleteXepLoai(int maHocSinh, int maHocKy)
        {
            try
            {
                if (maHocSinh <= 0)
                    throw new ArgumentException("Mã học sinh không hợp lệ");
                if (maHocKy <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                return xepLoaiDAO.DeleteXepLoai(maHocSinh, maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS DeleteXepLoai: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Validate dữ liệu xếp loại
        /// </summary>
        private bool ValidateXepLoai(XepLoaiDTO xepLoai)
        {
            if (xepLoai == null)
            {
                throw new ArgumentException("Dữ liệu xếp loại không hợp lệ");
            }

            if (xepLoai.MaHocSinh <= 0)
            {
                throw new ArgumentException("Mã học sinh không hợp lệ");
            }

            if (xepLoai.MaHocKy <= 0)
            {
                throw new ArgumentException("Mã học kỳ không hợp lệ");
            }

            if (string.IsNullOrWhiteSpace(xepLoai.HocLuc))
            {
                throw new ArgumentException("Học lực không được để trống");
            }

            if (!xepLoai.IsValidHocLuc())
            {
                throw new ArgumentException("Học lực không hợp lệ. Chỉ được phép: Giỏi, Khá, Trung bình, Yếu, Kém");
            }

            return true;
        }

        /// <summary>
        /// Tính xếp loại tự động dựa trên điểm trung bình
        /// </summary>
        public string TinhXepLoaiTuDong(float diemTrungBinh)
        {
            if (diemTrungBinh >= 8.0f)
                return "Giỏi";
            else if (diemTrungBinh >= 6.5f)
                return "Khá";
            else if (diemTrungBinh >= 5.0f)
                return "Trung bình";
            else if (diemTrungBinh >= 3.5f)
                return "Yếu";
            else
                return "Kém";
        }

        /// <summary>
        /// Tính và lưu xếp loại tự động cho học sinh trong học kỳ
        /// TODO: Cần sử dụng NhapDiemDAO thay vì DiemSoDAO
        /// </summary>
        public bool TinhVaLuuXepLoaiTuDong(int maHocSinh, int maHocKy)
        {
            throw new NotImplementedException("Chức năng này cần được cập nhật để sử dụng NhapDiemDAO");
            /*
            try
            {
                // Lấy tất cả điểm của học sinh trong học kỳ
                List<DiemSoDTO> danhSachDiem = diemSoDAO.LayDiemTheoHocSinhVaHocKy(maHocSinh, maHocKy);

                if (danhSachDiem == null || danhSachDiem.Count == 0)
                {
                    throw new Exception("Học sinh chưa có điểm để xếp loại");
                }

                // Tính điểm trung bình tất cả các môn
                float tongDiem = 0;
                int soMon = 0;
                foreach (var diem in danhSachDiem)
                {
                    if (diem.DiemTrungBinh.HasValue)
                    {
                        tongDiem += diem.DiemTrungBinh.Value;
                        soMon++;
                    }
                }

                if (soMon == 0)
                {
                    throw new Exception("Chưa có điểm trung bình của các môn");
                }

                float diemTrungBinhChung = tongDiem / soMon;

                // Tính xếp loại
                string hocLuc = TinhXepLoaiTuDong(diemTrungBinhChung);

                // Tạo DTO và lưu
                XepLoaiDTO xepLoai = new XepLoaiDTO
                {
                    MaHocSinh = maHocSinh,
                    MaHocKy = maHocKy,
                    HocLuc = hocLuc,
                    GhiChu = $"ĐTB: {diemTrungBinhChung:F2} - Tự động tính toán"
                };

                return AddOrUpdateXepLoai(xepLoai);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS TinhVaLuuXepLoaiTuDong: {ex.Message}");
                throw;
            }
            */
        }

        /// <summary>
        /// Tính xếp loại tự động cho tất cả học sinh trong học kỳ
        /// TODO: Cần sử dụng NhapDiemDAO thay vì DiemSoDAO
        /// </summary>
        public int TinhXepLoaiTuDongChoHocKy(int maHocKy)
        {
            throw new NotImplementedException("Chức năng này cần được cập nhật để sử dụng NhapDiemDAO");
            /*
            try
            {
                // Lấy danh sách tất cả học sinh có điểm trong học kỳ
                List<DiemSoDTO> danhSachDiem = diemSoDAO.LayTatCaDiemTheoHocKy(maHocKy);

                if (danhSachDiem == null || danhSachDiem.Count == 0)
                {
                    throw new Exception("Không có dữ liệu điểm trong học kỳ này");
                }

                // Nhóm theo học sinh
                Dictionary<int, List<DiemSoDTO>> diemTheoHocSinh = new Dictionary<int, List<DiemSoDTO>>();
                foreach (var diem in danhSachDiem)
                {
                    if (!diemTheoHocSinh.ContainsKey(diem.MaHocSinh))
                    {
                        diemTheoHocSinh[diem.MaHocSinh] = new List<DiemSoDTO>();
                    }
                    diemTheoHocSinh[diem.MaHocSinh].Add(diem);
                }

                // Tính xếp loại cho từng học sinh
                int soHocSinhDaXepLoai = 0;
                foreach (var kvp in diemTheoHocSinh)
                {
                    int maHocSinh = kvp.Key;
                    List<DiemSoDTO> diemCuaHocSinh = kvp.Value;

                    // Tính điểm trung bình
                    float tongDiem = 0;
                    int soMon = 0;
                    foreach (var diem in diemCuaHocSinh)
                    {
                        if (diem.DiemTrungBinh.HasValue)
                        {
                            tongDiem += diem.DiemTrungBinh.Value;
                            soMon++;
                        }
                    }

                    if (soMon > 0)
                    {
                        float diemTrungBinhChung = tongDiem / soMon;
                        string hocLuc = TinhXepLoaiTuDong(diemTrungBinhChung);

                        XepLoaiDTO xepLoai = new XepLoaiDTO
                        {
                            MaHocSinh = maHocSinh,
                            MaHocKy = maHocKy,
                            HocLuc = hocLuc,
                            GhiChu = $"ĐTB: {diemTrungBinhChung:F2} - Tự động tính toán hàng loạt"
                        };

                        if (AddOrUpdateXepLoai(xepLoai))
                        {
                            soHocSinhDaXepLoai++;
                        }
                    }
                }

                return soHocSinhDaXepLoai;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS TinhXepLoaiTuDongChoHocKy: {ex.Message}");
                throw;
            }
            */
        }

        /// <summary>
        /// Lấy thống kê xếp loại theo học kỳ
        /// </summary>
        public Dictionary<string, int> GetThongKeXepLoai(int maHocKy)
        {
            try
            {
                if (maHocKy <= 0)
                    throw new ArgumentException("Mã học kỳ không hợp lệ");

                return xepLoaiDAO.GetThongKeXepLoaiByHocKy(maHocKy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi BUS GetThongKeXepLoai: {ex.Message}");
                throw;
            }
        }
    }
}
