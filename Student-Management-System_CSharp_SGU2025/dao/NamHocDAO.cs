using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class NamHocDAO
    {
        public bool themNamHoc(NamHocDTO namHoc)
        {
            string query = "INSERT INTO NamHoc(MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc) VALUES(@MaNamHoc, @TenNamHoc, @NgayBatDau, @NgayKetThuc)";
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNamHoc", namHoc.MaNamHoc);
                        cmd.Parameters.AddWithValue("@TenNamHoc", namHoc.TenNamHoc);
                        cmd.Parameters.AddWithValue("@NgayBatDau", namHoc.NgayBD);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", namHoc.NgayKT);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi themNamHoc: {ex.Message}");
                throw;
            }
        }

        public List<NamHocDTO> DocDSNamHoc()
        {
            List<NamHocDTO> ds = new List<NamHocDTO>();
            string query = "SELECT MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc FROM NamHoc ORDER BY NgayBatDau DESC";

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
                                NamHocDTO nh = new NamHocDTO
                                {
                                    MaNamHoc = reader.GetString("MaNamHoc"),
                                    TenNamHoc = reader.GetString("TenNamHoc"),
                                    NgayBD = reader.GetDateTime("NgayBatDau"),
                                    NgayKT = reader.GetDateTime("NgayKetThuc")
                                };
                                ds.Add(nh);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi DocDSNamHoc: {ex.Message}");
                throw;
            }

            return ds;
        }

        public NamHocDTO LayNamHocTheoMa(string maNamHoc)
        {
            NamHocDTO namHoc = null;
            string query = "SELECT MaNamHoc, TenNamHoc, NgayBatDau, NgayKetThuc FROM NamHoc WHERE MaNamHoc=@MaNamHoc";

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
                            if (reader.Read())
                            {
                                namHoc = new NamHocDTO
                                {
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
                Console.WriteLine($"Lỗi LayNamHocTheoMa: {ex.Message}");
                throw;
            }

            return namHoc;
        }

        public bool updateNamHoc(NamHocDTO namHoc)
        {
            string query = "UPDATE NamHoc SET TenNamHoc=@TenNamHoc, NgayBatDau=@NgayBatDau, NgayKetThuc=@NgayKetThuc WHERE MaNamHoc=@MaNamHoc";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNamHoc", namHoc.TenNamHoc);
                        cmd.Parameters.AddWithValue("@NgayBatDau", namHoc.NgayBD);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", namHoc.NgayKT);
                        cmd.Parameters.AddWithValue("@MaNamHoc", namHoc.MaNamHoc);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi updateNamHoc: {ex.Message}");
                throw;
            }
        }

        public bool XoaNamHoc(string maNamHoc)
        {
            string query = "DELETE FROM NamHoc WHERE MaNamHoc = @MaNamHoc";
            
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                        
                        int result = cmd.ExecuteNonQuery();
                        
                        // Trả về true nếu có ít nhất 1 row bị xóa
                        return result > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Xử lý lỗi MySQL cụ thể
                Console.WriteLine($"MySQL Error [{ex.Number}]: {ex.Message}");
                throw new Exception($"Lỗi database: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi XoaNamHoc: {ex.Message}");
                throw;
            }
        }
    }
}