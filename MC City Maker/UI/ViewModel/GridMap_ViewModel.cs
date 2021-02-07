using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Classes;
using MC_City_Maker.Structures;
using MC_City_Maker.UI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

//YOU CAN ONLY BIND PROPERTIES and NOT! Individual variables!


namespace MC_City_Maker.UI.ViewModel
{
    public class GridMap_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private GridMap gridMap;

        /*Menu File Commands - vm prefix is View Model*/
        private ICommand vm_NewCity;
        private ICommand vm_OpenCity;
        private ICommand vm_SaveCity;
        private ICommand vm_CloseApplication;

        private ICommand vm_MouseDownCommand;
        private ICommand vmClickGridContainer;
        private ICommand vmClickGridSquare;
        private ICommand vmSelectZone;
        private ICommand closeWindow;
        
        //used for displaying the coordinates in labels on the UI Window
        private (int, int) _UIContainerArrayCoordinate;
        private (int, int) _UISquareArrayCoordinate;

        //initializes zone to building
        private GridSquare_Zoning vmSelectedZone = GridSquare_Zoning.Building;

        //Grid_Container[,] containers_and_squares;

        public ObservableCollection<Grid_Container> observable_ui_gridContainer { get; private set; } = new ObservableCollection<Grid_Container>();
        public ObservableCollection<Grid_Square> observable_ui_gridSquare { get; private set; } = new ObservableCollection<Grid_Square>();


        private String _BuildingStackPanel;
        private GenericBuilding _GenericBuildingTest;
        private string _TemplateLabelTest;


        NewCity_ViewModel NewCity_ViewModel;

        //initializes 
        public GridMap_ViewModel()
        {
            gridMap = new GridMap();
            //observable_ui_gridContainer = new ObservableCollection<UI_GridContainer>();
            //observable_ui_gridSquare = new ObservableCollection<UI_Grid_Square>();
            NewCityMenu = new RelayCommand(new Action<object>(NewCity));
            MouseDownCommand = new RelayCommand(new Action<object>(ShowMessage));
            ClickGridContainer = new RelayCommand(new Action<object>(SelectContainer));
            ClickGridSquare = new RelayCommand(new Action<object>(SelectSquare));
            ClickZone = new RelayCommand(new Action<object>(SelectZone));
            _TemplateLabelTest = "THIS IS A TEST";
        }

        #region Icommands
        public ICommand NewCityMenu
        {
            get
            {
                return vm_NewCity;
            }
            set
            {
                vm_NewCity = value;
            }
        }

        public ICommand MouseDownCommand
        {
            get { return vm_MouseDownCommand; }
            set
            {
                vm_MouseDownCommand = value;
            }
        }

        public ICommand ClickGridContainer
        {
            get { return vmClickGridContainer; }
            set
            {
                vmClickGridContainer = value;
            }
        }
        public ICommand ClickGridSquare
        {
            get { return vmClickGridSquare; }
            set
            {
                vmClickGridSquare = value;
            }
        }
        public ICommand ClickZone
        {
            get { return vmSelectZone; }
            set
            {
                vmSelectZone = value;
            }
        }

        public ICommand CloseWindowCommand
        {
            get { return closeWindow; }
            set
            {
                closeWindow = value;
            }
        }



        #endregion Icommands

        #region GridMapSize Definition

        /*Marked for deletion*/
        ///// <summary>
        ///// Number of Grid Containers that the city will contain.
        ///// </summary>
        //ObservableCollection<string> GridMapSizes = new ObservableCollection<string>()
        //{
        //    "1","4","9","16","25","36","49","64","81","100","121","144","169","196"
        //};
        ////public ObservableCollection<UIRectangle> RectItems { get; set; }

        //public ObservableCollection<string> GridSizes
        //{
        //    get { return GridMapSizes; }
        //    set { GridMapSizes = value;  }
        //}


        /// <summary>
        /// Handles building stack panel on the UI
        /// </summary>
        public string BuildingStackPanel
        {
            get
            {
                return _BuildingStackPanel;
            }
            set
            {
                _BuildingStackPanel = value;
                RaisePropertyChanged(nameof(BuildingStackPanel));
            }
        }

        /// <summary>
        /// Testing creation of templates for 
        /// </summary>
        public GenericBuilding GenericBuildingTest
        {
            get
            {
                return _GenericBuildingTest;
            }
            set
            {
                _GenericBuildingTest = value;
                RaisePropertyChanged(nameof(GenericBuildingTest));
            }
        }


        /// <summary>
        /// Testing the label on a template
        /// </summary>
        public string TemplateLabelTest
        {
            get
            {
                return _TemplateLabelTest;
            }
            set
            {
                _TemplateLabelTest = value;
                RaisePropertyChanged(nameof(TemplateLabelTest));
            }
        }

        //marked for deletion
        //public int SelectedGridSize
        //{
        //    get { return GridMap.number_of_Grid_Containers; }
        //    set {
        //        Console.WriteLine("GridSize Selected: " + value);
        //            if(observable_ui_gridContainer != null)
        //            {
        //                observable_ui_gridContainer.Clear();   
        //            }
        //            if(observable_ui_gridSquare != null)
        //            {
        //                observable_ui_gridSquare.Clear();
        //            }

        //            //Inlitializes the grid with the square root of the selected map size, the container array
        //            //is returned to be added to observables
        //            int grid_size = (int)Math.Sqrt(value);
        //            containers_and_squares = new UI_GridContainer[grid_size, grid_size];
        //            containers_and_squares = ui_gridmap.InitializeGrid(grid_size);
        //            InitalizeGridObservables(containers_and_squares);

        //            //initializes the first gridcontainer to be selected
        //            ui_gridmap.SelectedContainer(containers_and_squares[0, 0]);

        //        }
        //}
        #endregion GridMapSize Definition


        /// <summary>
        /// Launches NewCity Window to define coordinate and starting grid size
        /// </summary>
        /// <param name="obj"></param>
        public void NewCity(object obj)
        {

            //NewCity_ViewModel.OpenNewCityWindow();
            NewCityWindow _ncwindow = new NewCityWindow();
            _ncwindow.ShowDialog();
            SelectedGridSize();

            Console.WriteLine("Static size of grid is: " + Property_SizeOfTheGrid);
            Console.WriteLine("Static X,y,z is: " + StartCoordinate.x + "," + StartCoordinate.y + "," + StartCoordinate.z);
        }

        /// <summary>
        /// Processes grid size once collected from new window.
        /// </summary>
        public void SelectedGridSize()
        {
            if (observable_ui_gridContainer != null)
            {
                observable_ui_gridContainer.Clear();
            }
            if (observable_ui_gridSquare != null)
            {
                observable_ui_gridSquare.Clear();
            }

            
           
            //Inlitializes the grid with the square root of the selected map size, the container array
            //is returned to be added to observables
            int grid_size = (int)Math.Sqrt(Property_SizeOfTheGrid);

            gridMap.GenerateGrids(StartCoordinate, grid_size);
            InitalizeGridObservables(gridMap.PrimaryGridMap);

            //initializes the first gridcontainer to be selected
            gridMap.SelectedContainer(gridMap.PrimaryGridMap[0, 0]);
        }

        /// <summary>
        /// Example Message
        /// </summary>
        /// <param name="obj"></param>
        public void ShowMessage(object obj)
        {
            
            MessageBox.Show("Showme");
        }



        /// <summary>
        /// When a Grid Container is selected this method executes
        /// </summary>
        /// <param name="obj"></param>
        /// <remarks>Object is of type Grid_Container</remarks>
        public void SelectContainer(object obj)
        {
            if (obj is not Grid_Container)
                return;

            Grid_Container _selected = (Grid_Container)obj;
            gridMap.SelectedContainer(_selected);

            UpdateGridSquares(_selected);
            UIContainerArrayCoordinate = _selected.ContainerArrayUICoordinate;
            //Console.WriteLine("SelectingContainer");

        }

        /// <summary>
        /// Handles action when square is selected
        /// </summary>
        /// <param name="obj"></param>
        public void SelectSquare(object obj)
        {
            BuildingStackPanel = "Hidden";
            GenericBuilding aaa = new GenericBuilding();
            aaa.TemplateLabelTest = "SELECT SQUARE LABEL TEST";

            GenericBuildingTest = aaa;
            Grid_Square _selectedSquare = (Grid_Square)obj;
            UISquareArrayCoordinate = _selectedSquare.SquareArrayCoordinate;
            gridMap.SelectedSquare(_selectedSquare, vmSelectedZone);
            //_selectedSquare.FillColor = UI_Constants.GetZoningColor(vmSelectedZone);
            //Console.WriteLine("Selectingsquare: " + UISquareArrayCoordinate.Item1 + "," + UISquareArrayCoordinate.Item2);
        }

        /// <summary>
        /// Sets zone value for the clicked square
        /// </summary>
        /// <param name="obj"></param>
        public void SelectZone(object obj)
        {
            string selectedZone = (string)obj;
            vmSelectedZone = UI_Constants.StringToZoneConverter(selectedZone);
            Console.WriteLine(selectedZone);
        }



        /// <summary>
        /// Updates the Gridcontainer and square observables when there are changes
        /// </summary>
        /// <param name="grid"></param>
        public void InitalizeGridObservables(Grid_Container[,] grid)
        {
            Console.WriteLine("Initializing observables");
            Grid_Container initial = grid[0,0]; 
            for (int i = 0; i < gridMap.gridSize; i++)
            {
                for (int j = 0; j < gridMap.gridSize; j++)
                {
                    //adds each gridcontainer to observables
                    
                    observable_ui_gridContainer.Add(grid[i, j]);  
                }
            }

            for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
            {
                for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                {
                    //adds each grid square to observables (But only one container)
                    //if the for loop is not split up like this then, it will continue to add ALL the gridsquares
                    //to the observable and then it will have hundreds of squares on the canvas.
                    observable_ui_gridSquare.Add(initial.gridSquareMap[m, n]);
                    //Console.WriteLine("Coordinate is: " + initial.ui_GridSquares_array[m, n].X + ", " + initial.ui_GridSquares_array[m, n].Y);

                }
            }
            
            

        }


        void updateConditions(object sender, NotifyCollectionChangedEventArgs e)
        {
            /* Conditions = something */
            Console.WriteLine("UPDATING CONDITIONS");
        }

        /// <summary>
        /// Clears existing observable gridsquares and replaces it with the grid squares of the selected container
        /// </summary>
        /// <param name="aContainer"></param>
        public void UpdateGridSquares(Grid_Container aContainer)
        {
            observable_ui_gridSquare.Clear();
            
            for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
            {
                for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                {
                    observable_ui_gridSquare.Add(aContainer.gridSquareMap[m, n]);
                }
            }
        }


        /// <summary>
        /// Container coordinate Label within the UI Window 
        /// </summary>
        public (int,int) UIContainerArrayCoordinate
        {
            get { return _UIContainerArrayCoordinate; }
            set
            {
                _UIContainerArrayCoordinate = value;
                RaisePropertyChanged(nameof(UIContainerArrayCoordinate));
            }
        }

        /// <summary>
        /// Square coordinate label within the UI Window
        /// </summary>
        public (int, int) UISquareArrayCoordinate
        {
            get { return _UISquareArrayCoordinate; }
            set
            {
                _UISquareArrayCoordinate = value;
                RaisePropertyChanged(nameof(UISquareArrayCoordinate));
            }
        }


 

 



        //private ObservableCollection<UI_Grid_Square> _observable_ui_gridSquare = new ObservableCollection<UI_Grid_Square>();
        //public ObservableCollection<UI_Grid_Square> _observable_ui_gridSquare
        //{
        //    get { return observable_ui_gridSquare; }
        //    set
        //    {
        //        observable_ui_gridSquare = value;
        //        //RaisePropertyChanged("observable_ui_gridSquare");
        //    }

        //}

        //public ObservableCollection<UI_GridContainer> _observable_ui_gridContainer
        //{
        //    get { return observable_ui_gridContainer; }
        //    set
        //    {
        //        observable_ui_gridContainer = value;
        //        //RaisePropertyChanged("observable_ui_gridContainer");
        //    }

        //}



    }
}
