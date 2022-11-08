using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kasirSederhana.Model;
using FirebirdSql.Data.FirebirdClient;

namespace kasirSederhana.Dao
{
    public class kategoriDao
    {
        private FbConnection conn;
        private string strsql = string.Empty;

        public kategoriDao(FbConnection conn)
        {
            this.conn = conn;
        }

        //Query Tambah 
        public int Tambah(kategoriModel spl)
        {
            strsql = @"INSERT INTO kategori  VALUES (gen_id(gen_satuan_id, 1), @1,@2)";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", spl.Nama);
                cmd.Parameters.AddWithValue("@2", spl.Keterangan);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }

        //Query Update
        public int Update(kategoriModel spl)
        {
            strsql = @"Update kategori set 
                        NAMA = @1,
                        KETERANGAN = @2
                        Where ID = @5";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", spl.Nama);
                cmd.Parameters.AddWithValue("@2", spl.Keterangan);
                cmd.Parameters.AddWithValue("@5", spl.Id);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
        //Query Hapus
        public int Hapus(int Id)
        {
            strsql = "DELETE FROM kategori WHERE ID = @5";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@5", Id);

                return cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        private kategoriModel MappingRowToObject(FbDataReader dtr)
        {
            kategoriModel sat = new kategoriModel();
            sat.Id = int.Parse(dtr["ID"] is DBNull ? string.Empty : dtr["ID"].ToString());
            sat.Nama = dtr["NAMA"] is DBNull ? string.Empty : dtr["NAMA"].ToString();
            sat.Keterangan = dtr["KETERANGAN"] is DBNull ? string.Empty : dtr["KETERANGAN"].ToString();

            return sat;
        }
        //GET ALL DATA
        public List<kategoriModel> GetAll()
        {
            List<kategoriModel> dftrSatuan = new List<kategoriModel>();
            strsql = "SELECT * FROM kategori ORDER BY ID DESC";
            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                using (FbDataReader dtr = cmd.ExecuteReader())
                {

                    while (dtr.Read())
                    {
                        dftrSatuan.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return dftrSatuan;
        }
        public List<kategoriModel> GetByName(string nama)
        {
            List<kategoriModel> dftrSatuan = new List<kategoriModel>();
            strsql = @"SELECT * FROM kategori WHERE NAMA LIKE @1 OR KETERANGAN LIKE @1  ORDER BY NAMA";

            using (FbCommand cmd = new FbCommand(strsql, conn))
            {
                cmd.Parameters.AddWithValue("@1", "%" + nama + "%");
                using (FbDataReader dtr = cmd.ExecuteReader())
                {
                    while (dtr.Read())
                    {
                        dftrSatuan.Add(MappingRowToObject(dtr));
                    }
                }
            }
            return dftrSatuan;
        }
    }
}
