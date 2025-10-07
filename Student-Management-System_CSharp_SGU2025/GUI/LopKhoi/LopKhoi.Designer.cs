using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class LopKhoi
    {
        private Guna2Panel guna2Panel1;
        private Guna2Button guna2Button1;
        private Guna2Button guna2Button2;
        private Guna2Panel panelThongKe;

        private StatCardKhoi statCardKhoi10;
        private StatCardKhoi statCardKhoi11;
        private StatCardKhoi statCardKhoi12;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvLop = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MaLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Khoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThaoTac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.panelThongKe = new Guna.UI2.WinForms.Guna2Panel();
            this.statCardKhoi10 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.statCardKhoi11 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.statCardKhoi12 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).BeginInit();
            this.panelThongKe.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.dgvLop);
            this.guna2Panel1.Controls.Add(this.guna2Button1);
            this.guna2Panel1.Controls.Add(this.guna2Button2);
            this.guna2Panel1.Controls.Add(this.panelThongKe);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1184, 819);
            this.guna2Panel1.TabIndex = 0;
            // 
            // dgvLop
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvLop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.18868F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLop.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLop.ColumnHeadersHeight = 22;
            this.dgvLop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaLop,
            this.TenLop,
            this.Khoi,
            this.SiSo,
            this.GVCN,
            this.ThaoTac});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.471698F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLop.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvLop.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLop.Location = new System.Drawing.Point(3, 301);
            this.dgvLop.Name = "dgvLop";
            this.dgvLop.RowHeadersVisible = false;
            this.dgvLop.RowHeadersWidth = 45;
            this.dgvLop.Size = new System.Drawing.Size(1178, 515);
            this.dgvLop.TabIndex = 3;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvLop.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvLop.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLop.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvLop.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLop.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.471698F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvLop.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvLop.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLop.ThemeStyle.HeaderStyle.Height = 22;
            this.dgvLop.ThemeStyle.ReadOnly = false;
            this.dgvLop.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLop.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLop.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.471698F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvLop.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvLop.ThemeStyle.RowsStyle.Height = 22;
            this.dgvLop.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvLop.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // MaLop
            // 
            this.MaLop.HeaderText = "Mã lớp";
            this.MaLop.MinimumWidth = 6;
            this.MaLop.Name = "MaLop";
            // 
            // TenLop
            // 
            this.TenLop.HeaderText = "Tên lớp";
            this.TenLop.MinimumWidth = 6;
            this.TenLop.Name = "TenLop";
            // 
            // Khoi
            // 
            this.Khoi.HeaderText = "Khối";
            this.Khoi.MinimumWidth = 6;
            this.Khoi.Name = "Khoi";
            // 
            // SiSo
            // 
            this.SiSo.HeaderText = "Sĩ số";
            this.SiSo.MinimumWidth = 6;
            this.SiSo.Name = "SiSo";
            // 
            // GVCN
            // 
            this.GVCN.HeaderText = "GVCN";
            this.GVCN.MinimumWidth = 6;
            this.GVCN.Name = "GVCN";
            // 
            // ThaoTac
            // 
            this.ThaoTac.HeaderText = "Thao tác";
            this.ThaoTac.MinimumWidth = 6;
            this.ThaoTac.Name = "ThaoTac";
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.guna2Button1.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.add;
            this.guna2Button1.Location = new System.Drawing.Point(25, 20);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(189, 56);
            this.guna2Button1.TabIndex = 0;
            this.guna2Button1.Text = "Thêm lớp học";
            // 
            // guna2Button2
            // 
            this.guna2Button2.BorderRadius = 10;
            this.guna2Button2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.guna2Button2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.guna2Button2.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.filter;
            this.guna2Button2.Location = new System.Drawing.Point(238, 20);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(170, 56);
            this.guna2Button2.TabIndex = 1;
            this.guna2Button2.Text = "Tất cả khối";
            // 
            // panelThongKe
            // 
            this.panelThongKe.BackColor = System.Drawing.Color.Transparent;
            this.panelThongKe.BorderRadius = 15;
            this.panelThongKe.Controls.Add(this.statCardKhoi10);
            this.panelThongKe.Controls.Add(this.statCardKhoi11);
            this.panelThongKe.Controls.Add(this.statCardKhoi12);
            this.panelThongKe.FillColor = System.Drawing.Color.White;
            this.panelThongKe.Location = new System.Drawing.Point(20, 100);
            this.panelThongKe.Name = "panelThongKe";
            this.panelThongKe.ShadowDecoration.Enabled = true;
            this.panelThongKe.Size = new System.Drawing.Size(1140, 180);
            this.panelThongKe.TabIndex = 2;
            this.panelThongKe.Paint += new System.Windows.Forms.PaintEventHandler(this.panelThongKe_Paint);
            // 
            // statCardKhoi10
            // 
            this.statCardKhoi10.BackColor = System.Drawing.Color.RosyBrown;
            this.statCardKhoi10.Location = new System.Drawing.Point(74, 30);
            this.statCardKhoi10.Name = "statCardKhoi10";
            this.statCardKhoi10.Size = new System.Drawing.Size(250, 120);
            this.statCardKhoi10.TabIndex = 0;
            this.statCardKhoi10.Load += new System.EventHandler(this.statCardKhoi10_Load);
            // 
            // statCardKhoi11
            // 
            this.statCardKhoi11.BackColor = System.Drawing.Color.RosyBrown;
            this.statCardKhoi11.Location = new System.Drawing.Point(453, 30);
            this.statCardKhoi11.Name = "statCardKhoi11";
            this.statCardKhoi11.Size = new System.Drawing.Size(250, 120);
            this.statCardKhoi11.TabIndex = 1;
            // 
            // statCardKhoi12
            // 
            this.statCardKhoi12.BackColor = System.Drawing.Color.RosyBrown;
            this.statCardKhoi12.Location = new System.Drawing.Point(816, 30);
            this.statCardKhoi12.Name = "statCardKhoi12";
            this.statCardKhoi12.Size = new System.Drawing.Size(250, 120);
            this.statCardKhoi12.TabIndex = 2;
            // 
            // LopKhoi
            // 
            this.Controls.Add(this.guna2Panel1);
            this.Name = "LopKhoi";
            this.Size = new System.Drawing.Size(1184, 819);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).EndInit();
            this.panelThongKe.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Guna2DataGridView dgvLop;
        private DataGridViewTextBoxColumn MaLop;
        private DataGridViewTextBoxColumn TenLop;
        private DataGridViewTextBoxColumn Khoi;
        private DataGridViewTextBoxColumn SiSo;
        private DataGridViewTextBoxColumn GVCN;
        private DataGridViewTextBoxColumn ThaoTac;
    }
}
