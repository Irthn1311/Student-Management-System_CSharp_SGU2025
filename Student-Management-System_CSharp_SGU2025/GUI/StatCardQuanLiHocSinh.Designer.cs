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
            this.lbCardTitle = new System.Windows.Forms.Label();
            this.lbCardValue = new System.Windows.Forms.Label();
            this.lbCardNote = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbCardTitle
            // 
            this.lbCardTitle.AutoSize = true;
            this.lbCardTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lbCardTitle.Location = new System.Drawing.Point(25, 21);
            this.lbCardTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCardTitle.Name = "lbCardTitle";
            this.lbCardTitle.Size = new System.Drawing.Size(89, 22);
            this.lbCardTitle.TabIndex = 1;
            this.lbCardTitle.Text = "Card Title";
            // 
            // lbCardValue
            // 
            this.lbCardValue.AutoSize = true;
            this.lbCardValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lbCardValue.Location = new System.Drawing.Point(29, 56);
            this.lbCardValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCardValue.Name = "lbCardValue";
            this.lbCardValue.Size = new System.Drawing.Size(185, 29);
            this.lbCardValue.TabIndex = 2;
            this.lbCardValue.Text = "Card Nội Dung";
            this.lbCardValue.Click += new System.EventHandler(this.lbCardValue_Click);
            // 
            // lbCardNote
            // 
            this.lbCardNote.AutoSize = true;
            this.lbCardNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lbCardNote.Location = new System.Drawing.Point(31, 97);
            this.lbCardNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCardNote.Name = "lbCardNote";
            this.lbCardNote.Size = new System.Drawing.Size(98, 18);
            this.lbCardNote.TabIndex = 3;
            this.lbCardNote.Text = "Card Ghi Chú";
            // 
            // StatCardQuanLiHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbCardNote);
            this.Controls.Add(this.lbCardValue);
            this.Controls.Add(this.lbCardTitle);
            this.Name = "StatCardQuanLiHocSinh";
            this.Size = new System.Drawing.Size(264, 142);
            this.Load += new System.EventHandler(this.StatCardQuanLiHocSinh_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbCardTitle;
        public System.Windows.Forms.Label lbCardValue;
        public System.Windows.Forms.Label lbCardNote;
    }
}
