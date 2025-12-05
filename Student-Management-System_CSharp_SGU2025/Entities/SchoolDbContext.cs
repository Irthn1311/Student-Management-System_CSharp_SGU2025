using System.Data.Entity;
using System;

namespace Student_Management_System_CSharp_SGU2025.Entities
{
    /// <summary>
    /// DbContext chung cho các bảng sử dụng LINQ to Entities
    /// Hiện tại có: GiaoVien, MonHoc
    /// Có thể mở rộng thêm các bảng khác khi cần
    /// Các bảng khác vẫn sử dụng ADO.NET
    /// </summary>
    [DbConfigurationType(typeof(MySqlDbConfiguration))]
    public class SchoolDbContext : DbContext
    {
        /// <summary>
        /// Constructor - Sử dụng connection string hardcoded
        /// </summary>
        public SchoolDbContext() 
            : base(GetConnectionString())
        {
            // Tắt lazy loading để tránh vấn đề performance
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Lấy connection string cho Entity Framework - Hardcoded trong code
        /// Để thay đổi cấu hình, sửa trực tiếp trong method này
        /// </summary>
        private static string GetConnectionString()
        {
            // ✅ Cấu hình database - Sửa các giá trị dưới đây
            string server = "127.0.0.1";
            string database = "QuanLyHocSinh";
            string userId = "root";
            string password = "12345678";  // Để trống "" nếu localhost không có password
            int port = 3306;
            int connectionTimeout = 30;

            // Tạo connection string cho Entity Framework
            string serverWithPort = port != 3306 ? $"{server}:{port}" : server;
            string passwordParam = string.IsNullOrEmpty(password) ? "" : $"password={password};";
            return $"server={serverWithPort};database={database};user id={userId};{passwordParam}Connection Timeout={connectionTimeout};";
        }

        /// <summary>
        /// DbSet cho bảng GiaoVien - Sử dụng LINQ to Entities
        /// </summary>
        public DbSet<GiaoVienEntity> GiaoViens { get; set; }

        /// <summary>
        /// DbSet cho bảng MonHoc - Dùng cho join và có thể mở rộng
        /// </summary>
        public DbSet<MonHocEntity> MonHocs { get; set; }

        // TODO: Có thể thêm các DbSet khác khi cần:
        // public DbSet<HocSinhEntity> HocSinhs { get; set; }
        // public DbSet<LopHocEntity> LopHocs { get; set; }
        // ...

        /// <summary>
        /// Cấu hình mapping
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình GiaoVien
            modelBuilder.Entity<GiaoVienEntity>()
                .ToTable("GiaoVien")
                .HasKey(g => g.MaGiaoVien);

            modelBuilder.Entity<GiaoVienEntity>()
                .Property(g => g.MaGiaoVien)
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<GiaoVienEntity>()
                .Property(g => g.HoTen)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<GiaoVienEntity>()
                .Property(g => g.GioiTinh)
                .HasMaxLength(10);

            modelBuilder.Entity<GiaoVienEntity>()
                .Property(g => g.DiaChi)
                .HasMaxLength(255);

            modelBuilder.Entity<GiaoVienEntity>()
                .Property(g => g.SoDienThoai)
                .HasMaxLength(20);

            modelBuilder.Entity<GiaoVienEntity>()
                .Property(g => g.Email)
                .HasMaxLength(100);

            modelBuilder.Entity<GiaoVienEntity>()
                .Property(g => g.TrangThai)
                .HasMaxLength(50);

            // Cấu hình relationship với MonHoc
            modelBuilder.Entity<GiaoVienEntity>()
                .HasOptional(g => g.MonHoc)
                .WithMany()
                .HasForeignKey(g => g.MaMonChuyenMon);

            // Cấu hình MonHoc
            modelBuilder.Entity<MonHocEntity>()
                .ToTable("MonHoc")
                .HasKey(m => m.MaMonHoc);

            modelBuilder.Entity<MonHocEntity>()
                .Property(m => m.TenMonHoc)
                .HasMaxLength(100);

            modelBuilder.Entity<MonHocEntity>()
                .Property(m => m.GhiChu)
                .HasMaxLength(50);
        }
    }
}

