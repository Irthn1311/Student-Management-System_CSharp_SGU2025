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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnXemRole = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaRole = new Guna.UI2.WinForms.Guna2Button();
            this.lblRoleDescription = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblRoleName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.LightGray;
            this.guna2Panel1.BorderRadius = 6;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.btnXemRole);
            this.guna2Panel1.Controls.Add(this.btnXoaRole);
            this.guna2Panel1.Controls.Add(this.lblRoleDescription);
            this.guna2Panel1.Controls.Add(this.lblRoleName);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(823, 77);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // btnXemRole
            // 
            this.btnXemRole.BorderRadius = 10;
            this.btnXemRole.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXemRole.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXemRole.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXemRole.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXemRole.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemRole.ForeColor = System.Drawing.Color.White;
            this.btnXemRole.Location = new System.Drawing.Point(682, 11);
            this.btnXemRole.Margin = new System.Windows.Forms.Padding(2);
            this.btnXemRole.Name = "btnXemRole";
            this.btnXemRole.Size = new System.Drawing.Size(95, 37);
            this.btnXemRole.TabIndex = 7;
            this.btnXemRole.Text = "Xem";
            this.btnXemRole.Click += new System.EventHandler(this.btnEditRole_Click);
            // 
            // btnXoaRole
            // 
            this.btnXoaRole.BorderRadius = 10;
            this.btnXoaRole.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaRole.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaRole.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoaRole.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoaRole.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoaRole.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaRole.ForeColor = System.Drawing.Color.White;
            this.btnXoaRole.Location = new System.Drawing.Point(565, 11);
            this.btnXoaRole.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoaRole.Name = "btnXoaRole";
            this.btnXoaRole.Size = new System.Drawing.Size(90, 37);
            this.btnXoaRole.TabIndex = 6;
            this.btnXoaRole.Text = "Xoá";
            this.btnXoaRole.Click += new System.EventHandler(this.btnXoaRole_Click);
            // 
            // lblRoleDescription
            // 
            this.lblRoleDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblRoleDescription.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleDescription.ForeColor = System.Drawing.Color.DimGray;
            this.lblRoleDescription.Location = new System.Drawing.Point(21, 42);
            this.lblRoleDescription.Margin = new System.Windows.Forms.Padding(2);
            this.lblRoleDescription.Name = "lblRoleDescription";
            this.lblRoleDescription.Size = new System.Drawing.Size(85, 21);
            this.lblRoleDescription.TabIndex = 5;
            this.lblRoleDescription.Text = "Role Describe";
            // 
            // lblRoleName
            // 
            this.lblRoleName.BackColor = System.Drawing.Color.Transparent;
            this.lblRoleName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleName.Location = new System.Drawing.Point(20, 11);
            this.lblRoleName.Margin = new System.Windows.Forms.Padding(2);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(99, 27);
            this.lblRoleName.TabIndex = 4;
            this.lblRoleName.Text = "Role Name";
            // 
            // RoleItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RoleItem";
            this.Size = new System.Drawing.Size(823, 77);
            this.Load += new System.EventHandler(this.RoleItem_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnXemRole;
        private Guna.UI2.WinForms.Guna2Button btnXoaRole;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRoleDescription;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRoleName;
    }
}
