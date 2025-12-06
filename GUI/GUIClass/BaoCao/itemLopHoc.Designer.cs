namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class itemLopHoc
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
            this.pnlClass1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnXem = new Guna.UI2.WinForms.Guna2Button();
            this.lblClassInfo = new System.Windows.Forms.Label();
            this.lblClassName = new System.Windows.Forms.Label();
            this.pnlClass1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlClass1
            // 
            this.pnlClass1.BorderColor = System.Drawing.Color.DarkGray;
            this.pnlClass1.BorderRadius = 8;
            this.pnlClass1.BorderThickness = 1;
            this.pnlClass1.Controls.Add(this.btnXem);
            this.pnlClass1.Controls.Add(this.lblClassInfo);
            this.pnlClass1.Controls.Add(this.lblClassName);
            this.pnlClass1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlClass1.Location = new System.Drawing.Point(0, 0);
            this.pnlClass1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 16);
            this.pnlClass1.Name = "pnlClass1";
            this.pnlClass1.Padding = new System.Windows.Forms.Padding(17);
            this.pnlClass1.Size = new System.Drawing.Size(1071, 77);
            this.pnlClass1.TabIndex = 1;
            this.pnlClass1.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlClass1_Paint);
            // 
            // btnXem
            // 
            this.btnXem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXem.BorderRadius = 8;
            this.btnXem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnXem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(953, 23);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(98, 32);
            this.btnXem.TabIndex = 2;
            this.btnXem.Text = "Xem chi tiết";
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // lblClassInfo
            // 
            this.lblClassInfo.AutoSize = true;
            this.lblClassInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblClassInfo.Location = new System.Drawing.Point(17, 40);
            this.lblClassInfo.Name = "lblClassInfo";
            this.lblClassInfo.Size = new System.Drawing.Size(232, 15);
            this.lblClassInfo.TabIndex = 1;
            this.lblClassInfo.Text = "Sĩ số: 42 học sinh - GVCN: Nguyễn Thị Hoa";
            this.lblClassInfo.Click += new System.EventHandler(this.lblClassInfo_Click);
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblClassName.Location = new System.Drawing.Point(17, 17);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(77, 20);
            this.lblClassName.TabIndex = 0;
            this.lblClassName.Text = "Lớp 10A1";
            this.lblClassName.Click += new System.EventHandler(this.lblClassName_Click);
            // 
            // itemLopHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlClass1);
            this.Name = "itemLopHoc";
            this.Size = new System.Drawing.Size(1071, 77);
            this.pnlClass1.ResumeLayout(false);
            this.pnlClass1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlClass1;
        private Guna.UI2.WinForms.Guna2Button btnXem;
        private System.Windows.Forms.Label lblClassInfo;
        private System.Windows.Forms.Label lblClassName;
    }
}
