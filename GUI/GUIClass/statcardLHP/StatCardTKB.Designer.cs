namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class StatCardTKB
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
            this.panelStatTKB = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblGiaoVien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMonHoc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.panelStatTKB.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStatTKB
            // 
            this.panelStatTKB.BorderRadius = 6;
            this.panelStatTKB.Controls.Add(this.lblLop);
            this.panelStatTKB.Controls.Add(this.lblGiaoVien);
            this.panelStatTKB.Controls.Add(this.lblMonHoc);
            this.panelStatTKB.Controls.Add(this.progressBar);
            this.panelStatTKB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatTKB.FillColor = System.Drawing.Color.White;
            this.panelStatTKB.Location = new System.Drawing.Point(0, 0);
            this.panelStatTKB.Name = "panelStatTKB";
            this.panelStatTKB.Size = new System.Drawing.Size(149, 84);
            this.panelStatTKB.TabIndex = 0;
            // 
            // lblLop
            // 
            this.lblLop.BackColor = System.Drawing.Color.Transparent;
            this.lblLop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLop.Location = new System.Drawing.Point(38, 60);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(23, 17);
            this.lblLop.TabIndex = 3;
            this.lblLop.Text = "Lớp";
            // 
            // lblGiaoVien
            // 
            this.lblGiaoVien.BackColor = System.Drawing.Color.Transparent;
            this.lblGiaoVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaoVien.Location = new System.Drawing.Point(38, 37);
            this.lblGiaoVien.Name = "lblGiaoVien";
            this.lblGiaoVien.Size = new System.Drawing.Size(53, 17);
            this.lblGiaoVien.TabIndex = 4;
            this.lblGiaoVien.Text = "Giáo Viên";
            // 
            // lblMonHoc
            // 
            this.lblMonHoc.BackColor = System.Drawing.Color.Transparent;
            this.lblMonHoc.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonHoc.Location = new System.Drawing.Point(38, 9);
            this.lblMonHoc.Name = "lblMonHoc";
            this.lblMonHoc.Size = new System.Drawing.Size(66, 22);
            this.lblMonHoc.TabIndex = 5;
            this.lblMonHoc.Text = "Môn Học";
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.Transparent;
            this.progressBar.BorderRadius = 6;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.FillColor = System.Drawing.Color.Transparent;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBar.ProgressColor2 = System.Drawing.Color.Transparent;
            this.progressBar.Size = new System.Drawing.Size(149, 84);
            this.progressBar.TabIndex = 2;
            this.progressBar.Text = "guna2ProgressBar1";
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.progressBar.Value = 7;
            // 
            // StatCardTKB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.panelStatTKB);
            this.Name = "StatCardTKB";
            this.Size = new System.Drawing.Size(149, 84);
            this.panelStatTKB.ResumeLayout(false);
            this.panelStatTKB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelStatTKB;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGiaoVien;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMonHoc;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
    }
}
