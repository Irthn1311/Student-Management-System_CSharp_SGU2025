using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq; // Cần thêm using này

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ChinhSuaHocSinh : Form
    {
        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        // private LopHocBLL lopHocBLL; // Sẽ cần khi load cbLop từ DB
        // private PhanLopBLL phanLopBLL; // Sẽ cần khi load/lưu phân lớp

        private int maHocSinhToEdit;
        private HocSinhDTO currentHocSinh;
        private int originalMaPhuHuynh = -1;

        public ChinhSuaHocSinh(int maHocSinh)
        {
            InitializeComponent();

            // Khởi tạo tất cả các BLL cần thiết
            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            // lopHocBLL = new LopHocBLL();
            // phanLopBLL = new PhanLopBLL();

            this.maHocSinhToEdit = maHocSinh;

            // Load dữ liệu
            LoadComboBoxData(); // 1. Load TẤT CẢ lựa chọn vào ComboBox
            LoadHocSinhData();  // 2. Load thông tin HIỆN TẠI của học sinh
        }

        // 🌟 BƯỚC 1: NẠP TẤT CẢ LỰA CHỌN VÀO COMBOBOX
        private void LoadComboBoxData()
        {
            // --- Load ComboBox Phụ huynh (Dùng DataSource) ---
            try
            {
                List<PhuHuynhDTO> dsPhuHuynh = phuHuynhBLL.GetAllPhuHuynh();
                // Không thêm item mặc định "Chọn phụ huynh" ở đây
                // vì chúng ta sẽ chọn giá trị hiện tại của học sinh
                dsPhuHuynh.Insert(0, new PhuHuynhDTO { MaPhuHuynh = -1, HoTen = "Chọn phụ huynh" });
                cbPhuHuynh.DataSource = dsPhuHuynh;
                cbPhuHuynh.DisplayMember = "HoTen";
                cbPhuHuynh.ValueMember = "MaPhuHuynh";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phụ huynh: " + ex.Message);
            }

            // --- Load ComboBox Lớp học (Tạm thời) ---
            // TODO: Load từ LopHocBLL
            cbLop.Items.Clear();
            cbLop.Items.Add("10A1");
            cbLop.Items.Add("10A2");
            cbLop.Items.Add("11A1");
            cbLop.Items.Add("11A2");
            cbLop.Items.Add("12A1");
            cbLop.Items.Add("12A2");

            // --- Load ComboBox Mối Quan Hệ ---
            cbMoiQuanHe.Items.Clear();
            cbMoiQuanHe.Items.Add("Chọn mối quan hệ");
            cbMoiQuanHe.Items.Add("Bố");
            cbMoiQuanHe.Items.Add("Mẹ");
            cbMoiQuanHe.Items.Add("Người giám hộ");
        }

        // 🌟 BƯỚC 2: NẠP VÀ CHỌN THÔNG TIN HIỆN TẠI CỦA HỌC SINH
        private void LoadHocSinhData()
        {
            try
            {
                currentHocSinh = hocSinhBLL.GetHocSinhById(maHocSinhToEdit);

                if (currentHocSinh != null)
                {
                    // 1. Nạp thông tin cá nhân
                    // txtMaHS.Text = currentHocSinh.MaHS.ToString(); // Không cần vì đã có maHocSinhToEdit
                    txtHovaTen.Text = currentHocSinh.HoTen;
                    dateTimePickerNgaySinh.Value = currentHocSinh.NgaySinh;
                    if (currentHocSinh.GioiTinh == "Nam") rbNam.Checked = true;
                    else if (currentHocSinh.GioiTinh == "Nữ") rbNu.Checked = true;
                    txtSoDienThoai.Text = currentHocSinh.SdtHS;
                    txtEmail.Text = currentHocSinh.Email;
                    txtTrangThai.Text = currentHocSinh.TrangThai;

                    // 2. Nạp thông tin Phụ huynh & Mối quan hệ hiện tại
                    try
                    {
                        var dsQuanHe = hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(maHocSinhToEdit);
                        if (dsQuanHe != null && dsQuanHe.Any()) // Kiểm tra ds có rỗng không
                        {
                            // Lấy phụ huynh đầu tiên trong danh sách
                            var quanHeDauTien = dsQuanHe.First();
                            cbPhuHuynh.SelectedValue = quanHeDauTien.phuHuynh.MaPhuHuynh;
                            cbMoiQuanHe.SelectedItem = quanHeDauTien.moiQuanHe;

                            this.originalMaPhuHuynh = quanHeDauTien.phuHuynh.MaPhuHuynh;
                        }
                        else 
                        {
                            // Nếu KHÔNG CÓ quan hệ (đã bị xóa)
                            cbPhuHuynh.SelectedValue = -1; // Chọn item "--- Chọn phụ huynh ---"
                            cbMoiQuanHe.SelectedIndex = 0; // Chọn item "--- Chọn mối quan hệ ---"
                            this.originalMaPhuHuynh = -1; // Đặt mã gốc là -1
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải thông tin phụ huynh của học sinh: " + ex.Message);
                    }

                    // 3. Nạp thông tin Lớp hiện tại
                    // TODO: Bạn cần tạo hàm GetLopHienTaiCuaHocSinh trong PhanLopBLL
                    // LopHocDTO lopHienTai = phanLopBLL.GetLopHienTai(maHocSinhToEdit);
                    // if (lopHienTai != null) { cbLop.SelectedValue = lopHienTai.MaLop; }
                    // ----- Code mẫu tạm thời (giả sử HS 1 học 10A1) -----
                    if (maHocSinhToEdit == 1) cbLop.SelectedItem = "10A1";
                    // ----- Hết code mẫu -----

                }
                else
                {
                    MessageBox.Show($"Không tìm thấy thông tin học sinh với mã {maHocSinhToEdit}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin học sinh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }


        }

        // 🌟 BƯỚC 3: LƯU THÔNG TIN ĐÃ SỬA
        private void btnChinhSuaHocSinh_Click(object sender, EventArgs e)
        {
            HocSinhDTO updatedHs = new HocSinhDTO();
            try
            {
                // 1. Thu thập thông tin cá nhân
                updatedHs.MaHS = this.maHocSinhToEdit;
                updatedHs.HoTen = txtHovaTen.Text.Trim();
                updatedHs.NgaySinh = dateTimePickerNgaySinh.Value;
                updatedHs.GioiTinh = rbNam.Checked ? "Nam" : (rbNu.Checked ? "Nữ" : null);
                updatedHs.SdtHS = txtSoDienThoai.Text.Trim();
                updatedHs.Email = txtEmail.Text.Trim();
                updatedHs.TrangThai = txtTrangThai.Text.Trim(); // Lấy từ TextBox

                // Kiểm tra
                if (string.IsNullOrEmpty(updatedHs.GioiTinh))
                {
                    MessageBox.Show("Vui lòng chọn giới tính.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(updatedHs.TrangThai))
                {
                    MessageBox.Show("Vui lòng nhập trạng thái.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Thu thập thông tin Lớp và Phụ huynh đã sửa
                int selectedMaLop = -1; // TODO: Lấy MaLop từ cbLop.SelectedValue
                int selectedMaPhuHuynh = (int)cbPhuHuynh.SelectedValue;
                string moiQuanHe = cbMoiQuanHe.SelectedItem.ToString();
                // ... (Code lấy selectedMaLop từ cbLop) ...

                // 3. Gọi BLL để cập nhật

                // 3a. Cập nhật thông tin cá nhân
                bool updateHSSuccess = hocSinhBLL.UpdateHocSinh(updatedHs);
                bool updateQuanHeSuccess = true;

                // 3b. Cập nhật mối quan hệ PH (Ví dụ: Xóa cũ thêm mới, hoặc Update)
                try
                {
                    // Kiểm tra xem người dùng có chọn phụ huynh hợp lệ không
                    if (selectedMaPhuHuynh > 0)
                    {
                        // TH1: Người dùng ĐỔI sang một phụ huynh khác
                        if (this.originalMaPhuHuynh != -1 && this.originalMaPhuHuynh != selectedMaPhuHuynh)
                        {
                            // Xóa liên kết cũ
                            hocSinhPhuHuynhBLL.DeleteQuanHe(maHocSinhToEdit, this.originalMaPhuHuynh);
                            // Thêm liên kết mới
                            updateQuanHeSuccess = hocSinhPhuHuynhBLL.AddQuanHe(maHocSinhToEdit, selectedMaPhuHuynh, moiQuanHe);
                        }
                        // TH2: Người dùng chỉ cập nhật Mối quan hệ (Bố, Mẹ...) chứ không đổi phụ huynh
                        else if (this.originalMaPhuHuynh == selectedMaPhuHuynh)
                        {
                            updateQuanHeSuccess = hocSinhPhuHuynhBLL.UpdateQuanHe(maHocSinhToEdit, selectedMaPhuHuynh, moiQuanHe);
                        }
                        // TH3: Học sinh này ban đầu CHƯA CÓ phụ huynh (originalMaPhuHuynh = -1)
                        else if (this.originalMaPhuHuynh == -1)
                        {
                            // Thêm mới mối quan hệ
                            updateQuanHeSuccess = hocSinhPhuHuynhBLL.AddQuanHe(maHocSinhToEdit, selectedMaPhuHuynh, moiQuanHe);
                        }
                    }
                    // (Có thể thêm TH4: Người dùng bỏ chọn phụ huynh -> Xóa liên kết cũ)
                }
                catch (Exception exQH)
                {
                    updateQuanHeSuccess = false; // Đánh dấu là có lỗi xảy ra
                    MessageBox.Show("Lỗi khi cập nhật mối quan hệ phụ huynh: " + exQH.Message, "Lỗi phụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // 3c. Cập nhật Phân Lớp
                // int currentHocKy = ...;
                // bool updatePhanLopSuccess = phanLopBLL.UpdatePhanLop(maHocSinhToEdit, selectedMaLop, currentHocKy);


                if (updateHSSuccess) // Chỉ kiểm tra thành công của việc sửa thông tin cá nhân
                {
                    MessageBox.Show("Cập nhật thông tin học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException argEx) // Bắt lỗi validation từ BLL
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin:\n\n" + argEx.Message,
                                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) // Bắt các lỗi khác
            {
                MessageBox.Show("Đã xảy ra lỗi không mong muốn khi cập nhật: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChinhSuaHocSinh_Load(object sender, EventArgs e)
        {
            
        }
    }
}