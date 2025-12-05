using Student_Management_System_CSharp_SGU2025.GUI.HocSinh;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class XemChiTietHocSinh
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblLopHienTai;
        private System.Windows.Forms.Label lblGVCNLop;
        private System.Windows.Forms.Label lblSDTGVCN;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPhuHuynh;
        private Guna.UI2.WinForms.Guna2Button btnDong;
        private System.Windows.Forms.Panel panelLop;
        private System.Windows.Forms.Panel panelPhuHuynh;
        private System.Windows.Forms.Label lblTieuDe;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.panelLop = new System.Windows.Forms.Panel();
            this.lblLopHienTai = new System.Windows.Forms.Label();
            this.lblGVCNLop = new System.Windows.Forms.Label();
            this.lblSDTGVCN = new System.Windows.Forms.Label();
            this.panelPhuHuynh = new System.Windows.Forms.Panel();
            this.dgvPhuHuynh = new Guna.UI2.WinForms.Guna2DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.studentCard = new Student_Management_System_CSharp_SGU2025.GUI.HocSinh.StudentCard();
            this.panelLop.SuspendLayout();
            this.panelPhuHuynh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuHuynh)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblTieuDe.Location = new System.Drawing.Point(15, 16);
            this.lblTieuDe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(266, 32);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Chi tiết hồ sơ học sinh";
            // 
            // panelLop
            // 
            this.panelLop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.panelLop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLop.Controls.Add(this.lblLopHienTai);
            this.panelLop.Controls.Add(this.lblGVCNLop);
            this.panelLop.Controls.Add(this.lblSDTGVCN);
            this.panelLop.Location = new System.Drawing.Point(20, 420);
            this.panelLop.Margin = new System.Windows.Forms.Padding(2);
            this.panelLop.Name = "panelLop";
            this.panelLop.Size = new System.Drawing.Size(760, 90);
            this.panelLop.TabIndex = 2;
            // 
            // lblLopHienTai
            // 
            this.lblLopHienTai.AutoSize = true;
            this.lblLopHienTai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLopHienTai.Location = new System.Drawing.Point(15, 16);
            this.lblLopHienTai.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLopHienTai.Name = "lblLopHienTai";
            this.lblLopHienTai.Size = new System.Drawing.Size(0, 21);
            this.lblLopHienTai.TabIndex = 0;
            // 
            // lblGVCNLop
            // 
            this.lblGVCNLop.AutoSize = true;
            this.lblGVCNLop.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblGVCNLop.Location = new System.Drawing.Point(15, 45);
            this.lblGVCNLop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGVCNLop.Name = "lblGVCNLop";
            this.lblGVCNLop.Size = new System.Drawing.Size(0, 20);
            this.lblGVCNLop.TabIndex = 1;
            // 
            // lblSDTGVCN
            // 
            this.lblSDTGVCN.AutoSize = true;
            this.lblSDTGVCN.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSDTGVCN.Location = new System.Drawing.Point(300, 45);
            this.lblSDTGVCN.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSDTGVCN.Name = "lblSDTGVCN";
            this.lblSDTGVCN.Size = new System.Drawing.Size(0, 20);
            this.lblSDTGVCN.TabIndex = 2;
            // 
            // panelPhuHuynh
            // 
            this.panelPhuHuynh.BackColor = System.Drawing.Color.White;
            this.panelPhuHuynh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPhuHuynh.Controls.Add(this.dgvPhuHuynh);
            this.panelPhuHuynh.Location = new System.Drawing.Point(20, 525);
            this.panelPhuHuynh.Margin = new System.Windows.Forms.Padding(2);
            this.panelPhuHuynh.Name = "panelPhuHuynh";
            this.panelPhuHuynh.Size = new System.Drawing.Size(760, 135);
            this.panelPhuHuynh.TabIndex = 3;
            // 
            // dgvPhuHuynh
            // 
            this.dgvPhuHuynh.AllowUserToAddRows = false;
            this.dgvPhuHuynh.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvPhuHuynh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhuHuynh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPhuHuynh.ColumnHeadersHeight = 15;
            this.dgvPhuHuynh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvPhuHuynh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhuHuynh.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPhuHuynh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhuHuynh.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPhuHuynh.Location = new System.Drawing.Point(0, 0);
            this.dgvPhuHuynh.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPhuHuynh.MultiSelect = false;
            this.dgvPhuHuynh.Name = "dgvPhuHuynh";
            this.dgvPhuHuynh.ReadOnly = true;
            this.dgvPhuHuynh.RowHeadersVisible = false;
            this.dgvPhuHuynh.Size = new System.Drawing.Size(758, 133);
            this.dgvPhuHuynh.TabIndex = 0;
            this.dgvPhuHuynh.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhuHuynh.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvPhuHuynh.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvPhuHuynh.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvPhuHuynh.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvPhuHuynh.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhuHuynh.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPhuHuynh.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvPhuHuynh.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPhuHuynh.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvPhuHuynh.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPhuHuynh.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvPhuHuynh.ThemeStyle.HeaderStyle.Height = 15;
            this.dgvPhuHuynh.ThemeStyle.ReadOnly = true;
            this.dgvPhuHuynh.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhuHuynh.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPhuHuynh.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvPhuHuynh.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPhuHuynh.ThemeStyle.RowsStyle.Height = 22;
            this.dgvPhuHuynh.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPhuHuynh.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Họ và tên";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "SĐT";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Email";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Mối quan hệ";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // btnDong
            // 
            this.btnDong.BorderRadius = 8;
            this.btnDong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(682, 675);
            this.btnDong.Margin = new System.Windows.Forms.Padding(2);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(98, 35);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // studentCard
            // 
            this.studentCard.BackColor = System.Drawing.Color.White;
            this.studentCard.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.studentCard.Location = new System.Drawing.Point(86, 50);
            this.studentCard.Margin = new System.Windows.Forms.Padding(2);
            this.studentCard.Name = "studentCard";
            this.studentCard.Size = new System.Drawing.Size(672, 340);
            this.studentCard.TabIndex = 1;
            // 
            // XemChiTietHocSinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.ClientSize = new System.Drawing.Size(800, 725);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.panelPhuHuynh);
            this.Controls.Add(this.panelLop);
            this.Controls.Add(this.studentCard);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XemChiTietHocSinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết hồ sơ học sinh";
            this.panelLop.ResumeLayout(false);
            this.panelLop.PerformLayout();
            this.panelPhuHuynh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhuHuynh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private StudentCard studentCard;
    }
}