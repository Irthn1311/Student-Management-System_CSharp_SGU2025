using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class LopKhoi
    {
        private Guna2Panel guna2Panel1;
        private Guna2Button btnThem;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.statCardKhoi3 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.statCardKhoi2 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.statCardKhoi1 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dgvLop = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MaLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Khoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThaoTac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.guna2Panel1.Controls.Add(this.statCardKhoi3);
            this.guna2Panel1.Controls.Add(this.statCardKhoi2);
            this.guna2Panel1.Controls.Add(this.statCardKhoi1);
            this.guna2Panel1.Controls.Add(this.guna2ComboBox1);
            this.guna2Panel1.Controls.Add(this.dgvLop);
            this.guna2Panel1.Controls.Add(this.btnThem);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1168, 768);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // statCardKhoi3
            // 
            this.statCardKhoi3.BackColor = System.Drawing.Color.Transparent;
            this.statCardKhoi3.BorderColor = System.Drawing.Color.White;
            this.statCardKhoi3.Location = new System.Drawing.Point(792, 78);
            this.statCardKhoi3.Name = "statCardKhoi3";
            this.statCardKhoi3.PanelColor = System.Drawing.Color.White;
            this.statCardKhoi3.Size = new System.Drawing.Size(358, 136);
            this.statCardKhoi3.TabIndex = 5;
            this.statCardKhoi3.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // statCardKhoi2
            // 
            this.statCardKhoi2.BackColor = System.Drawing.Color.Transparent;
            this.statCardKhoi2.BorderColor = System.Drawing.Color.White;
            this.statCardKhoi2.Location = new System.Drawing.Point(410, 78);
            this.statCardKhoi2.Name = "statCardKhoi2";
            this.statCardKhoi2.PanelColor = System.Drawing.Color.White;
            this.statCardKhoi2.Size = new System.Drawing.Size(358, 136);
            this.statCardKhoi2.TabIndex = 5;
            this.statCardKhoi2.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // statCardKhoi1
            // 
            this.statCardKhoi1.BackColor = System.Drawing.Color.Transparent;
            this.statCardKhoi1.BorderColor = System.Drawing.Color.White;
            this.statCardKhoi1.Location = new System.Drawing.Point(25, 78);
            this.statCardKhoi1.Name = "statCardKhoi1";
            this.statCardKhoi1.PanelColor = System.Drawing.Color.White;
            this.statCardKhoi1.Size = new System.Drawing.Size(358, 136);
            this.statCardKhoi1.TabIndex = 5;
            this.statCardKhoi1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // guna2ComboBox1
            // 
            this.guna2ComboBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2ComboBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ComboBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2ComboBox1.ItemHeight = 30;
            this.guna2ComboBox1.Items.AddRange(new object[] {
            "Tất cả khối",
            "Khối 10",
            "Khối 11",
            "Khối 12"});
            this.guna2ComboBox1.Location = new System.Drawing.Point(209, 22);
            this.guna2ComboBox1.Name = "guna2ComboBox1";
            this.guna2ComboBox1.Size = new System.Drawing.Size(146, 36);
            this.guna2ComboBox1.StartIndex = 0;
            this.guna2ComboBox1.TabIndex = 4;
            this.guna2ComboBox1.Tag = "";
            this.guna2ComboBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.guna2ComboBox1.SelectedIndexChanged += new System.EventHandler(this.guna2ComboBox1_SelectedIndexChanged);
            // 
            // dgvLop
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvLop.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.18868F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLop.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvLop.ColumnHeadersHeight = 22;
            this.dgvLop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvLop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaLop,
            this.TenLop,
            this.Khoi,
            this.GVCN,
            this.ThaoTac});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.471698F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLop.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvLop.GridColor = System.Drawing.Color.White;
            this.dgvLop.Location = new System.Drawing.Point(25, 259);
            this.dgvLop.Name = "dgvLop";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLop.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvLop.RowHeadersVisible = false;
            this.dgvLop.RowHeadersWidth = 45;
            this.dgvLop.Size = new System.Drawing.Size(1125, 495);
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
            // btnThem
            // 
            this.btnThem.BorderRadius = 10;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnThem.Image = global::Student_Management_System_CSharp_SGU2025.Properties.Resources.plus1;
            this.btnThem.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThem.Location = new System.Drawing.Point(25, 18);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(156, 40);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm lớp học";
            this.btnThem.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // LopKhoi
            // 
            this.Controls.Add(this.guna2Panel1);
            this.Name = "LopKhoi";
            this.Size = new System.Drawing.Size(1168, 768);
            this.Load += new System.EventHandler(this.LopKhoi_Load_1);
            this.guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLop)).EndInit();
            this.ResumeLayout(false);

        }

        private Guna2DataGridView dgvLop;
        private Guna2ComboBox guna2ComboBox1;
        public StatCardKhoi statCardKhoi3;
        public StatCardKhoi statCardKhoi2;
        public StatCardKhoi statCardKhoi1;
        private DataGridViewTextBoxColumn MaLop;
        private DataGridViewTextBoxColumn TenLop;
        private DataGridViewTextBoxColumn Khoi;
        private DataGridViewTextBoxColumn GVCN;
        private DataGridViewTextBoxColumn ThaoTac;
    }
}
