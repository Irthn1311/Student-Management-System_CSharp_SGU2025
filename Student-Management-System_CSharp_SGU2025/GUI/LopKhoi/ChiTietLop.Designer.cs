using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ChiTietLop
    {
        private Guna2Panel panelMain;
        private Guna2Panel panelThongTin;
        private Guna2Panel panelHocSinh;
        private Guna2Panel panelTKB;
        private Guna2DataGridView dgvHocSinh;
        private TableLayoutPanel tableThoiKhoaBieu;
        private Label lblMaLop;
        private Label lblTenLop;
        private Label lblKhoi;
        private Label lblSiSo;
        private Label lblGVCN;
        private Label lblSDTGV;
        private Label lblEmailGV;
        private Label lblNamHoc;
        private Label lblThongBaoHS;
        private Label lblThongBaoTKB;
        private Label lblThongKe;
        private Guna2Button btnDong;
        private Guna2ComboBox cbHocKy;
        private Guna2TextBox txtTimKiemHS;
        private Guna2ComboBox cbHocSinhChuaPhanLop;
        private Guna2Button btnThemHocSinh;
        private Label lblSoLuongHSChuaPhanLop;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.panelThongTin = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMaLop = new System.Windows.Forms.Label();
            this.lblTenLop = new System.Windows.Forms.Label();
            this.lblKhoi = new System.Windows.Forms.Label();
            this.lblSiSo = new System.Windows.Forms.Label();
            this.lblGVCN = new System.Windows.Forms.Label();
            this.lblSDTGV = new System.Windows.Forms.Label();
            this.lblEmailGV = new System.Windows.Forms.Label();
            this.lblNamHoc = new System.Windows.Forms.Label();
            this.lblThongBaoHS = new System.Windows.Forms.Label();
            this.panelHocSinh = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvHocSinh = new Guna.UI2.WinForms.Guna2DataGridView();
            this.txtTimKiemHS = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbHocSinhChuaPhanLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnThemHocSinh = new Guna.UI2.WinForms.Guna2Button();
            this.lblSoLuongHSChuaPhanLop = new System.Windows.Forms.Label();
            this.panelTKB = new Guna.UI2.WinForms.Guna2Panel();
            this.tableThoiKhoaBieu = new System.Windows.Forms.TableLayoutPanel();
            this.lblThongBaoTKB = new System.Windows.Forms.Label();
            this.lblThongKe = new System.Windows.Forms.Label();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.cbHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChuyenLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Xoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMain.SuspendLayout();
            this.panelThongTin.SuspendLayout();
            this.panelHocSinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).BeginInit();
            this.panelTKB.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelThongTin);
            this.panelMain.Controls.Add(this.lblThongBaoHS);
            this.panelMain.Controls.Add(this.panelHocSinh);
            this.panelMain.Controls.Add(this.panelTKB);
            this.panelMain.Controls.Add(this.lblThongKe);
            this.panelMain.Controls.Add(this.btnDong);
            this.panelMain.Controls.Add(this.cbHocKy);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1057, 855);
            this.panelMain.TabIndex = 0;
            // 
            // panelThongTin
            // 
            this.panelThongTin.BorderColor = System.Drawing.Color.Gray;
            this.panelThongTin.BorderRadius = 10;
            this.panelThongTin.BorderThickness = 1;
            this.panelThongTin.Controls.Add(this.lblMaLop);
            this.panelThongTin.Controls.Add(this.lblTenLop);
            this.panelThongTin.Controls.Add(this.lblKhoi);
            this.panelThongTin.Controls.Add(this.lblSiSo);
            this.panelThongTin.Controls.Add(this.lblGVCN);
            this.panelThongTin.Controls.Add(this.lblSDTGV);
            this.panelThongTin.Controls.Add(this.lblEmailGV);
            this.panelThongTin.Controls.Add(this.lblNamHoc);
            this.panelThongTin.Location = new System.Drawing.Point(17, 17);
            this.panelThongTin.Name = "panelThongTin";
            this.panelThongTin.Size = new System.Drawing.Size(1027, 104);
            this.panelThongTin.TabIndex = 0;
            // 
            // lblMaLop
            // 
            this.lblMaLop.AutoSize = true;
            this.lblMaLop.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaLop.Location = new System.Drawing.Point(17, 13);
            this.lblMaLop.Name = "lblMaLop";
            this.lblMaLop.Size = new System.Drawing.Size(65, 21);
            this.lblMaLop.TabIndex = 0;
            this.lblMaLop.Text = "Mã lớp:";
            // 
            // lblTenLop
            // 
            this.lblTenLop.AutoSize = true;
            this.lblTenLop.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenLop.Location = new System.Drawing.Point(17, 39);
            this.lblTenLop.Name = "lblTenLop";
            this.lblTenLop.Size = new System.Drawing.Size(67, 21);
            this.lblTenLop.TabIndex = 1;
            this.lblTenLop.Text = "Tên lớp:";
            // 
            // lblKhoi
            // 
            this.lblKhoi.AutoSize = true;
            this.lblKhoi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblKhoi.Location = new System.Drawing.Point(214, 13);
            this.lblKhoi.Name = "lblKhoi";
            this.lblKhoi.Size = new System.Drawing.Size(42, 20);
            this.lblKhoi.TabIndex = 2;
            this.lblKhoi.Text = "Khối:";
            // 
            // lblSiSo
            // 
            this.lblSiSo.AutoSize = true;
            this.lblSiSo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSiSo.Location = new System.Drawing.Point(214, 39);
            this.lblSiSo.Name = "lblSiSo";
            this.lblSiSo.Size = new System.Drawing.Size(43, 20);
            this.lblSiSo.TabIndex = 3;
            this.lblSiSo.Text = "Sĩ số:";
            // 
            // lblGVCN
            // 
            this.lblGVCN.AutoSize = true;
            this.lblGVCN.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblGVCN.Location = new System.Drawing.Point(429, 13);
            this.lblGVCN.Name = "lblGVCN";
            this.lblGVCN.Size = new System.Drawing.Size(146, 20);
            this.lblGVCN.TabIndex = 4;
            this.lblGVCN.Text = "Giáo viên chủ nhiệm:";
            // 
            // lblSDTGV
            // 
            this.lblSDTGV.AutoSize = true;
            this.lblSDTGV.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSDTGV.Location = new System.Drawing.Point(429, 39);
            this.lblSDTGV.Name = "lblSDTGV";
            this.lblSDTGV.Size = new System.Drawing.Size(39, 20);
            this.lblSDTGV.TabIndex = 5;
            this.lblSDTGV.Text = "SĐT:";
            // 
            // lblEmailGV
            // 
            this.lblEmailGV.AutoSize = true;
            this.lblEmailGV.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblEmailGV.Location = new System.Drawing.Point(429, 65);
            this.lblEmailGV.Name = "lblEmailGV";
            this.lblEmailGV.Size = new System.Drawing.Size(49, 20);
            this.lblEmailGV.TabIndex = 6;
            this.lblEmailGV.Text = "Email:";
            // 
            // lblNamHoc
            // 
            this.lblNamHoc.AutoSize = true;
            this.lblNamHoc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNamHoc.Location = new System.Drawing.Point(739, 14);
            this.lblNamHoc.Name = "lblNamHoc";
            this.lblNamHoc.Size = new System.Drawing.Size(72, 20);
            this.lblNamHoc.TabIndex = 7;
            this.lblNamHoc.Text = "Năm học:";
            // 
            // lblThongBaoHS
            // 
            this.lblThongBaoHS.AutoSize = true;
            this.lblThongBaoHS.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblThongBaoHS.Location = new System.Drawing.Point(10, 169);
            this.lblThongBaoHS.Name = "lblThongBaoHS";
            this.lblThongBaoHS.Size = new System.Drawing.Size(141, 20);
            this.lblThongBaoHS.TabIndex = 0;
            this.lblThongBaoHS.Text = "Danh sách học sinh";
            // 
            // panelHocSinh
            // 
            this.panelHocSinh.BorderColor = System.Drawing.Color.Gray;
            this.panelHocSinh.BorderRadius = 10;
            this.panelHocSinh.BorderThickness = 1;
            this.panelHocSinh.Controls.Add(this.dgvHocSinh);
            this.panelHocSinh.Controls.Add(this.txtTimKiemHS);
            this.panelHocSinh.Controls.Add(this.cbHocSinhChuaPhanLop);
            this.panelHocSinh.Controls.Add(this.btnThemHocSinh);
            this.panelHocSinh.Controls.Add(this.lblSoLuongHSChuaPhanLop);
            this.panelHocSinh.Location = new System.Drawing.Point(7, 203);
            this.panelHocSinh.Name = "panelHocSinh";
            this.panelHocSinh.Size = new System.Drawing.Size(1046, 300);
            this.panelHocSinh.TabIndex = 1;
            this.panelHocSinh.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHocSinh_Paint);
            // 
            // dgvHocSinh
            // 
            this.dgvHocSinh.AllowUserToAddRows = false;
            this.dgvHocSinh.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvHocSinh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHocSinh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvHocSinh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHS,
            this.HoTen,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.ChuyenLop,
            this.Xoa});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHocSinh.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHocSinh.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHocSinh.Location = new System.Drawing.Point(3, 57);
            this.dgvHocSinh.Name = "dgvHocSinh";
            this.dgvHocSinh.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHocSinh.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvHocSinh.RowHeadersVisible = false;
            this.dgvHocSinh.Size = new System.Drawing.Size(1039, 240);
            this.dgvHocSinh.TabIndex = 1;
            this.dgvHocSinh.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvHocSinh.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvHocSinh.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvHocSinh.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvHocSinh.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvHocSinh.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvHocSinh.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHocSinh.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvHocSinh.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHocSinh.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvHocSinh.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHocSinh.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvHocSinh.ThemeStyle.HeaderStyle.Height = 23;
            this.dgvHocSinh.ThemeStyle.ReadOnly = true;
            this.dgvHocSinh.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvHocSinh.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHocSinh.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvHocSinh.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvHocSinh.ThemeStyle.RowsStyle.Height = 22;
            this.dgvHocSinh.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHocSinh.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvHocSinh.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHocSinh_CellClick);
            // 
            // txtTimKiemHS
            // 
            this.txtTimKiemHS.BorderColor = System.Drawing.Color.Gray;
            this.txtTimKiemHS.BorderRadius = 5;
            this.txtTimKiemHS.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiemHS.DefaultText = "";
            this.txtTimKiemHS.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiemHS.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiemHS.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiemHS.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiemHS.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiemHS.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiemHS.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiemHS.Location = new System.Drawing.Point(9, 5);
            this.txtTimKiemHS.Name = "txtTimKiemHS";
            this.txtTimKiemHS.PlaceholderText = "Tìm kiếm học sinh (mã, tên, SĐT, email)...";
            this.txtTimKiemHS.SelectedText = "";
            this.txtTimKiemHS.Size = new System.Drawing.Size(300, 32);
            this.txtTimKiemHS.TabIndex = 4;
            this.txtTimKiemHS.TextChanged += new System.EventHandler(this.txtTimKiemHS_TextChanged);
            // 
            // cbHocSinhChuaPhanLop
            // 
            this.cbHocSinhChuaPhanLop.BackColor = System.Drawing.Color.Transparent;
            this.cbHocSinhChuaPhanLop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHocSinhChuaPhanLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocSinhChuaPhanLop.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocSinhChuaPhanLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocSinhChuaPhanLop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbHocSinhChuaPhanLop.ForeColor = System.Drawing.Color.Black;
            this.cbHocSinhChuaPhanLop.ItemHeight = 35;
            this.cbHocSinhChuaPhanLop.Location = new System.Drawing.Point(315, 3);
            this.cbHocSinhChuaPhanLop.Name = "cbHocSinhChuaPhanLop";
            this.cbHocSinhChuaPhanLop.Size = new System.Drawing.Size(400, 41);
            this.cbHocSinhChuaPhanLop.TabIndex = 3;
            // 
            // btnThemHocSinh
            // 
            this.btnThemHocSinh.BorderRadius = 5;
            this.btnThemHocSinh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnThemHocSinh.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemHocSinh.ForeColor = System.Drawing.Color.White;
            this.btnThemHocSinh.Location = new System.Drawing.Point(731, 12);
            this.btnThemHocSinh.Name = "btnThemHocSinh";
            this.btnThemHocSinh.Size = new System.Drawing.Size(90, 32);
            this.btnThemHocSinh.TabIndex = 2;
            this.btnThemHocSinh.Text = "Thêm";
            this.btnThemHocSinh.Click += new System.EventHandler(this.btnThemHocSinh_Click);
            // 
            // lblSoLuongHSChuaPhanLop
            // 
            this.lblSoLuongHSChuaPhanLop.AutoSize = true;
            this.lblSoLuongHSChuaPhanLop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblSoLuongHSChuaPhanLop.ForeColor = System.Drawing.Color.Gray;
            this.lblSoLuongHSChuaPhanLop.Location = new System.Drawing.Point(750, 42);
            this.lblSoLuongHSChuaPhanLop.Name = "lblSoLuongHSChuaPhanLop";
            this.lblSoLuongHSChuaPhanLop.Size = new System.Drawing.Size(0, 15);
            this.lblSoLuongHSChuaPhanLop.TabIndex = 5;
            // 
            // panelTKB
            // 
            this.panelTKB.BorderColor = System.Drawing.Color.Gray;
            this.panelTKB.BorderRadius = 10;
            this.panelTKB.BorderThickness = 1;
            this.panelTKB.Controls.Add(this.tableThoiKhoaBieu);
            this.panelTKB.Controls.Add(this.lblThongBaoTKB);
            this.panelTKB.Location = new System.Drawing.Point(4, 549);
            this.panelTKB.Name = "panelTKB";
            this.panelTKB.Size = new System.Drawing.Size(1041, 262);
            this.panelTKB.TabIndex = 2;
            // 
            // tableThoiKhoaBieu
            // 
            this.tableThoiKhoaBieu.AutoScroll = true;
            this.tableThoiKhoaBieu.BackColor = System.Drawing.Color.White;
            this.tableThoiKhoaBieu.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableThoiKhoaBieu.ColumnCount = 7;
            this.tableThoiKhoaBieu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableThoiKhoaBieu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableThoiKhoaBieu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableThoiKhoaBieu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableThoiKhoaBieu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableThoiKhoaBieu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableThoiKhoaBieu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableThoiKhoaBieu.Location = new System.Drawing.Point(3, 44);
            this.tableThoiKhoaBieu.Name = "tableThoiKhoaBieu";
            this.tableThoiKhoaBieu.RowCount = 6;
            this.tableThoiKhoaBieu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableThoiKhoaBieu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableThoiKhoaBieu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableThoiKhoaBieu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableThoiKhoaBieu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableThoiKhoaBieu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableThoiKhoaBieu.Size = new System.Drawing.Size(1035, 218);
            this.tableThoiKhoaBieu.TabIndex = 1;
            // 
            // lblThongBaoTKB
            // 
            this.lblThongBaoTKB.AutoSize = true;
            this.lblThongBaoTKB.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblThongBaoTKB.Location = new System.Drawing.Point(9, 9);
            this.lblThongBaoTKB.Name = "lblThongBaoTKB";
            this.lblThongBaoTKB.Size = new System.Drawing.Size(111, 20);
            this.lblThongBaoTKB.TabIndex = 0;
            this.lblThongBaoTKB.Text = "Thời khóa biểu";
            // 
            // lblThongKe
            // 
            this.lblThongKe.AutoSize = true;
            this.lblThongKe.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblThongKe.Location = new System.Drawing.Point(7, 516);
            this.lblThongKe.Name = "lblThongKe";
            this.lblThongKe.Size = new System.Drawing.Size(77, 20);
            this.lblThongKe.TabIndex = 3;
            this.lblThongKe.Text = "Thống kê:";
            // 
            // btnDong
            // 
            this.btnDong.BorderRadius = 10;
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(939, 817);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(111, 35);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // cbHocKy
            // 
            this.cbHocKy.BackColor = System.Drawing.Color.Transparent;
            this.cbHocKy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocKy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocKy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocKy.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbHocKy.ForeColor = System.Drawing.Color.Black;
            this.cbHocKy.ItemHeight = 30;
            this.cbHocKy.Location = new System.Drawing.Point(17, 130);
            this.cbHocKy.Name = "cbHocKy";
            this.cbHocKy.Size = new System.Drawing.Size(172, 36);
            this.cbHocKy.TabIndex = 5;
            this.cbHocKy.SelectedIndexChanged += new System.EventHandler(this.cbHocKy_SelectedIndexChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Thứ";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tiết";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Môn học";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Giáo viên";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Phòng";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // MaHS
            // 
            this.MaHS.FillWeight = 57.79731F;
            this.MaHS.HeaderText = "Mã HS";
            this.MaHS.Name = "MaHS";
            this.MaHS.ReadOnly = true;
            // 
            // HoTen
            // 
            this.HoTen.FillWeight = 96.6277F;
            this.HoTen.HeaderText = "Họ tên";
            this.HoTen.Name = "HoTen";
            this.HoTen.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.FillWeight = 96.6277F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Ngày sinh";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.FillWeight = 96.6277F;
            this.dataGridViewTextBoxColumn9.HeaderText = "Giới tính";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.FillWeight = 96.6277F;
            this.dataGridViewTextBoxColumn10.HeaderText = "SĐT";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.FillWeight = 162.4366F;
            this.dataGridViewTextBoxColumn11.HeaderText = "Email";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // ChuyenLop
            // 
            this.ChuyenLop.FillWeight = 96.6277F;
            this.ChuyenLop.HeaderText = "Chuyển lớp";
            this.ChuyenLop.Name = "ChuyenLop";
            this.ChuyenLop.ReadOnly = true;
            // 
            // Xoa
            // 
            this.Xoa.FillWeight = 96.6277F;
            this.Xoa.HeaderText = "Xóa";
            this.Xoa.Name = "Xoa";
            this.Xoa.ReadOnly = true;
            // 
            // ChiTietLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 855);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChiTietLop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết lớp học";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelThongTin.ResumeLayout(false);
            this.panelThongTin.PerformLayout();
            this.panelHocSinh.ResumeLayout(false);
            this.panelHocSinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHocSinh)).EndInit();
            this.panelTKB.ResumeLayout(false);
            this.panelTKB.PerformLayout();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn MaHS;
        private DataGridViewTextBoxColumn HoTen;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn ChuyenLop;
        private DataGridViewTextBoxColumn Xoa;
    }
}

