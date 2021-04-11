using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Classes;
using MC_City_Maker.Grid_Zones.Structures;
using MC_City_Maker.Grid_Zones.Infrustructure;
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
using System.Windows.Shapes;

//YOU CAN ONLY BIND PROPERTIES and NOT! Individual variables!


namespace MC_City_Maker.UI.ViewModel
{
    public class GridMap_ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private GridMap gridMap;

        /*Menu File Commands - vm prefix is View Model*/

        private ICommand vm_MouseDownCommand;
        private ICommand vmClickGridContainer;
        private ICommand vmClickGridSquare;
        private ICommand vmRightClickGridSquare;
        private ICommand vmSelectZone;
        private ICommand vmSelectTool;
        private ICommand vmPlaceTool;
        private ICommand vmDeleteTool;
        private ICommand closeWindow;

        //private ICommand mouseMove;

        //used for displaying the coordinates in labels on the UI Window
        private (int, int) _UIContainerArrayCoordinate;
        private (int, int) _UISquareArrayCoordinate;

        //initializes zone to building
        private GridSquare_Zoning vmSelectedZone = GridSquare_Zoning.Building;
        //private GridSquare_Zoning vmSelectedZone = GridSquare_Zoning.None;
        //Default Building for zone
        private GenericBuilding _BuildingTemplate;
        private Roads _RoadTemplate;

       


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
            //MouseDownCommand = new RelayCommand(new Action<object>(ShowMessage));
            //ClickGridSquare = new RelayCommand(new Action<object>(UpdateSquare));
            //MouseMoveGridSquare = new RelayCommand(new Action<object>(OnMouseMove));

            //MENU RIBBON COMMANDS
                /* Menu Commands - File*/
                NewCityMenu = new RelayCommand(new Action<object>(NewCity));
                LoadCityMenu = new RelayCommand(new Action<object>(NewCity));
                SaveCityMenu = new RelayCommand(new Action<object>(NewCity));
                CloseApplication = new RelayCommand(new Action<object>(ApplicationClose));

            /* Menu Commands - Edit*/
                NewCityMenu = new RelayCommand(new Action<object>(NewCity));
                LoadCityMenu = new RelayCommand(new Action<object>(NewCity));
                SaveCityMenu = new RelayCommand(new Action<object>(NewCity));

                /* Menu Commands - View*/
                NewCityMenu = new RelayCommand(new Action<object>(NewCity));
                LoadCityMenu = new RelayCommand(new Action<object>(NewCity));
                SaveCityMenu = new RelayCommand(new Action<object>(NewCity));

                /* Menu Commands - Tools*/
                NewCityMenu = new RelayCommand(new Action<object>(NewCity));
                LoadCityMenu = new RelayCommand(new Action<object>(NewCity));
                SaveCityMenu = new RelayCommand(new Action<object>(NewCity));

                /* Menu Commands - Help*/
                NewCityMenu = new RelayCommand(new Action<object>(NewCity));
                LoadCityMenu = new RelayCommand(new Action<object>(NewCity));
                SaveCityMenu = new RelayCommand(new Action<object>(NewCity));

            /*Placement Radio Buttons*/



            ClickGridContainer = new RelayCommand(new Action<object>(SelectContainer));
            RightClickGridSquare = new RelayCommand(new Action<object>(DeselectSquare));

            ClickToolSelect = new RelayCommand(new Action<object>(SelectTool));
            ClickToolPlace = new RelayCommand(new Action<object>(PlaceTool));
            ClickToolDelete = new RelayCommand(new Action<object>(DeleteTool));
            ClickZoneBuilding = new RelayCommand(new Action<object>(ZoneBuildingSelected));
            ClickZoneInfrustructure = new RelayCommand(new Action<object>(ZoneInfrustructureSelected));
            ClickZoneScenery = new RelayCommand(new Action<object>(ZoneScenerySelected));
            ClickZoneWater = new RelayCommand(new Action<object>(ZoneWaterSelected));

            //_TemplateLabelTest = "THIS IS A TEST";
        }

        #region Icommands

        private ICommand vm_NewCity;
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


        private ICommand vm_LoadCityMenu;
        public ICommand LoadCityMenu
        {
            get
            {
                return vm_LoadCityMenu;
            }
            set
            {
                vm_LoadCityMenu = value;
            }
        }

        private ICommand vm_SaveCityMenu;
        public ICommand SaveCityMenu
        {
            get
            {
                return vm_SaveCityMenu;
            }
            set
            {
                vm_SaveCityMenu = value;
            }
        }

        private ICommand vm_CloseApplication;
        public ICommand CloseApplication
        {
            get
            {
                return vm_CloseApplication;
            }
            set
            {
                vm_CloseApplication = value;
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

        private bool _ZoneBuilding;
        public bool ZoneBuilding
        {
            get
            {
                return _ZoneBuilding;
            }
            set
            {
                _ZoneBuilding = value;
                RaisePropertyChanged(nameof(ZoneBuilding));
            }
        }

        private ICommand vmZoneBuilding;
        public ICommand ClickZoneBuilding
        {
            get { return vmZoneBuilding; }
            set
            {
                vmZoneBuilding = value;
            }
        }

        /// <summary>
        /// Controls whether or not grid square can be placed
        /// </summary>
        private bool _AllowGridSquarePlacement;
        public bool AllowGridSquarePlacement
        {
            get
            {
                return _AllowGridSquarePlacement;
            }
            set
            {
                _AllowGridSquarePlacement = value;
                RaisePropertyChanged(nameof(AllowGridSquarePlacement));
            }
        }

        private bool _ZoneInfrustructure;
        public bool ZoneInfrustructure
        {
            get
            {
                return _ZoneInfrustructure;
            }
            set
            {
                _ZoneInfrustructure = value;
                RaisePropertyChanged(nameof(ZoneInfrustructure));
            }
        }

        private ICommand vmZoneInfrustructure;
        public ICommand ClickZoneInfrustructure
        {
            get { return vmZoneInfrustructure; }
            set
            {
                vmZoneInfrustructure = value;
            }
        }

        private bool _ZoneScenery;
        public bool ZoneScenery
        {
            get
            {
                return _ZoneScenery;
            }
            set
            {
                _ZoneScenery = value;
                RaisePropertyChanged(nameof(ZoneScenery));
            }
        }

        private ICommand vmZoneScenery;
        public ICommand ClickZoneScenery
        {
            get { return vmZoneScenery; }
            set
            {
                vmZoneScenery = value;
            }
        }

        private bool _ZoneWater;
        public bool ZoneWater
        {
            get
            {
                return _ZoneWater;
            }
            set
            {
                _ZoneWater = value;
                RaisePropertyChanged(nameof(ZoneWater));
            }
        }

        private ICommand vmZoneWater;
        public ICommand ClickZoneWater
        {
            get { return vmZoneWater; }
            set
            {
                vmZoneWater = value;
            }
        }


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




        /// <summary>
        /// Creates Template for Generic Building
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
        /// Creates Template for Roads
        /// </summary>
        public Roads RoadTemplate
        {
            get
            {
                return _RoadTemplate;
            }
            set
            {
                _RoadTemplate = value;
                RaisePropertyChanged(nameof(RoadTemplate));
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

            //This is the default creation.
            ZoneBuilding = true;
            BuildingTemplate = new GenericBuilding();
            //BuildingTemplate = Template;
           

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


        }



        /// <summary>
        /// Places square
        /// </summary>
        /// <param name="_selectedSquare"></param>
        private void PlaceSquare(Grid_Square _selectedSquare)
        {
            //Debug.WriteLine("Square selected");
            //Debug.WriteLine("WIDTH OF BUILDING IS:  " + BuildingTemplate.width);

            //Assign a building that copies the default building values over.
            //_selectedSquare.Building = DefaultBuilding;

            Debug.WriteLine("PlaceSquare executed");
            UISquareArrayCoordinate = _selectedSquare.SquareArrayCoordinate;
            //gridMap.PlaceSquare(_selectedSquare, vmSelectedZone);

            switch (vmSelectedZone)
            {
                case GridSquare_Zoning.Building:

                    gridMap.PlaceSquare(_selectedSquare, vmSelectedZone, BuildingTemplate.length, BuildingTemplate.width);
                    break;
                case GridSquare_Zoning.Infrustructure:

                    gridMap.PlaceSquare(_selectedSquare, vmSelectedZone);
                    break;
                case GridSquare_Zoning.Scenery:
  
                    gridMap.PlaceSquare(_selectedSquare, vmSelectedZone);
                    break;
                case GridSquare_Zoning.Water:

                    gridMap.PlaceSquare(_selectedSquare, vmSelectedZone);
                    break;

            }
            gridMap.PlaceSquare(_selectedSquare, vmSelectedZone);
            //observable_ui_gridSquare.Add(_selectedSquare);
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

        public void ZoneBuildingSelected(object obj)
        {
            SetZoneToggleButton("Building");
            Debug.WriteLine("Building CLICKED");
            bool test = (bool)obj;

        }

        public void ZoneInfrustructureSelected(object obj)
        {
            SetZoneToggleButton("Infrustructure");
            Debug.WriteLine("Infrustructure CLICKED");
            bool test = (bool)obj;

        }

        public void ZoneScenerySelected(object obj)
        {
            SetZoneToggleButton("Scenery");
            Debug.WriteLine("Scenery CLICKED");
            bool test = (bool)obj;

        }

        public void ZoneWaterSelected(object obj)
        {
            SetZoneToggleButton("Water");
            Debug.WriteLine("select CLICKED");
            bool test = (bool)obj;

        }

        public void SetZoneToggleButton(string togglebutton)
        {
            
            switch (togglebutton)
            {
                case "Building":
                    ZoneInfrustructure = false;
                    ZoneWater = false;
                    ZoneScenery = false;
                    vmSelectedZone = GridSquare_Zoning.Building;
                    SetZoneClass(togglebutton);
                    break;
                case "Infrustructure":
                    ZoneBuilding = false;
                    ZoneWater = false;
                    ZoneScenery = false;
                    vmSelectedZone = GridSquare_Zoning.Infrustructure;
                    SetZoneClass(togglebutton);
                    break;
                case "Scenery":
                    ZoneBuilding = false;
                    ZoneInfrustructure = false;
                    ZoneWater = false;
                    vmSelectedZone = GridSquare_Zoning.Scenery;
                    SetZoneClass(togglebutton);
                    break;
                case "Water":
                    ZoneBuilding = false;
                    ZoneInfrustructure = false;
                    ZoneScenery = false;
                    vmSelectedZone = GridSquare_Zoning.Water;
                    SetZoneClass(togglebutton);
                    break;
                default:
                    ZoneInfrustructure = false;
                    ZoneWater = false;
                    ZoneScenery = false;
                    vmSelectedZone = GridSquare_Zoning.Building;
                    SetZoneClass(togglebutton);
                    break;

            }
        }



        private void SetZoneClass(string zone)
        {
            
            switch (zone)
            {
                case "Building":
                    BuildingTemplate = new GenericBuilding();
                    RoadTemplate = null;
                    //scenery = null
                    //water = null;
                    break;
                case "Infrustructure":
                    RoadTemplate = new Roads();
                    BuildingTemplate = null;
                    //scenery = null
                    //water = null;
                    break;
                case "Scenery":
                    //TODO add scenery template handling
                    break;
                case "Water":
                    //TODO Add water template handling
                    break;
                default:
                    BuildingTemplate = new GenericBuilding();
                    RoadTemplate = null;
                    //scenery = null
                    //water = null;
                    break;

            }

        }


        /**
         * Mouse Actions
         * 
         * This section handles the mouse down/move/up etc actions on the gird
         */


        private RelayCommand _mouseDownCommand;
        public RelayCommand MouseDownCommand
        {
            get
            {
                if (_mouseDownCommand == null) _mouseDownCommand = new RelayCommand(param => MouseDown((MouseEventArgs)param));
                return _mouseDownCommand;
            }
            set { _mouseDownCommand = value; }
        }

        private RelayCommand _mouseUpCommand;
        public RelayCommand MouseUpCommand
        {
            get
            {
                if (_mouseUpCommand == null) _mouseUpCommand = new RelayCommand(param => MouseUp((MouseEventArgs)param));
                return _mouseUpCommand;
            }
            set { _mouseUpCommand = value; }
        }

        private RelayCommand _mouseMoveCommand;
        public RelayCommand MouseMoveCommand
        {
            get
            {
                if (_mouseMoveCommand == null) _mouseMoveCommand = new RelayCommand(param => MouseMove((MouseEventArgs)param));
                return _mouseMoveCommand;
            }
            set { _mouseMoveCommand = value; }
        }

        private RelayCommand _mouseLeaveCommand;
        public RelayCommand MouseLeaveCommand
        {
            get
            {
                if (_mouseLeaveCommand == null) _mouseLeaveCommand = new RelayCommand(param => MouseLeave((MouseEventArgs)param));
                return _mouseLeaveCommand;
            }
            set { _mouseLeaveCommand = value; }
        }


        SelectionBox aSelectionBox;

        public ObservableCollection<SelectionBox> observableSelectionBox { get; private set; } = new ObservableCollection<SelectionBox>();

        bool mouseDown = false; // Set to 'true' when mouse is held down.
        Point mouseDownPos; // The point where the mouse button was clicked down.



        private void MouseDown(MouseEventArgs e)
        {

            Debug.WriteLine("Mouse down");

            mouseDownPos = e.GetPosition((IInputElement)e.Source);

            mouseDown = true;
            if(ToolSelect)
            {

            }

            if(ToolPlace)
            {

                    Debug.WriteLine("DRagging");
                    StartSelectionBox(e);

            }

            if(ToolDelete)
            {

                    StartSelectionBox(e);
   
            }


            //MINIMUM THRESHOLD FOR ENABLING DRAGGING EVENT
            //private const double _dragThreshold = 1.0;
            //private bool _dragging;
            //private Point startpos;
            //var currentpos = e.GetPosition(this);
            //var delta = currentpos - startpos;
            //if ((delta.Length > _dragThreshold || _dragging) && e.LeftButton == MouseButtonState.Pressed)
            //{
            //    _dragging = true;
            //    Left += currentpos.X - startpos.X;
            //    Top += currentpos.Y - startpos.Y;
            //}



            //Debug.WriteLine("MOUSE DOWN EVENT");
            //Debug.WriteLine(e.GetPosition((IInputElement)e.Source).ToString());

            //SolidColorBrush redBrush = new SolidColorBrush();
            //redBrush.Color = Colors.Red;
            //mouseDown = true;
            //mouseDownPos = e.GetPosition((IInputElement)e.Source);
            //Debug.WriteLine(mouseDownPos.X);
            //Debug.WriteLine(mouseDownPos.Y);
            //aSelectionBox = new SelectionBox(mouseDownPos.X, mouseDownPos.Y,0,0, redBrush);
            //aSelectionBox.selectionBoxVisibility = Visibility.Visible;

            //observableSelectionBox.Add(aSelectionBox);

        }

        private void StartSelectionBox(MouseEventArgs e)
        {
            
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            mouseDown = true;
            mouseDownPos = e.GetPosition((IInputElement)e.Source);
            Debug.WriteLine(mouseDownPos.X);
            Debug.WriteLine(mouseDownPos.Y);
            aSelectionBox = new SelectionBox(mouseDownPos.X, mouseDownPos.Y, 0, 0, redBrush);
            aSelectionBox.selectionBoxVisibility = Visibility.Visible;

            observableSelectionBox.Add(aSelectionBox);
        }

        private bool DetermineMouseDownHoldThreshold(Point mouseDown, Point mouseUp)
        {
            Debug.WriteLine("Drag Test");
            const double _dragThreshold = 1.0;
            //bool _dragging;
            //Point startpos;
            Vector delta;

            delta = mouseUp - mouseDown;

            Debug.WriteLine("delta = " + delta);

            if (delta.Length > _dragThreshold)
            {
                return true;
            }

            return false;
        }

        private void MouseMove(MouseEventArgs e)
        {
            //MessageBox.Show(e.GetPosition((IInputElement)e.Source).ToString());
            //Debug.WriteLine(e.GetPosition((IInputElement)e.Source).ToString());

            /*For Selection box*/
            if (mouseDown && (ToolPlace || ToolDelete))
            {
                Debug.WriteLine("Mouse down and moving");
                // When the mouse is held down, reposition the drag selection box.

                Point mousePos = e.GetPosition((IInputElement)e.Source);

                if (mouseDownPos.X < mousePos.X)
                {

                    aSelectionBox.selectionBoxX = mouseDownPos.X;
                    //Canvas.SetLeft(selectionBoxX, mouseDownPos.X);
                    aSelectionBox.selectionBoxWidth = mousePos.X - mouseDownPos.X;
                }
                else
                {

                    aSelectionBox.selectionBoxX = mousePos.X;
                    //Canvas.SetLeft(selectionBoxX, mousePos.X);
                    aSelectionBox.selectionBoxWidth = mouseDownPos.X - mousePos.X;
                }

                if (mouseDownPos.Y < mousePos.Y)
                {
                    aSelectionBox.selectionBoxY = mouseDownPos.Y;
                    //Canvas.SetTop(selectionBox, mouseDownPos.Y);
                    aSelectionBox.selectionBoxHeight = mousePos.Y - mouseDownPos.Y;
                }
                else
                {
                    aSelectionBox.selectionBoxY = mousePos.Y;
                    //Canvas.SetTop(selectionBox, mousePos.Y);
                    aSelectionBox.selectionBoxHeight = mouseDownPos.Y - mousePos.Y;
                }
            }

            /*For placement hover*/
            if(mouseDown == false && ToolPlace)
            {
                Point mousePos = e.GetPosition((IInputElement)e.Source);
              
                MouseInSquare(mousePos);
            }

        }


        private void MouseUp(MouseEventArgs e)
        {
            //MessageBox.Show(e.GetPosition((IInputElement)e.Source).ToString());

            Debug.WriteLine("Mouse Up");

            mouseDown = false;
            Point mouseUpPos = e.GetPosition((IInputElement)e.Source);


            if (DetermineMouseDownHoldThreshold(mouseDownPos, mouseUpPos))
            {
                Debug.WriteLine("Dragged");

                if(ToolPlace ||ToolDelete)
                {
                    SelectionBoxPlaceOrDelete();
                    aSelectionBox.selectionBoxVisibility = Visibility.Collapsed;
                }
                 

                
            }
            else
            {

                Debug.WriteLine("NOT dragged");

                foreach (Grid_Square sq in observable_ui_gridSquare.ToList())
                {
                    if (sq != null)
                    {
                        //Debug.WriteLine("NOT NULL");

                        //Rectangle rectangle = sq.gridSquareRectangle;

                        Rect rect = new Rect(sq.X, sq.Y, sq.Width, sq.Height);

                        if (rect.Contains(mouseDownPos))
                        {
                            if (ToolPlace && AllowGridSquarePlacement)
                            {
                                PlaceSquare(sq);
                            } else if (ToolDelete)
                            {
                                DeleteSquare(sq);
                            }
                                

                            Debug.WriteLine("Clicked square " + sq.SquareArrayCoordinate.Item1 + "," + sq.SquareArrayCoordinate.Item2);
                        }


                    }
                }
            }
        }

        private void MouseLeave(MouseEventArgs e)
        {
            //Point mousePos = e.GetPosition((IInputElement)e.Source);
            Debug.WriteLine("Mouse left location");
        }


        /// <summary>
        /// Identifies the selection box squares and places or deletes them
        /// </summary>
        public void SelectionBoxPlaceOrDelete()
        {
            foreach (Grid_Square sq in observable_ui_gridSquare.ToList())
            {
                if (sq != null)
                {
                    //Debug.WriteLine("NOT NULL");

                    //Rectangle rectangle = sq.gridSquareRectangle;

                    Rect rect = new Rect(sq.X, sq.Y, sq.Width, sq.Height);
                    Rect rect2 = new Rect(aSelectionBox.selectionBoxX, aSelectionBox.selectionBoxY, aSelectionBox.selectionBoxWidth, aSelectionBox.selectionBoxHeight);

                    if (rect.IntersectsWith(rect2))
                    {
                        //Debug.WriteLine("INTERSECTION " + sq.SquareArrayCoordinate.Item1 + "," + sq.SquareArrayCoordinate.Item2);

                        if (ToolPlace && AllowGridSquarePlacement)
                        {
                            PlaceSquare(sq);
                        }
                        else if (ToolDelete)
                        {
                            Debug.WriteLine("Delete Square");
                            DeleteSquare(sq);
                        }

                    }

                }
            }
        }



        private void MouseInSquare(Point mouseLocation)
        {
            List<Grid_Square> HoverSquares = new List<Grid_Square>();

            foreach (Grid_Square sq in observable_ui_gridSquare.ToList())
            {
                if (sq != null)
                {
                    //Debug.WriteLine("NOT NULL");

                    //Rectangle rectangle = sq.gridSquareRectangle;

                    //converts gridsquare rectangle to object that can determain contains point
                    Rect rect = new Rect(sq.X, sq.Y, sq.Width, sq.Height);

                    if (rect.Contains(mouseLocation))
                    {
                        //outputs coordinate (y,x) int format
                        Debug.WriteLine("Hover square " + sq.SquareArrayCoordinate.Item1 + "," + sq.SquareArrayCoordinate.Item2);


                        //TODO: Need to add the capability for ZoneInfrustructure, Scenery, and Water
                        if (ZoneBuilding)
                        {
                            //get the generic building object length and width
                            int buildingWidth = BuildingTemplate.width;
                            int buildingLength = BuildingTemplate.length;

                            (int, int) parentContainer = sq.ParentContainerArrayCoordinate;
                            (int, int) startingSquare = sq.SquareArrayCoordinate;

                            

                            try
                            {

                                //Loop gets the squares that will be hovering based on users selection of placement size
                                for (int i = 0; i < buildingLength; i++)
                                {
                                    //get squares from y
                                    //Grid_Square _tempSquare1 = gridMap.Get_SquareFromContainer(parentContainer, (startingSquare.Item1 + i, startingSquare.Item2));

                                    //if(!HoverSquares.Contains(_tempSquare1))
                                    //{
                                    //   _tempSquare1.HoverStatus = true;   //TODO NEED TO TRACK HOVER STATUS TO COMPARE
                                    //   HoverSquares.Add(_tempSquare1);
                                    //}

                                    
                                    
                                    for (int j = 0; j < buildingWidth; j++)
                                    {
                                        //get squares from x
                                        Grid_Square _tempSquare2 = gridMap.Get_SquareFromContainer(parentContainer, (startingSquare.Item1 + i, startingSquare.Item2 + j));
                                        if (!HoverSquares.Contains(_tempSquare2))
                                        {
                                            _tempSquare2.HoverStatus = true;
                                            HoverSquares.Add(_tempSquare2);

                                        }

                                    }

                                }
                            } catch (IndexOutOfRangeException e)
                            {
                                Debug.WriteLine("index error");


                            }

                    }


                    }
                }
            }




                foreach(Grid_Square obsSq in observable_ui_gridSquare.ToList())
                {
                    //If a gridsquare is not in hoversquares, but hover status is still true
                        //this means the user has moved the cursor and it is not valid anymore
                     //Remove the status and remove the square from the hoverlist
                     //then set the properties to return the square to the proper color
                    if (!HoverSquares.Contains(obsSq) && obsSq.HoverStatus == true)
                    {
                        HoverSquares.Remove(obsSq);
                        obsSq.HoverStatus = false;

                        //Debug.WriteLine("observable filling zone");

                        //TODO - square zone lookup method.
                        //This is overwriting the squares
                        
                        if(obsSq.IsEntityStartSquare || obsSq.IsEntityEndSquare)
                        {
                            obsSq.FillColor = UI_Constants.Start_End_GridSquare_Color;
                        }else
                        {
                            //Fill squares with their zone type
                            obsSq.FillColor = UI_Constants.GetZoningColor(obsSq.Zone);
                        }

                    }else
                    {
                        //do nothing
                    }
                }

            foreach (Grid_Square hovSq in HoverSquares)
            {
                foreach (Grid_Square obsSq in observable_ui_gridSquare.ToList())
                {

                    if(GridSquare_IntersectionTest(hovSq, obsSq) && obsSq.Selected)
                    {
                        foreach (Grid_Square hoverSquares in HoverSquares)
                        {
                            hoverSquares.FillColor = UI_Constants.Cannot_Place_Square;
                            
                        }

                        //Prevents grid square placement
                        AllowGridSquarePlacement = false;
                        return;
                            
                    }
                    else
                    {
                        //Allows gridsquare placement
                        AllowGridSquarePlacement = true;
                        hovSq.FillColor = UI_Constants.Can_Place_Square;
                    }
                }
                   

                //Debug.WriteLine("hover filling zone");

                //TODO NEW METHOD GRID SQUARE INTERSECT TEST WITH OBSERVABLE SQUARES.
                //IF NO INTERSECTION, Do hover color, if intersection RED COVER COLOR
                //TODO, NEED TO CREATE Bool - CAN PLACE, to prevent placement if it is not acceptable.
               

            }



        }

        /// <summary>
        /// Tests if two Grid Squares intersect
        /// </summary>
        /// <param name="square1"></param>
        /// <param name="square2"></param>
        /// <returns></returns>
        private bool GridSquare_IntersectionTest(Grid_Square square1, Grid_Square square2)
        {
            Rect rect1 = new Rect(square1.X, square1.Y, square1.Width, square1.Height);
            Rect rect2 = new Rect(square2.X, square2.Y, square2.Width, square2.Height);

            return rect1.IntersectsWith(rect2);

            
        }



        /**
         * Grid operations
         * 
         * This section handles operations within the grid and the binded elements wired up
         */




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


        public void ApplicationClose(object obj)
        {
            Application curApp = Application.Current;
            curApp.Shutdown();
        }





    }
}
