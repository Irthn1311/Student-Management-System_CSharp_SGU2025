using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class HocKyDAO
    {
        /// <summary>
        /// Lấy tất cả học kỳ
        /// </summary>
        public List<HocKyDTO> GetAllHocKy()
        {
            List<HocKyDTO> list = new List<HocKyDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
                    SELECT MaHocKy, TenHocKy, MaNamHoc, TrangThai, NgayBD, NgayKT
                    FROM HocKy
                    ORDER BY MaHocKy DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HocKyDTO hk = new HocKyDTO
                            {
                                MaHocKy = Convert.ToInt32(reader["MaHocKy"]),
                                TenHocKy = reader["TenHocKy"].ToString(),
                                MaNamHoc = reader["MaNamHoc"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayBD = reader["NgayBD"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgayBD"]) : (DateTime?)null,
                                NgayKT = reader["NgayKT"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["NgayKT"]) : (DateTime?)null
                            };
                            list.Add(hk);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách học kỳ: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return list;
        }
    }
}