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
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();

            new Settings(); // linking the settings class to this form

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();

            startGame();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            // This is where we will see the snake and it's parts move

            Graphics canvas = e.Graphics; // Creating a new Graphics class called canvas

            if (Settings.GameOver == false)
            {
                // if the game is not over, then we do the following

                Brush snakeColour; // Creating a new brush called snake colour

                // run a loop to check the snake parts
                for (int i=0; i<Snake.Count; i++)
                {
                    if (i == 0)
                    {
                        // Colour the head of the snake black
                        snakeColour = Brushes.Black;
                    }
                    else
                    {
                        // the rest of the body can be green
                        snakeColour = Brushes.Green;
                    }

                    //Draw snake body and head
                    canvas.FillEllipse(snakeColour, new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height, Settings.Width, Settings.Height));

                    // draw food
                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * Settings.Width, food.Y * Settings.Height, Settings.Width, Settings.Height));
                }

            }
            else
            {
                // This part will run when the game is over
                string gameOver = "Game Over\n" + "Final Score is " + Settings.Score + "\n Press Enter to Restart \n";
                lblEndText.Text = gameOver;
                lblEndText.Visible = true;
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
                if (Input.KeyPress(Keys.Right) && Settings.direction != Directions.Left)
                {
                    Settings.direction = Directions.Right;
                }
                else if (Input.KeyPress(Keys.Left) && Settings.direction != Directions.Right)
                {
                    Settings.direction = Directions.Left;
                }
                else if (Input.KeyPress(Keys.Up) && Settings.direction != Directions.Down)
                {
                    Settings.direction = Directions.Up;
                }
                else if (Input.KeyPress(Keys.Down) && Settings.direction != Directions.Up)
                {
                    Settings.direction = Directions.Down;
                }

                movePlayer();

            }
            pbCanvas.Invalidate(); // refresh the picture box and update the graphics on it
        }

        private void movePlayer()
        {
            // The main loop for the Snake head and parts
            for (int i=Snake.Count -1; i>= 0; i--)
            {
                if (i == 0)
                {
                    switch(Settings.direction)
                    {
                        case Directions.Right:
                            Snake[i].X++;
                            break;
                        case Directions.Left:
                            Snake[i].X--;
                            break;
                        case Directions.Up:
                            Snake[i].Y--;
                            break;
                        case Directions.Down:
                            Snake[i].Y++;
                            break;
                    }

                    //Restrict the snake from leaving the canvas
                    int maxXpos = pbCanvas.Size.Width / Settings.Width;
                    int maxYpos = pbCanvas.Size.Height / Settings.Height;

                    if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X > maxXpos || Snake[i].Y > maxYpos)
                    {
                        die();
                    }

                    // Detect Collision with the body
                    // this loop will check if the snake had a collision with other body parts
                    for (int j=1; j<Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            // if so, run the die function (game is over)
                            die();
                        }
                    }

                    // Detect collision between snake and food
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        //if so, eat it
                        eat();
                    }
                }
                else
                {
                    // if there are no collision with anything, move the snake
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void startGame()
        {
            lblEndText.Visible = false;
            new Settings();
            Snake.Clear();
            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);

            lblScoreVal.Text = Settings.Score.ToString();

            generateFood();
        }

        private void generateFood()
        {
            int maxXpos = pbCanvas.Size.Width / Settings.Width;
            int maxYpos = pbCanvas.Size.Height / Settings.Height;

            Random rnd = new Random();
            food = new Circle { X = rnd.Next(0, maxXpos), Y = rnd.Next(0, maxYpos) };
        }

        private void eat()
        {
            // Add a part to the body

            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };

            Snake.Add(body); // Add new body part to the Snake List
            Settings.Score += Settings.Points;
            lblScoreVal.Text = Settings.Score.ToString();
            generateFood();
        }

        private void die()
        {
            Settings.GameOver = true;
        }
    }
}
