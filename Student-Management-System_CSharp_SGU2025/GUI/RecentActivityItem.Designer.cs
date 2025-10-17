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
            this.lbTextName = new System.Windows.Forms.Label();
            this.lbNote = new System.Windows.Forms.Label();
            this.PictureBoxThongBao = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxThongBao)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTextName
            // 
            this.lbTextName.AutoSize = true;
            this.lbTextName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextName.Location = new System.Drawing.Point(46, 0);
            this.lbTextName.Name = "lbTextName";
            this.lbTextName.Size = new System.Drawing.Size(70, 17);
            this.lbTextName.TabIndex = 1;
            this.lbTextName.Text = "TextName";
            // 
            // lbNote
            // 
            this.lbNote.AutoSize = true;
            this.lbNote.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNote.ForeColor = System.Drawing.Color.DimGray;
            this.lbNote.Location = new System.Drawing.Point(46, 17);
            this.lbNote.Name = "lbNote";
            this.lbNote.Size = new System.Drawing.Size(32, 13);
            this.lbNote.TabIndex = 2;
            this.lbNote.Text = "Note";
            // 
            // PictureBoxThongBao
            // 
            this.PictureBoxThongBao.FillColor = System.Drawing.Color.RosyBrown;
            this.PictureBoxThongBao.ImageRotate = 0F;
            this.PictureBoxThongBao.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxThongBao.MaximumSize = new System.Drawing.Size(40, 40);
            this.PictureBoxThongBao.Name = "PictureBoxThongBao";
            this.PictureBoxThongBao.Size = new System.Drawing.Size(40, 40);
            this.PictureBoxThongBao.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBoxThongBao.TabIndex = 0;
            this.PictureBoxThongBao.TabStop = false;
            // 
            // RecentActivityItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lbNote);
            this.Controls.Add(this.lbTextName);
            this.Controls.Add(this.PictureBoxThongBao);
            this.Name = "RecentActivityItem";
            this.Size = new System.Drawing.Size(308, 40);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxThongBao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Guna.UI2.WinForms.Guna2PictureBox PictureBoxThongBao;
        public System.Windows.Forms.Label lbTextName;
        public System.Windows.Forms.Label lbNote;
    }
}
