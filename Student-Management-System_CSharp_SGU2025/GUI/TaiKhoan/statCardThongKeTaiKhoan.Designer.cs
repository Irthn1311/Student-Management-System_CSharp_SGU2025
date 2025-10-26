namespace Student_Management_System_CSharp_SGU2025.GUI.TaiKhoan
{
    partial class statCardThongKeTaiKhoan
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
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.PictureBoxTaiKhoan = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblLietKe = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.panelImage = new Guna.UI2.WinForms.Guna2Panel();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxTaiKhoan)).BeginInit();
            this.panelImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.BorderRadius = 6;
            this.panelMain.Controls.Add(this.panelImage);
            this.panelMain.Controls.Add(this.lblLietKe);
            this.panelMain.Controls.Add(this.lblGhiChu);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.FillColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(262, 165);
            this.panelMain.TabIndex = 1;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // PictureBoxTaiKhoan
            // 
            this.PictureBoxTaiKhoan.BorderRadius = 6;
            this.PictureBoxTaiKhoan.FillColor = System.Drawing.Color.Transparent;
            this.PictureBoxTaiKhoan.ImageRotate = 0F;
            this.PictureBoxTaiKhoan.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxTaiKhoan.Name = "PictureBoxTaiKhoan";
            this.PictureBoxTaiKhoan.Size = new System.Drawing.Size(48, 48);
            this.PictureBoxTaiKhoan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBoxTaiKhoan.TabIndex = 11;
            this.PictureBoxTaiKhoan.TabStop = false;
            this.PictureBoxTaiKhoan.Click += new System.EventHandler(this.PictureBoxTaiKhoan_Click);
            // 
            // lblLietKe
            // 
            this.lblLietKe.AutoSize = true;
            this.lblLietKe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLietKe.ForeColor = System.Drawing.Color.White;
            this.lblLietKe.Location = new System.Drawing.Point(25, 117);
            this.lblLietKe.Name = "lblLietKe";
            this.lblLietKe.Size = new System.Drawing.Size(63, 28);
            this.lblLietKe.TabIndex = 10;
            this.lblLietKe.Text = "Value";
            this.lblLietKe.Click += new System.EventHandler(this.lblLietKe_Click);
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGhiChu.ForeColor = System.Drawing.Color.White;
            this.lblGhiChu.Location = new System.Drawing.Point(27, 94);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(37, 17);
            this.lblGhiChu.TabIndex = 9;
            this.lblGhiChu.Text = "Note";
            this.lblGhiChu.Click += new System.EventHandler(this.lblMon_Click);
            // 
            // panelImage
            // 
            this.panelImage.BorderRadius = 6;
            this.panelImage.Controls.Add(this.PictureBoxTaiKhoan);
            this.panelImage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelImage.Location = new System.Drawing.Point(30, 24);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(48, 48);
            this.panelImage.TabIndex = 12;
            // 
            // statCardThongKeTaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelMain);
            this.Name = "statCardThongKeTaiKhoan";
            this.Size = new System.Drawing.Size(262, 165);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxTaiKhoan)).EndInit();
            this.panelImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        public System.Windows.Forms.Label lblLietKe;
        public System.Windows.Forms.Label lblGhiChu;
        private Guna.UI2.WinForms.Guna2PictureBox PictureBoxTaiKhoan;
        private Guna.UI2.WinForms.Guna2Panel panelImage;
    }
}
