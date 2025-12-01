using Org.BouncyCastle.Crypto.Paddings;
using Student_Management_System_CSharp_SGU2025.BUS;
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

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class frmAddPhanQuyen : Form
    {
        private PhanQuyenBUS phanQuyenBUS;
        // Dictionary để lưu thông tin checkbox: Key = MaChucNang, Value = Dictionary<HanhDong, CheckBox>
        private Dictionary<string, Dictionary<string, Guna.UI2.WinForms.Guna2CheckBox>> checkBoxDict;
        public frmAddPhanQuyen()
        {
            InitializeComponent();
            phanQuyenBUS = new PhanQuyenBUS();
            checkBoxDict = new Dictionary<string, Dictionary<string, Guna.UI2.WinForms.Guna2CheckBox>>();
        }

        private void frmAddPhanQuyen_Load(object sender, EventArgs e)
        {
            setUpTableChucNang();
            tableChucNang.CellPainting += tableChucNang_CellPainting;
            tableChucNang.Scroll += tableChucNang_Scroll;
            LoadChucNang();

        }

        private void tableChucNang_Scroll(object sender, ScrollEventArgs e)
        {
            // Cập nhật vị trí tất cả checkbox khi scroll
            UpdateAllCheckBoxPositions();
            tableChucNang.Invalidate();
        }

        private void UpdateAllCheckBoxPositions()
        {
            foreach (var maChucNangEntry in checkBoxDict)
            {
                string maChucNang = maChucNangEntry.Key;
                var checkBoxes = maChucNangEntry.Value;

                // Tìm row index tương ứng với maChucNang
                int rowIndex = -1;
                for (int i = 0; i < tableChucNang.Rows.Count; i++)
                {
                    if (tableChucNang.Rows[i].Tag?.ToString() == maChucNang)
                    {
                        rowIndex = i;
                        break;
                    }
                }

                if (rowIndex >= 0 && rowIndex < tableChucNang.Rows.Count)
                {
                    try
                    {
                        Rectangle cellRect = tableChucNang.GetCellDisplayRectangle(
                            tableChucNang.Columns["hanhDong"].Index, rowIndex, false);

                        // Kiểm tra xem cell có đang hiển thị không
                        bool isVisible = cellRect.Height > 0 && cellRect.Width > 0 &&
                                        cellRect.Y >= tableChucNang.ColumnHeadersHeight &&
                                        cellRect.Y < tableChucNang.Height;

                        if (checkBoxes.ContainsKey("them"))
                        {
                            checkBoxes["them"].Visible = isVisible;
                            if (isVisible)
                            {
                                checkBoxes["them"].Location = new Point(cellRect.X + 5, cellRect.Y + 6);
                                checkBoxes["them"].BringToFront();
                            }
                        }

                        if (checkBoxes.ContainsKey("sua"))
                        {
                            checkBoxes["sua"].Visible = isVisible;
                            if (isVisible)
                            {
                                checkBoxes["sua"].Location = new Point(cellRect.X + 75, cellRect.Y + 6);
                                checkBoxes["sua"].BringToFront();
                            }
                        }

                        if (checkBoxes.ContainsKey("xoa"))
                        {
                            checkBoxes["xoa"].Visible = isVisible;
                            if (isVisible)
                            {
                                checkBoxes["xoa"].Location = new Point(cellRect.X + 130, cellRect.Y + 6);
                                checkBoxes["xoa"].BringToFront();
                            }
                        }
                    }
                    catch
                    {
                        // Nếu cell không hiển thị, ẩn các checkbox
                        if (checkBoxes.ContainsKey("them"))
                            checkBoxes["them"].Visible = false;
                        if (checkBoxes.ContainsKey("sua"))
                            checkBoxes["sua"].Visible = false;
                        if (checkBoxes.ContainsKey("xoa"))
                            checkBoxes["xoa"].Visible = false;
                    }
                }
                else
                {
                    // Row không tồn tại, ẩn checkbox
                    if (checkBoxes.ContainsKey("them"))
                        checkBoxes["them"].Visible = false;
                    if (checkBoxes.ContainsKey("sua"))
                        checkBoxes["sua"].Visible = false;
                    if (checkBoxes.ContainsKey("xoa"))
                        checkBoxes["xoa"].Visible = false;
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddQuyen_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validate input
                string tenVaiTro = txtTenPhanQuyen.Text.Trim();

                if (string.IsNullOrWhiteSpace(tenVaiTro))
                {
                    MessageBox.Show("Vui lòng nhập tên vai trò!", "Cảnh báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenPhanQuyen.Focus();
                    return;
                }

                // 2. Thu thập thông tin quyền từ các checkbox
                Dictionary<string, List<string>> danhSachQuyen = new Dictionary<string, List<string>>();

                foreach (var item in checkBoxDict)
                {
                    string maChucNang = item.Key;
                    var checkBoxes = item.Value;

                    List<string> hanhDongs = new List<string>();

                    foreach (var cb in checkBoxes)
                    {
                        if (cb.Value.Checked)
                        {
                            string hanhDong = phanQuyenBUS.MapCheckBoxToHanhDong(cb.Key);
                            if (!string.IsNullOrEmpty(hanhDong))
                            {
                                hanhDongs.Add(hanhDong);
                            }
                        }
                    }

                    // Chỉ thêm vào nếu có ít nhất 1 hành động được chọn
                    if (hanhDongs.Count > 0)
                    {
                        danhSachQuyen.Add(maChucNang, hanhDongs);
                    }
                }

                // 3. Kiểm tra có quyền nào được chọn không
                if (danhSachQuyen.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một quyền cho vai trò!", "Cảnh báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Xác nhận trước khi thêm
                string thongTinQuyen = "";
                foreach (var item in danhSachQuyen)
                {
                    string tenChucNang = GetTenChucNangFromMa(item.Key);
                    thongTinQuyen += $"- {tenChucNang}: {string.Join(", ", item.Value)}\n";
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn thêm vai trò '{tenVaiTro}' với các quyền sau:\n\n{thongTinQuyen}",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                // 5. Thực hiện thêm vào database
                bool success = phanQuyenBUS.ThemVaiTroVoiQuyen(tenVaiTro, danhSachQuyen);

                if (success)
                {
                    MessageBox.Show("Thêm quyền mới thành công!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đóng form và trả về DialogResult.OK
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm quyền thất bại!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm quyền: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy tên chức năng từ mã chức năng
        /// </summary>
        private string GetTenChucNangFromMa(string maChucNang)
        {
            foreach (DataGridViewRow row in tableChucNang.Rows)
            {
                if (row.Tag?.ToString() == maChucNang)
                {
                    return row.Cells["chucNang"].Value?.ToString() ?? "";
                }
            }
            return maChucNang;
        }

        private void setUpTableChucNang()
        {
            tableChucNang.Columns.Clear();
            // Thiết lập các cột và dữ liệu mẫu cho tableChucNang nếu cần
            tableChucNang.Columns.Add("chucNang", "Chức năng");
            tableChucNang.Columns.Add("hanhDong", "Hành động");

            // Dữ liệu sẽ được load từ database ở LoadChucNang()

            tableChucNang.RowTemplate.Height = 48;
            tableChucNang.EnableHeadersVisualStyles = false;
            tableChucNang.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            tableChucNang.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            tableChucNang.BackgroundColor = Color.White;
            tableChucNang.BorderStyle = BorderStyle.None;
            tableChucNang.GridColor = Color.FromArgb(240, 240, 240);

            tableChucNang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tableChucNang.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            tableChucNang.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
            tableChucNang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 249, 255);
            tableChucNang.DefaultCellStyle.SelectionForeColor = Color.Black;
            tableChucNang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            tableChucNang.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            tableChucNang.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
            tableChucNang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            tableChucNang.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            tableChucNang.ColumnHeadersHeight = 50;
            tableChucNang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            tableChucNang.DefaultCellStyle.Padding = new Padding(17, 0, 0, 0);
            tableChucNang.AllowUserToResizeColumns = false;
            tableChucNang.AllowUserToResizeRows = false;
            tableChucNang.AllowUserToDeleteRows = false;
            tableChucNang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tableChucNang.MultiSelect = false;

            tableChucNang.AllowUserToAddRows = false;
        }

        /// <summary>
        /// Load danh sách chức năng từ database
        /// </summary>
        private void LoadChucNang()
        {
            try
            {
                List<ChucNangDTO> danhSach = phanQuyenBUS.GetAllChucNang();

                tableChucNang.Rows.Clear();

                foreach (var cn in danhSach)
                {
                    // ✅ BỎ QUA chức năng "Cài đặt" - không hiển thị trong bảng phân quyền
                    if (cn.MaChucNang.ToLower() == "qlcaidat")
                        continue;

                    int rowIndex = tableChucNang.Rows.Add(cn.TenChucNang);
                    // Lưu MaChucNang vào Tag của row
                    tableChucNang.Rows[rowIndex].Tag = cn.MaChucNang;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load danh sách chức năng: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool HasCheckBoxes(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= tableChucNang.Rows.Count)
                return false;

            string maChucNang = tableChucNang.Rows[rowIndex].Tag?.ToString() ?? "";
            return checkBoxDict.ContainsKey(maChucNang);
        }

        // Trong hàm AddCheckBoxesToRow, BỎ cbXem:

        private void AddCheckBoxesToRow(DataGridViewCellPaintingEventArgs e)
        {
            Rectangle cellRect = e.CellBounds;
            int row = e.RowIndex;

            string maChucNang = tableChucNang.Rows[row].Tag?.ToString() ?? "";

            if (string.IsNullOrEmpty(maChucNang))
                return;

            if (!checkBoxDict.ContainsKey(maChucNang))
            {
                checkBoxDict[maChucNang] = new Dictionary<string, Guna.UI2.WinForms.Guna2CheckBox>();

                // Checkbox Thêm
                var cbThem = new Guna.UI2.WinForms.Guna2CheckBox();
                cbThem.Text = "Thêm";
                cbThem.AutoSize = true;
                cbThem.Tag = new CheckBoxTag { Row = row, Action = "them", MaChucNang = maChucNang };
                cbThem.Location = new Point(cellRect.X + 5, cellRect.Y + 6);
                checkBoxDict[maChucNang]["them"] = cbThem;
                tableChucNang.Controls.Add(cbThem);
                cbThem.BringToFront();

                if (maChucNang == "qlxeploai")
                {
                    cbThem.Enabled = false;
                    cbThem.Checked = false;
                    cbThem.ForeColor = Color.Gray;
                }

                // Checkbox Sửa
                var cbSua = new Guna.UI2.WinForms.Guna2CheckBox();
                cbSua.Text = "Sửa";
                cbSua.AutoSize = true;
                cbSua.Tag = new CheckBoxTag { Row = row, Action = "sua", MaChucNang = maChucNang };
                cbSua.Location = new Point(cellRect.X + 75, cellRect.Y + 6);
                checkBoxDict[maChucNang]["sua"] = cbSua;
                tableChucNang.Controls.Add(cbSua);
                cbSua.BringToFront();

                // ✅ Disable checkbox Sửa cho Năm học, Phân công, và Thời khóa biểu
                if (maChucNang == "qlnamhoc" || maChucNang == "qlphancong" || maChucNang == "qltkb")
                {
                    cbSua.Enabled = false;
                    cbSua.Checked = false;
                    cbSua.ForeColor = Color.Gray;
                }

                // Checkbox Xóa
                var cbXoa = new Guna.UI2.WinForms.Guna2CheckBox();
                cbXoa.Text = "Xóa";
                cbXoa.AutoSize = true;
                cbXoa.Tag = new CheckBoxTag { Row = row, Action = "xoa", MaChucNang = maChucNang };
                cbXoa.Location = new Point(cellRect.X + 130, cellRect.Y + 6);
                checkBoxDict[maChucNang]["xoa"] = cbXoa;

                // ✅ Disable checkbox Xóa cho Điểm số và Hạnh kiểm
                if (maChucNang == "qldiem" || maChucNang == "qlhanhkiem" || maChucNang == "qlxeploai")
                {
                    cbXoa.Enabled = false;
                    cbXoa.Checked = false;
                    cbXoa.ForeColor = Color.Gray;
                }

                tableChucNang.Controls.Add(cbXoa);
                cbXoa.BringToFront();
            }
            else
            {
                // Cập nhật vị trí nếu đã tồn tại
                if (checkBoxDict[maChucNang].ContainsKey("them"))
                {
                    checkBoxDict[maChucNang]["them"].Location = new Point(cellRect.X + 5, cellRect.Y + 6);
                    checkBoxDict[maChucNang]["them"].Visible = true;
                    checkBoxDict[maChucNang]["them"].BringToFront();
                }
                if (checkBoxDict[maChucNang].ContainsKey("sua"))
                {
                    checkBoxDict[maChucNang]["sua"].Location = new Point(cellRect.X + 75, cellRect.Y + 6);
                    checkBoxDict[maChucNang]["sua"].Visible = true;
                    checkBoxDict[maChucNang]["sua"].BringToFront();
                }
                if (checkBoxDict[maChucNang].ContainsKey("xoa"))
                {
                    checkBoxDict[maChucNang]["xoa"].Location = new Point(cellRect.X + 130, cellRect.Y + 6);
                    checkBoxDict[maChucNang]["xoa"].Visible = true;
                    checkBoxDict[maChucNang]["xoa"].BringToFront();
                }
            }
        }


        private void tableChucNang_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.ColumnIndex == tableChucNang.Columns["hanhDong"].Index && e.RowIndex >= 0)
            {
                // Không vẽ checkbox nếu dòng này không có dữ liệu
                if (tableChucNang.Rows[e.RowIndex].Cells["chucNang"].Value == null ||
                    string.IsNullOrWhiteSpace(tableChucNang.Rows[e.RowIndex].Cells["chucNang"].Value.ToString()))
                {
                    return;
                }

                e.PaintBackground(e.CellBounds, true);
                e.PaintContent(e.CellBounds);

                AddCheckBoxesToRow(e);

                e.Handled = true;
            }
        }




        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableChucNang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
public class CheckBoxTag
{
    public int Row { get; set; }
    public string Action { get; set; }
    public string MaChucNang { get; set; }
}