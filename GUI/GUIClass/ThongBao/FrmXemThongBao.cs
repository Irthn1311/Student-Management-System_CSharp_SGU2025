using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DAO;
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

namespace Student_Management_System_CSharp_SGU2025.GUI.GUIClass.ThongBao
{
    public partial class FrmXemThongBao : Form
    {
        private ThongBaoBUS thongBaoBUS;
        private ThongBaoDAO thongBaoDAO;
        private KhenThuongKyLuatBUS khenThuongBUS;
        private HocSinhBLL hocSinhBLL;
        private PhanLopDAO phanLopDAO;
        private HocKyDAO hocKyDAO;
        private List<ThongBaoDTO> danhSachThongBao;
        private List<KhenThuongKyLuatDTO> danhSachKhenThuongKyLuat;

        public FrmXemThongBao()
        {
            InitializeComponent();
            thongBaoBUS = new ThongBaoBUS();
            thongBaoDAO = new ThongBaoDAO();
            khenThuongBUS = new KhenThuongKyLuatBUS();
            hocSinhBLL = new HocSinhBLL();
            phanLopDAO = new PhanLopDAO();
            hocKyDAO = new HocKyDAO();
            danhSachThongBao = new List<ThongBaoDTO>();
            danhSachKhenThuongKyLuat = new List<KhenThuongKyLuatDTO>();

            this.Load += FrmXemThongBao_Load;
        }

        private void FrmXemThongBao_Load(object sender, EventArgs e)
        {
            SetupUI();
            
            // Đợi UI render xong rồi mới load dữ liệu
            this.BeginInvoke(new Action(() =>
            {
                // Nếu là học sinh hoặc phụ huynh, load khen thưởng kỷ luật trước
                string vaiTro = SessionManager.VaiTro?.ToUpper() ?? "";
                if (vaiTro.Contains("HOC_SINH") || vaiTro.Contains("HỌC SINH") || 
                    vaiTro.Contains("PHU_HUYNH") || vaiTro.Contains("PHỤ HUYNH"))
                {
                    LoadKhenThuongKyLuat();
                }
                
                // Sau đó load thông báo
                LoadThongBao();
            }));
        }

        private void SetupUI()
        {
            // Cấu hình form
            this.Text = "Thông báo của tôi";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 900;
            this.Height = 700;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Tạo panel chứa danh sách thông báo
            Panel pnlContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true,
                BackColor = Color.FromArgb(248, 250, 252)
            };
            this.Controls.Add(pnlContainer);

            // Label tiêu đề
            Label lblTitle = new Label
            {
                Text = "📢 Thông báo & Đánh giá",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            pnlContainer.Controls.Add(lblTitle);

            // Panel danh sách thông báo
            FlowLayoutPanel flpThongBao = new FlowLayoutPanel
            {
                Name = "flpThongBao",
                Location = new Point(20, 70),
                Width = pnlContainer.Width - 60,
                Height = pnlContainer.Height - 100,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlContainer.Controls.Add(flpThongBao);
        }

        private void LoadThongBao()
        {
            try
            {
                // Lấy danh sách thông báo theo vai trò
                danhSachThongBao = GetThongBaoTheoVaiTro();

                // Sắp xếp theo ngày tạo mới nhất
                danhSachThongBao = danhSachThongBao.OrderByDescending(tb => tb.NgayTao).ToList();

                // Log để debug
                Console.WriteLine($"[FrmXemThongBao] Đã lấy được {danhSachThongBao.Count} thông báo cho {SessionManager.TenDangNhap} (Vai trò: {SessionManager.VaiTro})");
                Console.WriteLine($"[FrmXemThongBao] Số lượng khen thưởng kỷ luật: {danhSachKhenThuongKyLuat?.Count ?? 0}");

                DisplayThongBao();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thông báo: {ex.Message}\n\nChi tiết: {ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"[FrmXemThongBao] Lỗi: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private List<ThongBaoDTO> GetThongBaoTheoVaiTro()
        {
            string tenDangNhap = SessionManager.TenDangNhap;

            try
            {
                // Sử dụng BUS để lấy thông báo - BUS sẽ tự động xử lý logic theo vai trò
                // (giống như Dashboard đang làm - dòng 154-157)
                var danhSachThongBao = thongBaoBUS.LayDanhSachThongBao(
                    tenDangNhap,
                    null, null, null, 1, 1000  // Lấy tất cả (1000 items)
                );
                
                Console.WriteLine($"[FrmXemThongBao] GetThongBaoTheoVaiTro: Lấy được {danhSachThongBao?.Count ?? 0} thông báo cho {tenDangNhap} (Vai trò: {SessionManager.VaiTro})");
                
                // Log chi tiết để debug
                if (danhSachThongBao != null && danhSachThongBao.Count > 0)
                {
                    var thongBaoTheoPhamVi = danhSachThongBao.GroupBy(tb => tb.PhamVi ?? "UNKNOWN");
                    foreach (var group in thongBaoTheoPhamVi)
                    {
                        Console.WriteLine($"[FrmXemThongBao] {SessionManager.VaiTro}: {group.Count()} thông báo với PhamVi = {group.Key}");
                    }
                    
                    // Log thêm cho giáo viên
                    string vaiTro = SessionManager.VaiTro?.ToUpper() ?? "";
                    if (vaiTro.Contains("GIAO_VIEN") || vaiTro.Contains("GIÁO VIÊN"))
                    {
                        var thongBaoTheoDoiTuong = danhSachThongBao.GroupBy(tb => tb.DoiTuongNhan ?? "UNKNOWN");
                        foreach (var group in thongBaoTheoDoiTuong)
                        {
                            Console.WriteLine($"[FrmXemThongBao] Giáo viên: {group.Count()} thông báo với DoiTuongNhan = {group.Key}");
                        }
                    }
                }
                
                // Nếu null, trả về danh sách rỗng
                if (danhSachThongBao == null)
                {
                    Console.WriteLine("[FrmXemThongBao] WARNING: danhSachThongBao là null!");
                    return new List<ThongBaoDTO>();
                }
                
                return danhSachThongBao;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FrmXemThongBao] ERROR khi lấy thông báo: {ex.Message}\n{ex.StackTrace}");
                MessageBox.Show($"Lỗi khi lấy thông báo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<ThongBaoDTO>();
            }
        }

        private void LoadKhenThuongKyLuat()
        {
            try
            {
                string maHocSinh = GetMaHocSinhHienTai();
                if (string.IsNullOrEmpty(maHocSinh))
                {
                    Console.WriteLine("[FrmXemThongBao] LoadKhenThuongKyLuat: Không lấy được mã học sinh");
                    return;
                }

                Console.WriteLine($"[FrmXemThongBao] LoadKhenThuongKyLuat: Mã học sinh = {maHocSinh}");

                // Lấy danh sách khen thưởng kỷ luật của học sinh (chỉ lấy đã duyệt)
                var allKTKL = khenThuongBUS.LayDanhSachCoLoc(null, -1, -1, null);
                danhSachKhenThuongKyLuat = allKTKL
                    .Where(kt => kt.MaHocSinh == int.Parse(maHocSinh) && kt.TrangThaiDuyet == "Đã duyệt")
                    .OrderByDescending(kt => kt.NgayApDung)
                    .ToList();
                
                Console.WriteLine($"[FrmXemThongBao] LoadKhenThuongKyLuat: Lấy được {danhSachKhenThuongKyLuat.Count} khen thưởng kỷ luật");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FrmXemThongBao] Lỗi khi load khen thưởng kỷ luật: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void DisplayThongBao()
        {
            var flp = this.Controls.Find("flpThongBao", true).FirstOrDefault() as FlowLayoutPanel;
            if (flp == null)
            {
                Console.WriteLine("[FrmXemThongBao] ERROR: Không tìm thấy flpThongBao!");
                return;
            }

            Console.WriteLine($"[FrmXemThongBao] DisplayThongBao: Số lượng thông báo = {danhSachThongBao?.Count ?? 0}");
            
            flp.Controls.Clear();

            // Hiển thị khen thưởng kỷ luật trước (nếu có)
            if (danhSachKhenThuongKyLuat != null && danhSachKhenThuongKyLuat.Count > 0)
            {
                foreach (var item in danhSachKhenThuongKyLuat)
                {
                    Panel pnlItem = CreateKhenThuongKyLuatPanel(item);
                    flp.Controls.Add(pnlItem);
                }

                // Thêm đường phân cách
                Panel separator = new Panel
                {
                    Height = 2,
                    Width = flp.Width - 40,
                    BackColor = Color.FromArgb(226, 232, 240),
                    Margin = new Padding(0, 10, 0, 10)
                };
                flp.Controls.Add(separator);
            }

            // Hiển thị thông báo
            if (danhSachThongBao.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "📭 Không có thông báo nào",
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.FromArgb(148, 163, 184),
                    AutoSize = true,
                    Padding = new Padding(20)
                };
                flp.Controls.Add(lblEmpty);
                return;
            }

            foreach (var tb in danhSachThongBao)
            {
                Panel pnlItem = CreateThongBaoPanel(tb);
                flp.Controls.Add(pnlItem);
            }
        }

        private Panel CreateThongBaoPanel(ThongBaoDTO thongBao)
        {
            Panel pnl = new Panel
            {
                Width = 800,
                Height = 120,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15),
                Padding = new Padding(20),
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.Hand
            };

            // Tạo border radius effect
            pnl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(pnl.ClientRectangle, 10))
                {
                    e.Graphics.FillPath(new SolidBrush(Color.White), path);
                    e.Graphics.DrawPath(new Pen(Color.FromArgb(226, 232, 240), 1), path);
                }
            };

            // Icon loại thông báo
            PictureBox pbIcon = new PictureBox
            {
                Width = 40,
                Height = 40,
                Location = new Point(15, 15),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = GetIconBackColor(thongBao.LoaiThongBao)
            };
            pbIcon.Image = GetIconImage(thongBao.LoaiThongBao);
            pnl.Controls.Add(pbIcon);

            // Tiêu đề
            Label lblTitle = new Label
            {
                Text = thongBao.TieuDe,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(70, 15),
                AutoSize = true,
                MaximumSize = new Size(650, 0)
            };
            pnl.Controls.Add(lblTitle);

            // Nội dung (rút gọn)
            string noiDungRutGon = thongBao.NoiDung.Length > 100
                ? thongBao.NoiDung.Substring(0, 100) + "..."
                : thongBao.NoiDung;
            Label lblContent = new Label
            {
                Text = noiDungRutGon,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(70, 45),
                AutoSize = true,
                MaximumSize = new Size(650, 40)
            };
            pnl.Controls.Add(lblContent);

            // Ngày tạo
            Label lblDate = new Label
            {
                Text = $"📅 {thongBao.NgayTao:dd/MM/yyyy HH:mm}",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(70, 85),
                AutoSize = true
            };
            pnl.Controls.Add(lblDate);

            // Đối tượng nhận
            Label lblDoiTuong = new Label
            {
                Text = $"👥 {thongBao.DoiTuongNhan}",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(300, 85),
                AutoSize = true
            };
            pnl.Controls.Add(lblDoiTuong);

            // Sự kiện click để xem chi tiết
            pnl.Click += (s, e) => ShowThongBaoDetail(thongBao);
            foreach (Control ctrl in pnl.Controls)
            {
                ctrl.Click += (s, e) => ShowThongBaoDetail(thongBao);
            }

            return pnl;
        }

        private Panel CreateKhenThuongKyLuatPanel(KhenThuongKyLuatDTO item)
        {
            Panel pnl = new Panel
            {
                Width = 800,
                Height = 100,
                BackColor = item.Loai == "Khen thưởng" ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242),
                Margin = new Padding(0, 0, 0, 15),
                Padding = new Padding(20),
                BorderStyle = BorderStyle.None
            };

            // Icon
            Label lblIcon = new Label
            {
                Text = item.Loai == "Khen thưởng" ? "🏆" : "⚠️",
                Font = new Font("Segoe UI", 24),
                Location = new Point(15, 15),
                AutoSize = true
            };
            pnl.Controls.Add(lblIcon);

            // Loại
            Label lblLoai = new Label
            {
                Text = item.Loai.ToUpper(),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = item.Loai == "Khen thưởng" ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38),
                Location = new Point(70, 15),
                AutoSize = true
            };
            pnl.Controls.Add(lblLoai);

            // Nội dung
            Label lblNoiDung = new Label
            {
                Text = item.NoiDung,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(70, 40),
                AutoSize = true,
                MaximumSize = new Size(650, 0)
            };
            pnl.Controls.Add(lblNoiDung);

            // Ngày áp dụng
            Label lblNgay = new Label
            {
                Text = $"📅 {item.NgayApDung:dd/MM/yyyy}",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(70, 70),
                AutoSize = true
            };
            pnl.Controls.Add(lblNgay);

            return pnl;
        }

        private void ShowThongBaoDetail(ThongBaoDTO thongBao)
        {
            Form frmDetail = new Form
            {
                Text = "Chi tiết thông báo",
                Width = 600,
                Height = 500,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Panel pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true,
                BackColor = Color.White
            };
            frmDetail.Controls.Add(pnlContent);

            // Tiêu đề
            Label lblTitle = new Label
            {
                Text = thongBao.TieuDe,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                AutoSize = true,
                MaximumSize = new Size(540, 0),
                Location = new Point(20, 20)
            };
            pnlContent.Controls.Add(lblTitle);

            // Thông tin phụ
            Label lblInfo = new Label
            {
                Text = $"📅 {thongBao.NgayTao:dd/MM/yyyy HH:mm}\n👥 {thongBao.DoiTuongNhan}\n📌 {thongBao.LoaiThongBao}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                MaximumSize = new Size(540, 0),
                Location = new Point(20, lblTitle.Bottom + 10)
            };
            pnlContent.Controls.Add(lblInfo);

            // Đường kẻ
            Panel separator = new Panel
            {
                Height = 1,
                Width = 540,
                BackColor = Color.FromArgb(226, 232, 240),
                Location = new Point(20, lblInfo.Bottom + 15)
            };
            pnlContent.Controls.Add(separator);

            // Nội dung
            Label lblContent = new Label
            {
                Text = thongBao.NoiDung,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true,
                MaximumSize = new Size(540, 0),
                Location = new Point(20, separator.Bottom + 15)
            };
            pnlContent.Controls.Add(lblContent);

            // Đánh dấu đã đọc
            MarkAsRead(thongBao.MaThongBao);

            frmDetail.ShowDialog();
        }

        private void MarkAsRead(int maThongBao)
        {
            try
            {
                // TODO: Implement DanhDauDaDoc in ThongBaoBUS
                // thongBaoBUS.DanhDauDaDoc(maThongBao, SessionManager.TenDangNhap);
            }
            catch { }
        }

        private string GetMaHocSinhHienTai()
        {
            if (SessionManager.VaiTro == "HOC_SINH")
            {
                // Tìm MaHocSinh từ TenDangNhap
                try
                {
                    var danhSachHocSinh = hocSinhBLL.GetAllHocSinh();
                    var hocSinh = danhSachHocSinh.FirstOrDefault(hs => hs.TenDangNhap == SessionManager.TenDangNhap);
                    
                    if (hocSinh != null)
                    {
                        return hocSinh.MaHS.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi lấy thông tin học sinh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
            else if (SessionManager.VaiTro == "PHU_HUYNH")
            {
                // Lấy mã học sinh từ phụ huynh (giả sử có con đầu tiên)
                // TODO: Cần có logic lấy danh sách con của phụ huynh
                return SessionManager.TenDangNhap; // Tạm thời
            }
            return null;
        }

        private (int maHocSinh, int maLop, int maHocKy)? GetPhanLopHienTai(int maHocSinh)
        {
            try
            {
                // Lấy học kỳ đang diễn ra
                var danhSachHocKy = hocKyDAO.DocDSHocKy();
                var hocKyHienTai = danhSachHocKy
                    .Where(hk => DateTime.Now >= hk.NgayBD && DateTime.Now <= hk.NgayKT)
                    .OrderByDescending(hk => hk.MaHocKy)
                    .FirstOrDefault();

                if (hocKyHienTai == null) return null;

                // Lấy phân lớp của học sinh trong học kỳ hiện tại
                var danhSachPhanLop = phanLopDAO.LayTatCaPhanLop();
                return danhSachPhanLop
                    .FirstOrDefault(pl => pl.maHocSinh == maHocSinh && pl.maHocKy == hocKyHienTai.MaHocKy);
            }
            catch
            {
                return null;
            }
        }

        private string GetKhoiFromMaLop(int maLop)
        {
            try
            {
                // Lấy tên lớp từ database
                using (var conn = Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase.ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT TenLop FROM Lop WHERE MaLop = @MaLop";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        var tenLop = cmd.ExecuteScalar()?.ToString();
                        
                        if (!string.IsNullOrEmpty(tenLop))
                        {
                            // Trích xuất khối từ tên lớp (ví dụ: "10A1" -> "10")
                            if (tenLop.Length >= 2)
                            {
                                return tenLop.Substring(0, 2);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        private string GetTenLopFromMaLop(int maLop)
        {
            try
            {
                // Lấy tên lớp từ database
                using (var conn = Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase.ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT TenLop FROM Lop WHERE MaLop = @MaLop";
                    using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        return cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        private Color GetIconBackColor(string loaiThongBao)
        {
            switch (loaiThongBao?.ToUpper())
            {
                case "KHEN_THUONG":
                case "KHENTHUONG":
                    return Color.FromArgb(220, 252, 231);
                case "KY_LUAT":
                case "KYLUAT":
                    return Color.FromArgb(254, 242, 242);
                case "HOC_TAP":
                    return Color.FromArgb(219, 234, 254);
                case "SU_KIEN":
                case "SUKIEN":
                    return Color.FromArgb(243, 232, 255);
                default:
                    return Color.FromArgb(241, 245, 249);
            }
        }

        private Image GetIconImage(string loaiThongBao)
        {
            // Trả về icon tương ứng với loại thông báo (giống Dashboard)
            switch (loaiThongBao?.ToUpper())
            {
                case "HOP_PHU_HUYNH":
                case "HOP":
                    return Properties.Resources.icons8_notification_blue;
                case "KHEN_THUONG":
                case "KHENTHUONG":
                    return Properties.Resources.icons8_winners_medal_xanhla;
                case "BAO_CAO":
                case "BAOCAO":
                    return Properties.Resources.icons8_increase_profits_cam;
                case "LICH_TRINH":
                case "LICHTRINH":
                case "SU_KIEN":
                case "SUKIEN":
                    return Properties.Resources.icons8_timetable_tim;
                case "HOC_TAP":
                case "HOCTAP":
                    return Properties.Resources.icons8_notification_blue;
                case "KY_LUAT":
                case "KYLUAT":
                    return Properties.Resources.icons8_winners_medal_xanhla; // Có thể đổi icon khác
                default:
                    return Properties.Resources.icons8_notification_blue;
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
