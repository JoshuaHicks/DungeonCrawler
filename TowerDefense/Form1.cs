using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerDefense
{
    public partial class Form1 : Form
    {
        private List<Tower> towerList = new List<Tower>();
        private List<Bloon> bloonList = new List<Bloon>();

        public bool startRound = false;
        public bool roundInProgress = false;
        public bool bloonsAdded = false;

        public PictureBox newPB;

        public int currentGameTime;

        public int currentRound;

        public Form1()
        {
            InitializeComponent();
            currentGameTime = 0;
            currentRound = 1;

            new Settings(); // linking the settings class to this form

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();

            newPB = new PictureBox();
            newPB.Size = pbBasicTower.Size;
            newPB.Left = -100;
            newPB.Top = -100;
            newPB.BackColor = Color.Transparent;
            pbCanvas.Controls.Add(newPB);

            startGame();
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            // This is where we will see the bloons move

            Graphics canvas = e.Graphics; // Creating a new Graphics class called canvas

            if (Settings.GameOver == false)
            {
                try
                {
                    // draw bloon
                    for (int i = 0; i < bloonList.Count; i++)
                    {
                        canvas.FillEllipse(bloonList[i].colour, new Rectangle(bloonList[i].X, bloonList[i].Y, Settings.Width, Settings.Height));
                        if (bloonList[i].Y > pbCanvas.Height)
                        {
                            die();
                        }
                    }

                    //Draw projectiles
                    for (int i = 0; i < towerList.Count; i++)
                    {
                        for (int j = 0; j < towerList[i].projectileList.Count; j++)
                        {
                            towerList[i].projectileList[j].X += (int)towerList[i].projectileList[j].speedX;
                            towerList[i].projectileList[j].Y += (int)towerList[i].projectileList[j].speedY;

                            if (towerList[i].projectileList[j].X < 0 ||
                                towerList[i].projectileList[j].X > pbCanvas.Width ||
                                towerList[i].projectileList[j].Y < 0 ||
                                towerList[i].projectileList[j].Y > pbCanvas.Height)
                            {
                                towerList[i].projectileList.Remove(towerList[i].projectileList[j]);
                            }
                            canvas.FillEllipse(Brushes.Black, new Rectangle(towerList[i].projectileList[j].X, towerList[i].projectileList[j].Y, 5, 5));
                        }
                        // Drawing the range of the towers
                        canvas.DrawEllipse(Pens.Red, new RectangleF(towerList[i].X - (towerList[i].range / 2), towerList[i].Y - (towerList[i].range / 2), towerList[i].range, towerList[i].range));
                    }
                } catch (Exception ex)
                {

                }

                
            }
            else
            {
                // Gameover
                lblGameOver.Visible = Visible;
            }
            currentGameTime++;
        }

        private void updateScreen(object sender, EventArgs e)
        {
            // This is the timer's update screen function
            // each tick will run this function

            if (Settings.GameOver == true)
            {
                if (Input.KeyPress(Keys.Enter))
                {
                    startGame();
                }
            }
            else
            {
                if (startRound)
                {
                    startRound = false;
                    roundInProgress = true;
                }
                if (roundInProgress)
                {
                    if (bloonsAdded == false)
                    {
                        generateBloons(currentRound++);
                        bloonsAdded = true;
                    }
                    moveBloons();
                }
            }
            pbCanvas.Invalidate();
        }

        private void shootBloons(Tower currTower, double angle, int signX, int signY)
        {
            currTower.shootProjectiles(angle, signX, signY);
        }

        private void checkIfBloonWasHit()
        {
            try
            {
                for (int i = 0; i < towerList.Count; i++)
                {
                    for (int j = 0; j < towerList[i].projectileList.Count; j++)
                    {
                        for (int k = 0; k < bloonList.Count; k++)
                        {
                            var d = Math.Sqrt(Math.Pow(towerList[i].projectileList[j].X - bloonList[k].X, 2) + Math.Pow(towerList[i].projectileList[j].Y - bloonList[k].Y, 2));
                            //Console.WriteLine("d = " + d);
                            if (d <= 15)
                            {
                                // if bloon was red, remove it
                                if (bloonList[k].type == Types.Red)
                                {
                                    bloonList.Remove(bloonList[k]);
                                    if (bloonList.Count == 0)
                                    {
                                        roundInProgress = false;
                                        bloonsAdded = false;
                                    }
                                }
                                else
                                {
                                    bloonList[k].type -= 1;
                                    bloonList[k].setColour();
                                }
                                


                                towerList[i].projectileList.Remove(towerList[i].projectileList[j]);
                                Settings.Money++;
                                lblMoneyVal.Text = "$" + Settings.Money.ToString();
                            }

                        }
                    }
                }
            } catch (Exception e)
            {

            }
            
        }

        private void moveBloons()
        {
            try
            {
                for (int i = 0; i < bloonList.Count; i++)
                {
                    for (int t = 0; t < towerList.Count; t++)
                    {
                        // Pythagorean Therorem
                        // d = sqrt((x0 - x1)^2 + (y0 - y1)^2)
                        // (x0, y0) -> center of circle
                        // (x1, y1) -> bloon position
                        // if d <= range / 2

                        var d = Math.Sqrt(Math.Pow(towerList[t].X - bloonList[i].X, 2) + Math.Pow(towerList[t].Y - bloonList[i].Y, 2));

                        if (d - 10 <= towerList[t].range / 2)
                        {
                            // set angle of the projectile
                            double adjacent = Math.Abs(bloonList[i].Y - towerList[t].Y);
                            double opposite = Math.Abs(bloonList[i].X - towerList[t].X);
                            double angle = 0;
                            if (opposite != 0)
                                angle = Math.Atan(opposite / adjacent);

                            int signX = 1, signY = 1;

                            if (bloonList[i].X <= towerList[t].X && bloonList[i].Y <= towerList[t].Y) // top-left
                            {
                                signX = -1;
                                signY = -1;
                            }
                            else if (bloonList[i].X >= towerList[t].X && bloonList[i].Y <= towerList[t].Y) // top-right
                            {
                                signX = 1;
                                signY = -1;
                            }
                            else if (bloonList[i].X <= towerList[t].X && bloonList[i].Y >= towerList[t].Y) // bottom-left
                            {
                                signX = -1;
                                signY = 1;
                            }
                            
                            shootBloons(towerList[t], angle, signX, signY);
                        }
                    }

                    switch (bloonList[i].trackSection)
                    {
                        case TrackSections.Section1:
                            if (bloonList[i].Y < 65)
                            {
                                bloonList[i].Y += bloonList[i].speed;
                            }
                            else
                            {
                                bloonList[i].trackSection = TrackSections.Section2;
                            }
                            break;
                        case TrackSections.Section2:
                            if (bloonList[i].X < 326)
                            {
                                bloonList[i].X += bloonList[i].speed;
                            }
                            else
                            {
                                bloonList[i].trackSection = TrackSections.Section3;
                            }
                            break;
                        case TrackSections.Section3:
                            if (bloonList[i].Y < 302)
                            {
                                bloonList[i].Y += bloonList[i].speed;
                            }
                            else
                            {
                                bloonList[i].trackSection = TrackSections.Section4;
                            }
                            break;
                        case TrackSections.Section4:
                            if (bloonList[i].X > 189)
                            {
                                bloonList[i].X -= bloonList[i].speed;
                            }
                            else
                            {
                                bloonList[i].trackSection = TrackSections.Section5;
                            }
                            break;
                        case TrackSections.Section5:
                            if (bloonList[i].Y < 510)
                            {
                                bloonList[i].Y += bloonList[i].speed;
                            }
                            else
                            {
                                bloonList.Remove(bloonList[i]);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {

            }

            checkIfBloonWasHit();

        }

        private void startGame()
        {
            new Settings();

            lblMoneyVal.Text = "$" + Settings.Money.ToString();
        }

        private void generateBloons(int round)
        {
            switch (round)
            {
                case 1: // 10 red
                    bloonList.Clear();
                    for (int i = 0; i < 10; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Red) { X = 95, Y = -30, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Red) { X = 95, Y = bloonList[i - 1].Y - 35, trackSection = TrackSections.Section1, speed = 2 });
                        }
                    }
                    break;
                case 2: // 15 red
                    bloonList.Clear();
                    for (int i = 0; i < 15; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Red) { X = 95, Y = -30, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Red) { X = 95, Y = bloonList[i - 1].Y - 35, trackSection = TrackSections.Section1, speed = 2 });
                        }
                    }
                    break;
                case 3: // 20 red - 5 blue
                    bloonList.Clear();
                    for (int i = 0; i < 25; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Red) { X = 95, Y = -20, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else if (i <= 20)
                        {
                            bloonList.Add(new Bloon(Types.Red) { X = 95, Y = bloonList[i - 1].Y - 20, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Blue) { X = 95, Y = bloonList[i - 1].Y - 20, trackSection = TrackSections.Section1, speed = 2 });
                        }
                    }
                    break;
                case 4: // 10 red - 10 blue
                    bloonList.Clear();
                    for (int i = 0; i < 20; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Red) { X = 95, Y = -30, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else if (i <= 10)
                        {
                            bloonList.Add(new Bloon(Types.Red) { X = 95, Y = bloonList[i - 1].Y - 15, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Blue) { X = 95, Y = bloonList[i - 1].Y - 10, trackSection = TrackSections.Section1, speed = 2 });
                        }
                    }
                    break;
                case 5: // 20 blue
                    bloonList.Clear();
                    for (int i = 0; i < 20; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Blue) { X = 95, Y = -30, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Blue) { X = 95, Y = bloonList[i - 1].Y - 30, trackSection = TrackSections.Section1, speed = 2 });
                        }
                    }
                    break;
                case 6: // 25 blue - 5 yellow
                    bloonList.Clear();
                    for (int i = 0; i < 30; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Blue) { X = 95, Y = -25, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else if (i <= 25)
                        {
                            bloonList.Add(new Bloon(Types.Blue) { X = 95, Y = bloonList[i - 1].Y - 25, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Yellow) { X = 95, Y = bloonList[i - 10].Y - 35, trackSection = TrackSections.Section1, speed = 2 });
                        }
                    }
                    break;
                case 7: // 15 blue - 15 yellow
                    bloonList.Clear();
                    for (int i = 0; i < 30; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Blue) { X = 95, Y = -20, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else if (i <= 15)
                        {
                            bloonList.Add(new Bloon(Types.Blue) { X = 95, Y = bloonList[i - 1].Y - 20, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Yellow) { X = 95, Y = bloonList[i - 1].Y - 20, trackSection = TrackSections.Section1, speed = 2 });
                        }
                    }
                    break;
                case 8: // 20 yellow
                    bloonList.Clear();
                    for (int i = 0; i < 20; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Yellow) { X = 95, Y = -30, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Yellow) { X = 95, Y = bloonList[i - 1].Y - 35, trackSection = TrackSections.Section1, speed = 2 });
                        }
                    }
                    break;
                case 9: // 5 pink
                    bloonList.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Pink) { X = 95, Y = -30, trackSection = TrackSections.Section1, speed = 3 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Pink) { X = 95, Y = bloonList[i - 1].Y - 35, trackSection = TrackSections.Section1, speed = 3 });
                        }
                    }
                    break;
                case 10: // 10 yellow - 10 pink
                    bloonList.Clear();
                    for (int i = 0; i < 10; i++)
                    {
                        if (i == 0)
                        {
                            bloonList.Add(new Bloon(Types.Yellow) { X = 95, Y = -30, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else if (i <= 10)
                        {
                            bloonList.Add(new Bloon(Types.Yellow) { X = 95, Y = bloonList[i - 1].Y - 35, trackSection = TrackSections.Section1, speed = 2 });
                        }
                        else
                        {
                            bloonList.Add(new Bloon(Types.Pink) { X = 95, Y = bloonList[i - 5].Y - 23, trackSection = TrackSections.Section1, speed = 3 });
                        }
                    }
                    break;
            }
        }

        private void die()
        {
            Settings.GameOver = true;
        }

        public bool isDragging = false;
        public int currentX;
        public int currentY;
        private void pbBasicTower_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            
            currentX = e.X;
            currentY = e.Y;
        }

        
        private void pbBasicTower_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                newPB.Image = pbBasicTower.Image;
                newPB.Top = pbBasicTower.Top + (e.Y - currentY);
                newPB.Left = pbBasicTower.Left + (e.X - currentX);
            }
        }

        private void pbBasicTower_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pb = new PictureBox();
            pb.Top = pbBasicTower.Top + (e.Y - currentY);
            pb.Left = pbBasicTower.Left + (e.X - currentX);
            pb.Image = pbBasicTower.Image;
            pb.Size = pbBasicTower.Size;
            pb.BackColor = Color.Transparent;
            pbCanvas.Controls.Add(pb);

            towerList.Add(new Tower(pb));

            newPB.Left = -100;
            newPB.Top = -100;

            Settings.Money -= 450;

            lblMoneyVal.Text = "$" + Settings.Money;

            isDragging = false;
        }

        private void btnStart_MouseClick(object sender, MouseEventArgs e)
        {
            startRound = true;
            lblRoundVal.Text = currentRound.ToString();
        }
    }
}
