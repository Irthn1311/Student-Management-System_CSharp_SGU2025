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
    public partial class ThemHoSoHocSinh : Form
    {
        public ThemHoSoHocSinh()
        {
            InitializeComponent();
        }

        private void ThemHoSoHocSinh_Load(object sender, EventArgs e)
        {

        }

        private void PbNguoiDung_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh học sinh";
                ofd.Filter = "Hình ảnh (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    PbNguoiDung.Image = Image.FromFile(ofd.FileName);
                    PbNguoiDung.SizeMode = PictureBoxSizeMode.Zoom; // để ảnh hiển thị vừa khung
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
