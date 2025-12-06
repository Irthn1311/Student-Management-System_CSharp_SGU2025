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
    public partial class ThongKeXepLoai : UserControl
    {
        public ThongKeXepLoai()
        {
            InitializeComponent();
        }

        // TieuDe1 (tieuDe1)
        public string TieuDe1
        {
            get => tieuDe1.Text;
            set => tieuDe1.Text = value;
        }

        // TieuDe2 (lblNumber)
        public string TieuDe2
        {
            get => tieuDe2.Text;
            set => tieuDe2.Text = value;
        }

        // TieuDe3 (lblNote)
        public string TieuDe3
        {
            get => tieuDe3.Text;
            set => tieuDe3.Text = value;
        }

        // ✅ Thêm thuộc tính Font cho TieuDe2 (tieuDe2)
        public Font Font2
        {
            get => tieuDe2.Font;
            set => tieuDe2.Font = value;
        }

        // ✅ Thêm thuộc tính Font cho TieuDe3 (tieuDe3)
        public Font Font3
        {
            get => tieuDe3.Font;
            set => tieuDe3.Font = value;
        }

        // ✅ Thêm thuộc tính ForeColor cho TieuDe2 (lblNumber)
        public Color ForeColor2
        {
            get => tieuDe2.ForeColor;
            set => tieuDe2.ForeColor = value;
        }

        // ✅ Thêm thuộc tính ForeColor cho TieuDe3 (lblNote)
        public Color ForeColor3
        {
            get => tieuDe3.ForeColor;
            set => tieuDe3.ForeColor = value;
        }

        public Color FillColor
        {
            get => pnlThongKeXepLoai.FillColor;
            set => pnlThongKeXepLoai.FillColor = value;
        }


        private void pnlThongKeXepLoai_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tieuDe1_Click(object sender, EventArgs e)
        {

        }

        private void tieuDe2_Click(object sender, EventArgs e)
        {

        }

        private void tieuDe3_Click(object sender, EventArgs e)
        {

        }
    }
}
