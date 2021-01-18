using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Grid_Classes
{
    public class Coordinate
    {

        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }




        public Coordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        private double Distance_Between_Coordinates(Coordinate a, Coordinate b)
        {
           

            return 1.0;
        }

        private double Distance_Between_Coordinates(Coordinate b)
        {
            return 1.0;
            
        }

    }
}
