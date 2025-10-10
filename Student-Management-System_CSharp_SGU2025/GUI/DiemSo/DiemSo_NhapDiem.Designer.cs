namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class DiemSo_NhapDiem
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbMonHoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbHocKyNamHoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.tableNhapDiem = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MãHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXuatExcel = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuuDiem = new Guna.UI2.WinForms.Guna2Button();
            this.btnNhapDiem = new Guna.UI2.WinForms.Guna2Button();
            this.btnXemBangDiem = new Guna.UI2.WinForms.Guna2Button();
            this.tableXemBangDiem = new Guna.UI2.WinForms.Guna2DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnKhoaDiem = new Guna.UI2.WinForms.Guna2Button();
            this.statCardDiemTrungBinh = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardDiemCaoNhat = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardDiemThapNhat = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardDaNhap = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.headerQuanLiNhapDiem = new Student_Management_System_CSharp_SGU2025.GUI.HeaderQuanLiHocSinh();
            ((System.ComponentModel.ISupportInitialize)(this.tableNhapDiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableXemBangDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // cbMonHoc
            // 
            this.cbMonHoc.BackColor = System.Drawing.Color.Transparent;
            this.cbMonHoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonHoc.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMonHoc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMonHoc.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMonHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbMonHoc.ItemHeight = 30;
            this.cbMonHoc.Items.AddRange(new object[] {
            "Toán ",
            "Văn ",
            "Anh ",
            "Hóa ",
            "Vật Lí"});
            this.cbMonHoc.Location = new System.Drawing.Point(1028, 105);
            this.cbMonHoc.Name = "cbMonHoc";
            this.cbMonHoc.Size = new System.Drawing.Size(136, 36);
            this.cbMonHoc.StartIndex = 0;
            this.cbMonHoc.TabIndex = 12;
            // 
            // cbLop
            // 
            this.cbLop.BackColor = System.Drawing.Color.Transparent;
            this.cbLop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLop.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLop.ItemHeight = 30;
            this.cbLop.Items.AddRange(new object[] {
            "Tất cả lớp",
            "Lớp 6A1",
            "Lớp 7A2"});
            this.cbLop.Location = new System.Drawing.Point(844, 105);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(159, 36);
            this.cbLop.StartIndex = 0;
            this.cbLop.TabIndex = 13;
            // 
            // cbHocKyNamHoc
            // 
            this.cbHocKyNamHoc.BackColor = System.Drawing.Color.Transparent;
            this.cbHocKyNamHoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHocKyNamHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocKyNamHoc.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocKyNamHoc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocKyNamHoc.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHocKyNamHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbHocKyNamHoc.ItemHeight = 30;
            this.cbHocKyNamHoc.Items.AddRange(new object[] {
            "Tất cả Học kì",
            "Học Kỳ I - 2023 - 2024",
            "Học Kỳ II - 2023 - 2024",
            "Học Kỳ I - 2024 - 2025",
            "Học Kỳ II - 2024 - 2025"});
            this.cbHocKyNamHoc.Location = new System.Drawing.Point(551, 105);
            this.cbHocKyNamHoc.Name = "cbHocKyNamHoc";
            this.cbHocKyNamHoc.Size = new System.Drawing.Size(263, 36);
            this.cbHocKyNamHoc.StartIndex = 0;
            this.cbHocKyNamHoc.TabIndex = 14;
            this.cbHocKyNamHoc.SelectedIndexChanged += new System.EventHandler(this.cbHocKyNamHoc_SelectedIndexChanged);
            // 
            // tableNhapDiem
            // 
            this.tableNhapDiem.AllowUserToAddRows = false;
            this.tableNhapDiem.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tableNhapDiem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableNhapDiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tableNhapDiem.ColumnHeadersHeight = 19;
            this.tableNhapDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableNhapDiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MãHS,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column8});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableNhapDiem.DefaultCellStyle = dataGridViewCellStyle3;
            this.tableNhapDiem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableNhapDiem.Location = new System.Drawing.Point(21, 327);
            this.tableNhapDiem.Name = "tableNhapDiem";
            this.tableNhapDiem.RowHeadersVisible = false;
            this.tableNhapDiem.RowHeadersWidth = 51;
            this.tableNhapDiem.RowTemplate.Height = 24;
            this.tableNhapDiem.Size = new System.Drawing.Size(1143, 461);
            this.tableNhapDiem.TabIndex = 19;
            this.tableNhapDiem.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tableNhapDiem.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tableNhapDiem.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tableNhapDiem.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tableNhapDiem.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tableNhapDiem.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tableNhapDiem.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableNhapDiem.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tableNhapDiem.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tableNhapDiem.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableNhapDiem.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tableNhapDiem.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableNhapDiem.ThemeStyle.HeaderStyle.Height = 19;
            this.tableNhapDiem.ThemeStyle.ReadOnly = false;
            this.tableNhapDiem.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tableNhapDiem.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tableNhapDiem.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableNhapDiem.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableNhapDiem.ThemeStyle.RowsStyle.Height = 24;
            this.tableNhapDiem.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableNhapDiem.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableNhapDiem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableNhapDiem_CellContentClick);
            // 
            // MãHS
            // 
            this.MãHS.HeaderText = "STT";
            this.MãHS.MinimumWidth = 6;
            this.MãHS.Name = "MãHS";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Họ và Tên";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Điểm TX";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Giữa Kì";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Cuối Kì";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Trung Bình";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnXuatExcel.BorderRadius = 5;
            this.btnXuatExcel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatExcel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXuatExcel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXuatExcel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXuatExcel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.download;
            this.btnXuatExcel.Location = new System.Drawing.Point(1040, 809);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(124, 35);
            this.btnXuatExcel.TabIndex = 21;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnLuuDiem
            // 
            this.btnLuuDiem.BackColor = System.Drawing.Color.Transparent;
            this.btnLuuDiem.BorderRadius = 5;
            this.btnLuuDiem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuuDiem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuuDiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuuDiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuuDiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnLuuDiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLuuDiem.ForeColor = System.Drawing.Color.White;
            this.btnLuuDiem.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.diskette;
            this.btnLuuDiem.Location = new System.Drawing.Point(1040, 809);
            this.btnLuuDiem.Name = "btnLuuDiem";
            this.btnLuuDiem.Size = new System.Drawing.Size(124, 35);
            this.btnLuuDiem.TabIndex = 20;
            this.btnLuuDiem.Text = "Lưu điểm";
            this.btnLuuDiem.Click += new System.EventHandler(this.btnLuuDiem_Click);
            // 
            // btnNhapDiem
            // 
            this.btnNhapDiem.BackColor = System.Drawing.SystemColors.Control;
            this.btnNhapDiem.BorderRadius = 5;
            this.btnNhapDiem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNhapDiem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNhapDiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNhapDiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNhapDiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNhapDiem.ForeColor = System.Drawing.Color.White;
            this.btnNhapDiem.Location = new System.Drawing.Point(21, 105);
            this.btnNhapDiem.Name = "btnNhapDiem";
            this.btnNhapDiem.Size = new System.Drawing.Size(125, 36);
            this.btnNhapDiem.TabIndex = 23;
            this.btnNhapDiem.Text = "Nhập điểm";
            this.btnNhapDiem.Click += new System.EventHandler(this.btnNhapDiem_Click);
            // 
            // btnXemBangDiem
            // 
            this.btnXemBangDiem.BackColor = System.Drawing.SystemColors.Control;
            this.btnXemBangDiem.BorderRadius = 5;
            this.btnXemBangDiem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXemBangDiem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXemBangDiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXemBangDiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXemBangDiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXemBangDiem.ForeColor = System.Drawing.Color.White;
            this.btnXemBangDiem.Location = new System.Drawing.Point(148, 105);
            this.btnXemBangDiem.Name = "btnXemBangDiem";
            this.btnXemBangDiem.Size = new System.Drawing.Size(150, 36);
            this.btnXemBangDiem.TabIndex = 24;
            this.btnXemBangDiem.Text = "Xem bảng điểm";
            this.btnXemBangDiem.Click += new System.EventHandler(this.btnXemBangDiem_Click);
            // 
            // tableXemBangDiem
            // 
            this.tableXemBangDiem.AllowUserToAddRows = false;
            this.tableXemBangDiem.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.tableXemBangDiem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableXemBangDiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.tableXemBangDiem.ColumnHeadersHeight = 19;
            this.tableXemBangDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableXemBangDiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.Column4});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableXemBangDiem.DefaultCellStyle = dataGridViewCellStyle6;
            this.tableXemBangDiem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableXemBangDiem.Location = new System.Drawing.Point(21, 327);
            this.tableXemBangDiem.Name = "tableXemBangDiem";
            this.tableXemBangDiem.RowHeadersVisible = false;
            this.tableXemBangDiem.RowHeadersWidth = 51;
            this.tableXemBangDiem.RowTemplate.Height = 24;
            this.tableXemBangDiem.Size = new System.Drawing.Size(1143, 476);
            this.tableXemBangDiem.TabIndex = 25;
            this.tableXemBangDiem.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tableXemBangDiem.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tableXemBangDiem.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tableXemBangDiem.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tableXemBangDiem.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tableXemBangDiem.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tableXemBangDiem.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableXemBangDiem.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tableXemBangDiem.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tableXemBangDiem.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableXemBangDiem.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tableXemBangDiem.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableXemBangDiem.ThemeStyle.HeaderStyle.Height = 19;
            this.tableXemBangDiem.ThemeStyle.ReadOnly = false;
            this.tableXemBangDiem.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tableXemBangDiem.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tableXemBangDiem.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableXemBangDiem.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableXemBangDiem.ThemeStyle.RowsStyle.Height = 24;
            this.tableXemBangDiem.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableXemBangDiem.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableXemBangDiem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableXemBangDiem_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Học sinh";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Toán";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Văn";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Anh";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Vật Lí";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Hóa";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "TB Chung";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // btnKhoaDiem
            // 
            this.btnKhoaDiem.BackColor = System.Drawing.Color.Transparent;
            this.btnKhoaDiem.BorderRadius = 5;
            this.btnKhoaDiem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKhoaDiem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKhoaDiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKhoaDiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKhoaDiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnKhoaDiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKhoaDiem.ForeColor = System.Drawing.Color.White;
            this.btnKhoaDiem.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.padlock1;
            this.btnKhoaDiem.Location = new System.Drawing.Point(874, 809);
            this.btnKhoaDiem.Name = "btnKhoaDiem";
            this.btnKhoaDiem.Size = new System.Drawing.Size(140, 35);
            this.btnKhoaDiem.TabIndex = 26;
            this.btnKhoaDiem.Text = "Khóa điểm";
            this.btnKhoaDiem.Click += new System.EventHandler(this.btnKhoaDiem_Click);
            // 
            // statCardDiemTrungBinh
            // 
            this.statCardDiemTrungBinh.BackColor = System.Drawing.Color.White;
            this.statCardDiemTrungBinh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statCardDiemTrungBinh.Location = new System.Drawing.Point(18, 164);
            this.statCardDiemTrungBinh.Name = "statCardDiemTrungBinh";
            this.statCardDiemTrungBinh.Size = new System.Drawing.Size(255, 142);
            this.statCardDiemTrungBinh.TabIndex = 18;
            // 
            // statCardDiemCaoNhat
            // 
            this.statCardDiemCaoNhat.BackColor = System.Drawing.Color.White;
            this.statCardDiemCaoNhat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statCardDiemCaoNhat.Location = new System.Drawing.Point(315, 164);
            this.statCardDiemCaoNhat.Name = "statCardDiemCaoNhat";
            this.statCardDiemCaoNhat.Size = new System.Drawing.Size(252, 142);
            this.statCardDiemCaoNhat.TabIndex = 17;
            // 
            // statCardDiemThapNhat
            // 
            this.statCardDiemThapNhat.BackColor = System.Drawing.Color.White;
            this.statCardDiemThapNhat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statCardDiemThapNhat.Location = new System.Drawing.Point(617, 164);
            this.statCardDiemThapNhat.Name = "statCardDiemThapNhat";
            this.statCardDiemThapNhat.Size = new System.Drawing.Size(253, 142);
            this.statCardDiemThapNhat.TabIndex = 16;
            // 
            // statCardDaNhap
            // 
            this.statCardDaNhap.BackColor = System.Drawing.Color.White;
            this.statCardDaNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statCardDaNhap.Location = new System.Drawing.Point(916, 164);
            this.statCardDaNhap.Name = "statCardDaNhap";
            this.statCardDaNhap.Size = new System.Drawing.Size(248, 142);
            this.statCardDaNhap.TabIndex = 15;
            // 
            // headerQuanLiNhapDiem
            // 
            this.headerQuanLiNhapDiem.BackColor = System.Drawing.Color.White;
            this.headerQuanLiNhapDiem.Location = new System.Drawing.Point(0, 0);
            this.headerQuanLiNhapDiem.Name = "headerQuanLiNhapDiem";
            this.headerQuanLiNhapDiem.Size = new System.Drawing.Size(1184, 81);
            this.headerQuanLiNhapDiem.TabIndex = 0;
            this.headerQuanLiNhapDiem.Load += new System.EventHandler(this.headerQuanLiNhapDiem_Load);
            // 
            // DiemSo_NhapDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnKhoaDiem);
            this.Controls.Add(this.tableXemBangDiem);
            this.Controls.Add(this.btnXemBangDiem);
            this.Controls.Add(this.btnNhapDiem);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnLuuDiem);
            this.Controls.Add(this.tableNhapDiem);
            this.Controls.Add(this.statCardDiemTrungBinh);
            this.Controls.Add(this.statCardDiemCaoNhat);
            this.Controls.Add(this.statCardDiemThapNhat);
            this.Controls.Add(this.statCardDaNhap);
            this.Controls.Add(this.cbHocKyNamHoc);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.cbMonHoc);
            this.Controls.Add(this.headerQuanLiNhapDiem);
            this.Name = "DiemSo_NhapDiem";
            this.Size = new System.Drawing.Size(1184, 900);
            this.Load += new System.EventHandler(this.DiemSo_NhapDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableNhapDiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableXemBangDiem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HeaderQuanLiHocSinh headerQuanLiNhapDiem;
        private Guna.UI2.WinForms.Guna2ComboBox cbMonHoc;
        private Guna.UI2.WinForms.Guna2ComboBox cbLop;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocKyNamHoc;
        private StatCardQuanLiHocSinh statCardDaNhap;
        private StatCardQuanLiHocSinh statCardDiemThapNhat;
        private StatCardQuanLiHocSinh statCardDiemCaoNhat;
        private StatCardQuanLiHocSinh statCardDiemTrungBinh;
        private Guna.UI2.WinForms.Guna2DataGridView tableNhapDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MãHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private Guna.UI2.WinForms.Guna2Button btnLuuDiem;
        private Guna.UI2.WinForms.Guna2Button btnXuatExcel;
        private Guna.UI2.WinForms.Guna2Button btnNhapDiem;
        private Guna.UI2.WinForms.Guna2Button btnXemBangDiem;
        private Guna.UI2.WinForms.Guna2DataGridView tableXemBangDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private Guna.UI2.WinForms.Guna2Button btnKhoaDiem;
    }
}
