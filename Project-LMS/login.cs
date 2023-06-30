using MaterialSkin;
using MaterialSkin.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_LMS
{
    public partial class login : MaterialForm
    {
        public login()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey800, Primary.Blue900, Primary.LightBlue100, Accent.LightBlue200, TextShade.WHITE);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string name = materialTextBox1.Text;
            string password = materialTextBox2.Text;

            try
            {
                string connectionString = "server=localhost;database=library-management-system;uid=root;password=Same2u;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "SELECT * FROM users where name = 'name' and password = 'password'";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
            }
            catch
            {

            }

            if (name == "" && password == "")
            {
                admin admin = new admin();
                admin.Show();
                this.Close();
            }
            else if (name == "" && password == "")
            {
                student student = new student();
                student.Show();
                this.Close();
            }
            else if (name == "" && password == "")
            {
                lecturer lecturer = new lecturer();
                lecturer.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login Faild");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            signup signup = new signup();
            signup.Show();
            this.Hide();
        }
    }

}
