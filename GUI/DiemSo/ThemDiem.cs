using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.GUI.DiemSo
{
    public partial class ThemDiem : Form
    {
        private ThemDiemBUS themDiemBUS;
        private bool isEditMode = false;
        private string editMaHocSinh;
        private int editMaMonHoc;
        private int editMaHocKy;
        public string MaHocSinhVuaThem { get; private set; }

        public ThemDiem()
        {
            InitializeComponent();
            themDiemBUS = new ThemDiemBUS();
            isEditMode = false;

        }

        public ThemDiem(string maHocSinh, string hoTen, int? maLop, string tenLop,
                int maMonHoc, string tenMonHoc, int maHocKy, string tenHocKy,
                float? diemTX, float? diemGK, float? diemCK)
        {
            InitializeComponent();
            themDiemBUS = new ThemDiemBUS();
            isEditMode = true;

            // Lưu thông tin để sửa
            editMaHocSinh = maHocSinh;
            editMaMonHoc = maMonHoc;
            editMaHocKy = maHocKy;

            // Load form với dữ liệu
            this.Load += (s, e) => {
                // Đổi tiêu đề
                lblThemDiem.Text = "Sửa Điểm";

                // Load và chọn dữ liệu
                LoadDataForEdit(maHocSinh, hoTen, maLop, tenLop, maMonHoc, tenMonHoc,
                               maHocKy, tenHocKy, diemTX, diemGK, diemCK);
            };
        }

        private void LoadDataForEdit(string maHocSinh, string hoTen, int? maLop, string tenLop,
                               int maMonHoc, string tenMonHoc, int maHocKy, string tenHocKy,
                               float? diemTX, float? diemGK, float? diemCK)
        {
            try
            {
                // Tạm thời tắt event để tránh trigger
                cbLop.SelectedIndexChanged -= cbLop_SelectedIndexChanged;
                cbHocSinh.SelectedIndexChanged -= cbHocSinh_SelectedIndexChanged;
                cbHocKy.SelectedIndexChanged -= cbHocKy_SelectedIndexChanged;
                cbMonHoc.SelectedIndexChanged -= cbMonHoc_SelectedIndexChanged;

                // Load danh sách lớp
                LoadDanhSachLop();

                // Chọn lớp theo MaLop
                if (maLop.HasValue && maLop.Value > 0)
                {
                    for (int i = 0; i < cbLop.Items.Count; i++)
                    {
                        var item = cbLop.Items[i];
                        // Sử dụng reflection để lấy giá trị MaLop
                        var maLopProperty = item.GetType().GetProperty("MaLop");
                        if (maLopProperty != null)
                        {
                            int itemMaLop = (int)maLopProperty.GetValue(item);
                            if (itemMaLop == maLop.Value)
                            {
                                cbLop.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }

                // Load học sinh theo lớp
                if (maLop.HasValue && maLop.Value > 0)
                {
                    LoadDanhSachHocSinh(maLop.Value);
                }

                // Chọn học sinh theo MaHocSinh
                for (int i = 0; i < cbHocSinh.Items.Count; i++)
                {
                    var item = cbHocSinh.Items[i];
                    if (item.ToString() == "-- Chọn học sinh --") continue;

                    // Sử dụng reflection để lấy Value
                    var valueProperty = item.GetType().GetProperty("Value");
                    if (valueProperty != null)
                    {
                        string itemMaHS = valueProperty.GetValue(item)?.ToString();
                        if (itemMaHS == maHocSinh)
                        {
                            cbHocSinh.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Load và chọn học kỳ
                LoadDanhSachHocKy();
                for (int i = 0; i < cbHocKy.Items.Count; i++)
                {
                    var item = cbHocKy.Items[i];
                    if (item.ToString() == "-- Chọn học kỳ --") continue;

                    var valueProperty = item.GetType().GetProperty("Value");
                    if (valueProperty != null)
                    {
                        object valueObj = valueProperty.GetValue(item);
                        if (valueObj != null && int.TryParse(valueObj.ToString(), out int itemMaHK))
                        {
                            if (itemMaHK == maHocKy)
                            {
                                cbHocKy.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }

                // Load và chọn môn học
                LoadDanhSachMonHoc();
                for (int i = 0; i < cbMonHoc.Items.Count; i++)
                {
                    var item = cbMonHoc.Items[i];
                    if (item.ToString() == "-- Chọn môn học --") continue;

                    var valueProperty = item.GetType().GetProperty("Value");
                    if (valueProperty != null)
                    {
                        string valueStr = valueProperty.GetValue(item)?.ToString();
                        if (!string.IsNullOrEmpty(valueStr) && int.TryParse(valueStr, out int itemMaMH))
                        {
                            if (itemMaMH == maMonHoc)
                            {
                                cbMonHoc.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }

                // Điền điểm
                if (diemTX.HasValue) txtDiemTX.Text = diemTX.Value.ToString("0.0");
                if (diemGK.HasValue) txtDiemGK.Text = diemGK.Value.ToString("0.0");
                if (diemCK.HasValue) txtDiemCK.Text = diemCK.Value.ToString("0.0");

                // Bật lại event
                cbLop.SelectedIndexChanged += cbLop_SelectedIndexChanged;
                cbHocSinh.SelectedIndexChanged += cbHocSinh_SelectedIndexChanged;
                cbHocKy.SelectedIndexChanged += cbHocKy_SelectedIndexChanged;
                cbMonHoc.SelectedIndexChanged += cbMonHoc_SelectedIndexChanged;

                // Lock các control
                cbLop.Enabled = false;
                cbHocSinh.Enabled = false;
                cbHocKy.Enabled = false;
                cbMonHoc.Enabled = false;
                txtDiemGK.Enabled = false;
                txtDiemCK.Enabled = false;
                txtDiemGK.ReadOnly = true;
                txtDiemCK.ReadOnly = true;

                // Chỉ cho sửa txtDiemTX
                txtDiemTX.Enabled = true;
                txtDiemTX.ReadOnly = false;
                txtDiemTX.Focus();
                btnLuu.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message + "\n\nStackTrace: " + ex.StackTrace,
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThemDiem_Load(object sender, EventArgs e)
        {
            try
            {
                // Load danh sách lớp
                LoadDanhSachLop();

                // Vô hiệu hóa các controls khác cho đến khi chọn lớp
                cbHocSinh.Enabled = false;
                cbHocKy.Enabled = false;
                cbMonHoc.Enabled = false;
                txtDiemTX.Enabled = false;
                txtDiemGK.Enabled = false;
                txtDiemCK.Enabled = false;
                btnLuu.Enabled = false;

                cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachLop()
        {
            try
            {
                List<LopDTO> danhSachLop = themDiemBUS.GetDanhSachLop();

                var listForBinding = new List<object>();
                listForBinding.Add(new { TenLop = "-- Chọn lớp học --", MaLop = -1 });
                foreach (var lop in danhSachLop)
                {
                    listForBinding.Add(new { TenLop = lop.TenLop, MaLop = lop.MaLop });
                }

                cbLop.DataSource = listForBinding;
                cbLop.DisplayMember = "TenLop";
                cbLop.ValueMember = "MaLop";
                cbLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadDanhSachHocSinh(int maLop)
        {
            try
            {
                // Get the selected hoc ky or use a default value
                int maHocKy = 1; // Default value, you might want to get this from UI
                if (cbHocKy.SelectedValue != null && int.TryParse(cbHocKy.SelectedValue.ToString(), out int selectedHocKy))
                {
                    maHocKy = selectedHocKy;
                }
                
                List<HocSinhDTO> danhSachHS = themDiemBUS.GetHocSinhTheoLop(maLop, maHocKy);

                cbHocSinh.DataSource = null;
                cbHocSinh.Items.Clear();
                cbHocSinh.Items.Add("-- Chọn học sinh --");

                foreach (var hs in danhSachHS)
                {
                    string displayText = $"{hs.MaHocSinh} - {hs.HoTen}";
                    cbHocSinh.Items.Add(new { Text = displayText, Value = hs.MaHocSinh });
                }

                cbHocSinh.DisplayMember = "Text";
                cbHocSinh.ValueMember = "Value";
                cbHocSinh.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học sinh: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachHocKy()
        {
            try
            {
                List<HocKyDTO> danhSachHK = themDiemBUS.GetDanhSachHocKy();

                cbHocKy.DataSource = null;
                cbHocKy.Items.Clear();
                cbHocKy.Items.Add("-- Chọn học kỳ --");

                foreach (var hk in danhSachHK)
                {
                    string displayHK = $"{hk.MaNamHoc} - {hk.TenHocKy}";
                    cbHocKy.Items.Add(new { Text = displayHK, Value = hk.MaHocKy });
                }

                cbHocKy.DisplayMember = "Text";
                cbHocKy.ValueMember = "Value";
                cbHocKy.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách học kỳ: " + ex.Message, "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadDanhSachMonHoc()
        {
            try
            {
                Console.WriteLine("Bắt đầu load môn học...");

                List<MonHocDTO> danhSachMH = themDiemBUS.GetDanhSachMonHoc();

                Console.WriteLine($"Đã lấy {danhSachMH?.Count ?? 0} môn học");

                cbMonHoc.DataSource = null;
                cbMonHoc.Items.Clear();
                cbMonHoc.Items.Add(new { Text = "-- Chọn môn học --", Value = "" });

                if (danhSachMH != null)
                {
                    foreach (var mh in danhSachMH)
                    {
                        Console.WriteLine($"Môn: {mh.tenMon} - Mã: {mh.maMon}");
                        cbMonHoc.Items.Add(new { Text = mh.tenMon, Value = mh.maMon });
                    }
                }

                cbMonHoc.DisplayMember = "Text";
                cbMonHoc.ValueMember = "Value";
                cbMonHoc.SelectedIndex = 0;

                Console.WriteLine("Load môn học thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LỖI: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                MessageBox.Show($"Lỗi khi tải danh sách môn học:\n{ex.Message}\n\nChi tiết:\n{ex.InnerException?.Message}",
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbLop.SelectedValue == null)
                    return;

                int maLop;
                if (!int.TryParse(cbLop.SelectedValue.ToString(), out maLop))
                    return;

                if (maLop > 0)
                {
                    LoadDanhSachHocSinh(maLop);
                    LoadDanhSachHocKy();
                    LoadDanhSachMonHoc();

                    cbHocSinh.Enabled = true;
                    cbHocKy.Enabled = true;
                    cbMonHoc.Enabled = true;
                }
                else
                {
                    cbHocSinh.Enabled = false;
                    cbHocKy.Enabled = false;
                    cbMonHoc.Enabled = false;
                    txtDiemTX.Enabled = false;
                    txtDiemGK.Enabled = false;
                    txtDiemCK.Enabled = false;
                    btnLuu.Enabled = false;

                    cbHocSinh.Items.Clear();
                    cbHocKy.Items.Clear();
                    cbMonHoc.Items.Clear();
                    ClearDiemInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn lớp: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cbHocSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Có thể thêm logic kiểm tra điểm đã có hay chưa
            CheckEnableDiemInput();
        }

        private void cbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Có thể thêm logic kiểm tra điểm đã có hay chưa
            CheckEnableDiemInput();

        }

        private void cbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Có thể thêm logic kiểm tra điểm đã có hay chưa
            CheckEnableDiemInput();
        }


        /// <summary>
        /// Kiểm tra và kích hoạt nhập điểm khi đủ điều kiện
        /// </summary>
        private void CheckEnableDiemInput()
        {
            bool canInput = IsAllComboboxSelected();

            if (canInput)
            {
                txtDiemTX.Enabled = true;
            }
            else
            {
                txtDiemTX.Enabled = false;
                txtDiemGK.Enabled = false;
                txtDiemCK.Enabled = false;
                btnLuu.Enabled = false;
                ClearDiemInputs();
            }
        }


        /// <summary>
        /// Kiểm tra tất cả combobox đã được chọn
        /// </summary>
        private bool IsAllComboboxSelected()
        {
            return cbHocSinh.SelectedIndex > 0 &&
                   cbHocKy.SelectedIndex > 0 &&
                   cbMonHoc.SelectedIndex > 0;
        }

        private void txtDiemTX_TextChanged(object sender, EventArgs e)
        {
           

            ValidateDiem(txtDiemTX);

            // Nếu đang ở chế độ sửa, luôn enable btnLuu
            if (isEditMode)
            {
                btnLuu.Enabled = true;
                return;
            }

            // Code cũ cho chế độ thêm mới
            if (!string.IsNullOrWhiteSpace(txtDiemTX.Text))
            {
                if (float.TryParse(txtDiemTX.Text, out float diem))
                {
                    if (themDiemBUS.KiemTraDiemHopLe(diem))
                    {
                        txtDiemGK.Enabled = true;
                    }
                    else
                    {
                        txtDiemGK.Enabled = false;
                        txtDiemCK.Enabled = false;
                        btnLuu.Enabled = false;
                    }
                }
                else
                {
                    txtDiemGK.Enabled = false;
                    txtDiemCK.Enabled = false;
                    btnLuu.Enabled = false;
                }
            }
            else
            {
                txtDiemGK.Enabled = false;
                txtDiemGK.Text = "";
                txtDiemCK.Enabled = false;
                txtDiemCK.Text = "";
                btnLuu.Enabled = false;
            }

        }

        private void txtDiemGK_TextChanged(object sender, EventArgs e)
        {


            ValidateDiem(txtDiemGK);

            // Nếu đang ở chế độ sửa, luôn enable btnLuu
            if (isEditMode)
            {
                btnLuu.Enabled = true;
                return;
            }

            // Code cũ cho chế độ thêm mới...
            if (!string.IsNullOrWhiteSpace(txtDiemTX.Text) &&
                !string.IsNullOrWhiteSpace(txtDiemGK.Text))
            {
                if (float.TryParse(txtDiemGK.Text, out float diem))
                {
                    if (themDiemBUS.KiemTraDiemHopLe(diem))
                    {
                        txtDiemCK.Enabled = true;
                    }
                    else
                    {
                        txtDiemCK.Enabled = false;
                        btnLuu.Enabled = false;
                    }
                }
                else
                {
                    txtDiemCK.Enabled = false;
                    btnLuu.Enabled = false;
                }
            }
            else
            {
                txtDiemCK.Enabled = false;
                txtDiemCK.Text = "";
                btnLuu.Enabled = false;
            }

            CheckEnableSaveButton();

        }

        private void txtDiemCK_TextChanged(object sender, EventArgs e)
        {
            ValidateDiem(txtDiemCK);

            // Nếu đang ở chế độ sửa, luôn enable btnLuu
            if (isEditMode)
            {
                btnLuu.Enabled = true;
                return;
            }

            CheckEnableSaveButton();
        }

        private void CheckEnableSaveButton()
        {
            // Yêu cầu tối thiểu: có DiemTX
            if (!string.IsNullOrWhiteSpace(txtDiemTX.Text))
            {
                float diemTX;
                if (float.TryParse(txtDiemTX.Text, out diemTX) &&
                    themDiemBUS.KiemTraDiemHopLe(diemTX))
                {
                    btnLuu.Enabled = true;
                    return;
                }
            }

            btnLuu.Enabled = false;
        }

        private void ValidateDiem(Guna2TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (float.TryParse(textBox.Text, out float diem))
                {
                    if (!themDiemBUS.KiemTraDiemHopLe(diem))
                    {
                        textBox.BackColor = System.Drawing.Color.LightPink;
                    }
                    else
                    {
                        textBox.BackColor = System.Drawing.Color.White;
                    }
                }
                else
                {
                    textBox.BackColor = System.Drawing.Color.LightPink;
                }
            }
            else
            {
                textBox.BackColor = System.Drawing.Color.White;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditMode)
                {
                    // Chế độ sửa - chỉ sửa điểm TX
                    if (string.IsNullOrWhiteSpace(txtDiemTX.Text))
                    {
                        MessageBox.Show("Vui lòng nhập điểm thường xuyên!", "Thông báo",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDiemTX.Focus();
                        return;
                    }

                    float diemTX;
                    if (!float.TryParse(txtDiemTX.Text, out diemTX))
                    {
                        MessageBox.Show("Điểm thường xuyên không hợp lệ!", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!themDiemBUS.KiemTraDiemHopLe(diemTX))
                    {
                        MessageBox.Show("Điểm phải nằm trong khoảng 0-10!", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Lấy điểm GK và CK từ textbox (đã bị lock)
                    float? diemGK = null;
                    float? diemCK = null;

                    if (!string.IsNullOrWhiteSpace(txtDiemGK.Text))
                        diemGK = float.Parse(txtDiemGK.Text);

                    if (!string.IsNullOrWhiteSpace(txtDiemCK.Text))
                        diemCK = float.Parse(txtDiemCK.Text);

                    // Cập nhật điểm
                    bool success = themDiemBUS.SuaDiem(editMaHocSinh, editMaMonHoc, editMaHocKy,
                                                       diemTX, diemGK, diemCK);

                    //if (success)
                    //{
                    //    MessageBox.Show("Cập nhật điểm thành công!", "Thông báo",
                    //                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    this.DialogResult = DialogResult.OK;
                    //    this.Close();
                    //}
                    if (success)
                    {
                        MessageBox.Show("Cập nhật điểm thành công!", "Thông báo",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MaHocSinhVuaThem = editMaHocSinh; // Lưu mã học sinh
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật điểm thất bại!", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {


                    // Chế độ thêm mới
                    if (!IsAllComboboxSelected())
                    {
                        MessageBox.Show("Vui lòng chọn đầy đủ: Lớp, Học sinh, Học kỳ và Môn học!",
                                       "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtDiemTX.Text))
                    {
                        MessageBox.Show("Vui lòng nhập ít nhất Điểm thường xuyên!",
                                       "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDiemTX.Focus();
                        return;
                    }

                    string maHocSinh = ((dynamic)cbHocSinh.SelectedItem).Value;
                    int maHocKy = ((dynamic)cbHocKy.SelectedItem).Value;
                    string maMon = ((dynamic)cbMonHoc.SelectedItem).Value;

                    int maMonHoc;
                    if (!int.TryParse(maMon, out maMonHoc))
                    {
                        MessageBox.Show("Mã môn học không hợp lệ!", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    float? diemTX = null;
                    float? diemGK = null;
                    float? diemCK = null;

                    if (!string.IsNullOrWhiteSpace(txtDiemTX.Text))
                    {
                        if (float.TryParse(txtDiemTX.Text, out float tx))
                            diemTX = tx;
                    }

                    if (!string.IsNullOrWhiteSpace(txtDiemGK.Text))
                    {
                        if (float.TryParse(txtDiemGK.Text, out float gk))
                            diemGK = gk;
                    }

                    if (!string.IsNullOrWhiteSpace(txtDiemCK.Text))
                    {
                        if (float.TryParse(txtDiemCK.Text, out float ck))
                            diemCK = ck;
                    }

                    if (!themDiemBUS.KiemTraDiemHopLe(diemTX) ||
                        !themDiemBUS.KiemTraDiemHopLe(diemGK) ||
                        !themDiemBUS.KiemTraDiemHopLe(diemCK))
                    {
                        MessageBox.Show("Điểm phải nằm trong khoảng 0-10!", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool success = themDiemBUS.LuuDiem(maHocSinh, maMonHoc, maHocKy,
                                                       diemTX, diemGK, diemCK);

                    if (success)
                    {
                        //MessageBox.Show("Lưu điểm thành công!", "Thông báo",
                        //               MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //ClearDiemInputs();
                        //cbHocSinh.SelectedIndex = 0;

                        MessageBox.Show("Lưu điểm thành công!", "Thông báo",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MaHocSinhVuaThem = maHocSinh; // Lưu mã học sinh
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Lưu điểm thất bại!", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                //if (ex.Message == "DIEM_DA_TON_TAI")
                //{
                //    string tenHS = ((dynamic)cbHocSinh.SelectedItem).Text;
                //    string tenMH = ((dynamic)cbMonHoc.SelectedItem).Text;
                //    string tenHK = ((dynamic)cbHocKy.SelectedItem).Text;

                //    MessageBox.Show(
                //        $"Học sinh '{tenHS}' đã có điểm môn '{tenMH}' trong '{tenHK}'!\n\n" +
                //        "Vui lòng sử dụng chức năng 'Sửa điểm' để cập nhật điểm.",
                //        "Điểm đã tồn tại",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
                //else
                //{
                //    MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi",
                //                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                //if (ex.Message == "DIEM_DA_DAY_DU")
                //{
                //    string tenHS = ((dynamic)cbHocSinh.SelectedItem).Text;
                //    string tenMH = ((dynamic)cbMonHoc.SelectedItem).Text;
                //    string tenHK = ((dynamic)cbHocKy.SelectedItem).Text;

                //    MessageBox.Show(
                //        $"Học sinh '{tenHS}' đã có đủ điểm môn '{tenMH}' trong '{tenHK}'!\n\n" +
                //        "Các cột điểm Thường xuyên, Giữa kỳ và Cuối kỳ đều đã được nhập.\n" +
                //        "Vui lòng sử dụng chức năng 'Sửa điểm' để cập nhật.",
                //        "Điểm đã đầy đủ",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //}
                //else
                //{
                //    MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi",
                //                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                if (ex.Message == "DIEM_DA_DAY_DU")
                {
                    string tenHS = ((dynamic)cbHocSinh.SelectedItem).Text;
                    string tenMH = ((dynamic)cbMonHoc.SelectedItem).Text;
                    string tenHK = ((dynamic)cbHocKy.SelectedItem).Text;

                    MessageBox.Show(
                        $"Học sinh '{tenHS}' đã có đủ điểm môn '{tenMH}' trong '{tenHK}'!\n\n" +
                        "Các cột điểm Thường xuyên, Giữa kỳ và Cuối kỳ đều đã được nhập.\n" +
                        "Vui lòng sử dụng chức năng 'Sửa điểm' để cập nhật.",
                        "Điểm đã đầy đủ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else if (ex.Message == "DIEM_DA_TON_TAI_KHONG_THE_THEM")
                {
                    MessageBox.Show(
                        "Điểm đã tồn tại, hãy nhấn nút Sửa điểm nếu muốn đổi điểm!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lỗi khi lưu điểm: " + ex.Message, "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void ClearDiemInputs()
        {
            txtDiemTX.Text = "";
            txtDiemGK.Text = "";
            txtDiemCK.Text = "";
            txtDiemTX.BackColor = System.Drawing.Color.White;
            txtDiemGK.BackColor = System.Drawing.Color.White;
            txtDiemCK.BackColor = System.Drawing.Color.White;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

