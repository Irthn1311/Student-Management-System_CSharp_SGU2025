using Student_Management_System_CSharp_SGU2025.BUS;
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

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class frmPhanQuyen : Form
    {
        private PhanQuyenBUS phanQuyenBUS;
        private List<RoleItem> roleItems;
        public frmPhanQuyen()
        {
            InitializeComponent();
            phanQuyenBUS = new PhanQuyenBUS();
            roleItems = new List<RoleItem>();
        }

        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            LoadRoles();
        }

        private void LoadRoles()
        {
            try
            {
                // Xóa tất cả RoleItem cũ
                ClearAllRoleItems();

                // Lấy danh sách vai trò từ database
                List<VaiTroDTO> danhSachVaiTro = phanQuyenBUS.GetAllVaiTro();

                if (danhSachVaiTro.Count == 0)
                {
                    MessageBox.Show("Chưa có vai trò nào. Vui lòng thêm vai trò mới!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Tạo RoleItem cho mỗi vai trò
                int yPosition = 10; // Vị trí Y ban đầu
                int spacing = 10;   // Khoảng cách giữa các RoleItem

                foreach (var vaiTro in danhSachVaiTro)
                {
                    // Lấy danh sách tên chức năng của vai trò
                    List<string> tenChucNangs = phanQuyenBUS.GetTenChucNangByVaiTro(vaiTro.MaVaiTro);
                    string moTa = string.Join(", ", tenChucNangs);

                    // Tạo RoleItem
                    RoleItem roleItem = new RoleItem();
                    roleItem.RoleName = vaiTro.TenVaiTro;
                    roleItem.RoleDescription = moTa;
                    roleItem.Tag = vaiTro.MaVaiTro; // Lưu MaVaiTro vào Tag

                    // Thiết lập vị trí
                    roleItem.Location = new Point(10, yPosition);
                    roleItem.Width = pnlRoleContainer.Width - 30;

                    // Đăng ký sự kiện
                    roleItem.DeleteClicked += RoleItem_DeleteClicked;
                    roleItem.ViewClicked += RoleItem_ViewClicked;

                    // Thêm vào panel
                    pnlRoleContainer.Controls.Add(roleItem);
                    roleItems.Add(roleItem);

                    // Cập nhật vị trí Y cho item tiếp theo
                    yPosition += roleItem.Height + spacing;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load danh sách vai trò: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Xóa tất cả RoleItem
        /// </summary>
        private void ClearAllRoleItems()
        {
            foreach (var item in roleItems)
            {
                pnlRoleContainer.Controls.Remove(item);
                item.Dispose();
            }
            roleItems.Clear();
        }

        /// <summary>
        /// ✅ Xử lý sự kiện View - HIỂN THỊ CHI TIẾT VAI TRÒ
        /// </summary>
        private void RoleItem_ViewClicked(object sender, EventArgs e)
        {
            RoleItem roleItem = sender as RoleItem;
            if (roleItem == null) return;

            string maVaiTro = roleItem.Tag?.ToString() ?? "";
            string tenVaiTro = roleItem.RoleName;

            try
            {
                // Lấy chi tiết vai trò
                Dictionary<string, List<string>> chiTietVaiTro = phanQuyenBUS.GetChiTietVaiTro(maVaiTro);

                if (chiTietVaiTro.Count == 0)
                {
                    MessageBox.Show(
                        $"Vai trò '{tenVaiTro}' chưa có quyền nào!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // Tạo nội dung hiển thị
                StringBuilder message = new StringBuilder();
                message.AppendLine($"📋 CHI TIẾT VAI TRÒ: {tenVaiTro.ToUpper()}");
                message.AppendLine("".PadLeft(39, '='));
                message.AppendLine();

                int stt = 1;
                foreach (var item in chiTietVaiTro)
                {
                    string tenChucNang = item.Key;
                    List<string> hanhDongs = item.Value;

                    // Chuyển đổi hành động sang tiếng Việt
                    List<string> hanhDongsVN = hanhDongs
                        .Select(h => phanQuyenBUS.MapHanhDongToVietnamese(h))
                        .ToList();

                    message.AppendLine($"{stt}. 🔹 {tenChucNang}");
                    message.AppendLine($"   Quyền: {string.Join(", ", hanhDongsVN)}");
                    message.AppendLine();
                    stt++;
                }

                message.AppendLine("".PadLeft(39, '='));
                message.AppendLine($"Tổng số chức năng: {chiTietVaiTro.Count}");

                // Hiển thị MessageBox với scroll
                MessageBox.Show(
                    message.ToString(),
                    $"Chi tiết vai trò: {tenVaiTro}",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết vai trò: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Xử lý sự kiện Delete - LOGIC ĐÚNG
        /// </summary>
        private void RoleItem_DeleteClicked(object sender, EventArgs e)
        {
            RoleItem roleItem = sender as RoleItem;
            if (roleItem == null) return;

            string maVaiTro = roleItem.Tag?.ToString() ?? "";
            string tenVaiTro = roleItem.RoleName;

            try
            {
                // ✅ Bước 1: Kiểm tra vai trò có đang được sử dụng không
                bool dangDuocSuDung = !phanQuyenBUS.KiemTraCoTheXoaVaiTro(maVaiTro);

                if (dangDuocSuDung)
                {
                    // ❌ CHẶN XÓA - Hiển thị thông tin tài khoản đang dùng
                    List<string> danhSachNguoiDung = phanQuyenBUS.GetNguoiDungByVaiTro(maVaiTro);
                    int soLuongTaiKhoan = danhSachNguoiDung.Count;

                    StringBuilder message = new StringBuilder();
                    message.AppendLine($"❌ KHÔNG THỂ XÓA VAI TRÒ '{tenVaiTro.ToUpper()}'");
                    message.AppendLine();
                    message.AppendLine("".PadLeft(39, '='));
                    message.AppendLine();
                    message.AppendLine($"📌 Lý do: Vai trò này đang được gán cho {soLuongTaiKhoan} tài khoản");
                    message.AppendLine();

                    // ✅ CHỈ HIỂN THỊ DANH SÁCH NẾU SỐ LƯỢNG ≤ 10
                    if (soLuongTaiKhoan <= 10)
                    {
                        message.AppendLine("📋 Danh sách tài khoản:");
                        message.AppendLine();

                        int stt = 1;
                        foreach (string nguoiDung in danhSachNguoiDung)
                        {
                            message.AppendLine($"   {stt}. {nguoiDung}");
                            stt++;
                        }
                    }
                    else
                    {
                        // ⚠️ KHÔNG HIỂN THỊ DANH SÁCH - CHỈ THÔNG BÁO SỐ LƯỢNG
                        message.AppendLine($"⚠️ Số lượng tài khoản quá lớn ({soLuongTaiKhoan} tài khoản)");
                        message.AppendLine("   → Không hiển thị chi tiết để tối ưu hiệu năng");
                    }

                    message.AppendLine();
                    message.AppendLine("".PadLeft(39, '='));
                    message.AppendLine();
                    message.AppendLine("💡 Cách khắc phục:");
                    message.AppendLine("   1. Vào menu Quản lý Tài khoản");
                    message.AppendLine("   2. Thay đổi vai trò của các tài khoản trên");
                    message.AppendLine("   3. Sau đó mới có thể xóa vai trò này");

                    MessageBox.Show(
                        message.ToString(),
                        "Không thể xóa vai trò",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return; // ⛔ DỪNG LẠI - KHÔNG CHO XÓA
                }

                // ✅ Bước 2: Vai trò CHƯA được sử dụng → Cho phép xóa (xác nhận 1 lần)
                DialogResult confirm = MessageBox.Show(
                    $"⚠️ BẠN CÓ CHẮC CHẮN MUỐN XÓA VAI TRÒ '{tenVaiTro}'?\n\n" +
                    "📋 Thông tin sẽ bị xóa:\n" +
                    "   • Vai trò này\n" +
                    "   • Tất cả quyền liên quan đến vai trò\n" +
                    "   • Dữ liệu này KHÔNG THỂ KHÔI PHỤC!\n\n" +
                    "Bạn có muốn tiếp tục không?",
                    "Xác nhận xóa vai trò",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2); // ✅ Mặc định chọn "No" để an toàn

                if (confirm == DialogResult.Yes)
                {
                    // ✅ Thực hiện xóa
                    bool success = phanQuyenBUS.XoaVaiTro(maVaiTro);

                    if (success)
                    {
                        MessageBox.Show(
                            $"✅ Đã xóa vai trò '{tenVaiTro}' thành công!\n\n" +
                            $"Tất cả quyền liên quan đã được xóa khỏi hệ thống.",
                            "Xóa thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Reload lại danh sách
                        LoadRoles();
                    }
                    else
                    {
                        MessageBox.Show(
                            "❌ Xóa vai trò thất bại!\n\nVui lòng thử lại sau.",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi khi xóa vai trò:\n\n{ex.Message}",
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void roleItem1_Load(object sender, EventArgs e)
        {

        }

        private void roleItem2_Load(object sender, EventArgs e)
        {

        }

        private void roleItem3_Load(object sender, EventArgs e)
        {

        }

        private void roleItem4_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roleItem1_Load_1(object sender, EventArgs e)
        {

        }

        private void roleItem2_Load_1(object sender, EventArgs e)
        {

        }

        private void roleItem3_Load_1(object sender, EventArgs e)
        {

        }

        private void roleItem4_Load_1(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Tạo instance của form thêm quyền
            frmAddPhanQuyen addRoleForm = new frmAddPhanQuyen();
            // Căn giữa form con so với form cha
            addRoleForm.StartPosition = FormStartPosition.CenterParent;

            // Hiển thị form dưới dạng hộp thoại modal
            if (addRoleForm.ShowDialog(this) == DialogResult.OK)
            {
                // Reload lại danh sách vai trò sau khi thêm thành công
                LoadRoles();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void roleItem1_Load_2(object sender, EventArgs e)
        {

        }
    }
}
