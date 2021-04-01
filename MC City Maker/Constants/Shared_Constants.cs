using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker
{

    //public enum gridsquare_legend { Initialized, Selected, Infrustructure, Building, Scenery, water };
    public enum GridSquare_Zoning {None, Selected, Building, Infrustructure, Scenery, Water }
    public enum BuildingClass { None, Residential, Commercial, Industrial, Government, Educational, Recreational, Landmarks }
    public enum BuildingTypeResidential { None, Residential, Commercial, Industrial, Utility, Service }


    public enum InfrustructureClass { None, Road, Rail, Subway, bridge, Wall }
    public enum SceneryClass { None, Park, Playground, Garden, Other }
    public enum WaterClass { None, pond, lake, coast, waterfall, river}



    public enum Direction { North, East, South, West }



    public static class Shared_Constants
    {
        
        public const int DEMO_STARTING_X = 143;
        public const int DEMO_STARTING_Y = 75;
        public const int DEMO_STARTING_Z = -17;


        public const int GRID_CONTAINER_SIZE = 169;
        public const int GRID_SQUARE_SIZE = 13;
        
        public const int MAX_FILL = 32768;
        public const int GRID_CONTAINER_CENTER = 85;
        public const int GRID_SQUARE_CENTER = 6;

        public const int UI_GRID_RECTANGLE_SIZE = 13;

        public const int MAX_NUMBER_OF_COMMANDS = 9950;

        public const int FLAT_WORLD_STARTING_Y = 3;

        public const string BUILDING = "Building";
        public const string INFRUSTRUCTURE = "Infrustructure";
        public const string WATER = "Water";
        public const string SCENARY = "Scenary";


    }
}
