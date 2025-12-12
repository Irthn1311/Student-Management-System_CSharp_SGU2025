using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FormChiTietYeuCau
    {
        // ========== MAIN PANELS ==========
        private Guna2Panel panelMain;
        private Guna2Panel panelHeader;
        private Guna2Panel panelThongTin;
        private Guna2Panel panelLyDo;
        private Guna2Panel panelTrangThai;
        private Guna2Panel panelXuLy;
        private Guna2Panel panelLopDuocDuyet;
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
        private Label lblLopAdminDuyet;
        private Label lblLopAdminDuyetValue;

        // ========== LÝ DO SECTION ==========
        private Label lblLyDoTitle;
        private Guna2TextBox txtLyDoYeuCau;

        // ========== TRẠNG THÁI SECTION ==========
        private Label lblTrangThai;
        private Label lblTrangThaiValue;
        private Label lblNgayTao;
        private Label lblNgayTaoValue;
        private Label lblNguoiTao;
        private Label lblNguoiTaoValue;

        // ========== XỬ LÝ SECTION ==========
        private Label lblXuLyTitle;
        private Label lblNgayXuLy;
        private Label lblNgayXuLyValue;
        private Label lblNguoiXuLy;
        private Label lblNguoiXuLyValue;

        // ========== LỚP ĐƯỢC DUYỆT SECTION ==========
        private Label lblLopDuocDuyet;
        private Label lblLopDuocDuyetValue;

        // ========== GHI CHÚ SECTION ==========
        private Label lblGhiChuTitle;
        private Guna2TextBox txtGhiChuAdmin;

        // ========== BUTTONS ==========
        private Guna2Button btnDong;

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
            this.lblLopAdminDuyet = new System.Windows.Forms.Label();
            this.lblLopAdminDuyetValue = new System.Windows.Forms.Label();
            this.panelLyDo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLyDoTitle = new System.Windows.Forms.Label();
            this.txtLyDoYeuCau = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelTrangThai = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblTrangThaiValue = new System.Windows.Forms.Label();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.lblNgayTaoValue = new System.Windows.Forms.Label();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.lblNguoiTaoValue = new System.Windows.Forms.Label();
            this.panelXuLy = new Guna.UI2.WinForms.Guna2Panel();
            this.lblXuLyTitle = new System.Windows.Forms.Label();
            this.lblNgayXuLy = new System.Windows.Forms.Label();
            this.lblNgayXuLyValue = new System.Windows.Forms.Label();
            this.lblNguoiXuLy = new System.Windows.Forms.Label();
            this.lblNguoiXuLyValue = new System.Windows.Forms.Label();
            this.panelLopDuocDuyet = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLopDuocDuyet = new System.Windows.Forms.Label();
            this.lblLopDuocDuyetValue = new System.Windows.Forms.Label();
            this.panelGhiChu = new Guna.UI2.WinForms.Guna2Panel();
            this.lblGhiChuTitle = new System.Windows.Forms.Label();
            this.txtGhiChuAdmin = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelThongTin.SuspendLayout();
            this.panelLyDo.SuspendLayout();
            this.panelTrangThai.SuspendLayout();
            this.panelXuLy.SuspendLayout();
            this.panelLopDuocDuyet.SuspendLayout();
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
            this.panelMain.Controls.Add(this.panelTrangThai);
            this.panelMain.Controls.Add(this.panelXuLy);
            this.panelMain.Controls.Add(this.panelLopDuocDuyet);
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
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
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
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chi tiết yêu cầu";
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
            this.panelThongTin.Controls.Add(this.lblLopAdminDuyet);
            this.panelThongTin.Controls.Add(this.lblLopAdminDuyetValue);
            this.panelThongTin.Location = new System.Drawing.Point(20, 100);
            this.panelThongTin.Name = "panelThongTin";
            this.panelThongTin.Padding = new System.Windows.Forms.Padding(20);
            this.panelThongTin.Size = new System.Drawing.Size(660, 190);
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
            // lblLopAdminDuyet
            // 
            this.lblLopAdminDuyet.AutoSize = true;
            this.lblLopAdminDuyet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLopAdminDuyet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblLopAdminDuyet.Location = new System.Drawing.Point(370, 100);
            this.lblLopAdminDuyet.Name = "lblLopAdminDuyet";
            this.lblLopAdminDuyet.Size = new System.Drawing.Size(120, 19);
            this.lblLopAdminDuyet.TabIndex = 9;
            this.lblLopAdminDuyet.Text = "Lớp admin duyệt:";
            // 
            // lblLopAdminDuyetValue
            // 
            this.lblLopAdminDuyetValue.AutoSize = true;
            this.lblLopAdminDuyetValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblLopAdminDuyetValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblLopAdminDuyetValue.Location = new System.Drawing.Point(520, 100);
            this.lblLopAdminDuyetValue.Name = "lblLopAdminDuyetValue";
            this.lblLopAdminDuyetValue.Size = new System.Drawing.Size(35, 19);
            this.lblLopAdminDuyetValue.TabIndex = 10;
            this.lblLopAdminDuyetValue.Text = "N/A";
            // 
            // panelLyDo
            // 
            this.panelLyDo.BackColor = System.Drawing.Color.Gainsboro;
            this.panelLyDo.BorderRadius = 10;
            this.panelLyDo.Controls.Add(this.lblLyDoTitle);
            this.panelLyDo.Controls.Add(this.txtLyDoYeuCau);
            this.panelLyDo.Location = new System.Drawing.Point(20, 310);
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
            // 
            // panelTrangThai
            // 
            this.panelTrangThai.BackColor = System.Drawing.Color.Gainsboro;
            this.panelTrangThai.BorderRadius = 10;
            this.panelTrangThai.Controls.Add(this.lblTrangThai);
            this.panelTrangThai.Controls.Add(this.lblTrangThaiValue);
            this.panelTrangThai.Controls.Add(this.lblNgayTao);
            this.panelTrangThai.Controls.Add(this.lblNgayTaoValue);
            this.panelTrangThai.Controls.Add(this.lblNguoiTao);
            this.panelTrangThai.Controls.Add(this.lblNguoiTaoValue);
            this.panelTrangThai.Location = new System.Drawing.Point(20, 430);
            this.panelTrangThai.Name = "panelTrangThai";
            this.panelTrangThai.Padding = new System.Windows.Forms.Padding(20);
            this.panelTrangThai.Size = new System.Drawing.Size(660, 100);
            this.panelTrangThai.TabIndex = 3;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblTrangThai.Location = new System.Drawing.Point(40, 35);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(75, 19);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // lblTrangThaiValue
            // 
            this.lblTrangThaiValue.AutoSize = true;
            this.lblTrangThaiValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrangThaiValue.Location = new System.Drawing.Point(220, 35);
            this.lblTrangThaiValue.Name = "lblTrangThaiValue";
            this.lblTrangThaiValue.Size = new System.Drawing.Size(35, 19);
            this.lblTrangThaiValue.TabIndex = 1;
            this.lblTrangThaiValue.Text = "N/A";
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayTao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblNgayTao.Location = new System.Drawing.Point(40, 60);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(70, 19);
            this.lblNgayTao.TabIndex = 2;
            this.lblNgayTao.Text = "Ngày tạo:";
            // 
            // lblNgayTaoValue
            // 
            this.lblNgayTaoValue.AutoSize = true;
            this.lblNgayTaoValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayTaoValue.ForeColor = System.Drawing.Color.Black;
            this.lblNgayTaoValue.Location = new System.Drawing.Point(220, 60);
            this.lblNgayTaoValue.Name = "lblNgayTaoValue";
            this.lblNgayTaoValue.Size = new System.Drawing.Size(35, 19);
            this.lblNgayTaoValue.TabIndex = 3;
            this.lblNgayTaoValue.Text = "N/A";
            // 
            // lblNguoiTao
            // 
            this.lblNguoiTao.AutoSize = true;
            this.lblNguoiTao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNguoiTao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblNguoiTao.Location = new System.Drawing.Point(370, 60);
            this.lblNguoiTao.Name = "lblNguoiTao";
            this.lblNguoiTao.Size = new System.Drawing.Size(75, 19);
            this.lblNguoiTao.TabIndex = 4;
            this.lblNguoiTao.Text = "Người tạo:";
            // 
            // lblNguoiTaoValue
            // 
            this.lblNguoiTaoValue.AutoSize = true;
            this.lblNguoiTaoValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNguoiTaoValue.ForeColor = System.Drawing.Color.Black;
            this.lblNguoiTaoValue.Location = new System.Drawing.Point(520, 60);
            this.lblNguoiTaoValue.Name = "lblNguoiTaoValue";
            this.lblNguoiTaoValue.Size = new System.Drawing.Size(35, 19);
            this.lblNguoiTaoValue.TabIndex = 5;
            this.lblNguoiTaoValue.Text = "N/A";
            // 
            // panelXuLy
            // 
            this.panelXuLy.BackColor = System.Drawing.Color.Gainsboro;
            this.panelXuLy.BorderRadius = 10;
            this.panelXuLy.Controls.Add(this.lblXuLyTitle);
            this.panelXuLy.Controls.Add(this.lblNgayXuLy);
            this.panelXuLy.Controls.Add(this.lblNgayXuLyValue);
            this.panelXuLy.Controls.Add(this.lblNguoiXuLy);
            this.panelXuLy.Controls.Add(this.lblNguoiXuLyValue);
            this.panelXuLy.Location = new System.Drawing.Point(20, 550);
            this.panelXuLy.Name = "panelXuLy";
            this.panelXuLy.Padding = new System.Windows.Forms.Padding(20);
            this.panelXuLy.Size = new System.Drawing.Size(660, 80);
            this.panelXuLy.TabIndex = 4;
            this.panelXuLy.Visible = false;
            // 
            // lblXuLyTitle
            // 
            this.lblXuLyTitle.AutoSize = true;
            this.lblXuLyTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblXuLyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblXuLyTitle.Location = new System.Drawing.Point(40, 35);
            this.lblXuLyTitle.Name = "lblXuLyTitle";
            this.lblXuLyTitle.Size = new System.Drawing.Size(120, 21);
            this.lblXuLyTitle.TabIndex = 0;
            this.lblXuLyTitle.Text = "Thông tin xử lý";
            // 
            // lblNgayXuLy
            // 
            this.lblNgayXuLy.AutoSize = true;
            this.lblNgayXuLy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayXuLy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblNgayXuLy.Location = new System.Drawing.Point(40, 60);
            this.lblNgayXuLy.Name = "lblNgayXuLy";
            this.lblNgayXuLy.Size = new System.Drawing.Size(78, 19);
            this.lblNgayXuLy.TabIndex = 1;
            this.lblNgayXuLy.Text = "Ngày xử lý:";
            // 
            // lblNgayXuLyValue
            // 
            this.lblNgayXuLyValue.AutoSize = true;
            this.lblNgayXuLyValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayXuLyValue.ForeColor = System.Drawing.Color.Black;
            this.lblNgayXuLyValue.Location = new System.Drawing.Point(220, 60);
            this.lblNgayXuLyValue.Name = "lblNgayXuLyValue";
            this.lblNgayXuLyValue.Size = new System.Drawing.Size(35, 19);
            this.lblNgayXuLyValue.TabIndex = 2;
            this.lblNgayXuLyValue.Text = "N/A";
            // 
            // lblNguoiXuLy
            // 
            this.lblNguoiXuLy.AutoSize = true;
            this.lblNguoiXuLy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNguoiXuLy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblNguoiXuLy.Location = new System.Drawing.Point(370, 60);
            this.lblNguoiXuLy.Name = "lblNguoiXuLy";
            this.lblNguoiXuLy.Size = new System.Drawing.Size(82, 19);
            this.lblNguoiXuLy.TabIndex = 3;
            this.lblNguoiXuLy.Text = "Người xử lý:";
            // 
            // lblNguoiXuLyValue
            // 
            this.lblNguoiXuLyValue.AutoSize = true;
            this.lblNguoiXuLyValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNguoiXuLyValue.ForeColor = System.Drawing.Color.Black;
            this.lblNguoiXuLyValue.Location = new System.Drawing.Point(520, 60);
            this.lblNguoiXuLyValue.Name = "lblNguoiXuLyValue";
            this.lblNguoiXuLyValue.Size = new System.Drawing.Size(35, 19);
            this.lblNguoiXuLyValue.TabIndex = 4;
            this.lblNguoiXuLyValue.Text = "N/A";
            // 
            // panelLopDuocDuyet
            // 
            this.panelLopDuocDuyet.BackColor = System.Drawing.Color.Gainsboro;
            this.panelLopDuocDuyet.BorderRadius = 10;
            this.panelLopDuocDuyet.Controls.Add(this.lblLopDuocDuyet);
            this.panelLopDuocDuyet.Controls.Add(this.lblLopDuocDuyetValue);
            this.panelLopDuocDuyet.Location = new System.Drawing.Point(20, 650);
            this.panelLopDuocDuyet.Name = "panelLopDuocDuyet";
            this.panelLopDuocDuyet.Padding = new System.Windows.Forms.Padding(20);
            this.panelLopDuocDuyet.Size = new System.Drawing.Size(660, 60);
            this.panelLopDuocDuyet.TabIndex = 5;
            this.panelLopDuocDuyet.Visible = false;
            // 
            // lblLopDuocDuyet
            // 
            this.lblLopDuocDuyet.AutoSize = true;
            this.lblLopDuocDuyet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblLopDuocDuyet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblLopDuocDuyet.Location = new System.Drawing.Point(40, 20);
            this.lblLopDuocDuyet.Name = "lblLopDuocDuyet";
            this.lblLopDuocDuyet.Size = new System.Drawing.Size(130, 21);
            this.lblLopDuocDuyet.TabIndex = 0;
            this.lblLopDuocDuyet.Text = "✅ Lớp được duyệt:";
            // 
            // lblLopDuocDuyetValue
            // 
            this.lblLopDuocDuyetValue.AutoSize = true;
            this.lblLopDuocDuyetValue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblLopDuocDuyetValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblLopDuocDuyetValue.Location = new System.Drawing.Point(220, 20);
            this.lblLopDuocDuyetValue.Name = "lblLopDuocDuyetValue";
            this.lblLopDuocDuyetValue.Size = new System.Drawing.Size(35, 21);
            this.lblLopDuocDuyetValue.TabIndex = 1;
            this.lblLopDuocDuyetValue.Text = "N/A";
            // 
            // panelGhiChu
            // 
            this.panelGhiChu.BackColor = System.Drawing.Color.Gainsboro;
            this.panelGhiChu.BorderRadius = 10;
            this.panelGhiChu.Controls.Add(this.lblGhiChuTitle);
            this.panelGhiChu.Controls.Add(this.txtGhiChuAdmin);
            this.panelGhiChu.Location = new System.Drawing.Point(20, 730);
            this.panelGhiChu.Name = "panelGhiChu";
            this.panelGhiChu.Padding = new System.Windows.Forms.Padding(20);
            this.panelGhiChu.Size = new System.Drawing.Size(660, 100);
            this.panelGhiChu.TabIndex = 6;
            this.panelGhiChu.Visible = false;
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
            this.lblGhiChuTitle.Text = "💬 Ghi chú admin:";
            // 
            // txtGhiChuAdmin
            // 
            this.txtGhiChuAdmin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtGhiChuAdmin.BorderRadius = 8;
            this.txtGhiChuAdmin.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGhiChuAdmin.DefaultText = "";
            this.txtGhiChuAdmin.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtGhiChuAdmin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtGhiChuAdmin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.txtGhiChuAdmin.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGhiChuAdmin.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGhiChuAdmin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGhiChuAdmin.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGhiChuAdmin.Location = new System.Drawing.Point(20, 40);
            this.txtGhiChuAdmin.Multiline = true;
            this.txtGhiChuAdmin.Name = "txtGhiChuAdmin";
            this.txtGhiChuAdmin.PlaceholderText = "";
            this.txtGhiChuAdmin.ReadOnly = true;
            this.txtGhiChuAdmin.SelectedText = "";
            this.txtGhiChuAdmin.Size = new System.Drawing.Size(620, 50);
            this.txtGhiChuAdmin.TabIndex = 1;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.btnDong);
            this.panelButtons.Location = new System.Drawing.Point(20, 850);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(660, 60);
            this.panelButtons.TabIndex = 7;
            // 
            // btnDong
            // 
            this.btnDong.BorderRadius = 8;
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(590, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(70, 40);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FormChiTietYeuCau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 950);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChiTietYeuCau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết yêu cầu chuyển lớp";
            this.Load += new System.EventHandler(this.FormChiTietYeuCau_Load);
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelThongTin.ResumeLayout(false);
            this.panelThongTin.PerformLayout();
            this.panelLyDo.ResumeLayout(false);
            this.panelLyDo.PerformLayout();
            this.panelTrangThai.ResumeLayout(false);
            this.panelTrangThai.PerformLayout();
            this.panelXuLy.ResumeLayout(false);
            this.panelXuLy.PerformLayout();
            this.panelLopDuocDuyet.ResumeLayout(false);
            this.panelLopDuocDuyet.PerformLayout();
            this.panelGhiChu.ResumeLayout(false);
            this.panelGhiChu.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

