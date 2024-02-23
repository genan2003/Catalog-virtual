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
using DGVPrinterHelper;

namespace Atestat_v3
{
    public partial class PrintStudent : Form
    {
        StudentClass student = new StudentClass();
        DGVPrinter printer = new DGVPrinter();
        public PrintStudent()
        {
            InitializeComponent();
        }

        private void PrintStudent_Load(object sender, EventArgs e)
        {
            showData(new SqlCommand("SELECT * FROM Student"));
        }

        //cream o functie pentru a afisa elevii in DataGridView
        public void showData(SqlCommand command)
        {
            DataGridView1_student.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            DataGridView1_student.DataSource = student.getList(command);
            //coloana 7 este coloana cu imaginea 
            imageColumn = (DataGridViewImageColumn)DataGridView1_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            //prima data se bifeaza radiobutton-ul
            string selectQuery;
            if(radioButton_all.Checked)
            {
                selectQuery = "SELECT * FROM Student";
            }
            else if(radioButton_male.Checked)
            {
                selectQuery= "SELECT * FROM Student WHERE Sex='Masculin'";
            }
            else
            {
                selectQuery = "SELECT * FROM Student WHERE Sex='Feminin'";
            }
            showData(new SqlCommand(selectQuery));

        }

        private void button_print_Click(object sender, EventArgs e)
        {
            //vom avea nevoie de DGVprinter helper pentru a printa fisiere pdf
            printer.Title = "Lista elevi";
            printer.SubTitle = string.Format("Data: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView1_student);
        }
    }
}
