using Student_Management_System_CSharp_SGU2025.BUS;
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
namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class ucXepLoai : UserControl
    {
        private HocKyDAO hocKyDAO;
        private NamHocDAO namHocDAO;
        private XepLoaiDAO xepLoaiDAO;
        private XepLoaiBUS xepLoaiBUS;
        public ucXepLoai()
        {
            InitializeComponent();
            hocKyDAO = new HocKyDAO();
            namHocDAO = new NamHocDAO();
            xepLoaiDAO = new XepLoaiDAO();
            xepLoaiBUS = new XepLoaiBUS();
            LoadHocKy();
        }

        private void LoadSampleData()
        {
            // Thêm dữ liệu mẫu vào DataGridView
            tableXepLoai.Rows.Add("","Nguyễn Văn An", "10A1", "8.5", "Tốt", "Giỏi", "Giỏi");
            tableXepLoai.Rows.Add("","Trần Thị Bình", "10A1", "9.2", "Tốt", "Giỏi", "Giỏi");
            tableXepLoai.Rows.Add("","Lê Hoàng Cường", "10A2", "7.8", "Khá", "Khá", "Khá");
            tableXepLoai.Rows.Add("","Phạm Thị Dung", "10A2", "8.8", "Tốt", "Giỏi", "Giỏi");
            tableXepLoai.Rows.Add("", "Hoàng Văn Em", "11A1", "6.5", "Khá", "Trung bình", "Trung bình");
            tableXepLoai.Rows.Add("", "Vũ Thị Hoa", "11A1", "7.5", "Khá", "Khá", "Khá");
            tableXepLoai.Rows.Add("", "Vũ Thị A", "11A2", "9.5", "Tốt", "Giỏi", "Giỏi");
        }

        /// <summary>
        /// Load danh sách học kỳ vào combobox
        /// </summary>
        private void LoadHocKy()
        {
            try
            {
                cbHocKyNamHoc.Items.Clear();
                cbHocKyNamHoc.DisplayMember = "Text";
                cbHocKyNamHoc.ValueMember = "Value";

                List<HocKyDTO> dsHocKy = hocKyDAO.GetAllHocKy();

                foreach (var hk in dsHocKy)
                {
                    string displayText = $"{hk.TenHocKy} - {hk.MaNamHoc}";
                    cbHocKyNamHoc.Items.Add(new { Text = displayText, Value = hk.MaHocKy });
                }

                if (cbHocKyNamHoc.Items.Count > 0)
                {
                    cbHocKyNamHoc.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách học kỳ: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Load danh sách lớp theo học kỳ được chọn
        /// </summary>
        private void LoadLopTheoHocKy(int maHocKy)
        {
            try
            {
                cbLop.Items.Clear();
                cbLop.DisplayMember = "Text";
                cbLop.ValueMember = "Value";

                // Thêm tùy chọn "Tất cả lớp"
                cbLop.Items.Add(new { Text = "Tất cả lớp", Value = 0 });

                List<LopDTO> dsLop = xepLoaiDAO.GetDanhSachLopTheoHocKy(maHocKy);

                foreach (var lop in dsLop)
                {
                    cbLop.Items.Add(new { Text = lop.TenLop, Value = lop.MaLop });
                }

                if (cbLop.Items.Count > 0)
                {
                    cbLop.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách lớp: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load dữ liệu xếp loại vào DataGridView
        /// </summary>
        //private void LoadDuLieuXepLoai()
        //{
        //    try
        //    {
        //        if (cbHocKyNamHoc.SelectedItem == null)
        //        {
        //            tableXepLoai.Rows.Clear();
        //            return;
        //        }

        //        dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
        //        int maHocKy = selectedHocKy.Value;

        //        int? maLop = null;
        //        if (cbLop.SelectedItem != null)
        //        {
        //            dynamic selectedLop = cbLop.SelectedItem;
        //            int lopValue = selectedLop.Value;
        //            if (lopValue > 0)
        //            {
        //                maLop = lopValue;
        //            }
        //        }

        //        List<XepLoaiDTO> dsXepLoai = xepLoaiDAO.GetDanhSachXepLoai(maHocKy, maLop);

        //        tableXepLoai.Rows.Clear();
        //        foreach (var item in dsXepLoai)
        //        {
        //            tableXepLoai.Rows.Add(
        //                item.MaHocSinh,
        //                item.HoTen,
        //                item.TenLop,
        //                item.DiemTB.HasValue ? item.DiemTB.Value.ToString("0.0") : "",
        //                "", // Hành kiểm - để trống
        //                item.HocLuc,
        //                "" // Xếp loại - để trống
        //            );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi tải dữ liệu xếp loại: {ex.Message}",
        //            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void LoadDuLieuXepLoai()
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    tableXepLoai.Rows.Clear();
                    return;
                }

                dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                int? maLop = null;
                if (cbLop.SelectedItem != null)
                {
                    dynamic selectedLop = cbLop.SelectedItem;
                    int lopValue = selectedLop.Value;
                    if (lopValue > 0)
                    {
                        maLop = lopValue;
                    }
                }

                // Lấy danh sách đầy đủ (có cả hạnh kiểm và xếp loại)
                List<XepLoaiDTO> dsXepLoai = xepLoaiBUS.LayDanhSachXepLoaiDayDu(maHocKy, maLop);

                tableXepLoai.Rows.Clear();
                foreach (var item in dsXepLoai)
                {
                    tableXepLoai.Rows.Add(
                        item.MaHocSinh,
                        item.HoTen,
                        item.TenLop,
                        item.DiemTB.HasValue ? item.DiemTB.Value.ToString("0.0") : "",
                        item.HanhKiem ?? "", // Cột Hành kiểm
                        item.HocLuc ?? "",
                        item.XepLoaiTongKet ?? "" // Cột Xếp loại
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu xếp loại: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //// Column5 là cột "Học lực" (index 5)
            //if (tableXepLoai.Columns[e.ColumnIndex].Name == "Column5" ||
            //    tableXepLoai.Columns[e.ColumnIndex].Name == "hocLuc")
            //{
            //    if (e.Value != null)
            //    {
            //        string cellValue = e.Value.ToString();
            //        switch (cellValue)
            //        {
            //            case "Giỏi":
            //                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            //                break;
            //            case "Khá":
            //                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
            //                break;
            //            case "Trung bình":
            //                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(194, 65, 12);
            //                break;
            //            case "Yếu":
            //            case "Kém":
            //                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            //                break;
            //        }
            //    }
            //}

            // Tô màu cột Học lực (index 5)
            if (e.ColumnIndex == 5 && e.Value != null)
            {
                ApplyColorFormatting(e, e.Value.ToString());
            }

            // Tô màu cột Hạnh kiểm (index 4)
            if (e.ColumnIndex == 4 && e.Value != null)
            {
                ApplyColorFormatting(e, e.Value.ToString());
            }

            // Tô màu cột Xếp loại (index 6)
            if (e.ColumnIndex == 6 && e.Value != null)
            {
                ApplyColorFormatting(e, e.Value.ToString());
            }

        }

        private void ApplyColorFormatting(DataGridViewCellFormattingEventArgs e, string value)
        {
            switch (value)
            {
                case "Giỏi":
                case "Tốt":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
                    break;
                case "Khá":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(29, 78, 216);
                    break;
                case "Trung bình":
                case "Trung Bình":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(194, 65, 12);
                    break;
                case "Yếu":
                case "Kém":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
                    break;
            }
        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHocKyNamHoc.SelectedItem != null)
            {
                dynamic selected = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selected.Value;
                LoadLopTheoHocKy(maHocKy);
                LoadDuLieuXepLoai();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void ucXepLoai_Load(object sender, EventArgs e)
        {

        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDuLieuXepLoai();
        }

        private void panelTrong_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLuuTongKet_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbHocKyNamHoc.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn học kỳ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dynamic selectedHocKy = cbHocKyNamHoc.SelectedItem;
                int maHocKy = selectedHocKy.Value;

                // Đếm số học sinh có đủ điều kiện
                int soHocSinhDuDieuKien = 0;
                int soLuuThanhCong = 0;
                int soLuuThatBai = 0;
                int soThieuDieuKien = 0;

                foreach (DataGridViewRow row in tableXepLoai.Rows)
                {
                    if (row.IsNewRow) continue;

                    int maHocSinh = Convert.ToInt32(row.Cells[0].Value);
                    string hanhKiem = row.Cells[4].Value?.ToString() ?? "";
                    string hocLuc = row.Cells[5].Value?.ToString() ?? "";
                    string xepLoai = row.Cells[6].Value?.ToString() ?? "";

                    // Chỉ lưu nếu có đủ hạnh kiểm và học lực
                    if (!string.IsNullOrEmpty(hanhKiem) && !string.IsNullOrEmpty(hocLuc) &&
                        !string.IsNullOrEmpty(xepLoai))
                    {
                        soHocSinhDuDieuKien++;

                        if (xepLoaiBUS.LuuXepLoai(maHocSinh, maHocKy, xepLoai, ""))
                        {
                            soLuuThanhCong++;
                        }
                        else
                        {
                            soLuuThatBai++;
                        }
                    }
                    else
                    {
                        soThieuDieuKien++;
                    }
                }

                string thongBao = $"Kết quả lưu xếp loại tổng kết:\n" +
                                 $"- Lưu thành công: {soLuuThanhCong} học sinh\n";

                if (soLuuThatBai > 0)
                {
                    thongBao += $"- Lưu thất bại: {soLuuThatBai} học sinh\n";
                }

                if (soThieuDieuKien > 0)
                {
                    thongBao += $"- Chưa đủ điều kiện (thiếu học lực hoặc hạnh kiểm): {soThieuDieuKien} học sinh\n";
                }

                MessageBox.Show(thongBao,
                    soLuuThatBai == 0 ? "Thành công" : "Thông báo",
                    MessageBoxButtons.OK,
                    soLuuThatBai == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu xếp loại: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
