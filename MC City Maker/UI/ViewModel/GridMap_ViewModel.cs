using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Classes;
using MC_City_Maker.Structures;
using MC_City_Maker.UI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        private ICommand vmRightClickGridSquare;
        private ICommand vmSelectZone;
        private ICommand vmSelectTool;
        private ICommand vmPlaceTool;
        private ICommand vmDeleteTool;
        private ICommand closeWindow;
        
        //used for displaying the coordinates in labels on the UI Window
        private (int, int) _UIContainerArrayCoordinate;
        private (int, int) _UISquareArrayCoordinate;

        //initializes zone to building
        private GridSquare_Zoning vmSelectedZone = GridSquare_Zoning.Building;
        //Default Building for zone
        private GenericBuilding _BuildingTemplate;


       


        //Grid_Container[,] containers_and_squares;

        public ObservableCollection<Grid_Container> observable_ui_gridContainer { get; private set; } = new ObservableCollection<Grid_Container>();
        public ObservableCollection<Grid_Square> observable_ui_gridSquare { get; private set; } = new ObservableCollection<Grid_Square>();


        private string _BuildingStackPanel;
        private bool _ToolSelect;
        private bool _ToolPlace;
        private bool _ToolDelete;
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
            ClickGridSquare = new RelayCommand(new Action<object>(UpdateSquare));
            RightClickGridSquare = new RelayCommand(new Action<object>(DeselectSquare));
            ClickZone = new RelayCommand(new Action<object>(SelectZone));
            ClickToolSelect = new RelayCommand(new Action<object>(SelectTool));
            ClickToolPlace = new RelayCommand(new Action<object>(PlaceTool));
            ClickToolDelete = new RelayCommand(new Action<object>(DeleteTool));
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

        public ICommand RightClickGridSquare
        {
            get { return vmRightClickGridSquare; }
            set
            {
                vmRightClickGridSquare = value;
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

        public ICommand ClickToolSelect
        {
            get { return vmSelectTool; }
            set
            {
                vmSelectTool = value;
            }
        }

        public ICommand ClickToolPlace
        {
            get { return vmPlaceTool; }
            set
            {
                vmPlaceTool = value;
            }
        }

        public ICommand ClickToolDelete
        {
            get { return vmDeleteTool; }
            set
            {
                vmDeleteTool = value;
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


        public bool ToolSelect
        {
            get
            {
                return _ToolSelect;
            }
            set
            {
                _ToolSelect = value;
                RaisePropertyChanged(nameof(ToolSelect));
            }
        }

        public bool ToolPlace
        {
            get
            {
                return _ToolPlace;
            }
            set
            {
                _ToolPlace = value;
                RaisePropertyChanged(nameof(ToolPlace));
            }
        }

        public bool ToolDelete
        {
            get
            {
                return _ToolDelete;
            }
            set
            {
                _ToolDelete = value;
                RaisePropertyChanged(nameof(ToolDelete));
            }
        }


        //Marked for deletion
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
        public GenericBuilding BuildingTemplate
        {
            get
            {
                return _BuildingTemplate;
            }
            set
            {
                _BuildingTemplate = value;
                RaisePropertyChanged(nameof(BuildingTemplate));
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

            //Creates the genericbuilding and assigns genericbuildingdefault to display template
            GenericBuilding Template = new GenericBuilding();
            BuildingTemplate = Template;
           

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
        public void UpdateSquare(object obj)
        {
            Grid_Square _selectedSquare = (Grid_Square)obj;

            if (ToolSelect)
            {
                SelectSquare(_selectedSquare);
            }

            if(ToolPlace)
            {
                PlaceSquare(_selectedSquare);

            }

            if(ToolDelete)
            {
                UISquareArrayCoordinate = _selectedSquare.SquareArrayCoordinate;
                gridMap.DeleteSquare(_selectedSquare);
            }
        }

        private void SelectSquare(Grid_Square _selectedSquare)
        {
            UISquareArrayCoordinate = _selectedSquare.SquareArrayCoordinate;
            gridMap.SelectSquare(_selectedSquare, GridSquare_Zoning.Selected);
        }

        private void PlaceSquare(Grid_Square _selectedSquare)
        {
            Debug.WriteLine("Square selected");
            Debug.WriteLine("WIDTH OF BUILDING IS:  " + BuildingTemplate.width);

            //Assign a building that copies the default building values over.
            //_selectedSquare.Building = DefaultBuilding;

            UISquareArrayCoordinate = _selectedSquare.SquareArrayCoordinate;
            gridMap.PlaceSquare(_selectedSquare, vmSelectedZone);
        }

        private void DeleteSquare(Grid_Square _selectedSquare)
        {
            UISquareArrayCoordinate = _selectedSquare.SquareArrayCoordinate;
            gridMap.DeleteSquare(_selectedSquare);
        }

        /// <summary>
        /// Unselects a square and updates square data.
        /// </summary>
        /// <param name="obj"></param>
        public void DeselectSquare(object obj)
        {
            Grid_Square _selectedSquare = (Grid_Square)obj;
            UISquareArrayCoordinate = _selectedSquare.SquareArrayCoordinate;
            gridMap.DeleteSquare(_selectedSquare);
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

        public void SelectTool(object obj)
        {
            SetToolToggleButton("select");
            Debug.WriteLine("select CLICKED");
            bool test = (bool)obj;
            Debug.WriteLine(test);
            
        }

        public void PlaceTool(object obj)
        {
            SetToolToggleButton("place");
            Debug.WriteLine("place CLICKED");
            bool test = (bool)obj;
            Debug.WriteLine(test);
            
        }

        public void DeleteTool(object obj)
        {
            SetToolToggleButton("delete");
            Debug.WriteLine("delete CLICKED");
            bool test = (bool)obj;
            Debug.WriteLine(test);
           
        }

        public void SetToolToggleButton(string togglebutton)
        {
            switch(togglebutton)
            {
                case "select":
                    //ToolSelect = true;
                    ToolPlace = false;
                    ToolDelete = false;
                    break;
                case "place":
                    ToolSelect = false;
                    //ToolPlace = true;
                    ToolDelete = false;
                    break;
                case "delete":
                    ToolSelect = false;
                    ToolPlace = false;
                    //ToolDelete = true;
                    break;

            }
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
