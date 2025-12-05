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
            this.cardBaoCaoTongHop = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.cardThongKeDiem = new Guna.UI2.WinForms.Guna2Panel();
            this.lblThongKeDiemDesc = new System.Windows.Forms.Label();
            this.lblThongKeDiemTitle = new System.Windows.Forms.Label();
            this.pnlIconThongKe = new Guna.UI2.WinForms.Guna2Panel();
            this.cardBaoCaoHocSinh = new Guna.UI2.WinForms.Guna2Panel();
            this.lblBaoCaoHocSinhDesc = new System.Windows.Forms.Label();
            this.pnlIconBaoCao = new Guna.UI2.WinForms.Guna2Panel();
            this.lblBaoCaoHocSinhTitle = new System.Windows.Forms.Label();
            this.cboHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlClassesContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.itemLopHoc1 = new Student_Management_System_CSharp_SGU2025.GUI.BaoCao.itemLopHoc();
            this.pnlClassListHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.btnExportExcel = new Guna.UI2.WinForms.Guna2Button();
            this.btnExportPdf = new Guna.UI2.WinForms.Guna2Button();
            this.lblClassListTitle = new System.Windows.Forms.Label();
            this.pnlTabs = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThongKeHocLuc = new Guna.UI2.WinForms.Guna2Button();
            this.btnBangDiem = new Guna.UI2.WinForms.Guna2Button();
            this.btnDanhSachLop = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain.SuspendLayout();
            this.cardBaoCaoTongHop.SuspendLayout();
            this.cardThongKeDiem.SuspendLayout();
            this.cardBaoCaoHocSinh.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlClassesContainer.SuspendLayout();
            this.pnlClassListHeader.SuspendLayout();
            this.pnlTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMain.Controls.Add(this.cardBaoCaoTongHop);
            this.pnlMain.Controls.Add(this.cardThongKeDiem);
            this.pnlMain.Controls.Add(this.cardBaoCaoHocSinh);
            this.pnlMain.Controls.Add(this.cboHocKy);
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlTabs);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(24);
            this.pnlMain.Size = new System.Drawing.Size(1168, 768);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // cardBaoCaoTongHop
            // 
            this.cardBaoCaoTongHop.BackColor = System.Drawing.Color.White;
            this.cardBaoCaoTongHop.BorderRadius = 5;
            this.cardBaoCaoTongHop.Controls.Add(this.label1);
            this.cardBaoCaoTongHop.Controls.Add(this.label2);
            this.cardBaoCaoTongHop.Controls.Add(this.guna2Panel1);
            this.cardBaoCaoTongHop.Location = new System.Drawing.Point(775, 85);
            this.cardBaoCaoTongHop.Name = "cardBaoCaoTongHop";
            this.cardBaoCaoTongHop.Size = new System.Drawing.Size(358, 97);
            this.cardBaoCaoTongHop.TabIndex = 9;
            this.cardBaoCaoTongHop.Paint += new System.Windows.Forms.PaintEventHandler(this.cardBaoCaoTongHop_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.label1.Location = new System.Drawing.Point(96, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Cả năm học";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.label2.Location = new System.Drawing.Point(96, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Báo cáo tổng hợp";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(213)))));
            this.guna2Panel1.BorderRadius = 8;
            this.guna2Panel1.Location = new System.Drawing.Point(28, 21);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(48, 48);
            this.guna2Panel1.TabIndex = 6;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // cardThongKeDiem
            // 
            this.cardThongKeDiem.BackColor = System.Drawing.Color.White;
            this.cardThongKeDiem.BorderRadius = 5;
            this.cardThongKeDiem.Controls.Add(this.lblThongKeDiemDesc);
            this.cardThongKeDiem.Controls.Add(this.lblThongKeDiemTitle);
            this.cardThongKeDiem.Controls.Add(this.pnlIconThongKe);
            this.cardThongKeDiem.Location = new System.Drawing.Point(390, 85);
            this.cardThongKeDiem.Name = "cardThongKeDiem";
            this.cardThongKeDiem.Size = new System.Drawing.Size(358, 97);
            this.cardThongKeDiem.TabIndex = 4;
            this.cardThongKeDiem.Paint += new System.Windows.Forms.PaintEventHandler(this.cardThongKeDiem_Paint_1);
            // 
            // lblThongKeDiemDesc
            // 
            this.lblThongKeDiemDesc.AutoSize = true;
            this.lblThongKeDiemDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblThongKeDiemDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblThongKeDiemDesc.Location = new System.Drawing.Point(102, 49);
            this.lblThongKeDiemDesc.Name = "lblThongKeDiemDesc";
            this.lblThongKeDiemDesc.Size = new System.Drawing.Size(106, 15);
            this.lblThongKeDiemDesc.TabIndex = 8;
            this.lblThongKeDiemDesc.Text = "Theo lớp/môn học";
            this.lblThongKeDiemDesc.Click += new System.EventHandler(this.lblThongKeDiemDesc_Click);
            // 
            // lblThongKeDiemTitle
            // 
            this.lblThongKeDiemTitle.AutoSize = true;
            this.lblThongKeDiemTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblThongKeDiemTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblThongKeDiemTitle.Location = new System.Drawing.Point(102, 25);
            this.lblThongKeDiemTitle.Name = "lblThongKeDiemTitle";
            this.lblThongKeDiemTitle.Size = new System.Drawing.Size(115, 17);
            this.lblThongKeDiemTitle.TabIndex = 7;
            this.lblThongKeDiemTitle.Text = "Thống kê điểm";
            this.lblThongKeDiemTitle.Click += new System.EventHandler(this.lblThongKeDiemTitle_Click);
            // 
            // pnlIconThongKe
            // 
            this.pnlIconThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.pnlIconThongKe.BorderRadius = 8;
            this.pnlIconThongKe.Location = new System.Drawing.Point(35, 21);
            this.pnlIconThongKe.Name = "pnlIconThongKe";
            this.pnlIconThongKe.Size = new System.Drawing.Size(48, 48);
            this.pnlIconThongKe.TabIndex = 6;
            this.pnlIconThongKe.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlIconThongKe_Paint);
            // 
            // cardBaoCaoHocSinh
            // 
            this.cardBaoCaoHocSinh.BackColor = System.Drawing.Color.White;
            this.cardBaoCaoHocSinh.BorderRadius = 5;
            this.cardBaoCaoHocSinh.Controls.Add(this.lblBaoCaoHocSinhDesc);
            this.cardBaoCaoHocSinh.Controls.Add(this.pnlIconBaoCao);
            this.cardBaoCaoHocSinh.Controls.Add(this.lblBaoCaoHocSinhTitle);
            this.cardBaoCaoHocSinh.Location = new System.Drawing.Point(13, 85);
            this.cardBaoCaoHocSinh.Name = "cardBaoCaoHocSinh";
            this.cardBaoCaoHocSinh.Size = new System.Drawing.Size(358, 97);
            this.cardBaoCaoHocSinh.TabIndex = 3;
            this.cardBaoCaoHocSinh.Paint += new System.Windows.Forms.PaintEventHandler(this.cardBaoCaoHocSinh_Paint_1);
            // 
            // lblBaoCaoHocSinhDesc
            // 
            this.lblBaoCaoHocSinhDesc.AutoSize = true;
            this.lblBaoCaoHocSinhDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblBaoCaoHocSinhDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblBaoCaoHocSinhDesc.Location = new System.Drawing.Point(90, 49);
            this.lblBaoCaoHocSinhDesc.Name = "lblBaoCaoHocSinhDesc";
            this.lblBaoCaoHocSinhDesc.Size = new System.Drawing.Size(71, 15);
            this.lblBaoCaoHocSinhDesc.TabIndex = 2;
            this.lblBaoCaoHocSinhDesc.Text = "Số học sinh";
            this.lblBaoCaoHocSinhDesc.Click += new System.EventHandler(this.lblBaoCaoHocSinhDesc_Click);
            // 
            // pnlIconBaoCao
            // 
            this.pnlIconBaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.pnlIconBaoCao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlIconBaoCao.BorderRadius = 8;
            this.pnlIconBaoCao.Location = new System.Drawing.Point(26, 21);
            this.pnlIconBaoCao.Name = "pnlIconBaoCao";
            this.pnlIconBaoCao.Size = new System.Drawing.Size(48, 48);
            this.pnlIconBaoCao.TabIndex = 0;
            this.pnlIconBaoCao.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlIconBaoCao_Paint);
            // 
            // lblBaoCaoHocSinhTitle
            // 
            this.lblBaoCaoHocSinhTitle.AutoSize = true;
            this.lblBaoCaoHocSinhTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblBaoCaoHocSinhTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblBaoCaoHocSinhTitle.Location = new System.Drawing.Point(90, 25);
            this.lblBaoCaoHocSinhTitle.Name = "lblBaoCaoHocSinhTitle";
            this.lblBaoCaoHocSinhTitle.Size = new System.Drawing.Size(133, 17);
            this.lblBaoCaoHocSinhTitle.TabIndex = 1;
            this.lblBaoCaoHocSinhTitle.Text = "Báo cáo học sinh";
            this.lblBaoCaoHocSinhTitle.Click += new System.EventHandler(this.lblBaoCaoHocSinhTitle_Click);
            // 
            // cboHocKy
            // 
            this.cboHocKy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHocKy.BackColor = System.Drawing.Color.Transparent;
            this.cboHocKy.BorderColor = System.Drawing.Color.White;
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
            this.cboHocKy.Location = new System.Drawing.Point(868, 12);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(206, 36);
            this.cboHocKy.StartIndex = 0;
            this.cboHocKy.TabIndex = 1;
            this.cboHocKy.SelectedIndexChanged += new System.EventHandler(this.CboHocKy_SelectedIndexChanged);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.pnlClassesContainer);
            this.pnlContent.Controls.Add(this.pnlClassListHeader);
            this.pnlContent.Location = new System.Drawing.Point(13, 206);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1120, 657);
            this.pnlContent.TabIndex = 1;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // pnlClassesContainer
            // 
            this.pnlClassesContainer.AutoScroll = true;
            this.pnlClassesContainer.Controls.Add(this.itemLopHoc1);
            this.pnlClassesContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlClassesContainer.Location = new System.Drawing.Point(25, 83);
            this.pnlClassesContainer.Name = "pnlClassesContainer";
            this.pnlClassesContainer.Size = new System.Drawing.Size(1095, 554);
            this.pnlClassesContainer.TabIndex = 2;
            this.pnlClassesContainer.WrapContents = false;
            this.pnlClassesContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlClassesContainer_Paint);
            // 
            // itemLopHoc1
            // 
            this.itemLopHoc1.Location = new System.Drawing.Point(3, 3);
            this.itemLopHoc1.MaHocKy = 0;
            this.itemLopHoc1.MaLop = 0;
            this.itemLopHoc1.Name = "itemLopHoc1";
            this.itemLopHoc1.SiSo = 0;
            this.itemLopHoc1.Size = new System.Drawing.Size(1071, 77);
            this.itemLopHoc1.TabIndex = 0;
            this.itemLopHoc1.TenGVCN = null;
            this.itemLopHoc1.TenLop = null;
            this.itemLopHoc1.Load += new System.EventHandler(this.itemLopHoc1_Load);
            // 
            // pnlClassListHeader
            // 
            this.pnlClassListHeader.Controls.Add(this.btnExportExcel);
            this.pnlClassListHeader.Controls.Add(this.btnExportPdf);
            this.pnlClassListHeader.Controls.Add(this.lblClassListTitle);
            this.pnlClassListHeader.Location = new System.Drawing.Point(25, 23);
            this.pnlClassListHeader.Name = "pnlClassListHeader";
            this.pnlClassListHeader.Size = new System.Drawing.Size(1071, 40);
            this.pnlClassListHeader.TabIndex = 1;
            this.pnlClassListHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlClassListHeader_Paint);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcel.BorderRadius = 8;
            this.btnExportExcel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExportExcel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExportExcel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExportExcel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExportExcel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(906, 0);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(122, 40);
            this.btnExportExcel.TabIndex = 2;
            this.btnExportExcel.Text = "📊 Xuất Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPdf.BorderRadius = 8;
            this.btnExportPdf.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExportPdf.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExportPdf.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExportPdf.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExportPdf.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnExportPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExportPdf.ForeColor = System.Drawing.Color.White;
            this.btnExportPdf.Location = new System.Drawing.Point(765, 0);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(122, 40);
            this.btnExportPdf.TabIndex = 1;
            this.btnExportPdf.Text = "📄 Xuất PDF";
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // lblClassListTitle
            // 
            this.lblClassListTitle.AutoSize = true;
            this.lblClassListTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassListTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblClassListTitle.Location = new System.Drawing.Point(21, 11);
            this.lblClassListTitle.Name = "lblClassListTitle";
            this.lblClassListTitle.Size = new System.Drawing.Size(212, 21);
            this.lblClassListTitle.TabIndex = 0;
            this.lblClassListTitle.Text = "Báo cáo danh sách lớp học";
            this.lblClassListTitle.Click += new System.EventHandler(this.lblClassListTitle_Click);
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
            this.pnlTabs.Location = new System.Drawing.Point(13, 12);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Padding = new System.Windows.Forms.Padding(5);
            this.pnlTabs.Size = new System.Drawing.Size(390, 45);
            this.pnlTabs.TabIndex = 0;
            this.pnlTabs.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTabs_Paint);
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
            this.btnThongKeHocLuc.Click += new System.EventHandler(this.btnThongKeHocLuc_Click);
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
            this.btnBangDiem.Click += new System.EventHandler(this.btnBangDiem_Click);
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
            this.btnDanhSachLop.Click += new System.EventHandler(this.btnDanhSachLop_Click);
            // 
            // ucBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.Controls.Add(this.pnlMain);
            this.Name = "ucBaoCao";
            this.Size = new System.Drawing.Size(1168, 768);
            this.pnlMain.ResumeLayout(false);
            this.cardBaoCaoTongHop.ResumeLayout(false);
            this.cardBaoCaoTongHop.PerformLayout();
            this.cardThongKeDiem.ResumeLayout(false);
            this.cardThongKeDiem.PerformLayout();
            this.cardBaoCaoHocSinh.ResumeLayout(false);
            this.cardBaoCaoHocSinh.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlClassesContainer.ResumeLayout(false);
            this.pnlClassListHeader.ResumeLayout(false);
            this.pnlClassListHeader.PerformLayout();
            this.pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlTabs;
        private Guna.UI2.WinForms.Guna2Button btnDanhSachLop;
        private Guna.UI2.WinForms.Guna2Button btnBangDiem;
        private Guna.UI2.WinForms.Guna2Button btnThongKeHocLuc;
        private Guna.UI2.WinForms.Guna2ComboBox cboHocKy;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private System.Windows.Forms.Label lblBaoCaoHocSinhDesc;
        private System.Windows.Forms.Label lblBaoCaoHocSinhTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlIconBaoCao;
        private Guna.UI2.WinForms.Guna2Panel pnlClassListHeader;
        private Guna.UI2.WinForms.Guna2Button btnExportExcel;
        private Guna.UI2.WinForms.Guna2Button btnExportPdf;
        private System.Windows.Forms.Label lblClassListTitle;
        private System.Windows.Forms.FlowLayoutPanel pnlClassesContainer;
        private Guna.UI2.WinForms.Guna2Panel cardBaoCaoHocSinh;
        private Guna.UI2.WinForms.Guna2Panel cardBaoCaoTongHop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel cardThongKeDiem;
        private System.Windows.Forms.Label lblThongKeDiemDesc;
        private System.Windows.Forms.Label lblThongKeDiemTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlIconThongKe;
        private BaoCao.itemLopHoc itemLopHoc1;
    }
}