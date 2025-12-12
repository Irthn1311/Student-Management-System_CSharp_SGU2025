using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmXemChiTietPhanCongGiangDay : Form
    {
        private PhanCongGiangDayBUS phanCongBUS;
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;
        private int maPhanCong;

        public FrmXemChiTietPhanCongGiangDay(int maPhanCong)
        {
            InitializeComponent();
            this.maPhanCong = maPhanCong;
            phanCongBUS = new PhanCongGiangDayBUS();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
        }

        private void FrmXemChiTietPhanCongGiangDay_Load(object sender, EventArgs e)
        {
            try
            {
                LoadChiTietPhanCong();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietPhanCong()
        {
            try
            {
                PhanCongGiangDayDTO pc = phanCongBUS.LayPhanCongTheoMa(maPhanCong);
                if (pc == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin phân công!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Lấy thông tin chi tiết
                GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(pc.MaGiaoVien);
                MonHocDTO mh = monHocBUS.LayDSMonHocTheoId(pc.MaMonHoc);
                LopDTO lop = lopHocBUS.LayLopTheoId(pc.MaLop);
                HocKyDTO hk = hocKyBUS.LayHocKyTheoMa(pc.MaHocKy);

                // Hiển thị thông tin
                lblMaPhanCong.Text = $"Mã phân công: #{pc.MaPhanCong}";
                lblGiaoVien.Text = gv != null ? $"{gv.HoTen}" : pc.MaGiaoVien;
                
                // Thông tin bổ sung về giáo viên
                if (gv != null)
                {
                    lblGiaoVienChiTiet.Text = $"Mã giáo viên: {gv.MaGiaoVien}\n" +
                                              $"Email: {gv.Email ?? "Chưa có"}\n" +
                                              $"SĐT: {gv.SoDienThoai ?? "Chưa có"}\n" +
                                              $"Chuyên môn: {gv.TenMonChuyenMon ?? "Chưa có"}";
                }
                else
                {
                    lblGiaoVienChiTiet.Text = $"Mã giáo viên: {pc.MaGiaoVien}";
                }

                lblMonHoc.Text = mh != null ? mh.tenMon : $"Mã môn: {pc.MaMonHoc}";
                
                lblLop.Text = lop != null ? lop.tenLop : $"Mã lớp: {pc.MaLop}";
                
                // Thông tin bổ sung về lớp
                if (lop != null)
                {
                    lblLopChiTiet.Text = $"Sĩ số: {lop.siSo} học sinh";
                    if (!string.IsNullOrEmpty(lop.maGVCN))
                    {
                        var gvCN = giaoVienBUS.LayGiaoVienTheoMa(lop.maGVCN);
                        if (gvCN != null)
                        {
                            lblLopChiTiet.Text += $"\nChủ nhiệm: {gvCN.HoTen}";
                        }
                    }
                }

                lblHocKy.Text = hk != null ? $"{hk.TenHocKy}" : $"Mã học kỳ: {pc.MaHocKy}";
                
                // Thông tin bổ sung về học kỳ
                if (hk != null)
                {
                    lblHocKyChiTiet.Text = $"Năm học: {hk.MaNamHoc}\n" +
                                           $"Ngày bắt đầu: {hk.NgayBD:dd/MM/yyyy}\n" +
                                           $"Ngày kết thúc: {hk.NgayKT:dd/MM/yyyy}\n" +
                                           $"Trạng thái: {hk.TrangThai}";
                }

                lblNgayBatDau.Text = $"Ngày bắt đầu: {pc.NgayBatDau:dd/MM/yyyy}";
                lblNgayKetThuc.Text = $"Ngày kết thúc: {pc.NgayKetThuc:dd/MM/yyyy}";

                // Tính số ngày
                TimeSpan soNgay = pc.NgayKetThuc - pc.NgayBatDau;
                lblThoiGian.Text = $"Thời gian: {soNgay.Days} ngày";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
