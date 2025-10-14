namespace Student_Management_System_CSharp_SGU2025.GUI.statcardLHP
{
    partial class PhanCongGiangDay
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelPhanCongGiangDay = new Guna.UI2.WinForms.Guna2Panel();
            this.panelShow = new System.Windows.Forms.TableLayoutPanel();
            this.statCardPhanCongGiangDay1 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardPhanCongGiangDay();
            this.statCardPhanCongGiangDay3 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardPhanCongGiangDay();
            this.statCardPhanCongGiangDay4 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardPhanCongGiangDay();
            this.statCardPhanCongGiangDay2 = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.StatCardPhanCongGiangDay();
            this.dgvPhanCong = new Guna.UI2.WinForms.Guna2DataGridView();
            this.btnTatCaKhoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnLocTheoNam = new Guna.UI2.WinForms.Guna2Button();
            this.btnPhanCongMoi = new Guna.UI2.WinForms.Guna2Button();
            this.panelPhanCongGiangDay.SuspendLayout();
            this.panelShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanCong)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPhanCongGiangDay
            // 
            this.panelPhanCongGiangDay.Controls.Add(this.panelShow);
            this.panelPhanCongGiangDay.Controls.Add(this.dgvPhanCong);
            this.panelPhanCongGiangDay.Controls.Add(this.btnTatCaKhoi);
            this.panelPhanCongGiangDay.Controls.Add(this.btnLocTheoNam);
            this.panelPhanCongGiangDay.Controls.Add(this.btnPhanCongMoi);
            this.panelPhanCongGiangDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPhanCongGiangDay.Location = new System.Drawing.Point(0, 0);
            this.panelPhanCongGiangDay.Name = "panelPhanCongGiangDay";
            this.panelPhanCongGiangDay.Size = new System.Drawing.Size(1184, 819);
            this.panelPhanCongGiangDay.TabIndex = 0;
            // 
            // panelShow
            // 
            this.panelShow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShow.ColumnCount = 4;
            this.panelShow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelShow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelShow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelShow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelShow.Controls.Add(this.statCardPhanCongGiangDay1, 0, 0);
            this.panelShow.Controls.Add(this.statCardPhanCongGiangDay3, 2, 0);
            this.panelShow.Controls.Add(this.statCardPhanCongGiangDay4, 3, 0);
            this.panelShow.Controls.Add(this.statCardPhanCongGiangDay2, 1, 0);
            this.panelShow.Location = new System.Drawing.Point(26, 87);
            this.panelShow.Name = "panelShow";
            this.panelShow.RowCount = 1;
            this.panelShow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelShow.Size = new System.Drawing.Size(1136, 132);
            this.panelShow.TabIndex = 8;
            // 
            // statCardPhanCongGiangDay1
            // 
            this.statCardPhanCongGiangDay1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.statCardPhanCongGiangDay1.Location = new System.Drawing.Point(3, 3);
            this.statCardPhanCongGiangDay1.Name = "statCardPhanCongGiangDay1";
            this.statCardPhanCongGiangDay1.Size = new System.Drawing.Size(278, 126);
            this.statCardPhanCongGiangDay1.TabIndex = 0;
            this.statCardPhanCongGiangDay1.Title = "Tên Card";
            this.statCardPhanCongGiangDay1.Value = "Số lượng";
            // 
            // statCardPhanCongGiangDay3
            // 
            this.statCardPhanCongGiangDay3.BackColor = System.Drawing.SystemColors.Window;
            this.statCardPhanCongGiangDay3.Location = new System.Drawing.Point(571, 3);
            this.statCardPhanCongGiangDay3.Name = "statCardPhanCongGiangDay3";
            this.statCardPhanCongGiangDay3.Size = new System.Drawing.Size(278, 126);
            this.statCardPhanCongGiangDay3.TabIndex = 2;
            this.statCardPhanCongGiangDay3.Title = "Tên Card";
            this.statCardPhanCongGiangDay3.Value = "Số lượng";
            // 
            // statCardPhanCongGiangDay4
            // 
            this.statCardPhanCongGiangDay4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.statCardPhanCongGiangDay4.Location = new System.Drawing.Point(855, 3);
            this.statCardPhanCongGiangDay4.Name = "statCardPhanCongGiangDay4";
            this.statCardPhanCongGiangDay4.Size = new System.Drawing.Size(278, 126);
            this.statCardPhanCongGiangDay4.TabIndex = 3;
            this.statCardPhanCongGiangDay4.Title = "Tên Card";
            this.statCardPhanCongGiangDay4.Value = "Số lượng";
            // 
            // statCardPhanCongGiangDay2
            // 
            this.statCardPhanCongGiangDay2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.statCardPhanCongGiangDay2.Location = new System.Drawing.Point(287, 3);
            this.statCardPhanCongGiangDay2.Name = "statCardPhanCongGiangDay2";
            this.statCardPhanCongGiangDay2.Size = new System.Drawing.Size(278, 126);
            this.statCardPhanCongGiangDay2.TabIndex = 1;
            this.statCardPhanCongGiangDay2.Title = "Tên Card";
            this.statCardPhanCongGiangDay2.Value = "Số lượng";
            // 
            // dgvPhanCong
            // 
            this.dgvPhanCong.AllowUserToResizeColumns = false;
            this.dgvPhanCong.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvPhanCong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhanCong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPhanCong.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhanCong.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPhanCong.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvPhanCong.Location = new System.Drawing.Point(26, 232);
            this.dgvPhanCong.Name = "dgvPhanCong";
            this.dgvPhanCong.RowHeadersVisible = false;
            this.dgvPhanCong.RowHeadersWidth = 45;
            this.dgvPhanCong.RowTemplate.Height = 40;
            this.dgvPhanCong.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPhanCong.Size = new System.Drawing.Size(1136, 581);
            this.dgvPhanCong.TabIndex = 7;
            this.dgvPhanCong.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhanCong.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvPhanCong.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvPhanCong.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvPhanCong.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvPhanCong.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhanCong.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvPhanCong.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvPhanCong.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPhanCong.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.471698F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvPhanCong.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPhanCong.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPhanCong.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvPhanCong.ThemeStyle.ReadOnly = false;
            this.dgvPhanCong.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPhanCong.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPhanCong.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.471698F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.dgvPhanCong.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPhanCong.ThemeStyle.RowsStyle.Height = 40;
            this.dgvPhanCong.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPhanCong.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // btnTatCaKhoi
            // 
            this.btnTatCaKhoi.BorderRadius = 10;
            this.btnTatCaKhoi.FillColor = System.Drawing.Color.Cyan;
            this.btnTatCaKhoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnTatCaKhoi.ForeColor = System.Drawing.Color.Black;
            this.btnTatCaKhoi.Location = new System.Drawing.Point(480, 22);
            this.btnTatCaKhoi.Name = "btnTatCaKhoi";
            this.btnTatCaKhoi.Size = new System.Drawing.Size(180, 45);
            this.btnTatCaKhoi.TabIndex = 3;
            this.btnTatCaKhoi.Text = "Tất cả khối";
            // 
            // btnLocTheoNam
            // 
            this.btnLocTheoNam.BorderRadius = 10;
            this.btnLocTheoNam.FillColor = System.Drawing.Color.Cyan;
            this.btnLocTheoNam.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnLocTheoNam.ForeColor = System.Drawing.Color.Black;
            this.btnLocTheoNam.Location = new System.Drawing.Point(271, 22);
            this.btnLocTheoNam.Name = "btnLocTheoNam";
            this.btnLocTheoNam.Size = new System.Drawing.Size(180, 45);
            this.btnLocTheoNam.TabIndex = 2;
            this.btnLocTheoNam.Text = "Lọc theo năm";
            // 
            // btnPhanCongMoi
            // 
            this.btnPhanCongMoi.BorderRadius = 10;
            this.btnPhanCongMoi.FillColor = System.Drawing.Color.Cyan;
            this.btnPhanCongMoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnPhanCongMoi.ForeColor = System.Drawing.Color.Black;
            this.btnPhanCongMoi.Location = new System.Drawing.Point(49, 22);
            this.btnPhanCongMoi.Name = "btnPhanCongMoi";
            this.btnPhanCongMoi.Size = new System.Drawing.Size(180, 45);
            this.btnPhanCongMoi.TabIndex = 1;
            this.btnPhanCongMoi.Text = "Phân công mới";
            // 
            // PhanCongGiangDay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelPhanCongGiangDay);
            this.Name = "PhanCongGiangDay";
            this.Size = new System.Drawing.Size(1184, 819);
            this.Load += new System.EventHandler(this.PhanCongGiangDay_Load);
            this.panelPhanCongGiangDay.ResumeLayout(false);
            this.panelShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanCong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelPhanCongGiangDay;
        private System.Windows.Forms.TableLayoutPanel panelShow;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPhanCong;
        private Guna.UI2.WinForms.Guna2Button btnPhanCongMoi;
        private Guna.UI2.WinForms.Guna2Button btnLocTheoNam;
        private Guna.UI2.WinForms.Guna2Button btnTatCaKhoi;
        private StatCardPhanCongGiangDay statCardPhanCongGiangDay1;
        private StatCardPhanCongGiangDay statCardPhanCongGiangDay2;
        private StatCardPhanCongGiangDay statCardPhanCongGiangDay3;
        private StatCardPhanCongGiangDay statCardPhanCongGiangDay4;
    }
}
