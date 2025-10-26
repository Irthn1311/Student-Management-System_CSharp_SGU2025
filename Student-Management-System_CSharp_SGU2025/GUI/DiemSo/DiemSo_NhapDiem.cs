using Student_Management_System_CSharp_SGU2025.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System.Collections.Generic;
using Student_Management_System_CSharp_SGU2025.GUI.DiemSo;
using ClosedXML.Excel;
using System.IO;

namespace Student_Management_System_CSharp_SGU2025.GUI
{

    public partial class DiemSo_NhapDiem : UserControl
    {

        private NhapDiemBUS nhapDiemBUS;
        // Khai báo màu sắc
        private Color selectedColor = Color.FromArgb(33, 150, 243); // Màu xanh dương
        private Color normalColor = Color.White;
        private Color selectedTextColor = Color.White;
        private Color normalTextColor = Color.Black;
        private Color borderColor = Color.FromArgb(224, 224, 224);
        private int? selectedMaLop = null;
        private int? selectedMaMonHoc = null;
        private int? selectedMaHocKy = null;
        private int selectedRowIndex = -1;
        // Thêm biến để tránh trigger event khi load
        private bool isLoadingData = false;
        private int? selectedMaHocKyBD = null;
        private int selectedRowIndexBangDiem = -1;
        private int? selectedMaLopBD = null;
        private string searchKeyword = "";

        // Check khóa điểm 
        //private bool isLocked = true;
        public DiemSo_NhapDiem()
        {
            InitializeComponent();
            // Không cho phép chỉnh sửa cột Trung Bình
            tableNhapDiem.Columns[5].ReadOnly = true;

            nhapDiemBUS = new NhapDiemBUS();

            // Không cho phép chỉnh sửa cột Trung Bình
            tableNhapDiem.Columns[5].ReadOnly = true;
        }

        // Hàm highlight button và reset cái cũ
        private void HighlightButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            // Reset tất cả buttons về trạng thái bình thường
            btnNhapDiem.FillColor = normalColor;
            btnNhapDiem.ForeColor = normalTextColor;
            btnNhapDiem.BorderColor = borderColor;
            btnNhapDiem.BorderThickness = 1;

            btnXemBangDiem.FillColor = normalColor;
            btnXemBangDiem.ForeColor = normalTextColor;
            btnXemBangDiem.BorderColor = borderColor;
            btnXemBangDiem.BorderThickness = 1;

            // Đổi màu button được chọn
            activeButton.FillColor = selectedColor;
            activeButton.ForeColor = selectedTextColor;
            activeButton.BorderColor = selectedColor;
        }

        // Hàm hiển thị bảng Nhập điểm
        private void ShowNhapDiem()
        {
            // Ẩn bảng Xem điểm
            tableXemBangDiem.Visible = false;

            // Hiển thị bảng Nhập điểm
            tableNhapDiem.Visible = true;

            // Ẩn nút Xuất Excel
            btnXuatExcel.Visible = false;

            // Hiện nút Khóa điểm và Lưu điểm

            btnThemDiem.Visible = true;
            btnSuaDiem.Visible = true;
            cbHocKyNamHoc.Visible = true;
            cbMonHoc.Visible = true;
            cbLop.Visible = true;
            cbLopBD.Visible = false;
            btnXemChiTiet.Visible = false;
            cbHocKyBD.Visible = false;
        }

        // Hàm hiển thị bảng Xem điểm
        private void ShowXemBangDiem()
        {
            // Ẩn bảng Nhập điểm
            tableNhapDiem.Visible = false;

            // Hiển thị bảng Xem điểm
            tableXemBangDiem.Visible = true;

            // Hiện nút Xuất Excel
            btnXuatExcel.Visible = true;

            // Ẩn nút Khóa điểm và Lưu điểm
            btnThemDiem.Visible = false;
            btnSuaDiem.Visible = false;
            //btnLuuDiem.Visible = false;
            cbHocKyNamHoc.Visible = false;
            cbMonHoc.Visible = false;
            cbLop.Visible = false;
            cbLopBD.Visible = true;
            btnXemChiTiet.Visible = true;
            cbHocKyBD.Visible = true;
        }

        // Hàm khởi tạo buttons
        private void InitializeButtons()
        {
            // Thiết lập style cho button Nhập điểm
            btnNhapDiem.BorderThickness = 1;
            btnNhapDiem.BorderColor = borderColor;
            btnNhapDiem.FillColor = normalColor;
            btnNhapDiem.ForeColor = normalTextColor;
            btnNhapDiem.Cursor = Cursors.Hand;

            // Thiết lập style cho button Xem bảng điểm
            btnXemBangDiem.BorderThickness = 1;
            btnXemBangDiem.BorderColor = borderColor;
            btnXemBangDiem.FillColor = normalColor;
            btnXemBangDiem.ForeColor = normalTextColor;
            btnXemBangDiem.Cursor = Cursors.Hand;

            // Mặc định hiển thị Nhập điểm
            HighlightButton(btnNhapDiem);
            ShowNhapDiem();
        }

        private void DiemSo_NhapDiem_Load(object sender, EventArgs e)
        {
            isLoadingData = true;

            // Khởi tạo buttons
            InitializeButtons();

            // Trang trí tableNhapDiem
            ConfigureTableNhapDiem();

            // Trang trí tableXemBangDiem
            ConfigureTableXemBangDiem();

            statCardDiemTrungBinh.lbCardTitle.Text = "Điểm trung bình";
            statCardDiemCaoNhat.lbCardTitle.Text = "Điểm cao nhất";
            statCardDiemThapNhat.lbCardTitle.Text = "Điểm thấp nhất";
            statCardDaNhap.lbCardTitle.Text = "Đã Nhập";

            statCardDiemTrungBinh.lbCardNote.ForeColor = Color.FromArgb(22, 163, 74);
            statCardDiemCaoNhat.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardDiemThapNhat.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardDaNhap.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardDaNhap.lbCardNote.ForeColor = Color.FromArgb(220, 38, 38);

            // Load các ComboBox
            LoadComboBoxLop();
            LoadComboBoxMonHoc();
            LoadComboBoxHocKy();
            LoadComboBoxHocKyBD();
            LoadComboBoxLopBD();
            tableXemBangDiem.CellClick += tableXemBangDiem_CellClick;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyPress += txtSearch_KeyPress;
            //// Thêm dữ liệu mẫu vào tableXemBangDiem
            //LoadSampleDataXemBangDiem();

            //tableNhapDiem.ReadOnly = false;
            tableNhapDiem.CellClick += tableNhapDiem_CellClick;
            isLoadingData = false;
            LoadThongKe();
            ApplyFilter();
        }

        /// <summary>
        /// Load thống kê lên các stat cards
        /// </summary>
        private void LoadThongKe()
        {
            if (!selectedMaHocKy.HasValue)
            {
                // Reset về giá trị mặc định nếu chưa chọn học kỳ
                statCardDiemTrungBinh.lbCardValue.Text = "0.0";
                statCardDiemTrungBinh.lbCardNote.Text = "Chưa có dữ liệu";
                statCardDiemCaoNhat.lbCardValue.Text = "0.0";
                statCardDiemCaoNhat.lbCardNote.Text = "Chưa có";
                statCardDiemThapNhat.lbCardValue.Text = "0.0";
                statCardDiemThapNhat.lbCardNote.Text = "Chưa có";
                statCardDaNhap.lbCardValue.Text = "0 / 0";
                statCardDaNhap.lbCardNote.Text = "Chưa có dữ liệu";
                return;
            }

            try
            {
                ThongKeDTO thongKe = nhapDiemBUS.GetThongKeDiemTheoHocKy(selectedMaHocKy.Value);

                // Card Điểm Trung Bình
                statCardDiemTrungBinh.lbCardValue.Text = thongKe.DiemTBChung.ToString("0.0");

                if (thongKe.DiemTBChungKyTruoc > 0)
                {
                    float chenhLech = thongKe.DiemTBChung - thongKe.DiemTBChungKyTruoc;
                    string dau = chenhLech >= 0 ? "+" : "";
                    statCardDiemTrungBinh.lbCardNote.Text = $"{dau}{chenhLech:0.0} so với kỳ trước";

                    // Đổi màu dựa trên tăng/giảm
                    if (chenhLech > 0)
                        statCardDiemTrungBinh.lbCardNote.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                    else if (chenhLech < 0)
                        statCardDiemTrungBinh.lbCardNote.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                    else
                        statCardDiemTrungBinh.lbCardNote.ForeColor = Color.FromArgb(107, 114, 128); // Xám
                }
                else
                {
                    statCardDiemTrungBinh.lbCardNote.Text = "Chưa có dữ liệu kỳ trước";
                    statCardDiemTrungBinh.lbCardNote.ForeColor = Color.FromArgb(107, 114, 128);
                }

                // Card Điểm Cao Nhất
                statCardDiemCaoNhat.lbCardValue.Text = thongKe.DiemCaoNhat > 0 ?
                    thongKe.DiemCaoNhat.ToString("0.0") : "0.0";
                statCardDiemCaoNhat.lbCardNote.Text = thongKe.HocSinhDiemCaoNhat;
                statCardDiemCaoNhat.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);

                // Card Điểm Thấp Nhất
                statCardDiemThapNhat.lbCardValue.Text = thongKe.DiemThapNhat > 0 ?
                    thongKe.DiemThapNhat.ToString("0.0") : "0.0";
                statCardDiemThapNhat.lbCardNote.Text = thongKe.HocSinhDiemThapNhat;
                statCardDiemThapNhat.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);

                // Card Đã Nhập
                statCardDaNhap.lbCardValue.Text = $"{thongKe.HocSinhDaNhapDiem} / {thongKe.TongHocSinh}";
                statCardDaNhap.lbCardNote.Text = thongKe.HocSinhChuaNhapDiem > 0 ?
                    $"{thongKe.HocSinhChuaNhapDiem} học sinh chưa có điểm" :
                    "Tất cả học sinh đã có điểm";

                statCardDaNhap.lbCardValue.ForeColor = thongKe.HocSinhDaNhapDiem == thongKe.TongHocSinh ?
                    Color.FromArgb(22, 163, 74) : Color.FromArgb(234, 179, 8); // Xanh lá hoặc vàng

                statCardDaNhap.lbCardNote.ForeColor = thongKe.HocSinhChuaNhapDiem > 0 ?
                    Color.FromArgb(220, 38, 38) : Color.FromArgb(22, 163, 74);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thống kê: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm cấu hình tableNhapDiem
        private void ConfigureTableNhapDiem()
        {
            // Cấu hình header
            tableNhapDiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            tableNhapDiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableNhapDiem.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tableNhapDiem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableNhapDiem.ColumnHeadersHeight = 45;
            tableNhapDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cấu hình cells
            tableNhapDiem.DefaultCellStyle.BackColor = Color.White;
            tableNhapDiem.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            tableNhapDiem.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tableNhapDiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableNhapDiem.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            tableNhapDiem.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Cấu hình rows
            tableNhapDiem.RowTemplate.Height = 50;
            tableNhapDiem.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Cấu hình borders
            tableNhapDiem.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableNhapDiem.GridColor = Color.FromArgb(229, 231, 235);
            tableNhapDiem.BorderStyle = BorderStyle.None;

            // Cấu hình columns width
            tableNhapDiem.Columns[0].Width = 80;  
            tableNhapDiem.Columns[1].Width = 250; // Họ và Tên
            tableNhapDiem.Columns[2].Width = 150; // Điểm TX
            tableNhapDiem.Columns[3].Width = 150; // Giữa Kì
            tableNhapDiem.Columns[4].Width = 150; // Cuối Kì
            tableNhapDiem.Columns[5].Width = 150; // Trung Bình

            // Căn giữa các cột điểm
            for (int i = 2; i <= 5; i++)
            {
                tableNhapDiem.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Loại bỏ selection
            tableNhapDiem.EnableHeadersVisualStyles = false;

            // Ngăn đổi màu tiêu đề khi chọn
            tableNhapDiem.EnableHeadersVisualStyles = false;
            tableNhapDiem.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableNhapDiem.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(75, 85, 99);

            tableNhapDiem.ReadOnly = true;


        }

        // Hàm cấu hình tableXemBangDiem
        private void ConfigureTableXemBangDiem()
        {
            // Cấu hình header
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableXemBangDiem.ColumnHeadersHeight = 45;
            tableXemBangDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cấu hình cells
            tableXemBangDiem.DefaultCellStyle.BackColor = Color.White;
            tableXemBangDiem.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            tableXemBangDiem.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tableXemBangDiem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableXemBangDiem.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            tableXemBangDiem.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Cấu hình rows
            tableXemBangDiem.RowTemplate.Height = 50;
            tableXemBangDiem.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Cấu hình borders
            tableXemBangDiem.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tableXemBangDiem.GridColor = Color.FromArgb(229, 231, 235);
            tableXemBangDiem.BorderStyle = BorderStyle.None;

            // Cấu hình columns width
            tableXemBangDiem.Columns[0].Width = 80;  // Mã HS
            tableXemBangDiem.Columns[1].Width = 200; // Học sinh
            tableXemBangDiem.Columns[2].Width = 120; // Toán
            tableXemBangDiem.Columns[3].Width = 120; // Văn
            tableXemBangDiem.Columns[4].Width = 120; // Anh
            tableXemBangDiem.Columns[5].Width = 120; // Lý
            tableXemBangDiem.Columns[6].Width = 120; // Hóa
            tableXemBangDiem.Columns[7].Width = 140; // TB Chung

            // Căn giữa các cột điểm (trừ cột học sinh)
            //for (int i = 1; i <= 6; i++)
            //{
            //    tableXemBangDiem.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}

            // Loại bỏ selection
            tableXemBangDiem.EnableHeadersVisualStyles = false;

            // Không cho chỉnh sửa
            tableXemBangDiem.ReadOnly = true;

            tableXemBangDiem.EnableHeadersVisualStyles = false;
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tableXemBangDiem.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(75, 85, 99);

        }

        private void LoadComboBoxHocKyBD()
        {
            try
            {
                List<HocKyDTO> danhSachHocKy = nhapDiemBUS.GetDanhSachHocKy();

                cbHocKyBD.DataSource = null;
                cbHocKyBD.Items.Clear();

                foreach (var hk in danhSachHocKy)
                {
                    string displayText = $"{hk.MaNamHoc} - {hk.TenHocKy}";
                    cbHocKyBD.Items.Add(new ComboBoxItem
                    {
                        Text = displayText,
                        Value = hk.MaHocKy
                    });
                }

                cbHocKyBD.DisplayMember = "Text";
                cbHocKyBD.ValueMember = "Value";

                // Chọn học kỳ cuối cùng (index 0 vì đã ORDER BY DESC)
                if (cbHocKyBD.Items.Count > 0)
                {
                    cbHocKyBD.SelectedIndex = 0;
                    var firstItem = cbHocKyBD.SelectedItem as ComboBoxItem;
                    selectedMaHocKyBD = (int)firstItem.Value;
                    LoadBangDiem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách học kỳ: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm áp dụng màu cho cột điểm trung bình
        private void ApplyColorToDiemTB()
        {
            foreach (DataGridViewRow row in tableNhapDiem.Rows)
            {
                if (row.Cells[5].Value != null && !string.IsNullOrEmpty(row.Cells[5].Value.ToString()))
                {
                    if (float.TryParse(row.Cells[5].Value.ToString(), out float score))
                    {
                        if (score >= 8.0)
                        {
                            row.Cells[5].Style.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                            row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        }
                        else if (score >= 6.5)
                        {
                            row.Cells[5].Style.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương
                            row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        }
                        else
                        {
                            row.Cells[5].Style.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                            row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        }
                    }
                }
            }
        }

        // Thêm event handler cho việc tự động tính điểm trung bình khi nhập điểm
        private void tableNhapDiem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Chỉ xử lý khi thay đổi các cột điểm (cột 2, 3, 4)
            if (e.RowIndex >= 0 && (e.ColumnIndex >= 2 && e.ColumnIndex <= 4))
            {
                DataGridViewRow row = tableNhapDiem.Rows[e.RowIndex];

                try
                {
                    // Lấy giá trị điểm
                    float? diemTX = null;
                    float? diemGK = null;
                    float? diemCK = null;

                    if (row.Cells[2].Value != null && !string.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        diemTX = float.Parse(row.Cells[2].Value.ToString());

                    if (row.Cells[3].Value != null && !string.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                        diemGK = float.Parse(row.Cells[3].Value.ToString());

                    if (row.Cells[4].Value != null && !string.IsNullOrEmpty(row.Cells[4].Value.ToString()))
                        diemCK = float.Parse(row.Cells[4].Value.ToString());

                    // Kiểm tra tính hợp lệ
                    if (!nhapDiemBUS.KiemTraTatCaDiemHopLe(diemTX, diemGK, diemCK))
                    {
                        MessageBox.Show("Điểm phải nằm trong khoảng 0-10!", "Cảnh báo",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        row.Cells[e.ColumnIndex].Value = "";
                        return;
                    }

                    // Tính điểm trung bình
                    float? diemTB = nhapDiemBUS.TinhDiemTrungBinh(diemTX, diemGK, diemCK);

                    if (diemTB.HasValue)
                    {
                        row.Cells[5].Value = diemTB.Value.ToString("0.0");
                    }
                    else
                    {
                        row.Cells[5].Value = "";
                    }

                    // Áp dụng màu
                    ApplyColorToDiemTB();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tính điểm: " + ex.Message, "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadComboBoxLop()
        {
            try
            {
                List<LopHocDTO> danhSachLop = nhapDiemBUS.GetDanhSachLop();

                cbLop.DataSource = null;
                cbLop.Items.Clear();

                // Thêm "Tất cả lớp" ở đầu
                cbLop.Items.Add(new ComboBoxItem
                {
                    Text = "Tất cả lớp",
                    Value = -1 // Giá trị đặc biệt để nhận biết "Tất cả"
                });

                foreach (var lop in danhSachLop)
                {
                    cbLop.Items.Add(new ComboBoxItem
                    {
                        Text = lop.TenLop,
                        Value = lop.MaLop
                    });
                }

                cbLop.DisplayMember = "Text";
                cbLop.ValueMember = "Value";

                // Chọn "Tất cả lớp" làm mặc định
                if (cbLop.Items.Count > 0)
                {
                    cbLop.SelectedIndex = 0;
                    selectedMaLop = -1; // Tất cả
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách lớp: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
        private void LoadComboBoxMonHoc()
        {
            try
            {
                List<MonHocDTO> danhSachMonHoc = nhapDiemBUS.GetDanhSachMonHoc();

                cbMonHoc.DataSource = null;
                cbMonHoc.Items.Clear();

                foreach (var mon in danhSachMonHoc)
                {
                    cbMonHoc.Items.Add(new ComboBoxItem
                    {
                        Text = mon.tenMon,
                        Value = int.Parse(mon.maMon)
                    });
                }

                cbMonHoc.DisplayMember = "Text";
                cbMonHoc.ValueMember = "Value";

                // Chọn item đầu tiên nếu có dữ liệu
                if (cbMonHoc.Items.Count > 0)
                {
                    cbMonHoc.SelectedIndex = 0;
                    var firstItem = cbMonHoc.SelectedItem as ComboBoxItem;
                    selectedMaMonHoc = (int)firstItem.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách môn học: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxHocKy()
        {
            try
            {
                List<HocKyDTO> danhSachHocKy = nhapDiemBUS.GetDanhSachHocKy();

                cbHocKyNamHoc.DataSource = null;
                cbHocKyNamHoc.Items.Clear();

                foreach (var hk in danhSachHocKy)
                {
                    string displayText = $"{hk.TenHocKy} - {hk.MaNamHoc}";
                    cbHocKyNamHoc.Items.Add(new ComboBoxItem
                    {
                        Text = displayText,
                        Value = hk.MaHocKy
                    });
                }

                cbHocKyNamHoc.DisplayMember = "Text";
                cbHocKyNamHoc.ValueMember = "Value";

                // Chọn item đầu tiên nếu có dữ liệu
                if (cbHocKyNamHoc.Items.Count > 0)
                {
                    cbHocKyNamHoc.SelectedIndex = 0;
                    var firstItem = cbHocKyNamHoc.SelectedItem as ComboBoxItem;
                    selectedMaHocKy = (int)firstItem.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách học kỳ: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi chọn Lớp
        /// </summary>
        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            if (isLoadingData) return;

            if (cbLop.SelectedItem is ComboBoxItem item)
            {
                int value = (int)item.Value;
                selectedMaLop = value == -1 ? null : (int?)value; // null = tất cả
                ApplyFilter();
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi chọn Môn Học
        /// </summary>
        private void cbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            if (isLoadingData) return;

            if (cbMonHoc.SelectedItem is ComboBoxItem item)
            {
                selectedMaMonHoc = (int)item.Value;
                ApplyFilter();
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi chọn Học Kỳ
        /// </summary>
        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            if (isLoadingData) return;

            if (cbHocKyNamHoc.SelectedItem is ComboBoxItem item)
            {
                selectedMaHocKy = (int)item.Value;
                ApplyFilter();
                LoadThongKe();
            }
        }

      
        private void ApplyFilter()
        {
            FilterTableNhapDiem();


        }

        // Class hỗ trợ cho ComboBox
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        /// <summary>
        /// Load danh sách lớp có điểm vào cbLopBD
        /// </summary>
        private void LoadComboBoxLopBD()
        {
            try
            {
                if (!selectedMaHocKyBD.HasValue)
                {
                    cbLopBD.DataSource = null;
                    cbLopBD.Items.Clear();
                    return;
                }

                List<LopHocDTO> danhSachLop = nhapDiemBUS.GetDanhSachLopCoDiem(selectedMaHocKyBD.Value);

                cbLopBD.DataSource = null;
                cbLopBD.Items.Clear();

                // Thêm "Tất cả lớp" ở đầu
                cbLopBD.Items.Add(new ComboBoxItem
                {
                    Text = "Tất cả lớp",
                    Value = -1
                });

                foreach (var lop in danhSachLop)
                {
                    cbLopBD.Items.Add(new ComboBoxItem
                    {
                        Text = lop.TenLop,
                        Value = lop.MaLop
                    });
                }

                cbLopBD.DisplayMember = "Text";
                cbLopBD.ValueMember = "Value";

                // Chọn "Tất cả lớp" làm mặc định
                if (cbLopBD.Items.Count > 0)
                {
                    cbLopBD.SelectedIndex = 0;
                    selectedMaLopBD = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load danh sách lớp: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void headerQuanLiNhapDiem_Load(object sender, EventArgs e)
        {

        }

        private void tableNhapDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
            }
        }

        private void tableNhapDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void btnLuuDiem_Click(object sender, EventArgs e)
        {

        }

        private void btnNhapDiem_Click(object sender, EventArgs e)
        {
            // Highlight button Nhập điểm
            HighlightButton(btnNhapDiem);
            // Hiển thị bảng Nhập điểm
            ShowNhapDiem();
        }

        private void btnXemBangDiem_Click(object sender, EventArgs e)
        {
            // Highlight button Xem bảng điểm
            HighlightButton(btnXemBangDiem);
            // Hiển thị bảng Xem điểm
            ShowXemBangDiem();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (tableXemBangDiem.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!selectedMaHocKyBD.HasValue)
            {
                MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Xuất bảng điểm ra Excel";

                // Lấy tên học kỳ để đặt tên file
                string tenHocKy = ((ComboBoxItem)cbHocKyBD.SelectedItem).Text;
                saveDialog.FileName = $"BangDiem_{tenHocKy.Replace(" ", "_").Replace("-", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Tạo workbook và worksheet
                    var workbook = new ClosedXML.Excel.XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Bảng Điểm");

                    // === TIÊU ĐỀ ===
                    worksheet.Cell(1, 1).Value = "BẢNG ĐIỂM HỌC SINH";
                    worksheet.Range(1, 1, 1, 8).Merge();
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                    worksheet.Cell(1, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                    // === THÔNG TIN HỌC KỲ ===
                    worksheet.Cell(2, 1).Value = $"Học kỳ: {tenHocKy}";
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

                    // === HEADER ===
                    int headerRow = 5;
                    for (int i = 0; i < tableXemBangDiem.Columns.Count; i++)
                    {
                        worksheet.Cell(headerRow, i + 1).Value = tableXemBangDiem.Columns[i].HeaderText;
                        worksheet.Cell(headerRow, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(headerRow, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                        worksheet.Cell(headerRow, i + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(headerRow, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                    }

                    // === DỮ LIỆU ===
                    int currentRow = headerRow + 1;
                    foreach (DataGridViewRow row in tableXemBangDiem.Rows)
                    {
                        if (row.IsNewRow) continue;

                        for (int i = 0; i < tableXemBangDiem.Columns.Count; i++)
                        {
                            var cellValue = row.Cells[i].Value?.ToString() ?? "";
                            worksheet.Cell(currentRow, i + 1).Value = cellValue;

                            // Căn giữa các cột điểm (từ cột 3 trở đi)
                            if (i >= 2)
                            {
                                worksheet.Cell(currentRow, i + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                            }

                            // Tô màu cho điểm trung bình (cột cuối)
                            if (i == tableXemBangDiem.Columns.Count - 1 && !string.IsNullOrEmpty(cellValue))
                            {
                                if (float.TryParse(cellValue, out float score))
                                {
                                    if (score >= 8.0)
                                    {
                                        worksheet.Cell(currentRow, i + 1).Style.Font.FontColor = ClosedXML.Excel.XLColor.Green;
                                        worksheet.Cell(currentRow, i + 1).Style.Font.Bold = true;
                                    }
                                    else if (score >= 6.5)
                                    {
                                        worksheet.Cell(currentRow, i + 1).Style.Font.FontColor = ClosedXML.Excel.XLColor.Blue;
                                        worksheet.Cell(currentRow, i + 1).Style.Font.Bold = true;
                                    }
                                    else
                                    {
                                        worksheet.Cell(currentRow, i + 1).Style.Font.FontColor = ClosedXML.Excel.XLColor.Red;
                                        worksheet.Cell(currentRow, i + 1).Style.Font.Bold = true;
                                    }
                                }
                            }

                            worksheet.Cell(currentRow, i + 1).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                        }
                        currentRow++;
                    }

                    // === THỐNG KÊ ===
                    int statsRow = currentRow + 2;
                    worksheet.Cell(statsRow, 1).Value = "THỐNG KÊ";
                    worksheet.Cell(statsRow, 1).Style.Font.Bold = true;

                    statsRow++;
                    worksheet.Cell(statsRow, 1).Value = $"Tổng số học sinh: {tableXemBangDiem.Rows.Count}";

                    // Tính số học sinh đã có điểm TB
                    int soHSDaDiem = 0;
                    foreach (DataGridViewRow row in tableXemBangDiem.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Cells[7].Value != null && !string.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                        {
                            soHSDaDiem++;
                        }
                    }

                    statsRow++;
                    worksheet.Cell(statsRow, 1).Value = $"Học sinh đã có điểm: {soHSDaDiem}";
                    worksheet.Cell(statsRow, 1).Style.Font.FontColor = ClosedXML.Excel.XLColor.Green;

                    statsRow++;
                    worksheet.Cell(statsRow, 1).Value = $"Học sinh chưa có điểm: {tableXemBangDiem.Rows.Count - soHSDaDiem}";
                    if (tableXemBangDiem.Rows.Count - soHSDaDiem > 0)
                    {
                        worksheet.Cell(statsRow, 1).Style.Font.FontColor = ClosedXML.Excel.XLColor.Red;
                    }

                    // === TỰ ĐỘNG ĐIỀU CHỈNH ĐỘ RỘNG CỘT ===
                    worksheet.Columns().AdjustToContents();

                    // Đảm bảo cột tên không quá hẹp
                    if (worksheet.Column(2).Width < 30)
                        worksheet.Column(2).Width = 30;

                    // === LƯU FILE ===
                    workbook.SaveAs(saveDialog.FileName);

                    Cursor = Cursors.Default;

                    MessageBox.Show($"Xuất Excel thành công!\nĐường dẫn: {saveDialog.FileName}", "Thành công",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn mở file không
                    if (MessageBox.Show("Bạn có muốn mở file Excel vừa xuất?", "Xác nhận",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     

        private void tableXemBangDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Thêm event handler mới
        private void tableXemBangDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndexBangDiem = e.RowIndex;
            }
        }



        private void btnThemDiem_Click(object sender, EventArgs e)
        {
            //ThemDiem frm = new ThemDiem();
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.ShowDialog();

            ThemDiem frm = new ThemDiem();
            frm.StartPosition = FormStartPosition.CenterScreen;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Cập nhật chỉ dòng của học sinh vừa thêm - RẤT NHANH
                if (!string.IsNullOrEmpty(frm.MaHocSinhVuaThem))
                {
                    RefreshSingleStudent(frm.MaHocSinhVuaThem);
                }
            }   

        }

        private void btnSuaDiem_Click(object sender, EventArgs e)
        {
           

            if (selectedRowIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn học sinh cần sửa điểm!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!selectedMaMonHoc.HasValue || !selectedMaHocKy.HasValue)
            {
                MessageBox.Show("Vui lòng chọn môn học và học kỳ!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy thông tin từ hàng được chọn
                DataGridViewRow row = tableNhapDiem.Rows[selectedRowIndex];
                string maHocSinh = row.Cells[0].Value?.ToString();
                string hoTen = row.Cells[1].Value?.ToString();

                // Lấy điểm hiện tại
                float? diemTX = null;
                float? diemGK = null;
                float? diemCK = null;

                if (row.Cells[2].Value != null && !string.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                    diemTX = float.Parse(row.Cells[2].Value.ToString());

                if (row.Cells[3].Value != null && !string.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                    diemGK = float.Parse(row.Cells[3].Value.ToString());

                if (row.Cells[4].Value != null && !string.IsNullOrEmpty(row.Cells[4].Value.ToString()))
                    diemCK = float.Parse(row.Cells[4].Value.ToString());

                // Lấy tên môn và học kỳ
                string tenMonHoc = ((ComboBoxItem)cbMonHoc.SelectedItem).Text;
                string tenHocKy = ((ComboBoxItem)cbHocKyNamHoc.SelectedItem).Text;

                // Lấy mã lớp từ database thông qua BUS
                int? maLop = nhapDiemBUS.GetMaLopByMaHocSinh(maHocSinh);
                string tenLop = "";

                if (maLop.HasValue && maLop.Value > 0)
                {
                    tenLop = nhapDiemBUS.GetTenLopByMaLop(maLop.Value);
                }

                // Mở form sửa điểm với đầy đủ thông tin
                ThemDiem frm = new ThemDiem(
                    maHocSinh,
                    hoTen,
                    maLop,
                    tenLop,
                    selectedMaMonHoc.Value,
                    tenMonHoc,
                    selectedMaHocKy.Value,
                    tenHocKy,
                    diemTX,
                    diemGK,
                    diemCK
                );
                frm.StartPosition = FormStartPosition.CenterScreen;

                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    // Refresh lại dữ liệu sau khi sửa
                //    ApplyFilter();
                //}

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Cập nhật chỉ dòng của học sinh vừa sửa - RẤT NHANH
                    if (!string.IsNullOrEmpty(frm.MaHocSinhVuaThem))
                    {
                        RefreshSingleStudent(frm.MaHocSinhVuaThem);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form sửa điểm: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cbHocKyBD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingData) return;

            if (cbHocKyBD.SelectedItem is ComboBoxItem item)
            {
                selectedMaHocKyBD = (int)item.Value;

                // Load lại danh sách lớp theo học kỳ mới
                LoadComboBoxLopBD();

                // Load bảng điểm
                LoadBangDiem();
            }
        }


        // Thay thế phương thức LoadSampleDataXemBangDiem bằng LoadBangDiem
        private void LoadBangDiem()
        {
            FilterTableXemBangDiem();
        }

        // Thêm phương thức áp dụng màu cho bảng điểm
        private void ApplyColorToDiemTBBangDiem()
        {
            foreach (DataGridViewRow row in tableXemBangDiem.Rows)
            {
                if (row.Cells[7].Value != null && !string.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                {
                    if (float.TryParse(row.Cells[7].Value.ToString(), out float score))
                    {
                        if (score >= 8.0)
                        {
                            row.Cells[7].Style.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                        }
                        else if (score >= 6.5)
                        {
                            row.Cells[7].Style.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương
                        }
                        else
                        {
                            row.Cells[7].Style.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                        }
                        row.Cells[7].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                }
            }
        }

        /// <summary>
        /// Cập nhật chỉ 1 dòng học sinh cụ thể - nhanh hơn nhiều
        /// </summary>
        private void RefreshSingleStudent(string maHocSinh)
        {
            if (!selectedMaMonHoc.HasValue || !selectedMaHocKy.HasValue)
            {
                return;
            }

            try
            {
                // Tắt sự kiện tạm thời
                tableNhapDiem.CellValueChanged -= tableNhapDiem_CellValueChanged;

                // Tìm dòng của học sinh trong bảng
                int rowIndex = -1;
                for (int i = 0; i < tableNhapDiem.Rows.Count; i++)
                {
                    if (tableNhapDiem.Rows[i].Cells[0].Value?.ToString() == maHocSinh)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex >= 0)
                {
                    // Lấy điểm mới từ database
                    List<NhapDiemDTO> list;

                    if (!selectedMaLop.HasValue || selectedMaLop == -1)
                    {
                        list = nhapDiemBUS.GetDanhSachNhapDiem(
                            selectedMaMonHoc.Value,
                            selectedMaHocKy.Value);
                    }
                    else
                    {
                        list = nhapDiemBUS.GetDanhSachNhapDiemTheoLop(
                            selectedMaLop.Value,
                            selectedMaMonHoc.Value,
                            selectedMaHocKy.Value);
                    }

                    // Tìm học sinh trong danh sách
                    NhapDiemDTO student = list.Find(s => s.MaHocSinh == maHocSinh);

                    if (student != null)
                    {
                        // Cập nhật chỉ các ô điểm
                        tableNhapDiem.Rows[rowIndex].Cells[2].Value =
                            student.DiemTX.HasValue ? student.DiemTX.Value.ToString("0.0") : "";
                        tableNhapDiem.Rows[rowIndex].Cells[3].Value =
                            student.DiemGK.HasValue ? student.DiemGK.Value.ToString("0.0") : "";
                        tableNhapDiem.Rows[rowIndex].Cells[4].Value =
                            student.DiemCK.HasValue ? student.DiemCK.Value.ToString("0.0") : "";
                        tableNhapDiem.Rows[rowIndex].Cells[5].Value =
                            student.DiemTB.HasValue ? student.DiemTB.Value.ToString("0.0") : "";

                        // Áp dụng màu cho dòng này
                        ApplyColorToDiemTBForRow(rowIndex);
                    }
                }

                // Bật lại sự kiện
                tableNhapDiem.CellValueChanged += tableNhapDiem_CellValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Áp dụng màu cho một dòng cụ thể
        /// </summary>
        private void ApplyColorToDiemTBForRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= tableNhapDiem.Rows.Count)
                return;

            DataGridViewRow row = tableNhapDiem.Rows[rowIndex];

            if (row.Cells[5].Value != null && !string.IsNullOrEmpty(row.Cells[5].Value.ToString()))
            {
                if (float.TryParse(row.Cells[5].Value.ToString(), out float score))
                {
                    if (score >= 8.0)
                    {
                        row.Cells[5].Style.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                        row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                    else if (score >= 6.5)
                    {
                        row.Cells[5].Style.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương
                        row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                    else
                    {
                        row.Cells[5].Style.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                        row.Cells[5].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                }
            }
        }



        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (selectedRowIndexBangDiem < 0)
            {
                MessageBox.Show("Vui lòng chọn học sinh cần xem chi tiết!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!selectedMaHocKyBD.HasValue)
            {
                MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Lấy mã học sinh từ dòng được chọn
                DataGridViewRow row = tableXemBangDiem.Rows[selectedRowIndexBangDiem];
                string maHocSinh = row.Cells[0].Value?.ToString();

                if (string.IsNullOrEmpty(maHocSinh))
                {
                    MessageBox.Show("Không tìm thấy mã học sinh!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mở form chi tiết điểm
                ChiTietDiem frm = new ChiTietDiem(maHocSinh, selectedMaHocKyBD.Value);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem chi tiết: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statCardDiemCaoNhat_Load(object sender, EventArgs e)
        {

        }

        private void statCardDiemTrungBinh_Load(object sender, EventArgs e)
        {

        }

        private void statCardDiemThapNhat_Load(object sender, EventArgs e)
        {

        }

        private void statCardDaNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị loading indicator
                Cursor = Cursors.WaitCursor;

                // LUÔN LUÔN reload thống kê (4 stat cards) bất kể đang ở tab nào
                LoadThongKe();

                // Reload dữ liệu tùy theo tab đang hiển thị
                if (btnNhapDiem.FillColor == selectedColor)
                {
                    // Đang ở tab Nhập Điểm - reload bảng nhập điểm
                    ApplyFilter();

                    MessageBox.Show("Đã cập nhật dữ liệu nhập điểm và thống kê thành công!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnXemBangDiem.FillColor == selectedColor)
                {
                    // Đang ở tab Xem Bảng Điểm - reload bảng điểm
                    LoadBangDiem();

                    MessageBox.Show("Đã cập nhật bảng điểm và thống kê thành công!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi reload dữ liệu: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Khôi phục cursor
                Cursor = Cursors.Default;
            }
        }

        private void cbLopBD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingData) return;

            if (cbLopBD.SelectedItem is ComboBoxItem item)
            {
                int value = (int)item.Value;
                selectedMaLopBD = value == -1 ? null : (int?)value;
                LoadBangDiem();
            }
        }

        /// <summary>
        /// Lọc dữ liệu tableNhapDiem theo từ khóa tìm kiếm
        /// </summary>
        private void FilterTableNhapDiem()
        {
            if (!selectedMaMonHoc.HasValue || !selectedMaHocKy.HasValue)
            {
                tableNhapDiem.Rows.Clear();
                return;
            }

            try
            {
                // Tắt sự kiện tạm thời
                tableNhapDiem.CellValueChanged -= tableNhapDiem_CellValueChanged;

                // Xóa dữ liệu cũ
                tableNhapDiem.Rows.Clear();

                List<NhapDiemDTO> list;

                // Lấy dữ liệu theo lớp
                if (!selectedMaLop.HasValue || selectedMaLop == -1)
                {
                    list = nhapDiemBUS.GetDanhSachNhapDiem(
                        selectedMaMonHoc.Value,
                        selectedMaHocKy.Value);
                }
                else
                {
                    list = nhapDiemBUS.GetDanhSachNhapDiemTheoLop(
                        selectedMaLop.Value,
                        selectedMaMonHoc.Value,
                        selectedMaHocKy.Value);
                }

                // Lọc theo từ khóa tìm kiếm
                if (!string.IsNullOrWhiteSpace(searchKeyword))
                {
                    string keyword = searchKeyword.Trim().ToLower();
                    list = list.FindAll(x =>
                        x.MaHocSinh.ToLower().Contains(keyword) ||
                        x.HoTen.ToLower().Contains(keyword)
                    );
                }

                // Tạm dừng vẽ
                tableNhapDiem.SuspendLayout();

                // Thêm dữ liệu vào bảng
                foreach (NhapDiemDTO item in list)
                {
                    string diemTX = item.DiemTX.HasValue ? item.DiemTX.Value.ToString("0.0") : "";
                    string diemGK = item.DiemGK.HasValue ? item.DiemGK.Value.ToString("0.0") : "";
                    string diemCK = item.DiemCK.HasValue ? item.DiemCK.Value.ToString("0.0") : "";
                    string diemTB = item.DiemTB.HasValue ? item.DiemTB.Value.ToString("0.0") : "";

                    tableNhapDiem.Rows.Add(
                        item.MaHocSinh,
                        item.HoTen,
                        diemTX,
                        diemGK,
                        diemCK,
                        diemTB
                    );
                }

                // Áp dụng màu
                ApplyColorToDiemTB();

                // Tiếp tục vẽ
                tableNhapDiem.ResumeLayout();

                // Bật lại sự kiện
                tableNhapDiem.CellValueChanged += tableNhapDiem_CellValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lọc dữ liệu tableXemBangDiem theo từ khóa tìm kiếm
        /// </summary>
        private void FilterTableXemBangDiem()
        {
            if (!selectedMaHocKyBD.HasValue)
            {
                tableXemBangDiem.Rows.Clear();
                return;
            }

            try
            {
                tableXemBangDiem.Rows.Clear();

                // Lấy dữ liệu với lọc theo lớp
                int? maLop = (selectedMaLopBD.HasValue && selectedMaLopBD.Value > 0)
                    ? selectedMaLopBD
                    : null;

                List<XemBangDiemDTO> list = nhapDiemBUS.GetBangDiemTheoHocKyVaLop(
                    selectedMaHocKyBD.Value,
                    maLop);

                // Lọc theo từ khóa tìm kiếm
                if (!string.IsNullOrWhiteSpace(searchKeyword))
                {
                    string keyword = searchKeyword.Trim().ToLower();
                    list = list.FindAll(x =>
                        x.MaHocSinh.ToLower().Contains(keyword) ||
                        x.HoTen.ToLower().Contains(keyword)
                    );
                }

                // Thêm dữ liệu vào bảng
                foreach (var item in list)
                {
                    string diemToan = item.DiemToan.HasValue ? item.DiemToan.Value.ToString("0.0") : "";
                    string diemVan = item.DiemVan.HasValue ? item.DiemVan.Value.ToString("0.0") : "";
                    string diemAnh = item.DiemAnh.HasValue ? item.DiemAnh.Value.ToString("0.0") : "";
                    string diemLy = item.DiemLy.HasValue ? item.DiemLy.Value.ToString("0.0") : "";
                    string diemHoa = item.DiemHoa.HasValue ? item.DiemHoa.Value.ToString("0.0") : "";
                    string diemTB = item.DiemTB.HasValue ? item.DiemTB.Value.ToString("0.0") : "";

                    tableXemBangDiem.Rows.Add(
                        item.MaHocSinh,
                        item.HoTen,
                        diemToan,
                        diemVan,
                        diemAnh,
                        diemLy,
                        diemHoa,
                        diemTB
                    );
                }

                // Áp dụng màu cho cột điểm TB
                ApplyColorToDiemTBBangDiem();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        /// <summary>
        /// Event khi nhấn Enter trong ô tìm kiếm (tùy chọn)
        /// </summary>
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Ngăn tiếng beep

                // Lưu từ khóa tìm kiếm
                searchKeyword = txtSearch.Text.Trim();

                // Tìm kiếm trên bảng đang hiển thị
                if (btnNhapDiem.FillColor == selectedColor)
                {
                    FilterTableNhapDiem();
                }
                else if (btnXemBangDiem.FillColor == selectedColor)
                {
                    FilterTableXemBangDiem();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Lưu từ khóa tìm kiếm
            searchKeyword = txtSearch.Text.Trim();

            // Tìm kiếm trên bảng đang hiển thị
            if (btnNhapDiem.FillColor == selectedColor)
            {
                // Đang ở tab Nhập Điểm
                FilterTableNhapDiem();
            }
            else if (btnXemBangDiem.FillColor == selectedColor)
            {
                // Đang ở tab Xem Bảng Điểm
                FilterTableXemBangDiem();
            }
        }
    }
}
