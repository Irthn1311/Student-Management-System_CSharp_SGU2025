using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.Utils;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            // Tạo shadow effect và bo góc
            this.BackColor = Color.White;
            this.Padding = new Padding(10);
            
            // Vẽ viền bo góc
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
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
            // Vẽ gradient cho header
            using (LinearGradientBrush brush = new LinearGradientBrush(
                panelHeader.ClientRectangle,
                Color.FromArgb(37, 99, 235),  // Xanh đậm
                Color.FromArgb(59, 130, 246), // Xanh nhạt
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, panelHeader.ClientRectangle);
            }
        }

        private void MakeCircularPictureBox(PictureBox picBox)
        {
            // Tạo avatar tròn với viền
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picBox.Width - 1, picBox.Height - 1);
            Region rg = new Region(gp);
            picBox.Region = rg;
        }

        private void DisplayStudentInfo()
        {
            if (hocSinh == null) return;

            // Load ảnh đại diện
            try
            {
                if (!string.IsNullOrEmpty(hocSinh.AnhDaiDien))
                {
                    if (System.IO.File.Exists(hocSinh.AnhDaiDien))
                    {
                        picAvatar.Image = Image.FromFile(hocSinh.AnhDaiDien);
                    }
                    else
                    {
                        // Tạo ảnh placeholder đẹp với gradient
                        Bitmap placeholder = new Bitmap(140, 180);
                        using (Graphics g = Graphics.FromImage(placeholder))
                        {
                            // Gradient background
                            using (LinearGradientBrush brush = new LinearGradientBrush(
                                new Rectangle(0, 0, 140, 180),
                                Color.FromArgb(96, 165, 250),
                                Color.FromArgb(37, 99, 235),
                                LinearGradientMode.Vertical))
                            {
                                g.FillRectangle(brush, 0, 0, 140, 180);
                            }
                            
                            // Vẽ chữ cái đầu
                            using (Font font = new Font("Segoe UI", 32, FontStyle.Bold))
                            {
                                string initials = GetInitials(hocSinh.HoTen);
                                SizeF textSize = g.MeasureString(initials, font);
                                g.DrawString(initials, font, Brushes.White,
                                    (140 - textSize.Width) / 2, (180 - textSize.Height) / 2);
                            }
                        }
                        picAvatar.Image = placeholder;
                    }
                }
                else
                {
                    // Tạo ảnh placeholder đẹp với gradient
                    Bitmap placeholder = new Bitmap(140, 180);
                    using (Graphics g = Graphics.FromImage(placeholder))
                    {
                        // Gradient background
                        using (LinearGradientBrush brush = new LinearGradientBrush(
                            new Rectangle(0, 0, 140, 180),
                            Color.FromArgb(96, 165, 250),
                            Color.FromArgb(37, 99, 235),
                            LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(brush, 0, 0, 140, 180);
                        }
                        
                        // Vẽ chữ cái đầu
                        using (Font font = new Font("Segoe UI", 32, FontStyle.Bold))
                        {
                            string initials = GetInitials(hocSinh.HoTen);
                            SizeF textSize = g.MeasureString(initials, font);
                            g.DrawString(initials, font, Brushes.White,
                                (140 - textSize.Width) / 2, (180 - textSize.Height) / 2);
                        }
                    }
                    picAvatar.Image = placeholder;
                }
            }
            catch
            {
                // Nếu lỗi, để ảnh mặc định
                Bitmap placeholder = new Bitmap(140, 180);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.FromArgb(229, 231, 235));
                }
                picAvatar.Image = placeholder;
            }

            // Hiển thị thông tin
            lblMaHS.Text = $"Mã HS: {hocSinh.MaHS}";
             lblHoTen.Text = hocSinh.HoTen.ToUpper();
            lblNgaySinh.Text = $"📅 {hocSinh.NgaySinh:dd/MM/yyyy}";
            lblGioiTinh.Text = $"👤 {hocSinh.GioiTinh}";
            lblSDT.Text = $"📞 {hocSinh.SdtHS ?? "N/A"}";
            lblEmail.Text = $"✉️ {hocSinh.Email ?? "N/A"}";
            lblIDNumber.Text = $"ID: HS-{hocSinh.MaHS:D6}";

            // Làm tròn avatar
            MakeCircularPictureBox(picAvatar);

            // Hiển thị lớp
            if (!string.IsNullOrEmpty(tenLop))
            {
                lblLop.Text = $"🏫 Lớp: {tenLop}";
                lblLop.Visible = true;
            }
            else
            {
                lblLop.Visible = false;
            }

            // Hiển thị GVCN
            if (!string.IsNullOrEmpty(tenGVCN))
            {
                lblGVCN.Text = $"👨‍🏫 GVCN: {tenGVCN}";
                lblGVCN.Visible = true;
            }
            else
            {
                lblGVCN.Visible = false;
            }

            // Trạng thái với màu sắc
            lblTrangThai.Text = hocSinh.TrangThai;
            if (hocSinh.TrangThai == "Đang học")
            {
                lblTrangThai.ForeColor = Color.FromArgb(22, 163, 74);
                lblTrangThai.BackColor = Color.FromArgb(220, 252, 231);
            }
            else if (hocSinh.TrangThai.Contains("Nghỉ"))
            {
                lblTrangThai.ForeColor = Color.FromArgb(220, 38, 38);
                lblTrangThai.BackColor = Color.FromArgb(254, 226, 226);
            }
            else
            {
                lblTrangThai.ForeColor = Color.FromArgb(107, 114, 128);
                lblTrangThai.BackColor = Color.FromArgb(243, 244, 246);
            }

            // Sinh mã QR (giả lập)
            GenerateQRCode();
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
                // Tạo nội dung QR code với thông tin học sinh
                string qrContent = $"HOCSINH|MaHS:{hocSinh.MaHS}|HoTen:{hocSinh.HoTen}|NgaySinh:{hocSinh.NgaySinh:dd/MM/yyyy}|GioiTinh:{hocSinh.GioiTinh}|SDT:{hocSinh.SdtHS}";
                
                // Sử dụng QRCoder để tạo QR code thật có thể quét được
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                
                // Tạo QR code với màu xanh dương để match theme
                Bitmap qrBitmap = qrCode.GetGraphic(
                    pixelsPerModule: 3,
                    darkColor: Color.FromArgb(30, 64, 175), // Xanh dương đậm
                    lightColor: Color.White,
                    drawQuietZones: true
                );
                
                picQR.Image = qrBitmap;
            }
            catch (Exception ex)
            {
                // Nếu lỗi, tạo placeholder
                Bitmap placeholder = new Bitmap(80, 80);
                using (Graphics g = Graphics.FromImage(placeholder))
                {
                    g.Clear(Color.White);
                    using (Font font = new Font("Segoe UI", 7))
                    {
                        g.DrawString("QR\nError", font, Brushes.Red, 20, 30);
                    }
                }
                picQR.Image = placeholder;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Vẽ shadow
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddRectangle(new Rectangle(0, 0, Width, Height));
                this.Region = new Region(path);
            }

            // Vẽ viền gradient
            using (Pen pen = new Pen(Color.FromArgb(200, 229, 231, 235), 1))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }
    }
}
