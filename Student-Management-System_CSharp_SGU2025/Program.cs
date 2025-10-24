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
            bool isConnected = ConnectionDatabase.TestConnection();

            if (isConnected)
            {
                MessageBox.Show("Kết nối database thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kết nối database thất bại! Kiểm tra lại chuỗi kết nối và trạng thái server MySQL.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.MainForm());
        }
    }
}
