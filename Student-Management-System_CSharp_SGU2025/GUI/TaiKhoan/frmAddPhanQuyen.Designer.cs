namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class frmAddPhanQuyen
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
            this.btnAddQuyen = new Guna.UI2.WinForms.Guna2Button();
            this.tableChucNang = new Guna.UI2.WinForms.Guna2DataGridView();
            this.chucNang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hanhDong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
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
            this.lblPhanQuyen.Text = "Thêm phân quyền";
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
            this.txtTenPhanQuyen.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
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
            // btnAddQuyen
            // 
            this.btnAddQuyen.BorderRadius = 6;
            this.btnAddQuyen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddQuyen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddQuyen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddQuyen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddQuyen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddQuyen.ForeColor = System.Drawing.Color.White;
            this.btnAddQuyen.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus8;
            this.btnAddQuyen.ImageSize = new System.Drawing.Size(15, 15);
            this.btnAddQuyen.Location = new System.Drawing.Point(674, 31);
            this.btnAddQuyen.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddQuyen.Name = "btnAddQuyen";
            this.btnAddQuyen.Size = new System.Drawing.Size(135, 37);
            this.btnAddQuyen.TabIndex = 18;
            this.btnAddQuyen.Text = "Thêm";
            this.btnAddQuyen.Click += new System.EventHandler(this.btnAddQuyen_Click);
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
            this.tableChucNang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableChucNang_CellContentClick);
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
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 6;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.ForestGreen;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources._3que;
            this.guna2Button1.ImageSize = new System.Drawing.Size(15, 15);
            this.guna2Button1.Location = new System.Drawing.Point(593, 101);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(135, 37);
            this.guna2Button1.TabIndex = 21;
            this.guna2Button1.Text = "Toàn quyền";
            // 
            // frmAddPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(866, 594);
            this.ControlBox = false;
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.tableChucNang);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddQuyen);
            this.Controls.Add(this.txtTenPhanQuyen);
            this.Controls.Add(this.nameRole);
            this.Controls.Add(this.lblPhanQuyen);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmAddPhanQuyen";
            this.Text = "frmAddPhanQuyen";
            this.Load += new System.EventHandler(this.frmAddPhanQuyen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableChucNang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblPhanQuyen;
        private Guna.UI2.WinForms.Guna2HtmlLabel nameRole;
        private Guna.UI2.WinForms.Guna2TextBox txtTenPhanQuyen;
        private Guna.UI2.WinForms.Guna2Button btnAddQuyen;
        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2DataGridView tableChucNang;
        private System.Windows.Forms.DataGridViewTextBoxColumn chucNang;
        private System.Windows.Forms.DataGridViewTextBoxColumn hanhDong;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}