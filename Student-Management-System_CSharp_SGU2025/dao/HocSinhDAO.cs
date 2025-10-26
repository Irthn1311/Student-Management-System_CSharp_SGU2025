using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class HocSinhDAO
    {
        /// <summary>
        /// Lấy tất cả học sinh
        /// </summary>
        public List<HocSinhDTO> GetAllHocSinh()
        {
            List<HocSinhDTO> list = new List<HocSinhDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "SELECT MaHocSinh, HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai FROM HocSinh";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HocSinhDTO hs = new HocSinhDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : (DateTime?)null,
                                GioiTinh = reader["GioiTinh"].ToString(),
                                SDTHS = reader["SDTHS"].ToString(),
                                Email = reader["Email"].ToString(),
                                TrangThai = reader["TrangThai"].ToString()
                            };
                            list.Add(hs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách học sinh: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return list;
        }

        /// <summary>
        /// Lấy học sinh theo mã
        /// </summary>
        public HocSinhDTO GetHocSinhByMa(string maHocSinh)
        {
            HocSinhDTO hs = null;
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "SELECT MaHocSinh, HoTen, NgaySinh, GioiTinh, SDTHS, Email, TrangThai " +
                              "FROM HocSinh WHERE MaHocSinh = @MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hs = new HocSinhDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : (DateTime?)null,
                                GioiTinh = reader["GioiTinh"].ToString(),
                                SDTHS = reader["SDTHS"].ToString(),
                                Email = reader["Email"].ToString(),
                                TrangThai = reader["TrangThai"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin học sinh: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return hs;
        }
    }
}