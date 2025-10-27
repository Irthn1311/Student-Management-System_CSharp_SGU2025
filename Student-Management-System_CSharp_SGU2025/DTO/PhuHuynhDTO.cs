using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class PhuHuynhDTO
    {
        private int _maPhuHuynh;
        private string _hoTen;
        private string _soDienThoai;
        private string _email;
        private string _diaChi;

        public PhuHuynhDTO() { }

        public PhuHuynhDTO(int maPhuHuynh, string hoTen, string soDienThoai, string email, string diaChi)
        {
            this.MaPhuHuynh = maPhuHuynh;
            this.HoTen = hoTen;
            this.SoDienThoai = soDienThoai;
            this.Email = email;
            this.DiaChi = diaChi;
        }

        public int MaPhuHuynh
        {
            get { return this._maPhuHuynh; }
            set
            {
                if (value <= 0 && this._maPhuHuynh != 0)
                {
                    throw new ArgumentException("Mã phụ huynh phải là số dương.");
                }
                this._maPhuHuynh = value;
            }
        }

        public string HoTen
        {
            get { return this._hoTen; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Họ tên phụ huynh không được để trống.");
                }
                this._hoTen = value.Trim();
            }
        }

        public string SoDienThoai
        {
            get { return this._soDienThoai; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Số điện thoại phụ huynh không được để trống.");
                }
                this._soDienThoai = value.Trim();
            }
        }

        public string Email
        {
            get { return this._email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email phụ huynh không được để trống.");
                }
                this._email = value.Trim();
            }
        }

        public string DiaChi
        {
            get { return this._diaChi; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Địa chỉ phụ huynh không được để trống.");
                }
                this._diaChi = value.Trim();
            }
        }
    }
}