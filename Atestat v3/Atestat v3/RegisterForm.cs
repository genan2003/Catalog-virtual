using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Atestat_v3
{
    public partial class RegisterForm : Form
    {
        StudentClass student = new StudentClass();
        public RegisterForm()
        {
            InitializeComponent();
        }
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            showTable();
        }
        //pentru a afisa lista cu elevii in DatagridView
        public void showTable()
        {
            DataGridView_student.DataSource = student.getStudentlist(new SqlCommand("SELECT * FROM Student"));
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            //cauta o fotografie in calculatorul nostru
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif) |*.jpg;*.png;*.gif ";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_Fname.Clear();
            textBox_Lname.Clear();
            textBox_phone.Clear();
            textBox_address.Clear();
            radioButton_male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null;
        }
        //cream o functie pentru a verifica

        bool verify()
        {
            if ((textBox_Fname.Text == "") || (textBox_Lname.Text == "") || (textBox_phone.Text == "") || (textBox_address.Text == "") || (pictureBox_student.Image == null))
            {
                return false;
            }
            else
                return true;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            //adauga elevi noi
            string id = textBox_id.Text;
            string fname = textBox_Fname.Text;
            string lname = textBox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textBox_phone.Text;
            string address = textBox_address.Text;
            string gender = radioButton_male.Checked ? "Masculin" : "Feminin";


            //trebuie sa verificam daca varsta elevului este intre 10 si 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 12 || (this_year - born_year) > 100)
            {
                MessageBox.Show("Elevul trebuie sa aibe varsta cuprinsa intre 12 si 100 de ani", "Varsta invalida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    //pentru a obtine poza din picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    if (student.insertStudent(id,fname, lname, bdate, gender, phone, address, img))
                    {
                        showTable();
                        MessageBox.Show("Elev nou adaugat", "Adaugare elev", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)


                {
                    MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Camp gol", "Adaugare elev", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridView_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
