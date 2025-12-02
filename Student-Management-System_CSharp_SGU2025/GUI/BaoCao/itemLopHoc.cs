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

namespace Student_Management_System_CSharp_SGU2025.GUI.BaoCao
{
    public partial class itemLopHoc : UserControl
    {

        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public int SiSo { get; set; }
        public string TenGVCN { get; set; }

        public itemLopHoc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Thiết lập thông tin hiển thị cho item lớp học
        /// </summary>
        public void SetClassInfo(int maLop, string tenLop, int siSo, string tenGVCN)
        {
            MaLop = maLop;
            TenLop = tenLop;
            SiSo = siSo;
            TenGVCN = tenGVCN;

            // Cập nhật giao diện
            lblClassName.Text = tenLop;
            lblClassInfo.Text = $"Sĩ số: {siSo} học sinh - GVCN: {tenGVCN}";
        }

        /// <summary>
        /// Thiết lập thông tin từ DTO
        /// </summary>
        public void SetClassInfo(LopDTO lop, string tenGVCN, int siSo)
        {
            SetClassInfo(lop.maLop, lop.tenLop, siSo, tenGVCN);
        }

        private void pnlClass1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblClassName_Click(object sender, EventArgs e)
        {

        }

        private void lblClassInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            // TODO: Xử lý sự kiện xem chi tiết lớp
            MessageBox.Show($"Xem chi tiết lớp {TenLop}\nMã lớp: {MaLop}",
                "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
