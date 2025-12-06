namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ThemLopHoc
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
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.cbGVCN = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbKhoi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtTenLop = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaLop = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSiSo = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblGVCN = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSiSo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtKhoi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTenLop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMaLop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.lbHeader);
            this.guna2Panel1.Controls.Add(this.btnDong);
            this.guna2Panel1.Controls.Add(this.btnThem);
            this.guna2Panel1.Controls.Add(this.cbGVCN);
            this.guna2Panel1.Controls.Add(this.cbKhoi);
            this.guna2Panel1.Controls.Add(this.txtTenLop);
            this.guna2Panel1.Controls.Add(this.txtMaLop);
            this.guna2Panel1.Controls.Add(this.txtSiSo);
            this.guna2Panel1.Controls.Add(this.lblGVCN);
            this.guna2Panel1.Controls.Add(this.lblSiSo);
            this.guna2Panel1.Controls.Add(this.txtKhoi);
            this.guna2Panel1.Controls.Add(this.lblTenLop);
            this.guna2Panel1.Controls.Add(this.lblMaLop);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.SystemColors.Control;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(459, 489);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // lbHeader
            // 
            this.lbHeader.AutoSize = true;
            this.lbHeader.BackColor = System.Drawing.Color.Transparent;
            this.lbHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeader.ForeColor = System.Drawing.Color.White;
            this.lbHeader.Location = new System.Drawing.Point(27, 18);
            this.lbHeader.Name = "lbHeader";
            this.lbHeader.Size = new System.Drawing.Size(114, 21);
            this.lbHeader.TabIndex = 13;
            this.lbHeader.Text = "Thêm lớp học";
            this.lbHeader.Click += new System.EventHandler(this.lbHeader_Click);
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
            this.btnDong.Location = new System.Drawing.Point(56, 411);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(122, 29);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
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
            this.btnThem.Image = global::Student_Management_System_CSharp_SGU2025.GUI.Properties.Resources.plus;
            this.btnThem.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThem.Location = new System.Drawing.Point(269, 411);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(149, 29);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "Thêm lớp học";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // cbGVCN
            // 
            this.cbGVCN.BackColor = System.Drawing.Color.Transparent;
            this.cbGVCN.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbGVCN.BorderRadius = 5;
            this.cbGVCN.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbGVCN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGVCN.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGVCN.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGVCN.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbGVCN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbGVCN.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.cbGVCN.ItemHeight = 30;
            this.cbGVCN.Location = new System.Drawing.Point(176, 334);
            this.cbGVCN.Name = "cbGVCN";
            this.cbGVCN.Size = new System.Drawing.Size(242, 36);
            this.cbGVCN.TabIndex = 5;
            // 
            // cbKhoi
            // 
            this.cbKhoi.BackColor = System.Drawing.Color.Transparent;
            this.cbKhoi.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbKhoi.BorderRadius = 5;
            this.cbKhoi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbKhoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhoi.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbKhoi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbKhoi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbKhoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbKhoi.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.cbKhoi.ItemHeight = 30;
            this.cbKhoi.Items.AddRange(new object[] {
            "Chọn khối ...",
            "10",
            "11",
            "12"});
            this.cbKhoi.Location = new System.Drawing.Point(176, 209);
            this.cbKhoi.Name = "cbKhoi";
            this.cbKhoi.Size = new System.Drawing.Size(242, 36);
            this.cbKhoi.StartIndex = 0;
            this.cbKhoi.TabIndex = 3;
            this.cbKhoi.SelectedIndexChanged += new System.EventHandler(this.guna2ComboBox1_SelectedIndexChanged);
            // 
            // txtTenLop
            // 
            this.txtTenLop.BackColor = System.Drawing.Color.Transparent;
            this.txtTenLop.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTenLop.BorderRadius = 5;
            this.txtTenLop.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenLop.DefaultText = "";
            this.txtTenLop.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenLop.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenLop.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenLop.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenLop.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenLop.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenLop.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtTenLop.Location = new System.Drawing.Point(176, 154);
            this.txtTenLop.Name = "txtTenLop";
            this.txtTenLop.PlaceholderText = "";
            this.txtTenLop.SelectedText = "";
            this.txtTenLop.Size = new System.Drawing.Size(242, 32);
            this.txtTenLop.TabIndex = 2;
            // 
            // txtMaLop
            // 
            this.txtMaLop.BackColor = System.Drawing.Color.Transparent;
            this.txtMaLop.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtMaLop.BorderRadius = 5;
            this.txtMaLop.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaLop.DefaultText = "";
            this.txtMaLop.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaLop.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaLop.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaLop.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaLop.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaLop.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaLop.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtMaLop.Location = new System.Drawing.Point(176, 88);
            this.txtMaLop.Name = "txtMaLop";
            this.txtMaLop.PlaceholderText = "";
            this.txtMaLop.SelectedText = "";
            this.txtMaLop.Size = new System.Drawing.Size(242, 32);
            this.txtMaLop.TabIndex = 1;
            // 
            // txtSiSo
            // 
            this.txtSiSo.BackColor = System.Drawing.Color.Transparent;
            this.txtSiSo.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSiSo.BorderRadius = 5;
            this.txtSiSo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSiSo.DefaultText = "";
            this.txtSiSo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSiSo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSiSo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSiSo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSiSo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSiSo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSiSo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSiSo.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.txtSiSo.Location = new System.Drawing.Point(176, 268);
            this.txtSiSo.Name = "txtSiSo";
            this.txtSiSo.PlaceholderText = "";
            this.txtSiSo.SelectedText = "";
            this.txtSiSo.Size = new System.Drawing.Size(242, 32);
            this.txtSiSo.TabIndex = 4;
            // 
            // lblGVCN
            // 
            this.lblGVCN.BackColor = System.Drawing.Color.Transparent;
            this.lblGVCN.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGVCN.Location = new System.Drawing.Point(52, 340);
            this.lblGVCN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblGVCN.Name = "lblGVCN";
            this.lblGVCN.Size = new System.Drawing.Size(46, 23);
            this.lblGVCN.TabIndex = 12;
            this.lblGVCN.Text = "GVCN";
            this.lblGVCN.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSiSo
            // 
            this.lblSiSo.BackColor = System.Drawing.Color.Transparent;
            this.lblSiSo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSiSo.Location = new System.Drawing.Point(52, 277);
            this.lblSiSo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblSiSo.Name = "lblSiSo";
            this.lblSiSo.Size = new System.Drawing.Size(37, 23);
            this.lblSiSo.TabIndex = 11;
            this.lblSiSo.Text = "Sĩ số";
            this.lblSiSo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtKhoi
            // 
            this.txtKhoi.BackColor = System.Drawing.Color.Transparent;
            this.txtKhoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKhoi.Location = new System.Drawing.Point(52, 222);
            this.txtKhoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtKhoi.Name = "txtKhoi";
            this.txtKhoi.Size = new System.Drawing.Size(36, 23);
            this.txtKhoi.TabIndex = 10;
            this.txtKhoi.Text = "Khối";
            this.txtKhoi.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTenLop
            // 
            this.lblTenLop.BackColor = System.Drawing.Color.Transparent;
            this.lblTenLop.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenLop.Location = new System.Drawing.Point(52, 163);
            this.lblTenLop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblTenLop.Name = "lblTenLop";
            this.lblTenLop.Size = new System.Drawing.Size(58, 23);
            this.lblTenLop.TabIndex = 8;
            this.lblTenLop.Text = "Tên lớp";
            this.lblTenLop.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMaLop
            // 
            this.lblMaLop.BackColor = System.Drawing.Color.Transparent;
            this.lblMaLop.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaLop.Location = new System.Drawing.Point(56, 98);
            this.lblMaLop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblMaLop.Name = "lblMaLop";
            this.lblMaLop.Size = new System.Drawing.Size(54, 23);
            this.lblMaLop.TabIndex = 7;
            this.lblMaLop.Text = "Mã lớp";
            this.lblMaLop.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMaLop.Click += new System.EventHandler(this.guna2HtmlLabel2_Click_1);
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
            this.guna2HtmlLabel1.Click += new System.EventHandler(this.guna2HtmlLabel1_Click_1);
            // 
            // ThemLopHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 489);
            this.ControlBox = false;
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ThemLopHoc";
            this.Text = "FrmLopHocComponent";
            this.Load += new System.EventHandler(this.FrmLopHocComponent_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox txtMaLop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGVCN;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSiSo;
        private Guna.UI2.WinForms.Guna2HtmlLabel txtKhoi;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTenLop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMaLop;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TextBox txtTenLop;
        private Guna.UI2.WinForms.Guna2TextBox txtSiSo;
        private Guna.UI2.WinForms.Guna2ComboBox cbKhoi;
        private Guna.UI2.WinForms.Guna2ComboBox cbGVCN;
        private Guna.UI2.WinForms.Guna2Button btnDong;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private System.Windows.Forms.Label lbHeader;
    }
}