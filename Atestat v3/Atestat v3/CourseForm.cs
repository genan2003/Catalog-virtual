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
    public partial class CourseForm : Form
    {
        CourseClass course = new CourseClass();
        public CourseForm()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if(textBox_Cname.Text=="" || textBox_Chour.Text=="")
            {
                MessageBox.Show("Datele necesare pentru adaugarea materiei sunt incomplete", "Camp incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            { 
            string cName = textBox_Cname.Text;
            int chr = Convert.ToInt32(textBox_Chour.Text);
            string desc = textBox_description.Text;

            if(course.insertCourse(cName,chr,desc))
            {
                    showData();
                    button_clear.PerformClick();
                MessageBox.Show("Materie noua adaugata", "Adaugare materie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Materia nu a putut fi adaugata", "Adaugare materie", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_Cname.Clear();
            textBox_Chour.Clear();
            textBox_description.Clear();
        }

        private void CourseForm_Load(object sender, EventArgs e)
        {
            showData();
        }
        private void showData()
        {
            //pentru a afisa lista in datagridview
            DataGridView_course.DataSource = course.getCourse(new SqlCommand("SELECT * FROM Course"));
        }
    }
}
