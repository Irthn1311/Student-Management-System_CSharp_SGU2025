using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class LopDAO
    {
        //  Thêm lớp học
        public bool ThemLop(LopDTO lop)
        {
            string query = "INSERT INTO LopHoc (TenLop, MaKhoi,SiSo, MaGiaoVienChuNhiem) VALUES (@Ten_Lop,@Ma_Khoi,@siso, @GVCN)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten_Lop", lop.tenLop);
                    cmd.Parameters.AddWithValue("@Ma_Khoi",lop.maKhoi);
                    cmd.Parameters.AddWithValue("@siso",lop.siSo);
                    cmd.Parameters.AddWithValue("@GVCN", lop.maGVCN);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        // Đọc danh sách lớp học
        public List<LopDTO> DocDSLop()
        {
            List<LopDTO> ds = new List<LopDTO>();
            string query = "SELECT MaLop, TenLop, MaKhoi,SiSo, MaGiaoVienChuNhiem FROM LopHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LopDTO lop = new LopDTO();
                            lop.maLop = reader.GetInt32("MaLop");
                            lop.tenLop = reader.GetString("TenLop");
                            lop.maKhoi = reader.GetInt32("MaKhoi");
                            lop.siSo=reader.GetInt32("SiSo");
                            lop.maGVCN = reader.GetString("MaGiaoVienChuNhiem");
                            ds.Add(lop);
                        }
                    }
                }
            }
            return ds;
        }

        //  Lấy lớp theo ID
        public LopDTO LayLopTheoId(int maLop)
        {
            LopDTO lop = null;
            string query = "SELECT MaLop, TenLop, MaKhoi,SiSo, MaGiaoVienChuNhiem FROM LopHoc WHERE MaLop = @Ma_Lop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma_Lop", maLop);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lop = new LopDTO();
                            lop.maLop = reader.GetInt32("MaLop");
                            lop.tenLop = reader.GetString("TenLop");
                            lop.maKhoi = reader.GetInt32("MaKhoi");
                            lop.siSo = reader.GetInt32("SiSo");
                            lop.maGVCN = reader.GetString("MaGiaoVienChuNhiem");
                        }
                    }
                }
            }
            return lop;
        }
        
        //  Lấy lớp theo tên
        public LopDTO LayLopTheoTen(string tenLop)
        {
            LopDTO lop = null;
            string query = "SELECT MaLop, TenLop, MaKhoi,SiSo, MaGiaoVienChuNhiem FROM LopHoc WHERE TenLop = @Ten_Lop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten_Lop",tenLop);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lop = new LopDTO();
                            lop.maLop = reader.GetInt32("MaLop");
                            lop.tenLop = reader.GetString("TenLop");
                            lop.maKhoi = reader.GetInt32("MaKhoi");
                            lop.siSo = reader.GetInt32("SiSo");
                            lop.maGVCN = reader.GetString("MaGiaoVienChuNhiem");
                        }
                    }
                }
            }
            return lop;
        }
        //  Cập nhật lớp học
        public bool CapNhatLop(LopDTO lop)
        {
            string query = "UPDATE LopHoc SET TenLop = @Ten_Lop, MaKhoi = @Ma_Khoi,SiSo=@SiSo MaGiaoVienChuNhiem = @GVCN WHERE MaLop = @Ma_Lop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@Ten_Lop", lop.tenLop);
                    cmd.Parameters.AddWithValue("@Ma_Khoi", lop.maKhoi);
                    cmd.Parameters.AddWithValue("@SiSo", lop.siSo);
                    cmd.Parameters.AddWithValue("@GVCN", lop.maGVCN);
                    cmd.Parameters.AddWithValue("@Ma_Lop", lop.maLop);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        //  Xóa lớp học
        public bool XoaLop(int maLop)
        {
            string query = "DELETE FROM LopHoc WHERE MaLop = @Ma_Lop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma_Lop", maLop);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
        public List<LopDTO> GetDanhSachLopCoHocSinh()
        {
            List<LopDTO> list = new List<LopDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
                    SELECT DISTINCT l.MaLop, l.TenLop, l.MaKhoi,l.SiSo, l.MaGiaoVienChuNhiem
                    FROM LopHoc l
                    INNER JOIN PhanLop pl ON l.MaLop = pl.MaLop
                    ORDER BY l.TenLop";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LopDTO lop = new LopDTO
                            {
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                TenLop = reader["TenLop"].ToString(),
                                MaKhoi = Convert.ToInt32(reader["MaKhoi"]),
                                SiSo = Convert.ToInt32(reader["SiSo"]),
                                MaGVCN = reader["MaGiaoVienChuNhiem"]?.ToString()
                            };
                            list.Add(lop);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách lớp học: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return list;
        }

        public List<LopDTO> GetByHocKy(int hocKyId)
        {
            // Hiện chưa ràng buộc lớp theo học kỳ → trả toàn bộ danh sách lớp.
            return DocDSLop();
        }
        
        /// <summary>
        /// Lấy tên lớp theo mã lớp
        /// </summary>
        public string GetTenLopByMaLop(int maLop)
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "SELECT TenLop FROM LopHoc WHERE MaLop = @MaLop";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);

                    object result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tên lớp: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Lấy mã lớp tiếp theo
        /// </summary>
        public int LayMaLopTiepTheo()
        {
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "SELECT COALESCE(MAX(MaLop), 0) + 1 FROM LopHoc";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy mã lớp tiếp theo: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
        }

        /// <summary>
        /// Lấy danh sách mã giáo viên chủ nhiệm đang được phân công
        /// </summary>
        public List<string> LayDanhSachMaGVCNDangPhanCong()
        {
            List<string> ds = new List<string>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = "SELECT DISTINCT MaGiaoVienChuNhiem FROM LopHoc WHERE MaGiaoVienChuNhiem IS NOT NULL";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maGVCN = reader["MaGiaoVienChuNhiem"]?.ToString();
                            if (!string.IsNullOrEmpty(maGVCN))
                            {
                                ds.Add(maGVCN);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách mã GVCN: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return ds;
        }

        /// <summary>
        /// Lấy danh sách lớp đã được phân lớp trong học kỳ cụ thể
        /// </summary>
        public List<LopDTO> GetDanhSachLopTheoHocKy(int maHocKy)
        {
            List<LopDTO> list = new List<LopDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
            SELECT DISTINCT l.MaLop, l.TenLop, l.MaKhoi, l.SiSo, l.MaGiaoVienChuNhiem
            FROM LopHoc l
            INNER JOIN PhanLop pl ON l.MaLop = pl.MaLop
            WHERE pl.MaHocKy = @MaHocKy
            ORDER BY l.TenLop";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LopDTO lop = new LopDTO
                            {
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                TenLop = reader["TenLop"].ToString(),
                                MaKhoi = Convert.ToInt32(reader["MaKhoi"]),
                                SiSo = Convert.ToInt32(reader["SiSo"]),
                                MaGVCN = reader["MaGiaoVienChuNhiem"]?.ToString()
                            };
                            list.Add(lop);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách lớp theo học kỳ: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }

            return list;
        }

    }
}

