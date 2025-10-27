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
    public partial class frmPhanQuyen : Form
    {
        public frmPhanQuyen()
        {
            InitializeComponent();
        }

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            LoadRoles();
        }

        private void LoadRoles()
        {
            var roles = new List<(string Name, string Description)>
    {
        ("Admin", "Toàn quyền quản trị hệ thống"),
        ("Giáo vụ", "Quản lý học sinh, lớp học, điểm số"),
        ("Giáo viên", "Nhập điểm, xem thông tin lớp"),
        ("Học sinh", "Xem điểm, thời khóa biểu")
    };

            // Nếu bạn có sẵn 4 RoleItem (roleItem1...roleItem4)
            var items = new[] { roleItem1, roleItem2, roleItem3, roleItem4 };

            for (int i = 0; i < roles.Count; i++)
            {
                items[i].RoleName = roles[i].Name;
                items[i].RoleDescription = roles[i].Description;
            }
        }


        private void roleItem1_Load(object sender, EventArgs e)
        {

        }

        private void roleItem2_Load(object sender, EventArgs e)
        {

        }

        private void roleItem3_Load(object sender, EventArgs e)
        {

        }

        private void roleItem4_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roleItem1_Load_1(object sender, EventArgs e)
        {

        }

        private void roleItem2_Load_1(object sender, EventArgs e)
        {

        }

        private void roleItem3_Load_1(object sender, EventArgs e)
        {

        }

        private void roleItem4_Load_1(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Tạo instance của form thêm quyền
            frmAddPhanQuyen addRoleForm = new frmAddPhanQuyen();

            // Căn giữa form con so với form cha
            addRoleForm.StartPosition = FormStartPosition.CenterParent;

            // Hiển thị form dưới dạng hộp thoại modal
            addRoleForm.ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
