using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class PhanLopDAO
    {
        /// <summary>
        /// Lấy danh sách học sinh theo lớp
        /// </summary>
        public List<HocSinhDTO> GetHocSinhTheoLop(int maLop)
        {
            List<HocSinhDTO> list = new List<HocSinhDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
                    SELECT DISTINCT hs.MaHocSinh, hs.HoTen, hs.NgaySinh, 
                           hs.GioiTinh, hs.SDTHS, hs.Email, hs.TrangThai
                    FROM HocSinh hs
                    INNER JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh
                    WHERE pl.MaLop = @MaLop AND hs.TrangThai = 'Đang học'
                    ORDER BY hs.MaHocSinh";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HocSinhDTO hs = new HocSinhDTO
                            {
                                MaHocSinh = reader["MaHocSinh"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                NgaySinh = reader["NgaySinh"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgaySinh"]) : (DateTime?)null,
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
                throw new Exception("Lỗi khi lấy danh sách học sinh theo lớp: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return list;
        }
    }
}