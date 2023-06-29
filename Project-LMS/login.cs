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

            string username = materialTextBox1.Text;
            string password = materialTextBox2.Text;

            try
            {
                string connectionString = "server=localhost;user id=root;database=library-management-system;password=Same2u;";
                MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();
                string query = "Select * from users where username = 'username' and password = 'password'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dr = cmd.ExecuteReader();

                if (username == "admin" && password == "admin")
                {
                    admin admin = new admin();
                    admin.Show();
                    this.Hide();
                }
                else if (username == "lecturer" && password == "lecturer")
                {
                    lecturer lecturer = new lecturer();
                    lecturer.Show();
                    this.Hide();
                }
                else if (username == "student" && password == "student")
                {
                    student student = new student();
                    student.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }

            catch
            {

            }
        }
    }
}
