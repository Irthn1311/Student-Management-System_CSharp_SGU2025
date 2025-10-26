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
            string query = "INSERT INTO LopHoc (Ten_Lop, Ma_Khoi,Si_So, Giao_Vien_Chu_Nhiem) VALUES (@Ten_Lop,@Ma_Khoi, @Si_So, @GVCN)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ten_Lop", lop.tenLop);
                    cmd.Parameters.AddWithValue("@Ma_Khoi",lop.maKhoi); 
                    cmd.Parameters.AddWithValue("@Si_So", lop.siSo);
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
            string query = "SELECT Ma_Lop, Ten_Lop,Ma_Khoi, Si_So, Giao_Vien_Chu_Nhiem FROM LopHoc";
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
                            lop.maLop = reader.GetInt32("Ma_Lop");
                            lop.tenLop = reader.GetString("Ten_Lop");
                            lop.maKhoi = reader.GetInt32("Ma_Khoi");
                            lop.siSo = reader.GetInt32("Si_So");
                            lop.maGVCN = reader.GetString("Giao_Vien_Chu_Nhiem");
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
            string query = "SELECT Ma_Lop, Ten_Lop,Ma_Khoi, Si_So, Giao_Vien_Chu_Nhiem FROM LopHoc WHERE Ma_Lop = @Ma_Lop";
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
                            lop.maLop = reader.GetInt32("Ma_Lop");
                            lop.tenLop = reader.GetString("Ten_Lop");
                            lop.maKhoi = reader.GetInt32("Ma_Khoi");
                            lop.siSo = reader.GetInt32("Si_So");
                            lop.maGVCN = reader.GetString("Giao_Vien_Chu_Nhiem");
                        }
                    }
                }
            }
            return lop;
        }
        
        //  Lấy lớp theo ID
        public LopDTO LayLopTheoTen(string tenLop)
        {
            LopDTO lop = null;
            string query = "SELECT Ma_Lop, Ten_Lop,Ma_Khoi, Si_So, Giao_Vien_Chu_Nhiem FROM LopHoc WHERE Ten_Lop = @Ten_Lop";
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
                            lop.maLop = reader.GetInt32("Ma_Lop");
                            lop.tenLop = reader.GetString("Ten_Lop");
                            lop.maKhoi = reader.GetInt32("Ma_Khoi");
                            lop.siSo = reader.GetInt32("Si_So");
                            lop.maGVCN = reader.GetString("Giao_Vien_Chu_Nhiem");
                        }
                    }
                }
            }
            return lop;
        }
        //  Cập nhật lớp học
        public bool CapNhatLop(LopDTO lop)
        {
            string query = "UPDATE LopHoc SET Ten_Lop = @Ten_Lop, Ma_Khoi=@Ma_Khoi Si_So = @Si_So, Giao_Vien_Chu_Nhiem = @GVCN WHERE Ma_Lop = @Ma_Lop";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@Ten_Lop", lop.tenLop);
                    cmd.Parameters.AddWithValue("@Ma_khoi", lop.maKhoi);
                    cmd.Parameters.AddWithValue("@Si_So", lop.siSo);
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
            string query = "DELETE FROM LopHoc WHERE Ma_Lop = @Ma_Lop";
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
        public List<LopHocDTO> GetDanhSachLopCoHocSinh()
        {
            List<LopHocDTO> list = new List<LopHocDTO>();
            MySqlConnection conn = null;

            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();

                string query = @"
                    SELECT DISTINCT l.MaLop, l.TenLop, l.MaKhoi, l.MaGiaoVienChuNhiem
                    FROM LopHoc l
                    INNER JOIN PhanLop pl ON l.MaLop = pl.MaLop
                    ORDER BY l.TenLop";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LopHocDTO lop = new LopHocDTO
                            {
                                MaLop = Convert.ToInt32(reader["MaLop"]),
                                TenLop = reader["TenLop"].ToString(),
                                MaKhoi = Convert.ToInt32(reader["MaKhoi"]),
                                MaGiaoVienChuNhiem = reader["MaGiaoVienChuNhiem"]?.ToString()
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

    }
}

