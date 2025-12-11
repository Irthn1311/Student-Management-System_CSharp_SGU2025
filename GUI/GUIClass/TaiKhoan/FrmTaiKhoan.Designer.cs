namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FrmTaiKhoan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnVaiTro = new Guna.UI2.WinForms.Guna2Button();
            this.tbTaiKhoan = new Guna.UI2.WinForms.Guna2DataGridView();
            this.tenTaiKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vaiTro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thaoTac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddAcc = new Guna.UI2.WinForms.Guna2Button();
            this.statCardThongKeTaiKhoan4 = new Student_Management_System_CSharp_SGU2025.GUI.TaiKhoan.statCardThongKeTaiKhoan();
            this.statCardThongKeTaiKhoan3 = new Student_Management_System_CSharp_SGU2025.GUI.TaiKhoan.statCardThongKeTaiKhoan();
            this.statCardThongKeTaiKhoan2 = new Student_Management_System_CSharp_SGU2025.GUI.TaiKhoan.statCardThongKeTaiKhoan();
            this.statCardThongKeTaiKhoan1 = new Student_Management_System_CSharp_SGU2025.GUI.TaiKhoan.statCardThongKeTaiKhoan();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTrangHienTai = new System.Windows.Forms.Label();
            this.btnTrangTruoc = new Guna.UI2.WinForms.Guna2Button();
            this.btnTrangSau = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbTaiKhoan)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVaiTro
            // 
            this.btnVaiTro.BorderRadius = 8;
            this.btnVaiTro.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVaiTro.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVaiTro.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVaiTro.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVaiTro.FillColor = System.Drawing.Color.White;
            this.btnVaiTro.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVaiTro.ForeColor = System.Drawing.Color.Black;
            this.btnVaiTro.Location = new System.Drawing.Point(970, 24);
            this.btnVaiTro.Name = "btnVaiTro";
            this.btnVaiTro.Size = new System.Drawing.Size(162, 34);
            this.btnVaiTro.TabIndex = 2;
            this.btnVaiTro.Text = "Tất cả vai trò";
            this.btnVaiTro.Click += new System.EventHandler(this.btnVaiTro_Click);
            // 
            // tbTaiKhoan
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.tbTaiKhoan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbTaiKhoan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.tbTaiKhoan.ColumnHeadersHeight = 18;
            this.tbTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tbTaiKhoan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tenTaiKhoan,
            this.vaiTro,
            this.trangThai,
            this.lastLogin,
            this.thaoTac});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tbTaiKhoan.DefaultCellStyle = dataGridViewCellStyle9;
            this.tbTaiKhoan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tbTaiKhoan.Location = new System.Drawing.Point(32, 287);
            this.tbTaiKhoan.Margin = new System.Windows.Forms.Padding(2);
            this.tbTaiKhoan.Name = "tbTaiKhoan";
            this.tbTaiKhoan.RowHeadersVisible = false;
            this.tbTaiKhoan.RowHeadersWidth = 51;
            this.tbTaiKhoan.RowTemplate.Height = 24;
            this.tbTaiKhoan.Size = new System.Drawing.Size(1100, 449);
            this.tbTaiKhoan.TabIndex = 7;
            this.tbTaiKhoan.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tbTaiKhoan.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tbTaiKhoan.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tbTaiKhoan.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tbTaiKhoan.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tbTaiKhoan.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tbTaiKhoan.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tbTaiKhoan.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tbTaiKhoan.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tbTaiKhoan.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTaiKhoan.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tbTaiKhoan.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tbTaiKhoan.ThemeStyle.HeaderStyle.Height = 18;
            this.tbTaiKhoan.ThemeStyle.ReadOnly = false;
            this.tbTaiKhoan.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tbTaiKhoan.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tbTaiKhoan.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTaiKhoan.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tbTaiKhoan.ThemeStyle.RowsStyle.Height = 24;
            this.tbTaiKhoan.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tbTaiKhoan.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tbTaiKhoan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tbTaiKhoan_CellContentClick);
            // 
            // tenTaiKhoan
            // 
            this.tenTaiKhoan.HeaderText = "Tên tài khoản";
            this.tenTaiKhoan.MinimumWidth = 6;
            this.tenTaiKhoan.Name = "tenTaiKhoan";
            // 
            // vaiTro
            // 
            this.vaiTro.HeaderText = "Vai trò";
            this.vaiTro.MinimumWidth = 6;
            this.vaiTro.Name = "vaiTro";
            // 
            // trangThai
            // 
            this.trangThai.HeaderText = "Trạng thái";
            this.trangThai.MinimumWidth = 6;
            this.trangThai.Name = "trangThai";
            // 
            // lastLogin
            // 
            this.lastLogin.HeaderText = "Đăng nhập gần nhất";
            this.lastLogin.MinimumWidth = 6;
            this.lastLogin.Name = "lastLogin";
            // 
            // thaoTac
            // 
            this.thaoTac.HeaderText = "Thao Tác";
            this.thaoTac.MinimumWidth = 6;
            this.thaoTac.Name = "thaoTac";
            // 
            // btnAddAcc
            // 
            this.btnAddAcc.BorderRadius = 8;
            this.btnAddAcc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddAcc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddAcc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddAcc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddAcc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnAddAcc.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAcc.ForeColor = System.Drawing.Color.White;
            this.btnAddAcc.Image = global::Student_Management_System_CSharp_SGU2025.GUI.Properties.Resources.plus;
            this.btnAddAcc.ImageSize = new System.Drawing.Size(15, 15);
            this.btnAddAcc.Location = new System.Drawing.Point(32, 21);
            this.btnAddAcc.Name = "btnAddAcc";
            this.btnAddAcc.Size = new System.Drawing.Size(179, 40);
            this.btnAddAcc.TabIndex = 1;
            this.btnAddAcc.Text = "Tạo tài khoản";
            this.btnAddAcc.Click += new System.EventHandler(this.btnAddAcc_Click);
            // 
            // statCardThongKeTaiKhoan4
            // 
            this.statCardThongKeTaiKhoan4.BackColor = System.Drawing.Color.Transparent;
            this.statCardThongKeTaiKhoan4.Icon = null;
            this.statCardThongKeTaiKhoan4.IconFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.statCardThongKeTaiKhoan4.Location = new System.Drawing.Point(870, 90);
            this.statCardThongKeTaiKhoan4.Name = "statCardThongKeTaiKhoan4";
            this.statCardThongKeTaiKhoan4.PanelBackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.statCardThongKeTaiKhoan4.PictureBoxBackgroundColor = System.Drawing.Color.Transparent;
            this.statCardThongKeTaiKhoan4.Size = new System.Drawing.Size(262, 176);
            this.statCardThongKeTaiKhoan4.TabIndex = 8;
            this.statCardThongKeTaiKhoan4.TitleGhiChu = "Note";
            this.statCardThongKeTaiKhoan4.TitleLietKe = "Value";
            // 
            // statCardThongKeTaiKhoan3
            // 
            this.statCardThongKeTaiKhoan3.BackColor = System.Drawing.Color.Transparent;
            this.statCardThongKeTaiKhoan3.Icon = null;
            this.statCardThongKeTaiKhoan3.IconFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.statCardThongKeTaiKhoan3.Location = new System.Drawing.Point(589, 90);
            this.statCardThongKeTaiKhoan3.Name = "statCardThongKeTaiKhoan3";
            this.statCardThongKeTaiKhoan3.PanelBackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.statCardThongKeTaiKhoan3.PictureBoxBackgroundColor = System.Drawing.Color.Transparent;
            this.statCardThongKeTaiKhoan3.Size = new System.Drawing.Size(262, 176);
            this.statCardThongKeTaiKhoan3.TabIndex = 8;
            this.statCardThongKeTaiKhoan3.TitleGhiChu = "Note";
            this.statCardThongKeTaiKhoan3.TitleLietKe = "Value";
            // 
            // statCardThongKeTaiKhoan2
            // 
            this.statCardThongKeTaiKhoan2.BackColor = System.Drawing.Color.Transparent;
            this.statCardThongKeTaiKhoan2.Icon = null;
            this.statCardThongKeTaiKhoan2.IconFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.statCardThongKeTaiKhoan2.Location = new System.Drawing.Point(309, 90);
            this.statCardThongKeTaiKhoan2.Name = "statCardThongKeTaiKhoan2";
            this.statCardThongKeTaiKhoan2.PanelBackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.statCardThongKeTaiKhoan2.PictureBoxBackgroundColor = System.Drawing.Color.Transparent;
            this.statCardThongKeTaiKhoan2.Size = new System.Drawing.Size(262, 176);
            this.statCardThongKeTaiKhoan2.TabIndex = 8;
            this.statCardThongKeTaiKhoan2.TitleGhiChu = "Note";
            this.statCardThongKeTaiKhoan2.TitleLietKe = "Value";
            // 
            // statCardThongKeTaiKhoan1
            // 
            this.statCardThongKeTaiKhoan1.BackColor = System.Drawing.Color.Transparent;
            this.statCardThongKeTaiKhoan1.Icon = null;
            this.statCardThongKeTaiKhoan1.IconFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.statCardThongKeTaiKhoan1.Location = new System.Drawing.Point(32, 90);
            this.statCardThongKeTaiKhoan1.Name = "statCardThongKeTaiKhoan1";
            this.statCardThongKeTaiKhoan1.PanelBackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.statCardThongKeTaiKhoan1.PictureBoxBackgroundColor = System.Drawing.Color.Transparent;
            this.statCardThongKeTaiKhoan1.Size = new System.Drawing.Size(262, 176);
            this.statCardThongKeTaiKhoan1.TabIndex = 8;
            this.statCardThongKeTaiKhoan1.TitleGhiChu = "Note";
            this.statCardThongKeTaiKhoan1.TitleLietKe = "Value";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.guna2CustomGradientPanel1);
            this.guna2Panel1.Location = new System.Drawing.Point(607, 777);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(153, 37);
            this.guna2Panel1.TabIndex = 9;
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(124, 8);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(11, 8);
            this.guna2CustomGradientPanel1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Location = new System.Drawing.Point(81, 757);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Tìm kiếm tài khoản...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(236, 36);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblTrangHienTai
            // 
            this.lblTrangHienTai.AutoSize = true;
            this.lblTrangHienTai.BackColor = System.Drawing.Color.Transparent;
            this.lblTrangHienTai.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangHienTai.Location = new System.Drawing.Point(469, 772);
            this.lblTrangHienTai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTrangHienTai.Name = "lblTrangHienTai";
            this.lblTrangHienTai.Size = new System.Drawing.Size(43, 20);
            this.lblTrangHienTai.TabIndex = 62;
            this.lblTrangHienTai.Text = "1/10";
            // 
            // btnTrangTruoc
            // 
            this.btnTrangTruoc.BorderRadius = 7;
            this.btnTrangTruoc.BorderThickness = 1;
            this.btnTrangTruoc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTrangTruoc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTrangTruoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTrangTruoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTrangTruoc.FillColor = System.Drawing.Color.White;
            this.btnTrangTruoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrangTruoc.ForeColor = System.Drawing.Color.Black;
            this.btnTrangTruoc.ImageSize = new System.Drawing.Size(15, 15);
            this.btnTrangTruoc.Location = new System.Drawing.Point(422, 756);
            this.btnTrangTruoc.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrangTruoc.Name = "btnTrangTruoc";
            this.btnTrangTruoc.Size = new System.Drawing.Size(43, 35);
            this.btnTrangTruoc.TabIndex = 61;
            this.btnTrangTruoc.Text = "◀";
            this.btnTrangTruoc.Click += new System.EventHandler(this.btnTrangTruoc_Click);
            // 
            // btnTrangSau
            // 
            this.btnTrangSau.BorderRadius = 7;
            this.btnTrangSau.BorderThickness = 1;
            this.btnTrangSau.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTrangSau.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTrangSau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTrangSau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTrangSau.FillColor = System.Drawing.Color.White;
            this.btnTrangSau.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrangSau.ForeColor = System.Drawing.Color.Black;
            this.btnTrangSau.ImageSize = new System.Drawing.Size(15, 15);
            this.btnTrangSau.Location = new System.Drawing.Point(528, 757);
            this.btnTrangSau.Margin = new System.Windows.Forms.Padding(2);
            this.btnTrangSau.Name = "btnTrangSau";
            this.btnTrangSau.Size = new System.Drawing.Size(43, 35);
            this.btnTrangSau.TabIndex = 60;
            this.btnTrangSau.Text = "▶";
            this.btnTrangSau.Click += new System.EventHandler(this.btnTrangSau_Click);
            // 
            // FrmTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblTrangHienTai);
            this.Controls.Add(this.btnTrangTruoc);
            this.Controls.Add(this.btnTrangSau);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.statCardThongKeTaiKhoan4);
            this.Controls.Add(this.statCardThongKeTaiKhoan3);
            this.Controls.Add(this.statCardThongKeTaiKhoan2);
            this.Controls.Add(this.statCardThongKeTaiKhoan1);
            this.Controls.Add(this.tbTaiKhoan);
            this.Controls.Add(this.btnVaiTro);
            this.Controls.Add(this.btnAddAcc);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(1168, 768);
            this.Name = "FrmTaiKhoan";
            this.Size = new System.Drawing.Size(1134, 768);
            this.Load += new System.EventHandler(this.TaiKhoan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbTaiKhoan)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnAddAcc;
        private Guna.UI2.WinForms.Guna2Button btnVaiTro;
        private Guna.UI2.WinForms.Guna2DataGridView tbTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn vaiTro;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn thaoTac;
        private TaiKhoan.statCardThongKeTaiKhoan statCardThongKeTaiKhoan1;
        private TaiKhoan.statCardThongKeTaiKhoan statCardThongKeTaiKhoan2;
        private TaiKhoan.statCardThongKeTaiKhoan statCardThongKeTaiKhoan3;
        private TaiKhoan.statCardThongKeTaiKhoan statCardThongKeTaiKhoan4;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private System.Windows.Forms.Label lblTrangHienTai;
        private Guna.UI2.WinForms.Guna2Button btnTrangTruoc;
        private Guna.UI2.WinForms.Guna2Button btnTrangSau;
    }
}
