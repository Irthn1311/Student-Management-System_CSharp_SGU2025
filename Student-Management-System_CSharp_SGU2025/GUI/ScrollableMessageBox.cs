using System;
using System.Drawing;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public class ScrollableMessageBox : Form
    {
        private TextBox textBoxMessage;
        private Button btnOK;

        public ScrollableMessageBox(string title, string message, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            InitializeComponent(title, message, icon);
        }

        private void InitializeComponent(string title, string message, MessageBoxIcon icon)
        {
            this.Text = title;
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Panel chứa icon
            Panel panelIcon = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(50, 50)
            };

            PictureBox pictureBoxIcon = new PictureBox
            {
                Location = new Point(0, 0),
                Size = new Size(48, 48),
                SizeMode = PictureBoxSizeMode.CenterImage
            };

            // Đặt icon tùy theo loại
            switch (icon)
            {
                case MessageBoxIcon.Error:
                    pictureBoxIcon.Image = SystemIcons.Error.ToBitmap();
                    break;
                case MessageBoxIcon.Warning:
                    pictureBoxIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case MessageBoxIcon.Information:
                    pictureBoxIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
                default:
                    pictureBoxIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
            }

            panelIcon.Controls.Add(pictureBoxIcon);
            this.Controls.Add(panelIcon);

            // TextBox với thanh cuộn
            textBoxMessage = new TextBox
            {
                Location = new Point(80, 20),
                Size = new Size(490, 380),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 9),  // Font monospace để căn đều
                Text = message.Replace("\n", Environment.NewLine),  // ✅ Chuyển \n thành xuống dòng thực sự
                BackColor = Color.White,
                WordWrap = true,  // Tự động xuống dòng
                AcceptsReturn = true  // ✅ Cho phép xuống dòng
            };
            this.Controls.Add(textBoxMessage);

            // Button OK
            btnOK = new Button
            {
                Text = "OK",
                Location = new Point(250, 420),
                Size = new Size(100, 30),
                DialogResult = DialogResult.OK
            };
            btnOK.Click += (s, e) => this.Close();
            this.Controls.Add(btnOK);

            this.AcceptButton = btnOK;
        }

        public static void Show(string title, string message, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            using (var form = new ScrollableMessageBox(title, message, icon))
            {
                form.ShowDialog();
            }
        }
    }
}
