using MC_City_Maker.Constants;
using MC_City_Maker.Structures;
using MC_City_Maker.UI.Model;
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
        private UI_GridMap ui_gridmap;

        /*Menu File Commands*/
        private ICommand vm_NewCity;
        private ICommand vm_OpenCity;
        private ICommand vm_SaveCity;
        private ICommand vm_CloseApplication;

        private ICommand vm_MouseDownCommand;
        private ICommand vmClickGridContainer;
        private ICommand vmClickGridSquare;
        private ICommand vmSelectZone;
        private ICommand closeWindow;
        

        NewCity_ViewModel vmNewCity { get; set; }

        
        private (int, int) _UIContainerArrayCoordinate;
        private (int, int) _UISquareArrayCoordinate;

        private GridSquare_Zoning vmSelectedZone = GridSquare_Zoning.Building;

        UI_GridContainer[,] containers_and_squares;

        public ObservableCollection<UI_GridContainer> observable_ui_gridContainer { get; private set; } = new ObservableCollection<UI_GridContainer>();
        public ObservableCollection<UI_Grid_Square> observable_ui_gridSquare { get; private set; } = new ObservableCollection<UI_Grid_Square>();


        private String _BuildingStackPanel;
        private GenericBuilding _GenericBuildingTest;
        private string _TemplateLabelTest;


        NewCity_ViewModel NewCity_ViewModel;
        //initializes 
        public GridMap_ViewModel()
        {
            ui_gridmap = new UI_GridMap();
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

        /// <summary>
        /// Number of Grid Containers that the city will contain.
        /// </summary>
        ObservableCollection<string> GridMapSizes = new ObservableCollection<string>()
        {
            "1","4","9","16","25","36","49","64","81","100","121","144","169","196"
        };
        //public ObservableCollection<UIRectangle> RectItems { get; set; }

        public ObservableCollection<string> GridSizes
        {
            get { return GridMapSizes; }
            set { GridMapSizes = value;  }
        }

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
            containers_and_squares = new UI_GridContainer[grid_size, grid_size];
            containers_and_squares = ui_gridmap.InitializeGrid(grid_size);
            InitalizeGridObservables(containers_and_squares);

            //initializes the first gridcontainer to be selected
            ui_gridmap.SelectedContainer(containers_and_squares[0, 0]);
        }

        public void ShowMessage(object obj)
        {
            
            MessageBox.Show("Showme");
        }

        public void NewCity(object obj)
        {

            //NewCity_ViewModel.OpenNewCityWindow();
            NewCityWindow _ncwindow = new NewCityWindow();
            _ncwindow.ShowDialog();
            SelectedGridSize();
            
            Console.WriteLine("Static size of grid is: " + Property_SizeOfTheGrid);
            Console.WriteLine("Static X,y,z is: " + StartCoordinate.x + "," + StartCoordinate.y + "," + StartCoordinate.z);
        }

 
        public void SelectContainer(object obj)
        {

            UI_GridContainer _selected = (UI_GridContainer)obj;
            ui_gridmap.SelectedContainer(_selected);
            UpdateGridSquares(_selected);
            UIContainerArrayCoordinate = _selected.ContainerArrayCoordinate;
            //Console.WriteLine("SelectingContainer");

        }

        public void SelectSquare(object obj)
        {
            BuildingStackPanel = "Hidden";
            GenericBuilding aaa = new GenericBuilding();
            aaa.TemplateLabelTest = "SELECT SQUARE LABEL TEST";

            GenericBuildingTest = aaa;
            UI_Grid_Square _selectedSquare = (UI_Grid_Square)obj;
            UISquareArrayCoordinate = _selectedSquare.SquareArrayCoordinate;
            ui_gridmap.SelectedSquare(_selectedSquare, vmSelectedZone);
            //_selectedSquare.FillColor = UI_Constants.GetZoningColor(vmSelectedZone);
            //Console.WriteLine("Selectingsquare: " + UISquareArrayCoordinate.Item1 + "," + UISquareArrayCoordinate.Item2);
        }

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
        public void InitalizeGridObservables(UI_GridContainer[,] grid)
        {
            Console.WriteLine("Initializing observables");
            UI_GridContainer initial = grid[0,0]; 
            for (int i = 0; i < ui_gridmap.gridSize; i++)
            {
                for (int j = 0; j < ui_gridmap.gridSize; j++)
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
                    observable_ui_gridSquare.Add(initial.ui_GridSquares_array[m, n]);
                    //Console.WriteLine("Coordinate is: " + initial.ui_GridSquares_array[m, n].X + ", " + initial.ui_GridSquares_array[m, n].Y);

                }
            }
            
            

        }
        void updateConditions(object sender, NotifyCollectionChangedEventArgs e)
        {
            /* Conditions = something */
            Console.WriteLine("UPDATING CONDITIONS");
        }

        public void UpdateGridSquares(UI_GridContainer aContainer)
        {
            observable_ui_gridSquare.Clear();
            
            for (int m = 0; m < Shared_Constants.GRID_SQUARE_SIZE; m++)
            {
                for (int n = 0; n < Shared_Constants.GRID_SQUARE_SIZE; n++)
                {
                    observable_ui_gridSquare.Add(aContainer.ui_GridSquares_array[m, n]);
                }
            }
        }

        public (int,int) UIContainerArrayCoordinate
        {
            get { return _UIContainerArrayCoordinate; }
            set
            {
                _UIContainerArrayCoordinate = value;
                RaisePropertyChanged(nameof(UIContainerArrayCoordinate));
            }
        }
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
