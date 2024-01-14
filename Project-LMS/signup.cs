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
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue600,
            Primary.LightBlue100, Accent.LightBlue200, TextShade.WHITE);

            connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM roles";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                materialComboBox1.Items.Add(reader["type"].ToString());
            }
            reader.Close();

            query = "SELECT * FROM cities";
            cmd = new MySqlCommand(query, connection);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                materialComboBox2.Items.Add(reader["name"].ToString());
            }
            reader.Close();

            query = "SELECT * FROM genders";
            cmd = new MySqlCommand(query, connection);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                materialComboBox3.Items.Add(reader["type"].ToString());
            }
            reader.Close();

            connection.Close();
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

                    String roles = "SELECT * FROM roles WHERE type = '" + role + "'";
                    MySqlCommand cmd1 = new MySqlCommand(roles, connection);
                    MySqlDataReader reader1 = cmd1.ExecuteReader();
                    reader1.Read();
                    role = reader1["id"].ToString();
                    reader1.Close();

                    String cities = "SELECT * FROM cities WHERE name = '" + city + "'";
                    MySqlCommand cmd2 = new MySqlCommand(cities, connection);
                    MySqlDataReader reader2 = cmd2.ExecuteReader();
                    reader2.Read();
                    city = reader2["id"].ToString();
                    reader2.Close();

                    String genders = "SELECT * FROM genders WHERE type = '" + gender + "'";
                    MySqlCommand cmd3 = new MySqlCommand(genders, connection);
                    MySqlDataReader reader3 = cmd3.ExecuteReader();
                    reader3.Read();
                    gender = reader3["id"].ToString();
                    reader3.Close();

                    string query = "INSERT INTO users (`name`, `email`, `password`, `mobile`, `address`, `role_id`, `gender_id`, `city_id`) " + "VALUES(@name, @email,@password, @mobile, @address, @role, @gender, @city)"; 

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
                            MessageBox.Show("Registration successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Registration Failed!", "Success",
                            MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

        private void materialButton2_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Hide();
        }
    }
}
