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
    public partial class ucBaoCaoDanhSachLop : UserControl
    {
        public ucBaoCaoDanhSachLop()
        {
            InitializeComponent();
            SetupClassPanels();
            AttachEventHandlers();
        }

        private void SetupClassPanels()
        {
            // Configure class panels appearance
            SetupClassPanel(pnlClass1, "Lớp 10A1", "Sĩ số: 42 học sinh - GVCN: Nguyễn Thị Hoa");
            SetupClassPanel(pnlClass2, "Lớp 10A2", "Sĩ số: 42 học sinh - GVCN: Nguyễn Thị Hoa");
            SetupClassPanel(pnlClass3, "Lớp 10A3", "Sĩ số: 42 học sinh - GVCN: Nguyễn Thị Hoa");
            SetupClassPanel(pnlClass4, "Lớp 11A1", "Sĩ số: 42 học sinh - GVCN: Nguyễn Thị Hoa");
            SetupClassPanel(pnlClass5, "Lớp 11A2", "Sĩ số: 42 học sinh - GVCN: Nguyễn Thị Hoa");
            SetupClassPanel(pnlClass6, "Lớp 12A1", "Sĩ số: 42 học sinh - GVCN: Nguyễn Thị Hoa");
        }

        private void SetupClassPanel(Guna.UI2.WinForms.Guna2Panel panel, string className, string classInfo)
        {
            panel.BorderColor = Color.FromArgb(229, 231, 235);
            panel.BorderRadius = 8;
            panel.BorderThickness = 1;
            panel.Padding = new Padding(17);

            // Set initial selection for 10A2 as shown in the image
            if (className == "Lớp 10A2")
            {
                panel.BorderColor = Color.FromArgb(46, 143, 229);
            }
        }

        private void AttachEventHandlers()
        {
            // Class panel click handlers for highlighting
            pnlClass1.Click += (s, e) => HighlightClassPanel(pnlClass1);
            pnlClass2.Click += (s, e) => HighlightClassPanel(pnlClass2);
            pnlClass3.Click += (s, e) => HighlightClassPanel(pnlClass3);
            pnlClass4.Click += (s, e) => HighlightClassPanel(pnlClass4);
            pnlClass5.Click += (s, e) => HighlightClassPanel(pnlClass5);
            pnlClass6.Click += (s, e) => HighlightClassPanel(pnlClass6);

            // View detail button handlers
            btnViewDetail1.Click += (s, e) => { ViewClassDetail("10A1"); HighlightClassPanel(pnlClass1); };
            btnViewDetail2.Click += (s, e) => { ViewClassDetail("10A2"); HighlightClassPanel(pnlClass2); };
            btnViewDetail3.Click += (s, e) => { ViewClassDetail("10A3"); HighlightClassPanel(pnlClass3); };
            btnViewDetail4.Click += (s, e) => { ViewClassDetail("11A1"); HighlightClassPanel(pnlClass4); };
            btnViewDetail5.Click += (s, e) => { ViewClassDetail("11A2"); HighlightClassPanel(pnlClass5); };
            btnViewDetail6.Click += (s, e) => { ViewClassDetail("12A1"); HighlightClassPanel(pnlClass6); };

            // Set initial selection (10A2 as shown in the image)
            HighlightClassPanel(pnlClass2);
        }

        private Guna.UI2.WinForms.Guna2Panel selectedClassPanel = null;

        private void HighlightClassPanel(Guna.UI2.WinForms.Guna2Panel panel)
        {
            // Reset previous selection
            if (selectedClassPanel != null)
            {
                selectedClassPanel.BorderColor = Color.FromArgb(229, 231, 235);
            }

            // Set new selection
            selectedClassPanel = panel;
            panel.BorderColor = Color.FromArgb(46, 143, 229);
        }

        private void ViewClassDetail(string className)
        {
            MessageBox.Show($"Xem chi tiết lớp {className}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnViewDetail1_Click(object sender, EventArgs e)
        {
            ViewClassDetail("10A1");
            HighlightClassPanel(pnlClass1);
        }

        private void BtnViewDetail2_Click(object sender, EventArgs e)
        {
            ViewClassDetail("10A2");
            HighlightClassPanel(pnlClass2);
        }

        private void BtnViewDetail3_Click(object sender, EventArgs e)
        {
            ViewClassDetail("10A3");
            HighlightClassPanel(pnlClass3);
        }

        private void BtnViewDetail4_Click(object sender, EventArgs e)
        {
            ViewClassDetail("11A1");
            HighlightClassPanel(pnlClass4);
        }

        private void BtnViewDetail5_Click(object sender, EventArgs e)
        {
            ViewClassDetail("11A2");
            HighlightClassPanel(pnlClass5);
        }

        private void BtnViewDetail6_Click(object sender, EventArgs e)
        {
            ViewClassDetail("12A1");
            HighlightClassPanel(pnlClass6);
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất PDF đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
