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
            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.panelFilters = new Panel();
            this.lblHocKy = new Label();
            this.cbHocKy = new Guna2ComboBox();
            this.lblKhoi = new Label();
            this.cbKhoi = new Guna2ComboBox();
            this.lblMonHoc = new Label();
            this.cbMon = new Guna2ComboBox();
            this.lblMaxTiet = new Label();
            this.numMaxTiet = new Guna2NumericUpDown();
            this.swAllowNonPrimary = new Guna2ToggleSwitch();
            this.lblStatus = new Label();
            this.panelGrid = new Panel();
            this.grid = new Guna2DataGridView();
            this.panelActions = new Panel();
            this.progressBar = new Guna2ProgressBar();
            this.btnGenerate = new Guna2Button();
            this.btnValidate = new Guna2Button();
            this.btnSaveTemp = new Guna2Button();
            this.btnAccept = new Guna2Button();
            this.btnRollback = new Guna2Button();
            this.btnClose = new Guna2Button();
            
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
            this.panelHeader.BackColor = Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Location = new Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new Size(1109, 60);
            this.panelHeader.TabIndex = 0;
            
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(300, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Phân Công Giảng Dạy Tự Động";
            
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = Color.FromArgb(248, 250, 252);
            this.panelFilters.Controls.Add(this.lblHocKy);
            this.panelFilters.Controls.Add(this.cbHocKy);
            this.panelFilters.Controls.Add(this.lblKhoi);
            this.panelFilters.Controls.Add(this.cbKhoi);
            this.panelFilters.Controls.Add(this.lblMonHoc);
            this.panelFilters.Controls.Add(this.cbMon);
            this.panelFilters.Controls.Add(this.lblMaxTiet);
            this.panelFilters.Controls.Add(this.numMaxTiet);
            this.panelFilters.Controls.Add(this.swAllowNonPrimary);
            this.panelFilters.Dock = DockStyle.Top;
            this.panelFilters.Location = new Point(0, 60);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new Padding(20, 15, 20, 15);
            this.panelFilters.Size = new Size(1109, 120);
            this.panelFilters.TabIndex = 1;
            
            // 
            // lblHocKy
            // 
            this.lblHocKy.AutoSize = true;
            this.lblHocKy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblHocKy.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblHocKy.Location = new Point(20, 20);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new Size(55, 15);
            this.lblHocKy.TabIndex = 0;
            this.lblHocKy.Text = "Học kỳ:";
            
            // 
            // cbHocKy
            // 
            this.cbHocKy.BackColor = Color.Transparent;
            this.cbHocKy.BorderColor = Color.FromArgb(203, 213, 225);
            this.cbHocKy.BorderRadius = 8;
            this.cbHocKy.DrawMode = DrawMode.OwnerDrawFixed;
            this.cbHocKy.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbHocKy.FocusedColor = Color.FromArgb(59, 130, 246);
            this.cbHocKy.Font = new Font("Segoe UI", 9F);
            this.cbHocKy.ForeColor = Color.FromArgb(30, 41, 59);
            this.cbHocKy.ItemHeight = 30;
            this.cbHocKy.Location = new Point(85, 15);
            this.cbHocKy.Name = "cbHocKy";
            this.cbHocKy.Size = new Size(280, 36);
            this.cbHocKy.TabIndex = 1;
            this.cbHocKy.SelectedIndexChanged += new EventHandler(this.cbHocKy_SelectedIndexChanged);
            
            // 
            // lblKhoi
            // 
            this.lblKhoi.AutoSize = true;
            this.lblKhoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblKhoi.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblKhoi.Location = new Point(385, 20);
            this.lblKhoi.Name = "lblKhoi";
            this.lblKhoi.Size = new Size(40, 15);
            this.lblKhoi.TabIndex = 2;
            this.lblKhoi.Text = "Khối:";
            
            // 
            // cbKhoi
            // 
            this.cbKhoi.BackColor = Color.Transparent;
            this.cbKhoi.BorderColor = Color.FromArgb(203, 213, 225);
            this.cbKhoi.BorderRadius = 8;
            this.cbKhoi.DrawMode = DrawMode.OwnerDrawFixed;
            this.cbKhoi.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbKhoi.FocusedColor = Color.FromArgb(59, 130, 246);
            this.cbKhoi.Font = new Font("Segoe UI", 9F);
            this.cbKhoi.ForeColor = Color.FromArgb(30, 41, 59);
            this.cbKhoi.ItemHeight = 30;
            this.cbKhoi.Location = new Point(435, 15);
            this.cbKhoi.Name = "cbKhoi";
            this.cbKhoi.Size = new Size(120, 36);
            this.cbKhoi.TabIndex = 3;
            
            // 
            // lblMonHoc
            // 
            this.lblMonHoc.AutoSize = true;
            this.lblMonHoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblMonHoc.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblMonHoc.Location = new Point(575, 20);
            this.lblMonHoc.Name = "lblMonHoc";
            this.lblMonHoc.Size = new Size(60, 15);
            this.lblMonHoc.TabIndex = 4;
            this.lblMonHoc.Text = "Môn học:";
            
            // 
            // cbMon
            // 
            this.cbMon.BackColor = Color.Transparent;
            this.cbMon.BorderColor = Color.FromArgb(203, 213, 225);
            this.cbMon.BorderRadius = 8;
            this.cbMon.DrawMode = DrawMode.OwnerDrawFixed;
            this.cbMon.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbMon.FocusedColor = Color.FromArgb(59, 130, 246);
            this.cbMon.Font = new Font("Segoe UI", 9F);
            this.cbMon.ForeColor = Color.FromArgb(30, 41, 59);
            this.cbMon.ItemHeight = 30;
            this.cbMon.Location = new Point(645, 15);
            this.cbMon.Name = "cbMon";
            this.cbMon.Size = new Size(200, 36);
            this.cbMon.TabIndex = 5;
            
            // 
            // lblMaxTiet (Ẩn - không dùng giới hạn tiết/tuần)
            // 
            this.lblMaxTiet.Visible = false;
            
            // 
            // numMaxTiet (Ẩn - không dùng giới hạn tiết/tuần)
            // 
            this.numMaxTiet.Visible = false;
            
            // 
            // swAllowNonPrimary (Ẩn - chỉ cho phép GV dạy đúng chuyên môn)
            // 
            this.swAllowNonPrimary.Visible = false;
            
            // 
            // lblAllowNonPrimary (Ẩn)
            // 
            Label lblAllowNonPrimary = new Label();
            lblAllowNonPrimary.Visible = false;
            this.panelFilters.Controls.Add(lblAllowNonPrimary);
            
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.grid);
            this.panelGrid.Dock = DockStyle.Fill;
            this.panelGrid.Location = new Point(0, 180);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new Padding(20);
            this.panelGrid.Size = new Size(1109, 444);
            this.panelGrid.TabIndex = 2;
            
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = Color.White;
            this.grid.BorderStyle = BorderStyle.None;
            this.grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.grid.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            this.grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            this.grid.ColumnHeadersHeight = 40;
            this.grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Dock = DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.GridColor = Color.FromArgb(241, 245, 249);
            this.grid.Location = new Point(20, 20);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = false;
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 35;
            this.grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new Size(1069, 404);
            this.grid.TabIndex = 0;
            this.grid.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(248, 250, 252);
            this.grid.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            this.grid.ThemeStyle.BackColor = Color.White;
            this.grid.ThemeStyle.GridColor = Color.FromArgb(241, 245, 249);
            this.grid.ThemeStyle.HeaderStyle.BackColor = Color.White;
            this.grid.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            this.grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grid.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(100, 116, 139);
            this.grid.ThemeStyle.ReadOnly = false;
            this.grid.ThemeStyle.RowsStyle.BackColor = Color.White;
            this.grid.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            this.grid.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(30, 41, 59);
            this.grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            this.grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            
            // 
            // panelActions
            // 
            this.panelActions.BackColor = Color.White;
            this.panelActions.Controls.Add(this.lblStatus);
            this.panelActions.Controls.Add(this.progressBar);
            this.panelActions.Controls.Add(this.btnGenerate);
            this.panelActions.Controls.Add(this.btnValidate);
            this.panelActions.Controls.Add(this.btnSaveTemp);
            this.panelActions.Controls.Add(this.btnAccept);
            this.panelActions.Controls.Add(this.btnRollback);
            this.panelActions.Controls.Add(this.btnClose);
            this.panelActions.Dock = DockStyle.Bottom;
            this.panelActions.Location = new Point(0, 624);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new Padding(20, 15, 20, 15);
            this.panelActions.Size = new Size(1109, 120);
            this.panelActions.TabIndex = 3;
            
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = Color.FromArgb(59, 130, 246);
            this.lblStatus.Location = new Point(20, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(200, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "📌 Chọn học kỳ và nhấn 'Tạo tự động'";
            
            // 
            // progressBar
            // 
            this.progressBar.BackColor = Color.Transparent;
            this.progressBar.BorderRadius = 4;
            this.progressBar.FillColor = Color.FromArgb(241, 245, 249);
            this.progressBar.Location = new Point(20, 45);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = Color.FromArgb(59, 130, 246);
            this.progressBar.ProgressColor2 = Color.FromArgb(59, 130, 246);
            this.progressBar.Size = new Size(1069, 10);
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            
            // 
            // btnGenerate
            // 
            this.btnGenerate.BorderRadius = 8;
            this.btnGenerate.DisabledState.BorderColor = Color.DarkGray;
            this.btnGenerate.DisabledState.CustomBorderColor = Color.DarkGray;
            this.btnGenerate.DisabledState.FillColor = Color.Gray;
            this.btnGenerate.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            this.btnGenerate.FillColor = Color.FromArgb(59, 130, 246);
            this.btnGenerate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnGenerate.ForeColor = Color.White;
            this.btnGenerate.Location = new Point(20, 65);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new Size(130, 40);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Tạo tự động";
            this.btnGenerate.Click += new EventHandler(this.BtnGenerate_Click);
            
            // 
            // btnValidate
            // 
            this.btnValidate.BorderRadius = 8;
            this.btnValidate.DisabledState.BorderColor = Color.DarkGray;
            this.btnValidate.DisabledState.CustomBorderColor = Color.DarkGray;
            this.btnValidate.DisabledState.FillColor = Color.Gray;
            this.btnValidate.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            this.btnValidate.FillColor = Color.FromArgb(34, 197, 94);
            this.btnValidate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnValidate.ForeColor = Color.White;
            this.btnValidate.Location = new Point(160, 65);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new Size(110, 40);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "Kiểm tra";
            this.btnValidate.Enabled = false;
            this.btnValidate.Click += new EventHandler(this.BtnValidate_Click);
            
            // 
            // btnSaveTemp
            // 
            this.btnSaveTemp.BorderRadius = 8;
            this.btnSaveTemp.DisabledState.BorderColor = Color.DarkGray;
            this.btnSaveTemp.DisabledState.CustomBorderColor = Color.DarkGray;
            this.btnSaveTemp.DisabledState.FillColor = Color.Gray;
            this.btnSaveTemp.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            this.btnSaveTemp.FillColor = Color.FromArgb(234, 88, 12);
            this.btnSaveTemp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSaveTemp.ForeColor = Color.White;
            this.btnSaveTemp.Location = new Point(280, 65);
            this.btnSaveTemp.Name = "btnSaveTemp";
            this.btnSaveTemp.Size = new Size(110, 40);
            this.btnSaveTemp.TabIndex = 4;
            this.btnSaveTemp.Text = "Lưu tạm";
            this.btnSaveTemp.Enabled = false;
            this.btnSaveTemp.Click += new EventHandler(this.BtnSaveTemp_Click);
            
            // 
            // btnAccept
            // 
            this.btnAccept.BorderRadius = 8;
            this.btnAccept.DisabledState.BorderColor = Color.DarkGray;
            this.btnAccept.DisabledState.CustomBorderColor = Color.DarkGray;
            this.btnAccept.DisabledState.FillColor = Color.Gray;
            this.btnAccept.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            this.btnAccept.FillColor = Color.FromArgb(22, 163, 74);
            this.btnAccept.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAccept.ForeColor = Color.White;
            this.btnAccept.Location = new Point(400, 65);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new Size(120, 40);
            this.btnAccept.TabIndex = 5;
            this.btnAccept.Text = "Chấp nhận";
            this.btnAccept.Enabled = false;
            this.btnAccept.Click += new EventHandler(this.BtnAccept_Click);
            
            // 
            // btnRollback
            // 
            this.btnRollback.BorderRadius = 8;
            this.btnRollback.DisabledState.BorderColor = Color.DarkGray;
            this.btnRollback.DisabledState.CustomBorderColor = Color.DarkGray;
            this.btnRollback.DisabledState.FillColor = Color.Gray;
            this.btnRollback.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            this.btnRollback.FillColor = Color.FromArgb(220, 38, 38);
            this.btnRollback.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRollback.ForeColor = Color.White;
            this.btnRollback.Location = new Point(530, 65);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new Size(130, 40);
            this.btnRollback.TabIndex = 6;
            this.btnRollback.Text = "Xóa lưu tạm";
            this.btnRollback.Click += new EventHandler(this.BtnRollback_Click);
            
            // 
            // btnClose
            // 
            this.btnClose.BorderRadius = 8;
            this.btnClose.DisabledState.BorderColor = Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = Color.DarkGray;
            this.btnClose.DisabledState.FillColor = Color.Gray;
            this.btnClose.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            this.btnClose.FillColor = Color.FromArgb(100, 116, 139);
            this.btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.Location = new Point(959, 65);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(130, 40);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new EventHandler(this.BtnClose_Click);
            
            // 
            // frmAutoPhanCongPreview
            // 
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.ClientSize = new Size(1109, 744);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelActions);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(900, 600);
            this.Name = "frmAutoPhanCongPreview";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Phân Công Giảng Dạy Tự Động";
            this.Load += new EventHandler(this.frmAutoPhanCongPreview_Load);
            
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
    }
}
