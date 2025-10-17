namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    partial class StatCardKhoi
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
            this.lblSoHocSinh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSoLop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTenKhoi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.BorderColor = System.Drawing.Color.White;
            this.panelMain.BorderRadius = 15;
            this.panelMain.BorderThickness = 1;
            this.panelMain.Controls.Add(this.lblSoHocSinh);
            this.panelMain.Controls.Add(this.lblSoLop);
            this.panelMain.Controls.Add(this.lblTenKhoi);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.FillColor = System.Drawing.Color.White;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(229, 114);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // lblSoHocSinh
            // 
            this.lblSoHocSinh.BackColor = System.Drawing.Color.Transparent;
            this.lblSoHocSinh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoHocSinh.Location = new System.Drawing.Point(19, 80);
            this.lblSoHocSinh.Name = "lblSoHocSinh";
            this.lblSoHocSinh.Size = new System.Drawing.Size(74, 23);
            this.lblSoHocSinh.TabIndex = 5;
            this.lblSoHocSinh.Text = "X học sinh";
            // 
            // lblSoLop
            // 
            this.lblSoLop.BackColor = System.Drawing.Color.Transparent;
            this.lblSoLop.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLop.Location = new System.Drawing.Point(19, 42);
            this.lblSoLop.Name = "lblSoLop";
            this.lblSoLop.Size = new System.Drawing.Size(55, 32);
            this.lblSoLop.TabIndex = 4;
            this.lblSoLop.Text = "X lớp";
            // 
            // lblTenKhoi
            // 
            this.lblTenKhoi.BackColor = System.Drawing.Color.Transparent;
            this.lblTenKhoi.Font = new System.Drawing.Font("Segoe UI", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenKhoi.Location = new System.Drawing.Point(19, 13);
            this.lblTenKhoi.Name = "lblTenKhoi";
            this.lblTenKhoi.Size = new System.Drawing.Size(47, 23);
            this.lblTenKhoi.TabIndex = 3;
            this.lblTenKhoi.Text = "Khối X";
            // 
            // StatCardKhoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelMain);
            this.Name = "StatCardKhoi";
            this.Size = new System.Drawing.Size(229, 114);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public Guna.UI2.WinForms.Guna2Panel panelMain;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblSoHocSinh;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblSoLop;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblTenKhoi;
    }
}
