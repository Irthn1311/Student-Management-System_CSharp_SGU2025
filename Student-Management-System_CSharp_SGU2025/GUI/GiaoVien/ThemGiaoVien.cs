using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.GiaoVien
{
    public partial class ThemGiaoVien : Form
    {
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private List<MonHocDTO> danhSachMonHoc;
        private ErrorProvider errorProvider;

        public ThemGiaoVien()
        {
            InitializeComponent();
            giaoVienBUS = new GiaoVienBUS();
            monHocBUS = new MonHocBUS();
            danhSachMonHoc = new List<MonHocDTO>();

            // Khởi tạo Error Provider
            errorProvider = new ErrorProvider();
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            using (Bitmap bmp = new Bitmap(16, 16))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawIcon(SystemIcons.Error, new Rectangle(0, 0, 16, 16));
                errorProvider.Icon = Icon.FromHandle(bmp.GetHicon());
            }
        }

        private void ThemGiaoVien_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ Tự động tạo mã giáo viên
                string maTiepTheo = giaoVienBUS.LayMaGiaoVienTiepTheo();
                txtMaGiaoVien.Text = maTiepTheo;
                txtMaGiaoVien.ReadOnly = true; // Không cho sửa mã
                txtMaGiaoVien.Enabled = false; // Disable textbox

                // ✅ Load danh sách môn học sử dụng LINQ to Objects
                danhSachMonHoc = monHocBUS.DocDSMH();
                cbChuyenMon.Items.Clear();
                cbChuyenMon.Items.Add("-- Chọn môn chuyên môn --");
                // Sử dụng LINQ to Objects để thêm tên môn học vào combobox
                danhSachMonHoc.Select(m => m.tenMon)
                    .ToList()
                    .ForEach(tenMon => cbChuyenMon.Items.Add(tenMon));
                cbChuyenMon.SelectedIndex = 0;

                // Load danh sách giới tính
                cbGioiTinh.Items.Clear();
                cbGioiTinh.Items.Add("Nam");
                cbGioiTinh.Items.Add("Nữ");
                cbGioiTinh.SelectedIndex = 0;

                // Load danh sách trạng thái
                cbTrangThai.Items.Clear();
                cbTrangThai.Items.Add("Đang giảng dạy");
                cbTrangThai.Items.Add("Nghỉ dạy");
                cbTrangThai.Items.Add("Nghỉ hưu");
                cbTrangThai.SelectedIndex = 0;

                // Set ngày sinh mặc định
                dateNgaySinh.Value = DateTime.Now.AddYears(-30);
                dateNgaySinh.MaxDate = DateTime.Now.AddYears(-22);

                // Set focus vào họ tên
                txtHoTen.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnThemGiaoVien_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear errors
                ClearAllErrors();

                // Validation
                if (!ValidateInput())
                    return;

                // Tạo DTO
                GiaoVienDTO giaoVien = new GiaoVienDTO
                {
                    MaGiaoVien = txtMaGiaoVien.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dateNgaySinh.Value,
                    GioiTinh = cbGioiTinh.SelectedItem.ToString(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    TrangThai = cbTrangThai.SelectedItem.ToString()
                };

                // Chọn môn chuyên môn
                if (cbChuyenMon.SelectedIndex > 0)
                {
                    string tenMon = cbChuyenMon.SelectedItem.ToString();
                    var monHoc = danhSachMonHoc.FirstOrDefault(m => m.tenMon == tenMon);
                    if (monHoc != null)
                    {
                        giaoVien.MaMonChuyenMon = monHoc.maMon;
                    }
                }

                // Xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn thêm giáo viên?\n\n" +
                    $"Mã GV: {giaoVien.MaGiaoVien}\n" +
                    $"Họ tên: {giaoVien.HoTen}\n" +
                    $"Email: {giaoVien.Email}\n" +
                    $"Số điện thoại: {giaoVien.SoDienThoai}\n\n" +
                    $"Lưu ý: Tài khoản đăng nhập sẽ được tạo tự động với:\n" +
                    $"Tên đăng nhập: {giaoVien.MaGiaoVien}\n" +
                    $"Mật khẩu mặc định: {giaoVien.NgaySinh.ToString("ddMMyyyy")}",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Thêm giáo viên (sẽ tự động tạo tài khoản)
                    bool success = giaoVienBUS.ThemGiaoVien(giaoVien);
                    if (success)
                    {
                        MessageBox.Show("Thêm giáo viên thành công!\n\n" +
                            $"Tài khoản đăng nhập:\n" +
                            $"Tên đăng nhập: {giaoVien.MaGiaoVien}\n" +
                            $"Mật khẩu: {giaoVien.NgaySinh.ToString("ddMMyyyy")}",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            bool isValid = true;

            // Mã giáo viên - đã được tự động tạo, không cần validate
            // Nhưng vẫn kiểm tra để đảm bảo có giá trị
            if (string.IsNullOrWhiteSpace(txtMaGiaoVien.Text))
            {
                errorProvider.SetError(txtMaGiaoVien, "Mã giáo viên không được để trống");
                isValid = false;
            }

            // Họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                errorProvider.SetError(txtHoTen, "Họ tên không được để trống");
                isValid = false;
            }

            // Ngày sinh
            if (dateNgaySinh.Value >= DateTime.Now)
            {
                errorProvider.SetError(dateNgaySinh, "Ngày sinh phải nhỏ hơn ngày hiện tại");
                isValid = false;
            }

            int tuoi = DateTime.Now.Year - dateNgaySinh.Value.Year;
            if (tuoi < 22)
            {
                errorProvider.SetError(dateNgaySinh, "Giáo viên phải từ 22 tuổi trở lên");
                isValid = false;
            }

            // Email (nếu có)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                {
                    errorProvider.SetError(txtEmail, "Email không hợp lệ");
                    isValid = false;
                }
            }

            // Số điện thoại (nếu có)
            if (!string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                string sdt = txtSoDienThoai.Text.Trim();
                if (!sdt.StartsWith("0") || sdt.Length != 10 || !sdt.All(char.IsDigit))
                {
                    errorProvider.SetError(txtSoDienThoai, "Số điện thoại phải là 10 số và bắt đầu bằng 0");
                    isValid = false;
                }
            }

            return isValid;
        }

        private void ClearAllErrors()
        {
            errorProvider.SetError(txtMaGiaoVien, "");
            errorProvider.SetError(txtHoTen, "");
            errorProvider.SetError(dateNgaySinh, "");
            errorProvider.SetError(txtEmail, "");
            errorProvider.SetError(txtSoDienThoai, "");
        }
    }
}

