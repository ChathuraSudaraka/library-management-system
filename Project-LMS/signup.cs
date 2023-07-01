using MaterialSkin;
using MaterialSkin.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_LMS
{
    public partial class signup : MaterialForm
    {
        private MySqlConnection connection;
        private string connectionString = "server=localhost;database=library-management-system;uid=root;password=Same2u;";

        public signup()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue600, Primary.LightBlue100, Accent.LightBlue200, TextShade.WHITE);
        }

        private void signup_Load(object sender, EventArgs e)
        {

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string name = materialTextBox1.Text;
            string email = materialTextBox4.Text;
            string password = materialTextBox3.Text;
            string mobile = materialTextBox2.Text;
            string address = materialTextBox5.Text;
            string role = materialComboBox1.SelectedItem?.ToString();
            string gender = materialComboBox3.SelectedItem?.ToString();
            string city = materialComboBox2.SelectedItem?.ToString();

            try
            {
                using (connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO users (`name`, `email`, `password`, `mobile`, `address`, `role_id`, `gender_id`, `city_id`) " +
                        "VALUES (@name, @email, @password, @mobile, @address, @role, @gender, @city)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@mobile", mobile);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@city", city);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration Successful!");
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Registration Failed!");
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void ClearFields()
        {
            materialTextBox1.Clear();
            materialTextBox2.Clear();
            materialTextBox3.Clear();
            materialTextBox4.Clear();
            materialTextBox5.Clear();
            materialComboBox1.SelectedIndex = -1;
            materialComboBox3.SelectedIndex = -1;
            materialComboBox2.SelectedIndex = -1;
        }
    }
}