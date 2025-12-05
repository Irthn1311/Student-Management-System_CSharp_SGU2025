using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.GiaoVien
{
    public partial class ChinhSuaGiaoVien : Form
    {
        private GiaoVienBUS giaoVienBUS;
        private MonHocBUS monHocBUS;
        private List<MonHocDTO> danhSachMonHoc;
        private GiaoVienDTO giaoVienHienTai;
        private string maGiaoVien;
        private ErrorProvider errorProvider;
        private bool isReadOnly;

        public ChinhSuaGiaoVien(string maGiaoVien, bool readOnly = false)
        {
            InitializeComponent();
            this.maGiaoVien = maGiaoVien;
            this.isReadOnly = readOnly;
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

        private void ChinhSuaGiaoVien_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ Load danh sách môn học sử dụng LINQ to Objects
                danhSachMonHoc = monHocBUS.DocDSMH();
                cbChuyenMon.Items.Clear();
                cbChuyenMon.Items.Add("-- Chọn môn chuyên môn --");
                // Sử dụng LINQ to Objects để thêm tên môn học vào combobox
                danhSachMonHoc.Select(m => m.tenMon)
                    .ToList()
                    .ForEach(tenMon => cbChuyenMon.Items.Add(tenMon));

                // Load danh sách giới tính
                cbGioiTinh.Items.Clear();
                cbGioiTinh.Items.Add("Nam");
                cbGioiTinh.Items.Add("Nữ");

                // Load danh sách trạng thái
                cbTrangThai.Items.Clear();
                cbTrangThai.Items.Add("Đang giảng dạy");
                cbTrangThai.Items.Add("Nghỉ dạy");
                cbTrangThai.Items.Add("Nghỉ hưu");

                // Load thông tin giáo viên
                LoadGiaoVien();

                // Thiết lập chế độ chỉ đọc nếu cần
                if (isReadOnly)
                {
                    SetReadOnlyMode();
                    this.Text = "Chi tiết giáo viên";
                    btnLuu.Visible = false;
                    btnHuy.Text = "Đóng";
                }
                else
                {
                    this.Text = "Chỉnh sửa giáo viên";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGiaoVien()
        {
            try
            {
                giaoVienHienTai = giaoVienBUS.LayGiaoVienTheoMa(maGiaoVien);
                if (giaoVienHienTai == null)
                {
                    MessageBox.Show("Không tìm thấy giáo viên với mã này!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Điền dữ liệu vào form
                txtMaGiaoVien.Text = giaoVienHienTai.MaGiaoVien;
                txtMaGiaoVien.ReadOnly = true; // Không cho sửa mã
                txtHoTen.Text = giaoVienHienTai.HoTen;
                dateNgaySinh.Value = giaoVienHienTai.NgaySinh != DateTime.MinValue 
                    ? giaoVienHienTai.NgaySinh 
                    : DateTime.Now.AddYears(-30);
                dateNgaySinh.MaxDate = DateTime.Now.AddYears(-22);

                // Chọn giới tính
                if (!string.IsNullOrEmpty(giaoVienHienTai.GioiTinh))
                {
                    int index = cbGioiTinh.FindString(giaoVienHienTai.GioiTinh);
                    if (index >= 0)
                        cbGioiTinh.SelectedIndex = index;
                }

                txtSoDienThoai.Text = giaoVienHienTai.SoDienThoai ?? "";
                txtEmail.Text = giaoVienHienTai.Email ?? "";
                txtDiaChi.Text = giaoVienHienTai.DiaChi ?? "";

                // Chọn môn chuyên môn
                if (giaoVienHienTai.MaMonChuyenMon.HasValue)
                {
                    var monHoc = danhSachMonHoc.FirstOrDefault(m => m.maMon == giaoVienHienTai.MaMonChuyenMon.Value);
                    if (monHoc != null)
                    {
                        int index = cbChuyenMon.FindString(monHoc.tenMon);
                        if (index >= 0)
                            cbChuyenMon.SelectedIndex = index;
                    }
                }
                else
                {
                    cbChuyenMon.SelectedIndex = 0;
                }

                // Chọn trạng thái
                if (!string.IsNullOrEmpty(giaoVienHienTai.TrangThai))
                {
                    int index = cbTrangThai.FindString(giaoVienHienTai.TrangThai);
                    if (index >= 0)
                        cbTrangThai.SelectedIndex = index;
                }
                else
                {
                    cbTrangThai.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetReadOnlyMode()
        {
            txtMaGiaoVien.ReadOnly = true;
            txtHoTen.ReadOnly = true;
            dateNgaySinh.Enabled = false;
            cbGioiTinh.Enabled = false;
            txtSoDienThoai.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            cbChuyenMon.Enabled = false;
            cbTrangThai.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear errors
                ClearAllErrors();

                // Validation
                if (!ValidateInput())
                    return;

                // Cập nhật DTO
                giaoVienHienTai.HoTen = txtHoTen.Text.Trim();
                giaoVienHienTai.NgaySinh = dateNgaySinh.Value;
                giaoVienHienTai.GioiTinh = cbGioiTinh.SelectedItem.ToString();
                giaoVienHienTai.DiaChi = txtDiaChi.Text.Trim();
                giaoVienHienTai.SoDienThoai = txtSoDienThoai.Text.Trim();
                giaoVienHienTai.Email = txtEmail.Text.Trim();
                giaoVienHienTai.TrangThai = cbTrangThai.SelectedItem.ToString();

                // Chọn môn chuyên môn
                if (cbChuyenMon.SelectedIndex > 0)
                {
                    string tenMon = cbChuyenMon.SelectedItem.ToString();
                    var monHoc = danhSachMonHoc.FirstOrDefault(m => m.tenMon == tenMon);
                    if (monHoc != null)
                    {
                        giaoVienHienTai.MaMonChuyenMon = monHoc.maMon;
                    }
                }
                else
                {
                    giaoVienHienTai.MaMonChuyenMon = null;
                }

                // Xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn cập nhật thông tin giáo viên?\n\n" +
                    $"Mã GV: {giaoVienHienTai.MaGiaoVien}\n" +
                    $"Họ tên: {giaoVienHienTai.HoTen}",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cập nhật giáo viên
                    bool success = giaoVienBUS.CapNhatGiaoVien(giaoVienHienTai);
                    if (success)
                    {
                        MessageBox.Show("Cập nhật thông tin giáo viên thành công!",
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
                MessageBox.Show($"Lỗi khi cập nhật giáo viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            bool isValid = true;

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
            errorProvider.SetError(txtHoTen, "");
            errorProvider.SetError(dateNgaySinh, "");
            errorProvider.SetError(txtEmail, "");
            errorProvider.SetError(txtSoDienThoai, "");
        }
    }
}

