namespace WinFormsApp2
{
    partial class FrmNamHocHocKi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 900);
            this.Name = "FrmNamHocHocKi";
            this.Text = "FrmNamHocHocKi";
            this.Load += new System.EventHandler(this.FrmNamHocHocKi_Load);
        }

        #endregion

        // Kích thước giao diện ĐÃ CHỐT
        private const int FormWidth = 1440;
        private const int FormHeight = 900;
        private const int SidebarWidth = 192; // 192x900
        private const int HeaderHeight = 56;
        private const int ContentAreaWidth = 1440 - SidebarWidth; // 1248
        private const int ContentBodyHeight = 900 - HeaderHeight; // 844

        // Kích thước Card và Tab
        private const int TabButtonHeight = 40;
        private const int CardSpacing = 20;
        private const int CardWidth = (ContentAreaWidth - 4 * CardSpacing) / 3;
        private const int CardHeight = 120;
        private const int CardY = 80;
    }
}