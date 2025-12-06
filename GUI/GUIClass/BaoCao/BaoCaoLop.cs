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
   

    public partial class BaoCaoLop : Form
    {
        private PhanLopDAO phanLopDAO;
        private HocSinhDAO hocSinhDAO;
        private int maLop;
        private int maHocKy;
        private string tenLop;
        private List<HocSinhDTO> danhSachHocSinh;
        public event EventHandler OnClose;
        public BaoCaoLop()
        {
            InitializeComponent();

           
            this.FormBorderStyle = FormBorderStyle.Sizable; // Cho phép resize
            this.StartPosition = FormStartPosition.CenterParent; // Căn giữa parent form
            this.ShowInTaskbar = false;
            this.MinimizeBox = false;
            this.MaximizeBox = true;
            this.KeyPreview = true;
            tbHocSinh.CellFormatting += tbHocSinh_CellFormatting;

            phanLopDAO = new PhanLopDAO();
            hocSinhDAO = new HocSinhDAO();
            danhSachHocSinh = new List<HocSinhDTO>();

            // Xử lý phím ESC
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };
        }

        /// <summary>
        /// Thiết lập thông tin lớp và tải dữ liệu học sinh
        /// </summary>
        public void SetThongTinLop(int maLop, string tenLop, int maHocKy)
        {
            this.maLop = maLop;
            this.tenLop = tenLop;
            this.maHocKy = maHocKy;

            // Cập nhật tiêu đề
            lblTitle.Text = $"Danh sách học sinh lớp {tenLop}";

            // Load danh sách học sinh
            LoadDanhSachHocSinh();
        }

        /// <summary>
        /// Load danh sách học sinh theo lớp và học kỳ
        /// </summary>
        private void LoadDanhSachHocSinh()
        {
            try
            {
                // Lấy danh sách học sinh trong lớp
                danhSachHocSinh = phanLopDAO.LayDanhSachHocSinhTrongLop(maLop, maHocKy);

                // Sắp xếp theo mã học sinh
                danhSachHocSinh = danhSachHocSinh.OrderBy(hs => hs.MaHS).ToList();

                // Hiển thị lên DataGridView
                HienThiDanhSach(danhSachHocSinh);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách học sinh: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị danh sách học sinh lên DataGridView
        /// </summary>
        private void HienThiDanhSach(List<HocSinhDTO> dsHocSinh)
        {
            tbHocSinh.Rows.Clear();

            foreach (var hs in dsHocSinh)
            {
                tbHocSinh.Rows.Add(
                    hs.MaHS,
                    hs.HoTen,
                    hs.NgaySinh.ToString("dd/MM/yyyy"),
                    hs.GioiTinh,
                    hs.SdtHS ?? "",
                    hs.Email ?? "",
                    hs.TrangThai
                );
            }
        }

        /// <summary>
        /// Format các cell trong DataGridView (màu sắc cho giới tính và trạng thái)
        /// </summary>
        private void tbHocSinh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột Giới tính (index 3)
            if (e.ColumnIndex == 3 && e.Value != null)
            {
                string gioiTinh = e.Value.ToString();
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                e.CellStyle.Padding = new Padding(5, 3, 5, 3);

                if (gioiTinh == "Nam")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(29, 78, 216);
                    e.CellStyle.BackColor = Color.FromArgb(219, 234, 254);
                }
                else if (gioiTinh == "Nữ")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(190, 24, 93);
                    e.CellStyle.BackColor = Color.FromArgb(253, 232, 255);
                }
            }

            // Format cột Trạng thái (index 6)
            if (e.ColumnIndex == 6 && e.Value != null)
            {
                string trangThai = e.Value.ToString();
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                e.CellStyle.Padding = new Padding(5, 3, 5, 3);

                switch (trangThai)
                {
                    case "Đang học":
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                        e.CellStyle.BackColor = Color.FromArgb(220, 252, 231);
                        break;
                    case "Nghỉ học":
                        e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                        e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                        break;
                    case "Bảo lưu":
                        e.CellStyle.ForeColor = Color.FromArgb(194, 65, 12);
                        e.CellStyle.BackColor = Color.FromArgb(255, 237, 213);
                        break;
                    case "Thôi học":
                        e.CellStyle.ForeColor = Color.FromArgb(107, 114, 128);
                        e.CellStyle.BackColor = Color.FromArgb(243, 244, 246);
                        break;
                }
            }

            // Format cột Mã HS và Ngày sinh căn giữa
            if (e.ColumnIndex == 0 || e.ColumnIndex == 2)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void tbHocSinh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BaoCaoLop_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Xử lý phím ESC để đóng form
        /// </summary>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void BaoCaoLop_Load_1(object sender, EventArgs e)
        {

        }
    }
}
