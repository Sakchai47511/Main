using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjectProgram
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //ทำปุ่มระดับความยาก
        private void button1_Click(object sender, EventArgs e)
        {
            int diff = 1;
            Program.Win = false;
            Program.check = 1;
            Program.Form.startGame(diff);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int diff = 2;
            Program.Win = false;
            Program.check = 2;
            Program.Form.startGame(diff);
            Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int diff = 3;
            Program.Win = false;
            Program.check = 3;
            Program.Form.startGame(diff);
            Close();
        }
    }
}
