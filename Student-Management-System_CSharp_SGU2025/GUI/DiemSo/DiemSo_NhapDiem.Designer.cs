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
            this.maHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hoVaTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemGK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemCK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemTB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXuatExcel = new Guna.UI2.WinForms.Guna2Button();
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.statCardDaNhap = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardDiemThapNhat = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardDiemCaoNhat = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardDiemTrungBinh = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.headerQuanLiNhapDiem = new Student_Management_System_CSharp_SGU2025.GUI.HeaderQuanLiHocSinh();
            ((System.ComponentModel.ISupportInitialize)(this.tableNhapDiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableXemBangDiem)).BeginInit();
            this.guna2Panel1.SuspendLayout();
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
            "Chọn môn",
            "Toán ",
            "Văn ",
            "Anh ",
            "Hóa ",
            "Vật Lí"});
            this.cbMonHoc.Location = new System.Drawing.Point(882, 20);
            this.cbMonHoc.Margin = new System.Windows.Forms.Padding(2);
            this.cbMonHoc.Name = "cbMonHoc";
            this.cbMonHoc.Size = new System.Drawing.Size(262, 36);
            this.cbMonHoc.StartIndex = 0;
            this.cbMonHoc.TabIndex = 12;
            this.cbMonHoc.SelectedIndexChanged += new System.EventHandler(this.cbMonHoc_SelectedIndexChanged);
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
            "Chọn lớp",
            "Lớp 6A1",
            "Lớp 7A2"});
            this.cbLop.Location = new System.Drawing.Point(735, 20);
            this.cbLop.Margin = new System.Windows.Forms.Padding(2);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(120, 36);
            this.cbLop.StartIndex = 0;
            this.cbLop.TabIndex = 13;
            this.cbLop.SelectedIndexChanged += new System.EventHandler(this.cbLop_SelectedIndexChanged);
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
            "Chọn học kỳ",
            "Học Kỳ I - 2023 - 2024",
            "Học Kỳ II - 2023 - 2024",
            "Học Kỳ I - 2024 - 2025",
            "Học Kỳ II - 2024 - 2025"});
            this.cbHocKyNamHoc.Location = new System.Drawing.Point(503, 20);
            this.cbHocKyNamHoc.Margin = new System.Windows.Forms.Padding(2);
            this.cbHocKyNamHoc.Name = "cbHocKyNamHoc";
            this.cbHocKyNamHoc.Size = new System.Drawing.Size(184, 36);
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
            this.tableNhapDiem.Location = new System.Drawing.Point(16, 224);
            this.tableNhapDiem.Margin = new System.Windows.Forms.Padding(2);
            this.tableNhapDiem.Name = "tableNhapDiem";
            this.tableNhapDiem.RowHeadersVisible = false;
            this.tableNhapDiem.RowHeadersWidth = 51;
            this.tableNhapDiem.RowTemplate.Height = 24;
            this.tableNhapDiem.Size = new System.Drawing.Size(1128, 464);
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
            // maHS
            // 
            this.maHS.HeaderText = "Mã HS";
            this.maHS.MinimumWidth = 6;
            this.maHS.Name = "maHS";
            // 
            // hoVaTen
            // 
            this.hoVaTen.HeaderText = "Họ và Tên";
            this.hoVaTen.MinimumWidth = 6;
            this.hoVaTen.Name = "hoVaTen";
            // 
            // diemTX
            // 
            this.diemTX.HeaderText = "Điểm TX";
            this.diemTX.MinimumWidth = 6;
            this.diemTX.Name = "diemTX";
            // 
            // diemGK
            // 
            this.diemGK.HeaderText = "Giữa Kì";
            this.diemGK.MinimumWidth = 6;
            this.diemGK.Name = "diemGK";
            // 
            // diemCK
            // 
            this.diemCK.HeaderText = "Cuối Kì";
            this.diemCK.MinimumWidth = 6;
            this.diemCK.Name = "diemCK";
            // 
            // diemTB
            // 
            this.diemTB.HeaderText = "Trung Bình";
            this.diemTB.MinimumWidth = 6;
            this.diemTB.Name = "diemTB";
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
            this.btnXuatExcel.Location = new System.Drawing.Point(1006, 704);
            this.btnXuatExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(138, 43);
            this.btnXuatExcel.TabIndex = 21;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
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
            this.btnNhapDiem.Location = new System.Drawing.Point(10, 6);
            this.btnNhapDiem.Margin = new System.Windows.Forms.Padding(2);
            this.btnNhapDiem.Name = "btnNhapDiem";
            this.btnNhapDiem.Size = new System.Drawing.Size(139, 36);
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
            this.btnXemBangDiem.Location = new System.Drawing.Point(144, 6);
            this.btnXemBangDiem.Margin = new System.Windows.Forms.Padding(2);
            this.btnXemBangDiem.Name = "btnXemBangDiem";
            this.btnXemBangDiem.Size = new System.Drawing.Size(139, 36);
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
            this.tableXemBangDiem.Location = new System.Drawing.Point(16, 224);
            this.tableXemBangDiem.Margin = new System.Windows.Forms.Padding(2);
            this.tableXemBangDiem.Name = "tableXemBangDiem";
            this.tableXemBangDiem.RowHeadersVisible = false;
            this.tableXemBangDiem.RowHeadersWidth = 51;
            this.tableXemBangDiem.RowTemplate.Height = 24;
            this.tableXemBangDiem.Size = new System.Drawing.Size(1128, 464);
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
            // maHS1
            // 
            this.maHS1.HeaderText = "Mã HS";
            this.maHS1.Name = "maHS1";
            // 
            // hoTen
            // 
            this.hoTen.HeaderText = "Học sinh";
            this.hoTen.MinimumWidth = 6;
            this.hoTen.Name = "hoTen";
            // 
            // dToan
            // 
            this.dToan.HeaderText = "Toán";
            this.dToan.MinimumWidth = 6;
            this.dToan.Name = "dToan";
            // 
            // dVan
            // 
            this.dVan.HeaderText = "Văn";
            this.dVan.MinimumWidth = 6;
            this.dVan.Name = "dVan";
            // 
            // dAnh
            // 
            this.dAnh.HeaderText = "Anh";
            this.dAnh.MinimumWidth = 6;
            this.dAnh.Name = "dAnh";
            // 
            // dVatLi
            // 
            this.dVatLi.HeaderText = "Vật Lí";
            this.dVatLi.MinimumWidth = 6;
            this.dVatLi.Name = "dVatLi";
            // 
            // dHoa
            // 
            this.dHoa.HeaderText = "Hóa";
            this.dHoa.MinimumWidth = 6;
            this.dHoa.Name = "dHoa";
            // 
            // dTB
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderRadius = 7;
            this.guna2Panel1.Controls.Add(this.btnNhapDiem);
            this.guna2Panel1.Controls.Add(this.btnXemBangDiem);
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(16, 12);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(293, 48);
            this.guna2Panel1.TabIndex = 28;
            // 
            // statCardDaNhap
            // 
            this.statCardDaNhap.BackColor = System.Drawing.Color.Transparent;
            this.statCardDaNhap.Location = new System.Drawing.Point(882, 67);
            this.statCardDaNhap.Margin = new System.Windows.Forms.Padding(2);
            this.statCardDaNhap.Name = "statCardDaNhap";
            this.statCardDaNhap.Size = new System.Drawing.Size(262, 136);
            this.statCardDaNhap.TabIndex = 27;
            // 
            // statCardDiemThapNhat
            // 
            this.statCardDiemThapNhat.BackColor = System.Drawing.Color.Transparent;
            this.statCardDiemThapNhat.Location = new System.Drawing.Point(593, 67);
            this.statCardDiemThapNhat.Margin = new System.Windows.Forms.Padding(2);
            this.statCardDiemThapNhat.Name = "statCardDiemThapNhat";
            this.statCardDiemThapNhat.Size = new System.Drawing.Size(262, 136);
            this.statCardDiemThapNhat.TabIndex = 27;
            this.statCardDiemThapNhat.Load += new System.EventHandler(this.statCardDiemThapNhat_Load);
            // 
            // statCardDiemCaoNhat
            // 
            this.statCardDiemCaoNhat.BackColor = System.Drawing.Color.Transparent;
            this.statCardDiemCaoNhat.Location = new System.Drawing.Point(305, 67);
            this.statCardDiemCaoNhat.Margin = new System.Windows.Forms.Padding(2);
            this.statCardDiemCaoNhat.Name = "statCardDiemCaoNhat";
            this.statCardDiemCaoNhat.Size = new System.Drawing.Size(262, 136);
            this.statCardDiemCaoNhat.TabIndex = 27;
            // 
            // statCardDiemTrungBinh
            // 
            this.statCardDiemTrungBinh.BackColor = System.Drawing.Color.Transparent;
            this.statCardDiemTrungBinh.Location = new System.Drawing.Point(16, 67);
            this.statCardDiemTrungBinh.Margin = new System.Windows.Forms.Padding(2);
            this.statCardDiemTrungBinh.Name = "statCardDiemTrungBinh";
            this.statCardDiemTrungBinh.Size = new System.Drawing.Size(262, 136);
            this.statCardDiemTrungBinh.TabIndex = 27;
            // 
            // headerQuanLiNhapDiem
            // 
            this.headerQuanLiNhapDiem.BackColor = System.Drawing.Color.White;
            this.headerQuanLiNhapDiem.Location = new System.Drawing.Point(0, 0);
            this.headerQuanLiNhapDiem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.headerQuanLiNhapDiem.Name = "headerQuanLiNhapDiem";
            this.headerQuanLiNhapDiem.Size = new System.Drawing.Size(888, 0);
            this.headerQuanLiNhapDiem.TabIndex = 0;
            this.headerQuanLiNhapDiem.Visible = false;
            this.headerQuanLiNhapDiem.Load += new System.EventHandler(this.headerQuanLiNhapDiem_Load);
            // 
            // DiemSo_NhapDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.cbHocKyBD);
            this.Controls.Add(this.btnXemChiTiet);
            this.Controls.Add(this.cbLopBD);
            this.Controls.Add(this.btnSuaDiem);
            this.Controls.Add(this.btnThemDiem);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.statCardDaNhap);
            this.Controls.Add(this.statCardDiemThapNhat);
            this.Controls.Add(this.statCardDiemCaoNhat);
            this.Controls.Add(this.statCardDiemTrungBinh);
            this.Controls.Add(this.tableXemBangDiem);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.tableNhapDiem);
            this.Controls.Add(this.cbHocKyNamHoc);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.cbMonHoc);
            this.Controls.Add(this.headerQuanLiNhapDiem);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DiemSo_NhapDiem";
            this.Size = new System.Drawing.Size(1168, 768);
            this.Load += new System.EventHandler(this.DiemSo_NhapDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableNhapDiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableXemBangDiem)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HeaderQuanLiHocSinh headerQuanLiNhapDiem;
        private Guna.UI2.WinForms.Guna2ComboBox cbMonHoc;
        private Guna.UI2.WinForms.Guna2ComboBox cbLop;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocKyNamHoc;
        private Guna.UI2.WinForms.Guna2DataGridView tableNhapDiem;
        private Guna.UI2.WinForms.Guna2Button btnXuatExcel;
        private Guna.UI2.WinForms.Guna2Button btnNhapDiem;
        private Guna.UI2.WinForms.Guna2Button btnXemBangDiem;
        private Guna.UI2.WinForms.Guna2DataGridView tableXemBangDiem;
        private StatCardQuanLiHocSinh statCardDiemTrungBinh;
        private StatCardQuanLiHocSinh statCardDiemCaoNhat;
        private StatCardQuanLiHocSinh statCardDiemThapNhat;
        private StatCardQuanLiHocSinh statCardDaNhap;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn maHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoVaTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemGK;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemCK;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn maHS1;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn dToan;
        private System.Windows.Forms.DataGridViewTextBoxColumn dVan;
        private System.Windows.Forms.DataGridViewTextBoxColumn dAnh;
        private System.Windows.Forms.DataGridViewTextBoxColumn dVatLi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dHoa;
        private System.Windows.Forms.DataGridViewTextBoxColumn dTB;
        private Guna.UI2.WinForms.Guna2Button btnThemDiem;
        private Guna.UI2.WinForms.Guna2Button btnSuaDiem;
        private Guna.UI2.WinForms.Guna2ComboBox cbLopBD;
        private Guna.UI2.WinForms.Guna2Button btnXemChiTiet;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocKyBD;
        private Guna.UI2.WinForms.Guna2Button btnReload;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
    }
}
