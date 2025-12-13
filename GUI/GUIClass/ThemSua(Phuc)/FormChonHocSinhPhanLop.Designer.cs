namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FormChonHocSinhPhanLop
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblThongTinLop = new System.Windows.Forms.Label();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.dgvHocSinh = new System.Windows.Forms.DataGridView();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMaHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSdt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTongSo = new System.Windows.Forms.Label();
            this.lblDaChon = new System.Windows.Forms.Label();
            this.btnChonTatCa = new Guna.UI2.WinForms.Guna2Button();
            this.btnPhanLop = new Guna.UI2.WinForms.Guna2Button();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.lblHeader);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(900, 60);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(20, 18);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(258, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Chọn học sinh phân vào lớp";
            // 
            // lblThongTinLop
            // 
            this.lblThongTinLop.AutoSize = true;
            this.lblThongTinLop.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblThongTinLop.Location = new System.Drawing.Point(20, 80);
            this.lblThongTinLop.Name = "lblThongTinLop";
            this.lblThongTinLop.Size = new System.Drawing.Size(112, 21);
            this.lblThongTinLop.TabIndex = 1;
            this.lblThongTinLop.Text = "Thông tin lớp:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderColor = System.Drawing.Color.Black;
            this.txtTimKiem.BorderRadius = 5;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(120, 120);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm kiếm theo mã, tên, SĐT...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 36);
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.Location = new System.Drawing.Point(20, 128);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(70, 19);
            this.lblTimKiem.TabIndex = 3;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // dgvHocSinh
            // 
            this.dgvHocSinh.AllowUserToAddRows = false;
            this.dgvHocSinh.AllowUserToDeleteRows = false;
            this.dgvHocSinh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHocSinh.BackgroundColor = System.Drawing.Color.White;
            this.dgvHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHocSinh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChon,
            this.colMaHS,
            this.colHoTen,
            this.colGioiTinh,
            this.colNgaySinh,
            this.colSdt});
            this.dgvHocSinh.Location = new System.Drawing.Point(20, 170);
            this.dgvHocSinh.Name = "dgvHocSinh";
            this.dgvHocSinh.RowHeadersWidth = 51;
            this.dgvHocSinh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHocSinh.Size = new System.Drawing.Size(860, 350);
            this.dgvHocSinh.TabIndex = 4;
            this.dgvHocSinh.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHocSinh_CellContentClick);
            // 
            // colChon
            // 
            this.colChon.HeaderText = "Chọn";
            this.colChon.MinimumWidth = 50;
            this.colChon.Name = "colChon";
            // 
            // colMaHS
            // 
            this.colMaHS.HeaderText = "Mã HS";
            this.colMaHS.MinimumWidth = 80;
            this.colMaHS.Name = "colMaHS";
            this.colMaHS.ReadOnly = true;
            // 
            // colHoTen
            // 
            this.colHoTen.HeaderText = "Họ tên";
            this.colHoTen.MinimumWidth = 150;
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.HeaderText = "Giới tính";
            this.colGioiTinh.MinimumWidth = 80;
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.ReadOnly = true;
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.HeaderText = "Ngày sinh";
            this.colNgaySinh.MinimumWidth = 100;
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.ReadOnly = true;
            // 
            // colSdt
            // 
            this.colSdt.HeaderText = "SĐT";
            this.colSdt.MinimumWidth = 120;
            this.colSdt.Name = "colSdt";
            this.colSdt.ReadOnly = true;
            // 
            // lblTongSo
            // 
            this.lblTongSo.AutoSize = true;
            this.lblTongSo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongSo.Location = new System.Drawing.Point(20, 540);
            this.lblTongSo.Name = "lblTongSo";
            this.lblTongSo.Size = new System.Drawing.Size(94, 19);
            this.lblTongSo.TabIndex = 5;
            this.lblTongSo.Text = "Tổng số: 0 HS";
            // 
            // lblDaChon
            // 
            this.lblDaChon.AutoSize = true;
            this.lblDaChon.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblDaChon.Location = new System.Drawing.Point(200, 540);
            this.lblDaChon.Name = "lblDaChon";
            this.lblDaChon.Size = new System.Drawing.Size(90, 19);
            this.lblDaChon.TabIndex = 6;
            this.lblDaChon.Text = "Đã chọn: 0/0";
            // 
            // btnChonTatCa
            // 
            this.btnChonTatCa.BorderRadius = 5;
            this.btnChonTatCa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChonTatCa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChonTatCa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChonTatCa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChonTatCa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnChonTatCa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnChonTatCa.ForeColor = System.Drawing.Color.White;
            this.btnChonTatCa.Location = new System.Drawing.Point(450, 120);
            this.btnChonTatCa.Name = "btnChonTatCa";
            this.btnChonTatCa.Size = new System.Drawing.Size(120, 36);
            this.btnChonTatCa.TabIndex = 7;
            this.btnChonTatCa.Text = "Chọn tất cả";
            this.btnChonTatCa.Click += new System.EventHandler(this.btnChonTatCa_Click);
            // 
            // btnPhanLop
            // 
            this.btnPhanLop.BorderRadius = 5;
            this.btnPhanLop.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPhanLop.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPhanLop.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPhanLop.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPhanLop.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnPhanLop.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnPhanLop.ForeColor = System.Drawing.Color.White;
            this.btnPhanLop.Location = new System.Drawing.Point(600, 535);
            this.btnPhanLop.Name = "btnPhanLop";
            this.btnPhanLop.Size = new System.Drawing.Size(140, 40);
            this.btnPhanLop.TabIndex = 8;
            this.btnPhanLop.Text = "Phân vào lớp";
            this.btnPhanLop.Click += new System.EventHandler(this.btnPhanLop_Click);
            // 
            // btnDong
            // 
            this.btnDong.BorderRadius = 5;
            this.btnDong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(750, 535);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(130, 40);
            this.btnDong.TabIndex = 9;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FormChonHocSinhPhanLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnPhanLop);
            this.Controls.Add(this.btnChonTatCa);
            this.Controls.Add(this.lblDaChon);
            this.Controls.Add(this.lblTongSo);
            this.Controls.Add(this.dgvHocSinh);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblThongTinLop);
            this.Controls.Add(this.guna2Panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChonHocSinhPhanLop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn học sinh phân vào lớp";
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblThongTinLop;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.DataGridView dgvHocSinh;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSdt;
        private System.Windows.Forms.Label lblTongSo;
        private System.Windows.Forms.Label lblDaChon;
        private Guna.UI2.WinForms.Guna2Button btnChonTatCa;
        private Guna.UI2.WinForms.Guna2Button btnPhanLop;
        private Guna.UI2.WinForms.Guna2Button btnDong;
    }
}

