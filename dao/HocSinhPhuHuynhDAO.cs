using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase; // Namespace ConnectionDatabase
using Student_Management_System_CSharp_SGU2025.DTO;          // Namespace chứa DTOs
using System;
using System.Collections.Generic;
using System.Data;

namespace Student_Management_System_CSharp_SGU2025.DAO
{
    internal class HocSinhPhuHuynhDAO
    {
        /// <summary>
        /// Thêm một mối quan hệ mới giữa Học sinh và Phụ huynh.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maPhuHuynh">Mã phụ huynh.</param>
        /// <param name="moiQuanHe">Mối quan hệ (ví dụ: 'Bố', 'Mẹ').</param>
        /// <returns>True nếu thêm thành công, False nếu thất bại hoặc đã tồn tại.</returns>
        public bool ThemQuanHe(int maHocSinh, int maPhuHuynh, string moiQuanHe)
        {
            // Kiểm tra xem mối quan hệ đã tồn tại chưa
            if (KiemTraQuanHeTonTai(maHocSinh, maPhuHuynh))
            {
                Console.WriteLine($"Mối quan hệ giữa HS {maHocSinh} và PH {maPhuHuynh} đã tồn tại.");
                return false; // Hoặc ném Exception tùy logic
            }

            string sql = "INSERT INTO HocSinhPhuHuynh (MaHocSinh, MaPhuHuynh, MoiQuanHe) VALUES (@maHS, @maPH, @quanHe)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maPH", maPhuHuynh);
                        cmd.Parameters.AddWithValue("@quanHe", moiQuanHe);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi thêm mối quan hệ HS-PH: " + ex.Message);
                    // throw; // Có thể ném lại lỗi
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa một mối quan hệ giữa Học sinh và Phụ huynh.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maPhuHuynh">Mã phụ huynh.</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại.</returns>
        public bool XoaQuanHe(int maHocSinh, int maPhuHuynh)
        {
            string sql = "DELETE FROM HocSinhPhuHuynh WHERE MaHocSinh = @maHS AND MaPhuHuynh = @maPH";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maPH", maPhuHuynh);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa mối quan hệ HS-PH: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin mối quan hệ (chủ yếu là cột MoiQuanHe).
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maPhuHuynh">Mã phụ huynh.</param>
        /// <param name="moiQuanHeMoi">Mối quan hệ mới.</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại.</returns>
        public bool CapNhatQuanHe(int maHocSinh, int maPhuHuynh, string moiQuanHeMoi)
        {
            string sql = "UPDATE HocSinhPhuHuynh SET MoiQuanHe = @quanHe WHERE MaHocSinh = @maHS AND MaPhuHuynh = @maPH";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@quanHe", moiQuanHeMoi);
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maPH", maPhuHuynh);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi cập nhật mối quan hệ HS-PH: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy danh sách Phụ huynh và mối quan hệ của một Học sinh cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh cần tra cứu.</param>
        /// <returns>Danh sách các đối tượng PhuHuynhDTO kèm theo Mối quan hệ.</returns>
        public List<(PhuHuynhDTO phuHuynh, string moiQuanHe)> LayPhuHuynhCuaHocSinh(int maHocSinh)
        {
            List<(PhuHuynhDTO, string)> dsPhuHuynh = new List<(PhuHuynhDTO, string)>();
            // JOIN 3 bảng: HocSinhPhuHuynh -> PhuHuynh
            string sql = @"SELECT ph.*, hsph.MoiQuanHe
                           FROM PhuHuynh ph
                           JOIN HocSinhPhuHuynh hsph ON ph.MaPhuHuynh = hsph.MaPhuHuynh
                           WHERE hsph.MaHocSinh = @maHS";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
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
                                string moiQuanHe = reader.GetString("MoiQuanHe");
                                dsPhuHuynh.Add((ph, moiQuanHe)); // Thêm Tuple vào danh sách
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy phụ huynh của học sinh: " + ex.Message);
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
        /// Lấy danh sách Học sinh và mối quan hệ của một Phụ huynh cụ thể.
        /// </summary>
        /// <param name="maPhuHuynh">Mã phụ huynh cần tra cứu.</param>
        /// <returns>Danh sách các đối tượng HocSinhDTO kèm theo Mối quan hệ.</returns>
        public List<(HocSinhDTO hocSinh, string moiQuanHe)> LayHocSinhCuaPhuHuynh(int maPhuHuynh)
        {
            List<(HocSinhDTO, string)> dsHocSinh = new List<(HocSinhDTO, string)>();
            // JOIN 3 bảng: HocSinhPhuHuynh -> HocSinh
            string sql = @"SELECT hs.*, hsph.MoiQuanHe
                           FROM HocSinh hs
                           JOIN HocSinhPhuHuynh hsph ON hs.MaHocSinh = hsph.MaHocSinh
                           WHERE hsph.MaPhuHuynh = @maPH";
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
                                string moiQuanHe = reader.GetString("MoiQuanHe");
                                dsHocSinh.Add((hs, moiQuanHe)); // Thêm Tuple vào danh sách
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi lấy học sinh của phụ huynh: " + ex.Message);
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
        /// Kiểm tra xem một mối quan hệ cụ thể đã tồn tại hay chưa.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <param name="maPhuHuynh">Mã phụ huynh.</param>
        /// <returns>True nếu đã tồn tại, False nếu chưa.</returns>
        public bool KiemTraQuanHeTonTai(int maHocSinh, int maPhuHuynh)
        {
            string sql = "SELECT COUNT(*) FROM HocSinhPhuHuynh WHERE MaHocSinh = @maHS AND MaPhuHuynh = @maPH";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.Parameters.AddWithValue("@maPH", maPhuHuynh);
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi kiểm tra tồn tại quan hệ HS-PH: " + ex.Message);
                    throw;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Lấy tất cả các mối quan hệ Học sinh - Phụ huynh trong hệ thống.
        /// </summary>
        /// <returns>Danh sách các tuple (MaHocSinh, MaPhuHuynh, MoiQuanHe).</returns>
        public List<(int maHocSinh, int maPhuHuynh, string moiQuanHe)> LayTatCaQuanHe()
        {
            List<(int, int, string)> ds = new List<(int, int, string)>();
            string sql = "SELECT MaHocSinh, MaPhuHuynh, MoiQuanHe FROM HocSinhPhuHuynh";
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
                            ds.Add((reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2)));
                        }
                    }
                }
                catch (Exception ex) { 
                    Console.WriteLine($"{ex.Message}");
                    throw; 
                }
                finally { ConnectionDatabase.CloseConnection(conn); }
            }
            return ds;
        }

        /// <summary>
        /// Xóa TẤT CẢ mối quan hệ của một Học sinh cụ thể.
        /// </summary>
        /// <param name="maHocSinh">Mã học sinh.</param>
        /// <returns>True nếu xóa thành công (hoặc không có gì để xóa), False nếu có lỗi.</returns>
        public bool XoaQuanHeTheoMaHocSinh(int maHocSinh)
        {
            string sql = "DELETE FROM HocSinhPhuHuynh WHERE MaHocSinh = @maHS";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHS", maHocSinh);
                        cmd.ExecuteNonQuery(); // Không cần kiểm tra số dòng vì có thể không có QH nào
                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa mối quan hệ theo Mã Học Sinh: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }

        /// <summary>
        /// Xóa TẤT CẢ mối quan hệ của một Phụ huynh cụ thể.
        /// </summary>
        /// <param name="maPhuHuynh">Mã phụ huynh.</param>
        /// <returns>True nếu xóa thành công (hoặc không có gì để xóa), False nếu có lỗi.</returns>
        public bool XoaQuanHeTheoMaPhuHuynh(int maPhuHuynh)
        {
            string sql = "DELETE FROM HocSinhPhuHuynh WHERE MaPhuHuynh = @maPH";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maPH", maPhuHuynh);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Lỗi xóa mối quan hệ theo Mã Phụ Huynh: " + ex.Message);
                    return false;
                }
                finally
                {
                    ConnectionDatabase.CloseConnection(conn);
                }
            }
        }
    }
}