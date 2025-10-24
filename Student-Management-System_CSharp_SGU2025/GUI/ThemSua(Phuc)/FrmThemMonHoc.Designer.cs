namespace Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_
{
    partial class FrmThemMonHoc
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lbHeader = new System.Windows.Forms.Label();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.cbGhiChu = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtSoTiet = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTenMon = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaMon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblGhiChu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSoTiet = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTenMon = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMaMon = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.lbHeader);
            this.guna2Panel1.Controls.Add(this.btnDong);
            this.guna2Panel1.Controls.Add(this.btnThem);
            this.guna2Panel1.Controls.Add(this.cbGhiChu);
            this.guna2Panel1.Controls.Add(this.txtSoTiet);
            this.guna2Panel1.Controls.Add(this.txtTenMon);
            this.guna2Panel1.Controls.Add(this.txtMaMon);
            this.guna2Panel1.Controls.Add(this.lblGhiChu);
            this.guna2Panel1.Controls.Add(this.lblSoTiet);
            this.guna2Panel1.Controls.Add(this.lblTenMon);
            this.guna2Panel1.Controls.Add(this.lblMaMon);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.SystemColors.Control;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(459, 489);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.BackColor = System.Drawing.Color.Transparent;
            this.lbHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.White;
            this.lbHeader.Location = new System.Drawing.Point(27, 18);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(124, 21);
            this.lbHeader.TabIndex = 13;
            this.lbHeader.Text = "Thêm môn học";
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.Transparent;
            this.btnDong.BorderRadius = 5;
            this.btnDong.BorderStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            this.btnDong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDong.FillColor = System.Drawing.Color.YellowGreen;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(69, 367);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(122, 29);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // cbGhiChu
            // 
            this.cbGhiChu.BackColor = System.Drawing.Color.Transparent;
            this.cbGhiChu.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbGhiChu.BorderRadius = 5;
            this.cbGhiChu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbGhiChu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGhiChu.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGhiChu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbGhiChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbGhiChu.ItemHeight = 30;
            this.cbGhiChu.Items.AddRange(new object[] {
            "-- Chọn loại môn học --",
            "Môn chính",
            "Khoa học tự nhiên",
            "Khoa học xã hội",
            "Kỹ năng khác"});
            this.cbGhiChu.Location = new System.Drawing.Point(169, 266);
            this.cbGhiChu.Name = "cbGhiChu";
            this.cbGhiChu.Size = new System.Drawing.Size(242, 36);
            this.cbGhiChu.StartIndex = 0;
            this.cbGhiChu.TabIndex = 4;
            // 
            // txtSoTiet
            // 
            this.txtSoTiet.BackColor = System.Drawing.Color.Transparent;
            this.txtSoTiet.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSoTiet.BorderRadius = 5;
            this.txtSoTiet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoTiet.DefaultText = "";
            this.txtSoTiet.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSoTiet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSoTiet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoTiet.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoTiet.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoTiet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoTiet.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoTiet.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtSoTiet.Location = new System.Drawing.Point(169, 200);
            this.txtSoTiet.Name = "txtSoTiet";
            this.txtSoTiet.PlaceholderText = "Nhập số tiết";
            this.txtSoTiet.SelectedText = "";
            this.txtSoTiet.Size = new System.Drawing.Size(242, 32);
            this.txtSoTiet.TabIndex = 3;
            // 
            // txtTenMon
            // 
            this.txtTenMon.BackColor = System.Drawing.Color.Transparent;
            this.txtTenMon.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTenMon.BorderRadius = 5;
            this.txtTenMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenMon.DefaultText = "";
            this.txtTenMon.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenMon.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenMon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenMon.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenMon.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtTenMon.Location = new System.Drawing.Point(169, 137);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.PlaceholderText = "Nhập tên môn học";
            this.txtTenMon.SelectedText = "";
            this.txtTenMon.Size = new System.Drawing.Size(242, 32);
            this.txtTenMon.TabIndex = 2;
            // 
            // txtMaMon
            // 
            this.txtMaMon.BackColor = System.Drawing.Color.Transparent;
            this.txtMaMon.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtMaMon.BorderRadius = 5;
            this.txtMaMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaMon.DefaultText = "";
            this.txtMaMon.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaMon.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaMon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaMon.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaMon.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtMaMon.Location = new System.Drawing.Point(169, 79);
            this.txtMaMon.Name = "txtMaMon";
            this.txtMaMon.PlaceholderText = "";
            this.txtMaMon.SelectedText = "";
            this.txtMaMon.Size = new System.Drawing.Size(242, 32);
            this.txtMaMon.TabIndex = 1;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.BackColor = System.Drawing.Color.Transparent;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.Location = new System.Drawing.Point(31, 266);
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(70, 23);
            this.lblGhiChu.TabIndex = 12;
            this.lblGhiChu.Text = "Loại môn";
            this.lblGhiChu.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoTiet
            // 
            this.lblSoTiet.BackColor = System.Drawing.Color.Transparent;
            this.lblSoTiet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoTiet.Location = new System.Drawing.Point(31, 200);
            this.lblSoTiet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblSoTiet.Name = "lblSoTiet";
            this.lblSoTiet.Size = new System.Drawing.Size(51, 23);
            this.lblSoTiet.TabIndex = 10;
            this.lblSoTiet.Text = "Số tiết";
            this.lblSoTiet.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTenMon
            // 
            this.lblTenMon.BackColor = System.Drawing.Color.Transparent;
            this.lblTenMon.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenMon.Location = new System.Drawing.Point(31, 135);
            this.lblTenMon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblTenMon.Name = "lblTenMon";
            this.lblTenMon.Size = new System.Drawing.Size(98, 23);
            this.lblTenMon.TabIndex = 8;
            this.lblTenMon.Text = "Tên môn học";
            this.lblTenMon.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMaMon
            // 
            this.lblMaMon.BackColor = System.Drawing.Color.Transparent;
            this.lblMaMon.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaMon.Location = new System.Drawing.Point(31, 79);
            this.lblMaMon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblMaMon.Name = "lblMaMon";
            this.lblMaMon.Size = new System.Drawing.Size(94, 23);
            this.lblMaMon.TabIndex = 7;
            this.lblMaMon.Text = "Mã môn học";
            this.lblMaMon.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.guna2HtmlLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(0, 0);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(459, 57);
            this.guna2HtmlLabel1.TabIndex = 6;
            this.guna2HtmlLabel1.Text = null;
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.Transparent;
            this.btnThem.BorderRadius = 5;
            this.btnThem.BorderStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            this.btnThem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus2;
            this.btnThem.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThem.Location = new System.Drawing.Point(281, 367);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(149, 29);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "Thêm môn học";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // FrmThemMonHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 489);
            this.ControlBox = false;
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmThemMonHoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm môn học";
            this.Load += new System.EventHandler(this.FrmThemMonHoc_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lbHeader;
        private Guna.UI2.WinForms.Guna2Button btnDong;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2ComboBox cbGhiChu; // ✅ THAY ĐỔI
        private Guna.UI2.WinForms.Guna2TextBox txtSoTiet;
        private Guna.UI2.WinForms.Guna2TextBox txtTenMon;
        private Guna.UI2.WinForms.Guna2TextBox txtMaMon;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGhiChu;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoTiet;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTenMon;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMaMon;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}