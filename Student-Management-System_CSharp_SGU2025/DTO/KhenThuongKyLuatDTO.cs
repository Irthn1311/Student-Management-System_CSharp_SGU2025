using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class KhenThuongKyLuatDTO
    {
        private int maKTKL;
        private int maHocSinh;
        private string loai;
        private string noiDung;
        private string capKhenThuong;
        private string mucXuLy;
        private DateTime ngayApDung;
        private string nguoiLapID;
        private string trangThaiDuyet;

        public KhenThuongKyLuatDTO() { }

        public KhenThuongKyLuatDTO(int maKTKL, int maHocSinh, string loai, string noiDung,
            string capKhenThuong, string mucXuLy, DateTime ngayApDung, string nguoiLapID, string trangThaiDuyet)
        {
            this.maKTKL = maKTKL;
            this.maHocSinh = maHocSinh;
            this.loai = loai;
            this.noiDung = noiDung;
            this.capKhenThuong = capKhenThuong;
            this.mucXuLy = mucXuLy;
            this.ngayApDung = ngayApDung;
            this.nguoiLapID = nguoiLapID;
            this.trangThaiDuyet = trangThaiDuyet;
        }

        public KhenThuongKyLuatDTO(int maHocSinh, string loai, string noiDung,
            string capKhenThuong, string mucXuLy, DateTime ngayApDung, string nguoiLapID, string trangThaiDuyet)
        {
            this.maHocSinh = maHocSinh;
            this.loai = loai;
            this.noiDung = noiDung;
            this.capKhenThuong = capKhenThuong;
            this.mucXuLy = mucXuLy;
            this.ngayApDung = ngayApDung;
            this.nguoiLapID = nguoiLapID;
            this.trangThaiDuyet = trangThaiDuyet;
        }

        ~KhenThuongKyLuatDTO()
        {
            Console.WriteLine("Huy doi tuong KhenThuongKyLuatDTO");
        }

        public int MaKTKL
        {
            get { return maKTKL; }
            set
            {
                if (value >= 0)
                {
                    maKTKL = value;
                }
                else
                {
                    Console.WriteLine("MaKTKL phai lon hon hoac bang 0");
                }
            }
        }

        public int MaHocSinh
        {
            get { return maHocSinh; }
            set
            {
                if (value > 0)
                {
                    maHocSinh = value;
                }
                else
                {
                    Console.WriteLine("Ma hoc sinh phai lon hon 0");
                }
            }
        }

        public string Loai
        {
            get { return loai; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Loai khong duoc de trong");
                }
                else
                {
                    loai = value.Trim();
                }
            }
        }

        public string NoiDung
        {
            get { return noiDung; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Noi dung khong duoc de trong");
                }
                else
                {
                    noiDung = value.Trim();
                }
            }
        }

        public string CapKhenThuong
        {
            get { return capKhenThuong; }
            set
            {
                // Chỉ cần kiểm tra nếu là loại Khen thưởng
                if (!string.IsNullOrWhiteSpace(value))
                {
                    capKhenThuong = value.Trim();
                }
                else
                {
                    capKhenThuong = null; // Cho phép null nếu là Kỷ luật
                }
            }
        }

        public string MucXuLy
        {
            get { return mucXuLy; }
            set
            {
                // Chỉ cần kiểm tra nếu là loại Kỷ luật
                if (!string.IsNullOrWhiteSpace(value))
                {
                    mucXuLy = value.Trim();
                }
                else
                {
                    mucXuLy = null; // Cho phép null nếu là Khen thưởng
                }
            }
        }

        public DateTime NgayApDung
        {
            get { return ngayApDung; }
            set
            {
                if (value.Year < 2000 || value > DateTime.Now.AddYears(1))
                {
                    Console.WriteLine("Ngay ap dung khong hop le");
                }
                else
                {
                    ngayApDung = value;
                }
            }
        }

        public string NguoiLapID
        {
            get { return nguoiLapID; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Nguoi lap ID khong duoc de trong");
                }
                else
                {
                    nguoiLapID = value.Trim();
                }
            }
        }

        public string TrangThaiDuyet
        {
            get { return trangThaiDuyet; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Trang thai duyet khong duoc de trong");
                }
                else
                {
                    trangThaiDuyet = value.Trim();
                }
            }
        }
    }
}
