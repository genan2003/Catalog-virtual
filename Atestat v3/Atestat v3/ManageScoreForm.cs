using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_v3
{
    public partial class ManageScoreForm : Form
    {
        CourseClass course = new CourseClass();
        ScoreClass Score = new ScoreClass();
        public ManageScoreForm()
        {
            InitializeComponent();
        }

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            //populam combobox-ul cu materii
            comboBox_course.DataSource = course.getCourse(new SqlCommand("SELECT * FROM Course"));
            comboBox_course.DisplayMember = "Numele materiei";
            comboBox_course.ValueMember = "Numele materiei";
            //pentru a afisa mediile in datagridview
            showScore();
        }
        public void showScore()
        {
            DataGridView_course.DataSource = Score.getList(new SqlCommand("SELECT Score.Id,Student.[Numele de familie],Student.Prenume,Score.Materia,Score.Media,Score.Observatii FROM Student INNER JOIN Score ON Score.Id=student.Id"));
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Datele necesare pentru adaugarea materiei sunt incomplete", "Camp incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string stdid = textBox_stdId.Text;
                string cName = comboBox_course.Text;
                double score = Convert.ToDouble(textBox_score.Text);
                string desc = textBox_description.Text;
                    if (Score.updateScore(stdid,cName ,score, desc))
                    {
                        showScore();
                        button_clear.PerformClick();
                        MessageBox.Show("Media a fost actualizata", "Actualizare medie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Media nu a putut fi actualizata", "Actualizare medie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if(textBox_stdId.Text=="")
            {
                MessageBox.Show("Eroare de camp- este necesar sa completati id-ul elevului", "Stergere medie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string id = Convert.ToString(textBox_stdId.Text);
                if(Score.deleteScore(id))
                {
                    showScore();
                    MessageBox.Show("Media a fost eliminata", "Stergere medie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_clear.PerformClick();
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_description.Clear();
            textBox_score.Clear();
            textBox_search.Clear();
        }

        private void DataGridView_course_Click(object sender, EventArgs e)
        {
            textBox_stdId.Text = DataGridView_course.CurrentRow.Cells[0].Value.ToString();
            comboBox_course.Text= DataGridView_course.CurrentRow.Cells[3].Value.ToString();
            textBox_score.Text= DataGridView_course.CurrentRow.Cells[4].Value.ToString();
            textBox_description.Text= DataGridView_course.CurrentRow.Cells[5].Value.ToString();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //pentru a cauta in baza de date si a afisa datele in datagridview
            DataGridView_course.DataSource = course.getCourse(new SqlCommand("SELECT Score.Id,Student.[Numele de familie],Student.Prenume,Score.Materia,Score.Media,Score.Observatii FROM Student INNER JOIN Score ON Score.Id=student.Id WHERE CONCAT(Student.[Numele de familie],Student.Prenume,Score.Materia) LIKE '%"+textBox_search.Text+"%' "));
            textBox_search.Clear();
        }
    }
}
