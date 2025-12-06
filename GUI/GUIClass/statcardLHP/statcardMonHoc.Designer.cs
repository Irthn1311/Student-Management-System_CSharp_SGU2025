namespace Student_Management_System_CSharp_SGU2025.GUI
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
            this.lblMon = new System.Windows.Forms.Label();
            this.lblLietKe = new System.Windows.Forms.Label();
            this.lblSoLuong = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelMauNen = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1.SuspendLayout();
            this.panelMauNen.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderRadius = 6;
            this.guna2Panel1.Controls.Add(this.panelMauNen);
            this.guna2Panel1.Controls.Add(this.lblLietKe);
            this.guna2Panel1.Controls.Add(this.lblMon);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(262, 165);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // lblMon
            // 
            this.lblMon.AutoSize = true;
            this.lblMon.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMon.ForeColor = System.Drawing.Color.DimGray;
            this.lblMon.Location = new System.Drawing.Point(27, 94);
            this.lblMon.Name = "lblMon";
            this.lblMon.Size = new System.Drawing.Size(37, 17);
            this.lblMon.TabIndex = 9;
            this.lblMon.Text = "Note";
            // 
            // lblLietKe
            // 
            this.lblLietKe.AutoSize = true;
            this.lblLietKe.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLietKe.Location = new System.Drawing.Point(25, 117);
            this.lblLietKe.Name = "lblLietKe";
            this.lblLietKe.Size = new System.Drawing.Size(61, 25);
            this.lblLietKe.TabIndex = 10;
            this.lblLietKe.Text = "Value";
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = false;
            this.lblSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.Location = new System.Drawing.Point(0, 0);
            this.lblSoLuong.MaximumSize = new System.Drawing.Size(48, 48);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(48, 48);
            this.lblSoLuong.TabIndex = 11;
            this.lblSoLuong.Text = "xx";
            this.lblSoLuong.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMauNen
            // 
            this.panelMauNen.BorderRadius = 6;
            this.panelMauNen.Controls.Add(this.lblSoLuong);
            this.panelMauNen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panelMauNen.Location = new System.Drawing.Point(30, 24);
            this.panelMauNen.MaximumSize = new System.Drawing.Size(48, 48);
            this.panelMauNen.Name = "panelMauNen";
            this.panelMauNen.Size = new System.Drawing.Size(48, 48);
            this.panelMauNen.TabIndex = 12;
            // 
            // statcardMonHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Panel1);
            this.Name = "statcardMonHoc";
            this.Size = new System.Drawing.Size(262, 165);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.panelMauNen.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        public System.Windows.Forms.Label lblMon;
        public System.Windows.Forms.Label lblLietKe;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoLuong;
        private Guna.UI2.WinForms.Guna2Panel panelMauNen;
    }
}
