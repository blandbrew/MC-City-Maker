/*Minecraft building generator
 * Grid Map Class
 * 11/21/2020
 * 
 * Description: at this time this class obtains initial coordinates and then begins dividing up the areas for building.
 * 
 * 
 * Minecrat Math:
 * 
 * Max number of block to fill = 32768
 * max square grid size = 181
 *  181 * 181 = 32761 < 32768
 *
 *  
 * Largest Perfect dividable square = 13 x 13
 * 13 x 13 = 169
 * 169 x 169 = 28561
 *  
 * 13x13 is preferable for building generation
 * 181x181 is best for terraforming
 * 
 * Note: all of the above math is done assuming the Y value is 1, or depth of fill command is 1
 * All measurments are units squared.  filling a deeper area requires volume and is not calculated at this moment (11/21/2020)
 *  
 *  Individual grid squares of 13 also works well because it divides the grid by a centerline, allowing for more complex buildings
 *  Starting coordinates:  143,63,-17


    Direction:  west and East are on X axis  (-x West ; +X East)
                north and south are on Z axis  (+Z south ; -Z north)
*/



using MC_City_Maker.Grid_Classes;
using MC_City_Maker.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MC_City_Maker
{
    public class GridMap
    {

        /*Variables*/
        public static int number_of_Grid_Containers; //size of the grid in 169x169 block chunks
        public Coordinate startCoordinate { get; set; }
        public Coordinate endCoordinate { get; set; }
        public Grid_Container[,] PrimaryGridMap { get; set; }//This 2D array stores all the Grid Containers
        Grid_Square[,] ContainerMap { get; set; }


        /// <summary>
        /// This constructor takes 4 parameters.  The start location from where the functions will run, and then how big will the generator go.
        /// This is the same as calling GridMap.GridMapProvision_Start
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="startZ"></param>
        /// <param name="number_of_Grid_Containers"></param>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// <description>Must be Even and (Max is 4)</description>
        /// </item>
        /// <item>
        /// <description>Each grid container is 169x169 blocks</description>
        /// </item>
        /// </list>
        /// </remarks>
        public GridMap(int startX, int startY, int startZ, int number_of_Grid_Containers)
        {
            startCoordinate = new Coordinate(startX, startY, startZ);

            if (number_of_Grid_Containers == 1)
            {
                GridMap.number_of_Grid_Containers = 1;  
            }
            else
            {
                GridMap.number_of_Grid_Containers = number_of_Grid_Containers;
            }
        }


        /// <summary>
        /// Initilizing Grid Map on startup or other places
        /// </summary>
        public GridMap()
        {
            //Call GridMapProvision_Start to invoke the start process if not start from GridMap Constructor
        }

        /*Methods*/
        public void GridMapProvision_Start(int startX, int startY, int startZ, int number_of_Grid_Containers)
        {
            startCoordinate = new Coordinate(startX, startY, startZ);

            if (number_of_Grid_Containers == 1)
            {
                GridMap.number_of_Grid_Containers = 1;
            }
            else
            {
                GridMap.number_of_Grid_Containers = number_of_Grid_Containers;
            }
        }




        public Grid_Container Get_Container((int, int) container_location)
        {
            //application is working but throwing errors, need to test for validity without getting errors.  length maybe?
            if (PrimaryGridMap[container_location.Item1, container_location.Item2] != null)
                return PrimaryGridMap[container_location.Item1, container_location.Item2];
            else
                return null;
        }

        public Grid_Square Get_SquareFromContainer((int, int) container_location, (int, int) square_location)
        {
            if (PrimaryGridMap[container_location.Item1, container_location.Item2].gridSquareMap[square_location.Item1, square_location.Item2] != null)
                return PrimaryGridMap[container_location.Item1, container_location.Item2].gridSquareMap[square_location.Item1, square_location.Item2];
            else
                return null;
        }

        public void SetContainer((int, int) container_location, Grid_Container aContainer)
        {
            if (PrimaryGridMap[container_location.Item1, container_location.Item2] != null)
                PrimaryGridMap[container_location.Item1, container_location.Item2] = aContainer;
        }





        /// <summary>
        /// Method to begin the process of generating all of the grids.
        /// </summary>
        public void GenerateGrids()
        {
            Generate_Grid_Containers(startCoordinate, number_of_Grid_Containers);

            Generate_Grid_Squares(this.PrimaryGridMap);

            Generate_Adjacent_Containers(this.PrimaryGridMap);

            Generate_Adjacent_Grid_Squares(this.PrimaryGridMap);

        }

        /// <summary>
        /// Private method that generates <see cref="Grid_Container"/>s
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="number_of_Grid_Containers"></param>
        private void Generate_Grid_Containers(Coordinate startPoint, int number_of_Grid_Containers)
        {
            //PrimaryGridMap = new Grid_Container[,];
            if(number_of_Grid_Containers == 1)
            {
            
                PrimaryGridMap = new Grid_Container[1,1];

                Map_out_GridContainer(startPoint, PrimaryGridMap);

            } else
            {
                
                //If the number of grid containers is larger than 1, must be an even number.
                //to make an even grid map, divide the number of containers by 2 then minus 1 to account of array's starting at 0 and instantiate the 2D array.
                /**Example
                 * 
                 * number of containers: 4
                 * (4/2) - 1 = 1
                 * primaryGridMap[1,1] => now there are 4 accessible containers
                 */


                int _container = (int)Math.Sqrt(GridMap.number_of_Grid_Containers);
                //Console.WriteLine("Size of Containers: " + _container);
                PrimaryGridMap = new Grid_Container[_container, _container];

                //Sets the primary grid Map
                Map_out_GridContainer(startPoint, PrimaryGridMap);
            }


        }


        /// <summary>
        /// Private method that maps out the coordinates for the <see cref="Grid_Container"/>s and stores each in a 2D array.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        private void Map_out_GridContainer(Coordinate startPoint, Grid_Container[,] PrimaryGridMap)
        {

            double containerSize = ContainerSize();

            int _tempX = startPoint.x;
            int _tempY = startPoint.y;
            int _tempZ = startPoint.z;

            try
            {

                for (int i = 0; i < containerSize; i++)
                {
                    for (int k = 0; k < containerSize; k++)
                    {
                      
                        Grid_Container aGridContainer = new Grid_Container(new Coordinate(_tempX, _tempY, _tempZ), (i,k));

                        aGridContainer.endCoordinate = new Coordinate
                            (
                                aGridContainer.startCoordinate.x + Shared_Constants.GRID_CONTAINER_SIZE-1,
                                startPoint.y,
                                aGridContainer.startCoordinate.z + Shared_Constants.GRID_CONTAINER_SIZE-1
                            );
                        aGridContainer.centerblock = new Coordinate
                            (
                                aGridContainer.startCoordinate.x + Shared_Constants.GRID_CONTAINER_CENTER,
                                startPoint.y,
                                aGridContainer.startCoordinate.y + Shared_Constants.GRID_CONTAINER_CENTER
                            );

                        aGridContainer.isValid = true;

                     
                        PrimaryGridMap[i, k] = aGridContainer;
                        _tempX = aGridContainer.startCoordinate.x + Shared_Constants.GRID_CONTAINER_SIZE; //adds one on to mark the start point of next grid   
                       
                }
                    if (containerSize > 1)
                    {
                        _tempX = PrimaryGridMap[i, 0].startCoordinate.x; //Resets the X coord so process can iterate across again. (like a typewriter)
                        _tempZ += Shared_Constants.GRID_CONTAINER_SIZE; //adds 1 to mark the start point of next container
                    }

                }


                //Add UI_GRIDMAP Section here, run through all the containers



                this.PrimaryGridMap = PrimaryGridMap;

        } catch(Exception err)
            {
                //MessageBox.Show(err.Message, "Grid Map Generator Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }




}


        /// <summary>
        /// Private method to generate <see cref="Grid_Square"/>s within a grid container
        /// </summary>
        /// <param name="PrimaryGridMap"></param>
        private void Generate_Grid_Squares(Grid_Container[,] PrimaryGridMap)
        {
            double containerSize = ContainerSize();

            for (int i = 0; i < containerSize; i++)
            {
                for (int k = 0; k < containerSize; k++)
                {
                    
                    
                    
                    Coordinate startPoint = PrimaryGridMap[i, k].startCoordinate;
                    int _tempX = startPoint.x;
                    int _tempY = startPoint.y;
                    int _tempZ = startPoint.z;


                    Grid_Square[,] aGridSquareMap = new Grid_Square[Shared_Constants.GRID_SQUARE_SIZE, Shared_Constants.GRID_SQUARE_SIZE];
                    Grid_Container aContainer = PrimaryGridMap[i, k];

                    for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
                    {
                        for(int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                        {
                            Grid_Square aSquare = new Grid_Square(new Coordinate(_tempX, _tempY, _tempZ), (i,k), (m,n));

                            aSquare.endCoordinate = new Coordinate
                                (
                                    aSquare.startCoordinate.x + Shared_Constants.GRID_SQUARE_SIZE-1,
                                    aSquare.startCoordinate.y,
                                    aSquare.startCoordinate.z + Shared_Constants.GRID_SQUARE_SIZE-1
                                );
                            aSquare.centerblock = new Coordinate
                                (
                                    aSquare.startCoordinate.x + Shared_Constants.GRID_SQUARE_CENTER,
                                    aSquare.startCoordinate.y,
                                    aSquare.startCoordinate.z + Shared_Constants.GRID_SQUARE_CENTER
                                );

                            aSquare.isValid = true;
                       
                            aGridSquareMap[m, n] = aSquare;

                            _tempX = aSquare.startCoordinate.x + Shared_Constants.GRID_SQUARE_SIZE;

                        }

                        _tempX = aGridSquareMap[m, 0].startCoordinate.x; //Resets the X coord so process can iterate across again. (like a typewriter)
                        _tempZ += Shared_Constants.GRID_SQUARE_SIZE; //adds 1 to mark the start point of next container

                    }

                    //Associates the new GridsquareMap to the container
                    aContainer.gridSquareMap = aGridSquareMap;
                }
            }
        }

        /// <summary>
        /// Private method to identify and store adjacent <see cref="Grid_Container"/>
        /// </summary>
        /// <param name="PrimaryGridMap"></param>
        public void Generate_Adjacent_Containers(Grid_Container[,] PrimaryGridMap)
        {

            double containerSize = ContainerSize();

            for (int i = 0; i < containerSize; i++)
            {
                for (int k = 0; k < containerSize; k++)
                {
                    Grid_Container aContainer = PrimaryGridMap[i, k];


                    //This section is to test for adjacency and then associate the adjacent unit.
                    //Minecraft only has to test 4 directions since we are not considering diagnal adjacency
                    try
                    {

                        //before
                        if (k - 1 >= 0 && PrimaryGridMap[i, k - 1].isValid)
                        {

                            aContainer.Add_Adjacent_Container(PrimaryGridMap[i, k - 1]);
                        }
                        //next
                        if ((k + 1 < containerSize) && PrimaryGridMap[i, k + 1].isValid)
                        {
                            aContainer.Add_Adjacent_Container(PrimaryGridMap[i, k + 1]);
                        }
                        //above
                        if ((i + 1 < containerSize) && PrimaryGridMap[i + 1, k].isValid)
                        {
                            aContainer.Add_Adjacent_Container(PrimaryGridMap[i + 1, k]);
                        }
                        //below
                        if ((i - 1 >= 0) && PrimaryGridMap[i - 1, k].isValid)
                        {
                            aContainer.Add_Adjacent_Container(PrimaryGridMap[i - 1, k]);
                        }

                    }
                    catch (IndexOutOfRangeException err)
                    {
                        Console.WriteLine(err);
                    }
                }
            }

        }

        /// <summary>
        /// Private method to identify and store adjacent <see cref="Grid_Square"/>
        /// </summary>
        /// <param name="PrimaryGridMap"></param>
        private void Generate_Adjacent_Grid_Squares(Grid_Container[,] PrimaryGridMap)
        {
            double containerSize = ContainerSize();

            for (int i = 0; i < containerSize; i++)
            {
                for (int j = 0; j < containerSize; j++)
                {
                    Grid_Container aContainer = PrimaryGridMap[i, j];
                    Grid_Square[,] aGridSquareMap = aContainer.gridSquareMap;

                    for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
                    {
                        for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                        {

                            Grid_Square aGridSquare = aGridSquareMap[m, n];
                            try
                            {
                                //before
                                if ((n - 1 >= 0) && aGridSquareMap[m, n - 1].isValid)
                                {
                                    aGridSquare.Add_Adjacent_Square(aGridSquareMap[m, n - 1]);
                                }
                                //next
                                if ((n + 1 < Shared_Constants.GRID_SQUARE_SIZE) && aGridSquareMap[m, n + 1].isValid)
                                {
                                    aGridSquare.Add_Adjacent_Square(aGridSquareMap[m, n + 1]);
                                }
                                //above
                                if ((m + 1 < Shared_Constants.GRID_SQUARE_SIZE) && aGridSquareMap[m + 1, n].isValid)
                                {
                                    aGridSquare.Add_Adjacent_Square(aGridSquareMap[m + 1, n]);
                                }
                                //below
                                if ((m - 1 >= 0) && aGridSquareMap[m - 1, n].isValid)
                                {
                                    aGridSquare.Add_Adjacent_Square(aGridSquareMap[m - 1, n]);
                                }

                                (bool, string) edgeResult = EdgeTest(i, j, m, n);

                                if (edgeResult.Item1)
                                    GetAdjacentSquareFromAnotherContainer(aGridSquare, edgeResult.Item2);

                            }
                            catch (IndexOutOfRangeException err)
                            {
                                Console.WriteLine(err);
                            }

                        }

                    }

                }
            }
        }


        private (bool, string) EdgeTest(int i, int j, int m, int n)
        {
            //not working because of the limits imposed by i != 0, what happens if i does equal zero....
            if (m == 0 && i - 1 >= 0)
                return (true, "north");
            else if (m == 12 && i + 1 <= 12)
                return (true, "south");
            else if (n == 0 && j - 1 >= 0)
                return (true, "West");
            else if (n == 12 && j <= 12)
                return (true, "East");

            return (false, "none");
        }


        private void GetAdjacentSquareFromAnotherContainer(Grid_Square current_square, string direction)
        {
            //UI_Grid_Planning_Square adjacentsquare;
            int row;
            int col;
            (int, int) adjSquareCoordinate = current_square.SquareArrayCoordinate;


            switch (direction)
            {
                case "north":
                    row = -1;
                    col = 0;
                    adjSquareCoordinate.Item1 = 12;
                    break;
                case "east":
                    row = 0;
                    col = 1;
                    adjSquareCoordinate.Item2 = 0;
                    break;
                case "south":
                    row = 1;
                    col = 0;
                    adjSquareCoordinate.Item1 = 0;
                    break;
                case "west":
                    row = 0;
                    col = -1;
                    adjSquareCoordinate.Item2 = 12;
                    break;
                default:
                    return;
            }

            (int, int) parentContainer = current_square.ParentContainerArrayCoordinate;

            //get a container that is above this container
            Grid_Container adjContainer = Get_Container((parentContainer.Item1 + row, parentContainer.Item2 + col));
            Grid_Square adjSquare = adjContainer.gridSquareMap[adjSquareCoordinate.Item1, adjSquareCoordinate.Item2];

            //current grid square, need to know adjacnet one in another container
            //aGridSquare
            //Console.WriteLine("Parent square: " + current_square.SquareCoordinate);
            //Console.WriteLine("adjcent square: " + adjSquare.SquareCoordinate);
            current_square.adjacent_Squares.Add(adjSquare);
            adjSquare.adjacent_Squares.Add(current_square);
        }










        public double ContainerSize()
        {
            if (number_of_Grid_Containers == 1)
                return 1;
            else
                return Math.Sqrt(number_of_Grid_Containers);
        }
    }
}
