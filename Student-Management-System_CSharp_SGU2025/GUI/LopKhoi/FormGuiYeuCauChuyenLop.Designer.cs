using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FormGuiYeuCauChuyenLop
    {
        private Guna2Panel panelMain;
        private Label lblTitle;
        private Label lblHocSinh;
        private Label lblLopHienTai;
        private Label lblHocKy;
        private Label lblChonLopMongMuon;
        private ComboBox cbLopMongMuon;
        private Label lblLyDo;
        private Guna2TextBox txtLyDo;
        private Guna2Button btnGuiYeuCau;
        private Guna2Button btnHuy;
        private Label lblHuongDan;

        private void InitializeComponent()
        {
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHocSinh = new System.Windows.Forms.Label();
            this.lblLopHienTai = new System.Windows.Forms.Label();
            this.lblHocKy = new System.Windows.Forms.Label();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.lblChonLopMongMuon = new System.Windows.Forms.Label();
            this.cbLopMongMuon = new System.Windows.Forms.ComboBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnGuiYeuCau = new Guna.UI2.WinForms.Guna2Button();
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
            this.panelMain.Controls.Add(this.lblHuongDan);
            this.panelMain.Controls.Add(this.lblChonLopMongMuon);
            this.panelMain.Controls.Add(this.cbLopMongMuon);
            this.panelMain.Controls.Add(this.lblLyDo);
            this.panelMain.Controls.Add(this.txtLyDo);
            this.panelMain.Controls.Add(this.btnGuiYeuCau);
            this.panelMain.Controls.Add(this.btnHuy);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(550, 520);
            this.panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(219, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Gửi yêu cầu chuyển lớp";
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
            // lblHuongDan
            // 
            this.lblHuongDan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblHuongDan.ForeColor = System.Drawing.Color.Gray;
            this.lblHuongDan.Location = new System.Drawing.Point(25, 160);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(500, 35);
            this.lblHuongDan.TabIndex = 4;
            this.lblHuongDan.Text = "💡 Bạn có thể chọn lớp mong muốn hoặc để admin quyết định lớp phù hợp nhất.";
            // 
            // lblChonLopMongMuon
            // 
            this.lblChonLopMongMuon.AutoSize = true;
            this.lblChonLopMongMuon.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblChonLopMongMuon.Location = new System.Drawing.Point(25, 205);
            this.lblChonLopMongMuon.Name = "lblChonLopMongMuon";
            this.lblChonLopMongMuon.Size = new System.Drawing.Size(158, 19);
            this.lblChonLopMongMuon.TabIndex = 5;
            this.lblChonLopMongMuon.Text = "Lớp mong muốn (nếu có):";
            // 
            // cbLopMongMuon
            // 
            this.cbLopMongMuon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLopMongMuon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLopMongMuon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLopMongMuon.ForeColor = System.Drawing.Color.Black;
            this.cbLopMongMuon.FormattingEnabled = true;
            this.cbLopMongMuon.ItemHeight = 30;
            this.cbLopMongMuon.Location = new System.Drawing.Point(25, 230);
            this.cbLopMongMuon.Name = "cbLopMongMuon";
            this.cbLopMongMuon.Size = new System.Drawing.Size(500, 36);
            this.cbLopMongMuon.TabIndex = 6;
            this.cbLopMongMuon.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLopMongMuon_DrawItem);
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblLyDo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblLyDo.Location = new System.Drawing.Point(25, 285);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(181, 19);
            this.lblLyDo.TabIndex = 7;
            this.lblLyDo.Text = "Lý do chuyển lớp (bắt buộc):";
            // 
            // txtLyDo
            // 
            this.txtLyDo.BorderColor = System.Drawing.Color.Gray;
            this.txtLyDo.BorderRadius = 5;
            this.txtLyDo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLyDo.DefaultText = "";
            this.txtLyDo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLyDo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLyDo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLyDo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLyDo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLyDo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLyDo.Location = new System.Drawing.Point(25, 310);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.PlaceholderText = "Ví dụ: Con em bị bắt nạt, Muốn học cùng bạn thân, Lớp không phù hợp...";
            this.txtLyDo.SelectedText = "";
            this.txtLyDo.Size = new System.Drawing.Size(500, 100);
            this.txtLyDo.TabIndex = 8;
            // 
            // btnGuiYeuCau
            // 
            this.btnGuiYeuCau.BorderRadius = 5;
            this.btnGuiYeuCau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnGuiYeuCau.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuiYeuCau.ForeColor = System.Drawing.Color.White;
            this.btnGuiYeuCau.Location = new System.Drawing.Point(309, 440);
            this.btnGuiYeuCau.Name = "btnGuiYeuCau";
            this.btnGuiYeuCau.Size = new System.Drawing.Size(113, 46);
            this.btnGuiYeuCau.TabIndex = 9;
            this.btnGuiYeuCau.Text = "Gửi yêu cầu";
            this.btnGuiYeuCau.Click += new System.EventHandler(this.btnGuiYeuCau_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 5;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(440, 440);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(85, 46);
            this.btnHuy.TabIndex = 10;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FormGuiYeuCauChuyenLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 520);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGuiYeuCauChuyenLop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gửi yêu cầu chuyển lớp";
            this.Load += new System.EventHandler(this.FormGuiYeuCauChuyenLop_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

