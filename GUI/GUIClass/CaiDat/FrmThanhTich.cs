using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.GUIClass.CaiDat
{
    public partial class FrmThanhTich : Form
    {
        private HocKyBUS hocKyBUS;
        private HocSinhBLL hocSinhBLL;
        private NhapDiemBUS nhapDiemBUS;
        private MonHocBUS monHocBUS;
        private PhanLopBLL phanLopBLL;
        private LopHocBUS lopHocBUS;
        private HanhKiemBUS hanhKiemBUS;
        private XepLoaiBUS xepLoaiBUS;
        private DiemSoDAO diemSoDAO;
        private int? selectedMaHocKy = null;
        private int? maHocSinh = null;

        public FrmThanhTich()
        {
            InitializeComponent();
            hocKyBUS = new HocKyBUS();
            hocSinhBLL = new HocSinhBLL();
            nhapDiemBUS = new NhapDiemBUS();
            monHocBUS = new MonHocBUS();
            phanLopBLL = new PhanLopBLL();
            lopHocBUS = new LopHocBUS();
            hanhKiemBUS = new HanhKiemBUS();
            xepLoaiBUS = new XepLoaiBUS();
            diemSoDAO = new DiemSoDAO();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void tbThanhTich_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmThanhTich_Load(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đăng nhập
                if (!SessionManager.IsLoggedIn())
                {
                    MessageBox.Show("Bạn chưa đăng nhập!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Lấy mã học sinh từ tên đăng nhập
                string tenDangNhap = SessionManager.TenDangNhap;
                if (string.IsNullOrEmpty(tenDangNhap) || !tenDangNhap.StartsWith("HS", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Chức năng này chỉ dành cho học sinh!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                string maHocSinhStr = tenDangNhap.Substring(2);
                if (!int.TryParse(maHocSinhStr, out int maHS))
                {
                    MessageBox.Show("Không thể xác định mã học sinh.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                maHocSinh = maHS;

                // Load thông tin học sinh
                LoadThongTinHocSinh();

                // Cấu hình bảng tbThanhTich
                ConfigureTableThanhTich();

                // Load danh sách học kỳ
                LoadComboBoxHocKy();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load thông tin học sinh lên các label
        /// </summary>
        private void LoadThongTinHocSinh()
        {
            try
            {
                if (!maHocSinh.HasValue)
                    return;

                var hocSinh = hocSinhBLL.GetHocSinhById(maHocSinh.Value);
                if (hocSinh == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin học sinh.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Hiển thị thông tin cơ bản
                lblHoTen.Text = hocSinh.HoTen ?? "Chưa có";
                lblNgaySinh.Text = hocSinh.NgaySinh != null && hocSinh.NgaySinh != DateTime.MinValue
                    ? hocSinh.NgaySinh.ToString("dd/MM/yyyy") 
                    : "Chưa có";
                lblGioiTinh.Text = !string.IsNullOrEmpty(hocSinh.GioiTinh) 
                    ? hocSinh.GioiTinh 
                    : "Chưa có";

                // Lớp sẽ được load khi chọn học kỳ
                lblLop.Text = "Chưa chọn học kỳ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thông tin học sinh: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cấu hình bảng tbThanhTich giống tableNhapDiem
        /// </summary>
        private void ConfigureTableThanhTich()
        {
            // Cấu hình header
            tbThanhTich.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            tbThanhTich.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            tbThanhTich.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            tbThanhTich.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tbThanhTich.ColumnHeadersHeight = 45;
            tbThanhTich.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Cấu hình cells
            tbThanhTich.DefaultCellStyle.BackColor = Color.White;
            tbThanhTich.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            tbThanhTich.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            tbThanhTich.DefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tbThanhTich.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 41, 55);
            tbThanhTich.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);

            // Cấu hình rows
            tbThanhTich.RowTemplate.Height = 50;
            tbThanhTich.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            // Cấu hình borders
            tbThanhTich.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tbThanhTich.GridColor = Color.FromArgb(229, 231, 235);
            tbThanhTich.BorderStyle = BorderStyle.None;

            // Cấu hình columns width
            tbThanhTich.Columns["monHoc"].Width = 250;      // Môn học (giống cột Họ và Tên)
            tbThanhTich.Columns["diemTX"].Width = 150;      // Thường xuyên
            tbThanhTich.Columns["diemGK"].Width = 150;      // Giữa kỳ
            tbThanhTich.Columns["diemCK"].Width = 150;      // Cuối kỳ
            tbThanhTich.Columns["diemTB"].Width = 150;      // Trung bình

            // Căn giữa các cột điểm
            tbThanhTich.Columns["diemTX"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tbThanhTich.Columns["diemGK"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tbThanhTich.Columns["diemCK"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tbThanhTich.Columns["diemTB"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Loại bỏ selection
            tbThanhTich.EnableHeadersVisualStyles = false;

            // Ngăn đổi màu tiêu đề khi chọn
            tbThanhTich.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(243, 244, 246);
            tbThanhTich.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(75, 85, 99);

            // Không cho chỉnh sửa
            tbThanhTich.ReadOnly = true;
        }

        /// <summary>
        /// Load danh sách học kỳ vào ComboBox theo định dạng "TenHocKy - MaNamHoc"
        /// </summary>
        private void LoadComboBoxHocKy()
        {
            try
            {
                List<HocKyDTO> danhSachHocKy = hocKyBUS.DocDSHocKy();

                cbHocKyNamHoc.DataSource = null;
                cbHocKyNamHoc.Items.Clear();

                // Tạm thời hủy đăng ký event để tránh trigger khi load
                cbHocKyNamHoc.SelectedIndexChanged -= cbHocKyNamHoc_SelectedIndexChanged;

                // Hiển thị TẤT CẢ học kỳ vào combobox
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

                // Lấy học kỳ mới nhất có dữ liệu điểm số
                HocKyDTO hocKyMoiNhat = hocKyBUS.LayHocKyMoiNhatCoDuLieu();

                if (hocKyMoiNhat != null && cbHocKyNamHoc.Items.Count > 0)
                {
                    // Kiểm tra lại học kỳ này có dữ liệu điểm số thực sự không
                    bool coDuLieu = hocKyBUS.KiemTraHocKyCoDiemSo(hocKyMoiNhat.MaHocKy);

                    if (coDuLieu)
                    {
                        // Tìm và chọn học kỳ mới nhất có dữ liệu
                        for (int i = 0; i < cbHocKyNamHoc.Items.Count; i++)
                        {
                            var item = cbHocKyNamHoc.Items[i] as ComboBoxItem;
                            if (item != null && (int)item.Value == hocKyMoiNhat.MaHocKy)
                            {
                                cbHocKyNamHoc.SelectedIndex = i;
                                selectedMaHocKy = hocKyMoiNhat.MaHocKy;
                                
                                // Đăng ký lại event và load dữ liệu
                                cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
                                LoadThanhTich(selectedMaHocKy.Value);
                                return;
                            }
                        }
                    }
                }

                // Nếu không có học kỳ nào có điểm số, chọn học kỳ I của năm học mới nhất
                HocKyDTO hocKyIDauTien = hocKyBUS.LayHocKyIDauTienCuaNamHocMoiNhat();

                if (hocKyIDauTien != null && cbHocKyNamHoc.Items.Count > 0)
                {
                    // Tìm và chọn học kỳ I của năm học mới nhất
                    for (int i = 0; i < cbHocKyNamHoc.Items.Count; i++)
                    {
                        var item = cbHocKyNamHoc.Items[i] as ComboBoxItem;
                        if (item != null && (int)item.Value == hocKyIDauTien.MaHocKy)
                        {
                            cbHocKyNamHoc.SelectedIndex = i;
                            selectedMaHocKy = hocKyIDauTien.MaHocKy;
                            
                            // Đăng ký lại event và load dữ liệu
                            cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
                            LoadThanhTich(selectedMaHocKy.Value);
                            return;
                        }
                    }
                }

                // Fallback: Chọn học kỳ đầu tiên trong danh sách nếu không tìm thấy học kỳ I
                if (cbHocKyNamHoc.Items.Count > 0)
                {
                    cbHocKyNamHoc.SelectedIndex = 0;
                    var firstItem = cbHocKyNamHoc.SelectedItem as ComboBoxItem;
                    selectedMaHocKy = (int)firstItem.Value;
                    
                    // Đăng ký lại event và load dữ liệu cho học kỳ đầu tiên
                    cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
                    LoadThanhTich(selectedMaHocKy.Value);
                }
                else
                {
                    // Đăng ký lại event nếu không có item nào
                    cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                // Đảm bảo đăng ký lại event nếu có lỗi
                cbHocKyNamHoc.SelectedIndexChanged += cbHocKyNamHoc_SelectedIndexChanged;
                MessageBox.Show("Lỗi khi load danh sách học kỳ: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbHocKyNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKyNamHoc.SelectedItem is ComboBoxItem item)
            {
                selectedMaHocKy = (int)item.Value;
                LoadThanhTich(selectedMaHocKy.Value);
            }
        }

        /// <summary>
        /// Load dữ liệu thành tích theo học kỳ
        /// </summary>
        private void LoadThanhTich(int maHocKy)
        {
            try
            {
                if (!maHocSinh.HasValue)
                    return;

                // Load lớp của học sinh trong học kỳ này
                LoadLopHocSinh(maHocKy);

                // Load điểm các môn học
                LoadDiemCacMon(maHocKy);

                // Load hạnh kiểm, học lực, xếp loại
                LoadHanhKiemHocLucXepLoai(maHocKy);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load thành tích: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load lớp của học sinh trong học kỳ
        /// </summary>
        private void LoadLopHocSinh(int maHocKy)
        {
            try
            {
                if (!maHocSinh.HasValue)
                    return;

                int maLop = phanLopBLL.GetLopByHocSinh(maHocSinh.Value, maHocKy);
                if (maLop > 0)
                {
                    var lop = lopHocBUS.LayLopTheoId(maLop);
                    if (lop != null)
                    {
                        lblLop.Text = lop.tenLop ?? "Chưa có";
                    }
                    else
                    {
                        lblLop.Text = "Chưa có";
                    }
                }
                else
                {
                    lblLop.Text = "Chưa có";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load lớp: {ex.Message}");
                lblLop.Text = "Chưa có";
            }
        }

        /// <summary>
        /// Load điểm các môn học của học sinh theo học kỳ
        /// CHỈ HIỂN THỊ ĐIỂM CỦA HỌC KỲ ĐƯỢC CHỌN
        /// </summary>
        private void LoadDiemCacMon(int maHocKy)
        {
            try
            {
                if (!maHocSinh.HasValue)
                    return;

                // Xóa dữ liệu cũ
                tbThanhTich.Rows.Clear();

                // Lấy danh sách tất cả môn học
                List<MonHocDTO> danhSachMonHoc = monHocBUS.DocDSMH();

                if (danhSachMonHoc == null || danhSachMonHoc.Count == 0)
                {
                    MessageBox.Show("Không có môn học nào trong hệ thống.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy điểm của học sinh cho từng môn - CHỈ LẤY ĐIỂM CỦA HỌC KỲ ĐƯỢC CHỌN
                foreach (var monHoc in danhSachMonHoc)
                {
                    // Lấy điểm của học sinh cho môn này và học kỳ này (CHỈ học kỳ được chọn)
                    DiemSoDTO diem = diemSoDAO.GetDiemSo(maHocSinh.Value.ToString(), monHoc.maMon, maHocKy);

                    // Chỉ hiển thị điểm nếu có điểm của học kỳ này
                    // Nếu diem == null hoặc MaHocKy khác với maHocKy thì không có điểm
                    string diemTX = "";
                    string diemGK = "";
                    string diemCK = "";
                    string diemTB = "";

                    if (diem != null && diem.MaHocKy == maHocKy)
                    {
                        // Đảm bảo điểm thuộc đúng học kỳ được chọn
                        diemTX = diem.DiemThuongXuyen.HasValue 
                            ? diem.DiemThuongXuyen.Value.ToString("0.0") 
                            : "";
                        diemGK = diem.DiemGiuaKy.HasValue 
                            ? diem.DiemGiuaKy.Value.ToString("0.0") 
                            : "";
                        diemCK = diem.DiemCuoiKy.HasValue 
                            ? diem.DiemCuoiKy.Value.ToString("0.0") 
                            : "";
                        diemTB = diem.DiemTrungBinh.HasValue 
                            ? diem.DiemTrungBinh.Value.ToString("0.0") 
                            : "";
                    }

                    // Thêm vào bảng (luôn hiển thị tất cả môn học, nhưng chỉ có điểm nếu có điểm của học kỳ này)
                    tbThanhTich.Rows.Add(
                        monHoc.tenMon,
                        diemTX,
                        diemGK,
                        diemCK,
                        diemTB
                    );
                }

                // Áp dụng màu cho cột điểm TB
                ApplyColorToDiemTB();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load điểm các môn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load hạnh kiểm, học lực, xếp loại của học sinh theo học kỳ
        /// </summary>
        private void LoadHanhKiemHocLucXepLoai(int maHocKy)
        {
            try
            {
                if (!maHocSinh.HasValue)
                    return;

                // Load hạnh kiểm
                var hanhKiem = hanhKiemBUS.LayHanhKiem(maHocSinh.Value, maHocKy);
                string hanhKiemValue = "Chưa có";
                if (hanhKiem != null && !string.IsNullOrEmpty(hanhKiem.XepLoai))
                {
                    hanhKiemValue = hanhKiem.XepLoai;
                }
                lblHanhKiem.Text = hanhKiemValue;
                ApplyStyleToLabel(lblHanhKiem, hanhKiemValue);

                // Load xếp loại (có chứa học lực và xếp loại tổng kết)
                var xepLoai = xepLoaiBUS.GetXepLoaiByStudent(maHocSinh.Value, maHocKy);
                if (xepLoai != null)
                {
                    // Học lực
                    if (!string.IsNullOrEmpty(xepLoai.HocLuc))
                    {
                        lblHocLuc.Text = xepLoai.HocLuc;
                        ApplyStyleToLabel(lblHocLuc, xepLoai.HocLuc);
                    }
                    else
                    {
                        lblHocLuc.Text = "Chưa có";
                        ApplyStyleToLabel(lblHocLuc, "Chưa có");
                    }

                    // Xếp loại tổng kết - tính từ học lực và hạnh kiểm
                    if (!string.IsNullOrEmpty(xepLoai.XepLoaiTongKet))
                    {
                        lblXepLoai.Text = xepLoai.XepLoaiTongKet;
                        ApplyStyleToLabel(lblXepLoai, xepLoai.XepLoaiTongKet);
                    }
                    else if (!string.IsNullOrEmpty(xepLoai.HocLuc) && !string.IsNullOrEmpty(hanhKiemValue) && hanhKiemValue != "Chưa có")
                    {
                        // Tính xếp loại tổng kết nếu chưa có
                        string xepLoaiTongKet = xepLoaiBUS.TinhXepLoaiTongKet(xepLoai.HocLuc, hanhKiemValue);
                        lblXepLoai.Text = xepLoaiTongKet;
                        ApplyStyleToLabel(lblXepLoai, xepLoaiTongKet);
                    }
                    else
                    {
                        lblXepLoai.Text = "Chưa có";
                        ApplyStyleToLabel(lblXepLoai, "Chưa có");
                    }
                }
                else
                {
                    lblHocLuc.Text = "Chưa có";
                    lblXepLoai.Text = "Chưa có";
                    ApplyStyleToLabel(lblHocLuc, "Chưa có");
                    ApplyStyleToLabel(lblXepLoai, "Chưa có");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi load hạnh kiểm, học lực, xếp loại: {ex.Message}");
                lblHanhKiem.Text = "Chưa có";
                lblHocLuc.Text = "Chưa có";
                lblXepLoai.Text = "Chưa có";
                ApplyStyleToLabel(lblHanhKiem, "Chưa có");
                ApplyStyleToLabel(lblHocLuc, "Chưa có");
                ApplyStyleToLabel(lblXepLoai, "Chưa có");
            }
        }

        /// <summary>
        /// Áp dụng màu và định dạng cho label dựa trên giá trị xếp loại
        /// </summary>
        private void ApplyStyleToLabel(Guna.UI2.WinForms.Guna2HtmlLabel label, string value)
        {
            // Đặt font chữ đậm
            label.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // Áp dụng màu theo giá trị
            switch (value)
            {
                case "Giỏi":
                case "Tốt":
                    label.ForeColor = Color.FromArgb(21, 128, 61); // Xanh lá đậm
                    break;
                case "Khá":
                    label.ForeColor = Color.FromArgb(29, 78, 216); // Xanh dương
                    break;
                case "Trung bình":
                case "Trung Bình":
                    label.ForeColor = Color.FromArgb(194, 65, 12); // Cam
                    break;
                case "Yếu":
                case "Kém":
                    label.ForeColor = Color.FromArgb(185, 28, 28); // Đỏ
                    break;
                default:
                    label.ForeColor = Color.FromArgb(107, 114, 128); // Xám cho "Chưa có"
                    break;
            }
        }

        /// <summary>
        /// Áp dụng màu cho cột điểm trung bình
        /// </summary>
        private void ApplyColorToDiemTB()
        {
            foreach (DataGridViewRow row in tbThanhTich.Rows)
            {
                if (row.Cells["diemTB"].Value != null && !string.IsNullOrEmpty(row.Cells["diemTB"].Value.ToString()))
                {
                    if (float.TryParse(row.Cells["diemTB"].Value.ToString(), out float score))
                    {
                        if (score >= 8.0)
                        {
                            row.Cells["diemTB"].Style.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                            row.Cells["diemTB"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        }
                        else if (score >= 6.5)
                        {
                            row.Cells["diemTB"].Style.ForeColor = Color.FromArgb(30, 136, 229); // Xanh dương
                            row.Cells["diemTB"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        }
                        else
                        {
                            row.Cells["diemTB"].Style.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                            row.Cells["diemTB"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        }
                    }
                }
            }
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
        /// Hủy đăng ký event khi form đóng để tránh memory leak
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                // Hủy đăng ký event handler
                if (cbHocKyNamHoc != null)
                {
                    cbHocKyNamHoc.SelectedIndexChanged -= cbHocKyNamHoc_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đóng form: {ex.Message}");
            }
            finally
            {
                base.OnFormClosing(e);
            }
        }

    }
}
