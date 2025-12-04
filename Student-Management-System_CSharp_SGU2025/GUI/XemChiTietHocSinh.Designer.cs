namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class XemChiTietHocSinh
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMaLop;
        private System.Windows.Forms.Label lblTenLop;
        private System.Windows.Forms.Label lblKhoi;
        private System.Windows.Forms.Label lblSiSo;
        private System.Windows.Forms.Label lblGVCN;
        private System.Windows.Forms.Label lblSDTGV;
        private System.Windows.Forms.Label lblEmailGV;
        private System.Windows.Forms.Label lblLopHienTai;
        private System.Windows.Forms.Label lblGVCNLop;
        private System.Windows.Forms.Label lblSDTGVCN;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPhuHuynh;
        private Guna.UI2.WinForms.Guna2Button btnDong;
        private System.Windows.Forms.Panel panelThongTin;
        private System.Windows.Forms.Panel panelLop;
        private System.Windows.Forms.Panel panelPhuHuynh;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.PictureBox picAnhHocSinh;
        private System.Windows.Forms.Panel panelTheHocSinh;
        private System.Windows.Forms.PictureBox picAnhTheHocSinh;
        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.Label lblTheTenTruong;
        private System.Windows.Forms.Label lblTheDiaChi;
        private System.Windows.Forms.Label lblTheMaHS;
        private System.Windows.Forms.Label lblTheHoTen;
        private System.Windows.Forms.Label lblTheNgaySinh;
        private System.Windows.Forms.Label lblTheLop;
        private System.Windows.Forms.Label lblTheNgayHetHan;
        private System.Windows.Forms.Label lblTheTitle;
        private System.Windows.Forms.Label lblTheMaSo;
        private System.Windows.Forms.Label lblTheQuanLy;
        private System.Windows.Forms.Label lblTheTrangThai;
        private System.Windows.Forms.Label lblTheGioiTinh;
        private System.Windows.Forms.Panel panelTheBanner;
        private Guna.UI2.WinForms.Guna2Button btnUploadAnh;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.panelThongTin = new System.Windows.Forms.Panel();
            this.lblMaLop = new System.Windows.Forms.Label();
            this.lblTenLop = new System.Windows.Forms.Label();
            this.lblKhoi = new System.Windows.Forms.Label();
            this.lblSiSo = new System.Windows.Forms.Label();
            this.lblGVCN = new System.Windows.Forms.Label();
            this.lblSDTGV = new System.Windows.Forms.Label();
            this.lblEmailGV = new System.Windows.Forms.Label();
            this.panelLop = new System.Windows.Forms.Panel();
            this.lblLopHienTai = new System.Windows.Forms.Label();
            this.lblGVCNLop = new System.Windows.Forms.Label();
            this.lblSDTGVCN = new System.Windows.Forms.Label();
            this.panelPhuHuynh = new System.Windows.Forms.Panel();
            this.dgvPhuHuynh = new Guna.UI2.WinForms.Guna2DataGridView();
            this.picAnhHocSinh = new System.Windows.Forms.PictureBox();
            this.panelTheHocSinh = new System.Windows.Forms.Panel();
            this.picAnhTheHocSinh = new System.Windows.Forms.PictureBox();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.lblTheTenTruong = new System.Windows.Forms.Label();
            this.lblTheDiaChi = new System.Windows.Forms.Label();
            this.lblTheMaHS = new System.Windows.Forms.Label();
            this.lblTheHoTen = new System.Windows.Forms.Label();
            this.lblTheNgaySinh = new System.Windows.Forms.Label();
            this.lblTheLop = new System.Windows.Forms.Label();
            this.lblTheNgayHetHan = new System.Windows.Forms.Label();
            this.lblTheTitle = new System.Windows.Forms.Label();
            this.lblTheMaSo = new System.Windows.Forms.Label();
            this.lblTheQuanLy = new System.Windows.Forms.Label();
            this.lblTheTrangThai = new System.Windows.Forms.Label();
            this.lblTheGioiTinh = new System.Windows.Forms.Label();
            this.panelTheBanner = new System.Windows.Forms.Panel();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.btnUploadAnh = new Guna.UI2.WinForms.Guna2Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelThongTin.SuspendLayout();
            this.panelLop.SuspendLayout();
            this.panelPhuHuynh.SuspendLayout();
            this.panelTheHocSinh.SuspendLayout();
            this.panelTheBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuHuynh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhHocSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhTheHocSinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            this.SuspendLayout();

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 950);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết hồ sơ học sinh";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.AutoScroll = true;

            // Tiêu đề
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(20, 20);
            this.lblTieuDe.Size = new System.Drawing.Size(300, 41);
            this.lblTieuDe.Text = "Chi tiết hồ sơ học sinh";
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(30, 136, 229);

            // Panel Thông tin cá nhân
            this.panelThongTin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelThongTin.Location = new System.Drawing.Point(20, 70);
            this.panelThongTin.Size = new System.Drawing.Size(760, 180);
            this.panelThongTin.BackColor = System.Drawing.Color.White;

            this.lblMaLop.AutoSize = true;
            this.lblMaLop.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMaLop.Location = new System.Drawing.Point(20, 20);
            this.lblMaLop.Size = new System.Drawing.Size(200, 25);
            this.panelThongTin.Controls.Add(this.lblMaLop);

            this.lblTenLop.AutoSize = true;
            this.lblTenLop.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTenLop.Location = new System.Drawing.Point(20, 50);
            this.lblTenLop.Size = new System.Drawing.Size(400, 25);
            this.panelThongTin.Controls.Add(this.lblTenLop);

            this.lblKhoi.AutoSize = true;
            this.lblKhoi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblKhoi.Location = new System.Drawing.Point(20, 80);
            this.lblKhoi.Size = new System.Drawing.Size(200, 25);
            this.panelThongTin.Controls.Add(this.lblKhoi);

            this.lblSiSo.AutoSize = true;
            this.lblSiSo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSiSo.Location = new System.Drawing.Point(400, 80);
            this.lblSiSo.Size = new System.Drawing.Size(200, 25);
            this.panelThongTin.Controls.Add(this.lblSiSo);

            this.lblGVCN.AutoSize = true;
            this.lblGVCN.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblGVCN.Location = new System.Drawing.Point(20, 110);
            this.lblGVCN.Size = new System.Drawing.Size(300, 25);
            this.panelThongTin.Controls.Add(this.lblGVCN);

            this.lblSDTGV.AutoSize = true;
            this.lblSDTGV.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSDTGV.Location = new System.Drawing.Point(400, 110);
            this.lblSDTGV.Size = new System.Drawing.Size(300, 25);
            this.panelThongTin.Controls.Add(this.lblSDTGV);

            this.lblEmailGV.AutoSize = true;
            this.lblEmailGV.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEmailGV.Location = new System.Drawing.Point(20, 140);
            this.lblEmailGV.Size = new System.Drawing.Size(300, 25);
            this.panelThongTin.Controls.Add(this.lblEmailGV);

            // PictureBox Ảnh học sinh
            this.picAnhHocSinh.BackColor = System.Drawing.Color.White;
            this.picAnhHocSinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnhHocSinh.Location = new System.Drawing.Point(600, 20);
            this.picAnhHocSinh.Size = new System.Drawing.Size(140, 140);
            this.picAnhHocSinh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnhHocSinh.TabIndex = 0;
            this.picAnhHocSinh.TabStop = false;
            this.panelThongTin.Controls.Add(this.picAnhHocSinh);

            // Button Upload Ảnh
            this.btnUploadAnh.BorderRadius = 6;
            this.btnUploadAnh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnUploadAnh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnUploadAnh.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnUploadAnh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnUploadAnh.FillColor = System.Drawing.Color.FromArgb(30, 136, 229);
            this.btnUploadAnh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUploadAnh.ForeColor = System.Drawing.Color.White;
            this.btnUploadAnh.Location = new System.Drawing.Point(600, 165);
            this.btnUploadAnh.Name = "btnUploadAnh";
            this.btnUploadAnh.Size = new System.Drawing.Size(140, 30);
            this.btnUploadAnh.TabIndex = 1;
            this.btnUploadAnh.Text = "📷 Đổi ảnh";
            this.btnUploadAnh.Click += new System.EventHandler(this.btnUploadAnh_Click);
            this.panelThongTin.Controls.Add(this.btnUploadAnh);

            // OpenFileDialog
            this.openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            this.openFileDialog.Title = "Chọn ảnh học sinh";

            // Panel Lớp
            this.panelLop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLop.Location = new System.Drawing.Point(20, 270);
            this.panelLop.Size = new System.Drawing.Size(760, 100);
            this.panelLop.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);

            this.lblLopHienTai.AutoSize = true;
            this.lblLopHienTai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLopHienTai.Location = new System.Drawing.Point(20, 20);
            this.lblLopHienTai.Size = new System.Drawing.Size(400, 28);
            this.panelLop.Controls.Add(this.lblLopHienTai);

            this.lblGVCNLop.AutoSize = true;
            this.lblGVCNLop.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblGVCNLop.Location = new System.Drawing.Point(20, 55);
            this.lblGVCNLop.Size = new System.Drawing.Size(300, 25);
            this.panelLop.Controls.Add(this.lblGVCNLop);

            this.lblSDTGVCN.AutoSize = true;
            this.lblSDTGVCN.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSDTGVCN.Location = new System.Drawing.Point(400, 55);
            this.lblSDTGVCN.Size = new System.Drawing.Size(300, 25);
            this.panelLop.Controls.Add(this.lblSDTGVCN);

            // Panel Phụ huynh
            this.panelPhuHuynh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPhuHuynh.Location = new System.Drawing.Point(20, 390);
            this.panelPhuHuynh.Size = new System.Drawing.Size(760, 150);
            this.panelPhuHuynh.BackColor = System.Drawing.Color.White;

            // DataGridView Phụ huynh
            this.dgvPhuHuynh.AllowUserToAddRows = false;
            this.dgvPhuHuynh.AllowUserToDeleteRows = false;
            this.dgvPhuHuynh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhuHuynh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhuHuynh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhuHuynh.ReadOnly = true;
            this.dgvPhuHuynh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhuHuynh.MultiSelect = false;
            this.dgvPhuHuynh.RowHeadersVisible = false;
            this.dgvPhuHuynh.Columns.Add("HoTen", "Họ và tên");
            this.dgvPhuHuynh.Columns.Add("SoDienThoai", "SĐT");
            this.dgvPhuHuynh.Columns.Add("Email", "Email");
            this.dgvPhuHuynh.Columns.Add("MoiQuanHe", "Mối quan hệ");
            this.panelPhuHuynh.Controls.Add(this.dgvPhuHuynh);

            // Panel Thẻ Học Sinh (panel riêng, sau panel phụ huynh)
            this.panelTheHocSinh.BackColor = System.Drawing.Color.White;
            this.panelTheHocSinh.Location = new System.Drawing.Point(20, 550);
            this.panelTheHocSinh.Size = new System.Drawing.Size(760, 320);
            this.panelTheHocSinh.Visible = true;
            this.panelTheHocSinh.BringToFront();
            this.panelTheHocSinh.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTheHocSinh_Paint);

            // Panel Banner xanh (ở trên cùng)
            this.panelTheBanner.BackColor = System.Drawing.Color.FromArgb(30, 136, 229);
            this.panelTheBanner.Location = new System.Drawing.Point(0, 0);
            this.panelTheBanner.Size = new System.Drawing.Size(760, 80);
            this.panelTheBanner.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTheBanner_Paint);
            this.panelTheHocSinh.Controls.Add(this.panelTheBanner);

            // Label Tên trường (trong banner xanh)
            this.lblTheTenTruong.AutoSize = false;
            this.lblTheTenTruong.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTheTenTruong.Location = new System.Drawing.Point(20, 15);
            this.lblTheTenTruong.Size = new System.Drawing.Size(500, 30);
            this.lblTheTenTruong.Text = "TRƯỜNG THPT SÀI GÒN";
            this.lblTheTenTruong.ForeColor = System.Drawing.Color.White;
            this.panelTheBanner.Controls.Add(this.lblTheTenTruong);

            // Label "Quản lý học sinh" (trong banner xanh)
            this.lblTheQuanLy.AutoSize = false;
            this.lblTheQuanLy.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTheQuanLy.Location = new System.Drawing.Point(20, 45);
            this.lblTheQuanLy.Size = new System.Drawing.Size(500, 25);
            this.lblTheQuanLy.Text = "Quản lý học sinh";
            this.lblTheQuanLy.ForeColor = System.Drawing.Color.White;
            this.panelTheBanner.Controls.Add(this.lblTheQuanLy);

            // PictureBox QR Code (góc trên phải trong banner)
            this.picQRCode.BackColor = System.Drawing.Color.White;
            this.picQRCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.picQRCode.Location = new System.Drawing.Point(630, 10);
            this.picQRCode.Size = new System.Drawing.Size(110, 110);
            this.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQRCode.TabIndex = 0;
            this.picQRCode.TabStop = false;
            this.picQRCode.Paint += new System.Windows.Forms.PaintEventHandler(this.picQRCode_Paint);
            this.panelTheBanner.Controls.Add(this.picQRCode);

            // PictureBox Ảnh học sinh (bên trái, dưới banner)
            this.picAnhTheHocSinh.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.picAnhTheHocSinh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.picAnhTheHocSinh.Location = new System.Drawing.Point(20, 100);
            this.picAnhTheHocSinh.Size = new System.Drawing.Size(180, 200);
            this.picAnhTheHocSinh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnhTheHocSinh.TabIndex = 0;
            this.picAnhTheHocSinh.TabStop = false;
            this.picAnhTheHocSinh.Paint += new System.Windows.Forms.PaintEventHandler(this.picAnhTheHocSinh_Paint);
            this.panelTheHocSinh.Controls.Add(this.picAnhTheHocSinh);

            // Label "Mã HS" (trên ảnh, góc trên trái của ảnh)
            this.lblTheMaHS.AutoSize = false;
            this.lblTheMaHS.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTheMaHS.Location = new System.Drawing.Point(30, 110);
            this.lblTheMaHS.Size = new System.Drawing.Size(60, 20);
            this.lblTheMaHS.Text = "Mã HS:";
            this.lblTheMaHS.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.panelTheHocSinh.Controls.Add(this.lblTheMaHS);

            // Label Mã số (bên cạnh "Mã HS")
            this.lblTheMaSo.AutoSize = false;
            this.lblTheMaSo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTheMaSo.Location = new System.Drawing.Point(90, 110);
            this.lblTheMaSo.Size = new System.Drawing.Size(50, 20);
            this.lblTheMaSo.Text = "";
            this.lblTheMaSo.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.panelTheHocSinh.Controls.Add(this.lblTheMaSo);

            // Label Họ tên (lớn, đậm, bên phải ảnh)
            this.lblTheHoTen.AutoSize = false;
            this.lblTheHoTen.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTheHoTen.Location = new System.Drawing.Point(220, 100);
            this.lblTheHoTen.Size = new System.Drawing.Size(500, 40);
            this.lblTheHoTen.Text = "";
            this.lblTheHoTen.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.panelTheHocSinh.Controls.Add(this.lblTheHoTen);

            // Label Ngày sinh (có icon calendar)
            this.lblTheNgaySinh.AutoSize = false;
            this.lblTheNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTheNgaySinh.Location = new System.Drawing.Point(220, 150);
            this.lblTheNgaySinh.Size = new System.Drawing.Size(250, 25);
            this.lblTheNgaySinh.Text = "";
            this.lblTheNgaySinh.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.panelTheHocSinh.Controls.Add(this.lblTheNgaySinh);

            // Label Giới tính (có icon person)
            this.lblTheGioiTinh.AutoSize = false;
            this.lblTheGioiTinh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTheGioiTinh.Location = new System.Drawing.Point(480, 150);
            this.lblTheGioiTinh.Size = new System.Drawing.Size(150, 25);
            this.lblTheGioiTinh.Text = "";
            this.lblTheGioiTinh.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.panelTheHocSinh.Controls.Add(this.lblTheGioiTinh);

            // Label Trạng thái (màu xanh, góc trên phải)
            this.lblTheTrangThai.AutoSize = false;
            this.lblTheTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTheTrangThai.Location = new System.Drawing.Point(600, 100);
            this.lblTheTrangThai.Size = new System.Drawing.Size(120, 30);
            this.lblTheTrangThai.Text = "Đang học";
            this.lblTheTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTheTrangThai.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblTheTrangThai.ForeColor = System.Drawing.Color.White;
            this.panelTheHocSinh.Controls.Add(this.lblTheTrangThai);

            // Nút Đóng
            this.btnDong.BorderRadius = 8;
            this.btnDong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            this.btnDong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(650, 870);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(130, 40);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            // Controls
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.panelThongTin);
            this.Controls.Add(this.panelLop);
            this.Controls.Add(this.panelPhuHuynh);
            this.Controls.Add(this.panelTheHocSinh);
            this.Controls.Add(this.btnDong);

            this.panelThongTin.ResumeLayout(false);
            this.panelThongTin.PerformLayout();
            this.panelLop.ResumeLayout(false);
            this.panelLop.PerformLayout();
            this.panelPhuHuynh.ResumeLayout(false);
            this.panelTheHocSinh.ResumeLayout(false);
            this.panelTheBanner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuHuynh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhHocSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhTheHocSinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

