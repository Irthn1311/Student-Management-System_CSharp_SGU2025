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
            // Fix: Application.Run requires a Form, not a UserControl.
            // Wrap HocSinh (UserControl) in a Form before running.
            Form mainForm = new Form();
            GUI.HanhKiem hanhKiem = new GUI.HanhKiem();
            hanhKiem.Dock = DockStyle.Fill;
            mainForm.Controls.Add(hanhKiem);
            Application.Run(mainForm);
        }
    }
}
