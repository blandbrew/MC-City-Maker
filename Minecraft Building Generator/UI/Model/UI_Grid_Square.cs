using Minecraft_Building_Generator.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Minecraft_Building_Generator.UI.Model
{
    public class UI_Grid_Square : GridMap_ViewModel
    {
        /// <summary>
        /// Array Coordinate of the Parent Container
        /// </summary>
        public (int, int) ParentContainerArrayCoordinate { get; private set; }
        /// <summary>
        /// Array Coordinate of this current square
        /// </summary>
        public (int, int) SquareArrayCoordinate { get; private set; }

        public Rectangle rectangle { get; set; }

        //public GridSquare_Zoning zone { get; set; }
        public HashSet<UI_Grid_Square> AdjacentSquares { get; set; }

        public int clicks;

   


        //public (int, int) _ParentContainerArrayCoordinate;
        //public (int, int) ParentContainerArrayCoordinate
        //{
        //    get { return _ParentContainerArrayCoordinate; }
        //    set
        //    {
        //        _ParentContainerArrayCoordinate = value; RaisePropertyChanged(nameof(ParentContainerArrayCoordinate));
        //    }
        //}

        //private (int, int) _SquareArrayCoordinate;
        //public (int, int) SquareArrayCoordinate
        //{
        //    get { return _SquareArrayCoordinate; }
        //    set
        //    {
        //        _SquareArrayCoordinate = value; RaisePropertyChanged(nameof(SquareArrayCoordinate));
        //    }
        //}

        private GridSquare_Zoning _Zone;
        public GridSquare_Zoning Zone
        {
            get { return _Zone; }
            set { _Zone = value; RaisePropertyChanged(nameof(Zone)); }

        }

        private bool _Selected = false;
        public bool Selected
        {
            get { return _Selected; }
            set { _Selected = value; RaisePropertyChanged(nameof(Selected)); }

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

        public UI_Grid_Square(double x, double y, double width, double height, Color color, Color fillcolor, (int, int) containerarraycoords, (int, int) SquareCoord)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Color = color;
            FillColor = fillcolor;
            ParentContainerArrayCoordinate = containerarraycoords;
            SquareArrayCoordinate = SquareCoord;
            AdjacentSquares = new HashSet<UI_Grid_Square>();

        }

       


    }
}
