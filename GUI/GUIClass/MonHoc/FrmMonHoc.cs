using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmMonHoc : UserControl
    {
        private MonHocBUS monHocBUS;
        private BindingList<MonHocDTO> bindingListMonHoc;
        private MonHocDTO monHocDangChon;
        private bool dangThem = false;

        public FrmMonHoc()
        {
            InitializeComponent();
            monHocBUS = new MonHocBUS();
            bindingListMonHoc = new BindingList<MonHocDTO>();
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
            dgvMonHoc.SelectionChanged += dgvMonHoc_SelectionChanged;
            VoHieuHoaControls();
            txtTenMon.Validating += txtTenMon_Validating;
            txtSoTiet.Validating += txtSoTiet_Validating;
            txtMaMon.Validating += txtMaMon_Validating;
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;

            PermissionHelper.ApplyPermissionMonHoc(
             btnThemMonHoc,
             btnSua,
             btnXoa
         );
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
        }

        private void VoHieuHoaControls()
        {
            txtTenMon.Enabled = false;
            txtSoTiet.Enabled = false;
            cboLoaiMon.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void KichHoatControls()
        {
            txtTenMon.Enabled = true;
            txtSoTiet.Enabled = true;
            cboLoaiMon.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập tên môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtSoTiet.Text, out int soTiet) || soTiet <= 0)
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
            var monHocMoi = new MonHocDTO
            {
                tenMon = txtTenMon.Text.Trim(),
                soTiet = int.Parse(txtSoTiet.Text),
                ghiChu = cboLoaiMon.Text
            };

            int maMoiTao = monHocBUS.ThemMonHocVaLayId(monHocMoi);
            if (maMoiTao > 0)
            {
                monHocMoi.maMon = maMoiTao;
                bindingListMonHoc.Add(monHocMoi);

                MessageBox.Show("Thêm môn học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvMonHoc.CurrentCell = dgvMonHoc.Rows[bindingListMonHoc.Count - 1].Cells[0];
            }
            else
            {
                MessageBox.Show("Không thể thêm môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ SỬA MÔN HỌC
        private void SuaMonHoc()
        {
            if (monHocDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn môn học để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            monHocDangChon.tenMon = txtTenMon.Text.Trim();
            monHocDangChon.soTiet = int.Parse(txtSoTiet.Text);
            monHocDangChon.ghiChu = cboLoaiMon.Text;

            if (monHocBUS.UpdateMonHoc(monHocDangChon))
            {
                bindingListMonHoc.ResetBindings();
                MessageBox.Show("Cập nhật môn học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể cập nhật môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnThemMonHoc_Click(object sender, EventArgs e)
        {
            if (!PermissionHelper.CheckUpdatePermission(PermissionHelper.QLMONHOC, "Quản lý môn học"))
                return;
            dangThem = true;
            XoaDuLieuControls();
            KichHoatControls();

            txtMaMon.Text = "Tự động";
            txtTenMon.Focus();

            btnThemMonHoc.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
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
            KichHoatControls();
            btnThemMonHoc.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
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
            btnThemMonHoc.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dangThem = false;
            VoHieuHoaControls();
            btnThemMonHoc.Enabled = btnSua.Enabled = btnXoa.Enabled = true;

            if (monHocDangChon != null)
                HienThiThongTinMonHoc(monHocDangChon);
            else
                XoaDuLieuControls();
        }

        private void txtTenMon_Validating(object sender, CancelEventArgs e)
        {
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
    }
}