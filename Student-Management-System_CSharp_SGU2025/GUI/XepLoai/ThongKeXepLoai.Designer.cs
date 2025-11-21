namespace Student_Management_System_CSharp_SGU2025.GUI.XepLoai
{
    partial class ThongKeXepLoai
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
            this.pnlThongKeXepLoai = new Guna.UI2.WinForms.Guna2Panel();
            this.tieuDe3 = new System.Windows.Forms.Label();
            this.tieuDe2 = new System.Windows.Forms.Label();
            this.tieuDe1 = new System.Windows.Forms.Label();
            this.pnlThongKeXepLoai.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlThongKeXepLoai
            // 
            this.pnlThongKeXepLoai.BackColor = System.Drawing.Color.Transparent;
            this.pnlThongKeXepLoai.BorderColor = System.Drawing.Color.Transparent;
            this.pnlThongKeXepLoai.BorderRadius = 15;
            this.pnlThongKeXepLoai.Controls.Add(this.tieuDe3);
            this.pnlThongKeXepLoai.Controls.Add(this.tieuDe2);
            this.pnlThongKeXepLoai.Controls.Add(this.tieuDe1);
            this.pnlThongKeXepLoai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThongKeXepLoai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlThongKeXepLoai.Location = new System.Drawing.Point(0, 0);
            this.pnlThongKeXepLoai.Margin = new System.Windows.Forms.Padding(2);
            this.pnlThongKeXepLoai.Name = "pnlThongKeXepLoai";
            this.pnlThongKeXepLoai.ShadowDecoration.BorderRadius = 10;
            this.pnlThongKeXepLoai.ShadowDecoration.Color = System.Drawing.Color.Gray;
            this.pnlThongKeXepLoai.ShadowDecoration.Enabled = true;
            this.pnlThongKeXepLoai.Size = new System.Drawing.Size(262, 136);
            this.pnlThongKeXepLoai.TabIndex = 1;
            this.pnlThongKeXepLoai.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlThongKeXepLoai_Paint);
            // 
            // tieuDe3
            // 
            this.tieuDe3.AutoSize = true;
            this.tieuDe3.BackColor = System.Drawing.Color.Transparent;
            this.tieuDe3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tieuDe3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tieuDe3.Location = new System.Drawing.Point(23, 89);
            this.tieuDe3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tieuDe3.Name = "tieuDe3";
            this.tieuDe3.Size = new System.Drawing.Size(63, 16);
            this.tieuDe3.TabIndex = 5;
            this.tieuDe3.Text = "Tiêu đề 3";
            this.tieuDe3.Click += new System.EventHandler(this.tieuDe3_Click);
            // 
            // tieuDe2
            // 
            this.tieuDe2.AutoSize = true;
            this.tieuDe2.BackColor = System.Drawing.Color.Transparent;
            this.tieuDe2.Font = new System.Drawing.Font("Microsoft Sans Serif", 27F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.tieuDe2.ForeColor = System.Drawing.Color.White;
            this.tieuDe2.Location = new System.Drawing.Point(23, 49);
            this.tieuDe2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tieuDe2.Name = "tieuDe2";
            this.tieuDe2.Size = new System.Drawing.Size(135, 31);
            this.tieuDe2.TabIndex = 4;
            this.tieuDe2.Text = "Tiêu đề 2";
            this.tieuDe2.Click += new System.EventHandler(this.tieuDe2_Click);
            // 
            // tieuDe1
            // 
            this.tieuDe1.AutoSize = true;
            this.tieuDe1.BackColor = System.Drawing.Color.Transparent;
            this.tieuDe1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.tieuDe1.ForeColor = System.Drawing.Color.White;
            this.tieuDe1.Location = new System.Drawing.Point(23, 17);
            this.tieuDe1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tieuDe1.Name = "tieuDe1";
            this.tieuDe1.Size = new System.Drawing.Size(72, 16);
            this.tieuDe1.TabIndex = 3;
            this.tieuDe1.Text = "Tiêu đề 1";
            this.tieuDe1.Click += new System.EventHandler(this.tieuDe1_Click);
            // 
            // ThongKeXepLoai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlThongKeXepLoai);
            this.Name = "ThongKeXepLoai";
            this.Size = new System.Drawing.Size(262, 136);
            this.pnlThongKeXepLoai.ResumeLayout(false);
            this.pnlThongKeXepLoai.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlThongKeXepLoai;
        private System.Windows.Forms.Label tieuDe3;
        private System.Windows.Forms.Label tieuDe2;
        private System.Windows.Forms.Label tieuDe1;
    }
}
