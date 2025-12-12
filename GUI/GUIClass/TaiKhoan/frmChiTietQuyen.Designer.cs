namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class frmChiTietQuyen
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
            this.lblTenVaiTro = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tableChucNang = new Guna.UI2.WinForms.Guna2DataGridView();
            this.chucNang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hanhDong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.lblTongSo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblThongBao = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.tableChucNang)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTenVaiTro
            // 
            this.lblTenVaiTro.AutoSize = false;
            this.lblTenVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.lblTenVaiTro.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenVaiTro.Location = new System.Drawing.Point(37, 30);
            this.lblTenVaiTro.Margin = new System.Windows.Forms.Padding(2);
            this.lblTenVaiTro.Name = "lblTenVaiTro";
            this.lblTenVaiTro.Size = new System.Drawing.Size(500, 32);
            this.lblTenVaiTro.TabIndex = 0;
            this.lblTenVaiTro.Text = "Chi tiết quyền";
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
            this.tableChucNang.Location = new System.Drawing.Point(37, 119);
            this.tableChucNang.Name = "tableChucNang";
            this.tableChucNang.RowHeadersVisible = false;
            this.tableChucNang.Size = new System.Drawing.Size(804, 744);
            this.tableChucNang.TabIndex = 1;
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
            // btnDong
            // 
            this.btnDong.BorderRadius = 6;
            this.btnDong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(706, 31);
            this.btnDong.Margin = new System.Windows.Forms.Padding(2);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(135, 37);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // lblTongSo
            // 
            this.lblTongSo.AutoSize = false;
            this.lblTongSo.BackColor = System.Drawing.Color.Transparent;
            this.lblTongSo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSo.Location = new System.Drawing.Point(37, 80);
            this.lblTongSo.Margin = new System.Windows.Forms.Padding(2);
            this.lblTongSo.Name = "lblTongSo";
            this.lblTongSo.Size = new System.Drawing.Size(300, 22);
            this.lblTongSo.TabIndex = 3;
            this.lblTongSo.Text = "Tổng số chức năng: 0";
            this.lblTongSo.Click += new System.EventHandler(this.lblTongSo_Click);
            // 
            // lblThongBao
            // 
            this.lblThongBao.AutoSize = false;
            this.lblThongBao.BackColor = System.Drawing.Color.Transparent;
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(37, 250);
            this.lblThongBao.Margin = new System.Windows.Forms.Padding(2);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(804, 22);
            this.lblThongBao.TabIndex = 4;
            this.lblThongBao.Text = "Vai trò này chưa có quyền nào!";
            this.lblThongBao.Visible = false;
            // 
            // frmChiTietQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(866, 887);
            this.ControlBox = false;
            this.Controls.Add(this.lblThongBao);
            this.Controls.Add(this.lblTongSo);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.tableChucNang);
            this.Controls.Add(this.lblTenVaiTro);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmChiTietQuyen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết quyền";
            this.Load += new System.EventHandler(this.frmChiTietQuyen_Load);
            this.Shown += new System.EventHandler(this.frmChiTietQuyen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.tableChucNang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTenVaiTro;
        private Guna.UI2.WinForms.Guna2DataGridView tableChucNang;
        private System.Windows.Forms.DataGridViewTextBoxColumn chucNang;
        private System.Windows.Forms.DataGridViewTextBoxColumn hanhDong;
        private Guna.UI2.WinForms.Guna2Button btnDong;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTongSo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThongBao;
    }
}

