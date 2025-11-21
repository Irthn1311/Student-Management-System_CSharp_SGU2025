using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FormChuyenLop : Form
    {
        private int maHocSinh;
        private int maLopCu;
        private int maHocKy;
        private string tenHocSinh;
        private string tenLopCu;
        
        private LopHocBUS lopHocBUS;
        private HocKyBUS hocKyBUS;
        private NamHocBUS namHocBUS;

        public int MaLopMoi { get; private set; }
        public string LyDo { get; private set; }

        public FormChuyenLop(int maHocSinh, int maLopCu, int maHocKy, string tenHocSinh, string tenLopCu)
        {
            InitializeComponent();
            this.maHocSinh = maHocSinh;
            this.maLopCu = maLopCu;
            this.maHocKy = maHocKy;
            this.tenHocSinh = tenHocSinh;
            this.tenLopCu = tenLopCu;
            
            lopHocBUS = new LopHocBUS();
            hocKyBUS = new HocKyBUS();
            namHocBUS = new NamHocBUS();
            
            MaLopMoi = 0;
            LyDo = "";
        }

        private void FormChuyenLop_Load(object sender, EventArgs e)
        {
            LoadThongTin();
            LoadDanhSachLop();
        }

        private void LoadThongTin()
        {
            try
            {
                lblHocSinh.Text = $"Học sinh: {tenHocSinh} (Mã: {maHocSinh})";
                lblLopCu.Text = $"Lớp hiện tại: {tenLopCu}";
                
                // Lấy thông tin học kỳ
                var hocKy = hocKyBUS.LayHocKyTheoMa(maHocKy);
                if (hocKy != null)
                {
                    var namHoc = namHocBUS.LayNamHocTheoMa(hocKy.MaNamHoc);
                    lblHocKy.Text = $"Học kỳ: {hocKy.TenHocKy} - {namHoc?.TenNamHoc ?? hocKy.MaNamHoc}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachLop()
        {
            try
            {
                cbLopMoi.Items.Clear();
                cbLopMoi.Items.Add("-- Chọn lớp mới --");

                // Lấy danh sách tất cả lớp (trừ lớp hiện tại)
                var dsLop = lopHocBUS.DocDSLop();
                
                if (dsLop == null || dsLop.Count == 0)
                {
                    MessageBox.Show("Không có lớp nào để chuyển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Lọc bỏ lớp hiện tại và sắp xếp theo khối, tên lớp
                var dsLopFiltered = dsLop
                    .Where(l => l.maLop != maLopCu)
                    .OrderBy(l => l.maKhoi)
                    .ThenBy(l => l.tenLop)
                    .ToList();

                foreach (var lop in dsLopFiltered)
                {
                    string displayText = $"{lop.tenLop} (Khối {lop.maKhoi})";
                    cbLopMoi.Items.Add(new ComboBoxItem 
                    { 
                        Text = displayText, 
                        Value = lop.maLop 
                    });
                }

                cbLopMoi.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn lớp mới
                if (cbLopMoi.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng chọn lớp mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã lớp mới
                ComboBoxItem selectedItem = (ComboBoxItem)cbLopMoi.SelectedItem;
                MaLopMoi = (int)selectedItem.Value;

                // Lấy lý do (nếu có)
                LyDo = txtLyDo.Text.Trim();

                // Xác nhận
                string tenLopMoi = selectedItem.Text;
                string message = $"Bạn có chắc chắn muốn chuyển học sinh {tenHocSinh} từ lớp {tenLopCu} sang lớp {tenLopMoi}?";
                
                if (!string.IsNullOrEmpty(LyDo))
                {
                    message += $"\n\nLý do: {LyDo}";
                }

                if (MessageBox.Show(message, "Xác nhận chuyển lớp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Helper class cho ComboBox
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
