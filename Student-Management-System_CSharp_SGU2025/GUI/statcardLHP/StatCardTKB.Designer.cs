namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    partial class StatCardTKB
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
            this.lblPhong = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblGiaoVien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMonHoc = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.Controls.Add(this.lblPhong);
            this.guna2Panel1.Controls.Add(this.lblGiaoVien);
            this.guna2Panel1.Controls.Add(this.lblMonHoc);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(154, 103);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lblPhong
            // 
            this.lblPhong.AutoSize = false;
            this.lblPhong.BackColor = System.Drawing.Color.Transparent;
            this.lblPhong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhong.Location = new System.Drawing.Point(3, 65);
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.Size = new System.Drawing.Size(148, 20);
            this.lblPhong.TabIndex = 2;
            this.lblPhong.Text = "Phòng";
            this.lblPhong.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPhong.Click += new System.EventHandler(this.lblPhong_Click);
            // 
            // lblGiaoVien
            // 
            this.lblGiaoVien.AutoSize = false;
            this.lblGiaoVien.BackColor = System.Drawing.Color.Transparent;
            this.lblGiaoVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaoVien.Location = new System.Drawing.Point(3, 40);
            this.lblGiaoVien.Name = "lblGiaoVien";
            this.lblGiaoVien.Size = new System.Drawing.Size(148, 20);
            this.lblGiaoVien.TabIndex = 1;
            this.lblGiaoVien.Text = "Giáo viên";
            this.lblGiaoVien.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMonHoc
            // 
            this.lblMonHoc.AutoSize = false;
            this.lblMonHoc.BackColor = System.Drawing.Color.Transparent;
            this.lblMonHoc.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonHoc.Location = new System.Drawing.Point(3, 10);
            this.lblMonHoc.Name = "lblMonHoc";
            this.lblMonHoc.Size = new System.Drawing.Size(148, 25);
            this.lblMonHoc.TabIndex = 0;
            this.lblMonHoc.Text = "Môn Học";
            this.lblMonHoc.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StatCardTKB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.guna2Panel1);
            this.Name = "StatCardTKB";
            this.Size = new System.Drawing.Size(154, 103);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPhong;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGiaoVien;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMonHoc;
    }
}
