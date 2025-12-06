using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FormDuyetYeuCau
    {
        private Guna2Panel panelMain;
        private Label lblTitle;
        private Label lblHocSinh;
        private Label lblLopHienTai;
        private Label lblHocKy;
        private Label lblLopMongMuon;
        private Label lblLyDoYeuCauTitle;
        private Guna2TextBox txtLyDoYeuCau;
        private Label lblChonLopDuyet;
        private ComboBox cbLopDuocDuyet;
        private Label lblGhiChuAdmin;
        private Guna2TextBox txtGhiChuAdmin;
        private Guna2Button btnDuyetYeuCau;
        private Guna2Button btnHuy;

        private void InitializeComponent()
        {
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHocSinh = new System.Windows.Forms.Label();
            this.lblLopHienTai = new System.Windows.Forms.Label();
            this.lblHocKy = new System.Windows.Forms.Label();
            this.lblLopMongMuon = new System.Windows.Forms.Label();
            this.lblLyDoYeuCauTitle = new System.Windows.Forms.Label();
            this.txtLyDoYeuCau = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblChonLopDuyet = new System.Windows.Forms.Label();
            this.cbLopDuocDuyet = new System.Windows.Forms.ComboBox();
            this.lblGhiChuAdmin = new System.Windows.Forms.Label();
            this.txtGhiChuAdmin = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnDuyetYeuCau = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblHocSinh);
            this.panelMain.Controls.Add(this.lblLopHienTai);
            this.panelMain.Controls.Add(this.lblHocKy);
            this.panelMain.Controls.Add(this.lblLopMongMuon);
            this.panelMain.Controls.Add(this.lblLyDoYeuCauTitle);
            this.panelMain.Controls.Add(this.txtLyDoYeuCau);
            this.panelMain.Controls.Add(this.lblChonLopDuyet);
            this.panelMain.Controls.Add(this.cbLopDuocDuyet);
            this.panelMain.Controls.Add(this.lblGhiChuAdmin);
            this.panelMain.Controls.Add(this.txtGhiChuAdmin);
            this.panelMain.Controls.Add(this.btnDuyetYeuCau);
            this.panelMain.Controls.Add(this.btnHuy);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(600, 650);
            this.panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(238, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Duyệt yêu cầu chuyển lớp";
            // 
            // lblHocSinh
            // 
            this.lblHocSinh.AutoSize = true;
            this.lblHocSinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHocSinh.Location = new System.Drawing.Point(25, 65);
            this.lblHocSinh.Name = "lblHocSinh";
            this.lblHocSinh.Size = new System.Drawing.Size(65, 19);
            this.lblHocSinh.TabIndex = 1;
            this.lblHocSinh.Text = "Học sinh:";
            // 
            // lblLopHienTai
            // 
            this.lblLopHienTai.AutoSize = true;
            this.lblLopHienTai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLopHienTai.Location = new System.Drawing.Point(25, 95);
            this.lblLopHienTai.Name = "lblLopHienTai";
            this.lblLopHienTai.Size = new System.Drawing.Size(88, 19);
            this.lblLopHienTai.TabIndex = 2;
            this.lblLopHienTai.Text = "Lớp hiện tại:";
            // 
            // lblHocKy
            // 
            this.lblHocKy.AutoSize = true;
            this.lblHocKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHocKy.Location = new System.Drawing.Point(25, 125);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new System.Drawing.Size(54, 19);
            this.lblHocKy.TabIndex = 3;
            this.lblHocKy.Text = "Học kỳ:";
            // 
            // lblLopMongMuon
            // 
            this.lblLopMongMuon.AutoSize = true;
            this.lblLopMongMuon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLopMongMuon.Location = new System.Drawing.Point(25, 155);
            this.lblLopMongMuon.Name = "lblLopMongMuon";
            this.lblLopMongMuon.Size = new System.Drawing.Size(117, 19);
            this.lblLopMongMuon.TabIndex = 4;
            this.lblLopMongMuon.Text = "Lớp mong muốn:";
            // 
            // lblLyDoYeuCauTitle
            // 
            this.lblLyDoYeuCauTitle.AutoSize = true;
            this.lblLyDoYeuCauTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblLyDoYeuCauTitle.Location = new System.Drawing.Point(25, 190);
            this.lblLyDoYeuCauTitle.Name = "lblLyDoYeuCauTitle";
            this.lblLyDoYeuCauTitle.Size = new System.Drawing.Size(121, 19);
            this.lblLyDoYeuCauTitle.TabIndex = 5;
            this.lblLyDoYeuCauTitle.Text = "Lý do của yêu cầu:";
            // 
            // txtLyDoYeuCau
            // 
            this.txtLyDoYeuCau.BorderColor = System.Drawing.Color.Gray;
            this.txtLyDoYeuCau.BorderRadius = 5;
            this.txtLyDoYeuCau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLyDoYeuCau.DefaultText = "";
            this.txtLyDoYeuCau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLyDoYeuCau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLyDoYeuCau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLyDoYeuCau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLyDoYeuCau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLyDoYeuCau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLyDoYeuCau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLyDoYeuCau.Location = new System.Drawing.Point(25, 215);
            this.txtLyDoYeuCau.Multiline = true;
            this.txtLyDoYeuCau.Name = "txtLyDoYeuCau";
            this.txtLyDoYeuCau.ReadOnly = true;
            this.txtLyDoYeuCau.SelectedText = "";
            this.txtLyDoYeuCau.Size = new System.Drawing.Size(550, 80);
            this.txtLyDoYeuCau.TabIndex = 6;
            // 
            // lblChonLopDuyet
            // 
            this.lblChonLopDuyet.AutoSize = true;
            this.lblChonLopDuyet.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblChonLopDuyet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblChonLopDuyet.Location = new System.Drawing.Point(25, 315);
            this.lblChonLopDuyet.Name = "lblChonLopDuyet";
            this.lblChonLopDuyet.Size = new System.Drawing.Size(142, 19);
            this.lblChonLopDuyet.TabIndex = 7;
            this.lblChonLopDuyet.Text = "Chọn lớp để duyệt: *";
            // 
            // cbLopDuocDuyet
            // 
            this.cbLopDuocDuyet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLopDuocDuyet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLopDuocDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLopDuocDuyet.ForeColor = System.Drawing.Color.Black;
            this.cbLopDuocDuyet.FormattingEnabled = true;
            this.cbLopDuocDuyet.ItemHeight = 30;
            this.cbLopDuocDuyet.Location = new System.Drawing.Point(25, 340);
            this.cbLopDuocDuyet.Name = "cbLopDuocDuyet";
            this.cbLopDuocDuyet.Size = new System.Drawing.Size(550, 36);
            this.cbLopDuocDuyet.TabIndex = 8;
            this.cbLopDuocDuyet.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLopDuocDuyet_DrawItem);
            // 
            // lblGhiChuAdmin
            // 
            this.lblGhiChuAdmin.AutoSize = true;
            this.lblGhiChuAdmin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChuAdmin.ForeColor = System.Drawing.Color.Gray;
            this.lblGhiChuAdmin.Location = new System.Drawing.Point(25, 395);
            this.lblGhiChuAdmin.Name = "lblGhiChuAdmin";
            this.lblGhiChuAdmin.Size = new System.Drawing.Size(128, 19);
            this.lblGhiChuAdmin.TabIndex = 9;
            this.lblGhiChuAdmin.Text = "Ghi chú (tùy chọn):";
            // 
            // txtGhiChuAdmin
            // 
            this.txtGhiChuAdmin.BorderColor = System.Drawing.Color.Gray;
            this.txtGhiChuAdmin.BorderRadius = 5;
            this.txtGhiChuAdmin.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGhiChuAdmin.DefaultText = "";
            this.txtGhiChuAdmin.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtGhiChuAdmin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtGhiChuAdmin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGhiChuAdmin.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGhiChuAdmin.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGhiChuAdmin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGhiChuAdmin.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGhiChuAdmin.Location = new System.Drawing.Point(25, 420);
            this.txtGhiChuAdmin.Multiline = true;
            this.txtGhiChuAdmin.Name = "txtGhiChuAdmin";
            this.txtGhiChuAdmin.PlaceholderText = "Ví dụ: Đã xác nhận với GVCN, Phụ huynh đồng ý...";
            this.txtGhiChuAdmin.SelectedText = "";
            this.txtGhiChuAdmin.Size = new System.Drawing.Size(550, 120);
            this.txtGhiChuAdmin.TabIndex = 10;
            // 
            // btnDuyetYeuCau
            // 
            this.btnDuyetYeuCau.BorderRadius = 5;
            this.btnDuyetYeuCau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDuyetYeuCau.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnDuyetYeuCau.ForeColor = System.Drawing.Color.White;
            this.btnDuyetYeuCau.Location = new System.Drawing.Point(360, 565);
            this.btnDuyetYeuCau.Name = "btnDuyetYeuCau";
            this.btnDuyetYeuCau.Size = new System.Drawing.Size(113, 46);
            this.btnDuyetYeuCau.TabIndex = 11;
            this.btnDuyetYeuCau.Text = "✅ Duyệt";
            this.btnDuyetYeuCau.Click += new System.EventHandler(this.btnDuyetYeuCau_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 5;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(490, 565);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(85, 46);
            this.btnHuy.TabIndex = 12;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FormDuyetYeuCau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 650);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDuyetYeuCau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Duyệt yêu cầu chuyển lớp";
            this.Load += new System.EventHandler(this.FormDuyetYeuCau_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

