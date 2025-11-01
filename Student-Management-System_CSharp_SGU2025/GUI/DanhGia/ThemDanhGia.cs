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
using static Student_Management_System_CSharp_SGU2025.GUI.DiemSo_NhapDiem;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ThemDanhGia : Form
    {
        private HocKyDAO hocKyDAO = new HocKyDAO();
        private LopDAO lopDAO = new LopDAO();
        private HocSinhDAO hocSinhDAO = new HocSinhDAO();
        private PhanLopDAO phanLopDAO = new PhanLopDAO(); // Giả sử bạn có DAO này để lấy học sinh theo lớp và học kỳ
        private KhenThuongKyLuatDAO ktklDAO = new KhenThuongKyLuatDAO();
        private KhenThuongKyLuatBUS ktklBUS = new KhenThuongKyLuatBUS();
        private bool isEditMode = false;
        private int editMaKTKL = 0;
        public ThemDanhGia()
        {
            InitializeComponent();
            isEditMode = false;
        }

        // Constructor cho chế độ sửa
        public ThemDanhGia(int maKTKL) : this()
        {
            isEditMode = true;
            editMaKTKL = maKTKL;
        }

        private void ThemKhenThuong_Load(object sender, EventArgs e)
        {
            cbLoaiDanhGia.Items.Add("Chọn loại đánh giá");
            cbLoaiDanhGia.Items.Add("Khen thưởng");
            cbLoaiDanhGia.Items.Add("Kỷ luật");
            cbLoaiDanhGia.SelectedIndex = 0;
            LoadHocKy();
            LoadLop();

            // Ẩn 2 combobox phụ lúc khởi tạo
            cbCapKhenThuong.Visible = false;
            cbMucXuLy.Visible = false;
            ngayApDung.Value = DateTime.Now;

            // Nếu là chế độ sửa, load dữ liệu và lock các control
            if (isEditMode)
            {
                label1.Text = "Sửa đánh giá";
                LoadDataForEdit();
                LockControlsForEdit();
            }
            else
            {
                label1.Text = "Thêm đánh giá";
            }

        }

        private void LoadDataForEdit()
        {
            try
            {
                KhenThuongKyLuatDTO kt = ktklDAO.LayTheoMa(editMaKTKL);
                if (kt == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                HocSinhDTO hs = hocSinhDAO.TimHocSinhTheoMa(kt.MaHocSinh);
                if (hs == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin học sinh!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Lấy tất cả phân lớp của học sinh
                var dsPhanLop = phanLopDAO.LayTatCaPhanLop();
                var phanLopHS = dsPhanLop.Where(pl => pl.maHocSinh == kt.MaHocSinh).FirstOrDefault();

                if (phanLopHS != default)
                {
                    // Set Học kỳ
                    for (int i = 0; i < cbHocKy.Items.Count; i++)
                    {
                        ComboBoxItem item = (ComboBoxItem)cbHocKy.Items[i];
                        if (Convert.ToInt32(item.Value) == phanLopHS.maHocKy)
                        {
                            cbHocKy.SelectedIndex = i;
                            break;
                        }
                    }

                    // Set Lớp
                    for (int i = 0; i < cbLop.Items.Count; i++)
                    {
                        ComboBoxItem item = (ComboBoxItem)cbLop.Items[i];
                        if (Convert.ToInt32(item.Value) == phanLopHS.maLop)
                        {
                            cbLop.SelectedIndex = i;
                            break;
                        }
                    }

                    // Load danh sách học sinh của lớp đó
                    LoadHocSinhTheoLopVaHocKy(phanLopHS.maLop, phanLopHS.maHocKy);
                }

                // Set Học sinh
                for (int i = 0; i < cbHocSinh.Items.Count; i++)
                {
                    ComboBoxItem item = (ComboBoxItem)cbHocSinh.Items[i];
                    if (Convert.ToInt32(item.Value) == kt.MaHocSinh)
                    {
                        cbHocSinh.SelectedIndex = i;
                        break;
                    }
                }

                // Set Loại đánh giá
                if (kt.Loai == "Khen thưởng")
                {
                    cbLoaiDanhGia.SelectedIndex = 1;
                    cbCapKhenThuong.Visible = true;
                    cbMucXuLy.Visible = false;

                    // Set Cấp khen thưởng
                    if (!string.IsNullOrEmpty(kt.CapKhenThuong))
                    {
                        int capKhenIndex = cbCapKhenThuong.FindStringExact(kt.CapKhenThuong);
                        if (capKhenIndex >= 0)
                            cbCapKhenThuong.SelectedIndex = capKhenIndex;
                    }
                }
                else if (kt.Loai == "Kỷ luật")
                {
                    cbLoaiDanhGia.SelectedIndex = 2;
                    cbCapKhenThuong.Visible = false;
                    cbMucXuLy.Visible = true;

                    // Set Mức xử lý
                    if (!string.IsNullOrEmpty(kt.MucXuLy))
                    {
                        int mucXuLyIndex = cbMucXuLy.FindStringExact(kt.MucXuLy);
                        if (mucXuLyIndex >= 0)
                            cbMucXuLy.SelectedIndex = mucXuLyIndex;
                    }
                }

                // Set nội dung và ngày áp dụng
                txtNoiDung.Text = kt.NoiDung;
                ngayApDung.Value = kt.NgayApDung;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // Method mới: Lock các control không được sửa
        private void LockControlsForEdit()
        {
            cbHocKy.Enabled = false;
            cbLop.Enabled = false;
            cbHocSinh.Enabled = false;
            cbLoaiDanhGia.Enabled = false;

            // Các control được phép sửa
            txtNoiDung.Enabled = true;
            cbCapKhenThuong.Enabled = true;
            cbMucXuLy.Enabled = true;
            ngayApDung.Enabled = true;
        }

        private void LoadHocKy()
        {
            try
            {
                cbHocKy.Items.Clear();
                List<HocKyDTO> dsHocKy = hocKyDAO.GetAllHocKy();

                foreach (HocKyDTO hk in dsHocKy)
                {
                    // Định dạng: manamhoc-tenhocky
                    string displayText = $"{hk.MaNamHoc}-{hk.TenHocKy}";
                    cbHocKy.Items.Add(new ComboBoxItem { Text = displayText, Value = hk.MaHocKy });
                }

                if (cbHocKy.Items.Count > 0 && !isEditMode)
                {
                    cbHocKy.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học kỳ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLop()
        {
            try
            {
                cbLop.Items.Clear();
                List<LopDTO> dsLop = lopDAO.GetDanhSachLopCoHocSinh();

                foreach (LopDTO lop in dsLop)
                {
                    cbLop.Items.Add(new ComboBoxItem { Text = lop.TenLop, Value = lop.MaLop });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHocSinhTheoLopVaHocKy(int maLop, int maHocKy)
        {
            try
            {
                cbHocSinh.Items.Clear();
                cbHocSinh.Enabled = false;

                // Sử dụng hàm LayDanhSachHocSinhTrongLop từ PhanLopDAO
                List<HocSinhDTO> dsHocSinh = phanLopDAO.LayDanhSachHocSinhTrongLop(maLop, maHocKy);

                foreach (HocSinhDTO hs in dsHocSinh)
                {
                    // Định dạng: mahocsinh-hoten
                    string displayText = $"{hs.MaHS}-{hs.HoTen}";
                    cbHocSinh.Items.Add(new ComboBoxItem { Text = displayText, Value = hs.MaHS });
                }

                if (cbHocSinh.Items.Count > 0)
                {
                    cbHocSinh.Enabled = true;
                    if (!isEditMode)
                    {
                        cbHocSinh.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Không có học sinh nào trong lớp này ở học kỳ đã chọn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học sinh: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbMucXuLy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbLoaiDanhGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cbLoaiDanhGia.SelectedItem.ToString();

            if (selectedValue == "Khen thưởng")
            {
                cbCapKhenThuong.Visible = true;
                cbMucXuLy.Visible = false;

            }
            else if (selectedValue == "Kỷ luật")
            {
                cbCapKhenThuong.Visible = false;
                cbMucXuLy.Visible = true;

            }
            else // "Chọn loại đánh giá"
            {
                cbCapKhenThuong.Visible = false;
                cbMucXuLy.Visible = false;

            }
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLop.SelectedIndex >= 0 && cbHocKy.SelectedIndex >= 0)
            {
                ComboBoxItem selectedLop = (ComboBoxItem)cbLop.SelectedItem;
                ComboBoxItem selectedHocKy = (ComboBoxItem)cbHocKy.SelectedItem;

                int maLop = Convert.ToInt32(selectedLop.Value);
                int maHocKy = Convert.ToInt32(selectedHocKy.Value);

                LoadHocSinhTheoLopVaHocKy(maLop, maHocKy);
            }
        }

        private void cbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKy.SelectedIndex >= 0 && !isEditMode)
            {
                cbLop.Enabled = true;
                cbLop.SelectedIndex = -1;
                cbHocSinh.Items.Clear();
                cbHocSinh.Enabled = false;
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra validation
                if (!ValidateInput())
                {
                    return;
                }

                if (isEditMode)
                {
                    // Chế độ sửa
                    UpdateKhenThuongKyLuat();
                }
                else
                {
                    // Chế độ thêm mới
                    AddNewKhenThuongKyLuat();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void AddNewKhenThuongKyLuat()
        {
            ComboBoxItem selectedHocSinh = (ComboBoxItem)cbHocSinh.SelectedItem;
            int maHocSinh = Convert.ToInt32(selectedHocSinh.Value);
            string loai = cbLoaiDanhGia.SelectedItem.ToString();
            string noiDung = txtNoiDung.Text.Trim();
            DateTime ngayApDungValue = ngayApDung.Value;

            string capKhenThuong = null;
            string mucXuLy = null;

            if (loai == "Khen thưởng")
            {
                string selectedCapKhen = cbCapKhenThuong.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedCapKhen) && selectedCapKhen != "Cấp khen thưởng")
                {
                    capKhenThuong = selectedCapKhen;
                }
            }
            else if (loai == "Kỷ luật")
            {
                string selectedMucXuLy = cbMucXuLy.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedMucXuLy) && selectedMucXuLy != "Mức xử lý")
                {
                    mucXuLy = selectedMucXuLy;
                }
            }

            var result = ktklBUS.ThemKhenThuongKyLuat(
                maHocSinh, loai, noiDung, capKhenThuong, mucXuLy, ngayApDungValue);

            MessageBox.Show(result.Message, result.Success ? "Thông báo" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void UpdateKhenThuongKyLuat()
        {
            string loai = cbLoaiDanhGia.SelectedItem.ToString();
            string noiDung = txtNoiDung.Text.Trim();
            DateTime ngayApDungValue = ngayApDung.Value;

            string capKhenThuong = null;
            string mucXuLy = null;

            if (loai == "Khen thưởng")
            {
                string selectedCapKhen = cbCapKhenThuong.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedCapKhen) && selectedCapKhen != "Cấp khen thưởng")
                {
                    capKhenThuong = selectedCapKhen;
                }
            }
            else if (loai == "Kỷ luật")
            {
                string selectedMucXuLy = cbMucXuLy.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedMucXuLy) && selectedMucXuLy != "Mức xử lý")
                {
                    mucXuLy = selectedMucXuLy;
                }
            }

            var result = ktklBUS.CapNhatKhenThuongKyLuat(
                editMaKTKL, loai, noiDung, capKhenThuong, mucXuLy, ngayApDungValue);

            MessageBox.Show(result.Message, result.Success ? "Thông báo" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    

        private bool ValidateInput()
        {
            var validation = ktklBUS.ValidateKhenThuongKyLuat(
                cbLoaiDanhGia.SelectedIndex,
                cbHocKy.SelectedIndex,
                cbLop.SelectedIndex,
                cbHocSinh.SelectedIndex,
                txtNoiDung.Text,
                cbLoaiDanhGia.SelectedItem?.ToString(),
                cbCapKhenThuong.SelectedIndex,
                cbCapKhenThuong.SelectedItem?.ToString(),
                cbMucXuLy.SelectedIndex,
                cbMucXuLy.SelectedItem?.ToString(),
                isEditMode
            );

            if (!validation.IsValid)
            {
                MessageBox.Show(validation.ErrorMessage, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Focus vào field có lỗi
                switch (validation.FieldName)
                {
                    case "cbLoaiDanhGia": cbLoaiDanhGia.Focus(); break;
                    case "cbHocKy": cbHocKy.Focus(); break;
                    case "cbLop": cbLop.Focus(); break;
                    case "cbHocSinh": cbHocSinh.Focus(); break;
                    case "txtNoiDung": txtNoiDung.Focus(); break;
                    case "cbCapKhenThuong": cbCapKhenThuong.Focus(); break;
                    case "cbMucXuLy": cbMucXuLy.Focus(); break;
                }

                return false;
            }

            return true;
        }

        // Thêm class này vào cuối file, bên ngoài class ThemDanhGia
        public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

}
}
