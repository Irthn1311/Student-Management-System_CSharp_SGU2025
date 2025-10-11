namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ucBaoCao
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
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlCards = new System.Windows.Forms.FlowLayoutPanel();
            this.cardBaoCaoHocSinh = new Guna.UI2.WinForms.Guna2Panel();
            this.lblBaoCaoHocSinhDesc = new System.Windows.Forms.Label();
            this.lblBaoCaoHocSinhTitle = new System.Windows.Forms.Label();
            this.pnlIconBaoCao = new Guna.UI2.WinForms.Guna2Panel();
            this.cardThongKeDiem = new Guna.UI2.WinForms.Guna2Panel();
            this.lblThongKeDiemDesc = new System.Windows.Forms.Label();
            this.lblThongKeDiemTitle = new System.Windows.Forms.Label();
            this.pnlIconThongKe = new Guna.UI2.WinForms.Guna2Panel();
            this.cardBaoCaoTongHop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblBaoCaoTongHopDesc = new System.Windows.Forms.Label();
            this.lblBaoCaoTongHopTitle = new System.Windows.Forms.Label();
            this.pnlIconTongHop = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.cboHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlTabs = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThongKeHocLuc = new Guna.UI2.WinForms.Guna2Button();
            this.btnBangDiem = new Guna.UI2.WinForms.Guna2Button();
            this.btnDanhSachLop = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlCards.SuspendLayout();
            this.cardBaoCaoHocSinh.SuspendLayout();
            this.cardThongKeDiem.SuspendLayout();
            this.cardBaoCaoTongHop.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(24);
            this.pnlMain.Size = new System.Drawing.Size(1200, 900);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Controls.Add(this.pnlCards);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(24, 93);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1152, 783);
            this.pnlContent.TabIndex = 1;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // pnlCards
            // 
            this.pnlCards.Controls.Add(this.cardBaoCaoHocSinh);
            this.pnlCards.Controls.Add(this.cardThongKeDiem);
            this.pnlCards.Controls.Add(this.cardBaoCaoTongHop);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(0, 0);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(1152, 121);
            this.pnlCards.TabIndex = 0;
            // 
            // cardBaoCaoHocSinh
            // 
            this.cardBaoCaoHocSinh.BackColor = System.Drawing.Color.Transparent;
            this.cardBaoCaoHocSinh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.cardBaoCaoHocSinh.BorderRadius = 12;
            this.cardBaoCaoHocSinh.BorderThickness = 1;
            this.cardBaoCaoHocSinh.Controls.Add(this.lblBaoCaoHocSinhDesc);
            this.cardBaoCaoHocSinh.Controls.Add(this.lblBaoCaoHocSinhTitle);
            this.cardBaoCaoHocSinh.Controls.Add(this.pnlIconBaoCao);
            this.cardBaoCaoHocSinh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cardBaoCaoHocSinh.Location = new System.Drawing.Point(3, 3);
            this.cardBaoCaoHocSinh.Margin = new System.Windows.Forms.Padding(3, 3, 12, 24);
            this.cardBaoCaoHocSinh.Name = "cardBaoCaoHocSinh";
            this.cardBaoCaoHocSinh.Padding = new System.Windows.Forms.Padding(25);
            this.cardBaoCaoHocSinh.ShadowDecoration.BorderRadius = 12;
            this.cardBaoCaoHocSinh.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(13)))));
            this.cardBaoCaoHocSinh.ShadowDecoration.Depth = 5;
            this.cardBaoCaoHocSinh.ShadowDecoration.Enabled = true;
            this.cardBaoCaoHocSinh.Size = new System.Drawing.Size(358, 97);
            this.cardBaoCaoHocSinh.TabIndex = 0;
            // 
            // lblBaoCaoHocSinhDesc
            // 
            this.lblBaoCaoHocSinhDesc.AutoSize = true;
            this.lblBaoCaoHocSinhDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblBaoCaoHocSinhDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblBaoCaoHocSinhDesc.Location = new System.Drawing.Point(89, 49);
            this.lblBaoCaoHocSinhDesc.Name = "lblBaoCaoHocSinhDesc";
            this.lblBaoCaoHocSinhDesc.Size = new System.Drawing.Size(87, 15);
            this.lblBaoCaoHocSinhDesc.TabIndex = 2;
            this.lblBaoCaoHocSinhDesc.Text = "1,247 học sinh";
            // 
            // lblBaoCaoHocSinhTitle
            // 
            this.lblBaoCaoHocSinhTitle.AutoSize = true;
            this.lblBaoCaoHocSinhTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblBaoCaoHocSinhTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblBaoCaoHocSinhTitle.Location = new System.Drawing.Point(89, 25);
            this.lblBaoCaoHocSinhTitle.Name = "lblBaoCaoHocSinhTitle";
            this.lblBaoCaoHocSinhTitle.Size = new System.Drawing.Size(133, 17);
            this.lblBaoCaoHocSinhTitle.TabIndex = 1;
            this.lblBaoCaoHocSinhTitle.Text = "Báo cáo học sinh";
            // 
            // pnlIconBaoCao
            // 
            this.pnlIconBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.pnlIconBaoCao.BorderRadius = 8;
            this.pnlIconBaoCao.Location = new System.Drawing.Point(25, 25);
            this.pnlIconBaoCao.Name = "pnlIconBaoCao";
            this.pnlIconBaoCao.Size = new System.Drawing.Size(48, 48);
            this.pnlIconBaoCao.TabIndex = 0;
            // 
            // cardThongKeDiem
            // 
            this.cardThongKeDiem.BackColor = System.Drawing.Color.Transparent;
            this.cardThongKeDiem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.cardThongKeDiem.BorderRadius = 12;
            this.cardThongKeDiem.BorderThickness = 1;
            this.cardThongKeDiem.Controls.Add(this.lblThongKeDiemDesc);
            this.cardThongKeDiem.Controls.Add(this.lblThongKeDiemTitle);
            this.cardThongKeDiem.Controls.Add(this.pnlIconThongKe);
            this.cardThongKeDiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cardThongKeDiem.Location = new System.Drawing.Point(376, 3);
            this.cardThongKeDiem.Margin = new System.Windows.Forms.Padding(3, 3, 12, 24);
            this.cardThongKeDiem.Name = "cardThongKeDiem";
            this.cardThongKeDiem.Padding = new System.Windows.Forms.Padding(25);
            this.cardThongKeDiem.ShadowDecoration.BorderRadius = 12;
            this.cardThongKeDiem.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(13)))));
            this.cardThongKeDiem.ShadowDecoration.Depth = 5;
            this.cardThongKeDiem.ShadowDecoration.Enabled = true;
            this.cardThongKeDiem.Size = new System.Drawing.Size(358, 97);
            this.cardThongKeDiem.TabIndex = 1;
            // 
            // lblThongKeDiemDesc
            // 
            this.lblThongKeDiemDesc.AutoSize = true;
            this.lblThongKeDiemDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblThongKeDiemDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblThongKeDiemDesc.Location = new System.Drawing.Point(89, 49);
            this.lblThongKeDiemDesc.Name = "lblThongKeDiemDesc";
            this.lblThongKeDiemDesc.Size = new System.Drawing.Size(106, 15);
            this.lblThongKeDiemDesc.TabIndex = 2;
            this.lblThongKeDiemDesc.Text = "Theo lớp/môn học";
            // 
            // lblThongKeDiemTitle
            // 
            this.lblThongKeDiemTitle.AutoSize = true;
            this.lblThongKeDiemTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblThongKeDiemTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblThongKeDiemTitle.Location = new System.Drawing.Point(89, 25);
            this.lblThongKeDiemTitle.Name = "lblThongKeDiemTitle";
            this.lblThongKeDiemTitle.Size = new System.Drawing.Size(115, 17);
            this.lblThongKeDiemTitle.TabIndex = 1;
            this.lblThongKeDiemTitle.Text = "Thống kê điểm";
            // 
            // pnlIconThongKe
            // 
            this.pnlIconThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.pnlIconThongKe.BorderRadius = 8;
            this.pnlIconThongKe.Location = new System.Drawing.Point(25, 25);
            this.pnlIconThongKe.Name = "pnlIconThongKe";
            this.pnlIconThongKe.Size = new System.Drawing.Size(48, 48);
            this.pnlIconThongKe.TabIndex = 0;
            // 
            // cardBaoCaoTongHop
            // 
            this.cardBaoCaoTongHop.BackColor = System.Drawing.Color.Transparent;
            this.cardBaoCaoTongHop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.cardBaoCaoTongHop.BorderRadius = 12;
            this.cardBaoCaoTongHop.BorderThickness = 1;
            this.cardBaoCaoTongHop.Controls.Add(this.lblBaoCaoTongHopDesc);
            this.cardBaoCaoTongHop.Controls.Add(this.lblBaoCaoTongHopTitle);
            this.cardBaoCaoTongHop.Controls.Add(this.pnlIconTongHop);
            this.cardBaoCaoTongHop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cardBaoCaoTongHop.Location = new System.Drawing.Point(749, 3);
            this.cardBaoCaoTongHop.Margin = new System.Windows.Forms.Padding(3, 3, 3, 24);
            this.cardBaoCaoTongHop.Name = "cardBaoCaoTongHop";
            this.cardBaoCaoTongHop.Padding = new System.Windows.Forms.Padding(25);
            this.cardBaoCaoTongHop.ShadowDecoration.BorderRadius = 12;
            this.cardBaoCaoTongHop.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(13)))));
            this.cardBaoCaoTongHop.ShadowDecoration.Depth = 5;
            this.cardBaoCaoTongHop.ShadowDecoration.Enabled = true;
            this.cardBaoCaoTongHop.Size = new System.Drawing.Size(358, 97);
            this.cardBaoCaoTongHop.TabIndex = 2;
            // 
            // lblBaoCaoTongHopDesc
            // 
            this.lblBaoCaoTongHopDesc.AutoSize = true;
            this.lblBaoCaoTongHopDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblBaoCaoTongHopDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblBaoCaoTongHopDesc.Location = new System.Drawing.Point(89, 49);
            this.lblBaoCaoTongHopDesc.Name = "lblBaoCaoTongHopDesc";
            this.lblBaoCaoTongHopDesc.Size = new System.Drawing.Size(73, 15);
            this.lblBaoCaoTongHopDesc.TabIndex = 2;
            this.lblBaoCaoTongHopDesc.Text = "Cả năm học";
            // 
            // lblBaoCaoTongHopTitle
            // 
            this.lblBaoCaoTongHopTitle.AutoSize = true;
            this.lblBaoCaoTongHopTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblBaoCaoTongHopTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblBaoCaoTongHopTitle.Location = new System.Drawing.Point(89, 25);
            this.lblBaoCaoTongHopTitle.Name = "lblBaoCaoTongHopTitle";
            this.lblBaoCaoTongHopTitle.Size = new System.Drawing.Size(136, 17);
            this.lblBaoCaoTongHopTitle.TabIndex = 1;
            this.lblBaoCaoTongHopTitle.Text = "Báo cáo tổng hợp";
            // 
            // pnlIconTongHop
            // 
            this.pnlIconTongHop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(213)))));
            this.pnlIconTongHop.BorderRadius = 8;
            this.pnlIconTongHop.Location = new System.Drawing.Point(25, 25);
            this.pnlIconTongHop.Name = "pnlIconTongHop";
            this.pnlIconTongHop.Size = new System.Drawing.Size(48, 48);
            this.pnlIconTongHop.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.cboHocKy);
            this.pnlHeader.Controls.Add(this.pnlTabs);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(24, 24);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1152, 69);
            this.pnlHeader.TabIndex = 0;
            // 
            // cboHocKy
            // 
            this.cboHocKy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHocKy.BackColor = System.Drawing.Color.Transparent;
            this.cboHocKy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.cboHocKy.BorderRadius = 8;
            this.cboHocKy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHocKy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.cboHocKy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.cboHocKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboHocKy.ForeColor = System.Drawing.Color.Black;
            this.cboHocKy.ItemHeight = 30;
            this.cboHocKy.Items.AddRange(new object[] {
            "Học kỳ I - 2024-2025",
            "Học kỳ II - 2024-2025"});
            this.cboHocKy.Location = new System.Drawing.Point(940, 14);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(206, 36);
            this.cboHocKy.StartIndex = 0;
            this.cboHocKy.TabIndex = 1;
            // 
            // pnlTabs
            // 
            this.pnlTabs.BackColor = System.Drawing.Color.White;
            this.pnlTabs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.pnlTabs.BorderRadius = 8;
            this.pnlTabs.BorderThickness = 1;
            this.pnlTabs.Controls.Add(this.btnThongKeHocLuc);
            this.pnlTabs.Controls.Add(this.btnBangDiem);
            this.pnlTabs.Controls.Add(this.btnDanhSachLop);
            this.pnlTabs.Location = new System.Drawing.Point(15, 14);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Padding = new System.Windows.Forms.Padding(5);
            this.pnlTabs.Size = new System.Drawing.Size(390, 45);
            this.pnlTabs.TabIndex = 0;
            // 
            // btnThongKeHocLuc
            // 
            this.btnThongKeHocLuc.BorderRadius = 8;
            this.btnThongKeHocLuc.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnThongKeHocLuc.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnThongKeHocLuc.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnThongKeHocLuc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThongKeHocLuc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThongKeHocLuc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThongKeHocLuc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThongKeHocLuc.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnThongKeHocLuc.FillColor = System.Drawing.Color.Transparent;
            this.btnThongKeHocLuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnThongKeHocLuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnThongKeHocLuc.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.btnThongKeHocLuc.Location = new System.Drawing.Point(238, 5);
            this.btnThongKeHocLuc.Name = "btnThongKeHocLuc";
            this.btnThongKeHocLuc.Size = new System.Drawing.Size(141, 35);
            this.btnThongKeHocLuc.TabIndex = 2;
            this.btnThongKeHocLuc.Text = "Thống kê học lực";
            // 
            // btnBangDiem
            // 
            this.btnBangDiem.BorderRadius = 8;
            this.btnBangDiem.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnBangDiem.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnBangDiem.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnBangDiem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBangDiem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBangDiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBangDiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBangDiem.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBangDiem.FillColor = System.Drawing.Color.Transparent;
            this.btnBangDiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnBangDiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnBangDiem.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.btnBangDiem.Location = new System.Drawing.Point(128, 5);
            this.btnBangDiem.Name = "btnBangDiem";
            this.btnBangDiem.Size = new System.Drawing.Size(110, 35);
            this.btnBangDiem.TabIndex = 1;
            this.btnBangDiem.Text = "Bảng điểm";
            // 
            // btnDanhSachLop
            // 
            this.btnDanhSachLop.BorderRadius = 8;
            this.btnDanhSachLop.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDanhSachLop.Checked = true;
            this.btnDanhSachLop.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnDanhSachLop.CheckedState.ForeColor = System.Drawing.Color.White;
            this.btnDanhSachLop.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDanhSachLop.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDanhSachLop.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDanhSachLop.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDanhSachLop.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDanhSachLop.FillColor = System.Drawing.Color.Transparent;
            this.btnDanhSachLop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnDanhSachLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnDanhSachLop.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.btnDanhSachLop.Location = new System.Drawing.Point(5, 5);
            this.btnDanhSachLop.Name = "btnDanhSachLop";
            this.btnDanhSachLop.Size = new System.Drawing.Size(123, 35);
            this.btnDanhSachLop.TabIndex = 0;
            this.btnDanhSachLop.Text = "Danh sách lớp";
            // 
            // ucBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "ucBaoCao";
            this.Size = new System.Drawing.Size(1200, 900);
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlCards.ResumeLayout(false);
            this.cardBaoCaoHocSinh.ResumeLayout(false);
            this.cardBaoCaoHocSinh.PerformLayout();
            this.cardThongKeDiem.ResumeLayout(false);
            this.cardThongKeDiem.PerformLayout();
            this.cardBaoCaoTongHop.ResumeLayout(false);
            this.cardBaoCaoTongHop.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2Panel pnlTabs;
        private Guna.UI2.WinForms.Guna2Button btnDanhSachLop;
        private Guna.UI2.WinForms.Guna2Button btnBangDiem;
        private Guna.UI2.WinForms.Guna2Button btnThongKeHocLuc;
        private Guna.UI2.WinForms.Guna2ComboBox cboHocKy;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private System.Windows.Forms.FlowLayoutPanel pnlCards;
        private Guna.UI2.WinForms.Guna2Panel cardBaoCaoHocSinh;
        private Guna.UI2.WinForms.Guna2Panel pnlIconBaoCao;
        private System.Windows.Forms.Label lblBaoCaoHocSinhTitle;
        private System.Windows.Forms.Label lblBaoCaoHocSinhDesc;
        private Guna.UI2.WinForms.Guna2Panel cardThongKeDiem;
        private System.Windows.Forms.Label lblThongKeDiemDesc;
        private System.Windows.Forms.Label lblThongKeDiemTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlIconThongKe;
        private Guna.UI2.WinForms.Guna2Panel cardBaoCaoTongHop;
        private System.Windows.Forms.Label lblBaoCaoTongHopDesc;
        private System.Windows.Forms.Label lblBaoCaoTongHopTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlIconTongHop;
    }
}