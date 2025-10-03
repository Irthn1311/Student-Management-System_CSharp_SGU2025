using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class TrangChu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrangChu));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelHeaderSidebar = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonHamburger = new System.Windows.Forms.Button();
            this.pictureBoxHamburger = new System.Windows.Forms.PictureBox();
            this.mainContentHeaderPanel = new System.Windows.Forms.Panel();
            this.labelDashboardTitle = new System.Windows.Forms.Label();
            this.panelSearchAdmin = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelHelloAdmin = new System.Windows.Forms.Label();
            this.pictureBoxAdminAvatar = new System.Windows.Forms.PictureBox();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanelStatCards = new System.Windows.Forms.FlowLayoutPanel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelRecentActivities = new System.Windows.Forms.Panel();
            this.flowLayoutPanelActivities = new System.Windows.Forms.FlowLayoutPanel();
            this.labelRecentActivities = new System.Windows.Forms.Label();
            this.panelScoreChart = new System.Windows.Forms.Panel();
            this.pictureBoxScoreChart = new System.Windows.Forms.PictureBox();
            this.labelScoreChart = new System.Windows.Forms.Label();
            this.sidebarMenuItemTrangChu = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.sidebarMenuItemHocSinh = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.sidebarMenuItemThoiKhoaBieu = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.sidebarMenuItemThiVaDiem = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.sidebarMenuItemDanhGia = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.sidebarMenuItemHanhKiem = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.sidebarMenuItemBaoCao = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.sidebarMenuItemLuuTru = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.sidebarMenuItemCaiDat = new Student_Management_System_CSharp_SGU2025.GUI.SidebarMenuItem();
            this.statCardStudents = new Student_Management_System_CSharp_SGU2025.GUI.StatCard();
            this.statCardTeachers = new Student_Management_System_CSharp_SGU2025.GUI.StatCard();
            this.statCardClasses = new Student_Management_System_CSharp_SGU2025.GUI.StatCard();
            this.statCardUpcomingExams = new Student_Management_System_CSharp_SGU2025.GUI.StatCard();
            this.recentActivityItem1 = new Student_Management_System_CSharp_SGU2025.GUI.RecentActivityItem();
            this.recentActivityItem2 = new Student_Management_System_CSharp_SGU2025.GUI.RecentActivityItem();
            this.recentActivityItem3 = new Student_Management_System_CSharp_SGU2025.GUI.RecentActivityItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelHeaderSidebar.SuspendLayout();
            this.buttonHamburger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHamburger)).BeginInit();
            this.mainContentHeaderPanel.SuspendLayout();
            this.panelSearchAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdminAvatar)).BeginInit();
            this.mainContentPanel.SuspendLayout();
            this.flowLayoutPanelStatCards.SuspendLayout();
            this.panelRecentActivities.SuspendLayout();
            this.flowLayoutPanelActivities.SuspendLayout();
            this.panelScoreChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScoreChart)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.splitContainer2.Panel2.Controls.Add(this.mainContentHeaderPanel);
            this.splitContainer2.Panel2.Controls.Add(this.mainContentPanel);
            this.splitContainer2.Size = new System.Drawing.Size(1898, 1144);
            this.splitContainer2.SplitterDistance = 259;
            this.splitContainer2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.panelHeaderSidebar);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemTrangChu);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemHocSinh);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemThoiKhoaBieu);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemThiVaDiem);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemDanhGia);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemHanhKiem);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemBaoCao);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemLuuTru);
            this.flowLayoutPanel1.Controls.Add(this.sidebarMenuItemCaiDat);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(259, 1144);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panelHeaderSidebar
            // 
            this.panelHeaderSidebar.Controls.Add(this.labelTitle);
            this.panelHeaderSidebar.Controls.Add(this.buttonHamburger);
            this.panelHeaderSidebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeaderSidebar.Location = new System.Drawing.Point(3, 3);
            this.panelHeaderSidebar.Name = "panelHeaderSidebar";
            this.panelHeaderSidebar.Size = new System.Drawing.Size(242, 50);
            this.panelHeaderSidebar.TabIndex = 7;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.labelTitle.Location = new System.Drawing.Point(10, 10);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(119, 40);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "QLHS";
            // 
            // buttonHamburger
            // 
            this.buttonHamburger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHamburger.Controls.Add(this.pictureBoxHamburger);
            this.buttonHamburger.FlatAppearance.BorderSize = 0;
            this.buttonHamburger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHamburger.Location = new System.Drawing.Point(192, 5);
            this.buttonHamburger.Name = "buttonHamburger";
            this.buttonHamburger.Size = new System.Drawing.Size(0, 0);
            this.buttonHamburger.TabIndex = 1;
            this.buttonHamburger.UseVisualStyleBackColor = true;
            this.buttonHamburger.Click += new System.EventHandler(this.buttonHamburger_Click);
            // 
            // pictureBoxHamburger
            // 
            this.pictureBoxHamburger.Location = new System.Drawing.Point(10, 10);
            this.pictureBoxHamburger.Name = "pictureBoxHamburger";
            this.pictureBoxHamburger.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxHamburger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxHamburger.TabIndex = 0;
            this.pictureBoxHamburger.TabStop = false;
            // 
            // mainContentHeaderPanel
            // 
            this.mainContentHeaderPanel.BackColor = System.Drawing.Color.White;
            this.mainContentHeaderPanel.Controls.Add(this.labelDashboardTitle);
            this.mainContentHeaderPanel.Controls.Add(this.panelSearchAdmin);
            this.mainContentHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainContentHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.mainContentHeaderPanel.Name = "mainContentHeaderPanel";
            this.mainContentHeaderPanel.Size = new System.Drawing.Size(1635, 74);
            this.mainContentHeaderPanel.TabIndex = 0;
            // 
            // labelDashboardTitle
            // 
            this.labelDashboardTitle.AutoSize = true;
            this.labelDashboardTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDashboardTitle.Location = new System.Drawing.Point(46, 15);
            this.labelDashboardTitle.Name = "labelDashboardTitle";
            this.labelDashboardTitle.Size = new System.Drawing.Size(238, 33);
            this.labelDashboardTitle.TabIndex = 0;
            this.labelDashboardTitle.Text = "Bảng điều khiển";
            // 
            // panelSearchAdmin
            // 
            this.panelSearchAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchAdmin.Controls.Add(this.textBoxSearch);
            this.panelSearchAdmin.Controls.Add(this.labelHelloAdmin);
            this.panelSearchAdmin.Controls.Add(this.pictureBoxAdminAvatar);
            this.panelSearchAdmin.Location = new System.Drawing.Point(1121, 15);
            this.panelSearchAdmin.Name = "panelSearchAdmin";
            this.panelSearchAdmin.Size = new System.Drawing.Size(500, 40);
            this.panelSearchAdmin.TabIndex = 1;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(0, 8);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(250, 30);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.Text = "Tìm kiếm...";
            // 
            // labelHelloAdmin
            // 
            this.labelHelloAdmin.AutoSize = true;
            this.labelHelloAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelloAdmin.Location = new System.Drawing.Point(300, 11);
            this.labelHelloAdmin.Name = "labelHelloAdmin";
            this.labelHelloAdmin.Size = new System.Drawing.Size(155, 25);
            this.labelHelloAdmin.TabIndex = 1;
            this.labelHelloAdmin.Text = "Xin chào, Admin";
            // 
            // pictureBoxAdminAvatar
            // 
            this.pictureBoxAdminAvatar.Location = new System.Drawing.Point(450, 5);
            this.pictureBoxAdminAvatar.Name = "pictureBoxAdminAvatar";
            this.pictureBoxAdminAvatar.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxAdminAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAdminAvatar.TabIndex = 2;
            this.pictureBoxAdminAvatar.TabStop = false;
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.AutoScroll = true;
            this.mainContentPanel.Controls.Add(this.flowLayoutPanelStatCards);
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(0, 0);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Padding = new System.Windows.Forms.Padding(24);
            this.mainContentPanel.Size = new System.Drawing.Size(1635, 1144);
            this.mainContentPanel.TabIndex = 1;
            this.mainContentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainContentPanel_Paint);
            // 
            // flowLayoutPanelStatCards
            // 
            this.flowLayoutPanelStatCards.Controls.Add(this.statCardUpcomingExams);
            this.flowLayoutPanelStatCards.Controls.Add(this.statCardClasses);
            this.flowLayoutPanelStatCards.Controls.Add(this.statCardStudents);
            this.flowLayoutPanelStatCards.Controls.Add(this.statCardTeachers);
            this.flowLayoutPanelStatCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelStatCards.Location = new System.Drawing.Point(24, 24);
            this.flowLayoutPanelStatCards.Name = "flowLayoutPanelStatCards";
            this.flowLayoutPanelStatCards.Size = new System.Drawing.Size(1587, 1096);
            this.flowLayoutPanelStatCards.TabIndex = 0;
            this.flowLayoutPanelStatCards.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelStatCards_Paint);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "3que.png");
            this.imageList1.Images.SetKeyName(1, "home.png");
            this.imageList1.Images.SetKeyName(2, "student.png");
            this.imageList1.Images.SetKeyName(3, "calendar.png");
            // 
            // panelRecentActivities
            // 
            this.panelRecentActivities.BackColor = System.Drawing.Color.White;
            this.panelRecentActivities.Controls.Add(this.flowLayoutPanelActivities);
            this.panelRecentActivities.Controls.Add(this.labelRecentActivities);
            this.panelRecentActivities.Location = new System.Drawing.Point(24, 170);
            this.panelRecentActivities.Name = "panelRecentActivities";
            this.panelRecentActivities.Padding = new System.Windows.Forms.Padding(24);
            this.panelRecentActivities.Size = new System.Drawing.Size(400, 250);
            this.panelRecentActivities.TabIndex = 1;
            // 
            // flowLayoutPanelActivities
            // 
            this.flowLayoutPanelActivities.Controls.Add(this.recentActivityItem1);
            this.flowLayoutPanelActivities.Controls.Add(this.recentActivityItem2);
            this.flowLayoutPanelActivities.Controls.Add(this.recentActivityItem3);
            this.flowLayoutPanelActivities.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelActivities.Location = new System.Drawing.Point(24, 60);
            this.flowLayoutPanelActivities.Name = "flowLayoutPanelActivities";
            this.flowLayoutPanelActivities.Size = new System.Drawing.Size(350, 150);
            this.flowLayoutPanelActivities.TabIndex = 1;
            // 
            // labelRecentActivities
            // 
            this.labelRecentActivities.AutoSize = true;
            this.labelRecentActivities.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecentActivities.Location = new System.Drawing.Point(24, 24);
            this.labelRecentActivities.Name = "labelRecentActivities";
            this.labelRecentActivities.Size = new System.Drawing.Size(231, 29);
            this.labelRecentActivities.TabIndex = 0;
            this.labelRecentActivities.Text = "Hoạt động gần đây";
            // 
            // panelScoreChart
            // 
            this.panelScoreChart.BackColor = System.Drawing.Color.White;
            this.panelScoreChart.Controls.Add(this.pictureBoxScoreChart);
            this.panelScoreChart.Controls.Add(this.labelScoreChart);
            this.panelScoreChart.Location = new System.Drawing.Point(450, 170);
            this.panelScoreChart.Name = "panelScoreChart";
            this.panelScoreChart.Padding = new System.Windows.Forms.Padding(24);
            this.panelScoreChart.Size = new System.Drawing.Size(800, 250);
            this.panelScoreChart.TabIndex = 2;
            // 
            // pictureBoxScoreChart
            // 
            this.pictureBoxScoreChart.Location = new System.Drawing.Point(24, 60);
            this.pictureBoxScoreChart.Name = "pictureBoxScoreChart";
            this.pictureBoxScoreChart.Size = new System.Drawing.Size(750, 160);
            this.pictureBoxScoreChart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxScoreChart.TabIndex = 1;
            this.pictureBoxScoreChart.TabStop = false;
            // 
            // labelScoreChart
            // 
            this.labelScoreChart.AutoSize = true;
            this.labelScoreChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScoreChart.Location = new System.Drawing.Point(24, 24);
            this.labelScoreChart.Name = "labelScoreChart";
            this.labelScoreChart.Size = new System.Drawing.Size(203, 29);
            this.labelScoreChart.TabIndex = 0;
            this.labelScoreChart.Text = "Biểu đồ điểm số";
            // 
            // sidebarMenuItemTrangChu
            // 
            this.sidebarMenuItemTrangChu.Location = new System.Drawing.Point(4, 61);
            this.sidebarMenuItemTrangChu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemTrangChu.MenuIcon = null;
            this.sidebarMenuItemTrangChu.MenuText = "Trang chủ";
            this.sidebarMenuItemTrangChu.Name = "sidebarMenuItemTrangChu";
            this.sidebarMenuItemTrangChu.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemTrangChu.TabIndex = 8;
            // 
            // sidebarMenuItemHocSinh
            // 
            this.sidebarMenuItemHocSinh.Location = new System.Drawing.Point(4, 115);
            this.sidebarMenuItemHocSinh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemHocSinh.MenuIcon = null;
            this.sidebarMenuItemHocSinh.MenuText = "Học sinh";
            this.sidebarMenuItemHocSinh.Name = "sidebarMenuItemHocSinh";
            this.sidebarMenuItemHocSinh.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemHocSinh.TabIndex = 9;
            // 
            // sidebarMenuItemThoiKhoaBieu
            // 
            this.sidebarMenuItemThoiKhoaBieu.Location = new System.Drawing.Point(4, 169);
            this.sidebarMenuItemThoiKhoaBieu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemThoiKhoaBieu.MenuIcon = null;
            this.sidebarMenuItemThoiKhoaBieu.MenuText = "Thời khóa biểu";
            this.sidebarMenuItemThoiKhoaBieu.Name = "sidebarMenuItemThoiKhoaBieu";
            this.sidebarMenuItemThoiKhoaBieu.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemThoiKhoaBieu.TabIndex = 10;
            // 
            // sidebarMenuItemThiVaDiem
            // 
            this.sidebarMenuItemThiVaDiem.Location = new System.Drawing.Point(4, 223);
            this.sidebarMenuItemThiVaDiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemThiVaDiem.MenuIcon = null;
            this.sidebarMenuItemThiVaDiem.MenuText = "Thi & Điểm";
            this.sidebarMenuItemThiVaDiem.Name = "sidebarMenuItemThiVaDiem";
            this.sidebarMenuItemThiVaDiem.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemThiVaDiem.TabIndex = 11;
            // 
            // sidebarMenuItemDanhGia
            // 
            this.sidebarMenuItemDanhGia.Location = new System.Drawing.Point(4, 277);
            this.sidebarMenuItemDanhGia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemDanhGia.MenuIcon = null;
            this.sidebarMenuItemDanhGia.MenuText = "Đánh giá";
            this.sidebarMenuItemDanhGia.Name = "sidebarMenuItemDanhGia";
            this.sidebarMenuItemDanhGia.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemDanhGia.TabIndex = 12;
            // 
            // sidebarMenuItemHanhKiem
            // 
            this.sidebarMenuItemHanhKiem.Location = new System.Drawing.Point(4, 331);
            this.sidebarMenuItemHanhKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemHanhKiem.MenuIcon = null;
            this.sidebarMenuItemHanhKiem.MenuText = "Hạnh kiểm";
            this.sidebarMenuItemHanhKiem.Name = "sidebarMenuItemHanhKiem";
            this.sidebarMenuItemHanhKiem.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemHanhKiem.TabIndex = 13;
            // 
            // sidebarMenuItemBaoCao
            // 
            this.sidebarMenuItemBaoCao.Location = new System.Drawing.Point(4, 385);
            this.sidebarMenuItemBaoCao.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemBaoCao.MenuIcon = null;
            this.sidebarMenuItemBaoCao.MenuText = "Báo cáo";
            this.sidebarMenuItemBaoCao.Name = "sidebarMenuItemBaoCao";
            this.sidebarMenuItemBaoCao.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemBaoCao.TabIndex = 14;
            // 
            // sidebarMenuItemLuuTru
            // 
            this.sidebarMenuItemLuuTru.Location = new System.Drawing.Point(4, 439);
            this.sidebarMenuItemLuuTru.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemLuuTru.MenuIcon = null;
            this.sidebarMenuItemLuuTru.MenuText = "Lưu trữ";
            this.sidebarMenuItemLuuTru.Name = "sidebarMenuItemLuuTru";
            this.sidebarMenuItemLuuTru.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemLuuTru.TabIndex = 15;
            // 
            // sidebarMenuItemCaiDat
            // 
            this.sidebarMenuItemCaiDat.Location = new System.Drawing.Point(4, 493);
            this.sidebarMenuItemCaiDat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sidebarMenuItemCaiDat.MenuIcon = null;
            this.sidebarMenuItemCaiDat.MenuText = "Cài đặt";
            this.sidebarMenuItemCaiDat.Name = "sidebarMenuItemCaiDat";
            this.sidebarMenuItemCaiDat.Size = new System.Drawing.Size(240, 44);
            this.sidebarMenuItemCaiDat.TabIndex = 16;
            // 
            // statCardStudents
            // 
            this.statCardStudents.BackColor = System.Drawing.Color.White;
            this.statCardStudents.Icon = null;
            this.statCardStudents.Location = new System.Drawing.Point(460, 5);
            this.statCardStudents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.statCardStudents.Name = "statCardStudents";
            this.statCardStudents.Size = new System.Drawing.Size(220, 100);
            this.statCardStudents.TabIndex = 0;
            this.statCardStudents.Title = "Tổng số học sinh";
            this.statCardStudents.Value = "1,247";
            // 
            // statCardTeachers
            // 
            this.statCardTeachers.BackColor = System.Drawing.Color.White;
            this.statCardTeachers.Icon = null;
            this.statCardTeachers.Location = new System.Drawing.Point(688, 5);
            this.statCardTeachers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.statCardTeachers.Name = "statCardTeachers";
            this.statCardTeachers.Size = new System.Drawing.Size(220, 100);
            this.statCardTeachers.TabIndex = 1;
            this.statCardTeachers.Title = "Giáo viên";
            this.statCardTeachers.Value = "89";
            // 
            // statCardClasses
            // 
            this.statCardClasses.BackColor = System.Drawing.Color.White;
            this.statCardClasses.Icon = null;
            this.statCardClasses.Location = new System.Drawing.Point(232, 5);
            this.statCardClasses.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.statCardClasses.Name = "statCardClasses";
            this.statCardClasses.Size = new System.Drawing.Size(220, 100);
            this.statCardClasses.TabIndex = 2;
            this.statCardClasses.Title = "Lớp học";
            this.statCardClasses.Value = "32";
            // 
            // statCardUpcomingExams
            // 
            this.statCardUpcomingExams.BackColor = System.Drawing.Color.White;
            this.statCardUpcomingExams.Icon = null;
            this.statCardUpcomingExams.Location = new System.Drawing.Point(4, 5);
            this.statCardUpcomingExams.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.statCardUpcomingExams.Name = "statCardUpcomingExams";
            this.statCardUpcomingExams.Size = new System.Drawing.Size(220, 100);
            this.statCardUpcomingExams.TabIndex = 3;
            this.statCardUpcomingExams.Title = "Kỳ thi sắp tới";
            this.statCardUpcomingExams.Value = "3";
            this.statCardUpcomingExams.Load += new System.EventHandler(this.statCardUpcomingExams_Load);
            // 
            // recentActivityItem1
            // 
            this.recentActivityItem1.ActivityText = "Nguyễn Văn A đã nộp bài thi Toán";
            this.recentActivityItem1.IndicatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.recentActivityItem1.Location = new System.Drawing.Point(4, 5);
            this.recentActivityItem1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recentActivityItem1.Name = "recentActivityItem1";
            this.recentActivityItem1.Size = new System.Drawing.Size(300, 32);
            this.recentActivityItem1.TabIndex = 0;
            this.recentActivityItem1.TimeText = "5 phút trước";
            // 
            // recentActivityItem2
            // 
            this.recentActivityItem2.ActivityText = "Giáo viên Trần Thị B đã cập nhật điểm";
            this.recentActivityItem2.IndicatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.recentActivityItem2.Location = new System.Drawing.Point(4, 47);
            this.recentActivityItem2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recentActivityItem2.Name = "recentActivityItem2";
            this.recentActivityItem2.Size = new System.Drawing.Size(300, 32);
            this.recentActivityItem2.TabIndex = 1;
            this.recentActivityItem2.TimeText = "15 phút trước";
            // 
            // recentActivityItem3
            // 
            this.recentActivityItem3.ActivityText = "Thông báo: Lịch thi giữa kỳ đã được cập nhật";
            this.recentActivityItem3.IndicatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(115)))), ((int)(((byte)(22)))));
            this.recentActivityItem3.Location = new System.Drawing.Point(4, 89);
            this.recentActivityItem3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recentActivityItem3.Name = "recentActivityItem3";
            this.recentActivityItem3.Size = new System.Drawing.Size(300, 32);
            this.recentActivityItem3.TabIndex = 2;
            this.recentActivityItem3.TimeText = "1 giờ trước";
            // 
            // TrangChu
            // 
            this.ClientSize = new System.Drawing.Size(1898, 1144);
            this.Controls.Add(this.splitContainer2);
            this.Name = "TrangChu";
            this.Load += new System.EventHandler(this.TrangChu_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelHeaderSidebar.ResumeLayout(false);
            this.panelHeaderSidebar.PerformLayout();
            this.buttonHamburger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHamburger)).EndInit();
            this.mainContentHeaderPanel.ResumeLayout(false);
            this.mainContentHeaderPanel.PerformLayout();
            this.panelSearchAdmin.ResumeLayout(false);
            this.panelSearchAdmin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdminAvatar)).EndInit();
            this.mainContentPanel.ResumeLayout(false);
            this.flowLayoutPanelStatCards.ResumeLayout(false);
            this.panelRecentActivities.ResumeLayout(false);
            this.panelRecentActivities.PerformLayout();
            this.flowLayoutPanelActivities.ResumeLayout(false);
            this.panelScoreChart.ResumeLayout(false);
            this.panelScoreChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScoreChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panelHeaderSidebar;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonHamburger;
        private System.Windows.Forms.PictureBox pictureBoxHamburger;
        private SidebarMenuItem sidebarMenuItemTrangChu;
        private SidebarMenuItem sidebarMenuItemHocSinh;
        private SidebarMenuItem sidebarMenuItemThoiKhoaBieu;
        private SidebarMenuItem sidebarMenuItemThiVaDiem;
        private SidebarMenuItem sidebarMenuItemDanhGia;
        private SidebarMenuItem sidebarMenuItemHanhKiem;
        private SidebarMenuItem sidebarMenuItemBaoCao;
        private SidebarMenuItem sidebarMenuItemLuuTru;
        private SidebarMenuItem sidebarMenuItemCaiDat;
        private System.Windows.Forms.Panel mainContentHeaderPanel;
        private System.Windows.Forms.Label labelDashboardTitle;
        private System.Windows.Forms.Panel panelSearchAdmin;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelHelloAdmin;
        private System.Windows.Forms.PictureBox pictureBoxAdminAvatar;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelStatCards;
        private StatCard statCardStudents;
        private StatCard statCardTeachers;
        private StatCard statCardClasses;
        private StatCard statCardUpcomingExams;
        private System.Windows.Forms.Panel panelRecentActivities;
        private System.Windows.Forms.Label labelRecentActivities;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelActivities;
        private RecentActivityItem recentActivityItem1;
        private RecentActivityItem recentActivityItem2;
        private RecentActivityItem recentActivityItem3;
        private System.Windows.Forms.Panel panelScoreChart;
        private System.Windows.Forms.Label labelScoreChart;
        private System.Windows.Forms.PictureBox pictureBoxScoreChart;
    }
}