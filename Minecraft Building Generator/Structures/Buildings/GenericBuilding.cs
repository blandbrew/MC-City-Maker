using Minecraft_Building_Generator.Command_Generator;
using Minecraft_Building_Generator.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Structures
{
    public class GenericBuilding : abstract_Building, IBuilding
    {
        //inherited variables
        //protected int NnumberOfFloors { get; }
        //protected int MaxHeight { get; }
        //protected int MaxWidth { get; set; } = 13;
        //protected int SpaceBetweenFloors { get; set; } = 4;

        string buildingType;
        string color;
        string blockType;
        

        public GenericBuilding(int height, BuildingClass buildingClass)
        {
            Height = height;
        }

        public GenericBuilding()
        {

        }

        public void Building_OutsideWalls(Grid_Square square)
        {

            
            //Coordinate endPoint = new Coordinate(startingPoint.x + Width, startingPoint.y + Height, startingPoint.z + Width);

            //adds hollow command
            Generate_Commands.Add_Command($"fill {square.startCoordinate.x} {square.startCoordinate.y+Shared_Constants.FLAT_WORLD_STARTING_Y} {square.startCoordinate.z} {square.endCoordinate.x} {square.endCoordinate.y + Height} {square.endCoordinate.z} glass 1 hollow");

            //throw new NotImplementedException();
        }

        public void Building_Floor(Grid_Square square)
        {
            NumberOfFloors = Height / 4; //adds an additional floor onto the building height...may need to reconsider this
            for (int i = 0; i < NumberOfFloors; i++)
            {

                //Coordinate endPoint = new Coordinate(startingPoint.x + Width, startingPoint.y, startingPoint.z + Width );
                square.startCoordinate.y += 4;
                square.endCoordinate.y += 4;
                Generate_Commands.Add_Command($"fill {square.startCoordinate.x} {square.startCoordinate.y} {square.startCoordinate.z} {square.endCoordinate.x} {square.endCoordinate.y} {square.endCoordinate.z} stone");
                
            }
        }

        public void Building_Lighting()
        {
            throw new NotImplementedException();
        }

        public void Building_Door(string direction)
        {
            throw new NotImplementedException();
        }

        public void Building_Ladder()
        {
            throw new NotImplementedException();
        }

        public void Building_Windows()
        {
            throw new NotImplementedException();
        }

        public void Building_Rooftop(Grid_Square square)
        {
            //builds a pyramid top
            //need to create properties definitions for various building structure types, frames, architecture
            //Coordinate endPoint = new Coordinate(startPoint.x + Width, Height, startPoint.z + Width);

            for(int i = 0; i < Width/2; i++)
            {
                Generate_Commands.Add_Command($"fill {square.startCoordinate.x+i} {Height+i} {square.startCoordinate.z+i} {square.endCoordinate.x-i} {Height+i} {square.endCoordinate.z-i} concrete 9");
            }

            //throw new NotImplementedException();
        }
    }
}
