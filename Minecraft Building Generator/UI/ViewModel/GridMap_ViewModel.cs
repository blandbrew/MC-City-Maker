using Minecraft_Building_Generator.UI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Minecraft_Building_Generator.UI.ViewModel
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

        //initializes 
        public GridMap_ViewModel()
        {
            ui_gridmap = new UI_GridMap();
            NewCity = new RelayCommand(new Action<object>(ShowMessage));
            MouseDownCommand = new RelayCommand(new Action<object>(ShowMessage));
            ClickGridContainer = new RelayCommand(new Action<object>(SelectContainer));
            ClickGridSquare = new RelayCommand(new Action<object>(SelectSquare));
        }

        #region Icommands
        public ICommand NewCity
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
        public int SelectedGridSize
        {
            get { return GridMap.number_of_Grid_Containers; }
            set {
                    if(ui_gridContainer != null)
                    {
                        ui_gridContainer.Clear();
                        ui_gridSquare.Clear();
                    }
                        GridMap.number_of_Grid_Containers = (int)Math.Sqrt(value);
                        SetUIGridContainer(GridMap.number_of_Grid_Containers);
                        SetUIGridSquare();
                }
        }
        #endregion GridMapSize Definition





        public ObservableCollection<UI_GridContainer> ui_gridContainer { get; private set; } = new ObservableCollection<UI_GridContainer>();
        public ObservableCollection<UI_Grid_Square> ui_gridSquare { get; private set; } = new ObservableCollection<UI_Grid_Square>();
        
        //public void SetUIGridContainer(int gridSize)
        //{   
        //    int separatorValue = 17;
        //    int x = 20;
        //    int y = 20;
 
        //    //initialize 2d array of grid planner
        //    for (int i = 0; i < gridSize; i++)
        //    {
        //        for (int j = 0; j < gridSize; j++)
        //        {


        //            ui_gridContainer.Add(new UI_GridContainer(x, y, Colors.White, Colors.White));

        //            x += separatorValue;
        //        }
        //        x = 20;
        //        y += separatorValue;
        //    }
        //}
        public void SetUIGridSquare()
        {
            int separatorValue = 23;
            int x = 10;
            int y = 10;

            //initialize 2d array of grid planner
            for (int i = 0; i < Shared_Constants.GRID_SQUARE_SIZE; i++)
            {
                for (int j = 0; j < Shared_Constants.GRID_SQUARE_SIZE; j++)
                {
                    ui_gridSquare.Add(new UI_GridSquare
                    {
                        X = x,
                        Y = y,
                        Width = Shared_Constants.UI_GRID_RECTANGLE_SIZE+7,
                        Height = Shared_Constants.UI_GRID_RECTANGLE_SIZE+7,
                        Color = Colors.White,
                        FillColor = Colors.White
                    });
                
                        
                       
                    x += separatorValue;
                }
                x = 10;
                y += separatorValue;
            }
        }


        


        public void ShowMessage(object obj)
        {
            Console.WriteLine(obj);
            _UI_GridSquares temp = (_UI_GridSquares)obj;
            Console.WriteLine(temp.X);
            MessageBox.Show("Showme");
        }

        public void SelectGridSquare(object obj)
        {
            _UI_GridSquares temp = (_UI_GridSquares)obj;
            Console.WriteLine(temp.X);
          
        }
 
        public void SelectContainer(object obj)
        {

            _UI_GridContainer temp = (_UI_GridContainer)obj;
            Console.WriteLine(temp.X);

        }



        public void SelectSquare(object obj)
        {

            _UI_GridSquares temp = (_UI_GridSquares)obj;
            Console.WriteLine(temp.X);

        }






        //public string SetNumberOfContainers()
        //{
        //    get => ui_gridmap.gridSize + "";

        //    set
        //    {
        //        if (ui_gridmap.gridSize != value)
        //        {
        //            string _temp = ui_gridmap.gridSize + "";
        //            _temp = value;
        //            OnPropertyChange("GridSize");

        //        }
        //    }
        //}


        //public string GridSize
        //{
        //    get => gridmap.gridSize + "";
        //    set
        //    {
        //        if (gridmap.gridSize + "" != value)
        //        {
        //            string _temp = gridmap.gridSize + "";
        //            _temp = value;
        //            OnPropertyChange("GridSize");

        //        }
        //    }
        //}









    }
}
