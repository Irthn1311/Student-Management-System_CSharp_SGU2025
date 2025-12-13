using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.DAO.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Student_Management_System_CSharp_SGU2025.DAO
{
    /*CREATE TABLE MonHoc (
    MaMonHoc INT PRIMARY KEY AUTO_INCREMENT,
    TenMonHoc NVARCHAR(100) NOT NULL,
    SoTiet INT
);*/
    public class MonHocDAO
    {
        public List<MonHocDTO> GetByHocKy(int hocKyId)
        {
            // Mặc định tất cả môn áp dụng cho mọi học kỳ
            return DocDSMH();
        }

        public int GetRequiredPeriods(int maMonHoc, int? maLop = null)
        {
            var mh = LayDSMonHocTheoId(maMonHoc);
            return mh != null ? mh.soTiet : 0;
        }
        public bool ThemMonHoc(MonHocDTO monhoc)
        {
            string query = "insert into MonHoc(TenMonHoc,SoTiet,GhiChu,NamHocBatDau) values(@TenMonHoc,@SoTiet,@GhiChu,@NamHocBatDau)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMonHoc", monhoc.tenMon);
                    cmd.Parameters.AddWithValue("@SoTiet", monhoc.soTiet);
                    cmd.Parameters.AddWithValue("@GhiChu", monhoc.ghiChu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NamHocBatDau", string.IsNullOrEmpty(monhoc.namHocBatDau) ? (object)DBNull.Value : monhoc.namHocBatDau);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
        //tat ca
        public List<MonHocDTO> DocDSMH()
        {
            List<MonHocDTO> ds = new List<MonHocDTO>();
            string query = "select MaMonHoc, TenMonHoc, SoTiet, GhiChu, NamHocBatDau from MonHoc";
            //Tạo đối tượng kết nối tới cơ sở dữ liệu MySQL bằng hàm GetConnection() (do bạn tự định nghĩa trong lớp ConnectionDatabase).            
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                //Tạo một đối tượng lệnh (command) để thực hiện câu truy vấn SQL query thông qua kết nối conn.
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonHocDTO mh = new MonHocDTO();
                            mh.maMon = reader.GetInt32("MaMonHoc");
                            mh.tenMon = reader.GetString("TenMonHoc");
                            mh.soTiet = reader.GetInt32("SoTiet");
                            mh.ghiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString("GhiChu");
                            mh.namHocBatDau = reader.IsDBNull(reader.GetOrdinal("NamHocBatDau")) ? null : reader.GetString("NamHocBatDau");
                            ds.Add(mh); // thêm trong vòng lặp
                        }

                    }
                }
            }
            return ds;
        }
        public MonHocDTO LayDSMonHocTheoId(int maMonHoc)
        {
            MonHocDTO monHoc = null;
            string query = "select MaMonHoc,TenMonHoc,SoTiet,GhiChu,NamHocBatDau from MonHoc where MaMonHoc=@MaMonHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read()) //chi can doc 1 dong
                        {
                            monHoc = new MonHocDTO
                            {
                                maMon = reader.GetInt32("MaMonHoc"),
                                tenMon = reader.GetString("TenMonHoc"),
                                soTiet = reader.GetInt32("SoTiet"),
                                ghiChu = reader.GetString("GhiChu")
                            };
                        }
                    }
                }

            }
            return monHoc; // tra ve doi tuong do, hoac la null
        }
        public MonHocDTO LayDSMonHocTheoTen(string tenMonHoc)
        {
            MonHocDTO monHoc = null;
            string query = "select MaMonHoc,TenMonHoc,SoTiet,GhiChu,NamHocBatDau from MonHoc where TenMonHoc=@TenMonHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMonHoc", tenMonHoc);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read()) //chi can doc 1 dong
                        {
                            monHoc = new MonHocDTO
                            {
                                maMon = reader.GetInt32("MaMonHoc"),
                                tenMon = reader.GetString("TenMonHoc"),
                                soTiet = reader.GetInt32("SoTiet"),
                                ghiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString("GhiChu"),
                                namHocBatDau = reader.IsDBNull(reader.GetOrdinal("NamHocBatDau")) ? null : reader.GetString("NamHocBatDau")
                            };
                        }
                    }
                }

            }
            return monHoc; // tra ve doi tuong do, hoac la null
        }

        /// <summary>
        /// Lấy danh sách môn học theo năm học bắt đầu
        /// </summary>
        public List<MonHocDTO> LayMonHocTheoNamHocBatDau(string maNamHoc)
        {
            List<MonHocDTO> ds = new List<MonHocDTO>();
            string query = "select MaMonHoc, TenMonHoc, SoTiet, GhiChu, NamHocBatDau from MonHoc where NamHocBatDau = @MaNamHoc";
            
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

        public bool UpdateMonHoc(MonHocDTO monhoc) //cap nhat theo ma
        {
            string query = "update MonHoc set TenMonHoc=@TenMonHoc,SoTiet=@SoTiet, GhiChu=@GhiChu, NamHocBatDau=@NamHocBatDau where MaMonHoc=@MaMonHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMonHoc", monhoc.tenMon);
                    cmd.Parameters.AddWithValue("@SoTiet", monhoc.soTiet);
                    cmd.Parameters.AddWithValue("@MaMonHoc", monhoc.maMon);
                    cmd.Parameters.AddWithValue("@GhiChu", monhoc.ghiChu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NamHocBatDau", string.IsNullOrEmpty(monhoc.namHocBatDau) ? (object)DBNull.Value : monhoc.namHocBatDau);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
        /// <summary>
        /// Kiểm tra môn học có đang được sử dụng trong các bảng liên quan không
        /// </summary>
        public (bool dangSuDung, List<string> danhSachSuDung) KiemTraMonHocDangSuDung(int maMonHoc)
        {
            List<string> danhSachSuDung = new List<string>();
            bool dangSuDung = false;

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();

                // ✅ QUAN TRỌNG: Kiểm tra trong DiemSo trước - Nếu có điểm thì KHÔNG CHO XÓA
                string queryDiem = "SELECT COUNT(*) FROM DiemSo WHERE MaMonHoc = @MaMonHoc";
                using (MySqlCommand cmd = new MySqlCommand(queryDiem, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        dangSuDung = true;
                        danhSachSuDung.Add($"Điểm số ({count} bản ghi) - Không thể xóa môn học đã có điểm");
                    }
                }

                // Kiểm tra trong PhanCongGiangDay (chỉ thông báo, không chặn xóa)
                string queryPhanCong = "SELECT COUNT(*) FROM PhanCongGiangDay WHERE MaMonHoc = @MaMonHoc";
                using (MySqlCommand cmd = new MySqlCommand(queryPhanCong, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        danhSachSuDung.Add($"Phân công giảng dạy ({count} phân công)");
                    }
                }

                // Kiểm tra trong ThoiKhoaBieu (qua PhanCongGiangDay) - chỉ thông báo
                string queryTKB = @"
                    SELECT COUNT(*) 
                    FROM ThoiKhoaBieu tkb
                    INNER JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
                    WHERE pc.MaMonHoc = @MaMonHoc";
                using (MySqlCommand cmd = new MySqlCommand(queryTKB, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        danhSachSuDung.Add($"Thời khóa biểu ({count} tiết)");
                    }
                }

                // Kiểm tra trong GiaoVien (chuyên môn) - chỉ thông báo
                string queryGV = "SELECT COUNT(*) FROM GiaoVien WHERE MaMonChuyenMon = @MaMonHoc";
                using (MySqlCommand cmd = new MySqlCommand(queryGV, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        danhSachSuDung.Add($"Giáo viên chuyên môn ({count} giáo viên)");
                    }
                }
            }

            return (dangSuDung, danhSachSuDung);
        }

        public bool DeleteMonHoc(int maMonHoc)
        {
            string query = "delete from MonHoc where MaMonHoc=@MaMonHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
        // ✅✅✅ THÊM METHOD MỚI - TRẢ VỀ ID VỪA THÊM
        public int ThemMonHocVaLayId(MonHocDTO monhoc)
        {
            string query = "INSERT INTO MonHoc(TenMonHoc, SoTiet, GhiChu, NamHocBatDau) VALUES(@TenMonHoc, @SoTiet, @GhiChu, @NamHocBatDau); SELECT LAST_INSERT_ID();";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMonHoc", monhoc.tenMon);
                    cmd.Parameters.AddWithValue("@SoTiet", monhoc.soTiet);
                    cmd.Parameters.AddWithValue("@GhiChu", monhoc.ghiChu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NamHocBatDau", string.IsNullOrEmpty(monhoc.namHocBatDau) ? (object)DBNull.Value : monhoc.namHocBatDau);

                    // ✅ Lấy ID vừa thêm
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public List<MonHocDTO> GetAllMonHoc()
        {
            List<MonHocDTO> list = new List<MonHocDTO>();
            MySqlConnection conn = null;
            try
            {
                conn = ConnectionDatabase.GetConnection();
                conn.Open();
                string query = @"
    SELECT MaMonHoc, TenMonHoc, SoTiet, GhiChu, NamHocBatDau
    FROM MonHoc
    ORDER BY TenMonHoc";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonHocDTO mh = new MonHocDTO();

                            // GÁN TRỰC TIẾP VÀO FIELD thay vì property
                            mh.maMon = Convert.ToInt32(reader["MaMonHoc"]);
                            mh.tenMon = reader["TenMonHoc"].ToString();
                            mh.soTiet = Convert.ToInt32(reader["SoTiet"]);
                            mh.ghiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader["GhiChu"].ToString();
                            mh.namHocBatDau = reader.IsDBNull(reader.GetOrdinal("NamHocBatDau")) ? null : reader["NamHocBatDau"].ToString();

                            list.Add(mh);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách môn học: " + ex.Message);
            }
            finally
            {
                ConnectionDatabase.CloseConnection(conn);
            }
            return list;
        }

    }
}
