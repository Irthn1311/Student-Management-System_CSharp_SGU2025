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
    public partial class ucXepLoai : UserControl
    {
        public ucXepLoai()
        {
            InitializeComponent();
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            // Thêm dữ liệu mẫu vào DataGridView
            guna2DataGridView1.Rows.Add("Nguyễn Văn An", "10A1", "8.5", "Tốt", "Giỏi");
            guna2DataGridView1.Rows.Add("Trần Thị Bình", "10A1", "9.2", "Tốt", "Giỏi");
            guna2DataGridView1.Rows.Add("Lê Hoàng Cường", "10A2", "7.8", "Khá", "Khá");
            guna2DataGridView1.Rows.Add("Phạm Thị Dung", "10A2", "8.8", "Tốt", "Giỏi");
            guna2DataGridView1.Rows.Add("Hoàng Văn Em", "11A1", "6.5", "Khá", "Trung bình");
            guna2DataGridView1.Rows.Add("Vũ Thị Hoa", "11A1", "7.5", "Khá", "Khá");
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Column4" || guna2DataGridView1.Columns[e.ColumnIndex].Name == "Column5")
            {
                if (e.Value != null)
                {
                    string cellValue = e.Value.ToString();
                    switch (cellValue)
                    {
                        case "Tốt":
                        case "Giỏi":
                            e.CellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
                            e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
                            break;
                        case "Khá":
                            e.CellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
                            e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
                            break;
                        case "Trung bình":
                            e.CellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(213)))));
                            e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
                            break;
                        case "Yếu":
                            e.CellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
                            e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
                            break;
                    }
                }
            }
        }
    }
}
