namespace Student_Management_System_CSharp_SGU2025.GUI.GiaoVien
{
    partial class GiaoVien
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
            this.btnThemGiaoVien = new Guna.UI2.WinForms.Guna2Button();
            this.txtTimKiemGiaoVien = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbBoMon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.statCardTongGiaoVien = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardGiaoVienNam = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardGiaoVienNu = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.statCardBoMon = new Student_Management_System_CSharp_SGU2025.GUI.StatCardQuanLiHocSinh();
            this.tableGiaoVien = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MãHS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tableGiaoVien)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThemGiaoVien
            // 
            this.btnThemGiaoVien.BorderRadius = 7;
            this.btnThemGiaoVien.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemGiaoVien.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemGiaoVien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemGiaoVien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemGiaoVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemGiaoVien.ForeColor = System.Drawing.Color.White;
            this.btnThemGiaoVien.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus;
            this.btnThemGiaoVien.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThemGiaoVien.Location = new System.Drawing.Point(18, 22);
            this.btnThemGiaoVien.Margin = new System.Windows.Forms.Padding(2);
            this.btnThemGiaoVien.Name = "btnThemGiaoVien";
            this.btnThemGiaoVien.Size = new System.Drawing.Size(168, 51);
            this.btnThemGiaoVien.TabIndex = 2;
            this.btnThemGiaoVien.Text = "Thêm giáo viên";
            // 
            // txtTimKiemGiaoVien
            // 
            this.txtTimKiemGiaoVien.BorderRadius = 7;
            this.txtTimKiemGiaoVien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiemGiaoVien.DefaultText = "";
            this.txtTimKiemGiaoVien.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiemGiaoVien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiemGiaoVien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiemGiaoVien.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiemGiaoVien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiemGiaoVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiemGiaoVien.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiemGiaoVien.IconLeft = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.search;
            this.txtTimKiemGiaoVien.IconLeftOffset = new System.Drawing.Point(7, 0);
            this.txtTimKiemGiaoVien.Location = new System.Drawing.Point(218, 22);
            this.txtTimKiemGiaoVien.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTimKiemGiaoVien.Name = "txtTimKiemGiaoVien";
            this.txtTimKiemGiaoVien.PlaceholderText = "Tìm kiếm giáo viên ...";
            this.txtTimKiemGiaoVien.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTimKiemGiaoVien.SelectedText = "";
            this.txtTimKiemGiaoVien.Size = new System.Drawing.Size(323, 51);
            this.txtTimKiemGiaoVien.TabIndex = 7;
            // 
            // cbBoMon
            // 
            this.cbBoMon.BackColor = System.Drawing.Color.Transparent;
            this.cbBoMon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbBoMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoMon.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbBoMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbBoMon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBoMon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbBoMon.ItemHeight = 30;
            this.cbBoMon.Items.AddRange(new object[] {
            "Tất cả bộ môn",
            "Toán Học",
            "Ngữ Văn",
            "Tiếng Anh",
            "Vật Lí"});
            this.cbBoMon.Location = new System.Drawing.Point(979, 32);
            this.cbBoMon.Margin = new System.Windows.Forms.Padding(2);
            this.cbBoMon.Name = "cbBoMon";
            this.cbBoMon.Size = new System.Drawing.Size(158, 36);
            this.cbBoMon.StartIndex = 0;
            this.cbBoMon.TabIndex = 12;
            // 
            // statCardTongGiaoVien
            // 
            this.statCardTongGiaoVien.BackColor = System.Drawing.Color.White;
            this.statCardTongGiaoVien.Location = new System.Drawing.Point(18, 91);
            this.statCardTongGiaoVien.Margin = new System.Windows.Forms.Padding(2);
            this.statCardTongGiaoVien.Name = "statCardTongGiaoVien";
            this.statCardTongGiaoVien.Size = new System.Drawing.Size(262, 114);
            this.statCardTongGiaoVien.TabIndex = 13;
            // 
            // statCardGiaoVienNam
            // 
            this.statCardGiaoVienNam.BackColor = System.Drawing.Color.White;
            this.statCardGiaoVienNam.Location = new System.Drawing.Point(306, 91);
            this.statCardGiaoVienNam.Margin = new System.Windows.Forms.Padding(2);
            this.statCardGiaoVienNam.Name = "statCardGiaoVienNam";
            this.statCardGiaoVienNam.Size = new System.Drawing.Size(262, 114);
            this.statCardGiaoVienNam.TabIndex = 13;
            // 
            // statCardGiaoVienNu
            // 
            this.statCardGiaoVienNu.BackColor = System.Drawing.Color.White;
            this.statCardGiaoVienNu.Location = new System.Drawing.Point(597, 91);
            this.statCardGiaoVienNu.Margin = new System.Windows.Forms.Padding(2);
            this.statCardGiaoVienNu.Name = "statCardGiaoVienNu";
            this.statCardGiaoVienNu.Size = new System.Drawing.Size(262, 114);
            this.statCardGiaoVienNu.TabIndex = 13;
            // 
            // statCardBoMon
            // 
            this.statCardBoMon.BackColor = System.Drawing.Color.White;
            this.statCardBoMon.Location = new System.Drawing.Point(890, 91);
            this.statCardBoMon.Margin = new System.Windows.Forms.Padding(2);
            this.statCardBoMon.Name = "statCardBoMon";
            this.statCardBoMon.Size = new System.Drawing.Size(262, 114);
            this.statCardBoMon.TabIndex = 13;
            // 
            // tableGiaoVien
            // 
            this.tableGiaoVien.AllowUserToAddRows = false;
            this.tableGiaoVien.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.tableGiaoVien.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableGiaoVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tableGiaoVien.ColumnHeadersHeight = 19;
            this.tableGiaoVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableGiaoVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MãHS,
            this.Column1,
            this.Column3,
            this.Column2,
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
            this.tableGiaoVien.DefaultCellStyle = dataGridViewCellStyle3;
            this.tableGiaoVien.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableGiaoVien.Location = new System.Drawing.Point(18, 231);
            this.tableGiaoVien.Margin = new System.Windows.Forms.Padding(2);
            this.tableGiaoVien.Name = "tableGiaoVien";
            this.tableGiaoVien.RowHeadersVisible = false;
            this.tableGiaoVien.RowHeadersWidth = 51;
            this.tableGiaoVien.RowTemplate.Height = 24;
            this.tableGiaoVien.Size = new System.Drawing.Size(1132, 481);
            this.tableGiaoVien.TabIndex = 14;
            this.tableGiaoVien.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.tableGiaoVien.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.tableGiaoVien.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.tableGiaoVien.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.tableGiaoVien.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.tableGiaoVien.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.tableGiaoVien.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableGiaoVien.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.tableGiaoVien.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tableGiaoVien.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableGiaoVien.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.tableGiaoVien.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.tableGiaoVien.ThemeStyle.HeaderStyle.Height = 19;
            this.tableGiaoVien.ThemeStyle.ReadOnly = false;
            this.tableGiaoVien.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.tableGiaoVien.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.tableGiaoVien.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableGiaoVien.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.tableGiaoVien.ThemeStyle.RowsStyle.Height = 24;
            this.tableGiaoVien.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.tableGiaoVien.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // MãHS
            // 
            this.MãHS.HeaderText = "Mã GV";
            this.MãHS.MinimumWidth = 6;
            this.MãHS.Name = "MãHS";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Họ và Tên";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Giới Tính";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Chuyên Môn";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Sdt";
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
            // GiaoVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableGiaoVien);
            this.Controls.Add(this.statCardBoMon);
            this.Controls.Add(this.statCardGiaoVienNu);
            this.Controls.Add(this.statCardGiaoVienNam);
            this.Controls.Add(this.statCardTongGiaoVien);
            this.Controls.Add(this.cbBoMon);
            this.Controls.Add(this.txtTimKiemGiaoVien);
            this.Controls.Add(this.btnThemGiaoVien);
            this.Name = "GiaoVien";
            this.Size = new System.Drawing.Size(1168, 768);
            this.Load += new System.EventHandler(this.GiaoVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tableGiaoVien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnThemGiaoVien;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiemGiaoVien;
        private Guna.UI2.WinForms.Guna2ComboBox cbBoMon;
        private StatCardQuanLiHocSinh statCardTongGiaoVien;
        private StatCardQuanLiHocSinh statCardGiaoVienNam;
        private StatCardQuanLiHocSinh statCardGiaoVienNu;
        private StatCardQuanLiHocSinh statCardBoMon;
        private Guna.UI2.WinForms.Guna2DataGridView tableGiaoVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn MãHS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    }
}
