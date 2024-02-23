using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_v3
{
    public partial class ManageStudentForm : Form
    {
        StudentClass student = new StudentClass();
        public ManageStudentForm()
        {
            InitializeComponent();
        }

        private void button_clean_Click(object sender, EventArgs e)
        {

        }

        private void ManageStudentForm_Load(object sender, EventArgs e)
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

        //Afisam datele din baza de date "Student" in textbox
        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            textBox_Id.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            textBox_Fname.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            textBox_Lname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Value = (DateTime)DataGridView_student.CurrentRow.Cells[3].Value;
            if (DataGridView_student.CurrentRow.Cells[4].Value.ToString() == "Masculin")
                radioButton_male.Checked = true;
            textBox_phone.Text = DataGridView_student.CurrentRow.Cells[5].Value.ToString();
            textBox_address.Text = DataGridView_student.CurrentRow.Cells[6].Value.ToString();
            byte[] img = (byte[])DataGridView_student.CurrentRow.Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox_student.Image = Image.FromStream(ms);
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_Fname.Clear();
            textBox_Lname.Clear();
            textBox_phone.Clear();
            textBox_address.Clear();
            textBox_Id.Clear();
            radioButton_male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null;
        }

        private void button_upload_Click(object sender, EventArgs e)
        {
            //cauta o fotografie in calculatorul nostru
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif) |*.jpg;*.png;*.gif ";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.searchStudent(textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        bool verify()
        {
            if ((textBox_Fname.Text == "") || (textBox_Lname.Text == "") || (textBox_phone.Text == "") || (textBox_address.Text == "") || (pictureBox_student.Image == null))
            {
                return false;
            }
            else
                return true;
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            //actualizam datele elevului
            string id = textBox_Id.Text;
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
                    if (student.updateStudent(id,fname, lname, bdate, gender, phone, address, img))
                    {
                        showTable();
                        MessageBox.Show("Datele elevului au fost actualizate", "Actualizare date elev", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)


                {
                    MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Camp gol", "Actualizare date elev", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridView_student_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView_student_Click_1(object sender, EventArgs e)
        {
            textBox_Id.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            textBox_Fname.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            textBox_Lname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Value = (DateTime)DataGridView_student.CurrentRow.Cells[3].Value;
            if (DataGridView_student.CurrentRow.Cells[4].Value.ToString() == "Masculin")
                radioButton_male.Checked = true;
            textBox_phone.Text = DataGridView_student.CurrentRow.Cells[5].Value.ToString();
            textBox_address.Text = DataGridView_student.CurrentRow.Cells[6].Value.ToString();
            byte[] img = (byte[])DataGridView_student.CurrentRow.Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox_student.Image = Image.FromStream(ms);
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
        }

        private void button_delete_Click_1(object sender, EventArgs e)
        {
             
            if (textBox_Id.Text.Equals(""))
            {
                MessageBox.Show("Id-ul materiei este invalid", "Camp incomplet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string id = textBox_Id.Text;
                    if (student.deleteCourse(id))
                    {
                        showTable();
                        button_clear.PerformClick();
                        MessageBox.Show("Elevul a fost eliminat din baza de date", "Stergere elev", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Stergere elev", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void DataGridView_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
