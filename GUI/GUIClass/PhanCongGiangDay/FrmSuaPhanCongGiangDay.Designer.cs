namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FrmSuaPhanCongGiangDay
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2Panel panelContent;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMonHoc;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHocKy;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGiaoVienHienTai;
        private Guna.UI2.WinForms.Guna2ComboBox cbGiaoVienThayThe;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThongBao;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLabelNgayBatDau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLabelNgayKetThuc;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayBatDau;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayKetThuc;
        private Guna.UI2.WinForms.Guna2Panel panelThongTinCoBan;
        private Guna.UI2.WinForms.Guna2Panel panelThayDoi;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSectionThongTinCoBan;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSectionThayDoi;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;

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
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.panelContent = new Guna.UI2.WinForms.Guna2Panel();
            this.panelThayDoi = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLabelNgayKetThuc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblLabelNgayBatDau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSectionThayDoi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtpNgayKetThuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpNgayBatDau = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblThongBao = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cbGiaoVienThayThe = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblGiaoVienHienTai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelThongTinCoBan = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSectionThongTinCoBan = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblHocKy = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMonHoc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblLop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelMain.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelThayDoi.SuspendLayout();
            this.panelThongTinCoBan.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelMain.BorderRadius = 15;
            this.panelMain.BorderThickness = 2;
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.FillColor = System.Drawing.Color.White;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(700, 693);
            this.panelMain.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 10;
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(490, 364);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(130, 40);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 10;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(354, 362);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(130, 40);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Controls.Add(this.panelThayDoi);
            this.panelContent.Controls.Add(this.panelThongTinCoBan);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 100);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(30);
            this.panelContent.Size = new System.Drawing.Size(700, 593);
            this.panelContent.TabIndex = 1;
            // 
            // panelThayDoi
            // 
            this.panelThayDoi.BackColor = System.Drawing.Color.White;
            this.panelThayDoi.BorderRadius = 10;
            this.panelThayDoi.Controls.Add(this.btnHuy);
            this.panelThayDoi.Controls.Add(this.lblLabelNgayKetThuc);
            this.panelThayDoi.Controls.Add(this.btnLuu);
            this.panelThayDoi.Controls.Add(this.lblLabelNgayBatDau);
            this.panelThayDoi.Controls.Add(this.lblSectionThayDoi);
            this.panelThayDoi.Controls.Add(this.dtpNgayKetThuc);
            this.panelThayDoi.Controls.Add(this.dtpNgayBatDau);
            this.panelThayDoi.Controls.Add(this.lblThongBao);
            this.panelThayDoi.Controls.Add(this.cbGiaoVienThayThe);
            this.panelThayDoi.Controls.Add(this.lblGiaoVienHienTai);
            this.panelThayDoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelThayDoi.Location = new System.Drawing.Point(30, 179);
            this.panelThayDoi.Name = "panelThayDoi";
            this.panelThayDoi.Padding = new System.Windows.Forms.Padding(20);
            this.panelThayDoi.Size = new System.Drawing.Size(640, 411);
            this.panelThayDoi.TabIndex = 1;
            // 
            // lblLabelNgayKetThuc
            // 
            this.lblLabelNgayKetThuc.BackColor = System.Drawing.Color.Transparent;
            this.lblLabelNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLabelNgayKetThuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblLabelNgayKetThuc.Location = new System.Drawing.Point(20, 290);
            this.lblLabelNgayKetThuc.Name = "lblLabelNgayKetThuc";
            this.lblLabelNgayKetThuc.Size = new System.Drawing.Size(93, 19);
            this.lblLabelNgayKetThuc.TabIndex = 8;
            this.lblLabelNgayKetThuc.Text = "Ngày kết thúc:";
            // 
            // lblLabelNgayBatDau
            // 
            this.lblLabelNgayBatDau.BackColor = System.Drawing.Color.Transparent;
            this.lblLabelNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLabelNgayBatDau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblLabelNgayBatDau.Location = new System.Drawing.Point(20, 220);
            this.lblLabelNgayBatDau.Name = "lblLabelNgayBatDau";
            this.lblLabelNgayBatDau.Size = new System.Drawing.Size(90, 19);
            this.lblLabelNgayBatDau.TabIndex = 7;
            this.lblLabelNgayBatDau.Text = "Ngày bắt đầu:";
            // 
            // lblSectionThayDoi
            // 
            this.lblSectionThayDoi.BackColor = System.Drawing.Color.Transparent;
            this.lblSectionThayDoi.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblSectionThayDoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblSectionThayDoi.Location = new System.Drawing.Point(20, 15);
            this.lblSectionThayDoi.Name = "lblSectionThayDoi";
            this.lblSectionThayDoi.Size = new System.Drawing.Size(224, 25);
            this.lblSectionThayDoi.TabIndex = 6;
            this.lblSectionThayDoi.Text = "THÔNG TIN CẦN THAY ĐỔI";
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.BorderRadius = 10;
            this.dtpNgayKetThuc.Checked = true;
            this.dtpNgayKetThuc.FillColor = System.Drawing.Color.White;
            this.dtpNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(20, 316);
            this.dtpNgayKetThuc.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayKetThuc.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(600, 40);
            this.dtpNgayKetThuc.TabIndex = 5;
            this.dtpNgayKetThuc.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.BorderRadius = 10;
            this.dtpNgayBatDau.Checked = true;
            this.dtpNgayBatDau.FillColor = System.Drawing.Color.White;
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(20, 250);
            this.dtpNgayBatDau.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayBatDau.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(600, 40);
            this.dtpNgayBatDau.TabIndex = 4;
            this.dtpNgayBatDau.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // lblThongBao
            // 
            this.lblThongBao.BackColor = System.Drawing.Color.Transparent;
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblThongBao.Location = new System.Drawing.Point(20, 160);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(67, 19);
            this.lblThongBao.TabIndex = 3;
            this.lblThongBao.Text = "Thông báo";
            // 
            // cbGiaoVienThayThe
            // 
            this.cbGiaoVienThayThe.BackColor = System.Drawing.Color.Transparent;
            this.cbGiaoVienThayThe.BorderRadius = 10;
            this.cbGiaoVienThayThe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbGiaoVienThayThe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGiaoVienThayThe.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGiaoVienThayThe.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGiaoVienThayThe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbGiaoVienThayThe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbGiaoVienThayThe.ItemHeight = 30;
            this.cbGiaoVienThayThe.Location = new System.Drawing.Point(20, 120);
            this.cbGiaoVienThayThe.Name = "cbGiaoVienThayThe";
            this.cbGiaoVienThayThe.Size = new System.Drawing.Size(600, 36);
            this.cbGiaoVienThayThe.TabIndex = 2;
            // 
            // lblGiaoVienHienTai
            // 
            this.lblGiaoVienHienTai.BackColor = System.Drawing.Color.Transparent;
            this.lblGiaoVienHienTai.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGiaoVienHienTai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblGiaoVienHienTai.Location = new System.Drawing.Point(20, 50);
            this.lblGiaoVienHienTai.Name = "lblGiaoVienHienTai";
            this.lblGiaoVienHienTai.Size = new System.Drawing.Size(128, 22);
            this.lblGiaoVienHienTai.TabIndex = 1;
            this.lblGiaoVienHienTai.Text = "Giáo viên hiện tại:";
            // 
            // panelThongTinCoBan
            // 
            this.panelThongTinCoBan.BackColor = System.Drawing.Color.White;
            this.panelThongTinCoBan.BorderRadius = 10;
            this.panelThongTinCoBan.Controls.Add(this.lblSectionThongTinCoBan);
            this.panelThongTinCoBan.Controls.Add(this.lblHocKy);
            this.panelThongTinCoBan.Controls.Add(this.lblMonHoc);
            this.panelThongTinCoBan.Controls.Add(this.lblLop);
            this.panelThongTinCoBan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelThongTinCoBan.Location = new System.Drawing.Point(29, 13);
            this.panelThongTinCoBan.Name = "panelThongTinCoBan";
            this.panelThongTinCoBan.Padding = new System.Windows.Forms.Padding(20);
            this.panelThongTinCoBan.Size = new System.Drawing.Size(640, 160);
            this.panelThongTinCoBan.TabIndex = 0;
            // 
            // lblSectionThongTinCoBan
            // 
            this.lblSectionThongTinCoBan.BackColor = System.Drawing.Color.Transparent;
            this.lblSectionThongTinCoBan.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblSectionThongTinCoBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSectionThongTinCoBan.Location = new System.Drawing.Point(20, 15);
            this.lblSectionThongTinCoBan.Name = "lblSectionThongTinCoBan";
            this.lblSectionThongTinCoBan.Size = new System.Drawing.Size(338, 25);
            this.lblSectionThongTinCoBan.TabIndex = 3;
            this.lblSectionThongTinCoBan.Text = "THÔNG TIN CƠ BẢN (Không thể thay đổi)";
            // 
            // lblHocKy
            // 
            this.lblHocKy.BackColor = System.Drawing.Color.Transparent;
            this.lblHocKy.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHocKy.Location = new System.Drawing.Point(20, 120);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new System.Drawing.Size(51, 22);
            this.lblHocKy.TabIndex = 2;
            this.lblHocKy.Text = "Học kỳ:";
            this.lblHocKy.Click += new System.EventHandler(this.lblHocKy_Click);
            // 
            // lblMonHoc
            // 
            this.lblMonHoc.BackColor = System.Drawing.Color.Transparent;
            this.lblMonHoc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblMonHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMonHoc.Location = new System.Drawing.Point(20, 80);
            this.lblMonHoc.Name = "lblMonHoc";
            this.lblMonHoc.Size = new System.Drawing.Size(64, 22);
            this.lblMonHoc.TabIndex = 1;
            this.lblMonHoc.Text = "Môn học:";
            // 
            // lblLop
            // 
            this.lblLop.BackColor = System.Drawing.Color.Transparent;
            this.lblLop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblLop.Location = new System.Drawing.Point(20, 50);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(35, 23);
            this.lblLop.TabIndex = 0;
            this.lblLop.Text = "Lớp:";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(700, 100);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(387, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "✏️ SỬA PHÂN CÔNG GIẢNG DẠY";
            // 
            // FrmSuaPhanCongGiangDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(700, 693);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSuaPhanCongGiangDay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa phân công giảng dạy";
            this.Load += new System.EventHandler(this.FrmSuaPhanCongGiangDay_Load);
            this.panelMain.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelThayDoi.ResumeLayout(false);
            this.panelThayDoi.PerformLayout();
            this.panelThongTinCoBan.ResumeLayout(false);
            this.panelThongTinCoBan.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
