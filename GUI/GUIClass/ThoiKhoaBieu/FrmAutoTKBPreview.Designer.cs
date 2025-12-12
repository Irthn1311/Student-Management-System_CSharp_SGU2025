namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FrmAutoTKBPreview
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
            this.panelConfig = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTabuTenure = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTimeBudget = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblIterations = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.numTabuTenure = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.numTimeBudget = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.numIterations = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblInfoIter = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblInfoTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblInfoTabu = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelPreview = new Guna.UI2.WinForms.Guna2Panel();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnValidate = new Guna.UI2.WinForms.Guna2Button();
            this.btnRegenerate = new Guna.UI2.WinForms.Guna2Button();
            this.btnGenerate = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.txtLog = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTabuTenure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeBudget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelConfig
            // 
            this.panelConfig.BackColor = System.Drawing.Color.Transparent;
            this.panelConfig.BorderRadius = 10;
            this.panelConfig.Controls.Add(this.lblTabuTenure);
            this.panelConfig.Controls.Add(this.lblTimeBudget);
            this.panelConfig.Controls.Add(this.lblIterations);
            this.panelConfig.Controls.Add(this.numTabuTenure);
            this.panelConfig.Controls.Add(this.numTimeBudget);
            this.panelConfig.Controls.Add(this.numIterations);
            this.panelConfig.Controls.Add(this.lblInfoIter);
            this.panelConfig.Controls.Add(this.lblInfoTime);
            this.panelConfig.Controls.Add(this.lblInfoTabu);
            this.panelConfig.FillColor = System.Drawing.Color.White;
            this.panelConfig.Location = new System.Drawing.Point(30, 60);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.ShadowDecoration.Enabled = true;
            this.panelConfig.Size = new System.Drawing.Size(920, 140);
            this.panelConfig.TabIndex = 1;
            // 
            // lblTabuTenure
            // 
            this.lblTabuTenure.BackColor = System.Drawing.Color.Transparent;
            this.lblTabuTenure.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTabuTenure.Location = new System.Drawing.Point(20, 100);
            this.lblTabuTenure.Name = "lblTabuTenure";
            this.lblTabuTenure.Size = new System.Drawing.Size(101, 19);
            this.lblTabuTenure.TabIndex = 4;
            this.lblTabuTenure.Text = "Độ dài Tabu List:";
            // 
            // lblTimeBudget
            // 
            this.lblTimeBudget.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeBudget.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTimeBudget.Location = new System.Drawing.Point(20, 60);
            this.lblTimeBudget.Name = "lblTimeBudget";
            this.lblTimeBudget.Size = new System.Drawing.Size(134, 19);
            this.lblTimeBudget.TabIndex = 2;
            this.lblTimeBudget.Text = "Thời gian tối đa (giây):";
            // 
            // lblIterations
            // 
            this.lblIterations.BackColor = System.Drawing.Color.Transparent;
            this.lblIterations.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIterations.Location = new System.Drawing.Point(20, 20);
            this.lblIterations.Name = "lblIterations";
            this.lblIterations.Size = new System.Drawing.Size(142, 19);
            this.lblIterations.TabIndex = 0;
            this.lblIterations.Text = "Số vòng lặp (Iterations):";
            // 
            // numTabuTenure
            // 
            this.numTabuTenure.BackColor = System.Drawing.Color.Transparent;
            this.numTabuTenure.BorderRadius = 6;
            this.numTabuTenure.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numTabuTenure.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numTabuTenure.Location = new System.Drawing.Point(220, 95);
            this.numTabuTenure.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numTabuTenure.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTabuTenure.Name = "numTabuTenure";
            this.numTabuTenure.Size = new System.Drawing.Size(150, 30);
            this.numTabuTenure.TabIndex = 5;
            this.numTabuTenure.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // numTimeBudget
            // 
            this.numTimeBudget.BackColor = System.Drawing.Color.Transparent;
            this.numTimeBudget.BorderRadius = 6;
            this.numTimeBudget.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numTimeBudget.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numTimeBudget.Location = new System.Drawing.Point(220, 55);
            this.numTimeBudget.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numTimeBudget.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTimeBudget.Name = "numTimeBudget";
            this.numTimeBudget.Size = new System.Drawing.Size(150, 30);
            this.numTimeBudget.TabIndex = 3;
            this.numTimeBudget.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // numIterations
            // 
            this.numIterations.BackColor = System.Drawing.Color.Transparent;
            this.numIterations.BorderRadius = 6;
            this.numIterations.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numIterations.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numIterations.Location = new System.Drawing.Point(220, 15);
            this.numIterations.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numIterations.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numIterations.Name = "numIterations";
            this.numIterations.Size = new System.Drawing.Size(150, 30);
            this.numIterations.TabIndex = 1;
            this.numIterations.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // lblInfoIter
            // 
            this.lblInfoIter.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoIter.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblInfoIter.ForeColor = System.Drawing.Color.Gray;
            this.lblInfoIter.Location = new System.Drawing.Point(400, 20);
            this.lblInfoIter.Name = "lblInfoIter";
            this.lblInfoIter.Size = new System.Drawing.Size(184, 15);
            this.lblInfoIter.TabIndex = 6;
            this.lblInfoIter.Text = " Càng cao càng tốt (nhưng lâu hơn)";
            // 
            // lblInfoTime
            // 
            this.lblInfoTime.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoTime.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblInfoTime.ForeColor = System.Drawing.Color.Gray;
            this.lblInfoTime.Location = new System.Drawing.Point(400, 60);
            this.lblInfoTime.Name = "lblInfoTime";
            this.lblInfoTime.Size = new System.Drawing.Size(159, 15);
            this.lblInfoTime.TabIndex = 7;
            this.lblInfoTime.Text = "Timeout để tránh chạy quá lâu";
            // 
            // lblInfoTabu
            // 
            this.lblInfoTabu.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoTabu.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblInfoTabu.ForeColor = System.Drawing.Color.Gray;
            this.lblInfoTabu.Location = new System.Drawing.Point(400, 100);
            this.lblInfoTabu.Name = "lblInfoTabu";
            this.lblInfoTabu.Size = new System.Drawing.Size(96, 15);
            this.lblInfoTabu.TabIndex = 8;
            this.lblInfoTabu.Text = "Khuyến nghị: 7-12";
            // 
            // panelPreview
            // 
            this.panelPreview.Location = new System.Drawing.Point(0, 0);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(0, 0);
            this.panelPreview.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.BorderRadius = 8;
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnValidate);
            this.panelButtons.Controls.Add(this.btnRegenerate);
            this.panelButtons.Controls.Add(this.btnGenerate);
            this.panelButtons.FillColor = System.Drawing.Color.White;
            this.panelButtons.Location = new System.Drawing.Point(30, 580);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.ShadowDecoration.Enabled = true;
            this.panelButtons.Size = new System.Drawing.Size(920, 60);
            this.panelButtons.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 6;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(600, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "✗ Hủy";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 6;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Enabled = false;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(450, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 36);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "💾 Lưu & Đóng";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.BorderRadius = 6;
            this.btnValidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidate.Enabled = false;
            this.btnValidate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnValidate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnValidate.ForeColor = System.Drawing.Color.White;
            this.btnValidate.Location = new System.Drawing.Point(320, 12);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(120, 36);
            this.btnValidate.TabIndex = 2;
            this.btnValidate.Text = "✓ Kiểm tra";
            this.btnValidate.Click += new System.EventHandler(this.BtnValidate_Click);
            // 
            // btnRegenerate
            // 
            this.btnRegenerate.BorderRadius = 6;
            this.btnRegenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegenerate.Enabled = false;
            this.btnRegenerate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnRegenerate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRegenerate.ForeColor = System.Drawing.Color.White;
            this.btnRegenerate.Location = new System.Drawing.Point(170, 12);
            this.btnRegenerate.Name = "btnRegenerate";
            this.btnRegenerate.Size = new System.Drawing.Size(140, 36);
            this.btnRegenerate.TabIndex = 1;
            this.btnRegenerate.Text = "🔄 Regenerate";
            this.btnRegenerate.Click += new System.EventHandler(this.BtnRegenerate_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.BorderRadius = 6;
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(20, 12);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(140, 36);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "🚀 Generate";
            this.btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(642, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "<b>Auto Tạo Thời khóa biểu - Config-driven (Greedy + Tabu Search)</b>";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatus.Location = new System.Drawing.Point(30, 220);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(900, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Sẵn sàng tạo TKB. Nhấn \'Generate\' để bắt đầu.";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(30, 250);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.progressBar.ProgressColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.progressBar.Size = new System.Drawing.Size(920, 15);
            this.progressBar.TabIndex = 3;
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.progressBar.Visible = false;
            // 
            // txtLog
            // 
            this.txtLog.BorderRadius = 8;
            this.txtLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLog.DefaultText = "";
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.Location = new System.Drawing.Point(30, 280);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.PlaceholderText = "";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.SelectedText = "";
            this.txtLog.Size = new System.Drawing.Size(920, 280);
            this.txtLog.TabIndex = 4;
            // 
            // FrmAutoTKBPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panelConfig);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAutoTKBPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tự Động Tạo Thời khóa biểu";
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTabuTenure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeBudget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelConfig;
        private Guna.UI2.WinForms.Guna2Panel panelPreview;
        private Guna.UI2.WinForms.Guna2Panel panelButtons;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStatus;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIterations;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTimeBudget;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTabuTenure;
        private Guna.UI2.WinForms.Guna2NumericUpDown numIterations;
        private Guna.UI2.WinForms.Guna2NumericUpDown numTimeBudget;
        private Guna.UI2.WinForms.Guna2NumericUpDown numTabuTenure;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Guna.UI2.WinForms.Guna2TextBox txtLog;
        private Guna.UI2.WinForms.Guna2Button btnGenerate;
        private Guna.UI2.WinForms.Guna2Button btnRegenerate;
        private Guna.UI2.WinForms.Guna2Button btnValidate;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInfoIter;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInfoTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblInfoTabu;
    }
}
