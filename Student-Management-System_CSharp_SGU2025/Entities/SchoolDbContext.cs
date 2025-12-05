using System.Configuration;
using System.Data.Entity;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.Config;
using System;

namespace Student_Management_System_CSharp_SGU2025.Entities
{
    /// <summary>
    /// DbContext chung cho các bảng sử dụng LINQ to Entities
    /// Hiện tại có: GiaoVien, MonHoc
    /// Có thể mở rộng thêm các bảng khác khi cần
    /// Các bảng khác vẫn sử dụng ADO.NET
    /// </summary>
    public class SchoolDbContext : DbContext
    {
        /// <summary>
        /// Constructor - Ưu tiên connection string từ App.config, fallback về ConnectionDatabase
        /// </summary>
        public SchoolDbContext() 
            : base(GetConnectionStringOrName())
        {
            // Tắt lazy loading để tránh vấn đề performance
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Lấy connection string - Ưu tiên từ database_config.json, fallback về App.config
        /// </summary>
        private static string GetConnectionStringOrName()
        {
            try
            {
                // ✅ Ưu tiên 1: Lấy từ database_config.json
                return DatabaseConfig.GetEntityFrameworkConnectionString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"⚠️ Không thể đọc connection string từ database_config.json: {ex.Message}");
            }

            // ✅ Fallback: Sử dụng connection string name từ App.config
            try
            {
                var connStringFromConfig = ConfigurationManager.ConnectionStrings["SchoolDbContext"];
                if (connStringFromConfig != null)
                {
                    // Nếu có connection string name, sử dụng name (EF6 sẽ tự động lấy từ App.config)
                    return "SchoolDbContext";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"⚠️ Không thể đọc connection string từ App.config: {ex.Message}");
            }

            // ✅ Fallback cuối cùng: Lấy connection string trực tiếp
            return GetConnectionString();
        }

        /// <summary>
        /// Lấy connection string - Ưu tiên từ database_config.json, fallback về App.config
        /// </summary>
        private static string GetConnectionString()
        {
            try
            {
                // ✅ Ưu tiên 1: Lấy từ database_config.json
                return DatabaseConfig.GetEntityFrameworkConnectionString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"⚠️ Không thể đọc connection string từ database_config.json: {ex.Message}");
            }

            // ✅ Fallback: Lấy từ App.config nếu có
            try
            {
                var connStringFromConfig = ConfigurationManager.ConnectionStrings["SchoolDbContext"];
                if (connStringFromConfig != null && !string.IsNullOrEmpty(connStringFromConfig.ConnectionString))
                {
                    return connStringFromConfig.ConnectionString;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"⚠️ Không thể đọc connection string từ App.config: {ex.Message}");
            }

            // ✅ Fallback cuối cùng: Connection string mặc định
            throw new Exception($"❌ Không thể tạo connection string!\n\n" +
                "Vui lòng kiểm tra:\n" +
                "1. File Config/database_config.json tồn tại và hợp lệ\n" +
                "2. MySQL Server đang chạy\n" +
                "3. Database 'QuanLyHocSinh' đã được tạo");
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

