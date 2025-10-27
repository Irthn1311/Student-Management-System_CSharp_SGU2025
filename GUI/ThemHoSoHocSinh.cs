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

            SetupTablePhuHuynh();
            LoadSampleDataPhuHuynh();
            LoadMasterPhuHuynhList(); // Tải danh sách phụ huynh gốc
            RefreshPhuHuynhTable("");
        }

        private void SetupTablePhuHuynh()
        {
            // --- Xóa cột cũ và cấu hình chung ---
            tablePhuHuynh.Columns.Clear();
            ApplyBaseTableStyle(tablePhuHuynh); // Áp dụng style chung

            // --- Thêm cột mới ---
            tablePhuHuynh.Columns.Add("MaPH", "Mã PH");
            tablePhuHuynh.Columns.Add("HoTenPH", "Họ và Tên");
            tablePhuHuynh.Columns.Add("Sdt", "SĐT");
            tablePhuHuynh.Columns.Add("Email", "Email");
            tablePhuHuynh.Columns.Add("DiaChi", "Địa chỉ");

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tablePhuHuynh);
            tablePhuHuynh.Columns["HoTenPH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["DiaChi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- Tùy chỉnh kích thước ---
            tablePhuHuynh.Columns["MaPH"].FillWeight = 10; tablePhuHuynh.Columns["MaPH"].MinimumWidth = 50;
            tablePhuHuynh.Columns["HoTenPH"].FillWeight = 20; tablePhuHuynh.Columns["HoTenPH"].MinimumWidth = 110;
            tablePhuHuynh.Columns["Sdt"].FillWeight = 12; tablePhuHuynh.Columns["Sdt"].MinimumWidth = 100;
            tablePhuHuynh.Columns["Email"].FillWeight = 20; tablePhuHuynh.Columns["Email"].MinimumWidth = 170;
            tablePhuHuynh.Columns["DiaChi"].FillWeight = 25; tablePhuHuynh.Columns["DiaChi"].MinimumWidth = 200;

            tablePhuHuynh.CellDoubleClick += TablePhuHuynh_CellDoubleClick;
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

        /// <summary>
        /// Lọc và hiển thị danh sách phụ huynh lên bảng dựa trên từ khóa
        /// </summary>
        private void RefreshPhuHuynhTable(string keyword)
        {
            tablePhuHuynh.Rows.Clear();
            string keywordLower = keyword.ToLower().Trim();

            // Lọc từ danh sách gốc
            var filteredList = danhSachPhuHuynh.Where(ph =>
                ph.HoTen.ToLower().Contains(keywordLower) ||
                ph.SoDienThoai.Contains(keyword) 
            ).ToList();

            // Đổ dữ liệu đã lọc vào bảng
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

        private void LoadSampleDataPhuHuynh()
        {
            tablePhuHuynh.Rows.Clear();

            danhSachPhuHuynh = phuHuynhBLL.GetAllPhuHuynh();

            foreach (PhuHuynhDTO ph in danhSachPhuHuynh)
            {
                tablePhuHuynh.Rows.Add(ph.MaPhuHuynh, ph.HoTen, ph.SoDienThoai, ph.Email, ph.DiaChi);
            }
        }

        

        private void ThemHoSoHocSinh_Load(object sender, EventArgs e)
        {
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); // Đóng form hiện tại
        }

        

        private void btnThemHocSinh_Click_1(object sender, EventArgs e)
        {
            if (this.selectedMaPhuHuynh <= 0) // Kiểm tra bằng ID đã lưu
            {
                MessageBox.Show("Vui lòng chọn phụ huynh từ danh sách.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimKiem.Focus();
                return;
            }
            if (cbMoiQuanHe.SelectedIndex <= 0) // Giả sử Index 0 là "Chọn mối quan hệ"
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

                // Lấy thông tin quan hệ và phân lớp
                string moiQuanHe = cbMoiQuanHe.SelectedItem.ToString();


                // --- 3. Gọi BLL để thêm (Thực hiện tuần tự) ---

                // Bước 1: Thêm Học Sinh
                int newMaHocSinh = hocSinhBLL.AddHocSinh(hs);

                if (newMaHocSinh > 0)
                {
                    bool allSuccess = true;
                    string warningMessage = "";

                    // Bước 2: Thêm Mối Quan Hệ
                    try
                    {
                        bool addQuanHeSuccess = hocSinhPhuHuynhBLL.AddQuanHe(newMaHocSinh, this.selectedMaPhuHuynh, moiQuanHe);
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

        private void btnChon_Click(object sender, EventArgs e)
        {
            ChonPhuHuynhTuBang();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            RefreshPhuHuynhTable(txtTimKiem.Text);
        }

        private void TablePhuHuynh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Đảm bảo double click vào một dòng hợp lệ (không phải header)
            if (e.RowIndex >= 0)
            {
                ChonPhuHuynhTuBang();
            }
        }

        private void ChonPhuHuynhTuBang()
        {
            if (tablePhuHuynh.CurrentRow != null)
            {
                try
                {
                    // Lấy dữ liệu từ các ô trong dòng đang chọn
                    int maPH = Convert.ToInt32(tablePhuHuynh.CurrentRow.Cells["MaPH"].Value);
                    string tenPH = tablePhuHuynh.CurrentRow.Cells["HoTenPH"].Value.ToString();

                    // Lưu lại ID và hiển thị tên
                    this.selectedMaPhuHuynh = maPH;
                    txtPhuHuynhDuocChon.Text = tenPH;
                    txtPhuHuynhDuocChon.ForeColor = Color.Green; // (Tùy chọn) Đổi màu để báo đã chọn
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
    }
}