using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing; // Cần cho Color

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ThemHoSoHocSinh : Form
    {
        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        // private LopHocBLL lopHocBLL; // Sẽ cần khi load cbLop từ DB
        // private PhanLopBLL phanLopBLL; // Sẽ cần khi thêm phân lớp

        public ThemHoSoHocSinh()
        {
            InitializeComponent();

            // Khởi tạo các lớp BLL
            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            // lopHocBLL = new LopHocBLL();
            // phanLopBLL = new PhanLopBLL();

            // Load dữ liệu cho ComboBox khi form khởi tạo
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            // --- Load ComboBox Phụ huynh (Dùng DataSource) ---
            try
            {
                List<PhuHuynhDTO> dsPhuHuynh = phuHuynhBLL.GetAllPhuHuynh();
                dsPhuHuynh.Insert(0, new PhuHuynhDTO { MaPhuHuynh = -1, HoTen = "Chọn phụ huynh" });

                cbPhuHuynh.DataSource = dsPhuHuynh;
                cbPhuHuynh.DisplayMember = "HoTen";
                cbPhuHuynh.ValueMember = "MaPhuHuynh";
                cbPhuHuynh.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phụ huynh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbPhuHuynh.DataSource = null;
                cbPhuHuynh.Items.Clear();
                cbPhuHuynh.Items.Add("Lỗi tải dữ liệu");
                cbPhuHuynh.SelectedIndex = 0;
            }

            // --- Load ComboBox Lớp học ---
            // TODO: Thay thế bằng cách load từ LopHocBLL khi có
            cbLop.Items.Clear();
            cbLop.Items.Add("Chọn lớp học");
            cbLop.Items.Add("10A1");
            cbLop.Items.Add("10A2");
            cbLop.Items.Add("11A1");
            cbLop.Items.Add("11A2");
            cbLop.Items.Add("12A1");
            cbLop.Items.Add("12A2");
            cbLop.SelectedIndex = 0;

            // --- Load ComboBox Mối Quan Hệ ---
            cbMoiQuanHe.Items.Clear();
            cbMoiQuanHe.Items.Add("Chọn mối quan hệ");
            cbMoiQuanHe.Items.Add("Bố");
            cbMoiQuanHe.Items.Add("Mẹ");
            cbMoiQuanHe.Items.Add("Người giám hộ");
            cbMoiQuanHe.SelectedIndex = 0;

            // Không cần load cbGioiTinh vì dùng RadioButton
        }

        private void ThemHoSoHocSinh_Load(object sender, EventArgs e)
        {
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form hiện tại
        }

        

        private void btnThemHocSinh_Click_1(object sender, EventArgs e)
        {
            // --- 1. Kiểm tra các lựa chọn ---
            if (cbLop.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn lớp học.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLop.Focus();
                return;
            }
            if (cbPhuHuynh.SelectedValue == null || !(cbPhuHuynh.SelectedValue is int) || (int)cbPhuHuynh.SelectedValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn phụ huynh.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbPhuHuynh.Focus();
                return;
            }
            if (cbMoiQuanHe.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn mối quan hệ.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbMoiQuanHe.Focus();
                return;
            }
            if (rbNam.Checked == false && rbNu.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // --- 2. Thu thập dữ liệu từ Form ---
            HocSinhDTO hs = new HocSinhDTO();
            try
            {
                hs.HoTen = txtHovaTen.Text.Trim();
                hs.NgaySinh = dateTimePickerNgaySinh.Value;
                hs.GioiTinh = rbNam.Checked ? "Nam" : "Nữ";
                hs.SdtHS = txtSoDienThoai.Text.Trim();
                hs.Email = txtEmail.Text.Trim();
                hs.TrangThai = "Đang học"; // Mặc định

                int selectedMaPhuHuynh = (int)cbPhuHuynh.SelectedValue;
                string moiQuanHe = cbMoiQuanHe.SelectedItem.ToString();
                int selectedMaLop = -1;
                string selectedTenLop = cbLop.SelectedItem.ToString();

                // TODO: Thay thế bằng cách lấy MaLop từ BLL
                // selectedMaLop = lopHocBLL.GetMaLopByTenLop(selectedTenLop);
                // ----- Code mẫu tạm thời -----
                if (selectedTenLop == "10A1") selectedMaLop = 1;
                else if (selectedTenLop == "10A2") selectedMaLop = 2;
                else if (selectedTenLop == "11A1") selectedMaLop = 3;
                else if (selectedTenLop == "11A2") selectedMaLop = 4;
                else if (selectedTenLop == "12A1") selectedMaLop = 5;
                else if (selectedTenLop == "12A2") selectedMaLop = 6;
                // ----- Hết code mẫu -----
                if (selectedMaLop == -1)
                {
                    MessageBox.Show("Lớp học không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // --- 3. Gọi BLL để thêm (BLL tự validate) ---
                int newMaHocSinh = hocSinhBLL.AddHocSinh(hs);

                if (newMaHocSinh > 0)
                {
                    // Thêm Mối Quan Hệ
                    bool addQuanHeSuccess = hocSinhPhuHuynhBLL.AddQuanHe(newMaHocSinh, selectedMaPhuHuynh, moiQuanHe);
                    if (!addQuanHeSuccess)
                    {
                        MessageBox.Show($"Đã thêm học sinh (Mã: {newMaHocSinh}) nhưng không thể thêm mối quan hệ phụ huynh.\nVui lòng kiểm tra hoặc thêm thủ công sau.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Vẫn tiếp tục thêm phân lớp nếu có
                    }

                    // Thêm Phân Lớp (Cần có PhanLopBLL và MaHocKy)
                    try
                    {
                        // TODO: Lấy MaHocKy hiện tại (ví dụ từ ComboBox hoặc cài đặt)
                        // int currentMaHocKy = ... ;
                        // bool addPhanLopSuccess = phanLopBLL.AddPhanLop(newMaHocSinh, selectedMaLop, currentMaHocKy);
                        // if (!addPhanLopSuccess)
                        // {
                        //     MessageBox.Show($"Không thể tự động phân lớp cho học sinh (Mã: {newMaHocSinh}).\nVui lòng phân lớp thủ công.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // }
                    }
                    catch (Exception exPL)
                    {
                        MessageBox.Show($"Lỗi khi tự động phân lớp: {exPL.Message}", "Lỗi Phân Lớp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                    MessageBox.Show("Thêm hồ sơ học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Đặt kết quả thành công
                    this.Close(); // Đóng form
                }
                else
                {
                    MessageBox.Show("Thêm học sinh thất bại. Vui lòng kiểm tra lại thông tin hoặc thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException argEx) // Bắt lỗi validation từ BLL
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin:\n\n" + argEx.Message,
                                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException nrEx)
            {
                MessageBox.Show("Lỗi: Control trên form chưa được khởi tạo đúng cách hoặc giá trị null không mong muốn.\n" + nrEx.Message, "Lỗi Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // Bắt các lỗi khác
            {
                MessageBox.Show("Đã xảy ra lỗi không mong muốn trong quá trình thêm: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}