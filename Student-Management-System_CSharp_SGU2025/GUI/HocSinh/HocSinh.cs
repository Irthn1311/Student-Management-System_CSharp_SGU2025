using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO; // Cần cho FileInfo
using OfficeOpenXml; // Thư viện EPPlus
using OfficeOpenXml.Style; // Cần cho định dạng (tô màu, in đậm)

using Student_Management_System_CSharp_SGU2025.BUS; 
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class HocSinh : UserControl
    {

        private bool isShowingHocSinh = true;

        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;

        private List<HocSinhDTO> danhSachHocSinh;
        private List<PhuHuynhDTO> danhSachPhuHuynh;
        private List<(int hocSinh, int phuHuynh, string moiQuanHe)> danhSachMoiQuanHe;

        public HocSinh()
        {
            InitializeComponent();

            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();

            danhSachHocSinh = new List<HocSinhDTO>();
            danhSachPhuHuynh = new List<PhuHuynhDTO>();
            danhSachMoiQuanHe = new List<(int hocSinh, int phuHuynh, string moiQuanHe)>();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void statCardTongHocSinh_Load(object sender, EventArgs e)
        {

        }

        private void HocSinh_Load_1(object sender, EventArgs e)
        {
            // --- Thiết lập giao diện ban đầu ---
            SetInitialView();

            // --- Cấu hình các bảng ---
            SetupTableHocSinh();
            SetupTablePhuHuynh();
            SetupTableMoiQuanHe(); 

            // --- Nạp dữ liệu mẫu ---
            LoadSampleDataHocSinh();
            LoadSampleDataPhuHuynh();
            LoadSampleDataMoiQuanHe(); 

            // --- Cấu hình Header và Thẻ Thống kê ---
            SetupHeaderAndStats();
        }

        private void SetInitialView()
        {
            isShowingHocSinh = true;
            UpdateView();
        }

        private void UpdateView()
        {
            if (isShowingHocSinh)
            {
                tableHocSinh.Visible = true;
                btnThemHocSinh.Visible = true;
                tablePhuHuynh.Visible = false;
                btnThemPhuHuynh.Visible = false;
                btnPhuHuynh.Text = "Phụ Huynh";
                headerQuanLiHocSinh.lbHeader.Text = "Hồ sơ Học sinh"; // Cập nhật header
                headerQuanLiHocSinh.lbGhiChu.Text = "Trang chủ / Hồ sơ học sinh";
            }
            else
            {
                tableHocSinh.Visible = false;
                btnThemHocSinh.Visible = false;
                tablePhuHuynh.Visible = true;
                btnThemPhuHuynh.Visible = true;
                btnPhuHuynh.Text = "Học Sinh";
                headerQuanLiHocSinh.lbHeader.Text = "Thông tin Phụ huynh"; // Cập nhật header
                headerQuanLiHocSinh.lbGhiChu.Text = "Trang chủ / Phụ huynh";
            }
        }

        // Cấu hình Header và Thẻ Thống kê (Tách ra từ Load cũ)
        private void SetupHeaderAndStats()
        {
            headerQuanLiHocSinh.lbTenDangNhap.Text = "Nguyễn Văn A";
            headerQuanLiHocSinh.lbVaiTro.Text = "Giáo vụ";

            int tongHocSinh = hocSinhBLL.GetTotalHocSinh();
            int tongHocSinhNam = hocSinhBLL.GetTotalHocSinhNam();
            int tongHocSinhNu = hocSinhBLL.GetTotalHocSinhNu();
            int tongHocSinhDangHoc = hocSinhBLL.GetTotalHocSinhDangHoc();
            int tongHocSinhNghiHoc = tongHocSinh - tongHocSinhDangHoc;

            // --- Tính toán phần trăm ---
            double phanTramNam = 0;
            double phanTramNu = 0;

            if (tongHocSinh > 0) // Tránh lỗi chia cho 0
            {
                // Quan trọng: Phải ép kiểu (double) để phép chia ra số thập phân
                phanTramNam = Math.Round(((double)tongHocSinhNam / tongHocSinh) * 100, 1); // Làm tròn 1 chữ số thập phân
                phanTramNu = 100.0 - phanTramNam;                                                                                        
            }

            // --- Cập nhật các thẻ thống kê ---
            statCardTongHocSinh.lbCardTitle.Text = "Tổng học sinh";
            statCardTongHocSinh.lbCardValue.Text = tongHocSinh.ToString("N0"); // Định dạng có dấu phẩy ngăn cách
            int tongLop = 36; // Lấy từ lopHocBLL.GetTotalLopHoc();
            if (tongLop > 0)
            {
                double siSoTB = Math.Round((double)tongHocSinh / tongLop, 1);
                statCardTongHocSinh.lbCardNote.Text = $"TB: {siSoTB} HS/lớp";
            }

            statCardNam.lbCardTitle.Text = "Nam";
            statCardNam.lbCardValue.Text = tongHocSinhNam.ToString("N0");
            // 👇 Cập nhật Note với phần trăm đã tính
            statCardNam.lbCardNote.Text = $"{phanTramNam}% tổng số";

            statCardNu.lbCardTitle.Text = "Nữ";
            statCardNu.lbCardValue.Text = tongHocSinhNu.ToString("N0");
            // 👇 Cập nhật Note với phần trăm đã tính
            statCardNu.lbCardNote.Text = $"{phanTramNu}% tổng số";

            statCardDangHoc.lbCardTitle.Text = "Đang học";
            statCardDangHoc.lbCardValue.Text = tongHocSinhDangHoc.ToString("N0");
            // 👇 Cập nhật Note (thêm chữ "nghỉ học")
            statCardDangHoc.lbCardNote.Text = $"{tongHocSinhNghiHoc} nghỉ học";

            // --- Gán màu sắc (giữ nguyên) ---
            statCardTongHocSinh.lbCardNote.ForeColor = Color.FromArgb(22, 163, 74);
            statCardNam.lbCardValue.ForeColor = Color.FromArgb(30, 136, 229);
            statCardNu.lbCardValue.ForeColor = Color.FromArgb(219, 39, 119);
            statCardDangHoc.lbCardValue.ForeColor = Color.FromArgb(22, 163, 74);
            statCardDangHoc.lbCardNote.ForeColor = Color.FromArgb(220, 38, 38);
        }

        #region Bảng Học Sinh

        private void SetupTableHocSinh()
        {
            // --- Xóa cột cũ và cấu hình chung ---
            tableHocSinh.Columns.Clear();
            ApplyBaseTableStyle(tableHocSinh); // Áp dụng style chung

            // --- Thêm cột mới ---
            tableHocSinh.Columns.Add("MaHS", "Mã HS");
            tableHocSinh.Columns.Add("HoTen", "Họ và tên");
            tableHocSinh.Columns.Add("NgaySinh", "Ngày sinh");
            tableHocSinh.Columns.Add("GioiTinh", "Giới tính");
            tableHocSinh.Columns.Add("Lop", "Lớp");
            tableHocSinh.Columns.Add("TrangThai", "Trạng thái");
            tableHocSinh.Columns.Add("ThaoTacHS", "Thao tác"); // <-- Cột thao tác mới

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tableHocSinh);
            tableHocSinh.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- Tùy chỉnh kích thước ---
            tableHocSinh.Columns["MaHS"].FillWeight = 10; tableHocSinh.Columns["MaHS"].MinimumWidth = 60;
            tableHocSinh.Columns["HoTen"].FillWeight = 25; tableHocSinh.Columns["HoTen"].MinimumWidth = 150;
            tableHocSinh.Columns["NgaySinh"].FillWeight = 12; tableHocSinh.Columns["NgaySinh"].MinimumWidth = 100;
            tableHocSinh.Columns["GioiTinh"].FillWeight = 10; tableHocSinh.Columns["GioiTinh"].MinimumWidth = 80;
            tableHocSinh.Columns["Lop"].FillWeight = 10; tableHocSinh.Columns["Lop"].MinimumWidth = 70;
            tableHocSinh.Columns["TrangThai"].FillWeight = 10; tableHocSinh.Columns["TrangThai"].MinimumWidth = 90;
            tableHocSinh.Columns["ThaoTacHS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableHocSinh.Columns["ThaoTacHS"].Width = 100; // Độ rộng cột thao tác

            // --- Gắn sự kiện ---
            tableHocSinh.CellFormatting -= tableHocSinh_CellFormatting; // Gỡ sự kiện cũ (nếu có)
            tableHocSinh.CellFormatting += tableHocSinh_CellFormatting;
            tableHocSinh.CellPainting -= tableHocSinh_CellPainting; // Gỡ sự kiện cũ (nếu có)
            tableHocSinh.CellPainting += tableHocSinh_CellPainting;
            tableHocSinh.CellClick -= tableHocSinh_CellClick; // Gỡ sự kiện cũ (nếu có)
            tableHocSinh.CellClick += tableHocSinh_CellClick;
        }

        private void LoadSampleDataHocSinh()
        {
            tableHocSinh.Rows.Clear();
            danhSachHocSinh = hocSinhBLL.GetAllHocSinh();
            
            foreach(HocSinhDTO hs in danhSachHocSinh)
            {
                tableHocSinh.Rows.Add(hs.MaHS, hs.HoTen, hs.NgaySinh.ToString("dd/MM/yyyy"), hs.GioiTinh, "10A1", hs.TrangThai, "");
            }

        }

        // Vẽ icon cho bảng Học Sinh
        private void tableHocSinh_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableHocSinh.Columns["ThaoTacHS"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                Image editIcon = Properties.Resources.repair; // Đổi icon nếu cần
                Image deleteIcon = Properties.Resources.bin; // Đổi icon nếu cần

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle editRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);

                e.Graphics.DrawImage(editIcon, editRect);
                e.Graphics.DrawImage(deleteIcon, deleteRect);

                e.Handled = true;
            }
        }

        // Xử lý click icon cho bảng Học Sinh
        private void tableHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableHocSinh.Columns["ThaoTacHS"].Index)
            {
                HandleIconClick(tableHocSinh, e.RowIndex, "MaHS");
            }
        }

        // Định dạng màu cho cột Giới tính, Trạng thái
        private void tableHocSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua header

            // Giới tính
            if (tableHocSinh.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null)
            {
                FormatGenderCell(e); // Gọi hàm định dạng
            }

            // Trạng thái
            if (tableHocSinh.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                FormatStatusCell(e); // Gọi hàm định dạng
            }
        }

        #endregion

        #region Bảng Phụ Huynh

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
            tablePhuHuynh.Columns.Add("ThaoTacPH", "Thao tác"); 

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tablePhuHuynh);
            tablePhuHuynh.Columns["HoTenPH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["Email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tablePhuHuynh.Columns["DiaChi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // --- Tùy chỉnh kích thước ---
            tablePhuHuynh.Columns["MaPH"].FillWeight = 10; tablePhuHuynh.Columns["MaPH"].MinimumWidth = 60;
            tablePhuHuynh.Columns["HoTenPH"].FillWeight = 20; tablePhuHuynh.Columns["HoTenPH"].MinimumWidth = 130;
            tablePhuHuynh.Columns["Sdt"].FillWeight = 12; tablePhuHuynh.Columns["Sdt"].MinimumWidth = 100;
            tablePhuHuynh.Columns["Email"].FillWeight = 20; tablePhuHuynh.Columns["Email"].MinimumWidth = 150;
            tablePhuHuynh.Columns["DiaChi"].FillWeight = 25; tablePhuHuynh.Columns["DiaChi"].MinimumWidth = 180;
            tablePhuHuynh.Columns["ThaoTacPH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tablePhuHuynh.Columns["ThaoTacPH"].Width = 100; // Độ rộng cột thao tác

            // --- Gắn sự kiện ---
            tablePhuHuynh.CellPainting += tablePhuHuynh_CellPainting;
            tablePhuHuynh.CellClick += tablePhuHuynh_CellClick;
        }

        private void LoadSampleDataPhuHuynh()
        {
            tablePhuHuynh.Rows.Clear();

            danhSachPhuHuynh = phuHuynhBLL.GetAllPhuHuynh();

            foreach (PhuHuynhDTO ph in danhSachPhuHuynh)
            {
                tablePhuHuynh.Rows.Add(ph.MaPhuHuynh, ph.HoTen, ph.SoDienThoai, ph.Email, ph.DiaChi, "");
            }
        }

        // Vẽ icon cho bảng Phụ Huynh
        private void tablePhuHuynh_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tablePhuHuynh.Columns["ThaoTacPH"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                Image editIcon = Properties.Resources.repair;
                Image deleteIcon = Properties.Resources.bin;

                int iconSize = 18;
                int spacing = 15;
                int totalWidth = iconSize * 2 + spacing;
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle editRect = new Rectangle(startX, y, iconSize, iconSize);
                Rectangle deleteRect = new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize);

                e.Graphics.DrawImage(editIcon, editRect);
                e.Graphics.DrawImage(deleteIcon, deleteRect);

                e.Handled = true;
            }
        }

        // Xử lý click icon cho bảng Phụ Huynh
        private void tablePhuHuynh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tablePhuHuynh.Columns["ThaoTacPH"].Index)
            {
                HandleIconClick(tablePhuHuynh, e.RowIndex, "MaPH");
            }
        }

        #region Bảng Mối Quan Hệ

        private void SetupTableMoiQuanHe()
        {
            // --- Xóa cột cũ và cấu hình chung ---
            tableMoiQuanHe.Columns.Clear();
            ApplyBaseTableStyle(tableMoiQuanHe); // Áp dụng style chung

            // --- Thêm cột mới ---
            tableMoiQuanHe.Columns.Add("HocSinhMQH", "Học Sinh"); // Đổi tên để tránh trùng
            tableMoiQuanHe.Columns.Add("PhuHuynhMQH", "Phụ Huynh"); // Đổi tên để tránh trùng
            tableMoiQuanHe.Columns.Add("QuanHe", "Mối quan hệ");
            tableMoiQuanHe.Columns.Add("ThaoTacMQH", "Thao Tác"); // Đổi tên để tránh trùng

            // --- Căn chỉnh cột ---
            ApplyColumnAlignmentAndWrapping(tableMoiQuanHe);
            tableMoiQuanHe.Columns["HocSinhMQH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableMoiQuanHe.Columns["PhuHuynhMQH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableMoiQuanHe.Columns["QuanHe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa
            tableMoiQuanHe.Columns["ThaoTacMQH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa


            // --- Tùy chỉnh kích thước ---
            tableMoiQuanHe.Columns["HocSinhMQH"].FillWeight = 30; tableMoiQuanHe.Columns["HocSinhMQH"].MinimumWidth = 150;
            tableMoiQuanHe.Columns["PhuHuynhMQH"].FillWeight = 30; tableMoiQuanHe.Columns["PhuHuynhMQH"].MinimumWidth = 150;
            tableMoiQuanHe.Columns["QuanHe"].FillWeight = 15; tableMoiQuanHe.Columns["QuanHe"].MinimumWidth = 100;
            tableMoiQuanHe.Columns["ThaoTacMQH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tableMoiQuanHe.Columns["ThaoTacMQH"].Width = 80; // Độ rộng cột thao tác

            // --- Gắn sự kiện ---
            tableMoiQuanHe.CellPainting += tableMoiQuanHe_CellPainting;
            tableMoiQuanHe.CellClick += tableMoiQuanHe_CellClick;
        }

        private void LoadSampleDataMoiQuanHe()
        {
            tableMoiQuanHe.Rows.Clear();
            danhSachMoiQuanHe = hocSinhPhuHuynhBLL.GetAllQuanHe();

            foreach ((int maHS, int maPH, string mqh) item in danhSachMoiQuanHe) // Dùng var hoặc (int maHS, int maPH, string mqh)
            {
                
                string tenHS = hocSinhBLL.GetHocSinhById(item.maHS)?.HoTen ?? $"Không tìm thấy HS ({item.maHS})";
                string tenPH = phuHuynhBLL.GetPhuHuynhById(item.maPH)?.HoTen ?? $"Không tìm thấy PH ({item.maPH})";

                tableMoiQuanHe.Rows.Add(
                    tenHS,            
                    tenPH,            
                    item.mqh,    
                    ""                
                );
            }

        }

        // Vẽ icon cho bảng Mối Quan Hệ
        private void tableMoiQuanHe_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == tableMoiQuanHe.Columns["ThaoTacMQH"].Index)
            {
                e.PaintBackground(e.ClipBounds, true);

                // Chỉ lấy icon Xóa
                Image deleteIcon = Properties.Resources.bin;

                int iconSize = 18;

                // Tính toán vị trí X, Y để căn giữa 1 icon
                int startX = e.CellBounds.Left + (e.CellBounds.Width - iconSize) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                Rectangle deleteRect = new Rectangle(startX, y, iconSize, iconSize);

                // Chỉ vẽ icon Xóa
                e.Graphics.DrawImage(deleteIcon, deleteRect);

                e.Handled = true;
            }
        }

        // Xử lý click icon cho bảng Mối Quan Hệ
        private void tableMoiQuanHe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Chỉ xử lý khi click vào hàng dữ liệu và cột "ThaoTacMQH"
            if (e.RowIndex >= 0 && e.ColumnIndex == tableMoiQuanHe.Columns["ThaoTacMQH"].Index)
            {
                // Vì chỉ có 1 icon (Xóa), không cần kiểm tra vị trí click X
                try
                {
                    // Lấy lại Tuple từ danh sách gốc dựa vào chỉ số dòng
                    var quanHeToDelete = danhSachMoiQuanHe[e.RowIndex];
                    int maHS = quanHeToDelete.hocSinh;
                    int maPH = quanHeToDelete.phuHuynh;

                    if (MessageBox.Show($"Bạn có chắc muốn xóa mối quan hệ giữa HS {maHS} và PH {maPH}?",
                                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (hocSinhPhuHuynhBLL.DeleteQuanHe(maHS, maPH))
                        {
                            MessageBox.Show("Đã xóa mối quan hệ.");
                            LoadSampleDataMoiQuanHe(); // Nạp lại bảng MQH
                        }
                        else
                        {
                            MessageBox.Show("Xóa mối quan hệ thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException) // Bắt lỗi nếu rowIndex không hợp lệ
                {
                    MessageBox.Show("Không thể lấy thông tin mối quan hệ để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) // Bắt các lỗi khác
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xóa mối quan hệ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #endregion

        #region Hàm dùng chung và Helper

        // Hàm áp dụng style cơ bản cho DataGridView
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

            // Xóa sự kiện cũ để tránh gắn nhiều lần
            dgv.CellMouseEnter -= DataGridView_CellMouseEnter;
            dgv.CellMouseLeave -= DataGridView_CellMouseLeave;
            dgv.SelectionChanged -= DataGridView_SelectionChanged;

            // Gắn sự kiện hover và selection
            dgv.CellMouseEnter += DataGridView_CellMouseEnter;
            dgv.CellMouseLeave += DataGridView_CellMouseLeave;
            dgv.SelectionChanged += DataGridView_SelectionChanged;

            // Đảm bảo màu header không đổi khi click
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

        // Hàm xử lý click icon chung
        private void HandleIconClick(Guna.UI2.WinForms.Guna2DataGridView dgv, int rowIndex, string idColumnName)
        {
            // Lấy ô thao tác dựa trên tên cột của từng bảng
            string thaoTacColName = "";
            if (dgv == tableHocSinh) thaoTacColName = "ThaoTacHS";
            else if (dgv == tablePhuHuynh) thaoTacColName = "ThaoTacPH";
            else return; // Bảng không xác định

            Rectangle cellBounds = dgv.GetCellDisplayRectangle(dgv.Columns[thaoTacColName].Index, rowIndex, false);
            Point clickPosInCell = dgv.PointToClient(Cursor.Position);
            int xClick = clickPosInCell.X - cellBounds.Left;

            int iconSize = 18;
            int spacing = 15;
            int totalWidth = iconSize * 2 + spacing;
            int startXInCell = (cellBounds.Width - totalWidth) / 2;

            int editIconEndX = startXInCell + iconSize;
            int deleteIconStartX = startXInCell + iconSize + spacing;
            int deleteIconEndX = deleteIconStartX + iconSize;

            // Lấy ID chính của dòng (HS hoặc PH)
            string idValueStr = dgv.Rows[rowIndex].Cells[idColumnName].Value?.ToString();

            // Xử lý Click Sửa
            if (xClick >= startXInCell && xClick < editIconEndX)
            {
                int maToEdit;
                if (int.TryParse(idValueStr, out maToEdit))
                {
                    if (dgv == tableHocSinh) // Nếu là bảng Học Sinh
                    {
                        // Mở form Chỉnh sửa Học Sinh, truyền Mã HS vào constructor
                        ChinhSuaHocSinh frmEditHS = new ChinhSuaHocSinh(maToEdit);
                        frmEditHS.StartPosition = FormStartPosition.CenterParent; // Hiện giữa form cha

                        // Hiển thị form và kiểm tra kết quả sau khi đóng
                        DialogResult result = frmEditHS.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            // Nếu form sửa trả về OK (đã lưu thành công) -> Load lại dữ liệu
                            LoadSampleDataHocSinh();
                            LoadSampleDataMoiQuanHe();

                            SetupHeaderAndStats();     
                        }
                    }
                    else if (dgv == tablePhuHuynh) // Nếu là bảng Phụ Huynh
                    {
                        ChinhSuaPhuHuynh frmEditHS = new ChinhSuaPhuHuynh(maToEdit);
                        frmEditHS.StartPosition = FormStartPosition.CenterParent; // Hiện giữa form cha

                        // Hiển thị form và kiểm tra kết quả sau khi đóng
                        DialogResult result = frmEditHS.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            LoadSampleDataPhuHuynh();
                        }
                    }
                    
                }
                else { MessageBox.Show("Mã không hợp lệ để sửa."); }
            }
            // Xử lý Click Xóa
            else if (xClick >= deleteIconStartX && xClick < deleteIconEndX)
            {
                if (dgv == tableHocSinh) // Xóa Học Sinh
                {
                    if (MessageBox.Show($"Bạn có chắc muốn xóa học sinh {idValueStr}?\nTất cả mối quan hệ phụ huynh liên quan cũng sẽ bị xóa.",
                                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int maHS;
                        if (int.TryParse(idValueStr, out maHS))
                        {
                            bool deleteQuanHeSuccess = hocSinhPhuHuynhBLL.DeleteQuanHeByHocSinh(maHS); // Xóa QH trước
                            bool deleteHSSuccess = hocSinhBLL.DeleteHocSinh(maHS); // Xóa HS sau

                            if (deleteHSSuccess) // Chỉ cần kiểm tra xóa HS thành công
                            {
                                MessageBox.Show("Đã xóa học sinh và các mối quan hệ liên quan.");
                                LoadSampleDataHocSinh(); // Nạp lại bảng HS
                                LoadSampleDataMoiQuanHe(); // Nạp lại bảng MQH
                                SetupHeaderAndStats();      // Cập nhật lại các thẻ thống kê
                            }
                            else
                            {
                                MessageBox.Show("Xóa học sinh thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else { MessageBox.Show("Mã học sinh không hợp lệ."); }
                    }
                }
                else if (dgv == tablePhuHuynh) // Xóa Phụ Huynh
                {
                    if (MessageBox.Show($"Bạn có chắc muốn xóa phụ huynh {idValueStr}?\nTất cả mối quan hệ với học sinh liên quan cũng sẽ bị xóa.",
                                       "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int maPH;
                        if (int.TryParse(idValueStr, out maPH))
                        {
                            bool deleteQuanHeSuccess = hocSinhPhuHuynhBLL.DeleteQuanHeByPhuHuynh(maPH); // Xóa QH trước
                            bool deletePHSuccess = phuHuynhBLL.DeletePhuHuynh(maPH); // Xóa PH sau

                            if (deletePHSuccess)
                            {
                                MessageBox.Show("Đã xóa phụ huynh và các mối quan hệ liên quan.");
                                LoadSampleDataPhuHuynh(); // Nạp lại bảng PH
                                LoadSampleDataMoiQuanHe(); // Nạp lại bảng MQH
                                SetupHeaderAndStats();      // Cập nhật lại các thẻ thống kê
                            }
                            else
                            {
                                MessageBox.Show("Xóa phụ huynh thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else { MessageBox.Show("Mã phụ huynh không hợp lệ."); }
                    }
                }
                
            }
        }

        // Hàm định dạng ô Giới tính
        private void FormatGenderCell(DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold); // Hơi đậm hơn
            e.CellStyle.Padding = new Padding(5, 3, 5, 3); // Thêm padding nhẹ

            if (e.Value.ToString() == "Nam")
            {
                e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216);
                e.CellStyle.BackColor = Color.FromArgb(219, 234, 254);
            }
            else if (e.Value.ToString() == "Nữ")
            {
                e.CellStyle.ForeColor = Color.FromArgb(190, 24, 93);
                e.CellStyle.BackColor = Color.FromArgb(253, 232, 255); // Đổi màu nền Nữ
            }
        }

        // Hàm định dạng ô Trạng thái
        private void FormatStatusCell(DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9, FontStyle.Bold); // Hơi đậm hơn
            e.CellStyle.Padding = new Padding(5, 3, 5, 3); // Thêm padding nhẹ

            if (e.Value.ToString() == "Đang học")
            {
                e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52); // Đậm hơn
              
            }
            else // Nghỉ học hoặc trạng thái khác
            {
                e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27); // Đậm hơn
              
            }
        }


        // Sự kiện hover chung
        private void DataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(243, 246, 255);
            }
        }

        // Sự kiện rời chuột chung
        private void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                    (e.RowIndex % 2 == 0) ? Color.White : Color.FromArgb(250, 250, 250);
            }
        }

        // Bỏ chọn dòng khi selection thay đổi
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as Guna.UI2.WinForms.Guna2DataGridView;
            dgv.ClearSelection();
        }

        #endregion

        private void statCardNam_Load(object sender, EventArgs e)
        {

        }

        private void headerQuanLiHocSinh_Load(object sender, EventArgs e)
        {

        }

        private void btnThemHocSinh_Click(object sender, EventArgs e)
        {
            // 1. Tạo và hiển thị form Thêm
            ThemHoSoHocSinh frm = new ThemHoSoHocSinh();
            frm.StartPosition = FormStartPosition.CenterScreen; 

            // 2. Hiển thị form dưới dạng Dialog và chờ kết quả
            DialogResult result = frm.ShowDialog();

            // 3. Kiểm tra kết quả trả về từ form Thêm
            if (result == DialogResult.OK)
            {
                
                try
                {
                    LoadSampleDataHocSinh();    // Load lại bảng Học sinh
                    LoadSampleDataMoiQuanHe();  // Load lại bảng Mối quan hệ
                                                
                    SetupHeaderAndStats();      // Cập nhật lại các thẻ thống kê
                    MessageBox.Show("Dữ liệu đã được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPhuHuynh_Click(object sender, EventArgs e)
        {
            isShowingHocSinh = !isShowingHocSinh; // Đảo trạng thái
            UpdateView(); // Cập nhật lại giao diện
        }

        private void btnThemPhuHuynh_Click(object sender, EventArgs e)
        {
            // 1. Tạo và hiển thị form Thêm
            ThemPhuHuynh frm = new ThemPhuHuynh();
            frm.StartPosition = FormStartPosition.CenterScreen;

            // 2. Hiển thị form dưới dạng Dialog và chờ kết quả
            DialogResult result = frm.ShowDialog();

            // 3. Kiểm tra kết quả trả về từ form Thêm
            if (result == DialogResult.OK)
            {

                try
                {
                    LoadSampleDataPhuHuynh();    // Load lại bảng 
                  
                    MessageBox.Show("Dữ liệu đã được cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Hàm chính để tạo file Excel và thêm 3 worksheet.
        /// </summary>
        private void ExportAllDataToExcel(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Xóa các sheet cũ nếu file đã tồn tại
                while (package.Workbook.Worksheets.Count > 0)
                {
                    package.Workbook.Worksheets.Delete(0); // Xóa sheet ở vị trí đầu tiên
                }

                // 1. Thêm tab (Worksheet) "HocSinh"
                ExportDataGridViewToWorksheet(package, tableHocSinh, "HocSinh");

                // 2. Thêm tab "PhuHuynh"
                ExportDataGridViewToWorksheet(package, tablePhuHuynh, "PhuHuynh");

                // 3. Thêm tab "MoiQuanHe" (sửa tên nếu bảng của bạn khác)
                ExportDataGridViewToWorksheet(package, tableMoiQuanHe, "MoiQuanHe");

                // Lưu file
                package.Save();
            }
        }

        /// <summary>
        /// Hàm phụ trợ để xuất dữ liệu từ một DataGridView vào một Worksheet.
        /// </summary>
        private void ExportDataGridViewToWorksheet(ExcelPackage package, DataGridView dgv, string sheetName)
        {
            ExcelWorksheet ws = package.Workbook.Worksheets.Add(sheetName);

            // Tải tiêu đề cột (Headers)
            for (int col = 0; col < dgv.Columns.Count; col++)
            {
                // Bỏ qua các cột Thao tác
                if (dgv.Columns[col].Name.StartsWith("ThaoTac"))
                {
                    continue;
                }

                // Ghi tiêu đề và định dạng
                ws.Cells[1, col + 1].Value = dgv.Columns[col].HeaderText;
                ws.Cells[1, col + 1].Style.Font.Bold = true;
                ws.Cells[1, col + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[1, col + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                ws.Cells[1, col + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            // Tải dữ liệu (Rows)
            for (int row = 0; row < dgv.Rows.Count; row++)
            {
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    // Bỏ qua các cột Thao tác
                    if (dgv.Columns[col].Name.StartsWith("ThaoTac"))
                    {
                        continue;
                    }

                    // Lấy giá trị cell, xử lý giá trị null
                    object cellValue = dgv.Rows[row].Cells[col].Value;
                    ws.Cells[row + 2, col + 1].Value = cellValue?.ToString() ?? "";

                    // Định dạng màu sắc cho cột Giới tính và Trạng thái (giống như DataGridView)
                    if (dgv.Columns[col].Name == "GioiTinh")
                    {
                        if (cellValue?.ToString() == "Nam")
                            ws.Cells[row + 2, col + 1].Style.Font.Color.SetColor(Color.FromArgb(29, 78, 216));
                        else if (cellValue?.ToString() == "Nữ")
                            ws.Cells[row + 2, col + 1].Style.Font.Color.SetColor(Color.FromArgb(190, 24, 93));
                    }

                    if (dgv.Columns[col].Name == "TrangThai")
                    {
                        if (cellValue?.ToString() == "Đang học")
                            ws.Cells[row + 2, col + 1].Style.Font.Color.SetColor(Color.FromArgb(22, 101, 52));
                        else
                            ws.Cells[row + 2, col + 1].Style.Font.Color.SetColor(Color.FromArgb(153, 27, 27));
                    }
                }
            }

            try
            {
                if (sheetName == "HocSinh")
                {
                    ws.Column(1).Width = 10; // Mã HS
                    ws.Column(2).Width = 30; // Họ và tên
                    ws.Column(3).Width = 15; // Ngày sinh
                    ws.Column(4).Width = 12; // Giới tính
                    ws.Column(5).Width = 10; // Lớp
                    ws.Column(6).Width = 15; // Trạng thái
                }
                else if (sheetName == "PhuHuynh")
                {
                    ws.Column(1).Width = 10; // Mã PH
                    ws.Column(2).Width = 30; // Họ và tên
                    ws.Column(3).Width = 15; // SĐT
                    ws.Column(4).Width = 30; // Email
                    ws.Column(5).Width = 40; // Địa chỉ
                }
                else if (sheetName == "MoiQuanHe")
                {
                    ws.Column(1).Width = 30; // Học Sinh
                    ws.Column(2).Width = 30; // Phụ Huynh
                    ws.Column(3).Width = 18; // Mối quan hệ
                }
            }
            catch (Exception) { /* Bỏ qua lỗi nếu tên cột không khớp */ }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            // Yêu cầu bản quyền cho EPPlus 5 trở lên (dùng NonCommercial cho mục đích học tập/cá nhân)
            
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                sfd.FileName = "DanhSach_HocSinh_PhuHuynh.xlsx";
                sfd.Title = "Chọn nơi lưu file Excel";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Gọi hàm helper để xuất 3 bảng vào file
                        ExportAllDataToExcel(sfd.FileName);

                        MessageBox.Show("Xuất file Excel thành công!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi khi xuất file Excel: " + ex.Message, "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}


