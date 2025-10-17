using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.Dashboard
{
    public partial class ucDashboard : UserControl
    {
        public ucDashboard()
        {
            InitializeComponent();
        }

        private void cardHoatDongNoiBatDashboard3_Load(object sender, EventArgs e)
        {

        }

        private void ucDashboard_Load(object sender, EventArgs e)
        {
            recentActivityItemThongBao1.lbTextName.Text = "Họp phụ huynh lớp 12";
            recentActivityItemThongBao1.lbNote.Text = "Ngày 15/10/2025 - 8:00 AM";
            recentActivityItemThongBao1.PictureBoxThongBao.Image = Properties.Resources.icons8_notification_blue;
            recentActivityItemThongBao1.PictureBoxThongBao.BackColor = Color.FromArgb(219,234,254);

            recentActivityItemThongBao2.lbTextName.Text = "Khen thưởng học sinh giỏi";
            recentActivityItemThongBao2.lbNote.Text = "Ngày 12/10/2025";
            recentActivityItemThongBao2.PictureBoxThongBao.Image = Properties.Resources.icons8_winners_medal_xanhla;
            recentActivityItemThongBao2.PictureBoxThongBao.BackColor = Color.FromArgb(220, 252, 231);

            recentActivityItemThongBao3.lbTextName.Text = "Báo cáo kết quả học tập";
            recentActivityItemThongBao3.lbNote.Text = "Ngày 10/10/2025";
            recentActivityItemThongBao3.PictureBoxThongBao.Image = Properties.Resources.icons8_increase_profits_cam;
            recentActivityItemThongBao3.PictureBoxThongBao.BackColor = Color.FromArgb(255, 237, 213);

            recentActivityItemThongBao4.lbTextName.Text = "Lịch thi giữa kỳ";
            recentActivityItemThongBao4.lbNote.Text = "Ngày 8/10/2025";
            recentActivityItemThongBao4.PictureBoxThongBao.Image = Properties.Resources.icons8_timetable_tim;
            recentActivityItemThongBao4.PictureBoxThongBao.BackColor = Color.FromArgb(243, 232, 255);

            cardHoatDongNoiBatDashboard1.lbCardName.Text = "Học sinh mới";
            cardHoatDongNoiBatDashboard1.lbCardValue.Text = "42";
            cardHoatDongNoiBatDashboard1.lbCardGhiChu.Text = "Tuần này";
            cardHoatDongNoiBatDashboard1.PictureBoxThongBao.Image = Properties.Resources.icons8_notification_blue;
            cardHoatDongNoiBatDashboard1.PictureBoxThongBao.BackColor = Color.FromArgb(219, 234, 254);
            cardHoatDongNoiBatDashboard1.lbCardValue.ForeColor = Color.FromArgb(30,136,229);

            cardHoatDongNoiBatDashboard2.lbCardName.Text = "Khen thưởng";
            cardHoatDongNoiBatDashboard2.lbCardValue.Text = "18";
            cardHoatDongNoiBatDashboard2.lbCardGhiChu.Text = "Tháng này";
            cardHoatDongNoiBatDashboard2.PictureBoxThongBao.Image = Properties.Resources.icons8_winners_medal_xanhla;
            cardHoatDongNoiBatDashboard2.PictureBoxThongBao.BackColor = Color.FromArgb(220, 252, 231);
            cardHoatDongNoiBatDashboard2.lbCardValue.ForeColor = Color.FromArgb(22,163,74);

            cardHoatDongNoiBatDashboard3.lbCardName.Text = "Sự kiện";
            cardHoatDongNoiBatDashboard3.lbCardValue.Text = "5";
            cardHoatDongNoiBatDashboard3.lbCardGhiChu.Text = "Sắp tới";
            cardHoatDongNoiBatDashboard3.PictureBoxThongBao.Image = Properties.Resources.icons8_increase_profits_cam;
            cardHoatDongNoiBatDashboard3.PictureBoxThongBao.BackColor = Color.FromArgb(255, 237, 213);
            cardHoatDongNoiBatDashboard3.lbCardValue.ForeColor = Color.FromArgb(234,88,12);

            cardHoatDongNoiBatDashboard4.lbCardName.Text = "Điểm TB tăng";
            cardHoatDongNoiBatDashboard4.lbCardValue.Text = "+0.4";
            cardHoatDongNoiBatDashboard4.lbCardGhiChu.Text = "So với kì trước";
            cardHoatDongNoiBatDashboard4.PictureBoxThongBao.Image = Properties.Resources.icons8_timetable_tim;
            cardHoatDongNoiBatDashboard4.PictureBoxThongBao.BackColor = Color.FromArgb(243, 232, 255);
            cardHoatDongNoiBatDashboard4.lbCardValue.ForeColor = Color.FromArgb(147,51,234);

        }
    }
}
