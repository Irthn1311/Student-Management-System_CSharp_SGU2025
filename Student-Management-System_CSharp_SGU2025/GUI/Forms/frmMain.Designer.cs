namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelContent = new System.Windows.Forms.Panel();
            this.ucSidebar1 = new Student_Management_System_CSharp_SGU2025.GUI.ucSidebar();
            this.ucHeader1 = new Student_Management_System_CSharp_SGU2025.GUI.ucHeader();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.SystemColors.Control;
            this.panelContent.Location = new System.Drawing.Point(256, 84);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1168, 777);
            this.panelContent.TabIndex = 4;
            this.panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContent_Paint);
            // 
            // ucSidebar1
            // 
            this.ucSidebar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucSidebar1.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucSidebar1.Location = new System.Drawing.Point(0, 0);
            this.ucSidebar1.Margin = new System.Windows.Forms.Padding(2);
            this.ucSidebar1.Name = "ucSidebar1";
            this.ucSidebar1.Size = new System.Drawing.Size(256, 861);
            this.ucSidebar1.TabIndex = 3;
            this.ucSidebar1.Load += new System.EventHandler(this.ucSidebar1_Load);
            // 
            // ucHeader1
            // 
            this.ucHeader1.Location = new System.Drawing.Point(256, 0);
            this.ucHeader1.Name = "ucHeader1";
            this.ucHeader1.Size = new System.Drawing.Size(1184, 87);
            this.ucHeader1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 861);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.ucSidebar1);
            this.Controls.Add(this.ucHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private ucSidebar ucSidebar1;
        private ucHeader ucHeader1;
        private System.Windows.Forms.Panel panelContent;
    }
}