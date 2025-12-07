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

                // Hiển thị thông tin
                lblTieuDe.Text = thongBao.TieuDe;
                lblLoaiThongBao.Text = thongBao.TenLoaiThongBao ?? thongBao.LoaiThongBao;
                lblDoiTuongNhan.Text = thongBao.DoiTuongNhan;
                lblNguoiTao.Text = thongBao.TenNguoiTao ?? thongBao.MaNguoiTao;
                lblNgayTao.Text = thongBao.NgayTao.ToString("dd/MM/yyyy HH:mm");
                lblDoUuTien.Text = thongBao.GetDoUuTienText();
                txtNoiDung.Text = thongBao.NoiDung;

                if (thongBao.NgayHetHan.HasValue)
                {
                    lblNgayHetHan.Text = thongBao.NgayHetHan.Value.ToString("dd/MM/yyyy");
                    
                    // Hiển thị cảnh báo nếu đã hết hạn
                    if (thongBao.IsExpired())
                    {
                        lblNgayHetHan.ForeColor = Color.Red;
                        lblNgayHetHan.Text += " (Đã hết hạn)";
                    }
                }
                else
                {
                    lblNgayHetHan.Text = "Không có hạn";
                }

                // Màu sắc cho độ ưu tiên
                switch (thongBao.DoUuTien)
                {
                    case "KHAN_CAP":
                        lblDoUuTien.ForeColor = Color.Red;
                        break;
                    case "QUAN_TRONG":
                        lblDoUuTien.ForeColor = Color.Orange;
                        break;
                    default:
                        lblDoUuTien.ForeColor = Color.Green;
                        break;
                }
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

        #region Designer Code (Minimal)

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblLoaiThongBao;
        private System.Windows.Forms.Label lblDoiTuongNhan;
        private System.Windows.Forms.Label lblNguoiTao;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.Label lblDoUuTien;
        private System.Windows.Forms.Label lblNgayHetHan;
        private System.Windows.Forms.TextBox txtNoiDung;
        private Guna.UI2.WinForms.Guna2Button btnDong;

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblLoaiThongBao = new System.Windows.Forms.Label();
            this.lblDoiTuongNhan = new System.Windows.Forms.Label();
            this.lblNguoiTao = new System.Windows.Forms.Label();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.lblDoUuTien = new System.Windows.Forms.Label();
            this.lblNgayHetHan = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            
            // lblTieuDe
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Location = new System.Drawing.Point(20, 20);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(100, 30);
            this.lblTieuDe.Text = "Tiêu đề";
            
            // txtNoiDung
            this.txtNoiDung.Location = new System.Drawing.Point(20, 150);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.ReadOnly = true;
            this.txtNoiDung.Size = new System.Drawing.Size(760, 350);
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            
            // btnDong
            this.btnDong.Location = new System.Drawing.Point(680, 520);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 40);
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            
            // Form
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết thông báo";
            
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblLoaiThongBao);
            this.Controls.Add(this.lblDoiTuongNhan);
            this.Controls.Add(this.lblNguoiTao);
            this.Controls.Add(this.lblNgayTao);
            this.Controls.Add(this.lblDoUuTien);
            this.Controls.Add(this.lblNgayHetHan);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.btnDong);
            
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
