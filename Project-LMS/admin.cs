using MaterialSkin;
using MaterialSkin.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Project_LMS
{
    public partial class admin : MaterialForm
    {
        private string connectionString = "server=localhost;database=library-management-system;uid=root;password=Same2u;";
        private MySqlConnection connection;
        public admin()
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
                Role.Items.Add(reader["type"].ToString());
            }
            reader.Close();

            query = "SELECT * FROM cities";
            cmd = new MySqlCommand(query, connection);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                City.Items.Add(reader["name"].ToString());
            }
            reader.Close();

            this.LoadBooks();
            this.LoadUser();
        }

        /*======================= Load Books Start =======================*/
        public void LoadBooks(String name = "")
        {
            try
            {
                materialListView1.Items.Clear();
                string connectionString = "server=localhost;database=library-management-system; uid = root; password = Same2u; ";
                connection = new MySqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                String query = "SELECT books.id, isAvailable, titles.title AS title, authors.name AS author\r\nFROM books \r\nINNER JOIN authors ON books.authors_id = authors.id\r\nINNER JOIN titles ON books.titles_id = titles.id WHERE title LIKE '%" + name + "%' AND isDeleted = '0' ORDER BY title ASC";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["id"].ToString());
                    item.SubItems.Add(reader["title"].ToString());
                    item.SubItems.Add(reader["author"].ToString());
                    item.SubItems.Add(reader["isAvailable"].ToString());
                    materialListView1.Items.Add(item);
                }

                reader.Close();
                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            this.LoadBooks(materialTextBox1.Text);
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        /*======================= Load Books End =======================*/

        /*======================= Load User Start =======================*/

        public void LoadUser(string name = "")
        {
            try
            {
                materialListView2.Items.Clear();
                string connectionString = "server=localhost;database=library-management-system; uid = root; password = Same2u; ";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT users.*, genders.`type` AS gender, cities.name AS city, roles.type AS role FROM users " +
                    "INNER JOIN cities ON cities.id = users.city_id " +
                    "INNER JOIN genders ON genders.id = users.gender_id " +
                    "INNER JOIN roles ON roles.id = users.role_id " +
                    "WHERE users.name LIKE '%" + name + "%'";

                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["id"].ToString());
                            item.SubItems.Add(reader["name"].ToString());
                            item.SubItems.Add(reader["role"].ToString());
                            item.SubItems.Add(reader["gender"].ToString());
                            item.SubItems.Add(reader["city"].ToString());
                            materialListView2.Items.Add(item);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void materialButton7_Click(object sender, EventArgs e)
        {
            this.LoadUser(tbUserid.Text);
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            LoadUser();
        }

        /*======================= Load User End =======================*/

        /*======================= Add Books Start =======================*/
        private void materialButton8_Click(object sender, EventArgs e)
        {
            // Validate author and title inputs
            string author = tbAuthor.Text.Trim();
            string title = tbTitle.Text.Trim();

            if (string.IsNullOrWhiteSpace(author) || string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter both author and title.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connection.Open();

                // Validate if the author already exists
                int author_id;
                using (MySqlCommand cmd1 = new MySqlCommand("SELECT id FROM authors WHERE name = @author", connection))
                {
                    cmd1.Parameters.AddWithValue("@author", author);
                    object authorIdResult = cmd1.ExecuteScalar();

                    if (authorIdResult != null && int.TryParse(authorIdResult.ToString(), out author_id))
                    {
                        // Author exists, retrieve the id
                    }
                    else
                    {
                        // Author does not exist, insert into authors table
                        using (MySqlCommand cmdInsertAuthor = new MySqlCommand("INSERT INTO authors (name) VALUES (@author); SELECT LAST_INSERT_ID();", connection))
                        {
                            cmdInsertAuthor.Parameters.AddWithValue("@author", author);
                            author_id = Convert.ToInt32(cmdInsertAuthor.ExecuteScalar());
                        }
                    }
                }

                // Validate if the title already exists
                int title_id;
                using (MySqlCommand cmd2 = new MySqlCommand("SELECT id FROM titles WHERE title = @title", connection))
                {
                    cmd2.Parameters.AddWithValue("@title", title);
                    object titleIdResult = cmd2.ExecuteScalar();

                    if (titleIdResult != null && int.TryParse(titleIdResult.ToString(), out title_id))
                    {
                        // Title exists, retrieve the id
                    }
                    else
                    {
                        // Title does not exist, insert into titles table
                        using (MySqlCommand cmdInsertTitle = new MySqlCommand("INSERT INTO titles (title) VALUES (@title); SELECT LAST_INSERT_ID();", connection))
                        {
                            cmdInsertTitle.Parameters.AddWithValue("@title", title);
                            title_id = Convert.ToInt32(cmdInsertTitle.ExecuteScalar());
                        }
                    }
                }

                // Insert into books table
                using (MySqlCommand cmd3 = new MySqlCommand("INSERT INTO books (authors_id, titles_id, isAvailable) VALUES (@authors_id, @titles_id, 1); SELECT LAST_INSERT_ID();", connection))
                {
                    cmd3.Parameters.AddWithValue("@authors_id", author_id);
                    cmd3.Parameters.AddWithValue("@titles_id", title_id);
                    int book_id = Convert.ToInt32(cmd3.ExecuteScalar());

                    MessageBox.Show("Book added successfully with id: " + book_id, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }


        /*======================= Add Books End =======================*/

        private void materialButton9_Click(object sender, EventArgs e)
        {
            string userId = materialTextBox3.Text;
            string name = materialTextBox4.Text;
            string mobile = materialTextBox5.Text;
            string email = materialTextBox6.Text;
            string address = materialTextBox7.Text;
            // ...
            string city = City.SelectedItem?.ToString();
            string role = Role.SelectedItem?.ToString();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    if (role != null)
                    {
                        // Retrieve role_id
                        string roleIdQuery = "SELECT id FROM roles WHERE type = @role";
                        MySqlCommand roleIdCmd = new MySqlCommand(roleIdQuery, connection);
                        roleIdCmd.Parameters.AddWithValue("@role", role);
                        string roleId = roleIdCmd.ExecuteScalar()?.ToString();

                        if (roleId != null)
                        {
                            // Retrieve city_id
                            string cityIdQuery = "SELECT id FROM cities WHERE name = @city";
                            MySqlCommand cityIdCmd = new MySqlCommand(cityIdQuery, connection);
                            cityIdCmd.Parameters.AddWithValue("@city", city);
                            string cityId = cityIdCmd.ExecuteScalar()?.ToString();

                            if (cityId != null)
                            {
                                // Update the user's information
                                string query = "UPDATE users SET name = @name, email = @email, "
                                               + "mobile = @mobile, address = @address, "
                                               + "role_id = @role, city_id = @city "
                                               + "WHERE id = @userId";

                                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                                {
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Parameters.AddWithValue("@email", email);
                                    cmd.Parameters.AddWithValue("@mobile", mobile);
                                    cmd.Parameters.AddWithValue("@address", address);
                                    cmd.Parameters.AddWithValue("@role", roleId);
                                    cmd.Parameters.AddWithValue("@city", cityId);
                                    cmd.Parameters.AddWithValue("@userId", userId);

                                    int rowsAffected = cmd.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("User information updated successfully!", "Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("User information update failed!", "Error",
                                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Selected city not found. Please select a valid city.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selected role not found. Please select a valid role.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid role.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


            private void materialTextBox6_TextChanged(object sender, EventArgs e)
        {
            string id = materialTextBox3.Text;
            string query = "SELECT users.*, roles.`type` AS role, cities.name AS city FROM users INNER JOIN roles ON users.role_id = roles.id INNER JOIN cities ON users.city_id = cities.id WHERE users.id = @id";

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    materialTextBox4.Text = reader["name"].ToString();
                    materialTextBox5.Text = reader["mobile"].ToString();
                    materialTextBox6.Text = reader["email"].ToString();
                    materialTextBox7.Text = reader["address"].ToString();
                    City.SelectedItem = reader["city"].ToString();
                    Role.SelectedItem = reader["role"].ToString();

                    // Check for DBNull and null before setting ComboBox items
                    City.SelectedItem = reader["city"] != DBNull.Value ? reader["city"].ToString() : null;

                    // Check for null before setting ComboBox items
                    Role.SelectedItem = reader["role"] != DBNull.Value ? reader["role"].ToString() : null;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            delete delete = new delete();
            delete.Show();
        }

        private void materialButton5_Click_1(object sender, EventArgs e)
        {
            userremove userremove = new userremove();
            userremove.Show();
        }
    }
}
