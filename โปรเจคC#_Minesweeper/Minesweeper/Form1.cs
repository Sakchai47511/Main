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
    public partial class Form1 : Form
    {


        //ตัวแปร
        int Antbomber, width, height, X_start = 20, Y_start = 60;
        public Button[,] buttonPlan;
        public Label[,] labelPlan;
        Label label4 = new Label();

        bool markBomber = false;
        bool Win = false;
        bool Lose = false;
        int Antbombertext;

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=stock;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        public Form1()
        {
            InitializeComponent();
            
        }

        
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //การคลิกซ้าย

        public void LeftClick(object sender, System.EventArgs e)
        {
            Button buttonClick = sender as Button;

            if (markBomber)
            {
                if (buttonClick.Text == "X")
                {
                    buttonClick.Text = "";
                    Antbombertext++;
                }
                else
                {
                    buttonClick.Text = "X";
                    Antbombertext--;
                }
            }
            if (markBomber == false && buttonClick.Text != "X")
            {
                buttonClick.Visible = false;
                //เริ่มตัวจับเวลา
                if (!timer1.Enabled)
                    timer1.Enabled = true;

                //ตำแหน่งของการคลิกและเปรียบเทียบกับตำแหน่งของปุ่มจากนั้นตั้งค่าเป็น x, y
                int x = 0;
                int y = 0;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if ((buttonClick.Location.X == buttonPlan[i, j].Location.X && buttonClick.Location.Y == buttonPlan[i, j].Location.Y))
                        {
                            x = i;
                            y = j;
                        }
                    }
                }

                //ใช้ x และ y จากด้านบนเพื่อตรวจสอบว่ามีระเบิดที่ผู้เล่นคลิกหรือไม่
                if (labelPlan[x, y].Text == "*")
                {
                    timer1.Enabled = false;
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            buttonPlan[i, j].Visible = false;

                        }
                    }
                    button1.Text = (":(");
                }
                clearEmpty();
            }
        }


        //หากกล่องมีระเบิด 0 ลูกจะเอากล่องที่ว่างรอบๆนั้นออกไป
        public void clearEmpty()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (labelPlan[i, j].Text == " " && !buttonPlan[i, j].Visible)
                    {
                        clearEmptyHelp(i, j);
                    }
                }
            }
        }
        //ทำให้ดูง่ายขึ้นโดยกล่องที่กดจะลบขอบออกไป
        public void clearEmptyHelp(int x, int y)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i + x < width && j + y < height && i + x >= 0 && j + y >= 0)
                    {
                        if (labelPlan[i + x, j + y].Text != "X")
                        {
                            buttonPlan[x + i, y + j].Visible = false;

                        }
                    }
                }
            }
        }

        //เปิดหน้าต่างใหม่เพื่อเลือกระดับความยาก
        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 Start = new Form2();
            Start.Show();
        }

        //การเริ่มเกมจากระดับความยาก
        public void startGame(int diff)
        {
            label4.Visible = false;
            Win = false;
            clearPlan();

            if (diff == 1)
            {
                width = 10;
                height = 10;
                Antbomber = 15;
            }
            if (diff == 2)
            {
                width = 20;
                height = 20;
                Antbomber = 75;
            }
            if (diff == 3)
            {
                width = 40;
                height = 20;
                Antbomber = 145;
            }
            //ปรับขนาดหน้าต่าง
            Width = (X_start + 9) * 2 + width * 22;
            Height = Y_start * 2 + height * 22;

            createPlan();
        }
        //ปรับรูปแบบตัวเลขในกล่อง
        public Label createLabels(int x, int y)
        {
            Label label = new Label();
            Controls.AddRange(new System.Windows.Forms.Control[] { label, });
            label.Size = new System.Drawing.Size(22, 22);
            label.Location = new System.Drawing.Point(x, y);
            label.Text = " ";

            label.TextAlign = ContentAlignment.TopCenter;
            label.AutoSize = false;
            label.Font = new Font("Times new Roman", 18f);

            return label;
        }

        //แม่แบบสำหรับปุ่มทั้งหมด
        public Button createButton(int x, int y)
        {
            Button click = new Button();
            Controls.AddRange(new System.Windows.Forms.Control[] { click, });
            click.Size = new System.Drawing.Size(22, 22);
            click.Location = new System.Drawing.Point(x, y);

            click.Click += new System.EventHandler(LeftClick);

            return click;
        }


        //สร้างป้ายกำกับปุ่มระเบิดและทั้งหมด
        public void createPlan()
        {
            Random random = new Random();


            Antbombertext = Antbomber;
            label1.Text = ("Time: 0");
            label2.Text = ("Bombs: " + Antbombertext.ToString());
            button1.Text = (":)");

            buttonPlan = new Button[width, height];
            labelPlan = new Label[width, height];
            time = 0;

            button1.Location = new System.Drawing.Point((Width / 2) - 22, 0);
            button2.Location = new System.Drawing.Point((Width / 2) + 25, 0);

            //แก้ไขปุ่มและป้ายกำกับ
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    buttonPlan[i, j] = createButton(X_start + 22 * i, Y_start + 22 * j);
                    labelPlan[i, j] = createLabels(X_start + 22 * i, Y_start + 22 * j);
                }
            }

            //แก้ไขปรับแต่งระเบิด
            for (int i = 0; i < Antbomber; i++)
            {
                int bombX = random.Next(0, width);
                int bombY = random.Next(0, height);

                if (labelPlan[bombX, bombY].Text == "*")
                {
                    i--;
                }
                if (labelPlan[bombX, bombY].Text != "*")
                {
                    labelPlan[bombX, bombY].Text = "*";
                }
            }

            countBomber(width, height);
        }

        public int minorRunt = 0;

        //ตรวจสอบจำนวนระเบิดที่อยู่รอบ ๆ แต่ละกล่อง
        public void countBomber(int x, int y)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (labelPlan[i, j].Text != "*")
                    {
                        countBomberHelp(i, j);
                        if (minorRunt > 0)
                        {
                            labelPlan[i, j].Text = minorRunt.ToString();
                        }
                        minorRunt = 0;
                    }
                }
            }
        }

        //ตัวช่วยที่จะมาช่วยนับระเบิด
        public void countBomberHelp(int x, int y)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i + x < width && j + y < height && i + x >= 0 && j + y >= 0)
                    {
                        if (labelPlan[x + i, y + j].Text == "*")
                        {
                            minorRunt++;
                        }
                    }
                }
            }
        }
        private int time = 0;

        //ตัวแสดงเวลาและจำนวนระเบิด
        private void timer1_Tick(object sender, EventArgs e)
        {
            int quantity = 0;
            time++;
            label1.Text = ("Time: " + time.ToString());
            label2.Text = ("Bombs: " + Antbombertext.ToString());

            if (Antbombertext == 0)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (buttonPlan[i, j].Text != "*" && !buttonPlan[i, j].Visible)
                        {
                            quantity++;
                        }
                    }
                }

            }

            if (Antbombertext == 0 && (quantity == (width * height) - Antbomber))
            {
                Win = true;
                timer1.Enabled = false;
            }

            if (Win)
            {
                Controls.AddRange(new System.Windows.Forms.Control[] { label4, });
                label4.Location = new System.Drawing.Point(Width / 2 - 50, Height / 2-20);
                label4.Text = "You won!!";
                Form4 FM = new Form4();
                FM.Show();
                Program.pltime = time.ToString();
                string stat = FM.stat;
                if(stat == "1")
                {
                    clearPlan();
                    label4.Show();
                }



            }
        }


       
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 Start = new Form5();
            Start.Show();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Start = new Form3();
            Start.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //สร้างปุ่มรีสตาร์ท ":)"
        private void button1_Click(object sender, EventArgs e)
        {
            Win = false;
            label4.Visible = false;
            timer1.Enabled = false;
            clearPlan();
            createPlan();

        }

        //ล้างทั้งหมด
        public void clearPlan()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Controls.Remove(buttonPlan[i, j]);
                    Controls.Remove(labelPlan[i, j]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (markBomber == false)
                markBomber = true;
            else
                markBomber = false;
        }
    }
}
