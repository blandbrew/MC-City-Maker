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



using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Classes;
using MC_City_Maker.UI;
using System;
using System.Diagnostics;
using System.Windows.Media;

namespace MC_City_Maker
{
    public class GridMap
    {

        /*Variables*/
        public int gridSize { get; private set; }//size of the grid in 169x169 block chunks
        public Coordinate startCoordinate { get; set; }
        public Coordinate endCoordinate { get; set; }
        public Grid_Container[,] PrimaryGridMap { get; set; }//This 2D array stores all the Grid Containers
        Grid_Square[,] ContainerMap { get; set; }
        public Grid_Container PreviouslySelected_container { get; set; }


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
                gridSize = 1;  
            }
            else
            {
                gridSize = number_of_Grid_Containers;
            }

            GenerateGrids();
        }


        /// <summary>
        /// Initilizing Grid Map on startup or other places
        /// </summary>
        public GridMap()
        {
            //Call GridMapProvision_Start to invoke the start process if not start from GridMap Constructor
        }



        /*Methods*/
        
        
        //marked for deletion
        //public void GridMapProvision_Start(int startX, int startY, int startZ, int number_of_Grid_Containers)
        //{
        //    startCoordinate = new Coordinate(startX, startY, startZ);

        //    if (number_of_Grid_Containers == 1)
        //    {
        //        GridMap.gridSize = 1;
        //    }
        //    else
        //    {
        //        GridMap.gridSize = number_of_Grid_Containers;
        //    }
        //}



        /// <summary>
        /// Retrieve a Grid_Container
        /// </summary>
        /// <param name="container_location"></param>
        /// <returns></returns>
        public Grid_Container Get_Container((int, int) container_location)
        {
            //application is working but throwing errors, need to test for validity without getting errors.  length maybe?
            if (PrimaryGridMap[container_location.Item1, container_location.Item2] != null)
                return PrimaryGridMap[container_location.Item1, container_location.Item2];
            else
                return null;
        }

        /// <summary>
        /// Retrieve a grid_squre from a container
        /// </summary>
        /// <param name="container_location"></param>
        /// <param name="square_location"></param>
        /// <returns></returns>
        public Grid_Square Get_SquareFromContainer((int, int) container_location, (int, int) square_location)
        {
            if (PrimaryGridMap[container_location.Item1, container_location.Item2].gridSquareMap[square_location.Item1, square_location.Item2] != null)
                return PrimaryGridMap[container_location.Item1, container_location.Item2].gridSquareMap[square_location.Item1, square_location.Item2];
            else
                return null;
        }

        /// <summary>
        /// Set a container
        /// </summary>
        /// <param name="container_location"></param>
        /// <param name="aContainer"></param>
        public void SetContainer((int, int) container_location, Grid_Container aContainer)
        {
            if (PrimaryGridMap[container_location.Item1, container_location.Item2] != null)
                PrimaryGridMap[container_location.Item1, container_location.Item2] = aContainer;
        }


        /// <summary>
        /// Handles selection of a container
        /// </summary>
        /// <param name="selected"></param>
        public void SelectedContainer(Grid_Container selected)
        {
            //If true, reset the container.  if False, set to true and mark as selected

            if (PreviouslySelected_container == null)
            {
                PreviouslySelected_container = selected;
                selected.FillColor = UI_Constants.Selected_Container_Color;
                
                return;
            }


            if (PreviouslySelected_container != selected)
            {
                PreviouslySelected_container.FillColor = UI_Constants.Unselected_grid;
                PreviouslySelected_container = selected;
                selected.FillColor = UI_Constants.Selected_Container_Color;
                return;
            }
       
        }

        /************************************************************************
         * Grid Tools
         * 
         * - Select
         * - Place
         * - Delete
         * 
         * 
         * 
         */



        //Depricated - Looking to remove this feature
        /// <summary>
        /// Selects the UI Square for editing properties
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="zone"></param>
        //public void SelectSquare(Grid_Square selected, GridSquare_Zoning zone)
        //{
        //    if (selected.Selected == false)
        //    {
        //        selected.OutlineColor = UI_Constants.GetZoningColor(zone);
                
        //        selected.Selected = true;
        //        return;
        //    }

        //    if (selected.Selected == true)
        //    {
        //        selected.OutlineColor = UI_Constants.GetZoningColor(zone);
        //        selected.Selected = true;

        //        selected.FillColor = UI_Constants.GetZoningColor(zone);
                
        //        return;
        //    }
        //}


        public void PlaceSquare(Grid_Square selected, GridSquare_Zoning zone)
        {
            if (selected.Selected == false)
            {
                selected.FillColor = UI_Constants.GetZoningColor(zone);
                selected.Zone = zone;
                selected.Selected = true;
                return;
            }

            //This overrides the existing building
            if (selected.Selected == true && selected.Zone != zone)
            {

                selected.FillColor = UI_Constants.GetZoningColor(zone);
                selected.Zone = zone;
                return;
            }
        }

        /// <summary>
        /// Specify the Length and the width of the placed zone.  The clicked square
        /// will be designated the start square and squares are evaluated left to right then top to bottom and repeat.
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="zone"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        public void PlaceSquare(Grid_Square selected, GridSquare_Zoning zone, int length, int width)
        {
            if (selected.Selected == false)
            {

                try
                {

                    for (int i = 0; i < length; i++)
                    {
                        //get squares from y

                        for (int j = 0; j < width; j++)
                        {
                            //get squares from x
                            Grid_Square _tempSquare2 = Get_SquareFromContainer(selected.ParentContainerArrayCoordinate, (selected.SquareArrayCoordinate.Item1 + i, selected.SquareArrayCoordinate.Item2 + j));

                            if( i == length-1 && j == width-1)
                            {
                                //end Square
                                //sets the end square in the start square
                                selected.EntityEndSquare = _tempSquare2;

                                Color greyRed = new Color();
                                greyRed = Color.FromRgb(99, 75, 70);
                                //Sets the properties for the square
                                _tempSquare2.FillColor = greyRed;
                                _tempSquare2.Zone = zone;
                                _tempSquare2.Selected = true;

                            } else if(i == 0 && j ==0)
                            {
                                Color greyRed = new Color();
                                greyRed = Color.FromRgb(99, 75, 70);
                                //start square
                                selected.IsEntityStartSquare = true;
                                selected.FillColor = greyRed;
                                selected.Zone = zone;
                                selected.Selected = true;
                            } else
                            {
                                //Secondary squares
                                //Every secondary square has reference back to the start square which has reference to the end square as well.
                                
                                //added to the list of secondary squares which is stored in the start square
                                selected.EntitySecondarySquareList.Add(_tempSquare2);

                                //sets the properties of the secondary squares.
                                _tempSquare2.EntityStartSquare = selected;
                                _tempSquare2.IsEntitySecondarySquare = true;
                                _tempSquare2.FillColor = UI_Constants.GetZoningColor(zone);
                                _tempSquare2.Zone = zone;
                                _tempSquare2.Selected = true;
                                
                                
                            }
                        }

                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.WriteLine("index error");
                }



                return;
            }

            //This overrides the existing building
            if (selected.Selected == true && selected.Zone != zone)
            {
                Debug.WriteLine("OverWriting zone");
                selected.FillColor = UI_Constants.GetZoningColor(zone);
                selected.Zone = zone;
                return;
            }
        }



        public void DeleteSquare(Grid_Square selected)
        {
            if(selected.Selected == true)
            {
                //clear the square
                selected.FillColor = UI_Constants.GetZoningColor(GridSquare_Zoning.None);
                selected.Zone = GridSquare_Zoning.None;
                selected.Selected = false;
            }
        }

        /// <summary>
        /// Method to begin the process of generating all of the grids.
        /// </summary>
        private void GenerateGrids()
        {
            PrimaryGridMap = new Grid_Container[gridSize, gridSize];
            Map_out_GridContainer();
            InitializeUIGridContainer();

            Generate_Grid_Squares();
            InitializeUIGridSquare();

            
            Generate_Adjacent_Containers();
            Generate_Adjacent_Grid_Squares();

        }

        /// <summary>
        /// Generates a New Grid based on a new starting point and gridsize
        /// </summary>
        public void GenerateGrids(Coordinate newStartingCoordinate, int newGridSize)
        {

            startCoordinate = newStartingCoordinate;
            gridSize = newGridSize;
            PrimaryGridMap = new Grid_Container[gridSize, gridSize];

           
            Map_out_GridContainer();
            InitializeUIGridContainer();
            
            Generate_Grid_Squares();
            InitializeUIGridSquare();

            Generate_Adjacent_Containers();
            Generate_Adjacent_Grid_Squares();

        }




        /// <summary>
        /// Private method that maps out the coordinates for the <see cref="Grid_Container"/>s and stores each in a 2D array.
        /// </summary>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        private void Map_out_GridContainer()
        {

            int _tempX = startCoordinate.x;
            int _tempY = startCoordinate.y;
            int _tempZ = startCoordinate.z;

            try
            {

                for (int i = 0; i < gridSize; i++)
                {
                    for (int k = 0; k < gridSize; k++)
                    {
                      
                        Grid_Container aGridContainer = new Grid_Container(new Coordinate(_tempX, _tempY, _tempZ), (i,k));

                        aGridContainer.endCoordinate = new Coordinate
                            (
                                aGridContainer.startCoordinate.x + Shared_Constants.GRID_CONTAINER_SIZE-1,
                                startCoordinate.y,
                                aGridContainer.startCoordinate.z + Shared_Constants.GRID_CONTAINER_SIZE-1
                            );
                        aGridContainer.centerblock = new Coordinate
                            (
                                aGridContainer.startCoordinate.x + Shared_Constants.GRID_CONTAINER_CENTER,
                                startCoordinate.y,
                                aGridContainer.startCoordinate.y + Shared_Constants.GRID_CONTAINER_CENTER
                            );

                        aGridContainer.isValid = true;

                     
                        PrimaryGridMap[i, k] = aGridContainer;
                        _tempX = aGridContainer.startCoordinate.x + Shared_Constants.GRID_CONTAINER_SIZE; //adds one on to mark the start point of next grid   
                       
                }
                    if (gridSize > 1)
                    {
                        _tempX = PrimaryGridMap[i, 0].startCoordinate.x; //Resets the X coord so process can iterate across again. (like a typewriter)
                        _tempZ += Shared_Constants.GRID_CONTAINER_SIZE; //adds 1 to mark the start point of next container
                    }

                }




        } catch(Exception err)
            {
                //MessageBox.Show(err.Message, "Grid Map Generator Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(err);
            }

        }

        private void InitializeUIGridContainer()
        {
          

            //ui_gridContainer_array = new UI_GridContainer[gridSize, gridSize];
            int separatorValue = 17;
            int x = 20;
            int y = 20;

            //initialize 2d array of grid planner
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    /*Add all values for UI container to this location*/

                    PrimaryGridMap[i, j].Set_Grid_ContainerUIvalues(x, y, Colors.White, Colors.White, (i, j));

                    x += separatorValue;
                }
                x = 20;
                y += separatorValue;
         
            }
        }


        /// <summary>
        /// Private method to generate <see cref="Grid_Square"/>s within a grid container
        /// </summary>
        /// <param name="PrimaryGridMap"></param>
        private void Generate_Grid_Squares()
        {
          
            for (int i = 0; i < gridSize; i++)
            {
                for (int k = 0; k < gridSize; k++)
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
                            //Debug.WriteLine("Default zone = " + aSquare.Zone);
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

        private void InitializeUIGridSquare()
        {
            Grid_Container selected_container;
            int separatorValue = 23;
            int x = 10;
            int y = 10;

            //initialize 2d array of grid planner
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    //Console.WriteLine("Initalize grid3");
                    selected_container = PrimaryGridMap[i, j];
                    Grid_Square[,] _uiGridSquares = selected_container.gridSquareMap;
                    //Console.WriteLine("Initalize grid4");
                    for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
                    {
                        for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                        {
                            //Console.WriteLine("Initalize grid5");
                            //_uiGridSquares[m, n] = new UI_Grid_Square(x, y, Shared_Constants.UI_GRID_RECTANGLE_SIZE + 7, Shared_Constants.UI_GRID_RECTANGLE_SIZE + 7, Colors.White, Colors.White, (i, j), (m, n));
                            _uiGridSquares[m, n].SetGrid_Square_UI(x, y, Shared_Constants.UI_GRID_RECTANGLE_SIZE + 7, Shared_Constants.UI_GRID_RECTANGLE_SIZE + 7, Colors.White, Colors.White, (i, j), (m, n));
                           
                            x += separatorValue;
                            //Console.WriteLine("Initalize grid6");
                        }

                        x = 10;
                        y += separatorValue;
                    }
                    selected_container.gridSquareMap = _uiGridSquares;
                    x = 10;
                    y = 10;
                }

            }
        }


        /// <summary>
        /// Private method to identify and store adjacent <see cref="Grid_Container"/>
        /// </summary>
        /// <param name="PrimaryGridMap"></param>
        public void Generate_Adjacent_Containers()
        {
            

            for (int i = 0; i < gridSize; i++)
            {
                for (int k = 0; k < gridSize; k++)
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
                        if ((k + 1 < gridSize) && PrimaryGridMap[i, k + 1].isValid)
                        {
                            aContainer.Add_Adjacent_Container(PrimaryGridMap[i, k + 1]);
                            
                        }
                        //above
                        if ((i + 1 < gridSize) && PrimaryGridMap[i + 1, k].isValid)
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
                        Debug.WriteLine("adjacent container error");
                    }
                }
            }

        }

        /// <summary>
        /// Private method to identify and store adjacent <see cref="Grid_Square"/>
        /// </summary>
        /// <param name="PrimaryGridMap"></param>
        private void Generate_Adjacent_Grid_Squares()
        {
            string error = "";

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {

                    Grid_Container aContainer = PrimaryGridMap[i, j];
                    Grid_Square[,] aGridSquareMap = aContainer.gridSquareMap;

                    for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE-1; m++)
                    {
                        for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE-1; n++)
                        {
                            
                            Grid_Square aGridSquare = aGridSquareMap[m, n];

                            try
                            {
                                
                                //before
                                if ((n - 1 >= 0) && aGridSquareMap[m, n - 1].isValid)
                                {
                                    error = "before";
                                    aGridSquare.Add_Adjacent_Square(aGridSquareMap[m, n - 1]);
                                    
                                }
                                //next
                                if ((n + 1 < Shared_Constants.GRID_SQUARE_SIZE) && aGridSquareMap[m, n + 1].isValid)
                                {
                                    error = "next";
                                    aGridSquare.Add_Adjacent_Square(aGridSquareMap[m, n + 1]);
                                    
                                }
                                //above
                                if ((m + 1 < Shared_Constants.GRID_SQUARE_SIZE) && aGridSquareMap[m + 1, n].isValid)
                                {
                                    error = "above";
                                    aGridSquare.Add_Adjacent_Square(aGridSquareMap[m + 1, n]);
                                    
                                }
                                //below
                                if ((m - 1 >= 0) && aGridSquareMap[m - 1, n].isValid)
                                {
                                    error = "below"; //errors look like they are coming from here
                                    aGridSquare.Add_Adjacent_Square(aGridSquareMap[m - 1, n]);
                                    
                                }

                                (bool, string) edgeResult = EdgeTest(i, j, m, n);

                                if (edgeResult.Item1)
                                    GetAdjacentSquareFromAnotherContainer(aGridSquare, edgeResult.Item2);

                            }
                            catch (IndexOutOfRangeException err)
                            {
                                Debug.WriteLine(error);
                                Debug.WriteLine(err.Message);
                                
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
            Console.WriteLine("Here1");
            //current grid square, need to know adjacnet one in another container
            //aGridSquare
            //Console.WriteLine("Parent square: " + current_square.SquareCoordinate);
            //Console.WriteLine("adjcent square: " + adjSquare.SquareCoordinate);
            current_square.adjacent_Squares.Add(adjSquare);
            adjSquare.adjacent_Squares.Add(current_square);
        }

    }
}
