namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class frmPhanQuyen
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
            this.lblPhanQuyen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlRoleContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.btnExit = new Guna.UI2.WinForms.Guna2Button();
            this.btnAddRole = new Guna.UI2.WinForms.Guna2Button();
            this.roleItem4 = new Student_Management_System_CSharp_SGU2025.GUI.RoleItem();
            this.roleItem3 = new Student_Management_System_CSharp_SGU2025.GUI.RoleItem();
            this.roleItem2 = new Student_Management_System_CSharp_SGU2025.GUI.RoleItem();
            this.roleItem1 = new Student_Management_System_CSharp_SGU2025.GUI.RoleItem();
            this.pnlRoleContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPhanQuyen
            // 
            this.lblPhanQuyen.AutoSize = false;
            this.lblPhanQuyen.BackColor = System.Drawing.Color.Transparent;
            this.lblPhanQuyen.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhanQuyen.Location = new System.Drawing.Point(36, 15);
            this.lblPhanQuyen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lblPhanQuyen.Name = "lblPhanQuyen";
            this.lblPhanQuyen.Size = new System.Drawing.Size(233, 40);
            this.lblPhanQuyen.TabIndex = 0;
            this.lblPhanQuyen.Text = "Phân quyền hệ thống";
            // 
            // pnlRoleContainer
            // 
            this.pnlRoleContainer.AutoScroll = true;
            this.pnlRoleContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlRoleContainer.BorderColor = System.Drawing.Color.LightGray;
            this.pnlRoleContainer.BorderRadius = 6;
            this.pnlRoleContainer.BorderThickness = 1;
            this.pnlRoleContainer.Controls.Add(this.roleItem4);
            this.pnlRoleContainer.Controls.Add(this.roleItem3);
            this.pnlRoleContainer.Controls.Add(this.roleItem2);
            this.pnlRoleContainer.Controls.Add(this.roleItem1);
            this.pnlRoleContainer.FillColor = System.Drawing.Color.White;
            this.pnlRoleContainer.Location = new System.Drawing.Point(36, 66);
            this.pnlRoleContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlRoleContainer.Name = "pnlRoleContainer";
            this.pnlRoleContainer.Size = new System.Drawing.Size(824, 490);
            this.pnlRoleContainer.TabIndex = 1;
            this.pnlRoleContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // btnExit
            // 
            this.btnExit.BorderRadius = 6;
            this.btnExit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(537, 565);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(135, 37);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAddRole
            // 
            this.btnAddRole.BorderRadius = 6;
            this.btnAddRole.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddRole.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddRole.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddRole.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddRole.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRole.ForeColor = System.Drawing.Color.White;
            this.btnAddRole.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus7;
            this.btnAddRole.ImageSize = new System.Drawing.Size(15, 15);
            this.btnAddRole.Location = new System.Drawing.Point(694, 565);
            this.btnAddRole.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(166, 37);
            this.btnAddRole.TabIndex = 2;
            this.btnAddRole.Text = "Thêm phân quyền";
            this.btnAddRole.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // roleItem4
            // 
            this.roleItem4.BackColor = System.Drawing.Color.Transparent;
            this.roleItem4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roleItem4.Dock = System.Windows.Forms.DockStyle.Top;
            this.roleItem4.Location = new System.Drawing.Point(0, 231);
            this.roleItem4.Margin = new System.Windows.Forms.Padding(2);
            this.roleItem4.Name = "roleItem4";
            this.roleItem4.RoleDescription = "Role Describe";
            this.roleItem4.RoleName = "Role Name";
            this.roleItem4.Size = new System.Drawing.Size(824, 77);
            this.roleItem4.TabIndex = 3;
            // 
            // roleItem3
            // 
            this.roleItem3.BackColor = System.Drawing.Color.Transparent;
            this.roleItem3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roleItem3.Dock = System.Windows.Forms.DockStyle.Top;
            this.roleItem3.Location = new System.Drawing.Point(0, 154);
            this.roleItem3.Margin = new System.Windows.Forms.Padding(2);
            this.roleItem3.Name = "roleItem3";
            this.roleItem3.RoleDescription = "Role Describe";
            this.roleItem3.RoleName = "Role Name";
            this.roleItem3.Size = new System.Drawing.Size(824, 77);
            this.roleItem3.TabIndex = 2;
            // 
            // roleItem2
            // 
            this.roleItem2.BackColor = System.Drawing.Color.Transparent;
            this.roleItem2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roleItem2.Dock = System.Windows.Forms.DockStyle.Top;
            this.roleItem2.Location = new System.Drawing.Point(0, 77);
            this.roleItem2.Margin = new System.Windows.Forms.Padding(2);
            this.roleItem2.Name = "roleItem2";
            this.roleItem2.RoleDescription = "Role Describe";
            this.roleItem2.RoleName = "Role Name";
            this.roleItem2.Size = new System.Drawing.Size(824, 77);
            this.roleItem2.TabIndex = 1;
            // 
            // roleItem1
            // 
            this.roleItem1.BackColor = System.Drawing.Color.Transparent;
            this.roleItem1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roleItem1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roleItem1.Location = new System.Drawing.Point(0, 0);
            this.roleItem1.Margin = new System.Windows.Forms.Padding(2);
            this.roleItem1.Name = "roleItem1";
            this.roleItem1.RoleDescription = "Role Describe";
            this.roleItem1.RoleName = "Role Name";
            this.roleItem1.Size = new System.Drawing.Size(824, 77);
            this.roleItem1.TabIndex = 0;
            // 
            // frmPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 619);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddRole);
            this.Controls.Add(this.pnlRoleContainer);
            this.Controls.Add(this.lblPhanQuyen);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmPhanQuyen";
            this.Text = "frmPhanQuyen";
            this.Load += new System.EventHandler(this.frmPhanQuyen_Load);
            this.pnlRoleContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblPhanQuyen;
        private Guna.UI2.WinForms.Guna2Panel pnlRoleContainer;
        private Guna.UI2.WinForms.Guna2Button btnAddRole;
        private Guna.UI2.WinForms.Guna2Button btnExit;
        private RoleItem roleItem2;
        private RoleItem roleItem1;
        private RoleItem roleItem4;
        private RoleItem roleItem3;
    }
}