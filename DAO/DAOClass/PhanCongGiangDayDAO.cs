using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    public class PhanCongGiangDayDAO
    {
        public List<PhanCongGiangDayDTO> GetByHocKy(int hocKyId)
        {
            return LayPhanCongTheoHocKy(hocKyId);
        }

        public void InsertBatch(List<PhanCongGiangDayDTO> list, MySqlTransaction tx)
        {
            string query = @"INSERT INTO PhanCongGiangDay(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc)
                            VALUES(@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @NgayBatDau, @NgayKetThuc)";
            string checkQuery = @"SELECT COUNT(*) FROM PhanCongGiangDay 
                                WHERE MaLop = @MaLop 
                                AND MaMonHoc = @MaMonHoc 
                                AND MaHocKy = @MaHocKy";
            var conn = tx.Connection;
            
            foreach (var pc in list)
            {
                // ✅ Check for duplicate subject-class-semester before insert
                using (var checkCmd = new MySqlCommand(checkQuery, conn, tx))
                {
                    checkCmd.Parameters.AddWithValue("@MaLop", pc.MaLop);
                    checkCmd.Parameters.AddWithValue("@MaMonHoc", pc.MaMonHoc);
                    checkCmd.Parameters.AddWithValue("@MaHocKy", pc.MaHocKy);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        throw new InvalidOperationException(
                            $"Môn học {pc.MaMonHoc} đã được phân công cho lớp {pc.MaLop} trong học kỳ {pc.MaHocKy}");
                    }
                }

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

        /// <summary>
        /// Lưu tạm phân công vào bảng PhanCong_Temp
        /// </summary>
        /// <param name="list">Danh sách phân công cần lưu</param>
        /// <param name="hocKyId">Mã học kỳ (bắt buộc)</param>
        public void UpsertTemp(List<PhanCongCandidateDTO> list, int hocKyId)
        {
            if (hocKyId <= 0)
                throw new ArgumentException("Mã học kỳ không hợp lệ");

            const string clearSql = "DELETE FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
            const string insertSql = @"INSERT INTO PhanCong_Temp(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, SoTietTuan, Score, Note)
                                       VALUES(@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @SoTietTuan, @Score, @Note)";
            using (var conn = ConnectDatabase.ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // Xóa dữ liệu tạm của học kỳ này (không xóa toàn bộ)
                        using (var clr = new MySqlCommand(clearSql, conn, tx))
                        {
                            clr.Parameters.AddWithValue("@MaHocKy", hocKyId);
                            clr.ExecuteNonQuery();
                        }
                        
                        foreach (var c in list)
                        {
                            using (var cmd = new MySqlCommand(insertSql, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@MaLop", c.MaLop);
                                cmd.Parameters.AddWithValue("@MaGiaoVien", c.MaGiaoVien);
                                cmd.Parameters.AddWithValue("@MaMonHoc", c.MaMonHoc);
                                cmd.Parameters.AddWithValue("@MaHocKy", hocKyId); // ✅ Sửa: Dùng hocKyId từ tham số
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
            // ✅ Check for duplicate subject-class-semester before insert
            if (KiemTraTrungLapMonHoc(phanCong.MaLop, phanCong.MaMonHoc, phanCong.MaHocKy))
            {
                throw new InvalidOperationException(
                    $"Môn học {phanCong.MaMonHoc} đã được phân công cho lớp {phanCong.MaLop} trong học kỳ {phanCong.MaHocKy}");
            }

            string query = @"INSERT INTO PhanCongGiangDay(MaLop, MaGiaoVien, MaMonHoc, MaHocKy, NgayBatDau, NgayKetThuc) 
                            VALUES(@MaLop, @MaGiaoVien, @MaMonHoc, @MaHocKy, @NgayBatDau, @NgayKetThuc)";
            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlTransaction tx = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@MaLop", phanCong.MaLop);
                                cmd.Parameters.AddWithValue("@MaGiaoVien", phanCong.MaGiaoVien);
                                cmd.Parameters.AddWithValue("@MaMonHoc", phanCong.MaMonHoc);
                                cmd.Parameters.AddWithValue("@MaHocKy", phanCong.MaHocKy);
                                cmd.Parameters.AddWithValue("@NgayBatDau", phanCong.NgayBatDau);
                                cmd.Parameters.AddWithValue("@NgayKetThuc", phanCong.NgayKetThuc);

                                int result = cmd.ExecuteNonQuery();
                                tx.Commit(); // ✅ Commit inside try block
                                return result > 0;
                            }
                        }
                        catch
                        {
                            tx.Rollback(); // ✅ Rollback in catch block
                            throw;
                        }
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
            // ✅ Updated: Query GiaoVien table directly using MaMonChuyenMon
            const string sql = @"
                SELECT COUNT(*) 
                FROM GiaoVien 
                WHERE MaGiaoVien = @MaGiaoVien 
                AND MaMonChuyenMon = @MaMonHoc";
            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaGiaoVien", maGiaoVien);
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // Lấy danh sách giáo viên theo môn học (chuyên môn)
        public List<GiaoVienDTO> GetGiaoVienByMonHoc(int maMonHoc)
        {
            List<GiaoVienDTO> ds = new List<GiaoVienDTO>();
            // ✅ Updated: Query GiaoVien table directly using MaMonChuyenMon
            string query = @"SELECT MaGiaoVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaMonChuyenMon, TrangThai 
                            FROM GiaoVien 
                            WHERE MaMonChuyenMon = @MaMonHoc 
                            AND TrangThai = 'Đang giảng dạy'
                            ORDER BY HoTen";

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GiaoVienDTO gv = new GiaoVienDTO
                                {
                                    MaGiaoVien = reader.GetString("MaGiaoVien"),
                                    HoTen = reader.GetString("HoTen"),
                                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? DateTime.MinValue : reader.GetDateTime("NgaySinh"),
                                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString("GioiTinh"),
                                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString("DiaChi"),
                                    SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? "" : reader.GetString("SoDienThoai"),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString("Email"),
                                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? "Đang giảng dạy" : reader.GetString("TrangThai")
                                };
                                ds.Add(gv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi GetGiaoVienByMonHoc: {ex.Message}");
                throw;
            }

            return ds;
        }

        // Cập nhật phân công
        public bool CapNhatPhanCong(PhanCongGiangDayDTO phanCong)
        {
            // ✅ Check for duplicate subject-class-semester (excluding current record)
            if (KiemTraTrungLapMonHoc(phanCong.MaLop, phanCong.MaMonHoc, phanCong.MaHocKy, phanCong.MaPhanCong))
            {
                throw new InvalidOperationException(
                    $"Môn học {phanCong.MaMonHoc} đã được phân công cho lớp {phanCong.MaLop} trong học kỳ {phanCong.MaHocKy}");
            }

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
                    using (MySqlTransaction tx = conn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(query, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@MaLop", phanCong.MaLop);
                                cmd.Parameters.AddWithValue("@MaGiaoVien", phanCong.MaGiaoVien);
                                cmd.Parameters.AddWithValue("@MaMonHoc", phanCong.MaMonHoc);
                                cmd.Parameters.AddWithValue("@MaHocKy", phanCong.MaHocKy);
                                cmd.Parameters.AddWithValue("@NgayBatDau", phanCong.NgayBatDau);
                                cmd.Parameters.AddWithValue("@NgayKetThuc", phanCong.NgayKetThuc);
                                cmd.Parameters.AddWithValue("@MaPhanCong", phanCong.MaPhanCong);

                                int result = cmd.ExecuteNonQuery();
                                tx.Commit(); // ✅ Commit inside try block
                                return result > 0;
                            }
                        }
                        catch
                        {
                            tx.Rollback(); // ✅ Rollback in catch block
                            throw;
                        }
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

        // Kiểm tra trùng lặp môn học cho lớp trong học kỳ (không phân biệt giáo viên)
        // Input: maLop, maMonHoc, maHocKy, maPhanCongExclude (nullable)
        // Returns: true nếu đã tồn tại phân công môn học này cho lớp trong học kỳ
        public bool KiemTraTrungLapMonHoc(int maLop, int maMonHoc, int maHocKy, int? maPhanCongExclude = null)
        {
            string query = @"SELECT COUNT(*) FROM PhanCongGiangDay 
                            WHERE MaLop = @MaLop 
                            AND MaMonHoc = @MaMonHoc 
                            AND MaHocKy = @MaHocKy";

            // Nếu đang cập nhật, bỏ qua bản ghi hiện tại
            if (maPhanCongExclude.HasValue)
            {
                query += " AND MaPhanCong != @MaPhanCongExclude";
            }

            try
            {
                using (MySqlConnection conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                        cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);

                        if (maPhanCongExclude.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@MaPhanCongExclude", maPhanCongExclude.Value);
                        }

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0; // Trả về true nếu đã tồn tại
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi KiemTraTrungLapMonHoc: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Kiểm tra học kỳ có phân công chính thức không
        /// </summary>
        public bool HasAssignmentsForSemester(int maHocKy)
        {
            const string sql = "SELECT COUNT(*) FROM PhanCongGiangDay WHERE MaHocKy = @MaHocKy";
            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// Kiểm tra học kỳ có phân công tạm không
        /// </summary>
        public bool HasTempAssignmentsForSemester(int maHocKy)
        {
            const string sql = "SELECT COUNT(*) FROM PhanCong_Temp WHERE MaHocKy = @MaHocKy";
            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // Kiểm tra trùng lặp (UNIQUE constraint - đầy đủ: lớp, giáo viên, môn học, học kỳ)
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