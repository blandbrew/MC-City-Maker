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

        UI_GridContainer[,] containers_and_squares;

        public ObservableCollection<UI_GridContainer> observable_ui_gridContainer { get; private set; } = new ObservableCollection<UI_GridContainer>();
        public ObservableCollection<UI_Grid_Square> observable_ui_gridSquare { get; private set; } = new ObservableCollection<UI_Grid_Square>();
        //ObservableCollection<UI_GridContainer> observable_ui_gridContainer;
        //ObservableCollection<UI_Grid_Square> observable_ui_gridSquare;

        //initializes 
        public GridMap_ViewModel()
        {
            ui_gridmap = new UI_GridMap();
            //observable_ui_gridContainer = new ObservableCollection<UI_GridContainer>();
            //observable_ui_gridSquare = new ObservableCollection<UI_Grid_Square>();
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
                    if(observable_ui_gridContainer != null)
                    {
                        observable_ui_gridContainer.Clear();   
                    }
                    if(observable_ui_gridSquare != null)
                    {
                        observable_ui_gridSquare.Clear();
                    }
                
                    //Inlitializes the grid with the square root of the selected map size, the container array
                    //is returned to be added to observables
                    int grid_size = (int)Math.Sqrt(value);
                    containers_and_squares = new UI_GridContainer[grid_size, grid_size];
                    containers_and_squares = ui_gridmap.InitializeGrid(grid_size);
                    UpdateGridObservables(containers_and_squares);
                    ui_gridmap.SelectedContainer(containers_and_squares[0, 0]);
     
                }
        }
        #endregion GridMapSize Definition







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



        public void ShowMessage(object obj)
        {
            
            MessageBox.Show("Showme");
        }

 
        public void SelectContainer(object obj)
        {

            UI_GridContainer temp = (UI_GridContainer)obj;
            ui_gridmap.SelectedContainer(temp);
            Console.WriteLine("SelectingContainer");

        }

        public void SelectSquare(object obj)
        {

            UI_Grid_Square temp = (UI_Grid_Square)obj;
            Console.WriteLine("Selectingsquare");

        }



        public void UpdateGridObservables(UI_GridContainer[,] grid)
        {

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
                    //Console.WriteLine("Coordinate is: " + grid[i, j].ui_GridSquares_array[m, n].X + ", " + grid[i, j].ui_GridSquares_array[m, n].Y);

                }
            }

        }



        
        


    }
}
