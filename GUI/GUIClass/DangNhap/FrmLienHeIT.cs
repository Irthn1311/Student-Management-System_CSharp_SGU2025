using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.DangNhap
{
    public partial class FrmLienHeIT : Form
    {
        public FrmLienHeIT()
        {
            InitializeComponent();
            
            // Cấu hình form
            this.Text = "Liên hệ IT - THPT TTPT";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(500, 550);
            this.BackColor = Color.White;
        }

        private void FrmLienHeIT_Load(object sender, EventArgs e)
        {
            // Có thể thêm logic load ở đây nếu cần
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CopyToClipboard(string text, string label)
        {
            try
            {
                Clipboard.SetText(text);
                MessageBox.Show(
                    $"✅ Đã copy {label} vào clipboard!\n\n{text}",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể copy: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCopyEmail_Click(object sender, EventArgs e)
        {
            CopyToClipboard("it.support@thptttpt.edu.vn", "Email");
        }

        private void btnCopyZalo_Click(object sender, EventArgs e)
        {
            CopyToClipboard("@TTPT_IT", "Zalo/Telegram");
        }
    }
}
