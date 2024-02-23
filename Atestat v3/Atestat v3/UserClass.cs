using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Atestat_v3
{
    class UserClass
    {
        DBconnect connect = new DBconnect();
        public bool insertUser(string user,string pass)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [User] (Username,Password) VALUES (@user,@pass)", connect.getconnection);
            command.Parameters.Add("@user", System.Data.SqlDbType.VarChar).Value = user;
            command.Parameters.Add("@pass", System.Data.SqlDbType.VarChar).Value = pass;

            connect.openConnect();
            if(command.ExecuteNonQuery()==1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        public string exeuserLoggedIn(string query)
        {
            SqlCommand command = new SqlCommand(query, connect.getconnection);
            connect.openConnect();
            string user = command.ExecuteScalar().ToString();
            connect.closeConnect();
            return user;
        }
        public string userLoggedIn(string uname)
        {
            return exeuserLoggedIn("SELECT * FROM [User] WHERE Username='" + uname + "'");
        }
        public string getUsername(string uname)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE Username='" + uname + "'", connect.getconnection);
            connect.openConnect();
            string Username = (string)command.ExecuteScalar();
            connect.closeConnect();
            return Username;
        }
    }
}
