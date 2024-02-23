using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atestat_v3
{
    public partial class RegisterUserForm : Form
    {
        UserClass User = new UserClass();
        public RegisterUserForm()
        {
            InitializeComponent();
        }

        private void RegisterUserForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox_user.Text;
            string pass = textBox_password.Text;
            try
            {
                if(User.insertUser(user,pass))
                    {
                    
                    MessageBox.Show("Utilizator nou adaugat!", "Adaugare utilizator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
