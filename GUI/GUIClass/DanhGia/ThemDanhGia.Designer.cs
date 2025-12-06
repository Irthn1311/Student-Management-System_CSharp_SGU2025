namespace Student_Management_System_CSharp_SGU2025.GUI
{
    partial class ThemDanhGia
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbHocSinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbLoaiDanhGia = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtNoiDung = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cbCapKhenThuong = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbMucXuLy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.ngayApDung = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.cbHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 57);
            this.panel1.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thêm đánh giá";
            // 
            // cbLop
            // 
            this.cbLop.BackColor = System.Drawing.Color.Transparent;
            this.cbLop.BorderRadius = 5;
            this.cbLop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLop.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLop.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLop.ItemHeight = 30;
            this.cbLop.Items.AddRange(new object[] {
            "Chọn lớp học",
            "Lớp 6A1",
            "Lớp 7A2"});
            this.cbLop.Location = new System.Drawing.Point(81, 154);
            this.cbLop.Margin = new System.Windows.Forms.Padding(2);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(341, 36);
            this.cbLop.StartIndex = 0;
            this.cbLop.TabIndex = 49;
            this.cbLop.SelectedIndexChanged += new System.EventHandler(this.cbLop_SelectedIndexChanged);
            // 
            // cbHocSinh
            // 
            this.cbHocSinh.BackColor = System.Drawing.Color.Transparent;
            this.cbHocSinh.BorderRadius = 5;
            this.cbHocSinh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHocSinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocSinh.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocSinh.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocSinh.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHocSinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbHocSinh.ItemHeight = 30;
            this.cbHocSinh.Items.AddRange(new object[] {
            "Chọn học sinh",
            "Lớp 6A1",
            "Lớp 7A2"});
            this.cbHocSinh.Location = new System.Drawing.Point(81, 210);
            this.cbHocSinh.Margin = new System.Windows.Forms.Padding(2);
            this.cbHocSinh.Name = "cbHocSinh";
            this.cbHocSinh.Size = new System.Drawing.Size(341, 36);
            this.cbHocSinh.StartIndex = 0;
            this.cbHocSinh.TabIndex = 50;
            // 
            // cbLoaiDanhGia
            // 
            this.cbLoaiDanhGia.BackColor = System.Drawing.Color.Transparent;
            this.cbLoaiDanhGia.BorderRadius = 5;
            this.cbLoaiDanhGia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaiDanhGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoaiDanhGia.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLoaiDanhGia.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbLoaiDanhGia.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLoaiDanhGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLoaiDanhGia.ItemHeight = 30;
            this.cbLoaiDanhGia.Location = new System.Drawing.Point(81, 264);
            this.cbLoaiDanhGia.Margin = new System.Windows.Forms.Padding(2);
            this.cbLoaiDanhGia.Name = "cbLoaiDanhGia";
            this.cbLoaiDanhGia.Size = new System.Drawing.Size(341, 36);
            this.cbLoaiDanhGia.TabIndex = 51;
            this.cbLoaiDanhGia.SelectedIndexChanged += new System.EventHandler(this.cbLoaiDanhGia_SelectedIndexChanged);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNoiDung.DefaultText = "";
            this.txtNoiDung.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNoiDung.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNoiDung.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNoiDung.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNoiDung.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNoiDung.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNoiDung.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNoiDung.Location = new System.Drawing.Point(81, 370);
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.PlaceholderText = "";
            this.txtNoiDung.SelectedText = "";
            this.txtNoiDung.Size = new System.Drawing.Size(341, 69);
            this.txtNoiDung.TabIndex = 52;
            this.txtNoiDung.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(81, 326);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(132, 23);
            this.guna2HtmlLabel1.TabIndex = 53;
            this.guna2HtmlLabel1.Text = "Nội dung đánh giá";
            // 
            // cbCapKhenThuong
            // 
            this.cbCapKhenThuong.BackColor = System.Drawing.Color.Transparent;
            this.cbCapKhenThuong.BorderRadius = 5;
            this.cbCapKhenThuong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCapKhenThuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCapKhenThuong.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbCapKhenThuong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbCapKhenThuong.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCapKhenThuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbCapKhenThuong.ItemHeight = 30;
            this.cbCapKhenThuong.Items.AddRange(new object[] {
            "Cấp khen thưởng",
            "Cấp trường",
            "Cấp tỉnh",
            "Cấp thành phố",
            "Cấp quốc gia"});
            this.cbCapKhenThuong.Location = new System.Drawing.Point(81, 459);
            this.cbCapKhenThuong.Margin = new System.Windows.Forms.Padding(2);
            this.cbCapKhenThuong.Name = "cbCapKhenThuong";
            this.cbCapKhenThuong.Size = new System.Drawing.Size(341, 36);
            this.cbCapKhenThuong.StartIndex = 0;
            this.cbCapKhenThuong.TabIndex = 54;
            // 
            // cbMucXuLy
            // 
            this.cbMucXuLy.BackColor = System.Drawing.Color.Transparent;
            this.cbMucXuLy.BorderRadius = 5;
            this.cbMucXuLy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMucXuLy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMucXuLy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMucXuLy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMucXuLy.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMucXuLy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbMucXuLy.ItemHeight = 30;
            this.cbMucXuLy.Items.AddRange(new object[] {
            "Mức xử lý",
            "Nhắc nhở",
            "Cảnh cáo",
            "Khiển trách",
            "Kỷ luật"});
            this.cbMucXuLy.Location = new System.Drawing.Point(80, 459);
            this.cbMucXuLy.Margin = new System.Windows.Forms.Padding(2);
            this.cbMucXuLy.Name = "cbMucXuLy";
            this.cbMucXuLy.Size = new System.Drawing.Size(341, 36);
            this.cbMucXuLy.StartIndex = 0;
            this.cbMucXuLy.TabIndex = 55;
            this.cbMucXuLy.SelectedIndexChanged += new System.EventHandler(this.cbMucXuLy_SelectedIndexChanged);
            // 
            // ngayApDung
            // 
            this.ngayApDung.Checked = true;
            this.ngayApDung.FillColor = System.Drawing.Color.White;
            this.ngayApDung.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ngayApDung.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.ngayApDung.Location = new System.Drawing.Point(81, 544);
            this.ngayApDung.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.ngayApDung.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.ngayApDung.Name = "ngayApDung";
            this.ngayApDung.Size = new System.Drawing.Size(341, 36);
            this.ngayApDung.TabIndex = 56;
            this.ngayApDung.Value = new System.DateTime(2025, 10, 31, 15, 41, 32, 729);
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(81, 500);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(101, 23);
            this.guna2HtmlLabel2.TabIndex = 57;
            this.guna2HtmlLabel2.Text = "Ngày áp dụng";
            // 
            // btnHuy
            // 
            this.btnHuy.BorderColor = System.Drawing.Color.Red;
            this.btnHuy.BorderRadius = 7;
            this.btnHuy.BorderThickness = 2;
            this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuy.FillColor = System.Drawing.Color.White;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.ForeColor = System.Drawing.Color.Red;
            this.btnHuy.ImageSize = new System.Drawing.Size(15, 15);
            this.btnHuy.Location = new System.Drawing.Point(220, 657);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(53, 29);
            this.btnHuy.TabIndex = 59;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BorderRadius = 7;
            this.btnXacNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Image = global::Student_Management_System_CSharp_SGU2025.GUI.Properties.Resources.notes;
            this.btnXacNhan.ImageSize = new System.Drawing.Size(15, 15);
            this.btnXacNhan.Location = new System.Drawing.Point(345, 657);
            this.btnXacNhan.Margin = new System.Windows.Forms.Padding(2);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(122, 29);
            this.btnXacNhan.TabIndex = 60;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // cbHocKy
            // 
            this.cbHocKy.BackColor = System.Drawing.Color.Transparent;
            this.cbHocKy.BorderRadius = 5;
            this.cbHocKy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHocKy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocKy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbHocKy.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbHocKy.ItemHeight = 30;
            this.cbHocKy.Items.AddRange(new object[] {
            "Chọn học kỳ",
            "PH01 - Nguyễn Văn Tèo",
            "PH02 - Nguyễn Đình C",
            "PH03 - Huỳnh Mĩ A",
            "PH04 - Bùi Trường C"});
            this.cbHocKy.Location = new System.Drawing.Point(80, 98);
            this.cbHocKy.Margin = new System.Windows.Forms.Padding(2);
            this.cbHocKy.Name = "cbHocKy";
            this.cbHocKy.Size = new System.Drawing.Size(342, 36);
            this.cbHocKy.StartIndex = 0;
            this.cbHocKy.TabIndex = 61;
            this.cbHocKy.SelectedIndexChanged += new System.EventHandler(this.cbHocKy_SelectedIndexChanged);
            // 
            // ThemDanhGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 697);
            this.Controls.Add(this.cbHocKy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.ngayApDung);
            this.Controls.Add(this.cbMucXuLy);
            this.Controls.Add(this.cbCapKhenThuong);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.cbLoaiDanhGia);
            this.Controls.Add(this.cbHocSinh);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.panel1);
            this.Name = "ThemDanhGia";
            this.Text = "ThemKhenThuong";
            this.Load += new System.EventHandler(this.ThemKhenThuong_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cbLop;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocSinh;
        private Guna.UI2.WinForms.Guna2ComboBox cbLoaiDanhGia;
        private Guna.UI2.WinForms.Guna2TextBox txtNoiDung;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2ComboBox cbCapKhenThuong;
        private Guna.UI2.WinForms.Guna2ComboBox cbMucXuLy;
        private Guna.UI2.WinForms.Guna2DateTimePicker ngayApDung;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnXacNhan;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocKy;
    }
}