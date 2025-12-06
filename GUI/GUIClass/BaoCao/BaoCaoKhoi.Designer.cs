namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class BaoCaoKhoi
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
            this.pnlKhoiStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlGioi = new Guna.UI2.WinForms.Guna2Panel();
            this.lblGioi = new System.Windows.Forms.Label();
            this.lblGoi10Label = new System.Windows.Forms.Label();
            this.pnlKha = new Guna.UI2.WinForms.Guna2Panel();
            this.lblKha = new System.Windows.Forms.Label();
            this.lblKha10Label = new System.Windows.Forms.Label();
            this.pnlTrungBinh = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTrungBinh = new System.Windows.Forms.Label();
            this.lblTrungBinh10Label = new System.Windows.Forms.Label();
            this.pnlYeu = new Guna.UI2.WinForms.Guna2Panel();
            this.lblYeu = new System.Windows.Forms.Label();
            this.lblYeu10Label = new System.Windows.Forms.Label();
            this.pnlKhoiStats.SuspendLayout();
            this.pnlGioi.SuspendLayout();
            this.pnlKha.SuspendLayout();
            this.pnlTrungBinh.SuspendLayout();
            this.pnlYeu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlKhoiStats
            // 
            this.pnlKhoiStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlKhoiStats.Controls.Add(this.pnlGioi);
            this.pnlKhoiStats.Controls.Add(this.pnlKha);
            this.pnlKhoiStats.Controls.Add(this.pnlTrungBinh);
            this.pnlKhoiStats.Controls.Add(this.pnlYeu);
            this.pnlKhoiStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKhoiStats.Location = new System.Drawing.Point(0, 0);
            this.pnlKhoiStats.Name = "pnlKhoiStats";
            this.pnlKhoiStats.Size = new System.Drawing.Size(1096, 100);
            this.pnlKhoiStats.TabIndex = 2;
            this.pnlKhoiStats.WrapContents = false;
            this.pnlKhoiStats.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlKhoiStats_Paint);
            // 
            // pnlGioi
            // 
            this.pnlGioi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlGioi.BorderRadius = 4;
            this.pnlGioi.Controls.Add(this.lblGioi);
            this.pnlGioi.Controls.Add(this.lblGoi10Label);
            this.pnlGioi.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.pnlGioi.CustomBorderThickness = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnlGioi.Location = new System.Drawing.Point(3, 3);
            this.pnlGioi.Margin = new System.Windows.Forms.Padding(3, 3, 16, 3);
            this.pnlGioi.Name = "pnlGioi";
            this.pnlGioi.Padding = new System.Windows.Forms.Padding(16, 16, 20, 16);
            this.pnlGioi.Size = new System.Drawing.Size(255, 84);
            this.pnlGioi.TabIndex = 0;
            // 
            // lblGioi
            // 
            this.lblGioi.AutoSize = true;
            this.lblGioi.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold);
            this.lblGioi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblGioi.Location = new System.Drawing.Point(16, 16);
            this.lblGioi.Name = "lblGioi";
            this.lblGioi.Size = new System.Drawing.Size(48, 32);
            this.lblGioi.TabIndex = 0;
            this.lblGioi.Text = "98";
            this.lblGioi.Click += new System.EventHandler(this.lblGioi_Click);
            // 
            // lblGoi10Label
            // 
            this.lblGoi10Label.AutoSize = true;
            this.lblGoi10Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblGoi10Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblGoi10Label.Location = new System.Drawing.Point(16, 48);
            this.lblGoi10Label.Name = "lblGoi10Label";
            this.lblGoi10Label.Size = new System.Drawing.Size(29, 15);
            this.lblGoi10Label.TabIndex = 1;
            this.lblGoi10Label.Text = "Giỏi";
            // 
            // pnlKha
            // 
            this.pnlKha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlKha.BorderRadius = 4;
            this.pnlKha.Controls.Add(this.lblKha);
            this.pnlKha.Controls.Add(this.lblKha10Label);
            this.pnlKha.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlKha.CustomBorderThickness = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnlKha.Location = new System.Drawing.Point(277, 3);
            this.pnlKha.Margin = new System.Windows.Forms.Padding(3, 3, 16, 3);
            this.pnlKha.Name = "pnlKha";
            this.pnlKha.Padding = new System.Windows.Forms.Padding(16, 16, 20, 16);
            this.pnlKha.Size = new System.Drawing.Size(255, 84);
            this.pnlKha.TabIndex = 1;
            // 
            // lblKha
            // 
            this.lblKha.AutoSize = true;
            this.lblKha.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold);
            this.lblKha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblKha.Location = new System.Drawing.Point(16, 16);
            this.lblKha.Name = "lblKha";
            this.lblKha.Size = new System.Drawing.Size(65, 32);
            this.lblKha.TabIndex = 0;
            this.lblKha.Text = "192";
            // 
            // lblKha10Label
            // 
            this.lblKha10Label.AutoSize = true;
            this.lblKha10Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblKha10Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblKha10Label.Location = new System.Drawing.Point(16, 48);
            this.lblKha10Label.Name = "lblKha10Label";
            this.lblKha10Label.Size = new System.Drawing.Size(29, 15);
            this.lblKha10Label.TabIndex = 1;
            this.lblKha10Label.Text = "Khá";
            // 
            // pnlTrungBinh
            // 
            this.pnlTrungBinh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlTrungBinh.BorderRadius = 4;
            this.pnlTrungBinh.Controls.Add(this.lblTrungBinh);
            this.pnlTrungBinh.Controls.Add(this.lblTrungBinh10Label);
            this.pnlTrungBinh.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.pnlTrungBinh.CustomBorderThickness = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnlTrungBinh.Location = new System.Drawing.Point(551, 3);
            this.pnlTrungBinh.Margin = new System.Windows.Forms.Padding(3, 3, 16, 3);
            this.pnlTrungBinh.Name = "pnlTrungBinh";
            this.pnlTrungBinh.Padding = new System.Windows.Forms.Padding(16, 16, 20, 16);
            this.pnlTrungBinh.Size = new System.Drawing.Size(255, 84);
            this.pnlTrungBinh.TabIndex = 2;
            // 
            // lblTrungBinh
            // 
            this.lblTrungBinh.AutoSize = true;
            this.lblTrungBinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold);
            this.lblTrungBinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblTrungBinh.Location = new System.Drawing.Point(16, 16);
            this.lblTrungBinh.Name = "lblTrungBinh";
            this.lblTrungBinh.Size = new System.Drawing.Size(65, 32);
            this.lblTrungBinh.TabIndex = 0;
            this.lblTrungBinh.Text = "154";
            // 
            // lblTrungBinh10Label
            // 
            this.lblTrungBinh10Label.AutoSize = true;
            this.lblTrungBinh10Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTrungBinh10Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblTrungBinh10Label.Location = new System.Drawing.Point(16, 48);
            this.lblTrungBinh10Label.Name = "lblTrungBinh10Label";
            this.lblTrungBinh10Label.Size = new System.Drawing.Size(66, 15);
            this.lblTrungBinh10Label.TabIndex = 1;
            this.lblTrungBinh10Label.Text = "Trung bình";
            // 
            // pnlYeu
            // 
            this.pnlYeu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlYeu.BorderRadius = 4;
            this.pnlYeu.Controls.Add(this.lblYeu);
            this.pnlYeu.Controls.Add(this.lblYeu10Label);
            this.pnlYeu.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.pnlYeu.CustomBorderThickness = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnlYeu.Location = new System.Drawing.Point(825, 3);
            this.pnlYeu.Name = "pnlYeu";
            this.pnlYeu.Padding = new System.Windows.Forms.Padding(16, 16, 20, 16);
            this.pnlYeu.Size = new System.Drawing.Size(255, 84);
            this.pnlYeu.TabIndex = 3;
            // 
            // lblYeu
            // 
            this.lblYeu.AutoSize = true;
            this.lblYeu.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F, System.Drawing.FontStyle.Bold);
            this.lblYeu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblYeu.Location = new System.Drawing.Point(16, 16);
            this.lblYeu.Name = "lblYeu";
            this.lblYeu.Size = new System.Drawing.Size(48, 32);
            this.lblYeu.TabIndex = 0;
            this.lblYeu.Text = "36";
            // 
            // lblYeu10Label
            // 
            this.lblYeu10Label.AutoSize = true;
            this.lblYeu10Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblYeu10Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblYeu10Label.Location = new System.Drawing.Point(16, 48);
            this.lblYeu10Label.Name = "lblYeu10Label";
            this.lblYeu10Label.Size = new System.Drawing.Size(28, 15);
            this.lblYeu10Label.TabIndex = 1;
            this.lblYeu10Label.Text = "Yếu";
            // 
            // BaoCaoKhoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlKhoiStats);
            this.Name = "BaoCaoKhoi";
            this.Size = new System.Drawing.Size(1096, 100);
            this.pnlKhoiStats.ResumeLayout(false);
            this.pnlGioi.ResumeLayout(false);
            this.pnlGioi.PerformLayout();
            this.pnlKha.ResumeLayout(false);
            this.pnlKha.PerformLayout();
            this.pnlTrungBinh.ResumeLayout(false);
            this.pnlTrungBinh.PerformLayout();
            this.pnlYeu.ResumeLayout(false);
            this.pnlYeu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlKhoiStats;
        private Guna.UI2.WinForms.Guna2Panel pnlGioi;
        private System.Windows.Forms.Label lblGioi;
        private System.Windows.Forms.Label lblGoi10Label;
        private Guna.UI2.WinForms.Guna2Panel pnlKha;
        private System.Windows.Forms.Label lblKha;
        private System.Windows.Forms.Label lblKha10Label;
        private Guna.UI2.WinForms.Guna2Panel pnlTrungBinh;
        private System.Windows.Forms.Label lblTrungBinh;
        private System.Windows.Forms.Label lblTrungBinh10Label;
        private Guna.UI2.WinForms.Guna2Panel pnlYeu;
        private System.Windows.Forms.Label lblYeu;
        private System.Windows.Forms.Label lblYeu10Label;
    }
}
