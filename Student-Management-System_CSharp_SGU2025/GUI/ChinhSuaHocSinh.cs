using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq; // Cần thêm using này
using System.Windows.Forms;

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

            // Chạy tuần tự
            LoadComboBoxData();       // 1. Nạp các lựa chọn Lớp, Học Kỳ
            SetupTablePhuHuynh();     // 2. Cấu hình bảng Phụ huynh
            LoadMasterPhuHuynhList(); // 3. Nạp danh sách tổng Phụ huynh
            RefreshPhuHuynhTable(""); // 4. Hiển thị tất cả phụ huynh
            LoadHocSinhData();        // 5. Nạp thông tin CỤ THỂ của học sinh
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
            cbMoiQuanHe.Items.Add("Bố");
            cbMoiQuanHe.Items.Add("Mẹ");
            cbMoiQuanHe.Items.Add("Người giám hộ");
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
            // --- 1. Kiểm tra (Validation) ---
            if (this.selectedMaPhuHuynh <= 0)
            {
                MessageBox.Show("Vui lòng chọn phụ huynh từ danh sách.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimKiem.Focus();
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

                // Bước 2: Cập nhật Mối Quan Hệ
                try
                {
                    // TH1: Đổi phụ huynh
                    if (this.originalMaPhuHuynh != -1 && this.originalMaPhuHuynh != newSelectedMaPhuHuynh)
                    {
                        hocSinhPhuHuynhBLL.DeleteQuanHe(maHocSinhToEdit, this.originalMaPhuHuynh);
                        hocSinhPhuHuynhBLL.AddQuanHe(maHocSinhToEdit, newSelectedMaPhuHuynh, newMoiQuanHe);
                    }
                    // TH2: Giữ nguyên phụ huynh, chỉ đổi mối quan hệ (Bố -> Mẹ)
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            RefreshPhuHuynhTable(txtTimKiem.Text);
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            ChonPhuHuynhTuBang();
        }

        private void TablePhuHuynh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ChonPhuHuynhTuBang();
            }
        }

        /// <summary>
        /// Hàm phụ trợ để lấy thông tin phụ huynh đang chọn trên bảng
        /// </summary>
        private void ChonPhuHuynhTuBang()
        {
            if (tablePhuHuynh.CurrentRow != null)
            {
                try
                {
                    int maPH = Convert.ToInt32(tablePhuHuynh.CurrentRow.Cells["MaPH"].Value);
                    string tenPH = tablePhuHuynh.CurrentRow.Cells["HoTenPH"].Value.ToString();

                    // Lưu lại ID MỚI và hiển thị tên
                    this.selectedMaPhuHuynh = maPH;
                    txtPhuHuynhDuocChon.Text = tenPH;
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
            else
            {
                MessageBox.Show("Không có phụ huynh nào được chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Hàm Style (Sao chép từ form Thêm)

        private void SetupTablePhuHuynh()
        {
            tablePhuHuynh.Columns.Clear();
            ApplyBaseTableStyle(tablePhuHuynh);

            tablePhuHuynh.Columns.Add("MaPH", "Mã PH");
            tablePhuHuynh.Columns.Add("HoTenPH", "Họ và Tên");
            tablePhuHuynh.Columns.Add("Sdt", "SĐT");
            tablePhuHuynh.Columns.Add("Email", "Email");
            tablePhuHuynh.Columns.Add("DiaChi", "Địa chỉ");

            ApplyColumnAlignmentAndWrapping(tablePhuHuynh);
            tablePhuHuynh.Columns["HoTenPH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["DiaChi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            tablePhuHuynh.Columns["MaPH"].FillWeight = 10; tablePhuHuynh.Columns["MaPH"].MinimumWidth = 50;
            tablePhuHuynh.Columns["HoTenPH"].FillWeight = 20; tablePhuHuynh.Columns["HoTenPH"].MinimumWidth = 110;
            tablePhuHuynh.Columns["Sdt"].FillWeight = 12; tablePhuHuynh.Columns["Sdt"].MinimumWidth = 100;
            tablePhuHuynh.Columns["Email"].FillWeight = 20; tablePhuHuynh.Columns["Email"].MinimumWidth = 170;
            tablePhuHuynh.Columns["DiaChi"].FillWeight = 25; tablePhuHuynh.Columns["DiaChi"].MinimumWidth = 200;

            tablePhuHuynh.CellDoubleClick += TablePhuHuynh_CellDoubleClick;
        }

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

        private void RefreshPhuHuynhTable(string keyword)
        {
            tablePhuHuynh.Rows.Clear();
            string keywordLower = keyword.ToLower().Trim();

            var filteredList = danhSachPhuHuynh.Where(ph =>
                ph.HoTen.ToLower().Contains(keywordLower) ||
                ph.SoDienThoai.Contains(keyword)
            ).ToList();

            foreach (PhuHuynhDTO ph in filteredList)
            {
                tablePhuHuynh.Rows.Add(ph.MaPhuHuynh, ph.HoTen, ph.SoDienThoai, ph.Email, ph.DiaChi);
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
}