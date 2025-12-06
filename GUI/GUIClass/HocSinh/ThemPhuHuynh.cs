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
    public partial class ThemPhuHuynh : Form
    {
        private PhuHuynhBLL phuHuynhBLL;
        
        // ✅ Property để trả về phụ huynh vừa tạo
        public PhuHuynhDTO NewPhuHuynh { get; private set; }
        
        public ThemPhuHuynh()
        {
            InitializeComponent();
            phuHuynhBLL = new PhuHuynhBLL();
        }

        private void ThemPhuHuynh_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemPhuHuynh_Click(object sender, EventArgs e)
        {
            // --- 1. Thu thập dữ liệu từ Form ---
            PhuHuynhDTO ph = new PhuHuynhDTO();
            try
            {
                ph.HoTen = txtHovaTen.Text.Trim();
                ph.SoDienThoai = txtSoDienThoai.Text.Trim();
                ph.Email = txtEmail.Text.Trim();
                ph.DiaChi = txtDiaChi.Text.Trim();

                // --- 2. Gọi BLL và kiểm tra kết quả ---
                if (phuHuynhBLL.AddPhuHuynh(ph)) // Kiểm tra kết quả trả về
                {
                    // ✅ Lưu lại phụ huynh vừa tạo
                    // Lưu ý: Vì AddPhuHuynh chỉ return bool, ta cần load lại từ DB
                    // Cách tốt nhất: Sửa AddPhuHuynh return int (MaPhuHuynh)
                    // Tạm thời: Lấy phụ huynh có SĐT/Email vừa thêm
                    var allPH = phuHuynhBLL.GetAllPhuHuynh();
                    this.NewPhuHuynh = allPH.FirstOrDefault(p => 
                        p.SoDienThoai == ph.SoDienThoai && p.Email == ph.Email);
                    
                    MessageBox.Show("Thêm phụ huynh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); // Thêm thông báo
                    this.DialogResult = DialogResult.OK; // Đặt kết quả thành công
                    this.Close(); // Đóng form
                }
                else
                {
                    // Trường hợp BLL trả về false mà không ném lỗi
                    MessageBox.Show("Thêm phụ huynh thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException argEx) // Bắt lỗi validation từ BLL
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin:\n\n" + argEx.Message,
                                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // ... (các khối catch khác giữ nguyên) ...
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không mong muốn: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
