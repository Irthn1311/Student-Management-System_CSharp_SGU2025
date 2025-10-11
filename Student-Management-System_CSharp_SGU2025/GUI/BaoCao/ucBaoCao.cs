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
    public partial class ucBaoCao : UserControl
    {
        private ucBaoCaoDanhSachLop ucDanhSachLop;
        private ucBaoCaoBangDiem ucBangDiem;
        private ucBaoCaoThongKeHocLuc ucThongKeHocLuc;

        public ucBaoCao()
        {
            InitializeComponent();
            SetupFont();
            SetupCardHoverEffects();
            InitializeUserControls();
            AttachEventHandlers();
        }

        private void SetupFont()
        {
            // Set font Inter for all controls if available
            try
            {
                var interFont = new Font("Inter", 10F, FontStyle.Regular);
                // Font is available
            }
            catch
            {
                // Inter font not installed, will use default
            }
        }

        private void SetupCardHoverEffects()
        {
            // Add hover effects to cards
            SetupCardHover(cardBaoCaoHocSinh);
            SetupCardHover(cardThongKeDiem);
            SetupCardHover(cardBaoCaoTongHop);
        }

        private void SetupCardHover(Guna.UI2.WinForms.Guna2Panel card)
        {
            card.MouseEnter += (s, e) =>
            {
                card.ShadowDecoration.Depth = 10;
                card.Cursor = Cursors.Hand;
            };
            
            card.MouseLeave += (s, e) =>
            {
                card.ShadowDecoration.Depth = 5;
                card.Cursor = Cursors.Default;
            };
        }

        private void InitializeUserControls()
        {
            // Initialize UserControls
            ucDanhSachLop = new ucBaoCaoDanhSachLop();
            ucBangDiem = new ucBaoCaoBangDiem();
            ucThongKeHocLuc = new ucBaoCaoThongKeHocLuc();

            // Add to content panel
            pnlContent.Controls.Add(ucDanhSachLop);
            pnlContent.Controls.Add(ucBangDiem);
            pnlContent.Controls.Add(ucThongKeHocLuc);

            // Set initial visibility
            ucDanhSachLop.Visible = true;
            ucBangDiem.Visible = false;
            ucThongKeHocLuc.Visible = false;
        }

        private void AttachEventHandlers()
        {
            // Tab buttons
            btnDanhSachLop.Click += BtnTab_Click;
            btnBangDiem.Click += BtnTab_Click;
            btnThongKeHocLuc.Click += BtnTab_Click;

            // Card click handlers
            cardBaoCaoHocSinh.Click += (s, e) => BtnTab_Click(btnDanhSachLop, e);
            cardThongKeDiem.Click += (s, e) => BtnTab_Click(btnBangDiem, e);
            cardBaoCaoTongHop.Click += (s, e) => BtnTab_Click(btnThongKeHocLuc, e);

            // ComboBox
            cboHocKy.SelectedIndexChanged += CboHocKy_SelectedIndexChanged;
        }

        private void BtnTab_Click(object sender, EventArgs e)
        {
            // Hide all UserControls
            ucDanhSachLop.Visible = false;
            ucBangDiem.Visible = false;
            ucThongKeHocLuc.Visible = false;

            // Show corresponding UserControl based on sender
            if (sender == btnDanhSachLop || sender == cardBaoCaoHocSinh)
            {
                ucDanhSachLop.Visible = true;
                ucDanhSachLop.BringToFront();
                btnDanhSachLop.Checked = true;
                btnBangDiem.Checked = false;
                btnThongKeHocLuc.Checked = false;
            }
            else if (sender == btnBangDiem || sender == cardThongKeDiem)
            {
                ucBangDiem.Visible = true;
                ucBangDiem.BringToFront();
                btnDanhSachLop.Checked = false;
                btnBangDiem.Checked = true;
                btnThongKeHocLuc.Checked = false;
            }
            else if (sender == btnThongKeHocLuc || sender == cardBaoCaoTongHop)
            {
                ucThongKeHocLuc.Visible = true;
                ucThongKeHocLuc.BringToFront();
                btnDanhSachLop.Checked = false;
                btnBangDiem.Checked = false;
                btnThongKeHocLuc.Checked = true;
            }
        }

        private void CboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle semester change
            string selectedSemester = cboHocKy.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedSemester))
            {
                // Reload data based on selected semester
                // MessageBox.Show($"Đã chọn {selectedSemester}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
