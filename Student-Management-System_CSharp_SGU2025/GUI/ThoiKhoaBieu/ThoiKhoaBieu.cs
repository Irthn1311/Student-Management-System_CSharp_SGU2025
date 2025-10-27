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

namespace Student_Management_System_CSharp_SGU2025.GUI.ThoiKhoaBieu
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
           
            
        }

        private void guna2HtmlLabel25_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ThoiKhoaBieu_Load_1(object sender, EventArgs e)
        {
            var lichHoc = new[] {
                // Tiết 1
                new { Mon = "Toán học", GV = "Nguyễn T. Hoa", Phong = "A101", Thu = 2, Tiet = 1 },
                new { Mon = "Vật lý", GV = "Hoàng T. Lan", Phong = "A102", Thu = 3, Tiet = 1 },
                new { Mon = "Tiếng Anh", GV = "Lê T. Mai", Phong = "C301", Thu = 4, Tiet = 1 },
                new { Mon = "Sinh học", GV = "Đỗ T. Thu", Phong = "A104", Thu = 5, Tiet = 1 },
                new { Mon = "Toán học", GV = "Nguyễn T. Hoa", Phong = "A101", Thu = 6, Tiet = 1 },
                new { Mon = "Thể dục", GV = "Phạm V. Đức", Phong = "Sân TD", Thu = 7, Tiet = 1 },

                // Tiết 2
                new { Mon = "Toán học", GV = "Nguyễn T. Hoa", Phong = "A101", Thu = 2, Tiet = 2 },
                new { Mon = "Vật lý", GV = "Hoàng T. Lan", Phong = "A102", Thu = 3, Tiet = 2 },
                new { Mon = "Ngữ văn", GV = "Trần V. Nam", Phong = "B201", Thu = 5, Tiet = 2 },
                new { Mon = "Thể dục", GV = "Phạm V. Đức", Phong = "Sân TD", Thu = 7, Tiet = 2 },

                // Tiết 3
                new { Mon = "Hóa học", GV = "Vũ V. Hùng", Phong = "A103", Thu = 3, Tiet = 3 },
                new { Mon = "Toán học", GV = "Nguyễn T. Hoa", Phong = "A101", Thu = 4, Tiet = 3 },
                new { Mon = "Vật lý", GV = "Hoàng T. Lan", Phong = "A102", Thu = 5, Tiet = 3 },
                new { Mon = "Ngữ văn", GV = "Trần V. Nam", Phong = "B201", Thu = 6, Tiet = 3 },
    
                // Tiết 4
                new { Mon = "Tiếng Anh", GV = "Lê T. Mai", Phong = "C301", Thu = 2, Tiet = 4 },
                new { Mon = "Sinh học", GV = "Đỗ T. Thu", Phong = "A104", Thu = 4, Tiet = 4 },
                new { Mon = "Lịch sử", GV = "Ngô T. Hường", Phong = "B203", Thu = 5, Tiet = 4 },
                new { Mon = "Quốc phòng", GV = "Hoàng V. Kiên", Phong = "E501", Thu = 7, Tiet = 4 },

                // Tiết 5
                new { Mon = "Thể dục", GV = "Phạm V. Đức", Phong = "Sân TD", Thu = 2, Tiet = 5 },
                new { Mon = "GDCD", GV = "Bùi V. Toàn", Phong = "B202", Thu = 3, Tiet = 5 },
                new { Mon = "Địa lý", GV = "Trần V. Long", Phong = "B204", Thu = 4, Tiet = 5 },
                new { Mon = "Tin học", GV = "Lê V. An", Phong = "D401", Thu = 5, Tiet = 5 },

                // Tiết 6 (Buổi chiều)
                new { Mon = "Ngữ văn", GV = "Trần V. Nam", Phong = "B201", Thu = 3, Tiet = 6 },
                new { Mon = "Hóa học", GV = "Vũ V. Hùng", Phong = "A103", Thu = 5, Tiet = 6 },

                // Tiết 7
                new { Mon = "Tin học", GV = "Lê V. An", Phong = "D401", Thu = 2, Tiet = 7 },
                new { Mon = "Lịch sử", GV = "Ngô T. Hường", Phong = "B203", Thu = 4, Tiet = 7 },
    
                // Tiết 8
                new { Mon = "Địa lý", GV = "Trần V. Long", Phong = "B204", Thu = 3, Tiet = 8 },
                new { Mon = "Sinh học", GV = "Đỗ T. Thu", Phong = "A104", Thu = 6, Tiet = 8 },

                // Tiết 9
                new { Mon = "Quốc phòng", GV = "Hoàng V. Kiên", Phong = "E501", Thu = 4, Tiet = 9 },
    
                // Tiết 10
                new { Mon = "GDCD", GV = "Bùi V. Toàn", Phong = "B202", Thu = 2, Tiet = 10 }
            };

            // Lặp qua từng môn học để tạo thẻ
            foreach (var mon in lichHoc)
            {
                var card = new StatCardTKB();
                var colorSet = GetColorSetForSubject(mon.Mon);

                card.SetData(
                    mon.Mon,
                    mon.GV,
                    mon.Phong,
                    colorSet.TextColor,
                    colorSet.ProgressColor1,
                    colorSet.ProgressColor2
                );

                card.Dock = DockStyle.Fill;
                card.Margin = new Padding(5);

                // 👇 SỬA LẠI TỌA ĐỘ Ở ĐÂY 👇
                // Cột: mon.Thu - 1 (vì cột "Thứ 2" là cột 1, "Thứ 3" là 2,...)
                // Hàng: mon.Tiet (vì hàng "Tiết 1" là hàng 1, "Tiết 2" là 2,...)
                tableThoiKhoaBieu.Controls.Add(card, mon.Thu - 1, mon.Tiet);
            }
        }

        private (Color TextColor, Color ProgressColor1, Color ProgressColor2) GetColorSetForSubject(string subject)
        {
            switch (subject)
            {
                case "Toán học":
                    // Nền xanh dương rất nhạt
                    return (Color.FromArgb(30, 64, 175), Color.FromArgb(96, 165, 250), Color.FromArgb(239, 246, 255));

                case "Vật lý":
                    // Nền cam rất nhạt
                    return (Color.FromArgb(154, 52, 18), Color.FromArgb(251, 146, 60), Color.FromArgb(255, 247, 237));

                case "Tiếng Anh":
                    // Nền tím rất nhạt
                    return (Color.FromArgb(107, 33, 168), Color.FromArgb(192, 132, 252), Color.FromArgb(245, 243, 255));

                case "Sinh học":
                    // Nền xanh ngọc rất nhạt
                    return (Color.FromArgb(17, 94, 89), Color.FromArgb(45, 212, 191), Color.FromArgb(240, 253, 250));

                case "Hóa học":
                    // Nền hồng rất nhạt
                    return (Color.FromArgb(157, 23, 77), Color.FromArgb(244, 114, 182), Color.FromArgb(253, 242, 248));

                case "Ngữ văn":
                    // Nền xanh lá rất nhạt
                    return (Color.FromArgb(22, 101, 52), Color.FromArgb(74, 222, 128), Color.FromArgb(240, 253, 244));

                case "Lịch sử":
                    // Nền vàng rất nhạt
                    return (Color.FromArgb(133, 77, 14), Color.FromArgb(250, 204, 21), Color.FromArgb(254, 252, 232));

                case "Địa lý":
                    // Nền chàm rất nhạt
                    return (Color.FromArgb(55, 48, 163), Color.FromArgb(129, 140, 248), Color.FromArgb(238, 242, 255));

                case "GDCD":
                    // Nền đỏ rất nhạt
                    return (Color.FromArgb(153, 27, 27), Color.FromArgb(248, 113, 113), Color.FromArgb(254, 242, 242));

                case "Tự học":
                    // Nền xám rất nhạt
                    return (Color.Black, Color.FromArgb(209, 213, 219), Color.FromArgb(249, 250, 251));

                case "Thể dục":
                    // Nền xanh lá cây rất nhạt
                    return (Color.FromArgb(21, 128, 61), Color.FromArgb(74, 222, 128), Color.FromArgb(220, 252, 231));

                case "Quốc phòng":
                    // Nền xám xanh rất nhạt
                    return (Color.FromArgb(71, 85, 105), Color.FromArgb(148, 163, 184), Color.FromArgb(241, 245, 249));

                case "Tin học":
                    // Nền xám đậm rất nhạt
                    return (Color.FromArgb(15, 23, 42), Color.FromArgb(100, 116, 139), Color.FromArgb(241, 245, 249));

                // Màu mặc định cho các môn không có trong danh sách
                default:
                    return (Color.Black, Color.Gainsboro, Color.WhiteSmoke);
            }
        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

