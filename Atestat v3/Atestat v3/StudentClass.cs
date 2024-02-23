using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Atestat_v3
{
    class StudentClass
    {
        DBconnect connect = new DBconnect();
        //cream o functie pentru a adauga elevii in baza de date

        public bool insertStudent(string id,string fname,string lname,DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Student (Id,Prenume, [Numele de familie], [Data nasterii], Sex, Telefon, Adresa, Fotografie) VALUES (@id,@fn,@ln,@bd,@gd,@ph,@adr,@img)", connect.getconnection);

            //@fn,@ln,@bd,@gd,@ph,@adr,@img
            command.Parameters.Add("@id", System.Data.SqlDbType.VarChar).Value = id;
            command.Parameters.Add("@fn", System.Data.SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", System.Data.SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", System.Data.SqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", System.Data.SqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", System.Data.SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", System.Data.SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = img;

            connect.openConnect();
            if(command.ExecuteNonQuery() == 1)
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

        //pentru a vedea tabaelul cu elevii
        public DataTable getStudentlist(SqlCommand command)
        {
            command.Connection = connect.getconnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //cream o functie pentru a executa query-ul de numarare
        public string exeCount(string query)
        {
            SqlCommand command = new SqlCommand(query, connect.getconnection);
            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();
            return count;
        }
        //pentru a obtine totalul de elevi
        public string totalStudent()
        {
            return exeCount("SELECT COUNT(*) FROM Student");
        }
        //pentru a obtine numarul elevilor de sex masculin
        public string maleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM Student WHERE Sex='Masculin'");
        }
        //pentru a obtine numarul elevilor de sex feminin
        public string femaleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM Student WHERE Sex='Feminin'");
        }
        //cream o functie pentru a cauta un elev in baza de date(prenumele,numele de familie si adresa)
        public DataTable searchStudent(string searchdata)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE CONCAT(Prenume,[Numele de familie],Adresa) LIKE '%"+searchdata+"%'", connect.getconnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //cream o functie pentru a edita datele unui elev
        public bool updateStudent(string id,string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            SqlCommand command = new SqlCommand("UPDATE Student SET Prenume = @fn, [Numele de familie] = @ln, [Data nasterii] = @bd, Sex = @gd, Telefon = @ph, Adresa = @adr, Fotografie = @img WHERE Id=@id; ", connect.getconnection);

            //@fn,@ln,@bd,@gd,@ph,@adr,@img
            command.Parameters.Add("@id", System.Data.SqlDbType.VarChar).Value = id;
            command.Parameters.Add("@fn", System.Data.SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", System.Data.SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", System.Data.SqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", System.Data.SqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", System.Data.SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", System.Data.SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = img;

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
        //cream o functie pentru orice comanda a bazei de date studentdb
        public DataTable getList(SqlCommand command)
        {
            command.Connection = connect.getconnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool deleteCourse(string id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Student WHERE Id=@id", connect.getconnection);
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
        public DataTable getCourse(SqlCommand command)
        {
            command.Connection = connect.getconnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
