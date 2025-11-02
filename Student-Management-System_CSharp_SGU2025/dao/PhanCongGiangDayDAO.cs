using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class PhanCongGiangDayDAO
    {
        public List<PhanCongGiangDayDTO> GetByHocKy(int hocKyId)
        {
            return LayPhanCongTheoHocKy(hocKyId);
        }

        public void InsertBatch(List<PhanCongGiangDayDTO> list, MySqlTransaction tx)
        {
            string query = @"INSERT INTO PhanCongGiangDay(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc)
                            VALUES(@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @NgayBatDau, @NgayKetThuc)";
            var conn = tx.Connection;
            foreach (var pc in list)
            {
                using (var cmd = new MySqlCommand(query, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@MaLop", pc.MaLop);
                    cmd.Parameters.AddWithValue("@MaGiaoVien", pc.MaGiaoVien);
                    cmd.Parameters.AddWithValue("@MaMonHoc", pc.MaMonHoc);
                    cmd.Parameters.AddWithValue("@MaHocKy", pc.MaHocKy);
                    cmd.Parameters.AddWithValue("@NgayBatDau", pc.NgayBatDau);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", pc.NgayKetThuc);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpsertTemp(List<Services.PhanCongCandidate> list)
        {
            const string clearSql = "DELETE FROM PhanCong_Temp";
            const string insertSql = @"INSERT INTO PhanCong_Temp(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, SoTietTuan, Score, Note)
                                       VALUES(@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @SoTietTuan, @Score, @Note)";
            using (var conn = ConnectDatabase.ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var clr = new MySqlCommand(clearSql, conn, tx)) clr.ExecuteNonQuery();
                        foreach (var c in list)
                        {
                            using (var cmd = new MySqlCommand(insertSql, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@MaLop", c.MaLop);
                                cmd.Parameters.AddWithValue("@MaGiaoVien", c.MaGiaoVien);
                                cmd.Parameters.AddWithValue("@MaMonHoc", c.MaMonHoc);
                                cmd.Parameters.AddWithValue("@MaHocKy", 1);
                                cmd.Parameters.AddWithValue("@SoTietTuan", c.SoTietTuan);
                                cmd.Parameters.AddWithValue("@Score", c.Score);
                                cmd.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(c.Note) ? (object)DBNull.Value : c.Note);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
        // Thêm phân công giảng dạy
        public bool ThemPhanCong(PhanCongGiangDayDTO phanCong)
        {
            string query = @"INSERT INTO PhanCongGiangDay(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) 
                            VALUES(@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @NgayBatDau, @NgayKetThuc)";
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", phanCong.MaLop);
                        cmd.Parameters.AddWithValue("@MaGiaoVien", phanCong.MaGiaoVien);
                        cmd.Parameters.AddWithValue("@MaMonHoc", phanCong.MaMonHoc);
                        cmd.Parameters.AddWithValue("@MaHocKy", phanCong.MaHocKy);
                        cmd.Parameters.AddWithValue("@NgayBatDau", phanCong.NgayBatDau);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", phanCong.NgayKetThuc);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi ThemPhanCong: {ex.Message}");
                throw;
            }
        }

        // Đọc danh sách tất cả phân công
        public List<PhanCongGiangDayDTO> DocDSPhanCong()
        {
            List<PhanCongGiangDayDTO> ds = new List<PhanCongGiangDayDTO>();
            string query = @"SELECT MaPhanCong, MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc 
                            FROM PhanCongGiangDay 
                            ORDER BY MaHocKy DESC, MaLop ASC";

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

                                PhanCongGiangDayDTO pc = new PhanCongGiangDayDTO
                                {
                                    MaPhanCong = reader.GetInt32("MaPhanCong"),
                                    MaLop = reader.GetInt32("MaLop"),
                                    MaGiaoVien = reader.GetString("MaGiaoVien"),
                                    MaMonHoc = reader.GetInt32("MaMonHoc"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    NgayBatDau = reader.GetDateTime("NgayBatDau"),
                                    NgayKetThuc = reader.GetDateTime("NgayKetThuc")
                                };
                                ds.Add(pc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi DocDSPhanCong: {ex.Message}");
                throw;
            }

            return ds;
        }

        // Lấy phân công theo mã
        public PhanCongGiangDayDTO LayPhanCongTheoMa(int maPhanCong)
        {
            PhanCongGiangDayDTO phanCong = null;
            string query = @"SELECT MaPhanCong, MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc 
                            FROM PhanCongGiangDay 
                            WHERE MaPhanCong = @MaPhanCong";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhanCong", maPhanCong);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            
                                {
                                    phanCong = new PhanCongGiangDayDTO
                                    {
                                        MaPhanCong = reader.GetInt32("MaPhanCong"),
                                        MaLop = reader.GetInt32("MaLop"),
                                        MaGiaoVien = reader.GetString("MaGiaoVien"),
                                        MaMonHoc = reader.GetInt32("MaMonHoc"),
                                        MaHocKy = reader.GetInt32("MaHocKy"),
                                        NgayBatDau = reader.GetDateTime("NgayBatDau"),
                                        NgayKetThuc = reader.GetDateTime("NgayKetThuc")
                                    };
                                }
                                else
                                {
                                    Console.WriteLine("Warning: Record with null values found in LayPhanCongTheoMa");
                                }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayPhanCongTheoMa: {ex.Message}");
                throw;
            }

            return phanCong;
        }

        // Lấy danh sách phân công theo lớp
        public List<PhanCongGiangDayDTO> LayPhanCongTheoLop(int maLop)
        {
            List<PhanCongGiangDayDTO> ds = new List<PhanCongGiangDayDTO>();
            string query = @"SELECT MaPhanCong, MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc 
                            FROM PhanCongGiangDay 
                            WHERE MaLop = @MaLop";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                

                                PhanCongGiangDayDTO pc = new PhanCongGiangDayDTO
                                {
                                    MaPhanCong = reader.GetInt32("MaPhanCong"),
                                    MaLop = reader.GetInt32("MaLop"),
                                    MaGiaoVien = reader.GetString("MaGiaoVien"),
                                    MaMonHoc = reader.GetInt32("MaMonHoc"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    NgayBatDau = reader.GetDateTime("NgayBatDau"),
                                    NgayKetThuc = reader.GetDateTime("NgayKetThuc")
                                };
                                ds.Add(pc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayPhanCongTheoLop: {ex.Message}");
                throw;
            }

            return ds;
        }

        // Lấy danh sách phân công theo giáo viên
        public List<PhanCongGiangDayDTO> LayPhanCongTheoGiaoVien(string maGiaoVien)
        {
            List<PhanCongGiangDayDTO> ds = new List<PhanCongGiangDayDTO>();
            string query = @"SELECT MaPhanCong, MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc 
                            FROM PhanCongGiangDay 
                            WHERE MaGiaoVien = @MaGiaoVien";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                

                                PhanCongGiangDayDTO pc = new PhanCongGiangDayDTO
                                {
                                    MaPhanCong = reader.GetInt32("MaPhanCong"),
                                    MaLop = reader.GetInt32("MaLop"),
                                    MaGiaoVien = reader.GetString("MaGiaoVien"),
                                    MaMonHoc = reader.GetInt32("MaMonHoc"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    NgayBatDau = reader.GetDateTime("NgayBatDau"),
                                    NgayKetThuc = reader.GetDateTime("NgayKetThuc")
                                };
                                ds.Add(pc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayPhanCongTheoGiaoVien: {ex.Message}");
                throw;
            }

            return ds;
        }

        // Lấy danh sách phân công theo học kỳ
        public List<PhanCongGiangDayDTO> LayPhanCongTheoHocKy(int maHocKy)
        {
            List<PhanCongGiangDayDTO> ds = new List<PhanCongGiangDayDTO>();
            string query = @"SELECT MaPhanCong, MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc 
                            FROM PhanCongGiangDay 
                            WHERE MaHocKy = @MaHocKy";

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
                               

                                PhanCongGiangDayDTO pc = new PhanCongGiangDayDTO
                                {
                                    MaPhanCong = reader.GetInt32("MaPhanCong"),
                                    MaLop = reader.GetInt32("MaLop"),
                                    MaGiaoVien = reader.GetString("MaGiaoVien"),
                                    MaMonHoc = reader.GetInt32("MaMonHoc"),
                                    MaHocKy = reader.GetInt32("MaHocKy"),
                                    NgayBatDau = reader.GetDateTime("NgayBatDau"),
                                    NgayKetThuc = reader.GetDateTime("NgayKetThuc")
                                };
                                ds.Add(pc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi LayPhanCongTheoHocKy: {ex.Message}");
                throw;
            }

            return ds;
        }

        // Kiểm tra giáo viên có chuyên môn dạy môn học
        public bool KiemTraGiaoVienChuyenMon(string maGiaoVien, int maMonHoc)
        {
            const string sql = @"
                SELECT 1 FROM GiaoVienChuyenMon WHERE MaGiaoVien=@MaGiaoVien AND MaMonHoc=@MaMonHoc
                UNION ALL
                SELECT 1 FROM GiaoVien_MonHoc WHERE MaGiaoVien=@MaGiaoVien AND MaMonHoc=@MaMonHoc LIMIT 1";
            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    var obj = cmd.ExecuteScalar();
                    return obj != null;
                }
            }
        }

        // Cập nhật phân công
        public bool CapNhatPhanCong(PhanCongGiangDayDTO phanCong)
        {
            string query = @"UPDATE PhanCongGiangDay 
                            SET MaLop = @MaLop, 
                                MaGiaoVien = @MaGiaoVien, 
                                MaMonHoc = @MaMonHoc, 
                                MaHocKy = @MaHocKy, 
                                NgayBatDau = @NgayBatDau, 
                                NgayKetThuc = @NgayKetThuc 
                            WHERE MaPhanCong = @MaPhanCong";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", phanCong.MaLop);
                        cmd.Parameters.AddWithValue("@MaGiaoVien", phanCong.MaGiaoVien);
                        cmd.Parameters.AddWithValue("@MaMonHoc", phanCong.MaMonHoc);
                        cmd.Parameters.AddWithValue("@MaHocKy", phanCong.MaHocKy);
                        cmd.Parameters.AddWithValue("@NgayBatDau", phanCong.NgayBatDau);
                        cmd.Parameters.AddWithValue("@NgayKetThuc", phanCong.NgayKetThuc);
                        cmd.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi CapNhatPhanCong: {ex.Message}");
                throw;
            }
        }

        // Xóa phân công
        public bool XoaPhanCong(int maPhanCong)
        {
            string query = "DELETE FROM PhanCongGiangDay WHERE MaPhanCong = @MaPhanCong";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhanCong", maPhanCong);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi XoaPhanCong: {ex.Message}");
                throw;
            }
        }

        // Kiểm tra trùng lặp (UNIQUE constraint)
        public bool KiemTraTrungLap(int maLop, string maGiaoVien, int maMonHoc, int maHocKy, int? maPhanCongHienTai = null)
        {
            string query = @"SELECT COUNT(*) FROM PhanCongGiangDay 
                            WHERE MaLop = @MaLop 
                            AND MaGiaoVien = @MaGiaoVien 
                            AND MaMonHoc = @MaMonHoc 
                            AND MaHocKy = @MaHocKy";

            // Nếu đang cập nhật, bỏ qua bản ghi hiện tại
            if (maPhanCongHienTai.HasValue)
            {
                query += " AND MaPhanCong != @MaPhanCongHienTai";
            }

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                        cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                        if (maPhanCongHienTai.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@MaPhanCongHienTai", maPhanCongHienTai.Value);
                        }

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0; // Trả về true nếu đã tồn tại
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi KiemTraTrungLap: {ex.Message}");
                throw;
            }
        }
    }
}