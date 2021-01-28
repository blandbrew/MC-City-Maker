/**Grid Container Class
 * 
 * Description:
 * Grid container class is the container of smaller grid squares. 
 * Each grid container is 169x169 blocks
 * 
 * 
 */

using MC_City_Maker.UI;
using MC_City_Maker.UI.Model;
using MC_City_Maker.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MC_City_Maker.Grid_Classes
{
    public class Grid_Container : Grid_Properties, IGrid_Container
    {

        /*Variables*/
        public Grid_Square[,] gridSquareMap { get; set; }
        public (int, int) ContainerArrayUICoordinate { get; private set; }
        public HashSet<Grid_Container> AdjacentContainers { get; set; }

        private UI_GridContainer _Selected_container;
        public UI_GridContainer Selected_container
        {
            get { return _Selected_container; }
            set { _Selected_container = value; RaisePropertyChanged(nameof(Selected_container)); }

        }

        private UI_GridContainer _PreviouslySelected_container;
        public UI_GridContainer PreviouslySelected_container
        {
            get { return _PreviouslySelected_container; }
            set { _PreviouslySelected_container = value; RaisePropertyChanged(nameof(PreviouslySelected_container)); }

        }
        private double _X;
        public double X
        {
            get { return _X; }
            set { _X = value; RaisePropertyChanged(nameof(X)); }
        }

        private double _Y;
        public double Y
        {
            get { return _Y; }
            set { _Y = value; RaisePropertyChanged(nameof(Y)); }
        }

        private double _Width;
        public double Width
        {
            get { return _Width; }
            set { _Width = value; RaisePropertyChanged(nameof(Width)); }
        }

        private double _Height;
        public double Height
        {
            get { return _Height; }
            set { _Height = value; RaisePropertyChanged(nameof(Height)); }
        }

        private System.Windows.Media.Color _Color;
        public System.Windows.Media.Color Color
        {
            get { return _Color; }
            set { _Color = value; RaisePropertyChanged(nameof(Color)); }
        }

        private System.Windows.Media.Color _FillColor;
        public System.Windows.Media.Color FillColor
        {
            get { return _FillColor; }
            set { _FillColor = value; RaisePropertyChanged(nameof(FillColor)); }
        }


        /*Constructor*/
        public Grid_Container(Coordinate startPoint)
        {
            startCoordinate = startPoint;
            AdjacentContainers = new HashSet<Grid_Container>();
        }

        public Grid_Container(Coordinate startPoint, (int, int) ContainerArrayCoordinate)
        {
            startCoordinate = startPoint;
            AdjacentContainers = new HashSet<Grid_Container>();
            this.ContainerArrayUICoordinate = ContainerArrayCoordinate;
        }

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
        public void Add_Adjacent_Container(Grid_Container adjacentContainer)
        {
            AdjacentContainers.Add(adjacentContainer);
        }

        public HashSet<Grid_Container> GetAll_Adjacent_Containers()
        {
            return AdjacentContainers;
        }

    }
}
