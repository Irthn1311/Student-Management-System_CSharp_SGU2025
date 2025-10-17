namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ThongKeCard
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
            this.pnlThongKe = new Guna.UI2.WinForms.Guna2Panel();
            this.lblNote = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblNumber = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTieuDe = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlThongKe.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlThongKe
            // 
            this.pnlThongKe.BackColor = System.Drawing.Color.Transparent;
            this.pnlThongKe.BorderColor = System.Drawing.Color.Transparent;
            this.pnlThongKe.BorderRadius = 15;
            this.pnlThongKe.Controls.Add(this.lblNote);
            this.pnlThongKe.Controls.Add(this.lblNumber);
            this.pnlThongKe.Controls.Add(this.lblTieuDe);
            this.pnlThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThongKe.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlThongKe.Location = new System.Drawing.Point(0, 0);
            this.pnlThongKe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlThongKe.Name = "pnlThongKe";
            this.pnlThongKe.ShadowDecoration.BorderRadius = 10;
            this.pnlThongKe.ShadowDecoration.Color = System.Drawing.Color.Gray;
            this.pnlThongKe.ShadowDecoration.Enabled = true;
            this.pnlThongKe.Size = new System.Drawing.Size(198, 115);
            this.pnlThongKe.TabIndex = 0;
            this.pnlThongKe.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlThongKe_Paint);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = false;
            this.lblNote.BackColor = System.Drawing.Color.Transparent;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblNote.Location = new System.Drawing.Point(24, 80);
            this.lblNote.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(160, 32);
            this.lblNote.TabIndex = 2;
            this.lblNote.Text = "guna2HtmlLabel1";
            this.lblNote.Click += new System.EventHandler(this.lblNote_Click);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = false;
            this.lblNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblNumber.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.White;
            this.lblNumber.Location = new System.Drawing.Point(24, 43);
            this.lblNumber.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(160, 31);
            this.lblNumber.TabIndex = 1;
            this.lblNumber.Text = "Number";
            this.lblNumber.Click += new System.EventHandler(this.lblNumber_Click);
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = false;
            this.lblTieuDe.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.White;
            this.lblTieuDe.Location = new System.Drawing.Point(24, 16);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(160, 22);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tiêu Đề";
            this.lblTieuDe.Click += new System.EventHandler(this.lblTieuDe_Click);
            // 
            // ThongKeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnlThongKe);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ThongKeCard";
            this.Size = new System.Drawing.Size(198, 115);
            this.pnlThongKe.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlThongKe;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNumber;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTieuDe;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNote;
    }
}
