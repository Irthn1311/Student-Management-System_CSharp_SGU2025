namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FrmXacThucOTP
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
            this.lbNhapMaOTP = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtOTP = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnGuiLai = new Guna.UI2.WinForms.Guna2Button();
            this.lblTimeLeft = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // lbNhapMaOTP
            // 
            this.lbNhapMaOTP.AutoSize = false;
            this.lbNhapMaOTP.BackColor = System.Drawing.Color.Transparent;
            this.lbNhapMaOTP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNhapMaOTP.ForeColor = System.Drawing.Color.Black;
            this.lbNhapMaOTP.Location = new System.Drawing.Point(25, 125);
            this.lbNhapMaOTP.Name = "lbNhapMaOTP";
            this.lbNhapMaOTP.Size = new System.Drawing.Size(284, 23);
            this.lbNhapMaOTP.TabIndex = 0;
            this.lbNhapMaOTP.Text = "Nhập mã OTP đã gửi đến email của bạn :";
            // 
            // txtOTP
            // 
            this.txtOTP.BorderRadius = 5;
            this.txtOTP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOTP.DefaultText = "";
            this.txtOTP.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtOTP.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtOTP.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtOTP.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtOTP.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtOTP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOTP.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtOTP.Location = new System.Drawing.Point(325, 123);
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.PlaceholderText = "";
            this.txtOTP.SelectedText = "";
            this.txtOTP.Size = new System.Drawing.Size(233, 36);
            this.txtOTP.TabIndex = 1;
            // 
            // btnGuiLai
            // 
            this.btnGuiLai.BorderRadius = 7;
            this.btnGuiLai.BorderThickness = 1;
            this.btnGuiLai.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGuiLai.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGuiLai.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuiLai.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGuiLai.FillColor = System.Drawing.Color.White;
            this.btnGuiLai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuiLai.ForeColor = System.Drawing.Color.Black;
            this.btnGuiLai.ImageSize = new System.Drawing.Size(15, 15);
            this.btnGuiLai.Location = new System.Drawing.Point(293, 187);
            this.btnGuiLai.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuiLai.Name = "btnGuiLai";
            this.btnGuiLai.Size = new System.Drawing.Size(122, 36);
            this.btnGuiLai.TabIndex = 47;
            this.btnGuiLai.Text = "Gửi lại";
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.AutoSize = false;
            this.lblTimeLeft.BackColor = System.Drawing.Color.White;
            this.lblTimeLeft.Location = new System.Drawing.Point(163, 41);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(252, 27);
            this.lblTimeLeft.TabIndex = 48;
            this.lblTimeLeft.Text = "Thời gian xác thực";
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BorderColor = System.Drawing.Color.White;
            this.btnXacNhan.BorderRadius = 7;
            this.btnXacNhan.BorderThickness = 1;
            this.btnXacNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Image = global::Student_Management_System_CSharp_SGU2025.GUI.Properties.Resources.plus;
            this.btnXacNhan.ImageSize = new System.Drawing.Size(15, 15);
            this.btnXacNhan.Location = new System.Drawing.Point(436, 187);
            this.btnXacNhan.Margin = new System.Windows.Forms.Padding(2);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(122, 36);
            this.btnXacNhan.TabIndex = 46;
            this.btnXacNhan.Text = "Xác nhận";
            // 
            // FrmXacThucOTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 246);
            this.Controls.Add(this.lblTimeLeft);
            this.Controls.Add(this.btnGuiLai);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtOTP);
            this.Controls.Add(this.lbNhapMaOTP);
            this.MaximizeBox = false;
            this.Name = "FrmXacThucOTP";
            this.Text = "FrmXacThucOTP";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lbNhapMaOTP;
        private Guna.UI2.WinForms.Guna2TextBox txtOTP;
        private Guna.UI2.WinForms.Guna2Button btnXacNhan;
        private Guna.UI2.WinForms.Guna2Button btnGuiLai;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTimeLeft;
    }
}