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
            this.roleItem4 = new Student_Management_System_CSharp_SGU2025.GUI.RoleItem();
            this.roleItem3 = new Student_Management_System_CSharp_SGU2025.GUI.RoleItem();
            this.roleItem2 = new Student_Management_System_CSharp_SGU2025.GUI.RoleItem();
            this.roleItem1 = new Student_Management_System_CSharp_SGU2025.GUI.RoleItem();
            this.btnAddRole = new Guna.UI2.WinForms.Guna2Button();
            this.btnExit = new Guna.UI2.WinForms.Guna2Button();
            this.pnlRoleContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPhanQuyen
            // 
            this.lblPhanQuyen.AutoSize = false;
            this.lblPhanQuyen.BackColor = System.Drawing.Color.Transparent;
            this.lblPhanQuyen.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhanQuyen.Location = new System.Drawing.Point(48, 42);
            this.lblPhanQuyen.Name = "lblPhanQuyen";
            this.lblPhanQuyen.Size = new System.Drawing.Size(311, 49);
            this.lblPhanQuyen.TabIndex = 0;
            this.lblPhanQuyen.Text = "Phân quyền hệ thống";
            // 
            // pnlRoleContainer
            // 
            this.pnlRoleContainer.BackColor = System.Drawing.Color.White;
            this.pnlRoleContainer.Controls.Add(this.roleItem4);
            this.pnlRoleContainer.Controls.Add(this.roleItem3);
            this.pnlRoleContainer.Controls.Add(this.roleItem2);
            this.pnlRoleContainer.Controls.Add(this.roleItem1);
            this.pnlRoleContainer.Location = new System.Drawing.Point(48, 121);
            this.pnlRoleContainer.Name = "pnlRoleContainer";
            this.pnlRoleContainer.Size = new System.Drawing.Size(1099, 616);
            this.pnlRoleContainer.TabIndex = 1;
            this.pnlRoleContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // roleItem4
            // 
            this.roleItem4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roleItem4.Location = new System.Drawing.Point(3, 438);
            this.roleItem4.Name = "roleItem4";
            this.roleItem4.RoleDescription = "Role Describe";
            this.roleItem4.RoleName = "Role Name";
            this.roleItem4.Size = new System.Drawing.Size(1097, 148);
            this.roleItem4.TabIndex = 3;
            this.roleItem4.Load += new System.EventHandler(this.roleItem4_Load_1);
            // 
            // roleItem3
            // 
            this.roleItem3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roleItem3.Location = new System.Drawing.Point(2, 293);
            this.roleItem3.Name = "roleItem3";
            this.roleItem3.RoleDescription = "Role Describe";
            this.roleItem3.RoleName = "Role Name";
            this.roleItem3.Size = new System.Drawing.Size(1097, 148);
            this.roleItem3.TabIndex = 2;
            this.roleItem3.Load += new System.EventHandler(this.roleItem3_Load_1);
            // 
            // roleItem2
            // 
            this.roleItem2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roleItem2.Location = new System.Drawing.Point(3, 148);
            this.roleItem2.Name = "roleItem2";
            this.roleItem2.RoleDescription = "Role Describe";
            this.roleItem2.RoleName = "Role Name";
            this.roleItem2.Size = new System.Drawing.Size(1097, 148);
            this.roleItem2.TabIndex = 1;
            this.roleItem2.Load += new System.EventHandler(this.roleItem2_Load_1);
            // 
            // roleItem1
            // 
            this.roleItem1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roleItem1.Location = new System.Drawing.Point(3, 3);
            this.roleItem1.Name = "roleItem1";
            this.roleItem1.RoleDescription = "Role Describe";
            this.roleItem1.RoleName = "Role Name";
            this.roleItem1.Size = new System.Drawing.Size(1097, 148);
            this.roleItem1.TabIndex = 0;
            this.roleItem1.Load += new System.EventHandler(this.roleItem1_Load_1);
            // 
            // btnAddRole
            // 
            this.btnAddRole.BorderRadius = 15;
            this.btnAddRole.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddRole.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddRole.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddRole.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddRole.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(186)))), ((int)(((byte)(117)))));
            this.btnAddRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRole.ForeColor = System.Drawing.Color.White;
            this.btnAddRole.Location = new System.Drawing.Point(926, 42);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(221, 45);
            this.btnAddRole.TabIndex = 2;
            this.btnAddRole.Text = "➕   Thêm phân quyền";
            this.btnAddRole.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // btnExit
            // 
            this.btnExit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(500, 743);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(180, 45);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1230, 797);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddRole);
            this.Controls.Add(this.pnlRoleContainer);
            this.Controls.Add(this.lblPhanQuyen);
            this.Name = "frmPhanQuyen";
            this.Text = "frmPhanQuyen";
            this.Load += new System.EventHandler(this.frmPhanQuyen_Load);
            this.pnlRoleContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblPhanQuyen;
        private Guna.UI2.WinForms.Guna2Panel pnlRoleContainer;
        private RoleItem roleItem4;
        private RoleItem roleItem3;
        private RoleItem roleItem2;
        private RoleItem roleItem1;
        private Guna.UI2.WinForms.Guna2Button btnAddRole;
        private Guna.UI2.WinForms.Guna2Button btnExit;
    }
}