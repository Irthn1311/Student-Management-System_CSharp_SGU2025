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
            
            // Thêm sự kiện để làm nổi bật dòng được chọn
            dgvPhuHuynh.SelectionChanged += DgvPhuHuynh_SelectionChanged;
            
            // Thêm sự kiện hover để đổi màu khi di chuột
            dgvPhuHuynh.CellMouseEnter += DgvPhuHuynh_CellMouseEnter;
            dgvPhuHuynh.CellMouseLeave += DgvPhuHuynh_CellMouseLeave;
        }
        // Biến để lưu dòng đang hover
        private int hoveredRowIndex = -1;

        /// <summary>
        /// Xử lý khi thay đổi selection để làm nổi bật dòng được chọn
        /// </summary>
        private void DgvPhuHuynh_SelectionChanged(object sender, EventArgs e)
        {
            // Reset màu của tất cả các dòng về màu ban đầu
            for (int i = 0; i < dgvPhuHuynh.Rows.Count; i++)
            {
                DataGridViewRow row = dgvPhuHuynh.Rows[i];
                if (!row.Selected)
                {
                    // Trả về màu ban đầu (alternating hoặc white)
                    if (i % 2 == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            
            // Refresh để hiển thị màu selection rõ ràng
            if (dgvPhuHuynh.SelectedRows.Count > 0)
            {
                dgvPhuHuynh.InvalidateRow(dgvPhuHuynh.SelectedRows[0].Index);
            }
            
            // Reset hover index
            hoveredRowIndex = -1;
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
            // Màu nền khi chọn - màu xanh dương nhạt nổi bật
            dgvPhuHuynh.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            dgvPhuHuynh.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 136, 229);
            dgvPhuHuynh.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvPhuHuynh.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            // Màu nền khi chọn cho dòng alternating
            dgvPhuHuynh.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            dgvPhuHuynh.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 136, 229);
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

            // Cấu hình ThemeStyle cho Guna2DataGridView để đảm bảo màu selection được áp dụng
            dgvPhuHuynh.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            dgvPhuHuynh.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(30, 136, 229);
            dgvPhuHuynh.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            dgvPhuHuynh.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.FromArgb(30, 136, 229);

            // Căn chỉnh các cột
            dgvPhuHuynh.Columns["colHoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPhuHuynh.Columns["colHoTen"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPhuHuynh.Columns["colSoDienThoai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhuHuynh.Columns["colEmail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvPhuHuynh.Columns["colDiaChi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
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
                    ph.Email ?? "",
                    ph.DiaChi ?? ""
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
                    (ph.Email != null && ph.Email.ToLower().Contains(keyword)) ||
                    (ph.DiaChi != null && ph.DiaChi.ToLower().Contains(keyword))
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
        /// Xử lý nút Hủy
        /// </summary>
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Xử lý khi di chuột vào dòng - đổi màu hover
        /// </summary>
        private void DgvPhuHuynh_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPhuHuynh.Rows.Count)
            {
                DataGridViewRow row = dgvPhuHuynh.Rows[e.RowIndex];
                
                // Chỉ đổi màu nếu dòng đó chưa được chọn
                if (!row.Selected)
                {
                    hoveredRowIndex = e.RowIndex;
                    // Đổi màu nền thành xanh nhạt khi hover
                    row.DefaultCellStyle.BackColor = Color.FromArgb(243, 246, 255);
                }
            }
        }

        /// <summary>
        /// Xử lý khi di chuột ra khỏi dòng - trả về màu ban đầu
        /// </summary>
        private void DgvPhuHuynh_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPhuHuynh.Rows.Count)
            {
                DataGridViewRow row = dgvPhuHuynh.Rows[e.RowIndex];
                
                // Chỉ trả về màu ban đầu nếu dòng đó chưa được chọn
                if (!row.Selected)
                {
                    // Kiểm tra xem có phải dòng alternating không
                    if (e.RowIndex % 2 == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            
            // Reset hovered row
            if (hoveredRowIndex == e.RowIndex)
            {
                hoveredRowIndex = -1;
            }
        }
    }
}
