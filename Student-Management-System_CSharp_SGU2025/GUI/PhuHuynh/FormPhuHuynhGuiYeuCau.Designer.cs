using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.PhuHuynh
{
    partial class FormPhuHuynhGuiYeuCau
    {
        private Guna2Panel panelMain;
        private Label lblTitle;
        private Label lblHuongDan;
        private Label lblChonConEm;
        private ComboBox cbHocSinh;
        private Label lblHocSinh; // Hiển thị thông tin học sinh
        private Label lblChonHocKy;
        private ComboBox cbHocKy;
        private Label lblThongTinLop;
        private Guna2Button btnGuiYeuCau;
        private Guna2Button btnHuy;

        private void InitializeComponent()
        {
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.lblChonConEm = new System.Windows.Forms.Label();
            this.cbHocSinh = new System.Windows.Forms.ComboBox();
            this.lblHocSinh = new System.Windows.Forms.Label();
            this.lblChonHocKy = new System.Windows.Forms.Label();
            this.cbHocKy = new System.Windows.Forms.ComboBox();
            this.lblThongTinLop = new System.Windows.Forms.Label();
            this.btnGuiYeuCau = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblHuongDan);
            this.panelMain.Controls.Add(this.lblHocSinh);
            this.panelMain.Controls.Add(this.lblChonConEm);
            this.panelMain.Controls.Add(this.cbHocSinh);
            this.panelMain.Controls.Add(this.lblChonHocKy);
            this.panelMain.Controls.Add(this.cbHocKy);
            this.panelMain.Controls.Add(this.lblThongTinLop);
            this.panelMain.Controls.Add(this.btnGuiYeuCau);
            this.panelMain.Controls.Add(this.btnHuy);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(500, 370);
            this.panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(290, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📤 Gửi yêu cầu chuyển lớp";
            // 
            // lblHuongDan
            // 
            this.lblHuongDan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblHuongDan.ForeColor = System.Drawing.Color.Gray;
            this.lblHuongDan.Location = new System.Drawing.Point(25, 60);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(450, 40);
            this.lblHuongDan.TabIndex = 1;
            this.lblHuongDan.Text = "💡 Chọn con em và học kỳ, sau đó nhấn \"Gửi yêu cầu\" để bắt đầu quy trình chuyển lớp. Nhà trường sẽ xem xét và phản hồi sớm nhất.";
            // 
            // lblHocSinh (Hiển thị thông tin học sinh đang đăng nhập)
            // 
            this.lblHocSinh.AutoSize = true;
            this.lblHocSinh.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblHocSinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblHocSinh.Location = new System.Drawing.Point(25, 120);
            this.lblHocSinh.Name = "lblHocSinh";
            this.lblHocSinh.Size = new System.Drawing.Size(200, 20);
            this.lblHocSinh.TabIndex = 2;
            this.lblHocSinh.Text = "👤 Học sinh: ...";
            // 
            // lblChonConEm (ẨN - không dùng)
            // 
            this.lblChonConEm.AutoSize = true;
            this.lblChonConEm.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblChonConEm.Location = new System.Drawing.Point(25, 120);
            this.lblChonConEm.Name = "lblChonConEm";
            this.lblChonConEm.Size = new System.Drawing.Size(100, 19);
            this.lblChonConEm.TabIndex = 2;
            this.lblChonConEm.Text = "Chọn con em: *";
            this.lblChonConEm.Visible = false;
            // 
            // cbHocSinh (ẨN - không dùng)
            // 
            this.cbHocSinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocSinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbHocSinh.FormattingEnabled = true;
            this.cbHocSinh.Location = new System.Drawing.Point(25, 145);
            this.cbHocSinh.Name = "cbHocSinh";
            this.cbHocSinh.Size = new System.Drawing.Size(450, 25);
            this.cbHocSinh.TabIndex = 3;
            this.cbHocSinh.Visible = false;
            this.cbHocSinh.SelectedIndexChanged += new System.EventHandler(this.cbHocSinh_SelectedIndexChanged);
            // 
            // lblChonHocKy
            // 
            this.lblChonHocKy.AutoSize = true;
            this.lblChonHocKy.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblChonHocKy.Location = new System.Drawing.Point(25, 160);
            this.lblChonHocKy.Name = "lblChonHocKy";
            this.lblChonHocKy.Size = new System.Drawing.Size(101, 19);
            this.lblChonHocKy.TabIndex = 4;
            this.lblChonHocKy.Text = "Chọn học kỳ: *";
            // 
            // cbHocKy
            // 
            this.cbHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbHocKy.FormattingEnabled = true;
            this.cbHocKy.Location = new System.Drawing.Point(25, 185);
            this.cbHocKy.Name = "cbHocKy";
            this.cbHocKy.Size = new System.Drawing.Size(450, 25);
            this.cbHocKy.TabIndex = 5;
            this.cbHocKy.SelectedIndexChanged += new System.EventHandler(this.cbHocSinh_SelectedIndexChanged);
            // 
            // lblThongTinLop
            // 
            this.lblThongTinLop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblThongTinLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblThongTinLop.Location = new System.Drawing.Point(25, 230);
            this.lblThongTinLop.Name = "lblThongTinLop";
            this.lblThongTinLop.Size = new System.Drawing.Size(450, 30);
            this.lblThongTinLop.TabIndex = 6;
            this.lblThongTinLop.Text = "📚 Lớp hiện tại: ...";
            this.lblThongTinLop.Visible = false;
            // 
            // btnGuiYeuCau
            // 
            this.btnGuiYeuCau.BorderRadius = 5;
            this.btnGuiYeuCau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnGuiYeuCau.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnGuiYeuCau.ForeColor = System.Drawing.Color.White;
            this.btnGuiYeuCau.Location = new System.Drawing.Point(230, 290);
            this.btnGuiYeuCau.Name = "btnGuiYeuCau";
            this.btnGuiYeuCau.Size = new System.Drawing.Size(140, 50);
            this.btnGuiYeuCau.TabIndex = 7;
            this.btnGuiYeuCau.Text = "📤 Gửi yêu cầu";
            this.btnGuiYeuCau.Click += new System.EventHandler(this.btnGuiYeuCau_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 5;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(385, 290);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 50);
            this.btnHuy.TabIndex = 8;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FormPhuHuynhGuiYeuCau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 370);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPhuHuynhGuiYeuCau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gửi yêu cầu chuyển lớp";
            this.Load += new System.EventHandler(this.FormPhuHuynhGuiYeuCau_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}

