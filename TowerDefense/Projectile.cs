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
        public int speed { get; set; }

        public Projectile(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
