using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestat_v3
{
    class ScoreClass
    {
        DBconnect connect = new DBconnect();
        StudentClass student = new StudentClass();
        //cream o functie pentru a insera media fiecarui elev
       public string MediaFinala(string id)
        {
           return student.exeCount("SELECT AVG(Media) FROM Score WHERE Id='" + id + "'");
        }
        public bool insertScore(string stdid, string courseName, double score, string desc)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Score(Id,Materia,Media,Observatii) VALUES (@stdid,@cn,@sco,@desc)", connect.getconnection);
            //@stdid,@cn,@sco,@desc
            command.Parameters.Add("@stdid", System.Data.SqlDbType.VarChar).Value = stdid;
            command.Parameters.Add("@cn", System.Data.SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@sco", System.Data.SqlDbType.Decimal).Value = score;
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
        //cream o functie pentru a obtine lista
        public DataTable getList(SqlCommand command)
        {
            command.Connection = connect.getconnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //cream o fucntie pentru a verifica daca exista o media la materia respectiva
        public bool checkScore(string stdId,string cName)
        {
           DataTable table = getList(new SqlCommand("SELECT * FROM Score WHERE Id='"+ stdId +"'AND Materia='"+cName+"'"));
            if(table.Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //cream o functie pentru a actualiza datele
        public bool updateScore(string stdid, string courseName, double score, string desc)
        {
            SqlCommand command = new SqlCommand("UPDATE Score SET Media=@sco,Observatii=@desc WHERE Id=@stdid AND Materia=@cn", connect.getconnection);
            //@stdid,@sco,@desc
            command.Parameters.Add("@stdid", System.Data.SqlDbType.VarChar).Value = stdid;
            command.Parameters.Add("@cn", System.Data.SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@sco", System.Data.SqlDbType.Decimal).Value = score;
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
        //cream o functie pentru a sterge o medie din baza de date
        public bool deleteScore(string id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Score WHERE Id=@id", connect.getconnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.VarChar).Value = id;
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
