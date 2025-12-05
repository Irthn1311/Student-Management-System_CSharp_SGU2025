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
        public int MaHocKy { get; set; }

        public event EventHandler<ClassViewEventArgs> OnViewClassDetails;


        public itemLopHoc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Thiết lập thông tin hiển thị cho item lớp học
        /// </summary>
        public void SetClassInfo(int maLop, string tenLop, int siSo, string tenGVCN, int maHocKy)
        {
            MaLop = maLop;
            TenLop = tenLop;
            SiSo = siSo;
            TenGVCN = tenGVCN;
            MaHocKy = maHocKy;

            // Cập nhật giao diện
            lblClassName.Text = tenLop;
            lblClassInfo.Text = $"Sĩ số: {siSo} học sinh - GVCN: {tenGVCN}";
        }

        /// <summary>
        /// Thiết lập thông tin từ DTO
        /// </summary>
        public void SetClassInfo(LopDTO lop, string tenGVCN, int siSo, int maHocKy)
        {
            SetClassInfo(lop.maLop, lop.tenLop, siSo, tenGVCN, maHocKy);
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
            // Kích hoạt event để thông báo cho ucBaoCao
            OnViewClassDetails?.Invoke(this, new ClassViewEventArgs
            {
                MaLop = this.MaLop,
                TenLop = this.TenLop,
                MaHocKy = this.MaHocKy
            });
        }
    }
    // Class để truyền dữ liệu qua event
    public class ClassViewEventArgs : EventArgs
    {
        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public int MaHocKy { get; set; }
    }
}
