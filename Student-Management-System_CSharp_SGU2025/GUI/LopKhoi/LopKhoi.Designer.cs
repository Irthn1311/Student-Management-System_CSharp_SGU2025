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
        private StatCardKhoi statCardKhoi11;
        private StatCardKhoi statCardKhoi12;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvLop = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MaLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Khoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThaoTac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.statCardKhoi10 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.statCardKhoi11 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.statCardKhoi12 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.guna2Panel1.Controls.Add(this.guna2ComboBox1);
            this.guna2Panel1.Controls.Add(this.statCardKhoi10);
            this.guna2Panel1.Controls.Add(this.dgvLop);
            this.guna2Panel1.Controls.Add(this.statCardKhoi11);
            this.guna2Panel1.Controls.Add(this.guna2Button1);
            this.guna2Panel1.Controls.Add(this.statCardKhoi12);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1184, 819);
            this.guna2Panel1.TabIndex = 0;
            // 
            // dgvLop
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvLop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.18868F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLop.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLop.ColumnHeadersHeight = 22;
            this.dgvLop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaLop,
            this.TenLop,
            this.Khoi,
            this.SiSo,
            this.GVCN,
            this.ThaoTac});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.471698F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLop.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLop.GridColor = System.Drawing.Color.White;
            this.dgvLop.Location = new System.Drawing.Point(25, 286);
            this.dgvLop.Name = "dgvLop";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLop.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLop.RowHeadersVisible = false;
            this.dgvLop.RowHeadersWidth = 45;
            this.dgvLop.Size = new System.Drawing.Size(1135, 495);
            this.dgvLop.TabIndex = 3;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvLop.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvLop.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvLop.ThemeStyle.GridColor = System.Drawing.Color.White;
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
            // guna2ComboBox1
            // 
            this.guna2ComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ComboBox1.ForeColor = System.Drawing.Color.White;
            this.guna2ComboBox1.ItemHeight = 30;
            this.guna2ComboBox1.Items.AddRange(new object[] {
            "Tất cả khối",
            "Khối 10",
            "Khối 11",
            "Khối 12"});
            this.guna2ComboBox1.Location = new System.Drawing.Point(985, 40);
            this.guna2ComboBox1.Name = "guna2ComboBox1";
            this.guna2ComboBox1.Size = new System.Drawing.Size(175, 36);
            this.guna2ComboBox1.StartIndex = 0;
            this.guna2ComboBox1.TabIndex = 4;
            this.guna2ComboBox1.Tag = "";
            this.guna2ComboBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.guna2ComboBox1.SelectedIndexChanged += new System.EventHandler(this.guna2ComboBox1_SelectedIndexChanged);
            // 
            // statCardKhoi10
            // 
            this.statCardKhoi10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.statCardKhoi10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statCardKhoi10.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.statCardKhoi10.Location = new System.Drawing.Point(94, 116);
            this.statCardKhoi10.Name = "statCardKhoi10";
            this.statCardKhoi10.Size = new System.Drawing.Size(262, 136);
            this.statCardKhoi10.TabIndex = 0;
            this.statCardKhoi10.Load += new System.EventHandler(this.statCardKhoi10_Load);
            // 
            // statCardKhoi11
            // 
            this.statCardKhoi11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.statCardKhoi11.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.statCardKhoi11.Location = new System.Drawing.Point(444, 116);
            this.statCardKhoi11.Name = "statCardKhoi11";
            this.statCardKhoi11.Size = new System.Drawing.Size(262, 136);
            this.statCardKhoi11.TabIndex = 1;
            // 
            // statCardKhoi12
            // 
            this.statCardKhoi12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.statCardKhoi12.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.statCardKhoi12.Location = new System.Drawing.Point(808, 116);
            this.statCardKhoi12.Name = "statCardKhoi12";
            this.statCardKhoi12.Size = new System.Drawing.Size(262, 136);
            this.statCardKhoi12.TabIndex = 2;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.guna2Button1.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.add;
            this.guna2Button1.Location = new System.Drawing.Point(25, 40);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(201, 47);
            this.guna2Button1.TabIndex = 0;
            this.guna2Button1.Text = "Thêm lớp học";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // LopKhoi
            // 
            this.Controls.Add(this.guna2Panel1);
            this.Name = "LopKhoi";
            this.Size = new System.Drawing.Size(1184, 819);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).EndInit();
            this.ResumeLayout(false);

        }

        private Guna2DataGridView dgvLop;
        private DataGridViewTextBoxColumn MaLop;
        private DataGridViewTextBoxColumn TenLop;
        private DataGridViewTextBoxColumn Khoi;
        private DataGridViewTextBoxColumn SiSo;
        private DataGridViewTextBoxColumn GVCN;
        private DataGridViewTextBoxColumn ThaoTac;
        private StatCardKhoi statCardKhoi10;
        private Guna2ComboBox guna2ComboBox1;
    }
}
