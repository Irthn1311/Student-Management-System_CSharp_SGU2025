namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FrmMonHoc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMonHoc = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThemMonHoc = new Guna.UI2.WinForms.Guna2Button();
            this.dgvMonHoc = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MaMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTiet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TuyChinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statcardMonHoc4 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.statcardMonHoc();
            this.statcardMonHoc3 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.statcardMonHoc();
            this.statcardMonHoc2 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.statcardMonHoc();
            this.statcardMonHoc1 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.statcardMonHoc();
            this.panelMonHoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonHoc)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMonHoc
            // 
            this.panelMonHoc.Controls.Add(this.statcardMonHoc4);
            this.panelMonHoc.Controls.Add(this.statcardMonHoc3);
            this.panelMonHoc.Controls.Add(this.statcardMonHoc2);
            this.panelMonHoc.Controls.Add(this.statcardMonHoc1);
            this.panelMonHoc.Controls.Add(this.btnThemMonHoc);
            this.panelMonHoc.Controls.Add(this.dgvMonHoc);
            this.panelMonHoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMonHoc.Location = new System.Drawing.Point(0, 0);
            this.panelMonHoc.MaximumSize = new System.Drawing.Size(1168, 768);
            this.panelMonHoc.Name = "panelMonHoc";
            this.panelMonHoc.Size = new System.Drawing.Size(1168, 768);
            this.panelMonHoc.TabIndex = 0;
            this.panelMonHoc.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMonHoc_Paint);
            // 
            // btnThemMonHoc
            // 
            this.btnThemMonHoc.BorderRadius = 10;
            this.btnThemMonHoc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnThemMonHoc.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnThemMonHoc.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnThemMonHoc.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus1;
            this.btnThemMonHoc.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThemMonHoc.Location = new System.Drawing.Point(20, 17);
            this.btnThemMonHoc.Name = "btnThemMonHoc";
            this.btnThemMonHoc.Size = new System.Drawing.Size(156, 40);
            this.btnThemMonHoc.TabIndex = 2;
            this.btnThemMonHoc.Text = "Thêm môn học";
            // 
            // dgvMonHoc
            // 
            this.dgvMonHoc.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dgvMonHoc.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMonHoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMonHoc.ColumnHeadersHeight = 30;
            this.dgvMonHoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaMon,
            this.TenMon,
            this.SoTiet,
            this.GhiChu,
            this.TuyChinh});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMonHoc.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMonHoc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMonHoc.Location = new System.Drawing.Point(33, 261);
            this.dgvMonHoc.Name = "dgvMonHoc";
            this.dgvMonHoc.ReadOnly = true;
            this.dgvMonHoc.RowHeadersVisible = false;
            this.dgvMonHoc.RowHeadersWidth = 51;
            this.dgvMonHoc.RowTemplate.Height = 24;
            this.dgvMonHoc.Size = new System.Drawing.Size(1132, 478);
            this.dgvMonHoc.TabIndex = 1;
            this.dgvMonHoc.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMonHoc.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvMonHoc.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvMonHoc.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvMonHoc.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvMonHoc.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvMonHoc.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMonHoc.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvMonHoc.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMonHoc.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.dgvMonHoc.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMonHoc.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMonHoc.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvMonHoc.ThemeStyle.ReadOnly = true;
            this.dgvMonHoc.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMonHoc.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMonHoc.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dgvMonHoc.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvMonHoc.ThemeStyle.RowsStyle.Height = 24;
            this.dgvMonHoc.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMonHoc.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // MaMon
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.MaMon.DefaultCellStyle = dataGridViewCellStyle3;
            this.MaMon.HeaderText = "Mã môn";
            this.MaMon.MinimumWidth = 6;
            this.MaMon.Name = "MaMon";
            this.MaMon.ReadOnly = true;
            // 
            // TenMon
            // 
            this.TenMon.HeaderText = "Tên môn";
            this.TenMon.MinimumWidth = 6;
            this.TenMon.Name = "TenMon";
            this.TenMon.ReadOnly = true;
            // 
            // SoTiet
            // 
            this.SoTiet.HeaderText = "Số tiết";
            this.SoTiet.MinimumWidth = 6;
            this.SoTiet.Name = "SoTiet";
            this.SoTiet.ReadOnly = true;
            // 
            // GhiChu
            // 
            this.GhiChu.HeaderText = "Ghi chú";
            this.GhiChu.MinimumWidth = 6;
            this.GhiChu.Name = "GhiChu";
            this.GhiChu.ReadOnly = true;
            // 
            // TuyChinh
            // 
            this.TuyChinh.HeaderText = "Tùy chỉnh";
            this.TuyChinh.MinimumWidth = 6;
            this.TuyChinh.Name = "TuyChinh";
            this.TuyChinh.ReadOnly = true;
            // 
            // statcardMonHoc4
            // 
            this.statcardMonHoc4.BackColor = System.Drawing.Color.Transparent;
            this.statcardMonHoc4.Location = new System.Drawing.Point(890, 81);
            this.statcardMonHoc4.Name = "statcardMonHoc4";
            this.statcardMonHoc4.PanelBackgroundColor = System.Drawing.Color.Transparent;
            this.statcardMonHoc4.Size = new System.Drawing.Size(262, 165);
            this.statcardMonHoc4.SoLuongForeColor = System.Drawing.SystemColors.ControlText;
            this.statcardMonHoc4.TabIndex = 3;
            // 
            // statcardMonHoc3
            // 
            this.statcardMonHoc3.BackColor = System.Drawing.Color.Transparent;
            this.statcardMonHoc3.Location = new System.Drawing.Point(599, 81);
            this.statcardMonHoc3.Name = "statcardMonHoc3";
            this.statcardMonHoc3.PanelBackgroundColor = System.Drawing.Color.Transparent;
            this.statcardMonHoc3.Size = new System.Drawing.Size(262, 165);
            this.statcardMonHoc3.SoLuongForeColor = System.Drawing.SystemColors.ControlText;
            this.statcardMonHoc3.TabIndex = 3;
            // 
            // statcardMonHoc2
            // 
            this.statcardMonHoc2.BackColor = System.Drawing.Color.Transparent;
            this.statcardMonHoc2.Location = new System.Drawing.Point(310, 81);
            this.statcardMonHoc2.Name = "statcardMonHoc2";
            this.statcardMonHoc2.PanelBackgroundColor = System.Drawing.Color.Transparent;
            this.statcardMonHoc2.Size = new System.Drawing.Size(262, 165);
            this.statcardMonHoc2.SoLuongForeColor = System.Drawing.SystemColors.ControlText;
            this.statcardMonHoc2.TabIndex = 3;
            // 
            // statcardMonHoc1
            // 
            this.statcardMonHoc1.BackColor = System.Drawing.Color.Transparent;
            this.statcardMonHoc1.Location = new System.Drawing.Point(20, 81);
            this.statcardMonHoc1.Name = "statcardMonHoc1";
            this.statcardMonHoc1.PanelBackgroundColor = System.Drawing.Color.Transparent;
            this.statcardMonHoc1.Size = new System.Drawing.Size(262, 165);
            this.statcardMonHoc1.SoLuongForeColor = System.Drawing.SystemColors.ControlText;
            this.statcardMonHoc1.TabIndex = 3;
            // 
            // FrmMonHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMonHoc);
            this.MaximumSize = new System.Drawing.Size(1168, 768);
            this.Name = "FrmMonHoc";
            this.Size = new System.Drawing.Size(1168, 768);
            this.Load += new System.EventHandler(this.FrmMonHoc_Load);
            this.panelMonHoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonHoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMonHoc;
        private Guna.UI2.WinForms.Guna2DataGridView dgvMonHoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn GhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn TuyChinh;
        private Guna.UI2.WinForms.Guna2Button btnThemMonHoc;
        private statcardLHP.statcardMonHoc statcardMonHoc4;
        private statcardLHP.statcardMonHoc statcardMonHoc3;
        private statcardLHP.statcardMonHoc statcardMonHoc2;
        private statcardLHP.statcardMonHoc statcardMonHoc1;
    }
}