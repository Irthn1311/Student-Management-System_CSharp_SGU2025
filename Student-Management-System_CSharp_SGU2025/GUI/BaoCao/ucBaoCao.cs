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
        
        private ucBaoCaoBangDiem ucBangDiem;
        private ucBaoCaoThongKeHocLuc ucThongKeHocLuc;

        public ucBaoCao()
        {
            InitializeComponent();
           
            SetupCardHoverEffects();
            InitializeUserControls();
            AttachEventHandlers();
        }

        
        

        private void SetupCardHoverEffects()
        {
            // Add hover effects to cards
            SetupCardHover(cardThongKeDiem);
            SetupCardHover(cardBaoCaoTongHop);
        }

        private void ShowDefaultPanels(bool visible)
        {
            // Ẩn/hiện 2 panel gốc trong pnlContent
            pnlClassListHeader.Visible = visible;
            pnlClassesContainer.Visible = visible;
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
            // Khởi tạo 2 UserControl chính
            ucBangDiem = new ucBaoCaoBangDiem();
            ucThongKeHocLuc = new ucBaoCaoThongKeHocLuc();

            // Thêm vào panel (ẩn hết lúc đầu)
            pnlContent.Controls.Add(ucBangDiem);
            pnlContent.Controls.Add(ucThongKeHocLuc);

            ucBangDiem.Dock = DockStyle.Fill;
            ucThongKeHocLuc.Dock = DockStyle.Fill;

            ucBangDiem.Visible = false;
            ucThongKeHocLuc.Visible = false;
        }

        private void AttachEventHandlers()
        {
            // 3 nút điều hướng
            btnDanhSachLop.Click += BtnTab_Click;
            btnBangDiem.Click += BtnTab_Click;
            btnThongKeHocLuc.Click += BtnTab_Click;

            // 3 card điều hướng tương ứng
            cardBaoCaoHocSinh.Click += (s, e) => BtnTab_Click(btnDanhSachLop, e);
            cardThongKeDiem.Click += (s, e) => BtnTab_Click(btnBangDiem, e);
            cardBaoCaoTongHop.Click += (s, e) => BtnTab_Click(btnThongKeHocLuc, e);

            // Combobox chọn học kỳ
            cboHocKy.SelectedIndexChanged += CboHocKy_SelectedIndexChanged;
        }

        private void BtnTab_Click(object sender, EventArgs e)
        {
            // Ẩn tất cả các usercontrol trước
            ucBangDiem.Visible = false;
            ucThongKeHocLuc.Visible = false;

            // Ẩn tạm 2 panel gốc
            ShowDefaultPanels(false);

            // Kiểm tra nút được nhấn
            if (sender == btnDanhSachLop || sender == cardBaoCaoHocSinh)
            {
                // Hiện lại panel danh sách lớp
                ShowDefaultPanels(true);

                // Đặt trạng thái Checked cho nút
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
            string selectedSemester = cboHocKy.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedSemester))
            {
                // Tùy chọn: nạp lại dữ liệu tương ứng học kỳ
            }
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboHocKy_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cardThongKeDiem_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void cardBaoCaoHocSinh_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
