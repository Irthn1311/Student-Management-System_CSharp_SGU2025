using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class MonHoc_NamHoc_KhoiDAO
    {
        /// <summary>
        /// Thêm môn học cho năm học và khối cụ thể
        /// </summary>
        public bool ThemMonHocChoNamHocKhoi(int maMonHoc, string maNamHoc, int maKhoi)
        {
            string query = @"INSERT INTO MonHoc_NamHoc_Khoi (MaMonHoc, MaNamHoc, MaKhoi) 
                           VALUES (@MaMonHoc, @MaNamHoc, @MaKhoi)";
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                    cmd.Parameters.AddWithValue("@MaKhoi", maKhoi);
                    
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        /// <summary>
        /// Xóa môn học khỏi năm học và khối
        /// </summary>
        public bool XoaMonHocKhoiNamHoc(int maMonHoc, string maNamHoc, int maKhoi)
        {
            string query = @"DELETE FROM MonHoc_NamHoc_Khoi 
                           WHERE MaMonHoc = @MaMonHoc 
                           AND MaNamHoc = @MaNamHoc 
                           AND MaKhoi = @MaKhoi";
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                    cmd.Parameters.AddWithValue("@MaKhoi", maKhoi);
                    
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        /// <summary>
        /// Lấy danh sách môn học theo năm học và khối
        /// </summary>
        public List<MonHocDTO> LayDanhSachMonHocTheoNamHocKhoi(string maNamHoc, int maKhoi)
        {
            List<MonHocDTO> ds = new List<MonHocDTO>();
            string query = @"SELECT mh.MaMonHoc, mh.TenMonHoc, mh.SoTiet, mh.GhiChu, mh.NamHocBatDau
                           FROM MonHoc mh
                           INNER JOIN MonHoc_NamHoc_Khoi mhnk ON mh.MaMonHoc = mhnk.MaMonHoc
                           WHERE mhnk.MaNamHoc = @MaNamHoc AND mhnk.MaKhoi = @MaKhoi
                           ORDER BY mh.TenMonHoc";
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                    cmd.Parameters.AddWithValue("@MaKhoi", maKhoi);
                    
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonHocDTO mh = new MonHocDTO
                            {
                                maMon = reader.GetInt32("MaMonHoc"),
                                tenMon = reader.GetString("TenMonHoc"),
                                soTiet = reader.GetInt32("SoTiet"),
                                ghiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString("GhiChu"),
                                namHocBatDau = reader.IsDBNull(reader.GetOrdinal("NamHocBatDau")) ? null : reader.GetString("NamHocBatDau")
                            };
                            ds.Add(mh);
                        }
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// Lấy danh sách môn học theo năm học (tất cả khối)
        /// </summary>
        public List<MonHocDTO> LayDanhSachMonHocTheoNamHoc(string maNamHoc)
        {
            List<MonHocDTO> ds = new List<MonHocDTO>();
            string query = @"SELECT DISTINCT mh.MaMonHoc, mh.TenMonHoc, mh.SoTiet, mh.GhiChu, mh.NamHocBatDau
                           FROM MonHoc mh
                           INNER JOIN MonHoc_NamHoc_Khoi mhnk ON mh.MaMonHoc = mhnk.MaMonHoc
                           WHERE mhnk.MaNamHoc = @MaNamHoc
                           ORDER BY mh.TenMonHoc";
            
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
                            MonHocDTO mh = new MonHocDTO
                            {
                                maMon = reader.GetInt32("MaMonHoc"),
                                tenMon = reader.GetString("TenMonHoc"),
                                soTiet = reader.GetInt32("SoTiet"),
                                ghiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString("GhiChu"),
                                namHocBatDau = reader.IsDBNull(reader.GetOrdinal("NamHocBatDau")) ? null : reader.GetString("NamHocBatDau")
                            };
                            ds.Add(mh);
                        }
                    }
                }
            }
            return ds;
        }

        /// <summary>
        /// Kiểm tra môn học có tồn tại trong năm học và khối không
        /// </summary>
        public bool KiemTraMonHocTonTaiTrongNamHocKhoi(int maMonHoc, string maNamHoc, int maKhoi)
        {
            string query = @"SELECT COUNT(*) FROM MonHoc_NamHoc_Khoi 
                           WHERE MaMonHoc = @MaMonHoc 
                           AND MaNamHoc = @MaNamHoc 
                           AND MaKhoi = @MaKhoi";
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    cmd.Parameters.AddWithValue("@MaNamHoc", maNamHoc);
                    cmd.Parameters.AddWithValue("@MaKhoi", maKhoi);
                    
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// Thêm môn học cho tất cả khối trong một năm học
        /// </summary>
        public bool ThemMonHocChoTatCaKhoi(int maMonHoc, string maNamHoc)
        {
            List<int> danhSachKhoi = new List<int> { 10, 11, 12 };
            bool allSuccess = true;
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (int maKhoi in danhSachKhoi)
                        {
                            // Kiểm tra xem đã tồn tại chưa
                            if (!KiemTraMonHocTonTaiTrongNamHocKhoi(maMonHoc, maNamHoc, maKhoi))
                            {
                                if (!ThemMonHocChoNamHocKhoi(maMonHoc, maNamHoc, maKhoi))
                                {
                                    allSuccess = false;
                                    break;
                                }
                            }
                        }
                        
                        if (allSuccess)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        allSuccess = false;
                    }
                }
            }
            
            return allSuccess;
        }

        /// <summary>
        /// Lấy danh sách tất cả các bản ghi MonHoc_NamHoc_Khoi
        /// </summary>
        public List<MonHoc_NamHoc_KhoiDTO> LayTatCa()
        {
            List<MonHoc_NamHoc_KhoiDTO> ds = new List<MonHoc_NamHoc_KhoiDTO>();
            string query = @"SELECT MaMonHoc, MaNamHoc, MaKhoi FROM MonHoc_NamHoc_Khoi";
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonHoc_NamHoc_KhoiDTO dto = new MonHoc_NamHoc_KhoiDTO
                            {
                                maMonHoc = reader.GetInt32("MaMonHoc"),
                                maNamHoc = reader.GetString("MaNamHoc"),
                                maKhoi = reader.GetInt32("MaKhoi")
                            };
                            ds.Add(dto);
                        }
                    }
                }
            }
            return ds;
        }
    }
}

