using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ========================================
            // ✅ KIỂM TRA KẾT NỐI DATABASE
            // ========================================

            // Cách 1: Kiểm tra đơn giản
            if (!ConnectionDatabase.TestConnection())
            {
                MessageBox.Show(
                    "❌ KHÔNG THỂ KẾT NỐI DATABASE!\n\n" +
                    "Ứng dụng sẽ không chạy. Vui lòng:\n" +
                    "1. Bật MySQL Server\n" +
                    "2. Tạo database 'QuanLyHocSinh'\n" +
                    "3. Kiểm tra username/password",
                    "Lỗi nghiêm trọng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop
                );
                return; // Dừng ứng dụng
            }

            // Cách 2: Kiểm tra với thông báo chi tiết (Uncomment để dùng)
            // ConnectionDatabase.TestConnectionWithMessage();

            // Cách 3: Kiểm tra cấu trúc database (Uncomment để dùng)
            // ConnectionDatabase.CheckDatabaseStructure();

            // ========================================
            // ✅ CHẠY ỨNG DỤNG
            // ========================================
            Application.Run(new GUI.MainForm());
        }
    }
}