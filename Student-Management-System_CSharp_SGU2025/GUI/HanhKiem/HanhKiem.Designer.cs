namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class HanhKiem
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
            this.headerHanhKiem = new Student_Management_System_CSharp_SGU2025.GUI.HeaderQuanLiHocSinh();
            this.cbHocKyNamHoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnLuuHanhKiem = new Guna.UI2.WinForms.Guna2Button();
            this.paneltrong = new System.Windows.Forms.Panel();
            this.tableHanhKiem = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statCarHanhKiemTot = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardHanhKiemKha = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardHanhKiemTrungBinh = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardHanhKiemYeu = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardChuaDanhGiaHanhKiem = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            ((System.ComponentModel.ISupportInitialize)(this.tableHanhKiem)).BeginInit();
            this.SuspendLayout();
            // 
            // headerHanhKiem
            // 
            this.headerHanhKiem.BackColor = System.Drawing.Color.White;
            this.headerHanhKiem.Location = new System.Drawing.Point(0, 0);
            this.headerHanhKiem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.headerHanhKiem.Name = "headerHanhKiem";
            this.headerHanhKiem.Size = new System.Drawing.Size(872, 0);
            this.headerHanhKiem.TabIndex = 0;
            this.headerHanhKiem.Visible = false;
            this.headerHanhKiem.Load += new System.EventHandler(this.headerHanhKiem_Load);
            // 
            // cbHocKyNamHoc
            // 
            this.cbHocKyNamHoc.BackColor = System.Drawing.Color.Transparent;
            this.cbHocKyNamHoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHocKyNamHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocKyNamHoc.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocKyNamHoc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocKyNamHoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHocKyNamHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbHocKyNamHoc.ItemHeight = 30;
            this.cbHocKyNamHoc.Items.AddRange(new object[] {
            "Tất cả Học kì",
            "Học Kỳ I - 2023 - 2024",
            "Học Kỳ II - 2023 - 2024",
            "Học Kỳ I - 2024 - 2025",
            "Học Kỳ II - 2024 - 2025"});
            this.cbHocKyNamHoc.Location = new System.Drawing.Point(28, 15);
            this.cbHocKyNamHoc.Margin = new System.Windows.Forms.Padding(2);
            this.cbHocKyNamHoc.Name = "cbHocKyNamHoc";
            this.cbHocKyNamHoc.Size = new System.Drawing.Size(230, 36);
            this.cbHocKyNamHoc.StartIndex = 0;
            this.cbHocKyNamHoc.TabIndex = 15;
            this.cbHocKyNamHoc.SelectedIndexChanged += new System.EventHandler(this.cbHocKyNamHoc_SelectedIndexChanged);
            // 
            // cbLop
            // 
            this.cbLop.BackColor = System.Drawing.Color.Transparent;
            this.cbLop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLop.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLop.ItemHeight = 30;
            this.cbLop.Items.AddRange(new object[] {
            "Tất cả lớp",
            "Lớp 6A1",
            "Lớp 7A2"});
            this.cbLop.Location = new System.Drawing.Point(293, 15);
            this.cbLop.Margin = new System.Windows.Forms.Padding(2);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(161, 36);
            this.cbLop.StartIndex = 0;
            this.cbLop.TabIndex = 16;
            this.cbLop.SelectedIndexChanged += new System.EventHandler(this.cbLop_SelectedIndexChanged);
            // 
            // btnLuuHanhKiem
            // 
            this.btnLuuHanhKiem.BackColor = System.Drawing.Color.Transparent;
            this.btnLuuHanhKiem.BorderRadius = 5;
            this.btnLuuHanhKiem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuuHanhKiem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuuHanhKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuuHanhKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuuHanhKiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnLuuHanhKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLuuHanhKiem.ForeColor = System.Drawing.Color.White;
            this.btnLuuHanhKiem.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.diskette;
            this.btnLuuHanhKiem.Location = new System.Drawing.Point(950, 826);
            this.btnLuuHanhKiem.Margin = new System.Windows.Forms.Padding(2);
            this.btnLuuHanhKiem.Name = "btnLuuHanhKiem";
            this.btnLuuHanhKiem.Size = new System.Drawing.Size(186, 37);
            this.btnLuuHanhKiem.TabIndex = 27;
            this.btnLuuHanhKiem.Text = "Lưu hạnh kiểm";
            this.btnLuuHanhKiem.Click += new System.EventHandler(this.btnLuuHanhKiem_Click);
            // 
            // paneltrong
            // 
            this.paneltrong.Location = new System.Drawing.Point(248, 817);
            this.paneltrong.Name = "paneltrong";
            this.paneltrong.Size = new System.Drawing.Size(200, 64);
            this.paneltrong.TabIndex = 28;
            // 
            // tableHanhKiem
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tableHanhKiem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableHanhKiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tableHanhKiem.ColumnHeadersHeight = 18;
            this.tableHanhKiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableHanhKiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableHanhKiem.DefaultCellStyle = dataGridViewCellStyle3;
            this.tableHanhKiem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableHanhKiem.Location = new System.Drawing.Point(28, 372);
            this.tableHanhKiem.Margin = new System.Windows.Forms.Padding(2);
            this.tableHanhKiem.Name = "tableHanhKiem";
            this.tableHanhKiem.RowHeadersVisible = false;
            this.tableHanhKiem.RowHeadersWidth = 51;
            this.tableHanhKiem.RowTemplate.Height = 24;
            this.tableHanhKiem.Size = new System.Drawing.Size(1119, 430);
            this.tableHanhKiem.TabIndex = 29;
            this.tableHanhKiem.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tableHanhKiem.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tableHanhKiem.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tableHanhKiem.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tableHanhKiem.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tableHanhKiem.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tableHanhKiem.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableHanhKiem.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tableHanhKiem.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tableHanhKiem.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableHanhKiem.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tableHanhKiem.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableHanhKiem.ThemeStyle.HeaderStyle.Height = 18;
            this.tableHanhKiem.ThemeStyle.ReadOnly = false;
            this.tableHanhKiem.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tableHanhKiem.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tableHanhKiem.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableHanhKiem.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableHanhKiem.ThemeStyle.RowsStyle.Height = 24;
            this.tableHanhKiem.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableHanhKiem.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Mã Hạnh Kiểm";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Họ và Tên";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Lớp";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Học Kì";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Xếp Loại";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Nhận Xét";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            // 
            // statCarHanhKiemTot
            // 
            this.statCarHanhKiemTot.BackColor = System.Drawing.Color.Transparent;
            this.statCarHanhKiemTot.Location = new System.Drawing.Point(28, 68);
            this.statCarHanhKiemTot.Margin = new System.Windows.Forms.Padding(2);
            this.statCarHanhKiemTot.Name = "statCarHanhKiemTot";
            this.statCarHanhKiemTot.Size = new System.Drawing.Size(262, 136);
            this.statCarHanhKiemTot.TabIndex = 30;
            // 
            // statCardHanhKiemKha
            // 
            this.statCardHanhKiemKha.BackColor = System.Drawing.Color.Transparent;
            this.statCardHanhKiemKha.Location = new System.Drawing.Point(451, 68);
            this.statCardHanhKiemKha.Margin = new System.Windows.Forms.Padding(2);
            this.statCardHanhKiemKha.Name = "statCardHanhKiemKha";
            this.statCardHanhKiemKha.Size = new System.Drawing.Size(262, 136);
            this.statCardHanhKiemKha.TabIndex = 30;
            // 
            // statCardHanhKiemTrungBinh
            // 
            this.statCardHanhKiemTrungBinh.BackColor = System.Drawing.Color.Transparent;
            this.statCardHanhKiemTrungBinh.Location = new System.Drawing.Point(885, 68);
            this.statCardHanhKiemTrungBinh.Margin = new System.Windows.Forms.Padding(2);
            this.statCardHanhKiemTrungBinh.Name = "statCardHanhKiemTrungBinh";
            this.statCardHanhKiemTrungBinh.Size = new System.Drawing.Size(262, 136);
            this.statCardHanhKiemTrungBinh.TabIndex = 30;
            // 
            // statCardHanhKiemYeu
            // 
            this.statCardHanhKiemYeu.BackColor = System.Drawing.Color.Transparent;
            this.statCardHanhKiemYeu.Location = new System.Drawing.Point(235, 223);
            this.statCardHanhKiemYeu.Margin = new System.Windows.Forms.Padding(2);
            this.statCardHanhKiemYeu.Name = "statCardHanhKiemYeu";
            this.statCardHanhKiemYeu.Size = new System.Drawing.Size(262, 136);
            this.statCardHanhKiemYeu.TabIndex = 30;
            // 
            // statCardChuaDanhGiaHanhKiem
            // 
            this.statCardChuaDanhGiaHanhKiem.BackColor = System.Drawing.Color.Transparent;
            this.statCardChuaDanhGiaHanhKiem.Location = new System.Drawing.Point(666, 223);
            this.statCardChuaDanhGiaHanhKiem.Margin = new System.Windows.Forms.Padding(2);
            this.statCardChuaDanhGiaHanhKiem.Name = "statCardChuaDanhGiaHanhKiem";
            this.statCardChuaDanhGiaHanhKiem.Size = new System.Drawing.Size(262, 136);
            this.statCardChuaDanhGiaHanhKiem.TabIndex = 30;
            // 
            // HanhKiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.statCardHanhKiemTrungBinh);
            this.Controls.Add(this.statCardChuaDanhGiaHanhKiem);
            this.Controls.Add(this.statCardHanhKiemKha);
            this.Controls.Add(this.statCardHanhKiemYeu);
            this.Controls.Add(this.statCarHanhKiemTot);
            this.Controls.Add(this.paneltrong);
            this.Controls.Add(this.tableHanhKiem);
            this.Controls.Add(this.btnLuuHanhKiem);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.cbHocKyNamHoc);
            this.Controls.Add(this.headerHanhKiem);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HanhKiem";
            this.Size = new System.Drawing.Size(1151, 768);
            this.Load += new System.EventHandler(this.HanhKiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableHanhKiem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HeaderQuanLiHocSinh headerHanhKiem;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocKyNamHoc;
        private Guna.UI2.WinForms.Guna2ComboBox cbLop;
        private Guna.UI2.WinForms.Guna2Button btnLuuHanhKiem;
        private System.Windows.Forms.Panel paneltrong;
        private Guna.UI2.WinForms.Guna2DataGridView tableHanhKiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private StatCardQuanLiHocSinh statCarHanhKiemTot;
        private StatCardQuanLiHocSinh statCardHanhKiemKha;
        private StatCardQuanLiHocSinh statCardHanhKiemTrungBinh;
        private StatCardQuanLiHocSinh statCardHanhKiemYeu;
        private StatCardQuanLiHocSinh statCardChuaDanhGiaHanhKiem;
    }
}
