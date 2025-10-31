using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI.PhanCong
{
    partial class frmAutoPhanCongPreview
    {
        private System.ComponentModel.IContainer components = null;

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
            this.panelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.grid = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btnRollback = new Guna.UI2.WinForms.Guna2Button();
            this.btnValidate = new Guna.UI2.WinForms.Guna2Button();
            this.btnSaveTemp = new Guna.UI2.WinForms.Guna2Button();
            this.btnAccept = new Guna.UI2.WinForms.Guna2Button();
            this.btnGenerate = new Guna.UI2.WinForms.Guna2Button();
            this.progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.lblStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelFilters = new Guna.UI2.WinForms.Guna2Panel();
            this.swAllowNonPrimary = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblAllow = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.numMaxTiet = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblMaxTiet = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cbMon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMon = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cbKhoi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblKhoi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cbHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblHocKy = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTiet)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.Transparent;
            this.panelContainer.BorderRadius = 16;
            this.panelContainer.Controls.Add(this.grid);
            this.panelContainer.Controls.Add(this.panelButtons);
            this.panelContainer.Controls.Add(this.progressBar);
            this.panelContainer.Controls.Add(this.lblStatus);
            this.panelContainer.Controls.Add(this.panelFilters);
            this.panelContainer.Controls.Add(this.panelHeader);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.FillColor = System.Drawing.Color.White;
            this.panelContainer.Location = new System.Drawing.Point(20, 20);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1069, 704);
            this.panelContainer.TabIndex = 0;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.ColumnHeadersHeight = 48;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.grid.Location = new System.Drawing.Point(0, 251);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 46;
            this.grid.Size = new System.Drawing.Size(1069, 370);
            this.grid.TabIndex = 4;
            this.grid.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.grid.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grid.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grid.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.grid.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grid.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.grid.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.grid.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.grid.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grid.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.grid.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.grid.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.ThemeStyle.HeaderStyle.Height = 48;
            this.grid.ThemeStyle.ReadOnly = false;
            this.grid.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.grid.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grid.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grid.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grid.ThemeStyle.RowsStyle.Height = 46;
            this.grid.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.grid.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.btnRollback);
            this.panelButtons.Controls.Add(this.btnValidate);
            this.panelButtons.Controls.Add(this.btnSaveTemp);
            this.panelButtons.Controls.Add(this.btnAccept);
            this.panelButtons.Controls.Add(this.btnGenerate);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelButtons.Location = new System.Drawing.Point(0, 621);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(25, 16, 25, 16);
            this.panelButtons.Size = new System.Drawing.Size(1069, 83);
            this.panelButtons.TabIndex = 5;
            // 
            // btnRollback
            // 
            this.btnRollback.Animated = true;
            this.btnRollback.BackColor = System.Drawing.Color.Transparent;
            this.btnRollback.BorderRadius = 10;
            this.btnRollback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRollback.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRollback.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRollback.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnRollback.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRollback.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnRollback.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRollback.ForeColor = System.Drawing.Color.White;
            this.btnRollback.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnRollback.Location = new System.Drawing.Point(360, 18);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.ShadowDecoration.BorderRadius = 10;
            this.btnRollback.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnRollback.ShadowDecoration.Depth = 8;
            this.btnRollback.ShadowDecoration.Enabled = true;
            this.btnRollback.Size = new System.Drawing.Size(140, 48);
            this.btnRollback.TabIndex = 4;
            this.btnRollback.Text = "🗑 Xóa tạm";
            this.btnRollback.Click += new System.EventHandler(this.BtnRollback_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Animated = true;
            this.btnValidate.BackColor = System.Drawing.Color.Transparent;
            this.btnValidate.BorderRadius = 10;
            this.btnValidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnValidate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnValidate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnValidate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnValidate.Enabled = false;
            this.btnValidate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(146)))), ((int)(((byte)(60)))));
            this.btnValidate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnValidate.ForeColor = System.Drawing.Color.White;
            this.btnValidate.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnValidate.Location = new System.Drawing.Point(525, 18);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.ShadowDecoration.BorderRadius = 10;
            this.btnValidate.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(146)))), ((int)(((byte)(60)))));
            this.btnValidate.ShadowDecoration.Depth = 8;
            this.btnValidate.ShadowDecoration.Enabled = true;
            this.btnValidate.Size = new System.Drawing.Size(140, 48);
            this.btnValidate.TabIndex = 1;
            this.btnValidate.Text = "✓ Kiểm tra";
            this.btnValidate.Click += new System.EventHandler(this.BtnValidate_Click);
            // 
            // btnSaveTemp
            // 
            this.btnSaveTemp.Animated = true;
            this.btnSaveTemp.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveTemp.BorderRadius = 10;
            this.btnSaveTemp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveTemp.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveTemp.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSaveTemp.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnSaveTemp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSaveTemp.Enabled = false;
            this.btnSaveTemp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(189)))), ((int)(((byte)(248)))));
            this.btnSaveTemp.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveTemp.ForeColor = System.Drawing.Color.White;
            this.btnSaveTemp.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnSaveTemp.Location = new System.Drawing.Point(690, 18);
            this.btnSaveTemp.Name = "btnSaveTemp";
            this.btnSaveTemp.ShadowDecoration.BorderRadius = 10;
            this.btnSaveTemp.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(189)))), ((int)(((byte)(248)))));
            this.btnSaveTemp.ShadowDecoration.Depth = 8;
            this.btnSaveTemp.ShadowDecoration.Enabled = true;
            this.btnSaveTemp.Size = new System.Drawing.Size(140, 48);
            this.btnSaveTemp.TabIndex = 2;
            this.btnSaveTemp.Text = "💾 Lưu tạm";
            this.btnSaveTemp.Click += new System.EventHandler(this.BtnSaveTemp_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Animated = true;
            this.btnAccept.BackColor = System.Drawing.Color.Transparent;
            this.btnAccept.BorderRadius = 10;
            this.btnAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccept.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAccept.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAccept.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnAccept.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAccept.Enabled = false;
            this.btnAccept.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAccept.ForeColor = System.Drawing.Color.White;
            this.btnAccept.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnAccept.Location = new System.Drawing.Point(855, 18);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.ShadowDecoration.BorderRadius = 10;
            this.btnAccept.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnAccept.ShadowDecoration.Depth = 8;
            this.btnAccept.ShadowDecoration.Enabled = true;
            this.btnAccept.Size = new System.Drawing.Size(160, 48);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "✓ Chấp nhận";
            this.btnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Animated = true;
            this.btnGenerate.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerate.BorderRadius = 10;
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGenerate.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGenerate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGenerate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnGenerate.Location = new System.Drawing.Point(30, 18);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.ShadowDecoration.BorderRadius = 10;
            this.btnGenerate.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnGenerate.ShadowDecoration.Depth = 10;
            this.btnGenerate.ShadowDecoration.Enabled = true;
            this.btnGenerate.Size = new System.Drawing.Size(200, 48);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "🤖 Tạo tự động";
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // progressBar
            // 
            this.progressBar.BorderRadius = 5;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 239);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.progressBar.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.progressBar.Size = new System.Drawing.Size(1069, 12);
            this.progressBar.TabIndex = 3;
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.progressBar.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatus.Location = new System.Drawing.Point(0, 204);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(28, 8, 0, 8);
            this.lblStatus.Size = new System.Drawing.Size(1069, 35);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "📌 Chọn học kỳ và nhấn \'Tạo tự động\' để bắt đầu";
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.Transparent;
            this.panelFilters.Controls.Add(this.swAllowNonPrimary);
            this.panelFilters.Controls.Add(this.lblAllow);
            this.panelFilters.Controls.Add(this.numMaxTiet);
            this.panelFilters.Controls.Add(this.lblMaxTiet);
            this.panelFilters.Controls.Add(this.cbMon);
            this.panelFilters.Controls.Add(this.lblMon);
            this.panelFilters.Controls.Add(this.cbKhoi);
            this.panelFilters.Controls.Add(this.lblKhoi);
            this.panelFilters.Controls.Add(this.cbHocKy);
            this.panelFilters.Controls.Add(this.lblHocKy);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFilters.Location = new System.Drawing.Point(0, 86);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(25, 10, 25, 10);
            this.panelFilters.Size = new System.Drawing.Size(1069, 118);
            this.panelFilters.TabIndex = 1;
            // 
            // swAllowNonPrimary
            // 
            this.swAllowNonPrimary.BackColor = System.Drawing.Color.Transparent;
            this.swAllowNonPrimary.Checked = true;
            this.swAllowNonPrimary.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.swAllowNonPrimary.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.swAllowNonPrimary.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swAllowNonPrimary.CheckedState.InnerColor = System.Drawing.Color.White;
            this.swAllowNonPrimary.Location = new System.Drawing.Point(920, 70);
            this.swAllowNonPrimary.Name = "swAllowNonPrimary";
            this.swAllowNonPrimary.Size = new System.Drawing.Size(55, 28);
            this.swAllowNonPrimary.TabIndex = 9;
            this.swAllowNonPrimary.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.swAllowNonPrimary.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.swAllowNonPrimary.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swAllowNonPrimary.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblAllow
            // 
            this.lblAllow.BackColor = System.Drawing.Color.Transparent;
            this.lblAllow.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAllow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAllow.Location = new System.Drawing.Point(723, 73);
            this.lblAllow.Name = "lblAllow";
            this.lblAllow.Size = new System.Drawing.Size(167, 19);
            this.lblAllow.TabIndex = 8;
            this.lblAllow.Text = "Cho phép trái chuyên môn";
            // 
            // numMaxTiet
            // 
            this.numMaxTiet.BackColor = System.Drawing.Color.Transparent;
            this.numMaxTiet.BorderRadius = 8;
            this.numMaxTiet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numMaxTiet.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.numMaxTiet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numMaxTiet.Location = new System.Drawing.Point(600, 65);
            this.numMaxTiet.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numMaxTiet.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMaxTiet.Name = "numMaxTiet";
            this.numMaxTiet.Size = new System.Drawing.Size(100, 40);
            this.numMaxTiet.TabIndex = 7;
            this.numMaxTiet.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.numMaxTiet.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblMaxTiet
            // 
            this.lblMaxTiet.BackColor = System.Drawing.Color.Transparent;
            this.lblMaxTiet.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMaxTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMaxTiet.Location = new System.Drawing.Point(486, 73);
            this.lblMaxTiet.Name = "lblMaxTiet";
            this.lblMaxTiet.Size = new System.Drawing.Size(78, 19);
            this.lblMaxTiet.TabIndex = 6;
            this.lblMaxTiet.Text = "Max tiết/HK";
            // 
            // cbMon
            // 
            this.cbMon.BackColor = System.Drawing.Color.Transparent;
            this.cbMon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.cbMon.BorderRadius = 8;
            this.cbMon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMon.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbMon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbMon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbMon.ItemHeight = 34;
            this.cbMon.Location = new System.Drawing.Point(270, 65);
            this.cbMon.Name = "cbMon";
            this.cbMon.Size = new System.Drawing.Size(190, 40);
            this.cbMon.TabIndex = 5;
            // 
            // lblMon
            // 
            this.lblMon.BackColor = System.Drawing.Color.Transparent;
            this.lblMon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblMon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMon.Location = new System.Drawing.Point(210, 73);
            this.lblMon.Name = "lblMon";
            this.lblMon.Size = new System.Drawing.Size(31, 19);
            this.lblMon.TabIndex = 4;
            this.lblMon.Text = "Môn";
            // 
            // cbKhoi
            // 
            this.cbKhoi.BackColor = System.Drawing.Color.Transparent;
            this.cbKhoi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.cbKhoi.BorderRadius = 8;
            this.cbKhoi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbKhoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhoi.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbKhoi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbKhoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbKhoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbKhoi.ItemHeight = 34;
            this.cbKhoi.Items.AddRange(new object[] {
            "Tất cả",
            "10",
            "11",
            "12"});
            this.cbKhoi.Location = new System.Drawing.Point(68, 65);
            this.cbKhoi.Name = "cbKhoi";
            this.cbKhoi.Size = new System.Drawing.Size(120, 40);
            this.cbKhoi.StartIndex = 0;
            this.cbKhoi.TabIndex = 3;
            // 
            // lblKhoi
            // 
            this.lblKhoi.BackColor = System.Drawing.Color.Transparent;
            this.lblKhoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKhoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblKhoi.Location = new System.Drawing.Point(30, 73);
            this.lblKhoi.Name = "lblKhoi";
            this.lblKhoi.Size = new System.Drawing.Size(31, 19);
            this.lblKhoi.TabIndex = 2;
            this.lblKhoi.Text = "Khối";
            // 
            // cbHocKy
            // 
            this.cbHocKy.BackColor = System.Drawing.Color.Transparent;
            this.cbHocKy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbHocKy.BorderRadius = 8;
            this.cbHocKy.BorderThickness = 2;
            this.cbHocKy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocKy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbHocKy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.cbHocKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cbHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cbHocKy.ItemHeight = 28;
            this.cbHocKy.Location = new System.Drawing.Point(100, 12);
            this.cbHocKy.Name = "cbHocKy";
            this.cbHocKy.Size = new System.Drawing.Size(280, 34);
            this.cbHocKy.TabIndex = 1;
            this.cbHocKy.SelectedIndexChanged += new System.EventHandler(this.cbHocKy_SelectedIndexChanged);
            // 
            // lblHocKy
            // 
            this.lblHocKy.BackColor = System.Drawing.Color.Transparent;
            this.lblHocKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblHocKy.Location = new System.Drawing.Point(30, 18);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new System.Drawing.Size(60, 17);
            this.lblHocKy.TabIndex = 0;
            this.lblHocKy.Text = "📅 Học kỳ";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Transparent;
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.FillColor = System.Drawing.Color.White;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(28, 20, 28, 8);
            this.panelHeader.Size = new System.Drawing.Size(1069, 86);
            this.panelHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(1013, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Tự động phân công giảng dạy dựa trên chuyên môn và cân bằng tải";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Location = new System.Drawing.Point(28, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1013, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🎯 Phân Công Giảng Dạy Tự Động";
            // 
            // frmAutoPhanCongPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1109, 744);
            this.Controls.Add(this.panelContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmAutoPhanCongPreview";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân Công Giảng Dạy Tự Động";
            this.Load += new System.EventHandler(this.frmAutoPhanCongPreview_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTiet)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna2Panel panelContainer;
        private Guna2DataGridView grid;
        private Guna2Panel panelButtons;
        private Guna2Button btnRollback;
        private Guna2Button btnValidate;
        private Guna2Button btnSaveTemp;
        private Guna2Button btnAccept;
        private Guna2Button btnGenerate;
        private Guna2ProgressBar progressBar;
        private Guna2HtmlLabel lblStatus;
        private Guna2Panel panelFilters;
        private Guna2ToggleSwitch swAllowNonPrimary;
        private Guna2HtmlLabel lblAllow;
        private Guna2NumericUpDown numMaxTiet;
        private Guna2HtmlLabel lblMaxTiet;
        private Guna2ComboBox cbMon;
        private Guna2HtmlLabel lblMon;
        private Guna2ComboBox cbKhoi;
        private Guna2HtmlLabel lblKhoi;
        private Guna2ComboBox cbHocKy;
        private Guna2HtmlLabel lblHocKy;
        private Guna2Panel panelHeader;
        private Guna2HtmlLabel lblSubtitle;
        private Guna2HtmlLabel lblTitle;
    }
}