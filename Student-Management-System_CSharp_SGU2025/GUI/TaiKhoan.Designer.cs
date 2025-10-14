namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class TaiKhoan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnAddAcc = new Guna.UI2.WinForms.Guna2Button();
            this.btnVaiTro = new Guna.UI2.WinForms.Guna2Button();
            this.tbTaiKhoan = new Guna.UI2.WinForms.Guna2DataGridView();
            this.tenTaiKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vaiTro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thaoTac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thongKeTK4 = new Student_Management_System_CSharp_SGU2025.GUI.ThongKeCard();
            this.thongKeTK3 = new Student_Management_System_CSharp_SGU2025.GUI.ThongKeCard();
            this.thongKeTK2 = new Student_Management_System_CSharp_SGU2025.GUI.ThongKeCard();
            this.thongKeTK1 = new Student_Management_System_CSharp_SGU2025.GUI.ThongKeCard();
            this.shield3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lock1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.shield2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.shield1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbTaiKhoan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shield3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lock1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shield2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shield1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(4, 4);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1664, 69);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnAddAcc
            // 
            this.btnAddAcc.BorderRadius = 8;
            this.btnAddAcc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddAcc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddAcc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddAcc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddAcc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(136)))), ((int)(((byte)(225)))));
            this.btnAddAcc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAcc.ForeColor = System.Drawing.Color.White;
            this.btnAddAcc.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAddAcc.Location = new System.Drawing.Point(55, 106);
            this.btnAddAcc.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddAcc.Name = "btnAddAcc";
            this.btnAddAcc.Size = new System.Drawing.Size(212, 42);
            this.btnAddAcc.TabIndex = 1;
            this.btnAddAcc.Text = "➕   Tạo tài khoản";
            // 
            // btnVaiTro
            // 
            this.btnVaiTro.BorderRadius = 8;
            this.btnVaiTro.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnVaiTro.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnVaiTro.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnVaiTro.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnVaiTro.FillColor = System.Drawing.Color.White;
            this.btnVaiTro.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVaiTro.ForeColor = System.Drawing.Color.Black;
            this.btnVaiTro.Location = new System.Drawing.Point(975, 106);
            this.btnVaiTro.Margin = new System.Windows.Forms.Padding(4);
            this.btnVaiTro.Name = "btnVaiTro";
            this.btnVaiTro.Size = new System.Drawing.Size(216, 42);
            this.btnVaiTro.TabIndex = 2;
            this.btnVaiTro.Text = "Tất cả vai trò";
            this.btnVaiTro.Click += new System.EventHandler(this.btnVaiTro_Click);
            // 
            // tbTaiKhoan
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tbTaiKhoan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbTaiKhoan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tbTaiKhoan.ColumnHeadersHeight = 18;
            this.tbTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tbTaiKhoan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tenTaiKhoan,
            this.vaiTro,
            this.trangThai,
            this.lastLogin,
            this.thaoTac});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tbTaiKhoan.DefaultCellStyle = dataGridViewCellStyle3;
            this.tbTaiKhoan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tbTaiKhoan.Location = new System.Drawing.Point(55, 378);
            this.tbTaiKhoan.Name = "tbTaiKhoan";
            this.tbTaiKhoan.RowHeadersVisible = false;
            this.tbTaiKhoan.RowHeadersWidth = 51;
            this.tbTaiKhoan.RowTemplate.Height = 24;
            this.tbTaiKhoan.Size = new System.Drawing.Size(1136, 420);
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
            // thongKeTK4
            // 
            this.thongKeTK4.BackColor = System.Drawing.Color.White;
            this.thongKeTK4.Font2 = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeTK4.Font3 = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeTK4.ForeColor2 = System.Drawing.SystemColors.ControlText;
            this.thongKeTK4.ForeColor3 = System.Drawing.SystemColors.ControlText;
            this.thongKeTK4.Location = new System.Drawing.Point(959, 208);
            this.thongKeTK4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.thongKeTK4.Name = "thongKeTK4";
            this.thongKeTK4.Size = new System.Drawing.Size(264, 142);
            this.thongKeTK4.TabIndex = 6;
            this.thongKeTK4.TieuDe1 = "Tiêu Đề";
            this.thongKeTK4.TieuDe2 = "Number";
            this.thongKeTK4.TieuDe3 = "guna2HtmlLabel1";
            this.thongKeTK4.Load += new System.EventHandler(this.thongKeTK4_Load);
            // 
            // thongKeTK3
            // 
            this.thongKeTK3.BackColor = System.Drawing.Color.Silver;
            this.thongKeTK3.Font2 = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeTK3.Font3 = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeTK3.ForeColor2 = System.Drawing.SystemColors.ControlText;
            this.thongKeTK3.ForeColor3 = System.Drawing.SystemColors.ControlText;
            this.thongKeTK3.Location = new System.Drawing.Point(650, 208);
            this.thongKeTK3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.thongKeTK3.Name = "thongKeTK3";
            this.thongKeTK3.Size = new System.Drawing.Size(264, 142);
            this.thongKeTK3.TabIndex = 5;
            this.thongKeTK3.TieuDe1 = "Tiêu Đề";
            this.thongKeTK3.TieuDe2 = "Number";
            this.thongKeTK3.TieuDe3 = "guna2HtmlLabel1";
            this.thongKeTK3.Load += new System.EventHandler(this.thongKeTK3_Load);
            // 
            // thongKeTK2
            // 
            this.thongKeTK2.BackColor = System.Drawing.Color.White;
            this.thongKeTK2.Font2 = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeTK2.Font3 = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeTK2.ForeColor2 = System.Drawing.SystemColors.ControlText;
            this.thongKeTK2.ForeColor3 = System.Drawing.SystemColors.ControlText;
            this.thongKeTK2.Location = new System.Drawing.Point(337, 208);
            this.thongKeTK2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.thongKeTK2.Name = "thongKeTK2";
            this.thongKeTK2.Size = new System.Drawing.Size(264, 142);
            this.thongKeTK2.TabIndex = 4;
            this.thongKeTK2.TieuDe1 = "Tiêu Đề";
            this.thongKeTK2.TieuDe2 = "Number";
            this.thongKeTK2.TieuDe3 = "guna2HtmlLabel1";
            this.thongKeTK2.Load += new System.EventHandler(this.thongKeTK2_Load);
            // 
            // thongKeTK1
            // 
            this.thongKeTK1.BackColor = System.Drawing.Color.White;
            this.thongKeTK1.Font2 = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeTK1.Font3 = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thongKeTK1.ForeColor2 = System.Drawing.SystemColors.ControlText;
            this.thongKeTK1.ForeColor3 = System.Drawing.SystemColors.ControlText;
            this.thongKeTK1.Location = new System.Drawing.Point(31, 208);
            this.thongKeTK1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.thongKeTK1.Name = "thongKeTK1";
            this.thongKeTK1.Size = new System.Drawing.Size(264, 142);
            this.thongKeTK1.TabIndex = 3;
            this.thongKeTK1.TieuDe1 = "Tiêu Đề";
            this.thongKeTK1.TieuDe2 = "Number";
            this.thongKeTK1.TieuDe3 = "guna2HtmlLabel1";
            this.thongKeTK1.Load += new System.EventHandler(this.thongKeTK1_Load);
            // 
            // shield3
            // 
            this.shield3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(122)))), ((int)(((byte)(78)))));
            this.shield3.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.shield;
            this.shield3.ImageRotate = 0F;
            this.shield3.Location = new System.Drawing.Point(983, 219);
            this.shield3.Name = "shield3";
            this.shield3.Size = new System.Drawing.Size(45, 37);
            this.shield3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.shield3.TabIndex = 11;
            this.shield3.TabStop = false;
            // 
            // lock1
            // 
            this.lock1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(99)))), ((int)(((byte)(98)))));
            this.lock1.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources._lock;
            this.lock1.ImageRotate = 0F;
            this.lock1.Location = new System.Drawing.Point(672, 219);
            this.lock1.Name = "lock1";
            this.lock1.Size = new System.Drawing.Size(45, 37);
            this.lock1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.lock1.TabIndex = 10;
            this.lock1.TabStop = false;
            // 
            // shield2
            // 
            this.shield2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(161)))), ((int)(((byte)(112)))));
            this.shield2.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.shield;
            this.shield2.ImageRotate = 0F;
            this.shield2.Location = new System.Drawing.Point(362, 219);
            this.shield2.Name = "shield2";
            this.shield2.Size = new System.Drawing.Size(44, 37);
            this.shield2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.shield2.TabIndex = 9;
            this.shield2.TabStop = false;
            // 
            // shield1
            // 
            this.shield1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(129)))), ((int)(((byte)(183)))));
            this.shield1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(146)))), ((int)(((byte)(244)))));
            this.shield1.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.shield;
            this.shield1.ImageRotate = 0F;
            this.shield1.Location = new System.Drawing.Point(55, 219);
            this.shield1.Name = "shield1";
            this.shield1.Size = new System.Drawing.Size(44, 37);
            this.shield1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.shield1.TabIndex = 8;
            this.shield1.TabStop = false;
            this.shield1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // TaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.shield3);
            this.Controls.Add(this.lock1);
            this.Controls.Add(this.shield2);
            this.Controls.Add(this.shield1);
            this.Controls.Add(this.tbTaiKhoan);
            this.Controls.Add(this.thongKeTK4);
            this.Controls.Add(this.thongKeTK3);
            this.Controls.Add(this.thongKeTK2);
            this.Controls.Add(this.thongKeTK1);
            this.Controls.Add(this.btnVaiTro);
            this.Controls.Add(this.btnAddAcc);
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TaiKhoan";
            this.Size = new System.Drawing.Size(1248, 844);
            this.Load += new System.EventHandler(this.TaiKhoan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbTaiKhoan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shield3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lock1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shield2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shield1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnAddAcc;
        private Guna.UI2.WinForms.Guna2Button btnVaiTro;
        private ThongKeCard thongKeTK1;
        private ThongKeCard thongKeTK2;
        private ThongKeCard thongKeTK3;
        private ThongKeCard thongKeTK4;
        private Guna.UI2.WinForms.Guna2DataGridView tbTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn vaiTro;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn thaoTac;
        private Guna.UI2.WinForms.Guna2PictureBox shield1;
        private Guna.UI2.WinForms.Guna2PictureBox shield2;
        private Guna.UI2.WinForms.Guna2PictureBox lock1;
        private Guna.UI2.WinForms.Guna2PictureBox shield3;
    }
}
