using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjektProgram
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        //ทำปุ่มระดับความยาก
        private void button1_Click(object sender, EventArgs e)
        {
            int diff = 1;
          
            Program.Form.startGame(diff);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int diff = 2;
          
            Program.Form.startGame(diff);
            Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int diff = 3;

          
            Program.Form.startGame(diff);
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
