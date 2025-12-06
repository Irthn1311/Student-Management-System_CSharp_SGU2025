using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI;
using Student_Management_System_CSharp_SGU2025.GUI.NamHoc;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ucNamHoc : UserControl
    {
        private NamHocBUS namHocBUS;
        private HocKyBUS hocKyBUS; // ✅ THÊM BUS HỌC KỲ

        public ucNamHoc()
        {
            InitializeComponent();
            namHocBUS = new NamHocBUS();
            hocKyBUS = new HocKyBUS(); // ✅ KHỞI TẠO BUS HỌC KỲ
        }

        private void ucNamHoc_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ KIỂM TRA QUYỀN TRUY CẬP
                if (!PermissionHelper.CheckAccessPermission(PermissionHelper.QLNAMHOC, "Quản lý năm học"))
                {
                    this.Enabled = false;
                    return;
                }
                SetupCardNH();
                SetupTbNamHoc();
                InitializeDefaultView();
                SetupTbHocKy();
                SetupCardHK();

                // ✅ ÁP DỤNG PHÂN QUYỀN
                PermissionHelper.ApplyPermissionNamHoc(
                    btnAddNamHoc,
                    btnAddHocKy,
                    tbNamHoc,
                    tbHocKy
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải trang Năm học:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupCardNH()
        {
            try
            {
                List<NamHocDTO> danhSachNamHoc = namHocBUS.DocDSNamHoc();
                int tongNamHoc = danhSachNamHoc != null ? danhSachNamHoc.Count : 0;

                NamHocDTO namHocHienTai = danhSachNamHoc?.FirstOrDefault(nh =>
                    TinhTrangThai(nh.NgayBD, nh.NgayKT) == "Đang diễn ra");

                // ✅ LẤY HỌC KỲ HIỆN TẠI TỪ DATABASE
                List<HocKyDTO> danhSachHocKy = hocKyBUS.DocDSHocKy();
                HocKyDTO hocKyHienTai = danhSachHocKy?.FirstOrDefault(hk =>
                    hk.NgayBD.HasValue && hk.NgayKT.HasValue && 
                    TinhTrangThai(hk.NgayBD.Value, hk.NgayKT.Value) == "Đang diễn ra");

                // CARD 1 - Năm học hiện tại
                if (namHocHienTai != null)
                {
                    statCardNH1.SetData("Năm học hiện tại", namHocHienTai.TenNamHoc, "Đang diễn ra");
                }
                else
                {
                    statCardNH1.SetData("Năm học hiện tại", "Chưa có", "Không có năm học đang diễn ra");
                }

                statCardNH1.PanelColor = ColorTranslator.FromHtml("#20bf59");
                statCardNH1.TextColor = Color.White;

                var lblTen1 = statCardNH1.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
                var lblSo1 = statCardNH1.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
                var lblDesc1 = statCardNH1.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

                if (lblTen1 != null) lblTen1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                if (lblSo1 != null) lblSo1.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                if (lblDesc1 != null) lblDesc1.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                var panelMain1 = statCardNH1.Controls["panelMain"] as Guna2Panel;
                if (panelMain1 != null) panelMain1.BorderRadius = 15;

                // ✅ CARD 2 - HỌC KỲ HIỆN TẠI (ĐỒNG BỘ VỚI DATABASE)
                if (hocKyHienTai != null)
                {
                    string thoiGian = $"{hocKyHienTai.NgayBD:dd/MM} - {hocKyHienTai.NgayKT:dd/MM/yyyy}";
                    statCardNH2.SetData("Học kỳ", hocKyHienTai.TenHocKy, thoiGian);
                }
                else
                {
                    statCardNH2.SetData("Học kỳ", "Chưa có", "Không có học kỳ đang diễn ra");
                }

                statCardNH2.PanelColor = ColorTranslator.FromHtml("#3781f4");
                statCardNH2.TextColor = Color.White;

                var lblTen2 = statCardNH2.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
                var lblSo2 = statCardNH2.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
                var lblDesc2 = statCardNH2.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

                if (lblTen2 != null) lblTen2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                if (lblSo2 != null) lblSo2.Font = new Font("Segoe UI", 20, FontStyle.Bold); if (lblDesc2 != null)
                    if (lblDesc2 != null) lblDesc2.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                var panelMain2 = statCardNH2.Controls["panelMain"] as Guna2Panel;
                if (panelMain2 != null) panelMain2.BorderRadius = 15;

                // CARD 3 - Tổng năm học
                statCardNH3.SetData("Tổng năm học", tongNamHoc.ToString(), "Trong hệ thống");
                statCardNH3.PanelColor = ColorTranslator.FromHtml("#f66f14");
                statCardNH3.TextColor = Color.White;

                var lblTen3 = statCardNH3.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
                var lblSo3 = statCardNH3.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
                var lblDesc3 = statCardNH3.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

                if (lblTen3 != null) lblTen3.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                if (lblSo3 != null) lblSo3.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                if (lblDesc3 != null) lblDesc3.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                var panelMain3 = statCardNH3.Controls["panelMain"] as Guna2Panel;
                if (panelMain3 != null) panelMain3.BorderRadius = 15;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupCardHK()
        {
            try
            {
                List<HocKyDTO> danhSachHocKy = hocKyBUS.DocDSHocKy();
                int tongHocKy = danhSachHocKy != null ? danhSachHocKy.Count : 0;

                HocKyDTO hocKyHienTai = danhSachHocKy?.FirstOrDefault(hk =>
                    hk.NgayBD.HasValue && hk.NgayKT.HasValue && 
                    TinhTrangThai(hk.NgayBD.Value, hk.NgayKT.Value) == "Đang diễn ra");

                // CARD 1 - Học kỳ hiện tại
                if (hocKyHienTai != null)
                {
                    NamHocDTO namHoc = namHocBUS.LayNamHocTheoMa(hocKyHienTai.MaNamHoc);
                    string tenNamHoc = namHoc != null ? namHoc.TenNamHoc : hocKyHienTai.MaNamHoc;

                    statCardHK1.SetData("Học kỳ hiện tại", hocKyHienTai.TenHocKy, tenNamHoc);
                }
                else
                {
                    statCardHK1.SetData("Học kỳ hiện tại", "Chưa có", "Không có học kỳ đang diễn ra");
                }

                statCardHK1.PanelColor = ColorTranslator.FromHtml("#357ef1");
                statCardHK1.TextColor = Color.White;

                var lblTen4 = statCardHK1.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
                var lblSo4 = statCardHK1.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
                var lblDesc4 = statCardHK1.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

                if (lblTen4 != null) lblTen4.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                if (lblSo4 != null) lblSo4.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                if (lblDesc4 != null) lblDesc4.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                var panelMain4 = statCardHK1.Controls["panelMain"] as Guna2Panel;
                if (panelMain4 != null) panelMain4.BorderRadius = 15;

                // CARD 2 - Thời gian
                if (hocKyHienTai != null && hocKyHienTai.NgayBD.HasValue && hocKyHienTai.NgayKT.HasValue)
                {
                    TimeSpan duration = hocKyHienTai.NgayKT.Value - hocKyHienTai.NgayBD.Value;
                    int soThang = (int)(duration.TotalDays / 30);
                    string thoiGian = $"{hocKyHienTai.NgayBD.Value:dd/MM} - {hocKyHienTai.NgayKT.Value:dd/MM/yyyy}";

                    statCardHK2.SetData("Thời gian", $"{soThang} tháng", thoiGian);
                }
                else
                {
                    statCardHK2.SetData("Thời gian", "N/A", "Chưa có học kỳ");
                }

                statCardHK2.PanelColor = ColorTranslator.FromHtml("#1eba57");
                statCardHK2.TextColor = Color.White;

                var lblTen5 = statCardHK2.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
                var lblSo5 = statCardHK2.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
                var lblDesc5 = statCardHK2.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

                if (lblTen5 != null) lblTen5.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                if (lblSo5 != null) lblSo5.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                if (lblDesc5 != null) lblDesc5.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                var panelMain5 = statCardHK2.Controls["panelMain"] as Guna2Panel;
                if (panelMain5 != null) panelMain5.BorderRadius = 15;

                // CARD 3 - Tổng học kỳ
                statCardHK3.SetData("Tổng học kỳ", tongHocKy.ToString(), "Trong hệ thống");
                statCardHK3.PanelColor = ColorTranslator.FromHtml("#a44ef6");
                statCardHK3.TextColor = Color.White;

                var lblTen6 = statCardHK3.Controls.Find("lblTenKhoi", true).FirstOrDefault() as Label;
                var lblSo6 = statCardHK3.Controls.Find("lblSoLop", true).FirstOrDefault() as Label;
                var lblDesc6 = statCardHK3.Controls.Find("lblSoHocSinh", true).FirstOrDefault() as Label;

                if (lblTen6 != null) lblTen6.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                if (lblSo6 != null) lblSo6.Font = new Font("Segoe UI", 20, FontStyle.Bold);
                if (lblDesc6 != null) lblDesc6.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                var panelMain6 = statCardHK3.Controls["panelMain"] as Guna2Panel;
                if (panelMain6 != null) panelMain6.BorderRadius = 15;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê học kỳ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDefaultView()
        {
            tbNamHoc.Visible = true;
            tbHocKy.Visible = false;

            btnAddNamHoc.Visible = true;
            btnAddHocKy.Visible = false;

            statCardNH1.Visible = true;
            statCardNH2.Visible = true;
            statCardNH3.Visible = true;
            statCardHK1.Visible = false;
            statCardHK2.Visible = false;
            statCardHK3.Visible = false;

            btnNamHoc.FillColor = Color.FromArgb(30, 136, 229);
            btnNamHoc.ForeColor = Color.White;

            btnHocKy.FillColor = Color.White;
            btnHocKy.ForeColor = Color.Black;
        }

        private void SetupTbNamHoc()
        {
            try
            {
                tbNamHoc.Rows.Clear();
                tbNamHoc.Columns.Clear();

                tbNamHoc.Columns.Add("maNamHoc", "Mã năm học");
                tbNamHoc.Columns.Add("namHoc", "Tên năm học");
                tbNamHoc.Columns.Add("ngayBatDau", "Ngày bắt đầu");
                tbNamHoc.Columns.Add("ngayKetThuc", "Ngày kết thúc");
                tbNamHoc.Columns.Add("trangThai", "Trạng thái");
                tbNamHoc.Columns.Add("thaoTac", "Thao tác");

                tbNamHoc.RowTemplate.Height = 48;

                List<NamHocDTO> danhSachNamHoc = namHocBUS.DocDSNamHoc();

                if (danhSachNamHoc != null && danhSachNamHoc.Count > 0)
                {
                    foreach (NamHocDTO namHocDTO in danhSachNamHoc)
                    {
                        string trangThai = TinhTrangThai(namHocDTO.NgayBD, namHocDTO.NgayKT);

                        tbNamHoc.Rows.Add(
                            namHocDTO.MaNamHoc,
                            namHocDTO.TenNamHoc,
                            namHocDTO.NgayBD.ToString("dd/MM/yyyy"),
                            namHocDTO.NgayKT.ToString("dd/MM/yyyy"),
                            trangThai,
                            ""
                        );
                    }
                }

                tbNamHoc.EnableHeadersVisualStyles = false;
                tbNamHoc.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
                tbNamHoc.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
                tbNamHoc.BackgroundColor = Color.White;
                tbNamHoc.BorderStyle = BorderStyle.None;
                tbNamHoc.GridColor = Color.FromArgb(240, 240, 240);

                tbNamHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                tbNamHoc.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                tbNamHoc.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
                tbNamHoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
                tbNamHoc.DefaultCellStyle.SelectionForeColor = Color.Black;
                tbNamHoc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                tbNamHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                tbNamHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
                tbNamHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
                tbNamHoc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                tbNamHoc.ColumnHeadersHeight = 50;
                tbNamHoc.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                tbNamHoc.DefaultCellStyle.Padding = new Padding(17, 0, 0, 0);

                foreach (DataGridViewRow row in tbNamHoc.Rows)
                {
                    string status = row.Cells["trangThai"].Value?.ToString();
                    if (status == "Đang diễn ra")
                    {
                        row.Cells["trangThai"].Style.BackColor = Color.FromArgb(205, 255, 230);
                        row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(40, 150, 70);
                    }
                    else if (status == "Chưa bắt đầu")
                    {
                        row.Cells["trangThai"].Style.BackColor = Color.FromArgb(230, 237, 255);
                        row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(20, 100, 200);
                    }
                    else
                    {
                        row.Cells["trangThai"].Style.BackColor = Color.FromArgb(240, 240, 240);
                        row.Cells["trangThai"].Style.ForeColor = Color.FromArgb(100, 100, 100);
                    }

                    row.Cells["trangThai"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    row.Cells["trangThai"].Style.SelectionBackColor = row.Cells["trangThai"].Style.BackColor;
                }

                tbNamHoc.AllowUserToAddRows = false;
                tbNamHoc.ReadOnly = true;
                tbNamHoc.AllowUserToDeleteRows = false;
                tbNamHoc.AllowUserToResizeColumns = false;
                tbNamHoc.AllowUserToResizeRows = false;
                tbNamHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tbNamHoc.MultiSelect = false;

                tbNamHoc.CellPainting += tbNamHoc_CellPainting;
                tbNamHoc.CellClick += tbNamHoc_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách năm học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TinhTrangThai(DateTime ngayBD, DateTime ngayKT)
        {
            DateTime now = DateTime.Now.Date;
            DateTime batDau = ngayBD.Date;
            DateTime ketThuc = ngayKT.Date;

            if (now >= batDau && now <= ketThuc)
                return "Đang diễn ra";
            else if (now < batDau)
                return "Chưa bắt đầu";
            else
                return "Đã kết thúc";
        }

        private void SetupTbHocKy()
        {
            try
            {
                tbHocKy.Rows.Clear();
                tbHocKy.Columns.Clear();

                // Thêm cột mã học kỳ (ẩn)
                tbHocKy.Columns.Add("maHocKy", "Mã học kỳ");
                tbHocKy.Columns["maHocKy"].Visible = false;

                tbHocKy.Columns.Add("namHocHK", "Năm học");
                tbHocKy.Columns.Add("hocKy", "Học kỳ");
                tbHocKy.Columns.Add("ngayBatDauHK", "Ngày bắt đầu");
                tbHocKy.Columns.Add("ngayKetThucHK", "Ngày kết thúc");
                tbHocKy.Columns.Add("trangThaiHK", "Trạng thái");
                tbHocKy.Columns.Add("thaoTacHK", "Thao tác");

                tbHocKy.RowTemplate.Height = 48;

                // ✅ LẤY DỮ LIỆU TỪ DATABASE
                List<HocKyDTO> danhSachHocKy = hocKyBUS.DocDSHocKy();

                if (danhSachHocKy != null && danhSachHocKy.Count > 0)
                {
                    foreach (HocKyDTO hocKyDTO in danhSachHocKy)
                    {
                        // Lấy tên năm học từ mã năm học
                        NamHocDTO namHoc = namHocBUS.LayNamHocTheoMa(hocKyDTO.MaNamHoc);
                        string tenNamHoc = namHoc != null ? namHoc.TenNamHoc : hocKyDTO.MaNamHoc;

                        string trangThai = "";
                        if (hocKyDTO.NgayBD.HasValue && hocKyDTO.NgayKT.HasValue)
                        {
                            trangThai = TinhTrangThai(hocKyDTO.NgayBD.Value, hocKyDTO.NgayKT.Value);
                        }
                        else
                        {
                            trangThai = "Chưa xác định";
                        }

                        tbHocKy.Rows.Add(
                            hocKyDTO.MaHocKy,
                            tenNamHoc,
                            hocKyDTO.TenHocKy,
                            hocKyDTO.NgayBD?.ToString("dd/MM/yyyy") ?? "N/A",
                            hocKyDTO.NgayKT?.ToString("dd/MM/yyyy") ?? "N/A",
                            trangThai,
                            ""
                        );
                    }
                }

                // Styling giữ nguyên như cũ
                tbHocKy.EnableHeadersVisualStyles = false;
                tbHocKy.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
                tbHocKy.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
                tbHocKy.BackgroundColor = Color.White;
                tbHocKy.BorderStyle = BorderStyle.None;
                tbHocKy.GridColor = Color.FromArgb(240, 240, 240);

                tbHocKy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                tbHocKy.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                tbHocKy.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
                tbHocKy.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
                tbHocKy.DefaultCellStyle.SelectionForeColor = Color.Black;
                tbHocKy.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                tbHocKy.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                tbHocKy.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
                tbHocKy.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
                tbHocKy.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                tbHocKy.ColumnHeadersHeight = 50;
                tbHocKy.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                tbHocKy.DefaultCellStyle.Padding = new Padding(17, 0, 0, 0);

                foreach (DataGridViewRow row in tbHocKy.Rows)
                {
                    string status = row.Cells["trangThaiHK"].Value?.ToString();
                    if (status == "Đang diễn ra")
                    {
                        row.Cells["trangThaiHK"].Style.BackColor = Color.FromArgb(205, 255, 230);
                        row.Cells["trangThaiHK"].Style.ForeColor = Color.FromArgb(40, 150, 70);
                    }
                    else if (status == "Chưa bắt đầu")
                    {
                        row.Cells["trangThaiHK"].Style.BackColor = Color.FromArgb(230, 237, 255);
                        row.Cells["trangThaiHK"].Style.ForeColor = Color.FromArgb(20, 100, 200);
                    }
                    else
                    {
                        row.Cells["trangThaiHK"].Style.BackColor = Color.FromArgb(240, 240, 240);
                        row.Cells["trangThaiHK"].Style.ForeColor = Color.FromArgb(100, 100, 100);
                    }

                    row.Cells["trangThaiHK"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    row.Cells["trangThaiHK"].Style.SelectionBackColor = row.Cells["trangThaiHK"].Style.BackColor;
                }

                tbHocKy.AllowUserToAddRows = false;
                tbHocKy.ReadOnly = true;
                tbHocKy.AllowUserToDeleteRows = false;
                tbHocKy.AllowUserToResizeColumns = false;
                tbHocKy.AllowUserToResizeRows = false;
                tbHocKy.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tbHocKy.MultiSelect = false;

                tbHocKy.CellPainting += tbHocKy_CellPainting;
                tbHocKy.CellClick += tbHocKy_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách học kỳ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbNamHoc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbNamHoc.Columns["thaoTac"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                // ✅ Lấy permission từ Tag - Sử dụng cách an toàn hơn
                bool canDelete = true; // Mặc định true
                
                if (tbNamHoc.Tag != null)
                {
                    try
                    {
                        dynamic permissions = tbNamHoc.Tag;
                        canDelete = permissions?.CanDelete ?? true;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        // Nếu Tag không có thuộc tính CanDelete, giữ giá trị mặc định
                        canDelete = true;
                    }
                }

                int iconSize = 22;
                int iconEyeSize = 32;
                int padding = 6;

                int xEdit = e.CellBounds.Left + padding;
                int xDelete = xEdit + iconEyeSize + 4 * padding;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconEyeSize) / 2;
                int yDelete = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                try
                {
                    Image editIcon = Image.FromFile(@"..\..\Images\icon_eye.png");
                    Image deleteIcon = Image.FromFile(@"..\..\Images\deleteicon.png");

                    // ✅ Luôn vẽ icon Xem (không phụ thuộc permission)
                    e.Graphics.DrawImage(editIcon, new Rectangle(xEdit, y, iconEyeSize, iconEyeSize));

                    // ✅ Vẽ icon Xóa với độ mờ nếu không có quyền
                    if (canDelete)
                    {
                        e.Graphics.DrawImage(deleteIcon, new Rectangle(xDelete, yDelete, iconSize, iconSize));
                    }
                    else
                    {
                        var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
                            new float[][] {
                        new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                        new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                        new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                        new float[] {0, 0, 0, 0.3f, 0},
                        new float[] {0, 0, 0, 0, 1}
                            });
                        using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                        {
                            attributes.SetColorMatrix(grayScaleMatrix);
                            e.Graphics.DrawImage(deleteIcon, new Rectangle(xDelete, yDelete, iconSize, iconSize),
                                0, 0, deleteIcon.Width, deleteIcon.Height,
                                GraphicsUnit.Pixel, attributes);
                        }
                    }
                }
                catch { }

                e.Handled = true;
            }
        }

        private void tbNamHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbNamHoc.Columns["thaoTac"].Index)
            {
                var cell = tbNamHoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = tbNamHoc.PointToClient(Cursor.Position).X - cell.X;

                int iconSize = 22;
                int iconEyeSize = 32;
                int padding = 6;

                int eyeRight = padding + iconEyeSize;
                int deleteLeft = eyeRight + 4 * padding;
                int deleteRight = deleteLeft + iconSize;

                string maNamHoc = tbNamHoc.Rows[e.RowIndex].Cells["maNamHoc"].Value?.ToString();
                string namHoc = tbNamHoc.Rows[e.RowIndex].Cells["namHoc"].Value?.ToString();

                if (x < eyeRight)
                {
                    // XEM CHI TIẾT
                    XemChiTietNamHoc(maNamHoc, namHoc);
                }
                else if (x > deleteLeft && x < deleteRight)
                {
                    // ✅ KIỂM TRA QUYỀN XÓA
                    if (!PermissionHelper.CheckDeletePermission(PermissionHelper.QLNAMHOC, "Quản lý năm học"))
                        return;

                    // XÓA NĂM HỌC
                    XoaNamHoc(maNamHoc, namHoc, e.RowIndex);
                }
            }
        }

        private void XemChiTietNamHoc(string maNamHoc, string tenNamHoc)
        {
            try
            {
                NamHocDTO namHoc = namHocBUS.LayNamHocTheoMa(maNamHoc);

                if (namHoc != null)
                {
                    string thongTin = $"📚 THÔNG TIN NĂM HỌC\n\n" +
                                    $"🔑 Mã năm học: {namHoc.MaNamHoc}\n" +
                                    $"📝 Tên năm học: {namHoc.TenNamHoc}\n" +
                                    $"📅 Ngày bắt đầu: {namHoc.NgayBD:dd/MM/yyyy}\n" +
                                    $"📅 Ngày kết thúc: {namHoc.NgayKT:dd/MM/yyyy}\n" +
                                    $"🔄 Trạng thái: {TinhTrangThai(namHoc.NgayBD, namHoc.NgayKT)}";

                    MessageBox.Show(thongTin, "Chi tiết năm học",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin năm học!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaNamHoc(string maNamHoc, string tenNamHoc, int rowIndex)
        {
            try
            {
                // 1. Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrWhiteSpace(maNamHoc))
                {
                    MessageBox.Show("Không xác định được mã năm học cần xóa!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Hiển thị thông tin chi tiết trước khi xóa
                string thongTinXoa = $"Bạn có chắc chắn muốn xóa năm học này?\n\n" +
                                    $"📚 Tên: {tenNamHoc}\n" +
                                    $"🔑 Mã: {maNamHoc}\n\n" +
                                    $"⚠️ CẢNH BÁO:\n" +
                                    $"• Thao tác này sẽ xóa vĩnh viễn năm học\n" +
                                    $"• Có thể ảnh hưởng đến các học kỳ liên quan\n" +
                                    $"• KHÔNG THỂ HOÀN TÁC sau khi xóa!\n\n" +
                                    $"Bạn có muốn tiếp tục?";

                // 3. Xác nhận xóa
                DialogResult result = MessageBox.Show(
                    thongTinXoa,
                    "⚠️ Xác nhận xóa năm học",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2  // Mặc định chọn "No"
                );

                if (result == DialogResult.Yes)
                {
                    // 4. Thực hiện xóa trong database
                    bool xoaThanhCong = namHocBUS.XoaNamHoc(maNamHoc);

                    if (xoaThanhCong)
                    {
                        // 5. Xóa row khỏi DataGridView
                        tbNamHoc.Rows.RemoveAt(rowIndex);

                        // 6. Cập nhật lại card thống kê
                        SetupCardNH();

                        // 7. Thông báo thành công
                        MessageBox.Show(
                            $"✓ Đã xóa năm học '{tenNamHoc}' thành công!\n\n" +
                            $"• Đã xóa khỏi database\n" +
                            $"• Đã cập nhật danh sách hiển thị\n" +
                            $"• Đã cập nhật thống kê",
                            "Xóa thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        // 8. Thông báo lỗi nếu không xóa được
                        MessageBox.Show(
                            $"✗ Không thể xóa năm học '{tenNamHoc}'!\n\n" +
                            $"Có thể do:\n" +
                            $"• Năm học không tồn tại trong database\n" +
                            $"• Năm học đang được sử dụng bởi học kỳ khác\n" +
                            $"• Lỗi kết nối database\n" +
                            $"• Không đủ quyền thao tác\n\n" +
                            $"Vui lòng kiểm tra lại!",
                            "Lỗi xóa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        // Reload lại để đảm bảo dữ liệu đồng bộ
                        SetupTbNamHoc();
                    }
                }
                else
                {
                    // 9. Người dùng hủy thao tác
                    MessageBox.Show(
                        "Đã hủy thao tác xóa năm học.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                // 10. Xử lý exception
                MessageBox.Show(
                    $"❌ Lỗi nghiêm trọng khi xóa năm học!\n\n" +
                    $"Lỗi: {ex.Message}\n\n" +
                    $"Chi tiết:\n{ex.StackTrace}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // Reload lại dữ liệu để đảm bảo đồng bộ
                try
                {
                    SetupTbNamHoc();
                }
                catch { }
            }
        }

        private void tbHocKy_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbHocKy.Columns["thaoTacHK"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                // ✅ Lấy permission từ Tag - Sử dụng cách an toàn hơn
                bool canDelete = true; // Mặc định true
                
                if (tbHocKy.Tag != null)
                {
                    try
                    {
                        dynamic permissions = tbHocKy.Tag;
                        canDelete = permissions?.CanDelete ?? true;
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        // Nếu Tag không có thuộc tính CanDelete, giữ giá trị mặc định
                        canDelete = true;
                    }
                }

                int iconSizeHK = 22;
                int iconEyeSizeHK = 32;
                int paddingHK = 6;

                int xEditHK = e.CellBounds.Left + paddingHK;
                int xDeleteHK = xEditHK + iconEyeSizeHK + 4 * paddingHK;
                int yHK = e.CellBounds.Top + (e.CellBounds.Height - iconEyeSizeHK) / 2;
                int yDeleteHK = e.CellBounds.Top + (e.CellBounds.Height - iconSizeHK) / 2;

                try
                {
                    Image editIconHK = Image.FromFile(@"..\..\Images\icon_eye.png");
                    Image deleteIconHK = Image.FromFile(@"..\..\Images\deleteicon.png");

                    // Luôn vẽ icon Xem
                    e.Graphics.DrawImage(editIconHK, new Rectangle(xEditHK, yHK, iconEyeSizeHK, iconEyeSizeHK));

                    // ✅ Vẽ icon Xóa với độ mờ nếu không có quyền
                    if (canDelete)
                    {
                        e.Graphics.DrawImage(deleteIconHK, new Rectangle(xDeleteHK, yDeleteHK, iconSizeHK, iconSizeHK));
                    }
                    else
                    {
                        var grayScaleMatrix = new System.Drawing.Imaging.ColorMatrix(
                            new float[][] {
                        new float[] {0.3f, 0.3f, 0.3f, 0, 0},
                        new float[] {0.59f, 0.59f, 0.59f, 0, 0},
                        new float[] {0.11f, 0.11f, 0.11f, 0, 0},
                        new float[] {0, 0, 0, 0.3f, 0},
                        new float[] {0, 0, 0, 0, 1}
                            });
                        using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                        {
                            attributes.SetColorMatrix(grayScaleMatrix);
                            e.Graphics.DrawImage(deleteIconHK, new Rectangle(xDeleteHK, yDeleteHK, iconSizeHK, iconSizeHK),
                                0, 0, deleteIconHK.Width, deleteIconHK.Height,
                                GraphicsUnit.Pixel, attributes);
                        }
                    }
                }
                catch { }

                e.Handled = true;
            }
        }

        private void tbHocKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tbHocKy.Columns["thaoTacHK"].Index)
            {
                var cell = tbHocKy.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                int x = tbHocKy.PointToClient(Cursor.Position).X - cell.X;

                int iconSizeHK = 22;
                int iconEyeSizeHK = 32;
                int paddingHK = 6;

                int eyeRightHK = paddingHK + iconEyeSizeHK;
                int deleteLeftHK = eyeRightHK + 4 * paddingHK;
                int deleteRightHK = deleteLeftHK + iconSizeHK;

                int maHocKy = Convert.ToInt32(tbHocKy.Rows[e.RowIndex].Cells["maHocKy"].Value);
                string namHocHK = tbHocKy.Rows[e.RowIndex].Cells["namHocHK"].Value.ToString();
                string hocKyInfo = tbHocKy.Rows[e.RowIndex].Cells["hocKy"].Value.ToString();

                if (x < eyeRightHK)
                {
                    // XEM CHI TIẾT HỌC KỲ
                    XemChiTietHocKy(maHocKy, namHocHK, hocKyInfo);
                }
                else if (x > deleteLeftHK && x < deleteRightHK)
                {
                    // ✅ KIỂM TRA QUYỀN XÓA
                    if (!PermissionHelper.CheckDeletePermission(PermissionHelper.QLNAMHOC, "Quản lý năm học"))
                        return;

                    // XÓA HỌC KỲ
                    XoaHocKy(maHocKy, namHocHK, hocKyInfo, e.RowIndex);
                }
            }
        }

        private void XemChiTietHocKy(int maHocKy, string tenNamHoc, string tenHocKy)
        {
            try
            {
                HocKyDTO hocKy = hocKyBUS.LayHocKyTheoMa(maHocKy);

                if (hocKy != null)
                {
                    string thongTin = $"📚 THÔNG TIN HỌC KỲ\n\n" +
                                    $"🔑 Mã học kỳ: {hocKy.MaHocKy}\n" +
                                    $"📝 Tên học kỳ: {hocKy.TenHocKy}\n" +
                                    $"📅 Năm học: {tenNamHoc}\n" +
                                    $"📅 Ngày bắt đầu: {hocKy.NgayBD?.ToString("dd/MM/yyyy") ?? "N/A"}\n" +
                                    $"📅 Ngày kết thúc: {hocKy.NgayKT?.ToString("dd/MM/yyyy") ?? "N/A"}\n" +
                                    $"🔄 Trạng thái: {(hocKy.NgayBD.HasValue && hocKy.NgayKT.HasValue ? TinhTrangThai(hocKy.NgayBD.Value, hocKy.NgayKT.Value) : "Chưa xác định")}";

                    MessageBox.Show(thongTin, "Chi tiết học kỳ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin học kỳ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaHocKy(int maHocKy, string tenNamHoc, string tenHocKy, int rowIndex)
        {
            try
            {
                if (maHocKy <= 0)
                {
                    MessageBox.Show("Không xác định được mã học kỳ cần xóa!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string thongTinXoa = $"Bạn có chắc chắn muốn xóa học kỳ này?\n\n" +
                                    $"📚 Học kỳ: {tenHocKy}\n" +
                                    $"📆 Năm học: {tenNamHoc}\n" +
                                    $"🔑 Mã: {maHocKy}\n\n" +
                                    $"⚠️ CẢNH BÁO:\n" +
                                    $"• Thao tác này sẽ xóa vĩnh viễn học kỳ\n" +
                                    $"• Có thể ảnh hưởng đến dữ liệu liên quan\n" +
                                    $"• KHÔNG THỂ HOÀN TÁC sau khi xóa!\n\n" +
                                    $"Bạn có muốn tiếp tục?";

                DialogResult result = MessageBox.Show(
                    thongTinXoa,
                    "⚠️ Xác nhận xóa học kỳ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2
                );

                if (result == DialogResult.Yes)
                {
                    bool xoaThanhCong = hocKyBUS.XoaHocKy(maHocKy);

                    if (xoaThanhCong)
                    {
                        tbHocKy.Rows.RemoveAt(rowIndex);
                        SetupCardHK();

                        MessageBox.Show(
                            $"✓ Đã xóa học kỳ '{tenHocKy}' thành công!\n\n" +
                            $"• Đã xóa khỏi database\n" +
                            $"• Đã cập nhật danh sách hiển thị\n" +
                            $"• Đã cập nhật thống kê",
                            "Xóa thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            $"✗ Không thể xóa học kỳ '{tenHocKy}'!\n\n" +
                            $"Có thể do:\n" +
                            $"• Học kỳ không tồn tại trong database\n" +
                            $"• Học kỳ đang được sử dụng\n" +
                            $"• Lỗi kết nối database\n\n" +
                            $"Vui lòng kiểm tra lại!",
                            "Lỗi xóa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        SetupTbHocKy();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi nghiêm trọng khi xóa học kỳ!\n\n" +
                    $"Lỗi: {ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                try
                {
                    SetupTbHocKy();
                }
                catch { }
            }
        }

        private void statCardKhoi2_Load(object sender, EventArgs e) { }
        private void statCardNH1_Load(object sender, EventArgs e) { }
        private void statCardNH3_Load(object sender, EventArgs e) { }
        private void tbNamHoc_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void tbHocKy_CellContentClick(object sender, EventArgs e) { }

        private void btnNamHoc_Click(object sender, EventArgs e)
        {
            tbNamHoc.Visible = true;
            tbHocKy.Visible = false;
            btnAddNamHoc.Visible = true;
            btnAddHocKy.Visible = false;
            statCardNH1.Visible = true;
            statCardNH2.Visible = true;
            statCardNH3.Visible = true;
            statCardHK1.Visible = false;
            statCardHK2.Visible = false;
            statCardHK3.Visible = false;
            btnNamHoc.FillColor = Color.FromArgb(30, 136, 229);
            btnNamHoc.ForeColor = Color.White;
            btnHocKy.FillColor = Color.White;
            btnHocKy.ForeColor = Color.Black;
        }

        private void btnKyLuat_Click(object sender, EventArgs e)
        {
            tbNamHoc.Visible = false;
            tbHocKy.Visible = true;
            btnAddNamHoc.Visible = false;
            btnAddHocKy.Visible = true;
            statCardNH1.Visible = false;
            statCardNH2.Visible = false;
            statCardNH3.Visible = false;
            statCardHK1.Visible = true;
            statCardHK2.Visible = true;
            statCardHK3.Visible = true;
            btnHocKy.FillColor = Color.FromArgb(30, 136, 229);
            btnHocKy.ForeColor = Color.White;
            btnNamHoc.FillColor = Color.White;
            btnNamHoc.ForeColor = Color.Black;
        }

        private void btnAddNamHoc_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmThemNamHoc frm = new frmThemNamHoc())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        SetupTbNamHoc();
                        SetupCardNH();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAddHocKy_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmThemHocKy frm = new frmThemHocKy())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        // Reload dữ liệu học kỳ
                        SetupTbHocKy();
                        SetupCardHK();

                        // Cập nhật lại card năm học (CARD 2 hiển thị học kỳ hiện tại)
                        SetupCardNH();

                        MessageBox.Show("Đã cập nhật danh sách học kỳ!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
