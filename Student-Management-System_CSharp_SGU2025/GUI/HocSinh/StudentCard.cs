using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.Utils;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.HocSinh
{
    /// <summary>
    /// UserControl thẻ học sinh đẹp mắt với ảnh đại diện
    /// </summary>
    public partial class StudentCard : UserControl
    {
        private HocSinhDTO hocSinh;
        private string tenLop = "";
        private string tenGVCN = "";

        public StudentCard()
        {
            InitializeComponent();
            SetupCardStyle();
        }

        public void LoadStudentInfo(HocSinhDTO hs, string lop = "", string gvcn = "")
        {
            this.hocSinh = hs;
            this.tenLop = lop;
            this.tenGVCN = gvcn;

            DisplayStudentInfo();
        }

        private void SetupCardStyle()
        {
            // Thiết lập màu nền xanh nhạt đẹp mắt
            this.BackColor = Color.FromArgb(224, 242, 254); // Xanh sky nhạt
            this.Padding = new Padding(0);
            
            // Vẽ viền bo góc
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

        // Import hàm tạo vùng bo góc
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ header với gradient xanh dương đẹp mắt
            Rectangle rect = panelHeader.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.FromArgb(30, 64, 175), // Xanh dương đậm
                Color.FromArgb(59, 130, 246), // Xanh dương sáng
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            
            // Vẽ đường viền phân cách màu vàng nổi bật
            using (Pen pen = new Pen(Color.FromArgb(252, 211, 77), 2)) // Vàng vàng
            {
                e.Graphics.DrawLine(pen, 0, panelHeader.Height - 2, panelHeader.Width, panelHeader.Height - 2);
            }
        }

        private void LoadLogo()
        {
            // Load ảnh SGU từ Resources (đã được set trong Designer)
            // Nếu cần, có thể thêm viền vàng cho logo
            if (picLogo.Image != null)
            {
                // Logo đã được load từ Resources trong Designer
                // Có thể thêm viền vàng nếu muốn
            }
        }

        private void DisplayStudentInfo()
        {
            if (hocSinh == null) return;

            // Load logo SGU từ Resources (đã được set trong Designer)
            LoadLogo();

            // Load ảnh đại diện học sinh
            LoadStudentPhoto();

            // Hiển thị thông tin theo format thẻ sinh viên với màu sắc nổi bật
            lblIDNumber.Text = $"Mã học sinh: {hocSinh.MaHS:D6}";
            lblIDNumber.ForeColor = Color.FromArgb(30, 64, 175); // Xanh dương đậm
            
            lblHoTen.Text = $"Họ và tên: {hocSinh.HoTen.ToUpper()}";
            lblHoTen.ForeColor = Color.FromArgb(15, 23, 42); // Xanh đen đậm
            
            lblNgaySinh.Text = $"Ngày sinh: {hocSinh.NgaySinh:dd/MM/yyyy}";
            lblNgaySinh.ForeColor = Color.FromArgb(30, 64, 175); // Xanh dương đậm
            
            // Tính ngày hết hạn (5 năm từ ngày hiện tại)
            DateTime ngayHetHan = DateTime.Now.AddYears(5);
            lblNgayHetHan.Text = $"Ngày hết hạn: {ngayHetHan:dd/MM/yyyy}";
            lblNgayHetHan.ForeColor = Color.FromArgb(30, 64, 175); // Xanh dương đậm

            // Hiển thị lớp với màu vàng nổi bật
            if (!string.IsNullOrEmpty(tenLop))
            {
                lblLop.Text = $"Lớp: {tenLop}";
                lblLop.ForeColor = Color.FromArgb(217, 119, 6); // Cam vàng đậm
                lblLop.Visible = true;
            }
            else
            {
                lblLop.Text = "Lớp: Chưa phân lớp";
                lblLop.ForeColor = Color.FromArgb(107, 114, 128); // Xám
                lblLop.Visible = true;
            }

            // Hiển thị GVCN
            if (!string.IsNullOrEmpty(tenGVCN))
            {
                lblGVCN.Text = $"Giáo viên chủ nhiệm: {tenGVCN}";
                lblGVCN.ForeColor = Color.FromArgb(30, 64, 175); // Xanh dương đậm
                lblGVCN.Visible = true;
            }
            else
            {
                lblGVCN.Text = "Giáo viên chủ nhiệm: Chưa phân công";
                lblGVCN.ForeColor = Color.FromArgb(107, 114, 128); // Xám
                lblGVCN.Visible = true;
            }

            // Sinh mã QR với đầy đủ thông tin
            GenerateQRCode();
            
            // Vẽ lại để hiển thị watermark
            this.Invalidate();
        }

        private void LoadStudentPhoto()
        {
            // Dispose ảnh cũ nếu có để tránh memory leak
            if (picAvatar.Image != null)
            {
                Image oldImage = picAvatar.Image;
                picAvatar.Image = null;
                oldImage.Dispose();
            }

            // Load ảnh học sinh thật nếu có
            try
            {
                if (!string.IsNullOrEmpty(hocSinh.AnhDaiDien))
                {
                    if (System.IO.File.Exists(hocSinh.AnhDaiDien))
                    {
                        // Load ảnh từ file
                        using (Image originalImage = Image.FromFile(hocSinh.AnhDaiDien))
                        {
                            // Resize và crop ảnh để vừa khung 140x180
                            Image resizedImage = ResizeAndCropImage(originalImage, 140, 180);
                            picAvatar.Image = resizedImage;
                        }
                    }
                    else
                    {
                        CreatePhotoPlaceholder();
                    }
                }
                else
                {
                    CreatePhotoPlaceholder();
                }
            }
            catch (Exception ex)
            {
                // Nếu lỗi, tạo placeholder
                CreatePhotoPlaceholder();
            }
        }

        private Image ResizeAndCropImage(Image image, int width, int height)
        {
            // Tính toán để crop và resize ảnh giữ nguyên tỷ lệ
            double ratioX = (double)width / image.Width;
            double ratioY = (double)height / image.Height;
            double ratio = Math.Max(ratioX, ratioY); // Lấy tỷ lệ lớn hơn để đảm bảo phủ kín
            
            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);
            
            // Tạo bitmap mới
            Bitmap result = new Bitmap(width, height);
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            
            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                
                // Tính vị trí để crop từ giữa
                int x = (newWidth - width) / 2;
                int y = (newHeight - height) / 2;
                
                // Vẽ ảnh đã resize vào bitmap mới
                g.DrawImage(image, new Rectangle(-x, -y, newWidth, newHeight));
            }
            
            return result;
        }

        private void CreatePhotoPlaceholder()
        {
            // Tạo ảnh placeholder với màu xanh và vàng đẹp mắt
            Bitmap placeholder = new Bitmap(140, 180);
            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Nền xanh nhạt với gradient
                Rectangle rect = new Rectangle(0, 0, 140, 180);
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(191, 219, 254), // Xanh nhạt
                    Color.FromArgb(147, 197, 253), // Xanh trung bình
                    LinearGradientMode.Vertical))
                {
                    GraphicsPath path = CreateRoundedRectanglePath(rect, 5);
                    g.FillPath(brush, path);
                }
                
                // Vẽ viền vàng nổi bật
                using (Pen pen = new Pen(Color.FromArgb(252, 211, 77), 2)) // Vàng vàng
                {
                    DrawRoundedRectangle(g, pen, 1, 1, 138, 178, 5);
                }
                
                // Vẽ chữ "Đang tải..." màu xanh đậm
                using (Font font = new Font("Segoe UI", 9, FontStyle.Bold))
                {
                    SizeF textSize = g.MeasureString("Đang tải...", font);
                    g.DrawString("Đang tải...", font, 
                        new SolidBrush(Color.FromArgb(30, 64, 175)), // Xanh dương đậm
                        (140 - textSize.Width) / 2, (180 - textSize.Height) / 2);
                }
            }
            picAvatar.Image = placeholder;
        }

        private void DrawRoundedRectangle(Graphics g, Pen pen, float x, float y, float width, float height, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            path.AddArc(x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);
            path.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            g.DrawPath(pen, path);
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.X + rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.X + rect.Width - radius * 2, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return "HS";

            string[] words = fullName.Trim().Split(' ');
            if (words.Length >= 2)
            {
                return $"{words[0][0]}{words[words.Length - 1][0]}".ToUpper();
            }
            return fullName.Substring(0, Math.Min(2, fullName.Length)).ToUpper();
        }

        private void GenerateQRCode()
        {
            try
            {
                // Tạo hình ảnh thẻ học sinh
                Bitmap cardImage = RenderStudentCardImage();
                
                // Convert ảnh thẻ học sinh sang base64 với chất lượng nén tốt
                string cardImageBase64 = ConvertCardImageToBase64(cardImage);
                
                // Tạo data URL với hình ảnh thẻ học sinh (dùng JPEG vì đã convert sang JPEG)
                string dataUrl = $"data:image/jpeg;base64,{cardImageBase64}";
                
                System.Diagnostics.Debug.WriteLine($"QR Data URL Length: {dataUrl.Length}");
                
                // Kiểm tra độ dài và điều chỉnh ECC level
                QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.M; // Dùng M để cân bằng
                
                // Nếu quá dài, giảm chất lượng ảnh
                if (dataUrl.Length > 2950)
                {
                    System.Diagnostics.Debug.WriteLine("QR quá dài, giảm chất lượng ảnh");
                    cardImage = RenderStudentCardImage(smaller: true);
                    cardImageBase64 = ConvertCardImageToBase64(cardImage, quality: 60);
                    dataUrl = $"data:image/jpeg;base64,{cardImageBase64}";
                }
                
                // Tạo QR code
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(dataUrl, eccLevel);
                QRCode qrCode = new QRCode(qrCodeData);
                
                // Tạo QR code lớn và rõ
                Bitmap qrBitmap = qrCode.GetGraphic(
                    pixelsPerModule: 6,
                    darkColor: Color.Black,
                    lightColor: Color.White,
                    drawQuietZones: true
                );
                
                picQR.Image = qrBitmap;
                
                // Dispose ảnh tạm
                cardImage?.Dispose();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi tạo QR code: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Thử tạo QR code đơn giản chỉ với text
                try
                {
                    string simpleText = $"Mã HS: {hocSinh.MaHS:D6}\nHọ tên: {hocSinh.HoTen}\nLớp: {tenLop}";
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(simpleText, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrBitmap = qrCode.GetGraphic(6, Color.Black, Color.White, true);
                    picQR.Image = qrBitmap;
                }
                catch
                {
                    Bitmap placeholder = new Bitmap(130, 130);
                    using (Graphics g = Graphics.FromImage(placeholder))
                    {
                        g.Clear(Color.White);
                        using (Font font = new Font("Segoe UI", 9))
                        {
                            g.DrawString("QR\nLỗi", font, Brushes.Red, 40, 50);
                        }
                    }
                    picQR.Image = placeholder;
                }
            }
        }
        
        /// <summary>
        /// Vẽ toàn bộ thẻ học sinh thành hình ảnh bitmap
        /// </summary>
        private Bitmap RenderStudentCardImage(bool smaller = false)
        {
            if (hocSinh == null)
            {
                // Trả về bitmap placeholder nếu không có dữ liệu
                Bitmap placeholder = new Bitmap(600, 356);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.White);
                    using (Font font = new Font("Segoe UI", 12))
                    {
                        g.DrawString("Không có dữ liệu", font, Brushes.Red, 250, 170);
                    }
                }
                return placeholder;
            }
            
            int width = smaller ? 450 : 600;
            int height = smaller ? 267 : 356;
            
            Bitmap bitmap = new Bitmap(width, height);
            bitmap.SetResolution(96, 96);
            
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                
                // Vẽ nền
                g.Clear(Color.FromArgb(224, 242, 254));
                
                float scale = smaller ? 0.75f : 1.0f;
                
                // Vẽ header
                Rectangle headerRect = new Rectangle(0, 0, width, (int)(90 * scale));
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    headerRect,
                    Color.FromArgb(30, 64, 175),
                    Color.FromArgb(59, 130, 246),
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(brush, headerRect);
                }
                
                // Vẽ đường viền vàng dưới header
                using (Pen pen = new Pen(Color.FromArgb(252, 211, 77), 2))
                {
                    g.DrawLine(pen, 0, headerRect.Height - 2, width, headerRect.Height - 2);
                }
                
                // Vẽ logo
                if (picLogo.Image != null)
                {
                    int logoSize = (int)(50 * scale);
                    int logoX = (int)(20 * scale);
                    int logoY = (int)(15 * scale);
                    g.DrawImage(picLogo.Image, logoX, logoY, logoSize, logoSize);
                }
                
                // Vẽ text header
                using (Font headerFont = new Font("Segoe UI", 13 * scale, FontStyle.Bold))
                using (SolidBrush headerBrush = new SolidBrush(Color.FromArgb(252, 211, 77)))
                {
                    string headerText = "TRƯỜNG THPT SÀI GÒN";
                    g.DrawString(headerText, headerFont, headerBrush, 
                        (int)(80 * scale), (int)(15 * scale));
                }
                
                using (Font subFont = new Font("Segoe UI", 8 * scale))
                using (SolidBrush subBrush = new SolidBrush(Color.White))
                {
                    string subText = "123 Đường Nguyễn Văn Cừ, Quận 5, Thành phố Hồ Chí Minh, 70000, Việt Nam";
                    g.DrawString(
     subText,
     subFont,
     subBrush,
     new RectangleF((int)(80 * scale), (int)(45 * scale), width - (int)(100 * scale), (int)(35 * scale)),
     StringFormat.GenericDefault
 );
                }
                
                // Vẽ avatar
                int avatarX = (int)(20 * scale);
                int avatarY = (int)(110 * scale);
                int avatarWidth = (int)(140 * scale);
                int avatarHeight = (int)(180 * scale);
                
                if (picAvatar.Image != null)
                {
                    g.DrawImage(picAvatar.Image, avatarX, avatarY, avatarWidth, avatarHeight);
                }
                else
                {
                    // Vẽ placeholder
                    Rectangle avatarRect = new Rectangle(avatarX, avatarY, avatarWidth, avatarHeight);
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                        avatarRect,
                        Color.FromArgb(191, 219, 254),
                        Color.FromArgb(147, 197, 253),
                        LinearGradientMode.Vertical))
                    {
                        GraphicsPath path = CreateRoundedRectanglePath(avatarRect, (int)(5 * scale));
                        g.FillPath(brush, path);
                    }
                }
                
                // Vẽ thông tin học sinh
                int infoX = (int)(180 * scale);
                int infoStartY = (int)(120 * scale);
                int lineHeight = (int)(30 * scale);
                
                using (Font labelFont = new Font("Segoe UI", 12 * scale, FontStyle.Bold))
                using (Font valueFont = new Font("Segoe UI", 12 * scale))
                {
                    // Mã học sinh
                    using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(30, 64, 175)))
                    {
                        string idText = $"Mã học sinh: {hocSinh.MaHS:D6}";
                        g.DrawString(idText, labelFont, labelBrush, 
                            (int)(16 * scale), (int)(300 * scale));
                    }
                    
                    // Họ tên
                    using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(15, 23, 42)))
                    {
                        string hoTen = hocSinh.HoTen ?? "Chưa cập nhật";
                        string nameText = $"Họ và tên: {hoTen.ToUpper()}";
                        g.DrawString(nameText, valueFont, labelBrush, infoX, infoStartY);
                    }
                    
                    // Ngày sinh
                    using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(30, 64, 175)))
                    {
                        string dobText = $"Ngày sinh: {hocSinh.NgaySinh:dd/MM/yyyy}";
                        g.DrawString(dobText, valueFont, labelBrush, infoX, infoStartY + lineHeight);
                    }
                    
                    // Ngày hết hạn
                    DateTime ngayHetHan = DateTime.Now.AddYears(5);
                    using (SolidBrush expiryBrush = new SolidBrush(Color.FromArgb(30, 64, 175)))
                    {
                        string expiryText = $"Ngày hết hạn: {ngayHetHan:dd/MM/yyyy}";
                        g.DrawString(expiryText, valueFont, expiryBrush, infoX, infoStartY + lineHeight * 2);
                    }
                    
                    // Lớp
                    using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(217, 119, 6)))
                    {
                        string lop = string.IsNullOrEmpty(tenLop) ? "Chưa phân lớp" : tenLop;
                        string classText = $"Lớp: {lop}";
                        g.DrawString(classText, valueFont, labelBrush, infoX, infoStartY + lineHeight * 3);
                    }
                    
                    // GVCN
                    using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(30, 64, 175)))
                    {
                        string gvcn = string.IsNullOrEmpty(tenGVCN) ? "Chưa phân công" : tenGVCN;
                        string gvcnText = $"Giáo viên chủ nhiệm: {gvcn}";
                        RectangleF gvcnRect = new RectangleF(infoX, infoStartY + lineHeight * 4, 
                            width - infoX - (int)(20 * scale), lineHeight * 2);
                        g.DrawString(gvcnText, valueFont, labelBrush, gvcnRect);
                    }
                }
                
                // Vẽ watermark
                DrawWatermarkOnBitmap(g, width, height, scale);
                
                // Vẽ viền
                using (Pen pen = new Pen(Color.FromArgb(30, 64, 175), 2))
                {
                    GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(0, 0, width - 1, height - 1), (int)(10 * scale));
                    g.DrawPath(pen, path);
                }
                
                using (Pen pen = new Pen(Color.FromArgb(252, 211, 77), 1))
                {
                    GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(2, 2, width - 5, height - 5), (int)(8 * scale));
                    g.DrawPath(pen, path);
                }
            }
            
            return bitmap;
        }
        
        private void DrawWatermarkOnBitmap(Graphics g, int width, int height, float scale)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(40, 147, 197, 253)))
            {
                // Vẽ logo watermark
                int logoSize = (int)(60 * scale);
                Rectangle logoRect = new Rectangle(
                    width / 2 - logoSize / 2, 
                    height / 2 - logoSize / 2, 
                    logoSize, logoSize);
                g.FillEllipse(brush, logoRect);
                
                // Vẽ text watermark
                using (Font font = new Font("Segoe UI", 24 * scale, FontStyle.Bold))
                {
                    string watermarkText = "THPT SÀI GÒN";
                    SizeF textSize = g.MeasureString(watermarkText, font);
                    g.DrawString(watermarkText, font, brush,
                        (width - textSize.Width) / 2, height / 2 + (int)(40 * scale));
                }
            }
        }
        
        /// <summary>
        /// Convert hình ảnh thẻ học sinh sang base64 với chất lượng nén
        /// </summary>
        private string ConvertCardImageToBase64(Bitmap image, int quality = 75)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Lưu với chất lượng JPEG để giảm kích thước
                    System.Drawing.Imaging.ImageCodecInfo jpegCodec = 
                        System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
                        .FirstOrDefault(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                    
                    if (jpegCodec != null)
                    {
                        System.Drawing.Imaging.EncoderParameters encoderParams = 
                            new System.Drawing.Imaging.EncoderParameters(1);
                        encoderParams.Param[0] = 
                            new System.Drawing.Imaging.EncoderParameter(
                                System.Drawing.Imaging.Encoder.Quality, (long)quality);
                        
                        image.Save(ms, jpegCodec, encoderParams);
                        encoderParams.Dispose();
                    }
                    else
                    {
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi convert ảnh thẻ sang base64: {ex.Message}");
                return "";
            }
        }

        private string GetStudentImageBase64()
        {
            try
            {
                Image studentImage = null;
                
                // Lấy ảnh từ picAvatar nếu có
                if (picAvatar?.Image != null)
                {
                    studentImage = picAvatar.Image;
                }
                // Hoặc load từ file
                else if (hocSinh != null && !string.IsNullOrEmpty(hocSinh.AnhDaiDien) && System.IO.File.Exists(hocSinh.AnhDaiDien))
                {
                    studentImage = Image.FromFile(hocSinh.AnhDaiDien);
                }
                
                // Nếu có ảnh, resize và convert sang base64
                if (studentImage != null)
                {
                    // Resize ảnh để giảm kích thước (140x180 hoặc nhỏ hơn)
                    using (Image resized = ResizeImageForQR(studentImage, 140, 180))
                    {
                        return ImageToBase64(resized);
                    }
                }
                
                // Tạo placeholder và convert sang base64
                return CreatePlaceholderImageBase64();
            }
            catch
            {
                return CreatePlaceholderImageBase64();
            }
        }

        private string GetStudentImageBase64Small()
        {
            try
            {
                Image studentImage = null;
                
                if (picAvatar?.Image != null)
                {
                    studentImage = picAvatar.Image;
                }
                else if (hocSinh != null && !string.IsNullOrEmpty(hocSinh.AnhDaiDien) && System.IO.File.Exists(hocSinh.AnhDaiDien))
                {
                    studentImage = Image.FromFile(hocSinh.AnhDaiDien);
                }
                
                if (studentImage != null)
                {
                    // Resize nhỏ hơn (100x130) để giảm kích thước base64
                    using (Image resized = ResizeImageForQR(studentImage, 100, 130))
                    {
                        return ImageToBase64(resized);
                    }
                }
                
                return CreatePlaceholderImageBase64Small();
            }
            catch
            {
                return CreatePlaceholderImageBase64Small();
            }
        }

        private Image ResizeImageForQR(Image original, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(original, 0, 0, width, height);
            }
            return result;
        }

        private string ImageToBase64(Image image)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Lưu với chất lượng JPEG để giảm kích thước
                    System.Drawing.Imaging.ImageCodecInfo jpegCodec = 
                        System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
                        .FirstOrDefault(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                    
                    if (jpegCodec != null)
                    {
                        // Sử dụng JPEG encoder với chất lượng 75%
                        System.Drawing.Imaging.EncoderParameters encoderParams = 
                            new System.Drawing.Imaging.EncoderParameters(1);
                        encoderParams.Param[0] = 
                            new System.Drawing.Imaging.EncoderParameter(
                                System.Drawing.Imaging.Encoder.Quality, 75L);
                        
                        image.Save(ms, jpegCodec, encoderParams);
                        encoderParams.Dispose();
                    }
                    else
                    {
                        // Fallback: Lưu dạng JPEG thông thường nếu không tìm thấy codec
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi convert ảnh sang base64: {ex.Message}");
                // Trả về empty string nếu lỗi
                return "";
            }
        }

        private string CreatePlaceholderImageBase64()
        {
            Bitmap placeholder = new Bitmap(140, 180);
            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                Rectangle rect = new Rectangle(0, 0, 140, 180);
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(191, 219, 254),
                    Color.FromArgb(147, 197, 253),
                    LinearGradientMode.Vertical))
                {
                    GraphicsPath path = CreateRoundedRectanglePath(rect, 5);
                    g.FillPath(brush, path);
                }
                
                using (Pen pen = new Pen(Color.FromArgb(252, 211, 77), 2))
                {
                    DrawRoundedRectangle(g, pen, 1, 1, 138, 178, 5);
                }
                
                using (Font font = new Font("Segoe UI", 9, FontStyle.Bold))
                {
                    SizeF textSize = g.MeasureString("Đang tải...", font);
                    g.DrawString("Đang tải...", font, 
                        new SolidBrush(Color.FromArgb(30, 64, 175)),
                        (140 - textSize.Width) / 2, (180 - textSize.Height) / 2);
                }
            }
            
            using (placeholder)
            {
                return ImageToBase64(placeholder);
            }
        }

        private string CreatePlaceholderImageBase64Small()
        {
            Bitmap placeholder = new Bitmap(100, 130);
            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(191, 219, 254));
                using (Font font = new Font("Segoe UI", 8))
                {
                    g.DrawString("HS", font, 
                        new SolidBrush(Color.FromArgb(30, 64, 175)), 35, 50);
                }
            }
            using (placeholder)
            {
                return ImageToBase64(placeholder);
            }
        }

        private string CreateSimpleHTML(string jsonData, string imageBase64)
        {
            // Nếu không có ảnh, tạo layout không có ảnh
            string photoSection = "";
            if (!string.IsNullOrEmpty(imageBase64))
            {
                photoSection = $"<img src=\"data:image/jpeg;base64,{imageBase64}\" class=\"photo\" alt=\"Ảnh\">";
            }
            else
            {
                photoSection = "<div class=\"photo\" style=\"background:#BFDBFE;display:flex;align-items:center;justify-content:center;color:#1E40AF;font-weight:bold\">Không có ảnh</div>";
            }
            
            return $@"<!DOCTYPE html>
<html>
<head>
<meta charset=""UTF-8"">
<meta name=""viewport"" content=""width=device-width,initial-scale=1"">
<title>Thẻ HS</title>
<style>
*{{margin:0;padding:0;box-sizing:border-box}}
body{{font-family:Arial;background:#E0F2FE;display:flex;justify-content:center;align-items:center;min-height:100vh;padding:10px}}
.c{{background:#E0F2FE;border:2px solid #1E40AF;border-radius:8px;max-width:500px;width:100%}}
.h{{background:#1E40AF;padding:15px;color:#fff;text-align:center;border-bottom:2px solid #FCD34D}}
.h h1{{color:#FCD34D;font-size:18px;margin:0}}
.ct{{padding:15px;display:flex;gap:15px;flex-wrap:wrap}}
.photo{{width:100px;height:130px;border:2px solid #FCD34D;border-radius:4px;object-fit:cover}}
.info{{flex:1;min-width:200px}}
.it{{margin-bottom:8px;font-size:14px}}
.l{{font-weight:bold;color:#1E40AF;display:inline-block;min-width:90px}}
.v{{color:#000}}
.v.cl{{color:#D97706;font-weight:bold}}
.f{{background:#1E40AF;padding:8px;text-align:center;color:#FCD34D;font-weight:bold;font-size:12px}}
</style>
<script>
var d={jsonData};
function f(d){{return d&&d.length>=8?d.substring(6,8)+'/'+d.substring(4,6)+'/'+d.substring(0,4):'';}}
var html='<div class=""c""><div class=""h""><h1>TRƯỜNG THPT SÀI GÒN</h1></div><div class=""ct"">{photoSection}<div class=""info""><div class=""it""><span class=""l"">Mã HS:</span><span class=""v"">'+d.id+'</span></div><div class=""it""><span class=""l"">Họ tên:</span><span class=""v"">'+(d.n||'').toUpperCase()+'</span></div><div class=""it""><span class=""l"">Ngày sinh:</span><span class=""v"">'+f(d.d)+'</span></div><div class=""it""><span class=""l"">Giới tính:</span><span class=""v"">'+d.g+'</span></div><div class=""it""><span class=""l"">Lớp:</span><span class=""v cl"">'+d.l+'</span></div><div class=""it""><span class=""l"">GVCN:</span><span class=""v"">'+d.gv+'</span></div><div class=""it""><span class=""l"">Hết hạn:</span><span class=""v"">'+f(d.e)+'</span></div><div class=""it""><span class=""l"">Trạng thái:</span><span class=""v"">'+d.s+'</span></div></div></div><div class=""f"">Mã học sinh: '+d.id+'</div></div>';
document.body.innerHTML=html;
</script>
</head>
<body></body>
</html>";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Vẽ watermark ở background (logo và chữ mờ)
            DrawWatermark(e.Graphics);

            // Vẽ chữ "THẺ HỌC SINH" dọc bên phải
            DrawVerticalText(e.Graphics);

            // Vẽ viền bo góc cho card với màu xanh và vàng
            using (Pen pen = new Pen(Color.FromArgb(30, 64, 175), 2)) // Xanh dương đậm
            {
                GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(0, 0, Width - 1, Height - 1), 10);
                e.Graphics.DrawPath(pen, path);
            }
            
            // Vẽ viền vàng bên trong để tạo hiệu ứng nổi bật
            using (Pen pen = new Pen(Color.FromArgb(252, 211, 77), 1)) // Vàng vàng
            {
                GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(2, 2, Width - 5, Height - 5), 8);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void DrawWatermark(Graphics g)
        {
            // Vẽ watermark logo và text mờ với màu xanh nhạt
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(40, 147, 197, 253))) // Xanh nhạt mờ
            {
                // Vẽ logo watermark
                Rectangle logoRect = new Rectangle(Width / 2 - 30, Height / 2 - 30, 60, 60);
                g.FillEllipse(brush, logoRect);
                
                // Vẽ viền vàng mờ cho logo watermark
                using (Pen pen = new Pen(Color.FromArgb(30, 252, 211, 77), 2))
                {
                    g.DrawEllipse(pen, logoRect);
                }
                
                // Vẽ text watermark
                using (Font font = new Font("Segoe UI", 24, FontStyle.Bold))
                {
                    string watermarkText = "THPT SÀI GÒN";
                    SizeF textSize = g.MeasureString(watermarkText, font);
                    g.DrawString(watermarkText, font, brush,
                        (Width - textSize.Width) / 2, Height / 2 + 40);
                }
            }
        }

        private void DrawVerticalText(Graphics g)
        {
            // Vẽ chữ "THẺ HỌC SINH" dọc bên phải với màu vàng nổi bật
            using (Font font = new Font("Segoe UI", 12, FontStyle.Bold))
            {
                string verticalText = "THẺ HỌC SINH";
                SizeF textSize = g.MeasureString(verticalText, font);
                
                // Xoay text 90 độ
                g.TranslateTransform(Width - 20, Height / 2);
                g.RotateTransform(-90);
                
                // Vẽ text với màu vàng nổi bật
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(217, 119, 6))) // Cam vàng đậm
                {
                    g.DrawString(verticalText, font, brush, 
                        -textSize.Width / 2, -textSize.Height / 2);
                }
                
                g.ResetTransform();
            }
        }

        private void StudentCard_Load(object sender, EventArgs e)
        {

        }

        private void lblNgayHetHan_Click(object sender, EventArgs e)
        {

        }

        private void lblNgaySinh_Click(object sender, EventArgs e)
        {

        }

        private void picQR_Click(object sender, EventArgs e)
        {

        }
    }
}
