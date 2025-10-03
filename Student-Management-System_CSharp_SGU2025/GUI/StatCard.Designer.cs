namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class StatCard
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
            this.labelCardTitle = new System.Windows.Forms.Label();
            this.labelCardValue = new System.Windows.Forms.Label();
            this.pictureBoxCardIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCardIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCardTitle
            // 
            this.labelCardTitle.AutoSize = true;
            this.labelCardTitle.Font = new System.Drawing.Font("Inter", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.labelCardTitle.Location = new System.Drawing.Point(20, 20);
            this.labelCardTitle.Name = "labelCardTitle";
            this.labelCardTitle.Size = new System.Drawing.Size(71, 16);
            this.labelCardTitle.TabIndex = 0;
            this.labelCardTitle.Text = "Card Title";
            // 
            // labelCardValue
            // 
            this.labelCardValue.AutoSize = true;
            this.labelCardValue.Font = new System.Drawing.Font("Inter", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCardValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.labelCardValue.Location = new System.Drawing.Point(20, 45);
            this.labelCardValue.Name = "labelCardValue";
            this.labelCardValue.Size = new System.Drawing.Size(63, 25);
            this.labelCardValue.TabIndex = 1;
            this.labelCardValue.Text = "1234";
            // 
            // pictureBoxCardIcon
            // 
            this.pictureBoxCardIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCardIcon.Location = new System.Drawing.Point(150, 20);
            this.pictureBoxCardIcon.Name = "pictureBoxCardIcon";
            this.pictureBoxCardIcon.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxCardIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCardIcon.TabIndex = 2;
            this.pictureBoxCardIcon.TabStop = false;
            // 
            // StatCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBoxCardIcon);
            this.Controls.Add(this.labelCardValue);
            this.Controls.Add(this.labelCardTitle);
            this.Name = "StatCard";
            this.Size = new System.Drawing.Size(220, 100);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCardIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCardTitle;
        private System.Windows.Forms.Label labelCardValue;
        private System.Windows.Forms.PictureBox pictureBoxCardIcon;
    }
}
