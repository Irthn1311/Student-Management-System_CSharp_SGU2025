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
    public partial class ChinhSuaPhuHuynh : Form
    {
        private PhuHuynhBLL phuHuynhBLL;
        private PhuHuynhDTO currentPhuHuynh;
        private int maPhuHuynhToEdit;
        public ChinhSuaPhuHuynh(int maPhuHuynh)
        {
            InitializeComponent();

            phuHuynhBLL = new PhuHuynhBLL();
            this.maPhuHuynhToEdit = maPhuHuynh;

            loadDataPhuHuynh();
        }

        private void loadDataPhuHuynh()
        {
            
            try
            {
                currentPhuHuynh = phuHuynhBLL.GetPhuHuynhById(maPhuHuynhToEdit);

                if (currentPhuHuynh != null)
                {   
                    txtHovaTen.Text = currentPhuHuynh.HoTen;
                    txtSoDienThoai.Text = currentPhuHuynh.SoDienThoai;
                    txtEmail.Text = currentPhuHuynh.Email;
                    txtDiaChi.Text = currentPhuHuynh.DiaChi;

                }
                else
                {
                    MessageBox.Show($"Không tìm thấy thông tin phụ huynh với mã {maPhuHuynhToEdit}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin phụ huynh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ChinhSuaPhuHuynh_Load(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSuaPhuHuynh_Click(object sender, EventArgs e)
        {
            // --- 1. Thu thập dữ liệu đã sửa từ Form ---
            PhuHuynhDTO updatedPh = new PhuHuynhDTO();
            try
            {
                updatedPh.MaPhuHuynh = this.maPhuHuynhToEdit; // Lấy Mã PH đang sửa
                updatedPh.HoTen = txtHovaTen.Text.Trim();
                updatedPh.SoDienThoai = txtSoDienThoai.Text.Trim();
                updatedPh.Email = txtEmail.Text.Trim();
                updatedPh.DiaChi = txtDiaChi.Text.Trim(); // Giả sử có control txtDiaChi

                // --- 2. Gọi BLL để kiểm tra và cập nhật ---
                // BLL (UpdatePhuHuynh) sẽ tự động gọi ValidatePhuHuynh
                // và kiểm tra trùng SĐT/Email (loại trừ chính mã PH này)
                if (phuHuynhBLL.UpdatePhuHuynh(updatedPh))
                {
                    MessageBox.Show("Cập nhật thông tin phụ huynh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Đặt kết quả thành công
                    this.Close(); // Đóng form
                }
                else
                {
                    // UpdatePhuHuynh trả về false (có thể do lỗi DB)
                    MessageBox.Show("Cập nhật thất bại. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException argEx) // Bắt lỗi validation từ BLL (trùng SĐT/Email, thiếu tên,...)
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin:\n\n" + argEx.Message,
                                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException nrEx) // Bắt lỗi nếu control (ví dụ txtHovaTen) bị null
            {
                MessageBox.Show("Lỗi: Control trên form chưa được khởi tạo đúng cách.\n" + nrEx.Message, "Lỗi Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) // Bắt các lỗi khác (ví dụ: lỗi kết nối database)
            {
                MessageBox.Show("Đã xảy ra lỗi không mong muốn khi cập nhật: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
