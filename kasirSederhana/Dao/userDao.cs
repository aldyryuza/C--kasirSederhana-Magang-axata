using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kasirSederhana.Model;
using FirebirdSql.Data.FirebirdClient;

namespace kasirSederhana.Dao
{
    public class userDao
    {
        private FbConnection conn;
        private string strsql = string.Empty;

        //constructor

        public userDao(FbConnection conn)
        {
            this.conn = conn;
        }


        //Query Tambah 
        public int Tambah(userModel user)
        {
            strsql = @"INSERT INTO user1  VALUES (gen_id(gen_user1_id, 1), @1,@2,@3,@4)";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", user.Nama);
                cmd.Parameters.AddWithValue("@2", user.Username);
                cmd.Parameters.AddWithValue("@3", user.Password);
                cmd.Parameters.AddWithValue("@4", user.Keterangan);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }

        //Query Update
        public int Update(userModel spl)
        {
            strsql = @"Update user1 set 
                        NAMA = @1,
                        USERNAME = @2,
                        PW = @3,
                        KETERANGAN = @4
                        Where ID = @5";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", spl.Nama);
                cmd.Parameters.AddWithValue("@2", spl.Username);
                cmd.Parameters.AddWithValue("@3", spl.Password);
                cmd.Parameters.AddWithValue("@4", spl.Keterangan);
                cmd.Parameters.AddWithValue("@5", spl.Id);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        //Query Hapus
        public int Hapus(int Id)
        {
            strsql = "DELETE FROM user1 WHERE ID = @5";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@5", Id);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        private userModel MappingRowToObject(FbDataReader dtr)
        {
            userModel spl = new userModel();
            spl.Id = int.Parse(dtr["ID"] is DBNull ? string.Empty : dtr["ID"].ToString());
            spl.Nama = dtr["NAMA"] is DBNull ? string.Empty : dtr["NAMA"].ToString();
            spl.Username = dtr["USERNAME"] is DBNull ? string.Empty : dtr["USERNAME"].ToString();
            spl.Password = dtr["PW"] is DBNull ? string.Empty : dtr["PW"].ToString();
            spl.Keterangan = dtr["KETERANGAN"] is DBNull ? string.Empty : dtr["KETERANGAN"].ToString();

            return spl;
        }
        public List<userModel> GetAll()
        {
            List<userModel> daftarSuplier = new List<userModel>();
            strsql = "SELECT * FROM user1 ORDER BY ID";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                using (FbDataReader dtr = cmd.ExecuteReader())
                {

                    while (dtr.Read())
                    {
                        daftarSuplier.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarSuplier;
        }

        public List<userModel> GetByName(string nama)
        {
            List<userModel> daftarSuplier = new List<userModel>();
            strsql = @"SELECT * FROM user1 WHERE NAMA LIKE @1 OR KETERANGAN LIKE @1  ORDER BY NAMA";

            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", "%" + nama + "%");
                using (FbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        daftarSuplier.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return daftarSuplier;
        }

    }
}
