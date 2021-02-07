/**Grid Container Class
 * 
 * Description:
 * Grid container class is the container of smaller grid squares. 
 * Each grid container is 169x169 blocks
 * 
 * 
 */

using MC_City_Maker.UI;
using MC_City_Maker.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MC_City_Maker.Grid_Classes
{
    public class Grid_Container : Grid_Properties, IGrid_Container, INotifyPropertyChanged
    {


        /*Variables*/
        public Grid_Square[,] gridSquareMap { get; set; }
        public (int, int) ContainerArrayUICoordinate { get; private set; }
        public HashSet<Grid_Container> AdjacentContainers { get; set; }



        private Grid_Container _Selected_container;
        /// <summary>
        /// <remark>UI Field - </remark>
        /// The cointainer that is selected on the UI
        /// </summary>
        public Grid_Container Selected_container
        {
            get { return _Selected_container; }
            set { _Selected_container = value; RaisePropertyChanged(nameof(Selected_container)); }

        }

        private Grid_Container _PreviouslySelected_container;
        /// <summary>
        /// <remark>UI Field - </remark>
        /// This is a reference to the previously selected container
        /// </summary>
        public Grid_Container PreviouslySelected_container
        {
            get { return _PreviouslySelected_container; }
            set { _PreviouslySelected_container = value; RaisePropertyChanged(nameof(PreviouslySelected_container));}

        }
        private double _X;
        /// <summary>
        /// <remark>UI Field - </remark>
        /// Stores the X value location on the Canvas
        /// </summary>
        public double X
        {
            get { return _X; }
            set { _X = value; RaisePropertyChanged(nameof(X)); }
        }

        private double _Y;
        /// <summary>
        /// <remark>UI Field - </remark>
        /// Stores the Y value location on the Canvas
        /// </summary>
        public double Y
        {
            get { return _Y; }
            set { _Y = value; RaisePropertyChanged(nameof(Y)); }
        }

        private double _Width;
        /// <summary>
        /// <remark>UI Field - </remark>
        /// Stores the width of the square
        /// </summary>
        public double Width
        {
            get { return _Width; }
            set { _Width = value; RaisePropertyChanged(nameof(Width)); }
        }

        private double _Height;
        /// <summary>
        /// <remark>UI Field - </remark>
        /// Stores the hieght of the square
        /// </summary>
        public double Height
        {
            get { return _Height; }
            set { _Height = value; RaisePropertyChanged(nameof(Height)); }
        }


        //Review to see if necessary
        private System.Windows.Media.Color _Color;
        /// <summary>
        /// <remark>UI Field - </remark>
        /// Stores a color
        /// </summary>
        public System.Windows.Media.Color Color
        {
            get { return _Color; }
            set { _Color = value; RaisePropertyChanged(nameof(Color)); }
        }

        private System.Windows.Media.Color _FillColor;
        /// <summary>
        /// <remark>UI Field - </remark>
        /// The fill color of the square
        /// </summary>
        public System.Windows.Media.Color FillColor
        {
            get { return _FillColor; }
            set { _FillColor = value; RaisePropertyChanged(nameof(FillColor)); }
        }


        /*Constructor*/

        /// <summary>
        /// Constructor with a Coordinate parameter defining the startPoint
        /// </summary>
        /// <param name="startPoint"></param>
        public Grid_Container(Coordinate startPoint)
        {
            startCoordinate = startPoint;
            AdjacentContainers = new HashSet<Grid_Container>();
        }

        /// <summary>
        /// Constructor with a Coordinate parameter defining the startPoint And the container location within the UI Array
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="ContainerArrayCoordinate"></param>
        public Grid_Container(Coordinate startPoint, (int, int) ContainerArrayCoordinate)
        {
            startCoordinate = startPoint;
            AdjacentContainers = new HashSet<Grid_Container>();
            this.ContainerArrayUICoordinate = ContainerArrayCoordinate;
        }


        /// <summary>
        /// Constructor with all parameters necessary to setup a new grid container
        /// </summary>
        /// <param name="startPoint"></param>
        public Grid_Container(double x, double y, Color color, Color fillcolor, (int, int) containerarraycoords)
        {
            X = x;
            Y = y;
            Width = Shared_Constants.UI_GRID_RECTANGLE_SIZE;
            Height = Shared_Constants.UI_GRID_RECTANGLE_SIZE;
            Color = color;
            FillColor = fillcolor;
            ContainerArrayUICoordinate = containerarraycoords;
            AdjacentContainers = new HashSet<Grid_Container>();
            
        }

        /// <summary>
        /// Updates the values of an existing grid container
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="fillcolor"></param>
        /// <param name="containerarraycoords"></param>
        public void Set_Grid_ContainerUIvalues(double x, double y, Color color, Color fillcolor, (int, int) containerarraycoords)
        {
            X = x;
            Y = y;
            Width = Shared_Constants.UI_GRID_RECTANGLE_SIZE;
            Height = Shared_Constants.UI_GRID_RECTANGLE_SIZE;
            Color = color;
            FillColor = fillcolor;
            ContainerArrayUICoordinate = containerarraycoords;
            
        }



        /*Methods*/
        /// <summary>
        /// Adds ann adjacent container to the hashset
        /// </summary>
        /// <param name="adjacentContainer"></param>
        public void Add_Adjacent_Container(Grid_Container adjacentContainer)
        {
            AdjacentContainers.Add(adjacentContainer);
        }

        /// <summary>
        /// Retrieves all adjacent containers from the hashset
        /// </summary>
        /// <returns></returns>
        public HashSet<Grid_Container> GetAll_Adjacent_Containers()
        {
            return AdjacentContainers;
        }


        /// <summary>
        /// Property changed event handles to propagate bindings
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            //Console.WriteLine("Changed: " + propertyName);
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
