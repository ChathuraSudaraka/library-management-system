using MaterialSkin;
using MaterialSkin.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
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
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue600, Primary.LightBlue100, Accent.LightBlue200, TextShade.WHITE);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string name = materialTextBox1.Text;
            string password = materialTextBox2.Text;

            try
            {
                string connectionString = "server=localhost;database=library-management-system;uid=root;password=Same2u;";
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                String query = "SELECT * FROM users where name = 'name' and password = 'password'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["role"].ToString();

                    if (role == "admin")
                    {
                        admin admin = new admin();
                        admin.Show();

                    }
                    else if (role == "student")
                    {
                        student student = new student();
                        student.Show();

                    }
                    else if (role == "lecturer")
                    {
                        lecturer lecturer = new lecturer();
                        lecturer.Show();

                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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
