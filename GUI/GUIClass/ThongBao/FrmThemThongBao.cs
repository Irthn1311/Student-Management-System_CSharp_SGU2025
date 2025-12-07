using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThongBao
{
    /// <summary>
    /// Form thêm/sửa thông báo
    /// </summary>
    public partial class FrmThemThongBao : Form
    {
        private ThongBaoBUS thongBaoBUS;
        private LopHocBUS lopHocBUS;
        private ThongBaoDTO thongBaoEdit;
        private bool isEditMode;

        // ComboBox items helper class
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }

        public FrmThemThongBao()
        {
            InitializeComponent();
            Initialize(false, null);
        }

        public FrmThemThongBao(ThongBaoDTO thongBao)
        {
            InitializeComponent();
            Initialize(true, thongBao);
        }

        private void Initialize(bool editMode, ThongBaoDTO thongBao)
        {
            thongBaoBUS = new ThongBaoBUS();
            lopHocBUS = new LopHocBUS();
            isEditMode = editMode;
            thongBaoEdit = thongBao;
        }

        private void FrmThemThongBao_Load(object sender, EventArgs e)
        {
            LoadLoaiThongBao();
            LoadPhamVi();
            LoadDoUuTien();
            LoadVaiTro();
            LoadLop();
            LoadKhoi();

            // Ẩn các control phụ lúc đầu
            UpdatePhamViControls();

            if (isEditMode && thongBaoEdit != null)
            {
                this.Text = "Sửa thông báo";
                LoadDataForEdit();
            }
            else
            {
                this.Text = "Thêm thông báo";
            }
        }

        #region Load Data

        private void LoadLoaiThongBao()
        {
            try
            {
                var danhSach = thongBaoBUS.LayDanhSachLoaiThongBao();
                cbLoaiThongBao.DataSource = danhSach;
                cbLoaiThongBao.DisplayMember = "TenLoai";
                cbLoaiThongBao.ValueMember = "MaLoai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load loại thông báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPhamVi()
        {
            cbPhamVi.Items.Clear();
            cbPhamVi.Items.Add("Toàn trường");
            cbPhamVi.Items.Add("Theo vai trò");
            cbPhamVi.Items.Add("Theo lớp");
            cbPhamVi.Items.Add("Theo khối");
            cbPhamVi.SelectedIndex = 0;
            cbPhamVi.SelectedIndexChanged += CbPhamVi_SelectedIndexChanged;
        }

        private void LoadDoUuTien()
        {
            cbDoUuTien.Items.Clear();
            cbDoUuTien.Items.Add("Bình thường");
            cbDoUuTien.Items.Add("Quan trọng");
            cbDoUuTien.Items.Add("Khẩn cấp");
            cbDoUuTien.SelectedIndex = 0;
        }

        private void LoadVaiTro()
        {
            try
            {
                cbVaiTro.Items.Clear();
                cbVaiTro.Items.Add(new ComboBoxItem { Text = "Giáo viên", Value = "teacher" });
                cbVaiTro.Items.Add(new ComboBoxItem { Text = "Học sinh", Value = "student" });
                cbVaiTro.Items.Add(new ComboBoxItem { Text = "Phụ huynh", Value = "parent" });
                cbVaiTro.DisplayMember = "Text";
                cbVaiTro.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load vai trò: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLop()
        {
            try
            {
                cbLop.Items.Clear();
                var danhSachLop = lopHocBUS.DocDSLop();
                if (danhSachLop != null && danhSachLop.Count > 0)
                {
                    foreach (var lop in danhSachLop.OrderBy(l => l.tenLop))
                    {
                        cbLop.Items.Add(new ComboBoxItem
                        {
                            Text = lop.tenLop,
                            Value = lop.maLop
                        });
                    }
                }
                cbLop.DisplayMember = "Text";
                cbLop.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKhoi()
        {
            try
            {
                cbKhoi.Items.Clear();
                var danhSachKhoi = lopHocBUS.LayDanhSachKhoiLop();
                if (danhSachKhoi != null && danhSachKhoi.Count > 0)
                {
                    foreach (var khoi in danhSachKhoi.OrderBy(k => k.MaKhoi))
                    {
                        cbKhoi.Items.Add(new ComboBoxItem
                        {
                            Text = khoi.TenKhoi,
                            Value = khoi.MaKhoi
                        });
                    }
                }
                cbKhoi.DisplayMember = "Text";
                cbKhoi.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách khối: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataForEdit()
        {
            txtTieuDe.Text = thongBaoEdit.TieuDe;
            txtNoiDung.Text = thongBaoEdit.NoiDung;

            // Set loại thông báo
            if (!string.IsNullOrEmpty(thongBaoEdit.LoaiThongBao))
            {
                cbLoaiThongBao.SelectedValue = thongBaoEdit.LoaiThongBao;
            }

            // Set phạm vi
            switch (thongBaoEdit.PhamVi)
            {
                case "ALL": cbPhamVi.SelectedIndex = 0; break;
                case "VAI_TRO": cbPhamVi.SelectedIndex = 1; break;
                case "LOP": cbPhamVi.SelectedIndex = 2; break;
                case "KHOI": cbPhamVi.SelectedIndex = 3; break;
            }

            // Set độ ưu tiên
            switch (thongBaoEdit.DoUuTien)
            {
                case "BINH_THUONG": cbDoUuTien.SelectedIndex = 0; break;
                case "QUAN_TRONG": cbDoUuTien.SelectedIndex = 1; break;
                case "KHAN_CAP": cbDoUuTien.SelectedIndex = 2; break;
            }

            // Set giá trị phụ theo phạm vi
            if (thongBaoEdit.PhamVi == "VAI_TRO" && !string.IsNullOrEmpty(thongBaoEdit.MaVaiTroNhan))
            {
                foreach (ComboBoxItem item in cbVaiTro.Items)
                {
                    if (item.Value.ToString() == thongBaoEdit.MaVaiTroNhan)
                    {
                        cbVaiTro.SelectedItem = item;
                        break;
                    }
                }
            }
            else if (thongBaoEdit.PhamVi == "LOP" && thongBaoEdit.MaLop.HasValue)
            {
                foreach (ComboBoxItem item in cbLop.Items)
                {
                    if (Convert.ToInt32(item.Value) == thongBaoEdit.MaLop.Value)
                    {
                        cbLop.SelectedItem = item;
                        break;
                    }
                }
            }
            else if (thongBaoEdit.PhamVi == "KHOI" && thongBaoEdit.MaKhoi.HasValue)
            {
                foreach (ComboBoxItem item in cbKhoi.Items)
                {
                    if (Convert.ToInt32(item.Value) == thongBaoEdit.MaKhoi.Value)
                    {
                        cbKhoi.SelectedItem = item;
                        break;
                    }
                }
            }

            if (thongBaoEdit.NgayHetHan.HasValue)
            {
                chkCoHetHan.Checked = true;
                dtpNgayHetHan.Value = thongBaoEdit.NgayHetHan.Value;
            }
        }

        #endregion

        #region Event Handlers

        private void CbPhamVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePhamViControls();
        }

        private void UpdatePhamViControls()
        {
            // Ẩn tất cả các control phụ
            lblVaiTro.Visible = false;
            cbVaiTro.Visible = false;
            lblLop.Visible = false;
            cbLop.Visible = false;
            lblKhoi.Visible = false;
            cbKhoi.Visible = false;

            // Hiện control tương ứng với phạm vi được chọn
            switch (cbPhamVi.SelectedIndex)
            {
                case 1: // Theo vai trò
                    lblVaiTro.Visible = true;
                    cbVaiTro.Visible = true;
                    break;
                case 2: // Theo lớp
                    lblLop.Visible = true;
                    cbLop.Visible = true;
                    break;
                case 3: // Theo khối
                    lblKhoi.Visible = true;
                    cbKhoi.Visible = true;
                    break;
            }
        }

        private void ChkCoHetHan_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgayHetHan.Enabled = chkCoHetHan.Checked;
        }

        #endregion

        #region Save

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (!ValidateInput())
                {
                    return;
                }

                // Tạo DTO
                var thongBao = new ThongBaoDTO
                {
                    TieuDe = txtTieuDe.Text.Trim(),
                    NoiDung = txtNoiDung.Text.Trim(),
                    LoaiThongBao = cbLoaiThongBao.SelectedValue?.ToString(),
                    MaNguoiTao = SessionManager.TenDangNhap,
                    NgayHetHan = chkCoHetHan.Checked ? dtpNgayHetHan.Value : (DateTime?)null
                };

                // Set phạm vi và giá trị phụ
                switch (cbPhamVi.SelectedIndex)
                {
                    case 0: // Toàn trường
                        thongBao.PhamVi = "ALL";
                        break;
                    case 1: // Theo vai trò
                        thongBao.PhamVi = "VAI_TRO";
                        if (cbVaiTro.SelectedItem != null)
                        {
                            thongBao.MaVaiTroNhan = ((ComboBoxItem)cbVaiTro.SelectedItem).Value.ToString();
                        }
                        break;
                    case 2: // Theo lớp
                        thongBao.PhamVi = "LOP";
                        if (cbLop.SelectedItem != null)
                        {
                            thongBao.MaLop = Convert.ToInt32(((ComboBoxItem)cbLop.SelectedItem).Value);
                        }
                        break;
                    case 3: // Theo khối
                        thongBao.PhamVi = "KHOI";
                        if (cbKhoi.SelectedItem != null)
                        {
                            thongBao.MaKhoi = Convert.ToInt32(((ComboBoxItem)cbKhoi.SelectedItem).Value);
                        }
                        break;
                }

                // Set độ ưu tiên
                switch (cbDoUuTien.SelectedIndex)
                {
                    case 0: thongBao.DoUuTien = "BINH_THUONG"; break;
                    case 1: thongBao.DoUuTien = "QUAN_TRONG"; break;
                    case 2: thongBao.DoUuTien = "KHAN_CAP"; break;
                }

                // Tạo đối tượng nhận text
                thongBao.DoiTuongNhan = thongBaoBUS.TaoTextDoiTuongNhan(
                    thongBao.PhamVi,
                    thongBao.MaVaiTroNhan,
                    thongBao.MaLop,
                    thongBao.MaKhoi
                );

                OperationResult result;
                if (isEditMode)
                {
                    thongBao.MaThongBao = thongBaoEdit.MaThongBao;
                    result = thongBaoBUS.CapNhatThongBao(thongBao);
                }
                else
                {
                    result = thongBaoBUS.ThemThongBao(thongBao);
                }

                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Kiểm tra tiêu đề
            if (string.IsNullOrWhiteSpace(txtTieuDe.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề thông báo!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTieuDe.Focus();
                return false;
            }

            // Kiểm tra nội dung
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung thông báo!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoiDung.Focus();
                return false;
            }

            // Kiểm tra loại thông báo
            if (cbLoaiThongBao.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại thông báo!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoaiThongBao.Focus();
                return false;
            }

            // Kiểm tra phạm vi phụ
            if (cbPhamVi.SelectedIndex == 1 && cbVaiTro.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò nhận thông báo!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbVaiTro.Focus();
                return false;
            }

            if (cbPhamVi.SelectedIndex == 2 && cbLop.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn lớp nhận thông báo!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLop.Focus();
                return false;
            }

            if (cbPhamVi.SelectedIndex == 3 && cbKhoi.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn khối nhận thông báo!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbKhoi.Focus();
                return false;
            }

            // Kiểm tra ngày hết hạn
            if (chkCoHetHan.Checked && dtpNgayHetHan.Value < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày hết hạn không được nhỏ hơn ngày hiện tại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayHetHan.Focus();
                return false;
            }

            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Designer Code

        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2TextBox txtTieuDe;
        private Guna.UI2.WinForms.Guna2TextBox txtNoiDung;
        private Guna.UI2.WinForms.Guna2ComboBox cbLoaiThongBao;
        private Guna.UI2.WinForms.Guna2ComboBox cbPhamVi;
        private Guna.UI2.WinForms.Guna2ComboBox cbDoUuTien;
        private Guna.UI2.WinForms.Guna2ComboBox cbVaiTro;
        private Guna.UI2.WinForms.Guna2ComboBox cbLop;
        private Guna.UI2.WinForms.Guna2ComboBox cbKhoi;
        private System.Windows.Forms.DateTimePicker dtpNgayHetHan;
        private System.Windows.Forms.CheckBox chkCoHetHan;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblNoiDung;
        private System.Windows.Forms.Label lblLoaiThongBao;
        private System.Windows.Forms.Label lblPhamVi;
        private System.Windows.Forms.Label lblDoUuTien;
        private System.Windows.Forms.Label lblNgayHetHan;
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.Label lblKhoi;

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.txtTieuDe = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.txtNoiDung = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLoaiThongBao = new System.Windows.Forms.Label();
            this.cbLoaiThongBao = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblPhamVi = new System.Windows.Forms.Label();
            this.cbPhamVi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblDoUuTien = new System.Windows.Forms.Label();
            this.cbDoUuTien = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.chkCoHetHan = new System.Windows.Forms.CheckBox();
            this.dtpNgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.cbVaiTro = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblLop = new System.Windows.Forms.Label();
            this.cbLop = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblKhoi = new System.Windows.Forms.Label();
            this.cbKhoi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();

            // Form
            this.ClientSize = new System.Drawing.Size(700, 650);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm thông báo";
            this.Load += new System.EventHandler(this.FrmThemThongBao_Load);

            // lblTieuDe
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(30, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(70, 16);
            this.lblTieuDe.Text = "Tiêu đề:";
            this.lblTieuDe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // txtTieuDe
            this.txtTieuDe.Location = new System.Drawing.Point(120, 25);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(550, 35);
            this.txtTieuDe.PlaceholderText = "Nhập tiêu đề thông báo...";

            // lblNoiDung
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Location = new System.Drawing.Point(30, 80);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(70, 16);
            this.lblNoiDung.Text = "Nội dung:";
            this.lblNoiDung.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // txtNoiDung
            this.txtNoiDung.Location = new System.Drawing.Point(120, 75);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(550, 150);
            this.txtNoiDung.PlaceholderText = "Nhập nội dung thông báo...";
            this.txtNoiDung.ScrollBars = ScrollBars.Vertical;

            // lblLoaiThongBao
            this.lblLoaiThongBao.AutoSize = true;
            this.lblLoaiThongBao.Location = new System.Drawing.Point(30, 250);
            this.lblLoaiThongBao.Name = "lblLoaiThongBao";
            this.lblLoaiThongBao.Size = new System.Drawing.Size(110, 16);
            this.lblLoaiThongBao.Text = "Loại thông báo:";
            this.lblLoaiThongBao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // cbLoaiThongBao
            this.cbLoaiThongBao.Location = new System.Drawing.Point(150, 245);
            this.cbLoaiThongBao.Name = "cbLoaiThongBao";
            this.cbLoaiThongBao.Size = new System.Drawing.Size(250, 35);

            // lblPhamVi
            this.lblPhamVi.AutoSize = true;
            this.lblPhamVi.Location = new System.Drawing.Point(30, 300);
            this.lblPhamVi.Name = "lblPhamVi";
            this.lblPhamVi.Size = new System.Drawing.Size(60, 16);
            this.lblPhamVi.Text = "Phạm vi:";
            this.lblPhamVi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // cbPhamVi
            this.cbPhamVi.Location = new System.Drawing.Point(120, 295);
            this.cbPhamVi.Name = "cbPhamVi";
            this.cbPhamVi.Size = new System.Drawing.Size(250, 35);

            // lblVaiTro
            this.lblVaiTro.AutoSize = true;
            this.lblVaiTro.Location = new System.Drawing.Point(30, 350);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(60, 16);
            this.lblVaiTro.Text = "Vai trò:";
            this.lblVaiTro.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblVaiTro.Visible = false;

            // cbVaiTro
            this.cbVaiTro.Location = new System.Drawing.Point(120, 345);
            this.cbVaiTro.Name = "cbVaiTro";
            this.cbVaiTro.Size = new System.Drawing.Size(250, 35);
            this.cbVaiTro.Visible = false;

            // lblLop
            this.lblLop.AutoSize = true;
            this.lblLop.Location = new System.Drawing.Point(30, 350);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(40, 16);
            this.lblLop.Text = "Lớp:";
            this.lblLop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblLop.Visible = false;

            // cbLop
            this.cbLop.Location = new System.Drawing.Point(120, 345);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(250, 35);
            this.cbLop.Visible = false;

            // lblKhoi
            this.lblKhoi.AutoSize = true;
            this.lblKhoi.Location = new System.Drawing.Point(30, 350);
            this.lblKhoi.Name = "lblKhoi";
            this.lblKhoi.Size = new System.Drawing.Size(45, 16);
            this.lblKhoi.Text = "Khối:";
            this.lblKhoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblKhoi.Visible = false;

            // cbKhoi
            this.cbKhoi.Location = new System.Drawing.Point(120, 345);
            this.cbKhoi.Name = "cbKhoi";
            this.cbKhoi.Size = new System.Drawing.Size(250, 35);
            this.cbKhoi.Visible = false;

            // lblDoUuTien
            this.lblDoUuTien.AutoSize = true;
            this.lblDoUuTien.Location = new System.Drawing.Point(30, 400);
            this.lblDoUuTien.Name = "lblDoUuTien";
            this.lblDoUuTien.Size = new System.Drawing.Size(80, 16);
            this.lblDoUuTien.Text = "Độ ưu tiên:";
            this.lblDoUuTien.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // cbDoUuTien
            this.cbDoUuTien.Location = new System.Drawing.Point(120, 395);
            this.cbDoUuTien.Name = "cbDoUuTien";
            this.cbDoUuTien.Size = new System.Drawing.Size(250, 35);

            // lblNgayHetHan
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Location = new System.Drawing.Point(30, 450);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new System.Drawing.Size(90, 16);
            this.lblNgayHetHan.Text = "Ngày hết hạn:";
            this.lblNgayHetHan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // chkCoHetHan
            this.chkCoHetHan.AutoSize = true;
            this.chkCoHetHan.Location = new System.Drawing.Point(120, 450);
            this.chkCoHetHan.Name = "chkCoHetHan";
            this.chkCoHetHan.Size = new System.Drawing.Size(100, 20);
            this.chkCoHetHan.Text = "Có hết hạn";
            this.chkCoHetHan.CheckedChanged += new System.EventHandler(this.ChkCoHetHan_CheckedChanged);

            // dtpNgayHetHan
            this.dtpNgayHetHan.Location = new System.Drawing.Point(230, 448);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new System.Drawing.Size(200, 22);
            this.dtpNgayHetHan.Enabled = false;
            this.dtpNgayHetHan.Format = DateTimePickerFormat.Short;

            // btnLuu
            this.btnLuu.Location = new System.Drawing.Point(450, 550);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 40);
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            // btnHuy
            this.btnHuy.Location = new System.Drawing.Point(570, 550);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 40);
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // Add controls
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.txtTieuDe);
            this.Controls.Add(this.lblNoiDung);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.lblLoaiThongBao);
            this.Controls.Add(this.cbLoaiThongBao);
            this.Controls.Add(this.lblPhamVi);
            this.Controls.Add(this.cbPhamVi);
            this.Controls.Add(this.lblDoUuTien);
            this.Controls.Add(this.cbDoUuTien);
            this.Controls.Add(this.lblNgayHetHan);
            this.Controls.Add(this.chkCoHetHan);
            this.Controls.Add(this.dtpNgayHetHan);
            this.Controls.Add(this.lblVaiTro);
            this.Controls.Add(this.cbVaiTro);
            this.Controls.Add(this.lblLop);
            this.Controls.Add(this.cbLop);
            this.Controls.Add(this.lblKhoi);
            this.Controls.Add(this.cbKhoi);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
