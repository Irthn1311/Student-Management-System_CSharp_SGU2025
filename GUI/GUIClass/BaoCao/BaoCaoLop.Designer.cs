namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class BaoCaoLop
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbHocSinh = new Guna.UI2.WinForms.Guna2DataGridView();
            this.maHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDTHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDanhSach = new System.Windows.Forms.TabPage();
            this.tabPageThongKe = new System.Windows.Forms.TabPage();
            this.pnlCharts = new System.Windows.Forms.Panel();
            this.chartBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnXuatPDF = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHocSinh)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageDanhSach.SuspendLayout();
            this.tabPageThongKe.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 57);
            this.panel1.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 16);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(290, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Danh sách học sinh của lớp 10A";
            // 
            // tbHocSinh
            // 
            this.tbHocSinh.AllowUserToAddRows = false;
            this.tbHocSinh.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.tbHocSinh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbHocSinh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.tbHocSinh.ColumnHeadersHeight = 40;
            this.tbHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tbHocSinh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.maHS,
            this.hoTen,
            this.ngaySinh,
            this.gioiTinh,
            this.SDTHS,
            this.email,
            this.trangThai});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tbHocSinh.DefaultCellStyle = dataGridViewCellStyle6;
            this.tbHocSinh.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tbHocSinh.Location = new System.Drawing.Point(6, 6);
            this.tbHocSinh.Name = "tbHocSinh";
            this.tbHocSinh.ReadOnly = true;
            this.tbHocSinh.RowHeadersVisible = false;
            this.tbHocSinh.RowTemplate.Height = 35;
            this.tbHocSinh.Size = new System.Drawing.Size(950, 450);
            this.tbHocSinh.TabIndex = 5;
            this.tbHocSinh.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tbHocSinh.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tbHocSinh.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tbHocSinh.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tbHocSinh.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tbHocSinh.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tbHocSinh.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tbHocSinh.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tbHocSinh.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tbHocSinh.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tbHocSinh.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbHocSinh.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tbHocSinh.ThemeStyle.HeaderStyle.Height = 40;
            this.tbHocSinh.ThemeStyle.ReadOnly = true;
            this.tbHocSinh.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tbHocSinh.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tbHocSinh.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbHocSinh.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tbHocSinh.ThemeStyle.RowsStyle.Height = 35;
            this.tbHocSinh.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tbHocSinh.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tbHocSinh.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tbHocSinh_CellContentClick);
            this.tbHocSinh.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.tbHocSinh_CellFormatting);
            // 
            // maHS
            // 
            this.maHS.HeaderText = "Mã HS";
            this.maHS.Name = "maHS";
            this.maHS.ReadOnly = true;
            // 
            // hoTen
            // 
            this.hoTen.HeaderText = "Họ tên";
            this.hoTen.Name = "hoTen";
            this.hoTen.ReadOnly = true;
            // 
            // ngaySinh
            // 
            this.ngaySinh.HeaderText = "Ngày sinh";
            this.ngaySinh.Name = "ngaySinh";
            this.ngaySinh.ReadOnly = true;
            // 
            // gioiTinh
            // 
            this.gioiTinh.HeaderText = "Giới tính";
            this.gioiTinh.Name = "gioiTinh";
            this.gioiTinh.ReadOnly = true;
            // 
            // SDTHS
            // 
            this.SDTHS.HeaderText = "SDTHS";
            this.SDTHS.Name = "SDTHS";
            this.SDTHS.ReadOnly = true;
            // 
            // email
            // 
            this.email.HeaderText = "Email";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            // 
            // trangThai
            // 
            this.trangThai.HeaderText = "Trạng thái";
            this.trangThai.Name = "trangThai";
            this.trangThai.ReadOnly = true;
            // 
            // btnHuy
            // 
            this.btnHuy.BorderColor = System.Drawing.Color.Red;
            this.btnHuy.BorderRadius = 7;
            this.btnHuy.BorderThickness = 2;
            this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuy.FillColor = System.Drawing.Color.White;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.ForeColor = System.Drawing.Color.Red;
            this.btnHuy.ImageSize = new System.Drawing.Size(15, 15);
            this.btnHuy.Location = new System.Drawing.Point(823, 557);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(105, 39);
            this.btnHuy.TabIndex = 60;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageDanhSach);
            this.tabControl.Controls.Add(this.tabPageThongKe);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(2, 61);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(962, 490);
            this.tabControl.TabIndex = 6;
            // 
            // tabPageDanhSach
            // 
            this.tabPageDanhSach.Controls.Add(this.tbHocSinh);
            this.tabPageDanhSach.Location = new System.Drawing.Point(4, 24);
            this.tabPageDanhSach.Name = "tabPageDanhSach";
            this.tabPageDanhSach.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDanhSach.Size = new System.Drawing.Size(954, 462);
            this.tabPageDanhSach.TabIndex = 0;
            this.tabPageDanhSach.Text = "Danh sách học sinh";
            this.tabPageDanhSach.UseVisualStyleBackColor = true;
            // 
            // tabPageThongKe
            // 
            this.tabPageThongKe.Controls.Add(this.pnlCharts);
            this.tabPageThongKe.Location = new System.Drawing.Point(4, 24);
            this.tabPageThongKe.Name = "tabPageThongKe";
            this.tabPageThongKe.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageThongKe.Size = new System.Drawing.Size(954, 462);
            this.tabPageThongKe.TabIndex = 1;
            this.tabPageThongKe.Text = "Thống kê";
            this.tabPageThongKe.UseVisualStyleBackColor = true;
            // 
            // pnlCharts
            // 
            this.pnlCharts.Controls.Add(this.chartBar);
            this.pnlCharts.Controls.Add(this.chartPie);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCharts.Location = new System.Drawing.Point(3, 3);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Size = new System.Drawing.Size(948, 456);
            this.pnlCharts.TabIndex = 0;
            // 
            // chartBar
            // 
            this.chartBar.BorderlineColor = System.Drawing.Color.Gray;
            this.chartBar.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartBar.Location = new System.Drawing.Point(6, 6);
            this.chartBar.Name = "chartBar";
            this.chartBar.Size = new System.Drawing.Size(460, 440);
            this.chartBar.TabIndex = 0;
            this.chartBar.Text = "chartBar";
            // 
            // chartPie
            // 
            this.chartPie.BorderlineColor = System.Drawing.Color.Gray;
            this.chartPie.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartPie.Location = new System.Drawing.Point(482, 6);
            this.chartPie.Name = "chartPie";
            this.chartPie.Size = new System.Drawing.Size(460, 440);
            this.chartPie.TabIndex = 1;
            this.chartPie.Text = "chartPie";
            // 
            // btnXuatPDF
            // 
            this.btnXuatPDF.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnXuatPDF.BorderRadius = 7;
            this.btnXuatPDF.BorderThickness = 2;
            this.btnXuatPDF.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatPDF.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatPDF.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXuatPDF.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXuatPDF.FillColor = System.Drawing.Color.White;
            this.btnXuatPDF.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatPDF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnXuatPDF.ImageSize = new System.Drawing.Size(15, 15);
            this.btnXuatPDF.Location = new System.Drawing.Point(690, 557);
            this.btnXuatPDF.Margin = new System.Windows.Forms.Padding(2);
            this.btnXuatPDF.Name = "btnXuatPDF";
            this.btnXuatPDF.Size = new System.Drawing.Size(120, 39);
            this.btnXuatPDF.TabIndex = 61;
            this.btnXuatPDF.Text = "Xuất PDF";
            this.btnXuatPDF.Click += new System.EventHandler(this.btnXuatPDF_Click);
            // 
            // BaoCaoLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(983, 608);
            this.Controls.Add(this.btnXuatPDF);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Name = "BaoCaoLop";
            this.Load += new System.EventHandler(this.BaoCaoLop_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbHocSinh)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageDanhSach.ResumeLayout(false);
            this.tabPageThongKe.ResumeLayout(false);
            this.pnlCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2DataGridView tbHocSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn maHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn gioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDTHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn trangThai;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDanhSach;
        private System.Windows.Forms.TabPage tabPageThongKe;
        private System.Windows.Forms.Panel pnlCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private Guna.UI2.WinForms.Guna2Button btnXuatPDF;
    }
}
