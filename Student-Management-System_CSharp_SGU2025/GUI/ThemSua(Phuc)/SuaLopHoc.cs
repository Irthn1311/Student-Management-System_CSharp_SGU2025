using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.ThemSua_Phuc_
{
    public partial class SuaLopHoc : Form
    {
        public SuaLopHoc()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cập nhật lớp học đã thành công");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {this.Close();

        }

        private void SuaLopHoc_Load(object sender, EventArgs e)
        {
            lbHeader.BackColor = guna2HtmlLabel1.BackColor;
        }
    }
}
