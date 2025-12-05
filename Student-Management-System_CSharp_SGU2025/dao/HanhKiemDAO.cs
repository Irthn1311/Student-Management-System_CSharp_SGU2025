// HanhKiemDAO.cs
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class HanhKiemDAO
    {
        /// <summary>
        /// Lấy hoặc tính hạnh kiểm cho một học sinh trong học kỳ
        /// </summary>
        public HanhKiemDTO LayHanhKiem(int maHocSinh, int maHocKy)
        {
            string sql = "SELECT MaHocSinh, MaHocKy, XepLoai, NhanXet FROM HanhKiem WHERE MaHocSinh = @maHS AND MaHocKy = @maHK";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new HanhKiemDTO
                                {
                                    MaHocSinh = reader.GetInt32("MaHocSinh"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    XepLoai = reader.IsDBNull(reader.GetOrdinal("XepLoai")) ? null : reader.GetString("XepLoai"),
                                    NhanXet = reader.IsDBNull(reader.GetOrdinal("NhanXet")) ? null : reader.GetString("NhanXet")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi lấy hạnh kiểm: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return null;
        }

        /// <summary>
        /// Lưu hoặc cập nhật hạnh kiểm
        /// </summary>
        public bool LuuHanhKiem(HanhKiemDTO hk)
        {
            string sql = @"INSERT INTO HanhKiem (MaHocSinh, MaHocKy, XepLoai, NhanXet) 
                              VALUES (@maHS, @maHK, @xepLoai, @nhanXet)
                              ON DUPLICATE KEY UPDATE 
                              XepLoai = @xepLoai, 
                              NhanXet = @nhanXet";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", hk.MaHocSinh);
                        cmd.Parameters.AddWithValue("@maHK", hk.MaHocKy);
                        cmd.Parameters.AddWithValue("@xepLoai", (object)hk.XepLoai ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@nhanXet", (object)hk.NhanXet ?? DBNull.Value);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi lưu hạnh kiểm: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách hạnh kiểm theo học kỳ và lớp (Trả về BindingList)
        /// Hiển thị TẤT CẢ học sinh trong lớp, kể cả chưa có hạnh kiểm
        /// </summary>
        public BindingList<HanhKiemDTO> LayDanhSachHanhKiemBindingList(int maHocKy, int? maLop = null)
        {
            BindingList<HanhKiemDTO> ds = new BindingList<HanhKiemDTO>();

            string sql = @"SELECT DISTINCT hs.MaHocSinh, @maHK as MaHocKy, 
                      hk.XepLoai, 
                      hk.NhanXet
                      FROM HocSinh hs
                      INNER JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh AND pl.MaHocKy = @maHK
                      LEFT JOIN HanhKiem hk ON hs.MaHocSinh = hk.MaHocSinh AND hk.MaHocKy = @maHK
                      WHERE (hs.TrangThai = 'Đang học' OR hs.TrangThai = 'Đang học(CT)')";

            if (maLop.HasValue && maLop.Value > 0)
            {
                sql += " AND pl.MaLop = @maLop";
            }

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);
                        if (maLop.HasValue && maLop.Value > 0)
                        {
                            cmd.Parameters.AddWithValue("@maLop", maLop.Value);
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ds.Add(new HanhKiemDTO
                                {
                                    MaHocSinh = reader.GetInt32("MaHocSinh"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    XepLoai = reader.IsDBNull(reader.GetOrdinal("XepLoai")) || string.IsNullOrEmpty(reader["XepLoai"]?.ToString()) 
                                        ? null : reader.GetString("XepLoai"),
                                    NhanXet = reader.IsDBNull(reader.GetOrdinal("NhanXet")) ? null : reader.GetString("NhanXet")
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi lấy danh sách hạnh kiểm: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return ds;
        }

        /// <summary>
        /// Lấy hạnh kiểm theo khóa (MaHocSinh, MaHocKy) - Alias cho LayHanhKiem
        /// </summary>
        public HanhKiemDTO LayHanhKiemTheoKey(int maHocSinh, int maHocKy)
        {
            return LayHanhKiem(maHocSinh, maHocKy);
        }

        /// <summary>
        /// Lấy tất cả hạnh kiểm trong hệ thống
        /// </summary>
        public List<HanhKiemDTO> GetAllHanhKiem()
        {
            List<HanhKiemDTO> ds = new List<HanhKiemDTO>();
            string sql = "SELECT MaHocSinh, MaHocKy, XepLoai, NhanXet FROM HanhKiem";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ds.Add(new HanhKiemDTO
                                {
                                    MaHocSinh = reader.GetInt32("MaHocSinh"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    XepLoai = reader.IsDBNull(reader.GetOrdinal("XepLoai")) ? "" : reader.GetString("XepLoai"),
                                    NhanXet = reader.IsDBNull(reader.GetOrdinal("NhanXet")) ? "" : reader.GetString("NhanXet")
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi lấy tất cả hạnh kiểm: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return ds;
        }

    }
}