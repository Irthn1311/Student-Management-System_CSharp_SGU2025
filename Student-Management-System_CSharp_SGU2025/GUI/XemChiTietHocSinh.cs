using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QRCoder;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class XemChiTietHocSinh : Form
    {
        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        private LopHocBUS lopHocBUS;
        private PhanLopBLL phanLopBLL;
        private HocKyBUS hocKyBUS;

        private int maHocSinh;
        private HocSinhDTO hocSinh;

        public XemChiTietHocSinh(int maHocSinh)
        {
            InitializeComponent();
            this.maHocSinh = maHocSinh;

            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            lopHocBUS = new LopHocBUS();
            phanLopBLL = new PhanLopBLL();
            hocKyBUS = new HocKyBUS();

            LoadThongTinHocSinh();
        }

        private void LoadThongTinHocSinh()
        {
            try
            {
                // Lấy thông tin học sinh
                hocSinh = hocSinhBLL.GetHocSinhById(maHocSinh);
                if (hocSinh == null)
                {
                    MessageBox.Show($"Không tìm thấy học sinh với mã {maHocSinh}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // === THÔNG TIN CÁ NHÂN ===
                lblMaLop.Text = $"Mã HS: {hocSinh.MaHS}";
                lblTenLop.Text = $"Họ và tên: {hocSinh.HoTen}";
                lblKhoi.Text = $"Ngày sinh: {hocSinh.NgaySinh:dd/MM/yyyy}";
                lblSiSo.Text = $"Giới tính: {hocSinh.GioiTinh}";
                lblGVCN.Text = $"SĐT: {hocSinh.SdtHS ?? "N/A"}";
                lblSDTGV.Text = $"Email: {hocSinh.Email ?? "N/A"}";
                lblEmailGV.Text = $"Trạng thái: {hocSinh.TrangThai}";

                // Định dạng màu cho trạng thái
                if (hocSinh.TrangThai == "Đang học")
                {
                    lblEmailGV.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else if (hocSinh.TrangThai == "Nghỉ học" || hocSinh.TrangThai.Contains("Nghỉ"))
                {
                    lblEmailGV.ForeColor = Color.FromArgb(220, 38, 38);
                }
                else
                {
                    lblEmailGV.ForeColor = Color.FromArgb(107, 114, 128);
                }

                // === THÔNG TIN LỚP HIỆN TẠI ===
                LoadThongTinLop();

                // === DANH SÁCH PHỤ HUYNH ===
                LoadDanhSachPhuHuynh();

                // === HIỂN THỊ ẢNH HỌC SINH ===
                LoadAnhHocSinh();

                // === HIỂN THỊ THẺ HỌC SINH ===
                LoadTheHocSinh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin học sinh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAnhHocSinh()
        {
            try
            {
                if (hocSinh == null) return;

                string duongDanAnh = hocSinh.AnhDaiDien;
                
                // Nếu chưa có ảnh trong database, tự động phân bổ dựa trên MaHS
                if (string.IsNullOrWhiteSpace(duongDanAnh))
                {
                    int soAnh = ((hocSinh.MaHS - 1) % 4) + 1;
                    duongDanAnh = $"Images/Students/hs{soAnh}.jpg";
                }

                // Tải ảnh từ đường dẫn - thử nhiều đường dẫn khác nhau
                string fullPath = System.IO.Path.Combine(Application.StartupPath, duongDanAnh);
                
                // Thử nhiều đường dẫn khác nhau nếu không tìm thấy
                if (!System.IO.File.Exists(fullPath))
                {
                    // Thử đường dẫn từ BaseDirectory
                    fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, duongDanAnh);
                }
                
                if (!System.IO.File.Exists(fullPath))
                {
                    // Thử đường dẫn từ thư mục gốc project
                    string projectPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    fullPath = System.IO.Path.Combine(projectPath, duongDanAnh);
                }
                
                if (!System.IO.File.Exists(fullPath))
                {
                    // Thử đường dẫn relative
                    fullPath = duongDanAnh;
                }
                
                if (System.IO.File.Exists(fullPath))
                {
                    try
                    {
                        // Dispose ảnh cũ nếu có để tránh memory leak
                        if (picAnhHocSinh.Image != null)
                        {
                            Image oldImage = picAnhHocSinh.Image;
                            picAnhHocSinh.Image = null;
                            oldImage.Dispose();
                        }
                        
                        picAnhHocSinh.Image = Image.FromFile(fullPath);
                        picAnhHocSinh.SizeMode = PictureBoxSizeMode.Zoom;
                        picAnhHocSinh.BackColor = Color.White;
                    }
                    catch (Exception imgEx)
                    {
                        Console.WriteLine($"Lỗi khi load file ảnh: {imgEx.Message}");
                        // Nếu lỗi, tạo placeholder
                        picAnhHocSinh.Image = null;
                        picAnhHocSinh.BackColor = Color.FromArgb(240, 240, 240);
                    }
                }
                else
                {
                    // Nếu không tìm thấy ảnh, hiển thị placeholder
                    if (picAnhHocSinh.Image != null)
                    {
                        Image oldImage = picAnhHocSinh.Image;
                        picAnhHocSinh.Image = null;
                        oldImage.Dispose();
                    }
                    picAnhHocSinh.Image = null;
                    picAnhHocSinh.BackColor = Color.FromArgb(240, 240, 240);
                    Console.WriteLine($"Không tìm thấy ảnh tại: {duongDanAnh}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải ảnh học sinh: {ex.Message}");
                if (picAnhHocSinh.Image != null)
                {
                    Image oldImage = picAnhHocSinh.Image;
                    picAnhHocSinh.Image = null;
                    oldImage.Dispose();
                }
                picAnhHocSinh.Image = null;
                picAnhHocSinh.BackColor = Color.FromArgb(240, 240, 240);
            }
        }

        private void LoadThongTinLop()
        {
            try
            {
                // Lấy học kỳ hiện tại
                int maHocKyHienTai = 0;
                List<HocKyDTO> dsHocKy = hocKyBUS.DocDSHocKy();
                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    var hocKyDangDienRa = dsHocKy.FirstOrDefault(hk => hk.TrangThai == "Đang diễn ra");
                    if (hocKyDangDienRa != null)
                    {
                        maHocKyHienTai = hocKyDangDienRa.MaHocKy;
                    }
                    else
                    {
                        var hocKyMoiNhat = dsHocKy.OrderByDescending(hk => hk.NgayBD).FirstOrDefault();
                        if (hocKyMoiNhat != null)
                        {
                            maHocKyHienTai = hocKyMoiNhat.MaHocKy;
                        }
                    }
                }

                if (maHocKyHienTai > 0)
                {
                    int maLop = phanLopBLL.GetLopByHocSinh(maHocSinh, maHocKyHienTai);
                    if (maLop > 0)
                    {
                        var lop = lopHocBUS.LayLopTheoId(maLop);
                        if (lop != null)
                        {
                            tenLopHienTai = lop.tenLop;
                            lblLopHienTai.Text = $"Lớp hiện tại: {lop.tenLop}";
                            
                            // Lấy thông tin giáo viên chủ nhiệm
                            if (!string.IsNullOrEmpty(lop.maGVCN))
                            {
                                try
                                {
                                    GiaoVienBUS giaoVienBUS = new GiaoVienBUS();
                                    GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(lop.maGVCN);
                                    if (gv != null)
                                    {
                                        lblGVCNLop.Text = $"GVCN: {gv.HoTen}";
                                        lblSDTGVCN.Text = $"SĐT GVCN: {gv.SoDienThoai ?? "N/A"}";
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    else
                    {
                        lblLopHienTai.Text = "Lớp hiện tại: Chưa phân lớp";
                        lblLopHienTai.ForeColor = Color.FromArgb(234, 179, 8);
                    }
                }
                else
                {
                    lblLopHienTai.Text = "Lớp hiện tại: Chưa có học kỳ";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy thông tin lớp: {ex.Message}");
            }
        }

        private void LoadDanhSachPhuHuynh()
        {
            try
            {
                dgvPhuHuynh.Rows.Clear();

                var dsQuanHe = hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(maHocSinh);
                if (dsQuanHe == null || dsQuanHe.Count == 0)
                {
                    dgvPhuHuynh.Rows.Add("Chưa có phụ huynh", "", "", "");
                    return;
                }

                foreach (var qh in dsQuanHe)
                {
                    dgvPhuHuynh.Rows.Add(
                        qh.phuHuynh.HoTen,
                        qh.phuHuynh.SoDienThoai ?? "N/A",
                        qh.phuHuynh.Email ?? "N/A",
                        qh.moiQuanHe
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách phụ huynh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string tenLopHienTai = "";

        private void LoadTheHocSinh()
        {
            try
            {
                if (hocSinh == null) return;

                // Load ảnh học sinh vào thẻ
                LoadAnhTheHocSinh();

                // Mã học sinh
                lblTheMaHS.Text = "Mã HS:";
                lblTheMaSo.Text = hocSinh.MaHS.ToString();

                // Họ tên (chỉ tên, không có label)
                lblTheHoTen.Text = hocSinh.HoTen;

                // Ngày sinh (với icon calendar)
                lblTheNgaySinh.Text = $"📅 {hocSinh.NgaySinh:dd/MM/yyyy}";

                // Giới tính (với icon person)
                lblTheGioiTinh.Text = $"👤 {hocSinh.GioiTinh}";

                // Trạng thái
                if (hocSinh.TrangThai == "Đang học")
                {
                    lblTheTrangThai.Text = "Đang học";
                    lblTheTrangThai.BackColor = Color.FromArgb(22, 163, 74);
                }
                else if (hocSinh.TrangThai == "Nghỉ học" || hocSinh.TrangThai.Contains("Nghỉ"))
                {
                    lblTheTrangThai.Text = "Nghỉ học";
                    lblTheTrangThai.BackColor = Color.FromArgb(220, 38, 38);
                }
                else
                {
                    lblTheTrangThai.Text = hocSinh.TrangThai;
                    lblTheTrangThai.BackColor = Color.FromArgb(107, 114, 128);
                }
                
                // Refresh panel để vẽ lại
                panelTheHocSinh.Invalidate();

                // Tạo QR code placeholder
                CreateQRCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load thẻ học sinh: {ex.Message}");
            }
        }

        private void LoadAnhTheHocSinh()
        {
            try
            {
                if (hocSinh == null)
                {
                    picAnhTheHocSinh.Image = CreateStudentPhotoPlaceholder();
                    return;
                }

                string duongDanAnh = hocSinh.AnhDaiDien;
                
                // Nếu chưa có ảnh, tự động phân bổ
                if (string.IsNullOrWhiteSpace(duongDanAnh))
                {
                    int soAnh = ((hocSinh.MaHS - 1) % 4) + 1;
                    duongDanAnh = $"Images/Students/hs{soAnh}.jpg";
                }

                // Tải ảnh từ đường dẫn
                string fullPath = System.IO.Path.Combine(Application.StartupPath, duongDanAnh);
                
                // Thử nhiều đường dẫn khác nhau
                if (!System.IO.File.Exists(fullPath))
                {
                    // Thử đường dẫn tương đối
                    fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, duongDanAnh);
                }
                
                if (!System.IO.File.Exists(fullPath))
                {
                    // Thử đường dẫn từ thư mục gốc project
                    string projectPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    fullPath = System.IO.Path.Combine(projectPath, duongDanAnh);
                }

                if (System.IO.File.Exists(fullPath))
                {
                    try
                    {
                        using (Image img = Image.FromFile(fullPath))
                        {
                            // Resize ảnh để vừa với PictureBox với crop center
                            picAnhTheHocSinh.Image = ResizeImageWithCrop(img, 160, 180);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi load ảnh: {ex.Message}");
                        picAnhTheHocSinh.Image = CreateStudentPhotoPlaceholder();
                    }
                }
                else
                {
                    // Tạo placeholder nếu không có ảnh
                    picAnhTheHocSinh.Image = CreateStudentPhotoPlaceholder();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải ảnh thẻ học sinh: {ex.Message}");
                picAnhTheHocSinh.Image = CreateStudentPhotoPlaceholder();
            }
        }

        private Image ResizeImageWithCrop(Image img, int width, int height)
        {
            // Tính toán để crop center và resize
            double ratioX = (double)width / img.Width;
            double ratioY = (double)height / img.Height;
            double ratio = Math.Max(ratioX, ratioY);

            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // Fill background
                g.FillRectangle(new SolidBrush(Color.FromArgb(200, 220, 240)), 0, 0, width, height);

                // Draw image centered
                int x = (width - newWidth) / 2;
                int y = (height - newHeight) / 2;
                g.DrawImage(img, x, y, newWidth, newHeight);
            }
            return bmp;
        }


        private Image CreateStudentPhotoPlaceholder()
        {
            Bitmap bmp = new Bitmap(180, 200);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                
                // Gradient background (màu xám nhạt)
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, 0, 180, 200),
                    Color.FromArgb(240, 240, 240),
                    Color.FromArgb(220, 220, 220),
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, 0, 0, 180, 200);
                }

                // Vẽ chữ cái viết tắt nếu có tên học sinh
                if (hocSinh != null && !string.IsNullOrEmpty(hocSinh.HoTen))
                {
                    string[] parts = hocSinh.HoTen.Split(' ');
                    string initials = "";
                    if (parts.Length >= 2)
                    {
                        initials = parts[parts.Length - 2].Substring(0, 1).ToUpper() + 
                                  parts[parts.Length - 1].Substring(0, 1).ToUpper();
                    }
                    else if (parts.Length == 1)
                    {
                        initials = parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();
                    }

                    if (!string.IsNullOrEmpty(initials))
                    {
                        using (Font font = new Font("Segoe UI", 48, FontStyle.Bold))
                        {
                            using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 150, 150)))
                            {
                                StringFormat sf = new StringFormat
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Center
                                };
                                g.DrawString(initials, font, brush, new RectangleF(0, 0, 180, 200), sf);
                            }
                        }
                    }
                }
            }
            return bmp;
        }

        private void CreateQRCode()
        {
            try
            {
                if (hocSinh == null) return;

                // Tạo dữ liệu QR code từ thông tin học sinh
                string qrData = $"HS{hocSinh.MaHS}|{hocSinh.HoTen}|{tenLopHienTai}|{hocSinh.Email ?? ""}";
                
                // Sử dụng QRCoder để tạo QR code thực
                try
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(5);
                    
                    // Resize về kích thước 110x110
                    Bitmap resized = new Bitmap(qrCodeImage, new Size(110, 110));
                    picQRCode.Image = resized;
                    
                    // Dispose các đối tượng không cần thiết
                    qrCodeImage.Dispose();
                }
                catch (Exception qrEx)
                {
                    // Nếu không có thư viện QRCoder, tạo placeholder
                    Console.WriteLine($"Không thể tạo QR code thực: {qrEx.Message}. Sử dụng placeholder.");
                    CreateQRCodePlaceholder(qrData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tạo QR code: {ex.Message}");
                CreateQRCodePlaceholder($"HS{hocSinh?.MaHS}");
            }
        }

        private void CreateQRCodePlaceholder(string qrData)
        {
            // Tạo QR code placeholder chuyên nghiệp hơn
            Bitmap qrCode = new Bitmap(110, 110);
            using (Graphics g = Graphics.FromImage(qrCode))
            {
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Random rnd = new Random(qrData.GetHashCode()); // Seed consistent dựa trên dữ liệu

                int cellSize = 5;
                int padding = 10;
                
                // Vẽ pattern QR code
                for (int x = padding; x < 110 - padding; x += cellSize)
                {
                    for (int y = padding; y < 110 - padding; y += cellSize)
                    {
                        if (rnd.Next(2) == 0)
                        {
                            g.FillRectangle(Brushes.Black, x, y, cellSize, cellSize);
                        }
                    }
                }

                // Vẽ 3 góc vuông lớn (đặc trưng của QR code) - chuyên nghiệp hơn
                Pen blackPen = new Pen(Color.Black, 2);
                int cornerSize = 20;
                int cornerPadding = 5;

                // Góc trên trái
                g.DrawRectangle(blackPen, cornerPadding, cornerPadding, cornerSize, cornerSize);
                g.DrawRectangle(blackPen, cornerPadding + 3, cornerPadding + 3, cornerSize - 6, cornerSize - 6);
                g.FillRectangle(Brushes.Black, cornerPadding + 6, cornerPadding + 6, cornerSize - 12, cornerSize - 12);
                
                // Góc trên phải
                g.DrawRectangle(blackPen, 110 - cornerPadding - cornerSize, cornerPadding, cornerSize, cornerSize);
                g.DrawRectangle(blackPen, 110 - cornerPadding - cornerSize + 3, cornerPadding + 3, cornerSize - 6, cornerSize - 6);
                g.FillRectangle(Brushes.Black, 110 - cornerPadding - cornerSize + 6, cornerPadding + 6, cornerSize - 12, cornerSize - 12);
                
                // Góc dưới trái
                g.DrawRectangle(blackPen, cornerPadding, 110 - cornerPadding - cornerSize, cornerSize, cornerSize);
                g.DrawRectangle(blackPen, cornerPadding + 3, 110 - cornerPadding - cornerSize + 3, cornerSize - 6, cornerSize - 6);
                g.FillRectangle(Brushes.Black, cornerPadding + 6, 110 - cornerPadding - cornerSize + 6, cornerSize - 12, cornerSize - 12);

                // Vẽ border cho QR code
                g.DrawRectangle(new Pen(Color.FromArgb(200, 200, 200), 1), 0, 0, 109, 109);
            }

            picQRCode.Image = qrCode;
        }

        private void panelTheHocSinh_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Panel panel = sender as Panel;
                if (panel == null) return;

                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // Vẽ border và shadow cho thẻ
                int radius = 8;
                Rectangle rect = new Rectangle(2, 2, panel.Width - 4, panel.Height - 4);

                // Shadow
                for (int i = 0; i < 3; i++)
                {
                    using (GraphicsPath shadowPath = CreateRoundedRectanglePath(
                        new Rectangle(rect.X + i, rect.Y + i, rect.Width, rect.Height), radius))
                    {
                        using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(20 - i * 5, 0, 0, 0)))
                        {
                            g.FillPath(shadowBrush, shadowPath);
                        }
                    }
                }

                // Border tròn
                using (GraphicsPath path = CreateRoundedRectanglePath(rect, radius))
                {
                    // Border
                    using (Pen borderPen = new Pen(Color.FromArgb(200, 200, 200), 1))
                    {
                        g.DrawPath(borderPen, path);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi vẽ panel thẻ: {ex.Message}");
            }
        }

        private void panelTheBanner_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Panel panel = sender as Panel;
                if (panel == null) return;

                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Vẽ border tròn cho banner (chỉ góc trên)
                int radius = 8;
                Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                    path.AddLine(rect.Right - radius * 2, rect.Y, rect.Right, rect.Y);
                    path.AddLine(rect.Right, rect.Y, rect.Right, rect.Bottom);
                    path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
                    path.AddLine(rect.X, rect.Bottom, rect.X, rect.Y + radius * 2);
                    path.CloseAllFigures();

                    // Fill banner với màu xanh
                    using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(30, 136, 229)))
                    {
                        g.FillPath(bgBrush, path);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi vẽ banner: {ex.Message}");
            }
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        private void picAnhTheHocSinh_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                PictureBox pic = sender as PictureBox;
                if (pic == null) return;

                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Vẽ border tròn cho ảnh học sinh
                using (GraphicsPath path = CreateRoundedRectanglePath(
                    new Rectangle(2, 2, pic.Width - 4, pic.Height - 4), 8))
                {
                    using (Pen borderPen = new Pen(Color.FromArgb(150, 150, 150), 2))
                    {
                        g.DrawPath(borderPen, path);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi vẽ border ảnh: {ex.Message}");
            }
        }

        private void picQRCode_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                PictureBox pic = sender as PictureBox;
                if (pic == null) return;

                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Vẽ border cho QR code
                using (Pen borderPen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, pic.Width - 1, pic.Height - 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi vẽ border QR: {ex.Message}");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUploadAnh_Click(object sender, EventArgs e)
        {
            try
            {
                if (hocSinh == null)
                {
                    MessageBox.Show("Không có thông tin học sinh để cập nhật ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở dialog chọn ảnh
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    // Kiểm tra file có tồn tại không
                    if (!File.Exists(selectedFilePath))
                    {
                        MessageBox.Show("File ảnh không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Kiểm tra định dạng ảnh
                    string extension = Path.GetExtension(selectedFilePath).ToLower();
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".bmp")
                    {
                        MessageBox.Show("Vui lòng chọn file ảnh hợp lệ (jpg, jpeg, png, bmp).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra kích thước file (tối đa 5MB)
                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    if (fileInfo.Length > 5 * 1024 * 1024) // 5MB
                    {
                        MessageBox.Show("File ảnh quá lớn. Vui lòng chọn file nhỏ hơn 5MB.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tạo thư mục Images/Students nếu chưa có
                    string imagesFolder = Path.Combine(Application.StartupPath, "Images", "Students");
                    if (!Directory.Exists(imagesFolder))
                    {
                        Directory.CreateDirectory(imagesFolder);
                    }

                    // Tạo tên file mới dựa trên mã học sinh
                    string newFileName = $"hs_{hocSinh.MaHS}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
                    string newFilePath = Path.Combine(imagesFolder, newFileName);
                    string relativePath = $"Images/Students/{newFileName}";

                    // Xóa ảnh cũ nếu có (trừ các ảnh mặc định hs1.jpg, hs2.jpg, etc.)
                    if (!string.IsNullOrWhiteSpace(hocSinh.AnhDaiDien))
                    {
                        // Chỉ xóa nếu là ảnh đã upload (có dạng hs_{MaHS}_...) hoặc không phải ảnh mặc định
                        bool isDefaultImage = hocSinh.AnhDaiDien.StartsWith("Images/Students/hs") && 
                                             !hocSinh.AnhDaiDien.Contains($"hs_{hocSinh.MaHS}_") &&
                                             (hocSinh.AnhDaiDien.EndsWith("hs1.jpg") || 
                                              hocSinh.AnhDaiDien.EndsWith("hs2.jpg") || 
                                              hocSinh.AnhDaiDien.EndsWith("hs3.jpg") || 
                                              hocSinh.AnhDaiDien.EndsWith("hs4.jpg"));
                        
                        if (!isDefaultImage)
                        {
                            string oldFilePath = Path.Combine(Application.StartupPath, hocSinh.AnhDaiDien);
                            if (File.Exists(oldFilePath))
                            {
                                try
                                {
                                    File.Delete(oldFilePath);
                                }
                                catch { } // Bỏ qua nếu không xóa được
                            }
                        }
                    }

                    // Copy file ảnh mới vào thư mục
                    File.Copy(selectedFilePath, newFilePath, true);

                    // Cập nhật đường dẫn ảnh trong database
                    hocSinh.AnhDaiDien = relativePath;
                    bool updateSuccess = hocSinhBLL.UpdateHocSinh(hocSinh);

                    if (updateSuccess)
                    {
                        // Reload ảnh
                        LoadAnhHocSinh();
                        LoadAnhTheHocSinh();
                        MessageBox.Show("Cập nhật ảnh thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật ảnh thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Xóa file vừa copy nếu cập nhật thất bại
                        try
                        {
                            if (File.Exists(newFilePath))
                                File.Delete(newFilePath);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi upload ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

