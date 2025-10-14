namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class RoleItem
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
            this.lblRoleName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblRoleDescription = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnXoaRole = new Guna.UI2.WinForms.Guna2Button();
            this.btnEditRole = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // lblRoleName
            // 
            this.lblRoleName.AutoSize = false;
            this.lblRoleName.BackColor = System.Drawing.Color.Transparent;
            this.lblRoleName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleName.Location = new System.Drawing.Point(32, 24);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(328, 45);
            this.lblRoleName.TabIndex = 0;
            this.lblRoleName.Text = "Role Name";
            this.lblRoleName.Click += new System.EventHandler(this.txtRoleName_Click);
            // 
            // lblRoleDescription
            // 
            this.lblRoleDescription.AutoSize = false;
            this.lblRoleDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblRoleDescription.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleDescription.Location = new System.Drawing.Point(41, 100);
            this.lblRoleDescription.Name = "lblRoleDescription";
            this.lblRoleDescription.Size = new System.Drawing.Size(1036, 45);
            this.lblRoleDescription.TabIndex = 1;
            this.lblRoleDescription.Text = "Role Describe";
            this.lblRoleDescription.Click += new System.EventHandler(this.txtRole_Click);
            // 
            // btnXoaRole
            // 
            this.btnXoaRole.BorderRadius = 15;
            this.btnXoaRole.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaRole.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaRole.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoaRole.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoaRole.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnXoaRole.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaRole.ForeColor = System.Drawing.Color.Red;
            this.btnXoaRole.Location = new System.Drawing.Point(761, 46);
            this.btnXoaRole.Name = "btnXoaRole";
            this.btnXoaRole.Size = new System.Drawing.Size(141, 45);
            this.btnXoaRole.TabIndex = 2;
            this.btnXoaRole.Text = "Xoá";
            this.btnXoaRole.Click += new System.EventHandler(this.btnXoaRole_Click);
            // 
            // btnEditRole
            // 
            this.btnEditRole.BorderRadius = 15;
            this.btnEditRole.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEditRole.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEditRole.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditRole.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEditRole.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEditRole.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditRole.ForeColor = System.Drawing.Color.Black;
            this.btnEditRole.Location = new System.Drawing.Point(923, 46);
            this.btnEditRole.Name = "btnEditRole";
            this.btnEditRole.Size = new System.Drawing.Size(154, 45);
            this.btnEditRole.TabIndex = 3;
            this.btnEditRole.Text = "Chỉnh sửa";
            this.btnEditRole.Click += new System.EventHandler(this.btnEditRole_Click);
            // 
            // RoleItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnEditRole);
            this.Controls.Add(this.btnXoaRole);
            this.Controls.Add(this.lblRoleDescription);
            this.Controls.Add(this.lblRoleName);
            this.Name = "RoleItem";
            this.Size = new System.Drawing.Size(1097, 148);
            this.Load += new System.EventHandler(this.RoleItem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblRoleName;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRoleDescription;
        private Guna.UI2.WinForms.Guna2Button btnXoaRole;
        private Guna.UI2.WinForms.Guna2Button btnEditRole;
    }
}
