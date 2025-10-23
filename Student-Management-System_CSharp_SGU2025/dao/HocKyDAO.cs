using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class HocKyDAO
    {
        public bool ThemHocKy(HocKyDTO hocKy)
        {
            string query = "INSERT INTO HocKy(TenHocKy, MaNamHoc, NgayBD, NgayKT, TrangThai) VALUES(@TenHocKy, @MaNamHoc, @NgayBD, @NgayKT, @TrangThai)";
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenHocKy", hocKy.TenHocKy);
                        cmd.Parameters.AddWithValue("@MaNamHoc", hocKy.MaNamHoc);
                        cmd.Parameters.AddWithValue("@NgayBD", hocKy.NgayBD);
                        cmd.Parameters.AddWithValue("@NgayKT", hocKy.NgayKT);
                        cmd.Parameters.AddWithValue("@TrangThai", hocKy.TrangThai);
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

        public List<HocKyDTO> DocDSHocKy()
        {
            List<HocKyDTO> ds = new List<HocKyDTO>();
            string query = @"SELECT hk.MaHocKy, hk.TenHocKy, hk.MaNamHoc, hk.NgayBD, hk.NgayKT, hk.TrangThai 
                           FROM HocKy hk 
                           INNER JOIN NamHoc nh ON hk.MaNamHoc = nh.MaNamHoc 
                           ORDER BY nh.NgayBatDau DESC, hk.NgayBD DESC";

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
                                    NgayBD = reader.GetDateTime("NgayBD"),
                                    NgayKT = reader.GetDateTime("NgayKT"),
                                    TrangThai = reader.GetString("TrangThai")
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

        public HocKyDTO LayHocKyTheoMa(int maHocKy)
        {
            HocKyDTO hocKy = null;
            string query = "SELECT MaHocKy, TenHocKy, MaNamHoc, NgayBD, NgayKT, TrangThai FROM HocKy WHERE MaHocKy=@MaHocKy";

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
                                    NgayBD = reader.GetDateTime("NgayBD"),
                                    NgayKT = reader.GetDateTime("NgayKT"),
                                    TrangThai = reader.GetString("TrangThai")
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

        public bool CapNhatHocKy(HocKyDTO hocKy)
        {
            string query = "UPDATE HocKy SET TenHocKy=@TenHocKy, MaNamHoc=@MaNamHoc, NgayBD=@NgayBD, NgayKT=@NgayKT, TrangThai=@TrangThai WHERE MaHocKy=@MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenHocKy", hocKy.TenHocKy);
                        cmd.Parameters.AddWithValue("@MaNamHoc", hocKy.MaNamHoc);
                        cmd.Parameters.AddWithValue("@NgayBD", hocKy.NgayBD);
                        cmd.Parameters.AddWithValue("@NgayKT", hocKy.NgayKT);
                        cmd.Parameters.AddWithValue("@TrangThai", hocKy.TrangThai);
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

        public List<HocKyDTO> LayDanhSachHocKyTheoNamHoc(string maNamHoc)
        {
            List<HocKyDTO> ds = new List<HocKyDTO>();
            string query = "SELECT MaHocKy, TenHocKy, MaNamHoc, NgayBD, NgayKT, TrangThai FROM HocKy WHERE MaNamHoc=@MaNamHoc ORDER BY NgayBD";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HocKyDTO hk = new HocKyDTO
                                {
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    TenHocKy = reader.GetString("TenHocKy"),
                                    MaNamHoc = reader.GetString("MaNamHoc"),
                                    NgayBD = reader.GetDateTime("NgayBD"),
                                    NgayKT = reader.GetDateTime("NgayKT"),
                                    TrangThai = reader.GetString("TrangThai")
                                };
                                ds.Add(hk);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayDanhSachHocKyTheoNamHoc: {ex.Message}");
                throw;
            }

            return ds;
        }
    }
}