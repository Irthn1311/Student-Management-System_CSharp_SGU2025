using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FormChiTietYeuCau : Form
    {
        private YeuCauChuyenLopDTO yeuCau;
        private LopHocBUS lopHocBUS;

        public FormChiTietYeuCau(YeuCauChuyenLopDTO yeuCau)
        {
            InitializeComponent();
            this.yeuCau = yeuCau;
            lopHocBUS = new LopHocBUS();
        }

        private void FormChiTietYeuCau_Load(object sender, EventArgs e)
        {
            LoadThongTinYeuCau();
        }

        private void LoadThongTinYeuCau()
        {
            try
            {
                // Hiển thị thông tin học sinh
                lblHocSinhValue.Text = yeuCau.TenHocSinh;
                lblLopHienTaiValue.Text = yeuCau.TenLopHienTai;
                lblHocKyValue.Text = $"{yeuCau.TenHocKy} - {yeuCau.TenNamHoc}";
                lblLopMongMuonValue.Text = yeuCau.TenLopMongMuon ?? "Không chỉ định";
                
                // ✅ Hiển thị lớp admin duyệt (luôn hiển thị để so sánh với lớp mong muốn)
                if (!string.IsNullOrEmpty(yeuCau.TenLopDuocDuyet))
                {
                    lblLopAdminDuyetValue.Text = yeuCau.TenLopDuocDuyet;
                    lblLopAdminDuyetValue.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                }
                else
                {
                    lblLopAdminDuyetValue.Text = "Chưa duyệt";
                    lblLopAdminDuyetValue.ForeColor = Color.FromArgb(100, 100, 100); // Xám
                }
                
                // Hiển thị lý do
                txtLyDoYeuCau.Text = yeuCau.LyDoYeuCau;
                txtLyDoYeuCau.ReadOnly = true;

                // Hiển thị mã yêu cầu
                lblMaYeuCau.Text = $"Mã yêu cầu: #{yeuCau.MaYeuCau}";

                // Hiển thị thông tin trạng thái và xử lý
                lblTrangThaiValue.Text = yeuCau.TrangThai;
                
                // Đặt màu cho trạng thái
                switch (yeuCau.TrangThai)
                {
                    case "Chờ duyệt":
                        lblTrangThaiValue.ForeColor = Color.FromArgb(234, 179, 8); // Vàng
                        break;
                    case "Đã duyệt":
                        lblTrangThaiValue.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                        break;
                    case "Từ chối":
                        lblTrangThaiValue.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                        break;
                    default:
                        lblTrangThaiValue.ForeColor = Color.Black;
                        break;
                }

                // Hiển thị thông tin ngày tạo và người tạo
                lblNgayTaoValue.Text = yeuCau.NgayTao.ToString("dd/MM/yyyy HH:mm");
                lblNguoiTaoValue.Text = yeuCau.NguoiTao;

                // Điều chỉnh vị trí các panel
                int currentY = 310 + 130 + 20; // panelLyDo bottom + spacing
                currentY += 100 + 20; // panelTrangThai height + spacing

                // Hiển thị thông tin xử lý nếu đã được xử lý
                if (yeuCau.TrangThai != "Chờ duyệt")
                {
                    panelXuLy.Visible = true;
                    panelXuLy.Location = new Point(20, currentY);
                    lblNgayXuLyValue.Text = yeuCau.NgayXuLy?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
                    lblNguoiXuLyValue.Text = yeuCau.NguoiXuLy ?? "N/A";
                    currentY += 80 + 20;

                    // Hiển thị lớp được duyệt nếu đã duyệt
                    if (yeuCau.TrangThai == "Đã duyệt" && !string.IsNullOrEmpty(yeuCau.TenLopDuocDuyet))
                    {
                        lblLopDuocDuyetValue.Text = yeuCau.TenLopDuocDuyet;
                        panelLopDuocDuyet.Visible = true;
                        panelLopDuocDuyet.Location = new Point(20, currentY);
                        currentY += 60 + 20;
                    }
                    else
                    {
                        panelLopDuocDuyet.Visible = false;
                    }

                    // Hiển thị ghi chú admin nếu có
                    if (!string.IsNullOrWhiteSpace(yeuCau.GhiChuAdmin))
                    {
                        txtGhiChuAdmin.Text = yeuCau.GhiChuAdmin;
                        txtGhiChuAdmin.ReadOnly = true;
                        panelGhiChu.Visible = true;
                        panelGhiChu.Location = new Point(20, currentY);
                        currentY += 100 + 20;
                    }
                    else
                    {
                        panelGhiChu.Visible = false;
                    }
                }
                else
                {
                    panelXuLy.Visible = false;
                    panelLopDuocDuyet.Visible = false;
                    panelGhiChu.Visible = false;
                }

                // Điều chỉnh vị trí nút đóng
                panelButtons.Location = new Point(20, currentY);
                
                // Điều chỉnh kích thước form
                this.Height = currentY + 60 + 40; // panelButtons height + margin
                panelMain.Height = this.Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin yêu cầu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

