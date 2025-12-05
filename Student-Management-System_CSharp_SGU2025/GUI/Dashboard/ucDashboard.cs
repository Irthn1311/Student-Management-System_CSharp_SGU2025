using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
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

namespace Student_Management_System_CSharp_SGU2025.GUI.Dashboard
{
    public partial class ucDashboard : UserControl
    {
        private XepLoaiBUS xepLoaiBUS;
        private HocKyDAO hocKyDAO;

        public ucDashboard()
        {
            InitializeComponent();
            hocKyDAO = new HocKyDAO();
            xepLoaiBUS = new XepLoaiBUS();
        }

        private void cardHoatDongNoiBatDashboard3_Load(object sender, EventArgs e)
        {

        }

        private void ucDashboard_Load(object sender, EventArgs e)
        {
            LoadHocKyToCombobox();

            recentActivityItemThongBao1.lbTextName.Text = "Họp phụ huynh lớp 12";
            recentActivityItemThongBao1.lbNote.Text = "Ngày 15/10/2025 - 8:00 AM";
            recentActivityItemThongBao1.PictureBoxThongBao.Image = Properties.Resources.icons8_notification_blue;
            recentActivityItemThongBao1.PictureBoxThongBao.BackColor = Color.FromArgb(219,234,254);

            recentActivityItemThongBao2.lbTextName.Text = "Khen thưởng học sinh giỏi";
            recentActivityItemThongBao2.lbNote.Text = "Ngày 12/10/2025";
            recentActivityItemThongBao2.PictureBoxThongBao.Image = Properties.Resources.icons8_winners_medal_xanhla;
            recentActivityItemThongBao2.PictureBoxThongBao.BackColor = Color.FromArgb(220, 252, 231);

            recentActivityItemThongBao3.lbTextName.Text = "Báo cáo kết quả học tập";
            recentActivityItemThongBao3.lbNote.Text = "Ngày 10/10/2025";
            recentActivityItemThongBao3.PictureBoxThongBao.Image = Properties.Resources.icons8_increase_profits_cam;
            recentActivityItemThongBao3.PictureBoxThongBao.BackColor = Color.FromArgb(255, 237, 213);

            recentActivityItemThongBao4.lbTextName.Text = "Lịch thi giữa kỳ";
            recentActivityItemThongBao4.lbNote.Text = "Ngày 8/10/2025";
            recentActivityItemThongBao4.PictureBoxThongBao.Image = Properties.Resources.icons8_timetable_tim;
            recentActivityItemThongBao4.PictureBoxThongBao.BackColor = Color.FromArgb(243, 232, 255);

            cardHoatDongNoiBatDashboard1.lbCardName.Text = "Học sinh mới";
            cardHoatDongNoiBatDashboard1.lbCardValue.Text = "42";
            cardHoatDongNoiBatDashboard1.lbCardGhiChu.Text = "Tuần này";
            cardHoatDongNoiBatDashboard1.PictureBoxThongBao.Image = Properties.Resources.icons8_notification_blue;
            cardHoatDongNoiBatDashboard1.PictureBoxThongBao.BackColor = Color.FromArgb(219, 234, 254);
            cardHoatDongNoiBatDashboard1.lbCardValue.ForeColor = Color.FromArgb(30,136,229);

            cardHoatDongNoiBatDashboard2.lbCardName.Text = "Khen thưởng";
            cardHoatDongNoiBatDashboard2.lbCardValue.Text = "18";
            cardHoatDongNoiBatDashboard2.lbCardGhiChu.Text = "Tháng này";
            cardHoatDongNoiBatDashboard2.PictureBoxThongBao.Image = Properties.Resources.icons8_winners_medal_xanhla;
            cardHoatDongNoiBatDashboard2.PictureBoxThongBao.BackColor = Color.FromArgb(220, 252, 231);
            cardHoatDongNoiBatDashboard2.lbCardValue.ForeColor = Color.FromArgb(22,163,74);

            cardHoatDongNoiBatDashboard3.lbCardName.Text = "Sự kiện";
            cardHoatDongNoiBatDashboard3.lbCardValue.Text = "5";
            cardHoatDongNoiBatDashboard3.lbCardGhiChu.Text = "Sắp tới";
            cardHoatDongNoiBatDashboard3.PictureBoxThongBao.Image = Properties.Resources.icons8_increase_profits_cam;
            cardHoatDongNoiBatDashboard3.PictureBoxThongBao.BackColor = Color.FromArgb(255, 237, 213);
            cardHoatDongNoiBatDashboard3.lbCardValue.ForeColor = Color.FromArgb(234,88,12);

            cardHoatDongNoiBatDashboard4.lbCardName.Text = "Điểm TB tăng";
            cardHoatDongNoiBatDashboard4.lbCardValue.Text = "+0.4";
            cardHoatDongNoiBatDashboard4.lbCardGhiChu.Text = "So với kì trước";
            cardHoatDongNoiBatDashboard4.PictureBoxThongBao.Image = Properties.Resources.icons8_timetable_tim;
            cardHoatDongNoiBatDashboard4.PictureBoxThongBao.BackColor = Color.FromArgb(243, 232, 255);
            cardHoatDongNoiBatDashboard4.lbCardValue.ForeColor = Color.FromArgb(147,51,234);

            // 🆕 Thêm button "Gửi yêu cầu chuyển lớp" cho PHỤ HUYNH
            ThemButtonGuiYeuCauChuyenLop();
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

        /// <summary>
        /// 🆕 Thêm button "Gửi yêu cầu chuyển lớp" CHỈ cho PHỤ HUYNH và HỌC SINH
        /// </summary>
        private void ThemButtonGuiYeuCauChuyenLop()
        {
            try
            {
                // ✅ KIỂM TRA QUYỀN: Chỉ hiển thị cho Phụ huynh hoặc Học sinh
                bool coQuyen = false;
                string tenDangNhap = Utils.SessionManager.TenDangNhap;
                string vaiTro = Utils.SessionManager.VaiTro ?? "";

                // CÁCH 1: Kiểm tra theo VaiTro từ SessionManager
                if (!string.IsNullOrEmpty(vaiTro))
                {
                    string vaiTroUpper = vaiTro.ToUpper();
                    if (vaiTroUpper.Contains("HỌC SINH") || vaiTroUpper.Contains("HOCSINH") || 
                        vaiTroUpper.Contains("PHỤ HUYNH") || vaiTroUpper.Contains("PHUHUYNH") ||
                        vaiTroUpper == "HỌC SINH" || vaiTroUpper == "PHỤ HUYNH")
                    {
                        coQuyen = true;
                    }
                }

                // CÁCH 2: Fallback - Kiểm tra theo tên đăng nhập
                if (!coQuyen && !string.IsNullOrEmpty(tenDangNhap))
                {
                    string upper = tenDangNhap.ToUpper();
                    // HS1001, PH001, hoặc chứa từ khóa
                    if (upper.StartsWith("HS") || upper.StartsWith("PH") || 
                        upper.Contains("HOCSINH") || upper.Contains("PHUHUYNH"))
                    {
                        coQuyen = true;
                    }
                }

                // CÁCH 3: Kiểm tra theo PermissionHelper (nếu không phải Admin/GV)
                if (!coQuyen)
                {
                    try
                    {
                        // Nếu KHÔNG có quyền admin → có thể là HS/PH
                        bool coQuyenAdmin = Utils.PermissionHelper.HasAccessToFunction("QLLOPHOC");
                        if (!coQuyenAdmin)
                        {
                            coQuyen = true; // Không phải admin → cho phép
                        }
                    }
                    catch
                    {
                        // Nếu lỗi, không cho phép
                    }
                }

                // ❌ Nếu KHÔNG có quyền → Không hiển thị button
                if (!coQuyen)
                {
                    Console.WriteLine($"ℹ️ Button 'Gửi yêu cầu' không hiển thị - User: {tenDangNhap}, Role: {vaiTro}");
                    return;
                }

                // ✅ CÓ QUYỀN → Tạo button
                Console.WriteLine($"✅ Button 'Gửi yêu cầu' sẽ hiển thị cho User: {tenDangNhap}, Role: {vaiTro}");

                // ✅ CÓ QUYỀN → Tạo button
                Guna.UI2.WinForms.Guna2Button btnGuiYeuCau = new Guna.UI2.WinForms.Guna2Button();
                btnGuiYeuCau.Name = "btnGuiYeuCauChuyenLop";
                btnGuiYeuCau.Text = "📤 Gửi yêu cầu chuyển lớp";
                btnGuiYeuCau.Size = new Size(250, 80);
                btnGuiYeuCau.FillColor = Color.FromArgb(34, 197, 94); // Màu xanh lá
                btnGuiYeuCau.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                btnGuiYeuCau.ForeColor = Color.White;
                btnGuiYeuCau.BorderRadius = 12;
                btnGuiYeuCau.Cursor = Cursors.Hand;

                // ĐẶT VỊ TRÍ: Góc phải trên
                btnGuiYeuCau.Location = new Point(920, 30);

                // Gắn sự kiện click
                btnGuiYeuCau.Click += BtnGuiYeuCauChuyenLop_Click;

                // Thêm vào dashboard
                this.Controls.Add(btnGuiYeuCau);
                btnGuiYeuCau.BringToFront();

                // Debug log
                Console.WriteLine("✅ Button 'Gửi yêu cầu chuyển lớp' đã được thêm cho Phụ huynh/Học sinh!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi khi thêm button Gửi yêu cầu: {ex.Message}");
            }
        }

        /// <summary>
        /// 🆕 Event khi click button "Gửi yêu cầu chuyển lớp"
        /// </summary>
        private void BtnGuiYeuCauChuyenLop_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Lấy tên đăng nhập từ SessionManager (đúng tên đăng nhập trong hệ thống)
                string tenDangNhap = Utils.SessionManager.TenDangNhap;

                if (string.IsNullOrEmpty(tenDangNhap))
                {
                    MessageBox.Show("Không xác định được người dùng hiện tại.\n\nVui lòng đăng nhập lại.", 
                        "Lỗi", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }

                // Mở form gửi yêu cầu cho học sinh/phụ huynh
                PhuHuynh.FormPhuHuynhGuiYeuCau form = new PhuHuynh.FormPhuHuynhGuiYeuCau(tenDangNhap);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form gửi yêu cầu: {ex.Message}", 
                    "Lỗi", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
    }
}
