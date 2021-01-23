/**
  * The build manager will be responsible for handling the buliding 
  */

using MC_City_Maker.Command_Generator;
using MC_City_Maker.Grid_Classes;
using MC_City_Maker.Structures.Infrustructure;
using MC_City_Maker.Structures.Scenary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_City_Maker.Structures
{
    public class Build_Manager
    {

        public int DensityFactor { get; set; } //How dense shall the generated city be?

        private Grid_Container[,] FullGridMap { get; set; }
        private int TotalNumberContainers;

        


        public Build_Manager(Grid_Container[,] map)
        {
            FullGridMap = map;
        }


        //Loops through the grid container/squares and runs build commands
        //This is where types of buildings will need to be identified and 
        public void Process_Containers()
        {
            Random rand = new Random();
            //loop through containers
            for (int i = 0; i < Math.Sqrt(GridMap.number_of_Grid_Containers); i++)
            {
                for (int j = 0; j < Math.Sqrt(GridMap.number_of_Grid_Containers); j++)
                {
                    Grid_Container aContainer = FullGridMap[i, j];
                    Grid_Square[,] squareMap = aContainer.gridSquareMap;

                    for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
                    {
                        for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                        {
                            Grid_Square aSquare = squareMap[m, n];

                            switch(aSquare.zone)
                            {
                                case GridSquare_Zoning.Building:
                                    int randomHeight = rand.Next(20, 40);
                                    while (randomHeight % 4 != 0)//ensures that every building has a rooftop
                                    {
                                        randomHeight = rand.Next(20, 40);
                                    }

                                    GenericBuilding gb = new GenericBuilding(randomHeight, BuildingClass.Commercial);

                                    gb.Building_OutsideWalls(aSquare);
                                    gb.Building_Floor(aSquare);
                                    gb.Building_Rooftop(aSquare);

                                    break;
                                case GridSquare_Zoning.Infrustructure:
                                    Roads road = new Roads();
                                    //road.Build_Road(aSquare.startCoordinate, Direction.North);
                                    HashSet<Grid_Square> squares = aSquare.GetAll_Adjacent_Squares();
                                    road.Road_Adjacency(squares, aSquare);

                                    
                                    break;

                                case GridSquare_Zoning.Scenery:
                                    Scenery grass = new Scenery();
                                    grass.Build_Scenery(aSquare);
                                    break;

                                case GridSquare_Zoning.Water:

                                    break;
                            }



                        }
                    }
                }
            }
        }



    }
}
