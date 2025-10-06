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
    public partial class ucBaoCaoThongKeHocLuc : UserControl
    {
        public ucBaoCaoThongKeHocLuc()
        {
            InitializeComponent();
            SetupStatisticsPanel();
        }

        private void SetupStatisticsPanel()
        {
            // Set up statistics panel based on CSS from baocao_thongkehocluc.txt
            pnlStatistics.Visible = true;

            // Configure main statistics panel
            pnlStatistics.BorderColor = Color.FromArgb(243, 244, 246);
            pnlStatistics.BorderRadius = 12;
            pnlStatistics.BorderThickness = 1;
            pnlStatistics.ShadowDecoration.BorderRadius = 12;
            pnlStatistics.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 13);
            pnlStatistics.ShadowDecoration.Depth = 5;
            pnlStatistics.ShadowDecoration.Enabled = true;

            // Configure Khoi 10
            SetupKhoiPanel(pnlKhoi10, lblKhoi10Title, "Khối 10", flpnlKhoi10Stats);
            SetupStatPanel(pnlGoi10, lblGoi10Count, lblGoi10Label, "98", "Giỏi", Color.FromArgb(22, 163, 74), Color.White);
            SetupStatPanel(pnlKha10, lblKha10Count, lblKha10Label, "192", "Khá", Color.FromArgb(37, 99, 235), Color.White);
            SetupStatPanel(pnlTrungBinh10, lblTrungBinh10Count, lblTrungBinh10Label, "154", "Trung bình", Color.FromArgb(234, 88, 12), Color.White);
            SetupStatPanel(pnlYeu10, lblYeu10Count, lblYeu10Label, "36", "Yếu", Color.FromArgb(220, 38, 38), Color.White);

            // Configure Khoi 11
            SetupKhoiPanel(pnlKhoi11, lblKhoi11Title, "Khối 11", flpnlKhoi11Stats);
            SetupStatPanel(pnlGoi11, lblGoi11Count, lblGoi11Label, "102", "Giỏi", Color.FromArgb(22, 163, 74), Color.White);
            SetupStatPanel(pnlKha11, lblKha11Count, lblKha11Label, "178", "Khá", Color.FromArgb(37, 99, 235), Color.White);
            SetupStatPanel(pnlTrungBinh11, lblTrungBinh11Count, lblTrungBinh11Label, "142", "Trung bình", Color.FromArgb(234, 88, 12), Color.White);
            SetupStatPanel(pnlYeu11, lblYeu11Count, lblYeu11Label, "34", "Yếu", Color.FromArgb(220, 38, 38), Color.White);

            // Configure Khoi 12
            SetupKhoiPanel(pnlKhoi12, lblKhoi12Title, "Khối 12", flpnlKhoi12Stats);
            SetupStatPanel(pnlGoi12, lblGoi12Count, lblGoi12Label, "85", "Giỏi", Color.FromArgb(22, 163, 74), Color.White);
            SetupStatPanel(pnlKha12, lblKha12Count, lblKha12Label, "168", "Khá", Color.FromArgb(37, 99, 235), Color.White);
            SetupStatPanel(pnlTrungBinh12, lblTrungBinh12Count, lblTrungBinh12Label, "147", "Trung bình", Color.FromArgb(234, 88, 12), Color.White);
            SetupStatPanel(pnlYeu12, lblYeu12Count, lblYeu12Label, "20", "Yếu", Color.FromArgb(220, 38, 38), Color.White);

            // Configure export button
            btnExportStatistics.FillColor = Color.FromArgb(30, 136, 229);
            btnExportStatistics.ForeColor = Color.White;
            btnExportStatistics.BorderRadius = 8;
            btnExportStatistics.Text = "📊 Xuất báo cáo tổng hợp";
        }

        private void SetupKhoiPanel(Guna.UI2.WinForms.Guna2Panel khoiPanel, Label titleLabel, string title, FlowLayoutPanel statsContainer)
        {
            khoiPanel.BorderColor = Color.FromArgb(243, 244, 246);
            khoiPanel.BorderRadius = 4;
            khoiPanel.BorderThickness = 1;
            khoiPanel.Padding = new Padding(16);
            khoiPanel.Size = new Size(1071, 120);

            titleLabel.Text = title;
            titleLabel.Font = new Font("Inter", 11F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(17, 24, 39);
            titleLabel.Location = new Point(0, 0);

            statsContainer.FlowDirection = FlowDirection.LeftToRight;
            statsContainer.WrapContents = false;
            statsContainer.AutoSize = true;
            statsContainer.Dock = DockStyle.Fill;
        }

        private void SetupStatPanel(Guna.UI2.WinForms.Guna2Panel statPanel, Label countLabel, Label textLabel, string count, string text, Color textColor, Color bgColor)
        {
            statPanel.BackColor = bgColor;
            statPanel.BorderRadius = 4;
            statPanel.BorderColor = textColor;
            statPanel.BorderThickness = 2;
            statPanel.Size = new Size(255, 84);
            statPanel.Padding = new Padding(16, 16, 20, 16);

            countLabel.Text = count;
            countLabel.Font = new Font("Inter", 21F, FontStyle.Bold);
            countLabel.ForeColor = textColor;
            countLabel.Location = new Point(0, 0);
            countLabel.AutoSize = true;

            textLabel.Text = text;
            textLabel.Font = new Font("Inter", 9F, FontStyle.Regular);
            textLabel.ForeColor = Color.FromArgb(75, 85, 99);
            textLabel.Location = new Point(0, 32);
            textLabel.AutoSize = true;
        }

        private void BtnExportStatistics_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất báo cáo tổng hợp đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
