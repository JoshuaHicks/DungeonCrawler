﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public enum Directions
    {
        Left,
        Right,
        Up,
        Down
    };

    class Settings
    { 
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Money { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
        public static Directions direction { get; set; }

        public Settings()
        {
            // Default settings
            Width = 16;
            Height = 16;
            Speed = 60;
            Money = 800;
            Points = 100;
            GameOver = false;
            direction = Directions.Down; // Default direction is down
        }
    }
}
