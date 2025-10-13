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
    internal class MonHocDAO
    {
        public bool ThemMonHoc(MonHocDTO monhoc)
        {
            string query = "insert into MonHoc(Ma_Mon_Hoc,Ten_Mon_Hoc,So_Tiet) values(@Ma_Mon_Hoc,@Ten_Mon_Hoc,@So_Tiet)";
            using (MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Ma_Mon_Hoc", monhoc.maMon);
                    cmd.Parameters.AddWithValue("@Ten_Mon_Hoc", monhoc.tenMon);
                    cmd.Parameters.AddWithValue("@So_Tiet", monhoc.soTiet);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
        //tat ca
        public List<MonHocDTO> DocDSMN(){
            List<MonHocDTO> ds = new List<MonHocDTO>();
            string query="select Ma_Mon_Hoc, Ten_Mon_Hoc, So_Tiet from MonHoc";
            //Tạo đối tượng kết nối tới cơ sở dữ liệu MySQL bằng hàm GetConnection() (do bạn tự định nghĩa trong lớp ConnectionDatabase).            
            using (MySqlConnection conn=ConnectionDatabase.GetConnection()) 
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
                            mh.maMon = reader.GetString("Ma_Mon_Hoc");
                            mh.tenMon = reader.GetString("Ten_Mon_Hoc");
                            mh.soTiet = reader.GetInt32("So_Tiet");
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
            string query = "select Ma_Mon_Hoc,Ten_Mon_Hoc,So_Tiet from MonHoc where Ma_Mon_Hoc=@MaMonHoc";
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
                                maMon = reader.GetString("Ma_Mon_Hoc"),
                                tenMon = reader.GetString("Ten_Mon_Hoc"),
                                soTiet = reader.GetInt32("So_Tiet")
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
            string query = "select Ma_Mon_Hoc,So_Tiet from MonHoc where Ten_Mon_Hoc=@TenMonHoc";
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
                                maMon = reader.GetString("Ma_Mon_Hoc"),
                                tenMon = reader.GetString("Ten_Mon_Hoc"),
                                soTiet = reader.GetInt32("So_Tiet")
                            };
                        }
                    }
                }

            }
            return monHoc; // tra ve doi tuong do, hoac la null
        }
        public bool UpdateMonHoc(MonHocDTO monhoc) //cap nhat theo ma
        {
            string query = "update MonHoc set Ten_Mon_Hoc=@TenMonHoc,So_Tiet=@SoTiet where Ma_Mon_Hoc=@MaMonHoc";
            using(MySqlConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd=new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMonHoc", monhoc.tenMon);
                    cmd.Parameters.AddWithValue("@SoTiet", monhoc.soTiet);
                    cmd.Parameters.AddWithValue("@MaMonHoc", monhoc.maMon);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
        public bool DeleteMonHoc(int maMonHoc)
        {
            string query = "delete from MonHoc where Ma_Mon_Hoc=@MaMonHoc";
            using (MySqlConnection conn=ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd=new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", maMonHoc);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
