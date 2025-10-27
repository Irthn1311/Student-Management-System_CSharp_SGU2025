using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class PhanLopDAO
    {
        /// <summary>
        /// Thêm một học sinh vào lớp trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu thêm thành công, False nếu thất bại hoặc đã tồn tại.</returns>
        public bool ThemPhanLop(int maHocSinh, int maLop, int maHocKy)
        {
            // Kiểm tra xem phân lớp đã tồn tại chưa
            if (KiemTraPhanLopTonTai(maHocSinh, maLop, maHocKy))
            {
                Console.WriteLine($"Học sinh {maHocSinh} đã được phân vào lớp {maLop} trong học kỳ {maHocKy}.");
                return false;
            }

            string sql = "INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy) VALUES (@maHS, @maLop, @maHK)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maLop", maLop);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm phân lớp: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa phân lớp của một học sinh khỏi lớp trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại.</returns>
        public bool XoaPhanLop(int maHocSinh, int maLop, int maHocKy)
        {
            string sql = "DELETE FROM PhanLop WHERE MaHocSinh = @maHS AND MaLop = @maLop AND MaHocKy = @maHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maLop", maLop);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa phân lớp: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Chuyển học sinh từ lớp này sang lớp khác trong cùng học kỳ.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maLopCu">Mã lớp cũ.</param>
        /// <param name="maLopMoi">Mã lớp mới.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu chuyển thành công, False nếu thất bại.</returns>
        public bool ChuyenLop(int maHocSinh, int maLopCu, int maLopMoi, int maHocKy)
        {
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                MySqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Xóa phân lớp cũ
                    string sqlXoa = "DELETE FROM PhanLop WHERE MaHocSinh = @maHS AND MaLop = @maLopCu AND MaHocKy = @maHK";
                    using (MySqlCommand cmdXoa = new MySqlCommand(sqlXoa, conn, transaction))
                    {
                        cmdXoa.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmdXoa.Parameters.AddWithValue("@maLopCu", maLopCu);
                        cmdXoa.Parameters.AddWithValue("@maHK", maHocKy);
                        cmdXoa.ExecuteNonQuery();
                    }

                    // Thêm phân lớp mới
                    string sqlThem = "INSERT INTO PhanLop (MaHocSinh, MaLop, MaHocKy) VALUES (@maHS, @maLopMoi, @maHK)";
                    using (MySqlCommand cmdThem = new MySqlCommand(sqlThem, conn, transaction))
                    {
                        cmdThem.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmdThem.Parameters.AddWithValue("@maLopMoi", maLopMoi);
                        cmdThem.Parameters.AddWithValue("@maHK", maHocKy);
                        cmdThem.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi chuyển lớp: " + ex.Message);
                    transaction?.Rollback();
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh trong một lớp cụ thể của học kỳ.
        /// </summary>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Danh sách các đối tượng HocSinhDTO.</returns>
        public List<HocSinhDTO> LayDanhSachHocSinhTrongLop(int maLop, int maHocKy)
        {
            List<HocSinhDTO> dsHocSinh = new List<HocSinhDTO>();
            string sql = @"SELECT hs.* 
                          FROM HocSinh hs 
                          JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh 
                          WHERE pl.MaLop = @maLop AND pl.MaHocKy = @maHK";
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maLop", maLop);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);
                        
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HocSinhDTO hs = new HocSinhDTO(
                                    reader.GetInt32("MaHocSinh"),
                                    reader.GetString("HoTen"),
                                    reader.GetDateTime("NgaySinh"),
                                    reader.GetString("GioiTinh"),
                                    reader.IsDBNull(reader.GetOrdinal("SDTHS")) ? null : reader.GetString("SDTHS"),
                                    reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                    reader.GetString("TrangThai")
                                );
                                dsHocSinh.Add(hs);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy danh sách học sinh trong lớp: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return dsHocSinh;
        }

        /// <summary>
        /// Lấy thông tin lớp hiện tại của một học sinh trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Mã lớp nếu tìm thấy, -1 nếu không tìm thấy.</returns>
        public int LayLopCuaHocSinh(int maHocSinh, int maHocKy)
        {
            string sql = "SELECT MaLop FROM PhanLop WHERE MaHocSinh = @maHS AND MaHocKy = @maHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);
                        
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy lớp của học sinh: " + ex.Message);
                    return -1;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Đếm số lượng học sinh trong một lớp cụ thể của học kỳ.
        /// </summary>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Số lượng học sinh.</returns>
        public int DemSoLuongHocSinhTrongLop(int maLop, int maHocKy)
        {
            string sql = "SELECT COUNT(*) FROM PhanLop WHERE MaLop = @maLop AND MaHocKy = @maHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maLop", maLop);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);
                        
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi đếm số lượng học sinh trong lớp: " + ex.Message);
                    return 0;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Kiểm tra xem một học sinh đã được phân vào lớp trong học kỳ cụ thể chưa.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maLop">Mã lớp.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu đã tồn tại, False nếu chưa.</returns>
        public bool KiemTraPhanLopTonTai(int maHocSinh, int maLop, int maHocKy)
        {
            string sql = "SELECT COUNT(*) FROM PhanLop WHERE MaHocSinh = @maHS AND MaLop = @maLop AND MaHocKy = @maHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maLop", maLop);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);
                        
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra phân lớp tồn tại: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Kiểm tra xem một học sinh đã được phân vào lớp nào trong học kỳ cụ thể chưa.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu đã được phân lớp, False nếu chưa.</returns>
        public bool KiemTraHocSinhDaPhanLop(int maHocSinh, int maHocKy)
        {
            string sql = "SELECT COUNT(*) FROM PhanLop WHERE MaHocSinh = @maHS AND MaHocKy = @maHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);
                        
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra học sinh đã phân lớp: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa tất cả phân lớp của một học sinh trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại.</returns>
        public bool XoaPhanLopTheoHocSinh(int maHocSinh, int maHocKy)
        {
            string sql = "DELETE FROM PhanLop WHERE MaHocSinh = @maHS AND MaHocKy = @maHK";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);
                        
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa phân lớp theo học sinh: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy tất cả phân lớp trong hệ thống.
        /// </summary>
        /// <returns>Danh sách các tuple (MaHocSinh, MaLop, MaHocKy).</returns>
        public List<(int maHocSinh, int maLop, int maHocKy)> LayTatCaPhanLop()
        {
            List<(int, int, int)> ds = new List<(int, int, int)>();
            string sql = "SELECT MaHocSinh, MaLop, MaHocKy FROM PhanLop ORDER BY MaHocKy, MaLop, MaHocSinh";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ds.Add((reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi lấy tất cả phân lớp: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return ds;
        }

        /// <summary>
        /// Lấy danh sách học sinh chưa được phân lớp trong học kỳ cụ thể.
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ.</param>
        /// <returns>Danh sách các đối tượng HocSinhDTO chưa được phân lớp.</returns>
        public List<HocSinhDTO> LayHocSinhChuaPhanLop(int maHocKy)
        {
            List<HocSinhDTO> dsHocSinh = new List<HocSinhDTO>();
            string sql = @"SELECT hs.* 
                          FROM HocSinh hs 
                          LEFT JOIN PhanLop pl ON hs.MaHocSinh = pl.MaHocSinh AND pl.MaHocKy = @maHK
                          WHERE pl.MaHocSinh IS NULL AND hs.TrangThai = 'Đang học'";
            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHK", maHocKy);
                        
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HocSinhDTO hs = new HocSinhDTO(
                                    reader.GetInt32("MaHocSinh"),
                                    reader.GetString("HoTen"),
                                    reader.GetDateTime("NgaySinh"),
                                    reader.GetString("GioiTinh"),
                                    reader.IsDBNull(reader.GetOrdinal("SDTHS")) ? null : reader.GetString("SDTHS"),
                                    reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                    reader.GetString("TrangThai")
                                );
                                dsHocSinh.Add(hs);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy học sinh chưa phân lớp: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return dsHocSinh;
        }
    }
}