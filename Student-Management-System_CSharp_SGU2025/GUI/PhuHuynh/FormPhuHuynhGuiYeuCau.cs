using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.PhuHuynh
{
    public partial class FormPhuHuynhGuiYeuCau : Form
    {
        private string tenDangNhap;
        private HocSinhBLL hocSinhBLL;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;
        private PhanLopBLL phanLopBLL;
        private HocSinhDAO hocSinhDAO;
        private HocSinhDTO hocSinhHienTai;

        public FormPhuHuynhGuiYeuCau(string tenDangNhap)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            hocSinhBLL = new HocSinhBLL();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
            phanLopBLL = new PhanLopBLL();
            hocSinhDAO = new DAO.HocSinhDAO();
        }

        private void FormPhuHuynhGuiYeuCau_Load(object sender, EventArgs e)
        {
            LoadThongTinHocSinh();
            LoadHocKy();
        }

        private void LoadThongTinHocSinh()
        {
            try
            {
                if (string.IsNullOrEmpty(tenDangNhap))
                {
                    MessageBox.Show("Không xác định được tên đăng nhập.", 
                        "Lỗi", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // CÁCH 1: Thử lấy học sinh theo tên đăng nhập trước (từ cột TenDangNhap trong bảng HocSinh)
                hocSinhHienTai = hocSinhDAO.LayHocSinhTheoTenDangNhap(tenDangNhap);

                // CÁCH 2: Nếu không tìm thấy, thử parse mã từ tên đăng nhập (HS1001 → 1001)
                if (hocSinhHienTai == null)
                {
                    string temp = tenDangNhap.ToUpper().Replace("HS", "").Trim();
                    if (int.TryParse(temp, out int maHocSinh))
                    {
                        hocSinhHienTai = hocSinhDAO.LayHocSinhTheoMa(maHocSinh);
                    }
                }

                // CÁCH 3: Nếu vẫn không tìm thấy, log để debug
                if (hocSinhHienTai == null)
                {
                    Console.WriteLine($"⚠️ Không tìm thấy học sinh với tên đăng nhập: {tenDangNhap}");
                    Console.WriteLine($"   - Đã thử tìm theo TenDangNhap trong bảng HocSinh");
                    Console.WriteLine($"   - Đã thử parse mã từ tên đăng nhập (HS1001 → 1001)");
                }

                // Nếu vẫn không tìm thấy
                if (hocSinhHienTai == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin học sinh liên kết với tài khoản này.\n\n" +
                        $"Tên đăng nhập: {tenDangNhap}\n\n" +
                        "Vui lòng kiểm tra:\n" +
                        "1. Tên đăng nhập có đúng không?\n" +
                        "2. Học sinh đã được tạo trong hệ thống chưa?\n" +
                        "3. Cột TenDangNhap trong bảng HocSinh đã được cập nhật chưa?\n\n" +
                        "Nếu vẫn không được, vui lòng liên hệ nhà trường.", 
                        "Không tìm thấy học sinh", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                // Hiển thị thông tin học sinh
                lblHocSinh.Text = $"👤 Học sinh: {hocSinhHienTai.HoTen} (Mã: {hocSinhHienTai.MaHS})";
                cbHocSinh.Visible = false; // Ẩn combobox vì đã biết học sinh rồi
                lblChonConEm.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin học sinh: {ex.Message}\n\n" +
                    $"Tên đăng nhập: {tenDangNhap}\n\n" +
                    $"Chi tiết: {ex.StackTrace}", 
                    "Lỗi", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadHocKy()
        {
            try
            {
                cbHocKy.Items.Clear();
                cbHocKy.Items.Add("-- Chọn học kỳ --");

                var dsHocKy = hocKyBUS.DocDSHocKy();
                if (dsHocKy == null || dsHocKy.Count == 0) return;

                // Lấy học kỳ hiện tại (đang diễn ra)
                HocKyDTO hocKyHienTai = null;
                foreach (var hk in dsHocKy)
                {
                    try
                    {
                        if (hk.NgayBD.HasValue && hk.NgayKT.HasValue &&
                            hk.NgayBD.Value.Date <= DateTime.Today && hk.NgayKT.Value.Date >= DateTime.Today)
                        {
                            hocKyHienTai = hk;
                            break;
                        }
                    }
                    catch { }
                }

                // Nếu không có học kỳ hiện tại, lấy học kỳ mới nhất
                if (hocKyHienTai == null && dsHocKy.Count > 0)
                {
                    hocKyHienTai = dsHocKy[0];
                }

                if (hocKyHienTai != null)
                {
                    cbHocKy.Items.Add(new ComboBoxItem
                    {
                        Text = $"{hocKyHienTai.TenHocKy}",
                        Value = hocKyHienTai.MaHocKy
                    });

                    cbHocKy.SelectedIndex = 1; // Tự động chọn học kỳ hiện tại
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải học kỳ: {ex.Message}", 
                    "Lỗi", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void cbHocSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Không cần nữa vì đã tự động lấy học sinh
            CapNhatThongTinLopHienTai();
        }

        private void CapNhatThongTinLopHienTai()
        {
            try
            {
                if (hocSinhHienTai == null || cbHocKy.SelectedIndex <= 0) return;

                var hocKyItem = cbHocKy.SelectedItem as ComboBoxItem;
                if (hocKyItem == null) return;

                int maHocKy = (int)hocKyItem.Value;

                // Tìm lớp hiện tại của học sinh
                var lopHienTai = LayLopHienTaiCuaHocSinh(hocSinhHienTai.MaHS, maHocKy);

                if (lopHienTai != null)
                {
                    lblThongTinLop.Text = $"📚 Lớp hiện tại: {lopHienTai.tenLop} (Khối {lopHienTai.maKhoi})";
                    lblThongTinLop.ForeColor = Color.FromArgb(34, 197, 94);
                    lblThongTinLop.Visible = true;
                }
                else
                {
                    lblThongTinLop.Text = "⚠️ Chưa được phân lớp trong học kỳ này";
                    lblThongTinLop.ForeColor = Color.FromArgb(220, 38, 38);
                    lblThongTinLop.Visible = true;
                }
            }
            catch
            {
                lblThongTinLop.Visible = false;
            }
        }

        private LopDTO LayLopHienTaiCuaHocSinh(int maHocSinh, int maHocKy)
        {
            try
            {
                // Lấy tất cả lớp
                var dsLop = lopHocBUS.DocDSLop();
                
                foreach (var lop in dsLop)
                {
                    var dsHocSinhTrongLop = phanLopBLL.GetHocSinhByLop(lop.maLop, maHocKy);
                    if (dsHocSinhTrongLop != null)
                    {
                        foreach (var hs in dsHocSinhTrongLop)
                        {
                            if (hs.MaHS == maHocSinh)
                            {
                                return lop;
                            }
                        }
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private void btnGuiYeuCau_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã có thông tin học sinh chưa
                if (hocSinhHienTai == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin học sinh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra đã chọn học kỳ chưa
                if (cbHocKy.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var hocKyItem = cbHocKy.SelectedItem as ComboBoxItem;
                if (hocKyItem == null) return;

                int maHocKy = (int)hocKyItem.Value;

                // Lấy lớp hiện tại
                var lopHienTai = LayLopHienTaiCuaHocSinh(hocSinhHienTai.MaHS, maHocKy);

                if (lopHienTai == null)
                {
                    MessageBox.Show("Bạn chưa được phân lớp trong học kỳ này.\n\n" +
                        "Không thể gửi yêu cầu chuyển lớp.", 
                        "Không thể gửi yêu cầu", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                    return;
                }

                // Mở form gửi yêu cầu
                FormGuiYeuCauChuyenLop form = new FormGuiYeuCauChuyenLop(
                    hocSinhHienTai.MaHS,
                    lopHienTai.maLop,
                    maHocKy,
                    hocSinhHienTai.HoTen,
                    lopHienTai.tenLop,
                    tenDangNhap
                );

                if (form.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("✅ Đã gửi yêu cầu chuyển lớp thành công!\n\n" +
                        "Yêu cầu của bạn đang chờ nhà trường xem xét.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }
    }
}

