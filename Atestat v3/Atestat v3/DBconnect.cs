using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Atestat_v3
{
    //In aceasta clasa vom realiza conexiunea dintre aplicatie si baza de date sql

    class DBconnect
    {
        //pentru a crea conexiunea
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\studentdb.mdf;Integrated Security=True;Connect Timeout=30");

        //pentru a obtine conexiunea
        public SqlConnection getconnection
        {
            get
            {
                return connect;
            }
        }

        //cream o functie pentru a deschide conexiunea cu baza de date
        public void openConnect()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        //cream o functie pentru a inchide conexiunea cu baza de date
        public void closeConnect()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }
    }
}
