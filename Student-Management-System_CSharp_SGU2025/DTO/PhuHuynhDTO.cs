using System; // Có thể không cần thiết nếu không dùng DateTime, etc.

namespace Student_Management_System_CSharp_SGU2025.DTO // Đảm bảo đúng namespace
{
    public class PhuHuynhDTO 
    {
        // Fields
        private int _maPhuHuynh;
        private string _hoTen;
        private string _soDienThoai;
        private string _email;
        private string _diaChi;

        // Constructors
        public PhuHuynhDTO() { }

        public PhuHuynhDTO(int maPhuHuynh, string hoTen, string soDienThoai, string email, string diaChi)
        {
            this._maPhuHuynh = maPhuHuynh;
            this._hoTen = hoTen;
            this._soDienThoai = soDienThoai;
            this._email = email;
            this._diaChi = diaChi;
        }

        // Properties
        public int MaPhuHuynh
        {
            get { return this._maPhuHuynh; }
            set { this._maPhuHuynh = value; }
        }

        public string HoTen
        {
            get { return this._hoTen; }
            set { this._hoTen = value; }
        }

        public string SoDienThoai
        {
            get { return this._soDienThoai; }
            set { this._soDienThoai = value; }
        }

        public string Email
        {
            get { return this._email; }
            set { this._email = value; }
        }

        public string DiaChi
        {
            get { return this._diaChi; }
            set { this._diaChi = value; }
        }
    }
}