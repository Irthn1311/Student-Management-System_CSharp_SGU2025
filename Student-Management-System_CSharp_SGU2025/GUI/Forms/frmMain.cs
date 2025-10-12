using System;
using System.Drawing;
using System.Windows.Forms;

// Đảm bảo bạn đã thêm các using cho các UserControl của mình, ví dụ:
// using Student_Management_System_CSharp_SGU2025.GUI.userControl; 
// using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;

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
            // Đặt trang mặc định là Dashboard khi form khởi chạy
            ShowDashboard();
        }

        private void InitializeNavigation()
        {
            // Gán sự kiện cho các nút ở sidebar
            ucSidebar1.BangTinButton.Click += (s, e) => ShowDashboard();
            ucSidebar1.XepLoaiButton.Click += (s, e) => ShowXepLoai();
            ucSidebar1.BaoCaoButton.Click += (s, e) => ShowBaoCao();
            ucSidebar1.HanhKiemButton.Click += (s, e) => ShowHanhKiem();
            ucSidebar1.HocSinhButton.Click += (s, e) => ShowHocSinh();
            ucSidebar1.DiemSoButton.Click += (s, e) => ShowDiemSo();
            ucSidebar1.LopHocButton.Click += (s, e) => ShowLopKhoi();
            ucSidebar1.MonHocButton.Click += (s, e) => ShowFrmMonHoc();
            ucSidebar1.PhanCongButton.Click += (s, e) => ShowPhanCongGiangDay();
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

        // --- Các phương thức Show... bây giờ rất gọn gàng ---

        private void ShowDashboard()
        {
            ucHeader1.UpdateHeader("Bảng tin", "Trang chủ / Bảng tin");
            LoadControlToPanel<ucDashboard>();
        }

        private void ShowXepLoai()
        {
            ucHeader1.UpdateHeader("Xếp loại & Tổng kết", "Trang chủ / Xếp loại & Tổng kết");
            LoadControlToPanel<ucXepLoai>();
        }

        private void ShowBaoCao()
        {
            ucHeader1.UpdateHeader("Báo cáo", "Trang chủ / Báo cáo");
            LoadControlToPanel<ucBaoCao>();
        }

        private void ShowHanhKiem()
        {
            ucHeader1.UpdateHeader("Hạnh kiểm", "Trang chủ / Hạnh kiểm");
            LoadControlToPanel<HanhKiem>();
        }

        private void ShowHocSinh()
        {
            ucHeader1.UpdateHeader("Hồ sơ Học sinh", "Trang chủ / Hồ sơ học sinh");
            LoadControlToPanel<HocSinh>();
        }

        private void ShowDiemSo()
        {
            ucHeader1.UpdateHeader("Điểm số", "Trang chủ / Điểm số");
            LoadControlToPanel<DiemSo_NhapDiem>();
        }

        private void ShowLopKhoi()
        {
            ucHeader1.UpdateHeader("Lớp học", "Trang chủ / Lớp học");
            LoadControlToPanel<LopKhoi>();
        }

        private void ShowFrmMonHoc()
        {
            ucHeader1.UpdateHeader("Môn học", "Trang chủ / Môn học");
            LoadControlToPanel<FrmMonHoc>();
        }

        private void ShowPhanCongGiangDay()
        {
            ucHeader1.UpdateHeader("Phân công giảng dạy", "Trang chủ / Phân công giảng dạy");
            LoadControlToPanel<Student_Management_System_CSharp_SGU2025.GUI.statcardLHP.PhanCongGiangDay>();
        }
    }
}