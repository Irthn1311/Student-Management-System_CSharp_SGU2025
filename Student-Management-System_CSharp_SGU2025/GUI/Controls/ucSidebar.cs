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
    public partial class ucSidebar : UserControl
    {
        public ucSidebar()
        {
            InitializeComponent();
        }

        // Public properties to expose buttons for better maintainability
        public Guna.UI2.WinForms.Guna2Button BangTinButton => btnBangTin;
        public Guna.UI2.WinForms.Guna2Button XepLoaiButton => btnXepLoai;
        public Guna.UI2.WinForms.Guna2Button GiaoVienButton => btnGiaoVien;
        public Guna.UI2.WinForms.Guna2Button BaoCaoButton => btnBaoCao;
        public Guna.UI2.WinForms.Guna2Button HanhKiemButton => btnHanhKiem;
        public Guna.UI2.WinForms.Guna2Button HocSinhButton => btnHocSinh;
        public Guna.UI2.WinForms.Guna2Button DiemSoButton => btnDiemSo;
        public Guna.UI2.WinForms.Guna2Button LopHocButton => btnLopHoc;
        public Guna.UI2.WinForms.Guna2Button MonHocButton => btnMonHoc;
        public Guna.UI2.WinForms.Guna2Button PhanCongButton => btnPhanCong;
        public Guna.UI2.WinForms.Guna2Button ThoiKhoaBieuButton => btnThoiKhoaBieu;
        public Guna.UI2.WinForms.Guna2Button TaiKhoanButton => btnTaiKhoan;
        public Guna.UI2.WinForms.Guna2Button CaiDatButton => btnCaiDat;
        public Guna.UI2.WinForms.Guna2Button DanhGiaButton => btnKhenThuong;
        public Guna.UI2.WinForms.Guna2Button ThongBaoButton => btnThongBao; 

    
        public Guna.UI2.WinForms.Guna2Button NamHocButton => btnNamHoc;
        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flpnlNav_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {

        }

        private void pnlLogout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

        }

        private void btnThongBao_Click(object sender, EventArgs e)
        {
        
        }
        private void btnNamHoc_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận đăng xuất
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Đóng MainForm hiện tại
                    Form mainForm = this.FindForm();
                    if (mainForm != null)
                    {
                        mainForm.Hide(); // Ẩn MainForm trước
                        
                        // Mở lại form đăng nhập
                        FrmDangNhap loginForm = new FrmDangNhap();
                        loginForm.FormClosed += (s, args) => 
                        {
                            // Khi form đăng nhập đóng, đóng luôn MainForm
                            mainForm.Close();
                        };
                        loginForm.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Đã xảy ra lỗi khi đăng xuất:\n{ex.Message}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
    }
}
