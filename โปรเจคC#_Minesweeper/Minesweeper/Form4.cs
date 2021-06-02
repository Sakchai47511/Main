using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjektProgram
{
    public partial class Form4 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        public string stat;
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string pltime = Convert.ToString(Program.pltime);
            MessageBox.Show("Saved!!");
            MySqlConnection con = databaseConnection();
            MySqlCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = $"INSERT INTO som(Name,Time) VALUES (\"{textBox1.Text}\",@time)";
            cmd.Parameters.AddWithValue("@time", pltime);
            cmd.ExecuteNonQuery();
            con.Close();
            this.Hide();
            stat = "1";
        }
    }
}
