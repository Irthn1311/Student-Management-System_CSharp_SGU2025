using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FormChonHocSinhPhanLop : Form
    {
        private int maLop;
        private string tenLop;
        private int maHocKy;
        private int soChoTrong;
        private List<HocSinhDTO> danhSachHocSinh;
        private PhanLopBLL phanLopBLL;
        private HocKyBUS hocKyBUS;
        private bool viewOnly; // Chế độ chỉ xem, không cho phân lớp

        /// <summary>
        /// Constructor cho FormChonHocSinhPhanLop
        /// </summary>
        /// <param name="maLop">Mã lớp</param>
        /// <param name="tenLop">Tên lớp</param>
        /// <param name="danhSachHocSinh">Danh sách học sinh</param>
        /// <param name="maHocKy">Mã học kỳ</param>
        /// <param name="soChoTrong">Số chỗ trống</param>
        /// <param name="viewOnly">Nếu true, chỉ cho xem danh sách, không cho phân lớp (disable các nút)</param>
        public FormChonHocSinhPhanLop(int maLop, string tenLop, List<HocSinhDTO> danhSachHocSinh, int maHocKy, int soChoTrong, bool viewOnly = false)
        {
            InitializeComponent();
            this.maLop = maLop;
            this.tenLop = tenLop;
            this.maHocKy = maHocKy;
            this.soChoTrong = soChoTrong;
            this.danhSachHocSinh = danhSachHocSinh ?? new List<HocSinhDTO>();
            this.phanLopBLL = new PhanLopBLL();
            this.hocKyBUS = new HocKyBUS();
            this.viewOnly = viewOnly;

            this.Load += FormChonHocSinhPhanLop_Load;
        }

        private void FormChonHocSinhPhanLop_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin lớp
            lblThongTinLop.Text = $"Lớp: {tenLop} | Số chỗ trống: {soChoTrong}";
            
            // Load danh sách học sinh vào DataGridView
            LoadDanhSachHocSinh();
            
            // Cập nhật số lượng đã chọn
            CapNhatSoLuongDaChon();

            // ✅ Nếu chế độ viewOnly, disable các nút và checkbox
            if (viewOnly)
            {
                btnPhanLop.Enabled = false;
                btnChonTatCa.Enabled = false;
                
                // Disable cột checkbox
                dgvHocSinh.Columns["colChon"].ReadOnly = true;
                
                // Thay đổi header và text để người dùng biết đây là chế độ xem
                lblHeader.Text = "Xem danh sách học sinh chưa phân lớp";
                btnPhanLop.Text = "Chưa khả dụng";
                lblThongTinLop.Text = $"Lớp: {tenLop} | Số chỗ trống: {soChoTrong} | (Chỉ xem)";
            }
        }

        private void LoadDanhSachHocSinh()
        {
            dgvHocSinh.Rows.Clear();

            foreach (var hs in danhSachHocSinh)
            {
                int rowIndex = dgvHocSinh.Rows.Add(
                    false,  // Checkbox
                    hs.MaHS,
                    hs.HoTen ?? "",
                    hs.GioiTinh ?? "",
                    hs.NgaySinh != default(DateTime) ? hs.NgaySinh.ToString("dd/MM/yyyy") : "",
                    hs.SdtHS ?? ""
                );

                // Lưu đối tượng học sinh vào Tag
                dgvHocSinh.Rows[rowIndex].Tag = hs;
            }

            // Cập nhật số lượng
            lblTongSo.Text = $"Tổng số: {danhSachHocSinh.Count} học sinh";
        }

        private void dgvHocSinh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ✅ Nếu viewOnly, không cho phép thay đổi checkbox
            if (viewOnly)
            {
                return;
            }

            // Xử lý click vào checkbox
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell cell = dgvHocSinh.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;
                if (cell != null)
                {
                    bool isChecked = Convert.ToBoolean(cell.Value);
                    
                    // Kiểm tra số chỗ trống
                    int soLuongDaChon = DemSoLuongDaChon();
                    if (!isChecked && soLuongDaChon >= soChoTrong)
                    {
                        MessageBox.Show(
                            $"Lớp '{tenLop}' chỉ còn {soChoTrong} chỗ trống. Vui lòng bỏ chọn một số học sinh khác!",
                            "Cảnh báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        cell.Value = false;
                        return;
                    }
                    
                    cell.Value = !isChecked;
                    CapNhatSoLuongDaChon();
                }
            }
        }

        private int DemSoLuongDaChon()
        {
            int count = 0;
            foreach (DataGridViewRow row in dgvHocSinh.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value))
                {
                    count++;
                }
            }
            return count;
        }

        private void CapNhatSoLuongDaChon()
        {
            int soLuongDaChon = DemSoLuongDaChon();
            lblDaChon.Text = $"Đã chọn: {soLuongDaChon}/{soChoTrong}";
            
            // Đổi màu nếu đã chọn đủ
            if (soLuongDaChon >= soChoTrong)
            {
                lblDaChon.ForeColor = Color.Red;
                lblDaChon.Font = new Font(lblDaChon.Font, FontStyle.Bold);
            }
            else
            {
                lblDaChon.ForeColor = Color.Black;
                lblDaChon.Font = new Font(lblDaChon.Font, FontStyle.Regular);
            }
        }

        private void btnChonTatCa_Click(object sender, EventArgs e)
        {
            int soLuongDaChon = DemSoLuongDaChon();
            int soLuongCoTheChon = Math.Min(soChoTrong, danhSachHocSinh.Count);
            
            if (soLuongDaChon >= soChoTrong)
            {
                // Bỏ chọn tất cả
                foreach (DataGridViewRow row in dgvHocSinh.Rows)
                {
                    row.Cells[0].Value = false;
                }
            }
            else
            {
                // Chọn đến khi đủ số chỗ trống
                int count = 0;
                foreach (DataGridViewRow row in dgvHocSinh.Rows)
                {
                    if (count < soChoTrong)
                    {
                        row.Cells[0].Value = true;
                        count++;
                    }
                    else
                    {
                        row.Cells[0].Value = false;
                    }
                }
            }
            
            CapNhatSoLuongDaChon();
        }

        private void btnPhanLop_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy danh sách học sinh đã chọn
                List<int> danhSachMaHS = new List<int>();
                foreach (DataGridViewRow row in dgvHocSinh.Rows)
                {
                    if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value))
                    {
                        int maHS = Convert.ToInt32(row.Cells[1].Value);
                        danhSachMaHS.Add(maHS);
                    }
                }

                if (danhSachMaHS.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một học sinh để phân lớp!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra số lượng
                if (danhSachMaHS.Count > soChoTrong)
                {
                    MessageBox.Show(
                        $"Số học sinh đã chọn ({danhSachMaHS.Count}) vượt quá số chỗ trống ({soChoTrong})!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Xác nhận
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc muốn phân {danhSachMaHS.Count} học sinh vào lớp '{tenLop}'?",
                    "Xác nhận phân lớp",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dr != DialogResult.Yes)
                    return;

                // Lấy HK1 và HK2 của năm học hiện tại
                HocKyDTO hocKyHienTai = hocKyBUS.LayHocKyTheoMa(maHocKy);
                if (hocKyHienTai == null)
                {
                    MessageBox.Show("Không tìm thấy học kỳ hiện tại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy tất cả học kỳ của năm học
                List<HocKyDTO> dsHocKy = hocKyBUS.DocDSHocKy();
                var hocKyCungNamHoc = dsHocKy
                    .Where(hk => hk.MaNamHoc == hocKyHienTai.MaNamHoc)
                    .OrderBy(hk => hk.TenHocKy)
                    .ToList();

                // Tìm HK1 và HK2
                HocKyDTO hk1 = hocKyCungNamHoc.FirstOrDefault(hk =>
                    (hk.TenHocKy.ToLower().Contains("i") && !hk.TenHocKy.ToLower().Contains("ii")) ||
                    (hk.TenHocKy.ToLower().Contains("1") && !hk.TenHocKy.ToLower().Contains("2")));
                HocKyDTO hk2 = hocKyCungNamHoc.FirstOrDefault(hk =>
                    hk.TenHocKy.ToLower().Contains("ii") || hk.TenHocKy.ToLower().Contains("2"));

                // Phân lớp cho từng học sinh
                int soLuongThanhCong = 0;
                int soLuongThatBai = 0;
                List<string> danhSachLoi = new List<string>();

                foreach (int maHS in danhSachMaHS)
                {
                    try
                    {
                        bool successHK1 = false;
                        bool successHK2 = false;

                        // Phân lớp cho HK1 (nếu có)
                        if (hk1 != null)
                        {
                            try
                            {
                                // Kiểm tra xem học sinh đã được phân lớp trong HK1 chưa
                                if (!phanLopBLL.CheckHocSinhDaPhanLop(maHS, hk1.MaHocKy))
                                {
                                    successHK1 = phanLopBLL.AddPhanLop(maHS, maLop, hk1.MaHocKy);
                                }
                                else
                                {
                                    successHK1 = true; // Đã có rồi, coi như thành công
                                }
                            }
                            catch (Exception ex)
                            {
                                danhSachLoi.Add($"Học sinh {maHS} - HK1: {ex.Message}");
                            }
                        }

                        // Phân lớp cho HK2 (nếu có)
                        if (hk2 != null)
                        {
                            try
                            {
                                // Kiểm tra xem học sinh đã được phân lớp trong HK2 chưa
                                if (!phanLopBLL.CheckHocSinhDaPhanLop(maHS, hk2.MaHocKy))
                                {
                                    successHK2 = phanLopBLL.AddPhanLop(maHS, maLop, hk2.MaHocKy);
                                }
                                else
                                {
                                    successHK2 = true; // Đã có rồi, coi như thành công
                                }
                            }
                            catch (Exception ex)
                            {
                                danhSachLoi.Add($"Học sinh {maHS} - HK2: {ex.Message}");
                            }
                        }

                        if ((hk1 == null || successHK1) && (hk2 == null || successHK2))
                        {
                            soLuongThanhCong++;
                        }
                        else
                        {
                            soLuongThatBai++;
                        }
                    }
                    catch (Exception ex)
                    {
                        soLuongThatBai++;
                        danhSachLoi.Add($"Học sinh {maHS}: {ex.Message}");
                    }
                }

                // Hiển thị kết quả
                string message = $"Đã phân lớp thành công {soLuongThanhCong} học sinh vào lớp '{tenLop}'.";
                if (soLuongThatBai > 0)
                {
                    message += $"\n\nCó {soLuongThatBai} học sinh phân lớp thất bại.";
                    if (danhSachLoi.Count > 0)
                    {
                        message += "\n\nChi tiết lỗi:\n" + string.Join("\n", danhSachLoi.Take(5));
                        if (danhSachLoi.Count > 5)
                        {
                            message += $"\n... và {danhSachLoi.Count - 5} lỗi khác.";
                        }
                    }
                }

                MessageBoxIcon icon = soLuongThatBai > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information;
                MessageBox.Show(message, "Kết quả phân lớp", MessageBoxButtons.OK, icon);

                // Đóng form nếu thành công
                if (soLuongThanhCong > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi phân lớp: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.ToLower().Trim();
            
            if (string.IsNullOrEmpty(keyword))
            {
                LoadDanhSachHocSinh();
                return;
            }

            // Lọc danh sách học sinh
            var danhSachLoc = danhSachHocSinh.Where(hs =>
                hs.MaHS.ToString().Contains(keyword) ||
                (hs.HoTen != null && hs.HoTen.ToLower().Contains(keyword)) ||
                (hs.SdtHS != null && hs.SdtHS.Contains(keyword))
            ).ToList();

            // Hiển thị kết quả lọc
            dgvHocSinh.Rows.Clear();
            foreach (var hs in danhSachLoc)
            {
                int rowIndex = dgvHocSinh.Rows.Add(
                    false,
                    hs.MaHS,
                    hs.HoTen ?? "",
                    hs.GioiTinh ?? "",
hs.NgaySinh != default(DateTime) ? hs.NgaySinh.ToString("dd/MM/yyyy") : "",
hs.SdtHS ?? ""
                );
                dgvHocSinh.Rows[rowIndex].Tag = hs;
            }

            lblTongSo.Text = $"Tìm thấy: {danhSachLoc.Count} học sinh";
        }
    }
}

