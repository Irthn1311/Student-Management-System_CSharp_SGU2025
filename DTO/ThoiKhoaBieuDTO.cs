using System;
using System.Text;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    internal class ThoiKhoaBieu
    {
        // Fields (Thuộc tính cơ bản)
        private int maThoiKhoaBieu;
        private int maPhanCong;
        private string thuTrongTuan;
        private int tietBatDau;
        private int soTiet;
        private string phongHoc;

        // Constructors (Hàm khởi tạo)
        public ThoiKhoaBieu() { }

        public ThoiKhoaBieu(int maThoiKhoaBieu, int maPhanCong, string thuTrongTuan, int tietBatDau, int soTiet, string phongHoc)
        {
            this.MaThoiKhoaBieu = maThoiKhoaBieu;
            this.MaPhanCong = maPhanCong;
            this.ThuTrongTuan = thuTrongTuan;
            this.TietBatDau = tietBatDau;
            this.SoTiet = soTiet;
            this.PhongHoc = phongHoc;
        }

        // Destructor (Hàm hủy)
        ~ThoiKhoaBieu()
        {
            // Console.WriteLine("Huy doi tuong ThoiKhoaBieu");
        }

        // Properties (Thuộc tính)
        public int MaThoiKhoaBieu
        {
            get { return maThoiKhoaBieu; }
            set
            {
                // Thường là AUTO_INCREMENT, nhưng vẫn thêm kiểm tra cơ bản
                if (value >= 0)
                {
                    maThoiKhoaBieu = value;
                }
                else
                {
                    Console.WriteLine("Ma thoi khoa bieu khong hop le.");
                }
            }
        }

        public int MaPhanCong
        {
            get { return maPhanCong; }
            set
            {
                if (value > 0)
                {
                    maPhanCong = value;
                }
                else
                {
                    Console.WriteLine("Ma phan cong phai lon hon 0.");
                }
            }
        }

        public string ThuTrongTuan
        {
            get { return thuTrongTuan; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    thuTrongTuan = value;
                }
                else
                {
                    Console.WriteLine("Thu trong tuan khong duoc de trong.");
                }
            }
        }

        public int TietBatDau
        {
            get { return tietBatDau; }
            set
            {
                if (value > 0)
                {
                    tietBatDau = value;
                }
                else
                {
                    Console.WriteLine("Tiet bat dau phai lon hon 0.");
                }
            }
        }

        public int SoTiet
        {
            get { return soTiet; }
            set
            {
                if (value > 0)
                {
                    soTiet = value;
                }
                else
                {
                    Console.WriteLine("So tiet phai lon hon 0.");
                }
            }
        }

        public string PhongHoc
        {
            get { return phongHoc; }
            set { phongHoc = value; }
        }
    }
}