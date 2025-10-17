namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class StatCardQuanLiHocSinh
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
            this.lbCardNote = new System.Windows.Forms.Label();
            this.lbCardValue = new System.Windows.Forms.Label();
            this.lbCardTitle = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderRadius = 7;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.lbCardNote);
            this.guna2Panel1.Controls.Add(this.lbCardValue);
            this.guna2Panel1.Controls.Add(this.lbCardTitle);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(198, 115);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lbCardNote
            // 
            this.lbCardNote.AutoSize = true;
            this.lbCardNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lbCardNote.Location = new System.Drawing.Point(25, 81);
            this.lbCardNote.Name = "lbCardNote";
            this.lbCardNote.Size = new System.Drawing.Size(80, 15);
            this.lbCardNote.TabIndex = 6;
            this.lbCardNote.Text = "Card Ghi Chú";
            // 
            // lbCardValue
            // 
            this.lbCardValue.AutoSize = true;
            this.lbCardValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lbCardValue.Location = new System.Drawing.Point(24, 48);
            this.lbCardValue.Name = "lbCardValue";
            this.lbCardValue.Size = new System.Drawing.Size(148, 24);
            this.lbCardValue.TabIndex = 5;
            this.lbCardValue.Text = "Card Nội Dung";
            // 
            // lbCardTitle
            // 
            this.lbCardTitle.AutoSize = true;
            this.lbCardTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lbCardTitle.Location = new System.Drawing.Point(21, 19);
            this.lbCardTitle.Name = "lbCardTitle";
            this.lbCardTitle.Size = new System.Drawing.Size(71, 18);
            this.lbCardTitle.TabIndex = 4;
            this.lbCardTitle.Text = "Card Title";
            // 
            // StatCardQuanLiHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StatCardQuanLiHocSinh";
            this.Size = new System.Drawing.Size(198, 115);
            this.Load += new System.EventHandler(this.StatCardQuanLiHocSinh_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        public System.Windows.Forms.Label lbCardNote;
        public System.Windows.Forms.Label lbCardValue;
        public System.Windows.Forms.Label lbCardTitle;
    }
}
