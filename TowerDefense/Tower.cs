﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerDefense
{
    public enum TowerTypes
    {
        BasicTower
    }

    class Tower
    {
        public TowerTypes towerType { get; set; }
        public PictureBox pbBox { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<Projectile> projectileList { get; set; }
        public int range { get; set; } // diameter of circle range of tower
        public int cost { get; set; }

        public Tower(PictureBox pb)
        {
            projectileList = new List<Projectile>();
            X = pb.Left + (pb.Width / 2);
            Y = pb.Top + (pb.Height / 2);
            range = 150;
            cost = 450;
        }

        public void shootProjectiles(double angle, int signX, int signY)
        {
            projectileList.Add(new Projectile(X, Y, angle, signX, signY));
        }
    }

}
