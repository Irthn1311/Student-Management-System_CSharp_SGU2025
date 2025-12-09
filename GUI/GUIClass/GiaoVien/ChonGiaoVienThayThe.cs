using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ChonGiaoVienThayThe : Form
    {
        private GiaoVienBUS giaoVienBUS;
        private PhanCongGiangDayBUS phanCongBUS;
        private MonHocBUS monHocBUS;
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;

        private string maGiaoVien;
        private GiaoVienDTO giaoVien;
        private List<PhanCongGiangDayDTO> danhSachPhanCong;
        private Dictionary<int, string> danhSachGiaoVienThayThe; // MaPhanCong -> MaGiaoVienThayThe

        public Dictionary<int, string> DanhSachGiaoVienThayThe 
        { 
            get { return danhSachGiaoVienThayThe; } 
        }

        public ChonGiaoVienThayThe(string maGiaoVien)
        {
            InitializeComponent();
            this.maGiaoVien = maGiaoVien;
            danhSachGiaoVienThayThe = new Dictionary<int, string>();

            giaoVienBUS = new GiaoVienBUS();
            phanCongBUS = new PhanCongGiangDayBUS();
            monHocBUS = new MonHocBUS();
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();

            LoadThongTin();
        }

        private void LoadThongTin()
        {
            try
            {
                // Load thông tin giáo viên
                giaoVien = giaoVienBUS.LayGiaoVienTheoMa(maGiaoVien);
                if (giaoVien == null)
                {
                    MessageBox.Show("Không tìm thấy giáo viên!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                lblThongBao.Text = $"Giáo viên {giaoVien.HoTen} ({giaoVien.MaGiaoVien}) đang phụ trách các phân công giảng dạy sau:";

                // Load danh sách phân công
                danhSachPhanCong = phanCongBUS.LayPhanCongTheoGiaoVien(maGiaoVien);
                var danhSachMonHoc = monHocBUS.DocDSMH();
                var danhSachLop = lopHocBUS.DocDSLop();
                var danhSachHocKy = hocKyBUS.DocDSHocKy();

                // Tạo DataGridView với combobox cho mỗi phân công
                dgvPhanCong.Rows.Clear();
                dgvPhanCong.Columns.Clear();

                // Tạo các cột
                dgvPhanCong.Columns.Add("MaPhanCong", "Mã PC");
                dgvPhanCong.Columns["MaPhanCong"].Visible = false;
                dgvPhanCong.Columns.Add("TenLop", "Lớp");
                dgvPhanCong.Columns.Add("TenMon", "Môn học");
                dgvPhanCong.Columns.Add("HocKy", "Học kỳ");
                
                // Tạo cột combobox cho giáo viên thay thế
                DataGridViewComboBoxColumn comboColumn = new DataGridViewComboBoxColumn();
                comboColumn.Name = "GiaoVienThayThe";
                comboColumn.HeaderText = "Giáo viên thay thế";
                comboColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                comboColumn.FlatStyle = FlatStyle.Flat;
                comboColumn.Width = 250;
                dgvPhanCong.Columns.Add(comboColumn);

                // Thêm dữ liệu
                foreach (var pc in danhSachPhanCong)
                {
                    string tenLop = danhSachLop.FirstOrDefault(l => l.maLop == pc.MaLop)?.tenLop ?? $"Lớp {pc.MaLop}";
                    string tenMon = danhSachMonHoc.FirstOrDefault(m => m.maMon == pc.MaMonHoc)?.tenMon ?? $"Môn {pc.MaMonHoc}";
                    string tenHocKy = danhSachHocKy.FirstOrDefault(hk => hk.MaHocKy == pc.MaHocKy)?.TenHocKy ?? $"HK {pc.MaHocKy}";

                    int rowIndex = dgvPhanCong.Rows.Add(
                        pc.MaPhanCong,
                        tenLop,
                        tenMon,
                        tenHocKy,
                        null // Giáo viên thay thế sẽ được set sau
                    );

                    // Lấy danh sách giáo viên thay thế cho môn học này
                    var danhSachGVThayThe = giaoVienBUS.LayDanhSachGiaoVienThayThe(pc.MaMonHoc, maGiaoVien);
                    
                    // Lấy cell combobox sau khi row đã được thêm
                    DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dgvPhanCong.Rows[rowIndex].Cells["GiaoVienThayThe"];
                    
                    if (danhSachGVThayThe.Count == 0)
                    {
                        comboCell.Items.Add("KHÔNG CÓ GIÁO VIÊN THAY THẾ");
                        comboCell.Value = "KHÔNG CÓ GIÁO VIÊN THAY THẾ";
                        // Đặt ReadOnly SAU KHI cell đã được thêm vào row
                        comboCell.ReadOnly = true;
                        comboCell.Style.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        foreach (var gv in danhSachGVThayThe)
                        {
                            comboCell.Items.Add($"{gv.MaGiaoVien} - {gv.HoTen}");
                        }
                        // Chọn giáo viên đầu tiên làm mặc định
                        if (danhSachGVThayThe.Count > 0)
                        {
                            string defaultValue = $"{danhSachGVThayThe[0].MaGiaoVien} - {danhSachGVThayThe[0].HoTen}";
                            comboCell.Value = defaultValue;
                            danhSachGiaoVienThayThe[pc.MaPhanCong] = danhSachGVThayThe[0].MaGiaoVien;
                        }
                    }
                }

                // Thiết lập sự kiện khi chọn giáo viên thay thế
                dgvPhanCong.CellValueChanged += DgvPhanCong_CellValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPhanCong_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPhanCong.Columns["GiaoVienThayThe"].Index && e.RowIndex >= 0)
            {
                string selectedValue = dgvPhanCong.Rows[e.RowIndex].Cells["GiaoVienThayThe"].Value?.ToString();
                if (!string.IsNullOrEmpty(selectedValue) && !selectedValue.Contains("KHÔNG CÓ"))
                {
                    // Lấy MaGiaoVien từ chuỗi "MaGV - HoTen"
                    string maGV = selectedValue.Split('-')[0].Trim();
                    int maPhanCong = Convert.ToInt32(dgvPhanCong.Rows[e.RowIndex].Cells["MaPhanCong"].Value);
                    danhSachGiaoVienThayThe[maPhanCong] = maGV;
                }
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra tất cả phân công đều đã chọn giáo viên thay thế
            List<string> phanCongThieuGV = new List<string>();
            
            foreach (DataGridViewRow row in dgvPhanCong.Rows)
            {
                int maPhanCong = Convert.ToInt32(row.Cells["MaPhanCong"].Value);
                string selectedValue = row.Cells["GiaoVienThayThe"].Value?.ToString();

                if (string.IsNullOrEmpty(selectedValue) || selectedValue.Contains("KHÔNG CÓ"))
                {
                    string tenLop = row.Cells["TenLop"].Value?.ToString();
                    string tenMon = row.Cells["TenMon"].Value?.ToString();
                    phanCongThieuGV.Add($"{tenLop} - {tenMon}");
                }
                else
                {
                    // Đảm bảo dictionary được cập nhật với giá trị hiện tại
                    if (!selectedValue.Contains("KHÔNG CÓ"))
                    {
                        string maGV = selectedValue.Split('-')[0].Trim();
                        danhSachGiaoVienThayThe[maPhanCong] = maGV;
                    }
                }
            }

            if (phanCongThieuGV.Count > 0)
            {
                string message = "Không thể xóa giáo viên vì các phân công sau không có giáo viên thay thế:\n\n";
                message += string.Join("\n", phanCongThieuGV);
                message += "\n\nVui lòng thêm giáo viên cùng chuyên môn và chưa có phân công giảng dạy để thay thế.";
                MessageBox.Show(message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra dictionary có đủ dữ liệu không
            if (danhSachGiaoVienThayThe.Count != danhSachPhanCong.Count)
            {
                MessageBox.Show("Có lỗi xảy ra khi lấy thông tin giáo viên thay thế. Vui lòng thử lại.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
