using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Types
{
    Red,
    Blue,
    Yellow,
    Pink
};

public enum TrackSections
{
    Section1,
    Section2,
    Section3,
    Section4,
    Section5,
    Unknown
}

namespace TowerDefense
{
    class Bloon
    {
        public int X { get; set; } // x pos of bloon
        public int Y { get; set; } // y pos of bloon
        public Types type { get; set; } // the type of bloon
        public TrackSections trackSection { get; set; }
        public bool isMoving { get; set; }

        public Bloon()
        {
            X = 0;
            Y = 0;
            isMoving = false;
        }
    }
}
