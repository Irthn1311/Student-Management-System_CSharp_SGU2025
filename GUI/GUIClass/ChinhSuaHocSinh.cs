using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Cần thêm using này
using System.Windows.Forms;
using System.IO;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ChinhSuaHocSinh : Form
    {
        // BLLs
        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        private LopHocBUS lopHocBUS;
        private PhanLopBLL phanLopBLL;
        private HocKyBUS hocKyBUS;

        // Dữ liệu
        private int maHocSinhToEdit;
        private HocSinhDTO currentHocSinh;
        private List<PhuHuynhDTO> danhSachPhuHuynh; // Danh sách tổng
        private List<LopDTO> danhSachLop;
        private List<HocKyDTO> danhSachHocKy;

        // Biến để theo dõi thay đổi
        private int selectedMaPhuHuynh = -1; // ID phụ huynh MỚI do người dùng chọn
        private int originalMaPhuHuynh = -1; // ID phụ huynh GỐC
        
        // Biến để lưu đường dẫn ảnh mới (nếu người dùng chọn ảnh mới)
        private string newImagePath = null;

        // ✅ Property để trả về học sinh đã sửa
        public HocSinhDTO UpdatedHocSinh { get; private set; }

        // ✅ Error Provider để hiển thị lỗi validation
        private ErrorProvider errorProvider;

        public ChinhSuaHocSinh(int maHocSinh)
        {
            InitializeComponent();

            // Khởi tạo các lớp BLL
            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            lopHocBUS = new LopHocBUS();
            phanLopBLL = new PhanLopBLL();
            hocKyBUS = new HocKyBUS();

            // Khởi tạo danh sách
            danhSachPhuHuynh = new List<PhuHuynhDTO>();
            danhSachLop = new List<LopDTO>();
            danhSachHocKy = new List<HocKyDTO>();

            this.maHocSinhToEdit = maHocSinh;

            // ✅ Khởi tạo Error Provider với icon X đỏ nhỏ
            errorProvider = new ErrorProvider();
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            // Tạo icon X đỏ nhỏ hơn (16x16 thay vì 32x32 mặc định)
            using (Bitmap bmp = new Bitmap(16, 16))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawIcon(SystemIcons.Error, new Rectangle(0, 0, 16, 16));
                errorProvider.Icon = Icon.FromHandle(bmp.GetHicon());
            }

            // Chạy tuần tự
            LoadComboBoxData();       // 1. Nạp các lựa chọn Lớp, Học Kỳ
            LoadMasterPhuHuynhList(); // 2. Nạp danh sách tổng Phụ huynh (để dùng khi chọn)
            LoadHocSinhData();        // 3. Nạp thông tin CỤ THỂ của học sinh
            
            // ✅ Thiết lập Tab Order
            SetupTabOrder();
        }

        #region Nạp Dữ Liệu và Cấu Hình Bảng

        /// <summary>
        /// Nạp dữ liệu cho ComboBox Lớp và Học Kỳ
        /// </summary>
        private void LoadComboBoxData()
        {

            // --- Load ComboBox Mối Quan Hệ ---
            cbMoiQuanHe.Items.Clear();
            cbMoiQuanHe.Items.Add("Chọn mối quan hệ");
            cbMoiQuanHe.Items.Add("Cha");
            cbMoiQuanHe.Items.Add("Mẹ");
            cbMoiQuanHe.Items.Add("Ông");
            cbMoiQuanHe.Items.Add("Bà");
            cbMoiQuanHe.Items.Add("Người giám hộ");
        }

        /// <summary>
        /// ✅ Thiết lập thứ tự Tab cho các control
        /// </summary>
        private void SetupTabOrder()
        {
            // Đặt TabStop = false cho txtPhuHuynhDuocChon
            txtPhuHuynhDuocChon.TabStop = false;

            // Thiết lập thứ tự Tab
            int tabIndex = 0;
            txtHovaTen.TabIndex = tabIndex++;
            dateTimePickerNgaySinh.TabIndex = tabIndex++;
            rbNam.TabIndex = tabIndex++;
            rbNu.TabIndex = tabIndex++;
            txtSoDienThoai.TabIndex = tabIndex++;
            txtEmail.TabIndex = tabIndex++;
            txtTrangThai.TabIndex = tabIndex++;
            btnChon.TabIndex = tabIndex++;
            cbMoiQuanHe.TabIndex = tabIndex++;
            btnChinhSuaHocSinh.TabIndex = tabIndex++;
            btnHuy.TabIndex = tabIndex++;
        }

        /// <summary>
        /// ✅ Xóa tất cả lỗi Error Provider
        /// </summary>
        private void ClearAllErrors()
        {
            errorProvider.SetError(txtHovaTen, "");
            errorProvider.SetError(dateTimePickerNgaySinh, "");
            errorProvider.SetError(txtSoDienThoai, "");
            errorProvider.SetError(txtEmail, "");
            errorProvider.SetError(txtTrangThai, "");
            errorProvider.SetError(txtPhuHuynhDuocChon, "");
            errorProvider.SetError(cbMoiQuanHe, "");
            errorProvider.SetError(groupBoxGioiTinh, ""); // ✅ Xóa lỗi GroupBox giới tính
        }

        /// <summary>
        /// Nạp thông tin HIỆN TẠI của học sinh vào các control
        /// </summary>
        private void LoadHocSinhData()
        {
            try
            {
                currentHocSinh = hocSinhBLL.GetHocSinhById(maHocSinhToEdit);
                if (currentHocSinh == null)
                {
                    MessageBox.Show($"Không tìm thấy thông tin học sinh với mã {maHocSinhToEdit}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // 1. Nạp thông tin cá nhân
                txtHovaTen.Text = currentHocSinh.HoTen;
                dateTimePickerNgaySinh.Value = currentHocSinh.NgaySinh;
                if (currentHocSinh.GioiTinh == "Nam") rbNam.Checked = true;
                else if (currentHocSinh.GioiTinh == "Nữ") rbNu.Checked = true;
                txtSoDienThoai.Text = currentHocSinh.SdtHS;
                txtEmail.Text = currentHocSinh.Email;
                txtTrangThai.Text = currentHocSinh.TrangThai;
                
                // 1.1. Nạp ảnh đại diện học sinh
                LoadAnhHocSinh();

                // 2. Nạp thông tin Phụ huynh & Mối quan hệ hiện tại
                try
                {
                    var dsQuanHe = hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(maHocSinhToEdit);
                    if (dsQuanHe != null && dsQuanHe.Any())
                    {
                        var quanHeDauTien = dsQuanHe.First();

                        // Cập nhật TextBox và các biến theo dõi
                        txtPhuHuynhDuocChon.Text = quanHeDauTien.phuHuynh.HoTen;
                        txtPhuHuynhDuocChon.ForeColor = Color.Green;
                        this.selectedMaPhuHuynh = quanHeDauTien.phuHuynh.MaPhuHuynh;
                        this.originalMaPhuHuynh = quanHeDauTien.phuHuynh.MaPhuHuynh;

                        // Chọn ComboBox Mối quan hệ
                        cbMoiQuanHe.SelectedItem = quanHeDauTien.moiQuanHe;
                        if (cbMoiQuanHe.SelectedIndex == -1) cbMoiQuanHe.SelectedIndex = 0;
                    }
                    else
                    {
                        // Nếu không có phụ huynh
                        txtPhuHuynhDuocChon.Text = "Chưa chọn";
                        txtPhuHuynhDuocChon.ForeColor = Color.Red;
                        this.selectedMaPhuHuynh = -1;
                        this.originalMaPhuHuynh = -1;
                        cbMoiQuanHe.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải thông tin phụ huynh của học sinh: " + ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin học sinh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        #endregion

        #region Xử lý Sự kiện (Click, TextChanged...)

        /// <summary>
        /// Sự kiện chính: LƯU thông tin đã chỉnh sửa
        /// </summary>
        private void btnChinhSuaHocSinh_Click(object sender, EventArgs e)
        {
            // ✅ Xóa lỗi cũ trước khi validate
            ClearAllErrors();

            // ✅ Validate từng trường và hiển thị Error Provider
            bool hasError = false;

            // Kiểm tra Họ và Tên
            if (string.IsNullOrWhiteSpace(txtHovaTen.Text))
            {
                errorProvider.SetError(txtHovaTen, "Vui lòng nhập họ và tên học sinh");
                hasError = true;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(txtHovaTen.Text, @"\d"))
            {
                errorProvider.SetError(txtHovaTen, "Họ và tên không được chứa số");
                hasError = true;
            }

            // Kiểm tra Ngày sinh (từ 16 tuổi trở lên)
            if (dateTimePickerNgaySinh.Value >= DateTime.Now)
            {
                errorProvider.SetError(dateTimePickerNgaySinh, "Ngày sinh phải nhỏ hơn ngày hiện tại");
                hasError = true;
            }
            else
            {
                // Tính tuổi
                int age = DateTime.Now.Year - dateTimePickerNgaySinh.Value.Year;
                if (dateTimePickerNgaySinh.Value.Date > DateTime.Now.AddYears(-age))
                {
                    age--;
                }
                
                if (age < 16)
                {
                    errorProvider.SetError(dateTimePickerNgaySinh, "Học sinh phải từ 16 tuổi trở lên");
                    hasError = true;
                }
            }

            // Kiểm tra Số điện thoại (bắt buộc)
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                errorProvider.SetError(txtSoDienThoai, "Vui lòng nhập số điện thoại");
                hasError = true;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtSoDienThoai.Text, @"^[0-9]{10,11}$"))
            {
                errorProvider.SetError(txtSoDienThoai, "Số điện thoại phải là 10-11 chữ số");
                hasError = true;
            }

            // Kiểm tra Email (bắt buộc)
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Vui lòng nhập email");
                hasError = true;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider.SetError(txtEmail, "Email không hợp lệ");
                hasError = true;
            }

            // Kiểm tra Giới tính (hiển thị lỗi tại GroupBox)
            if (!rbNam.Checked && !rbNu.Checked)
            {
                errorProvider.SetError(groupBoxGioiTinh, "Vui lòng chọn giới tính");
                hasError = true;
            }

            // Kiểm tra Trạng thái
            if (string.IsNullOrWhiteSpace(txtTrangThai.Text))
            {
                errorProvider.SetError(txtTrangThai, "Vui lòng nhập trạng thái học sinh");
                hasError = true;
            }

            // Kiểm tra Phụ huynh
            if (this.selectedMaPhuHuynh <= 0)
            {
                errorProvider.SetError(txtPhuHuynhDuocChon, "Vui lòng chọn phụ huynh từ danh sách");
                btnChon.Focus();
                hasError = true;
            }

            // Kiểm tra Mối quan hệ
            if (cbMoiQuanHe.SelectedIndex <= 0 || cbMoiQuanHe.SelectedItem.ToString() == "Chọn mối quan hệ")
            {
                errorProvider.SetError(cbMoiQuanHe, "Vui lòng chọn mối quan hệ");
                cbMoiQuanHe.Focus();
                hasError = true;
            }

            // ✅ Nếu có lỗi, dừng lại và hiển thị thông báo tổng hợp
            if (hasError)
            {
                MessageBox.Show("Vui lòng kiểm tra lại các trường đánh dấu đỏ.", "Dữ liệu không hợp lệ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. Thu thập dữ liệu MỚI ---
            HocSinhDTO updatedHs = new HocSinhDTO();
            try
            {
                // 2a. Thông tin cá nhân
                updatedHs.MaHS = this.maHocSinhToEdit;
                updatedHs.HoTen = txtHovaTen.Text.Trim();
                updatedHs.NgaySinh = dateTimePickerNgaySinh.Value;
                updatedHs.GioiTinh = rbNam.Checked ? "Nam" : (rbNu.Checked ? "Nữ" : null);
                updatedHs.SdtHS = txtSoDienThoai.Text.Trim();
                updatedHs.Email = txtEmail.Text.Trim();
                updatedHs.TrangThai = txtTrangThai.Text.Trim();
                
                // ✅ QUAN TRỌNG: Giữ nguyên TenDangNhap từ học sinh gốc
                updatedHs.TenDangNhap = currentHocSinh.TenDangNhap;
                
                // ✅ Cập nhật ảnh đại diện (nếu có thay đổi)
                if (!string.IsNullOrEmpty(newImagePath))
                {
                    updatedHs.AnhDaiDien = newImagePath;
                }
                else
                {
                    // Giữ nguyên ảnh cũ
                    updatedHs.AnhDaiDien = currentHocSinh.AnhDaiDien;
                }

                // 2b. Thông tin quan hệ & phân lớp MỚI
                int newSelectedMaPhuHuynh = this.selectedMaPhuHuynh;
                string newMoiQuanHe = cbMoiQuanHe.SelectedItem.ToString();

                

                // --- 3. Gọi BLL để cập nhật ---
                string warningMessage = "";

                // Bước 1: Cập nhật thông tin cá nhân học sinh
                bool updateHSSuccess = hocSinhBLL.UpdateHocSinh(updatedHs);
                if (!updateHSSuccess)
                {
                    warningMessage += "Cập nhật thông tin cá nhân thất bại.\n";
                }
                else
                {
                    // ✅ Lưu lại học sinh đã cập nhật
                    this.UpdatedHocSinh = updatedHs;
                }

                // Bước 2: Cập nhật Mối Quan Hệ
                try
                {
                    // TH1: Đổi phụ huynh
                    if (this.originalMaPhuHuynh != -1 && this.originalMaPhuHuynh != newSelectedMaPhuHuynh)
                    {
                        hocSinhPhuHuynhBLL.DeleteQuanHe(maHocSinhToEdit, this.originalMaPhuHuynh);
                        hocSinhPhuHuynhBLL.AddQuanHe(maHocSinhToEdit, newSelectedMaPhuHuynh, newMoiQuanHe);
                    }
                    // TH2: Giữ nguyên phụ huynh, chỉ đổi mối quan hệ (Cha -> Mẹ)
                    else if (this.originalMaPhuHuynh == newSelectedMaPhuHuynh)
                    {
                        hocSinhPhuHuynhBLL.UpdateQuanHe(maHocSinhToEdit, newSelectedMaPhuHuynh, newMoiQuanHe);
                    }
                    // TH3: Trước đó không có, giờ thêm mới
                    else if (this.originalMaPhuHuynh == -1)
                    {
                        hocSinhPhuHuynhBLL.AddQuanHe(maHocSinhToEdit, newSelectedMaPhuHuynh, newMoiQuanHe);
                    }
                    // TH4: (Tùy chọn) Bỏ chọn phụ huynh -> Xóa (Tùy logic, ở đây ta bắt buộc chọn nên bỏ qua)
                }
                catch (Exception exQH)
                {
                    warningMessage += $"Lỗi khi cập nhật mối quan hệ: {exQH.Message}\n";
                }

                // --- 4. Thông báo kết quả ---
                if (string.IsNullOrEmpty(warningMessage))
                {
                    MessageBox.Show("Cập nhật học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Đã xảy ra lỗi/cảnh báo:\n\n{warningMessage}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (updateHSSuccess) // Nếu thông tin cá nhân vẫn đúng
                    {
                        this.DialogResult = DialogResult.OK; // Vẫn đóng form
                        this.Close();
                    }
                }
            }
            catch (ArgumentException argEx) // Bắt lỗi validation
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Mở dialog để chọn phụ huynh từ danh sách
        /// </summary>
        private void btnChon_Click(object sender, EventArgs e)
        {
            ChonPhuHuynhTuDialog();
        }

        /// <summary>
        /// Hiển thị dialog đơn giản để chọn phụ huynh
        /// </summary>
        private void ChonPhuHuynhTuDialog()
        {
            if (danhSachPhuHuynh == null || danhSachPhuHuynh.Count == 0)
            {
                MessageBox.Show("Danh sách phụ huynh trống. Vui lòng thêm phụ huynh trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Tạo form dialog đơn giản
            Form dialogForm = new Form
            {
                Text = "Chọn phụ huynh",
                Size = new Size(500, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Tạo ListBox để hiển thị danh sách phụ huynh
            ListBox listBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F)
            };

            // Thêm danh sách phụ huynh vào ListBox với format hiển thị
            foreach (var ph in danhSachPhuHuynh)
            {
                listBox.Items.Add(new { ph.MaPhuHuynh, DisplayText = $"{ph.HoTen} - {ph.SoDienThoai}" });
            }

            // Chọn phụ huynh hiện tại nếu có
            if (this.selectedMaPhuHuynh > 0)
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    var item = listBox.Items[i];
                    var maPH = item.GetType().GetProperty("MaPhuHuynh").GetValue(item);
                    if (maPH != null && Convert.ToInt32(maPH) == this.selectedMaPhuHuynh)
                    {
                        listBox.SelectedIndex = i;
                        break;
                    }
                }
            }

            // Panel chứa các nút
            Panel panelButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            Button btnOK = new Button
            {
                Text = "Chọn",
                DialogResult = DialogResult.OK,
                Size = new Size(100, 35),
                Location = new Point(dialogForm.Width - 220, 10),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            Button btnCancel = new Button
            {
                Text = "Hủy",
                DialogResult = DialogResult.Cancel,
                Size = new Size(100, 35),
                Location = new Point(dialogForm.Width - 110, 10),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            panelButtons.Controls.Add(btnOK);
            panelButtons.Controls.Add(btnCancel);
            dialogForm.Controls.Add(listBox);
            dialogForm.Controls.Add(panelButtons);
            dialogForm.AcceptButton = btnOK;
            dialogForm.CancelButton = btnCancel;

            // Hiển thị dialog
            if (dialogForm.ShowDialog(this) == DialogResult.OK && listBox.SelectedItem != null)
            {
                try
                {
                    var selectedItem = listBox.SelectedItem;
                    int maPH = Convert.ToInt32(selectedItem.GetType().GetProperty("MaPhuHuynh").GetValue(selectedItem));
                    string displayText = selectedItem.GetType().GetProperty("DisplayText").GetValue(selectedItem).ToString();
                    string hoTen = displayText.Split('-')[0].Trim();

                    // Lưu lại ID MỚI và hiển thị tên
                    this.selectedMaPhuHuynh = maPH;
                    txtPhuHuynhDuocChon.Text = hoTen;
                    txtPhuHuynhDuocChon.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn phụ huynh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.selectedMaPhuHuynh = -1;
                    txtPhuHuynhDuocChon.Text = "Chưa chọn";
                    txtPhuHuynhDuocChon.ForeColor = Color.Red;
                }
            }

            dialogForm.Dispose();
        }

        /// <summary>
        /// Load ảnh học sinh hiện tại
        /// </summary>
        private void LoadAnhHocSinh()
        {
            try
            {
                if (currentHocSinh == null) return;
                
                // Sử dụng PictureBox đã được thêm vào form
                if (picAnhHocSinh != null)
                {
                    ChinhSuaHocSinhImageHelper.LoadAnhHocSinh(picAnhHocSinh, currentHocSinh.AnhDaiDien, currentHocSinh.MaHS);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load ảnh học sinh: {ex.Message}");
            }
        }

        /// <summary>
        /// Xử lý khi người dùng click button chọn ảnh mới
        /// </summary>
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            try
            {
                string newPath = ChinhSuaHocSinhImageHelper.ChonAnhMoi(maHocSinhToEdit);
                if (!string.IsNullOrEmpty(newPath))
                {
                    newImagePath = newPath;
                    
                    // Cập nhật PictureBox
                    if (picAnhHocSinh != null)
                    {
                        ChinhSuaHocSinhImageHelper.LoadAnhHocSinh(picAnhHocSinh, newPath, maHocSinhToEdit);
                    }
                    
                    MessageBox.Show("Đã chọn ảnh mới. Nhấn 'Sửa học sinh' để lưu thay đổi.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Hàm Style (Sao chép từ form Thêm)

        private void LoadMasterPhuHuynhList()
        {
            try
            {
                danhSachPhuHuynh = phuHuynhBLL.GetAllPhuHuynh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phụ huynh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                danhSachPhuHuynh = new List<PhuHuynhDTO>();
            }
        }

        private void ApplyBaseTableStyle(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ColumnHeadersHeight = 42;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 229, 255); // Màu chọn
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.RowTemplate.Height = 46;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
        }

        private void ApplyColumnAlignmentAndWrapping(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }

        private void ChinhSuaHocSinh_Load(object sender, EventArgs e)
        {
            // (Đã chuyển logic vào Constructor)
        }

        #endregion
    }

    /// <summary>
    /// Helper class để xử lý ảnh trong form ChinhSuaHocSinh
    /// </summary>
    public static class ChinhSuaHocSinhImageHelper
    {
        /// <summary>
        /// Load ảnh học sinh vào PictureBox
        /// </summary>
        public static void LoadAnhHocSinh(PictureBox picBox, string anhDaiDien, int maHS)
        {
            try
            {
                if (picBox == null) return;

                string duongDanAnh = anhDaiDien;

                // Nếu chưa có ảnh, tự động phân bổ dựa trên MaHS
                if (string.IsNullOrWhiteSpace(duongDanAnh))
                {
                    int soAnh = ((maHS - 1) % 4) + 1;
                    duongDanAnh = $"Images/Students/hs{soAnh}.jpg";
                }

                // Tải ảnh từ đường dẫn
                string fullPath = Path.Combine(Application.StartupPath, duongDanAnh);

                // Nếu file không tồn tại ở đường dẫn absolute, thử đường dẫn relative
                if (!File.Exists(fullPath))
                {
                    fullPath = duongDanAnh;
                }

                if (File.Exists(fullPath))
                {
                    // Dispose ảnh cũ nếu có để tránh memory leak
                    if (picBox.Image != null)
                    {
                        Image oldImage = picBox.Image;
                        picBox.Image = null;
                        oldImage.Dispose();
                    }

                    picBox.Image = Image.FromFile(fullPath);
                    picBox.SizeMode = PictureBoxSizeMode.Zoom;
                    picBox.BackColor = Color.White;
                }
                else
                {
                    // Nếu không tìm thấy ảnh, hiển thị placeholder
                    if (picBox.Image != null)
                    {
                        Image oldImage = picBox.Image;
                        picBox.Image = null;
                        oldImage.Dispose();
                    }
                    picBox.Image = null;
                    picBox.BackColor = Color.FromArgb(240, 240, 240);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải ảnh học sinh: {ex.Message}");
                if (picBox != null)
                {
                    if (picBox.Image != null)
                    {
                        Image oldImage = picBox.Image;
                        picBox.Image = null;
                        oldImage.Dispose();
                    }
                    picBox.Image = null;
                    picBox.BackColor = Color.FromArgb(240, 240, 240);
                }
            }
        }

        /// <summary>
        /// Mở dialog để chọn ảnh mới và copy vào thư mục Images/Students
        /// </summary>
        /// <returns>Đường dẫn ảnh mới (relative path) hoặc null nếu hủy</returns>
        public static string ChonAnhMoi(int maHS)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*",
                    Title = "Chọn ảnh đại diện học sinh",
                    RestoreDirectory = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sourcePath = openFileDialog.FileName;
                    string fileName = $"hs_{maHS}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(sourcePath)}";
                    string targetDir = Path.Combine(Application.StartupPath, "Images", "Students");
                    string targetPath = Path.Combine(targetDir, fileName);

                    // Tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(targetDir))
                    {
                        Directory.CreateDirectory(targetDir);
                    }

                    // Copy file ảnh vào thư mục Images/Students
                    File.Copy(sourcePath, targetPath, true);

                    // Trả về đường dẫn relative
                    return $"Images/Students/{fileName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }
    }
}