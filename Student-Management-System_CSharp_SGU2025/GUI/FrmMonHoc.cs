using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class FrmMonHoc : Form
    {
        private BindingList<MonHoc> dsMonHoc = new BindingList<MonHoc>();
        public FrmMonHoc()
        {
            InitializeComponent();

        }

        private void MonHoc_Load(object sender, EventArgs e)
        {
            // Gán tên property cho cột đã kéo thả
            Column1.DataPropertyName = "MaMon";
            Column2.DataPropertyName = "TenMon";
            Column3.DataPropertyName = "SoTiet";

            dgvMonHoc.AutoGenerateColumns = false;
            dgvMonHoc.AllowUserToAddRows = false;

            /*
            DataGridViewButtonColumn colFree=new DataGridViewButtonColumn();
            
            colFree.HeaderText = "Tùy chỉnh";
            colFree.UseColumnTextForButtonValue = true;
            dgvMonHoc.Columns.Add(colFree);


            // Thêm cột Xóa
            DataGridViewButtonColumn colDelete = new DataGridViewButtonColumn();
            colDelete.HeaderText = "Xóa";
            colDelete.Text = "Xóa";
            colDelete.UseColumnTextForButtonValue = true;
            dgvMonHoc.Columns.Add(colDelete);
            */
            // Dữ liệu mẫu
            dsMonHoc.Add(new MonHoc() { MaMon = "MH01", TenMon = "Toán", SoTiet = 4 });
            dsMonHoc.Add(new MonHoc() { MaMon = "MH02", TenMon = "Vật Lý", SoTiet = 3 });
            dsMonHoc.Add(new MonHoc() { MaMon = "MH03", TenMon = "Hóa Học", SoTiet = 3 });

            dgvMonHoc.DataSource = dsMonHoc;

            // Gắn sự kiện click
            dgvMonHoc.CellClick += dgvMonHoc_CellClick;
        }
        private void dgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Cột Lưu (Thêm/Sửa)
                if (e.ColumnIndex == 3) // sau 3 cột đã có
                {
                    var mon = dgvMonHoc.Rows[e.RowIndex].DataBoundItem as MonHoc;
                    if (mon != null)
                    {
                        var exist = dsMonHoc.FirstOrDefault(x => x.MaMon == mon.MaMon);
                        if (exist != null)
                        {
                            exist.TenMon = mon.TenMon;
                            exist.SoTiet = mon.SoTiet;
                            dgvMonHoc.Refresh();
                            MessageBox.Show("Cập nhật môn học thành công!");
                        }
                        else
                        {
                            dsMonHoc.Add(mon);
                            MessageBox.Show("Thêm môn học thành công!");
                        }
                    }
                }
                // Cột Xóa
                else if (e.ColumnIndex == 4)
                {
                    var mon = dgvMonHoc.Rows[e.RowIndex].DataBoundItem as MonHoc;
                    if (mon != null)
                    {
                        var confirm = MessageBox.Show($"Bạn có chắc muốn xóa {mon.TenMon}?",
                            "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirm == DialogResult.Yes)
                        {
                            dsMonHoc.Remove(mon);
                        }
                    }
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMonHoc.AutoGenerateColumns = false;
            dgvMonHoc.AllowUserToAddRows = false;

            // Cột Mã môn
            DataGridViewTextBoxColumn colMa = new DataGridViewTextBoxColumn();
            colMa.HeaderText = "Mã Môn";
            colMa.DataPropertyName = "MaMon";
            colMa.Width = 100;
            dgvMonHoc.Columns.Add(colMa);

            // Cột Tên môn
            DataGridViewTextBoxColumn colTen = new DataGridViewTextBoxColumn();
            colTen.HeaderText = "Tên Môn";
            colTen.DataPropertyName = "TenMon";
            colTen.Width = 200;
            dgvMonHoc.Columns.Add(colTen);

            // Cột Số tiết
            DataGridViewTextBoxColumn colTiet = new DataGridViewTextBoxColumn();
            colTiet.HeaderText = "Số Tiết";
            colTiet.DataPropertyName = "SoTiet";
            colTiet.Width = 80;
            dgvMonHoc.Columns.Add(colTiet);

            // Cột nút Thêm/Sửa
            DataGridViewButtonColumn colAddEdit = new DataGridViewButtonColumn();
            colAddEdit.HeaderText = "Thêm/Sửa";
            colAddEdit.Text = "Lưu";
            colAddEdit.UseColumnTextForButtonValue = true;
            colAddEdit.Width = 100;
            dgvMonHoc.Columns.Add(colAddEdit);

            // Cột nút Xóa
            DataGridViewButtonColumn colDelete = new DataGridViewButtonColumn();
            colDelete.HeaderText = "Xóa";
            colDelete.Text = "Xóa";
            colDelete.UseColumnTextForButtonValue = true;
            colDelete.Width = 80;
            dgvMonHoc.Columns.Add(colDelete);

            // Dữ liệu mẫu
            dsMonHoc.Add(new MonHoc() { MaMon = "MH01", TenMon = "Toán", SoTiet = 4 });
            dsMonHoc.Add(new MonHoc() { MaMon = "MH02", TenMon = "Vật Lý", SoTiet = 3 });
            dsMonHoc.Add(new MonHoc() { MaMon = "MH03", TenMon = "Hóa Học", SoTiet = 3 });

            dgvMonHoc.DataSource = dsMonHoc;
        }
    }
    }
