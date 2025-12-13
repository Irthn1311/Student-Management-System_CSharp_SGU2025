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
    public partial class frmEditPhanQuyen : Form
    {
        private PhanQuyenBUS phanQuyenBUS;
        private string maVaiTro;
        private Dictionary<string, Dictionary<string, Guna.UI2.WinForms.Guna2CheckBox>> checkBoxDict;

        public frmEditPhanQuyen(string maVaiTro)
        {
            InitializeComponent();
            this.maVaiTro = maVaiTro;
            phanQuyenBUS = new PhanQuyenBUS();
            checkBoxDict = new Dictionary<string, Dictionary<string, Guna.UI2.WinForms.Guna2CheckBox>>();
        }

        private void frmEditPhanQuyen_Load(object sender, EventArgs e)
        {
            // Load thông tin vai trò
            VaiTroDTO vaiTro = phanQuyenBUS.GetVaiTroByMa(maVaiTro);
            if (vaiTro != null)
            {
                txtTenPhanQuyen.Text = vaiTro.TenVaiTro;
                txtTenPhanQuyen.Enabled = false; // Không cho sửa tên vai trò
            }

            setUpTableChucNang();
            tableChucNang.CellPainting += tableChucNang_CellPainting;
            tableChucNang.Scroll += tableChucNang_Scroll;
            LoadChucNang();
        }

        private void frmEditPhanQuyen_Shown(object sender, EventArgs e)
        {
            // Load quyền sau khi form đã được hiển thị hoàn toàn
            LoadQuyenHienTai();
        }

        private void LoadQuyenHienTai()
        {
            try
            {
                // Lấy chi tiết quyền hiện tại của vai trò
                Dictionary<string, List<string>> chiTietVaiTro = phanQuyenBUS.GetChiTietVaiTro(maVaiTro);

                if (chiTietVaiTro.Count == 0)
                    return;

                // Force paint tất cả các cell để đảm bảo checkbox được tạo
                tableChucNang.Refresh();
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);

                // Scroll qua tất cả các row để trigger CellPainting và tạo checkbox
                if (tableChucNang.Rows.Count > 0)
                {
                    tableChucNang.FirstDisplayedScrollingRowIndex = 0;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                    
                    // Scroll xuống cuối để trigger paint cho tất cả các row
                    if (tableChucNang.Rows.Count > 1)
                    {
                        tableChucNang.FirstDisplayedScrollingRowIndex = tableChucNang.Rows.Count - 1;
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(100);
                        tableChucNang.FirstDisplayedScrollingRowIndex = 0;
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(100);
                    }
                }

                // Check các checkbox dựa trên quyền hiện tại
                foreach (var item in chiTietVaiTro)
                {
                    string tenChucNang = item.Key;
                    List<string> hanhDongs = item.Value;

                    // Tìm maChucNang từ tenChucNang
                    string maChucNang = GetMaChucNangFromTen(tenChucNang);
                    if (string.IsNullOrEmpty(maChucNang))
                        continue;

                    // Đợi checkbox được tạo (retry nhiều lần)
                    int retryCount = 0;
                    while (!checkBoxDict.ContainsKey(maChucNang) && retryCount < 30)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(50);
                        retryCount++;
                    }

                    if (checkBoxDict.ContainsKey(maChucNang))
                    {
                        var checkBoxes = checkBoxDict[maChucNang];

                        // Check các checkbox tương ứng với từng hành động
                        foreach (string hanhDong in hanhDongs)
                        {
                            string hanhDongLower = hanhDong.ToLower();
                            
                            // Xử lý "read" -> checkbox "Đọc"
                            if (hanhDongLower == "read" && checkBoxes.ContainsKey("doc"))
                            {
                                checkBoxes["doc"].Checked = true;
                            }
                            // Xử lý các hành động khác
                            else
                            {
                                string checkBoxKey = MapHanhDongToCheckBox(hanhDong);
                                if (!string.IsNullOrEmpty(checkBoxKey) && checkBoxes.ContainsKey(checkBoxKey))
                                {
                                    checkBoxes[checkBoxKey].Checked = true;
                                }
                            }
                        }
                    }
                }
                
                // Refresh lại để hiển thị
                tableChucNang.Invalidate();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load quyền hiện tại: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetMaChucNangFromTen(string tenChucNang)
        {
            foreach (DataGridViewRow row in tableChucNang.Rows)
            {
                if (row.Cells["chucNang"].Value?.ToString() == tenChucNang)
                {
                    return row.Tag?.ToString() ?? "";
                }
            }
            return "";
        }

        private string MapHanhDongToCheckBox(string hanhDong)
        {
            switch (hanhDong.ToLower())
            {
                case "create":
                    return "them";
                case "update":
                    return "sua";
                case "delete":
                    return "xoa";
                default:
                    return "";
            }
        }

        private void tableChucNang_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateAllCheckBoxPositions();
            tableChucNang.Invalidate();
        }

        private void UpdateAllCheckBoxPositions()
        {
            foreach (var maChucNangEntry in checkBoxDict)
            {
                string maChucNang = maChucNangEntry.Key;
                var checkBoxes = maChucNangEntry.Value;

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

                        bool isVisible = cellRect.Height > 0 && cellRect.Width > 0 &&
                                        cellRect.Y >= tableChucNang.ColumnHeadersHeight &&
                                        cellRect.Y < tableChucNang.Height;

                        if (checkBoxes.ContainsKey("doc"))
                        {
                            checkBoxes["doc"].Visible = isVisible;
                            if (isVisible)
                            {
                                checkBoxes["doc"].Location = new Point(cellRect.X + 5, cellRect.Y + 6);
                                checkBoxes["doc"].BringToFront();
                            }
                        }

                        if (checkBoxes.ContainsKey("them"))
                        {
                            checkBoxes["them"].Visible = isVisible;
                            if (isVisible)
                            {
                                checkBoxes["them"].Location = new Point(cellRect.X + 75, cellRect.Y + 6);
                                checkBoxes["them"].BringToFront();
                            }
                        }

                        if (checkBoxes.ContainsKey("sua"))
                        {
                            checkBoxes["sua"].Visible = isVisible;
                            if (isVisible)
                            {
                                checkBoxes["sua"].Location = new Point(cellRect.X + 145, cellRect.Y + 6);
                                checkBoxes["sua"].BringToFront();
                            }
                        }

                        if (checkBoxes.ContainsKey("xoa"))
                        {
                            checkBoxes["xoa"].Visible = isVisible;
                            if (isVisible)
                            {
                                checkBoxes["xoa"].Location = new Point(cellRect.X + 215, cellRect.Y + 6);
                                checkBoxes["xoa"].BringToFront();
                            }
                        }
                    }
                    catch
                    {
                        if (checkBoxes.ContainsKey("doc"))
                            checkBoxes["doc"].Visible = false;
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
                    if (checkBoxes.ContainsKey("doc"))
                        checkBoxes["doc"].Visible = false;
                    if (checkBoxes.ContainsKey("them"))
                        checkBoxes["them"].Visible = false;
                    if (checkBoxes.ContainsKey("sua"))
                        checkBoxes["sua"].Visible = false;
                    if (checkBoxes.ContainsKey("xoa"))
                        checkBoxes["xoa"].Visible = false;
                }
            }
        }

        private void btnUpdateQuyen_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Thu thập thông tin quyền từ các checkbox
                Dictionary<string, List<string>> danhSachQuyen = new Dictionary<string, List<string>>();

                foreach (var item in checkBoxDict)
                {
                    string maChucNang = item.Key;
                    var checkBoxes = item.Value;

                    List<string> hanhDongs = new List<string>();
                    bool coQuyenKhac = false;

                    foreach (var cb in checkBoxes)
                    {
                        if (cb.Value.Checked)
                        {
                            // Xử lý checkbox "Đọc"
                            if (cb.Key == "doc")
                            {
                                hanhDongs.Add("read");
                            }
                            else
                            {
                                string hanhDong = phanQuyenBUS.MapCheckBoxToHanhDong(cb.Key);
                                if (!string.IsNullOrEmpty(hanhDong))
                                {
                                    hanhDongs.Add(hanhDong);
                                    coQuyenKhac = true;
                                }
                            }
                        }
                    }

                    // Tự động thêm READ nếu có quyền khác nhưng chưa có read
                    if (coQuyenKhac && !hanhDongs.Contains("read"))
                    {
                        hanhDongs.Add("read");
                    }

                    if (hanhDongs.Count > 0)
                    {
                        danhSachQuyen.Add(maChucNang, hanhDongs);
                    }
                }

                // 2. Kiểm tra có quyền nào được chọn không
                if (danhSachQuyen.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một quyền cho vai trò!", "Cảnh báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Xác nhận trước khi cập nhật
                string thongTinQuyen = "";
                foreach (var item in danhSachQuyen)
                {
                    string tenChucNang = GetTenChucNangFromMa(item.Key);
                    List<string> quyenVN = item.Value.Select(h => phanQuyenBUS.MapHanhDongToVietnamese(h)).ToList();
                    thongTinQuyen += $"- {tenChucNang}: {string.Join(", ", quyenVN)}\n";
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn cập nhật quyền cho vai trò '{txtTenPhanQuyen.Text}' với các quyền sau:\n\n{thongTinQuyen}",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                // 4. Thực hiện cập nhật vào database
                bool success = phanQuyenBUS.CapNhatVaiTroVoiQuyen(maVaiTro, danhSachQuyen);

                if (success)
                {
                    MessageBox.Show("Cập nhật quyền thành công!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật quyền thất bại!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật quyền: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            tableChucNang.Columns.Add("chucNang", "Chức năng");
            tableChucNang.Columns.Add("hanhDong", "Hành động");

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

        private void LoadChucNang()
        {
            try
            {
                List<ChucNangDTO> danhSach = phanQuyenBUS.GetAllChucNang();
                tableChucNang.Rows.Clear();

                foreach (var cn in danhSach)
                {
                    if (cn.MaChucNang.ToLower() == "qlcaidat")
                        continue;
                    if (cn.MaChucNang.ToLower() == "qlyeucau_chuyenlop")
                        continue;

                    int rowIndex = tableChucNang.Rows.Add(cn.TenChucNang);
                    tableChucNang.Rows[rowIndex].Tag = cn.MaChucNang;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load danh sách chức năng: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                // Checkbox Đọc
                var cbDoc = new Guna.UI2.WinForms.Guna2CheckBox();
                cbDoc.Text = "Đọc";
                cbDoc.AutoSize = true;
                cbDoc.Tag = new CheckBoxTag { Row = row, Action = "doc", MaChucNang = maChucNang };
                cbDoc.Location = new Point(cellRect.X + 5, cellRect.Y + 6);
                checkBoxDict[maChucNang]["doc"] = cbDoc;
                tableChucNang.Controls.Add(cbDoc);
                cbDoc.BringToFront();
                cbDoc.Enabled = true; // Cho phép chọn/bỏ chọn

                // Checkbox Thêm
                var cbThem = new Guna.UI2.WinForms.Guna2CheckBox();
                cbThem.Text = "Thêm";
                cbThem.AutoSize = true;
                cbThem.Tag = new CheckBoxTag { Row = row, Action = "them", MaChucNang = maChucNang };
                cbThem.Location = new Point(cellRect.X + 75, cellRect.Y + 6);
                checkBoxDict[maChucNang]["them"] = cbThem;
                tableChucNang.Controls.Add(cbThem);
                cbThem.BringToFront();

                // ✅ Bỏ qlmonhoc khỏi danh sách disable - cho phép thêm môn học
                if (maChucNang == "qlxeploai" || maChucNang == "qlbaocao" || maChucNang == "qllophoc")
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
                cbSua.Location = new Point(cellRect.X + 145, cellRect.Y + 6);
                checkBoxDict[maChucNang]["sua"] = cbSua;
                tableChucNang.Controls.Add(cbSua);
                cbSua.BringToFront();

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
                cbXoa.Location = new Point(cellRect.X + 215, cellRect.Y + 6);
                checkBoxDict[maChucNang]["xoa"] = cbXoa;

                // ✅ Bỏ qlmonhoc khỏi danh sách disable - cho phép xóa môn học
                if (maChucNang == "qldiem" || maChucNang == "qlhanhkiem" || maChucNang == "qlxeploai" || maChucNang == "qltkb" || maChucNang == "qlbaocao")
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
                if (checkBoxDict[maChucNang].ContainsKey("doc"))
                {
                    checkBoxDict[maChucNang]["doc"].Location = new Point(cellRect.X + 5, cellRect.Y + 6);
                    checkBoxDict[maChucNang]["doc"].Visible = true;
                    checkBoxDict[maChucNang]["doc"].BringToFront();
                }
                if (checkBoxDict[maChucNang].ContainsKey("them"))
                {
                    checkBoxDict[maChucNang]["them"].Location = new Point(cellRect.X + 75, cellRect.Y + 6);
                    checkBoxDict[maChucNang]["them"].Visible = true;
                    checkBoxDict[maChucNang]["them"].BringToFront();
                }
                if (checkBoxDict[maChucNang].ContainsKey("sua"))
                {
                    checkBoxDict[maChucNang]["sua"].Location = new Point(cellRect.X + 145, cellRect.Y + 6);
                    checkBoxDict[maChucNang]["sua"].Visible = true;
                    checkBoxDict[maChucNang]["sua"].BringToFront();
                }
                if (checkBoxDict[maChucNang].ContainsKey("xoa"))
                {
                    checkBoxDict[maChucNang]["xoa"].Location = new Point(cellRect.X + 215, cellRect.Y + 6);
                    checkBoxDict[maChucNang]["xoa"].Visible = true;
                    checkBoxDict[maChucNang]["xoa"].BringToFront();
                }
            }
        }

        private void tableChucNang_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == tableChucNang.Columns["hanhDong"].Index && e.RowIndex >= 0)
            {
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

        private void btnToanQuyen_Click(object sender, EventArgs e)
        {
            try
            {
                bool tatCaDaChon = true;

                foreach (var maChucNangEntry in checkBoxDict)
                {
                    var checkBoxes = maChucNangEntry.Value;

                    if ((checkBoxes.ContainsKey("doc") && checkBoxes["doc"].Enabled && !checkBoxes["doc"].Checked) ||
                        (checkBoxes.ContainsKey("them") && checkBoxes["them"].Enabled && !checkBoxes["them"].Checked) ||
                        (checkBoxes.ContainsKey("sua") && checkBoxes["sua"].Enabled && !checkBoxes["sua"].Checked) ||
                        (checkBoxes.ContainsKey("xoa") && checkBoxes["xoa"].Enabled && !checkBoxes["xoa"].Checked))
                    {
                        tatCaDaChon = false;
                        break;
                    }
                }

                foreach (var maChucNangEntry in checkBoxDict)
                {
                    var checkBoxes = maChucNangEntry.Value;

                    if (checkBoxes.ContainsKey("doc") && checkBoxes["doc"].Enabled)
                        checkBoxes["doc"].Checked = !tatCaDaChon;
                    if (checkBoxes.ContainsKey("them") && checkBoxes["them"].Enabled)
                        checkBoxes["them"].Checked = !tatCaDaChon;
                    if (checkBoxes.ContainsKey("sua") && checkBoxes["sua"].Enabled)
                        checkBoxes["sua"].Checked = !tatCaDaChon;
                    if (checkBoxes.ContainsKey("xoa") && checkBoxes["xoa"].Enabled)
                        checkBoxes["xoa"].Checked = !tatCaDaChon;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn toàn quyền: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

