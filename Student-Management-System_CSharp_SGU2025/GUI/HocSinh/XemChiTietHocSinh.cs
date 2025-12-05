using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.GUI.HocSinh;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI
{
    public partial class XemChiTietHocSinh : Form
    {
        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        private LopHocBUS lopHocBUS;
        private PhanLopBLL phanLopBLL;
        private HocKyBUS hocKyBUS;

        private int maHocSinh;
        private HocSinhDTO hocSinh;

        public XemChiTietHocSinh(int maHocSinh)
        {
            InitializeComponent();
            this.maHocSinh = maHocSinh;

            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            lopHocBUS = new LopHocBUS();
            phanLopBLL = new PhanLopBLL();
            hocKyBUS = new HocKyBUS();

            LoadThongTinHocSinh();
        }

        private void LoadThongTinHocSinh()
        {
            try
            {
                // Lấy thông tin học sinh
                hocSinh = hocSinhBLL.GetHocSinhById(maHocSinh);
                if (hocSinh == null)
                {
                    MessageBox.Show($"Không tìm thấy học sinh với mã {maHocSinh}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // === THÔNG TIN CÁ NHÂN - Hiển thị qua StudentCard ===
                if (studentCard != null)
                {
                    // Lấy thông tin lớp để truyền vào StudentCard
                    string tenLop = "";
                    string tenGVCN = "";
                    try
                    {
                        int maHocKyHienTai = 0;
                        List<HocKyDTO> dsHocKy = hocKyBUS.DocDSHocKy();
                        if (dsHocKy != null && dsHocKy.Count > 0)
                        {
                            var hocKyDangDienRa = dsHocKy.FirstOrDefault(hk => hk.TrangThai == "Đang diễn ra");
                            if (hocKyDangDienRa != null)
                            {
                                maHocKyHienTai = hocKyDangDienRa.MaHocKy;
                            }
                            else
                            {
                                var hocKyMoiNhat = dsHocKy.OrderByDescending(hk => hk.NgayBD).FirstOrDefault();
                                if (hocKyMoiNhat != null)
                                {
                                    maHocKyHienTai = hocKyMoiNhat.MaHocKy;
                                }
                            }
                        }

                        if (maHocKyHienTai > 0)
                        {
                            int maLop = phanLopBLL.GetLopByHocSinh(maHocSinh, maHocKyHienTai);
                            if (maLop > 0)
                            {
                                var lop = lopHocBUS.LayLopTheoId(maLop);
                                if (lop != null)
                                {
                                    tenLop = lop.tenLop;
                                    
                                    if (!string.IsNullOrEmpty(lop.maGVCN))
                                    {
                                        try
                                        {
                                            GiaoVienBUS giaoVienBUS = new GiaoVienBUS();
                                            GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(lop.maGVCN);
                                            if (gv != null)
                                            {
                                                tenGVCN = gv.HoTen;
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                    
                    studentCard.LoadStudentInfo(hocSinh, tenLop, tenGVCN);
                }

                // === THÔNG TIN LỚP HIỆN TẠI ===
                LoadThongTinLop();

                // === DANH SÁCH PHỤ HUYNH ===
                LoadDanhSachPhuHuynh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin học sinh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongTinLop()
        {
            try
            {
                // Lấy học kỳ hiện tại
                int maHocKyHienTai = 0;
                List<HocKyDTO> dsHocKy = hocKyBUS.DocDSHocKy();
                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    var hocKyDangDienRa = dsHocKy.FirstOrDefault(hk => hk.TrangThai == "Đang diễn ra");
                    if (hocKyDangDienRa != null)
                    {
                        maHocKyHienTai = hocKyDangDienRa.MaHocKy;
                    }
                    else
                    {
                        var hocKyMoiNhat = dsHocKy.OrderByDescending(hk => hk.NgayBD).FirstOrDefault();
                        if (hocKyMoiNhat != null)
                        {
                            maHocKyHienTai = hocKyMoiNhat.MaHocKy;
                        }
                    }
                }

                if (maHocKyHienTai > 0)
                {
                    int maLop = phanLopBLL.GetLopByHocSinh(maHocSinh, maHocKyHienTai);
                    if (maLop > 0)
                    {
                        var lop = lopHocBUS.LayLopTheoId(maLop);
                        if (lop != null)
                        {
                            lblLopHienTai.Text = $"Lớp hiện tại: {lop.tenLop}";
                            
                            // Lấy thông tin giáo viên chủ nhiệm
                            if (!string.IsNullOrEmpty(lop.maGVCN))
                            {
                                try
                                {
                                    GiaoVienBUS giaoVienBUS = new GiaoVienBUS();
                                    GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(lop.maGVCN);
                                    if (gv != null)
                                    {
                                        lblGVCNLop.Text = $"GVCN: {gv.HoTen}";
                                        lblSDTGVCN.Text = $"SĐT GVCN: {gv.SoDienThoai ?? "N/A"}";
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    else
                    {
                        lblLopHienTai.Text = "Lớp hiện tại: Chưa phân lớp";
                        lblLopHienTai.ForeColor = Color.FromArgb(234, 179, 8);
                    }
                }
                else
                {
                    lblLopHienTai.Text = "Lớp hiện tại: Chưa có học kỳ";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy thông tin lớp: {ex.Message}");
            }
        }

        private void LoadDanhSachPhuHuynh()
        {
            try
            {
                dgvPhuHuynh.Rows.Clear();

                var dsQuanHe = hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(maHocSinh);
                if (dsQuanHe == null || dsQuanHe.Count == 0)
                {
                    dgvPhuHuynh.Rows.Add("Chưa có phụ huynh", "", "", "");
                    return;
                }

                foreach (var qh in dsQuanHe)
                {
                    dgvPhuHuynh.Rows.Add(
                        qh.phuHuynh.HoTen,
                        qh.phuHuynh.SoDienThoai ?? "N/A",
                        qh.phuHuynh.Email ?? "N/A",
                        qh.moiQuanHe
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách phụ huynh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

