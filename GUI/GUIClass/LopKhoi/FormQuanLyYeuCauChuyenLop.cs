using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FormQuanLyYeuCauChuyenLop : Form
    {
        private YeuCauChuyenLopBLL yeuCauBLL;
        private LopHocBUS lopHocBUS;
        private PhanLopBLL phanLopBLL;
        private string tenDangNhapAdmin;
        private List<YeuCauChuyenLopDTO> danhSachYeuCau;

        public FormQuanLyYeuCauChuyenLop(string tenDangNhapAdmin = null)
        {
            InitializeComponent();
            // Lấy tên đăng nhập từ SessionManager nếu không được truyền vào
            this.tenDangNhapAdmin = tenDangNhapAdmin ?? SessionManager.TenDangNhap ?? "admin";
            yeuCauBLL = new YeuCauChuyenLopBLL();
            lopHocBUS = new LopHocBUS();
            phanLopBLL = new PhanLopBLL();
            danhSachYeuCau = new List<YeuCauChuyenLopDTO>();
        }

        private void FormQuanLyYeuCauChuyenLop_Load(object sender, EventArgs e)
        {
            LoadDanhSachYeuCau();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvYeuCau.AutoGenerateColumns = true; // ✅ Tạm thời để true để tự động tạo cột
            dgvYeuCau.AllowUserToAddRows = false;
            dgvYeuCau.AllowUserToDeleteRows = false;
            dgvYeuCau.ReadOnly = true;
            dgvYeuCau.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvYeuCau.MultiSelect = false;
            dgvYeuCau.RowHeadersVisible = false;
            dgvYeuCau.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // ✅ Không tự động resize
            dgvYeuCau.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvYeuCau.DefaultCellStyle.Padding = new Padding(5);
            dgvYeuCau.EnableHeadersVisualStyles = false;
            dgvYeuCau.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dgvYeuCau.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 136, 229);
            dgvYeuCau.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvYeuCau.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvYeuCau.ColumnHeadersHeight = 45;
            dgvYeuCau.GridColor = Color.FromArgb(226, 232, 240);
            dgvYeuCau.BorderStyle = BorderStyle.None;
        }

        private void LoadDanhSachYeuCau()
        {
            try
            {
                // Lấy trạng thái lọc
                string trangThai = "Tất cả";
                if (rbChoDuyet.Checked) trangThai = "Chờ duyệt";
                else if (rbDaDuyet.Checked) trangThai = "Đã duyệt";
                else if (rbTuChoi.Checked) trangThai = "Từ chối";

                // Lấy danh sách
                if (trangThai == "Tất cả")
                {
                    danhSachYeuCau = yeuCauBLL.LayTatCaYeuCau();
                }
                else
                {
                    danhSachYeuCau = yeuCauBLL.LayYeuCauTheoTrangThai(trangThai);
                }

                // Bind vào DataGridView
                dgvYeuCau.DataSource = null;
                dgvYeuCau.DataSource = danhSachYeuCau;

                // ✅ Ẩn các cột không cần thiết, chỉ hiển thị cột quan trọng
                AnCacCotKhongCanThiet();

                // Cập nhật label đếm với màu sắc
                int soChoDuyet = 0;
                int soDaDuyet = 0;
                int soTuChoi = 0;

                foreach (var yc in danhSachYeuCau)
                {
                    if (yc.TrangThai == "Chờ duyệt") soChoDuyet++;
                    else if (yc.TrangThai == "Đã duyệt") soDaDuyet++;
                    else if (yc.TrangThai == "Từ chối") soTuChoi++;
                }

                lblThongKe.Text = $"📊 Tổng: {danhSachYeuCau.Count} | ⏳ Chờ duyệt: {soChoDuyet} | ✅ Đã duyệt: {soDaDuyet} | ❌ Từ chối: {soTuChoi}";

                // Format DataGridView
                FormatDataGridView();
                
                // Format cột ngày tạo
                if (dgvYeuCau.Columns["colNgayTao"] != null)
                {
                    dgvYeuCau.Columns["colNgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    dgvYeuCau.Columns["colNgayTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách yêu cầu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvYeuCau.Rows.Count == 0) return;

            // Tô màu theo trạng thái với hiệu ứng đẹp hơn
            foreach (DataGridViewRow row in dgvYeuCau.Rows)
            {
                if (row.DataBoundItem is YeuCauChuyenLopDTO yc)
                {
                    // Đặt font cho các cell
                    row.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
                    
                    if (yc.TrangThai == "Chờ duyệt")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 251, 235); // Vàng nhạt
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(120, 53, 15);
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 243, 199);
                        row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(120, 53, 15);
                    }
                    else if (yc.TrangThai == "Đã duyệt")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(240, 253, 244); // Xanh nhạt
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 247, 208);
                        row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(22, 163, 74);
                    }
                    else if (yc.TrangThai == "Từ chối")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(254, 242, 242); // Đỏ nhạt
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 202, 202);
                        row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(220, 38, 38);
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                    }
                    
                    // Format cột trạng thái với icon
                    if (dgvYeuCau.Columns["colTrangThai"] != null && row.Cells["colTrangThai"] != null)
                    {
                        string trangThaiText = yc.TrangThai;
                        if (yc.TrangThai == "Chờ duyệt") trangThaiText = "⏳ " + trangThaiText;
                        else if (yc.TrangThai == "Đã duyệt") trangThaiText = "✅ " + trangThaiText;
                        else if (yc.TrangThai == "Từ chối") trangThaiText = "❌ " + trangThaiText;
                        row.Cells["colTrangThai"].Value = trangThaiText;
                        row.Cells["colTrangThai"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        row.Cells["colTrangThai"].Style.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachYeuCau();
        }

        private void rbTatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTatCa.Checked) LoadDanhSachYeuCau();
        }

        private void rbChoDuyet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbChoDuyet.Checked) LoadDanhSachYeuCau();
        }

        private void rbDaDuyet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDaDuyet.Checked) LoadDanhSachYeuCau();
        }

        private void rbTuChoi_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTuChoi.Checked) LoadDanhSachYeuCau();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvYeuCau.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu cần xem.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            YeuCauChuyenLopDTO yeuCau = dgvYeuCau.SelectedRows[0].DataBoundItem as YeuCauChuyenLopDTO;
            if (yeuCau == null) return;

            ShowChiTietYeuCau(yeuCau);
        }

        private void ShowChiTietYeuCau(YeuCauChuyenLopDTO yeuCau)
        {
            string message = $"📋 CHI TIẾT YÊU CẦU CHUYỂN LỚP\n\n" +
                $"Mã yêu cầu: {yeuCau.MaYeuCau}\n" +
                $"Trạng thái: {yeuCau.TrangThai}\n\n" +
                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                $"👤 Học sinh: {yeuCau.TenHocSinh}\n" +
                $"📤 Từ lớp: {yeuCau.TenLopHienTai}\n" +
                $"📥 Lớp mong muốn: {yeuCau.TenLopMongMuon ?? "Để admin quyết định"}\n" +
                $"📚 Học kỳ: {yeuCau.TenHocKy} - {yeuCau.TenNamHoc}\n\n" +
                $"📝 Lý do:\n{yeuCau.LyDoYeuCau}\n\n" +
                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                $"🕐 Ngày tạo: {yeuCau.NgayTao:dd/MM/yyyy HH:mm}\n" +
                $"👤 Người tạo: {yeuCau.NguoiTao}\n";

            if (yeuCau.TrangThai != "Chờ duyệt")
            {
                message += $"\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                    $"🕐 Ngày xử lý: {yeuCau.NgayXuLy?.ToString("dd/MM/yyyy HH:mm") ?? "N/A"}\n" +
                    $"👤 Người xử lý: {yeuCau.NguoiXuLy ?? "N/A"}\n";

                if (yeuCau.TrangThai == "Đã duyệt")
                {
                    message += $"✅ Lớp được duyệt: {yeuCau.TenLopDuocDuyet}\n";
                }

                if (!string.IsNullOrWhiteSpace(yeuCau.GhiChuAdmin))
                {
                    message += $"\n💬 Ghi chú admin:\n{yeuCau.GhiChuAdmin}\n";
                }
            }

            MessageBox.Show(message, "Chi tiết yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (dgvYeuCau.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu cần duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            YeuCauChuyenLopDTO yeuCau = dgvYeuCau.SelectedRows[0].DataBoundItem as YeuCauChuyenLopDTO;
            if (yeuCau == null) return;

            if (yeuCau.TrangThai != "Chờ duyệt")
            {
                MessageBox.Show("Chỉ có thể duyệt yêu cầu đang chờ duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form duyệt yêu cầu
            FormDuyetYeuCau formDuyet = new FormDuyetYeuCau(yeuCau, tenDangNhapAdmin);
            if (formDuyet.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachYeuCau();
                MessageBox.Show("✅ Đã duyệt yêu cầu và chuyển lớp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (dgvYeuCau.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu cần từ chối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            YeuCauChuyenLopDTO yeuCau = dgvYeuCau.SelectedRows[0].DataBoundItem as YeuCauChuyenLopDTO;
            if (yeuCau == null) return;

            if (yeuCau.TrangThai != "Chờ duyệt")
            {
                MessageBox.Show("Chỉ có thể từ chối yêu cầu đang chờ duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nhập lý do từ chối
            string lyDoTuChoi = Microsoft.VisualBasic.Interaction.InputBox(
                "Vui lòng nhập lý do từ chối:\n\nVí dụ: Lớp không còn chỗ, Lý do không hợp lý, v.v.",
                "Từ chối yêu cầu",
                "",
                -1, -1);

            if (string.IsNullOrWhiteSpace(lyDoTuChoi))
            {
                MessageBox.Show("Vui lòng nhập lý do từ chối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Xác nhận từ chối yêu cầu chuyển lớp:\n\n" +
                $"📌 Học sinh: {yeuCau.TenHocSinh}\n" +
                $"📤 Từ lớp: {yeuCau.TenLopHienTai}\n" +
                $"📝 Lý do từ chối: {lyDoTuChoi}\n\n" +
                $"Bạn có chắc chắn?",
                "Xác nhận từ chối",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool thanhCong = yeuCauBLL.TuChoiYeuCau(yeuCau.MaYeuCau, tenDangNhapAdmin, lyDoTuChoi);
                    if (thanhCong)
                    {
                        LoadDanhSachYeuCau();
                        MessageBox.Show("✅ Đã từ chối yêu cầu chuyển lớp!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không thể từ chối yêu cầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvYeuCau.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            YeuCauChuyenLopDTO yeuCau = dgvYeuCau.SelectedRows[0].DataBoundItem as YeuCauChuyenLopDTO;
            if (yeuCau == null) return;

            var confirm = MessageBox.Show(
                $"Xác nhận xóa yêu cầu:\n\n" +
                $"📌 Mã yêu cầu: {yeuCau.MaYeuCau}\n" +
                $"👤 Học sinh: {yeuCau.TenHocSinh}\n" +
                $"📊 Trạng thái: {yeuCau.TrangThai}\n\n" +
                $"Bạn có chắc chắn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool thanhCong = yeuCauBLL.XoaYeuCau(yeuCau.MaYeuCau);
                    if (thanhCong)
                    {
                        LoadDanhSachYeuCau();
                        MessageBox.Show("✅ Đã xóa yêu cầu!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa yêu cầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvYeuCau_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnXemChiTiet_Click(sender, e);
            }
        }

        /// <summary>
        /// ✅ Ẩn các cột không cần thiết, chỉ hiển thị cột quan trọng
        /// </summary>
        private void AnCacCotKhongCanThiet()
        {
            if (dgvYeuCau.Columns.Count == 0) return;

            // Danh sách các cột CẦN HIỂN THỊ
            string[] cotCanHienThi = {
                "MaYeuCau",           // Mã YC
                "TenHocSinh",          // Học sinh
                "TenLopHienTai",       // Lớp hiện tại
                "TenLopMongMuon",      // Lớp mong muốn
                "LyDoYeuCau",          // Lý do
                "TrangThai",           // Trạng thái
                "NgayTao",             // Ngày tạo
                "NguoiTao"             // Người tạo
            };

            // Ẩn tất cả các cột trước
            foreach (DataGridViewColumn col in dgvYeuCau.Columns)
            {
                col.Visible = false;
            }

            // Chỉ hiển thị các cột cần thiết và đặt tên header đẹp
            int displayIndex = 0;
            if (dgvYeuCau.Columns["MaYeuCau"] != null)
            {
                dgvYeuCau.Columns["MaYeuCau"].Visible = true;
                dgvYeuCau.Columns["MaYeuCau"].HeaderText = "Mã YC";
                dgvYeuCau.Columns["MaYeuCau"].Width = 80;
                dgvYeuCau.Columns["MaYeuCau"].DisplayIndex = displayIndex++;
            }
            
            if (dgvYeuCau.Columns["NgayTao"] != null)
            {
                dgvYeuCau.Columns["NgayTao"].Visible = true;
                dgvYeuCau.Columns["NgayTao"].HeaderText = "📅 Ngày tạo";
                dgvYeuCau.Columns["NgayTao"].Width = 130;
                dgvYeuCau.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvYeuCau.Columns["NgayTao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvYeuCau.Columns["NgayTao"].DisplayIndex = displayIndex++;
            }
            
            if (dgvYeuCau.Columns["TenHocSinh"] != null)
            {
                dgvYeuCau.Columns["TenHocSinh"].Visible = true;
                dgvYeuCau.Columns["TenHocSinh"].HeaderText = "👤 Học sinh";
                dgvYeuCau.Columns["TenHocSinh"].Width = 180;
                dgvYeuCau.Columns["TenHocSinh"].DisplayIndex = displayIndex++;
            }
            
            if (dgvYeuCau.Columns["TenLopHienTai"] != null)
            {
                dgvYeuCau.Columns["TenLopHienTai"].Visible = true;
                dgvYeuCau.Columns["TenLopHienTai"].HeaderText = "📤 Lớp hiện tại";
                dgvYeuCau.Columns["TenLopHienTai"].Width = 120;
                dgvYeuCau.Columns["TenLopHienTai"].DisplayIndex = displayIndex++;
            }
            
            if (dgvYeuCau.Columns["TenLopMongMuon"] != null)
            {
                dgvYeuCau.Columns["TenLopMongMuon"].Visible = true;
                dgvYeuCau.Columns["TenLopMongMuon"].HeaderText = "📥 Lớp mong muốn";
                dgvYeuCau.Columns["TenLopMongMuon"].Width = 140;
                dgvYeuCau.Columns["TenLopMongMuon"].DisplayIndex = displayIndex++;
            }
            
            if (dgvYeuCau.Columns["LyDoYeuCau"] != null)
            {
                dgvYeuCau.Columns["LyDoYeuCau"].Visible = true;
                dgvYeuCau.Columns["LyDoYeuCau"].HeaderText = "📝 Lý do";
                dgvYeuCau.Columns["LyDoYeuCau"].Width = 250;
                dgvYeuCau.Columns["LyDoYeuCau"].DisplayIndex = displayIndex++;
            }
            
            if (dgvYeuCau.Columns["TrangThai"] != null)
            {
                dgvYeuCau.Columns["TrangThai"].Visible = true;
                dgvYeuCau.Columns["TrangThai"].HeaderText = "🏷️ Trạng thái";
                dgvYeuCau.Columns["TrangThai"].Width = 130;
                dgvYeuCau.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvYeuCau.Columns["TrangThai"].DisplayIndex = displayIndex++;
            }
            
            if (dgvYeuCau.Columns["NguoiTao"] != null)
            {
                dgvYeuCau.Columns["NguoiTao"].Visible = true;
                dgvYeuCau.Columns["NguoiTao"].HeaderText = "👨‍💼 Người tạo";
                dgvYeuCau.Columns["NguoiTao"].Width = 120;
                dgvYeuCau.Columns["NguoiTao"].DisplayIndex = displayIndex++;
            }

            // ✅ Đặt AutoSizeColumnsMode cho cột Lý do để tự động mở rộng
            if (dgvYeuCau.Columns["LyDoYeuCau"] != null)
            {
                dgvYeuCau.Columns["LyDoYeuCau"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}

