using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class XepLoaiDAO
    {
        /// <summary>
        /// Lấy xếp loại của học sinh trong một học kỳ cụ thể
        /// </summary>
        public XepLoaiDTO GetXepLoaiByHocSinhVaHocKy(int maHocSinh, int maHocKy)
        {
            XepLoaiDTO xepLoai = null;
            string query = @"SELECT MaHocSinh, MaHocKy, HocLuc, GhiChu 
                           FROM XepLoai 
                           WHERE MaHocSinh = @MaHocSinh AND MaHocKy = @MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                xepLoai = new XepLoaiDTO
                                {
                                    MaHocSinh = reader.GetInt32("MaHocSinh"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    HocLuc = reader.IsDBNull(reader.GetOrdinal("HocLuc")) ? "" : reader.GetString("HocLuc"),
                                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString("GhiChu")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetXepLoaiByHocSinhVaHocKy: " + ex.Message);
                throw;
            }

            return xepLoai;
        }

        /// <summary>
        /// Lấy tất cả xếp loại của một học sinh (tất cả các học kỳ)
        /// </summary>
        public List<XepLoaiDTO> GetAllXepLoaiByHocSinh(int maHocSinh)
        {
            List<XepLoaiDTO> danhSach = new List<XepLoaiDTO>();
            string query = @"SELECT MaHocSinh, MaHocKy, HocLuc, GhiChu 
                           FROM XepLoai 
                           WHERE MaHocSinh = @MaHocSinh 
                           ORDER BY MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                XepLoaiDTO xepLoai = new XepLoaiDTO
                                {
                                    MaHocSinh = reader.GetInt32("MaHocSinh"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    HocLuc = reader.IsDBNull(reader.GetOrdinal("HocLuc")) ? "" : reader.GetString("HocLuc"),
                                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString("GhiChu")
                                };
                                danhSach.Add(xepLoai);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetAllXepLoaiByHocSinh: " + ex.Message);
                throw;
            }

            return danhSach;
        }

        /// <summary>
        /// Lấy tất cả xếp loại trong một học kỳ cụ thể
        /// </summary>
        public List<XepLoaiDTO> GetAllXepLoaiByHocKy(int maHocKy)
        {
            List<XepLoaiDTO> danhSach = new List<XepLoaiDTO>();
            string query = @"SELECT MaHocSinh, MaHocKy, HocLuc, GhiChu 
                           FROM XepLoai 
                           WHERE MaHocKy = @MaHocKy 
                           ORDER BY MaHocSinh";

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
                            while (reader.Read())
                            {
                                XepLoaiDTO xepLoai = new XepLoaiDTO
                                {
                                    MaHocSinh = reader.GetInt32("MaHocSinh"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    HocLuc = reader.IsDBNull(reader.GetOrdinal("HocLuc")) ? "" : reader.GetString("HocLuc"),
                                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString("GhiChu")
                                };
                                danhSach.Add(xepLoai);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetAllXepLoaiByHocKy: " + ex.Message);
                throw;
            }

            return danhSach;
        }

        /// <summary>
        /// Thêm xếp loại mới cho học sinh
        /// </summary>
        public bool InsertXepLoai(XepLoaiDTO xepLoai)
        {
            string query = @"INSERT INTO XepLoai (MaHocSinh, MaHocKy, HocLuc, GhiChu) 
                           VALUES (@MaHocSinh, @MaHocKy, @HocLuc, @GhiChu)";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocSinh", xepLoai.MaHocSinh);
                        cmd.Parameters.AddWithValue("@MaHocKy", xepLoai.MaHocKy);
                        cmd.Parameters.AddWithValue("@HocLuc", xepLoai.HocLuc);
                        cmd.Parameters.AddWithValue("@GhiChu", xepLoai.GhiChu ?? "");

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi InsertXepLoai: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cập nhật xếp loại của học sinh
        /// </summary>
        public bool UpdateXepLoai(XepLoaiDTO xepLoai)
        {
            string query = @"UPDATE XepLoai 
                           SET HocLuc = @HocLuc, GhiChu = @GhiChu 
                           WHERE MaHocSinh = @MaHocSinh AND MaHocKy = @MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocSinh", xepLoai.MaHocSinh);
                        cmd.Parameters.AddWithValue("@MaHocKy", xepLoai.MaHocKy);
                        cmd.Parameters.AddWithValue("@HocLuc", xepLoai.HocLuc);
                        cmd.Parameters.AddWithValue("@GhiChu", xepLoai.GhiChu ?? "");

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi UpdateXepLoai: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Xóa xếp loại của học sinh trong học kỳ
        /// </summary>
        public bool DeleteXepLoai(int maHocSinh, int maHocKy)
        {
            string query = @"DELETE FROM XepLoai 
                           WHERE MaHocSinh = @MaHocSinh AND MaHocKy = @MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DeleteXepLoai: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem học sinh đã có xếp loại trong học kỳ chưa
        /// </summary>
        public bool KiemTraTonTai(int maHocSinh, int maHocKy)
        {
            string query = @"SELECT COUNT(*) FROM XepLoai 
                           WHERE MaHocSinh = @MaHocSinh AND MaHocKy = @MaHocKy";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHocSinh", maHocSinh);
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi KiemTraTonTai: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Lấy thống kê xếp loại theo học kỳ
        /// </summary>
        public Dictionary<string, int> GetThongKeXepLoaiByHocKy(int maHocKy)
        {
            Dictionary<string, int> thongKe = new Dictionary<string, int>
            {
                { "Giỏi", 0 },
                { "Khá", 0 },
                { "Trung bình", 0 },
                { "Yếu", 0 },
                { "Kém", 0 }
            };

            string query = @"SELECT HocLuc, COUNT(*) as SoLuong 
                           FROM XepLoai 
                           WHERE MaHocKy = @MaHocKy 
                           GROUP BY HocLuc";

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
                            while (reader.Read())
                            {
                                string hocLuc = reader.GetString("HocLuc");
                                int soLuong = reader.GetInt32("SoLuong");
                                if (thongKe.ContainsKey(hocLuc))
                                {
                                    thongKe[hocLuc] = soLuong;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi GetThongKeXepLoaiByHocKy: " + ex.Message);
            }

            return thongKe;
        }

        /// <summary>
        /// Lấy tất cả xếp loại trong hệ thống
        /// Dùng cho logic phân lớp tự động
        /// </summary>
        public List<XepLoaiDTO> GetAllXepLoai()
        {
            List<XepLoaiDTO> danhSachXepLoai = new List<XepLoaiDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                string query = "SELECT MaHocSinh, MaHocKy, HocLuc, GhiChu FROM XepLoai";
                
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        XepLoaiDTO xl = new XepLoaiDTO
                        {
                            MaHocSinh = reader.GetInt32("MaHocSinh"),
                            MaHocKy = reader.GetInt32("MaHocKy"),
                            HocLuc = reader.IsDBNull(reader.GetOrdinal("HocLuc")) ? "" : reader.GetString("HocLuc"),
                            GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString("GhiChu")
                        };
                        danhSachXepLoai.Add(xl);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tất cả xếp loại: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return danhSachXepLoai;
        }
    }
}
