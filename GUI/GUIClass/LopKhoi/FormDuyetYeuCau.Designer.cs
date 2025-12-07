using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FormDuyetYeuCau
    {
        // ========== MAIN PANELS ==========
        private Guna2Panel panelMain;
        private Guna2Panel panelHeader;
        private Guna2Panel panelThongTin;
        private Guna2Panel panelLyDo;
        private Guna2Panel panelChonLop;
        private Guna2Panel panelGhiChu;
        private Guna2Panel panelButtons;

        // ========== HEADER SECTION ==========
        private Label lblTitle;
        private Label lblMaYeuCau;

        // ========== THÔNG TIN HỌC SINH SECTION ==========
        private Label lblThongTinTitle;
        private Label lblHocSinh;
        private Label lblHocSinhValue;
        private Label lblLopHienTai;
        private Label lblLopHienTaiValue;
        private Label lblHocKy;
        private Label lblHocKyValue;
        private Label lblLopMongMuon;
        private Label lblLopMongMuonValue;

        // ========== LÝ DO SECTION ==========
        private Label lblLyDoTitle;
        private Guna2TextBox txtLyDoYeuCau;

        // ========== CHỌN LỚP SECTION ==========
        private Label lblChonLopTitle;
        private ComboBox cbLopDuocDuyet;

        // ========== GHI CHÚ SECTION ==========
        private Label lblGhiChuTitle;
        private Guna2TextBox txtGhiChuAdmin;

        // ========== BUTTONS ==========
        private Guna2Button btnDuyetYeuCau;
        private Guna2Button btnHuy;

        private void InitializeComponent()
        {
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaYeuCau = new System.Windows.Forms.Label();
            this.panelThongTin = new Guna.UI2.WinForms.Guna2Panel();
            this.lblThongTinTitle = new System.Windows.Forms.Label();
            this.lblHocSinh = new System.Windows.Forms.Label();
            this.lblHocSinhValue = new System.Windows.Forms.Label();
            this.lblLopHienTai = new System.Windows.Forms.Label();
            this.lblLopHienTaiValue = new System.Windows.Forms.Label();
            this.lblHocKy = new System.Windows.Forms.Label();
            this.lblHocKyValue = new System.Windows.Forms.Label();
            this.lblLopMongMuon = new System.Windows.Forms.Label();
            this.lblLopMongMuonValue = new System.Windows.Forms.Label();
            this.panelLyDo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLyDoTitle = new System.Windows.Forms.Label();
            this.txtLyDoYeuCau = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelChonLop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblChonLopTitle = new System.Windows.Forms.Label();
            this.cbLopDuocDuyet = new System.Windows.Forms.ComboBox();
            this.panelGhiChu = new Guna.UI2.WinForms.Guna2Panel();
            this.lblGhiChuTitle = new System.Windows.Forms.Label();
            this.txtGhiChuAdmin = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDuyetYeuCau = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelThongTin.SuspendLayout();
            this.panelLyDo.SuspendLayout();
            this.panelChonLop.SuspendLayout();
            this.panelGhiChu.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Controls.Add(this.panelThongTin);
            this.panelMain.Controls.Add(this.panelLyDo);
            this.panelMain.Controls.Add(this.panelChonLop);
            this.panelMain.Controls.Add(this.panelGhiChu);
            this.panelMain.Controls.Add(this.panelButtons);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(700, 750);
            this.panelMain.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblMaYeuCau);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelHeader.Size = new System.Drawing.Size(700, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(39, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(271, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Duyệt yêu cầu chuyển lớp";
            // 
            // lblMaYeuCau
            // 
            this.lblMaYeuCau.AutoSize = true;
            this.lblMaYeuCau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaYeuCau.ForeColor = System.Drawing.Color.White;
            this.lblMaYeuCau.Location = new System.Drawing.Point(40, 46);
            this.lblMaYeuCau.Name = "lblMaYeuCau";
            this.lblMaYeuCau.Size = new System.Drawing.Size(103, 19);
            this.lblMaYeuCau.TabIndex = 1;
            this.lblMaYeuCau.Text = "Mã yêu cầu: #0";
            // 
            // panelThongTin
            // 
            this.panelThongTin.BackColor = System.Drawing.Color.LightGray;
            this.panelThongTin.BorderRadius = 10;
            this.panelThongTin.Controls.Add(this.lblThongTinTitle);
            this.panelThongTin.Controls.Add(this.lblHocSinh);
            this.panelThongTin.Controls.Add(this.lblHocSinhValue);
            this.panelThongTin.Controls.Add(this.lblLopHienTai);
            this.panelThongTin.Controls.Add(this.lblLopHienTaiValue);
            this.panelThongTin.Controls.Add(this.lblHocKy);
            this.panelThongTin.Controls.Add(this.lblHocKyValue);
            this.panelThongTin.Controls.Add(this.lblLopMongMuon);
            this.panelThongTin.Controls.Add(this.lblLopMongMuonValue);
            this.panelThongTin.Location = new System.Drawing.Point(20, 100);
            this.panelThongTin.Name = "panelThongTin";
            this.panelThongTin.Padding = new System.Windows.Forms.Padding(20);
            this.panelThongTin.Size = new System.Drawing.Size(660, 160);
            this.panelThongTin.TabIndex = 1;
            // 
            // lblThongTinTitle
            // 
            this.lblThongTinTitle.AutoSize = true;
            this.lblThongTinTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblThongTinTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblThongTinTitle.Location = new System.Drawing.Point(40, 35);
            this.lblThongTinTitle.Name = "lblThongTinTitle";
            this.lblThongTinTitle.Size = new System.Drawing.Size(144, 21);
            this.lblThongTinTitle.TabIndex = 0;
            this.lblThongTinTitle.Text = "Thông tin học sinh";
            // 
            // lblHocSinh
            // 
            this.lblHocSinh.AutoSize = true;
            this.lblHocSinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHocSinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblHocSinh.Location = new System.Drawing.Point(40, 70);
            this.lblHocSinh.Name = "lblHocSinh";
            this.lblHocSinh.Size = new System.Drawing.Size(65, 19);
            this.lblHocSinh.TabIndex = 1;
            this.lblHocSinh.Text = "Học sinh:";
            // 
            // lblHocSinhValue
            // 
            this.lblHocSinhValue.AutoSize = true;
            this.lblHocSinhValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblHocSinhValue.ForeColor = System.Drawing.Color.Black;
            this.lblHocSinhValue.Location = new System.Drawing.Point(220, 70);
            this.lblHocSinhValue.Name = "lblHocSinhValue";
            this.lblHocSinhValue.Size = new System.Drawing.Size(35, 19);
            this.lblHocSinhValue.TabIndex = 2;
            this.lblHocSinhValue.Text = "N/A";
            // 
            // lblLopHienTai
            // 
            this.lblLopHienTai.AutoSize = true;
            this.lblLopHienTai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLopHienTai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblLopHienTai.Location = new System.Drawing.Point(40, 100);
            this.lblLopHienTai.Name = "lblLopHienTai";
            this.lblLopHienTai.Size = new System.Drawing.Size(84, 19);
            this.lblLopHienTai.TabIndex = 3;
            this.lblLopHienTai.Text = "Lớp hiện tại:";
            // 
            // lblLopHienTaiValue
            // 
            this.lblLopHienTaiValue.AutoSize = true;
            this.lblLopHienTaiValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblLopHienTaiValue.ForeColor = System.Drawing.Color.Black;
            this.lblLopHienTaiValue.Location = new System.Drawing.Point(220, 100);
            this.lblLopHienTaiValue.Name = "lblLopHienTaiValue";
            this.lblLopHienTaiValue.Size = new System.Drawing.Size(35, 19);
            this.lblLopHienTaiValue.TabIndex = 4;
            this.lblLopHienTaiValue.Text = "N/A";
            // 
            // lblHocKy
            // 
            this.lblHocKy.AutoSize = true;
            this.lblHocKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblHocKy.Location = new System.Drawing.Point(40, 130);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new System.Drawing.Size(54, 19);
            this.lblHocKy.TabIndex = 5;
            this.lblHocKy.Text = "Học kỳ:";
            // 
            // lblHocKyValue
            // 
            this.lblHocKyValue.AutoSize = true;
            this.lblHocKyValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblHocKyValue.ForeColor = System.Drawing.Color.Black;
            this.lblHocKyValue.Location = new System.Drawing.Point(220, 130);
            this.lblHocKyValue.Name = "lblHocKyValue";
            this.lblHocKyValue.Size = new System.Drawing.Size(35, 19);
            this.lblHocKyValue.TabIndex = 6;
            this.lblHocKyValue.Text = "N/A";
            // 
            // lblLopMongMuon
            // 
            this.lblLopMongMuon.AutoSize = true;
            this.lblLopMongMuon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLopMongMuon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblLopMongMuon.Location = new System.Drawing.Point(370, 70);
            this.lblLopMongMuon.Name = "lblLopMongMuon";
            this.lblLopMongMuon.Size = new System.Drawing.Size(115, 19);
            this.lblLopMongMuon.TabIndex = 7;
            this.lblLopMongMuon.Text = "Lớp mong muốn:";
            // 
            // lblLopMongMuonValue
            // 
            this.lblLopMongMuonValue.AutoSize = true;
            this.lblLopMongMuonValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblLopMongMuonValue.ForeColor = System.Drawing.Color.Black;
            this.lblLopMongMuonValue.Location = new System.Drawing.Point(520, 70);
            this.lblLopMongMuonValue.Name = "lblLopMongMuonValue";
            this.lblLopMongMuonValue.Size = new System.Drawing.Size(35, 19);
            this.lblLopMongMuonValue.TabIndex = 8;
            this.lblLopMongMuonValue.Text = "N/A";
            // 
            // panelLyDo
            // 
            this.panelLyDo.BackColor = System.Drawing.Color.Gainsboro;
            this.panelLyDo.BorderRadius = 10;
            this.panelLyDo.Controls.Add(this.lblLyDoTitle);
            this.panelLyDo.Controls.Add(this.txtLyDoYeuCau);
            this.panelLyDo.Location = new System.Drawing.Point(20, 280);
            this.panelLyDo.Name = "panelLyDo";
            this.panelLyDo.Padding = new System.Windows.Forms.Padding(20);
            this.panelLyDo.Size = new System.Drawing.Size(660, 130);
            this.panelLyDo.TabIndex = 2;
            // 
            // lblLyDoTitle
            // 
            this.lblLyDoTitle.AutoSize = true;
            this.lblLyDoTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblLyDoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblLyDoTitle.Location = new System.Drawing.Point(40, 35);
            this.lblLyDoTitle.Name = "lblLyDoTitle";
            this.lblLyDoTitle.Size = new System.Drawing.Size(108, 21);
            this.lblLyDoTitle.TabIndex = 0;
            this.lblLyDoTitle.Text = "Lý do yêu cầu";
            // 
            // txtLyDoYeuCau
            // 
            this.txtLyDoYeuCau.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtLyDoYeuCau.BorderRadius = 8;
            this.txtLyDoYeuCau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLyDoYeuCau.DefaultText = "";
            this.txtLyDoYeuCau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLyDoYeuCau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtLyDoYeuCau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.txtLyDoYeuCau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLyDoYeuCau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLyDoYeuCau.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLyDoYeuCau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLyDoYeuCau.Location = new System.Drawing.Point(20, 45);
            this.txtLyDoYeuCau.Multiline = true;
            this.txtLyDoYeuCau.Name = "txtLyDoYeuCau";
            this.txtLyDoYeuCau.PlaceholderText = "";
            this.txtLyDoYeuCau.ReadOnly = true;
            this.txtLyDoYeuCau.SelectedText = "";
            this.txtLyDoYeuCau.Size = new System.Drawing.Size(620, 70);
            this.txtLyDoYeuCau.TabIndex = 1;
            this.txtLyDoYeuCau.TextChanged += new System.EventHandler(this.txtLyDoYeuCau_TextChanged);
            // 
            // panelChonLop
            // 
            this.panelChonLop.BackColor = System.Drawing.Color.Gainsboro;
            this.panelChonLop.BorderRadius = 10;
            this.panelChonLop.Controls.Add(this.lblChonLopTitle);
            this.panelChonLop.Controls.Add(this.cbLopDuocDuyet);
            this.panelChonLop.Location = new System.Drawing.Point(20, 430);
            this.panelChonLop.Name = "panelChonLop";
            this.panelChonLop.Padding = new System.Windows.Forms.Padding(20);
            this.panelChonLop.Size = new System.Drawing.Size(660, 90);
            this.panelChonLop.TabIndex = 3;
            // 
            // lblChonLopTitle
            // 
            this.lblChonLopTitle.AutoSize = true;
            this.lblChonLopTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblChonLopTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblChonLopTitle.Location = new System.Drawing.Point(40, 35);
            this.lblChonLopTitle.Name = "lblChonLopTitle";
            this.lblChonLopTitle.Size = new System.Drawing.Size(160, 21);
            this.lblChonLopTitle.TabIndex = 0;
            this.lblChonLopTitle.Text = "Chọn lớp để duyệt: *";
            // 
            // cbLopDuocDuyet
            // 
            this.cbLopDuocDuyet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLopDuocDuyet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLopDuocDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLopDuocDuyet.ForeColor = System.Drawing.Color.Black;
            this.cbLopDuocDuyet.FormattingEnabled = true;
            this.cbLopDuocDuyet.ItemHeight = 30;
            this.cbLopDuocDuyet.Location = new System.Drawing.Point(20, 45);
            this.cbLopDuocDuyet.Name = "cbLopDuocDuyet";
            this.cbLopDuocDuyet.Size = new System.Drawing.Size(620, 36);
            this.cbLopDuocDuyet.TabIndex = 1;
            this.cbLopDuocDuyet.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLopDuocDuyet_DrawItem);
            this.cbLopDuocDuyet.SelectedIndexChanged += new System.EventHandler(this.cbLopDuocDuyet_SelectedIndexChanged);
            // 
            // panelGhiChu
            // 
            this.panelGhiChu.BackColor = System.Drawing.Color.Gainsboro;
            this.panelGhiChu.BorderRadius = 10;
            this.panelGhiChu.Controls.Add(this.lblGhiChuTitle);
            this.panelGhiChu.Controls.Add(this.txtGhiChuAdmin);
            this.panelGhiChu.Location = new System.Drawing.Point(20, 540);
            this.panelGhiChu.Name = "panelGhiChu";
            this.panelGhiChu.Padding = new System.Windows.Forms.Padding(20);
            this.panelGhiChu.Size = new System.Drawing.Size(660, 120);
            this.panelGhiChu.TabIndex = 4;
            // 
            // lblGhiChuTitle
            // 
            this.lblGhiChuTitle.AutoSize = true;
            this.lblGhiChuTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblGhiChuTitle.Location = new System.Drawing.Point(40, 35);
            this.lblGhiChuTitle.Name = "lblGhiChuTitle";
            this.lblGhiChuTitle.Size = new System.Drawing.Size(148, 19);
            this.lblGhiChuTitle.TabIndex = 0;
            this.lblGhiChuTitle.Text = "💬 Ghi chú (tùy chọn):";
            // 
            // txtGhiChuAdmin
            // 
            this.txtGhiChuAdmin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtGhiChuAdmin.BorderRadius = 8;
            this.txtGhiChuAdmin.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGhiChuAdmin.DefaultText = "";
            this.txtGhiChuAdmin.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtGhiChuAdmin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtGhiChuAdmin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGhiChuAdmin.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGhiChuAdmin.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGhiChuAdmin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGhiChuAdmin.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGhiChuAdmin.Location = new System.Drawing.Point(20, 40);
            this.txtGhiChuAdmin.Multiline = true;
            this.txtGhiChuAdmin.Name = "txtGhiChuAdmin";
            this.txtGhiChuAdmin.PlaceholderText = "Ví dụ: Đã xác nhận với GVCN, Phụ huynh đồng ý...";
            this.txtGhiChuAdmin.SelectedText = "";
            this.txtGhiChuAdmin.Size = new System.Drawing.Size(620, 65);
            this.txtGhiChuAdmin.TabIndex = 1;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.btnDuyetYeuCau);
            this.panelButtons.Controls.Add(this.btnHuy);
            this.panelButtons.Location = new System.Drawing.Point(20, 680);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(660, 60);
            this.panelButtons.TabIndex = 5;
            // 
            // btnDuyetYeuCau
            // 
            this.btnDuyetYeuCau.BorderRadius = 8;
            this.btnDuyetYeuCau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDuyetYeuCau.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnDuyetYeuCau.ForeColor = System.Drawing.Color.White;
            this.btnDuyetYeuCau.Location = new System.Drawing.Point(450, 10);
            this.btnDuyetYeuCau.Name = "btnDuyetYeuCau";
            this.btnDuyetYeuCau.Size = new System.Drawing.Size(130, 40);
            this.btnDuyetYeuCau.TabIndex = 0;
            this.btnDuyetYeuCau.Text = "✅ Duyệt";
            this.btnDuyetYeuCau.Click += new System.EventHandler(this.btnDuyetYeuCau_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 8;
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(590, 10);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(70, 40);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FormDuyetYeuCau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 750);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDuyetYeuCau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Duyệt yêu cầu chuyển lớp";
            this.Load += new System.EventHandler(this.FormDuyetYeuCau_Load);
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelThongTin.ResumeLayout(false);
            this.panelThongTin.PerformLayout();
            this.panelLyDo.ResumeLayout(false);
            this.panelLyDo.PerformLayout();
            this.panelChonLop.ResumeLayout(false);
            this.panelChonLop.PerformLayout();
            this.panelGhiChu.ResumeLayout(false);
            this.panelGhiChu.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
