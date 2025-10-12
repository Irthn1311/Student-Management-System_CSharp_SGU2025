using Student_Management_System_CSharp_SGU2025.GUI.statcardLHP;
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
    public partial class ThoiKhoaBieu : UserControl
    {
        public ThoiKhoaBieu()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ThoiKhoaBieu_Load);
        }

        private void ThoiKhoaBieu_Load(object sender, EventArgs e)
        {
           
            // Card 1
            statCardTKB1.MonHoc = "Toan";
            statCardTKB1.GiaoVien = "Ng T. Hoa";
            statCardTKB1.Phong = "A101";
            statCardTKB1.MauNen = Color.FromArgb(225, 245, 254);

            // Card 2
            statCardTKB2.MonHoc = "Vật lý";
            statCardTKB2.GiaoVien = "Hoàng T. Lan";
            statCardTKB2.Phong = "A102";
            statCardTKB2.MauNen = Color.FromArgb(255, 236, 179);

            // Card 3
            statCardTKB3.MonHoc = "Hóa học";
            statCardTKB3.GiaoVien = "Vũ V. Hưng";
            statCardTKB3.Phong = "A103";
            statCardTKB3.MauNen = Color.FromArgb(255, 204, 188);

            // Card 4
            statCardTKB4.MonHoc = "Tiếng Anh";
            statCardTKB4.GiaoVien = "Lê T. Mai";
            statCardTKB4.Phong = "B202";
            statCardTKB4.MauNen = Color.FromArgb(232, 234, 246);

            // Card 5
            statCardTKB5.MonHoc = "Sinh học";
            statCardTKB5.GiaoVien = "Đỗ T. Thu";
            statCardTKB5.Phong = "B203";
            statCardTKB5.MauNen = Color.FromArgb(200, 230, 201);

            // Card 6
            statCardTKB6.MonHoc = "Lịch sử";
            statCardTKB6.GiaoVien = "Phan V. Dũng";
            statCardTKB6.Phong = "C301";
            statCardTKB6.MauNen = Color.FromArgb(255, 224, 178);

            // 👉 Bạn tiếp tục gán cho các card còn lại (statCardTKB7 → statCardTKB30)
        }

    }

}

