//This class defines a 13x13 grid square.
//each can have specific properties such ash
//marking adjacent grids, offsets for roads, subterannian features, etc.

using MC_City_Maker.Constants;
using MC_City_Maker.Grid_Zones.Structures;
using MC_City_Maker.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        /// <summary>
        /// Returns true if the square is being hovered over
        /// </summary>
        private bool _HoverStatus;
        public bool HoverStatus
        {
            get { return _HoverStatus; }
            set { _HoverStatus = value; RaisePropertyChanged(nameof(HoverStatus)); }
        }

        //All squres need to designate what the start square for multi grid square buildings
        private Grid_Square _EntityStartSquare;
        public Grid_Square EntityStartSquare
        {
            get { return _EntityStartSquare; }
            set { _EntityStartSquare = value; RaisePropertyChanged(nameof(EntityStartSquare)); }

        }
        private bool _IsEntityStartSquare;
        public bool IsEntityStartSquare
        {
            get { return _IsEntityStartSquare; }
            set { _IsEntityStartSquare = value; RaisePropertyChanged(nameof(IsEntityStartSquare)); }
        }

        //All squres need to designate what the start square for multi grid square buildings
        private Grid_Square _EntityEndSquare;
        public Grid_Square EntityEndSquare
        {
            get { return _EntityEndSquare; }
            set { _EntityEndSquare = value; RaisePropertyChanged(nameof(EntityEndSquare)); }
        }

        private bool _IsEntityEndSquare;
        public bool IsEntityEndSquare
        {
            get { return _IsEntityEndSquare; }
            set { _IsEntityEndSquare = value; RaisePropertyChanged(nameof(IsEntityEndSquare)); }
        }

        private List<Grid_Square> _EntitySecondarySquareList = new List<Grid_Square>();
        /// <summary>
        /// List of entity squares
        /// </summary>
        public List<Grid_Square> EntitySecondarySquareList
        {
            get { return _EntitySecondarySquareList; }
            set { _EntitySecondarySquareList = value; RaisePropertyChanged(nameof(EntitySecondarySquareList)); }
        }


        private bool _IsEntitySecondarySquare;
        /// <summary>
        /// This square is identified as one that has an entity placed on it, but it is not the primary square
        /// </summary>
        public bool IsEntitySecondarySquare
        {
            get { return _IsEntitySecondarySquare; }
            set { _IsEntitySecondarySquare = value; RaisePropertyChanged(nameof(IsEntitySecondarySquare)); }
        }

        //stores a generic building on the grid, remember to cycle through this during generation
        private GenericBuilding building;
        public GenericBuilding Building
        {
            get { return building; }
            set { building = value; RaisePropertyChanged(nameof(Building)); }
        }


        //TODO Need to create a new class to store all the different Generic types for the zones



        private bool _Selected = false;
        /// <summary>
        /// Marks a grid square as having been selected
        /// </summary>
        public bool Selected
        {
            get { return _Selected; }
            set { _Selected = value; RaisePropertyChanged(nameof(Selected)); }
        }

        /*Deprication review- this may be OBE and replaced with the new entity start/stop/secondary*/
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
        public System.Windows.Media.Color FillColor //Should look at setting this to private
        {
            get { return _FillColor; }
            set { //BUG MCB-14
                  _FillColor = value; RaisePropertyChanged(nameof(FillColor)); }

         
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

        /// <summary>
        /// Converts current gridsquare to system.windows.Rect
        /// </summary>
        /// <returns></returns>
        public Rect Convert_GridSqureToRect()
        {
            return new Rect(this.X, this.Y, this.Width, this.Height);
        }


        /*Placing Square Properties*/

        /**Grid Square handling
         * The start square is the main square for the entire placement/deleting.  All squares reference back to the placed entities start square.
         * This way, when a user interacts with the grid, a reference can always be called back to the start square and retrieve all information
         * of any associated square of the entity.
         */


        /// <summary>
        /// Grid Placement - Sets the properties for the entity's start square
        /// </summary>
        /// <param name="startSquare"></param>
        /// <param name="zone"></param>
        public void PlaceStartSquare_set_properties(Grid_Square startSquare, GridSquare_Zoning zone)
        {
            EntityStartSquare = startSquare;
            IsEntityStartSquare = true;
            IsEntitySecondarySquare = false;
            IsEntityEndSquare = false;
            FillColor = UI_Constants.Start_End_GridSquare_Color;
            Zone = zone;
            Selected = true;
        }

        /// <summary>
        /// Grid Placement - Sets the properties for entity's secondary square
        /// </summary>
        /// <param name="secondarySquare"></param>
        /// <param name="startSquare"></param>
        /// <param name="zone"></param>
        public void PlaceSecondarySquare_set_properties(Grid_Square startSquare, GridSquare_Zoning zone)
        {
            EntityStartSquare = startSquare;
            IsEntityStartSquare = false;
            IsEntitySecondarySquare = true;
            IsEntityEndSquare = false;
            FillColor = UI_Constants.Start_End_GridSquare_Color;
            Zone = zone;
            Selected = true;
            startSquare.EntitySecondarySquareList.Add(this); //adds the secondary square to the list inside the start square
        }

        /// <summary>
        /// Grid Placement - Sets the properties for the entity's end square
        /// </summary>
        /// <param name="endSquare"></param>
        /// <param name="startSquare"></param>
        /// <param name="zone"></param>
        public void PlaceEndSquare_set_properties(Grid_Square startSquare, GridSquare_Zoning zone)
        {
            EntityStartSquare = startSquare;
            startSquare.EntityEndSquare = this;
            IsEntityStartSquare = false;
            IsEntitySecondarySquare = false;
            IsEntityEndSquare = true;
            FillColor = UI_Constants.Start_End_GridSquare_Color;
            Zone = zone;
            Selected = true;
            
        }



        public void DeleteSquare_set_propeties()
        {

            EntityStartSquare = null;
            EntityEndSquare = null;
            IsEntityStartSquare = false;
            IsEntitySecondarySquare = false;
            IsEntityEndSquare = false;
            FillColor = UI_Constants.GetZoningColor(GridSquare_Zoning.None);
            Zone = GridSquare_Zoning.None; ;
            Selected = false;


            //if(selectedSquareToDelete.IsEntityStartSquare)
            //{



            //} else if(selectedSquareToDelete.IsEntityEndSquare)
            //{

            //} else if(selectedSquareToDelete.IsEntitySecondarySquare)
            //{

            //}
            //else
            //{
            //   //Not valid selected square was clicked
            //}


        }


        private bool IsSquareFilledCheck()
        {
            if (IsEntityStartSquare || IsEntitySecondarySquare || IsEntityEndSquare)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
