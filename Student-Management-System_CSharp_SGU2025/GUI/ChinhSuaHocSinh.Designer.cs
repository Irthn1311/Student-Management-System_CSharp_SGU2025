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
            this.cbMoiQuanHe = new Guna.UI2.WinForms.Guna2ComboBox();
            this.groupBoxGioiTinh = new System.Windows.Forms.GroupBox();
            this.rbNu = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbNam = new Guna.UI2.WinForms.Guna2RadioButton();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.txtTrangThai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbTrangThai = new System.Windows.Forms.Label();
            this.lbEmail = new System.Windows.Forms.Label();
            this.lbSdt = new System.Windows.Forms.Label();
            this.dateTimePickerNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lbGioiTinh = new System.Windows.Forms.Label();
            this.lbNgaySinh = new System.Windows.Forms.Label();
            this.lbHovaTen = new System.Windows.Forms.Label();
            this.cbPhuHuynh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChinhSuaHocSinh = new Guna.UI2.WinForms.Guna2Button();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtHovaTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbHocKyNamHoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.groupBoxGioiTinh.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.cbMoiQuanHe.Location = new System.Drawing.Point(214, 150);
            this.cbMoiQuanHe.Margin = new System.Windows.Forms.Padding(2);
            this.cbMoiQuanHe.Name = "cbMoiQuanHe";
            this.cbMoiQuanHe.Size = new System.Drawing.Size(174, 36);
            this.cbMoiQuanHe.StartIndex = 0;
            this.cbMoiQuanHe.TabIndex = 66;
            // 
            // groupBoxGioiTinh
            // 
            this.groupBoxGioiTinh.Controls.Add(this.rbNu);
            this.groupBoxGioiTinh.Controls.Add(this.rbNam);
            this.groupBoxGioiTinh.Location = new System.Drawing.Point(146, 302);
            this.groupBoxGioiTinh.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxGioiTinh.Name = "groupBoxGioiTinh";
            this.groupBoxGioiTinh.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxGioiTinh.Size = new System.Drawing.Size(134, 49);
            this.groupBoxGioiTinh.TabIndex = 65;
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
            this.btnHuy.Location = new System.Drawing.Point(199, 502);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(53, 29);
            this.btnHuy.TabIndex = 64;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.BorderRadius = 7;
            this.txtTrangThai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTrangThai.DefaultText = "";
            this.txtTrangThai.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTrangThai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTrangThai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTrangThai.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTrangThai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTrangThai.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTrangThai.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.txtTrangThai.Location = new System.Drawing.Point(146, 461);
            this.txtTrangThai.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.PlaceholderText = "";
            this.txtTrangThai.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTrangThai.SelectedText = "";
            this.txtTrangThai.Size = new System.Drawing.Size(242, 29);
            this.txtTrangThai.TabIndex = 62;
            // 
            // lbTrangThai
            // 
            this.lbTrangThai.AutoSize = true;
            this.lbTrangThai.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTrangThai.Location = new System.Drawing.Point(24, 461);
            this.lbTrangThai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTrangThai.Name = "lbTrangThai";
            this.lbTrangThai.Size = new System.Drawing.Size(82, 20);
            this.lbTrangThai.TabIndex = 61;
            this.lbTrangThai.Text = "Trạng thái :";
            // 
            // lbEmail
            // 
            this.lbEmail.AutoSize = true;
            this.lbEmail.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmail.Location = new System.Drawing.Point(24, 411);
            this.lbEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(53, 20);
            this.lbEmail.TabIndex = 58;
            this.lbEmail.Text = "Email :";
            // 
            // lbSdt
            // 
            this.lbSdt.AutoSize = true;
            this.lbSdt.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSdt.Location = new System.Drawing.Point(24, 363);
            this.lbSdt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSdt.Name = "lbSdt";
            this.lbSdt.Size = new System.Drawing.Size(104, 20);
            this.lbSdt.TabIndex = 57;
            this.lbSdt.Text = "Số điện thoại :";
            // 
            // dateTimePickerNgaySinh
            // 
            this.dateTimePickerNgaySinh.BorderRadius = 5;
            this.dateTimePickerNgaySinh.Checked = true;
            this.dateTimePickerNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerNgaySinh.FillColor = System.Drawing.Color.White;
            this.dateTimePickerNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerNgaySinh.Location = new System.Drawing.Point(146, 268);
            this.dateTimePickerNgaySinh.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerNgaySinh.Name = "dateTimePickerNgaySinh";
            this.dateTimePickerNgaySinh.Size = new System.Drawing.Size(242, 29);
            this.dateTimePickerNgaySinh.TabIndex = 56;
            this.dateTimePickerNgaySinh.Value = new System.DateTime(2025, 10, 9, 22, 44, 15, 114);
            // 
            // lbGioiTinh
            // 
            this.lbGioiTinh.AutoSize = true;
            this.lbGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGioiTinh.Location = new System.Drawing.Point(24, 320);
            this.lbGioiTinh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbGioiTinh.Name = "lbGioiTinh";
            this.lbGioiTinh.Size = new System.Drawing.Size(72, 20);
            this.lbGioiTinh.TabIndex = 55;
            this.lbGioiTinh.Text = "Giới tính :";
            // 
            // lbNgaySinh
            // 
            this.lbNgaySinh.AutoSize = true;
            this.lbNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNgaySinh.Location = new System.Drawing.Point(24, 268);
            this.lbNgaySinh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNgaySinh.Name = "lbNgaySinh";
            this.lbNgaySinh.Size = new System.Drawing.Size(81, 20);
            this.lbNgaySinh.TabIndex = 54;
            this.lbNgaySinh.Text = "Ngày sinh :";
            // 
            // lbHovaTen
            // 
            this.lbHovaTen.AutoSize = true;
            this.lbHovaTen.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHovaTen.Location = new System.Drawing.Point(24, 222);
            this.lbHovaTen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHovaTen.Name = "lbHovaTen";
            this.lbHovaTen.Size = new System.Drawing.Size(82, 20);
            this.lbHovaTen.TabIndex = 52;
            this.lbHovaTen.Text = "Họ và Tên :";
            // 
            // cbPhuHuynh
            // 
            this.cbPhuHuynh.BackColor = System.Drawing.Color.Transparent;
            this.cbPhuHuynh.BorderRadius = 5;
            this.cbPhuHuynh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPhuHuynh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhuHuynh.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbPhuHuynh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbPhuHuynh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPhuHuynh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbPhuHuynh.ItemHeight = 30;
            this.cbPhuHuynh.Items.AddRange(new object[] {
            "Chọn phụ huynh",
            "PH01 - Nguyễn Văn Tèo",
            "PH02 - Nguyễn Đình C",
            "PH03 - Huỳnh Mĩ A",
            "PH04 - Bùi Trường C"});
            this.cbPhuHuynh.Location = new System.Drawing.Point(228, 90);
            this.cbPhuHuynh.Margin = new System.Windows.Forms.Padding(2);
            this.cbPhuHuynh.Name = "cbPhuHuynh";
            this.cbPhuHuynh.Size = new System.Drawing.Size(160, 36);
            this.cbPhuHuynh.StartIndex = 0;
            this.cbPhuHuynh.TabIndex = 51;
            // 
            // cbLop
            // 
            this.cbLop.BackColor = System.Drawing.Color.Transparent;
            this.cbLop.BorderRadius = 5;
            this.cbLop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLop.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLop.ItemHeight = 30;
            this.cbLop.Items.AddRange(new object[] {
            "Chọn lớp học",
            "Lớp 6A1",
            "Lớp 7A2"});
            this.cbLop.Location = new System.Drawing.Point(28, 150);
            this.cbLop.Margin = new System.Windows.Forms.Padding(2);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(168, 36);
            this.cbLop.StartIndex = 0;
            this.cbLop.TabIndex = 50;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 57);
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
            this.btnChinhSuaHocSinh.Location = new System.Drawing.Point(266, 502);
            this.btnChinhSuaHocSinh.Margin = new System.Windows.Forms.Padding(2);
            this.btnChinhSuaHocSinh.Name = "btnChinhSuaHocSinh";
            this.btnChinhSuaHocSinh.Size = new System.Drawing.Size(122, 29);
            this.btnChinhSuaHocSinh.TabIndex = 63;
            this.btnChinhSuaHocSinh.Text = "Sửa học sinh";
            this.btnChinhSuaHocSinh.Click += new System.EventHandler(this.btnChinhSuaHocSinh_Click);
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
            this.txtEmail.Location = new System.Drawing.Point(146, 411);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Nhập Email";
            this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(242, 32);
            this.txtEmail.TabIndex = 60;
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
            this.txtSoDienThoai.Location = new System.Drawing.Point(146, 357);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PlaceholderText = "Nhập số điện thoại";
            this.txtSoDienThoai.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(242, 32);
            this.txtSoDienThoai.TabIndex = 59;
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
            this.txtHovaTen.Location = new System.Drawing.Point(146, 219);
            this.txtHovaTen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtHovaTen.Name = "txtHovaTen";
            this.txtHovaTen.PlaceholderText = "Nhập Họ và Tên ...";
            this.txtHovaTen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHovaTen.SelectedText = "";
            this.txtHovaTen.Size = new System.Drawing.Size(242, 32);
            this.txtHovaTen.TabIndex = 53;
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
            "Chọn Học Kì",
            "Học Kỳ I - 2023 - 2024",
            "Học Kỳ II - 2023 - 2024",
            "Học Kỳ I - 2024 - 2025",
            "Học Kỳ II - 2024 - 2025"});
            this.cbHocKyNamHoc.Location = new System.Drawing.Point(28, 90);
            this.cbHocKyNamHoc.Margin = new System.Windows.Forms.Padding(2);
            this.cbHocKyNamHoc.Name = "cbHocKyNamHoc";
            this.cbHocKyNamHoc.Size = new System.Drawing.Size(188, 36);
            this.cbHocKyNamHoc.StartIndex = 0;
            this.cbHocKyNamHoc.TabIndex = 67;
            // 
            // ChinhSuaHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 542);
            this.ControlBox = false;
            this.Controls.Add(this.cbHocKyNamHoc);
            this.Controls.Add(this.cbMoiQuanHe);
            this.Controls.Add(this.groupBoxGioiTinh);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnChinhSuaHocSinh);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.lbTrangThai);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lbEmail);
            this.Controls.Add(this.lbSdt);
            this.Controls.Add(this.dateTimePickerNgaySinh);
            this.Controls.Add(this.lbGioiTinh);
            this.Controls.Add(this.lbNgaySinh);
            this.Controls.Add(this.txtHovaTen);
            this.Controls.Add(this.lbHovaTen);
            this.Controls.Add(this.cbPhuHuynh);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(427, 581);
            this.MinimumSize = new System.Drawing.Size(427, 581);
            this.Name = "ChinhSuaHocSinh";
            this.Text = "ChinhSuaHocSinh";
            this.Load += new System.EventHandler(this.ChinhSuaHocSinh_Load);
            this.groupBoxGioiTinh.ResumeLayout(false);
            this.groupBoxGioiTinh.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ComboBox cbMoiQuanHe;
        private System.Windows.Forms.GroupBox groupBoxGioiTinh;
        private Guna.UI2.WinForms.Guna2RadioButton rbNu;
        private Guna.UI2.WinForms.Guna2RadioButton rbNam;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnChinhSuaHocSinh;
        private Guna.UI2.WinForms.Guna2TextBox txtTrangThai;
        private System.Windows.Forms.Label lbTrangThai;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtSoDienThoai;
        private System.Windows.Forms.Label lbEmail;
        private System.Windows.Forms.Label lbSdt;
        private Guna.UI2.WinForms.Guna2DateTimePicker dateTimePickerNgaySinh;
        private System.Windows.Forms.Label lbGioiTinh;
        private System.Windows.Forms.Label lbNgaySinh;
        private Guna.UI2.WinForms.Guna2TextBox txtHovaTen;
        private System.Windows.Forms.Label lbHovaTen;
        private Guna.UI2.WinForms.Guna2ComboBox cbPhuHuynh;
        private Guna.UI2.WinForms.Guna2ComboBox cbLop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocKyNamHoc;
    }
}