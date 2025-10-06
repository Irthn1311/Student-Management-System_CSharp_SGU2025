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
    public partial class ucBaoCaoBangDiem : UserControl
    {
        public ucBaoCaoBangDiem()
        {
            InitializeComponent();
            SetupGradeTable();
        }

        private void SetupGradeTable()
        {
            // Configure DataGridView appearance
            dgvGrades.DefaultCellStyle.SelectionBackColor = Color.FromArgb(249, 250, 251);
            dgvGrades.DefaultCellStyle.SelectionForeColor = Color.FromArgb(55, 65, 81);
            dgvGrades.EnableHeadersVisualStyles = false;
            dgvGrades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvGrades.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(17, 24, 39);
            dgvGrades.ColumnHeadersDefaultCellStyle.Font = new Font("Inter", 9F, FontStyle.Bold);
            dgvGrades.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            dgvGrades.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Configure DataGridView columns
            dgvGrades.Columns.Clear();

            // STT Column
            var colSTT = new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Inter", 10F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    Padding = new Padding(12, 0, 12, 0)
                }
            };
            dgvGrades.Columns.Add(colSTT);

            // Student Name Column
            var colStudent = new DataGridViewTextBoxColumn
            {
                Name = "HocSinh",
                HeaderText = "Học sinh",
                Width = 260,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft,
                    Font = new Font("Inter", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    Padding = new Padding(12, 0, 12, 0)
                }
            };
            dgvGrades.Columns.Add(colStudent);

            // Subject Columns with center alignment
            var subjects = new[] { "Toán", "Văn", "Anh", "Lý", "Hóa", "TB" };
            foreach (var subject in subjects)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = subject,
                    HeaderText = subject,
                    Width = subject == "TB" ? 110 : 115,
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } },
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter,
                        Font = new Font("Inter", 10F, subject == "TB" ? FontStyle.Bold : FontStyle.Regular),
                        ForeColor = subject == "TB" ? Color.FromArgb(30, 136, 229) : Color.FromArgb(55, 65, 81)
                    }
                };
                dgvGrades.Columns.Add(col);
            }

            // Load sample data
            LoadGradeData();
        }

        private void LoadGradeData()
        {
            dgvGrades.Rows.Clear();

            // Sample data matching the image
            var students = new[]
            {
                new { STT = "1", Name = "Nguyễn Văn An", Toan = "8.5", Van = "8.2", Anh = "7.8", Ly = "7.5", Hoa = "8.0", TB = "8.0" },
                new { STT = "2", Name = "Trần Thị Bình", Toan = "9.0", Van = "8.5", Anh = "8.8", Ly = "8.2", Hoa = "8.5", TB = "8.6" },
                new { STT = "3", Name = "Lê Hoàng Cường", Toan = "7.0", Van = "7.5", Anh = "7.2", Ly = "6.8", Hoa = "7.0", TB = "7.1" }
            };

            foreach (var student in students)
            {
                dgvGrades.Rows.Add(student.STT, student.Name, student.Toan, student.Van, student.Anh, student.Ly, student.Hoa, student.TB);
            }
        }

        private void CboClassSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reload grade data when class is changed
            LoadGradeData();
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pnlGradeTable_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
