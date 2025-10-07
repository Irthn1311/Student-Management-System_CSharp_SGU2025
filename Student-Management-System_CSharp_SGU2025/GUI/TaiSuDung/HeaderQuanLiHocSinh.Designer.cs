namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class HeaderQuanLiHocSinh
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbHeader = new System.Windows.Forms.Label();
            this.lbGhiChu = new System.Windows.Forms.Label();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThongBao = new Guna.UI2.WinForms.Guna2Button();
            this.PbNguoiDung = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lbTenDangNhap = new System.Windows.Forms.Label();
            this.lbVaiTro = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PbNguoiDung)).BeginInit();
            this.SuspendLayout();
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.Black;
            this.lbHeader.Location = new System.Drawing.Point(15, 14);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(131, 25);
            this.lbHeader.TabIndex = 1;
            this.lbHeader.Text = "Tên Header";
            // 
            // lbGhiChu
            // 
            this.lbGhiChu.AutoSize = true;
            this.lbGhiChu.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGhiChu.ForeColor = System.Drawing.Color.DimGray;
            this.lbGhiChu.Location = new System.Drawing.Point(17, 48);
            this.lbGhiChu.Name = "lbGhiChu";
            this.lbGhiChu.Size = new System.Drawing.Size(53, 17);
            this.lbGhiChu.TabIndex = 2;
            this.lbGhiChu.Text = "Ghi chú";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 7;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.search;
            this.txtTimKiem.IconLeftOffset = new System.Drawing.Point(7, 0);
            this.txtTimKiem.Location = new System.Drawing.Point(642, 23);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm kiếm ...";
            this.txtTimKiem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(261, 35);
            this.txtTimKiem.TabIndex = 6;
            // 
            // btnThongBao
            // 
            this.btnThongBao.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThongBao.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThongBao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThongBao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThongBao.FillColor = System.Drawing.Color.Transparent;
            this.btnThongBao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThongBao.ForeColor = System.Drawing.Color.White;
            this.btnThongBao.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.notification_bell;
            this.btnThongBao.ImageSize = new System.Drawing.Size(25, 25);
            this.btnThongBao.Location = new System.Drawing.Point(920, 14);
            this.btnThongBao.Name = "btnThongBao";
            this.btnThongBao.Size = new System.Drawing.Size(45, 51);
            this.btnThongBao.TabIndex = 7;
            // 
            // PbNguoiDung
            // 
            this.PbNguoiDung.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PbNguoiDung.FillColor = System.Drawing.Color.Transparent;
            this.PbNguoiDung.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.student;
            this.PbNguoiDung.ImageRotate = 0F;
            this.PbNguoiDung.Location = new System.Drawing.Point(971, 14);
            this.PbNguoiDung.Name = "PbNguoiDung";
            this.PbNguoiDung.ShadowDecoration.BorderRadius = 50;
            this.PbNguoiDung.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.PbNguoiDung.Size = new System.Drawing.Size(62, 51);
            this.PbNguoiDung.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbNguoiDung.TabIndex = 8;
            this.PbNguoiDung.TabStop = false;
            // 
            // lbTenDangNhap
            // 
            this.lbTenDangNhap.AutoSize = true;
            this.lbTenDangNhap.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTenDangNhap.ForeColor = System.Drawing.Color.Black;
            this.lbTenDangNhap.Location = new System.Drawing.Point(1039, 14);
            this.lbTenDangNhap.Name = "lbTenDangNhap";
            this.lbTenDangNhap.Size = new System.Drawing.Size(77, 23);
            this.lbTenDangNhap.TabIndex = 9;
            this.lbTenDangNhap.Text = "Họ Tên ";
            // 
            // lbVaiTro
            // 
            this.lbVaiTro.AutoSize = true;
            this.lbVaiTro.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVaiTro.ForeColor = System.Drawing.Color.DimGray;
            this.lbVaiTro.Location = new System.Drawing.Point(1039, 48);
            this.lbVaiTro.Name = "lbVaiTro";
            this.lbVaiTro.Size = new System.Drawing.Size(54, 19);
            this.lbVaiTro.TabIndex = 10;
            this.lbVaiTro.Text = "Vai trò";
            // 
            // HeaderQuanLiHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbVaiTro);
            this.Controls.Add(this.lbTenDangNhap);
            this.Controls.Add(this.PbNguoiDung);
            this.Controls.Add(this.btnThongBao);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lbGhiChu);
            this.Controls.Add(this.lbHeader);
            this.Name = "HeaderQuanLiHocSinh";
            this.Size = new System.Drawing.Size(1184, 81);
            this.Load += new System.EventHandler(this.HeaderQuanLiHocSinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PbNguoiDung)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbHeader;
        public System.Windows.Forms.Label lbGhiChu;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2Button btnThongBao;
        public Guna.UI2.WinForms.Guna2CirclePictureBox PbNguoiDung;
        public System.Windows.Forms.Label lbTenDangNhap;
        public System.Windows.Forms.Label lbVaiTro;
    }
}
