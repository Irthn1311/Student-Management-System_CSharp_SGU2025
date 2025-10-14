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
            this.lbCardNote = new System.Windows.Forms.Label();
            this.lbCardTitle = new System.Windows.Forms.Label();
            this.lbCardValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbCardNote
            // 
            this.lbCardNote.AutoSize = true;
            this.lbCardNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lbCardNote.Location = new System.Drawing.Point(23, 79);
            this.lbCardNote.Name = "lbCardNote";
            this.lbCardNote.Size = new System.Drawing.Size(80, 15);
            this.lbCardNote.TabIndex = 3;
            this.lbCardNote.Text = "Card Ghi Chú";
            // 
            // lbCardTitle
            // 
            this.lbCardTitle.AutoSize = true;
            this.lbCardTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lbCardTitle.Location = new System.Drawing.Point(19, 17);
            this.lbCardTitle.Name = "lbCardTitle";
            this.lbCardTitle.Size = new System.Drawing.Size(71, 18);
            this.lbCardTitle.TabIndex = 1;
            this.lbCardTitle.Text = "Card Title";
            // 
            // lbCardValue
            // 
            this.lbCardValue.AutoSize = true;
            this.lbCardValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lbCardValue.Location = new System.Drawing.Point(22, 46);
            this.lbCardValue.Name = "lbCardValue";
            this.lbCardValue.Size = new System.Drawing.Size(148, 24);
            this.lbCardValue.TabIndex = 2;
            this.lbCardValue.Text = "Card Nội Dung";
            this.lbCardValue.Click += new System.EventHandler(this.lbCardValue_Click);
            // 
            // StatCardQuanLiHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbCardNote);
            this.Controls.Add(this.lbCardValue);
            this.Controls.Add(this.lbCardTitle);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "StatCardQuanLiHocSinh";
            this.Size = new System.Drawing.Size(198, 115);
            this.Load += new System.EventHandler(this.StatCardQuanLiHocSinh_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbCardNote;
        public System.Windows.Forms.Label lbCardTitle;
        public System.Windows.Forms.Label lbCardValue;
    }
}
