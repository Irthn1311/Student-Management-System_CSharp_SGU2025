using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ChonPhuHuynhDialog : Form
    {
        private PhuHuynhBLL phuHuynhBLL;
        private List<PhuHuynhDTO> danhSachPhuHuynh;
        private List<PhuHuynhDTO> danhSachPhuHuynhFiltered;

        // Property để trả về phụ huynh đã chọn
        public PhuHuynhDTO SelectedPhuHuynh { get; private set; }

        // Property để trả về phụ huynh vừa thêm mới (nếu có)
        public PhuHuynhDTO NewPhuHuynh { get; private set; }

        // ID phụ huynh được chọn trước đó (để highlight)
        private int preselectedMaPhuHuynh = -1;

        public ChonPhuHuynhDialog(int preselectedMaPhuHuynh = -1)
        {
            InitializeComponent();
            this.preselectedMaPhuHuynh = preselectedMaPhuHuynh;
            phuHuynhBLL = new PhuHuynhBLL();
            danhSachPhuHuynh = new List<PhuHuynhDTO>();
            danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>();
        }

        private void ChonPhuHuynhDialog_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDanhSachPhuHuynh();
        }

        /// <summary>
        /// Thiết lập style cho DataGridView
        /// </summary>
        private void SetupDataGridView()
        {
            dgvPhuHuynh.EnableHeadersVisualStyles = false;
            dgvPhuHuynh.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPhuHuynh.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgvPhuHuynh.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPhuHuynh.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            dgvPhuHuynh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhuHuynh.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvPhuHuynh.ColumnHeadersHeight = 42;

            dgvPhuHuynh.DefaultCellStyle.BackColor = Color.White;
            dgvPhuHuynh.DefaultCellStyle.ForeColor = Color.FromArgb(40, 40, 40);
            dgvPhuHuynh.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvPhuHuynh.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 247, 250);
            dgvPhuHuynh.DefaultCellStyle.SelectionForeColor = Color.FromArgb(40, 40, 40);
            dgvPhuHuynh.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvPhuHuynh.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgvPhuHuynh.GridColor = Color.FromArgb(230, 230, 230);
            dgvPhuHuynh.RowTemplate.Height = 46;
            dgvPhuHuynh.BorderStyle = BorderStyle.None;
            dgvPhuHuynh.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPhuHuynh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhuHuynh.MultiSelect = false;
            dgvPhuHuynh.ReadOnly = true;
            dgvPhuHuynh.AllowUserToAddRows = false;
            dgvPhuHuynh.AllowUserToDeleteRows = false;
            dgvPhuHuynh.AllowUserToResizeColumns = false;
            dgvPhuHuynh.AllowUserToResizeRows = false;
            dgvPhuHuynh.RowHeadersVisible = false;

            dgvPhuHuynh.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvPhuHuynh.ColumnHeadersDefaultCellStyle.BackColor;
            dgvPhuHuynh.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgvPhuHuynh.ColumnHeadersDefaultCellStyle.ForeColor;

            // Căn chỉnh các cột
            dgvPhuHuynh.Columns["colHoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPhuHuynh.Columns["colHoTen"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPhuHuynh.Columns["colSoDienThoai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhuHuynh.Columns["colEmail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// Tải danh sách phụ huynh từ database
        /// </summary>
        private void LoadDanhSachPhuHuynh()
        {
            try
            {
                danhSachPhuHuynh = phuHuynhBLL.GetAllPhuHuynh();
                danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>(danhSachPhuHuynh);
                BindDataToGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phụ huynh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                danhSachPhuHuynh = new List<PhuHuynhDTO>();
                danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>();
            }
        }

        /// <summary>
        /// Bind dữ liệu vào DataGridView
        /// </summary>
        private void BindDataToGridView()
        {
            dgvPhuHuynh.Rows.Clear();

            foreach (var ph in danhSachPhuHuynhFiltered)
            {
                int rowIndex = dgvPhuHuynh.Rows.Add(
                    ph.MaPhuHuynh,
                    ph.HoTen,
                    ph.SoDienThoai,
                    ph.Email ?? ""
                );

                // Highlight phụ huynh được chọn trước đó
                if (ph.MaPhuHuynh == preselectedMaPhuHuynh)
                {
                    dgvPhuHuynh.Rows[rowIndex].Selected = true;
                    dgvPhuHuynh.FirstDisplayedScrollingRowIndex = Math.Max(0, rowIndex - 3);
                }
            }

            // Nếu có phụ huynh được chọn trước đó nhưng không có trong danh sách filtered, clear selection
            if (preselectedMaPhuHuynh > 0 && !danhSachPhuHuynhFiltered.Any(p => p.MaPhuHuynh == preselectedMaPhuHuynh))
            {
                dgvPhuHuynh.ClearSelection();
            }
        }

        /// <summary>
        /// Xử lý tìm kiếm
        /// </summary>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                danhSachPhuHuynhFiltered = new List<PhuHuynhDTO>(danhSachPhuHuynh);
            }
            else
            {
                danhSachPhuHuynhFiltered = danhSachPhuHuynh.Where(ph =>
                    ph.HoTen.ToLower().Contains(keyword) ||
                    ph.SoDienThoai.Contains(keyword) ||
                    (ph.Email != null && ph.Email.ToLower().Contains(keyword))
                ).ToList();
            }

            BindDataToGridView();
        }

        /// <summary>
        /// Xử lý double click để chọn nhanh
        /// </summary>
        private void dgvPhuHuynh_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectCurrentPhuHuynh();
            }
        }

        /// <summary>
        /// Xử lý nút Chọn
        /// </summary>
        private void btnChon_Click(object sender, EventArgs e)
        {
            if (dgvPhuHuynh.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phụ huynh từ danh sách.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectCurrentPhuHuynh();
        }

        /// <summary>
        /// Chọn phụ huynh hiện tại
        /// </summary>
        private void SelectCurrentPhuHuynh()
        {
            if (dgvPhuHuynh.SelectedRows.Count > 0)
            {
                int maPhuHuynh = Convert.ToInt32(dgvPhuHuynh.SelectedRows[0].Cells["colMaPhuHuynh"].Value);
                SelectedPhuHuynh = danhSachPhuHuynh.FirstOrDefault(p => p.MaPhuHuynh == maPhuHuynh);

                if (SelectedPhuHuynh != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Xử lý nút Thêm mới
        /// </summary>
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            using (ThemPhuHuynh formThemPH = new ThemPhuHuynh())
            {
                if (formThemPH.ShowDialog(this) == DialogResult.OK && formThemPH.NewPhuHuynh != null)
                {
                    NewPhuHuynh = formThemPH.NewPhuHuynh;
                    
                    // Load lại danh sách phụ huynh
                    LoadDanhSachPhuHuynh();
                    
                    // Tự động chọn phụ huynh vừa thêm
                    SelectedPhuHuynh = NewPhuHuynh;
                    preselectedMaPhuHuynh = NewPhuHuynh.MaPhuHuynh;
                    BindDataToGridView();

                    // Tự động đóng dialog và trả về kết quả
                    MessageBox.Show($"Đã thêm và chọn phụ huynh: {NewPhuHuynh.HoTen}", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Xử lý nút Hủy
        /// </summary>
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
