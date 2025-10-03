namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class RecentActivityItem
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
            this.panelIndicator = new System.Windows.Forms.Panel();
            this.labelActivityText = new System.Windows.Forms.Label();
            this.labelTimeText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelIndicator
            // 
            this.panelIndicator.BackColor = System.Drawing.Color.Blue;
            this.panelIndicator.Location = new System.Drawing.Point(12, 12);
            this.panelIndicator.Name = "panelIndicator";
            this.panelIndicator.Size = new System.Drawing.Size(8, 8);
            this.panelIndicator.TabIndex = 0;
            // 
            // labelActivityText
            // 
            this.labelActivityText.AutoSize = true;
            this.labelActivityText.Font = new System.Drawing.Font("Inter", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelActivityText.Location = new System.Drawing.Point(30, 10);
            this.labelActivityText.Name = "labelActivityText";
            this.labelActivityText.Size = new System.Drawing.Size(95, 16);
            this.labelActivityText.TabIndex = 1;
            this.labelActivityText.Text = "Activity Text";
            // 
            // labelTimeText
            // 
            this.labelTimeText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTimeText.AutoSize = true;
            this.labelTimeText.Font = new System.Drawing.Font("Inter", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeText.ForeColor = System.Drawing.Color.Gray;
            this.labelTimeText.Location = new System.Drawing.Point(200, 12);
            this.labelTimeText.Name = "labelTimeText";
            this.labelTimeText.Size = new System.Drawing.Size(60, 14);
            this.labelTimeText.TabIndex = 2;
            this.labelTimeText.Text = "Time Text";
            // 
            // RecentActivityItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTimeText);
            this.Controls.Add(this.labelActivityText);
            this.Controls.Add(this.panelIndicator);
            this.Name = "RecentActivityItem";
            this.Size = new System.Drawing.Size(300, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelIndicator;
        private System.Windows.Forms.Label labelActivityText;
        private System.Windows.Forms.Label labelTimeText;
    }
}
