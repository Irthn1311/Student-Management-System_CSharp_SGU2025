namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucSidebar1 = new Student_Management_System_CSharp_SGU2025.GUI.ucSidebar();
            this.ucHeader1 = new Student_Management_System_CSharp_SGU2025.GUI.ucHeader();
            this.SuspendLayout();
            // 
            // ucSidebar1
            // 
            this.ucSidebar1.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucSidebar1.Location = new System.Drawing.Point(0, 0);
            this.ucSidebar1.Margin = new System.Windows.Forms.Padding(2);
            this.ucSidebar1.Name = "ucSidebar1";
            this.ucSidebar1.Size = new System.Drawing.Size(256, 900);
            this.ucSidebar1.TabIndex = 0;
            // 
            // ucHeader1
            // 
            this.ucHeader1.Location = new System.Drawing.Point(256, 0);
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.Size = new System.Drawing.Size(1184, 80);
            this.ucHeader1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.ucHeader1);
            this.Controls.Add(this.ucSidebar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private ucSidebar ucSidebar1;
        private ucHeader ucHeader1;
    }
}