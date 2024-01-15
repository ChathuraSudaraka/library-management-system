using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;
using System.Reflection;
using System.Security.Cryptography;

namespace Project_LMS
{
    public partial class student : MaterialForm
    {
        private static MySqlConnection connection;

        public student()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue600,
            Primary.LightBlue100, Accent.LightBlue200, TextShade.WHITE);

            this.LoadBooks();
        }

        public void LoadBooks(String name = "")
        {
            try
            {
                materialListView1.Items.Clear();
                string connectionString = "server=localhost;database=library-management-system; uid = root; password = Same2u; ";

                connection = new MySqlConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                String query = "SELECT books.id, isAvailable, titles.title AS title, authors.name AS author "
                 + "FROM books "
                 + "INNER JOIN authors ON books.authors_id = authors.id "
                 + "INNER JOIN titles ON books.titles_id = titles.id "
                 + "WHERE titles.title LIKE '%" + name + "%' "
                 + "ORDER BY title ASC";

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
        private void materialButton2_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }
        private void materialButton1_Click(object sender, EventArgs e)
        {
            this.LoadBooks(materialTextBox1.Text);

        }
    }
}
