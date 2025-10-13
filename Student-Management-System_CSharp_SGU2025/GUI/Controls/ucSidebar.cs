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
        public Guna.UI2.WinForms.Guna2Button BaoCaoButton => btnBaoCao;
        public Guna.UI2.WinForms.Guna2Button HanhKiemButton => btnHanhKiem;
        public Guna.UI2.WinForms.Guna2Button HocSinhButton => btnHocSinh;
        public Guna.UI2.WinForms.Guna2Button DiemSoButton => btnDiemSo;
        public Guna.UI2.WinForms.Guna2Button LopHocButton => btnLopHoc;
        public Guna.UI2.WinForms.Guna2Button MonHocButton => btnMonHoc;
        public Guna.UI2.WinForms.Guna2Button PhanCongButton => btnPhanCong;
        public Guna.UI2.WinForms.Guna2Button ThoiKhoaBieuButton => btnThoiKhoaBieu;
    
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
    }
}
