using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    class Projectile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double speedX { get; set; }
        public double speedY { get; set; }
        public double angle { get; set; }

        public Projectile(int x, int y, double defaultAngle, int signX, int signY)
        {
            X = x;
            Y = y;
            angle = defaultAngle; //default angle is 0
            speedX = (Math.Sin(angle) * 5) * signX;
            speedY = (Math.Cos(angle) * 5) * signY;
        }
    }
}
