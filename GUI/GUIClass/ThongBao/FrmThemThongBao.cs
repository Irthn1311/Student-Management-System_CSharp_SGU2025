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
                
                // Lọc bỏ "Khen thưởng" và "Kỷ luật" vì đã có giao diện riêng
                var danhSachFiltered = danhSach
                    .Where(loai => 
                        !loai.TenLoai.Equals("Khen thưởng", StringComparison.OrdinalIgnoreCase) &&
                        !loai.TenLoai.Equals("Kỷ luật", StringComparison.OrdinalIgnoreCase) &&
                        !loai.MaLoai.Equals("KHEN_THUONG", StringComparison.OrdinalIgnoreCase) &&
                        !loai.MaLoai.Equals("KY_LUAT", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                
                cbLoaiThongBao.DataSource = danhSachFiltered;
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
                cbKhoi.DataSource = null; // Clear DataSource trước
                
                var danhSachKhoi = lopHocBUS.LayDanhSachKhoiLop();
                if (danhSachKhoi != null && danhSachKhoi.Count > 0)
                {
                    // Loại bỏ duplicate và sắp xếp
                    var khoiUnique = danhSachKhoi
                        .GroupBy(k => k.MaKhoi)
                        .Select(g => g.First())
                        .OrderBy(k => k.MaKhoi)
                        .ToList();
                    
                    // Giới hạn số lượng items để tránh lỗi
                    if (khoiUnique.Count > 100)
                    {
                        MessageBox.Show("Cảnh báo: Có quá nhiều khối lớp trong hệ thống!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    foreach (var khoi in khoiUnique)
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
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(30, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(52, 15);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tiêu đề:";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.BorderRadius = 5;
            this.txtTieuDe.BorderThickness = 2;
            this.txtTieuDe.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTieuDe.DefaultText = "";
            this.txtTieuDe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTieuDe.Location = new System.Drawing.Point(120, 25);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.PlaceholderText = "Nhập tiêu đề thông báo...";
            this.txtTieuDe.SelectedText = "";
            this.txtTieuDe.Size = new System.Drawing.Size(550, 35);
            this.txtTieuDe.TabIndex = 1;
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNoiDung.Location = new System.Drawing.Point(30, 80);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(60, 15);
            this.lblNoiDung.TabIndex = 2;
            this.lblNoiDung.Text = "Nội dung:";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.BorderRadius = 5;
            this.txtNoiDung.BorderThickness = 2;
            this.txtNoiDung.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNoiDung.DefaultText = "";
            this.txtNoiDung.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNoiDung.Location = new System.Drawing.Point(120, 75);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.PlaceholderText = "Nhập nội dung thông báo...";
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoiDung.SelectedText = "";
            this.txtNoiDung.Size = new System.Drawing.Size(550, 150);
            this.txtNoiDung.TabIndex = 3;
            // 
            // lblLoaiThongBao
            // 
            this.lblLoaiThongBao.AutoSize = true;
            this.lblLoaiThongBao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoaiThongBao.Location = new System.Drawing.Point(30, 250);
            this.lblLoaiThongBao.Name = "lblLoaiThongBao";
            this.lblLoaiThongBao.Size = new System.Drawing.Size(91, 15);
            this.lblLoaiThongBao.TabIndex = 4;
            this.lblLoaiThongBao.Text = "Loại thông báo:";
            // 
            // cbLoaiThongBao
            // 
            this.cbLoaiThongBao.BackColor = System.Drawing.Color.Transparent;
            this.cbLoaiThongBao.BorderRadius = 3;
            this.cbLoaiThongBao.BorderThickness = 2;
            this.cbLoaiThongBao.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLoaiThongBao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoaiThongBao.FocusedColor = System.Drawing.Color.Empty;
            this.cbLoaiThongBao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLoaiThongBao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLoaiThongBao.ItemHeight = 30;
            this.cbLoaiThongBao.Location = new System.Drawing.Point(150, 245);
            this.cbLoaiThongBao.Name = "cbLoaiThongBao";
            this.cbLoaiThongBao.Size = new System.Drawing.Size(250, 36);
            this.cbLoaiThongBao.TabIndex = 5;
            // 
            // lblPhamVi
            // 
            this.lblPhamVi.AutoSize = true;
            this.lblPhamVi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhamVi.Location = new System.Drawing.Point(30, 300);
            this.lblPhamVi.Name = "lblPhamVi";
            this.lblPhamVi.Size = new System.Drawing.Size(54, 15);
            this.lblPhamVi.TabIndex = 6;
            this.lblPhamVi.Text = "Phạm vi:";
            // 
            // cbPhamVi
            // 
            this.cbPhamVi.BackColor = System.Drawing.Color.Transparent;
            this.cbPhamVi.BorderRadius = 3;
            this.cbPhamVi.BorderThickness = 2;
            this.cbPhamVi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPhamVi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPhamVi.FocusedColor = System.Drawing.Color.Empty;
            this.cbPhamVi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbPhamVi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbPhamVi.ItemHeight = 30;
            this.cbPhamVi.Location = new System.Drawing.Point(120, 295);
            this.cbPhamVi.Name = "cbPhamVi";
            this.cbPhamVi.Size = new System.Drawing.Size(250, 36);
            this.cbPhamVi.TabIndex = 7;
            // 
            // lblDoUuTien
            // 
            this.lblDoUuTien.AutoSize = true;
            this.lblDoUuTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDoUuTien.Location = new System.Drawing.Point(30, 400);
            this.lblDoUuTien.Name = "lblDoUuTien";
            this.lblDoUuTien.Size = new System.Drawing.Size(69, 15);
            this.lblDoUuTien.TabIndex = 8;
            this.lblDoUuTien.Text = "Độ ưu tiên:";
            // 
            // cbDoUuTien
            // 
            this.cbDoUuTien.BackColor = System.Drawing.Color.Transparent;
            this.cbDoUuTien.BorderRadius = 3;
            this.cbDoUuTien.BorderThickness = 2;
            this.cbDoUuTien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbDoUuTien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDoUuTien.FocusedColor = System.Drawing.Color.Empty;
            this.cbDoUuTien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbDoUuTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbDoUuTien.ItemHeight = 30;
            this.cbDoUuTien.Location = new System.Drawing.Point(120, 395);
            this.cbDoUuTien.Name = "cbDoUuTien";
            this.cbDoUuTien.Size = new System.Drawing.Size(250, 36);
            this.cbDoUuTien.TabIndex = 9;
            // 
            // lblNgayHetHan
            // 
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgayHetHan.Location = new System.Drawing.Point(30, 450);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new System.Drawing.Size(83, 15);
            this.lblNgayHetHan.TabIndex = 10;
            this.lblNgayHetHan.Text = "Ngày hết hạn:";
            // 
            // chkCoHetHan
            // 
            this.chkCoHetHan.AutoSize = true;
            this.chkCoHetHan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkCoHetHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCoHetHan.Location = new System.Drawing.Point(120, 450);
            this.chkCoHetHan.Name = "chkCoHetHan";
            this.chkCoHetHan.Size = new System.Drawing.Size(106, 24);
            this.chkCoHetHan.TabIndex = 11;
            this.chkCoHetHan.Text = "Có hết hạn";
            this.chkCoHetHan.UseVisualStyleBackColor = false;
            this.chkCoHetHan.CheckedChanged += new System.EventHandler(this.ChkCoHetHan_CheckedChanged);
            // 
            // dtpNgayHetHan
            // 
            this.dtpNgayHetHan.Enabled = false;
            this.dtpNgayHetHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHetHan.Location = new System.Drawing.Point(232, 449);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new System.Drawing.Size(200, 31);
            this.dtpNgayHetHan.TabIndex = 12;
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.AutoSize = true;
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVaiTro.Location = new System.Drawing.Point(30, 350);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(46, 15);
            this.lblVaiTro.TabIndex = 13;
            this.lblVaiTro.Text = "Vai trò:";
            this.lblVaiTro.Visible = false;
            // 
            // cbVaiTro
            // 
            this.cbVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.cbVaiTro.BorderRadius = 3;
            this.cbVaiTro.BorderThickness = 2;
            this.cbVaiTro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVaiTro.FocusedColor = System.Drawing.Color.Empty;
            this.cbVaiTro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbVaiTro.ItemHeight = 30;
            this.cbVaiTro.Location = new System.Drawing.Point(120, 345);
            this.cbVaiTro.Name = "cbVaiTro";
            this.cbVaiTro.Size = new System.Drawing.Size(250, 36);
            this.cbVaiTro.TabIndex = 14;
            this.cbVaiTro.Visible = false;
            // 
            // lblLop
            // 
            this.lblLop.AutoSize = true;
            this.lblLop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLop.Location = new System.Drawing.Point(30, 350);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(31, 15);
            this.lblLop.TabIndex = 15;
            this.lblLop.Text = "Lớp:";
            this.lblLop.Visible = false;
            // 
            // cbLop
            // 
            this.cbLop.BackColor = System.Drawing.Color.Transparent;
            this.cbLop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLop.FocusedColor = System.Drawing.Color.Empty;
            this.cbLop.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbLop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbLop.ItemHeight = 30;
            this.cbLop.Location = new System.Drawing.Point(120, 345);
            this.cbLop.Name = "cbLop";
            this.cbLop.Size = new System.Drawing.Size(250, 36);
            this.cbLop.TabIndex = 16;
            this.cbLop.Visible = false;
            // 
            // lblKhoi
            // 
            this.lblKhoi.AutoSize = true;
            this.lblKhoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKhoi.Location = new System.Drawing.Point(30, 350);
            this.lblKhoi.Name = "lblKhoi";
            this.lblKhoi.Size = new System.Drawing.Size(35, 15);
            this.lblKhoi.TabIndex = 17;
            this.lblKhoi.Text = "Khối:";
            this.lblKhoi.Visible = false;
            // 
            // cbKhoi
            // 
            this.cbKhoi.BackColor = System.Drawing.Color.Transparent;
            this.cbKhoi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbKhoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKhoi.FocusedColor = System.Drawing.Color.Empty;
            this.cbKhoi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbKhoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbKhoi.ItemHeight = 30;
            this.cbKhoi.Location = new System.Drawing.Point(120, 345);
            this.cbKhoi.Name = "cbKhoi";
            this.cbKhoi.Size = new System.Drawing.Size(250, 36);
            this.cbKhoi.TabIndex = 18;
            this.cbKhoi.Visible = false;
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 5;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(450, 550);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 40);
            this.btnLuu.TabIndex = 19;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.BorderColor = System.Drawing.Color.Red;
            this.btnHuy.BorderRadius = 5;
            this.btnHuy.BorderThickness = 2;
            this.btnHuy.FillColor = System.Drawing.Color.White;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.Red;
            this.btnHuy.Location = new System.Drawing.Point(570, 550);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 40);
            this.btnHuy.TabIndex = 20;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // FrmThemThongBao
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(700, 650);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmThemThongBao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm thông báo";
            this.Load += new System.EventHandler(this.FrmThemThongBao_Load);
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
