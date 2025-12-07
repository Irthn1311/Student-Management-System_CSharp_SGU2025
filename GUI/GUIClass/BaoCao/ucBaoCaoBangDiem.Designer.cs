namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ucBaoCaoBangDiem
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
            this.pnlGradeTable = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvGrades = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlGradeTableHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.btnExportExcelGrade = new Guna.UI2.WinForms.Guna2Button();
            this.cboClassSelect = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblGradeTableTitle = new System.Windows.Forms.Label();
            this.pnlCharts = new Guna.UI2.WinForms.Guna2Panel();
            this.chartLine = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlChartsHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblChartsTitle = new System.Windows.Forms.Label();
            this.pnlGradeTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrades)).BeginInit();
            this.pnlGradeTableHeader.SuspendLayout();
            this.pnlCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLine)).BeginInit();
            this.pnlChartsHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCharts
            // 
            this.pnlCharts.BackColor = System.Drawing.Color.White;
            this.pnlCharts.BorderColor = System.Drawing.Color.White;
            this.pnlCharts.BorderRadius = 12;
            this.pnlCharts.BorderThickness = 1;
            this.pnlCharts.Controls.Add(this.chartLine);
            this.pnlCharts.Controls.Add(this.chartBar);
            this.pnlCharts.Controls.Add(this.pnlChartsHeader);
            this.pnlCharts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCharts.Location = new System.Drawing.Point(0, 0);
            this.pnlCharts.Name = "pnlCharts";
            this.pnlCharts.Padding = new System.Windows.Forms.Padding(20);
            this.pnlCharts.ShadowDecoration.BorderRadius = 12;
            this.pnlCharts.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(13)))));
            this.pnlCharts.ShadowDecoration.Depth = 5;
            this.pnlCharts.ShadowDecoration.Enabled = true;
            this.pnlCharts.Size = new System.Drawing.Size(1120, 340);
            this.pnlCharts.TabIndex = 1;
            // 
            // chartLine
            // 
            this.chartLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartLine.BackColor = System.Drawing.Color.Transparent;
            this.chartLine.BorderlineColor = System.Drawing.Color.Transparent;
            this.chartLine.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartLine.Location = new System.Drawing.Point(566, 62);
            this.chartLine.Name = "chartLine";
            this.chartLine.Size = new System.Drawing.Size(534, 258);
            this.chartLine.TabIndex = 2;
            this.chartLine.Text = "chartLine";
            // 
            // chartBar
            // 
            this.chartBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartBar.BackColor = System.Drawing.Color.Transparent;
            this.chartBar.BorderlineColor = System.Drawing.Color.Transparent;
            this.chartBar.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chartBar.Location = new System.Drawing.Point(20, 62);
            this.chartBar.Name = "chartBar";
            this.chartBar.Size = new System.Drawing.Size(534, 258);
            this.chartBar.TabIndex = 1;
            this.chartBar.Text = "chartBar";
            // 
            // pnlChartsHeader
            // 
            this.pnlChartsHeader.BackColor = System.Drawing.Color.White;
            this.pnlChartsHeader.Controls.Add(this.lblChartsTitle);
            this.pnlChartsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChartsHeader.Location = new System.Drawing.Point(20, 20);
            this.pnlChartsHeader.Name = "pnlChartsHeader";
            this.pnlChartsHeader.Size = new System.Drawing.Size(1080, 42);
            this.pnlChartsHeader.TabIndex = 0;
            // 
            // lblChartsTitle
            // 
            this.lblChartsTitle.AutoSize = true;
            this.lblChartsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChartsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblChartsTitle.Location = new System.Drawing.Point(16, 11);
            this.lblChartsTitle.Name = "lblChartsTitle";
            this.lblChartsTitle.Size = new System.Drawing.Size(156, 21);
            this.lblChartsTitle.TabIndex = 0;
            this.lblChartsTitle.Text = "Thống kê điểm lớp";
            // 
            // pnlGradeTable
            // 
            this.pnlGradeTable.BackColor = System.Drawing.Color.White;
            this.pnlGradeTable.BorderColor = System.Drawing.Color.White;
            this.pnlGradeTable.BorderRadius = 12;
            this.pnlGradeTable.BorderThickness = 1;
            this.pnlGradeTable.Controls.Add(this.dgvGrades);
            this.pnlGradeTable.Controls.Add(this.pnlGradeTableHeader);
            this.pnlGradeTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGradeTable.Location = new System.Drawing.Point(0, 340);
            this.pnlGradeTable.Name = "pnlGradeTable";
            this.pnlGradeTable.ShadowDecoration.BorderRadius = 12;
            this.pnlGradeTable.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(13)))));
            this.pnlGradeTable.ShadowDecoration.Depth = 5;
            this.pnlGradeTable.ShadowDecoration.Enabled = true;
            this.pnlGradeTable.Size = new System.Drawing.Size(1120, 317);
            this.pnlGradeTable.TabIndex = 0;
            this.pnlGradeTable.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGradeTable_Paint);
            // 
            // dgvGrades
            // 
            this.dgvGrades.AllowUserToAddRows = false;
            this.dgvGrades.AllowUserToDeleteRows = false;
            this.dgvGrades.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvGrades.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGrades.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGrades.ColumnHeadersHeight = 44;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGrades.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGrades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGrades.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.dgvGrades.Location = new System.Drawing.Point(0, 62);
            this.dgvGrades.MultiSelect = false;
            this.dgvGrades.Name = "dgvGrades";
            this.dgvGrades.ReadOnly = true;
            this.dgvGrades.RowHeadersVisible = false;
            this.dgvGrades.RowTemplate.Height = 48;
            this.dgvGrades.Size = new System.Drawing.Size(1120, 595);
            this.dgvGrades.TabIndex = 1;
            this.dgvGrades.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvGrades.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvGrades.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvGrades.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvGrades.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvGrades.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvGrades.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.dgvGrades.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dgvGrades.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvGrades.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.dgvGrades.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.dgvGrades.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGrades.ThemeStyle.HeaderStyle.Height = 44;
            this.dgvGrades.ThemeStyle.ReadOnly = true;
            this.dgvGrades.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvGrades.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvGrades.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dgvGrades.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.dgvGrades.ThemeStyle.RowsStyle.Height = 48;
            this.dgvGrades.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dgvGrades.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.dgvGrades.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrades_CellContentClick);
            // 
            // pnlGradeTableHeader
            // 
            this.pnlGradeTableHeader.BackColor = System.Drawing.Color.White;
            this.pnlGradeTableHeader.Controls.Add(this.btnExportExcelGrade);
            this.pnlGradeTableHeader.Controls.Add(this.cboClassSelect);
            this.pnlGradeTableHeader.Controls.Add(this.lblGradeTableTitle);
            this.pnlGradeTableHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGradeTableHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlGradeTableHeader.Name = "pnlGradeTableHeader";
            this.pnlGradeTableHeader.Size = new System.Drawing.Size(1120, 62);
            this.pnlGradeTableHeader.TabIndex = 0;
            // 
            // btnExportExcelGrade
            // 
            this.btnExportExcelGrade.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcelGrade.BorderRadius = 8;
            this.btnExportExcelGrade.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExportExcelGrade.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExportExcelGrade.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExportExcelGrade.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExportExcelGrade.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnExportExcelGrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnExportExcelGrade.ForeColor = System.Drawing.Color.White;
            this.btnExportExcelGrade.Location = new System.Drawing.Point(990, 9);
            this.btnExportExcelGrade.Name = "btnExportExcelGrade";
            this.btnExportExcelGrade.Size = new System.Drawing.Size(130, 40);
            this.btnExportExcelGrade.TabIndex = 2;
            this.btnExportExcelGrade.Text = "📊 Xuất Excel";
            this.btnExportExcelGrade.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // 
            // cboClassSelect
            // 
            this.cboClassSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClassSelect.BackColor = System.Drawing.Color.Transparent;
            this.cboClassSelect.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(213)))), ((int)(((byte)(219)))));
            this.cboClassSelect.BorderRadius = 8;
            this.cboClassSelect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboClassSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClassSelect.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.cboClassSelect.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.cboClassSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboClassSelect.ForeColor = System.Drawing.Color.Black;
            this.cboClassSelect.ItemHeight = 34;
            this.cboClassSelect.Items.AddRange(new object[] {
            "Lớp 10A1",
            "Lớp 10A2",
            "Lớp 10A3",
            "Lớp 11A1",
            "Lớp 11A2",
            "Lớp 12A1"});
            this.cboClassSelect.Location = new System.Drawing.Point(854, 9);
            this.cboClassSelect.Name = "cboClassSelect";
            this.cboClassSelect.Size = new System.Drawing.Size(130, 40);
            this.cboClassSelect.StartIndex = 0;
            this.cboClassSelect.TabIndex = 1;
            this.cboClassSelect.SelectedIndexChanged += new System.EventHandler(this.CboClassSelect_SelectedIndexChanged);
            // 
            // lblGradeTableTitle
            // 
            this.lblGradeTableTitle.AutoSize = true;
            this.lblGradeTableTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGradeTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblGradeTableTitle.Location = new System.Drawing.Point(36, 22);
            this.lblGradeTableTitle.Name = "lblGradeTableTitle";
            this.lblGradeTableTitle.Size = new System.Drawing.Size(156, 21);
            this.lblGradeTableTitle.TabIndex = 0;
            this.lblGradeTableTitle.Text = "Báo cáo bảng điểm";
            // 
            // ucBaoCaoBangDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlGradeTable);
            this.Controls.Add(this.pnlCharts);
            this.Name = "ucBaoCaoBangDiem";
            this.Size = new System.Drawing.Size(1120, 657);
            this.pnlGradeTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrades)).EndInit();
            this.pnlGradeTableHeader.ResumeLayout(false);
            this.pnlGradeTableHeader.PerformLayout();
            this.pnlCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLine)).EndInit();
            this.pnlChartsHeader.ResumeLayout(false);
            this.pnlChartsHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlGradeTable;
        private Guna.UI2.WinForms.Guna2DataGridView dgvGrades;
        private Guna.UI2.WinForms.Guna2Panel pnlGradeTableHeader;
        private Guna.UI2.WinForms.Guna2Button btnExportExcelGrade;
        private Guna.UI2.WinForms.Guna2ComboBox cboClassSelect;
        private System.Windows.Forms.Label lblGradeTableTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLine;
        private Guna.UI2.WinForms.Guna2Panel pnlChartsHeader;
        private System.Windows.Forms.Label lblChartsTitle;
    }
}
