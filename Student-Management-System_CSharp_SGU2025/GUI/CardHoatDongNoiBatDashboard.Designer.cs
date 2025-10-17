namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class CardHoatDongNoiBatDashboard
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
            this.lbCardGhiChu = new System.Windows.Forms.Label();
            this.lbCardValue = new System.Windows.Forms.Label();
            this.lbCardName = new System.Windows.Forms.Label();
            this.PictureBoxThongBao = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxThongBao)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Panel1.BorderRadius = 5;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.lbCardGhiChu);
            this.guna2Panel1.Controls.Add(this.lbCardValue);
            this.guna2Panel1.Controls.Add(this.lbCardName);
            this.guna2Panel1.Controls.Add(this.PictureBoxThongBao);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(256, 165);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // lbCardGhiChu
            // 
            this.lbCardGhiChu.AutoSize = true;
            this.lbCardGhiChu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardGhiChu.ForeColor = System.Drawing.Color.DimGray;
            this.lbCardGhiChu.Location = new System.Drawing.Point(32, 133);
            this.lbCardGhiChu.Name = "lbCardGhiChu";
            this.lbCardGhiChu.Size = new System.Drawing.Size(37, 17);
            this.lbCardGhiChu.TabIndex = 8;
            this.lbCardGhiChu.Text = "Note";
            // 
            // lbCardValue
            // 
            this.lbCardValue.AutoSize = true;
            this.lbCardValue.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardValue.Location = new System.Drawing.Point(28, 97);
            this.lbCardValue.Name = "lbCardValue";
            this.lbCardValue.Size = new System.Drawing.Size(61, 25);
            this.lbCardValue.TabIndex = 7;
            this.lbCardValue.Text = "Value";
            // 
            // lbCardName
            // 
            this.lbCardName.AutoSize = true;
            this.lbCardName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCardName.Location = new System.Drawing.Point(29, 63);
            this.lbCardName.Name = "lbCardName";
            this.lbCardName.Size = new System.Drawing.Size(88, 21);
            this.lbCardName.TabIndex = 6;
            this.lbCardName.Text = "TextName";
            // 
            // PictureBoxThongBao
            // 
            this.PictureBoxThongBao.BorderRadius = 5;
            this.PictureBoxThongBao.FillColor = System.Drawing.Color.RosyBrown;
            this.PictureBoxThongBao.ImageRotate = 0F;
            this.PictureBoxThongBao.Location = new System.Drawing.Point(32, 15);
            this.PictureBoxThongBao.MaximumSize = new System.Drawing.Size(40, 40);
            this.PictureBoxThongBao.Name = "PictureBoxThongBao";
            this.PictureBoxThongBao.Size = new System.Drawing.Size(40, 40);
            this.PictureBoxThongBao.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBoxThongBao.TabIndex = 5;
            this.PictureBoxThongBao.TabStop = false;
            // 
            // CardHoatDongNoiBatDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Panel1);
            this.MaximumSize = new System.Drawing.Size(256, 165);
            this.Name = "CardHoatDongNoiBatDashboard";
            this.Size = new System.Drawing.Size(256, 165);
            this.Load += new System.EventHandler(this.CardHoatDongNoiBatDashboard_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxThongBao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        public System.Windows.Forms.Label lbCardGhiChu;
        public System.Windows.Forms.Label lbCardValue;
        public System.Windows.Forms.Label lbCardName;
        public Guna.UI2.WinForms.Guna2PictureBox PictureBoxThongBao;
    }
}
