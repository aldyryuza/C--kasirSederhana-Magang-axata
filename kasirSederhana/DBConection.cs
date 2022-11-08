using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;

namespace kasirSederhana
{
    public class DBConection
    {
        private FbConnection conn = null;
        private static DBConection dbConn = null;
        //constructor
        private DBConection()
        {
            if (conn == null)
            {
                //
                string server = ConfigurationSettings.AppSettings["Server"];
                string database = ConfigurationSettings.AppSettings["Database"];
                string db_filepath = @"D:\DATAKU\KERJA\AXATA-TECHNOLOGY\MAGANG\BULAN1\hari7 mulai project\PROJECT APLIKASI KASIR\DBKASIR.FDB";
                string user = "SYSDBA";
                string password = "masterkey";

                string strConn = "Database=" + db_filepath + ";" +
            "User=" + user + ";" +
            "Password=" + password + ";" +
            "Dialect= 3;" +
            "ServerType=0;";

                conn = new FbConnection(strConn);
                conn.Open();

            }
        }
        public static DBConection GetInstance()
        {
            if (dbConn == null)
            {
                dbConn = new DBConection();
            }
            return dbConn;
        }
        public FbConnection GetConnection()
        {
            return this.conn;
        }
    }
}
