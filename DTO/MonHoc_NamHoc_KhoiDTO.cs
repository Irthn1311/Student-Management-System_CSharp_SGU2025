using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.DTO
{
    public class MonHoc_NamHoc_KhoiDTO
    {
        public int maMonHoc;
        public string maNamHoc;
        public int maKhoi;

        public MonHoc_NamHoc_KhoiDTO() { }

        public MonHoc_NamHoc_KhoiDTO(int maMonHoc, string maNamHoc, int maKhoi)
        {
            this.maMonHoc = maMonHoc;
            this.maNamHoc = maNamHoc;
            this.maKhoi = maKhoi;
        }

        public int MaMonHoc
        {
            get { return maMonHoc; }
            set
            {
                if (value > 0)
                {
                    maMonHoc = value;
                }
                else
                {
                    throw new ArgumentException("Mã môn học phải lớn hơn 0");
                }
            }
        }

        public string MaNamHoc
        {
            get { return maNamHoc; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    maNamHoc = value;
                }
                else
                {
                    throw new ArgumentException("Mã năm học không được để trống");
                }
            }
        }

        public int MaKhoi
        {
            get { return maKhoi; }
            set
            {
                if (value > 0)
                {
                    maKhoi = value;
                }
                else
                {
                    throw new ArgumentException("Mã khối phải lớn hơn 0");
                }
            }
        }

        public bool IsValid()
        {
            return maMonHoc > 0 && !string.IsNullOrWhiteSpace(maNamHoc) && maKhoi > 0;
        }
    }
}

