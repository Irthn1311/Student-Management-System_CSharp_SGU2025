namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    partial class statcardMonHoc
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSoLuong = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMon = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblLietKe = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.lblLietKe);
            this.guna2Panel1.Controls.Add(this.lblMon);
            this.guna2Panel1.Controls.Add(this.lblSoLuong);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(262, 136);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = false;
            this.lblSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.Location = new System.Drawing.Point(71, 16);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(133, 36);
            this.lblSoLuong.TabIndex = 0;
            this.lblSoLuong.Text = "SoLuong";
            this.lblSoLuong.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSoLuong.Click += new System.EventHandler(this.lblSoLuong_Click);
            // 
            // lblMon
            // 
            this.lblMon.AutoSize = false;
            this.lblMon.BackColor = System.Drawing.Color.Transparent;
            this.lblMon.Location = new System.Drawing.Point(37, 58);
            this.lblMon.Name = "lblMon";
            this.lblMon.Size = new System.Drawing.Size(194, 26);
            this.lblMon.TabIndex = 1;
            this.lblMon.Text = "MON";
            this.lblMon.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLietKe
            // 
            this.lblLietKe.AutoSize = false;
            this.lblLietKe.BackColor = System.Drawing.Color.Transparent;
            this.lblLietKe.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLietKe.Location = new System.Drawing.Point(37, 92);
            this.lblLietKe.Name = "lblLietKe";
            this.lblLietKe.Size = new System.Drawing.Size(194, 26);
            this.lblLietKe.TabIndex = 2;
            this.lblLietKe.Text = "MON";
            this.lblLietKe.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statcardMonHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Panel1);
            this.Name = "statcardMonHoc";
            this.Size = new System.Drawing.Size(262, 136);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLietKe;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMon;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoLuong;
    }
}
