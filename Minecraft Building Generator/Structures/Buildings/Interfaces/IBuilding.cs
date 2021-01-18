/**
 * This interfaces defines a building and the methods that need to execute for each building
 */


using Minecraft_Building_Generator.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator
{
    interface IBuilding
    {
        void Building_OutsideWalls(Grid_Square square); //fill hollow
        void Building_Floor(Grid_Square square); //startpoint / endpoint
        void Building_Lighting();
        void Building_Door(string direction);
        void Building_Ladder();
        void Building_Windows();
        void Building_Rooftop(Grid_Square square);



    }
}
