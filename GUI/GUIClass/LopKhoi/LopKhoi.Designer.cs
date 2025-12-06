using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.GUI;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class LopKhoi
    {
        private Guna2Panel guna2Panel1;
        private Guna2Button btnThem;
        //private Guna2TextBox txtSearch;
        //private Guna2ComboBox cbGiaoVien;
        //private Guna2ComboBox cbSiSo;
        //private Guna2ComboBox cbTrangThai;
        private Guna2Button btnResetFilter;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.statCardKhoi3 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.statCardKhoi2 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.statCardKhoi1 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi();
            this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dgvLop = new Guna.UI2.WinForms.Guna2DataGridView();
            this.MaLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Khoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SiSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThaoTac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.cbNamHoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbGiaoVien = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbSiSo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnResetFilter = new Guna.UI2.WinForms.Guna2Button();
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
            this.guna2Panel1.Controls.Add(this.cbNamHoc);
            this.guna2Panel1.Controls.Add(this.txtSearch);
            this.guna2Panel1.Controls.Add(this.cbGiaoVien);
            this.guna2Panel1.Controls.Add(this.cbSiSo);
            this.guna2Panel1.Controls.Add(this.btnResetFilter);
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
            this.statCardKhoi3.Location = new System.Drawing.Point(775, 150);
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
            this.statCardKhoi2.Location = new System.Drawing.Point(398, 150);
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
            this.statCardKhoi1.Location = new System.Drawing.Point(25, 150);
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
            this.dgvLop.Location = new System.Drawing.Point(25, 292);
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
            this.dgvLop.Size = new System.Drawing.Size(1125, 464);
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
            this.dgvLop.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLop_CellContentClick);
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
            this.SiSo.HeaderText = "Sỉ số";
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
            // btnThem
            // 
            this.btnThem.BorderRadius = 10;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnThem.Image = global::Student_Management_System_CSharp_SGU2025.GUI.Properties.Resources.plus;
            this.btnThem.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThem.Location = new System.Drawing.Point(25, 18);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(156, 40);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm lớp học";
            this.btnThem.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // cbNamHoc
            // 
            this.cbNamHoc.BackColor = System.Drawing.Color.Transparent;
            this.cbNamHoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbNamHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNamHoc.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbNamHoc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbNamHoc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNamHoc.ForeColor = System.Drawing.Color.Black;
            this.cbNamHoc.ItemHeight = 30;
            this.cbNamHoc.Location = new System.Drawing.Point(370, 22);
            this.cbNamHoc.Name = "cbNamHoc";
            this.cbNamHoc.Size = new System.Drawing.Size(200, 36);
            this.cbNamHoc.TabIndex = 6;
            this.cbNamHoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cbNamHoc.SelectedIndexChanged += new System.EventHandler(this.cbNamHoc_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderRadius = 5;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Location = new System.Drawing.Point(25, 70);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Tìm kiếm theo mã lớp, tên lớp, giáo viên...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(300, 36);
            this.txtSearch.TabIndex = 8;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cbGiaoVien
            // 
            this.cbGiaoVien.BackColor = System.Drawing.Color.Transparent;
            this.cbGiaoVien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbGiaoVien.DropDownHeight = 250;
            this.cbGiaoVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGiaoVien.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGiaoVien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbGiaoVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbGiaoVien.ForeColor = System.Drawing.Color.Black;
            this.cbGiaoVien.IntegralHeight = false;
            this.cbGiaoVien.ItemHeight = 30;
            this.cbGiaoVien.Location = new System.Drawing.Point(370, 70);
            this.cbGiaoVien.Name = "cbGiaoVien";
            this.cbGiaoVien.Size = new System.Drawing.Size(200, 36);
            this.cbGiaoVien.TabIndex = 9;
            this.cbGiaoVien.SelectedIndexChanged += new System.EventHandler(this.cbGiaoVien_SelectedIndexChanged);
            // 
            // cbSiSo
            // 
            this.cbSiSo.BackColor = System.Drawing.Color.Transparent;
            this.cbSiSo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbSiSo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSiSo.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbSiSo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbSiSo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cbSiSo.ForeColor = System.Drawing.Color.Black;
            this.cbSiSo.ItemHeight = 30;
            this.cbSiSo.Location = new System.Drawing.Point(628, 22);
            this.cbSiSo.Name = "cbSiSo";
            this.cbSiSo.Size = new System.Drawing.Size(140, 36);
            this.cbSiSo.TabIndex = 10;
            this.cbSiSo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cbSiSo.SelectedIndexChanged += new System.EventHandler(this.cbSiSo_SelectedIndexChanged);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.BorderRadius = 5;
            this.btnResetFilter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnResetFilter.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnResetFilter.ForeColor = System.Drawing.Color.White;
            this.btnResetFilter.Location = new System.Drawing.Point(865, 22);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(120, 36);
            this.btnResetFilter.TabIndex = 12;
            this.btnResetFilter.Text = "Bỏ chọn tất cả";
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
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
        public Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi statCardKhoi3;
        public Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi statCardKhoi2;
        public Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardKhoi statCardKhoi1;
        private DataGridViewTextBoxColumn MaLop;
        private DataGridViewTextBoxColumn TenLop;
        private DataGridViewTextBoxColumn Khoi;
        private DataGridViewTextBoxColumn SiSo;
        private DataGridViewTextBoxColumn GVCN;
        private DataGridViewTextBoxColumn ThaoTac;
        private Guna2ComboBox cbNamHoc;
        private Guna2TextBox txtSearch;
        private Guna2ComboBox cbGiaoVien;
        private Guna2ComboBox cbSiSo;
    }
}
