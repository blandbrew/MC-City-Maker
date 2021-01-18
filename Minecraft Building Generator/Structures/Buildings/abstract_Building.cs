using Minecraft_Building_Generator.Grid_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Building_Generator.Structures
{

    //public enum BuildingClass { Residential, Commercial, Industrial, Other, None}
    //public enum BuildingMaterial { Glass, Wooden, Rock, Concrete, Jewel, Other, None}

    public abstract class abstract_Building
    {
    
        
        protected Coordinate startCoordinate; //starting coordinate in a 13x13 grid

        //Building Description
        protected int NumberOfFloors { get; set; }
        protected int Height { get; set; }
        protected int Width { get; set; } = 12;
        protected int SpaceBetweenFloors { get; set; } = 4;

        protected BuildingClass building_Class;
        protected BuildingMaterial building_Category;

        




        //Subterranian Features
        //bool isUnderground;
        //int maxDepth;

        //BuildingProperties
        //block type
        //block color
        //door
        //windows
        //roof





    }
}
