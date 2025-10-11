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
    public partial class MainForm : Form
    {
        // Current active user control
        private UserControl currentUserControl = null;
        
        public MainForm()
        {
            InitializeComponent();
            InitializeNavigation();

            // Set default page to Dashboard
            ShowDashboard();
        }

        private void InitializeNavigation()
        {
            // Wire up sidebar button events using public properties
            ucSidebar1.BangTinButton.Click += BtnBangTin_Click;
            ucSidebar1.XepLoaiButton.Click += BtnXepLoai_Click;
            ucSidebar1.BaoCaoButton.Click += BtnBaoCao_Click;
            ucSidebar1.HanhKiemButton.Click += BtnHanhKiem_Click;
            ucSidebar1.HocSinhButton.Click += BtnHocSinh_Click;
            ucSidebar1.DiemSoButton.Click += BtnDiemSo_Click;
            ucSidebar1.LopHocButton.Click += BtnLopHoc_Click;
            ucSidebar1.MonHocButton.Click += BtnMonHoc_Click;
            ucSidebar1.PhanCongButton.Click += BtnPhanCong_Click;
        }


        private void BtnBangTin_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void BtnXepLoai_Click(object sender, EventArgs e)
        {
            ShowXepLoai();
        }

        private void BtnBaoCao_Click(object sender, EventArgs e)
        {
            ShowBaoCao();
        }

        private void BtnHanhKiem_Click(object sender, EventArgs e)
        {
            ShowHanhKiem();
        }

        private void BtnHocSinh_Click(object sender, EventArgs e)
        {
            ShowHocSinh();
        }

        private void BtnDiemSo_Click(object sender, EventArgs e)
        {
            ShowDiemSo();
        }

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

            // Bring to front
            dashboard.BringToFront();
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

            // Bring to front
            xepLoai.BringToFront();
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

            // Bring to front
            baoCao.BringToFront();
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

            // Bring to front
            hanhKiem.BringToFront();
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

            // Bring to front
            hocSinh.BringToFront();
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

            // Bring to front
            diemSo.BringToFront();
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

            // Bring to front
            lopKhoi.BringToFront();
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

            // Bring to front
            frmMonHoc.BringToFront();
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

            // Bring to front
            phanCongGiangDay.BringToFront();
        }

    }
}
