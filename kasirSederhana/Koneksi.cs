using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;

namespace kasirSederhana
{
    public class Koneksi
    {
        string db_filepath = @"D:\DATAKU\KERJA\AXATA-TECHNOLOGY\MAGANG\BULAN1\hari7 mulai project\PROJECT APLIKASI KASIR\DBKASIR.FDB";
        string user = "SYSDBA";
        string password = "masterkey";




        public FbConnection GetConn()
        {
            FbConnection Conn = new FbConnection();
            Conn.ConnectionString =
                "Database=" + db_filepath + ";" +
                "User=" + user + ";" +
                "Password=" + password + ";" +
                "Dialect= 3;" +
                "ServerType=0;";
            return Conn;
        }
    }
}
