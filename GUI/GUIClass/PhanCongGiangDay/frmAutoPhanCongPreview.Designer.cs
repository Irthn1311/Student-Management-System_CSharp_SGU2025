using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class frmAutoPhanCongPreview
    {
        private System.ComponentModel.IContainer components = null;
        
        // Controls
        private Panel panelHeader;
        private Panel panelFilters;
        private Panel panelGrid;
        private Panel panelActions;
        
        private Label lblTitle;
        private Label lblHocKy;
        private Label lblKhoi;
        private Label lblMonHoc;
        private Label lblMaxTiet;
        private Label lblStatus;
        
        private Guna2ComboBox cbHocKy;
        private Guna2ComboBox cbKhoi;
        private Guna2ComboBox cbMon;
        private Guna2NumericUpDown numMaxTiet;
        private Guna2ToggleSwitch swAllowNonPrimary;
        
        private Guna2DataGridView grid;
        
        private Guna2Button btnGenerate;
        private Guna2Button btnValidate;
        private Guna2Button btnSaveTemp;
        private Guna2Button btnAccept;
        private Guna2Button btnRollback;
        private Guna2Button btnClose;
        
        private Guna2ProgressBar progressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.lblHocKy = new System.Windows.Forms.Label();
            this.cbHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblKhoi = new System.Windows.Forms.Label();
            this.cbKhoi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMonHoc = new System.Windows.Forms.Label();
            this.cbMon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMaxTiet = new System.Windows.Forms.Label();
            this.numMaxTiet = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.swAllowNonPrimary = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblAllowNonPrimary = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.grid = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panelActions = new System.Windows.Forms.Panel();
            this.progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.btnGenerate = new Guna.UI2.WinForms.Guna2Button();
            this.btnValidate = new Guna.UI2.WinForms.Guna2Button();
            this.btnSaveTemp = new Guna.UI2.WinForms.Guna2Button();
            this.btnAccept = new Guna.UI2.WinForms.Guna2Button();
            this.btnRollback = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.panelHeader.SuspendLayout();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTiet)).BeginInit();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1109, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(334, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Phân Công Giảng Dạy Tự Động";
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFilters.Controls.Add(this.lblHocKy);
            this.panelFilters.Controls.Add(this.cbHocKy);
            this.panelFilters.Controls.Add(this.lblKhoi);
            this.panelFilters.Controls.Add(this.cbKhoi);
            this.panelFilters.Controls.Add(this.lblMonHoc);
            this.panelFilters.Controls.Add(this.cbMon);
            this.panelFilters.Controls.Add(this.lblMaxTiet);
            this.panelFilters.Controls.Add(this.numMaxTiet);
            this.panelFilters.Controls.Add(this.swAllowNonPrimary);
            this.panelFilters.Controls.Add(this.lblAllowNonPrimary);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 60);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelFilters.Size = new System.Drawing.Size(1109, 120);
            this.panelFilters.TabIndex = 1;
            // 
            // lblHocKy
            // 
            this.lblHocKy.AutoSize = true;
            this.lblHocKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblHocKy.Location = new System.Drawing.Point(20, 20);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new System.Drawing.Size(48, 15);
            this.lblHocKy.TabIndex = 0;
            this.lblHocKy.Text = "Học kỳ:";
            // 
            // cbHocKy
            // 
            this.cbHocKy.BackColor = System.Drawing.Color.Transparent;
            this.cbHocKy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.cbHocKy.BorderRadius = 8;
            this.cbHocKy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocKy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbHocKy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbHocKy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cbHocKy.ItemHeight = 30;
            this.cbHocKy.Location = new System.Drawing.Point(85, 15);
            this.cbHocKy.Name = "cbHocKy";
            this.cbHocKy.Size = new System.Drawing.Size(389, 36);
            this.cbHocKy.TabIndex = 1;
            this.cbHocKy.SelectedIndexChanged += new System.EventHandler(this.cbHocKy_SelectedIndexChanged);
            // 
            // lblKhoi
            // 
            this.lblKhoi.AutoSize = true;
            this.lblKhoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKhoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblKhoi.Location = new System.Drawing.Point(494, 20);
            this.lblKhoi.Name = "lblKhoi";
            this.lblKhoi.Size = new System.Drawing.Size(35, 15);
            this.lblKhoi.TabIndex = 2;
            this.lblKhoi.Text = "Khối:";
            // 
            // cbKhoi
            // 
            this.cbKhoi.BackColor = System.Drawing.Color.Transparent;
            this.cbKhoi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.cbKhoi.BorderRadius = 8;
            this.cbKhoi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbKhoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhoi.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbKhoi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbKhoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbKhoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cbKhoi.ItemHeight = 30;
            this.cbKhoi.Location = new System.Drawing.Point(536, 15);
            this.cbKhoi.Name = "cbKhoi";
            this.cbKhoi.Size = new System.Drawing.Size(120, 36);
            this.cbKhoi.TabIndex = 3;
            // 
            // lblMonHoc
            // 
            this.lblMonHoc.AutoSize = true;
            this.lblMonHoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMonHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMonHoc.Location = new System.Drawing.Point(673, 20);
            this.lblMonHoc.Name = "lblMonHoc";
            this.lblMonHoc.Size = new System.Drawing.Size(58, 15);
            this.lblMonHoc.TabIndex = 4;
            this.lblMonHoc.Text = "Môn học:";
            // 
            // cbMon
            // 
            this.cbMon.BackColor = System.Drawing.Color.Transparent;
            this.cbMon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.cbMon.BorderRadius = 8;
            this.cbMon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMon.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbMon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbMon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cbMon.ItemHeight = 30;
            this.cbMon.Location = new System.Drawing.Point(739, 15);
            this.cbMon.Name = "cbMon";
            this.cbMon.Size = new System.Drawing.Size(200, 36);
            this.cbMon.TabIndex = 5;
            // 
            // lblMaxTiet
            // 
            this.lblMaxTiet.Location = new System.Drawing.Point(0, 0);
            this.lblMaxTiet.Name = "lblMaxTiet";
            this.lblMaxTiet.Size = new System.Drawing.Size(100, 23);
            this.lblMaxTiet.TabIndex = 6;
            this.lblMaxTiet.Visible = false;
            // 
            // numMaxTiet
            // 
            this.numMaxTiet.BackColor = System.Drawing.Color.Transparent;
            this.numMaxTiet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numMaxTiet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numMaxTiet.Location = new System.Drawing.Point(0, 0);
            this.numMaxTiet.Name = "numMaxTiet";
            this.numMaxTiet.Size = new System.Drawing.Size(100, 36);
            this.numMaxTiet.TabIndex = 7;
            this.numMaxTiet.Visible = false;
            // 
            // swAllowNonPrimary
            // 
            this.swAllowNonPrimary.Location = new System.Drawing.Point(0, 0);
            this.swAllowNonPrimary.Name = "swAllowNonPrimary";
            this.swAllowNonPrimary.Size = new System.Drawing.Size(35, 20);
            this.swAllowNonPrimary.TabIndex = 8;
            this.swAllowNonPrimary.Visible = false;
            // 
            // lblAllowNonPrimary
            // 
            this.lblAllowNonPrimary.Location = new System.Drawing.Point(0, 0);
            this.lblAllowNonPrimary.Name = "lblAllowNonPrimary";
            this.lblAllowNonPrimary.Size = new System.Drawing.Size(100, 23);
            this.lblAllowNonPrimary.TabIndex = 9;
            this.lblAllowNonPrimary.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblStatus.Location = new System.Drawing.Point(20, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(208, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "📌 Chọn học kỳ và nhấn \'Tạo tự động\'";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.grid);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 180);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(20);
            this.panelGrid.Size = new System.Drawing.Size(1109, 444);
            this.panelGrid.TabIndex = 2;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.grid.Location = new System.Drawing.Point(20, 20);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 35;
            this.grid.Size = new System.Drawing.Size(1069, 404);
            this.grid.TabIndex = 0;
            this.grid.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.grid.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.grid.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.grid.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.grid.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.grid.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.grid.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.grid.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.White;
            this.grid.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grid.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grid.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.grid.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.ThemeStyle.HeaderStyle.Height = 40;
            this.grid.ThemeStyle.ReadOnly = false;
            this.grid.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.grid.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grid.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grid.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grid.ThemeStyle.RowsStyle.Height = 35;
            this.grid.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.grid.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.lblStatus);
            this.panelActions.Controls.Add(this.progressBar);
            this.panelActions.Controls.Add(this.btnGenerate);
            this.panelActions.Controls.Add(this.btnValidate);
            this.panelActions.Controls.Add(this.btnSaveTemp);
            this.panelActions.Controls.Add(this.btnAccept);
            this.panelActions.Controls.Add(this.btnRollback);
            this.panelActions.Controls.Add(this.btnClose);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 624);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelActions.Size = new System.Drawing.Size(1109, 120);
            this.panelActions.TabIndex = 3;
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.Transparent;
            this.progressBar.BorderRadius = 4;
            this.progressBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.progressBar.Location = new System.Drawing.Point(20, 45);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.progressBar.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.progressBar.Size = new System.Drawing.Size(1069, 10);
            this.progressBar.TabIndex = 1;
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.progressBar.Visible = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BorderRadius = 8;
            this.btnGenerate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerate.DisabledState.FillColor = System.Drawing.Color.Gray;
            this.btnGenerate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGenerate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(20, 65);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(130, 40);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Tạo tự động";
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.BorderRadius = 8;
            this.btnValidate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnValidate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnValidate.DisabledState.FillColor = System.Drawing.Color.Gray;
            this.btnValidate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnValidate.Enabled = false;
            this.btnValidate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnValidate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnValidate.ForeColor = System.Drawing.Color.White;
            this.btnValidate.Location = new System.Drawing.Point(160, 65);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(110, 40);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "Kiểm tra";
            this.btnValidate.Click += new System.EventHandler(this.BtnValidate_Click);
            // 
            // btnSaveTemp
            // 
            this.btnSaveTemp.BorderRadius = 8;
            this.btnSaveTemp.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveTemp.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveTemp.DisabledState.FillColor = System.Drawing.Color.Gray;
            this.btnSaveTemp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSaveTemp.Enabled = false;
            this.btnSaveTemp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnSaveTemp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveTemp.ForeColor = System.Drawing.Color.White;
            this.btnSaveTemp.Location = new System.Drawing.Point(280, 65);
            this.btnSaveTemp.Name = "btnSaveTemp";
            this.btnSaveTemp.Size = new System.Drawing.Size(110, 40);
            this.btnSaveTemp.TabIndex = 4;
            this.btnSaveTemp.Text = "Lưu tạm";
            this.btnSaveTemp.Click += new System.EventHandler(this.BtnSaveTemp_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.BorderRadius = 8;
            this.btnAccept.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAccept.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAccept.DisabledState.FillColor = System.Drawing.Color.Gray;
            this.btnAccept.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAccept.Enabled = false;
            this.btnAccept.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAccept.ForeColor = System.Drawing.Color.White;
            this.btnAccept.Location = new System.Drawing.Point(400, 65);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(120, 40);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "Chấp nhận";
            this.btnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // btnRollback
            // 
            this.btnRollback.BorderRadius = 8;
            this.btnRollback.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRollback.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRollback.DisabledState.FillColor = System.Drawing.Color.Gray;
            this.btnRollback.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRollback.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnRollback.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRollback.ForeColor = System.Drawing.Color.White;
            this.btnRollback.Location = new System.Drawing.Point(530, 65);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(130, 40);
            this.btnRollback.TabIndex = 6;
            this.btnRollback.Text = "Xóa lưu tạm";
            this.btnRollback.Click += new System.EventHandler(this.BtnRollback_Click);
            // 
            // btnClose
            // 
            this.btnClose.BorderRadius = 8;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.Gray;
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(959, 65);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 40);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // frmAutoPhanCongPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1109, 744);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelActions);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmAutoPhanCongPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân Công Giảng Dạy Tự Động";
            this.Load += new System.EventHandler(this.frmAutoPhanCongPreview_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTiet)).EndInit();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.panelActions.PerformLayout();
            this.ResumeLayout(false);

        }
        
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        private Label lblAllowNonPrimary;
    }
}
