using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
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
    internal class MonHocDAO
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
            string query = "insert into MonHoc(TenMonHoc,SoTiet,GhiChu) values(@TenMonHoc,@SoTiet,@GhiChu)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMonHoc", monhoc.tenMon);
                    cmd.Parameters.AddWithValue("@SoTiet", monhoc.soTiet);
                    cmd.Parameters.AddWithValue("@GhiChu", monhoc.ghiChu);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
        //tat ca
        public List<MonHocDTO> DocDSMH()
        {
            List<MonHocDTO> ds = new List<MonHocDTO>();
            string query = "select MaMonHoc, TenMonHoc, SoTiet,GhiChu from MonHoc";
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
                            mh.ghiChu = reader.GetString("GhiChu");
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
            string query = "select MaMonHoc,TenMonHoc,SoTiet,GhiChu from MonHoc where MaMonHoc=@MaMonHoc";
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
            string query = "select MaMonHoc,SoTiet,GhiChu from MonHoc where TenMonHoc=@TenMonHoc";
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
                                ghiChu = reader.GetString("GhiChu")
                            };
                        }
                    }
                }

            }
            return monHoc; // tra ve doi tuong do, hoac la null
        }
        public bool UpdateMonHoc(MonHocDTO monhoc) //cap nhat theo ma
        {
            string query = "update MonHoc set TenMonHoc=@TenMonHoc,SoTiet=@SoTiet, GhiChu=@GhiChu where MaMonHoc=@MaMonHoc";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMonHoc", monhoc.tenMon);
                    cmd.Parameters.AddWithValue("@SoTiet", monhoc.soTiet);
                    cmd.Parameters.AddWithValue("@MaMonHoc", monhoc.maMon);
                    cmd.Parameters.AddWithValue("@GhiChu", monhoc.ghiChu);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
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
            string query = "INSERT INTO MonHoc(TenMonHoc, SoTiet, GhiChu) VALUES(@TenMonHoc, @SoTiet, @GhiChu); SELECT LAST_INSERT_ID();";

            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMonHoc", monhoc.tenMon);
                    cmd.Parameters.AddWithValue("@SoTiet", monhoc.soTiet);
                    cmd.Parameters.AddWithValue("@GhiChu", monhoc.ghiChu);

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
    SELECT MaMonHoc, TenMonHoc, SoTiet
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
