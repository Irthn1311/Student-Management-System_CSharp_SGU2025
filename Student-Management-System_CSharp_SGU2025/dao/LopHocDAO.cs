//using System;
//using System.Collections.Generic;
//using MySql.Data.MySqlClient;
//using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
//using Student_Management_System_CSharp_SGU2025.DTO;

//namespace Student_Management_System_CSharp_SGU2025.DAO
//{
//    internal class LopDAO
//    {
//        // 🧠 Hàm đọc toàn bộ danh sách lớp
//        public List<LopDTO> LayDSLop()
//        {
//            List<LopDTO> ds = new List<LopDTO>();
//            string query = "SELECT Ma_Lop, Ten_Lop, Ma_Giao_Vien, Ma_Khoa FROM Lop";

//            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
//            {
//                conn.Open();
//                using (MySqlCommand cmd = new MySqlCommand(query, conn))
//                {
//                    using (MySqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {
//                            LopDTO lop = new LopDTO
//                            {
//                                MaLop = reader.GetInt32("Ma_Lop"),
//                                TenLop = reader.GetString("Ten_Lop"),
//                                MaGiaoVien = reader.IsDBNull(reader.GetOrdinal("Ma_Giao_Vien")) ? 0 : reader.GetInt32("Ma_Giao_Vien"),
//                                MaKhoa = reader.IsDBNull(reader.GetOrdinal("Ma_Khoa")) ? 0 : reader.GetInt32("Ma_Khoa")
//                            };
//                            ds.Add(lop);
//                        }
//                    }
//                }
//            }
//            return ds;
//        }

//        // 🔍 Lấy lớp theo mã
//        public LopDTO LayLopTheoMa(int maLop)
//        {
//            LopDTO lop = null;
//            string query = "SELECT Ma_Lop, Ten_Lop, Ma_Giao_Vien, Ma_Khoa FROM Lop WHERE Ma_Lop = @MaLop";

//            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
//            {
//                conn.Open();
//                using (MySqlCommand cmd = new MySqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@MaLop", maLop);
//                    using (MySqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            lop = new LopDTO
//                            {
//                                MaLop = reader.GetInt32("Ma_Lop"),
//                                TenLop = reader.GetString("Ten_Lop"),
//                                MaGiaoVien = reader.IsDBNull(reader.GetOrdinal("Ma_Giao_Vien")) ? 0 : reader.GetInt32("Ma_Giao_Vien"),
//                                MaKhoa = reader.IsDBNull(reader.GetOrdinal("Ma_Khoa")) ? 0 : reader.GetInt32("Ma_Khoa")
//                            };
//                        }
//                    }
//                }
//            }
//            return lop;
//        }

//        // ➕ Thêm lớp
//        public bool ThemLop(LopDTO lop)
//        {
//            string query = "INSERT INTO Lop (Ten_Lop, Ma_Giao_Vien, Ma_Khoa) VALUES (@TenLop, @MaGiaoVien, @MaKhoa)";
//            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
//            {
//                conn.Open();
//                using (MySqlCommand cmd = new MySqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@TenLop", lop.TenLop);
//                    cmd.Parameters.AddWithValue("@MaGiaoVien", lop.MaGiaoVien);
//                    cmd.Parameters.AddWithValue("@MaKhoa", lop.MaKhoa);
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//        }

//        // ✏️ Sửa lớp
//        public bool SuaLop(LopDTO lop)
//        {
//            string query = "UPDATE Lop SET Ten_Lop=@TenLop, Ma_Giao_Vien=@MaGiaoVien, Ma_Khoa=@MaKhoa WHERE Ma_Lop=@MaLop";
//            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
//            {
//                conn.Open();
//                using (MySqlCommand cmd = new MySqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@TenLop", lop.TenLop);
//                    cmd.Parameters.AddWithValue("@MaGiaoVien", lop.MaGiaoVien);
//                    cmd.Parameters.AddWithValue("@MaKhoa", lop.MaKhoa);
//                    cmd.Parameters.AddWithValue("@MaLop", lop.MaLop);
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//        }

//        // ❌ Xóa lớp
//        public bool XoaLop(int maLop)
//        {
//            string query = "DELETE FROM Lop WHERE Ma_Lop=@MaLop";
//            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
//            {
//                conn.Open();
//                using (MySqlCommand cmd = new MySqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@MaLop", maLop);
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//        }
//    }
//}
