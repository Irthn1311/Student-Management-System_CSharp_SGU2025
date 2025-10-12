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
        // Add this field to your MainForm class
        // Đúng namespace và class
        private Student_Management_System_CSharp_SGU2025.GUI.ThoiKhoaBieu.ThoiKhoaBieu ucThoiKhoaBieu1;

        public MainForm()
        {
            InitializeComponent();
            InitializeNavigation();

            // Add this initialization in your MainForm constructor after InitializeComponent();
            ucThoiKhoaBieu1 = new Student_Management_System_CSharp_SGU2025.GUI.ThoiKhoaBieu.ThoiKhoaBieu();
            ucThoiKhoaBieu1.Visible = false;
            ucThoiKhoaBieu1.Dock = DockStyle.Fill;
            this.Controls.Add(ucThoiKhoaBieu1);

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
            ucSidebar1.ThoiKhoaBieuButton.Click += BtnThoiKhoaBieu_Click;
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

        private void ShowDashboard()
        {
            // Hide all content panels
            ucDashboard1.Visible = true;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Bảng tin", "Trang chủ / Bảng tin");

            // Bring to front
            ucDashboard1.BringToFront();
        }

        private void ShowXepLoai()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = true;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Xếp loại & Tổng kết", "Trang chủ / Xếp loại & Tổng kết");

            // Bring to front
            ucXepLoai1.BringToFront();
        }

        private void ShowBaoCao()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = true;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Báo cáo", "Trang chủ / Báo cáo");

            // Bring to front
            ucBaoCao1.BringToFront();
        }

        private void ShowHanhKiem()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = true;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Hạnh kiểm", "Trang chủ / Hạnh kiểm");

            // Bring to front
            ucHanhKiem1.BringToFront();
        }

        private void ShowHocSinh()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = true;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Hồ sơ Học sinh", "Trang chủ / Hồ sơ học sinh");

            // Bring to front
            ucHocSinh1.BringToFront();
        }

        private void ShowDiemSo()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = true;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Điểm số", "Trang chủ / Điểm số");

            // Bring to front
            ucDiemSo1.BringToFront();
        }

        private void ShowLopKhoi()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = true;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Lớp học", "Trang chủ / Lớp học");

            // Bring to front
            ucLopKhoi1.BringToFront();
        }

        private void ShowFrmMonHoc()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = true;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Môn học", "Trang chủ / Môn học");

            // Bring to front
            ucFrmMonHoc1.BringToFront();
        }

        private void ShowPhanCongGiangDay()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = true;
            ucThoiKhoaBieu1.Visible = false;

            // Update header
            ucHeader1.UpdateHeader("Phân công giảng dạy", "Trang chủ / Phân công giảng dạy");

            // Bring to front
            ucPhanCongGiangDay1.BringToFront();
        }

        private void ucDashboard1_Load(object sender, EventArgs e)
        {

        }

        private void ucSidebar1_Load(object sender, EventArgs e)
        {

        }// Add this method to your MainForm class

        private void ShowThoiKhoaBieu()
        {
            // Hide all content panels
            ucDashboard1.Visible = false;
            ucXepLoai1.Visible = false;
            ucBaoCao1.Visible = false;
            ucHanhKiem1.Visible = false;
            ucHocSinh1.Visible = false;
            ucDiemSo1.Visible = false;
            ucLopKhoi1.Visible = false;
            ucFrmMonHoc1.Visible = false;
            ucPhanCongGiangDay1.Visible = false;
            ucThoiKhoaBieu1.Visible = true;
            
            // Add your timetable user control here, for example:
            // ucThoiKhoaBieu1.Visible = true;

            // Update header
            ucHeader1.UpdateHeader("Thời khóa biểu", "Trang chủ / Thời khóa biểu");

            // Bring to front
            ucThoiKhoaBieu1.BringToFront();
        }
        private void BtnThoiKhoaBieu_Click(object sender, EventArgs e)
        {
            ShowThoiKhoaBieu();
        }
    }
}
