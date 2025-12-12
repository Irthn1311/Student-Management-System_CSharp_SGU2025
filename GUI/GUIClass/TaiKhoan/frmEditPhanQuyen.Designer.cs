namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class frmEditPhanQuyen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPhanQuyen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.nameRole = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTenPhanQuyen = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnExit = new Guna.UI2.WinForms.Guna2Button();
            this.btnUpdateQuyen = new Guna.UI2.WinForms.Guna2Button();
            this.tableChucNang = new Guna.UI2.WinForms.Guna2DataGridView();
            this.chucNang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hanhDong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnToanQuyen = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.tableChucNang)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPhanQuyen
            // 
            this.lblPhanQuyen.AutoSize = false;
            this.lblPhanQuyen.BackColor = System.Drawing.Color.Transparent;
            this.lblPhanQuyen.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhanQuyen.Location = new System.Drawing.Point(37, 30);
            this.lblPhanQuyen.Margin = new System.Windows.Forms.Padding(2);
            this.lblPhanQuyen.Name = "lblPhanQuyen";
            this.lblPhanQuyen.Size = new System.Drawing.Size(224, 32);
            this.lblPhanQuyen.TabIndex = 0;
            this.lblPhanQuyen.Text = "Sửa phân quyền";
            // 
            // nameRole
            // 
            this.nameRole.AutoSize = false;
            this.nameRole.BackColor = System.Drawing.Color.Transparent;
            this.nameRole.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameRole.Location = new System.Drawing.Point(37, 90);
            this.nameRole.Margin = new System.Windows.Forms.Padding(2);
            this.nameRole.Name = "nameRole";
            this.nameRole.Size = new System.Drawing.Size(121, 22);
            this.nameRole.TabIndex = 1;
            this.nameRole.Text = "Tên phân quyền :";
            // 
            // txtTenPhanQuyen
            // 
            this.txtTenPhanQuyen.BorderRadius = 5;
            this.txtTenPhanQuyen.BorderThickness = 2;
            this.txtTenPhanQuyen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenPhanQuyen.DefaultText = "";
            this.txtTenPhanQuyen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenPhanQuyen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenPhanQuyen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenPhanQuyen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenPhanQuyen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenPhanQuyen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenPhanQuyen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenPhanQuyen.Location = new System.Drawing.Point(190, 90);
            this.txtTenPhanQuyen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTenPhanQuyen.Name = "txtTenPhanQuyen";
            this.txtTenPhanQuyen.PlaceholderText = "";
            this.txtTenPhanQuyen.SelectedText = "";
            this.txtTenPhanQuyen.Size = new System.Drawing.Size(290, 31);
            this.txtTenPhanQuyen.TabIndex = 2;
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
            this.btnExit.Location = new System.Drawing.Point(519, 31);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(135, 37);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Đóng";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdateQuyen
            // 
            this.btnUpdateQuyen.BorderRadius = 6;
            this.btnUpdateQuyen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnUpdateQuyen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnUpdateQuyen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnUpdateQuyen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnUpdateQuyen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateQuyen.ForeColor = System.Drawing.Color.White;
            this.btnUpdateQuyen.Image = global::Student_Management_System_CSharp_SGU2025.GUI.Properties.Resources.plus;
            this.btnUpdateQuyen.ImageSize = new System.Drawing.Size(15, 15);
            this.btnUpdateQuyen.Location = new System.Drawing.Point(674, 31);
            this.btnUpdateQuyen.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateQuyen.Name = "btnUpdateQuyen";
            this.btnUpdateQuyen.Size = new System.Drawing.Size(135, 37);
            this.btnUpdateQuyen.TabIndex = 18;
            this.btnUpdateQuyen.Text = "Cập nhật";
            this.btnUpdateQuyen.Click += new System.EventHandler(this.btnUpdateQuyen_Click);
            // 
            // tableChucNang
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tableChucNang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableChucNang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tableChucNang.ColumnHeadersHeight = 15;
            this.tableChucNang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableChucNang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chucNang,
            this.hanhDong});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableChucNang.DefaultCellStyle = dataGridViewCellStyle3;
            this.tableChucNang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableChucNang.Location = new System.Drawing.Point(37, 152);
            this.tableChucNang.Name = "tableChucNang";
            this.tableChucNang.RowHeadersVisible = false;
            this.tableChucNang.Size = new System.Drawing.Size(804, 323);
            this.tableChucNang.TabIndex = 20;
            this.tableChucNang.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tableChucNang.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tableChucNang.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tableChucNang.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tableChucNang.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tableChucNang.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tableChucNang.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableChucNang.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tableChucNang.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tableChucNang.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableChucNang.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tableChucNang.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableChucNang.ThemeStyle.HeaderStyle.Height = 15;
            this.tableChucNang.ThemeStyle.ReadOnly = false;
            this.tableChucNang.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tableChucNang.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tableChucNang.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableChucNang.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableChucNang.ThemeStyle.RowsStyle.Height = 22;
            this.tableChucNang.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableChucNang.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // chucNang
            // 
            this.chucNang.HeaderText = "Chức năng";
            this.chucNang.Name = "chucNang";
            // 
            // hanhDong
            // 
            this.hanhDong.HeaderText = "Hành động";
            this.hanhDong.Name = "hanhDong";
            // 
            // btnToanQuyen
            // 
            this.btnToanQuyen.BorderRadius = 6;
            this.btnToanQuyen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnToanQuyen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnToanQuyen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnToanQuyen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnToanQuyen.FillColor = System.Drawing.Color.ForestGreen;
            this.btnToanQuyen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToanQuyen.ForeColor = System.Drawing.Color.White;
            this.btnToanQuyen.Image = global::Student_Management_System_CSharp_SGU2025.GUI.Properties.Resources._3que;
            this.btnToanQuyen.ImageSize = new System.Drawing.Size(15, 15);
            this.btnToanQuyen.Location = new System.Drawing.Point(593, 101);
            this.btnToanQuyen.Margin = new System.Windows.Forms.Padding(2);
            this.btnToanQuyen.Name = "btnToanQuyen";
            this.btnToanQuyen.Size = new System.Drawing.Size(135, 37);
            this.btnToanQuyen.TabIndex = 21;
            this.btnToanQuyen.Text = "Toàn quyền";
            this.btnToanQuyen.Click += new System.EventHandler(this.btnToanQuyen_Click);
            // 
            // frmEditPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(866, 594);
            this.ControlBox = false;
            this.Controls.Add(this.btnToanQuyen);
            this.Controls.Add(this.tableChucNang);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdateQuyen);
            this.Controls.Add(this.txtTenPhanQuyen);
            this.Controls.Add(this.nameRole);
            this.Controls.Add(this.lblPhanQuyen);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmEditPhanQuyen";
            this.Text = "Sửa phân quyền";
            this.Load += new System.EventHandler(this.frmEditPhanQuyen_Load);
            this.Shown += new System.EventHandler(this.frmEditPhanQuyen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.tableChucNang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblPhanQuyen;
        private Guna.UI2.WinForms.Guna2HtmlLabel nameRole;
        private Guna.UI2.WinForms.Guna2TextBox txtTenPhanQuyen;
        private Guna.UI2.WinForms.Guna2Button btnUpdateQuyen;
        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2DataGridView tableChucNang;
        private System.Windows.Forms.DataGridViewTextBoxColumn chucNang;
        private System.Windows.Forms.DataGridViewTextBoxColumn hanhDong;
        private Guna.UI2.WinForms.Guna2Button btnToanQuyen;
    }
}

