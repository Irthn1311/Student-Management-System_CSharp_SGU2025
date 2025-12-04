namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ucBaoCaoThongKeHocLuc
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
            this.pnlStatistics = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlStatisticsFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.btnExportStatistics = new Guna.UI2.WinForms.Guna2Button();
            this.pnlStatisticsContent = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlKhoi10 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblKhoi10Title = new System.Windows.Forms.Label();
            this.pnlKhoi11 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblKhoi11Title = new System.Windows.Forms.Label();
            this.pnlKhoi12 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblKhoi12Title = new System.Windows.Forms.Label();
            this.pnlStatisticsHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblStatisticsTitle = new System.Windows.Forms.Label();
            this.baoCaoKhoi1 = new Student_Management_System_CSharp_SGU2025.GUI.BaoCao.BaoCaoKhoi();
            this.baoCaoKhoi2 = new Student_Management_System_CSharp_SGU2025.GUI.BaoCao.BaoCaoKhoi();
            this.baoCaoKhoi3 = new Student_Management_System_CSharp_SGU2025.GUI.BaoCao.BaoCaoKhoi();
            this.pnlStatistics.SuspendLayout();
            this.pnlStatisticsFooter.SuspendLayout();
            this.pnlStatisticsContent.SuspendLayout();
            this.pnlKhoi10.SuspendLayout();
            this.pnlKhoi11.SuspendLayout();
            this.pnlKhoi12.SuspendLayout();
            this.pnlStatisticsHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlStatistics
            // 
            this.pnlStatistics.BackColor = System.Drawing.Color.Transparent;
            this.pnlStatistics.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlStatistics.BorderRadius = 12;
            this.pnlStatistics.BorderThickness = 1;
            this.pnlStatistics.Controls.Add(this.pnlStatisticsFooter);
            this.pnlStatistics.Controls.Add(this.pnlStatisticsContent);
            this.pnlStatistics.Controls.Add(this.pnlStatisticsHeader);
            this.pnlStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatistics.Location = new System.Drawing.Point(0, 0);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.ShadowDecoration.BorderRadius = 12;
            this.pnlStatistics.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(13)))));
            this.pnlStatistics.ShadowDecoration.Depth = 5;
            this.pnlStatistics.ShadowDecoration.Enabled = true;
            this.pnlStatistics.Size = new System.Drawing.Size(1120, 657);
            this.pnlStatistics.TabIndex = 0;
            // 
            // pnlStatisticsFooter
            // 
            this.pnlStatisticsFooter.BackColor = System.Drawing.SystemColors.Control;
            this.pnlStatisticsFooter.Controls.Add(this.btnExportStatistics);
            this.pnlStatisticsFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatisticsFooter.Location = new System.Drawing.Point(0, 582);
            this.pnlStatisticsFooter.Name = "pnlStatisticsFooter";
            this.pnlStatisticsFooter.Size = new System.Drawing.Size(1120, 75);
            this.pnlStatisticsFooter.TabIndex = 2;
            this.pnlStatisticsFooter.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatisticsFooter_Paint);
            // 
            // btnExportStatistics
            // 
            this.btnExportStatistics.BorderRadius = 8;
            this.btnExportStatistics.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExportStatistics.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExportStatistics.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExportStatistics.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExportStatistics.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnExportStatistics.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExportStatistics.ForeColor = System.Drawing.Color.White;
            this.btnExportStatistics.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnExportStatistics.Location = new System.Drawing.Point(861, 15);
            this.btnExportStatistics.Name = "btnExportStatistics";
            this.btnExportStatistics.Size = new System.Drawing.Size(238, 48);
            this.btnExportStatistics.TabIndex = 0;
            this.btnExportStatistics.Text = "📊 Xuất báo cáo tổng hợp";
            this.btnExportStatistics.Click += new System.EventHandler(this.BtnExportStatistics_Click);
            // 
            // pnlStatisticsContent
            // 
            this.pnlStatisticsContent.BackColor = System.Drawing.Color.White;
            this.pnlStatisticsContent.Controls.Add(this.pnlKhoi10);
            this.pnlStatisticsContent.Controls.Add(this.pnlKhoi11);
            this.pnlStatisticsContent.Controls.Add(this.pnlKhoi12);
            this.pnlStatisticsContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStatisticsContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlStatisticsContent.Location = new System.Drawing.Point(0, 69);
            this.pnlStatisticsContent.Name = "pnlStatisticsContent";
            this.pnlStatisticsContent.Padding = new System.Windows.Forms.Padding(0, 0, 0, 60);
            this.pnlStatisticsContent.Size = new System.Drawing.Size(1120, 588);
            this.pnlStatisticsContent.TabIndex = 1;
            this.pnlStatisticsContent.WrapContents = false;
            // 
            // pnlKhoi10
            // 
            this.pnlKhoi10.BackColor = System.Drawing.Color.Transparent;
            this.pnlKhoi10.Controls.Add(this.baoCaoKhoi1);
            this.pnlKhoi10.Controls.Add(this.lblKhoi10Title);
            this.pnlKhoi10.Location = new System.Drawing.Point(3, 3);
            this.pnlKhoi10.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.pnlKhoi10.Name = "pnlKhoi10";
            this.pnlKhoi10.Size = new System.Drawing.Size(1096, 143);
            this.pnlKhoi10.TabIndex = 0;
            // 
            // lblKhoi10Title
            // 
            this.lblKhoi10Title.AutoSize = true;
            this.lblKhoi10Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblKhoi10Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblKhoi10Title.Location = new System.Drawing.Point(0, 0);
            this.lblKhoi10Title.Name = "lblKhoi10Title";
            this.lblKhoi10Title.Size = new System.Drawing.Size(69, 20);
            this.lblKhoi10Title.TabIndex = 0;
            this.lblKhoi10Title.Text = "Khối 10";
            // 
            // pnlKhoi11
            // 
            this.pnlKhoi11.BackColor = System.Drawing.Color.Transparent;
            this.pnlKhoi11.Controls.Add(this.baoCaoKhoi2);
            this.pnlKhoi11.Controls.Add(this.lblKhoi11Title);
            this.pnlKhoi11.Location = new System.Drawing.Point(3, 169);
            this.pnlKhoi11.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.pnlKhoi11.Name = "pnlKhoi11";
            this.pnlKhoi11.Size = new System.Drawing.Size(1096, 140);
            this.pnlKhoi11.TabIndex = 1;
            // 
            // lblKhoi11Title
            // 
            this.lblKhoi11Title.AutoSize = true;
            this.lblKhoi11Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblKhoi11Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblKhoi11Title.Location = new System.Drawing.Point(0, 0);
            this.lblKhoi11Title.Name = "lblKhoi11Title";
            this.lblKhoi11Title.Size = new System.Drawing.Size(69, 20);
            this.lblKhoi11Title.TabIndex = 0;
            this.lblKhoi11Title.Text = "Khối 11";
            // 
            // pnlKhoi12
            // 
            this.pnlKhoi12.BackColor = System.Drawing.Color.Transparent;
            this.pnlKhoi12.Controls.Add(this.baoCaoKhoi3);
            this.pnlKhoi12.Controls.Add(this.lblKhoi12Title);
            this.pnlKhoi12.Location = new System.Drawing.Point(3, 332);
            this.pnlKhoi12.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.pnlKhoi12.Name = "pnlKhoi12";
            this.pnlKhoi12.Size = new System.Drawing.Size(1096, 140);
            this.pnlKhoi12.TabIndex = 2;
            // 
            // lblKhoi12Title
            // 
            this.lblKhoi12Title.AutoSize = true;
            this.lblKhoi12Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblKhoi12Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblKhoi12Title.Location = new System.Drawing.Point(0, 0);
            this.lblKhoi12Title.Name = "lblKhoi12Title";
            this.lblKhoi12Title.Size = new System.Drawing.Size(69, 20);
            this.lblKhoi12Title.TabIndex = 0;
            this.lblKhoi12Title.Text = "Khối 12";
            // 
            // pnlStatisticsHeader
            // 
            this.pnlStatisticsHeader.BackColor = System.Drawing.Color.White;
            this.pnlStatisticsHeader.Controls.Add(this.lblStatisticsTitle);
            this.pnlStatisticsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatisticsHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlStatisticsHeader.Name = "pnlStatisticsHeader";
            this.pnlStatisticsHeader.Size = new System.Drawing.Size(1120, 69);
            this.pnlStatisticsHeader.TabIndex = 0;
            this.pnlStatisticsHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatisticsHeader_Paint);
            // 
            // lblStatisticsTitle
            // 
            this.lblStatisticsTitle.AutoSize = true;
            this.lblStatisticsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatisticsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblStatisticsTitle.Location = new System.Drawing.Point(24, 25);
            this.lblStatisticsTitle.Name = "lblStatisticsTitle";
            this.lblStatisticsTitle.Size = new System.Drawing.Size(218, 21);
            this.lblStatisticsTitle.TabIndex = 0;
            this.lblStatisticsTitle.Text = "Thống kê học lực theo khối";
            // 
            // baoCaoKhoi1
            // 
            this.baoCaoKhoi1.Location = new System.Drawing.Point(0, 40);
            this.baoCaoKhoi1.Name = "baoCaoKhoi1";
            this.baoCaoKhoi1.Size = new System.Drawing.Size(1096, 100);
            this.baoCaoKhoi1.SoGioi = "98";
            this.baoCaoKhoi1.SoKha = "192";
            this.baoCaoKhoi1.SoTrungBinh = "154";
            this.baoCaoKhoi1.SoYeu = "36";
            this.baoCaoKhoi1.TabIndex = 1;
            this.baoCaoKhoi1.Load += new System.EventHandler(this.baoCaoKhoi1_Load);
            // 
            // baoCaoKhoi2
            // 
            this.baoCaoKhoi2.Location = new System.Drawing.Point(0, 37);
            this.baoCaoKhoi2.Name = "baoCaoKhoi2";
            this.baoCaoKhoi2.Size = new System.Drawing.Size(1096, 100);
            this.baoCaoKhoi2.SoGioi = "98";
            this.baoCaoKhoi2.SoKha = "192";
            this.baoCaoKhoi2.SoTrungBinh = "154";
            this.baoCaoKhoi2.SoYeu = "36";
            this.baoCaoKhoi2.TabIndex = 2;
            // 
            // baoCaoKhoi3
            // 
            this.baoCaoKhoi3.Location = new System.Drawing.Point(0, 37);
            this.baoCaoKhoi3.Name = "baoCaoKhoi3";
            this.baoCaoKhoi3.Size = new System.Drawing.Size(1096, 100);
            this.baoCaoKhoi3.SoGioi = "98";
            this.baoCaoKhoi3.SoKha = "192";
            this.baoCaoKhoi3.SoTrungBinh = "154";
            this.baoCaoKhoi3.SoYeu = "36";
            this.baoCaoKhoi3.TabIndex = 2;
            // 
            // ucBaoCaoThongKeHocLuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlStatistics);
            this.Name = "ucBaoCaoThongKeHocLuc";
            this.Size = new System.Drawing.Size(1120, 657);
            this.pnlStatistics.ResumeLayout(false);
            this.pnlStatisticsFooter.ResumeLayout(false);
            this.pnlStatisticsContent.ResumeLayout(false);
            this.pnlKhoi10.ResumeLayout(false);
            this.pnlKhoi10.PerformLayout();
            this.pnlKhoi11.ResumeLayout(false);
            this.pnlKhoi11.PerformLayout();
            this.pnlKhoi12.ResumeLayout(false);
            this.pnlKhoi12.PerformLayout();
            this.pnlStatisticsHeader.ResumeLayout(false);
            this.pnlStatisticsHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlStatistics;
        private Guna.UI2.WinForms.Guna2Panel pnlStatisticsFooter;
        private Guna.UI2.WinForms.Guna2Button btnExportStatistics;
        private System.Windows.Forms.FlowLayoutPanel pnlStatisticsContent;
        private Guna.UI2.WinForms.Guna2Panel pnlKhoi10;
        private System.Windows.Forms.Label lblKhoi10Title;
        private Guna.UI2.WinForms.Guna2Panel pnlKhoi11;
        private System.Windows.Forms.Label lblKhoi11Title;
        private Guna.UI2.WinForms.Guna2Panel pnlKhoi12;
        private System.Windows.Forms.Label lblKhoi12Title;
        private Guna.UI2.WinForms.Guna2Panel pnlStatisticsHeader;
        private System.Windows.Forms.Label lblStatisticsTitle;
        private BaoCao.BaoCaoKhoi baoCaoKhoi1;
        private BaoCao.BaoCaoKhoi baoCaoKhoi2;
        private BaoCao.BaoCaoKhoi baoCaoKhoi3;
    }
}
