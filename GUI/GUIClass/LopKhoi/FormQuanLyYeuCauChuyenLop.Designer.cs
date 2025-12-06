using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FormQuanLyYeuCauChuyenLop
    {
        private Guna2Panel panelMain;
        private Label lblTitle;
        private Guna2Panel panelFilter;
        private RadioButton rbTatCa;
        private RadioButton rbChoDuyet;
        private RadioButton rbDaDuyet;
        private RadioButton rbTuChoi;
        private DataGridView dgvYeuCau;
        private Label lblThongKe;
        private Guna2Panel panelButtons;
        private Guna2Button btnLamMoi;
        private Guna2Button btnXemChiTiet;
        private Guna2Button btnDuyet;
        private Guna2Button btnTuChoi;
        private Guna2Button btnXoa;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvYeuCau = new System.Windows.Forms.DataGridView();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXemChiTiet = new Guna.UI2.WinForms.Guna2Button();
            this.btnDuyet = new Guna.UI2.WinForms.Guna2Button();
            this.btnTuChoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.lblThongKe = new System.Windows.Forms.Label();
            this.panelFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.rbTatCa = new System.Windows.Forms.RadioButton();
            this.rbChoDuyet = new System.Windows.Forms.RadioButton();
            this.rbDaDuyet = new System.Windows.Forms.RadioButton();
            this.rbTuChoi = new System.Windows.Forms.RadioButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYeuCau)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dgvYeuCau);
            this.panelMain.Controls.Add(this.panelButtons);
            this.panelMain.Controls.Add(this.lblThongKe);
            this.panelMain.Controls.Add(this.panelFilter);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 700);
            this.panelMain.TabIndex = 0;
            // 
            // dgvYeuCau
            // 
            this.dgvYeuCau.AllowUserToAddRows = false;
            this.dgvYeuCau.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvYeuCau.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvYeuCau.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvYeuCau.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvYeuCau.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvYeuCau.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvYeuCau.EnableHeadersVisualStyles = false;
            this.dgvYeuCau.Location = new System.Drawing.Point(12, 180);
            this.dgvYeuCau.MultiSelect = false;
            this.dgvYeuCau.Name = "dgvYeuCau";
            this.dgvYeuCau.ReadOnly = true;
            this.dgvYeuCau.RowHeadersVisible = false;
            this.dgvYeuCau.RowTemplate.Height = 40;
            this.dgvYeuCau.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvYeuCau.Size = new System.Drawing.Size(1185, 430);
            this.dgvYeuCau.TabIndex = 3;
            this.dgvYeuCau.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvYeuCau_CellDoubleClick);
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.btnLamMoi);
            this.panelButtons.Controls.Add(this.btnXemChiTiet);
            this.panelButtons.Controls.Add(this.btnDuyet);
            this.panelButtons.Controls.Add(this.btnTuChoi);
            this.panelButtons.Controls.Add(this.btnXoa);
            this.panelButtons.Location = new System.Drawing.Point(30, 625);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1140, 60);
            this.panelButtons.TabIndex = 4;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 8;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(10, 8);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 45);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.BorderRadius = 8;
            this.btnXemChiTiet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnXemChiTiet.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnXemChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnXemChiTiet.Location = new System.Drawing.Point(145, 8);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(140, 45);
            this.btnXemChiTiet.TabIndex = 1;
            this.btnXemChiTiet.Text = "📋 Xem chi tiết";
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnDuyet
            // 
            this.btnDuyet.BorderRadius = 8;
            this.btnDuyet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDuyet.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnDuyet.ForeColor = System.Drawing.Color.White;
            this.btnDuyet.Location = new System.Drawing.Point(300, 8);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(120, 45);
            this.btnDuyet.TabIndex = 2;
            this.btnDuyet.Text = "✅ Duyệt";
            this.btnDuyet.Click += new System.EventHandler(this.btnDuyet_Click);
            // 
            // btnTuChoi
            // 
            this.btnTuChoi.BorderRadius = 8;
            this.btnTuChoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnTuChoi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnTuChoi.ForeColor = System.Drawing.Color.White;
            this.btnTuChoi.Location = new System.Drawing.Point(435, 8);
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(120, 45);
            this.btnTuChoi.TabIndex = 3;
            this.btnTuChoi.Text = "❌ Từ chối";
            this.btnTuChoi.Click += new System.EventHandler(this.btnTuChoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 8;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(570, 8);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(120, 45);
            this.btnXoa.TabIndex = 4;
            this.btnXoa.Text = "🗑️ Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // lblThongKe
            // 
            this.lblThongKe.AutoSize = true;
            this.lblThongKe.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblThongKe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblThongKe.Location = new System.Drawing.Point(30, 145);
            this.lblThongKe.Name = "lblThongKe";
            this.lblThongKe.Size = new System.Drawing.Size(352, 20);
            this.lblThongKe.TabIndex = 2;
            this.lblThongKe.Text = "📊 Tổng: 0 | Chờ duyệt: 0 | Đã duyệt: 0 | Từ chối: 0";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelFilter.BorderRadius = 10;
            this.panelFilter.BorderThickness = 1;
            this.panelFilter.Controls.Add(this.rbTatCa);
            this.panelFilter.Controls.Add(this.rbChoDuyet);
            this.panelFilter.Controls.Add(this.rbDaDuyet);
            this.panelFilter.Controls.Add(this.rbTuChoi);
            this.panelFilter.Location = new System.Drawing.Point(30, 85);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(700, 50);
            this.panelFilter.TabIndex = 1;
            // 
            // rbTatCa
            // 
            this.rbTatCa.AutoSize = true;
            this.rbTatCa.Checked = true;
            this.rbTatCa.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.rbTatCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rbTatCa.Location = new System.Drawing.Point(20, 15);
            this.rbTatCa.Name = "rbTatCa";
            this.rbTatCa.Size = new System.Drawing.Size(89, 23);
            this.rbTatCa.TabIndex = 0;
            this.rbTatCa.TabStop = true;
            this.rbTatCa.Text = "📋 Tất cả";
            this.rbTatCa.UseVisualStyleBackColor = false;
            this.rbTatCa.CheckedChanged += new System.EventHandler(this.rbTatCa_CheckedChanged);
            // 
            // rbChoDuyet
            // 
            this.rbChoDuyet.AutoSize = true;
            this.rbChoDuyet.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.rbChoDuyet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.rbChoDuyet.Location = new System.Drawing.Point(130, 15);
            this.rbChoDuyet.Name = "rbChoDuyet";
            this.rbChoDuyet.Size = new System.Drawing.Size(116, 23);
            this.rbChoDuyet.TabIndex = 1;
            this.rbChoDuyet.Text = "⏳ Chờ duyệt";
            this.rbChoDuyet.UseVisualStyleBackColor = false;
            this.rbChoDuyet.CheckedChanged += new System.EventHandler(this.rbChoDuyet_CheckedChanged);
            // 
            // rbDaDuyet
            // 
            this.rbDaDuyet.AutoSize = true;
            this.rbDaDuyet.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.rbDaDuyet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.rbDaDuyet.Location = new System.Drawing.Point(280, 15);
            this.rbDaDuyet.Name = "rbDaDuyet";
            this.rbDaDuyet.Size = new System.Drawing.Size(107, 23);
            this.rbDaDuyet.TabIndex = 2;
            this.rbDaDuyet.Text = "✅ Đã duyệt";
            this.rbDaDuyet.UseVisualStyleBackColor = false;
            this.rbDaDuyet.CheckedChanged += new System.EventHandler(this.rbDaDuyet_CheckedChanged);
            // 
            // rbTuChoi
            // 
            this.rbTuChoi.AutoSize = true;
            this.rbTuChoi.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.rbTuChoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.rbTuChoi.Location = new System.Drawing.Point(420, 15);
            this.rbTuChoi.Name = "rbTuChoi";
            this.rbTuChoi.Size = new System.Drawing.Size(99, 23);
            this.rbTuChoi.TabIndex = 3;
            this.rbTuChoi.Text = "❌ Từ chối";
            this.rbTuChoi.UseVisualStyleBackColor = false;
            this.rbTuChoi.CheckedChanged += new System.EventHandler(this.rbTuChoi_CheckedChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📋 Quản lý yêu cầu chuyển lớp";
            // 
            // FormQuanLyYeuCauChuyenLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormQuanLyYeuCauChuyenLop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý yêu cầu chuyển lớp";
            this.Load += new System.EventHandler(this.FormQuanLyYeuCauChuyenLop_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYeuCau)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

