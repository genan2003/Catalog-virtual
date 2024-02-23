using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Atestat_v3
{
    public partial class ScoreForm : Form
    {
        CourseClass course = new CourseClass();
        StudentClass student = new StudentClass();
        ScoreClass Score = new ScoreClass();
        public ScoreForm()
        {
            InitializeComponent();
        }

        private void textBox_Cname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //cream o functie pentru a afisa datele in datagridview
        private void showScore()
        {
            DataGridView_student.DataSource = Score.getList(new SqlCommand("SELECT Score.Id,Student.[Numele de familie],Student.Prenume,Score.Materia,Score.Media,Score.Observatii FROM Student INNER JOIN Score ON Score.Id=student.Id"));
        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            //populam combobox-ul cu materii
            comboBox_course.DataSource = course.getCourse(new SqlCommand("SELECT * FROM Course"));
            comboBox_course.DisplayMember = "Numele materiei";
            comboBox_course.ValueMember = "Numele materiei";
            //pentru a afisa media in datagridview
            //pentru a afisa lista cu elevii
            DataGridView_student.DataSource = student.getList(new SqlCommand("SELECT Id,[Numele de familie],Prenume FROM Student"));
        }

        private void button_add_Click(object sender, EventArgs e)
        {

            if (textBox_stdId.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Datele necesare pentru adaugarea materiei sunt incomplete", "Camp incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string stdid =textBox_stdId.Text;
                string cName = comboBox_course.Text;
                double score = Convert.ToDouble(textBox_score.Text);
                string desc = textBox_description.Text;
                if (!Score.checkScore(stdid, cName))
                {
                    if (Score.insertScore(stdid, cName, score, desc))
                    {
                        showScore();
                        button_clear.PerformClick();
                        MessageBox.Show("Media noua a fost inregistrata", "Adaugare medie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Media nu a putut fi inregistrata", "Adaugare medie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Exista deja o medie inregistrata pentru aceasta materie", "Adaugare medie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_score.Clear();
            comboBox_course.SelectedIndex = 0;
            textBox_description.Clear();
        }

        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_stdId.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
        }

        private void button_sStudent_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.getList(new SqlCommand("SELECT Id,[Numele de familie],Prenume FROM Student"));
        }

        private void button_sScore_Click(object sender, EventArgs e)
        {
            showScore();
        }
    }
}
