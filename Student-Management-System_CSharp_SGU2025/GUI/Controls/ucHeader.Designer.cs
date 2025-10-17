namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ucHeader
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
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.btnNotifications = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblBreadcrumb = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLogName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tenDangNhap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox4 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblLogName);
            this.pnlHeader.Controls.Add(this.tenDangNhap);
            this.pnlHeader.Controls.Add(this.guna2PictureBox4);
            this.pnlHeader.Controls.Add(this.btnNotifications);
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.lblBreadcrumb);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.FillColor = System.Drawing.Color.White;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(27, 18, 27, 18);
            this.pnlHeader.Size = new System.Drawing.Size(1184, 81);
            this.pnlHeader.TabIndex = 1;
            // 
            // btnNotifications
            // 
            this.btnNotifications.BackColor = System.Drawing.Color.White;
            this.btnNotifications.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNotifications.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.btnNotifications.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNotifications.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNotifications.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNotifications.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNotifications.FillColor = System.Drawing.Color.Transparent;
            this.btnNotifications.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNotifications.ForeColor = System.Drawing.Color.White;
            this.btnNotifications.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.btnNotifications.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.notification_bell;
            this.btnNotifications.ImageSize = new System.Drawing.Size(25, 25);
            this.btnNotifications.Location = new System.Drawing.Point(927, 29);
            this.btnNotifications.Name = "btnNotifications";
            this.btnNotifications.Size = new System.Drawing.Size(40, 40);
            this.btnNotifications.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderRadius = 8;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.search;
            this.txtSearch.Location = new System.Drawing.Point(665, 29);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Tìm kiếm...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(256, 40);
            this.txtSearch.TabIndex = 2;
            // 
            // lblBreadcrumb
            // 
            this.lblBreadcrumb.AutoSize = true;
            this.lblBreadcrumb.BackColor = System.Drawing.Color.White;
            this.lblBreadcrumb.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBreadcrumb.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBreadcrumb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(115)))), ((int)(((byte)(128)))));
            this.lblBreadcrumb.Location = new System.Drawing.Point(27, 55);
            this.lblBreadcrumb.Name = "lblBreadcrumb";
            this.lblBreadcrumb.Size = new System.Drawing.Size(167, 23);
            this.lblBreadcrumb.TabIndex = 1;
            this.lblBreadcrumb.Text = "Trang chủ / Bảng tin";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(27, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(124, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bảng tin";
            // 
            // lblLogName
            // 
            this.lblLogName.AutoSize = false;
            this.lblLogName.BackColor = System.Drawing.Color.Transparent;
            this.lblLogName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogName.ForeColor = System.Drawing.Color.DimGray;
            this.lblLogName.Location = new System.Drawing.Point(1027, 53);
            this.lblLogName.Margin = new System.Windows.Forms.Padding(2);
            this.lblLogName.Name = "lblLogName";
            this.lblLogName.Size = new System.Drawing.Size(67, 16);
            this.lblLogName.TabIndex = 10;
            this.lblLogName.Text = "Login name";
            // 
            // tenDangNhap
            // 
            this.tenDangNhap.AutoSize = false;
            this.tenDangNhap.BackColor = System.Drawing.Color.Transparent;
            this.tenDangNhap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenDangNhap.Location = new System.Drawing.Point(1027, 26);
            this.tenDangNhap.Margin = new System.Windows.Forms.Padding(2);
            this.tenDangNhap.Name = "tenDangNhap";
            this.tenDangNhap.Size = new System.Drawing.Size(128, 23);
            this.tenDangNhap.TabIndex = 8;
            this.tenDangNhap.Text = "Tên đăng nhập";
            // 
            // guna2PictureBox4
            // 
            this.guna2PictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox4.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.user;
            this.guna2PictureBox4.ImageRotate = 0F;
            this.guna2PictureBox4.Location = new System.Drawing.Point(983, 29);
            this.guna2PictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.guna2PictureBox4.Name = "guna2PictureBox4";
            this.guna2PictureBox4.Size = new System.Drawing.Size(40, 40);
            this.guna2PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox4.TabIndex = 9;
            this.guna2PictureBox4.TabStop = false;
            // 
            // ucHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucHeader";
            this.Size = new System.Drawing.Size(1184, 81);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2Button btnNotifications;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private System.Windows.Forms.Label lblBreadcrumb;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLogName;
        private Guna.UI2.WinForms.Guna2HtmlLabel tenDangNhap;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox4;
    }
}
