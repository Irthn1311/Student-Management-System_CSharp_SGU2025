using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class FormChuyenLop
    {
        private Guna2Panel panelMain;
        private Label lblTitle;
        private Label lblHocSinh;
        private Label lblLopCu;
        private Label lblHocKy;
        private Label lblChonLopMoi;
        private ComboBox cbLopMoi;
        private Label lblLyDo;
        private Guna2TextBox txtLyDo;
        private Guna2Button btnXacNhan;
        private Guna2Button btnHuy;

        private void InitializeComponent()
        {
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHocSinh = new System.Windows.Forms.Label();
            this.lblLopCu = new System.Windows.Forms.Label();
            this.lblHocKy = new System.Windows.Forms.Label();
            this.lblChonLopMoi = new System.Windows.Forms.Label();
            this.cbLopMoi = new System.Windows.Forms.ComboBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblHocSinh);
            this.panelMain.Controls.Add(this.lblLopCu);
            this.panelMain.Controls.Add(this.lblHocKy);
            this.panelMain.Controls.Add(this.lblChonLopMoi);
            this.panelMain.Controls.Add(this.cbLopMoi);
            this.panelMain.Controls.Add(this.lblLyDo);
            this.panelMain.Controls.Add(this.txtLyDo);
            this.panelMain.Controls.Add(this.btnXacNhan);
            this.panelMain.Controls.Add(this.btnHuy);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(492, 395);
            this.panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chuyển lớp";
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
            // lblLopCu
            // 
            this.lblLopCu.AutoSize = true;
            this.lblLopCu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLopCu.Location = new System.Drawing.Point(25, 95);
            this.lblLopCu.Name = "lblLopCu";
            this.lblLopCu.Size = new System.Drawing.Size(53, 19);
            this.lblLopCu.TabIndex = 2;
            this.lblLopCu.Text = "Lớp cũ:";
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
            // lblChonLopMoi
            // 
            this.lblChonLopMoi.AutoSize = true;
            this.lblChonLopMoi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblChonLopMoi.Location = new System.Drawing.Point(25, 165);
            this.lblChonLopMoi.Name = "lblChonLopMoi";
            this.lblChonLopMoi.Size = new System.Drawing.Size(99, 19);
            this.lblChonLopMoi.TabIndex = 4;
            this.lblChonLopMoi.Text = "Chọn lớp mới:";
            // 
            // cbLopMoi
            // 
            this.cbLopMoi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLopMoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLopMoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLopMoi.ForeColor = System.Drawing.Color.Black;
            this.cbLopMoi.FormattingEnabled = true;
            this.cbLopMoi.ItemHeight = 30;
            this.cbLopMoi.Location = new System.Drawing.Point(25, 190);
            this.cbLopMoi.Name = "cbLopMoi";
            this.cbLopMoi.Size = new System.Drawing.Size(450, 36);
            this.cbLopMoi.TabIndex = 5;
            this.cbLopMoi.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLopMoi_DrawItem);
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLyDo.ForeColor = System.Drawing.Color.Gray;
            this.lblLyDo.Location = new System.Drawing.Point(25, 240);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(111, 19);
            this.lblLyDo.TabIndex = 6;
            this.lblLyDo.Text = "Lý do (tùy chọn):";
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
            this.txtLyDo.Location = new System.Drawing.Point(25, 265);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.PlaceholderText = "Nhập lý do chuyển lớp (tùy chọn)...";
            this.txtLyDo.SelectedText = "";
            this.txtLyDo.Size = new System.Drawing.Size(450, 40);
            this.txtLyDo.TabIndex = 7;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BorderRadius = 5;
            this.btnXacNhan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(259, 320);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(113, 46);
            this.btnXacNhan.TabIndex = 8;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 5;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(390, 320);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(85, 46);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FormChuyenLop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 395);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChuyenLop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chuyển lớp";
            this.Load += new System.EventHandler(this.FormChuyenLop_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
