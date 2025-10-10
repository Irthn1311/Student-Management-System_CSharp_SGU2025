namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class HocSinh
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.tableHocSinh = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MãHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTimKiemHocSinh = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThemHocSinh = new Guna.UI2.WinForms.Guna2Button();
            this.headerQuanLiHocSinh = new Student_Management_System_CSharp_SGU2025.GUI.HeaderQuanLiHocSinh();
            this.statCardDangHoc = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardNu = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardNam = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardTongHocSinh = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            ((System.ComponentModel.ISupportInitialize)(this.tableHocSinh)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLop
            // 
            this.cbLop.BackColor = System.Drawing.Color.Transparent;
            this.cbLop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLop.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLop.ItemHeight = 30;
            this.cbLop.Items.AddRange(new object[] {
            "Tất cả lớp",
            "Lớp 6A1",
            "Lớp 7A2"});
            this.cbLop.Location = new System.Drawing.Point(998, 30);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(168, 36);
            this.cbLop.StartIndex = 0;
            this.cbLop.TabIndex = 11;
            // 
            // tableHocSinh
            // 
            this.tableHocSinh.AllowUserToAddRows = false;
            this.tableHocSinh.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tableHocSinh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableHocSinh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tableHocSinh.ColumnHeadersHeight = 19;
            this.tableHocSinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableHocSinh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MãHS,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column8,
            this.Column9});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tableHocSinh.DefaultCellStyle = dataGridViewCellStyle3;
            this.tableHocSinh.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableHocSinh.Location = new System.Drawing.Point(23, 264);
            this.tableHocSinh.Name = "tableHocSinh";
            this.tableHocSinh.RowHeadersVisible = false;
            this.tableHocSinh.RowHeadersWidth = 51;
            this.tableHocSinh.RowTemplate.Height = 24;
            this.tableHocSinh.Size = new System.Drawing.Size(1143, 429);
            this.tableHocSinh.TabIndex = 12;
            this.tableHocSinh.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tableHocSinh.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tableHocSinh.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tableHocSinh.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tableHocSinh.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tableHocSinh.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tableHocSinh.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableHocSinh.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tableHocSinh.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tableHocSinh.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableHocSinh.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tableHocSinh.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableHocSinh.ThemeStyle.HeaderStyle.Height = 19;
            this.tableHocSinh.ThemeStyle.ReadOnly = false;
            this.tableHocSinh.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tableHocSinh.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tableHocSinh.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableHocSinh.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableHocSinh.ThemeStyle.RowsStyle.Height = 24;
            this.tableHocSinh.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableHocSinh.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableHocSinh.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tableHocSinh_CellContentClick);
            // 
            // MãHS
            // 
            this.MãHS.HeaderText = "Mã HS";
            this.MãHS.MinimumWidth = 6;
            this.MãHS.Name = "MãHS";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Họ và Tên";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Ngày Sinh";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Giới Tính";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Lớp";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Trạng Thái";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Thao Tác";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            // 
            // txtTimKiemHocSinh
            // 
            this.txtTimKiemHocSinh.BorderRadius = 7;
            this.txtTimKiemHocSinh.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiemHocSinh.DefaultText = "";
            this.txtTimKiemHocSinh.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiemHocSinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiemHocSinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiemHocSinh.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiemHocSinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiemHocSinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiemHocSinh.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiemHocSinh.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.search;
            this.txtTimKiemHocSinh.IconLeftOffset = new System.Drawing.Point(7, 0);
            this.txtTimKiemHocSinh.Location = new System.Drawing.Point(224, 37);
            this.txtTimKiemHocSinh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiemHocSinh.Name = "txtTimKiemHocSinh";
            this.txtTimKiemHocSinh.PlaceholderText = "Tìm kiếm học sinh ...";
            this.txtTimKiemHocSinh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTimKiemHocSinh.SelectedText = "";
            this.txtTimKiemHocSinh.Size = new System.Drawing.Size(306, 35);
            this.txtTimKiemHocSinh.TabIndex = 6;
            // 
            // btnThemHocSinh
            // 
            this.btnThemHocSinh.BorderRadius = 7;
            this.btnThemHocSinh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemHocSinh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemHocSinh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemHocSinh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemHocSinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemHocSinh.ForeColor = System.Drawing.Color.White;
            this.btnThemHocSinh.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus;
            this.btnThemHocSinh.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThemHocSinh.Location = new System.Drawing.Point(23, 30);
            this.btnThemHocSinh.Name = "btnThemHocSinh";
            this.btnThemHocSinh.Size = new System.Drawing.Size(168, 45);
            this.btnThemHocSinh.TabIndex = 1;
            this.btnThemHocSinh.Text = "Thêm học sinh";
            this.btnThemHocSinh.Click += new System.EventHandler(this.btnThemHocSinh_Click);
            // 
            // headerQuanLiHocSinh
            // 
            this.headerQuanLiHocSinh.BackColor = System.Drawing.Color.White;
            this.headerQuanLiHocSinh.Location = new System.Drawing.Point(0, 0);
            this.headerQuanLiHocSinh.Name = "headerQuanLiHocSinh";
            this.headerQuanLiHocSinh.Size = new System.Drawing.Size(1184, 0);
            this.headerQuanLiHocSinh.TabIndex = 13;
            this.headerQuanLiHocSinh.Visible = false;
            this.headerQuanLiHocSinh.Load += new System.EventHandler(this.headerQuanLiHocSinh_Load);
            // 
            // statCardDangHoc
            // 
            this.statCardDangHoc.BackColor = System.Drawing.Color.White;
            this.statCardDangHoc.Location = new System.Drawing.Point(902, 92);
            this.statCardDangHoc.Name = "statCardDangHoc";
            this.statCardDangHoc.Size = new System.Drawing.Size(264, 142);
            this.statCardDangHoc.TabIndex = 10;
            // 
            // statCardNu
            // 
            this.statCardNu.BackColor = System.Drawing.Color.White;
            this.statCardNu.Location = new System.Drawing.Point(614, 92);
            this.statCardNu.Name = "statCardNu";
            this.statCardNu.Size = new System.Drawing.Size(264, 142);
            this.statCardNu.TabIndex = 9;
            // 
            // statCardNam
            // 
            this.statCardNam.BackColor = System.Drawing.Color.White;
            this.statCardNam.Location = new System.Drawing.Point(317, 92);
            this.statCardNam.Name = "statCardNam";
            this.statCardNam.Size = new System.Drawing.Size(264, 142);
            this.statCardNam.TabIndex = 8;
            this.statCardNam.Load += new System.EventHandler(this.statCardNam_Load);
            // 
            // statCardTongHocSinh
            // 
            this.statCardTongHocSinh.BackColor = System.Drawing.Color.White;
            this.statCardTongHocSinh.Location = new System.Drawing.Point(23, 92);
            this.statCardTongHocSinh.Name = "statCardTongHocSinh";
            this.statCardTongHocSinh.Size = new System.Drawing.Size(264, 142);
            this.statCardTongHocSinh.TabIndex = 7;
            this.statCardTongHocSinh.Load += new System.EventHandler(this.statCardTongHocSinh_Load);
            // 
            // HocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.headerQuanLiHocSinh);
            this.Controls.Add(this.tableHocSinh);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.statCardDangHoc);
            this.Controls.Add(this.statCardNu);
            this.Controls.Add(this.statCardNam);
            this.Controls.Add(this.statCardTongHocSinh);
            this.Controls.Add(this.txtTimKiemHocSinh);
            this.Controls.Add(this.btnThemHocSinh);
            this.Name = "HocSinh";
            this.Size = new System.Drawing.Size(1184, 820);
            this.Load += new System.EventHandler(this.HocSinh_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.tableHocSinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnThemHocSinh;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiemHocSinh;
        private StatCardQuanLiHocSinh statCardTongHocSinh;
        private StatCardQuanLiHocSinh statCardNam;
        private StatCardQuanLiHocSinh statCardNu;
        private StatCardQuanLiHocSinh statCardDangHoc;
        private Guna.UI2.WinForms.Guna2ComboBox cbLop;
        private Guna.UI2.WinForms.Guna2DataGridView tableHocSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn MãHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private HeaderQuanLiHocSinh headerQuanLiHocSinh;
    }
}
