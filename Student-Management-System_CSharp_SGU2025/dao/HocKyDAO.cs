using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class HocKyDAO
    {
        // ✅ THÊM HỌC KỲ - MaHocKy tự động tăng bởi AUTO_INCREMENT
        public bool ThemHocKy(HocKyDTO hocKy)
        {
            // Không cần truyền MaHocKy vì nó AUTO_INCREMENT
            string query = "INSERT INTO HocKy(TenHocKy, MaNamHoc, NgayBatDau, NgayKetThuc, TrangThai) VALUES(@TenHocKy, @MaNamHoc, @NgayBatDau, @NgayKetThuc, @TrangThai)";
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenHocKy", hocKy.TenHocKy);
                        cmd.Parameters.AddWithValue("@MaNamHoc", hocKy.MaNamHoc);
                        cmd.Parameters.AddWithValue("@NgayBatDau", hocKy.NgayBD);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", hocKy.NgayKT);
                        
                        // Tính trạng thái dựa trên ngày
                        string trangThai = TinhTrangThai(hocKy.NgayBD, hocKy.NgayKT);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ThemHocKy: {ex.Message}");
                throw;
            }
        }

        // ✅ ĐỌC DANH SÁCH HỌC KỲ TỪ DATABASE
        public List<HocKyDTO> DocDSHocKy()
        {
            List<HocKyDTO> ds = new List<HocKyDTO>();
            string query = @"SELECT hk.MaHocKy, hk.TenHocKy, hk.MaNamHoc, nh.TenNamHoc, hk.NgayBatDau, hk.NgayKetThuc 
                            FROM HocKy hk 
                            INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc 
                            ORDER BY hk.NgayBatDau DESC";

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
                                HocKyDTO hk = new HocKyDTO
                                {
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    TenHocKy = reader.GetString("TenHocKy"),
                                    MaNamHoc = reader.GetString("MaNamHoc"),
                                    TenNamHoc = reader.GetString("TenNamHoc"),
                                    NgayBD = reader.GetDateTime("NgayBatDau"),
                                    NgayKT = reader.GetDateTime("NgayKetThuc")
                                };
                                ds.Add(hk);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi DocDSHocKy: {ex.Message}");
                throw;
            }

            return ds;
        }

        // ✅ LẤY HỌC KỲ THEO MÃ
        public HocKyDTO LayHocKyTheoMa(int maHocKy)
        {
            HocKyDTO hocKy = null;
            string query = @"SELECT hk.MaHocKy, hk.TenHocKy, hk.MaNamHoc, nh.TenNamHoc, hk.NgayBatDau, hk.NgayKetThuc 
                            FROM HocKy hk 
                            INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc 
                            WHERE hk.MaHocKy=@MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hocKy = new HocKyDTO
                                {
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    TenHocKy = reader.GetString("TenHocKy"),
                                    MaNamHoc = reader.GetString("MaNamHoc"),
                                    TenNamHoc = reader.GetString("TenNamHoc"),
                                    NgayBD = reader.GetDateTime("NgayBatDau"),
                                    NgayKT = reader.GetDateTime("NgayKetThuc")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayHocKyTheoMa: {ex.Message}");
                throw;
            }

            return hocKy;
        }

        // ✅ CẬP NHẬT HỌC KỲ
        public bool CapNhatHocKy(HocKyDTO hocKy)
        {
            string query = "UPDATE HocKy SET TenHocKy=@TenHocKy, MaNamHoc=@MaNamHoc, NgayBatDau=@NgayBatDau, NgayKetThuc=@NgayKetThuc, TrangThai=@TrangThai WHERE MaHocKy=@MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenHocKy", hocKy.TenHocKy);
                        cmd.Parameters.AddWithValue("@MaNamHoc", hocKy.MaNamHoc);
                        cmd.Parameters.AddWithValue("@NgayBatDau", hocKy.NgayBD);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", hocKy.NgayKT);
                        
                        string trangThai = TinhTrangThai(hocKy.NgayBD, hocKy.NgayKT);
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        
                        cmd.Parameters.AddWithValue("@MaHocKy", hocKy.MaHocKy);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CapNhatHocKy: {ex.Message}");
                throw;
            }
        }

        // ✅ XÓA HỌC KỲ
        public bool XoaHocKy(int maHocKy)
        {
            string query = "DELETE FROM HocKy WHERE MaHocKy = @MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
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
                Console.WriteLine($"Lỗi XoaHocKy: {ex.Message}");
                throw;
            }
        }

        // ✅ TÍNH TRẠNG THÁI
        private string TinhTrangThai(DateTime ngayBD, DateTime ngayKT)
        {
            DateTime now = DateTime.Now.Date;
            DateTime batDau = ngayBD.Date;
            DateTime ketThuc = ngayKT.Date;

            if (now >= batDau && now <= ketThuc)
                return "Đang diễn ra";
            else if (now < batDau)
                return "Chưa bắt đầu";
            else
                return "Đã kết thúc";
        }
    }
}   