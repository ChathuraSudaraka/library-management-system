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
using System.Security.Cryptography;
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
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900,
            Primary.Blue600, Primary.LightBlue100, Accent.LightBlue200, TextShade.WHITE);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            String name = materialTextBox1.Text;
            String password = materialTextBox2.Text;

            try
            {
                String connectionString = "server=localhost;database=library-management-system; uid = root; password = Same2u; ";
                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                String query = "SELECT * FROM users \r\nWHERE `name` = '" + name + "'\r\nAND `password` = '"+ password +"'";	
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // 1 = student, 2 = lecuturer, 3 = admin
                    String role = reader["role_id"].ToString();

                    if (role == "1")
                    {
                        student student = new student();
                        student.Show();

                    }
                    else if (role == "2")
                    {
                        lecturer lecturer = new lecturer();
                        lecturer.Show();

                    }
                    else if (role == "3")
                    {
                        admin admin = new admin();
                        admin.Show();

                    }
                    connection.Close();
                    reader.Close();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Success",
             MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
