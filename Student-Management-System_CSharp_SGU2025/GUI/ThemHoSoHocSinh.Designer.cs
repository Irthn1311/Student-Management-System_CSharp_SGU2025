namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ThemHoSoHocSinh
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PbNguoiDung = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.groupBoxGioiTinh = new System.Windows.Forms.GroupBox();
            this.rbNu = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbNam = new Guna.UI2.WinForms.Guna2RadioButton();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemHocSinh = new Guna.UI2.WinForms.Guna2Button();
            this.txtTrangThai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbTrangThai = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbEmail = new System.Windows.Forms.Label();
            this.lbSdt = new System.Windows.Forms.Label();
            this.dateTimePickerNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lbGioiTinh = new System.Windows.Forms.Label();
            this.lbNgaySinh = new System.Windows.Forms.Label();
            this.txtHovaTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbHovaTen = new System.Windows.Forms.Label();
            this.cbPhuHuynh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbNguoiDung)).BeginInit();
            this.groupBoxGioiTinh.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 70);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thêm học sinh";
            // 
            // PbNguoiDung
            // 
            this.PbNguoiDung.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PbNguoiDung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbNguoiDung.FillColor = System.Drawing.Color.Transparent;
            this.PbNguoiDung.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.arrow;
            this.PbNguoiDung.ImageRotate = 0F;
            this.PbNguoiDung.Location = new System.Drawing.Point(22, 96);
            this.PbNguoiDung.Name = "PbNguoiDung";
            this.PbNguoiDung.ShadowDecoration.BorderRadius = 50;
            this.PbNguoiDung.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.PbNguoiDung.Size = new System.Drawing.Size(132, 132);
            this.PbNguoiDung.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbNguoiDung.TabIndex = 10;
            this.PbNguoiDung.TabStop = false;
            this.PbNguoiDung.Click += new System.EventHandler(this.PbNguoiDung_Click);
            // 
            // groupBoxGioiTinh
            // 
            this.groupBoxGioiTinh.Controls.Add(this.rbNu);
            this.groupBoxGioiTinh.Controls.Add(this.rbNam);
            this.groupBoxGioiTinh.Location = new System.Drawing.Point(194, 360);
            this.groupBoxGioiTinh.Name = "groupBoxGioiTinh";
            this.groupBoxGioiTinh.Size = new System.Drawing.Size(179, 60);
            this.groupBoxGioiTinh.TabIndex = 47;
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
            this.rbNu.Location = new System.Drawing.Point(109, 18);
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
            this.rbNam.Location = new System.Drawing.Point(19, 18);
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
            this.btnHuy.Location = new System.Drawing.Point(265, 606);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(71, 36);
            this.btnHuy.TabIndex = 46;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnThemHocSinh
            // 
            this.btnThemHocSinh.BorderRadius = 7;
            this.btnThemHocSinh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemHocSinh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemHocSinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemHocSinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemHocSinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemHocSinh.ForeColor = System.Drawing.Color.White;
            this.btnThemHocSinh.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus;
            this.btnThemHocSinh.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThemHocSinh.Location = new System.Drawing.Point(355, 606);
            this.btnThemHocSinh.Name = "btnThemHocSinh";
            this.btnThemHocSinh.Size = new System.Drawing.Size(162, 36);
            this.btnThemHocSinh.TabIndex = 45;
            this.btnThemHocSinh.Text = "Thêm học sinh";
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
            this.txtTrangThai.Location = new System.Drawing.Point(194, 555);
            this.txtTrangThai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.PlaceholderText = "";
            this.txtTrangThai.ReadOnly = true;
            this.txtTrangThai.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTrangThai.SelectedText = "";
            this.txtTrangThai.Size = new System.Drawing.Size(323, 36);
            this.txtTrangThai.TabIndex = 44;
            // 
            // lbTrangThai
            // 
            this.lbTrangThai.AutoSize = true;
            this.lbTrangThai.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTrangThai.Location = new System.Drawing.Point(32, 555);
            this.lbTrangThai.Name = "lbTrangThai";
            this.lbTrangThai.Size = new System.Drawing.Size(82, 20);
            this.lbTrangThai.TabIndex = 43;
            this.lbTrangThai.Text = "Trạng thái :";
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
            this.txtEmail.Location = new System.Drawing.Point(194, 493);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Nhập Email";
            this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(323, 40);
            this.txtEmail.TabIndex = 42;
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
            this.txtSoDienThoai.Location = new System.Drawing.Point(194, 427);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PlaceholderText = "Nhập số điện thoại";
            this.txtSoDienThoai.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(323, 40);
            this.txtSoDienThoai.TabIndex = 41;
            // 
            // lbEmail
            // 
            this.lbEmail.AutoSize = true;
            this.lbEmail.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmail.Location = new System.Drawing.Point(32, 493);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(53, 20);
            this.lbEmail.TabIndex = 40;
            this.lbEmail.Text = "Email :";
            // 
            // lbSdt
            // 
            this.lbSdt.AutoSize = true;
            this.lbSdt.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSdt.Location = new System.Drawing.Point(32, 435);
            this.lbSdt.Name = "lbSdt";
            this.lbSdt.Size = new System.Drawing.Size(104, 20);
            this.lbSdt.TabIndex = 39;
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
            this.dateTimePickerNgaySinh.Location = new System.Drawing.Point(194, 318);
            this.dateTimePickerNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerNgaySinh.Name = "dateTimePickerNgaySinh";
            this.dateTimePickerNgaySinh.Size = new System.Drawing.Size(323, 36);
            this.dateTimePickerNgaySinh.TabIndex = 38;
            this.dateTimePickerNgaySinh.Value = new System.DateTime(2025, 10, 9, 22, 44, 15, 114);
            // 
            // lbGioiTinh
            // 
            this.lbGioiTinh.AutoSize = true;
            this.lbGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGioiTinh.Location = new System.Drawing.Point(32, 381);
            this.lbGioiTinh.Name = "lbGioiTinh";
            this.lbGioiTinh.Size = new System.Drawing.Size(72, 20);
            this.lbGioiTinh.TabIndex = 37;
            this.lbGioiTinh.Text = "Giới tính :";
            // 
            // lbNgaySinh
            // 
            this.lbNgaySinh.AutoSize = true;
            this.lbNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNgaySinh.Location = new System.Drawing.Point(32, 318);
            this.lbNgaySinh.Name = "lbNgaySinh";
            this.lbNgaySinh.Size = new System.Drawing.Size(81, 20);
            this.lbNgaySinh.TabIndex = 36;
            this.lbNgaySinh.Text = "Ngày sinh :";
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
            this.txtHovaTen.Location = new System.Drawing.Point(194, 257);
            this.txtHovaTen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHovaTen.Name = "txtHovaTen";
            this.txtHovaTen.PlaceholderText = "Nhập Họ và Tên ...";
            this.txtHovaTen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtHovaTen.SelectedText = "";
            this.txtHovaTen.Size = new System.Drawing.Size(323, 40);
            this.txtHovaTen.TabIndex = 35;
            // 
            // lbHovaTen
            // 
            this.lbHovaTen.AutoSize = true;
            this.lbHovaTen.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHovaTen.Location = new System.Drawing.Point(32, 261);
            this.lbHovaTen.Name = "lbHovaTen";
            this.lbHovaTen.Size = new System.Drawing.Size(82, 20);
            this.lbHovaTen.TabIndex = 34;
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
            this.cbPhuHuynh.Location = new System.Drawing.Point(213, 183);
            this.cbPhuHuynh.Name = "cbPhuHuynh";
            this.cbPhuHuynh.Size = new System.Drawing.Size(304, 36);
            this.cbPhuHuynh.StartIndex = 0;
            this.cbPhuHuynh.TabIndex = 33;
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
            this.cbLop.Location = new System.Drawing.Point(213, 114);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(304, 36);
            this.cbLop.StartIndex = 0;
            this.cbLop.TabIndex = 32;
            // 
            // ThemHoSoHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 667);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxGioiTinh);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnThemHocSinh);
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
            this.Controls.Add(this.PbNguoiDung);
            this.Controls.Add(this.panel1);
            this.Name = "ThemHoSoHocSinh";
            this.Text = "ThemHoSoHocSinh";
            this.Load += new System.EventHandler(this.ThemHoSoHocSinh_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbNguoiDung)).EndInit();
            this.groupBoxGioiTinh.ResumeLayout(false);
            this.groupBoxGioiTinh.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        public Guna.UI2.WinForms.Guna2CirclePictureBox PbNguoiDung;
        private System.Windows.Forms.GroupBox groupBoxGioiTinh;
        private Guna.UI2.WinForms.Guna2RadioButton rbNu;
        private Guna.UI2.WinForms.Guna2RadioButton rbNam;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnThemHocSinh;
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
    }
}