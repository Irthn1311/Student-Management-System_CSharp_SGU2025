namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class StatCardPhanCongGiangDay
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
            this.lblTen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderRadius = 6;
            this.guna2Panel1.Controls.Add(this.lblSoLuong);
            this.guna2Panel1.Controls.Add(this.lblTen);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(211, 99);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.Location = new System.Drawing.Point(18, 48);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(85, 27);
            this.lblSoLuong.TabIndex = 3;
            this.lblSoLuong.Text = "Số lượng";
            // 
            // lblTen
            // 
            this.lblTen.BackColor = System.Drawing.Color.Transparent;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.ForeColor = System.Drawing.Color.DimGray;
            this.lblTen.Location = new System.Drawing.Point(18, 16);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(65, 23);
            this.lblTen.TabIndex = 2;
            this.lblTen.Text = "Tên Card";
            // 
            // StatCardPhanCongGiangDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Panel1);
            this.Name = "StatCardPhanCongGiangDay";
            this.Size = new System.Drawing.Size(211, 99);
            this.Load += new System.EventHandler(this.StatCardPhanCongGiangDay_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoLuong;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTen;
    }
}
