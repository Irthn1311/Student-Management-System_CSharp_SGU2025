using System;
using System.Windows.Forms;

namespace WinFormsApp2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Cấu hình WinForms tiêu chuẩn
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy Form Thời khóa biểu
            Application.Run(new FrmNamHocHocKi());
        }
    }
}