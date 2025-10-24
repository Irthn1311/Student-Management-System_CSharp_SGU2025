using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class HocSinhDTO
    {
        // Fields
        private int _maHS;
        private string _hoTen;
        private DateTime _ngaySinh;
        private string _gioiTinh;
        private string _sdtHS;
        private string _email;
        private string _trangThai; // Giữ lại trạng thái (Đang học/Nghỉ học)

        // Constructors
        public HocSinhDTO() { }

        public HocSinhDTO(int maHS, string hoTen, DateTime ngaySinh, string gioiTinh, string sdtHS, string email, string trangThai)
        {
            this._maHS = maHS;
            this._hoTen = hoTen;
            this._ngaySinh = ngaySinh;
            this._gioiTinh = gioiTinh;
            this._sdtHS = sdtHS;
            this._email = email;
            this._trangThai = trangThai;
        }

        // Properties
        public int MaHS
        {
            get { return this._maHS; }
            set { this._maHS = value; }
        }

        public string HoTen
        {
            get { return this._hoTen; }
            set { this._hoTen = value; }
        }

        public DateTime NgaySinh
        {
            get { return this._ngaySinh; }
            set { this._ngaySinh = value; }
        }

        public string GioiTinh
        {
            get { return this._gioiTinh; }
            set { this._gioiTinh = value; }
        }

        public string SdtHS
        {
            get { return this._sdtHS; }
            set { this._sdtHS = value; }
        }

        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        public string TrangThai
        {
            get { return this._trangThai; }
            set { this._trangThai = value; }
        }
    }
}
