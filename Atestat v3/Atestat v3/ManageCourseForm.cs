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
    public partial class ManageCourseForm : Form
    {
        CourseClass course = new CourseClass();
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            showData();
        }
        //afisam data din tabelul cu materii
        private void showData()
        {
            //pentru a afisa lista in datagridview
            DataGridView_course.DataSource = course.getCourse(new SqlCommand("SELECT * FROM Course"));
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_Cname.Clear();
            textBox_Chour.Clear();
            textBox_description.Clear();
            textBox_id.Clear();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (textBox_Cname.Text == "" || textBox_Chour.Text == "" || textBox_id.Text.Equals(""))
            {
                MessageBox.Show("Datele necesare pentru adaugarea materiei sunt incomplete", "Camp incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int id = Convert.ToInt32(textBox_id.Text);
                string cName = textBox_Cname.Text;
                int chr = Convert.ToInt32(textBox_Chour.Text);
                string desc = textBox_description.Text;

                if (course.updateCourse(id,cName, chr, desc))
                {
                    showData();
                    button_clear.PerformClick();
                    MessageBox.Show("Datele au fost actualizate", "Actualizare materie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Materia nu a putut fi actualizata", "Actualizare materie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id.Text.Equals(""))
            {
                MessageBox.Show("Id-ul materiei este invalid", "Camp incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(textBox_id.Text);
                    if (course.deleteCourse(id))
                    {
                        showData();
                        button_clear.PerformClick();
                        MessageBox.Show("Materia a fost eliminata", "Stergere materie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Stergere materie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridView_course_Click(object sender, EventArgs e)
        {
            textBox_id.Text = DataGridView_course.CurrentRow.Cells[0].Value.ToString();
            textBox_Cname.Text= DataGridView_course.CurrentRow.Cells[1].Value.ToString();
            textBox_Chour.Text= DataGridView_course.CurrentRow.Cells[2].Value.ToString();
            textBox_description.Text= DataGridView_course.CurrentRow.Cells[3].Value.ToString();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //pentru a cauta materia si o afisa in datagridview
            DataGridView_course.DataSource = course.getCourse(new SqlCommand("SELECT * FROM Course WHERE CONCAT([Numele materiei],Descriere)LIKE '%" + textBox_search.Text + "%'"));
            textBox_search.Clear();

        }
    }
}
