using MC_City_Maker.Constants;
using MC_City_Maker.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MC_City_Maker.UI.Model
{
    class UI_GridMap
    {

        private UI_GridContainer[,] ui_gridContainer_array { get; set; }

        public int gridSize { get; private set; }
        //public int previousSizeOfGrid { get; set; }

        public UI_GridContainer PreviouslySelected_container { get; set; }



        //private UI_GridContainer _Selected_container;
        //public UI_GridContainer Selected_container
        //{
        //    get { return _Selected_container; }
        //    set { _Selected_container = value; RaisePropertyChanged(nameof(Selected_container)); }

        //}

        //private UI_GridContainer _PreviouslySelected_container;
        //public UI_GridContainer PreviouslySelected_container
        //{
        //    get { return _PreviouslySelected_container; }
        //    set { _PreviouslySelected_container = value; RaisePropertyChanged(nameof(PreviouslySelected_container)); }

        //}

        public UI_GridContainer GetContainer((int, int) container_location)
        {
            //application is working but throwing errors, need to test for validity without getting errors.  length maybe?
            if (ui_gridContainer_array[container_location.Item1, container_location.Item2] != null)
                return ui_gridContainer_array[container_location.Item1, container_location.Item2];
            else
                return null;

        }

        public UI_Grid_Square GetSquareFromContainer((int, int) container_location, (int, int) square_location)
        {
            if (ui_gridContainer_array[container_location.Item1, container_location.Item2].ui_GridSquares_array[square_location.Item1, square_location.Item2] != null)
                return ui_gridContainer_array[container_location.Item1, container_location.Item2].ui_GridSquares_array[square_location.Item1, square_location.Item2];
            else
                return null;
        }

        public void setCointainer((int, int) container_location, UI_GridContainer aContainer)
        {
            if (ui_gridContainer_array[container_location.Item1, container_location.Item2] != null)
                ui_gridContainer_array[container_location.Item1, container_location.Item2] = aContainer;

        }

        public void SelectedContainer(UI_GridContainer selected)
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

            }
        }

        public void SelectedSquare(UI_Grid_Square selected, GridSquare_Zoning zone)
        {
            if(selected.Selected == false)
            {
                selected.FillColor = UI_Constants.GetZoningColor(zone);
                selected.Zone = zone;
                selected.Selected = true;
                return;
            }
            if (selected.Selected == true && selected.Zone != zone)
            {
               
                selected.FillColor = UI_Constants.GetZoningColor(zone);
                selected.Zone = zone;
                return;
            }

            if (selected.Selected == true)
            {
                //clear the square
                selected.FillColor = UI_Constants.GetZoningColor(GridSquare_Zoning.None);
                selected.Zone = GridSquare_Zoning.None;
                selected.Selected = false;  
            }
        }







        public UI_GridContainer[,] InitializeGrid(int gSize)
        {
            gridSize = gSize;
            InitializeUIGridContainer();
            InitializeUIGridSquare();
            
            return ui_gridContainer_array;
        }

        private void InitializeUIGridContainer()
        {
            ui_gridContainer_array = new UI_GridContainer[gridSize, gridSize];
            int separatorValue = 17;
            int x = 20;
            int y = 20;

            //initialize 2d array of grid planner
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    //Console.WriteLine("Initalize grid1");
                    ui_gridContainer_array[i,j] = new UI_GridContainer(x, y, Colors.White, Colors.White, (i,j));
                    x += separatorValue;
                }
                x = 20;
                y += separatorValue;
                //Console.WriteLine("Initalize grid2");
            }
        }

        private void InitializeUIGridSquare()
        {
            UI_GridContainer selected_container;
            int separatorValue = 23;
            int x = 10;
            int y = 10;

            //initialize 2d array of grid planner
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    //Console.WriteLine("Initalize grid3");
                    selected_container = ui_gridContainer_array[i, j];
                    UI_Grid_Square[,] _uiGridSquares = new UI_Grid_Square[Shared_Constants.GRID_SQUARE_SIZE, Shared_Constants.GRID_SQUARE_SIZE];
                    //Console.WriteLine("Initalize grid4");
                    for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
                    {
                        for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                        {
                            //Console.WriteLine("Initalize grid5");
                            _uiGridSquares[m, n] = new UI_Grid_Square(x, y,Shared_Constants.UI_GRID_RECTANGLE_SIZE+7, Shared_Constants.UI_GRID_RECTANGLE_SIZE+7, Colors.White, Colors.White, (i, j), (m, n));

                            x += separatorValue;
                            //Console.WriteLine("Initalize grid6");
                        }

                        x = 10;
                        y += separatorValue;
                    }
                    selected_container.ui_GridSquares_array = _uiGridSquares;
                    x = 10;
                    y = 10;
                }
               
            }

            //AdjacenctContainerCalculation();
            //AdjacenctSquaresCalculation();
        }


        /// <summary>
        /// Calculates the Adjacent Containers and adds them to the list
        /// </summary>
        private void AdjacenctContainerCalculation()
        {

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    UI_GridContainer aContainer = ui_gridContainer_array[i, j];

                    //Console.WriteLine(UI_Grid_Container[i, j+1].ContainerCoordinate);
                    //This section is to test for adjacency and then associate the adjacent unit.
                    //Minecraft only has to test 4 directions since we are not considering diagnal adjacency
                    //before
                    if (j - 1 >= 0 && ui_gridContainer_array[i, j - 1] != null)
                    {

                        aContainer.AdjacentContainers.Add(ui_gridContainer_array[i, j - 1]);
                    }
                    //next
                    if ((j + 1 < gridSize) && ui_gridContainer_array[i, j + 1] != null)
                    {
                        //Console.WriteLine("ADDED CONTAINER");
                        aContainer.AdjacentContainers.Add(ui_gridContainer_array[i, j + 1]);
                    }
                    //above
                    if ((i + 1 < gridSize) && ui_gridContainer_array[i + 1, j] != null)
                    {

                        aContainer.AdjacentContainers.Add(ui_gridContainer_array[i + 1, j]);
                    }
                    //below
                    if ((i - 1 >= 0) && ui_gridContainer_array[i - 1, j] != null)
                    {

                        aContainer.AdjacentContainers.Add(ui_gridContainer_array[i - 1, j]);
                    }

                }
            }
        }


        private void AdjacenctSquaresCalculation()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    UI_GridContainer aContainer = ui_gridContainer_array[i, j];
                    UI_Grid_Square[,] aGridSquareMap = aContainer.ui_GridSquares_array;


                    for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
                    {
                        for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                        {

                            UI_Grid_Square aGridSquare = aGridSquareMap[m, n];

                            try
                            {
                                //left
                                if ((n - 1 >= 0) && aGridSquareMap[m, n - 1] != null)
                                {
                                    aGridSquare.AdjacentSquares.Add(aGridSquareMap[m, n - 1]);
                                }
                                //right
                                if ((n + 1 < Shared_Constants.GRID_SQUARE_SIZE) && aGridSquareMap[m, n + 1] != null)
                                {

                                    aGridSquare.AdjacentSquares.Add(aGridSquareMap[m, n + 1]);
                                }
                                //below
                                if ((m + 1 < Shared_Constants.GRID_SQUARE_SIZE) && aGridSquareMap[m + 1, n] != null)
                                {
                                    aGridSquare.AdjacentSquares.Add(aGridSquareMap[m + 1, n]);
                                }
                                //above
                                if ((m - 1 >= 0) && aGridSquareMap[m - 1, n] != null)
                                {
                                    aGridSquare.AdjacentSquares.Add(aGridSquareMap[m - 1, n]);
                                }

                                (bool, string) edgeResult = EdgeTest(i, j, m, n);

                                if (edgeResult.Item1)
                                {
                                    Console.WriteLine(edgeResult.Item2);
                                    GetAdjacentSquareFromAnotherContainer(aGridSquare, edgeResult.Item2);
                                }

                            }
                            catch (IndexOutOfRangeException err)
                            {
                                //Console.WriteLine(err);
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
            else if (m == 12 && i + 1 <= gridSize)
                return (true, "south");
            else if (n == 0 && j - 1 >= 0)
                return (true, "west");
            else if (n == 12 && j <= gridSize)
                return (true, "east");

            return (false, "none");
        }

        private void GetAdjacentSquareFromAnotherContainer(UI_Grid_Square current_square, string direction)
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
            UI_GridContainer adjContainer = GetContainer((parentContainer.Item1 + row, parentContainer.Item2 + col));
            UI_Grid_Square adjSquare = adjContainer.ui_GridSquares_array[adjSquareCoordinate.Item1, adjSquareCoordinate.Item2];

            //current grid square, need to know adjacnet one in another container
            //aGridSquare
            Console.WriteLine("direction: " + direction);
            //Console.WriteLine("Parent square: " + current_square.SquareArrayCoordinate);
            //Console.WriteLine("adjcent square: " + adjSquare.SquareArrayCoordinate);
            current_square.AdjacentSquares.Add(adjSquare);
            adjSquare.AdjacentSquares.Add(current_square);
        }
    }
}
