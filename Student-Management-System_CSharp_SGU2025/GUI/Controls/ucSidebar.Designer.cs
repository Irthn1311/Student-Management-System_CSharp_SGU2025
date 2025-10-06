namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ucSidebar
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
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAppDescription = new System.Windows.Forms.Label();
            this.picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblAppName = new System.Windows.Forms.Label();
            this.flpnlNav = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBangTin = new Guna.UI2.WinForms.Guna2Button();
            this.btnLopHoc = new Guna.UI2.WinForms.Guna2Button();
            this.btnMonHoc = new Guna.UI2.WinForms.Guna2Button();
            this.btnNamHoc = new Guna.UI2.WinForms.Guna2Button();
            this.btnPhanCong = new Guna.UI2.WinForms.Guna2Button();
            this.btnHocSinh = new Guna.UI2.WinForms.Guna2Button();
            this.btnDiemSo = new Guna.UI2.WinForms.Guna2Button();
            this.btnHanhKiem = new Guna.UI2.WinForms.Guna2Button();
            this.btnKhenThuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnXepLoai = new Guna.UI2.WinForms.Guna2Button();
            this.btnGiaoVien = new Guna.UI2.WinForms.Guna2Button();
            this.btnThoiKhoaBieu = new Guna.UI2.WinForms.Guna2Button();
            this.btnBaoCao = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaiKhoan = new Guna.UI2.WinForms.Guna2Button();
            this.btnCaiDat = new Guna.UI2.WinForms.Guna2Button();
            this.pnlLogout = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.sprtLogout = new Guna.UI2.WinForms.Guna2Separator();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.flpnlNav.SuspendLayout();
            this.pnlLogout.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.BorderRadius = 20;
            this.pnlHeader.Controls.Add(this.lblAppDescription);
            this.pnlHeader.Controls.Add(this.picLogo);
            this.pnlHeader.Controls.Add(this.lblAppName);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24);
            this.pnlHeader.Size = new System.Drawing.Size(256, 93);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // lblAppDescription
            // 
            this.lblAppDescription.AutoSize = true;
            this.lblAppDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblAppDescription.Location = new System.Drawing.Point(75, 51);
            this.lblAppDescription.Name = "lblAppDescription";
            this.lblAppDescription.Size = new System.Drawing.Size(118, 18);
            this.lblAppDescription.TabIndex = 3;
            this.lblAppDescription.Text = "Quản lý học sinh";
            this.lblAppDescription.Click += new System.EventHandler(this.label1_Click);
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.BorderRadius = 8;
            this.picLogo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.picLogo.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.education;
            this.picLogo.ImageRotate = 0F;
            this.picLogo.Location = new System.Drawing.Point(27, 26);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(40, 40);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppName.Location = new System.Drawing.Point(73, 20);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(158, 29);
            this.lblAppName.TabIndex = 2;
            this.lblAppName.Text = "QLHS THPT";
            // 
            // flpnlNav
            // 
            this.flpnlNav.AutoScroll = true;
            this.flpnlNav.Controls.Add(this.btnBangTin);
            this.flpnlNav.Controls.Add(this.btnLopHoc);
            this.flpnlNav.Controls.Add(this.btnMonHoc);
            this.flpnlNav.Controls.Add(this.btnNamHoc);
            this.flpnlNav.Controls.Add(this.btnPhanCong);
            this.flpnlNav.Controls.Add(this.btnHocSinh);
            this.flpnlNav.Controls.Add(this.btnDiemSo);
            this.flpnlNav.Controls.Add(this.btnHanhKiem);
            this.flpnlNav.Controls.Add(this.btnKhenThuong);
            this.flpnlNav.Controls.Add(this.btnXepLoai);
            this.flpnlNav.Controls.Add(this.btnGiaoVien);
            this.flpnlNav.Controls.Add(this.btnThoiKhoaBieu);
            this.flpnlNav.Controls.Add(this.btnBaoCao);
            this.flpnlNav.Controls.Add(this.btnTaiKhoan);
            this.flpnlNav.Controls.Add(this.btnCaiDat);
            this.flpnlNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpnlNav.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpnlNav.Location = new System.Drawing.Point(0, 93);
            this.flpnlNav.Name = "flpnlNav";
            this.flpnlNav.Padding = new System.Windows.Forms.Padding(12, 16, 12, 16);
            this.flpnlNav.Size = new System.Drawing.Size(256, 807);
            this.flpnlNav.TabIndex = 1;
            this.flpnlNav.WrapContents = false;
            this.flpnlNav.Paint += new System.Windows.Forms.PaintEventHandler(this.flpnlNav_Paint);
            // 
            // btnBangTin
            // 
            this.btnBangTin.BorderRadius = 8;
            this.btnBangTin.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnBangTin.Checked = true;
            this.btnBangTin.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnBangTin.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnBangTin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBangTin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBangTin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBangTin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBangTin.FillColor = System.Drawing.Color.Transparent;
            this.btnBangTin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBangTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnBangTin.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnBangTin.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.home;
            this.btnBangTin.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBangTin.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnBangTin.Location = new System.Drawing.Point(12, 16);
            this.btnBangTin.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnBangTin.Name = "btnBangTin";
            this.btnBangTin.Size = new System.Drawing.Size(231, 40);
            this.btnBangTin.TabIndex = 0;
            this.btnBangTin.Text = "Bảng tin";
            this.btnBangTin.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBangTin.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnLopHoc
            // 
            this.btnLopHoc.BorderRadius = 8;
            this.btnLopHoc.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnLopHoc.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnLopHoc.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnLopHoc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLopHoc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLopHoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLopHoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLopHoc.FillColor = System.Drawing.Color.Transparent;
            this.btnLopHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLopHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnLopHoc.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnLopHoc.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.peoplethree;
            this.btnLopHoc.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLopHoc.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnLopHoc.Location = new System.Drawing.Point(12, 60);
            this.btnLopHoc.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnLopHoc.Name = "btnLopHoc";
            this.btnLopHoc.Size = new System.Drawing.Size(231, 40);
            this.btnLopHoc.TabIndex = 1;
            this.btnLopHoc.Text = "Lớp học";
            this.btnLopHoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLopHoc.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnMonHoc
            // 
            this.btnMonHoc.BorderRadius = 8;
            this.btnMonHoc.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnMonHoc.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnMonHoc.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnMonHoc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMonHoc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMonHoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMonHoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMonHoc.FillColor = System.Drawing.Color.Transparent;
            this.btnMonHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnMonHoc.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnMonHoc.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.book;
            this.btnMonHoc.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMonHoc.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnMonHoc.Location = new System.Drawing.Point(12, 104);
            this.btnMonHoc.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnMonHoc.Name = "btnMonHoc";
            this.btnMonHoc.Size = new System.Drawing.Size(231, 40);
            this.btnMonHoc.TabIndex = 2;
            this.btnMonHoc.Text = "Môn học";
            this.btnMonHoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnMonHoc.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnNamHoc
            // 
            this.btnNamHoc.BorderRadius = 8;
            this.btnNamHoc.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnNamHoc.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnNamHoc.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnNamHoc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNamHoc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNamHoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNamHoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNamHoc.FillColor = System.Drawing.Color.Transparent;
            this.btnNamHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNamHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnNamHoc.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnNamHoc.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.calendar;
            this.btnNamHoc.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNamHoc.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnNamHoc.Location = new System.Drawing.Point(12, 148);
            this.btnNamHoc.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnNamHoc.Name = "btnNamHoc";
            this.btnNamHoc.Size = new System.Drawing.Size(231, 40);
            this.btnNamHoc.TabIndex = 3;
            this.btnNamHoc.Text = "Năm học";
            this.btnNamHoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNamHoc.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnPhanCong
            // 
            this.btnPhanCong.BorderRadius = 8;
            this.btnPhanCong.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnPhanCong.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnPhanCong.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnPhanCong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPhanCong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPhanCong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPhanCong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPhanCong.FillColor = System.Drawing.Color.Transparent;
            this.btnPhanCong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhanCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnPhanCong.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnPhanCong.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.list;
            this.btnPhanCong.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnPhanCong.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnPhanCong.Location = new System.Drawing.Point(12, 192);
            this.btnPhanCong.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnPhanCong.Name = "btnPhanCong";
            this.btnPhanCong.Size = new System.Drawing.Size(231, 40);
            this.btnPhanCong.TabIndex = 4;
            this.btnPhanCong.Text = "Phân công";
            this.btnPhanCong.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnPhanCong.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnHocSinh
            // 
            this.btnHocSinh.BorderRadius = 8;
            this.btnHocSinh.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnHocSinh.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnHocSinh.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnHocSinh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHocSinh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHocSinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHocSinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHocSinh.FillColor = System.Drawing.Color.Transparent;
            this.btnHocSinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHocSinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnHocSinh.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnHocSinh.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.student_2;
            this.btnHocSinh.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHocSinh.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnHocSinh.Location = new System.Drawing.Point(12, 236);
            this.btnHocSinh.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnHocSinh.Name = "btnHocSinh";
            this.btnHocSinh.Size = new System.Drawing.Size(231, 40);
            this.btnHocSinh.TabIndex = 5;
            this.btnHocSinh.Text = "Học sinh";
            this.btnHocSinh.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHocSinh.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnDiemSo
            // 
            this.btnDiemSo.BorderRadius = 8;
            this.btnDiemSo.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDiemSo.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnDiemSo.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnDiemSo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDiemSo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDiemSo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDiemSo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDiemSo.FillColor = System.Drawing.Color.Transparent;
            this.btnDiemSo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiemSo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnDiemSo.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnDiemSo.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.notes;
            this.btnDiemSo.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDiemSo.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnDiemSo.Location = new System.Drawing.Point(12, 280);
            this.btnDiemSo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnDiemSo.Name = "btnDiemSo";
            this.btnDiemSo.Size = new System.Drawing.Size(231, 40);
            this.btnDiemSo.TabIndex = 6;
            this.btnDiemSo.Text = "Điểm số";
            this.btnDiemSo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDiemSo.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnHanhKiem
            // 
            this.btnHanhKiem.BorderRadius = 8;
            this.btnHanhKiem.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnHanhKiem.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnHanhKiem.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnHanhKiem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHanhKiem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHanhKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHanhKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHanhKiem.FillColor = System.Drawing.Color.Transparent;
            this.btnHanhKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHanhKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnHanhKiem.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnHanhKiem.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.badge;
            this.btnHanhKiem.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHanhKiem.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnHanhKiem.Location = new System.Drawing.Point(12, 324);
            this.btnHanhKiem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnHanhKiem.Name = "btnHanhKiem";
            this.btnHanhKiem.Size = new System.Drawing.Size(231, 40);
            this.btnHanhKiem.TabIndex = 7;
            this.btnHanhKiem.Text = "Hạnh kiểm";
            this.btnHanhKiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnHanhKiem.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnKhenThuong
            // 
            this.btnKhenThuong.BorderRadius = 8;
            this.btnKhenThuong.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnKhenThuong.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnKhenThuong.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnKhenThuong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKhenThuong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKhenThuong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKhenThuong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKhenThuong.FillColor = System.Drawing.Color.Transparent;
            this.btnKhenThuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhenThuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnKhenThuong.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnKhenThuong.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.medal;
            this.btnKhenThuong.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKhenThuong.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnKhenThuong.Location = new System.Drawing.Point(12, 368);
            this.btnKhenThuong.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnKhenThuong.Name = "btnKhenThuong";
            this.btnKhenThuong.Size = new System.Drawing.Size(231, 40);
            this.btnKhenThuong.TabIndex = 8;
            this.btnKhenThuong.Text = "Khen thưởng";
            this.btnKhenThuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKhenThuong.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnXepLoai
            // 
            this.btnXepLoai.BorderRadius = 8;
            this.btnXepLoai.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnXepLoai.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnXepLoai.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnXepLoai.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXepLoai.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXepLoai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXepLoai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXepLoai.FillColor = System.Drawing.Color.Transparent;
            this.btnXepLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXepLoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnXepLoai.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnXepLoai.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.statistics;
            this.btnXepLoai.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnXepLoai.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnXepLoai.Location = new System.Drawing.Point(12, 412);
            this.btnXepLoai.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnXepLoai.Name = "btnXepLoai";
            this.btnXepLoai.Size = new System.Drawing.Size(231, 40);
            this.btnXepLoai.TabIndex = 9;
            this.btnXepLoai.Text = "Xếp loại";
            this.btnXepLoai.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnXepLoai.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnGiaoVien
            // 
            this.btnGiaoVien.BorderRadius = 8;
            this.btnGiaoVien.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnGiaoVien.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnGiaoVien.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnGiaoVien.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGiaoVien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGiaoVien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGiaoVien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGiaoVien.FillColor = System.Drawing.Color.Transparent;
            this.btnGiaoVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiaoVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnGiaoVien.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnGiaoVien.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.people;
            this.btnGiaoVien.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnGiaoVien.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnGiaoVien.Location = new System.Drawing.Point(12, 456);
            this.btnGiaoVien.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnGiaoVien.Name = "btnGiaoVien";
            this.btnGiaoVien.Size = new System.Drawing.Size(231, 40);
            this.btnGiaoVien.TabIndex = 10;
            this.btnGiaoVien.Text = "Giáo viên";
            this.btnGiaoVien.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnGiaoVien.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnThoiKhoaBieu
            // 
            this.btnThoiKhoaBieu.BorderRadius = 8;
            this.btnThoiKhoaBieu.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnThoiKhoaBieu.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnThoiKhoaBieu.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnThoiKhoaBieu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThoiKhoaBieu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThoiKhoaBieu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThoiKhoaBieu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThoiKhoaBieu.FillColor = System.Drawing.Color.Transparent;
            this.btnThoiKhoaBieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoiKhoaBieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnThoiKhoaBieu.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnThoiKhoaBieu.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.calendar;
            this.btnThoiKhoaBieu.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnThoiKhoaBieu.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnThoiKhoaBieu.Location = new System.Drawing.Point(12, 500);
            this.btnThoiKhoaBieu.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnThoiKhoaBieu.Name = "btnThoiKhoaBieu";
            this.btnThoiKhoaBieu.Size = new System.Drawing.Size(231, 40);
            this.btnThoiKhoaBieu.TabIndex = 11;
            this.btnThoiKhoaBieu.Text = "Thời khóa biểu";
            this.btnThoiKhoaBieu.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnThoiKhoaBieu.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.BorderRadius = 8;
            this.btnBaoCao.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnBaoCao.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnBaoCao.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnBaoCao.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBaoCao.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBaoCao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBaoCao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBaoCao.FillColor = System.Drawing.Color.Transparent;
            this.btnBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnBaoCao.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnBaoCao.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.statistics;
            this.btnBaoCao.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBaoCao.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnBaoCao.Location = new System.Drawing.Point(12, 544);
            this.btnBaoCao.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(231, 40);
            this.btnBaoCao.TabIndex = 12;
            this.btnBaoCao.Text = "Báo cáo";
            this.btnBaoCao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBaoCao.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.BorderRadius = 8;
            this.btnTaiKhoan.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnTaiKhoan.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnTaiKhoan.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnTaiKhoan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaiKhoan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaiKhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaiKhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaiKhoan.FillColor = System.Drawing.Color.Transparent;
            this.btnTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnTaiKhoan.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnTaiKhoan.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.user;
            this.btnTaiKhoan.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTaiKhoan.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnTaiKhoan.Location = new System.Drawing.Point(12, 588);
            this.btnTaiKhoan.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Size = new System.Drawing.Size(231, 40);
            this.btnTaiKhoan.TabIndex = 13;
            this.btnTaiKhoan.Text = "Tài khoản";
            this.btnTaiKhoan.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTaiKhoan.TextOffset = new System.Drawing.Point(20, 0);
            this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.BorderRadius = 8;
            this.btnCaiDat.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnCaiDat.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnCaiDat.CheckedState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnCaiDat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCaiDat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCaiDat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCaiDat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCaiDat.FillColor = System.Drawing.Color.Transparent;
            this.btnCaiDat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaiDat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCaiDat.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnCaiDat.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.setting;
            this.btnCaiDat.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCaiDat.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnCaiDat.Location = new System.Drawing.Point(12, 632);
            this.btnCaiDat.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Size = new System.Drawing.Size(231, 40);
            this.btnCaiDat.TabIndex = 14;
            this.btnCaiDat.Text = "Cài đặt";
            this.btnCaiDat.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCaiDat.TextOffset = new System.Drawing.Point(20, 0);
            this.btnCaiDat.Click += new System.EventHandler(this.guna2Button13_Click);
            // 
            // pnlLogout
            // 
            this.pnlLogout.Controls.Add(this.btnLogout);
            this.pnlLogout.Controls.Add(this.sprtLogout);
            this.pnlLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLogout.Location = new System.Drawing.Point(0, 835);
            this.pnlLogout.Name = "pnlLogout";
            this.pnlLogout.Padding = new System.Windows.Forms.Padding(12);
            this.pnlLogout.Size = new System.Drawing.Size(256, 65);
            this.pnlLogout.TabIndex = 2;
            // 
            // btnLogout
            // 
            this.btnLogout.BorderRadius = 8;
            this.btnLogout.Checked = true;
            this.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogout.FillColor = System.Drawing.Color.Transparent;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnLogout.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnLogout.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.logout;
            this.btnLogout.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLogout.ImageOffset = new System.Drawing.Point(12, 0);
            this.btnLogout.Location = new System.Drawing.Point(12, 22);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(232, 31);
            this.btnLogout.TabIndex = 15;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLogout.TextOffset = new System.Drawing.Point(20, 0);
            // 
            // sprtLogout
            // 
            this.sprtLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.sprtLogout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.sprtLogout.Location = new System.Drawing.Point(12, 12);
            this.sprtLogout.Name = "sprtLogout";
            this.sprtLogout.Size = new System.Drawing.Size(232, 10);
            this.sprtLogout.TabIndex = 0;
            // 
            // ucSidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlLogout);
            this.Controls.Add(this.flpnlNav);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucSidebar";
            this.Size = new System.Drawing.Size(256, 900);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.flpnlNav.ResumeLayout(false);
            this.pnlLogout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblAppDescription;
        private Guna.UI2.WinForms.Guna2PictureBox picLogo;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.FlowLayoutPanel flpnlNav;
        private Guna.UI2.WinForms.Guna2Button btnBangTin;
        private Guna.UI2.WinForms.Guna2Button btnLopHoc;
        private Guna.UI2.WinForms.Guna2Button btnMonHoc;
        private Guna.UI2.WinForms.Guna2Button btnNamHoc;
        private Guna.UI2.WinForms.Guna2Button btnPhanCong;
        private Guna.UI2.WinForms.Guna2Button btnHocSinh;
        private Guna.UI2.WinForms.Guna2Button btnDiemSo;
        private Guna.UI2.WinForms.Guna2Button btnHanhKiem;
        private Guna.UI2.WinForms.Guna2Button btnKhenThuong;
        private Guna.UI2.WinForms.Guna2Button btnXepLoai;
        private Guna.UI2.WinForms.Guna2Button btnGiaoVien;
        private Guna.UI2.WinForms.Guna2Button btnThoiKhoaBieu;
        private Guna.UI2.WinForms.Guna2Button btnBaoCao;
        private Guna.UI2.WinForms.Guna2Button btnTaiKhoan;
        private Guna.UI2.WinForms.Guna2Button btnCaiDat;
        private Guna.UI2.WinForms.Guna2Panel pnlLogout;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private Guna.UI2.WinForms.Guna2Separator sprtLogout;
    }
}
