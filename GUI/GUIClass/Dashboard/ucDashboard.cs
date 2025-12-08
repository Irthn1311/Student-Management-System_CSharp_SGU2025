using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ucDashboard : UserControl
    {
        private XepLoaiBUS xepLoaiBUS;
        private HocKyDAO hocKyDAO;
        private HocSinhBLL hocSinhBLL;
        private GiaoVienBUS giaoVienBUS;
        private LopHocBUS lopHocBUS;
        private NamHocBUS namHocBUS;
        private ThongBaoBUS thongBaoBUS;
        private KhenThuongKyLuatBUS khenThuongBUS;

        public ucDashboard()
        {
            InitializeComponent();
            hocKyDAO = new HocKyDAO();
            xepLoaiBUS = new XepLoaiBUS();
            hocSinhBLL = new HocSinhBLL();
            giaoVienBUS = new GiaoVienBUS();
            lopHocBUS = new LopHocBUS();
            namHocBUS = new NamHocBUS();
            thongBaoBUS = new ThongBaoBUS();
            khenThuongBUS = new KhenThuongKyLuatBUS();
        }

        private void cardHoatDongNoiBatDashboard3_Load(object sender, EventArgs e)
        {

        }

        private void ucDashboard_Load(object sender, EventArgs e)
        {
            LoadHocKyToCombobox();
            LoadThongKeTongQuan();
            LoadThongBaoGanDay();
            LoadHoatDongNoiBat();
            LoadThongKeXepLoai(); // Load thống kê xếp loại khi form load
        }

        private void LoadThongKeTongQuan()
        {
            try
            {
                // Tổng học sinh
                var danhSachHocSinh = hocSinhBLL.GetAllHocSinh();
                int tongHocSinh = danhSachHocSinh != null ? danhSachHocSinh.Count : 0;
                lblDemSoHocSinh.Text = tongHocSinh.ToString("#,##0");
                
                // Tính phần trăm thay đổi học sinh (so với 3 tháng trước)
                double phanTramThayDoiHS = TinhPhanTramThayDoiHocSinh();
                if (phanTramThayDoiHS > 0)
                {
                    lblThayDoiHocSinh.Text = $"+{phanTramThayDoiHS:F1}%";
                    lblThayDoiHocSinh.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                }
                else if (phanTramThayDoiHS < 0)
                {
                    lblThayDoiHocSinh.Text = $"{phanTramThayDoiHS:F1}%";
                    lblThayDoiHocSinh.ForeColor = Color.FromArgb(239, 68, 68); // Đỏ
                }
                else
                {
                    lblThayDoiHocSinh.Text = "0%";
                    lblThayDoiHocSinh.ForeColor = Color.FromArgb(107, 114, 128); // Xám
                }

                // Tổng giáo viên
                var danhSachGiaoVien = giaoVienBUS.DocDSGiaoVien();
                int tongGiaoVien = danhSachGiaoVien != null ? danhSachGiaoVien.Count : 0;
                lblDemGiaoVien.Text = tongGiaoVien.ToString("#,##0");
                
                // Tính phần trăm thay đổi giáo viên (so với 3 tháng trước)
                double phanTramThayDoiGV = TinhPhanTramThayDoiGiaoVien();
                if (phanTramThayDoiGV > 0)
                {
                    lblThayDoiGiaoVien.Text = $"+{phanTramThayDoiGV:F1}%";
                    lblThayDoiGiaoVien.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                }
                else if (phanTramThayDoiGV < 0)
                {
                    lblThayDoiGiaoVien.Text = $"{phanTramThayDoiGV:F1}%";
                    lblThayDoiGiaoVien.ForeColor = Color.FromArgb(239, 68, 68); // Đỏ
                }
                else
                {
                    lblThayDoiGiaoVien.Text = "0%";
                    lblThayDoiGiaoVien.ForeColor = Color.FromArgb(107, 114, 128); // Xám
                }

                // Tổng lớp học
                var danhSachLop = lopHocBUS.DocDSLop();
                int tongLop = danhSachLop != null ? danhSachLop.Count : 0;
                lblDemLopHoc.Text = tongLop.ToString("#,##0");

                // Năm học hiện tại
                var danhSachNamHoc = namHocBUS.DocDSNamHoc();
                if (danhSachNamHoc != null && danhSachNamHoc.Count > 0)
                {
                    var namHocHienTai = danhSachNamHoc
                        .Where(nh => DateTime.Now >= nh.NgayBD && DateTime.Now <= nh.NgayKT)
                        .OrderByDescending(nh => nh.NgayBD)
                        .FirstOrDefault();
                    
                    if (namHocHienTai != null)
                    {
                        lblNamHocHienTai.Text = namHocHienTai.TenNamHoc;
                        lblTrangThaiNamHoc.Text = "Đang diễn ra";
                        lblTrangThaiNamHoc.ForeColor = Color.FromArgb(34, 197, 94);
                    }
                    else
                    {
                        // Lấy năm học mới nhất
                        var namHocMoiNhat = danhSachNamHoc.OrderByDescending(nh => nh.NgayBD).First();
                        lblNamHocHienTai.Text = namHocMoiNhat.TenNamHoc;
                        lblTrangThaiNamHoc.Text = "Chưa bắt đầu";
                        lblTrangThaiNamHoc.ForeColor = Color.FromArgb(107, 114, 128);
                    }
                }
                else
                {
                    lblNamHocHienTai.Text = "N/A";
                    lblTrangThaiNamHoc.Text = "Chưa có dữ liệu";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thống kê tổng quan: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongBaoGanDay()
        {
            try
            {
                // Lấy 4 thông báo mới nhất
                var danhSachThongBao = thongBaoBUS.LayDanhSachThongBao(
                    SessionManager.TenDangNhap,
                    null, null, null, 1, 4
                );

                var thongBaoItems = new[] 
                { 
                    recentActivityItemThongBao1, 
                    recentActivityItemThongBao2, 
                    recentActivityItemThongBao3, 
                    recentActivityItemThongBao4 
                };

                for (int i = 0; i < thongBaoItems.Length; i++)
                {
                    if (i < danhSachThongBao.Count)
                    {
                        var tb = danhSachThongBao[i];
                        thongBaoItems[i].lbTextName.Text = tb.TieuDe;
                        thongBaoItems[i].lbNote.Text = tb.NgayTao.ToString("dd/MM/yyyy HH:mm");
                        
                        // Đặt icon và màu theo loại thông báo
                        SetThongBaoIcon(thongBaoItems[i], tb.LoaiThongBao);
                    }
                    else
                    {
                        // Ẩn các item không có dữ liệu
                        thongBaoItems[i].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thông báo gần đây: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetThongBaoIcon(RecentActivityItem item, string loaiThongBao)
        {
            switch (loaiThongBao?.ToUpper())
            {
                case "HOP_PHU_HUYNH":
                case "HOP":
                    item.PictureBoxThongBao.Image = Properties.Resources.icons8_notification_blue;
                    item.PictureBoxThongBao.BackColor = Color.FromArgb(219, 234, 254);
                    break;
                case "KHEN_THUONG":
                case "KHENTHUONG":
                    item.PictureBoxThongBao.Image = Properties.Resources.icons8_winners_medal_xanhla;
                    item.PictureBoxThongBao.BackColor = Color.FromArgb(220, 252, 231);
                    break;
                case "BAO_CAO":
                case "BAOCAO":
                    item.PictureBoxThongBao.Image = Properties.Resources.icons8_increase_profits_cam;
                    item.PictureBoxThongBao.BackColor = Color.FromArgb(255, 237, 213);
                    break;
                case "LICH_TRINH":
                case "LICHTRINH":
                case "SU_KIEN":
                case "SUKIEN":
                    item.PictureBoxThongBao.Image = Properties.Resources.icons8_timetable_tim;
                    item.PictureBoxThongBao.BackColor = Color.FromArgb(243, 232, 255);
                    break;
                default:
                    item.PictureBoxThongBao.Image = Properties.Resources.icons8_notification_blue;
                    item.PictureBoxThongBao.BackColor = Color.FromArgb(219, 234, 254);
                    break;
            }
        }

        private void LoadHoatDongNoiBat()
        {
            try
            {
                // 1. Tổng số lớp học
                var danhSachLopHoc = lopHocBUS.DocDSLop();
                int tongLopHoc = danhSachLopHoc != null ? danhSachLopHoc.Count : 0;
                cardHoatDongNoiBatDashboard1.lbCardName.Text = "Tổng lớp học";
                cardHoatDongNoiBatDashboard1.lbCardValue.Text = tongLopHoc.ToString();
                cardHoatDongNoiBatDashboard1.lbCardGhiChu.Text = "Toàn trường";
                cardHoatDongNoiBatDashboard1.PictureBoxThongBao.Image = Properties.Resources.icons8_notification_blue;
                cardHoatDongNoiBatDashboard1.PictureBoxThongBao.BackColor = Color.FromArgb(219, 234, 254);
                cardHoatDongNoiBatDashboard1.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);

                // 2. Khen thưởng (tháng này)
                DateTime thangNayBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime thangNayKetThuc = thangNayBatDau.AddMonths(1);
                int khenThuongThangNay = DemKhenThuongThangNay(thangNayBatDau, thangNayKetThuc);
                cardHoatDongNoiBatDashboard2.lbCardName.Text = "Khen thưởng";
                cardHoatDongNoiBatDashboard2.lbCardValue.Text = khenThuongThangNay.ToString();
                cardHoatDongNoiBatDashboard2.lbCardGhiChu.Text = "Tháng này";
                cardHoatDongNoiBatDashboard2.PictureBoxThongBao.Image = Properties.Resources.icons8_winners_medal_xanhla;
                cardHoatDongNoiBatDashboard2.PictureBoxThongBao.BackColor = Color.FromArgb(220, 252, 231);
                cardHoatDongNoiBatDashboard2.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);

                // 3. Thông báo chưa đọc
                int thongBaoChuaDoc = DemThongBaoChuaDoc();
                cardHoatDongNoiBatDashboard3.lbCardName.Text = "Thông báo mới";
                cardHoatDongNoiBatDashboard3.lbCardValue.Text = thongBaoChuaDoc.ToString();
                cardHoatDongNoiBatDashboard3.lbCardGhiChu.Text = "Chưa đọc";
                cardHoatDongNoiBatDashboard3.PictureBoxThongBao.Image = Properties.Resources.icons8_increase_profits_cam;
                cardHoatDongNoiBatDashboard3.PictureBoxThongBao.BackColor = Color.FromArgb(255, 237, 213);
                cardHoatDongNoiBatDashboard3.lbCardValue.ForeColor = Color.FromArgb(234, 88, 12);

                // 4. Điểm TB tăng (so với kỳ trước)
                double diemTBTang = TinhDiemTBTang();
                cardHoatDongNoiBatDashboard4.lbCardName.Text = "Điểm TB tăng";
                cardHoatDongNoiBatDashboard4.lbCardValue.Text = diemTBTang >= 0 ? $"+{diemTBTang:F1}" : diemTBTang.ToString("F1");
                cardHoatDongNoiBatDashboard4.lbCardGhiChu.Text = "So với kì trước";
                cardHoatDongNoiBatDashboard4.PictureBoxThongBao.Image = Properties.Resources.icons8_timetable_tim;
                cardHoatDongNoiBatDashboard4.PictureBoxThongBao.BackColor = Color.FromArgb(243, 232, 255);
                cardHoatDongNoiBatDashboard4.lbCardValue.ForeColor = Color.FromArgb(147, 51, 234);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load hoạt động nổi bật: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int DemHocSinhMoi(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                // Đếm học sinh có tài khoản được tạo trong khoảng thời gian
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT COUNT(DISTINCT hs.MaHocSinh) 
                                   FROM HocSinh hs
                                   LEFT JOIN NguoiDung nd ON hs.TenDangNhap = nd.TenDangNhap
                                   WHERE hs.TrangThai IN ('Đang học', 'Đang học(CT)', 'Nghỉ học')
                                   AND nd.NgayTao IS NOT NULL
                                   AND nd.NgayTao >= @TuNgay 
                                   AND nd.NgayTao < @DenNgay";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay);
                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        private int DemKhenThuongThangNay(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                // Lấy tất cả khen thưởng
                var danhSachKTKL = khenThuongBUS.LayDanhSachCoLoc("Khen thưởng", -1, -1, null);
                if (danhSachKTKL == null) return 0;
                
                // Đếm khen thưởng trong tháng (Loai = "Khen thưởng")
                return danhSachKTKL.Count(kt => 
                    kt.Loai == "Khen thưởng" &&
                    kt.NgayApDung >= tuNgay &&
                    kt.NgayApDung < denNgay);
            }
            catch
            {
                return 0;
            }
        }

        private int DemSuKienSapToi()
        {
            try
            {
                // Lấy thông báo có ngày hết hạn trong tương lai (sự kiện sắp tới)
                var danhSachThongBao = thongBaoBUS.LayDanhSachThongBao(
                    SessionManager.TenDangNhap, null, null, null, 1, 1000);
                
                if (danhSachThongBao == null) return 0;
                
                DateTime now = DateTime.Now;
                return danhSachThongBao.Count(tb => 
                    tb.NgayHetHan.HasValue && 
                    tb.NgayHetHan.Value > now &&
                    (tb.LoaiThongBao == "SU_KIEN" || tb.LoaiThongBao == "LICH_TRINH"));
            }
            catch
            {
                return 0;
            }
        }

        private int DemThongBaoChuaDoc()
        {
            try
            {
                // Đếm thông báo chưa đọc trong 7 ngày gần đây
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT COUNT(DISTINCT tb.MaThongBao) 
                                   FROM ThongBao tb
                                   INNER JOIN NguoiNhanThongBao nntb ON tb.MaThongBao = nntb.MaThongBao
                                   WHERE nntb.TenDangNhap = @TenDangNhap 
                                   AND nntb.TrangThaiDoc = 0
                                   AND tb.NgayTao >= @TuNgay";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", SessionManager.TenDangNhap);
                        cmd.Parameters.AddWithValue("@TuNgay", DateTime.Now.AddDays(-7));
                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                // Nếu lỗi, lấy từ BUS (phương án dự phòng)
                try
                {
                    var danhSachThongBao = thongBaoBUS.LayDanhSachThongBao(
                        SessionManager.TenDangNhap, null, null, null, 1, 100);
                    return danhSachThongBao?.Count ?? 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        private double TinhDiemTBTang()
        {
            try
            {
                // Lấy học kỳ hiện tại và học kỳ trước
                var danhSachHocKy = hocKyDAO.DocDSHocKy();
                if (danhSachHocKy == null || danhSachHocKy.Count < 2) return 0.0;
                
                var hocKyHienTai = danhSachHocKy.OrderByDescending(hk => hk.MaHocKy).FirstOrDefault();
                var hocKyTruoc = danhSachHocKy.OrderByDescending(hk => hk.MaHocKy).Skip(1).FirstOrDefault();
                
                if (hocKyHienTai == null || hocKyTruoc == null) return 0.0;
                
                // Tính điểm TB trung bình của học kỳ hiện tại
                double diemTBHienTai = TinhDiemTBTrungBinh(hocKyHienTai.MaHocKy);
                
                // Tính điểm TB trung bình của học kỳ trước
                double diemTBTruoc = TinhDiemTBTrungBinh(hocKyTruoc.MaHocKy);
                
                return diemTBHienTai - diemTBTruoc;
            }
            catch
            {
                return 0.0;
            }
        }

        private double TinhDiemTBTrungBinh(int maHocKy)
        {
            try
            {
                // Lấy thống kê xếp loại để tính điểm TB
                var thongKe = xepLoaiBUS.ThongKeXepLoaiTongKet(maHocKy, null);
                int tongHS = thongKe.Values.Sum();
                if (tongHS == 0) return 0.0;
                
                // Tính điểm TB dựa trên xếp loại (ước tính)
                // Giỏi = 8.5, Khá = 7.0, Trung bình = 5.5, Yếu = 4.0
                double tongDiem = thongKe["Giỏi"] * 8.5 + 
                                 thongKe["Khá"] * 7.0 + 
                                 thongKe["Trung bình"] * 5.5 + 
                                 thongKe["Yếu"] * 4.0;
                
                return tongDiem / tongHS;
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Tính phần trăm thay đổi số lượng học sinh so với 3 tháng trước
        /// </summary>
        private double TinhPhanTramThayDoiHocSinh()
        {
            try
            {
                // Lấy số lượng học sinh hiện tại
                var danhSachHocSinh = hocSinhBLL.GetAllHocSinh();
                int soLuongHienTai = danhSachHocSinh != null ? danhSachHocSinh.Count : 0;
                
                if (soLuongHienTai == 0) return 0.0;

                // Tính số lượng học sinh 3 tháng trước (ước tính dựa trên dữ liệu)
                // Sử dụng query trực tiếp đến database để đếm học sinh có tài khoản được tạo trước 3 tháng
                DateTime baThangTruoc = DateTime.Now.AddMonths(-3);
                int soLuongBaThangTruoc = DemHocSinhTheoThoiGian(baThangTruoc);

                if (soLuongBaThangTruoc == 0)
                {
                    // Nếu không có dữ liệu, giả sử tăng trưởng 0%
                    return 0.0;
                }

                // Tính phần trăm thay đổi
                double phanTram = ((double)(soLuongHienTai - soLuongBaThangTruoc) / soLuongBaThangTruoc) * 100;
                return phanTram;
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Tính phần trăm thay đổi số lượng giáo viên so với 3 tháng trước
        /// </summary>
        private double TinhPhanTramThayDoiGiaoVien()
        {
            try
            {
                // Lấy số lượng giáo viên hiện tại
                var danhSachGiaoVien = giaoVienBUS.DocDSGiaoVien();
                int soLuongHienTai = danhSachGiaoVien != null ? danhSachGiaoVien.Count : 0;
                
                if (soLuongHienTai == 0) return 0.0;

                // Tính số lượng giáo viên 3 tháng trước (ước tính dựa trên dữ liệu)
                DateTime baThangTruoc = DateTime.Now.AddMonths(-3);
                int soLuongBaThangTruoc = DemGiaoVienTheoThoiGian(baThangTruoc);

                if (soLuongBaThangTruoc == 0)
                {
                    // Nếu không có dữ liệu, giả sử tăng trưởng 0%
                    return 0.0;
                }

                // Tính phần trăm thay đổi
                double phanTram = ((double)(soLuongHienTai - soLuongBaThangTruoc) / soLuongBaThangTruoc) * 100;
                return phanTram;
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Đếm số lượng học sinh có tài khoản được tạo trước thời điểm chỉ định
        /// </summary>
        private int DemHocSinhTheoThoiGian(DateTime thoiDiem)
        {
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    // Đếm học sinh có TenDangNhap và tài khoản được tạo trước thời điểm chỉ định
                    // Nếu không có trường NgayTao, sẽ đếm dựa trên MaHocSinh (giả sử học sinh cũ có mã nhỏ hơn)
                    string sql = @"SELECT COUNT(DISTINCT hs.MaHocSinh) 
                                   FROM HocSinh hs
                                   LEFT JOIN NguoiDung nd ON hs.TenDangNhap = nd.TenDangNhap
                                   WHERE hs.TrangThai IN ('Đang học', 'Đang học(CT)', 'Nghỉ học')
                                   AND (nd.NgayTao IS NULL OR nd.NgayTao <= @ThoiDiem)";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ThoiDiem", thoiDiem);
                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                // Nếu lỗi, trả về 0 hoặc ước tính dựa trên số lượng hiện tại
                var danhSachHocSinh = hocSinhBLL.GetAllHocSinh();
                int soLuongHienTai = danhSachHocSinh != null ? danhSachHocSinh.Count : 0;
                // Ước tính: giả sử 95% số lượng hiện tại là từ 3 tháng trước
                return (int)(soLuongHienTai * 0.95);
            }
        }

        /// <summary>
        /// Đếm số lượng giáo viên có tài khoản được tạo trước thời điểm chỉ định
        /// </summary>
        private int DemGiaoVienTheoThoiGian(DateTime thoiDiem)
        {
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    // Đếm giáo viên có TenDangNhap và tài khoản được tạo trước thời điểm chỉ định
                    string sql = @"SELECT COUNT(DISTINCT gv.MaGiaoVien) 
                                   FROM GiaoVien gv
                                   LEFT JOIN NguoiDung nd ON gv.MaGiaoVien = nd.TenDangNhap
                                   WHERE gv.TrangThai = 'Đang giảng dạy'
                                   AND (nd.NgayTao IS NULL OR nd.NgayTao <= @ThoiDiem)";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ThoiDiem", thoiDiem);
                        object result = cmd.ExecuteScalar();
                        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                // Nếu lỗi, trả về 0 hoặc ước tính dựa trên số lượng hiện tại
                var danhSachGiaoVien = giaoVienBUS.DocDSGiaoVien();
                int soLuongHienTai = danhSachGiaoVien != null ? danhSachGiaoVien.Count : 0;
                // Ước tính: giả sử 95% số lượng hiện tại là từ 3 tháng trước
                return (int)(soLuongHienTai * 0.95);
            }
        }

        private void LoadHocKyToCombobox()
        {
            try
            {
                // Lấy danh sách học kỳ từ database (đã sắp xếp theo thứ tự mới nhất)
                List<HocKyDTO> dsHocKy = hocKyDAO.DocDSHocKy();

                // Xóa dữ liệu cũ trong combobox
                cbHocKiNamHoc.Items.Clear();
                cbHocKiNamHoc.DisplayMember = "Text";
                cbHocKiNamHoc.ValueMember = "Value";

                // Tạo danh sách các item để thêm vào combobox
                var itemsToAdd = new List<dynamic>();

                // Tìm học kỳ mới nhất có dữ liệu
                int indexHocKyMoiNhatCoDuLieu = -1;

                for (int i = 0; i < dsHocKy.Count; i++)
                {
                    HocKyDTO hocKy = dsHocKy[i];
                    string displayText = $"{hocKy.TenHocKy} - {hocKy.MaNamHoc}";

                    var item = new { Text = displayText, Value = hocKy.MaHocKy };
                    itemsToAdd.Add(item);

                    // Kiểm tra học kỳ này có dữ liệu không
                    if (indexHocKyMoiNhatCoDuLieu == -1 && hocKyDAO.KiemTraHocKyCoXepLoai(hocKy.MaHocKy))
                    {
                        indexHocKyMoiNhatCoDuLieu = i;
                    }
                }

                // Thêm tất cả items vào combobox
                foreach (var item in itemsToAdd)
                {
                    cbHocKiNamHoc.Items.Add(item);
                }

                // Chọn học kỳ mới nhất có dữ liệu, nếu không có thì chọn học kỳ mới nhất
                if (cbHocKiNamHoc.Items.Count > 0)
                {
                    if (indexHocKyMoiNhatCoDuLieu >= 0)
                    {
                        cbHocKiNamHoc.SelectedIndex = indexHocKyMoiNhatCoDuLieu;
                    }
                    else
                    {
                        cbHocKiNamHoc.SelectedIndex = 0; // Chọn học kỳ mới nhất (dù chưa có dữ liệu)
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu học kỳ: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongKeXepLoai()
        {
            try
            {
                if (cbHocKiNamHoc.SelectedItem == null)
                {
                    // Reset về 0 nếu không có học kỳ được chọn
                    ResetProgressBars();
                    return;
                }

                // Lấy mã học kỳ được chọn
                dynamic selectedHocKy = cbHocKiNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                // Lấy thống kê xếp loại tổng kết theo học kỳ (toàn trường, không phân biệt lớp)
                Dictionary<string, int> thongKe = xepLoaiBUS.ThongKeXepLoaiTongKet(maHocKy, null);

                // Tính tổng số học sinh
                int tongSoHocSinh = thongKe.Values.Sum();

                if (tongSoHocSinh > 0)
                {
                    // Tính phần trăm và cập nhật từng ProgressBar
                    double phanTramGioi = (double)thongKe["Giỏi"] / tongSoHocSinh * 100;
                    double phanTramKha = (double)thongKe["Khá"] / tongSoHocSinh * 100;
                    double phanTramTrungBinh = (double)thongKe["Trung bình"] / tongSoHocSinh * 100;
                    double phanTramYeu = (double)thongKe["Yếu"] / tongSoHocSinh * 100;

                    // Cập nhật ProgressBar Giỏi
                    CapNhatProgressBar(pgbGioi, lblGioiPhanTram, phanTramGioi, thongKe["Giỏi"]);

                    // Cập nhật ProgressBar Khá
                    CapNhatProgressBar(pgbKha, lblKhaPhanTram, phanTramKha, thongKe["Khá"]);

                    // Cập nhật ProgressBar Trung bình
                    CapNhatProgressBar(pgbTrungBinh, lblTrungBinhPhanTram, phanTramTrungBinh, thongKe["Trung bình"]);

                    // Cập nhật ProgressBar Yếu
                    CapNhatProgressBar(pgbYeu, lblYeuPhanTram, phanTramYeu, thongKe["Yếu"]);
                }
                else
                {
                    // Không có dữ liệu, reset về 0
                    ResetProgressBars();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê xếp loại: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetProgressBars();
            }
        }

        private void CapNhatProgressBar(Guna.UI2.WinForms.Guna2ProgressBar progressBar, Label label, double phanTram, int soLuong)
        {
            // Set Maximum = 1000 để có độ chính xác cao (1000 = 100%)
            progressBar.Maximum = 1000;

            // Tính Value = phần trăm * 10 (vì Maximum = 1000)
            int value = (int)Math.Round(phanTram * 10);
            progressBar.Value = Math.Min(value, 1000); // Đảm bảo không vượt quá Maximum

            // Hiển thị label với format: số lượng (phần trăm%)
            label.Text = $"{soLuong} học sinh ({phanTram:0.0}%)";
        }

        /// <summary>
        /// Reset tất cả ProgressBar về 0
        /// </summary>
        private void ResetProgressBars()
        {
            pgbGioi.Maximum = 1000;
            pgbGioi.Value = 0;
            lblGioiPhanTram.Text = "0 học sinh (0.0%)";

            pgbKha.Maximum = 1000;
            pgbKha.Value = 0;
            lblKhaPhanTram.Text = "0 học sinh (0.0%)";

            pgbTrungBinh.Maximum = 1000;
            pgbTrungBinh.Value = 0;
            lblTrungBinhPhanTram.Text = "0 học sinh (0.0%)";

            pgbYeu.Maximum = 1000;
            pgbYeu.Value = 0;
            lblYeuPhanTram.Text = "0 học sinh (0.0%)";
        }

        private void pgbGioi_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbHocKiNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThongKeXepLoai();
        }
    }
}
