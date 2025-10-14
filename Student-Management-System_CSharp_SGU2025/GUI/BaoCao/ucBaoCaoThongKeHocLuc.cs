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
    public partial class ucBaoCaoThongKeHocLuc : UserControl
    {
        public ucBaoCaoThongKeHocLuc()
        {
            InitializeComponent();
            
        }

        

        private void BtnExportStatistics_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất báo cáo tổng hợp đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
