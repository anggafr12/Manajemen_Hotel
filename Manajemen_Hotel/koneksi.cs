using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Manajemen_Hotel
{
    class koneksi
    {
        public SqlConnection GetConn()
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = "Data source=DESKTOP-DL0NFGV\\ANGGA;initial catalog=db_hotel;integrated security=true";
            return Conn;
        }
    }
}
