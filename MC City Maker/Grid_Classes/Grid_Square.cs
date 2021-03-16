//This class defines a 13x13 grid square.
//each can have specific properties such ash
//marking adjacent grids, offsets for roads, subterannian features, etc.

using MC_City_Maker.Structures;
using MC_City_Maker.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MC_City_Maker.Grid_Classes
{
    public class Grid_Square : Grid_Properties, IGrid_Square, INotifyPropertyChanged
    {
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

        public (int, int) ParentContainerArrayCoordinate { get; private set; }
        public (int, int) SquareArrayCoordinate { get; private set; }

        //marked for removal
        public Rectangle gridSquareRectangle { get; set; }



        /*Variables*/
        /// <summary>
        /// Grid Squares that are adjacent, maximum number of 4.  Excludes Diagnal Squares
        /// </summary>
        public HashSet<Grid_Square> adjacent_Squares { get; set; }

        /// <summary>
        /// Squares that have been marked for adjacent building.
        /// </summary>
        public HashSet<Grid_Square> Joined_Squares { get; set; }


        private GridSquare_Zoning _Zone;
        public GridSquare_Zoning Zone
        {
            get { return _Zone; }
            set { _Zone = value; RaisePropertyChanged(nameof(Zone)); }

        }

        //All squres need to designate what the start square for multi grid square buildings
        private bool _EntityStartSquare;
        public bool EntityStartSquare
        {
            get { return _EntityStartSquare; }
            set { _EntityStartSquare = value; RaisePropertyChanged(nameof(EntityStartSquare)); }

        }

        //All squres need to designate what the start square for multi grid square buildings
        private bool _EntityEndSquare;
        public bool EntityEndSquare
        {
            get { return _EntityEndSquare; }
            set { _EntityEndSquare = value; RaisePropertyChanged(nameof(EntityEndSquare)); }
        }

        //This square is identified as one that has an entity placed on it, but it is not the primary square
        private bool _EntitySecondarySquare;
        public bool EntitySecondarySquare
        {
            get { return _EntitySecondarySquare; }
            set { _EntitySecondarySquare = value; RaisePropertyChanged(nameof(EntitySecondarySquare)); }
        }

        //stores a generic building on the grid, remember to cycle through this during generation
        private GenericBuilding building;
        public GenericBuilding Building
        {
            get { return building; }
            set { building = value; RaisePropertyChanged(nameof(Building)); }
        }


        private bool _Selected = false;
        public bool Selected
        {
            get { return _Selected; }
            set { _Selected = value; RaisePropertyChanged(nameof(Selected)); }
        }

        private bool _Placed = false;
        public bool Placed
        {
            get { return _Placed; }
            set { _Placed = value; RaisePropertyChanged(nameof(Placed)); }
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

        private System.Windows.Media.Color _OutlineColor;
        public System.Windows.Media.Color OutlineColor
        {
            get { return _OutlineColor; }
            set { _OutlineColor = value; RaisePropertyChanged(nameof(OutlineColor)); }
        }

        private System.Windows.Media.Color _FillColor;
        public System.Windows.Media.Color FillColor
        {
            get { return _FillColor; }
            set { _FillColor = value; RaisePropertyChanged(nameof(FillColor)); }
        }

        //marked for removal
        //public Grid_Square(double x, double y, double width, double height, Color color, Color fillcolor, (int, int) containerarraycoords, (int, int) SquareCoord)
        //{
        //    X = x;
        //    Y = y;
        //    Width = width;
        //    Height = height;
        //    Color = color;
        //    FillColor = fillcolor;
        //    ParentContainerArrayCoordinate = containerarraycoords;
        //    SquareArrayCoordinate = SquareCoord;
        //    adjacent_Squares = new HashSet<Grid_Square>();

        //}

        public void SetGrid_Square_UI(double x, double y, double width, double height, Color color, Color fillcolor, (int, int) containerarraycoords, (int, int) SquareCoord)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            OutlineColor = color;
            FillColor = fillcolor;
            ParentContainerArrayCoordinate = containerarraycoords;
            SquareArrayCoordinate = SquareCoord;
        }

        /*Constructor*/

        //marked for removal
        //public Grid_Square(Coordinate startPoint)
        //{
        //    startCoordinate = startPoint;
        //    adjacent_Squares = new HashSet<Grid_Square>();
        //}

        /// <summary>
        /// Create Grid squares and define array coordinates for the grid layout
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="parentContainerArrayCoordinate"></param>
        /// <param name="squareArrayCoordinate"></param>
        public Grid_Square(Coordinate startPoint, (int, int) parentContainerArrayCoordinate, (int,int) squareArrayCoordinate)
        {
            startCoordinate = startPoint;
            adjacent_Squares = new HashSet<Grid_Square>();
            SquareArrayCoordinate = squareArrayCoordinate;
            ParentContainerArrayCoordinate = parentContainerArrayCoordinate;
        }


        /*Methods*/
        public void Add_Adjacent_Square(Grid_Square adjacentSquare)
        {
            this.adjacent_Squares.Add(adjacentSquare);
            
        }

        public HashSet<Grid_Square> GetAll_Adjacent_Squares()
        {
            return adjacent_Squares;
            throw new NotImplementedException();
        }


    }
}
