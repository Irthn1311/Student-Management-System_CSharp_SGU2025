namespace Student_Management_System_CSharp_SGU2025
{
    partial class DangNhap
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnForgot;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.btnForgot = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.lblWelcome);
            this.panelLeft.Controls.Add(this.lblSub);
            this.panelLeft.Controls.Add(this.lblTitle);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(960, 1200);
            this.panelLeft.TabIndex = 0;
            this.panelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLeft_Paint_1);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(50, 283);
            this.lblWelcome.MaximumSize = new System.Drawing.Size(990, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(317, 48);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Chào mừng trở lại!";
            this.lblWelcome.Click += new System.EventHandler(this.lblWelcome_Click);
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Italic);
            this.lblSub.ForeColor = System.Drawing.Color.White;
            this.lblSub.Location = new System.Drawing.Point(45, 188);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(505, 54);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "School Management System";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(43, 100);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(543, 86);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "IrthnEduManage";
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.label2);
            this.panelRight.Controls.Add(this.label3);
            this.panelRight.Controls.Add(this.btnForgot);
            this.panelRight.Controls.Add(this.btnLogin);
            this.panelRight.Controls.Add(this.cbRole);
            this.panelRight.Controls.Add(this.lblRole);
            this.panelRight.Controls.Add(this.txtPass);
            this.panelRight.Controls.Add(this.lblPass);
            this.panelRight.Controls.Add(this.txtUser);
            this.panelRight.Controls.Add(this.lblUser);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(960, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(960, 1200);
            this.panelRight.TabIndex = 1;
            // 
            // btnForgot
            // 
            this.btnForgot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnForgot.FlatAppearance.BorderSize = 0;
            this.btnForgot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForgot.ForeColor = System.Drawing.Color.Blue;
            this.btnForgot.Location = new System.Drawing.Point(340, 921);
            this.btnForgot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnForgot.Name = "btnForgot";
            this.btnForgot.Size = new System.Drawing.Size(338, 50);
            this.btnForgot.TabIndex = 7;
            this.btnForgot.Text = "Quên mật khẩu?";
            this.btnForgot.UseVisualStyleBackColor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogin.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(340, 818);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(338, 75);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // cbRole
            // 
            this.cbRole.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRole.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Items.AddRange(new object[] {
            "Quản trị viên",
            "Giáo viên",
            "Học sinh"});
            this.cbRole.Location = new System.Drawing.Point(162, 730);
            this.cbRole.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(337, 46);
            this.cbRole.TabIndex = 5;
            // 
            // lblRole
            // 
            this.lblRole.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblRole.Location = new System.Drawing.Point(155, 655);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(96, 38);
            this.lblRole.TabIndex = 4;
            this.lblRole.Text = "Vai trò";
            // 
            // txtPass
            // 
            this.txtPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtPass.Location = new System.Drawing.Point(162, 581);
            this.txtPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(600, 45);
            this.txtPass.TabIndex = 3;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // lblPass
            // 
            this.lblPass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblPass.Location = new System.Drawing.Point(155, 504);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(133, 38);
            this.lblPass.TabIndex = 2;
            this.lblPass.Text = "Mật khẩu";
            this.lblPass.Click += new System.EventHandler(this.lblPass_Click);
            // 
            // txtUser
            // 
            this.txtUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtUser.Location = new System.Drawing.Point(162, 407);
            this.txtUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(589, 45);
            this.txtUser.TabIndex = 1;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            // 
            // lblUser
            // 
            this.lblUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblUser.Location = new System.Drawing.Point(155, 319);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(200, 38);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Tên đăng nhập";
            this.lblUser.Click += new System.EventHandler(this.lblUser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(50, 347);
            this.label1.MaximumSize = new System.Drawing.Size(700, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(697, 96);
            this.label1.TabIndex = 3;
            this.label1.Text = "Vui lòng đăng nhập để truy cập bảng điều khiển và quản lý trường học.";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(406, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 54);
            this.label3.TabIndex = 9;
            this.label3.Text = "Đăng nhập";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.label2.Location = new System.Drawing.Point(334, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "Vui lòng nhập thông tin để tiếp tục";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1200);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "DangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - IrthnEduManage";
            this.Load += new System.EventHandler(this.DangNhap_Load);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}
