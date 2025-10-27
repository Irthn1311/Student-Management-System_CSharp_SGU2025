using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase; // Namespace chứa ConnectionDatabase
using Student_Management_System_CSharp_SGU2025.DTO;          // Namespace chứa PhuHuynhDTO
using System;
using System.Collections.Generic;
using System.Data;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class PhuHuynhDAO
    {
        /// <summary>
        /// Kiểm tra xem Mã Phụ Huynh đã tồn tại trong database chưa.
        /// </summary>
        /// <param name="maPhuHuynh">Mã phụ huynh cần kiểm tra.</param>
        /// <returns>True nếu tồn tại, False nếu không.</returns>
        public bool KiemTraTonTai(int maPhuHuynh)
        {
            string sql = "SELECT COUNT(*) FROM PhuHuynh WHERE MaPhuHuynh = @maPH";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maPH", maPhuHuynh);
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra tồn tại phụ huynh: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Kiểm tra SĐT đã tồn tại chưa.
        /// Nếu maPhuHuynhToExclude > 0, hàm sẽ loại trừ mã đó ra khỏi tìm kiếm (dùng cho Cập nhật).
        /// </summary>
        public bool KiemTraTrungSdt(string sdt, int maPhuHuynhToExclude = 0)
        {
            if (string.IsNullOrWhiteSpace(sdt)) return false;

            // Xây dựng câu SQL động
            string sql = "SELECT COUNT(*) FROM PhuHuynh WHERE SoDienThoai = @sdt";
            if (maPhuHuynhToExclude > 0)
            {
                sql += " AND MaPhuHuynh != @maPH";
            }

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@sdt", sdt.Trim());
                        if (maPhuHuynhToExclude > 0)
                        {
                            cmd.Parameters.AddWithValue("@maPH", maPhuHuynhToExclude);
                        }
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra trùng SĐT: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Kiểm tra Email đã tồn tại chưa.
        /// Nếu maPhuHuynhToExclude > 0, hàm sẽ loại trừ mã đó ra khỏi tìm kiếm (dùng cho Cập nhật).
        /// </summary>
        public bool KiemTraTrungEmail(string email, int maPhuHuynhToExclude = 0)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            string sql = "SELECT COUNT(*) FROM PhuHuynh WHERE Email = @email";
            if (maPhuHuynhToExclude > 0)
            {
                sql += " AND MaPhuHuynh != @maPH";
            }

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email.Trim());
                        if (maPhuHuynhToExclude > 0)
                        {
                            cmd.Parameters.AddWithValue("@maPH", maPhuHuynhToExclude);
                        }
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra trùng Email: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả phụ huynh từ database.
        /// </summary>
        /// <returns>Danh sách các đối tượng PhuHuynhDTO.</returns>
        public List<PhuHuynhDTO> LayDanhSachPhuHuynh()
        {
            List<PhuHuynhDTO> dsPhuHuynh = new List<PhuHuynhDTO>();
            string sql = "SELECT * FROM PhuHuynh";
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
                            PhuHuynhDTO ph = new PhuHuynhDTO(
                                reader.GetInt32("MaPhuHuynh"),
                                reader.GetString("HoTen"),
                                reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString("SoDienThoai"),
                                reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString("DiaChi")
                            );
                            dsPhuHuynh.Add(ph);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy danh sách phụ huynh: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return dsPhuHuynh;
        }

        /// <summary>
        /// Thêm một phụ huynh mới vào database.
        /// </summary>
        /// <param name="ph">Đối tượng PhuHuynhDTO chứa thông tin cần thêm.</param>
        /// <returns>True nếu thêm thành công, False nếu thất bại.</returns>
        public bool ThemPhuHuynh(PhuHuynhDTO ph)
        {
            // MaPhuHuynh là AUTO_INCREMENT
            string sql = "INSERT INTO PhuHuynh (HoTen, SoDienThoai, Email, DiaChi) VALUES (@hoTen, @sdt, @email, @diaChi)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                MySqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@hoTen", ph.HoTen);
                        cmd.Parameters.AddWithValue("@sdt", (object)ph.SoDienThoai ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", (object)ph.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@diaChi", (object)ph.DiaChi ?? DBNull.Value);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm phụ huynh: " + ex.Message);
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
        /// Xóa một phụ huynh khỏi database dựa vào Mã Phụ Huynh.
        /// </summary>
        /// <param name="maPhuHuynh">Mã phụ huynh cần xóa.</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại.</returns>
        public bool XoaPhuHuynh(int maPhuHuynh)
        {
            string sql = "DELETE FROM PhuHuynh WHERE MaPhuHuynh = @maPH";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maPH", maPhuHuynh);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa phụ huynh: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin một phụ huynh trong database.
        /// </summary>
        /// <param name="ph">Đối tượng PhuHuynhDTO chứa thông tin cập nhật.</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại.</returns>
        public bool CapNhatPhuHuynh(PhuHuynhDTO ph)
        {
            string sql = @"UPDATE PhuHuynh SET
                           HoTen = @hoTen,
                           SoDienThoai = @sdt,
                           Email = @email,
                           DiaChi = @diaChi
                         WHERE MaPhuHuynh = @maPH";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@hoTen", ph.HoTen);
                        cmd.Parameters.AddWithValue("@sdt", (object)ph.SoDienThoai ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", (object)ph.Email ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@diaChi", (object)ph.DiaChi ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@maPH", ph.MaPhuHuynh); // Điều kiện WHERE

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật phụ huynh: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Tìm kiếm một phụ huynh theo Mã Phụ Huynh.
        /// </summary>
        /// <param name="maPhuHuynh">Mã phụ huynh cần tìm.</param>
        /// <returns>Đối tượng PhuHuynhDTO nếu tìm thấy, null nếu không.</returns>
        public PhuHuynhDTO TimPhuHuynhTheoMa(int maPhuHuynh)
        {
            string sql = "SELECT * FROM PhuHuynh WHERE MaPhuHuynh = @maPH";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maPH", maPhuHuynh);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                PhuHuynhDTO ph = new PhuHuynhDTO(
                                    reader.GetInt32("MaPhuHuynh"),
                                    reader.GetString("HoTen"),
                                    reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString("SoDienThoai"),
                                    reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                    reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString("DiaChi")
                                );
                                return ph;
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi tìm phụ huynh theo mã: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
            return null; // Không tìm thấy
        }

        // --- Bạn có thể thêm các hàm tìm kiếm/lấy dữ liệu khác ở đây ---
        // Ví dụ: Lấy danh sách phụ huynh của một học sinh (cần JOIN)
        // public List<PhuHuynhDTO> LayPhuHuynhCuaHocSinh(int maHocSinh) { ... }
    }
}