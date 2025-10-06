namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucDashboard1 = new Student_Management_System_CSharp_SGU2025.GUI.ucDashboard();
            this.ucSidebar1 = new Student_Management_System_CSharp_SGU2025.GUI.ucSidebar();
            this.ucHeader1 = new Student_Management_System_CSharp_SGU2025.GUI.ucHeader();
            this.ucXepLoai1 = new Student_Management_System_CSharp_SGU2025.GUI.ucXepLoai();
            this.ucBaoCao1 = new Student_Management_System_CSharp_SGU2025.GUI.ucBaoCao();
            this.SuspendLayout();
            // 
            // ucDashboard1
            // 
            this.ucDashboard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.ucDashboard1.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucDashboard1.Location = new System.Drawing.Point(256, 80);
            this.ucDashboard1.Margin = new System.Windows.Forms.Padding(2);
            this.ucDashboard1.Name = "ucDashboard1";
            this.ucDashboard1.Size = new System.Drawing.Size(1184, 900);
            this.ucDashboard1.TabIndex = 1;
            this.ucDashboard1.Load += new System.EventHandler(this.ucDashboard1_Load);
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
            // ucXepLoai1
            // 
            this.ucXepLoai1.AutoScroll = true;
            this.ucXepLoai1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.ucXepLoai1.Location = new System.Drawing.Point(256, 80);
            this.ucXepLoai1.Margin = new System.Windows.Forms.Padding(2);
            this.ucXepLoai1.Name = "ucXepLoai1";
            this.ucXepLoai1.Padding = new System.Windows.Forms.Padding(24);
            this.ucXepLoai1.Size = new System.Drawing.Size(1184, 900);
            this.ucXepLoai1.TabIndex = 3;
            this.ucXepLoai1.Visible = false;
            // 
            // ucBaoCao1
            // 
            this.ucBaoCao1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.ucBaoCao1.Location = new System.Drawing.Point(256, 80);
            this.ucBaoCao1.Margin = new System.Windows.Forms.Padding(2);
            this.ucBaoCao1.Name = "ucBaoCao1";
            this.ucBaoCao1.Size = new System.Drawing.Size(1184, 900);
            this.ucBaoCao1.TabIndex = 4;
            this.ucBaoCao1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.ucHeader1);
            this.Controls.Add(this.ucBaoCao1);
            this.Controls.Add(this.ucXepLoai1);
            this.Controls.Add(this.ucDashboard1);
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
        private ucDashboard ucDashboard1;
        private ucHeader ucHeader1;
        private ucXepLoai ucXepLoai1;
        private ucBaoCao ucBaoCao1;
    }
}