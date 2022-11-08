using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using kasirSederhana.Model;


namespace kasirSederhana.Dao
{
    public class suplierDao
    {
        private FbConnection conn;
        private string strsql = string.Empty;

        public suplierDao(FbConnection conn)
        {
            this.conn = conn;
        }

        public int TambahSuplier(suplierModel spl)
        {
            strsql = @"INSERT INTO suplier2  VALUES (gen_id(gen_suplier2_id, 1), @1,@2,@3,@4)";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", spl.Nama);
                cmd.Parameters.AddWithValue("@2", spl.Alamat);
                cmd.Parameters.AddWithValue("@3", spl.Telepon);
                cmd.Parameters.AddWithValue("@4", spl.Keterangan);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }

        public int UpdateSuplier(suplierModel spl)
        {
            strsql = @"Update suplier2 set 
                        NAMA = @1,
                        ALAMAT = @2,
                        TELEPON = @3,
                        KETERANGAN = @4
                        Where ID = @5";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", spl.Nama);
                cmd.Parameters.AddWithValue("@2", spl.Alamat);
                cmd.Parameters.AddWithValue("@3", spl.Telepon);
                cmd.Parameters.AddWithValue("@4", spl.Keterangan);
                cmd.Parameters.AddWithValue("@5", spl.Id);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public int HapusSuplier(int IdSuplier )
        {
            strsql = "DELETE FROM suplier2 WHERE ID = @5";
            using (FbCommand cmd = new FbCommand(strsql , conn))
            {
                cmd.Parameters.AddWithValue("@5", IdSuplier);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        private suplierModel MappingRowToObject(FbDataReader dtr)
        {
            suplierModel spl = new suplierModel();
            spl.Id = int.Parse(dtr["ID"] is DBNull ? string.Empty : dtr["ID"].ToString());
            spl.Nama = dtr["NAMA"] is DBNull ? string.Empty : dtr["NAMA"].ToString();
            spl.Alamat = dtr["ALAMAT"] is DBNull ? string.Empty : dtr["ALAMAT"].ToString();
            spl.Telepon = dtr["TELEPON"] is DBNull ? string.Empty : dtr["TELEPON"].ToString();
            spl.Keterangan = dtr["KETERANGAN"] is DBNull ? string.Empty : dtr["KETERANGAN"].ToString();

            return spl;
        }
        public List<suplierModel> GetAllSuplier()
        {
            List<suplierModel> daftarSuplier = new List<suplierModel>();
            strsql = "SELECT * FROM suplier2 ORDER BY ID";
            using (FbCommand cmd = new FbCommand(strsql ,conn))
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

        public List<suplierModel> GetByNameSuplier(string nama)
        {
            List<suplierModel> daftarSuplier = new List<suplierModel>();
            strsql = @"SELECT * FROM suplier2 WHERE NAMA LIKE @1 OR ALAMAT LIKE @1  ORDER BY NAMA";

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
