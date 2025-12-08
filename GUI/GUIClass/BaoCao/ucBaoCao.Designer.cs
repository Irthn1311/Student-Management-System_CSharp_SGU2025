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
            this.cboHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlClassesContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.itemLopHoc1 = new Student_Management_System_CSharp_SGU2025.GUI.itemLopHoc();
            this.pnlClassListHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.btnExportExcel = new Guna.UI2.WinForms.Guna2Button();
            this.btnExportPdf = new Guna.UI2.WinForms.Guna2Button();
            this.lblClassListTitle = new System.Windows.Forms.Label();
            this.pnlTabs = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThongKeHocLuc = new Guna.UI2.WinForms.Guna2Button();
            this.btnBangDiem = new Guna.UI2.WinForms.Guna2Button();
            this.btnDanhSachLop = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain.SuspendLayout();
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
            this.pnlContent.Location = new System.Drawing.Point(13, 85);
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
        private Guna.UI2.WinForms.Guna2Panel pnlClassListHeader;
        private Guna.UI2.WinForms.Guna2Button btnExportExcel;
        private Guna.UI2.WinForms.Guna2Button btnExportPdf;
        private System.Windows.Forms.Label lblClassListTitle;
        private System.Windows.Forms.FlowLayoutPanel pnlClassesContainer;
        private itemLopHoc itemLopHoc1;
    }
}