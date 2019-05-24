using System;
using System.Collections.Generic;
using System.Drawing;
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
        public int speed { get; set; } //speed of the bloon
        public Types type { get; set; } // the type of bloon
        public TrackSections trackSection { get; set; }
        public bool isMoving { get; set; }
        public Brush colour { get; set; }

        public Bloon(Types bloonType)
        {
            X = 0;
            Y = 0;
            speed = 1; // Default is 1
            isMoving = false;
            type = bloonType;

            switch (bloonType)
            {
                case Types.Red:
                    colour = Brushes.Red;
                    break;
                case Types.Blue:
                    colour = Brushes.Blue;
                    break;
                case Types.Yellow:
                    colour = Brushes.Yellow;
                    break;
                case Types.Pink:
                    colour = Brushes.Pink;
                    break;
                default:
                    colour = Brushes.Black;
                    break;
            }
        }

        public void setColour()
        {
            switch (type)
            {
                case Types.Red:
                    colour = Brushes.Red;
                    break;
                case Types.Blue:
                    colour = Brushes.Blue;
                    break;
                case Types.Yellow:
                    colour = Brushes.Yellow;
                    break;
                case Types.Pink:
                    colour = Brushes.Pink;
                    break;
                default:
                    colour = Brushes.Black;
                    break;
            }
        }
    }
}
