using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmMonHoc : UserControl
    {
        private MonHocBUS monHocBUS;
        public FrmMonHoc()
        {
            InitializeComponent();
            monHocBUS = new MonHocBUS();
        }

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            // ===================================
            // 1️⃣ CẤU HÌNH DATAGRIDVIEW
            // ===================================

            // --- Cài đặt cơ bản ---
            dgvMonHoc.AutoGenerateColumns = false;
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.RowTemplate.Height = 40; // Chiều cao mỗi dòng dữ liệu

            // --- Style cho tiêu đề cột ---
            dgvMonHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            dgvMonHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 136, 229); // <-- Thay đổi màu nền tiêu đề
            dgvMonHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMonHoc.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 136, 229); // <-- Ngăn đổi màu khi click
            dgvMonHoc.ColumnHeadersHeight = 40; // <-- Tăng chiều cao tiêu đề
            dgvMonHoc.EnableHeadersVisualStyles = false;

            // --- Style cho các dòng dữ liệu ---
            dgvMonHoc.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvMonHoc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgvMonHoc.DefaultCellStyle.SelectionForeColor = Color.Black;

            // ===================================
            // 2️⃣ NẠP DỮ LIỆU MẪU
            // ===================================


            // ===================================
            // 3️⃣ GẮN SỰ KIỆN
            // ===================================
            LoadDuLieuMonHoc();
            dgvMonHoc.CellPainting += dgvMonHoc_CellPainting;
            dgvMonHoc.CellClick += dgvMonHoc_CellClick;

            // ===================================
            // 4️⃣ THIẾT LẬP CÁC STAT CARD
            // ===================================
            // Card 1
            statcardMonHoc1.SetData("4", "Môn chính", "Toán,Văn,Anh");
            statcardMonHoc1.PanelBackgroundColor = Color.FromArgb(219, 234, 254);
            statcardMonHoc1.SoLuongForeColor = Color.FromArgb(30, 136, 229);

            // Card 2
            statcardMonHoc2.SetData("3", "Khoa học tự nhiên", "Lý,Hóa,Sinh");
            statcardMonHoc2.PanelBackgroundColor = Color.FromArgb(220, 252, 231);
            statcardMonHoc2.SoLuongForeColor = Color.FromArgb(22, 163, 74);

            // Card 3
            statcardMonHoc3.SetData("3", "Khoa học xã hội", "GDCD,Sử,Địa");
            statcardMonHoc3.PanelBackgroundColor = Color.FromArgb(255, 237, 213);
            statcardMonHoc3.SoLuongForeColor = Color.FromArgb(234, 88, 12);

            // Card 4
            statcardMonHoc4.SetData("3", "Kỹ năng khác", "GDCD,TD,QP");
            statcardMonHoc4.PanelBackgroundColor = Color.FromArgb(243, 243, 255);
            statcardMonHoc4.SoLuongForeColor = Color.FromArgb(147, 51, 234);
        }
        private void LoadDuLieuMonHoc()
        {
            try
            {
                dgvMonHoc.Rows.Clear(); // Xóa dữ liệu cũ trước khi nạp mới

                List<MonHocDTO> dsMonHoc = monHocBUS.DocDSMN();

                foreach (MonHocDTO mh in dsMonHoc)
                {
                    
                    
                    dgvMonHoc.Rows.Add(mh.maMon.ToString(), mh.tenMon, $"{mh.soTiet} tiết", mh.ghiChu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool XoaMonHocVaReload(int maMon)
        {
            try
            {
                // Xóa từ cơ sở dữ liệu
                if (monHocBUS.DeleteMonHoc(maMon))
                {
                    // Load lại toàn bộ dữ liệu lên bảng
                    LoadDuLieuMonHoc();
                    // Cập nhật stat cards
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void dgvMonHoc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Kiểm tra vẽ ô trong cột thao tác
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvMonHoc.Columns["TuyChinh"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Lấy icon từ Resources
                Image editIcon = Properties.Resources.edit_icon;   // ✏️
                Image deleteIcon = Properties.Resources.delete_icon; // 🗑️

                int iconSize = 20;
                int spacing = 10;
                int totalWidth = iconSize * 2 + spacing;

                // Căn giữa 2 icon trong ô
                int startX = e.CellBounds.Left + (e.CellBounds.Width - totalWidth) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                // Vẽ 2 icon
                e.Graphics.DrawImage(editIcon, new Rectangle(startX, y, iconSize, iconSize));
                e.Graphics.DrawImage(deleteIcon, new Rectangle(startX + iconSize + spacing, y, iconSize, iconSize));

                e.Handled = true;
            }
        }

        // Cập nhật phương thức dgvMonHoc_CellClick trong FrmMonHoc.cs

        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua click tiêu đề
            if (e.RowIndex < 0 || e.ColumnIndex != dgvMonHoc.Columns["TuyChinh"].Index)
                return;

            // Xác định vị trí click
            Point clickPoint = dgvMonHoc.PointToClient(Cursor.Position);
            Rectangle cellRect = dgvMonHoc.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

            int iconSize = 20;
            int spacing = 10;
            int totalWidth = iconSize * 2 + spacing;
            int startX = cellRect.Left + (cellRect.Width - totalWidth) / 2;

            int xEdit = startX;
            int xDelete = startX + iconSize + spacing;

            // Lấy thông tin môn học
            int maMon = Convert.ToInt32(dgvMonHoc.Rows[e.RowIndex].Cells["MaMon"].Value);
            string tenMon = dgvMonHoc.Rows[e.RowIndex].Cells["TenMon"].Value.ToString();

            // Xử lý click từng icon
            if (clickPoint.X >= xEdit && clickPoint.X <= xEdit + iconSize)
            {
                // XỬ LÝ SỬA - làm sau
                MessageBox.Show($"📝 Chỉnh sửa môn: {tenMon} (Mã: {maMon})", "Sửa môn học",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                // TODO: Mở form chỉnh sửa
            }
            else if (clickPoint.X >= xDelete && clickPoint.X <= xDelete + iconSize)
            {
                // XỬ LÝ XÓA
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa môn học:\n\n" +
                    $"Mã môn: {maMon}\n" +
                    $"Tên môn: {tenMon}\n\n" +
                    $"⚠️ Hành động này không thể hoàn tác!",
                    "Xác nhận xóa môn học",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr == DialogResult.Yes)
                {
                    if (XoaMonHocVaReload(maMon))
                    {
                        MessageBox.Show(
                            $"✓ Đã xóa môn học '{tenMon}' thành công!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            $"✗ Không thể xóa môn học '{tenMon}'!\n\nVui lòng thử lại.",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        
        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            // Tùy chọn xử lý nếu cần
        }

        private void statcardMonHoc2_Load(object sender, EventArgs e)
        {

        }

        private void statcardMonHoc1_Load(object sender, EventArgs e)
        {

        }

        private void panelMonHoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvMonHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
