using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SpaceRanger
{
    public partial class Form1 : Form
    {
        bool isShooting = false;
        double counterValue;
        int score, minutes, enemyCount, enemyPlaceCorrection;
        
        Bitmap spaceRangerBitmap, enemyBitmap;
        Graphics g;

        Color[] enemyColors;

        List<SpaceEnemyDestroyedParticles> enemyDestroyedAnimations;
        
        //List<int> enemiesDestroyed = new List<int>();
        List<Point> spaceRangerShots;
        List<SpaceEnemy> enemiesList;
        
        Point spaceRangerPosition = new Point();

        Rectangle spaceRangerRigidBody, spaceRangerShot, enemyShot;

        Timer shotTimer;
        Timer enemyTimer;
        Timer enemyShotTimer;
        Timer elapsedTimeTimer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // anti-fickering fix
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, panelBattleSpace, new object[] { true });

            enemyColors = new Color[6] {Color.Crimson, Color.LimeGreen, Color.Cyan, Color.Plum, Color.DarkOrange, Color.WhiteSmoke};        

            // applies custom renderer, so that menu strip can have different colors after hovering the items
            menuStrip.Renderer = new CustomToolStripRenderer();
            menuStrip.BackColor = Color.FromArgb(210, 126, 77);
            menuStrip.ForeColor = Color.WhiteSmoke;

            StartNewGame();
            
        }

        private void StartNewGame()
        {
            if (enemyTimer != null)
            {
                shotTimer.Dispose();
                enemyTimer.Dispose();
                enemyShotTimer.Dispose();
                elapsedTimeTimer.Dispose();
            }

            shotTimer = new Timer();
            enemyTimer = new Timer();
            enemyShotTimer = new Timer();
            elapsedTimeTimer = new Timer();
            score = 0;
            minutes = 0;
            enemyCount = 24;
            enemyPlaceCorrection = 10;
            counterValue = 0;
            labelScore.Text = "Score: 0";

            // provides shot animations
            shotTimer.Interval = 10;
            shotTimer.Tick += timer_Tick;

            // moves enemies
            enemyTimer.Interval = 20;
            enemyTimer.Enabled = true;
            enemyTimer.Tick += enemyTimer_Tick;

            enemyShotTimer.Interval = 4000;
            enemyShotTimer.Enabled = true;
            enemyShotTimer.Tick += enemyShotTimer_Tick;

            // counts elapsed time
            elapsedTimeTimer.Enabled = true;
            elapsedTimeTimer.Interval = 1;
            elapsedTimeTimer.Tick += counter_Tick;

            spaceRangerShots = new List<Point>();
            enemyDestroyedAnimations = new List<SpaceEnemyDestroyedParticles>();
            enemiesList = new List<SpaceEnemy>();

            // calculate Space Ranger position (center and down the panel)
            spaceRangerPosition.X = (panelBattleSpace.Width / 2) - 20;
            spaceRangerPosition.Y = panelBattleSpace.Height - 45;

            // initialize Space Ranger rigid body
            spaceRangerRigidBody = new Rectangle(spaceRangerPosition, new Size(40, 40));
            spaceRangerBitmap = new Bitmap("E:\\SpaceRanger\\SpaceRanger\\Sprites\\spaceship.png");

            enemyBitmap = new Bitmap("E:\\SpaceRanger\\SpaceRanger\\Sprites\\alien.png");

            // define shot dimensions
            spaceRangerShot.Width = 3;
            spaceRangerShot.Height = 3;

            // generate enemies
            for (int i = 0; i < enemyCount; i++)
            {
                if (i != 0 && i % (enemyCount / 3) == 0)
                {
                    enemyPlaceCorrection += 10;
                }

                enemiesList.Add(
                    new SpaceEnemy(
                        new Point((40 * (i % (enemyCount/3))) + enemyPlaceCorrection, (int) (i/(enemyCount/3)) * 40 + enemyPlaceCorrection),
                        new Size(30, 20),
                        3 - (i / (enemyCount / 3))
                    )
                );
            }
        }

        // create new shot with each tick - enemy is chosen at random
        void enemyShotTimer_Tick(object sender, EventArgs e)
        {
            int randomEnemyIndex = new Random().Next(enemiesList.Count);
            enemyShot = new Rectangle(enemiesList.ElementAt(randomEnemyIndex).RigidBody.Location, new Size(5, 5));
        }

        // counts elapsed time
        void counter_Tick(object sender, EventArgs e)
        {
            counterValue += 0.01;
            if (counterValue % 60 == 0 && counterValue != 0)
                minutes++;
            labelCounter.Text = minutes.ToString() + ":" + (counterValue % 60).ToString("00.00").Replace(",",":");
        }

        // timer represents enemies - moves them, play animations, etc
        void enemyTimer_Tick(object sender, EventArgs e)
        {
            // move shot if it is on battle space
            if (enemyShot.Y < panelBattleSpace.Height)
                enemyShot.Y += 7;

            // if enemy shot hits the Ranger - game ends
            if (spaceRangerRigidBody.Contains(enemyShot))
                endGame();

            for (int i = 0; i < enemiesList.Count; i++) {
                SpaceEnemy currentEnemy = enemiesList.ElementAt(i);
                currentEnemy.X += currentEnemy.incrementX;
                currentEnemy.Invalidate();
                enemiesList.RemoveAt(i);
                enemiesList.Insert(i, currentEnemy);

                // if enemy touches edge of battle space, change its X drection
                if (enemiesList.ElementAt(i).X > panelBattleSpace.Width - 30 || enemiesList.ElementAt(i).X < 0)
                    currentEnemy.incrementX *= -1;

                // if enemies colides with themselves, change their X direction
                for (int j = 0; j < enemiesList.Count; j++)
                {
                    SpaceEnemy colidingEnemy = enemiesList.ElementAt(j);
                    if (currentEnemy.RigidBody.Contains(colidingEnemy.RigidBody.Location))
                    {
                        currentEnemy.incrementX *= -1;
                        colidingEnemy.incrementX *= -1;
                    }
                }
            }

            if (enemyDestroyedAnimations.Count > 0)
            {
                for (int i = 0; i < enemyDestroyedAnimations.Count; i++)
                {
                    enemyDestroyedAnimations.ElementAt(i).moveParticles();
                    if (enemyDestroyedAnimations.ElementAt(i).MoveCount > 10)
                        enemyDestroyedAnimations.RemoveAt(i);
                }

            }

            panelBattleSpace.Refresh();
        }

        // moves bullet
        void timer_Tick(object sender, EventArgs e)
        {
            // recalculate each bullets Y position
            for (int i = 0; i < spaceRangerShots.Count; i++)
            {
                Point currentShot = spaceRangerShots.ElementAt(i);
                currentShot.Y -= 5;
                spaceRangerShots.RemoveAt(i);
                spaceRangerShots.Insert(i, currentShot);

                // if bullets goes past the edge of the screen, delete it
                if (spaceRangerShots.ElementAt(i).Y < 0)
                {
                    spaceRangerShots.RemoveAt(i);
                }
            }

            // if bullet hits enemy, destroy them both
            for (int i = 0; i < enemiesList.Count; i++)
            {
                SpaceEnemy currentEnemy = enemiesList.ElementAt(i);

                for (int j = 0; j < spaceRangerShots.Count; j++)
                {
                    Point currentShot = spaceRangerShots.ElementAt(j);
                    if (currentEnemy.RigidBody.Contains(currentShot))
                    {
                        enemyDestroyedAnimations.Add(new SpaceEnemyDestroyedParticles(currentEnemy.RigidBody));
                        score += 50;
                        labelScore.Text = "Score: " + score;
                        enemiesList.RemoveAt(i);
                        spaceRangerShots.RemoveAt(j);
                        break;
                    }
                }
            }

            // if all enemies are gone, end the game
            if (enemiesList.Count == 0)
                endGame();

            panelBattleSpace.Refresh();

        }

        // occurs when game ends
        private void endGame()
        {
            isShooting = false;
            spaceRangerShots.Clear();
            
            shotTimer.Enabled = false;
            shotTimer.Stop();
            
            elapsedTimeTimer.Enabled = false;
            elapsedTimeTimer.Stop();
            
            enemyTimer.Enabled = false;
            enemyTimer.Stop();

            enemyShotTimer.Enabled = false;
            enemyShotTimer.Stop();

            ShowWinDialog();
        }

        // show input for player name to Hall of Fame
        private void ShowWinDialog()
        {
            FormWinDialog winDialog = new FormWinDialog(labelCounter.Text);
            DialogResult result = winDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                MessageBox.Show(winDialog.getName() + " - " + labelCounter.Text);
            }
            else
            {
                MessageBox.Show("Nie podałeś imienia :(");
            }

            winDialog.Dispose();
        }

        private void panelBattleSpace_Paint(object sender, PaintEventArgs e)
        {
            this.DoubleBuffered = true;
            g = e.Graphics;

            spaceRangerRigidBody.X = spaceRangerPosition.X;
            spaceRangerRigidBody.Y = spaceRangerPosition.Y;

            // draw Space Ranger
            g.DrawImage(spaceRangerBitmap, spaceRangerPosition.X, spaceRangerPosition.Y, 40, 40);
            //g.DrawRectangle(new Pen(Color.Cyan), spaceRangerRigidBody);

            // draw laser
            g.DrawLine(new Pen(Color.FromArgb(45, 255, 0, 0)), new Point(spaceRangerPosition.X + 20, spaceRangerPosition.Y), new Point(spaceRangerPosition.X + 20, panelBattleSpace.Height / 2));
            
            // enemy shot
            g.DrawRectangle(new Pen(Color.Cyan), enemyShot);
            g.FillRectangle(new SolidBrush(Color.Cyan), enemyShot);

            // draw enemies based on their position
            for (int i = 0; i < enemiesList.Count; i++)
            {
                g.DrawImage(((Bitmap)enemyBitmap.Clone()), enemiesList.ElementAt(i).RigidBody);
            }

            // if Ranger is shooting, start moving the bullet
            if (isShooting)
            {
                for (int i = 0; i < spaceRangerShots.Count; i++)
                {
                    shotTimer.Enabled = true;
                    g.FillRectangle(new SolidBrush(Color.LightYellow), new Rectangle(spaceRangerShots.ElementAt(i), new Size(4, 4)));
                }
            }

            if (enemyDestroyedAnimations.Count > 0)
            {
                for (int i = 0; i < enemyDestroyedAnimations.Count; i++)
                {
                    g.FillRectangles(new SolidBrush(enemyColors[new Random().Next(0, enemyColors.Length)]), enemyDestroyedAnimations.ElementAt(i).ParticleList);
                }
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                    if (spaceRangerPosition.X > 14)
                        spaceRangerPosition.X = spaceRangerPosition.X - 4;
                    break;

                case Keys.Right:
                    if (spaceRangerPosition.X < panelBattleSpace.Width - 31)
                        spaceRangerPosition.X = spaceRangerPosition.X + 4;
                    break;

                case Keys.Space:
                    if (spaceRangerShots.Count < 5)
                    {
                        // 18 = (spaceRanger.Width / 2) - (shot.Width / 2)
                        spaceRangerShots.Add(new Point(spaceRangerPosition.X + 18, spaceRangerPosition.Y - 3));
                        spaceRangerShot.X = spaceRangerPosition.X + 19;
                        spaceRangerShot.Y = spaceRangerPosition.Y - 3;
                        isShooting = true;
                    }
                    break;
            }

            panelBattleSpace.Refresh();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
