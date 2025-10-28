using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class GiaoVienDAO
    {
        public List<GiaoVienDTO> GetByHocKy(int hocKyId)
        {
            // Giáo viên độc lập theo học kỳ → trả tất cả, có thể mở rộng sau
            return DocDSGiaoVien();
        }

        public List<int> GetChuyenMon(string maGiaoVien)
        {
            var ds = new List<int>();
            string query = @"SELECT MaMonHoc FROM GiaoVien_MonHoc WHERE MaGiaoVien=@MaGiaoVien";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ds.Add(reader.GetInt32("MaMonHoc"));
                        }
                    }
                }
            }
            return ds;
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
        // ✅ LẤY TÊN GIÁO VIÊN THEO MÃ (Requirement chính của bạn)
        public string LayTenGiaoVienTheoMa(string maGiaoVien)
        {
            string tenGiaoVien = null;
            string query = "SELECT HoTen FROM GiaoVien WHERE MaGiaoVien = @MaGiaoVien";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                        object result = cmd.ExecuteScalar();
                        
                        if (result != null)
                        {
                            tenGiaoVien = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayTenGiaoVienTheoMa: {ex.Message}");
                throw;
            }

            return tenGiaoVien;
        }

        // Thêm giáo viên
        public bool ThemGiaoVien(GiaoVienDTO giaoVien)
        {
            string query = "INSERT INTO GiaoVien(MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, TrangThai) " +
                          "VALUES(@MaGiaoVien, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @Email, @TrangThai)";
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaGiaoVien", giaoVien.MaGiaoVien);
                        cmd.Parameters.AddWithValue("@HoTen", giaoVien.HoTen);
                        cmd.Parameters.AddWithValue("@NgaySinh", giaoVien.NgaySinh);
                        cmd.Parameters.AddWithValue("@GioiTinh", giaoVien.GioiTinh);
                        cmd.Parameters.AddWithValue("@DiaChi", giaoVien.DiaChi ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoDienThoai", giaoVien.SoDienThoai ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", giaoVien.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", giaoVien.TrangThai ?? "Đang giảng dạy");
                        
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ThemGiaoVien: {ex.Message}");
                throw;
            }
        }

        // Đọc danh sách giáo viên
        public List<GiaoVienDTO> DocDSGiaoVien()
        {
            List<GiaoVienDTO> ds = new List<GiaoVienDTO>();
            string query = "SELECT MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, TrangThai FROM GiaoVien ORDER BY HoTen";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GiaoVienDTO gv = new GiaoVienDTO
                                {
                                    MaGiaoVien = reader.GetString("MaGiaoVien"),
                                    HoTen = reader.GetString("HoTen"),
                                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? DateTime.MinValue : reader.GetDateTime("NgaySinh"),
                                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString("GioiTinh"),
                                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString("DiaChi"),
                                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? "" : reader.GetString("SoDienThoai"),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email"),
                                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? "Đang giảng dạy" : reader.GetString("TrangThai")
                                };
                                ds.Add(gv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi DocDSGiaoVien: {ex.Message}");
                throw;
            }

            return ds;
        }

        // Lấy giáo viên theo mã
        public GiaoVienDTO LayGiaoVienTheoMa(string maGiaoVien)
        {
            GiaoVienDTO giaoVien = null;
            string query = "SELECT MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, TrangThai FROM GiaoVien WHERE MaGiaoVien = @MaGiaoVien";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                giaoVien = new GiaoVienDTO
                                {
                                    MaGiaoVien = reader.GetString("MaGiaoVien"),
                                    HoTen = reader.GetString("HoTen"),
                                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? DateTime.MinValue : reader.GetDateTime("NgaySinh"),
                                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString("GioiTinh"),
                                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString("DiaChi"),
                                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? "" : reader.GetString("SoDienThoai"),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email"),
                                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? "Đang giảng dạy" : reader.GetString("TrangThai")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayGiaoVienTheoMa: {ex.Message}");
                throw;
            }

            return giaoVien;
        }

        // Cập nhật giáo viên
        public bool CapNhatGiaoVien(GiaoVienDTO giaoVien)
        {
            string query = "UPDATE GiaoVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, " +
                          "DiaChi=@DiaChi, SoDienThoai=@SoDienThoai, Email=@Email, TrangThai=@TrangThai " +
                          "WHERE MaGiaoVien=@MaGiaoVien";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HoTen", giaoVien.HoTen);
                        cmd.Parameters.AddWithValue("@NgaySinh", giaoVien.NgaySinh);
                        cmd.Parameters.AddWithValue("@GioiTinh", giaoVien.GioiTinh);
                        cmd.Parameters.AddWithValue("@DiaChi", giaoVien.DiaChi ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoDienThoai", giaoVien.SoDienThoai ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", giaoVien.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", giaoVien.TrangThai);
                        cmd.Parameters.AddWithValue("@MaGiaoVien", giaoVien.MaGiaoVien);
                        
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CapNhatGiaoVien: {ex.Message}");
                throw;
            }
        }

        // Xóa giáo viên
        public bool XoaGiaoVien(string maGiaoVien)
        {
            string query = "DELETE FROM GiaoVien WHERE MaGiaoVien = @MaGiaoVien";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error [{ex.Number}]: {ex.Message}");
                throw new Exception($"Lỗi database: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi XoaGiaoVien: {ex.Message}");
                throw;
            }
        }

        // Kiểm tra email đã tồn tại chưa
        public bool KiemTraEmailTonTai(string email, string maGiaoVienHienTai = null)
        {
            string query = "SELECT COUNT(*) FROM GiaoVien WHERE Email = @Email";
            
            if (!string.IsNullOrEmpty(maGiaoVienHienTai))
            {
                query += " AND MaGiaoVien != @MaGiaoVien";
            }

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        if (!string.IsNullOrEmpty(maGiaoVienHienTai))
                        {
                            cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVienHienTai);
                        }
                        
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi KiemTraEmailTonTai: {ex.Message}");
                throw;
            }
        }
    }
}