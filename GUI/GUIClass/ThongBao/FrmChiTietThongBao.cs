using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThongBao
{
    /// <summary>
    /// Form xem chi tiết thông báo
    /// </summary>
    public partial class FrmChiTietThongBao : Form
    {
        private ThongBaoBUS thongBaoBUS;
        private int maThongBao;
        private ThongBaoDTO thongBao;

        public FrmChiTietThongBao(int maThongBao)
        {
            InitializeComponent();
            this.maThongBao = maThongBao;
            thongBaoBUS = new ThongBaoBUS();
        }

        public FrmChiTietThongBao(ThongBaoDTO thongBao)
        {
            InitializeComponent();
            this.thongBao = thongBao;
            this.maThongBao = thongBao.MaThongBao;
            thongBaoBUS = new ThongBaoBUS();
        }

        private void FrmChiTietThongBao_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Nếu chưa có thông báo, load từ database
                if (thongBao == null)
                {
                    // Lấy chi tiết thông báo (tự động đánh dấu đã đọc)
                    thongBao = thongBaoBUS.LayChiTietThongBao(maThongBao, SessionManager.TenDangNhap);

                    if (thongBao == null)
                    {
                        MessageBox.Show("Không tìm thấy thông báo!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }
                }

                // Hiển thị thông tin - Đảm bảo các label được gán đúng
                lblTieuDeValue.Text = thongBao.TieuDe ?? "";
                lblLoaiThongBaoValue.Text = thongBao.TenLoaiThongBao ?? thongBao.LoaiThongBao ?? "N/A";
                lblDoiTuongNhanValue.Text = !string.IsNullOrEmpty(thongBao.DoiTuongNhan) ? thongBao.DoiTuongNhan : "N/A";
                lblNguoiTaoValue.Text = !string.IsNullOrEmpty(thongBao.TenNguoiTao) ? thongBao.TenNguoiTao : (thongBao.MaNguoiTao ?? "N/A");
                lblNgayTaoValue.Text = thongBao.NgayTao != null ? thongBao.NgayTao.ToString("dd/MM/yyyy HH:mm") : "N/A";
                lblDoUuTienValue.Text = thongBao.GetDoUuTienText();
                txtNoiDung.Text = thongBao.NoiDung ?? "";
                
                // Debug: Kiểm tra giá trị
                Console.WriteLine($"[DEBUG] TieuDe: {thongBao.TieuDe}");
                Console.WriteLine($"[DEBUG] DoiTuongNhan: {thongBao.DoiTuongNhan}");
                Console.WriteLine($"[DEBUG] NgayTao: {thongBao.NgayTao}");

                if (thongBao.NgayHetHan.HasValue)
                {
                    lblNgayHetHanValue.Text = thongBao.NgayHetHan.Value.ToString("dd/MM/yyyy");
                    
                    // Hiển thị cảnh báo nếu đã hết hạn
                    if (thongBao.IsExpired())
                    {
                        lblNgayHetHanValue.ForeColor = Color.Red;
                        lblNgayHetHanValue.Text += " (Đã hết hạn)";
                    }
                    else
                    {
                        lblNgayHetHanValue.ForeColor = Color.FromArgb(17, 24, 39);
                    }
                }
                else
                {
                    lblNgayHetHanValue.Text = "Không có hạn";
                    lblNgayHetHanValue.ForeColor = Color.FromArgb(107, 114, 128);
                }

                // Màu sắc cho độ ưu tiên
                switch (thongBao.DoUuTien)
                {
                    case "KHAN_CAP":
                        lblDoUuTienValue.ForeColor = Color.Red;
                        break;
                    case "QUAN_TRONG":
                        lblDoUuTienValue.ForeColor = Color.Orange;
                        break;
                    default:
                        lblDoUuTienValue.ForeColor = Color.Green;
                        break;
                }

                // Form chi tiết chỉ để xem, không có nút Sửa và Xóa
                // Các thao tác này đã có trong cột "Thao tác" của bảng
                btnSua.Visible = false;
                btnXoa.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Kiểm tra quyền chỉnh sửa
                bool hasUpdatePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.UPDATE);
                bool hasCreatePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.CREATE);
                
                // ✅ Logic phân quyền:
                // - Nếu có quyền UPDATE → được sửa tất cả thông báo
                // - Nếu CHỈ có quyền CREATE → chỉ được sửa thông báo của chính mình
                bool canEdit = hasUpdatePermission;
                
                if (!canEdit && hasCreatePermission)
                {
                    // Kiểm tra xem có phải thông báo của chính mình không
                    string currentUser = SessionManager.TenDangNhap ?? "";
                    canEdit = !string.IsNullOrEmpty(thongBao.MaNguoiTao) && 
                             !string.IsNullOrEmpty(currentUser) && 
                             thongBao.MaNguoiTao.Equals(currentUser, StringComparison.OrdinalIgnoreCase);
                }
                
                if (!canEdit)
                {
                    MessageBox.Show("Bạn chỉ có thể chỉnh sửa thông báo do chính mình tạo!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở form chỉnh sửa
                using (var frmSua = new FrmThemThongBao(thongBao))
                {
                    if (frmSua.ShowDialog() == DialogResult.OK)
                    {
                        // Reload dữ liệu
                        LoadData();
                        this.DialogResult = DialogResult.OK; // Báo cho form cha biết đã cập nhật
                        MessageBox.Show("Thông báo đã được cập nhật thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form chỉnh sửa: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Kiểm tra quyền xóa
                bool hasDeletePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.DELETE);
                bool hasCreatePermission = PermissionHelper.HasPermission(PermissionHelper.QLTHONGBAO, PermissionHelper.CREATE);
                
                // ✅ Logic phân quyền:
                // - Nếu có quyền DELETE → được xóa tất cả thông báo
                // - Nếu CHỈ có quyền CREATE → chỉ được xóa thông báo của chính mình
                bool canDelete = hasDeletePermission;
                
                if (!canDelete && hasCreatePermission)
                {
                    // Kiểm tra xem có phải thông báo của chính mình không
                    string currentUser = SessionManager.TenDangNhap ?? "";
                    canDelete = !string.IsNullOrEmpty(thongBao.MaNguoiTao) && 
                               !string.IsNullOrEmpty(currentUser) && 
                               thongBao.MaNguoiTao.Equals(currentUser, StringComparison.OrdinalIgnoreCase);
                }
                
                if (!canDelete)
                {
                    MessageBox.Show("Bạn chỉ có thể xóa thông báo do chính mình tạo!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa thông báo này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var deleteResult = thongBaoBUS.XoaThongBao(thongBao.MaThongBao, SessionManager.TenDangNhap);
                    if (deleteResult.Success)
                    {
                        MessageBox.Show("Thông báo đã được xóa thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK; // Báo cho form cha biết đã xóa
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(deleteResult.Message, "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa thông báo: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Designer Code

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTieuDeValue;
        private System.Windows.Forms.Label lblLoaiThongBao;
        private System.Windows.Forms.Label lblLoaiThongBaoValue;
        private System.Windows.Forms.Label lblDoiTuongNhan;
        private System.Windows.Forms.Label lblDoiTuongNhanValue;
        private System.Windows.Forms.Label lblNguoiTao;
        private System.Windows.Forms.Label lblNguoiTaoValue;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.Label lblNgayTaoValue;
        private System.Windows.Forms.Label lblDoUuTien;
        private System.Windows.Forms.Label lblDoUuTienValue;
        private System.Windows.Forms.Label lblNgayHetHan;
        private System.Windows.Forms.Label lblNgayHetHanValue;
        private System.Windows.Forms.Label lblNoiDung;
        private Guna.UI2.WinForms.Guna2TextBox txtNoiDung;
        private Guna.UI2.WinForms.Guna2Button btnDong;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlFooter;

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblTieuDeValue = new System.Windows.Forms.Label();
            this.lblLoaiThongBao = new System.Windows.Forms.Label();
            this.lblLoaiThongBaoValue = new System.Windows.Forms.Label();
            this.lblDoiTuongNhan = new System.Windows.Forms.Label();
            this.lblDoiTuongNhanValue = new System.Windows.Forms.Label();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.lblNguoiTaoValue = new System.Windows.Forms.Label();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.lblNgayTaoValue = new System.Windows.Forms.Label();
            this.lblDoUuTien = new System.Windows.Forms.Label();
            this.lblDoUuTienValue = new System.Windows.Forms.Label();
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.lblNgayHetHanValue = new System.Windows.Forms.Label();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.txtNoiDung = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.lblTieuDe.Location = new System.Drawing.Point(25, 25);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(49, 15);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "Tiêu đề:";
            // 
            // lblTieuDeValue
            // 
            this.lblTieuDeValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDeValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTieuDeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTieuDeValue.Location = new System.Drawing.Point(25, 50);
            this.lblTieuDeValue.Name = "lblTieuDeValue";
            this.lblTieuDeValue.Size = new System.Drawing.Size(750, 45);
            this.lblTieuDeValue.TabIndex = 1;
            // 
            // lblLoaiThongBao
            // 
            this.lblLoaiThongBao.AutoSize = true;
            this.lblLoaiThongBao.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoaiThongBao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLoaiThongBao.Location = new System.Drawing.Point(25, 110);
            this.lblLoaiThongBao.Name = "lblLoaiThongBao";
            this.lblLoaiThongBao.Size = new System.Drawing.Size(90, 15);
            this.lblLoaiThongBao.TabIndex = 2;
            this.lblLoaiThongBao.Text = "Loại thông báo:";
            // 
            // lblLoaiThongBaoValue
            // 
            this.lblLoaiThongBaoValue.AutoSize = true;
            this.lblLoaiThongBaoValue.BackColor = System.Drawing.Color.Transparent;
            this.lblLoaiThongBaoValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLoaiThongBaoValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblLoaiThongBaoValue.Location = new System.Drawing.Point(25, 132);
            this.lblLoaiThongBaoValue.Name = "lblLoaiThongBaoValue";
            this.lblLoaiThongBaoValue.Size = new System.Drawing.Size(0, 17);
            this.lblLoaiThongBaoValue.TabIndex = 3;
            // 
            // lblDoiTuongNhan
            // 
            this.lblDoiTuongNhan.AutoSize = true;
            this.lblDoiTuongNhan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDoiTuongNhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDoiTuongNhan.Location = new System.Drawing.Point(25, 165);
            this.lblDoiTuongNhan.Name = "lblDoiTuongNhan";
            this.lblDoiTuongNhan.Size = new System.Drawing.Size(94, 15);
            this.lblDoiTuongNhan.TabIndex = 4;
            this.lblDoiTuongNhan.Text = "Đối tượng nhận:";
            // 
            // lblDoiTuongNhanValue
            // 
            this.lblDoiTuongNhanValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDoiTuongNhanValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDoiTuongNhanValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDoiTuongNhanValue.Location = new System.Drawing.Point(25, 187);
            this.lblDoiTuongNhanValue.Name = "lblDoiTuongNhanValue";
            this.lblDoiTuongNhanValue.Size = new System.Drawing.Size(375, 45);
            this.lblDoiTuongNhanValue.TabIndex = 5;
            // 
            // lblNguoiTao
            // 
            this.lblNguoiTao.AutoSize = true;
            this.lblNguoiTao.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNguoiTao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNguoiTao.Location = new System.Drawing.Point(425, 110);
            this.lblNguoiTao.Name = "lblNguoiTao";
            this.lblNguoiTao.Size = new System.Drawing.Size(63, 15);
            this.lblNguoiTao.TabIndex = 6;
            this.lblNguoiTao.Text = "Người tạo:";
            // 
            // lblNguoiTaoValue
            // 
            this.lblNguoiTaoValue.AutoSize = true;
            this.lblNguoiTaoValue.BackColor = System.Drawing.Color.Transparent;
            this.lblNguoiTaoValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiTaoValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNguoiTaoValue.Location = new System.Drawing.Point(425, 132);
            this.lblNguoiTaoValue.Name = "lblNguoiTaoValue";
            this.lblNguoiTaoValue.Size = new System.Drawing.Size(0, 17);
            this.lblNguoiTaoValue.TabIndex = 7;
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgayTao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNgayTao.Location = new System.Drawing.Point(425, 165);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(58, 15);
            this.lblNgayTao.TabIndex = 8;
            this.lblNgayTao.Text = "Ngày tạo:";
            // 
            // lblNgayTaoValue
            // 
            this.lblNgayTaoValue.BackColor = System.Drawing.Color.Transparent;
            this.lblNgayTaoValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayTaoValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNgayTaoValue.Location = new System.Drawing.Point(425, 187);
            this.lblNgayTaoValue.Name = "lblNgayTaoValue";
            this.lblNgayTaoValue.Size = new System.Drawing.Size(350, 25);
            this.lblNgayTaoValue.TabIndex = 9;
            // 
            // lblDoUuTien
            // 
            this.lblDoUuTien.AutoSize = true;
            this.lblDoUuTien.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDoUuTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDoUuTien.Location = new System.Drawing.Point(25, 230);
            this.lblDoUuTien.Name = "lblDoUuTien";
            this.lblDoUuTien.Size = new System.Drawing.Size(66, 15);
            this.lblDoUuTien.TabIndex = 10;
            this.lblDoUuTien.Text = "Độ ưu tiên:";
            // 
            // lblDoUuTienValue
            // 
            this.lblDoUuTienValue.AutoSize = true;
            this.lblDoUuTienValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDoUuTienValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDoUuTienValue.Location = new System.Drawing.Point(25, 252);
            this.lblDoUuTienValue.Name = "lblDoUuTienValue";
            this.lblDoUuTienValue.Size = new System.Drawing.Size(0, 17);
            this.lblDoUuTienValue.TabIndex = 11;
            // 
            // lblNgayHetHan
            // 
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNgayHetHan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNgayHetHan.Location = new System.Drawing.Point(425, 230);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new System.Drawing.Size(81, 15);
            this.lblNgayHetHan.TabIndex = 12;
            this.lblNgayHetHan.Text = "Ngày hết hạn:";
            // 
            // lblNgayHetHanValue
            // 
            this.lblNgayHetHanValue.AutoSize = true;
            this.lblNgayHetHanValue.BackColor = System.Drawing.Color.Transparent;
            this.lblNgayHetHanValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgayHetHanValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNgayHetHanValue.Location = new System.Drawing.Point(425, 252);
            this.lblNgayHetHanValue.Name = "lblNgayHetHanValue";
            this.lblNgayHetHanValue.Size = new System.Drawing.Size(0, 17);
            this.lblNgayHetHanValue.TabIndex = 13;
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblNoiDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNoiDung.Location = new System.Drawing.Point(28, 289);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(71, 19);
            this.lblNoiDung.TabIndex = 0;
            this.lblNoiDung.Text = "Nội dung:";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtNoiDung.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtNoiDung.BorderRadius = 10;
            this.txtNoiDung.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNoiDung.DefaultText = "";
            this.txtNoiDung.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtNoiDung.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNoiDung.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtNoiDung.Location = new System.Drawing.Point(30, 311);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.PlaceholderText = "";
            this.txtNoiDung.ReadOnly = true;
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoiDung.SelectedText = "";
            this.txtNoiDung.Size = new System.Drawing.Size(750, 288);
            this.txtNoiDung.TabIndex = 1;
            // 
            // btnDong
            // 
            // btnDong - Chỉ giữ lại nút Đóng, các thao tác khác đã có trong cột "Thao tác"
            this.btnDong.BorderRadius = 8;
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnDong.Location = new System.Drawing.Point(690, 20); // Giữ ở bên phải
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 42);
            this.btnDong.TabIndex = 0;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 8;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSua.Location = new System.Drawing.Point(575, 20);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 42);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 8;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnXoa.Location = new System.Drawing.Point(25, 20);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 42);
            this.btnXoa.TabIndex = 0;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Controls.Add(this.lblTieuDeValue);
            this.pnlHeader.Controls.Add(this.lblLoaiThongBao);
            this.pnlHeader.Controls.Add(this.lblLoaiThongBaoValue);
            this.pnlHeader.Controls.Add(this.lblDoiTuongNhan);
            this.pnlHeader.Controls.Add(this.lblDoiTuongNhanValue);
            this.pnlHeader.Controls.Add(this.lblNguoiTao);
            this.pnlHeader.Controls.Add(this.lblNguoiTaoValue);
            this.pnlHeader.Controls.Add(this.lblNgayTao);
            this.pnlHeader.Controls.Add(this.lblNgayTaoValue);
            this.pnlHeader.Controls.Add(this.lblDoUuTien);
            this.pnlHeader.Controls.Add(this.lblDoUuTienValue);
            this.pnlHeader.Controls.Add(this.lblNgayHetHan);
            this.pnlHeader.Controls.Add(this.lblNgayHetHanValue);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(25, 25, 25, 20);
            this.pnlHeader.Size = new System.Drawing.Size(820, 286);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlContent.Controls.Add(this.lblNoiDung);
            this.pnlContent.Controls.Add(this.txtNoiDung);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(25, 25, 25, 20);
            this.pnlContent.Size = new System.Drawing.Size(820, 605);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlFooter - Chỉ có nút Đóng
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFooter.Controls.Add(this.btnDong);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 605);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(25, 20, 25, 20);
            this.pnlFooter.Size = new System.Drawing.Size(820, 75);
            this.pnlFooter.TabIndex = 2;
            // 
            // FrmChiTietThongBao
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(820, 680);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChiTietThongBao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết thông báo";
            this.Load += new System.EventHandler(this.FrmChiTietThongBao_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

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
