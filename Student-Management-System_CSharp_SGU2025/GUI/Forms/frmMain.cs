using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Student_Management_System_CSharp_SGU2025.GUI.Dashboard;

// Đảm bảo bạn đã thêm các using cho các UserControl của mình, ví dụ:
// using Student_Management_System_CSharp_SGU2025.GUI.userControl; 
// using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class MainForm : Form
    {
        // Current active user control
        private UserControl currentUserControl = null;
        private System.ComponentModel.IContainer components = null;


        public MainForm()
        {
            InitializeComponent();
            InitializeNavigation();
            // Đặt trang mặc định là Dashboard khi form khởi chạy
            ShowDashboard();
        }

        private void InitializeNavigation()
        {
            // Gán sự kiện cho các nút ở sidebar
            ucSidebar1.BangTinButton.Click += (s, e) => ShowDashboard();
            ucSidebar1.XepLoaiButton.Click += (s, e) => ShowXepLoai();
            ucSidebar1.GiaoVienButton.Click += (s, e) => ShowGiaoVien();
            ucSidebar1.BaoCaoButton.Click += (s, e) => ShowBaoCao();
            ucSidebar1.HanhKiemButton.Click += (s, e) => ShowHanhKiem();
            ucSidebar1.HocSinhButton.Click += (s, e) => ShowHocSinh();
            ucSidebar1.DiemSoButton.Click += (s, e) => ShowDiemSo();
            ucSidebar1.LopHocButton.Click += (s, e) => ShowLopKhoi();
            ucSidebar1.MonHocButton.Click += (s, e) => ShowFrmMonHoc();
            ucSidebar1.PhanCongButton.Click += (s, e) => ShowPhanCongGiangDay();
            ucSidebar1.NamHocButton.Click += (s, e) => ShowNamHoc();
            // Kết nối các form mới
            ucSidebar1.TaiKhoanButton.Click += (s, e) => ShowTaiKhoan();
            ucSidebar1.CaiDatButton.Click += (s, e) => ShowCaiDat();
            ucSidebar1.DanhGiaButton.Click += (s, e) => ShowDanhGia();
            ucSidebar1.ThoiKhoaBieuButton.Click += (s, e) => ShowThoiKhoaBieu();
        }

        private void XepLoaiButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Phương thức chung để tải một UserControl vào panelContent.
        /// </summary>
        /// <typeparam name="T">Loại UserControl cần tải.</typeparam>
        private void LoadControlToPanel<T>() where T : UserControl, new()
        {
            // 1. Xóa control hiện tại đang có trong panelContent (nếu có)
            if (panelContent.Controls.Count > 0)
            {
                // Lấy control cũ ra và giải phóng bộ nhớ
                Control oldControl = panelContent.Controls[0];
                panelContent.Controls.Remove(oldControl);
                oldControl.Dispose();
            }

            // 2. Tạo một thể hiện (instance) mới của UserControl bạn muốn hiển thị
            T newControl = new T();

            // 3. Quan trọng: Thiết lập để UserControl lấp đầy toàn bộ Panel
            newControl.Dock = DockStyle.Fill;

            // 4. Thêm UserControl mới này vào BÊN TRONG panelContent
            panelContent.Controls.Add(newControl);
        }

        // --- Các phương thức Show... 

        private void BtnLopHoc_Click(object sender, EventArgs e)
        {
            ShowLopKhoi();
        }

        private void BtnMonHoc_Click(object sender, EventArgs e)
        {
            ShowFrmMonHoc();
        }

        private void BtnPhanCong_Click(object sender, EventArgs e)
        {
            ShowPhanCongGiangDay();
        }

        private void DisposeCurrentUserControl()
        {
            if (currentUserControl != null)
            {
                this.Controls.Remove(currentUserControl);
                currentUserControl.Dispose();
                currentUserControl = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeCurrentUserControl();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void ShowDashboard()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show Dashboard
            var dashboard = new ucDashboard();
            dashboard.BackColor = Color.FromArgb(243, 244, 246);
            dashboard.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dashboard.Location = new Point(256, 80);
            dashboard.Margin = new Padding(2);
            dashboard.Name = "ucDashboard1";
            dashboard.Size = new Size(1184, 900);
            dashboard.TabIndex = 1;

            this.Controls.Add(dashboard);
            currentUserControl = dashboard;

            // Update header
            ucHeader1.UpdateHeader("Bảng tin", "Trang chủ / Bảng tin");
            LoadControlToPanel<Student_Management_System_CSharp_SGU2025.GUI.Dashboard.ucDashboard>();
        }

        private void ShowXepLoai()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show XepLoai
            var xepLoai = new ucXepLoai();
            xepLoai.AutoScroll = true;
            xepLoai.BackColor = Color.FromArgb(243, 244, 246);
            xepLoai.Location = new Point(256, 80);
            xepLoai.Margin = new Padding(2);
            xepLoai.Name = "ucXepLoai1";
            xepLoai.Padding = new Padding(24);
            xepLoai.Size = new Size(1184, 900);
            xepLoai.TabIndex = 3;

            this.Controls.Add(xepLoai);
            currentUserControl = xepLoai;

            // Update header
            ucHeader1.UpdateHeader("Xếp loại & Tổng kết", "Trang chủ / Xếp loại & Tổng kết");
            LoadControlToPanel<ucXepLoai>();
        }

        private void ShowBaoCao()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show BaoCao
            var baoCao = new ucBaoCao();
            baoCao.BackColor = Color.FromArgb(249, 250, 251);
            baoCao.Location = new Point(256, 80);
            baoCao.Margin = new Padding(2);
            baoCao.Name = "ucBaoCao1";
            baoCao.Size = new Size(1184, 900);
            baoCao.TabIndex = 4;

            this.Controls.Add(baoCao);
            currentUserControl = baoCao;

            // Update header
            ucHeader1.UpdateHeader("Báo cáo", "Trang chủ / Báo cáo");
            LoadControlToPanel<ucBaoCao>();
        }

        private void ShowHanhKiem()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show HanhKiem
            var hanhKiem = new HanhKiem();
            hanhKiem.BackColor = Color.FromArgb(243, 244, 246);
            hanhKiem.Location = new Point(256, 80);
            hanhKiem.Margin = new Padding(2);
            hanhKiem.Name = "ucHanhKiem1";
            hanhKiem.Size = new Size(1184, 900);
            hanhKiem.TabIndex = 5;

            this.Controls.Add(hanhKiem);
            currentUserControl = hanhKiem;

            // Update header
            ucHeader1.UpdateHeader("Hạnh kiểm", "Trang chủ / Hạnh kiểm");
            LoadControlToPanel<HanhKiem>();
        }

        private void ShowHocSinh()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show HocSinh
            var hocSinh = new HocSinh();
            hocSinh.BackColor = Color.FromArgb(243, 244, 246);
            hocSinh.Location = new Point(256, 80);
            hocSinh.Margin = new Padding(2);
            hocSinh.Name = "ucHocSinh1";
            hocSinh.Size = new Size(1184, 900);
            hocSinh.TabIndex = 6;

            this.Controls.Add(hocSinh);
            currentUserControl = hocSinh;

            // Update header
            ucHeader1.UpdateHeader("Hồ sơ Học sinh", "Trang chủ / Hồ sơ học sinh");
            LoadControlToPanel<HocSinh>();
        }

        private void ShowDiemSo()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show DiemSo
            var diemSo = new DiemSo_NhapDiem();
            diemSo.BackColor = Color.FromArgb(243, 244, 246);
            diemSo.Location = new Point(256, 80);
            diemSo.Margin = new Padding(2);
            diemSo.Name = "ucDiemSo1";
            diemSo.Size = new Size(1184, 900);
            diemSo.TabIndex = 7;

            this.Controls.Add(diemSo);
            currentUserControl = diemSo;

            // Update header
            ucHeader1.UpdateHeader("Điểm số", "Trang chủ / Điểm số");
            LoadControlToPanel<DiemSo_NhapDiem>();
        }

        private void ShowLopKhoi()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show LopKhoi
            var lopKhoi = new LopKhoi();
            lopKhoi.BackColor = Color.FromArgb(243, 244, 246);
            lopKhoi.Location = new Point(256, 80);
            lopKhoi.Margin = new Padding(2);
            lopKhoi.Name = "ucLopKhoi1";
            lopKhoi.Size = new Size(1184, 900);
            lopKhoi.TabIndex = 8;

            this.Controls.Add(lopKhoi);
            currentUserControl = lopKhoi;

            // Update header
            ucHeader1.UpdateHeader("Lớp học", "Trang chủ / Lớp học");
            LoadControlToPanel<LopKhoi>();
        }

        private void ShowFrmMonHoc()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show FrmMonHoc
            var frmMonHoc = new FrmMonHoc();
            frmMonHoc.BackColor = Color.FromArgb(243, 244, 246);
            frmMonHoc.Location = new Point(256, 80);
            frmMonHoc.Margin = new Padding(2);
            frmMonHoc.Name = "ucFrmMonHoc1";
            frmMonHoc.Size = new Size(1184, 900);
            frmMonHoc.TabIndex = 9;

            this.Controls.Add(frmMonHoc);
            currentUserControl = frmMonHoc;

            // Update header
            ucHeader1.UpdateHeader("Môn học", "Trang chủ / Môn học");
            LoadControlToPanel<FrmMonHoc>();
        }

        private void ShowPhanCongGiangDay()
        {
            // Dispose current user control if exists
            DisposeCurrentUserControl();

            // Create and show PhanCongGiangDay
            var phanCongGiangDay = new Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.PhanCongGiangDay();
            phanCongGiangDay.BackColor = Color.FromArgb(243, 244, 246);
            phanCongGiangDay.Location = new Point(256, 80);
            phanCongGiangDay.Margin = new Padding(2);
            phanCongGiangDay.Name = "ucPhanCongGiangDay1";
            phanCongGiangDay.Size = new Size(1184, 900);
            phanCongGiangDay.TabIndex = 10;

            this.Controls.Add(phanCongGiangDay);
            currentUserControl = phanCongGiangDay;

            // Update header
            ucHeader1.UpdateHeader("Phân công giảng dạy", "Trang chủ / Phân công giảng dạy");
            LoadControlToPanel<Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.PhanCongGiangDay>();
        }

        private void ShowGiaoVien()
        {
            ucHeader1.UpdateHeader("Giáo viên", "Trang chủ / Giáo viên");
            LoadControlToPanel<Student_Management_System_CSharp_SGU2025.GUI.GiaoVien.GiaoVien>();

        }

        private void ShowTaiKhoan()
        {
            ucHeader1.UpdateHeader("Tài khoản", "Trang chủ / Tài khoản");
            LoadControlToPanel<FrmTaiKhoan>();
        }

        private void ShowCaiDat()
        {
            ucHeader1.UpdateHeader("Cài đặt", "Trang chủ / Cài đặt");
            LoadControlToPanel<CaiDat>();
        }

        private void ShowDanhGia()
        {
            ucHeader1.UpdateHeader("Đánh giá", "Trang chủ / Đánh giá");
            LoadControlToPanel<DanhGia>();
        }

        private void ShowThoiKhoaBieu()
        {
            ucHeader1.UpdateHeader("Thời khóa biểu", "Trang chủ / Thời khóa biểu");
            LoadControlToPanel<Student_Management_System_CSharp_SGU2025.GUI.ThoiKhoaBieu.ThoiKhoaBieu>();
        }
        private void ShowNamHoc()
        {
            ucHeader1.UpdateHeader("Năm học", "Trang chủ / Năm học");
            LoadControlToPanel<Student_Management_System_CSharp_SGU2025.GUI.NamHoc.ucNamHoc>();
        }
        private void ucSidebar1_Load(object sender, EventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}