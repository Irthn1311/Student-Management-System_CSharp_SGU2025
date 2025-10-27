using Guna.UI2.WinForms;
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
    public partial class ThongKeCard : UserControl
    {
        public ThongKeCard()
        {
            InitializeComponent();
        }

        // TieuDe1 (lblTieuDe)
        public string TieuDe1
        {
            get => lblTieuDe.Text;
            set => lblTieuDe.Text = value;
        }

        // TieuDe2 (lblNumber)
        public string TieuDe2
        {
            get => lblNumber.Text;
            set => lblNumber.Text = value;
        }

        // TieuDe3 (lblNote)
        public string TieuDe3
        {
            get => lblNote.Text;
            set => lblNote.Text = value;
        }

        // ✅ Thêm thuộc tính Font cho TieuDe2 (lblNumber)
        public Font Font2
        {
            get => lblNumber.Font;
            set => lblNumber.Font = value;
        }

        // ✅ Thêm thuộc tính Font cho TieuDe3 (lblNote)
        public Font Font3
        {
            get => lblNote.Font;
            set => lblNote.Font = value;
        }

        // ✅ Thêm thuộc tính ForeColor cho TieuDe2 (lblNumber)
        public Color ForeColor2
        {
            get => lblNumber.ForeColor;
            set => lblNumber.ForeColor = value;
        }

        // ✅ Thêm thuộc tính ForeColor cho TieuDe3 (lblNote)
        public Color ForeColor3
        {
            get => lblNote.ForeColor;
            set => lblNote.ForeColor = value;
        }

        public Color FillColor         {
            get => pnlThongKe.FillColor;
            set => pnlThongKe.FillColor = value;
        }

        private void pnlTitle_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnlThongKe_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblNumber_Click(object sender, EventArgs e)
        {
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
        }

        private void lblNote_Click(object sender, EventArgs e)
        {
        }
    }
}