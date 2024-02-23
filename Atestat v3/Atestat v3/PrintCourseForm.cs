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
using DGVPrinterHelper;

namespace Atestat_v3
{
    public partial class PrintCourseForm : Form
    {
        CourseClass course = new CourseClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintCourseForm()
        {
            InitializeComponent();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //pentru a cauta materia si o afisa in datagridview
            DataGridView_course.DataSource = course.getCourse(new SqlCommand("SELECT * FROM Course WHERE CONCAT([Numele materiei],Descriere)LIKE '%" + textBox_search.Text + "%'"));
            textBox_search.Clear();
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            //vom avea nevoie de DGVprinter helper pentru a printa fisiere pdf
            printer.Title = "Lista materii";
            printer.SubTitle = string.Format("Data: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_course);
        }

        private void DataGridView_course_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PrintCourseForm_Load(object sender, EventArgs e)
        {
            DataGridView_course.DataSource = course.getCourse(new SqlCommand("SELECT * FROM Course"));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
