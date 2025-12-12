namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FrmXemChiTietPhanCongGiangDay
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2Panel panelContent;
        private Guna.UI2.WinForms.Guna2Panel panelGiaoVien;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGiaoVien;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGiaoVienChiTiet;
        private Guna.UI2.WinForms.Guna2Panel panelMonHoc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMonHoc;
        private Guna.UI2.WinForms.Guna2Panel panelLop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLopChiTiet;
        private Guna.UI2.WinForms.Guna2Panel panelHocKy;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHocKy;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHocKyChiTiet;
        private Guna.UI2.WinForms.Guna2Panel panelThoiGian;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNgayBatDau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNgayKetThuc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThoiGian;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMaPhanCong;
        private Guna.UI2.WinForms.Guna2Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.panelContent = new Guna.UI2.WinForms.Guna2Panel();
            this.panelThoiGian = new Guna.UI2.WinForms.Guna2Panel();
            this.lblThoiGian = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblNgayKetThuc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblNgayBatDau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelHocKy = new Guna.UI2.WinForms.Guna2Panel();
            this.lblHocKyChiTiet = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblHocKy = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelLop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLopChiTiet = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblLop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelMonHoc = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMonHoc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelGiaoVien = new Guna.UI2.WinForms.Guna2Panel();
            this.lblGiaoVienChiTiet = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblGiaoVien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMaPhanCong = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelMain.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelThoiGian.SuspendLayout();
            this.panelHocKy.SuspendLayout();
            this.panelLop.SuspendLayout();
            this.panelMonHoc.SuspendLayout();
            this.panelGiaoVien.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelMain.BorderRadius = 15;
            this.panelMain.BorderThickness = 2;
            this.panelMain.Controls.Add(this.btnDong);
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.FillColor = System.Drawing.Color.White;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(800, 650);
            this.panelMain.TabIndex = 0;
            // 
            // btnDong
            // 
            this.btnDong.BorderRadius = 10;
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(650, 595);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(130, 40);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Controls.Add(this.panelThoiGian);
            this.panelContent.Controls.Add(this.panelHocKy);
            this.panelContent.Controls.Add(this.panelLop);
            this.panelContent.Controls.Add(this.panelMonHoc);
            this.panelContent.Controls.Add(this.panelGiaoVien);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 100);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20);
            this.panelContent.Size = new System.Drawing.Size(800, 550);
            this.panelContent.TabIndex = 1;
            this.panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContent_Paint);
            // 
            // panelThoiGian
            // 
            this.panelThoiGian.BackColor = System.Drawing.Color.White;
            this.panelThoiGian.BorderRadius = 10;
            this.panelThoiGian.Controls.Add(this.lblThoiGian);
            this.panelThoiGian.Controls.Add(this.lblNgayKetThuc);
            this.panelThoiGian.Controls.Add(this.lblNgayBatDau);
            this.panelThoiGian.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelThoiGian.Location = new System.Drawing.Point(20, 360);
            this.panelThoiGian.Name = "panelThoiGian";
            this.panelThoiGian.Padding = new System.Windows.Forms.Padding(20);
            this.panelThoiGian.Size = new System.Drawing.Size(760, 120);
            this.panelThoiGian.TabIndex = 4;
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.BackColor = System.Drawing.Color.Transparent;
            this.lblThoiGian.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblThoiGian.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblThoiGian.Location = new System.Drawing.Point(20, 85);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(84, 25);
            this.lblThoiGian.TabIndex = 2;
            this.lblThoiGian.Text = "Thời gian:";
            // 
            // lblNgayKetThuc
            // 
            this.lblNgayKetThuc.BackColor = System.Drawing.Color.Transparent;
            this.lblNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayKetThuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNgayKetThuc.Location = new System.Drawing.Point(20, 50);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(97, 22);
            this.lblNgayKetThuc.TabIndex = 1;
            this.lblNgayKetThuc.Text = "Ngày kết thúc:";
            // 
            // lblNgayBatDau
            // 
            this.lblNgayBatDau.BackColor = System.Drawing.Color.Transparent;
            this.lblNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNgayBatDau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNgayBatDau.Location = new System.Drawing.Point(20, 20);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(96, 22);
            this.lblNgayBatDau.TabIndex = 0;
            this.lblNgayBatDau.Text = "Ngày bắt đầu:";
            // 
            // panelHocKy
            // 
            this.panelHocKy.BackColor = System.Drawing.Color.White;
            this.panelHocKy.BorderRadius = 10;
            this.panelHocKy.Controls.Add(this.lblHocKyChiTiet);
            this.panelHocKy.Controls.Add(this.lblHocKy);
            this.panelHocKy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelHocKy.Location = new System.Drawing.Point(20, 240);
            this.panelHocKy.Name = "panelHocKy";
            this.panelHocKy.Padding = new System.Windows.Forms.Padding(20);
            this.panelHocKy.Size = new System.Drawing.Size(760, 100);
            this.panelHocKy.TabIndex = 3;
            // 
            // lblHocKyChiTiet
            // 
            this.lblHocKyChiTiet.BackColor = System.Drawing.Color.Transparent;
            this.lblHocKyChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHocKyChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHocKyChiTiet.Location = new System.Drawing.Point(20, 55);
            this.lblHocKyChiTiet.Name = "lblHocKyChiTiet";
            this.lblHocKyChiTiet.Size = new System.Drawing.Size(84, 19);
            this.lblHocKyChiTiet.TabIndex = 1;
            this.lblHocKyChiTiet.Text = "Chi tiết học kỳ";
            // 
            // lblHocKy
            // 
            this.lblHocKy.BackColor = System.Drawing.Color.Transparent;
            this.lblHocKy.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblHocKy.Location = new System.Drawing.Point(20, 15);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new System.Drawing.Size(70, 27);
            this.lblHocKy.TabIndex = 0;
            this.lblHocKy.Text = "Học kỳ:";
            // 
            // panelLop
            // 
            this.panelLop.BackColor = System.Drawing.Color.White;
            this.panelLop.BorderRadius = 10;
            this.panelLop.Controls.Add(this.lblLopChiTiet);
            this.panelLop.Controls.Add(this.lblLop);
            this.panelLop.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelLop.Location = new System.Drawing.Point(400, 140);
            this.panelLop.Name = "panelLop";
            this.panelLop.Padding = new System.Windows.Forms.Padding(20);
            this.panelLop.Size = new System.Drawing.Size(380, 80);
            this.panelLop.TabIndex = 2;
            // 
            // lblLopChiTiet
            // 
            this.lblLopChiTiet.BackColor = System.Drawing.Color.Transparent;
            this.lblLopChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLopChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLopChiTiet.Location = new System.Drawing.Point(20, 50);
            this.lblLopChiTiet.Name = "lblLopChiTiet";
            this.lblLopChiTiet.Size = new System.Drawing.Size(66, 19);
            this.lblLopChiTiet.TabIndex = 1;
            this.lblLopChiTiet.Text = "Chi tiết lớp";
            // 
            // lblLop
            // 
            this.lblLop.BackColor = System.Drawing.Color.Transparent;
            this.lblLop.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblLop.Location = new System.Drawing.Point(20, 15);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(42, 27);
            this.lblLop.TabIndex = 0;
            this.lblLop.Text = "Lớp:";
            // 
            // panelMonHoc
            // 
            this.panelMonHoc.BackColor = System.Drawing.Color.White;
            this.panelMonHoc.BorderRadius = 10;
            this.panelMonHoc.Controls.Add(this.lblMonHoc);
            this.panelMonHoc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelMonHoc.Location = new System.Drawing.Point(20, 140);
            this.panelMonHoc.Name = "panelMonHoc";
            this.panelMonHoc.Padding = new System.Windows.Forms.Padding(20);
            this.panelMonHoc.Size = new System.Drawing.Size(360, 80);
            this.panelMonHoc.TabIndex = 1;
            // 
            // lblMonHoc
            // 
            this.lblMonHoc.BackColor = System.Drawing.Color.Transparent;
            this.lblMonHoc.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMonHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblMonHoc.Location = new System.Drawing.Point(20, 25);
            this.lblMonHoc.Name = "lblMonHoc";
            this.lblMonHoc.Size = new System.Drawing.Size(87, 27);
            this.lblMonHoc.TabIndex = 0;
            this.lblMonHoc.Text = "Môn học:";
            // 
            // panelGiaoVien
            // 
            this.panelGiaoVien.BackColor = System.Drawing.Color.White;
            this.panelGiaoVien.BorderRadius = 10;
            this.panelGiaoVien.Controls.Add(this.lblGiaoVienChiTiet);
            this.panelGiaoVien.Controls.Add(this.lblGiaoVien);
            this.panelGiaoVien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelGiaoVien.Location = new System.Drawing.Point(20, 20);
            this.panelGiaoVien.Name = "panelGiaoVien";
            this.panelGiaoVien.Padding = new System.Windows.Forms.Padding(20);
            this.panelGiaoVien.Size = new System.Drawing.Size(760, 100);
            this.panelGiaoVien.TabIndex = 0;
            // 
            // lblGiaoVienChiTiet
            // 
            this.lblGiaoVienChiTiet.BackColor = System.Drawing.Color.Transparent;
            this.lblGiaoVienChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiaoVienChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGiaoVienChiTiet.Location = new System.Drawing.Point(20, 50);
            this.lblGiaoVienChiTiet.Name = "lblGiaoVienChiTiet";
            this.lblGiaoVienChiTiet.Size = new System.Drawing.Size(100, 19);
            this.lblGiaoVienChiTiet.TabIndex = 1;
            this.lblGiaoVienChiTiet.Text = "Chi tiết giáo viên";
            // 
            // lblGiaoVien
            // 
            this.lblGiaoVien.BackColor = System.Drawing.Color.Transparent;
            this.lblGiaoVien.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblGiaoVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblGiaoVien.Location = new System.Drawing.Point(20, 15);
            this.lblGiaoVien.Name = "lblGiaoVien";
            this.lblGiaoVien.Size = new System.Drawing.Size(91, 27);
            this.lblGiaoVien.TabIndex = 0;
            this.lblGiaoVien.Text = "Giáo viên:";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblMaPhanCong);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(800, 100);
            this.panelHeader.TabIndex = 0;
            // 
            // lblMaPhanCong
            // 
            this.lblMaPhanCong.BackColor = System.Drawing.Color.Transparent;
            this.lblMaPhanCong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaPhanCong.ForeColor = System.Drawing.Color.White;
            this.lblMaPhanCong.Location = new System.Drawing.Point(30, 60);
            this.lblMaPhanCong.Name = "lblMaPhanCong";
            this.lblMaPhanCong.Size = new System.Drawing.Size(91, 19);
            this.lblMaPhanCong.TabIndex = 1;
            this.lblMaPhanCong.Text = "Mã phân công:";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(394, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHI TIẾT PHÂN CÔNG GIẢNG DẠY";
            // 
            // FrmXemChiTietPhanCongGiangDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmXemChiTietPhanCongGiangDay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết phân công giảng dạy";
            this.Load += new System.EventHandler(this.FrmXemChiTietPhanCongGiangDay_Load);
            this.panelMain.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelThoiGian.ResumeLayout(false);
            this.panelThoiGian.PerformLayout();
            this.panelHocKy.ResumeLayout(false);
            this.panelHocKy.PerformLayout();
            this.panelLop.ResumeLayout(false);
            this.panelLop.PerformLayout();
            this.panelMonHoc.ResumeLayout(false);
            this.panelMonHoc.PerformLayout();
            this.panelGiaoVien.ResumeLayout(false);
            this.panelGiaoVien.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
