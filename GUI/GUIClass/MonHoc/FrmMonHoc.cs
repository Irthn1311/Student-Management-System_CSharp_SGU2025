using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmMonHoc : UserControl
    {
        private MonHocBUS monHocBUS;
        private NamHocBUS namHocBUS;
        private BindingList<MonHocDTO> bindingListMonHoc;
        private MonHocDTO monHocDangChon;
        private bool dangThem = false;

        // ✅ UI Controls for year selection đã được khai báo trong Designer.cs

        public FrmMonHoc()
        {
            InitializeComponent();
            monHocBUS = new MonHocBUS();
            namHocBUS = new NamHocBUS();
            bindingListMonHoc = new BindingList<MonHocDTO>();
            // Controls đã được khởi tạo trong Designer, chỉ cần load dữ liệu
            LoadNamHocComboBox();
        }

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckAccessPermission(PermissionHelper.QLMONHOC, "Quản lý môn học"))
            {
                this.Enabled = false;
                MessageBox.Show(
                    "Bạn không có quyền truy cập chức năng 'Quản lý môn học'!",
                    "Không có quyền",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            SetupDataGridView();
            LoadData();
            LoadNamHocComboBox();
            dgvMonHoc.SelectionChanged += dgvMonHoc_SelectionChanged;
            VoHieuHoaControls();
            txtTenMon.Validating += txtTenMon_Validating;
            txtSoTiet.Validating += txtSoTiet_Validating;
            txtMaMon.Validating += txtMaMon_Validating;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;

            // ✅ Đảm bảo panelButtons được BringToFront trước
            if (panelButtons != null)
            {
                panelButtons.BringToFront();
                panelButtons.Visible = true;
            }

            // Áp dụng quyền cho các nút
            PermissionHelper.ApplyPermissionMonHoc(
                btnThemMoi,
                btnSua,
                btnXoaMoi
            );
            
            // ✅ FORCE HIỂN THỊ: Đảm bảo button luôn visible nếu có quyền
            // Bỏ qua logic ẩn của PermissionHelper nếu có quyền
            if (btnThemMoi != null)
            {
                bool hasCreate = PermissionHelper.HasPermission(PermissionHelper.QLMONHOC, PermissionHelper.CREATE);
                if (hasCreate)
                {
                    btnThemMoi.Visible = true;
                    btnThemMoi.Enabled = true;
                    btnThemMoi.BringToFront();
                    // Đảm bảo button có size và location hợp lý
                    if (btnThemMoi.Size.Width == 0 || btnThemMoi.Size.Height == 0)
                    {
                        btnThemMoi.Size = new System.Drawing.Size(120, 40);
                    }
                    if (btnThemMoi.Location.X < 0 || btnThemMoi.Location.Y < 0)
                    {
                        btnThemMoi.Location = new System.Drawing.Point(6, 5);
                    }
                }
            }
            
            if (btnXoaMoi != null)
            {
                bool hasDelete = PermissionHelper.HasPermission(PermissionHelper.QLMONHOC, PermissionHelper.DELETE);
                if (hasDelete)
                {
                    btnXoaMoi.Visible = true;
                    btnXoaMoi.Enabled = true;
                    btnXoaMoi.BringToFront();
                    // Đảm bảo button có size và location hợp lý
                    if (btnXoaMoi.Size.Width == 0 || btnXoaMoi.Size.Height == 0)
                    {
                        btnXoaMoi.Size = new System.Drawing.Size(120, 40);
                    }
                    if (btnXoaMoi.Location.X < 0 || btnXoaMoi.Location.Y < 0)
                    {
                        btnXoaMoi.Location = new System.Drawing.Point(974, 5);
                    }
                }
            }
            
            // ✅ Refresh panelButtons để đảm bảo hiển thị
            if (panelButtons != null)
            {
                panelButtons.Refresh();
                panelButtons.Invalidate();
                panelButtons.Update();
            }
        }

        // =======================================================
        // === PHẦN CHUẨN BỊ VÀ HỖ TRỢ ===
        // =======================================================
        private void SetupDataGridView()
        {
            dgvMonHoc.AutoGenerateColumns = false;
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.ReadOnly = true;
            dgvMonHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMonHoc.MultiSelect = false;
            dgvMonHoc.DataSource = bindingListMonHoc;

            dgvMonHoc.Columns["MaMon"].DataPropertyName = "maMon";
            dgvMonHoc.Columns["TenMon"].DataPropertyName = "tenMon";
            dgvMonHoc.Columns["SoTiet"].DataPropertyName = "soTiet";
            dgvMonHoc.Columns["GhiChu"].DataPropertyName = "ghiChu";
        }

        private void LoadData()
        {
            try
            {
                var danhSach = monHocBUS.DocDSMH();
                CapNhatBindingList(danhSach);

                if (bindingListMonHoc.Count > 0 && dgvMonHoc.Rows.Count > 0)
                    dgvMonHoc.CurrentCell = dgvMonHoc.Rows[0].Cells[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatBindingList(List<MonHocDTO> danhSachMoi)
        {
            for (int i = bindingListMonHoc.Count - 1; i >= 0; i--)
            {
                var mhCu = bindingListMonHoc[i];
                if (!danhSachMoi.Any(m => m.maMon == mhCu.maMon))
                    bindingListMonHoc.RemoveAt(i);
            }

            foreach (var mhMoi in danhSachMoi)
            {
                var mhCu = bindingListMonHoc.FirstOrDefault(m => m.maMon == mhMoi.maMon);
                if (mhCu == null)
                    bindingListMonHoc.Add(mhMoi);
                else
                {
                    mhCu.tenMon = mhMoi.tenMon;
                    mhCu.soTiet = mhMoi.soTiet;
                    mhCu.ghiChu = mhMoi.ghiChu;
                }
            }
            bindingListMonHoc.ResetBindings();
        }

        private void dgvMonHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMonHoc.CurrentRow?.DataBoundItem is MonHocDTO mh && !dangThem)
            {
                monHocDangChon = mh;
                HienThiThongTinMonHoc(mh);
            }
        }

        private void HienThiThongTinMonHoc(MonHocDTO mh)
        {
            if (mh == null) return;
            txtMaMon.Text = mh.maMon.ToString();
            txtTenMon.Text = mh.tenMon;
            txtSoTiet.Text = mh.soTiet.ToString();
            cboLoaiMon.Text = mh.ghiChu;
        }

        private void XoaDuLieuControls()
        {
            txtMaMon.Clear();
            txtTenMon.Clear();
            txtSoTiet.Clear();
            cboLoaiMon.SelectedIndex = -1;
            monHocDangChon = null;
            
            // Reset year selection
            if (chkApDungTuNamHoc != null)
                chkApDungTuNamHoc.Checked = false;
            if (cbNamHocBatDau != null)
            {
                cbNamHocBatDau.SelectedIndex = -1;
                cbNamHocBatDau.Enabled = false;
            }
            if (chkApDungTatCaKhoi != null)
            {
                chkApDungTatCaKhoi.Checked = false;
                chkApDungTatCaKhoi.Enabled = false;
            }
        }

        private void VoHieuHoaControls()
        {
            txtTenMon.Enabled = false;
            txtSoTiet.Enabled = false;
            cboLoaiMon.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            
            // Disable year selection controls
            if (chkApDungTuNamHoc != null)
                chkApDungTuNamHoc.Enabled = false;
            if (cbNamHocBatDau != null)
                cbNamHocBatDau.Enabled = false;
            if (chkApDungTatCaKhoi != null)
                chkApDungTatCaKhoi.Enabled = false;
        }

        private void KichHoatControls()
        {
            txtTenMon.Enabled = true;
            txtSoTiet.Enabled = true;
            cboLoaiMon.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            
            // Enable year selection controls only when adding new
            if (dangThem)
            {
                if (chkApDungTuNamHoc != null)
                    chkApDungTuNamHoc.Enabled = true;
            }
        }

        // ✅ KÍCH HOẠT CONTROLS CHỈ ĐỂ SỬA SỐ TIẾT
        private void KichHoatControlsSua()
        {
            txtTenMon.Enabled = false;  // Không cho sửa tên môn
            txtSoTiet.Enabled = true;   // Chỉ cho sửa số tiết
            cboLoaiMon.Enabled = false; // Không cho sửa loại môn
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private bool KiemTraDuLieu()
        {
            // Khi đang sửa (không phải thêm), chỉ validate số tiết
            if (!dangThem)
            {
                if (!int.TryParse(txtSoTiet.Text, out int soTiet) || soTiet <= 0)
                {
                    MessageBox.Show("Số tiết phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }

            // Khi đang thêm mới, validate tất cả
            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập tên môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtSoTiet.Text, out int soTietThem) || soTietThem <= 0)
            {
                MessageBox.Show("Số tiết phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboLoaiMon.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // =======================================================
        // === PHẦN NGHIỆP VỤ: THÊM – SỬA – XÓA ===
        // =======================================================

        // ✅ THÊM MÔN HỌC
        private void ThemMonHoc()
        {
            try
            {
                var monHocMoi = new MonHocDTO
                {
                    tenMon = txtTenMon.Text.Trim(),
                    soTiet = int.Parse(txtSoTiet.Text),
                    ghiChu = cboLoaiMon.Text
                };

                // Kiểm tra nếu có chọn năm học bắt đầu
                string maNamHoc = null;
                bool apDungTatCaKhoi = false;

                if (chkApDungTuNamHoc != null && chkApDungTuNamHoc.Checked)
                {
                    if (cbNamHocBatDau != null && cbNamHocBatDau.SelectedItem != null)
                    {
                        var item = cbNamHocBatDau.SelectedItem as ComboBoxItem;
                        if (item != null && item.Value != null)
                        {
                            maNamHoc = item.Value.ToString();
                        }
                    }

                    if (string.IsNullOrEmpty(maNamHoc))
                    {
                        MessageBox.Show("Vui lòng chọn năm học bắt đầu áp dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (chkApDungTatCaKhoi != null)
                    {
                        apDungTatCaKhoi = chkApDungTatCaKhoi.Checked;
                    }
                }

                // Sử dụng method mới để thêm môn học và liên kết với năm học/khối
                int maMoiTao = monHocBUS.ThemMonHocVaLienKetNamHocKhoi(monHocMoi, maNamHoc, apDungTatCaKhoi);
                
                if (maMoiTao > 0)
                {
                    monHocMoi.maMon = maMoiTao;
                    bindingListMonHoc.Add(monHocMoi);

                    string message = "Thêm môn học thành công!";
                    if (!string.IsNullOrEmpty(maNamHoc) && apDungTatCaKhoi)
                    {
                        message += "\nMôn học đã được áp dụng cho tất cả khối (10, 11, 12) từ năm học " + maNamHoc;
                    }
                    else if (!string.IsNullOrEmpty(maNamHoc))
                    {
                        message += "\nMôn học sẽ bắt đầu từ năm học " + maNamHoc;
                    }

                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvMonHoc.CurrentCell = dgvMonHoc.Rows[bindingListMonHoc.Count - 1].Cells[0];
                }
                else
                {
                    MessageBox.Show("Không thể thêm môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Lỗi khi thêm môn học: {ex.Message}";
                
                // ✅ Kiểm tra nếu là lỗi AUTO_INCREMENT
                if (ex.Message.Contains("doesn't have a default value") || ex.Message.Contains("MaMonHoc"))
                {
                    errorMessage += "\n\n" +
                        "⚠️ VẤN ĐỀ: Bảng MonHoc chưa có AUTO_INCREMENT cho cột MaMonHoc.\n\n" +
                        "🔧 CÁCH KHẮC PHỤC:\n" +
                        "1. Mở MySQL Workbench hoặc công cụ quản lý database\n" +
                        "2. Chạy script: DAO/ConnectDatabase/05_fix_monhoc_autoincrement.sql\n" +
                        "3. Hoặc chạy lệnh SQL:\n" +
                        "   ALTER TABLE MonHoc MODIFY COLUMN MaMonHoc INT AUTO_INCREMENT;";
                }
                
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ SỬA MÔN HỌC - CHỈ SỬA SỐ TIẾT
        private void SuaMonHoc()
        {
            if (monHocDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn môn học để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chỉ cập nhật số tiết, giữ nguyên tên môn và loại môn
            monHocDangChon.soTiet = int.Parse(txtSoTiet.Text);

            if (monHocBUS.UpdateMonHoc(monHocDangChon))
            {
                bindingListMonHoc.ResetBindings();
                MessageBox.Show("Cập nhật số tiết thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể cập nhật số tiết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ XÓA MÔN HỌC
        private void XoaMonHoc()
        {
            if (monHocDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn môn học cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dr = MessageBox.Show(
                $"Bạn có chắc muốn xóa môn học {monHocDangChon.tenMon}?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (dr == DialogResult.Yes)
            {
                if (monHocBUS.DeleteMonHoc(monHocDangChon.maMon))
                {
                    bindingListMonHoc.Remove(monHocDangChon);
                    XoaDuLieuControls();
                    MessageBox.Show("Đã xóa môn học!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể xóa môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =======================================================
        // === PHẦN GỌI HÀM QUA NÚT NHẤN ===
        // =======================================================

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckCreatePermission(PermissionHelper.QLMONHOC, "Quản lý môn học"))
                return;
            dangThem = true;
            XoaDuLieuControls();
            KichHoatControls();

            txtMaMon.Text = "Tự động";
            
            // Reset year selection controls
            if (chkApDungTuNamHoc != null)
            {
                chkApDungTuNamHoc.Checked = false;
            }
            if (cbNamHocBatDau != null)
            {
                cbNamHocBatDau.SelectedIndex = -1;
                cbNamHocBatDau.Enabled = false;
            }
            if (chkApDungTatCaKhoi != null)
            {
                chkApDungTatCaKhoi.Checked = false;
                chkApDungTatCaKhoi.Enabled = false;
            }

            txtTenMon.Focus();
            btnSua.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckUpdatePermission(PermissionHelper.QLMONHOC, "Quản lý môn học"))
                return;
            if (monHocDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn môn học cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dangThem = false;
            KichHoatControlsSua(); // ✅ Chỉ cho phép sửa số tiết
            btnSua.Enabled = false;
        }

        private void btnXoaMoi_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckDeletePermission(PermissionHelper.QLMONHOC, "Quản lý môn học"))
                return;
            XoaMonHoc();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu()) return;

            if (dangThem)
                ThemMonHoc();
            else
                SuaMonHoc();

            dangThem = false;
            VoHieuHoaControls();
            btnSua.Enabled= true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dangThem = false;
            VoHieuHoaControls();
              btnSua.Enabled = true;

            if (monHocDangChon != null)
                HienThiThongTinMonHoc(monHocDangChon);
            else
                XoaDuLieuControls();
        }

        private void txtTenMon_Validating(object sender, CancelEventArgs e)
        {
            // Khi đang sửa (không phải thêm), không validate tên môn
            if (!dangThem)
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTenMon, null);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTenMon, "Tên môn học không được để trống.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTenMon, null);
            }
        }
        private void txtSoTiet_Validating(object sender, CancelEventArgs e)
        {
            if (!int.TryParse(txtSoTiet.Text, out int soTiet) || soTiet <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSoTiet, "Số tiết phải là số nguyên dương.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtSoTiet, null);
            }
        }
        private void txtMaMon_Validating(object sender, CancelEventArgs e)
        {
            if (!dangThem && (string.IsNullOrWhiteSpace(txtMaMon.Text) || !int.TryParse(txtMaMon.Text, out int maMon) || maMon <= 0))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMaMon, "Mã môn học không hợp lệ.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtMaMon, null);
            }
        }

        private void panelThongTin_Paint(object sender, PaintEventArgs e)
        {

        }

        // ✅ Controls đã được khởi tạo trong Designer.cs, không cần InitializeYearSelectionControls() nữa

        /// <summary>
        /// Load danh sách năm học vào ComboBox
        /// </summary>
        private void LoadNamHocComboBox()
        {
            try
            {
                if (cbNamHocBatDau == null) return;

                cbNamHocBatDau.Items.Clear();
                var dsNamHoc = namHocBUS.DocDSNamHoc();
                
                if (dsNamHoc != null && dsNamHoc.Count > 0)
                {
                    foreach (var nh in dsNamHoc.OrderByDescending(n => n.NgayBD))
                    {
                        cbNamHocBatDau.Items.Add(new ComboBoxItem { Text = nh.TenNamHoc, Value = nh.MaNamHoc });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load danh sách năm học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event handler khi checkbox "Áp dụng từ năm học" thay đổi
        /// </summary>
        private void ChkApDungTuNamHoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApDungTuNamHoc != null && cbNamHocBatDau != null && chkApDungTatCaKhoi != null)
            {
                bool isChecked = chkApDungTuNamHoc.Checked;
                cbNamHocBatDau.Enabled = isChecked;
                chkApDungTatCaKhoi.Enabled = isChecked;
                
                if (!isChecked)
                {
                    cbNamHocBatDau.SelectedIndex = -1;
                    chkApDungTatCaKhoi.Checked = false;
                }
            }
        }

        /// <summary>
        /// ✅ DEBUG: Kiểm tra trạng thái button
        /// </summary>
        private void DebugButtonState(string stage)
        {
            StringBuilder debug = new StringBuilder();
            debug.AppendLine($"=== DEBUG BUTTON STATE: {stage} ===");
            
            if (btnThemMoi != null)
            {
                debug.AppendLine($"btnThemMoi:");
                debug.AppendLine($"  - IsNull: {btnThemMoi == null}");
                debug.AppendLine($"  - Visible: {btnThemMoi.Visible}");
                debug.AppendLine($"  - Enabled: {btnThemMoi.Enabled}");
                debug.AppendLine($"  - Location: {btnThemMoi.Location}");
                debug.AppendLine($"  - Size: {btnThemMoi.Size}");
                debug.AppendLine($"  - Parent: {btnThemMoi.Parent?.Name ?? "NULL"}");
                debug.AppendLine($"  - HasPermission(CREATE): {PermissionHelper.HasPermission(PermissionHelper.QLMONHOC, PermissionHelper.CREATE)}");
            }
            else
            {
                debug.AppendLine("btnThemMoi: NULL!");
            }
            
            if (btnXoaMoi != null)
            {
                debug.AppendLine($"btnXoaMoi:");
                debug.AppendLine($"  - IsNull: {btnXoaMoi == null}");
                debug.AppendLine($"  - Visible: {btnXoaMoi.Visible}");
                debug.AppendLine($"  - Enabled: {btnXoaMoi.Enabled}");
                debug.AppendLine($"  - Location: {btnXoaMoi.Location}");
                debug.AppendLine($"  - Size: {btnXoaMoi.Size}");
                debug.AppendLine($"  - Parent: {btnXoaMoi.Parent?.Name ?? "NULL"}");
                debug.AppendLine($"  - HasPermission(DELETE): {PermissionHelper.HasPermission(PermissionHelper.QLMONHOC, PermissionHelper.DELETE)}");
            }
            else
            {
                debug.AppendLine("btnXoaMoi: NULL!");
            }
            
            if (panelButtons != null)
            {
                debug.AppendLine($"panelButtons:");
                debug.AppendLine($"  - Visible: {panelButtons.Visible}");
                debug.AppendLine($"  - Enabled: {panelButtons.Enabled}");
                debug.AppendLine($"  - Size: {panelButtons.Size}");
                debug.AppendLine($"  - Controls.Count: {panelButtons.Controls.Count}");
                debug.AppendLine($"  - Contains btnThemMoi: {panelButtons.Controls.Contains(btnThemMoi)}");
                debug.AppendLine($"  - Contains btnXoaMoi: {panelButtons.Controls.Contains(btnXoaMoi)}");
            }
            else
            {
                debug.AppendLine("panelButtons: NULL!");
            }
            
            debug.AppendLine("=====================================");
            
            // Ghi vào Console và hiển thị MessageBox
            Console.WriteLine(debug.ToString());
            System.Diagnostics.Debug.WriteLine(debug.ToString());
        }

        /// <summary>
        /// Helper class cho ComboBox items
        /// </summary>
        private class ComboBoxItem
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