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
        private Bloon bloon1 = new Bloon(); // creating a single red bloon

        private List<Tower> towerList = new List<Tower>();
        private List<Bloon> bloonList = new List<Bloon>();

        public bool startRound = false;

        public PictureBox newPB;

        public int currentGameTime;

        public Form1()
        {
            InitializeComponent();
            currentGameTime = 0;

            new Settings(); // linking the settings class to this form

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();

            newPB = new PictureBox();
            newPB.Size = pbBasicTower.Size;
            newPB.Left = -100;
            newPB.Top = -100;
            pbCanvas.Controls.Add(newPB);

            startGame();
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            // This is where we will see the bloons move

            Graphics canvas = e.Graphics; // Creating a new Graphics class called canvas

            if (Settings.GameOver == false)
            {
                // draw bloon
                for (int i=0; i<bloonList.Count; i++)
                {
                    canvas.FillEllipse(Brushes.Red, new Rectangle(bloonList[i].X, bloonList[i].Y, Settings.Width, Settings.Height));
                }

                //Draw projectiles
                for (int i=0; i<towerList.Count; i++)
                {
                    for (int j=0; j<towerList[i].projectileList.Count; j++)
                    {
                        canvas.FillEllipse(Brushes.Black, new Rectangle(towerList[i].projectileList[j].X, towerList[i].projectileList[j].Y, 5, 5));
                    }
                }
            }
            else
            {
                // Gameover
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
                moveBloons();

                // if bloon is in range of tower
               // for (int i=0; i<towerList.Count; i++)
                //{
                    // if bloon is in range of tower
                    //for (int j=0; j<bloonList.Count; j++)
                    //{
                        //if (bloonList[j].isMoving)
                        //{
                            //Console.WriteLine("Bloon x = " + bloonList[j].X);
                            //Console.WriteLine("Tower x = " + towerList[i].X);

                            //if (Math.Abs((towerList[i].X - towerList[i].range) - bloonList[j].X) <= 5)
                            //{
                                 //shootBloons(towerList[i]);
                            //}
                       //}
                    //}
                    //shootBloons(towerList[i]);
                //}
                
            }
            pbCanvas.Invalidate();
        }

        private void shootBloons(Tower currTower)
        {
            if (currentGameTime % 20 == 0)
                currTower.shootProjectiles();

            for (int j = 0; j < currTower.projectileList.Count; j++)
            {
                if (currTower.projectileList[j].X < currTower.X - currTower.range) //update this
                    currTower.projectileList.Remove(currTower.projectileList[j]);
                else
                    currTower.projectileList[j].X--;
            }

            checkIfBloonWasHit();
        }

        private void checkIfBloonWasHit()
        {
            for (int i=0; i<towerList.Count; i++)
            {
                for (int j=0; j<towerList[i].projectileList.Count; j++)
                {
                    for (int k=0; k<bloonList.Count; k++)
                    {
                        if (bloonList[k].isMoving)
                        {
                            if ((Math.Abs(bloonList[k].X - towerList[i].projectileList[j].X) <= 12) && (Math.Abs(bloonList[k].Y - towerList[i].projectileList[j].Y) <= 12))
                            {
                                bloonList.Remove(bloonList[k]);
                                towerList[i].projectileList.Remove(towerList[i].projectileList[j]);
                                Settings.Money++;
                                lblMoneyVal.Text = "$" + Settings.Money.ToString();
                            }
                        }
                    }
                }
            }
        }

        private void moveBloons()
        {
            if (startRound)
            {
                for (int i = 0; i < bloonList.Count; i++)
                {
                    for (int t = 0; t<towerList.Count; t++)
                    {
                        if (Math.Abs((towerList[t].X - towerList[t].range) - bloonList[i].X) <= 15)
                        {
                            shootBloons(towerList[t]);
                        }
                    }

                    if (bloonList[i].isMoving)
                    {
                        switch (bloonList[i].trackSection)
                        {
                            case TrackSections.Section1:
                                if (bloonList[i].Y < 65)
                                {
                                    bloonList[i].Y+=bloonList[i].speed;
                                }
                                else
                                {
                                    bloonList[i].trackSection = TrackSections.Section2;
                                }
                                break;
                            case TrackSections.Section2:
                                if (bloonList[i].X < 328)
                                {
                                    bloonList[i].X+= bloonList[i].speed;
                                }
                                else
                                {
                                    bloonList[i].trackSection = TrackSections.Section3;
                                }
                                break;
                            case TrackSections.Section3:
                                if (bloonList[i].Y < 302)
                                {
                                    bloonList[i].Y+= bloonList[i].speed;
                                }
                                else
                                {
                                    bloonList[i].trackSection = TrackSections.Section4;
                                }
                                break;
                            case TrackSections.Section4:
                                if (bloonList[i].X > 189)
                                {
                                    bloonList[i].X-= bloonList[i].speed;
                                }
                                else
                                {
                                    bloonList[i].trackSection = TrackSections.Section5;
                                }
                                break;
                            case TrackSections.Section5:
                                if (bloonList[i].Y < 510)
                                {
                                    bloonList[i].Y+= bloonList[i].speed;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            bloonList[i].isMoving = true;
                        }
                        else if (bloonList[i - 1].Y > 0)
                        {
                            bloonList[i].isMoving = true;
                        }
                    }
                }
            }
        }

        private void startGame()
        {
            new Settings();

            lblMoneyVal.Text = "$" + Settings.Money.ToString();

            generateBloons();
        }

        private void generateBloons()
        {
            for (int i=0; i<100; i++)
            {
                bloonList.Add(new Bloon() { X = 95, Y = -30, trackSection = TrackSections.Section1 , speed = 2});
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
            pbCanvas.Controls.Add(pb);

            towerList.Add(new Tower(pb));

            newPB.Left = -100;
            newPB.Top = -100;

            isDragging = false;
        }

        private void btnStart_MouseClick(object sender, MouseEventArgs e)
        {
            startRound = true;
            lblRoundVal.Text = "01";
        }
    }
}
