using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;

using System.Collections.Generic;

using System.Drawing; // Cần cho Color
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ThemHoSoHocSinh : Form
    {
        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        private LopHocBUS lopHocBUS;
        private PhanLopBLL phanLopBLL;
        private HocKyBUS hocKyBUS;
        private List<PhuHuynhDTO> danhSachPhuHuynh;
        private List<LopDTO> danhSachLop;
        private List<HocKyDTO> danhSachHocKy;

        // Biến để lưu ID phụ huynh đã chọn
        private int selectedMaPhuHuynh = -1;
        
        // Biến để phân biệt: true = nhập mới phụ huynh, false = chọn phụ huynh có sẵn
        private bool isNhapPhuHuynhMoi = false;
        
        // Thông tin phụ huynh mới (nếu nhập mới)
        private PhuHuynhDTO phuHuynhMoi = null;

        // ✅ Property để trả về học sinh vừa tạo
        public HocSinhDTO NewHocSinh { get; private set; }

        // ✅ Error Provider để hiển thị lỗi validation
        private ErrorProvider errorProvider;

        public ThemHoSoHocSinh()
        {
            InitializeComponent();

            // Khởi tạo các lớp BLL
            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            lopHocBUS = new LopHocBUS();
            phanLopBLL = new PhanLopBLL();
            hocKyBUS = new HocKyBUS();
            danhSachPhuHuynh = new List<PhuHuynhDTO>();
            danhSachLop = new List<LopDTO>();
            danhSachHocKy = new List<HocKyDTO>();

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

            LoadMasterPhuHuynhList(); // Tải danh sách phụ huynh gốc để dùng khi chọn
            
            // ✅ Thiết lập Tab Order
            SetupTabOrder();
        }


        /// <summary>
        /// ✅ Thiết lập thứ tự Tab cho các control
        /// </summary>
        private void SetupTabOrder()
        {
            // Đặt TabStop = false cho txtPhuHuynhDuocChon
            //txtPhuHuynhDuocChon.TabStop = false;
            
            // Đặt TabStop = false cho các RadioButton (sử dụng mũi tên để chọn)
            rbChonPhuHuynh.TabStop = true;
            rbNhapPhuHuynhMoi.TabStop = true;

            // Thiết lập thứ tự Tab
            int tabIndex = 0;
            txtHovaTen.TabIndex = tabIndex++;
            dateTimePickerNgaySinh.TabIndex = tabIndex++;
            rbNam.TabIndex = tabIndex++;
            rbNu.TabIndex = tabIndex++;
            txtSoDienThoai.TabIndex = tabIndex++;
            txtEmail.TabIndex = tabIndex++;
            rbChonPhuHuynh.TabIndex = tabIndex++;
            rbNhapPhuHuynhMoi.TabIndex = tabIndex++;
            btnChon.TabIndex = tabIndex++;
            txtHovaTenPH.TabIndex = tabIndex++;
            txtSoDienThoaiPH.TabIndex = tabIndex++;
            txtEmailPH.TabIndex = tabIndex++;
            txtDiaChiPH.TabIndex = tabIndex++;
            cbMoiQuanHe.TabIndex = tabIndex++;
            btnThemHocSinh.TabIndex = tabIndex++;
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
            //errorProvider.SetError(txtPhuHuynhDuocChon, "");
            errorProvider.SetError(cbMoiQuanHe, "");
            errorProvider.SetError(groupBoxGioiTinh, ""); // ✅ Xóa lỗi GroupBox giới tính
            // Xóa lỗi các trường phụ huynh mới
            errorProvider.SetError(txtHovaTenPH, "");
            errorProvider.SetError(txtSoDienThoaiPH, "");
            errorProvider.SetError(txtEmailPH, "");
            errorProvider.SetError(txtDiaChiPH, "");
        }

        /// <summary>
        /// Tải danh sách phụ huynh gốc vào biến 'danhSachPhuHuynh'
        /// </summary>
        private void LoadMasterPhuHuynhList()
        {
            try
            {
                danhSachPhuHuynh = phuHuynhBLL.GetAllPhuHuynh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phụ huynh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                danhSachPhuHuynh = new List<PhuHuynhDTO>(); // Khởi tạo rỗng
            }
        }


        private void ApplyBaseTableStyle(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Mặc định căn giữa header
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ColumnHeadersHeight = 42; // Tăng chiều cao header

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
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

        // Hàm căn chỉnh cột và wrap text
        private void ApplyColumnAlignmentAndWrapping(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Mặc định căn giữa cell
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            }
        }


        private void ThemHoSoHocSinh_Load(object sender, EventArgs e)
        {
            // Mặc định chọn "Chọn phụ huynh có sẵn"
            rbChonPhuHuynh.Checked = true;
            UpdatePhuHuynhUI();
        }

        /// <summary>
        /// Xử lý khi thay đổi RadioButton phụ huynh
        /// </summary>
        private void rbPhuHuynh_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePhuHuynhUI();
        }

        /// <summary>
        /// Cập nhật UI dựa trên RadioButton được chọn
        /// </summary>
        private void UpdatePhuHuynhUI()
        {
            if (rbChonPhuHuynh.Checked)
            {
                // Hiển thị phần chọn phụ huynh có sẵn
                //txtPhuHuynhDuocChon.Visible = true;
                btnChon.Visible = true;
                panelNhapPhuHuynhMoi.Visible = false;
                isNhapPhuHuynhMoi = false;
                
                // Xóa dữ liệu phụ huynh mới nếu có
                ClearPhuHuynhMoiFields();
            }
            else if (rbNhapPhuHuynhMoi.Checked)
            {
                // Hiển thị phần nhập phụ huynh mới
                btnChon.Visible = false;
                panelNhapPhuHuynhMoi.Visible = true;
                panelThongTinPhuHuynh.Visible = false; // Ẩn panel thông tin phụ huynh
                isNhapPhuHuynhMoi = true;
                
                // Reset selectedMaPhuHuynh
                selectedMaPhuHuynh = -1;
            }
        }

        /// <summary>
        /// Xóa các trường nhập phụ huynh mới
        /// </summary>
        private void ClearPhuHuynhMoiFields()
        {
            txtHovaTenPH.Text = "";
            txtSoDienThoaiPH.Text = "";
            txtEmailPH.Text = "";
            txtDiaChiPH.Text = "";
            phuHuynhMoi = null;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // Đóng form hiện tại
        }

        

        private void btnThemHocSinh_Click_1(object sender, EventArgs e)
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

            // Kiểm tra Phụ huynh
            if (rbChonPhuHuynh.Checked)
            {
                // Kiểm tra nếu chọn phụ huynh có sẵn
                if (this.selectedMaPhuHuynh <= 0)
                {
                    //errorProvider.SetError(txtPhuHuynhDuocChon, "Vui lòng chọn phụ huynh từ danh sách");
                    btnChon.Focus();
                    hasError = true;
                }
            }
            else if (rbNhapPhuHuynhMoi.Checked)
            {
                // Kiểm tra nếu nhập phụ huynh mới
                if (string.IsNullOrWhiteSpace(txtHovaTenPH.Text))
                {
                    errorProvider.SetError(txtHovaTenPH, "Vui lòng nhập họ và tên phụ huynh");
                    hasError = true;
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(txtHovaTenPH.Text, @"\d"))
                {
                    errorProvider.SetError(txtHovaTenPH, "Họ và tên không được chứa số");
                    hasError = true;
                }

                if (string.IsNullOrWhiteSpace(txtSoDienThoaiPH.Text))
                {
                    errorProvider.SetError(txtSoDienThoaiPH, "Vui lòng nhập số điện thoại phụ huynh");
                    hasError = true;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(txtSoDienThoaiPH.Text, @"^[0-9]{10,11}$"))
                {
                    errorProvider.SetError(txtSoDienThoaiPH, "Số điện thoại phải là 10-11 chữ số");
                    hasError = true;
                }

                if (string.IsNullOrWhiteSpace(txtEmailPH.Text))
                {
                    errorProvider.SetError(txtEmailPH, "Vui lòng nhập email phụ huynh");
                    hasError = true;
                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmailPH.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    errorProvider.SetError(txtEmailPH, "Email không hợp lệ");
                    hasError = true;
                }

                if (string.IsNullOrWhiteSpace(txtDiaChiPH.Text))
                {
                    errorProvider.SetError(txtDiaChiPH, "Vui lòng nhập địa chỉ phụ huynh");
                    hasError = true;
                }
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

                // Lấy thông tin quan hệ và phân lớp
                string moiQuanHe = cbMoiQuanHe.SelectedItem.ToString();

                // --- 3. Xử lý Phụ huynh trước khi thêm học sinh ---
                int maPhuHuynhToUse = -1;
                
                if (rbNhapPhuHuynhMoi.Checked)
                {
                    // Tạo phụ huynh mới
                    PhuHuynhDTO phMoi = new PhuHuynhDTO();
                    phMoi.HoTen = txtHovaTenPH.Text.Trim();
                    phMoi.SoDienThoai = txtSoDienThoaiPH.Text.Trim();
                    phMoi.Email = txtEmailPH.Text.Trim();
                    phMoi.DiaChi = txtDiaChiPH.Text.Trim();

                    // Thêm phụ huynh mới vào database
                    if (phuHuynhBLL.AddPhuHuynh(phMoi))
                    {
                        // Lấy lại phụ huynh vừa thêm để lấy MaPhuHuynh
                        var allPH = phuHuynhBLL.GetAllPhuHuynh();
                        var phVuaThem = allPH.FirstOrDefault(p => 
                            p.SoDienThoai == phMoi.SoDienThoai && 
                            p.Email == phMoi.Email &&
                            p.HoTen == phMoi.HoTen);
                        
                        if (phVuaThem != null)
                        {
                            maPhuHuynhToUse = phVuaThem.MaPhuHuynh;
                            this.phuHuynhMoi = phVuaThem;
                        }
                        else
                        {
                            MessageBox.Show("Đã thêm phụ huynh nhưng không thể lấy mã phụ huynh. Vui lòng thử lại.", 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm phụ huynh mới. Vui lòng kiểm tra lại thông tin.", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Sử dụng phụ huynh đã chọn
                    maPhuHuynhToUse = this.selectedMaPhuHuynh;
                }

                if (maPhuHuynhToUse <= 0)
                {
                    MessageBox.Show("Không thể xác định phụ huynh. Vui lòng kiểm tra lại.", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // --- 4. Gọi BLL để thêm (Thực hiện tuần tự) ---

                // Bước 1: Thêm Học Sinh
                int newMaHocSinh = hocSinhBLL.AddHocSinh(hs);

                if (newMaHocSinh > 0)
                {
                    // ✅ Lưu lại MaHS vào object để trả về
                    hs.MaHS = newMaHocSinh;
                    this.NewHocSinh = hs;

                    bool allSuccess = true;
                    string warningMessage = "";

                    // Bước 2: Thêm Mối Quan Hệ
                    try
                    {
                        bool addQuanHeSuccess = hocSinhPhuHuynhBLL.AddQuanHe(newMaHocSinh, maPhuHuynhToUse, moiQuanHe);
                        if (!addQuanHeSuccess)
                        {
                            warningMessage += "Không thể thêm mối quan hệ phụ huynh.\n";
                            allSuccess = false;
                        }
                    }
                    catch (Exception exQH)
                    {
                        warningMessage += $"Lỗi khi thêm mối quan hệ: {exQH.Message}\n";
                        allSuccess = false;
                    }


                    // --- 4. Thông báo kết quả ---
                    if (allSuccess)
                    {
                        MessageBox.Show("Thêm hồ sơ học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK; // Đặt kết quả thành công
                        this.Close(); // Đóng form
                    }
                    else
                    {
                        // Thêm HS thành công, nhưng lỗi quan hệ/phân lớp
                        MessageBox.Show($"Đã thêm học sinh (Mã: {newMaHocSinh}) nhưng có lỗi xảy ra:\n\n{warningMessage}\nVui lòng kiểm tra và thêm thủ công các mục bị lỗi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.OK; // Vẫn là OK vì HS đã được tạo
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Thêm học sinh thất bại. Vui lòng kiểm tra lại thông tin hoặc thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException argEx) // Bắt lỗi validation từ BLL.AddHocSinh
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin:\n\n" + argEx.Message,
                                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) // Bắt các lỗi khác
            {
                MessageBox.Show("Đã xảy ra lỗi không mong muốn trong quá trình thêm: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbPhuHuynh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Mở dialog để chọn phụ huynh hoặc thêm mới
        /// </summary>
        private void btnChon_Click(object sender, EventArgs e)
        {
            ChonPhuHuynhTuDialog();
        }

        /// <summary>
        /// Hiển thị dialog đầy đủ để chọn phụ huynh với tùy chọn thêm mới
        /// </summary>
        private void ChonPhuHuynhTuDialog()
        {
            using (ChonPhuHuynhDialog dialog = new ChonPhuHuynhDialog(this.selectedMaPhuHuynh))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    // Xử lý phụ huynh đã chọn
                    if (dialog.SelectedPhuHuynh != null)
                    {
                        this.selectedMaPhuHuynh = dialog.SelectedPhuHuynh.MaPhuHuynh;
                        HienThiThongTinPhuHuynh(dialog.SelectedPhuHuynh);
                    }
                }
            }
        }

        /// <summary>
        /// Hiển thị thông tin phụ huynh đã chọn
        /// </summary>
        private void HienThiThongTinPhuHuynh(PhuHuynhDTO phuHuynh)
        {
            if (phuHuynh != null)
            {
                lblTenPhuHuynh.Text = $"Họ và tên: {phuHuynh.HoTen}";
                lblSDTPhuHuynh.Text = $"Số điện thoại: {phuHuynh.SoDienThoai}";
                lblEmailPhuHuynh.Text = $"Email: {(string.IsNullOrEmpty(phuHuynh.Email) ? "Chưa có" : phuHuynh.Email)}";
                panelThongTinPhuHuynh.Visible = true;
            }
            else
            {
                panelThongTinPhuHuynh.Visible = false;
            }
        }

        private void groupBoxGioiTinh_Enter(object sender, EventArgs e)
        {

        }
    }
}
