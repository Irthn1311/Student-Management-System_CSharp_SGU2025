using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.TaiKhoan
{
    public partial class ThemTaiKhoan : Form
    {
        private readonly PhanQuyenBUS phanQuyenBUS;
        private readonly NguoiDungBLL nguoiDungBLL;
        private bool isEditMode = false;
        private string editTenDangNhap = null;

        public ThemTaiKhoan()
        {
            InitializeComponent();
            phanQuyenBUS = new PhanQuyenBUS();
            nguoiDungBLL = new NguoiDungBLL();
            isEditMode = false;

        }

        public ThemTaiKhoan(string tenDangNhap, string hoTen, string email, string soDienThoai,
                         DateTime ngaySinh, string gioiTinh, string maVaiTroHienTai)
        {
            InitializeComponent();
            phanQuyenBUS = new PhanQuyenBUS();
            nguoiDungBLL = new NguoiDungBLL();

            isEditMode = true;
            editTenDangNhap = tenDangNhap;

            // Load form và điền dữ liệu
            this.Load += (s, e) => {
                LoadDataForEdit(tenDangNhap, hoTen, email, soDienThoai, ngaySinh, gioiTinh, maVaiTroHienTai);
            };
        }

        private void ThemTaiKhoan_Load(object sender, EventArgs e)
        {
            try
            {
                // Load danh sách vai trò vào ComboBox
                LoadVaiTro();

                // Load danh sách giới tính
                cbGioiTinh.Items.Clear();
                cbGioiTinh.Items.Add("Nam");
                cbGioiTinh.Items.Add("Nữ");
                cbGioiTinh.SelectedIndex = 0;

                // Set ngày sinh mặc định
                dateNgaySinh.Value = DateTime.Now.AddYears(-18);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ THÊM: Load dữ liệu cho chế độ sửa
        /// </summary>
        private void LoadDataForEdit(string tenDangNhap, string hoTen, string email, string soDienThoai,
                                     DateTime ngaySinh, string gioiTinh, string maVaiTroHienTai)
        {
            try
            {
                // Đổi tiêu đề form
                this.Text = "Sửa vai trò tài khoản";
                if (this.Controls.Find("lblTitle", true).FirstOrDefault() is Label lblTitle)
                {
                    lblTitle.Text = "Sửa vai trò tài khoản";
                }

                // Điền dữ liệu vào form
                txtDangNhap.Text = tenDangNhap;
                txtHoTen.Text = hoTen; // ✅ HIỂN THỊ HỌ TÊN
                txtNumber.Text = soDienThoai;
                txtEmail.Text = email;
                dateNgaySinh.Value = ngaySinh;

                // Chọn giới tính
                if (!string.IsNullOrEmpty(gioiTinh))
                {
                    int index = cbGioiTinh.FindString(gioiTinh);
                    if (index >= 0)
                        cbGioiTinh.SelectedIndex = index;
                }

                // Chọn vai trò hiện tại
                if (!string.IsNullOrEmpty(maVaiTroHienTai))
                {
                    // Tách MaVaiTro nếu có nhiều vai trò (lấy vai trò đầu tiên)
                    string maVaiTroDauTien = maVaiTroHienTai.Split(',')[0].Trim();
                    cbVaiTro.SelectedValue = maVaiTroDauTien;
                }

                // ✅ KHÓA CÁC TEXTBOX, CHỈ CHO PHÉP ĐỔI VAI TRÒ
                txtDangNhap.Enabled = false;
                txtHoTen.Enabled = false; // ✅ KHÓA HỌ TÊN
                txtNumber.Enabled = false;
                txtEmail.Enabled = false;
                dateNgaySinh.Enabled = false;
                cbGioiTinh.Enabled = false;

                // ✅ CHỈ CHO PHÉP THAY ĐỔI VAI TRÒ
                cbVaiTro.Enabled = true;
                cbVaiTro.Focus();

                // Đổi text nút Xác nhận
                btnXacNhan.Text = "Cập nhật";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load danh sách vai trò
        /// </summary>
        private void LoadVaiTro()
        {
            try
            {
                List<VaiTroDTO> danhSachVaiTro = phanQuyenBUS.GetAllVaiTro();

                cbVaiTro.DataSource = danhSachVaiTro;
                cbVaiTro.DisplayMember = "TenVaiTro";
                cbVaiTro.ValueMember = "MaVaiTro";

                if (danhSachVaiTro.Count > 0)
                {
                    cbVaiTro.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách vai trò: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ XỬ LÝ CHẾ ĐỘ SỬA
                if (isEditMode)
                {
                    if (cbVaiTro.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string maVaiTroMoi = cbVaiTro.SelectedValue.ToString();
                    string tenVaiTroMoi = cbVaiTro.Text;

                    // Xác nhận
                    DialogResult editResult = MessageBox.Show(
                        $"Bạn có chắc chắn muốn thay đổi vai trò của tài khoản '{editTenDangNhap}'?\n" +
                        $"Vai trò mới: {tenVaiTroMoi}\n",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (editResult == DialogResult.Yes)
                    {
                        // ✅ Cập nhật vai trò và đồng bộ LoaiDoiTuong
                        bool success = nguoiDungBLL.CapNhatVaiTroVaLoaiDoiTuong(editTenDangNhap, maVaiTroMoi);

                        if (success)
                        {
                            MessageBox.Show(
                                $"Cập nhật vai trò thành công!\n" +
                                $"Tài khoản: {editTenDangNhap}\n" +
                                $"Vai trò mới: {tenVaiTroMoi}\n",
                                "Thành công",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật vai trò thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    return;
                }

                // ✅ XỬ LÝ CHẾ ĐỘ THÊM MỚI - SỬA LẠI LOGIC

                // 1. Validate tên đăng nhập
                if (string.IsNullOrWhiteSpace(txtDangNhap.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDangNhap.Focus();
                    return;
                }

                // ✅ 2. Validate HỌ TÊN (BẮT BUỘC)
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }

                // Kiểm tra họ tên có ít nhất 2 từ
                string hoTen = txtHoTen.Text.Trim();
                if (hoTen.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 2)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ họ và tên (ít nhất 2 từ)!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }
                // ✅ 4. Validate SỐ ĐIỆN THOẠI với ValidationHelper
                string soDienThoai = txtNumber.Text.Trim();
                string phoneErrorMessage;
                if (!ValidationHelper.ValidatePhoneNumberWithMessage(soDienThoai, out phoneErrorMessage))
                {
                    MessageBox.Show($"Số điện thoại không hợp lệ!\n\n{phoneErrorMessage}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNumber.Focus();
                    return;
                }

                // ✅ 3. Validate EMAIL với ValidationHelper
                string email = txtEmail.Text.Trim();
                string emailErrorMessage;
                if (!ValidationHelper.ValidateEmailWithMessage(email, out emailErrorMessage))
                {
                    MessageBox.Show($"Email không hợp lệ!\n\n{emailErrorMessage}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // ✅ 5. Chuẩn hóa số điện thoại (chuyển +84 → 0)
                string soDienThoaiChuanHoa = ValidationHelper.ConvertToVietnameseFormat(soDienThoai);

                // 5. Validate vai trò
                if (cbVaiTro.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 6. Validate giới tính
                if (cbGioiTinh.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Lấy dữ liệu từ form
                string tenDangNhap = txtDangNhap.Text.Trim();
                string hoTenNguoiDung = txtHoTen.Text.Trim(); // ✅ LẤY TỪ txtHoTen
                DateTime ngaySinh = dateNgaySinh.Value;
                string gioiTinh = cbGioiTinh.SelectedItem.ToString();
                string maVaiTro = cbVaiTro.SelectedValue.ToString();

                // Xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn thêm tài khoản?\n\n" +
                    $"Tên đăng nhập: {tenDangNhap}\n" +
                    $"Họ và tên: {hoTenNguoiDung}\n" +
                    $"Email: {email}\n" +
                    $"Số điện thoại: {soDienThoai}\n" +
                    $"Vai trò: {cbVaiTro.Text}\n\n" +
                    $"Mật khẩu mặc định sẽ là ngày sinh: {ngaySinh.ToString("ddMMyyyy")}",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // ✅ Thêm tài khoản với HỌ TÊN từ txtHoTen
                    bool success = nguoiDungBLL.ThemTaiKhoan(
                        tenDangNhap,
                        hoTenNguoiDung,
                        email,
                        soDienThoaiChuanHoa, // ✅ Dùng số đã chuẩn hóa
                        ngaySinh,
                        gioiTinh,
                        maVaiTro);

                    if (success)
                    {
                        MessageBox.Show(
                            $"Thêm tài khoản thành công!\n\n" +
                            $"Tên đăng nhập: {tenDangNhap}\n" +
                            $"Họ và tên: {hoTenNguoiDung}\n" +
                            $"Mật khẩu: {ngaySinh.ToString("ddMMyyyy")}\n\n" +
                            $"⚠️ Vui lòng thông báo cho người dùng đổi mật khẩu sau lần đăng nhập đầu tiên!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm tài khoản thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
