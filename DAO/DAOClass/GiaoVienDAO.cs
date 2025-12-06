using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using Student_Management_System_CSharp_SGU2025.DAO.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class GiaoVienDAO
    {
        public List<GiaoVienDTO> GetByHocKy(int hocKyId)
        {
            // Giáo viên độc lập theo học kỳ → trả tất cả, có thể mở rộng sau
            return DocDSGiaoVien();
        }

        // ✅ GetChuyenMon - Sử dụng LINQ to Entities
        public List<int> GetChuyenMon(string maGiaoVien)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ LINQ to Entities query
                    var results = db.GiaoViens
                        .Where(g => g.MaGiaoVien == maGiaoVien && g.MaMonChuyenMon.HasValue)
                        .Select(g => g.MaMonChuyenMon.Value)
                        .Distinct()
                        .ToList();
                    
                    return results;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi GetChuyenMon: {ex.Message}");
                throw;
            }
        }

        public int GetCurrentLoad(string maGiaoVien, int hocKyId)
        {
            string query = @"SELECT COALESCE(SUM(m.SoTiet),0) AS LoadTiet
                             FROM PhanCongGiangDay pc JOIN MonHoc m ON pc.MaMonHoc=m.MaMonHoc
                             WHERE pc.MaGiaoVien=@MaGiaoVien AND pc.MaHocKy=@MaHocKy";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                    cmd.Parameters.AddWithValue("@MaHocKy", hocKyId);
                    object val = cmd.ExecuteScalar();
                    return Convert.ToInt32(val);
                }
            }
        }
        // ✅ LẤY TÊN GIÁO VIÊN THEO MÃ - Sử dụng LINQ to Entities
        public string LayTenGiaoVienTheoMa(string maGiaoVien)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ LINQ to Entities query
                    var tenGiaoVien = db.GiaoViens
                        .Where(g => g.MaGiaoVien == maGiaoVien)
                        .Select(g => g.HoTen)
                        .FirstOrDefault();
                    
                    return tenGiaoVien;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayTenGiaoVienTheoMa: {ex.Message}");
                throw;
            }
        }

        // ✅ Thêm giáo viên - Sử dụng LINQ to Entities
        public bool ThemGiaoVien(GiaoVienDTO giaoVien)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ Chuyển đổi từ DTO sang Entity
                    var entity = new GiaoVienEntity
                    {
                        MaGiaoVien = giaoVien.MaGiaoVien,
                        HoTen = giaoVien.HoTen,
                        NgaySinh = giaoVien.NgaySinh != DateTime.MinValue ? (DateTime?)giaoVien.NgaySinh : null,
                        GioiTinh = giaoVien.GioiTinh,
                        DiaChi = giaoVien.DiaChi,
                        SoDienThoai = giaoVien.SoDienThoai,
                        Email = giaoVien.Email,
                        MaMonChuyenMon = giaoVien.MaMonChuyenMon,
                        TrangThai = giaoVien.TrangThai ?? "Đang giảng dạy"
                    };

                    // ✅ LINQ to Entities - Add và SaveChanges
                    db.GiaoViens.Add(entity);
                    int result = db.SaveChanges();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ThemGiaoVien: {ex.Message}");
                throw;
            }
        }

        // ✅ Đọc danh sách giáo viên - Sử dụng LINQ to Entities
        public List<GiaoVienDTO> DocDSGiaoVien()
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ LINQ to Entities query với LEFT JOIN
                    var danhSach = (from gv in db.GiaoViens
                                   join mh in db.MonHocs on gv.MaMonChuyenMon equals mh.MaMonHoc into monHocGroup
                                   from mh in monHocGroup.DefaultIfEmpty()
                                   orderby gv.HoTen
                                   select new GiaoVienDTO
                                   {
                                       MaGiaoVien = gv.MaGiaoVien,
                                       HoTen = gv.HoTen,
                                       NgaySinh = gv.NgaySinh ?? DateTime.MinValue,
                                       GioiTinh = gv.GioiTinh ?? "",
                                       DiaChi = gv.DiaChi ?? "",
                                       SoDienThoai = gv.SoDienThoai ?? "",
                                       Email = gv.Email ?? "",
                                       TrangThai = gv.TrangThai ?? "Đang giảng dạy",
                                       MaMonChuyenMon = gv.MaMonChuyenMon,
                                       TenMonChuyenMon = mh != null ? mh.TenMonHoc : ""
                                   }).ToList();

                    return danhSach;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi DocDSGiaoVien: {ex.Message}");
                throw;
            }
        }

        // ✅ Lấy giáo viên theo mã - Sử dụng LINQ to Entities
        public GiaoVienDTO LayGiaoVienTheoMa(string maGiaoVien)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ LINQ to Entities query với LEFT JOIN
                    var giaoVien = (from gv in db.GiaoViens
                                   where gv.MaGiaoVien == maGiaoVien
                                   join mh in db.MonHocs on gv.MaMonChuyenMon equals mh.MaMonHoc into monHocGroup
                                   from mh in monHocGroup.DefaultIfEmpty()
                                   select new GiaoVienDTO
                                   {
                                       MaGiaoVien = gv.MaGiaoVien,
                                       HoTen = gv.HoTen,
                                       NgaySinh = gv.NgaySinh ?? DateTime.MinValue,
                                       GioiTinh = gv.GioiTinh ?? "",
                                       DiaChi = gv.DiaChi ?? "",
                                       SoDienThoai = gv.SoDienThoai ?? "",
                                       Email = gv.Email ?? "",
                                       TrangThai = gv.TrangThai ?? "Đang giảng dạy",
                                       MaMonChuyenMon = gv.MaMonChuyenMon,
                                       TenMonChuyenMon = mh != null ? mh.TenMonHoc : ""
                                   }).FirstOrDefault();

                    return giaoVien;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayGiaoVienTheoMa: {ex.Message}");
                throw;
            }
        }

        // ✅ Cập nhật giáo viên - Sử dụng LINQ to Entities
        public bool CapNhatGiaoVien(GiaoVienDTO giaoVien)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ LINQ to Entities - Tìm entity theo mã
                    var entity = db.GiaoViens.FirstOrDefault(g => g.MaGiaoVien == giaoVien.MaGiaoVien);
                    
                    if (entity == null)
                        return false;

                    // ✅ Cập nhật các thuộc tính
                    entity.HoTen = giaoVien.HoTen;
                    entity.NgaySinh = giaoVien.NgaySinh != DateTime.MinValue ? (DateTime?)giaoVien.NgaySinh : null;
                    entity.GioiTinh = giaoVien.GioiTinh;
                    entity.DiaChi = giaoVien.DiaChi;
                    entity.SoDienThoai = giaoVien.SoDienThoai;
                    entity.Email = giaoVien.Email;
                    entity.MaMonChuyenMon = giaoVien.MaMonChuyenMon;
                    entity.TrangThai = giaoVien.TrangThai;

                    // ✅ LINQ to Entities - SaveChanges
                    int result = db.SaveChanges();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CapNhatGiaoVien: {ex.Message}");
                throw;
            }
        }

        // ✅ Xóa giáo viên - Sử dụng LINQ to Entities
        public bool XoaGiaoVien(string maGiaoVien)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ LINQ to Entities - Tìm entity theo mã
                    var entity = db.GiaoViens.FirstOrDefault(g => g.MaGiaoVien == maGiaoVien);
                    
                    if (entity == null)
                        return false;

                    // ✅ LINQ to Entities - Remove và SaveChanges
                    db.GiaoViens.Remove(entity);
                    int result = db.SaveChanges();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi XoaGiaoVien: {ex.Message}");
                throw;
            }
        }

        // ✅ Kiểm tra email đã tồn tại chưa - Sử dụng LINQ to Entities
        public bool KiemTraEmailTonTai(string email, string maGiaoVienHienTai = null)
        {
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ LINQ to Entities query
                    var query = db.GiaoViens.Where(g => g.Email == email);
                    
                    if (!string.IsNullOrEmpty(maGiaoVienHienTai))
                    {
                        query = query.Where(g => g.MaGiaoVien != maGiaoVienHienTai);
                    }
                    
                    return query.Any();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi KiemTraEmailTonTai: {ex.Message}");
                throw;
            }
        }

        // ✅ Lấy mã giáo viên tiếp theo tự động - Sử dụng LINQ to Entities
        public string LayMaGiaoVienTiepTheo()
        {
            string maTiepTheo = "GV001"; // Mã mặc định nếu chưa có giáo viên nào
            
            try
            {
                using (var db = new SchoolDbContext())
                {
                    // ✅ LINQ to Entities - Lấy mã giáo viên lớn nhất
                    // Materialize query first, then parse in memory (cannot use out parameters in expression trees)
                    var maGiaoViens = db.GiaoViens
                        .Where(gv => !string.IsNullOrEmpty(gv.MaGiaoVien) && gv.MaGiaoVien.StartsWith("GV"))
                        .Select(gv => gv.MaGiaoVien)
                        .ToList();
                    
                    var maxSo = maGiaoViens
                        .Select(ma =>
                        {
                            string soStr = ma.Substring(2);
                            return int.TryParse(soStr, out int so) ? so : 0;
                        })
                        .DefaultIfEmpty(0)
                        .Max();
                    
                    maTiepTheo = $"GV{(maxSo + 1):D3}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayMaGiaoVienTiepTheo: {ex.Message}");
                // Nếu có lỗi, trả về mã mặc định
            }
            
            return maTiepTheo;
        }
    }
}