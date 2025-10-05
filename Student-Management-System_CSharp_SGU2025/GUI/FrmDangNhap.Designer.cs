namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FrmDangNhap
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelLeftDangNhap = new Guna.UI2.WinForms.Guna2Panel();
            this.lbChaoMung2 = new System.Windows.Forms.Label();
            this.lbChaoMung1 = new System.Windows.Forms.Label();
            this.logoChaoMung = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTenDangNhap = new System.Windows.Forms.Label();
            this.lbMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenDangNhap = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            this.linkLbQuenMatKhau = new System.Windows.Forms.LinkLabel();
            this.panelLeftDangNhap.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeftDangNhap
            // 
            this.panelLeftDangNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.panelLeftDangNhap.Controls.Add(this.lbChaoMung2);
            this.panelLeftDangNhap.Controls.Add(this.lbChaoMung1);
            this.panelLeftDangNhap.Controls.Add(this.logoChaoMung);
            this.panelLeftDangNhap.Location = new System.Drawing.Point(3, 0);
            this.panelLeftDangNhap.Name = "panelLeftDangNhap";
            this.panelLeftDangNhap.Size = new System.Drawing.Size(724, 856);
            this.panelLeftDangNhap.TabIndex = 0;
            // 
            // lbChaoMung2
            // 
            this.lbChaoMung2.AutoSize = true;
            this.lbChaoMung2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChaoMung2.ForeColor = System.Drawing.Color.White;
            this.lbChaoMung2.Location = new System.Drawing.Point(88, 454);
            this.lbChaoMung2.Name = "lbChaoMung2";
            this.lbChaoMung2.Size = new System.Drawing.Size(526, 26);
            this.lbChaoMung2.TabIndex = 2;
            this.lbChaoMung2.Text = "Chào mừng đến với hệ thống quản lí học sinh hiện đại";
            // 
            // lbChaoMung1
            // 
            this.lbChaoMung1.AutoSize = true;
            this.lbChaoMung1.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbChaoMung1.ForeColor = System.Drawing.Color.White;
            this.lbChaoMung1.Location = new System.Drawing.Point(116, 395);
            this.lbChaoMung1.Name = "lbChaoMung1";
            this.lbChaoMung1.Size = new System.Drawing.Size(472, 42);
            this.lbChaoMung1.TabIndex = 1;
            this.lbChaoMung1.Text = "Hệ Thống Quản Lí Học Sinh";
            this.lbChaoMung1.Click += new System.EventHandler(this.label1_Click);
            // 
            // logoChaoMung
            // 
            this.logoChaoMung.BackColor = System.Drawing.Color.Transparent;
            this.logoChaoMung.BackgroundImage = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.school1;
            this.logoChaoMung.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logoChaoMung.Location = new System.Drawing.Point(251, 237);
            this.logoChaoMung.Name = "logoChaoMung";
            this.logoChaoMung.Size = new System.Drawing.Size(200, 154);
            this.logoChaoMung.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1006, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 35);
            this.label1.TabIndex = 3;
            this.label1.Text = "Đăng nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(978, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Vui lòng nhập để tiếp tục";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbTenDangNhap
            // 
            this.lbTenDangNhap.AutoSize = true;
            this.lbTenDangNhap.BackColor = System.Drawing.Color.White;
            this.lbTenDangNhap.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTenDangNhap.ForeColor = System.Drawing.Color.DimGray;
            this.lbTenDangNhap.Location = new System.Drawing.Point(842, 312);
            this.lbTenDangNhap.Name = "lbTenDangNhap";
            this.lbTenDangNhap.Size = new System.Drawing.Size(124, 22);
            this.lbTenDangNhap.TabIndex = 5;
            this.lbTenDangNhap.Text = "Tên đăng nhập";
            this.lbTenDangNhap.Click += new System.EventHandler(this.lbTenDangNhap_Click);
            // 
            // lbMatKhau
            // 
            this.lbMatKhau.AutoSize = true;
            this.lbMatKhau.BackColor = System.Drawing.Color.White;
            this.lbMatKhau.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMatKhau.ForeColor = System.Drawing.Color.DimGray;
            this.lbMatKhau.Location = new System.Drawing.Point(842, 436);
            this.lbMatKhau.Name = "lbMatKhau";
            this.lbMatKhau.Size = new System.Drawing.Size(82, 22);
            this.lbMatKhau.TabIndex = 7;
            this.lbMatKhau.Text = "Mật khẩu";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BorderRadius = 7;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "";
            this.txtMatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhau.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.padlock;
            this.txtMatKhau.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtMatKhau.Location = new System.Drawing.Point(846, 475);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PlaceholderText = "Nhập mật khẩu";
            this.txtMatKhau.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtMatKhau.SelectedText = "";
            this.txtMatKhau.Size = new System.Drawing.Size(466, 37);
            this.txtMatKhau.TabIndex = 8;
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.BorderRadius = 7;
            this.txtTenDangNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDangNhap.DefaultText = "";
            this.txtTenDangNhap.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenDangNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenDangNhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenDangNhap.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenDangNhap.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDangNhap.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenDangNhap.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.people2;
            this.txtTenDangNhap.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtTenDangNhap.Location = new System.Drawing.Point(846, 348);
            this.txtTenDangNhap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập";
            this.txtTenDangNhap.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTenDangNhap.SelectedText = "";
            this.txtTenDangNhap.Size = new System.Drawing.Size(466, 37);
            this.txtTenDangNhap.TabIndex = 6;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BorderRadius = 6;
            this.btnDangNhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDangNhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDangNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDangNhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(1001, 551);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(180, 45);
            this.btnDangNhap.TabIndex = 9;
            this.btnDangNhap.Text = "Đăng nhập";
            // 
            // linkLbQuenMatKhau
            // 
            this.linkLbQuenMatKhau.AutoSize = true;
            this.linkLbQuenMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbQuenMatKhau.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.linkLbQuenMatKhau.Location = new System.Drawing.Point(1023, 616);
            this.linkLbQuenMatKhau.Name = "linkLbQuenMatKhau";
            this.linkLbQuenMatKhau.Size = new System.Drawing.Size(136, 20);
            this.linkLbQuenMatKhau.TabIndex = 10;
            this.linkLbQuenMatKhau.TabStop = true;
            this.linkLbQuenMatKhau.Text = "Quên mật khẩu ?";
            // 
            // FrmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1422, 853);
            this.Controls.Add(this.linkLbQuenMatKhau);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.lbMatKhau);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.lbTenDangNhap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelLeftDangNhap);
            this.Name = "FrmDangNhap";
            this.Text = "FrmDangNhap";
            this.Load += new System.EventHandler(this.FrmDangNhap_Load);
            this.panelLeftDangNhap.ResumeLayout(false);
            this.panelLeftDangNhap.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelLeftDangNhap;
        private Guna.UI2.WinForms.Guna2Panel logoChaoMung;
        private System.Windows.Forms.Label lbChaoMung1;
        private System.Windows.Forms.Label lbChaoMung2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTenDangNhap;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lbMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private System.Windows.Forms.LinkLabel linkLbQuenMatKhau;
    }
}