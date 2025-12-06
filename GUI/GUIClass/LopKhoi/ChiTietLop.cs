using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Scheduling;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;

using OfficeOpenXml;
using OfficeOpenXml.Style;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ChiTietLop : Form
    {
        private int maLop;
        private LopHocBUS lopHocBUS;
        private GiaoVienBUS giaoVienBUS;
        private PhanLopBLL phanLopBLL;
        private HocKyBUS hocKyBUS;
        private ThoiKhoaBieuBUS tkbBUS;
        private MonHocBUS monHocBUS;
        private NamHocBUS namHocBUS;
        private int maHocKyHienTai; // Lưu học kỳ đang được chọn
        private string genderFilter = "all"; // all, nam, nu
        
        // Biến để track vị trí in (tránh vòng lặp vô hạn)
        private int currentPrintIndex = 0;

        public ChiTietLop(int maLop)
        {
            InitializeComponent();
            this.maLop = maLop;
            lopHocBUS = new LopHocBUS();
            giaoVienBUS = new GiaoVienBUS();
            phanLopBLL = new PhanLopBLL();
            hocKyBUS = new HocKyBUS();
            tkbBUS = new ThoiKhoaBieuBUS();
            monHocBUS = new MonHocBUS();
            namHocBUS = new NamHocBUS();
            maHocKyHienTai = 0;

            LoadHocKyComboBox();
            LoadThongTinLop();
            //LoadHocSinhChuaPhanLop(); // Load dropdown học sinh chưa phân lớp
            LoadDanhSachHocSinh();
            //LoadThoiKhoaBieu();
            LoadThongKe();
            
            // 🆕 Thêm button "Gửi yêu cầu chuyển lớp" CHỈ cho PHỤ HUYNH
            // ✅ ADMIN không được phép gửi yêu cầu, chỉ được quản lý và duyệt
            if (SessionManager.VaiTro == "PhuHuynh")
            {
                ThemButtonGuiYeuCauChuyenLop();
            }
            
            // 🆕 Thêm button "Quản lý yêu cầu chuyển lớp" CHỈ cho ADMIN
            if (SessionManager.VaiTro == "Admin")
            {
                ThemButtonQuanLyYeuCauChuyenLop();
            }
        }

        // ✅ LOAD DROPDOWN HỌC KỲ (CHỈ HIỂN THỊ HỌC KỲ TRONG NĂM HỌC HIỆN TẠI)
        private void LoadHocKyComboBox()
        {
            try
            {
                cbHocKy.Items.Clear();
                
                // Lấy tất cả học kỳ
                List<HocKyDTO> dsHocKy = hocKyBUS.DocDSHocKy();
                
                if (dsHocKy == null || dsHocKy.Count == 0)
                {
                    cbHocKy.Items.Add("Không có học kỳ");
                    return;
                }

                // Tìm năm học hiện tại (đang diễn ra)
                List<NamHocDTO> dsNamHoc = namHocBUS.DocDSNamHoc();
                NamHocDTO namHocHienTai = dsNamHoc?.FirstOrDefault(nh => 
                    nh.NgayBD.Date <= DateTime.Today && nh.NgayKT.Date >= DateTime.Today);

                // Nếu không có năm học đang diễn ra, lấy năm học gần nhất
                if (namHocHienTai == null && dsNamHoc != null && dsNamHoc.Count > 0)
                {
                    namHocHienTai = dsNamHoc.OrderByDescending(nh => nh.NgayBD).FirstOrDefault();
                }

                // Lọc chỉ các học kỳ trong năm học hiện tại
                List<HocKyDTO> dsHocKyTheoNam;
                if (namHocHienTai != null)
                {
                    dsHocKyTheoNam = dsHocKy
                        .Where(hk => hk.MaNamHoc == namHocHienTai.MaNamHoc)
                        .OrderBy(hk => hk.NgayBD)
                        .ToList();
                }
                else
                {
                    // Fallback: nếu không tìm thấy năm học hiện tại, lấy tất cả
                    dsHocKyTheoNam = dsHocKy.OrderByDescending(hk => hk.NgayBD).ToList();
                }

                if (dsHocKyTheoNam.Count == 0)
                {
                    cbHocKy.Items.Add("Không có học kỳ trong năm học này");
                    return;
                }

                // Thêm các học kỳ vào ComboBox
                string tenNamHoc = namHocHienTai?.TenNamHoc ?? "Năm học";
                foreach (var hk in dsHocKyTheoNam)
                {
                    string displayText = $"{hk.TenHocKy}";
                    cbHocKy.Items.Add(new ComboBoxItem { Text = displayText, Value = hk.MaHocKy });
                }

                // Chọn học kỳ đang diễn ra trong năm học hiện tại
                HocKyDTO hocKyDangDienRa = dsHocKyTheoNam.FirstOrDefault(hk => hk.TrangThai == "Đang diễn ra");
                if (hocKyDangDienRa != null)
                {
                    for (int i = 0; i < cbHocKy.Items.Count; i++)
                    {
                        ComboBoxItem item = (ComboBoxItem)cbHocKy.Items[i];
                        if ((int)item.Value == hocKyDangDienRa.MaHocKy)
                        {
                            cbHocKy.SelectedIndex = i;
                            maHocKyHienTai = hocKyDangDienRa.MaHocKy;
                            break;
                        }
                    }
                }
                else if (cbHocKy.Items.Count > 0)
                {
                    cbHocKy.SelectedIndex = 0;
                    ComboBoxItem firstItem = (ComboBoxItem)cbHocKy.Items[0];
                    maHocKyHienTai = (int)firstItem.Value;
                }

                // Cập nhật hiển thị năm học
                UpdateNamHocDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách học kỳ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ XỬ LÝ KHI CHỌN HỌC KỲ
        private void cbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKy.SelectedItem == null) return;

            try
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cbHocKy.SelectedItem;
                maHocKyHienTai = (int)selectedItem.Value;

                // Cập nhật hiển thị năm học
                UpdateNamHocDisplay();

                // Reload dữ liệu theo học kỳ mới
                LoadDanhSachHocSinh();
                //LoadThoiKhoaBieu();
                LoadThongKe();
                //LoadHocSinhChuaPhanLop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn học kỳ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CẬP NHẬT HIỂN THỊ NĂM HỌC
        private void UpdateNamHocDisplay()
        {
            try
            {
                if (maHocKyHienTai <= 0)
                {
                    lblNamHoc.Text = "Năm học: --";
                    return;
                }

                HocKyDTO hocKy = hocKyBUS.LayHocKyTheoMa(maHocKyHienTai);
                if (hocKy != null && !string.IsNullOrEmpty(hocKy.MaNamHoc))
                {
                    NamHocDTO namHoc = namHocBUS.LayNamHocTheoMa(hocKy.MaNamHoc);
                    if (namHoc != null)
                    {
                        lblNamHoc.Text = $"Năm học: {namHoc.TenNamHoc}";
                    }
                    else
                    {
                        lblNamHoc.Text = $"Năm học: {hocKy.MaNamHoc}";
                    }
                }
                else
                {
                    lblNamHoc.Text = "Năm học: --";
                }
            }
            catch
            {
                lblNamHoc.Text = "Năm học: --";
            }
        }

        // ✅ DANH SÁCH HỌC SINH CHƯA PHÂN LỚP (GỐC)
        private List<HocSinhDTO> danhSachHocSinhChuaPhanLopGoc = new List<HocSinhDTO>();
        
        // ✅ DANH SÁCH HỌC SINH TRONG LỚP (GỐC) - Để tìm kiếm
        private List<HocSinhDTO> danhSachHocSinhGoc = new List<HocSinhDTO>();

        // ✅ LOAD DANH SÁCH HỌC SINH CHƯA PHÂN LỚP
        //private void LoadHocSinhChuaPhanLop()
        //{
        //    try
        //    {
        //        cbHocSinhChuaPhanLop.Items.Clear();
        //        txtTimKiemHS.Text = "";

        //        if (maHocKyHienTai <= 0)
        //        {
        //            lblSoLuongHSChuaPhanLop.Text = "";
        //            return;
        //        }

        //        danhSachHocSinhChuaPhanLopGoc = phanLopBLL.GetHocSinhChuaPhanLop(maHocKyHienTai);
                
        //        if (danhSachHocSinhChuaPhanLopGoc == null)
        //        {
        //            danhSachHocSinhChuaPhanLopGoc = new List<HocSinhDTO>();
        //        }

        //        // Cập nhật số lượng
        //        lblSoLuongHSChuaPhanLop.Text = $"({danhSachHocSinhChuaPhanLopGoc.Count} học sinh chưa phân lớp)";

        //        // Load vào combobox
        //        FilterAndLoadHocSinh("");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi tải danh sách học sinh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // ✅ LỌC VÀ LOAD HỌC SINH VÀO COMBOBOX (REALTIME)
        private void FilterAndLoadHocSinh(string searchText)
        {
            try
            {
                //// Kiểm tra danh sách gốc đã được load chưa
                //if (danhSachHocSinhChuaPhanLopGoc == null)
                //{
                //    danhSachHocSinhChuaPhanLopGoc = new List<HocSinhDTO>();
                //}

                //cbHocSinhChuaPhanLop.Items.Clear();
                //cbHocSinhChuaPhanLop.Items.Add("-- Chọn học sinh --");

                //if (danhSachHocSinhChuaPhanLopGoc.Count == 0)
                //{
                //    cbHocSinhChuaPhanLop.SelectedIndex = 0;
                //    lblSoLuongHSChuaPhanLop.Text = "(0 học sinh chưa phân lớp)";
                //    return;
                //}

                // Lọc học sinh theo từ khóa tìm kiếm (realtime - lọc ngay cả khi chỉ có 1 ký tự)
                List<HocSinhDTO> filteredList;
                
                if (string.IsNullOrEmpty(searchText))
                {
                    // Nếu xóa hết thì hiển thị lại tất cả
                    filteredList = danhSachHocSinhChuaPhanLopGoc.OrderBy(h => h.HoTen).ToList();
                }
                else
                {
                    // Lọc theo từ khóa (không cần trim, lọc ngay cả khi có khoảng trắng)
                    string keyword = searchText.ToLower();
                    filteredList = danhSachHocSinhChuaPhanLopGoc
                        .Where(hs => 
                            hs.MaHS.ToString().Contains(keyword) ||
                            (hs.HoTen != null && hs.HoTen.ToLower().Contains(keyword)) ||
                            (hs.SdtHS != null && hs.SdtHS.Contains(keyword))
                        )
                        .OrderBy(h => h.HoTen)
                        .ToList();
                }

                //// Thêm vào combobox với format rõ ràng hơn
                //foreach (var hs in filteredList)
                //{
                //    // Hiển thị: Mã HS - Họ tên (SĐT nếu có)
                //    string displayText = $"{hs.MaHS} - {hs.HoTen}";
                //    if (!string.IsNullOrEmpty(hs.SdtHS))
                //    {
                //        displayText += $" ({hs.SdtHS})";
                //    }

                //    cbHocSinhChuaPhanLop.Items.Add(new ComboBoxItem 
                //    { 
                //        Text = displayText, 
                //        Value = hs.MaHS 
                //    });
                //}

                //// Chọn item đầu tiên (không trigger event)
                //if (cbHocSinhChuaPhanLop.Items.Count > 0)
                //{
                //    cbHocSinhChuaPhanLop.SelectedIndex = 0;
                //}

                // Cập nhật số lượng sau khi lọc
                if (!string.IsNullOrEmpty(searchText))
                {
                    lblSoLuongHSChuaPhanLop.Text = $"({filteredList.Count}/{danhSachHocSinhChuaPhanLopGoc.Count} học sinh)";
                }
                else
                {
                    lblSoLuongHSChuaPhanLop.Text = $"({danhSachHocSinhChuaPhanLopGoc.Count} học sinh chưa phân lớp)";
                }
            }
            catch (Exception ex)
            {
                // Không hiển thị MessageBox để không làm gián đoạn việc gõ
                Console.WriteLine($"Lỗi khi lọc học sinh: {ex.Message}");
            }
        }

        // ✅ XỬ LÝ TÌM KIẾM HỌC SINH TRONG BẢNG (REALTIME)
        private void txtTimKiemHS_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // ✅ ÁP DỤNG BỘ LỌC (TÌM KIẾM + GIỚI TÍNH)
        private void ApplyFilters()
        {
            try
            {
                if (danhSachHocSinhGoc == null || danhSachHocSinhGoc.Count == 0)
                {
                    return;
                }

                string searchText = txtTimKiemHS.Text ?? "";
                List<HocSinhDTO> filteredList = danhSachHocSinhGoc;

                // Lọc theo từ khóa tìm kiếm
                if (!string.IsNullOrEmpty(searchText))
                {
                    string keyword = searchText.ToLower();
                    filteredList = filteredList
                        .Where(hs => 
                            hs.MaHS.ToString().Contains(keyword) ||
                            (hs.HoTen != null && hs.HoTen.ToLower().Contains(keyword)) ||
                            (hs.SdtHS != null && hs.SdtHS.Contains(keyword)) ||
                            (hs.Email != null && hs.Email.ToLower().Contains(keyword))
                        )
                        .ToList();
                }

                // Lọc theo giới tính
                if (genderFilter == "nam")
                {
                    filteredList = filteredList.Where(hs => hs.GioiTinh == "Nam").ToList();
                }
                else if (genderFilter == "nu")
                {
                    filteredList = filteredList.Where(hs => hs.GioiTinh == "Nữ").ToList();
                }

                // Hiển thị kết quả đã lọc
                HienThiDanhSachHocSinh(filteredList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lọc: {ex.Message}");
            }
        }

        // Helper class cho ComboBox
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }

        private void LoadThongTinLop()
        {
            try
            {
                LopDTO lop = lopHocBUS.LayLopTheoId(maLop);
                if (lop == null)
                {
                    MessageBox.Show("Không tìm thấy lớp học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                lblMaLop.Text = $"Mã lớp: {lop.maLop}";
                lblTenLop.Text = $"Tên lớp: {lop.tenLop}";
                lblKhoi.Text = $"Khối: {lop.maKhoi}";
                lblSiSo.Text = $"Sĩ số: {lop.siSo}";

                // Thông tin giáo viên chủ nhiệm
                if (!string.IsNullOrEmpty(lop.maGVCN))
                {
                    try
                    {
                        GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(lop.maGVCN);
                        if (gv != null)
                        {
                            lblGVCN.Text = $"Giáo viên CN: {gv.HoTen}";
                            lblSDTGV.Text = $"SĐT: {gv.SoDienThoai ?? "N/A"}";
                            lblEmailGV.Text = $"Email: {gv.Email ?? "N/A"}";
                        }
                        else
                        {
                            lblGVCN.Text = "Giáo viên CN: Chưa phân công";
                        }
                    }
                    catch
                    {
                        lblGVCN.Text = "Giáo viên CN: Lỗi khi tải thông tin";
                    }
                }
                else
                {
                    lblGVCN.Text = "Giáo viên CN: Chưa phân công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachHocSinh()
        {
            try
            {
                dgvHocSinh.Rows.Clear();
                danhSachHocSinhGoc.Clear();

                if (maHocKyHienTai <= 0)
                {
                    lblThongBaoHS.Text = "Vui lòng chọn học kỳ";
                    return;
                }

                // Lấy danh sách học sinh trong lớp theo học kỳ được chọn
                List<HocSinhDTO> dsHocSinh = phanLopBLL.GetHocSinhByLop(maLop, maHocKyHienTai);
                
                if (dsHocSinh == null || dsHocSinh.Count == 0)
                {
                    lblThongBaoHS.Text = "Lớp chưa có học sinh trong học kỳ này";
                    return;
                }

                // Lưu danh sách gốc để tìm kiếm
                danhSachHocSinhGoc = dsHocSinh.OrderBy(h => h.HoTen).ToList();

                // Hiển thị danh sách (có thể đã được lọc)
                HienThiDanhSachHocSinh(danhSachHocSinhGoc);
                
                // ✅ Ẩn cột "Chuyển lớp" (không cần thiết vì đã có button "Gửi yêu cầu chuyển lớp")
                if (dgvHocSinh.Columns["ChuyenLop"] != null)
                {
                    dgvHocSinh.Columns["ChuyenLop"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách học sinh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ HIỂN THỊ DANH SÁCH HỌC SINH VÀO DATAGRIDVIEW
        private void HienThiDanhSachHocSinh(List<HocSinhDTO> dsHocSinh)
        {
            try
            {
                dgvHocSinh.Rows.Clear();

                // ✅ Ẩn cột "Chuyển lớp" (không cần thiết vì đã có button "Gửi yêu cầu chuyển lớp")
                if (dgvHocSinh.Columns["ChuyenLop"] != null)
                {
                    dgvHocSinh.Columns["ChuyenLop"].Visible = false;
                }

                if (dsHocSinh == null || dsHocSinh.Count == 0)
                {
                    lblThongBaoHS.Text = " Không có học sinh";
                    return;
                }

                // Đếm số lượng nam/nữ
                int soNam = dsHocSinh.Count(hs => hs.GioiTinh == "Nam");
                int soNu = dsHocSinh.Count(hs => hs.GioiTinh == "Nữ");
                int tongGoc = danhSachHocSinhGoc?.Count ?? 0;

                // Hiển thị thống kê với icon
                string statsText = $"Tổng: {dsHocSinh.Count} học sinh";
                if (dsHocSinh.Count < tongGoc)
                {
                    statsText += $"/{tongGoc}";
                }
                statsText += $" | Nam: {soNam} | Nữ: {soNu}";
                
                lblThongBaoHS.Text = statsText;

                foreach (HocSinhDTO hs in dsHocSinh)
                {
                    int rowIndex = dgvHocSinh.Rows.Add(
                        hs.MaHS,
                        hs.HoTen,
                        hs.NgaySinh.ToString("dd/MM/yyyy"),
                        hs.GioiTinh,
                        hs.SdtHS ?? "N/A",
                        hs.Email ?? "N/A",
                        "Chuyển lớp",
                        "Xóa"
                    );
                    // Lưu MaHocKy vào Tag để dùng khi xóa/chuyển lớp
                    dgvHocSinh.Rows[rowIndex].Tag = maHocKyHienTai;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị danh sách học sinh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void LoadThoiKhoaBieu()
        //{
        //    try
        //    {
        //        tableThoiKhoaBieu.Controls.Clear();

        //        if (maHocKyHienTai <= 0)
        //        {
        //            lblThongBaoTKB.Text = "Vui lòng chọn học kỳ";
        //            return;
        //        }

        //        // Lấy thời khóa biểu của lớp theo học kỳ được chọn
        //        var tkb = tkbBUS.GetOfficialSchedule(maHocKyHienTai, maLop);
                
        //        if (tkb == null || tkb.Count == 0)
        //        {
        //            lblThongBaoTKB.Text = "Lớp chưa có thời khóa biểu";
        //            return;
        //        }

        //        // Đếm số tiết thực tế (không trùng lặp)
        //        var uniqueSlots = tkb.GroupBy(s => new { s.Thu, s.Tiet }).Count();
        //        lblThongBaoTKB.Text = $"Số tiết: {uniqueSlots}";

        //        // Tạo header cho bảng
        //        CreateTableHeader();

        //        // Tạo dictionary để map (Thu, Tiet) -> slot
        //        var slotDict = new Dictionary<(int Thu, int Tiet), Scheduling.AssignmentSlot>();
        //        foreach (var slot in tkb)
        //        {
        //            slotDict[(slot.Thu, slot.Tiet)] = slot;
        //        }

        //        // Điền dữ liệu vào bảng (5 tiết, 6 ngày: Thứ 2-7)
        //        for (int tiet = 1; tiet <= 5; tiet++)
        //        {
        //            // Cột đầu tiên: Số tiết
        //            var lblTiet = new Label
        //            {
        //                Text = tiet.ToString(),
        //                Dock = DockStyle.Fill,
        //                TextAlign = ContentAlignment.MiddleCenter,
        //                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
        //                BackColor = Color.White,
        //                ForeColor = Color.Black
        //            };
        //            tableThoiKhoaBieu.Controls.Add(lblTiet, 0, tiet);

        //            // Các cột còn lại: Thứ 2-7
        //            for (int thu = 2; thu <= 7; thu++)
        //            {
        //                int col = thu - 1; // Thu 2 -> col 1, Thu 7 -> col 6
        //                int row = tiet;

        //                if (slotDict.TryGetValue((thu, tiet), out var slot))
        //                {
        //                    // Lấy tên môn học
        //                    string tenMon = $"Môn {slot.MaMon}";
        //                    try
        //                    {
        //                        var mon = monHocBUS.LayDSMonHocTheoId(slot.MaMon);
        //                        if (mon != null) tenMon = mon.tenMon;
        //                    }
        //                    catch { }

        //                    // Lấy tên giáo viên
        //                    string tenGV = slot.MaGV;
        //                    try
        //                    {
        //                        var gv = giaoVienBUS.LayGiaoVienTheoMa(slot.MaGV);
        //                        if (gv != null && !string.IsNullOrEmpty(gv.HoTen))
        //                        {
        //                            // Lấy họ tên ngắn gọn (chỉ họ và tên, không lấy tên đệm)
        //                            string[] parts = gv.HoTen.Split(' ');
        //                            if (parts.Length >= 3)
        //                            {
        //                                tenGV = $"{parts[0]} {parts[1]} {parts[parts.Length - 1]}"; // Họ + Tên
        //                            }
        //                            else
        //                            {
        //                                tenGV = gv.HoTen;
        //                            }
        //                        }
        //                    }
        //                    catch { }

        //                    // Tạo label hiển thị môn học và giáo viên
        //                    var lblMon = new Label
        //                    {
        //                        Text = $"{tenMon} -- {tenGV}",
        //                        Dock = DockStyle.Fill,
        //                        TextAlign = ContentAlignment.MiddleCenter,
        //                        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
        //                        ForeColor = Color.Black,
        //                        BackColor = GetBackgroundColorForSubject(tenMon)
        //                    };

        //                    tableThoiKhoaBieu.Controls.Add(lblMon, col, row);
        //                }
        //                else
        //                {
        //                    // Ô trống
        //                    var lblEmpty = new Label
        //                    {
        //                        Text = "",
        //                        Dock = DockStyle.Fill,
        //                        BackColor = Color.White
        //                    };
        //                    tableThoiKhoaBieu.Controls.Add(lblEmpty, col, row);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi tải thời khóa biểu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // ✅ TẠO HEADER CHO BẢNG
        //private void CreateTableHeader()
        //{
        //    // Ô trống góc trên bên trái
        //    var lblEmpty = new Label
        //    {
        //        Text = "",
        //        Dock = DockStyle.Fill,
        //        BackColor = Color.FromArgb(173, 216, 230) // Màu xanh nhạt như trong ảnh
        //    };
        //    tableThoiKhoaBieu.Controls.Add(lblEmpty, 0, 0);

        //    // Header các ngày: Thứ 2-7
        //    string[] thuArray = { "", "", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
        //    for (int thu = 2; thu <= 7; thu++)
        //    {
        //        int col = thu - 1;
        //        var lblThu = new Label
        //        {
        //            Text = thuArray[thu],
        //            Dock = DockStyle.Fill,
        //            TextAlign = ContentAlignment.MiddleCenter,
        //            Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
        //            ForeColor = Color.Black,
        //            BackColor = Color.FromArgb(173, 216, 230) // Màu xanh nhạt như trong ảnh
        //        };
        //        tableThoiKhoaBieu.Controls.Add(lblThu, col, 0);
        //    }
        //}

        // ✅ LẤY MÀU NỀN NHẸ NHÀNG CHO MÔN HỌC
        private Color GetBackgroundColorForSubject(string subject)
        {
            // Màu nền nhẹ nhàng, không quá đậm
            switch (subject)
            {
                case "Toán học":
                case "Toán":
                    return Color.FromArgb(240, 248, 255); // Xanh nhạt
                case "Vật lý":
                case "Vật Lý":
                    return Color.FromArgb(255, 250, 240); // Cam nhạt
                case "Tiếng Anh":
                case "Ngoại ngữ":
                    return Color.FromArgb(248, 245, 255); // Tím nhạt
                case "Sinh học":
                    return Color.FromArgb(240, 253, 250); // Xanh lá nhạt
                case "Hóa học":
                    return Color.FromArgb(255, 240, 245); // Hồng nhạt
                case "Ngữ văn":
                case "Văn học":
                    return Color.FromArgb(240, 253, 244); // Xanh lá nhạt
                case "Lịch sử":
                    return Color.FromArgb(255, 252, 232); // Vàng nhạt
                case "Địa lý":
                case "Địa lí":
                    return Color.FromArgb(245, 243, 255); // Tím nhạt
                case "GDCD":
                case "Giáo Dục Công Dân":
                case "GD Kinh tế & Pháp luật":
                    return Color.FromArgb(255, 242, 242); // Đỏ nhạt
                case "Thể dục":
                case "Giáo dục thể chất":
                    return Color.FromArgb(240, 253, 244); // Xanh lá nhạt
                case "Quốc phòng":
                case "Giáo dục Quốc phòng và An ninh":
                case "GDQP-AN":
                case "GDQP":
                    return Color.FromArgb(248, 250, 252); // Xám nhạt
                case "Tin học":
                case "Công nghệ":
                    return Color.FromArgb(248, 250, 252); // Xám nhạt
                case "Sinh hoạt":
                    return Color.FromArgb(255, 250, 240); // Vàng nhạt
                default:
                    return Color.White;
            }
        }

        // ✅ LẤY MÀU SẮC CHO MÔN HỌC
        private (Color TextColor, Color ProgressColor1, Color ProgressColor2) GetColorSetForSubject(string subject)
        {
            switch (subject)
            {
                case "Toán học":
                case "Toán":
                    return (Color.FromArgb(30, 64, 175), Color.FromArgb(96, 165, 250), Color.FromArgb(239, 246, 255));

                case "Vật lý":
                case "Vật Lý":
                    return (Color.FromArgb(154, 52, 18), Color.FromArgb(251, 146, 60), Color.FromArgb(255, 247, 237));

                case "Tiếng Anh":
                    return (Color.FromArgb(107, 33, 168), Color.FromArgb(192, 132, 252), Color.FromArgb(245, 243, 255));

                case "Sinh học":
                    return (Color.FromArgb(17, 94, 89), Color.FromArgb(45, 212, 191), Color.FromArgb(240, 253, 250));

                case "Hóa học":
                    return (Color.FromArgb(157, 23, 77), Color.FromArgb(244, 114, 182), Color.FromArgb(253, 242, 248));

                case "Ngữ văn":
                    return (Color.FromArgb(22, 101, 52), Color.FromArgb(74, 222, 128), Color.FromArgb(240, 253, 244));

                case "Lịch sử":
                    return (Color.FromArgb(133, 77, 14), Color.FromArgb(250, 204, 21), Color.FromArgb(254, 252, 232));

                case "Địa lý":
                    return (Color.FromArgb(55, 48, 163), Color.FromArgb(129, 140, 248), Color.FromArgb(238, 242, 255));

                case "GDCD":
                case "Giáo Dục Công Dân":
                case "GD Kinh tế & Pháp luật":
                    return (Color.FromArgb(153, 27, 27), Color.FromArgb(248, 113, 113), Color.FromArgb(254, 242, 242));

                case "Tự học":
                    return (Color.Black, Color.FromArgb(209, 213, 219), Color.FromArgb(249, 250, 251));

                case "Thể dục":
                case "Giáo dục thể chất":
                    return (Color.FromArgb(21, 128, 61), Color.FromArgb(74, 222, 128), Color.FromArgb(220, 252, 231));

                case "Quốc phòng":
                case "Giáo dục Quốc phòng và An ninh":
                case "GDQP-AN":
                    return (Color.FromArgb(71, 85, 105), Color.FromArgb(148, 163, 184), Color.FromArgb(241, 245, 249));

                case "Tin học":
                case "Công nghệ":
                    return (Color.FromArgb(15, 23, 42), Color.FromArgb(100, 116, 139), Color.FromArgb(241, 245, 249));

                default:
                    return (Color.Black, Color.Gainsboro, Color.WhiteSmoke);
            }
        }

        private string GetThuTiengViet(int thu)
        {
            // Thu trong DB: 2=Thứ 2, 3=Thứ 3, ..., 6=Thứ 6
            string[] thuArray = { "", "", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            if (thu >= 2 && thu <= 7)
                return thuArray[thu];
            return $"Thứ {thu}";
        }

        private void LoadThongKe()
        {
            try
            {
                if (maHocKyHienTai <= 0)
                {
                    lblThongKe.Text = "Vui lòng chọn học kỳ";
                    return;
                }

                // Lấy danh sách học sinh theo học kỳ được chọn
                List<HocSinhDTO> dsHocSinh = phanLopBLL.GetHocSinhByLop(maLop, maHocKyHienTai);
                
                if (dsHocSinh == null || dsHocSinh.Count == 0)
                {
                    lblThongKe.Text = "Lớp chưa có học sinh";
                    return;
                }

                int tongHS = dsHocSinh.Count;
                int soNam = dsHocSinh.Count(h => h.GioiTinh == "Nam");
                int soNu = dsHocSinh.Count(h => h.GioiTinh == "Nữ");

                lblThongKe.Text = $"Tổng: {tongHS} học sinh | Nam: {soNam} | Nữ: {soNu}";
            }
            catch (Exception ex)
            {
                lblThongKe.Text = $"Lỗi: {ex.Message}";
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //// ✅ THÊM HỌC SINH VÀO LỚP
        //private void btnThemHocSinh_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (maHocKyHienTai <= 0)
        //        {
        //            MessageBox.Show("Vui lòng chọn học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        if (cbHocSinhChuaPhanLop.SelectedIndex <= 0)
        //        {
        //            MessageBox.Show("Vui lòng chọn học sinh cần thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        ComboBoxItem selectedItem = (ComboBoxItem)cbHocSinhChuaPhanLop.SelectedItem;
        //        int maHS = (int)selectedItem.Value;

        //        // Thêm học sinh vào lớp
        //        // Lấy thông tin học sinh để hiển thị
        //        HocSinhDTO hocSinh = danhSachHocSinhChuaPhanLopGoc.FirstOrDefault(h => h.MaHS == maHS);
        //        string tenHS = hocSinh?.HoTen ?? $"Mã {maHS}";

        //        if (phanLopBLL.AddPhanLop(maHS, maLop, maHocKyHienTai))
        //        {
        //            MessageBox.Show($"Đã thêm học sinh {tenHS} (Mã: {maHS}) vào lớp.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
        //            // Reload dữ liệu
        //            LoadDanhSachHocSinh();
        //            LoadHocSinhChuaPhanLop(); // Reload dropdown (sẽ tự động xóa học sinh đã thêm)
        //            LoadThongKe();
                    
        //            // Reset tìm kiếm và chọn lại item đầu tiên
        //            txtTimKiemHS.Text = "";
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không thể thêm học sinh. Có thể học sinh đã được phân lớp trong học kỳ này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // ✅ XỬ LÝ CLICK VÀO DATAGRIDVIEW HỌC SINH
        private void dgvHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int maHS = Convert.ToInt32(dgvHocSinh.Rows[e.RowIndex].Cells["MaHS"].Value);
                string tenHS = dgvHocSinh.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
                int maHocKy = (int)dgvHocSinh.Rows[e.RowIndex].Tag;

                // Xử lý nút "Chuyển lớp"
                if (e.ColumnIndex == dgvHocSinh.Columns["ChuyenLop"].Index)
                {
                    ChuyenLopHocSinh(maHS, tenHS, maHocKy);
                    return;
                }

                // Xử lý nút "Xóa"
                if (e.ColumnIndex == dgvHocSinh.Columns["Xoa"].Index)
                {
                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa học sinh {tenHS} (Mã: {maHS}) khỏi lớp này?", 
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (phanLopBLL.DeletePhanLop(maHS, maLop, maHocKy))
                        {
                            MessageBox.Show("Đã xóa học sinh khỏi lớp.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachHocSinh();
                            //LoadHocSinhChuaPhanLop(); // Reload dropdown
                            LoadThongKe();
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa học sinh khỏi lớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CHUYỂN LỚP HỌC SINH
        private void ChuyenLopHocSinh(int maHS, string tenHS, int maHocKy)
        {
            try
            {
                // Lấy tên lớp hiện tại
                var lopHienTai = lopHocBUS.LayLopTheoId(maLop);
                string tenLopCu = lopHienTai?.tenLop ?? $"Lớp {maLop}";

                // Mở form chọn lớp mới
                using (FormChuyenLop formChuyenLop = new FormChuyenLop(maHS, maLop, maHocKy, tenHS, tenLopCu))
                {
                    if (formChuyenLop.ShowDialog() == DialogResult.OK)
                    {
                        int maLopMoi = formChuyenLop.MaLopMoi;
                        string lyDo = formChuyenLop.LyDo;

                        // Thực hiện chuyển lớp
                        if (phanLopBLL.ChuyenLop(maHS, maLop, maLopMoi, maHocKy, lyDo, null))
                        {
                            // Lấy tên lớp mới
                            var lopMoi = lopHocBUS.LayLopTheoId(maLopMoi);
                            string tenLopMoi = lopMoi?.tenLop ?? $"Lớp {maLopMoi}";

                            MessageBox.Show($"Đã chuyển học sinh {tenHS} từ lớp {tenLopCu} sang lớp {tenLopMoi} thành công.", 
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reload dữ liệu
                            LoadDanhSachHocSinh();
                            //LoadHocSinhChuaPhanLop();
                            LoadThongKe();
                        }
                        else
                        {
                            MessageBox.Show("Không thể chuyển lớp. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chuyển lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelHocSinh_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        // ✅ XỬ LÝ FILTER GIỚI TÍNH
        private void rdoGender_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = sender as RadioButton;
            if (rdo == null || !rdo.Checked) return;

            if (rdo == rdoTatCa)
            {
                genderFilter = "all";
            }
            else if (rdo == rdoNam)
            {
                genderFilter = "nam";
            }
            else if (rdo == rdoNu)
            {
                genderFilter = "nu";
            }

            ApplyFilters();
        }

        // ✅ XUẤT EXCEL
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (danhSachHocSinhGoc == null || danhSachHocSinhGoc.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Yêu cầu bản quyền cho EPPlus 5 trở lên
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Xuất danh sách học sinh",
                    FileName = $"DanhSachLop_{maLop}_{DateTime.Now:yyyyMMdd}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportDanhSachHocSinhToExcel(saveDialog.FileName);
                    MessageBox.Show("Xuất file Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
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
                MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất danh sách học sinh ra file Excel
        /// </summary>
        private void ExportDanhSachHocSinhToExcel(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Xóa các sheet cũ nếu file đã tồn tại
                while (package.Workbook.Worksheets.Count > 0)
                {
                    package.Workbook.Worksheets.Delete(0);
                }

                var ws = package.Workbook.Worksheets.Add("DanhSachHocSinh");

                // Lấy thông tin lớp
                LopDTO lop = lopHocBUS.LayLopTheoId(maLop);
                string tenLop = lop?.tenLop ?? $"Lớp {maLop}";
                string tenHocKy = "N/A";
                string tenNamHoc = "N/A";
                
                if (maHocKyHienTai > 0)
                {
                    HocKyDTO hocKy = hocKyBUS.LayHocKyTheoMa(maHocKyHienTai);
                    if (hocKy != null)
                    {
                        tenHocKy = hocKy.TenHocKy;
                        if (!string.IsNullOrEmpty(hocKy.MaNamHoc))
                        {
                            NamHocDTO namHoc = namHocBUS.LayNamHocTheoMa(hocKy.MaNamHoc);
                            if (namHoc != null)
                            {
                                tenNamHoc = namHoc.TenNamHoc;
                            }
                        }
                    }
                }

                // === TIÊU ĐỀ BÁO CÁO ===
                ws.Cells[1, 1].Value = $"DANH SÁCH HỌC SINH LỚP {tenLop.ToUpper()}";
                ws.Cells[1, 1, 1, 7].Merge = true;
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.Font.Size = 16;
                ws.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                ws.Cells[1, 1].Style.Font.Color.SetColor(Color.White);
                ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Row(1).Height = 30;

                // Thông tin bổ sung
                ws.Cells[2, 1].Value = $"Học kỳ: {tenHocKy} | Năm học: {tenNamHoc} | Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                ws.Cells[2, 1, 2, 7].Merge = true;
                ws.Cells[2, 1].Style.Font.Italic = true;
                ws.Cells[2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Row(2).Height = 20;

                // === HEADER ===
                int headerRow = 3;
                ws.Cells[headerRow, 1].Value = "STT";
                ws.Cells[headerRow, 2].Value = "Mã HS";
                ws.Cells[headerRow, 3].Value = "Họ và tên";
                ws.Cells[headerRow, 4].Value = "Ngày sinh";
                ws.Cells[headerRow, 5].Value = "Giới tính";
                ws.Cells[headerRow, 6].Value = "SĐT";
                ws.Cells[headerRow, 7].Value = "Email";

                // Định dạng Header
                using (var range = ws.Cells[headerRow, 1, headerRow, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }
                ws.Row(headerRow).Height = 25;

                // === DỮ LIỆU ===
                int row = headerRow + 1;
                int stt = 1;
                foreach (var hs in danhSachHocSinhGoc)
                {
                    ws.Cells[row, 1].Value = stt;
                    ws.Cells[row, 2].Value = hs.MaHS;
                    ws.Cells[row, 3].Value = hs.HoTen;
                    ws.Cells[row, 4].Value = hs.NgaySinh.ToString("dd/MM/yyyy");
                    ws.Cells[row, 5].Value = hs.GioiTinh;
                    ws.Cells[row, 6].Value = hs.SdtHS ?? "";
                    ws.Cells[row, 7].Value = hs.Email ?? "";

                    // Định dạng màu cho Giới tính
                    if (hs.GioiTinh == "Nam")
                        ws.Cells[row, 5].Style.Font.Color.SetColor(Color.FromArgb(29, 78, 216));
                    else if (hs.GioiTinh == "Nữ")
                        ws.Cells[row, 5].Style.Font.Color.SetColor(Color.FromArgb(190, 24, 93));

                    // Thêm viền
                    using (var range = ws.Cells[row, 1, row, 7])
                    {
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }

                    row++;
                    stt++;
                }

                // === TỔNG KẾT ===
                int tongKetRow = row;
                ws.Cells[tongKetRow, 1].Value = "TỔNG CỘNG:";
                ws.Cells[tongKetRow, 2].Value = danhSachHocSinhGoc.Count;
                ws.Cells[tongKetRow, 1, tongKetRow, 2].Merge = true;
                ws.Cells[tongKetRow, 1].Style.Font.Bold = true;
                ws.Cells[tongKetRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[tongKetRow, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                ws.Cells[tongKetRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int soNam = danhSachHocSinhGoc.Count(h => h.GioiTinh == "Nam");
                int soNu = danhSachHocSinhGoc.Count(h => h.GioiTinh == "Nữ");
                ws.Cells[tongKetRow, 5].Value = $"Nam: {soNam} | Nữ: {soNu}";
                ws.Cells[tongKetRow, 5, tongKetRow, 7].Merge = true;
                ws.Cells[tongKetRow, 5].Style.Font.Bold = true;
                ws.Cells[tongKetRow, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[tongKetRow, 5].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                ws.Cells[tongKetRow, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Thêm viền cho dòng tổng kết
                using (var range = ws.Cells[tongKetRow, 1, tongKetRow, 7])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                // === TỰ ĐỘNG ĐIỀU CHỈNH ĐỘ RỘNG CỘT ===
                ws.Column(1).Width = 8;   // STT
                ws.Column(2).Width = 12;  // Mã HS
                ws.Column(3).Width = 30;   // Họ và tên
                ws.Column(4).Width = 15;   // Ngày sinh
                ws.Column(5).Width = 12;  // Giới tính
                ws.Column(6).Width = 15;   // SĐT
                ws.Column(7).Width = 30;   // Email

                // Căn giữa cột STT và Mã HS
                ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(4).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Lưu file
                package.Save();
            }
        }

        // ✅ IN DANH SÁCH
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (danhSachHocSinhGoc == null || danhSachHocSinhGoc.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Reset index về 0 khi bắt đầu in
                currentPrintIndex = 0;

                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += PrintDocument_PrintPage;

                PrintPreviewDialog previewDialog = new PrintPreviewDialog
                {
                    Document = printDoc,
                    WindowState = FormWindowState.Maximized
                };

                // Hiển thị preview trước khi in
                previewDialog.ShowDialog();
                
                // Reset lại sau khi đóng preview
                currentPrintIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentPrintIndex = 0; // Reset nếu có lỗi
            }
        }

        /// <summary>
        /// Xử lý sự kiện in trang
        /// </summary>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                // Lấy thông tin lớp
                LopDTO lop = lopHocBUS.LayLopTheoId(maLop);
                string tenLop = lop?.tenLop ?? $"Lớp {maLop}";
                string tenHocKy = "N/A";
                string tenNamHoc = "N/A";
                
                if (maHocKyHienTai > 0)
                {
                    HocKyDTO hocKy = hocKyBUS.LayHocKyTheoMa(maHocKyHienTai);
                    if (hocKy != null)
                    {
                        tenHocKy = hocKy.TenHocKy;
                        if (!string.IsNullOrEmpty(hocKy.MaNamHoc))
                        {
                            NamHocDTO namHoc = namHocBUS.LayNamHocTheoMa(hocKy.MaNamHoc);
                            if (namHoc != null)
                            {
                                tenNamHoc = namHoc.TenNamHoc;
                            }
                        }
                    }
                }

                Graphics g = e.Graphics;
                Font titleFont = new Font("Arial", 18, FontStyle.Bold);
                Font headerFont = new Font("Arial", 11, FontStyle.Bold);
                Font normalFont = new Font("Arial", 10, FontStyle.Regular);
                Font infoFont = new Font("Arial", 9, FontStyle.Italic);
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black, 1);

                float yPos = e.MarginBounds.Top;
                float leftMargin = e.MarginBounds.Left;
                float rightMargin = e.MarginBounds.Right;
                float pageWidth = e.MarginBounds.Width;

                // === TIÊU ĐỀ ===
                string title = $"DANH SÁCH HỌC SINH LỚP {tenLop.ToUpper()}";
                SizeF titleSize = g.MeasureString(title, titleFont);
                float titleX = leftMargin + (pageWidth - titleSize.Width) / 2;
                g.DrawString(title, titleFont, brush, titleX, yPos);
                yPos += titleSize.Height + 10;

                // === THÔNG TIN BỔ SUNG ===
                string info = $"Học kỳ: {tenHocKy} | Năm học: {tenNamHoc} | Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                SizeF infoSize = g.MeasureString(info, infoFont);
                float infoX = leftMargin + (pageWidth - infoSize.Width) / 2;
                g.DrawString(info, infoFont, brush, infoX, yPos);
                yPos += infoSize.Height + 15;

                // === HEADER BẢNG ===
                float headerY = yPos;
                float col1 = leftMargin;                    // STT
                float col2 = col1 + 35;                     // Mã HS
                float col3 = col2 + 70;                     // Họ tên
                float col4 = col3 + 150;                    // Ngày sinh
                float col5 = col4 + 80;                     // Giới tính
                float col6 = col5 + 70;                     // SĐT
                float col7 = col6 + 95;                     // Email
                float rowHeight = 25;

                // Vẽ header background
                RectangleF headerRect = new RectangleF(leftMargin, headerY, pageWidth, rowHeight);
                g.FillRectangle(new SolidBrush(Color.FromArgb(79, 129, 189)), headerRect);

                // Vẽ text header
                g.DrawString("STT", headerFont, Brushes.White, col1 + 5, headerY + 5);
                g.DrawString("Mã HS", headerFont, Brushes.White, col2 + 5, headerY + 5);
                g.DrawString("Họ và tên", headerFont, Brushes.White, col3 + 5, headerY + 5);
                g.DrawString("Ngày sinh", headerFont, Brushes.White, col4 + 5, headerY + 5);
                g.DrawString("Giới tính", headerFont, Brushes.White, col5 + 5, headerY + 5);
                g.DrawString("SĐT", headerFont, Brushes.White, col6 + 5, headerY + 5);
                g.DrawString("Email", headerFont, Brushes.White, col7 + 5, headerY + 5);

                // Vẽ viền header
                g.DrawRectangle(pen, headerRect.X, headerRect.Y, headerRect.Width, headerRect.Height);

                yPos += rowHeight;

                // === DỮ LIỆU ===
                // Chỉ in từ vị trí currentPrintIndex trở đi (tránh vòng lặp vô hạn)
                int stt = currentPrintIndex + 1;
                float maxY = e.MarginBounds.Bottom - 50; // Để chỗ cho tổng kết
                bool isLastPage = false;

                // In từ vị trí hiện tại
                for (int i = currentPrintIndex; i < danhSachHocSinhGoc.Count; i++)
                {
                    // Kiểm tra nếu hết chỗ trên trang này
                    if (yPos + rowHeight > maxY)
                    {
                        // Cập nhật index để trang sau tiếp tục từ đây
                        currentPrintIndex = i;
                        e.HasMorePages = true;
                        return;
                    }

                    var hs = danhSachHocSinhGoc[i];

                    // Vẽ viền dòng
                    RectangleF rowRect = new RectangleF(leftMargin, yPos, pageWidth, rowHeight);
                    g.DrawRectangle(pen, rowRect.X, rowRect.Y, rowRect.Width, rowRect.Height);

                    // Vẽ dữ liệu
                    g.DrawString(stt.ToString(), normalFont, brush, col1 + 5, yPos + 5);
                    g.DrawString(hs.MaHS.ToString(), normalFont, brush, col2 + 5, yPos + 5);
                    g.DrawString(hs.HoTen ?? "", normalFont, brush, col3 + 5, yPos + 5);
                    g.DrawString(hs.NgaySinh.ToString("dd/MM/yyyy"), normalFont, brush, col4 + 5, yPos + 5);
                    
                    // Màu cho giới tính
                    Brush genderBrush = brush;
                    if (hs.GioiTinh == "Nam")
                        genderBrush = new SolidBrush(Color.FromArgb(29, 78, 216));
                    else if (hs.GioiTinh == "Nữ")
                        genderBrush = new SolidBrush(Color.FromArgb(190, 24, 93));
                    
                    g.DrawString(hs.GioiTinh ?? "", normalFont, genderBrush, col5 + 5, yPos + 5);
                    g.DrawString(hs.SdtHS ?? "", normalFont, brush, col6 + 5, yPos + 5);
                    
                    // Cắt email nếu quá dài để vừa trong cột
                    string email = hs.Email ?? "";
                    float emailColWidth = rightMargin - col7 - 10; // Tính độ rộng còn lại
                    SizeF emailSize = g.MeasureString(email, normalFont);
                    
                    // Nếu email quá dài, cắt bớt
                    if (emailSize.Width > emailColWidth && email.Length > 0)
                    {
                        // Cắt dần cho đến khi vừa
                        while (emailSize.Width > emailColWidth && email.Length > 3)
                        {
                            email = email.Substring(0, email.Length - 1);
                            emailSize = g.MeasureString(email + "...", normalFont);
                        }
                        email = email + "...";
                    }
                    g.DrawString(email, normalFont, brush, col7 + 5, yPos + 5);

                    yPos += rowHeight;
                    stt++;
                    currentPrintIndex = i + 1; // Cập nhật index

                    // Nếu đã in hết, đánh dấu là trang cuối
                    if (i == danhSachHocSinhGoc.Count - 1)
                    {
                        isLastPage = true;
                    }
                }

                // === TỔNG KẾT (chỉ hiển thị ở trang cuối) ===
                if (isLastPage)
                {
                    yPos += 10;
                    float totalY = yPos;
                    int soNam = danhSachHocSinhGoc.Count(h => h.GioiTinh == "Nam");
                    int soNu = danhSachHocSinhGoc.Count(h => h.GioiTinh == "Nữ");

                    // Kiểm tra xem còn chỗ cho tổng kết không
                    if (totalY + rowHeight <= e.MarginBounds.Bottom)
                    {
                        // Vẽ background tổng kết
                        RectangleF totalRect = new RectangleF(leftMargin, totalY, pageWidth, rowHeight);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), totalRect);

                        string totalText = $"TỔNG CỘNG: {danhSachHocSinhGoc.Count} học sinh (Nam: {soNam} | Nữ: {soNu})";
                        g.DrawString(totalText, headerFont, brush, col1 + 5, totalY + 5);
                        g.DrawRectangle(pen, totalRect.X, totalRect.Y, totalRect.Width, totalRect.Height);
                    }
                }

                // Đánh dấu không còn trang nào nữa
                e.HasMorePages = false;
                currentPrintIndex = 0; // Reset sau khi in xong
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ LÀM MỚI DỮ LIỆU
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachHocSinh();
                //LoadHocSinhChuaPhanLop();
                //LoadThoiKhoaBieu();
                LoadThongKe();
                txtTimKiemHS.Text = "";
                rdoTatCa.Checked = true;
                MessageBox.Show("Đã làm mới dữ liệu!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblThongBaoHS_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 🆕 Thêm button "Gửi yêu cầu chuyển lớp" CHỈ cho PHỤ HUYNH
        /// ✅ Admin KHÔNG được phép gửi yêu cầu - chỉ quản lý và duyệt
        /// </summary>
        private void ThemButtonGuiYeuCauChuyenLop()
        {
            try
            {
                // Tạo button mới
                Guna.UI2.WinForms.Guna2Button btnGuiYeuCau = new Guna.UI2.WinForms.Guna2Button();
                btnGuiYeuCau.Text = "📤 Gửi yêu cầu chuyển lớp";
                btnGuiYeuCau.Size = new Size(220, 38);
                btnGuiYeuCau.FillColor = Color.FromArgb(34, 197, 94); // Màu xanh lá
                btnGuiYeuCau.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                btnGuiYeuCau.ForeColor = Color.White;
                btnGuiYeuCau.BorderRadius = 8;
                btnGuiYeuCau.Cursor = Cursors.Hand;

                // ✅ Đặt vị trí button trong panelThongTin (header màu xanh), góc phải trên
                if (panelThongTin != null)
                {
                    // Đặt ở góc phải trên của panelThongTin
                    btnGuiYeuCau.Location = new Point(panelThongTin.Width - 230, 8);
                }
                else
                {
                    btnGuiYeuCau.Location = new Point(800, 25);
                }

                // Gắn sự kiện click
                btnGuiYeuCau.Click += BtnGuiYeuCau_Click;

                // ✅ Thêm button vào panelThongTin thay vì form
                if (panelThongTin != null)
                {
                    panelThongTin.Controls.Add(btnGuiYeuCau);
                    btnGuiYeuCau.BringToFront();
                }
                else
                {
                    this.Controls.Add(btnGuiYeuCau);
                    btnGuiYeuCau.BringToFront();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm button Gửi yêu cầu: {ex.Message}");
            }
        }

        /// <summary>
        /// 🆕 Event khi click button "Gửi yêu cầu chuyển lớp"
        /// </summary>
        private void BtnGuiYeuCau_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn học sinh chưa
                if (dgvHocSinh.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn học sinh để gửi yêu cầu chuyển lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra đã chọn học kỳ chưa
                if (maHocKyHienTai == 0)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin học sinh đã chọn
                var row = dgvHocSinh.SelectedRows[0];
                int maHocSinh = Convert.ToInt32(row.Cells["MaHS"].Value);
                string tenHocSinh = row.Cells["HoTen"].Value.ToString();

                // Lấy thông tin lớp hiện tại
                var lopHienTai = lopHocBUS.LayLopTheoId(maLop);
                string tenLopHienTai = lopHienTai?.tenLop ?? "N/A";

                // Lấy tên đăng nhập người tạo từ SessionManager
                string tenDangNhapNguoiTao = SessionManager.TenDangNhap;

                if (string.IsNullOrEmpty(tenDangNhapNguoiTao))
                {
                    MessageBox.Show("Không xác định được người dùng hiện tại.\n\nVui lòng đăng nhập lại.", 
                        "Lỗi", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    return;
                }

                // Mở form gửi yêu cầu chuyển lớp
                FormGuiYeuCauChuyenLop form = new FormGuiYeuCauChuyenLop(
                    maHocSinh, 
                    maLop, 
                    maHocKyHienTai, 
                    tenHocSinh, 
                    tenLopHienTai, 
                    tenDangNhapNguoiTao
                );

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Reload dữ liệu sau khi gửi yêu cầu thành công
                    LoadDanhSachHocSinh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form gửi yêu cầu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 🆕 Thêm button "Quản lý yêu cầu chuyển lớp" cho ADMIN
        /// </summary>
        private void ThemButtonQuanLyYeuCauChuyenLop()
        {
            try
            {
                // ✅ CHỈ HIỂN THỊ CHO ADMIN (có quyền QLLOPHOC hoặc QLYEUCAUCHUYENLOP)
                // Nếu không có quyền QLYEUCAUCHUYENLOP, thử dùng QLLOPHOC (vì quản lý yêu cầu là phần của quản lý lớp)
                bool coQuyen = PermissionHelper.HasAccessToFunction(PermissionHelper.QLYEUCAUCHUYENLOP) ||
                               PermissionHelper.HasAccessToFunction(PermissionHelper.QLLOPHOC);
                
                if (!coQuyen)
                {
                    return; // Không hiển thị button cho người không có quyền
                }

                // Tạo button mới
                Guna.UI2.WinForms.Guna2Button btnQuanLyYeuCau = new Guna.UI2.WinForms.Guna2Button();
                btnQuanLyYeuCau.Text = "📋 Quản lý yêu cầu chuyển lớp";
                btnQuanLyYeuCau.Size = new Size(250, 38);
                btnQuanLyYeuCau.FillColor = Color.FromArgb(100, 88, 255); // Màu tím
                btnQuanLyYeuCau.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                btnQuanLyYeuCau.ForeColor = Color.White;
                btnQuanLyYeuCau.BorderRadius = 8;
                btnQuanLyYeuCau.Cursor = Cursors.Hand;

                // ✅ Đặt vị trí button trong panelThongTin (header màu xanh), bên trái button "Gửi yêu cầu"
                if (panelThongTin != null)
                {
                    // Đặt bên trái button "Gửi yêu cầu" (cách 10px)
                    btnQuanLyYeuCau.Location = new Point(panelThongTin.Width - 490, 8);
                }
                else
                {
                    btnQuanLyYeuCau.Location = new Point(600, 25);
                }

                // Gắn sự kiện click
                btnQuanLyYeuCau.Click += BtnQuanLyYeuCau_Click;

                // ✅ Thêm button vào panelThongTin thay vì form
                if (panelThongTin != null)
                {
                    panelThongTin.Controls.Add(btnQuanLyYeuCau);
                    btnQuanLyYeuCau.BringToFront();
                }
                else
                {
                    this.Controls.Add(btnQuanLyYeuCau);
                    btnQuanLyYeuCau.BringToFront();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm button Quản lý yêu cầu: {ex.Message}");
            }
        }

        /// <summary>
        /// 🆕 Event khi click button "Quản lý yêu cầu chuyển lớp"
        /// </summary>
        private void BtnQuanLyYeuCau_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tên đăng nhập admin từ SessionManager
                string tenDangNhapAdmin = SessionManager.TenDangNhap ?? "admin";

                // Mở form quản lý yêu cầu chuyển lớp
                FormQuanLyYeuCauChuyenLop form = new FormQuanLyYeuCauChuyenLop(tenDangNhapAdmin);
                form.ShowDialog();

                // Sau khi đóng form, có thể reload dữ liệu nếu cần
                LoadDanhSachHocSinh();
                LoadThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý yêu cầu: {ex.Message}", 
                    "Lỗi", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }
    }
}

