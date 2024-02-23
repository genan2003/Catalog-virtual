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
    public partial class PrintScoreForm : Form
    {
        ScoreClass Score = new ScoreClass();
        CourseClass course = new CourseClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintScoreForm()
        {
            InitializeComponent();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //pentru a cauta in baza de date si a afisa datele in datagridview
            DataGridView_score.DataSource = course.getCourse(new SqlCommand("SELECT Score.Id,Student.[Numele de familie],Student.Prenume,Score.Materia,Score.Media,Score.Observatii FROM Student INNER JOIN Score ON Score.Id=student.Id WHERE CONCAT(Student.[Numele de familie],Student.Prenume,Score.Materia) LIKE '%" + textBox_search.Text + "%' "));
            textBox_search.Clear();
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            //vom avea nevoie de DGVprinter helper pentru a printa fisiere pdf
            printer.Title = "Lista medii";
            printer.SubTitle = string.Format("Data: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_score);
        }

        private void PrintScoreForm_Load(object sender, EventArgs e)
        {
            showScore();
        }
        public void showScore()
        {
            DataGridView_score.DataSource = Score.getList(new SqlCommand("SELECT Score.Id,Student.[Numele de familie],Student.Prenume,Score.Materia,Score.Media,Score.Observatii FROM Student INNER JOIN Score ON Score.Id=student.Id"));
        }
    }
}
