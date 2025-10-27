using System;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class HocSinhDTO
    {
        private int _maHS;
        private string _hoTen;
        private DateTime _ngaySinh;
        private string _gioiTinh;
        private string _sdtHS;
        private string _email;
        private string _trangThai;

        public HocSinhDTO() { }

        public HocSinhDTO(int maHS, string hoTen, DateTime ngaySinh, string gioiTinh, string sdtHS, string email, string trangThai)
        {
            this.MaHS = maHS;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.SdtHS = sdtHS;
            this.Email = email;
            this.TrangThai = trangThai;
        }

        public int MaHS
        {
            get { return this._maHS; }
            set
            {
                if (value <= 0 && this._maHS != 0)
                {
                    throw new ArgumentException("Mã học sinh phải là số dương.");
                }
                this._maHS = value;
            }
        }

        public string HoTen
        {
            get { return this._hoTen; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Họ tên không được để trống.");
                }
                this._hoTen = value.Trim();
            }
        }

        public DateTime NgaySinh
        {
            get { return this._ngaySinh; }
            set { this._ngaySinh = value; }
        }

        public string GioiTinh
        {
            get { return this._gioiTinh; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Giới tính không được để trống.");
                }
                this._gioiTinh = value;
            }
        }

        public string SdtHS
        {
            get { return this._sdtHS; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Số điện thoại học sinh không được để trống.");
                }
                this._sdtHS = value.Trim();
            }
        }

        public string Email
        {
            get { return this._email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Email học sinh không được để trống.");
                }
                this._email = value.Trim();
            }
        }

        public string TrangThai
        {
            get { return this._trangThai; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Trạng thái không được để trống.");
                }
                this._trangThai = value.Trim();
            }
        }
    }
}