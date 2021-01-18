using Minecraft_Building_Generator.Command_Generator;
using Minecraft_Building_Generator.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Structures.Scenary
{
    public class Scenery
    {

        Coordinate StartPoint;


        Coordinate CenterlineStart;
        Coordinate CrosswalkStart;


        public Scenery()
        {

        }

        public void Build_Scenery(Grid_Square square)
        {
            //Coordinate endPoint = new Coordinate(startingPoint.x + 12, startingPoint.y, startingPoint.z + 12);

            Generate_Commands.Add_Command($"fill {square.startCoordinate.x} {square.startCoordinate.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {square.startCoordinate.z} {square.endCoordinate.x} {square.endCoordinate.y + Shared_Constants.FLAT_WORLD_STARTING_Y} {square.endCoordinate.z} concrete 13"); //13 is green concrete
            
        }
    }

}
