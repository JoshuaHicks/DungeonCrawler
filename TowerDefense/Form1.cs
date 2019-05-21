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

        private List<Bloon> bloonList = new List<Bloon>();

        public bool startRound = false;

        public Form1()
        {
            InitializeComponent();

            new Settings(); // linking the settings class to this form

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();

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
            }
            else
            {
                // Gameover
            }
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
            }
            pbCanvas.Invalidate();
        }

        private void moveBloons()
        {
            if (startRound)
            {
                for (int i = 0; i < bloonList.Count; i++)
                {
                    if (i == 0)
                    {
                        bloonList[i].isMoving = true;
                    }
                    else if (bloonList[i - 1].Y > 0)
                    {
                        bloonList[i].isMoving = true;
                    }
                    if (bloonList[i].isMoving)
                    {
                        switch (bloonList[i].trackSection)
                        {
                            case TrackSections.Section1:
                                if (bloonList[i].Y < 65)
                                {
                                    bloonList[i].Y++;
                                }
                                else
                                {
                                    bloonList[i].trackSection = TrackSections.Section2;
                                }
                                break;
                            case TrackSections.Section2:
                                if (bloonList[i].X < 328)
                                {
                                    bloonList[i].X++;
                                }
                                else
                                {
                                    bloonList[i].trackSection = TrackSections.Section3;
                                }
                                break;
                            case TrackSections.Section3:
                                if (bloonList[i].Y < 302)
                                {
                                    bloonList[i].Y++;
                                }
                                else
                                {
                                    bloonList[i].trackSection = TrackSections.Section4;
                                }
                                break;
                            case TrackSections.Section4:
                                if (bloonList[i].X > 189)
                                {
                                    bloonList[i].X--;
                                }
                                else
                                {
                                    bloonList[i].trackSection = TrackSections.Section5;
                                }
                                break;
                            case TrackSections.Section5:
                                if (bloonList[i].Y < 510)
                                {
                                    bloonList[i].Y++;
                                }
                                break;
                            default:
                                break;
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
                bloonList.Add(new Bloon() { X = 95, Y = -30, trackSection = TrackSections.Section1 });
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
                pbBasicTower.Top = pbBasicTower.Top + (e.Y - currentY);
                pbBasicTower.Left = pbBasicTower.Left + (e.X - currentX);
            }
        }

        private void pbBasicTower_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void btnStart_MouseClick(object sender, MouseEventArgs e)
        {
            startRound = true;
            lblRoundVal.Text = "01";
        }
    }
}
