namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ChinhSuaHocSinh
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPhuHuynhDuocChon = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbMoiQuanHe = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dateTimePickerNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lbNgaySinh = new System.Windows.Forms.Label();
            this.lbHovaTen = new System.Windows.Forms.Label();
            this.groupBoxGioiTinh = new System.Windows.Forms.GroupBox();
            this.rbNu = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbNam = new Guna.UI2.WinForms.Guna2RadioButton();
            this.txtTrangThai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbTrangThai = new System.Windows.Forms.Label();
            this.lbEmail = new System.Windows.Forms.Label();
            this.lbSdt = new System.Windows.Forms.Label();
            this.lbGioiTinh = new System.Windows.Forms.Label();
            this.btnChon = new Guna.UI2.WinForms.Guna2Button();
            this.tablePhuHuynh = new Guna.UI2.WinForms.Guna2DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtHovaTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnChinhSuaHocSinh = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            this.groupBoxGioiTinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePhuHuynh)).BeginInit();
            this.SuspendLayout();
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
            this.btnHuy.Location = new System.Drawing.Point(790, 80);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(62, 36);
            this.btnHuy.TabIndex = 64;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(997, 57);
            this.panel1.TabIndex = 49;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chỉnh sửa học sinh";
            // 
            // txtPhuHuynhDuocChon
            // 
            this.txtPhuHuynhDuocChon.BorderRadius = 7;
            this.txtPhuHuynhDuocChon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPhuHuynhDuocChon.DefaultText = "";
            this.txtPhuHuynhDuocChon.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPhuHuynhDuocChon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPhuHuynhDuocChon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPhuHuynhDuocChon.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPhuHuynhDuocChon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPhuHuynhDuocChon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPhuHuynhDuocChon.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPhuHuynhDuocChon.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtPhuHuynhDuocChon.Location = new System.Drawing.Point(53, 80);
            this.txtPhuHuynhDuocChon.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtPhuHuynhDuocChon.Name = "txtPhuHuynhDuocChon";
            this.txtPhuHuynhDuocChon.PlaceholderText = "Chọn phụ huynh";
            this.txtPhuHuynhDuocChon.ReadOnly = true;
            this.txtPhuHuynhDuocChon.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPhuHuynhDuocChon.SelectedText = "";
            this.txtPhuHuynhDuocChon.Size = new System.Drawing.Size(177, 36);
            this.txtPhuHuynhDuocChon.TabIndex = 72;
            // 
            // cbMoiQuanHe
            // 
            this.cbMoiQuanHe.BackColor = System.Drawing.Color.Transparent;
            this.cbMoiQuanHe.BorderRadius = 5;
            this.cbMoiQuanHe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMoiQuanHe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMoiQuanHe.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMoiQuanHe.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMoiQuanHe.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMoiQuanHe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbMoiQuanHe.ItemHeight = 30;
            this.cbMoiQuanHe.Items.AddRange(new object[] {
            "Chọn mối quan hệ",
            "Cha",
            "Mẹ",
            "Ông",
            "Bà",
            "Người giám hộ"});
            this.cbMoiQuanHe.Location = new System.Drawing.Point(253, 80);
            this.cbMoiQuanHe.Margin = new System.Windows.Forms.Padding(2);
            this.cbMoiQuanHe.Name = "cbMoiQuanHe";
            this.cbMoiQuanHe.Size = new System.Drawing.Size(179, 36);
            this.cbMoiQuanHe.StartIndex = 0;
            this.cbMoiQuanHe.TabIndex = 70;
            // 
            // dateTimePickerNgaySinh
            // 
            this.dateTimePickerNgaySinh.BorderRadius = 5;
            this.dateTimePickerNgaySinh.Checked = true;
            this.dateTimePickerNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerNgaySinh.FillColor = System.Drawing.Color.White;
            this.dateTimePickerNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerNgaySinh.Location = new System.Drawing.Point(575, 130);
            this.dateTimePickerNgaySinh.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerNgaySinh.Name = "dateTimePickerNgaySinh";
            this.dateTimePickerNgaySinh.Size = new System.Drawing.Size(242, 29);
            this.dateTimePickerNgaySinh.TabIndex = 69;
            this.dateTimePickerNgaySinh.Value = new System.DateTime(2025, 10, 9, 22, 44, 15, 114);
            // 
            // lbNgaySinh
            // 
            this.lbNgaySinh.AutoSize = true;
            this.lbNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNgaySinh.Location = new System.Drawing.Point(453, 130);
            this.lbNgaySinh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNgaySinh.Name = "lbNgaySinh";
            this.lbNgaySinh.Size = new System.Drawing.Size(81, 20);
            this.lbNgaySinh.TabIndex = 68;
            this.lbNgaySinh.Text = "Ngày sinh :";
            // 
            // lbHovaTen
            // 
            this.lbHovaTen.AutoSize = true;
            this.lbHovaTen.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHovaTen.Location = new System.Drawing.Point(49, 130);
            this.lbHovaTen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHovaTen.Name = "lbHovaTen";
            this.lbHovaTen.Size = new System.Drawing.Size(82, 20);
            this.lbHovaTen.TabIndex = 66;
            this.lbHovaTen.Text = "Họ và Tên :";
            // 
            // groupBoxGioiTinh
            // 
            this.groupBoxGioiTinh.Controls.Add(this.rbNu);
            this.groupBoxGioiTinh.Controls.Add(this.rbNam);
            this.groupBoxGioiTinh.Location = new System.Drawing.Point(171, 175);
            this.groupBoxGioiTinh.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxGioiTinh.Name = "groupBoxGioiTinh";
            this.groupBoxGioiTinh.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxGioiTinh.Size = new System.Drawing.Size(134, 49);
            this.groupBoxGioiTinh.TabIndex = 80;
            this.groupBoxGioiTinh.TabStop = false;
            // 
            // rbNu
            // 
            this.rbNu.AutoSize = true;
            this.rbNu.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbNu.CheckedState.BorderThickness = 0;
            this.rbNu.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbNu.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rbNu.CheckedState.InnerOffset = -4;
            this.rbNu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNu.Location = new System.Drawing.Point(82, 15);
            this.rbNu.Margin = new System.Windows.Forms.Padding(2);
            this.rbNu.Name = "rbNu";
            this.rbNu.Size = new System.Drawing.Size(45, 23);
            this.rbNu.TabIndex = 19;
            this.rbNu.Text = "Nữ";
            this.rbNu.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rbNu.UncheckedState.BorderThickness = 2;
            this.rbNu.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rbNu.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // rbNam
            // 
            this.rbNam.AutoSize = true;
            this.rbNam.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbNam.CheckedState.BorderThickness = 0;
            this.rbNam.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbNam.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rbNam.CheckedState.InnerOffset = -4;
            this.rbNam.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNam.Location = new System.Drawing.Point(14, 15);
            this.rbNam.Margin = new System.Windows.Forms.Padding(2);
            this.rbNam.Name = "rbNam";
            this.rbNam.Size = new System.Drawing.Size(56, 23);
            this.rbNam.TabIndex = 18;
            this.rbNam.Text = "Nam";
            this.rbNam.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rbNam.UncheckedState.BorderThickness = 2;
            this.rbNam.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rbNam.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.BorderRadius = 7;
            this.txtTrangThai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTrangThai.DefaultText = "Đang học (mặc định)";
            this.txtTrangThai.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTrangThai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTrangThai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTrangThai.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTrangThai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTrangThai.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTrangThai.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtTrangThai.Location = new System.Drawing.Point(575, 238);
            this.txtTrangThai.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.PlaceholderText = "";
            this.txtTrangThai.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTrangThai.SelectedText = "";
            this.txtTrangThai.Size = new System.Drawing.Size(242, 29);
            this.txtTrangThai.TabIndex = 79;
            // 
            // lbTrangThai
            // 
            this.lbTrangThai.AutoSize = true;
            this.lbTrangThai.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTrangThai.Location = new System.Drawing.Point(453, 238);
            this.lbTrangThai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTrangThai.Name = "lbTrangThai";
            this.lbTrangThai.Size = new System.Drawing.Size(82, 20);
            this.lbTrangThai.TabIndex = 78;
            this.lbTrangThai.Text = "Trạng thái :";
            // 
            // lbEmail
            // 
            this.lbEmail.AutoSize = true;
            this.lbEmail.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmail.Location = new System.Drawing.Point(49, 238);
            this.lbEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(53, 20);
            this.lbEmail.TabIndex = 75;
            this.lbEmail.Text = "Email :";
            // 
            // lbSdt
            // 
            this.lbSdt.AutoSize = true;
            this.lbSdt.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSdt.Location = new System.Drawing.Point(453, 187);
            this.lbSdt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSdt.Name = "lbSdt";
            this.lbSdt.Size = new System.Drawing.Size(104, 20);
            this.lbSdt.TabIndex = 74;
            this.lbSdt.Text = "Số điện thoại :";
            // 
            // lbGioiTinh
            // 
            this.lbGioiTinh.AutoSize = true;
            this.lbGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGioiTinh.Location = new System.Drawing.Point(49, 193);
            this.lbGioiTinh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbGioiTinh.Name = "lbGioiTinh";
            this.lbGioiTinh.Size = new System.Drawing.Size(72, 20);
            this.lbGioiTinh.TabIndex = 73;
            this.lbGioiTinh.Text = "Giới tính :";
            // 
            // btnChon
            // 
            this.btnChon.BorderRadius = 7;
            this.btnChon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChon.ForeColor = System.Drawing.Color.White;
            this.btnChon.ImageSize = new System.Drawing.Size(15, 15);
            this.btnChon.Location = new System.Drawing.Point(864, 280);
            this.btnChon.Margin = new System.Windows.Forms.Padding(2);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(122, 36);
            this.btnChon.TabIndex = 83;
            this.btnChon.Text = "Chọn";
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // tablePhuHuynh
            // 
            this.tablePhuHuynh.AllowUserToAddRows = false;
            this.tablePhuHuynh.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tablePhuHuynh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablePhuHuynh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tablePhuHuynh.ColumnHeadersHeight = 19;
            this.tablePhuHuynh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tablePhuHuynh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tablePhuHuynh.DefaultCellStyle = dataGridViewCellStyle3;
            this.tablePhuHuynh.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tablePhuHuynh.Location = new System.Drawing.Point(17, 333);
            this.tablePhuHuynh.Margin = new System.Windows.Forms.Padding(2);
            this.tablePhuHuynh.Name = "tablePhuHuynh";
            this.tablePhuHuynh.RowHeadersVisible = false;
            this.tablePhuHuynh.RowHeadersWidth = 51;
            this.tablePhuHuynh.RowTemplate.Height = 24;
            this.tablePhuHuynh.Size = new System.Drawing.Size(969, 285);
            this.tablePhuHuynh.TabIndex = 81;
            this.tablePhuHuynh.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tablePhuHuynh.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tablePhuHuynh.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tablePhuHuynh.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tablePhuHuynh.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tablePhuHuynh.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tablePhuHuynh.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tablePhuHuynh.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tablePhuHuynh.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tablePhuHuynh.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablePhuHuynh.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tablePhuHuynh.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tablePhuHuynh.ThemeStyle.HeaderStyle.Height = 19;
            this.tablePhuHuynh.ThemeStyle.ReadOnly = false;
            this.tablePhuHuynh.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tablePhuHuynh.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tablePhuHuynh.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablePhuHuynh.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tablePhuHuynh.ThemeStyle.RowsStyle.Height = 24;
            this.tablePhuHuynh.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tablePhuHuynh.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã PH";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Họ và Tên";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Sdt";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Email";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Địa chỉ";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 7;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.search1;
            this.txtTimKiem.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtTimKiem.Location = new System.Drawing.Point(600, 280);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm phụ huynh ...";
            this.txtTimKiem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(242, 32);
            this.txtTimKiem.TabIndex = 82;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderRadius = 7;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.couple;
            this.txtEmail.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtEmail.Location = new System.Drawing.Point(171, 238);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Nhập Email";
            this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(242, 32);
            this.txtEmail.TabIndex = 77;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.BorderRadius = 7;
            this.txtSoDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoDienThoai.DefaultText = "";
            this.txtSoDienThoai.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSoDienThoai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSoDienThoai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoDienThoai.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoDienThoai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSoDienThoai.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSoDienThoai.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.phone_call;
            this.txtSoDienThoai.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtSoDienThoai.Location = new System.Drawing.Point(575, 181);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PlaceholderText = "Nhập số điện thoại";
            this.txtSoDienThoai.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(242, 32);
            this.txtSoDienThoai.TabIndex = 76;
            // 
            // txtHovaTen
            // 
            this.txtHovaTen.BorderRadius = 7;
            this.txtHovaTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHovaTen.DefaultText = "";
            this.txtHovaTen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtHovaTen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtHovaTen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtHovaTen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtHovaTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHovaTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHovaTen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHovaTen.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.people;
            this.txtHovaTen.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtHovaTen.Location = new System.Drawing.Point(171, 127);
            this.txtHovaTen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtHovaTen.Name = "txtHovaTen";
            this.txtHovaTen.PlaceholderText = "Nhập Họ và Tên ...";
            this.txtHovaTen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHovaTen.SelectedText = "";
            this.txtHovaTen.Size = new System.Drawing.Size(242, 32);
            this.txtHovaTen.TabIndex = 67;
            // 
            // btnChinhSuaHocSinh
            // 
            this.btnChinhSuaHocSinh.BorderRadius = 7;
            this.btnChinhSuaHocSinh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChinhSuaHocSinh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChinhSuaHocSinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChinhSuaHocSinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChinhSuaHocSinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChinhSuaHocSinh.ForeColor = System.Drawing.Color.White;
            this.btnChinhSuaHocSinh.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus;
            this.btnChinhSuaHocSinh.ImageSize = new System.Drawing.Size(15, 15);
            this.btnChinhSuaHocSinh.Location = new System.Drawing.Point(864, 80);
            this.btnChinhSuaHocSinh.Margin = new System.Windows.Forms.Padding(2);
            this.btnChinhSuaHocSinh.Name = "btnChinhSuaHocSinh";
            this.btnChinhSuaHocSinh.Size = new System.Drawing.Size(122, 36);
            this.btnChinhSuaHocSinh.TabIndex = 63;
            this.btnChinhSuaHocSinh.Text = "Sửa học sinh";
            this.btnChinhSuaHocSinh.Click += new System.EventHandler(this.btnChinhSuaHocSinh_Click);
            // 
            // ChinhSuaHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 629);
            this.ControlBox = false;
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.tablePhuHuynh);
            this.Controls.Add(this.groupBoxGioiTinh);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.lbTrangThai);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lbEmail);
            this.Controls.Add(this.lbSdt);
            this.Controls.Add(this.lbGioiTinh);
            this.Controls.Add(this.txtPhuHuynhDuocChon);
            this.Controls.Add(this.cbMoiQuanHe);
            this.Controls.Add(this.dateTimePickerNgaySinh);
            this.Controls.Add(this.lbNgaySinh);
            this.Controls.Add(this.txtHovaTen);
            this.Controls.Add(this.lbHovaTen);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnChinhSuaHocSinh);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(1013, 668);
            this.MinimumSize = new System.Drawing.Size(1013, 668);
            this.Name = "ChinhSuaHocSinh";
            this.Text = "ChinhSuaHocSinh";
            this.Load += new System.EventHandler(this.ChinhSuaHocSinh_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxGioiTinh.ResumeLayout(false);
            this.groupBoxGioiTinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePhuHuynh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnChinhSuaHocSinh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtPhuHuynhDuocChon;
        private Guna.UI2.WinForms.Guna2ComboBox cbMoiQuanHe;
        private Guna.UI2.WinForms.Guna2DateTimePicker dateTimePickerNgaySinh;
        private System.Windows.Forms.Label lbNgaySinh;
        private Guna.UI2.WinForms.Guna2TextBox txtHovaTen;
        private System.Windows.Forms.Label lbHovaTen;
        private System.Windows.Forms.GroupBox groupBoxGioiTinh;
        private Guna.UI2.WinForms.Guna2RadioButton rbNu;
        private Guna.UI2.WinForms.Guna2RadioButton rbNam;
        private Guna.UI2.WinForms.Guna2TextBox txtTrangThai;
        private System.Windows.Forms.Label lbTrangThai;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtSoDienThoai;
        private System.Windows.Forms.Label lbEmail;
        private System.Windows.Forms.Label lbSdt;
        private System.Windows.Forms.Label lbGioiTinh;
        private Guna.UI2.WinForms.Guna2Button btnChon;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2DataGridView tablePhuHuynh;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}