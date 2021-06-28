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

namespace ProjectProgram
{
    public partial class Form4 : Form
    {
        string[] AQ = { "Time_EZ", "Time_Med", "Time_Hard" };

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
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM score01 WHERE Name=\"{textBox1.Text}\""; //จากชื่อที่พิมพ์
            MySqlDataReader row = cmd.ExecuteReader();
            if (row.HasRows)
            {
                while (row.Read())
                {
                    if (row.GetString(Program.check) != "") //เช็ค
                    {
                        if (int.Parse(row.GetString(Program.check)) > Program.time_form) //อัปเดทสกอร์ 
                        {
                            conn = databaseConnection();
                            conn.Open();
                            string sql = "UPDATE score01 SET " + AQ[Program.check - 1] + "='" + Program.time_form + "'WHERE Name='" + textBox1.Text + "'";
                            cmd = new MySqlCommand(sql, conn);
                            cmd.ExecuteReader();
                            conn.Close();
                            MessageBox.Show("Saved!!");
                            Program.quantity = 0;
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Saved!!");
                            Program.quantity = 0;
                            this.Hide();
                        }
                    }
                    else
                    {
                        conn = databaseConnection();
                        conn.Open();
                        string sql = "UPDATE score01 SET " + AQ[Program.check - 1] + "='" + Program.time_form + "'WHERE Name='" + textBox1.Text + "'";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteReader();
                        conn.Close();
                        MessageBox.Show("Saved!!");
                        Program.quantity = 0;
                        this.Hide();
                    }
                }
            }

            else
            {
                if (Program.check == 1)
                {
                    MySqlConnection con = databaseConnection();
                    con.Open();
                    string sql1 = "INSERT INTO score01(Name,Time_EZ,Time_Med,Time_Hard) VALUES('" + textBox1.Text + "','" + Program.time_form + "','','')";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, con);
                    cmd1.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Saved!!");
                    Program.quantity = 0;
                    this.Hide();
                }
                else if (Program.check == 2)
                {
                    MySqlConnection con = databaseConnection();
                    con.Open();
                    string sql1 = "INSERT INTO score01(Name,Time_EZ,Time_Med,Time_Hard) VALUES('" + textBox1.Text + "','','" + Program.time_form + "','')";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, con);
                    cmd1.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Saved!!");
                    Program.quantity = 0;
                    this.Hide();
                }
                else
                {
                    MySqlConnection con = databaseConnection();
                    con.Open();
                    string sql1 = "INSERT INTO score01(Name,Time_EZ,Time_Med,Time_Hard) VALUES('" + textBox1.Text + "','','','" + Program.time_form + "')";
                    MySqlCommand cmd1 = new MySqlCommand(sql1, con);
                    cmd1.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Saved!!");
                    Program.quantity = 0;
                    this.Hide();
                }
            }
        }

    }
}
