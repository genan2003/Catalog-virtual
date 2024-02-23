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
    public partial class LoginForm : Form
    {
        StudentClass student = new StudentClass();
        UserClass UserClass = new UserClass();
        public LoginForm()
        {
            InitializeComponent();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Red;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Transparent;
        }

        public void button_login_Click(object sender, EventArgs e)
        {
            if (textBox_username.Text == "" || textBox_password.Text == "")
            {
                MessageBox.Show("Datele de autentificare sunt incomplete!", "Eroare de autentificare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string uname = textBox_username.Text;
                string pass = textBox_password.Text;
                DataTable table = student.getList(new SqlCommand("SELECT * FROM [User] WHERE Username='" + uname + "' AND Password='" + pass + "'"));
                if (table.Rows.Count > 0)
                {

                    MainForm main = new MainForm();
                    this.Hide();
                    main.label_user.Text = textBox_username.Text;
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Numele de utilizator sau parola sunt incorecte!", "Eroare de autentificare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }
        private void Register_button_Click(object sender, EventArgs e)
        {
            RegisterUserForm reg = new RegisterUserForm();
            reg.Show();
        }

        public void textBox_username_TextChanged(object sender, EventArgs e)
        {

        }
        public string getUserName()
        {
            return textBox_username.Text;
        }
    }
}
