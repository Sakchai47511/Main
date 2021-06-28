using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace ProjectProgram
{
    public partial class Form1 : Form
    {
        //ตัวแปร
        int thebomber;
        int width; //ความกว้าง
        int height; //ความสูง
        int X_start = 20; //ความกว้างหน้าต่าง คงที่
        int Y_start = 60; //ความสูงหน้าต่าง คงที่
        int thebombertext;

        public Button[,] buttonPlan;
        public Label[,] labelPlan;
        Label label4 = new Label();

        bool markBomber = false;

        public Form1()
        {
            InitializeComponent();
        }

        //สิ่งที่ต้องแสดงบน Form1
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            newGameToolStripMenuItem.Visible = false;
            menuStrip1.Visible = false;
        }

        //ปุ่ม start 
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 Start = new Form2();
            Start.Show();
        }

        //ปุ่ม statistic
        private void button4_Click(object sender, EventArgs e)
        {
            Form3 Start = new Form3();
            Start.Show();
        }

        //ปุ่ม Rules
        private void button5_Click(object sender, EventArgs e)
        {
            Form5 Start = new Form5();
            Start.Show();
        }

        // Label ตัวเลข,ตำแหน่งว่างเปล่า
        public Label createLabels(int x, int y)
        {
            Label label = new Label();
            Controls.AddRange(new System.Windows.Forms.Control[] { label, });
            label.Size = new System.Drawing.Size(22, 22);
            label.Location = new System.Drawing.Point(x, y);
            label.Text = " ";
            label.BackColor = Color.Pink;
            label.ForeColor = Color.Blue;
            label.TextAlign = ContentAlignment.TopCenter;
            label.AutoSize = false;
            label.Font = new Font("Fixedsys", 15f);

            return label;
        }

        //ปรับแต่ง button ตัวเลข,ตำแหน่งว่างเปล่า ที่อยู่บน Label
        public Button createButton(int x, int y)
        {
            Button click = new Button();
            Controls.AddRange(new System.Windows.Forms.Control[] { click, });
            click.Size = new System.Drawing.Size(22, 22);
            click.Location = new System.Drawing.Point(x, y);
            click.BackColor = Color.Transparent;
            click.Click += new System.EventHandler(LeftClick);

            return click;
        }

        //การเริ่มเกมหลังเลือกระดับความยาก
        public void startGame(int diff)
        {
            label4.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            pictureBox1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            newGameToolStripMenuItem.Visible = true;
            menuStrip1.Visible = true;

            clearPlan();
            //กำหนดจำนวนระเบิดและขนาดกระดาน
            if (diff == 1)
            {
                width = 10;
                height = 10;
                thebomber = 6;
            }
            if (diff == 2)
            {
                width = 20;
                height = 20;
                thebomber = 12;
            }
            if (diff == 3)
            {
                width = 30;
                height = 20;
                thebomber = 24;
            }

            //ปรับขนาดหน้าต่าง
            Width = (X_start + 9) * 2 + width * 22;
            Height = Y_start * 2 + height * 22;

            createPlan();
        }

        //ตัวแสดงเวลาและจำนวนระเบิด
        private void timer1_Tick(object sender, EventArgs e)
        {
            int quantity = 0;
            time++;
            label1.Text = ("Time : " + time.ToString() + "วืนาที");
            label2.Text = "Bombs :" + thebombertext.ToString();

            if (thebombertext == 0)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (buttonPlan[i, j].Text != "⚈" && !buttonPlan[i, j].Visible)
                        {
                            quantity++;
                        }
                    }
                }

            }

            if (thebombertext == 0 && (quantity == (width * height) - thebomber))
            {
                Program.Win = true;
                timer1.Enabled = false;

            }

            if (Program.Win == true)
            {
                Controls.AddRange(new System.Windows.Forms.Control[] { label4, });
                label4.Location = new System.Drawing.Point(Width / 2 - 50, Height / 2 - 20);
                label4.Text = "";
                label4.BackColor = Color.Transparent;
                Program.time_form = time;
                Form4 FM = new Form4();
                FM.Show();
                Program.pltime = time.ToString();
                clearPlan();
                label4.Show();


            }
        }

        //สร้างป้ายกำกับปุ่มระเบิดและทั้งหมด
        public void createPlan()
        {
            Random random = new Random();

            //Label แสดงเวลา และจำนวณระเบิด
            thebombertext = thebomber;
            label1.Text = ("Time : 0");
            label2.Text = "Bombs :" + thebombertext.ToString();

            buttonPlan = new Button[width, height];
            labelPlan = new Label[width, height];
            time = 0;

            button1.Location = new System.Drawing.Point((Width / 2) - 22, 0); //ตำแหน่งปุ่ม restart
            button2.Location = new System.Drawing.Point((Width / 2) + 25, 0); //ตำแหน่งปุ่ม Mark Bomb

            //ปุ่มและป้ายกำกับที่ต้องแสดงเมื่อเกม
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    buttonPlan[i, j] = createButton(X_start + 22 * i, Y_start + 22 * j);
                    labelPlan[i, j] = createLabels(X_start + 22 * i, Y_start + 22 * j);
                }
            }

            //แก้ไขปรับแต่ง Label ที่มีระเบิด
            for (int i = 0; i < thebomber; i++)
            {
                int bombX = random.Next(0, width);
                int bombY = random.Next(0, height);

                if (labelPlan[bombX, bombY].Text == "⚈")
                {
                    i--;
                }
                if (labelPlan[bombX, bombY].Text != "⚈")
                {
                    string Pom = "⚈";
                    labelPlan[bombX, bombY].ForeColor = Color.DimGray;
                    labelPlan[bombX, bombY].Text = $"{Pom}";
                    labelPlan[bombX, bombY].Font = new Font("Microsoft Sans Serif", 16f);
                    labelPlan[bombX, bombY].TextAlign = ContentAlignment.TopCenter;
                }
            }

            countBomber(width, height);
        }
        public int minorRunt = 0;

        //การคลิก
        public void LeftClick(object sender, System.EventArgs e)
        {
            Button buttonClick = sender as Button;
            //ตัว mark ระเบิด
            if (markBomber)
            {
                if (buttonClick.Text == "●")
                {
                    buttonClick.FlatStyle = FlatStyle.Standard;
                    buttonClick.Text = "";
                    thebombertext++; //เมื่อคลิกครั้งแรกจะแสดงตัว ● ขึ้นมา
                }
                else
                {
                    buttonClick.FlatStyle = FlatStyle.Popup;
                    buttonClick.Font = new Font("Microsoft Sans Serif", 14f);
                    buttonClick.Text = "●";
                    buttonClick.ForeColor = Color.Red;
                    thebombertext--; //เมื่อคลิกอีกครั้งตัว ● จะหายไป
                }
            }
            if (markBomber == false && buttonClick.Text != "●") //เมื่อกดตอนที่ไม่มีMarker
            {
                buttonClick.Visible = false;
                //เริ่มตัวจับเวลา หากไม่โดนระเบิด
                if (!timer1.Enabled)
                    timer1.Enabled = true;

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

                //เมื่อคลิกในตำแหน่ง x,y ที่กำหนด
                if (labelPlan[x, y].Text == "⚈") //เมื่อคลิกในจุดที่เป็น "⚈" เวลาจะหยุดแล้วปุ่มจะหาย
                {
                    timer1.Enabled = false;
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            buttonPlan[i, j].Visible = false;

                        }
                    }
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
                    if (labelPlan[i, j].Text == " " && !buttonPlan[i, j].Visible) //เมื่อเช็คแล้วว่าว่างปล่าว
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
                        if (labelPlan[i + x, j + y].Text != "⚈") //ถ้าเช็ตแล้วว่าไม่มีระเบิด จะเคลียร์
                        {
                            buttonPlan[x + i, y + j].Visible = false;

                        }
                    }
                }
            }
        }

        //การนับจำนวนระเบิด ระเบิดทั้งหมด
        public void countBomber(int x, int y)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (labelPlan[i, j].Text != "⚈") //ถ้าเช็คว่าไม่มีระเบิด
                    {
                        countBomberHelp(i, j);
                        if (minorRunt > 0)
                        {
                            labelPlan[i, j].Text = minorRunt.ToString();
                        }
                        minorRunt = 0; //จำนวนระเบิด
                    }
                }
            }
        }

        //ตัวช่วยที่จะมาช่วยนับระเบิด เป็นระเบิดที่เหลือ
        public void countBomberHelp(int x, int y)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i + x < width && j + y < height && i + x >= 0 && j + y >= 0)
                    {
                        if (labelPlan[x + i, y + j].Text == "⚈") //เมื่อเช็คแล้วว่ามีระเบิด จะทำการเริ่มนับ
                        {
                            minorRunt++;
                        }
                    }
                }
            }
        }
        private int time = 0;

        //เปิดหน้าต่างใหม่เพื่อเลือกระดับความยาก(Menubar)
        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 Start = new Form2();
            Start.Show();
        }

        //แสดงหน้าต่างวิธีการเล่น
        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 Start = new Form5();
            Start.Show();
        }

        //แสดงหน้าต่างสถิติผลการเล่น
        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 Start = new Form3();
            Start.Show();
        }

        //event เมื่อกดปุ่มเริ่มเกม
        private void button1_Click(object sender, EventArgs e)
        {
            Program.Win = false;
            label4.Visible = false;
            timer1.Enabled = false;
            clearPlan();
            createPlan();

        }

        //event เมื่อกดปุ่ม Mark Bomb
        private void button2_Click(object sender, EventArgs e)
        {
            if (markBomber == false)
                markBomber = true;
            else
                markBomber = false;
                button2.FlatStyle = FlatStyle.Popup;
        }

        //ล้างตารางทั้งหมด
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

        //สูตรโกง?
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Program.Win = true;
            timer1.Enabled = false;

            if (Program.Win == true)
            {
                Controls.AddRange(new System.Windows.Forms.Control[] { label4, });
                label4.Location = new System.Drawing.Point(Width / 2 - 50, Height / 2 - 20);
                label4.Text = "You won!!";
                Program.time_form = time;
                Form4 FM = new Form4();
                FM.Show();
                Program.pltime = time.ToString();
                string stat = FM.stat;
                if (stat == "1")
                {
                    clearPlan();
                    label4.Show();
                }
                stat = "1";
            }


        }

    }
}
