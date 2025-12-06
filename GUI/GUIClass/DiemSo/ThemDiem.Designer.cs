namespace Student_Management_System_CSharp_SGU2025.GUI.DiemSo
{
    partial class ThemDiem
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
            this.lblThemDiem = new System.Windows.Forms.Label();
            this.cbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbHocSinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtDiemTX = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiemGK = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.lbHovaTen = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDiemCK = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbMonHoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.panel1.Controls.Add(this.lblThemDiem);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 57);
            this.panel1.TabIndex = 2;
            // 
            // lblThemDiem
            // 
            this.lblThemDiem.AutoSize = true;
            this.lblThemDiem.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThemDiem.ForeColor = System.Drawing.Color.White;
            this.lblThemDiem.Location = new System.Drawing.Point(12, 16);
            this.lblThemDiem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblThemDiem.Name = "lblThemDiem";
            this.lblThemDiem.Size = new System.Drawing.Size(110, 25);
            this.lblThemDiem.TabIndex = 0;
            this.lblThemDiem.Text = "Thêm điểm";
            this.lblThemDiem.Click += new System.EventHandler(this.label1_Click);
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
            this.cbLop.Location = new System.Drawing.Point(44, 93);
            this.cbLop.Margin = new System.Windows.Forms.Padding(2);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(341, 36);
            this.cbLop.StartIndex = 0;
            this.cbLop.TabIndex = 33;
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
            "PH01 - Nguyễn Văn Tèo",
            "PH02 - Nguyễn Đình C",
            "PH03 - Huỳnh Mĩ A",
            "PH04 - Bùi Trường C"});
            this.cbHocSinh.Location = new System.Drawing.Point(44, 148);
            this.cbHocSinh.Margin = new System.Windows.Forms.Padding(2);
            this.cbHocSinh.Name = "cbHocSinh";
            this.cbHocSinh.Size = new System.Drawing.Size(341, 36);
            this.cbHocSinh.StartIndex = 0;
            this.cbHocSinh.TabIndex = 34;
            this.cbHocSinh.SelectedIndexChanged += new System.EventHandler(this.cbHocSinh_SelectedIndexChanged);
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
            this.cbHocKy.Location = new System.Drawing.Point(44, 201);
            this.cbHocKy.Margin = new System.Windows.Forms.Padding(2);
            this.cbHocKy.Name = "cbHocKy";
            this.cbHocKy.Size = new System.Drawing.Size(342, 36);
            this.cbHocKy.StartIndex = 0;
            this.cbHocKy.TabIndex = 35;
            this.cbHocKy.SelectedIndexChanged += new System.EventHandler(this.cbHocKy_SelectedIndexChanged);
            // 
            // txtDiemTX
            // 
            this.txtDiemTX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemTX.DefaultText = "";
            this.txtDiemTX.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemTX.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemTX.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemTX.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemTX.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemTX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiemTX.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemTX.Location = new System.Drawing.Point(156, 310);
            this.txtDiemTX.Name = "txtDiemTX";
            this.txtDiemTX.PlaceholderText = "";
            this.txtDiemTX.SelectedText = "";
            this.txtDiemTX.Size = new System.Drawing.Size(229, 32);
            this.txtDiemTX.TabIndex = 36;
            this.txtDiemTX.TextChanged += new System.EventHandler(this.txtDiemTX_TextChanged);
            // 
            // txtDiemGK
            // 
            this.txtDiemGK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemGK.DefaultText = "";
            this.txtDiemGK.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemGK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemGK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemGK.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemGK.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemGK.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiemGK.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemGK.Location = new System.Drawing.Point(157, 359);
            this.txtDiemGK.Name = "txtDiemGK";
            this.txtDiemGK.PlaceholderText = "";
            this.txtDiemGK.SelectedText = "";
            this.txtDiemGK.Size = new System.Drawing.Size(229, 32);
            this.txtDiemGK.TabIndex = 37;
            this.txtDiemGK.TextChanged += new System.EventHandler(this.txtDiemGK_TextChanged);
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
            this.btnHuy.Location = new System.Drawing.Point(156, 489);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(53, 29);
            this.btnHuy.TabIndex = 47;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 7;
            this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Image = global::Student_Management_System_CSharp_SGU2025.GUI.Properties.Resources.notes;
            this.btnLuu.ImageSize = new System.Drawing.Size(15, 15);
            this.btnLuu.Location = new System.Drawing.Point(241, 489);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(122, 29);
            this.btnLuu.TabIndex = 48;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // lbHovaTen
            // 
            this.lbHovaTen.AutoSize = true;
            this.lbHovaTen.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHovaTen.Location = new System.Drawing.Point(40, 322);
            this.lbHovaTen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbHovaTen.Name = "lbHovaTen";
            this.lbHovaTen.Size = new System.Drawing.Size(73, 20);
            this.lbHovaTen.TabIndex = 49;
            this.lbHovaTen.Text = "Điểm TX :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 371);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 50;
            this.label2.Text = "Điểm GK :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 420);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 51;
            this.label3.Text = "Điểm CK :";
            // 
            // txtDiemCK
            // 
            this.txtDiemCK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemCK.DefaultText = "";
            this.txtDiemCK.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemCK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemCK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemCK.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemCK.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemCK.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiemCK.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemCK.Location = new System.Drawing.Point(156, 408);
            this.txtDiemCK.Name = "txtDiemCK";
            this.txtDiemCK.PlaceholderText = "";
            this.txtDiemCK.SelectedText = "";
            this.txtDiemCK.Size = new System.Drawing.Size(229, 32);
            this.txtDiemCK.TabIndex = 38;
            this.txtDiemCK.TextChanged += new System.EventHandler(this.txtDiemCK_TextChanged);
            // 
            // cbMonHoc
            // 
            this.cbMonHoc.BackColor = System.Drawing.Color.Transparent;
            this.cbMonHoc.BorderRadius = 5;
            this.cbMonHoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonHoc.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMonHoc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbMonHoc.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMonHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbMonHoc.ItemHeight = 30;
            this.cbMonHoc.Items.AddRange(new object[] {
            "Chọn môn học",
            "PH01 - Nguyễn Văn Tèo",
            "PH02 - Nguyễn Đình C",
            "PH03 - Huỳnh Mĩ A",
            "PH04 - Bùi Trường C"});
            this.cbMonHoc.Location = new System.Drawing.Point(44, 254);
            this.cbMonHoc.Margin = new System.Windows.Forms.Padding(2);
            this.cbMonHoc.Name = "cbMonHoc";
            this.cbMonHoc.Size = new System.Drawing.Size(342, 36);
            this.cbMonHoc.StartIndex = 0;
            this.cbMonHoc.TabIndex = 52;
            this.cbMonHoc.SelectedIndexChanged += new System.EventHandler(this.cbMonHoc_SelectedIndexChanged);
            // 
            // ThemDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 542);
            this.Controls.Add(this.cbMonHoc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbHovaTen);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.txtDiemCK);
            this.Controls.Add(this.txtDiemGK);
            this.Controls.Add(this.txtDiemTX);
            this.Controls.Add(this.cbHocKy);
            this.Controls.Add(this.cbHocSinh);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.panel1);
            this.Name = "ThemDiem";
            this.Text = "ThemDiem";
            this.Load += new System.EventHandler(this.ThemDiem_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblThemDiem;
        private Guna.UI2.WinForms.Guna2ComboBox cbLop;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocSinh;
        private Guna.UI2.WinForms.Guna2ComboBox cbHocKy;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemTX;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemGK;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private System.Windows.Forms.Label lbHovaTen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemCK;
        private Guna.UI2.WinForms.Guna2ComboBox cbMonHoc;
    }
}