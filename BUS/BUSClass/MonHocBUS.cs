using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    public class MonHocBUS
    {
        private MonHocDAO monHocDAO;
        private MonHoc_NamHoc_KhoiBUS monHocNamHocKhoiBUS;
        private NamHocDAO namHocDAO;

        public MonHocBUS()
        {
            monHocDAO = new MonHocDAO();
            monHocNamHocKhoiBUS = new MonHoc_NamHoc_KhoiBUS();
            namHocDAO = new NamHocDAO();
        }

        // Thêm môn học với validation
        public bool ThemMonHoc(MonHocDTO monHoc)
        {
            // Validation dữ liệu
            if (string.IsNullOrWhiteSpace(monHoc.tenMon))
            {
                throw new ArgumentException("Tên môn học không được để trống.");
            }

            if (monHoc.soTiet <= 0)
            {
                throw new ArgumentException("Số tiết môn học phải lớn hơn 0.");
            }

            // Kiểm tra xem môn học với tên này đã tồn tại chưa
            if (monHocDAO.LayDSMonHocTheoTen(monHoc.tenMon) != null)
            {
                throw new ArgumentException("Môn học với tên này đã tồn tại.");
            }

            return monHocDAO.ThemMonHoc(monHoc);
        }

        // Đọc danh sách môn học
        public List<MonHocDTO> DocDSMH()
        {
            return monHocDAO.DocDSMH();
        }

        // Lấy môn học theo ID
        public MonHocDTO LayDSMonHocTheoId(int maMonHoc)
        {
            if (maMonHoc <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            return monHocDAO.LayDSMonHocTheoId(maMonHoc);
        }


        public int ThemMonHocVaLayId(MonHocDTO mh)
        {
            return monHocDAO.ThemMonHocVaLayId(mh);
        }
        // Lấy môn học theo tên
        public MonHocDTO LayDSMonHocTheoTen(string tenMonHoc)
        {
            if (string.IsNullOrWhiteSpace(tenMonHoc))
            {
                throw new ArgumentException("Tên môn học không được để trống.");
            }

            return monHocDAO.LayDSMonHocTheoTen(tenMonHoc);
        }

        // Cập nhật môn học với validation
        public bool UpdateMonHoc(MonHocDTO monHoc)
        {
            // Validation dữ liệu
            if (monHoc.maMon <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            if (string.IsNullOrWhiteSpace(monHoc.tenMon))
            {
                throw new ArgumentException("Tên môn học không được để trống.");
            }

            if (monHoc.soTiet <= 0)
            {
                throw new ArgumentException("Số tiết môn học phải lớn hơn 0.");
            }

            // Kiểm tra xem tên môn học mới có bị trùng với môn khác không (trừ môn hiện tại)
            MonHocDTO monHocHienTai = monHocDAO.LayDSMonHocTheoId(monHoc.maMon);
            if (monHocHienTai != null && monHocHienTai.tenMon != monHoc.tenMon && monHocDAO.LayDSMonHocTheoTen(monHoc.tenMon) != null)
            {
                throw new ArgumentException("Tên môn học này đã tồn tại cho một môn khác.");
            }

            return monHocDAO.UpdateMonHoc(monHoc);
        }

        // Xóa môn học với validation
        public bool DeleteMonHoc(int maMonHoc)
        {
            if (maMonHoc <= 0)
            {
                throw new ArgumentException("Mã môn học phải lớn hơn 0.");
            }

            MonHocDTO monHoc = monHocDAO.LayDSMonHocTheoId(maMonHoc);
            if (monHoc == null)
            {
                throw new ArgumentException("Không tìm thấy môn học với mã này.");
            }

            // Kiểm tra môn học có đang được sử dụng không
            var (dangSuDung, danhSachSuDung) = monHocDAO.KiemTraMonHocDangSuDung(maMonHoc);
            if (dangSuDung)
            {
                // Kiểm tra xem có điểm số không (điểm số là ràng buộc cứng - không cho xóa)
                bool coDiemSo = danhSachSuDung.Any(s => s.Contains("Điểm số"));
                
                if (coDiemSo)
                {
                    // Nếu có điểm số → KHÔNG CHO XÓA (ràng buộc cứng)
                    string message = $"Không thể xóa môn học '{monHoc.tenMon}'!\n\n";
                    message += "Lý do: Môn học này đã có điểm số trong hệ thống.\n\n";
                    message += "Điểm số là dữ liệu lịch sử quan trọng, không thể xóa môn học đã có điểm.\n\n";
                    
                    // Thêm thông tin các dữ liệu liên quan khác (nếu có)
                    var danhSachKhac = danhSachSuDung.Where(s => !s.Contains("Điểm số")).ToList();
                    if (danhSachKhac.Count > 0)
                    {
                        message += "Ngoài ra, môn học này còn được sử dụng trong:\n";
                        message += string.Join("\n", danhSachKhac);
                    }
                    
                    throw new InvalidOperationException(message);
                }
                else
                {
                    // Nếu không có điểm số nhưng có dữ liệu khác → vẫn cho phép xóa (có thể cascade)
                    // Nhưng thông báo để người dùng biết
                    string message = $"Môn học '{monHoc.tenMon}' đang được sử dụng trong:\n\n";
                    message += string.Join("\n", danhSachSuDung);
                    message += "\n\nBạn có chắc muốn xóa? Dữ liệu liên quan có thể bị ảnh hưởng.";
                    throw new InvalidOperationException(message);
                }
            }

            // ✅ Kiểm tra năm học: Chỉ cho phép xóa nếu năm học "Chưa bắt đầu" hoặc "Đang diễn ra" trong tuần đầu
            var monHocNamHocKhoiBUS = new MonHoc_NamHoc_KhoiBUS();
            var danhSachNamHoc = monHocNamHocKhoiBUS.LayDanhSachNamHocTheoMonHoc(maMonHoc);
            
            if (danhSachNamHoc != null && danhSachNamHoc.Count > 0)
            {
                DateTime now = DateTime.Now.Date;
                var namHocDAO = new NamHocDAO();
                var namHocKhongChoPhepXoa = new List<string>();
                
                foreach (string maNamHoc in danhSachNamHoc)
                {
                    var namHoc = namHocDAO.LayNamHocTheoMa(maNamHoc);
                    if (namHoc != null)
                    {
                        DateTime ngayBDNamHoc = namHoc.NgayBD.Date;
                        DateTime ngayKTNamHoc = namHoc.NgayKT.Date;
                        
                        // Kiểm tra năm học có đang diễn ra không
                        bool namHocDangDienRa = (now >= ngayBDNamHoc && now <= ngayKTNamHoc);
                        bool namHocChuaBatDau = (now < ngayBDNamHoc);
                        
                        if (namHocDangDienRa)
                        {
                            // Kiểm tra xem có quá 1 tuần từ ngày bắt đầu không
                            TimeSpan diff = now - ngayBDNamHoc;
                            if (diff.TotalDays > 7)
                            {
                                namHocKhongChoPhepXoa.Add($"{namHoc.TenNamHoc} (Đang diễn ra - đã quá 1 tuần)");
                            }
                        }
                        // Nếu năm học "Chưa bắt đầu" hoặc "Đang diễn ra" trong tuần đầu → cho phép xóa
                        // Nếu năm học "Đã kết thúc" → không kiểm tra (giữ nguyên logic cũ)
                    }
                }
                
                if (namHocKhongChoPhepXoa.Count > 0)
                {
                    string message = $"Không thể xóa môn học '{monHoc.tenMon}'!\n\n";
                    message += "Lý do: Môn học này đang được sử dụng trong các năm học sau:\n\n";
                    message += string.Join("\n", namHocKhongChoPhepXoa);
                    message += "\n\nChỉ có thể xóa môn học trong năm học 'Chưa bắt đầu' hoặc năm học 'Đang diễn ra' (trong tuần đầu).";
                    throw new InvalidOperationException(message);
                }
            }

            return monHocDAO.DeleteMonHoc(maMonHoc);
        }

        /// <summary>
        /// Thêm môn học và liên kết với năm học và khối (nếu có)
        /// </summary>
        public int ThemMonHocVaLienKetNamHocKhoi(MonHocDTO monHoc, string maNamHoc, bool apDungChoTatCaKhoi)
        {
            // Validation dữ liệu
            if (string.IsNullOrWhiteSpace(monHoc.tenMon))
            {
                throw new ArgumentException("Tên môn học không được để trống.");
            }

            if (monHoc.soTiet <= 0)
            {
                throw new ArgumentException("Số tiết môn học phải lớn hơn 0.");
            }

            // Kiểm tra xem môn học với tên này đã tồn tại chưa
            if (monHocDAO.LayDSMonHocTheoTen(monHoc.tenMon) != null)
            {
                throw new ArgumentException("Môn học với tên này đã tồn tại.");
            }

            // Validate năm học nếu có
            if (!string.IsNullOrWhiteSpace(maNamHoc))
            {
                var namHoc = namHocDAO.LayNamHocTheoMa(maNamHoc);
                if (namHoc == null)
                {
                    throw new ArgumentException("Năm học không tồn tại.");
                }
                monHoc.namHocBatDau = maNamHoc;
            }

            // Thêm môn học và lấy ID
            int maMonMoi = monHocDAO.ThemMonHocVaLayId(monHoc);
            
            if (maMonMoi > 0 && !string.IsNullOrWhiteSpace(maNamHoc))
            {
                // Liên kết với năm học và khối
                if (apDungChoTatCaKhoi)
                {
                    // Thêm cho tất cả khối (10, 11, 12)
                    monHocNamHocKhoiBUS.ThemMonHocChoTatCaKhoi(maMonMoi, maNamHoc);
                }
            }

            return maMonMoi;
        }

        /// <summary>
        /// Lấy danh sách môn học theo năm học bắt đầu
        /// </summary>
        public List<MonHocDTO> LayMonHocTheoNamHocBatDau(string maNamHoc)
        {
            if (string.IsNullOrWhiteSpace(maNamHoc))
            {
                return new List<MonHocDTO>();
            }

            return monHocDAO.LayMonHocTheoNamHocBatDau(maNamHoc);
        }
    }
}