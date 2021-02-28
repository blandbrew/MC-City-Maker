using MC_City_Maker.Command_Generator;
using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Structures.Buildings.Building_Construction
{
    public class Roof
    {
        public Roof()
        {

        }

        public void PyramidRoof(Coordinate startCoordinate, Coordinate endCoordinate, int Width, int Height, Block block)
        {
            //builds a pyramid top
            //need to create properties definitions for various building structure types, frames, architecture
            //Coordinate endPoint = new Coordinate(startPoint.x + Width, Height, startPoint.z + Width);

            string blockname = block.name;
            int dv;
            if (block.dataValue == 0)
            {
                dv = 0;
            }
            else
            {
                dv = block.dataValue;
            }

            for (int i = 0; i < Width / 2; i++)
            {
                Generate_Commands.Add_Command($"fill {startCoordinate.x + i} {Height + i} {startCoordinate.z + i} {endCoordinate.x - i} {Height + i} {endCoordinate.z - i} {blockname} {dv}");
            }
        }

        public void FlatRoof()
        {

        }
    }
}
