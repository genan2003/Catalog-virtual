using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Atestat_v3
{
    class CourseClass
    {
        DBconnect connect = new DBconnect();
        //cream o functrie pentru a insera materia in tabel
        public bool insertCourse(string cName,int hr,string desc)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Course([Numele materiei],[Ore pe saptamana],Descriere) VALUES (@cn,@ch,@desc)", connect.getconnection);
            //@cn,@ch,@desc
            command.Parameters.Add("@cn", System.Data.SqlDbType.VarChar).Value = cName;
            command.Parameters.Add("@ch", System.Data.SqlDbType.Int).Value = hr;
            command.Parameters.Add("@desc", System.Data.SqlDbType.VarChar).Value = desc;
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

        //cream o functie pentru a afisa o lista cu materiile din baza de date
        public DataTable getCourse(SqlCommand command)
        {
            command.Connection = connect.getconnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //cream o functie pentru a actualiza modificarile aduse materiei
        public bool updateCourse(int id,string cName, int hr, string desc)
        {
            SqlCommand command = new SqlCommand("UPDATE Course SET [Numele materiei]=@cn,[Ore pe saptamana]=@ch,Descriere=@desc WHERE Id=@id", connect.getconnection);
            //@id,@cn,@ch,@desc
            command.Parameters.Add("@id", System.Data.SqlDbType.VarChar).Value = id;
            command.Parameters.Add("@cn", System.Data.SqlDbType.VarChar).Value = cName;
            command.Parameters.Add("ch", System.Data.SqlDbType.Int).Value = hr;
            command.Parameters.Add("@desc", System.Data.SqlDbType.VarChar).Value = desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
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

        //cream o functie pentru a putea sterge o materie
        //vom folosi doar id-ul
        public bool deleteCourse(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Course WHERE Id=@id",connect.getconnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
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
    }
}
