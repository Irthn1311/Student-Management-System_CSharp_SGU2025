using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management_System_CSharp_SGU2025.DAO.Entities
{
    /// <summary>
    /// Entity class cho bảng GiaoVien - Sử dụng LINQ to Entities
    /// </summary>
    [Table("GiaoVien")]
    public class GiaoVienEntity
    {
        [Key]
        [Column("MaGiaoVien")]
        [StringLength(15)]
        public string MaGiaoVien { get; set; }

        [Required]
        [Column("HoTen")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Column("NgaySinh")]
        public DateTime? NgaySinh { get; set; }

        [Column("GioiTinh")]
        [StringLength(10)]
        public string GioiTinh { get; set; }

        [Column("DiaChi")]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [Column("SoDienThoai")]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [Column("Email")]
        [StringLength(100)]
        public string Email { get; set; }

        [Column("MaMonChuyenMon")]
        public int? MaMonChuyenMon { get; set; }

        [Column("TrangThai")]
        [StringLength(50)]
        public string TrangThai { get; set; }

        // Navigation property để join với MonHoc
        [ForeignKey("MaMonChuyenMon")]
        public virtual MonHocEntity MonHoc { get; set; }
    }

    /// <summary>
    /// Entity class cho bảng MonHoc - Dùng cho navigation property
    /// </summary>
    [Table("MonHoc")]
    public class MonHocEntity
    {
        [Key]
        [Column("MaMonHoc")]
        public int MaMonHoc { get; set; }

        [Column("TenMonHoc")]
        [StringLength(100)]
        public string TenMonHoc { get; set; }

        [Column("SoTiet")]
        public int? SoTiet { get; set; }

        [Column("GhiChu")]
        [StringLength(50)]
        public string GhiChu { get; set; }
    }
}

