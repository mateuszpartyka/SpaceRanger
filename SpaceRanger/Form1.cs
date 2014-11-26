using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceRanger
{
    public partial class Form1 : Form
    {
        Point spaceRangerPosition = new Point();
        Rectangle[] enemiesPositions = new Rectangle[16];
        Rectangle spaceRangerRigidBody, spaceRangerShot;
        Graphics g;
        bool isShooting = false;
        Timer timer = new Timer();
        Timer enemyTimer = new Timer();
        Timer counter = new Timer();
        int score = 0, incrementX = 1;
        double counterValue;

        //PointF[] spaceRangerRigidBody = new PointF[4];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, panelBattleSpace, new object[] { true });

            timer.Interval = 10;
            timer.Tick += timer_Tick;

            enemyTimer.Interval = 20;
            enemyTimer.Enabled = true;
            enemyTimer.Tick += enemyTimer_Tick;

            counter.Enabled = true;
            counter.Interval = 10;
            counter.Tick += counter_Tick;

            menuStrip.Renderer = new CustomToolStripRenderer();
            menuStrip.BackColor = Color.FromArgb(210, 126, 77);
            menuStrip.ForeColor = Color.WhiteSmoke;

            spaceRangerPosition.X = (panelBattleSpace.Width / 2) - 10;
            spaceRangerPosition.Y = panelBattleSpace.Height - 25;

            spaceRangerRigidBody = new Rectangle(spaceRangerPosition, new Size(20, 15));

            spaceRangerShot.Width = 3;
            spaceRangerShot.Height = 3;

            enemiesPositions[0].X = 20;
            enemiesPositions[0].Y = 10;

            for (int i = 1; i < enemiesPositions.Length; i++)
            {
                
                if (i == (enemiesPositions.Length / 2) + 1)
                    enemiesPositions[i - 1].X = 10;

                if (i >= (enemiesPositions.Length / 2))
                {
                    enemiesPositions[i].Y = 40;
                }
                else
                {
                    enemiesPositions[i].Y = 10;
                }

                enemiesPositions[i].X = enemiesPositions[i - 1].X + 40;
                enemiesPositions[i].Size = new Size(15, 15);

            }

            //labelScore.Font = new Font(labelScore.Font.Name, 12);

        }

        void counter_Tick(object sender, EventArgs e)
        {
            counterValue += 0.1;
            labelCounter.Text = counterValue.ToString("#.##");
        }

        void enemyTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < enemiesPositions.Length; i++)
                enemiesPositions[i].X += incrementX;

            if (enemiesPositions[enemiesPositions.Length - 1].X == panelBattleSpace.Width - 20 || enemiesPositions[0].X == 0)
                incrementX *= -1;

            panelBattleSpace.Refresh();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            spaceRangerShot.Y -= 5;

            if (spaceRangerShot.Y < 0)
            {
                timer.Enabled = false;
                isShooting = false;
            }

            for (int i = 0; i < enemiesPositions.Length; i++)
            {
                if (enemiesPositions[i].Contains(spaceRangerShot))
                {
                    timer.Enabled = false;
                    isShooting = false;
                    score += 50;
                    labelScore.Text = "Score: " + score;
                    enemiesPositions[i].Y = -50;
                }
            }

                panelBattleSpace.Refresh();

        }

        private void panelBattleSpace_Paint(object sender, PaintEventArgs e)
        {
            this.DoubleBuffered = true;
            g = e.Graphics;

            spaceRangerRigidBody.X = spaceRangerPosition.X;
            spaceRangerRigidBody.Y = spaceRangerPosition.Y;

            g.FillRectangle(new SolidBrush(Color.FromName("WhiteSmoke")), spaceRangerRigidBody);
            g.DrawRectangle(new Pen(Color.FromName("WhiteSmoke")), spaceRangerRigidBody);

            for (int i = 0; i < enemiesPositions.Length; i++)
            {
                g.FillRectangle(new SolidBrush(Color.FromName("Green")), enemiesPositions[i]);
                g.DrawRectangle(new Pen(Color.FromName("Green")), enemiesPositions[i]);
            }

            if (isShooting)
            {
                timer.Enabled = true;
                g.DrawRectangle(new Pen(Color.LightYellow), spaceRangerShot);
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                    if (spaceRangerPosition.X > 2)
                        spaceRangerPosition.X = spaceRangerPosition.X - 4;
                    break;

                case Keys.Right:
                    if (spaceRangerPosition.X < panelBattleSpace.Width - 22)
                        spaceRangerPosition.X = spaceRangerPosition.X + 4;
                    break;

                case Keys.Space:
                    isShooting = true;
                    spaceRangerShot.X = spaceRangerPosition.X + 10;
                    spaceRangerShot.Y = spaceRangerPosition.Y - 3;
                    break;
            }

            panelBattleSpace.Refresh();
        }
    }
}
